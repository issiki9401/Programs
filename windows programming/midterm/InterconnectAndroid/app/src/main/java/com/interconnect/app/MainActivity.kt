package com.interconnect.app

import android.app.Activity
import android.content.Intent
import android.content.pm.PackageManager
import android.graphics.Bitmap
import android.graphics.BitmapFactory
import android.graphics.Canvas
import android.graphics.Color
import android.graphics.Paint
import android.graphics.Path
import android.graphics.RectF
import android.os.Build
import android.os.Bundle
import android.util.Base64
import android.view.Gravity
import android.view.View
import android.view.ViewGroup
import android.widget.FrameLayout
import android.widget.LinearLayout
import android.widget.ScrollView
import android.widget.TextView

/** MD3 主題色票。 */
object Md3 {
    @Volatile var dark: Boolean = true
    private val dSurface = Color.rgb(11, 14, 17)
    private val dContainer = Color.rgb(21, 24, 28)
    private val dContainerHigh = Color.rgb(29, 33, 38)
    private val dOutline = Color.rgb(68, 72, 77)
    private val dOnSurface = Color.rgb(226, 225, 229)
    private val dOnVariant = Color.rgb(195, 198, 207)
    private val dPrimary = Color.rgb(168, 199, 250)
    private val dOnPrimary = Color.rgb(6, 46, 111)
    private val dPrimaryContainer = Color.rgb(8, 66, 160)
    private val lSurface = Color.rgb(247, 248, 250)
    private val lContainer = Color.rgb(255, 255, 255)
    private val lContainerHigh = Color.rgb(240, 242, 246)
    private val lOutline = Color.rgb(196, 199, 199)
    private val lOnSurface = Color.rgb(26, 28, 30)
    private val lOnVariant = Color.rgb(68, 71, 78)
    private val lPrimary = Color.rgb(11, 87, 208)
    private val lOnPrimary = Color.rgb(255, 255, 255)
    private val lPrimaryContainer = Color.rgb(211, 227, 253)

    val surface: Int get() = if (dark) dSurface else lSurface
    val container: Int get() = if (dark) dContainer else lContainer
    val containerHigh: Int get() = if (dark) dContainerHigh else lContainerHigh
    val outline: Int get() = if (dark) dOutline else lOutline
    val onSurface: Int get() = if (dark) dOnSurface else lOnSurface
    val onVariant: Int get() = if (dark) dOnVariant else lOnVariant
    val primary: Int get() = if (dark) dPrimary else lPrimary
    val onPrimary: Int get() = if (dark) dOnPrimary else lOnPrimary
    val primaryContainer: Int get() = if (dark) dPrimaryContainer else lPrimaryContainer
}

/** 線條風格圖標。 */
object LineIcon {
    fun door(c: Canvas, r: RectF, color: Int, w: Float) {
        val p = Paint(Paint.ANTI_ALIAS_FLAG).apply { this.color = color; style = Paint.Style.STROKE; strokeWidth = w; strokeCap = Paint.Cap.ROUND; strokeJoin = Paint.Join.ROUND }
        val x = r.left + r.width() * 0.16f; val y = r.top + r.height() * 0.10f
        val ww = r.width() * 0.40f; val hh = r.height() * 0.80f
        c.drawRect(x, y, x + ww, y + hh, p)
        val ay = y + hh / 2
        val ax0 = x + ww * 0.5f; val ax1 = r.right - r.width() * 0.10f
        c.drawLine(ax0, ay, ax1, ay, p)
        c.drawLine(ax1 - r.width() * 0.16f, ay - r.height() * 0.14f, ax1, ay, p)
        c.drawLine(ax1 - r.width() * 0.16f, ay + r.height() * 0.14f, ax1, ay, p)
    }
    fun chevron(c: Canvas, r: RectF, color: Int, w: Float) {
        val p = Paint(Paint.ANTI_ALIAS_FLAG).apply { this.color = color; style = Paint.Style.STROKE; strokeWidth = w; strokeCap = Paint.Cap.ROUND; strokeJoin = Paint.Join.ROUND }
        val path = Path()
        path.moveTo(r.left + r.width() * 0.22f, r.top + r.height() * 0.36f)
        path.lineTo(r.left + r.width() * 0.5f, r.top + r.height() * 0.68f)
        path.lineTo(r.left + r.width() * 0.78f, r.top + r.height() * 0.36f)
        c.drawPath(path, p)
    }
}

