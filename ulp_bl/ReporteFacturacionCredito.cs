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
using ulp_dl.aspel_sae80;

namespace ulp_bl
{
    public class ReporteFacturacionCredito
    {
        public static DataTable RegresaSeriesFactura()
        {
            string conStr = "";
            DataTable dt = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                conStr = dbContext.Database.Connection.ConnectionString;
            }

            SqlServerCommand cmd = new SqlServerCommand();
            cmd.Connection = DALUtil.GetConnection(conStr);
            cmd.ObjectName = "usp_ConsultaSeriesFactura";
            dt = cmd.GetDataTable();
            return dt;
        }
        public static DataTable RegresaReporteFacturacion(DateTime fechaDesde, DateTime fechaHasta, String serie)
        {
            string conStr = "";
            DataTable dt = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                conStr = dbContext.Database.Connection.ConnectionString;
            }

            SqlServerCommand cmd = new SqlServerCommand();
            cmd.Connection = DALUtil.GetConnection(conStr);
            cmd.ObjectName = "usp_ConsultaFacturacion";
            cmd.Parameters.Add(new SqlParameter("@fechaDesde", fechaDesde));
            cmd.Parameters.Add(new SqlParameter("@fechaHasta", fechaHasta));
            cmd.Parameters.Add(new SqlParameter("@serie", serie));
            dt = cmd.GetDataTable();
            return dt;
        }
        public static void GeneraArchivoExcel(string RutaYNombreArchivo, DataTable dtFacturacion, DateTime FechaDesde, DateTime FechaHasta)
        {
            HSSFWorkbook xlsWorkBook = new HSSFWorkbook();
            ISheet sheet = xlsWorkBook.CreateSheet("Hoja1");

            //formatos
            #region Formatos


            //formato de miles SIN decimales
            ICellStyle fmtoMiles = xlsWorkBook.CreateCellStyle();
            fmtoMiles.DataFormat = ExcelNpoiUtil.FormatoCelda(ref xlsWorkBook, "#,##0");

            //formato para Texto Centrado
            ICellStyle fmtCentrado = xlsWorkBook.CreateCellStyle();
            fmtCentrado.Alignment = HorizontalAlignment.Center;

            //formato para texto en Negritas
            ICellStyle fmtNegritas = xlsWorkBook.CreateCellStyle();

            ICellStyle fmtPesos = xlsWorkBook.CreateCellStyle();
            fmtPesos.DataFormat = ExcelNpoiUtil.FormatoCelda(ref xlsWorkBook, "$#,###.##");

            ICellStyle fmtTitulo = xlsWorkBook.CreateCellStyle();
            IFont fontTitulo = xlsWorkBook.CreateFont();
            fontTitulo.Boldweight = 18;
            fontTitulo.FontHeightInPoints = 16;
            fmtTitulo.Alignment = HorizontalAlignment.Center;
            fmtTitulo.SetFont(fontTitulo);


            #endregion

            #region Encabezado

            IRow rngEncabezado = sheet.CreateRow(0);
            ICell celEncabezado = rngEncabezado.CreateCell(0);
            celEncabezado.SetCellValue("Reporte de facturas emitidas VS recibidas");
            celEncabezado.CellStyle = fmtTitulo;

            //se combinan las celdas

            CellRangeAddress range = new CellRangeAddress(0, 0, 0, 3);
            sheet.AddMergedRegion(range);


            #endregion

            #region Se escriben los rangos de fecha, cliente y DS CMP

            IRow rngEmitidoDel = sheet.CreateRow(1); //renglón: "Emitido del.."
            rngEmitidoDel.CreateCell(1).SetCellValue(string.Format("Emitido del      {0}", FechaDesde.ToShortDateString()));

            IRow rngEmitidoAl = sheet.CreateRow(2); //renglón: "Al.."
            rngEmitidoAl.CreateCell(1).SetCellValue(string.Format("Al                   {0}", FechaHasta.ToShortDateString()));


            #endregion            

            #region TOTALES
            int j = 8;

            var totales = (from row in dtFacturacion.AsEnumerable() where row.Field<String>("recibidaPorCredito") == "NO" select new { nombre = row.Field<String>("enPosesionDe") }).Distinct();


            int totalCapturadas = 0, totalCredito = 0, totalFacturas = 0;

            foreach (var nombre in totales.ToList())
            {
                if (nombre.nombre != "")
                {
                    IRow rngPersona = sheet.CreateRow(j);
                    rngPersona.CreateCell(0).SetCellValue(nombre.nombre.ToString());
                    var suma = (from row in dtFacturacion.AsEnumerable() where row.Field<String>("enPosesionDe") == nombre.nombre && row.Field<String>("recibidaPorCredito") == "NO" select row).ToList();
                    totalCapturadas += suma.Count();
                    rngPersona.CreateCell(1).SetCellValue(suma.Count());
                    j++;
                }
            }

            //RECIBIDAS POR CREDITO
            IRow rngCredito = sheet.CreateRow(j);
            rngCredito.CreateCell(0).SetCellValue("Crédito");
            var sumaCredito = (from row in dtFacturacion.AsEnumerable() where row.Field<String>("recibidaPorCredito") == "SI" select row).ToList();
            totalCredito = sumaCredito.Count();
            rngCredito.CreateCell(1).SetCellValue(totalCredito);
            j++;

            //DIFERENCIA
            IRow rngDiferencia2 = sheet.CreateRow(j);
            rngDiferencia2.CreateCell(0).SetCellValue("Diferencia");
            rngDiferencia2.CreateCell(1).SetCellValue(dtFacturacion.Rows.Count - totalCredito - totalCapturadas);
            j++;
            //TOTAL DE FACTURAS
            IRow rngTotalFacturas = sheet.CreateRow(j);
            rngTotalFacturas.CreateCell(0).SetCellValue("Total Facturas");
            rngTotalFacturas.CreateCell(1).SetCellValue(dtFacturacion.Rows.Count);
            j++;
            #endregion

            #region Encabezados

            

            IRow rngEncabezados = sheet.CreateRow(j+1);
            rngEncabezados.CreateCell(0).SetCellValue("Factura");
            rngEncabezados.CreateCell(1).SetCellValue("Cliente");
            rngEncabezados.CreateCell(2).SetCellValue("F. Elab.");
            rngEncabezados.CreateCell(3).SetCellValue("Total");
            rngEncabezados.CreateCell(4).SetCellValue("En posesión de");
            j++
                ;
            #endregion

            #region Detalle

            int iRenglonDetalle = j + 1;

            decimal totalFacturado = 0, totalRecibido = 0;

            foreach (DataRow _dr in dtFacturacion.Rows)
            {
                totalFacturado += decimal.Parse(_dr["MONTO"].ToString());
                totalRecibido += decimal.Parse(_dr["totalRecibido"].ToString());

                if (_dr["recibidaPorCredito"].ToString() == "NO")
                {
                    IRow renglonDetalle = sheet.CreateRow(iRenglonDetalle);

                    renglonDetalle.CreateCell(0).SetCellValue(_dr["FACTURA"].ToString());
                    renglonDetalle.CreateCell(1).SetCellValue(_dr["CLIENTE"].ToString());
                    renglonDetalle.CreateCell(2).SetCellValue(DateTime.Parse(_dr["FECHA_ELABORACION"].ToString()).ToString("dd/MM/yyyy"));
                    renglonDetalle.CreateCell(3).SetCellValue(double.Parse(_dr["MONTO"].ToString())); renglonDetalle.Cells[3].CellStyle = fmtPesos;
                    renglonDetalle.CreateCell(4).SetCellValue(_dr["enPosesionDe"].ToString());

                    iRenglonDetalle++;
                }
            }

            IRow rngTotal = sheet.CreateRow(4);
            rngTotal.CreateCell(0).SetCellValue("TOTAL FACTURADO");
            rngTotal.CreateCell(1).SetCellValue(double.Parse(totalFacturado.ToString())); rngTotal.Cells[1].CellStyle = fmtPesos;

            IRow rngSubTotal = sheet.CreateRow(5);
            rngSubTotal.CreateCell(0).SetCellValue("TOTAL RECIBIDO");
            rngSubTotal.CreateCell(1).SetCellValue(double.Parse(totalRecibido.ToString())); rngSubTotal.Cells[1].CellStyle = fmtPesos;

            IRow rngDiferencia = sheet.CreateRow(6);
            rngDiferencia.CreateCell(0).SetCellValue("DIFERENCIA");
            rngDiferencia.CreateCell(1).SetCellValue(double.Parse((totalFacturado - totalRecibido).ToString())); rngDiferencia.Cells[1].CellStyle = fmtPesos;


            #endregion



            for (int i = 0; i <= 4; i++)
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
