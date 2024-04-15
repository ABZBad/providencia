using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ulp_dl.aspel_sae80;
using ulp_dl.SIPReportes;
using ulp_dl.SIPNegocio;
using ulp_bl.Utiles;
using ulp_dl;
using sm_dl.SqlServer;
using sm_dl;

namespace ulp_bl
{
    public class PED_MSTR : ICrud<PED_MSTR>
    {
        private Exception error;
        private bool tieneError;

        public int PEDIDO { get; set; }
        public string CLIENTE { get; set; }
        public DateTime FECHA { get; set; }
        public double? DESCUENTO { get; set; }
        public string TERMINOS { get; set; }
        public double? COMISION { get; set; }
        public string AGENTE { get; set; }
        public string ESTATUS { get; set; }
        public string REMITIDO { get; set; }
        public string CONSIGNADO { get; set; }
        public string OBSERVACIONES { get; set; }
        public decimal? IMPORTE { get; set; }
        public int? PRENDAS { get; set; }
        public double? DESC_DADO { get; set; }
        public int? LISTA { get; set; }
        public string OC { get; set; }
        public DateTime? FECHA_IMPRESION { get; set; }
        public string TIPO { get; set; }
        public string FORMADEPAGOSAT { get; set; }
        public string USO_CFDI { get; set; }
        public string METODODEPAGO { get; set; }
        public int? NUMERO_COTIZACION { get; set; }

        public bool TieneError
        {
            get { return tieneError; }
        }

        public Exception Error
        {
            get { return error; }
        }

