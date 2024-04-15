using sm_dl;
using sm_dl.SqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ulp_dl.SIPReportes;

namespace ulp_bl
{
    public class EliminarPedidosTemporales
    {
        public static void EliminaPedidosTempo()
        {
            DataTable dataTableElminaPedidos = new DataTable();
            using (var dbContext = new SIPReportesContext())
            {
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmd.ObjectName = "usp_EliminarPedidosTemporales";
                cmd.Execute();
                cmd.Connection.Close();

            }
        }
    }
}
