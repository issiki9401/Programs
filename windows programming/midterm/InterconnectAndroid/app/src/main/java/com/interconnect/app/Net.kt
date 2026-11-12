package com.interconnect.app

import android.content.Context
import android.net.nsd.NsdManager
import android.net.nsd.NsdServiceInfo
import android.net.wifi.WifiManager
import android.os.Build
import android.provider.Settings
import java.net.DatagramPacket
import java.net.DatagramSocket
import java.net.Inet4Address
import java.net.InetAddress
import java.net.InetSocketAddress
import java.net.NetworkInterface
import java.net.ServerSocket
import java.net.Socket
import java.util.concurrent.ConcurrentHashMap
import java.util.concurrent.CopyOnWriteArrayList
import java.util.concurrent.Executors
import java.util.concurrent.atomic.AtomicLong
import kotlin.concurrent.thread

data class DeviceInfo(
    var name: String,
    var ip: String = "",
    var platform: String = "",
    var controlPort: Int = 0,
    var lastSeen: Long = 0
) {
    val online: Boolean get() = System.currentTimeMillis() - lastSeen < 6000
}

class Stats {
    @Volatile var rttMs = -1.0
    @Volatile var localIp = ""
    @Volatile var syncCount = 0L
}

/**
 * 連線與多媒體同步管理單例：
 *  - UDP Beacon 廣播發現（+ mDNS NsdManager）
 *  - TCP JSON 控制通道（hello / ping / pong / media / mediaCmd / bye）
 *  - 所有網路寫入皆透過專用單一背景執行緒發送，避免 NetworkOnMainThreadException。
 */
object Net {
    const val DISCOVERY_PORT = 47811
    const val CONTROL_PORT = 47810

    interface Listener {
        fun onDevices() {}
        fun onConnection() {}
        fun onStats() {}
        fun onPeerMedia(m: CtrlMsg) {}
        fun onMediaCmd(m: CtrlMsg) {}
        fun onToast(msg: String) {}
    }

    fun normalizeIp(raw: String): String {
        var s = raw.trim()
        if (s.startsWith("::ffff:", ignoreCase = true)) s = s.substring(7)
        val pct = s.indexOf('%')
        if (pct >= 0) s = s.substring(0, pct)
        return s
    }

    private class Conn(val socket: Socket, val initiator: Boolean) {
        val out = socket.getOutputStream()
        val remoteIp: String = normalizeIp((socket.remoteSocketAddress as? InetSocketAddress)?.address?.hostAddress ?: "")
    }

    val listeners = CopyOnWriteArrayList<Listener>()
    val devices = ConcurrentHashMap<String, DeviceInfo>()
    val stats = Stats()
    @Volatile var peer: DeviceInfo? = null
    @Volatile var deviceName: String = Build.MODEL
    @Volatile private var conn: Conn? = null
    val connected: Boolean get() = conn != null && peer != null

    // 快取對端（Windows）傳來的媒體狀態供主畫面直接顯示
    @Volatile var peerTitle: String = ""
    @Volatile var peerArtist: String = ""
    @Volatile var peerPlaying: Boolean = false
    @Volatile var peerHasMedia: Boolean = false
    @Volatile var peerPosMs: Long = 0L
    @Volatile var peerDurMs: Long = 0L
    @Volatile var peerArtB64: String? = null

    private lateinit var appCtx: Context
    @Volatile private var running = false
    private var discSocket: DatagramSocket? = null
    private var serverSocket: ServerSocket? = null
    @Volatile private var lastPeer: DeviceInfo? = null
    @Volatile private var reconnecting = false
    @Volatile private var lastRxMs = System.currentTimeMillis()
    private val writer = Executors.newSingleThreadExecutor()
    private var mcastLock: WifiManager.MulticastLock? = null

    private fun each(f: Listener.() -> Unit) { for (l in listeners) try { l.f() } catch (_: Exception) { } }
    fun toast(msg: String) = each { onToast(msg) }

