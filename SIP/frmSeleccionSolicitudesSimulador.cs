using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SIP
{
    public partial class frmSeleccionSolicitudesSimulador : Form
    {
        DataTable dtSolicitudes;
        public frmSeleccionSolicitudesSimulador(DataTable _dtSolicitudes)
        {
            this.dtSolicitudes = _dtSolicitudes;
            InitializeComponent();
        }

        private void frmSeleccionSolicitudesSimulador_Load(object sender, EventArgs e)
        {
            dgvSolicitudes.DataSource = this.dtSolicitudes;
            dgvSolicitudes.Refresh();
        }

        private void dgvSolicitudes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvSolicitudes.Columns[e.ColumnIndex].Name == "Simulador")
            {
                frmSimuladorCostos frmSimulador = new frmSimuladorCostos(ulp_bl.Enumerados.TipoSimulador.SimuladorDeCostos, dgvSolicitudes.CurrentRow.Cells["Codigo"].Value.ToString());
                frmSimulador.Show();
            }
        }
    }
}
