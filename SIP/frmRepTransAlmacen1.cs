using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ulp_bl.Utiles;
using ulp_bl;
using SIP.Utiles;


namespace SIP
{
    public partial class frmRepTransAlmacen1 : Form
    {

        #region <ATRIBUTOS Y CONSTRUCTORES>
        private BackgroundWorker bgw;
        private Precarga precarga;
        private Exception ex;

        public frmRepTransAlmacen1()
        {
            InitializeComponent();
            precarga = new Precarga(this);
        }

        #endregion
        #region <EVENTOS>
        private void frmRepTransAlmacen1_Load(object sender, EventArgs e)
        {
            Exception ex = new Exception();
            //CARGAMOS LOS ALMACENES
            cbAlmacenes.DisplayMember = "CVE_ALM";
            cbAlmacenes.ValueMember = "CVE_ALM";
            cbAlmacenes.DataSource = CargaAlmacenes();
            //ASIGNAMOS EL PRIMER DIA DEL MES DE LA FECHA ACTUAL
            dtFechaDesde.Value = GetPrimerDiaMes(DateTime.Now);
            dtFechaHasta.Value = GetUltimoDiaMes(dtFechaDesde.Value);
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {

            if (lstAlmacenes.FindString(cbAlmacenes.SelectedValue.ToString()) == -1)
                lstAlmacenes.Items.Add(cbAlmacenes.SelectedValue);
        }
        private void lstAlmacenes_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lstAlmacenes.SelectedIndex != -1)
            {
                lstAlmacenes.Items.RemoveAt(lstAlmacenes.SelectedIndex);
            }
        }
        private void btnContinuar_Click(object sender, EventArgs e)
        {
            bgw = new BackgroundWorker();
            bgw.DoWork += bgw_DoWork;
            bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
            precarga.MostrarEspera();
            bgw.RunWorkerAsync();
        }
        private void dtFechaDesde_ValueChanged(object sender, EventArgs e)
        {
            dtFechaHasta.Value = GetUltimoDiaMes(dtFechaDesde.Value);
        }

        #endregion
        #region <METODOS>
        private DataTable CargaAlmacenes()
        {
            DataSet dsListadoAlmacenes = new DataSet();
            dsListadoAlmacenes = ulp_bl.RepTransAlmacen1.GetListadoAlmacenes(ref ex);
            if (dsListadoAlmacenes != null)
            {
                return dsListadoAlmacenes.Tables[0];
            }
            else
                return null;
        }
        private DateTime GetPrimerDiaMes(DateTime FechaBase)
        {
            return new DateTime(FechaBase.Year, FechaBase.Month, 1);
        }
        private DateTime GetUltimoDiaMes(DateTime FechaBase)
        {
            DateTime FechaAux = GetPrimerDiaMes(FechaBase);
            return FechaAux.AddMonths(1).AddDays(-1);
        }


        #endregion
        #region <WORKERS>
        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            String AlmacenesDestino = String.Empty;

            if (lstAlmacenes.Items.Count==0)
            {
                MessageBox.Show("Se debe de seleccionar al menos un almacen de destino.","SIP",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return;
            }

            foreach(Object _almacen in lstAlmacenes.Items)
            {
                AlmacenesDestino +=_almacen + ",";
            }
            AlmacenesDestino = AlmacenesDestino.Substring(0,AlmacenesDestino.Length-1); //QUITAMOS LA ULTIMA ,

            precarga.AsignastatusProceso("Procesando información...");
            DataSet dsReporteTransferencias = RepTransAlmacen1.RegresaDatosSalidasDeAlmacen(dtFechaDesde.Value, dtFechaHasta.Value, "1", AlmacenesDestino,ref ex);
           
            if (ex == null)
            {
                if (dsReporteTransferencias != null)
                {
                    if (dsReporteTransferencias.Tables[0].Rows.Count != 0)
                    {
                        precarga.AsignastatusProceso("Generando archivo de Excel...");
                        //generar excel
                        string archivoTemporal = System.IO.Path.GetTempFileName().Replace(".tmp", ".xls");
                        RepTransAlmacen1.GeneraArchivoExcel(archivoTemporal, dsReporteTransferencias, dtFechaDesde.Value, dtFechaHasta.Value, "1",AlmacenesDestino);

                        //System.Diagnostics.Process.Start(archivoTemporal);
                        FuncionalidadesFormularios.MostrarExcel(archivoTemporal);
                    }
                    else
                        MessageBox.Show("No existen registros con este rango de fechas", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("No existen registros con este rango de fechas", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            precarga.RemoverEspera();
            if (ex != null)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ex = null;
            }
        }
        #endregion

        
    }
}
