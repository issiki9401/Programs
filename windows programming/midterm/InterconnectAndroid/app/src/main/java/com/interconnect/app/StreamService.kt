package com.interconnect.app

import android.app.Notification
import android.app.NotificationChannel
import android.app.NotificationManager
import android.app.PendingIntent
import android.app.Service
import android.content.BroadcastReceiver
import android.content.Context
import android.content.Intent
import android.content.pm.ServiceInfo
import android.graphics.Bitmap
import android.graphics.BitmapFactory
import android.media.MediaMetadata
import android.media.session.MediaSession
import android.media.session.PlaybackState
import android.net.wifi.WifiManager
import android.os.Build
import android.os.Handler
import android.os.IBinder
import android.os.Looper
import android.os.PowerManager
import android.util.Base64

/**
 * 前台服務（常駐維持連線與多媒體雙向同步）：
 *  - 對端 → 本機：把 Windows 的播放狀態鏡像到 MediaSession 與通知列控制項，點擊按鈕轉成 mediaCmd 送回 Windows；
 *  - 本機 → 對端：讀取手機正在播放的媒體（需通知存取授權）送給 Windows 鏡像，並執行 Windows 傳來的控制指令。
 */
class StreamService : Service(), Net.Listener {

    private var wakeLock: PowerManager.WakeLock? = null
    private var wifiLock: WifiManager.WifiLock? = null
    private var mediaSession: MediaSession? = null
    private var watcher: AndroidMediaWatcher? = null
    private val handler = Handler(Looper.getMainLooper())
    private var started = false

    private var peerArt: Bitmap? = null
    private var peerTitle = ""
    private var peerArtist = ""
    private var peerPlaying = false
    private var peerHas = false
    private var peerPos = 0L
    private var peerDur = 0L

    private var artSentKey = "\u0000"
    private var lastPeerName: String? = null

    companion object {
        @Volatile var instance: StreamService? = null
        const val CHANNEL = "interconnect_stream"
        const val NOTIF_ID = 1
        const val ACTION_MEDIA_CMD = "com.interconnect.app.MEDIA_CMD"
    }

    /** 通知欄按鈕接收器：將上一首/播放/暫停/下一首轉發為 mediaCmd 給對端。 */
    class MediaCmdReceiver : BroadcastReceiver() {
        override fun onReceive(context: Context?, intent: Intent?) {
            val cmd = intent?.getStringExtra("cmd") ?: return
            Net.send(CtrlMsg(t = "mediaCmd", cmd = cmd))
        }
    }

    override fun onBind(intent: Intent?): IBinder? = null

    override fun onStartCommand(intent: Intent?, flags: Int, startId: Int): Int {
        instance = this
        createChannel()
        enterForeground()

        if (!started) {
            started = true
            acquireLocks()
            setupMediaSession()
            Net.start(this)
            Net.listeners.add(this)
            watcher = AndroidMediaWatcher(this) { handler.post { sendMedia() } }
            handler.post(object : Runnable {
                override fun run() {
                    watcher?.poll()
                    if (Net.connected) sendMedia()
                    handler.postDelayed(this, 2000)
                }
            })
        }
        updateNotification()
        return START_STICKY
    }

    private fun enterForeground() {
        val notif = buildNotification()
        try {
            if (Build.VERSION.SDK_INT >= 29) {
                startForeground(NOTIF_ID, notif, ServiceInfo.FOREGROUND_SERVICE_TYPE_MEDIA_PLAYBACK)
            } else startForeground(NOTIF_ID, notif)
        } catch (_: Exception) {
            try { startForeground(NOTIF_ID, notif) } catch (_: Exception) { }
        }
    }

    override fun onConnection() {
        handler.post {
            val peer = Net.peer
            if (peer?.name != lastPeerName) { lastPeerName = peer?.name; artSentKey = "\u0000" }
            updateNotification()
            if (Net.connected) sendMedia() else clearPeerMedia()
        }
    }

    override fun onPeerMedia(m: CtrlMsg) {
        handler.post {
            m.art?.let { b64 ->
                peerArt = if (b64.isEmpty()) null else try {
                    val bytes = Base64.decode(b64, Base64.DEFAULT)
                    BitmapFactory.decodeByteArray(bytes, 0, bytes.size)
                } catch (_: Exception) { null }
            }
            peerHas = m.hasControls
            peerTitle = m.title ?: ""
            peerArtist = m.artist ?: ""
            peerPlaying = m.playing
            peerPos = m.positionMs
            peerDur = m.durationMs
            applyPeerMedia()
            updateNotification()
        }
    }

    override fun onMediaCmd(m: CtrlMsg) {
        val cmd = m.cmd ?: return
        handler.post { watcher?.command(cmd, m.positionMs) }
    }

    private fun clearPeerMedia() {
        peerHas = false; peerPlaying = false; peerTitle = ""; peerArtist = ""; peerArt = null
        applyPeerMedia(); updateNotification()
    }

    private fun sendMedia() {
        val w = watcher ?: return
        if (!Net.connected) return
        val st = w.state
        val msg = CtrlMsg(
            t = "media", title = st.title, artist = st.artist, playing = st.playing, hasControls = st.has,
            positionMs = st.positionMs, durationMs = st.durationMs
        )
        if (st.artKey != artSentKey) { msg.art = st.artB64 ?: ""; artSentKey = st.artKey }
        Net.send(msg)
    }

