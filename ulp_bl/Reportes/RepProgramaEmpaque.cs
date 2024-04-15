using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ulp_dl.SIPReportes;
using sm_dl.SqlServer;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.IO;
using System.Data.SqlClient;
using ulp_bl.Utiles;

namespace ulp_bl.Reportes
{
    public class RepProgramaEmpaque
    {
        public DataTable RegresaReporte(DateTime Fecha)
        {
            System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
            DataTable dataTable = new DataTable();
            using (var DbContext = new SIPReportesContext())
            {
                SqlServerCommand _cmd = new SqlServerCommand();
                _cmd.Connection = sm_dl.DALUtil.GetConnection(DbContext.Database.Connection.ConnectionString);
                _cmd.ObjectName = "usp_RepProgramaEmpaque";
                _cmd.Parameters.Add(new SqlParameter("@fecha", Fecha));
                dataTable = _cmd.GetDataTable();
                _cmd.Connection.Close();

            }
            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.ElapsedMilliseconds);
            return dataTable;
        }
        public static void GeneraArchivoExcel(string RutaYNombreArchivo, DataTable TablaReporte, DateTime Fecha)
        {
            HSSFWorkbook xlsWorkBook = new HSSFWorkbook();
            ISheet sheet = xlsWorkBook.CreateSheet("Hoja1");

            //variables de control
            String fechaAgrupadora = "";
            Boolean primeraIteracion = true;

            #region ESTILOS

            //WHITE FONT

            IFont whiteFont = xlsWorkBook.CreateFont();
            whiteFont.Color = IndexedColors.White.Index;

            //TITLE FONT
            IFont titleFont = xlsWorkBook.CreateFont();
            titleFont.FontHeightInPoints = 24;

            IFont totalFont = xlsWorkBook.CreateFont();
            totalFont.FontHeightInPoints = 10;
            totalFont.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;

            //ALL BORDERED
            HSSFCellStyle borderedCellStyle = (HSSFCellStyle)xlsWorkBook.CreateCellStyle();
            borderedCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            borderedCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            borderedCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            borderedCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;

            //BOTTOM BORDERED
            HSSFCellStyle bottomBorderedCellStyle = (HSSFCellStyle)xlsWorkBook.CreateCellStyle();
            bottomBorderedCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium;


            //BLUE BACKGROUND BORDERED
            HSSFCellStyle blueBackGroundCellStyle = (HSSFCellStyle)xlsWorkBook.CreateCellStyle();
            blueBackGroundCellStyle.FillForegroundColor = IndexedColors.DarkBlue.Index;
            blueBackGroundCellStyle.SetFont(whiteFont);
            blueBackGroundCellStyle.FillPattern = FillPattern.SolidForeground;

            blueBackGroundCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium;
            blueBackGroundCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium;
            blueBackGroundCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium;
            blueBackGroundCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium;

            //TITLE 
            HSSFCellStyle titleCellStyle = (HSSFCellStyle)xlsWorkBook.CreateCellStyle();
            titleCellStyle.SetFont(titleFont);
            titleCellStyle.Alignment = HorizontalAlignment.Center;

            HSSFCellStyle totalCellStyle = (HSSFCellStyle)xlsWorkBook.CreateCellStyle();
            totalCellStyle.SetFont(totalFont);
            totalCellStyle.Alignment = HorizontalAlignment.Right;
            #endregion

            #region ENCABEZADO DEL REPORTE

            //TITULO DE EMPACADORES

            IRow renglonEmpacadores = sheet.CreateRow(0);
            ICell celdaEmpacadores = renglonEmpacadores.CreateCell(1);
            celdaEmpacadores.SetCellValue("Empacadores");
            CellRangeAddress range = new CellRangeAddress(0, 0, 1, 2);
            sheet.AddMergedRegion(range);
            celdaEmpacadores.CellStyle = blueBackGroundCellStyle;


            ICell celdaTotalEmpacadores = renglonEmpacadores.CreateCell(3);
            celdaTotalEmpacadores.SetCellValue("");
            celdaTotalEmpacadores.CellStyle = borderedCellStyle;

            ICell celdaFaltas = renglonEmpacadores.CreateCell(5);
            celdaFaltas.SetCellValue("FALTAS");

            ICell celdaTotalFaltas = renglonEmpacadores.CreateCell(6);
            celdaTotalFaltas.SetCellValue("");
            celdaTotalFaltas.CellStyle = bottomBorderedCellStyle;
            range = new CellRangeAddress(0, 0, 6, 8);
            sheet.AddMergedRegion(range);


            //TITULO DE CAPACIDAD DIARIA
            IRow renglonCapacidadDiaria = sheet.CreateRow(1);
            ICell celdaCapacidadDiaria = renglonCapacidadDiaria.CreateCell(1);
            celdaCapacidadDiaria.SetCellValue("Capacidad Diaria");
            range = new CellRangeAddress(1, 1, 1, 2);
            sheet.AddMergedRegion(range);
            celdaCapacidadDiaria.CellStyle = blueBackGroundCellStyle;

            ICell celdaTotalCapacidad = renglonCapacidadDiaria.CreateCell(3);
            celdaTotalCapacidad.SetCellValue("");
            celdaTotalCapacidad.CellStyle = borderedCellStyle;

            ICell celdaTotalFaltas2 = renglonCapacidadDiaria.CreateCell(6);
            celdaTotalFaltas2.SetCellValue("");
            range = new CellRangeAddress(1, 1, 6, 8);
            sheet.AddMergedRegion(range);
            celdaTotalFaltas2.CellStyle = bottomBorderedCellStyle;

            //TITULO DE 
            IRow renglonCapacidad85 = sheet.CreateRow(2);
            ICell celdaCapacidad85 = renglonCapacidad85.CreateCell(1);
            celdaCapacidad85.SetCellValue("Capacidad 85%");
            range = new CellRangeAddress(2, 2, 1, 2);
            sheet.AddMergedRegion(range);
            celdaCapacidad85.CellStyle = blueBackGroundCellStyle;

            ICell celdaTotalCapacidad85 = renglonCapacidad85.CreateCell(3);
            celdaTotalCapacidad85.SetCellValue("");
            celdaTotalCapacidad85.CellStyle = borderedCellStyle;

            ICell celdaTotalFaltas3 = renglonCapacidad85.CreateCell(6);
            celdaTotalFaltas3.SetCellValue("");
            range = new CellRangeAddress(1, 1, 6, 8);
            sheet.AddMergedRegion(range);
            celdaTotalFaltas3.CellStyle = bottomBorderedCellStyle;

            //TITULO DEL REPORTE
            IRow renglonTitulo = sheet.CreateRow(3);

            ICell celdaTitulo = renglonTitulo.CreateCell(0);
            celdaTitulo.SetCellValue("PROGRAMA DE EMPAQUE");
            range = new CellRangeAddress(3, 4, 0, 6);
            sheet.AddMergedRegion(range);
            celdaTitulo.CellStyle = titleCellStyle;

            ICell celdaTituloFecha = renglonTitulo.CreateCell(7);
            celdaTituloFecha.SetCellValue(Fecha.Day.ToString() + "-" + Fecha.ToString("MMM", System.Globalization.CultureInfo.CreateSpecificCulture("es")));
            range = new CellRangeAddress(3, 4, 7, 9);
            sheet.AddMergedRegion(range);
            celdaTituloFecha.CellStyle = titleCellStyle;




            #endregion

            #region ENCABEZADO DE DETALLE
            IRow renglonEncabezadoDetalle = sheet.CreateRow(5);

            ICell celdaProcesos = renglonEncabezadoDetalle.CreateCell(0); celdaProcesos.SetCellValue("");
            ICell celdaProcesosOK = renglonEncabezadoDetalle.CreateCell(1); celdaProcesosOK.SetCellValue("");
            ICell celdaFechaVencimiento = renglonEncabezadoDetalle.CreateCell(2); celdaFechaVencimiento.SetCellValue("F. VENC.");
            ICell celdaPedido = renglonEncabezadoDetalle.CreateCell(3); celdaPedido.SetCellValue("PEDIDO");
            ICell celdaFechaPedido = renglonEncabezadoDetalle.CreateCell(4); celdaFechaPedido.SetCellValue("F. PEDIDO");
            ICell celdaCliente = renglonEncabezadoDetalle.CreateCell(5); celdaCliente.SetCellValue("CTE.");
            ICell celdaNombre = renglonEncabezadoDetalle.CreateCell(6); celdaNombre.SetCellValue("NOMBRE");
            ICell celdaAgente = renglonEncabezadoDetalle.CreateCell(7); celdaAgente.SetCellValue("AGT.");
            ICell celdaPrendas = renglonEncabezadoDetalle.CreateCell(8); celdaPrendas.SetCellValue("PREN.");
            ICell celdaDiferencia = renglonEncabezadoDetalle.CreateCell(9); celdaDiferencia.SetCellValue("DIF.");
            ICell celdaAux1 = renglonEncabezadoDetalle.CreateCell(10); celdaAux1.SetCellValue("");
            ICell celdaAux2 = renglonEncabezadoDetalle.CreateCell(11); celdaAux2.SetCellValue("");
            ICell celdaAux3 = renglonEncabezadoDetalle.CreateCell(12); celdaAux3.SetCellValue("");

            #endregion

            #region DETALLE VENCIDOS

            var linqVencidos = from vencidos in TablaReporte.AsEnumerable() where vencidos.Field<DateTime>("F_VENCIMIENTO") < Fecha select vencidos;
            DataTable tablaVencidos = linqVencidos.CopyToDataTable();

            int rowAux = 6;
            //GENERAMOS DETALLE
            foreach (DataRow dr in tablaVencidos.Rows)
            {
                IRow renglonDetalleVencidos = sheet.CreateRow(rowAux);

                ICell celdaDetalleProcesos = renglonDetalleVencidos.CreateCell(0); celdaDetalleProcesos.SetCellValue(dr["PROCESOS"].ToString()); celdaDetalleProcesos.CellStyle = borderedCellStyle;
                ICell celdaDetalleProcesosOK = renglonDetalleVencidos.CreateCell(1); celdaDetalleProcesosOK.SetCellValue(dr["ESTATUS"].ToString()); celdaDetalleProcesosOK.CellStyle = borderedCellStyle;
                ICell celdaDetalleFechaVencimiento = renglonDetalleVencidos.CreateCell(2); celdaDetalleFechaVencimiento.SetCellValue(DateTime.Parse(dr["F_VENCIMIENTO"].ToString()).ToString("dd/MM/yyyy"));
                ICell celdaDetallePedido = renglonDetalleVencidos.CreateCell(3); celdaDetallePedido.SetCellValue(dr["PEDIDO"].ToString());
                ICell celdaDetallePedidoFecha = renglonDetalleVencidos.CreateCell(4); celdaDetallePedidoFecha.SetCellValue(DateTime.Parse(dr["FECHA_PEDIDO"].ToString()).ToString("dd/MM/yyyy"));
                ICell celdaDetalleCliente = renglonDetalleVencidos.CreateCell(5); celdaDetalleCliente.SetCellValue(dr["CLAVE_CLIENTE"].ToString());
                ICell celdaDetalleNombre = renglonDetalleVencidos.CreateCell(6); celdaDetalleNombre.SetCellValue(dr["NOMBRE_CLIENTE"].ToString());
                ICell celdaDetalleAgente = renglonDetalleVencidos.CreateCell(7); celdaDetalleAgente.SetCellValue(dr["VENDEDOR"].ToString());
                ICell celdaDetallePrendas = renglonDetalleVencidos.CreateCell(8); celdaDetallePrendas.SetCellValue(int.Parse(dr["PRENDAS"].ToString()));
                ICell celdaDetalleDiferencia = renglonDetalleVencidos.CreateCell(9); celdaDetalleDiferencia.SetCellValue(int.Parse(dr["DIFERENCIA"].ToString()));
                ICell celdaDetalleAux1 = renglonDetalleVencidos.CreateCell(10); celdaDetalleAux1.SetCellValue(""); celdaDetalleAux1.CellStyle = borderedCellStyle;
                ICell celdaDetalleAux2 = renglonDetalleVencidos.CreateCell(11); celdaDetalleAux2.SetCellValue(""); celdaDetalleAux2.CellStyle = borderedCellStyle;
                ICell celdaDetalleAux3 = renglonDetalleVencidos.CreateCell(12); celdaDetalleAux3.SetCellValue(""); celdaDetalleAux3.CellStyle = borderedCellStyle;
                rowAux++;
            }
            //GENERAMOS TOTALES UNICAMENTE TEXTOS VACIOS
            //FACTURA POR ADELANTADO
            IRow renglonTotalesVencidosFacturaPorAdelantado = sheet.CreateRow(rowAux);
            ICell celdaDetalleTotalFacturaPorAdelantado = renglonTotalesVencidosFacturaPorAdelantado.CreateCell(0);
            celdaDetalleTotalFacturaPorAdelantado.SetCellValue("FAC POR ADELA");
            range = new CellRangeAddress(rowAux, rowAux, 0, 7);
            sheet.AddMergedRegion(range);
            celdaDetalleTotalFacturaPorAdelantado.CellStyle = totalCellStyle;
            ICell celdaDetalleTotalFacturaPorAdelantadoTotal = renglonTotalesVencidosFacturaPorAdelantado.CreateCell(8); celdaDetalleTotalFacturaPorAdelantadoTotal.SetCellValue(0); celdaDetalleTotalFacturaPorAdelantadoTotal.CellStyle = totalCellStyle;
            ICell celdaDetalleTotalFacturaPorAdelantadoAux1 = renglonTotalesVencidosFacturaPorAdelantado.CreateCell(9); celdaDetalleTotalFacturaPorAdelantadoAux1.SetCellValue("");
            ICell celdaDetalleTotalFacturaPorAdelantadoAux2 = renglonTotalesVencidosFacturaPorAdelantado.CreateCell(10); celdaDetalleTotalFacturaPorAdelantadoAux2.SetCellValue("T.P.");
            ICell celdaDetalleTotalFacturaPorAdelantadoAux3 = renglonTotalesVencidosFacturaPorAdelantado.CreateCell(11); celdaDetalleTotalFacturaPorAdelantadoAux3.SetCellValue(0);
            ICell celdaDetalleTotalFacturaPorAdelantadoAux4 = renglonTotalesVencidosFacturaPorAdelantado.CreateCell(12); celdaDetalleTotalFacturaPorAdelantadoAux4.SetCellValue("");
            rowAux++;

            //DAT
            IRow renglonTotalesVencidosDAT = sheet.CreateRow(rowAux);
            ICell celdaDetalleTotalDAT = renglonTotalesVencidosDAT.CreateCell(0);
            celdaDetalleTotalDAT.SetCellValue("TOTAL DAT");
            range = new CellRangeAddress(rowAux, rowAux, 0, 7);
            sheet.AddMergedRegion(range);
            celdaDetalleTotalDAT.CellStyle = totalCellStyle;
            ICell celdaDetalleTotalDATTotal = renglonTotalesVencidosDAT.CreateCell(8); celdaDetalleTotalDATTotal.SetCellValue(0); celdaDetalleTotalDATTotal.CellStyle = totalCellStyle;
            ICell celdaDetalleTotalDATAux1 = renglonTotalesVencidosDAT.CreateCell(9); celdaDetalleTotalDATAux1.SetCellFormula(string.Format("I{0} + I{1} + I{2}", rowAux, rowAux + 1, rowAux + 2)); celdaDetalleTotalDATAux1.CellStyle = totalCellStyle;
            ICell celdaDetalleTotalDATAux2 = renglonTotalesVencidosDAT.CreateCell(10); celdaDetalleTotalDATAux2.SetCellValue("P. EN RUTA");
            ICell celdaDetalleTotalDATAux3 = renglonTotalesVencidosDAT.CreateCell(11); celdaDetalleTotalDATAux3.SetCellFormula(string.Format("L{0} + L{1}", rowAux, rowAux + 2));
            ICell celdaDetalleTotalDATAux4 = renglonTotalesVencidosDAT.CreateCell(12); celdaDetalleTotalDATAux4.SetCellFormula(string.Format("L{0}/L{1}*100", rowAux + 1, rowAux));
            rowAux++;
            //POR FACTURAR
            IRow renglonTotalesVencidosPorFacturar = sheet.CreateRow(rowAux);
            ICell celdaDetalleTotalPorFacturar = renglonTotalesVencidosPorFacturar.CreateCell(0);
            celdaDetalleTotalPorFacturar.SetCellValue("TOTAL POR FACT");
            range = new CellRangeAddress(rowAux, rowAux, 0, 7);
            sheet.AddMergedRegion(range);
            celdaDetalleTotalPorFacturar.CellStyle = totalCellStyle;
            ICell celdaDetalleTotalPorFacturarTotal = renglonTotalesVencidosPorFacturar.CreateCell(8); celdaDetalleTotalPorFacturarTotal.SetCellValue(0); celdaDetalleTotalPorFacturarTotal.CellStyle = totalCellStyle;
            ICell celdaDetalleTotalPorFacturarAux1 = renglonTotalesVencidosPorFacturar.CreateCell(9); celdaDetalleTotalPorFacturarAux1.SetCellValue("");
            ICell celdaDetalleTotalPorFacturarAux2 = renglonTotalesVencidosPorFacturar.CreateCell(10); celdaDetalleTotalPorFacturarAux2.SetCellValue("P. DETENIDOS");
            ICell celdaDetalleTotalPorFacturarAux3 = renglonTotalesVencidosPorFacturar.CreateCell(11); celdaDetalleTotalPorFacturarAux3.SetCellValue(0);
            ICell celdaDetalleTotalPorFacturarAux4 = renglonTotalesVencidosPorFacturar.CreateCell(12); celdaDetalleTotalPorFacturarAux4.SetCellValue("");
            rowAux++;
            #endregion

            #region DETALLE NO VENCIDOS

            var linqNoVencidos = from vencidos in TablaReporte.AsEnumerable() where vencidos.Field<DateTime>("F_VENCIMIENTO") >= Fecha select vencidos;
            DataTable tablaNoVencidos = linqNoVencidos.CopyToDataTable();
            foreach (DataRow dr in tablaNoVencidos.Rows)
            {
                if (!primeraIteracion && fechaAgrupadora != dr["F_VENCIMIENTO"].ToString())
                {
                    //GENERAMOS TOTALES UNICAMENTE TEXTOS VACIOS
                    //FACTURA POR ADELANTADO
                    renglonTotalesVencidosFacturaPorAdelantado = sheet.CreateRow(rowAux);
                    celdaDetalleTotalFacturaPorAdelantado = renglonTotalesVencidosFacturaPorAdelantado.CreateCell(0);
                    celdaDetalleTotalFacturaPorAdelantado.SetCellValue("FAC POR ADELA");
                    range = new CellRangeAddress(rowAux, rowAux, 0, 7);
                    sheet.AddMergedRegion(range);
                    celdaDetalleTotalFacturaPorAdelantado.CellStyle = totalCellStyle;
                    celdaDetalleTotalFacturaPorAdelantadoTotal = renglonTotalesVencidosFacturaPorAdelantado.CreateCell(8); celdaDetalleTotalFacturaPorAdelantadoTotal.SetCellValue(0); celdaDetalleTotalFacturaPorAdelantadoTotal.CellStyle = totalCellStyle;
                    celdaDetalleTotalFacturaPorAdelantadoAux1 = renglonTotalesVencidosFacturaPorAdelantado.CreateCell(9); celdaDetalleTotalFacturaPorAdelantadoAux1.SetCellValue("");
                    celdaDetalleTotalFacturaPorAdelantadoAux2 = renglonTotalesVencidosFacturaPorAdelantado.CreateCell(10); celdaDetalleTotalFacturaPorAdelantadoAux2.SetCellValue("T.P.");
                    celdaDetalleTotalFacturaPorAdelantadoAux3 = renglonTotalesVencidosFacturaPorAdelantado.CreateCell(11); celdaDetalleTotalFacturaPorAdelantadoAux3.SetCellValue(0);
                    celdaDetalleTotalFacturaPorAdelantadoAux4 = renglonTotalesVencidosFacturaPorAdelantado.CreateCell(12); celdaDetalleTotalFacturaPorAdelantadoAux4.SetCellValue("");
                    rowAux++;
                    //DAT
                    renglonTotalesVencidosDAT = sheet.CreateRow(rowAux);
                    celdaDetalleTotalDAT = renglonTotalesVencidosDAT.CreateCell(0);
                    celdaDetalleTotalDAT.SetCellValue("TOTAL DAT");
                    range = new CellRangeAddress(rowAux, rowAux, 0, 7);
                    sheet.AddMergedRegion(range);
                    celdaDetalleTotalDAT.CellStyle = totalCellStyle;
                    celdaDetalleTotalDATTotal = renglonTotalesVencidosDAT.CreateCell(8); celdaDetalleTotalDATTotal.SetCellValue(0); celdaDetalleTotalDATTotal.CellStyle = totalCellStyle;
                    celdaDetalleTotalDATAux1 = renglonTotalesVencidosDAT.CreateCell(9); celdaDetalleTotalDATAux1.SetCellFormula(string.Format("I{0} + I{1} + I{2}", rowAux, rowAux + 1, rowAux + 2)); celdaDetalleTotalDATAux1.CellStyle = totalCellStyle;
                    celdaDetalleTotalDATAux2 = renglonTotalesVencidosDAT.CreateCell(10); celdaDetalleTotalDATAux2.SetCellValue("P. EN RUTA");
                    celdaDetalleTotalDATAux3 = renglonTotalesVencidosDAT.CreateCell(11); celdaDetalleTotalDATAux3.SetCellFormula(string.Format("L{0} + L{1}", rowAux, rowAux + 2));
                    celdaDetalleTotalDATAux4 = renglonTotalesVencidosDAT.CreateCell(12); celdaDetalleTotalDATAux4.SetCellFormula(string.Format("L{0}/L{1}*100", rowAux + 1, rowAux));
                    rowAux++;
                    //POR FACTURAR
                    renglonTotalesVencidosPorFacturar = sheet.CreateRow(rowAux);
                    celdaDetalleTotalPorFacturar = renglonTotalesVencidosPorFacturar.CreateCell(0);
                    celdaDetalleTotalPorFacturar.SetCellValue("TOTAL POR FACT");
                    range = new CellRangeAddress(rowAux, rowAux, 0, 7);
                    sheet.AddMergedRegion(range);
                    celdaDetalleTotalPorFacturar.CellStyle = totalCellStyle;
                    celdaDetalleTotalPorFacturarTotal = renglonTotalesVencidosPorFacturar.CreateCell(8); celdaDetalleTotalPorFacturarTotal.SetCellValue(0); celdaDetalleTotalPorFacturarTotal.CellStyle = totalCellStyle;
                    celdaDetalleTotalPorFacturarAux1 = renglonTotalesVencidosPorFacturar.CreateCell(9); celdaDetalleTotalPorFacturarAux1.SetCellValue("");
                    celdaDetalleTotalPorFacturarAux2 = renglonTotalesVencidosPorFacturar.CreateCell(10); celdaDetalleTotalPorFacturarAux2.SetCellValue("P. DETENIDOS");
                    celdaDetalleTotalPorFacturarAux3 = renglonTotalesVencidosPorFacturar.CreateCell(11); celdaDetalleTotalPorFacturarAux3.SetCellValue(0);
                    celdaDetalleTotalPorFacturarAux4 = renglonTotalesVencidosPorFacturar.CreateCell(12); celdaDetalleTotalPorFacturarAux4.SetCellValue("");
                    rowAux++;
                    fechaAgrupadora = dr["F_VENCIMIENTO"].ToString();
                    primeraIteracion = false;
                    rowAux++;
                }
                //DETALLE POR FILA
                fechaAgrupadora = dr["F_VENCIMIENTO"].ToString();
                primeraIteracion = false;
                IRow renglonDetalleVencidos = sheet.CreateRow(rowAux);
                ICell celdaDetalleProcesos = renglonDetalleVencidos.CreateCell(0); celdaDetalleProcesos.SetCellValue(dr["PROCESOS"].ToString()); celdaDetalleProcesos.CellStyle = borderedCellStyle;
                ICell celdaDetalleProcesosOK = renglonDetalleVencidos.CreateCell(1); celdaDetalleProcesosOK.SetCellValue(dr["ESTATUS"].ToString()); celdaDetalleProcesosOK.CellStyle = borderedCellStyle;
                ICell celdaDetalleFechaVencimiento = renglonDetalleVencidos.CreateCell(2); celdaDetalleFechaVencimiento.SetCellValue(DateTime.Parse(dr["F_VENCIMIENTO"].ToString()) >= new DateTime(2999, 1, 1) ? "" : DateTime.Parse(dr["F_VENCIMIENTO"].ToString()).ToString("dd/MM/yyyy"));
                ICell celdaDetallePedido = renglonDetalleVencidos.CreateCell(3); celdaDetallePedido.SetCellValue(dr["PEDIDO"].ToString());
                ICell celdaDetallePedidoFecha = renglonDetalleVencidos.CreateCell(4); celdaDetallePedidoFecha.SetCellValue(DateTime.Parse(dr["FECHA_PEDIDO"].ToString()).ToString("dd/MM/yyyy"));
                ICell celdaDetalleCliente = renglonDetalleVencidos.CreateCell(5); celdaDetalleCliente.SetCellValue(dr["CLAVE_CLIENTE"].ToString());
                ICell celdaDetalleNombre = renglonDetalleVencidos.CreateCell(6); celdaDetalleNombre.SetCellValue(dr["NOMBRE_CLIENTE"].ToString());
                ICell celdaDetalleAgente = renglonDetalleVencidos.CreateCell(7); celdaDetalleAgente.SetCellValue(dr["VENDEDOR"].ToString());
                ICell celdaDetallePrendas = renglonDetalleVencidos.CreateCell(8); celdaDetallePrendas.SetCellValue(int.Parse(dr["PRENDAS"].ToString()));
                ICell celdaDetalleDiferencia = renglonDetalleVencidos.CreateCell(9); celdaDetalleDiferencia.SetCellValue(int.Parse(dr["DIFERENCIA"].ToString()));
                ICell celdaDetalleAux1 = renglonDetalleVencidos.CreateCell(10); celdaDetalleAux1.SetCellValue(""); celdaDetalleAux1.CellStyle = borderedCellStyle;
                ICell celdaDetalleAux2 = renglonDetalleVencidos.CreateCell(11); celdaDetalleAux2.SetCellValue(""); celdaDetalleAux2.CellStyle = borderedCellStyle;
                ICell celdaDetalleAux3 = renglonDetalleVencidos.CreateCell(12); celdaDetalleAux3.SetCellValue(""); celdaDetalleAux3.CellStyle = borderedCellStyle;
                rowAux++;
            }

            //TOTALES FINALES
            //FACTURA POR ADELANTADO
            renglonTotalesVencidosFacturaPorAdelantado = sheet.CreateRow(rowAux);
            celdaDetalleTotalFacturaPorAdelantado = renglonTotalesVencidosFacturaPorAdelantado.CreateCell(0);
            celdaDetalleTotalFacturaPorAdelantado.SetCellValue("FAC POR ADELA");
            range = new CellRangeAddress(rowAux, rowAux, 0, 7);
            sheet.AddMergedRegion(range);
            celdaDetalleTotalFacturaPorAdelantado.CellStyle = totalCellStyle;
            celdaDetalleTotalFacturaPorAdelantadoTotal = renglonTotalesVencidosFacturaPorAdelantado.CreateCell(8); celdaDetalleTotalFacturaPorAdelantadoTotal.SetCellValue(0); celdaDetalleTotalFacturaPorAdelantadoTotal.CellStyle = totalCellStyle;
            celdaDetalleTotalFacturaPorAdelantadoAux1 = renglonTotalesVencidosFacturaPorAdelantado.CreateCell(9); celdaDetalleTotalFacturaPorAdelantadoAux1.SetCellValue("");
            celdaDetalleTotalFacturaPorAdelantadoAux2 = renglonTotalesVencidosFacturaPorAdelantado.CreateCell(10); celdaDetalleTotalFacturaPorAdelantadoAux2.SetCellValue("T.P.");
            celdaDetalleTotalFacturaPorAdelantadoAux3 = renglonTotalesVencidosFacturaPorAdelantado.CreateCell(11); celdaDetalleTotalFacturaPorAdelantadoAux3.SetCellValue(0);
            celdaDetalleTotalFacturaPorAdelantadoAux4 = renglonTotalesVencidosFacturaPorAdelantado.CreateCell(12); celdaDetalleTotalFacturaPorAdelantadoAux4.SetCellValue("");
            rowAux++;
            //DAT
            renglonTotalesVencidosDAT = sheet.CreateRow(rowAux);
            celdaDetalleTotalDAT = renglonTotalesVencidosDAT.CreateCell(0);
            celdaDetalleTotalDAT.SetCellValue("TOTAL DAT");
            range = new CellRangeAddress(rowAux, rowAux, 0, 7);
            sheet.AddMergedRegion(range);
            celdaDetalleTotalDAT.CellStyle = totalCellStyle;
            celdaDetalleTotalDATTotal = renglonTotalesVencidosDAT.CreateCell(8); celdaDetalleTotalDATTotal.SetCellValue(0); celdaDetalleTotalDATTotal.CellStyle = totalCellStyle;
            celdaDetalleTotalDATAux1 = renglonTotalesVencidosDAT.CreateCell(9); celdaDetalleTotalDATAux1.SetCellFormula(string.Format("I{0} + I{1} + I{2}", rowAux, rowAux + 1, rowAux + 2)); celdaDetalleTotalDATAux1.CellStyle = totalCellStyle;
            celdaDetalleTotalDATAux2 = renglonTotalesVencidosDAT.CreateCell(10); celdaDetalleTotalDATAux2.SetCellValue("P. EN RUTA");
            celdaDetalleTotalDATAux3 = renglonTotalesVencidosDAT.CreateCell(11); celdaDetalleTotalDATAux3.SetCellFormula(string.Format("L{0} + L{1}", rowAux, rowAux + 2));
            celdaDetalleTotalDATAux4 = renglonTotalesVencidosDAT.CreateCell(12); celdaDetalleTotalDATAux4.SetCellFormula(string.Format("L{0}/L{1}*100", rowAux + 1, rowAux));
            rowAux++;
            //POR FACTURAR
            renglonTotalesVencidosPorFacturar = sheet.CreateRow(rowAux);
            celdaDetalleTotalPorFacturar = renglonTotalesVencidosPorFacturar.CreateCell(0);
            celdaDetalleTotalPorFacturar.SetCellValue("TOTAL POR FACT");
            range = new CellRangeAddress(rowAux, rowAux, 0, 7);
            sheet.AddMergedRegion(range);
            celdaDetalleTotalPorFacturar.CellStyle = totalCellStyle;
            celdaDetalleTotalPorFacturarTotal = renglonTotalesVencidosPorFacturar.CreateCell(8); celdaDetalleTotalPorFacturarTotal.SetCellValue(0); celdaDetalleTotalPorFacturarTotal.CellStyle = totalCellStyle;
            celdaDetalleTotalPorFacturarAux1 = renglonTotalesVencidosPorFacturar.CreateCell(9); celdaDetalleTotalPorFacturarAux1.SetCellValue("");
            celdaDetalleTotalPorFacturarAux2 = renglonTotalesVencidosPorFacturar.CreateCell(10); celdaDetalleTotalPorFacturarAux2.SetCellValue("P. DETENIDOS");
            celdaDetalleTotalPorFacturarAux3 = renglonTotalesVencidosPorFacturar.CreateCell(11); celdaDetalleTotalPorFacturarAux3.SetCellValue(0);
            celdaDetalleTotalPorFacturarAux4 = renglonTotalesVencidosPorFacturar.CreateCell(12); celdaDetalleTotalPorFacturarAux4.SetCellValue("");
            rowAux++;

            #endregion

            #region TAMAÑO DE COLUMNAS

            for (int i = 0; i < 9; i++)
            {
                sheet.AutoSizeColumn(i);
            }

            sheet.SetColumnWidth(10, ExcelNpoiUtil.AnchoColumna(100));
            sheet.SetColumnWidth(11, ExcelNpoiUtil.AnchoColumna(100));
            sheet.SetColumnWidth(12, ExcelNpoiUtil.AnchoColumna(100));

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
