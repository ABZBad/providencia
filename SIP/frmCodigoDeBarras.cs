using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using ulp_bl;

namespace SIP
{
    public partial class frmCodigoDeBarras : Form
    {
        List<CodigoBarra> ListaCodigos;
        String numeroPedido;
        int ordenMaquila;
        Boolean export = false;
        String path = "";

        public frmCodigoDeBarras(String numeroPedido, int ordenMaquila, List<CodigoBarra> codigos, bool export = false, string path = "")
        {
            InitializeComponent();
            this.ListaCodigos = codigos;
            this.numeroPedido = numeroPedido;
            this.ordenMaquila = ordenMaquila;
            this.export = export;
            this.path = path;
            if (export)
            {
                this.Exporta();
            }
        }

        private void frmCodigoDeBarras_Load(object sender, EventArgs e)
        {
            MemoryStream ms = new MemoryStream();
            byte[] imgBarCodeByte;
            Reportes.Image dsImage = new Reportes.Image();
            DataRow rowImage;
            Image imgBarCode;

            foreach (CodigoBarra _codigo in this.ListaCodigos)
            {
                ms = new MemoryStream();
                imgBarCode = BarcodeLib.Barcode.DoEncode(BarcodeLib.TYPE.CODE39, _codigo.UUID + " " + _codigo.Consecutivo.ToString(), false, 800, 300);
                imgBarCode.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                imgBarCodeByte = ms.ToArray();

                rowImage = dsImage.myImage.NewRow();
                rowImage["image"] = imgBarCodeByte;
                rowImage["descripcion"] = string.Format("{0}|{1}|{2}|{3}", _codigo.Descripcion.ToLower(), _codigo.Talla, _codigo.Cantidad, _codigo.Referencia.ToLower()).ToUpper();
                rowImage["contador"] = _codigo.Contador;
                dsImage.myImage.Rows.Add(rowImage);
            }

            Reportes.rptCodigosBarras rptCodigo = new Reportes.rptCodigosBarras();
            rptCodigo.Load();
            rptCodigo.SetDataSource(dsImage);
            crystalReportViewer.ReportSource = rptCodigo;
        }
        private void Exporta()
        {
            MemoryStream ms = new MemoryStream();
            byte[] imgBarCodeByte;
            Reportes.Image dsImage = new Reportes.Image();
            DataRow rowImage;
            Image imgBarCode;

            foreach (CodigoBarra _codigo in this.ListaCodigos)
            {
                ms = new MemoryStream();
                imgBarCode = BarcodeLib.Barcode.DoEncode(BarcodeLib.TYPE.CODE39, _codigo.UUID + " " + _codigo.Consecutivo.ToString(), false, 800, 300);
                imgBarCode.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                imgBarCodeByte = ms.ToArray();

                rowImage = dsImage.myImage.NewRow();
                rowImage["image"] = imgBarCodeByte;
                rowImage["descripcion"] = string.Format("{0}|{1}|{2}|{3}", _codigo.Descripcion.ToLower(), _codigo.Talla, _codigo.Cantidad, _codigo.Referencia.ToLower()).ToUpper();
                rowImage["contador"] = _codigo.Contador;
                dsImage.myImage.Rows.Add(rowImage);
            }

            Reportes.rptCodigosBarras rptCodigo = new Reportes.rptCodigosBarras();
            rptCodigo.Load();
            rptCodigo.SetDataSource(dsImage);
            rptCodigo.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, this.path);
        }
    }
}
