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
    public class ControlFlujo
    {
        public enum TiposPrceso
        {
            SolicitudEspecial = 1,
            PedidoEspecial = 2,
            PedidoLinea = 3,
            OrdenesProduccion = 4,
            OrdenesProduccionFaltante = 5,
            PedidoDAT = 6,
            PedidoMostrador = 7,
            RequisicionMostrador = 8
        }
        public static List<String> Flujo1_Columnas = new List<String> { "Seleccion", "Fecha", "Solicitud", "Clave", "Cliente", "Vendedor", "Estatus", "Proceso", "Observaciones" };
        public static List<String> Flujo2_Columnas = new List<String> { "Seleccion", "Fecha", "Pedido", "Clave", "Cliente", "Vendedor", "Prendas", "Proceso", "Origen", "Observaciones", "Estatus" };
        public static List<String> Flujo3_Columnas = new List<String> { "Seleccion", "Fecha", "Pedido", "Clave", "Cliente", "Vendedor", "Prendas", "Proceso", "Origen", "Observaciones", "Estatus", "RequiereOP", "OP", "FechaEntregaOP" };
        public static List<String> Flujo4_Columnas = new List<String> { "Seleccion", "ID", "Usuario", "Descripcion", "Fecha", "Proceso", "Observaciones", "Estatus" };
        public static List<String> Flujo5_Columnas = new List<String> { "Seleccion", "ID", "Usuario", "Descripcion", "Fecha", "Proceso", "Observaciones", "Estatus" };
        public static List<String> Flujo6_Columnas = new List<String> { "Seleccion", "Fecha", "Pedido", "Clave", "Cliente", "Vendedor", "Prendas", "Proceso", "Origen", "Observaciones", "Estatus", "RequiereOP", "OP", "FechaEntregaOP" };
        public static List<String> Flujo7_Columnas = new List<String> { "Seleccion", "Fecha", "Pedido", "Clave", "Cliente", "Vendedor", "Prendas", "Proceso", "Origen", "Observaciones", "Estatus", "RequiereOP", "OP", "FechaEntregaOP" };
        public static List<String> Flujo8_Columnas = new List<String> { "Seleccion", "ID", "Requisicion", "Usuario", "Fecha", "Proceso", "Observaciones", "Estatus" };

        public static DataTable GetFlujo(String _usuario)
        {
            //usp_ConsultaAreas
            DataTable dt = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.Parameters.Add(new SqlParameter("@usuario", _usuario));
                cmdDatos.ObjectName = "usp_ConsultaFlujoControl";
                dt = cmdDatos.GetDataTable();
            }
            return dt;

        }
        public static DataTable GetResults(String _ClaveTipoProceso, String _ClaveArea, String _Usuario)
        {
            //usp_ConsultaAreas
            DataTable dt = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "usp_getFlujosById";
                cmdDatos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ClaveTipoProceso", int.Parse(_ClaveTipoProceso)));
                cmdDatos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ClaveArea", _ClaveArea));
                cmdDatos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Usuario", _Usuario));
                dt = cmdDatos.GetDataTable();
            }
            return dt;

        }
        public static DataTable GetListaMenus(String _ClaveArea, int _OrdenAgrupador, int _tipoProceso)
        {
            //usp_GetListaMenusUPPedidos
            DataTable dt = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "usp_GetListaMenusContorlFlujo";
                cmdDatos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Area", _ClaveArea));
                cmdDatos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OrdenAgrupador", _OrdenAgrupador));
                cmdDatos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@tipoProceso", _tipoProceso));
                dt = cmdDatos.GetDataTable();
            }
            return dt;

        }
        public static DataTable GetHistoricoFlujo(int _id)
        {
            //usp_ConsultaAreas
            DataTable dt = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "usp_getHistoricoFlujo";
                cmdDatos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Id", _id));
                dt = cmdDatos.GetDataTable();
            }
            return dt;

        }
        public static DataTable GetObservacionesFlujo(int _id)
        {
            //usp_ConsultaAreas
            DataTable dt = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "usp_getObservacionesFlujo";
                cmdDatos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Id", _id));
                dt = cmdDatos.GetDataTable();
            }
            return dt;

        }
        public static void SetFinFlujo(int _id)
        {
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "usp_setFinFlujo";
                cmdDatos.Parameters.Add(new SqlParameter("@Id", _id));
                cmdDatos.Execute();
            }
        }
        public static string GetSiguienteAreaFlujo(String _claveMenu)
        {
            DataTable dt = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "usp_getFlujoSiguienteArea";
                cmdDatos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ClaveMenu", _claveMenu));
                dt = cmdDatos.GetDataTable();
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["Area"].ToString();
                }
                else
                {
                    return "";
                }
            }
        }
    }
}
