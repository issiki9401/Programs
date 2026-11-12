using System;
using System.IO;
using System.Runtime.InteropServices;
using Windows.Media;
using Windows.Storage.Streams;
using Midterm.Net;

namespace Midterm.Media
{
    [ComImport]
    [Guid("ddb0472d-c911-4a1f-86d9-dc3d71a95f5a")]
    [InterfaceType(ComInterfaceType.InterfaceIsIInspectable)]
    internal interface ISystemMediaTransportControlsInterop
    {
        [return: MarshalAs(UnmanagedType.IInspectable)]
        object GetForWindow(IntPtr appWindow, [In] ref Guid riid);
    }

    /// <summary>
    /// 將對端（Android）的多媒體播放狀態（標題/歌手/封面/進度/播放中）發布到 Windows 系統多媒體控制項（SMTC），
    /// 並把 SMTC 按鈕（播放/暫停/上下首/拖曳進度）轉發為 mediaCmd 給對端。對端沒有媒體工作階段時隱藏。
    /// </summary>
    public sealed class MediaBridge : IDisposable
    {
        private static readonly Guid IidInterop = new Guid("ddb0472d-c911-4a1f-86d9-dc3d71a95f5a");
        private static Guid IidSmtc = new Guid("AF86E2E0-B12D-4C6A-9C5A-D7AA65101E90"); // IInspectable（已驗證可轉型為 SMTC）
        private SystemMediaTransportControls? _smtc;
        private string _lastThumbB64 = "";

        [DllImport("combase.dll", PreserveSig = false)]
        private static extern IntPtr RoGetActivationFactory(
            [MarshalAs(UnmanagedType.HString)] string activatableClassId,
            [In] ref Guid iid);

        /// <summary>(cmd, positionMs)</summary>
        public event Action<string, long>? CommandForwarded;
        public bool Ready => _smtc != null;

        public bool Attach(IntPtr hwnd)
        {
            try
            {
                Guid iid = IidInterop;
                IntPtr pFactory = RoGetActivationFactory("Windows.Media.SystemMediaTransportControls", ref iid);
                var interop = (ISystemMediaTransportControlsInterop)Marshal.GetObjectForIUnknown(pFactory);
                Marshal.Release(pFactory);
                Guid riid = IidSmtc;
                object raw = interop.GetForWindow(hwnd, ref riid);
                _smtc = (SystemMediaTransportControls)raw;
                _smtc.ButtonPressed += OnButtonPressed;
                _smtc.PlaybackPositionChangeRequested += (s, e) => CommandForwarded?.Invoke("seek", (long)e.RequestedPlaybackPosition.TotalMilliseconds);
                _smtc.IsEnabled = false;
                return true;
            }
            catch { return false; }
        }

        private void OnButtonPressed(SystemMediaTransportControls sender, SystemMediaTransportControlsButtonPressedEventArgs args)
        {
            string cmd = args.Button switch
            {
                SystemMediaTransportControlsButton.Play => "play",
                SystemMediaTransportControlsButton.Pause => "pause",
                SystemMediaTransportControlsButton.Next => "next",
                SystemMediaTransportControlsButton.Previous => "prev",
                _ => "toggle"
            };
            CommandForwarded?.Invoke(cmd, 0);
        }

        /// <summary>以對端 media 快照更新 SMTC；對端無媒體時隱藏。</summary>
        public void Update(CtrlMsg m)
        {
            if (_smtc == null) return;
            try
            {
                if (!m.hasControls)
                {
                    Clear();
                    return;
                }
                _smtc.IsEnabled = true;
                _smtc.IsPlayEnabled = true;
                _smtc.IsPauseEnabled = true;
                _smtc.IsNextEnabled = true;
                _smtc.IsPreviousEnabled = true;
                _smtc.IsStopEnabled = false;
                _smtc.PlaybackStatus = m.playing ? MediaPlaybackStatus.Playing : MediaPlaybackStatus.Paused;
                var disp = _smtc.DisplayUpdater;
                disp.Type = MediaPlaybackType.Music;
                disp.MusicProperties.Title = string.IsNullOrEmpty(m.title) ? "Interconnect" : m.title;
                disp.MusicProperties.Artist = string.IsNullOrEmpty(m.artist) ? "Android" : m.artist;
                if (m.art != null && m.art != _lastThumbB64)
                {
                    _lastThumbB64 = m.art;
                    if (m.art.Length == 0) disp.Thumbnail = null;
                    else
                    {
                        var ms = new InMemoryRandomAccessStream();
                        var bytes = Convert.FromBase64String(m.art);
                        var w = new DataWriter(ms.GetOutputStreamAt(0));
                        w.WriteBytes(bytes);
                        w.StoreAsync().AsTask().Wait();
                        w.DetachStream();
                        disp.Thumbnail = RandomAccessStreamReference.CreateFromStream(ms);
                    }
                }
                disp.Update();
                if (m.durationMs > 0)
                {
                    var tl = new SystemMediaTransportControlsTimelineProperties
                    {
                        StartTime = TimeSpan.Zero,
                        MinSeekTime = TimeSpan.Zero,
                        EndTime = TimeSpan.FromMilliseconds(m.durationMs),
                        MaxSeekTime = TimeSpan.FromMilliseconds(m.durationMs),
                        Position = TimeSpan.FromMilliseconds(Math.Min(m.positionMs, m.durationMs)),
                    };
                    _smtc.UpdateTimelineProperties(tl);
                }
            }
            catch { }
        }

        /// <summary>斷線或對端無媒體：隱藏控制項。</summary>
        public void Clear()
        {
            try
            {
                if (_smtc == null) return;
                _lastThumbB64 = "";
                _smtc.PlaybackStatus = MediaPlaybackStatus.Closed;
                _smtc.DisplayUpdater.ClearAll();
                _smtc.DisplayUpdater.Update();
                _smtc.IsEnabled = false;
            }
            catch { }
        }

        public void Dispose()
        {
            try { if (_smtc != null) { _smtc.ButtonPressed -= OnButtonPressed; _smtc.IsEnabled = false; } } catch { }
        }
    }
}
