using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ulp_bl;
using SIP.Utiles;

namespace SIP
{
    public partial class frmSimuladorCompras : Form
    {
        public delegate void onItemSelectedHandler(DataRow SelectedRow);
        public event onItemSelectedHandler OnItemSelected;
        SimuladorCompra oSimuladorCompra;
        private Precarga precarga;
        private BackgroundWorker bgw;
        DataTable MateriaPrima = new DataTable();
        DataTable Proveedores = new DataTable();
        DataTable Simulaciones = new DataTable();
        DataTable dtGrid = new DataTable();
        Boolean esEdicion;
        int idSimulacion;


        public frmSimuladorCompras()
        {
            InitializeComponent();
            oSimuladorCompra = new SimuladorCompra();
            precarga = new Precarga(this);
            //CreateGrid();
        }

        private void btnAgregarProveedor_Click(object sender, EventArgs e)
        {
            cmbProveedores.SelectedIndex = 0;
            dgvArticulos.Rows.Clear();
            panel1.Visible = true;
            cmbProveedores.Enabled = true;
            txtCriterio.Text = "";
        }
        
        private void frmSimuladorCompras_Load(object sender, EventArgs e)
        {
            this.Proveedores = SimuladorCompra.getProveedoresSimulador();
            this.Simulaciones = SimuladorCompra.getSimuladorCompras();
            this.MateriaPrima = SimuladorCompra.getArticulosSimulador();

            dateTimePicker1.Value = DateTime.Now;

            cmbProveedores.DisplayMember = "NOMBRE";
            cmbProveedores.ValueMember = "CLAVE";
            cmbProveedores.DataSource = this.Proveedores;

            cmbSimulaciones.DisplayMember = "Descripcion";
            cmbSimulaciones.ValueMember = "Clave";
            cmbSimulaciones.DataSource = this.Simulaciones;

            dataGridView1.DataSource = this.MateriaPrima.AsDataView();

            dateTimePicker1.CustomFormat = "MMMM yyyy";
            dateTimePicker1.ShowUpDown = true;
            this.esEdicion = false;

        }

        private void getTreeSimulador(SimuladorCompra oSimulador)
        {
            treeView.Nodes.Clear();
            foreach (SimuladorCompra.Cotizacion oCotizacion in oSimulador.Cotizaciones)
            {
                TreeNode oNode = new TreeNode(oCotizacion.NombreProveedor + " - " + oCotizacion.getTotal().ToString("C"));
                oNode.Name = oCotizacion.ClaveProveedor;
                foreach (SimuladorCompra.Articulo oArticulo in oCotizacion.Articulos)
                {
                    TreeNode oChildNode = new TreeNode(oArticulo.Cantidad + " " + oArticulo.Descripcion + " Total: " + oArticulo.Total.ToString("C"));
                    oChildNode.Name = oCotizacion.ClaveProveedor;
                    oNode.Nodes.Add(oChildNode);
                }
                treeView.Nodes.Add(oNode);
            }
            treeView.ExpandAll();
            txtSubtotal.Text = oSimuladorCompra.getTotal().ToString("C");
        }

        private void CreateGrid()
        {

            DataGridViewComboBoxColumn cbColumn;
            DataGridViewTextBoxColumn txColumn;

            //AGREGAMOS LAS COLUMNAS
            txColumn = new DataGridViewTextBoxColumn();
            txColumn.Name = "CANTIDAD";
            txColumn.Width = 70;
            dgvArticulos.Columns.Add(txColumn);

            cbColumn = new DataGridViewComboBoxColumn();
            cbColumn.AutoComplete = true;
            cbColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
            cbColumn.Name = "CLAVE";
            cbColumn.ValueMember = "CLAVE";
            cbColumn.DisplayMember = "DESCRIPCION";
            cbColumn.DataSource = MateriaPrima;

            cbColumn.Width = 320;
            dgvArticulos.Columns.Add(cbColumn);

            txColumn = new DataGridViewTextBoxColumn();
            txColumn.Name = "PRECIO";
            txColumn.Width = 70;
            dgvArticulos.Columns.Add(txColumn);

            txColumn = new DataGridViewTextBoxColumn();
            txColumn.Name = "TOTAL";
            txColumn.Width = 70;
            txColumn.DefaultCellStyle.BackColor = Color.LightGray;
            txColumn.ReadOnly = true;
            dgvArticulos.Columns.Add(txColumn);

        }

