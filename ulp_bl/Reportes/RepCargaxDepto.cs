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
//using ulp_dl.aspel_prod30;
using ulp_dl.SIPReportes;

namespace ulp_bl.Reportes
{
    public class RepCargaxDepto
    {
        public static DataTable RegresaDepartamentos()
        {
            U_DEPARTAMENTO u_depto = new U_DEPARTAMENTO();

            return u_depto.ConsultarDepartamentos();
        }

        public static DataTable RegresaEstadoCuentaXDepto(string NombreDepartamento)
        {
            string conStr = "";
            DataTable dataTableEstadoCuentaXDepto = new DataTable();
            using (var dbContext = new SIPReportesContext())
            {
                conStr = dbContext.Database.Connection.ConnectionString;
            }
            SqlServerCommand cmd = new SqlServerCommand();
            cmd.Connection = DALUtil.GetConnection(conStr);
            if (NombreDepartamento.ToUpper().Contains("BORDADO") && NombreDepartamento.ToUpper().Contains("COSTURA"))
                cmd.ObjectName = "usp_RepEstadoCuentaXDeptoFull";
            else
            {
                cmd.ObjectName = "usp_RepEstadoCuentaXDepto";
                cmd.Parameters.Add(new SqlParameter("@nombre_departamento", NombreDepartamento));
            }
            dataTableEstadoCuentaXDepto = cmd.GetDataTable();
            cmd.Connection.Close();
            return dataTableEstadoCuentaXDepto;
        }

