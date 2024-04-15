using SIP.Utiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ulp_bl;
using Excel = Microsoft.Office.Interop.Excel;

namespace SIP
{
    public partial class frmFlujoOP : Form
    {
        #region ATRIBUTOS Y CONSTRUCTORES
        OpenFileDialog ofd = new OpenFileDialog();
        int id = 0;
        DataTable dtFlujo = new DataTable();
        DataSet dsFlujoDetalle = new DataSet();
        DataSet dsPrograma = new DataSet();
        bool soloConsulta = false;
        bool cargaPrograma = false;
        bool cargaFecha = false;

        public bool hojaCuotasCargada = false;
        public bool hojaExistenciasCargada = false;
        public bool hojaPropuestaCargada = false;
        public bool hojaObservacionesCargada = false;
        public bool programaProduccionCreado = false;
        public bool fechasAsignadas = false;
        public bool bloqueaConsultaCuotas = false;


        int selectedRow;
        string pathCB = "";

        DateTimePicker dateTimePickerFechaEntrega;

        public frmFlujoOP()
        {
            InitializeComponent();
        }
        public frmFlujoOP(int id)
        {
            InitializeComponent();
            this.id = id;
        }
        public frmFlujoOP(int id, bool soloConsulta)
        {
            InitializeComponent();
            this.id = id;
            this.soloConsulta = soloConsulta;
        }
        public frmFlujoOP(int id, bool soloConsulta, bool cargaPrograma)
        {
            InitializeComponent();
            this.dgvResumenOrdenes.AutoGenerateColumns = false;
            this.dgvListaTallas.AutoGenerateColumns = false;
            this.id = id;
            this.soloConsulta = soloConsulta;
            this.cargaPrograma = cargaPrograma;
        }
        public frmFlujoOP(int id, bool soloConsulta, bool cargaPrograma, bool cargaFecha)
        {
            InitializeComponent();
            this.dgvFlujoOPDetalle.AutoGenerateColumns = false;
            this.id = id;
            this.soloConsulta = soloConsulta;
            this.cargaPrograma = cargaPrograma;
            this.cargaFecha = cargaFecha;
        }
        public frmFlujoOP(int id, bool soloConsulta, bool cargaPrograma, bool cargaFecha, bool programaProduccionCreado)
        {
            InitializeComponent();
            this.id = id;
            this.programaProduccionCreado = programaProduccionCreado;
            this.soloConsulta = soloConsulta;
            this.cargaPrograma = cargaPrograma;
            this.cargaFecha = cargaFecha;
        }
        public frmFlujoOP(int id, bool soloConsulta, bool cargaPrograma, bool cargaFecha, bool programaProduccionCreado, bool bloqueaConsultaCuotas)
        {
            InitializeComponent();
            this.id = id;
            this.programaProduccionCreado = programaProduccionCreado;
            this.soloConsulta = soloConsulta;
            this.cargaPrograma = cargaPrograma;
            this.cargaFecha = cargaFecha;
            this.bloqueaConsultaCuotas = bloqueaConsultaCuotas;
        }


        List<OrdenProduccionMasiva> ListaOrdenProduccion = new List<OrdenProduccionMasiva> { };
        #endregion
        #region EVENTOS
        private void frmFlujoOP_Load(object sender, EventArgs e)
        {
            if (this.id != 0)
            {
                // OBTENEMOS EL REGISTRO DE LA OP
                this.dtFlujo = this.CargaOP(this.id);
                this.CaragComponentes(this.dtFlujo);
                // OBTENEMOS DETALLE DE PROGRAMA PARA FECHAS DE ENTREGA
                if (this.cargaFecha || this.programaProduccionCreado)
                {
                    this.dsFlujoDetalle = this.CargaOPDetalle(this.id);
                    CargaComponentesFechaEntrega(this.dsFlujoDetalle);
                }
            }
            this.HabilitaComponentes();
            this.dgvFlujoOPDetalle.AutoGenerateColumns = false;
        }

