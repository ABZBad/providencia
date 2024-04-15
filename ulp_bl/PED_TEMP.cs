using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ulp_dl.aspel_sae80;
using ulp_dl.SIPNegocio;
using ulp_bl.Utiles;
using ulp_dl;
using System.Data;

namespace ulp_bl
{
    public class PED_TEMP : ICrud<PED_TEMP>
    {
        public int PEDIDO { get; set; }
        public string MODELO { get; set; }
        public string DESCRIPCION { get; set; }
        public string TALLAS { get; set; }
        public string AGRUPADOR { get; set; }
        public int PRENDAS { get; set; }
        public decimal? PRECIO { get; set; }
        public decimal? IMPORTE { get; set; }
        public decimal? PRE_PROCESOS { get; set; }

        public bool TieneError
        {
            get { throw new NotImplementedException(); }
        }

        public Exception Error
        {
            get { throw new NotImplementedException(); }
        }

        public PED_TEMP Consultar(int ID)
        {
            throw new NotImplementedException();
        }

        public DataTable ConsultarPartidasPedido(int ID)
        {
            decimal PRECIO_LISTA = 0;
            DataTable partidasPedido = new DataTable();
            using (var dbContext = new AspelSae80Context())
            {
                var resultado = from pedido in dbContext.PED_TEMP where pedido.PEDIDO == ID orderby pedido.PEDIDO select pedido;
                partidasPedido = Linq2DataTable.CopyToDataTable(resultado);

                DataColumn dtPRECIO_LISTA = new DataColumn("PRECIO_LISTA", typeof(decimal));
                partidasPedido.Columns.Add(dtPRECIO_LISTA);

                DataColumn dtDESCUENTO = new DataColumn("DESCUENTO", typeof(decimal));
                partidasPedido.Columns.Add(dtDESCUENTO);

                var precios = dbContext.PED_DET.Where(p => p.PEDIDO == ID).GroupBy(precio_lista => new { CODIGO = precio_lista.CODIGO.Substring(0, 8), precio_lista.PRECIO_LISTA, PRECIO = precio_lista.PRECIO_PROD });
                if (precios != null && partidasPedido.Rows.Count >= 1)
                {
                    foreach (var precio in precios)
                    {
                        try
                        {
                            partidasPedido.Select(string.Format("MODELO='{0}'", precio.Key.CODIGO)).ToList<DataRow>().ForEach(r => r["PRECIO_LISTA"] = Convert.ToDecimal(precio.Key.PRECIO_LISTA));
                            partidasPedido.Select(string.Format("MODELO='{0}'", precio.Key.CODIGO)).ToList<DataRow>().ForEach(r => r["DESCUENTO"] = (1 - (Convert.ToDecimal(precio.Key.PRECIO) / Convert.ToDecimal(precio.Key.PRECIO_LISTA))));
                        }
                        catch { }
                    }
                }
            }


            return partidasPedido;
        }

        public void Crear(PED_TEMP tEntidad)
        {
            ulp_dl.aspel_sae80.PED_TEMP ped_temp = new ulp_dl.aspel_sae80.PED_TEMP();
            using (var dbContext = new AspelSae80Context())
            {
                CopyClass.CopyObject(tEntidad, ref ped_temp);
                dbContext.PED_TEMP.Add(ped_temp);
                dbContext.SaveChanges();
            }
        }
        public void CrearPorPedido(PED_TEMP tEntidad, Enumerados.TipoPedido tipo)
        {

            vw_EstadoCuenta vw_estadoCuenta = new vw_EstadoCuenta();
            DataTable EstadoCuenta = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand sel = new sm_dl.SqlServer.SqlServerCommand();
                sel.Connection = sm_dl.DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                sel.ObjectName = "usp_Inserta_PED_TEMP_de_PED_DET";
                sel.Parameters.Add(new System.Data.SqlClient.SqlParameter("@idPedido", tEntidad.PEDIDO));
                switch (tipo)
                {
                    case Enumerados.TipoPedido.Pedido:
                    case Enumerados.TipoPedido.PedidoDAT:
                    case Enumerados.TipoPedido.PedidoMOS:
                    case Enumerados.TipoPedido.PedidoEC:
                    case Enumerados.TipoPedido.PedidoMOSCP:
                        sel.Parameters.Add(new System.Data.SqlClient.SqlParameter("@tipo", Enumerados.TipoPedido.Pedido));
                        break;
                    case Enumerados.TipoPedido.OrdenTrabajo:
                        sel.Parameters.Add(new System.Data.SqlClient.SqlParameter("@tipo", tipo));
                        break;
                    default:
                        sel.Parameters.Add(new System.Data.SqlClient.SqlParameter("@tipo", tipo));
                        break;
                }

                sel.Execute();
                sel.Connection.Close();
            }
        }

        public void Modificar(PED_TEMP tEntidad)
        {
            throw new NotImplementedException();
        }

        public void Borrar(PED_TEMP tEntidad, Enumerados.TipoBorrado TipoBorrado)
        {
            using (var dbContext = new AspelSae80Context())
            {
                var resultado = from pedTemp in dbContext.PED_TEMP
                                where pedTemp.PEDIDO == tEntidad.PEDIDO && pedTemp.AGRUPADOR.Trim() == tEntidad.AGRUPADOR && pedTemp.MODELO.Trim() == tEntidad.MODELO
                                select pedTemp;
                dbContext.PED_TEMP.RemoveRange(resultado);
                dbContext.SaveChanges();
            }
        }


        public DataTable ConsultarTodos()
        {
            throw new NotImplementedException();
        }
    }
}
