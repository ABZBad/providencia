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
    public partial class frmNuevoPedido : Form
    {
        #region Propiedades y constructor
        public int numeroPedido;
        public Boolean procesado = false;
        DataTable dtCalogoFormaPagoComisiones;
        decimal formaPagoComision = 0;
        string clave_cliente = "";
        string nombre_vendedor = "";
        vw_Clientes cliente = new vw_Clientes();
        List<int> Solicitudes;
        List<TextBox> txtPedSipAValidar = new List<TextBox>();
        List<ComboBox> cmbPedSipAValidar = new List<ComboBox>();
        AddPartidasPedi2 frmAgregarPartidas;

        public frmNuevoPedido(String _claveCliente, List<int> _Solicitudes)
        {
            InitializeComponent();
            this.clave_cliente = _claveCliente;
            this.Solicitudes = _Solicitudes;
            this.ClienteLlenaDatos(this.clave_cliente);
            this.PedidosSIPHabilitaControlesCambios(true);
            btnPedSIPAgregarPartidas.Enabled = false;
            btnPedSIPElimModPartidas.Enabled = false;
            btnPedSIPDocumentos.Enabled = false;
            btnPedSIPImprimir.Enabled = false;
            txtPedSIPOc.Focus();
        }

        #endregion
        #region Eventos
        private void frmNuevoPedido_Load(object sender, EventArgs e)
        {
            CargaCatalogos();
            LlenaDatos();
            txtPedSIPOc.Focus();
            txtPedSipAValidar.Add(txtPedSIPRemitido);
            cmbPedSipAValidar.Add(cmbPedSIPFormaPago);
            cmbPedSipAValidar.Add(cmbPedSIPUsoCFDI);
            cmbPedSipAValidar.Add(cmbPedSIPMetodoPago);
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string resultado = "";
            if (!GeneralesExistenDatosVacios(txtPedSipAValidar) && !GeneralesExistenDatosVacios(cmbPedSipAValidar))
            {
                if (this.numeroPedido > 0)
                {
                    resultado = PedidosSIPModificar();
                }
                else
                {
                    resultado = PedidosSIPGuardar();
                }
                if (resultado == "")
                {
                    MessageBox.Show("Pedido creado/modificado de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PedidosSIPHabilitaControlesCambios(true);
                }
            }
        }
        private void btnPedSIPAgregarPartidas_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPedSIPCliente.Text))
            {
                /*
                frmAgregarPartidas = new AddPartidasPedi(txtPedSIPCliente.Text,
                    Convert.ToInt32(txtPedSIPNo.Text), Enumerados.TipoPedido.Pedido, nombre_vendedor);
                frmAgregarPartidas.Show();
                 * */
                frmAgregarPartidas = new AddPartidasPedi2(txtPedSIPCliente.Text, Convert.ToInt32(txtPedSIPNo.Text), Enumerados.TipoPedido.Pedido, this.nombre_vendedor);
                frmAgregarPartidas.ShowDialog();
                if (frmAgregarPartidas.pedidoEstatusModificado)
                {
                    this.procesado = true;
                    this.Close();
                }
            }
        }
        private void btnPedSIPElimModPartidas_Click(object sender, EventArgs e)
        {
            frmEliminarPartidas frmEliminarPartidas = new frmEliminarPartidas(clave_cliente, Convert.ToInt32(txtPedSIPNo.Text), Enumerados.TipoPedido.Pedido);
            frmEliminarPartidas.Show();
        }
        #endregion
        #region Metodos
        private void CargaCatalogos()
        {
            DataTable dtCatalogoFormaPago = Catalogos.GetCatalogoFormaPago();
            DataTable dtCatalogoUsoCFDI = Catalogos.GetCatalogoUsoCFDI();
            DataTable dtCatalogoMetodoPago = Catalogos.GetCatalogoMetodoPago();
            dtCalogoFormaPagoComisiones = Catalogos.GetCatalogoFormaPagoComision();
            //SE CARGAN CATALOGOS 
            //***********************************FORMAS DE PAGO************************            
            cmbPedSIPFormaPago.DisplayMember = "Descripcion";
            cmbPedSIPFormaPago.ValueMember = "Clave";
            cmbPedSIPFormaPago.DataSource = dtCatalogoFormaPago;
            cmbPedSIPFormaPago.SelectedValue = "03";
            //***********************************USO DE CFDI************************            
            cmbPedSIPUsoCFDI.DisplayMember = "Descripcion";
            cmbPedSIPUsoCFDI.ValueMember = "Clave";
            cmbPedSIPUsoCFDI.DataSource = dtCatalogoUsoCFDI;
            cmbPedSIPUsoCFDI.SelectedValue = "G01";
            //***********************************METODOS DE PAGO************************
            //PEDIDOS NORMALES
            cmbPedSIPMetodoPago.DisplayMember = "Descripcion";
            cmbPedSIPMetodoPago.ValueMember = "Clave";
            cmbPedSIPMetodoPago.DataSource = dtCatalogoMetodoPago;
            cmbPedSIPMetodoPago.SelectedValue = "PUE";
        }
        private void LlenaDatos()
        {
            txtPedSIPCliente.Text = this.clave_cliente;
            txtPedSIPFecha.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            txtPedSIPTerminos.Text = this.cliente.DIAS_CRE.ToString();
            txtPedSIPAgente.Text = this.cliente.CVE_VEND.ToString();

        }
        private void ClienteLlenaDatos(string id)
        {
            this.cliente = this.cliente.Consultar(id);
            this.nombre_vendedor = this.cliente.NOMBRE_VENDEDOR;
        }
        private string PedidosSIPGuardar()
        {
            string resultado = "";
            int pedido = 0;
            PED_MSTR guardaPedido = new PED_MSTR();
            guardaPedido.AGENTE = this.cliente.CVE_VEND.ToString();
            guardaPedido.FECHA = Convert.ToDateTime(txtPedSIPFecha.Text);
            guardaPedido.CLIENTE = txtPedSIPCliente.Text;
            guardaPedido.OC = txtPedSIPOc.Text;
            guardaPedido.TERMINOS = this.cliente.DIAS_CRE.ToString();
            guardaPedido.REMITIDO = txtPedSIPRemitido.Text;
            guardaPedido.CONSIGNADO = txtPedSIPConsignado.Text;
            guardaPedido.OBSERVACIONES = txtPedSIPObservaciones.Text;
            guardaPedido.TIPO = "OV";
            guardaPedido.LISTA = 1;
            guardaPedido.ESTATUS = "P";
            guardaPedido.FORMADEPAGOSAT = cmbPedSIPFormaPago.SelectedValue.ToString();
            guardaPedido.USO_CFDI = cmbPedSIPUsoCFDI.SelectedValue.ToString();
            guardaPedido.METODODEPAGO = cmbPedSIPMetodoPago.SelectedValue.ToString();
            guardaPedido.NUMERO_COTIZACION = txtPedSIPCotizacion.Text == "" ? null : (int?)(int.Parse(txtPedSIPCotizacion.Text));
            guardaPedido.Crear(guardaPedido, ref pedido);
            if (!guardaPedido.TieneError)
            {
                UPPEDIDOS guardaUppedidos = new UPPEDIDOS();
                guardaUppedidos.PEDIDO = pedido;
                guardaUppedidos.COD_CLIENTE = clave_cliente;
                guardaUppedidos.F_CAPT = DateTime.Now;
                guardaUppedidos.Crear(guardaUppedidos);
                txtPedSIPNo.Text = pedido.ToString();
                this.numeroPedido = pedido;
            }
            else
            {
                resultado = guardaPedido.Error.InnerException.ToString();
            }
            return resultado;
        }
        private string PedidosSIPModificar()
        {
            string resultado = "";
            PED_MSTR pedidoModifica = new PED_MSTR();
            pedidoModifica.OC = txtPedSIPOc.Text;
            pedidoModifica.TERMINOS = txtPedSIPTerminos.Text;
            pedidoModifica.REMITIDO = txtPedSIPRemitido.Text;
            pedidoModifica.CONSIGNADO = txtPedSIPConsignado.Text;
            pedidoModifica.OBSERVACIONES = txtPedSIPObservaciones.Text;
            pedidoModifica.PEDIDO = Convert.ToInt32(txtPedSIPNo.Text);
            pedidoModifica.FORMADEPAGOSAT = cmbPedSIPFormaPago.SelectedValue.ToString();
            pedidoModifica.USO_CFDI = cmbPedSIPUsoCFDI.SelectedValue.ToString();
            pedidoModifica.METODODEPAGO = cmbPedSIPMetodoPago.SelectedValue.ToString();
            pedidoModifica.NUMERO_COTIZACION = txtPedSIPCotizacion.Text == "" ? null : (int?)(int.Parse(txtPedSIPCotizacion.Text));
            pedidoModifica.Modificar(pedidoModifica);
            if (pedidoModifica.TieneError)
            {
                resultado = pedidoModifica.Error.InnerException.ToString();
            }
            return resultado;
        }
        private void PedidosSIPHabilitaControlesCambios(bool Habilita)
        {
            lblPedSIPYaImpreso.Visible = !Habilita;
            txtPedSIPOc.Enabled = Habilita;
            txtPedSIPRemitido.Enabled = Habilita;
            txtPedSIPConsignado.Enabled = Habilita;
            txtPedSIPObservaciones.Enabled = Habilita;
            btnPedSIPAgregarPartidas.Enabled = Habilita;
            btnPedSIPElimModPartidas.Enabled = Habilita;
            btnPedSIPImprimir.Enabled = Habilita;
            btnPedSIPDocumentos.Enabled = Habilita;
            txtPedSIPCotizacion.Enabled = Habilita;
        }
        private bool GeneralesExistenDatosVacios(List<TextBox> txts)
        {
            bool datoRetorno = false;
            errorProvider1.Clear();
            foreach (TextBox txt in txts)
            {
                if (string.IsNullOrWhiteSpace(txt.Text))
                {
                    errorProvider1.SetError(txt, "Debe escribir un valor.");
                    datoRetorno = true;
                }
            }
            return datoRetorno;

        }
        private bool GeneralesExistenDatosVacios(List<ComboBox> cmbs)
        {
            bool datoRetorno = false;
            errorProvider1.Clear();
            foreach (ComboBox cmb in cmbs)
            {
                if (string.IsNullOrWhiteSpace(cmb.Text))
                {
                    errorProvider1.SetError(cmb, "Debe seleccionar un valor.");
                    datoRetorno = true;
                }
            }
            return datoRetorno;

        }
        #endregion

        private void btnPedSIPImprimir_Click(object sender, EventArgs e)
        {
            if (this.numeroPedido > 0)
            {
                Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos,
                    clave_cliente, 0, this.numeroPedido, Enumerados.TipoPedido.Pedido, "", false, false);
                frmReportes.ShowDialog();
                if (frmReportes.enVentas)
                {
                    this.procesado = true;
                    this.Close();
                }
            }
        }

        private void frmNuevoPedido_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.numeroPedido > 0 && !this.procesado)
            {
                if (MessageBox.Show("El Pedido no se ha pasado a Ventas, ¿Seguro que desea salir del proceso.?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void btnPedSIPDocumentos_Click(object sender, EventArgs e)
        {
            frmDocumentosElectronicos btnPedSIPImprimir = new frmDocumentosElectronicos(Convert.ToInt32(txtPedSIPNo.Text));
            btnPedSIPImprimir.Show();
        }
    }
}
