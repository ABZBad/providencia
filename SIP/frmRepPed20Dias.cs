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
    public partial class frmRepPed20Dias : Form
    {
        private Precarga precarga;
        public frmRepPed20Dias()
        {
            InitializeComponent();
            precarga = new Precarga(this);
        }

        private void cmdContinuar_Click(object sender, EventArgs e)
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

        private void backGroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            precarga.AsignastatusProceso("Procesando pedidos...");
            RepPed20Dias VentasPrendas = new RepPed20Dias();
            DataTable dataTable = VentasPrendas.RegresaTabla(dtpFecha.Value);
            if (dataTable.Rows.Count >0)
            {
            precarga.AsignastatusProceso("Creando archivo de excel...");
            string archivoTemporal = System.IO.Path.GetTempFileName().Replace(".tmp", ".xls");
            RepPed20Dias.GeneraArchivoExcel(archivoTemporal, dataTable, dtpFecha.Value);
            //System.Diagnostics.Process.Start(archivoTemporal);
            FuncionalidadesFormularios.MostrarExcel(archivoTemporal);
            }
            else
            {
                MessageBox.Show("No existen registros en la fecha indicada.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
    }
}
