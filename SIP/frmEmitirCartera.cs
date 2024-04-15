using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIP.Utiles;
using ulp_bl.Reportes;

namespace SIP
{
    public partial class frmEmitirCartera : Form
    {
        private BackgroundWorker backGroundWorker;
        private Precarga precarga;
        private Queue<Control> colaCajasTexto = new Queue<Control>();
        public frmEmitirCartera()
        {
            InitializeComponent();
            precarga = new Precarga(this);
        }

        private void btnEmitir_Click(object sender, EventArgs e)
        {
            colaCajasTexto.Clear();

            if (CamposRequeridos())
            {
                precarga.MostrarEspera();
                backGroundWorker = new BackgroundWorker();
                backGroundWorker.DoWork += backGroundWorker_DoWork;
                backGroundWorker.RunWorkerCompleted += backGroundWorker_RunWorkerCompleted;
                backGroundWorker.RunWorkerAsync();
            }
            else
            {
                TextBox txtBox = (TextBox) colaCajasTexto.Dequeue();
                txtBox.Focus();
            }



        }

        private bool CamposRequeridos()
        {
            int errores = 0;
            errorProvider1.Clear();
            if (string.IsNullOrEmpty(txtAniosAEmitir.Text))
            {
                errorProvider1.SetError(txtAniosAEmitir, "El campo es requerido");
                colaCajasTexto.Enqueue(txtAniosAEmitir);
                errores++;
            }            
            if (string.IsNullOrEmpty(txtAniosAtras.Text))
            {
                errorProvider1.SetError(txtAniosAtras, "El campo es requerido");
                colaCajasTexto.Enqueue(txtAniosAtras);
                errores++;
            }
            if (string.IsNullOrEmpty(txtMesesMC.Text))
            {
                errorProvider1.SetError(txtMesesMC, "El campo es requerido");
                colaCajasTexto.Enqueue(txtMesesMC);
                errores++;
            }
            if (string.IsNullOrEmpty(txtAgente.Text))
            {
                errorProvider1.SetError(txtAgente, "El campo es requerido");
                colaCajasTexto.Enqueue(txtAgente);
                errores++;
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

        private void backGroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Quitar progressbar
            precarga.RemoverEspera();
            backGroundWorker.Dispose();
        }

        private void backGroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            EmisionCartera emisionCartera = new EmisionCartera();

            System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

            precarga.AsignastatusProceso("Procesando cartera...");
            DataTable dataEmisionCartera = EmisionCartera.RegresaEmisionCartera(
                                                                             int.Parse(txtAniosAEmitir.Text),
                                                                             int.Parse(txtAniosAtras.Text),
                                                                             int.Parse(txtMesesMC.Text),
                                                                             txtAgente.Text
                                                                         );
            precarga.AsignastatusProceso("Creando archivo de excel...");

            string archivoTemporal = System.IO.Path.GetTempFileName().Replace(".tmp", ".xls");

            EmisionCartera.GeneraArchivoExcel(archivoTemporal, dataEmisionCartera, txtAgente.Text);
            
            //System.Diagnostics.Process.Start(archivoTemporal);
            FuncionalidadesFormularios.MostrarExcel(archivoTemporal);

        }
    }
}
