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

namespace SIP
{
    public partial class frmRepStaff : Form
    {
        private BackgroundWorker bgw;
        private Precarga precarga;
        private Exception ex;
        public frmRepStaff()
        {
            InitializeComponent();
            precarga = new Precarga(this);
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            bgw = new BackgroundWorker();
            bgw.DoWork += bgw_DoWork;
            bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
            precarga.MostrarEspera();
            bgw.RunWorkerAsync();
        }

        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {   
            precarga.RemoverEspera();
            if (ex != null)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ex = null;
            }
        }

        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {

            precarga.AsignastatusProceso("Procesando información...");
            DataSet dsRepStaff = RepStaff.RegresaDatosReporteStaff(dtFechaDesde.Value, dtFechaHasta.Value, ref ex);
            if (ex == null)
            {
                if (dsRepStaff != null)
                {
                    precarga.AsignastatusProceso("Generando archivo de Excel...");
                    //generar excel
                    string archivoTemporal = System.IO.Path.GetTempFileName().Replace(".tmp", ".xls");
                    RepStaff.GeneraArchivoExcel(archivoTemporal, dsRepStaff, dtFechaDesde.Value, dtFechaHasta.Value);

                    //System.Diagnostics.Process.Start(archivoTemporal);
                    FuncionalidadesFormularios.MostrarExcel(archivoTemporal);
                }
                else
                {
                    MessageBox.Show("No existen registros con este rango de fechas", "", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                }
            }
        }

        private void frmRepStaff_Load(object sender, EventArgs e)
        {
            dtFechaDesde.Value = DateTime.Now.AddDays(-1);
            dtFechaHasta.Value = DateTime.Now;
        }
    }
}
