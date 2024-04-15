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
using System.Reflection;

namespace SIP
{
    public partial class frmOrdProduccionMasiva : Form
    {
        public enum TipoRequisicionEnum
        {
            Pedido,
            Requisicion
        }

        int selectedRow;
        string pathCB = "";
        public Boolean StatusProceso = false;
        public Boolean generaFlujoOP = false;

        List<OrdenProduccionMasiva> ListaOrdenProduccion = new List<OrdenProduccionMasiva> { };
        List<int> pedidosPorProcesar = new List<int> { };
        List<int> pedidosProcesados = new List<int> { };
        List<int> pedidosExistentes = new List<int> { };
        TipoRequisicionEnum tipoRequisicion;

        public frmOrdProduccionMasiva()
        {
            InitializeComponent();
            this.dgvResumenOrdenes.AutoGenerateColumns = false;
            this.dgvListaTallas.AutoGenerateColumns = false;
            this.tipoRequisicion = TipoRequisicionEnum.Pedido;
        }

        public frmOrdProduccionMasiva(int _pedido, Boolean generaFlujoOP = false, TipoRequisicionEnum tipoRequisicion = TipoRequisicionEnum.Pedido)
        {
            InitializeComponent();
            this.dgvResumenOrdenes.AutoGenerateColumns = false;
            this.dgvListaTallas.AutoGenerateColumns = false;
            dgvOrdenes.Rows.Add();
            dgvOrdenes.Rows[0].Cells[0].Value = _pedido;
            this.tipoRequisicion = tipoRequisicion;
            if (tipoRequisicion == TipoRequisicionEnum.Requisicion)
            {
                txtAlmacen.Text = "1";
            }
            btnProcesaOrdenes_Click(null, null);
            //this.pedidosPorProcesar.Add(_pedido);
            //this.CargaPedidosPorProcesar();
            this.generaFlujoOP = generaFlujoOP;
        }

        private void frmOrdProduccionMasiva_Load(object sender, EventArgs e)
        {

        }

