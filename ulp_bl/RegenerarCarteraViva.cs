using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using sm_dl;
using sm_dl.SqlServer;
using ulp_dl.SIPNegocio;

namespace ulp_bl
{
    public class RegenerarCarteraViva
    {
        public static void RegenerarCartera(int Año,ref Exception Ex)
        {
            try
            {
                string connStr = "";
                using (var dbContext = new SIPNegocioContext())
                {
                    connStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(connStr);
                cmd.ObjectName = "usp_RegenerarCarteraViva";
                cmd.Parameters.Add(new SqlParameter("@annio", Año));
                cmd.Execute();
                cmd.Connection.Close();
            }
            catch (Exception E)
            {
                Ex = E;
            }
        }
    }
}
