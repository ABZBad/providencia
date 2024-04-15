using sm_dl;
using sm_dl.SqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ulp_bl.Utiles;
using ulp_dl.aspel_sae80;
using ulp_dl.SIPNegocio;
using ulp_dl.SIPReportes;

namespace ulp_bl
{
    public class CancelaPedido
    {
        public static String AplicaCancelacionPedido(int NumeroPedido)
        {
            String _res=string.Empty;
            DataTable dataTableCancelaPedido = new DataTable();

            LogUPPedidos upPedidosOri = new LogUPPedidos();
            LogUPPedidos upPedidosNvo = new LogUPPedidos();

            //saca snapshot del registro original
            using (var dbContext = new AspelSae80Context())
            {
                var query = (from up in dbContext.UPPEDIDOS where up.PEDIDO == (double)NumeroPedido select up).FirstOrDefault();
                CopyClass.CopyObject(query, ref upPedidosOri);
            }


            using (var dbContext = new SIPReportesContext())
            {
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmd.ObjectName = "usp_CancelaPedido";
                cmd.Parameters.Add(new SqlParameter("@NumeroPedido", NumeroPedido));
                SqlParameter _out = new SqlParameter("@out", SqlDbType.VarChar, 200);
                _out.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(_out);
                cmd.Execute();
                _res = _out.Value.ToString();
                cmd.Connection.Close();
            }

            //saca snapshot del registro ya modificado
            using (var dbContext = new AspelSae80Context())
            {
                var query2 = (from up in dbContext.UPPEDIDOS where up.PEDIDO == (double)NumeroPedido select up).FirstOrDefault();

                CopyClass.CopyObject(query2, ref upPedidosNvo);
            }


            UpPedidosLog.RegistraEntrada(upPedidosOri, upPedidosNvo, "CANCELACIÓN", "Cancelar pedido");
            return _res;
        }
    }
}