        private void btnProcesaOrdenes_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (dgvOrdenes.Rows.Count == 0)
            {
                MessageBox.Show("Debe de existir al menos 1 orden a procesar.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cursor.Current = Cursors.Default;
                return;
            }

            this.pedidosPorProcesar = new List<int> { };
            this.pedidosProcesados = new List<int> { };
            this.pedidosExistentes = new List<int> { };

            //AGREGAMOS LOS PEDIDOS QUE VAMOS A PROCESAR
            foreach (DataGridViewRow dr in dgvOrdenes.Rows)
            {
                if (dr.Cells["noPedido"].Value != null)
                {
                    try
                    {
                        this.pedidosPorProcesar.Add(int.Parse(dr.Cells["noPedido"].Value.ToString()));
                    }
                    catch { MessageBox.Show("El valor tiene un formato de entrada incorrecto.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }

            //YA QUE TENEMOS LA LISTA DE PEDIDOS, OBTENEMOS LOS MODELOS Y TALLAS DE LA MISMA PARA GENERAR LA ORDEN DE PRODUCIÓN
            this.ListaOrdenProduccion = new List<OrdenProduccionMasiva> { };


            foreach (int _pedido in this.pedidosPorProcesar.Distinct())
            {
                DataTable dt = new DataTable();
                if (tipoRequisicion == TipoRequisicionEnum.Pedido)
                {
                    dt = OrdenProduccionMasiva.GetTablaModelosByPedido(_pedido);
                }
                if (tipoRequisicion == TipoRequisicionEnum.Requisicion)
                {
                    dt = OrdenProduccionMasiva.GetTablaModelosByRequisicion(_pedido);
                }
                if (dt.Rows.Count > 0)
                {
                    try
                    {
                        if (dt.Rows[0]["PEDIDO"].ToString() == "EXISTE")
                        {
                            this.pedidosExistentes.Add(_pedido);
                            continue;
                        }
                    }
                    catch { }
                    //solo en caso de que existan modelos especiales procesamos y agregamos
                    foreach (DataRow dr in dt.Rows)
                    {
                        //VERIFICAMOS QUE EL MODELO EXISTA EN LA LISTA 
                        if (this.ListaOrdenProduccion.Where(x => x.Modelo == dr["MODELO"].ToString()).FirstOrDefault() == null)
                        {
                            //AGREGAMOS EL MODELO A LA OP
                            OrdenProduccionMasiva _ordenProduccion = new OrdenProduccionMasiva();
                            _ordenProduccion.Modelo = dr["MODELO"].ToString();
                            _ordenProduccion.ListaPedidos.Add(_pedido);
                            _ordenProduccion.ListaTallas.Add(new OrdenProduccionMasiva.Talla { talla = dr["TALLA"].ToString(), cantidad = int.Parse(dr["CANTIDAD"].ToString()) });
                            _ordenProduccion.objConfiguracion.observacionesOP = dr["OBSERVACIONES"].ToString();
                            this.ListaOrdenProduccion.Add(_ordenProduccion);
                        }
                        else
                        {
                            //SI EL MODELO YA EXISTE VERIFICAMOS SI YA EXISTE LA ORDEN
                            if (!this.ListaOrdenProduccion.Where(x => x.Modelo == dr["MODELO"].ToString()).FirstOrDefault().ListaPedidos.Contains(_pedido))
                            {
                                this.ListaOrdenProduccion.Where(x => x.Modelo == dr["MODELO"].ToString()).FirstOrDefault().ListaPedidos.Add(_pedido);
                            }

                            //SI EL MODELO YA EXISTE VERIFICAMOS SI LA TALLA EXISTE
                            if (this.ListaOrdenProduccion.Where(x => x.Modelo == dr["MODELO"].ToString()).FirstOrDefault().ListaTallas.Where(y => y.talla == dr["TALLA"].ToString()).FirstOrDefault() != null)
                            {
                                //SI LA TALLA YA EXISTE, AGREGAMOS SOLO CANTIDADES
                                this.ListaOrdenProduccion.Where(x => x.Modelo == dr["MODELO"].ToString()).FirstOrDefault().ListaTallas.Where(y => y.talla == dr["TALLA"].ToString()).FirstOrDefault().cantidad += int.Parse(dr["CANTIDAD"].ToString());
                            }
                            else
                            {
                                //AGREGAMOS LA TALLA
                                this.ListaOrdenProduccion.Where(x => x.Modelo == dr["MODELO"].ToString()).First().ListaTallas.Add(new OrdenProduccionMasiva.Talla { talla = dr["TALLA"].ToString(), cantidad = int.Parse(dr["CANTIDAD"].ToString()) });
                            }

                        }
                        List<OrdenProduccionMasiva.Talla> listaOrdenada = this.ListaOrdenProduccion.Where(x => x.Modelo == dr["MODELO"].ToString()).FirstOrDefault().ListaTallas.OrderBy(x => x.talla).ToList();
                        this.ListaOrdenProduccion.Where(x => x.Modelo == dr["MODELO"].ToString()).FirstOrDefault().ListaTallas = listaOrdenada;
                        this.pedidosProcesados.Add(_pedido);
                    }
                }
            }
            if (this.pedidosExistentes.Count > 0)
            {
                String _message = "";
                foreach (int _pedido in this.pedidosExistentes)
                {
                    _message += _pedido.ToString() + (char)13;
                }
                MessageBox.Show("Los siguientes Pedidos ya han sido procesados con anterioridad" + (char)13 + _message, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }

            if (this.ListaOrdenProduccion.Count == 0)
            {
                MessageBox.Show("No existen MODELOS especiales a procesar.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Cursor.Current = Cursors.Default;
                return;
            }

            this.ListaOrdenProduccion = this.ListaOrdenProduccion.OrderBy(x => x.Modelo).ToList();
            dgvResumenOrdenes.DataSource = this.ListaOrdenProduccion;
            dgvResumenOrdenes.Refresh();
            dgvResumenOrdenes.RefreshEdit();
            Cursor.Current = Cursors.Default;
        }

        private void dgvOrdenes_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            if (dgvOrdenes.CurrentCell.ColumnIndex == 0) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                }
            }
        }
        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dgvResumenOrdenes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return;
            if (dgvResumenOrdenes.Columns[e.ColumnIndex].Name == "Configurar")
            {
                this.selectedRow = e.RowIndex;
                this.dgvListaTallas.DataSource = this.ListaOrdenProduccion[e.RowIndex].ListaTallas;
                this.lblTotalCantidad.Text = this.ListaOrdenProduccion[e.RowIndex].ListaTallas.Where(x => x.procesar).Sum(x => x.cantidad).ToString();
                txtObservaciones.Text = this.ListaOrdenProduccion[dgvResumenOrdenes.CurrentRow.Index].objConfiguracion.observacionesOP;
                txtObservaciones.Select(0, 0);
                gbConfiguracion.Enabled = true;
                txtClaveProveedor.Focus();

            }
        }

        private void btnGuardarConfiguracion_Click(object sender, EventArgs e)
        {
            if (this.ListaOrdenProduccion[this.selectedRow].ListaTallas.Count(x => x.procesar) == 0)
            {
                MessageBox.Show("Debe de existir al menos una talla a procesar", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.ListaOrdenProduccion[this.selectedRow].objConfiguracion.noProveedor = txtClaveProveedor.Text;
            this.ListaOrdenProduccion[this.selectedRow].objConfiguracion.costo = float.Parse(txtCosto.Text == "" ? "0.0" : txtCosto.Text);
            this.ListaOrdenProduccion[this.selectedRow].objConfiguracion.observacionesOM = txtObservaciones.Text;
            this.ListaOrdenProduccion[this.selectedRow].TieneConfiguracion = true;
            //
            dgvResumenOrdenes.DataSource = this.ListaOrdenProduccion;
            dgvResumenOrdenes.Refresh();
            LimpiaConfiguracion();
            gbConfiguracion.Enabled = false;
        }

        private void LimpiaConfiguracion()
        {
            this.txtClaveProveedor.Text = "";
            this.lblNombreProveedor.Text = "";
            this.txtCosto.Text = "0";
            this.txtObservaciones.Text = "";
            this.dgvListaTallas.DataSource = null;
            this.lblTotalCantidad.Text = "0";
            this.txtNumReferencia.Text = "";
            this.lblPathCodigoBarraValor.Text = "";
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            PROV01 prov01 = new PROV01();
            Cursor = Cursors.WaitCursor;
            DataTable dataTableProv01 = prov01.ConsultarTodoParaBusqueda();
            Cursor = Cursors.Default;
            frmBusquedaGenerica frmBusqueda = new frmBusquedaGenerica(dataTableProv01, "NOMBRE", "LOCALIZACION DE PROVEEDORES");
            frmBusqueda.ShowDialog();
            if (frmBusqueda.RenglonSeleccionado != null)
            {
                txtClaveProveedor.Text = frmBusqueda.RenglonSeleccionado["Clave"].ToString().Trim();
                txtClaveProveedor_Leave(sender, new EventArgs());
                txtCosto.Focus();
            }
        }

        private void txtClaveProveedor_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtClaveProveedor.Text))
            {
                if (txtClaveProveedor.Text != "0")
                {
                    PROV01 prov01 = new PROV01();
                    prov01 = prov01.Consultar(txtClaveProveedor.Text, true);

                    if (!string.IsNullOrEmpty(prov01.NOMBRE))
                    {
                        lblNombreProveedor.Text = prov01.NOMBRE;
                    }
                    else
                    {
                        MessageBox.Show("El Proveedor no existe.", "Verifique", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtClaveProveedor.Focus();
                    }
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

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            int referenciaInicial;
            if (txtNumReferencia.Text == "")
            {
                MessageBox.Show("Se debe de indicar el numero de referencia Inicial.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Cursor.Current = Cursors.Default;
                return;
            }

            if (txtAlmacen.Text == "")
            {
                MessageBox.Show("Se debe de indicar el almacén.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Cursor.Current = Cursors.Default;
                return;
            }

            if (this.ListaOrdenProduccion.Where(x => x.TieneConfiguracion == true).Count() == 0)
            {
                MessageBox.Show("Debe de existir al menos 1 Modelo configurado para poder continuar con el proceso.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Cursor.Current = Cursors.Default;
                return;
            }
            else
            {
                //COMENZAMOS A PROCESAR TODOS LOS MODELOS
                int idFlujoOP = 0;
                int referenciaProceso = 0;
                referenciaInicial = int.Parse(txtNumReferencia.Text);
                foreach (OrdenProduccionMasiva _orden in this.ListaOrdenProduccion.Where(x => x.TieneConfiguracion))
                {
                    //GENERAMOS EL GRID DE LAS TALLAS SELECCIONADAS
                    DataTable tallasSeleccionadas = new DataTable();
                    tallasSeleccionadas.Columns.Add(new DataColumn("MODELO", typeof(string)));
                    tallasSeleccionadas.Columns.Add(new DataColumn("TALLA", typeof(string)));
                    tallasSeleccionadas.Columns.Add(new DataColumn("CANTIDAD", typeof(int)));
                    foreach (OrdenProduccionMasiva.Talla _talla in _orden.ListaTallas.Where(x => x.procesar).OrderBy(x => x.talla))
                    {
                        var row = tallasSeleccionadas.NewRow();
                        row["MODELO"] = _orden.Modelo;
                        row["TALLA"] = _talla.talla;
                        row["CANTIDAD"] = _talla.cantidad;
                        tallasSeleccionadas.Rows.Add(row);
                    }
                    //GENERAMOS LA OP                    
                    bool resultado = OrdProduccion.GeneraOrdenProduccionDeLinea(
                                                                            tallasSeleccionadas,
                                                                            referenciaInicial.ToString(),
                                                                            dtFechaVencimiento.Value,
                                                                            _orden.objConfiguracion.observacionesOP,
                                                                            "",
                                                                            Convert.ToInt32(txtAlmacen.Text)
                                                                            );

                    if (resultado)
                    {
                        DataTable dataTableOrden = OrdMaquila2.RegresaOrden(referenciaInicial.ToString());
                        int CVE_DOC;
                        Exception ex = null; ;
                        CVE_DOC = OrdMaquila2.GenerarOrden(
                                                    dataTableOrden,
                                                    dataTableOrden.Rows.Count,
                                                    _orden.ListaTallas.Sum(x => x.cantidad),
                                                    _orden.objConfiguracion.noProveedor.ToString(),
                                                    Convert.ToInt32(_orden.objConfiguracion.costo),
                                                    Convert.ToInt32(txtEsqDeImp.Text),
                                                    _orden.objConfiguracion.observacionesOM,
                                                    int.Parse(txtAlmacen.Text),
                                                    ref ex
                                                    );
                        _orden.OrdenMaquila = CVE_DOC;
                        OrdProduccion.LiberarOrden(referenciaInicial.ToString());
                        // GENERAMOS LOS CODIGOS DE BARRAS
                        DataTable dtTallas = RecOrdProduccionMaquilaCodigoBarras.ConsultaCodigoBarrasByOrdenMaquilaSAE(referenciaInicial.ToString(), CVE_DOC.ToString());
                        List<CodigoBarra> ListaCodigos2 = generaCodigosDeBarra(referenciaInicial.ToString(), CVE_DOC.ToString(), dtTallas);
                        RecOrdProduccionMaquilaCodigoBarras.GuardaCodigoBarras(ListaCodigos2);
                        String ruta = this.pathCB + "//" + _orden.OrdenProduccion.ToString() + "_" + referenciaInicial + "_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".xls";
                        frmCodigoDeBarras frmCodigoDeBarras = new frmCodigoDeBarras("", 0, ListaCodigos2, true, ruta.Replace(".xls", ".pdf"));
                        // GENERAMOS EL EXCEL DE SALIDA
                        RecOrdProduccionMaquilaCodigoBarras.GeneraArchivoExcelDetalle(ListaCodigos2, ruta);
                        if (this.generaFlujoOP)
                        {
                            if (tipoRequisicion == TipoRequisicionEnum.Pedido)
                            {
                                referenciaProceso = FlujoOP.GuardaFlujoOP(String.Format("OP Automática Pedido {0} ({1})", _orden.PedidosString, _orden.Modelo), null, Globales.UsuarioActual.UsuarioUsuario, false);
                            }
                            if (tipoRequisicion == TipoRequisicionEnum.Requisicion)
                            {
                                referenciaProceso = FlujoOP.GuardaFlujoOP(String.Format("OP Automática Requisición de Mostrador {0} ({1})", _orden.PedidosString, _orden.Modelo), null, Globales.UsuarioActual.UsuarioUsuario, false, "RequisicionMostrador");
                            }
                            idFlujoOP = int.Parse(ControlPedidos.getSiguienteIDProceso().Rows[0]["ID"].ToString());
                            // CREAMOS EL FLUJO EN CONTROL FLUJO
                            ControlPedidos.setAltaLineaTiempoPedido(idFlujoOP, "GO", "G", 9, "", Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.OrdenesProduccion, referenciaProceso, "", ref ex);
                            // CREAMOS DETALLE DE OP
                            FlujoOP.GuardaFlujoOPDetale(referenciaProceso, _orden.Modelo, referenciaInicial, CVE_DOC, Globales.UsuarioActual.UsuarioUsuario);
                        }
                    }
                    _orden.OrdenProduccion = referenciaInicial;
                    referenciaInicial++;
                }
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Se han procesado de forma correcta las Ordenes de Producción", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.StatusProceso = true;
                //DAMOS DE ALTA CADA PEDIDO PROCESADO
                foreach (int _pedido in this.pedidosProcesados)
                {
                    OrdenProduccionMasiva.InsertaPedidoOPMasiva(_pedido);
                }
                //UNA VEZ QUE TERMINAMOS GENERAMOS EL EXCEL DE SALIDA
                string archivoTemporal = System.IO.Path.GetTempFileName().Replace(".tmp", ".xls");
                OrdenProduccionMasiva.GeneraArchivoExcel(archivoTemporal, this.ListaOrdenProduccion);
                // ACTUALIZAMOS COLUMNA DE REPORTE EN FLUJOOP
                Byte[] file = System.IO.File.ReadAllBytes(archivoTemporal);
                FlujoOP.ActualizaFlujoOP(referenciaProceso, file, 4);
                FuncionalidadesFormularios.MostrarExcel(archivoTemporal);
                LimpiaConfiguracion();
                LimpiaFormulario();
            }
        }

        private void LimpiaFormulario()
        {
            this.ListaOrdenProduccion = new List<OrdenProduccionMasiva> { };
            dgvResumenOrdenes.DataSource = this.ListaOrdenProduccion;
            dgvOrdenes.Rows.Clear();
        }

        private void txtNumReferencia_Leave(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            if (OrdProduccion.ReferenciaUtilizada(txtNumReferencia.Text))
            {
                Cursor = Cursors.Default;
                MessageBox.Show("La referencia ya ha sido utilizada anteriormente. Por favor inténtelo nuevamente", "Verifique", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtNumReferencia.Focus();
                btnGenerar.Enabled = false;
            }
            else if (pathCB != "")
                btnGenerar.Enabled = true;
            Cursor = Cursors.Default;
        }

        private void dgvResumenOrdenes_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            this.ListaOrdenProduccion[e.RowIndex].objConfiguracion.observacionesOP = dgvResumenOrdenes[e.ColumnIndex, e.RowIndex].Value == null ? "" : dgvResumenOrdenes[e.ColumnIndex, e.RowIndex].Value.ToString();
            dgvResumenOrdenes.Refresh();
        }


        private void dgvListaTallas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvListaTallas.Columns[e.ColumnIndex].Name == "Procesar")
            {
                this.ListaOrdenProduccion[dgvResumenOrdenes.CurrentRow.Index].ListaTallas[e.RowIndex].procesar = Boolean.Parse(dgvListaTallas[e.ColumnIndex, e.RowIndex].Value.ToString());
                dgvListaTallas.Refresh();
                this.lblTotalCantidad.Text = this.ListaOrdenProduccion[dgvResumenOrdenes.CurrentRow.Index].ListaTallas.Where(x => x.procesar).Sum(y => y.cantidad).ToString();
                dgvListaTallas.Refresh();
                txtObservaciones.Focus();

            }
        }

        private void dgvListaTallas_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            this.lblTotalCantidad.Text = this.ListaOrdenProduccion[dgvResumenOrdenes.CurrentRow.Index].ListaTallas.Where(x => x.procesar).Sum(y => y.cantidad).ToString();
        }

        private void txtObservaciones_Enter(object sender, EventArgs e)
        {
            txtObservaciones.SelectAllOnFocus = false;
            txtObservaciones.SelectionStart = 0;
            txtObservaciones.SelectionLength = 0;
            txtObservaciones.Refresh();
        }

        private void dgvResumenOrdenes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvResumenOrdenes_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((dgvResumenOrdenes.Rows[e.RowIndex].DataBoundItem != null) &&
      (dgvResumenOrdenes.Columns[e.ColumnIndex].DataPropertyName.Contains(".")))
            {
                e.Value = BindProperty(
                              dgvResumenOrdenes.Rows[e.RowIndex].DataBoundItem,
                              dgvResumenOrdenes.Columns[e.ColumnIndex].DataPropertyName
                            );
            }

        }

        private string BindProperty(object property, string propertyName)
        {
            string retValue = "";

            if (propertyName.Contains("."))
            {
                PropertyInfo[] arrayProperties;
                string leftPropertyName;

                leftPropertyName = propertyName.Substring(0, propertyName.IndexOf("."));
                arrayProperties = property.GetType().GetProperties();

                foreach (PropertyInfo propertyInfo in arrayProperties)
                {
                    if (propertyInfo.Name == leftPropertyName)
                    {
                        retValue = BindProperty(
                          propertyInfo.GetValue(property, null),
                          propertyName.Substring(propertyName.IndexOf(".") + 1));
                        break;
                    }
                }
            }
            else
            {
                Type propertyType;
                PropertyInfo propertyInfo;

                propertyType = property.GetType();
                propertyInfo = propertyType.GetProperty(propertyName);
                retValue = propertyInfo.GetValue(property, null).ToString();
            }

            return retValue;
        }

        private void dgvResumenOrdenes_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvResumenOrdenes_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridViewTextBoxEditingControl tb = (DataGridViewTextBoxEditingControl)e.Control;
            tb.KeyPress += new KeyPressEventHandler(dgvResumenOrdenes_KeyPress);
            e.Control.KeyPress += new KeyPressEventHandler(dgvResumenOrdenes_KeyPress);
        }

        private void dgvResumenOrdenes_KeyPress(object sender, KeyPressEventArgs e)
        {
            DataGridViewTextBoxEditingControl _sender = (DataGridViewTextBoxEditingControl)sender;
            if (_sender.Text.Length > 244)
            {
                if (e.KeyChar != Convert.ToChar(Keys.Back) && e.KeyChar != Convert.ToChar(Keys.Enter))
                {
                    e.Handled = true;
                }
            }

        }



        private void CargaPedidosPorProcesar()
        {
            foreach (int _pedido in this.pedidosPorProcesar.Distinct())
            {
                DataTable dt = new DataTable();
                dt = OrdenProduccionMasiva.GetTablaModelosByPedido(_pedido);
                if (dt.Rows.Count > 0)
                {
                    try
                    {
                        if (dt.Rows[0]["PEDIDO"].ToString() == "EXISTE")
                        {
                            this.pedidosExistentes.Add(_pedido);
                            continue;
                        }
                    }
                    catch { }
                    //solo en caso de que existan modelos especiales procesamos y agregamos
                    foreach (DataRow dr in dt.Rows)
                    {
                        //VERIFICAMOS QUE EL MODELO EXISTA EN LA LISTA 
                        if (this.ListaOrdenProduccion.Where(x => x.Modelo == dr["MODELO"].ToString()).FirstOrDefault() == null)
                        {
                            //AGREGAMOS EL MODELO A LA OP
                            OrdenProduccionMasiva _ordenProduccion = new OrdenProduccionMasiva();
                            _ordenProduccion.Modelo = dr["MODELO"].ToString();
                            _ordenProduccion.ListaPedidos.Add(_pedido);
                            _ordenProduccion.ListaTallas.Add(new OrdenProduccionMasiva.Talla { talla = dr["TALLA"].ToString(), cantidad = int.Parse(dr["CANTIDAD"].ToString()) });
                            _ordenProduccion.objConfiguracion.observacionesOP = dr["OBSERVACIONES"].ToString();
                            this.ListaOrdenProduccion.Add(_ordenProduccion);
                        }
                        else
                        {
                            //SI EL MODELO YA EXISTE VERIFICAMOS SI YA EXISTE LA ORDEN
                            if (!this.ListaOrdenProduccion.Where(x => x.Modelo == dr["MODELO"].ToString()).FirstOrDefault().ListaPedidos.Contains(_pedido))
                            {
                                this.ListaOrdenProduccion.Where(x => x.Modelo == dr["MODELO"].ToString()).FirstOrDefault().ListaPedidos.Add(_pedido);
                            }

                            //SI EL MODELO YA EXISTE VERIFICAMOS SI LA TALLA EXISTE
                            if (this.ListaOrdenProduccion.Where(x => x.Modelo == dr["MODELO"].ToString()).FirstOrDefault().ListaTallas.Where(y => y.talla == dr["TALLA"].ToString()).FirstOrDefault() != null)
                            {
                                //SI LA TALLA YA EXISTE, AGREGAMOS SOLO CANTIDADES
                                this.ListaOrdenProduccion.Where(x => x.Modelo == dr["MODELO"].ToString()).FirstOrDefault().ListaTallas.Where(y => y.talla == dr["TALLA"].ToString()).FirstOrDefault().cantidad += int.Parse(dr["CANTIDAD"].ToString());
                            }
                            else
                            {
                                //AGREGAMOS LA TALLA
                                this.ListaOrdenProduccion.Where(x => x.Modelo == dr["MODELO"].ToString()).First().ListaTallas.Add(new OrdenProduccionMasiva.Talla { talla = dr["TALLA"].ToString(), cantidad = int.Parse(dr["CANTIDAD"].ToString()) });
                            }

                        }
                        List<OrdenProduccionMasiva.Talla> listaOrdenada = this.ListaOrdenProduccion.Where(x => x.Modelo == dr["MODELO"].ToString()).FirstOrDefault().ListaTallas.OrderBy(x => x.talla).ToList();
                        this.ListaOrdenProduccion.Where(x => x.Modelo == dr["MODELO"].ToString()).FirstOrDefault().ListaTallas = listaOrdenada;
                        this.pedidosProcesados.Add(_pedido);
                    }
                }
            }
            if (this.pedidosExistentes.Count > 0)
            {
                String _message = "";
                foreach (int _pedido in this.pedidosExistentes)
                {
                    _message += _pedido.ToString() + (char)13;
                }
                MessageBox.Show("Los siguientes Pedidos ya han sido procesados con anterioridad" + (char)13 + _message, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }

            if (this.ListaOrdenProduccion.Count == 0)
            {
                MessageBox.Show("No existen MODELOS especiales a procesar.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Cursor.Current = Cursors.Default;
                return;
            }




            dgvResumenOrdenes.DataSource = this.ListaOrdenProduccion;
            dgvResumenOrdenes.Refresh();
            dgvResumenOrdenes.RefreshEdit();
            Cursor.Current = Cursors.Default;
        }

        private void btnPathCodigoBarra_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lblPathCodigoBarraValor.Text = folderBrowserDialog.SelectedPath;
                this.pathCB = folderBrowserDialog.SelectedPath;
                if (txtNumReferencia.Text != "")
                    btnGenerar.Enabled = true;
            }

        }

        List<CodigoBarra> generaCodigosDeBarra(string pedido, string ordenMaquila, DataTable tallasSeleccionadas)
        {
            DataTable dtCModelosEspeciales = RecOrdProduccionMaquilaCodigoBarras.ConsultaCodigoDeBarrasModelosEspeciales();
            List<CodigoBarra> codigos = new List<CodigoBarra> { };
            int cantidadTotal;
            int consecutivo = 0;
            int contador = 0;
            int cantidadAgrupada = 0;
            foreach (DataRow dr in tallasSeleccionadas.Rows)
            {
                cantidadTotal = int.Parse(dr["CANTIDAD"].ToString());
                var result = dtCModelosEspeciales.Select("Modelo = '" + dr["MODELO"].ToString() + "'").FirstOrDefault();
                if (result != null)
                    cantidadAgrupada = int.Parse(result["CantidadAgrupada"].ToString());
                else
                    cantidadAgrupada = 10;

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
                    objCodigo.Almacen = (int)dr["ALMACEN"];
                    objCodigo.Modelo = dr["MODELO"].ToString();
                    objCodigo.Descripcion = dr["DESCRIPCION"].ToString();
                    objCodigo.Talla = dr["TALLA"].ToString();
                    objCodigo.Tipo = (int)dr["ALMACEN"] == 32 ? "E" : "L";
                    objCodigo.Cantidad = cantidadAgrupada;
                    objCodigo.FechaGeneracion = DateTime.Now;
                    codigos.Add(objCodigo);

                }
                if ((cantidadTotal - ((cantidadTotal / cantidadAgrupada) * cantidadAgrupada)) > 0)
                {
                    consecutivo++;
                    contador++;
                    CodigoBarra objCodigo = new CodigoBarra();
                    objCodigo.UUID = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
                    objCodigo.Consecutivo = consecutivo;
                    objCodigo.Contador = contador;
                    objCodigo.Referencia = pedido;
                    objCodigo.OrdenMaquila = ordenMaquila;
                    objCodigo.Almacen = (int)dr["ALMACEN"]; ;
                    objCodigo.Modelo = dr["MODELO"].ToString();
                    objCodigo.Descripcion = dr["DESCRIPCION"].ToString();
                    objCodigo.Talla = dr["TALLA"].ToString();
                    objCodigo.Tipo = (int)dr["ALMACEN"] == 32 ? "E" : "L";
                    objCodigo.Cantidad = (cantidadTotal - ((cantidadTotal / cantidadAgrupada) * cantidadAgrupada));
                    objCodigo.FechaGeneracion = DateTime.Now;
                    codigos.Add(objCodigo);
                }
            }




            return codigos;
        }

        private void txtObservaciones_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtObservaciones.Text.Trim().Length > 255)
            {
                e.Handled = true;
            }
        }

    }
}
