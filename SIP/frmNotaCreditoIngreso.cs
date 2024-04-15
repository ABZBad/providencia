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
using SIP.Utiles;
using System.IO;

namespace SIP
{
    public partial class frmNotaCreditoIngreso : Form
    {
        #region Variables y constructores
        BackgroundWorker bgwLoad;
        BackgroundWorker bgwNotaCredito;
        Precarga precarga;

        string factura = "";
        string uuidFactura = "";
        DataSet dsFactura;
        String error = "";
        String observaciones = "";
        String serie, folio, clave_cliente, almacen = "";
        Boolean cancelacion = false;
        DataTable dtResult;

        // VARIABLES PARA CFDI33
        DataTable dtEncabezado = new DataTable();
        DataTable dtDetalle = new DataTable();

        public frmNotaCreditoIngreso(String _factura)
        {
            InitializeComponent();
            this.factura = _factura;
            precarga = new Precarga(this);
            bgwLoad = new BackgroundWorker();
            bgwLoad.DoWork += bgwLoad_DoWork;
            bgwLoad.RunWorkerCompleted += bgwLoad_RunWorkerCompleted;
            bgwNotaCredito = new BackgroundWorker();
            bgwNotaCredito.DoWork += bgwNotaCredito_DoWork;
            bgwNotaCredito.RunWorkerCompleted += bgwNotaCredito_RunWorkerCompleted;
            dgvDetalle.AutoGenerateColumns = false;
        }
        #endregion
        #region Eventos
        private void frmNotaCreditoDetalle_Load(object sender, EventArgs e)
        {
            precarga.MostrarEspera();
            precarga.AsignastatusProceso("Cargando información de la factura...");
            // CARGAMOS FACTURA
            bgwLoad.RunWorkerAsync();
        }
        private void dgvDetalle_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var dataGridView = sender as DataGridView;
            if (dataGridView.Rows[e.RowIndex].Selected)
            {
                e.CellStyle.Font = new Font(new FontFamily(e.CellStyle.Font.Name), e.CellStyle.Font.Size - 1, FontStyle.Bold);
                // edit: to change the background color:
                //e.CellStyle.SelectionBackColor = Color.Coral;
            }
        }
        private void dgvDetalle_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.Value);
            if (e.Value != null)
            {
                e.CellStyle.ForeColor = Color.Blue;

            }
        }
        private void dgvDetalle_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            this.CalculaTotales();
        }
        private void dgvDetalle_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            this.CalculaTotales();
        }
        private void dgvDetalle_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.CalculaTotales();
        }
        private void cmbSerie_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            if (cmbSerie.SelectedValue != null)
            {
                this.oComprobante33.Serie = Enum.Parse(typeof(CFDIPAC.SeriesNC), cmbSerie.SelectedValue.ToString()).ToString();
                if (cmbSerie.Text == "D")
                {
                    List<String> lst = new List<string> { };
                    lst.Add("5");
                    cmbAlmacen.DataSource = lst;
                }
                if (cmbSerie.Text == "C")
                {
                    List<String> lst = new List<string> { };
                    lst.Add("4");
                    lst.Add("6");
                    cmbAlmacen.DataSource = lst;
                }
            }
             * */

        }
        private void btnGenerar_Click(object sender, EventArgs e)
        {
            precarga.MostrarEspera();
            precarga.AsignastatusProceso("Generando Nota de Crédito...");
            bgwNotaCredito.RunWorkerAsync();

        }
        private void dgvDetalle_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            this.CalculaTotales();
        }

        #endregion
        #region Workers
        void bgwLoad_DoWork(object sender, DoWorkEventArgs e)
        {
            this.dsFactura = ulp_bl.CFDIPAC.getDetalleFactura(this.factura);
            if (dsFactura.Tables.Count == 0)
            {
                return;
            }
            dtEncabezado = this.dsFactura.Tables[0];
            dtDetalle = this.dsFactura.Tables[1];
            //CARGAMOS LOS DATOS ADICIONALES PARA LA VISTA IMPRESA
            this.uuidFactura = dtEncabezado.Rows[0]["UUID"].ToString();
        }
        void bgwLoad_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            precarga.RemoverEspera();
            if (this.dsFactura.Tables.Count == 0)
            {
                MessageBox.Show("No se pudo cargar el detalle de la factura.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            dgvDetalle.DataSource = dtDetalle;
            this.CalculaTotales();
            lblClienteRFC.Text = "RFC: " + dtEncabezado.Rows[0]["ClienteRFC"].ToString().ToUpper().Trim().Replace("-", "");
            lblClienteNombre.Text = "Nombre: " + dtEncabezado.Rows[0]["ClienteNombre"].ToString().ToUpper().Trim();
            lblClienteClave.Text = "Clave: " + dtEncabezado.Rows[0]["ClienteClave"].ToString().ToUpper().Trim();
            this.clave_cliente = dtEncabezado.Rows[0]["ClienteClave"].ToString().ToUpper().Trim();
            lblSerie.Text = "Serie: " + dtEncabezado.Rows[0]["Serie"].ToString().ToUpper().Trim();
            this.serie = dtEncabezado.Rows[0]["Serie"].ToString().ToUpper().Trim();
            lblFolio.Text = "Folio: " + dtEncabezado.Rows[0]["Folio"].ToString().ToUpper().Trim();
            this.folio = dtEncabezado.Rows[0]["Folio"].ToString().ToUpper().Trim();
            lblAlmacen.Text = "Almacén: " + dtEncabezado.Rows[0]["Almacen"].ToString().ToUpper().Trim();
            this.almacen = dtEncabezado.Rows[0]["Almacen"].ToString().ToUpper().Trim();

        }
        void bgwNotaCredito_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                // GUARDAMOS EN BD
                string _xmlDetalle = "<conceptos>";
                foreach (DataRow _detalle in dtDetalle.Rows)
                {
                    if (_detalle["Seleccion"].ToString() == "1")
                    {
                        _xmlDetalle += "<concepto>";
                        _xmlDetalle += "<ClaveProdServ>" + _detalle["ClaveProductoServicio"].ToString() + "</ClaveProdServ>";
                        _xmlDetalle += "<NoIdentificacion>" + _detalle["Clave"].ToString() + "</NoIdentificacion>";
                        _xmlDetalle += "<Cantidad>" + _detalle["Cantidad"].ToString() + "</Cantidad>";
                        _xmlDetalle += "<ClaveUnidad>" + _detalle["ClaveUnidad"].ToString() + "</ClaveUnidad>";
                        _xmlDetalle += "<Unidad>" + _detalle["Unidadventa"].ToString() + "</Unidad>";
                        _xmlDetalle += "<Descripcion>" + _detalle["Descripcion"].ToString() + "</Descripcion>";
                        _xmlDetalle += "<ValorUnitario>" + _detalle["Precio"].ToString() + "</ValorUnitario>";
                        _xmlDetalle += "<Importe>" + _detalle["Subtotal"].ToString() + "</Importe>";
                        _xmlDetalle += "<IVA>" + _detalle["IVAImporte"].ToString() + "</IVA>";
                        _xmlDetalle += "</concepto>";
                    }
                }
                _xmlDetalle += "</conceptos>";
                dtResult = CFDIPAC.setAltaNCIngreso(_xmlDetalle, int.Parse(this.almacen), this.serie, int.Parse(this.folio), this.clave_cliente, this.cancelacion);
            }
            catch (Exception ex)
            {
                this.error = "Excepción: " + ex.Message + ". " + ex.InnerException;
            }
        }
        void bgwNotaCredito_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            precarga.RemoverEspera();
            if (this.error != "")
            {
                MessageBox.Show(this.error, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Proceso finalizado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // GENERAMOS EXCEL DE SALIDA CON LOS MOVIMIENTOS
                precarga.AsignastatusProceso("Generando archivo de Excel");
                string archivoTemporal = System.IO.Path.GetTempFileName().Replace(".tmp", ".xls");
                CFDIPAC.GeneraArchivoExcelMovimientos(this.cancelacion, DateTime.Now, this.serie, this.folio, this.clave_cliente, this.GetMovimientos(this.dtResult), archivoTemporal);
                FuncionalidadesFormularios.MostrarExcel(archivoTemporal);
                precarga.RemoverEspera();
                this.Close();
            }
        }
        #endregion
        #region Metodos
        private void CalculaTotales()
        {
            decimal subtotal = 0;
            decimal iva = 0;
            decimal total = 0;

            foreach (DataRow dr in dtDetalle.Rows)
            {
                if (dr["Seleccion"].ToString() == "1")
                {
                    subtotal += decimal.Parse(dr["Subtotal"].ToString());
                    iva += decimal.Parse(dr["Subtotal"].ToString()) * decimal.Parse(dr["IVATasa"].ToString());
                    total += decimal.Parse(dr["Subtotal"].ToString()) + decimal.Parse(dr["Subtotal"].ToString()) * (decimal.Parse(dr["IVATasa"].ToString()));
                }

            }
            lblSubtotalImporte.Text = subtotal.ToString("C2");
            lblIVAImporte.Text = iva.ToString("C2");
            lblTotalImporte.Text = total.ToString("C2");
        }
        private List<MINVE01> GetMovimientos(DataTable dtResult)
        {
            List<MINVE01> Movimientos = new List<MINVE01> { };
            foreach (DataRow dr in dtResult.Rows)
            {
                MINVE01 oMINVE01 = new MINVE01();
                oMINVE01.NUM_MOV = int.Parse(dr["NUM_MOV"].ToString());
                oMINVE01.ALMACEN = int.Parse(dr["ALMACEN"].ToString());
                oMINVE01.FECHA_DOCU = DateTime.Parse(dr["FECHA_DOCU"].ToString());
                oMINVE01.REFER = dr["REFER"].ToString();
                oMINVE01.CVE_ART = dr["CVE_ART"].ToString();
                oMINVE01.CANT = double.Parse(dr["CANT"].ToString());
                oMINVE01.PRECIO = double.Parse(dr["PRECIO"].ToString());
                Movimientos.Add(oMINVE01);
            }
            return Movimientos;
        }
        #endregion

        private void chkCancelacion_CheckedChanged(object sender, EventArgs e)
        {
            this.cancelacion = chkCancelacion.Checked;
        }
    }
}
