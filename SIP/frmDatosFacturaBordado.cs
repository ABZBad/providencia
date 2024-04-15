using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIP.Reportes;
using SIP.Utiles;
using ulp_bl;
using ulp_bl.Reportes;

namespace SIP
{
    public partial class frmDatosFacturaBordado : Form
    {
        private ErrorProvider errorProviderMaquilero = new ErrorProvider();
        private ErrorProvider errorProviderFactura = new ErrorProvider();
        private Queue _pilaErrores = new Queue();        
        private Precarga precarga;
        public frmDatosFacturaBordado()
        {
            InitializeComponent();
            errorProviderFactura.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProviderMaquilero.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            precarga = new Precarga(this);
        }

        private void btnEmitir_Click(object sender, EventArgs e)
        {
            if (DatosValidos())
            {
                bool existenDatos = RepDatosFacturaBordado.ExistenRegistrosMaquiladorVsFactura(Convert.ToInt32(txtMaquilero.Text),txtFactura.Text);
                if (existenDatos)
                {
                    precarga.MostrarEspera();
                    precarga.AsignastatusProceso("Procesando ...");
                    PROV01 prov01 = new PROV01();
                    prov01 = prov01.Consultar(Convert.ToInt32(txtMaquilero.Text));
                    string etiqueta = string.Format("Factura {0}, Maquilador {1} {2}", txtFactura.Text,
                        txtMaquilero.Text, prov01.NOMBRE);
                    frmReportes reporte = new frmReportes(ulp_bl.Enumerados.TipoReporteCrystal.DatosFacturaBordado, Convert.ToInt32(txtMaquilero.Text), txtFactura.Text, etiqueta);
                    reporte.Show();
                    precarga.RemoverEspera();
                }
                else
                {
                    MessageBox.Show(string.Format("No se han encontrado registros de la factura \"{0}\" para el maquilero \"{1}\".",txtFactura.Text,txtMaquilero.Text),"Verifique",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                //MessageBox.Show("Complete los campos requeridos","",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                TextBox txtConError = (TextBox)_pilaErrores.Dequeue();
                txtConError.Focus();
            }
        }

       

        private bool DatosValidos()
        {
            errorProviderMaquilero.Clear();
            errorProviderFactura.Clear();
            _pilaErrores.Clear();
            if (string.IsNullOrEmpty(txtMaquilero.Text))
            {
                errorProviderMaquilero.SetError(txtMaquilero, "La clave del Maquilero es un dato requerido");
                _pilaErrores.Enqueue(txtMaquilero);
            }
            if (string.IsNullOrEmpty(txtFactura.Text))
            {
                errorProviderMaquilero.SetError(txtFactura, "La Factura es un dato requerido");
                _pilaErrores.Enqueue(txtFactura);
            }
            if (_pilaErrores.Count > 0)
                return false;
            else
                return true;
        }
    }
}