        private void dgvArticulos_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //if (e.ColumnIndex == 1 || e.ColumnIndex == 3)
            if (dgvArticulos.Columns[e.ColumnIndex].Name == "CANTIDAD" || dgvArticulos.Columns[e.ColumnIndex].Name == "PRECIO")
            {
                decimal i;
                if (e.FormattedValue.ToString().Replace("$", "") != "")
                {
                    if (!decimal.TryParse(Convert.ToString(e.FormattedValue.ToString().Replace("$", "")), out i))
                    {
                        MessageBox.Show("Solo se permiten valores numéricos.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Cancel = true;
                    }
                }
            }

        }

        private void dgvArticulos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            decimal cantidad, precio;
            try
            {
                //decimal.TryParse(dgvArticulos[1, e.RowIndex].Value.ToString(), out cantidad);
                decimal.TryParse(dgvArticulos.Rows[e.RowIndex].Cells["CANTIDAD"].Value.ToString(), out cantidad);
            }
            catch { cantidad = 0; }
            try
            {
                //decimal.TryParse(dgvArticulos[3, e.RowIndex].Value.ToString(), out precio);
                decimal.TryParse(dgvArticulos.Rows[e.RowIndex].Cells["PRECIO"].Value.ToString(), out precio);
            }
            catch { precio = 0; }
            if (cantidad == null)
                cantidad = 0;
            if (precio == null)
                precio = 0;



            //dgvArticulos[4, e.RowIndex].Value = (cantidad * precio) * (decimal).16; //IVA
            dgvArticulos.Rows[e.RowIndex].Cells["SUBTOTAL"].Value = (cantidad * precio);
            dgvArticulos.Rows[e.RowIndex].Cells["IVA"].Value = (cantidad * precio) * (decimal).16;
            dgvArticulos.Rows[e.RowIndex].Cells["TOTAL"].Value = (cantidad * precio) * (decimal)1.16;

            lblProveedorTotal.Text = getTotalCotizacion().ToString("C");
        }

        private decimal getTotalCotizacion()
        {
            decimal total = 0;
            foreach (DataGridViewRow dr in dgvArticulos.Rows)
            {
                if (dr.Cells["TOTAL"] != null)
                    if (dr.Cells["TOTAL"].Value != null)
                    {
                        {
                            total += decimal.Parse(dr.Cells["TOTAL"].Value.ToString());
                        }
                    }
            }
            return total;
        }

