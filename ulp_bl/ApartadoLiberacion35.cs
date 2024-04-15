using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ulp_dl.aspel_sae80;
using ulp_dl.SIPNegocio;
using ulp_dl.SIPReportes;
using System.Data;
using System.Data.SqlClient;
using sm_dl;
using sm_dl.SqlServer;

namespace ulp_bl
{
    public class ApartadoLiberacion35
    {
        public static DataTable getDetallePedidoApartado(int Pedido, ref Exception Ex)
        {
            String conStr = "";
            DataTable dtPedidos = new DataTable();
            try
            {
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "usp_getDetallePedidoApartado";
                cmd.Parameters.Add(new SqlParameter("@Pedido", Pedido));

                dtPedidos = cmd.GetDataTable();
                cmd.Connection.Close();
                Ex = null;
                return dtPedidos;
            }
            catch (Exception ex)
            {
                Ex = ex;
                return null;
            }
        }
        public static DataTable ValidaLiberacionPedido(String Pedido, ref Exception Ex)
        {
            String conStr = "";
            DataTable dtPedidos = new DataTable();
            try
            {
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "usp_ValidaLiberacionPedido";
                cmd.Parameters.Add(new SqlParameter("@Pedido", Pedido));

                dtPedidos = cmd.GetDataTable();
                cmd.Connection.Close();
                Ex = null;
                return dtPedidos;
            }
            catch (Exception ex)
            {
                Ex = ex;
                return null;
            }
        }
        public static void setTransferenciaApartadoArticulo(String Pedido, String ClaveArticulo,int Origen, int Cantidad, int ClaveFolioAgrupador, String Referencia, ref Exception _ex)
        {
            try
            {
                String conStr = String.Empty;
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "[usp_InsertaTransferenciaApartado]";
                cmd.Parameters.Add(new SqlParameter("@Pedido", Pedido));
                cmd.Parameters.Add(new SqlParameter("@CVE_ART", ClaveArticulo));
                cmd.Parameters.Add(new SqlParameter("@Origen", Origen));
                cmd.Parameters.Add(new SqlParameter("@Cantidad", Cantidad));
                cmd.Parameters.Add(new SqlParameter("@Referencia", Referencia.Substring(0, Referencia.Length > 19 ? 20 : Referencia.Length)));
                cmd.Parameters.Add(new SqlParameter("@CVE_FOLIO_AGRUPADOR", ClaveFolioAgrupador));
                cmd.Execute();
                cmd.Connection.Close();
                _ex = null;
            }
            catch (Exception ex)
            {
                _ex = ex;
            }
        }
        public static void setLiberaPedidoApartado(String Pedido, ref Exception _ex)
        {
            try
            {
                String conStr = String.Empty;
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "[usp_setLiberarPedidoApartado]";
                cmd.Parameters.Add(new SqlParameter("@Pedido", Pedido));
                cmd.Execute();
                cmd.Connection.Close();
                _ex = null;
            }
            catch (Exception ex)
            {
                _ex = ex;
            }
        }
    }
}
