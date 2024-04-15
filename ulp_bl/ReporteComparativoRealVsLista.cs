using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sm_dl;
using sm_dl.SqlServer;
using ulp_bl.Utiles;
using ulp_dl.SIPNegocio;
using System.Data;
using System.Data.SqlClient;
using ulp_dl;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.IO;

namespace ulp_bl
{
    public class ReporteComparativoRealVsLista
    {
        public static DataTable GetReporteComparativo(String ClaveUsuario, DateTime FechaInicio, DateTime FechaFin,  ref Exception Ex)
        {
            String conStr = "";
            DataTable dtResultado = new DataTable();
            try
            {
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "usp_ReporteComparativo_RealVsLista";
                cmd.Parameters.Add(new SqlParameter("@Usuario", ClaveUsuario));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                dtResultado = cmd.GetDataTable();
                cmd.Connection.Close();
                Ex = null;
                return dtResultado;
            }
            catch (Exception ex)
            {
                Ex = ex;
                return null;
            }
        }
        public static void GeneraArchivoExcel(string RutaYNombreArchivo, DataTable dtPedidosDSyCMP, DateTime FechaDesde, DateTime FechaHasta)
        {

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

            #region Encabezado

            IRow rngEncabezado = sheet.CreateRow(0);
            ICell celEncabezado = rngEncabezado.CreateCell(0);
            celEncabezado.SetCellValue("Reporte comparativo de Ventas (Precio Lista VS Precio Facturado.");
            celEncabezado.CellStyle = fmtCentrado;

            //se combinan las celdas

            CellRangeAddress range = new CellRangeAddress(0, 0, 0, 3);
            sheet.AddMergedRegion(range);


            #endregion

            #region Se escriben los rangos de fecha

            IRow rngEmitidoDel = sheet.CreateRow(1); //renglón: "Emitido del.."
            rngEmitidoDel.CreateCell(1).SetCellValue(string.Format("Emitido del      {0}", FechaDesde.ToShortDateString()));

            IRow rngEmitidoAl = sheet.CreateRow(2); //renglón: "Al.."
            rngEmitidoAl.CreateCell(1).SetCellValue(string.Format("Al                   {0}", FechaHasta.ToShortDateString()));

            #endregion

            #region Encabezados
            //ENCABEZADOS DE DETALLE
            IRow renglonEncabezados = sheet.CreateRow(4);

            ICell celdaEcnClaveVendedor = renglonEncabezados.CreateCell(0);
            celdaEcnClaveVendedor.SetCellValue("C. VEND");

            ICell celdaEncVendedor = renglonEncabezados.CreateCell(1);
            celdaEncVendedor.SetCellValue("VENDEDOR");

            ICell celdaEncClasificacion = renglonEncabezados.CreateCell(2);
            celdaEncClasificacion.SetCellValue("CLASIFICACION");

            ICell celdaEncTotalCobrado= renglonEncabezados.CreateCell(3);
            celdaEncTotalCobrado.SetCellValue("TOTAL FACTURADO");

            ICell celdaEncTotalLista = renglonEncabezados.CreateCell(4);
            celdaEncTotalLista.SetCellValue("TOTAL LISTA");

            ICell celdaEncDescuento = renglonEncabezados.CreateCell(5);
            celdaEncDescuento.SetCellValue("% DESCUENTO");

            ICell celdaEncPromedioDescuento = renglonEncabezados.CreateCell(6);
            celdaEncPromedioDescuento.SetCellValue("% PROMEDIO DESCUENTO");

            #endregion

            #region Detalle

            int iRenglonDetalle = 5;
            foreach (DataRow _dr in dtPedidosDSyCMP.Rows)
            {
                //MOSTRAMOS LOS DETALLES

                IRow renglonDetalle = sheet.CreateRow(iRenglonDetalle);

                ICell celdaDetalleClaveVendedor = renglonDetalle.CreateCell(0);
                celdaDetalleClaveVendedor.SetCellValue(_dr["CVE_VEND"].ToString());

                ICell celdaDetalleVendedor = renglonDetalle.CreateCell(1);
                celdaDetalleVendedor.SetCellValue(_dr["NOMBRE"].ToString());

                ICell celdaDetalleClasificacion = renglonDetalle.CreateCell(2);
                celdaDetalleClasificacion.SetCellValue(_dr["CLASIFICACION"].ToString());

                ICell celdaDetalleTotalCobrado = renglonDetalle.CreateCell(3);
                celdaDetalleTotalCobrado.SetCellValue(decimal.Parse(_dr["TOTAL COBRADO"].ToString()).ToString());
                celdaDetalleTotalCobrado.CellStyle = fmtoMoneda;

                ICell celdaDetalleTotalLista = renglonDetalle.CreateCell(4);
                celdaDetalleTotalLista.SetCellValue(decimal.Parse(_dr["TOTAL LISTA"].ToString()).ToString());
                celdaDetalleTotalLista.CellStyle = fmtoMoneda;

                ICell celdaDetalleTotalDescuento = renglonDetalle.CreateCell(5);
                celdaDetalleTotalDescuento.SetCellValue(decimal.Parse(_dr["% DESCUENTO"].ToString()).ToString());
                celdaDetalleTotalDescuento.CellStyle = fmtoMilesDec;

                ICell celdaDetalleTotalPromedioDescuento = renglonDetalle.CreateCell(6);
                celdaDetalleTotalPromedioDescuento.SetCellValue(decimal.Parse(_dr["% PROMEDIO DESCUENTO"].ToString()).ToString());
                celdaDetalleTotalPromedioDescuento.CellStyle = fmtoMilesDec;

                iRenglonDetalle++;
            }

            sheet.SetColumnWidth(0, ExcelNpoiUtil.AnchoColumna(60));
            sheet.SetColumnWidth(1, ExcelNpoiUtil.AnchoColumna(300));
            sheet.SetColumnWidth(2, ExcelNpoiUtil.AnchoColumna(100));
            sheet.SetColumnWidth(3, ExcelNpoiUtil.AnchoColumna(100));
            sheet.SetColumnWidth(4, ExcelNpoiUtil.AnchoColumna(100));
            sheet.SetColumnWidth(5, ExcelNpoiUtil.AnchoColumna(100));
            sheet.SetColumnWidth(6, ExcelNpoiUtil.AnchoColumna(100));

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
