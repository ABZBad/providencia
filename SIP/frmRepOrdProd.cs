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
    public partial class frmRepOrdProd : frmInputBox
    {
        private Precarga precarga;

        string referenciaInicial;
        string referenciaFinal;
        bool Switch;
        Enumerados.TipoOrdenProduccion tipo = new Enumerados.TipoOrdenProduccion();

        public frmRepOrdProd()
        {
            InitializeComponent();
            precarga = new Precarga(this);
            referenciaInicial="";
            referenciaFinal = "";
            Switch = false;
            
        }
        public frmRepOrdProd(Enumerados.TipoOrdenProduccion tipoOrden)
        {
            InitializeComponent();
            tipo = tipoOrden;
            precarga = new Precarga(this);
            referenciaInicial = "";
            referenciaFinal = "";
            Switch = false;

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ejecutar();
        }
        
        private void ejecutar()
        {
            if (NTxtOrden.Text.Trim() != "")
            {
                if (Switch == false)
                {
                    referenciaInicial = NTxtOrden.Text;
                    NTxtOrden.Text = "";
                    lblTitulo.Text = "Órden Final";
                    Switch = true;
                    NTxtOrden.Focus();
                }
                else
                {
                    referenciaFinal = NTxtOrden.Text;
                    ejecuta();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show(this, "Debe ingresar un número de referencia valido.", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void ejecuta ()
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
            precarga.AsignastatusProceso("Procesando reporte...");
            RepOrdProd OrdProd = new RepOrdProd();
            DataTable dataTable = OrdProd.RegresaTabla(referenciaInicial, referenciaFinal, tipo);
            if (dataTable.Rows.Count > 0)
            {
                precarga.AsignastatusProceso("Creando archivo de excel...");
                string archivoTemporal = System.IO.Path.GetTempFileName().Replace(".tmp", ".xls");
                if (tipo==Enumerados.TipoOrdenProduccion.Liberada)
                {
                    RepOrdProd.GeneraArchivoExcel(archivoTemporal, dataTable, referenciaFinal);    
                }
                else
                {
                    RepOrdProd.GeneraArchivoExcelOrdenesNoLiberadas(archivoTemporal, dataTable, referenciaFinal);
                }
                
                //System.Diagnostics.Process.Start(archivoTemporal);
                FuncionalidadesFormularios.MostrarExcel(archivoTemporal);
            }
            else
            {
                MessageBox.Show("No existen registros con las opciones ingresadas.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void NTxtOrden_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\r")
            {
                ejecutar();
            }
        }

        private void frmRepOrdProd_Load(object sender, EventArgs e)
        {

        }
    }
}