        public static void GeneraArchivoExcel(string RutaYNombreArchivo, DataTable CostoVsProc, String departamentoAConsultar = "")
        {
            HSSFWorkbook xlsWorkBook = new HSSFWorkbook();
            ISheet sheet = xlsWorkBook.CreateSheet("Estado de Cuenta");

            //variables de control
            int iColumnaInicialReporte = 1;
            int iRenglonInicialDetalle = 10;
            //se asigna título del reporte
            #region TÍTULO DEL REPORTE

            string tituloReporte = "Estado de Cuenta";

            #endregion
            #region ENCABEZADOS

            //creación del título
            IRow renglonTitulo = sheet.CreateRow(5);
            ICell celdaTitulo = renglonTitulo.CreateCell(iColumnaInicialReporte);
            celdaTitulo.SetCellValue(tituloReporte);

            //se crea el estilo del título
            ICellStyle cellStyleTitulo = xlsWorkBook.CreateCellStyle();
            IFont fontTitulo = xlsWorkBook.CreateFont();
            fontTitulo.FontHeightInPoints = 14;
            fontTitulo.Boldweight = (short)FontBoldWeight.Bold;
            cellStyleTitulo.SetFont(fontTitulo);
            cellStyleTitulo.Alignment = HorizontalAlignment.Center;
            celdaTitulo.CellStyle = cellStyleTitulo;
            //Se hace un Merge de celdas para el título
            CellRangeAddress rangoMerge = new CellRangeAddress(5, 5, 1, 9);
            sheet.AddMergedRegion(rangoMerge);
            //se crean las columnas del reporte
            IRow renglonCabezera = sheet.CreateRow(6);
            int iCol = iColumnaInicialReporte; //contador de columnas
            foreach (DataColumn Columna in CostoVsProc.Columns)
            {
                renglonCabezera.CreateCell(iCol).SetCellValue(Columna.ColumnName);
                iCol++;
            }
            #endregion
            #region SE ESCRIBE EL DETALLE EN EL ARCHIVO
            int iRenglonActual = iRenglonInicialDetalle;

            IDataFormat dataFormat2Decimales = xlsWorkBook.CreateDataFormat();
            ICellStyle cellStyle2Decimales = xlsWorkBook.CreateCellStyle();
            cellStyle2Decimales.DataFormat = dataFormat2Decimales.GetFormat("###,##0.00");

            IDataFormat dataFormatFecha = xlsWorkBook.CreateDataFormat();
            ICellStyle cellStyleFecha = xlsWorkBook.CreateCellStyle();
            cellStyleFecha.DataFormat = dataFormatFecha.GetFormat("dd/mm/yyyy");

            foreach (DataRow renglonCliente in CostoVsProc.Rows)
            {
                IRow renglonDetalle = sheet.CreateRow(iRenglonActual);
                int i = 0;
                //pedido
                renglonDetalle.CreateCell(iColumnaInicialReporte + i).SetCellValue(((int)renglonCliente[CostoVsProc.Columns[i].ColumnName])); i++;
                //cliente
                renglonDetalle.CreateCell(iColumnaInicialReporte + i).SetCellValue((string)renglonCliente[CostoVsProc.Columns[i].ColumnName]); i++;
                //modelo
                renglonDetalle.CreateCell(iColumnaInicialReporte + i).SetCellValue((string)renglonCliente[CostoVsProc.Columns[i].ColumnName]); i++;
                //cantidad
                renglonDetalle.CreateCell(iColumnaInicialReporte + i).SetCellValue((int)renglonCliente[CostoVsProc.Columns[i].ColumnName]); i++;
                //ruta
                renglonDetalle.CreateCell(iColumnaInicialReporte + i).SetCellValue((string)renglonCliente[CostoVsProc.Columns[i].ColumnName]); i++;
                //Depto.
                renglonDetalle.CreateCell(iColumnaInicialReporte + i).SetCellValue((string)renglonCliente[CostoVsProc.Columns[i].ColumnName]); i++;
                //Fec. Venc.
                if (renglonCliente[CostoVsProc.Columns[i].ColumnName] != DBNull.Value)
                {

                    ICell celdaFechaVenc = renglonDetalle.CreateCell(iColumnaInicialReporte + i);
                    celdaFechaVenc.SetCellValue((DateTime)renglonCliente[CostoVsProc.Columns[i].ColumnName]);
                    celdaFechaVenc.CellStyle = cellStyleFecha;
                }
                i++;
                // TInta REal
                if (renglonCliente[CostoVsProc.Columns[i].ColumnName] != DBNull.Value)
                {

                    ICell celdaFechaVenc = renglonDetalle.CreateCell(iColumnaInicialReporte + i);
                    celdaFechaVenc.SetCellValue((int)renglonCliente[CostoVsProc.Columns[i].ColumnName]);

                }
                i++;
                // Puntadas
                if (renglonCliente[CostoVsProc.Columns[i].ColumnName] != DBNull.Value)
                {
                    if (departamentoAConsultar.Contains("BORDADO"))
                    {
                        renglonDetalle.CreateCell(iColumnaInicialReporte + i).SetCellValue((int)renglonCliente[CostoVsProc.Columns[i].ColumnName]);
                    }
                }
                i++;
                //Indice:
                renglonDetalle.CreateCell(iColumnaInicialReporte + i).SetCellValue((int)renglonCliente[CostoVsProc.Columns[i].ColumnName]); i++;
                //Tipo.
                if (renglonCliente[CostoVsProc.Columns[i].ColumnName] != DBNull.Value)
                    renglonDetalle.CreateCell(iColumnaInicialReporte + i).SetCellValue((string)renglonCliente[CostoVsProc.Columns[i].ColumnName]);
                i++;
                //DEtalle
                if (renglonCliente[CostoVsProc.Columns[i].ColumnName] != DBNull.Value)
                {
                    if (departamentoAConsultar.Contains("EMPAQUE"))
                    {
                        renglonDetalle.CreateCell(iColumnaInicialReporte + i).SetCellValue((string)renglonCliente[CostoVsProc.Columns[i].ColumnName]);
                    }
                }
                i++;
                /*

                //P. Total
                ICell cellPTotal = renglonDetalle.CreateCell(iColumnaInicialReporte + 6);
                cellPTotal.SetCellValue(Convert.ToDouble(renglonCliente[CostoVsProc.Columns[6].ColumnName]));
                cellPTotal.CellStyle = cellStyle2Decimales;
                //Costo
                ICell cellCosto = renglonDetalle.CreateCell(iColumnaInicialReporte + 7);
                cellCosto.SetCellValue(Convert.ToDouble(renglonCliente[CostoVsProc.Columns[7].ColumnName]));
                cellCosto.CellStyle = cellStyle2Decimales;
                //C. Total
                ICell cellCTotal = renglonDetalle.CreateCell(iColumnaInicialReporte + 8);
                cellCTotal.SetCellValue(Convert.ToDouble(Math.Round((decimal)renglonCliente[CostoVsProc.Columns[8].ColumnName], 2)));
                cellCTotal.CellStyle = cellStyle2Decimales;
                //Diferencia
                //englonDetalle.CreateCell(iColumnaInicialReporte + 9).SetCellValue(Convert.ToDouble(renglonCliente[CostoVsPrecFlete.Columns[9].ColumnName]));
                //ICellStyle cellStylePrendas = xlsWorkBook.CreateCellStyle();
                //cellStylePrendas.DataFormat = HSSFDataFormat.GetBuiltinFormat("#,##0_);(#,##0)");
                */
                //se incrementa renglón
                iRenglonActual++;
            }
            #endregion
            //se crea formula para la sumatoria de la prendas


            IRow renglonSumatoriaCantidad = sheet.CreateRow(iRenglonActual + 1);
            renglonSumatoriaCantidad.CreateCell(3).SetCellValue("Totales:");
            ICell celdaSumatoriaCantidad = renglonSumatoriaCantidad.CreateCell(4);
            ICellStyle cellStyleSumatoria = xlsWorkBook.CreateCellStyle();
            cellStyleSumatoria.DataFormat = HSSFDataFormat.GetBuiltinFormat("#,##0_);(#,##0)");
            List<string> formatos = HSSFDataFormat.GetBuiltinFormats();
            celdaSumatoriaCantidad.CellStyle = cellStyleSumatoria;
            celdaSumatoriaCantidad.SetCellFormula(String.Format("SUM(E{0}:E{1})", iRenglonInicialDetalle + 1, iRenglonActual));

            //se ajustan las culumnas al ancho automático
            /*
            sheet.SetColumnWidth(1, Utiles.ExcelNpoiUtil.AnchoColumna(54));
            sheet.SetColumnWidth(2, Utiles.ExcelNpoiUtil.AnchoColumna(51));
            sheet.SetColumnWidth(3, Utiles.ExcelNpoiUtil.AnchoColumna(115));
            sheet.SetColumnWidth(4, Utiles.ExcelNpoiUtil.AnchoColumna(665));
            sheet.SetColumnWidth(5, Utiles.ExcelNpoiUtil.AnchoColumna(63));
            sheet.SetColumnWidth(6, Utiles.ExcelNpoiUtil.AnchoColumna(108));
            sheet.SetColumnWidth(7, Utiles.ExcelNpoiUtil.AnchoColumna(49));
            sheet.SetColumnWidth(8, Utiles.ExcelNpoiUtil.AnchoColumna(36));
            sheet.SetColumnWidth(9, Utiles.ExcelNpoiUtil.AnchoColumna(76));
             * */
            for (int i = 1; i < 10; i++)
            {
                sheet.AutoSizeColumn(i);
            }

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
        public static void GeneraArchivoExcelEstampado(string RutaYNombreArchivo, DataTable CostoVsProc)
        {
            HSSFWorkbook xlsWorkBook = new HSSFWorkbook();
            ISheet sheet = xlsWorkBook.CreateSheet("Estado de Cuenta");

            //variables de control
            int iColumnaInicialReporte = 1;
            int iRenglonInicialDetalle = 8;
            //se asigna título del reporte
            #region TÍTULO DEL REPORTE

            string tituloReporte = "Estado de Cuenta";

            #endregion
            #region ENCABEZADOS

            //creación del título
            IRow renglonTitulo = sheet.CreateRow(5);
            ICell celdaTitulo = renglonTitulo.CreateCell(iColumnaInicialReporte);
            celdaTitulo.SetCellValue(tituloReporte);

            //se crea el estilo del título
            ICellStyle cellStyleTitulo = xlsWorkBook.CreateCellStyle();
            IFont fontTitulo = xlsWorkBook.CreateFont();
            fontTitulo.FontHeightInPoints = 14;
            fontTitulo.Boldweight = (short)FontBoldWeight.Bold;
            cellStyleTitulo.SetFont(fontTitulo);
            cellStyleTitulo.Alignment = HorizontalAlignment.Center;
            celdaTitulo.CellStyle = cellStyleTitulo;
            //Se hace un Merge de celdas para el título
            CellRangeAddress rangoMerge = new CellRangeAddress(5, 5, 1, 17);
            sheet.AddMergedRegion(rangoMerge);
            //se crean las columnas del reporte

            IRow renglonCabezera = sheet.CreateRow(6);
            int iCol = iColumnaInicialReporte; //contador de columnas
            /*
            foreach (DataColumn Columna in CostoVsProc.Columns)
            {
                renglonCabezera.CreateCell(iCol).SetCellValue(Columna.ColumnName);
                iCol++;
            }*/

            renglonCabezera.CreateCell(iCol).SetCellValue("Indice"); iCol++;
            renglonCabezera.CreateCell(iCol).SetCellValue("Pedido"); iCol++;
            renglonCabezera.CreateCell(iCol).SetCellValue("Modelo"); iCol++;
            renglonCabezera.CreateCell(iCol).SetCellValue("Marcos"); iCol++;
            renglonCabezera.CreateCell(iCol).SetCellValue("Cliente"); iCol++;

            renglonCabezera.CreateCell(iCol).SetCellValue("Estampador"); iCol++;
            renglonCabezera.CreateCell(iCol).SetCellValue("Preparado"); iCol++;

            renglonCabezera.CreateCell(iCol).SetCellValue("Cantidad"); iCol++;
            renglonCabezera.CreateCell(iCol).SetCellValue("Ruta"); iCol++;
            renglonCabezera.CreateCell(iCol).SetCellValue("Maquina"); iCol++;
            renglonCabezera.CreateCell(iCol).SetCellValue("Tipo"); iCol++;
            renglonCabezera.CreateCell(iCol).SetCellValue("Fecha de Salida"); iCol++;

            renglonCabezera.CreateCell(iCol).SetCellValue("Colores"); iCol++;
            renglonCabezera.CreateCell(iCol).SetCellValue("Conv."); iCol++;
            renglonCabezera.CreateCell(iCol).SetCellValue("T. Est."); iCol++;
            renglonCabezera.CreateCell(iCol).SetCellValue("F.E.C."); iCol++;
            renglonCabezera.CreateCell(iCol).SetCellValue("Logo"); iCol++;




            #endregion
            #region SE ESCRIBE EL DETALLE EN EL ARCHIVO
            int iRenglonActual = iRenglonInicialDetalle;

            IDataFormat dataFormat2Decimales = xlsWorkBook.CreateDataFormat();
            ICellStyle cellStyle2Decimales = xlsWorkBook.CreateCellStyle();
            cellStyle2Decimales.DataFormat = dataFormat2Decimales.GetFormat("###,##0.00");

            IDataFormat dataFormatFecha = xlsWorkBook.CreateDataFormat();
            ICellStyle cellStyleFecha = xlsWorkBook.CreateCellStyle();
            cellStyleFecha.DataFormat = dataFormatFecha.GetFormat("dd/mm/yyyy");

            foreach (DataRow renglonCliente in CostoVsProc.Rows)
            {
                IRow renglonDetalle = sheet.CreateRow(iRenglonActual);
                int i = 0;
                //Indice:
                renglonDetalle.CreateCell(iColumnaInicialReporte).SetCellValue((int)renglonCliente[CostoVsProc.Columns[0].ColumnName]);
                //pedido
                renglonDetalle.CreateCell(iColumnaInicialReporte + 1).SetCellValue(((int)renglonCliente[CostoVsProc.Columns[1].ColumnName]));
                //modelo
                renglonDetalle.CreateCell(iColumnaInicialReporte + 2).SetCellValue((string)renglonCliente[CostoVsProc.Columns[2].ColumnName]);
                //marcos
                renglonDetalle.CreateCell(iColumnaInicialReporte + 3).SetCellValue("          ");
                //cliente
                renglonDetalle.CreateCell(iColumnaInicialReporte + 4).SetCellValue((string)renglonCliente[CostoVsProc.Columns[3].ColumnName]);
                //estampador
                renglonDetalle.CreateCell(iColumnaInicialReporte + 5).SetCellValue("          ");
                //preparado
                renglonDetalle.CreateCell(iColumnaInicialReporte + 6).SetCellValue("          ");

                //cantidad
                renglonDetalle.CreateCell(iColumnaInicialReporte + 7).SetCellValue((int)renglonCliente[CostoVsProc.Columns[4].ColumnName]);
                //ruta
                renglonDetalle.CreateCell(iColumnaInicialReporte + 8).SetCellValue((string)renglonCliente[CostoVsProc.Columns[5].ColumnName]);
                //Depto.
                renglonDetalle.CreateCell(iColumnaInicialReporte + 9).SetCellValue((string)renglonCliente[CostoVsProc.Columns[6].ColumnName]);
                //Tipo.
                if (renglonCliente[CostoVsProc.Columns[7].ColumnName] != DBNull.Value)
                    renglonDetalle.CreateCell(iColumnaInicialReporte + 10).SetCellValue((string)renglonCliente[CostoVsProc.Columns[7].ColumnName]);
                //Fec. Venc.
                if (renglonCliente[CostoVsProc.Columns[8].ColumnName] != DBNull.Value)
                {

                    ICell celdaFechaVenc = renglonDetalle.CreateCell(iColumnaInicialReporte + 11);
                    celdaFechaVenc.SetCellValue((DateTime)renglonCliente[CostoVsProc.Columns[8].ColumnName]);
                    celdaFechaVenc.CellStyle = cellStyleFecha;
                }

                //Tinta Real - se coloca por el usuario
                ICell celdaTintaReal = renglonDetalle.CreateCell(iColumnaInicialReporte + 12);
                celdaTintaReal.SetCellValue((int)renglonCliente[CostoVsProc.Columns[9].ColumnName]);

                //Tinta Real - se coloca por el usuario
                ICell celdaConversion = renglonDetalle.CreateCell(iColumnaInicialReporte + 13);
                celdaConversion.SetCellFormula(String.Format("IF(N{0}=1,1,IF(N{0}=2,1.5,IF(N{0}=3,2,IF(N{0}=4,2.5,IF(N{0}=5,3,IF(N{0}=6,3.5,0))))))", iRenglonActual + 1));

                //Tiempo Estimado
                ICell celdaTiempoEstimado = renglonDetalle.CreateCell(iColumnaInicialReporte + 14);
                celdaTiempoEstimado.SetCellFormula(String.Format("I{0}*O{0}", iRenglonActual + 1));

                iRenglonActual++;
            }
            #endregion
            //se crea formula para la sumatoria de la prendas


            IRow renglonSumatoriaCantidad = sheet.CreateRow(iRenglonActual + 1);
            renglonSumatoriaCantidad.CreateCell(4).SetCellValue("Totales:");
            ICell celdaSumatoriaCantidad = renglonSumatoriaCantidad.CreateCell(5);
            ICellStyle cellStyleSumatoria = xlsWorkBook.CreateCellStyle();
            cellStyleSumatoria.DataFormat = HSSFDataFormat.GetBuiltinFormat("#,##0_);(#,##0)");
            List<string> formatos = HSSFDataFormat.GetBuiltinFormats();
            celdaSumatoriaCantidad.CellStyle = cellStyleSumatoria;
            celdaSumatoriaCantidad.SetCellFormula(String.Format("SUM(F{0}:F{1})", iRenglonInicialDetalle + 1, iRenglonActual));

            //se ajustan las culumnas al ancho automático            
            sheet.SetColumnWidth(1, Utiles.ExcelNpoiUtil.AnchoColumna(54));
            sheet.SetColumnWidth(2, Utiles.ExcelNpoiUtil.AnchoColumna(51));
            sheet.SetColumnWidth(3, Utiles.ExcelNpoiUtil.AnchoColumna(115));
            sheet.SetColumnWidth(4, Utiles.ExcelNpoiUtil.AnchoColumna(51));
            sheet.SetColumnWidth(5, Utiles.ExcelNpoiUtil.AnchoColumna(665));

            sheet.SetColumnWidth(6, Utiles.ExcelNpoiUtil.AnchoColumna(51));
            sheet.SetColumnWidth(7, Utiles.ExcelNpoiUtil.AnchoColumna(51));

            sheet.SetColumnWidth(8, Utiles.ExcelNpoiUtil.AnchoColumna(63));
            sheet.SetColumnWidth(9, Utiles.ExcelNpoiUtil.AnchoColumna(108));
            sheet.SetColumnWidth(10, Utiles.ExcelNpoiUtil.AnchoColumna(49));
            sheet.SetColumnWidth(11, Utiles.ExcelNpoiUtil.AnchoColumna(36));
            sheet.SetColumnWidth(12, Utiles.ExcelNpoiUtil.AnchoColumna(76));

            sheet.SetColumnWidth(13, Utiles.ExcelNpoiUtil.AnchoColumna(51));
            sheet.SetColumnWidth(14, Utiles.ExcelNpoiUtil.AnchoColumna(51));
            sheet.SetColumnWidth(15, Utiles.ExcelNpoiUtil.AnchoColumna(51));
            sheet.SetColumnWidth(16, Utiles.ExcelNpoiUtil.AnchoColumna(51));
            sheet.SetColumnWidth(17, Utiles.ExcelNpoiUtil.AnchoColumna(51));


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
        public static void GeneraArchivoExcelEmpaque(string RutaYNombreArchivo, DataTable CostoVsProc, String departamentoAConsultar = "")
        {
            HSSFWorkbook xlsWorkBook = new HSSFWorkbook();
            ISheet sheet = xlsWorkBook.CreateSheet("Estado de Cuenta");

            //variables de control
            int iColumnaInicialReporte = 1;
            int iRenglonInicialDetalle = 10;
            //se asigna título del reporte
            #region TÍTULO DEL REPORTE

            string tituloReporte = "Estado de Cuenta";

            #endregion
            #region ENCABEZADOS

            //creación del título
            IRow renglonTitulo = sheet.CreateRow(5);
            ICell celdaTitulo = renglonTitulo.CreateCell(iColumnaInicialReporte);
            celdaTitulo.SetCellValue(tituloReporte);

            //se crea el estilo del título
            ICellStyle cellStyleTitulo = xlsWorkBook.CreateCellStyle();
            IFont fontTitulo = xlsWorkBook.CreateFont();
            fontTitulo.FontHeightInPoints = 14;
            fontTitulo.Boldweight = (short)FontBoldWeight.Bold;
            cellStyleTitulo.SetFont(fontTitulo);
            cellStyleTitulo.Alignment = HorizontalAlignment.Center;
            celdaTitulo.CellStyle = cellStyleTitulo;
            //Se hace un Merge de celdas para el título
            CellRangeAddress rangoMerge = new CellRangeAddress(5, 5, 1, 9);
            sheet.AddMergedRegion(rangoMerge);
            //se crean las columnas del reporte
            IRow renglonCabezera = sheet.CreateRow(6);
            int iCol = iColumnaInicialReporte; //contador de columnas
            foreach (DataColumn Columna in CostoVsProc.Columns)
            {
                renglonCabezera.CreateCell(iCol).SetCellValue(Columna.ColumnName);
                iCol++;
            }
            #endregion
            #region SE ESCRIBE EL DETALLE EN EL ARCHIVO
            int iRenglonActual = iRenglonInicialDetalle;

            IDataFormat dataFormat2Decimales = xlsWorkBook.CreateDataFormat();
            ICellStyle cellStyle2Decimales = xlsWorkBook.CreateCellStyle();
            cellStyle2Decimales.DataFormat = dataFormat2Decimales.GetFormat("###,##0.00");

            IDataFormat dataFormatFecha = xlsWorkBook.CreateDataFormat();
            ICellStyle cellStyleFecha = xlsWorkBook.CreateCellStyle();
            cellStyleFecha.DataFormat = dataFormatFecha.GetFormat("dd/mm/yyyy");

            foreach (DataRow renglonCliente in CostoVsProc.Rows)
            {
                IRow renglonDetalle = sheet.CreateRow(iRenglonActual);
                int i = 0;
                //Ruta
                renglonDetalle.CreateCell(iColumnaInicialReporte + i).SetCellValue(((string)renglonCliente[CostoVsProc.Columns[i].ColumnName])); i++;
                //Estatus
                renglonDetalle.CreateCell(iColumnaInicialReporte + i).SetCellValue(((string)renglonCliente[CostoVsProc.Columns[i].ColumnName])); i++;
                //pedido
                renglonDetalle.CreateCell(iColumnaInicialReporte + i).SetCellValue(((int)renglonCliente[CostoVsProc.Columns[i].ColumnName])); i++;
                //cliente
                renglonDetalle.CreateCell(iColumnaInicialReporte + i).SetCellValue((string)renglonCliente[CostoVsProc.Columns[i].ColumnName]); i++;
                //modelo
                renglonDetalle.CreateCell(iColumnaInicialReporte + i).SetCellValue((string)renglonCliente[CostoVsProc.Columns[i].ColumnName]); i++;
                //cantidad
                renglonDetalle.CreateCell(iColumnaInicialReporte + i).SetCellValue((int)renglonCliente[CostoVsProc.Columns[i].ColumnName]); i++;
                //ruta
                renglonDetalle.CreateCell(iColumnaInicialReporte + i).SetCellValue((string)renglonCliente[CostoVsProc.Columns[i].ColumnName]); i++;
                //Depto.
                renglonDetalle.CreateCell(iColumnaInicialReporte + i).SetCellValue((string)renglonCliente[CostoVsProc.Columns[i].ColumnName]); i++;
                //Detalle
                if (renglonCliente[CostoVsProc.Columns[i].ColumnName] != DBNull.Value)
                {
                    if (departamentoAConsultar.Contains("EMPAQUE"))
                    {
                        renglonDetalle.CreateCell(iColumnaInicialReporte + i).SetCellValue((string)renglonCliente[CostoVsProc.Columns[i].ColumnName]);
                    }
                }
                i++;
                //Fec. Venc.
                if (renglonCliente[CostoVsProc.Columns[i].ColumnName] != DBNull.Value)
                {

                    ICell celdaFechaVenc = renglonDetalle.CreateCell(iColumnaInicialReporte + i);
                    celdaFechaVenc.SetCellValue((DateTime)renglonCliente[CostoVsProc.Columns[i].ColumnName]);
                    celdaFechaVenc.CellStyle = cellStyleFecha;
                }
                i++;
                // TInta REal
                if (renglonCliente[CostoVsProc.Columns[i].ColumnName] != DBNull.Value)
                {

                    ICell celdaFechaVenc = renglonDetalle.CreateCell(iColumnaInicialReporte + i);
                    celdaFechaVenc.SetCellValue((int)renglonCliente[CostoVsProc.Columns[i].ColumnName]);

                }
                i++;
                // Puntadas
                if (renglonCliente[CostoVsProc.Columns[i].ColumnName] != DBNull.Value)
                {
                    if (departamentoAConsultar.Contains("BORDADO"))
                    {
                        renglonDetalle.CreateCell(iColumnaInicialReporte + i).SetCellValue((int)renglonCliente[CostoVsProc.Columns[i].ColumnName]);
                    }
                }
                i++;
                //Tipo.
                if (renglonCliente[CostoVsProc.Columns[i].ColumnName] != DBNull.Value)
                    renglonDetalle.CreateCell(iColumnaInicialReporte + i).SetCellValue((string)renglonCliente[CostoVsProc.Columns[i].ColumnName]);
                i++;
                //Indice:
                renglonDetalle.CreateCell(iColumnaInicialReporte + i).SetCellValue((int)renglonCliente[CostoVsProc.Columns[i].ColumnName]); i++;
                /*

                //P. Total
                ICell cellPTotal = renglonDetalle.CreateCell(iColumnaInicialReporte + 6);
                cellPTotal.SetCellValue(Convert.ToDouble(renglonCliente[CostoVsProc.Columns[6].ColumnName]));
                cellPTotal.CellStyle = cellStyle2Decimales;
                //Costo
                ICell cellCosto = renglonDetalle.CreateCell(iColumnaInicialReporte + 7);
                cellCosto.SetCellValue(Convert.ToDouble(renglonCliente[CostoVsProc.Columns[7].ColumnName]));
                cellCosto.CellStyle = cellStyle2Decimales;
                //C. Total
                ICell cellCTotal = renglonDetalle.CreateCell(iColumnaInicialReporte + 8);
                cellCTotal.SetCellValue(Convert.ToDouble(Math.Round((decimal)renglonCliente[CostoVsProc.Columns[8].ColumnName], 2)));
                cellCTotal.CellStyle = cellStyle2Decimales;
                //Diferencia
                //englonDetalle.CreateCell(iColumnaInicialReporte + 9).SetCellValue(Convert.ToDouble(renglonCliente[CostoVsPrecFlete.Columns[9].ColumnName]));
                //ICellStyle cellStylePrendas = xlsWorkBook.CreateCellStyle();
                //cellStylePrendas.DataFormat = HSSFDataFormat.GetBuiltinFormat("#,##0_);(#,##0)");
                */
                //se incrementa renglón
                iRenglonActual++;
            }
            #endregion
            //se crea formula para la sumatoria de la prendas


            IRow renglonSumatoriaCantidad = sheet.CreateRow(iRenglonActual + 1);
            renglonSumatoriaCantidad.CreateCell(3).SetCellValue("Totales:");
            ICell celdaSumatoriaCantidad = renglonSumatoriaCantidad.CreateCell(6);
            ICellStyle cellStyleSumatoria = xlsWorkBook.CreateCellStyle();
            cellStyleSumatoria.DataFormat = HSSFDataFormat.GetBuiltinFormat("#,##0_);(#,##0)");
            List<string> formatos = HSSFDataFormat.GetBuiltinFormats();
            celdaSumatoriaCantidad.CellStyle = cellStyleSumatoria;
            celdaSumatoriaCantidad.SetCellFormula(String.Format("SUM(G{0}:G{1})", iRenglonInicialDetalle + 1, iRenglonActual));

            //se ajustan las culumnas al ancho automático
            /*
            sheet.SetColumnWidth(1, Utiles.ExcelNpoiUtil.AnchoColumna(54));
            sheet.SetColumnWidth(2, Utiles.ExcelNpoiUtil.AnchoColumna(51));
            sheet.SetColumnWidth(3, Utiles.ExcelNpoiUtil.AnchoColumna(115));
            sheet.SetColumnWidth(4, Utiles.ExcelNpoiUtil.AnchoColumna(665));
            sheet.SetColumnWidth(5, Utiles.ExcelNpoiUtil.AnchoColumna(63));
            sheet.SetColumnWidth(6, Utiles.ExcelNpoiUtil.AnchoColumna(108));
            sheet.SetColumnWidth(7, Utiles.ExcelNpoiUtil.AnchoColumna(49));
            sheet.SetColumnWidth(8, Utiles.ExcelNpoiUtil.AnchoColumna(36));
            sheet.SetColumnWidth(9, Utiles.ExcelNpoiUtil.AnchoColumna(76));
             * */
            for (int i = 1; i < 10; i++)
            {
                sheet.AutoSizeColumn(i);
            }

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
        public static void GeneraArchivoExcelEmbarque(string RutaYNombreArchivo, DataTable CostoVsProc, String departamentoAConsultar = "")
        {
            HSSFWorkbook xlsWorkBook = new HSSFWorkbook();
            ISheet sheet = xlsWorkBook.CreateSheet("Estado de Cuenta");

            //variables de control
            int iColumnaInicialReporte = 1;
            int iRenglonInicialDetalle = 10;
            //se asigna título del reporte
            #region TÍTULO DEL REPORTE

            string tituloReporte = "Estado de Cuenta";

            #endregion
            #region ENCABEZADOS

            //creación del título
            IRow renglonTitulo = sheet.CreateRow(5);
            ICell celdaTitulo = renglonTitulo.CreateCell(iColumnaInicialReporte);
            celdaTitulo.SetCellValue(tituloReporte);

            //se crea el estilo del título
            ICellStyle cellStyleTitulo = xlsWorkBook.CreateCellStyle();
            IFont fontTitulo = xlsWorkBook.CreateFont();
            fontTitulo.FontHeightInPoints = 14;
            fontTitulo.Boldweight = (short)FontBoldWeight.Bold;
            cellStyleTitulo.SetFont(fontTitulo);
            cellStyleTitulo.Alignment = HorizontalAlignment.Center;
            celdaTitulo.CellStyle = cellStyleTitulo;
            //Se hace un Merge de celdas para el título
            CellRangeAddress rangoMerge = new CellRangeAddress(5, 5, 1, 9);
            sheet.AddMergedRegion(rangoMerge);
            //se crean las columnas del reporte
            IRow renglonCabezera = sheet.CreateRow(6);
            int iCol = iColumnaInicialReporte; //contador de columnas
            foreach (DataColumn Columna in CostoVsProc.Columns)
            {
                renglonCabezera.CreateCell(iCol).SetCellValue(Columna.ColumnName);
                iCol++;
            }
            #endregion
            #region SE ESCRIBE EL DETALLE EN EL ARCHIVO
            int iRenglonActual = iRenglonInicialDetalle;

            IDataFormat dataFormat2Decimales = xlsWorkBook.CreateDataFormat();
            ICellStyle cellStyle2Decimales = xlsWorkBook.CreateCellStyle();
            cellStyle2Decimales.DataFormat = dataFormat2Decimales.GetFormat("###,##0.00");

            IDataFormat dataFormatFecha = xlsWorkBook.CreateDataFormat();
            ICellStyle cellStyleFecha = xlsWorkBook.CreateCellStyle();
            cellStyleFecha.DataFormat = dataFormatFecha.GetFormat("dd/mm/yyyy");

            foreach (DataRow renglonCliente in CostoVsProc.Rows)
            {
                IRow renglonDetalle = sheet.CreateRow(iRenglonActual);
                int i = 0;
                //Pedido
                renglonDetalle.CreateCell(iColumnaInicialReporte + i).SetCellValue(((int)renglonCliente[CostoVsProc.Columns[i].ColumnName])); i++;
                //Fecha
                renglonDetalle.CreateCell(iColumnaInicialReporte + i).SetCellValue((DateTime.Parse(renglonCliente[CostoVsProc.Columns[i].ColumnName].ToString())).ToString("dd/MM/yyyy")); i++;
                //clave cliente
                renglonDetalle.CreateCell(iColumnaInicialReporte + i).SetCellValue(((string)renglonCliente[CostoVsProc.Columns[i].ColumnName])); i++;
                //nombre cliente
                renglonDetalle.CreateCell(iColumnaInicialReporte + i).SetCellValue((string)renglonCliente[CostoVsProc.Columns[i].ColumnName]); i++;
                //cantidad prendas
                renglonDetalle.CreateCell(iColumnaInicialReporte + i).SetCellValue(((int)renglonCliente[CostoVsProc.Columns[i].ColumnName])); i++;
                //Fecha Vencimiento
                renglonDetalle.CreateCell(iColumnaInicialReporte + i).SetCellValue((renglonCliente[CostoVsProc.Columns[i].ColumnName].ToString() != "" ? DateTime.Parse(renglonCliente[CostoVsProc.Columns[i].ColumnName].ToString()).ToString("dd/MM/yyyy") : "")); i++;
                //Fecha Empaque
                renglonDetalle.CreateCell(iColumnaInicialReporte + i).SetCellValue((renglonCliente[CostoVsProc.Columns[i].ColumnName].ToString() != "" ? DateTime.Parse(renglonCliente[CostoVsProc.Columns[i].ColumnName].ToString()).ToString("dd/MM/yyyy") : "")); i++;
                //Direccion
                renglonDetalle.CreateCell(iColumnaInicialReporte + i).SetCellValue((string)renglonCliente[CostoVsProc.Columns[i].ColumnName]); i++;

                //se incrementa renglón
                iRenglonActual++;
            }
            #endregion
            //se crea formula para la sumatoria de la prendas


            IRow renglonSumatoriaCantidad = sheet.CreateRow(iRenglonActual + 1);
            renglonSumatoriaCantidad.CreateCell(3).SetCellValue("Totales:");
            ICell celdaSumatoriaCantidad = renglonSumatoriaCantidad.CreateCell(5);
            ICellStyle cellStyleSumatoria = xlsWorkBook.CreateCellStyle();
            cellStyleSumatoria.DataFormat = HSSFDataFormat.GetBuiltinFormat("#,##0_);(#,##0)");
            List<string> formatos = HSSFDataFormat.GetBuiltinFormats();
            celdaSumatoriaCantidad.CellStyle = cellStyleSumatoria;
            celdaSumatoriaCantidad.SetCellFormula(String.Format("SUM(F{0}:F{1})", iRenglonInicialDetalle + 1, iRenglonActual));

            //se ajustan las culumnas al ancho automático
            /*
            sheet.SetColumnWidth(1, Utiles.ExcelNpoiUtil.AnchoColumna(54));
            sheet.SetColumnWidth(2, Utiles.ExcelNpoiUtil.AnchoColumna(51));
            sheet.SetColumnWidth(3, Utiles.ExcelNpoiUtil.AnchoColumna(115));
            sheet.SetColumnWidth(4, Utiles.ExcelNpoiUtil.AnchoColumna(665));
            sheet.SetColumnWidth(5, Utiles.ExcelNpoiUtil.AnchoColumna(63));
            sheet.SetColumnWidth(6, Utiles.ExcelNpoiUtil.AnchoColumna(108));
            sheet.SetColumnWidth(7, Utiles.ExcelNpoiUtil.AnchoColumna(49));
            sheet.SetColumnWidth(8, Utiles.ExcelNpoiUtil.AnchoColumna(36));
            sheet.SetColumnWidth(9, Utiles.ExcelNpoiUtil.AnchoColumna(76));
             * */
            for (int i = 1; i < 10; i++)
            {
                sheet.AutoSizeColumn(i);
            }

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
