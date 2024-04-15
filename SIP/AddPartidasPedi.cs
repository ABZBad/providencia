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
    public partial class AddPartidasPedi : Form
    {
        #region Declaración de variables        
            AddPartidasPediLogoPreview frmLogoPreview = new AddPartidasPediLogoPreview();                                
            private delegate void DelAsignarValoresModeloAcontroles(DataTable AsignarValoresModelo);
            enum RecorridoTablasTallas
            {
                Guardar,
                CalcularCantidad
            }
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
            BackgroundWorker backgroundWorkerModelos = new BackgroundWorker();            
        #endregion

        #region Funcionalidades y métodos generales
            public AddPartidasPedi()
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
            public AddPartidasPedi(string idCliente, int idPedido, Enumerados.TipoPedido tipo,string vendedor)
            {
                InitializeComponent();
                pedidoEstatusModificado = false;
                tipoPartida = tipo;
                cliente = idCliente;
                txtPedido.Text = idPedido.ToString();
                nombre_vendedor = vendedor;
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
            }

            private void button1_Click(object sender, EventArgs e)
            {
                this.Close();
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
                        if (tablasTallas.Tables[i].Rows[0][y].ToString()=="")
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
        #endregion   

        #region Funcionalidades Cajas de texto
            private void txtModelo_TextChanged(object sender, EventArgs e)
            {
                if (txtModelo.Text.Length == 8 || txtModelo.Text.ToUpper()=="LOGISTICA")
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

                        if (tipoPartida == Enumerados.TipoPedido.Pedido)
                        {
                            lblPrecioConsolidado.Text = DevuelvePrecioConsolidado().ToString();
                        }                        
                        DataGridViewCell cell = dgViewProcesos.CurrentCell;
                        if (cell!=null)
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
                if (tipoPartida == Enumerados.TipoPedido.Pedido)
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
                        if (e.Value.ToString() != "Bordado" && e.Value.ToString() != "Estampado")
                        {
                            DataGridViewTextBoxCell cellAEditar = (DataGridViewTextBoxCell)dgViewProcesos.Rows[e.RowIndex].Cells["Como"];
                            cellAEditar.ReadOnly = true;
                            cellAEditar.Style.BackColor = System.Drawing.SystemColors.Control;
                            DataGridViewTextBoxCell cellDonde = (DataGridViewTextBoxCell)dgViewProcesos.Rows[e.RowIndex].Cells["Donde"];
                            cellDonde.ReadOnly = true;
                            cellDonde.Style.BackColor = System.Drawing.SystemColors.Control;
                        }
                    }
                    if (e.ColumnIndex==4&&tipoPartida==Enumerados.TipoPedido.OrdenTrabajo)
                    {
                        DataGridViewTextBoxCell cellAEditar = (DataGridViewTextBoxCell)dgViewProcesos.Rows[e.RowIndex].Cells["Precio"];
                            cellAEditar.ReadOnly = true;
                            cellAEditar.Style.BackColor = System.Drawing.SystemColors.Control;                        
                    }
                }

            }
            private void dgViewProcesos_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
            {
                if (tipoPartida == Enumerados.TipoPedido.Pedido)
                {                                            
                    if (e.ColumnIndex == 2 && (dgViewProcesos.Rows[e.RowIndex].Cells[0].Value.ToString() == "Bordado" || dgViewProcesos.Rows[e.RowIndex].Cells[0].Value.ToString() == "Estampado"))
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
                                    MessageBox.Show("Atención:\n\nEste Logotipo existe pero no es exclusivo de este cliente.", "",MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    MessageBox.Show("Debe capturar el formato numérico correcto", "", MessageBoxButtons.OK,MessageBoxIcon.Warning);
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
            private void dgViewProcesos_Leave(object sender, EventArgs e)
            {
                if (dgViewProcesos.RowCount>0)
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
                dgViewTallas1.Visible = Habilita;
                dgViewTallas2.Visible = Habilita;
                dgViewTallas3.Visible = Habilita;
                btnProcesar.Enabled = Habilita;
            }
            public void HabilitaControlesProcesos(bool Habilita)
            {
                dgViewProcesos.Enabled = Habilita;                
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
                        if (posicion<1)
                        {
                            lblModeloDescripcion.Text = datosModelo.Rows[0]["DESCR"].ToString();
                        }
                        else
                        {
                            lblModeloDescripcion.Text = datosModelo.Rows[0]["DESCR"].ToString().Substring(0, posicion);
                        }       
                 
                        if (datosModelo.Rows[0]["DESCR"].ToString().Length>50)
                        {
                            descripcion = datosModelo.Rows[0]["DESCR"].ToString().Substring(0, 50);    
                        }
                        else
                        {                            
                            descripcion = datosModelo.Rows[0]["DESCR"].ToString();    
                        }
                        HabilitaControlesPartidas(true);
                        llenaDatosTallas(datosModelo);
                        if (tipoPartida == Enumerados.TipoPedido.Pedido)
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
                vw_Inventario inventario = new vw_Inventario();
                tiposProductos = inventario.Consultar(txtModelo.Text);
                AsignarValoresModeloAcontroles(tiposProductos);
            }

            private void llenaDatosTallas(DataTable tallas)
            {
                int tot = 0; ;
                tablasTallas = new DataSet();
                int numeroTablas = ((tallas.Rows.Count - 1) / 10);
                dgViewTallas1.DataSource = null;
                dgViewTallas2.DataSource = null;
                dgViewTallas2.Enabled = false;
                dgViewTallas3.DataSource = null;
                dgViewTallas3.Enabled = false;
                for (int i = 0; i <= numeroTablas; i++)
                {

                    string nombreTabla = "T" + i.ToString();
                    tablasTallas.Tables.Add(nombreTabla);
                    for (int y = 0; y < 10; y++)
                    {
                        int longitudCol;
                        DataColumn columna = new DataColumn();
                        longitudCol = tallas.Rows[tot]["CLV_ART"].ToString().Length;
                        if (longitudCol>8)
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
                        if (y == 9 || (tallas.Rows.Count-1) == tot)
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
                        dgViewTallas2.Enabled = true;
                        dgViewTallas2.DataSource = tablasTallas.Tables[nombreTabla];
                        DataGridViewCell cell2 = dgViewTallas2.CurrentCell;
                        cell2.Selected = false;
                    }
                    if (i == 2)
                    {
                        dgViewTallas3.Enabled = true;
                        dgViewTallas3.DataSource = tablasTallas.Tables[nombreTabla];
                        DataGridViewCell cell3 = dgViewTallas3.CurrentCell;
                        cell3.Selected = false;
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

                    registros["Que"] = "Qué";
                    if (txtProcesos.Text.Substring(i, 1).ToUpper() == "B" || txtProcesos.Text.Substring(i, 1).ToUpper() == "E")
                        registros["Como"] = "Cómo";
                    else
                        registros["Como"] = "";

                    if (txtProcesos.Text.Substring(i, 1).ToUpper() == "B" || txtProcesos.Text.Substring(i, 1).ToUpper() == "E")
                        registros["Donde"] = "Dónde";
                    else
                        registros["Donde"] = "";
                    if (tipoPartida == Enumerados.TipoPedido.Pedido)
                    {
                        registros["Precio"] = 0.00;
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
                    if (!"FCBRIDE".Contains(txtProcesos.Text.Substring(i, 1).ToUpper()))
                    {
                        resultado = false;
                        break;
                    }
                }
                return resultado;
            }
            private void CalculaTotalPrendas()
            {
                ProcesaPrendas(RecorridoTablasTallas.CalcularCantidad);
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
            private void GuardarPed_Det(int idTabla, int idColumna)
            {
                PED_DET pedidos_detalle = new PED_DET(); 
                pedidos_detalle.PEDIDO = Convert.ToInt32(txtPedido.Text);
                pedidos_detalle.CODIGO = txtModelo.Text + tablasTallas.Tables[idTabla].Columns[idColumna].Caption;                
                //pedidos_detalle.DESCRIPCION = descripcion;
                pedidos_detalle.DESCRIPCION = vw_Inventario.RegresaModeloDescripcion(pedidos_detalle.CODIGO);
                pedidos_detalle.DESCUENTO = 0;
                pedidos_detalle.CANTIDAD = Convert.ToInt32(tablasTallas.Tables[idTabla].Rows[0][idColumna].ToString());
                pedidos_detalle.PROCESOS = txtProcesos.Text;
                pedidos_detalle.AGRUPADOR = agrupador;
                if (tipoPartida == Enumerados.TipoPedido.Pedido)
                {
                    pedidos_detalle.PRECIO_PROD = Convert.ToDecimal(txtPrecio.Text);
                    if (procesos.Rows.Count>0)
                    {
                        pedidos_detalle.PREC_PROCESO = Convert.ToDecimal(procesos.Compute("sum(Precio)", "").ToString());
                        pedidos_detalle.SUBTOTAL = Convert.ToDecimal(txtPrecio.Text) + Convert.ToDecimal(procesos.Compute("sum(Precio)", "").ToString());                        
                    }
                    else
                    {
                        pedidos_detalle.PREC_PROCESO = 0;
                        pedidos_detalle.SUBTOTAL = Convert.ToDecimal(txtPrecio.Text);
                    }
                    pedidos_detalle.PRECIO_LISTA = Convert.ToDecimal(precioLista);
                }
                pedidos_detalle.Crear(pedidos_detalle);
            }

            private void GuardarPed_Temp()
            {
                PED_TEMP ped_temp = new PED_TEMP();
                ped_temp.PEDIDO = Convert.ToInt32(txtPedido.Text);
                ped_temp.CrearPorPedido(ped_temp, tipoPartida);
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
                ProcesaPrendas(RecorridoTablasTallas.Guardar);

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

                    if (tipoPartida == Enumerados.TipoPedido.Pedido)
                    {
                        cmt_det.CMT_PUNTADAS = puntadas;
                        cmt_det.CMT_PRE_PROCESO = Convert.ToDecimal(procesos.Rows[i]["Precio"].ToString());
                        cmt_det.CMT_COS_PROCESO = 0;
                    }
                    else
                    {
                        cmt_det.CMT_TIPO = "OT";        
                    }
                    cmt_det.Crear(cmt_det);                    
                }               
                Cursor.Current = Cursors.Default;
                if (tipoPartida==Enumerados.TipoPedido.Pedido)
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
                        if (MessageBox.Show("¿Desea visualizar el Pedido en Pantalla?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, cliente, 0, Convert.ToInt32(txtPedido.Text), tipoPartida);
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
                
                string celdasVacias ="";
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
                        txt.AutoCompleteCustomSource = GlobalesUI.autoCompleteLogosCollection;
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
                if (dgViewProcesos.CurrentCell.ColumnIndex == 2)
                {
                    if (dgViewProcesos.CurrentCell != null)
                    {
                        string cellText = ((TextBox) sender).Text;

                        if (cellText.IndexOf("-") < cellText.LastIndexOf("-"))
                        {
                            if (cellText.Length == cellText.LastIndexOf("-") + 3)
                            {

                                Image imageLogo = IMAGENES.LogotiposDevueImagen(cellText);

                                if (imageLogo == null)
                                {
                                    imageLogo = Properties.Resources.sin_imagen;
                                }

                                frmLogoPreview.Location = new Point(this.Location.X - frmLogoPreview.Width,this.Location.Y);
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
      


    }
}
