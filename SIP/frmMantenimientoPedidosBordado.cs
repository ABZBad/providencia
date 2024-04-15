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
    public partial class frmMantenimientoPedidosBordado : Form
    {
        private int numeroPedido = 0;
        private DataTable dataTableDetallePedidosBordado = new DataTable();
        public frmMantenimientoPedidosBordado()
        {
            InitializeComponent();
        }

        private void frmMantenimientoPedidosBordado_Load(object sender, EventArgs e)
        {

            ConsultarPedido();


        }

        private void ConsultarPedido()
        {
            frmInputBox frmInputBox = new frmInputBox();
            frmInputBox.Text = "Mantenimiento de ordenes de trabajo";
            frmInputBox.lblTitulo.Text = "Numero de orden de trabajo, sin P";

            if (frmInputBox.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                numeroPedido = Convert.ToInt32(frmInputBox.NTxtOrden.Text);
                int resultadoBusqueda = 0;

                DataTable dataTableMantenimientoPedidosBordado = MantenimientoPedidosBordado.RegresaTablaPedidosBordado(numeroPedido, ref resultadoBusqueda);
                if (resultadoBusqueda == 0)
                {
                    //TODO: enlazar navigator                    
                    bindingSource1.DataSource = dataTableMantenimientoPedidosBordado;

                    if (txtNumeroPedido.DataBindings.Count == 0)
                    {
                        txtNumeroPedido.DataBindings.Add(new Binding("Text", bindingSource1, "PEDIDO"));
                        txtNumeroPedido.DataBindings.Add(new Binding("Tag", bindingSource1, "AGRUPADOR"));
                        txtModelo.DataBindings.Add(new Binding("Text", bindingSource1, "MODELO"));
                        txtPrendas.DataBindings.Add(new Binding("Text", bindingSource1, "CANTIDAD"));
                    }
                    LlenaDgvDetalle();

                }
                else if (resultadoBusqueda == 1)
                {
                    MessageBox.Show("No se ha encontrado número de orden");
                    Close();
                }
            }
        }
        private void bindingSource1_PositionChanged(object sender, EventArgs e)
        {
            LlenaDgvDetalle();
        }

        private void LlenaDgvDetalle()
        {

            if (!string.IsNullOrEmpty(txtNumeroPedido.Text))
            {
                string agrupador = txtNumeroPedido.Tag.ToString();
                dataTableDetallePedidosBordado = MantenimientoPedidosBordado.RegresaTablaPedidosBordadoDetalle(
                        Convert.ToInt32(txtNumeroPedido.Text), agrupador);
                dataTableDetallePedidosBordado.ColumnChanged+=dataTableDetallePedidosBordado_ColumnChanged;
                if (dataTableDetallePedidosBordado.Rows.Count > 0)
                {
                    dgvDetalle.DataSource = dataTableDetallePedidosBordado;
                }
                else
                {
                    if (dgvDetalle.DataSource != null)
                    {
                        ((DataTable) dgvDetalle.DataSource).Rows.Clear();
                    }
                }


            }
        }

        void dataTableDetallePedidosBordado_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            if (("CMT_MAQUILERO|CMT_FACT_MAQUILA").Contains(e.Column.ColumnName))
            {
                MantenimientoPedidosBordado.GuardaCampoValorCMT_DET(Convert.ToInt32(txtNumeroPedido.Text),
                    e.Column.ColumnName, e.ProposedValue, e.Column.DataType, Convert.ToInt32(e.Row["CMT_INDX"]));
            }            
        }

        private void frmMantenimientoPedidosBordado_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult resp = MessageBox.Show("¿Deseas capturar otro Pedido?", "Confirme", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resp == DialogResult.Yes)
            {
                e.Cancel = true;
                ConsultarPedido();
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
