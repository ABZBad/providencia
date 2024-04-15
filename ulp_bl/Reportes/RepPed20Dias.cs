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

namespace ulp_bl.Reportes
{
    public class RepPed20Dias
    {
        public DataTable RegresaTabla(DateTime Fecha)
        {
            System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
            DataTable dataTablePedidos = new DataTable();
            using (var DbContext = new SIPReportesContext())
            {
                SqlServerCommand _cmd = new SqlServerCommand();
                _cmd.Connection = sm_dl.DALUtil.GetConnection(DbContext.Database.Connection.ConnectionString);
                _cmd.ObjectName = "usp_RepPedidosMas20Dias";
                _cmd.Parameters.Add(new SqlParameter("@fecha", Fecha));
                dataTablePedidos = _cmd.GetDataTable();
                _cmd.Connection.Close();

            }
            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.ElapsedMilliseconds);
            return dataTablePedidos;
        }

        public static void GeneraArchivoExcel(string RutaYNombreArchivo, DataTable TablaPedidos, DateTime Fecha)
        {

            HSSFWorkbook xlsWorkBook = new HSSFWorkbook();

            ISheet sheet = xlsWorkBook.CreateSheet("Hoja1");

            #region ENCABEZADOS

            IRow renglonCabezera = sheet.CreateRow(0);
            renglonCabezera.CreateCell(0).SetCellValue("F Venc");
            renglonCabezera.CreateCell(1).SetCellValue("PEDIDO");
            renglonCabezera.CreateCell(2).SetCellValue("F PEDIDO");
            renglonCabezera.CreateCell(3).SetCellValue("Cte");
            renglonCabezera.CreateCell(4).SetCellValue("NOMBRE");
            renglonCabezera.CreateCell(5).SetCellValue("Agt");
            renglonCabezera.CreateCell(6).SetCellValue("PRENDAS");
            renglonCabezera.CreateCell(7).SetCellValue("Dif");
            renglonCabezera.CreateCell(8).SetCellValue("FACTURA");

            IRow espacio = sheet.CreateRow(1);

            #endregion

            int renglonIndex = 2; //basado en índice 0
            #region Ventas

            ICellStyle celdaEstiloSUM = xlsWorkBook.CreateCellStyle();
            celdaEstiloSUM = xlsWorkBook.CreateCellStyle();
            celdaEstiloSUM.DataFormat = HSSFDataFormat.GetBuiltinFormat("(0);(0)");

            ICellStyle indicadorStyle = xlsWorkBook.CreateCellStyle();
            indicadorStyle = xlsWorkBook.CreateCellStyle();
            indicadorStyle.FillForegroundColor = IndexedColors.Yellow.Index;
            indicadorStyle.FillPattern = FillPattern.SolidForeground;

            ICellStyle indicador2Style = xlsWorkBook.CreateCellStyle();
            indicador2Style = xlsWorkBook.CreateCellStyle();
            indicador2Style.FillForegroundColor = IndexedColors.Orange.Index;
            indicador2Style.FillPattern = FillPattern.SolidForeground;

            foreach (DataRow renglon in TablaPedidos.Rows)
            {
                IRow renglonDetalle = sheet.CreateRow(renglonIndex);
                //renglonFechaIni.RowStyle.Alignment = HorizontalAlignment.Left;
                ICell oCell = renglonDetalle.CreateCell(0);
                if (renglon["F_VENCIMIENTO"].ToString() != "")
                {
                    renglonDetalle.CreateCell(0).SetCellValue(((DateTime)renglon["F_VENCIMIENTO"]).ToShortDateString());
                }
                else
                {
                    renglonDetalle.CreateCell(0).SetCellValue((renglon["F_VENCIMIENTO"]).ToString());
                }

                ICell oCellCVE = renglonDetalle.CreateCell(1);
                oCellCVE.SetCellValue(renglon["CVE_DOC"].ToString().Trim());
                if ((bool)renglon["INDICADOR"] && renglon["FACTURA"].ToString() == "")
                    oCellCVE.CellStyle = indicadorStyle;
                else if ((bool)renglon["INDICADOR"] && renglon["FACTURA"].ToString() != "")
                    oCellCVE.CellStyle = indicador2Style;
                //renglonDetalle.CreateCell(1).
                //renglonDetalle.Cells[1].CellStyle = xlsWorkBook.CreateCellStyle();
                //renglonDetalle.Cells[1].CellStyle.FillBackgroundColor = IndexedColors.Red.Index;

                if (renglon["FECHA_DOC"].ToString() != "")
                {
                    renglonDetalle.CreateCell(2).SetCellValue(((DateTime)renglon["FECHA_DOC"]).ToShortDateString());
                }
                else
                {
                    renglonDetalle.CreateCell(2).SetCellValue((renglon["FECHA_DOC"]).ToString());
                }
                renglonDetalle.CreateCell(3).SetCellValue(renglon["CCLIE"].ToString().Trim());
                renglonDetalle.CreateCell(4).SetCellValue(renglon["NOMBRE"].ToString().Trim());
                renglonDetalle.CreateCell(5).SetCellValue(renglon["CVE_VEND"].ToString().Trim());


                ICell Prendas = renglonDetalle.CreateCell(6);
                Prendas.SetCellValue((int)renglon["PRENDAS"]);
                //Prendas.CellStyle = celdaEstiloSUM;

                //renglonDetalle.CreateCell(6).SetCellValue(renglon["PRENDAS"].ToString());

                renglonDetalle.CreateCell(7).SetCellValue(Convert.ToDouble(renglon["Dif"].ToString()));
                renglonDetalle.CreateCell(8).SetCellValue(renglon["FACTURA"].ToString());


                renglonIndex++;
            }
            #endregion




            //Totales
            IRow RowTotal = sheet.CreateRow(renglonIndex + 1);

            // Total de pesos
            ICell Total = RowTotal.CreateCell(6);
            Total.CellFormula = string.Format("SUM(G3:G" + renglonIndex.ToString() + ")");
            Total.CellStyle = celdaEstiloSUM;



            for (int i = 0; i < 7; i++)
            {
                sheet.AutoSizeColumn(i);
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
