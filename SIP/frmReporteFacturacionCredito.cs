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
    public partial class frmReporteFacturacionCredito : Form
    {
        #region<ATRIBUTOS Y CONSTRUCTORES>
        private BackgroundWorker bgw;
        private Precarga precarga;
        private Exception ex;
        private DataTable dtSerie;
        private String serieSeleccionada;

        public frmReporteFacturacionCredito()
        {
            InitializeComponent();
            precarga = new Precarga(this);
            this.dtSerie = new DataTable();
            this.dtpDesde.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            this.dtpHasta.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
        }
        #endregion
        #region<EVENTOS>
        private void frmReporteFacturacionCredito_Load(object sender, EventArgs e)
        {
            this.dtSerie = ReporteFacturacionCredito.RegresaSeriesFactura();
            cmbSerie.ValueMember = "SERIE";
            cmbSerie.DisplayMember = "SERIE";
            cmbSerie.DataSource = this.dtSerie;
        }
        private void btnContinuar_Click(object sender, EventArgs e)
        {
            this.serieSeleccionada = cmbSerie.SelectedValue.ToString();
            bgw = new BackgroundWorker();
            bgw.DoWork += bgw_DoWork;
            bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
            precarga.MostrarEspera();
            bgw.RunWorkerAsync();
        }
        #endregion
        #region <WORKERS>
        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            precarga.AsignastatusProceso("Generando información...");
            DataTable dtReporte = new DataTable();

            dtReporte = ReporteFacturacionCredito.RegresaReporteFacturacion(dtpDesde.Value, dtpHasta.Value, this.serieSeleccionada);

            if (dtReporte != null)
            {
                if (dtReporte.Rows.Count != 0)
                {
                    precarga.AsignastatusProceso("Generando archivo de Excel...");
                    //GENERAMOS EL EXCEL
                    string archivoTemporal = System.IO.Path.GetTempFileName().Replace(".tmp", ".xls");
                    ReporteFacturacionCredito.GeneraArchivoExcel(archivoTemporal, dtReporte, dtpDesde.Value, dtpHasta.Value);
                    FuncionalidadesFormularios.MostrarExcel(archivoTemporal);
                }
                else
                {
                    MessageBox.Show("No existen facturas con este rango de fechas", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("No existen facturas con este rango de fechas", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            precarga.RemoverEspera();
        }
        #endregion

    }
}
