using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sm_dl;
using sm_dl.SqlServer;
using ulp_bl.Utiles;
using ulp_dl.SIPNegocio;
using System.Data;
using System.Data.SqlClient;
using ulp_dl;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.IO;

namespace ulp_bl
{
    public class CatalogosSolicitudesEspeciales
    {
        public static DataSet getCatalogosEspeciales()
        {
            String conStr = "";
            DataSet ds = new DataSet();
            try
            {
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "usp_CargaCatalogosSolicitudEspciales";
                ds = cmd.GetDataSet();
                cmd.Connection.Close();
                return ds;
            }
            catch { return null; }
        }
    }
}
