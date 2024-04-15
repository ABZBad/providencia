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
    public partial class frmCapFactProvFlete : Form
    {
        private DataTable dataTableDocumentosPedidos = new DataTable();
        ErrorProvider errorPClave = new ErrorProvider();
        ErrorProvider errorPFactura = new ErrorProvider();
        ErrorProvider errorPSubTotal = new ErrorProvider();
        ErrorProvider errorDgvFletes = new ErrorProvider();
        public frmCapFactProvFlete()
        {
            InitializeComponent();
            InicializaTablaDocumentosPedidos();
            lblMsg.Text = "";
        }

        private bool AgregaNuevoPedidoDgv(int NumeroPedido,int PrendasFletes,string Facturas,decimal CostoEnvio)
        {
            lblMsg.Text = "";
            try
            {
                DataRow renglon = dataTableDocumentosPedidos.NewRow();
                renglon["PEDIDO"] = NumeroPedido;
                renglon["PRENDAS_FLETE"] = PrendasFletes;
                renglon["FACTURAS"] = Facturas;
                renglon["COSTO_PREVIO"] = CostoEnvio;
                dataTableDocumentosPedidos.Rows.Add(renglon);
                return true;
            }
            catch (ConstraintException constraintEx)
            {
                lblMsg.Text = "El Pedido ha sido agregado previamente.";
                return false;
            }
        }

        private void InicializaTablaDocumentosPedidos()
        {

            DataColumn colPedido        = new DataColumn("PEDIDO", typeof (int));
            DataColumn colPrendas       = new DataColumn("PRENDAS_FLETE", typeof (int));
            DataColumn colFacturas = new DataColumn("FACTURAS", typeof(string));
            DataColumn colCostoPrevio = new DataColumn("COSTO_PREVIO", typeof(decimal));

            dataTableDocumentosPedidos.Columns.Add(colPedido);
            dataTableDocumentosPedidos.Columns.Add(colPrendas);
            dataTableDocumentosPedidos.Columns.Add(colFacturas);
            dataTableDocumentosPedidos.Columns.Add(colCostoPrevio);

            dataTableDocumentosPedidos.PrimaryKey = new DataColumn[] {colPedido};

            dgvFletes.DataSource = dataTableDocumentosPedidos;

        }

        private void txtClave_Leave(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtClave.Text))
            {
                if (txtClave.Text != "0")
                {
                    PROV01 prov01 = new PROV01();
                    prov01 = prov01.Consultar(txtClave.Text);

                    if (!string.IsNullOrEmpty(prov01.NOMBRE))
                    {
                        lblNombreProveedor.Text = prov01.NOMBRE;
                        errorPClave.Clear();
                    }
                }
            }
            else
            {
                lblNombreProveedor.Text = "";

            }

        }

        private void txtClave_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtClave.Text))
            {
                lblNombreProveedor.Text = "";
            }
            else
            {
                errorPClave.Clear();
            }
        }

        private void btnMostrarBusqueda_Click(object sender, EventArgs e)
        {
            PROV01 prov01 = new PROV01();

            DataTable dataTableProv01 = prov01.ConsultarTodoParaBusqueda();
            frmBusquedaGenerica frmBusqueda = new frmBusquedaGenerica(dataTableProv01,"NOMBRE","LOCALIZACION DE PROVEEDORES");
            frmBusqueda.ShowDialog();
            if (frmBusqueda.RenglonSeleccionado != null)
            {
                txtClave.Text           = frmBusqueda.RenglonSeleccionado["Clave"].ToString().Trim();
                txtClave_Leave(sender, new EventArgs());                
                txtFactura.Focus();
            }
        }

        private void txtFactura_Leave(object sender, EventArgs e)
        {
            bool facturaCapturada = FLET_MSTR.FacturaCapturada(txtClave.Text, txtFactura.Text);
            if (facturaCapturada)
            {                
                txtClave.Focus();                
            }
            if (!string.IsNullOrEmpty(txtFactura.Text))
            {
                errorPFactura.Clear();
            }
            
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSubTotal.Text))
            {
                MessageBox.Show("El Subtotal es un dato requerido", "Verifique", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                txtSubTotal.Focus();
                return;
            }
            if (!string.IsNullOrEmpty(txtNumeroPedido.Text))
            {
                DataTable dataTableDocPedidos           = ProveedoresFletes.RegresaDocumentosPorPedido(int.Parse(txtNumeroPedido.Text));
                DataTable dataTableDocPedidosPrevios    = ProveedoresFletes.RegresaDocumentosPorPedidoPrevios(int.Parse(txtNumeroPedido.Text));

                if (dataTableDocPedidosPrevios.Rows.Count > 0)
                {
                    //TODO: Reviar bien, posible falla ya que no encontramos ningún número de pedido para checar los pedidos previos
                    dataTableDocPedidos.Merge(dataTableDocPedidosPrevios, true, MissingSchemaAction.AddWithKey);
                }
                if (dataTableDocPedidos.Rows.Count > 0)
                {
                    if ((int)dataTableDocPedidos.Rows[0]["PRENDAS_FLETE"] != 0)
                    {
                        bool renglonAgregado = AgregaNuevoPedidoDgv(
                            (int) dataTableDocPedidos.Rows[0]["PEDIDO"],
                            (int) dataTableDocPedidos.Rows[0]["PRENDAS_FLETE"],
                            (string) dataTableDocPedidos.Rows[0]["FACTURAS"],
                            (decimal) dataTableDocPedidos.Rows[0]["COSTO_PREVIO"]
                            );
                        if (renglonAgregado)
                        {
                            CalculaTotales();
                            txtNumeroPedido.Text = "";
                        }                        
                    }
                    else
                    {
                        lblMsg.Text = "El Pedido no contiene procesos de Flete.";
                    }
                }
                else
                {
                    lblMsg.Text = "El Pedido no existe.";
                }
                txtNumeroPedido.SelectionStart = 0;
                txtNumeroPedido.SelectAll();
                txtNumeroPedido.Focus();
            }
        }

        private void CalculaTotales()
        {
            string fmto = "N2";
            int totalPrendas = 0;
            decimal costoPrevio = 0;
            decimal costoActual = 0;
            foreach (DataRow renglonPedidoDoc in dataTableDocumentosPedidos.Rows)
            {
                try
                {
                    totalPrendas += (int) renglonPedidoDoc["PRENDAS_FLETE"];
                    costoPrevio += (int) renglonPedidoDoc["PRENDAS_FLETE"]*(decimal) renglonPedidoDoc["COSTO_PREVIO"];
                }
                catch (RowNotInTableException rnitEx) { }

            }

            costoPrevio = Math.Round(costoPrevio, 0);
            costoActual = costoPrevio + Convert.ToDecimal(txtSubTotal.Text);
            lblTotalPrendas.Text = totalPrendas.ToString(fmto);
            lblCostoTotalPrevio.Text = costoPrevio.ToString(fmto);
            lblCostoTotalActual.Text = costoActual.ToString(fmto);
            if (totalPrendas > 0)
            {
                lblCostoPorPrenda.Text = Math.Round(costoActual/totalPrendas,4).ToString();
                lblCostoPorPrenda.Tag = Math.Round(costoActual/totalPrendas, 4);
            }
            else
            {
                lblCostoPorPrenda.Text = "0";
                lblCostoPorPrenda.Tag = 0;
            }
            if (dgvFletes.Rows.Count > 0)
                errorDgvFletes.Clear();
        }
    
        private void txtNumeroPedido_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnAgregar_Click(sender, new EventArgs());
        }

        private void txtNumeroPedido_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNumeroPedido.Text))
            {
                lblMsg.Text = "";
            }
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (dgvFletes.SelectedRows.Count > 0)
            {
                dataTableDocumentosPedidos.Rows.RemoveAt(dgvFletes.SelectedRows[0].Index);
                CalculaTotales();
            }
        }

        private void dgvFletes_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CalculaTotales();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (CamposCompletos())
            {
                DialogResult dlgResultado = MessageBox.Show("¿Están correctos los datos?","Confirme",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);


                if (dlgResultado == DialogResult.Yes)
                {
                    bool informacionGuardada = ProveedoresFletes.GuardaInformacionFlete(
                        txtClave.Text,
                        txtFactura.Text,
                        Convert.ToDecimal(txtSubTotal.Text),
                        (decimal) lblCostoPorPrenda.Tag,
                        dataTableDocumentosPedidos
                        );
                    if (informacionGuardada)
                    {
                        MessageBox.Show(Properties.Resources.Cadena_DatosGuardados, "", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        var resp = MessageBox.Show("¿ Capturar otra Factura ?", "Confirme", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                        if (resp == System.Windows.Forms.DialogResult.Yes)
                        {
                            LimpiaCampos();
                            txtClave.Focus();
                        }
                        else
                        {
                            this.Close();
                        }
                        
                    }
                }
            }
            else
            {
                MessageBox.Show("Captura la información del Panel de Proveedor.","Verifique",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
        }

        private bool CamposCompletos()
        {
            int numErorres = 0;
            errorPClave.Clear();
            errorPFactura.Clear();
            if (string.IsNullOrEmpty(txtClave.Text) || txtClave.Text == "0")
            {                
                errorPClave.BlinkStyle = ErrorBlinkStyle.NeverBlink;
                errorPClave.RightToLeft = true;
                errorPClave.SetError(txtClave, "Dato requerido");
                numErorres++;
            }
            if (string.IsNullOrEmpty(txtFactura.Text))
            {
               
                errorPFactura.BlinkStyle = ErrorBlinkStyle.NeverBlink;
                errorPFactura.RightToLeft = true;
                errorPFactura.SetError(txtFactura, "Dato requerido");
                numErorres++;
            }
            if (string.IsNullOrEmpty(txtSubTotal.Text))
            {
                errorPSubTotal.BlinkStyle = ErrorBlinkStyle.NeverBlink;
                errorPSubTotal.RightToLeft = true;
                errorPSubTotal.SetError(txtSubTotal, "Dato requerido");
                numErorres++;
            }
            if (dgvFletes.Rows.Count == 0)
            {
                errorDgvFletes.BlinkStyle = ErrorBlinkStyle.NeverBlink;
                errorDgvFletes.RightToLeft = true;
                errorDgvFletes.SetError(dgvFletes, "Captura la información del Panel de Documentos");
                numErorres++;
            }
            return !(numErorres > 0);
        }

        private void txtSubTotal_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSubTotal.Text))
            { errorPSubTotal.Clear(); }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LimpiaCampos()
        {
            dataTableDocumentosPedidos.Clear();
            txtClave.Text = "0";
            txtFactura.Text = "";
            txtSubTotal.Text = "";
            lblCostoPorPrenda.Text = "0";
            lblCostoTotalActual.Text = "0";
            lblCostoTotalPrevio.Text = "0";
            lblMsg.Text = "";
            lblNombreProveedor.Text = "";
            lblTotalPrendas.Text = "0";
        }
    }
}

