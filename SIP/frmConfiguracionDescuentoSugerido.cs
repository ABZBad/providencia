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
    public partial class frmConfiguracionDescuentoSugerido : Form
    {      

        public frmConfiguracionDescuentoSugerido()
        {
            InitializeComponent();
        }

        private void frmConfiguracionDescuentoSugerido_Load(object sender, EventArgs e)
        {
            this.Height = 400;
            DataTable dtConfiguracion = new DataTable();
            dtConfiguracion = ConfiguracionDescuentoSugerido.GetTablaDescuentosYPreciosSugeridos();
            if (dtConfiguracion != null)
            {
                dgvDescuentos.DataSource = dtConfiguracion;
                dgvDescuentos.Refresh();
                dgvDescuentos.Columns["Porcentaje"].ReadOnly = false;
                dgvDescuentos.Columns["Precio"].ReadOnly = false;
            }
            else
            {
                MessageBox.Show("Error al obtener la tabla de Politica de Descuentos.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDescuentos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            switch (dgvDescuentos.Columns[e.ColumnIndex].Name)
            {
                case "Porcentaje":
                    ConfiguracionDescuentoSugerido.SetDescuentoSugerido(int.Parse(dgvDescuentos["Id",e.RowIndex].Value.ToString()),decimal.Parse(dgvDescuentos[e.ColumnIndex,e.RowIndex].Value.ToString()));
                    break;
                case "Precio":
                    ConfiguracionDescuentoSugerido.SetPrecioSugerido(int.Parse(dgvDescuentos["Id", e.RowIndex].Value.ToString()), decimal.Parse(dgvDescuentos[e.ColumnIndex, e.RowIndex].Value.ToString()));
                    break;
            }
        }

        private void dgvDescuentos_CellErrorTextChanged(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show("Tipo de dato incorrecto.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void dgvDescuentos_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Tipo de dato incorrecto.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro que desea eliminar el rango seleccionado?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                ConfiguracionDescuentoSugerido.SetBaja((int)dgvDescuentos["Id", dgvDescuentos.CurrentRow.Index].Value);
                frmConfiguracionDescuentoSugerido_Load(null, null);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            this.Height = 500;
            LimpiaAlta();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            this.Height = 400;
            LimpiaAlta();
        }

        private void LimpiaAlta()
        {
            nudMin.Value = 1;
            nudMax.Value = 200;
            txtCMP.Text = "0.00";
            txtDescuento.Text = "0.0";

        }

        private Boolean ValidaAlta()
        {
            //RANGO
            if (nudMax.Value <= nudMin.Value)
            {
                MessageBox.Show("El Rango Máximo no puede ser menor al Rango Mínimo", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //DESCUENTO
            if (txtDescuento.Text == "")
            {
                MessageBox.Show("El descuento es obligatorio", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else 
            if (double.IsNaN(double.Parse(txtDescuento.Text)))
            {
                MessageBox.Show("El descuento debe ser un valor numérico", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //PRECIO
            if (txtCMP.Text == "")
            {
                MessageBox.Show("El precio es obligatorio", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
                if (double.IsNaN(double.Parse(txtDescuento.Text)))
                {
                    MessageBox.Show("El precio debe ser un valor numérico", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            return true;
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidaAlta())
            {
                ConfiguracionDescuentoSugerido.SetAlta(nudMin.Value, nudMax.Value, decimal.Parse(txtDescuento.Text), decimal.Parse(txtCMP.Text));
                MessageBox.Show("Datos guardados de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiaAlta();
                frmConfiguracionDescuentoSugerido_Load(null, null);
                groupBox1.Visible = false;
                this.Height = 400;
            }
            
        }

    }
}
