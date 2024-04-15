using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using sm_dl;
using sm_dl.SqlServer;
using ulp_bl.Utiles;
using ulp_dl.SIPNegocio;

namespace ulp_bl
{
    public class RepStaff
    {
        public static DataSet RegresaDatosReporteStaff(DateTime FechaDesde, DateTime FechaHasta, ref Exception Ex)
        {
            string conStr = "";
            DataSet dsRepStaff = new DataSet();
            try
            {
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }

                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "usp_RepStaff";
                cmd.Parameters.Add(new SqlParameter("@fecha_desde", FechaDesde));
                cmd.Parameters.Add(new SqlParameter("@fecha_hasta", FechaHasta));

                dsRepStaff = cmd.GetDataSet();



                cmd.Connection.Close();
                return dsRepStaff;
            }
            catch (Exception Exc)
            {
                return null;
                Ex = Exc;
            }

        }
        public static DataSet RegresaDatosReporteStaffAdministracion(DateTime FechaDesde, DateTime FechaHasta, ref Exception Ex)
        {
            string conStr = "";
            DataSet dsRepStaff = new DataSet();
            try
            {
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }

                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "usp_RepStaffAdministracion";
                cmd.Parameters.Add(new SqlParameter("@fecha_desde", FechaDesde));
                cmd.Parameters.Add(new SqlParameter("@fecha_hasta", FechaHasta));

                dsRepStaff = cmd.GetDataSet();



                cmd.Connection.Close();
                return dsRepStaff;
            }
            catch (Exception Exc)
            {
                return null;
                Ex = Exc;
            }

        }

        public static void GeneraArchivoExcel(string RutaYNombreArchivo, DataSet dsReporteStaff, DateTime FechaDesde, DateTime FechaHasta)
        {

            int fila = 0;

            DataTable dtFacturasYNC = dsReporteStaff.Tables[0];
            DataTable dtPedidosExist = dsReporteStaff.Tables[1];
            DataTable dtExist = dsReporteStaff.Tables[2];
            DataTable dtCartera = dsReporteStaff.Tables[3];
            DataTable dtRecepcionTalleres = dsReporteStaff.Tables[4];

            HSSFWorkbook xlsWorkBook = new HSSFWorkbook();
            ISheet sheet = xlsWorkBook.CreateSheet("Hoja1");

            //formatos
            #region Formatos
            //formato de moneda
            ICellStyle fmtoMoneda = xlsWorkBook.CreateCellStyle();
            fmtoMoneda.DataFormat = ExcelNpoiUtil.FormatoCelda(ref xlsWorkBook, Enumerados.FormatosNPOI.MONEDA);

            //formato de miles SIN decimales
            ICellStyle fmtoMiles = xlsWorkBook.CreateCellStyle();
            fmtoMiles.DataFormat = ExcelNpoiUtil.FormatoCelda(ref xlsWorkBook, "#,##0");

            //formato de miles CON decimales
            ICellStyle fmtoMilesDec = xlsWorkBook.CreateCellStyle();
            fmtoMilesDec.DataFormat = ExcelNpoiUtil.FormatoCelda(ref xlsWorkBook, "_-* #,##0.00");

            //formato para Texto Centrado
            ICellStyle fmtCentrado = xlsWorkBook.CreateCellStyle();
            fmtCentrado.Alignment = HorizontalAlignment.Center;
            #endregion

            //encabezado
            #region Encabezado

            IRow rngEncabezado = sheet.CreateRow(0);
            ICell celEncabezado = rngEncabezado.CreateCell(0);
            celEncabezado.SetCellValue("Reporte de Staff");
            celEncabezado.CellStyle = fmtCentrado;


            ExcelNpoiUtil.AsignaValorCelda(ref sheet, 0, 0, fmtCentrado, "Reporte de Staff");



            //se combinan las celdas

            CellRangeAddress range = new CellRangeAddress(0, 0, 0, 3);
            sheet.AddMergedRegion(range);


            #endregion

            #region Se escriben los rangos de fecha

            IRow rngEmitidoDel = sheet.CreateRow(1); //renglón: "Emitido del.."
            rngEmitidoDel.CreateCell(3).SetCellValue(string.Format("Emitido del      {0}", FechaDesde.ToShortDateString()));

            IRow rngEmitidoAl = sheet.CreateRow(2); //renglón: "Al.."
            rngEmitidoAl.CreateCell(3).SetCellValue(string.Format("Al                     {0}", FechaHasta.ToShortDateString()));

            #endregion

            #region Sección correspondientes a Facturas y Notas de Crédito

            fila = 3;

            //encabezados
            IRow rngTitulos = sheet.CreateRow(fila);
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, null, "Facturas");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 2, null, "Notas de Crédito");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 3, null, "Descuentos");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 4, null, "TOTAL");
            fila++;
            //SUBTOTAL
            IRow rngSubTotal = sheet.CreateRow(fila);
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "SUBTOTAL");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMoneda, Convert.ToDouble(dtFacturasYNC.Rows[0]["factura_subtotal"]));
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 2, fmtoMoneda, Convert.ToDouble(dtFacturasYNC.Rows[0]["nc_subtotal"]));
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 3, fmtoMoneda, Convert.ToDouble(dtFacturasYNC.Rows[0]["devolUcion_subtotal"]));
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 4, fmtoMoneda, Convert.ToDouble(dtFacturasYNC.Rows[0]["subtotal_total"]));
            fila++;
            //IVA
            IRow rngIva = sheet.CreateRow(fila);
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "IVA");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMoneda, Convert.ToDouble(dtFacturasYNC.Rows[0]["factura_iva"]));
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 2, fmtoMoneda, Convert.ToDouble(dtFacturasYNC.Rows[0]["nc_iva"]));
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 3, fmtoMoneda, Convert.ToDouble(dtFacturasYNC.Rows[0]["devolucion_iva"]));
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 4, fmtoMoneda, Convert.ToDouble(dtFacturasYNC.Rows[0]["subtotal_iva"]));
            fila++;
            //TOTAL
            IRow rngTotal = sheet.CreateRow(fila);
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "TOTAL");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMoneda, Convert.ToDouble(dtFacturasYNC.Rows[0]["factura_total"]));
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 2, fmtoMoneda, Convert.ToDouble(dtFacturasYNC.Rows[0]["nc_total"]));
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 3, fmtoMoneda, Convert.ToDouble(dtFacturasYNC.Rows[0]["devolucion_total"]));
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 4, fmtoMoneda, Convert.ToDouble(dtFacturasYNC.Rows[0]["subtotal_total_total"]));
            fila++;

            //PRENDAS
            IRow rngPrendas = sheet.CreateRow(fila);
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "PRENDAS");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtFacturasYNC.Rows[0]["factura_prendas"]));
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 2, fmtoMiles, Convert.ToDouble(dtFacturasYNC.Rows[0]["nc_prendas"]));
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 4, fmtoMoneda, Convert.ToDouble(dtFacturasYNC.Rows[0]["prendas_total"]));
            fila++;

            //PROMEDIO PRENDAS
            IRow rngPromPrendas = sheet.CreateRow(fila);
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 2, null, "Promedio por prenda");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 4, fmtoMilesDec, Convert.ToDouble(dtFacturasYNC.Rows[0]["promedio_x_prenda"]));
            fila += 2;
            #endregion

            #region Sección Pedidos y Existencias
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Pedidos capturados en el periodo");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtPedidosExist.Rows[0]["pedidos_cap_periodo"]));
            fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Prendas capturadas en el periodo");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtPedidosExist.Rows[0]["prendas_cap_periodo"]));
            fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Pedidos ingresados con fecha de elaboracion");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtPedidosExist.Rows[0]["pedidos_ing_periodo"]));
            fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Prendas ingresasdas con fecha de elaboracion");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtPedidosExist.Rows[0]["prendas_ing_periodo"]));
            fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Promedio de prendas por pedido en el periodo");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMilesDec, Convert.ToDouble(dtPedidosExist.Rows[0]["promedio_prendas_cap_periodo"]));
            fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Pedidos en proceso");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtPedidosExist.Rows[0]["pedidos_en_proceso"]));
            fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Prendas en proceso");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtPedidosExist.Rows[0]["prendas_en_proceso"]));
            fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Promedio de prendas por pedido en proceso");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMilesDec, Convert.ToDouble(dtPedidosExist.Rows[0]["promedio_prendas_x_proceso"]));
            fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Liberados");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtPedidosExist.Rows[0]["liberados"]));
            fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "No Liberados");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtPedidosExist.Rows[0]["no_liberados"]));
            fila += 2;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Clientes atendidos en año natural");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtPedidosExist.Rows[0]["clientes_atendidos_año_nat"]));
            fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Clientes atendidos en el periodo");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtPedidosExist.Rows[0]["clientes_atendidos_en_periodo"]));
            fila += 2;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Existencia Alm General(1)");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtExist.Rows[0]["existencia_alm_Gral_1"]));
            fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Existencia Alm Surtido(3)");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtExist.Rows[0]["existencia_alm_Surtido_3"]));
            fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Existencia Alm Mostrador(4)");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtExist.Rows[0]["existencia_alm_Mostrador_4"]));
            //fórmula
            ExcelNpoiUtil.AsignaFormulaCelda(ref sheet, fila, 2, fmtoMiles, "B25+B26+B27");
            fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Existencia Alm Pedidos Parciales(40)");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtExist.Rows[0]["existencia_alm_Pedidos_Par_40"]));
            fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Existencia Alm Facturación(5)");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtExist.Rows[0]["existencia_alm_Fact_Par_5"]));
            fila++;

            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Existencia Alm DAT(6)");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtExist.Rows[0]["existencia_alm_Dat_6"]));
            fila++;

            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Existencia Alm Consignado(7) owens");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtExist.Rows[0]["existencia_alm_Consig_7"]));
            fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Existencia Alm Consignado(8) metalsa");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtExist.Rows[0]["existencia_alm_Consig_8"]));
            fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Existencia Alm Especiales(32)");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtExist.Rows[0]["existencia_alm_Esp_32"]));
            fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Existencia Alm Hospital(35)");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtExist.Rows[0]["existencia_alm_Hospital_35"]));
            //Fórmula
            ExcelNpoiUtil.AsignaFormulaCelda(ref sheet, fila, 2, fmtoMiles, "B25+B26+B27+B28+B29+B30+B31+B32+B33+B34");
            fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Existencia Prod Proc");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtExist.Rows[0]["existencia_prod_Proc"]));
            //Fórmula
            ExcelNpoiUtil.AsignaFormulaCelda(ref sheet, fila, 2, fmtoMiles, "C34+B35");
            fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Existencia MP(2)");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtExist.Rows[0]["existencia_mp_2"]));
            fila += 2;



            #endregion

            #region Recepción Talleres
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Recepcion de produccion de los talleres");
            if (dtRecepcionTalleres.Rows.Count > 0)
            {
                fila++;
                foreach (DataRow rngRT in dtRecepcionTalleres.Rows)
                {

                    string descr = string.Format("        {0} {1}", rngRT["CLAVE_CLPV"], rngRT["NOMBRE"]);
                    double total = Convert.ToDouble(rngRT["TOTAL"]);
                    ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, descr);
                    ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 3, fmtoMiles, total);
                    fila++;
                }
                ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 2, null, "Suma TOTAL");
                ExcelNpoiUtil.AsignaFormulaCelda(ref sheet, fila, 3, fmtoMiles, string.Format("SUM(D34:D{0})", fila));

            }
            else
            {
                fila++;
                ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "*** No hay recepción en este periodo ***");
            }
            fila += 2; ;
            #endregion


            #region Sección Cartera
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Valor de Cartera");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMilesDec, Convert.ToDouble(dtCartera.Rows[0]["valor_cartera_total"]));
            fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Relación entre Venta y Cartera");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMilesDec, Convert.ToDouble(dtCartera.Rows[0]["relacion_venta_cartera"]));
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 2, null, "En dias");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 3, fmtoMilesDec, Convert.ToDouble(dtCartera.Rows[0]["relacion_venta_cartera_dias"]));
            fila += 2;
            #endregion

            #region Notas
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "NOTAS:"); fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Las PRENDAS capturadas por periodo no consideran las PRENDAS de Mostrador"); fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Para que los datos de relacion entre VENTA y cartera sean coherentes,"); fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "el rango a emitir debe ser de un mes"); fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Pedidos capturados en el periodo: Pedidos ingresados en el periodo sin importar la fecha de elaboración del documento."); fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Pedidos ingresados con fecha de elaboración: Es lo que se hace actualmente."); fila++;
            #endregion



            #region Anchos de columna

            sheet.SetColumnWidth(0, ExcelNpoiUtil.AnchoColumna(299));
            sheet.SetColumnWidth(1, ExcelNpoiUtil.AnchoColumna(106));
            sheet.SetColumnWidth(2, ExcelNpoiUtil.AnchoColumna(140));
            sheet.SetColumnWidth(3, ExcelNpoiUtil.AnchoColumna(163));
            sheet.SetColumnWidth(4, ExcelNpoiUtil.AnchoColumna(163));
            #endregion



            #region SE ESCRIBE EL ARCHIVO
            if (File.Exists(RutaYNombreArchivo))
            {
                File.Delete(RutaYNombreArchivo);
            }
            FileStream fs = new FileStream(RutaYNombreArchivo, FileMode.CreateNew);

            xlsWorkBook.Write(fs);


            fs.Close();
            #endregion
        }
        public static void GeneraArchivoExcelAdministracion(string RutaYNombreArchivo, DataSet dsReporteStaff, DateTime FechaDesde, DateTime FechaHasta)
        {

            int fila = 0;

            DataTable dtFacturasYNC = dsReporteStaff.Tables[0];
            DataTable dtPedidosExist = dsReporteStaff.Tables[1];
            DataTable dtExist = dsReporteStaff.Tables[2];
            DataTable dtCartera = dsReporteStaff.Tables[3];
            DataTable dtRecepcionTalleres = dsReporteStaff.Tables[4];

            HSSFWorkbook xlsWorkBook = new HSSFWorkbook();
            ISheet sheet = xlsWorkBook.CreateSheet("Hoja1");

            //formatos
            #region Formatos
            //formato de moneda
            ICellStyle fmtoMoneda = xlsWorkBook.CreateCellStyle();
            fmtoMoneda.DataFormat = ExcelNpoiUtil.FormatoCelda(ref xlsWorkBook, Enumerados.FormatosNPOI.MONEDA);

            //formato de miles SIN decimales
            ICellStyle fmtoMiles = xlsWorkBook.CreateCellStyle();
            fmtoMiles.DataFormat = ExcelNpoiUtil.FormatoCelda(ref xlsWorkBook, "#,##0");

            //formato de miles CON decimales
            ICellStyle fmtoMilesDec = xlsWorkBook.CreateCellStyle();
            fmtoMilesDec.DataFormat = ExcelNpoiUtil.FormatoCelda(ref xlsWorkBook, "_-* #,##0.00");

            //formato para Texto Centrado
            ICellStyle fmtCentrado = xlsWorkBook.CreateCellStyle();
            fmtCentrado.Alignment = HorizontalAlignment.Center;
            #endregion

            //encabezado
            #region Encabezado

            IRow rngEncabezado = sheet.CreateRow(0);
            ICell celEncabezado = rngEncabezado.CreateCell(0);
            celEncabezado.SetCellValue("Reporte de Staff");
            celEncabezado.CellStyle = fmtCentrado;


            ExcelNpoiUtil.AsignaValorCelda(ref sheet, 0, 0, fmtCentrado, "Reporte de Staff");



            //se combinan las celdas

            CellRangeAddress range = new CellRangeAddress(0, 0, 0, 3);
            sheet.AddMergedRegion(range);


            #endregion

            #region Se escriben los rangos de fecha

            IRow rngEmitidoDel = sheet.CreateRow(1); //renglón: "Emitido del.."
            rngEmitidoDel.CreateCell(3).SetCellValue(string.Format("Emitido del      {0}", FechaDesde.ToShortDateString()));

            IRow rngEmitidoAl = sheet.CreateRow(2); //renglón: "Al.."
            rngEmitidoAl.CreateCell(3).SetCellValue(string.Format("Al                     {0}", FechaHasta.ToShortDateString()));

            #endregion

            #region Sección correspondientes a Facturas, Notas de Crédito y Cancelaciones

            fila = 3;

            //encabezados
            IRow rngTitulos = sheet.CreateRow(fila);
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, null, "Facturas");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 2, null, "Notas de Crédito");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 3, null, "Descuentos");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 4, null, "Cancelación SAT");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 5, null, "TOTAL");
            fila++;
            //SUBTOTAL
            IRow rngSubTotal = sheet.CreateRow(fila);
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "SUBTOTAL");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMoneda, Convert.ToDouble(dtFacturasYNC.Rows[0]["factura_subtotal"]));
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 2, fmtoMoneda, Convert.ToDouble(dtFacturasYNC.Rows[0]["nc_subtotal"]));
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 3, fmtoMoneda, Convert.ToDouble(dtFacturasYNC.Rows[0]["devolucion_subtotal"]));
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 4, fmtoMoneda, Convert.ToDouble(dtFacturasYNC.Rows[0]["cancelacionSAT_subtotal"]));
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 5, fmtoMoneda, Convert.ToDouble(dtFacturasYNC.Rows[0]["subtotal_total"]));
            fila++;
            //IVA
            IRow rngIva = sheet.CreateRow(fila);
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "IVA");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMoneda, Convert.ToDouble(dtFacturasYNC.Rows[0]["factura_iva"]));
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 2, fmtoMoneda, Convert.ToDouble(dtFacturasYNC.Rows[0]["nc_iva"]));
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 3, fmtoMoneda, Convert.ToDouble(dtFacturasYNC.Rows[0]["devolucion_iva"]));
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 4, fmtoMoneda, Convert.ToDouble(dtFacturasYNC.Rows[0]["cancelacionSAT_iva"]));
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 5, fmtoMoneda, Convert.ToDouble(dtFacturasYNC.Rows[0]["subtotal_iva"]));
            fila++;
            //TOTAL
            IRow rngTotal = sheet.CreateRow(fila);
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "TOTAL");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMoneda, Convert.ToDouble(dtFacturasYNC.Rows[0]["factura_total"]));
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 2, fmtoMoneda, Convert.ToDouble(dtFacturasYNC.Rows[0]["nc_total"]));
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 3, fmtoMoneda, Convert.ToDouble(dtFacturasYNC.Rows[0]["devolucion_total"]));
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 4, fmtoMoneda, Convert.ToDouble(dtFacturasYNC.Rows[0]["cancelacionSAT_total"]));
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 5, fmtoMoneda, Convert.ToDouble(dtFacturasYNC.Rows[0]["subtotal_total_total"]));
            fila++;

            //PRENDAS
            IRow rngPrendas = sheet.CreateRow(fila);
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "PRENDAS");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtFacturasYNC.Rows[0]["factura_prendas"]));
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 2, fmtoMiles, Convert.ToDouble(dtFacturasYNC.Rows[0]["nc_prendas"]));
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 4, fmtoMiles, Convert.ToDouble(dtFacturasYNC.Rows[0]["cancelacionSAT_prendas"]));
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 5, fmtoMoneda, Convert.ToDouble(dtFacturasYNC.Rows[0]["prendas_total"]));
            fila++;

            //PROMEDIO PRENDAS
            IRow rngPromPrendas = sheet.CreateRow(fila);
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 2, null, "Promedio por prenda");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 4, fmtoMilesDec, Convert.ToDouble(dtFacturasYNC.Rows[0]["promedio_x_prenda"]));
            fila += 2;
            #endregion

            #region Sección Pedidos y Existencias
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Pedidos capturados en el periodo");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtPedidosExist.Rows[0]["pedidos_cap_periodo"]));
            fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Prendas capturadas en el periodo");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtPedidosExist.Rows[0]["prendas_cap_periodo"]));
            fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Pedidos ingresados con fecha de elaboracion");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtPedidosExist.Rows[0]["pedidos_ing_periodo"]));
            fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Prendas ingresasdas con fecha de elaboracion");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtPedidosExist.Rows[0]["prendas_ing_periodo"]));
            fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Promedio de prendas por pedido en el periodo");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMilesDec, Convert.ToDouble(dtPedidosExist.Rows[0]["promedio_prendas_cap_periodo"]));
            fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Pedidos en proceso");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtPedidosExist.Rows[0]["pedidos_en_proceso"]));
            fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Prendas en proceso");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtPedidosExist.Rows[0]["prendas_en_proceso"]));
            fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Promedio de prendas por pedido en proceso");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMilesDec, Convert.ToDouble(dtPedidosExist.Rows[0]["promedio_prendas_x_proceso"]));
            fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Liberados");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtPedidosExist.Rows[0]["liberados"]));
            fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "No Liberados");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtPedidosExist.Rows[0]["no_liberados"]));
            fila += 2;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Clientes atendidos en año natural");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtPedidosExist.Rows[0]["clientes_atendidos_año_nat"]));
            fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Clientes atendidos en el periodo");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtPedidosExist.Rows[0]["clientes_atendidos_en_periodo"]));
            fila += 2;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Existencia Alm General(1)");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtExist.Rows[0]["existencia_alm_Gral_1"]));
            fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Existencia Alm Surtido(3)");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtExist.Rows[0]["existencia_alm_Surtido_3"]));
            fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Existencia Alm Mostrador(4)");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtExist.Rows[0]["existencia_alm_Mostrador_4"]));
            //fórmula
            ExcelNpoiUtil.AsignaFormulaCelda(ref sheet, fila, 2, fmtoMiles, "B25+B26+B27");
            fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Existencia Alm Pedidos Parciales(40)");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtExist.Rows[0]["existencia_alm_Pedidos_Par_40"]));
            fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Existencia Alm Facturación(5)");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtExist.Rows[0]["existencia_alm_Fact_Par_5"]));
            fila++;

            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Existencia Alm DAT(6)");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtExist.Rows[0]["existencia_alm_Dat_6"]));
            fila++;

            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Existencia Alm Consignado(7) owens");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtExist.Rows[0]["existencia_alm_Consig_7"]));
            fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Existencia Alm Consignado(8) metalsa");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtExist.Rows[0]["existencia_alm_Consig_8"]));
            fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Existencia Alm Especiales(32)");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtExist.Rows[0]["existencia_alm_Esp_32"]));
            fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Existencia Alm Hospital(35)");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtExist.Rows[0]["existencia_alm_Hospital_35"]));
            //Fórmula
            ExcelNpoiUtil.AsignaFormulaCelda(ref sheet, fila, 2, fmtoMiles, "B25+B26+B27+B28+B29+B30+B31+B32+B33+B34");
            fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Existencia Prod Proc");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtExist.Rows[0]["existencia_prod_Proc"]));
            //Fórmula
            ExcelNpoiUtil.AsignaFormulaCelda(ref sheet, fila, 2, fmtoMiles, "C34+B35");
            fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Existencia MP(2)");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMiles, Convert.ToDouble(dtExist.Rows[0]["existencia_mp_2"]));
            fila += 2;



            #endregion

            #region Recepción Talleres
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Recepcion de produccion de los talleres");
            if (dtRecepcionTalleres.Rows.Count > 0)
            {
                fila++;
                foreach (DataRow rngRT in dtRecepcionTalleres.Rows)
                {

                    string descr = string.Format("        {0} {1}", rngRT["CLAVE_CLPV"], rngRT["NOMBRE"]);
                    double total = Convert.ToDouble(rngRT["TOTAL"]);
                    ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, descr);
                    ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 3, fmtoMiles, total);
                    fila++;
                }
                ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 2, null, "Suma TOTAL");
                ExcelNpoiUtil.AsignaFormulaCelda(ref sheet, fila, 3, fmtoMiles, string.Format("SUM(D34:D{0})", fila));

            }
            else
            {
                fila++;
                ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "*** No hay recepción en este periodo ***");
            }
            fila += 2; ;
            #endregion


            #region Sección Cartera
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Valor de Cartera");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMilesDec, Convert.ToDouble(dtCartera.Rows[0]["valor_cartera_total"]));
            fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Relación entre Venta y Cartera");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 1, fmtoMilesDec, Convert.ToDouble(dtCartera.Rows[0]["relacion_venta_cartera"]));
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 2, null, "En dias");
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 3, fmtoMilesDec, Convert.ToDouble(dtCartera.Rows[0]["relacion_venta_cartera_dias"]));
            fila += 2;
            #endregion

            #region Notas
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "NOTAS:"); fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Las PRENDAS capturadas por periodo no consideran las PRENDAS de Mostrador"); fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Para que los datos de relacion entre VENTA y cartera sean coherentes,"); fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "el rango a emitir debe ser de un mes"); fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Pedidos capturados en el periodo: Pedidos ingresados en el periodo sin importar la fecha de elaboración del documento."); fila++;
            ExcelNpoiUtil.AsignaValorCelda(ref sheet, fila, 0, null, "Pedidos ingresados con fecha de elaboración: Es lo que se hace actualmente."); fila++;
            #endregion



            #region Anchos de columna

            sheet.SetColumnWidth(0, ExcelNpoiUtil.AnchoColumna(299));
            sheet.SetColumnWidth(1, ExcelNpoiUtil.AnchoColumna(106));
            sheet.SetColumnWidth(2, ExcelNpoiUtil.AnchoColumna(140));
            sheet.SetColumnWidth(3, ExcelNpoiUtil.AnchoColumna(163));
            sheet.SetColumnWidth(4, ExcelNpoiUtil.AnchoColumna(163));
            sheet.SetColumnWidth(5, ExcelNpoiUtil.AnchoColumna(163));
            #endregion



            #region SE ESCRIBE EL ARCHIVO
            if (File.Exists(RutaYNombreArchivo))
            {
                File.Delete(RutaYNombreArchivo);
            }
            FileStream fs = new FileStream(RutaYNombreArchivo, FileMode.CreateNew);

            xlsWorkBook.Write(fs);


            fs.Close();
            #endregion
        }

    }
}
