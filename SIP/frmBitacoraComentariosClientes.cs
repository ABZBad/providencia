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
    public partial class frmBitacoraComentariosClientes : Form
    {
        DataTable dtClientes;
        public frmBitacoraComentariosClientes()
        {
            this.dtClientes = new DataTable();
            InitializeComponent();
        }
        private void frmBitacoraComentariosClientes_Load(object sender, EventArgs e)
        {
            this.dtClientes = BitacoraComentarioClientes.getClientesSAE(Globales.UsuarioActual.Id);

            if (dtClientes.Rows.Count == 0)
            {
                MessageBox.Show("El Usuario no cuenta con clientes asignados.", "SUP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
            }
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

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (BitacoraComentarioClientes.setAltaBitacoraComentarioCliente(Globales.UsuarioActual.Id, cmbClientesClave.SelectedValue.ToString().Trim(), txtComentarios.Text) == 1)
            {
                MessageBox.Show("Se ha registrado el seguimiento de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiaFormulario();
            }
            else
            {
                MessageBox.Show("Ocurrió un error al dar de alta el segumiento, favor de ponerse en contacto con el Administrador del Sistema.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void LimpiaFormulario()
        {
            cmbClientesClave.SelectedIndex = 0;
            txtComentarios.Text = "";
            dtpFecha.Value = DateTime.Now;
        }

    }
}