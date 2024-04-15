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
    public partial class frmRepVentPesosPrendas : Form
    {
        private Exception ex;
        private Precarga precarga;
        public frmRepVentPesosPrendas()
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

        private void backGroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            precarga.AsignastatusProceso("Procesando pedidos...");
            RepVentPesosPrendas VentasPrendas = new RepVentPesosPrendas();
            
            DataTable dataTable = VentasPrendas.RegresaTablaPedidos(dtpIni.Value, dtpFin.Value, chkVtasEnRango.Checked, ref ex);
            if (ex != null)
            { MessageBox.Show(ex.Message); }

            if (dataTable.Rows.Count >0)
            {
                precarga.AsignastatusProceso("Creando archivo de excel...");
                string archivoTemporal = System.IO.Path.GetTempFileName().Replace(".tmp", ".xls");
                RepVentPesosPrendas.GeneraArchivoExcel(archivoTemporal, dataTable, dtpIni.Value, dtpFin.Value);
                //System.Diagnostics.Process.Start(archivoTemporal);
                FuncionalidadesFormularios.MostrarExcel(archivoTemporal);
            }
            else
            {
                MessageBox.Show("No existen registros con los rangos de fecha ingresados.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }

        }

        private void frmRepVentPesosPrendas_Load(object sender, EventArgs e)
        {
            dtpIni.Value = DateTime.Today.AddDays(-1);
            dtpFin.Value = DateTime.Today;
        }
    }
}
