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
    public partial class frmCorporativos : Form
    {
        ulp_bl.CLIE01 bl_Clientes;


        public frmCorporativos()
        {
            InitializeComponent();
            dgvClientesCorporativo.AutoGenerateColumns = false;
        }

        private void frmCorporativos_Load(object sender, EventArgs e)
        {
            LoadClientes();
            LoadCorporativos();
            

        }

        private void btnAgregarCorporativo_Click(object sender, EventArgs e)
        {
            frmInputBox f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto);
            f.lblTitulo.Text = "Introduce el nombre del Corporativo: ";
            f.Text = "Nuevo Corporativo";
            f.ShowDialog();
            if (f.txtOrden.Text.Trim() != "")
            {
                if (ulp_bl.GestionCorporativos.setAltaCorporativo(f.txtOrden.Text.Trim()))
                {
                    LoadCorporativos();
                    MessageBox.Show("El corporativo se dió de alta de forma correcta.","SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("El corporativo no se pudo dar de alta, favor de revisarlo con el administrador del Sistema.","SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void LoadClientes()
        {
            //GET CLIENTES
            bl_Clientes = new CLIE01();
            cmbClientes.DisplayMember = "CLAVENOMBRE";
            cmbClientes.ValueMember = "CLAVE";
            cmbClientes.DataSource = bl_Clientes.ConsultarTodos();
        }
        private void LoadCorporativos()
        {
            //GET CORPORATIVOS            
            cmbCorporativos.DisplayMember = "Nombre";
            cmbCorporativos.ValueMember = "Clave";
            cmbCorporativos.DataSource = ulp_bl.GestionCorporativos.GetCorporativos();
        }

        private void cmbCorporativos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCorporativos.SelectedValue != null)
            {
                dgvClientesCorporativo.DataSource =ulp_bl.GestionCorporativos.GetClientesCorporativos(cmbCorporativos.SelectedValue.ToString());
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cmbClientes.SelectedValue!=null && cmbCorporativos.SelectedValue!=null)
            {
                if (ulp_bl.GestionCorporativos.setAltaClientesCorporativos(cmbClientes.SelectedValue.ToString(), int.Parse(cmbCorporativos.SelectedValue.ToString())))
                    cmbCorporativos_SelectedIndexChanged(null, null);
            }
        }

        private void btnEliminaCorporativo_Click(object sender, EventArgs e)
        {
            if (cmbCorporativos.SelectedValue!=null)
                if (MessageBox.Show("¿Eliminar por completo el Corporativo: " + cmbCorporativos.Text + "?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    ulp_bl.GestionCorporativos.setBajaCorporativo(int.Parse(cmbCorporativos.SelectedValue.ToString()));
                    cmbCorporativos.Text = "";
                    dgvClientesCorporativo.DataSource = null;
                    LoadCorporativos();
                    cmbCorporativos_SelectedIndexChanged(null, null);
                }
        }

        private void dgvClientesCorporativo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvClientesCorporativo.CurrentRow.Cells[e.ColumnIndex].Value.ToString() == "Eliminar")
            {
                ulp_bl.GestionCorporativos.setBajaClientesCorporativos(dgvClientesCorporativo.CurrentRow.Cells["Clave"].Value.ToString(), int.Parse(cmbCorporativos.SelectedValue.ToString()));
                cmbCorporativos_SelectedIndexChanged(null, null);
            }
        }
    }
}
