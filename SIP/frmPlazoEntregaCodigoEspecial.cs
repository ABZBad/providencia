using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ulp_bl;

namespace SIP
{
    public partial class frmPlazoEntregaCodigoEspecial : Form
    {
        public String Plazo { get; set; }
        public String Codigo { get; set; }

        public frmPlazoEntregaCodigoEspecial()
        {
            this.Plazo = "";
            this.Codigo = "";
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtCodigoEspecial.Text.Trim() != "" && txtPlazoEntrega.Text.Trim() != "")
            {
                //VERIFICAMOS QUE EL COIGO ASIGNADO EXISTA EN LA BD
                Exception ex = new Exception();
                DataTable dtInfoModelo = SimuladorCostos.ModeloExistente(txtCodigoEspecial.Text.Trim().ToUpper(), ref ex);
                if (dtInfoModelo == null)
                { MessageBox.Show("El código no se encontro en el Sistema, el proceso no puede continuar.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                else if (dtInfoModelo.Rows.Count == 0)
                { MessageBox.Show("El código no se encontro en el Sistema, el proceso no puede continuar.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                this.Plazo = txtPlazoEntrega.Text.Trim();
                this.Codigo = txtCodigoEspecial.Text.Trim().ToUpper();
                this.Close();
            }
            else
                MessageBox.Show("Los datos son obligatorios.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Plazo = "";
            this.Codigo = "";
            this.Close();
        }

        private void txtCodigoEspecial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                btnAceptar_Click(null, null);
            }
        }
    }
}
