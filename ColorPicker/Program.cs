using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ColorPicker
{
    static class Program
    {
        private static readonly IntPtr DpiAwarenessContextPerMonitorAwareV2 = new IntPtr(-4);
        private static readonly IntPtr DpiAwarenessContextPerMonitorAware = new IntPtr(-3);

        private enum ProcessDpiAwareness
        {
            ProcessDpiUnaware = 0,
            ProcessSystemDpiAware = 1,
            ProcessPerMonitorDpiAware = 2
        }

        [DllImport("user32.dll")]
        private static extern bool SetProcessDpiAwarenessContext(IntPtr dpiContext);

        [DllImport("shcore.dll")]
        private static extern int SetProcessDpiAwareness(ProcessDpiAwareness value);

        [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            EnableDpiAwareness();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }

        private static void EnableDpiAwareness()
        {
            if (TrySetProcessDpiAwarenessContext(DpiAwarenessContextPerMonitorAwareV2) ||
                TrySetProcessDpiAwarenessContext(DpiAwarenessContextPerMonitorAware) ||
                TrySetProcessDpiAwareness(ProcessDpiAwareness.ProcessPerMonitorDpiAware))
            {
                return;
            }

            try
            {
                SetProcessDPIAware();
            }
            catch (EntryPointNotFoundException)
            {
                // Older systems without DPI APIs should continue with default scaling.
            }
        }

        private static bool TrySetProcessDpiAwarenessContext(IntPtr dpiContext)
        {
            try
            {
                return SetProcessDpiAwarenessContext(dpiContext);
            }
            catch (EntryPointNotFoundException)
            {
                return false;
            }
        }

        private static bool TrySetProcessDpiAwareness(ProcessDpiAwareness value)
        {
            try
            {
                return SetProcessDpiAwareness(value) == 0;
            }
            catch (EntryPointNotFoundException)
            {
                return false;
            }
            catch (DllNotFoundException)
            {
                return false;
            }
        }
    }
}
