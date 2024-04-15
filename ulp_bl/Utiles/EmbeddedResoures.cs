using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ulp_bl.Utiles
{
    public class EmbeddedResoures
    {
        public static string GetTextResource(string EmbeddedResourceName)
        {
            string stringResourceValue = "";
            Assembly assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream(EmbeddedResourceName))
            {
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    stringResourceValue = streamReader.ReadToEnd();
                }
            }
            return stringResourceValue;
        }
    }
}
