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
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.IO;


namespace ulp_bl
{
    public class RepTransAlmacen1
    {
        public static DataSet GetListadoAlmacenes(ref Exception Ex)
        {
            string conStr = "";
            DataSet dsListadoAlmacenes = new DataSet();
            try
            {
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }

                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "usp_GetListadoAlmacenes";
                dsListadoAlmacenes = cmd.GetDataSet();

                cmd.Connection.Close();
                return dsListadoAlmacenes;
            }
            catch (Exception Exc)
            {
                Ex = Exc;
                return null;
            }
        }
        /// <summary>
        /// GET TRANSFERENCIAS DE ALMACEN
        /// </summary>
        /// <param name="FechaDesde">Fecha Inicial</param>
        /// <param name="FechaHasta">Fecha Final</param>
        /// <param name="AlmacenOrigen">Por parte del requerimiento inicialmente será el Alamacen 1</param>
        /// <param name="AlmacenesDestino">Listado de Almacenes de transferencia</param>
        /// <returns></returns>
        public static DataSet RegresaDatosSalidasDeAlmacen(DateTime FechaDesde, DateTime FechaHasta, String AlmacenOrigen, String AlmacenesDestino, ref Exception Ex)
        {
             string conStr = "";
            DataSet dsSalidasAlmacen = new DataSet();
            try
            {
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }

                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "usp_RepSalidasDeAlmacen";
                cmd.Parameters.Add(new SqlParameter("@Almacen_Origen", AlmacenOrigen));
                cmd.Parameters.Add(new SqlParameter("@Almacen_Destino", AlmacenesDestino));
                cmd.Parameters.Add(new SqlParameter("@Fecha_Ini", FechaDesde));
                cmd.Parameters.Add(new SqlParameter("@Fecha_Fin", FechaHasta));

                dsSalidasAlmacen = cmd.GetDataSet();
                cmd.Connection.Close();
                Ex = null;
                return dsSalidasAlmacen;
            }
            catch(Exception ex)
            {
                Ex = ex;
                return null;
            }
        }
        public static void GeneraArchivoExcel(string RutaYNombreArchivo, DataSet dsReporteTransferencias, DateTime FechaDesde, DateTime FechaHasta, String AlmacenOrigen, String AlmacenesDestino)
        {
            DataTable dtSalidas = dsReporteTransferencias.Tables[0];

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
            celEncabezado.SetCellValue("Reporte de Transferencias de Almacen " + AlmacenOrigen);
            celEncabezado.CellStyle = fmtCentrado;


            ExcelNpoiUtil.AsignaValorCelda(ref sheet, 0, 0, fmtCentrado, "Reporte de Transferencias de Almacen " + AlmacenOrigen);

            //se combinan las celdas

            CellRangeAddress range = new CellRangeAddress(0, 0, 0,3);
            sheet.AddMergedRegion(range);


            #endregion

            #region Se escriben los rangos de fecha y los almacenes de Destino

            IRow rngEmitidoDel = sheet.CreateRow(1); //renglón: "Emitido del.."
            rngEmitidoDel.CreateCell(3).SetCellValue(string.Format("Emitido del      {0}", FechaDesde.ToShortDateString()));

            IRow rngEmitidoAl = sheet.CreateRow(2); //renglón: "Al.."
            rngEmitidoAl.CreateCell(3).SetCellValue(string.Format("Al                   {0}", FechaHasta.ToShortDateString()));
            IRow rAlmacenesDestino = sheet.CreateRow(3); //renglón: "Almacenes Destino"
            rAlmacenesDestino.CreateCell(3).SetCellValue(string.Format("Almacenes Destino                     {0}", AlmacenesDestino));

            #endregion

            #region Encabezados
            //encabezados
            IRow renglonEncabezados = sheet.CreateRow(5);

            ICell celdaEncArticulo = renglonEncabezados.CreateCell(0);
            celdaEncArticulo.SetCellValue("ARTICULO");

            ICell celdaEncTotal = renglonEncabezados.CreateCell(1);
            celdaEncTotal.SetCellValue("TOTAL");

            #endregion

            #region Detalle

            int iRenglonDetalle = 7;
            int subTotal = 0;
            int total = 0;
            String LetraGrupo = String.Empty;
            String LetraGrupoEspecial = String.Empty;
            String LetraGrupoAux = String.Empty;
            foreach (DataRow _dr in dtSalidas.Rows)
            {
                LetraGrupo = _dr["CVE_ART"].ToString().Substring(0, 1);
                LetraGrupoEspecial = _dr["CVE_ART"].ToString().Substring(0, 2);

                if (LetraGrupoEspecial.Contains("PO")||LetraGrupoEspecial.Contains("PR"))
                {
                    LetraGrupo = LetraGrupoEspecial;
                }

                if (LetraGrupo != LetraGrupoAux)
                {
                    LetraGrupoAux = LetraGrupo;
                    //ASIGNAMOS EL TOTAL DE LA LETRA ACTUAL
                    if (subTotal != 0)
                    {
                        iRenglonDetalle++;
                        IRow renglonSubtotal = sheet.CreateRow(iRenglonDetalle);
                        ICell celdaDetalleSubtotal = renglonSubtotal.CreateCell(0);
                        celdaDetalleSubtotal.SetCellValue("SUBTOTAL");
                        

                        ICell celdaDetalleSubtotalValor = renglonSubtotal.CreateCell(1);
                        celdaDetalleSubtotalValor.SetCellValue(subTotal);
                        celdaDetalleSubtotalValor.CellStyle = fmtoMiles;
                        iRenglonDetalle+=2;
                        subTotal = 0;
                    }
                }               

                IRow renglonDetalle = sheet.CreateRow(iRenglonDetalle);
                ICell celdaDetalleArticulo = renglonDetalle.CreateCell(0);
                celdaDetalleArticulo.SetCellValue(_dr["CVE_ART"].ToString());

                ICell celdaDetalleTotal= renglonDetalle.CreateCell(1);
                celdaDetalleTotal.SetCellValue(int.Parse(_dr["TOTAL"].ToString()));
                celdaDetalleTotal.CellStyle=fmtoMiles;

                subTotal += int.Parse(_dr["TOTAL"].ToString());
                total += int.Parse(_dr["TOTAL"].ToString());

                iRenglonDetalle++;
            }

            iRenglonDetalle++;
            IRow renglonSubtotalFinal = sheet.CreateRow(iRenglonDetalle);
            ICell celdaDetalleSubtotalFinal = renglonSubtotalFinal.CreateCell(0);
            celdaDetalleSubtotalFinal.SetCellValue("SUBTOTAL");

            ICell celdaDetalleSubtotalValorFinal = renglonSubtotalFinal.CreateCell(1);
            celdaDetalleSubtotalValorFinal.SetCellValue(subTotal);
            celdaDetalleSubtotalValorFinal.CellStyle = fmtoMiles;
            iRenglonDetalle += 2;

            IRow renglonTotal= sheet.CreateRow(iRenglonDetalle);
            ICell celdaDetalleTotalFinal = renglonTotal.CreateCell(0);
            celdaDetalleTotalFinal.SetCellValue("TOTAL GENERAL");

            ICell celdaDetalleTotalValorFinal = renglonTotal.CreateCell(1);
            celdaDetalleTotalValorFinal.SetCellValue(total);
            celdaDetalleTotalValorFinal.CellStyle = fmtoMiles;
            

            sheet.SetColumnWidth(0, ExcelNpoiUtil.AnchoColumna(120));
            sheet.SetColumnWidth(1, ExcelNpoiUtil.AnchoColumna(80));

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
