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
    public partial class frmDupCodProdEstr : Form
    {
        private Precarga precarga;
        private BackgroundWorker bgwDatosModelo;
        private ModeloYTallas modeloYTallas;
        private DataTable EstructuraExistente = new DataTable();
        private bool darFocoTxtModelo = true;
        private bool modeloCapturado = false;
        private bool agregandoComponente;

        public frmDupCodProdEstr()
        {
            InitializeComponent();
            precarga = new Precarga(this);
        }
        public frmDupCodProdEstr(String _codigo)
        {
            InitializeComponent();
            precarga = new Precarga(this);
            txtModelo.Text = _codigo;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtModelo_Leave(object sender, EventArgs e)
        {
            if (txtModelo.Text.Length == 8)
            {

                this.Cursor = Cursors.WaitCursor;

                bgwDatosModelo = new BackgroundWorker();
                bgwDatosModelo.DoWork += bgwDatosModelo_DoWork;
                bgwDatosModelo.RunWorkerCompleted += bgwDatosModelo_RunWorkerCompleted;
                bgwDatosModelo.RunWorkerAsync(txtModelo.Text);
                precarga.MostrarEspera();
            }
        }
        private CLIN01 RegresaLineaProd(string ID)
        {
            CLIN01 lineaProd = new CLIN01();
            lineaProd = lineaProd.Consultar(ID);
            return lineaProd;
        }
        void LimpiaCampos()
        {
            modeloCapturado = false;
            txtDescripcion.Text = "";
            txtLineaProd.Text = "";
            txtPrecioPublico.Text = "";
            lblModelo.Text = "";
            lblExiste.Text = "";
        }
        void bgwDatosModelo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (modeloYTallas.DatosModelo.MODELO != null)
            {
                modeloCapturado = true;
                txtDescripcion.Text = modeloYTallas.DatosModelo.DESCR;
                txtLineaProd.Text = modeloYTallas.DatosModelo.LIN_PROD;
                txtPrecioPublico.Text = modeloYTallas.DatosModelo.PRECIO.ToString();
                lblModelo.Text = modeloYTallas.DatosModelo.DESC_LIN;
                txtObservaciones.Text = modeloYTallas.DatosModelo.OBSERVACIONES;
                txtPeso.Text = modeloYTallas.DatosModelo.PESO.ToString();
                txtAgrupacion.Text = modeloYTallas.DatosModelo.AGRUPACION.ToString();
                txtDescripcionEtiqueta.Text = modeloYTallas.DatosModelo.ETIQUETA.ToString();
                dgViewCodigosExistentes.DataSource = modeloYTallas.CodigosExistentes;
                EstructuraExistente = DupCodProdEstr.RegresaEstructuraExistente(modeloYTallas.DatosModelo.CVE_ART);
                dgViewEsctructuraExistente.DataSource = EstructuraExistente;
                if (EstructuraExistente != null)
                {
                    if (EstructuraExistente.Rows.Count > 0)
                    {
                        txtEstruAlmPT.Text = EstructuraExistente.Rows[0]["PT_ALMACEN"].ToString();
                    }
                    else
                    {
                        txtEstruAlmPT.Text = "1";
                    }
                }
                List<CheckBox> chkAlmacen = new List<CheckBox>();
                chkAlmacen = Utiles.FuncionalidadesFormularios.DevuelveListaDeObjetosContenidosEnFormulario<CheckBox>(this, new CheckBox());
                foreach (CheckBox check in chkAlmacen)
                {
                    foreach (int elemento in modeloYTallas.Almacenes)
                    {
                        if (check.Tag != null)
                        {
                            if (check.Tag.ToString() == elemento.ToString())
                            {
                                check.Checked = true;
                            }
                        }
                    }
                }
                lblExiste.Text = "El Modelo existe";
                CalculaCostoPrenda();
            }
            else
            {
                LimpiaCampos();
                lblExiste.Text = "El Modelo no existe";
                dgViewCodigosExistentes.DataSource = modeloYTallas.CodigosExistentes;
                EstructuraExistente = DupCodProdEstr.RegresaEstructuraExistente();
                dgViewEsctructuraExistente.DataSource = EstructuraExistente;
                txtEstruAlmPT.Text = "2";
            }
            precarga.RemoverEspera();
            this.Cursor = Cursors.Default;
        }

        void bgwDatosModelo_DoWork(object sender, DoWorkEventArgs e)
        {
            string modelo = e.Argument.ToString();
            modeloYTallas = DupCodProdEstr.RegresaDatosModelo(modelo);

        }

        private void txtModelo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
                LimpiaCampos();
        }

        private void txtModelo_TextChanged(object sender, EventArgs e)
        {
            if (txtModelo.Text.Length < 8 && dgViewCodigosExistentes.Rows.Count > 0)
            {
                LimpiaCampos();
                LimpiaValores();
            }
        }

        private void frmDupCodProdEstr_Activated(object sender, EventArgs e)
        {
            if (darFocoTxtModelo)
            {
                txtModelo.Focus();
                darFocoTxtModelo = false;
            }
        }

        private void chkLinea_CheckedChanged(object sender, EventArgs e)
        {
            chk1.Checked = chkLinea.Checked;
            chk3.Checked = chkLinea.Checked;
            chk4.Checked = chkLinea.Checked;
            chk5.Checked = chkLinea.Checked;
            chk35.Checked = chkLinea.Checked;
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkTodos_CheckedChanged(object sender, EventArgs e)
        {
            chk1.Checked = chkTodos.Checked;
            chk2.Checked = chkTodos.Checked;
            chk3.Checked = chkTodos.Checked;
            chk4.Checked = chkTodos.Checked;
            chk5.Checked = chkTodos.Checked;
            chk32.Checked = chkTodos.Checked;
            chk35.Checked = chkTodos.Checked;
            chk36.Checked = chkTodos.Checked;
            chk40.Checked = chkTodos.Checked;
        }

        private void chkEspeciales_CheckedChanged(object sender, EventArgs e)
        {
            chk5.Checked = chkEspeciales.Checked;
            chk32.Checked = chkEspeciales.Checked;
        }

        private void txtLineaProd_Leave(object sender, EventArgs e)
        {
            lblModelo.Text = RegresaLineaProd(txtLineaProd.Text).DESC_LIN;
        }

        private void btnLineaProd_Click(object sender, EventArgs e)
        {
            CLIN01 linea_prod = new CLIN01();
            DataTable datos_linea_prod = new DataTable();
            datos_linea_prod = linea_prod.ConsultarCoincidencias("");
            darFocoTxtModelo = false;
            frmBusquedaGenerica frmBusqueda = new frmBusquedaGenerica(datos_linea_prod, txtLineaProd.Text, "CLAVE", "LÍNEAS DE PRODUCTOS");
            frmBusqueda.ShowDialog();
            if (frmBusqueda.RenglonSeleccionado != null)
            {
                txtLineaProd.Text = frmBusqueda.RenglonSeleccionado["CLAVE"].ToString().Trim();
                lblModelo.Text = frmBusqueda.RenglonSeleccionado["NOMBRE"].ToString().Trim();
            }
        }
        private void btnAgregarTalla_Click(object sender, EventArgs e)
        {
            //agrega registro
            if (txtModelo.Text.Trim().Length == 8 && txtDescripcion.Text != "" && txtLineaProd.Text != "" && txtPrecioPublico.Text != "0")
            {
                if (txtTalla.TextLength == 4)
                {
                    DataRow[] tallasExistentes = modeloYTallas.CodigosExistentes.Select(string.Format("TALLA='{0}'", txtTalla.Text));
                    if (tallasExistentes.Length == 0)
                    {
                        groupBoxPrincipal.Enabled = false;
                        lblExiste.Text = "";
                        DataRow row = modeloYTallas.CodigosExistentes.NewRow();
                        row["TALLA"] = txtTalla.Text;

                        string descr = DupCodProdEstr.RegresaDescripcionCodigo(txtDescripcion.Text, txtTalla.Text, txtSufijoDescripcion.Text);

                        if (descr.Length > 40)
                            descr = DupCodProdEstr.RegresaDescripcionCodigo(txtDescripcion.Text, txtTalla.Text, txtSufijoDescripcion.Text).Substring(0, 40);

                        row["DESCR"] = descr;
                        row["ESTATUS"] = "ACTIVO";
                        row["EXIST"] = 0;
                        //row["ETIQUETA"] = txtDescripcionEtiqueta.Text;
                        row["Accion"] = "Agregar";
                        modeloYTallas.CodigosExistentes.Rows.Add(row);
                        txtTalla.Text = "";
                        if (dgViewCodigosExistentes.RowCount > 0)
                        {
                            dgViewCodigosExistentes.FirstDisplayedScrollingRowIndex = dgViewCodigosExistentes.RowCount - 1;
                        }
                    }
                    else
                    {
                        MessageBox.Show("La Talla ha sido agregado previamente.");
                        txtTalla.Focus();
                    }
                }
                else
                {
                    lblExiste.Text = "Captura una Talla válida.";
                    txtTalla.Focus();
                }

            }

        }

        private void btnQuitarTalla_Click(object sender, EventArgs e)
        {
            //eliminar registro

            if (dgViewCodigosExistentes.CurrentRow != null)
            {

                if (dgViewCodigosExistentes.CurrentRow.Cells["Accion"].Value == "Agregar")
                {
                    int registro = dgViewCodigosExistentes.CurrentRow.Index;
                    modeloYTallas.CodigosExistentes.Rows.RemoveAt(registro);
                    lblExiste.Text = "";
                    DataRow[] tallasNuevas = modeloYTallas.CodigosExistentes.Select("Accion='Agregar'");
                    if (tallasNuevas.Length == 0)
                    {
                        groupBoxPrincipal.Enabled = true;
                    }
                    Totales();
                }
                else
                {
                    lblExiste.Text = "No se pueden eliminar Tallas existentes.";
                }
            }
        }

        private void btnEstruProceso_Click(object sender, EventArgs e)
        {
            DespliegaBusquedaGenericaProceso();
        }
        private void DespliegaBusquedaGenericaProceso()
        {
            Cursor.Current = Cursors.WaitCursor;
            PROCESOS01 procesos = new PROCESOS01();
            DataTable datos = new DataTable();
            datos = procesos.ConsultarCoincidencias("");
            frmBusquedaGenerica frmBusqueda = new frmBusquedaGenerica(datos, txtEstruProceso.Text, "CLAVE", "Procesos de Producción");
            frmBusqueda.ShowDialog();
            if (frmBusqueda.RenglonSeleccionado != null)
            {
                txtEstruProceso.Text = frmBusqueda.RenglonSeleccionado["CLAVE"].ToString().Trim();
            }
            Cursor.Current = Cursors.Default;
        }
        private void DespliegaBusquedaGenericaComponente(int Tecla)
        {
            Cursor.Current = Cursors.WaitCursor;
            string catalogo = "";
            DataTable datos = new DataTable();
            int shF2 = Convert.ToInt32(Keys.Shift) + Convert.ToInt32(Keys.F2);
            if (Tecla == shF2 || Tecla == Convert.ToInt32(Keys.F2))
            {
                catalogo = string.Format("Productos [Almacén {0}]", txtEstruAlm.Text);
                datos = DupCodProdEstr.ComponenteConsultarCoincidencias("", 1, Convert.ToInt32(txtEstruAlm.Text));
            }
            else
            {
                catalogo = "INSUMOS";
                datos = DupCodProdEstr.ComponenteConsultarCoincidencias("", 2, 0);
            }


            frmBusquedaGenerica frmBusqueda = new frmBusquedaGenerica(datos, txtEstruComponente.Text, "CLAVE", catalogo);
            frmBusqueda.ShowDialog();
            if (frmBusqueda.RenglonSeleccionado != null)
            {
                txtEstruComponente.Text = frmBusqueda.RenglonSeleccionado["CLAVE"].ToString().Trim();
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnEstruComponente_Click(object sender, EventArgs e)
        {
            DespliegaBusquedaGenericaComponente(113);
        }

        private void txtEstruComponente_Leave(object sender, EventArgs e)
        {
            if (txtEstruComponente.Text != "")
            {
                INSUMOS01 insumos = new INSUMOS01();
                insumos = insumos.Consultar(txtEstruComponente.Text);
                if (insumos.TIPOG != null)
                {
                    lblEstruStatus.Text = "";
                    txtEstruTipo.Text = "INS";
                    txtEstruCostoUni.Text = insumos.CSTO_UNIT.ToString();
                }
                else
                {
                    DataTable datosCosto = DupCodProdEstr.UltimoCostoUnitarioConsultar("PT", txtEstruComponente.Text, Convert.ToInt32(txtEstruAlm.Text));
                    if (datosCosto.Rows.Count > 0)
                    {
                        lblEstruStatus.Text = "";
                        txtEstruTipo.Text = "PT";
                        txtEstruCostoUni.Text = datosCosto.Rows[0]["ULT_COSTO"].ToString();
                    }
                    else
                    {
                        datosCosto = new DataTable();
                        datosCosto = DupCodProdEstr.UltimoCostoUnitarioConsultar("MP", txtEstruComponente.Text, Convert.ToInt32(txtEstruAlm.Text));
                        if (datosCosto.Rows.Count > 0)
                        {
                            lblEstruStatus.Text = "";
                            txtEstruTipo.Text = "MP";
                            txtEstruCostoUni.Text = datosCosto.Rows[0]["ULT_COSTO"].ToString();
                        }
                        else
                        {
                            lblEstruStatus.Text = "El Componente no existe.";
                            txtEstruTipo.Text = "";
                            txtEstruCostoUni.Text = "0.00";
                        }
                    }
                }

            }


        }

        private void txtEstruComponente_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtEstruComponente_KeyDown(object sender, KeyEventArgs e)
        {
            int resp = Convert.ToInt32(e.KeyData);
            int shF2 = Convert.ToInt32(Keys.Shift) + Convert.ToInt32(Keys.F2);
            int shF5 = Convert.ToInt32(Keys.Shift) + Convert.ToInt32(Keys.F5);
            if (resp == shF2 || resp == shF5 || resp == Convert.ToInt32(Keys.F2) || resp == Convert.ToInt32(Keys.F5))
            {
                DespliegaBusquedaGenericaComponente(resp);
            }
        }

        private void btnEstruAgrega_Click(object sender, EventArgs e)
        {

            if (txtEstruAlmPT.Text.Trim() != "" && txtEstruProceso.Text.Trim() != "" && txtEstruCantidad.Text.Trim() != "" && txtEstruAlm.Text.Trim() != "" && txtEstruComponente.Text.Trim() != "" && txtEstruTipo.Text.Trim() != "" && txtEstruCostoUni.Text.Trim() != "")
            {
                if (txtEstruAlmPT.Text != "0" && txtEstruCantidad.Text != "0" && txtEstruAlm.Text != "0" && txtEstruCostoUni.Text != "0")
                {
                    string componente = txtEstruComponente.Text.Trim();
                    if (componente != txtModelo.Text)
                    {
                        DataRow[] estruExistentes = EstructuraExistente.Select(string.Format("COMPONENTE='{0}'", txtEstruComponente.Text));
                        if (estruExistentes.Length == 0)
                        {
                            lblExiste.Text = "";
                            DataRow row = EstructuraExistente.NewRow();
                            row["PT_ALMACEN"] = Convert.ToInt32(txtEstruAlmPT.Text);
                            //row["CLAVE"] = txtEstruProceso.Text;
                            row["PROCESO"] = txtEstruProceso.Text;
                            row["CANTIDAD"] = Convert.ToDecimal(txtEstruCantidad.Text);
                            row["ALMACEN"] = Convert.ToInt32(txtEstruAlm.Text);
                            row["COMPONENTE"] = txtEstruComponente.Text;
                            row["TIPO"] = txtEstruTipo.Text;
                            row["COSTOU"] = Convert.ToDecimal(txtEstruCostoUni.Text);
                            agregandoComponente = true;
                            EstructuraExistente.Rows.Add(row);
                            CalculaCostoPrenda();
                            txtEstruProceso.Text = "";
                            txtEstruComponente.Text = "";
                            txtEstruTipo.Text = "";
                            txtEstruCostoUni.Text = "0.00";
                            txtEstruCantidad.Text = "";
                            txtEstruProceso.Focus();
                        }
                        else
                        {
                            lblEstruStatus.Text = "El Componente ha sido agregado previamente.";
                        }
                    }
                    else
                    {
                        lblEstruStatus.Text = "No puede tenerse a si mismo como subensamble.";
                    }
                }
                else
                {
                    lblEstruStatus.Text = "Captura todos los datos del Componente.";
                }
            }
            else
            {
                lblEstruStatus.Text = "Captura todos los datos del Componente.";
            }
        }

        private void btnEstruQuita_Click(object sender, EventArgs e)
        {
            int registro = dgViewEsctructuraExistente.CurrentRow.Index;
            EstructuraExistente.Rows.RemoveAt(registro);
            CalculaCostoPrenda();
        }
        private void CalculaCostoPrenda()
        {
            //lblCostoPrenda.Text = EstructuraExistente.Compute("Sum(CANTIDAD*COSTOU)", "").ToString();
            double costoPrenda = 0;
            foreach (DataRow row in EstructuraExistente.Rows)
            {
                costoPrenda += Convert.ToDouble(row["CANTIDAD"].ToString()) * Convert.ToDouble(row["COSTOU"].ToString());
            }
            lblCostoPrenda.Text = costoPrenda.ToString();
        }

        private void txtEstruProceso_KeyDown(object sender, KeyEventArgs e)
        {
            int resp = Convert.ToInt32(e.KeyData);
            int shF2 = Convert.ToInt32(Keys.Shift) + Convert.ToInt32(Keys.F2);
            int shF5 = Convert.ToInt32(Keys.Shift) + Convert.ToInt32(Keys.F5);
            if (resp == shF2 || resp == shF5 || resp == Convert.ToInt32(Keys.F2) || resp == Convert.ToInt32(Keys.F5))
            {
                DespliegaBusquedaGenericaProceso();
            }
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidaModelo() && dgViewCodigosExistentes.Rows.Count > 0)
            {
                if (!ValidaAlmacenes())
                {
                    MessageBox.Show("Captura la información del Panel de Almacenes.", "Verifique", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    chkTodos.Focus();
                }
                else
                {
                    if (dgViewEsctructuraExistente.Rows.Count == 0)
                    {
                        MessageBox.Show("Captura la Estructura del Producto Terminado.", "Verifique", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtEstruProceso.Focus();
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(txtEstruAlmPT.Text))
                        {
                            MessageBox.Show("Captura el Almacén para PT.", "Verifique", MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                            txtEstruAlmPT.Focus();
                        }
                        else
                        {
                            DialogResult resp = MessageBox.Show("¿Están correctos los datos?", "Confirme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (resp == DialogResult.Yes)
                            {
                                DataTable dtTallas = (DataTable)dgViewCodigosExistentes.DataSource;
                                DataTable dtEstr = (DataTable)dgViewEsctructuraExistente.DataSource;

                                List<int> lstAlmacenes = RegresaAlmacenesSeleccionados();


                                Exception ex = null;
                                //TODO: Guardar
                                this.Cursor = Cursors.WaitCursor;
                                DupCodProdEstr.Guardar2(
                                                        txtModelo.Text,
                                                        txtLineaProd.Text,
                                                        double.Parse(txtPrecioPublico.Text),
                                                        double.Parse(lblCostoPrenda.Text),
                                                        short.Parse(txtEstruAlmPT.Text),
                                                        lstAlmacenes,
                                                        dtTallas,
                                                        dtEstr,
                                                        ref ex,
                                                        txtObservaciones.Text.Trim(),
                                                        double.Parse(txtPeso.Text),
                                                        txtDescripcionEtiqueta.Text.Trim(),
                                                        txtAgrupacion.Text.Trim()
                                                        );

                                if (ex == null)
                                {
                                    this.Cursor = Cursors.Default;
                                    MessageBox.Show("Códigos y Estructuras guardados correctamente.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //llamar al clic botón Reestablecer...
                                    btnReestablecer_Click(sender, e);
                                }
                                else
                                {
                                    this.Cursor = Cursors.Default;
                                    groupBoxPrincipal.Enabled = true;
                                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }
            }
        }

        private List<int> RegresaAlmacenesSeleccionados()
        {
            List<int> lstAlmacenes = new List<int>();
            foreach (Control control in groupBox2.Controls)
            {
                if (control is CheckBox)
                {
                    CheckBox chkAlmacen = (CheckBox)control;

                    if (chkAlmacen.Tag != null && chkAlmacen.Checked)
                    {
                        lstAlmacenes.Add(int.Parse(chkAlmacen.Tag.ToString()));
                    }

                }
            }
            return lstAlmacenes;
        }
        private bool ValidaModelo()
        {

            decimal precioPublico = 0;
            decimal peso = 0;
            decimal.TryParse(txtPrecioPublico.Text, out precioPublico);
            decimal.TryParse(txtPeso.Text, out peso);

            if (txtModelo.Text.Length == 8 && !string.IsNullOrEmpty(txtDescripcion.Text) &&
                !string.IsNullOrEmpty(txtLineaProd.Text) && precioPublico != 0 && peso != 0)
            {
                if (txtObservaciones.Text.Length > 255)
                    return false;
                return true;
            }
            else
            {
                return false;
            }


        }
        private bool ValidaAlmacenes()
        {
            foreach (Control control in groupBox2.Controls)
            {
                CheckBox chkAlmacen = control as CheckBox;
                if (chkAlmacen != null)
                {
                    if (chkAlmacen.Checked)
                        return true;

                }
            }
            return false;
        }

        void LimpiaValores()
        {
            groupBoxPrincipal.Enabled = true;
            Utiles.FuncionalidadesFormularios.LimpiaobjetosPorTipo<TextBox>(this, new TextBox());
            Utiles.FuncionalidadesFormularios.LimpiaobjetosPorTipo<CheckBox>(this, new CheckBox());
            modeloCapturado = false;
            txtSufijoDescripcion.Text = "TALLA XXYY";
            txtEstruAlmPT.Text = "1";
            txtEstruCantidad.Text = "1.00";
            txtEstruAlm.Text = "1";
            ((DataTable)dgViewCodigosExistentes.DataSource).Rows.Clear();
            ((DataTable)dgViewEsctructuraExistente.DataSource).Rows.Clear();
            lblCostoPrenda.Text = "0.00";
            txtDescripcionEtiqueta.Text = "";
            txtAgrupacion.Text = "";
            txtModelo.Focus();
        }

        private void btnReestablecer_Click(object sender, EventArgs e)
        {
            LimpiaValores();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDupCodProdEstr_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (modeloCapturado)
            {
                DialogResult resp = MessageBox.Show("Los cambios no se han guardado. ¿Deseas cancelar su captura?", "Confirme",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (resp == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void dgViewCodigosExistentes_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            string status = dgViewCodigosExistentes.Rows[e.RowIndex].Cells["ESTATUS"].Value.ToString();
            string accion = dgViewCodigosExistentes.Rows[e.RowIndex].Cells["Accion"].Value.ToString();

            if (accion == "Agregar")
            {
                dgViewCodigosExistentes.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Blue;
            }
            else if (status.ToUpper() == "BAJA")
            {
                dgViewCodigosExistentes.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
            }
            else
            {
                if (accion == "Actualizar" && status.ToUpper() == "ACTIVO")
                {
                    e.CellStyle.ForeColor = SystemColors.WindowText;
                }
            }
        }

        private void NumeraRenglones()
        {
            for (int i = 0; i < dgViewCodigosExistentes.Rows.Count; i++)
            {
                dgViewCodigosExistentes.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }
        }

        private void dgViewCodigosExistentes_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            NumeraRenglones();
            Totales();
        }

        private void Totales()
        {
            DataTable dtTallas = (DataTable)dgViewCodigosExistentes.DataSource;

            try
            {
                txtBajas.Text =
                    dtTallas.AsEnumerable().Count(t => t["ESTATUS"].ToString().ToUpper() == "BAJA").ToString();
                txtActivos.Text =
                    dtTallas.AsEnumerable().Count(t => t["ESTATUS"].ToString().ToUpper() == "ACTIVO").ToString();
                txtTotal.Text = dtTallas.Rows.Count.ToString();
            }
            catch (Exception Ex) { }

        }

        private void dgViewCodigosExistentes_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgViewEsctructuraExistente_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex == dgViewEsctructuraExistente.Rows.Count - 1)
            {
                if (agregandoComponente)
                {
                    dgViewEsctructuraExistente.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Blue;
                    agregandoComponente = false;

                }
            }
        }


        private void btnEstuInsum_Click(object sender, EventArgs e)
        {
            DespliegaBusquedaGenericaComponente(Convert.ToInt32(Keys.F5));
        }

        private void txtEstruComponente_TextChanged(object sender, EventArgs e)
        {
            txtEstruTipo.Text = "";
            txtEstruCostoUni.Text = "0";
        }

        private void txtTalla_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAgregarTalla_Click(sender, new EventArgs());
            }

        }

        private void frmDupCodProdEstr_Load(object sender, EventArgs e)
        {
            if (txtModelo.Text != "")
                txtModelo_Leave(null, null);
        }

        private void txtAgrupacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