    private fun computeName(ctx: Context): String = try {
        val n = Settings.Global.getString(ctx.contentResolver, "device_name")
        if (!n.isNullOrBlank()) n else Build.MODEL
    } catch (_: Exception) { Build.MODEL }

    fun localIps(): Set<String> = try {
        NetworkInterface.getNetworkInterfaces().toList().flatMap { it.inetAddresses.toList() }
            .filterIsInstance<Inet4Address>().mapNotNull { it.hostAddress }.toSet() + "127.0.0.1"
    } catch (e: Exception) { setOf("127.0.0.1") }

    fun localIp(): String = try {
        NetworkInterface.getNetworkInterfaces().toList()
            .filter { it.isUp && !it.isLoopback }
            .flatMap { it.inetAddresses.toList() }
            .filterIsInstance<Inet4Address>()
            .firstOrNull()?.hostAddress ?: "127.0.0.1"
    } catch (e: Exception) { "127.0.0.1" }

    private fun broadcastAddresses(): List<InetAddress> = try {
        NetworkInterface.getNetworkInterfaces().toList()
            .filter { it.isUp && !it.isLoopback }
            .flatMap { it.interfaceAddresses }
            .mapNotNull { it.broadcast }
    } catch (e: Exception) { emptyList() }

    fun isSelf(d: DeviceInfo): Boolean = d.name == deviceName && localIps().contains(d.ip)

    fun start(ctx: Context) {
        if (running) return
        running = true
        appCtx = ctx.applicationContext
        deviceName = computeName(appCtx)
        stats.localIp = localIp()
        try {
            val wm = appCtx.getSystemService(Context.WIFI_SERVICE) as WifiManager
            mcastLock = wm.createMulticastLock("interconnect:mcast").apply { setReferenceCounted(false); acquire() }
        } catch (_: Exception) { }
        thread(isDaemon = true, name = "beacon-rx") { beaconReceive() }
        thread(isDaemon = true, name = "beacon-tx") {
            while (running) {
                try {
                    val buf = "INTR-ADV v1|$deviceName|AND|$CONTROL_PORT|0".toByteArray()
                    val s = discSocket()
                    try { s.send(DatagramPacket(buf, buf.size, InetAddress.getByName("255.255.255.255"), DISCOVERY_PORT)) } catch (_: Exception) { }
                    for (b in broadcastAddresses()) try { s.send(DatagramPacket(buf, buf.size, b, DISCOVERY_PORT)) } catch (_: Exception) { }
                } catch (_: Exception) { }
                each { onDevices() }
                try { Thread.sleep(1500) } catch (_: InterruptedException) { }
            }
        }
        thread(isDaemon = true, name = "ctrl-server") { acceptLoop() }
        thread(isDaemon = true, name = "beat") {
            while (running) {
                try { Thread.sleep(1500) } catch (_: InterruptedException) { }
                val c = conn ?: continue
                if (System.currentTimeMillis() - lastRxMs > 12000) { try { c.socket.close() } catch (_: Exception) { } }
                else send(CtrlMsg(t = "ping", ts = System.currentTimeMillis()))
            }
        }
        thread(isDaemon = true, name = "stats") {
            while (running) {
                try { Thread.sleep(1000) } catch (_: InterruptedException) { }
                if (!connected) stats.rttMs = -1.0
                each { onStats() }
            }
        }
        startNsd()
    }

    private fun discSocket(): DatagramSocket = discSocket ?: synchronized(this) {
        discSocket ?: DatagramSocket(null).apply {
            reuseAddress = true
            bind(InetSocketAddress(DISCOVERY_PORT))
            broadcast = true
            discSocket = this
        }
    }

