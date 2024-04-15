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
    public partial class frmPartidas : Form
    {
        private bool formaCargada;
        DataTable detallePedido;
        private FACTP01 datos_resumen = null;
        int Pedido = 0;
        private Precarga precarga;
        public frmPartidas()
        {
            InitializeComponent();
        }
        public frmPartidas(int pedido)
        {
            InitializeComponent();
            precarga = new Precarga(this);
            Pedido = pedido;
        }

        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dgViewPartidas.DataSource = detallePedido;

            lblTotal1.Text = Convert.ToDouble(datos_resumen.CAN_TOT).ToString("C2");
            lblTotal2.Text = Convert.ToDouble(datos_resumen.IMP_TOT4).ToString("C2");
            lblTotal3.Text = Convert.ToDouble(datos_resumen.CAN_TOT - datos_resumen.DES_TOT + datos_resumen.IMP_TOT4).ToString("C2");
            lblTotal4.Text = detallePedido.Rows.Count == 0 ? Convert.ToDouble(0).ToString("C2") : Convert.ToDouble(detallePedido.Compute("sum(CANT)", "")).ToString("C2");
            lblTotal5.Text = Convert.ToDouble(datos_resumen.DES_TOT).ToString();
            precarga.RemoverEspera();
        }

        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            LlenaDatos();
        }

        private void lblImpuesto_Click(object sender, EventArgs e)
        {

        }
        private void LlenaDatos()
        {
            PED_DET ped_det = new PED_DET();
            detallePedido = ped_det.ConsultarDetalle(Pedido);

            datos_resumen = new FACTP01();
            //datos_resumen = datos_resumen.ConsultarIdCadena("D" + Pedido.ToString());
            datos_resumen = datos_resumen.ConsultarIdCadena(Pedido.ToString());
        }

        private void frmPartidas_Activated(object sender, EventArgs e)
        {
            /*
            if (!formaCargada)
            {
                precarga.MostrarEspera();
                BackgroundWorker bgw = new BackgroundWorker();
                bgw.DoWork += bgw_DoWork;
                bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
                bgw.RunWorkerAsync();
            }*/
        }

        private void frmPartidas_Load(object sender, EventArgs e)
        {
            precarga.MostrarEspera();
            BackgroundWorker bgw = new BackgroundWorker();
            bgw.DoWork += bgw_DoWork;
            bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
            bgw.RunWorkerAsync();
        }

    }
}
