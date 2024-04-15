using SIP.Utiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using ulp_bl;
using ulp_bl.Utiles;

namespace SIP
{
    public partial class frmControlPedidos : Form
    {
        #region ATRIBUTOS Y CONSTRUCTORES

        DataTable dtPedidosPorArea;
        Exception ex;

        String AreaConsulta = "";
        String AreaSeleccionada = "";
        Boolean reload = false;

        /*
        System.Timers.Timer timer = new System.Timers.Timer(15000);
        public event EventHandler<EventArgs> TimerExecuted;
        System.Windows.Forms.Timer formsTimer;
        public System.Threading.ManualResetEvent TimerNotice;
         * */

        public frmControlPedidos()
        {
            InitializeComponent();
            dtPedidosPorArea = new DataTable();
            dgvPedidos.AutoGenerateColumns = false;
            ex = null;
            //this.SetTimmer();
            if (Globales.UsuarioActual.UsuarioArea == "DG" || Globales.UsuarioActual.UsuarioArea == "TD")
            {
                btnReporte.Visible = true;
            }

        }
        #endregion
        #region EVENTOS
        private void frmControlPedidos_Load(object sender, EventArgs e)
        {
            DataTable dtAreas = new DataTable();

            dtAreas = getAreas();

            //LOAD TREE VIEW AREAS
            //treeViewProcesos.Nodes.Add(BindTreeNode("Órdenes", dtAreas, "DescripcionCompletaArea"));            
            treeViewProcesos.Nodes.Add(BindTreeNode("Pedidos", dtAreas, "DescripcionCompletaArea"));
            treeViewProcesos.ExpandAll();

            statusStripLblArea.Text = "Área: " + Globales.UsuarioActual.UsuarioArea;
            statusStripLblUsuario.Text = "Usuario: " + Globales.UsuarioActual.UsuarioUsuario;
            statusStripLblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");

        }
        private void treeViewProcesos_AfterSelect(object sender, TreeViewEventArgs e)
        {
            List<String> AreasExcepcion = new List<string> { "TD", "CN" };
            this.AreaSeleccionada = e.Node.Text.Trim().Split('-')[0];
            this.AreaConsulta = this.AreaSeleccionada;
            if (e.Node.Parent == null)
                return;
            dgvHistorico.Columns.Clear();
            dgvHistorico.Rows.Clear();
            dtPedidosPorArea = new DataTable();
            // CARGAMOS USUARIOS ESPECIALES ACTIVOS Y QUE NO SEA EL USUARIO JAT
            DataTable dtUsuariosEspeciales = new DataTable();
            dtUsuariosEspeciales = ControlPedidos.getUsuariosEspeciales();
            //  VALIDAMOS QUE EL USUARIO PUEDA GESTIONAR EL AREA SELECCIONADA            
            if (this.AreaSeleccionada != Globales.UsuarioActual.UsuarioArea && !AreasExcepcion.Contains(Globales.UsuarioActual.UsuarioArea))
            {
                if (dtUsuariosEspeciales.Select("Activo=1 AND Area='" + this.AreaSeleccionada + "'").FirstOrDefault() != null)
                {
                    if (Globales.UsuarioActual.UsuarioUsuario == "jat")
                    {
                        MessageBox.Show("Existen usuarios con permisos especiales que están activos, se deberán desactivar para poder gestionar el área seleccionada.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (dtUsuariosEspeciales.Select("Usuario='" + Globales.UsuarioActual.UsuarioUsuario + "' AND Activo=1").FirstOrDefault() == null)
                    {
                        MessageBox.Show("Sin permisos para gestionar el área seleccionada.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Sin permisos para gestionar el área seleccionada: " + this.AreaSeleccionada, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // VALIDAMOS USUARIOS ACTIVOS
            if (dtUsuariosEspeciales.Select("Activo=1 AND Area='" + this.AreaSeleccionada + "'").FirstOrDefault() != null)
            {
                if (Globales.UsuarioActual.UsuarioUsuario == "jat")
                {
                    MessageBox.Show("Existen usuarios con permisos especiales que están activos, se deberán desactivar para poder gestionar el área seleccionada.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }


            switch (e.Node.Parent.Text)
            {
                case "Pedidos":
                    dtPedidosPorArea = getPedidosPorArea(this.AreaSeleccionada, Globales.UsuarioActual.UsuarioUsuario);
                    break;
                default:
                    break;
            }

            dgvPedidos.DataSource = dtPedidosPorArea;
            MarcaGridAlertas();
            MarcaGridHabilitados();
            if (this.AreaSeleccionada == "EV")
            {
                dgvPedidos.Columns["Seleccion"].Visible = true;
                dgvPedidos.Columns["Seleccion"].ReadOnly = false;
            }
            else
            {
                dgvPedidos.Columns["Seleccion"].Visible = false;
            }
            getProcesosGrid();
            getClientesGrid();
            getTiposPedidoGrid();
        }
        private void dgvPedidos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvHistorico.Columns.Clear();
            dgvHistorico.Rows.Clear();
            //OBTENEMOS EL HISTORICO DEL PEDIDO
            String Pedido = dgvPedidos.CurrentRow.Cells["Referencia"].Value.ToString();
            DataTable dtHistoricoPedido = new DataTable();
            dtHistoricoPedido = getHistoricoPedido(Pedido);
            if (dtHistoricoPedido.Rows.Count > 0)
            {
                DataGridViewRow rowFechas = new DataGridViewRow();
                DataGridViewRow rowDias = new DataGridViewRow();
                int totalColumnas = dtHistoricoPedido.Rows.Count * 2;
                int posicion = 1;

                //CREAMOS ENCABEZADOS
                dgvHistorico.Columns.Add("Col0", "Descripcion");
                dgvHistorico.Rows.Add();
                dgvHistorico.Rows.Add();

                dgvHistorico.Rows[0].Cells[0].Value = "Calendario";
                dgvHistorico.Rows[1].Cells[0].Value = "Días Transcurridos";
                //RECORREMOS CADA FILA Y GENERAMOS LAS COLUMANAS AL GRID
                foreach (DataRow _dr in dtHistoricoPedido.Rows)
                {
                    dgvHistorico.Columns.Add("Col" + posicion.ToString(), "INI - " + _dr["Area"].ToString());
                    dgvHistorico.Rows[0].Cells[posicion].Value = DateTime.Parse(_dr["FechaInicio"].ToString()).ToString("dd/MM/yyyy HH:mm");
                    dgvHistorico.Rows[1].Cells[posicion].Value = "";
                    posicion += 1;
                    dgvHistorico.Columns.Add("Col" + posicion.ToString(), "FIN - " + _dr["Area"].ToString());
                    dgvHistorico.Rows[0].Cells[posicion].Value = DateTime.Parse(_dr["FechaFin"].ToString()) == DateTime.Parse("01/01/1900") ? "En Curso" : DateTime.Parse(_dr["FechaFin"].ToString()).ToString("dd/MM/yyyy HH:mm");
                    TimeSpan time = TimeSpan.FromSeconds(double.Parse(_dr["DiasTranscurridosSegundos"].ToString()));
                    if (time.Ticks > 0)
                    {
                        string timeFormat = string.Format("{0:D2}days {1:D2}h:{2:D2}m",
                                        time.Days,
                                        time.Hours,
                                        time.Minutes);
                        dgvHistorico.Rows[1].Cells[posicion].Value = time.Ticks == 0 ? "En Curso" : timeFormat;
                    }

                    else
                        dgvHistorico.Rows[1].Cells[posicion].Value = "0";

                    //dgvHistorico.Rows[1].Cells[posicion].Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

                    posicion += 1;
                }
                TimeSpan totalTime = TimeSpan.FromSeconds(double.Parse(dtHistoricoPedido.Rows[0]["TiempoTotalTranscurridoSegundos"].ToString()));
                string totalTimeFormat = string.Format("{0:D2}days {1:D2}h:{2:D2}m",
                                       totalTime.Days,
                                       totalTime.Hours,
                                       totalTime.Minutes);
                dgvHistorico.Columns.Add("Col" + posicion.ToString(), "Total");
                dgvHistorico.Rows[0].Cells[posicion].Value = "";
                dgvHistorico.Rows[1].Cells[posicion].Value = totalTimeFormat;

            }
        }
        private void dgvPedidos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            lstObservaciones.Items.Clear();
            if (dgvPedidos.Columns[e.ColumnIndex].Name == "OBSERVACIONES")
            {
                if (dgvPedidos[e.ColumnIndex, e.RowIndex].Value.ToString() != "")
                {
                    DataTable dt = new DataTable();
                    dt = getObservacionesPedido(dgvPedidos[1, e.RowIndex].Value.ToString());
                    if (dt != null)
                        if (dt.Rows.Count > 0)
                        {
                            //MOSTRAMOS PANEL DE OBSERVACIONES
                            foreach (DataRow _dr in dt.Rows)
                            {
                                lstObservaciones.Items.Add(_dr["FechaFin"].ToString() + " - " + _dr["DescripcionArea"].ToString() + "( " + _dr["Usuario"].ToString() + "): " + _dr["Observaciones"].ToString());
                            }
                            pnlObservaciones.Focus();
                            pnlObservaciones.Visible = true;

                        }
                }
            }
        }
        private void dgvPedidos_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                try
                {
                    dgvPedidos.EndEdit();
                    ContextMenuStrip menu = new ContextMenuStrip();
                    int position_click = dgvPedidos.HitTest(e.X, e.Y).RowIndex;
                    if (position_click >= 0)
                    {
                        String _area = String.Empty;
                        int _ordenAgrupador;
                        int _tipoProceso;
                        //SELECCIONAMOS EL TIPO DE AREA EN LA QUE NOS ENCONTRAMOS Y GENERAMOS LOS MENUS CORRESPONDIENTES 
                        //CONSIDERACION 1: AREA
                        //CONSIDERACION 2: ORDEN AGRUPADOR
                        _area = treeViewProcesos.SelectedNode.Text.Trim().Split('-')[0];
                        _ordenAgrupador = (int)dgvPedidos.CurrentRow.Cells["OrdenAgrupador"].Value;
                        _tipoProceso = (int)dgvPedidos.CurrentRow.Cells["ClaveTipoProceso"].Value;

                        if (Globales.UsuarioActual.UsuarioArea == _area || Globales.UsuarioActual.UsuarioArea == "TD")
                        {
                            DataTable dtMenus = getListaMenus(_area, _ordenAgrupador, _tipoProceso);
                            foreach (DataRow _dr in dtMenus.Rows)
                            {
                                menu.Items.Add(_dr["NombreMenu"].ToString()).Name = _dr["ClaveMenu"].ToString();
                            }
                        }
                        else
                        {
                            DataTable dtUsuariosEspeciales = new DataTable();
                            dtUsuariosEspeciales = ControlPedidos.getUsuariosEspeciales();
                            if (dtUsuariosEspeciales.Select("Activo=1 AND Area='" + _area + "'").FirstOrDefault() != null)
                            {
                                if (dtUsuariosEspeciales.Select("Usuario='" + Globales.UsuarioActual.UsuarioUsuario + "' AND Activo=1").FirstOrDefault() != null)
                                {
                                    DataTable dtMenus = getListaMenus(_area, _ordenAgrupador, _tipoProceso);
                                    foreach (DataRow _dr in dtMenus.Rows)
                                    {
                                        menu.Items.Add(_dr["NombreMenu"].ToString()).Name = _dr["ClaveMenu"].ToString();
                                    }
                                }
                            }
                        }
                        // SI EL REGISTRO ESTÁ RECHAZADO AGREGAMOS LA OPCION DE CANCELACION
                        if (dgvPedidos.CurrentRow.Cells["Estatus"].Value.ToString().ToUpper() == "RECHAZADO")
                        {
                            if (Globales.UsuarioActual.UsuarioArea != "CN")
                            {
                                menu.Items.Add("Cancelar").Name = "CANCELAR";
                            }
                        }
                        // SI EL REGISTRO SE TRATA DE UN PEDIDO AGREGAMOS LA OPCION DE DOCUMENTACIÓN ELECTRONICA
                        if (dgvPedidos.CurrentRow.Cells["ClaveTipoProceso"].Value.ToString().ToUpper() != "1")
                        {
                            if (Globales.UsuarioActual.UsuarioArea != "CN")
                            {
                                menu.Items.Add("Expediente Digital").Name = "EXPEDIENTEDIGITAL";
                            }
                        }
                        // SI EL REGISTRO SE TRATA DE UNA SOLICITUD Y ES TD AGREGAMOS LA OPCION DE 'Eliminar Solicitud' 
                        if (dgvPedidos.CurrentRow.Cells["ClaveTipoProceso"].Value.ToString().ToUpper() == "1")
                        {
                            if (Globales.UsuarioActual.UsuarioArea == "TD")
                            {
                                menu.Items.Add("Eliminar Solicitud").Name = "ELIMINARSOLICITUD";
                            }
                        }
                    }
                    menu.Show(dgvPedidos, new Point(e.X, e.Y));
                    menu.ItemClicked += new ToolStripItemClickedEventHandler(menu_ItemClicked);
                }
                catch { }
            }
        }

        private void btnCerrarObservaciones_Click(object sender, EventArgs e)
        {
            lstObservaciones.DataSource = null;
            lstObservaciones.Update();
            pnlObservaciones.Visible = false;
            dgvPedidos.ClearSelection();
        }

        private void pnlObservaciones_Leave(object sender, EventArgs e)
        {
            pnlObservaciones.Visible = false;
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {

            if (dtpFechaDesde.Value > dtpFechaHasta.Value)
            {
                if (chkFechaDesde.Checked && chkFechaHasta.Checked)
                {
                    MessageBox.Show("La fecha de inicio no puede ser mayor a la fecha final", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            dgvHistorico.Columns.Clear();
            dgvHistorico.Rows.Clear();

            if (!(chkFechaDesde.Checked || chkFechaHasta.Checked || chkProceso.Checked || chkCliente.Checked || chkTipo.Checked))
            {
                dgvPedidos.DataSource = dtPedidosPorArea;
                dgvPedidos.Update();
                dgvPedidos.Refresh();
                MarcaGridAlertas();
                MarcaGridHabilitados();
            }
            else
            {
                DateTime _FechaDesde = chkFechaDesde.Checked ? dtpFechaDesde.Value.Date : DateTime.MinValue.Date;
                DateTime _FechaHasta = chkFechaHasta.Checked ? dtpFechaHasta.Value.Date : DateTime.Now;
                String _Proceso = chkProceso.Checked ? cmbProcesos.Text.ToString() : "";
                String _Cliente = chkCliente.Checked ? cmbClientes.Text.ToString() : "";
                String _Tipo = chkTipo.Checked ? cmbTipo.Text.ToString() : "";

                var query = from pedidos in dtPedidosPorArea.AsEnumerable()
                            where
                                pedidos.Field<DateTime>("FECHA") >= _FechaDesde &&
                                pedidos.Field<DateTime>("FECHA") <= _FechaHasta &&
                                pedidos.Field<String>("NombreCorto") == (_Proceso == "" ? pedidos.Field<String>("NombreCorto") : _Proceso) &&
                                pedidos.Field<String>("NOMBRE") == (_Cliente == "" ? pedidos.Field<String>("NOMBRE") : _Cliente) &&
                                pedidos.Field<String>("DescripcionProceso") == (_Tipo == "" ? pedidos.Field<String>("DescripcionProceso") : _Tipo)
                            select pedidos;

                dgvPedidos.DataSource = query.AsDataView();
                dgvPedidos.Update();
                dgvPedidos.Refresh();
                MarcaGridAlertas();
                MarcaGridHabilitados();
            }


        }

        void menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            String _ClaveArea, _Estatus, _Observaciones, _Usuario, _Resultado, _Cliente;
            _ClaveArea = _Estatus = _Observaciones = _Usuario = _Resultado = String.Empty;
            Boolean permisoEspecial = false;

            int _Pedido, _referenciaProceso, _tipoProceso;

            _Pedido = (int)dgvPedidos.CurrentRow.Cells["ID"].Value;
            _Usuario = Globales.UsuarioActual.UsuarioUsuario;
            _referenciaProceso = (int)dgvPedidos.CurrentRow.Cells["Referencia"].Value;
            _ClaveArea = treeViewProcesos.SelectedNode.Text.Trim().Split('-')[0];
            _Cliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString().Trim();
            _tipoProceso = (int)dgvPedidos.CurrentRow.Cells["ClaveTipoProceso"].Value;

            //Validamos si es usuario especial
            DataTable dtUsuariosEspeciales = new DataTable();
            dtUsuariosEspeciales = ControlPedidos.getUsuariosEspeciales();
            if (dtUsuariosEspeciales.Select("Activo=1 AND Area='" + _ClaveArea + "'").FirstOrDefault() != null)
            {
                if (dtUsuariosEspeciales.Select("Usuario='" + Globales.UsuarioActual.UsuarioUsuario + "' AND Activo=1").FirstOrDefault() != null)
                {
                    permisoEspecial = true;
                }
            }

            //VALIDAMOS SI EL USUARIO ACTUAL TIENE PERMISOS PARA PROCESAR LAS SOLICITUDES
            if (Globales.UsuarioActual.UsuarioArea == "TD" || Globales.UsuarioActual.UsuarioArea == _ClaveArea || permisoEspecial)
            {

                if (_tipoProceso != 3) // AQUELLOS QUE NO SEAN PEDIDOS DE LINEA
                {
                    switch (_ClaveArea)
                    {
                        case "EV":
                            GeneraProcesoEV(_Pedido, e.ClickedItem.Name, _referenciaProceso, _Cliente);
                            break;
                        case "GV":
                            GeneraProcesoGV(_Pedido, e.ClickedItem.Name, _referenciaProceso, _Cliente);
                            break;
                        case "CPR":
                            GeneraProcesoCPR(_Pedido, e.ClickedItem.Name, _referenciaProceso, _Cliente);
                            break;
                        case "GO":
                            GeneraProcesoGO(_Pedido, e.ClickedItem.Name, _referenciaProceso, _Cliente);
                            break;
                        case "DG":
                            GeneraProcesoDG(_Pedido, e.ClickedItem.Name, _referenciaProceso, _Cliente);
                            break;
                        case "CP":
                            GeneraProcesoCP(_Pedido, e.ClickedItem.Name, _referenciaProceso, _Cliente);
                            break;
                        case "FA":
                            GeneraProcesoFA(_Pedido, e.ClickedItem.Name, _referenciaProceso, _Cliente);
                            break;
                        case "CR":
                            GeneraProcesoCR(_Pedido, e.ClickedItem.Name, _referenciaProceso, _Cliente);
                            break;
                        case "SU":
                            GeneraprocesoSU(_Pedido, e.ClickedItem.Name, _referenciaProceso, _Cliente);
                            break;
                        case "CA":
                            GeneraProcesoCA(_Pedido, e.ClickedItem.Name, _referenciaProceso, _Cliente);
                            break;
                        case "PL":
                            GeneraProcesoPL(_Pedido, e.ClickedItem.Name, _referenciaProceso, _Cliente);
                            break;
                    }
                }
                else //PROCESOS APLICABLES PARA PEDIDOS DE LINEA
                {
                    switch (_ClaveArea)
                    {
                        case "CP":
                            GeneraProcesoCPLinea(_Pedido, e.ClickedItem.Name, _referenciaProceso, _Cliente);
                            break;
                        case "EV":
                            GeneraProcesoEVLinea(_Pedido, e.ClickedItem.Name, _referenciaProceso, _Cliente);
                            break;
                        case "GV":
                            GeneraProcesoGVLinea(_Pedido, e.ClickedItem.Name, _referenciaProceso, _Cliente);
                            break;
                        case "DG":
                            GeneraProcesoDGLinea(_Pedido, e.ClickedItem.Name, _referenciaProceso, _Cliente);
                            break;
                        case "FA":
                            GeneraProcesoFALinea(_Pedido, e.ClickedItem.Name, _referenciaProceso, _Cliente);
                            break;
                        case "CR":
                            GeneraProcesoCRLinea(_Pedido, e.ClickedItem.Name, _referenciaProceso, _Cliente);
                            break;
                        case "CPR":
                            GeneraProcesoCPRLinea(_Pedido, e.ClickedItem.Name, _referenciaProceso, _Cliente);
                            break;
                        case "SU":
                            GeneraprocesoSULinea(_Pedido, e.ClickedItem.Name, _referenciaProceso, _Cliente);
                            break;
                        case "EM":
                            GeneraProcesoEMLinea(_Pedido, e.ClickedItem.Name, _referenciaProceso, _Cliente);
                            break;
                        case "PL":
                            GeneraProcesoPLLinea(_Pedido, e.ClickedItem.Name, _referenciaProceso, _Cliente);
                            break;

                    }
                }
            }
            if (reload)
            {
                dtPedidosPorArea = getPedidosPorArea(treeViewProcesos.SelectedNode.Text.Trim().Split('-')[0], Globales.UsuarioActual.UsuarioUsuario);
                dgvPedidos.DataSource = dtPedidosPorArea;
                MarcaGridAlertas();
                MarcaGridHabilitados();
            }

        }
        private void frmControlPedidos_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*
            this.formsTimer.Stop();
            this.timer.Stop();
             */
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (this.AreaSeleccionada != "")
            {
                this.AreaConsulta = this.AreaSeleccionada;
            }
            else
            {
                this.AreaConsulta = Globales.UsuarioActual.UsuarioArea == "TD" ? "GV" : Globales.UsuarioActual.UsuarioArea;
            }
            this.dtPedidosPorArea = getPedidosPorArea(this.AreaConsulta, Globales.UsuarioActual.UsuarioUsuario);
            dgvPedidos.DataSource = dtPedidosPorArea;
            MarcaGridAlertas();
            MarcaGridHabilitados();
            if (this.AreaSeleccionada == "EV")
            {
                dgvPedidos.Columns["Seleccion"].Visible = true;
                dgvPedidos.Columns["Seleccion"].ReadOnly = false;
            }
            else
            {
                dgvPedidos.Columns["Seleccion"].Visible = false;
            }
            getProcesosGrid();
            getClientesGrid();
            getTiposPedidoGrid();
            this.treeViewProcesos.SelectedNode = this.treeViewProcesos.Nodes[0].Nodes[this.AreaConsulta];

        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            // OBTENEMOS EL REPORTE
            DataTable dtResult = ControlPedidos.getReportePrendasAutorizadas();
            if (dtResult != null)
            {
                if (dtResult.Rows.Count > 0)
                {
                    string archivoTemporal = System.IO.Path.GetTempFileName().Replace(".tmp", ".xls");
                    ControlPedidos.GeneraArchivoExcelPrendasAutorizadas(archivoTemporal, dtResult);
                    FuncionalidadesFormularios.MostrarExcel(archivoTemporal);
                }
                else
                {
                    MessageBox.Show("No se encontraron registros", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Error al procesar el reporte, ponerse en contacto con el Administrador del Sistema.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor.Current = Cursors.Default;
        }
        #endregion
        #region METODOS

        private DataTable getAreas()
        {
            DataTable dt = new DataTable();
            dt = ControlPedidos.getAreasSIP();
            return dt;
        }
        private DataTable getEstatus()
        {
            DataTable dt = new DataTable();
            dt = ControlPedidos.getEstatusSIP();
            return dt;
        }
        private void getProcesosGrid()
        {
            List<String> Procesos = new List<string> { };
            foreach (DataGridViewRow _dr in dgvPedidos.Rows)
            {
                if (!Procesos.Contains(_dr.Cells["NombreCorto"].Value.ToString()))
                {
                    Procesos.Add(_dr.Cells["NombreCorto"].Value.ToString());
                }
            }
            cmbProcesos.DataSource = Procesos.ToArray();
            cmbProcesos.Refresh();
        }
        private void getClientesGrid()
        {
            List<String> Clientes = new List<string> { };
            foreach (DataGridViewRow _dr in dgvPedidos.Rows)
            {
                if (!Clientes.Contains(_dr.Cells["NOMBRE"].Value.ToString()))
                {
                    Clientes.Add(_dr.Cells["NOMBRE"].Value.ToString());
                }
            }
            cmbClientes.DataSource = Clientes.ToArray();
            cmbClientes.Refresh();
        }
        private void getTiposPedidoGrid()
        {

            List<String> TiposPedido = new List<string> { };
            foreach (DataGridViewRow _dr in dgvPedidos.Rows)
            {
                if (!TiposPedido.Contains(_dr.Cells["TipoProceso"].Value.ToString()))
                {
                    TiposPedido.Add(_dr.Cells["TipoProceso"].Value.ToString());
                }
            }
            cmbTipo.DataSource = TiposPedido.ToArray();
            cmbTipo.Refresh();

        }

        private TreeNode BindTreeNode(String NodeName, DataTable dt, String rowName)
        {
            TreeNode rootNode = new TreeNode(NodeName);
            foreach (DataRow row in dt.Rows)
            {
                TreeNode NewNode = new TreeNode(row[rowName].ToString());
                NewNode.Name = row[rowName].ToString().Split('-')[0];
                rootNode.Nodes.Add(NewNode);
            }
            return rootNode;
        }
        private DataTable getPedidosPorArea(String _ClaveArea, String _Usuario)
        {
            DataTable dt = new DataTable();
            dt = ControlPedidos.getPedidosPorArea(_ClaveArea, _Usuario);
            return dt;
        }
        private DataTable getHistoricoPedido(String _Pedido)
        {
            DataTable dt = new DataTable();
            dt = ControlPedidos.getHistoricoPedido(_Pedido);
            return dt;
        }
        private DataTable getObservacionesPedido(String _Pedido)
        {
            DataTable dt = new DataTable();
            dt = ControlPedidos.getObservacionesPedido(_Pedido);
            return dt;
        }
        private DataTable getListaMenus(String _ClaveArea, int _OrdenAgrupador, int _tipoProceso)
        {
            DataTable dt = new DataTable();
            dt = ControlPedidos.getListaMenus(_ClaveArea, _OrdenAgrupador, _tipoProceso);
            return dt;
        }

        private String setLineaTiempoPedido(int _Pedido, String _ClaveArea, string _Estatus, int _OrdenAgrupador, string _Observaciones, string _usuario, int _cveTipoProceso, int _referenciaProceso, String _Cliente)
        {
            ex = null;
            ControlPedidos.setLineaTiempoPedido(_Pedido, _ClaveArea, _Estatus, _OrdenAgrupador, _Observaciones, _usuario, _cveTipoProceso, _referenciaProceso, _Cliente, ref ex);
            if (ex == null)
                return "";
            else
                return ex.Message;
        }

        private String setAutorizacionPedido(int _Pedido, String _ClaveArea, String _Usuario, Boolean _aplicaFirma = false)
        {
            ex = null;
            ControlPedidos.setAutorizacionPedido(_Pedido, _ClaveArea, _Usuario, ref ex, _aplicaFirma);
            if (ex == null)
                return "";
            else
                return ex.Message;
        }

        private String setCancelaAutorizacionPedido(int _Pedido, String _ClaveArea, String _Usuario, string _ClaveAreaInicial)
        {
            ex = null;
            ControlPedidos.setCancelaAutorizacionPedido(_Pedido, _ClaveArea, _Usuario, _ClaveAreaInicial, ref ex);
            if (ex == null)
                return "";
            else
                return ex.Message;
        }

        private String setAltaLineaTiempoPedido(int _Pedido, String _ClaveArea, string _Estatus, int _OrdenAgrupador, string _Observaciones, string _usuario, int _cveTipoProceso, int _referenciaProceso, String _Cliente)
        {
            Exception ex = null;
            ControlPedidos.setAltaLineaTiempoPedido(_Pedido, _ClaveArea, _Estatus, _OrdenAgrupador, _Observaciones, _usuario, _cveTipoProceso, _referenciaProceso, _Cliente, ref ex);
            if (ex == null)
                return "";
            else
                return ex.Message;
        }

        private void MarcaGridAlertas()
        {
            dgvPedidos.ClearSelection();
            foreach (DataGridViewRow dr in dgvPedidos.Rows)
            {
                if ((bool)dr.Cells["Marcar"].Value)
                    dr.DefaultCellStyle.ForeColor = Color.Red;
            }
        }

        private void MarcaGridHabilitados()
        {
            dgvPedidos.ClearSelection();
            foreach (DataGridViewRow dr in dgvPedidos.Rows)
            {
                if ((bool)dr.Cells["MarcarHabilitado"].Value)
                    dr.DefaultCellStyle.ForeColor = Color.Blue;
            }
        }

        /// <summary>
        /// AUTORIZACIONES GENERADAS POR EL GERENTE DE VENTAS - GV
        /// </summary>
        /// <param name="_ID"></param>
        /// <param name="_ClaveTipoProceso"></param>
        /// <param name="_referencia"></param>
        private void GeneraProcesoGV(int _ID, String _ClaveTipoProceso, int _referencia, String _Cliente)
        {
            DataTable _dtSolicitudes = new DataTable();
            List<int> _Solicitudes;
            String _observaciones = String.Empty;
            decimal _precio = 0;
            String _res = String.Empty;
            frmInputBox f;
            frmVisorSolicitudesEspeciales frmVisor;
            switch (_ClaveTipoProceso)
            {
                //*******************************************************************************************/
                //ACTIVIDAD 2. Llenado de Solicitudes de cotización de código y muestra con formato A027.
                #region ACTIVIDAD 2
                case "2GV1":
                    //2.1 AUTORIZAR
                    // validamos que la solicitud no siga rechazada
                    if (dgvPedidos.CurrentRow.Cells["EstatusPedido"].Value.ToString() != "A")
                    {
                        MessageBox.Show("La solicitud sigue rechazada, favor de editar la información correspondiente.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        break;
                    }
                    if ((MessageBox.Show("Desea introducir observaciones adicionales para la siguiente área?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes))
                    {
                        f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto);
                        f.Text = "OBSERVACIONES SIP";
                        f.lblTitulo.Text = "Introduce las observaciones: ";
                        f.ShowDialog();
                        _observaciones = f.txtOrden.Text.Trim();
                        if (_observaciones == "")
                        {
                            MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            break;
                        }
                    }
                    _res = setLineaTiempoPedido(_ID, "GO", "A", 4, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 1, _referencia, _Cliente);
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    break;
                case "2GV2":
                    //2.2 ENVIAR AUTORIZACION A DIRECCION
                    // validamos que la solicitud no siga rechazada
                    if (dgvPedidos.CurrentRow.Cells["EstatusPedido"].Value.ToString() != "A")
                    {
                        MessageBox.Show("La solicitud sigue rechazada, favor de editar la información correspondiente.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        break;
                    }
                    if ((MessageBox.Show("Desea introducir observaciones adicionales para la siguiente área?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes))
                    {
                        f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto);
                        f.Text = "OBSERVACIONES SIP";
                        f.lblTitulo.Text = "Introduce las observaciones: ";
                        f.ShowDialog();
                        _observaciones = f.txtOrden.Text.Trim();
                        if (_observaciones == "")
                        {
                            MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            break;
                        }
                    }
                    _res = setLineaTiempoPedido(_ID, "DG", "A", 3, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 1, _referencia, _Cliente);
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "2GV3":
                    //2.3 CONSULTAR SOLICITUD
                    frmVisor = new frmVisorSolicitudesEspeciales(_referencia);
                    frmVisor.Show();
                    break;
                case "2GV4":
                    //2.4 VER REPORTE DE ARTICULOS POR CLIENTE
                    String _criterioDeBusqueda = dgvPedidos.CurrentRow.Cells["NOMBRE"].Value.ToString();
                    frmReporteArticulosCliente frmReporteArticulosCliente = new frmReporteArticulosCliente(_criterioDeBusqueda);
                    frmReporteArticulosCliente.Show();
                    break;
                case "2GV5":
                    //2.5 Rechazar
                    f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto, true);
                    f.Text = "OBSERVACIONES SIP";
                    f.lblTitulo.Text = "Introduce las razones de rechazo: ";
                    f.ShowDialog();
                    _observaciones = f.txtOrden.Text.Trim();
                    if (_observaciones == "")
                    {
                        MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        break;
                    }
                    _res = setLineaTiempoPedido(_ID, "EV", "R", 1, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 1, _referencia, _Cliente);
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                #endregion
                //*******************************************************************************************/
                //ACTIVIDAD 5. Control de Decision para establecer precio
                #region ACTIVIDAD 5
                case "5GV1":
                    //5.1 Asignar precio
                    DataTable Solicitudes = new DataTable("SOLICITUDES");
                    String _codigo = String.Empty;
                    SOLICITUDES_ESPECIALES solicitudesBl = new SOLICITUDES_ESPECIALES();
                    Solicitudes = solicitudesBl.ConsultarSolicitud(_referencia);
                    if (Solicitudes.Rows.Count > 0)
                    {
                        _codigo = Solicitudes.Rows[0]["CODIGO_COTIZACION"].ToString();
                    }
                    DataTable dtInfoModelo = SimuladorCostos.ModeloExistente(_codigo, ref ex);

                    f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Numerica, true);
                    f.NTxtOrden.NumberType = UserControls.TipoDeNumero.Decimal;
                    f.Text = "ASIGNACIÓN DE PRECIO";
                    f.lblTitulo.Text = "Introduce el precio: ";
                    f.NTxtOrden.Text = dtInfoModelo.Rows[0]["PRECIO"].ToString();
                    f.ShowDialog();
                    if (f.NTxtOrden.Text.Trim() == "")
                    {
                        MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        break;
                    }
                    _precio = decimal.Parse(f.NTxtOrden.Text.Trim());
                    //ACTUALIZAMOS EL PRECIO DE LA SOLICITUD.
                    ulp_bl.SOLICITUDES_ESPECIALES.AsignaPrecio(_referencia, _precio);
                    ulp_bl.SOLICITUDES_ESPECIALES.AsignaPrecioCodigo(_codigo, _precio);
                    //ASIGNAMOS A LA SIGUIENTE AREA
                    _res = setLineaTiempoPedido(_ID, "EV", "A", 7, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 1, _referencia, _Cliente);
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    break;
                case "5GV2":
                    //5.2 Enviar a direccion para asignar precio
                    if ((MessageBox.Show("Desea introducir observaciones adicionales para la siguiente área?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes))
                    {
                        f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto);
                        f.Text = "OBSERVACIONES SIP";
                        f.lblTitulo.Text = "Introduce las observaciones: ";
                        f.ShowDialog();
                        _observaciones = f.txtOrden.Text.Trim();
                        if (_observaciones == "")
                        {
                            MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            break;
                        }
                    }
                    _res = setLineaTiempoPedido(_ID, "DG", "A", 6, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 1, _referencia, _Cliente);
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    break;
                case "5GV3":
                    //5.3 Ver datos Solicitud
                    frmVisor = new frmVisorSolicitudesEspeciales(_referencia);
                    frmVisor.Show();
                    break;
                #endregion
                //*******************************************************************************************/
                //ACTIVIDAD 7. Recepcion de Precio por Direccion
                #region ACTIVIDAD 7
                case "7GV1":
                    //7.1 Ver datos Solicitud
                    frmVisor = new frmVisorSolicitudesEspeciales(_referencia);
                    frmVisor.Show();
                    break;
                case "7GV2":
                    if ((MessageBox.Show("Desea introducir observaciones adicionales para la siguiente área?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes))
                    {
                        f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto);
                        f.Text = "OBSERVACIONES SIP";
                        f.lblTitulo.Text = "Introduce las observaciones: ";
                        f.ShowDialog();
                        _observaciones = f.txtOrden.Text.Trim();
                        if (_observaciones == "")
                        {
                            MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            break;
                        }
                    }
                    _res = setLineaTiempoPedido(_ID, "EV", "A", 7, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 1, _referencia, _Cliente);
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                #endregion
                //*******************************************************************************************/
                //ACTIVIDAD 10. Autorizacion de Pedido
                #region ACTIVIDAD 10
                case "10GV1":
                    //10.1 Autorizar Pedido                    
                    // CREAMOS AUTORIZACION POR GV
                    _res = setAutorizacionPedido(_referencia, "GV", Globales.UsuarioActual.UsuarioUsuario, true);
                    //MOSTRAMOS EL PEDIDO CON LA FIRMA
                    String _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    frmReportes.ShowDialog();
                    if (MessageBox.Show("¿Desea procesar la autorización del pedido?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        if ((MessageBox.Show("Desea introducir observaciones adicionales para la siguiente área?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes))
                        {
                            f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto);
                            f.Text = "OBSERVACIONES SIP";
                            f.lblTitulo.Text = "Introduce las observaciones: ";
                            f.ShowDialog();
                            _observaciones = f.txtOrden.Text.Trim();
                            if (_observaciones == "")
                            {
                                MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                break;
                            }
                        }
                        _res = setLineaTiempoPedido(_ID, "DG", "A", 11, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 2, _referencia, _Cliente);
                        // CREMAMOS EL REGISTRO EN UPPEDIDOS DE LA AUTORIZACION                        
                        UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                        uppedidosModif.PEDIDO = _referencia;
                        uppedidosModif.F_GV = DateTime.Now;
                        uppedidosModif.Modificar(uppedidosModif, "Autorización GV - Control Pedidos");
                        if (_res == "")
                        {
                            MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.reload = true;
                        }
                        else
                            MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        // CANCELAMOS LA AUTORIZACION PREVIA.                        
                        _res = setCancelaAutorizacionPedido(_referencia, "GV", Globales.UsuarioActual.UsuarioUsuario, "EV");
                    }
                    break;
                case "10GV2":
                    //10.2 Ver Pedido
                    _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    /* QUITAMOS IMPRESION NORMAL
                    SIP.Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido);
                     * */
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.OrdenTrabajo, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    if (frmReportes.pedidoTambienImprimeOT)
                    {
                        frmReportes.Show();
                    }
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    frmReportes.Show();
                    break;
                case "10GV3":
                    //10.3 Ver Datos de Solicitud
                    //OBTENEMOS LAS SOLICITUDES RELACIONADAS AL PEDIDO
                    _dtSolicitudes = ControlPedidos.getSolicitudesPedido(_referencia);
                    if (_dtSolicitudes.Rows.Count == 0)
                    {
                        MessageBox.Show("Imposible determinar las solicitudes asignadas al pedido.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                    _Solicitudes = new List<int> { };
                    foreach (DataRow _dr in _dtSolicitudes.Rows)
                    {
                        _Solicitudes.Add(int.Parse(_dr["Solicitud"].ToString()));
                    }
                    // CONSULTAR SOLICITUDES
                    frmVisorSolicitudesEspeciales frmVisorSolicitudesEspeciales = new frmVisorSolicitudesEspeciales(_Solicitudes);
                    frmVisorSolicitudesEspeciales.Show();
                    break;
                case "10GV4":
                    //10.4 Rechazar y liberar
                    f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto, true);
                    f.Text = "OBSERVACIONES SIP";
                    f.lblTitulo.Text = "Introduce las razones de rechazo: ";
                    f.ShowDialog();
                    _observaciones = f.txtOrden.Text.Trim();
                    if (_observaciones == "")
                    {
                        MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        break;
                    }
                    //LIBERAMOS EL PEDIDO A ESTATUS U PARA QUE EL EV LO PUEDA MODIFICAR
                    _res = EliminarHabilitarPedidoAspelSaeSip.Ejecutar(Convert.ToInt32(_referencia));
                    if (_res != "")
                    {
                        MessageBox.Show("Error al habilitar el pedido. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                    _res = setLineaTiempoPedido(_ID, "EV", "R", 10, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 2, _referencia, _Cliente);
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                #endregion
                //*******************************************************************************************/
                //CANCELACION
                #region CANCELACION
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        //ControlPedidos.setCancelaPedido(_referencia);
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                //EXPEDIENTE DIGITAL
                #region EXPEDIENTE DIGITAL
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion
                // ELIMINAR SOLICITUD
                #region ELIMINAR SOLICITUD
                case "ELIMINARSOLICITUD":
                    if (MessageBox.Show("¿Seguro que desea eliminar la solicitud seleccionada?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        SOLICITUDES_ESPECIALES.EliminaSolicitud(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    break;
                #endregion
            }
        }
        private void GeneraProcesoGVLinea(int _ID, String _ClaveTipoProceso, int _referencia, String _Cliente)
        {
            String _observaciones = String.Empty;
            String _tallas = string.Empty;
            String _res = String.Empty;
            int _pedido;
            int _idProceso;
            frmInputBox f;
            switch (_ClaveTipoProceso)
            {
                //*******************************************************************************************/
                //Actividad 2. AUTORIZACIÓN DE PEDIDO
                #region Actividad 2
                // NUEVO FLUJO YA NO APLICA
                /*
            case "2GV1":
                // 2.1 Enivar a dirección
                if ((MessageBox.Show("Desea introducir observaciones adicionales para la siguiente área?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes))
                {
                    f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto);
                    f.Text = "OBSERVACIONES SIP";
                    f.lblTitulo.Text = "Introduce las observaciones: ";
                    f.ShowDialog();
                    _observaciones = f.txtOrden.Text.Trim();
                    if (_observaciones == "")
                    {
                        MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        break;
                    }
                }
                _res = setLineaTiempoPedido(_ID, "DG", "A", 4, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 3, _referencia, _Cliente);
                if (_res == "")
                    MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                break;
                 */

                case "2GV2":
                    // 2.2 Ver Pedido
                    String _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    /* QUITAMOS IMPRESION NORMAL
                    SIP.Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido);
                     * */
                    Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.OrdenTrabajo, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    if (frmReportes.pedidoTambienImprimeOT)
                    {
                        frmReportes.Show();
                    }
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    frmReportes.Show();
                    break;
                case "2GV3":
                    // 2.3 Rechazar Pedido
                    f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto, true);
                    f.Text = "OBSERVACIONES SIP";
                    f.lblTitulo.Text = "Introduce las razones de rechazo: ";
                    f.ShowDialog();
                    _observaciones = f.txtOrden.Text.Trim();
                    if (_observaciones == "")
                    {
                        MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        break;
                    }
                    //LIBERAMOS EL PEDIDO A ESTATUS U PARA QUE EL EV LO PUEDA MODIFICAR
                    _res = EliminarHabilitarPedidoAspelSaeSip.Ejecutar(Convert.ToInt32(_referencia));
                    if (_res != "")
                    {
                        MessageBox.Show("Error al habilitar el pedido. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }

                    //_res = setLineaTiempoPedido(_ID, "CP", "R", 1, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 3, _referencia, _Cliente);
                    _res = setLineaTiempoPedido(_ID, "EV", "R", 3, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 3, _referencia, _Cliente);

                    // CANCELAMOS LA AUTORIZACION PREVIA
                    _res = setCancelaAutorizacionPedido(_referencia, "GV", Globales.UsuarioActual.UsuarioUsuario, "EV");
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "2GV4":
                    // 4.1 Autorizar
                    //1.1 Autorizar pedido
                    // CREAMOS AUTORIZACION POR GV
                    _res = setAutorizacionPedido(_referencia, "GV", Globales.UsuarioActual.UsuarioUsuario, true);
                    _res = setAutorizacionPedido(_referencia, "FA", Globales.UsuarioActual.UsuarioUsuario, true);
                    //MOSTRAMOS EL PEDIDO CON LA FIRMA
                    _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    frmReportes.ShowDialog();
                    //FINALMENTE VALIDAMOS SI SE QUIERE PROCESAR LA AUTORIZACION                    
                    if (MessageBox.Show("¿Desea procesar la autorización del pedido?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        if ((MessageBox.Show("Desea introducir observaciones adicionales para la siguiente área?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes))
                        {
                            f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto);
                            f.Text = "OBSERVACIONES SIP";
                            f.lblTitulo.Text = "Introduce las observaciones: ";
                            f.ShowDialog();
                            _observaciones = f.txtOrden.Text.Trim();
                            if (_observaciones == "")
                            {
                                MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                break;
                            }
                        }
                        // CAPTURAMOS DE FORMA AUTOMÁTICA EL PEDIDO EN SAE
                        // obtenemos la comision del pedido
                        decimal comision = 0;
                        int pedidoFacturar = dgvPedidos.CurrentRow.Cells["PedidoOrigen"].Value.ToString() == "N/A" ? _referencia : int.Parse(dgvPedidos.CurrentRow.Cells["PedidoOrigen"].Value.ToString());
                        if (pedidoFacturar != _referencia)
                        {
                            // si el pedido no ha sido facturado, facturamos el pedido original
                            // en caso de que ya haya sido facturado el pedido original, unicamente se procesa el pedido en flujo de control pedidos
                            if (CargaPedidosSAE.ValidaPedidoFacturado(pedidoFacturar, "P"))
                            {
                                if (MessageBox.Show(String.Format("Se procederá a facturar el Pedido {0} de manera anticipada. ¿Desea continuar?", pedidoFacturar.ToString()), "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                                {
                                    return;
                                }
                                // ENVIAMOS EL PEDIDO ORIGEN
                                PED_MSTR pedido_imprimir = new PED_MSTR();
                                DataTable datos_pedido = new DataTable();
                                datos_pedido = pedido_imprimir.ConsultaImprimir(pedidoFacturar);
                                comision = datos_pedido.Rows[0]["COMISION"] == null ? 0 : Convert.ToDecimal(datos_pedido.Rows[0]["COMISION"]);
                                CargaPedidosSAE.CargaPedidoEnSae(pedidoFacturar, comision, 16, ref ex, "P");
                            }
                        }
                        else
                        {
                            // ENVIAMOS EL PEDIDO ACTUAL
                            PED_MSTR pedido_imprimir = new PED_MSTR();
                            DataTable datos_pedido = new DataTable();
                            datos_pedido = pedido_imprimir.ConsultaImprimir(pedidoFacturar);
                            comision = datos_pedido.Rows[0]["COMISION"] == null ? 0 : Convert.ToDecimal(datos_pedido.Rows[0]["COMISION"]);
                            CargaPedidosSAE.CargaPedidoEnSae(pedidoFacturar, comision, 16, ref ex, "P");
                        }
                        // ENVIAMOS EL PEDIDO A CRÉDITO 
                        //_res = setLineaTiempoPedido(_ID, "CR", "A", 7, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 3, _referencia, _Cliente);
                        _res = setLineaTiempoPedido(_ID, "CP", "A", 1, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 3, _referencia, _Cliente);
                        // CREMAMOS EL REGISTRO EN UPPEDIDOS DE LA AUTORIZACION
                        UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                        uppedidosModif.PEDIDO = _referencia;
                        uppedidosModif.F_GV = DateTime.Now;
                        uppedidosModif.Modificar(uppedidosModif, "Autorización GV - Control Pedidos");
                        // CREMAMOS EL REGISTRO EN UPPEDIDOS DE LA CAPTURA
                        uppedidosModif = new UPPEDIDOS();
                        uppedidosModif.PEDIDO = _referencia;
                        uppedidosModif.F_CAPT_ASPEL = DateTime.Now;
                        uppedidosModif.Modificar(uppedidosModif, "Autorización GV - Control Pedidos");
                        if (_res == "")
                        {
                            MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.reload = true;
                        }
                        else
                            MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        // CANCELAMOS LA AUTORIZACION PREVIA.
                        _res = setCancelaAutorizacionPedido(_referencia, "GV", Globales.UsuarioActual.UsuarioUsuario, "EV");
                    }
                    break;

                #endregion
                //CANCELACION
                #region CANCELACION
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        //ControlPedidos.setCancelaPedido(_referencia);
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                //EXPEDIENTE DIGITAL
                #region EXPEDIENTE DIGITAL
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion
            }
        }

        /// <summary>
        /// AUTORIZACIONES GENERADAS POR EL COORDINADOR DE PROCESOS - CPR
        /// </summary>
        /// <param name="_ID"></param>
        /// <param name="_ClaveTipoProceso"></param>
        /// <param name="_referencia"></param>
        private void GeneraProcesoCPR(int _ID, String _ClaveTipoProceso, int _referencia, String _Cliente)
        {
            String _observaciones = String.Empty;
            String _res = String.Empty;
            frmInputBox f;
            switch (_ClaveTipoProceso)
            {
                //ACTIVIDAD 18. PROGRAMAR PRODUCCION
                #region ACTIVIDAD 18
                case "18CPR1":
                    //18.1 Orden de Produccion

                    frmOrdProduccionMasiva frmOrdProduccionMasiva = new frmOrdProduccionMasiva(_referencia);
                    frmOrdProduccionMasiva.ShowDialog();

                    /*
                    frmOrdProduccion frmOrdProduccion = new frmOrdProduccion();
                    frmOrdProduccion.ShowDialog();
                     * */
                    if (frmOrdProduccionMasiva.StatusProceso)
                    {
                        // _res = setLineaTiempoPedido(_ID, "SU", "A", 20, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 2, _referencia, _Cliente);
                        ControlPedidos.setFinPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    break;
                case "18CPR2":
                    //18.2 Ver Pedido
                    String _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    /* QUITAMOS IMPRESION NORMAL
                    SIP.Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido);
                     * */
                    Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.OrdenTrabajo, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    if (frmReportes.pedidoTambienImprimeOT)
                    {
                        frmReportes.Show();
                    }
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    frmReportes.Show();
                    break;
                case "18CPR3":
                    f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto, true);
                    f.Text = "OBSERVACIONES SIP";
                    f.lblTitulo.Text = "Introduce las razones de rechazo: ";
                    f.ShowDialog();
                    _observaciones = f.txtOrden.Text.Trim();
                    if (_observaciones == "")
                    {
                        MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        break;
                    }
                    _res = setLineaTiempoPedido(_ID, "GO", "R", 16, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 2, _referencia, _Cliente);
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "18CPR4":
                    // 20.2 FINALIZAR PEDIDO
                    ControlPedidos.setFinPedido(_referencia);
                    MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.reload = true;
                    break;
                #endregion
                //ACTIVIDAD 19. RECIBIR PRODUCCION
                #region ACTIVIDAD 19
                case "19CPR1":
                    frmRecOrdProduccionMaquilaCodigoBarras frmRecOrdProduccionMaquilaCodigoBarras = new frmRecOrdProduccionMaquilaCodigoBarras();
                    frmRecOrdProduccionMaquilaCodigoBarras.Show();
                    break;
                case "19CPR2":
                    if ((MessageBox.Show("Desea introducir observaciones adicionales para la siguiente área?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes))
                    {
                        f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto);
                        f.Text = "OBSERVACIONES SIP";
                        f.lblTitulo.Text = "Introduce las observaciones: ";
                        f.ShowDialog();
                        _observaciones = f.txtOrden.Text.Trim();
                        if (_observaciones == "")
                        {
                            MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            break;
                        }
                    }
                    _res = setLineaTiempoPedido(_ID, "SU", "A", 20, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 2, _referencia, _Cliente);
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "19CPR3":
                    _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    /* QUITAMOS IMPRESION NORMAL
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido);
                     * */
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.OrdenTrabajo, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    if (frmReportes.pedidoTambienImprimeOT)
                    {
                        frmReportes.Show();
                    }
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    frmReportes.Show();
                    break;
                #endregion
                //*******************************************************************************************/
                //CANCELACION
                #region CANCELACION
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        //ControlPedidos.setCancelaPedido(_referencia);
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                //EXPEDIENTE DIGITAL
                #region EXPEDIENTE DIGITAL
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion
                // ELIMINAR SOLICITUD
                #region ELIMINAR SOLICITUD
                case "ELIMINARSOLICITUD":
                    if (MessageBox.Show("¿Seguro que desea eliminar la solicitud seleccionada?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        SOLICITUDES_ESPECIALES.EliminaSolicitud(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    break;
                #endregion
            }
        }
        private void GeneraProcesoCPRLinea(int _ID, String _ClaveTipoProceso, int _referencia, String _Cliente)
        {
            String _observaciones = String.Empty;
            String _res = String.Empty;
            frmInputBox f;
            switch (_ClaveTipoProceso)
            {
                //*******************************************************************************************/
                //ACTIVIDAD 8. RUTA
                // NUEVO FLUJO YA NO APLICA
                /*
                #region ACTIVIDAD 8
                case "8CPR1":
                    //8.1 UP Pedidos
                    frmUpPedidos frmUpPedidos = new frmUpPedidos(_Cliente, _referencia);
                    frmUpPedidos.ShowDialog();
                    if (frmUpPedidos.procesado)
                    {
                        if ((MessageBox.Show("Desea introducir observaciones adicionales para la siguiente área?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes))
                        {
                            f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto);
                            f.Text = "OBSERVACIONES SIP";
                            f.lblTitulo.Text = "Introduce las observaciones: ";
                            f.ShowDialog();
                            _observaciones = f.txtOrden.Text.Trim();
                            if (_observaciones == "")
                            {
                                MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                break;
                            }
                        }
                        _res = setLineaTiempoPedido(_ID, "SU", "A", 10, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 3, _referencia, _Cliente);
                        if (_res == "")
                            MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case "8CPR2":
                    //8.2 Ver pedido
                    String _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    SIP.Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido);
                    frmReportes.enVentas = true;
                    frmReportes.Show();
                    break;
                #endregion
                 * */
                //ACTIVIDAD 12. EMPAQUE
                #region ACTIVIDAD 12
                case "12CPR1":
                    //8.1 UP Pedidos
                    frmUpPedidos frmUpPedidos = new frmUpPedidos(_Cliente, _referencia);
                    frmUpPedidos.ShowDialog();
                    if (frmUpPedidos.procesado)
                    {
                        ControlPedidos.setFinPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;

                    }
                    break;
                case "12CPR2":
                    //8.2 Ver pedido
                    String _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    /* QUITAMOS IMPRESION NORMAL
                    SIP.Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido);
                     * */
                    Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.OrdenTrabajo, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    if (frmReportes.pedidoTambienImprimeOT)
                    {
                        frmReportes.Show();
                    }
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    frmReportes.Show();
                    break;
                #endregion
                //CANCELACION
                #region CANCELACION
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        ControlPedidos.setCancelaPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                //EXPEDIENTE DIGITAL
                #region EXPEDIENTE DIGITAL
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion
            }
        }

        /// <summary>
        /// AUTORIZACIONES GENERADAS POR EL GERENTE DE OPERACIONES - GO
        /// </summary>
        /// <param name="_ID"></param>
        /// <param name="_ClaveTipoProceso"></param>
        /// <param name="_referencia"></param>
        private void GeneraProcesoGO(int _ID, String _ClaveTipoProceso, int _referencia, String _Cliente)
        {
            String _observaciones = String.Empty;
            String _res = String.Empty;
            frmInputBox f;
            switch (_ClaveTipoProceso)
            {
                //ACTIVIDAD 4. CREACION DE CODIGO, Y PLAZOS DE ENTREGA
                #region ACTIVIDAD 4
                case "4GO1":
                    //5.1 INGRRESAR CODIGO Y FECHA DE ENTREGA
                    String _Plazo, _Codigo;
                    _Plazo = _Codigo = String.Empty;
                    frmPlazoEntregaCodigoEspecial frmPlazoCodigo = new frmPlazoEntregaCodigoEspecial();
                    frmPlazoCodigo.ShowDialog();
                    _Plazo = frmPlazoCodigo.Plazo;
                    _Codigo = frmPlazoCodigo.Codigo;
                    if (_Plazo == "" || _Codigo == "")
                    {
                        MessageBox.Show("Se debe de indicar plazo y código para poder continuar", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }


                    //ACTUALIZAMOS LA SOLICITUD
                    SOLICITUDES_ESPECIALES.AsignaPlazoCodigo(_referencia, _Plazo, _Codigo);
                    //ASIGNAMOS LA SOLICITUD A LA SIGUIENTE AREA
                    if ((MessageBox.Show("Desea introducir observaciones adicionales para la siguiente área?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes))
                    {
                        f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto);
                        f.Text = "OBSERVACIONES SIP";
                        f.lblTitulo.Text = "Introduce las observaciones: ";
                        f.ShowDialog();
                        _observaciones = f.txtOrden.Text.Trim();
                        if (_observaciones == "")
                        {
                            MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            break;
                        }
                    }
                    _res = setLineaTiempoPedido(_ID, "GV", "A", 5, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 1, _referencia, _Cliente);
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "4GO2":
                    //4.2 Duplicador de Códigos
                    frmDupCodProdEstr frmDupCodProdEstr = new frmDupCodProdEstr();
                    frmDupCodProdEstr.Show();
                    break;
                case "4GO3":
                    //4.3 VER REPORTE DE ARTICULOS POR CLIENTE
                    String _criterioDeBusqueda = dgvPedidos.CurrentRow.Cells["NOMBRE"].Value.ToString();
                    frmReporteArticulosCliente frmReporteArticulosCliente = new frmReporteArticulosCliente(_criterioDeBusqueda);
                    frmReporteArticulosCliente.Show();
                    break;
                case "4GO4":
                    //4.4 CONSULTAR SOLICITUD
                    frmVisorSolicitudesEspeciales frmVisorSolicitudesEspeciales = new frmVisorSolicitudesEspeciales(_referencia);
                    frmVisorSolicitudesEspeciales.Show();
                    break;
                case "4GO5":
                    //4.5 Rechazar
                    f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto, true);
                    f.Text = "OBSERVACIONES SIP";
                    f.lblTitulo.Text = "Introduce las razones de rechazo: ";
                    f.ShowDialog();
                    _observaciones = f.txtOrden.Text.Trim();
                    if (_observaciones == "")
                    {
                        MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        break;
                    }
                    _res = setLineaTiempoPedido(_ID, "GV", "R", 2, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 1, _referencia, _Cliente);
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                #endregion
                //ACTIVIDAD 8. ASIGNACION DE CÓDIGOS.
                #region ACTIVIDAD 8
                case "8GO1":
                    //8.1 Ingresar Codigos Especiales.
                    String _codigos = String.Empty;
                    f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto, true);
                    f.Text = "ASIGNACION DE CÓDIGOS SIP";
                    f.lblTitulo.Text = "Introduce los códigos especiales: ";
                    f.ShowDialog();
                    _codigos = f.txtOrden.Text.Trim();
                    if (_codigos == "")
                    {
                        MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        break;
                    }
                    //ACTUALIZAMOS EL CAMPO DE CODIGO ASIGNADO
                    ulp_bl.SOLICITUDES_ESPECIALES.AsignaCodigos(_referencia, _codigos);
                    //ASIGNAMOS A LA SIGUIENTE AREA
                    _res = setLineaTiempoPedido(_ID, "EV", "A", 9, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 1, _referencia, _Cliente);
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "8GO2":
                    //8.2 Duplicador de Códigos
                    //OBTENEMOS EL CODIGO
                    DataTable Solicitudes = new DataTable("SOLICITUDES");
                    String _codigo = String.Empty;
                    SOLICITUDES_ESPECIALES solicitudesBl = new SOLICITUDES_ESPECIALES();
                    Solicitudes = solicitudesBl.ConsultarSolicitud(_referencia);
                    if (Solicitudes.Rows.Count > 0)
                    {
                        _codigo = Solicitudes.Rows[0]["CODIGO_COTIZACION"].ToString().Substring(0, 8);
                    }

                    frmDupCodProdEstr = new frmDupCodProdEstr(_codigo);
                    frmDupCodProdEstr.Show();
                    break;
                case "8GO3":
                    //8.3 Ver Datos Solicitud
                    frmVisorSolicitudesEspeciales = new frmVisorSolicitudesEspeciales(_referencia);
                    frmVisorSolicitudesEspeciales.Show();
                    break;
                case "8GO4":
                    //8.4 Rechazar
                    f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto, true);
                    f.Text = "OBSERVACIONES SIP";
                    f.lblTitulo.Text = "Introduce las razones de rechazo: ";
                    f.ShowDialog();
                    _observaciones = f.txtOrden.Text.Trim();
                    if (_observaciones == "")
                    {
                        MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        break;
                    }
                    _res = setLineaTiempoPedido(_ID, "EV", "R", 7, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 1, _referencia, _Cliente);
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                #endregion
                //ACTIVIDAD 16. PROGRAMACION DE PEDIDO*/
                #region ACTIVIDAD 16
                case "16GO1":
                    //16.1 Generar Requisicion de compra
                    //observaciones obligatorias
                    f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto, true);
                    f.Text = "OBSERVACIONES SIP";
                    f.lblTitulo.Text = "Introduce los datos de la requisición: ";
                    f.ShowDialog();
                    _observaciones = f.txtOrden.Text.Trim();
                    if (_observaciones == "")
                    {
                        MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
                    _res = setLineaTiempoPedido(_ID, "CP", "A", 17, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 2, _referencia, _Cliente);
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "16GO2":
                    //16.2 Enviar a programacion
                    //observaciones obligatorias
                    f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto, true);
                    f.Text = "OBSERVACIONES SIP";
                    f.lblTitulo.Text = "Introduce las observaciones: ";
                    f.ShowDialog();
                    _observaciones = f.txtOrden.Text.Trim();
                    if (_observaciones == "")
                    {
                        MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
                    _res = setLineaTiempoPedido(_ID, "CPR", "A", 18, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 2, _referencia, _Cliente);
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "16GO3":
                    //16.3 Ver pedido
                    String _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    /*QUITAMOS IMPRESION NORMAL
                    SIP.Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido);
                     * */
                    Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.OrdenTrabajo, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    if (frmReportes.pedidoTambienImprimeOT)
                    {
                        frmReportes.Show();
                    }
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    frmReportes.Show();
                    break;
                case "16GO4":
                    // 16.4 IMPRIMIR PEDIDO CON FIRMAS                    
                    _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, true);
                    frmReportes.enVentas = true;
                    frmReportes.ShowDialog();
                    if (frmReportes.pedidoEstatusModificado)
                    {
                        // CREMAMOS EL REGISTRO EN UPPEDIDOS
                        UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                        uppedidosModif.PEDIDO = _referencia;
                        uppedidosModif.F_IMPRESION = DateTime.Now;
                        uppedidosModif.Modificar(uppedidosModif, "Impresión Firmas - Control Pedidos");
                    }
                    break;
                #endregion
                //*******************************************************************************************/
                //CANCELACION
                #region CANCELACION
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        //ControlPedidos.setCancelaPedido(_referencia);
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                //EXPEDIENTE DIGITAL
                #region EXPEDIENTE DIGITAL
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion
                // ELIMINAR SOLICITUD
                #region ELIMINAR SOLICITUD
                case "ELIMINARSOLICITUD":
                    if (MessageBox.Show("¿Seguro que desea eliminar la solicitud seleccionada?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        SOLICITUDES_ESPECIALES.EliminaSolicitud(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    break;
                #endregion
            }
        }

        /// <summary>
        /// AUTORIZACIONES GENERADAS POR EL DIRECTOR GENERAL - DG
        /// </summary>
        /// <param name="_ID"></param>
        /// <param name="_ClaveTipoProceso"></param>
        /// <param name="_referencia"></param>
        private void GeneraProcesoDG(int _ID, String _ClaveTipoProceso, int _referencia, String _Cliente)
        {
            DataTable _dtSolicitudes = new DataTable();
            List<int> _Solicitudes;
            String _observaciones = String.Empty;
            String _res = String.Empty;
            Decimal _precio = 0;
            frmInputBox f;
            switch (_ClaveTipoProceso)
            {
                //ACTIVIDAD 3. Autorización electrónica de tela ESPECIAL.
                #region ACTIVIDAD 3
                case "3DG1":
                    //3.1 AUTORIZAR TELA DE LINEA
                    /*
                    if ((MessageBox.Show("Desea introducir observaciones adicionales para la siguiente área?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes))
                    {
                        f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto);
                        f.Text = "OBSERVACIONES SIP";
                        f.lblTitulo.Text = "Introduce las observaciones: ";
                        f.ShowDialog();
                        _observaciones = f.txtOrden.Text.Trim();
                        if (_observaciones == "")
                        {
                            MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            break;
                        }
                    }*/
                    _res = setLineaTiempoPedido(_ID, "GO", "A", 4, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 1, _referencia, _Cliente);
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    break;
                case "3DG2":
                    //3.2 Reporte de artíclos por cliente
                    String _criterioDeBusqueda = dgvPedidos.CurrentRow.Cells["NOMBRE"].Value.ToString();
                    frmReporteArticulosCliente frmReporteArticulosCliente = new frmReporteArticulosCliente(_criterioDeBusqueda);
                    frmReporteArticulosCliente.Show();
                    break;
                case "3DG3":
                    //3.3 CONSULTAR SOLICITUD
                    frmVisorSolicitudesEspeciales frmVisorSolicitudesEspeciales = new frmVisorSolicitudesEspeciales(_referencia);
                    frmVisorSolicitudesEspeciales.Show();
                    break;
                case "3DG4":
                    //3.4 RECHAZAR
                    f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto, true);
                    f.Text = "OBSERVACIONES SIP";
                    f.lblTitulo.Text = "Introduce las razones de rechazo: ";
                    f.ShowDialog();
                    _observaciones = f.txtOrden.Text.Trim();
                    if (_observaciones == "")
                    {
                        MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        break;
                    }
                    _res = setLineaTiempoPedido(_ID, "GV", "R", 2, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 1, _referencia, _Cliente);
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                #endregion
                //ACTIVIDAD 6. Establecer precio
                #region ACTIVIDAD6
                case "6DG1":
                    //6.1 Asignar Precio.
                    //OBTENEMOS EL PRECIO FINAL DEL SIMULADOR
                    DataTable Solicitudes = new DataTable("SOLICITUDES");
                    String _codigo = String.Empty;
                    SOLICITUDES_ESPECIALES solicitudesBl = new SOLICITUDES_ESPECIALES();
                    Solicitudes = solicitudesBl.ConsultarSolicitud(_referencia);
                    if (Solicitudes.Rows.Count > 0)
                    {
                        _codigo = Solicitudes.Rows[0]["CODIGO_COTIZACION"].ToString();
                    }
                    DataTable dtInfoModelo = SimuladorCostos.ModeloExistente(_codigo, ref ex);


                    f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Numerica, true);
                    f.NTxtOrden.NumberType = UserControls.TipoDeNumero.Decimal;
                    f.Text = "ASIGNACIÓN DE PRECIO";
                    f.lblTitulo.Text = "Introduce el precio: ";
                    f.NTxtOrden.Text = dtInfoModelo.Rows[0]["PRECIO"].ToString();
                    f.ShowDialog();
                    if (f.NTxtOrden.Text == "")
                    {
                        MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
                    _precio = decimal.Parse(f.NTxtOrden.Text.Trim());
                    //ACTUALIZAMOS EL PRECIO DE LA SOLICITUD.
                    ulp_bl.SOLICITUDES_ESPECIALES.AsignaPrecio(_referencia, _precio);
                    ulp_bl.SOLICITUDES_ESPECIALES.AsignaPrecioCodigo(_codigo, _precio);


                    //ASIGNAMOS A LA SIGUIENTE AREA
                    _res = setLineaTiempoPedido(_ID, "GV", "A", 7, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 1, _referencia, _Cliente);
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "6DG2":
                    //6.2 Simulador de costos
                    //segun la solicitud, obtenemos el codigo

                    _codigo = String.Empty;
                    solicitudesBl = new SOLICITUDES_ESPECIALES();
                    Solicitudes = solicitudesBl.ConsultarSolicitud(_referencia);
                    if (Solicitudes.Rows.Count > 0)
                    {
                        _codigo = Solicitudes.Rows[0]["CODIGO_COTIZACION"].ToString();
                    }

                    frmSimuladorCostos frmSimulador = new frmSimuladorCostos(Enumerados.TipoSimulador.SimuladorDeCostos, _codigo);
                    frmSimulador.ShowDialog();

                    if (frmSimulador.precioModificado)
                    {
                        if ((MessageBox.Show("Desea introducir observaciones adicionales para la siguiente área?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes))
                        {
                            f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto);
                            f.Text = "OBSERVACIONES SIP";
                            f.lblTitulo.Text = "Introduce las observaciones: ";
                            f.ShowDialog();
                            _observaciones = f.txtOrden.Text.Trim();
                            if (_observaciones == "")
                            {
                                MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                break;
                            }
                        }
                        //EN CASO DE QUE SE HAYA MODIFICADO EL PRECIO AVANZAMOS AUTOMATICAMENTE LA ORDEN
                        ulp_bl.SOLICITUDES_ESPECIALES.AsignaPrecio(_referencia, frmSimulador.precio);
                        //ASIGNAMOS A LA SIGUIENTE AREA
                        _res = setLineaTiempoPedido(_ID, "GV", "A", 7, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 1, _referencia, _Cliente);
                        if (_res == "")
                        {
                            MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.reload = true;
                        }
                        else
                            MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;
                case "6DG3":
                    //6.3 Rechazar
                    f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto, true);
                    f.Text = "OBSERVACIONES SIP";
                    f.lblTitulo.Text = "Introduce las razones de rechazo: ";
                    f.ShowDialog();
                    _observaciones = f.txtOrden.Text.Trim();
                    if (_observaciones == "")
                    {
                        MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        break;
                    }
                    _res = setLineaTiempoPedido(_ID, "GV", "R", 5, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 1, _referencia, _Cliente);
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "6DG4":
                    //3.3 CONSULTAR SOLICITUD
                    frmVisorSolicitudesEspeciales = new frmVisorSolicitudesEspeciales(_referencia);
                    frmVisorSolicitudesEspeciales.Show();
                    break;
                #endregion
                //ACTIVIDAD 11. Autorizar 
                #region ACTIVIDAD 11
                case "11DG1":
                    //11.1 Autorizar Pedido
                    _res = setAutorizacionPedido(_referencia, "DG", Globales.UsuarioActual.UsuarioUsuario, true);
                    //MOSTRAMOS EL PEDIDO CON LA FIRMA
                    String _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    frmReportes.ShowDialog();
                    if (MessageBox.Show("¿Desea procesar la autorización del pedido?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        /*
                        if ((MessageBox.Show("Desea introducir observaciones adicionales para la siguiente área?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes))
                        {
                            f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto);
                            f.Text = "OBSERVACIONES SIP";
                            f.lblTitulo.Text = "Introduce las observaciones: ";
                            f.ShowDialog();
                            _observaciones = f.txtOrden.Text.Trim();
                        }
                         */
                        _res = setLineaTiempoPedido(_ID, "CP", "A", 13, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 2, _referencia, _Cliente);
                        // CREMAMOS EL REGISTRO EN UPPEDIDOS DE LA AUTORIZACION                        
                        UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                        uppedidosModif.PEDIDO = _referencia;
                        uppedidosModif.F_DIRECCION = DateTime.Now;
                        uppedidosModif.Modificar(uppedidosModif, "Autorización DG - Control Pedidos");
                        if (_res == "")
                        {
                            MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.reload = true;
                        }
                        else
                            MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        // CANCELAMOS LA AUTORIZACION PREVIA.                        
                        _res = setCancelaAutorizacionPedido(_referencia, "DG", Globales.UsuarioActual.UsuarioUsuario, "GV");
                    }
                    break;
                case "11DG2":
                    //11.2 Simulador de Costos
                    //A ESTAS ALTURAS LA REFERENCIA YA ES EL "NUMERO DE PEDIDO", 
                    //OBTENEMOS LAS SOLICITUDES ASIGNADAS AL PEDIDO
                    _dtSolicitudes = ControlPedidos.getSolicitudesPedido(_referencia);
                    if (_dtSolicitudes.Rows.Count > 1)
                    {
                        //ABRIMOS LA VENTANA PARA SELECCION DE SOLICITUD Y DICHA VENTANA MANDARA A LLAMAR AL SIMULADOR CON LA SOLCIITUD SELECCIONADA
                        frmSeleccionSolicitudesSimulador frmSeleccionSolicitudesSimulador = new frmSeleccionSolicitudesSimulador(_dtSolicitudes);
                        frmSeleccionSolicitudesSimulador.Show();
                    }
                    else
                    {
                        //segun la solicitud, obtenemos el codigo
                        Solicitudes = new DataTable("SOLICITUDES");
                        _codigo = String.Empty;
                        solicitudesBl = new SOLICITUDES_ESPECIALES();
                        Solicitudes = solicitudesBl.ConsultarSolicitud(int.Parse(_dtSolicitudes.Rows[0]["Solicitud"].ToString()));
                        if (Solicitudes.Rows.Count > 0)
                        {
                            _codigo = Solicitudes.Rows[0]["CODIGO_COTIZACION"].ToString();
                            frmSimulador = new frmSimuladorCostos(Enumerados.TipoSimulador.SimuladorDeCostos, _codigo);
                            frmSimulador.Show();
                        }
                        else
                        {
                            _codigo = _dtSolicitudes.Rows[0]["CODIGO_COTIZACION"].ToString();
                            frmSimulador = new frmSimuladorCostos(Enumerados.TipoSimulador.SimuladorDeCostos, _codigo);
                            frmSimulador.Show();
                        }
                    }

                    break;
                case "11DG3":
                    //11.3 VER DATOS DE PEDIDO
                    //10.2 Ver Pedido
                    _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    /* QUITAMOS IMPRESION NORMAL
                    SIP.Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido);
                     * */
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.OrdenTrabajo, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    if (frmReportes.pedidoTambienImprimeOT)
                    {
                        frmReportes.Show();
                    }
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    frmReportes.Show();
                    break;
                case "11DG4":
                    //11.4 Ver solicitud
                    //OBTENEMOS LAS SOLICITUDES RELACIONADAS AL PEDIDO
                    _dtSolicitudes = ControlPedidos.getSolicitudesPedido(_referencia);
                    if (_dtSolicitudes.Rows.Count == 0)
                    {
                        MessageBox.Show("Imposible determinar las solicitudes asignadas al pedido.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                    _Solicitudes = new List<int> { };
                    foreach (DataRow _dr in _dtSolicitudes.Rows)
                    {
                        _Solicitudes.Add(int.Parse(_dr["Solicitud"].ToString()));
                    }
                    // CONSULTAR SOLICITUDES
                    frmVisorSolicitudesEspeciales = new frmVisorSolicitudesEspeciales(_Solicitudes);
                    frmVisorSolicitudesEspeciales.Show();
                    break;
                case "11DG5":
                    //11.5 Rechazar
                    //2.5 Rechazar
                    f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto, true);
                    f.Text = "OBSERVACIONES SIP";
                    f.lblTitulo.Text = "Introduce las razones de rechazo: ";
                    f.ShowDialog();
                    _observaciones = f.txtOrden.Text.Trim();
                    if (_observaciones == "")
                    {
                        MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        break;
                    }
                    // CANCELAMOS LA AUTORIZACION PREVIA.
                    _res = setCancelaAutorizacionPedido(_referencia, "DG", Globales.UsuarioActual.UsuarioUsuario, "GV");
                    _res = setLineaTiempoPedido(_ID, "GV", "R", 10, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 2, _referencia, _Cliente);
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                #endregion
                //*******************************************************************************************/
                //CANCELACION
                #region CANCELACION
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        //ControlPedidos.setCancelaPedido(_referencia);
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                //EXPEDIENTE DIGITAL
                #region EXPEDIENTE DIGITAL
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion
                // ELIMINAR SOLICITUD
                #region ELIMINAR SOLICITUD
                case "ELIMINARSOLICITUD":
                    if (MessageBox.Show("¿Seguro que desea eliminar la solicitud seleccionada?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        SOLICITUDES_ESPECIALES.EliminaSolicitud(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    break;
                #endregion
            }
        }
        private void GeneraProcesoDGLinea(int _ID, String _ClaveTipoProceso, int _referencia, String _Cliente)
        {
            String _observaciones = String.Empty;
            String _tallas = string.Empty;
            String _res = String.Empty;
            int _pedido;
            int _idProceso;
            frmInputBox f;
            switch (_ClaveTipoProceso)
            {
                //*******************************************************************************************/
                //Actividad 4. AUTORIZACIÓN
                #region ACTIVIDAD 4
                /*
                case "4DG1":
                    // 4.1 Autorizar
                    if ((MessageBox.Show("Desea introducir observaciones adicionales para la siguiente área?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes))
                    {
                        f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto);
                        f.Text = "OBSERVACIONES SIP";
                        f.lblTitulo.Text = "Introduce las observaciones: ";
                        f.ShowDialog();
                        _observaciones = f.txtOrden.Text.Trim();
                        if (_observaciones == "")
                        {
                            MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            break;
                        }
                    }
                    // CREAMOS AUTORIZACION POR DG
                    _res = setAutorizacionPedido(_referencia, "DG", Globales.UsuarioActual.UsuarioUsuario);
                    // ENVIAMOS EL PEDIDO AL GV
                    _res = setLineaTiempoPedido(_ID, "CP", "A", 5, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 3, _referencia, _Cliente);
                    if (_res == "")
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "4DG2":
                    // 2.2 Ver Pedido
                    String _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    SIP.Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido);
                    frmReportes.enVentas = true;
                    frmReportes.Show();
                    break;
                case "4DG3":
                    // 2.3 Rechazar Pedido
                    f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto, true);
                    f.Text = "OBSERVACIONES SIP";
                    f.lblTitulo.Text = "Introduce las razones de rechazo: ";
                    f.ShowDialog();
                    _observaciones = f.txtOrden.Text.Trim();
                    if (_observaciones == "")
                    {
                        MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        break;
                    }
                    _res = setLineaTiempoPedido(_ID, "GV", "R", 2, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 3, _referencia, _Cliente);
                    if (_res == "")
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                 * */
                #endregion
                //Actividad 13. AUTORIZACIÓN
                #region ACTIVIDAD 13
                case "13DG1":
                    // 4.1 Autorizar
                    // CREAMOS AUTORIZACION POR DG
                    _res = setAutorizacionPedido(_referencia, "DG", Globales.UsuarioActual.UsuarioUsuario, true);
                    //MOSTRAMOS EL PEDIDO CON LA FIRMA
                    String _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    frmReportes.ShowDialog();
                    //FINALMENTE VALIDAMOS SI SE QUIERE PROCESAR LA AUTORIZACION                    
                    if (MessageBox.Show("¿Desea procesar la autorización del pedido?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        /*
                        if ((MessageBox.Show("Desea introducir observaciones adicionales para la siguiente área?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes))
                        {
                            f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto);
                            f.Text = "OBSERVACIONES SIP";
                            f.lblTitulo.Text = "Introduce las observaciones: ";
                            f.ShowDialog();
                            _observaciones = f.txtOrden.Text.Trim();
                            if (_observaciones == "")
                            {
                                MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                break;
                            }
                        }*/
                        // ENVIAMOS EL PEDIDO A SU
                        _res = setLineaTiempoPedido(_ID, "SU", "A", 10, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 3, _referencia, _Cliente);
                        // CREMAMOS EL REGISTRO EN UPPEDIDOS
                        UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                        uppedidosModif.PEDIDO = _referencia;
                        uppedidosModif.F_DIRECCION = DateTime.Now;
                        uppedidosModif.Modificar(uppedidosModif, "Autorización Dirección - Control Pedidos");
                        if (_res == "")
                        {
                            MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.reload = true;
                        }
                        else
                            MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        // CANCELAMOS LA AUTORIZACION PREVIA.
                        _res = setCancelaAutorizacionPedido(_referencia, "DG", Globales.UsuarioActual.UsuarioUsuario, "PL");
                    }
                    break;
                case "13DG2":
                    // 2.2 Ver Pedido
                    _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    /* QUITAMOS IMPRESION NORMAL
                    Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido);
                     * */
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.OrdenTrabajo, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    if (frmReportes.pedidoTambienImprimeOT)
                    {
                        frmReportes.Show();
                    }
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    frmReportes.Show();
                    break;
                case "13DG3":
                    // 2.3 Rechazar Pedido
                    f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto, true);
                    f.Text = "OBSERVACIONES SIP";
                    f.lblTitulo.Text = "Introduce las razones de rechazo: ";
                    f.ShowDialog();
                    _observaciones = f.txtOrden.Text.Trim();
                    if (_observaciones == "")
                    {
                        MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        break;
                    }
                    _res = setLineaTiempoPedido(_ID, "CP", "R", 1, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 3, _referencia, _Cliente);
                    // CANCELAMOS LA AUTORIZACION PREVIA.
                    _res = setCancelaAutorizacionPedido(_referencia, "DG", Globales.UsuarioActual.UsuarioUsuario, "EV");
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                #endregion
                //*******************************************************************************************/
                //CANCELACION
                #region CANCELACION
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        //ControlPedidos.setCancelaPedido(_referencia);
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                //EXPEDIENTE DIGITAL
                #region EXPEDIENTE DIGITAL
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion
            }
        }

        /// <summary>
        /// AUTORIZACIONES GENERADAS POR EL EJECUTIVO DE VENTAS- EV
        /// </summary>
        /// <param name="_ID"></param>
        /// <param name="_ClaveTipoProceso"></param>
        /// <param name="_referencia"></param>
        private void GeneraProcesoEV(int _ID, String _ClaveTipoProceso, int _referencia, String _Cliente)
        {
            String _observaciones = String.Empty;
            String _tallas = string.Empty;
            String _res = String.Empty;
            int _pedido;
            int _idProceso;
            frmInputBox f;
            switch (_ClaveTipoProceso)
            {
                //******************************************************************
                //Actividad 1. SOLCITUD DE CODIGO ESPECIAL
                #region ACTIVIDAD 1
                case "1EV1":
                    //1.1 Buscador de Clientes
                    String _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    frmFindClie frmFindClie = new frmFindClie(_claveCliente);
                    frmFindClie.Show();
                    break;
                case "1EV2":
                    //1.2 Enviar solicitud a GV
                    // validamos que la solicitud no siga rechazada
                    if (dgvPedidos.CurrentRow.Cells["EstatusPedido"].Value.ToString() != "A")
                    {
                        MessageBox.Show("La solicitud sigue rechazada, favor de editar la información correspondiente.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        break;
                    }

                    if ((MessageBox.Show("Desea introducir observaciones adicionales para la siguiente área?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes))
                    {
                        f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto);
                        f.Text = "OBSERVACIONES SIP";
                        f.lblTitulo.Text = "Introduce las observaciones: ";
                        f.ShowDialog();
                        _observaciones = f.txtOrden.Text.Trim();
                        if (_observaciones == "")
                        {
                            MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            break;
                        }

                    }
                    _res = setLineaTiempoPedido(_ID, "GV", "A", 2, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 1, _referencia, _Cliente);
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                #endregion
                //******************************************************************
                //Actividad 7. SOLICITUD DE TALLAS
                #region ACTIVIDAD 7
                case "7EV1":
                    //7.1 Solicitud de tallas
                    frmCargaTallasEspeciales frmCargaTallasEspeciales = new frmCargaTallasEspeciales(_referencia);
                    frmCargaTallasEspeciales.ShowDialog();
                    /*
                    f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto, true);
                    f.Text = "SOLICITUD DE TALLAS SIP";
                    f.lblTitulo.Text = "Introduce las tallas solicitadas: ";
                    f.ShowDialog();
                    _tallas = f.txtOrden.Text.Trim();
                     * */
                    //ACTUALIZAMOS LAS TALLAS DE LA SOLICITUD
                    if (frmCargaTallasEspeciales.TallasString == "")
                    {
                        MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        break;
                    }
                    ulp_bl.SOLICITUDES_ESPECIALES.AsignaTallas(_referencia, frmCargaTallasEspeciales.TallasString);
                    List<String> Codigos = new List<String> { };
                    if (!frmCargaTallasEspeciales.codigoExistente)
                    {
                        Codigos = ulp_bl.SOLICITUDES_ESPECIALES.AsignaTallasCodigo(_referencia, frmCargaTallasEspeciales.Tallas);
                        if (Codigos.Count == 0)
                        {
                            MessageBox.Show("Error al generar la estructura de códigos.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                        ulp_bl.SOLICITUDES_ESPECIALES.AsignaCodigos(_referencia, String.Join(",", Codigos));
                        ulp_bl.SOLICITUDES_ESPECIALES.EliminaCodigoBaseEspecial(_referencia);
                    }
                    else
                    {
                        if (frmCargaTallasEspeciales.TallasAdicionales.Count > 0)
                        {
                            ulp_bl.SOLICITUDES_ESPECIALES.AsignaTallasCodigo(_referencia, frmCargaTallasEspeciales.TallasAdicionales);
                        }
                        Codigos = frmCargaTallasEspeciales.Codigos;
                        ulp_bl.SOLICITUDES_ESPECIALES.AsignaCodigos(_referencia, String.Join(",", Codigos));
                    }
                    //ASIGNAMOS A LA SIGUIENTE AREA 
                    //SE MANDA DIRECTO AL EJECUTIVO DE VENTA
                    //_res = setLineaTiempoPedido(_ID, "GO", "A", 8, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 1, _referencia, _Cliente);
                    _res = setLineaTiempoPedido(_ID, "EV", "A", 9, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 1, _referencia, _Cliente);
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);


                    break;
                case "7EV2":
                    //7.2 Ver Datos Solicitud
                    frmVisorSolicitudesEspeciales frmVisorSolicitudesEspeciales = new frmVisorSolicitudesEspeciales(_referencia);
                    frmVisorSolicitudesEspeciales.Show();
                    break;
                #endregion
                //******************************************************************
                //Actividad 9. ALTA DE PEDIDO
                #region ACTIVIDAD 9
                case "9EV1":
                    /*
                    //9.1 Buscador de Clientes
                    List<int> Solicitudes = new List<int> { };
                    String _error = String.Empty;
                    Solicitudes = getSolicitudesSeleccionadas(ref _error);
                    if (Solicitudes.Count > 0)
                    {
                        // MOSTRAMOS EL VISOR DE LAS SOLICITUDES SELECCIONADAS
                        frmVisorSolicitudesEspeciales = new frmVisorSolicitudesEspeciales(Solicitudes);
                        frmVisorSolicitudesEspeciales.Show();
                    }
                    _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    frmFindClie = new frmFindClie(_claveCliente);
                    frmFindClie.Show();
                     * */
                    // 9.1 Alta de pedido a traves de códigos especiales
                    List<int> Solicitudes = new List<int> { };
                    String _error = String.Empty;
                    Solicitudes = getSolicitudesSeleccionadas(ref _error);

                    if (Solicitudes.Count > 0)
                    {
                        // VALIDAMOS QUE ALGUNA DE LAS SOLICITUDES, NOE STEN RELACIONADAS A ALGUN PEDIDO
                        DataTable dtPedidosPorSolicitud = ControlPedidos.getPedidosPorSolicitud(String.Join(",", Solicitudes));
                        if (dtPedidosPorSolicitud.Rows.Count > 0)
                        {
                            MessageBox.Show("Error al procesar. Descripción: " + "Alguna de las solicitudes seleccionadas ya tiene creado un PEDIDO.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                            // MOSTRAMOS EL VISOR DE LAS SOLICITUDES SELECCIONADAS
                            frmVisorSolicitudesEspeciales = new frmVisorSolicitudesEspeciales(Solicitudes);
                            frmVisorSolicitudesEspeciales.Show();
                            // MOSTRAMOS EL ALTA DE NUEVO PEDIDO
                            frmNuevoPedido frmNuevoPedido = new frmNuevoPedido(_claveCliente, Solicitudes);
                            frmNuevoPedido.ShowDialog();
                            if (frmNuevoPedido.numeroPedido > 0 && frmNuevoPedido.procesado) // SE CREO EL PEDIDO Y SE PASO A VENTAS
                            {
                                frmVisorSolicitudesEspeciales.Close();
                                //YA QUE TENEMOS EL PEDIDO Y LAS SOLICITUDES ASIGNADAS, DAMOS DE ALTA LA RELACION
                                ControlPedidos.setSolicitudesToPedido(frmNuevoPedido.numeroPedido, Solicitudes);
                                //PARA CADA SOLICITUD MATAMOS LA LINEA DE TIEMPO.
                                ControlPedidos.setFinSolicitudes(Solicitudes);
                                //DAMOS DE ALTA UNA NUEVA LINEA DE TIEMPO AHORA PARA EL PEDIDO
                                //_idProceso = int.Parse(ControlPedidos.getSiguienteIDProceso().Rows[0]["ID"].ToString());
                                //setAltaLineaTiempoPedido(_idProceso, "EV", "A", 9, "", Globales.UsuarioActual.UsuarioUsuario, 2, frmNuevoPedido.numeroPedido, _Cliente);

                                //_res = setLineaTiempoPedido(_idProceso, "GV", "A", 10, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 2, frmNuevoPedido.numeroPedido, _Cliente);
                                //if (_res == "")
                                MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.reload = true;
                                //else
                                //MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                            else if (frmNuevoPedido.numeroPedido > 0) // SOLAMENTE SE CREA EL PEDIDO
                            {
                                ControlPedidos.setSolicitudesToPedido(frmNuevoPedido.numeroPedido, Solicitudes);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error al procesar. Descripción: " + "Se debe de seleciconar al menos 1 código para realizar el pedido.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case "9EV2":
                    //9.2
                    //7.2 Ver Datos Solicitud
                    frmVisorSolicitudesEspeciales = new frmVisorSolicitudesEspeciales(_referencia);
                    frmVisorSolicitudesEspeciales.Show();
                    break;
                case "9EV3":
                    //9.3 ASignar solicitudes al pedido
                    //VALIDAMOS QUE HAYA UNA SOLICITUD AL MENOS SELECCIONADA
                    //CASO 1) NO EXISTE NINGUNA SOLICITUD, POR LO QUE ASIGNAMOS LA SELECCIONADA AL DAR CLIC
                    //CASO 2) EXISTEN MAS DE 2 SOLICITUDES SELECCIONADAS POR LO TANTO VALIDAMOS 2 COSAS
                    // 1) QUE PERTENEZCAN AL MISMO CLIENTE
                    // 2) QUE ESTEN EN ESTATUS "PEDIDO SIP" U ORDEN AGRUPADOR SEA EL 9
                    Solicitudes = new List<int> { };
                    _error = String.Empty;
                    Solicitudes = getSolicitudesSeleccionadas(ref _error);
                    if (_error != "")
                    {
                        MessageBox.Show("No se puede continuar con la solicitud. Razón: " + _error, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }

                    f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Numerica);
                    f.NTxtOrden.NumberType = UserControls.TipoDeNumero.Integer;
                    f.Text = "PEDIDO SIP";
                    f.lblTitulo.Text = "Introduce el numero de pedido generado: ";
                    f.ShowDialog();
                    if (f.NTxtOrden.Text == "")
                    {
                        MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        break;
                    }
                    _pedido = int.Parse(f.NTxtOrden.Text.Trim());
                    // BUSCAMOS EL ESTATUS DEL PEDIDO
                    DataTable dtPedido = ControlPedidos.getPedidosPorSolicitud(String.Join(",", Solicitudes));
                    if (dtPedido.Rows.Count > 0)
                    {
                        if (dtPedido.Rows.Count != Solicitudes.Count)
                        {
                            MessageBox.Show("Error al procesar. Descripción: Alguna de las solicitudes seleccionadas no tienen Pedido generado.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                        else
                        {
                            if (dtPedido.Select("Pedido = " + _pedido.ToString()).Any())
                            {
                                if (dtPedido.Select("Pedido = " + _pedido.ToString()).FirstOrDefault()["ESTATUS"] == "P")
                                {
                                    MessageBox.Show("Error al procesar. Descripción: El Pedido no se ha liberado a ventas.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Error al procesar. Descripción: El Pedido no corresponde a ninguna de las solicitudes seleccionadas.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (dtPedido.Rows.Count != Solicitudes.Count)
                        {
                            MessageBox.Show("Error al procesar. Descripción: Alguna de las solicitudes seleccionadas no tienen Pedido generado.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    }

                    //YA QUE TENEMOS EL PEDIDO Y LAS SOLICITUDES ASIGNADAS, DAMOS DE ALTA LA RELACION
                    ControlPedidos.setSolicitudesToPedido(_pedido, Solicitudes);
                    //PARA CADA SOLICITUD MATAMOS LA LINEA DE TIEMPO.
                    ControlPedidos.setFinSolicitudes(Solicitudes);
                    //DAMOS DE ALTA UNA NUEVA LINEA DE TIEMPO AHORA PARA EL PEDIDO SIEMPRE Y CUANDO NO EXISTA
                    DataTable dtControlPedidos = ControlPedidos.getLineaTiempo(_pedido);
                    if (dtControlPedidos.Rows.Count == 0)
                    {
                        _idProceso = int.Parse(ControlPedidos.getSiguienteIDProceso().Rows[0]["ID"].ToString());

                        setAltaLineaTiempoPedido(_idProceso, "EV", "A", 9, "", Globales.UsuarioActual.UsuarioUsuario, 2, _pedido, _Cliente);
                        _res = setLineaTiempoPedido(_idProceso, "GV", "A", 10, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 2, _pedido, _Cliente);

                        if (_res == "")
                        {
                            MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.reload = true;
                        }
                        else
                            MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    break;
                case "9EV4":
                    CLIE01 clientes = new CLIE01();
                    Globales.tablaclientes = clientes.Consultar(Globales.UsuarioActual, "");
                    frmBusquedaGenerica frmBusquedaGenerica = new frmBusquedaGenerica(ulp_bl.Globales.tablaclientes, "CLAVE", "LOCALIZACIÓN DE CLIENTES");
                    frmBusquedaGenerica.ShowDialog();
                    if (frmBusquedaGenerica.RenglonSeleccionado != null)
                    {
                        if (MessageBox.Show(String.Format("¿Está seguro que desea reemplazar el cliente de la solicitud actual por {0} - {1} ?", frmBusquedaGenerica.RenglonSeleccionado["Clave"].ToString().Trim(), frmBusquedaGenerica.RenglonSeleccionado["Nombre"].ToString().Trim()), "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            SOLICITUDES_ESPECIALES.ReasignaCliente(_referencia, frmBusquedaGenerica.RenglonSeleccionado["Clave"].ToString().Trim());
                            MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.reload = true;
                        }
                    }
                    break;
                #endregion
                //******************************************************************
                //Actividad 10. CORRECCION DE PEDIDO
                #region ACTIVIDAD 10
                case "10EV1":
                    //10.1 Buscador Cliente:
                    _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    frmFindClie = new frmFindClie(_claveCliente);
                    frmFindClie.Show();
                    break;
                case "10EV2":
                    //10.2 Enviar autorizacion
                    if ((MessageBox.Show("Desea introducir observaciones adicionales para la siguiente área?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes))
                    {
                        f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto);
                        f.Text = "OBSERVACIONES SIP";
                        f.lblTitulo.Text = "Introduce las observaciones: ";
                        f.ShowDialog();
                        _observaciones = f.txtOrden.Text.Trim();
                        if (_observaciones == "")
                        {
                            MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            break;
                        }
                    }
                    _res = setLineaTiempoPedido(_ID, "GV", "A", 10, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 2, _referencia, _Cliente);
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                #endregion
                //*******************************************************************************************/
                //CANCELACION
                #region CANCELACION
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        //ControlPedidos.setCancelaPedido(_referencia);
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                //EXPEDIENTE DIGITAL
                #region EXPEDIENTE DIGITAL
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion
                // ELIMINAR SOLICITUD
                #region ELIMINAR SOLICITUD
                case "ELIMINARSOLICITUD":
                    if (MessageBox.Show("¿Seguro que desea eliminar la solicitud seleccionada?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        SOLICITUDES_ESPECIALES.EliminaSolicitud(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    break;
                #endregion
            }
        }
        private void GeneraProcesoEVLinea(int _ID, String _ClaveTipoProceso, int _referencia, String _Cliente)
        {
            String _observaciones = String.Empty;
            String _tallas = string.Empty;
            String _res = String.Empty;
            int _pedido;
            int _idProceso;
            String _claveCliente;
            frmInputBox f;
            switch (_ClaveTipoProceso)
            {
                //Actividad 3. CORRECCION DE PEDIDO
                #region ACTIVIDAD 3
                case "3EV1":
                    //10.1 Buscador Cliente:
                    _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    frmFindClie frmFindClie = new frmFindClie(_claveCliente, "frmControlPedidos");
                    frmFindClie.Show();
                    dtPedidosPorArea = getPedidosPorArea("EV", Globales.UsuarioActual.UsuarioUsuario);
                    dgvPedidos.DataSource = dtPedidosPorArea;
                    break;
                case "3EV2":
                    //10.2 Enviar autorizacion
                    //SE VALIDA QUE EL PEDIDO HAYA SIDO PASADO A VENTAS
                    if (dgvPedidos.CurrentRow.Cells["ESTATUSPEDIDO"].Value.ToString() != "I")
                    {
                        MessageBox.Show("El pedido no puede ser procesado, ya que no se ha pasado a ventas.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if ((MessageBox.Show("Desea introducir observaciones adicionales para la siguiente área?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes))
                    {
                        f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto);
                        f.Text = "OBSERVACIONES SIP";
                        f.lblTitulo.Text = "Introduce las observaciones: ";
                        f.ShowDialog();
                        _observaciones = f.txtOrden.Text.Trim();
                        if (_observaciones == "")
                        {
                            MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            break;
                        }
                    }
                    // CREAMOS AUTORIZACION POR EV
                    _res = setAutorizacionPedido(_referencia, "EV", Globales.UsuarioActual.UsuarioUsuario);
                    _res = setLineaTiempoPedido(_ID, "GV", "A", 2, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 3, _referencia, _Cliente);
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                #endregion
                //*******************************************************************************************/
                //CANCELACION
                #region CANCELACION
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        //ControlPedidos.setCancelaPedido(_referencia);
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                //EXPEDIENTE DIGITAL
                #region EXPEDIENTE DIGITAL
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion
            }
        }

        /// <summary>
        /// AUTORIZACIONES GENERADAS POR EL COORDINADOR DE PEDIDOS- CP
        /// </summary>
        /// <param name="_ID"></param>
        /// <param name="_ClaveTipoProceso"></param>
        /// <param name="_referencia"></param>
        private void GeneraProcesoCP(int _ID, String _ClaveTipoProceso, int _referencia, String _Cliente)
        {
            String _claveCliente;
            String _observaciones = String.Empty;
            String _tallas = string.Empty;
            String _res = String.Empty;
            String _fecha = String.Empty;
            DataTable _dtSolicitudes = new DataTable();
            List<int> _Solicitudes;
            frmInputBox f;
            switch (_ClaveTipoProceso)
            {
                //*********************************************************************
                //Actividad 12. Fecha de entrega con base a codigos especiales
                #region ACTIVIDAD 12
                case "12CP1":
                    //12.1 Asignar Fecha
                    f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Fecha, true);
                    f.Text = "ASIGNACION DE CÓDIGOS SIP";
                    f.lblTitulo.Text = "Introduce la fecha de entrega: ";
                    f.ShowDialog();
                    _fecha = f.dtpFecha.Value.ToString("dd/MM/yyyy");
                    if (_fecha == "")
                    {
                        MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        break;
                    }
                    //ACTUALIZAMOS LA FECHA DE ENTREGA DEL PEDIDO
                    PED_MSTR.ActualizaFechaEntrega(_referencia, DateTime.Parse(_fecha));
                    //ENVIAMOS EL PEDIDO A LA SIGUIENTE ÁREA
                    //ASIGNAMOS A LA SIGUIENTE AREA
                    _res = setLineaTiempoPedido(_ID, "FA", "A", 13, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 2, _referencia, _Cliente);
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "12CP2":
                    //12.2 Ver Pedido
                    _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    /* QUITAMOS IMPRESION NORMAL
                    SIP.Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido);
                     * */
                    Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.OrdenTrabajo, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    if (frmReportes.pedidoTambienImprimeOT)
                    {
                        frmReportes.Show();
                    }
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    frmReportes.Show();
                    break;
                case "12CP3":
                    //12.3 Ver Solicitud
                    _dtSolicitudes = ControlPedidos.getSolicitudesPedido(_referencia);
                    if (_dtSolicitudes.Rows.Count == 0)
                    {
                        MessageBox.Show("Imposible determinar las solicitudes asignadas al pedido.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                    _Solicitudes = new List<int> { };
                    foreach (DataRow _dr in _dtSolicitudes.Rows)
                    {
                        _Solicitudes.Add(int.Parse(_dr["Solicitud"].ToString()));
                    }
                    // CONSULTAR SOLICITUDES
                    frmVisorSolicitudesEspeciales frmVisorSolicitudesEspeciales = new frmVisorSolicitudesEspeciales(_Solicitudes);
                    frmVisorSolicitudesEspeciales.Show();
                    break;

                #endregion
                //Actividad 13. ENVIO A FACTURACION
                #region ACTIVIDAD 13
                case "13CP1":
                    //13.1 CAPTURAR PEDIDO A SAE
                    //frmCargaPedidosSAE frmSAE = new frmCargaPedidosSAE(_referencia);
                    //frmSAE.ShowDialog();
                    _res = setAutorizacionPedido(_referencia, "CP", Globales.UsuarioActual.UsuarioUsuario, true);
                    _res = setAutorizacionPedido(_referencia, "FA", Globales.UsuarioActual.UsuarioUsuario, true);

                    //MOSTRAMOS EL PEDIDO CON LA FIRMA
                    _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    frmReportes.ShowDialog();

                    //FINALMENTE VALIDAMOS SI SE QUIERE PROCESAR LA AUTORIZACION                    
                    if (MessageBox.Show("¿Desea procesar la autorización del pedido?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        if ((MessageBox.Show("Desea introducir observaciones adicionales para la siguiente área?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes))
                        {
                            f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto);
                            f.Text = "OBSERVACIONES SIP";
                            f.lblTitulo.Text = "Introduce las observaciones: ";
                            f.ShowDialog();
                            _observaciones = f.txtOrden.Text.Trim();
                        }
                        // CAPTURAMOS DE FORMA AUTOMÁTICA EL PEDIDO EN SAE
                        // obtenemos la comision del pedido                        
                        decimal comision = 0;
                        int pedidoFacturar = dgvPedidos.CurrentRow.Cells["PedidoOrigen"].Value.ToString() == "N/A" ? _referencia : int.Parse(dgvPedidos.CurrentRow.Cells["PedidoOrigen"].Value.ToString());

                        if (pedidoFacturar != _referencia)
                        {
                            // si el pedido no ha sido facturado, facturamos el pedido original
                            // en caso de que ya haya sido facturado el pedido original, unicamente se procesa el pedido en flujo de control pedidos
                            if (CargaPedidosSAE.ValidaPedidoFacturado(pedidoFacturar, "P"))
                            {
                                if (MessageBox.Show(String.Format("Se procederá a facturar el Pedido {0} de manera anticipada. ¿Desea continuar?", pedidoFacturar.ToString()), "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                                {
                                    return;
                                }
                                // ENVIAMOS EL PEDIDO ORIGEN
                                PED_MSTR pedido_imprimir = new PED_MSTR();
                                DataTable datos_pedido = new DataTable();
                                datos_pedido = pedido_imprimir.ConsultaImprimir(pedidoFacturar);
                                comision = datos_pedido.Rows[0]["COMISION"] == null ? 0 : Convert.ToDecimal(datos_pedido.Rows[0]["COMISION"]);
                                CargaPedidosSAE.CargaPedidoEnSae(pedidoFacturar, comision, 16, ref ex, "P");
                            }
                        }
                        else
                        {
                            // ENVIAMOS EL PEDIDO ACTUAL
                            PED_MSTR pedido_imprimir = new PED_MSTR();
                            DataTable datos_pedido = new DataTable();
                            datos_pedido = pedido_imprimir.ConsultaImprimir(pedidoFacturar);
                            comision = datos_pedido.Rows[0]["COMISION"] == null ? 0 : Convert.ToDecimal(datos_pedido.Rows[0]["COMISION"]);
                            CargaPedidosSAE.CargaPedidoEnSae(pedidoFacturar, comision, 16, ref ex, "P");
                        }

                        //SE MANDA EN AUTOMATICO A LA SIGUIENTE ÁREA
                        _res = setLineaTiempoPedido(_ID, "CR", "A", 14, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 2, _referencia, _Cliente);
                        // CREMAMOS EL REGISTRO EN UPPEDIDOS DE LA AUTORIZACION                        
                        UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                        uppedidosModif.PEDIDO = _referencia;
                        uppedidosModif.F_COORDINADOR = DateTime.Now;
                        uppedidosModif.Modificar(uppedidosModif, "Autorización CP - Control Pedidos");
                        if (_res == "")
                        {
                            MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.reload = true;
                        }
                        else
                            MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        // CANCELAMOS LA AUTORIZACION PREVIA.
                        _res = setCancelaAutorizacionPedido(_referencia, "GV", Globales.UsuarioActual.UsuarioUsuario, "DG");
                    }
                    break;
                case "13CP2":
                    //13.2 RECHAZAR
                    f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto, true);
                    f.Text = "OBSERVACIONES SIP";
                    f.lblTitulo.Text = "Introduce las razones de rechazo: ";
                    f.ShowDialog();
                    _observaciones = f.txtOrden.Text.Trim();
                    if (_observaciones == "")
                    {
                        MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        break;
                    }
                    _res = setLineaTiempoPedido(_ID, "GV", "R", 10, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 2, _referencia, _Cliente);
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "13CP3":
                    _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    /* QUITAMOS IMPRESION NORMAL
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido);
                     * */
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.OrdenTrabajo, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    if (frmReportes.pedidoTambienImprimeOT)
                    {
                        frmReportes.Show();
                    }
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    frmReportes.Show();
                    break;
                case "13CP4":
                    //13.4 Rechazar y liberar
                    f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto, true);
                    f.Text = "OBSERVACIONES SIP";
                    f.lblTitulo.Text = "Introduce las razones de rechazo: ";
                    f.ShowDialog();
                    _observaciones = f.txtOrden.Text.Trim();
                    if (_observaciones == "")
                    {
                        MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        break;
                    }
                    //LIBERAMOS EL PEDIDO A ESTATUS U PARA QUE EL EV LO PUEDA MODIFICAR
                    _res = EliminarHabilitarPedidoAspelSaeSip.Ejecutar(Convert.ToInt32(_referencia));
                    if (_res != "")
                    {
                        MessageBox.Show("Error al habilitar el pedido. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                    _res = setLineaTiempoPedido(_ID, "EV", "R", 10, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 2, _referencia, _Cliente);
                    // CANCELAMOS LA AUTORIZACION PREVIA
                    _res = setCancelaAutorizacionPedido(_referencia, "CP", Globales.UsuarioActual.UsuarioUsuario, "EV");
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                #endregion
                //Actividad 15. NEGOCIACION
                #region ACTIVIDAD 15
                case "15CP1":
                    //15.1 Enviar a área de Crédito
                    //OBSERVACIONES OBLIGATORIAS.
                    _res = setAutorizacionPedido(_referencia, "CP", Globales.UsuarioActual.UsuarioUsuario, true);
                    _res = setAutorizacionPedido(_referencia, "FA", Globales.UsuarioActual.UsuarioUsuario, true);

                    //MOSTRAMOS EL PEDIDO CON LA FIRMA
                    _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    frmReportes.ShowDialog();

                    if (MessageBox.Show("¿Desea procesar la autorización del pedido?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        if ((MessageBox.Show("Desea introducir observaciones adicionales para la siguiente área?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes))
                        {
                            f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto);
                            f.Text = "OBSERVACIONES SIP";
                            f.lblTitulo.Text = "Introduce las observaciones: ";
                            f.ShowDialog();
                            _observaciones = f.txtOrden.Text.Trim();
                        }
                        // CAPTURAMOS DE FORMA AUTOMÁTICA EL PEDIDO EN SAE
                        // obtenemos la comision del pedido                        
                        decimal comision = 0;
                        PED_MSTR pedido_imprimir = new PED_MSTR();
                        DataTable datos_pedido = new DataTable();
                        datos_pedido = pedido_imprimir.ConsultaImprimir(Convert.ToInt32(_referencia));
                        comision = datos_pedido.Rows[0]["COMISION"] == null ? 0 : Convert.ToDecimal(datos_pedido.Rows[0]["COMISION"]);
                        CargaPedidosSAE.CargaPedidoEnSae(Convert.ToInt32(_referencia), comision, 16, ref ex, "P");

                        //SE MANDA EN AUTOMATICO A LA SIGUIENTE ÁREA
                        _res = setLineaTiempoPedido(_ID, "CR", "A", 14, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 2, _referencia, _Cliente);
                        // CREMAMOS EL REGISTRO EN UPPEDIDOS DE LA AUTORIZACION                        
                        UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                        uppedidosModif.PEDIDO = _referencia;
                        uppedidosModif.F_COORDINADOR = DateTime.Now;
                        uppedidosModif.Modificar(uppedidosModif, "Autorización CP - Control Pedidos");
                        if (_res == "")
                        {
                            MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.reload = true;
                        }
                        else
                            MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    else
                    {
                        // CANCELAMOS LA AUTORIZACION PREVIA.
                        _res = setCancelaAutorizacionPedido(_referencia, "GV", Globales.UsuarioActual.UsuarioUsuario, "DG");
                    }
                    break;

                //f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto, true);
                //f.Text = "OBSERVACIONES SIP";
                //f.lblTitulo.Text = "Introduce las observaciones: ";
                //f.ShowDialog();
                //_observaciones = f.txtOrden.Text.Trim();
                //if (_observaciones == "")
                //{
                //    MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    break;
                //}
                //_res = setLineaTiempoPedido(_ID, "CR", "A", 14, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 2, _referencia, _Cliente);
                //if (_res == "")
                //{
                //    MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    this.reload = true;
                //}
                //else
                //    MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //break;
                case "15CP2":
                    //15.2 Buscador Cliente
                    //9.1 Buscador de Clientes
                    _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    frmFindClie frmFindClie = new frmFindClie(_claveCliente);
                    frmFindClie.Show();
                    break;
                case "15CP3":
                    //13.4 Rechazar y liberar
                    f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto, true);
                    f.Text = "OBSERVACIONES SIP";
                    f.lblTitulo.Text = "Introduce las razones de rechazo: ";
                    f.ShowDialog();
                    _observaciones = f.txtOrden.Text.Trim();
                    if (_observaciones == "")
                    {
                        MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        break;
                    }
                    //LIBERAMOS EL PEDIDO A ESTATUS U PARA QUE EL EV LO PUEDA MODIFICAR
                    _res = EliminarHabilitarPedidoAspelSaeSip.Ejecutar(Convert.ToInt32(_referencia));
                    if (_res != "")
                    {
                        MessageBox.Show("Error al habilitar el pedido. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                    _res = setLineaTiempoPedido(_ID, "EV", "R", 10, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 2, _referencia, _Cliente);
                    // CANCELAMOS LA AUTORIZACION PREVIA
                    _res = setCancelaAutorizacionPedido(_referencia, "DG", Globales.UsuarioActual.UsuarioUsuario, "EV");
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                #endregion
                //Actividad 17. REQUISICION DE COMPRAS
                #region ACTIVIDAD 17
                case "17CP1":
                    //17.1 Liberar Compra
                    //observaciones obligatorias
                    f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto, true);
                    f.Text = "OBSERVACIONES SIP";
                    f.lblTitulo.Text = "Introduce observaciones: ";
                    f.ShowDialog();
                    _observaciones = f.txtOrden.Text.Trim();
                    if (_observaciones == "")
                    {
                        MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
                    _res = setLineaTiempoPedido(_ID, "CA", "A", 22, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 2, _referencia, _Cliente);
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "17CP2":
                    //17.2 Ver Pedido
                    _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.OrdenTrabajo, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    if (frmReportes.pedidoTambienImprimeOT)
                    {
                        frmReportes.Show();
                    }
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido);
                    frmReportes.enVentas = true;
                    frmReportes.Show();
                    break;
                #endregion
                //*******************************************************************************************/
                //CANCELACION
                #region CANCELACION
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        //ControlPedidos.setCancelaPedido(_referencia);
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                //EXPEDIENTE DIGITAL
                #region EXPEDIENTE DIGITAL
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion
                // ELIMINAR SOLICITUD
                #region ELIMINAR SOLICITUD
                case "ELIMINARSOLICITUD":
                    if (MessageBox.Show("¿Seguro que desea eliminar la solicitud seleccionada?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        SOLICITUDES_ESPECIALES.EliminaSolicitud(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    break;
                #endregion
            }
        }
        private void GeneraProcesoCPLinea(int _ID, String _ClaveTipoProceso, int _referencia, String _Cliente)
        {
            String _observaciones = String.Empty;
            String _tallas = string.Empty;
            String _res = String.Empty;
            int _pedido;
            int _idProceso;
            frmInputBox f;
            switch (_ClaveTipoProceso)
            {
                //******************************************************************
                //Actividad 1. AUTORIZAR PEDIDO
                #region ACTIVIDAD 1
                case "1CP1":
                    //1.1 Autorizar pedido
                    // CREAMOS AUTORIZACION POR CP
                    _res = setAutorizacionPedido(_referencia, "CP", Globales.UsuarioActual.UsuarioUsuario, true);
                    //MOSTRAMOS EL PEDIDO CON LA FIRMA
                    String _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    frmReportes.ShowDialog();
                    //FINALMENTE VALIDAMOS SI SE QUIERE PROCESAR LA AUTORIZACION                    
                    if (MessageBox.Show("¿Desea procesar la autorización del pedido?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        if ((MessageBox.Show("Desea introducir observaciones adicionales para la siguiente área?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes))
                        {
                            f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto);
                            f.Text = "OBSERVACIONES SIP";
                            f.lblTitulo.Text = "Introduce las observaciones: ";
                            f.ShowDialog();
                            _observaciones = f.txtOrden.Text.Trim();
                            if (_observaciones == "")
                            {
                                MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                break;
                            }
                        }
                        // ENVIAMOS EL PEDIDO AL GV
                        //_res = setLineaTiempoPedido(_ID, "GV", "A", 2, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 3, _referencia, _Cliente);
                        // ENVIAMOS EL PEDIDO A CR
                        _res = setLineaTiempoPedido(_ID, "CR", "A", 7, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 3, _referencia, _Cliente);
                        // CREMAMOS EL REGISTRO EN UPPEDIDOS                        
                        UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                        uppedidosModif.PEDIDO = _referencia;
                        uppedidosModif.F_COORDINADOR = DateTime.Now;
                        uppedidosModif.Modificar(uppedidosModif, "Autorización de CP - Control Pedidos");


                        if (_res == "")
                        {
                            MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.reload = true;
                        }
                        else
                            MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        // CANCELAMOS LA AUTORIZACION PREVIA.
                        _res = setCancelaAutorizacionPedido(_referencia, "CP", Globales.UsuarioActual.UsuarioUsuario, "FA");
                        if (_res != "")
                            MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case "1CP2":
                    //1.2 Ver pedido
                    _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    /*QUITAMOS LA IMPRESION NORMAL
                    SIP.Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido);
                     * */
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.OrdenTrabajo, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    if (frmReportes.pedidoTambienImprimeOT)
                    {
                        frmReportes.Show();
                    }
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    frmReportes.Show();
                    break;
                case "1CP3":
                    //1.3 Rechazar y liberar
                    f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto, true);
                    f.Text = "OBSERVACIONES SIP";
                    f.lblTitulo.Text = "Introduce las razones de rechazo: ";
                    f.ShowDialog();
                    _observaciones = f.txtOrden.Text.Trim();
                    if (_observaciones == "")
                    {
                        MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        break;
                    }
                    //LIBERAMOS EL PEDIDO A ESTATUS U PARA QUE EL EV LO PUEDA MODIFICAR
                    _res = EliminarHabilitarPedidoAspelSaeSip.Ejecutar(Convert.ToInt32(_referencia));
                    if (_res != "")
                    {
                        MessageBox.Show("Error al habilitar el pedido. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                    _res = setLineaTiempoPedido(_ID, "EV", "R", 3, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 3, _referencia, _Cliente);
                    // CANCELAMOS LA AUTORIZACION PREVIA.
                    _res = setCancelaAutorizacionPedido(_referencia, "CP", Globales.UsuarioActual.UsuarioUsuario, "EV");
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                #endregion
                //Actividad 5. ENVIAR A FACTURACIÓN
                #region ACTIVIDAD 5
                case "5CP1":
                    //5.1 ENVIAR A FACTURACION
                    if ((MessageBox.Show("Desea introducir observaciones adicionales para la siguiente área?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes))
                    {
                        f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto);
                        f.Text = "OBSERVACIONES SIP";
                        f.lblTitulo.Text = "Introduce las observaciones: ";
                        f.ShowDialog();
                        _observaciones = f.txtOrden.Text.Trim();
                    }
                    _res = setAutorizacionPedido(_referencia, "CP", Globales.UsuarioActual.UsuarioUsuario);
                    _res = setLineaTiempoPedido(_ID, "FA", "A", 6, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 3, _referencia, _Cliente);
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "5CP2":
                    // 5.2 Ver Pedido
                    _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    /*
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido);
                     * */
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.OrdenTrabajo, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    if (frmReportes.pedidoTambienImprimeOT)
                    {
                        frmReportes.Show();
                    }
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    frmReportes.Show();
                    break;
                #endregion
                //Actividad 9. NEGOCIACION DE LINEA DE CREDITO
                #region ACTIVIDAD 9
                case "9CP1":
                    //15.1 Enviar a área de Crédito
                    //OBSERVACIONES OBLIGATORIAS.
                    f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto, true);
                    f.Text = "OBSERVACIONES SIP";
                    f.lblTitulo.Text = "Introduce las observaciones: ";
                    f.ShowDialog();
                    _observaciones = f.txtOrden.Text.Trim();
                    if (_observaciones == "")
                    {
                        MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
                    _res = setLineaTiempoPedido(_ID, "CR", "A", 7, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 3, _referencia, _Cliente);
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "9CP2":
                    //15.2 Buscador Cliente
                    //9.1 Buscador de Clientes
                    _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    frmFindClie frmFindClie = new frmFindClie(_claveCliente);
                    frmFindClie.Show();
                    break;
                #endregion
                // Actividad 14. CAPTURA PEDIDO SAE
                #region ACTIVIDAD 14
                case "14CP1":
                    //13.1 Captura de Pedido SAE 6
                    frmCargaPedidosSAE frmSAE = new frmCargaPedidosSAE(_referencia);
                    frmSAE.ShowDialog();
                    // VERIFICAMOS EL PROCESO Y SI SE CAPTURA EN SAE, 
                    if (frmSAE.StatusProceso == true)
                    {
                        _res = setAutorizacionPedido(_referencia, "FA", Globales.UsuarioActual.UsuarioUsuario, true);
                        //SE MANDA EN AUTOMATICO A LA SIGUIENTE ÁREA
                        _res = setLineaTiempoPedido(_ID, "CR", "A", 7, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 3, _referencia, _Cliente);
                        // CREMAMOS EL REGISTRO EN UPPEDIDOS
                        UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                        uppedidosModif.PEDIDO = _referencia;
                        uppedidosModif.Modificar(uppedidosModif, "UpPedidos");
                        if (_res == "")
                        {
                            MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.reload = true;
                        }
                        else
                            MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        break;
                    }
                    break;
                case "14CP2":
                    //6.2 Ver Pedido
                    _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    /* QUITAMOS IMPRESION NORMAL
                    SIP.Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido);
                     * */
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.OrdenTrabajo, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    if (frmReportes.pedidoTambienImprimeOT)
                    {
                        frmReportes.Show();
                    }
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    frmReportes.Show();
                    break;
                #endregion
                //CANCELACION
                #region CANCELACION
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        //ControlPedidos.setCancelaPedido(_referencia);
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                //EXPEDIENTE DIGITAL
                #region EXPEDIENTE DIGITAL
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion
            }

        }

        /// <summary>
        /// AUTORIZACIONES GENERADAS POR EL FACTURISTA - FA
        /// </summary>
        /// <param name="_ID"></param>
        /// <param name="_ClaveTipoProceso"></param>
        /// <param name="_referencia"></param>
        private void GeneraProcesoFA(int _ID, String _ClaveTipoProceso, int _referencia, String _Cliente)
        {
            String _res = String.Empty;
            String _observaciones = String.Empty;
            frmInputBox f;
            switch (_ClaveTipoProceso)
            {
                //*********************************************************************
                //Actividad 13. Captura de Pedido en SAE
                #region ACTIVIDAD 13
                case "13FA1":
                    //13.1 Captura de Pedido SAE 6
                    frmCargaPedidosSAE frmSAE = new frmCargaPedidosSAE(_referencia);
                    frmSAE.ShowDialog();
                    // VERIFICAMOS EL PROCESO Y SI SE CAPTURA EN SAE, 
                    if (frmSAE.StatusProceso == true)
                    {
                        _res = setAutorizacionPedido(_referencia, "FA", Globales.UsuarioActual.UsuarioUsuario);
                        //SE MANDA EN AUTOMATICO A LA SIGUIENTE ÁREA
                        _res = setLineaTiempoPedido(_ID, "CR", "A", 14, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 2, _referencia, _Cliente);
                        if (_res == "")
                        {
                            MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.reload = true;
                        }
                        else
                            MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        break;
                    }
                    break;
                case "13FA2":
                    //13.3 Ver Pedido
                    String _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    /*
                    SIP.Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido);
                     * */
                    Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.OrdenTrabajo, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    if (frmReportes.pedidoTambienImprimeOT)
                    {
                        frmReportes.Show();
                    }
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    frmReportes.Show();
                    break;
                #endregion
                //*******************************************************************************************/
                //CANCELACION
                #region CANCELACION
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        //ControlPedidos.setCancelaPedido(_referencia);
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                //EXPEDIENTE DIGITAL
                #region EXPEDIENTE DIGITAL
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion
                // ELIMINAR SOLICITUD
                #region ELIMINAR SOLICITUD
                case "ELIMINARSOLICITUD":
                    if (MessageBox.Show("¿Seguro que desea eliminar la solicitud seleccionada?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        SOLICITUDES_ESPECIALES.EliminaSolicitud(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    break;
                #endregion
            }
        }
        private void GeneraProcesoFALinea(int _ID, String _ClaveTipoProceso, int _referencia, String _Cliente)
        {
            String _res = String.Empty;
            String _observaciones = String.Empty;
            frmInputBox f;
            switch (_ClaveTipoProceso)
            {
                //*********************************************************************
                //Actividad 6. Captura de Pedido en SAE
                #region ACTIVIDAD 6
                case "6FA1":
                    //13.1 Captura de Pedido SAE 6
                    frmCargaPedidosSAE frmSAE = new frmCargaPedidosSAE(_referencia);
                    frmSAE.ShowDialog();
                    // VERIFICAMOS EL PROCESO Y SI SE CAPTURA EN SAE, 
                    if (frmSAE.StatusProceso == true)
                    {
                        _res = setAutorizacionPedido(_referencia, "FA", Globales.UsuarioActual.UsuarioUsuario, true);
                        //SE MANDA EN AUTOMATICO A LA SIGUIENTE ÁREA
                        _res = setLineaTiempoPedido(_ID, "CR", "A", 7, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 3, _referencia, _Cliente);
                        if (_res == "")
                        {
                            MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.reload = true;
                        }
                        else
                            MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        break;
                    }
                    break;
                case "6FA2":
                    //6.2 Ver Pedido
                    String _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    /* QUITAMOS IMPRESION NORMAL
                    SIP.Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido);
                     * */
                    Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    frmReportes.Show();
                    break;
                #endregion
                //*******************************************************************************************/
                //CANCELACION
                #region CANCELACION
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        //ControlPedidos.setCancelaPedido(_referencia);
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                //EXPEDIENTE DIGITAL
                #region EXPEDIENTE DIGITAL
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion
            }
        }

        /// <summary>
        /// AUTORIZACIONES GENERADAS POR EL AREA DE CREDITO - CR
        /// </summary>
        /// <param name="_ID"></param>
        /// <param name="_ClaveTipoProceso"></param>
        /// <param name="_referencia"></param>
        private void GeneraProcesoCR(int _ID, String _ClaveTipoProceso, int _referencia, String _Cliente)
        {
            String _res = String.Empty;
            String _observaciones = String.Empty;
            frmFindClie frmFindClie;
            String _claveCliente;
            frmInputBox f;
            switch (_ClaveTipoProceso)
            {
                //*********************************************************************
                //Actividad 14. Verificar credito
                #region ACTIVIDAD 14
                case "14CR1":
                    //14.1 Autorizar linea de crédito
                    // CREAMOS AUTORIZACION POR CR
                    _res = setAutorizacionPedido(_referencia, "CR", Globales.UsuarioActual.UsuarioUsuario, true);
                    //MOSTRAMOS EL PEDIDO CON LA FIRMA
                    _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    frmReportes.ShowDialog();
                    //FINALMENTE VALIDAMOS SI SE QUIERE PROCESAR LA AUTORIZACION                    
                    if (MessageBox.Show("¿Desea procesar la autorización del pedido?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        if ((MessageBox.Show("Desea introducir observaciones adicionales para la siguiente área?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes))
                        {
                            f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto);
                            f.Text = "OBSERVACIONES SIP";
                            f.lblTitulo.Text = "Introduce las observaciones: ";
                            f.ShowDialog();
                            _observaciones = f.txtOrden.Text.Trim();
                            if (_observaciones == "")
                            {
                                MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                break;
                            }
                        }
                        // CREMAMOS EL REGISTRO EN UPPEDIDOS
                        UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                        uppedidosModif.PEDIDO = _referencia;
                        uppedidosModif.F_CREDITO = DateTime.Now;
                        uppedidosModif.Modificar(uppedidosModif, "Autorización Crédito - Control Pedidos");
                        // _res = setLineaTiempoPedido(_ID, "GO", "A", 16, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 2, _referencia, _Cliente); SE AJUSTA FLUJO POR PETICION DE GV
                        _res = setLineaTiempoPedido(_ID, "CA", "A", 22, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 2, _referencia, _Cliente);
                        if (_res == "")
                        {
                            MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.reload = true;
                        }
                        else
                            MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        // CANCELAMOS LA AUTORIZACION PREVIA.
                        _res = setCancelaAutorizacionPedido(_referencia, "CR", Globales.UsuarioActual.UsuarioUsuario, "FA");
                    }
                    break;
                case "14CR2":
                    //14.2 Rechazar linea de crédito

                    f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto, true);
                    f.Text = "OBSERVACIONES SIP";
                    f.lblTitulo.Text = "Introduce las observaciones: ";
                    f.ShowDialog();
                    _observaciones = f.txtOrden.Text.Trim();
                    if (_observaciones == "")
                    {
                        MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
                    // CANCELAMOS LA AUTORIZACION PREVIA.
                    _res = setCancelaAutorizacionPedido(_referencia, "CR", Globales.UsuarioActual.UsuarioUsuario, "DG");
                    _res = setLineaTiempoPedido(_ID, "CP", "R", 15, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 2, _referencia, _Cliente);
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "14CR3":
                    //14.3 Buscador de cliente para estado de cuenta
                    _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    frmFindClie = new frmFindClie(_claveCliente);
                    frmFindClie.Show();
                    break;
                case "14CR4":
                    // 14.4 VER PEDIDO
                    _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    /* QUITAMOS IMPRESION NORMAL
                    SIP.Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido);
                     * */
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.OrdenTrabajo, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    if (frmReportes.pedidoTambienImprimeOT)
                    {
                        frmReportes.Show();
                    }
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    frmReportes.Show();
                    break;
                #endregion
                //*******************************************************************************************/
                //CANCELACION
                #region CANCELACION
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        //ControlPedidos.setCancelaPedido(_referencia);
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                //EXPEDIENTE DIGITAL
                #region EXPEDIENTE DIGITAL
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion
                // ELIMINAR SOLICITUD
                #region ELIMINAR SOLICITUD
                case "ELIMINARSOLICITUD":
                    if (MessageBox.Show("¿Seguro que desea eliminar la solicitud seleccionada?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        SOLICITUDES_ESPECIALES.EliminaSolicitud(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    break;
                #endregion

            }
        }
        private void GeneraProcesoCRLinea(int _ID, String _ClaveTipoProceso, int _referencia, String _Cliente)
        {
            String _res = String.Empty;
            String _observaciones = String.Empty;
            frmFindClie frmFindClie;
            String _claveCliente;
            frmInputBox f;
            switch (_ClaveTipoProceso)
            {
                //*********************************************************************
                //Actividad 7. Verificar credito
                #region ACTIVIDAD 14
                case "7CR1":
                    //7.1 Autorizar linea de crédito
                    // CREAMOS AUTORIZACION POR CR
                    _res = setAutorizacionPedido(_referencia, "CR", Globales.UsuarioActual.UsuarioUsuario, true);
                    //MOSTRAMOS EL PEDIDO CON LA FIRMA
                    _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    frmReportes.ShowDialog();
                    //FINALMENTE VALIDAMOS SI SE QUIERE PROCESAR LA AUTORIZACION                    
                    if (MessageBox.Show("¿Desea procesar la autorización del pedido?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        if ((MessageBox.Show("Desea introducir observaciones adicionales para la siguiente área?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes))
                        {
                            f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto);
                            f.Text = "OBSERVACIONES SIP";
                            f.lblTitulo.Text = "Introduce las observaciones: ";
                            f.ShowDialog();
                            _observaciones = f.txtOrden.Text.Trim();
                            if (_observaciones == "")
                            {
                                MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                break;
                            }
                        }
                        // ENVIAMOS EL PEDIDO A FACTURACION
                        _res = setLineaTiempoPedido(_ID, "PL", "A", 8, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 3, _referencia, _Cliente);
                        // CREMAMOS EL REGISTRO EN UPPEDIDOS
                        UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                        uppedidosModif.PEDIDO = _referencia;
                        uppedidosModif.F_CREDITO = DateTime.Now;
                        uppedidosModif.Modificar(uppedidosModif, "Autorización Crédito - Control Pedidos");
                        if (_res == "")
                        {
                            MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.reload = true;
                        }
                        else
                            MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        // CANCELAMOS LA AUTORIZACION PREVIA.
                        _res = setCancelaAutorizacionPedido(_referencia, "CR", Globales.UsuarioActual.UsuarioUsuario, "CP");
                    }
                    break;
                case "7CR2":
                    //7.2 Rechazar linea de crédito
                    f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto, true);
                    f.Text = "OBSERVACIONES SIP";
                    f.lblTitulo.Text = "Introduce las observaciones: ";
                    f.ShowDialog();
                    _observaciones = f.txtOrden.Text.Trim();
                    if (_observaciones == "")
                    {
                        MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }

                    _res = setLineaTiempoPedido(_ID, "CP", "R", 1, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 3, _referencia, _Cliente);
                    // CANCELAMOS LA AUTORIZACION PREVIA.
                    _res = setCancelaAutorizacionPedido(_referencia, "CR", Globales.UsuarioActual.UsuarioUsuario, "GV");
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "7CR3":
                    //7.3 Buscador de cliente
                    _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    frmFindClie = new frmFindClie(_claveCliente);
                    frmFindClie.Show();
                    break;
                case "7CR4":
                    //7.4 Buscador de cliente para estado de cuenta
                    _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    frmFindClie = new frmFindClie(_claveCliente, "frmControlPedidos", "EstadoCuenta");
                    frmFindClie.Show();
                    break;

                #endregion
                //*******************************************************************************************/
                //CANCELACION
                #region CANCELACION
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        //ControlPedidos.setCancelaPedido(_referencia);
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                //EXPEDIENTE DIGITAL
                #region EXPEDIENTE DIGITAL
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion

            }
        }

        /// <summary>
        /// AUTORIZACIONES GENERADAS POR EL AREA DE SURTIDO - SU
        /// </summary>
        /// <param name="_ID"></param>
        /// <param name="_ClaveTipoProceso"></param>
        /// <param name="_referencia"></param>
        /// <param name="_Cliente"></param>
        private void GeneraprocesoSU(int _ID, String _ClaveTipoProceso, int _referencia, String _Cliente)
        {
            String _claveCliente;
            String _res = String.Empty;
            switch (_ClaveTipoProceso)
            {
                //*********************************************************************
                //Actividad 20. IMPRESION DE PEDIDO CON FIRMAS DIGITALES
                #region ACTIVIDAD 20
                case "20SU1":
                    // 20.1 IMPRIMIR PEDIDO CON FIRMAS
                    _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    SIP.Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, true);
                    frmReportes.enVentas = true;
                    frmReportes.ShowDialog();
                    if (frmReportes.pedidoEstatusModificado)
                    {
                        // CREMAMOS EL REGISTRO EN UPPEDIDOS
                        UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                        uppedidosModif.PEDIDO = _referencia;
                        uppedidosModif.F_IMPRESION = DateTime.Now;
                        uppedidosModif.Modificar(uppedidosModif, "Impresión Firmas - Control Pedidos");
                        //_res = setLineaTiempoPedido(_ID, "SU", "A", 21, "", Globales.UsuarioActual.UsuarioUsuario, 2, _referencia, _Cliente);                        
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                //*********************************************************************
                //Actividad 21. FINALIZAR PEDIDO
                #region ACTIVIDAD 21
                case "21SU1":
                    // 20.2 FINALIZAR PEDIDO
                    ControlPedidos.setFinPedido(_referencia);
                    MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.reload = true;
                    break;
                #endregion
                //*******************************************************************************************/
                //CANCELACION
                #region CANCELACION
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        //ControlPedidos.setCancelaPedido(_referencia);
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                //EXPEDIENTE DIGITAL
                #region EXPEDIENTE DIGITAL
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion
                // ELIMINAR SOLICITUD
                #region ELIMINAR SOLICITUD
                case "ELIMINARSOLICITUD":
                    if (MessageBox.Show("¿Seguro que desea eliminar la solicitud seleccionada?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        SOLICITUDES_ESPECIALES.EliminaSolicitud(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    break;
                #endregion
            }
        }
        private void GeneraprocesoSULinea(int _ID, String _ClaveTipoProceso, int _referencia, String _Cliente)
        {
            String _claveCliente;
            String _res = String.Empty;
            frmInputBox f;
            String _observaciones = "";
            switch (_ClaveTipoProceso)
            {
                //*********************************************************************
                //Actividad 10. TRANSFERENCIA Y LIBERACIÓN
                #region ACTIVIDAD 10
                case "10SU1":
                    //10.1 Transferencia de pedido
                    frmTransferenciaXModelo frmTransferenciaXModelo = new frmTransferenciaXModelo();
                    frmTransferenciaXModelo.Show();
                    break;
                case "10SU2":
                    //10.3 Imprimir y liberar pedido
                    _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, true);
                    frmReportes.enVentas = true;
                    frmReportes.ShowDialog();
                    if (frmReportes.pedidoEstatusModificado)
                    {
                        // CREMAMOS EL REGISTRO EN UPPEDIDOS
                        UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                        uppedidosModif.PEDIDO = _referencia;
                        uppedidosModif.F_IMPRESION = DateTime.Now;
                        uppedidosModif.Modificar(uppedidosModif, "Impresión Firmas - Control Pedidos");
                        //FIN DEL PROCESO
                        ControlPedidos.setFinPedido(_referencia);
                        //_res = setLineaTiempoPedido(_ID, "EM", "A", 11, "", Globales.UsuarioActual.UsuarioUsuario, 3, _referencia, _Cliente);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;

                #endregion
                //*******************************************************************************************/
                //CANCELACION
                #region CANCELACION
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        //ControlPedidos.setCancelaPedido(_referencia);
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                //EXPEDIENTE DIGITAL
                #region EXPEDIENTE DIGITAL
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion

            }
        }

        /// <summary>
        /// AUTORIZACIONES GENERADAS POR EL AREA DE EMPAQUE - EM
        /// </summary>
        /// <param name="_ID"></param>
        /// <param name="_ClaveTipoProceso"></param>
        /// <param name="_referencia"></param>
        /// <param name="_Cliente"></param>
        private void GeneraProcesoEMLinea(int _ID, String _ClaveTipoProceso, int _referencia, String _Cliente)
        {
            String _observaciones = String.Empty;
            String _tallas = string.Empty;
            String _res = String.Empty;
            int _pedido;
            int _idProceso;
            frmInputBox f;
            switch (_ClaveTipoProceso)
            {
                //*******************************************************************************************/
                //Actividad 11. EMPAQUE                
                #region ACTIVIDAD 11
                case "11EM1":
                    //8.1 UP Pedidos
                    frmUpPedidos frmUpPedidos = new frmUpPedidos(_Cliente, _referencia);
                    frmUpPedidos.ShowDialog();
                    if (frmUpPedidos.procesado)
                    {
                        if ((MessageBox.Show("Desea introducir observaciones adicionales para la siguiente área?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes))
                        {
                            f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto);
                            f.Text = "OBSERVACIONES SIP";
                            f.lblTitulo.Text = "Introduce las observaciones: ";
                            f.ShowDialog();
                            _observaciones = f.txtOrden.Text.Trim();
                            if (_observaciones == "")
                            {
                                MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                break;
                            }
                        }
                        _res = setLineaTiempoPedido(_ID, "CPR", "A", 12, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 3, _referencia, _Cliente);
                        if (_res == "")
                        {
                            MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.reload = true;
                        }
                        else
                            MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case "11EM2":
                    //8.2 Ver pedido
                    String _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    /* QUITAMOS IMPRESION NORMAL
                    SIP.Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido);
                     * */
                    Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.OrdenTrabajo, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    if (frmReportes.pedidoTambienImprimeOT)
                    {
                        frmReportes.Show();
                    }
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    frmReportes.Show();
                    break;
                #endregion
                //*******************************************************************************************/
                //CANCELACION
                #region CANCELACION
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        //ControlPedidos.setCancelaPedido(_referencia);
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                //EXPEDIENTE DIGITAL
                #region EXPEDIENTE DIGITAL
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion
            }
        }

        /// <summary>
        /// AUTORIZACIONES GENERADAS POR EL COORDINADOR DE PROCESOS - CPR
        /// </summary>
        /// <param name="_ID"></param>
        /// <param name="_ClaveTipoProceso"></param>
        /// <param name="_referencia"></param>
        private void GeneraProcesoPL(int _ID, String _ClaveTipoProceso, int _referencia, String _Cliente)
        {
            String _observaciones = String.Empty;
            String _res = String.Empty;
            frmInputBox f;
            switch (_ClaveTipoProceso)
            {
                //*******************************************************************************************/
                //ACTIVIDAD 24. RUTA
                #region ACTIVIDAD 8
                case "24PL1":
                    //8.1 UP Pedidos
                    frmUpPedidos frmUpPedidos = new frmUpPedidos(_Cliente, _referencia);
                    frmUpPedidos.ShowDialog();
                    if (frmUpPedidos.procesado)
                    {
                        ControlPedidos.setFinPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    break;
                case "24PL2":
                    //8.2 Ver pedido
                    String _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    /* QUITAMOS IMPRESION NORMAL
                    SIP.Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido);
                     * */
                    Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.OrdenTrabajo, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    if (frmReportes.pedidoTambienImprimeOT)
                    {
                        frmReportes.Show();
                    }
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    frmReportes.Show();
                    break;
                #endregion
                //*******************************************************************************************/
                //CANCELACION
                #region CANCELACION
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        //ControlPedidos.setCancelaPedido(_referencia);
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                //EXPEDIENTE DIGITAL
                #region EXPEDIENTE DIGITAL
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion
            }
        }
        private void GeneraProcesoPLLinea(int _ID, String _ClaveTipoProceso, int _referencia, String _Cliente)
        {
            String _observaciones = String.Empty;
            String _res = String.Empty;
            frmInputBox f;
            switch (_ClaveTipoProceso)
            {
                //*******************************************************************************************/
                //ACTIVIDAD 8. RUTA
                #region ACTIVIDAD 8
                case "8PL1":
                    //8.1 UP Pedidos
                    frmUpPedidos frmUpPedidos = new frmUpPedidos(_Cliente, _referencia);
                    frmUpPedidos.ShowDialog();
                    if (frmUpPedidos.procesado)
                    {
                        if ((MessageBox.Show("Desea introducir observaciones adicionales para la siguiente área?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes))
                        {
                            f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto);
                            f.Text = "OBSERVACIONES SIP";
                            f.lblTitulo.Text = "Introduce las observaciones: ";
                            f.ShowDialog();
                            _observaciones = f.txtOrden.Text.Trim();
                            if (_observaciones == "")
                            {
                                MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                break;
                            }
                        }
                        _res = setAutorizacionPedido(_referencia, "PL", Globales.UsuarioActual.UsuarioUsuario);
                        _res = setLineaTiempoPedido(_ID, "DG", "A", 13, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 3, _referencia, _Cliente);
                        if (_res == "")
                        {
                            MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.reload = true;
                        }
                        else
                            MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case "8PL2":
                    //8.2 Ver pedido
                    String _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    /* QUITAMOS IMPRESION NORMAL
                    SIP.Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido);
                     * */
                    Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.OrdenTrabajo, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    if (frmReportes.pedidoTambienImprimeOT)
                    {
                        frmReportes.Show();
                    }
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    frmReportes.Show();
                    break;
                case "8PL3":
                    //8.3 Rechazar Pedido
                    f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto, true);
                    f.Text = "OBSERVACIONES SIP";
                    f.lblTitulo.Text = "Introduce las observaciones: ";
                    f.ShowDialog();
                    _observaciones = f.txtOrden.Text.Trim();
                    if (_observaciones == "")
                    {
                        MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
                    _res = setLineaTiempoPedido(_ID, "CP", "R", 1, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 3, _referencia, _Cliente);
                    // CANCELAMOS LA AUTORIZACION PREVIA.
                    _res = setCancelaAutorizacionPedido(_referencia, "PL", Globales.UsuarioActual.UsuarioUsuario, "FA");
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                #endregion
                //*******************************************************************************************/
                //CANCELACION
                #region CANCELACION
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        //ControlPedidos.setCancelaPedido(_referencia);
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                //EXPEDIENTE DIGITAL
                #region EXPEDIENTE DIGITAL
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion
            }
        }

        /// <summary>
        /// AUTORIZACIONES GENERADAS POR EL GERENTE DE OPERACIONES - GO
        /// </summary>
        /// <param name="_ID"></param>
        /// <param name="_ClaveTipoProceso"></param>
        /// <param name="_referencia"></param>
        private void GeneraProcesoCA(int _ID, String _ClaveTipoProceso, int _referencia, String _Cliente)
        {
            String _observaciones = String.Empty;
            String _res = String.Empty;
            frmInputBox f;
            switch (_ClaveTipoProceso)
            {
                #region ACTIVIDAD 22
                case "22CA1":
                    //22.1 Generar programa de produccion
                    frmOrdProduccionMasiva frmOrdProduccionMasiva = new frmOrdProduccionMasiva(_referencia);
                    frmOrdProduccionMasiva.ShowDialog();

                    if (frmOrdProduccionMasiva.StatusProceso)
                    {
                        _res = setLineaTiempoPedido(_ID, "CA", "A", 23, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 2, _referencia, _Cliente);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    break;
                case "22CA2":
                    //22.2 Generar Requisicion de compra
                    //observaciones obligatorias
                    f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto, true);
                    f.Text = "OBSERVACIONES SIP";
                    f.lblTitulo.Text = "Introduce los datos de la requisición: ";
                    f.ShowDialog();
                    _observaciones = f.txtOrden.Text.Trim();
                    if (_observaciones == "")
                    {
                        MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
                    _res = setLineaTiempoPedido(_ID, "CP", "A", 17, _observaciones, Globales.UsuarioActual.UsuarioUsuario, 2, _referencia, _Cliente);
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "22CA3":
                    //22.3 Ver pedido
                    String _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    /*QUITAMOS IMPRESION NORMAL
                    SIP.Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido);
                     * */
                    Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.OrdenTrabajo, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    if (frmReportes.pedidoTambienImprimeOT)
                    {
                        frmReportes.Show();
                    }
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    frmReportes.Show();
                    break;
                case "22CA4":
                    //22.4 Imprimir Pedido
                    // 23.1 IMPRIMIR PEDIDO CON FIRMAS
                    _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, true);
                    frmReportes.enVentas = true;
                    frmReportes.ShowDialog();
                    if (frmReportes.pedidoEstatusModificado)
                    {
                        // CREMAMOS EL REGISTRO EN UPPEDIDOS
                        UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                        uppedidosModif.PEDIDO = _referencia;
                        uppedidosModif.F_IMPRESION = DateTime.Now;
                        uppedidosModif.Modificar(uppedidosModif, "Impresión Firmas - Control Pedidos");
                        _res = setLineaTiempoPedido(_ID, "PL", "A", 24, "", Globales.UsuarioActual.UsuarioUsuario, 2, _referencia, _Cliente);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    break;
                #endregion
                #region ACTIVIDAD 23
                case "23CA1":
                    // 23.1 IMPRIMIR PEDIDO CON FIRMAS
                    _claveCliente = dgvPedidos.CurrentRow.Cells["CLAVE"].Value.ToString();
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, true);
                    frmReportes.enVentas = true;
                    frmReportes.ShowDialog();
                    if (frmReportes.pedidoEstatusModificado)
                    {
                        // CREMAMOS EL REGISTRO EN UPPEDIDOS
                        UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                        uppedidosModif.PEDIDO = _referencia;
                        uppedidosModif.F_IMPRESION = DateTime.Now;
                        uppedidosModif.Modificar(uppedidosModif, "Impresión Firmas - Control Pedidos");
                        _res = setLineaTiempoPedido(_ID, "PL", "A", 24, "", Globales.UsuarioActual.UsuarioUsuario, 2, _referencia, _Cliente);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                //*******************************************************************************************/
                //CANCELACION
                #region CANCELACION
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        //ControlPedidos.setCancelaPedido(_referencia);
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                //EXPEDIENTE DIGITAL
                #region EXPEDIENTE DIGITAL
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion
                // ELIMINAR SOLICITUD
                #region ELIMINAR SOLICITUD
                case "ELIMINARSOLICITUD":
                    if (MessageBox.Show("¿Seguro que desea eliminar la solicitud seleccionada?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        SOLICITUDES_ESPECIALES.EliminaSolicitud(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    break;
                #endregion
            }
        }


        private List<int> getSolicitudesSeleccionadas(ref String _error)
        {
            List<String> Cliente = new List<string> { };
            List<int> Solicitudes = new List<int> { };

            foreach (DataGridViewRow _dr in dgvPedidos.Rows)
            {
                if (_dr.Cells["Seleccion"].Value.ToString() == "1")
                {
                    if (_dr.Cells["OrdenAgrupador"].Value.ToString() != "9")
                    {
                        Solicitudes.Clear();
                        _error = "No se pueden seleccionar solicitudes en diferente proceso. (PEDIDO SIP)";
                        break;
                    }
                    Solicitudes.Add(int.Parse(_dr.Cells["Referencia"].Value.ToString()));
                    if (!Cliente.Contains(_dr.Cells["CLAVE"].Value.ToString().Trim()))
                        Cliente.Add(_dr.Cells["CLAVE"].Value.ToString().Trim());
                }
            }

            if (Cliente.Count > 1)
            {
                Solicitudes.Clear();
                _error = "Solo se pueden seleccionar solicitudes de un solo cliente";
            }

            if (Solicitudes.Count == 0)
                _error = "Se debe seleccionar al menos una solicitud.";

            return Solicitudes;
        }
        private void SetTimmer()
        {
            /*
            TimerNotice = new System.Threading.ManualResetEvent(false);
            formsTimer = new System.Windows.Forms.Timer();
            formsTimer.Interval = 10000;
            formsTimer.Tick += new EventHandler(SetDataTimmer);
            formsTimer.Start();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);
            timer.Start();
             * */
        }
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (this.AreaSeleccionada != "")
            {
                this.AreaConsulta = this.AreaSeleccionada;
            }
            else
            {
                this.AreaConsulta = Globales.UsuarioActual.UsuarioArea == "TD" ? "GV" : Globales.UsuarioActual.UsuarioArea;
            }
            this.dtPedidosPorArea = getPedidosPorArea(this.AreaConsulta, Globales.UsuarioActual.UsuarioUsuario);
        }

        private void SetDataTimmer(object sender, EventArgs e)
        {
            dgvPedidos.DataSource = dtPedidosPorArea;
            MarcaGridAlertas();
            MarcaGridHabilitados();
            if (this.AreaSeleccionada == "EV")
            {
                dgvPedidos.Columns["Seleccion"].Visible = true;
                dgvPedidos.Columns["Seleccion"].ReadOnly = false;
            }
            else
            {
                dgvPedidos.Columns["Seleccion"].Visible = false;
            }
            getProcesosGrid();
            getClientesGrid();
            getTiposPedidoGrid();
            this.treeViewProcesos.SelectedNode = this.treeViewProcesos.Nodes[0].Nodes[this.AreaConsulta];
        }

        #endregion
    }
}