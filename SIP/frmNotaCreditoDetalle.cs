using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ulp_bl;
using SIP.Utiles;
using CFDI33;
using System.IO;

namespace SIP
{
    public partial class frmNotaCreditoDetalle : Form
    {
        #region Variables y constructores
        BackgroundWorker bgwLoad;
        BackgroundWorker bgwNotaCredito;
        Precarga precarga;

        string factura = "";
        string uuidFactura = "";
        DataSet dsFactura;
        byte[] byteCer;
        byte[] byteKey;
        String Password;
        String RFCCertificado;
        String NoCER;
        DateTime Desde;
        DateTime Hasta;
        String path = "";
        String remitido = "";
        String ordenCompra = "";
        String vendedor = "";
        String error = "";
        String observaciones = "";
        String tipoRelacion = "";

        // VARIABLES PARA CFDI33
        CFDI33.Clases.Generales.CfdiRelacionados ListaCFDIRelacionados = new CFDI33.Clases.Generales.CfdiRelacionados();
        CFDI33.Clases.CFDI.Comprobante_33 oComprobante33 = new CFDI33.Clases.CFDI.Comprobante_33();
        CFDI33.Clases.Generales.Emisor oEmisor = new CFDI33.Clases.Generales.Emisor();
        CFDI33.Clases.Generales.Receptor oReceptor = new CFDI33.Clases.Generales.Receptor();
        DataTable dtEncabezado = new DataTable();
        DataTable dtDetalle = new DataTable();
        DataTable dtFolio = new DataTable();

