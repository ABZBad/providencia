using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sm_dl;
using ulp_dl.SIPNegocio;

namespace ulp_bl
{
    public class ActualizacionCostosSinManoObra
    {
        public static void Actualiza(int paso)
        {
            using (var dbContext=new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand ejecuta = new sm_dl.SqlServer.SqlServerCommand();
                ejecuta.Connection = sm_dl.DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);                
                ejecuta.ObjectName = "usp_ActualizacionCostosSinManoObra";
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@paso", paso));
                ejecuta.Execute();
            }
        }
    }
}
