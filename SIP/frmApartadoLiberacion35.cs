using System;
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
    public partial class frmApartadoLiberacion35 : Form
    {
        int Pedido;
        DataTable dtDetallePedido;
        private BackgroundWorker bgw;
        private Precarga precarga;
        private Exception ex;
        private String Error = String.Empty;
        private List<String> Transferencias;

        public frmApartadoLiberacion35(int Pedido)
        {
            InitializeComponent();
            this.Pedido = Pedido;
            precarga = new Precarga(this);
            dtDetallePedido = new DataTable();
            dgvDetallePedido.AutoGenerateColumns = false;
            this.Transferencias = new List<string> { };
        }

        private void frmApartadoLiberacion35_Load(object sender, EventArgs e)
        {
            this.ex = new Exception();
            this.dtDetallePedido = ApartadoLiberacion35.getDetallePedidoApartado(this.Pedido, ref ex);
            if (this.dtDetallePedido != null)
            {
                dgvDetallePedido.DataSource = this.dtDetallePedido;
                if (dtDetallePedido.Rows.Count > 0)
                {
                    if (dtDetallePedido.Rows[0]["CONAPARTADO"].ToString()=="1")
                        lblNota.Text = "NOTA: El Pedido ya cuenta con artículos apartados en el Almacén 35, por lo tanto no podra ser tranferido a traves del Sistema iSIP.";
                    else
                        lblNota.Text = "NOTA: El Pedido no cuenta con artículos apartiados en el Almacén 35.";


                    if (this.dtDetallePedido.AsEnumerable().Sum(item => item.Field<Double>("PORSURTIR")) == 0)
                        btnLiberar.Enabled = true;
                    else
                        btnLiberar.Enabled = false;

                }
            }
        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            this.Error = "";
            this.Transferencias = new List<string> { };
            foreach (DataGridViewRow dr in dgvDetallePedido.Rows)
            {
                if (dr.Cells["_TRANSFERENCIA"].Value != null)
                {
                    if ((int.Parse(dr.Cells["_TRANSFERENCIA"].Value.ToString()) > int.Parse(dr.Cells["_EXISTENCIAS"].Value.ToString()) || (int.Parse(dr.Cells["_TRANSFERENCIA"].Value.ToString()) > int.Parse(dr.Cells["_POR_SURTIR"].Value.ToString()))))
                    {
                        this.Transferencias.Clear();
                        this.Error = "No se puede transferir mas mercancia que la existencia, o que la necesaria para surtir el pedido.";
                    }
                    else
                        Transferencias.Add(dr.Cells["_CVE_ART"].Value.ToString() + "|" + dr.Cells["_ORIGEN"].Value.ToString() + "|" + dr.Cells["_TRANSFERENCIA"].Value.ToString());
                }
            }

            bgw = new BackgroundWorker();
            bgw.DoWork += bgw_DoWork;
            bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
            precarga.MostrarEspera();
            bgw.RunWorkerAsync();     
        }

        //APARTADO
        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            if (this.Error != "")
                return;

            if (this.Transferencias.Count == 0)
            {
                this.Error = "No hay transferencias para procesar.";
                return;
            }
            
            //OBTENEMOS LA CLAVE DE FOLIO AGRUPADOR
            int ClaveFolio = FacturasPorAnticipado.getSiguienteFolioAgrupador();
            //YA QUE TENEMOS LA CLAVE POR CADA ARTICULO GENERAMOS LA TRANSFERENCIA VIRTUAL
            foreach (String _transferencia in this.Transferencias)
            {
                ex = new Exception();
                ApartadoLiberacion35.setTransferenciaApartadoArticulo("P" + this.Pedido.ToString(), _transferencia.Split('|')[0].ToString(), int.Parse(_transferencia.Split('|')[1].ToString()), int.Parse(_transferencia.Split('|')[2].ToString()), ClaveFolio, txtFactApa.Text + txtReferencia.Text.Trim(), ref ex);
                if (ex != null)
                    this.Error += ex.Message;
            }
            FacturasPorAnticipado.setSiguienteFolioAgrupador(ClaveFolio);
        }
        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (this.Error == "")
            {
                MessageBox.Show("La Transferencia se procesó de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error al procesar la transferencia. " + this.Error, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //DAMOS REVERSA AL PROCESO POR NUMERO DE PEDIDO
            }
            precarga.RemoverEspera();
            frmApartadoLiberacion35_Load(null, null);
        }
        //LIBERACION
        void bgw_DoWork2(object sender, DoWorkEventArgs e)
        {
            this.Error = "";
            this.ex = new Exception();

            DataTable dtValidacion = ApartadoLiberacion35.ValidaLiberacionPedido("P" + this.Pedido.ToString(), ref ex);
            if (ex != null)
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

            ApartadoLiberacion35.setLiberaPedidoApartado("P" + this.Pedido.ToString(), ref ex);
            if (ex != null)
            {
                this.Error = ex.Message;
            }           
        }
        void bgw_RunWorkerCompleted2(object sender, RunWorkerCompletedEventArgs e)
        {
            if (this.Error == "")
            {
                MessageBox.Show("El Pedido se liberó de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error al liberar el pedido. " + this.Error, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //DAMOS REVERSA AL PROCESO POR NUMERO DE PEDIDO
            }
            precarga.RemoverEspera();
            frmApartadoLiberacion35_Load(null, null);
        }


        private void dgvDetallePedido_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDetallePedido[e.ColumnIndex, e.RowIndex].Value != null)
            {
                //VALIDAMOS QUE LA TRANSFERENCIA NO SOBRE PASE LA EXISTENCIAS
                if (int.Parse(dgvDetallePedido[7, e.RowIndex].Value.ToString()) < int.Parse(dgvDetallePedido[e.ColumnIndex, e.RowIndex].Value.ToString()))
                {
                    MessageBox.Show("La transferencia no puede sobrepasar las existencias del Almacen Origen", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void dgvDetallePedido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void dgvDetallePedido_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvDetallePedido.CurrentCell.ColumnIndex == 7)
            {
                e.Control.KeyPress += new KeyPressEventHandler(dgvDetallePedido_KeyPress);
            }
        }

        private void btnLiberar_Click(object sender, EventArgs e)
        {
            bgw = new BackgroundWorker();
            bgw.DoWork += bgw_DoWork2;
            bgw.RunWorkerCompleted += bgw_RunWorkerCompleted2;
            precarga.MostrarEspera();
            bgw.RunWorkerAsync();     
        }
    }
}
