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
    public class FlujoOP
    {
        public static int GuardaFlujoOP(String observaciones, Byte[] hojaCuotas, String usuario, bool soloFaltante, string tipoOP = "Pedido")
        {
            //usp_ConsultaAreas
            DataTable dt = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.Parameters.Add(new SqlParameter("@Referencia", observaciones));
                cmdDatos.Parameters.Add(new SqlParameter("@Usuario", usuario));
                cmdDatos.Parameters.Add(new SqlParameter("@SoloFaltante", soloFaltante));
                cmdDatos.Parameters.Add(new SqlParameter("@HojaCuotas", hojaCuotas));
                cmdDatos.Parameters.Add(new SqlParameter("@TipoOP", tipoOP));
                SqlParameter id = new SqlParameter("@Id", 0);
                id.Direction = ParameterDirection.Output;
                cmdDatos.Parameters.Add(id);
                cmdDatos.ObjectName = "usp_setFlujoOP";
                cmdDatos.Execute();
                return int.Parse(id.Value.ToString());
            }
        }
        public static void GuardaFlujoOPDetale(int _id, String _modelo, int _ordenProduccion, int _ordenMaquila, String usuario)
        {
            //usp_ConsultaAreas
            DataTable dt = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.Parameters.Add(new SqlParameter("@IdFlujoOP", _id));
                cmdDatos.Parameters.Add(new SqlParameter("@Modelo", _modelo));
                cmdDatos.Parameters.Add(new SqlParameter("@OrdenProduccion", _ordenProduccion));
                cmdDatos.Parameters.Add(new SqlParameter("@OrdenMaquila", _ordenMaquila));
                cmdDatos.Parameters.Add(new SqlParameter("@Usuario", usuario));
                cmdDatos.ObjectName = "usp_setFlujoOPDetalle";
                cmdDatos.Execute();
            }
        }
        public static void GuardaFlujoOPFechas(int _id, String xml)
        {
            //usp_ConsultaAreas
            DataTable dt = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.Parameters.Add(new SqlParameter("@IdFlujoOP", _id));
                cmdDatos.Parameters.Add(new SqlParameter("@XmlFechas", xml));
                cmdDatos.ObjectName = "usp_setFlujoOPFechas";
                cmdDatos.Execute();
            }
        }
        public static void ActualizaFlujoOP(int id, Byte[] file, int tipo, string observacionesPrograma = "")
        {
            //usp_ConsultaAreas
            DataTable dt = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.Parameters.Add(new SqlParameter("@id", id));
                cmdDatos.Parameters.Add(new SqlParameter("@File", file));
                cmdDatos.Parameters.Add(new SqlParameter("@ObservacionesPrograma", observacionesPrograma));
                cmdDatos.Parameters.Add(new SqlParameter("@tipo", tipo));
                cmdDatos.ObjectName = "usp_updateFlujoOP";
                cmdDatos.Execute();
            }
        }
        public static DataTable ConsultaFlujoOP(int id)
        {
            //usp_ConsultaAreas
            DataTable dt = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.Parameters.Add(new SqlParameter("@id", id));
                cmdDatos.ObjectName = "usp_getFlujoOP";
                DataTable dtResult = cmdDatos.GetDataTable();
                return dtResult;
            }
        }
        public static DataSet ConsultaFlujoOPDetalle(int id)
        {
            //usp_ConsultaAreas
            DataTable dt = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.Parameters.Add(new SqlParameter("@IdFlujoOP", id));
                cmdDatos.ObjectName = "usp_getFlujoOPDetalle";
                DataSet dtResult = cmdDatos.GetDataSet();
                return dtResult;
            }
        }
        public static void EliminaOPById(int id)
        {
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.Parameters.Add(new SqlParameter("@IdFlujoOP", id));
                cmdDatos.ObjectName = "usp_setEliminaOPByFlujoId";
                cmdDatos.Execute();
            }
        }
        public static void EliminaProgramaOP(int id)
        {
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.Parameters.Add(new SqlParameter("@IdFlujoOP", id));
                cmdDatos.ObjectName = "usp_setEliminaProgramaOP";
                cmdDatos.Execute();
            }
        }
        public static DataTable ValidaModelos(string _xmlModelos)
        {
            DataTable dt = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.Parameters.Add(new SqlParameter("@XmlModelos", _xmlModelos));
                cmdDatos.ObjectName = "usp_validaModelos";
                DataTable dtResult = cmdDatos.GetDataTable();
                return dtResult;
            }
        }
        public static DataTable ConsultaPedidosPendientesOP()
        {
            DataTable dt = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "usp_ConsultaPedidosPendientesOP";
                DataTable dtResult = cmdDatos.GetDataTable();
                return dtResult;
            }
        }
        public static void GuardaRelacionPedidosOP(int id, string xml)
        {
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.Parameters.Add(new SqlParameter("@IdFlujoOP", id));
                cmdDatos.Parameters.Add(new SqlParameter("@XmlPedidos", xml));
                cmdDatos.ObjectName = "usp_GuardaRelacionPedidosOP";
                cmdDatos.Execute();
            }
        }
        public static DataTable GetFlujoPedidosEnlazados(List<int> ListaPedidos)
        {
            DataTable dt = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.Parameters.Add(new SqlParameter("@XmlPedidos", GetXmlPedidosEnlazados(ListaPedidos)));
                cmdDatos.ObjectName = "usp_GetFlujoPedidosEnlazados";

                DataTable dtResult = cmdDatos.GetDataTable();
                return dtResult;
            }
        }
        private static string GetXmlPedidosEnlazados(List<int> ListaPedidos)
        {
            string xmlString = "";
            xmlString = "<Pedidos>";
            foreach (int pedido in ListaPedidos)
            {
                xmlString += String.Format("<Pedido>{0}</Pedido>", pedido.ToString());
            }
            xmlString += "</Pedidos>";
            return xmlString;
        }
        public static DataSet AsignaOPAutomaticaPedidos(int Pedido)
        {
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.Parameters.Add(new SqlParameter("@Pedido", Pedido));
                cmdDatos.ObjectName = "usp_AsignaOPAutomaticaPedidos";
                DataSet dsResult = cmdDatos.GetDataSet();
                return dsResult;
            }
        }

    }
}
