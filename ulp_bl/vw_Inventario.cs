using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ulp_dl;
using ulp_dl.SIPNegocio;
using ulp_bl.Utiles;
using sm_dl;

namespace ulp_bl
{
    public class PreciosXModelo
    {
        public string TIPO { set; get; }
        public double PRECIO1 { set; get; }
    }
    public class vw_Inventario : ICrud<vw_Inventario>
    {

        public string CLV_ART { get; set; }
        public string DESCR { get; set; }
        public Nullable<double> PRECIO1 { get; set; }
        public string TIPO_ELE { get; set; }


        public bool TieneError
        {
            get { throw new NotImplementedException(); }
        }

        public Exception Error
        {
            get { throw new NotImplementedException(); }
        }

        public vw_Inventario Consultar(int ID)
        {
            throw new NotImplementedException();
        }
        public DataTable Consultar(string clv_art)
        {
            DataTable productos = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                var resultado = from prod in dbContext.vw_Inventario where prod.CLV_ART.Substring(0, 8) == clv_art select prod;
                //CopyClass.CopyObject(resultado, ref productos);
                productos = Linq2DataTable.CopyToDataTable(resultado);
            }
            return productos;
        }
        public DataTable ConsultarConDatosPedido(int pedido, string modelo, string agrupador)
        {
            DataTable resultado = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                //var datos = from reg in dbContext.PED_DET where reg.PEDIDO == ID select reg;
                //resultado = Linq2DataTable.CopyToDataTable(datos);                
                sm_dl.SqlServer.SqlServerCommand datos = new sm_dl.SqlServer.SqlServerCommand();
                datos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                datos.ObjectName = "usp_inventario_con_pedido";
                datos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@idPedido", pedido));
                datos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@modelo", modelo));
                datos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@agrupador", agrupador));
                resultado = datos.GetDataTable();
            }
            return resultado;
        }

        public void Crear(vw_Inventario tEntidad)
        {
            throw new NotImplementedException();
        }

        public void Modificar(vw_Inventario tEntidad)
        {
            throw new NotImplementedException();
        }

        public void Borrar(vw_Inventario tEntidad, Enumerados.TipoBorrado TipoBorrado)
        {
            throw new NotImplementedException();
        }


        public DataTable ConsultarTodos()
        {
            throw new NotImplementedException();
        }

        public static List<PreciosXModelo> TotalDePreciosEnModelo(DataTable InventarioModelo)
        {
            List<PreciosXModelo> lstPreciosXModelo = new List<PreciosXModelo>();


            var preciosBase = InventarioModelo.AsEnumerable().GroupBy(precio => precio["PRECIO1"]);

            if (preciosBase.Count() > 1)
            {

                var precios =
                    InventarioModelo.AsEnumerable()
                        .GroupBy(
                            precio =>
                                new
                                {
                                    PRECIO1 = precio["PRECIO1"],
                                    TIPO =
                                        precio["CLV_ART"].ToString()
                                            .Substring(precio["CLV_ART"].ToString().Length - 2, 2)
                                });

                foreach (var precio in precios)
                {
                    lstPreciosXModelo.Add(new PreciosXModelo()
                    {
                        PRECIO1 = (double)precio.Key.PRECIO1,
                        TIPO = (string)precio.Key.TIPO
                    });
                }
            }


            return lstPreciosXModelo;
        }
        /// <summary>
        /// Regresa el campo DESCR del inventario de productos
        /// </summary>
        /// <param name="MODELO">CLV_ART</param>
        /// <returns></returns>
        public static string RegresaModeloDescripcion(string MODELO)
        {
            string DESCR = "";
            using (var dbContext = new SIPNegocioContext())
            {
                var query = (from inventario in dbContext.vw_Inventario where inventario.CLV_ART == MODELO select inventario.DESCR).SingleOrDefault();
                DESCR = query.ToString();
            }
            return DESCR;
        }
        public DataTable GetExistenciasPorModelo134(string clv_art)
        {
            DataTable resultado = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand datos = new sm_dl.SqlServer.SqlServerCommand();
                datos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                datos.ObjectName = "usp_GetExistenciasPorModelo134";
                datos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CVE_ART", clv_art));
                resultado = datos.GetDataTable();
            }
            return resultado;
        }
        public DataTable ValidaExistenciaTotalPedido134(int pedido)
        {
            DataTable resultado = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand datos = new sm_dl.SqlServer.SqlServerCommand();
                datos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                datos.ObjectName = "usp_ValidaExistenciaTotalPedido134";
                datos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Pedido", pedido));
                resultado = datos.GetDataTable();
            }
            return resultado;
        }
        public DataTable GetExistenciasPorModeloTipoProceso(string clv_art, int tipo)
        {
            DataTable resultado = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand datos = new sm_dl.SqlServer.SqlServerCommand();
                datos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                datos.ObjectName = "usp_GetExistenciasPorModeloTipo";
                datos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CVE_ART", clv_art));
                datos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TIPO", tipo + 2));
                resultado = datos.GetDataTable();
            }
            return resultado;
        }
    }
}