/** Android 主介面：MD3 風格、網路延遲監控面板、遠端多媒體控制卡片、區域網路裝置列表。 */
class MainActivity : Activity() {

    private lateinit var root: FrameLayout
    private lateinit var monitor: MonitorView
    private lateinit var infoText: TextView
    private lateinit var pingText: TextView
    private lateinit var mediaCard: MediaCardView
    private lateinit var deviceList: LinearLayout
    private lateinit var themeBtn: IconView
    private lateinit var accessHint: TextView
    private val history = ArrayDeque<Double>(60)
    private val REQ_NOTIF = 1003

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        Md3.dark = getSharedPreferences("ui", MODE_PRIVATE).getBoolean("dark", true)
        for (i in 0 until 60) history.addLast(12.0 + Math.sin(i * 0.25) * 4.0)
        root = FrameLayout(this)
        buildUi()
        setContentView(root)
        window.decorView.systemUiVisibility = (View.SYSTEM_UI_FLAG_LAYOUT_STABLE or View.SYSTEM_UI_FLAG_LAYOUT_FULLSCREEN)
        window.statusBarColor = Color.TRANSPARENT
        root.setPadding(0, statusBarHeight(), 0, 0)

        if (Build.VERSION.SDK_INT >= 33 && checkSelfPermission(android.Manifest.permission.POST_NOTIFICATIONS) != PackageManager.PERMISSION_GRANTED)
            requestPermissions(arrayOf(android.Manifest.permission.POST_NOTIFICATIONS), REQ_NOTIF)
        Net.start(this)
        startStreamService()
        Net.listeners.add(netListener)
        promptBatteryOptimizationOnce()
        refreshDevices()
        refreshStats()
    }

    private val netListener = object : Net.Listener {
        override fun onDevices() { runOnUiThread { refreshDevices() } }
        override fun onConnection() {
            runOnUiThread {
                refreshDevices()
                refreshStats()
                mediaCard.invalidate()
            }
        }
        override fun onPeerMedia(m: CtrlMsg) {
            runOnUiThread { mediaCard.updateArt(Net.peerArtB64); mediaCard.invalidate() }
        }
        override fun onStats() {
            runOnUiThread {
                val v = if (Net.connected && Net.stats.rttMs >= 0) Net.stats.rttMs else 14.0
                history.addLast(v)
                if (history.size > 60) history.removeFirst()
                monitor.history = history.toList()
                monitor.invalidate()
                refreshStats()
                mediaCard.invalidate()
            }
        }
        override fun onToast(msg: String) { runOnUiThread { android.widget.Toast.makeText(this@MainActivity, msg, android.widget.Toast.LENGTH_LONG).show() } }
    }

    private fun startStreamService() {
        val i = Intent(this, StreamService::class.java)
        try { androidx.core.content.ContextCompat.startForegroundService(this, i) } catch (_: Exception) { try { startService(i) } catch (_: Exception) { } }
    }

    override fun onResume() {
        super.onResume()
        refreshDevices()
        refreshAccessHint()
        mediaCard.invalidate()
    }

    override fun onDestroy() {
        Net.listeners.remove(netListener)
        super.onDestroy()
    }

    private fun promptBatteryOptimizationOnce() {
        val sp = getSharedPreferences("ui", MODE_PRIVATE)
        if (sp.getBoolean("battery_asked", false)) return
        sp.edit().putBoolean("battery_asked", true).apply()
        try {
            val pm = getSystemService(android.content.Context.POWER_SERVICE) as android.os.PowerManager
            if (!pm.isIgnoringBatteryOptimizations(packageName)) {
                startActivity(Intent(android.provider.Settings.ACTION_REQUEST_IGNORE_BATTERY_OPTIMIZATIONS)
                    .setData(android.net.Uri.parse("package:$packageName")))
            }
        } catch (_: Exception) { }
    }

    private fun marginPx(): Int {
        val wDp = resources.displayMetrics.widthPixels / resources.displayMetrics.density
        return dp(if (wDp < 380) 14 else if (wDp < 600) 20 else 28)
    }
    private fun gapPx(): Int = dp(14)

    private fun buildUi() {
        val m = marginPx(); val gap = gapPx()
        root.setBackgroundColor(Md3.surface)
        val col = LinearLayout(this).apply {
            orientation = LinearLayout.VERTICAL
            setPadding(m, dp(18), m, m)
        }

        // 1. 標頭
        val header = LinearLayout(this).apply { orientation = LinearLayout.HORIZONTAL; gravity = Gravity.CENTER_VERTICAL }
        header.addView(IconView(this) { c, r, color, w -> LineIcon.door(c, r, color, w) }, LinearLayout.LayoutParams(dp(30), dp(30)))
        header.addView(TextView(this).apply {
            text = "INTER"; textSize = 17f; setTextColor(Md3.onSurface)
            setTypeface(typeface, android.graphics.Typeface.BOLD)
        }, LinearLayout.LayoutParams(ViewGroup.LayoutParams.WRAP_CONTENT, ViewGroup.LayoutParams.WRAP_CONTENT).apply { leftMargin = dp(10) })
        header.addView(TextView(this).apply {
            text = "connect"; textSize = 13f; setTextColor(Md3.onVariant)
        }, LinearLayout.LayoutParams(0, ViewGroup.LayoutParams.WRAP_CONTENT, 1f).apply { leftMargin = dp(5) })
        themeBtn = IconView(this) { c, r, color, w -> LineIcon.chevron(c, r, color, w) }
        themeBtn.setOnClickListener { v ->
            val menu = android.widget.PopupMenu(this, v)
            menu.menu.add(0, 1, 0, "Dark").setCheckable(true).isChecked = Md3.dark
            menu.menu.add(0, 2, 1, "Light").setCheckable(true).isChecked = !Md3.dark
            menu.setOnMenuItemClickListener { item ->
                Md3.dark = item.itemId == 1
                getSharedPreferences("ui", MODE_PRIVATE).edit().putBoolean("dark", Md3.dark).apply()
                rebuildUi()
                true
            }
            menu.show()
        }
        header.addView(themeBtn, LinearLayout.LayoutParams(dp(34), dp(34)))
        col.addView(header)

        // 2. 網路延遲監控卡片
        val monCard = LinearLayout(this).apply {
            orientation = LinearLayout.HORIZONTAL
            gravity = Gravity.CENTER_VERTICAL
            setPadding(dp(14), dp(12), dp(14), dp(12))
            background = RoundBg(Md3.container, Md3.outline, dp(18))
        }
        monitor = MonitorView(this).apply { history = this@MainActivity.history.toList() }
        monCard.addView(monitor, LinearLayout.LayoutParams(0, dp(88), 1.15f))
        val infoCol = LinearLayout(this).apply { orientation = LinearLayout.VERTICAL }
        infoText = TextView(this).apply { textSize = 10.5f; setTextColor(Md3.onVariant); setLineSpacing(0f, 1.15f) }
        pingText = TextView(this).apply { textSize = 17f; setTextColor(Md3.primary); setTypeface(typeface, android.graphics.Typeface.BOLD) }
        infoCol.addView(infoText)
        infoCol.addView(pingText, LinearLayout.LayoutParams(ViewGroup.LayoutParams.WRAP_CONTENT, ViewGroup.LayoutParams.WRAP_CONTENT).apply { topMargin = dp(4) })
        monCard.addView(infoCol, LinearLayout.LayoutParams(0, ViewGroup.LayoutParams.WRAP_CONTENT, 1f).apply { leftMargin = dp(14) })
        col.addView(monCard, LinearLayout.LayoutParams(ViewGroup.LayoutParams.MATCH_PARENT, ViewGroup.LayoutParams.WRAP_CONTENT).apply { topMargin = gap })

        // 3. 多媒體遠端控制卡片
        mediaCard = MediaCardView(this)
        mediaCard.updateArt(Net.peerArtB64)
        col.addView(mediaCard, LinearLayout.LayoutParams(ViewGroup.LayoutParams.MATCH_PARENT, dp(156)).apply { topMargin = gap })

        // 4. 通知存取授權提示
        accessHint = TextView(this).apply {
            text = "點此開啟「通知存取」，讓電腦也能同步顯示並控制這支手機的播放"
            textSize = 11.5f; setTextColor(Md3.primary)
            setPadding(dp(14), dp(10), dp(14), dp(10))
            background = RoundBg(Md3.container, Md3.outline, dp(14))
            setOnClickListener {
                try { startActivity(Intent(android.provider.Settings.ACTION_NOTIFICATION_LISTENER_SETTINGS)) } catch (_: Exception) { }
            }
        }
        col.addView(accessHint, LinearLayout.LayoutParams(ViewGroup.LayoutParams.MATCH_PARENT, ViewGroup.LayoutParams.WRAP_CONTENT).apply { topMargin = gap })
        refreshAccessHint()

        // 5. 裝置列表
        val scroll = ScrollView(this)
        deviceList = LinearLayout(this).apply { orientation = LinearLayout.VERTICAL; setPadding(0, dp(4), 0, 0) }
        scroll.addView(deviceList)
        col.addView(scroll, LinearLayout.LayoutParams(ViewGroup.LayoutParams.MATCH_PARENT, 0, 1f).apply { topMargin = gap })

        root.addView(col)
    }

    private fun rebuildUi() {
        root.removeAllViews()
        buildUi()
        refreshDevices()
        refreshStats()
    }

    private fun refreshAccessHint() {
        if (!::accessHint.isInitialized) return
        accessHint.visibility = if (AndroidMediaWatcher.isListenerEnabled(this)) View.GONE else View.VISIBLE
    }

    private fun refreshDevices() {
        deviceList.removeAllViews()
        val devs = Net.devices.values
            .filter { !Net.isSelf(it) && (it.online || (Net.connected && Net.peer?.name == it.name)) }
            .sortedWith(compareByDescending<DeviceInfo> { Net.connected && Net.peer?.name == it.name }.thenBy { it.name })
        if (devs.isEmpty()) {
            deviceList.addView(TextView(this).apply {
                text = "掃描中…尚未發現裝置（請確認兩台裝置在同一區網）"; textSize = 12f; setTextColor(Md3.onVariant)
                setPadding(dp(6), dp(10), 0, 0)
            })
        }
        for (d in devs) {
            val isConn = Net.peer?.name == d.name && Net.connected
            val card = DeviceCardView(this, d, isConn)
            card.onConnect = { dev -> Net.connectAsync(dev) { runOnUiThread { refreshDevices() } } }
            card.onDisconnect = { Net.disconnect(); refreshDevices() }
            deviceList.addView(card, LinearLayout.LayoutParams(ViewGroup.LayoutParams.MATCH_PARENT, dp(if (isConn) 96 else 68)).apply { bottomMargin = dp(12) })
        }
    }

    private fun refreshStats() {
        val ip = if (Net.connected && !Net.peer?.ip.isNullOrEmpty()) Net.peer!!.ip else Net.stats.localIp
        val status = if (Net.connected) "已連線 (${Net.peer?.platform})" else "區網探索中"
        infoText.text = "IP: $ip\n狀態: $status\n同步事件: ${Net.stats.syncCount} 次"
        pingText.text = if (Net.connected && Net.stats.rttMs >= 0) "${Net.stats.rttMs.toInt()} ms" else "LAN Ready"
    }

    private fun dp(v: Int) = (v * resources.displayMetrics.density).toInt()

    private fun statusBarHeight(): Int {
        val res = resources.getIdentifier("status_bar_height", "dimen", "android")
        return if (res > 0) resources.getDimensionPixelSize(res) else dp(24)
    }

    private class RoundBg(private val fill: Int, private val stroke: Int, private val rad: Int) : android.graphics.drawable.ColorDrawable(Color.TRANSPARENT) {
        override fun draw(canvas: Canvas) {
            val p = Paint(Paint.ANTI_ALIAS_FLAG)
            val r = RectF(bounds).apply { inset(1f, 1f) }
            p.style = Paint.Style.FILL; p.color = fill
            canvas.drawRoundRect(r, rad.toFloat(), rad.toFloat(), p)
            p.style = Paint.Style.STROKE; p.color = stroke; p.strokeWidth = 1.4f
            canvas.drawRoundRect(r, rad.toFloat(), rad.toFloat(), p)
        }
    }

    private class IconView(ctx: android.content.Context, private val drawer: (Canvas, RectF, Int, Float) -> Unit) : View(ctx) {
        init { isClickable = true }
        override fun onDraw(canvas: Canvas) {
            val pad = width * 0.14f
            drawer(canvas, RectF(pad, pad, width - pad, height - pad), Md3.onSurface, 2f * resources.displayMetrics.density)
        }
    }

    private class MonitorView(ctx: android.content.Context) : View(ctx) {
        var history: List<Double> = emptyList()
        private val paint = Paint(Paint.ANTI_ALIAS_FLAG)
        override fun onDraw(canvas: Canvas) {
            if (history.size < 2) return
            val max = (history.maxOf { it } * 1.2).coerceAtLeast(25.0)
            paint.style = Paint.Style.STROKE
            paint.strokeWidth = dpf(2)
            paint.color = Md3.primary
            paint.strokeJoin = Paint.Join.ROUND
            val path = Path()
            for (i in history.indices) {
                val x = dpf(4) + i * (width - dpf(8)) / (history.size - 1).toFloat()
                val y = height - dpf(6) - (history[i] / max * (height - dpf(14))).toFloat()
                if (i == 0) path.moveTo(x, y) else path.lineTo(x, y)
            }
            canvas.drawPath(path, paint)
        }
        private fun dpf(v: Int) = v * resources.displayMetrics.density
    }

    /** 遠端多媒體播放控制卡片。 */
    private class MediaCardView(ctx: android.content.Context) : View(ctx) {
        private val paint = Paint(Paint.ANTI_ALIAS_FLAG)
        private var artBmp: Bitmap? = null
        private var lastArtB64: String? = null

        init {
            setOnTouchListener { _, ev ->
                if (ev.action == android.view.MotionEvent.ACTION_UP) {
                    val w = width.toFloat(); val h = height.toFloat()
                    val cy = h - dpf(28)
                    val cx = w / 2f
                    if (ev.y > h - dpf(54)) {
                        when {
                            Math.abs(ev.x - cx) < dpf(28) -> Net.send(CtrlMsg(t = "mediaCmd", cmd = if (Net.peerPlaying) "pause" else "play"))
                            ev.x < cx - dpf(28) && ev.x > cx - dpf(90) -> Net.send(CtrlMsg(t = "mediaCmd", cmd = "prev"))
                            ev.x > cx + dpf(28) && ev.x < cx + dpf(90) -> Net.send(CtrlMsg(t = "mediaCmd", cmd = "next"))
                        }
                    }
                }
                true
            }
            isClickable = true
        }

        fun updateArt(b64: String?) {
            if (b64 == lastArtB64) return
            lastArtB64 = b64
            artBmp = if (b64.isNullOrEmpty()) null else try {
                val bytes = Base64.decode(b64, Base64.DEFAULT)
                BitmapFactory.decodeByteArray(bytes, 0, bytes.size)
            } catch (_: Exception) { null }
        }

        override fun onDraw(canvas: Canvas) {
            val w = width.toFloat(); val h = height.toFloat(); val r = dpf(18)
            paint.style = Paint.Style.FILL; paint.color = Md3.container
            canvas.drawRoundRect(RectF(0f, 0f, w, h), r, r, paint)
            paint.style = Paint.Style.STROKE; paint.strokeWidth = dpf(1.2f); paint.color = Md3.outline
            canvas.drawRoundRect(RectF(1f, 1f, w - 1f, h - 1f), r, r, paint)

            val pad = dpf(16)
            val artR = RectF(pad, pad, pad + dpf(54), pad + dpf(54))
            paint.style = Paint.Style.FILL; paint.color = Md3.containerHigh
            canvas.drawRoundRect(artR, dpf(12), dpf(12), paint)
            artBmp?.let { canvas.drawBitmap(it, null, artR, paint) }

            val tx = artR.right + dpf(14)
            paint.color = Md3.onSurface; paint.textSize = dpf(15); paint.isFakeBoldText = true
            val title = if (Net.connected && Net.peerTitle.isNotEmpty()) Net.peerTitle else "多媒體同步控制"
            canvas.drawText(title, tx, pad + dpf(20), paint)
            paint.isFakeBoldText = false; paint.color = Md3.onVariant; paint.textSize = dpf(12)
            val sub = if (Net.connected) (Net.peerArtist.ifEmpty { "已連線至 ${Net.peer?.name}" }) else "連線電腦後即可遠端控制播放"
            canvas.drawText(sub, tx, pad + dpf(42), paint)

            // 進度列
            val barY = artR.bottom + dpf(14)
            val barR = RectF(pad, barY, w - pad, barY + dpf(5))
            paint.color = Md3.containerHigh
            canvas.drawRoundRect(barR, dpf(3), dpf(3), paint)
            if (Net.peerDurMs > 0) {
                val ratio = (Net.peerPosMs.toFloat() / Net.peerDurMs).coerceIn(0f, 1f)
                paint.color = Md3.primary
                canvas.drawRoundRect(RectF(pad, barY, pad + barR.width() * ratio, barY + dpf(5)), dpf(3), dpf(3), paint)
            }

            // 按鈕列（上一首 / 播放暫停 / 下一首）
            val cy = h - dpf(26)
            val cx = w / 2f
            paint.color = Md3.primary
            canvas.drawCircle(cx, cy, dpf(19), paint)
            paint.color = Md3.onPrimary; paint.textAlign = Paint.Align.CENTER; paint.textSize = dpf(13); paint.isFakeBoldText = true
            canvas.drawText(if (Net.peerPlaying) "❚❚" else "▶", cx, cy + dpf(4.5f), paint)
            paint.color = Md3.onSurface
            canvas.drawText("⏮", cx - dpf(56), cy + dpf(4.5f), paint)
            canvas.drawText("⏭", cx + dpf(56), cy + dpf(4.5f), paint)
            paint.textAlign = Paint.Align.LEFT; paint.isFakeBoldText = false
        }

        private fun dpf(v: Int) = v.toFloat() * resources.displayMetrics.density
        private fun dpf(v: Float) = v * resources.displayMetrics.density
    }

    private class DeviceCardView(ctx: android.content.Context, val dev: DeviceInfo, val connected: Boolean) : View(ctx) {
        var onConnect: ((DeviceInfo) -> Unit)? = null
        var onDisconnect: ((DeviceInfo) -> Unit)? = null
        private val paint = Paint(Paint.ANTI_ALIAS_FLAG)
        init {
            setOnTouchListener { _, ev ->
                if (ev.action == android.view.MotionEvent.ACTION_UP) {
                    val dens = resources.displayMetrics.density
                    val w = width.toFloat()
                    if (connected) {
                        if (ev.x > w - 140 * dens && ev.y > height - 48 * dens) onDisconnect?.invoke(dev)
                    } else if (dev.online) onConnect?.invoke(dev)
                }
                true
            }
            isClickable = true
        }
        override fun onDraw(canvas: Canvas) {
            val h = height.toFloat(); val w = width.toFloat(); val r = dpf(16)
            paint.style = Paint.Style.FILL
            paint.color = Md3.container
            canvas.drawRoundRect(RectF(0f, 0f, w, h), r, r, paint)
            paint.style = Paint.Style.STROKE
            paint.strokeWidth = if (connected) 2f * resources.displayMetrics.density else 1.2f * resources.displayMetrics.density
            paint.color = if (connected) Md3.primary else Md3.outline
            canvas.drawRoundRect(RectF(1f, 1f, w - 1f, h - 1f), r, r, paint)
            paint.style = Paint.Style.FILL
            paint.color = if (dev.online) Md3.onSurface else Md3.onVariant
            paint.textSize = dpf(15)
            paint.isFakeBoldText = true
            canvas.drawText(dev.name, dpf(18), dpf(30), paint)
            paint.isFakeBoldText = false
            paint.textSize = dpf(10)
            paint.color = Md3.onVariant
            val status = when {
                connected -> "已連線 · 多媒體同步中"
                dev.online -> "線上 · 點擊連線"
                else -> "離線"
            }
            canvas.drawText("$status · ${dev.platform} · ${dev.ip}", dpf(18), dpf(50), paint)
            if (connected) {
                paint.color = Md3.containerHigh
                canvas.drawRoundRect(RectF(w - dpf(132), h - dpf(42), w - dpf(16), h - dpf(12)), dpf(16), dpf(16), paint)
                paint.style = Paint.Style.STROKE
                paint.color = Md3.outline
                paint.strokeWidth = 1f * resources.displayMetrics.density
                canvas.drawRoundRect(RectF(w - dpf(132), h - dpf(42), w - dpf(16), h - dpf(12)), dpf(16), dpf(16), paint)
                paint.style = Paint.Style.FILL
                paint.color = Md3.onSurface
                paint.textSize = dpf(11)
                paint.textAlign = Paint.Align.CENTER
                canvas.drawText("Disconnect", w - dpf(74), h - dpf(22), paint)
                paint.textAlign = Paint.Align.LEFT
            }
        }
        private fun dpf(v: Int) = v.toFloat() * resources.displayMetrics.density
        private fun dpf(v: Float) = v * resources.displayMetrics.density
    }
}
