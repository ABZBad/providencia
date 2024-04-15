using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using sm_dl;
using sm_dl.SqlServer;
using ulp_bl.Utiles;
using ulp_dl.aspel_sae80;
using ulp_dl.SIPNegocio;

namespace ulp_bl
{
    public class CargaPedidosSAE
    {
        public static EstandaresPedido RegresaEstandares()
        {
            return StandPedi.RegresarEstandares();
        }

        public static bool CargaPedidoEnSae(int NumeroPedido, decimal Comision, decimal IVA, ref Exception ExceptionParameter, String _Prefijo)
        {
            if (_Prefijo == "DT") { _Prefijo = "D"; }
            if (_Prefijo == "MS") { _Prefijo = "M"; }
            if (_Prefijo == "MP") { _Prefijo = "MP"; }
            if (_Prefijo == "EC") { _Prefijo = "E"; }

            //se concatena la P para buscar en la table y checar si existe un registro ya guardado
            string pNumeroPedido = string.Format("{0}{1}", _Prefijo, NumeroPedido);
            string sNumeroPedido = NumeroPedido.ToString();
            string rfcCliente = "";
            int totalPartidas = 0; decimal CantidadTotal = 0; decimal DescuentoTotal = 0; decimal ProcesoTotal = 0;
            String _TIPO = "";
            if (_Prefijo == "D")
                _TIPO = "DT";
            if (_Prefijo == "DT")
                _TIPO = "DT";
            if (_Prefijo == "M")
                _TIPO = "MS";
            if (_Prefijo == "P")
                _TIPO = "OV";
            if (_Prefijo == "E")
                _TIPO = "EC";
            if (_Prefijo == "EC")
                _TIPO = "EC";
            if (_Prefijo == "MP")
                _TIPO = "MP";
            if (_Prefijo == "MS")
                _TIPO = "MS";
            //clases para almacenar información del Log de UPPEDIDOS
            LogUPPedidos upPedidoOri = new LogUPPedidos();
            LogUPPedidos upPedidoModi = new LogUPPedidos();

            /*EN CASO DE QUE SEA DAT/MOSTRADOR OBTENEMOS LOS DISTINTOS PROCESOS Y AGRUPAMOS*/
            DataTable dtProcesos = new DataTable("Procesos");
            dtProcesos.Columns.Add("Proceso", typeof(string));
            dtProcesos.Columns.Add("Precio", typeof(decimal));


            using (var dbContext = new AspelSae80Context())
            {

                #region Obtiene Spapshot actual de UPPEDIDOS para el Log

                using (var dbContextSae60 = new AspelSae80Context())
                {
                    var queryUpPedido = (from upOri in dbContextSae60.UPPEDIDOS where upOri.PEDIDO == NumeroPedido select upOri).FirstOrDefault();
                    CopyClass.CopyObject(queryUpPedido, ref upPedidoOri);
                }
                #endregion

                using (var tran = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        #region Se verifica que el pedido que se intenta importar no esté cancelado

                        var statusQuery = (from uppedido in dbContext.UPPEDIDOS where uppedido.PEDIDO == NumeroPedido select uppedido).SingleOrDefault();
                        if (statusQuery != null)
                        {
                            if (statusQuery.ESTATUS_UPPEDIDOS != null)
                            {
                                if (statusQuery.ESTATUS_UPPEDIDOS.ToUpper().Trim() == "C")
                                {
                                    ExceptionParameter =
                                        new Exception(
                                            "Este pedido está cancelado, no es posible importarlo a Aspel-SAE 7.0");
                                    ExceptionParameter.Source = "UPPEDIDOS";
                                    return false;
                                }
                            }
                        }
                        #endregion
                        #region Se verifica que no se haya guardado el pedido con anterioridad

                        //Se verifica que no se haya guardado el pedido con anterioridad
                        var queryFact =
                            (from factp01 in dbContext.FACTP01
                             where factp01.CVE_DOC == pNumeroPedido && factp01.TIP_DOC == "P" //(!esDAT ? "P" : "D")
                             select factp01);
                        if (queryFact.Any())
                        {
                            ExceptionParameter =
                                new Exception("Este pedido ya ha sido previamente capturado en Aspel-SAE 7.0");
                            ExceptionParameter.Source = "FACTP01";
                            return false;
                        }

                        #endregion
                        #region Se verifica que el pedido tenga partidas en caso contrario se almacena el total de partidas

                        //Verificamos que el pedido exista
                        /*
                        if (!_Prefijo.Contains("P"))
                        {

                            if (_Prefijo == "D")
                                _TIPO = "DT";
                            if (_Prefijo == "M")
                                _TIPO = "MS";
                            // PARA TODOS LOS PEDIDOS QUE NO SON P       
                            var queryPed = from ped in dbContext.PED_MSTR
                                           where ped.PEDIDO == NumeroPedido && ped.TIPO == _TIPO
                                           select ped;
                            if (!queryPed.Any())
                            {
                                ExceptionParameter = new Exception("El pedido a capturar no existe.");
                                ExceptionParameter.Source = "PED_DET";
                                return false;
                            }
                        }
                        else
                        {
                            var queryPed = from ped in dbContext.PED_MSTR
                                           where ped.PEDIDO == NumeroPedido && ped.TIPO == _TIPO
                                           select ped;
                            if (!queryPed.Any())
                            {
                                ExceptionParameter = new Exception("El pedido a capturar no existe.");
                                ExceptionParameter.Source = "PED_DET";
                                return false;
                            }
                        }
                        */

                        var queryPed = from ped in dbContext.PED_MSTR
                                       where ped.PEDIDO == NumeroPedido && ped.TIPO == _TIPO
                                       select ped;
                        if (!queryPed.Any())
                        {
                            ExceptionParameter = new Exception("El pedido a capturar no existe.");
                            ExceptionParameter.Source = "PED_DET";
                            return false;
                        }

                        //Se verifica que el pedido tenga partidas en caso contrario se almacena el total de partidas
                        var queryPed_Det = from ped_det in dbContext.PED_DET
                                           where ped_det.CANTIDAD > 0 && ped_det.PEDIDO == NumeroPedido
                                           select ped_det;
                        if (!queryPed_Det.Any())
                        {
                            ExceptionParameter = new Exception("Pedido con cantidades en 0 o sin partidas");
                            ExceptionParameter.Source = "PED_DET";
                            return false;
                        }
                        else
                        {
                            totalPartidas = queryPed_Det.Count();
                            //+ 1; //referencia: Paso 4 (Linea 72) del código original.

                        }

                        #endregion
                        #region Se calcula el Importe de las Prendas (Precio x Cantidad)

                        //Se calcula el Importe de las Prendas (Precio x Cantidad)
                        String modelo_aux = "";
                        foreach (ulp_dl.aspel_sae80.PED_DET pedDet in queryPed_Det)
                        {
                            if (_Prefijo != "P") //EN CASO DE DAT, PUEDE APLICAR DESCUENTO POR LO TANTO SE TOMA EL PRECIO DE LISTA PARA ENVIAR DETALLE A SAE
                            {
                                // IMPORTE TOTAL 
                                if (pedDet.PRECIO_PROD > pedDet.PRECIO_LISTA)
                                {
                                    CantidadTotal += Convert.ToDecimal(pedDet.PRECIO_PROD) * Convert.ToInt32(pedDet.CANTIDAD);
                                    DescuentoTotal = 0;
                                }
                                else
                                {
                                    CantidadTotal += Convert.ToDecimal(pedDet.PRECIO_LISTA) * Convert.ToInt32(pedDet.CANTIDAD);
                                    DescuentoTotal += Convert.ToDecimal(pedDet.PRECIO_LISTA - pedDet.PRECIO_PROD) * Convert.ToInt32(pedDet.CANTIDAD);
                                }

                                // 
                                ProcesoTotal += Convert.ToDecimal(pedDet.PREC_PROCESO) * Convert.ToInt32(pedDet.CANTIDAD);
                                // OBTENEMOS PROCESOS DEL ARTICULO
                                CMT_DET cmtDet = new CMT_DET();
                                DataTable dtProcesosPedido = new DataTable();
                                if (modelo_aux != pedDet.CODIGO.Substring(0, 8) + pedDet.AGRUPADOR)
                                {
                                    dtProcesosPedido = cmtDet.Consultar2(int.Parse(sNumeroPedido), pedDet.CODIGO.Substring(0, 8), pedDet.AGRUPADOR);
                                    modelo_aux = pedDet.CODIGO.Substring(0, 8) + pedDet.AGRUPADOR;
                                }
                                //RECORREMOS CADA PROCESO Y AGRUPAMOS
                                foreach (DataRow _dr in dtProcesosPedido.Rows)
                                {
                                    DataRow drBusquedaProceso = dtProcesos.Select("Proceso = '" + _dr["CMT_PROCESO"].ToString() + "'").FirstOrDefault();
                                    if (drBusquedaProceso != null)
                                    {
                                        drBusquedaProceso["Precio"] = decimal.Parse(drBusquedaProceso["Precio"].ToString()) + decimal.Parse(_dr["CMT_CANTIDAD"].ToString()) * decimal.Parse(_dr["CMT_PRE_PROCESO"].ToString());

                                    }
                                    else
                                    {
                                        DataRow drProceso = dtProcesos.NewRow();
                                        drProceso["Proceso"] = _dr["CMT_PROCESO"].ToString();
                                        drProceso["Precio"] = decimal.Parse(_dr["CMT_CANTIDAD"].ToString()) * decimal.Parse(_dr["CMT_PRE_PROCESO"].ToString());
                                        dtProcesos.Rows.Add(drProceso);
                                    }
                                }
                            }
                            else
                            {
                                CantidadTotal += Convert.ToDecimal(pedDet.SUBTOTAL) * Convert.ToInt32(pedDet.CANTIDAD);
                            }
                        }

                        #endregion
                        #region Se obtiene el RFC del Cliente

                        //Se obtiene el RFC del Cliente
                        ulp_dl.aspel_sae80.PED_MSTR ped_Mstr =
                            (from pedMstr in dbContext.PED_MSTR where pedMstr.PEDIDO == NumeroPedido select pedMstr)
                                .SingleOrDefault();
                        string claveCliente = ped_Mstr.CLIENTE.Trim();
                        var queryClienteRfc =
                            (from clie01 in dbContext.CLIE01
                             where clie01.CLAVE.Trim() == claveCliente
                             select new { clie01.RFC })
                                .SingleOrDefault();
                        rfcCliente = queryClienteRfc.RFC;

                        #endregion
                        #region Carga del encabezado del pedido FACTP01 y se agrega un nuevo registro

                        //Carga del encabezado del pedido FACTP01 y se agrega un nuevo registro (Linea 110 del código original)
                        ulp_dl.aspel_sae80.FACTP01 factP01 = new ulp_dl.aspel_sae80.FACTP01();
                        factP01.TIP_DOC = "P"; //!esDAT ? "P" : "D";
                        factP01.CVE_DOC = pNumeroPedido;
                        //aquí se hace una validación y se rellena c13DG1on espacios a la derecha
                        //la razón? aún no se sabe, tan solo se adapta la mis funcionalidad por compatibilidad
                        //Código original: (Línea 116 proyecto original de VB6)
                        int cveClientePedMstr = 0;
                        bool isCveClienteIsNumeric = int.TryParse(ped_Mstr.CLIENTE.Trim(), out cveClientePedMstr);
                        if (isCveClienteIsNumeric)
                        {
                            factP01.CVE_CLPV = ped_Mstr.CLIENTE.Trim().PadLeft(10);
                        }
                        else
                        {
                            factP01.CVE_CLPV = ped_Mstr.CLIENTE.Trim();
                        }
                        factP01.STATUS = "E";
                        factP01.DAT_MOSTR = 0;
                        factP01.CVE_VEND = ped_Mstr.AGENTE.Trim();
                        factP01.CVE_PEDI = ped_Mstr.OC;
                        factP01.FECHA_DOC = ped_Mstr.FECHA.Date;
                        factP01.FECHA_ENT = ped_Mstr.FECHA.Date;
                        factP01.FECHA_VEN = ped_Mstr.FECHA.Date;
                        factP01.FECHA_CANCELA = null;
                        if (_Prefijo != "P") //EN CASO DE DAT, EL IMPORTE NO INCLUYE LOS PROCESOS (ESTOS SE AGRAGARÁN COMO OTRA PARTIDA LLAMADA "+DIFEREN")
                        {
                            factP01.CAN_TOT = Convert.ToDouble(CantidadTotal);
                        }
                        else
                        {
                            factP01.CAN_TOT = Convert.ToDouble(CantidadTotal + ProcesoTotal);
                        }

                        factP01.IMP_TOT1 = 0;
                        factP01.IMP_TOT2 = 0;
                        factP01.IMP_TOT3 = 0;
                        factP01.IMP_TOT4 = Convert.ToDouble((CantidadTotal - DescuentoTotal + ProcesoTotal) * IVA / 100);
                        factP01.DES_TOT = Convert.ToDouble(DescuentoTotal);
                        factP01.DES_FIN = 0;
                        factP01.COM_TOT = Convert.ToDouble(CantidadTotal * Comision / 100);

                        factP01.CONDICION = ped_Mstr.TERMINOS;
                        factP01.CVE_OBS = 0;
                        factP01.NUM_ALMA = 1;
                        factP01.ACT_CXC = "S";
                        factP01.ACT_COI = "S";
                        factP01.ENLAZADO = "O";
                        factP01.TIP_DOC_E = "O";
                        factP01.NUM_MONED = 1;
                        factP01.TIPCAMB = 1;
                        factP01.NUM_PAGOS = 0;
                        factP01.FECHAELAB = DateTime.Now;
                        factP01.PRIMERPAGO = 0;
                        factP01.RFC = rfcCliente;
                        factP01.CTLPOL = 0;
                        factP01.ESCFD = "N";
                        factP01.AUTORIZA = 0;
                        factP01.SERIE = "";
                        factP01.FOLIO = 0;
                        factP01.AUTOANIO = "";
                        factP01.DAT_ENVIO = 0;
                        factP01.CONTADO = "N";
                        factP01.CVE_BITA = null;
                        factP01.BLOQ = "N";
                        factP01.FORMAENVIO = null;
                        factP01.DES_FIN_PORC = 0;
                        factP01.DES_TOT_PORC = 0;
                        factP01.IMPORTE = Convert.ToDouble((CantidadTotal - DescuentoTotal + ProcesoTotal) + ((CantidadTotal - DescuentoTotal + ProcesoTotal) * IVA / 100));
                        factP01.COM_TOT_PORC = Convert.ToDouble(Comision);
                        factP01.METODODEPAGO = "";
                        factP01.NUMCTAPAGO = "";
                        factP01.FORMADEPAGOSAT = ped_Mstr.FORMADEPAGOSAT;
                        factP01.USO_CFDI = ped_Mstr.USO_CFDI;
                        factP01.METODODEPAGO = ped_Mstr.METODODEPAGO;

                        dbContext.FACTP01.Add(factP01);
                        dbContext.SaveChanges();


                        #endregion
                        #region Se graba el detalle de las partidas en PAR_FACTP01

                        int noPartida = 0;
                        foreach (ulp_dl.aspel_sae80.PED_DET pedDet in queryPed_Det)
                        {
                            //se obtiene el detalle del artículo del inventario
                            string codigo = pedDet.CODIGO.Trim();
                            PAR_FACTP01 par_Factp01 = new PAR_FACTP01();
                            INVE01 articuloInventario01 = new INVE01();
                            using (var dbContext2 = new AspelSae80Context())
                            {
                                articuloInventario01 = (from inve01 in dbContext2.INVE01
                                                        where inve01.CVE_ART.Trim() == codigo
                                                        select inve01).SingleOrDefault();



                                noPartida++;
                                par_Factp01.CVE_DOC = pNumeroPedido;
                                par_Factp01.NUM_PAR = noPartida;
                                par_Factp01.CVE_ART = pedDet.CODIGO.ToUpper();
                                par_Factp01.CANT = pedDet.CANTIDAD;
                                par_Factp01.PXS = pedDet.CANTIDAD;
                                if (_Prefijo != "P")
                                {
                                    if (pedDet.PRECIO_PROD > pedDet.PRECIO_LISTA)
                                    {
                                        par_Factp01.PREC = Convert.ToDouble(pedDet.PRECIO_PROD);
                                    }
                                    else
                                    {
                                        par_Factp01.PREC = Convert.ToDouble(pedDet.PRECIO_LISTA);
                                    }
                                }
                                else
                                {
                                    par_Factp01.PREC = Convert.ToDouble(pedDet.SUBTOTAL);
                                }

                                par_Factp01.COST = 0;
                                par_Factp01.IMPU1 = 0;
                                par_Factp01.IMPU2 = 0;
                                par_Factp01.IMPU3 = 0;
                                par_Factp01.IMPU4 = Convert.ToDouble(IVA);
                                par_Factp01.IMP1APLA = 4;
                                par_Factp01.IMP2APLA = 4;
                                par_Factp01.IMP3APLA = 4;
                                par_Factp01.IMP4APLA = 0;
                                par_Factp01.TOTIMP1 = 0;
                                par_Factp01.TOTIMP2 = 0;
                                par_Factp01.TOTIMP3 = 0;
                                par_Factp01.TOTIMP4 = (Convert.ToDouble(par_Factp01.CANT) *
                                                            Convert.ToDouble(par_Factp01.PREC) -
                                                            (Convert.ToDouble(par_Factp01.CANT) *
                                                             Convert.ToDouble(par_Factp01.PREC) *
                                                             Convert.ToDouble(ped_Mstr.DESCUENTO))) * Convert.ToDouble(IVA) / 100;
                                if (_Prefijo != "P")
                                {
                                    par_Factp01.DESC1 = pedDet.DESCUENTO * 100;
                                }
                                else
                                {
                                    par_Factp01.DESC1 = 0;
                                }
                                par_Factp01.DESC2 = 0;
                                par_Factp01.DESC3 = 0;
                                par_Factp01.COMI = Convert.ToDouble(Comision);
                                par_Factp01.APAR = 0;
                                par_Factp01.ACT_INV = "S";
                                par_Factp01.NUM_ALM = 1;
                                par_Factp01.POLIT_APLI = "";
                                par_Factp01.TIP_CAM = 1;
                                par_Factp01.UNI_VENTA = articuloInventario01.UNI_MED;
                                par_Factp01.TIPO_PROD = articuloInventario01.TIPO_ELE;
                                par_Factp01.CVE_OBS = 0;
                                par_Factp01.REG_SERIE = 0;
                                par_Factp01.E_LTPD = 0;
                                par_Factp01.TIPO_ELEM = "N";
                                par_Factp01.NUM_MOV = null;
                                par_Factp01.TOT_PARTIDA = Convert.ToDouble(pedDet.CANTIDAD * par_Factp01.PREC);

                                articuloInventario01.PEND_SURT = articuloInventario01.PEND_SURT + pedDet.CANTIDAD;
                                dbContext2.SaveChanges();
                            }
                            dbContext.PAR_FACTP01.Add(par_Factp01);
                        }
                        //VERIFICAMOS SI ES DAT/MOSTRADOR, EN CASO DE SER ASI CREAMOS UNA PARTIDA DEPENDIENDO DEL PROCESO

                        if (_Prefijo != "P" && ProcesoTotal > 0)
                        {
                            string codigo = "";
                            foreach (DataRow _dr in dtProcesos.Rows)
                            {
                                //se obtiene el detalle del artículo del inventario
                                //procesos aplicables FCBRIDEL
                                switch (_dr["Proceso"].ToString())
                                {
                                    case "F":
                                        codigo = "+LOGISTICA";
                                        break;
                                    case "C":
                                        codigo = "+COSTURA";
                                        break;
                                    case "B":
                                        codigo = "+BORDADO";
                                        break;
                                    case "R":
                                        codigo = "+COSTURA";
                                        break;
                                    case "I":
                                        codigo = "+INICIALES";
                                        break;
                                    case "D":
                                        codigo = "+DIBUJO";
                                        break;
                                    case "E":
                                        codigo = "+ESTAMPADO";
                                        break;
                                    case "L":
                                        codigo = "+LARGO";
                                        break;
                                    default:
                                        codigo = "+DIFERENCIA";
                                        break;

                                }

                                PAR_FACTP01 par_Factp01 = new PAR_FACTP01();
                                INVE01 articuloInventario01 = new INVE01();
                                using (var dbContext2 = new AspelSae80Context())
                                {
                                    articuloInventario01 = (from inve01 in dbContext2.INVE01
                                                            where inve01.CVE_ART.Trim() == codigo
                                                            select inve01).SingleOrDefault();



                                    noPartida++;
                                    par_Factp01.CVE_DOC = pNumeroPedido;
                                    par_Factp01.NUM_PAR = noPartida;
                                    par_Factp01.CVE_ART = codigo;
                                    par_Factp01.CANT = 1;
                                    par_Factp01.PXS = 1;
                                    par_Factp01.PREC = Convert.ToDouble(_dr["Precio"].ToString());
                                    par_Factp01.COST = 0;
                                    par_Factp01.IMPU1 = 0;
                                    par_Factp01.IMPU2 = 0;
                                    par_Factp01.IMPU3 = 0;
                                    par_Factp01.IMPU4 = Convert.ToDouble(IVA);
                                    par_Factp01.IMP1APLA = 4;
                                    par_Factp01.IMP2APLA = 4;
                                    par_Factp01.IMP3APLA = 4;
                                    par_Factp01.IMP4APLA = 0;
                                    par_Factp01.TOTIMP1 = 0;
                                    par_Factp01.TOTIMP2 = 0;
                                    par_Factp01.TOTIMP3 = 0;
                                    par_Factp01.TOTIMP4 = Convert.ToDouble(Convert.ToDouble(_dr["Precio"].ToString()) * Convert.ToDouble(IVA) / 100);
                                    par_Factp01.DESC1 = 0;
                                    par_Factp01.DESC2 = 0;
                                    par_Factp01.DESC3 = 0;
                                    par_Factp01.COMI = 0;
                                    par_Factp01.APAR = 0;
                                    par_Factp01.ACT_INV = "S";
                                    par_Factp01.NUM_ALM = 1;
                                    par_Factp01.POLIT_APLI = "";
                                    par_Factp01.TIP_CAM = 1;
                                    par_Factp01.UNI_VENTA = articuloInventario01.UNI_MED;
                                    par_Factp01.TIPO_PROD = articuloInventario01.TIPO_ELE;
                                    par_Factp01.CVE_OBS = 0;
                                    par_Factp01.REG_SERIE = 0;
                                    par_Factp01.E_LTPD = 0;
                                    par_Factp01.TIPO_ELEM = "N";
                                    par_Factp01.NUM_MOV = null;
                                    par_Factp01.TOT_PARTIDA = Convert.ToDouble(_dr["Precio"].ToString());

                                    articuloInventario01.PEND_SURT = 0;
                                    dbContext2.SaveChanges();
                                    dbContext.PAR_FACTP01.Add(par_Factp01);
                                }
                            }
                        }


                        dbContext.SaveChanges();

                        #endregion

                        tran.Commit();
                        #region Se actualiza UPPEDIDOS y se enlazan los Estándares actuales al Pedido
                        //Se actualiza UPPEDIDOS y se enlazan los Estándares actuales al Pedido
                        using (var dbContext3 = new SIPNegocioContext())
                        {
                            using (var tranSIP = dbContext3.Database.BeginTransaction())
                            {
                                try
                                {
                                    var filasAfectadas = dbContext3.Database.ExecuteSqlCommand("usp_ActualizaUPPedidosCapturaSae @NumeroPedido = {0}", NumeroPedido);
                                    tranSIP.Commit();

                                }
                                catch (Exception Ex3)
                                {
                                    tranSIP.Rollback();
                                    //tran.Rollback();
                                }
                            }
                        }

                        #endregion

                    }
                    catch (Exception Ex)
                    {
                        tran.Rollback();
                        ExceptionParameter = Ex;
                    }

                }

                #region Obtiene Spapshot de cómo quedó UPPEDIDOS para el Log

                using (var dbContextSae60 = new AspelSae80Context())
                {
                    var queryUpPedido = (from upOri in dbContextSae60.UPPEDIDOS where upOri.PEDIDO == NumeroPedido select upOri).FirstOrDefault();
                    CopyClass.CopyObject(queryUpPedido, ref upPedidoModi);
                }
                #endregion

                #region Se registra la entrada al Log de UPPEDIDOS

                UpPedidosLog.RegistraEntrada(upPedidoOri, upPedidoModi, "MODIFICACIÓN", "Capturar pedido en Aspel-SAE");

                #endregion
            }
            return false;
        }

