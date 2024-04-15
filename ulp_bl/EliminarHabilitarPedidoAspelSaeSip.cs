using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ulp_bl.Utiles;
using ulp_dl.aspel_sae80;
using ulp_dl.SIPNegocio;
using System.Data;

namespace ulp_bl
{
    public class EliminarHabilitarPedidoAspelSaeSip
    {
        public static string Ejecutar(int pedido, bool aplicaCandadoBloqueo = false, bool aplicaCancelacionDivision = false)
        {
            string resultado = "";


            LogUPPedidos upPedidosOri = new LogUPPedidos();
            LogUPPedidos upPedidosNvo = new LogUPPedidos();

            //saca snapshot del registro original
            using (var dbContext = new AspelSae80Context())
            {
                var query = (from up in dbContext.UPPEDIDOS where up.PEDIDO == (double)pedido select up).FirstOrDefault();
                CopyClass.CopyObject(query, ref upPedidosOri);
            }
            //se procesa el pedido
            using (var dbContext = new SIPNegocioContext())
            {

                sm_dl.SqlServer.SqlServerCommand ejecuta = new sm_dl.SqlServer.SqlServerCommand();
                ejecuta.Connection = sm_dl.DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                ejecuta.ObjectName = "usp_EliminarHabilitarPedidoAspelSaeSip";
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@pedido", pedido));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@aplicaCandadoBloqueo", aplicaCandadoBloqueo));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@aplicaCancelacionDivision", aplicaCancelacionDivision));
                System.Data.SqlClient.SqlParameter parRes = new System.Data.SqlClient.SqlParameter("@resultado", SqlDbType.VarChar, 500);
                parRes.Direction = System.Data.ParameterDirection.Output;
                ejecuta.Parameters.Add(parRes);
                ejecuta.Execute();
                resultado = parRes.Value.ToString();

            }

            //saca snapshot del registro ya modificado
            using (var dbContext = new AspelSae80Context())
            {
                var query2 = (from up in dbContext.UPPEDIDOS where up.PEDIDO == (double)pedido select up).FirstOrDefault();

                CopyClass.CopyObject(query2, ref upPedidosNvo);
            }


            UpPedidosLog.RegistraEntrada(upPedidosOri, upPedidosNvo, "MODIFICACIÓN", "Eliminar / Habilitar pedido (Aspel-SAE/SIP)");


            return resultado;
        }
        public static void CrearPedidoCopia(int pedido, ref int pedidoNuevo, ref string cliente)
        {
            using (var dbContext = new SIPNegocioContext())
            {

                sm_dl.SqlServer.SqlServerCommand ejecuta = new sm_dl.SqlServer.SqlServerCommand();
                ejecuta.Connection = sm_dl.DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                ejecuta.ObjectName = "usp_CreaPedidoCopia";
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PedidoOrigen", pedido));
                System.Data.SqlClient.SqlParameter parPedidoNuevo = new System.Data.SqlClient.SqlParameter("@PedidoNuevo", SqlDbType.VarChar, 500);
                System.Data.SqlClient.SqlParameter parCliente = new System.Data.SqlClient.SqlParameter("@Cliente", SqlDbType.VarChar, 500);
                parPedidoNuevo.Direction = System.Data.ParameterDirection.Output;
                parCliente.Direction = System.Data.ParameterDirection.Output;
                ejecuta.Parameters.Add(parPedidoNuevo);
                ejecuta.Parameters.Add(parCliente);
                ejecuta.Execute();
                pedidoNuevo = int.Parse(parPedidoNuevo.Value.ToString());
                cliente = parCliente.Value.ToString();

            }
        }
    }
}
