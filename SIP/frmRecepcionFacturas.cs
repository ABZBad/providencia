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
    public partial class frmRecepcionFacturas : Form
    {
        DataTable dtFacturas;
        Boolean esRecepcion;
        public frmRecepcionFacturas(Boolean esRecepcion)
        {
            InitializeComponent();
            dtFacturas = new DataTable();
            this.esRecepcion = esRecepcion;
        }

        private void frmRecepción_Load(object sender, EventArgs e)
        {
            this.dtFacturas = RecepcionFacturas.getFacturas();
            dgvFacturas.DataSource = dtFacturas;
            dgvFacturas.ClearSelection();
            SeteaFormulario(this.esRecepcion);


        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {

                if (e.KeyChar == (char)13)
                {
                    if (dtFacturas.Rows.Count > 0)
                    {
                        dgvFacturas.Rows[0].Selected = true;
                        dgvFacturas.Refresh();
                        dgvFacturas.Focus();
                    }

                }
            }
            catch { }
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            dtFacturas.DefaultView.RowFilter = string.Format("FACTURA LIKE '%{0}%'", txtBusqueda.Text);
            dgvFacturas.ClearSelection();
        }

        private void dgvFacturas_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void dgvFacturas_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (lstFacturas.SelectedIndex >= 0)
                lstFacturas.Items.RemoveAt(lstFacturas.SelectedIndex);
        }

        private void dgvFacturas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (!lstFacturas.Items.Contains(dgvFacturas[0, dgvFacturas.SelectedRows[0].Index].Value.ToString()))
                {
                    lstFacturas.Items.Add(dgvFacturas[0, dgvFacturas.SelectedRows[0].Index].Value.ToString());
                    dgvFacturas.Refresh();
                }
            }
        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            if ((txtArea.Text == "" || txtEntrega.Text == "") && this.esRecepcion)
            {
                MessageBox.Show("Se deben de llenar todos los campos.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (lstFacturas.Items.Count == 0)
            {
                MessageBox.Show("No existe ninguna factura recibida.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int idRecepcion = RecepcionFacturas.setAltaRecepcion(lstFacturas.Items.Cast<String>().ToList(), txtArea.Text, txtEntrega.Text, DateTime.Now, dtpFecha.Value, this.esRecepcion, Globales.UsuarioActual.Id);
            if (idRecepcion > 0)
            {
                //RecepcionFacturas.setAltaRecepcionDetalle(idRecepcion, lstFacturas.Items.Cast<String>().ToList());
                MessageBox.Show("La recepción se ha registrado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiaFormulario();
                frmRecepción_Load(null, null);
            }
            else
            {
                MessageBox.Show("La factura ya fue capturada, únicamente falta la recepción por el área de crédito.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


        }

        void LimpiaFormulario()
        {
            this.txtEntrega.Text = "";
            this.txtArea.Text = "";
            this.lstFacturas.Items.Clear();
            this.dtpFecha.Value = DateTime.Now;
        }

        void SeteaFormulario(Boolean esRecepcion)
        {
            this.txtArea.Enabled = esRecepcion;
            this.txtEntrega.Enabled = esRecepcion;
        }


    }
}
