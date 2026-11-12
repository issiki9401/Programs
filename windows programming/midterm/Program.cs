using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Midterm
{
    internal static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length >= 2 && args[0] == "--export-screenshot")
            {
                ApplicationConfiguration.Initialize();
                using var f = new Form1();
                f.ExportDemoScreenshot(args[1]);
                return;
            }
            var suffix = Environment.GetEnvironmentVariable("INTERCONNECT_TEST") ?? "";
            using var mutex = new Mutex(true, "Interconnect-Midterm-SingleInstance" + suffix, out bool createdNew);
            if (!createdNew) return; // 單一實例：避免重複啟動

            var crashLog = Path.Combine(Path.GetTempPath(), "interconnect_crash.log");
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                try { File.AppendAllText(crashLog, DateTime.Now + " DOMAIN " + e.ExceptionObject + "\n"); } catch { }
            };
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (s, e) =>
            {
                try { File.AppendAllText(crashLog, DateTime.Now + " THREAD " + e.Exception + "\n"); } catch { }
            };

            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}
