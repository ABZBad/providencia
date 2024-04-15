using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using sm_dl;
using sm_dl.SqlServer;
using ulp_bl.Utiles;
//using kk = ulp_dl;
using ulp_dl.SIPReportes;

namespace ulp_bl.Reportes
{
    public class EmisionCartera
    {
        public static DataTable RegresaEmisionCartera(int AniosAEmitir,int AniosAtras,int MesesAConsiderar,string Agente)
        {
            //Se crea instancia de tabla que almacenará el resultado del Stored Procedure
            DataTable dataTableCartera = new DataTable();
            //Se crea instancia de tabla que se devolverá en esta función
            DataTable dataTableCarteraFinal = new DataTable();
            using (var DbContext = new SIPReportesContext())
            {
                //Se crea objeto para invocar SP
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(DbContext.Database.Connection.ConnectionString);
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@cve_vend", Agente));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@anio", AniosAEmitir - AniosAtras));
                cmd.ObjectName = "usp_EmisionCartera";
                dataTableCartera = cmd.GetDataTable();  //Se devuelven los resultados del SP como Tabla
                // Se copia de la estructura (sin datos) a la tabla que se va a devolver
                dataTableCarteraFinal = dataTableCartera.Clone();
                
                //Se ordena la tabla resultado del SP en la tabla debido a que ordenarla en el SP es muy lento
                dataTableCartera.DefaultView.Sort = "ANNIO DESC";
                //Re-asigno la vista de la tabla ya ordenada a su misma Tabla base
                dataTableCartera = dataTableCartera.DefaultView.ToTable();

                //Se hace una consulta de tipo DISTINCT para obtener los años de la tabla y se guardan en una nueva tabla con 1 solo campo (año)
                DataTable dtDistintcAnnios = new DataView(dataTableCartera).ToTable(true,new string[] { "ANNIO" });

                //Establezco un patrón de búsqueda usando una expresión regular ya que voy a extraer solo los registros con clave numérica para después ordenarlos
                //Cabe aclarar que la clave numérica está almacenada como un String (Legacy del sistema anterior)
                Regex reg = new Regex("^[0-9]+$",RegexOptions.Compiled);

