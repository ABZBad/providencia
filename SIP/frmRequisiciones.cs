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

namespace SIP
{
    public partial class frmRequisiciones : Form
    {
        DataTable dtRequisiciones = new DataTable();
        int tipo = 0;
        public frmRequisiciones()
        {
            InitializeComponent();
            this.dgvRequisiciones.AutoGenerateColumns = false;
        }

        private void frmRequisiciones_Load(object sender, EventArgs e)
        {
            if (Globales.UsuarioActual.UsuarioArea == "CA") { this.tipo = 2; }
            if (Globales.UsuarioActual.UsuarioArea == "CP") { this.tipo = 1; }
            if (Globales.UsuarioActual.UsuarioArea == "TD") { this.tipo = 3; }
            dtRequisiciones = this.CargaRequisicionesPorTipo(tipo);
            this.dgvRequisiciones.DataSource = dtRequisiciones;
        }
        private DataTable CargaRequisicionesPorTipo(int tipo)
        {
            DataTable dtResult = new DataTable();
            dtResult = RequisicionPedido.GetRequisicionTipo(tipo);
            return dtResult;
        }

        private void dgvRequisiciones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvRequisiciones.Columns[e.ColumnIndex].Name == "GV_Ver")
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    int pedido = int.Parse(dgvRequisiciones["GV_Pedido", e.RowIndex].Value.ToString());
                    Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Requisicion, pedido);
                    frmReportes.ShowDialog();
                }
            }
        }

        private void dgvRequisiciones_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnNotificarRecepcion_Click(object sender, EventArgs e)
        {
            FlujoControlNotificaciones oNotificaicion;
            foreach (DataGridViewRow _row in dgvRequisiciones.Rows)
            {
                bool isSelected = Convert.ToBoolean(_row.Cells["GV_Seleccionar"].Value);
                if (isSelected)
                {
                    oNotificaicion = new FlujoControlNotificaciones(Globales.UsuarioActual.UsuarioUsuario, "CP", "Recepción de requisición", String.Format("Requisición procesada para el pedido {0}.", _row.Cells["GV_Pedido"].Value.ToString()));
                    oNotificaicion.InsertaNotificacion();
                    RequisicionPedido.ActualizaFechaLiberacion(int.Parse(_row.Cells["GV_Pedido"].Value.ToString()));
                }
            }
            MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dtRequisiciones = this.CargaRequisicionesPorTipo(this.tipo);
            this.dgvRequisiciones.DataSource = dtRequisiciones;
        }
    }
}
