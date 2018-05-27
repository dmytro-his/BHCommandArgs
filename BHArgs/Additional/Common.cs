using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BeHappy.Args
{
    internal static class Common
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        public static bool HasConsole()
        {
            return GetConsoleWindow() != IntPtr.Zero;
        }
    }
}
