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
using ulp_dl.SIPNegocio;

namespace ulp_bl.Reportes
{
    public class RepVentPesosPrendas
    {
        public DataTable RegresaTablaPedidos(DateTime FechaInicial, DateTime FechaFinal, bool SoloVentasEnRango, ref Exception ex)
        {
            try
            {
                String conStr = "";
                //System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
                DataTable dataTablePedidos = new DataTable();
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                    SqlServerCommand _cmd = new SqlServerCommand();
                    _cmd.Connection = sm_dl.DALUtil.GetConnection(conStr);
                    _cmd.ObjectName = "usp_RepVentPesosPrendas";
                    _cmd.Parameters.Add(new SqlParameter("@fecha_inicial", FechaInicial));
                    _cmd.Parameters.Add(new SqlParameter("@fecha_final", FechaFinal));
                    _cmd.Parameters.Add(new SqlParameter("@vtas_en_rango", (SoloVentasEnRango ? 1 : 0)));
                    dataTablePedidos = _cmd.GetDataTable();
                    ex = null;
                    _cmd.Connection.Close();
                
                //sw.Stop();
                //System.Diagnostics.Debug.WriteLine(sw.ElapsedMilliseconds);
                return dataTablePedidos;
            }
            catch (Exception _ex)
            {
                ex = _ex;
                return null;
            }
        }

        public static void GeneraArchivoExcel(string RutaYNombreArchivo, DataTable TablaPedidos, DateTime FechaInicial, DateTime FechaFinal)
        {
            HSSFWorkbook xlsWorkBook = new HSSFWorkbook();

            ISheet sheet = xlsWorkBook.CreateSheet("Hoja1");

            #region ENCABEZADOS

            IRow renglonTitulo = sheet.CreateRow(0);
            renglonTitulo.CreateCell(0).SetCellValue("");
            renglonTitulo.CreateCell(1).SetCellValue("Rep Vtas en pesos y PRENDAS por AGENTE");

            ICellStyle celdaEstiloAligneRight = xlsWorkBook.CreateCellStyle();
            celdaEstiloAligneRight = xlsWorkBook.CreateCellStyle();
            celdaEstiloAligneRight.Alignment = HorizontalAlignment.Right;

            

            IRow renglonFechaIni = sheet.CreateRow(1);
            renglonFechaIni.CreateCell(0).SetCellValue("Emitido del");
            ICell celdaEmitido = renglonFechaIni.CreateCell(1);
            celdaEmitido.SetCellValue(FechaInicial.ToLongDateString());
            celdaEmitido.CellStyle = celdaEstiloAligneRight;

            IRow renglonFechaFin = sheet.CreateRow(2);
            renglonFechaFin.CreateCell(0).SetCellValue("Al");
            ICell celdaAl = renglonFechaFin.CreateCell(1);
            celdaAl.SetCellValue(FechaFinal.ToLongDateString());
            celdaAl.CellStyle = celdaEstiloAligneRight;

            IRow renglonCabezera = sheet.CreateRow(4);
            renglonCabezera.CreateCell(0).SetCellValue("AGENTE");
            renglonCabezera.CreateCell(1).SetCellValue("NOMBRE");
            renglonCabezera.CreateCell(2).SetCellValue("Pesos");
            renglonCabezera.CreateCell(3).SetCellValue("PRENDAS");
            renglonCabezera.CreateCell(4).SetCellValue("Promedio");
            
            #endregion

            int renglonIndex = 5; //basado en índice 0
            #region Ventas

            ICellStyle celdaEstilo2Decimales = xlsWorkBook.CreateCellStyle();
            celdaEstilo2Decimales = xlsWorkBook.CreateCellStyle();
            celdaEstilo2Decimales.DataFormat = HSSFDataFormat.GetBuiltinFormat("#,##0.00_);(#,##0.00)");

            foreach (DataRow renglon in TablaPedidos.Rows)
            {
                IRow renglonDetalle = sheet.CreateRow(renglonIndex);

                
                
                

                //renglonFechaIni.RowStyle.Alignment = HorizontalAlignment.Left;
                renglonDetalle.CreateCell(0).SetCellValue(renglon["AGENTE"].ToString());
                renglonDetalle.CreateCell(1).SetCellValue(renglon["Nombre"].ToString().Trim());
                //renglonFechaIni.RowStyle.Alignment = HorizontalAlignment.Right;

                ICell Pesos = renglonDetalle.CreateCell(2);
                Pesos.SetCellValue(Math.Round(Convert.ToDouble(renglon["Pesos"]), 2));
                Pesos.CellStyle = celdaEstilo2Decimales;

                ICell Prendas = renglonDetalle.CreateCell(3);
                Prendas.SetCellValue(Convert.ToInt32(renglon["PRENDAS"]));
                //Prendas.CellStyle = celdaEstilo2Decimales;

                ICell Promedio = renglonDetalle.CreateCell(4);
                Promedio.SetCellValue(Math.Round(Convert.ToDouble(renglon["Promedio"]), 2));
                Promedio.CellStyle = celdaEstilo2Decimales;
                
                        
                


                renglonIndex++;
            }
            #endregion

            renglonIndex++;
            //Totales
            IRow RowTotal = sheet.CreateRow(renglonIndex);

            // Total de pesos
            ICell Total = RowTotal.CreateCell(2);
            Total.CellFormula = string.Format("SUM(C6:C" + renglonIndex.ToString() + ")");
            Total.CellStyle = celdaEstilo2Decimales;

            //Total de prendas

            ICell TotalP = RowTotal.CreateCell(3);
            TotalP.CellFormula = string.Format("SUM(D6:D" + renglonIndex.ToString() + ")");
            TotalP.CellStyle = celdaEstilo2Decimales;

            //Total promedio
            
            ICell TotalProm = RowTotal.CreateCell(4);
            TotalProm.CellFormula = string.Format("C" + (renglonIndex + 1).ToString() + "/D" + (renglonIndex +1).ToString());
            TotalProm.CellStyle = celdaEstilo2Decimales;

            for (int i = 0; i < 6; i++)
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
