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
    public partial class frmReporteGrupoCarso : Form
    {
        #region<ATRIBUTOS Y CONSTRUCTORES>
        private BackgroundWorker bgw;
        private Precarga precarga;
        private Exception ex;
        

        public frmReporteGrupoCarso()
        {
            InitializeComponent();
            precarga = new Precarga(this);
        }
         #endregion
        #region<EVENTOS>
        private void frmReporteGrupoCarso_Load(object sender, EventArgs e)
        {

        }
        #endregion
        #region<WORKERS>
        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            precarga.AsignastatusProceso("Generando información...");
            DataTable dtComprasCarso = new DataTable();

            dtComprasCarso = ReporteGrupoCarso.RegresaComprasGrupoCarso(dtpDesde.Value, dtpHasta.Value, ref ex);
            if (ex == null)
            {
                if (dtComprasCarso != null)
                {
                    if (dtComprasCarso.Rows.Count != 0)
                    {
                        precarga.AsignastatusProceso("Generando archivo de Excel...");
                        //GENERAMOS EL EXCEL
                        string archivoTemporal = System.IO.Path.GetTempFileName().Replace(".tmp", ".xls");
                        ReporteGrupoCarso.GeneraArchivoExcel(archivoTemporal, dtComprasCarso, dtpDesde.Value, dtpHasta.Value);
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
            else
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            precarga.RemoverEspera();
        }
        #endregion

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            bgw = new BackgroundWorker();
            bgw.DoWork += bgw_DoWork;
            bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
            precarga.MostrarEspera();
            bgw.RunWorkerAsync();
        }
    }
}
