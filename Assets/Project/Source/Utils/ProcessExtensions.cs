using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Exa.Utils {
    public static class ProcessExtensions {
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr handle);

        public static void Focus(this Process process) {
            SetForegroundWindow(process.MainWindowHandle);
        }
    }
}