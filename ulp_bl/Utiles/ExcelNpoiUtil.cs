using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace ulp_bl.Utiles
{
    public class ExcelNpoiUtil
    {
        public static Stream Dt2Excel(DataTable dt)
        {
            HSSFWorkbook xlsWorkBook = new HSSFWorkbook();
            int rowNumber = 0;


            ISheet sheet = xlsWorkBook.CreateSheet();

            //escribe nombres de columnas:
            int colNumber = 0;
            foreach (DataColumn dataColumn in dt.Columns)
            {
                AsignaValorCelda(ref sheet, rowNumber, colNumber, null, dataColumn.ColumnName);
                colNumber++;
            }
            rowNumber++;
            foreach (DataRow dataRow in dt.Rows)
            {
                colNumber = 0;
                foreach (DataColumn dataColumn in dt.Columns)
                {
                    AsignaValorCelda(ref sheet, rowNumber, colNumber, null, dataRow[dataColumn.ColumnName]);
                    colNumber++;
                }
                rowNumber++;
            }


            MemoryStream st = new MemoryStream();

            xlsWorkBook.Write(st);

            return st;

        }
        public static int AnchoColumna(int Pixels)
        {
            return (int)(5000*Pixels)/137;
        }

        public static void AsignaValorCelda(ref ISheet Hoja,int Reng, int Col, ICellStyle Estilo,object Valor)
        {
            IRow Renglon = Hoja.GetRow(Reng);

            if (Renglon == null)
                Renglon = Hoja.CreateRow(Reng);

            ICell celda = Renglon.CreateCell(Col);

            switch (Valor.GetType().ToString())
            {
                case "System.Double":
                    celda.SetCellValue(Convert.ToDouble(Valor));
                    break;
                case "System.DateTime":
                    celda.SetCellValue(Convert.ToDateTime(Valor));
                    break;
                default:
                    celda.SetCellValue(Valor.ToString());
                    break;
            }

            if (Estilo != null)
                celda.CellStyle = Estilo;

        }
        public static void AsignaFormulaCelda(ref ISheet Hoja, int Reng, int Col, ICellStyle Estilo, string Formula)
        {
            IRow Renglon = Hoja.GetRow(Reng);

            if (Renglon == null)
                Renglon = Hoja.CreateRow(Reng);

            ICell celda = Renglon.CreateCell(Col);

            
            celda.SetCellFormula(Formula);
            

            if (Estilo != null)
                celda.CellStyle = Estilo;

        }
        public static short FormatoCelda(ref HSSFWorkbook Libro, Enumerados.FormatosNPOI Formato)
        {
            short fmto = 0;

            ICellStyle estilo = Libro.CreateCellStyle();
            

            switch (Formato)
            {
                case Enumerados.FormatosNPOI.MONEDA:
                    fmto = Libro.CreateDataFormat().GetFormat("_-$* #,##0.00_-;-$* #,##0.00_-;_-$* \" - \"??_-;_-@_-");
                    break;
                case Enumerados.FormatosNPOI.MILES:
                    fmto = Libro.CreateDataFormat().GetFormat("#,##0");
                    break;
                case Enumerados.FormatosNPOI.MILES2DECIMALES:
                    fmto = Libro.CreateDataFormat().GetFormat("#,##0.00");
                    break;
            }
            return fmto;
        }
        public static short FormatoCelda(ref HSSFWorkbook Libro, string Formato)
        {
            short fmto = 0;

            ICellStyle estilo = Libro.CreateCellStyle();

            fmto = Libro.CreateDataFormat().GetFormat(Formato);

            return fmto;
        }
        public static void AsignaFormatoGeneral1(IWorkbook xlsWorkBook,ISheet sheet, int numCols,int anchoColumnaDetalle)
        { 
                ICellStyle estiloGeneral = xlsWorkBook.CreateCellStyle();
                IFont fontStyle = xlsWorkBook.CreateFont();
                fontStyle.FontHeightInPoints = 11;
                fontStyle.FontName = "Calibri";
                estiloGeneral.SetFont(fontStyle);            
                for (int c = 0; c < numCols; c++)
                {
                    sheet.SetDefaultColumnStyle(c, estiloGeneral);
                    sheet.SetColumnWidth(c, AnchoColumna(anchoColumnaDetalle));
                }
                sheet.SetColumnWidth(7, AnchoColumna(60));
        }

        public static CellValue EvaluaFormula(IWorkbook xlsWorkBook,ISheet sheet,ICell CeldaAEvaluar)
        {
            CellValue cellValue = null;
            HSSFFormulaEvaluator fev = new HSSFFormulaEvaluator(sheet,xlsWorkBook);
            cellValue = fev.Evaluate(CeldaAEvaluar);
            return cellValue;
        }
        public static void AplicaEstiloCeldasParaNegativos(ref IRow row, ICellStyle estilo)
        {           
            

            HSSFFormulaEvaluator fev = new HSSFFormulaEvaluator(row.Sheet.Workbook);

            double valor = 0;
            for (var i = 0; i <= row.Cells.Count - 1; i++)
            {
                ICell cell = row.GetCell(i) ?? row.CreateCell(i);
                if (cell.CellType.ToString() == "Numeric" || cell.CellType.ToString() == "Formula")
                {
                    if (cell.CellType.ToString() == "Formula")
                    {
                        CellValue val = fev.Evaluate(cell);
                        double.TryParse(val.NumberValue.ToString(), out valor);
                    }
                    else
                    {
                        double.TryParse(cell.NumericCellValue.ToString(), out valor);
                    }

                    if (valor < 0)
                    {
                        cell.CellStyle = estilo;
                    }
                }

            }
        }

        public static void AplicaEstiloCeldaParaNegativos(ref ICell cell, double valor)
        {
            ICellStyle estilo = cell.Row.Sheet.Workbook.CreateCellStyle();
            estilo.FillBackgroundColor = IndexedColors.Yellow.Index;
            estilo.FillForegroundColor = IndexedColors.Yellow.Index;
            estilo.FillPattern = FillPattern.SolidForeground;
            if (valor < 0)
            {
                cell.CellStyle = estilo;
            }
        }
    }
}
