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
    public class ReporteGrupoCarso
    {
        public static DataTable RegresaComprasGrupoCarso(DateTime FechaDesde, DateTime FechaHasta, ref Exception Ex)
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
                cmd.ObjectName = "usp_ReporteComprasCorporativoCarso";
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaDesde));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaHasta));
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
        public static void GeneraArchivoExcel(string RutaYNombreArchivo, DataTable dtComprasGrupoCarso, DateTime FechaDesde, DateTime FechaHasta)
        {

            HSSFWorkbook xlsWorkBook = new HSSFWorkbook();
            ISheet sheet = xlsWorkBook.CreateSheet("Hoja1");

            //formatos
            #region Formatos


            //formato de miles SIN decimales
            ICellStyle fmtoPesos = xlsWorkBook.CreateCellStyle();
            fmtoPesos.DataFormat = ExcelNpoiUtil.FormatoCelda(ref xlsWorkBook, Enumerados.FormatosNPOI.MONEDA);

            //formato para Texto Centrado
            ICellStyle fmtCentrado = xlsWorkBook.CreateCellStyle();
            fmtCentrado.Alignment = HorizontalAlignment.Center;

            //formato para texto en Negritas
            ICellStyle fmtNegritas = xlsWorkBook.CreateCellStyle();
            

            #endregion

            #region Encabezado

            IRow rngEncabezado = sheet.CreateRow(0);
            ICell celEncabezado = rngEncabezado.CreateCell(0);
            celEncabezado.SetCellValue("Reporte de Compras de Clientes Grupo CARSO");
            celEncabezado.CellStyle = fmtCentrado;

            //se combinan las celdas

            CellRangeAddress range = new CellRangeAddress(0, 0, 0, 3);
            sheet.AddMergedRegion(range);


            #endregion

            #region Se escriben los rangos de fecha

            IRow rngEmitidoDel = sheet.CreateRow(1); //renglón: "Emitido del.."
            rngEmitidoDel.CreateCell(1).SetCellValue(string.Format("Emitido del      {0}", FechaDesde.ToShortDateString()));

            IRow rngEmitidoAl = sheet.CreateRow(2); //renglón: "Al.."
            rngEmitidoAl.CreateCell(1).SetCellValue(string.Format("Al                   {0}", FechaHasta.ToShortDateString()));

            #endregion

            #region Encabezados



            #endregion

            #region Detalle

            int iRenglonDetalle = 4;
            double TotalVolumen=0, subtotal=0, iva=0, total=0;
            int startRow=0, endRow=0;


            //ENCABEZADOS DE DETALLE
            IRow renglonEncabezados = sheet.CreateRow(iRenglonDetalle);

            ICell celdaEncNoFactura = renglonEncabezados.CreateCell(0);
            celdaEncNoFactura.SetCellValue("No. Factura");
            celdaEncNoFactura.CellStyle = fmtNegritas;

            ICell celdaEncOC= renglonEncabezados.CreateCell(1);
            celdaEncOC.SetCellValue("O.C.");
            celdaEncOC.CellStyle = fmtNegritas;

            ICell celdaEncCliente= renglonEncabezados.CreateCell(2);
            celdaEncCliente.SetCellValue("Cliente");
            celdaEncCliente.CellStyle = fmtNegritas;

            ICell celdaEncPrenda= renglonEncabezados.CreateCell(3);
            celdaEncPrenda.SetCellValue("Prenda");
            celdaEncPrenda.CellStyle = fmtNegritas;

            ICell celdaEncDescripcion= renglonEncabezados.CreateCell(4);
            celdaEncDescripcion.SetCellValue("Descripcion");
            celdaEncDescripcion.CellStyle = fmtNegritas;

            ICell celdaEncProcesos = renglonEncabezados.CreateCell(5);
            celdaEncProcesos.SetCellValue("Procesos");
            celdaEncProcesos.CellStyle = fmtNegritas;

            ICell celdaEncProveedor = renglonEncabezados.CreateCell(6);
            celdaEncProveedor.SetCellValue("Proveedor");
            celdaEncProveedor.CellStyle = fmtNegritas;

            ICell celdaEncVolumen = renglonEncabezados.CreateCell(7);
            celdaEncVolumen.SetCellValue("Volumen");
            celdaEncVolumen.CellStyle = fmtNegritas;

            ICell celdaEncSubtotal= renglonEncabezados.CreateCell(8);
            celdaEncSubtotal.SetCellValue("Subtotal");
            celdaEncSubtotal.CellStyle = fmtNegritas;

            ICell celdaEncTotal= renglonEncabezados.CreateCell(9);
            celdaEncTotal.SetCellValue("Total");
            celdaEncTotal.CellStyle = fmtNegritas;

            iRenglonDetalle++;

            String _Factura = String.Empty;

            foreach (DataRow _dr in dtComprasGrupoCarso.Rows)
            {
                //AGRUPAMOS POR FACTURA    

                IRow renglonDetalle = sheet.CreateRow(iRenglonDetalle);

                if (_Factura != _dr["CVE_DOC"].ToString())
                {
                    if (startRow != 0)
                    {
                        sheet.AddMergedRegion(new CellRangeAddress(startRow,endRow,0,0));
                        sheet.AddMergedRegion(new CellRangeAddress(startRow, endRow, 1, 1));
                        sheet.AddMergedRegion(new CellRangeAddress(startRow, endRow, 2, 2));
                    }

                    ICell celdaDetalleNoFactura = renglonDetalle.CreateCell(0);
                    celdaDetalleNoFactura.SetCellValue(_dr["CVE_DOC"].ToString());

                    ICell celdaDetalleOC = renglonDetalle.CreateCell(1);
                    celdaDetalleOC.SetCellValue(_dr["OC"].ToString());

                    ICell celdaDetalleNombre = renglonDetalle.CreateCell(2);
                    celdaDetalleNombre.SetCellValue(_dr["NOMBRE"].ToString());
                    startRow = iRenglonDetalle;
                }
                _Factura = _dr["CVE_DOC"].ToString();
                endRow = iRenglonDetalle;

                ICell celdaDetallePrenda= renglonDetalle.CreateCell(3);
                celdaDetallePrenda.SetCellValue(_dr["CLAVE_ARTICULO"].ToString());

                ICell celdaDetalleDescripcion = renglonDetalle.CreateCell(4);
                celdaDetalleDescripcion.SetCellValue(_dr["DESCRIPCION_ARTICULO"].ToString());

                ICell celdaDetalleProcesos = renglonDetalle.CreateCell(5);
                celdaDetalleProcesos.SetCellValue(getDescripcionProcesos(_dr["PROCESOS"].ToString()));

                ICell celdaDetalleProveedor = renglonDetalle.CreateCell(6);
                celdaDetalleProveedor.SetCellValue("31");

                ICell celdaDetalleVolumen= renglonDetalle.CreateCell(7);
                celdaDetalleVolumen.SetCellValue(double.Parse(_dr["VOLUMEN"].ToString()));

                ICell celdaDetalleSubtotal = renglonDetalle.CreateCell(8);
                celdaDetalleSubtotal.SetCellValue(double.Parse(_dr["SUBTOTAL"].ToString()));
                celdaDetalleSubtotal.CellStyle = fmtoPesos;

                ICell celdaDetalleTotal = renglonDetalle.CreateCell(9);
                celdaDetalleTotal.SetCellValue(double.Parse(_dr["TOTAL"].ToString()));
                celdaDetalleTotal.CellStyle = fmtoPesos;

                TotalVolumen+=double.Parse(_dr["VOLUMEN"].ToString());
                subtotal += double.Parse(_dr["TOTAL"].ToString());
                iRenglonDetalle++;

                //CellRangeAddress range = new CellRangeAddress(
                //IRow range = sheet.add
            }

            //ESCRIBIMOS LOS TOTALES
            iva = subtotal * .16;
            total = subtotal + iva;
            //SUBTOTAL
            IRow renglonTotales = sheet.CreateRow(iRenglonDetalle);

            ICell celdaDetalleTotal1 = renglonTotales.CreateCell(7);
            celdaDetalleTotal1.SetCellValue(TotalVolumen);

            celdaDetalleTotal1 = renglonTotales.CreateCell(8);
            celdaDetalleTotal1.SetCellValue("Sub-Total");

            celdaDetalleTotal1 = renglonTotales.CreateCell(9);
            celdaDetalleTotal1.SetCellValue(subtotal);
            celdaDetalleTotal1.CellStyle = fmtoPesos;
            
            iRenglonDetalle++;
            //IVA

            renglonTotales = sheet.CreateRow(iRenglonDetalle);
            celdaDetalleTotal1 = renglonTotales.CreateCell(8);
            celdaDetalleTotal1.SetCellValue("IVA");

            celdaDetalleTotal1 = renglonTotales.CreateCell(9);
            celdaDetalleTotal1.SetCellValue(iva);
            celdaDetalleTotal1.CellStyle = fmtoPesos;

            iRenglonDetalle++;
            //TOTAL
            renglonTotales = sheet.CreateRow(iRenglonDetalle);
            celdaDetalleTotal1 = renglonTotales.CreateCell(8);
            celdaDetalleTotal1.SetCellValue("TOTAL");

            celdaDetalleTotal1 = renglonTotales.CreateCell(9);
            celdaDetalleTotal1.SetCellValue(total);
            celdaDetalleTotal1.CellStyle = fmtoPesos;


            sheet.SetColumnWidth(0, ExcelNpoiUtil.AnchoColumna(60));
            sheet.SetColumnWidth(1, ExcelNpoiUtil.AnchoColumna(120));
            sheet.SetColumnWidth(2, ExcelNpoiUtil.AnchoColumna(320));
            sheet.SetColumnWidth(3, ExcelNpoiUtil.AnchoColumna(100));
            sheet.SetColumnWidth(4, ExcelNpoiUtil.AnchoColumna(320));
            sheet.SetColumnWidth(5, ExcelNpoiUtil.AnchoColumna(170));
            sheet.SetColumnWidth(6, ExcelNpoiUtil.AnchoColumna(80));
            sheet.SetColumnWidth(7, ExcelNpoiUtil.AnchoColumna(80));
            sheet.SetColumnWidth(8, ExcelNpoiUtil.AnchoColumna(150));
            sheet.SetColumnWidth(9, ExcelNpoiUtil.AnchoColumna(80));


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
        private static String getDescripcionProcesos(String _Procesos)
        {
            String descripcion = string.Empty;
            int contador;
            //OBTENEMOS EL NUMERO DE BORDADOS
            contador = _Procesos.Count(x => x == 'B');
            if (contador == 1)
                descripcion += "BORDADO ";
            else if (contador > 1)
                descripcion += contador.ToString() + " BORDADOS ";
            //OBTENEMOS EL NUMERO DE COSTURAS
            contador = _Procesos.Count(x => x == 'C');
            if (contador == 1)
                descripcion += "COSTURA ";
            else if (contador > 1)
                descripcion += contador.ToString() + " COSTURAS ";
            //OBTENEMOS EL NUMERO DE DIBUJOS
            contador = _Procesos.Count(x => x == 'D');
            if (contador == 1)
                descripcion += "DIBUJO ";
            else if (contador > 1)
                descripcion += contador.ToString() + " DIBUJOS ";
            //OBTENEMOS EL NUMERO DE PONCHADOS
            contador = _Procesos.Count(x => x == 'E');
            if (contador == 1)
                descripcion += "PONCHADO ";
            else if (contador > 1)
                descripcion += contador.ToString() + " PONCHADOS ";
            //OBTENEMOS EL NUMERO DE CORTES
            contador = _Procesos.Count(x => x == 'R');
            if (contador == 1)
                descripcion += "CORTE ";
            else if (contador > 1)
                descripcion += contador.ToString() + " CORTES ";
            contador = _Procesos.Count(x => x == 'x');
            if (contador>=1)
                descripcion += " SIN PROCESOS ";
            

            return descripcion.Trim();
        }

    }
}