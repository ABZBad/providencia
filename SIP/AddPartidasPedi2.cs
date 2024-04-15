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
    public partial class AddPartidasPedi2 : Form
    {
        #region Declaración de variables
        AddPartidasPediLogoPreview frmLogoPreview = new AddPartidasPediLogoPreview();
        private delegate void DelAsignarValoresModeloAcontroles(DataTable AsignarValoresModelo);
        enum RecorridoTablasTallas
        {
            Guardar,
            CalcularCantidad
        }
        AutoCompleteStringCollection tipoEmpaque = new AutoCompleteStringCollection();
        Enumerados.TipoPedido tipoPartida;
        Reportes.frmReportes frmReportes;
        public bool pedidoEstatusModificado = false;
        int totalPrendas = 0;
        string agrupador;
        string cliente;
        int puntadas = 0;
        double precioLista = 0;
        string descripcion = "";
        string nombre_vendedor = "";
        DataSet tablasTallas;
        DataTable procesos = new DataTable();
        bool PrecioEspecial = false;
        bool esDAT = false;
        decimal formaPagoComision = 0;
        BackgroundWorker backgroundWorkerModelos = new BackgroundWorker();
        double descuento = 0;
        string frmOrigen = "";
        Boolean dividir = false;
        DataTable existencias = new DataTable();
        #endregion

        #region Funcionalidades y métodos generales
        public AddPartidasPedi2()
        {
            InitializeComponent();
            txtPedido.Text = "5876";
            cliente = "469";
            tipoPartida = Enumerados.TipoPedido.OrdenTrabajo;
            backgroundWorkerModelos.DoWork += backgroundWorkerModelos_DoWork;
            backgroundWorkerModelos.RunWorkerCompleted += backgroundWorkerModelos_RunWorkerCompleted;
            if (tipoPartida == Enumerados.TipoPedido.OrdenTrabajo)
            {
                txtPrecio.Visible = false;
                label2.Visible = false;
                lblPrecioConsolidado.Visible = false;
                label6.Visible = false;
            }
        }
        public AddPartidasPedi2(string idCliente, int idPedido, Enumerados.TipoPedido tipo, string vendedor, bool esDAT = false, decimal formaPagoComision = 0, string frmOrigen = "", bool dividir = false)
        {
            InitializeComponent();
            pedidoEstatusModificado = false;
            tipoPartida = tipo;
            cliente = idCliente;
            txtPedido.Text = idPedido.ToString();
            nombre_vendedor = vendedor;
            this.frmOrigen = frmOrigen;
            this.esDAT = esDAT;
            this.formaPagoComision = formaPagoComision;
            this.dividir = dividir;
            DataColumn[] elementosProcesos ={
                                               new DataColumn("NombreProceso",typeof(string)),
                                               new DataColumn("Que",typeof(string)),
                                               new DataColumn("Como",typeof(string)),
                                               new DataColumn("Donde",typeof(string)),
                                               new DataColumn("Precio",typeof(double))
                                           };
            procesos.Columns.AddRange(elementosProcesos);

            backgroundWorkerModelos.DoWork += backgroundWorkerModelos_DoWork;
            backgroundWorkerModelos.RunWorkerCompleted += backgroundWorkerModelos_RunWorkerCompleted;
            if (tipoPartida == Enumerados.TipoPedido.OrdenTrabajo)
            {
                txtPrecio.Visible = false;
                label2.Visible = false;
                this.Text = "Nueva Partida (OT) - Órden de Trabajo";
                lblPrecioConsolidado.Visible = false;
                label6.Visible = false;
            }
            this.HabilidaControlesDAT(this.esDAT);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ProcesaPrendas(RecorridoTablasTallas tipoProceso, double descuentoArticulo)
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
                            GuardarPed_Det(i, y, descuentoArticulo / 100);
                        }
                        else
                        {
                            totalPrendas = totalPrendas + Convert.ToInt32(tablasTallas.Tables[i].Rows[0][y].ToString());
                        }
                    }
                }
            }
        }
        private void ProcesaPrendasComision(RecorridoTablasTallas tipoProceso, double descuentoArticulo, decimal montoComision)
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
                            GuardarPed_DetComision("+DIFEREN", montoComision, i, y, 0);
                        }
                        else
                        {
                            totalPrendas = totalPrendas + Convert.ToInt32(tablasTallas.Tables[i].Rows[0][y].ToString());
                        }
                    }
                }
            }
        }
        #endregion

        #region Funcionalidades Cajas de texto
        private void txtModelo_TextChanged(object sender, EventArgs e)
        {
            if (txtModelo.Text.Length == 8 || txtModelo.Text.ToUpper() == "LOGISTICA")
            {
                txtModelo.Enabled = false;
                lblProcesando.Visible = true;
                backgroundWorkerModelos.RunWorkerAsync();
            }
            else
            {
                limpiaValores();
                HabilitaControlesProcesos(false);
                HabilitaControlesPartidas(false);
            }
        }
        private void txtProcesos_TextChanged(object sender, EventArgs e)
        {
            if (contenidoValidoTxtProcesos())
            {
                llenaDataTableProcesos();

                if (tipoPartida == Enumerados.TipoPedido.Pedido || tipoPartida == Enumerados.TipoPedido.PedidoEC || tipoPartida == Enumerados.TipoPedido.PedidoDAT || tipoPartida == Enumerados.TipoPedido.PedidoMOS || tipoPartida == Enumerados.TipoPedido.PedidoMOSCP)
                {
                    lblPrecioConsolidado.Text = DevuelvePrecioConsolidado().ToString();
                }
                DataGridViewCell cell = dgViewProcesos.CurrentCell;
                if (cell != null)
                {
                    cell.Selected = false;
                }
                HabilitaControlesProcesos(true);

            }
            else
            {
                HabilitaControlesProcesos(false);
                MessageBox.Show("El Proceso Capturado es inválido.\r\nPor favor intentelo nuevamente", "Error de captura", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            if (tipoPartida == Enumerados.TipoPedido.Pedido || tipoPartida == Enumerados.TipoPedido.PedidoEC || tipoPartida == Enumerados.TipoPedido.PedidoDAT || tipoPartida == Enumerados.TipoPedido.PedidoMOS || tipoPartida == Enumerados.TipoPedido.PedidoMOSCP)
            {
                lblPrecioConsolidado.Text = DevuelvePrecioConsolidado().ToString();
            }
        }
        #endregion

        #region Funcionalidades Grids
        private void dgViewProcesos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewTextBoxCell cell = (DataGridViewTextBoxCell)dgViewProcesos.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (e.ColumnIndex == 4)
            {
                if (cell.Value.ToString() == "")
                    cell.Value = 0;
                cell.Value = Convert.ToDecimal(DevuelveFormatoNumericoValidado(cell.Value.ToString()));
                lblPrecioConsolidado.Text = DevuelvePrecioConsolidado().ToString();
            }
        }

        private void dgViewProcesos_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void dgViewProcesos_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                DataGridViewTextBoxCell cell = (DataGridViewTextBoxCell)dgViewProcesos.Rows[e.RowIndex].Cells[0];
                if (e.ColumnIndex == 0)
                {
                    cell.Style.BackColor = System.Drawing.SystemColors.Control;
                    if (e.Value.ToString() != "Bordado" && e.Value.ToString() != "Estampado" && e.Value.ToString() != "Empaque" && e.Value.ToString() != "Embarque")
                    {
                        DataGridViewTextBoxCell cellAEditar = (DataGridViewTextBoxCell)dgViewProcesos.Rows[e.RowIndex].Cells["Como"];
                        cellAEditar.ReadOnly = true;
                        cellAEditar.Style.BackColor = System.Drawing.SystemColors.Control;
                        DataGridViewTextBoxCell cellDonde = (DataGridViewTextBoxCell)dgViewProcesos.Rows[e.RowIndex].Cells["Donde"];
                        cellDonde.ReadOnly = true;
                        cellDonde.Style.BackColor = System.Drawing.SystemColors.Control;
                    }
                    if (e.Value.ToString() == "Empaque")
                    {
                        DataGridViewTextBoxCell cellAEditar = (DataGridViewTextBoxCell)dgViewProcesos.Rows[e.RowIndex].Cells["Que"];
                        cellAEditar.ReadOnly = false;
                        cellAEditar.Style.BackColor = System.Drawing.SystemColors.Control;
                        DataGridViewTextBoxCell cellDonde = (DataGridViewTextBoxCell)dgViewProcesos.Rows[e.RowIndex].Cells["Donde"];
                        cellDonde.ReadOnly = false;
                        cellDonde.Style.BackColor = System.Drawing.SystemColors.Control;
                    }
                    if (e.Value.ToString() == "Embarque")
                    {
                        DataGridViewTextBoxCell cellAEditar = (DataGridViewTextBoxCell)dgViewProcesos.Rows[e.RowIndex].Cells["Que"];
                        cellAEditar.ReadOnly = true;
                        cellAEditar.Style.BackColor = System.Drawing.SystemColors.Control;
                        DataGridViewTextBoxCell cellComo = (DataGridViewTextBoxCell)dgViewProcesos.Rows[e.RowIndex].Cells["Como"];
                        cellComo.ReadOnly = true;
                        cellComo.Style.BackColor = System.Drawing.SystemColors.Control;
                        DataGridViewTextBoxCell cellDonde = (DataGridViewTextBoxCell)dgViewProcesos.Rows[e.RowIndex].Cells["Donde"];
                        cellDonde.ReadOnly = false;
                    }

                }
                if (e.ColumnIndex == 4 && tipoPartida == Enumerados.TipoPedido.OrdenTrabajo)
                {
                    DataGridViewTextBoxCell cellAEditar = (DataGridViewTextBoxCell)dgViewProcesos.Rows[e.RowIndex].Cells["Precio"];
                    cellAEditar.ReadOnly = true;
                    cellAEditar.Style.BackColor = System.Drawing.SystemColors.Control;
                }
            }

        }
        private void dgViewProcesos_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (tipoPartida == Enumerados.TipoPedido.Pedido || tipoPartida == Enumerados.TipoPedido.PedidoEC || tipoPartida == Enumerados.TipoPedido.PedidoDAT || tipoPartida == Enumerados.TipoPedido.PedidoMOS || tipoPartida == Enumerados.TipoPedido.PedidoMOSCP)
            {
                if (e.ColumnIndex == 2 && (dgViewProcesos.Rows[e.RowIndex].Cells[0].Value.ToString() == "Bordado" || dgViewProcesos.Rows[e.RowIndex].Cells[0].Value.ToString() == "Estampado"))
                {
                    if (dgViewProcesos.Rows[e.RowIndex].Cells[0].Value.ToString() != "Empaque")
                    {
                        DataTable imagenes = new DataTable();
                        IMAGENES logo = new IMAGENES();
                        imagenes = logo.Consultar(e.FormattedValue.ToString());

                        if (imagenes.Rows.Count > 0)
                        {
                            if (imagenes.Rows[0]["PUNTADAS"].ToString() != "")
                            {
                                puntadas = Convert.ToInt32(imagenes.Rows[0]["PUNTADAS"].ToString());
                                string cod_cliente = imagenes.Rows[0]["COD_CLIENTE"].ToString();
                                if (cod_cliente.Trim() != cliente.Trim())
                                {
                                    MessageBox.Show("Atención:\n\nEste Logotipo existe pero no es exclusivo de este cliente.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                        }
                        else
                        {

                            if (
                                (MessageBox.Show("El código que escribió no es válido. \n\n ¿Desea reintentar?",
                                    "Logotipo inválido", MessageBoxButtons.RetryCancel, MessageBoxIcon.Question)) ==
                                DialogResult.Retry)
                            {
                                // this.Close();
                                e.Cancel = true;

                            }
                            else
                            {
                                /*
                                dgViewProcesos.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "Cómo";
                                e.Cancel = false;
                                 * */
                                dgViewProcesos.CancelEdit();
                                dgViewProcesos.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "Cómo";
                            }
                        }
                    }
                    else
                    {

                    }

                }

                if (e.ColumnIndex == 3 && (dgViewProcesos.Rows[e.RowIndex].Cells[0].Value.ToString() == "Embarque"))
                {
                    // VALIDAMOS QUE LA DIRECCION CONTENGA CP
                    // 1. VALIDAR LA PALABRA CP
                    // 2. VALIDAR 
                    if (e.FormattedValue.ToString().Trim() != "Dónde" && e.FormattedValue.ToString().Trim() != "")
                    {
                        if (!ContieneCP(e.FormattedValue.ToString()))
                        {
                            MessageBox.Show("Atención:\n\nLa dirección no contiene Código Postal.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            e.Cancel = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Atención:\n\nLa dirección no puede ir vacía.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        e.Cancel = true;
                    }
                }
            }
        }

        private void dgViewProcesos_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewTextBoxCell cell = (DataGridViewTextBoxCell)dgViewProcesos.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (cell.Style.BackColor == System.Drawing.SystemColors.Control)
            {
                SendKeys.Send("{TAB}");
            }

        }
        private void dgViewProcesos_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                MessageBox.Show("Debe capturar el formato numérico correcto", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
        private void dgViewTallas1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Debe capturar el formato numérico correcto", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void dgViewTallas2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Debe capturar el formato numérico correcto", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void dgViewTallas3_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Debe capturar el formato numérico correcto", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private void dgViewTallas4_DataError(object sender, DataGridViewDataErrorEventArgs e)
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
        private void dgViewProcesos_Leave(object sender, EventArgs e)
        {
            if (dgViewProcesos.RowCount > 0)
            {
                DataGridViewCell cell = dgViewProcesos.CurrentCell;
                cell.Selected = false;
            }

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

        #endregion

        #region Métodos para limpiar y habilitar controles
        public void HabilitaControlesPartidas(bool Habilita)
        {
            txtPrecio.Enabled = Habilita;
            txtProcesos.Enabled = Habilita;
            txtDescuento.Enabled = Habilita;
            dgViewTallas1.Visible = Habilita;
            dgViewTallas2.Visible = Habilita;
            dgViewTallas3.Visible = Habilita;
            btnProcesar.Enabled = Habilita;
            if (this.tipoPartida == Enumerados.TipoPedido.PedidoMOS)
            {
                txtProcesos.Enabled = false;
            }
        }
        public void HabilitaControlesProcesos(bool Habilita)
        {
            dgViewProcesos.Enabled = Habilita;
        }
        public void HabilidaControlesDAT(bool esDAT)
        {
            //label4.Visible = !esDAT;
            //txtProcesos.Visible = !esDAT;
            lblDescuento.Visible = esDAT;
            txtDescuento.Visible = esDAT;
            txtDescuento.Text = "0";
        }
        private void limpiaValores()
        {
            lblModeloDescripcion.Text = "";
            txtPrecio.Text = "";
            txtProcesos.Text = "";
            //lblPrecioConsolidado.Text = "";
            //totalPrendas = 0;
            agrupador = "";
            precioLista = 0;
            descripcion = "";
        }
        #endregion

        #region Métodos de llenado y devolución de información

        private void backgroundWorkerModelos_DoWork(object sender, DoWorkEventArgs e)
        {
            ProcesoBusquedaModelo();
        }

        private void backgroundWorkerModelos_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            txtModelo.Enabled = true;
            lblProcesando.Visible = false;
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

                    //-----
                    List<PreciosXModelo> lstPreciosXModelo = vw_Inventario.TotalDePreciosEnModelo(datosModelo);
                    //if (lstPreciosXModelo.Count == -10) //<--------- esto es para que siempre entre a la parte falsa mientras autoricen la cotizacíon de esta modificación
                    /*POR REQUERIMIENTO ESTA PARTE SE SIGUE OMITIENDO
                    if (lstPreciosXModelo.Count > 1)  //<--------- descomentar para implementar                        
                    {
                        AddPartidasPediListaPrecio appListaPrecios = new AddPartidasPediListaPrecio(txtModelo.Text,lstPreciosXModelo);
                        DialogResult dlgResPrecios = appListaPrecios.ShowDialog();
                        if (dlgResPrecios == System.Windows.Forms.DialogResult.OK)
                        {
                            precioLista = appListaPrecios.PRECIO1;
                            txtPrecio.Text = appListaPrecios.PRECIO1.ToString();
                        }
                        else
                        {                                
                            if (datosModelo.Rows[0]["Precio1"] == System.DBNull.Value)
                            {
                                txtPrecio.Text = "0";
                                precioLista = 0;
                            }
                            else
                            {
                                txtPrecio.Text = datosModelo.Rows[0]["Precio1"].ToString();
                                precioLista = double.Parse(datosModelo.Rows[0]["Precio1"].ToString());
                            }
                        }
                    }*/

                    //else
                    //{
                    //-----
                    double.TryParse(datosModelo.Rows[0]["Precio1"].ToString(), out precioLista);
                    if (datosModelo.Rows[0]["Precio1"] == System.DBNull.Value)
                    {
                        txtPrecio.Text = "0";
                        precioLista = 0;
                    }
                    else
                    {
                        txtPrecio.Text = datosModelo.Rows[0]["Precio1"].ToString();
                        precioLista = double.Parse(datosModelo.Rows[0]["Precio1"].ToString());
                    }
                    //}

                    //txtPrecio.Text = precioLista.ToString();



                    lblPrecioConsolidado.Text = DevuelvePrecioConsolidado().ToString();


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
                    txtPrecio.Enabled = !PrecioEspecial;
                    if (tipoPartida == Enumerados.TipoPedido.Pedido || tipoPartida == Enumerados.TipoPedido.PedidoEC || tipoPartida == Enumerados.TipoPedido.PedidoMOS || tipoPartida == Enumerados.TipoPedido.PedidoDAT || tipoPartida == Enumerados.TipoPedido.PedidoMOSCP)
                    {
                        txtPrecio.Focus();
                    }
                    else
                    {
                        txtProcesos.Focus();
                    }

                }
                else
                {
                    limpiaValores();
                    HabilitaControlesProcesos(false);
                    HabilitaControlesPartidas(false);
                }
            }
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
            this.existencias = inventario2.GetExistenciasPorModelo134(txtModelo.Text);
            AsignarValoresModeloAcontroles(tiposProductos);
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
                        /*
                        // agregamos existencias de almacen 3
                        registroNuevo = tablasTallas.Tables[nombreTabla].NewRow();
                        for (int z = 0; z < registroNuevo.ItemArray.Length; z++)
                        {
                            string filtro = String.Format("CVE_ART = '{0}' AND CVE_ALM = 3", txtModelo.Text + tablasTallas.Tables[nombreTabla].Columns[z].ColumnName);
                            registroNuevo[z] = this.existencias.Select(filtro).FirstOrDefault()["EXIST"];
                        }
                        tablasTallas.Tables[nombreTabla].Rows.Add(registroNuevo);
                        // agregamos existencias de almacen 4
                        registroNuevo = tablasTallas.Tables[nombreTabla].NewRow();
                        for (int z = 0; z < registroNuevo.ItemArray.Length; z++)
                        {
                            string filtro = String.Format("CVE_ART = '{0}' AND CVE_ALM = 4", txtModelo.Text + tablasTallas.Tables[nombreTabla].Columns[z].ColumnName);
                            registroNuevo[z] = this.existencias.Select(filtro).FirstOrDefault()["EXIST"];
                        }
                        tablasTallas.Tables[nombreTabla].Rows.Add(registroNuevo);
                        */
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
                    /*dgViewTallas1.Rows[2].ReadOnly = true;
                    dgViewTallas1.Rows[2].DefaultCellStyle.BackColor = Color.LightGray;
                    dgViewTallas1.Rows[3].ReadOnly = true;
                    dgViewTallas1.Rows[3].DefaultCellStyle.BackColor = Color.LightGray;*/

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
        private void llenaDataTableProcesos()
        {
            procesos.Rows.Clear();
            //procesos.Columns.Clear();
            for (int i = 0; i < txtProcesos.TextLength; i++)
            {
                DataRow registros = procesos.NewRow();
                if (txtProcesos.Text.Substring(i, 1).ToUpper() == "C") registros["NombreProceso"] = "Costura";
                if (txtProcesos.Text.Substring(i, 1).ToUpper() == "B") registros["NombreProceso"] = "Bordado";
                if (txtProcesos.Text.Substring(i, 1).ToUpper() == "R") registros["NombreProceso"] = "Corte";
                if (txtProcesos.Text.Substring(i, 1).ToUpper() == "I") registros["NombreProceso"] = "Iniciales";
                if (txtProcesos.Text.Substring(i, 1).ToUpper() == "D") registros["NombreProceso"] = "Dibujo";
                if (txtProcesos.Text.Substring(i, 1).ToUpper() == "E") registros["NombreProceso"] = "Estampado";
                if (txtProcesos.Text.Substring(i, 1).ToUpper() == "F") registros["NombreProceso"] = "Logistica";
                if (txtProcesos.Text.Substring(i, 1).ToUpper() == "L") registros["NombreProceso"] = "Largo";
                if (txtProcesos.Text.Substring(i, 1).ToUpper() == "M") registros["NombreProceso"] = "Empaque";
                if (txtProcesos.Text.Substring(i, 1).ToUpper() == "Q") registros["NombreProceso"] = "Embarque";


                // Area Qué ******************
                // SE AGREGO NUEVO PROCESO M
                if (txtProcesos.Text.Substring(i, 1).ToUpper() == "M" || txtProcesos.Text.Substring(i, 1).ToUpper() == "Q")
                {
                    registros["Que"] = "";
                }
                else
                {
                    registros["Que"] = "Qué";
                }


                // Area Cómo ******************
                if (txtProcesos.Text.Substring(i, 1).ToUpper() == "B" || txtProcesos.Text.Substring(i, 1).ToUpper() == "E")
                {
                    registros["Como"] = "Cómo";
                }

                // SE AGREGO NUEVO PROCESO M
                else if (txtProcesos.Text.Substring(i, 1).ToUpper() == "M")
                {
                    registros["Como"] = "Cómo";
                }

                else
                {
                    registros["Como"] = "";
                }


                // Area Dónde ******************
                if (txtProcesos.Text.Substring(i, 1).ToUpper() == "B" || txtProcesos.Text.Substring(i, 1).ToUpper() == "E" || txtProcesos.Text.Substring(i, 1).ToUpper() == "Q")
                    registros["Donde"] = "Dónde";
                else
                    registros["Donde"] = "";


                if (tipoPartida == Enumerados.TipoPedido.Pedido || tipoPartida == Enumerados.TipoPedido.PedidoEC || tipoPartida == Enumerados.TipoPedido.PedidoMOS || tipoPartida == Enumerados.TipoPedido.PedidoDAT || tipoPartida == Enumerados.TipoPedido.PedidoMOSCP)
                {
                    registros["Precio"] = 0.00;
                }

                if (txtProcesos.Text.Substring(i, 1).ToUpper() == "Q")
                {
                    // BUSCAMOS SI YA EXISTE UN PROCESO "DONDE" PARA ASIGNAR EL MISMO VALOR
                    CMT_DET procesoEmbarque = BuscaDireccionEmbarque(int.Parse(txtPedido.Text));
                    if (procesoEmbarque != null)
                    {
                        registros["Donde"] = procesoEmbarque.CMT_DONDE;
                        registros["Precio"] = procesoEmbarque.CMT_PRE_PROCESO.ToString();
                    }
                }

                procesos.Rows.Add(registros);
            }
            dgViewProcesos.DataSource = procesos;
        }
        private bool contenidoValidoTxtProcesos()
        {
            bool resultado = true;
            //if (txtProcesos.Text.Length == 0) resultado = false;
            for (int i = 0; i < txtProcesos.TextLength; i++)
            {
                if (this.tipoPartida != Enumerados.TipoPedido.PedidoDAT)
                {
                    if (!"FCBRIDELMQ".Contains(txtProcesos.Text.Substring(i, 1).ToUpper())) //SE AGREGA L PARA PROCESO DE LARGO
                    {
                        resultado = false;
                        break;
                    }
                }
                else
                {
                    if (!"FLMQ".Contains(txtProcesos.Text.Substring(i, 1).ToUpper())) //SE AGREGA VALIDACION PARA DAT
                    {
                        resultado = false;
                        break;
                    }
                }

            }
            return resultado;
        }
        private void CalculaTotalPrendas()
        {
            ProcesaPrendas(RecorridoTablasTallas.CalcularCantidad, double.Parse(txtDescuento.Text == "" ? "0" : txtDescuento.Text));
            lblTotalPrendas.Text = totalPrendas.ToString();
        }

        private decimal DevuelvePrecioConsolidado()
        {
            decimal precioConsolidado = 0;
            if (txtPrecio.Text != "")
            {
                precioConsolidado = Convert.ToDecimal(txtPrecio.Text);
                if (procesos.Rows.Count > 0)
                {
                    precioConsolidado += Convert.ToDecimal(procesos.Compute("sum(Precio)", "").ToString());
                }
            }
            return precioConsolidado;
        }
        private decimal DevuelveFormatoNumericoValidado(string valor)
        {
            decimal result = 0;
            result = decimal.Parse(valor);
            return result;
        }
        #endregion

        #region Funcionalidades y métodos de guardado de información
        private void GuardarPed_Det(int idTabla, int idColumna, double descuentoArticulo = 0)
        {
            PED_DET pedidos_detalle = new PED_DET();
            pedidos_detalle.PEDIDO = Convert.ToInt32(txtPedido.Text);
            pedidos_detalle.CODIGO = txtModelo.Text + tablasTallas.Tables[idTabla].Columns[idColumna].Caption;
            //pedidos_detalle.DESCRIPCION = descripcion;
            pedidos_detalle.DESCRIPCION = vw_Inventario.RegresaModeloDescripcion(pedidos_detalle.CODIGO);
            pedidos_detalle.DESCUENTO = descuentoArticulo;
            pedidos_detalle.CANTIDAD = Convert.ToInt32(tablasTallas.Tables[idTabla].Rows[0][idColumna].ToString());
            pedidos_detalle.PROCESOS = txtProcesos.Text;
            pedidos_detalle.AGRUPADOR = agrupador;
            if (tipoPartida == Enumerados.TipoPedido.Pedido || tipoPartida == Enumerados.TipoPedido.PedidoEC || tipoPartida == Enumerados.TipoPedido.PedidoDAT || tipoPartida == Enumerados.TipoPedido.PedidoMOS || tipoPartida == Enumerados.TipoPedido.PedidoMOSCP)
            {
                pedidos_detalle.PRECIO_PROD = Convert.ToDecimal(Convert.ToDouble(txtPrecio.Text) - (Convert.ToDouble(txtPrecio.Text) * descuentoArticulo));
                if (procesos.Rows.Count > 0)
                {
                    pedidos_detalle.PREC_PROCESO = Convert.ToDecimal(procesos.Compute("sum(Precio)", "").ToString());
                    pedidos_detalle.SUBTOTAL = pedidos_detalle.PRECIO_PROD + Convert.ToDecimal(procesos.Compute("sum(Precio)", "").ToString());
                }
                else
                {
                    pedidos_detalle.PREC_PROCESO = 0;
                    pedidos_detalle.SUBTOTAL = pedidos_detalle.PRECIO_PROD;
                }
                pedidos_detalle.PRECIO_LISTA = Convert.ToDecimal(precioLista);
            }
            pedidos_detalle.Crear(pedidos_detalle);
        }
        private void GuardarPed_DetComision(String _codigo, decimal _precio, int idTabla, int idColumna, double descuentoArticulo = 0)
        {
            PED_DET pedidos_detalle = new PED_DET();
            pedidos_detalle.PEDIDO = Convert.ToInt32(txtPedido.Text);
            pedidos_detalle.CODIGO = _codigo + tablasTallas.Tables[idTabla].Columns[idColumna].Caption;
            //pedidos_detalle.DESCRIPCION = descripcion;
            pedidos_detalle.DESCRIPCION = vw_Inventario.RegresaModeloDescripcion(pedidos_detalle.CODIGO);
            pedidos_detalle.DESCUENTO = descuentoArticulo;
            pedidos_detalle.CANTIDAD = Convert.ToInt32(tablasTallas.Tables[idTabla].Rows[0][idColumna].ToString());
            pedidos_detalle.PREC_PROCESO = 0;
            pedidos_detalle.PROCESOS = "xx";
            pedidos_detalle.AGRUPADOR = agrupador;
            if (tipoPartida == Enumerados.TipoPedido.PedidoMOS || tipoPartida == Enumerados.TipoPedido.PedidoMOSCP)
            {
                pedidos_detalle.PRECIO_PROD = _precio;
                pedidos_detalle.PRECIO_LISTA = _precio;
                pedidos_detalle.SUBTOTAL = _precio;
            }
            pedidos_detalle.Crear(pedidos_detalle);
        }
        private void GuardarPed_Temp()
        {
            PED_TEMP ped_temp = new PED_TEMP();
            ped_temp.PEDIDO = Convert.ToInt32(txtPedido.Text);
            ped_temp.CrearPorPedido(ped_temp, tipoPartida);
        }

        private void ActualizaAcumuladosPed_Mstr(int Pedido)
        {
            PED_MSTR ped_mstr = new PED_MSTR();
            ped_mstr.PEDIDO = Pedido;
            ped_mstr.ActualizaAcumulados(ped_mstr);
        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            //valida que no dejen vacías celdas en el grid de los procesos
            string celdasVacias = GridProcesosCeldasVacias();
            if (celdasVacias != string.Empty)
            {
                //mensaje : no puedes dejar celdas vacías
                MessageBox.Show(celdasVacias, "Verifique", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //obtiene el agrupador
            Cursor.Current = Cursors.WaitCursor;
            Random rnd = new Random();
            //agrupador = Convert.ToString(rnd.Next() * DateTime.Now.TimeOfDay.Ticks);
            agrupador = Convert.ToString(rnd.Next() * DateTime.Now.ToOADate());
            agrupador = agrupador.Substring(0, 10);
            //inserta las tallas de las prendas que hayan elegido en el pedido
            ProcesaPrendas(RecorridoTablasTallas.Guardar, double.Parse(txtDescuento.Text == "" ? "0" : txtDescuento.Text));

            //Llenado de procesos en cmt_det
            CMT_DET cmt_det = new CMT_DET();
            for (int i = 0; i < procesos.Rows.Count; i++)
            {
                cmt_det.CMT_PEDIDO = Convert.ToInt32(txtPedido.Text);
                cmt_det.CMT_CMMT = procesos.Rows[i]["Que"].ToString();
                cmt_det.CMT_COMO = procesos.Rows[i]["Como"].ToString();
                cmt_det.CMT_DONDE = procesos.Rows[i]["Donde"].ToString();
                cmt_det.CMT_AGRUPADOR = agrupador;
                cmt_det.CMT_PROCESO = txtProcesos.Text.Substring(i, 1);
                cmt_det.CMT_MAQUILERO = "1";
                cmt_det.CMT_MODELO = txtModelo.Text;
                cmt_det.CMT_RUTA = txtProcesos.Text;
                cmt_det.CMT_CLIENTE = cliente;
                cmt_det.CMT_ESTATUS = "P";
                cmt_det.CMT_CANTIDAD = totalPrendas;
                cmt_det.CMT_ORDENAMIENTO = i;

                if (tipoPartida == Enumerados.TipoPedido.Pedido || tipoPartida == Enumerados.TipoPedido.PedidoEC || tipoPartida == Enumerados.TipoPedido.PedidoDAT || tipoPartida == Enumerados.TipoPedido.PedidoMOS || tipoPartida == Enumerados.TipoPedido.PedidoMOSCP)
                {
                    cmt_det.CMT_PUNTADAS = puntadas;
                    cmt_det.CMT_PRE_PROCESO = Convert.ToDecimal(procesos.Rows[i]["Precio"].ToString());
                    cmt_det.CMT_COS_PROCESO = 0;
                }
                else
                {
                    cmt_det.CMT_TIPO = "OT";
                }

                /*ADECUACION PARA ASIGNAR RUTA DE FORMA AUTOMATICA*/
                switch (cmt_det.CMT_PROCESO)
                {
                    case "B":
                        cmt_det.CMT_DEPARTAMENTO = "B1";
                        break;
                    case "C":
                        cmt_det.CMT_DEPARTAMENTO = "C1";
                        break;
                    case "I":
                        cmt_det.CMT_DEPARTAMENTO = "B3";
                        break;
                    case "E":
                        cmt_det.CMT_DEPARTAMENTO = "E1";
                        break;
                }


                cmt_det.Crear(cmt_det);
            }
            Cursor.Current = Cursors.Default;
            if (tipoPartida == Enumerados.TipoPedido.Pedido || tipoPartida == Enumerados.TipoPedido.PedidoEC || tipoPartida == Enumerados.TipoPedido.PedidoDAT || tipoPartida == Enumerados.TipoPedido.PedidoMOS || tipoPartida == Enumerados.TipoPedido.PedidoMOSCP)
            {
                if (MessageBox.Show("¿Desea agregar otra partida?", "Agregar más partidas", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    GuardarPed_Temp();
                    txtProcesos.Text = "";
                    txtModelo.Text = "";
                    txtModelo.Focus();
                }
                else
                {
                    /*
                    // EN CASO DE QUE EXISTA ALGUNA COMISION SE APLICA EN LA PARTIDA +DIFERENCIA
                    //1. OBTENEMOS TALLAS PARA +DIFEREN
                    //DE LO CONTRARIO BUSCAMOS EL PRECIO EN LA LISTA GENERAL
                    if (this.formaPagoComision > 0)
                    {                        
                        vw_Inventario inventario = new vw_Inventario();
                        DataTable tiposProductos = new DataTable();
                        tiposProductos = inventario.Consultar("+DIFEREN");
                        if (tiposProductos.Rows.Count > 0)
                        {
                            //2. ASIGNAMOS TALLA
                            llenaDatosTallas(tiposProductos);
                            //3. ASIGNAMOS LA CANTIDAD
                            tablasTallas.Tables[0].Rows[0][0] = 1;
                            //4. OBTENEMOS EL SUBTOTAL DEL PEDIDO
                            DataTable datos_pedido = new DataTable();
                            PED_MSTR pedido_imprimir = new PED_MSTR();
                            GuardarPed_Temp();
                            ActualizaAcumuladosPed_Mstr(Convert.ToInt32(txtPedido.Text));
                            datos_pedido = pedido_imprimir.ConsultaImprimir(Convert.ToInt32(txtPedido.Text));


                            rnd = new Random();
                            //agrupador = Convert.ToString(rnd.Next() * DateTime.Now.TimeOfDay.Ticks);
                            agrupador = Convert.ToString(rnd.Next() * DateTime.Now.ToOADate());
                            agrupador = agrupador.Substring(0, 10);
                            decimal montoComision = Convert.ToDecimal(datos_pedido.Rows[0]["Importe"].ToString()) * (this.formaPagoComision / 100);

                            //inserta las tallas de las prendas que hayan elegido en el pedido
                            ProcesaPrendasComision(RecorridoTablasTallas.Guardar, double.Parse(txtDescuento.Text == "" ? "0" : txtDescuento.Text), montoComision);
                        }
                    }
                    */

                    if (MessageBox.Show("¿Desea visualizar el Pedido en Pantalla?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, cliente, 0, Convert.ToInt32(txtPedido.Text), tipoPartida, this.esDAT, this.descuento, this.frmOrigen, this.dividir);
                        frmReportes.ShowDialog(this);
                        pedidoEstatusModificado = frmReportes.pedidoEstatusModificado;
                        //if (frmReportes.pedidoTambienImprimeOT)
                        //{
                        //    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.OrdenTrabajo, cliente, 0, Convert.ToInt32(txtPedido.Text), Enumerados.TipoPedido.OrdenTrabajo);
                        //    frmReportes.ShowDialog(this);
                        //}
                    }
                    else
                    {
                        GuardarPed_Temp();
                    }
                    this.Close();
                }
            }
            else
            {
                if (MessageBox.Show("¿Desea agregar otra partida a la Órden de Trabajo?", "Agregar más partidas", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    GuardarPed_Temp();
                    txtProcesos.Text = "";
                    txtModelo.Text = "";
                    txtModelo.Focus();
                }
                else
                {
                    if (MessageBox.Show("¿Desea visualizar la Órden de Trabajo en Pantalla?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.OrdenTrabajo, cliente, 0, Convert.ToInt32(txtPedido.Text), tipoPartida);
                        frmReportes.Show();
                        pedidoEstatusModificado = frmReportes.pedidoEstatusModificado;
                    }
                    else
                    {
                        GuardarPed_Temp();
                    }
                    this.Close();
                }
            }


        }
        private string GridProcesosCeldasVacias()
        {

            string celdasVacias = "";
            DataTable dataTableProc = (DataTable)dgViewProcesos.DataSource;
            int iProc = 0;
            if (dataTableProc != null)
            {
                if (dataTableProc.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTableProc.Rows)
                    {
                        iProc++;
                        if (row["que"].ToString() == "Qué")
                        {
                            celdasVacias += string.Format(
                                "En \"{1}\" del renglón: {0} escriba un valor para \"Qué\"\n", iProc,
                                row["NombreProceso"].ToString());
                        }
                        if (row["como"].ToString() == "Cómo")
                        {
                            celdasVacias += string.Format(
                                "En \"{1}\" del renglón: {0} escriba un valor para \"Cómo\"\n", iProc,
                                row["NombreProceso"].ToString());
                        }
                        if (row["donde"].ToString() == "Dónde")
                        {
                            celdasVacias +=
                                string.Format("En \"{1}\" del renglón: {0} escriba un valor para \"Dónde\"\n", iProc,
                                    row["NombreProceso"].ToString());
                        }
                    }
                }
            }
            return celdasVacias;
        }
        #endregion

        private void AddPartidasPedi_Load(object sender, EventArgs e)
        {
            //frmLogoPreview.Location = new Point(this.Location.X - frmLogoPreview.Width, this.Location.Y);
            //frmLogoPreview.Visible = false;
            this.tipoEmpaque.AddRange(new string[]
                    {
                        "Granel",
                        "Lista de empaque",
                        "Por departamentos",
                        "Iniciales bordadas",
                        "Prenda embolsada"
                    });
        }

        private void dgViewProcesos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgViewProcesos_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgViewProcesos.CurrentCell.ColumnIndex == 2)
            {
                TextBox txt = e.Control as TextBox;
                if (txt != null)
                {
                    txt.TextChanged += txt_TextChanged;
                    txt.LostFocus += txt_LostFocus;
                    txt.AutoCompleteMode = AutoCompleteMode.Suggest;
                    txt.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    txt.AutoCompleteCustomSource = dgViewProcesos.CurrentRow.Cells[0].Value == "Empaque" ? this.tipoEmpaque : GlobalesUI.autoCompleteLogosCollection;
                }
            }
            else
            {
                TextBox txt = e.Control as TextBox;
                if (txt != null)
                {
                    txt.AutoCompleteMode = AutoCompleteMode.None;
                }
            }
        }

        void txt_LostFocus(object sender, EventArgs e)
        {
            frmLogoPreview.Visible = false;
        }

        void txt_TextChanged(object sender, EventArgs e)
        {
            if (dgViewProcesos.CurrentCell.ColumnIndex == 2 && dgViewProcesos.CurrentRow.Cells[0].Value != "Empaque")
            {
                if (dgViewProcesos.CurrentCell != null)
                {
                    string cellText = ((TextBox)sender).Text;

                    if (cellText.IndexOf("-") < cellText.LastIndexOf("-"))
                    {
                        if (cellText.Length == cellText.LastIndexOf("-") + 3)
                        {

                            Image imageLogo = IMAGENES.LogotiposDevueImagen(cellText);

                            if (imageLogo == null)
                            {
                                imageLogo = Properties.Resources.sin_imagen;
                            }

                            frmLogoPreview.Location = new Point(this.Location.X - frmLogoPreview.Width, this.Location.Y);
                            frmLogoPreview.pictureBox1.Image = imageLogo;
                            frmLogoPreview.Show();
                        }
                        else
                        {
                            frmLogoPreview.Hide();
                        }
                    }
                }
                else
                {
                    frmLogoPreview.Hide();
                }
            }
        }

        private void AddPartidasPedi_Activated(object sender, EventArgs e)
        {
            frmLogoPreview.Location = new Point(this.Location.X - frmLogoPreview.Width, this.Location.Y);
        }

        private void dgViewProcesos_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            //if (dgViewProcesos.IsCurrentCellDirty)
            //{                    
            //    dgViewProcesos.CommitEdit(DataGridViewDataErrorContexts.Commit);
            //}
        }

        private CMT_DET BuscaDireccionEmbarque(int pedido)
        {
            CMT_DET det = new CMT_DET();
            det = CMT_DET.BuscaProcesoEmbarque(pedido);
            return det;
        }

        private bool ContieneCP(string direccion)
        {
            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
            List<string> list = direccion.Split(delimiterChars).ToList();
            int parseCP = 0;
            bool cpIniciaCon0 = false;
            foreach (string value in list)
            {
                int.TryParse(value.Trim(), out parseCP);
                if (value.Length > 0)
                {
                    if (value[0].ToString() == "0")
                    {
                        cpIniciaCon0 = true;
                    }
                }
                if (value.ToUpper().Trim().Replace(".", "").Replace(",", "") == "CP")
                {
                    // buscamos la posición de value (CP)
                    int index = list.IndexOf(value);
                    // validamos que la siguiente posición corresponda a un valor de 5 caracteres que representa el CP
                    if (list.Count > index + 1)
                    {
                        int parseNextValue;
                        int.TryParse(list[index + 1].Trim(), out parseNextValue);
                        if (parseNextValue.ToString().Length == 5) { return true; }
                    }
                }
                if (parseCP > 0)
                {
                    if (parseCP.ToString().Length == (cpIniciaCon0 ? 4 : 5)) { return true; }
                }
            }

            return false;
        }
    }
}
