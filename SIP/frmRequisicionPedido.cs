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
    public partial class frmRequisicionPedido : Form
    {
        DataSet dsResult = new DataSet();
        public int Pedido { get; set; }
        public bool Procesada { get; set; }
        public frmRequisicionPedido(int pedido)
        {
            InitializeComponent();
            this.dgvDetalle.AutoGenerateColumns = false;
            this.dgvMP.AutoGenerateColumns = false;
            this.dgvComprado.AutoGenerateColumns = false;
            this.Pedido = pedido;
        }

        private void frmRequisicionPedido_Load(object sender, EventArgs e)
        {
            this.lblNumeroPedido.Text = this.Pedido.ToString().Trim();
            this.dsResult = GetRequisicionPedido(this.Pedido);
            if (dsResult.Tables.Count > 0)
            {
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    this.SetDatosDetalle(dsResult.Tables[0]);
                    this.SetDatosMP(dsResult.Tables[1]);
                    this.SetDatosComprado(dsResult.Tables[2]);
                }
                else
                {
                    this.btnImprimir.Enabled = false;
                }
            }
        }

        public DataSet GetRequisicionPedido(int pedido)
        {
            return RequisicionPedido.GetRequisicionPedido(pedido);
        }

        public void SetDatosDetalle(DataTable dt)
        {
            this.dgvDetalle.DataSource = dt;
        }
        public void SetDatosMP(DataTable dt)
        {
            this.dgvMP.DataSource = dt;
        }
        public void SetDatosComprado(DataTable dt)
        {
            this.dgvComprado.DataSource = dt;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            bool aplicaNotificacionCP = false;
            bool aplicaNotificacionCA = false;
            // 1. Creamos XML de MP
            string _xmlComponentes = "<componentes>";
            foreach (DataGridViewRow _row in dgvMP.Rows)
            {
                bool isSelected = Convert.ToBoolean(_row.Cells["MP_Seleccionar"].Value);
                if (isSelected)
                {
                    aplicaNotificacionCP = true;
                    _xmlComponentes += "<componente>";
                    _xmlComponentes += String.Format("<componente>{0}</componente>", _row.Cells["MP_Componente"].Value.ToString());
                    _xmlComponentes += String.Format("<descripcion>{0}</descripcion>", _row.Cells["MP_Descripcion"].Value.ToString());
                    _xmlComponentes += String.Format("<cantidad>{0}</cantidad>", _row.Cells["MP_Total"].Value.ToString());
                    _xmlComponentes += "</componente>";
                }
            }
            _xmlComponentes += "</componentes>";
            // 1. Creamos XML de Modelos
            string _xmlModelos = "<modelos>";
            foreach (DataGridViewRow _row in dgvComprado.Rows)
            {
                bool isSelected = Convert.ToBoolean(_row.Cells["Comprado_Seleccionar"].Value);
                if (isSelected)
                {
                    aplicaNotificacionCA = true;
                    _xmlModelos += "<modelo>";
                    _xmlModelos += String.Format("<modelo>{0}</modelo>", _row.Cells["Comprado_Modelo"].Value.ToString());
                    _xmlModelos += String.Format("<descripcion>{0}</descripcion>", _row.Cells["Comprado_Descripcion"].Value.ToString());
                    _xmlModelos += String.Format("<cantidad>{0}</cantidad>", _row.Cells["Comprado_Total"].Value.ToString());
                    _xmlModelos += "</modelo>";
                }
            }
            _xmlModelos += "</modelos>";
            RequisicionPedido.AltaRequisicion(this.Pedido, _xmlComponentes, _xmlModelos);
            MessageBox.Show("Requisición generadade forma correcta", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Procesada = true;
            this.Close();
        }

        private void GeneraRequisicion()
        {

        }
    }
}
