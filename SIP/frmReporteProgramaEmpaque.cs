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
    public partial class frmReporteProgramaEmpaque : Form
    {
        #region "Atributos y Constructores"
        private BackgroundWorker bgw;
        private Precarga precarga;

        public frmReporteProgramaEmpaque()
        {
            InitializeComponent();
            precarga = new Precarga(this);      
        }
        #endregion        
        #region "Eventos"
        private void cmdContinuar_Click(object sender, EventArgs e)
        {
            bgw = new BackgroundWorker();
            bgw.DoWork += bgw_DoWork;
            bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
            precarga.MostrarEspera();
            bgw.RunWorkerAsync();
        }
        #endregion
        #region "Workers"
        private void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            precarga.AsignastatusProceso("Generando información...");
            RepProgramaEmpaque frmReporte = new RepProgramaEmpaque();
            DataTable dataTable = frmReporte.RegresaReporte(dtpFecha.Value);
            if (dataTable.Rows.Count > 0)
            {
                precarga.AsignastatusProceso("Generando archivo de Excel...");
                string archivoTemporal = System.IO.Path.GetTempFileName().Replace(".tmp", ".xls");
                RepProgramaEmpaque.GeneraArchivoExcel(archivoTemporal, dataTable, dtpFecha.Value);
                //System.Diagnostics.Process.Start(archivoTemporal);
                FuncionalidadesFormularios.MostrarExcel(archivoTemporal);
            }
            else
            {
                MessageBox.Show("No existen registros en la fecha indicada.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            precarga.RemoverEspera();
        }
        #endregion
    }
}
