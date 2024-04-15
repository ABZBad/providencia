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
    public partial class frmAltaBeneficiarioBanco : Form
    {
        #region "ATRIBUTOS Y CONSTRUCTORES"

        Boolean esEdicion = false;
        DataTable dtBeneficiarios;

        String currentRFC = String.Empty;
        String nuevaClave = String.Empty;
        String beneficiarioSeleccionado = String.Empty;

        private BackgroundWorker bgw;
        private Precarga precarga;

        BENEF objBenef;

        //1=RFC, 2=CUENTA CONTABLE, 3=BANCO, 4=SUCURSAL, 5=REFERENCIA, 6=CUENTA, 7 = CLABE, 8 = NOMBRE
        String[] comparacionBitacora = { "", "", "", "", "", "", "","" };

        public frmAltaBeneficiarioBanco()
        {
            InitializeComponent();
            precarga = new Precarga(this);
            this.objBenef = new BENEF();
        }
        #endregion
        #region "EVENTOS"
        private void frmAltaBeneficiarioBanco_Load(object sender, EventArgs e)
        {
            this.dtBeneficiarios = getBeneficiarios();

            cmbBeneficiarioClave.DisplayMember = "NUM_REG";
            cmbBeneficiarioClave.ValueMember = "NUM_REG";
            cmbBeneficiarioClave.DataSource = dtBeneficiarios;

            cmbBeneficiarioRFC.DisplayMember = "RFC";
            cmbBeneficiarioRFC.ValueMember = "NUM_REG";
            cmbBeneficiarioRFC.DataSource = dtBeneficiarios;

            cmbBeneficiarioNombre.DisplayMember = "NOMBRE";
            cmbBeneficiarioNombre.ValueMember = "NUM_REG";
            cmbBeneficiarioNombre.DataSource = dtBeneficiarios;
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (cmbBeneficiarioClave.SelectedValue == null)
            {
                MessageBox.Show("El Beneficiario no existe.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            dtBeneficiarios.PrimaryKey = new[] { dtBeneficiarios.Columns["NUM_REG"] };

            var Beneficiario = (from res in this.dtBeneficiarios.AsEnumerable() where res.Field<int>("NUM_REG") == int.Parse(cmbBeneficiarioClave.SelectedValue.ToString().Trim()) select res);

            List<DataRow> _Beneficiario = Beneficiario.ToList();

            if (_Beneficiario.Count > 0)
            {
                this.esEdicion = true;
                this.beneficiarioSeleccionado = cmbBeneficiarioClave.SelectedValue.ToString();
                lblAccion.Text = "EDICIÓN DE BENEFICIARIO";
                lblAccion.Visible = true;


                String regRFC = @"^([A-ZÑ\x26]{3,4}([0-9]{2})(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1]))((-)?([A-Z\d]{3}))?$";
                this.currentRFC = _Beneficiario[0]["RFC"].ToString().Trim();

                //*****************************************************************************************************************************
                //**************************************************DATOS GENERALES************************************************************
                //*****************************************************************************************************************************

                txtClave.Text = _Beneficiario[0]["NUM_REG"] == null ? "" : _Beneficiario[0]["NUM_REG"].ToString().Trim();
                txtNombre.Text = _Beneficiario[0]["NOMBRE"] == null ? "" : _Beneficiario[0]["NOMBRE"].ToString().Trim();
                this.comparacionBitacora[7] = txtNombre.Text;
                txtRFC.Text = _Beneficiario[0]["RFC"] == null ? "" : _Beneficiario[0]["RFC"].ToString().Trim();
                this.comparacionBitacora[0] = txtRFC.Text;
                if (txtRFC.Text != "")
                {
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
                }

                txtGenerales.Text = _Beneficiario[0]["INF_GENERAL"] == null ? "" : _Beneficiario[0]["INF_GENERAL"].ToString().Trim();
                txtCuentaContable.Text = _Beneficiario[0]["CTA_CONTAB"] == null ? "" : _Beneficiario[0]["CTA_CONTAB"].ToString().Trim();
                this.comparacionBitacora[1] = txtCuentaContable.Text;
                //*****************************************************************************************************************************
                //**************************************************DATOS BANCARIOS************************************************************
                //*****************************************************************************************************************************
                txtBanco.Text = _Beneficiario[0]["BANCO"] == null ? "" : _Beneficiario[0]["BANCO"].ToString().Trim();
                this.comparacionBitacora[2] = txtBanco.Text;
                txtSucursal.Text = _Beneficiario[0]["SUCURSAL"] == null ? "" : _Beneficiario[0]["SUCURSAL"].ToString().Trim();
                this.comparacionBitacora[3] = txtSucursal.Text;
                txtReferencia.Text = _Beneficiario[0]["REFERENCIA"] == null ? "" : _Beneficiario[0]["REFERENCIA"].ToString().Trim();
                this.comparacionBitacora[4] = txtReferencia.Text;
                txtCuenta.Text = _Beneficiario[0]["CUENTA"] == null ? "" : _Beneficiario[0]["CUENTA"].ToString().Trim();
                this.comparacionBitacora[5] = txtCuenta.Text;
                txtClabe.Text = _Beneficiario[0]["CLABE"] == null ? "" : _Beneficiario[0]["CLABE"].ToString().Trim();
                this.comparacionBitacora[6] = txtClabe.Text;
                chkBancoExterno.Checked = _Beneficiario[0]["ESBANCOEXT"] == null ? false : _Beneficiario[0]["ESBANCOEXT"].ToString() == "1" ? true : false;
                txtDescripcion.Text = _Beneficiario[0]["BANCODESC"] == null ? "" : _Beneficiario[0]["BANCODESC"].ToString().Trim();

                btnGuardar.Visible = btnCancelar.Visible = true;
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
                        var busquedaRFC = from res in this.dtBeneficiarios.AsEnumerable() where (res.Field<String>("RFC") == null ? "" : res.Field<String>("RFC").ToUpper().Trim().Replace("-", "")) == txtRFC.Text.ToUpper().Trim().Replace("-", "") select res;
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
            }
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            limpiaFormulario();
            lblAccion.Text = "ALTA DE NUEVO BENEFICIARIO";
            lblAccion.Visible = true;
            this.esEdicion = false;
            btnGuardar.Visible = btnCancelar.Visible = true;
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiaFormulario();
            btnGuardar.Visible = btnCancelar.Visible = false;
            lblAccion.Visible = false;
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validaAlta())
            {

                this.objBenef = new BENEF();

                this.objBenef.NOMBRE = txtNombre.Text.Trim();
                this.objBenef.RFC = txtRFC.Text.Trim();
                this.objBenef.INF_GENERAL = txtGenerales.Text.Trim();
                this.objBenef.CTA_CONTAB = txtCuentaContable.Text.Trim();

                this.objBenef.BANCO = txtBanco.Text.Trim();
                this.objBenef.SUCURSAL = txtSucursal.Text.Trim();
                this.objBenef.REFERENCIA = txtReferencia.Text.Trim();
                this.objBenef.CUENTA = txtCuenta.Text.Trim();
                this.objBenef.CLABE = txtClabe.Text.Trim();
                this.objBenef.ESBANCOEXT = chkBancoExterno.Checked ? 1 : 0;
                this.objBenef.BANCODESC = txtDescripcion.Text.Trim();

                bgw = new BackgroundWorker();
                bgw.DoWork += bgw_DoWork;
                bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
                precarga.MostrarEspera();
                bgw.RunWorkerAsync();

            }

        }

        private void btnActualizarClientes_Click(object sender, EventArgs e)
        {
            this.dtBeneficiarios = getBeneficiarios();

            cmbBeneficiarioClave.DisplayMember = "NUM_REG";
            cmbBeneficiarioClave.ValueMember = "NUM_REG";
            cmbBeneficiarioClave.DataSource = dtBeneficiarios;

            cmbBeneficiarioRFC.DisplayMember = "RFC";
            cmbBeneficiarioRFC.ValueMember = "NUM_REG";
            cmbBeneficiarioRFC.DataSource = dtBeneficiarios;

            cmbBeneficiarioNombre.DisplayMember = "NOMBRE";
            cmbBeneficiarioNombre.ValueMember = "NUM_REG";
            cmbBeneficiarioNombre.DataSource = dtBeneficiarios;

            MessageBox.Show("El catálogo de Beneficiarios se ha actualizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion
        #region "WORKERS"
        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            switch (this.esEdicion)
            {
                case true:
                    objBenef.NUM_REG = int.Parse(this.beneficiarioSeleccionado);
                    AltaBeneficiarioBanco.setActualizaBeneficiario(this.objBenef);
                    String _ov = String.Empty;
                    String _nv = String.Empty;
                    //RFC

                    if (this.comparacionBitacora[0] != objBenef.RFC)
                    {
                        _ov = this.comparacionBitacora[0];
                        _nv = objBenef.RFC;
                        BITA01.setAltaBitacora(BITA01.ModuloBitacora.BANCO, DateTime.Now, Environment.MachineName, "Aspel-Banco 4.0", Globales.DatosUsuario.CLAVE, "6", "1", "2", "Se actualizó el RFC del Beneficiario [" + objBenef.NUM_REG.ToString().Trim() + "]", "Intermedio", _ov, _nv);
                        break;
                    }

                    //CUENTA CONTABLE


                    if (this.comparacionBitacora[1] != objBenef.CTA_CONTAB)
                    {
                        _ov = this.comparacionBitacora[1];
                        _nv = objBenef.CTA_CONTAB;
                        BITA01.setAltaBitacora(BITA01.ModuloBitacora.BANCO, DateTime.Now, Environment.MachineName, "Aspel-Banco 4.0", Globales.DatosUsuario.CLAVE, "6", "1", "2", "Se actualizó la cuenta contable del Beneficiario [" + objBenef.NUM_REG.ToString().Trim() + "]", "Intermedio", _ov, _nv);
                        break;
                    }

                    //BANCO

                    if (this.comparacionBitacora[2] != objBenef.BANCO)
                    {
                        _ov = this.comparacionBitacora[2];
                        _nv = objBenef.BANCO;
                        BITA01.setAltaBitacora(BITA01.ModuloBitacora.BANCO, DateTime.Now, Environment.MachineName, "Aspel-Banco 4.0", Globales.DatosUsuario.CLAVE, "6", "1", "2", "Se actualizó el Banco del Beneficiario [" + objBenef.NUM_REG.ToString().Trim() + "]", "Intermedio", _ov, _nv);
                        break;
                    }

                    //SUCURSAL

                    if (this.comparacionBitacora[3] != objBenef.SUCURSAL)
                    {
                        _ov = this.comparacionBitacora[3];
                        _nv = objBenef.SUCURSAL;
                        BITA01.setAltaBitacora(BITA01.ModuloBitacora.BANCO, DateTime.Now, Environment.MachineName, "Aspel-Banco 4.0", Globales.DatosUsuario.CLAVE, "6", "1", "2", "Se actualizó la sucursal del Beneficiario [" + objBenef.NUM_REG.ToString().Trim() + "]", "Intermedio", _ov, _nv);
                        break;
                    }

                    //REFERENCIA

                    if (this.comparacionBitacora[4] != objBenef.REFERENCIA)
                    {
                        _ov = this.comparacionBitacora[4];
                        _nv = objBenef.REFERENCIA;
                        BITA01.setAltaBitacora(BITA01.ModuloBitacora.BANCO, DateTime.Now, Environment.MachineName, "Aspel-Banco 4.0", Globales.DatosUsuario.CLAVE, "6", "1", "2", "Se actualizó la referencia Bancaria del Beneficiario [" + objBenef.NUM_REG.ToString().Trim() + "]", "Intermedio", _ov, _nv);
                        break;
                    }

                    //CUENTA

                    if (this.comparacionBitacora[5] != objBenef.CUENTA)
                    {
                        _ov = this.comparacionBitacora[5];
                        _nv = objBenef.CUENTA;
                        BITA01.setAltaBitacora(BITA01.ModuloBitacora.BANCO, DateTime.Now, Environment.MachineName, "Aspel-Banco 4.0", Globales.DatosUsuario.CLAVE, "6", "1", "2", "Se actualizó el número de cuenta del Beneficiario [" + objBenef.NUM_REG.ToString().Trim() + "]", "Intermedio", _ov, _nv);
                        break;
                    }

                    //CUENTA

                    if (this.comparacionBitacora[6] != objBenef.CLABE)
                    {
                        _ov = this.comparacionBitacora[6];
                        _nv = objBenef.CLABE;
                        BITA01.setAltaBitacora(BITA01.ModuloBitacora.BANCO, DateTime.Now, Environment.MachineName, "Aspel-Banco 4.0", Globales.DatosUsuario.CLAVE, "6", "1", "2", "Se actualizó la CLABE del Beneficiario [" + objBenef.NUM_REG.ToString().Trim() + "]", "Intermedio", _ov, _nv);
                        break;
                    }

                    //CUENTA

                    if (this.comparacionBitacora[7] != objBenef.NOMBRE)
                    {
                        _ov = this.comparacionBitacora[7];
                        _nv = objBenef.NOMBRE;
                        BITA01.setAltaBitacora(BITA01.ModuloBitacora.BANCO, DateTime.Now, Environment.MachineName, "Aspel-Banco 4.0", Globales.DatosUsuario.CLAVE, "6", "1", "2", "Se actualizó el Nombre del Beneficiario [" + objBenef.NUM_REG.ToString().Trim() + "]", "Intermedio", _ov, _nv);
                        break;
                    }




                    BITA01.setAltaBitacora(BITA01.ModuloBitacora.BANCO, DateTime.Now, Environment.MachineName, "Aspel-Banco 4.0", Globales.DatosUsuario.CLAVE, "6", "1", "2", "Se actualizó el Beneficiario [" + objBenef.NUM_REG.ToString().Trim() + "]", "Intermedio", "", "");
                    break;
                default:
                    nuevaClave = AltaBeneficiarioBanco.setAltaBeneficiario(this.objBenef);
                    BITA01.setAltaBitacora(BITA01.ModuloBitacora.BANCO, DateTime.Now, Environment.MachineName, "Aspel-Banco 4.0", Globales.DatosUsuario.CLAVE, "6", "1", "1", "Alta de de Beneficiario [" + nuevaClave.ToString().Trim() + "]", "Intermedio", "", "");
                    break;

            }
        }
        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (this.esEdicion)
                MessageBox.Show("El Beneficiario se ha actualizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("El Beneficiario se dió de alta de forma correcta con la clave: " + nuevaClave, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.dtBeneficiarios = getBeneficiarios();

            cmbBeneficiarioClave.DisplayMember = "NUM_REG";
            cmbBeneficiarioClave.ValueMember = "NUM_REG";
            cmbBeneficiarioClave.DataSource = dtBeneficiarios;

            cmbBeneficiarioRFC.DisplayMember = "RFC";
            cmbBeneficiarioRFC.ValueMember = "NUM_REG";
            cmbBeneficiarioRFC.DataSource = dtBeneficiarios;

            cmbBeneficiarioNombre.DisplayMember = "NOMBRE";
            cmbBeneficiarioNombre.ValueMember = "NUM_REG";
            cmbBeneficiarioNombre.DataSource = dtBeneficiarios;

            limpiaFormulario();
            precarga.RemoverEspera();
            btnGuardar.Visible = false;
            btnCancelar.Visible = false;
            lblAccion.Visible = false;
        }
        #endregion
        #region "METODOS"
        DataTable getBeneficiarios()
        {
            return AltaBeneficiarioBanco.getBeneficiariosSAE();
        }
        void limpiaFormulario()
        {
            txtClave.Text = "";
            txtNombre.Text = "";
            txtRFC.Text = "";
            txtGenerales.Text = "";
            txtCuentaContable.Text = "";
            this.currentRFC = "";

            lblRFCValidacion.Text = "";
        }
        public Boolean validaAlta()
        {
            String regRFC = @"^([A-ZÑ\x26]{3,4}([0-9]{2})(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1]))((-)?([A-Z\d]{3}))?$";
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
            return true;
        }
        #endregion
    }
}