        public PED_MSTR Consultar(int ID)
        {
            ulp_bl.PED_MSTR datosPedido = new PED_MSTR();
            using (var dbContext = new AspelSae80Context())
            {
                var result = dbContext.PED_MSTR.Find(ID);
                CopyClass.CopyObject(result, ref datosPedido);
            }
            return datosPedido;
        }
        public DataTable CosultarPedidosCliente(string idCliente)
        {
            DataTable Pedidos = new DataTable();
            using (var dbContext = new AspelSae80Context())
            {
                try
                {

                    var resultado = from pedidos in dbContext.PED_MSTR
                                    where pedidos.CLIENTE.Trim() == idCliente.Trim() && pedidos.ESTATUS != "C" && (pedidos.TIPO == "OV")
                                    orderby pedidos.PEDIDO
                                    select pedidos;
                    Pedidos = Linq2DataTable.CopyToDataTable(resultado);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

            }
            return Pedidos;
        }

        public DataTable CosultarPedidosDATCliente(string idCliente)
        {
            DataTable Pedidos = new DataTable();
            using (var dbContext = new AspelSae80Context())
            {
                var resultado = from pedidos in dbContext.PED_MSTR
                                where pedidos.CLIENTE.Trim() == idCliente.Trim() && pedidos.ESTATUS != "C" && pedidos.TIPO == "DT"
                                orderby pedidos.PEDIDO
                                select pedidos;
                Pedidos = Linq2DataTable.CopyToDataTable(resultado);
                // MULTIPLICAMOS POR 100 EL DESCUENTO
                foreach (DataRow dr in Pedidos.Rows)
                {
                    if (dr["DESCUENTO"].ToString() == "")
                    {
                        dr["DESCUENTO"] = 0;
                    }
                    else
                    {
                        dr["DESCUENTO"] = (double.Parse(dr["DESCUENTO"].ToString()) * 100).ToString();
                    }
                }
            }
            return Pedidos;
        }
        public DataTable CosultarPedidosMOSCliente(string idCliente)
        {
            DataTable Pedidos = new DataTable();
            using (var dbContext = new AspelSae80Context())
            {
                var resultado = from pedidos in dbContext.PED_MSTR
                                where pedidos.CLIENTE.Trim() == idCliente.Trim() && pedidos.ESTATUS != "C" && (pedidos.TIPO == "MS" || pedidos.TIPO == "EC")
                                orderby pedidos.PEDIDO
                                select pedidos;
                Pedidos = Linq2DataTable.CopyToDataTable(resultado);
                // MULTIPLICAMOS POR 100 EL DESCUENTO
                foreach (DataRow dr in Pedidos.Rows)
                {
                    if (dr["DESCUENTO"].ToString() == "")
                    {
                        dr["DESCUENTO"] = 0;
                    }
                    else
                    {
                        dr["DESCUENTO"] = (double.Parse(dr["DESCUENTO"].ToString()) * 100).ToString();
                    }
                }
            }
            return Pedidos;
        }
        public DataTable CosultarPedidosMOSCPCliente(string idCliente)
        {
            DataTable Pedidos = new DataTable();
            using (var dbContext = new AspelSae80Context())
            {
                var resultado = from pedidos in dbContext.PED_MSTR
                                where pedidos.CLIENTE.Trim() == idCliente.Trim() && pedidos.ESTATUS != "C" && pedidos.TIPO == "MP"
                                orderby pedidos.PEDIDO
                                select pedidos;
                Pedidos = Linq2DataTable.CopyToDataTable(resultado);
                // MULTIPLICAMOS POR 100 EL DESCUENTO
                foreach (DataRow dr in Pedidos.Rows)
                {
                    if (dr["DESCUENTO"].ToString() == "")
                    {
                        dr["DESCUENTO"] = 0;
                    }
                    else
                    {
                        dr["DESCUENTO"] = (double.Parse(dr["DESCUENTO"].ToString()) * 100).ToString();
                    }
                }
            }
            return Pedidos;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CriterioABuscar"></param>
        /// <param name="Usuario">Este campo corresponde al campo CLAVE de la tabla USUARIOS de aspel_sae50</param>
        /// <returns></returns>
        public DataTable ConsultarPedidosPorCriterioCliente(string CriterioABuscar, string Usuario)
        {
            DataTable dataTablePedidos = new DataTable();
            SqlServerCommand cmd = new SqlServerCommand();
            using (var dbContext = new SIPNegocioContext())
            {
                /*
                usp_BuscaPedidosPorCriterio
                 */

                cmd.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);

            }

            cmd.ObjectName = "usp_BuscaPedidosPorCriterio";
            cmd.Parameters.Add(new SqlParameter("@criterio", CriterioABuscar));
            cmd.Parameters.Add(new SqlParameter("@clave", Usuario));
            dataTablePedidos = cmd.GetDataTable();
            cmd.Connection.Close();

            return dataTablePedidos;
        }
        public DataTable ConsultaImprimirOrdenTrabajo(int idPedido)
        {
            DataTable datosOT = new DataTable();
            using (var dbContext = new SIPReportesContext())
            {
                sm_dl.SqlServer.SqlServerCommand seleccionar = new sm_dl.SqlServer.SqlServerCommand();
                seleccionar.Connection = sm_dl.DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                seleccionar.ObjectName = "usp_OrdenTrabajo";
                seleccionar.Parameters.Add(new System.Data.SqlClient.SqlParameter("@idPedido", idPedido));
                datosOT = seleccionar.GetDataTable();
            }
            return datosOT;
        }
        public DataTable ConsultaImprimir(int idPedido)
        {
            DataTable datosPedido = new DataTable();
            using (var dbContext = new SIPReportesContext())
            {
                //var resultado = from pedido in dbContext.vw_Pedido
                //                where pedido.PEDIDO == idPedido
                //                select pedido;

                //datosPedido = Linq2DataTable.CopyToDataTable(resultado);

                sm_dl.SqlServer.SqlServerCommand sel = new sm_dl.SqlServer.SqlServerCommand();
                sel.Connection = sm_dl.DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                sel.ObjectName = "usp_Pedido";
                sel.Parameters.Add(new System.Data.SqlClient.SqlParameter("@idPedido", idPedido));
                datosPedido = sel.GetDataTable();
                sel.Connection.Close();
            }
            return datosPedido;
        }
        public static string ConsultaImprimirDevuelveDescuentoCifrado(double descuento)
        {
            string sDescuento = "";
            double nvoDescuento = descuento * 100;
            string sDescuentoOrig = nvoDescuento.ToString();
            if ((descuento * 100) < 10)
            {
                sDescuento = "30" + sDescuentoOrig.Replace(".", "") + "6 I";
            }
            else
            {
                sDescuento = "3" + sDescuentoOrig.Replace(".", "") + "6 I";
            }
            return sDescuento;
        }
        public DataTable ConsultarOrdenTrabajo(string idCliente)
        {
            DataTable oT = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand sel = new sm_dl.SqlServer.SqlServerCommand();
                sel.Connection = sm_dl.DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                sel.ObjectName = "usp_OrdenTrabajoPorcliente";
                sel.Parameters.Add(new System.Data.SqlClient.SqlParameter("@clave_cliente", idCliente));
                oT = sel.GetDataTable();
                sel.Connection.Close();

                //var resultado = from edo in dbContext.vw_EstadoCuenta where edo.CVE_CLIE.Equals(ID) select edo;                               
                //EstadoCuenta =  Linq2DataTable.CopyToDataTable(resultado);

            }
            //var resultado = from ordenTrab in dbContext.PED_MSTR
            //                where ordenTrab.CLIENTE.Trim() == idCliente.Trim() && ordenTrab.TIPO.Equals("OT")
            //                select ordenTrab;

            //ordenesTrabajo = Linq2DataTable.CopyToDataTable(resultado);

            return oT;
        }
        public void Crear(PED_MSTR tEntidad)
        {
            throw new NotImplementedException();
        }
        public void Crear(PED_MSTR tEntidad, ref int idPedido)
        {
            try
            {
                ulp_dl.aspel_sae80.PED_MSTR pedidos = new ulp_dl.aspel_sae80.PED_MSTR();
                //PED_MSTR pedidos = new PED_MSTR();
                using (var dbContext = new AspelSae80Context())
                {
                    CopyClass.CopyObject(tEntidad, ref pedidos);
                    dbContext.PED_MSTR.Add(pedidos);
                    dbContext.SaveChanges();
                    idPedido = pedidos.PEDIDO;
                }
            }
            catch (Exception ex)
            {
                tieneError = true;
                error = ex;
            }

        }

