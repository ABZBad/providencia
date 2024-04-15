using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ulp_bl;

namespace SIP
{
    public partial class frmEliminarHabilitarPedidos : Form
    {
        int pedidoNuevo = 0;
        string cliente = "";

        public frmEliminarHabilitarPedidos()
        {
            InitializeComponent();
        }

        private void frmEliminarHabilitarPedidos_Load(object sender, EventArgs e)
        {
            txtNumeroPedido.Select();
            txtNumeroPedido.Focus();
            this.ActiveControl = txtNumeroPedido;
        }

        private void txtNumeroPedido_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // PROCESAMOS LA CANCELACIÓN
                if (CancelaPedido(int.Parse(txtNumeroPedido.Text.Trim())))
                {
                    this.Close();
                }
            }
        }

        private Boolean CancelaPedido(int pedido)
        {
            string resp = "";
            resp = EliminarHabilitarPedidoAspelSaeSip.Ejecutar(pedido, true, rbVirtual.Checked);
            if (resp == "")
            {
                if (rbVirtual.Checked)
                {
                    // creamos una copia del pedido original
                    CreaPedido(pedido);
                    UPPEDIDOS guardaUppedidos = new UPPEDIDOS();
                    guardaUppedidos.PEDIDO = this.pedidoNuevo;
                    guardaUppedidos.COD_CLIENTE = this.cliente;
                    guardaUppedidos.F_CAPT = DateTime.Now;
                    guardaUppedidos.Crear(guardaUppedidos);
                    MessageBox.Show("Se ha creado el nuevo pedido virtual para que pueda ser modificado: " + this.pedidoNuevo.ToString(), "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("El pedido ha sido liberado exitosamente.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return true;
            }
            else
            {
                MessageBox.Show(resp, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void txtNumeroPedido_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            if (CancelaPedido(int.Parse(txtNumeroPedido.Text.Trim())))
            {
                this.Close();
            }
        }

        private void CreaPedido(int pedidoOrigen)
        {
            EliminarHabilitarPedidoAspelSaeSip.CrearPedidoCopia(pedidoOrigen, ref this.pedidoNuevo, ref this.cliente);
        }

        private void rbVirtual_CheckedChanged(object sender, EventArgs e)
        {
            if (rbVirtual.Checked)
            {
                MessageBox.Show("El sistema generará de forma automática un nuevo pedido virtual para las aprobaciones y surtimiento del mismo.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
