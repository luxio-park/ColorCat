using System;
using System.Runtime.InteropServices;

namespace Luxio.ColorCat
{
    /// <summary>
    /// Imports Windows API
    /// </summary>
    static class NativeMethods
    {
        [DllImport("User32.dll", SetLastError = true)]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("User32.dll", SetLastError = true)]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("Gdi32.dll", SetLastError = true)]
        public static extern uint GetPixel(IntPtr hDC, int x, int y);
    }
}
