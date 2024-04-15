using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CFDI33.Clases
{
    public class FuncionesGlobales
    {
        public static MemoryStream GenerateStreamFromString(string cadena)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(cadena);
            writer.Flush();
            stream.Position = 0;

            return stream;
        }
        public static string GenerateStringFromStream(Stream stream)
        {
            var st = new StreamReader(stream);

            return st.ReadToEnd();
        }
        public static string cortarEspacios(string _cadena)
        {
            string result = "";

            foreach (string ss in _cadena.Split(' '))
            {
                if (ss != "")
                    result += ss + " ";
            }

            result = result.Trim();

            return result;
        }
        public static string mondar(object clase)
        {
            string result = "";

            try
            {
                Type m_tipo = null;
                PropertyInfo[] m_propiedades = null;

                m_tipo = ((object)clase).GetType();
                m_propiedades = m_tipo.GetProperties();

                foreach (PropertyInfo m_propiedad in m_propiedades)
                {
                    if (m_propiedad.PropertyType.Name == "String")
                    {
                        m_propiedad.SetValue(((object)clase), FuncionesGlobales.cortarEspacios(m_propiedad.GetValue(((object)clase)).ToString()));
                    }

                }

            }
            catch (Exception ex)
            {
                 result += "Se genero un error inesperado: " + ex.Message;
            }

            return result;
        }

        public static string getString(string format, DateTime fecha)
        {
            string r = "";
            switch (format.ToUpper())
            {
                case "SAT":
                    var _with1 = fecha;
                    r = _with1.Year + "-";
                    //
                    if (_with1.Month < 10)
                    {
                        r = r + "0";
                    }
                    r = r + _with1.Month + "-";
                    //
                    if (_with1.Day < 10)
                    {
                        r = r + "0";
                    }
                    r = r + _with1.Day + "T";
                    //
                    if (_with1.Hour < 10)
                    {
                        r = r + "0";
                    }
                    r = r + _with1.Hour + ":";
                    //
                    if (_with1.Minute < 10)
                    {
                        r = r + "0";
                    }
                    r = r + _with1.Minute + ":";
                    //
                    if (_with1.Second < 10)
                    {
                        r = r + "0";
                    }
                    r = r + _with1.Second;

                    return r;
                case "AMECE":
                    var _with2 = fecha;
                    r = _with2.Year.ToString();
                    //
                    if (_with2.Month < 10)
                    {
                        r = r + "0";
                    }
                    r = r + _with2.Month;
                    //
                    if (_with2.Day < 10)
                    {
                        r = r + "0";
                    }
                    r = r + _with2.Day;
                    return r;
                case "SIMPLE":
                    return fecha.Day.ToString() + "/" + fecha.Month.ToString() + "/" + fecha.Year;
                default:

                    return "";

            }

        }
    }
}
