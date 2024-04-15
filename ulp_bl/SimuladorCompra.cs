using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using sm_dl;
using sm_dl.SqlServer;
using ulp_dl;
//using ulp_dl.aspel_prod30;
using ulp_dl.aspel_sae80;
using ulp_dl.SIPNegocio;
using System.Data.SqlClient;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using ulp_bl.Utiles;
using System.IO;


namespace ulp_bl
{
    public class SimuladorCompra
    {
        #region "Atribuos y Constructor"
        public List<Cotizacion> Cotizaciones { get; set; }
        public DateTime Fecha { get; set; }
        public Decimal PresupuestoCobranza { get; set; }
        public Decimal Porcentaje { get; set; }
        public decimal PresupuestoCobranzaFinal { get { return this.PresupuestoCobranza * ((this.Porcentaje / 100)); } }
        public decimal Total { get; set; }
        public decimal Diferencia { get; set; }
        public SimuladorCompra()
        {
            this.Fecha = DateTime.Now;
            this.Cotizaciones = new List<Cotizacion> { };
            this.PresupuestoCobranza = 0;
            this.Porcentaje = 52;
            this.Total = 0;
            this.Diferencia = 0;
        }
        #endregion
        #region "Metodos"
        public Decimal getTotal()
        {
            Decimal total = 0;
            foreach (Cotizacion oCotizacion in this.Cotizaciones)
            {
                foreach (Articulo oArticulo in oCotizacion.Articulos)
                {
                    total += oArticulo.Total;
                }
            }
            return total;
        }
        public static DataTable getArticulosSimulador()
        {
            String conStr = "";
            DataTable dtArticulos = new DataTable();
            try
            {
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "usp_getArticulosSimuladorCompras";
                dtArticulos = cmd.GetDataTable();
                cmd.Connection.Close();
                return dtArticulos;
            }
            catch
            {
                return null;
            }

        }
        public static DataTable getProveedoresSimulador()
        {
            {
                String conStr = "";
                DataTable dtProveedores = new DataTable();
                try
                {
                    using (var dbContext = new SIPNegocioContext())
                    {
                        conStr = dbContext.Database.Connection.ConnectionString;
                    }
                    SqlServerCommand cmd = new SqlServerCommand();
                    cmd.Connection = DALUtil.GetConnection(conStr);
                    cmd.ObjectName = "usp_getProveedoresSimuladorCompras";
                    dtProveedores = cmd.GetDataTable();
                    cmd.Connection.Close();
                    return dtProveedores;
                }
                catch
                {
                    return null;
                }

            }
        }
        public static DataTable getSimuladorCompras()
        {
            {
                String conStr = "";
                DataTable dtProveedores = new DataTable();
                try
                {
                    using (var dbContext = new SIPNegocioContext())
                    {
                        conStr = dbContext.Database.Connection.ConnectionString;
                    }
                    SqlServerCommand cmd = new SqlServerCommand();
                    cmd.Connection = DALUtil.GetConnection(conStr);
                    cmd.ObjectName = "usp_getSimuladorCompras";
                    dtProveedores = cmd.GetDataTable();
                    cmd.Connection.Close();
                    return dtProveedores;
                }
                catch
                {
                    return null;
                }

            }
        }
        public static SimuladorCompra getSimuladorCompraByID(int Clave)
        {
            String conStr = "";
            DataSet dsProveedores = new DataSet();
            SimuladorCompra oSimulador = new SimuladorCompra();
            try
            {
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "usp_getSimuladorCompraById";
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Clave", Clave));
                dsProveedores = cmd.GetDataSet();
                cmd.Connection.Close();

                if (dsProveedores != null)
                {
                    if (dsProveedores.Tables.Count > 0)
                    {
                        //OBTENEMOS LOS DATOS DEL ENCABEZADO
                        oSimulador.PresupuestoCobranza = (decimal)dsProveedores.Tables[0].Rows[0]["PresupuestoCobranza"];
                        oSimulador.Porcentaje = (decimal)dsProveedores.Tables[0].Rows[0]["Porcentaje"];
                        oSimulador.Total = (decimal)dsProveedores.Tables[0].Rows[0]["Total"];
                        oSimulador.Diferencia = (decimal)dsProveedores.Tables[0].Rows[0]["Diferencia"];
                        oSimulador.Fecha = (DateTime)dsProveedores.Tables[0].Rows[0]["Fecha"];
                        foreach (DataRow dr in dsProveedores.Tables[1].Rows)
                        {
                            Cotizacion oCotizacion = new Cotizacion();
                            oCotizacion.ClaveProveedor = dr["ClaveProveedor"].ToString();
                            oCotizacion.NombreProveedor = dr["Proveedor"].ToString();
                            //BUSCAMOS LOS ARTICULOS QUE LE CORRESPONDEN AL PROVEEDOR
                            foreach (DataRow dr2 in dsProveedores.Tables[2].Rows)
                            {
                                if (dr["Clave"].ToString() == dr2["ClaveSimuladorCompraProveedor"].ToString())
                                {
                                    Articulo oArticulo = new Articulo();
                                    oArticulo.Pedido = dr2["Pedido"].ToString();
                                    oArticulo.Cantidad = (decimal)dr2["Cantidad"];
                                    oArticulo.ClaveArticulo = dr2["ClaveArticulo"].ToString(); ;
                                    oArticulo.Descripcion = dr2["DescripcionArticulo"].ToString();
                                    oArticulo.PrecioUnitario = (decimal)dr2["Precio"];
                                    oArticulo.Subtotal = (decimal)dr2["Subtotal"];
                                    oArticulo.IVA = (decimal)dr2["IVA"];
                                    oArticulo.Total = (decimal)dr2["Total"];
                                    oCotizacion.Articulos.Add(oArticulo);
                                }
                            }
                            oSimulador.Cotizaciones.Add(oCotizacion);
                        }
                    }
                }
                return oSimulador;
            }
            catch
            {
                return null;
            }
        }
        public void setAltaSimulador(int id)
        {
            int idSimuldadorCompra, idSimuladorCompraProveedor;
            String conStr = "";
            try
            {
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "usp_AltaSimuladorCompra";
                if (id != 0)
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@idSimulador", id));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Fecha", this.Fecha));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PresupuestoCobranza", this.PresupuestoCobranza));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Porcentaje", this.Porcentaje));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PresupuestoCobranzaFinal", this.PresupuestoCobranzaFinal));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Total", this.Total));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Diferencia", this.Diferencia));
                SqlParameter output = new SqlParameter("id", SqlDbType.Int, 500);
                output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output);

                cmd.Execute();
                idSimuldadorCompra = (int)output.Value;


                //RECORREMOS CADA COTIZACION Y DAMOS DE ALTA EL PROVEEDOR CON SUS ARTICULOS
                foreach (Cotizacion oCotizacion in this.Cotizaciones)
                {
                    cmd.Parameters.Clear();
                    cmd.ObjectName = "usp_AltaSimuladorCompraProveedor";
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ClaveSimuladorCompra", idSimuldadorCompra));
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ClaveProveedor", oCotizacion.ClaveProveedor));
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Proveedor", oCotizacion.NombreProveedor));
                    output = new SqlParameter("id", SqlDbType.Int, 500);
                    output.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(output);
                    cmd.Execute();
                    idSimuladorCompraProveedor = (int)output.Value;

                    foreach (Articulo oArticulo in oCotizacion.Articulos)
                    {
                        cmd.Parameters.Clear();
                        cmd.ObjectName = "usp_AltaSimuladorCompraProveedorArticulo";
                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ClaveSimuladorCompraProveedor", idSimuladorCompraProveedor));
                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Pedido", oArticulo.Pedido));
                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Cantidad", oArticulo.Cantidad));
                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ClaveArticulo", oArticulo.ClaveArticulo));
                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DescripcionArticulo", oArticulo.Descripcion));
                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UltimoCosto", oArticulo.UltimoCosto));
                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Precio", oArticulo.PrecioUnitario));
                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Subtotal", oArticulo.Subtotal));
                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IVA", oArticulo.IVA));
                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Total", oArticulo.Total));
                        cmd.Execute();
                    }
                }

                cmd.Connection.Close();

            }
            catch
            {

            }

        }
        public static DataTable getUltimoCosto(string Modelo)
        {
            DataTable dtComp = new DataTable();
            using (var dbContext = new AspelSae80Context())
            {

                var query = (from par in dbContext.PAR_COMPO01
                             join com in dbContext.COMPO01 on par.CVE_DOC equals com.CVE_DOC
                             where par.CVE_ART.Trim() == Modelo && com.STATUS == "E"
                             select par).OrderByDescending(x => x.CVE_DOC);

                dtComp = Linq2DataTable.CopyToDataTable(query);

            }
            return dtComp;
        }
        public static void GeneraArchivoExcel(string RutaYNombreArchivo, SimuladorCompra oSimuladorCompra)
        {
            HSSFWorkbook xlsWorkBook = new HSSFWorkbook();
            ISheet sheet = xlsWorkBook.CreateSheet("Hoja1");

            //formatos
            #region Formatos


            //formato de miles SIN decimales
            ICellStyle fmtoPesos = xlsWorkBook.CreateCellStyle();
            fmtoPesos.DataFormat = ExcelNpoiUtil.FormatoCelda(ref xlsWorkBook, Enumerados.FormatosNPOI.MONEDA);

            //formato para Texto Centrado
            ICellStyle fmtCentrado = xlsWorkBook.CreateCellStyle();
            fmtCentrado.Alignment = HorizontalAlignment.Center;

            //formato para texto en Negritas
            ICellStyle fmtNegritas = xlsWorkBook.CreateCellStyle();
            var font = xlsWorkBook.CreateFont();
            font.Boldweight = 18;
            font.FontHeightInPoints = (short)18;
            fmtNegritas.SetFont(font);

            #endregion

            #region Encabezado

            IRow rngEncabezado = sheet.CreateRow(0);
            ICell celEncabezado = rngEncabezado.CreateCell(0);
            celEncabezado.SetCellValue("Simulador de Compras");            
            celEncabezado.CellStyle = fmtCentrado;

            //se combinan las celdas

            CellRangeAddress range = new CellRangeAddress(0, 0, 0, 3);
            sheet.AddMergedRegion(range);

            ICell celFecha = rngEncabezado.CreateCell(5);
            celFecha.SetCellValue(oSimuladorCompra.Fecha.ToString("MMM-yyyy"));
            celFecha.CellStyle = fmtNegritas;

            #endregion

            #region Se escriben los rangos de fecha, cliente y DS CMP

            IRow rngEmitidoDel = sheet.CreateRow(1); //renglón: "Emitido del.."
            //rngEmitidoDel.CreateCell(1).SetCellValue(string.Format("Fecha de Simulación      {0}", oSimuladorCompra.Fecha.ToString("MM-yyyy")));

            #endregion
            #region Detalle de Cada proveedor

            int iRenglonDetalle = 3;
            foreach (Cotizacion oCotizacion in oSimuladorCompra.Cotizaciones)
            {

                //ENCABEZADOS PROVEEDOR
                IRow renglonEncabezados = sheet.CreateRow(iRenglonDetalle);

                ICell celdaEncClaveProveedor = renglonEncabezados.CreateCell(0);
                celdaEncClaveProveedor.SetCellValue("Proveedor: " + oCotizacion.ClaveProveedor + " - " + oCotizacion.NombreProveedor);


                iRenglonDetalle++;

                //ENCABEZADOS PARA DETALLE
                IRow renglonEncabezadoArticulos = sheet.CreateRow(iRenglonDetalle);

                ICell celdaEncCantidad = renglonEncabezadoArticulos.CreateCell(1);
                celdaEncCantidad.SetCellValue("CANTIDAD");
                ICell celdaEncClave = renglonEncabezadoArticulos.CreateCell(2);
                celdaEncClave.SetCellValue("CLAVE");
                ICell celdaEncArticulo = renglonEncabezadoArticulos.CreateCell(3);
                celdaEncArticulo.SetCellValue("ARTICULO");
                ICell celdaEncPrecio = renglonEncabezadoArticulos.CreateCell(4);
                celdaEncPrecio.SetCellValue("PRECIO U.");
                ICell celdaEncSubtotal = renglonEncabezadoArticulos.CreateCell(5);
                celdaEncSubtotal.SetCellValue("TOTAL");
                ICell celdaEncTotal = renglonEncabezadoArticulos.CreateCell(6);
                celdaEncTotal.SetCellValue("IVA");
                iRenglonDetalle++;

                foreach (Articulo oArticulo in oCotizacion.Articulos)
                {
                    IRow renglonDetalle = sheet.CreateRow(iRenglonDetalle);

                    ICell celdaDetalleCantidad = renglonDetalle.CreateCell(1);
                    celdaDetalleCantidad.SetCellValue(oArticulo.Cantidad.ToString());

                    ICell celdaDetalleClave = renglonDetalle.CreateCell(2);
                    celdaDetalleClave.SetCellValue(oArticulo.ClaveArticulo.ToString());

                    ICell celdaDetalleArticulo = renglonDetalle.CreateCell(3);
                    celdaDetalleArticulo.SetCellValue(oArticulo.Descripcion.ToString());

                    ICell celdaDetallePrecio = renglonDetalle.CreateCell(4);
                    //celdaDetallePrecio.SetCellValue((oArticulo.PrecioUnitario * (decimal)1.16).ToString("C"));
                    celdaDetallePrecio.SetCellValue(oArticulo.PrecioUnitario.ToString("C"));

                    ICell celdaDetalleSubtotal = renglonDetalle.CreateCell(5);
                    celdaDetalleSubtotal.SetCellValue((oArticulo.PrecioUnitario * oArticulo.Cantidad).ToString("C"));

                    ICell celdaDetalleTotal = renglonDetalle.CreateCell(6);
                    celdaDetalleTotal.SetCellValue(oArticulo.Total.ToString("C"));

                    iRenglonDetalle++;
                }
                IRow renglonTotalizado = sheet.CreateRow(iRenglonDetalle);

                ICell celdaTotalizado = renglonTotalizado.CreateCell(4);
                celdaTotalizado.SetCellValue("TOTAL");
                ICell celdaTotalizadoSubtotal = renglonTotalizado.CreateCell(5);
                celdaTotalizadoSubtotal.SetCellValue(oCotizacion.getSubTotal().ToString("C"));
                ICell celdaTotalizadoTotal = renglonTotalizado.CreateCell(6);
                celdaTotalizadoTotal.SetCellValue(oCotizacion.getTotal().ToString("C"));

                iRenglonDetalle++;
            }

            #endregion

            #region Presupuestos

            //PRESUPUESTO
            iRenglonDetalle++;
            IRow renglonPresupuesto = sheet.CreateRow(iRenglonDetalle);
            ICell celdaPresupuesto = renglonPresupuesto.CreateCell(0);
            celdaPresupuesto.SetCellValue("PRESUPUESTO DE COBRANZA");

            ICell celdaPresupuestoMonto = renglonPresupuesto.CreateCell(1);
            celdaPresupuestoMonto.SetCellValue(oSimuladorCompra.PresupuestoCobranza.ToString("C"));
            iRenglonDetalle++;
            //PORCENTAJE
            IRow renglonPorcentaje = sheet.CreateRow(iRenglonDetalle);
            ICell celdaPorcentaje = renglonPorcentaje.CreateCell(0);
            celdaPorcentaje.SetCellValue("PORCENTAJE %");
            ICell celdaPorcentajeMonto = renglonPorcentaje.CreateCell(1);
            celdaPorcentajeMonto.SetCellValue(oSimuladorCompra.Porcentaje.ToString());
            iRenglonDetalle++;
            //PRESUPUESTO
            IRow renglonPresupuestoFinal = sheet.CreateRow(iRenglonDetalle);
            ICell celdaPresupuestoFinal = renglonPresupuestoFinal.CreateCell(0);
            celdaPresupuestoFinal.SetCellValue("PRESUPUESTO");
            ICell celdaPresupuestoFinalMonto = renglonPresupuestoFinal.CreateCell(1);
            celdaPresupuestoFinalMonto.SetCellValue(oSimuladorCompra.PresupuestoCobranzaFinal.ToString("C"));
            iRenglonDetalle++;
            //TOTAL
            IRow renglonTotal = sheet.CreateRow(iRenglonDetalle);
            ICell celdaTotal = renglonTotal.CreateCell(0);
            celdaTotal.SetCellValue("COMPRA DEL MES");

            ICell celdaTotalMonto = renglonTotal.CreateCell(1);
            celdaTotalMonto.SetCellValue(oSimuladorCompra.Total.ToString("C"));

            ICell celdaTotalMonto2 = renglonTotal.CreateCell(6);
            celdaTotalMonto2.SetCellValue(oSimuladorCompra.Total.ToString("C"));

            iRenglonDetalle++;
            //TOTAL
            IRow renglonDiferencia = sheet.CreateRow(iRenglonDetalle);
            ICell celdaDiferencia = renglonDiferencia.CreateCell(0);
            celdaDiferencia.SetCellValue("DIFERENCIA");
            ICell celdaDiferenciaMonto = renglonDiferencia.CreateCell(1);
            celdaDiferenciaMonto.SetCellValue(oSimuladorCompra.Diferencia >= 0 ? oSimuladorCompra.Diferencia.ToString("C") : "(" + (oSimuladorCompra.Diferencia * -1).ToString("C") + ")");


            #endregion

            #region Ajuste de los anchos de columna
            sheet.SetColumnWidth(0, ExcelNpoiUtil.AnchoColumna(200));
            sheet.SetColumnWidth(1, ExcelNpoiUtil.AnchoColumna(80));
            sheet.SetColumnWidth(2, ExcelNpoiUtil.AnchoColumna(80));
            sheet.SetColumnWidth(3, ExcelNpoiUtil.AnchoColumna(300));
            sheet.SetColumnWidth(4, ExcelNpoiUtil.AnchoColumna(80));
            sheet.SetColumnWidth(5, ExcelNpoiUtil.AnchoColumna(80));
            sheet.SetColumnWidth(6, ExcelNpoiUtil.AnchoColumna(80));
            #endregion

            if (File.Exists(RutaYNombreArchivo))
            {
                File.Delete(RutaYNombreArchivo);
            }

            FileStream fs = new FileStream(RutaYNombreArchivo, FileMode.CreateNew);

            xlsWorkBook.Write(fs);


            fs.Close();
        }
        #endregion
        #region "Composicion"
        public class Cotizacion
        {
            public String ClaveProveedor { get; set; }
            public String NombreProveedor { get; set; }
            public List<Articulo> Articulos { get; set; }
            public Cotizacion()
            {
                this.Articulos = new List<Articulo> { };
            }
            public decimal getTotal()
            {
                decimal total = 0;
                foreach (Articulo oArticulo in this.Articulos)
                {
                    total += oArticulo.Total;
                }
                return total;
            }
            public decimal getSubTotal()
            {
                decimal total = 0;
                foreach (Articulo oArticulo in this.Articulos)
                {
                    total += oArticulo.Cantidad * oArticulo.PrecioUnitario;
                }
                return total;
            }

        }
        public class Articulo
        {
            public string Pedido { get; set; }
            public decimal Cantidad { get; set; }
            public String ClaveArticulo { get; set; }
            public String Descripcion { get; set; }
            public Decimal UltimoCosto { get; set; }
            public Decimal PrecioUnitario { get; set; }
            public Decimal Subtotal { get; set; }
            public Decimal IVA { get; set; }
            public Decimal Total { get; set; }
        }
        #endregion
    }
}