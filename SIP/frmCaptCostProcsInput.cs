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
    public partial class frmCaptCostProcsInput : frmInputBox
    {
        private Precarga precarga;

        string NumPedido;
        bool MostrarPantalla=false;
        public frmCaptCostProcsInput()
        {
            InitializeComponent();
            precarga = new Precarga(this);
            NumPedido = "";
        }

        private void frmCaptCostProcs_Load(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ejecutar();
        }

        private void ejecutar()
        {
            if (NTxtOrden.Text.Trim() != "")
            {
                NumPedido = NTxtOrden.Text;
                ejecuta();
                this.Close();
            }
            else
            {
                MessageBox.Show(this, "Debe ingresar un número de pedido valido.", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void ejecuta()
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

            if (MostrarPantalla == true)
            {
                frmCaptCostProcs CaptCostProcs = new frmCaptCostProcs();
                CaptCostProcs.NumPedido = this.NumPedido;
                CaptCostProcs.Show();
            }
        }

        private void backGroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            precarga.AsignastatusProceso("Procesando...");


                CaptCostProcs CaptCostProcs = new CaptCostProcs();
                // La clase CaptCostProcs Modifica internamente datos, ejecuta procesos etc para que finalmente al llamar la pantalla frmCaptCostProcs consulte los datos ya actualizados según el proceso
                precarga.AsignastatusProceso("Creando archivo de excel...");
                if (CaptCostProcs.ActualizaDatos(NumPedido)== false)
                {
                    MostrarPantalla = false;
                    MessageBox.Show("Pedido sin datos que mostrar.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                MostrarPantalla = true;
        }

        private void NTxtOrden_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\r")
            {
                ejecutar();
            }
        }
    }
}