    private fun setupMediaSession() {
        if (mediaSession != null) return
        mediaSession = MediaSession(this, "Interconnect").apply {
            setCallback(object : MediaSession.Callback() {
                override fun onPlay() { Net.send(CtrlMsg(t = "mediaCmd", cmd = "play")) }
                override fun onPause() { Net.send(CtrlMsg(t = "mediaCmd", cmd = "pause")) }
                override fun onSkipToNext() { Net.send(CtrlMsg(t = "mediaCmd", cmd = "next")) }
                override fun onSkipToPrevious() { Net.send(CtrlMsg(t = "mediaCmd", cmd = "prev")) }
                override fun onSeekTo(pos: Long) { Net.send(CtrlMsg(t = "mediaCmd", cmd = "seek", positionMs = pos)) }
            })
        }
    }

    private fun applyPeerMedia() {
        val ms = mediaSession ?: return
        if (!peerHas) { ms.isActive = false; return }
        val actions = PlaybackState.ACTION_PLAY or PlaybackState.ACTION_PAUSE or PlaybackState.ACTION_PLAY_PAUSE or
                PlaybackState.ACTION_SKIP_TO_NEXT or PlaybackState.ACTION_SKIP_TO_PREVIOUS or
                (if (peerDur > 0) PlaybackState.ACTION_SEEK_TO else 0L)
        ms.setMetadata(
            MediaMetadata.Builder()
                .putString(MediaMetadata.METADATA_KEY_TITLE, peerTitle.ifEmpty { "Interconnect" })
                .putString(MediaMetadata.METADATA_KEY_ARTIST, peerArtist.ifEmpty { Net.peer?.name ?: "Windows" })
                .putLong(MediaMetadata.METADATA_KEY_DURATION, if (peerDur > 0) peerDur else -1L)
                .apply { peerArt?.let { putBitmap(MediaMetadata.METADATA_KEY_ALBUM_ART, it) } }
                .build()
        )
        ms.setPlaybackState(
            PlaybackState.Builder()
                .setActions(actions)
                .setState(
                    if (peerPlaying) PlaybackState.STATE_PLAYING else PlaybackState.STATE_PAUSED,
                    if (peerDur > 0) peerPos else PlaybackState.PLAYBACK_POSITION_UNKNOWN,
                    if (peerPlaying) 1f else 0f
                ).build()
        )
        ms.isActive = true
    }

    private fun createChannel() {
        val nm = getSystemService(NotificationManager::class.java)
        nm.createNotificationChannel(NotificationChannel(CHANNEL, "Interconnect 多媒體同步", NotificationManager.IMPORTANCE_LOW))
    }

    private fun cmdIntent(code: Int, cmd: String): PendingIntent = PendingIntent.getBroadcast(
        this, code,
        Intent(this, MediaCmdReceiver::class.java).setAction(ACTION_MEDIA_CMD).putExtra("cmd", cmd),
        PendingIntent.FLAG_IMMUTABLE or PendingIntent.FLAG_UPDATE_CURRENT
    )

    private fun statusText(): String {
        val p = Net.peer
        return if (!Net.connected) "尚未連線" else "已連線至 ${p?.name} · 多媒體同步中"
    }

    private fun buildNotification(): Notification {
        val open = PendingIntent.getActivity(this, 0, Intent(this, MainActivity::class.java), PendingIntent.FLAG_IMMUTABLE)
        val b = Notification.Builder(this, CHANNEL)
            .setContentTitle(if (peerHas && peerTitle.isNotEmpty()) peerTitle else "Interconnect")
            .setContentText(if (peerHas && peerArtist.isNotEmpty()) peerArtist else statusText())
            .setSubText(statusText())
            .setSmallIcon(R.drawable.ic_notif)
            .setContentIntent(open)
            .setOngoing(true)
            .setShowWhen(false)
        if (peerHas) {
            peerArt?.let { b.setLargeIcon(it) }
            b.addAction(Notification.Action.Builder(android.R.drawable.ic_media_previous, "上一首", cmdIntent(11, "prev")).build())
            b.addAction(Notification.Action.Builder(
                if (peerPlaying) android.R.drawable.ic_media_pause else android.R.drawable.ic_media_play,
                if (peerPlaying) "暫停" else "播放", cmdIntent(12, if (peerPlaying) "pause" else "play")).build())
            b.addAction(Notification.Action.Builder(android.R.drawable.ic_media_next, "下一首", cmdIntent(13, "next")).build())
            mediaSession?.let { b.setStyle(Notification.MediaStyle().setMediaSession(it.sessionToken).setShowActionsInCompactView(0, 1, 2)) }
        }
        return b.build()
    }

    private fun updateNotification() {
        try { getSystemService(NotificationManager::class.java).notify(NOTIF_ID, buildNotification()) } catch (_: Exception) { }
    }

    private fun acquireLocks() {
        try {
            val pm = getSystemService(Context.POWER_SERVICE) as PowerManager
            wakeLock = pm.newWakeLock(PowerManager.PARTIAL_WAKE_LOCK, "interconnect:stream").apply { setReferenceCounted(false); acquire() }
            val wm = applicationContext.getSystemService(Context.WIFI_SERVICE) as WifiManager
            val mode = if (Build.VERSION.SDK_INT >= 29) WifiManager.WIFI_MODE_FULL_LOW_LATENCY else @Suppress("DEPRECATION") WifiManager.WIFI_MODE_FULL_HIGH_PERF
            wifiLock = wm.createWifiLock(mode, "interconnect:wifi").apply { setReferenceCounted(false); acquire() }
        } catch (_: Exception) { }
    }

    override fun onDestroy() {
        instance = null
        Net.listeners.remove(this)
        handler.removeCallbacksAndMessages(null)
        watcher?.stop()
        try { wakeLock?.release() } catch (_: Exception) { }
        try { wifiLock?.release() } catch (_: Exception) { }
        mediaSession?.release()
        mediaSession = null
        started = false
        super.onDestroy()
    }
}
