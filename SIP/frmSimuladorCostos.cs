using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIP.UserControls;
using SIP.Utiles;
using ulp_bl;


namespace SIP
{
    public partial class frmSimuladorCostos : Form
    {

        private delegate void DelAsignaEnfoque(Control Control);
        private delegate void DelAsignaValorControl(Control Control, string Valor);
        private delegate void DelAsignaDataSource(DataTable DataSource);
        private delegate void DelCalculaCostoPrimo();
        private delegate void DelCalculaUtilidad();
        private delegate void DelAsiginaVisibilidadPanel(bool Valor);
        private delegate void DelAsignaTallasPreviasYSiguientes(string Modelo);

        private bool EnfoqueSoloPara4 = false;
        private List<string> modelosTallas;
        private Enumerados.TipoSimulador tipoSimulador;
        private Precarga precarga;
        private BackgroundWorker bgw;
        private BackgroundWorker bgw2;
        public Boolean precioModificado = false;
        public decimal precio = 0;

        public frmSimuladorCostos(Enumerados.TipoSimulador TipoSimulador)
        {
            InitializeComponent();
            tipoSimulador = TipoSimulador;
            precarga = new Precarga(this);
        }
        public frmSimuladorCostos(Enumerados.TipoSimulador TipoSimulador, String _modelo)
        {
            InitializeComponent();
            tipoSimulador = TipoSimulador;
            precarga = new Precarga(this);
            txtModelo.Text = _modelo;
            txtModelo_Leave(null, null);
            txtTotal.Focus();

        }

        private void dgViewComponentes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void CalculaCostoPrimo()
        {

            if (this.InvokeRequired)
            {
                DelCalculaCostoPrimo delCalculaCostoPrimo = new DelCalculaCostoPrimo(CalculaCostoPrimo);
                this.Invoke(delCalculaCostoPrimo);
            }
            else
            {

                DataTable dtEstructura = (DataTable)dgViewComponentes.DataSource;
                if (dtEstructura == null)
                    return;

                double sumaDeSubtotal = (double)dtEstructura.Compute("SUM(CSubt)", null);
                txtCostoPrimo.Text = Math.Round(sumaDeSubtotal, 2).ToString();
                CalculaPorcentajeAAplicar();
            }
        }

        private void CalculaUtilidad()
        {
            if (this.InvokeRequired)
            {
                DelCalculaUtilidad delCalculaUtilidad = new DelCalculaUtilidad(CalculaUtilidad);
                this.Invoke(delCalculaUtilidad);
            }
            else
            {
                if (!String.IsNullOrEmpty(txtTotal.Text))
                {
                    //OBTENEMOS EL PORCENTAJE DE INTEGRACION...
                    decimal CP = Math.Round(Decimal.Parse(txtCostoPrimo.Text), 2); //COSTO PRIMO
                    decimal MARGEN = Math.Round(Decimal.Parse(txtMargen.Text), 2); //MARGEN
                    decimal TOTAL = Math.Round(Decimal.Parse(txtTotal.Text), 2); // PRECIO FINAL


                    decimal APLICAR = Math.Round((((CP) / (MARGEN * TOTAL) * 100) * 100), 2);
                    decimal UTILIDAD = 100 - APLICAR - Math.Round(Decimal.Parse(txtPorcGtosOpera.Text), 2);

                    AsignaValorControl(txtPorcUtilidad, UTILIDAD.ToString());
                    AsignaValorControl(txtPorcAplicar, APLICAR.ToString());
                    txtSubPrecio.Text = (Math.Round((Convert.ToDouble(txtCostoPrimo.Text) / Convert.ToDouble(txtPorcAplicar.Text)) * 100, 2)).ToString();
                    CalculatSubt1();
                    CalculatSubt2();
                    CalculaPorcentajeIncremento();
                }


            }
        }

        private void frmSimuladorCostos_Load(object sender, EventArgs e)
        {
            Exception ex = new Exception();
            dgViewComponentes.CellEndEdit += dgViewComponentes_CellEndEdit;
            dgViewComponentes.CellClick += dgViewComponentes_CellClick;
            txtPorcGtosOpera.KeyUp += txtPorcentaje_TextChanged;
            txtPorcUtilidad.KeyUp += txtPorcentaje_TextChanged;
            txtMargen.KeyUp += txtMargen_TextChanged;
            txtPrecioAnterior.KeyUp += txtPrecioAnterior_TextChanged;
            txtSubPrecio.KeyUp += txtSubPrecio_TextChanged;

            txtTotal.KeyUp += txtTotal_KeyUp;

            if (tipoSimulador == Enumerados.TipoSimulador.EstructuraDeProducto)
            {
                dgViewComponentes.Columns["CPreci_Simulado"].Visible = false;
                dgViewComponentes.Columns["CPreci"].DefaultCellStyle = new DataGridViewCellStyle();
                dgViewComponentes.Columns["CPreci"].ReadOnly = false;
                CActuali.Visible = false;
                CCambiaComp.Visible = false;
                btnExportarExcel.Enabled = false;
                this.Height = 500;
                this.Text = "Estructura de Producto";
            }
            this.dgViewComponentes.AutoGenerateColumns = false;
        }

