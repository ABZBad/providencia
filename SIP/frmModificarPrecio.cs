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
    public partial class frmModificarPrecio : Form
    {
        int Pedido = 0;
        string Agrupador = "";
        public frmModificarPrecio()
        {
            InitializeComponent();
        }
        public frmModificarPrecio(int pedido, string agrupador, decimal precio)
        {
            InitializeComponent();
            txtPrecioActual.Text = precio.ToString();
            txtPrecioAnterior.Text = precio.ToString();
            txtPrecioAnterior.Enabled = false;
            Pedido = pedido;
            Agrupador = agrupador;
        }
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            PED_DET modifica_precio = new PED_DET();
            modifica_precio.AGRUPADOR = Agrupador;
            modifica_precio.PEDIDO = Pedido;
            modifica_precio.PRECIO_PROD = Convert.ToDecimal(txtPrecioActual.Text);
            modifica_precio.Modificar(modifica_precio);
            this.Close();
        }
    }
}
