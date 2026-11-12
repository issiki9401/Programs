#include <jni.h>
#include <stdlib.h>
#include <string.h>
#include <stdatomic.h>
#include <android/log.h>
#include <aaudio/AAudio.h>
#include "opus.h"

#define TAG "interconnect_native"
#define LOGI(...) __android_log_print(ANDROID_LOG_INFO, TAG, __VA_ARGS__)

/* ---------- Opus encoder (RESTRICTED_LOWDELAY) ---------- */
JNIEXPORT jlong JNICALL
Java_com_interconnect_app_NativeAudio_nativeEncoderCreate(JNIEnv *env, jclass clz,
                                                          jint channels, jint bitrateKbps) {
    int err = 0;
    OpusEncoder *enc = opus_encoder_create(48000, channels, OPUS_APPLICATION_RESTRICTED_LOWDELAY, &err);
    if (!enc) return 0;
    opus_encoder_ctl(enc, OPUS_SET_BITRATE(bitrateKbps * 1000));
    return (jlong)(intptr_t)enc;
}

JNIEXPORT jbyteArray JNICALL
Java_com_interconnect_app_NativeAudio_nativeEncode(JNIEnv *env, jclass clz, jlong handle,
                                                   jshortArray pcm, jint frameSamples) {
    OpusEncoder *enc = (OpusEncoder *)(intptr_t)handle;
    if (!enc) return NULL;
    jshort *buf = (*env)->GetShortArrayElements(env, pcm, NULL);
    unsigned char out[4000];
    int n = opus_encode(enc, (const opus_int16 *)buf, frameSamples, out, sizeof(out));
    (*env)->ReleaseShortArrayElements(env, pcm, buf, JNI_ABORT);
    if (n < 0) return NULL;
    jbyteArray res = (*env)->NewByteArray(env, n);
    (*env)->SetByteArrayRegion(env, res, 0, n, (const jbyte *)out);
    return res;
}

/* ---------- Opus decoder (with PLC) ---------- */
JNIEXPORT jlong JNICALL
Java_com_interconnect_app_NativeAudio_nativeDecoderCreate(JNIEnv *env, jclass clz, jint channels) {
    int err = 0;
    OpusDecoder *dec = opus_decoder_create(48000, channels, &err);
    if (!dec) return 0;
    return (jlong)(intptr_t)dec;
}

/* packet[offset, offset+len) 解碼；packet 為 NULL 或 len<=0 時執行 PLC。回傳每聲道樣本數。 */
JNIEXPORT jint JNICALL
Java_com_interconnect_app_NativeAudio_nativeDecode(JNIEnv *env, jclass clz, jlong handle,
                                                    jbyteArray packet, jint offset, jint len,
                                                    jshortArray outPcm, jint maxSamples) {
    OpusDecoder *dec = (OpusDecoder *)(intptr_t)handle;
    if (!dec) return -1;
    jshort *out = (*env)->GetShortArrayElements(env, outPcm, NULL);
    int n;
    if (packet == NULL || len <= 0) {
        n = opus_decode(dec, NULL, 0, (opus_int16 *)out, maxSamples, 0);
    } else {
        jbyte *p = (*env)->GetByteArrayElements(env, packet, NULL);
        n = opus_decode(dec, (const unsigned char *)(p + offset), len, (opus_int16 *)out, maxSamples, 0);
        (*env)->ReleaseByteArrayElements(env, packet, p, JNI_ABORT);
    }
    (*env)->ReleaseShortArrayElements(env, outPcm, out, 0);
    return n;
}

/* ---------- AAudio low-latency player with lock-free SPSC ring buffer ----------
 * 生產者：Java 接收執行緒（解碼後寫入）；消費者：AAudio callback。
 * 以單調遞增的影格計數（wrIdx/rdIdx）實作；buffer 深度不足 PRIME_FRAMES 時輸出靜音並等待補滿，
 * 欠載後自動重新蓄積（對應 Jitter Buffer 的 prefill）；深度超過 MAX_DEPTH 時丟棄新影格以控制延遲。 */
#define RING_FRAMES 16384            /* 2 的次方，約 340ms */
#define RING_MASK (RING_FRAMES - 1)
#define PRIME_FRAMES (48 * 40)       /* 40ms */
#define MAX_DEPTH (48 * 100)         /* 100ms */
static int16_t ring[RING_FRAMES * 2];
static atomic_uint wrIdx, rdIdx;
static atomic_int primed;
static AAudioStream *playerStream = NULL;

static aaudio_data_callback_result_t
playerCallback(AAudioStream *stream, void *userData, void *audioData, int32_t numFrames) {
    int16_t *dst = (int16_t *)audioData;
    unsigned w = atomic_load_explicit(&wrIdx, memory_order_acquire);
    unsigned r = atomic_load_explicit(&rdIdx, memory_order_relaxed);
    unsigned depth = w - r;
    if (!atomic_load(&primed)) {
        if (depth >= PRIME_FRAMES) atomic_store(&primed, 1);
        else { memset(dst, 0, (size_t)numFrames * 2 * sizeof(int16_t)); return AAUDIO_CALLBACK_RESULT_CONTINUE; }
    }
    unsigned n = depth < (unsigned)numFrames ? depth : (unsigned)numFrames;
    for (unsigned i = 0; i < n; i++) {
        unsigned idx = (r + i) & RING_MASK;
        dst[i * 2] = ring[idx * 2];
        dst[i * 2 + 1] = ring[idx * 2 + 1];
    }
    if (n < (unsigned)numFrames) {
        memset(dst + n * 2, 0, ((size_t)numFrames - n) * 2 * sizeof(int16_t)); /* underrun: 靜音填補並重新蓄積 */
        atomic_store(&primed, 0);
    }
    atomic_store_explicit(&rdIdx, r + n, memory_order_release);
    return AAUDIO_CALLBACK_RESULT_CONTINUE;
}