        private void btnProveedorAgregar_Click(object sender, EventArgs e)
        {
            try
            {

                SimuladorCompra.Cotizacion oCotizacion = new SimuladorCompra.Cotizacion();

                oCotizacion.NombreProveedor = cmbProveedores.Text;
                oCotizacion.ClaveProveedor = cmbProveedores.SelectedValue.ToString();

                foreach (DataGridViewRow dr in dgvArticulos.Rows)
                {
                    if (dr.Cells["CANTIDAD"].Value != null)
                    {
                        SimuladorCompra.Articulo oArticulo = new SimuladorCompra.Articulo();
                        oArticulo.Pedido = dr.Cells["PEDIDO"].Value.ToString();
                        oArticulo.Cantidad = decimal.Parse(dr.Cells["CANTIDAD"].Value.ToString());
                        oArticulo.ClaveArticulo = dr.Cells["DESCRIPCION"].Value.ToString();
                        oArticulo.Descripcion = dr.Cells["DESCRIPCION"].FormattedValue.ToString();
                        oArticulo.UltimoCosto = decimal.Parse(dr.Cells["ULTIMOCOSTO"].Value.ToString());
                        oArticulo.PrecioUnitario = decimal.Parse(dr.Cells["PRECIO"].Value.ToString());
                        oArticulo.Subtotal = (oArticulo.Cantidad * oArticulo.PrecioUnitario);
                        oArticulo.IVA = (oArticulo.Cantidad * oArticulo.PrecioUnitario) * (decimal)0.16;
                        oArticulo.Total = (oArticulo.Cantidad * oArticulo.PrecioUnitario) * (decimal)1.16;
                        oCotizacion.Articulos.Add(oArticulo);
                    }
                }

                //VERIFICAMOS SI YA EXISTE COTIZACION PARA ESE PROVEEDOR
                String cveProveedor = cmbProveedores.SelectedValue.ToString();

                if (this.esEdicion)
                {
                    this.oSimuladorCompra.Cotizaciones.Where(x => x.ClaveProveedor == cveProveedor).FirstOrDefault().Articulos = oCotizacion.Articulos;
                }
                else
                {
                    oSimuladorCompra.Cotizaciones.Add(oCotizacion);
                }
                getTreeSimulador(oSimuladorCompra);
                oSimuladorCompra.PresupuestoCobranza = txtPresupuesto.Text == "" ? 0 : decimal.Parse(txtPresupuesto.Text.Replace("$", ""));
                txtPresupuestoFinal.Text = oSimuladorCompra.PresupuestoCobranzaFinal.ToString("C");
                txtSubtotal.Text = oSimuladorCompra.getTotal().ToString("C");
                txtDiferencia.Text = (oSimuladorCompra.PresupuestoCobranzaFinal - oSimuladorCompra.getTotal()).ToString("C");



                panel1.Visible = false;
            }
            catch
            {
                MessageBox.Show("Error al capturar la información, favor de verificar", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPresupuesto_KeyUp(object sender, KeyEventArgs e)
        {
            oSimuladorCompra.PresupuestoCobranza = txtPresupuesto.Text == "" ? 0 : decimal.Parse(txtPresupuesto.Text.Replace("$", ""));
            txtPresupuestoFinal.Text = oSimuladorCompra.PresupuestoCobranzaFinal.ToString("C");
            txtSubtotal.Text = oSimuladorCompra.getTotal().ToString("C");
            txtDiferencia.Text = (oSimuladorCompra.PresupuestoCobranzaFinal - oSimuladorCompra.getTotal()).ToString("C");
        }

        private void txtPresupuesto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void btnProveedorCerrar_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            bgw = new BackgroundWorker();
            bgw.DoWork += bgw_DoWork;
            bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
            precarga.MostrarEspera();
            bgw.RunWorkerAsync();
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            bgw = new BackgroundWorker();
            bgw.DoWork += bgw_DoWorkExport;
            bgw.RunWorkerCompleted += bgw_RunWorkerExportCompleted;
            precarga.MostrarEspera();
            bgw.RunWorkerAsync();

        }



        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            precarga.AsignastatusProceso("Generando información...");
            oSimuladorCompra.Fecha = dateTimePicker1.Value;
            oSimuladorCompra.Total = oSimuladorCompra.getTotal();
            oSimuladorCompra.Diferencia = oSimuladorCompra.PresupuestoCobranzaFinal - oSimuladorCompra.Total;
            if (this.esEdicion)
            {
                oSimuladorCompra.setAltaSimulador(this.idSimulacion);
            }
            else
            {
                oSimuladorCompra.setAltaSimulador(0);
            }


        }
        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            precarga.RemoverEspera();
            MessageBox.Show("Simulación guardada de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LimpiaForma();
            frmSimuladorCompras_Load(null, null);
        }

        void bgw_DoWorkExport(object sender, DoWorkEventArgs e)
        {
            precarga.AsignastatusProceso("Generando archivo excel...");
            oSimuladorCompra.Fecha = dateTimePicker1.Value;
            oSimuladorCompra.Total = oSimuladorCompra.getTotal();
            oSimuladorCompra.Diferencia = oSimuladorCompra.PresupuestoCobranzaFinal - oSimuladorCompra.Total;
            string archivoTemporal = System.IO.Path.GetTempFileName().Replace(".tmp", ".xls");
            SimuladorCompra.GeneraArchivoExcel(archivoTemporal, this.oSimuladorCompra);
            FuncionalidadesFormularios.MostrarExcel(archivoTemporal);
        }
        void bgw_RunWorkerExportCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            precarga.RemoverEspera();
        }

