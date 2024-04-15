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
    public partial class AddPartidasPediListaPrecio : Form
    {
        private List<PreciosXModelo> preciosXModelo;
        private string modelo = "";
        public double PRECIO1 { get; set; }
        public AddPartidasPediListaPrecio(string Modelo, List<PreciosXModelo> PreciosXModelo)
        {
            InitializeComponent();
            preciosXModelo = PreciosXModelo;
            modelo = Modelo;
        }

        private void AddPartidasPediListaPrecio_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = preciosXModelo;
            lblModelo.Text = modelo;
        }

        private void AddPartidasPediListaPrecio_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (PRECIO1 == 0)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            PRECIO1 = Convert.ToDouble(dataGridView1.SelectedRows[0].Cells[1].Value);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOk_Click(sender, new EventArgs());
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            btnOk_Click(sender, new EventArgs());
        }
    }
}
