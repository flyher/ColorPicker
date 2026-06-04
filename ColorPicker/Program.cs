using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ColorPicker
{
    static class Program
    {
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
            try
            {
                SetProcessDPIAware();
            }
            catch (EntryPointNotFoundException)
            {
                // Older systems without DPI APIs should continue with default scaling.
            }
        }
    }
}
