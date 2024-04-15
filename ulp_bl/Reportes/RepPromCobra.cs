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

namespace ulp_bl.Reportes
{
    public class RepPromCobra
    {
        public DataTable RegresaTabla(string Agente)
        {
            System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
            DataTable dataTablePedidos = new DataTable();
            using (var DbContext = new SIPReportesContext())
            {
                SqlServerCommand _cmd = new SqlServerCommand();
                _cmd.Connection = sm_dl.DALUtil.GetConnection(DbContext.Database.Connection.ConnectionString);
                _cmd.ObjectName = "usp_RepPromCobra";
                _cmd.Parameters.Add(new SqlParameter("@Agente", Agente));
                dataTablePedidos = _cmd.GetDataTable();
                _cmd.Connection.Close();

            }
            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.ElapsedMilliseconds);
            return dataTablePedidos;
        }

        public static void GeneraArchivoExcel(string RutaYNombreArchivo, DataTable TablaPedidos, string Agente)
        {

            HSSFWorkbook xlsWorkBook = new HSSFWorkbook();

            ISheet sheet = xlsWorkBook.CreateSheet("Hoja1");

            #region ENCABEZADOS

            IRow renglonNombreReporte = sheet.CreateRow(0);
            renglonNombreReporte.CreateCell(0).SetCellValue("PRONOSTICO DE COBRANZA DEL AGENTE " + Agente);

            //el reporte original no lleva encabezado pero aqui se indica por si se quisiera saber a que refiere cada columna PERO SE COMENTA

            //IRow renglonCabezera = sheet.CreateRow(1);
            //renglonCabezera.CreateCell(1).SetCellValue("CLAVE VENDEDOR");
            //renglonCabezera.CreateCell(2).SetCellValue("NOMBRE");
            //renglonCabezera.CreateCell(3).SetCellValue("SALDO");

            //el reporte original no lleva SUB encabezado pero aqui se indica por si se quisiera saber a que refiere cada columna del SUB ENCABEZADO PERO SE COMENTA
            //IRow renglonCabezera = sheet.CreateRow(2);
            //renglonCabezera.CreateCell(1).SetCellValue("FACTURA");
            //renglonCabezera.CreateCell(2).SetCellValue("FECHA ELABORA");
            //renglonCabezera.CreateCell(3).SetCellValue("FECHA VENCIMIENTO");
            //renglonCabezera.CreateCell(3).SetCellValue("SALDO");
            //renglonCabezera.CreateCell(3).SetCellValue("DIAS VENCIDA");

            #endregion

            int renglonIndex = 2; //basado en índice 0
            #region Ventas

            // AQUI COMIENZA else VERDADERO DETALLE delegate REPORTE


            ICellStyle celdaEstiloSUM = xlsWorkBook.CreateCellStyle();
            celdaEstiloSUM = xlsWorkBook.CreateCellStyle();
            celdaEstiloSUM.DataFormat = HSSFDataFormat.GetBuiltinFormat("#,##0.00_);(#,##0.00)");


            string CLIENTE = TablaPedidos.Rows[0]["CCLIE"].ToString();

            //string FACTURA = TablaPedidos.Rows[0]["FACTURA"].ToString();

            //IRow renglonCLIENTEA = sheet.CreateRow(renglonIndex); ;

            foreach (DataRow renglon in TablaPedidos.Rows)
            {
                if ((CLIENTE != renglon["CCLIE"].ToString()) || renglonIndex ==2)
                {
                    CLIENTE = renglon["CCLIE"].ToString();

                    renglonIndex++;
                    IRow renglonCLIENTE = sheet.CreateRow(renglonIndex); ;

                    renglonCLIENTE.CreateCell(0).SetCellValue(renglon["CCLIE"].ToString());
                    renglonCLIENTE.CreateCell(1).SetCellValue(renglon["NOMBRE"].ToString());
                    ICell saldoCabeceraCliente = renglonCLIENTE.CreateCell(2);
                    saldoCabeceraCliente.SetCellValue(Convert.ToDouble(renglon["SALDO"]));
                    saldoCabeceraCliente.CellStyle = celdaEstiloSUM;

                    renglonIndex++;

                    IRow renglonTitulo = sheet.CreateRow(renglonIndex);
                    renglonTitulo.CreateCell(0).SetCellValue("FACTURA");
                    renglonTitulo.CreateCell(1).SetCellValue("F.ELAB");
                    renglonTitulo.CreateCell(2).SetCellValue("F.VENC");
                    renglonTitulo.CreateCell(3).SetCellValue("SALDO");
                    renglonTitulo.CreateCell(4).SetCellValue("DIAS VENCIDA");

                    renglonIndex++;
                }              

                // Detalle
                IRow renglonDetalle = sheet.CreateRow(renglonIndex);
                renglonDetalle.CreateCell(0).SetCellValue(renglon["FACTURA"].ToString());
                renglonDetalle.CreateCell(1).SetCellValue(((DateTime)renglon["F_ELAB"]).ToShortDateString());
                renglonDetalle.CreateCell(2).SetCellValue(((DateTime)renglon["F_VENC"]).ToShortDateString());
                
                ICell SALDOS = renglonDetalle.CreateCell(3);
                SALDOS.SetCellValue((double)renglon["CARGO"]);
                SALDOS.CellStyle = celdaEstiloSUM;

                renglonDetalle.CreateCell(4).SetCellValue(Convert.ToDouble(renglon["DIAS_VENC"].ToString().Trim()));

                renglonIndex++;
            }
            #endregion

            for (int i = 0; i < 7; i++)
            {
                sheet.SetColumnWidth(i, ExcelNpoiUtil.AnchoColumna(80));
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
