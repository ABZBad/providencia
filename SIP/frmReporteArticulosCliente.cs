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


namespace SIP
{
    public partial class frmReporteArticulosCliente : Form
    {
        #region <Atributos y Constructores>
        
        private BackgroundWorker bgw;
        private Precarga precarga;
        private Exception ex;
        private String Cliente;
        private String ClienteDesc;
        private String _CriterioDeBusqueda;
        
        private DataTable tablaABuscar = new DataTable();

        public frmReporteArticulosCliente()
        {
            InitializeComponent();
            precarga = new Precarga(this);
            tablaABuscar = ulp_bl.Globales.tablaclientes;
            _CriterioDeBusqueda = "";
        }
        public frmReporteArticulosCliente(String _criterioDeBusqueda)
        {
            InitializeComponent();
            precarga = new Precarga(this);
            tablaABuscar = ulp_bl.Globales.tablaclientes;
            this._CriterioDeBusqueda= _criterioDeBusqueda;
        }

        #endregion
        #region <Eventos>      
  
        private void frmReporteArticulosCliente_Load(object sender, EventArgs e)
        {
            dtpDesde.Value = DateTime.Now;
            dtpHasta.Value = DateTime.Now;
            CargaClientes();
            txtCriterio.Text = this._CriterioDeBusqueda;
        }
        private void btnContinuar_Click(object sender, EventArgs e)
        {
            Cliente = dataGridView1.CurrentRow.Cells[0].Value.ToString().Trim();
            ClienteDesc = Cliente + " - " + dataGridView1.CurrentRow.Cells[1].Value.ToString().Trim();
            bgw = new BackgroundWorker();
            bgw.DoWork += bgw_DoWork;
            bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
            precarga.MostrarEspera();
            bgw.RunWorkerAsync();                                
        }
        private void txtCriterio_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCriterio.Text))
                {
                    if (txtCriterio.Text.Length >= 2)
                    {
                        string filtro = RegresaFiltro(txtCriterio.Text);

                        DataView renglonesEncontrados = new DataView(tablaABuscar, filtro, "CLAVE",
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
            catch (Exception Ex)
            {
                if (Ex is EvaluateException)
                {
                    MessageBox.Show("Algún caracter de los que ha escrito no está permitido, la expresión no se pudo evaluar", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCriterio.SelectionStart = txtCriterio.Text.Length - 1;
                    txtCriterio.SelectionLength = 1;
                    txtCriterio.Focus();
                }
            }

        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var dataGridView = sender as DataGridView;
            if (dataGridView.Rows[e.RowIndex].Selected)
            {
                e.CellStyle.Font = new Font(new FontFamily(e.CellStyle.Font.Name), e.CellStyle.Font.Size - 1, FontStyle.Bold);
                // edit: to change the background color:
                //e.CellStyle.SelectionBackColor = Color.Coral;
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
        #endregion
        #region <Workers>
        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            precarga.RemoverEspera();
            
        }
        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            precarga.AsignastatusProceso("Generando información...");
            DataTable dtArticulosCliente = new DataTable();
            DataTable dtDescuentoPrecioCliente = new DataTable();

            //OBTENEMOS EL PRECIO Y DESCUENTO SUGERIDO
            dtDescuentoPrecioCliente = ConfiguracionDescuentoSugerido.GetPrecioDescuentoCliente(Cliente, dtpHasta.Value,ref ex);

            dtArticulosCliente = ReporteArticulosCliente.RegresaArticulosCliente(dtpDesde.Value, dtpHasta.Value, Cliente, ref ex);
            if (ex == null)
            {
                if (dtArticulosCliente != null)
                {
                    if (dtArticulosCliente.Rows.Count != 0)
                    {
                        precarga.AsignastatusProceso("Generando archivo de Excel...");
                        //GENERAMOS EL EXCEL
                        string archivoTemporal = System.IO.Path.GetTempFileName().Replace(".tmp", ".xls");
                        ReporteArticulosCliente.GeneraArchivoExcel(archivoTemporal, dtArticulosCliente, dtpDesde.Value, dtpHasta.Value, ClienteDesc, decimal.Parse(dtDescuentoPrecioCliente.Rows[0]["DS"].ToString()), decimal.Parse(dtDescuentoPrecioCliente.Rows[0]["CMP"].ToString()));
                        FuncionalidadesFormularios.MostrarExcel(archivoTemporal);
                    }
                    else
                    {
                        MessageBox.Show("No existen registros con este rango de fechas", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("No existen registros con este rango de fechas", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        #endregion
        #region<METODOS>
        private void CargaClientes()
        {
            CLIE01 clientes = new CLIE01();
            Globales.tablaclientes = clientes.Consultar(Globales.UsuarioActual, "");
            tablaABuscar = Globales.tablaclientes;
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
        #endregion       

    }
}
