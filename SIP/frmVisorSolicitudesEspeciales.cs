using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ulp_bl;
using SIP.Utiles;

namespace SIP
{
    public partial class frmVisorSolicitudesEspeciales : Form
    {
        private List<int> NumeroSolicitudes;
        DataTable Solicitudes = new DataTable("SOLICITUDES");

        public frmVisorSolicitudesEspeciales()
        {
            this.NumeroSolicitudes = new List<int> { };
            InitializeComponent();
        }
        public frmVisorSolicitudesEspeciales(int _NumeroSolcitud)
        {
            this.NumeroSolicitudes = new List<int> { };
            this.NumeroSolicitudes.Add(_NumeroSolcitud);
            InitializeComponent();
        }

        public frmVisorSolicitudesEspeciales(List<int> _NumeroSolicitudes)
        {
            this.NumeroSolicitudes = new List<int> { };
            this.NumeroSolicitudes = _NumeroSolicitudes;
            InitializeComponent();
        }

        private void frmVisorSolicitudesEspeciales_Load(object sender, EventArgs e)
        {

            SOLICITUDES_ESPECIALES solicitudesBl = new SOLICITUDES_ESPECIALES();
            Solicitudes = solicitudesBl.ConsultarSolicitudes(this.NumeroSolicitudes);
            bindingSourceSolicitudes.DataSource = Solicitudes;

            lblNumero.DataBindings.Add("Text", bindingSourceSolicitudes, "SOLICITUD");
            lblCliente.DataBindings.Add("Text", bindingSourceSolicitudes, "CLIENTE");
            lblNombreCliente.DataBindings.Add("Text", bindingSourceSolicitudes, "CLIENTE_NOMBRE");
            lblFecha.DataBindings.Add("Text", bindingSourceSolicitudes, "FECHA");
            lblAgente.DataBindings.Add("Text", bindingSourceSolicitudes, "AGENTE");
            lblCantidadLinea.DataBindings.Add("Text", bindingSourceSolicitudes, "PRENDAS_LINEA");
            lblCantidadEspeciales.DataBindings.Add("Text", bindingSourceSolicitudes, "PRENDAS_ESP");
            lblGenero.DataBindings.Add("Text", bindingSourceSolicitudes, "GENERO");


            lblTipoPrenda.DataBindings.Add("Text", bindingSourceSolicitudes, "TIPO_PRENDA");
            lblComposicionTela.DataBindings.Add("Text", bindingSourceSolicitudes, "COMPOSICION_TELA");
            lblColor.DataBindings.Add("Text", bindingSourceSolicitudes, "COLOR");

            lblPlazoEntrega.DataBindings.Add("Text", bindingSourceSolicitudes, "PLAZO_ENTREGA");
            lblCodigoCotizacion.DataBindings.Add("Text", bindingSourceSolicitudes, "CODIGO_COTIZACION");
            lblTallas.DataBindings.Add("Text", bindingSourceSolicitudes, "TALLAS");
            lblPrecio.DataBindings.Add("Text", bindingSourceSolicitudes, "PRECIO_ASIGNADO");



            if (lblPlazoEntrega.Text == "") lblPlazoEntrega.Text = "SIN  ASIGNAR";
            if (lblCodigoCotizacion.Text == "") lblCodigoCotizacion.Text = "SIN  ASIGNAR";
            if (lblTallas.Text == "") lblTallas.Text = "SIN  ASIGNAR";
            if (lblPrecio.Text == "") lblPrecio.Text = "SIN  ASIGNAR";

            txtEspecificaciones.DataBindings.Add("Text", bindingSourceSolicitudes, "ESPECIFICACIONES");
            txtCodigosAsignados.DataBindings.Add("Text", bindingSourceSolicitudes, "CODIGOS_ASIGNADOS");
            //txtEspecificaciones.Text = Solicitudes.Rows[0]["ESPECIFICACIONES"].ToString();
            //txtCodigosAsignados.Text = Solicitudes.Rows[0]["CODIGOS_ASIGNADOS"].ToString();
            bindingNavigatorSolicitudes.BindingSource = bindingSourceSolicitudes;

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblPlazoEntrega_Click(object sender, EventArgs e)
        {

        }

        private void lblTallas_Click(object sender, EventArgs e)
        {

        }

        private void lblCodigoCotizacion_Click(object sender, EventArgs e)
        {

        }

        private void lblPrecio_Click(object sender, EventArgs e)
        {

        }

    }
}
