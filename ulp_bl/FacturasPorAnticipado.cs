using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ulp_bl.Utiles;
using ulp_dl.aspel_sae80;
using ulp_dl.SIPNegocio;
using ulp_dl.SIPReportes;
using System.Data;
using System.Data.SqlClient;
using sm_dl;
using sm_dl.SqlServer;

namespace ulp_bl
{
    public class FacturasPorAnticipado
    {
        public static int getSiguienteFolioAgrupador()
        {
            using (AspelSae80Context DBContext = new AspelSae80Context())
            {
                int ULT_CVE = (int)DBContext.TBLCONTROL01.Where(item=>item.ID_TABLA==32).Max(item=>item.ULT_CVE).Value;
                return ULT_CVE+1;
            }
        }
        public static void setSiguienteFolioAgrupador(int NuevoFolioSiguiente)
        {
            using (AspelSae80Context DBContext = new AspelSae80Context())
            {
                var TBLCONTROL01 = DBContext.TBLCONTROL01.Where(item => item.ID_TABLA == 32).FirstOrDefault();
                if (TBLCONTROL01!=null)
                {
                    TBLCONTROL01.ULT_CVE = NuevoFolioSiguiente;
                    DBContext.Entry(TBLCONTROL01).CurrentValues.SetValues(TBLCONTROL01);
                    DBContext.SaveChanges();
                }
                
            }
        }
        public static void setTransferenciaVirtualArticulo(String Pedido, String ClaveArticulo, int Cantidad, int ClaveFolioAgrupador, String Referencia, ref Exception _ex)
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
                cmd.ObjectName = "[usp_InsertaTransferenciaVirtual]";
                cmd.Parameters.Add(new SqlParameter("@Pedido", Pedido));
                cmd.Parameters.Add(new SqlParameter("@CVE_ART", ClaveArticulo));
                cmd.Parameters.Add(new SqlParameter("@Cantidad", Cantidad));
                cmd.Parameters.Add(new SqlParameter("@Referencia", Referencia.Substring(0,Referencia.Length>19?20:Referencia.Length)));
                cmd.Parameters.Add(new SqlParameter("@CVE_FOLIO_AGRUPADOR", ClaveFolioAgrupador));
                cmd.Execute();
                cmd.Connection.Close();
                _ex = null;
            }
            catch(Exception ex)
            {
                _ex = ex;
            }
        }
        public static DataTable ValidaTransferenciaPedido(String Pedido, ref Exception Ex)
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
                cmd.ObjectName = "usp_ValidaTransferenciaVirtual";
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
    }
}
