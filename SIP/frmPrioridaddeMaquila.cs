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
using System.IO;

namespace SIP
{
    public partial class frmPrioridaddeMaquila : Form
    {
        private Precarga precarga;
        private BackgroundWorker bgw;
        public frmPrioridaddeMaquila()
        {
            InitializeComponent();
            precarga = new Precarga(this);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            LlenaDatos();
            Cursor = Cursors.Default;
        }
        private void LlenaDatos()
        {
            DataTable datos = new DataTable();
            datos = PrioridaddeMaquila.DevuelveDatosBusqueda(txtProveedor.Text, txtPrefijo.Text);
            dataGridView1.DataSource = datos;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            bgw = new BackgroundWorker();
            bgw.DoWork += bgw_DoWork;
            bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;

            precarga.MostrarEspera();

            bgw.RunWorkerAsync();

        }

        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            precarga.RemoverEspera();
        }

        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            string ruta = Path.GetTempFileName().Replace(".tmp", ".xls");
            PrioridaddeMaquila.GeneraArchivoExcel(PrioridaddeMaquila.DevuelveDatosBusquedaXls(txtProveedor.Text, txtPrefijo.Text), ruta);
            //System.Diagnostics.Process proc = new System.Diagnostics.Process();
            //System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(ruta);
            //psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized;            
            //System.Diagnostics.Process.Start(psi);
            FuncionalidadesFormularios.MostrarExcel(ruta);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmInputBox input = new frmInputBox();
            input.Text = "SIP";
            input.lblTitulo.Text = string.Format("Prioridad para: {0}", dataGridView1.Rows[e.RowIndex].Cells["OC"].Value.ToString());
            DialogResult respuesta = input.ShowDialog();
            if (respuesta==DialogResult.OK)
            {
                string paramRes = input.NTxtOrden.Text;
                if (paramRes!="")
                {
                    PrioridaddeMaquila.ActualizaPrioridad(dataGridView1.Rows[e.RowIndex].Cells["OC"].Value.ToString(), Convert.ToInt32(paramRes));
                    //dataGridView1.Rows[e.RowIndex].Cells[5].Value = Convert.ToInt32(paramRes);
                    btnBuscar_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Es necesario capturar un número de Pedido.");
                }
                //Es necesario capturar un número de Pedido.
            }
        }
    }
}
