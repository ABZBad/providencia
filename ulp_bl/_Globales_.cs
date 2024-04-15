using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;

namespace ulp_bl
{
    
    /// <summary>
    /// Esta clase tiene como propósito almacenar todas aquellas variables que deban ser globales
    /// y que siempre deban estar disponibles
    /// </summary>
    public class Globales
    {
        public static Usuario UsuarioActual { set; get; }
        public static USUARIOS DatosUsuario { set; get; }
        public static string ConnectionString { set; get; }
        public static string ConnectionStringSae80 { set; get; }
        //public static string ConnectionStringProd30 { set; get; }
        public static string DataSource { set; get; }
        //public static string rutaImagenes = Directory.GetCurrentDirectory().Trim() + @" \CliesLogos\";
        public static string rutaImagenes = @"\\192.168.75.2\Paquete SIP 7\CliesLogos\";
        public static string CodificaCifra(decimal Cifra)
        {
            string resultado = Cifra.ToString().Replace("1", "R")
                .Replace("2", "E")
                .Replace("3", "P")
                .Replace("4", "U")
                .Replace("5", "B")
                .Replace("6", "L")
                .Replace("7", "Y")
                .Replace("8", "C")
                .Replace("9", "A")
                .Replace("0", "Z")
                .Replace(".", "x");

            return resultado;
        }

        public static DataTable tablaclientes { set; get; }
        public static string GeneraAgrupador()
        {
            Random rndtmp = new Random();
            Random rnd = new Random(rndtmp.Next() * DateTime.Now.Millisecond);
            return Convert.ToString(rnd.Next(10000000, 99999999));
        }
    }
}