        public void Modificar(PED_MSTR tEntidad)
        {
            try
            {
                using (var dbContext = new AspelSae80Context())
                {
                    //var resultado = dbContext.PED_MSTR.Find(tEntidad.PEDIDO);
                    var resultado = (from res in dbContext.PED_MSTR where res.PEDIDO == tEntidad.PEDIDO select res).FirstOrDefault();
                    //no se utiliza un ciclo por que la consulta linq siempre devolverá sólo un resultado find lo determina
                    resultado.TERMINOS = tEntidad.TERMINOS;
                    resultado.REMITIDO = tEntidad.REMITIDO;
                    resultado.CONSIGNADO = tEntidad.CONSIGNADO;
                    resultado.OBSERVACIONES = tEntidad.OBSERVACIONES;
                    resultado.FORMADEPAGOSAT = tEntidad.FORMADEPAGOSAT;
                    resultado.METODODEPAGO = tEntidad.METODODEPAGO;
                    resultado.USO_CFDI = tEntidad.USO_CFDI;
                    resultado.OC = tEntidad.OC;
                    resultado.DESCUENTO = tEntidad.DESCUENTO;
                    resultado.NUMERO_COTIZACION = tEntidad.NUMERO_COTIZACION;
                    int i = 0;
                    i = dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                tieneError = true;
                error = ex;
            }
        }
        public void ModificarStatus(PED_MSTR tEntidad)
        {
            using (var dbContext = new AspelSae80Context())
            {
                var resultado = dbContext.PED_MSTR.Find(tEntidad.PEDIDO);
                //no se utiliza un ciclo por que la consulta linq siempre devolverá sólo un resultado find lo determina
                resultado.ESTATUS = tEntidad.ESTATUS;
                dbContext.SaveChanges();
            }
        }
        public void ActualizaAcumulados(PED_MSTR tEntidad)
        {
            using (var dbContext = new SIPNegocioContext())
            {
                SqlServerCommand actualiza = new SqlServerCommand();
                actualiza.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                actualiza.ObjectName = "usp_AddPartidasPedi_ActualizaAcumulados_PED_MSTR";
                actualiza.Parameters.Add(new SqlParameter("@Pedido", tEntidad.PEDIDO));
                actualiza.Execute();
            }
        }
        public void CalculaComision(PED_MSTR tEntidad)
        {
            Random rnd = new Random();
            string agrupador;
            agrupador = Convert.ToString(rnd.Next() * DateTime.Now.ToOADate());
            agrupador = agrupador.Substring(0, 10);
            using (var dbContext = new SIPNegocioContext())
            {
                SqlServerCommand actualiza = new SqlServerCommand();
                actualiza.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                actualiza.ObjectName = "usp_GeneraComisionPedido";
                actualiza.Parameters.Add(new SqlParameter("@Pedido", tEntidad.PEDIDO));
                actualiza.Parameters.Add(new SqlParameter("@agrupador", agrupador));
                actualiza.Execute();
            }
        }



        public void Borrar(PED_MSTR tEntidad, Enumerados.TipoBorrado TipoBorrado)
        {
            using (var dbContext = new AspelSae80Context())
            {
                var pedido_a_borrar = dbContext.PED_MSTR.Find(tEntidad.PEDIDO);
                dbContext.PED_MSTR.Remove(pedido_a_borrar);
                dbContext.SaveChanges();
            }
        }


        public DataTable ConsultarTodos()
        {
            throw new NotImplementedException();
        }

        public static void ActualizaFechaEntrega(int _idPedido, DateTime _fechaEntrega)
        {
            using (var dbContext = new SIPReportesContext())
            {

                sm_dl.SqlServer.SqlServerCommand sel = new sm_dl.SqlServer.SqlServerCommand();
                sel.Connection = sm_dl.DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                sel.ObjectName = "usp_SetFechaEntregaPedido";
                sel.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Id", _idPedido));
                sel.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Fecha", _fechaEntrega));
                sel.Execute();
                sel.Connection.Close();
            }

        }

        public DataTable CosultarPedidosVirtualesCliente(string idCliente)
        {
            DataTable pedidos = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand sel = new sm_dl.SqlServer.SqlServerCommand();
                sel.Connection = sm_dl.DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                sel.ObjectName = "usp_ConsultaPedidosVirtuales";
                sel.Parameters.Add(new System.Data.SqlClient.SqlParameter("@clave_cliente", idCliente));
                pedidos = sel.GetDataTable();
                sel.Connection.Close();
            }
            return pedidos;
        }