        private void btnExcelCuotas_Click(object sender, EventArgs e)
        {
            ofd.Filter = "Archivo Excel (xls) | *.xlsx";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (ofd.FileName != "")
                {
                    lblArchivoCuotas.Text = ofd.FileName;
                }
            }
        }
        private void btnGuardarHojaCuotas_Click(object sender, EventArgs e)
        {
            if (txtReferencia.Text.Trim() != String.Empty && (ofd.FileName != "" || this.chkFaltantes.Checked))
            {
                Byte[] file = null;
                if (!chkFaltantes.Checked)
                {
                    file = System.IO.File.ReadAllBytes(ofd.FileName);
                }
                int referenciaProceso = FlujoOP.GuardaFlujoOP(txtReferencia.Text.Trim(), file, Globales.UsuarioActual.UsuarioUsuario, this.chkFaltantes.Checked);
                int id = int.Parse(ControlPedidos.getSiguienteIDProceso().Rows[0]["ID"].ToString());
                // CREAMOS EL FLUJO EN CONTROL FLUJO
                Exception ex = null;
                if (!chkFaltantes.Checked)
                {
                    ControlPedidos.setAltaLineaTiempoPedido(id, "GO", "G", 1, "", Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.OrdenesProduccion, referenciaProceso, "", ref ex);
                }
                else
                {
                    ControlPedidos.setAltaLineaTiempoPedido(id, "CA", "G", 1, "", Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.OrdenesProduccionFaltante, referenciaProceso, "", ref ex);
                }

                MessageBox.Show("Programa de producción generado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.hojaCuotasCargada = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Se deben de llenar todos los campos.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnConsultarHojaCuotas_Click(object sender, EventArgs e)
        {
            if (dtFlujo.Rows.Count > 0)
            {
                DataRow dr = dtFlujo.Rows[0];
                this.MostrarExcel((byte[])dr["HojaCuotas"]);
            }
        }

        private void btnExcelExistencias_Click(object sender, EventArgs e)
        {
            ofd.Filter = "Archivo Excel (xls) | *.xlsx";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (ofd.FileName != "")
                {
                    lblArchivoExistencias.Text = ofd.FileName;
                }
            }
        }
        private void btnGuardarHojaExistencias_Click(object sender, EventArgs e)
        {
            if (txtReferencia.Text.Trim() != String.Empty && ofd.FileName != "")
            {
                Byte[] file = System.IO.File.ReadAllBytes(ofd.FileName);
                FlujoOP.ActualizaFlujoOP(this.id, file, 2);
                MessageBox.Show("Hoja de existencias cargada de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.hojaExistenciasCargada = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Se deben de llenar todos los campos.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnConsultarHojaExistencias_Click(object sender, EventArgs e)
        {
            if (dtFlujo.Rows.Count > 0)
            {
                DataRow dr = dtFlujo.Rows[0];
                this.MostrarExcel((byte[])dr["HojaExistencias"]);
            }
        }

        private void btnExcelPropuesta_Click(object sender, EventArgs e)
        {
            ofd.Filter = "Archivo Excel (xls) | *.xlsx";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (ofd.FileName != "")
                {
                    lblArchivoPropuesta.Text = ofd.FileName;
                }
            }
        }
        private void btnGuardarHojaPropuesta_Click(object sender, EventArgs e)
        {
            if (txtReferencia.Text.Trim() != String.Empty && ofd.FileName != "")
            {
                Byte[] file = System.IO.File.ReadAllBytes(ofd.FileName);
                FlujoOP.ActualizaFlujoOP(this.id, file, 3);
                MessageBox.Show("Hoja de propuesta cargada de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.hojaPropuestaCargada = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Se deben de llenar todos los campos.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnConsultarHojaPropuesta_Click(object sender, EventArgs e)
        {
            if (dtFlujo.Rows.Count > 0)
            {
                DataRow dr = dtFlujo.Rows[0];
                this.MostrarExcel((byte[])dr["HojaPropuesta"]);
            }
        }

        private void btnExcelObservaciones_Click(object sender, EventArgs e)
        {
            ofd.Filter = "Archivo Excel (xls) | *.xlsx";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (ofd.FileName != "")
                {
                    lblArchivoObservaciones.Text = ofd.FileName;
                }
            }
        }
        private void btnGuardarHojaObservaciones_Click(object sender, EventArgs e)
        {
            if (txtReferencia.Text.Trim() != String.Empty && ofd.FileName != "")
            {
                Byte[] file = System.IO.File.ReadAllBytes(ofd.FileName);
                FlujoOP.ActualizaFlujoOP(this.id, file, 4, txtObservacionesPropuesta.Text.Trim());
                MessageBox.Show("Hoja de observaciones cargada de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.hojaObservacionesCargada = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Se deben de llenar todos los campos.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnConsultarHojaObservaciones_Click(object sender, EventArgs e)
        {
            if (dtFlujo.Rows.Count > 0)
            {
                DataRow dr = dtFlujo.Rows[0];
                this.MostrarExcel((byte[])dr["HojaProgramaObservaciones"]);
            }
        }

        private void btnExcelPrograma_Click(object sender, EventArgs e)
        {
            ofd.Filter = "Archivo Excel (xlsx) | *.xlsx";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (ofd.FileName != "")
                {
                    lblArchivoObservaciones.Text = ofd.FileName;
                    if (this.ValidaFormatoPrograma(ofd.FileName))
                    {
                        if (this.dsPrograma.Tables.Count > 0)
                        {
                            this.GeneraDatosPrograma();
                            string errores = this.ValidaTallasPrograma(this.ListaOrdenProduccion);
                            if (errores != "")
                            {
                                MessageBox.Show("El archivo contiene errores en las siguientes tallas: " + "\n" + errores + "\n" + "Favor de validar.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.ListaOrdenProduccion = new List<OrdenProduccionMasiva> { };
                                this.dgvResumenOrdenes.DataSource = this.ListaOrdenProduccion;
                                return;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("El archivo está vacío o tiene el formato correcto de la plantilla, favor de validar con el administrador del sistema.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }

        private void btnConsultarHojaReporte_Click(object sender, EventArgs e)
        {
            if (this.dsFlujoDetalle.Tables.Count > 0)
            {
                DataRow dr = this.dsFlujoDetalle.Tables[0].Rows[0];
                this.MostrarExcel((byte[])dr["HojaReporteOP"], ".xls");
            }
        }

        private void dgvResumenOrdenes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return;
            if (this.dgvResumenOrdenes.Columns[e.ColumnIndex].Name == "Configurar")
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
        private void dgvResumenOrdenes_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            this.ListaOrdenProduccion[e.RowIndex].objConfiguracion.observacionesOP = dgvResumenOrdenes[e.ColumnIndex, e.RowIndex].Value == null ? "" : dgvResumenOrdenes[e.ColumnIndex, e.RowIndex].Value.ToString();
            dgvResumenOrdenes.Refresh();
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
        private void btnPathCodigoBarra_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lblPathCodigoBarraValor.Text = folderBrowserDialog.SelectedPath;
                this.pathCB = folderBrowserDialog.SelectedPath;
                if (txtNumReferencia.Text != "")
                    btnGenerar.Enabled = true;
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

            if (this.ListaOrdenProduccion.Where(x => x.TieneConfiguracion == true).Count() == 0)
            {
                MessageBox.Show("Debe de existir al menos 1 Modelo configurado para poder continuar con el proceso.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Cursor.Current = Cursors.Default;
                return;
            }
            else
            {
                //COMENZAMOS A PROCESAR TODOS LOS MODELOS
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
                    // OBTENEMOS EL TIPO DE PRODUCTO LINEA (ALMACEN 1), ESPECIAL (ALMACEN32)
                    int _almacen = 0;
                    _almacen = OrdProduccion.GetLineaArticulo(_orden.Modelo) == "ESPE" ? 32 : 1;
                    //GENERAMOS LA OP
                    bool resultado = OrdProduccion.GeneraOrdenProduccionDeLinea(
                                                                            tallasSeleccionadas,
                                                                            referenciaInicial.ToString(),
                                                                            dtFechaVencimiento.Value,
                                                                            _orden.objConfiguracion.observacionesOP,
                                                                            "",
                                                                            _almacen
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
                                                    String.Format("{0} OP {1}", _orden.objConfiguracion.observacionesOM, referenciaInicial.ToString()),
                                                    _almacen,
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
                        FlujoOP.GuardaFlujoOPDetale(this.id, _orden.Modelo, referenciaInicial, CVE_DOC, Globales.UsuarioActual.UsuarioUsuario);
                    }
                    _orden.OrdenProduccion = referenciaInicial;
                    referenciaInicial++;
                }



                Cursor.Current = Cursors.Default;
                MessageBox.Show("Se han procesado de forma correcta las Ordenes de Producción", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.programaProduccionCreado = true;
                // GUARDAMOS EL PROGRAMA CARGADO
                Byte[] file = System.IO.File.ReadAllBytes(ofd.FileName);
                FlujoOP.ActualizaFlujoOP(this.id, file, 5);
                //UNA VEZ QUE TERMINAMOS GENERAMOS EL EXCEL DE SALIDA
                string archivoTemporal = System.IO.Path.GetTempFileName().Replace(".tmp", ".xls");
                OrdenProduccionMasiva.GeneraArchivoExcel(archivoTemporal, this.ListaOrdenProduccion);
                // GUARDAMOS EL REPORTE DE OP
                file = System.IO.File.ReadAllBytes(archivoTemporal);
                FlujoOP.ActualizaFlujoOP(this.id, file, 6);
                FuncionalidadesFormularios.MostrarExcel(archivoTemporal);
                this.Close();
            }
        }
        private void txtObservaciones_Enter(object sender, EventArgs e)
        {
            txtObservaciones.SelectAllOnFocus = false;
            txtObservaciones.SelectionStart = 0;
            txtObservaciones.SelectionLength = 0;
            txtObservaciones.Refresh();
        }
        private void txtObservaciones_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtObservaciones.Text.Trim().Length > 255)
            {
                e.Handled = true;
            }
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
        private void txtAlmacen_Leave(object sender, EventArgs e)
        {
            if (pathCB != "" && txtNumReferencia.Text != "")
            {
                btnGenerar.Enabled = true;
            }
        }
        private void dgvFlujoOPDetalle_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (this.dgvFlujoOPDetalle.Columns[e.ColumnIndex].Name == "ProgramaFechaEntrega")
            {
                string DateValue;
                DateTime DateFormated;
                DateValue = e.FormattedValue.ToString();
                if (e.FormattedValue != "")
                {
                    if (!DateTime.TryParseExact(DateValue, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateFormated))
                    {
                        MessageBox.Show("El valor debe de tener el siguiente formato: dd/MM/yyyy", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Cancel = true;
                    }
                }
            }
        }

        /*
        private void dgvFlujoOPDetalle_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvFlujoOPDetalle.Columns[e.ColumnIndex].Name.ToUpper() == "PROGRAMAFECHAENTREGA")
            {
                //Creamos el control por código
                dateTimePickerFechaEntrega = new DateTimePicker();

                //Agregamos el control de fecha dentro del DataGridView 
                this.dgvFlujoOPDetalle.Controls.Add(dateTimePickerFechaEntrega);

                // Hacemos que el control sea invisible (para que no moleste visualmente)
                dateTimePickerFechaEntrega.Visible = false;

                // Establecemos el formato (depende de tu localización en tu PC)
                dateTimePickerFechaEntrega.Format = DateTimePickerFormat.Short;  //Ej: 24/08/2016

                // Agregamos el evento para cuando seleccionemos una fecha
                dateTimePickerFechaEntrega.TextChanged += new EventHandler(dateTimePicker1_OnTextChange);

                // Lo hacemos visible
                dateTimePickerFechaEntrega.Visible = true;

                // Creamos un rectángulo que representa el área visible de la celda
                Rectangle rectangle1 = this.dgvFlujoOPDetalle.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);

                //Establecemos el tamaño del control DateTimePicker (que sería el tamaño total de la celda)
                dateTimePickerFechaEntrega.Size = new Size(rectangle1.Width, rectangle1.Height);

                // Establecemos la ubicación del control
                dateTimePickerFechaEntrega.Location = new Point(rectangle1.X, rectangle1.Y);

                // Generamos el evento de cierre del control fecha
                //dateTimePickerFechaEntrega.CloseUp += new EventHandler(dateTimePicker1_CloseUp);
            }
        }
         * */
        /*
        private void dateTimePicker1_OnTextChange(object sender, EventArgs e)
        {
            //Asignamos a la celda el valor de la feha seleccionada
            this.dgvFlujoOPDetalle.CurrentCell.Value = dateTimePickerFechaEntrega.Text.ToString();
        }
         * */
        void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            //Volvemos a colocar en invisible el control
            dateTimePickerFechaEntrega.Visible = false;
        }
        private void btnGuardarFechas_Click(object sender, EventArgs e)
        {
            int totalFechasAsignadas = 0;
            string _xml = "<ordenes>";
            foreach (DataGridViewRow dr in this.dgvFlujoOPDetalle.Rows)
            {
                if (dr.Cells["ProgramaFechaEntrega"].Value.ToString() != "")
                {
                    _xml += "<orden>";
                    _xml += String.Format("<ordenProduccion>{0}</ordenProduccion>", dr.Cells["ProgramaOrdenProduccion"].Value.ToString());
                    _xml += String.Format("<ordenMaquila>{0}</ordenMaquila>", dr.Cells["ProgramaOrdenMaquila"].Value.ToString());
                    _xml += String.Format("<fechaEntrega>{0}</fechaEntrega>", DateTime.Parse(dr.Cells["ProgramaFechaEntrega"].Value.ToString()).ToString("yyyy/MM/dd"));
                    _xml += "</orden>";
                    totalFechasAsignadas++;
                }
            }
            _xml += "</ordenes>";
            FlujoOP.GuardaFlujoOPFechas(this.id, _xml);
            MessageBox.Show("Fechas de entrega guardadas de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // VALIDAMOS SI YA SE SETEARON EL 100% DE LAS FECHAS
            if (this.dgvFlujoOPDetalle.Rows.Count == totalFechasAsignadas)
                fechasAsignadas = true;
        }
        private void chkFaltantes_CheckedChanged(object sender, EventArgs e)
        {
            this.btnExcelCuotas.Enabled = !chkFaltantes.Checked;
        }
        #endregion
        #region METODOS
        private DataTable CargaOP(int id)
        {
            return FlujoOP.ConsultaFlujoOP(id);
        }
        private void HabilitaComponentes()
        {
            // Hoja de cuotas
            this.txtReferencia.Enabled = !this.hojaCuotasCargada && !soloConsulta;
            this.btnConsultarHojaCuotas.Enabled = (this.hojaCuotasCargada || soloConsulta) && !bloqueaConsultaCuotas;
            this.btnGuardarHojaCuotas.Enabled = !(this.hojaCuotasCargada || this.chkFaltantes.Checked) && !soloConsulta;
            this.btnExcelCuotas.Enabled = !this.hojaCuotasCargada && !soloConsulta;
            this.chkFaltantes.Enabled = !this.hojaCuotasCargada && !soloConsulta;
            // Hoja de existencias
            this.gbHojaExistencias.Enabled = this.hojaCuotasCargada || soloConsulta;
            this.btnConsultarHojaExistencias.Enabled = this.hojaExistenciasCargada || (soloConsulta && this.hojaExistenciasCargada);
            this.btnGuardarHojaExistencias.Enabled = !this.hojaExistenciasCargada && !soloConsulta;
            this.btnExcelExistencias.Enabled = !this.hojaExistenciasCargada && !soloConsulta;

            // Hoja de propuesta
            this.gbHojaPropuesta.Enabled = this.hojaExistenciasCargada || soloConsulta; ;
            this.btnConsultarHojaPropuesta.Enabled = this.hojaPropuestaCargada || (soloConsulta && this.hojaPropuestaCargada);
            this.btnGuardarHojaPropuesta.Enabled = !this.hojaPropuestaCargada && !soloConsulta;
            this.btnExcelPropuesta.Enabled = !this.hojaPropuestaCargada && !soloConsulta;

            // Hoja de observaciones
            this.txtObservacionesPropuesta.Enabled = !this.hojaObservacionesCargada && !soloConsulta && (!this.cargaPrograma && !this.cargaFecha);
            this.gbHojaObservaciones.Enabled = this.hojaPropuestaCargada || soloConsulta; ;
            this.btnConsultarHojaObservaciones.Enabled = this.hojaObservacionesCargada || (soloConsulta && this.hojaObservacionesCargada);
            this.btnGuardarHojaObservaciones.Enabled = !this.hojaObservacionesCargada && !soloConsulta && (!this.cargaPrograma && !this.cargaFecha);
            this.btnExcelObservaciones.Enabled = !this.hojaObservacionesCargada && !soloConsulta && (!this.cargaPrograma && !this.cargaFecha);

            // Reporte de OP
            this.btnConsultarHojaReporte2.Visible = this.programaProduccionCreado;
            /*
            if (!(this.hojaCuotasCargada && this.hojaExistenciasCargada && this.hojaPropuestaCargada) && !this.cargaPrograma)
            {
                this.tabControl.TabPages.Remove(this.tabPagePrograma);
                if (!this.cargaFecha)
                    this.tabControl.TabPages.Remove(this.tabPageFechaEntrega);
            }*/
            if (!this.cargaPrograma)
                this.tabControl.TabPages.Remove(this.tabPagePrograma);
            if (!this.cargaFecha)
                this.tabControl.TabPages.Remove(this.tabPageFechaEntrega);
        }
        private void CaragComponentes(DataTable dtFlujo)
        {
            if (dtFlujo.Rows.Count > 0)
            {
                DataRow dr = dtFlujo.Rows[0];
                // Hoja de cuota
                if (dr["HojaCuotas"].ToString() != "" || (bool)dr["SoloFaltante"])
                {
                    this.txtReferencia.Text = dr["Referencia"].ToString();
                    this.lblArchivoCuotas.Text = String.Format("Archivo cargado - {0}", DateTime.Parse(dr["FechaCuotas"].ToString()).ToString("dd/MM/yyyy HH:mm:ss"));
                    this.hojaCuotasCargada = true;
                    this.chkFaltantes.Checked = (bool)dr["SoloFaltante"];
                }
                // Hoja de existencias
                if (dr["HojaExistencias"].ToString() != "")
                {
                    this.lblArchivoExistencias.Text = String.Format("Archivo cargado - {0}", DateTime.Parse(dr["FechaExistencias"].ToString()).ToString("dd/MM/yyyy HH:mm:ss"));
                    this.hojaExistenciasCargada = true;
                }
                // Hoja de propuesta
                if (dr["HojaPropuesta"].ToString() != "")
                {
                    this.lblArchivoPropuesta.Text = String.Format("Archivo cargado - {0}", DateTime.Parse(dr["FechaPropuesta"].ToString()).ToString("dd/MM/yyyy HH:mm:ss"));
                    this.hojaPropuestaCargada = true;
                }
                if (dr["HojaProgramaObservaciones"].ToString() != "")
                {
                    txtObservacionesPropuesta.Text = dr["Observaciones"].ToString();
                    this.lblArchivoObservaciones.Text = String.Format("Archivo cargado - {0}", DateTime.Parse(dr["FechaProgramaObservaciones"].ToString()).ToString("dd/MM/yyyy HH:mm:ss"));
                    this.hojaObservacionesCargada = true;
                }
            }
        }
        private void MostrarExcel(Byte[] File, String extension = ".xlsx")
        {
            string archivoTemporal = System.IO.Path.GetTempFileName().Replace(".tmp", extension);
            using (var fs = new FileStream(archivoTemporal, FileMode.Create, FileAccess.Write))
            {
                fs.Write(File, 0, File.Length);
            }
            FuncionalidadesFormularios.MostrarExcel(archivoTemporal);
        }
        private Boolean ValidaFormatoPrograma(String path)
        {
            bool validacion = false;
            OleDbCommand oleExcelCommand = default(OleDbCommand);
            OleDbDataReader oleExcelReader = default(OleDbDataReader);
            OleDbConnection oleExcelConnection = default(OleDbConnection);
            String sConnection = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=\"Excel 8.0;HDR=yes;\"", path);
            this.dsPrograma = new DataSet();

            try
            {
                Excel.Application oExcel = new Excel.Application();
                Excel.Workbook WB = oExcel.Workbooks.Open(path);
                int totalSheets = oExcel.Sheets.Count;
                try
                {
                    foreach (Excel.Worksheet xlworksheet in WB.Worksheets)
                    {
                        //Excel.Worksheet wks = (Excel.Worksheet)WB.Worksheets[1];
                        Excel.Worksheet wks = xlworksheet;
                        Excel.Range xlRange = wks.UsedRange;
                        DataTable dtSheet = new DataTable();

                        DataRow row = null;
                        for (int i = 1; i <= xlRange.Rows.Count; i++)
                        {
                            if (i != 1)
                                row = dtSheet.NewRow();
                            for (int j = 1; j <= xlRange.Columns.Count; j++)
                            {
                                if (i == 1)
                                {
                                    try
                                    {
                                        dtSheet.Columns.Add(((Excel.Range)xlRange.Cells[1, j]).Value.ToString(), typeof(string));
                                    }

                                    catch { }
                                }
                                else
                                    row[j - 1] = ((Excel.Range)(xlRange.Cells[i, j])).Value;
                            }
                            if (row != null)
                                dtSheet.Rows.Add(row);
                        }
                        dsPrograma.Tables.Add(dtSheet);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    WB.Close();
                    oExcel.Quit();
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            try
            {
                int totalColumnasObligatorias = 3;
                int totalColumnas = 0;
                foreach (DataTable dt in dsPrograma.Tables)
                {
                    totalColumnas = 0;
                    foreach (DataColumn column in dt.Columns)
                    {
                        // VALIDAMOS QUE EXISTAN LAS COLUMNAS (LINEA, MODELO Y TOTAL)
                        {
                            if (column.ColumnName.ToUpper() == "LINEA") { totalColumnas++; }
                            if (column.ColumnName.ToUpper() == "MODELO") { totalColumnas++; }
                            if (column.ColumnName.ToUpper() == "TOTAL") { totalColumnas++; }
                        }
                    }
                    if (totalColumnas != totalColumnasObligatorias)
                    {
                        return false;
                    }
                }
                return dsPrograma.Tables.Count > 0;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return validacion;

        }
        private void GeneraDatosPrograma()
        {
            if (this.dsPrograma.Tables.Count > 0)
            {
                this.ListaOrdenProduccion = new List<OrdenProduccionMasiva> { };
                DataTable dtDatosOrdenProduccion = this.GeneraTablaPorModelo();
                foreach (DataRow dr in dtDatosOrdenProduccion.Rows)
                {
                    //VERIFICAMOS QUE EL MODELO EXISTA EN LA LISTA 
                    if (this.ListaOrdenProduccion.Where(x => x.Modelo == dr["MODELO"].ToString()).FirstOrDefault() == null)
                    {
                        //AGREGAMOS EL MODELO A LA OP
                        OrdenProduccionMasiva _ordenProduccion = new OrdenProduccionMasiva();
                        _ordenProduccion.Modelo = dr["MODELO"].ToString();
                        _ordenProduccion.ListaTallas.Add(new OrdenProduccionMasiva.Talla { talla = dr["TALLA"].ToString().Replace(" ", "").Trim(), cantidad = int.Parse(dr["CANTIDAD"].ToString()) });
                        _ordenProduccion.objConfiguracion.observacionesOP = dr["OBSERVACIONES"].ToString();
                        this.ListaOrdenProduccion.Add(_ordenProduccion);
                    }
                    else
                    {
                        //SI EL MODELO YA EXISTE VERIFICAMOS SI LA TALLA EXISTE
                        if (this.ListaOrdenProduccion.Where(x => x.Modelo == dr["MODELO"].ToString()).FirstOrDefault().ListaTallas.Where(y => y.talla == dr["TALLA"].ToString()).FirstOrDefault() != null)
                        {
                            //SI LA TALLA YA EXISTE, AGREGAMOS SOLO CANTIDADES
                            this.ListaOrdenProduccion.Where(x => x.Modelo == dr["MODELO"].ToString()).FirstOrDefault().ListaTallas.Where(y => y.talla == dr["TALLA"].ToString()).FirstOrDefault().cantidad += int.Parse(dr["CANTIDAD"].ToString());
                        }
                        else
                        {
                            //AGREGAMOS LA TALLA
                            this.ListaOrdenProduccion.Where(x => x.Modelo == dr["MODELO"].ToString()).First().ListaTallas.Add(new OrdenProduccionMasiva.Talla { talla = dr["TALLA"].ToString().Replace(" ", "").Trim(), cantidad = int.Parse(dr["CANTIDAD"].ToString()) });
                        }

                    }
                }
                this.ListaOrdenProduccion = this.ListaOrdenProduccion.OrderBy(x => x.Modelo).ToList();
                this.dgvResumenOrdenes.DataSource = this.ListaOrdenProduccion;
            }
        }
        private DataTable GeneraTablaPorModelo()
        {
            List<String> ListaTallas = new List<String> { };
            // OBTENEMOS EL TOTAL DE TALLAS DENTRO DEL FORMATO
            foreach (DataTable dt in this.dsPrograma.Tables)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ColumnName.ToUpper() != "LINEA" && column.ColumnName.ToUpper() != "MODELO" && column.ColumnName.ToUpper() != "TOTAL")
                    {
                        ListaTallas.Add(column.ColumnName);
                    }
                }
            }
            // RECORREMOS CADA REGISTRO Y GENERAMOS LA TALLA APLICABLE
            DataTable dtTablaPorModelo = new DataTable();
            dtTablaPorModelo.Columns.Add("MODELO");
            dtTablaPorModelo.Columns.Add("TALLA");
            dtTablaPorModelo.Columns.Add("CANTIDAD");
            dtTablaPorModelo.Columns.Add("OBSERVACIONES");
            foreach (DataTable dt in this.dsPrograma.Tables)
            {
                foreach (DataRow row in dt.Rows)
                {
                    foreach (string columnName in ListaTallas.Distinct().ToList())
                    {
                        try
                        {
                            if (row[columnName].ToString().Replace(" ", "").Trim() != "")
                            {
                                DataRow dr = dtTablaPorModelo.NewRow();
                                dr["MODELO"] = row["MODELO"].ToString();
                                dr["TALLA"] = columnName;
                                dr["CANTIDAD"] = row[columnName].ToString();
                                dr["OBSERVACIONES"] = "";
                                dtTablaPorModelo.Rows.Add(dr);
                            }
                        }
                        catch { }
                    }
                }
            }
            return dtTablaPorModelo;
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
        private DataSet CargaOPDetalle(int id)
        {
            return FlujoOP.ConsultaFlujoOPDetalle(id);
        }
        private void CargaComponentesFechaEntrega(DataSet dsFlujoDetalle)
        {
            if (dsFlujoDetalle.Tables.Count > 0)
            {
                DataTable dtReporte = dsFlujoDetalle.Tables[0];
                DataTable dtDetalle = dsFlujoDetalle.Tables[1];
                this.dgvFlujoOPDetalle.DataSource = dtDetalle;
            }
        }
        private String ValidaTallasPrograma(List<OrdenProduccionMasiva> ListaOrdenProduccion)
        {
            string validacion = "";
            string _xml = "";
            _xml += "<modelos>";
            foreach (OrdenProduccionMasiva orden in ListaOrdenProduccion)
            {
                foreach (OrdenProduccionMasiva.Talla talla in orden.ListaTallas)
                {
                    _xml += "<modelo>" + orden.Modelo + talla.talla + "</modelo>";
                }
            }
            _xml += "</modelos>";
            DataTable result = FlujoOP.ValidaModelos(_xml);
            if (result.Rows.Count > 0)
            {
                string[] errores = result.Rows.OfType<DataRow>().Select(k => k[0].ToString()).ToArray();
                validacion = String.Join(",", errores);
            }
            return validacion;
        }
        #endregion


    }
}