                //Recorro los años
                foreach (DataRow renglonAnnio in dtDistintcAnnios.Rows)
                {

                    //Extraigo los registros numéricos y los guardo en una nueva tabla
                    var registrosNumericos = from n in dataTableCartera.AsEnumerable() where reg.IsMatch(n.Field<string>("CLAVE_CLIENTE").Trim()) && n.Field<int>("ANNIO") == (int)renglonAnnio["ANNIO"] select n;

                    if (registrosNumericos.Any())
                    {
                        DataTable dtSoloNumericos = registrosNumericos.CopyToDataTable();

                        //Copio la estructura de la tabla resultante y cambio el tipo de dato de la columna Clave_Cliente de String a Int para poder
                        //Hacer un ordenamiento ascendiente
                        DataTable dtClone = new DataTable();
                        dtClone = dtSoloNumericos.Clone();
                        dtClone.Columns["CLAVE_CLIENTE"].DataType = typeof (int);
                        dtClone.Merge(dtSoloNumericos, true, MissingSchemaAction.Ignore);
                        //Ordeno la tabla por el nuevo campo numérico
                        dtClone.DefaultView.Sort = "CLAVE_CLIENTE";
                        //Re-Asigno los resultados a su tabla base
                        dtClone = dtClone.DefaultView.ToTable();
                        //Agrego los registros ya ordenados a la tabla final resultante (la que se va a devolver)
                        dataTableCarteraFinal.Merge(dtClone, true, MissingSchemaAction.Ignore);
                    }
                    //Hago el mismo proceso anterior pero para los registros no numéricos
                    var registrosNoNumericos = from n in dataTableCartera.AsEnumerable() where !reg.IsMatch(n.Field<string>("CLAVE_CLIENTE").Trim()) && n.Field<int>("ANNIO") == (int)renglonAnnio["ANNIO"] select n;
                    if (registrosNoNumericos.Any())
                    {
                        DataTable dtSoloNoNumericos = registrosNoNumericos.CopyToDataTable();
                        dtSoloNoNumericos.DefaultView.Sort = "CLAVE_CLIENTE";
                        dtSoloNoNumericos = dtSoloNoNumericos.DefaultView.ToTable();


                        dataTableCarteraFinal.Merge(dtSoloNoNumericos, true, MissingSchemaAction.Ignore);
                    }
                }

            }
            //Regreso la tabla final
            return dataTableCarteraFinal;
        }

        public static void GeneraArchivoExcel(string RutaYNombreArchivo, DataTable TablaEmisionCartera,string Agente)
        {
            HSSFWorkbook xlsWorkBook = new HSSFWorkbook();
            ISheet sheet = xlsWorkBook.CreateSheet("Hoja1");


            #region FORMATO POR DEFAULT

            ICellStyle cellStyle = xlsWorkBook.CreateCellStyle();
            IFont fontStyle = xlsWorkBook.CreateFont();
            fontStyle.FontHeightInPoints = 9;
            cellStyle.SetFont(fontStyle);
            for (int i = 0; i < 16; i++)
            {
                sheet.SetDefaultColumnStyle(i, cellStyle);
            }
            #endregion

            #region ENCABEZADOS
            //título del reporte
            IRow renglonTitulo = sheet.CreateRow(0);
            renglonTitulo.CreateCell(0).SetCellValue(string.Format("Reporte de Cartera del Agente: {0}", Agente));

            IRow renglonCabecera = sheet.CreateRow(2);
            renglonCabecera.CreateCell(0).SetCellValue("Cte");
            renglonCabecera.CreateCell(1).SetCellValue("NOMBRE");
            renglonCabecera.CreateCell(2).SetCellValue("Año");
            renglonCabecera.CreateCell(3).SetCellValue("Ene");
            renglonCabecera.CreateCell(4).SetCellValue("Feb");
            renglonCabecera.CreateCell(5).SetCellValue("Mar");
            renglonCabecera.CreateCell(6).SetCellValue("Abr");
            renglonCabecera.CreateCell(7).SetCellValue("May");
            renglonCabecera.CreateCell(8).SetCellValue("Jun");
            renglonCabecera.CreateCell(9).SetCellValue("Jul");
            renglonCabecera.CreateCell(10).SetCellValue("Ago");
            renglonCabecera.CreateCell(11).SetCellValue("Sep");
            renglonCabecera.CreateCell(12).SetCellValue("Oct");
            renglonCabecera.CreateCell(13).SetCellValue("Nov");
            renglonCabecera.CreateCell(14).SetCellValue("Dic");
            renglonCabecera.CreateCell(15).SetCellValue("Tot");
            //NUEVO CAMPOS
            renglonCabecera.CreateCell(16).SetCellValue("FILIAL");

            


            #endregion
            //Variables de control
            int renglonActual = 4;  //Renglón inicial (basado en índice 0)
            int renglonInicial = renglonActual;
            //Escribe el detalle
            #region DETALLE
            foreach (DataRow renglonEmisionCartera in TablaEmisionCartera.Rows)
            {
                IRow renglonXlsEmisionCartera = sheet.CreateRow(renglonActual);

                renglonXlsEmisionCartera.CreateCell(0).SetCellValue(renglonEmisionCartera["CLAVE_CLIENTE"].ToString());
                if (renglonEmisionCartera["NOMBRE_CLIENTE"].ToString().Trim().Length > 30)
                {
                    renglonXlsEmisionCartera.CreateCell(1).SetCellValue(renglonEmisionCartera["NOMBRE_CLIENTE"].ToString().Trim().Substring(0, 30));
                }
                else
                {
                    renglonXlsEmisionCartera.CreateCell(1).SetCellValue(renglonEmisionCartera["NOMBRE_CLIENTE"].ToString().Trim());
                }
                renglonXlsEmisionCartera.CreateCell(2).SetCellValue(Convert.ToDouble(renglonEmisionCartera["ANNIO"]));

                for (int i = 0; i < 12; i++)
                {
                    double _valorMes;
                    //double valorMes = Convert.(renglonEmisionCartera[string.Format("MES{0}", i+1)]);
                    String valorMes = renglonEmisionCartera[string.Format("MES{0}", i+1)].ToString();
                    if (valorMes != "")
                    {
                        if (double.TryParse(valorMes,out _valorMes))
                        {
                            renglonXlsEmisionCartera.CreateCell(3 + i).SetCellValue(_valorMes);
                        }
                        else
                            renglonXlsEmisionCartera.CreateCell(3 + i).SetCellValue(valorMes);
                        
                    }
                }
                renglonXlsEmisionCartera.CreateCell(15).SetCellValue(Convert.ToDouble(renglonEmisionCartera["TOTAL"]));
                renglonXlsEmisionCartera.CreateCell(16).SetCellValue(renglonEmisionCartera["FILIAL"].ToString());

                renglonActual++;
            }
            #endregion
            //Se ajustan las columnas
            #region ANCHOS DE COLUMNA
            sheet.SetColumnWidth(0, ExcelNpoiUtil.AnchoColumna(42));
            sheet.SetColumnWidth(1, ExcelNpoiUtil.AnchoColumna(208));
            for (int i = 2; i < 15; i++)
            {
                sheet.SetColumnWidth(i, ExcelNpoiUtil.AnchoColumna(33));
            }
            sheet.SetColumnWidth(15,ExcelNpoiUtil.AnchoColumna(35));
            #endregion            
            //Se inmobilizan páneles
            sheet.CreateFreezePane(1, 4);
            //Se escribe el total y la fórmula desumatoria
            IRow renglonSumatoria = sheet.CreateRow(renglonActual + 2);

            ICell celdaEtiquetaTotal = renglonSumatoria.CreateCell(12);
            celdaEtiquetaTotal.SetCellValue("TOTAL");


            ICellStyle estiloFmtoNumerico = xlsWorkBook.CreateCellStyle();
            estiloFmtoNumerico.DataFormat = HSSFDataFormat.GetBuiltinFormat("#,##0_);(#,##0)");
            IFont fuenteFmtoNumerico = xlsWorkBook.CreateFont();
            fuenteFmtoNumerico.FontHeightInPoints = 9;
            estiloFmtoNumerico.SetFont(fuenteFmtoNumerico);
            ICell celdaFormulaTotal = renglonSumatoria.CreateCell(14);
            celdaFormulaTotal.SetCellFormula(string.Format("SUM(P{0}:P{1})", renglonInicial, renglonActual));
            celdaFormulaTotal.CellStyle = estiloFmtoNumerico;
            //Merge
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(renglonActual + 2, renglonActual + 2, 14, 15));
            //guarda el archivo
            if (File.Exists(RutaYNombreArchivo))
            {
                File.Delete(RutaYNombreArchivo);
            }
            FileStream fs = new FileStream(RutaYNombreArchivo, FileMode.CreateNew);

            xlsWorkBook.Write(fs);


            fs.Close();
        }
    }
}
