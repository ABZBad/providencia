using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ulp_bl;
using SIP.Utiles;

namespace SIP
{
    public partial class frmReporteMovimientosMP : Form
    {
        private BackgroundWorker bgw;
        private Precarga precarga;
        DataTable dtConceptos = new DataTable();
        String xml = "";

        public frmReporteMovimientosMP()
        {
            InitializeComponent();
            precarga = new Precarga(this);
            dgConceptos.AutoGenerateColumns = false;
            this.dtpDesde.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            this.dtpHasta.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
        }
        private void frmReporteMovimientosMP_Load(object sender, EventArgs e)
        {
            // Cargamos el catalogo de conceptos
            this.dtConceptos = ReporteMovimientosMP.ConsultaCatalogoConceptos();
            dgConceptos.DataSource = this.dtConceptos;
        }
        private void btnContinuar_Click(object sender, EventArgs e)
        {
            var checkedRows = from DataGridViewRow r in dgConceptos.Rows
                              where Convert.ToBoolean(r.Cells["SELECCION"].Value ?? false) == true
                              select r;
            this.xml = "<conceptos>";

            if (checkedRows.Count() > 0)
            {
                foreach (var row in checkedRows)
                {
                    this.xml += String.Format("<concepto>{0}</concepto>", row.Cells["CVE_CPTO"].Value.ToString());
                }
                this.xml += "</conceptos>";

                bgw = new BackgroundWorker();
                bgw.DoWork += bgw_DoWork;
                bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
                precarga.MostrarEspera();
                bgw.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show("Se debe de seleccionar al menos 1 concepto", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #region WORKERS
        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            string archivoTemporal = Path.GetTempFileName().Replace(".tmp", ".xls");
            precarga.AsignastatusProceso("Generando información...");
            //REPORTE DE SEGUIMIENTO            
            DataTable dataTableRecepcion = ReporteMovimientosMP.ConsultaReporteMovimientosMP(dtpDesde.Value, dtpHasta.Value, this.xml);
            if (dataTableRecepcion.Rows.Count == 0)
            {
                MessageBox.Show("No se encontraron registros", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            precarga.AsignastatusProceso("Creando archivo de excel...");
            ReporteMovimientosMP.GetReporteExcel(archivoTemporal, dataTableRecepcion, dtpDesde.Value, dtpHasta.Value);
            FuncionalidadesFormularios.MostrarExcel(archivoTemporal);

        }
        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            precarga.RemoverEspera();
        }

        #endregion
    }
}