        private void btnCargarSimulacion_Click(object sender, EventArgs e)
        {
            try
            {
                //OBTENEMOS TODOS LS DATOS DE LA SIMULACION
                this.idSimulacion = (int)cmbSimulaciones.SelectedValue;
                this.esEdicion = true;
                oSimuladorCompra = SimuladorCompra.getSimuladorCompraByID(this.idSimulacion);
                if (oSimuladorCompra != null)
                {
                    getTreeSimulador(oSimuladorCompra);
                    dateTimePicker1.Value = oSimuladorCompra.Fecha;
                    txtPresupuesto.Text = oSimuladorCompra.PresupuestoCobranza.ToString("C");
                    txtPorcentaje.Text = oSimuladorCompra.Porcentaje.ToString();
                    txtPresupuestoFinal.Text = oSimuladorCompra.PresupuestoCobranzaFinal.ToString("C");
                    txtSubtotal.Text = oSimuladorCompra.Total.ToString("C");
                    txtDiferencia.Text = oSimuladorCompra.Diferencia.ToString("C");
                }
                else
                {
                    MessageBox.Show("Error al cargar los datos del Simulador.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch { }

        }

        private void LimpiaForma()
        {
            txtPresupuesto.Text = "0.0";
            txtPorcentaje.Text = "52";
            txtPresupuestoFinal.Text = "0.0";
            txtSubtotal.Text = "0.0";
            txtDiferencia.Text = "0.0";
            treeView.Nodes.Clear();
            this.oSimuladorCompra = new SimuladorCompra();
        }


        private void txtCriterio_TextChanged(object sender, EventArgs e)
        {

            var res = from row in this.MateriaPrima.AsEnumerable() where row.Field<String>("DESCRIPCION").ToUpper().Contains(txtCriterio.Text.ToUpper()) select row;
            dataGridView1.DataSource = res.AsDataView();
            if (dataGridView1.Rows.Count > 0)
                dataGridView1.Rows[0].Selected = false;

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            // Text = e.RowIndex.ToString();
            dataGridView1.Rows[e.RowIndex].Selected = true;
            //dataGridView1_KeyPress(sender, new KeyPressEventArgs((char) Keys.Down));
            dataGridView1_CellClick(sender, new DataGridViewCellEventArgs(e.ColumnIndex, e.RowIndex));
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dataGridView1.SelectedRows.Count == 1)
                {
                    dataGridView1_CellMouseDoubleClick(sender,
                        new DataGridViewCellMouseEventArgs(0, dataGridView1.CurrentRow.Index, 0, 0,
                            new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0)));
                }
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                //OBTENEMOS EL ULTIMO COSTO
                decimal ultimoCosto = 0;


                DataTable dt = ((DataView)dataGridView1.DataSource).ToTable();
                DataRow RenglonSeleccionado = dt.Rows[e.RowIndex];
                DataTable dtCompras = SimuladorCompra.getUltimoCosto(RenglonSeleccionado["CLAVE"].ToString());

                ultimoCosto = dtCompras.Rows.Count > 0 ? decimal.Parse(dtCompras.Rows[0]["COST"].ToString()) : 0;
                //AGREGAMOS A LA TABLA PRINCIPAL
                dgvArticulos.Rows.Add();
                dgvArticulos.Rows[dgvArticulos.Rows.Count - 1].Cells["PEDIDO"].Value = "";
                dgvArticulos.Rows[dgvArticulos.Rows.Count - 1].Cells["CANTIDAD"].Value = 1;
                dgvArticulos.Rows[dgvArticulos.Rows.Count - 1].Cells["DESCRIPCION"].Value = RenglonSeleccionado["DESCRIPCION"].ToString(); // descripcion
                dgvArticulos.Rows[dgvArticulos.Rows.Count - 1].Cells["ULTIMOCOSTO"].Value = ultimoCosto;
                dgvArticulos.Rows[dgvArticulos.Rows.Count - 1].Cells["PRECIO"].Value = 0;
                dgvArticulos.Rows[dgvArticulos.Rows.Count - 1].Cells["SUBTOTAL"].Value = 0;
                dgvArticulos.Rows[dgvArticulos.Rows.Count - 1].Cells["IVA"].Value = 0;
                dgvArticulos.Rows[dgvArticulos.Rows.Count - 1].Cells["TOTAL"].Value = 0;

            }
        }

