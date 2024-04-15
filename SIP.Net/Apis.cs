using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SIP.Net
{
    

    public class Apis
    {
        [DllImport("User32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
    }
}
