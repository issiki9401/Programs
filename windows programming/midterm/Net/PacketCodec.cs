using System;
using System.Text.Json.Nodes;

namespace Midterm.Net
{
    /// <summary>
    /// TCP 控制通道訊息（Newline-delimited JSON, UTF-8）。
    /// 負責雙系統握手（hello）、延遲量測（ping/pong）、多媒體資訊同步（media）與遠端控制指令（mediaCmd）。
    /// </summary>
    public sealed class CtrlMsg
    {
        public string t = "";
        public string? name;
        public string? platform;
        public long ts;
        public string? title;
        public string? artist;
        public bool playing;
        public bool hasControls;
        public long positionMs;
        public long durationMs;
        public string? cmd;
        /// <summary>Base64 JPEG 專輯封面；null 代表未變更、空字串代表清除封面。</summary>
        public string? art;

        public string Serialize()
        {
            var o = new JsonObject { ["t"] = t };
            if (name != null) o["name"] = name;
            if (platform != null) o["platform"] = platform;
            if (ts != 0) o["ts"] = ts;
            if (t == "media")
            {
                o["title"] = title ?? "";
                o["artist"] = artist ?? "";
                o["playing"] = playing;
                o["hasControls"] = hasControls;
                o["positionMs"] = positionMs;
                o["durationMs"] = durationMs;
                if (art != null) o["art"] = art;
            }
            if (cmd != null) o["cmd"] = cmd;
            if (t == "mediaCmd" && positionMs != 0) o["positionMs"] = positionMs;
            return o.ToJsonString();
        }

        public static CtrlMsg? Parse(string line)
        {
            try
            {
                if (JsonNode.Parse(line) is not JsonObject o) return null;
                string? S(string k) => o[k] is JsonValue v && v.TryGetValue<string>(out var s) ? s : null;
                long L(string k)
                {
                    if (o[k] is not JsonValue v) return 0;
                    if (v.TryGetValue<long>(out var l)) return l;
                    if (v.TryGetValue<double>(out var d)) return (long)d;
                    return 0;
                }
                bool B(string k) => o[k] is JsonValue v && v.TryGetValue<bool>(out var b) && b;
                return new CtrlMsg
                {
                    t = S("t") ?? "",
                    name = S("name"),
                    platform = S("platform"),
                    ts = L("ts"),
                    title = S("title"),
                    artist = S("artist"),
                    playing = B("playing"),
                    hasControls = B("hasControls"),
                    positionMs = L("positionMs"),
                    durationMs = L("durationMs"),
                    cmd = S("cmd"),
                    art = S("art"),
                };
            }
            catch { return null; }
        }
    }
}
