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
    public partial class frmTransferenciaXModelo : Form
    {
        DataSet tablasTallas;
        int total = 0;
        int agrupador = 0;


        public frmTransferenciaXModelo()
        {
            InitializeComponent();
        }

        private void txtDestino_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmTransferenciaXModelo_Load(object sender, EventArgs e)
        {

            dgViewTallas1.DataError += dgvDataError;
            dgViewTallas2.DataError += dgvDataError;
            dgViewTallas3.DataError += dgvDataError;
            dgViewTallas4.DataError += dgvDataError;

            dgViewTallas1.CellEnter += dgvCellEnter;
            dgViewTallas2.CellEnter += dgvCellEnter;
            dgViewTallas3.CellEnter += dgvCellEnter;
            dgViewTallas4.CellEnter += dgvCellEnter;
            agrupador = TransferenciaPorModelo.incrementaAgrupador();
            /*
            this.dgViewTallas1.Rows.Add(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            this.dgViewTallas1.Rows.Add();
            this.dgViewTallas2.Rows.Add(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            this.dgViewTallas2.Rows.Add();
            this.dgViewTallas3.Rows.Add(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            this.dgViewTallas3.Rows.Add();

            for (int i = 0; i < dgViewTallas1.Columns.Count; i++)
            {
                this.dgViewTallas1.Rows[0].Cells[i].ValueType = typeof(int);
                this.dgViewTallas1.Rows[1].Cells[i].Style.BackColor = System.Drawing.SystemColors.Control;
                this.dgViewTallas1.Rows[1].Cells[i].ReadOnly = true;

                this.dgViewTallas2.Rows[0].Cells[i].ValueType = typeof(int);
                this.dgViewTallas2.Rows[1].Cells[i].Style.BackColor = System.Drawing.SystemColors.Control;
                this.dgViewTallas2.Rows[1].Cells[i].ReadOnly = true;

                this.dgViewTallas3.Rows[0].Cells[i].ValueType = typeof(int);
                this.dgViewTallas3.Rows[1].Cells[i].Style.BackColor = System.Drawing.SystemColors.Control;
                this.dgViewTallas3.Rows[1].Cells[i].ReadOnly = true;
            }
             */
        }

        private void dgvCellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (e.RowIndex == 1)
            {
                if (dgv.Name == "dgViewTallas1")
                    dgViewTallas2.Focus();

                if (dgv.Name == "dgViewTallas2")
                    dgViewTallas3.Focus();

                if (dgv.Name == "dgViewTallas3")
                    dgViewTallas4.Focus();

                if (dgv.Name == "dgViewTallas4")
                    btnProcesar.Focus();

            }
        }

        private void dgvDataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("El valor que ha escrito es inválido", "Verifique", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void txtDestino_Leave(object sender, EventArgs e)
        {
            gbTallas.Visible = true;
            DataTable datos = new DataTable();
            TransferenciaPorModelo transferenciaPorModelo = new TransferenciaPorModelo();
            this.Cursor = Cursors.WaitCursor;
            datos = TransferenciaPorModelo.DevuelveDatosConsulta(txtModelo.Text, txtOrigen.Text, txtDestino.Text);
            this.Cursor = Cursors.Default;
            if (datos.Rows.Count > 0)
            {
                lblModelo.Text = datos.Rows[0]["DESCR"].ToString();
                llenaDatosTallas(datos);
                btnProcesar.Enabled = true;
                //dgViewTallas1.Focus();                
            }
            else
            {
                dgViewTallas1.DataSource = null;
                dgViewTallas2.DataSource = null;
                //MessageBox.Show("No se ha encontrado información con los criterios especificados", "Verifique",
                //  MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            ValidaPermisosTransferencia();
        }
        private void llenaDatosTallas(DataTable tallas)
        {
            int tot = 0; ;
            tablasTallas = new DataSet();
            //int numeroTablas = ((tallas.Rows.Count - 1) / 10);
            int numeroTablas = ulp_bl.AddPartidasPedi.NumeroDeTablasSegunTotalTallasModificar(tallas);
            dgViewTallas1.DataSource = null;
            dgViewTallas2.DataSource = null;
            dgViewTallas3.DataSource = null;
            dgViewTallas4.DataSource = null;

            for (int i = 0; i < numeroTablas; i++)
            {

                string nombreTabla = "T" + i.ToString();
                tablasTallas.Tables.Add(nombreTabla);
                DataRow registroNuevo2 = tablasTallas.Tables[nombreTabla].NewRow();
                for (int y = 0; y < 10; y++)
                {
                    int longitudCol;
                    DataColumn columna = new DataColumn();
                    longitudCol = tallas.Rows[tot]["CLV_ART"].ToString().Length;
                    columna.Caption = tallas.Rows[tot]["CLV_ART"].ToString().Substring(8, longitudCol - 8);
                    columna.ColumnName = tallas.Rows[tot]["CLV_ART"].ToString().Substring(8, longitudCol - 8);
                    columna.DefaultValue = 0;
                    columna.DataType = typeof(Int32);
                    tablasTallas.Tables[nombreTabla].Columns.Add(columna);
                    registroNuevo2[columna.ColumnName] = Convert.ToInt32(Math.Round(Convert.ToDecimal(tallas.Rows[tot]["EXIST"].ToString()), 0));
                    if (y == 9 || (tallas.Rows.Count - 1) == tot)
                    {
                        DataRow registroNuevo = tablasTallas.Tables[nombreTabla].NewRow();
                        tablasTallas.Tables[nombreTabla].Rows.Add(registroNuevo);
                        tablasTallas.Tables[nombreTabla].Rows.Add(registroNuevo2);
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
                    dgViewTallas2.DataSource = tablasTallas.Tables[nombreTabla];
                    DataGridViewCell cell2 = dgViewTallas2.CurrentCell;
                    cell2.Selected = false;
                }
                if (i == 2)
                {
                    dgViewTallas3.DataSource = tablasTallas.Tables[nombreTabla];
                    DataGridViewCell cell3 = dgViewTallas3.CurrentCell;
                    cell3.Selected = false;
                }
                if (i == 3)
                {
                    dgViewTallas4.DataSource = tablasTallas.Tables[nombreTabla];
                    DataGridViewCell cell4 = dgViewTallas4.CurrentCell;
                    cell4.Selected = false;
                }
            }
            for (int i = 0; i < dgViewTallas1.Columns.Count; i++)
            {
                this.dgViewTallas1.Rows[1].Cells[i].Style.BackColor = System.Drawing.SystemColors.Control;
                this.dgViewTallas1.Rows[1].Cells[i].ReadOnly = true;
                this.dgViewTallas1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dgViewTallas1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            }
            for (int i = 0; i < dgViewTallas2.Columns.Count; i++)
            {
                this.dgViewTallas2.Rows[1].Cells[i].Style.BackColor = System.Drawing.SystemColors.Control;
                this.dgViewTallas2.Rows[1].Cells[i].ReadOnly = true;
                this.dgViewTallas2.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dgViewTallas2.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            }
            for (int i = 0; i < dgViewTallas3.Columns.Count; i++)
            {
                this.dgViewTallas3.Rows[1].Cells[i].Style.BackColor = System.Drawing.SystemColors.Control;
                this.dgViewTallas3.Rows[1].Cells[i].ReadOnly = true;
                this.dgViewTallas3.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dgViewTallas3.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            }
            for (int i = 0; i < dgViewTallas4.Columns.Count; i++)
            {
                this.dgViewTallas4.Rows[1].Cells[i].Style.BackColor = System.Drawing.SystemColors.Control;
                this.dgViewTallas4.Rows[1].Cells[i].ReadOnly = true;
                this.dgViewTallas4.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dgViewTallas4.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            }
        }
        private void CalculaTotales()
        {
            total = 0;

            for (int i = 0; i < dgViewTallas1.Columns.Count; i++)
            {
                if (dgViewTallas1[i, 0].Value.ToString() == "")
                {
                    tablasTallas.Tables[0].Rows[0].SetField(i, 0);
                }
                total = total + Convert.ToInt32(dgViewTallas1[i, 0].Value);
            }
            for (int i = 0; i < dgViewTallas2.Columns.Count; i++)
            {
                if (dgViewTallas2[i, 0].Value.ToString() == "")
                {
                    tablasTallas.Tables[1].Rows[0].SetField(i, 0);
                }
                total = total + Convert.ToInt32(dgViewTallas2[i, 0].Value);
            }
            for (int i = 0; i < dgViewTallas3.Columns.Count; i++)
            {
                if (dgViewTallas3[i, 0].Value.ToString() == "")
                {
                    tablasTallas.Tables[2].Rows[0].SetField(i, 0);
                }
                total = total + Convert.ToInt32(dgViewTallas3[i, 0].Value);
            }
            for (int i = 0; i < dgViewTallas4.Columns.Count; i++)
            {
                if (dgViewTallas4[i, 0].Value.ToString() == "")
                {
                    tablasTallas.Tables[3].Rows[0].SetField(i, 0);
                }
                total = total + Convert.ToInt32(dgViewTallas4[i, 0].Value);
            }

            lblTotal.Text = "Total: " + total.ToString();
        }

        private void dgViewTallas1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.Width = 40;
            Font fuente = new Font("Calibri", 8);
            e.Column.HeaderCell.Style.Font = fuente;
        }

        private void dgViewTallas2_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.Width = 40;
            Font fuente = new Font("Calibri", 8);
            e.Column.HeaderCell.Style.Font = fuente;
        }

        private void dgViewTallas3_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.Width = 40;
            Font fuente = new Font("Calibri", 8);
            e.Column.HeaderCell.Style.Font = fuente;
        }

        private void dgViewTallas4_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.Width = 40;
            Font fuente = new Font("Calibri", 8);
            e.Column.HeaderCell.Style.Font = fuente;
        }

        private void dgViewTallas1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            CalculaTotales();
        }

        private void dgViewTallas2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            CalculaTotales();
        }

        private void dgViewTallas3_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            CalculaTotales();
        }

        private void dgViewTallas4_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            CalculaTotales();
        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            ValidaPermisosTransferencia();
            this.Cursor = Cursors.WaitCursor;
            if (ProcesaPrendas())
            {

                this.Cursor = Cursors.Default;

                DialogResult resp = MessageBox.Show("¿ Desea transferir otro modelo ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resp == DialogResult.Yes)
                {
                    LimpiaInfo();
                    txtModelo.Focus();
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    if (MessageBox.Show("Desea imprimir la transferencia?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        DespliegaRpt();
                    }
                    this.Close();
                }


            }

        }

        private bool ProcesaPrendas()
        {
            int idTot = 0;
            bool procesada = true;
            for (int i = 0; i < tablasTallas.Tables.Count; i++)
            {
                for (int y = 0; y < tablasTallas.Tables[i].Columns.Count; y++)
                {
                    if (Convert.ToInt32(tablasTallas.Tables[i].Rows[0][y].ToString()) > 0)
                    {
                        if (Convert.ToInt32(tablasTallas.Tables[i].Rows[0][y].ToString()) <= Convert.ToInt32(tablasTallas.Tables[i].Rows[1][y].ToString()))
                        {
                            string res = GuardarTallas(i, y, idTot);
                            if (res != "")
                            {
                                procesada = false;
                                MessageBox.Show(res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                        }
                    }
                    idTot++;
                }
            }
            return procesada;
        }
        private string GuardarTallas(int idTabla, int idCol, int idDatos)
        {
            string resultado = TransferenciaPorModelo.ProcesaTransferencia(txtModelo.Text + tablasTallas.Tables[idTabla].Columns[idCol].ColumnName.ToString(),
                txtOrigen.Text, txtDestino.Text, Convert.ToInt32(tablasTallas.Tables[idTabla].Rows[0][idCol].ToString()), Globales.UsuarioActual.UsuarioUsuario, agrupador);
            return resultado;

        }
        private void LimpiaInfo()
        {
            tablasTallas.Clear();
            txtModelo.Text = "";
            lblModelo.Text = "";
            lblTotal.Text = "Total: 0";
            txtDestino.Text = "3";
            txtOrigen.Text = "1";
            dgViewTallas3.DataSource = null;
            dgViewTallas1.DataSource = null;
            dgViewTallas2.DataSource = null;
            dgViewTallas4.DataSource = null;
            btnProcesar.Enabled = false;
        }
        private void DespliegaRpt()
        {
            //DataTable datosRpt = new DataTable();
            //datosRpt = TransferenciaPorModelo.DevuelveDatosRpt(Convert.ToString(agrupador));
            //datosRpt.WriteXmlSchema("esquema.xsd");
            Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.TransferenciaXModelo, agrupador);
            frmReportes.FormClosed += frmReportes_FormClosed;
            frmReportes.Show();
        }

        void frmReportes_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void ValidaPermisosTransferencia()
        {
            int noAlmacenOrigen = 0;
            int noAlmacenDestino = 0;

            int.TryParse(txtOrigen.Text, out noAlmacenOrigen);
            int.TryParse(txtDestino.Text, out noAlmacenDestino);

            String _criterio = "Transferir_" + noAlmacenOrigen.ToString() + "a" + noAlmacenDestino.ToString();

            //if (noAlmacenOrigen == 1 && noAlmacenDestino == 3)
            //{
            bool permisoTransferencia =
                //ulp_bl.Reportes.PermisosUsuarioEspeciales.TienePermisos(Globales.UsuarioActual.Id, 93, 113);
                ulp_bl.Reportes.PermisosUsuarioEspeciales.TienePermisos(Globales.UsuarioActual.Id, 93, "Transferencia", _criterio);
            btnProcesar.Enabled = permisoTransferencia;
            if (permisoTransferencia)
            {
                lblPermisoParaTransferir.Text = "";
            }
            else
            {
                lblPermisoParaTransferir.Text = "Solicite acceso para transferir del almacén " + noAlmacenOrigen.ToString() + " al " + noAlmacenDestino.ToString();
            }
            //}
            //else
            //{
            //    lblPermisoParaTransferir.Text = "";
            //    btnProcesar.Enabled = true;
            //}


        }

        private void txtModelo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtOrigen_Leave(object sender, EventArgs e)
        {
            if (txtModelo.Text != "" && txtOrigen.Text != "" && txtDestino.Text != "")
            {
                gbTallas.Visible = true;
                DataTable datos = new DataTable();
                TransferenciaPorModelo transferenciaPorModelo = new TransferenciaPorModelo();
                this.Cursor = Cursors.WaitCursor;
                datos = TransferenciaPorModelo.DevuelveDatosConsulta(txtModelo.Text, txtOrigen.Text, txtDestino.Text);
                this.Cursor = Cursors.Default;
                if (datos.Rows.Count > 0)
                {
                    lblModelo.Text = datos.Rows[0]["DESCR"].ToString();
                    llenaDatosTallas(datos);
                    btnProcesar.Enabled = true;
                    //dgViewTallas1.Focus();                
                }
                else
                {
                    dgViewTallas1.DataSource = null;
                    dgViewTallas2.DataSource = null;
                    //MessageBox.Show("No se ha encontrado información con los criterios especificados", "Verifique",
                    //  MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                ValidaPermisosTransferencia();
            }
        }

        private void txtModelo_Leave(object sender, EventArgs e)
        {
            if (txtModelo.Text != "" && txtOrigen.Text != "" && txtDestino.Text != "")
            {
                gbTallas.Visible = true;
                DataTable datos = new DataTable();
                TransferenciaPorModelo transferenciaPorModelo = new TransferenciaPorModelo();
                this.Cursor = Cursors.WaitCursor;
                datos = TransferenciaPorModelo.DevuelveDatosConsulta(txtModelo.Text, txtOrigen.Text, txtDestino.Text);
                this.Cursor = Cursors.Default;
                if (datos.Rows.Count > 0)
                {
                    lblModelo.Text = datos.Rows[0]["DESCR"].ToString();
                    llenaDatosTallas(datos);
                    btnProcesar.Enabled = true;
                    //dgViewTallas1.Focus();                
                }
                else
                {
                    dgViewTallas1.DataSource = null;
                    dgViewTallas2.DataSource = null;
                    //MessageBox.Show("No se ha encontrado información con los criterios especificados", "Verifique",
                    //  MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                ValidaPermisosTransferencia();
            }
        }
    }
}
