using SIP.Utiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ulp_bl;


namespace SIP
{
    public partial class frmControlPedidos2 : Form
    {
        #region ATRIBUTOS Y CONSTRUCTORES

        DataTable dtResult;
        DataTable dtFlujo;
        DataTable dtAreas;
        Exception ex;

        String AreaConsulta = "";
        String AreaSeleccionada = "";
        Boolean reload = false;
        List<String> AreasEspeciales = new List<string> { "DG", "TD" };
        List<ControlFlujo.TiposPrceso> ProcesosConExpediente = new List<ControlFlujo.TiposPrceso> { ControlFlujo.TiposPrceso.PedidoEspecial, ControlFlujo.TiposPrceso.PedidoLinea, ControlFlujo.TiposPrceso.PedidoMostrador, ControlFlujo.TiposPrceso.PedidoDAT };
        string claveTipoProcesoSelected = "";
        string claveAreaSelected = "";

        String _res = String.Empty;
        frmInputBox f;
        String _observaciones = String.Empty;

        private Timer notificationTimer;

        public frmControlPedidos2()
        {
            InitializeComponent();
            this.dtResult = new DataTable();
            this.dtFlujo = new DataTable();
            this.dtAreas = new DataTable();
            this.CargaPermisosEspeciales();
        }
        #endregion
        #region EVENTOS
        private void frmControlPedidos2_Load(object sender, EventArgs e)
        {
            this.dtFlujo = this.GetFlujo(Globales.UsuarioActual.UsuarioUsuario);
            // CREAMOS ARBOL DE PROCESOS
            this.GenerateTreeNode(this.dtFlujo);
            statusStripLblArea.Text = "Área: " + Globales.UsuarioActual.UsuarioArea;
            statusStripLblUsuario.Text = "Usuario: " + Globales.UsuarioActual.UsuarioUsuario;
            statusStripLblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            this.notificationTimer_Tick(null, null);
            this.initNotificationTimer();
        }
        private void treeViewProcesos_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode nodeSelected = e.Node;
            if (nodeSelected.Parent != null)
            {
                this.claveTipoProcesoSelected = nodeSelected.Parent.Name;
                this.claveAreaSelected = nodeSelected.Name;
                this.ClearGrid();
                // Obtenemos los registros
                this.dtResult = GetResults(this.claveTipoProcesoSelected, this.claveAreaSelected, Globales.UsuarioActual.UsuarioUsuario);
                this.dgvResult.DataSource = this.dtResult;
                this.SetColumnsByProceso((ControlFlujo.TiposPrceso)int.Parse(this.claveTipoProcesoSelected));
                this.MarcaGridAlertas();
            }
        }
        private void dgvResult_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                try
                {
                    this.dgvResult.EndEdit();
                    ContextMenuStrip menu = new ContextMenuStrip();
                    int position_click = this.dgvResult.HitTest(e.X, e.Y).RowIndex;
                    if (position_click >= 0)
                    {
                        String _area = String.Empty;
                        int _ordenAgrupador;
                        int _tipoProceso;
                        //SELECCIONAMOS EL TIPO DE AREA EN LA QUE NOS ENCONTRAMOS Y GENERAMOS LOS MENUS CORRESPONDIENTES 
                        //CONSIDERACION 1: AREA
                        //CONSIDERACION 2: ORDEN AGRUPADOR
                        _area = this.claveAreaSelected;
                        _ordenAgrupador = (int)this.dgvResult.CurrentRow.Cells["OrdenAgrupador"].Value;
                        _tipoProceso = int.Parse(this.claveTipoProcesoSelected);

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
                        if (this.dgvResult.CurrentRow.Cells["Estatus"].Value.ToString().ToUpper() == "RECHAZADO")
                        {
                            if (Globales.UsuarioActual.UsuarioArea != "CN")
                            {
                                menu.Items.Add("Cancelar").Name = "CANCELAR";
                            }
                        }
                        // SI EL REGISTRO SE ENCUENTRA DENTRO DE LOS PROCESO QUE TIENEN EXPEDIENTE DIGITAL
                        if (this.ProcesosConExpediente.Contains((ControlFlujo.TiposPrceso)int.Parse(this.claveTipoProcesoSelected)))
                        {
                            if (Globales.UsuarioActual.UsuarioArea != "CN")
                            {
                                menu.Items.Add("Expediente Digital").Name = "EXPEDIENTEDIGITAL";
                            }

                        }
                        // SI EL REGISTRO SE TRATA DE UNA SOLICITUD Y ES TD AGREGAMOS LA OPCION DE 'Eliminar Solicitud' 
                        if (int.Parse(this.claveTipoProcesoSelected) == (int)ControlFlujo.TiposPrceso.SolicitudEspecial)
                        {
                            if (Globales.UsuarioActual.UsuarioArea == "TD")
                            {
                                menu.Items.Add("Eliminar Solicitud").Name = "ELIMINARSOLICITUD";
                            }
                        }
                    }
                    menu.Show(this.dgvResult, new Point(e.X, e.Y));
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
            this.dgvResult.ClearSelection();
        }
        private void dgvResult_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvHistorico.Columns.Clear();
            dgvHistorico.Rows.Clear();
            //OBTENEMOS EL HISTORICO DEL PEDIDO
            int id = (int)dgvResult.CurrentRow.Cells["Id"].Value;
            DataTable dtHistoricoPedido = new DataTable();
            dtHistoricoPedido = getHistorico(id);
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
        private void dgvResult_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            lstObservaciones.Items.Clear();
            if (this.dgvResult.Columns[e.ColumnIndex].Name.ToUpper() == "OBSERVACIONES")
            {
                if (this.dgvResult[e.ColumnIndex, e.RowIndex].Value.ToString() != "")
                {
                    DataTable dt = new DataTable();
                    dt = getObservaciones((int)dgvResult.CurrentRow.Cells["Id"].Value);
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
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (this.claveAreaSelected != "" && this.claveTipoProcesoSelected != "")
            {
                this.Cursor = Cursors.WaitCursor;
                // RECONSTRUIMOS MENU Y CONTADOR
                this.dtFlujo = this.GetFlujo(Globales.UsuarioActual.UsuarioUsuario);
                // CREAMOS ARBOL DE PROCESOS
                this.GenerateTreeNode(this.dtFlujo);
                // CONSULTAMOS DATA
                this.dtResult = GetResults(this.claveTipoProcesoSelected, this.claveAreaSelected, Globales.UsuarioActual.UsuarioUsuario);
                this.dgvResult.DataSource = this.dtResult;
                this.SetColumnsByProceso((ControlFlujo.TiposPrceso)int.Parse(this.claveTipoProcesoSelected));
                this.MarcaGridAlertas();
                this.treeViewProcesos.SelectedNode = this.treeViewProcesos.Nodes[String.Format("{0}", this.claveTipoProcesoSelected)].Nodes[this.claveAreaSelected];
                this.Cursor = Cursors.Default;
            }
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
        private void listNotificaciones_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked)
            {
                int _id = int.Parse(listNotificaciones.Items[e.Item.Index].Text);
                FlujoControlNotificaciones.MarcaNotificacionLeida(_id);
                listNotificaciones.Items.Remove(e.Item);
            }
        }
        private void btnImprimirNotificaciones_Click(object sender, EventArgs e)
        {
            Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.NotificacionesSIVO, Globales.UsuarioActual.UsuarioArea);
            frmReportes.ShowDialog();
        }
        void menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            String _ClaveArea, _Estatus, _Observaciones, _Usuario, _Resultado, _ClaveMenu, _ClaveAreaSiguiente;
            this._observaciones = _ClaveArea = _ClaveAreaSiguiente = _ClaveMenu = _Estatus = _Observaciones = _Usuario = _Resultado = String.Empty;
            Boolean permisoEspecial = false;

            int _id, _referenciaProceso;

            _Usuario = Globales.UsuarioActual.UsuarioUsuario;
            _id = (int)this.dgvResult.CurrentRow.Cells["ID"].Value;
            _referenciaProceso = (int)this.dgvResult.CurrentRow.Cells["Referencia"].Value;
            _ClaveMenu = e.ClickedItem.Name;
            _ClaveAreaSiguiente = ControlFlujo.GetSiguienteAreaFlujo(_ClaveMenu);
            _ClaveArea = treeViewProcesos.SelectedNode.Name;

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
                switch (int.Parse(this.claveTipoProcesoSelected))
                {
                    //*******************************************************************************************/
                    //FLUJO 1. SOLICITUD DE CODIGOS ESPECIALES
                    //*******************************************************************************************/
                    case (int)ControlFlujo.TiposPrceso.SolicitudEspecial:
                        switch (this.claveAreaSelected)
                        {
                            case "EV":
                                this.Proceso1_EV(_id, _ClaveMenu, _referenciaProceso, _ClaveAreaSiguiente);
                                break;
                            case "GV":
                                this.Proceso1_GV(_id, _ClaveMenu, _referenciaProceso, _ClaveAreaSiguiente);
                                break;
                            case "DG":
                                this.Proceso1_DG(_id, _ClaveMenu, _referenciaProceso, _ClaveAreaSiguiente);
                                break;
                            case "GO":
                                this.Proceso1_GO(_id, _ClaveMenu, _referenciaProceso, _ClaveAreaSiguiente);
                                break;
                        }
                        break;
                    //*******************************************************************************************/
                    //FLUJO 2. PEDIDOS ESPECIALES
                    //*******************************************************************************************/
                    case (int)ControlFlujo.TiposPrceso.PedidoEspecial:
                        switch (this.claveAreaSelected)
                        {
                            case "EV":
                                this.Proceso2_EV(_id, _ClaveMenu, _referenciaProceso, _ClaveAreaSiguiente);
                                break;
                            case "CA":
                                this.Proceso2_CA(_id, _ClaveMenu, _referenciaProceso, _ClaveAreaSiguiente);
                                break;
                            case "GV":
                                this.Proceso2_GV(_id, _ClaveMenu, _referenciaProceso, _ClaveAreaSiguiente);
                                break;
                            case "DG":
                                this.Proceso2_DG(_id, _ClaveMenu, _referenciaProceso, _ClaveAreaSiguiente);
                                break;
                            case "CP":
                                this.Proceso2_CP(_id, _ClaveMenu, _referenciaProceso, _ClaveAreaSiguiente);
                                break;
                            case "CR":
                                this.Proceso2_CR(_id, _ClaveMenu, _referenciaProceso, _ClaveAreaSiguiente);
                                break;
                            case "PL":
                                this.Proceso2_PL(_id, _ClaveMenu, _referenciaProceso, _ClaveAreaSiguiente);
                                break;
                        }
                        break;
                    //*******************************************************************************************/
                    //FLUJO 3. PEDIDOS DE LINEA
                    //*******************************************************************************************/
                    case (int)ControlFlujo.TiposPrceso.PedidoLinea:
                        switch (this.claveAreaSelected)
                        {
                            case "EV":
                                this.Proceso3_EV(_id, _ClaveMenu, _referenciaProceso, _ClaveAreaSiguiente);
                                break;
                            case "GV":
                                this.Proceso3_GV(_id, _ClaveMenu, _referenciaProceso, _ClaveAreaSiguiente);
                                break;
                            case "CP":
                                this.Proceso3_CP(_id, _ClaveMenu, _referenciaProceso, _ClaveAreaSiguiente);
                                break;
                            case "CR":
                                this.Proceso3_CR(_id, _ClaveMenu, _referenciaProceso, _ClaveAreaSiguiente);
                                break;
                            case "PL":
                                this.Proceso3_PL(_id, _ClaveMenu, _referenciaProceso, _ClaveAreaSiguiente);
                                break;
                            case "DG":
                                this.Proceso3_DG(_id, _ClaveMenu, _referenciaProceso, _ClaveAreaSiguiente);
                                break;
                            case "SU":
                                this.Proceso3_SU(_id, _ClaveMenu, _referenciaProceso, _ClaveAreaSiguiente);
                                break;
                            case "CA":
                                this.Proceso3_CA(_id, _ClaveMenu, _referenciaProceso, _ClaveAreaSiguiente);
                                break;
                        }
                        break;
                    //*******************************************************************************************/
                    //FLUJO 4. ORDENES DE PRODUCCION
                    //*******************************************************************************************/
                    case (int)ControlFlujo.TiposPrceso.OrdenesProduccion:
                        switch (this.claveAreaSelected)
                        {
                            case "GO":
                                this.Proceso4_GO(_id, _ClaveMenu, _referenciaProceso, _ClaveAreaSiguiente);
                                break;
                            case "GV":
                                this.Proceso4_GV(_id, _ClaveMenu, _referenciaProceso, _ClaveAreaSiguiente);
                                break;
                            case "CA":
                                this.Proceso4_CA(_id, _ClaveMenu, _referenciaProceso, _ClaveAreaSiguiente);
                                break;
                            case "DG":
                                this.Proceso4_DG(_id, _ClaveMenu, _referenciaProceso, _ClaveAreaSiguiente);
                                break;
                            case "CPR":
                                this.Proceso4_CPR(_id, _ClaveMenu, _referenciaProceso, _ClaveAreaSiguiente);
                                break;
                        }
                        break;
                    //*******************************************************************************************/
                    //FLUJO 5. ORDENES DE PRODUCCION DE FALTANTE
                    //*******************************************************************************************/
                    case (int)ControlFlujo.TiposPrceso.OrdenesProduccionFaltante:
                        switch (this.claveAreaSelected)
                        {
                            case "CA":
                                this.Proceso5_CA(_id, _ClaveMenu, _referenciaProceso, _ClaveAreaSiguiente);
                                break;
                            case "GV":
                                this.Proceso5_GV(_id, _ClaveMenu, _referenciaProceso, _ClaveAreaSiguiente);
                                break;
                            case "GO":
                                this.Proceso5_GO(_id, _ClaveMenu, _referenciaProceso, _ClaveAreaSiguiente);
                                break;
                            case "CPR":
                                this.Proceso5_CPR(_id, _ClaveMenu, _referenciaProceso, _ClaveAreaSiguiente);
                                break;
                        }
                        break;
                    //*******************************************************************************************/
                    //FLUJO 6. PEDIDOS DAT
                    //*******************************************************************************************/
                    case (int)ControlFlujo.TiposPrceso.PedidoDAT:
                        switch (this.claveAreaSelected)
                        {
                            case "EV":
                                this.Proceso6_EV(_id, _ClaveMenu, _referenciaProceso, _ClaveAreaSiguiente);
                                break;
                            case "GV":
                                this.Proceso6_GV(_id, _ClaveMenu, _referenciaProceso, _ClaveAreaSiguiente);
                                break;
                            case "CP":
                                this.Proceso6_CP(_id, _ClaveMenu, _referenciaProceso, _ClaveAreaSiguiente);
                                break;
                            case "CR":
                                this.Proceso6_CR(_id, _ClaveMenu, _referenciaProceso, _ClaveAreaSiguiente);
                                break;
                            case "SU":
                                this.Proceso6_SU(_id, _ClaveMenu, _referenciaProceso, _ClaveAreaSiguiente);
                                break;
                            case "CM":
                                this.Proceso6_CM(_id, _ClaveMenu, _referenciaProceso, _ClaveAreaSiguiente);
                                break;
                        }
                        break;
                    //*******************************************************************************************/
                    //FLUJO 7. PEDIDOS MOS
                    //*******************************************************************************************/
                    case (int)ControlFlujo.TiposPrceso.PedidoMostrador:
                        switch (this.claveAreaSelected)
                        {
                            case "EV":
                                this.Proceso7_EV(_id, _ClaveMenu, _referenciaProceso, _ClaveAreaSiguiente);
                                break;
                            case "GV":
                                this.Proceso7_GV(_id, _ClaveMenu, _referenciaProceso, _ClaveAreaSiguiente);
                                break;
                            case "CR":
                                this.Proceso7_CR(_id, _ClaveMenu, _referenciaProceso, _ClaveAreaSiguiente);
                                break;
                            case "CM":
                                this.Proceso7_CM(_id, _ClaveMenu, _referenciaProceso, _ClaveAreaSiguiente);
                                break;
                            case "PL":
                                this.Proceso7_PL(_id, _ClaveMenu, _referenciaProceso, _ClaveAreaSiguiente);
                                break;
                        }
                        break;
                    //*******************************************************************************************/
                    //FLUJO 7. PEDIDOS MOS
                    //*******************************************************************************************/
                    case (int)ControlFlujo.TiposPrceso.RequisicionMostrador:
                        switch (this.claveAreaSelected)
                        {
                            case "CM":
                                break;
                            case "SU":
                                this.Proceso8_SU(_id, _ClaveMenu, _referenciaProceso, _ClaveAreaSiguiente);
                                break;
                            case "CA":
                                this.Proceso8_CA(_id, _ClaveMenu, _referenciaProceso, _ClaveAreaSiguiente);
                                break;
                        }
                        break;
                }
            }
            if (reload)
            {
                this.Cursor = Cursors.WaitCursor;
                // RECONSTRUIMOS MENU Y CONTADOR
                this.dtFlujo = this.GetFlujo(Globales.UsuarioActual.UsuarioUsuario);
                // CREAMOS ARBOL DE PROCESOS
                this.GenerateTreeNode(this.dtFlujo);
                // CONSULTAMOS DATA                
                this.dtResult = this.GetResults(this.claveTipoProcesoSelected, this.claveAreaSelected, Globales.UsuarioActual.UsuarioUsuario);
                this.dgvResult.DataSource = this.dtResult;
                this.SetColumnsByProceso((ControlFlujo.TiposPrceso)int.Parse(this.claveTipoProcesoSelected));
                this.MarcaGridAlertas();
                this.treeViewProcesos.SelectedNode = this.treeViewProcesos.Nodes[String.Format("{0}", this.claveTipoProcesoSelected)].Nodes[this.claveAreaSelected];
                this.Cursor = Cursors.Default;
            }

        }
        #endregion
        #region METODOS
        private DataTable GetFlujo(String _usuario)
        {
            DataTable dt = new DataTable();
            dt = ControlFlujo.GetFlujo(_usuario);
            return dt;
        }
        private void CargaPermisosEspeciales()
        {
            if (this.AreasEspeciales.Contains(Globales.UsuarioActual.UsuarioArea))
            {
                btnReporte.Visible = true;
            }
        }
        private void GenerateTreeNode(DataTable dt)
        {
            treeViewProcesos.Nodes.Clear();
            if (dt.Rows.Count > 0)
            {
                // Agrupamos por tipo de proceso
                DataTable dtTipoProceso = dt.DefaultView.ToTable(true, "ClaveTipoProceso", "DescripcionProceso");
                foreach (DataRow dr in dtTipoProceso.Rows)
                {
                    // Agregamos nodo principal de tipo de proceso
                    String rootTotal = dt.Select(String.Format("ClaveTipoProceso = {0}", dr["ClaveTipoProceso"].ToString())).AsEnumerable().Sum(x => x.Field<int>("Total")).ToString();
                    TreeNode rootNode = new TreeNode(String.Format("{0} - ({1})", dr["DescripcionProceso"].ToString(), rootTotal.ToString()));
                    rootNode.Name = dt.Select(String.Format("DescripcionProceso = '{0}'", dr["DescripcionProceso"].ToString())).FirstOrDefault().Field<int>("ClaveTipoProceso").ToString();
                    this.treeViewProcesos.Nodes.Add(rootNode);
                    // Agregamos áreas
                    DataTable dtAreas = new DataTable();
                    if (Globales.UsuarioActual.UsuarioArea == "TD")
                    {
                        dtAreas = dt.Select(String.Format("DescripcionProceso = '{0}'", dr["DescripcionProceso"].ToString())).CopyToDataTable();
                    }
                    else
                    {
                        dtAreas = dt.Select(String.Format("DescripcionProceso = '{0}' AND ClaveArea = '{1}'", dr["DescripcionProceso"].ToString(), Globales.UsuarioActual.UsuarioArea)).CopyToDataTable();
                    }
                    foreach (DataRow row in dtAreas.Rows)
                    {
                        TreeNode NewNode = new TreeNode(String.Format("{0} - ({1})", row["DescripcionArea"].ToString(), row["Total"].ToString()));
                        NewNode.Name = row["ClaveArea"].ToString();
                        rootNode.Nodes.Add(NewNode);
                    }
                }
            }
        }
        private void ClearGrid()
        {
            dgvHistorico.Columns.Clear();
            dgvHistorico.Rows.Clear();
        }
        private DataTable GetResults(String _ClaveTipoProceso, String _ClaveArea, String _usuario)
        {
            DataTable dtResults = new DataTable();
            dtResult = ControlFlujo.GetResults(_ClaveTipoProceso, _ClaveArea, _usuario);
            return dtResult;
        }
        private DataTable getListaMenus(String _ClaveArea, int _OrdenAgrupador, int _tipoProceso)
        {
            DataTable dt = new DataTable();
            dt = ControlFlujo.GetListaMenus(_ClaveArea, _OrdenAgrupador, _tipoProceso);
            return dt;
        }
        private void SetColumnsByProceso(ControlFlujo.TiposPrceso tipoProceso)
        {
            // COLUMNAS COMUNES
            foreach (DataGridViewColumn column in this.dgvResult.Columns)
            {
                column.ReadOnly = true;
                if (column.Name == "Observaciones")
                {
                    DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
                    cellStyle.ForeColor = Color.Blue;
                    cellStyle.SelectionForeColor = Color.White;
                    cellStyle.Font = new Font(FontFamily.GenericSansSerif, 8, FontStyle.Underline | FontStyle.Bold);

                    column.DefaultCellStyle = cellStyle;
                }
            }
            // COLUMNAS VISIBLES POR TIPO DE PROCESO
            List<String> ListaColumnasVisibles = new List<string> { };
            switch (int.Parse(this.claveTipoProcesoSelected))
            {
                case (int)ControlFlujo.TiposPrceso.SolicitudEspecial:
                    ListaColumnasVisibles = ControlFlujo.Flujo1_Columnas;
                    break;
                case (int)ControlFlujo.TiposPrceso.PedidoEspecial:
                    ListaColumnasVisibles = ControlFlujo.Flujo2_Columnas;
                    break;
                case (int)ControlFlujo.TiposPrceso.PedidoLinea:
                    ListaColumnasVisibles = ControlFlujo.Flujo3_Columnas;
                    break;
                case (int)ControlFlujo.TiposPrceso.OrdenesProduccion:
                    ListaColumnasVisibles = ControlFlujo.Flujo4_Columnas;
                    break;
                case (int)ControlFlujo.TiposPrceso.OrdenesProduccionFaltante:
                    ListaColumnasVisibles = ControlFlujo.Flujo5_Columnas;
                    break;
                case (int)ControlFlujo.TiposPrceso.PedidoDAT:
                    ListaColumnasVisibles = ControlFlujo.Flujo6_Columnas;
                    break;
                case (int)ControlFlujo.TiposPrceso.PedidoMostrador:
                    ListaColumnasVisibles = ControlFlujo.Flujo7_Columnas;
                    break;
                case (int)ControlFlujo.TiposPrceso.RequisicionMostrador:
                    ListaColumnasVisibles = ControlFlujo.Flujo8_Columnas;
                    break;
            }
            foreach (DataGridViewColumn column in this.dgvResult.Columns)
            {
                if (column.Name == "Seleccion")
                {
                    column.Visible = this.claveAreaSelected == "EV" ? true : false;
                    column.ReadOnly = this.claveAreaSelected == "EV" ? false : true;
                }
                else
                {
                    column.Visible = ListaColumnasVisibles.Contains(column.Name) ? true : false;
                }

            }
            this.dgvResult.AutoResizeColumns();
            this.dgvResult.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
        private void MarcaGridAlertas()
        {
            this.dgvResult.ClearSelection();
            foreach (DataGridViewRow dr in dgvResult.Rows)
            {
                if ((bool)dr.Cells["Marcar"].Value)
                    dr.DefaultCellStyle.ForeColor = Color.Red;
            }
        }
        private void MarcaGridHabilitados()
        {
            dgvResult.ClearSelection();
            foreach (DataGridViewRow dr in dgvResult.Rows)
            {
                if ((bool)dr.Cells["MarcarHabilitado"].Value)
                    dr.DefaultCellStyle.ForeColor = Color.Blue;
            }
        }
        private void initNotificationTimer()
        {
            this.notificationTimer = new Timer();
            this.notificationTimer.Tick += new EventHandler(notificationTimer_Tick);
            this.notificationTimer.Interval = 60000;
            this.notificationTimer.Start();
        }
        private void notificationTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                List<FlujoControlNotificaciones> notificaciones = FlujoControlNotificaciones.GetNotificacionesByArea(Globales.UsuarioActual.UsuarioArea, FlujoControlNotificaciones.Estatus.PENDIENTES);
                listNotificaciones.Items.Clear();
                foreach (FlujoControlNotificaciones _notificacion in notificaciones)
                {
                    ListViewItem _item = new ListViewItem(_notificacion.Id.ToString());
                    _item.SubItems.Add(_notificacion.Modulo);
                    _item.SubItems.Add(_notificacion.Notificacion);
                    listNotificaciones.Items.Add(_item);
                }
                listNotificaciones.AutoResizeColumns(listNotificaciones.Items.Count > 0 ? ColumnHeaderAutoResizeStyle.ColumnContent : ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            catch { }
        }
        #endregion
        #region ACCIONES PARA PROCESO 1 - SOLICITUDES ESPECIALES
        private void Proceso1_EV(int _ID, String _ClaveMenu, int _referencia, string _ClaveAreaSiguiente)
        {
            String _claveCliente = this.dgvResult.CurrentRow.Cells["Clave"].Value.ToString();
            int _pedido;
            int _idProceso;
            switch (_ClaveMenu)
            {
                //*******************************************************************************************/
                //ACTIVIDAD 1. GENERACIÓN DE SOLICITUD
                //*******************************************************************************************/
                #region ACTIVIDAD 1. GENERACIÓN DE SOLICITUD
                case "1_1_EV_1": // Buscador de Cliente
                    _claveCliente = this.dgvResult.CurrentRow.Cells["Clave"].Value.ToString();
                    frmFindClie frmFindClie = new frmFindClie(_claveCliente);
                    frmFindClie.Show();
                    break;
                case "1_1_EV_2": // Enviar Solicitud de Código
                    if (this.dgvResult.CurrentRow.Cells["EstatusSolicitud"].Value.ToString() != "A")
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
                    // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.SolicitudEspecial, _referencia, _claveCliente);
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
                //ACTIVIDAD 7. SOLICITUD DE TALLAS
                //*******************************************************************************************/
                #region ACTIVIDAD 7. SOLICITUD DE TALLAS
                case "7_1_EV_1": // Solicitud de tallas
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
                    // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.SolicitudEspecial, _referencia, _claveCliente);
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "7_1_EV_2": // Ver Datos Solicitud
                    frmVisorSolicitudesEspeciales frmVisorSolicitudesEspeciales = new frmVisorSolicitudesEspeciales(_referencia);
                    frmVisorSolicitudesEspeciales.Show();
                    break;
                #endregion
                //*******************************************************************************************/
                //ACTIVIDAD 9. CREACION DE PEDIDO
                //*******************************************************************************************/
                #region ACTIVIDAD 9. CREACION DE PEDIDO
                case "9_1_EV_1": // Crear pedido
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
                            _claveCliente = this.dgvResult.CurrentRow.Cells["Clave"].Value.ToString();
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
                case "9_1_EV_2": // Ver Datos Solicitud
                    frmVisorSolicitudesEspeciales = new frmVisorSolicitudesEspeciales(_referencia);
                    frmVisorSolicitudesEspeciales.Show();
                    break;
                case "9_1_EV_3": // Asignar numero de Pedido
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
                        this.setAltaLineaTiempo(_idProceso, "EV", "A", 1, "", Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.SolicitudEspecial, _referencia, _claveCliente);
                        this.setLineaTiempo(_idProceso, "GV", "A", 2, "", Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.SolicitudEspecial, _referencia, _claveCliente);
                        ControlPedidos.GeneraNotificacionPedidoExistencias(_referencia);

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
                case "9_1_EV_4": // Reasignar Cliente
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
                //*******************************************************************************************/
                //ACCIONES GENERALES
                //*******************************************************************************************/
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
        private void Proceso1_GV(int _ID, String _ClaveMenu, int _referencia, string _ClaveAreaSiguiente)
        {
            String _claveCliente = this.dgvResult.CurrentRow.Cells["Clave"].Value.ToString();
            decimal _precio = 0;
            switch (_ClaveMenu)
            {
                //*******************************************************************************************/
                //ACTIVIDAD 2. AUTORIZACIÓN DE TELA
                //*******************************************************************************************/
                #region ACTIVIDAD 2. AUTORIZACIÓN DE TELA
                case "2_1_GV_1": // Autorizar
                    // validamos que la solicitud no siga rechazada
                    if (this.dgvResult.CurrentRow.Cells["EstatusSolicitud"].Value.ToString() != "A")
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
                    // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.SolicitudEspecial, _referencia, _claveCliente);
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    break;
                case "2_1_GV_2": // Enviar autorización a Direccion                    
                    // validamos que la solicitud no siga rechazada
                    if (this.dgvResult.CurrentRow.Cells["EstatusSolicitud"].Value.ToString() != "A")
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
                    // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.SolicitudEspecial, _referencia, _claveCliente);
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "2_1_GV_3": // Ver Datos de Solicitud
                    frmVisorSolicitudesEspeciales frmVisor = new frmVisorSolicitudesEspeciales(_referencia);
                    frmVisor.Show();
                    break;
                case "2_1_GV_4": // Reporte de Articulos por Cliente
                    String _criterioDeBusqueda = this.dgvResult.CurrentRow.Cells["Cliente"].Value.ToString();
                    frmReporteArticulosCliente frmReporteArticulosCliente = new frmReporteArticulosCliente(_criterioDeBusqueda);
                    frmReporteArticulosCliente.Show();
                    break;
                case "2_1_GV_5": // Rechazar
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
                    // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "R", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.SolicitudEspecial, _referencia, _claveCliente);
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
                //ACTIVIDAD 5. AUTORIZACIÓN DE TELA
                //*******************************************************************************************/
                #region ACTIVIDAD 5. AUTORIZACIÓN DE TELA
                case "5_1_GV_1": // Asignar Precio
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
                    // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.SolicitudEspecial, _referencia, _claveCliente);
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    break;
                case "5_1_GV_2": // Envíar a Direccion
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
                    // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.SolicitudEspecial, _referencia, _claveCliente);
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    break;
                case "5_1_GV_3": // Ver Datos de Solicitud
                    frmVisor = new frmVisorSolicitudesEspeciales(_referencia);
                    frmVisor.Show();
                    break;
                #endregion
                //*******************************************************************************************/
                //ACTIVIDAD 8. REVISIÓN DE PRECIO
                //*******************************************************************************************/
                #region ACTIVIDAD 8. REVISIÓN DE PRECIO
                case "8_1_GV_1": // Ver Datos Solicitud
                    frmVisor = new frmVisorSolicitudesEspeciales(_referencia);
                    frmVisor.Show();
                    break;
                case "8_1_GV_2": // Enviar a Ejecutivo
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
                    // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.SolicitudEspecial, _referencia, _claveCliente);
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
                //ACCIONES GENERALES
                //*******************************************************************************************/
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
        private void Proceso1_DG(int _ID, String _ClaveMenu, int _referencia, string _ClaveAreaSiguiente)
        {
            String _claveCliente = this.dgvResult.CurrentRow.Cells["Clave"].Value.ToString();
            Decimal _precio = 0;
            switch (_ClaveMenu)
            {
                //*******************************************************************************************/
                //ACTIVIDAD 3. AUTORIZACIÓN DE TELA
                //*******************************************************************************************/
                #region ACTIVIDAD 3. AUTORIZACIÓN DE TELA
                case "3_1_DG_1": // Autorizar
                    // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.SolicitudEspecial, _referencia, _claveCliente);
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    break;
                case "3_1_DG_2": // Reporte de Articulos por Cliente
                    String _criterioDeBusqueda = this.dgvResult.CurrentRow.Cells["Cliente"].Value.ToString();
                    frmReporteArticulosCliente frmReporteArticulosCliente = new frmReporteArticulosCliente(_criterioDeBusqueda);
                    frmReporteArticulosCliente.Show();
                    break;
                case "3_1_DG_3": // Ver Datos de Solicitud
                    frmVisorSolicitudesEspeciales frmVisorSolicitudesEspeciales = new frmVisorSolicitudesEspeciales(_referencia);
                    frmVisorSolicitudesEspeciales.Show();
                    break;
                case "3_1_DG_4": // Rechazar
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
                    // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "R", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.SolicitudEspecial, _referencia, _claveCliente);
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
                //ACTIVIDAD 6. ESTABLECER PRECIO
                //*******************************************************************************************/
                #region ACTIVIDAD 6. ESTABLECER PRECIO
                case "6_1_DG_1": // Asignar Precio
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

                    // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.SolicitudEspecial, _referencia, _claveCliente);
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "6_1_DG_2": // Simulador de Costos
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
                        // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                        _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.SolicitudEspecial, _referencia, _claveCliente);
                        if (_res == "")
                        {
                            MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.reload = true;
                        }
                        else
                            MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;
                case "6_1_DG_3": // Rechazar
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
                    // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "R", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.SolicitudEspecial, _referencia, _claveCliente);
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "6_1_DG_4": // Ver Datos Solicitud
                    frmVisorSolicitudesEspeciales = new frmVisorSolicitudesEspeciales(_referencia);
                    frmVisorSolicitudesEspeciales.Show();
                    break;
                #endregion
                //*******************************************************************************************/
                //*******************************************************************************************/
                //ACCIONES GENERALES
                //*******************************************************************************************/
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
        private void Proceso1_GO(int _ID, String _ClaveMenu, int _referencia, string _ClaveAreaSiguiente)
        {
            String _claveCliente = this.dgvResult.CurrentRow.Cells["Clave"].Value.ToString();
            switch (_ClaveMenu)
            {
                //*******************************************************************************************/
                //ACTIVIDAD 4. CODIGO Y PLAZO
                //*******************************************************************************************/
                #region ACTIVIDAD 4. CODIGO Y PLAZO
                case "4_1_GO_1": // Ingresar Plazo y Código Especial
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
                    // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.SolicitudEspecial, _referencia, _claveCliente);
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "4_1_GO_2": // Duplicador de Códigos
                    frmDupCodProdEstr frmDupCodProdEstr = new frmDupCodProdEstr();
                    frmDupCodProdEstr.Show();
                    break;
                case "4_1_GO_3": // Reporte de Artículos por Cliente
                    String _criterioDeBusqueda = this.dgvResult.CurrentRow.Cells["Cliente"].Value.ToString();
                    frmReporteArticulosCliente frmReporteArticulosCliente = new frmReporteArticulosCliente(_criterioDeBusqueda);
                    frmReporteArticulosCliente.Show();
                    break;
                case "4_1_GO_4": // Ver Datos de Solicitud
                    frmVisorSolicitudesEspeciales frmVisorSolicitudesEspeciales = new frmVisorSolicitudesEspeciales(_referencia);
                    frmVisorSolicitudesEspeciales.Show();
                    break;
                case "4_1_GO_5": // Rechazar
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
                    // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "R", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.SolicitudEspecial, _referencia, _claveCliente);
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break; ;
                #endregion
                //*******************************************************************************************/
                //ACCIONES GENERALES
                //*******************************************************************************************/
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
        #endregion
        #region ACCIONES PARA PROCESO 2 - PEDIDOS ESPECIALES
        private void Proceso2_EV(int _ID, String _ClaveMenu, int _referencia, string _ClaveAreaSiguiente)
        {
            String _claveCliente = this.dgvResult.CurrentRow.Cells["Clave"].Value.ToString();
            switch (_ClaveMenu)
            {
                //*******************************************************************************************/
                //ACTIVIDAD 1. CORRECCIÓN DE PEDIDO
                //*******************************************************************************************/
                #region 1. CORRECCIÓN DE PEDIDO
                case "1_2_EV_1": // Buscador de cliente
                    _claveCliente = this.dgvResult.CurrentRow.Cells["CLAVE"].Value.ToString();
                    frmFindClie frmFindClie = new frmFindClie(_claveCliente, "frmControlPedidos");
                    frmFindClie.ShowDialog();
                    this.btnRefresh_Click(null, null);
                    break;
                case "1_2_EV_2": // Enviar pedido autorización
                    //SE VALIDA QUE EL PEDIDO HAYA SIDO PASADO A VENTAS
                    if (this.dgvResult.CurrentRow.Cells["ESTATUSPEDIDO"].Value.ToString() != "I")
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
                    _res = this.setAutorizacionPedido(_referencia, "EV", Globales.UsuarioActual.UsuarioUsuario);
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoEspecial, _referencia, _claveCliente);
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
                //ACCIONES GENERALES
                //*******************************************************************************************/
                #region CANCELAR
                //*******************************************************************************************/
                //CANCELACION
                //*******************************************************************************************/
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                #region EXPEDIENTE DIGITAL
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion
            }
        }
        private void Proceso2_CA(int _ID, String _ClaveMenu, int _referencia, string _ClaveAreaSiguiente)
        {
            String _claveCliente = this.dgvResult.CurrentRow.Cells["Clave"].Value.ToString();
            switch (_ClaveMenu)
            {
                //*******************************************************************************************/
                //ACTIVIDAD 2. VALIDACIÓN DE PEDIDO
                //*******************************************************************************************/
                #region 2. VALIDACIÓN DE PEDIDO
                case "2_2_CA_1": // Enviar a Gerente de Ventas

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
                    // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoEspecial, _referencia, _claveCliente);
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    break;
                case "2_2_CA_2":
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
                //ACTIVIDAD 7. PROGRAMACIÓN
                //*******************************************************************************************/
                #region 7. PROGRAMACIÓN
                case "7_2_CA_1": // Programar Producción
                    frmOrdProduccionMasiva frmOrdProduccionMasiva = new frmOrdProduccionMasiva(_referencia, true);
                    frmOrdProduccionMasiva.ShowDialog();

                    if (frmOrdProduccionMasiva.StatusProceso)
                    {
                        // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                        _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoEspecial, _referencia, _claveCliente);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    break;
                case "7_2_CA_2": // Generar Requisición de Compra
                    /*
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
                     */
                    frmRequisicionPedido frmRequisicionPedido = new frmRequisicionPedido(_referencia);
                    frmRequisicionPedido.ShowDialog();
                    if (frmRequisicionPedido.Procesada)
                    {
                        // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                        _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoEspecial, _referencia, _claveCliente);
                        if (_res == "")
                        {
                            MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.reload = true;
                        }
                        else
                            MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                    break;
                case "7_2_CA_3": // Ver Datos Pedido
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
                case "7_2_CA_4": // Imprimir Pedido
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
                        // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                        _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoEspecial, _referencia, _claveCliente);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    break;
                #endregion
                //*******************************************************************************************/
                //ACTIVIDAD 8. IMPRESIÓN
                //*******************************************************************************************/
                #region 8. IMPRESION
                case "8_2_CA_1":
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
                        // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                        _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoEspecial, _referencia, _claveCliente);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    break;
                #endregion
                //*******************************************************************************************/
                //ACCIONES GENERALES
                //*******************************************************************************************/
                #region CANCELAR
                //*******************************************************************************************/
                //CANCELACION
                //*******************************************************************************************/
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                #region EXPEDIENTE DIGITALs
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion
            }
        }
        private void Proceso2_GV(int _ID, String _ClaveMenu, int _referencia, string _ClaveAreaSiguiente)
        {
            String _claveCliente = this.dgvResult.CurrentRow.Cells["Clave"].Value.ToString();
            DataTable _dtSolicitudes = new DataTable();
            List<int> _Solicitudes;

            switch (_ClaveMenu)
            {
                //*******************************************************************************************/
                //ACTIVIDAD 3. AUTORIZACIÓN DE PEDIDOS
                //*******************************************************************************************/
                #region 3. AUTORIZACIÓN DE PEDIDOS
                case "3_2_GV_1": // Enviar autorización a Dirección
                    // CREAMOS AUTORIZACION POR GV
                    _res = setAutorizacionPedido(_referencia, "GV", Globales.UsuarioActual.UsuarioUsuario, true);
                    //MOSTRAMOS EL PEDIDO CON LA FIRMA
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
                        // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                        _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoEspecial, _referencia, _claveCliente);
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
                case "3_2_GV_2": // Ver Datos Pedido
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
                case "3_2_GV_3": // Ver Datos de Solicitud
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
                case "3_2_GV_4": // Rechazar y activar pedido
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
                    // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "R", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoEspecial, _referencia, _claveCliente);
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
                //ACCIONES GENERALES
                //*******************************************************************************************/
                #region CANCELAR
                //*******************************************************************************************/
                //CANCELACION
                //*******************************************************************************************/
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                #region EXPEDIENTE DIGITALs
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion
            }
        }
        private void Proceso2_DG(int _ID, String _ClaveMenu, int _referencia, string _ClaveAreaSiguiente)
        {
            String _claveCliente = this.dgvResult.CurrentRow.Cells["Clave"].Value.ToString();
            DataTable _dtSolicitudes = new DataTable();
            List<int> _Solicitudes;
            DataTable Solicitudes = new DataTable("SOLICITUDES");
            String _codigo = String.Empty;

            switch (_ClaveMenu)
            {
                //*******************************************************************************************/
                //ACTIVIDAD 4. AUTORIZACIÓN DE PEDIDOS
                //*******************************************************************************************/
                #region 4. AUTORIZACIÓN DE PEDIDOS
                case "4_2_DG_1": // Autorizar Pedido
                    _res = setAutorizacionPedido(_referencia, "DG", Globales.UsuarioActual.UsuarioUsuario, true);
                    //MOSTRAMOS EL PEDIDO CON LA FIRMA
                    Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    frmReportes.ShowDialog();
                    if (MessageBox.Show("¿Desea procesar la autorización del pedido?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                        _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoEspecial, _referencia, _claveCliente);
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
                case "4_2_DG_2": // Simulador de Costos
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
                        SOLICITUDES_ESPECIALES solicitudesBl = new SOLICITUDES_ESPECIALES();
                        Solicitudes = solicitudesBl.ConsultarSolicitud(int.Parse(_dtSolicitudes.Rows[0]["Solicitud"].ToString()));
                        if (Solicitudes.Rows.Count > 0)
                        {
                            _codigo = Solicitudes.Rows[0]["CODIGO_COTIZACION"].ToString();
                            frmSimuladorCostos frmSimulador = new frmSimuladorCostos(Enumerados.TipoSimulador.SimuladorDeCostos, _codigo);
                            frmSimulador.Show();
                        }
                        else
                        {
                            _codigo = _dtSolicitudes.Rows[0]["CODIGO_COTIZACION"].ToString();
                            frmSimuladorCostos frmSimulador = new frmSimuladorCostos(Enumerados.TipoSimulador.SimuladorDeCostos, _codigo);
                            frmSimulador.Show();
                        }
                    }
                    break;
                case "4_2_DG_3": // Ver Datos Pedido
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
                case "4_2_DG_4": // Ver Datos Solicitud
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
                case "4_2_DG_5": // Rechazar
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
                    // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "R", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoEspecial, _referencia, _claveCliente);
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
                //ACCIONES GENERALES
                //*******************************************************************************************/
                #region CANCELAR
                //*******************************************************************************************/
                //CANCELACION
                //*******************************************************************************************/
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                #region EXPEDIENTE DIGITALs
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion
            }
        }
        private void Proceso2_CP(int _ID, String _ClaveMenu, int _referencia, string _ClaveAreaSiguiente)
        {
            String _claveCliente = this.dgvResult.CurrentRow.Cells["Clave"].Value.ToString();
            DataTable _dtSolicitudes = new DataTable();
            List<int> _Solicitudes;
            DataTable Solicitudes = new DataTable("SOLICITUDES");
            String _codigo = String.Empty;

            switch (_ClaveMenu)
            {
                //*******************************************************************************************/
                //ACTIVIDAD 5. CAPTURA SAE
                //*******************************************************************************************/
                #region 5. CAPTURA SAE
                case "5_2_CP_1": // Autorizar Pedido
                    // Creamos firmas de autorización
                    _res = setAutorizacionPedido(_referencia, "CP", Globales.UsuarioActual.UsuarioUsuario, true);
                    _res = setAutorizacionPedido(_referencia, "FA", Globales.UsuarioActual.UsuarioUsuario, true);
                    //MOSTRAMOS EL PEDIDO CON LA FIRMA                    
                    Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    frmReportes.ShowDialog();
                    // FINALMENTE VALIDAMOS SI SE QUIERE PROCESAR LA AUTORIZACION                    
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
                        int pedidoFacturar = this.dgvResult.CurrentRow.Cells["Origen"].Value.ToString() == "N/A" ? _referencia : int.Parse(this.dgvResult.CurrentRow.Cells["Origen"].Value.ToString());

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

                        // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                        _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoEspecial, _referencia, _claveCliente);
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
                case "5_2_CP_2": // Ver Datos Pedido
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
                case "5_2_CP_3": // Rechazar y activar pedido
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
                    // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "R", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoEspecial, _referencia, _claveCliente);
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
                //*******************************************************************************************/
                //ACTIVIDAD 9. REQUISICION DE COMPRA
                //*******************************************************************************************/
                #region 9. REQUISICION DE COMPRA
                case "9_2_CP_1": // Liberar Compra
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
                    // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoEspecial, _referencia, _claveCliente);
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "9_2_CP_2": // Ver Datos Pedido
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
                case "9_2_CP_3": // Ver detalle de requisición
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Requisicion, _referencia);
                    frmReportes.ShowDialog();
                    if (frmReportes.procesado)
                    {
                        MessageBox.Show("Se ha notificado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;
                #endregion
                //*******************************************************************************************/
                //ACCIONES GENERALES
                //*******************************************************************************************/
                #region CANCELAR
                //*******************************************************************************************/
                //CANCELACION
                //*******************************************************************************************/
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                #region EXPEDIENTE DIGITALs
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion
            }
        }
        private void Proceso2_CR(int _ID, String _ClaveMenu, int _referencia, string _ClaveAreaSiguiente)
        {
            String _claveCliente = this.dgvResult.CurrentRow.Cells["Clave"].Value.ToString();
            DataTable _dtSolicitudes = new DataTable();
            List<int> _Solicitudes;
            DataTable Solicitudes = new DataTable("SOLICITUDES");
            String _codigo = String.Empty;

            switch (_ClaveMenu)
            {
                //*******************************************************************************************/
                //ACTIVIDAD 6. VERIFICAR CRÉDITO
                //*******************************************************************************************/
                #region 6. VERIFICAR CRÉDITO
                case "6_2_CR_1": // Autorizar Línea de Crédito
                    // CREAMOS AUTORIZACION POR CR
                    _res = setAutorizacionPedido(_referencia, "CR", Globales.UsuarioActual.UsuarioUsuario, true);
                    //MOSTRAMOS EL PEDIDO CON LA FIRMA                    
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
                        // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                        _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoEspecial, _referencia, _claveCliente);
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
                case "6_2_CR_2": // Rechazar Línea de Crédito
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
                    // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "R", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoEspecial, _referencia, _claveCliente);
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "6_2_CR_3": // Buscador de Cliente
                    frmFindClie frmFindClie = new frmFindClie(_claveCliente);
                    frmFindClie.Show();
                    break;
                case "6_2_CR_4": // Ver Datos Pedido
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
                //ACCIONES GENERALES
                //*******************************************************************************************/
                #region CANCELAR
                //*******************************************************************************************/
                //CANCELACION
                //*******************************************************************************************/
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                #region EXPEDIENTE DIGITALs
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion
            }
        }
        private void Proceso2_PL(int _ID, String _ClaveMenu, int _referencia, string _ClaveAreaSiguiente)
        {
            String _claveCliente = this.dgvResult.CurrentRow.Cells["Clave"].Value.ToString();
            DataTable _dtSolicitudes = new DataTable();
            List<int> _Solicitudes;
            DataTable Solicitudes = new DataTable("SOLICITUDES");
            String _codigo = String.Empty;

            switch (_ClaveMenu)
            {
                //*******************************************************************************************/
                //ACTIVIDAD 10. RUTA
                //*******************************************************************************************/
                #region 10. RUTA
                case "10_2_PL_1": // UP Pedidos
                    frmUpPedidos frmUpPedidos = new frmUpPedidos(_claveCliente, _referencia);
                    frmUpPedidos.ShowDialog();
                    if (frmUpPedidos.procesado)
                    {
                        ControlPedidos.setFinPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    break;
                case "10_2_PL_2": // Ver Pedido
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
                //ACCIONES GENERALES
                //*******************************************************************************************/
                #region CANCELAR
                //*******************************************************************************************/
                //CANCELACION
                //*******************************************************************************************/
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                #region EXPEDIENTE DIGITALs
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion
            }
        }
        #endregion
        #region ACCIONES PARA PROCESO 3 - PEDIDOS DE LINEA
        private void Proceso3_EV(int _ID, String _ClaveMenu, int _referencia, string _ClaveAreaSiguiente)
        {
            String _claveCliente = this.dgvResult.CurrentRow.Cells["Clave"].Value.ToString();
            switch (_ClaveMenu)
            {
                //*******************************************************************************************/
                //ACTIVIDAD 1. CORRECCIÓN DE PEDIDO
                //*******************************************************************************************/
                #region 1. CORRECCIÓN DE PEDIDO
                case "1_3_EV_1": // Buscador de cliente
                    _claveCliente = this.dgvResult.CurrentRow.Cells["CLAVE"].Value.ToString();
                    frmFindClie frmFindClie = new frmFindClie(_claveCliente, "frmControlPedidos");
                    frmFindClie.ShowDialog();
                    this.btnRefresh_Click(null, null);
                    break;
                case "1_3_EV_2": // Enviar pedido autorización
                    //SE VALIDA QUE EL PEDIDO HAYA SIDO PASADO A VENTAS
                    if (this.dgvResult.CurrentRow.Cells["ESTATUSPEDIDO"].Value.ToString() != "I")
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
                    _res = this.setAutorizacionPedido(_referencia, "EV", Globales.UsuarioActual.UsuarioUsuario);
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoLinea, _referencia, _claveCliente);
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
                //ACCIONES GENERALES
                //*******************************************************************************************/
                #region CANCELAR
                //*******************************************************************************************/
                //CANCELACION
                //*******************************************************************************************/
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                #region EXPEDIENTE DIGITAL
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion
            }
        }
        private void Proceso3_GV(int _ID, String _ClaveMenu, int _referencia, string _ClaveAreaSiguiente)
        {
            String _claveCliente = this.dgvResult.CurrentRow.Cells["Clave"].Value.ToString();
            switch (_ClaveMenu)
            {
                //*******************************************************************************************/
                //ACTIVIDAD 2. AUTORIZACIÓN DE PEDIDOS
                //*******************************************************************************************/
                #region 2. AUTORIZACIÓN DE PEDIDOS
                case "2_3_GV_1": // Ver Pedido
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
                case "2_3_GV_2": // Rechazar Pedido
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
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "R", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoLinea, _referencia, _claveCliente);
                    // CANCELAMOS LA AUTORIZACION PREVIA
                    _res = this.setCancelaAutorizacionPedido(_referencia, "GV", Globales.UsuarioActual.UsuarioUsuario, "EV");
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "2_3_GV_3": // Autorizar Pedido
                    // CREAMOS AUTORIZACION POR GV
                    _res = setAutorizacionPedido(_referencia, "GV", Globales.UsuarioActual.UsuarioUsuario, true);
                    _res = setAutorizacionPedido(_referencia, "FA", Globales.UsuarioActual.UsuarioUsuario, true);
                    //MOSTRAMOS EL PEDIDO CON LA FIRMA
                    _claveCliente = this.dgvResult.CurrentRow.Cells["CLAVE"].Value.ToString();
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
                        int pedidoFacturar = this.dgvResult.CurrentRow.Cells["Origen"].Value.ToString() == "N/A" ? _referencia : int.Parse(this.dgvResult.CurrentRow.Cells["Origen"].Value.ToString());
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
                        // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                        _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoLinea, _referencia, _claveCliente);
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
                //*******************************************************************************************/
                //ACCIONES GENERALES
                //*******************************************************************************************/
                #region CANCELAR
                //*******************************************************************************************/
                //CANCELACION
                //*******************************************************************************************/
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                #region EXPEDIENTE DIGITALs
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion
            }
        }
        private void Proceso3_CP(int _ID, String _ClaveMenu, int _referencia, string _ClaveAreaSiguiente)
        {
            String _claveCliente = this.dgvResult.CurrentRow.Cells["Clave"].Value.ToString();
            switch (_ClaveMenu)
            {
                //*******************************************************************************************/
                //ACTIVIDAD 3. REVISIÓN DE PEDIDOS
                //*******************************************************************************************/
                #region 3. REVISIÓN DE PEDIDOS
                case "3_3_CP_1": // Ver Pedido
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
                case "3_3_CP_2": // Autorizar Pedido
                    // CREAMOS AUTORIZACION POR CP
                    _res = setAutorizacionPedido(_referencia, "CP", Globales.UsuarioActual.UsuarioUsuario, true);
                    //MOSTRAMOS EL PEDIDO CON LA FIRMA                    
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
                        // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                        _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoLinea, _referencia, _claveCliente);
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
                case "3_3_CP_3": // Rechazar Pedido
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
                    // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "R", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoLinea, _referencia, _claveCliente);
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
                //*******************************************************************************************/
                //ACTIVIDAD 9. REQUISICIÓN DE COMPRA
                //*******************************************************************************************/
                #region 9. REQUISICIÓN DE COMPRA
                case "9_3_CP_1": // Ver Pedido
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
                case "9_3_CP_2": // Liberar Compra
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
                    // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoLinea, _referencia, _claveCliente);
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
                //ACCIONES GENERALES
                //*******************************************************************************************/
                #region CANCELAR
                //*******************************************************************************************/
                //CANCELACION
                //*******************************************************************************************/
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                #region EXPEDIENTE DIGITALs
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion
            }
        }
        private void Proceso3_CR(int _ID, String _ClaveMenu, int _referencia, string _ClaveAreaSiguiente)
        {
            String _claveCliente = this.dgvResult.CurrentRow.Cells["Clave"].Value.ToString();
            switch (_ClaveMenu)
            {
                //*******************************************************************************************/
                //ACTIVIDAD 4. VERIFICAR CRÉDITO
                //*******************************************************************************************/
                #region 4. VERIFICAR CRÉDITO
                case "4_3_CR_1": // Autorizar Línea de Crédito
                    //7.1 Autorizar linea de crédito
                    // CREAMOS AUTORIZACION POR CR
                    _res = setAutorizacionPedido(_referencia, "CR", Globales.UsuarioActual.UsuarioUsuario, true);
                    //MOSTRAMOS EL PEDIDO CON LA FIRMA
                    _claveCliente = this.dgvResult.CurrentRow.Cells["CLAVE"].Value.ToString();
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
                        // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                        _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoLinea, _referencia, _claveCliente);
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
                case "4_3_CR_2": // Rechazar Línea de Crédito
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

                    // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "R", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoLinea, _referencia, _claveCliente);
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
                case "4_3_CR_3": // Buscador de cliente
                    frmFindClie frmFindClie = new frmFindClie(_claveCliente);
                    frmFindClie.Show();
                    break;
                case "4_3_CR_4": // Estado de cuenta
                    frmFindClie = new frmFindClie(_claveCliente, "frmControlPedidos", "EstadoCuenta");
                    frmFindClie.Show();
                    break;
                #endregion
                //*******************************************************************************************/
                //ACCIONES GENERALES
                //*******************************************************************************************/
                #region CANCELAR
                //*******************************************************************************************/
                //CANCELACION
                //*******************************************************************************************/
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                #region EXPEDIENTE DIGITAL
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion
            }
        }
        private void Proceso3_PL(int _ID, String _ClaveMenu, int _referencia, string _ClaveAreaSiguiente)
        {
            String _claveCliente = this.dgvResult.CurrentRow.Cells["Clave"].Value.ToString();
            switch (_ClaveMenu)
            {
                //*******************************************************************************************/
                //ACTIVIDAD 5. RUTA
                //*******************************************************************************************/
                #region 5. RUTA
                case "5_3_PL_1": // UP Pedidos
                    frmUpPedidos frmUpPedidos = new frmUpPedidos(_claveCliente, _referencia);
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
                        // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                        _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoLinea, _referencia, _claveCliente);
                        if (_res == "")
                        {
                            MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.reload = true;
                        }
                        else
                            MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case "5_3_PL_2": // Ver Pedido
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
                case "5_3_PL_3": // Rechazar Pedido
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
                    // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "R", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoLinea, _referencia, _claveCliente);
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
                //ACCIONES GENERALES
                //*******************************************************************************************/
                #region CANCELAR
                //*******************************************************************************************/
                //CANCELACION
                //*******************************************************************************************/
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                #region EXPEDIENTE DIGITALs
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion
            }
        }
        private void Proceso3_DG(int _ID, String _ClaveMenu, int _referencia, string _ClaveAreaSiguiente)
        {
            String _claveCliente = this.dgvResult.CurrentRow.Cells["Clave"].Value.ToString();
            switch (_ClaveMenu)
            {
                //*******************************************************************************************/
                //ACTIVIDAD 6. AUTORIZACION DE PEDIDO
                //*******************************************************************************************/
                #region 6. AUTORIZACION DE PEDIDO
                case "6_3_DG_1": // Ver Pedido
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
                case "6_3_DG_2": // Autorizar Pedido
                    // CREAMOS AUTORIZACION POR DG
                    _res = setAutorizacionPedido(_referencia, "DG", Globales.UsuarioActual.UsuarioUsuario, true);
                    //MOSTRAMOS EL PEDIDO CON LA FIRMA                    
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    frmReportes.ShowDialog();
                    //FINALMENTE VALIDAMOS SI SE QUIERE PROCESAR LA AUTORIZACION                    
                    if (MessageBox.Show("¿Desea procesar la autorización del pedido?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        // VALIDAMOS SI EL PEDIDO ES CON EXISTENCIA O (SIN EXISTENCIAS Y SIN OP)
                        Boolean requiereOP = Boolean.Parse(this.dgvResult.CurrentRow.Cells["RequiereOP"].Value.ToString());
                        // SI EL PEDIDO REQUIERE OP, BUSCAMOS ALGUNA DISPONIBLE
                        // SI LA OP CUBRE EL 100% DEL PEDIDO SE ASIGNA DE FORMA DIRECTA Y SE MANDA AL AREA CORRESPONDIETE EN POSICION 1 DE LA CONFIGURACION
                        // SI LA OP NO CUBRE EL 100% DEL PEDIDO SE ASIGNA DE FORMA DIRECTA Y SE MANDA AL AREA CORRESPONDIETE EN POSICION 2 DE LA CONFIGURACION
                        if (requiereOP)
                        {
                            DataSet ds = FlujoOP.AsignaOPAutomaticaPedidos(_referencia);
                            if (ds.Tables.Count > 0)
                            {
                                if ((Boolean)ds.Tables[0].Rows[0]["Asignacion"])
                                {
                                    String _message = "";
                                    foreach (DataRow dr in ds.Tables[1].Rows)
                                    {
                                        _message += String.Format("{0} {1}", dr["OPSURTIR"].ToString(), "\r\n");
                                    }
                                    // MessageBox.Show("Se han asignado la OP de forma automática: \r\n" + _message, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    // SE ASIGNA POSICION 0
                                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('|')[0].Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('|')[0].Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoLinea, _referencia, _claveCliente);
                                }
                                else
                                {
                                    // SE ASIGNA POSICION 1
                                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('|')[!requiereOP ? 0 : 1].Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('|')[!requiereOP ? 0 : 1].Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoLinea, _referencia, _claveCliente);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Error al asignar OP.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                        }
                        else
                        {
                            _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('|')[0].Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('|')[0].Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoLinea, _referencia, _claveCliente);
                        }
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
                case "6_3_DG_3": // Rechazar Pedido
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
                    // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "R", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoLinea, _referencia, _claveCliente);
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
                //ACCIONES GENERALES
                //*******************************************************************************************/
                #region CANCELAR
                //*******************************************************************************************/
                //CANCELACION
                //*******************************************************************************************/
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                #region EXPEDIENTE DIGITALs
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion
            }
        }
        private void Proceso3_SU(int _ID, String _ClaveMenu, int _referencia, string _ClaveAreaSiguiente)
        {
            String _claveCliente = this.dgvResult.CurrentRow.Cells["Clave"].Value.ToString();
            switch (_ClaveMenu)
            {
                //*******************************************************************************************/
                //ACTIVIDAD 7. IMPRESION
                //*******************************************************************************************/
                #region 7. IMPRESION
                case "7_3_SU_1": // Imprimir Pedido y Fin de Flujo
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
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    break;
                #endregion
                //*******************************************************************************************/
                //ACCIONES GENERALES
                //*******************************************************************************************/
                #region CANCELAR
                //*******************************************************************************************/
                //CANCELACION
                //*******************************************************************************************/
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                #region EXPEDIENTE DIGITALs
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion
            }
        }
        private void Proceso3_CA(int _ID, String _ClaveMenu, int _referencia, string _ClaveAreaSiguiente)
        {
            String _claveCliente = this.dgvResult.CurrentRow.Cells["Clave"].Value.ToString();
            switch (_ClaveMenu)
            {
                //*******************************************************************************************/
                //ACTIVIDAD 8. REQUISICION DE OP
                //*******************************************************************************************/
                #region 8. REQUISICION DE OP
                case "8_3_CA_1": // Ver Pedido
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
                case "8_3_CA_2": // Generar Requisición de Compra
                    DialogResult res = MessageBox.Show("Desea introducir observaciones adicionales para la siguiente área?", "SIP", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (res == DialogResult.Cancel)
                    {
                        break;
                    }
                    if (res == System.Windows.Forms.DialogResult.Yes)
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
                    // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoLinea, _referencia, _claveCliente);
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "8_3_CA_3": // Reporte de faltantes
                    Cursor.Current = Cursors.WaitCursor;
                    // OBTENEMOS EL REPORTE
                    DataTable dtResult = ControlPedidos.getReportePedidosFaltantes();
                    if (dtResult != null)
                    {
                        if (dtResult.Rows.Count > 0)
                        {
                            string archivoTemporal = System.IO.Path.GetTempFileName().Replace(".tmp", ".xls");
                            ControlPedidos.GeneraArchivoExcelPedidosFaltantes(archivoTemporal, dtResult);
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
                    break;
                #endregion
                //*******************************************************************************************/
                //ACTIVIDAD 10. REQUISICION ELABORADA
                #region 10. REQUISICION ELABORADA
                case "10_3_CA_1": // Ver Pedido
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
                case "10_3_CA_2": // Enviar pedido a surtido
                    res = MessageBox.Show("Desea introducir observaciones adicionales para la siguiente área?", "SIP", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (res == DialogResult.Cancel)
                    {
                        break;
                    }
                    if (res == System.Windows.Forms.DialogResult.Yes)
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
                    // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoLinea, _referencia, _claveCliente);
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
                //*******************************************************************************************/
                //ACCIONES GENERALES
                //*******************************************************************************************/
                #region CANCELAR
                //*******************************************************************************************/
                //CANCELACION
                //*******************************************************************************************/
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                #region EXPEDIENTE DIGITAL
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion
            }
        }
        #endregion
        #region ACCIONES PARA PROCESO 4 - ORDENES DE PRODUCCION
        private void Proceso4_GO(int _ID, String _ClaveMenu, int _referencia, string _ClaveAreaSiguiente)
        {
            switch (_ClaveMenu)
            {
                //*******************************************************************************************/
                //ACTIVIDAD 1. CARGA DE INSUMOS
                //*******************************************************************************************/
                #region 1. CARGA DE INSUMOS
                case "1_4_GO_1":  // Cargar hoja de existencias
                    frmFlujoOP frmFlujoOP = new frmFlujoOP(_referencia, false, false, false, false, true);
                    frmFlujoOP.ShowDialog();
                    if (frmFlujoOP.hojaExistenciasCargada)
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
                        _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.OrdenesProduccion, _referencia, "");
                        if (_res == "")
                        {
                            MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.reload = true;
                        }
                        else
                            MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case "1_4_GO_2":  // Rechazar
                    ControlFlujo.SetFinFlujo(_ID);
                    MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.reload = true;
                    break;
                #endregion
                //*******************************************************************************************/
                //ACTIVIDAD 7. ENVIAR PROGRAMA
                //*******************************************************************************************/
                #region 7. ENVIAR PROGRAMA CP
                case "7_4_GO_1": // Enviar programa a CP
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
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.OrdenesProduccion, _referencia, "");
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
                //ACTIVIDAD 9. CARGA DE FECHAS DE ENTREGA
                //*******************************************************************************************/
                #region 9. CARGA DE FECHAS DE ENTREGA
                case "9_4_GO_1":
                    frmFlujoOP = new frmFlujoOP(_referencia, false, false, true, true, true);
                    frmFlujoOP.ShowDialog();
                    if (frmFlujoOP.fechasAsignadas)
                    {
                        ControlFlujo.SetFinFlujo(_ID);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    break;
                #endregion
            }
        }
        private void Proceso4_GV(int _ID, String _ClaveMenu, int _referencia, string _ClaveAreaSiguiente)
        {
            switch (_ClaveMenu)
            {
                //*******************************************************************************************/
                //ACTIVIDAD 2. AUTORIZACION DE INSUMOS
                //*******************************************************************************************/
                #region 2. AUTORIZACION DE INSUMOS
                case "2_4_GV_1":  // Consulta de documentación
                    frmFlujoOP frmFlujoOP = new frmFlujoOP(_referencia, true);
                    frmFlujoOP.ShowDialog();
                    break;
                case "2_4_GV_2":  // Autorizar documentación
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
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.OrdenesProduccion, _referencia, "");
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "2_4_GV_3":
                    ControlFlujo.SetFinFlujo(_ID);
                    MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.reload = true;
                    break;
                #endregion
                //*******************************************************************************************/
                //ACTIVIDAD 6. ENVIAR PROGRAMA
                //*******************************************************************************************/
                #region 6. ENVIAR PROGRAMA
                case "6_4_GV_1": // Enviar programa a CA
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
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.OrdenesProduccion, _referencia, "");
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "6_4_GV_2": // Consultar documentación
                    frmFlujoOP = new frmFlujoOP(_referencia, true, false, false, true);
                    frmFlujoOP.ShowDialog();
                    break;
                case "6_4_GV_3": // Rechazar - se devuelve a area correspondiente
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
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "R", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.OrdenesProduccion, _referencia, "");
                    // ELIMINAMOS LAS OP RELACIONADAS AL FLUJO
                    FlujoOP.EliminaOPById(_referencia);
                    FlujoOP.EliminaProgramaOP(_referencia);
                    // ELIMINAMOS EL FLUJO DE LA OP HASTA LA CARGA DE LA PROPUESTA
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                #endregion
            }
        }
        private void Proceso4_CA(int _ID, String _ClaveMenu, int _referencia, string _ClaveAreaSiguiente)
        {
            switch (_ClaveMenu)
            {
                //*******************************************************************************************/
                //ACTIVIDAD 3. PROPUESTA DE PROGRAMA
                //*******************************************************************************************/
                #region 3. PROPUESTA DE PROGRAMA
                case "3_4_CA_1": //Cargar Propuesta
                    frmFlujoOP frmFlujoOP = new frmFlujoOP(_referencia);
                    frmFlujoOP.ShowDialog();
                    if (frmFlujoOP.hojaPropuestaCargada)
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
                        _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.OrdenesProduccion, _referencia, "");
                        if (_res == "")
                        {
                            MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.reload = true;
                        }
                        else
                            MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case "3_4_CA_2": // Rechazar
                    ControlFlujo.SetFinFlujo(_ID);
                    MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.reload = true;
                    break;

                #endregion
                //*******************************************************************************************/
                //ACTIVIDAD 5. PROGRAMA DE PRODUCCION
                //*******************************************************************************************/
                #region 5. PROGRAMA DE PRODUCCION
                case "5_4_CA_1": // Cargar programa de producción
                    frmFlujoOP = new frmFlujoOP(_referencia, false, true);
                    frmFlujoOP.ShowDialog();
                    if (frmFlujoOP.programaProduccionCreado)
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
                        _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.OrdenesProduccion, _referencia, "");
                        if (_res == "")
                        {
                            MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.reload = true;
                        }
                        else
                            MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                #endregion
                //*******************************************************************************************/
                //ACTIVIDAD 10. ENLACE DE PEDIDOS
                //*******************************************************************************************/
                #region 10. ENLACE DE PEDIDOS
                case "10_4_CA_1": // Enlazar Pedidos a OP
                    frmRelacionPedidosOP frmRelacionPedidosOP = new frmRelacionPedidosOP(_referencia);
                    frmRelacionPedidosOP.ShowDialog();
                    if (frmRelacionPedidosOP.enlaceFinalizado)
                    {
                        // BUSCAMOS TODOS LOS PEDIDOS PROCESADOS PARA MOVERLOS A LA SIGUIENTE AREA:
                        DataTable dtPedidos = FlujoOP.GetFlujoPedidosEnlazados(frmRelacionPedidosOP.ListaPedidosEnlazados);

                        foreach (DataRow _rowPedido in dtPedidos.Rows)
                        {
                            String _ClaveAreaSiguienteAux = _rowPedido["ClaveMenuSiguiente"].ToString();
                            _res = this.setLineaTiempo((int)_rowPedido["Id"], _ClaveAreaSiguienteAux.Split('_')[2], "A", int.Parse(_ClaveAreaSiguienteAux.Split('_')[0]), "", Globales.UsuarioActual.UsuarioUsuario, (int)_rowPedido["ClaveTipoProceso"], (int)_rowPedido["ReferenciaProceso"], _rowPedido["Cliente"].ToString());
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
                        _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.OrdenesProduccion, _referencia, "");
                        if (_res == "")
                        {
                            MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.reload = true;
                        }
                        else
                        {
                            MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    }
                    break;
                case "10_4_CA_2": // Envíar OP
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
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.OrdenesProduccion, _referencia, "");
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                #endregion
            }
        }
        private void Proceso4_DG(int _ID, String _ClaveMenu, int _referencia, string _ClaveAreaSiguiente)
        {
            switch (_ClaveMenu)
            {
                //*******************************************************************************************/
                //ACTIVIDAD 4. AUTORIZACION DE PROPUESTA
                //*******************************************************************************************/
                #region 4. AUTORIZACION DE PROPUESTA
                case "4_4_DG_1": //Consultar documentacion
                    frmFlujoOP frmFlujoOP = new frmFlujoOP(_referencia);
                    frmFlujoOP.ShowDialog();
                    break;
                case "4_4_DG_2": //Autorizar propuesta

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
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.OrdenesProduccion, _referencia, "");
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    break;
                case "4_4_DG_3":
                    ControlFlujo.SetFinFlujo(_ID);
                    MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.reload = true;
                    break;
                #endregion
            }
        }
        private void Proceso4_CPR(int _ID, String _ClaveMenu, int _referencia, string _ClaveAreaSiguiente)
        {
            switch (_ClaveMenu)
            {
                //*******************************************************************************************/
                //ACTIVIDAD 8. PROGRAMA
                //*******************************************************************************************/
                #region 8. PROGRAMA
                case "8_4_CPR_1": // Enviar programa a GO
                    if ((MessageBox.Show("Desea introducird observaciones adicionales para la siguiente área?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes))
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
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.OrdenesProduccion, _referencia, "");
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "8_4_CPR_2": // Consultar documentación
                    frmFlujoOP frmFlujoOP = new frmFlujoOP(_referencia, true, false, false, true);
                    frmFlujoOP.ShowDialog();
                    break;
                case "8_4_CPR_3": // Reporte OP
                    DataSet dsDetalle = new DataSet();
                    dsDetalle = FlujoOP.ConsultaFlujoOPDetalle(_referencia);
                    if (dsDetalle.Tables.Count == 2) // TABLA 2 ES DETALLE DE OP
                    {
                        RepOrdProd OrdProd = new RepOrdProd();
                        DataView view = new DataView(dsDetalle.Tables[1]);
                        DataTable distinctValues = view.ToTable(true, "OrdenProduccion");
                        DataTable dataTable = OrdProd.RegresaTabla(distinctValues.Rows[0][0].ToString(), distinctValues.Rows[distinctValues.Rows.Count - 1][0].ToString(), Enumerados.TipoOrdenProduccion.Liberada);
                        string archivoTemporal = System.IO.Path.GetTempFileName().Replace(".tmp", ".xls");
                        RepOrdProd.GeneraArchivoExcel(archivoTemporal, dataTable, distinctValues.Rows[0][0].ToString());
                        FuncionalidadesFormularios.MostrarExcel(archivoTemporal);
                    }

                    break;
                #endregion
            }
        }
        #endregion
        #region ACCIONES PARA PROCESO 5 - ORDENES DE PRODUCCION FALTANTES
        private void Proceso5_CA(int _ID, String _ClaveMenu, int _referencia, string _ClaveAreaSiguiente)
        {
            switch (_ClaveMenu)
            {
                //*******************************************************************************************/
                //ACTIVIDAD 1. PROGRAMA DE PRODUCCION
                //*******************************************************************************************/
                #region 1. PROGRAMA DE PRODUCCION
                case "1_5_CA_1": // Cargar programa de producción
                    frmFlujoOP frmFlujoOP = new frmFlujoOP(_referencia, false, true);
                    frmFlujoOP.ShowDialog();
                    if (frmFlujoOP.programaProduccionCreado)
                    {
                        _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.OrdenesProduccionFaltante, _referencia, "");
                        if (_res == "")
                        {
                            MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.reload = true;
                        }
                        else
                            MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                #endregion
                //*******************************************************************************************/
                //ACTIVIDAD 2. ENLACE DE PEDIDOS
                //*******************************************************************************************/
                #region 2. ENLACE DE PEDIDOS
                case "2_5_CA_1": // Enlazar Pedidos a OP
                    frmRelacionPedidosOP frmRelacionPedidosOP = new frmRelacionPedidosOP(_referencia);
                    frmRelacionPedidosOP.ShowDialog();
                    if (frmRelacionPedidosOP.enlaceFinalizado)
                    {
                        // BUSCAMOS TODOS LOS PEDIDOS PROCESADOS PARA MOVERLOS A LA SIGUIENTE AREA:
                        DataTable dtPedidos = FlujoOP.GetFlujoPedidosEnlazados(frmRelacionPedidosOP.ListaPedidosEnlazados);

                        foreach (DataRow _rowPedido in dtPedidos.Rows)
                        {
                            String _ClaveAreaSiguienteAux = _rowPedido["ClaveMenuSiguiente"].ToString();
                            _res = this.setLineaTiempo((int)_rowPedido["Id"], _ClaveAreaSiguienteAux.Split('_')[2], "A", int.Parse(_ClaveAreaSiguienteAux.Split('_')[0]), "", Globales.UsuarioActual.UsuarioUsuario, (int)_rowPedido["ClaveTipoProceso"], (int)_rowPedido["ReferenciaProceso"], _rowPedido["Cliente"].ToString());
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
                        _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.OrdenesProduccionFaltante, _referencia, "");
                        if (_res == "")
                        {
                            MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.reload = true;
                        }
                        else
                        {
                            MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    }
                    break;
                #endregion
            }
        }
        private void Proceso5_GV(int _ID, String _ClaveMenu, int _referencia, string _ClaveAreaSiguiente)
        {
            switch (_ClaveMenu)
            {
                //*******************************************************************************************/
                //ACTIVIDAD 3. ENVIAR PROGRAMA
                //*******************************************************************************************/
                #region 3. ENVIAR PROGRAMA
                case "3_5_GV_1": // Enviar programa
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
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.OrdenesProduccionFaltante, _referencia, "");
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                #endregion
            }
        }
        private void Proceso5_GO(int _ID, String _ClaveMenu, int _referencia, string _ClaveAreaSiguiente)
        {
            switch (_ClaveMenu)
            {
                //*******************************************************************************************/
                //ACTIVIDAD 4. ENVIAR PROGRAMA
                //*******************************************************************************************/
                #region 4. ENVIAR PROGRAMA
                case "4_5_GO_1": // Enviar programa
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
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.OrdenesProduccionFaltante, _referencia, "");
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
                //ACTIVIDAD 6. FECHAS DE ENTREGA
                //*******************************************************************************************/
                #region 6. FECHAS DE ENTREGA
                case "6_5_GO_1":
                    frmFlujoOP frmFlujoOP = new frmFlujoOP(_referencia, true, false, true, true, true);
                    frmFlujoOP.ShowDialog();
                    if (frmFlujoOP.fechasAsignadas)
                    {
                        ControlFlujo.SetFinFlujo(_ID);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    break;
                #endregion
            }
        }
        private void Proceso5_CPR(int _ID, String _ClaveMenu, int _referencia, string _ClaveAreaSiguiente)
        {
            switch (_ClaveMenu)
            {
                //*******************************************************************************************/
                //ACTIVIDAD 5. ENVIAR PROGRAMA
                //*******************************************************************************************/
                #region 5. ENVIAR PROGRAMA
                case "5_5_CPR_1": // Enviar programa
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
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.OrdenesProduccionFaltante, _referencia, "");
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "5_5_CPR_2":
                    DataSet dsDetalle = new DataSet();
                    dsDetalle = FlujoOP.ConsultaFlujoOPDetalle(_referencia);
                    if (dsDetalle.Tables.Count == 2) // TABLA 2 ES DETALLE DE OP
                    {
                        RepOrdProd OrdProd = new RepOrdProd();
                        DataView view = new DataView(dsDetalle.Tables[1]);
                        DataTable distinctValues = view.ToTable(true, "OrdenProduccion");
                        DataTable dataTable = OrdProd.RegresaTabla(distinctValues.Rows[0][0].ToString(), distinctValues.Rows[distinctValues.Rows.Count - 1][0].ToString(), Enumerados.TipoOrdenProduccion.Liberada);
                        string archivoTemporal = System.IO.Path.GetTempFileName().Replace(".tmp", ".xls");
                        RepOrdProd.GeneraArchivoExcel(archivoTemporal, dataTable, distinctValues.Rows[0][0].ToString());
                        FuncionalidadesFormularios.MostrarExcel(archivoTemporal);
                    }
                    break;
                #endregion
            }
        }
        #endregion
        #region ACCIONES PARA PROCESO 6 - PEDIDOS DAT
        private void Proceso6_EV(int _ID, String _ClaveMenu, int _referencia, string _ClaveAreaSiguiente)
        {
            String _claveCliente = this.dgvResult.CurrentRow.Cells["Clave"].Value.ToString();
            switch (_ClaveMenu)
            {
                //*******************************************************************************************/
                //ACTIVIDAD 1. CORRECCIÓN DE PEDIDO
                //*******************************************************************************************/
                #region 1. CORRECCIÓN DE PEDIDO
                case "1_6_EV_1": // Buscador de cliente
                    _claveCliente = this.dgvResult.CurrentRow.Cells["CLAVE"].Value.ToString();
                    frmFindClie frmFindClie = new frmFindClie(_claveCliente, "frmControlPedidos");
                    frmFindClie.ShowDialog();
                    this.btnRefresh_Click(null, null);
                    break;
                case "1_6_EV_2": // Enviar pedido autorización
                    //SE VALIDA QUE EL PEDIDO HAYA SIDO PASADO A VENTAS
                    if (this.dgvResult.CurrentRow.Cells["ESTATUSPEDIDO"].Value.ToString() != "I")
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
                    _res = this.setAutorizacionPedido(_referencia, "EV", Globales.UsuarioActual.UsuarioUsuario);
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoDAT, _referencia, _claveCliente);
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
                //ACCIONES GENERALES
                //*******************************************************************************************/
                #region CANCELAR
                //*******************************************************************************************/
                //CANCELACION
                //*******************************************************************************************/
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                #region EXPEDIENTE DIGITAL
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion
            }
        }
        private void Proceso6_GV(int _ID, String _ClaveMenu, int _referencia, string _ClaveAreaSiguiente)
        {
            String _claveCliente = this.dgvResult.CurrentRow.Cells["Clave"].Value.ToString();
            switch (_ClaveMenu)
            {
                //*******************************************************************************************/
                //ACTIVIDAD 2. AUTORIZACIÓN DE PEDIDOS
                //*******************************************************************************************/
                #region 2. AUTORIZACIÓN DE PEDIDOS
                case "2_6_GV_1": // Ver Pedido
                    /* QUITAMOS IMPRESION NORMAL
                    SIP.Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido);
                     * */
                    Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.OrdenTrabajo, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    if (frmReportes.pedidoTambienImprimeOT)
                    {
                        frmReportes.Show();
                    }
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.PedidoDAT, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    frmReportes.Show();
                    break;
                case "2_6_GV_2": // Rechazar Pedido
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
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "R", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoDAT, _referencia, _claveCliente);
                    // CANCELAMOS LA AUTORIZACION PREVIA
                    _res = this.setCancelaAutorizacionPedido(_referencia, "GV", Globales.UsuarioActual.UsuarioUsuario, "EV");
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "2_6_GV_3": // Autorizar Pedido
                    // CREAMOS AUTORIZACION POR GV
                    _res = setAutorizacionPedido(_referencia, "GV", Globales.UsuarioActual.UsuarioUsuario, true);
                    _res = setAutorizacionPedido(_referencia, "FA", Globales.UsuarioActual.UsuarioUsuario, true);
                    //MOSTRAMOS EL PEDIDO CON LA FIRMA
                    _claveCliente = this.dgvResult.CurrentRow.Cells["CLAVE"].Value.ToString();
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.PedidoDAT, "frmControlPedidos", true, false);
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
                        int pedidoFacturar = this.dgvResult.CurrentRow.Cells["Origen"].Value.ToString() == "N/A" ? _referencia : int.Parse(this.dgvResult.CurrentRow.Cells["Origen"].Value.ToString());
                        if (pedidoFacturar != _referencia)
                        {
                            // si el pedido no ha sido facturado, facturamos el pedido original
                            // en caso de que ya haya sido facturado el pedido original, unicamente se procesa el pedido en flujo de control pedidos
                            if (CargaPedidosSAE.ValidaPedidoFacturado(pedidoFacturar, "D"))
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
                                CargaPedidosSAE.CargaPedidoEnSae(pedidoFacturar, comision, 16, ref ex, "D");
                            }
                        }
                        else
                        {
                            // ENVIAMOS EL PEDIDO ACTUAL
                            PED_MSTR pedido_imprimir = new PED_MSTR();
                            DataTable datos_pedido = new DataTable();
                            datos_pedido = pedido_imprimir.ConsultaImprimir(pedidoFacturar);
                            comision = datos_pedido.Rows[0]["COMISION"] == null ? 0 : Convert.ToDecimal(datos_pedido.Rows[0]["COMISION"]);
                            CargaPedidosSAE.CargaPedidoEnSae(pedidoFacturar, comision, 16, ref ex, "D");
                        }
                        // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                        _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoDAT, _referencia, _claveCliente);
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
                //*******************************************************************************************/
                //ACCIONES GENERALES
                //*******************************************************************************************/
                #region CANCELAR
                //*******************************************************************************************/
                //CANCELACION
                //*******************************************************************************************/
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                #region EXPEDIENTE DIGITALs
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion
            }
        }
        private void Proceso6_CP(int _ID, String _ClaveMenu, int _referencia, string _ClaveAreaSiguiente)
        {
            String _claveCliente = this.dgvResult.CurrentRow.Cells["Clave"].Value.ToString();
            switch (_ClaveMenu)
            {
                //*******************************************************************************************/
                //ACTIVIDAD 3. REVISIÓN DE PEDIDOS
                //*******************************************************************************************/
                #region 3. REVISIÓN DE PEDIDOS
                case "3_6_CP_1": // Ver Pedido
                    Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.OrdenTrabajo, _claveCliente, 0, _referencia, Enumerados.TipoPedido.PedidoDAT, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    if (frmReportes.pedidoTambienImprimeOT)
                    {
                        frmReportes.Show();
                    }
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.PedidoDAT, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    frmReportes.Show();
                    break;
                case "3_6_CP_2": // Autorizar Pedido
                    // CREAMOS AUTORIZACION POR CP
                    _res = setAutorizacionPedido(_referencia, "CP", Globales.UsuarioActual.UsuarioUsuario, true);
                    //MOSTRAMOS EL PEDIDO CON LA FIRMA                    
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.PedidoDAT, "frmControlPedidos", true, false);
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
                        // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                        _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoDAT, _referencia, _claveCliente);
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
                case "3_6_CP_3": // Rechazar Pedido
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
                    // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "R", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoDAT, _referencia, _claveCliente);
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
                //*******************************************************************************************/
                //ACTIVIDAD 6. FECHA DE ENTREGA
                #region 6. FECHA DE ENTREGA
                case "6_6_CP_1": // Ver pedido
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.OrdenTrabajo, _claveCliente, 0, _referencia, Enumerados.TipoPedido.PedidoDAT, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    if (frmReportes.pedidoTambienImprimeOT)
                    {
                        frmReportes.Show();
                    }
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.PedidoDAT, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    frmReportes.Show();
                    break;
                case "6_6_CP_2": // Fecha de entrega
                    frmUpPedidos frmUpPedidos = new frmUpPedidos(_claveCliente, _referencia);
                    frmUpPedidos.ShowDialog();
                    if (frmUpPedidos.procesado)
                    {
                        // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                        _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoDAT, _referencia, _claveCliente);
                        if (_res == "")
                        {
                            MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.reload = true;
                        }
                        else
                            MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                #endregion
                //*******************************************************************************************/
                //*******************************************************************************************/
                //ACCIONES GENERALES
                //*******************************************************************************************/
                #region CANCELAR
                //*******************************************************************************************/
                //CANCELACION
                //*******************************************************************************************/
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                #region EXPEDIENTE DIGITALs
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion
            }
        }
        private void Proceso6_CR(int _ID, String _ClaveMenu, int _referencia, string _ClaveAreaSiguiente)
        {
            String _claveCliente = this.dgvResult.CurrentRow.Cells["Clave"].Value.ToString();
            switch (_ClaveMenu)
            {
                //*******************************************************************************************/
                //ACTIVIDAD 4. VERIFICAR CRÉDITO
                //*******************************************************************************************/
                #region 4. VERIFICAR CRÉDITO
                case "4_6_CR_1": // Autorizar Línea de Crédito
                    //7.1 Autorizar linea de crédito
                    // CREAMOS AUTORIZACION POR CR
                    _res = setAutorizacionPedido(_referencia, "CR", Globales.UsuarioActual.UsuarioUsuario, true);
                    //MOSTRAMOS EL PEDIDO CON LA FIRMA
                    _claveCliente = this.dgvResult.CurrentRow.Cells["CLAVE"].Value.ToString();
                    Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.PedidoDAT, "frmControlPedidos", true, false);
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
                        // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                        // RN SE VALIDA SI ES MAYOR A 100 PRENDAS
                        int totalPrendas = int.Parse(this.dgvResult.CurrentRow.Cells["Prendas"].Value.ToString());
                        _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('|')[totalPrendas > 100 ? 0 : 1].Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('|')[totalPrendas > 100 ? 0 : 1].Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoDAT, _referencia, _claveCliente);
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
                case "4_6_CR_2": // Rechazar Línea de Crédito
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

                    // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "R", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoDAT, _referencia, _claveCliente);
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
                case "4_6_CR_3": // Buscador de cliente
                    frmFindClie frmFindClie = new frmFindClie(_claveCliente);
                    frmFindClie.Show();
                    break;
                case "4_6_CR_4": // Estado de cuenta
                    frmFindClie = new frmFindClie(_claveCliente, "frmControlPedidos", "EstadoCuenta");
                    frmFindClie.Show();
                    break;
                #endregion
                //*******************************************************************************************/
                //ACCIONES GENERALES
                //*******************************************************************************************/
                #region CANCELAR
                //*******************************************************************************************/
                //CANCELACION
                //*******************************************************************************************/
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                #region EXPEDIENTE DIGITAL
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion
            }
        }
        private void Proceso6_SU(int _ID, String _ClaveMenu, int _referencia, string _ClaveAreaSiguiente)
        {
            String _claveCliente = this.dgvResult.CurrentRow.Cells["Clave"].Value.ToString();
            switch (_ClaveMenu)
            {
                //*******************************************************************************************/
                //ACTIVIDAD 5. FECHA DE ENTREGA
                //*******************************************************************************************/
                #region 5. FECHA DE ENTREGA
                case "5_6_SU_1": // Ver Pedido
                    Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.OrdenTrabajo, _claveCliente, 0, _referencia, Enumerados.TipoPedido.PedidoDAT, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    if (frmReportes.pedidoTambienImprimeOT)
                    {
                        frmReportes.Show();
                    }
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.PedidoDAT, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    frmReportes.Show();
                    break;
                case "5_6_SU_2": // Fecha de entrega
                    frmUpPedidos frmUpPedidos = new frmUpPedidos(_claveCliente, _referencia);
                    frmUpPedidos.ShowDialog();
                    if (frmUpPedidos.procesado)
                    {
                        // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                        _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoDAT, _referencia, _claveCliente);
                        if (_res == "")
                        {
                            MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.reload = true;
                        }
                        else
                            MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                #endregion
                //*******************************************************************************************/
                //ACTIVIDAD 7. IMPRESION DE PEDIDO
                //*******************************************************************************************/
                #region 7. IMPRESION DE PEDIDO
                case "7_6_SU_1": // Impresión
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.PedidoDAT, "frmControlPedidos", true, true);
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
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    break;
                #endregion
                //*******************************************************************************************/
                //ACCIONES GENERALES
                //*******************************************************************************************/
                #region CANCELAR
                //*******************************************************************************************/
                //CANCELACION
                //*******************************************************************************************/
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                #region EXPEDIENTE DIGITAL
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion
            }
        }
        private void Proceso6_CM(int _ID, String _ClaveMenu, int _referencia, string _ClaveAreaSiguiente)
        {
            String _claveCliente = this.dgvResult.CurrentRow.Cells["Clave"].Value.ToString();
            switch (_ClaveMenu)
            {
                //*******************************************************************************************/
                //ACTIVIDAD 3. REVISIÓN DE PEDIDOS
                //*******************************************************************************************/                
                //ACTIVIDAD 6. FECHA DE ENTREGA
                #region 6. FECHA DE ENTREGA
                case "6_6_CM_1": // Ver pedido
                    Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.OrdenTrabajo, _claveCliente, 0, _referencia, Enumerados.TipoPedido.PedidoDAT, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    if (frmReportes.pedidoTambienImprimeOT)
                    {
                        frmReportes.Show();
                    }
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.PedidoDAT, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    frmReportes.Show();
                    break;
                case "6_6_CM_2": // Fecha de entrega
                    frmUpPedidos frmUpPedidos = new frmUpPedidos(_claveCliente, _referencia);
                    frmUpPedidos.ShowDialog();
                    if (frmUpPedidos.procesado)
                    {
                        // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                        _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoDAT, _referencia, _claveCliente);
                        if (_res == "")
                        {
                            MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.reload = true;
                        }
                        else
                            MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                #endregion
                //*******************************************************************************************/
                //ACTIVIDAD 8. IMPRESION DE PEDIDO
                //*******************************************************************************************/
                #region 7. IMPRESION DE PEDIDO
                case "8_6_CM_1": // Impresión
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.PedidoDAT, "frmControlPedidos", true, true);
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
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    break;
                #endregion
                //*******************************************************************************************/
                //ACCIONES GENERALES
                //*******************************************************************************************/
                #region CANCELAR
                //*******************************************************************************************/
                //CANCELACION
                //*******************************************************************************************/
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                #region EXPEDIENTE DIGITALs
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion
            }
        }
        #endregion
        #region ACCIONES PARA PROCESO 7 - PEDIDOS MOSTRADOR Y MOSTRADOR CP
        private void Proceso7_EV(int _ID, String _ClaveMenu, int _referencia, string _ClaveAreaSiguiente)
        {
            String _claveCliente = this.dgvResult.CurrentRow.Cells["Clave"].Value.ToString();
            switch (_ClaveMenu)
            {
                //*******************************************************************************************/
                //ACTIVIDAD 1. CORRECCIÓN DE PEDIDO
                //*******************************************************************************************/
                #region 1. CORRECCIÓN DE PEDIDO
                case "1_7_EV_1": // Buscador de cliente
                    _claveCliente = this.dgvResult.CurrentRow.Cells["CLAVE"].Value.ToString();
                    frmFindClie frmFindClie = new frmFindClie(_claveCliente, "frmControlPedidos");
                    frmFindClie.ShowDialog();
                    this.btnRefresh_Click(null, null);
                    break;
                case "1_7_EV_2": // Enviar pedido autorización
                    //SE VALIDA QUE EL PEDIDO HAYA SIDO PASADO A VENTAS
                    if (this.dgvResult.CurrentRow.Cells["ESTATUSPEDIDO"].Value.ToString() != "I")
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
                    string formaPago = this.dgvResult.CurrentRow.Cells["FormaPago"].Value.ToString();
                    Boolean aplicaCredito = true;
                    String areaSiguienteAsignada = "";
                    switch (formaPago)
                    {
                        case "01":
                        case "04":
                        case "28":
                            aplicaCredito = false;
                            break;
                        default:
                            aplicaCredito = true;
                            break;
                    }
                    _res = this.setAutorizacionPedido(_referencia, "EV", Globales.UsuarioActual.UsuarioUsuario);
                    _res = this.setLineaTiempo(_ID, aplicaCredito ? _ClaveAreaSiguiente.Split('|')[0].Split('_')[2] : _ClaveAreaSiguiente.Split('|')[1].Split('_')[2], "A", int.Parse(aplicaCredito ? _ClaveAreaSiguiente.Split('|')[0].Split('_')[0] : _ClaveAreaSiguiente.Split('|')[1].Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoMostrador, _referencia, _claveCliente);
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
                //ACCIONES GENERALES
                //*******************************************************************************************/
                #region CANCELAR
                //*******************************************************************************************/
                //CANCELACION
                //*******************************************************************************************/
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                #region EXPEDIENTE DIGITAL
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion
            }
        }
        private void Proceso7_GV(int _ID, String _ClaveMenu, int _referencia, string _ClaveAreaSiguiente)
        {
            String _claveCliente = this.dgvResult.CurrentRow.Cells["Clave"].Value.ToString();
            Enumerados.TipoPedido _tipoPedido = this.dgvResult.CurrentRow.Cells["TipoPedido"].Value.ToString() == "MS" ? Enumerados.TipoPedido.PedidoMOS : (this.dgvResult.CurrentRow.Cells["TipoPedido"].Value.ToString() == "EC" ? Enumerados.TipoPedido.PedidoEC : Enumerados.TipoPedido.PedidoMOSCP);
            switch (_ClaveMenu)
            {
                //*******************************************************************************************/
                //ACTIVIDAD 2. AUTORIZACIÓN DE PEDIDOS
                //*******************************************************************************************/
                #region 2. AUTORIZACIÓN DE PEDIDOS
                case "2_7_GV_1": // Ver Pedido
                    /* QUITAMOS IMPRESION NORMAL
                    SIP.Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, Enumerados.TipoPedido.Pedido);
                     * */
                    Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.OrdenTrabajo, _claveCliente, 0, _referencia, _tipoPedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    if (frmReportes.pedidoTambienImprimeOT)
                    {
                        frmReportes.Show();
                    }
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, _tipoPedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    frmReportes.Show();
                    break;
                case "2_7_GV_2": // Rechazar Pedido
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
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "R", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoMostrador, _referencia, _claveCliente);
                    // CANCELAMOS LA AUTORIZACION PREVIA
                    _res = this.setCancelaAutorizacionPedido(_referencia, "GV", Globales.UsuarioActual.UsuarioUsuario, "EV");
                    if (_res == "")
                    {
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "2_7_GV_3": // Autorizar Pedido
                    // CREAMOS AUTORIZACION POR GV
                    _res = setAutorizacionPedido(_referencia, "GV", Globales.UsuarioActual.UsuarioUsuario, true);
                    _res = setAutorizacionPedido(_referencia, "FA", Globales.UsuarioActual.UsuarioUsuario, true);
                    //MOSTRAMOS EL PEDIDO CON LA FIRMA
                    _claveCliente = this.dgvResult.CurrentRow.Cells["CLAVE"].Value.ToString();
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, _tipoPedido, "frmControlPedidos", true, false);
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
                        int pedidoFacturar = this.dgvResult.CurrentRow.Cells["Origen"].Value.ToString() == "N/A" ? _referencia : int.Parse(this.dgvResult.CurrentRow.Cells["Origen"].Value.ToString());
                        if (pedidoFacturar != _referencia)
                        {
                            // si el pedido no ha sido facturado, facturamos el pedido original
                            // en caso de que ya haya sido facturado el pedido original, unicamente se procesa el pedido en flujo de control pedidos
                            if (CargaPedidosSAE.ValidaPedidoFacturado(pedidoFacturar, this.dgvResult.CurrentRow.Cells["TipoPedido"].Value.ToString()))
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
                                CargaPedidosSAE.CargaPedidoEnSae(pedidoFacturar, comision, 16, ref ex, this.dgvResult.CurrentRow.Cells["TipoPedido"].Value.ToString());
                            }
                        }
                        else
                        {
                            // ENVIAMOS EL PEDIDO ACTUAL
                            PED_MSTR pedido_imprimir = new PED_MSTR();
                            DataTable datos_pedido = new DataTable();
                            datos_pedido = pedido_imprimir.ConsultaImprimir(pedidoFacturar);
                            comision = datos_pedido.Rows[0]["COMISION"] == null ? 0 : Convert.ToDecimal(datos_pedido.Rows[0]["COMISION"]);
                            CargaPedidosSAE.CargaPedidoEnSae(pedidoFacturar, comision, 16, ref ex, this.dgvResult.CurrentRow.Cells["TipoPedido"].Value.ToString());
                        }
                        // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                        _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoMostrador, _referencia, _claveCliente);
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
                //*******************************************************************************************/
                //ACCIONES GENERALES
                //*******************************************************************************************/
                #region CANCELAR
                //*******************************************************************************************/
                //CANCELACION
                //*******************************************************************************************/
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                #region EXPEDIENTE DIGITALs
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion
            }
        }
        private void Proceso7_CR(int _ID, String _ClaveMenu, int _referencia, string _ClaveAreaSiguiente)
        {
            String _claveCliente = this.dgvResult.CurrentRow.Cells["Clave"].Value.ToString();
            Enumerados.TipoPedido _tipoPedido = this.dgvResult.CurrentRow.Cells["TipoPedido"].Value.ToString() == "MS" ? Enumerados.TipoPedido.PedidoMOS : (this.dgvResult.CurrentRow.Cells["TipoPedido"].Value.ToString() == "EC" ? Enumerados.TipoPedido.PedidoEC : Enumerados.TipoPedido.PedidoMOSCP);
            switch (_ClaveMenu)
            {
                //*******************************************************************************************/
                //ACTIVIDAD 3. VERIFICAR CRÉDITO
                //*******************************************************************************************/
                #region 3. VERIFICAR CRÉDITO
                case "3_7_CR_1": // Autorizar Línea de Crédito
                    //7.1 Autorizar linea de crédito
                    // CREAMOS AUTORIZACION POR CR
                    _res = setAutorizacionPedido(_referencia, "GV", Globales.UsuarioActual.UsuarioUsuario, true);
                    _res = setAutorizacionPedido(_referencia, "FA", Globales.UsuarioActual.UsuarioUsuario, true);
                    _res = setAutorizacionPedido(_referencia, "CR", Globales.UsuarioActual.UsuarioUsuario, true);
                    //MOSTRAMOS EL PEDIDO CON LA FIRMA
                    _claveCliente = this.dgvResult.CurrentRow.Cells["CLAVE"].Value.ToString();
                    Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, _tipoPedido, "frmControlPedidos", true, false);
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
                        // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                        _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoMostrador, _referencia, _claveCliente);
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
                        // CREMAMOS EL REGISTRO EN UPPEDIDOS
                        uppedidosModif = new UPPEDIDOS();
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
                        _res = setCancelaAutorizacionPedido(_referencia, "CR", Globales.UsuarioActual.UsuarioUsuario, "GV");
                    }
                    break;
                case "3_7_CR_2": // Rechazar Línea de Crédito
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

                    // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "R", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoMostrador, _referencia, _claveCliente);
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
                case "3_7_CR_3": // Buscador de cliente
                    frmFindClie frmFindClie = new frmFindClie(_claveCliente);
                    frmFindClie.Show();
                    break;
                case "3_7_CR_4": // Estado de cuenta
                    frmFindClie = new frmFindClie(_claveCliente, "frmControlPedidos", "EstadoCuenta");
                    frmFindClie.Show();
                    break;
                #endregion
                //*******************************************************************************************/
                //ACCIONES GENERALES
                //*******************************************************************************************/
                #region CANCELAR
                //*******************************************************************************************/
                //CANCELACION
                //*******************************************************************************************/
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                #region EXPEDIENTE DIGITAL
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion
            }
        }
        private void Proceso7_CM(int _ID, String _ClaveMenu, int _referencia, string _ClaveAreaSiguiente)
        {
            String _claveCliente = this.dgvResult.CurrentRow.Cells["Clave"].Value.ToString();
            Enumerados.TipoPedido _tipoPedido = this.dgvResult.CurrentRow.Cells["TipoPedido"].Value.ToString() == "MS" ? Enumerados.TipoPedido.PedidoMOS : (this.dgvResult.CurrentRow.Cells["TipoPedido"].Value.ToString() == "EC" ? Enumerados.TipoPedido.PedidoEC : Enumerados.TipoPedido.PedidoMOSCP);
            switch (_ClaveMenu)
            {
                //*******************************************************************************************/
                //ACTIVIDAD 4. IMPRESIÓN DE PEDIDO
                //*******************************************************************************************/
                #region 4. IMPRESIÓN DE PEDIDO
                case "4_7_CM_1": // Imprimir Pedido
                    Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, _tipoPedido, "frmControlPedidos", true, true);
                    frmReportes.enVentas = true;
                    frmReportes.ShowDialog();
                    if (frmReportes.pedidoEstatusModificado)
                    {
                        // CREMAMOS EL REGISTRO EN UPPEDIDOS
                        UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                        uppedidosModif.PEDIDO = _referencia;
                        uppedidosModif.F_IMPRESION = DateTime.Now;
                        uppedidosModif.Modificar(uppedidosModif, "Impresión Firmas - Control Pedidos");
                        // CAPTURAMOS DE FORMA AUTOMÁTICA EL PEDIDO EN SAE
                        // obtenemos la comision del pedido
                        decimal comision = 0;
                        int pedidoFacturar = this.dgvResult.CurrentRow.Cells["Origen"].Value.ToString() == "N/A" ? _referencia : int.Parse(this.dgvResult.CurrentRow.Cells["Origen"].Value.ToString());
                        if (pedidoFacturar != _referencia)
                        {
                            // si el pedido no ha sido facturado, facturamos el pedido original
                            // en caso de que ya haya sido facturado el pedido original, unicamente se procesa el pedido en flujo de control pedidos
                            if (CargaPedidosSAE.ValidaPedidoFacturado(pedidoFacturar, this.dgvResult.CurrentRow.Cells["TipoPedido"].Value.ToString()))
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
                                CargaPedidosSAE.CargaPedidoEnSae(pedidoFacturar, comision, 16, ref ex, this.dgvResult.CurrentRow.Cells["TipoPedido"].Value.ToString());
                            }
                        }
                        else
                        {
                            // ENVIAMOS EL PEDIDO ACTUAL
                            PED_MSTR pedido_imprimir = new PED_MSTR();
                            DataTable datos_pedido = new DataTable();
                            datos_pedido = pedido_imprimir.ConsultaImprimir(pedidoFacturar);
                            comision = datos_pedido.Rows[0]["COMISION"] == null ? 0 : Convert.ToDecimal(datos_pedido.Rows[0]["COMISION"]);
                            CargaPedidosSAE.CargaPedidoEnSae(pedidoFacturar, comision, 16, ref ex, this.dgvResult.CurrentRow.Cells["TipoPedido"].Value.ToString());
                        }
                        // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                        if (_tipoPedido == Enumerados.TipoPedido.PedidoMOSCP)
                        {
                            _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('|')[0].Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('|')[0].Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoMostrador, _referencia, _claveCliente);
                        }
                        else
                        {
                            Boolean _requiereEnvio = false;
                            if (!_requiereEnvio)
                            {
                                ControlPedidos.setFinPedido(_referencia);
                            }
                            else
                            {
                                _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('|')[1].Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('|')[1].Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoMostrador, _referencia, _claveCliente);
                            }
                        }

                        if (_res == "")
                        {
                            MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.reload = true;
                        }
                        else
                            MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                #endregion
                //*******************************************************************************************/
                //ACCIONES GENERALES
                //*******************************************************************************************/
                #region CANCELAR
                //*******************************************************************************************/
                //CANCELACION
                //*******************************************************************************************/
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                #region EXPEDIENTE DIGITAL
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion
            }
        }
        private void Proceso7_PL(int _ID, String _ClaveMenu, int _referencia, string _ClaveAreaSiguiente)
        {
            String _claveCliente = this.dgvResult.CurrentRow.Cells["Clave"].Value.ToString();
            Enumerados.TipoPedido _tipoPedido = this.dgvResult.CurrentRow.Cells["TipoPedido"].Value.ToString() == "MS" ? Enumerados.TipoPedido.PedidoMOS : (this.dgvResult.CurrentRow.Cells["TipoPedido"].Value.ToString() == "EC" ? Enumerados.TipoPedido.PedidoEC : Enumerados.TipoPedido.PedidoMOSCP);
            switch (_ClaveMenu)
            {
                //*******************************************************************************************/
                //ACTIVIDAD 5. FECHA DE ENTREGA
                //*******************************************************************************************/
                #region 5. FECHA DE ENTREGA
                case "5_7_PL_1": // Ingresar fecha de entrega
                    frmUpPedidos frmUpPedidos = new frmUpPedidos(_claveCliente, _referencia);
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
                        // FIN DEL PROCESO
                        ControlPedidos.setFinPedido(_referencia);
                        if (_res == "")
                        {
                            MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.reload = true;
                        }
                        else
                            MessageBox.Show("Error al procesar. Descripción: " + _res, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case "5_7_PL_2": // Ver Pedido
                    Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.OrdenTrabajo, _claveCliente, 0, _referencia, _tipoPedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    if (frmReportes.pedidoTambienImprimeOT)
                    {
                        frmReportes.Show();
                    }
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, _claveCliente, 0, _referencia, _tipoPedido, "frmControlPedidos", true, false);
                    frmReportes.enVentas = true;
                    frmReportes.Show();
                    break;
                #endregion
                //*******************************************************************************************/
                //ACCIONES GENERALES
                //*******************************************************************************************/
                #region CANCELAR
                //*******************************************************************************************/
                //CANCELACION
                //*******************************************************************************************/
                case "CANCELAR":
                    if (MessageBox.Show("¿Está seguro que desa cancelar la solicitud?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        CancelaPedido.AplicaCancelacionPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }

                    break;
                #endregion
                #region EXPEDIENTE DIGITALs
                case "EXPEDIENTEDIGITAL":
                    frmDocumentosElectronicos frmDocumentosElectronicos = new frmDocumentosElectronicos(_referencia);
                    frmDocumentosElectronicos.Show();
                    break;
                #endregion
            }
        }
        #endregion
        #region ACCIONES PARA PROCESO 8 - REQUISICIONES DE MOSTRADOR
        private void Proceso8_SU(int _ID, String _ClaveMenu, int _referencia, string _ClaveAreaSiguiente)
        {
            switch (_ClaveMenu)
            {
                //*******************************************************************************************/
                //ACTIVIDAD 2. IMPRESION
                //*******************************************************************************************/
                #region 2. IMPRESION
                case "2_8_SU_1":
                    Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.RequisicionMostrador, _referencia);
                    frmReportes.ShowDialog();
                    if (frmReportes.procesado)
                    {
                        // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                        //_res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.RequisicionMostrador, _referencia, "");
                        // FIN PROCESO
                        ControlPedidos.setFinPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    break;
                #endregion
                //*******************************************************************************************/
                //ACTIVIDAD 3. TRANSFERENCIA
                //*******************************************************************************************/
                #region 3. TRANSFERENCIA
                case "3_8_SU_1":

                    break;
                case "3_8_SU_2":
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.RequisicionMostrador, _referencia);
                    frmReportes.ShowDialog();
                    break;
                #endregion
            }
        }
        private void Proceso8_CA(int _ID, String _ClaveMenu, int _referencia, string _ClaveAreaSiguiente)
        {
            switch (_ClaveMenu)
            {
                //*******************************************************************************************/
                //ACTIVIDAD 4. IMPRESION
                //*******************************************************************************************/
                #region 4. IMPRESION
                case "4_8_CA_1":
                    Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.RequisicionMostrador, _referencia);
                    frmReportes.ShowDialog();
                    if (frmReportes.procesado)
                    {
                        // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                        // _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.RequisicionMostrador, _referencia, "");
                        ControlPedidos.setFinPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    break;
                #endregion
                //*******************************************************************************************/
                //ACTIVIDAD 5. ENVIO A OP
                //*******************************************************************************************/
                #region 5. ENVIO A OP
                case "5_8_CA_1":  // Ver Requisicion
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.RequisicionMostrador, _referencia);
                    frmReportes.enVentas = true;
                    frmReportes.ShowDialog();
                    break;
                case "5_8_CA_2":  // Programar OP
                    frmOrdProduccionMasiva frmOrdProduccionMasiva = new frmOrdProduccionMasiva(_referencia, true, frmOrdProduccionMasiva.TipoRequisicionEnum.Requisicion);
                    frmOrdProduccionMasiva.ShowDialog();

                    if (frmOrdProduccionMasiva.StatusProceso)
                    {
                        // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                        _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.RequisicionMostrador, _referencia, "");
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    else
                        MessageBox.Show("No se puede continuar con la solicitud.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    break;
                    // ENVIAMOS REQUISICIÓN A LA SIGUIENTE AREA
                    _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.RequisicionMostrador, _referencia, "");
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
                //ACTIVIDAD 6. IMPRESION
                //*******************************************************************************************/
                #region 4. IMPRESION
                case "6_8_CA_1":
                     frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.RequisicionMostrador, _referencia);
                    frmReportes.ShowDialog();
                    if (frmReportes.procesado)
                    {
                        // ENVIAMOS PEDIDO A LA SIGUIENTE AREA
                        // _res = this.setLineaTiempo(_ID, _ClaveAreaSiguiente.Split('_')[2], "A", int.Parse(_ClaveAreaSiguiente.Split('_')[0]), _observaciones, Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.RequisicionMostrador, _referencia, "");
                        ControlPedidos.setFinPedido(_referencia);
                        MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.reload = true;
                    }
                    break;
                #endregion
            }
        }
        #endregion
        #region METODOS LINEA DE TIEMPO
        private String setAltaLineaTiempo(int _id, String _ClaveArea, string _Estatus, int _OrdenAgrupador, string _Observaciones, string _usuario, int _cveTipoProceso, int _referenciaProceso, String _Cliente)
        {
            Exception ex = null;
            ControlPedidos.setAltaLineaTiempoPedido(_id, _ClaveArea, _Estatus, _OrdenAgrupador, _Observaciones, _usuario, _cveTipoProceso, _referenciaProceso, _Cliente, ref ex);
            if (ex == null)
                return "";
            else
                return ex.Message;
        }
        private String setLineaTiempo(int _id, String _ClaveArea, string _Estatus, int _OrdenAgrupador, string _Observaciones, string _usuario, int _cveTipoProceso, int _referenciaProceso, String _Cliente)
        {
            ex = null;
            ControlPedidos.setLineaTiempoPedido(_id, _ClaveArea, _Estatus, _OrdenAgrupador, _Observaciones, _usuario, _cveTipoProceso, _referenciaProceso, _Cliente, ref ex);
            if (ex == null)
                return "";
            else
                return ex.Message;
        }
        private String setCancelaAutorizacion(int _id, String _ClaveArea, String _Usuario, string _ClaveAreaInicial)
        {
            ex = null;
            ControlPedidos.setCancelaAutorizacionPedido(_id, _ClaveArea, _Usuario, _ClaveAreaInicial, ref ex);
            if (ex == null)
                return "";
            else
                return ex.Message;
        }
        private DataTable getHistorico(int _id)
        {
            DataTable dt = new DataTable();
            dt = ControlFlujo.GetHistoricoFlujo(_id);
            return dt;
        }
        private DataTable getObservaciones(int _id)
        {
            DataTable dt = new DataTable();
            dt = ControlFlujo.GetObservacionesFlujo(_id);
            return dt;
        }
        #endregion
        #region METODOS PARA PEDIDOS
        private String setCancelaAutorizacionPedido(int _Pedido, String _ClaveArea, String _Usuario, string _ClaveAreaInicial)
        {
            ex = null;
            ControlPedidos.setCancelaAutorizacionPedido(_Pedido, _ClaveArea, _Usuario, _ClaveAreaInicial, ref ex);
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
        #endregion
        #region METODOS PARA SOLICITUDES
        private List<int> getSolicitudesSeleccionadas(ref String _error)
        {
            List<String> Cliente = new List<string> { };
            List<int> Solicitudes = new List<int> { };

            foreach (DataGridViewRow _dr in this.dgvResult.Rows)
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
        #endregion
    }
}
