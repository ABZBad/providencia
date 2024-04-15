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
    public partial class frmReporteProvision : Form
    {
        #region CONSTRUCTOR Y ATRIBUTOS
        private BackgroundWorker bgw;
        private Precarga precarga;
        private Exception ex;

        public frmReporteProvision()
        {
            InitializeComponent();
            precarga = new Precarga(this);
        }
        #endregion
        #region EVENTOS
        private void frmReporteProvision_Load(object sender, EventArgs e)
        {
            this.dtpDesde.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            this.dtpHasta.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
        }
        private void btnContinuar_Click(object sender, EventArgs e)
        {
            bgw = new BackgroundWorker();
            bgw.DoWork += bgw_DoWork;
            bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
            precarga.MostrarEspera();
            bgw.RunWorkerAsync();
        }
        #endregion
        #region<WORKERS>
        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            precarga.AsignastatusProceso("Generando información...");
            DataSet dsResultado = new DataSet();

            dsResultado = ReporteProvisionNomina.ConsultaReporteProvision(dtpDesde.Value, dtpHasta.Value);

            if (dsResultado != null)
            {
                if (dsResultado.Tables.Count != 0)
                {
                    // Creamos un solo DataTable con cada Percepcion (gravado y exento) y retenciones (gravado y exento)
                    DataTable dtResultado = new DataTable();

                    precarga.AsignastatusProceso("Generando archivo de Excel...");
                    //GENERAMOS EL EXCEL
                    string archivoTemporal = System.IO.Path.GetTempFileName().Replace(".tmp", ".xls");
                    ReporteProvisionNomina.GetReporteExcel(archivoTemporal, dsResultado, dtpDesde.Value, dtpHasta.Value);
                    FuncionalidadesFormularios.MostrarExcel(archivoTemporal);
                }
                else
                {
                    MessageBox.Show("No existen pedidos con este rango de fechas", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("No existen pedidos con este rango de fechas", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            precarga.RemoverEspera();
        }
        #endregion
    }
}
