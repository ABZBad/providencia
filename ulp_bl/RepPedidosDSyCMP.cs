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
    public class RepPedidosDSyCMP
    {
        public static DataTable RegresaPedidosDSyCMP(DateTime FechaDesde, DateTime FechaHasta, ref Exception Ex)
        {
            String conStr = "";
            DataTable dtPedidos = new DataTable();
            try
            {
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "usp_RepPedidosDsyCMP";
                cmd.Parameters.Add(new SqlParameter("@Fecha_Ini", FechaDesde));
                cmd.Parameters.Add(new SqlParameter("@Fecha_Fin", FechaHasta));
                dtPedidos = cmd.GetDataTable();
                cmd.Connection.Close();
                Ex = null;
                return dtPedidos;
            }
            catch (Exception ex)
            {
                Ex = ex;
                return null;
            }
        }
        public static void GeneraArchivoExcel(string RutaYNombreArchivo, DataTable dtPedidosDSyCMP, DateTime FechaDesde, DateTime FechaHasta)
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
            celEncabezado.SetCellValue("Reporte de Pedidos con DS y CMP");
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

            int iRenglonDetalle = 3;
            int grupo = 0;
            int grupoAux = -1;
            String descripcionGrupo;
            bool muestroEncabezado = true;
            foreach (DataRow _dr in dtPedidosDSyCMP.Rows)
            {
                //DESCRIPCION DE RANGOS
                if ((_dr["RangoMinimo"].ToString() == _dr["RangoMaximo"].ToString()) && _dr["RangoMinimo"].ToString()=="0")
                    descripcionGrupo = "PEDIDOS CON COMPRAS MENORES A " + _dr["RangoMaximo"].ToString() + " PRENDAS.";
                else if ((_dr["RangoMinimo"].ToString() == _dr["RangoMaximo"].ToString()) && _dr["RangoMinimo"].ToString() != "0")
                    descripcionGrupo = "PEDIDOS CON COMPRAS MAYORES A " + _dr["RangoMinimo"].ToString() + " PRENDAS.";
                else
                    descripcionGrupo = "PEDIDOS CON COMPRAS ENTRE " + _dr["RangoMinimo"].ToString() + " Y " + _dr["RangoMaximo"].ToString() + " PRENDAS.";

                grupo =int.Parse(_dr["AgrupadorDSyCMP"].ToString());
                if (grupo != grupoAux)
                {
                    iRenglonDetalle++;
                    //ARMAMOS Y ESCRIBIMOS EL ENCABEZADO DEL GRUPO
                    IRow renglonDescripcionGrupo = sheet.CreateRow(iRenglonDetalle);
                    ICell celdaDescripcionGrupo = renglonDescripcionGrupo.CreateCell(0);
                    celdaDescripcionGrupo.SetCellValue(descripcionGrupo);
                    grupoAux = grupo;
                    muestroEncabezado = true;
                    iRenglonDetalle+=2;
                }

                if (muestroEncabezado)
                {

                    //ENCABEZADOS DE DETALLE
                    IRow renglonEncabezados = sheet.CreateRow(iRenglonDetalle);

                    ICell celdaEncPedido = renglonEncabezados.CreateCell(0);
                    celdaEncPedido.SetCellValue("PEDIDO");

                    ICell celdaEncNombre = renglonEncabezados.CreateCell(1);
                    celdaEncNombre.SetCellValue("RAZON SOCIAL");

                    ICell celdaEncDescuento = renglonEncabezados.CreateCell(2);
                    celdaEncDescuento.SetCellValue("DESCUENTO");

                    ICell celdaEncDS = renglonEncabezados.CreateCell(3);
                    celdaEncDS.SetCellValue("DS");

                    ICell celdaEncCMP = renglonEncabezados.CreateCell(4);
                    celdaEncCMP.SetCellValue("CMP");
                    
                    iRenglonDetalle++;
                    muestroEncabezado = false;
                }

                //MOSTRAMOS LOS DETALLES

                IRow renglonDetalle = sheet.CreateRow(iRenglonDetalle);

                ICell celdaDetallePedido = renglonDetalle.CreateCell(0);
                celdaDetallePedido.SetCellValue(int.Parse(_dr["PEDIDO"].ToString()));

                ICell celdaDetalleNombre = renglonDetalle.CreateCell(1);
                celdaDetalleNombre.SetCellValue(_dr["NOMBRE"].ToString());

                ICell celdaDetalleDescuento = renglonDetalle.CreateCell(2);
                celdaDetalleDescuento.SetCellValue(double.Parse(_dr["DESCUENTO"].ToString()));

                ICell celdaDetalleDS = renglonDetalle.CreateCell(3);
                celdaDetalleDS.SetCellValue(double.Parse(_dr["DS"].ToString()));

                ICell celdaDetalleCMP = renglonDetalle.CreateCell(4);
                celdaDetalleCMP.SetCellValue(double.Parse(_dr["CMP"].ToString()));

                iRenglonDetalle++;
            }



            
           


            sheet.SetColumnWidth(0, ExcelNpoiUtil.AnchoColumna(60));
            sheet.SetColumnWidth(1, ExcelNpoiUtil.AnchoColumna(220));
            sheet.SetColumnWidth(2, ExcelNpoiUtil.AnchoColumna(100));
            sheet.SetColumnWidth(3, ExcelNpoiUtil.AnchoColumna(60));
            sheet.SetColumnWidth(3, ExcelNpoiUtil.AnchoColumna(60));

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
