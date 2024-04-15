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
    public class ReporteAnalisisRetrasosOP
    {
        public static DataTable GetAnalisisRetrasosOP(DateTime Fecha, ref Exception Ex)
        {
            String conStr = "";
            DataTable dtAnalis = new DataTable();
            try
            {
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "usp_ReporteAalisisOP";
                cmd.Parameters.Add(new SqlParameter("@fecha", Fecha));
                dtAnalis = cmd.GetDataTable();
                cmd.Connection.Close();
                Ex = null;
                return dtAnalis;
            }
            catch (Exception ex)
            {
                Ex = ex;
                return null;
            }
        }
        public static void GeneraArchivoExcel(string RutaYNombreArchivo, DataTable dtAnalisis, DateTime Fecha)
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
            celEncabezado.SetCellValue("Reporte de Análisis de Retraso de OP");
            celEncabezado.CellStyle = fmtCentrado;

            //se combinan las celdas

            CellRangeAddress range = new CellRangeAddress(0, 0, 0, 3);
            sheet.AddMergedRegion(range);


            #endregion

            #region Se escriben los rangos de fecha, cliente y DS CMP

            IRow rngEmitidoDel = sheet.CreateRow(1); //renglón: "Emitido del.."
            rngEmitidoDel.CreateCell(1).SetCellValue(string.Format("Emitido el      {0}", Fecha.ToShortDateString()));


            #endregion

            #region DETALLE
            int iRenglonDetalle = 3;
            String tipoLinea = String.Empty;
            int idPedido = 0;
            foreach (DataRow _dr in dtAnalisis.Rows)
            {
                if (tipoLinea == "" || tipoLinea != _dr["TIPO_LINEA"].ToString())
                {
                    //ESCRIBIMOS ENCABEZADO DEL TIPO DE LINEA QUE ESTAMOS MANEJADO
                    tipoLinea = _dr["TIPO_LINEA"].ToString();
                    iRenglonDetalle++;
                    IRow renglonEncabezadoTipoLinea = sheet.CreateRow(iRenglonDetalle);

                    ICell celdaEncabezadoTipoLineaTipo = renglonEncabezadoTipoLinea.CreateCell(0);
                    celdaEncabezadoTipoLineaTipo.SetCellValue("DETALLE DE PEDIDOS POR TIPO DE LINEA: " + _dr["TIPO_LINEA"]);
                    celdaEncabezadoTipoLineaTipo.CellStyle = fmtNegritas;
                    iRenglonDetalle++;

                    IRow renglonEncabezado = sheet.CreateRow(iRenglonDetalle);

                    ICell celdaEncabezadoPedido = renglonEncabezado.CreateCell(0);
                    celdaEncabezadoPedido.SetCellValue("PEDIDO");
                    celdaEncabezadoPedido.CellStyle = fmtNegritas;

                    ICell celdaEncabezadoFechaInicial = renglonEncabezado.CreateCell(1);
                    celdaEncabezadoFechaInicial.SetCellValue("FECHA INICIAL");
                    celdaEncabezadoFechaInicial.CellStyle = fmtNegritas;

                    ICell celdaEncabezadoFechaEntrega = renglonEncabezado.CreateCell(2);
                    celdaEncabezadoFechaEntrega.SetCellValue("FECHA ENTREGA");
                    celdaEncabezadoFechaEntrega.CellStyle = fmtNegritas;

                    ICell celdaEncabezadoDias = renglonEncabezado.CreateCell(3);
                    celdaEncabezadoDias.SetCellValue("DIAS ENTREGA");
                    celdaEncabezadoDias.CellStyle = fmtNegritas;
                }

                if (idPedido == 0 || idPedido != int.Parse(_dr["IDPEDIDO"].ToString()))
                {
                    //ESCRIBIMOS EL ENCABEZADO DEL PEDIDO
                    idPedido = int.Parse(_dr["IDPEDIDO"].ToString());
                    iRenglonDetalle++;
                    IRow renglonEncabezadoPedido = sheet.CreateRow(iRenglonDetalle);

                    ICell celdaEncabezadoPedidoID = renglonEncabezadoPedido.CreateCell(0);
                    celdaEncabezadoPedidoID.SetCellValue(_dr["IDPEDIDO"].ToString());

                    ICell celdaEncabezadoPedidoFechaInicial = renglonEncabezadoPedido.CreateCell(1);
                    celdaEncabezadoPedidoFechaInicial.SetCellValue(DateTime.Parse(_dr["FINICIAL"].ToString()).ToString("dd/MM/yyyy"));
                    

                    ICell celdaEncabezadoPedidoFechaEntrega = renglonEncabezadoPedido.CreateCell(2);
                    celdaEncabezadoPedidoFechaEntrega.SetCellValue(DateTime.Parse(_dr["FENTREGA"].ToString()).ToString("dd/MM/yyyy"));

                    ICell celdaEncabezadoPedidoDias = renglonEncabezadoPedido.CreateCell(3);
                    celdaEncabezadoPedidoDias.SetCellValue(_dr["DIASRETRASO"].ToString());
                    iRenglonDetalle++;

                    //ESCRIBIMOS EL ENCABEZADO PARA EL DETALLE
                    IRow renglonEncabezadoDetalle = sheet.CreateRow(iRenglonDetalle);

                    ICell celdaEncabezadoDetalleProducto = renglonEncabezadoDetalle.CreateCell(4);
                    celdaEncabezadoDetalleProducto.SetCellValue("PRODUCTO");

                    ICell celdaEncabezadoDetalleCantidad = renglonEncabezadoDetalle.CreateCell(5);
                    celdaEncabezadoDetalleCantidad.SetCellValue("CANTIDAD");

                    ICell celdaEncabezadoDetalleCantidadTerminada = renglonEncabezadoDetalle.CreateCell(6);
                    celdaEncabezadoDetalleCantidadTerminada.SetCellValue("CANTIDAD TERMINADA");

                    ICell celdaEncabezadoDetalleFaltantes = renglonEncabezadoDetalle.CreateCell(7);
                    celdaEncabezadoDetalleFaltantes.SetCellValue("FALTANTES");
                    iRenglonDetalle++;
                }

                IRow renglonDetalle = sheet.CreateRow(iRenglonDetalle);

                ICell celdaDetalleProducto = renglonDetalle.CreateCell(4);
                celdaDetalleProducto.SetCellValue(_dr["PRODUCTO"].ToString());

                ICell celdaDetalleCantidad = renglonDetalle.CreateCell(5);
                celdaDetalleCantidad.SetCellValue(_dr["CANTIDAD"].ToString());

                ICell celdaDetalleCantidadTerminada = renglonDetalle.CreateCell(6);
                celdaDetalleCantidadTerminada.SetCellValue(_dr["CANTTERM"].ToString());

                ICell celdaDetalleFaltantes = renglonDetalle.CreateCell(7);
                celdaDetalleFaltantes.SetCellValue(_dr["FALTANTES"].ToString());
                iRenglonDetalle++;

            }
            #endregion
            sheet.SetColumnWidth(0, ExcelNpoiUtil.AnchoColumna(70));
            sheet.SetColumnWidth(1, ExcelNpoiUtil.AnchoColumna(70));
            sheet.SetColumnWidth(2, ExcelNpoiUtil.AnchoColumna(70));
            sheet.SetColumnWidth(3, ExcelNpoiUtil.AnchoColumna(70));

            sheet.SetColumnWidth(4, ExcelNpoiUtil.AnchoColumna(200));
            sheet.SetColumnWidth(5, ExcelNpoiUtil.AnchoColumna(200));
            sheet.SetColumnWidth(6, ExcelNpoiUtil.AnchoColumna(200));
            sheet.SetColumnWidth(7, ExcelNpoiUtil.AnchoColumna(200));

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
