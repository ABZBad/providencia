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
    public class BitacoraComentarioClientes
    {
        public static DataTable getClientesSAE(int idUsuario)
        {

            String conStr = "";
            DataTable dtVend = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                conStr = dbContext.Database.Connection.ConnectionString;
            }
            SqlServerCommand cmd = new SqlServerCommand();
            cmd.Connection = DALUtil.GetConnection(conStr);
            cmd.ObjectName = "usp_getClientesSAEClasificacion";
            cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuario));
            dtVend = cmd.GetDataTable();
            cmd.Connection.Close();
            return dtVend;
        }
        public static int setAltaBitacoraComentarioCliente(int idUsuario, string claveCliente, string comentarios)
        {
            try
            {
                String conStr = "";
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "usp_AltaBitacoraComentarioCliente";
                cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuario));
                cmd.Parameters.Add(new SqlParameter("@claveCliente", claveCliente));
                cmd.Parameters.Add(new SqlParameter("@comentarios", comentarios));
                int i = cmd.Execute();
                cmd.Connection.Close();
                return i;
            }
            catch { return 0; }
        }
        public static DataTable getBitacora(DateTime fechaDesde, DateTime fechaHasta)
        {
            String conStr = "";
            DataTable dtBitacora = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                conStr = dbContext.Database.Connection.ConnectionString;
            }
            SqlServerCommand cmd = new SqlServerCommand();
            cmd.Connection = DALUtil.GetConnection(conStr);
            cmd.ObjectName = "usp_ConsultaBitacoraClientes";
            cmd.Parameters.Add(new SqlParameter("@fechaDesde", fechaDesde));
            cmd.Parameters.Add(new SqlParameter("@fechaHasta", fechaHasta));
            dtBitacora = cmd.GetDataTable();
            cmd.Connection.Close();
            return dtBitacora;
        }
        public static void GeneraArchivoExcel(string RutaYNombreArchivo, DataTable dtBitacora, DateTime FechaDesde, DateTime FechaHasta)
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
            celEncabezado.SetCellValue("Reporte de Seguimiento de Clientes");
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
            rngEncabezados.CreateCell(0).SetCellValue("Clave Cliente");
            rngEncabezados.CreateCell(1).SetCellValue("Cliente");
            rngEncabezados.CreateCell(2).SetCellValue("Comentarios");
            rngEncabezados.CreateCell(3).SetCellValue("Fecha");
            rngEncabezados.CreateCell(4).SetCellValue("Saldo");
            rngEncabezados.CreateCell(5).SetCellValue("Usuario");

            #endregion

            #region Detalle

            int iRenglonDetalle = 5;

            foreach (DataRow _dr in dtBitacora.Rows)
            {

                IRow renglonDetalle = sheet.CreateRow(iRenglonDetalle);

                ICell celdaClave= renglonDetalle.CreateCell(0);
                celdaClave.SetCellValue(_dr["CLAVE"].ToString());

                ICell celdaCliente = renglonDetalle.CreateCell(1);
                celdaCliente.SetCellValue(_dr["NOMBRE"].ToString());

                ICell celdaComentario = renglonDetalle.CreateCell(2);
                celdaComentario.SetCellValue(_dr["comentario"].ToString());

                ICell celdaFecha= renglonDetalle.CreateCell(3);
                celdaFecha.SetCellValue(DateTime.Parse(_dr["fecha"].ToString()).ToString("dd/MM/yyyy HH:mm:ss"));

                ICell celdaSaldo= renglonDetalle.CreateCell(4);
                celdaSaldo.SetCellValue(double.Parse(_dr["SALDO"].ToString()));                

                ICell celdaUsuario= renglonDetalle.CreateCell(5);
                celdaUsuario.SetCellValue(_dr["UsuarioNombre"].ToString());


                iRenglonDetalle++;
            }


            sheet.SetColumnWidth(0, ExcelNpoiUtil.AnchoColumna(80));
            sheet.SetColumnWidth(1, ExcelNpoiUtil.AnchoColumna(220));
            sheet.SetColumnWidth(2, ExcelNpoiUtil.AnchoColumna(500));
            sheet.SetColumnWidth(3, ExcelNpoiUtil.AnchoColumna(140));
            sheet.SetColumnWidth(4, ExcelNpoiUtil.AnchoColumna(120));
            sheet.SetColumnWidth(5, ExcelNpoiUtil.AnchoColumna(150));

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
