using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIP.Reportes;
using ulp_bl;

namespace SIP
{
    public partial class frmEtiquetasEmpaque : Form
    {
        public frmEtiquetasEmpaque()
        {
            InitializeComponent();
        }

        private void txtNumeroPedido_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNumeroPedido.Text))
            {
                try
                {
                    DataTable dataTableEtiquetasEmpaque =
                        EtiquetasEmpaque.RegresaEtiquetasEmpaque(Convert.ToInt32(txtNumeroPedido.Text));

                    if (dataTableEtiquetasEmpaque.Rows.Count > 0)
                    {
                        DataRow renglonDatos = dataTableEtiquetasEmpaque.Rows[0];
                        lblRazonSocial.Text = renglonDatos["RAZON_SOCIAL"].ToString();
                        txtDireccion.Text = string.Format("{0} {1} {2} Entidad: {3} {4} {5}", renglonDatos["CALLE"], renglonDatos["COLONIA"], renglonDatos["MUNICIPIO"], renglonDatos["ESTADO"], renglonDatos["CODIGO"], renglonDatos["TELEFONO"]);
                        txtAntencion.Text = renglonDatos["ATENCION"].ToString();
                        txtRemitido.Text = renglonDatos["REMITIDO"].ToString();
                        txtConsignado.Text = renglonDatos["CONSIGNADO"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Pedido NO existe", "Verifique", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                        txtNumeroPedido.Focus();
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (lblRazonSocial.Text != "Razón Social")
            {
                frmReportes frmReportes = new frmReportes(Enumerados.TipoReporteCrystal.EtiquetasEmpaque,
                    Convert.ToInt32(txtNumeroPedido.Text), lblRazonSocial.Text, txtDireccion.Text, txtAntencion.Text,
                    txtRemitido.Text, txtConsignado.Text, txtContenido.Text);
                frmReportes.Show();
            }
            else
            {
                MessageBox.Show("Pedido NO existe", "Verifique", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                        txtNumeroPedido.Focus();
            }
        }
    }
}
