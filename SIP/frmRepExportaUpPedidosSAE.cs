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
    public partial class frmRepExportaUpPedidosSAE : Form
    {
        private Precarga precarga;
        public frmRepExportaUpPedidosSAE()
        {
            InitializeComponent();
            precarga = new Precarga(this);
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            precarga.MostrarEspera();
            BackgroundWorker backGroundWorker = new BackgroundWorker();

            backGroundWorker.DoWork += backGroundWorker_DoWork;
            backGroundWorker.RunWorkerCompleted += backGroundWorker_RunWorkerCompleted;
            backGroundWorker.RunWorkerAsync();


        }

        void backGroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            precarga.RemoverEspera();
        }

        void backGroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            
            precarga.AsignastatusProceso("Procesando pedidos...");
            vw_UpPedidosSae vw_upPedidosSae = new vw_UpPedidosSae();
            DataTable dataTablePedidosSae = vw_upPedidosSae.RegresaTablaPedidos(chkSinFechaSurtido.Checked, chkSinFechaEntregado.Checked, dtpIni.Value, dtpFin.Value);
            precarga.AsignastatusProceso("Creando archivo de excel...");
            string archivoTemporal = System.IO.Path.GetTempFileName().Replace(".tmp", ".xls");
            vw_UpPedidosSae.GeneraArchivoExcel(archivoTemporal, dataTablePedidosSae);
            //System.Diagnostics.Process.Start(archivoTemporal);
            FuncionalidadesFormularios.MostrarExcel(archivoTemporal);
        }
    }
}
