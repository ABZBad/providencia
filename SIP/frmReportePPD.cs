using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ulp_bl;
using SIP.Utiles;

namespace SIP
{
    public partial class frmReportePPD : Form
    {
        
        CFDI.TipoCuenta tipo;
        private BackgroundWorker bgw;
        private Precarga precarga;
        public frmReportePPD()
        {
            InitializeComponent();
            precarga = new Precarga(this);
            this.dtpDesde.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            this.dtpHasta.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
        }
        private void btnContinuar_Click(object sender, EventArgs e)
        {
            if (radCXP.Checked)
                this.tipo = CFDI.TipoCuenta.CXP;
            else
                this.tipo = CFDI.TipoCuenta.CXC;
            bgw = new BackgroundWorker();
            bgw.DoWork += bgw_DoWork;
            bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
            precarga.MostrarEspera();
            bgw.RunWorkerAsync();
        }
        #region WORKERS
        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            string archivoTemporal = Path.GetTempFileName().Replace(".tmp", ".xls");
            precarga.AsignastatusProceso("Generando información...");
            //REPORTE DE SEGUIMIENTO
            DataTable dataTableReporte=new DataTable();
            if (this.tipo==CFDI.TipoCuenta.CXP)
                dataTableReporte= CFDI.getReprotePPD_CXP(dtpDesde.Value, dtpHasta.Value);
            if (this.tipo==CFDI.TipoCuenta.CXC)
                dataTableReporte = CFDI.getReprotePPD_CXC(dtpDesde.Value, dtpHasta.Value);
            if (dataTableReporte.Rows.Count == 0)
            {
                MessageBox.Show("No se encontraron registros", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            precarga.AsignastatusProceso("Creando archivo de excel...");
            CFDI.GeneraArchivoExcel(archivoTemporal, dataTableReporte, dtpDesde.Value, dtpHasta.Value);

            FuncionalidadesFormularios.MostrarExcel(archivoTemporal);

        }
        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            precarga.RemoverEspera();
        }

        #endregion

        


    }
}
