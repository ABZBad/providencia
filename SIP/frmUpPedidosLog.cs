using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ulp_bl;

namespace SIP
{
    public partial class frmUpPedidosLog : Form
    {

        private Color colorPar = Color.FromArgb(216, 248, 224);
        private Color colorNon = Color.FromArgb(255, 255, 225);
        public frmUpPedidosLog()
        {
            InitializeComponent();
        }
        public frmUpPedidosLog(int NumperoPedido)
        {
            InitializeComponent();
            txtNumeroPedido.Text = NumperoPedido.ToString();
            btnBuscar_Click(null,new EventArgs());
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            DataTable dtAgrupado = UpPedidosLog.RegresaLog2(UpPedidosLog.RegresaLog(int.Parse(txtNumeroPedido.Text)));
            dgvLogUpPedidos.DataSource = dtAgrupado;
        }

        private void dgvLogUpPedidos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int tranNo = (int) dgvLogUpPedidos.Rows[e.RowIndex].Cells[0].Value;
            if (tranNo%2 == 0)
            {
                dgvLogUpPedidos.Rows[e.RowIndex].DefaultCellStyle.BackColor = colorPar;
            }
            else
            {
                dgvLogUpPedidos.Rows[e.RowIndex].DefaultCellStyle.BackColor = colorNon;
            }
        }

        private void frmUpPedidosLog_Load(object sender, EventArgs e)
        {
            
        }

        private void txtNumeroPedido_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBuscar_Click(sender, new EventArgs());
            }
        }
    }
}
