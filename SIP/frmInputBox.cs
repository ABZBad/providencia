using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIP.Utiles;
using ulp_bl;
using ulp_bl.Reportes;

namespace SIP
{
    public partial class frmInputBox : Form
    {
        private Precarga precarga;

        Enumerados.TipoCajaTextoInputBox tipoCaja = new Enumerados.TipoCajaTextoInputBox();
        
        Boolean obligatorio;
        string referenciaInicial;
        string referenciaFinal;
        bool Switch;
        private bool ValidarEntrada;
        public frmInputBox()
        {
            InitializeComponent();
        }
        public frmInputBox(Enumerados.TipoCajaTextoInputBox tipoCajaTexto)
        {
            InitializeComponent();
            tipoCaja = tipoCajaTexto;
            obligatorio = false;
            ValidarEntrada = false;
        }

        public frmInputBox(Enumerados.TipoCajaTextoInputBox tipoCajaTexto, Boolean _obligatorio)
        {
            InitializeComponent();
            tipoCaja = tipoCajaTexto;
            this.obligatorio = _obligatorio;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ValidarEntrada = false;
            obligatorio = false;
            NTxtOrden.Text = "";
            txtOrden.Text = ""; 
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ValidarEntrada = true;
        }
        
        private void NTxtOrden_KeyPress(object sender, KeyPressEventArgs e)
        {
        
        }

        private void frmRepOrdProd_Load(object sender, EventArgs e)
        {

        }

        private void frmInputBox_Activated(object sender, EventArgs e)
        {
            if (tipoCaja == Enumerados.TipoCajaTextoInputBox.Texto)
            {
                NTxtOrden.Visible = false;
                dtpFecha.Visible = false;
                txtOrden.Focus();
            }
            if (tipoCaja == Enumerados.TipoCajaTextoInputBox.Fecha)
            {
                NTxtOrden.Visible = false;
                txtOrden.Visible = false;
                dtpFecha.Visible = true;
                dtpFecha.Focus();
            }
        }

        private void frmInputBox_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (e.CloseReason == CloseReason.UserClosing)
            {
                txtOrden.Text = "";
                NTxtOrden.Text = "";
            }
            else
            {
                TextBox txtInput;

                if (tipoCaja == Enumerados.TipoCajaTextoInputBox.Texto)
                {
                    txtInput = txtOrden;
                }
                else
                {
                    txtInput = NTxtOrden;
                }

                if (tipoCaja != Enumerados.TipoCajaTextoInputBox.Fecha)
                {
                    if (ValidarEntrada || obligatorio)
                    {
                        if (string.IsNullOrEmpty(txtInput.Text))
                        {
                            ValidarEntrada = false;
                            MessageBox.Show("El valor escrito es incorrecto o es obligatorio.", "Verifique", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                            e.Cancel = true;
                            txtInput.Focus();


                        }
                    }
                }
            }
        }
    }
}
