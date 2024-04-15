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
    public partial class frmDocumentosElectronicos : Form
    {
        int numeroPedido;
        string path = "";
        DataTable dtDocumentos = new DataTable();

        public frmDocumentosElectronicos(int numeroPedido)
        {
            this.numeroPedido = numeroPedido;
            InitializeComponent();
            dgDocumentos.AutoGenerateColumns = false;
        }

        private void frmDocumentosElectronicos_Load(object sender, EventArgs e)
        {
            this.CargaDocumentosPedido();
            this.lblTitulo.Text += " " + this.numeroPedido.ToString();
        }
        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lblPath.Text = openFileDialog1.FileName;
                lblNombreArchivo.Text = openFileDialog1.SafeFileName;
                this.path = openFileDialog1.FileName;
            }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Byte[] file = System.IO.File.ReadAllBytes(this.path);

            ControlPedidos.setAltaDocumentoElectronico(this.numeroPedido, file, txtTipo.Text.Trim(), openFileDialog1.SafeFileName.Trim(), txtObservaciones.Text.Trim(), Globales.UsuarioActual.UsuarioUsuario.ToString());
            MessageBox.Show("El documento se guardó de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.path = "";
            lblNombreArchivo.Text = "";
            lblNombreArchivo.Text = "Sin archivo seleccionado...";
            txtTipo.Text = "";
            txtObservaciones.Text = "";
            this.CargaDocumentosPedido();
        }

        private void CargaDocumentosPedido()
        {
            this.dtDocumentos = ControlPedidos.getDocumentosElectronicos(this.numeroPedido);
            dgDocumentos.DataSource = this.dtDocumentos;
            dgDocumentos.Refresh();
        }

        private void dgDocumentos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int idDocumento = (int)dgDocumentos.Rows[e.RowIndex].Cells["idDocumento"].Value;
            if (dgDocumentos.Columns[e.ColumnIndex].Name == "Ver")
            {
                Reportes.frmVisorPDF frmVisorPDF = new Reportes.frmVisorPDF();
                String tmpPath = System.IO.Path.GetTempFileName().Replace(".tmp", ".pdf");
                Byte[] report = this.dtDocumentos.Select("idDocumento = " + idDocumento).FirstOrDefault().Field<Byte[]>("PDF");

                System.IO.File.WriteAllBytes(tmpPath, report);
                frmVisorPDF.axAcroPDF1.LoadFile(tmpPath);
                frmVisorPDF.axAcroPDF1.setZoom(80);
                frmVisorPDF.Show();
                try
                {
                    System.IO.File.Delete(tmpPath);
                }
                catch { }
            }
            if (dgDocumentos.Columns[e.ColumnIndex].Name == "Eliminar")
            {
                if (MessageBox.Show("¿Seguro que desea dar de baja el documento seleccionado?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    ControlPedidos.setEliminaDocumento(this.numeroPedido, idDocumento);
                    MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.CargaDocumentosPedido();
                }
            }
        }
    }
}
