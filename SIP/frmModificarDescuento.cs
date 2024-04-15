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
    public partial class frmModificarDescuento : Form
    {
        int Pedido = 0;
        string Agrupador = "";
        decimal precio = 0;
        decimal precioLista = 0;
        public frmModificarDescuento()
        {
            InitializeComponent();
        }
        public frmModificarDescuento(int pedido, string agrupador, decimal descuento, decimal precio, decimal precioLista)
        {
            InitializeComponent();
            txtPrecioActual.Text = (descuento * 100).ToString();
            txtPrecioAnterior.Text = (descuento * 100).ToString();
            txtPrecioAnterior.Enabled = false;
            Pedido = pedido;
            Agrupador = agrupador;
            this.precio = precio;
            this.precioLista = precioLista;
        }
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            PED_DET modifica_precio = new PED_DET();
            modifica_precio.AGRUPADOR = Agrupador;
            modifica_precio.PEDIDO = Pedido;
            modifica_precio.DESCUENTO = Convert.ToDouble(txtPrecioActual.Text) / 100;
            modifica_precio.PRECIO_PROD = Convert.ToDecimal(this.precioLista * (1 - Convert.ToDecimal(modifica_precio.DESCUENTO)));

            modifica_precio.Modificar(modifica_precio);
            this.Close();
        }
    }
}
