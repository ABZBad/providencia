using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIP.Utiles;
using ulp_bl.Reportes;

namespace SIP
{
    public partial class frmRptUniPediEmq : Form
    {
        private BackgroundWorker bgw = new BackgroundWorker();
        private Precarga precarga;
        public frmRptUniPediEmq()
        {
            InitializeComponent();
            precarga = new Precarga(this);
            Show();
        }

        private void frmRptUniPediEmq_Load(object sender, EventArgs e)
        {
            bgw.DoWork += bgw_DoWork;
            bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
            bgw.RunWorkerAsync();
            precarga.MostrarEspera();            
        }

        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            precarga.RemoverEspera();
            Close();
        }

        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            precarga.AsignastatusProceso("Procesando datos...");
            DataTable dataTableUiversoPedidosEmpaque = RepUniversoPedidosEmpaque.RegresaUniversoPedidosEmpaque();
            precarga.AsignastatusProceso("Generando archivo de Excel...");
            string archivoTemporal = System.IO.Path.GetTempFileName().Replace(".tmp", ".xls");
            RepUniversoPedidosEmpaque.GeneraArchivoExcel(dataTableUiversoPedidosEmpaque, archivoTemporal);
            //System.Diagnostics.Process.Start(archivoTemporal);
            FuncionalidadesFormularios.MostrarExcel(archivoTemporal);

        }
    }
}
