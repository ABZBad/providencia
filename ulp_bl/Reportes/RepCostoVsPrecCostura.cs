using System;
using System.Data;

namespace ulp_bl.Reportes
{
    public class RepCostoVsPrecCostura
    {
        public static DataTable RegresaCostoVsPrecCostura(DateTime FehcaInicial,DateTime FechaFinal)
        {
            string conStr = "";
            DataTable dataTableCostoVsPrecCostura = new DataTable();

            dataTableCostoVsPrecCostura = RepCostoVsProceso.RegresaCostoVsProceso(FehcaInicial, FechaFinal, Enumerados.Procesos.C);

            return dataTableCostoVsPrecCostura;
        }

        public static void GeneraArchivoExcel(string RutaYNombreArchivo, DataTable CostoVsPreCostura)
        {

            RepCostoVsProceso.GeneraArchivoExcel(RutaYNombreArchivo, CostoVsPreCostura, Enumerados.Procesos.C);


        }
    }
}
