using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIP.UserControls;
using SIP.Utiles;
using ulp_bl;

namespace SIP
{
    public partial class frmAltaDeptos : Form
    {
        private ErrorProvider errorProviderNombre = new ErrorProvider();
        private ErrorProvider errorProviderDesc = new ErrorProvider();
        private ErrorProvider errorProviderDepto = new ErrorProvider();
        private Queue colaControles = new Queue();
        private BackgroundWorker bgw = new BackgroundWorker();
        private Precarga precarga;
        public frmAltaDeptos()
        {
            InitializeComponent();
            precarga = new Precarga(this);
            bgw.DoWork += bgw_DoWork;
            bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
            errorProviderNombre.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProviderDesc.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProviderDepto.BlinkStyle = ErrorBlinkStyle.NeverBlink;
        }

        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            precarga.RemoverEspera();
            txtNombre.Text = "";
            txtDepartamento.Text = "";
            txtDescripcion.Text = "";
            MessageBox.Show("El departamento se dió de alta de manera satisfactoria", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            AltaDeptos.CreaDepartamento(txtNombre.Text, txtDescripcion.Text, txtDepartamento.Text);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {

            if (CamposValidos())
            {
                DialogResult resp = MessageBox.Show("¿ Confirma dar de alta el departamento ?","Confirme",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (resp == System.Windows.Forms.DialogResult.Yes)
                {
                    try
                    {
                        precarga.MostrarEspera();
                        precarga.AsignastatusProceso("Grabando información...");
                        bgw.RunWorkerAsync();
                    }
                    catch (Exception Ex)
                    {
                        precarga.RemoverEspera();
                        MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }                

                
            }
            else
            {                
                TextBoxEx txtCtrl = (TextBoxEx) colaControles.Dequeue();
                txtCtrl.Focus();
            }

        }
        private bool CamposValidos()
        {
            errorProviderDepto.Clear();
            errorProviderDesc.Clear();
            errorProviderNombre.Clear();
            colaControles.Clear();
            int errores = 0;
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                errorProviderNombre.SetError(txtNombre, "Campo requerido");
                errores++;
                colaControles.Enqueue(txtNombre);
            }
            if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                errorProviderNombre.SetError(txtDescripcion, "Campo requerido");
                errores++;
                colaControles.Enqueue(txtDescripcion);
            }
            if (string.IsNullOrEmpty(txtDepartamento.Text))
            {
                errorProviderNombre.SetError(txtDepartamento, "Campo requerido");
                errores++;
                colaControles.Enqueue(txtDepartamento);
            }
            if (errores > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