        private void txtTotal_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                CalculaUtilidad();
            }
            catch { }
        }

        private void txtPrecioAnterior_TextChanged(object sender, EventArgs e)
        {
            CalculaPorcentajeIncremento();
        }

        void txtMargen_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CalculaPrecioFinal();
            }
            catch { }
        }

        void txtSubPrecio_TextChanged(object sender, EventArgs e)
        {
            CalculatSubt1();
            CalculatSubt2();
            CalculaPrecioFinal();
        }

        private void txtPorcentaje_TextChanged(object sender, EventArgs e)
        {
            try
            {
                NumericTextBox txtObj = (NumericTextBox)sender;

                CalculaCostoPrimo();
            }
            catch { }

        }


        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CalculaUtilidad();
            }
            catch { }
        }

        void dgViewComponentes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7)
            {
                Cursor = Cursors.WaitCursor;
                DataTable dtEstructura = (DataTable)dgViewComponentes.DataSource;
                string componente = Convert.ToString(dtEstructura.Rows[e.RowIndex]["CCod"]);
                double precio = Convert.ToDouble(dtEstructura.Rows[e.RowIndex]["CPreci_Simulado"]);
                int NumeroRegistro = Convert.ToInt32(dtEstructura.Rows[e.RowIndex]["CReg"]);
                Exception ex = SimuladorCostos.ActualizaPrecio(componente, precio, NumeroRegistro);
                Cursor = Cursors.Default;
                if (ex != null)
                {
                    MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }

        }

        void dgViewComponentes_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4 || e.ColumnIndex == 5 || e.ColumnIndex == 6)
            {
                DataTable dtEstructura = (DataTable)dgViewComponentes.DataSource;
                double cantidad = 0;
                if (dtEstructura.Rows[e.RowIndex]["CCant"] != DBNull.Value)
                {
                    cantidad = Convert.ToDouble(dtEstructura.Rows[e.RowIndex]["CCant"]);
                }
                else
                {
                    dtEstructura.Rows[e.RowIndex].SetField<double>(4, 0);
                }
                double precio = 0;
                if (tipoSimulador == Enumerados.TipoSimulador.SimuladorDeCostos)
                {


                    if (dtEstructura.Rows[e.RowIndex]["CPreci_Simulado"] != DBNull.Value)
                    {
                        precio = Convert.ToDouble(dtEstructura.Rows[e.RowIndex]["CPreci_Simulado"]);
                    }
                    else
                    {
                        dtEstructura.Rows[e.RowIndex].SetField<double>(6, 0);
                    }
                }
                else if (tipoSimulador == Enumerados.TipoSimulador.EstructuraDeProducto)
                {
                    if (dtEstructura.Rows[e.RowIndex]["CPreci"] != DBNull.Value)
                    {
                        precio = Convert.ToDouble(dtEstructura.Rows[e.RowIndex]["CPreci"]);
                    }
                    else
                    {
                        dtEstructura.Rows[e.RowIndex].SetField<double>(5, 0);
                    }
                }
                dtEstructura.Rows[e.RowIndex]["CSubt"] = Math.Round(cantidad * precio, 2);
                CalculaCostoPrimo();
                dgViewComponentes.Refresh();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void gbComponentesCostos_Enter(object sender, EventArgs e)
        {

        }

        private void txtModelo_Leave(object sender, EventArgs e)
        {

            if (txtModelo.Text.Trim() == "")
                return;

            bgw2 = new BackgroundWorker();
            precarga.MostrarEspera();
            precarga.AsignastatusProceso("Buscando información...");
            bgw2.DoWork += bgw2_DoWork;
            bgw2.RunWorkerCompleted += bgw2_RunWorkerCompleted;
            bgw2.RunWorkerAsync();

        }

        private void AsignaVisibilidad(bool Valor)
        {
            if (panelTallas.InvokeRequired)
            {
                DelAsiginaVisibilidadPanel delPanel = new DelAsiginaVisibilidadPanel(AsignaVisibilidad);
                panelTallas.Invoke(delPanel, new object[] { Valor });
            }
            else
            {
                panelTallas.Visible = Valor;
            }
        }
        private void AsignaValorControl(Control Control, string Valor)
        {
            if (Control.InvokeRequired)
            {
                DelAsignaValorControl delAsignaValorControl = new DelAsignaValorControl(AsignaValorControl);
                Control.Invoke(delAsignaValorControl, new object[] { Control, Valor });
            }
            else
            {
                Control.Text = Valor;
            }
        }
        private void AsignaEnfoque(Control Control)
        {
            if (Control.InvokeRequired)
            {
                DelAsignaEnfoque delAsignaEnfoque = new DelAsignaEnfoque(AsignaEnfoque);

                Control.BeginInvoke(delAsignaEnfoque, new object[] { Control });
            }
            else
            {
                Control.Focus();
            }
        }
        private void AsignaDataSource(DataTable DataTable)
        {
            if (dgViewComponentes.InvokeRequired)
            {
                DelAsignaDataSource delAsignaDataSource = new DelAsignaDataSource(AsignaDataSource);
                dgViewComponentes.Invoke(delAsignaDataSource, new object[] { DataTable });
            }
            else
            {
                dgViewComponentes.DataSource = DataTable;
            }
        }
        private void bgw2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            precarga.RemoverEspera();
        }

        private void bgw2_DoWork(object sender, DoWorkEventArgs e)
        {
            Exception ex = SimuladorCostos.LogitudValida(txtModelo.Text);
            if (ex == null)
            {
                DataTable dtInfoModelo = SimuladorCostos.ModeloExistente(txtModelo.Text, ref ex);

                if (ex == null)
                {
                    AsignaVisibilidad(true);

                    //modelosTallas.Clear();
                    modelosTallas = SimuladorCostos.RegresaTallas(txtModelo.Text.Substring(0, 8));
                    AsignaTallasPreviasYSiguientes(txtModelo.Text);
                    AsignaValorControl(lblDescripcion, Convert.ToString(dtInfoModelo.Rows[0]["DESCR"]));
                    //lblDescripcion.Text = Convert.ToString(dtInfoModelo.Rows[0]["DESCR"]);
                    AsignaValorControl(txtPrecioAnterior, Convert.ToString(dtInfoModelo.Rows[0]["PRECIO"]));
                    //txtPrecioAnterior.Text = Convert.ToString(dtInfoModelo.Rows[0]["PRECIO"]);
                    //dgViewComponentes.DataSource = SimuladorCostos.RegresaEstructuraModelo(txtModelo.Text, ref ex);
                    DataTable dtEstructura = SimuladorCostos.RegresaEstructuraModelo(txtModelo.Text, tipoSimulador, ref ex);
                    AsignaDataSource(dtEstructura);
                    //lblFecha.Text = String.Format("Fecha de actualización: {0}", Convert.ToDateTime(dtEstructura.Compute("max([CFecha])", string.Empty)).ToString("dd/MM/yyyy HH:mm:ss"));
                    //txtPorcGtosOpera.Text = "24";
                    AsignaValorControl(txtPorcGtosOpera, "24");
                    AsignaValorControl(lblFecha, String.Format("Fecha de actualización: {0}", Convert.ToDateTime(dtEstructura.Compute("max([CFecha])", string.Empty)).ToString("dd/MM/yyyy HH:mm:ss")));
                    //txtPorcUtilidad.Text = "5";
                    AsignaValorControl(txtPorcUtilidad, "5");
                    CalculaCostoPrimo();


                    AsignaValorControl(txtTotal, dtInfoModelo.Rows[0]["PRECIO"].ToString());
                    CalculaUtilidad();



                }
                else
                {
                    AsignaVisibilidad(false);
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    AsignaEnfoque(txtModelo);
                }
            }
            else
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                AsignaEnfoque(txtModelo);
            }
        }
        private void CalculaPorcentajeAAplicar()
        {
            double costoPrimo = Convert.ToDouble(txtPrecioAnterior.Text);
            if (!string.IsNullOrEmpty(txtPorcUtilidad.Text) && !string.IsNullOrEmpty(txtPorcGtosOpera.Text))
            {
                txtPorcAplicar.Text = (100 - (Convert.ToDouble(txtPorcGtosOpera.Text) + Convert.ToDouble(txtPorcUtilidad.Text))).ToString();
                CalculaIntegracion();
            }
        }

        private void CalculaIntegracion()
        {
            txtSubPrecio.Text = (Math.Round((Convert.ToDouble(txtCostoPrimo.Text) / Convert.ToDouble(txtPorcAplicar.Text)) * 100, 2)).ToString();
            CalculatSubt1();
            CalculatSubt2();
            CalculaPrecioFinal();
        }

        private void CalculatSubt1()
        {
            if (!string.IsNullOrEmpty(txtSubPrecio.Text))
            {
                txtSubt.Text = Math.Round(Convert.ToDouble(txtSubPrecio.Text) * (Convert.ToDouble(txtPorcGtosOpera.Text) / 100), 2).ToString();
            }
        }
        private void CalculatSubt2()
        {
            if (!string.IsNullOrEmpty(txtSubPrecio.Text))
            {
                txtSubt2.Text = Math.Round(Convert.ToDouble(txtSubPrecio.Text) * (Convert.ToDouble(txtPorcUtilidad.Text) / 100), 2).ToString();
            }
        }
        private void CalculaPrecioFinal()
        {

            //if (txtSubPrecio.Text == string.Empty)
            //    return;


            //if (txtMargen.Text == string.Empty || txtMargen.Text == "0")
            //    return;

            //try
            //{
            if (!string.IsNullOrEmpty(txtMargen.Text) && !string.IsNullOrEmpty(txtSubPrecio.Text))
            {
                txtTotal.Text =
                    (Math.Round((Convert.ToDouble(txtSubPrecio.Text) / Convert.ToDouble(txtMargen.Text)) * 100, 2)).ToString();
                CalculaPorcentajeIncremento();
            }
            //}
            //catch {}
        }
        private void CalculaPorcentajeIncremento()
        {
            if (!string.IsNullOrEmpty(txtTotal.Text) && !string.IsNullOrEmpty(txtPrecioAnterior.Text))
            {
                txtPorcIncr.Text = (((Convert.ToDouble(txtTotal.Text) / Convert.ToDouble(txtPrecioAnterior.Text)) - 1) * 100).ToString("N2");
            }
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            precarga.MostrarEspera();
            bgw = new BackgroundWorker();
            bgw.DoWork += bgw_DoWork;
            bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
            bgw.RunWorkerAsync();
        }

        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            precarga.RemoverEspera();
        }

        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            precarga.AsignastatusProceso("Generando archivo de Excel");
            SimuladorCostos simuladorCostos = new SimuladorCostos();
            simuladorCostos.Modelo = txtModelo.Text;
            simuladorCostos.ModeloDescripcion = lblDescripcion.Text;
            simuladorCostos.ModeloEstructura = (DataTable)dgViewComponentes.DataSource;
            simuladorCostos.CostoPrimo = Convert.ToDouble(txtCostoPrimo.Text);
            simuladorCostos.PorcentajeGastosOperacion = Convert.ToDouble(txtPorcGtosOpera.Text);
            simuladorCostos.PorcentajeUtilidad = Convert.ToDouble(txtPorcUtilidad.Text);
            simuladorCostos.PorcentajeAAplicar = Convert.ToDouble(txtPorcAplicar.Text);
            simuladorCostos.Margen = Convert.ToDouble(txtMargen.Text);
            simuladorCostos.PrecioAnterior = Convert.ToDouble(txtPrecioAnterior.Text);
            simuladorCostos.SubT1 = Convert.ToDouble(txtSubt.Text);
            simuladorCostos.SubT2 = Convert.ToDouble(txtSubt2.Text);
            simuladorCostos.Integracion = Convert.ToDouble(txtSubPrecio.Text);
            simuladorCostos.PrecioFinal = Convert.ToDouble(txtTotal.Text);
            simuladorCostos.PorcentajeIncremento = Convert.ToDouble(txtPorcIncr.Text);

            string archivoTemporal = System.IO.Path.GetTempFileName().Replace(".tmp", ".xls");

            SimuladorCostos.GeneraArchivoExcel(simuladorCostos, archivoTemporal);

            //System.Diagnostics.Process.Start(archivoTemporal);
            FuncionalidadesFormularios.MostrarExcel(archivoTemporal);
        }

        private void txtModelo_TextChanged(object sender, EventArgs e)
        {
            LimpiaControles();
        }

        private void LimpiaControles()
        {
            lblDescripcion.Text = "";
            if (dgViewComponentes.DataSource != null)
                ((DataTable)dgViewComponentes.DataSource).Clear();

            txtCostoPrimo.Text = "0";
            //txtMargen.Text = "0";            
            //txtPorcAplicar.Text = "0";
            //txtPorcGtosOpera.Text = "24";
            //txtPorcIncr.Text = "0";
            //txtPorcUtilidad.Text = "5";
            //txtPrecioAnterior.Text = "0";
            //txtSubPrecio.Text = "0";
            //txtSubt.Text = "0";
            //txtSubt2.Text = "0";
            //txtTotal.Text = "0";



        }

        private void txtModelo_KeyDown(object sender, KeyEventArgs e)
        {
            if (tipoSimulador == Enumerados.TipoSimulador.EstructuraDeProducto)
            {
                if (e.KeyCode == Keys.Down)
                {
                    string siguienteModelo = SimuladorCostos.RegresaSiguienteTalla(txtModelo.Text, modelosTallas, DireccionBusqueda.Siguiente);
                    if (siguienteModelo != string.Empty)
                    {
                        txtModelo.Text = siguienteModelo;
                        AsignaTallasPreviasYSiguientes(siguienteModelo);
                        SendKeys.Send("{TAB}");
                        SendKeys.Send("+{TAB}");
                        //EnfoqueSoloPara4 = true;

                    }
                }
                if (e.KeyCode == Keys.Up)
                {
                    string siguienteModelo = SimuladorCostos.RegresaSiguienteTalla(txtModelo.Text, modelosTallas,
                        DireccionBusqueda.Anterior);
                    if (siguienteModelo != string.Empty)
                    {
                        txtModelo.Text = siguienteModelo;
                        AsignaTallasPreviasYSiguientes(siguienteModelo);
                        SendKeys.Send("{TAB}");
                        SendKeys.Send("+{TAB}");
                        //EnfoqueSoloPara4 = true;
                    }
                }
            }
        }

        private void txtModelo_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txtModelo_Enter(object sender, EventArgs e)
        {
            //if (EnfoqueSoloPara4)
            //{
            //    txtModelo.SelectAllOnFocus = false;
            //    //txtModelo.Select(txtModelo.Text.Length - 4, 4);               
            //    txtModelo.SelectionStart = txtModelo.Text.Length - 4;
            //    txtModelo.SelectionLength = 4;
            //    EnfoqueSoloPara4 = false;                
            //}
        }

        private void AsignaTallasPreviasYSiguientes(string modelo)
        {


            if (lnkPrev.InvokeRequired && lnkSig.InvokeRequired)
            {
                DelAsignaTallasPreviasYSiguientes delTallasSiguientes = new DelAsignaTallasPreviasYSiguientes(AsignaTallasPreviasYSiguientes);
                this.Invoke(delTallasSiguientes, new object[] { modelo });
            }
            else
            {

                string prev = SimuladorCostos.RegresaSiguienteTalla(modelo, modelosTallas, DireccionBusqueda.Anterior);
                string sig = SimuladorCostos.RegresaSiguienteTalla(modelo, modelosTallas, DireccionBusqueda.Siguiente);
                if (prev != string.Empty)
                {
                    lnkPrev.Text = prev.Substring(prev.Length - 4, 4);
                }
                else
                {
                    lnkPrev.Text = "--";
                }
                if (sig != string.Empty)
                {
                    lnkSig.Text = sig.Substring(sig.Length - 4, 4);
                }
                else
                {
                    lnkSig.Text = "--";
                }
                //var j = 0;
                //var i = 2;
                //var r = i/j;

            }
        }

        private void dgViewComponentes_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.ColumnIndex == 7)
            {
                e.ToolTipText = "Actualizar este precio para todos los productos";
            }
        }

        private void dgViewComponentes_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {

            if (dgViewComponentes.CurrentCell.ColumnIndex == 4 || dgViewComponentes.CurrentCell.ColumnIndex == 5 || dgViewComponentes.CurrentCell.ColumnIndex == 6)
            {
                dgViewComponentes.CommitEdit(DataGridViewDataErrorContexts.Commit);
                dgViewComponentes.Refresh();
                dgViewComponentes_CellEndEdit(sender, new DataGridViewCellEventArgs(dgViewComponentes.CurrentCell.ColumnIndex, dgViewComponentes.CurrentCell.RowIndex));
            }
        }

        private void dgViewComponentes_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("El valor escrito es inválido", e.Exception.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void lnkSig_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void btnGuardarPrecio_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea modificar el precio del código seleccionado?", "Asignación de precio", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                ulp_bl.SOLICITUDES_ESPECIALES.AsignaPrecioCodigo(txtModelo.Text, decimal.Parse(txtTotal.Text));
                MessageBox.Show("Precio modificado de forma correcta", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.precioModificado = true;
                this.precio = decimal.Parse(txtTotal.Text);
            }

        }

    }
}
