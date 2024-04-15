using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIP.Properties;
using SIP.Reportes;
using SIP.Utiles;
using ulp_bl;

namespace SIP
{
    public partial class frmCargaPedidosSAE : Form
    {

        public bool StatusProceso;
        private bool Consultando;
        private BackgroundWorker bgw = new BackgroundWorker();
        private EstandaresPedido estandaresPedido = null;
        private Precarga precarga;
        private Timer tmrConsultar = new Timer();
        String Prefijo = "P";
        public frmCargaPedidosSAE()
        {
            InitializeComponent();
            bgw.DoWork += bgw_DoWork;
            bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
            precarga = new Precarga(this);
            tmrConsultar.Interval = 250;
            tmrConsultar.Tick += tmrConsultar_Tick;
            this.StatusProceso = false;
        }

        public frmCargaPedidosSAE(int Pedido)
        {
            InitializeComponent();
            bgw.DoWork += bgw_DoWork;
            bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
            precarga = new Precarga(this);
            tmrConsultar.Interval = 250;
            tmrConsultar.Tick += tmrConsultar_Tick;
            txtPedido.Text = Pedido.ToString();
        }

        void tmrConsultar_Tick(object sender, EventArgs e)
        {
            tmrConsultar.Enabled = false;
            precarga.MostrarEspera();
            precarga.AsignastatusProceso("Consultando estándares...");
            bgw.RunWorkerAsync();

        }

        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            precarga.DesactivarControles(false);
            precarga.RemoverEspera();
            lblAdministrativo.Text = string.Format("Administrativo: ({0})", estandaresPedido.ADVO);
            lblLiberacion.Text = string.Format("Liberación: ({0})", estandaresPedido.LIB);
            lblSurtido.Text = string.Format("Surtido: ({0})", estandaresPedido.SUR);
            lblCorte.Text = string.Format("Corte: ({0})", estandaresPedido.COR);
            lblEstampado.Text = string.Format("Estampado: ({0})", estandaresPedido.EST);
            lblBordado.Text = string.Format("Bordado: ({0})", estandaresPedido.BOR);
            lblIni.Text = string.Format("Ini: ({0})", estandaresPedido.INI);
            lblCostura.Text = string.Format("Costura: ({0})", estandaresPedido.COS);
            lblEmpaque.Text = string.Format("Empaque: ({0})", estandaresPedido.EMP);
            lblEmbarque.Text = string.Format("Embarque: ({0})", estandaresPedido.EMB);

            IblIntroValor.Text = string.Format("Última modificación por: {0} el \n{1}", estandaresPedido.USUARIO,
                estandaresPedido.FCH_MODI);

            txtPedido.Focus();
        }

        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            estandaresPedido = CargaPedidosSAE.RegresaEstandares();
        }

        private void frmCargaPedidosSAE_Load(object sender, EventArgs e)
        {

        }

        private void frmCargaPedidosSAE_Activated(object sender, EventArgs e)
        {
            if (!Consultando)
            {
                Consultando = true;
                tmrConsultar.Enabled = true;
                precarga.DesactivarControles(true);
            }
        }

        private void btnCapturaImprimir_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtPedido.Text))
            {
                string noPedido = txtPedido.Text;
                Exception ex = null;

                if (string.IsNullOrEmpty(txtComision.Text))
                    txtComision.Text = "0";

                Cursor = Cursors.WaitCursor;

                if (chkPedido.Checked)
                    Prefijo = "P";
                if (chkDAT.Checked)
                    Prefijo = "D";
                if (chkMostrador.Checked)
                    Prefijo = "M";
                if (chkPedidoEC.Checked)
                    Prefijo = "E";
                if (chkMostradorCP.Checked)
                    Prefijo = "MP";

                CargaPedidosSAE.CargaPedidoEnSae(Convert.ToInt32(noPedido), decimal.Parse(txtComision.Text),
                    decimal.Parse(txtIva.Text), ref ex, Prefijo);
                if (ex == null)
                {
                    Cursor = Cursors.Default;
                    //TODO: enviar mensaje: Pedido cargado OK!
                    MessageBox.Show(Resources.Cadena_DatosGuardados, "Carga exitosa", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    txtPedido.Text = "";
                    txtComision.Text = "0";
                    StatusProceso = true;
                    // Imprimir(noPedido);
                }
                else
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    switch (ex.Source)
                    {
                        case "FACTP01":
                            //el pedido ya ha sido cargado con anterioridad y se pregunta al usuario si quiere imprimirlo
                            StatusProceso = true;
                            // Imprimir(txtPedido.Text);
                            break;
                        default:
                            StatusProceso = false;
                            break;
                    }

                }
            }
            else
            {
                MessageBox.Show("El Pedido es un dato requerido", "Verifique", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                txtPedido.Focus();
            }
        }


        private void Imprimir(string NumeroPedido)
        {
            DialogResult resp = MessageBox.Show("¿ Imprimir ?", "Confirme", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);
            if (resp == System.Windows.Forms.DialogResult.Yes)
            {
                /*
                Reportes.frmReportes frmReportes =
                    new Reportes.frmReportes(Enumerados.TipoReporteCrystal.PedidoCapturaSAE, Convert.ToInt32(NumeroPedido));
                frmReportes.Show();
                 */
                DataTable dataTablePedidoSae = CargaPedidosSAE.RegresaPedidoSae(Convert.ToInt32(NumeroPedido), this.Prefijo);
                rptPedidosCapturaSae rptPedidosCapturaSae = new rptPedidosCapturaSae();
                rptPedidosCapturaSae.SetDataSource(dataTablePedidoSae);
                rptPedidosCapturaSae.PrintToPrinter(1, true, 0, 0);
            }
        }

    }
}
