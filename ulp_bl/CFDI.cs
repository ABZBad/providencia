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
    public class CFDI
    {

        public enum TipoCuenta
        {
            CXP,
            CXC
        }

        public String UUID { get; set; }
        public String RFCEmisor { get; set; }
        public String RFCReceptor { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime FechaTimbrado { get; set; }
        public String TipoCFDI { get; set; }
        public String metodoPago { get; set; }
        public String formaPago { get; set; }
        public String usoCFDI { get; set; }
        public String Serie { get; set; }
        public String Folio { get; set; }
        public String XML { get; set; }
        public decimal Subtotal { get; set; }
        public decimal IVA { get; set; }
        public decimal Total { get; set; }

        public decimal TotalPorTipoCFDI
        {
            get
            {
                if (TipoCFDI == "Pago")
                    return this.ListaPago.Sum(x => x.Monto);
                else
                    return this.Total;
            }
        }
        public List<ComplementoPago> ListaPago { get; set; }


        public CFDI()
        {
            this.UUID = "";
            this.RFCEmisor = "";
            this.RFCReceptor = "";
            this.FechaEmision = DateTime.Now;
            this.FechaTimbrado = DateTime.Now;
            this.TipoCFDI = "";
            this.metodoPago = "";
            this.formaPago = "";
            this.usoCFDI = "";
            this.Serie = "";
            this.Folio = "";
            this.XML = "";
            this.Subtotal = 0;
            this.IVA = 0;
            this.Total = 0;
            this.ListaPago = new List<ComplementoPago> { };
        }
        public class ComplementoPago
        {
            public String CuentaBenf { get; set; }
            public String RFCBenf { get; set; }
            public String CuentaOrd { get; set; }
            public String RFCOrd { get; set; }
            public decimal Monto { get; set; }
            public DateTime FechaPago { get; set; }
            public List<DocumentoRelacionado> ListaDocumentosRelacionados { get; set; }

            public ComplementoPago()
            {
                this.CuentaBenf = "";
                this.RFCBenf = "";
                this.RFCOrd = "";
                this.Monto = 0;
                this.FechaPago = DateTime.Now;
                this.ListaDocumentosRelacionados = new List<DocumentoRelacionado> { };
            }

            public class DocumentoRelacionado
            {
                public String MetodoPago { get; set; }
                public String Serie { get; set; }
                public String Folio { get; set; }
                public decimal ImportePagado { get; set; }
                public decimal ImporteAnterior { get; set; }
                public String idDocumento { get; set; }
                public DocumentoRelacionado()
                {
                    this.MetodoPago = "";
                    this.Serie = "";
                    this.Folio = "";
                    this.ImportePagado = 0;
                    this.ImporteAnterior = 0;
                    this.idDocumento = "";
                }
            }
        }

        public static void setAltaCFDI(CFDI oCFDI)
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
                cmd.ObjectName = "usp_AltaCFDI";
                cmd.Parameters.Add(new SqlParameter("@UUID", oCFDI.UUID));
                cmd.Parameters.Add(new SqlParameter("@rfcEmisor", oCFDI.RFCEmisor));
                cmd.Parameters.Add(new SqlParameter("@rfcReceptor", oCFDI.RFCReceptor));
                cmd.Parameters.Add(new SqlParameter("@fechaEmision", oCFDI.FechaEmision));
                cmd.Parameters.Add(new SqlParameter("@fechaTimbrado", oCFDI.FechaTimbrado));
                cmd.Parameters.Add(new SqlParameter("@tipoCFDI", oCFDI.TipoCFDI));
                cmd.Parameters.Add(new SqlParameter("@metodoPago", oCFDI.metodoPago));
                cmd.Parameters.Add(new SqlParameter("@formaPago", oCFDI.formaPago));
                cmd.Parameters.Add(new SqlParameter("@usoCFDI", oCFDI.usoCFDI));
                cmd.Parameters.Add(new SqlParameter("@serie", oCFDI.Serie));
                cmd.Parameters.Add(new SqlParameter("@folio", oCFDI.Folio));
                cmd.Parameters.Add(new SqlParameter("@subtotal", oCFDI.Subtotal));
                cmd.Parameters.Add(new SqlParameter("@IVA", oCFDI.IVA));
                cmd.Parameters.Add(new SqlParameter("@total", oCFDI.TotalPorTipoCFDI));
                cmd.Parameters.Add(new SqlParameter("@xml", oCFDI.XML));

                int i = cmd.Execute();
                cmd.Connection.Close();
            }
            catch { }
        }
        public static void setAltaCFDIPago(String UUID, CFDI.ComplementoPago oComplemento)
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
                cmd.ObjectName = "usp_AltaCFDIPago";
                cmd.Parameters.Add(new SqlParameter("@UUID", UUID));
                cmd.Parameters.Add(new SqlParameter("@ctaBeneficiario ", oComplemento.CuentaBenf));
                cmd.Parameters.Add(new SqlParameter("@rfcEmisorBeneficiario", oComplemento.RFCBenf));
                cmd.Parameters.Add(new SqlParameter("@ctaOrdenante", oComplemento.CuentaOrd));
                cmd.Parameters.Add(new SqlParameter("@rfcEmisorOrdenante", oComplemento.RFCOrd));
                cmd.Parameters.Add(new SqlParameter("@monto", oComplemento.Monto));
                cmd.Parameters.Add(new SqlParameter("@fechaPago", oComplemento.FechaPago));
                SqlParameter _out = new SqlParameter("@idPago", 0);
                _out.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(_out);
                cmd.Execute();

                if (int.Parse(_out.Value.ToString()) > 0)
                {
                    cmd = new SqlServerCommand();
                    cmd.Connection = DALUtil.GetConnection(conStr);
                    cmd.ObjectName = "usp_AltaCFDIPagoRelacionado";
                    foreach (CFDI.ComplementoPago.DocumentoRelacionado _relacionado in oComplemento.ListaDocumentosRelacionados)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SqlParameter("@idPago", int.Parse(_out.Value.ToString())));
                        cmd.Parameters.Add(new SqlParameter("@metodoPago", _relacionado.MetodoPago));
                        cmd.Parameters.Add(new SqlParameter("@folio", _relacionado.Folio));
                        cmd.Parameters.Add(new SqlParameter("@serie", _relacionado.Serie));
                        cmd.Parameters.Add(new SqlParameter("@importePagado", _relacionado.ImportePagado));
                        cmd.Parameters.Add(new SqlParameter("@importeAnterior", _relacionado.ImporteAnterior));
                        cmd.Parameters.Add(new SqlParameter("@idDocumento", _relacionado.idDocumento));
                        cmd.Execute();
                    }
                }
                cmd.Connection.Close();

            }
            catch { }
        }
        public static DataTable getReprotePPD_CXP(DateTime fechaDesde, DateTime fechaHasta)
        {
            String conStr = "";
            DataTable dtBitacora = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                conStr = dbContext.Database.Connection.ConnectionString;
            }
            SqlServerCommand cmd = new SqlServerCommand();
            cmd.Connection = DALUtil.GetConnection(conStr);
            cmd.ObjectName = "usp_ConsultaReportePPD_CXP";
            cmd.Parameters.Add(new SqlParameter("@fechaInicio", fechaDesde));
            cmd.Parameters.Add(new SqlParameter("@fechaFin", fechaHasta));
            dtBitacora = cmd.GetDataTable();
            cmd.Connection.Close();
            return dtBitacora;
        }
        public static DataTable getReprotePPD_CXC(DateTime fechaDesde, DateTime fechaHasta)
        {
            String conStr = "";
            DataTable dtBitacora = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                conStr = dbContext.Database.Connection.ConnectionString;
            }
            SqlServerCommand cmd = new SqlServerCommand();
            cmd.Connection = DALUtil.GetConnection(conStr);
            cmd.ObjectName = "usp_ConsultaReportePPD_CXC";
            cmd.Parameters.Add(new SqlParameter("@fechaInicio", fechaDesde));
            cmd.Parameters.Add(new SqlParameter("@fechaFin", fechaHasta));
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
            rngEmitidoDel.CreateCell(0).SetCellValue(string.Format("Emitido del      {0}", FechaDesde.ToShortDateString()));

            IRow rngEmitidoAl = sheet.CreateRow(2); //renglón: "Al.."
            rngEmitidoAl.CreateCell(0).SetCellValue(string.Format("Al                   {0}", FechaHasta.ToShortDateString()));

            #endregion

            #region Encabezados

            IRow rngEncabezados = sheet.CreateRow(4);
            int i = 0;
            rngEncabezados.CreateCell(i).SetCellValue("Metodo"); i++;
            rngEncabezados.CreateCell(i).SetCellValue("Factura"); i++;
            rngEncabezados.CreateCell(i).SetCellValue("RFC Emisor"); i++;
            rngEncabezados.CreateCell(i).SetCellValue("Serie"); i++;
            rngEncabezados.CreateCell(i).SetCellValue("Folio"); i++;
            rngEncabezados.CreateCell(i).SetCellValue("F. Emisión"); i++;
            rngEncabezados.CreateCell(i).SetCellValue("F. Timbrado"); i++;
            rngEncabezados.CreateCell(i).SetCellValue("Subtotal Factura"); i++;
            rngEncabezados.CreateCell(i).SetCellValue("IVA Factura"); i++;
            rngEncabezados.CreateCell(i).SetCellValue("Total Factura"); i++;
            rngEncabezados.CreateCell(i).SetCellValue("Total Pagado"); i++;
            rngEncabezados.CreateCell(i).SetCellValue("Complemento"); i++;
            rngEncabezados.CreateCell(i).SetCellValue("Cta. Beneficiario"); i++;
            rngEncabezados.CreateCell(i).SetCellValue("Cta. Ordenante"); i++;
            rngEncabezados.CreateCell(i).SetCellValue("Total Pago"); i++;
            rngEncabezados.CreateCell(i).SetCellValue("Fecha Pago"); i++;
            rngEncabezados.CreateCell(i).SetCellValue("Poliza Pago"); i++;
            rngEncabezados.CreateCell(i).SetCellValue("Uso CFDI"); i++;

            #endregion

            #region Detalle

            int iRenglonDetalle = 5;

            foreach (DataRow _dr in dtBitacora.Rows)
            {

                IRow renglonDetalle = sheet.CreateRow(iRenglonDetalle);
                i = 0;
                renglonDetalle.CreateCell(i).SetCellValue(_dr["metodo"].ToString()); i++;
                renglonDetalle.CreateCell(i).SetCellValue(_dr["uuid"].ToString()); i++;
                renglonDetalle.CreateCell(i).SetCellValue(_dr["rfcEmisor"].ToString()); i++;
                renglonDetalle.CreateCell(i).SetCellValue(_dr["serie"].ToString()); i++;
                renglonDetalle.CreateCell(i).SetCellValue(_dr["folio"].ToString()); i++;
                renglonDetalle.CreateCell(i).SetCellValue(DateTime.Parse(_dr["fechaFactura"].ToString()).ToString("dd/MM/yyyy")); i++;
                renglonDetalle.CreateCell(i).SetCellValue(DateTime.Parse(_dr["fechaTimbradoFactura"].ToString()).ToString("dd/MM/yyyy")); i++;
                renglonDetalle.CreateCell(i).SetCellValue(double.Parse(_dr["subtotalFactura"].ToString())); i++;
                renglonDetalle.CreateCell(i).SetCellValue(double.Parse(_dr["ivaFactura"].ToString())); i++;
                renglonDetalle.CreateCell(i).SetCellValue(double.Parse(_dr["totalFactura"].ToString())); i++;
                renglonDetalle.CreateCell(i).SetCellValue(double.Parse(_dr["totalPagado"].ToString())); i++;
                renglonDetalle.CreateCell(i).SetCellValue(_dr["uuidComplemento"].ToString()); i++;
                renglonDetalle.CreateCell(i).SetCellValue(_dr["cuentaBeneficiario"].ToString()); i++;
                renglonDetalle.CreateCell(i).SetCellValue(_dr["ctaOrdenante"].ToString()); i++;
                renglonDetalle.CreateCell(i).SetCellValue(double.Parse(_dr["importePagado"].ToString())); i++;
                renglonDetalle.CreateCell(i).SetCellValue(_dr["fechaPago"].ToString()); i++;
                renglonDetalle.CreateCell(i).SetCellValue(_dr["polizaPago"].ToString()); i++;
                renglonDetalle.CreateCell(i).SetCellValue(_dr["usoCFDI"].ToString()); i++;

                iRenglonDetalle++;
            }


            for (i = 0; i <= 17; i++)
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
