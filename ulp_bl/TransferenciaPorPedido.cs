using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ulp_dl.SIPNegocio;
using sm_dl.SqlServer;

namespace ulp_bl
{
    public class TransferenciaPorPedido
    {
        public static DataTable DevuelveDatosConsulta(int Pedido,int origen,int destino)
        {
            DataTable datos = new DataTable();
            using (var dbContext=new SIPNegocioContext())
            {
                SqlServerCommand resultado = new SqlServerCommand();
                resultado.Connection = sm_dl.DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                resultado.ObjectName = "usp_frmTransferenciaXPedido";
                resultado.Parameters.Add(new SqlParameter("@pedido", Pedido));
                resultado.Parameters.Add(new SqlParameter("@origen", origen));
                resultado.Parameters.Add(new SqlParameter("@destino", destino));

                datos = resultado.GetDataTable();

                resultado.Connection.Close();
            }
            return datos;
        }
        public static DataTable Procesar(int Pedido, int origen, int destino)
        {
            DataTable datos = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                SqlServerCommand resultado = new SqlServerCommand();
                resultado.Connection = sm_dl.DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                resultado.ObjectName = "usp_frmTransferenciaXPedidoProcesar";
                resultado.Parameters.Add(new SqlParameter("@pedido", Pedido));
                resultado.Parameters.Add(new SqlParameter("@origen", origen));
                resultado.Parameters.Add(new SqlParameter("@destino", destino));

                datos = resultado.GetDataTable();
            }
            return datos;
        }

    }
}
