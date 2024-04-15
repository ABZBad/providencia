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
    public partial class frmRepPartArt : Form
    {
        private Precarga precarga;

        enum TipoFiltro
        {
            Linea = 1,
            Modelo = 2, 
            Talla = 3    
        };

        public frmRepPartArt()
        {
            InitializeComponent();
            precarga = new Precarga(this);
        }

        private void btnEmitir_Click(object sender, EventArgs e)
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
            int opcionReporte =0;
            if (this.option1.Checked)
            {
                opcionReporte = (int)TipoFiltro.Linea;
            }
            else if (this.option2.Checked)
            {
                opcionReporte = (int)TipoFiltro.Modelo;
            }
            else if (this.option3.Checked)
            {
                opcionReporte = (int)TipoFiltro.Talla;
            }

            precarga.AsignastatusProceso("Procesando reporte...");
            RepPartArt PartArt = new RepPartArt();
            DataTable dataTable = PartArt.RegresaTabla(dtpFechaIni.Value, dtpFechaFin.Value, opcionReporte);
            if (dataTable.Rows.Count > 0)
            {
                precarga.AsignastatusProceso("Creando archivo de excel...");
                string archivoTemporal = System.IO.Path.GetTempFileName().Replace(".tmp", ".xls");
                RepPartArt.GeneraArchivoExcel(archivoTemporal, dataTable, opcionReporte, dtpFechaIni.Value, dtpFechaFin.Value);
                //System.Diagnostics.Process.Start(archivoTemporal);
                FuncionalidadesFormularios.MostrarExcel(archivoTemporal);
            }
            else
            {
                MessageBox.Show("No existen registros con las opciones ingresadas.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void frmRepPartArt_Load(object sender, System.EventArgs e)
        {
            dtpFechaIni.Value = DateTime.Today.AddDays(-1);
            dtpFechaFin.Value = DateTime.Today;
        }
    }
}
