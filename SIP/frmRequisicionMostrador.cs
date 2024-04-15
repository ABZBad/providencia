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
    public partial class frmRequisicionMostrador : Form
    {

        #region VARIABLES Y CONSTRUCTORES

        class GridResumen
        {
            public string modelo { get; set; }
            public string tallas { get; set; }
            public int total { get; set; }
        }

        enum RecorridoTablasTallas
        {
            Guardar,
            CalcularCantidad
        }

        private Precarga precarga;
        BackgroundWorker backgroundWorkerModelos = new BackgroundWorker();
        BackgroundWorker backgroundWorkerGuardar = new BackgroundWorker();
        string cliente = "";
        string descripcion = "";
        string nombre_vendedor = "";
        DataSet tablasTallas;
        DataTable existencias = new DataTable();
        bool PrecioEspecial = false;
        double precioLista = 0;
        string agrupador;
        int totalPrendas = 0;
        int tipoOrigenDestino = 0;
        bool generaOP = false;
        DataTable dtValidacionExistencias = new DataTable();
        private delegate void DelAsignarValoresModeloAcontroles(DataTable AsignarValoresModelo);
        RequisicionMostrador oRequisición = new RequisicionMostrador();
        RequisicionMostrador oRequisiciónFaltante = new RequisicionMostrador();

        public frmRequisicionMostrador()
        {
            InitializeComponent();
            precarga = new Precarga(this);
            backgroundWorkerModelos.DoWork += backgroundWorkerModelos_DoWork;
            backgroundWorkerModelos.RunWorkerCompleted += backgroundWorkerModelos_RunWorkerCompleted;
            backgroundWorkerGuardar.DoWork += backgroundWorkerGuardar_DoWork;
            backgroundWorkerGuardar.RunWorkerCompleted += backgroundWorkerGuardar_RunWorkerCompleted;
            txtModelo.Focus();
            cmbOrigenDestino.SelectedIndex = tipoOrigenDestino;
            oRequisición = new RequisicionMostrador();
        }

        #endregion
        #region EVENTOS
        private void txtModelo_TextChanged(object sender, EventArgs e)
        {
            if (txtModelo.Text.Length == 8 || txtModelo.Text.ToUpper() == "LOGISTICA")
            {
                txtModelo.Enabled = false;
                lblProcesando.Visible = true;
                this.precarga.MostrarEspera();
                backgroundWorkerModelos.RunWorkerAsync();
            }
            else
            {
                LimpiaValores();
                HabilitaControlesPartidas(false);
            }
        }

        /*******************************************************************************************/
        /*EVENTOS GRID 1*/
        /*******************************************************************************************/
        private void dgViewTallas1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            CalculaTotalPrendas();
        }
        private void dgViewTallas1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.Width = 58;
        }
        private void dgViewTallas1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Debe capturar el formato numérico correcto", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private void dgViewTallas1_Leave(object sender, EventArgs e)
        {
            DataGridViewCell cell = dgViewTallas1.CurrentCell;
            if (cell != null)
            {
                cell.Selected = false;
            }
        }
        /*******************************************************************************************/
        /*EVENTOS GRID 2*/
        /*******************************************************************************************/
        private void dgViewTallas2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            CalculaTotalPrendas();
        }
        private void dgViewTallas2_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.Width = 58;
        }
        private void dgViewTallas2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Debe capturar el formato numérico correcto", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private void dgViewTallas2_Leave(object sender, EventArgs e)
        {
            DataGridViewCell cell = dgViewTallas2.CurrentCell;
            if (cell != null)
            {
                cell.Selected = false;
            }
        }
        /*******************************************************************************************/
        /*EVENTOS GRID 3*/
        /*******************************************************************************************/
        private void dgViewTallas3_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            CalculaTotalPrendas();
        }
        private void dgViewTallas3_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.Width = 58;
        }
        private void dgViewTallas3_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Debe capturar el formato numérico correcto", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private void dgViewTallas3_Leave(object sender, EventArgs e)
        {
            DataGridViewCell cell = dgViewTallas3.CurrentCell;
            if (cell != null)
            {
                cell.Selected = false;
            }
        }
        /*******************************************************************************************/
        /*GENERALES3*/
        /*******************************************************************************************/
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            this.ResetModelo();
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            ProcesaPrendas(RecorridoTablasTallas.Guardar, 0);
            this.ResetModelo();
            txtModelo.Focus();
            this.HabilitaGuardarRequisicion();
        }
        private void dgvTallas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvTallas.Columns[e.ColumnIndex].Name == "resumen_accion")
            {
                this.oRequisición.detalle.RemoveAt(e.RowIndex);
                this.LlenaGridResumen();
                this.HabilitaGuardarRequisicion();
            }
        }
        private void frmRequisicionMostrador_Load(object sender, EventArgs e)
        {
            txtModelo.Focus();
            this.ActiveControl = txtModelo;
        }
        private void txtOrigen_TextChanged(object sender, EventArgs e)
        {
            this.HabilitaGuardarRequisicion();
        }
        private void txtDestino_TextChanged(object sender, EventArgs e)
        {
            this.HabilitaGuardarRequisicion();
        }
        private void txtOrigen_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void txtDestino_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            this.precarga.MostrarEspera();
            this.oRequisición.origen = ObtenAlmacen(0);
            this.oRequisición.destino = ObtenAlmacen(1);
            this.backgroundWorkerGuardar.RunWorkerAsync();
        }
        private void cmbOrigenDestino_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbOrigenDestino.SelectedIndex >= 0)
            {
                this.tipoOrigenDestino = cmbOrigenDestino.SelectedIndex;
                this.ResetModelo();
            }
        }
        #endregion
        #region WORKERS
        private void backgroundWorkerModelos_DoWork(object sender, DoWorkEventArgs e)
        {
            this.precarga.AsignastatusProceso("Cargando tallas...");
            ProcesoBusquedaModelo();
        }
        private void backgroundWorkerModelos_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            txtModelo.Enabled = true;
            lblProcesando.Visible = false;
            btnAgregar.Enabled = true;
            dgViewTallas1.Focus();
            this.precarga.RemoverEspera();
            this.HabilitaGuardarRequisicion();
        }

        private void backgroundWorkerGuardar_DoWork(object sender, DoWorkEventArgs e)
        {
            this.precarga.AsignastatusProceso("Guardando requisición...");
            this.oRequisición.AltaRequisicion(Globales.UsuarioActual.UsuarioUsuario);
            // Una vez creada la requisición validamos si se debe de generar OP
            // 1. Debe ser de almacen 3 -> 1
            // 2. Debe de haber faltante


            dtValidacionExistencias = this.oRequisición.ValidaExistenciasTotalesRequisicion(this.tipoOrigenDestino);
            if (dtValidacionExistencias.Rows.Count > 0)
            {
                if (dtValidacionExistencias.Select("(EXISTENCIAS - CANTIDAD) < 0").Any())
                {
                    this.generaOP = true;
                }
            }


        }
        private void backgroundWorkerGuardar_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            List<string> RequisicionesId = new List<string>();
            RequisicionesId.Add(oRequisición.idRequisicion.ToString());
            this.precarga.AsignastatusProceso("Generando flujo SIVO...");
            if (!this.generaOP)
            {
                // ALTA FLUJO DE REQUISICION 100% EXISTENCIA Y TIPO DE ORIGEN/DESTINO
                this.AltaFlujoSIVO(oRequisición.idRequisicion, this.tipoOrigenDestino, false);

            }
            else
            {
                this.CreaRequisicionFaltante(this.oRequisición); // GENERAMOS FALTANTE CON BASE A REQUISICION ORIGEN
                RequisicionesId.Add(oRequisiciónFaltante.idRequisicion.ToString());
                this.AltaFlujoSIVO(oRequisición.idRequisicion, this.tipoOrigenDestino, false);
                this.AltaFlujoSIVO(oRequisiciónFaltante.idRequisicion, this.tipoOrigenDestino, true);

            }

            MessageBox.Show("Se ha procesado la requisición con id: " + String.Join(",", RequisicionesId), "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LimpiaValores();
            this.precarga.RemoverEspera();
            this.oRequisición = new RequisicionMostrador();
            LlenaGridResumen();
        }
        #endregion
        #region METODOS PRIVADOS
        private void LimpiaValores()
        {
            lblModeloDescripcion.Text = "";
            //lblPrecioConsolidado.Text = "";
            //totalPrendas = 0;
            agrupador = "";
            precioLista = 0;
            descripcion = "";
            btnAgregar.Enabled = false;
        }
        private void ResetModelo()
        {
            txtModelo.Text = "";
            lblModeloDescripcion.Text = "";
            lblTotalPrendas.Text = "0";
        }
        private void ProcesoBusquedaModelo()
        {
            DataTable tiposProductos = new DataTable();
            //VERIFICAMOS SI EL PRODUCTO TIENE PRECIO ESPECIAL PARA ESE CLIENTE
            tiposProductos = ulp_bl.GestionCorporativos.GetPreciosEspecialesCliente(cliente.Trim(), txtModelo.Text.Trim());
            PrecioEspecial = true;
            if (tiposProductos == null)
            {
                //DE LO CONTRARIO BUSCAMOS EL PRECIO EN LA LISTA GENERAL
                vw_Inventario inventario = new vw_Inventario();
                tiposProductos = inventario.Consultar(txtModelo.Text);
                PrecioEspecial = false;
            }
            // OBTENEMOS EXISTENCIAS
            vw_Inventario inventario2 = new vw_Inventario();
            this.existencias = inventario2.GetExistenciasPorModeloTipoProceso(txtModelo.Text, this.tipoOrigenDestino);
            AsignarValoresModeloAcontroles(tiposProductos);
        }
        public void HabilitaControlesPartidas(bool Habilita)
        {
            dgViewTallas1.Visible = Habilita;
            dgViewTallas2.Visible = Habilita;
            dgViewTallas3.Visible = Habilita;
            btnGuardar.Enabled = Habilita;
        }
        private void AsignarValoresModeloAcontroles(DataTable datosModelo)
        {
            if (this.InvokeRequired)
            {
                DelAsignarValoresModeloAcontroles delCargarValoresModelo = new DelAsignarValoresModeloAcontroles(AsignarValoresModeloAcontroles);
                this.Invoke(delCargarValoresModelo, new object[] { datosModelo });
            }
            else
            {
                if (datosModelo.Rows.Count > 0)
                {
                    List<PreciosXModelo> lstPreciosXModelo = vw_Inventario.TotalDePreciosEnModelo(datosModelo);

                    double.TryParse(datosModelo.Rows[0]["Precio1"].ToString(), out precioLista);
                    if (datosModelo.Rows[0]["Precio1"] == System.DBNull.Value)
                    {
                        precioLista = 0;
                    }
                    else
                    {
                        precioLista = double.Parse(datosModelo.Rows[0]["Precio1"].ToString());
                    }

                    int posicion = datosModelo.Rows[0]["DESCR"].ToString().IndexOf("TALLA");
                    if (posicion < 1)
                    {
                        lblModeloDescripcion.Text = datosModelo.Rows[0]["DESCR"].ToString();
                    }
                    else
                    {
                        lblModeloDescripcion.Text = datosModelo.Rows[0]["DESCR"].ToString().Substring(0, posicion);
                    }

                    if (datosModelo.Rows[0]["DESCR"].ToString().Length > 50)
                    {
                        descripcion = datosModelo.Rows[0]["DESCR"].ToString().Substring(0, 50);
                    }
                    else
                    {
                        descripcion = datosModelo.Rows[0]["DESCR"].ToString();
                    }
                    HabilitaControlesPartidas(true);
                    llenaDatosTallas(datosModelo);
                }
                else
                {
                    LimpiaValores();
                    HabilitaControlesPartidas(false);
                }
            }
        }
        private void llenaDatosTallas(DataTable tallas)
        {
            int tot = 0; ;
            tablasTallas = new DataSet();
            //int numeroTablas = ((tallas.Rows.Count - 1) / 10);
            int numeroTablas = ulp_bl.AddPartidasPedi.NumeroDeTablasSegunTotalTallas(tallas);
            dgViewTallas1.DataSource = null;
            dgViewTallas2.DataSource = null;
            dgViewTallas2.Enabled = false;
            dgViewTallas3.DataSource = null;
            dgViewTallas3.Enabled = false;

            for (int i = 0; i < numeroTablas; i++)
            {

                string nombreTabla = "T" + i.ToString();
                tablasTallas.Tables.Add(nombreTabla);

                for (int y = 0; y < 14; y++)
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
                    columna.DefaultValue = 0;
                    columna.DataType = typeof(Int32);
                    columna.AllowDBNull = false;
                    tablasTallas.Tables[nombreTabla].Columns.Add(columna);
                    if (y == 13 || (tallas.Rows.Count - 1) == tot)
                    {
                        DataRow registroNuevo = tablasTallas.Tables[nombreTabla].NewRow();
                        tablasTallas.Tables[nombreTabla].Rows.Add(registroNuevo);

                        // agregamos existencias (se calculan con base a almacen 1+3+4-virtual)
                        registroNuevo = tablasTallas.Tables[nombreTabla].NewRow();
                        for (int z = 0; z < registroNuevo.ItemArray.Length; z++)
                        {
                            string filtro = String.Format("CVE_ART = '{0}' AND CVE_ALM = 1", txtModelo.Text + tablasTallas.Tables[nombreTabla].Columns[z].ColumnName.Replace("_", ""));
                            try
                            {
                                registroNuevo[z] = this.existencias.Select(filtro).FirstOrDefault()["EXIST"];
                            }
                            catch { registroNuevo[z] = 0; }
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
                    /*
                    dgViewTallas2.Rows[2].ReadOnly = true;
                    dgViewTallas2.Rows[2].DefaultCellStyle.BackColor = Color.LightGray;
                    dgViewTallas2.Rows[3].ReadOnly = true;
                    dgViewTallas2.Rows[3].DefaultCellStyle.BackColor = Color.LightGray;*/
                }
                if (i == 2)
                {
                    dgViewTallas3.Enabled = true;
                    dgViewTallas3.DataSource = tablasTallas.Tables[nombreTabla];
                    DataGridViewCell cell3 = dgViewTallas3.CurrentCell;
                    cell3.Selected = false;
                    dgViewTallas3.Rows[1].ReadOnly = true;
                    dgViewTallas3.Rows[1].DefaultCellStyle.BackColor = Color.LightGray;
                    /*
                    dgViewTallas3.Rows[2].ReadOnly = true;
                    dgViewTallas3.Rows[2].DefaultCellStyle.BackColor = Color.LightGray;
                    dgViewTallas3.Rows[3].ReadOnly = true;
                    dgViewTallas3.Rows[3].DefaultCellStyle.BackColor = Color.LightGray;
                    */
                }
            }
        }
        private void CalculaTotalPrendas()
        {
            ProcesaPrendas(RecorridoTablasTallas.CalcularCantidad, 0);
            lblTotalPrendas.Text = totalPrendas.ToString();
        }
        private void ProcesaPrendas(RecorridoTablasTallas tipoProceso, double descuentoArticulo)
        {
            RequisicionMostrador.DetalleRequisición oDetalle = new RequisicionMostrador.DetalleRequisición();

            if (tipoProceso == RecorridoTablasTallas.CalcularCantidad)
            {
                totalPrendas = 0;
            }

            if (tipoProceso == RecorridoTablasTallas.Guardar)
            {
                // VALIDAMOS SI YA EXISTE EL MODELO DE LO CONTRARIO LO CREAMOS
                oDetalle = new RequisicionMostrador.DetalleRequisición { modelo = txtModelo.Text.Trim(), tallas = new List<Dictionary<string, string>> { } };
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
                            //GuardarPed_Det(i, y, descuentoArticulo / 100);
                            oDetalle.tallas.Add(new Dictionary<string, string> { 
                            { "talla", tablasTallas.Tables[i].Columns[y].Caption }, 
                            { "cantidad", tablasTallas.Tables[i].Rows[0][y].ToString() } });
                        }
                        else
                        {
                            totalPrendas = totalPrendas + Convert.ToInt32(tablasTallas.Tables[i].Rows[0][y].ToString());
                        }
                    }
                }
            }

            if (tipoProceso == RecorridoTablasTallas.Guardar)
            {
                this.oRequisición.detalle.Add(oDetalle);
                LlenaGridResumen();
            }
        }
        private void LlenaGridResumen()
        {
            List<GridResumen> resumen = new List<GridResumen> { };
            foreach (RequisicionMostrador.DetalleRequisición _detalle in this.oRequisición.detalle)
            {
                var _stringDetalle = _detalle.tallas.Select(x => String.Format("Talla {0} = {1}", x["talla"], x["cantidad"])).ToArray();
                int total = _detalle.tallas.Select(x => int.Parse(x["cantidad"])).Sum();
                resumen.Add(new GridResumen { modelo = _detalle.modelo, tallas = String.Join(",", _stringDetalle), total = total });
            }

            dgvTallas.DataSource = resumen;
            dgvTallas.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            lblTotalPrendasRequisicion.Text = resumen.Select(x => x.total).Sum().ToString();
        }
        private void HabilitaGuardarRequisicion()
        {
            this.btnGuardar.Enabled = this.oRequisición.detalle.Count > 0;
        }
        private void AltaFlujoSIVO(int referencia, int tipo, bool requiereOP)
        {
            int _idProceso = int.Parse(ControlPedidos.getSiguienteIDProceso().Rows[0]["ID"].ToString());
            Exception ex = null;
            if (!requiereOP)
            {
                if (tipo == 0)
                {
                    ControlPedidos.setAltaLineaTiempoPedido(_idProceso, "CM", "A", 1, "", Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.RequisicionMostrador, referencia, "", ref ex);
                    ControlPedidos.setLineaTiempoPedido(_idProceso, "SU", "G", 2, "", Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.RequisicionMostrador, referencia, "", ref ex);
                }
                if (tipo == 1)
                {
                    ControlPedidos.setAltaLineaTiempoPedido(_idProceso, "CM", "A", 1, "", Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.RequisicionMostrador, referencia, "", ref ex);
                    ControlPedidos.setLineaTiempoPedido(_idProceso, "CA", "G", 4, "", Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.RequisicionMostrador, referencia, "", ref ex);
                }
            }
            else
            {
                ControlPedidos.setAltaLineaTiempoPedido(_idProceso, "CM", "1", 1, "", Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.RequisicionMostrador, referencia, "", ref ex);
                ControlPedidos.setLineaTiempoPedido(_idProceso, "CA", "G", 5, "", Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.RequisicionMostrador, referencia, "", ref ex);
            }
        }
        private int ObtenAlmacen(int tipo)
        {
            // tipo: 0 = origen, 1 = destino
            // Posicion 0: Del 4 al 3
            // Posicion 1: Del 4 al 3
            if (cmbOrigenDestino.SelectedIndex >= 0)
            {
                if (tipo == 0) //ORIGEN
                {
                    return cmbOrigenDestino.SelectedIndex == 0 ? 4 : 3;
                }
                else // DESTINO
                {
                    return cmbOrigenDestino.SelectedIndex == 0 ? 3 : 1;
                }

            }
            else { return 0; }
        }
        private void CreaRequisicionFaltante(RequisicionMostrador requisicion)
        {
            oRequisiciónFaltante = new RequisicionMostrador();
            // SELECCIONAMOS LOS PRODUCTOS DONDE NO TENEMOS EXISTENCIA TOTAL Y GENERAMOS UNA NUEVA OP

            oRequisiciónFaltante.fecha = requisicion.fecha;
            oRequisiciónFaltante.origen = requisicion.origen;
            oRequisiciónFaltante.destino = requisicion.destino;
            oRequisiciónFaltante.requiereOP = true;

            var dtNuevoDetalle = this.dtValidacionExistencias.Select("(EXISTENCIAS - CANTIDAD) < 0").ToList();
            foreach (var detalle in dtNuevoDetalle)
            {
                if (!oRequisiciónFaltante.detalle.Select(x => x.modelo).Distinct().Where(x => x.Contains(detalle["CVE_ART"].ToString().Substring(0, 8))).Any())
                {
                    RequisicionMostrador.DetalleRequisición oDetalleNuevo = new RequisicionMostrador.DetalleRequisición();
                    oDetalleNuevo.modelo = detalle["CVE_ART"].ToString().Substring(0, 8);
                    oDetalleNuevo.tallas.Add(new Dictionary<string, string> { 
                        { "talla", detalle["CVE_ART"].ToString().Substring(8,4)}, 
                        { "cantidad",  (int.Parse(detalle["CANTIDAD"].ToString()) - int.Parse(detalle["EXISTENCIAS"].ToString())).ToString()}});
                    oRequisiciónFaltante.detalle.Add(oDetalleNuevo);
                }
                else
                {
                    oRequisiciónFaltante.detalle.Where(x => x.modelo == detalle["CVE_ART"].ToString().Substring(0, 8)).FirstOrDefault().tallas.Add(new Dictionary<string, string> { 
                        { "talla", detalle["CVE_ART"].ToString().Substring(8,4)}, 
                        { "cantidad",  (int.Parse(detalle["CANTIDAD"].ToString()) - int.Parse(detalle["EXISTENCIAS"].ToString())).ToString()}});
                }
            }

            oRequisiciónFaltante.AltaRequisicionFaltante(requisicion.idRequisicion, Globales.UsuarioActual.UsuarioUsuario);
        }
        #endregion
    }
}