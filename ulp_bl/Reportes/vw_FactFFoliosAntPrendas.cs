using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using sm_dl;
using sm_dl.SqlServer;
using ulp_bl.Utiles;
using ulp_dl.SIPReportes;
using ulp_dl;

namespace ulp_bl.Reportes
{
    public class vw_FactFoliosAntPrendas
    {

        public Enumerados.TipoReporteFoliosAnt TipoReporte { get; set; }



        public DataTable RegresaTablaFacturacionAnt(Enumerados.TipoReporteFolios TipoReporteFolios, Enumerados.AreasEmpresa TipoArea, DateTime FechaInicial, DateTime FechaFinal)
        {
            DataTable dataTableResult = new DataTable();
            using (var dbContext = new SIPReportesContext())
            {

                if (TipoReporte == Enumerados.TipoReporteFoliosAnt.Credito)
                {
                    var cveDoc = new string[] { };
                    if (TipoArea == Enumerados.AreasEmpresa.Contabilidad)
                        cveDoc = new string[] { "A", "C", "D", "F" };
                    else
                        cveDoc = new string[] { "A", "C", "D" };

                    var datosFacturacion = from f in dbContext.vw_FactFFoliosAntPrendas where cveDoc.Contains(f.CVE_DOC.Substring(0, 1)) && (f.FECHA_DOC >= FechaInicial.Date && f.FECHA_DOC <= FechaFinal.Date) orderby f.FOLIO select f;
                    dataTableResult = Linq2DataTable.CopyToDataTable(datosFacturacion);
                }
                if (TipoReporte == Enumerados.TipoReporteFoliosAnt.Mostrador)
                {
                    var listaDocs = new List<string>();
                    if (TipoReporteFolios == Enumerados.TipoReporteFolios.Anteriores)
                    {
                        listaDocs.AddRange(new string[] { "M", "B" });
                    }
                    else if (TipoReporteFolios == Enumerados.TipoReporteFolios.Nuevos)
                    {
                        listaDocs.AddRange(new string[] { "B" });
                    }
                    var cveDoc = listaDocs.ToArray();
                    var datosFacturacion = from f in dbContext.vw_FactFFoliosAntPrendas where cveDoc.Contains(f.CVE_DOC.Substring(0, 1)) && (f.FECHA_DOC >= FechaInicial.Date && f.FECHA_DOC <= FechaFinal.Date) orderby f.FOLIO select f;
                    dataTableResult = Linq2DataTable.CopyToDataTable(datosFacturacion);
                }
                if (TipoReporte == Enumerados.TipoReporteFoliosAnt.Ambos)
                {
                    var datosFacturacion = from f in dbContext.vw_FactFFoliosAntPrendas where (f.CVE_DOC.Substring(0, 1) != (TipoArea == Enumerados.AreasEmpresa.Contabilidad ? "" : "F") && f.CVE_DOC.Substring(0, 1) != "G") && (f.FECHA_DOC >= FechaInicial.Date && f.FECHA_DOC <= FechaFinal.Date) orderby f.FOLIO select f;
                    dataTableResult = Linq2DataTable.CopyToDataTable(datosFacturacion);

                    /*
                    //SqlServerSelectCommand cmd = new SqlServerSelectCommand();
                    cmd.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                    cmd.ObjectName = "select * from vw_FactFFoliosAntPrendas where convert(date,FECHA_DOC) between @fecha_inicial and @fecha_final";
                    cmd.Parameters.Add(new SqlParameter("@fecha_inicial", FechaInicial));
                    cmd.Parameters.Add(new SqlParameter("@fecha_final", FechaFinal));
                    dataTableResult = cmd.GetDataTable();
                    */
                }
            }
            return dataTableResult;

        }
        public DataTable RegresaTablaNotasCredito(Enumerados.TipoReporteFolios TipoReporteFolios, DateTime FechaInicial, DateTime FechaFinal)
        {
            DataTable dataTableResult = new DataTable();
            using (var dbContext = new SIPReportesContext())
            {

                if (TipoReporte == Enumerados.TipoReporteFoliosAnt.Credito)
                {
                    var listaDocs = new List<string>();

                    if (TipoReporteFolios == Enumerados.TipoReporteFolios.Anteriores)
                    {
                        listaDocs.AddRange(new string[] { "A", "C", "D" });
                    }
                    else if (TipoReporteFolios == Enumerados.TipoReporteFolios.Nuevos)
                    {
                        listaDocs.AddRange(new string[] { "C", "D" });
                    }
                    var cveDoc = listaDocs.ToArray();
                    var datosFacturacion = from f in dbContext.vw_FactDFoliosAntPrendas where cveDoc.Contains(f.CVE_DOC.Substring(0, 1)) && (f.FECHA_DOC >= FechaInicial.Date && f.FECHA_DOC <= FechaFinal.Date) orderby f.FOLIO select f;
                    dataTableResult = Linq2DataTable.CopyToDataTable(datosFacturacion);
                }
                if (TipoReporte == Enumerados.TipoReporteFoliosAnt.Mostrador)
                {
                    var listaDocs = new List<string>();
                    if (TipoReporteFolios == Enumerados.TipoReporteFolios.Anteriores)
                    {
                        listaDocs.AddRange(new string[] { "M", "C" });
                    }
                    else if (TipoReporteFolios == Enumerados.TipoReporteFolios.Nuevos)
                    {
                        listaDocs.AddRange(new string[] { "C" });
                    }
                    var cveDoc = listaDocs.ToArray();
                    var datosFacturacion = from f in dbContext.vw_FactDFoliosAntPrendas where cveDoc.Contains(f.CVE_DOC.Substring(0, 1)) && (f.FECHA_DOC >= FechaInicial.Date && f.FECHA_DOC <= FechaFinal.Date) orderby f.FOLIO select f;
                    dataTableResult = Linq2DataTable.CopyToDataTable(datosFacturacion);
                }
                if (TipoReporte == Enumerados.TipoReporteFoliosAnt.Ambos)
                {
                    var datosFacturacion = from f in dbContext.vw_FactDFoliosAntPrendas where f.FECHA_DOC >= FechaInicial.Date && f.FECHA_DOC <= FechaFinal.Date orderby f.FOLIO select f;
                    dataTableResult = Linq2DataTable.CopyToDataTable(datosFacturacion);
                }
            }
            return dataTableResult;
        }
        public DataTable RegresaTablaNVAnt(DateTime FechaInicial, DateTime FechaFinal)
        {
            DataTable dataTableResult = new DataTable();
            using (var dbContext = new SIPReportesContext())
            {
                
                var datosNV = from f in dbContext.vw_FactNVFoliosAntPrendas where (f.FECHA_DOC >= FechaInicial.Date && f.FECHA_DOC <= FechaFinal.Date) orderby f.FOLIO select f;
                dataTableResult = Linq2DataTable.CopyToDataTable(datosNV);
                
                /*
                //SqlServerSelectCommand cmd = new SqlServerSelectCommand();
                cmd.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmd.ObjectName = "select * from vw_FactFFoliosAntPrendas where convert(date,FECHA_DOC) between @fecha_inicial and @fecha_final";
                cmd.Parameters.Add(new SqlParameter("@fecha_inicial", FechaInicial));
                cmd.Parameters.Add(new SqlParameter("@fecha_final", FechaFinal));
                dataTableResult = cmd.GetDataTable();
                */
            }
            return dataTableResult;

        }
        public static void GeneraArchivoExcel(string RutaYNombreArchivo, DataTable TablaFacturacion, DataTable TablaNotasCredito)
        {
            HSSFWorkbook xlsWorkBook = new HSSFWorkbook();




            ISheet sheet = xlsWorkBook.CreateSheet("Hoja1");

            #region ENCABEZADOS

            IRow renglonCabezera = sheet.CreateRow(0);
            renglonCabezera.CreateCell(0).SetCellValue("S");
            renglonCabezera.CreateCell(1).SetCellValue("FECHA");
            renglonCabezera.CreateCell(2).SetCellValue("FACTURA");
            renglonCabezera.CreateCell(3).SetCellValue("RAZÓN SOCIAL");
            renglonCabezera.CreateCell(4).SetCellValue("VEND");
            renglonCabezera.CreateCell(5).SetCellValue("SUBTOTAL");
            renglonCabezera.CreateCell(6).SetCellValue("IVA");
            renglonCabezera.CreateCell(7).SetCellValue("TOTAL");
            renglonCabezera.CreateCell(8).SetCellValue("PRENDAS");
            renglonCabezera.CreateCell(9).SetCellValue("CTE");

            #endregion


            int renglonTotalesTabla1 = 0;
            int renglonTotalesTabla2 = 0;
            int renglonExcel = 2;
            int renglonInicial = renglonExcel;
            int totalRegistros = 0;
            IRow renlonNpoiExcel;
            ICell celdaNpoi;
            ICellStyle celdaEstilo2Decimales = xlsWorkBook.CreateCellStyle();
            celdaEstilo2Decimales = xlsWorkBook.CreateCellStyle();
            celdaEstilo2Decimales.DataFormat = HSSFDataFormat.GetBuiltinFormat("#,##0.00_);(#,##0.00)");

            ICellStyle celdaEstilo0Decimales = xlsWorkBook.CreateCellStyle();
            celdaEstilo0Decimales = xlsWorkBook.CreateCellStyle();
            celdaEstilo0Decimales.DataFormat = HSSFDataFormat.GetBuiltinFormat("#,##0_);(#,##0)");

            DataTable dataTableReporte;
            for (int j = 0; j < 2; j++)
            {
                if (j == 0)
                {
                    dataTableReporte = TablaFacturacion;
                }
                else
                {
                    totalRegistros = 0;
                    renglonExcel += 4;
                    renglonInicial = renglonExcel;
                    dataTableReporte = TablaNotasCredito;
                }
                #region DETALLE DE FACTURACION

                foreach (DataRow renglonFactura in dataTableReporte.Rows)
                {
                    totalRegistros++;

                    renlonNpoiExcel = sheet.CreateRow(renglonExcel);
                    renlonNpoiExcel.CreateCell(0).SetCellValue(renglonFactura["STATUS"].ToString());
                    renlonNpoiExcel.CreateCell(1).SetCellValue(Convert.ToDateTime(renglonFactura["FECHA_DOC"]).ToShortDateString());
                    renlonNpoiExcel.CreateCell(2).SetCellValue(renglonFactura["CVE_DOC"].ToString());
                    renlonNpoiExcel.CreateCell(3).SetCellValue(renglonFactura["NOMBRE"].ToString());
                    renlonNpoiExcel.CreateCell(4).SetCellValue(renglonFactura["CVE_VEND"].ToString());

                    ICell celSubTotal = renlonNpoiExcel.CreateCell(5);
                    celSubTotal.SetCellValue(Math.Round(Convert.ToDouble(renglonFactura["SUBTOTAL"]), 2));
                    celSubTotal.CellStyle = celdaEstilo2Decimales;

                    ICell celIva = renlonNpoiExcel.CreateCell(6);
                    celIva.SetCellValue(Math.Round(Convert.ToDouble(renglonFactura["IVA"]), 2));
                    celIva.CellStyle = celdaEstilo2Decimales;

                    ICell celTotal = renlonNpoiExcel.CreateCell(7);
                    celTotal.SetCellValue(Math.Round(Convert.ToDouble(renglonFactura["TOTAL"]), 2));
                    celTotal.CellStyle = celdaEstilo2Decimales;

                    ICell celPrendas = renlonNpoiExcel.CreateCell(8);
                    celPrendas.SetCellValue(int.Parse(Math.Round(decimal.Parse(renglonFactura["PRENDAS"].ToString()), 0).ToString()));

                    ICell celCliente = renlonNpoiExcel.CreateCell(9);
                    celCliente.SetCellValue(renglonFactura["CCLIE"].ToString().Trim());

                    renglonExcel++;

                }
                #endregion
                #region SUMATORIAS POR TABLA
                IRow totalRenglon = sheet.CreateRow(renglonExcel);
                totalRenglon.CreateCell(0).SetCellValue("T");
                totalRenglon.CreateCell(1).SetCellValue(totalRegistros);

                renlonNpoiExcel = sheet.CreateRow(renglonExcel + 1);
                ICell celSubTotalSumatoria = renlonNpoiExcel.CreateCell(5);
                celSubTotalSumatoria.CellFormula = string.Format("SUM(F{0}:F{1})", renglonInicial + 1, renglonExcel);
                celSubTotalSumatoria.CellStyle = celdaEstilo2Decimales;

                ICell celIvaSumatoria = renlonNpoiExcel.CreateCell(6);
                celIvaSumatoria.CellFormula = string.Format("SUM(G{0}:G{1})", renglonInicial + 1, renglonExcel);
                celIvaSumatoria.CellStyle = celdaEstilo2Decimales;

                ICell celTotalSumatoria = renlonNpoiExcel.CreateCell(7);
                celTotalSumatoria.CellFormula = string.Format("SUM(H{0}:H{1})", renglonInicial + 1, renglonExcel);
                celTotalSumatoria.CellStyle = celdaEstilo2Decimales;

                ICell celPrendaSumatoria = renlonNpoiExcel.CreateCell(8);
                celPrendaSumatoria.CellFormula = string.Format("SUM(I{0}:I{1})", renglonInicial + 1, renglonExcel);
                celPrendaSumatoria.CellStyle = celdaEstilo0Decimales;

                if (j == 0)
                {
                    renglonTotalesTabla1 = renglonExcel + 2;
                }
                else
                {
                    renglonTotalesTabla2 = renglonExcel + 2;
                }

                #endregion

            }
            IRow renlonNpoiDif = sheet.CreateRow(renglonExcel + 4);
            ICell celDiferencia = renlonNpoiDif.CreateCell(5);
            celDiferencia.CellFormula = string.Format("F{0}-F{1}", renglonTotalesTabla1, renglonTotalesTabla2);
            celDiferencia.CellStyle = celdaEstilo2Decimales;

            ICell celDiferenciaPrendas = renlonNpoiDif.CreateCell(8);
            celDiferenciaPrendas.CellFormula = string.Format("I{0}-I{1}", renglonTotalesTabla1, renglonTotalesTabla2);
            celDiferenciaPrendas.CellStyle = celdaEstilo0Decimales;


            for (int i = 0; i < 10; i++)
            {
                sheet.AutoSizeColumn(i);
            }

            sheet.FitToPage = false;
            sheet.PrintSetup.Scale = 50;

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