JNIEXPORT jboolean JNICALL
Java_com_interconnect_app_NativeAudio_nativePlayerStart(JNIEnv *env, jclass clz,
                                                        jint sampleRate, jint channels,
                                                        jboolean exclusive) {
    if (playerStream) return JNI_TRUE;
    AAudioStreamBuilder *b;
    if (AAudio_createStreamBuilder(&b) != AAUDIO_OK) return JNI_FALSE;
    AAudioStreamBuilder_setFormat(b, AAUDIO_FORMAT_PCM_I16);
    AAudioStreamBuilder_setSampleRate(b, sampleRate);
    AAudioStreamBuilder_setChannelCount(b, channels);
    AAudioStreamBuilder_setDirection(b, AAUDIO_DIRECTION_OUTPUT);
    AAudioStreamBuilder_setPerformanceMode(b, AAUDIO_PERFORMANCE_MODE_LOW_LATENCY);
    AAudioStreamBuilder_setSharingMode(b, exclusive ? AAUDIO_SHARING_MODE_EXCLUSIVE
                                                    : AAUDIO_SHARING_MODE_SHARED);
    AAudioStreamBuilder_setDataCallback(b, playerCallback, NULL);
    atomic_store(&wrIdx, 0); atomic_store(&rdIdx, 0); atomic_store(&primed, 0);
    aaudio_result_t r = AAudioStreamBuilder_openStream(b, &playerStream);
    AAudioStreamBuilder_delete(b);
    if (r != AAUDIO_OK || !playerStream) { playerStream = NULL; return JNI_FALSE; }
    if (AAudioStream_getFormat(playerStream) != AAUDIO_FORMAT_PCM_I16 ||
        AAudioStream_getSampleRate(playerStream) != sampleRate ||
        AAudioStream_getChannelCount(playerStream) != channels) {
        AAudioStream_close(playerStream);
        playerStream = NULL;
        return JNI_FALSE;
    }
    if (AAudioStream_requestStart(playerStream) != AAUDIO_OK) {
        AAudioStream_close(playerStream);
        playerStream = NULL;
        return JNI_FALSE;
    }
    LOGI("AAudio player started, sharing=%s",
         AAudioStream_getSharingMode(playerStream) == AAUDIO_SHARING_MODE_EXCLUSIVE ? "exclusive" : "shared");
    return JNI_TRUE;
}

/* frames = 每聲道樣本數；pcm 為交錯立體聲 */
JNIEXPORT void JNICALL
Java_com_interconnect_app_NativeAudio_nativePlayerWrite(JNIEnv *env, jclass clz, jshortArray pcm, jint frames) {
    if (!playerStream || frames <= 0) return;
    unsigned w = atomic_load_explicit(&wrIdx, memory_order_relaxed);
    unsigned r = atomic_load_explicit(&rdIdx, memory_order_acquire);
    if ((w - r) + (unsigned)frames > MAX_DEPTH) return; /* 延遲過大：丟棄 */
    jshort *src = (*env)->GetShortArrayElements(env, pcm, NULL);
    for (int f = 0; f < frames; f++) {
        unsigned idx = (w + (unsigned)f) & RING_MASK;
        ring[idx * 2] = src[f * 2];
        ring[idx * 2 + 1] = src[f * 2 + 1];
    }
    (*env)->ReleaseShortArrayElements(env, pcm, src, JNI_ABORT);
    atomic_store_explicit(&wrIdx, w + (unsigned)frames, memory_order_release);
}

JNIEXPORT jint JNICALL
Java_com_interconnect_app_NativeAudio_nativePlayerDepth(JNIEnv *env, jclass clz) {
    return (jint)(atomic_load(&wrIdx) - atomic_load(&rdIdx));
}

JNIEXPORT void JNICALL
Java_com_interconnect_app_NativeAudio_nativePlayerStop(JNIEnv *env, jclass clz) {
    if (!playerStream) return;
    AAudioStream_requestStop(playerStream);
    AAudioStream_close(playerStream);
    playerStream = NULL;
}

JNIEXPORT void JNICALL
Java_com_interconnect_app_NativeAudio_nativeEncoderDestroy(JNIEnv *env, jclass clz, jlong h) {
    if (h) opus_encoder_destroy((OpusEncoder *)(intptr_t)h);
}

JNIEXPORT void JNICALL
Java_com_interconnect_app_NativeAudio_nativeDecoderDestroy(JNIEnv *env, jclass clz, jlong h) {
    if (h) opus_decoder_destroy((OpusDecoder *)(intptr_t)h);
}
