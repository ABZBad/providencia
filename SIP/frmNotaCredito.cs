using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ulp_bl;
using SIP.Utiles;

namespace SIP
{
    public enum TipoNC
    {
        GENERAR = 0,
        INGRESAR = 1
    };

    public partial class frmNotaCredito : Form
    {
        #region "Variables y constructores"
        private byte[] byteCer;
        private byte[] byteKey;
        String Password;
        String RFCCertificado;
        String NoCER;
        DateTime Desde;
        DateTime Hasta;
        private DataTable dtPedidos;
        private BackgroundWorker bgwPedidos;
        private BackgroundWorker bgwFacturacion;
        private Precarga precarga;
        String busqueda = "";
        TipoNC tipoNC;

        public frmNotaCredito(TipoNC _tipoNC)
        {
            InitializeComponent();
            dgvPedidos.AutoGenerateColumns = false;
            this.dtPedidos = new DataTable();
            this.tipoNC = _tipoNC;
            precarga = new Precarga(this);
            bgwPedidos = new BackgroundWorker();
            bgwPedidos.DoWork += bgwPedidos_DoWork;
            bgwPedidos.RunWorkerCompleted += bgwPedidos_RunWorkerCompleted;
        }

        #endregion
        #region "Workers"
        void bgwPedidos_DoWork(object sender, DoWorkEventArgs e)
        {
            //CARGAMOS LOS PEDIDOS POR FACTURAR
            this.dtPedidos = ulp_bl.CFDIPAC.getFacturasParaNC(this.busqueda);
        }
        void bgwPedidos_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dgvPedidos.DataSource = this.dtPedidos;
            precarga.RemoverEspera();
            bgwPedidos.Dispose();
            if (this.dtPedidos.Rows.Count > 0)
            {
                dgvPedidos.Focus();
            }
            else
            {
                MessageBox.Show("No existen coincidencias con los criterios de búsqueda.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion
        #region "Eventos"
        private void frmNotaCredito_Load(object sender, EventArgs e)
        {
        }
        private void txtBusqueda_Leave(object sender, EventArgs e)
        {
            if (txtBusqueda.Text.Trim().ToUpper() != "")
            {
                this.busqueda = txtBusqueda.Text.Trim().ToUpper();
                precarga.MostrarEspera();
                precarga.AsignastatusProceso("Buscando facturas...");
                bgwPedidos.RunWorkerAsync();
            }
        }
        private void txtBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtBusqueda_Leave(null, null);
            }
        }
        private void dgvPedidos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var dataGridView = sender as DataGridView;
            if (dataGridView.Rows[e.RowIndex].Selected)
            {
                e.CellStyle.Font = new Font(new FontFamily(e.CellStyle.Font.Name), e.CellStyle.Font.Size - 1, FontStyle.Bold);
                // edit: to change the background color:
                //e.CellStyle.SelectionBackColor = Color.Coral;
            }
        }
        private void dgvPedidos_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.Value);
            if (e.Value != null)
            {
                if (e.Value.ToString().ToUpper().Contains(this.busqueda.ToUpper()))
                {
                    e.CellStyle.ForeColor = Color.Blue;
                }
            }
        }
        private void dgvPedidos_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            dgvPedidos.Rows[e.RowIndex].Selected = true;
        }
        private void dgvPedidos_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (this.dtPedidos.Rows[e.RowIndex][0].ToString() != "")
            {
                switch (this.tipoNC)
                {
                    case TipoNC.GENERAR:
                        frmNotaCreditoDetalle frmNotaCreditoDetalle = new frmNotaCreditoDetalle(this.dtPedidos.Rows[e.RowIndex][0].ToString());
                        frmNotaCreditoDetalle.ShowDialog();
                        break;
                    case TipoNC.INGRESAR:
                        frmNotaCreditoIngreso frmNotaCreditoIngreso = new frmNotaCreditoIngreso(this.dtPedidos.Rows[e.RowIndex][0].ToString());
                        frmNotaCreditoIngreso.ShowDialog();
                        break;
                }

            }
        }
        private void dgvPedidos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dgvPedidos.SelectedRows.Count == 1)
                {
                    dgvPedidos_CellMouseDoubleClick(sender,
                        new DataGridViewCellMouseEventArgs(0, dgvPedidos.CurrentRow.Index, 0, 0,
                            new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0)));
                }
            }
        }
        #endregion
        #region "Metodos"
        #endregion
    }
}