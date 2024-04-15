using System;
using System.Collections;
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
    public partial class frmBusquedaGenerica : Form
    {

        public delegate void onItemSelectedHandler(DataRow SelectedRow);

        public event onItemSelectedHandler OnItemSelected;

        private DataTable tablaABuscar = new DataTable();
        private string campoOrdenarPor = "";
        Enumerados.TipoBusqueda tipo = new Enumerados.TipoBusqueda();
        public DataRow RenglonSeleccionado { get; set; }
        /// <summary>
        /// 0= Busca localmente en la tabla que se pase en la sobrecarga: DataTable TablaABuscar,string CampoOrdenarPor,string NombreCatalogo
        /// 1 = Busca en pedidos utilizando PED_MSTR.ConsultarPedidosPorCriterioCliente("criterio")
        /// </summary>        

        public frmBusquedaGenerica(DataTable TablaABuscar,string CampoOrdenarPor,string NombreCatalogo)
        {            
           campoOrdenarPor = CampoOrdenarPor;
           tablaABuscar = TablaABuscar;
           InitializeComponent();
            lblLocalizacion.Text = NombreCatalogo;
            tipo = Enumerados.TipoBusqueda.General;
        }
        public frmBusquedaGenerica(DataTable TablaABuscar, string filtroPre, string CampoOrdenarPor, string NombreCatalogo)
        {
            campoOrdenarPor = CampoOrdenarPor;
            tablaABuscar = TablaABuscar;
            InitializeComponent();
            lblLocalizacion.Text = NombreCatalogo;
            tipo = Enumerados.TipoBusqueda.General;
            txtCriterio.Text = filtroPre;

        }
        public frmBusquedaGenerica(string NombreCatalogo, Enumerados.TipoBusqueda tipoBusq)
        {
            InitializeComponent();
            lblLocalizacion.Text = NombreCatalogo;
            tipo = tipoBusq;            
        }
        private void frmBusquedaGenerica_Load(object sender, EventArgs e)
        {            
                   
        }

        private void LlenaLista()
        {
            //dataGridView1.DataSource = tablaABuscar;

        }

        private void txtCriterio_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (tipo == Enumerados.TipoBusqueda.General)
                {
                    if (!string.IsNullOrEmpty(txtCriterio.Text))
                    {
                        if (txtCriterio.Text.Length >= 2)
                        {
                            string filtro = RegresaFiltro(txtCriterio.Text);

                            DataView renglonesEncontrados = new DataView(tablaABuscar, filtro, campoOrdenarPor,
                                DataViewRowState.CurrentRows);


                            dataGridView1.DataSource = renglonesEncontrados;

                            if (dataGridView1.Rows.Count > 0)
                                dataGridView1.Rows[0].Selected = false;
                            else
                            {
                                dataGridView1.DataSource = null;
                            }
                        }
                        else
                        {
                            dataGridView1.DataSource = null;
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(txtCriterio.Text))
                    {
                        if (txtCriterio.Text.Length >= 2)
                        {
                            PED_MSTR pedMstr = new PED_MSTR();
                            DataTable dtPedidos = pedMstr.ConsultarPedidosPorCriterioCliente(txtCriterio.Text,
                                Globales.UsuarioActual.UsuarioUsuario);
                            //string filtro = RegresaFiltro(txtCriterio.Text);
                            //DataView renglonesEncontrados = new DataView(dtPedidos, filtro, campoOrdenarPor, DataViewRowState.CurrentRows);
                            if (tipo == Enumerados.TipoBusqueda.Pedidos)
                            {
                                dtPedidos.Columns.Remove("FECHA");
                            }

                            DataView dtViewPedidos = new DataView(dtPedidos);

                            dataGridView1.DataSource = dtViewPedidos;
                            if (dataGridView1.Rows.Count > 0)
                            {
                                dataGridView1.Rows[0].Selected = false;
                            }
                            else
                            {
                                dataGridView1.DataSource = null;
                            }
                        }
                        else
                        {
                            dataGridView1.DataSource = null;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                if (Ex is EvaluateException)
                {
                    MessageBox.Show("Algún caracter de los que ha escrito no está permitido, la expresión no se pudo evaluar","",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    txtCriterio.SelectionStart = txtCriterio.Text.Length - 1;
                    txtCriterio.SelectionLength = 1;
                    txtCriterio.Focus();
                }
            }
        }
        private string RegresaFiltro(string TextABuscar)
        {
            StringBuilder sb = new StringBuilder();
            int i = 0, j = 0;
            foreach (DataColumn col in tablaABuscar.Columns)
            {
                i++;

                if (col.DataType == typeof(string))
                {
                    j++;
                    sb.Append(string.Format(" {0} like '%{1}%' ", col.ColumnName, TextABuscar));
                }
                else if (col.DataType == typeof(int))
                {
                    j++;
                    sb.Append(string.Format(" CONVERT({0},System.String) like '%{1}%' ", col.ColumnName, TextABuscar));
                }

                if (sb.ToString().Substring(sb.Length - 2, 2) != "or")
                {
                    sb.Append("or");
                }

            }
            sb.Remove(sb.Length - 3, 3);
            return sb.ToString();
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                DataTable dt = ((DataView) dataGridView1.DataSource).ToTable();

                RenglonSeleccionado = dt.Rows[e.RowIndex];
                if (this.OnItemSelected != null)
                {
                    //esto se ocupa por si queremos que la forma de búsqueda no se cierre pero que sí puedan seleccionar elementos
                    this.OnItemSelected(RenglonSeleccionado);
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.Value);
            if (e.Value != null)
            {
                if (e.Value.ToString().ToUpper().Contains(txtCriterio.Text.ToUpper()))
                {
                    e.CellStyle.ForeColor = Color.Blue;
                }
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var dataGridView = sender as DataGridView;
            if (dataGridView.Rows[e.RowIndex].Selected)
            {
                e.CellStyle.Font = new Font(new FontFamily(e.CellStyle.Font.Name),e.CellStyle.Font.Size - 1, FontStyle.Bold);
                // edit: to change the background color:
                //e.CellStyle.SelectionBackColor = Color.Coral;
            }
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*
            if (true)
            {
                
            }
             */
            return;
            /*
            if (e.KeyChar == (char) 13)
            {
                if (dataGridView1.SelectedRows.Count == 1)
                {
                    int selectedIndex = 0;
                    if (dataGridView1.Rows.Count > 1 && dataGridView1.CurrentRow.Index + 1 < dataGridView1.Rows.GetLastRow())
                    {
                        selectedIndex = dataGridView1.CurrentRow.Index - 1;
                    }
                    else if (dataGridView1.CurrentRow.Index == dataGridView1.Rows.Count - 1)
                    {
                        selectedIndex = dataGridView1.CurrentRow.Index;
                    }
                    dataGridView1_CellMouseDoubleClick(sender,
                        new DataGridViewCellMouseEventArgs(0, selectedIndex, 0, 0,
                            new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0)));
                }
            }
             * */
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
           // Text = e.RowIndex.ToString();
                dataGridView1.Rows[e.RowIndex].Selected = true;
            //dataGridView1_KeyPress(sender, new KeyPressEventArgs((char) Keys.Down));
                dataGridView1_CellClick(sender,new DataGridViewCellEventArgs(e.ColumnIndex,e.RowIndex));            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

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

    }
}
