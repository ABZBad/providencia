using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SIP
{
    public class Apis
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct FLASHWINFO
        {
            public UInt32 cbSize;
            public IntPtr hwnd;
            public UInt32 dwFlags;
            public UInt32 uCount;
            public UInt32 dwTimeout;
        }

        [DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy,uint uFlags);


        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FlashWindowEx(ref FLASHWINFO pwfi);
    }
}