    private fun beaconReceive() {
        val buf = ByteArray(512)
        while (running) {
            try {
                val pkt = DatagramPacket(buf, buf.size)
                discSocket().receive(pkt)
                val text = String(pkt.data, 0, pkt.length)
                if (!text.startsWith("INTR-ADV v1|")) continue
                val p = text.split("|")
                if (p.size < 4) continue
                val ip = normalizeIp(pkt.address.hostAddress ?: continue)
                if (p[1] == deviceName && localIps().contains(ip)) continue
                val d = devices.getOrPut(p[1]) { DeviceInfo(p[1]) }
                d.ip = ip; d.platform = p[2]
                d.controlPort = p[3].toIntOrNull() ?: CONTROL_PORT
                d.lastSeen = System.currentTimeMillis()
                each { onDevices() }
            } catch (_: Exception) { if (!running) break; try { Thread.sleep(200) } catch (_: Exception) { } }
        }
    }

    private fun startNsd() {
        try {
            val nsd = appCtx.getSystemService(Context.NSD_SERVICE) as NsdManager
            val info = NsdServiceInfo().apply {
                serviceName = "Interconnect-$deviceName"
                serviceType = "_interconnect._tcp."
                port = CONTROL_PORT
            }
            nsd.registerService(info, NsdManager.PROTOCOL_DNS_SD, object : NsdManager.RegistrationListener {
                override fun onServiceRegistered(i: NsdServiceInfo) {}
                override fun onRegistrationFailed(i: NsdServiceInfo, code: Int) {}
                override fun onServiceUnregistered(i: NsdServiceInfo) {}
                override fun onUnregistrationFailed(i: NsdServiceInfo, code: Int) {}
            })
            val dl = object : NsdManager.DiscoveryListener {
                override fun onDiscoveryStarted(regType: String) {}
                override fun onDiscoveryStopped(regType: String) {}
                override fun onStartDiscoveryFailed(regType: String, code: Int) {}
                override fun onStopDiscoveryFailed(regType: String, code: Int) {}
                override fun onServiceFound(i: NsdServiceInfo) {
                    @Suppress("DEPRECATION")
                    nsd.resolveService(i, object : NsdManager.ResolveListener {
                        override fun onResolveFailed(i: NsdServiceInfo, code: Int) {}
                        override fun onServiceResolved(i: NsdServiceInfo) {
                            @Suppress("DEPRECATION")
                            val ip = normalizeIp(i.host?.hostAddress ?: return)
                            if (localIps().contains(ip)) return
                            val name = i.serviceName.removePrefix("Interconnect-")
                            val d = devices.getOrPut(name) { DeviceInfo(name, platform = "AND") }
                            d.ip = ip
                            if (d.controlPort == 0) d.controlPort = i.port
                            d.lastSeen = System.currentTimeMillis()
                            each { onDevices() }
                        }
                    })
                }
                override fun onServiceLost(i: NsdServiceInfo) {}
            }
            nsd.discoverServices("_interconnect._tcp.", NsdManager.PROTOCOL_DNS_SD, dl)
        } catch (_: Exception) { }
    }

    private fun acceptLoop() {
        try {
            serverSocket = ServerSocket().apply { reuseAddress = true; bind(InetSocketAddress(CONTROL_PORT)) }
            while (running) {
                val s = serverSocket!!.accept()
                lastPeer = null
                attach(s, initiator = false)
            }
        } catch (_: Exception) { }
    }

    fun connect(d: DeviceInfo): Boolean = try {
        if (connected && peer?.name == d.name) true
        else {
            disconnect(keepLast = true)
            val s = Socket()
            s.connect(InetSocketAddress(d.ip, d.controlPort), 2500)
            lastPeer = d
            reconnecting = false
            peer = d
            attach(s, initiator = true)
            true
        }
    } catch (e: Exception) { toast("連線失敗：${d.name}"); false }

    fun connectAsync(d: DeviceInfo, done: ((Boolean) -> Unit)? = null) {
        thread(isDaemon = true, name = "connect") { done?.invoke(connect(d)) }
    }

    private fun attach(s: Socket, initiator: Boolean) {
        try { s.tcpNoDelay = true } catch (_: Exception) { }
        val old = conn
        val c = Conn(s, initiator)
        if (old != null) { try { old.socket.close() } catch (_: Exception) { } }
        conn = c
        lastRxMs = System.currentTimeMillis()
        thread(isDaemon = true, name = "ctrl-read") { readLoop(c) }
        if (initiator) {
            send(CtrlMsg(t = "hello", name = deviceName, platform = "AND"))
            send(CtrlMsg(t = "ping", ts = System.currentTimeMillis()))
        }
        each { onConnection() }
    }

