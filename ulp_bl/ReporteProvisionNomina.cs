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
    public class ReporteProvisionNomina
    {
        public static DataSet ConsultaReporteProvision(DateTime FechaInicio, DateTime FechaFin)
        {
            String conStr = "";
            DataSet dsReporteProvision = new DataSet();
            try
            {
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "[NOI].[usp_ReporteProvision]";
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));

                dsReporteProvision = cmd.GetDataSet();
                cmd.Connection.Close();
                return dsReporteProvision;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static string GetReporteExcel(String RutaYNombreArchivo, DataSet dsReporte, DateTime FechaInicio, DateTime FechaFin)
        {

            DataTable dtGravado = new DataTable();
            DataTable dtExento = new DataTable();

            dtGravado = dsReporte.Tables[0];
            dtExento = dsReporte.Tables[1];

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

            //titulo
            IRow renglonTitulo = hoja.CreateRow(0);
            ICell celdaTitulo = renglonTitulo.CreateCell(0);
            celdaTitulo.CellStyle = estiloNegritas;
            celdaTitulo.SetCellValue(string.Format("REPORTE DE PROVISIÓN DE NÓMINA DEL {0} AL {1}", FechaInicio.ToString("dd/MM/yyyy"), FechaFin.ToString("dd/MM/yyyy")));
            celdaTitulo.CellStyle.Alignment = HorizontalAlignment.Center;
            hoja.AddMergedRegion(new CellRangeAddress(0, 0, 0, 4));

            IRow renglonEncabezados = hoja.CreateRow(2);
            int contadorColumnas = dtGravado.Columns.Count;

            bool columnaPercepcionAplicada = false; int rowTotalPercepciones = 0;
            bool columnaDeduccionAplicada = false; int rowTotalDeducciones = 0;
            int aumento = 0;
            int aumentoExento = 0;

            for (int i = 0; i < contadorColumnas; i++)
            {
                aumento = i + aumentoExento;
                if (!columnaPercepcionAplicada && !columnaDeduccionAplicada)
                {
                    aumento = i + aumentoExento;
                }
                else if (columnaPercepcionAplicada && !columnaDeduccionAplicada)
                {
                    aumento = (i + aumentoExento) + 1;
                }
                else if (columnaPercepcionAplicada && columnaDeduccionAplicada)
                {
                    aumento = (i + aumentoExento) + 2;
                }
                /* CREAMOS LA COLUMNA DINAMICA */
                ICell celda = renglonEncabezados.CreateCell(aumento);
                celda.CellStyle = estiloNegritas;
                if (dtGravado.Columns[i].ColumnName.Contains("_P"))
                {
                    celda.SetCellValue(dtGravado.Columns[i].ColumnName.Replace("_P", "_GRAVADO"));
                }
                else if (dtGravado.Columns[i].ColumnName.Contains("_D"))
                {
                    //celda.SetCellValue(dtGravado.Columns[i].ColumnName.Replace("_D", "_GRAVADO"));
                    celda.SetCellValue(dtGravado.Columns[i].ColumnName.Replace("_D", ""));
                }
                else
                {
                    celda.SetCellValue(dtGravado.Columns[i].ColumnName);
                }

                /* VERIFICAMOS SI PARA ESE CONCEPTO EXISTE UN EXENTO */
                if (dtExento.Columns.Contains(dtGravado.Columns[i].ColumnName) && dtGravado.Columns[i].ColumnName.Contains("_P"))
                {
                    aumento++;
                    aumentoExento++;
                    ICell celdaExento = renglonEncabezados.CreateCell(aumento);
                    celdaExento.CellStyle = estiloNegritas;
                    celdaExento.SetCellValue(dtGravado.Columns[i].ColumnName.Replace("_P", "").Replace("_D", "") + "_" + "EXENTO");
                }

                /*
                if (dtGravado.Columns[i].ColumnName.Contains("_P") || dtGravado.Columns[i].ColumnName.Contains("_D"))
                {
                    aumento++;
                    aumentoExento++;
                    ICell celdaExento = renglonEncabezados.CreateCell(aumento);
                    celdaExento.CellStyle = estiloNegritas;
                    celdaExento.SetCellValue(dtGravado.Columns[i].ColumnName.Replace("_P", "").Replace("_D", "") + "_" + "EXENTO");
                }*/


                if (dtGravado.Columns[i].ColumnName.Contains("_P") && dtGravado.Columns[i + 1].ColumnName.Contains("_D"))
                {
                    columnaPercepcionAplicada = true;
                    rowTotalPercepciones = aumento + 1;
                    ICell celdaPercepciones = renglonEncabezados.CreateCell(aumento + 1);
                    celdaPercepciones.CellStyle = estiloNegritas;
                    celdaPercepciones.SetCellValue("TOTAL PERCEPCIONES");
                }
                //if (dtGravado.Columns[i].ColumnName.Contains("_D") && dtGravado.Columns[i + 1].ColumnName.Contains("NETO"))
                if (i == contadorColumnas - 1)
                {
                    columnaDeduccionAplicada = true;
                    rowTotalDeducciones = aumento + 1;
                    ICell celdaDeducciones = renglonEncabezados.CreateCell(aumento + 1);
                    celdaDeducciones.CellStyle = estiloNegritas;
                    celdaDeducciones.SetCellValue("TOTAL DEDUCCIONES");
                }
            }
            // CREAMOS LAS COLUMNAS TOTALES            
            ICell celdaTotales = renglonEncabezados.CreateCell(aumento + 2);
            celdaTotales.CellStyle = estiloNegritas;
            celdaTotales.SetCellValue("NETO");
            celdaTotales = renglonEncabezados.CreateCell(aumento + 3);
            celdaTotales.CellStyle = estiloNegritas;
            celdaTotales.SetCellValue("TOTAL GRAVADO");
            celdaTotales = renglonEncabezados.CreateCell(aumento + 4);
            celdaTotales.CellStyle = estiloNegritas;
            celdaTotales.SetCellValue("TOTAL EXENTO");



            int iRenglonDetalle = 3;
            int iCeldaFormatoMoneda = 7;
            columnaPercepcionAplicada = false;
            columnaDeduccionAplicada = false;
            double totalPercepciones = 0;
            double totalDeducciones = 0;
            double neto = 0;
            double totalGravado = 0;
            double totalExento = 0;
            aumento = 0;
            aumentoExento = 0;

            // CREAMOS DETALLE
            foreach (DataRow dataRow in dtGravado.Rows)
            {
                columnaPercepcionAplicada = false;
                columnaDeduccionAplicada = false;
                aumento = 0;
                aumentoExento = 0;
                totalPercepciones = 0;
                totalDeducciones = 0;
                totalGravado = 0;
                totalExento = 0;
                neto = 0;
                IRow rowDetalle = hoja.CreateRow(iRenglonDetalle);
                for (int i = 0; i < contadorColumnas; i++)
                {
                    aumento = i + aumentoExento;
                    if (!columnaPercepcionAplicada && !columnaDeduccionAplicada)
                    {
                        aumento = i + aumentoExento;
                    }
                    else if (columnaPercepcionAplicada && !columnaDeduccionAplicada)
                    {
                        aumento = (i + aumentoExento) + 1;
                    }
                    else if (columnaPercepcionAplicada && columnaDeduccionAplicada)
                    {
                        aumento = (i + aumentoExento) + 2;
                    }

                    ICell celdaDetalle = rowDetalle.CreateCell(aumento);
                    if (i >= iCeldaFormatoMoneda)
                    {
                        celdaDetalle.SetCellValue(double.Parse(dataRow[i].ToString()));
                        celdaDetalle.CellStyle = cellStyle2Decimales;
                        if (dataRow.Table.Columns[i].ColumnName.Contains("_P"))
                        {
                            totalPercepciones += double.Parse(dataRow[i].ToString());
                            totalGravado += double.Parse(dataRow[i].ToString());
                        }
                        else if (dataRow.Table.Columns[i].ColumnName.Contains("_D"))
                        {
                            totalDeducciones += double.Parse(dataRow[i].ToString());
                        }
                        else if (dataRow.Table.Columns[i].ColumnName.Contains("NETO"))
                        {
                            neto = totalPercepciones - totalDeducciones;
                            celdaDetalle.SetCellValue(neto);
                        }
                        // BUSCAMOS EL VALOR EXENTO

                        // if (dataRow.Table.Columns[i].ColumnName.Contains("_P") || dataRow.Table.Columns[i].ColumnName.Contains("_D"))
                        // {                            
                        if (dtExento.Columns.Contains(dataRow.Table.Columns[i].ColumnName))
                        {
                            aumento++;
                            ICell celdaDetalleExento = rowDetalle.CreateCell(aumento);
                            DataRow[] dr = dtExento.Select("CVE_TRAB = " + dataRow["CVE_TRAB"].ToString());
                            celdaDetalleExento.SetCellValue(double.Parse(dr[0][dataRow.Table.Columns[i].ColumnName].ToString()));
                            celdaDetalleExento.CellStyle = cellStyle2Decimales;
                            totalPercepciones += double.Parse(dr[0][dataRow.Table.Columns[i].ColumnName].ToString());
                            totalExento += double.Parse(dr[0][dataRow.Table.Columns[i].ColumnName].ToString());

                            aumentoExento++;
                        }
                        /*
                        if (dataRow.Table.Columns[i].ColumnName.Contains("_P"))
                        {
                            totalPercepciones += double.Parse(dr[0][i].ToString());
                        }
                        else if (dataRow.Table.Columns[i].ColumnName.Contains("_D"))
                        {
                            totalDeducciones += double.Parse(dr[0][i].ToString());
                        }
                        else if (dataRow.Table.Columns[i].ColumnName.Contains("NETO"))
                        {
                            neto = totalPercepciones - totalDeducciones;
                            celdaDetalle.SetCellValue(neto);
                        }
                         * */
                        //}
                        //aumentoExento++;

                    }
                    else
                    {
                        celdaDetalle.SetCellValue(dataRow[i].ToString());
                    }

                    if (aumento + 1 == rowTotalPercepciones)
                    {
                        columnaPercepcionAplicada = true;
                        ICell celdaDetallePercepciones = rowDetalle.CreateCell(aumento + 1);
                        celdaDetallePercepciones.SetCellValue(totalPercepciones);
                        celdaDetallePercepciones.CellStyle = cellStyle2Decimales;
                    }
                    if (aumento + 1 == rowTotalDeducciones)
                    {
                        columnaDeduccionAplicada = true;
                        ICell celdaDetalleDeducciones = rowDetalle.CreateCell(aumento + 1);
                        celdaDetalleDeducciones.SetCellValue(totalDeducciones);
                        celdaDetalleDeducciones.CellStyle = cellStyle2Decimales;
                    }
                }
                // insertamos totales
                // NETO
                ICell celdaDetalleTotales = rowDetalle.CreateCell(aumento + 2);
                celdaDetalleTotales.SetCellValue(totalPercepciones - totalDeducciones);
                celdaDetalleTotales.CellStyle = cellStyle2Decimales;
                // GRAVADO
                celdaDetalleTotales = rowDetalle.CreateCell(aumento + 3);
                celdaDetalleTotales.SetCellValue(totalGravado);
                celdaDetalleTotales.CellStyle = cellStyle2Decimales;
                //EXENTO
                celdaDetalleTotales = rowDetalle.CreateCell(aumento + 4);
                celdaDetalleTotales.SetCellValue(totalExento);
                celdaDetalleTotales.CellStyle = cellStyle2Decimales;

                iRenglonDetalle++;


            }

            //AUTOZISECOLUMNAS
            for (int i = 0; i < (contadorColumnas * 2) + 3; i++) // se crea +2 por las columnas de TotalPercepciones, TotalDeducciones y NETO y *2 por gravado y exento
            {
                hoja.AutoSizeColumn(i);
            }

            for (int i = iCeldaFormatoMoneda; i < (contadorColumnas * 2) + 3; i++)
            {
                hoja.SetDefaultColumnStyle(i, cellStyle2Decimales);
            }

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
