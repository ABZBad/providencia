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
    public partial class frmTransferenciaXPedido : Form
    {
        enum TipoClick
        {
            Consultar,
            Cancelar
        }
        TipoClick tipo = new TipoClick();
        DataTable datos=new DataTable();
        public frmTransferenciaXPedido()
        {
            InitializeComponent();
        }

        private void frmTransferenciaXPedido_Load(object sender, EventArgs e)
        {
            
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (tipo== TipoClick.Consultar)
            {                
                datos = TransferenciaPorPedido.DevuelveDatosConsulta(Convert.ToInt32(txtPedido.Text), Convert.ToInt32(txtAlmOrigen.Text), Convert.ToInt32(txtAlmDestino.Text));
                if (datos.Columns.Count > 1)
                {
                    lblCliente.Text = datos.Rows[0][0].ToString();
                    datos.Columns.Remove("CLIENTE");
                    datos.Columns.Remove("PXS");
                    dgViewDetalle.DataSource = datos;
                    btnProcesar.Enabled = AplicaFormatos();
                }
                else
                {
                    lblStatus.Text = datos.Rows[0][0].ToString();
                }
                btnConsultar.Text = "Cancelar";
                txtPedido.Enabled = false;                
                tipo = TipoClick.Cancelar;
            }
            else
            {
                LimpiaInfo();
                txtPedido.Focus();
            }
        }
        private bool AplicaFormatos()
        {
            bool habilitaProcesar=true;
            foreach (DataGridViewRow row in dgViewDetalle.Rows)
            {
                if (dgViewDetalle.Rows[row.Index].Cells[6].Value.ToString() == "Exist. Insuficientes")
                {
                    habilitaProcesar = false;
                    foreach (DataGridViewCell cell in dgViewDetalle.Rows[row.Index].Cells)
                    {
                        cell.Style.ForeColor = Color.Red;
                    }
                }
            }
            return habilitaProcesar;
        }
        private void LimpiaInfo()
        {            
            datos.Rows.Clear();
            dgViewDetalle.DataSource = datos;
//            dgViewDetalle.Refresh();
            lblStatus.Text = "";
            lblCliente.Text = "";
            btnConsultar.Text = "Consultar";
            txtPedido.Enabled = true;            
            btnProcesar.Enabled = false;
            tipo = TipoClick.Consultar;
        }

        private void txtPedido_TextChanged(object sender, EventArgs e)
        {
            LimpiaInfo();
        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            DataTable resultado = new DataTable();
            this.Cursor = Cursors.WaitCursor;
            resultado = TransferenciaPorPedido.Procesar(Convert.ToInt32(txtPedido.Text), Convert.ToInt32(txtAlmOrigen.Text), Convert.ToInt32(txtAlmDestino.Text));
            this.Cursor = Cursors.Default;
            if (resultado.Rows[0][0].ToString()=="OK")
            {
                Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.TransferenciaXModelo, Convert.ToInt32(resultado.Rows[0]["agrupador"].ToString()));
                frmReportes.Show();
                MessageBox.Show("La Transferencia se ha realizado exitosamente.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiaInfo();
            }
            else
            {
                if (resultado.Columns.Count>0)
                {
                    MessageBox.Show(string.Format("Existencias Insuficientes para surtir el pedido:\n\n -{0} {1} Exist. {2} Soli. {3}",
                        resultado.Rows[0]["CVE_ART"].ToString(), resultado.Rows[0]["ORIGEN"].ToString(),
                        resultado.Rows[0]["EXIST"].ToString(), resultado.Rows[0]["PXS"].ToString()), "SIP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(resultado.Rows[0][0].ToString(), "SIP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void txtPedido_Leave(object sender, EventArgs e)
        {
            if (txtPedido.Text != string.Empty)
            {
                datos = new DataTable();
                datos = TransferenciaPorPedido.DevuelveDatosConsulta(Convert.ToInt32(txtPedido.Text),
                    Convert.ToInt32(txtAlmOrigen.Text), Convert.ToInt32(txtAlmDestino.Text));
                if (datos.Columns.Count == 1)
                {
                    lblStatus.Text = datos.Rows[0][0].ToString();
                }
            }
            else
            {
                LimpiaInfo();
            }
        }
    }
}
