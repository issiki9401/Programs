package com.interconnect.app

import android.content.ComponentName
import android.content.Context
import android.graphics.Bitmap
import android.graphics.Canvas
import android.graphics.Rect
import android.media.MediaMetadata
import android.media.session.MediaController
import android.media.session.MediaSessionManager
import android.media.session.PlaybackState
import android.os.Handler
import android.os.Looper
import android.os.SystemClock
import android.util.Base64
import androidx.core.app.NotificationManagerCompat
import java.io.ByteArrayOutputStream

/**
 * 空的 NotificationListenerService：使用者於「通知存取」授權本 App 後，
 * 才能以 MediaSessionManager.getActiveSessions() 讀取手機上其他 App（Spotify、YT Music…）的播放狀態並控制。
 */
class MediaListener : android.service.notification.NotificationListenerService()

/** 本機（Android）目前播放中的媒體快照。 */
data class LocalMedia(
    val has: Boolean = false,
    val title: String = "",
    val artist: String = "",
    val playing: Boolean = false,
    val positionMs: Long = 0,
    val durationMs: Long = 0,
    val artKey: String = "",
    val artB64: String? = null
)

/**
 * 讀取 Android 本機正在播放的媒體（排除本 App 自己的鏡像工作階段），
 * 供傳給 Windows 鏡像；並接受 Windows 傳來的指令（播放/暫停/上下首/跳轉）控制本機播放器。
 */
class AndroidMediaWatcher(private val ctx: Context, private val onChanged: () -> Unit) {
    private val mgr = ctx.getSystemService(Context.MEDIA_SESSION_SERVICE) as MediaSessionManager
    private val comp = ComponentName(ctx, MediaListener::class.java)
    private val handler = Handler(Looper.getMainLooper())
    private var attached = false
    private var controller: MediaController? = null
    @Volatile var state = LocalMedia()
        private set
    private var stateAt = SystemClock.elapsedRealtime()
    private var artKey = ""
    private var artB64: String? = null

    private val sessionsListener = MediaSessionManager.OnActiveSessionsChangedListener { list -> pick(list ?: emptyList()) }
    private val cb = object : MediaController.Callback() {
        override fun onPlaybackStateChanged(s: PlaybackState?) = recompute()
        override fun onMetadataChanged(m: MediaMetadata?) = recompute()
        override fun onSessionDestroyed() { handler.post { refresh() } }
    }

    companion object {
        fun isListenerEnabled(ctx: Context): Boolean =
            NotificationManagerCompat.getEnabledListenerPackages(ctx).contains(ctx.packageName)
    }

    /** 定期呼叫（主執行緒）：未授權時重試附加；已附加則刷新進度。 */
    fun poll() {
        if (!attached) tryAttach() else recompute()
    }

    private fun tryAttach() {
        if (!isListenerEnabled(ctx)) return
        try {
            mgr.addOnActiveSessionsChangedListener(sessionsListener, comp, handler)
            attached = true
            refresh()
        } catch (_: SecurityException) { attached = false }
    }

    private fun refresh() {
        try { pick(mgr.getActiveSessions(comp)) } catch (_: SecurityException) { attached = false }
    }

    private fun pick(list: List<MediaController>) {
        val cands = list.filter { it.packageName != ctx.packageName }
        val best = cands.firstOrNull { it.playbackState?.state == PlaybackState.STATE_PLAYING } ?: cands.firstOrNull()
        if (best?.sessionToken != controller?.sessionToken) {
            try { controller?.unregisterCallback(cb) } catch (_: Exception) { }
            controller = best
            try { controller?.registerCallback(cb, handler) } catch (_: Exception) { }
        }
        recompute()
    }

    private fun recompute() {
        val c = controller
        val ns: LocalMedia
        if (c == null) ns = LocalMedia()
        else {
            val md = c.metadata
            val ps = c.playbackState
            val title = md?.getString(MediaMetadata.METADATA_KEY_TITLE) ?: md?.getString(MediaMetadata.METADATA_KEY_DISPLAY_TITLE) ?: ""
            val artist = md?.getString(MediaMetadata.METADATA_KEY_ARTIST) ?: md?.getString(MediaMetadata.METADATA_KEY_ALBUM_ARTIST) ?: ""
            val album = md?.getString(MediaMetadata.METADATA_KEY_ALBUM) ?: ""
            val playing = ps?.state == PlaybackState.STATE_PLAYING || ps?.state == PlaybackState.STATE_BUFFERING
            val key = "${c.packageName}|$title|$artist|$album"
            if (key != artKey) { artKey = key; artB64 = encodeArt(md) }
            var pos = ps?.position ?: 0L
            if (pos < 0) pos = 0
            if (playing && ps != null) pos += ((SystemClock.elapsedRealtime() - ps.lastPositionUpdateTime) * ps.playbackSpeed).toLong()
            val dur = md?.getLong(MediaMetadata.METADATA_KEY_DURATION) ?: 0L
            if (dur > 0 && pos > dur) pos = dur
            ns = LocalMedia(true, title, artist, playing, pos, maxOf(dur, 0), key, artB64)
        }
        val o = state
        val expected = o.positionMs + if (o.playing) SystemClock.elapsedRealtime() - stateAt else 0
        val changed = o.has != ns.has || o.title != ns.title || o.artist != ns.artist || o.playing != ns.playing ||
                o.durationMs != ns.durationMs || o.artKey != ns.artKey || Math.abs(expected - ns.positionMs) > 1500
        if (changed) { state = ns; stateAt = SystemClock.elapsedRealtime(); onChanged() }
    }

    private fun encodeArt(md: MediaMetadata?): String? {
        val bmp = md?.getBitmap(MediaMetadata.METADATA_KEY_ALBUM_ART) ?: md?.getBitmap(MediaMetadata.METADATA_KEY_ART)
            ?: md?.getBitmap(MediaMetadata.METADATA_KEY_DISPLAY_ICON) ?: return null
        return try {
            val side = 192
            val out = Bitmap.createBitmap(side, side, Bitmap.Config.ARGB_8888)
            val s = minOf(bmp.width, bmp.height)
            val src = Rect((bmp.width - s) / 2, (bmp.height - s) / 2, (bmp.width + s) / 2, (bmp.height + s) / 2)
            Canvas(out).drawBitmap(bmp, src, Rect(0, 0, side, side), null)
            val bos = ByteArrayOutputStream()
            out.compress(Bitmap.CompressFormat.JPEG, 70, bos)
            Base64.encodeToString(bos.toByteArray(), Base64.NO_WRAP)
        } catch (_: Exception) { null }
    }

    /** 執行 Windows 傳來的媒體指令（於主執行緒呼叫）。 */
    fun command(cmd: String, positionMs: Long) {
        val tc = controller?.transportControls ?: return
        try {
            when (cmd) {
                "play" -> tc.play()
                "pause" -> tc.pause()
                "next" -> tc.skipToNext()
                "prev" -> tc.skipToPrevious()
                "seek" -> tc.seekTo(positionMs)
                else -> if (state.playing) tc.pause() else tc.play()
            }
        } catch (_: Exception) { }
        handler.postDelayed({ recompute() }, 250)
    }

    fun stop() {
        try { controller?.unregisterCallback(cb) } catch (_: Exception) { }
        try { mgr.removeOnActiveSessionsChangedListener(sessionsListener) } catch (_: Exception) { }
        controller = null; attached = false
    }
}