        public static DataTable RegresaPedidoSae(int NumeroPedido, String _Prefijo = "P")
        {
            string connStr = "";
            using (var dbContext = new SIPNegocioContext())
            {
                connStr = dbContext.Database.Connection.ConnectionString;
            }
            SqlServerCommand cmd = new SqlServerCommand();
            cmd.Connection = DALUtil.GetConnection(connStr);
            cmd.ObjectName = "usp_RepPedidosCapturaSae";
            cmd.Parameters.Add(new SqlParameter("@numero_pedido", String.Format("{0}{1}", _Prefijo, NumeroPedido.ToString())));

            DataTable dtPedidoSae = cmd.GetDataTable();
            cmd.Connection.Close();
            return dtPedidoSae;


        }

        public static bool ValidaPedidoFacturado(int NumeroPedido, String _Prefijo)
        {
            //se concatena la P para buscar en la table y checar si existe un registro ya guardado
            string pNumeroPedido = string.Format("{0}{1}", _Prefijo, NumeroPedido);
            string sNumeroPedido = NumeroPedido.ToString();
            String _TIPO = "";
            if (_Prefijo == "D")
                _TIPO = "DT";
            if (_Prefijo == "M")
                _TIPO = "MS";
            if (_Prefijo == "P")
                _TIPO = "OV";
            if (_Prefijo == "E")
                _TIPO = "EC";
            if (_Prefijo == "MP")
                _TIPO = "MP";

            using (var dbContext = new AspelSae80Context())
            {
                #region Se verifica que el pedido que se intenta importar no esté cancelado

                var statusQuery = (from uppedido in dbContext.UPPEDIDOS where uppedido.PEDIDO == NumeroPedido select uppedido).SingleOrDefault();
                if (statusQuery != null)
                {
                    if (statusQuery.ESTATUS_UPPEDIDOS != null)
                    {
                        if (statusQuery.ESTATUS_UPPEDIDOS.ToUpper().Trim() == "C")
                        {
                            return false;
                        }
                    }
                }
                #endregion
                #region Se verifica que no se haya guardado el pedido con anterioridad

                //Se verifica que no se haya guardado el pedido con anterioridad
                var queryFact =
                    (from factp01 in dbContext.FACTP01
                     where factp01.CVE_DOC == pNumeroPedido && factp01.TIP_DOC == "P" //(!esDAT ? "P" : "D")
                     select factp01);
                if (queryFact.Any())
                {
                    return false;
                }
                #endregion
            }
            return true;
        }


    }
}

