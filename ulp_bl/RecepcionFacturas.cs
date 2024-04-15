using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using sm_dl;
using sm_dl.SqlServer;
using ulp_dl;
//using ulp_dl.aspel_prod30;
using ulp_dl.aspel_sae80;
using ulp_dl.SIPNegocio;
using System.Data.SqlClient;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using ulp_bl.Utiles;
using System.IO;

namespace ulp_bl
{
    public class RecepcionFacturas
    {
        public static DataTable getFacturas()
        {
            String conStr = "";
            DataTable dtVend = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                conStr = dbContext.Database.Connection.ConnectionString;
            }
            SqlServerCommand cmd = new SqlServerCommand();
            cmd.Connection = DALUtil.GetConnection(conStr);
            cmd.ObjectName = "usp_ConsultaFacturasRecepcion";
            dtVend = cmd.GetDataTable();
            cmd.Connection.Close();
            return dtVend;
        }
        public static int setAltaRecepcion(List<String> lstFacturas, String area, String persona, DateTime fechaCaptura, DateTime fechaRecepcion, Boolean esRecepcion, int idUsuario)
        {
            int res = 0;
            String conStr = "";
            DataTable dtVend = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                conStr = dbContext.Database.Connection.ConnectionString;
            }
            SqlServerCommand cmd = new SqlServerCommand();
            cmd.Connection = DALUtil.GetConnection(conStr);
            cmd.ObjectName = "usp_setAltaRecepcionFactura";
            foreach (String cve_doc in lstFacturas)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@area", area));
                cmd.Parameters.Add(new SqlParameter("@personaEntrega", persona));
                cmd.Parameters.Add(new SqlParameter("@fechaCaptura", fechaCaptura));
                cmd.Parameters.Add(new SqlParameter("@fechaRecepcion", fechaRecepcion));
                cmd.Parameters.Add(new SqlParameter("@esRecepcion", esRecepcion));
                cmd.Parameters.Add(new SqlParameter("@CVE_DOC", cve_doc));
                cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuario));
                res += cmd.Execute();
            }
            return res;
        }
        public static void setAltaRecepcionDetalle(int idRecepcion, List<String> ListaFacturas)
        {
            String conStr = "";
            DataTable dtFacturaDetalle = new DataTable();
            dtFacturaDetalle.Columns.Add("idRecepcionFactura", typeof(int));
            dtFacturaDetalle.Columns.Add("CVE_DOC", typeof(String));

            foreach (String _factura in ListaFacturas)
            {
                DataRow dr = dtFacturaDetalle.NewRow();
                dr["idRecepcionFactura"] = idRecepcion;
                dr["CVE_DOC"] = _factura;
                dtFacturaDetalle.Rows.Add(dr);
            }

            using (var dbContext = new SIPNegocioContext())
            {
                conStr = dbContext.Database.Connection.ConnectionString;
            }
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                SqlBulkCopy bulk = new SqlBulkCopy(
                conn,
                SqlBulkCopyOptions.TableLock |
                SqlBulkCopyOptions.FireTriggers |
                SqlBulkCopyOptions.UseInternalTransaction |
                SqlBulkCopyOptions.KeepIdentity,
                null);
                bulk.DestinationTableName = "RecepcionFacturaDetalle";
                bulk.ColumnMappings.Add("idRecepcionFactura", "idRecepcionFactura");
                bulk.ColumnMappings.Add("CVE_DOC", "CVE_DOC");

                conn.Open();
                bulk.WriteToServer(dtFacturaDetalle);
                conn.Close();
            }
        }
        public static DataTable getRecepcion(DateTime fechaDesde, DateTime fechaHasta)
        {
            String conStr = "";
            DataTable dtBitacora = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                conStr = dbContext.Database.Connection.ConnectionString;
            }
            SqlServerCommand cmd = new SqlServerCommand();
            cmd.Connection = DALUtil.GetConnection(conStr);
            cmd.ObjectName = "usp_ConsultaRecepcionFacturas";
            cmd.Parameters.Add(new SqlParameter("@fechaDesde", fechaDesde));
            cmd.Parameters.Add(new SqlParameter("@fechaHasta", fechaHasta));
            dtBitacora = cmd.GetDataTable();
            cmd.Connection.Close();
            return dtBitacora;
        }
        public static void GeneraArchivoExcel(string RutaYNombreArchivo, DataTable dtRecepcion, DateTime FechaDesde, DateTime FechaHasta)
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

            #endregion

            #region Encabezado

            IRow rngEncabezado = sheet.CreateRow(0);
            ICell celEncabezado = rngEncabezado.CreateCell(0);
            celEncabezado.SetCellValue("Reporte de Recepción de Facturas");
            celEncabezado.CellStyle = fmtCentrado;

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

            #region Encabezados

            IRow rngEncabezados = sheet.CreateRow(4);
            //rngEncabezados.CreateCell(0).SetCellValue("Id.");
            rngEncabezados.CreateCell(1).SetCellValue("Factura");
            rngEncabezados.CreateCell(2).SetCellValue("Pedido");
            rngEncabezados.CreateCell(3).SetCellValue("F. Factura");
            rngEncabezados.CreateCell(4).SetCellValue("Clave Cliente");
            rngEncabezados.CreateCell(5).SetCellValue("Cliente");
            rngEncabezados.CreateCell(6).SetCellValue("F. Recepción");
            rngEncabezados.CreateCell(7).SetCellValue("Área Entrega");
            rngEncabezados.CreateCell(8).SetCellValue("Persona Entrega");
            rngEncabezados.CreateCell(9).SetCellValue("F. Entrega");
            rngEncabezados.CreateCell(10).SetCellValue("Diferencia");


            #endregion

            #region Detalle

            int iRenglonDetalle = 5;

            foreach (DataRow _dr in dtRecepcion.Rows)
            {

                IRow renglonDetalle = sheet.CreateRow(iRenglonDetalle);

                //renglonDetalle.CreateCell(0).SetCellValue(int.Parse(_dr["id"].ToString()));
                renglonDetalle.CreateCell(1).SetCellValue(_dr["factura"].ToString());
                renglonDetalle.CreateCell(2).SetCellValue(_dr["pedido"].ToString());
                renglonDetalle.CreateCell(3).SetCellValue(DateTime.Parse(_dr["fechaFactura"].ToString()).ToString("dd/MM/yyyy"));
                renglonDetalle.CreateCell(4).SetCellValue(_dr["claveCliente"].ToString());
                renglonDetalle.CreateCell(5).SetCellValue(_dr["nombreCliente"].ToString());
                renglonDetalle.CreateCell(6).SetCellValue(DateTime.Parse(_dr["fechaRecepcion"].ToString()).ToString("dd/MM/yyyy"));
                renglonDetalle.CreateCell(7).SetCellValue(_dr["area"].ToString());
                renglonDetalle.CreateCell(8).SetCellValue(_dr["persona"].ToString());
                renglonDetalle.CreateCell(9).SetCellValue(_dr["fechaEntrega"].ToString() == "" ? "" : DateTime.Parse(_dr["fechaEntrega"].ToString()).ToString("dd/MM/yyyy"));
                renglonDetalle.CreateCell(10).SetCellValue(int.Parse(_dr["diferencia"].ToString()));




                iRenglonDetalle++;
            }

            for (int i = 0; i <= 10; i++)
            {
                sheet.AutoSizeColumn(i);
            }

            
            #endregion


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
