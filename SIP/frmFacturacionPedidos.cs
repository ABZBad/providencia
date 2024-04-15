using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CRYPTO100;
using ulp_bl;
using CFDI33;
using SIP.Utiles;

namespace SIP
{
    public partial class frmFacturacionPedidos : Form
    {
        private byte[] byteCer;
        private byte[] byteKey;
        String Password;
        String RFCCertificado;
        String NoCER;
        DateTime Desde;
        DateTime Hasta;
        private DataTable dtPedidos;
        private BackgroundWorker bgwPedidos;
        private BackgroundWorker bgwFacturacion;
        private Precarga precarga;



        public frmFacturacionPedidos()
        {
            InitializeComponent();
            dgvPedidos.AutoGenerateColumns = false;
            this.dtPedidos = new DataTable();
            precarga = new Precarga(this);
            bgwPedidos = new BackgroundWorker();
            bgwPedidos.DoWork += bgwPedidos_DoWork;
            bgwPedidos.RunWorkerCompleted += bgwPedidos_RunWorkerCompleted;
        }
        private void btnCargaCer_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Llave Pública (.cer) | *.cer";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtCer.Text = ofd.FileName;
            }
        }
        private void btnCargarKey_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Llave Privada (.key) | *.key";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtKey.Text = ofd.FileName;
            }
        }
        private void btnValidar_Click(object sender, EventArgs e)
        {
            if (txtCer.Text == "" || txtKey.Text == "" || txtPass.Text == "")
            {
                MessageBox.Show("Se deben de llenar todos los datos del CSD", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (BinaryReader breader = new BinaryReader(File.Open(txtCer.Text, FileMode.Open)))
            {
                breader.BaseStream.Position = 0;
                byteCer = breader.ReadBytes((int)breader.BaseStream.Length);
                breader.Close();
            }

            using (BinaryReader breader = new BinaryReader(File.Open(txtKey.Text, FileMode.Open)))
            {
                breader.BaseStream.Position = 0;
                byteKey = breader.ReadBytes((int)breader.BaseStream.Length);
                breader.Close();
            }

            this.Password = txtPass.Text.Trim();

            List<String> lstErrores = new List<string> { };
            lstErrores = ValidaCertificado(this.byteCer, this.byteKey, this.Password);
            if (lstErrores.Count == 0)
            {
                if (ulp_bl.CFDIPAC.setAltaCSD(this.RFCCertificado, this.NoCER, this.Desde, this.Hasta, this.byteCer, this.byteKey, this.Password) > 0)
                {
                    MessageBox.Show("CSD registrado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LlenaDatosCSD(true);
                }
                else
                {
                    MessageBox.Show("El CSD no se ha registrado, favor de contactar con su administrador del sistema.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LlenaDatosCSD(false);
                }
                LimpiaNuevoCSD();
            }
            else
            {
                String _errorMessage = String.Empty;
                foreach (String _error in lstErrores)
                {
                    _errorMessage += _error + (char)13;
                }
                MessageBox.Show(_errorMessage, "SIP - CSD Errores de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private List<string> ValidaCertificado(byte[] byteCer, byte[] byteKey, string pass)
        {
            this.NoCER = "";
            List<string> res = new List<string> { };
            String[] miArray;
            String[] miArrayAux;
            String[] miArrayAux2;
            String aux = "";
            int i = 0;
            String aNombreDe = "";
            Boolean existe = false;
            String[] Sello;
            System.Security.Cryptography.X509Certificates.X509Certificate2 cer = new System.Security.Cryptography.X509Certificates.X509Certificate2(byteCer);
            miArray = cer.Subject.Split(',');
            //VERIFICAMOS EL OU
            foreach (String obj in miArray)
            {
                if (obj.Contains("OU="))
                {
                    existe = true;
                    break;
                }
            }
            if (!existe)
            {
                res.Add("Este no es un Certificado de Sello Digital.");
            }
            existe = false;
            //VERIFICAMOS EL ATRIBUTO 2.5.4.45
            foreach (String obj in miArray)
            {
                if (obj.Contains("2.5.4.45="))
                {
                    if (obj.Contains("/"))
                    {
                        miArrayAux = obj.Split('=');
                        aux = miArrayAux[1];
                        miArrayAux2 = aux.Split('/');
                        RFCCertificado = miArrayAux2[0].Trim();
                        existe = true;
                    }
                    else
                    {
                        miArrayAux = obj.Split('=');
                        RFCCertificado = miArrayAux[1].Trim();
                        existe = true;
                    }
                }
                if (obj.Contains("O="))
                    aNombreDe = obj.Split('=')[1];
            }
            if (!existe)
            {
                res.Add("Este no es un Certificado de Sello Digital.");
            }
            existe = false;
            //VERIFICAMOS QUE NO HAYA CADUCADO
            if (cer.NotAfter < DateTime.Now.Date)
                res.Add("Este Certificado ha caducado.");
            //VERIFICAMOS EL RFC DEL CERTIFICADO
            if (!Regex.IsMatch(RFCCertificado, @"^[A-Z,Ñ,&amp;]{3,4}[0-9]{2}[0-1][0-9][0-3][0-9][A-Z,0-9]?[A-Z,0-9]?[0-9,A-Z]?$"))
                res.Add("Este Certificado contiene un RFC incorrecto.");
            //if (RFCCertificado != _rfcEmrpesa)
            //    res.Add("Este Certificado no pertenece a la empresa");
            while (i < cer.SerialNumber.Length)
            {
                this.NoCER += int.Parse(cer.SerialNumber.Substring(i, 2)) - 30;
                i += 2;
            }
            //YA QUE VALIDAMOS TODO EL CONTENIDO DEL CERTIFICADO VERIFICAMOS QUE PODAMOS DECRIPTAR EL CER, EL KEY Y EL PASS
            Sello = SelloDigital.getSelloDigital(byteCer, byteKey, pass, "SELLO PARA COMPROBAR CER, KEY Y PASS", TipoAlgoritmo.SHA256);
            if (Sello[0] == "")
                res.Add(Sello[1]);
            if (res.Count == 0)
            {
                //._RFC_EMPRESA = RFCCertificado;
                Desde = cer.NotBefore;
                Hasta = cer.NotAfter;
            }
            return res;
        }
        private void frmFacturacionPedidos_Load(object sender, EventArgs e)
        {
            precarga.MostrarEspera();
            precarga.AsignastatusProceso("Cargando Pedidos...");
            bgwPedidos.RunWorkerAsync();

            //CARGAMOS LOS DATOS DEL CSD
            DataTable dtCSD = ulp_bl.CFDIPAC.getCSD();
            cmbSerie.DataSource = Enum.GetNames(typeof(CFDIPAC.SeriesFAC));
            cmbAlmacen.SelectedIndex = 0;
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
            txtPedido.Text = "";
            txtPedido.Select();
            txtPedido.Focus();
            if (!Globales.UsuarioActual.UsuarioUsuario.ToLower().Contains("sup"))
            {
                gbCSD.Enabled = false;
            }
        }
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
        private void LimpiaNuevoCSD()
        {
            txtCer.Text = "";
            txtKey.Text = "";
            txtPass.Text = "";
        }
        private void btnFacturar_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (folderBrowserDialog1.SelectedPath != "")
                {
                    //RECORREMOS LOS PEDIDOS SELECCIONADOS
                    var checkedRows = from DataGridViewRow r in dgvPedidos.Rows
                                      where Convert.ToBoolean(r.Cells[0].Value) == true
                                      select r;



                    if (checkedRows.ToList().Count == 0)
                    {
                        MessageBox.Show("Se debe de seleccionar al menos 1 Pedido para facturar.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    Cursor.Current = Cursors.WaitCursor;
                    //CARGAMOS LOS DATOS DE EMISOR
                    CFDI33.Clases.Generales.Emisor oEmisor = new CFDI33.Clases.Generales.Emisor();
                    oEmisor.RFC = "UPR8507265UA";
                    oEmisor.RegimenFiscal = "601";
                    oEmisor.Nombre = "UNIFORMES PROVIDENCIA SA DE CV";
                    Boolean procesar = true;
                    List<String> Log = new List<string> { };

                    foreach (DataGridViewRow dr in checkedRows.ToList())
                    {
                        procesar = true;
                        //OBTENEMOS EL MAESTRO-DETALLE DEL PEDIDO
                        DataSet _dsDetalle = CFDIPAC.getDetallePedido(int.Parse(dr.Cells["Pedido"].Value.ToString()), cmbSerie.Text, int.Parse(cmbAlmacen.Text));
                        DataTable _dtDetalle = new DataTable();
                        DataTable _dtFolio = new DataTable();
                        _dtDetalle = _dsDetalle.Tables[0];
                        _dtFolio = _dsDetalle.Tables[1];
                        //VERIFICAMOS QUE EL PEDIDO ACTUAL TENGA EXISTENCIA SUFICIENTE EN CADA UNA DE LAS PARTIDAS

                        CFDI33.Clases.CFDI.Comprobante_33 oComprobante33 = new CFDI33.Clases.CFDI.Comprobante_33();
                        oComprobante33.Emisor = oEmisor;
                        //CARGAMOS LOS DATOS DEL COMPROBANTE
                        //ASIGNAMOS
                        oComprobante33.Serie = Enum.Parse(typeof(CFDIPAC.SeriesFAC), cmbSerie.SelectedValue.ToString()).ToString();
                        oComprobante33.Folio = _dtFolio.Rows[0]["ULT_DOC"].ToString() == "" ? "1" : _dtFolio.Rows[0]["ULT_DOC"].ToString();
                        oComprobante33.LugarExpedicion = "91340";
                        oComprobante33.FormaPago = _dtDetalle.Rows[0]["PEDIDO_FORMAPAGO"].ToString();
                        oComprobante33.Moneda = "MXN";
                        oComprobante33.TipoCambio = 1;
                        oComprobante33.MetodoPago = _dtDetalle.Rows[0]["PEDIDO_METODOPAGO"].ToString();
                        oComprobante33.TipoDeComprobante = "I";
                        oComprobante33.Fecha = DateTime.Now;
                        oComprobante33.SubTotal = decimal.Parse(_dtDetalle.Rows[0]["PEDIDO_SUBTOTAL"].ToString());
                        oComprobante33.Total = decimal.Parse(_dtDetalle.Rows[0]["PEDIDO_TOTAL"].ToString());
                        oComprobante33.NoCertificado = this.NoCER;
                        oComprobante33.Certificado = Convert.ToBase64String(this.byteCer);


                        //CARGAMOS LOS DATOS DEL RECEPTOR;
                        CFDI33.Clases.Generales.Receptor oReceptor = new CFDI33.Clases.Generales.Receptor();
                        oReceptor.RFC = _dtDetalle.Rows[0]["CLIENTE_RFC"].ToString().Replace("-", "").Trim();
                        oReceptor.Nombre = _dtDetalle.Rows[0]["CLIENTE_NOMBRE"].ToString();
                        oReceptor.UsoCFDI = _dtDetalle.Rows[0]["CLIENTE_USO"].ToString();
                        oReceptor.ClaveReceptor = _dtDetalle.Rows[0]["CLIENTE_CLAVE"].ToString();
                        oComprobante33.Receptor = oReceptor;

                        List<String> productosSinExistencia = new List<string> { };

                        foreach (DataRow drDetalle in _dtDetalle.Rows)
                        {
                            if (decimal.Parse(drDetalle["EXISTENCIAS"].ToString()) < decimal.Parse(drDetalle["CONCEPTO_CANTIDAD"].ToString()))
                            {
                                productosSinExistencia.Add(drDetalle["CONCEPTO_CODIGO"].ToString() + " (" + drDetalle["CONCEPTO_DESCRIPCION"].ToString() + ") | Cantidad: " + drDetalle["CONCEPTO_CANTIDAD"].ToString() + " | Existencias: " + drDetalle["EXISTENCIAS"].ToString());
                                procesar = false;
                                continue;
                            }
                            CFDI33.Clases.Generales.Concepto oConcepto = new CFDI33.Clases.Generales.Concepto();
                            oConcepto.Cantidad = decimal.Parse(drDetalle["CONCEPTO_CANTIDAD"].ToString());
                            oConcepto.Descripcion = drDetalle["CONCEPTO_DESCRIPCION"].ToString();
                            oConcepto.Unidad = drDetalle["CONCEPTO_UNIDAD"].ToString();
                            oConcepto.ClaveUnidad = drDetalle["CONCEPTO_CLAVEUNIDAD"].ToString();
                            oConcepto.ClaveProdServ = drDetalle["CONCEPTO_CLAVEPRODSERV"].ToString();
                            oConcepto.ValorUnitario = decimal.Parse(drDetalle["CONCEPTO_VALORUNITARIO"].ToString());
                            oConcepto.Importe = decimal.Parse(drDetalle["CONCEPTO_IMPORTE"].ToString());
                            oConcepto.NoIdentificacion = drDetalle["CONCEPTO_CODIGO"].ToString();
                            //GENERAMOS EL IMPUESTO TRASLADADO
                            CFDI33.Clases.Generales.Traslado oTraslado = new CFDI33.Clases.Generales.Traslado();
                            oTraslado.Base = decimal.Parse(drDetalle["IMPUESTO_BASE"].ToString());
                            oTraslado.TasaOCuota = decimal.Parse(drDetalle["IMPUESTO_TASACUOTA"].ToString());
                            oTraslado.Importe = decimal.Round(decimal.Parse(drDetalle["IMPUESTO_IMPORTE"].ToString()), 2);
                            oTraslado.Impuesto = drDetalle["IMPUESTO_IMPUESTO"].ToString();
                            oTraslado.TipoFactor = drDetalle["IMPUESTO_TIPOFACTOR"].ToString();
                            oConcepto.Impuestos.Traslados.Add(oTraslado);
                            oComprobante33.Conceptos.Add(oConcepto);
                        }
                        if (!procesar)
                        {
                            String msg = "El pedido: " + dr.Cells["Pedido"].Value.ToString() + " no tiene suficientes existencias por lo que será omitido en el proceso de facturación" + "\n";
                            foreach (String _producto in productosSinExistencia)
                            {
                                msg += _producto + "\n";
                            }

                            MessageBox.Show(msg, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue;
                        }
                        //GENERAMOS XML
                        Stream res = new MemoryStream();
                        res = oComprobante33.GeneraXML_Factura(this.byteCer, this.byteKey, this.Password, 2);

                        String _xmlString = String.Empty;
                        _xmlString = CFDI33.Clases.FuncionesGlobales.GenerateStringFromStream(res);

                        if (_xmlString.Contains("Error"))
                        {
                            MessageBox.Show("El pedido: " + dr.Cells["Pedido"].Value.ToString() + " no se puede procesar: " + _xmlString, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue;
                        }

                        System.Xml.XmlDocument _xml = new System.Xml.XmlDocument();
                        _xml.LoadXml(_xmlString);
                        //YA QUE TENEMOS EL XML, LO TIMBRAMOS
                        String _errorTimbrado = "";
                        _xml = Timbra(_xml, ref _errorTimbrado);

                        if (_errorTimbrado != "")
                        {
                            if (MessageBox.Show("Error al timbrar el pedido: " + dr.Cells["Pedido"].Value.ToString() + ". Error: " + _errorTimbrado + ". ¿Desea continuar facturando los pedidos restantes?.", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.Yes)
                                continue;
                            else
                                break;
                        }

                        _xml.Save(folderBrowserDialog1.SelectedPath + "\\" + oComprobante33.Receptor.RFC + "_" + oComprobante33.Fecha.ToString("ddMMyyyy") + "_" + oComprobante33.Serie + oComprobante33.Folio.ToString() + ".xml");
                        Reportes.Factura.VistaImpresa.ExportarFAC(folderBrowserDialog1.SelectedPath + "\\" + oComprobante33.Receptor.RFC + "_" + oComprobante33.Fecha.ToString("ddMMyyyy") + "_" + oComprobante33.Serie + oComprobante33.Folio.ToString() + ".xml", "I", oComprobante33.Receptor.ClaveReceptor, _dtDetalle.Rows[0]["PEDIDO_REMITIDO"].ToString(), "", _dtDetalle.Rows[0]["PEDIDO_NUMEROPEDIDO"].ToString(), _dtDetalle.Rows[0]["PEDIDO_AGENTE"].ToString(), "", decimal.Parse(_dtDetalle.Rows[0]["PEDIDO_COMISION"].ToString()));
                        //GUARDAMOS LOS DATOS DE LA FACTURA
                        //1. ENCABEZADO
                        CFDIPAC.setAltaCFDI(
                            _xml["cfdi:Comprobante"]["cfdi:Complemento"]["tfd:TimbreFiscalDigital"].Attributes["UUID"].Value,
                            dr.Cells["Pedido"].Value.ToString(),
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
                            int.Parse(cmbAlmacen.Text),
                            Globales.UsuarioActual.UsuarioUsuario,
                            true
                            );
                        //2. DETALLE
                        foreach (System.Xml.XmlNode _concepto in _xml["cfdi:Comprobante"]["cfdi:Conceptos"].ChildNodes)
                        {
                            CFDIPAC.setAltaCFDIDetalle(
                                _xml["cfdi:Comprobante"]["cfdi:Complemento"]["tfd:TimbreFiscalDigital"].Attributes["UUID"].Value,
                                _concepto.Attributes["ClaveProdServ"].Value,
                                _concepto.Attributes["NoIdentificacion"].Value,
                                _concepto.Attributes["Cantidad"].Value,
                                _concepto.Attributes["ClaveUnidad"].Value,
                                _concepto.Attributes["Unidad"].Value,
                                _concepto.Attributes["Descripcion"].Value,
                                _concepto.Attributes["ValorUnitario"].Value,
                                _concepto.Attributes["Importe"].Value,
                                _concepto["cfdi:Impuestos"].ChildNodes[0]["cfdi:Traslado"].Attributes["Importe"].Value
                                );
                        }

                        Log.Add("PEDIDO: " + dr.Cells["Pedido"].Value.ToString() + " -> CLAVE: " + oComprobante33.Serie.ToString() + oComprobante33.Folio.ToString() + " -> UUID: " + _xml["cfdi:Comprobante"]["cfdi:Complemento"]["tfd:TimbreFiscalDigital"].Attributes["UUID"].Value);
                    }
                    Cursor.Current = Cursors.Default;

                    if (Log.Count > 0)
                    {
                        string path = folderBrowserDialog1.SelectedPath + "\\" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".txt";
                        // Create a file to write to.
                        using (StreamWriter sw = File.CreateText(path))
                        {
                            foreach (String _log in Log)
                            {
                                sw.WriteLine(_log);
                            }
                        }
                    }

                    MessageBox.Show("Proceso finalizado", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.frmFacturacionPedidos_Load(null, null);
                }
            }



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

            /*
            //CREAMOS UN TIMBRE DE PRUEBA
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
            att.Value = "2B8A0D65-ECF6-4957-845F-70185361D512";
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

        #region "Workers"
        void bgwPedidos_DoWork(object sender, DoWorkEventArgs e)
        {
            //CARGAMOS LOS PEDIDOS POR FACTURAR
            this.dtPedidos = ulp_bl.CFDIPAC.getPedidosPorFacturar();
        }
        void bgwPedidos_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dgvPedidos.DataSource = this.dtPedidos;
            precarga.RemoverEspera();
            bgwPedidos.Dispose();
        }

        #endregion

        private void txtPedido_KeyUp(object sender, KeyEventArgs e)
        {
            if (dtPedidos != null)
            {
                if (dtPedidos.Rows.Count > 0)
                {
                    try
                    {
                        this.dtPedidos.DefaultView.RowFilter = string.Format("PEDIDO LIKE '%{0}%'", txtPedido.Text);
                    }
                    catch { }

                }
            }
        }

        private void cmbSerie_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSerie.Text == "A")
            {
                List<String> lst = new List<string> { };
                lst.Add("5");
                cmbAlmacen.DataSource = lst;
            }
            if (cmbSerie.Text == "B")
            {
                List<String> lst = new List<string> { };
                lst.Add("4");
                lst.Add("6");
                cmbAlmacen.DataSource = lst;
            }

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            frmFacturacionPedidos_Load(null, null);
        }
    }
}
