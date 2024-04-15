using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SIP.Net
{
    public class Utils
    {
        public static string ProgramFilesx86()
        {
            /*
            if (8 == IntPtr.Size
                || (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
            {
                return Environment.GetEnvironmentVariable("ProgramFiles(x86)");
            }

            return Environment.GetEnvironmentVariable("ProgramFiles");
             * */

            return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        }
    }
}
