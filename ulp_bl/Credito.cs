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
    public class Credito
    {
        public static DataTable getClientesSAE()
        {

            String conStr = "";
            DataTable dtClientes = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                conStr = dbContext.Database.Connection.ConnectionString;
            }
            SqlServerCommand cmd = new SqlServerCommand();
            cmd.Connection = DALUtil.GetConnection(conStr);
            cmd.ObjectName = "usp_ConsultaClientesExcedentesCredito";
            dtClientes = cmd.GetDataTable();
            cmd.Connection.Close();
            return dtClientes;
        }
        public static void GeneraArchivoExcel(string RutaYNombreArchivo, DataTable dtClientes)
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
            celEncabezado.SetCellValue("Reporte de Clientes con excedente de crédito");
            celEncabezado.CellStyle = fmtCentrado;

            //se combinan las celdas

            CellRangeAddress range = new CellRangeAddress(0, 0, 0, 6);
            sheet.AddMergedRegion(range);


            #endregion

            #region Se escriben los rangos de fecha, cliente y DS CMP

            IRow rngEmitidoDel = sheet.CreateRow(1); //renglón: "Emitido del.."
            rngEmitidoDel.CreateCell(1).SetCellValue(string.Format("Emitido el      {0}", DateTime.Now.ToShortDateString()));


            #endregion

            #region Encabezados

            IRow rngEncabezados = sheet.CreateRow(4);
            rngEncabezados.CreateCell(0).SetCellValue("Clave");
            rngEncabezados.CreateCell(1).SetCellValue("Nombre");
            rngEncabezados.CreateCell(2).SetCellValue("Línea Crédito");
            rngEncabezados.CreateCell(3).SetCellValue("Saldo");
            rngEncabezados.CreateCell(4).SetCellValue("Diferencia");



            #endregion

            #region Detalle

            int iRenglonDetalle = 5;

            foreach (DataRow _dr in dtClientes.Rows)
            {

                IRow renglonDetalle = sheet.CreateRow(iRenglonDetalle);

                renglonDetalle.CreateCell(0).SetCellValue(_dr["clave"].ToString());
                renglonDetalle.CreateCell(1).SetCellValue(_dr["nombre"].ToString());
                renglonDetalle.CreateCell(2).SetCellValue(double.Parse(_dr["lineaCredito"].ToString()));
                renglonDetalle.CreateCell(3).SetCellValue(double.Parse(_dr["saldo"].ToString()));
                renglonDetalle.CreateCell(4).SetCellValue(double.Parse(_dr["diferencia"].ToString()));
                
                iRenglonDetalle++;
            }

            for (int i = 0; i <= 4; i++)
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