        private void btnNuevoSimulador_Click(object sender, EventArgs e)
        {
            LimpiaForma();
            this.esEdicion = false;
        }

        private void dgvArticulos_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            lblProveedorTotal.Text = getTotalCotizacion().ToString("C");
        }

        private void dgvArticulos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvArticulos.Columns[e.ColumnIndex].Name == "PRECIO")
            {
                decimal value;
                if (e.Value != null && decimal.TryParse(e.Value.ToString(), out value))
                {
                    e.Value = value.ToString("C");

                    /*** OR ***

                    e.Value = value;
                    e.CellStyle.Format = "#k";

                    */
                }
            }
            if (dgvArticulos.Columns[e.ColumnIndex].Name == "Cantidad")
            {
                decimal value;
                if (e.Value != null && decimal.TryParse(e.Value.ToString(), out value))
                {
                    e.Value = value.ToString("N0");

                    /*** OR ***

                    e.Value = value;
                    e.CellStyle.Format = "#k";

                    */
                }
            }

        }

        private void txtPresupuesto_Leave(object sender, EventArgs e)
        {
            try
            {
                txtPresupuesto.Text = oSimuladorCompra.PresupuestoCobranza.ToString("C");
            }
            catch { }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (treeView.SelectedNode != null)
            {
                if (treeView.SelectedNode.Name != "")
                {
                    String cveProveedor = treeView.SelectedNode.Name;
                    cmbProveedores.SelectedValue = cveProveedor;
                    cmbProveedores.Enabled = false;
                    dgvArticulos.Rows.Clear();
                    panel1.Visible = true;
                    txtCriterio.Text = "";
                    // buscamos la cotizacion y llenamos los articulos
                    var coti = this.oSimuladorCompra.Cotizaciones.Where(x => x.ClaveProveedor == cveProveedor).FirstOrDefault();
                    foreach (SimuladorCompra.Articulo _articulo in coti.Articulos)
                    {
                        dgvArticulos.Rows.Add();
                        dgvArticulos.Rows[dgvArticulos.Rows.Count - 1].Cells["PEDIDO"].Value = _articulo.Pedido;
                        dgvArticulos.Rows[dgvArticulos.Rows.Count - 1].Cells["CANTIDAD"].Value = _articulo.Cantidad;
                        dgvArticulos.Rows[dgvArticulos.Rows.Count - 1].Cells["DESCRIPCION"].Value = _articulo.Descripcion;
                        dgvArticulos.Rows[dgvArticulos.Rows.Count - 1].Cells["ULTIMOCOSTO"].Value = _articulo.UltimoCosto;
                        dgvArticulos.Rows[dgvArticulos.Rows.Count - 1].Cells["PRECIO"].Value = _articulo.PrecioUnitario;
                        dgvArticulos.Rows[dgvArticulos.Rows.Count - 1].Cells["SUBTOTAL"].Value = _articulo.Subtotal;
                        dgvArticulos.Rows[dgvArticulos.Rows.Count - 1].Cells["IVA"].Value = _articulo.IVA;
                        dgvArticulos.Rows[dgvArticulos.Rows.Count - 1].Cells["TOTAL"].Value = _articulo.Total;
                    }

                }
                else
                {
                    MessageBox.Show("Se debe de seleccionar el proveedor.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Se debe de seleccionar el proveedor.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }


}
