using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ulp_bl;
using SIP.Utiles;
using System.IO;

namespace SIP
{
    public partial class frmReporteOPyOM : Form
    {
        #region ATRIBUTOS Y CONSTRUCTORES

        private BackgroundWorker bgw;
        private Precarga precarga;

        public frmReporteOPyOM()
        {
            InitializeComponent();
            precarga = new Precarga(this);
            this.dtpDesde.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            this.dtpHasta.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
        }
        #endregion
        #region EVENTOS
        private void btnContinuar_Click(object sender, EventArgs e)
        {
            bgw = new BackgroundWorker();
            bgw.DoWork += bgw_DoWork;
            bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
            precarga.MostrarEspera();
            bgw.RunWorkerAsync();
        }
        #endregion
        #region WORKERS
        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            string archivoTemporal = Path.GetTempFileName().Replace(".tmp", ".xls");
            precarga.AsignastatusProceso("Generando información...");
            //REPORTE DE SEGUIMIENTO
            DataTable dataTableReporte = new DataTable();
            dataTableReporte = OrdProduccion.RegresaReporteOPyOM(dtpDesde.Value, dtpHasta.Value);
            if (dataTableReporte.Rows.Count == 0)
            {
                MessageBox.Show("No se encontraron registros", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            precarga.AsignastatusProceso("Creando archivo de excel...");
            OrdProduccion.GeneraArchivoExcelOPyOM(archivoTemporal, dataTableReporte, dtpDesde.Value, dtpHasta.Value);

            FuncionalidadesFormularios.MostrarExcel(archivoTemporal);

        }
        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            precarga.RemoverEspera();
        }
        #endregion
    }
}
