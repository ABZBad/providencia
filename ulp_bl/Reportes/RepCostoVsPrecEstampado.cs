using System;
using System.Data;

namespace ulp_bl.Reportes
{
    public class RepCostoVsPrecEstampado
    {
        public static DataTable RegresaCostoVsPrecCostura(DateTime FehcaInicial,DateTime FechaFinal)
        {
            string conStr = "";
            DataTable dataTableCostoVsPrecEstampado = new DataTable();

            dataTableCostoVsPrecEstampado = RepCostoVsProceso.RegresaCostoVsProceso(FehcaInicial, FechaFinal, Enumerados.Procesos.E);

            return dataTableCostoVsPrecEstampado;
        }

        public static void GeneraArchivoExcel(string RutaYNombreArchivo, DataTable CostoVsPreCostura)
        {

            RepCostoVsProceso.GeneraArchivoExcel(RutaYNombreArchivo, CostoVsPreCostura, Enumerados.Procesos.E);


        }
    }
}
