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
    public partial class frmPreciosEspecialesCorporativo : Form
    {
        public frmPreciosEspecialesCorporativo()
        {
            InitializeComponent();
            dgvArticulosCorporativo.AutoGenerateColumns = false;
        }

        private void frmPreciosEspecialesCorporativo_Load(object sender, EventArgs e)
        {
            LoadCorporativos();
            LoadArticulos();
        }

        private void LoadCorporativos()
        {
            //GET CORPORATIVOS            
            cmbCorporativos.DisplayMember = "Nombre";
            cmbCorporativos.ValueMember = "Clave";
            cmbCorporativos.DataSource = ulp_bl.GestionCorporativos.GetCorporativos();
        }
        private void LoadArticulos()
        {
            //GET CORPORATIVOS            
            cmbArticulos.DisplayMember = "Descripcion";
            cmbArticulos.ValueMember = "Clave";
            cmbArticulos.DataSource = ulp_bl.GestionCorporativos.GetArticulos();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmInputBox f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Numerica);
            f.NTxtOrden.NumberType = UserControls.TipoDeNumero.Decimal;
            f.lblTitulo.Text = "Introduce el Precio del artículo: ";
            f.Text = "Nuevo Corporativo";
            f.ShowDialog();
            if (f.NTxtOrden.Text.Trim() != "")
            {
                if (cmbCorporativos.SelectedValue!=null && cmbArticulos.SelectedValue!=null)
                    if (ulp_bl.GestionCorporativos.setAltaArticuloCorporativos(int.Parse(cmbCorporativos.SelectedValue.ToString()),cmbArticulos.SelectedValue.ToString(), cmbArticulos.Text,decimal.Parse(f.NTxtOrden.Text)))
                        cmbCorporativos_SelectedIndexChanged(null, null);
                
                
            }
        }

        private void cmbCorporativos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCorporativos.SelectedValue != null)
            {
                dgvArticulosCorporativo.DataSource = ulp_bl.GestionCorporativos.GetArticulosCorporativos(cmbCorporativos.SelectedValue.ToString());
            }
        }

        private void dgvArticulosCorporativo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvArticulosCorporativo.CurrentRow.Cells[e.ColumnIndex].Value.ToString() == "Eliminar")
            {
                ulp_bl.GestionCorporativos.setBajaArticuloCorporativos(dgvArticulosCorporativo.CurrentRow.Cells["Articulo"].Value.ToString(), int.Parse(cmbCorporativos.SelectedValue.ToString()));
                cmbCorporativos_SelectedIndexChanged(null, null);
            }
        }
    }
}