        public int CreaPedidoVirtualRestante(int Pedido)
        {
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand sel = new sm_dl.SqlServer.SqlServerCommand();
                sel.Connection = sm_dl.DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                sel.ObjectName = "usp_CreaPedidoRestante";
                sel.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PedidoOrigen", Pedido));
                System.Data.SqlClient.SqlParameter parPedidoNuevo = new System.Data.SqlClient.SqlParameter("@PedidoNuevo", SqlDbType.Int);
                parPedidoNuevo.Direction = System.Data.ParameterDirection.Output;
                sel.Parameters.Add(parPedidoNuevo);
                sel.Execute();
                sel.Connection.Close();
                return int.Parse(parPedidoNuevo.Value.ToString());
            }
        }
        public DataTable ValidaDivisionPedido(int Pedido)
        {
            DataTable pedidos = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand sel = new sm_dl.SqlServer.SqlServerCommand();
                sel.Connection = sm_dl.DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                sel.ObjectName = "usp_ValidaDivisionPedido";
                sel.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Pedido", Pedido));
                pedidos = sel.GetDataTable();
                sel.Connection.Close();
            }
            return pedidos;
        }
        public DataTable DividePedidoSinExistencias(int Pedido, bool FacturaTotal)
        {
            DataTable pedidos = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand sel = new sm_dl.SqlServer.SqlServerCommand();
                sel.Connection = sm_dl.DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                sel.ObjectName = "usp_DividePedidoSinExistencias";
                sel.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Pedido", Pedido));
                sel.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FacturaTotal", FacturaTotal));
                pedidos = sel.GetDataTable();
                sel.Connection.Close();
                return pedidos;

            }
        }
        public void AltaPedidoFaltante(int Pedido)
        {
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand sel = new sm_dl.SqlServer.SqlServerCommand();
                sel.Connection = sm_dl.DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                sel.ObjectName = "usp_AltaPedidoFaltante";
                sel.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Pedido", Pedido));
                sel.Execute();
                sel.Connection.Close();
            }
        }
    }
}
