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
    public class ReporteArticulosCliente
    {
        public static DataTable RegresaArticulosCliente(DateTime FechaDesde, DateTime FechaHasta, String Cliente, ref Exception Ex)
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
                cmd.ObjectName = "usp_ReporteArticuloCliente";
                cmd.Parameters.Add(new SqlParameter("@Fecha_Ini", FechaDesde));
                cmd.Parameters.Add(new SqlParameter("@Fecha_Fin", FechaHasta));
                cmd.Parameters.Add(new SqlParameter("@Cliente",Cliente));
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
        public static DataTable CargaClientes(ref Exception Ex)
        {
            String conStr = "";
            DataTable dtClientes = new DataTable();
            try
            {
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "usp_CargaClientes";
                dtClientes = cmd.GetDataTable();
                cmd.Connection.Close();
                Ex = null;
                return dtClientes;
            }
            catch (Exception ex)
            {
                Ex = ex;
                return null;
            }
        }
        public static void GeneraArchivoExcel(string RutaYNombreArchivo, DataTable dtArticulosCliente, DateTime FechaDesde, DateTime FechaHasta, String Cliente, decimal DS, decimal CMP)
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
            celEncabezado.SetCellValue("Reporte de Articulos por Cliente a un periodo");
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

         

            #endregion

            #region Detalle

            int iRenglonDetalle = 4;
            int total = 0;
            int subtotal = 0;
            bool muestroEncabezado = true;
            String cliente, clienteAux = String.Empty;
            foreach (DataRow _dr in dtArticulosCliente.Rows)
            {
                cliente = _dr["CLIENTE"].ToString();
                if (cliente != clienteAux)
                {
                    muestroEncabezado = true;
                    if (subtotal != 0)
                    {
                        IRow renglonSubTotal = sheet.CreateRow(iRenglonDetalle);
                        ICell celdaDetalleSubTotalFinal = renglonSubTotal.CreateCell(2);
                        celdaDetalleSubTotalFinal.SetCellValue("SUBTOTAL");

                        ICell celdaDetalleSubTotalValorFinal = renglonSubTotal.CreateCell(3);
                        celdaDetalleSubTotalValorFinal.SetCellValue(subtotal);
                        celdaDetalleSubTotalValorFinal.CellStyle = fmtoMiles;
                        subtotal = 0;
                        iRenglonDetalle +=2;
                    }
                }
                if (muestroEncabezado)
                {
                    //AQUI METEMOS EL ENCABEZADO PARA CADA PERSONA
                    //1 es PRINCIPAL, 2 ES RELACIONADO
                    IRow rngTipo = sheet.CreateRow(iRenglonDetalle);
                    rngTipo.CreateCell(1).SetCellValue(string.Format("Tipo                        {0}", _dr["TIPO"].ToString()=="1"?"PRINCIPAL":"FILIAL"));
                    iRenglonDetalle++;
                    IRow rngCliente = sheet.CreateRow(iRenglonDetalle);
                    rngCliente.CreateCell(1).SetCellValue(string.Format("Cliente                     {0} - {1}",_dr["CLIENTE"].ToString(), _dr["NOMBRE"].ToString()));
                    iRenglonDetalle ++;
                    IRow rngDS = sheet.CreateRow(iRenglonDetalle); //renglón: "Almacenes Destino"
                    rngDS.CreateCell(1).SetCellValue(string.Format("DS                           {0}", DS));
                    iRenglonDetalle++;
                    IRow rngCMP = sheet.CreateRow(iRenglonDetalle); //renglón: "Almacenes Destino"
                    rngCMP.CreateCell(1).SetCellValue(string.Format("CMP                         {0}", CMP));
                    iRenglonDetalle+=2;

                    //ENCABEZADOS DE DETALLE
                    IRow renglonEncabezados = sheet.CreateRow(iRenglonDetalle);

                    ICell celdaEncAnio = renglonEncabezados.CreateCell(0);
                    celdaEncAnio.SetCellValue("AÑO");

                    ICell celdaEncMes = renglonEncabezados.CreateCell(1);
                    celdaEncMes.SetCellValue("MES");

                    ICell celdaEncArticulo = renglonEncabezados.CreateCell(2);
                    celdaEncArticulo.SetCellValue("ARTICULO");

                    ICell celdaEncTotalMes = renglonEncabezados.CreateCell(3);
                    celdaEncTotalMes.SetCellValue("TOTAL MES");
                    iRenglonDetalle++;
                    clienteAux = _dr["CLIENTE"].ToString();
                    muestroEncabezado = false;
                }

                IRow renglonDetalle = sheet.CreateRow(iRenglonDetalle);

                ICell celdaDetalleAnio = renglonDetalle.CreateCell(0);
                celdaDetalleAnio.SetCellValue(int.Parse(_dr["ANIO"].ToString()));

                ICell celdaDetalleMes = renglonDetalle.CreateCell(1);
                celdaDetalleMes.SetCellValue(_dr["MES"].ToString());

                ICell celdaDetalleArticulo = renglonDetalle.CreateCell(2);
                celdaDetalleArticulo.SetCellValue(_dr["ARTICULO"].ToString());

                ICell celdaDetalleTotalMes = renglonDetalle.CreateCell(3);
                celdaDetalleTotalMes.SetCellValue(int.Parse(_dr["TOTAL_MES"].ToString()));

                subtotal += int.Parse(_dr["TOTAL_MES"].ToString());
                total += int.Parse(_dr["TOTAL_MES"].ToString());

                iRenglonDetalle++;
            }

            IRow renglonSubTotal_ = sheet.CreateRow(iRenglonDetalle);

            ICell celdaDetalleSubTotalFinal_ = renglonSubTotal_.CreateCell(2);
            celdaDetalleSubTotalFinal_.SetCellValue("SUBTOTAL");
            ICell celdaDetalleSubTotalValorFinal_ = renglonSubTotal_.CreateCell(3);
            celdaDetalleSubTotalValorFinal_.SetCellValue(subtotal);
            celdaDetalleSubTotalValorFinal_.CellStyle = fmtoMiles;

            iRenglonDetalle += 2;
            IRow renglonTotal = sheet.CreateRow(iRenglonDetalle);
            ICell celdaDetalleTotalFinal = renglonTotal.CreateCell(2);
            celdaDetalleTotalFinal.SetCellValue("TOTAL GENERAL");

            ICell celdaDetalleTotalValorFinal = renglonTotal.CreateCell(3);
            celdaDetalleTotalValorFinal.SetCellValue(total);
            celdaDetalleTotalValorFinal.CellStyle = fmtoMiles;


            sheet.SetColumnWidth(0, ExcelNpoiUtil.AnchoColumna(80));
            sheet.SetColumnWidth(1, ExcelNpoiUtil.AnchoColumna(120));
            sheet.SetColumnWidth(2, ExcelNpoiUtil.AnchoColumna(120));
            sheet.SetColumnWidth(3, ExcelNpoiUtil.AnchoColumna(80));

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
