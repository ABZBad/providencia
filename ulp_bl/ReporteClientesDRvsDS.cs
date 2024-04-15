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


namespace ulp_bl
{
    public class ReporteClientesDRvsDS
    {
        public static DataTable GetDSvsDRClientesPorEjecutivo(String Agente, DateTime Fecha, ref Exception Ex)
        {
            String conStr = "";
            DataTable dtArticulosCliente = new DataTable();
            try
            {
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "usp_RepDRyDS";
                cmd.Parameters.Add(new SqlParameter("@Agente", Agente));
                cmd.Parameters.Add(new SqlParameter("@Fecha", Fecha));                
                dtArticulosCliente = cmd.GetDataTable();
                cmd.Connection.Close();
                Ex = null;
                return dtArticulosCliente;
            }
            catch (Exception ex)
            {
                Ex = ex;
                return null;
            }
        }
        public static void GeneraArchivoExcel(string RutaYNombreArchivo, DataTable dtDSvsDR, String Agente, DateTime Fecha)
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
            celEncabezado.SetCellValue("Reporte de DS y DR de Clientes por Agente");
            celEncabezado.CellStyle = fmtCentrado;

            //se combinan las celdas

            CellRangeAddress range = new CellRangeAddress(0, 0, 0, 3);
            sheet.AddMergedRegion(range);


            #endregion

            #region Se escriben los rangos de fecha, cliente y DS CMP

            IRow rngEmitidoDel = sheet.CreateRow(1); //renglón: "Emitido del.."
            rngEmitidoDel.CreateCell(1).SetCellValue(string.Format("Emitido el      {0}", Fecha.ToShortDateString()));

            IRow rngAgente = sheet.CreateRow(2); //renglón: "Emitido del.."
            rngAgente.CreateCell(1).SetCellValue(string.Format("Agente      {0}", Agente));
            #endregion

            #region Encabezados



            #endregion

            #region Detalle

            int iRenglonDetalle = 4;            
            String cliente, clienteAux = String.Empty;           
            

            //ENCABEZADOS DE DETALLE
            IRow renglonEncabezados = sheet.CreateRow(iRenglonDetalle);

            ICell celdaEncAnio = renglonEncabezados.CreateCell(0);
            celdaEncAnio.SetCellValue("CLIENTE");

            ICell celdaEncMes = renglonEncabezados.CreateCell(1);
            celdaEncMes.SetCellValue("DR");

            ICell celdaEncArticulo = renglonEncabezados.CreateCell(2);
            celdaEncArticulo.SetCellValue("DS");
            iRenglonDetalle++;

            foreach (DataRow _dr in dtDSvsDR.Rows)
            {
                IRow renglonDetalle = sheet.CreateRow(iRenglonDetalle);

                ICell celdaDetalleCliente = renglonDetalle.CreateCell(0);
                celdaDetalleCliente.SetCellValue(_dr["NOMBRE"].ToString());

                ICell celdaDetalleDR = renglonDetalle.CreateCell(1);
                celdaDetalleDR.SetCellValue(float.Parse(_dr["DR"].ToString()));

                ICell celdaDetalleDS = renglonDetalle.CreateCell(2);
                celdaDetalleDS.SetCellValue(float.Parse(_dr["DS"].ToString()));

                

                iRenglonDetalle++;
            }

           

            sheet.SetColumnWidth(0, ExcelNpoiUtil.AnchoColumna(450));
            sheet.SetColumnWidth(1, ExcelNpoiUtil.AnchoColumna(50));
            sheet.SetColumnWidth(2, ExcelNpoiUtil.AnchoColumna(50));
            

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
