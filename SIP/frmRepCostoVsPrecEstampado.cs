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
    public partial class frmRepCostoVsPrecEstampado : Form
    {
        private Precarga precarga;
        public frmRepCostoVsPrecEstampado()
        {
            InitializeComponent();
            precarga = new Precarga(this);
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            precarga.MostrarEspera();
            BackgroundWorker backGroundWorker = new BackgroundWorker();
            backGroundWorker.DoWork +=backGroundWorker_DoWork;
            backGroundWorker.RunWorkerCompleted += backGroundWorker_RunWorkerCompleted;
            backGroundWorker.RunWorkerAsync();
        }

        void backGroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            precarga.RemoverEspera();
        }

        private void backGroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            precarga.AsignastatusProceso("Procesando Costo Vs Prec. Estampado...");


            DataTable CostoVsPrecEstampado = RepCostoVsPrecEstampado.RegresaCostoVsPrecCostura(dtpIni.Value, dtpFin.Value);
            
            string archivoTemporal = System.IO.Path.GetTempFileName().Replace(".tmp", ".xls");
            precarga.AsignastatusProceso("Creando archivo de excel...");
            RepCostoVsPrecEstampado.GeneraArchivoExcel(archivoTemporal, CostoVsPrecEstampado);
            //System.Diagnostics.Process.Start(archivoTemporal);
            FuncionalidadesFormularios.MostrarExcel(archivoTemporal);
        }

        private void frmRepExportaUpPedidos_Load(object sender, EventArgs e)
        {
            dtpIni.Value = DateTime.Now.AddDays(-1);
            dtpFin.Value = DateTime.Now;
        }

        private void dtpFin_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpIni_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
