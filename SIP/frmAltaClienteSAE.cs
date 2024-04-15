using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ulp_bl;
using SIP.Utiles;

namespace SIP
{
    public partial class frmAltaClienteSAE : Form
    {
        #region "ATRIBUTOS Y CONSTRUCTOR"
        Boolean esEdicion = false;
        Boolean esEdicionContacto = false;
        char tipoPersona;
        Array dtClientesAux;
        DataTable dtClientes;
        DataTable dtVendedores;
        DataTable dtContactos;
        DataTable dtCatalogoRegimen;
        //1=RFC, 2=CURP, 3=LIMITE DE CREDITO, 4=vendedor, 5=porcentaje, 6=cuenta contable, 7 = email, 8=nombre
        String[] comparacionBitacora = { "", "", "", "", "", "", "", "" };

        String currentRFC = String.Empty;
        String nuevaClave = String.Empty;
        List<TipoContacto> ListaTipoContacto;

        public String clienteSeleccionado = String.Empty;
        ulp_bl.CLIE01 objClie;
        ulp_bl.CLIE_CLIB01 objClieClib;



        private BackgroundWorker bgw;
        private Precarga precarga;

        public frmAltaClienteSAE()
        {
            InitializeComponent();
            dtClientes = new DataTable();
            precarga = new Precarga(this);
            dgvContactos.AutoGenerateColumns = false;
            this.ListaTipoContacto = new List<TipoContacto> { };
            this.ListaTipoContacto.Add(new TipoContacto { Clave = "V", Descripcion = "Ventas" });
            this.ListaTipoContacto.Add(new TipoContacto { Clave = "C", Descripcion = "Compras" });
        }
        #endregion
        #region "EVENTOS"
        private void frmAltaClienteSAE_Load(object sender, EventArgs e)
        {
            //this.dtClientes = getClientes("Administrador");
            this.dtClientes = getClientes();

            if (!Globales.UsuarioActual.UsuarioPerfil.Contains("Administrador"))
            {
                var query = from res in this.dtClientes.AsEnumerable()
                            where res.Field<String>("perfil") == Globales.UsuarioActual.UsuarioPerfil
                            select new
                            {
                                CLAVE = res.Field<String>("CLAVE").ToString().Trim(),
                                RFC = res.Field<String>("RFC") == null ? "" : res.Field<String>("RFC").ToString().Trim(),
                                NOMBRE = res.Field<String>("NOMBRE") == null ? "" : res.Field<String>("NOMBRE").ToString().Trim()
                            };
                this.dtClientesAux = query.ToArray();
            }
            else
            {
                var query = from res in this.dtClientes.AsEnumerable()
                            select new
                            {
                                CLAVE = res.Field<String>("CLAVE").ToString().Trim(),
                                RFC = res.Field<String>("RFC") == null ? "" : res.Field<String>("RFC").ToString().Trim(),
                                NOMBRE = res.Field<String>("NOMBRE") == null ? "" : res.Field<String>("NOMBRE").ToString().Trim()
                            };
                this.dtClientesAux = query.ToArray();
            }





            //CLIENTES

            cmbClientesClave.DisplayMember = "CLAVE";
            cmbClientesClave.ValueMember = "CLAVE";

            cmbClientesRFC.DisplayMember = "RFC";
            cmbClientesRFC.ValueMember = "CLAVE";

            cmbClientesNombre.DisplayMember = "NOMBRE";
            cmbClientesNombre.ValueMember = "CLAVE";

            cmbClientesClave.DataSource = dtClientesAux;
            cmbClientesRFC.DataSource = dtClientesAux;
            cmbClientesNombre.DataSource = dtClientesAux;

            //VENDEDORES

            this.dtVendedores = getVendedores();

            cmbVendedores.DisplayMember = "NOMBRE";
            cmbVendedores.ValueMember = "CVE_VEND";
            cmbVendedores.DataSource = dtVendedores;

            //TIPOS DE CONTACTO
            cmbContactoTipo.DisplayMember = "Descripcion";
            cmbContactoTipo.ValueMember = "Clave";
            cmbContactoTipo.DataSource = this.ListaTipoContacto;
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (cmbClientesClave.SelectedValue == null)
            {
                MessageBox.Show("El Cliente seleccionado no existe.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //buscamos todos los datos dentro del datatable principal 
            //filtramos por area



            dtClientes.PrimaryKey = new[] { dtClientes.Columns["CLAVE"] };
            var Cliente = (from res in this.dtClientes.AsEnumerable() where res.Field<String>("CLAVE").Trim() == cmbClientesClave.SelectedValue.ToString().Trim() select res);

            List<DataRow> _Cliente = Cliente.ToList();

            if (_Cliente.Count > 0)
            {
                this.esEdicion = true;
                this.clienteSeleccionado = cmbClientesClave.SelectedValue.ToString();
                btnContactoNuevo.Enabled = true;
                lblAlertaContacto.Visible = false;
                lblAccion.Text = "EDICIÓN DE CLIENTE";
                lblAccion.Visible = true;

                String regRFC = @"^([A-ZÑ\x26]{3,4}([0-9]{2})(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1]))((-)?([A-Z\d]{3}))?$";
                this.currentRFC = _Cliente[0]["RFC"].ToString().Trim();

                //*****************************************************************************************************************************
                //**************************************************DATOS GENERALES************************************************************
                //*****************************************************************************************************************************
                //matriz o sucursal
                if (_Cliente[0]["TIPO_EMPRESA"].ToString() == "M")
                { rbIdentificacionMatriz.Checked = true; rbIdentificacionSucursal.Checked = false; }
                else
                { rbIdentificacionMatriz.Checked = false; rbIdentificacionSucursal.Checked = true; }
                //Clave
                txtClave.Text = _Cliente[0]["CLAVE"].ToString().Trim();
                if (_Cliente[0]["STATUS"].ToString() == "A")
                    txtEstatus.Text = "ACTIVO";
                else
                    txtEstatus.Text = "INACTIVO";
                //rfc
                txtRFC.Text = _Cliente[0]["RFC"].ToString().Trim();
                comparacionBitacora[0] = txtRFC.Text;

                if (System.Text.RegularExpressions.Regex.IsMatch(txtRFC.Text.Trim().Replace("-", ""), regRFC))
                {

                    lblRFCValidacion.Text = "RFC válido.";
                    lblRFCValidacion.ForeColor = Color.Green;

                }
                else
                {
                    lblRFCValidacion.Text = "RFC no válido.";
                    lblRFCValidacion.ForeColor = Color.Red;
                }


                //curp
                txtCURP.Text = _Cliente[0]["CURP"].ToString().Trim();
                comparacionBitacora[1] = txtCURP.Text;
                //REGIMEN FISCAL
                // Asignamos tipoPersona con base a RFC
                this.tipoPersona = txtRFC.Text.Trim().Replace("-", "").Length == 13 ? 'F' : 'M';
                //REGIMEN FISCAL
                this.dtCatalogoRegimen = getCatalogoRegimenFiscal(this.tipoPersona);
                cmbRegimenFiscal.DisplayMember = "Descripcion";
                cmbRegimenFiscal.ValueMember = "Clave";
                cmbRegimenFiscal.DataSource = this.dtCatalogoRegimen;
                if (_Cliente[0]["REG_FISC"].ToString() == "")
                {
                    cmbRegimenFiscal.SelectedIndex = -1;
                }
                else
                {
                    cmbRegimenFiscal.SelectedValue = _Cliente[0]["REG_FISC"].ToString();
                }

                //nombre
                txtNombre.Text = _Cliente[0]["NOMBRE"].ToString().Trim();
                comparacionBitacora[7] = txtNombre.Text;
                //DATOS DE CONTACTO
                //CALLE
                txtCalle.Text = _Cliente[0]["CALLE"] == null ? "" : _Cliente[0]["CALLE"].ToString().Trim();
                //num ext
                txtNumExt.Text = _Cliente[0]["NUMEXT"] == null ? "" : _Cliente[0]["NUMEXT"].ToString().Trim();
                //num int
                txtNumInt.Text = _Cliente[0]["NUMINT"] == null ? "" : _Cliente[0]["NUMINT"].ToString().Trim();
                //ENTRE CALLE
                txtEntreCalle.Text = _Cliente[0]["CRUZAMIENTOS"] == null ? "" : _Cliente[0]["CRUZAMIENTOS"].ToString().Trim();
                //Y CALLE
                txtYCalle.Text = _Cliente[0]["CRUZAMIENTOS2"] == null ? "" : _Cliente[0]["CRUZAMIENTOS2"].ToString().Trim();
                //COLONIA
                txtColonia.Text = _Cliente[0]["COLONIA"] == null ? "" : _Cliente[0]["COLONIA"].ToString().Trim();
                //REFERENCIA
                txtReferencia.Text = _Cliente[0]["REFERDIR"] == null ? "" : _Cliente[0]["REFERDIR"].ToString().Trim();
                //POBLACION
                txtPoblacion.Text = _Cliente[0]["LOCALIDAD"] == null ? "" : _Cliente[0]["LOCALIDAD"].ToString().Trim();
                //cp
                txtCP.Text = _Cliente[0]["CODIGO"] == null ? "" : _Cliente[0]["CODIGO"].ToString().Trim();

                //estado
                txtEstado.Text = _Cliente[0]["ESTADO"] == null ? "" : _Cliente[0]["ESTADO"].ToString().Trim();
                //pais
                txtPais.Text = _Cliente[0]["PAIS"] == null ? "" : _Cliente[0]["PAIS"].ToString().Trim();
                //municipio
                txtMunicipio.Text = _Cliente[0]["MUNICIPIO"] == null ? "" : _Cliente[0]["MUNICIPIO"].ToString().Trim();
                //nacionalidad
                txtNacionalidad.Text = _Cliente[0]["NACIONALIDAD"] == null ? "" : _Cliente[0]["NACIONALIDAD"].ToString().Trim();
                //telefono
                txtTelefono.Text = _Cliente[0]["TELEFONO"] == null ? "" : _Cliente[0]["TELEFONO"].ToString().Trim();
                //fax
                txtFax.Text = _Cliente[0]["FAX"] == null ? "" : _Cliente[0]["FAX"].ToString().Trim();

                //*****************************************************************************************************************************
                //******************************************************CREDITO****************************************************************
                //*****************************************************************************************************************************
                Boolean conCredito = _Cliente[0]["CON_CREDITO"] == null ? false : _Cliente[0]["CON_CREDITO"].ToString() == "S" ? true : false;
                activaControlesCredito(conCredito);
                if (conCredito)
                {
                    numDiasCredito.Value = _Cliente[0]["DIASCRED"] == null ? 0 : _Cliente[0]["DIASCRED"].ToString() == "" ? 0 : decimal.Parse(_Cliente[0]["DIASCRED"].ToString());
                    txtLimiteCredito.Text = _Cliente[0]["LIMCRED"] == null ? "0" : _Cliente[0]["LIMCRED"].ToString().Trim();
                    this.comparacionBitacora[2] = txtLimiteCredito.Text;
                    txtSaldo.Text = _Cliente[0]["SALDO"] == null ? "0" : _Cliente[0]["SALDO"].ToString().Trim();
                    txtDiaRevision.Text = _Cliente[0]["DIAREV"] == null ? "" : _Cliente[0]["DIAREV"].ToString().Trim();
                    txtDiaRevision.Text = _Cliente[0]["DIAPAGO"] == null ? "" : _Cliente[0]["DIAPAGO"].ToString().Trim();
                }
                if (_Cliente[0]["CVE_VEND"] == null)
                {
                    cmbVendedores.SelectedIndex = -1;
                    lblClaveVendedor.Text = "-";
                    this.comparacionBitacora[3] = "";
                }
                else if (_Cliente[0]["CVE_VEND"].ToString() == "")
                {
                    cmbVendedores.SelectedIndex = -1;
                    lblClaveVendedor.Text = "-";
                    this.comparacionBitacora[3] = "";
                }
                else
                {

                    cmbVendedores.SelectedValue = _Cliente[0]["CVE_VEND"] == null ? "" : _Cliente[0]["CVE_VEND"].ToString().Trim();
                    lblClaveVendedor.Text = _Cliente[0]["CVE_VEND"].ToString();
                    this.comparacionBitacora[3] = lblClaveVendedor.Text.Trim();
                }

                if (esEdicion)
                {
                    cmbVendedores.Enabled = Globales.UsuarioActual.UsuarioPerfil.Trim() != "";

                }
                txtDescuento.Text = _Cliente[0]["DESCUENTO"] == null ? "0" : _Cliente[0]["DESCUENTO"].ToString().Trim();
                this.comparacionBitacora[4] = txtDescuento.Text;
                txtCuentaContable.Text = _Cliente[0]["CUENTA_CONTABLE"] == null ? "" : _Cliente[0]["CUENTA_CONTABLE"].ToString().Trim();
                txtFilial.Text = _Cliente[0]["FILIAL"] == null ? "" : _Cliente[0]["FILIAL"].ToString().Trim();
                this.comparacionBitacora[5] = txtCuentaContable.Text;
                //*****************************************************************************************************************************
                //******************************************************IMPRESIÓN**************************************************************
                //*****************************************************************************************************************************

                chkImprimir.Checked = _Cliente[0]["IMPRIR"] == null ? false : _Cliente[0]["IMPRIR"].ToString() == "S" ? true : false;
                chkEnviarEmail.Checked = _Cliente[0]["MAIL"] == null ? false : _Cliente[0]["MAIL"].ToString() == "S" ? true : false;
                txtEmail.Text = _Cliente[0]["EMAILPRED"] == null ? "" : _Cliente[0]["EMAILPRED"].ToString().Trim();
                this.comparacionBitacora[6] = txtEmail.Text;
                chkEmailSilencioso.Checked = _Cliente[0]["ENVIOSILEN"] == null ? false : _Cliente[0]["ENVIOSILEN"].ToString() == "S" ? true : false;

                //*****************************************************************************************************************************
                //******************************************************CONTACTOS**************************************************************
                //*****************************************************************************************************************************
                dtContactos = this.getContactosCliente(txtClave.Text.Trim());
                dgvContactos.DataSource = dtContactos;


                tabCliente.Enabled = true;
                //btnGuardar.Text = "Actualizar";
                btnGuardar.Visible = btnCancelar.Visible = true;
                btnGuardar.Enabled = btnCancelar.Enabled = true;

            }
        }

        private void txtRFC_Leave(object sender, EventArgs e)
        {
            String regRFC = @"^([A-ZÑ\x26]{3,4}([0-9]{2})(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1]))((-)?([A-Z\d]{3}))?$";
            if (txtRFC.Text.Trim() != "")
            {

                if (System.Text.RegularExpressions.Regex.IsMatch(txtRFC.Text.Trim().Replace("-", ""), regRFC))
                {
                    if (this.currentRFC.ToUpper().Trim().Replace("-", "") != txtRFC.Text.ToUpper().Trim().Replace("-", ""))
                    {
                        var busquedaRFC = from res in this.dtClientes.AsEnumerable() where (res.Field<String>("RFC") == null ? "" : res.Field<String>("RFC").ToUpper().Trim().Replace("-", "")) == txtRFC.Text.ToUpper().Trim().Replace("-", "") select res;
                        if (busquedaRFC.ToArray().Length > 0)
                        {
                            lblRFCValidacion.Text = "El RFC ya existe.";
                            lblRFCValidacion.ForeColor = Color.Red;
                            btnGuardar.Enabled = false;
                        }
                        else
                        {
                            lblRFCValidacion.Text = "RFC válido.";
                            lblRFCValidacion.ForeColor = Color.Green;
                            btnGuardar.Enabled = true;
                        }
                    }
                    else
                    {
                        lblRFCValidacion.Text = "RFC válido.";
                        lblRFCValidacion.ForeColor = Color.Green;
                        btnGuardar.Enabled = true;
                    }
                }
                else
                {
                    lblRFCValidacion.Text = "RFC no válido.";
                    lblRFCValidacion.ForeColor = Color.Red;
                    btnGuardar.Enabled = false;
                }
                // Asignamos tipoPersona con base a RFC
                this.tipoPersona = txtRFC.Text.Trim().Replace("-", "").Length == 13 ? 'F' : 'M';
                //REGIMEN FISCAL
                this.dtCatalogoRegimen = getCatalogoRegimenFiscal(this.tipoPersona);
                cmbRegimenFiscal.DisplayMember = "Descripcion";
                cmbRegimenFiscal.ValueMember = "Clave";
                cmbRegimenFiscal.DataSource = this.dtCatalogoRegimen;


            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            limpiaFormulario();
            activaControlesCredito(false);
            tabCliente.Enabled = true;
            btnContactoNuevo.Enabled = false;
            lblAlertaContacto.Visible = true;
            lblAccion.Text = "ALTA DE NUEVO CLIENTE";
            lblAccion.Visible = true;
            this.esEdicion = false;

            //btnGuardar.Text = "Guardar";
            btnGuardar.Visible = btnCancelar.Visible = true;
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validaAlta())
            {

                this.objClie = new CLIE01();
                this.objClieClib = new CLIE_CLIB01();

                //GENERALES

                if (rbIdentificacionMatriz.Checked)
                    objClie.TIPO_EMPRESA = "M";
                if (rbIdentificacionSucursal.Checked)
                    objClie.TIPO_EMPRESA = "S";


                objClie.RFC = txtRFC.Text.Trim();
                objClie.CURP = txtCURP.Text.Trim();
                objClie.NOMBRE = txtNombre.Text.Trim();
                objClie.REG_FISC = cmbRegimenFiscal.SelectedValue == null ? "" : cmbRegimenFiscal.SelectedValue.ToString();

                objClie.CALLE = txtCalle.Text.Trim();
                objClie.NUMEXT = txtNumExt.Text.Trim();
                objClie.NUMINT = txtNumInt.Text.Trim();
                objClie.CRUZAMIENTOS = txtEntreCalle.Text.Trim();
                objClie.CRUZAMIENTOS2 = txtYCalle.Text.Trim();
                objClie.COLONIA = txtColonia.Text.Trim();
                objClie.REFERDIR = txtReferencia.Text.Trim();
                objClie.LOCALIDAD = txtPoblacion.Text.Trim();
                objClie.CODIGO = txtCP.Text.Trim();
                objClie.ESTADO = txtEstado.Text.Trim();
                objClie.PAIS = txtPais.Text.Trim();
                objClie.MUNICIPIO = txtMunicipio.Text.Trim();
                objClie.NACIONALIDAD = txtNacionalidad.Text.Trim();
                objClie.TELEFONO = txtTelefono.Text;
                objClie.FAX = txtFax.Text;

                //CREDITO
                objClie.CON_CREDITO = chkManejoCredito.Checked ? "S" : "N";
                if (chkManejoCredito.Checked)
                {
                    objClie.DIASCRED = numDiasCredito.Value == null ? 0 : (int)numDiasCredito.Value;
                    objClie.LIMCRED = txtLimiteCredito.Text == "" ? 0 : double.Parse(txtLimiteCredito.Text);
                    objClie.SALDO = txtSaldo.Text == "" ? 0 : double.Parse(txtSaldo.Text);
                    objClie.METODODEPAGO = txtMetodoPago.Text.Trim();
                    objClie.NUMCTAPAGO = txtNumeroCuenta.Text.Trim();
                }

                objClie.CVE_VEND = lblClaveVendedor.Text.Trim();
                objClie.CUENTA_CONTABLE = txtCuentaContable.Text;
                objClieClib.CAMPLIB9 = txtFilial.Text.Trim();
                objClie.DESCUENTO = txtDescuento.Text == "" ? 0 : Double.Parse(txtDescuento.Text);

                //EMISION DE DOCUMENTOS

                objClie.IMPRIR = chkImprimir.Checked ? "S" : "N";
                objClie.MAIL = chkEnviarEmail.Checked ? "S" : "N";
                objClie.EMAILPRED = txtEmail.Text.Trim();
                objClie.ENVIOSILEN = chkEmailSilencioso.Checked ? "S" : "N";


                bgw = new BackgroundWorker();
                bgw.DoWork += bgw_DoWork;
                bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
                precarga.MostrarEspera();
                bgw.RunWorkerAsync();
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiaFormulario();
            btnGuardar.Visible = btnCancelar.Visible = false;
            tabCliente.Enabled = false;
            lblAccion.Visible = false;
        }
        private void btnActualizarClientes_Click(object sender, EventArgs e)
        {
            this.dtClientes = getClientes();
            var query = from res in dtClientes.AsEnumerable()
                        select new
                        {
                            CLAVE = res.Field<String>("CLAVE").ToString().Trim(),
                            RFC = res.Field<String>("RFC") == null ? "" : res.Field<String>("RFC").ToString().Trim(),
                            NOMBRE = res.Field<String>("NOMBRE") == null ? "" : res.Field<String>("NOMBRE").ToString().Trim()
                        };

            var dtClientesAux = query.ToArray();

            cmbClientesClave.DisplayMember = "CLAVE";
            cmbClientesClave.ValueMember = "CLAVE";

            cmbClientesRFC.DisplayMember = "RFC";
            cmbClientesRFC.ValueMember = "CLAVE";

            cmbClientesNombre.DisplayMember = "NOMBRE";
            cmbClientesNombre.ValueMember = "CLAVE";

            cmbClientesClave.DataSource = dtClientesAux;
            cmbClientesRFC.DataSource = dtClientesAux;
            cmbClientesNombre.DataSource = dtClientesAux;

            MessageBox.Show("El catálogo de clientes se ha actualizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void btnContactoNuevo_Click(object sender, EventArgs e)
        {
            activaAltaEdicionContacto(true);
            btnContactoAgregar.Text = "Agregar";
            this.esEdicionContacto = false;

        }
        private void btnContactoCancelar_Click(object sender, EventArgs e)
        {
            activaAltaEdicionContacto(false);
        }
        private void btnContactoAgregar_Click(object sender, EventArgs e)
        {
            ulp_bl.CONTAC01 objContact01 = new CONTAC01();
            objContact01.CVE_CLIE = txtClave.Text;
            objContact01.NOMBRE = txtContactoNombre.Text;
            objContact01.DIRECCION = txtContactoDireccion.Text;
            objContact01.TELEFONO = txtContactoTelefono.Text;
            objContact01.EMAIL = txtContactoEmail.Text;
            objContact01.TIPOCONTAC = cmbContactoTipo.SelectedValue.ToString();
            objContact01.STATUS = "A";

            if (this.esEdicionContacto)
            {
                objContact01.NCONTACTO = int.Parse(lblContactoClave.Text);
                AltaClienteSAE.setActualizaContacto(objContact01);
                MessageBox.Show("Se ha actualizado la información del contacto.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                AltaClienteSAE.setAltaContacto(objContact01);
                MessageBox.Show("El contacto se ha dado de alta de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.dtContactos = getContactosCliente(txtClave.Text);
            dgvContactos.DataSource = null;
            dgvContactos.DataSource = this.dtContactos;
            activaAltaEdicionContacto(false);

        }
        private void dgvContactos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7) //EDICION
            {
                activaAltaEdicionContacto(true);

                lblContactoClave.Text = dgvContactos["ContactoClave", e.RowIndex].Value.ToString();
                txtContactoNombre.Text = dgvContactos["ContactoNombre", e.RowIndex].Value.ToString();
                txtContactoDireccion.Text = dgvContactos["ContactoDireccion", e.RowIndex].Value.ToString();
                txtContactoTelefono.Text = dgvContactos["ContactoTelefono", e.RowIndex].Value.ToString();
                txtContactoEmail.Text = dgvContactos["ContactoEmail", e.RowIndex].Value.ToString();
                cmbContactoTipo.SelectedValue = dgvContactos["ContactoTipo", e.RowIndex].Value;

                this.esEdicionContacto = true;
                btnContactoAgregar.Text = "Actualizar";
            }
            if (e.ColumnIndex == 8) //ELIMINACION
            {

            }
        }
        private void chkManejoCredito_CheckedChanged(object sender, EventArgs e)
        {
            activaControlesCredito(chkManejoCredito.Checked);
            if (chkManejoCredito.Checked) numDiasCredito.Focus();

        }
        private void cmbVendedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbVendedores.SelectedValue != null)
            {
                lblClaveVendedor.Text = cmbVendedores.SelectedValue.ToString();
            }
        }
        #endregion
        #region "WORKERS"
        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            switch (this.esEdicion)
            {
                case true:
                    this.objClie.CLAVE = this.clienteSeleccionado;
                    this.objClieClib.CVE_CLIE = this.clienteSeleccionado;

                    AltaClienteSAE.setActualizaCliente(this.objClie, this.objClieClib);
                    String _ov = String.Empty;
                    String _nv = String.Empty;
                    //RFC
                    if (this.comparacionBitacora[0] != objClie.RFC)
                    {
                        _ov = this.comparacionBitacora[0];
                        _nv = objClie.RFC;
                        BITA01.setAltaBitacora(BITA01.ModuloBitacora.SAE, DateTime.Now, Environment.MachineName, "Aspel-SAE 8.00", Globales.DatosUsuario.CLAVE, "1", "1", "2", "Se actualizó el RFC del Cliente [" + objClie.CLAVE.ToString().Trim() + "]", "Intermedio", _ov, _nv);
                        break;
                    }
                    //CURP
                    if (this.comparacionBitacora[1] != objClie.CURP)
                    {
                        _ov = this.comparacionBitacora[1];
                        _nv = objClie.CURP;
                        BITA01.setAltaBitacora(BITA01.ModuloBitacora.SAE, DateTime.Now, Environment.MachineName, "Aspel-SAE 8.00", Globales.DatosUsuario.CLAVE, "1", "1", "2", "Se actualizó el CURP del Cliente [" + objClie.CLAVE.ToString().Trim() + "]", "Intermedio", _ov, _nv);
                        break;
                    }
                    //LIMITE DE CREDITO
                    if (this.comparacionBitacora[2] != objClie.LIMCRED.ToString())
                    {
                        _ov = this.comparacionBitacora[2];
                        _nv = objClie.LIMCRED.ToString();
                        BITA01.setAltaBitacora(BITA01.ModuloBitacora.SAE, DateTime.Now, Environment.MachineName, "Aspel-SAE 8.00", Globales.DatosUsuario.CLAVE, "1", "1", "2", "Se actualizó el límite de crédito del Cliente [" + objClie.CLAVE.ToString().Trim() + "]", "Intermedio", _ov, _nv);
                        break;
                    }
                    //VENDEDOR
                    if (this.comparacionBitacora[3] != objClie.CVE_VEND)
                    {
                        _ov = this.comparacionBitacora[3];
                        _nv = objClie.CVE_VEND;
                        BITA01.setAltaBitacora(BITA01.ModuloBitacora.SAE, DateTime.Now, Environment.MachineName, "Aspel-SAE 8.00", Globales.DatosUsuario.CLAVE, "1", "1", "2", "Se actualizó el vendedor del Cliente [" + objClie.CLAVE.ToString().Trim() + "]", "Intermedio", _ov, _nv);
                        break;
                    }
                    //PORCENTAJE
                    if (this.comparacionBitacora[4] != objClie.DESCUENTO.ToString())
                    {
                        _ov = this.comparacionBitacora[4];
                        _nv = objClie.DESCUENTO.ToString();
                        BITA01.setAltaBitacora(BITA01.ModuloBitacora.SAE, DateTime.Now, Environment.MachineName, "Aspel-SAE 8.00", Globales.DatosUsuario.CLAVE, "1", "1", "2", "Se actualizó el descuento del Cliente [" + objClie.CLAVE.ToString().Trim() + "]", "Intermedio", _ov, _nv);
                        break;
                    }
                    //cuenta contable
                    if (this.comparacionBitacora[5] != objClie.CUENTA_CONTABLE)
                    {
                        _ov = this.comparacionBitacora[5];
                        _nv = objClie.CUENTA_CONTABLE;
                        BITA01.setAltaBitacora(BITA01.ModuloBitacora.SAE, DateTime.Now, Environment.MachineName, "Aspel-SAE 8.00", Globales.DatosUsuario.CLAVE, "1", "1", "2", "Se actualizó la cuenta contable del Cliente [" + objClie.CLAVE.ToString().Trim() + "]", "Intermedio", _ov, _nv);
                        break;
                    }
                    //EMAIL
                    if (this.comparacionBitacora[6] != objClie.EMAILPRED)
                    {
                        _ov = this.comparacionBitacora[6];
                        _nv = objClie.EMAILPRED;
                        BITA01.setAltaBitacora(BITA01.ModuloBitacora.SAE, DateTime.Now, Environment.MachineName, "Aspel-SAE 8.00", Globales.DatosUsuario.CLAVE, "1", "1", "2", "Se actualizó el Email del Cliente [" + objClie.CLAVE.ToString().Trim() + "]", "Intermedio", _ov, _nv);
                        break;
                    }
                    //NOMBRE
                    if (this.comparacionBitacora[7] != objClie.NOMBRE)
                    {
                        _ov = this.comparacionBitacora[7];
                        _nv = objClie.NOMBRE;
                        BITA01.setAltaBitacora(BITA01.ModuloBitacora.SAE, DateTime.Now, Environment.MachineName, "Aspel-SAE 8.00", Globales.DatosUsuario.CLAVE, "1", "1", "2", "Se actualizó el Nombre del Cliente [" + objClie.CLAVE.ToString().Trim() + "]", "Intermedio", _ov, _nv);
                        break;
                    }

                    BITA01.setAltaBitacora(BITA01.ModuloBitacora.SAE, DateTime.Now, Environment.MachineName, "Aspel-SAE 8.00", Globales.DatosUsuario.CLAVE, "1", "1", "2", "Actualización de Cliente [" + objClie.CLAVE.ToString().Trim() + "]", "Intermedio", "", "");
                    break;
                default:
                    nuevaClave = AltaClienteSAE.setAltaCliente(this.objClie, this.objClieClib);
                    this.objClieClib.CVE_CLIE = nuevaClave;

                    BITA01.setAltaBitacora(BITA01.ModuloBitacora.SAE, DateTime.Now, Environment.MachineName, "Aspel-SAE 8.00", Globales.DatosUsuario.CLAVE, "1", "1", "1", "Alta de de Cliente [" + nuevaClave.ToString().Trim() + "]", "Intermedio", "", "");


                    break;

            }
        }
        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (this.esEdicion)
                MessageBox.Show("El Cliente se ha actualizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("El Cliente se dió de alta de forma correcta con la clave: " + nuevaClave, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.dtClientes = getClientes();
            var query = from res in dtClientes.AsEnumerable()
                        select new
                        {
                            CLAVE = res.Field<String>("CLAVE").ToString().Trim(),
                            RFC = res.Field<String>("RFC") == null ? "" : res.Field<String>("RFC").ToString().Trim(),
                            NOMBRE = res.Field<String>("NOMBRE") == null ? "" : res.Field<String>("NOMBRE").ToString().Trim()
                        };

            var dtClientesAux = query.ToArray();

            //CLIENTES

            cmbClientesClave.DisplayMember = "CLAVE";
            cmbClientesClave.ValueMember = "CLAVE";

            cmbClientesRFC.DisplayMember = "RFC";
            cmbClientesRFC.ValueMember = "CLAVE";

            cmbClientesNombre.DisplayMember = "NOMBRE";
            cmbClientesNombre.ValueMember = "CLAVE";

            cmbClientesClave.DataSource = dtClientesAux;
            cmbClientesRFC.DataSource = dtClientesAux;
            cmbClientesNombre.DataSource = dtClientesAux;

            limpiaFormulario();
            precarga.RemoverEspera();
            tabCliente.Enabled = false;
            btnGuardar.Visible = false;
            btnCancelar.Visible = false;
            lblAccion.Visible = false;
        }
        #endregion
        #region "METODOS"
        DataTable getClientes()
        {
            return AltaClienteSAE.getClientesSAE();
        }
        DataTable getVendedores()
        {
            return AltaClienteSAE.getVendedoresSAE(Globales.UsuarioActual.UsuarioPerfil);
        }
        DataTable getCatalogoRegimenFiscal(char tipoPersona)
        {
            return AltaClienteSAE.getCatalogoRegimenFiscal(tipoPersona);
        }
        DataTable getContactosCliente(String Clave)
        {
            DataTable dtContactos = new DataTable();
            dtContactos = AltaClienteSAE.getContactosClientesSAE(Clave);
            return dtContactos;
        }
        void activaControlesCredito(Boolean conCredito)
        {
            chkManejoCredito.Checked = conCredito;
            numDiasCredito.Enabled = conCredito;
            txtLimiteCredito.Enabled = conCredito;
            txtDiaPago.Enabled = conCredito;
            txtDiaRevision.Enabled = conCredito;
            txtSaldo.Enabled = conCredito;
            txtMetodoPago.Enabled = conCredito;
            txtNumeroCuenta.Enabled = conCredito;
        }
        Boolean validaAlta()
        {
            String regRFC = @"^([A-ZÑ\x26]{3,4}([0-9]{2})(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1]))((-)?([A-Z\d]{3}))?$";
            String regCURP = @"^[A-Z][A,E,I,O,U,X][A-Z]{2}[0-9]{2}[0-1][0-9][0-3][0-9][M,H][A-Z]{2}[B,C,D,F,G,H,J,K,L,M,N,Ñ,P,Q,R,S,T,V,W,X,Y,Z]{3}[0-9,A-Z][0-9]?$";
            String regEmail = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

            if (txtNombre.Text.Trim() == "")
            {
                MessageBox.Show("El Nombre es obligatorio.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (txtRFC.Text.Trim() == "")
            {
                MessageBox.Show("El RFC es obligatorio.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(txtRFC.Text.Trim().Replace("-", ""), regRFC))
                {
                    MessageBox.Show("El RFC no es válido.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            if (txtCURP.Text.Trim() != "")
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(txtCURP.Text.Trim().Replace("-", ""), regCURP))
                {
                    MessageBox.Show("El CURP no es válido.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            if (txtEmail.Text.Trim() != "")
            {
                if (txtEmail.Text.Trim().Contains(" "))
                {
                    MessageBox.Show("El Email es incorrecto, en caso de ocupar mas de 1 correo, favor de separarlo con el caractér (punto y coma)", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                List<String> ListaEmail = new List<string> { };
                ListaEmail = txtEmail.Text.Split(';').ToList();
                foreach (String _email in ListaEmail)
                {
                    try
                    {
                        System.Net.Mail.MailAddress m = new System.Net.Mail.MailAddress(_email);
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("El email " + _email + " no es válido", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }

            if (lblVendedor.Text == "-" || lblVendedor.Text == "")
            {
                MessageBox.Show("El Vendedor es obligatorio.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;

        }
        void limpiaFormulario()
        {
            txtClave.Text = "POR ASIGNAR";
            txtEstatus.Text = "ACTIVO";
            txtRFC.Text = "";
            txtCURP.Text = "";
            rbIdentificacionMatriz.Checked = true; rbIdentificacionSucursal.Checked = false;
            txtNombre.Text = "";
            lblRFCValidacion.Text = "";
            txtCalle.Text = "";
            txtNumExt.Text = "";
            txtNumInt.Text = "";
            txtEntreCalle.Text = "";
            txtYCalle.Text = "";
            txtColonia.Text = "";
            txtReferencia.Text = "";
            txtPoblacion.Text = "";
            txtCP.Text = "";
            txtEstado.Text = "";
            txtPais.Text = "";
            txtMunicipio.Text = "";
            txtNacionalidad.Text = "";
            txtTelefono.Text = "";
            txtFax.Text = "";

            chkManejoCredito.Checked = false;
            numDiasCredito.Value = 0;
            txtLimiteCredito.Text = "";
            txtSaldo.Text = "";
            txtMetodoPago.Text = "";
            txtNumeroCuenta.Text = "";
            txtDiaPago.Text = "";
            txtDiaRevision.Text = "";
            cmbVendedores.SelectedIndex = -1;
            txtCuentaContable.Text = "1111-0001-000";
            txtFilial.Text = "";

            chkImprimir.Checked = false;
            chkEnviarEmail.Checked = false;
            txtEmail.Text = "";
            chkEmailSilencioso.Checked = false;

            this.currentRFC = "";

            dgvContactos.DataSource = null;

            tabCliente.SelectedIndex = 0;

        }
        void activaAltaEdicionContacto(Boolean activa)
        {
            txtContactoNombre.Text = "";
            txtContactoDireccion.Text = "";
            txtContactoTelefono.Text = "";
            txtContactoEmail.Text = "";
            cmbContactoTipo.SelectedIndex = 0;
            groupBox6.Visible = activa;
        }
        #endregion
        #region "COMPOSICION"
        class TipoContacto
        {
            public String Clave { get; set; }
            public String Descripcion { get; set; }
            public TipoContacto()
            {
                this.Clave = "";
                this.Descripcion = "";
            }
        }
        #endregion

        private void lblFilial_Click(object sender, EventArgs e)
        {

        }

        private void txtLimiteCredito_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtRFC_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabDatosGenerales_Click(object sender, EventArgs e)
        {

        }
    }
}
