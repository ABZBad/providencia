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
    public partial class frmRepExportaUpPedidos : Form
    {
        private Precarga precarga;
        public frmRepExportaUpPedidos()
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
            vw_UpPedidos vwUpPedidos = new vw_UpPedidos();
            ulp_bl.Enumerados.TipoReportePedido tipoReportePedido;
            if (chkSinFechaSurtido.Checked)
            {
                tipoReportePedido = ulp_bl.Enumerados.TipoReportePedido.SinFechaSurtido;
            }
            else
            {
                tipoReportePedido = ulp_bl.Enumerados.TipoReportePedido.ConFechaSurtido;
            }
            /*
             * 
             * Permisos para este módulo:
             * 
             *      91=Exporta UpPedidos a Excel fecha SIP (MÓDULO)
             *      96="Incluir usuario en reporte"	Incluir_Usuario
             * 
             * 
             */

            bool incluirVendedor = PermisosUsuarioEspeciales.TienePermisos(Globales.UsuarioActual.Id, 91, 96);

            DataTable dataTablePedidos = vwUpPedidos.RegresaTablaPedidos(tipoReportePedido, dtpIni.Value, dtpFin.Value, (incluirVendedor ? Globales.UsuarioActual.UsuarioUsuario : ""));



            precarga.AsignastatusProceso("Procesando pedidos...");
            string archivoTemporal = System.IO.Path.GetTempFileName().Replace(".tmp", ".xls");
            precarga.AsignastatusProceso("Creando archivo de excel...");
            vw_UpPedidos.GeneraArchivoExcel(archivoTemporal, dataTablePedidos);
            //System.Diagnostics.Process.Start(archivoTemporal);
            FuncionalidadesFormularios.MostrarExcel(archivoTemporal);
        }

        private void GeneraARchivo()
        {

        }

        private void frmRepExportaUpPedidos_Load(object sender, EventArgs e)
        {
            dtpIni.Value = DateTime.Now.AddDays(-1);
            dtpFin.Value = DateTime.Now;
        }

        private void dtpFin_ValueChanged(object sender, EventArgs e)
        {

        }

        private void chkSinFechaSurtido_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dtpIni_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
