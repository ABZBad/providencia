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
    public class ReporteMovimientosMP
    {
        public static DataTable ConsultaCatalogoConceptos()
        {
            String conStr = "";
            DataTable dtCatalogo = new DataTable();
            try
            {
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "usp_ConsultaCatalogoConceptosMINVE";
                dtCatalogo = cmd.GetDataTable();
                cmd.Connection.Close();
                return dtCatalogo;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static DataTable ConsultaReporteMovimientosMP(DateTime FechaInicio, DateTime FechaFin, String xmlConceptos)
        {
            String conStr = "";
            DataTable dtReporteMovimientosMP = new DataTable();
            try
            {
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "[dbo].[usp_ReporteMovimientosMP]";
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
                cmd.Parameters.Add(new SqlParameter("@xmlConceptos", xmlConceptos));

                dtReporteMovimientosMP = cmd.GetDataTable();
                cmd.Connection.Close();
                return dtReporteMovimientosMP;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static string GetReporteExcel(String RutaYNombreArchivo, DataTable dtReporte, DateTime FechaInicio, DateTime FechaFin)
        {
            HSSFWorkbook libro = new HSSFWorkbook();
            ISheet hoja = libro.CreateSheet("Hoja1");
            //String rutaYNombre = String.Format("{0}\\{1}_{2}.{3}", this.PATH, this.N_NOMINA, this.F_NOMINA.ToString("yyyyMMdd"), "xls");
            String rutaYNombre = RutaYNombreArchivo;

            //fmto. negrillas:

            ICellStyle estiloNegritas = libro.CreateCellStyle();
            IFont fuenteNegritas = libro.CreateFont();
            fuenteNegritas.Boldweight = (short)FontBoldWeight.Bold;
            estiloNegritas.SetFont(fuenteNegritas);

            //fmto numérico con separador de miles
            IDataFormat dataFormat2Decimales = libro.CreateDataFormat();
            ICellStyle cellStyle2Decimales = libro.CreateCellStyle();
            cellStyle2Decimales.DataFormat = dataFormat2Decimales.GetFormat("###,##0.00");

            //fmto numerico con separador de miles y titulo
            IDataFormat dataFormat2DecimalesNegritas = libro.CreateDataFormat();
            ICellStyle cellStyle2DecimalesNegritas = libro.CreateCellStyle();
            cellStyle2DecimalesNegritas.DataFormat = dataFormat2DecimalesNegritas.GetFormat("###,##0.00");
            cellStyle2DecimalesNegritas.SetFont(fuenteNegritas);

            //titulo
            IRow renglonTitulo = hoja.CreateRow(0);
            ICell celdaTitulo = renglonTitulo.CreateCell(0);
            celdaTitulo.CellStyle = estiloNegritas;
            celdaTitulo.SetCellValue(string.Format("RESUMEN DE MOVIMIENTOS MP DEL {0} AL {1}", FechaInicio.ToString("dd/MM/yyyy"), FechaFin.ToString("dd/MM/yyyy")));
            celdaTitulo.CellStyle.Alignment = HorizontalAlignment.Center;
            hoja.AddMergedRegion(new CellRangeAddress(0, 0, 0, 4));

            var ListaArticulos = (from DataRow dRow in dtReporte.Rows select dRow["CVE_ART"]).Distinct().ToList();

            int iArticulo = 2;
            double totalGeneralCantidad = 0, totalGeneralCosto = 0, totalGeneralPrecio = 0;
            // POR CADA CLAVE GENERAMOS ENCABEZADO
            foreach (String _clave in ListaArticulos)
            {
                int col = 0;
                double totalCantidad = 0, totalCostoTotal = 0, totalPrecioTotal = 0;
                //CREAMOS ENCABEZADOS DE ARTICULO
                DataRow[] detalle = dtReporte.Select("CVE_ART = '" + _clave + "'");
                IRow renglonEncabezadoArticulo = hoja.CreateRow(iArticulo);
                renglonEncabezadoArticulo.CreateCell(0).SetCellValue("Producto");
                renglonEncabezadoArticulo.CreateCell(1).SetCellValue(_clave);
                renglonEncabezadoArticulo.CreateCell(2).SetCellValue("Descripción");
                renglonEncabezadoArticulo.CreateCell(3).SetCellValue(detalle[0]["ART"].ToString());
                renglonEncabezadoArticulo.CreateCell(4).SetCellValue("Almacén");
                renglonEncabezadoArticulo.CreateCell(5).SetCellValue(detalle[0]["ALMACEN"].ToString());
                renglonEncabezadoArticulo.Cells[0].CellStyle = estiloNegritas;
                renglonEncabezadoArticulo.Cells[1].CellStyle = estiloNegritas;
                renglonEncabezadoArticulo.Cells[2].CellStyle = estiloNegritas;
                renglonEncabezadoArticulo.Cells[3].CellStyle = estiloNegritas;
                renglonEncabezadoArticulo.Cells[4].CellStyle = estiloNegritas;
                renglonEncabezadoArticulo.Cells[5].CellStyle = estiloNegritas;

                //CREAMOS ENCABEZADO PARA DETALLE
                iArticulo++;
                IRow renglonEncabezadoDetalle = hoja.CreateRow(iArticulo);
                renglonEncabezadoDetalle.CreateCell(col).SetCellValue("Fecha"); col++;
                renglonEncabezadoDetalle.CreateCell(col).SetCellValue("Docto."); col++;
                renglonEncabezadoDetalle.CreateCell(col).SetCellValue("Folio"); col++;
                renglonEncabezadoDetalle.CreateCell(col).SetCellValue("Movimiento"); col++;
                renglonEncabezadoDetalle.CreateCell(col).SetCellValue("CI/Pv"); col++;
                renglonEncabezadoDetalle.CreateCell(col).SetCellValue("Cantidad"); col++;
                renglonEncabezadoDetalle.CreateCell(col).SetCellValue("Costo unitario"); col++;
                renglonEncabezadoDetalle.CreateCell(col).SetCellValue("Costo total"); col++;
                renglonEncabezadoDetalle.CreateCell(col).SetCellValue("Precio unitario"); col++;
                renglonEncabezadoDetalle.CreateCell(col).SetCellValue("Precio total"); col++;
                col = 0;
                renglonEncabezadoDetalle.Cells[col].CellStyle = estiloNegritas; col++;
                renglonEncabezadoDetalle.Cells[col].CellStyle = estiloNegritas; col++;
                renglonEncabezadoDetalle.Cells[col].CellStyle = estiloNegritas; col++;
                renglonEncabezadoDetalle.Cells[col].CellStyle = estiloNegritas; col++;
                renglonEncabezadoDetalle.Cells[col].CellStyle = estiloNegritas; col++;
                renglonEncabezadoDetalle.Cells[col].CellStyle = estiloNegritas; col++;
                renglonEncabezadoDetalle.Cells[col].CellStyle = estiloNegritas; col++;
                renglonEncabezadoDetalle.Cells[col].CellStyle = estiloNegritas; col++;
                renglonEncabezadoDetalle.Cells[col].CellStyle = estiloNegritas; col++;
                renglonEncabezadoDetalle.Cells[col].CellStyle = estiloNegritas; col++;
                iArticulo++;
                foreach (DataRow _dr in detalle)
                {
                    col = 0;
                    IRow renglonDetalle = hoja.CreateRow(iArticulo);
                    renglonDetalle.CreateCell(col).SetCellValue(DateTime.Parse(_dr["FECHA_DOCU"].ToString()).ToString("dd/MM/yyyy")); col++;
                    renglonDetalle.CreateCell(col).SetCellValue(_dr["REFER"].ToString()); col++;
                    renglonDetalle.CreateCell(col).SetCellValue(_dr["CVE_FOLIO"].ToString()); col++;
                    renglonDetalle.CreateCell(col).SetCellValue(_dr["DESCR"].ToString()); col++;
                    renglonDetalle.CreateCell(col).SetCellValue(_dr["CLPV"].ToString()); col++;
                    renglonDetalle.CreateCell(col).SetCellValue(double.Parse(_dr["CANTIDAD"].ToString()));
                    renglonDetalle.Cells[col].CellStyle = cellStyle2Decimales; col++;
                    renglonDetalle.CreateCell(col).SetCellValue(double.Parse(_dr["COSTO"].ToString()));
                    renglonDetalle.Cells[col].CellStyle = cellStyle2Decimales; col++;
                    renglonDetalle.CreateCell(col).SetCellValue(double.Parse(_dr["COSTO_TOTAL"].ToString()));
                    renglonDetalle.Cells[col].CellStyle = cellStyle2Decimales; col++;
                    renglonDetalle.CreateCell(col).SetCellValue(double.Parse(_dr["PRECIO"].ToString()));
                    renglonDetalle.Cells[col].CellStyle = cellStyle2Decimales; col++;
                    renglonDetalle.CreateCell(col).SetCellValue(double.Parse(_dr["PRECIO_TOTAL"].ToString()));
                    renglonDetalle.Cells[col].CellStyle = cellStyle2Decimales; col++;


                    totalCantidad += double.Parse(_dr["CANTIDAD"].ToString());
                    totalCostoTotal += double.Parse(_dr["COSTO_TOTAL"].ToString());
                    totalPrecioTotal += double.Parse(_dr["PRECIO_TOTAL"].ToString());

                    iArticulo++;
                }
                // CREAMOS TOTALES
                iArticulo++;
                IRow renglonTotales = hoja.CreateRow(iArticulo);
                renglonTotales.CreateCell(4).SetCellValue("Subtotales");
                renglonTotales.CreateCell(5).SetCellValue(totalCantidad);
                renglonTotales.CreateCell(7).SetCellValue(totalCostoTotal);
                renglonTotales.CreateCell(9).SetCellValue(totalPrecioTotal);
                renglonTotales.Cells[0].CellStyle = estiloNegritas;
                renglonTotales.Cells[1].CellStyle = estiloNegritas; renglonTotales.Cells[1].CellStyle = cellStyle2DecimalesNegritas;
                renglonTotales.Cells[2].CellStyle = estiloNegritas; renglonTotales.Cells[2].CellStyle = cellStyle2DecimalesNegritas;
                renglonTotales.Cells[3].CellStyle = estiloNegritas; renglonTotales.Cells[3].CellStyle = cellStyle2DecimalesNegritas;

                totalGeneralCantidad += totalCantidad;
                totalGeneralCosto += totalCostoTotal;
                totalGeneralPrecio += totalPrecioTotal;
                iArticulo += 2;
            }
            // AGREGAMOS FILA DE TOTALES
            IRow renglonTotalesGlobales = hoja.CreateRow(iArticulo);
            renglonTotalesGlobales.CreateCell(4).SetCellValue("Totales");
            renglonTotalesGlobales.CreateCell(5).SetCellValue(totalGeneralCantidad);
            renglonTotalesGlobales.CreateCell(7).SetCellValue(totalGeneralCosto);
            renglonTotalesGlobales.CreateCell(9).SetCellValue(totalGeneralPrecio);
            renglonTotalesGlobales.Cells[0].CellStyle = estiloNegritas;
            renglonTotalesGlobales.Cells[1].CellStyle = estiloNegritas; renglonTotalesGlobales.Cells[1].CellStyle = cellStyle2DecimalesNegritas;
            renglonTotalesGlobales.Cells[2].CellStyle = estiloNegritas; renglonTotalesGlobales.Cells[2].CellStyle = cellStyle2DecimalesNegritas;
            renglonTotalesGlobales.Cells[3].CellStyle = estiloNegritas; renglonTotalesGlobales.Cells[3].CellStyle = cellStyle2DecimalesNegritas;



            //AUTOZISECOLUMNAS
            for (int i = 0; i <= 9; i++) // se crea +2 por las columnas de TotalPercepciones, TotalDeducciones y NETO y *2 por gravado y exento
            {
                hoja.AutoSizeColumn(i);
            }

            //GENERAMOS ARCHIVO
            if (System.IO.File.Exists(rutaYNombre))
            {
                System.IO.File.Delete(rutaYNombre);
            }
            System.IO.FileStream fs = new System.IO.FileStream(rutaYNombre, System.IO.FileMode.CreateNew);
            libro.Write(fs);
            fs.Close();
            return "";
        }
    }
}
