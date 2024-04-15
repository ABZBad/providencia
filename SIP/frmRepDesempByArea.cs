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
    public partial class frmRepDesempByArea : Form
    {        
        private Precarga precarga;
        public frmRepDesempByArea()
        {
            InitializeComponent();
            precarga = new Precarga(this);
        }

        private void frmRepDesempByArea_Load(object sender, EventArgs e)
        {
            dtFechaInicial.Value = DateTime.Now.AddDays(-1);
            dtFechaFinal.Value = DateTime.Now;
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {                        
            precarga.MostrarEspera();
            BackgroundWorker backGroundWorker = new BackgroundWorker();

            backGroundWorker.DoWork += backGroundWorker_DoWork;
            backGroundWorker.RunWorkerCompleted += backGroundWorker_RunWorkerCompleted;
            backGroundWorker.RunWorkerAsync();

        }

        private void backGroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            precarga.RemoverEspera();
            precarga = null;
            precarga = new Precarga(this);
   
        }

        private void backGroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            precarga.AsignastatusProceso("Procesando Detalle de pedidos...");
            DataTable dataTableDetalleDesempeño = DesempByArea.RegresaDetalleDesempeño(dtFechaInicial.Value, dtFechaFinal.Value);


            if (dataTableDetalleDesempeño.Rows.Count > 0)
            {

                precarga.AsignastatusProceso("Procesando fechas cumplidas...");
                int x1 = DesempByArea.RegresaFechasCumplidas(dtFechaInicial.Value, dtFechaFinal.Value,
                    Enumerados.TipoFechaReporteDesempeños.FechasCumplidas);
                precarga.AsignastatusProceso("Procesando fechas adelantadas...");
                int x2 = DesempByArea.RegresaFechasCumplidas(dtFechaInicial.Value, dtFechaFinal.Value,
                    Enumerados.TipoFechaReporteDesempeños.FechasAdelantadas);
                precarga.AsignastatusProceso("Procesando fechas no cumplidas...");
                int x3 = DesempByArea.RegresaFechasCumplidas(dtFechaInicial.Value, dtFechaFinal.Value,
                    Enumerados.TipoFechaReporteDesempeños.FechasNoCumplidas);
                precarga.AsignastatusProceso("Procesando fechas no entregadas...");
                int x4 = DesempByArea.RegresaFechasCumplidas(dtFechaInicial.Value, dtFechaFinal.Value,
                    Enumerados.TipoFechaReporteDesempeños.FechasNoEntregadas);
                precarga.AsignastatusProceso("Procesando pedidos por agente...");
                DataTable desempAgente = DesempByArea.RegresaPedidosContadoPorAgente(dtFechaInicial.Value,
                    dtFechaFinal.Value);
                precarga.AsignastatusProceso("Procesando resumen por área...");
                DataTable desempResumen = DesempByArea.RegresaDesempeñoPorAreaResumen(dtFechaInicial.Value,
                    dtFechaFinal.Value);
                precarga.AsignastatusProceso("Creando archivo de Excel...");
                string archivoTemporal = System.IO.Path.GetTempFileName().Replace(".tmp", ".xls");
                DesempByArea.GeneraArchivoExcel(dataTableDetalleDesempeño, desempAgente, x1, x2, x3, x4, desempResumen,
                    archivoTemporal);
                //System.Diagnostics.Process.Start(archivoTemporal);
                FuncionalidadesFormularios.MostrarExcel(archivoTemporal);
            }
            else
            {
                MessageBox.Show("No hay información para generar el reporte", "", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
    }
}
