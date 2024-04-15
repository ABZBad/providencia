using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using sm_dl;
using sm_dl.SqlServer;
using ulp_dl.aspel_sae80;
using ulp_dl.SIPReportes;

namespace ulp_bl.Reportes
{
    public class RepCostoVsPrecFlete
    {
        public static DataTable RegresaCostoVsPrecFlete(DateTime FehcaInicial,DateTime FechaFinal)
        {
            string conStr = "";
            DataTable dataTableCostoVsPrecFlete = new DataTable();

            dataTableCostoVsPrecFlete = RepCostoVsProceso.RegresaCostoVsProceso(FehcaInicial, FechaFinal, Enumerados.Procesos.F);

            return dataTableCostoVsPrecFlete;
        }

        public static void GeneraArchivoExcel(string RutaYNombreArchivo, DataTable CostoVsPrecFlete)
        {

            RepCostoVsProceso.GeneraArchivoExcel(RutaYNombreArchivo, CostoVsPrecFlete, Enumerados.Procesos.F);


        }
    }
}
