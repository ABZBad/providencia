using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ulp_bl;

namespace SIP
{
    public partial class frmOrdProduccion : Form
    {
        DataSet tablasTallas;
        DataTable dataTableTallas = new DataTable();
        DataTable tallasSeleccionadas = new DataTable();
        public bool StatusProceso;
        public frmOrdProduccion()
        {
            InitializeComponent();
            this.StatusProceso = false;
        }

        private void frmOrdProduccion_Load(object sender, EventArgs e)
        {
            dtFechaVencimiento.Value = dtFechaVencimiento.Value.AddDays(30);
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
                    longitudCol = tallas.Rows[tot]["CLAVE"].ToString().Length;
                    columna.Caption = tallas.Rows[tot]["CLAVE"].ToString().Substring(8, longitudCol - 8);
                    columna.ColumnName = tallas.Rows[tot]["CLAVE"].ToString().Substring(8, longitudCol - 8);
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
        private void txtNumReferencia_Leave(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            if (OrdProduccion.ReferenciaUtilizada(txtNumReferencia.Text))
            {
                Cursor = Cursors.Default;
                MessageBox.Show("La referencia ya ha sido utilizada anteriormente. Por favor inténtelo nuevamente", "Verifique", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtNumReferencia.Focus();
            }
            Cursor = Cursors.Default;
        }

        private void txtModelo_Leave(object sender, EventArgs e)
        {
            if (txtModelo.Text.Trim() != string.Empty)
            {
                Cursor = Cursors.WaitCursor;
                dataTableTallas = OrdProduccion.RegresaTablaTallas(txtModelo.Text, txtAlmacen.Text);
                bool tallasEncontradas = llenaDatosTallas(dataTableTallas);
                Cursor = Cursors.Default;
                if (!tallasEncontradas)
                {
                    MessageBox.Show("Modelo incorrecto, inexistente, o no existe en el almacén seleccionado",
                        "Verifique", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    LimpiaControles();
                    txtModelo.Focus();
                }
            }
        }

        private void txtAlmacen_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAlmacen.Text))
            {
                txtAlmacen.Text = "1";
            }
            if (!string.IsNullOrEmpty(txtModelo.Text))
            {
                txtModelo_Leave(sender, e);
            }
        }

