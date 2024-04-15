using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Microsoft.Win32;

namespace SIP.Net
{
    public class Prerequisites
    {
        public static bool CrstalReportsInstalled()
        {
            RegistryKey regKey = Registry.LocalMachine.OpenSubKey(@"Software\SAP BusinessObjects\Crystal Reports for .NET Framework 4.0");

            if (regKey != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
