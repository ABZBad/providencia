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
    public partial class frmRetroAlimManObraPedido : Form
    {
        #region "Atributos y Constructores"

        DataTable dataTableProcesos = new DataTable();
        private BackgroundWorker bgwLoadProcesos = new BackgroundWorker();
        private BackgroundWorker bgwSaveProcesos = new BackgroundWorker();
        Precarga precarga;
        List<int> ListaIndices;
        List<String> ListaErrores;

        public frmRetroAlimManObraPedido()
        {
            InitializeComponent();
            precarga = new Precarga(this);
            dgvProcesos.AutoGenerateColumns = false;
        }
        #endregion
        #region "Eventos"
        private void txtPedido_Leave(object sender, EventArgs e)
        {
            if (txtPedido.Text.Trim() != "")
            {
                precarga.MostrarEspera();
                bgwLoadProcesos = new BackgroundWorker();
                bgwLoadProcesos.DoWork += bgwLoadProcesos_DoWork;
                bgwLoadProcesos.RunWorkerCompleted += bgwLoadProcesos_RunWorkerCompleted;
                bgwLoadProcesos.RunWorkerAsync();
            }
        }
        private void txtPedido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            precarga.MostrarEspera();
            this.ListaIndices = new List<int> { };           
            foreach (DataGridViewRow dr in dgvProcesos.Rows)
            {
                if (dr.Cells["CMT_PROCESADO"].Value.ToString() == "NO" && dr.Cells["CMT_CHECK"].Value.ToString()=="1")
                    this.ListaIndices.Add((int)dr.Cells["CMT_INDX"].Value);
            }
            
           
           bgwSaveProcesos = new BackgroundWorker();
           bgwSaveProcesos.DoWork += bgwSaveProcesos_DoWork;
           bgwSaveProcesos.RunWorkerCompleted += bgwSaveProcesos_RunWorkerCompleted;
           bgwSaveProcesos.RunWorkerAsync();
            
        }
        #endregion
        #region "Workers"
        //LOAD
        private void bgwLoadProcesos_DoWork(object sender, DoWorkEventArgs e)
        {
            precarga.AsignastatusProceso("Cargando Procesos...");
            dataTableProcesos = RetroAlimManObraPedido.CargaProcesosPedido(int.Parse(txtPedido.Text));


        }
        void bgwLoadProcesos_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dgvProcesos.DataSource = dataTableProcesos;
            InhabilitaGridProcesos();
            precarga.RemoverEspera();
        }
        //SAVE
        private void bgwSaveProcesos_DoWork(object sender, DoWorkEventArgs e)
        {
            precarga.AsignastatusProceso("Guardando Información...");
            this.ListaErrores = new List<string> { };
            foreach (int indice in ListaIndices)
            {
                int resultado = 0;
                RetroAlimManObra.CerrarIndice(indice, ref resultado);
                switch (resultado)
                {
                    case 1:
                        this.ListaErrores.Add("Indice: " + indice.ToString() + " - No se ha encontrado este número de órden");
                        break;
                    case 2:
                        this.ListaErrores.Add("Indice: " + indice.ToString() + " - Esta orden ya ha sido previamente cerrada");
                        break;
                }       
            }
            if (this.ListaIndices.Count > 0)
            {
                DataTable dtProcesosActualizarUPPedidos = new DataTable();
                dtProcesosActualizarUPPedidos = RetroAlimManObraPedido.ConsultaProcesosLiberados(int.Parse(txtPedido.Text));
                ActualizaUPPedidos(int.Parse(txtPedido.Text), dtProcesosActualizarUPPedidos);
            }
        }
        void bgwSaveProcesos_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (this.ListaErrores.Count == 0)
                MessageBox.Show("Proceso finalizado con éxito.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                String resultadoFinal = "" ;
                foreach (String error in this.ListaErrores)
                {
                    resultadoFinal+= error + (char)13;
                }
                MessageBox.Show("Existieron errores al procesar: " + (char)13 + resultadoFinal, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            txtPedido.Text = "";
            dgvProcesos.DataSource = null;
            precarga.RemoverEspera();
        }
        #endregion
        #region "Funciones"
        void InhabilitaGridProcesos()
        {
            foreach (DataGridViewRow dr in dgvProcesos.Rows)
            {
                if ((int)dr.Cells["CMT_CHECK"].Value == 1)
                {
                    dr.DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#d6f5d6");
                    dr.Cells["CMT_CHECK"].ReadOnly = true;
                }

            }
        }
        void ActualizaUPPedidos(int Pedido, DataTable Procesos)
        {
            foreach (DataRow dr in Procesos.Rows)
            {
                UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                uppedidosModif.PEDIDO = Pedido;
                switch (dr["PROCESO"].ToString())
                {
                    case "I":
                        uppedidosModif.F_INICIALES = DateTime.Now;
                        break;
                    case "B":
                        uppedidosModif.F_BORDADO = DateTime.Now;
                        break;
                    case "C":
                        uppedidosModif.F_COSTURA= DateTime.Now;
                        break;
                    case "E":
                        uppedidosModif.F_ESTAMPADO= DateTime.Now;
                        break;
                    case "R":
                        uppedidosModif.F_CORTE = DateTime.Now;
                        break;
                }
                uppedidosModif.Modificar(uppedidosModif, "UpPedidos");
            }
        }
        #endregion
    }
}
