using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ulp_bl;

namespace SIP
{
    public class frmRegenerarCarteraViva
    {
        private frmEspera frmEspera = new frmEspera();
        private BackgroundWorker bgw = new BackgroundWorker();
        private int Annio = 0;
        private Exception Ex;
        public frmRegenerarCarteraViva()
        {
            bgw.DoWork += bgw_DoWork;
            bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
        }

        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            frmEspera.Close();
            if (Ex != null)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("Proceso concluido exitosamente.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            RegenerarCarteraViva.RegenerarCartera(Annio, ref Ex);
        }
        public void Show()
        {
            frmInputBox frmInputBox = new frmInputBox(ulp_bl.Enumerados.TipoCajaTextoInputBox.Numerica);
            frmInputBox.lblTitulo.Text = "Año a reconstruir";
            frmInputBox.Text = "Regeneración de Cartera";
            if (frmInputBox.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    Annio = int.Parse(frmInputBox.NTxtOrden.Text);
                    frmEspera.Show();
                    bgw.RunWorkerAsync();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message,Ex.Source,MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                }
            }
            
        }
    }
}
