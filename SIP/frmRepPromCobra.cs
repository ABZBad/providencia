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
    public partial class frmRepPromCobra : Form
    {
        private Precarga precarga;
        public frmRepPromCobra()
        {
            InitializeComponent();
            precarga = new Precarga(this);
            
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {

            precarga.MostrarEspera();
            cambia_botones();
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
            RepPromCobra PronCobra = new RepPromCobra();
            DataTable dataTable = PronCobra.RegresaTabla(txtAgente.Text.Trim());
            if (dataTable.Rows.Count>0)
            {
                precarga.AsignastatusProceso("Creando archivo de excel...");
                string archivoTemporal = System.IO.Path.GetTempFileName().Replace(".tmp", ".xls");
                RepPromCobra.GeneraArchivoExcel(archivoTemporal, dataTable,txtAgente.Text.Trim());
                //System.Diagnostics.Process.Start(archivoTemporal);
                FuncionalidadesFormularios.MostrarExcel(archivoTemporal);
            }
            else
            {
                MessageBox.Show("No existen registros con el agente ingresados.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
        }

        public void cambia_botones()
        {
            
            if(this.btnContinuar.Enabled == true)
            {
                btnContinuar.Enabled = false;
                
                //AQUI FALTA DESHABILITAR OPCIONES DEL MENÚ Y DE OTROS BOTONES DE ALGUNAS PANTALLAS
                

            }
            else
            {
                btnContinuar.Enabled = true;

                //AQUI FALTA HABILITAR OPCIONES DEL MENÚ Y DE OTROS BOTONES DE ALGUNAS PANTALLAS

            }
        }
    }
}
