using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace InterconnectStub
{
    /// <summary>
    /// 全架構單一 exe 啟動器：內嵌 x64 / x86 / arm64 三個自含式執行檔，
    /// 執行時依系統原生架構解壓對應版本至 %LOCALAPPDATA%\Interconnect 並啟動。
    /// net48 AnyCPU：三種架構皆可執行（ARM 32-bit 裝置由 Windows 自動以 x86 模擬執行）。
    /// </summary>
    internal static class Program
    {
        [DllImport("kernel32.dll")]
        private static extern void GetNativeSystemInfo(out SYSTEM_INFO info);
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int MessageBoxW(IntPtr hWnd, string text, string caption, uint type);

        [StructLayout(LayoutKind.Sequential)]
        private struct SYSTEM_INFO
        {
            public ushort wProcessorArchitecture;
            public ushort wReserved;
            public uint dwPageSize;
            public IntPtr lpMinimumApplicationAddress;
            public IntPtr lpMaximumApplicationAddress;
            public IntPtr dwActiveProcessorMask;
            public uint dwNumberOfProcessors;
            public uint dwProcessorType;
            public uint dwAllocationGranularity;
            public ushort wProcessorLevel;
            public ushort wProcessorRevision;
        }

        [STAThread]
        private static void Main()
        {
            try
            {
                GetNativeSystemInfo(out var si);
                string arch;
                switch (si.wProcessorArchitecture)
                {
                    case 12: arch = "arm64"; break;
                    case 0:
                    case 5: arch = "x86"; break;
                    default: arch = "x64"; break;
                }
                var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Interconnect");
                Directory.CreateDirectory(dir);
                var target = Path.Combine(dir, $"Interconnect-{arch}.exe");
                try
                {
                    foreach (var p in Process.GetProcessesByName($"Interconnect-{arch}"))
                        try { p.Kill(); p.WaitForExit(1500); } catch { }
                    foreach (var p in Process.GetProcessesByName("Interconnect"))
                        try { p.Kill(); p.WaitForExit(1500); } catch { }
                }
                catch { }
                var asm = Assembly.GetExecutingAssembly();
                using (var gz = new System.IO.Compression.GZipStream(asm.GetManifestResourceStream(arch), System.IO.Compression.CompressionMode.Decompress))
                using (var dst = File.Create(target))
                {
                    gz.CopyTo(dst);
                }
                var psi = new ProcessStartInfo(target) { UseShellExecute = true, WorkingDirectory = dir };
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBoxW(IntPtr.Zero, "啟動失敗：" + ex.Message, "Interconnect", 0x10);
            }
        }
    }
}
