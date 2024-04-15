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
    public partial class frmOrdMaquila : Form
    {
        DataSet tablasTallas;
        DataTable tallasSeleccionadas = new DataTable();
        private BackgroundWorker bgw = new BackgroundWorker();
        private PROV01 prov01;
        ErrorProvider errorPClave = new ErrorProvider();
        ErrorProvider errorPCosto = new ErrorProvider();
        ErrorProvider errorPEsq = new ErrorProvider();
        ErrorProvider errorPTotal = new ErrorProvider();
        public frmOrdMaquila()
        {
            InitializeComponent();
            bgw.DoWork += bgw_DoWork;
            bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
            DefineTablaTallasSeleccionadas();
            dgViewTallas1.CellEndEdit += dgViewTallas_CellEndEdit;
            dgViewTallas1.DataError += dgViewTallas_DataError;
            dgViewTallas2.CellEndEdit += dgViewTallas_CellEndEdit;
            dgViewTallas2.DataError += dgViewTallas_DataError;
            dgViewTallas3.CellEndEdit += dgViewTallas_CellEndEdit;
            dgViewTallas3.DataError += dgViewTallas_DataError;
            dgViewTallas4.CellEndEdit += dgViewTallas_CellEndEdit;
            dgViewTallas4.DataError += dgViewTallas_DataError;
        }
        void dgViewTallas_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception is FormatException)
            {
                MessageBox.Show("El valor escrito es incorrecto", "Verifique", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show(e.Exception.Message, "Verifique", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            DataGridView dgv = (DataGridView)sender;
            DataGridViewCell dgvCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];



            TextBox txtCell = (TextBox)dgv.EditingControl;

            txtCell.SelectionStart = 0;
            txtCell.SelectionLength = txtCell.Text.Length;



        }

        void dgViewTallas_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            DataGridViewCell dgvCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];

            if (dgvCell.Value.ToString().Trim() == string.Empty)
            {
                dgvCell.Value = 0;
            }

        }
        private void DefineTablaTallasSeleccionadas()
        {
            tallasSeleccionadas.Columns.Add(new DataColumn("MODELO", typeof(string)));
            tallasSeleccionadas.Columns.Add(new DataColumn("TALLA", typeof(string)));
            tallasSeleccionadas.Columns.Add(new DataColumn("CANTIDAD", typeof(int)));
        }
        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!string.IsNullOrEmpty(prov01.NOMBRE))
            {
                lblNombreProveedor.Text = prov01.NOMBRE;
            }
            else
            {
                lblNombreProveedor.Text = "Proveedor no encontrado!";
            }
        }

        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            prov01 = new PROV01();
            prov01 = prov01.Consultar(txtClaveProveedor.Text, true);
        }

        private void txtClaveProveedor_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtClaveProveedor.Text))
            {
                if (txtClaveProveedor.Text != "0")
                {
                    lblNombreProveedor.Text = "Consultando proveedor...";
                    bgw.RunWorkerAsync();

                }
                else
                {
                    lblNombreProveedor.Text = "";
                }
            }
            else
            {
                lblNombreProveedor.Text = "";
            }
        }

        private void txtModelo_Leave(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtModelo.Text))
            {
                DataTable dataTableInventario = OrdMaquila.RegresaTablaInventario(txtPrefijoModelo.Text + txtModelo.Text);
                tablasTallas = new DataSet();
                tallasSeleccionadas.Clear();
                if (dataTableInventario.Rows.Count > 0)
                {

                    llenaDatosTallas(dataTableInventario);
                    txtCosto.Text = ((double) dataTableInventario.Rows[0]["ULT_COSTO"]).ToString();
                    lblDescripcion.Text = (string) dataTableInventario.Rows[0]["DESCR"];
                }
                else
                {

                    MessageBox.Show(
                        string.Format("No se han encontrado registros con el modelo: {0}{1}", txtPrefijoModelo.Text,
                            txtModelo.Text), "Verifique", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtModelo.Focus();
                }
            }

        }
        private bool llenaDatosTallas(DataTable tallas)
        {

            if (tallas.Rows.Count == 0)
            {
                return false;
            }


            int tot = 0; ;
            tablasTallas = new DataSet();
            //int numeroTablas = ((tallas.Rows.Count - 1) / 10);
            int numeroTablas = ulp_bl.AddPartidasPedi.NumeroDeTablasSegunTotalTallasModificar(tallas);
            dgViewTallas1.DataSource = null;
            dgViewTallas2.DataSource = null;
            dgViewTallas2.Visible = false;
            dgViewTallas3.DataSource = null;
            dgViewTallas3.Visible = false;
            dgViewTallas4.DataSource = null;
            dgViewTallas4.Visible = false;
            for (int i = 0; i < numeroTablas; i++)
            {

                string nombreTabla = "T" + i.ToString();
                tablasTallas.Tables.Add(nombreTabla);
                for (int y = 0; y < 10; y++)
                {
                    int longitudCol;
                    DataColumn columna = new DataColumn();
                    longitudCol = tallas.Rows[tot]["CVE_ART"].ToString().Length;
                    columna.Caption = tallas.Rows[tot]["CVE_ART"].ToString().Substring(9, longitudCol - 9);
                    columna.ColumnName = tallas.Rows[tot]["CVE_ART"].ToString().Substring(9, longitudCol - 9);
                    columna.DefaultValue = 0;
                    columna.DataType = typeof(Int32);
                    tablasTallas.Tables[nombreTabla].Columns.Add(columna);
                    if (y == 9 || (tallas.Rows.Count - 1) == tot)
                    {
                        DataRow registroNuevo = tablasTallas.Tables[nombreTabla].NewRow();
                        tablasTallas.Tables[nombreTabla].Rows.Add(registroNuevo);
                        tot++;
                        break;
                    }
                    tot++;
                }
                if (i == 0)
                {
                    dgViewTallas1.DataSource = tablasTallas.Tables[nombreTabla];
                    DataGridViewCell cell1 = dgViewTallas1.CurrentCell;
                    cell1.Selected = false;
                }
                if (i == 1)
                {
                    dgViewTallas2.Visible = true;
                    dgViewTallas2.DataSource = tablasTallas.Tables[nombreTabla];
                    DataGridViewCell cell2 = dgViewTallas2.CurrentCell;
                    cell2.Selected = false;
                }
                if (i == 2)
                {
                    dgViewTallas3.Visible = true;
                    dgViewTallas3.DataSource = tablasTallas.Tables[nombreTabla];
                    DataGridViewCell cell3 = dgViewTallas3.CurrentCell;
                    cell3.Selected = false;
                }
                if (i == 3)
                {
                    dgViewTallas4.Visible = true;
                    dgViewTallas4.DataSource = tablasTallas.Tables[nombreTabla];
                    DataGridViewCell cell4 = dgViewTallas4.CurrentCell;
                    cell4.Selected = false;
                }
            }
            return true;
        }

        private void dgView_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.Width = 58;
        }

        private void txtModelo_TextChanged(object sender, EventArgs e)
        {

        }
        private int SumaDeColumnas(DataGridView dgv)
        {
            int suma = 0;
            foreach (DataGridViewColumn colum in dgv.Columns)
            {
                if (dgv.Rows[0].Cells[colum.Name].Value != DBNull.Value)
                {
                    suma += Convert.ToInt32(dgv.Rows[0].Cells[colum.Name].Value);
                }
            }
            return suma;
        }
        private int SumaDePrendas()
        {
            int total = SumaDeColumnas(dgViewTallas1) + SumaDeColumnas(dgViewTallas2) + SumaDeColumnas(dgViewTallas3) + SumaDeColumnas(dgViewTallas4);
            return total;
        }
        private void dgViewTallas_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            lblTotal.Text = Convert.ToString(SumaDePrendas());
        }
        private void AsignaTallasSeleccionadasPorGrid()
        {

            tallasSeleccionadas.Clear();

            Queue<DataGridView> colaDgv = new Queue<DataGridView>();

            colaDgv.Enqueue(dgViewTallas1);
            colaDgv.Enqueue(dgViewTallas2);
            colaDgv.Enqueue(dgViewTallas3);
            colaDgv.Enqueue(dgViewTallas4);

            do
            {
                DataGridView dgv = colaDgv.Dequeue();
                foreach (DataGridViewColumn column in dgv.Columns)
                {
                    int cantidad = Convert.ToInt32(dgv.Rows[0].Cells[column.Name].Value);
                    if (cantidad > 0)
                    {
                        var row = tallasSeleccionadas.NewRow();
                        row["MODELO"] = txtPrefijoModelo.Text + txtModelo.Text + column.Name;
                        row["TALLA"] = column.Name;
                        row["CANTIDAD"] = Convert.ToInt32(dgv.Rows[0].Cells[column.Name].Value);
                        tallasSeleccionadas.Rows.Add(row);
                    }
                }
            } while (colaDgv.Count > 0);
            colaDgv = null;
        }
        private void btnProcesar_Click(object sender, EventArgs e)
        {
            Exception ex = null;
            if (DatosValidos())
            {
                AsignaTallasSeleccionadasPorGrid();
                int NumeroOrden = OrdMaquila2.GenerarOrden(
                    tallasSeleccionadas,
                    tallasSeleccionadas.Rows.Count,
                    int.Parse(lblTotal.Text),
                    txtClaveProveedor.Text,
                    int.Parse(txtCosto.Text),
                    int.Parse(txtEsquemaImpuesto.Text),
                    txtObservaciones.Text,
                    1,
                    ref ex
                    );
                if (ex != null)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    txtOM.Text = NumeroOrden.ToString();
                    MessageBox.Show(string.Format("Se genero la orden de compra de maquila {0} ", NumeroOrden), "",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult resp = MessageBox.Show("¿Desea agregar otra Órden de Maquila?", "Confirme",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (resp == DialogResult.Yes)
                    {
                        LimpiaControles();
                        txtClaveProveedor.Focus();
                    }

                }
            }
            else
            {
                MessageBox.Show("Existen datos inválidos, verifique e intente de nuevo", "Verifique",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private bool DatosValidos()
        {
            int errores = 0;
            if (int.Parse(lblTotal.Text) == 0)
            {
                errorPTotal.BlinkStyle = ErrorBlinkStyle.NeverBlink;
                errorPTotal.SetIconAlignment(lblTotal, ErrorIconAlignment.BottomRight);
                errorPTotal.SetError(lblTotal, "El total debe ser mayor a Cero, verifique la cantidad por talla y que el Modelo sea válido");
                errores++;
            }
            else
            {
                errorPTotal.Clear();
            }
            if (string.IsNullOrEmpty(txtClaveProveedor.Text) || txtClaveProveedor.Text == "0")
            {
                errorPClave.BlinkStyle = ErrorBlinkStyle.NeverBlink;
                errorPClave.SetIconAlignment(txtClaveProveedor, ErrorIconAlignment.BottomRight);
                errorPClave.SetError(txtClaveProveedor, "Campo requerido");
                errores++;
            }
            else
            {
                errorPClave.Clear();
            }
            if (string.IsNullOrEmpty(txtCosto.Text))
            {
                errorPCosto.BlinkStyle = ErrorBlinkStyle.NeverBlink;
                errorPCosto.SetIconAlignment(txtCosto, ErrorIconAlignment.BottomRight);
                errorPCosto.SetError(txtCosto, "Campo requerido");
                errores++;
            }
            else
            {
                errorPCosto.Clear();
            }
            if (string.IsNullOrEmpty(txtEsquemaImpuesto.Text) || txtEsquemaImpuesto.Text == "0")
            {
                errorPEsq.BlinkStyle = ErrorBlinkStyle.NeverBlink;
                errorPEsq.SetIconAlignment(txtEsquemaImpuesto, ErrorIconAlignment.BottomRight);
                errorPEsq.SetError(txtEsquemaImpuesto, "Campo requerido");
                errores++;
            }
            else
            {
                errorPEsq.Clear();
            }
            if (errores > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void LimpiaControles()
        {
            dgViewTallas1.DataSource = null;
            dgViewTallas2.DataSource = null;
            dgViewTallas3.DataSource = null;
            dgViewTallas4.DataSource = null;
            txtOM.Text = "";
            tallasSeleccionadas.Clear();
            txtClaveProveedor.Text = "";
            txtEsquemaImpuesto.Text = "";
            txtCosto.Text = "";
            txtModelo.Text = "";
            lblTotal.Text = "0";
            lblNombreProveedor.Text = "";
            lblDescripcion.Text = "";
            txtObservaciones.Text = "";
        }
    }
}