        private void LimpiaControles()
        {
            dgViewTallas1.DataSource = null;
            dgViewTallas2.DataSource = null;
            dgViewTallas3.DataSource = null;
            dgViewTallas4.DataSource = null;
            tallasSeleccionadas.Clear();
            txtNumReferencia.Text = "";
            txtPedido.Text = "";
            txtModelo.Text = "";
            //txtAlmacen.Text = "1";
            lblTotal.Text = "0";
            txtObservaciones.Text = "";

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
                        row["MODELO"] = txtModelo.Text;
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
            try
            {
                DialogResult resp =
                    MessageBox.Show(
                        "Se va a generar: Orden de Producción, Orden de Maquila y la Liberación de la Orden\n¿ Desea Continuar ?",
                        "Confirme", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (resp == DialogResult.No)
                    return;


                AsignaTallasSeleccionadasPorGrid();

                if (tallasSeleccionadas.Rows.Count == 0) { MessageBox.Show("No hay tallas seleccionadas"); return; }

                bool resultado = OrdProduccion.GeneraOrdenProduccionDeLinea(
                                                                            tallasSeleccionadas,
                                                                            txtNumReferencia.Text,
                                                                            dtFechaVencimiento.Value,
                                                                            txtObservaciones.Text,
                                                                            txtPedido.Text,
                                                                            Convert.ToInt32(txtAlmacen.Text)
                                                                            );

                if (resultado)
                {
                    MessageBox.Show("Orden de Producción generada", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmOrdMaquila2 frmOrdMaquila2 = new SIP.frmOrdMaquila2(txtNumReferencia.Text, int.Parse(txtAlmacen.Text));
                    frmOrdMaquila2.ShowDialog();
                    if (frmOrdMaquila2.CVE_DOC > 0)
                    {
                        if (frmOrdMaquila2.CVE_DOC != frmOrdMaquila2.CVE_DOC_SUGGESTED)
                        {
                            MessageBox.Show(string.Format("Por actualización en red, la Orden de Compra de Maquila\nha sido guardada con el Folio '{0}'", frmOrdMaquila2.CVE_DOC), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(string.Format("La Orden de Compra de Maquila\nha sido guardada con el Folio '{0}'", frmOrdMaquila2.CVE_DOC), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }

                    string resultadoLiberarOrden = OrdProduccion.LiberarOrden(txtNumReferencia.Text);
                    if (resultadoLiberarOrden != "")
                    {
                        MessageBox.Show(resultadoLiberarOrden, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    List<CodigoBarra> _ListaCodigos = new List<CodigoBarra> { };
                    _ListaCodigos = generaCodigosDeBarra(txtNumReferencia.Text, frmOrdMaquila2.CVE_DOC.ToString(), Convert.ToInt32(txtAlmacen.Text), tallasSeleccionadas);
                    RecOrdProduccionMaquilaCodigoBarras.GuardaCodigoBarras(_ListaCodigos);
                    //ACTUALIZACION. ABRIMOS PANEL DE GENERACION DE CODIGO DE BARRAS DE LA OP
                    frmCodigoDeBarras frmCodigoDeBarras = new SIP.frmCodigoDeBarras(txtNumReferencia.Text, frmOrdMaquila2.CVE_DOC, _ListaCodigos);
                    frmCodigoDeBarras.ShowDialog();

                    //GENERAMOS EL EXCEL DE SALIDA
                    generaExcelCodigoBarra(_ListaCodigos);

                    DialogResult resp2 = MessageBox.Show("¿Desea agregar otra órden de producción?", "Confirme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resp2 == System.Windows.Forms.DialogResult.Yes)
                    {
                        LimpiaControles();
                        txtNumReferencia.Focus();
                    }
                    else
                    {
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al generar la Orden de Compra, consulte con su Adminsitrador de Sistema: '{0}'", ex.Message), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        private void dgViewTallas2_Enter(object sender, EventArgs e)
        {
            if (dgViewTallas2.Rows.Count > 0)
            {
                if (dgViewTallas2.Rows[0].Cells.Count > 0)
                {
                    dgViewTallas2.CurrentCell = dgViewTallas2.Rows[0].Cells[0];
                }
            }
        }

        private void dgViewTallas1_Enter(object sender, EventArgs e)
        {
            if (dgViewTallas1.Rows.Count > 0)
            {
                if (dgViewTallas1.Rows[0].Cells.Count > 0)
                {
                    dgViewTallas1.CurrentCell = dgViewTallas1.Rows[0].Cells[0];
                }
            }
        }

        private void dgViewTallas3_Enter(object sender, EventArgs e)
        {
            if (dgViewTallas3.Rows.Count > 0)
            {
                if (dgViewTallas3.Rows[0].Cells.Count > 0)
                {
                    dgViewTallas3.CurrentCell = dgViewTallas3.Rows[0].Cells[0];
                }
            }
        }
        private void dgViewTallas4_Enter(object sender, EventArgs e)
        {
            if (dgViewTallas4.Rows.Count > 0)
            {
                if (dgViewTallas4.Rows[0].Cells.Count > 0)
                {
                    dgViewTallas4.CurrentCell = dgViewTallas4.Rows[0].Cells[0];
                }
            }
        }

        public List<CodigoBarra> generaCodigosDeBarra(string pedido, string ordenMaquila, int almacen, DataTable tallasSeleccionadas)
        {
            DataTable dtCModelosEspeciales = RecOrdProduccionMaquilaCodigoBarras.ConsultaCodigoDeBarrasModelosEspeciales();
            List<CodigoBarra> codigos = new List<CodigoBarra> { };
            int cantidadTotal;
            int consecutivo = 0;
            int contador = 0;
            int cantidadAgrupada = 0;

            //VERIFICAMOS SI EL MODELO ES DE AGRUPADOR ESPECIAL


            foreach (DataRow dr in tallasSeleccionadas.Rows)
            {
                consecutivo = 0;
                cantidadTotal = (int)dr["CANTIDAD"];               

                var result = dtCModelosEspeciales.Select("Modelo = '" + dr["MODELO"].ToString() + "'").FirstOrDefault();

                if (result != null)
                    cantidadAgrupada = int.Parse(result["CantidadAgrupada"].ToString());
                else
                    cantidadAgrupada = 10;

                var query = from row in this.dataTableTallas.AsEnumerable() where row.Field<String>("CLAVE") == dr["MODELO"].ToString() + dr["TALLA"].ToString() select row;
                for (int i = 1; i <= cantidadTotal / cantidadAgrupada; i++)
                {
                    consecutivo = i;
                    contador++;
                    CodigoBarra objCodigo = new CodigoBarra();
                    objCodigo.UUID = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
                    objCodigo.Consecutivo = consecutivo;
                    objCodigo.Contador = contador;
                    objCodigo.Referencia = pedido;
                    objCodigo.OrdenMaquila = ordenMaquila;
                    objCodigo.Almacen = almacen;
                    objCodigo.Modelo = dr["MODELO"].ToString();
                    objCodigo.Descripcion = query.ToList()[0]["DESCR"].ToString();
                    objCodigo.Talla = dr["TALLA"].ToString();
                    objCodigo.Tipo = almacen == 32 ? "E" : "L";
                    objCodigo.Cantidad = cantidadAgrupada;
                    objCodigo.FechaGeneracion = DateTime.Now;
                    codigos.Add(objCodigo);
                    
                }
                if ((cantidadTotal - ((cantidadTotal / cantidadAgrupada) * cantidadAgrupada)) > 0)
                {
                    contador++;
                    consecutivo++;
                    CodigoBarra objCodigo = new CodigoBarra();
                    objCodigo.UUID = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
                    objCodigo.Consecutivo = consecutivo;
                    objCodigo.Contador = contador;
                    objCodigo.Referencia = pedido;
                    objCodigo.OrdenMaquila = ordenMaquila;
                    objCodigo.Almacen = almacen;
                    objCodigo.Modelo = dr["MODELO"].ToString();
                    objCodigo.Descripcion = query.ToList()[0]["DESCR"].ToString();
                    objCodigo.Talla = dr["TALLA"].ToString();
                    objCodigo.Tipo = almacen == 32 ? "E" : "L";
                    objCodigo.Cantidad = (cantidadTotal - ((cantidadTotal / cantidadAgrupada) * cantidadAgrupada));
                    objCodigo.FechaGeneracion = DateTime.Now;
                    codigos.Add(objCodigo);
                }
            }


            //CONSIDERAMOS QUE ALMACEN 32 ES ESPECIALES, CUALQUIER OTRO ALMACEN ES LINEA
            /*switch (almacen)
            {
                case 32:
                    //SE GENERA CODIGO DE BARRAS POR MODELO (EN MULTIPLOS DE 10)
                    cantidadTotal = int.Parse(tallasSeleccionadas.Compute("Sum(CANTIDAD)", "").ToString());
                    for (int i = 1; i <= cantidadTotal / 10; i++)
                    {
                        consecutivo = i;
                        CodigoBarra objCodigo = new CodigoBarra();
                        objCodigo.UUID = Guid.NewGuid().ToString().Substring(0, 5).ToUpper();
                        objCodigo.Consecutivo = consecutivo;
                        objCodigo.Referencia = pedido;
                        objCodigo.OrdenMaquila = ordenMaquila;
                        objCodigo.Almacen = almacen;
                        objCodigo.Modelo = tallasSeleccionadas.Rows[0]["MODELO"].ToString();
                        objCodigo.Descripcion = tallasSeleccionadas.Rows[0]["DESCR"].ToString();
                        objCodigo.Talla = tallasSeleccionadas.Rows[0]["TALLA"].ToString();
                        objCodigo.Tipo = "E";
                        objCodigo.Cantidad = 10;
                        objCodigo.FechaGeneracion = DateTime.Now;
                        codigos.Add(objCodigo);
                    }
                    consecutivo++;
                    if ((cantidadTotal - ((cantidadTotal / 10) * 10)) > 0)
                    {
                        CodigoBarra objCodigo = new CodigoBarra();
                        objCodigo.UUID = Guid.NewGuid().ToString().Substring(0, 5).ToUpper();
                        objCodigo.Consecutivo = consecutivo;
                        objCodigo.Referencia = pedido;
                        objCodigo.OrdenMaquila = ordenMaquila;
                        objCodigo.Almacen = almacen;
                        objCodigo.Modelo = tallasSeleccionadas.Rows[0]["MODELO"].ToString();
                        objCodigo.Talla = tallasSeleccionadas.Rows[0]["TALLA"].ToString();
                        objCodigo.Tipo = "E";
                        objCodigo.Cantidad = (cantidadTotal - ((cantidadTotal / 10) * 10));
                        objCodigo.FechaGeneracion = DateTime.Now;
                        codigos.Add(objCodigo);
                    }
                    break;
                default:
                    //SE GENERA CODIGO DE BARRAS POR MODELO POR TALLA EN MULTIPLOS DE 10
                    foreach (DataRow dr in tallasSeleccionadas.Rows)
                    {
                        cantidadTotal = (int)dr["CANTIDAD"];
                        var query = from row in this.dataTableTallas.AsEnumerable() where row.Field<String>("CLAVE") == dr["MODELO"].ToString() + dr["TALLA"].ToString() select row;
                        for (int i = 1; i <= cantidadTotal / 10; i++)
                        {
                            consecutivo = i;
                            CodigoBarra objCodigo = new CodigoBarra();
                            objCodigo.UUID = Guid.NewGuid().ToString().Substring(0,5).ToUpper();
                            objCodigo.Consecutivo = consecutivo;
                            objCodigo.Referencia = pedido;
                            objCodigo.OrdenMaquila = ordenMaquila;
                            objCodigo.Almacen = almacen;
                            objCodigo.Modelo = dr["MODELO"].ToString();
                            objCodigo.Descripcion = query.ToList()[0]["DESCR"].ToString();
                            objCodigo.Talla = dr["TALLA"].ToString();
                            objCodigo.Tipo = "L";
                            objCodigo.Cantidad = 10;
                            objCodigo.FechaGeneracion = DateTime.Now;
                            codigos.Add(objCodigo);
                        }
                        consecutivo++;
                        if ((cantidadTotal - ((cantidadTotal / 10) * 10)) > 0)
                        {
                            CodigoBarra objCodigo = new CodigoBarra();
                            objCodigo.UUID = Guid.NewGuid().ToString().Substring(0, 5).ToUpper();
                            objCodigo.Consecutivo = consecutivo;
                            objCodigo.Referencia = pedido;
                            objCodigo.OrdenMaquila = ordenMaquila;
                            objCodigo.Almacen = almacen;
                            objCodigo.Modelo = dr["MODELO"].ToString();
                            objCodigo.Descripcion = query.ToList()[0]["DESCR"].ToString();
                            objCodigo.Talla = dr["TALLA"].ToString();
                            objCodigo.Tipo = "L";
                            objCodigo.Cantidad = (cantidadTotal - ((cantidadTotal / 10) * 10));
                            objCodigo.FechaGeneracion = DateTime.Now;
                            codigos.Add(objCodigo);
                        }
                    }

                    break;
            }*/

            return codigos;
        }

        void generaExcelCodigoBarra(List<CodigoBarra> ListaCodigo)
        {
            System.Windows.Forms.SaveFileDialog _file = new SaveFileDialog();
            _file.Filter = "Archivo de Excel (.xls) | *.xls";
            _file.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            _file.FileName = ListaCodigo[0].Referencia.ToString() + "_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".xls";
            
            if (_file.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //string ruta = (ListaCodigo[0].OrdenMaquila.ToString() + "_" + Path()).Replace(".tmp", ".xls");
                //string ruta = (Path.GetTempPath() + "//" + ListaCodigo[0].OrdenMaquila.ToString() + "_" + Path.GetFileName(Path.GetTempFileName())).Replace(".tmp", ".xls");
                String ruta = _file.FileName;
                RecOrdProduccionMaquilaCodigoBarras.GeneraArchivoExcelDetalle(ListaCodigo, ruta);
                Utiles.FuncionalidadesFormularios.MostrarExcel(ruta);
            }
            
        }
    }
}
