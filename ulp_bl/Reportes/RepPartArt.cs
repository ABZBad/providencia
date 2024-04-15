using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ulp_dl.SIPReportes;
using sm_dl.SqlServer;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using System.Data.SqlClient;
using ulp_bl.Utiles;
using NPOI.SS.Util;

namespace ulp_bl.Reportes
{
    public class RepPartArt
    {
        enum TipoFiltro
        {

            Linea = 1,
            Modelo = 2, 
            Talla = 3    

        };
        
        public DataTable RegresaTabla(DateTime FechaInicial, DateTime FechaFinal, int TipoReporte)
        {
            System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
            DataTable dataTablePedidos = new DataTable();
            using (var DbContext = new SIPReportesContext())
            {
                SqlServerCommand _cmd = new SqlServerCommand();
                _cmd.Connection = sm_dl.DALUtil.GetConnection(DbContext.Database.Connection.ConnectionString);
                _cmd.ObjectName = "usp_RepPartArt";
                _cmd.Parameters.Add(new SqlParameter("@fechaInicial", FechaInicial));
                _cmd.Parameters.Add(new SqlParameter("@fechaFinal", FechaFinal));
                _cmd.Parameters.Add(new SqlParameter("@tipoReporte", TipoReporte));
                dataTablePedidos = _cmd.GetDataTable();
                _cmd.Connection.Close();

            }
            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.ElapsedMilliseconds);
            return dataTablePedidos;
        }

        public static void GeneraArchivoExcel(string RutaYNombreArchivo, DataTable Tabla, int TipoReporte, DateTime FechaIni, DateTime FechaFin)
        {
            string TituloTipoReporte="";
            
            if (TipoReporte == 1)
            {
                TituloTipoReporte = "LINEA";
            }
            if (TipoReporte == 2)
            {
                TituloTipoReporte = "MODELO";
            }
            if (TipoReporte == 3)
            {
                TituloTipoReporte = "TALLA";
            }
            
            HSSFWorkbook xlsWorkBook = new HSSFWorkbook();

            ISheet sheet = xlsWorkBook.CreateSheet("Hoja1");

            #region ENCABEZADOS

            IRow renglonEncabezado = sheet.CreateRow(0);

            renglonEncabezado.CreateCell(0).SetCellValue(string.Format("Reporte de participación de mercado por {0}", TituloTipoReporte));

            IRow renglonDetallesEncabezado = sheet.CreateRow(2);
            renglonDetallesEncabezado.CreateCell(0).SetCellValue("Emitido del");
            renglonDetallesEncabezado.CreateCell(1).SetCellValue(FechaIni.ToShortDateString());

            IRow renglonDetallesEncabezado2 = sheet.CreateRow(3);
            renglonDetallesEncabezado2.CreateCell(0).SetCellValue("Al");
            renglonDetallesEncabezado2.CreateCell(1).SetCellValue(FechaFin.ToShortDateString());

            IRow renglonDetallesEncabezado3 = sheet.CreateRow(5);
            renglonDetallesEncabezado3.CreateCell(0).SetCellValue("MODELO");
            renglonDetallesEncabezado3.CreateCell(1).SetCellValue("DESCRIPCION");
            renglonDetallesEncabezado3.CreateCell(2).SetCellValue("FACTURA");
            renglonDetallesEncabezado3.CreateCell(3).SetCellValue("NC");
            renglonDetallesEncabezado3.CreateCell(4).SetCellValue("SUMA");
            renglonDetallesEncabezado3.CreateCell(5).SetCellValue("% PART");

            //sheet.CreateFreezePane(1, 6);

            #endregion

            int renglonIndex = 6; //basado en índice 0
            #region Ventas

            // AQUI COMIENZA else VERDADERO DETALLE delegate REPORTE

            ICellStyle celdaEstiloSUM = xlsWorkBook.CreateCellStyle();
            celdaEstiloSUM = xlsWorkBook.CreateCellStyle();
            celdaEstiloSUM.DataFormat = HSSFDataFormat.GetBuiltinFormat("#,##0.00_);(#,##0.00)");


            IDataFormat dFormat2 = xlsWorkBook.CreateDataFormat();
            IDataFormat dFormat4 = xlsWorkBook.CreateDataFormat();

            short dosDig = dFormat2.GetFormat("0.00%");
            short cuatroDig = dFormat4.GetFormat("0.0000%");

            ICellStyle celdaEstiloPorcent2Dig = xlsWorkBook.CreateCellStyle();
            celdaEstiloPorcent2Dig = xlsWorkBook.CreateCellStyle();
            celdaEstiloPorcent2Dig.DataFormat = dosDig;

            ICellStyle celdaEstiloPorcent4Dig = xlsWorkBook.CreateCellStyle();
            celdaEstiloPorcent4Dig = xlsWorkBook.CreateCellStyle();
            celdaEstiloPorcent4Dig.DataFormat = cuatroDig;



            foreach (DataRow renglon in Tabla.Rows)
            {
                IRow renglonDetalle = sheet.CreateRow(renglonIndex); ;

                renglonDetalle.CreateCell(0).SetCellValue(renglon["MODELO"].ToString());
                renglonDetalle.CreateCell(1).SetCellValue(renglon["DESCRIPCION"].ToString() );
                renglonDetalle.CreateCell(2).SetCellValue((int)renglon["FACTURA"]);
                renglonDetalle.CreateCell(3).SetCellValue((int)renglon["NC"]);
                renglonDetalle.CreateCell(4).SetCellValue((int)renglon["SUMA"]);

                // Porcentaje por renglón
                ICell PorcRow = renglonDetalle.CreateCell(5);
                PorcRow.CellFormula = string.Format("E" + (renglonIndex+1).ToString() + "/E" + (Tabla.Rows.Count+9).ToString());
                PorcRow.CellStyle = celdaEstiloPorcent4Dig;

                
                renglonIndex++;

            }
            #endregion


            //Totales
            IRow RowTotal = sheet.CreateRow(renglonIndex + 2);

            // Total Prendas en Factura
            ICell TotalFact = RowTotal.CreateCell(2);
            TotalFact.CellFormula = string.Format("SUM(C7:C" + renglonIndex.ToString() + ")");
            TotalFact.CellStyle = celdaEstiloSUM;

            // Total Prendas en NC
            ICell TotalNC = RowTotal.CreateCell(3);
            TotalNC.CellFormula = string.Format("SUM(D7:D" + renglonIndex.ToString() + ")");
            TotalNC.CellStyle = celdaEstiloSUM;

            // Total Prendas en SUMA
            ICell TotalSUMA = RowTotal.CreateCell(4);
            TotalSUMA.CellFormula = string.Format("SUM(E7:E" + renglonIndex.ToString() + ")");
            TotalSUMA.CellStyle = celdaEstiloSUM;

            // Total Porcentaje
            ICell TotalPorc = RowTotal.CreateCell(5);
            TotalPorc.CellFormula = string.Format("SUM(F7:F" + renglonIndex.ToString() + ")");
            TotalPorc.CellStyle = celdaEstiloPorcent2Dig;


            for (int i = 0; i < 6; i++)
            {
                if (i == 0)
                {
                    sheet.SetColumnWidth(i, ExcelNpoiUtil.AnchoColumna(95));
                }
                else if (i == 1)
                {
                    sheet.SetColumnWidth(i, ExcelNpoiUtil.AnchoColumna(257));
                }
                else
                {
                    sheet.SetColumnWidth(i, ExcelNpoiUtil.AnchoColumna(80));
                }
                
            }
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
