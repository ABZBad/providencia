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
    public partial class frmModificarTallas2 : Form
    {
        int Pedido = 0;
        string Modelo = "";
        decimal Precio = 0;
        string descripcion = "";
        string Agrupador = "";
        int totalPrendas = 0;
        decimal precioLista = 0;
        DataSet tablasTallas;
        DataTable tallasTotales;
        DataTable existencias = new DataTable();
        decimal descuento = 0;
        enum RecorridoTablasTallas
        {
            Guardar,
            CalcularCantidad
        }
        Enumerados.TipoPedido tipoPartida;
        public frmModificarTallas2()
        {
            InitializeComponent();
        }
        public frmModificarTallas2(int pedido, string modelo, string agrupador, decimal precio, decimal PRECIO_LISTA, decimal descuento = 0)
        {
            InitializeComponent();
            DataTable tiposProductos = new DataTable();
            vw_Inventario inventario = new vw_Inventario();
            tiposProductos = inventario.ConsultarConDatosPedido(pedido, modelo, agrupador);
            Pedido = pedido;
            Modelo = modelo;
            precioLista = PRECIO_LISTA;
            Agrupador = agrupador;
            descripcion = tiposProductos.Rows[0]["DESCR"].ToString();
            Precio = precio;
            tallasTotales = tiposProductos;
            this.descuento = descuento;
            // OBTENEMOS EXISTENCIAS
            vw_Inventario inventario2 = new vw_Inventario();
            this.existencias = inventario2.GetExistenciasPorModelo134(modelo);
        }
        private void llenaDatosTallas(DataTable tallas)
        {
            int tot = 0;
            tablasTallas = new DataSet();
            //int numeroTablas = ((tallas.Rows.Count - 1) / 10);
            int numeroTablas = ulp_bl.AddPartidasPedi.NumeroDeTablasSegunTotalTallasModificar(tallas);
            dgViewTallas1.DataSource = null;
            dgViewTallas2.DataSource = null;
            dgViewTallas2.Enabled = false;
            dgViewTallas3.DataSource = null;
            dgViewTallas3.Enabled = false;
            dgViewTallas4.DataSource = null;
            dgViewTallas4.Enabled = false;
            dgViewTallas5.DataSource = null;
            dgViewTallas5.Enabled = false;
            for (int i = 0; i < numeroTablas; i++)
            {

                string nombreTabla = "T" + i.ToString();
                tablasTallas.Tables.Add(nombreTabla);
                for (int y = 0; y < 10; y++)
                {
                    int longitudCol;
                    DataColumn columna = new DataColumn();
                    longitudCol = tallas.Rows[tot]["CLV_ART"].ToString().Length;
                    if (longitudCol > 8)
                    {
                        columna.ColumnName = tallas.Rows[tot]["CLV_ART"].ToString().Substring(8, longitudCol - 8);
                        columna.Caption = tallas.Rows[tot]["CLV_ART"].ToString().Substring(8, longitudCol - 8);
                    }
                    else
                    {
                        columna.ColumnName = "__";
                        columna.Caption = "";
                    }
                    columna.DefaultValue = Convert.ToInt32(tallas.Rows[tot]["CANTIDAD"].ToString());
                    columna.DataType = typeof(Int32);
                    tablasTallas.Tables[nombreTabla].Columns.Add(columna);
                    if (y == 9 || (tallas.Rows.Count - 1) == tot)
                    {
                        DataRow registroNuevo = tablasTallas.Tables[nombreTabla].NewRow();
                        tablasTallas.Tables[nombreTabla].Rows.Add(registroNuevo);

                        // agregamos existencias (se calculan con base a almacen 1+3+4-virtual)
                        registroNuevo = tablasTallas.Tables[nombreTabla].NewRow();
                        for (int z = 0; z < registroNuevo.ItemArray.Length; z++)
                        {
                            string filtro = String.Format("CVE_ART = '{0}' AND CVE_ALM = 1", Modelo + tablasTallas.Tables[nombreTabla].Columns[z].ColumnName);
                            registroNuevo[z] = this.existencias.Select(filtro).FirstOrDefault()["EXIST"];
                        }
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
                    dgViewTallas1.Rows[1].ReadOnly = true;
                    dgViewTallas1.Rows[1].DefaultCellStyle.BackColor = Color.LightGray;
                }
                if (i == 1)
                {
                    dgViewTallas2.Enabled = true;
                    dgViewTallas2.DataSource = tablasTallas.Tables[nombreTabla];
                    DataGridViewCell cell2 = dgViewTallas2.CurrentCell;
                    cell2.Selected = false;
                    dgViewTallas2.Rows[1].ReadOnly = true;
                    dgViewTallas2.Rows[1].DefaultCellStyle.BackColor = Color.LightGray;
                }
                if (i == 2)
                {
                    dgViewTallas3.Enabled = true;
                    dgViewTallas3.DataSource = tablasTallas.Tables[nombreTabla];
                    DataGridViewCell cell3 = dgViewTallas3.CurrentCell;
                    cell3.Selected = false;
                    dgViewTallas3.Rows[1].ReadOnly = true;
                    dgViewTallas3.Rows[1].DefaultCellStyle.BackColor = Color.LightGray;
                }
                if (i == 3)
                {
                    dgViewTallas4.Enabled = true;
                    dgViewTallas4.DataSource = tablasTallas.Tables[nombreTabla];
                    DataGridViewCell cell4 = dgViewTallas4.CurrentCell;
                    cell4.Selected = false;
                    dgViewTallas4.Rows[1].ReadOnly = true;
                    dgViewTallas4.Rows[1].DefaultCellStyle.BackColor = Color.LightGray;
                }
                if (i == 4)
                {
                    dgViewTallas5.Enabled = true;
                    dgViewTallas5.DataSource = tablasTallas.Tables[nombreTabla];
                    DataGridViewCell cell5 = dgViewTallas5.CurrentCell;
                    cell5.Selected = false;
                    dgViewTallas5.Rows[1].ReadOnly = true;
                    dgViewTallas5.Rows[1].DefaultCellStyle.BackColor = Color.LightGray;
                }
            }
        }
        private void GuardarPed_Det(int idTabla, int idColumna)
        {
            PED_DET pedidos_detalle = new PED_DET();
            pedidos_detalle.PEDIDO = Pedido;
            pedidos_detalle.CODIGO = Modelo + tablasTallas.Tables[idTabla].Columns[idColumna].Caption;
            pedidos_detalle.DESCRIPCION = vw_Inventario.RegresaModeloDescripcion(pedidos_detalle.CODIGO);
            pedidos_detalle.DESCUENTO = double.Parse(this.descuento.ToString());
            pedidos_detalle.CANTIDAD = Convert.ToInt32(tablasTallas.Tables[idTabla].Rows[0][idColumna].ToString());
            pedidos_detalle.PROCESOS = "xx";
            pedidos_detalle.AGRUPADOR = Agrupador;
            //if (tipoPartida == Enumerados.TipoPedido.Pedido)
            //{
            pedidos_detalle.PRECIO_PROD = Precio;
            pedidos_detalle.PREC_PROCESO = 0;
            pedidos_detalle.SUBTOTAL = 0;
            pedidos_detalle.PRECIO_LISTA = precioLista;
            //}
            pedidos_detalle.Crear(pedidos_detalle);
        }
        private void ProcesaPrendas(RecorridoTablasTallas tipoProceso)
        {
            if (tipoProceso == RecorridoTablasTallas.CalcularCantidad)
            {
                totalPrendas = 0;
            }

            for (int i = 0; i < tablasTallas.Tables.Count; i++)
            {
                for (int y = 0; y < tablasTallas.Tables[i].Columns.Count; y++)
                {
                    if (tablasTallas.Tables[i].Rows[0][y].ToString() == "")
                    {
                        tablasTallas.Tables[i].Rows[0].SetField(y, 0);
                    }
                    int valor = 0;
                    int.TryParse(tablasTallas.Tables[i].Rows[0][y].ToString(), out valor);

                    if (valor > 0)
                    {
                        if (tipoProceso == RecorridoTablasTallas.Guardar)
                        {
                            GuardarPed_Det(i, y);
                        }
                        else
                        {
                            totalPrendas = totalPrendas + Convert.ToInt32(tablasTallas.Tables[i].Rows[0][y].ToString());
                        }
                    }
                }
            }
        }
        private void CalculaTotalPrendas()
        {
            ProcesaPrendas(RecorridoTablasTallas.CalcularCantidad);
            lblTotalPrendas.Text = totalPrendas.ToString();
        }

        private void dgViewTallas1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            CalculaTotalPrendas();
        }

        private void dgViewTallas2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            CalculaTotalPrendas();
        }

        private void dgViewTallas3_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            CalculaTotalPrendas();
        }
        private void dgViewTallas4_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            CalculaTotalPrendas();
        }
        private void dgViewTallas4_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Debe capturar el formato numérico correcto", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private void dgViewTallas3_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Debe capturar el formato numérico correcto", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void dgViewTallas2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Debe capturar el formato numérico correcto", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void dgViewTallas1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Debe capturar el formato numérico correcto", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        private void dgViewTallas4_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.Width = 58;
        }
        private void dgViewTallas1_Leave(object sender, EventArgs e)
        {
            DataGridViewCell cell = dgViewTallas1.CurrentCell;
            if (cell != null)
            {
                cell.Selected = false;
            }
        }

        private void dgViewTallas2_Leave(object sender, EventArgs e)
        {
            DataGridViewCell cell = dgViewTallas2.CurrentCell;
            if (cell != null)
            {
                cell.Selected = false;
            }
        }

        private void dgViewTallas3_Leave(object sender, EventArgs e)
        {
            DataGridViewCell cell = dgViewTallas3.CurrentCell;
            if (cell != null)
            {
                cell.Selected = false;
            }
        }
        private void dgViewTallas4_Leave(object sender, EventArgs e)
        {
            DataGridViewCell cell = dgViewTallas4.CurrentCell;
            if (cell != null)
            {
                cell.Selected = false;
            }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            PED_DET elimina_oed_det = new PED_DET();
            elimina_oed_det.PEDIDO = Pedido;
            elimina_oed_det.AGRUPADOR = Agrupador;
            elimina_oed_det.CODIGO = Modelo;
            elimina_oed_det.Borrar(elimina_oed_det, Enumerados.TipoBorrado.Fisico);

            CMT_DET.ActualizaCMT_CantidadTotal(Pedido, Agrupador, Modelo, totalPrendas);


            ProcesaPrendas(RecorridoTablasTallas.Guardar);
            Cursor.Current = Cursors.Default;
            this.Close();
        }

        private void frmModificarTallas_Activated(object sender, EventArgs e)
        {
            llenaDatosTallas(tallasTotales);
            CalculaTotalPrendas();
        }

        private void dgViewTallas5_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            CalculaTotalPrendas();
        }

        private void dgViewTallas5_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.Width = 58;
        }

        private void dgViewTallas5_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Debe capturar el formato numérico correcto", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void dgViewTallas5_Leave(object sender, EventArgs e)
        {
            DataGridViewCell cell = dgViewTallas5.CurrentCell;
            if (cell != null)
            {
                cell.Selected = false;
            }
        }
    }
}
