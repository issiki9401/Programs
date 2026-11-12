package com.interconnect.app

import org.json.JSONObject

/** TCP 控制訊息（newline-delimited JSON），與 Windows 端欄位一致。 */
data class CtrlMsg(
    var t: String = "",
    var name: String? = null,
    var platform: String? = null,
    var ts: Long = 0,
    var title: String? = null,
    var artist: String? = null,
    var playing: Boolean = false,
    var hasControls: Boolean = false,
    var positionMs: Long = 0,
    var durationMs: Long = 0,
    var cmd: String? = null,
    /** Base64 JPEG 封面；null＝未變更、空字串＝清除封面。 */
    var art: String? = null
) {
    fun serialize(): String {
        val o = JSONObject()
        o.put("t", t)
        name?.let { o.put("name", it) }
        platform?.let { o.put("platform", it) }
        if (ts != 0L) o.put("ts", ts)
        if (t == "media") {
            o.put("title", title ?: "")
            o.put("artist", artist ?: "")
            o.put("playing", playing)
            o.put("hasControls", hasControls)
            o.put("positionMs", positionMs)
            o.put("durationMs", durationMs)
            art?.let { o.put("art", it) }
        }
        cmd?.let { o.put("cmd", it) }
        if (t == "mediaCmd" && positionMs != 0L) o.put("positionMs", positionMs)
        return o.toString()
    }

    companion object {
        fun parse(line: String): CtrlMsg? = try {
            val o = JSONObject(line)
            fun s(k: String): String? = if (o.has(k) && !o.isNull(k)) o.getString(k) else null
            CtrlMsg(
                t = s("t") ?: "",
                name = s("name"),
                platform = s("platform"),
                ts = o.optLong("ts", 0),
                title = s("title"),
                artist = s("artist"),
                playing = o.optBoolean("playing", false),
                hasControls = o.optBoolean("hasControls", false),
                positionMs = o.optLong("positionMs", 0),
                durationMs = o.optLong("durationMs", 0),
                cmd = s("cmd"),
                art = s("art")
            )
        } catch (e: Exception) { null }
    }
}
