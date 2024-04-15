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
    public class RetroAlimManObraPedido
    {
        public static DataTable ConsultaProcesosLiberados(int Pedido)
        {
            String conStr = "";
            DataTable dtProcesos = new DataTable();
            try
            {
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "usp_ConsultaProcesosActualizarUPPedidos";
                cmd.Parameters.Add(new SqlParameter("@PEDIDO", Pedido));
                dtProcesos = cmd.GetDataTable();
                cmd.Connection.Close();

                return dtProcesos;
            }
            catch { return new DataTable(); }
        }
        public static DataTable CargaProcesosPedido(int Pedido)
        {
            String conStr = "";
            DataTable dtProcesos = new DataTable();
            try
            {
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "usp_COnsultaProcesosPedido";
                cmd.Parameters.Add(new SqlParameter("@PEDIDO", Pedido));
                dtProcesos = cmd.GetDataTable();
                cmd.Connection.Close();

                return dtProcesos;
            }
            catch { return new DataTable();}
        }
    }
}
