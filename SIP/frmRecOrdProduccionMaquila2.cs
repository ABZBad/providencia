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
    public partial class frmRecOrdProduccionMaquila2 : Form
    {
        string idRefAcumulado = "";
        int totalOK = 0;
        int totalDef = 0;
        DataSet tablasTallas;
        DataTable datos;
        private Precarga precarga;
        private BackgroundWorker bgw;
        public frmRecOrdProduccionMaquila2()
        {
            InitializeComponent();
            precarga = new Precarga(this);
        }

        private void frmRecOrdProduccionMaquila2_Load(object sender, EventArgs e)
        {
            /*
            this.dgViewTallas.Rows.Add();
            this.dgViewTallas.Rows.Add();
            this.dgViewTallas.Rows.Add(0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0);
            this.dgViewTallas1.Rows.Add();
            this.dgViewTallas1.Rows.Add();
            this.dgViewTallas1.Rows.Add(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            this.dgViewTallas2.Rows.Add();
            this.dgViewTallas2.Rows.Add();
            this.dgViewTallas2.Rows.Add(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

            for (int i = 0; i < dgViewTallas.Columns.Count; i++)
            {
                this.dgViewTallas.Rows[0].Cells[i].ValueType = typeof(int);
                this.dgViewTallas.Rows[1].Cells[i].ValueType = typeof(int);
                this.dgViewTallas.Rows[2].Cells[i].Style.BackColor = System.Drawing.SystemColors.Control;
                //this.dgViewTallas.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                this.dgViewTallas.Rows[2].Cells[i].ReadOnly = true;

                this.dgViewTallas1.Rows[0].Cells[i].ValueType = typeof(int);
                this.dgViewTallas1.Rows[1].Cells[i].ValueType = typeof(int);
                this.dgViewTallas1.Rows[2].Cells[i].Style.BackColor = System.Drawing.SystemColors.Control;
                this.dgViewTallas1.Rows[2].Cells[i].ReadOnly = true;

                this.dgViewTallas2.Rows[0].Cells[i].ValueType = typeof(int);
                this.dgViewTallas2.Rows[1].Cells[i].ValueType = typeof(int);
                this.dgViewTallas2.Rows[2].Cells[i].Style.BackColor = System.Drawing.SystemColors.Control;
                this.dgViewTallas2.Rows[2].Cells[i].ReadOnly = true;
            }
            */
        }
        #region Funcionalidades generales
        private void CalculaTotales()
        {
            totalOK = 0;
            totalDef = 0;
            for (int i = 0; i < dgViewTallas.Columns.Count; i++)
            {
                totalOK = totalOK + Convert.ToInt32(dgViewTallas[i, 0].Value);
                totalDef = totalDef + Convert.ToInt32(dgViewTallas[i, 1].Value);
            }
            for (int i = 0; i < dgViewTallas1.Columns.Count; i++)
            {
                totalOK = totalOK + Convert.ToInt32(dgViewTallas1[i, 0].Value);
                totalDef = totalDef + Convert.ToInt32(dgViewTallas1[i, 1].Value);
            }
            for (int i = 0; i < dgViewTallas2.Columns.Count; i++)
            {
                totalOK = totalOK + Convert.ToInt32(dgViewTallas2[i, 0].Value);
                totalDef = totalDef + Convert.ToInt32(dgViewTallas2[i, 1].Value);
            }
            for (int i = 0; i < dgViewTallas3.Columns.Count; i++)
            {
                totalOK = totalOK + Convert.ToInt32(dgViewTallas3[i, 0].Value);
                totalDef = totalDef + Convert.ToInt32(dgViewTallas3[i, 1].Value);
            }
            lblTotalOK.Text = "Total OK: " + totalOK.ToString();
            lblTotalDefectuosos.Text = "Total Defectuoso: " + totalDef.ToString();
        }

        private void LimpiaInfo()
        {
            lblClave.Text = "";
            lblProveedor.Text = "";
            lblModelo.Text = "";
            lblDescripcion.Text = "";
            lblTotalOK.Text = "";
            lblTotalDefectuosos.Text = "";
            btnProcesar.Enabled = false;
            txtOProd.Text = "";
            txtOMaquila.Text = "";
            txtOProd.Focus();
            totalOK = 0;
            totalDef = 0;
            tablasTallas.Clear();
            datos.Clear();
            dgViewTallas.DataSource = null;
            dgViewTallas1.DataSource = null;
            dgViewTallas2.DataSource = null;
            dgViewTallas3.DataSource = null;
        }
        #endregion Funcionalidades generales
        
        #region Metodos de llenado y guardado de info
        private void btnBuscarOPyOC_Click(object sender, EventArgs e)
        {
            datos = new DataTable();
            Cursor = Cursors.WaitCursor;
            datos = RecOrdProduccionMaquila.DevuelveDatosBusqueda(txtOProd.Text, txtOMaquila.Text, txtAlmacen.Text);
            if (datos.Rows.Count > 0)
            {
                lblClave.Text = datos.Rows[0]["CLAVE"].ToString();
                lblProveedor.Text = datos.Rows[0]["NOMBRE"].ToString();
                lblModelo.Text = datos.Rows[0]["PRODUCTO"].ToString();
                lblDescripcion.Text = datos.Rows[0]["DESCR"].ToString();
                txtCostoConfeccion.Text = datos.Rows[0]["COST"].ToString();
                llenaDatosGrids(datos);
                btnProcesar.Enabled = true;
                Cursor = Cursors.Default;
            }
            else
            {
                Cursor = Cursors.Default;
                MessageBox.Show("Orden no liberada, ya recibida o almacen erróneo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtOMaquila.Text = "";
                txtAlmacen.Text = "1";
                txtCostoConfeccion.Text = "0";
                txtOProd.Text = "";
                txtOProd.Focus();
            }
        }

        private void llenaDatosGrids(DataTable tallas)
        {
            int tot = 0;
            tablasTallas = new DataSet();
            //int numeroTablas = ((tallas.Rows.Count - 1) / 10);
            int numeroTablas = ulp_bl.AddPartidasPedi.NumeroDeTablasSegunTotalTallasModificar(tallas);
            dgViewTallas.DataSource = null;
            dgViewTallas1.DataSource = null;
            dgViewTallas2.DataSource = null;
            dgViewTallas3.DataSource = null;
            for (int i = 0; i < numeroTablas; i++)
            {

                string nombreTabla = "T" + i.ToString();
                tablasTallas.Tables.Add(nombreTabla);
                DataRow registroNuevo3 = tablasTallas.Tables[nombreTabla].NewRow();
                for (int y = 0; y < 10; y++)
                {
                    int longitudCol;
                    DataColumn columna = new DataColumn();
                    longitudCol = tallas.Rows[tot]["PRODUCTO"].ToString().Length;
                    columna.Caption = tallas.Rows[tot]["PRODUCTO"].ToString().Substring(8, longitudCol - 8);
                    columna.ColumnName = tallas.Rows[tot]["PRODUCTO"].ToString().Substring(8, longitudCol - 8);
                    columna.DefaultValue = 0;
                    columna.DataType = typeof(Int32);
                    tablasTallas.Tables[nombreTabla].Columns.Add(columna);
                    registroNuevo3[columna.ColumnName] = Convert.ToInt32(tallas.Rows[tot]["PXR"].ToString());
                    if (y == 9 || (tallas.Rows.Count - 1) == tot)
                    {
                        DataRow registroNuevo = tablasTallas.Tables[nombreTabla].NewRow();
                        DataRow registroNuevo2 = tablasTallas.Tables[nombreTabla].NewRow();
                        tablasTallas.Tables[nombreTabla].Rows.Add(registroNuevo);
                        tablasTallas.Tables[nombreTabla].Rows.Add(registroNuevo2);
                        tablasTallas.Tables[nombreTabla].Rows.Add(registroNuevo3);
                        tot++;
                        break;
                    }
                    tot++;
                }
                if (i == 0)
                {
                    dgViewTallas.DataSource = tablasTallas.Tables[nombreTabla];
                    DataGridViewCell cell1 = dgViewTallas.CurrentCell;
                    cell1.Selected = false;
                }
                if (i == 1)
                {
                    dgViewTallas1.DataSource = tablasTallas.Tables[nombreTabla];
                    DataGridViewCell cell2 = dgViewTallas1.CurrentCell;
                    cell2.Selected = false;
                }
                if (i == 2)
                {
                    dgViewTallas2.DataSource = tablasTallas.Tables[nombreTabla];
                    DataGridViewCell cell3 = dgViewTallas2.CurrentCell;
                    cell3.Selected = false;
                }
                if (i == 3)
                {
                    dgViewTallas3.DataSource = tablasTallas.Tables[nombreTabla];
                    DataGridViewCell cell4 = dgViewTallas3.CurrentCell;
                    cell4.Selected = false;
                }
            }
            for (int i = 0; i < dgViewTallas.Columns.Count; i++)
            {
                this.dgViewTallas.Rows[2].Cells[i].Style.BackColor = System.Drawing.SystemColors.Control;
                this.dgViewTallas.Rows[2].Cells[i].ReadOnly = true;
                this.dgViewTallas.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dgViewTallas.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            }
            for (int i = 0; i < dgViewTallas1.Columns.Count; i++)
            {
                this.dgViewTallas1.Rows[2].Cells[i].Style.BackColor = System.Drawing.SystemColors.Control;
                this.dgViewTallas1.Rows[2].Cells[i].ReadOnly = true;
                this.dgViewTallas1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dgViewTallas1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            }
            for (int i = 0; i < dgViewTallas2.Columns.Count; i++)
            {
                this.dgViewTallas2.Rows[2].Cells[i].Style.BackColor = System.Drawing.SystemColors.Control;
                this.dgViewTallas2.Rows[2].Cells[i].ReadOnly = true;
                this.dgViewTallas2.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dgViewTallas2.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            }
            for (int i = 0; i < dgViewTallas3.Columns.Count; i++)
            {
                this.dgViewTallas3.Rows[2].Cells[i].Style.BackColor = System.Drawing.SystemColors.Control;
                this.dgViewTallas3.Rows[2].Cells[i].ReadOnly = true;
                this.dgViewTallas3.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dgViewTallas3.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            }

        }

        private void ProcesaPrendas()
        {
            int idTot = 0;
            for (int i = 0; i < tablasTallas.Tables.Count; i++)
            {
                for (int y = 0; y < tablasTallas.Tables[i].Columns.Count; y++)
                {
                    if ((Convert.ToInt32(tablasTallas.Tables[i].Rows[0][y].ToString()) + Convert.ToInt32(tablasTallas.Tables[i].Rows[1][y].ToString())) > 0)
                    {
                        GuardarTallas(i, y, idTot);
                    }
                    idTot++;
                }
            }
        }

        private void GuardarInfoBase()
        {
            RecOrdProduccionMaquila guarda = new RecOrdProduccionMaquila();
            guarda.GuardaRegistroProc1(Convert.ToInt32(txtAlmacen.Text), lblClave.Text, txtOMaquila.Text,
                Convert.ToDecimal(txtCostoConfeccion.Text), totalOK, (totalDef + totalOK), txtEsquemaImp.Text);
        }

        private void GuardarTallas(int idTabla, int idCol, int idDatos)
        {
            RecOrdProduccionMaquila guarda = new RecOrdProduccionMaquila();
            guarda.GuardaRegistroProc(Convert.ToInt32(txtAlmacen.Text), Convert.ToInt32(datos.Rows[idDatos]["NUM_REG"].ToString()),
                datos.Rows[idDatos]["REFERENCIA"].ToString(), datos.Rows[idDatos]["PRODUCTO"].ToString(),
                Convert.ToInt32(tablasTallas.Tables[idTabla].Rows[0][idCol].ToString()),
                Convert.ToInt32(tablasTallas.Tables[idTabla].Rows[1][idCol].ToString()), txtCodDefectuosos.Text, txtOMaquila.Text,
                Convert.ToDecimal(txtCostoConfeccion.Text), (totalDef + totalOK), txtEsquemaImp.Text, idDatos + 1, txtPrefijo.Text);
        }
        private void btnProcesar_Click(object sender, EventArgs e)
        {
            //VALIDAMOS QUE EL ALMACEN SELECCIONADO UNICAMENTE APLIQUE PARA EL 1 Y EL 32
            if (txtAlmacen.Text.Trim() != "1" && txtAlmacen.Text.Trim() != "32" && txtAlmacen.Text.Trim() != "3")
            {
                MessageBox.Show("Por seguridad, unicamente se permite el uso de almacenes 1, 3 y 32", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bgw = new BackgroundWorker();
            bgw.DoWork += bgw_DoWork;
            bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;

            precarga.MostrarEspera();

            bgw.RunWorkerAsync();
        }

        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            precarga.RemoverEspera();
            if (MessageBox.Show("Desea dar de baja otra orden ?", "Recepcion de ordenes de produccion y maquila", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                LimpiaInfo();
                txtOProd.Focus();
            }
            else
            {
                if (
                    MessageBox.Show("Desea imprimir el recibo de la recepción ? ",
                        "Impresion de recibo de orden de maquila", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                    DialogResult.Yes)
                {
                    string ruta = Path.GetTempFileName().Replace(".tmp", ".xls");
                    RecOrdProduccionMaquila.GeneraArchivoExcel(
                        RecOrdProduccionMaquila.DevuelveDatosXls(idRefAcumulado), ruta);
                    Utiles.FuncionalidadesFormularios.MostrarExcel(ruta);
                    this.Close();
                }
                else
                {
                    this.Close();
                }
            }
        }

        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            //guarda info base
            GuardarInfoBase();
            //guarda info de cada celda que tenga cada grid
            ProcesaPrendas();
            idRefAcumulado = idRefAcumulado + "(" + txtOProd.Text + ")";
        }
        #endregion Metodos de llenado y guardado de info

        #region Funcionalidades Grids
        private void dgViewTallas_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            DataGridView dgv = (DataGridView)sender;

            DataGridViewCell dgvCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];

            if (dgvCell.Value.ToString().Trim() == string.Empty)
            {
                dgvCell.Value = 0;
            }

            int totEscrito = Convert.ToInt32(dgViewTallas.Rows[0].Cells[e.ColumnIndex].Value) + Convert.ToInt32(dgViewTallas.Rows[1].Cells[e.ColumnIndex].Value);
            int toPendientes = Convert.ToInt32(dgViewTallas.Rows[2].Cells[e.ColumnIndex].Value);
            if (totEscrito > toPendientes)
            {
                dgViewTallas.Rows[0].Cells[e.ColumnIndex].Value = 0;
                dgViewTallas.Rows[1].Cells[e.ColumnIndex].Value = 0;
            }
            
            CalculaTotales();
        }

        private void dgViewTallas1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            DataGridViewCell dgvCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];

            if (dgvCell.Value.ToString().Trim() == string.Empty)
            {
                dgvCell.Value = 0;
            }

            int totEscrito = Convert.ToInt32(dgViewTallas1.Rows[0].Cells[e.ColumnIndex].Value) + Convert.ToInt32(dgViewTallas1.Rows[1].Cells[e.ColumnIndex].Value);
            int toPendientes = Convert.ToInt32(dgViewTallas1.Rows[2].Cells[e.ColumnIndex].Value);
            if (totEscrito > toPendientes)
            {
                dgViewTallas1.Rows[0].Cells[e.ColumnIndex].Value = 0;
                dgViewTallas1.Rows[1].Cells[e.ColumnIndex].Value = 0;
            }
            CalculaTotales();
        }

        private void dgViewTallas2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            DataGridViewCell dgvCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];

            if (dgvCell.Value.ToString().Trim() == string.Empty)
            {
                dgvCell.Value = 0;
            }

            int totEscrito = Convert.ToInt32(dgViewTallas2.Rows[0].Cells[e.ColumnIndex].Value) + Convert.ToInt32(dgViewTallas2.Rows[1].Cells[e.ColumnIndex].Value);
            int toPendientes = Convert.ToInt32(dgViewTallas2.Rows[2].Cells[e.ColumnIndex].Value);
            if (totEscrito > toPendientes)
            {
                dgViewTallas2.Rows[0].Cells[e.ColumnIndex].Value = 0;
                dgViewTallas2.Rows[1].Cells[e.ColumnIndex].Value = 0;
            }
            CalculaTotales();
        }

        private void dgViewTallas3_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            DataGridViewCell dgvCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];

            if (dgvCell.Value.ToString().Trim() == string.Empty)
            {
                dgvCell.Value = 0;
            }

            int totEscrito = Convert.ToInt32(dgViewTallas3.Rows[0].Cells[e.ColumnIndex].Value) + Convert.ToInt32(dgViewTallas3.Rows[1].Cells[e.ColumnIndex].Value);
            int toPendientes = Convert.ToInt32(dgViewTallas3.Rows[2].Cells[e.ColumnIndex].Value);
            if (totEscrito > toPendientes)
            {
                dgViewTallas3.Rows[0].Cells[e.ColumnIndex].Value = 0;
                dgViewTallas3.Rows[1].Cells[e.ColumnIndex].Value = 0;
            }
            CalculaTotales();
        }

        private void dgViewTallas_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Debe escribir sólo números.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void dgViewTallas1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Debe escribir sólo números.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void dgViewTallas2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Debe escribir sólo números.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void dgViewTallas3_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Debe escribir sólo números.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void dgViewTallas_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.Width = 58;
        }

        private void dgViewTallas1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.Width = 58;
        }

        private void dgViewTallas2_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.Width = 58;
        }

        private void dgViewTallas3_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.Width = 58;
        }

        #endregion Funcionalidades Grids
    }
}