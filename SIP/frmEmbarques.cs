using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIP.UserControls;
using ulp_bl;

namespace SIP
{
    public partial class frmEmbarques : Form
    {
        private bool noPreguntarSalida = false;
        private int numeroPedido = 0;
        public frmEmbarques()
        {
            InitializeComponent();
        }

        private void frmEmbarques_Load(object sender, EventArgs e)
        {
            Show();
            SolictaNumeroPedido();
        }

        private void SolictaNumeroPedido()
        {
            frmInputBox frmInputBox = new frmInputBox();
            frmInputBox.Text = "Embarques";
            frmInputBox.lblTitulo.Text = "Número de pedido";
            DialogResult resp = frmInputBox.ShowDialog();
            if (resp == System.Windows.Forms.DialogResult.OK)
            {
                numeroPedido = Convert.ToInt32(frmInputBox.NTxtOrden.Text);
                UPPEDIDOS upPedido = Embarques.RegresaUpPedido(numeroPedido);

                if (upPedido != null)
                {
                    if (upPedido.PEDIDO != 0)
                    {
                        btnGuardar.Enabled = true;
                        txtNumeroPedido.Text = upPedido.PEDIDO.ToString("0");
                        txtFechaPedido.Text = (upPedido.F_CAPT == null ? "" : upPedido.F_CAPT.ToString());
                        txtCodCliente.Text = upPedido.COD_CLIENTE;
                        txtGuia.Text = upPedido.GUIA;
                        txtCajas.Text = upPedido.CAJAS.ToString();
                        txtDepartamento.Text = upPedido.DEPARTAMENTO;
                        txtChofer.Text = upPedido.CHOFER;
                        txtDestino.Text = upPedido.DESTINO;
                        txtEstatus.Text = upPedido.ESTATUS;
                        txtTransporte.Text = upPedido.TRANSPORTE;
                        txtFechaRuta.Text = (upPedido.FECHARUTA == null
                            ? ""
                            : Convert.ToDateTime(upPedido.FECHARUTA.ToString()).ToShortDateString());
                        txtObservaciones.Text = upPedido.OBSERVACIONES;
                        txtGuia.Focus();
                    }
                }
                else
                {
                    MessageBox.Show(
                        string.Format("El pedido {0} no existe por favor verifique y vuelva a intentarlo.", numeroPedido),
                        "Verifique", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    btnGuardar.Enabled = false;
                }


            }
            else
            {
                DialogResult resp2 = MessageBox.Show("¿ Desea salir de esta pantalla ?", "Confirme",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resp2 == System.Windows.Forms.DialogResult.Yes)
                {
                    noPreguntarSalida = true;
                    this.Close();
                }
                else
                {
                    SolictaNumeroPedido();
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            DialogResult resp = MessageBox.Show("¿Confirma guardar los cambios?","Confirme",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            int cajas = 0;
            if (!string.IsNullOrEmpty(txtCajas.Text))
            {
                cajas = Convert.ToInt32(txtCajas.Text);
            }

            if (resp == DialogResult.Yes)
            {
                try
                {
                    UPPEDIDOS upPedido = new UPPEDIDOS();

                    upPedido.PEDIDO = Convert.ToDouble(txtNumeroPedido.Text);
                    upPedido.GUIA = txtGuia.Text;
                    upPedido.CAJAS = cajas;
                    upPedido.CHOFER = txtChofer.Text;
                    upPedido.DEPARTAMENTO = txtDepartamento.Text;
                    upPedido.DESTINO = txtDestino.Text;
                    upPedido.ESTATUS = txtEstatus.Text;
                    upPedido.TRANSPORTE = txtTransporte.Text;
                    if (!string.IsNullOrEmpty(txtFechaRuta.Text))
                    {
                        upPedido.FECHARUTA = Convert.ToDateTime(txtFechaRuta.Text);
                    }
                    upPedido.OBSERVACIONES = txtObservaciones.Text;

                    string resultado = Embarques.ModificarPedido(upPedido);

                    if (resultado == string.Empty)
                    {
                        DialogResult resp3 = MessageBox.Show("¿Deseas capturar otro Pedido?", "Confirme",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (resp3 == DialogResult.Yes)
                        {
                            LimpiaCajasDeTexto();
                            SolictaNumeroPedido();
                        }
                        else
                        {
                            noPreguntarSalida = true;
                            Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show(resultado, "Errores", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
        }

        private void frmEmbarques_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!noPreguntarSalida)
            {
                DialogResult resp =
                    MessageBox.Show("Los datos de Embarque no se han guardado. ¿Deseas cancelar su captura?", "Confirme",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resp == DialogResult.Yes)
                {
                    DialogResult resp2 = MessageBox.Show("¿Deseas capturar otro Pedido?", "Confirme",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (resp2 == DialogResult.Yes)
                    {
                        LimpiaCajasDeTexto();
                        SolictaNumeroPedido();
                        e.Cancel = true;
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }

        }

        private void LimpiaCajasDeTexto()
        {
            foreach (Control control in this.groupBox1.Controls)
            {
                if (control is TextBoxEx)
                {
                    TextBoxEx txtCtrl = (TextBoxEx) control;
                    txtCtrl.Text = "";
                }
            }
            foreach (Control control in this.groupBox2.Controls)
            {
                if (control is TextBoxEx)
                {
                    TextBoxEx txtCtrl = (TextBoxEx)control;
                    txtCtrl.Text = "";
                }
            }
        }
    }
}
