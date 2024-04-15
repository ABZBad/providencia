using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using SIP.Utiles;
using ulp_bl;
using ulp_bl.Reportes;

namespace SIP
{
    public partial class frmRepFolNuev : Form
    {
        private BackgroundWorker backGroundWorker;
        private Precarga precarga;
        private int totalRegistros = 0;
        Enumerados.AreasEmpresa TipoArea;
        public frmRepFolNuev(Enumerados.AreasEmpresa TipoArea)
        {
            InitializeComponent();
            precarga = new Precarga(this);
            this.TipoArea = TipoArea;
        }

        private void frmRepFolAnt_Load(object sender, EventArgs e)
        {
            dtpIni.Value = DateTime.Now.AddDays(-1);
            dtpFin.Value = DateTime.Now;
            if (this.TipoArea == Enumerados.AreasEmpresa.Contabilidad)
            {
                radioAmbos.Checked = true;
                radioCredito.Enabled = false;
                radioMostrador.Enabled = false;

            }
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            precarga.MostrarEspera();
            backGroundWorker = new BackgroundWorker();
            backGroundWorker.DoWork += backGroundWorker_DoWork;
            backGroundWorker.RunWorkerCompleted += backGroundWorker_RunWorkerCompleted;
            backGroundWorker.RunWorkerAsync();
        }

        void backGroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Quitar progressbar
            precarga.RemoverEspera();
            backGroundWorker.Dispose();
            //MessageBox.Show(string.Format("Excel generado"));
        }

        void backGroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            precarga.AsignastatusProceso("Espere...");
            vw_FactFoliosAntPrendas factFoliosAntPrendas = new vw_FactFoliosAntPrendas();
            //poner progressbar


            //crear primer bloque de resultados
            if (radioCredito.Checked)
            {
                factFoliosAntPrendas.TipoReporte = Enumerados.TipoReporteFoliosAnt.Credito;
            }
            else if (radioMostrador.Checked)
            {
                factFoliosAntPrendas.TipoReporte = Enumerados.TipoReporteFoliosAnt.Mostrador;
            }
            else
            {
                factFoliosAntPrendas.TipoReporte = Enumerados.TipoReporteFoliosAnt.Ambos;
            }
            precarga.AsignastatusProceso("Procesando fact. nuevo...");
            DataTable dataTableFacAnt = factFoliosAntPrendas.RegresaTablaFacturacionAnt(Enumerados.TipoReporteFolios.Nuevos, this.TipoArea, dtpIni.Value, dtpFin.Value);
            precarga.AsignastatusProceso("Procesando notas de crédito...");
            DataTable dataTableNotas = factFoliosAntPrendas.RegresaTablaNotasCredito(Enumerados.TipoReporteFolios.Nuevos, dtpIni.Value, dtpFin.Value);
            precarga.AsignastatusProceso("Procesando notas de venta...");
            DataTable dataTableNV = factFoliosAntPrendas.RegresaTablaNVAnt(dtpIni.Value, dtpFin.Value);
            precarga.AsignastatusProceso("Creando archivo de excel...");

            dataTableFacAnt.Merge(dataTableNV, true);

            string archivoTemporal = System.IO.Path.GetTempFileName().Replace(".tmp", ".xls");

            vw_FactFoliosAntPrendas.GeneraArchivoExcel(archivoTemporal, dataTableFacAnt, dataTableNotas);

            //System.Diagnostics.Process.Start(archivoTemporal);

            FuncionalidadesFormularios.MostrarExcel(archivoTemporal);

        }

    }
}
