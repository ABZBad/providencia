using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIP;
using sm_dl;
using sm_dl.SqlServer;
//using ulp_dl.aspel_prod30;
using ulp_dl.aspel_sae80;
using ulp_dl.SIPNegocio;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;


namespace ulp_bl
{
    public class CFDIPAC
    {

        public enum SeriesFAC
        {
            A,
            B
        }
        public enum SeriesNC
        {
            C,
            D
        }

        public enum AlmacenesFAC
        {
            [Description("Almacen 4")]
            Almacen_4 = 4
        }
        public enum AlmacenesNC
        {
            [Description("Almacen ")]
            Almacen_4 = 4
        }

        public static int setAltaCSD(String RFC, String noCer, DateTime fechaDesde, DateTime fechaHasta, Byte[] byteCer, Byte[] byteKey, String pass)
        {
            int i = 0;

            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand ejecuta = new sm_dl.SqlServer.SqlServerCommand();
                ejecuta.Connection = sm_dl.DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                ejecuta.ObjectName = "usp_AltaCSD";
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@rfc", RFC));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@noCertificado", noCer));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@fechaDesde", fechaDesde));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@fechaHasta", fechaHasta));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@byteCer", byteCer));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@byteKey", byteKey));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@pass", pass));
                i = ejecuta.Execute();
            }

            return i;
        }
        public static DataTable getCSD()
        {
            DataTable dtCSD = new DataTable();
            string connStr = "";
            using (var dbContext = new SIPNegocioContext())
            {
                connStr = dbContext.Database.Connection.ConnectionString;
            }
            SqlServerCommand cmd = new SqlServerCommand();
            cmd.Connection = DALUtil.GetConnection(connStr);
            cmd.ObjectName = "usp_ConsultaCSD";
            dtCSD = cmd.GetDataTable();
            cmd.Connection.Close();
            return dtCSD;
        }
        public static DataTable getPedidosPorFacturar()
        {
            DataTable dtPedidos = new DataTable();
            string connStr = "";
            using (var dbContext = new SIPNegocioContext())
            {
                connStr = dbContext.Database.Connection.ConnectionString;
            }
            SqlServerCommand cmd = new SqlServerCommand();
            cmd.Connection = DALUtil.GetConnection(connStr);
            cmd.ObjectName = "usp_ConsultaPedidosPorFacturar";
            dtPedidos = cmd.GetDataTable();
            cmd.Connection.Close();
            return dtPedidos;
        }
        public static DataSet getDetallePedido(int Pedido, string serie, int almacen)
        {
            DataSet dtPedidos = new DataSet();
            string connStr = "";
            using (var dbContext = new SIPNegocioContext())
            {
                connStr = dbContext.Database.Connection.ConnectionString;
            }
            SqlServerCommand cmd = new SqlServerCommand();
            cmd.Connection = DALUtil.GetConnection(connStr);
            cmd.ObjectName = "usp_ConsultaPedido";
            cmd.Parameters.Add(new SqlParameter("@pedido", Pedido));
            cmd.Parameters.Add(new SqlParameter("@serie", serie));
            cmd.Parameters.Add(new SqlParameter("@almacen", almacen));
            dtPedidos = cmd.GetDataSet();
            cmd.Connection.Close();
            return dtPedidos;
        }
        public static int setAltaCFDI(
            String _UUID,
            String _Pedido,
            String _XML,
            String _rfcEmisor,
            String _rfcReceptor,
            String _fecha,
            String _subtotal,
            String _iva,
            String _total,
            String _serie,
            String _folio,
            String _sello,
            String _certificado,
            String _noCerficiado,
            String _fechaTimbrado,
            String _metodoPago,
            String _formaPago,
            int _numAlma,
            string _usuario,
            Boolean _activa
            )
        {
            int i = 0;

            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand ejecuta = new sm_dl.SqlServer.SqlServerCommand();
                ejecuta.Connection = sm_dl.DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                ejecuta.ObjectName = "usp_setAltaCFDIPedido";

                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UUID", _UUID));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Pedido", _Pedido));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@XML", _XML));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@rfcEmisor", _rfcEmisor));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@rfcReceptor", _rfcReceptor));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@fecha", DateTime.Parse(_fecha)));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@subtotal", Decimal.Parse(_subtotal)));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@iva", Decimal.Parse(_iva)));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@total", Decimal.Parse(_total)));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@serie", _serie));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@folio", int.Parse(_folio)));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@sello", _sello));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@certificado", _certificado));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@noCertificado", _noCerficiado));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@fechaTimbrado", _fechaTimbrado));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@metodoPago", _metodoPago));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@formaPago", _formaPago));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NUM_ALMA", _numAlma));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UsuarioSIP", _usuario));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@activa", _activa));

                i = ejecuta.Execute();
            }

            return i;
        }
        public static int setAltaCFDINC(
            String _UUID,
            String _Pedido,
            String _XML,
            String _rfcEmisor,
            String _rfcReceptor,
            String _fecha,
            String _subtotal,
            String _iva,
            String _total,
            String _serie,
            String _folio,
            String _sello,
            String _certificado,
            String _noCerficiado,
            String _fechaTimbrado,
            String _metodoPago,
            String _formaPago,
            String _xmlDetalle,
            int _numAlma,
            string _usuario,
            Boolean _activa,
            String _cve_clie,
            String _usoCFDI,
            String _tipRel,
            String _observaciones
            )
        {
            int i = 0;

            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand ejecuta = new sm_dl.SqlServer.SqlServerCommand();
                ejecuta.Connection = sm_dl.DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                ejecuta.ObjectName = "usp_setAltaCFDINC";

                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UUID", _UUID));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Pedido", _Pedido));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@XML", _XML));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@rfcEmisor", _rfcEmisor));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@rfcReceptor", _rfcReceptor));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@fecha", DateTime.Parse(_fecha)));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@subtotal", Decimal.Parse(_subtotal)));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@iva", Decimal.Parse(_iva)));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@total", Decimal.Parse(_total)));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@serie", _serie));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@folio", int.Parse(_folio)));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@sello", _sello));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@certificado", _certificado));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@noCertificado", _noCerficiado));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@fechaTimbrado", DateTime.Parse(_fechaTimbrado)));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@metodoPago", _metodoPago));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@formaPago", _formaPago));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@xmlDetalle", _xmlDetalle));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NUM_ALMA", _numAlma));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UsuarioSIP", _usuario));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@activa", _activa));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CVE_CLIE", _cve_clie));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@uso_cfdi", _usoCFDI));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@tip_rel", _tipRel));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@observaciones", _observaciones));

                i = ejecuta.Execute();
            }

            return i;
        }
        public static int setAltaCFDIDetalle(
            String _UUID,
            String _ClaveProdSer,
            String _NoIdentificacion,
            String _Cantidad,
            String _ClaveUnidad,
            String _Unidad,
            String _Descripcion,
            String _ValorUnitario,
            String _Importe,
            String _IVA
            )
        {
            int i = 0;

            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand ejecuta = new sm_dl.SqlServer.SqlServerCommand();
                ejecuta.Connection = sm_dl.DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                ejecuta.ObjectName = "usp_setAltaCFDIPedidoDetalle";

                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@_UUID_CFDIPedido", _UUID));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ClaveProdServ", _ClaveProdSer));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NoIdentificacion", _NoIdentificacion));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Cantidad", decimal.Parse(_Cantidad)));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ClaveUnidad", _ClaveUnidad));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Unidad", _Unidad));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Descripcion", _Descripcion));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ValorUnitario", Decimal.Parse(_ValorUnitario)));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Importe", Decimal.Parse(_ValorUnitario)));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IVA", Decimal.Parse(_IVA)));

                i = ejecuta.Execute();
            }

            return i;
        }
        public static DataTable getFacturasParaNC(String _busqueda)
        {
            DataTable dtPedidos = new DataTable();
            string connStr = "";
            using (var dbContext = new SIPNegocioContext())
            {
                connStr = dbContext.Database.Connection.ConnectionString;
            }
            SqlServerCommand cmd = new SqlServerCommand();
            cmd.Connection = DALUtil.GetConnection(connStr);
            cmd.ObjectName = "usp_ConsultaFacturasParaNC";
            cmd.Parameters.Add(new SqlParameter("@busqueda", _busqueda));
            dtPedidos = cmd.GetDataTable();
            cmd.Connection.Close();
            return dtPedidos;
        }
        public static DataSet getDetalleFactura(String _factura)
        {
            DataSet dsFactura = new DataSet();
            string connStr = "";
            using (var dbContext = new SIPNegocioContext())
            {
                connStr = dbContext.Database.Connection.ConnectionString;
            }
            SqlServerCommand cmd = new SqlServerCommand();
            cmd.Connection = DALUtil.GetConnection(connStr);
            cmd.ObjectName = "usp_ConsultaFacturaDetalle";
            cmd.Parameters.Add(new SqlParameter("@CVE_DOC", _factura));
            dsFactura = cmd.GetDataSet();
            cmd.Connection.Close();
            return dsFactura;
        }
        public static DataTable getCatalogoTipoRelacion()
        {
            DataTable dtCSD = new DataTable();
            string connStr = "";
            using (var dbContext = new SIPNegocioContext())
            {
                connStr = dbContext.Database.Connection.ConnectionString;
            }
            SqlServerCommand cmd = new SqlServerCommand();
            cmd.Connection = DALUtil.GetConnection(connStr);
            cmd.ObjectName = "usp_ConsultaTipoRelacion";
            dtCSD = cmd.GetDataTable();
            cmd.Connection.Close();
            return dtCSD;
        }
        public static string getFolio(String _factura, String _tipoDoc)
        {
            DataTable dtFolio = new DataTable();
            string connStr = "";
            using (var dbContext = new SIPNegocioContext())
            {
                connStr = dbContext.Database.Connection.ConnectionString;
            }
            SqlServerCommand cmd = new SqlServerCommand();
            cmd.Connection = DALUtil.GetConnection(connStr);
            cmd.ObjectName = "usp_ConsultaFolioParaCFDI";
            cmd.Parameters.Add(new SqlParameter("@SERIE", _factura));
            cmd.Parameters.Add(new SqlParameter("@TIPO_DOC", _tipoDoc));
            dtFolio = cmd.GetDataTable();
            if (dtFolio.Rows.Count > 0)
            {
                return dtFolio.Rows[0]["ULT_DOC"].ToString();
            }
            else
            {
                return "";
            }
        }
        public static DataTable setAltaNCIngreso(
            String _xmlDetalle,
            int _numAlma,
            string _serie,
            int _folio,
            string _cve_clie,
            bool _cancelacion
            )
        {
            DataTable dtResult = new DataTable();

            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand ejecuta = new sm_dl.SqlServer.SqlServerCommand();
                ejecuta.Connection = sm_dl.DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                ejecuta.ObjectName = "usp_setAltaNCIngreso";
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@xmlDetalle", _xmlDetalle));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NUM_ALMA", _numAlma));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SERIE", _serie));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FOLIO", _folio));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CVE_CLIE", _cve_clie));
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CANCELACION", _cancelacion));
                dtResult = ejecuta.GetDataTable();
                ejecuta.Connection.Close();
                return dtResult;
            }


        }
        public static void GeneraArchivoExcelMovimientos(Boolean Cancelacion, DateTime Fecha, String Serie, String Folio, String CVE_CLIE, List<MINVE01> Movimientos, string RutaYNombreArchivo)
        {
            HSSFWorkbook xlsWorkBook = new HSSFWorkbook();
            ISheet sheet = xlsWorkBook.CreateSheet("Hoja1");
            int renglonDetalle = 5;

            #region Titulo
            IRow renglonTitulo = sheet.CreateRow(0);            //creación del renglón donde va el Título
            renglonTitulo.CreateCell(0).SetCellValue("REPORTE DE INGRESO DE FACTURA A INVENTARIO");
            IRow renglonSubTitulo = sheet.CreateRow(1);            //creación del renglón donde va el Título
            renglonSubTitulo.CreateCell(0).SetCellValue(String.Format("Serie {0} Folio {1} Cliente {2}. Fecha de Ingreso: {3}", Serie, Folio, CVE_CLIE, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")));

            #endregion
            #region Encabezado
            IRow renglonEncabezados = sheet.CreateRow(4);

            renglonEncabezados.CreateCell(0).SetCellValue("CVE_ARTI"); //ENCABEZADOS (modelo, descripción dmoelo)
            renglonEncabezados.CreateCell(1).SetCellValue("ALMACEN");
            renglonEncabezados.CreateCell(2).SetCellValue("NUM_MOV");
            renglonEncabezados.CreateCell(3).SetCellValue("FECHA");
            renglonEncabezados.CreateCell(4).SetCellValue("REFER");
            renglonEncabezados.CreateCell(5).SetCellValue("CANTITDAD");
            renglonEncabezados.CreateCell(6).SetCellValue("PRECIO");

            #endregion
            #region Detalle de movimientos

            foreach (MINVE01 movimiento in Movimientos)
            {
                IRow renglonDetalleEstructura = sheet.CreateRow(renglonDetalle);
                renglonDetalleEstructura.CreateCell(0).SetCellValue(movimiento.CVE_ART);
                renglonDetalleEstructura.CreateCell(1).SetCellValue(movimiento.ALMACEN);
                renglonDetalleEstructura.CreateCell(2).SetCellValue(movimiento.NUM_MOV);
                renglonDetalleEstructura.CreateCell(3).SetCellValue(DateTime.Parse(movimiento.FECHA_DOCU.ToString()).ToString("dd/MM/yyyy"));
                renglonDetalleEstructura.CreateCell(4).SetCellValue(movimiento.REFER);
                renglonDetalleEstructura.CreateCell(5).SetCellValue(movimiento.CANT.ToString());
                renglonDetalleEstructura.CreateCell(6).SetCellValue(movimiento.PRECIO.ToString());
                renglonDetalle++;
            }
            #endregion
            #region Ajuste de los anchos de columna
            //Ajuste de los anchos de columna
            sheet.SetColumnWidth(0, 50); // TITULOS
            for (int i = 1; i < 6; i++)
            {
                sheet.AutoSizeColumn(i);
            }
            #endregion
            if (File.Exists(RutaYNombreArchivo))
            {
                File.Delete(RutaYNombreArchivo);
            }

            FileStream fs = new FileStream(RutaYNombreArchivo, FileMode.CreateNew);

            xlsWorkBook.Write(fs);


            fs.Close();
        }
        public static DataTable getReporteIngresoFacturacionSAT(DateTime FechaInicio, DateTime FechaFin)
        {
            DataTable dtPedidos = new DataTable();
            string connStr = "";
            using (var dbContext = new SIPNegocioContext())
            {
                connStr = dbContext.Database.Connection.ConnectionString;
            }
            SqlServerCommand cmd = new SqlServerCommand();
            cmd.Connection = DALUtil.GetConnection(connStr);
            cmd.ObjectName = "usp_ConsultaIngresosFacturacionSAT";
            cmd.Parameters.Add(new SqlParameter("@FechaInicio", FechaInicio));
            cmd.Parameters.Add(new SqlParameter("@FechaFin", FechaFin));
            dtPedidos = cmd.GetDataTable();
            cmd.Connection.Close();
            return dtPedidos;
        }
        public static void GeneraArchivoExcelIngresoFacturacionSAT(DataTable dtResult, DateTime FechaInicio, DateTime FechaFin, string RutaYNombreArchivo)
        {
            HSSFWorkbook xlsWorkBook = new HSSFWorkbook();
            ISheet sheet = xlsWorkBook.CreateSheet("Hoja1");
            int renglonDetalle = 5;

            #region Titulo
            IRow renglonTitulo = sheet.CreateRow(0);            //creación del renglón donde va el Título
            renglonTitulo.CreateCell(0).SetCellValue("REPORTE DE INGRESO DE FACTURACIÓN SAT");
            IRow renglonSubTitulo = sheet.CreateRow(1);            //creación del renglón donde va el Título
            renglonSubTitulo.CreateCell(0).SetCellValue(String.Format("Fecha desde: {0} hasta: {1}", FechaInicio, FechaFin));

            #endregion
            #region Encabezado
            IRow renglonEncabezados = sheet.CreateRow(3);

            renglonEncabezados.CreateCell(0).SetCellValue("SERIE");
            renglonEncabezados.CreateCell(1).SetCellValue("FOLIO");
            renglonEncabezados.CreateCell(2).SetCellValue("CVE_CLIE");
            renglonEncabezados.CreateCell(3).SetCellValue("FECHA INGRESO");
            renglonEncabezados.CreateCell(4).SetCellValue("CVE_ARTI");
            renglonEncabezados.CreateCell(5).SetCellValue("ALMACEN");
            renglonEncabezados.CreateCell(6).SetCellValue("NUM_MOV");
            renglonEncabezados.CreateCell(7).SetCellValue("REFER");
            renglonEncabezados.CreateCell(8).SetCellValue("CANTITDAD");
            renglonEncabezados.CreateCell(9).SetCellValue("PRECIO");

            #endregion
            #region Detalle de movimientos

            foreach (DataRow dr in dtResult.Rows)
            {
                IRow renglonDetalleEstructura = sheet.CreateRow(renglonDetalle);
                renglonDetalleEstructura.CreateCell(0).SetCellValue(dr["Serie"].ToString());
                renglonDetalleEstructura.CreateCell(1).SetCellValue(dr["Folio"].ToString());
                renglonDetalleEstructura.CreateCell(2).SetCellValue(dr["CVE_CLIE"].ToString());
                renglonDetalleEstructura.CreateCell(3).SetCellValue(DateTime.Parse(dr["FechaIngreso"].ToString()).ToString("dd/MM/yyyy"));
                renglonDetalleEstructura.CreateCell(4).SetCellValue(dr["CVE_ART"].ToString());
                renglonDetalleEstructura.CreateCell(5).SetCellValue(dr["Almacen"].ToString());
                renglonDetalleEstructura.CreateCell(6).SetCellValue(dr["NUM_MOV"].ToString());
                renglonDetalleEstructura.CreateCell(7).SetCellValue(dr["REFER"].ToString());
                renglonDetalleEstructura.CreateCell(8).SetCellValue(dr["CANT"].ToString());
                renglonDetalleEstructura.CreateCell(9).SetCellValue(dr["PRECIO"].ToString());
                renglonDetalle++;
            }
            #endregion
            #region Ajuste de los anchos de columna
            //Ajuste de los anchos de columna
            sheet.SetColumnWidth(0, 50); // TITULOS
            for (int i = 1; i < 9; i++)
            {
                sheet.AutoSizeColumn(i);
            }
            #endregion
            if (File.Exists(RutaYNombreArchivo))
            {
                File.Delete(RutaYNombreArchivo);
            }

            FileStream fs = new FileStream(RutaYNombreArchivo, FileMode.CreateNew);

            xlsWorkBook.Write(fs);


            fs.Close();
        }
    }
}
