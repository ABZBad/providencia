using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ulp_bl.Permisos;
using ulp_bl;

namespace SIP
{
    public partial class frmAltaAgente : Form
    {
        public frmAltaAgente()
        {
            InitializeComponent();
        }

        private void frmAltaAgente_Load(object sender, EventArgs e)
        {
            cmbTipoUsuario.SelectedIndex = 0;
            cmbArea.DataSource = ControlPedidos.getAreasSIP(false);
            cmbArea.DisplayMember = "DescripcionArea";
            cmbArea.ValueMember = "ClaveArea";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.AutoValidate = AutoValidate.Disable;
            this.Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {

                if (txtContrasena.Text != txtContrasena2.Text)
                {
                    txtContrasena.Focus();
                    errorProvider1.SetError(txtContrasena2, "Las contraseñas no son las mismas");
                }
                else
                {


                    Usuario usuarioExistente = new Usuario();
                    usuarioExistente = usuarioExistente.Consultar(txtIdApp.Text);

                    if (usuarioExistente.UsuarioUsuario == null)
                    {

                        if (
                            MessageBox.Show(
                                string.Format("¿ Confirma dar de alta al usuario: \"{0}\" ?", txtIdApp.Text),
                                "Confirme", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {

                            Cursor = Cursors.WaitCursor;
                            Usuario usuario = new Usuario();
                            usuario.UsuarioNombre = txtNombre.Text;
                            usuario.UsuarioUsuario = txtIdApp.Text;
                            usuario.UsuarioContraseña = Utilerias.GenerarMD5Hash(txtContrasena.Text);
                            usuario.UsuarioFechaIngreso = DateTime.Now;
                            usuario.UsuarioStatus = true;
                            usuario.UsuarioCorreo = "none";
                            usuario.UsuarioArea = cmbArea.SelectedValue.ToString();
                            usuario.Crear(usuario);
                            usuario.CrearSae60(usuario, txtAcceso.Text, cmbTipoUsuario.SelectedItem.ToString());
                            Usuario usuarioInsertado = new Usuario();
                            usuarioInsertado = usuarioInsertado.Consultar(txtIdApp.Text);
                            Cursor = Cursors.Default;
                            if (!usuario.TieneError)
                            {
                                DialogResult resultado =
                                    MessageBox.Show(
                                        Properties.Resources.Cadena_DatosGuardados +
                                        "\n\r\n\r¿ Desea asignar permisos al usuario: " + txtIdApp.Text + " ?",
                                        "Confirme", MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question);
                                if (resultado == DialogResult.Yes)
                                {
                                    frmPermisos frmPermisos = new frmPermisos(usuarioInsertado.Id);
                                    frmPermisos.Show();
                                    this.Close();
                                }
                            }
                            else
                            {
                                Cursor = Cursors.Default;
                                MessageBox.Show(Properties.Resources.Cadena_ErrorAlGuardar + "\n\r\n\r" +
                                                usuario.Error, "Error", MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                            }
                        }
                    }
                    else
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show(Properties.Resources.Cadena_ErrorAlGuardar + "\n\r\n\r" +
                                        string.Format("El usuario: \"{0}\" ya existe", txtIdApp.Text), "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                }
            }
        }

        private void txtNombre_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                txtNombre.Focus();
                errorProvider1.SetError(txtNombre, "El Nombre es un dato requerido");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtNombre, "");
            }
        }

        private void txtIdApp_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtIdApp.Text))
            {
                txtIdApp.Focus();
                errorProvider1.SetError(txtIdApp, "El Usuario es un dato requerido");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtIdApp, "");
            }
        }

        private void txtContrasena_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtContrasena.Text))
            {
                txtNombre.Focus();
                errorProvider1.SetError(txtContrasena, "La contraseña es un dato requerido");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtContrasena, "");
            }
        }

        private void txtContrasena2_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtContrasena2.Text))
            {
                txtContrasena2.Focus();
                errorProvider1.SetError(txtContrasena2, "La confirmación de la contraseña es un dato requerido");
                e.Cancel = true;

            }
            else
            {
                errorProvider1.SetError(txtContrasena2, "");
            }
        }

        private void cmbTipoUsuario_Validating(object sender, CancelEventArgs e)
        {
            if (cmbTipoUsuario.SelectedIndex == 0)
            {
                cmbTipoUsuario.Focus();
                errorProvider1.SetError(cmbTipoUsuario, "Seleccione un tipo de usuario");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(cmbTipoUsuario, "");
            }
        }

        private void txtAcceso_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtAcceso.Text.Trim()))
            {
                txtAcceso.Focus();
                errorProvider1.SetError(txtAcceso, "Escriba el nivel de acceso: \"L\" o \"T\"");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtAcceso, "");
            }
        }
    }
}
