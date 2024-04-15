using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIP.UserControls;
using SIP.Utiles;
using ulp_bl;
using ulp_bl.Reportes;

namespace SIP
{
    public partial class frmRepClieNuevRecu : Form
    {
        private BackgroundWorker backGroundWorker;
        
        private Precarga precarga;
        
        public frmRepClieNuevRecu()
        {
            InitializeComponent();        
        }

        private void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            if (optNuevos.Checked)
            {
                if (!optForaneo.Checked && !optMetropolitano.Checked)
                {
                    MessageBox.Show("Para emitir el reporte de Clientes Nuevos es necesario especificar si será de tipo Foráneo o Metropolitano","", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            precarga = new Precarga(this);
            precarga.MostrarEspera();
            backGroundWorker = new BackgroundWorker();
            backGroundWorker.DoWork += backGroundWorker_DoWork;
            backGroundWorker.RunWorkerCompleted += backGroundWorker_RunWorkerCompleted;
            backGroundWorker.RunWorkerAsync();


            
            
            
        }

        private void backGroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Quitar progressbar
            precarga.RemoverEspera();
            backGroundWorker.Dispose();
        }

        private void backGroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            DataTable dataTableReporte = new DataTable();
            Enumerados.TipoReporteClientesRecuperados tipoReporte = Enumerados.TipoReporteClientesRecuperados.Ninguno;
            if (optNuevos.Checked)
            {

                if (optForaneo.Checked)
                {
                    tipoReporte = Enumerados.TipoReporteClientesRecuperados.Foraneo;
                }
                else
                {
                    tipoReporte = Enumerados.TipoReporteClientesRecuperados.Metropolitano;
                }
                precarga.AsignastatusProceso("Procesando clientes nuevos...");
                dataTableReporte = RepClientesRecuperados.RegresaClientesNuevos(dtFechaReporte.Value, tipoReporte);

            }
            else
            {
                precarga.AsignastatusProceso("Procesando clientes recuperados...");
                dataTableReporte = RepClientesRecuperados.RegresaClientesRecuperados(dtFechaReporte.Value);
            }

            if (dataTableReporte.Rows.Count > 0)
            {

                precarga.AsignastatusProceso("Generando archivo de Excel...");
                //generar excel
                string archivoTemporal = System.IO.Path.GetTempFileName().Replace(".tmp", ".xls");

                RepClientesRecuperados.GeneraArchivoExcel(archivoTemporal, dataTableReporte, tipoReporte,
                    dtFechaReporte.Value);

                //System.Diagnostics.Process.Start(archivoTemporal);
                FuncionalidadesFormularios.MostrarExcel(archivoTemporal);
            }
            else
            {
                MessageBox.Show("El reporte no contiene datos","",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
        }

        private void optRecuperados_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Visible = !optRecuperados.Checked;
        }
    }
}
