using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ulp_dl.SIPNegocio;
using sm_dl;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.SS.Util;
using ulp_bl.Utiles;
using System.IO;


namespace ulp_bl
{

    public class RecOrdProduccionMaquilaCodigoBarras
    {

        public static void GuardaRecepcionCodigoBarra(String _UUID,
            int _Consecutivo,
            String _Referencia,
            String _OrdenMaquila,
            int _TotalPrendasDefectuosas,
            String _usuario
        )
        {
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand guarda = new sm_dl.SqlServer.SqlServerCommand();
                guarda.Connection = sm_dl.DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                guarda.ObjectName = "usp_RecepcionCodigoBarras";
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@uuid", _UUID));
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@consecutivo", _Consecutivo));
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Referencia", _Referencia));
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OrdenMaquila", _OrdenMaquila));
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TotalPrendasDefectuosas", _TotalPrendasDefectuosas));
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@usuario", _usuario));
                guarda.Execute();

            }
        }

        public static string GuardaRecepcionCodigoBarrasSAE(
            String _Referencia,
            String _OrdenMaquila,
            int _OrdenProduccion,
            Decimal _CostoConfeccion,
            int _TotalPrendas,
            int _TotalPrendasDefectuosas,
            String _EsquemaImpuestos,
            String _Modelo,
            String _Talla,
            int _ConsecutivoReg,
            String _NombreDefectuoso,
            String _prefijo,
            String _usuario
            )
        {
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand guarda = new sm_dl.SqlServer.SqlServerCommand();
                guarda.Connection = sm_dl.DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                guarda.ObjectName = "usp_RecepcionOrdenProduccionCodigoBarras";
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Referencia", _Referencia));
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OrdenMaquila", _OrdenMaquila));
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OrdenProduccion", _OrdenProduccion));
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CostoConfeccion", _CostoConfeccion));
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TotalPrendas", _TotalPrendas));
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TotalPrendasDefectuosas", _TotalPrendasDefectuosas));
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EsquemaImpuestos", _EsquemaImpuestos));
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Modelo", _Modelo));
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Talla", _Talla));
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ConsecutivoReg", _ConsecutivoReg));
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NombreDefectuoso", _NombreDefectuoso));
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Prefijo", _prefijo));
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@usuario", _usuario));
                System.Data.SqlClient.SqlParameter _out = new System.Data.SqlClient.SqlParameter("@error",SqlDbType.VarChar,200);
                _out.Direction=ParameterDirection.Output;
                guarda.Parameters.Add(_out);
                guarda.Execute();
                return _out.Value.ToString();

            }
        }

        public static void GuardaCodigoBarras(List<CodigoBarra> ListaCodigoBarras)
        {
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand guarda = new sm_dl.SqlServer.SqlServerCommand();
                guarda.Connection = sm_dl.DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                guarda.ObjectName = "usp_setAltaCodigoBarras";
                foreach (CodigoBarra _codigo in ListaCodigoBarras)
                {
                    guarda.Parameters.Clear();
                    guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UUID", _codigo.UUID));
                    guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Consecutivo", _codigo.Consecutivo));
                    guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Referencia", _codigo.Referencia));
                    guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OrdenMaquila", _codigo.OrdenMaquila));
                    guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Almacen", _codigo.Almacen));
                    guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Modelo", _codigo.Modelo));
                    guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Descripcion", _codigo.Descripcion));
                    guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Talla", _codigo.Talla));
                    guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TipoLinea", _codigo.Tipo));
                    guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Cantidad", _codigo.Cantidad));
                    guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FechaGeneracion", _codigo.FechaGeneracion));
                    guarda.Execute();
                }



            }

        }
        public static void GeneraArchivoExcel(List<CodigoBarra> datosMaquila, string RutaYNombreArchivo)
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
            ICell celEncabezado = rngEncabezado.CreateCell(2);
            celEncabezado.SetCellValue("Reporte de Recepción de OP y OM por Código de Barras");
            celEncabezado.CellStyle = fmtCentrado;

            //se combinan las celdas

            CellRangeAddress range = new CellRangeAddress(0, 0, 2, 4);
            sheet.AddMergedRegion(range);


            #endregion

            #region Se escriben la fecha de Recepción

            IRow rngEmitidoDel = sheet.CreateRow(1); //renglón: "Emitido del.."
            rngEmitidoDel.CreateCell(2).SetCellValue(string.Format("Fecha de Recepción:      {0}", DateTime.Now.ToShortDateString()));

            #endregion

            #region Encabezados para el detalle
            int iRenglonDetalle = 3;

            //ENCABEZADOS DE DETALLE
            IRow renglonEncabezados = sheet.CreateRow(iRenglonDetalle);

            ICell celdaEncPedido = renglonEncabezados.CreateCell(0);
            celdaEncPedido.SetCellValue("REF. OP.");

            ICell celdaEncOrdenProduccion = renglonEncabezados.CreateCell(1);
            celdaEncOrdenProduccion.SetCellValue("ORDEN PRODUCCION");

            ICell celdaEncOrdenMaquila = renglonEncabezados.CreateCell(2);
            celdaEncOrdenMaquila.SetCellValue("ORDEN COMPRA");


            ICell celdaEncModelo = renglonEncabezados.CreateCell(3);
            celdaEncModelo.SetCellValue("MODELO");

            ICell celdaEncDescripcion = renglonEncabezados.CreateCell(4);
            celdaEncDescripcion.SetCellValue("DESCRIPCION");

            ICell celdaEncTalla = renglonEncabezados.CreateCell(5);
            celdaEncTalla.SetCellValue("TALLA");

            ICell celdaEncCantidadTotal = renglonEncabezados.CreateCell(6);
            celdaEncCantidadTotal.SetCellValue("CANTIDAD TOTAL");

            ICell celdaEncCantidadRecibida = renglonEncabezados.CreateCell(7);
            celdaEncCantidadRecibida.SetCellValue("CANTIDAD RECIBIDA");

            ICell celdaEncCantidadDefectuosa = renglonEncabezados.CreateCell(8);
            celdaEncCantidadDefectuosa.SetCellValue("CANTIDAD DEFECTUOSA");

            #endregion

            #region DETALLE
            iRenglonDetalle++;
            foreach (CodigoBarra _objRecepcion in datosMaquila.OrderBy(x => x.Modelo).OrderBy(x => x.Talla))
            {
                IRow renglonDetalle = sheet.CreateRow(iRenglonDetalle);

                ICell celdaDetallePedido = renglonDetalle.CreateCell(0);
                celdaDetallePedido.SetCellValue(_objRecepcion.Referencia);

                ICell celdaDetalleOrdenProduccion = renglonDetalle.CreateCell(1);
                celdaDetalleOrdenProduccion.SetCellValue(_objRecepcion.OrdenProduccion.ToString());

                ICell celdaDetalleOrdenMaquila = renglonDetalle.CreateCell(2);
                celdaDetalleOrdenMaquila.SetCellValue(_objRecepcion.OrdenMaquila.ToString());

                ICell celdaDetalleModelo = renglonDetalle.CreateCell(3);
                celdaDetalleModelo.SetCellValue(_objRecepcion.Modelo.ToString());

                ICell celdaDetalleDescripcion = renglonDetalle.CreateCell(4);
                celdaDetalleDescripcion.SetCellValue(_objRecepcion.Descripcion.ToString());

                ICell celdaDetalleTalla = renglonDetalle.CreateCell(5);
                celdaDetalleTalla.SetCellValue(_objRecepcion.Talla.ToString());

                ICell celdaDetalleCantidad = renglonDetalle.CreateCell(6);
                celdaDetalleCantidad.SetCellValue(_objRecepcion.Cantidad.ToString());

                ICell celdaDetalleRecibidos = renglonDetalle.CreateCell(7);
                celdaDetalleRecibidos.SetCellValue(_objRecepcion.Recibidos.ToString());

                ICell celdaDetalleDefectuosos = renglonDetalle.CreateCell(8);
                celdaDetalleDefectuosos.SetCellValue(_objRecepcion.Defectuosos.ToString());


                iRenglonDetalle++;
            }

            #endregion

            sheet.SetColumnWidth(0, ExcelNpoiUtil.AnchoColumna(100)); //REFERENCIA
            sheet.SetColumnWidth(1, ExcelNpoiUtil.AnchoColumna(0)); //OP
            sheet.SetColumnWidth(2, ExcelNpoiUtil.AnchoColumna(100)); //OM
            sheet.SetColumnWidth(3, ExcelNpoiUtil.AnchoColumna(100)); //MODELO
            sheet.SetColumnWidth(4, ExcelNpoiUtil.AnchoColumna(350)); //DESCRIPCION
            sheet.SetColumnWidth(5, ExcelNpoiUtil.AnchoColumna(100)); //TALLA
            sheet.SetColumnWidth(6, ExcelNpoiUtil.AnchoColumna(170)); //CANTIDAD TOTAL
            sheet.SetColumnWidth(7, ExcelNpoiUtil.AnchoColumna(170)); //CANTIDAD TOTAL
            sheet.SetColumnWidth(8, ExcelNpoiUtil.AnchoColumna(170)); //DEFECTUOSOS

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
        public static void GeneraArchivoExcelDetalle(List<CodigoBarra> datosMaquila, string RutaYNombreArchivo)
        {
            HSSFWorkbook xlsWorkBook = new HSSFWorkbook();
            ISheet sheet = xlsWorkBook.CreateSheet("DETALLE");

            #region Encabezados para el detalle
            int iRenglonDetalle = 0;

            //ENCABEZADOS DE DETALLE
            IRow renglonEncabezados = sheet.CreateRow(iRenglonDetalle);

            ICell celdaEncUUID = renglonEncabezados.CreateCell(0);
            celdaEncUUID.SetCellValue("UUID");

            ICell celdaEncConsecutivo = renglonEncabezados.CreateCell(1);
            celdaEncConsecutivo.SetCellValue("CONSECUTIVO");

            ICell celdaEncReferencia = renglonEncabezados.CreateCell(2);
            celdaEncReferencia.SetCellValue("REFERENCIA");


            ICell celdaEncOrdenProduccion = renglonEncabezados.CreateCell(3);
            celdaEncOrdenProduccion.SetCellValue("ORDEN_PRODUCCION");

            ICell celdaEncOrdenMaquila = renglonEncabezados.CreateCell(4);
            celdaEncOrdenMaquila.SetCellValue("ORDEN_MAQUILA");

            ICell celdaEncAlmacen = renglonEncabezados.CreateCell(5);
            celdaEncAlmacen.SetCellValue("ALMACEN");

            ICell celdaEncModelo = renglonEncabezados.CreateCell(6);
            celdaEncModelo.SetCellValue("MODELO");

            ICell celdaEncDescripcion = renglonEncabezados.CreateCell(7);
            celdaEncDescripcion.SetCellValue("DESCRIPCION");

            ICell celdaEncTalla = renglonEncabezados.CreateCell(8);
            celdaEncTalla.SetCellValue("TALLA");

            ICell celdaEncTipoLinea = renglonEncabezados.CreateCell(9);
            celdaEncTipoLinea.SetCellValue("TIPO_LINEA");

            ICell celdaEncCantidad = renglonEncabezados.CreateCell(10);
            celdaEncCantidad.SetCellValue("CANTIDAD");

            iRenglonDetalle++;

            #endregion

            #region DETALLE

            foreach (CodigoBarra _codigo in datosMaquila.OrderBy(x => x.Modelo).OrderBy(x => x.Talla))
            {
                IRow renglonDetalle = sheet.CreateRow(iRenglonDetalle);

                ICell celdaDetalle;
                //UUID

                celdaDetalle = renglonDetalle.CreateCell(0);
                celdaDetalle.SetCellValue(Seguridad.Encriptar(_codigo.UUID.Trim()));
                //CONSECUTIVO
                celdaDetalle = renglonDetalle.CreateCell(1);
                celdaDetalle.SetCellValue(Seguridad.Encriptar(_codigo.Consecutivo.ToString().Trim()));
                //REFERENCIA
                celdaDetalle = renglonDetalle.CreateCell(2);
                celdaDetalle.SetCellValue(Seguridad.Encriptar(_codigo.Referencia.ToString().Trim()));
                //ORDEN PRODUCCION
                celdaDetalle = renglonDetalle.CreateCell(3);
                celdaDetalle.SetCellValue(Seguridad.Encriptar(_codigo.OrdenProduccion.ToString().Trim()));
                //ORDEN DE MAQUILA
                celdaDetalle = renglonDetalle.CreateCell(4);
                celdaDetalle.SetCellValue(Seguridad.Encriptar(_codigo.OrdenMaquila.ToString().Trim()));
                //ALMACEN
                celdaDetalle = renglonDetalle.CreateCell(5);
                celdaDetalle.SetCellValue(Seguridad.Encriptar(_codigo.Almacen.ToString().Trim()));
                //MODELO
                celdaDetalle = renglonDetalle.CreateCell(6);
                celdaDetalle.SetCellValue(Seguridad.Encriptar(_codigo.Modelo.ToString().Trim()));
                //DESCRIPCION
                celdaDetalle = renglonDetalle.CreateCell(7);
                celdaDetalle.SetCellValue(Seguridad.Encriptar(_codigo.Descripcion.ToString().Trim()));
                //TALLA
                celdaDetalle = renglonDetalle.CreateCell(8);
                celdaDetalle.SetCellValue(Seguridad.Encriptar(_codigo.Talla.ToString().Trim()));
                //TIPO DE LINEA
                celdaDetalle = renglonDetalle.CreateCell(9);
                celdaDetalle.SetCellValue(Seguridad.Encriptar(_codigo.Tipo.ToString().Trim()));
                //CANTIDAD
                celdaDetalle = renglonDetalle.CreateCell(10);
                celdaDetalle.SetCellValue(Seguridad.Encriptar(_codigo.Cantidad.ToString().Trim()));




                iRenglonDetalle++;
            }

            #endregion

            sheet.SetColumnWidth(0, ExcelNpoiUtil.AnchoColumna(100)); //REFERENCIA
            sheet.SetColumnWidth(1, ExcelNpoiUtil.AnchoColumna(100)); //OP
            sheet.SetColumnWidth(2, ExcelNpoiUtil.AnchoColumna(100)); //OM
            sheet.SetColumnWidth(3, ExcelNpoiUtil.AnchoColumna(0)); //MODELO
            sheet.SetColumnWidth(4, ExcelNpoiUtil.AnchoColumna(350)); //DESCRIPCION
            sheet.SetColumnWidth(5, ExcelNpoiUtil.AnchoColumna(100)); //TALLA
            sheet.SetColumnWidth(6, ExcelNpoiUtil.AnchoColumna(170)); //CANTIDAD TOTAL
            sheet.SetColumnWidth(7, ExcelNpoiUtil.AnchoColumna(170)); //DEFECTUOSOS

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

        public static DataTable ConsultaCodigoBarras(String _UUID, int _Consecutivo)
        {
            DataTable dtCodigoBarras = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand guarda = new sm_dl.SqlServer.SqlServerCommand();
                guarda.Connection = sm_dl.DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                guarda.ObjectName = "usp_ConsultaCodigoBarras";
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UUID", _UUID));
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Consecutivo", _Consecutivo));
                dtCodigoBarras = guarda.GetDataTable();
            }
            return dtCodigoBarras;
        }
        public static DataTable ConsultaCodigoBarrasEscaneados(String _UUID, int _Consecutivo)
        {
            DataTable dtCodigoBarras = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand guarda = new sm_dl.SqlServer.SqlServerCommand();
                guarda.Connection = sm_dl.DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                guarda.ObjectName = "usp_ConsultaCodigoBarrasEscaneado";
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UUID", _UUID));
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Consecutivo", _Consecutivo));
                dtCodigoBarras = guarda.GetDataTable();
            }
            return dtCodigoBarras;
        }
        public static DataTable ConsultaCodigoBarrasByOrdenMaquila(String _OrdenMaquila)
        {
            DataTable dtCodigoBarras = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand guarda = new sm_dl.SqlServer.SqlServerCommand();
                guarda.Connection = sm_dl.DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                guarda.ObjectName = "usp_ConsultaCodigoBarrasByOrdenMaquila";
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ordenMaquila", _OrdenMaquila));
                dtCodigoBarras = guarda.GetDataTable();
            }
            return dtCodigoBarras;
        }
        public static DataTable ConsultaCodigoBarrasByOrdenMaquilaSAE(String _OrdenMaquila, String _OrdenCompra)
        {
            DataTable dtCodigoBarras = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand guarda = new sm_dl.SqlServer.SqlServerCommand();
                guarda.Connection = sm_dl.DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                guarda.ObjectName = "usp_ConsultaCodigoBarrasByOrdenMaquilaSAE";
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ordenMaquila", _OrdenMaquila));
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ordenProduccion", _OrdenCompra));
                dtCodigoBarras = guarda.GetDataTable();
            }
            return dtCodigoBarras;
        }

        public static DataTable ConsultaCodigoDeBarrasModelosEspeciales()
        {
            DataTable dtCodigoBarras = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand guarda = new sm_dl.SqlServer.SqlServerCommand();
                guarda.Connection = sm_dl.DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                guarda.ObjectName = "usp_ConsultaCodigoBarrasModelosEspeciales";
                dtCodigoBarras = guarda.GetDataTable();
            }
            return dtCodigoBarras;
        }
    }
}
