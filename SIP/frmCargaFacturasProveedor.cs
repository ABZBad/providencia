using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ulp_bl;

namespace SIP
{
    public partial class frmCargaFacturasProveedor : Form
    {
        List<CFDI> LisaCFDI;
        public frmCargaFacturasProveedor()
        {
            InitializeComponent();
            this.LisaCFDI = new List<CFDI> { };
            this.dgvCDFI.AutoGenerateColumns = false;
        }

        private void frmFacProv_Load(object sender, EventArgs e)
        {

        }

        private void btnSelección_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (openFileDialog.FileNames.Length > 0)
                {
                    this.LisaCFDI = new List<CFDI> { };
                    //RECORREMOS CADA ARCHIVO PARA VERIFICAR SI ES UNA FACTURA, NC O COMPLEMENTO DE PAGO
                    foreach (String _fileName in openFileDialog.FileNames)
                    {
                        try
                        {
                            System.Xml.XmlDocument xml = new System.Xml.XmlDocument();
                            xml.Load(_fileName);
                            //GUARDAMOS LA INFORMACION GENERAL DEL Xml
                            CFDI oCFDI = new CFDI();
                            oCFDI.UUID = xml["cfdi:Comprobante"]["cfdi:Complemento"]["tfd:TimbreFiscalDigital"].Attributes["UUID"].Value;
                            oCFDI.RFCEmisor = xml["cfdi:Comprobante"]["cfdi:Emisor"].Attributes["Rfc"].Value;
                            oCFDI.RFCReceptor = xml["cfdi:Comprobante"]["cfdi:Receptor"].Attributes["Rfc"].Value;
                            oCFDI.FechaEmision = DateTime.Parse(xml["cfdi:Comprobante"].Attributes["Fecha"].Value);
                            oCFDI.FechaTimbrado = DateTime.Parse(xml["cfdi:Comprobante"]["cfdi:Complemento"]["tfd:TimbreFiscalDigital"].Attributes["FechaTimbrado"].Value);
                            if (xml["cfdi:Comprobante"].Attributes["TipoDeComprobante"].Value == "P")
                                oCFDI.TipoCFDI = "Pago";
                            else if (xml["cfdi:Comprobante"].Attributes["TipoDeComprobante"].Value == "I")
                                oCFDI.TipoCFDI = "Factura";
                            else
                                oCFDI.TipoCFDI = "NC";
                            if (oCFDI.TipoCFDI == "Pago")
                            {
                                oCFDI.metodoPago = "";
                                oCFDI.formaPago = "";
                                oCFDI.usoCFDI = "";
                                oCFDI.Serie = "";
                                oCFDI.Folio = "";
                                oCFDI.Subtotal = 0;
                                oCFDI.IVA = 0;
                                oCFDI.Total = 0;
                            }
                            else
                            {
                                oCFDI.Subtotal = decimal.Parse(xml["cfdi:Comprobante"].Attributes["SubTotal"].Value);
                                if (xml["cfdi:Comprobante"]["cfdi:Impuestos"] == null)
                                    oCFDI.IVA = 0;
                                else
                                    oCFDI.IVA = xml["cfdi:Comprobante"]["cfdi:Impuestos"].Attributes["TotalImpuestosTrasladados"] == null ? 0 : decimal.Parse(xml["cfdi:Comprobante"]["cfdi:Impuestos"].Attributes["TotalImpuestosTrasladados"].Value);
                                oCFDI.Total = decimal.Parse(xml["cfdi:Comprobante"].Attributes["Total"].Value);
                                oCFDI.metodoPago = xml["cfdi:Comprobante"].Attributes["MetodoPago"] == null ? "" : xml["cfdi:Comprobante"].Attributes["MetodoPago"].Value;
                                oCFDI.formaPago = xml["cfdi:Comprobante"].Attributes["FormaPago"] == null ? "" : xml["cfdi:Comprobante"].Attributes["FormaPago"].Value;
                                oCFDI.usoCFDI = xml["cfdi:Comprobante"]["cfdi:Receptor"].Attributes["UsoCFDI"] == null ? "" : xml["cfdi:Comprobante"]["cfdi:Receptor"].Attributes["UsoCFDI"].Value;
                                oCFDI.Serie = xml["cfdi:Comprobante"].Attributes["Serie"] == null ? "" : xml["cfdi:Comprobante"].Attributes["Serie"].Value;
                                oCFDI.Folio = xml["cfdi:Comprobante"].Attributes["Folio"] == null ? "" : xml["cfdi:Comprobante"].Attributes["Folio"].Value;
                            }
                            oCFDI.XML = xml.InnerXml;
                            //VERIFICAMOS SI TIENE COMPLEMENTO DE PAGO
                            if (xml["cfdi:Comprobante"]["cfdi:Complemento"]["pago10:Pagos"] != null)
                            {
                                foreach (System.Xml.XmlNode _Pago in xml["cfdi:Comprobante"]["cfdi:Complemento"]["pago10:Pagos"].ChildNodes)
                                {
                                    CFDI.ComplementoPago oPago = new CFDI.ComplementoPago();
                                    oPago.CuentaBenf = _Pago.Attributes["CtaBeneficiario"] == null ? "" : _Pago.Attributes["CtaBeneficiario"].Value;
                                    oPago.RFCBenf = _Pago.Attributes["RfcEmisorCtaBen"] == null ? "" : _Pago.Attributes["RfcEmisorCtaBen"].Value;
                                    oPago.CuentaOrd = _Pago.Attributes["CtaOrdenante"] == null ? "" : _Pago.Attributes["CtaOrdenante"].Value;
                                    oPago.RFCOrd = _Pago.Attributes["RfcEmisorCtaOrd"] == null ? "" : _Pago.Attributes["RfcEmisorCtaOrd"].Value;
                                    oPago.Monto = _Pago.Attributes["Monto"] == null ? 0 : decimal.Parse(_Pago.Attributes["Monto"].Value);
                                    oPago.FechaPago = DateTime.Parse(_Pago.Attributes["FechaPago"].Value);
                                    foreach (System.Xml.XmlNode _DocumentoRelacionado in _Pago.ChildNodes)
                                    {
                                        CFDI.ComplementoPago.DocumentoRelacionado oDocumento = new CFDI.ComplementoPago.DocumentoRelacionado();
                                        oDocumento.MetodoPago = _DocumentoRelacionado.Attributes["MetodoDePagoDR"].Value;
                                        oDocumento.Serie = _DocumentoRelacionado.Attributes["Serie"] == null ? "" : _DocumentoRelacionado.Attributes["Serie"].Value;
                                        oDocumento.Folio = _DocumentoRelacionado.Attributes["Folio"] == null ? "" : _DocumentoRelacionado.Attributes["Folio"].Value;
                                        oDocumento.ImportePagado = decimal.Parse(_DocumentoRelacionado.Attributes["ImpPagado"].Value);
                                        oDocumento.ImporteAnterior = decimal.Parse(_DocumentoRelacionado.Attributes["ImpSaldoAnt"].Value);
                                        oDocumento.idDocumento = _DocumentoRelacionado.Attributes["IdDocumento"].Value;
                                        oPago.ListaDocumentosRelacionados.Add(oDocumento);
                                    }
                                    oCFDI.ListaPago.Add(oPago);

                                }
                            }
                            this.LisaCFDI.Add(oCFDI);
                        }
                        catch (Exception ex)
                        {
                            Console.Write(ex.Message);
                        }
                    }
                }
                dgvCDFI.DataSource = this.LisaCFDI;
                dgvCDFI.Refresh();
                lblCantidad.Text = "Total de Documentos: " + this.LisaCFDI.Count.ToString();
            }
        }

        private void btnProceso_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            //GUARDAMOS EN BD
            foreach (CFDI oCFDI in this.LisaCFDI)
            {
                CFDI.setAltaCFDI(oCFDI);
                if (oCFDI.TipoCFDI == "Pago")
                {
                    foreach (CFDI.ComplementoPago oComplemento in oCFDI.ListaPago)
                    {
                        CFDI.setAltaCFDIPago(oCFDI.UUID, oComplemento);
                    }
                }
            }
            Cursor.Current = Cursors.Default;
            MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            FolderBrowserDialog save = new FolderBrowserDialog();
            save.ShowNewFolderButton = true;
            if (save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (save.SelectedPath != "")
                {
                    foreach(CFDI _cfdi in this.LisaCFDI)
                    {
                        System.Xml.XmlDocument _doc = new System.Xml.XmlDocument();
                        _doc.LoadXml(_cfdi.XML);
                        _doc.Save(save.SelectedPath + "\\" + _cfdi.RFCEmisor.Trim().Replace("-","") + (_cfdi.Serie==""?"":"_"+_cfdi.Serie) + (_cfdi.Folio==""?"":"_"+_cfdi.Folio) + "_" + _cfdi.UUID.ToUpper() + ".xml");
                    }
                    MessageBox.Show("Archivos guardados correctamente.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        void LimpiaFormulario()
        {
            this.LisaCFDI.Clear();
            lblCantidad.Text = "Total de Documentos: 0" ;
            dgvCDFI.DataSource = this.LisaCFDI;
            dgvCDFI.Refresh();

        }
    }
}