        public frmNotaCreditoDetalle(String _factura)
        {
            InitializeComponent();
            this.factura = _factura;
            precarga = new Precarga(this);
            bgwLoad = new BackgroundWorker();
            bgwLoad.DoWork += bgwLoad_DoWork;
            bgwLoad.RunWorkerCompleted += bgwLoad_RunWorkerCompleted;
            bgwNotaCredito = new BackgroundWorker();
            bgwNotaCredito.DoWork += bgwNotaCredito_DoWork;
            bgwNotaCredito.RunWorkerCompleted += bgwNotaCredito_RunWorkerCompleted;
            dgvDetalle.AutoGenerateColumns = false;
        }
        #endregion
        #region Eventos
        private void frmNotaCreditoDetalle_Load(object sender, EventArgs e)
        {
            precarga.MostrarEspera();
            precarga.AsignastatusProceso("Cargando información de la factura...");
            // CARGAMOS CSD
            DataTable dtCSD = ulp_bl.CFDIPAC.getCSD();
            //cmbSerie.DataSource = Enum.GetNames(typeof(CFDIPAC.SeriesNC));
            //cmbAlmacen.SelectedIndex = 0;
            if (dtCSD.Rows.Count > 0)
            {
                this.NoCER = dtCSD.Rows[0]["noCertificado"].ToString();
                this.RFCCertificado = dtCSD.Rows[0]["rfc"].ToString();
                this.Desde = DateTime.Parse(dtCSD.Rows[0]["fechaDesde"].ToString());
                this.Hasta = DateTime.Parse(dtCSD.Rows[0]["fechaHasta"].ToString());
                this.byteCer = (Byte[])dtCSD.Rows[0]["byteCer"];
                this.byteKey = (Byte[])dtCSD.Rows[0]["byteKey"];
                this.Password = dtCSD.Rows[0]["pass"].ToString();
                LlenaDatosCSD(true);
            }
            if (!Globales.UsuarioActual.UsuarioUsuario.ToLower().Contains("sup"))
            {
                gbCSD.Enabled = false;
            }
            this.CargaCatalogos();
            // CARGAMOS FACTURA
            bgwLoad.RunWorkerAsync();
        }
        private void dgvDetalle_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var dataGridView = sender as DataGridView;
            if (dataGridView.Rows[e.RowIndex].Selected)
            {
                e.CellStyle.Font = new Font(new FontFamily(e.CellStyle.Font.Name), e.CellStyle.Font.Size - 1, FontStyle.Bold);
                // edit: to change the background color:
                //e.CellStyle.SelectionBackColor = Color.Coral;
            }
        }
        private void dgvDetalle_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.Value);
            if (e.Value != null)
            {
                e.CellStyle.ForeColor = Color.Blue;

            }
        }
        private void dgvDetalle_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            this.CalculaTotales();
        }
        private void dgvDetalle_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            this.CalculaTotales();
        }
        private void dgvDetalle_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.CalculaTotales();
        }
        private void cmbSerie_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            if (cmbSerie.SelectedValue != null)
            {
                this.oComprobante33.Serie = Enum.Parse(typeof(CFDIPAC.SeriesNC), cmbSerie.SelectedValue.ToString()).ToString();
                if (cmbSerie.Text == "D")
                {
                    List<String> lst = new List<string> { };
                    lst.Add("5");
                    cmbAlmacen.DataSource = lst;
                }
                if (cmbSerie.Text == "C")
                {
                    List<String> lst = new List<string> { };
                    lst.Add("4");
                    lst.Add("6");
                    cmbAlmacen.DataSource = lst;
                }
            }
             * */

        }
        private void btnGenerar_Click(object sender, EventArgs e)
        {
            // RECORREMOS LOS CONCEPTOS SELECCIONADOS Y GENERAMOS EL DETALLE
            foreach (DataRow dgv in dtDetalle.Rows)
            {
                if (dgv["Seleccion"].ToString() == "1")
                {
                    CFDI33.Clases.Generales.Concepto oConcepto = new CFDI33.Clases.Generales.Concepto();
                    oConcepto.Cantidad = decimal.Parse(dgv["Cantidad"].ToString());
                    oConcepto.Descripcion = dgv["Descripcion"].ToString();
                    oConcepto.Unidad = dgv["Unidadventa"].ToString();
                    oConcepto.ClaveUnidad = dgv["ClaveUnidad"].ToString();
                    oConcepto.ClaveProdServ = dgv["ClaveProductoServicio"].ToString();
                    oConcepto.ValorUnitario = decimal.Parse(dgv["Precio"].ToString());
                    oConcepto.Importe = decimal.Parse(dgv["Subtotal"].ToString());
                    oConcepto.NoIdentificacion = dgv["Clave"].ToString();
                    oConcepto.Descuento = decimal.Parse(dgv["MontoDescuento"].ToString());
                    //GENERAMOS EL IMPUESTO TRASLADADO
                    CFDI33.Clases.Generales.Traslado oTraslado = new CFDI33.Clases.Generales.Traslado();
                    oTraslado.Base = oConcepto.Importe - oConcepto.Descuento;
                    oTraslado.TasaOCuota = decimal.Parse(dgv["IVATasa"].ToString());
                    oTraslado.Importe = decimal.Parse(dgv["IVAImporte"].ToString());
                    oTraslado.Impuesto = "002";
                    oTraslado.TipoFactor = "Tasa";
                    oConcepto.Impuestos.Traslados.Add(oTraslado);
                    oComprobante33.Conceptos.Add(oConcepto);
                }
            }
            // ASIGNAMOS VALORES GENERALES
            oComprobante33.MetodoPago = cmbMetodoDePago.SelectedValue.ToString();
            oComprobante33.FormaPago = cmbFormaDePago.SelectedValue.ToString();
            oReceptor.UsoCFDI = cmbUsoCFDI.SelectedValue.ToString();
            oComprobante33.Folio = CFDIPAC.getFolio(oComprobante33.Serie, "D");
            if (oComprobante33.Folio == "")
            {
                this.error = "No se pudo obtener el folio para generar el documento.";
                return;
            }
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (folderBrowserDialog1.SelectedPath != "")
                {
                    this.path = folderBrowserDialog1.SelectedPath;
                    this.observaciones = txtObservaciones.Text;
                    precarga.MostrarEspera();
                    precarga.AsignastatusProceso("Generando Nota de Crédito...");
                    bgwNotaCredito.RunWorkerAsync();
                }
            }


        }
        private void dgvDetalle_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            this.CalculaTotales();
        }
        private void cmbTipoRelacion_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbTipoRelacion.SelectedValue != null)
            {
                this.tipoRelacion = cmbTipoRelacion.SelectedValue.ToString();
            }
        }
        #endregion
        #region Workers
        void bgwLoad_DoWork(object sender, DoWorkEventArgs e)
        {
            this.dsFactura = ulp_bl.CFDIPAC.getDetalleFactura(this.factura);
            if (dsFactura.Tables.Count == 0)
            {
                return;
            }
            dtEncabezado = this.dsFactura.Tables[0];
            dtDetalle = this.dsFactura.Tables[1];
            dtFolio = this.dsFactura.Tables[2];
            //CARGAMOS LOS DATOS ADICIONALES PARA LA VISTA IMPRESA
            this.remitido = dtEncabezado.Rows[0]["Remitido"].ToString();
            this.ordenCompra = dtEncabezado.Rows[0]["OrdenCompra"].ToString();
            this.vendedor = dtEncabezado.Rows[0]["Vendedor"].ToString();
            this.uuidFactura = dtEncabezado.Rows[0]["UUID"].ToString();
            //CARGAMOS LOS DATOS DE EMISOR
            oEmisor = new CFDI33.Clases.Generales.Emisor();
            oEmisor.RFC = "UPR8507265UA";
            oEmisor.RegimenFiscal = "601";
            oEmisor.Nombre = "UNIFORMES PROVIDENCIA SA DE CV";
            //CARGAMOS LOS DATOS DEL COMPROBANTE
            this.oComprobante33 = new CFDI33.Clases.CFDI.Comprobante_33();
            oComprobante33.Emisor = oEmisor;
            //oComprobante33.Serie = Enum.Parse(typeof(CFDIPAC.SeriesFAC), cmbSerie.SelectedValue.ToString()).ToString();
            //oComprobante33.Folio = dtFolio.Rows[0]["ULT_DOC"].ToString() == "" ? "1" : dtFolio.Rows[0]["ULT_DOC"].ToString();
            oComprobante33.LugarExpedicion = "91340";
            oComprobante33.FormaPago = dtEncabezado.Rows[0]["FormaDePago"].ToString();
            oComprobante33.Moneda = "MXN";
            oComprobante33.TipoCambio = 1;
            oComprobante33.MetodoPago = dtEncabezado.Rows[0]["MetodoDePago"].ToString();
            oComprobante33.TipoDeComprobante = "E";
            oComprobante33.Fecha = DateTime.Now;
            oComprobante33.SubTotal = decimal.Parse(dtEncabezado.Rows[0]["SubTotal"].ToString());
            oComprobante33.Total = decimal.Parse(dtEncabezado.Rows[0]["Total"].ToString());
            oComprobante33.NoCertificado = this.NoCER;
            oComprobante33.Certificado = Convert.ToBase64String(this.byteCer);
            //CARGAMOS LOS DATOS DEL RECEPTOR;
            oReceptor = new CFDI33.Clases.Generales.Receptor();
            oReceptor.RFC = dtEncabezado.Rows[0]["ClienteRFC"].ToString().ToUpper().Trim().Replace("-", "");
            oReceptor.Nombre = dtEncabezado.Rows[0]["ClienteNombre"].ToString().ToUpper().Trim();
            oReceptor.UsoCFDI = dtEncabezado.Rows[0]["UsoCFDI"].ToString().ToUpper().Trim();
            oReceptor.ClaveReceptor = dtEncabezado.Rows[0]["ClienteClave"].ToString().ToUpper().Trim();
            oComprobante33.Receptor = oReceptor;
            //AGREGAMOS EL DOCUMENTO RELACIONADO
            if (this.uuidFactura != "")
            {
                this.ListaCFDIRelacionados.TipoRelacion = this.tipoRelacion;
                this.ListaCFDIRelacionados.Cfdi_Relacionados.Add(new CFDI33.Clases.Generales.CfdiRelaccionado { UUID = this.uuidFactura });
                oComprobante33.CfdiRelacionados = ListaCFDIRelacionados;
            }
        }
        void bgwLoad_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            precarga.RemoverEspera();
            if (this.dsFactura.Tables.Count == 0)
            {
                MessageBox.Show("No se pudo cargar el detalle de la factura.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            dgvDetalle.DataSource = dtDetalle;
            this.CalculaTotales();
            lblClienteRFC.Text = "RFC: " + oReceptor.RFC;
            lblClienteNombre.Text = "Nombre: " + oReceptor.Nombre;
            this.AsignaSerieYAlmacen(dtEncabezado.Rows[0]["Serie"].ToString(), dtEncabezado.Rows[0]["Almacen"].ToString());
            this.cmbFormaDePago.SelectedValue = dtEncabezado.Rows[0]["FormaDePago"].ToString();
            this.cmbMetodoDePago.SelectedValue = dtEncabezado.Rows[0]["MetodoDePago"].ToString();

        }
        void bgwNotaCredito_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                // GENERAMOS DOCUMENTO XML
                Stream res = new MemoryStream();
                res = oComprobante33.GeneraXML_Factura(this.byteCer, this.byteKey, this.Password, 6);
                String _xmlString = String.Empty;
                _xmlString = CFDI33.Clases.FuncionesGlobales.GenerateStringFromStream(res);
                if (_xmlString.Contains("Error"))
                {
                    //MessageBox.Show("La Nota de Crédito no se puede procesar: " + _xmlString, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.error = "La Nota de Crédito no se puede procesar: " + _xmlString;
                    return;
                }
                System.Xml.XmlDocument _xml = new System.Xml.XmlDocument();
                _xml.LoadXml(_xmlString);
                //YA QUE TENEMOS EL XML, LO TIMBRAMOS
                String _errorTimbrado = "";
                _xml = Timbra(_xml, ref _errorTimbrado);
                if (_errorTimbrado != "")
                {
                    //MessageBox.Show("Error al timbrar. Error: " + _errorTimbrado, "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    this.error = "Error al timbrar. Error: " + _errorTimbrado;
                    return;
                }

                // GUARDAMOS XML Y GENERAMOS PDF
                _xml.Save(this.path + "\\" + oComprobante33.Receptor.RFC + "_" + oComprobante33.Fecha.ToString("ddMMyyyy") + "_" + oComprobante33.Serie + oComprobante33.Folio.ToString() + ".xml");
                Reportes.Factura.VistaImpresa.ExportarNC(this.path + "\\" + oComprobante33.Receptor.RFC + "_" + oComprobante33.Fecha.ToString("ddMMyyyy") + "_" + oComprobante33.Serie + oComprobante33.Folio.ToString() + ".xml", "E", oComprobante33.Receptor.ClaveReceptor, this.remitido, "", this.factura, this.ordenCompra, this.vendedor, 0, this.observaciones);

                // GUARDAMOS EN BD
                string _xmlDetalle = "<conceptos>";
                foreach (System.Xml.XmlNode _concepto in _xml["cfdi:Comprobante"]["cfdi:Conceptos"].ChildNodes)
                {
                    _xmlDetalle += "<concepto>";
                    _xmlDetalle += "<ClaveProdServ>" + _concepto.Attributes["ClaveProdServ"].Value + "</ClaveProdServ>";
                    _xmlDetalle += "<NoIdentificacion>" + _concepto.Attributes["NoIdentificacion"].Value + "</NoIdentificacion>";
                    _xmlDetalle += "<Cantidad>" + _concepto.Attributes["Cantidad"].Value + "</Cantidad>";
                    _xmlDetalle += "<ClaveUnidad>" + _concepto.Attributes["ClaveUnidad"].Value + "</ClaveUnidad>";
                    _xmlDetalle += "<Unidad>" + _concepto.Attributes["Unidad"].Value + "</Unidad>";
                    _xmlDetalle += "<Descripcion>" + _concepto.Attributes["Descripcion"].Value + "</Descripcion>";
                    _xmlDetalle += "<ValorUnitario>" + _concepto.Attributes["ValorUnitario"].Value + "</ValorUnitario>";
                    _xmlDetalle += "<Importe>" + _concepto.Attributes["Importe"].Value + "</Importe>";
                    _xmlDetalle += "<Descuento>" + (_concepto.Attributes["Descuento"] != null ? _concepto.Attributes["Descuento"].Value : "0") + "</Descuento>";
                    _xmlDetalle += "<IVA>" + _concepto["cfdi:Impuestos"].ChildNodes[0]["cfdi:Traslado"].Attributes["Importe"].Value + "</IVA>";
                    _xmlDetalle += "</concepto>";
                }
                _xmlDetalle += "</conceptos>";
                CFDIPAC.setAltaCFDINC(
                                _xml["cfdi:Comprobante"]["cfdi:Complemento"]["tfd:TimbreFiscalDigital"].Attributes["UUID"].Value,
                                this.factura,
                                _xml.InnerXml,
                                _xml["cfdi:Comprobante"]["cfdi:Emisor"].Attributes["Rfc"].Value,
                                _xml["cfdi:Comprobante"]["cfdi:Receptor"].Attributes["Rfc"].Value,
                                _xml["cfdi:Comprobante"].Attributes["Fecha"].Value,
                                _xml["cfdi:Comprobante"].Attributes["SubTotal"].Value,
                                _xml["cfdi:Comprobante"]["cfdi:Impuestos"].Attributes["TotalImpuestosTrasladados"].Value,
                                _xml["cfdi:Comprobante"].Attributes["Total"].Value,
                                _xml["cfdi:Comprobante"].Attributes["Serie"].Value,
                                _xml["cfdi:Comprobante"].Attributes["Folio"].Value,
                                _xml["cfdi:Comprobante"].Attributes["Sello"].Value,
                                _xml["cfdi:Comprobante"].Attributes["Certificado"].Value,
                                _xml["cfdi:Comprobante"].Attributes["NoCertificado"].Value,
                                _xml["cfdi:Comprobante"]["cfdi:Complemento"]["tfd:TimbreFiscalDigital"].Attributes["FechaTimbrado"].Value,
                                _xml["cfdi:Comprobante"].Attributes["MetodoPago"].Value,
                                _xml["cfdi:Comprobante"].Attributes["FormaPago"].Value,
                                _xmlDetalle,
                                int.Parse(lblAlmacenValor.Text),
                                Globales.UsuarioActual.UsuarioUsuario,
                                true,
                                oReceptor.ClaveReceptor,
                                _xml["cfdi:Comprobante"]["cfdi:Receptor"].Attributes["UsoCFDI"].Value,
                                this.tipoRelacion,
                                this.observaciones
                                );
            }
            catch (Exception ex)
            {
                this.error = "Excepción: " + ex.Message + ". " + ex.InnerException;
            }
        }
        void bgwNotaCredito_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            precarga.RemoverEspera();
            if (this.error != "")
            {
                MessageBox.Show(this.error, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Proceso finalizado de forma correcta. Folio de la NC: " + oComprobante33.Serie + oComprobante33.Folio, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }
        #endregion
        #region Metodos
        private void LlenaDatosCSD(Boolean csdCorrecto)
        {
            if (csdCorrecto)
            {
                lblRFCValor.Text = this.RFCCertificado;
                lblNoCerValor.Text = this.NoCER;
                lblFechaVencimientoValor.Text = this.Hasta.ToString("dd/MM/yyyy HH:mm:ss");
                lblEstatusValor.Text = this.Hasta > DateTime.Now ? "Activo" : "Inactivo";
            }
            else
            {
                lblRFCValor.Text = "Sin Configurar";
                lblNoCerValor.Text = "Sin Configurar";
                lblFechaVencimientoValor.Text = "Sin Configurar";
                lblEstatusValor.Text = "Sin Configurar";
            }

        }
        private void CargaCatalogos()
        {
            // CARGAMOS CATALOGOS
            DataSet dsCatalogosEspeciales = CatalogosSolicitudesEspeciales.getCatalogosEspeciales();

            DataTable dtCatalogoFormaPago = Catalogos.GetCatalogoFormaPago();
            DataTable dtCatalogoUsoCFDI = Catalogos.GetCatalogoUsoCFDI();
            DataTable dtCatalogoMetodoPago = Catalogos.GetCatalogoMetodoPago();
            DataTable dtCatalogoTipoRelacion = CFDIPAC.getCatalogoTipoRelacion();

            cmbFormaDePago.DataSource = dtCatalogoFormaPago;
            cmbFormaDePago.DisplayMember = "Descripcion";
            cmbFormaDePago.ValueMember = "Clave";
            cmbFormaDePago.SelectedValue = "03";

            cmbMetodoDePago.DataSource = dtCatalogoMetodoPago;
            cmbMetodoDePago.DisplayMember = "Descripcion";
            cmbMetodoDePago.ValueMember = "Clave";
            cmbMetodoDePago.SelectedValue = "PUE";

            cmbUsoCFDI.DataSource = dtCatalogoUsoCFDI;
            cmbUsoCFDI.DisplayMember = "Descripcion";
            cmbUsoCFDI.ValueMember = "Clave";
            cmbUsoCFDI.SelectedValue = "G02";

            cmbTipoRelacion.DataSource = dtCatalogoTipoRelacion;
            cmbTipoRelacion.DisplayMember = "Descripcion";
            cmbTipoRelacion.ValueMember = "Clave";
            cmbTipoRelacion.SelectedValue = "01";

        }
        private void CalculaTotales()
        {
            decimal subtotal = 0;
            decimal iva = 0;
            decimal total = 0;

            foreach (DataRow dr in dtDetalle.Rows)
            {
                if (dr["Seleccion"].ToString() == "1")
                {
                    subtotal += decimal.Parse(dr["Subtotal"].ToString());
                    iva += decimal.Parse(dr["Subtotal"].ToString()) * decimal.Parse(dr["IVATasa"].ToString());
                    total += decimal.Parse(dr["Subtotal"].ToString()) + decimal.Parse(dr["Subtotal"].ToString()) * (decimal.Parse(dr["IVATasa"].ToString()));
                }

            }
            lblSubtotalImporte.Text = subtotal.ToString("C2");
            lblIVAImporte.Text = iva.ToString("C2");
            lblTotalImporte.Text = total.ToString("C2");
        }
        private void AsignaSerieYAlmacen(String _serieFactura, String _almacenFactura)
        {
            if (_serieFactura == "A")
            {
                this.oComprobante33.Serie = "D";
                this.lblAlmacenValor.Text = _almacenFactura != "" ? _almacenFactura : "5";
            }
            if (_serieFactura == "B")
            {
                this.oComprobante33.Serie = "C";
                this.lblAlmacenValor.Text = this.lblAlmacenValor.Text = _almacenFactura != "" ? _almacenFactura : "4";
            }
            this.lblSerieValor.Text = this.oComprobante33.Serie;
        }
        private System.Xml.XmlDocument Timbra(System.Xml.XmlDocument _xml, ref String _error)
        {
            
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(_xml.InnerXml);
            PAC_Edifact.timbrarCFDIPortTypeClient oPAC = new PAC_Edifact.timbrarCFDIPortTypeClient();
            PAC_Edifact.respuestaTimbrado oRespuesta = new PAC_Edifact.respuestaTimbrado();
            oRespuesta = oPAC.timbrarCFDI(lblRFCValor.Text, lblRFCValor.Text, System.Convert.ToBase64String(plainTextBytes));
            if (oRespuesta.codigoResultado != "100")
            {
                _error = oRespuesta.codigoResultado + " - " + oRespuesta.codigoDescripcion;
                return null;
            }

            _xml.LoadXml(System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(oRespuesta.documentoTimbrado)));
            


            //CREAMOS UN TIMBRE DE PRUEBA
            /*
            System.Xml.XmlElement timbre = _xml.CreateElement("tfd:TimbreFiscalDigital", "http://www.sat.gob.mx/TimbreFiscalDigital");

            System.Xml.XmlAttribute att;


            att = _xml.CreateAttribute("xmlns:xsi");
            att.Value = "http://www.w3.org/2001/XMLSchema-instance";
            timbre.Attributes.Append(att);

            att = _xml.CreateAttribute("xsi:schemaLocation", "http://www.w3.org/2001/XMLSchema-instance");
            att.Value = "http://www.sat.gob.mx/TimbreFiscalDigital http://www.sat.gob.mx/sitio_internet/cfd/timbrefiscaldigital/TimbreFiscalDigitalv11.xsd";
            timbre.Attributes.Append(att);

            att = _xml.CreateAttribute("Version");
            att.Value = "1.1";
            timbre.Attributes.Append(att);

            att = _xml.CreateAttribute("UUID");
            att.Value = Guid.NewGuid().ToString().ToUpper();
            timbre.Attributes.Append(att);

            att = _xml.CreateAttribute("FechaTimbrado");
            att.Value = "2018-12-20T18:21:36";
            timbre.Attributes.Append(att);

            att = _xml.CreateAttribute("RfcProvCertif");
            att.Value = "MAS0810247C0";
            timbre.Attributes.Append(att);

            att = _xml.CreateAttribute("SelloCFD");
            att.Value = "LasBIbC3fZDVpH5iJoWHSHtQxXADOuVleJ+yTTXbRBugwPMFnPqshk2ZiX0kYEwQC5ZMRoa16oDoaOtw2ToZFLY8a1TJ6nImQkutPg0dkONu7u4ODupOIXHMvI2GBDIZa42ilTSg0E1S2Sjd60Gcm3Nqf65KbkTA5VU9mdv5Hp5pMbAc2QXcGqfxqdLZKYmIf9rfdy7q";
            timbre.Attributes.Append(att);

            att = _xml.CreateAttribute("SelloSAT");
            att.Value = "LasBIbC3fZDVpH5iJoWHSHtQxXADOuVleJ+yTTXbRBugwPMFnPqshk2ZiX0kYEwQC5ZMRoa16oDoaOtw2ToZFLY8a1TJ6nImQkutPg0dkONu7u4ODupOIXHMvI2GBDIZa42ilTSg0E1S2Sjd60Gcm3Nqf65KbkTA5VU9mdv5Hp5pMbAc2QXcGqfxqdLZKYmIf9rfdy7q";
            timbre.Attributes.Append(att);

            att = _xml.CreateAttribute("NoCertificadoSAT");
            att.Value = "00001000000404486074";
            timbre.Attributes.Append(att);

            _xml["cfdi:Comprobante"]["cfdi:Complemento"].AppendChild(timbre);*/

            return _xml;
        }
        #endregion
    }
}
