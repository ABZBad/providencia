using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SIP.Utiles;
using ulp_bl;

namespace SIP
{
    public partial class frmRelacionPedidosOP : Form
    {
        #region ATRIBUTOS Y CONSTRUCTORES
        private BackgroundWorker bgw = new BackgroundWorker();
        private BackgroundWorker bgwProcess = new BackgroundWorker();
        private Precarga precarga;
        int idFlujo = 0;
        DataTable dtPedidos = new DataTable();
        DataTable dtOpDetalle = new DataTable();
        String xmlPedidos = "";
        public Boolean enlaceFinalizado = false;
        public List<int> ListaPedidosEnlazados;
        public frmRelacionPedidosOP(int idFlujo)
        {
            InitializeComponent();
            this.idFlujo = idFlujo;
            this.ListaPedidosEnlazados = new List<int> { };
            precarga = new Precarga(this);
            bgw.DoWork += bgw_DoWork;
            bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
            bgwProcess.DoWork += bgwProcess_DoWork;
            bgwProcess.RunWorkerCompleted += bgwProcess_RunWorkerCompleted;
            this.dgvPedidos.AutoGenerateColumns = false;
            this.dgvOPDetalle.AutoGenerateColumns = false;

        }
        #endregion
        #region EVENTOS
        private void frmRelacionPedidosOP_Load(object sender, EventArgs e)
        {
            precarga.MostrarEspera();
            precarga.AsignastatusProceso("Cargando Pedidos...");
            bgw.RunWorkerAsync();
        }
        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            precarga.MostrarEspera();
            precarga.AsignastatusProceso("Guardando información...");
            this.xmlPedidos = GetXMLStringPedidos();
            bgwProcess.RunWorkerAsync();
        }
        #endregion        
        #region WORKERS
        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            // OBTENEMOS LAS OP Y DETALLE DEL FLUJO
            this.dtOpDetalle = FlujoOP.ConsultaFlujoOPDetalle(this.idFlujo).Tables[1];
            // CONSULTAMOS PEDIDOS PENDIENTES
            this.dtPedidos = FlujoOP.ConsultaPedidosPendientesOP();
        }
        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            precarga.RemoverEspera();
            this.dgvPedidos.DataSource = this.dtPedidos;
            this.dgvOPDetalle.DataSource = this.dtOpDetalle;
            this.SeleccionarPedidos();
        }
        void bgwProcess_DoWork(object sender, DoWorkEventArgs e)
        {
            FlujoOP.GuardaRelacionPedidosOP(this.idFlujo, this.xmlPedidos);
        }
        void bgwProcess_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            precarga.RemoverEspera();
            MessageBox.Show("Proceso finalizado de forma correcta", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.enlaceFinalizado = true;
            this.Close();
        }

        #endregion
        #region METODOS
        private void SeleccionarPedidos()
        {
            foreach (DataGridViewRow row in dgvPedidos.Rows)
            {
                row.Cells["Seleccion"].Value = true;
            }
        }
        private string GetXMLStringPedidos()
        {
            string xml = "<Pedidos>";
            foreach (DataGridViewRow row in dgvPedidos.Rows)
            {
                if ((Boolean)row.Cells["Seleccion"].Value)
                {
                    ListaPedidosEnlazados.Add((int)row.Cells["Pedido"].Value);
                    xml += String.Format("<Pedido>{0}</Pedido>", row.Cells["Pedido"].Value.ToString());
                }
            }
            xml += "</Pedidos>";
            return xml;
        }
        #endregion
    }
}