    private fun readLoop(c: Conn) {
        val input = c.socket.getInputStream()
        val buf = ByteArray(16384)
        val sb = StringBuilder()
        try {
            while (running) {
                val n = input.read(buf)
                if (n <= 0) break
                lastRxMs = System.currentTimeMillis()
                sb.append(String(buf, 0, n, Charsets.UTF_8))
                var idx = sb.indexOf('\n')
                while (idx >= 0) {
                    val line = sb.substring(0, idx).trim()
                    sb.delete(0, idx + 1)
                    if (line.isNotEmpty()) handle(c, CtrlMsg.parse(line))
                    idx = sb.indexOf('\n')
                }
            }
        } catch (_: Exception) { }
        try { c.socket.close() } catch (_: Exception) { }
        if (conn === c) {
            conn = null
            peer = null
            peerHasMedia = false
            each { onConnection() }
            if (c.initiator) scheduleReconnect()
        }
    }

    private fun scheduleReconnect() {
        val target = lastPeer ?: return
        if (reconnecting) return
        reconnecting = true
        thread(isDaemon = true, name = "reconnect") {
            var tries = 0
            while (reconnecting && !connected && lastPeer != null && tries++ < 30) {
                try { Thread.sleep(2000) } catch (_: Exception) { }
                val t = devices[target.name] ?: target
                if (reconnecting && !connected && t.online) connect(t)
            }
            reconnecting = false
        }
    }

    private fun handle(c: Conn, m: CtrlMsg?) {
        if (m == null || conn !== c) return
        when (m.t) {
            "hello" -> {
                val n = m.name ?: return
                val d = devices.getOrPut(n) { DeviceInfo(n) }
                d.ip = c.remoteIp
                m.platform?.let { d.platform = it }
                if (d.controlPort == 0) d.controlPort = CONTROL_PORT
                d.lastSeen = System.currentTimeMillis()
                peer = d
                if (!c.initiator) {
                    send(CtrlMsg(t = "hello", name = deviceName, platform = "AND"))
                }
                send(CtrlMsg(t = "ping", ts = System.currentTimeMillis()))
                each { onConnection() }
            }
            "ping" -> send(CtrlMsg(t = "pong", ts = m.ts))
            "pong" -> {
                stats.rttMs = maxOf(1L, System.currentTimeMillis() - m.ts).toDouble()
                each { onStats() }
            }
            "media" -> {
                stats.syncCount++
                peerHasMedia = m.hasControls
                peerTitle = m.title ?: ""
                peerArtist = m.artist ?: ""
                peerPlaying = m.playing
                peerPosMs = m.positionMs
                peerDurMs = m.durationMs
                m.art?.let { peerArtB64 = it.ifEmpty { null } }
                each { onPeerMedia(m) }
            }
            "mediaCmd" -> {
                stats.syncCount++
                each { onMediaCmd(m) }
            }
            "bye" -> { lastPeer = null; reconnecting = false; try { c.socket.close() } catch (_: Exception) { } }
        }
    }

    fun send(m: CtrlMsg) {
        val c = conn ?: return
        val data = (m.serialize() + "\n").toByteArray(Charsets.UTF_8)
        writer.execute {
            try { synchronized(c.out) { c.out.write(data); c.out.flush() } } catch (_: Exception) { }
        }
    }

    fun disconnect(keepLast: Boolean = false) {
        if (!keepLast) { lastPeer = null; reconnecting = false }
        val c = conn
        if (c != null) {
            send(CtrlMsg(t = "bye"))
            conn = null
            peer = null
            peerHasMedia = false
            writer.execute { try { c.socket.close() } catch (_: Exception) { } }
            each { onConnection() }
        }
    }
}
