using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ulp_dl.aspel_sae80;
using ulp_bl.Utiles;
using ulp_dl.SIPNegocio;
using sm_dl;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;

namespace ulp_bl
{
    public class RequisicionPedido
    {
        public static DataSet GetRequisicionPedido(int pedido)
        {
            DataSet ds = new DataSet();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "usp_ConsultaRequisicionPedido";
                cmdDatos.Parameters.Add(new SqlParameter("@Pedido", pedido));
                ds = cmdDatos.GetDataSet();
            }
            return ds;
        }
        public static DataTable GetRequisicion(int pedido)
        {
            DataTable dt = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "usp_ConsultaRequisicion";
                cmdDatos.Parameters.Add(new SqlParameter("@Pedido", pedido));
                dt = cmdDatos.GetDataTable();
            }
            return dt;
        }
        public static void AltaRequisicion(int pedido, string xmlComponentes, string xmlModelos)
        {
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "usp_GeneraRequisicionPedido";
                cmdDatos.Parameters.Add(new SqlParameter("@pedido", pedido));
                cmdDatos.Parameters.Add(new SqlParameter("@xml_componentes", xmlComponentes));
                cmdDatos.Parameters.Add(new SqlParameter("@xml_modelos", xmlModelos));
                cmdDatos.Execute();
            }
        }
        public static void ActualizaFechaImpresion(int pedido)
        {
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "usp_ActualizaRequisicionImpresion";
                cmdDatos.Parameters.Add(new SqlParameter("@Pedido", pedido));
                cmdDatos.Execute();
            }
        }
        public static void ActualizaFechaLiberacion(int pedido)
        {
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "usp_ActualizaRequisicionLiberacion";
                cmdDatos.Parameters.Add(new SqlParameter("@Pedido", pedido));
                cmdDatos.Execute();
            }
        }
        public static DataTable GetRequisicionTipo(int tipo)
        {
            DataTable dt = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "usp_ConsultaRequisicionesPorTipo";
                cmdDatos.Parameters.Add(new SqlParameter("@Tipo", tipo));
                dt = cmdDatos.GetDataTable();
            }
            return dt;
        }
    }
}
