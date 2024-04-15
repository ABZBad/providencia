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
    public partial class frmReporteAnalisisRetrasoOP : Form
    {
        #region<ATRIBUTOS Y CONSTRUCTORES>
        private BackgroundWorker bgw;
        private Precarga precarga;
        private Exception ex;
        private String _Agente;
        public frmReporteAnalisisRetrasoOP()
        {
            InitializeComponent();
            precarga = new Precarga(this);
        }
        #endregion
        #region <EVENTOS>
        private void frmReporteAnalisisRetrasoOP_Load(object sender, EventArgs e)
        {
            this.dtpFecha.Value = DateTime.Now;
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
        #region <WORKERS>
        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            precarga.AsignastatusProceso("Generando información...");
            DataTable dt = new DataTable();
            dt = ReporteAnalisisRetrasosOP.GetAnalisisRetrasosOP(dtpFecha.Value, ref ex);
            if (ex == null)
            {
                if (dt != null)
                {
                    if (dt.Rows.Count != 0)
                    {
                        precarga.AsignastatusProceso("Generando archivo de Excel...");
                        //GENERAMOS EL EXCEL
                        string archivoTemporal = System.IO.Path.GetTempFileName().Replace(".tmp", ".xls");
                        ReporteAnalisisRetrasosOP.GeneraArchivoExcel(archivoTemporal, dt, dtpFecha.Value);
                        FuncionalidadesFormularios.MostrarExcel(archivoTemporal);
                    }
                    else
                    {
                        MessageBox.Show("No existen pedidos con los criterios seleccionados.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("No existen órdenes con los criterios seleccionados.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            precarga.RemoverEspera();
        }
        #endregion


    }
}
