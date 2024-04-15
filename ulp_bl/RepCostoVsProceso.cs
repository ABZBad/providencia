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
using ulp_dl.SIPReportes;

namespace ulp_bl
{
    public class RepCostoVsProceso
    {
        public static DataTable RegresaCostoVsProceso(DateTime FehcaInicial, DateTime FechaFinal, Enumerados.Procesos Proceso)
        {
            string conStr = "";
            DataTable dataTableCostoVsPrecFlete = new DataTable();
            using (var dbContext = new SIPReportesContext())
            {
                conStr = dbContext.Database.Connection.ConnectionString;
            }
            SqlServerCommand cmd = new SqlServerCommand();
            cmd.Connection = DALUtil.GetConnection(conStr);
            cmd.ObjectName = "usp_RepCostoVsPrecioFlete";
            cmd.Parameters.Add(new SqlParameter("@fecha_inicial", FehcaInicial));
            cmd.Parameters.Add(new SqlParameter("@fecha_final", FechaFinal));
            cmd.Parameters.Add(new SqlParameter("@proceso", Proceso.ToString()));
            dataTableCostoVsPrecFlete = cmd.GetDataTable();
            cmd.Connection.Close();
            return dataTableCostoVsPrecFlete;
        }
        public static void GeneraArchivoExcel(string RutaYNombreArchivo, DataTable CostoVsProc, Enumerados.Procesos Proceso)
        {
            HSSFWorkbook xlsWorkBook = new HSSFWorkbook();
            ISheet sheet = xlsWorkBook.CreateSheet("Hoja1");

            //variables de control
            int iColumnaInicialReporte = 1;
            int iRenglonInicialDetalle = 7;
            //se asigna título del reporte
            #region TÍTULO DEL REPORTE

            string tituloReporte = "";
            switch (Proceso)
            {
                case Enumerados.Procesos.F:
                    tituloReporte = "Precio Vs Costo (Flete)";
                    break;
                case Enumerados.Procesos.C:
                    tituloReporte = "Precio Vs Costo (Costura)";
                    break;
                case Enumerados.Procesos.E:
                    tituloReporte = "Precio Vs Costo (Estampado)";
                    break;
            }

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
            CellRangeAddress rangoMerge = new CellRangeAddress(5, 5, 1, 10);
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

            foreach (DataRow renglonCliente in CostoVsProc.Rows)
            {
                IRow renglonDetalle = sheet.CreateRow(iRenglonActual);
                //Factura:
                renglonDetalle.CreateCell(iColumnaInicialReporte).SetCellValue((string)renglonCliente[CostoVsProc.Columns[0].ColumnName]);
                //Pedido:
                renglonDetalle.CreateCell(iColumnaInicialReporte + 1).SetCellValue((int)renglonCliente[CostoVsProc.Columns[1].ColumnName]);
                //clave del cliente
                renglonDetalle.CreateCell(iColumnaInicialReporte + 2).SetCellValue(((string)renglonCliente[CostoVsProc.Columns[2].ColumnName]).Trim());
                //clave del vendedor
                renglonDetalle.CreateCell(iColumnaInicialReporte + 3).SetCellValue((string)renglonCliente[CostoVsProc.Columns[3].ColumnName]);
                //nombre vendedor
                renglonDetalle.CreateCell(iColumnaInicialReporte + 4).SetCellValue((int)renglonCliente[CostoVsProc.Columns[4].ColumnName]);
                //Prendas
                renglonDetalle.CreateCell(iColumnaInicialReporte + 5).SetCellValue(Globales.CodificaCifra(Math.Round((decimal)renglonCliente[CostoVsProc.Columns[5].ColumnName], 2)));
                //Precio
                renglonDetalle.CreateCell(iColumnaInicialReporte + 6).SetCellValue(Globales.CodificaCifra(Math.Round((decimal)renglonCliente[CostoVsProc.Columns[6].ColumnName], 2)));

                //P. Total
                ICell cellPTotal = renglonDetalle.CreateCell(iColumnaInicialReporte + 7);
                cellPTotal.SetCellValue(Convert.ToDouble(renglonCliente[CostoVsProc.Columns[7].ColumnName]));
                cellPTotal.CellStyle = cellStyle2Decimales;
                //Costo
                ICell cellCosto = renglonDetalle.CreateCell(iColumnaInicialReporte + 8);
                cellCosto.SetCellValue(Convert.ToDouble(renglonCliente[CostoVsProc.Columns[8].ColumnName]));
                cellCosto.CellStyle = cellStyle2Decimales;
                //C. Total
                ICell cellCTotal = renglonDetalle.CreateCell(iColumnaInicialReporte + 9);
                cellCTotal.SetCellValue(Convert.ToDouble(Math.Round((decimal)renglonCliente[CostoVsProc.Columns[9].ColumnName], 2)));
                cellCTotal.CellStyle = cellStyle2Decimales;
                //Diferencia
                //englonDetalle.CreateCell(iColumnaInicialReporte + 9).SetCellValue(Convert.ToDouble(renglonCliente[CostoVsPrecFlete.Columns[9].ColumnName]));
                //ICellStyle cellStylePrendas = xlsWorkBook.CreateCellStyle();
                //cellStylePrendas.DataFormat = HSSFDataFormat.GetBuiltinFormat("#,##0_);(#,##0)");

                //se incrementa renglón
                iRenglonActual++;
            }
            #endregion
            //se crea formula para la sumatoria de la prendas


            IRow renglonSumatoriaPrendas = sheet.CreateRow(iRenglonActual + 1);
            ICell celdaSumatoriaPrendas = renglonSumatoriaPrendas.CreateCell(5);
            ICellStyle cellStyleSumatoria = xlsWorkBook.CreateCellStyle();
            cellStyleSumatoria.DataFormat = HSSFDataFormat.GetBuiltinFormat("#,##0_);(#,##0)");
            List<string> formatos = HSSFDataFormat.GetBuiltinFormats();
            celdaSumatoriaPrendas.CellStyle = cellStyleSumatoria;
            celdaSumatoriaPrendas.SetCellFormula(String.Format("SUM(F{0}:F{1})", iRenglonInicialDetalle + 1, iRenglonActual));

            //se obtienen sumatorias de Precio y P. total
            string precioCodific = Globales.CodificaCifra(Math.Round((decimal)CostoVsProc.Compute("SUM(PRECIO)", null), 2));
            string pTotalCodific = Globales.CodificaCifra(Math.Round((decimal)CostoVsProc.Compute("SUM([P. TOTAL])", null), 2));

            renglonSumatoriaPrendas.CreateCell(6).SetCellValue(precioCodific);
            renglonSumatoriaPrendas.CreateCell(7).SetCellValue(pTotalCodific);

            ICell celdaSumatoriaDif = renglonSumatoriaPrendas.CreateCell(10);
            celdaSumatoriaDif.SetCellFormula(String.Format("SUM(K{0}:K{1})", iRenglonInicialDetalle + 1, iRenglonActual));
            celdaSumatoriaDif.CellStyle = cellStyle2Decimales;

            //se ajustan las culumnas al ancho automático
            for (int i = 0; i < 5; i++)
            {
                sheet.AutoSizeColumn(i + 1);
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
