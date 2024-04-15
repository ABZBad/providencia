using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ulp_bl;
using SIP.Utiles;

namespace SIP
{
    public partial class frmFacturasPorAnticipado : Form
    {
        int Pedido;
        DataTable dtDetallePedido;
        private BackgroundWorker bgw;
        private Precarga precarga;
        private Exception ex;
        private String Error = String.Empty;

        public frmFacturasPorAnticipado(int Pedido)
        {
            InitializeComponent();
            this.Pedido = Pedido;
            precarga = new Precarga(this);
            dtDetallePedido = new DataTable();
        }

        private void frmFacturasPorAnticipado_Load(object sender, EventArgs e)
        {
            PED_DET ped_det = new PED_DET();
            dtDetallePedido = ped_det.ConsultarDetalle(Pedido);
            dgvDetallePedido.DataSource = dtDetallePedido;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            bgw = new BackgroundWorker();
            bgw.DoWork += bgw_DoWork;
            bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
            precarga.MostrarEspera();
            bgw.RunWorkerAsync();       
        }

        void bgw_DoWork(object sender, DoWorkEventArgs e) { 
            //VALIDAMOS QUE EL PEDIDO NO HAYA SIDO YA TRANSFERIDO
            this.ex = new Exception();
            this.Error = "";
            DataTable dtValidacion = FacturasPorAnticipado.ValidaTransferenciaPedido("P" + this.Pedido.ToString(), ref ex);
            if (ex!=null)
            {
                return;
            }
            if (dtValidacion.Rows.Count > 0)
            {
                if (dtValidacion.Rows[0]["ERROR"].ToString() != "")
                {
                    this.Error = dtValidacion.Rows[0]["ERROR"].ToString();
                    return;
                }
            }

            //OBTENEMOS LA CLAVE DE FOLIO AGRUPADOR
            int ClaveFolio = FacturasPorAnticipado.getSiguienteFolioAgrupador();
            //YA QUE TENEMOS LA CLAVE POR CADA ARTICULO GENERAMOS LA TRANSFERENCIA VIRTUAL
            foreach (DataRow dr in dtDetallePedido.Rows)
            {
                ex = new Exception();
                FacturasPorAnticipado.setTransferenciaVirtualArticulo("P" + this.Pedido.ToString(), dr["CVE_ART"].ToString(), int.Parse(dr["CANT"].ToString()), ClaveFolio, "FactAnt:"+ txtReferencia.Text.Trim(),ref ex);
                if (ex!=null)
                    this.Error += ex.Message;
            }
            FacturasPorAnticipado.setSiguienteFolioAgrupador(ClaveFolio);
            
        }

        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (this.Error == "")
            {
                MessageBox.Show("La Transferencia se procesó de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error al procesar la transferencia virtual. " + this.Error, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //DAMOS REVERSA AL PROCESO POR NUMERO DE PEDIDO
            }
            precarga.RemoverEspera();
            this.Close();
        }
    }
}
