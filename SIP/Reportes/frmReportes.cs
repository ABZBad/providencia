using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ulp_bl.Reportes;
using ulp_bl;
using System.IO;

namespace SIP.Reportes
{
    public partial class frmReportes : Form
    {
        Enumerados.TipoReporteCrystal tipoReporte = new Enumerados.TipoReporteCrystal();
        Enumerados.TipoPedido tPedido = new Enumerados.TipoPedido();
        //public int id_usuario = 0;
        public int totProcesos = 0;
        public bool enVentas = false;
        public bool pedidoEstatusModificado = false;
        public bool pedidoTambienImprimeOT = false;
        string clave_cliente = "";
        string sPedidoDescuento = "";
        int Pedido = 0;
        double CMP = 0;
        double DS = 0;
        string metodoPago = "";
        string formaPago = "";
        string usoCFDI = "";
        string grupo = "";
        double descuentoPedido = 0;
        double importeDescuentoPedido = 0;
        DataTable datos_pedido;
        DataTable datos_pedidoVirtual;
        DataTable datos_pedido1;
        DataTable datos_pedido2;
        DataTable datosRequisicion = new DataTable("Reporte");
        DataTable datosRequisicionMostrador = new DataTable("Reporte");

        string Origen = "";
        Boolean imprimirFirmas = false;
        Boolean imprimirPedido = false;

        Boolean esDAT = false;
        Boolean dividido = false;
        public Boolean procesado = false;

        public string direccionEntrega = "";

        public frmReportes(Enumerados.TipoReporteCrystal tipo)
        {
            if (tipo == Enumerados.TipoReporteCrystal.Precarga)
            {
                InitializeComponent();
                rptPreLoad reporte = new rptPreLoad();
                crystalReportViewer1.ReportSource = reporte;
            }
        }
        public frmReportes(Enumerados.TipoReporteCrystal tipo, string cliente, int idImagen, int idPedido, Enumerados.TipoPedido tipoPedido)
        {
            InitializeComponent();
            pedidoEstatusModificado = false;
            pedidoTambienImprimeOT = false;
            Cursor.Current = Cursors.WaitCursor;
            tipoReporte = tipo;
            clave_cliente = cliente;
            Pedido = idPedido;
            tPedido = tipoPedido;
            if (tipoReporte == Enumerados.TipoReporteCrystal.Logotipos)
            {
                DespliegaReporteLogos(idImagen);
            }
            if (tipoReporte == Enumerados.TipoReporteCrystal.Pedidos)
            {
                Pedido = idPedido;
                DespliegaReportePedidos(idPedido);
            }
            if (tipoReporte == Enumerados.TipoReporteCrystal.OrdenTrabajo)
            {
                DespliegaReporteOrdenTrabajo(idPedido);
            }
            Cursor.Current = Cursors.Default;
        }
        public frmReportes(Enumerados.TipoReporteCrystal tipo, string cliente, int idImagen, int idPedido, Enumerados.TipoPedido tipoPedido, string origen, bool imprimirFirmas, bool imprimirPedido, bool dividido = false)
        {
            InitializeComponent();
            pedidoEstatusModificado = false;
            pedidoTambienImprimeOT = false;
            Cursor.Current = Cursors.WaitCursor;
            tipoReporte = tipo;
            clave_cliente = cliente;
            Pedido = idPedido;
            tPedido = tipoPedido;
            esDAT = (tPedido == Enumerados.TipoPedido.PedidoDAT || tPedido == Enumerados.TipoPedido.PedidoMOS || tPedido == Enumerados.TipoPedido.PedidoMOSCP) ? true : false;
            this.Origen = origen;
            this.imprimirFirmas = imprimirFirmas;
            this.imprimirPedido = imprimirPedido;
            this.dividido = dividido;
            if (tipoReporte == Enumerados.TipoReporteCrystal.Logotipos)
            {
                DespliegaReporteLogos(idImagen);
            }
            if (tipoReporte == Enumerados.TipoReporteCrystal.Pedidos)
            {
                Pedido = idPedido;
                if (this.Origen == "frmControlPedidos" && this.imprimirFirmas)
                {
                    DespliegaReportePedidosFirmas(idPedido);
                }
                else
                {
                    DespliegaReportePedidos(idPedido);
                }

            }
            if (tipoReporte == Enumerados.TipoReporteCrystal.OrdenTrabajo)
            {
                Origen = origen;
                DespliegaReporteOrdenTrabajo(idPedido);
            }
            Cursor.Current = Cursors.Default;
        }
        public frmReportes(Enumerados.TipoReporteCrystal tipo, int ClaveMaquilador, string Factura, string Etiqueta)
        {
            InitializeComponent();
            if (tipo == Enumerados.TipoReporteCrystal.DatosFacturaBordado)
            {
                DespliegaReporteDatosFacturaBordado(ClaveMaquilador, Factura, Etiqueta);
            }
        }

        public frmReportes(Enumerados.TipoReporteCrystal tipo, int NumeroPedido, string RazonSocial, string Direccion, string Atencion, string Remitido, string Consignado, string Contenido)
        {
            InitializeComponent();
            if (tipo == Enumerados.TipoReporteCrystal.EtiquetasEmpaque)
            {
                DespliegaReporteEtiquetasEmpaque(NumeroPedido, RazonSocial, Direccion, Atencion, Remitido, Consignado,
                    Contenido);
            }
        }

        public frmReportes(Enumerados.TipoReporteCrystal tipo, int NumeroPedido)
        {
            InitializeComponent();
            if (tipo == Enumerados.TipoReporteCrystal.PedidoCapturaSAE)
            {
                DespliegaReportePedidoCapturaSae(NumeroPedido);
            }
            if (tipo == Enumerados.TipoReporteCrystal.TransferenciaXModelo)
            {
                DespliegaTransferenciaXModelo(NumeroPedido);
            }
            if (tipo == Enumerados.TipoReporteCrystal.Requisicion)
            {
                tipoReporte = tipo;
                Pedido = NumeroPedido;
                DespliegaRequisicion(NumeroPedido);
            }
            if (tipo == Enumerados.TipoReporteCrystal.NotificacionesSIVO)
            {
                DespliegaRequisicion(NumeroPedido);
            }
            if (tipo == Enumerados.TipoReporteCrystal.RequisicionMostrador)
            {
                tipoReporte = tipo;
                DespliegaRequisicionMostrador(NumeroPedido);
            }
        }

        public frmReportes(Enumerados.TipoReporteCrystal tipo, string cliente, int idImagen, int idPedido, Enumerados.TipoPedido tipoPedido, Boolean esDAT, double descuento, string origen, bool dividido = false)
        {
            InitializeComponent();
            pedidoEstatusModificado = false;
            pedidoTambienImprimeOT = false;
            Cursor.Current = Cursors.WaitCursor;
            tipoReporte = tipo;
            clave_cliente = cliente;
            Pedido = idPedido;
            tPedido = tipoPedido;
            this.Origen = origen;
            this.esDAT = esDAT;
            this.dividido = dividido;
            if (tipoReporte == Enumerados.TipoReporteCrystal.Logotipos)
            {
                DespliegaReporteLogos(idImagen);
            }
            if (tipoReporte == Enumerados.TipoReporteCrystal.Pedidos)
            {
                Pedido = idPedido;
                DespliegaReportePedidos(idPedido);
            }
            if (tipoReporte == Enumerados.TipoReporteCrystal.OrdenTrabajo)
            {
                DespliegaReporteOrdenTrabajo(idPedido);
            }
            Cursor.Current = Cursors.Default;
        }

        public frmReportes(Enumerados.TipoReporteCrystal tipo, String areaUsuario)
        {
            InitializeComponent();
            if (tipo == Enumerados.TipoReporteCrystal.NotificacionesSIVO)
            {
                DespliegaReporteNotificaciones(areaUsuario);
            }
        }

        private void DespliegaReportePedidoCapturaSae(int NumeroPedido)
        {
            DataTable dataTablePedidoSae = CargaPedidosSAE.RegresaPedidoSae(NumeroPedido);
            rptPedidosCapturaSae rptPedidosCapturaSae = new rptPedidosCapturaSae();
            rptPedidosCapturaSae.SetDataSource(dataTablePedidoSae);
            crystalReportViewer1.ReportSource = rptPedidosCapturaSae;
        }
        private void DespliegaReporteEtiquetasEmpaque(int NumeroPedido, string RazonSocial, string Direccion, string Atencion, string Remitido, string Consignado, string Contenido)
        {
            rptEtiquetasEmpaque rptEtiquetasEmpaque = new rptEtiquetasEmpaque();

            ParameterFields paramCampos = new ParameterFields();
            ParameterField paramNumeroPedido = new ParameterField();
            ParameterField paramRazonSocial = new ParameterField();
            ParameterField paramDireccion = new ParameterField();
            ParameterField paramAtencion = new ParameterField();
            ParameterField paramRemitido = new ParameterField();
            ParameterField paramConsignado = new ParameterField();
            ParameterField paramContenido = new ParameterField();

            ParameterDiscreteValue paramDiscretValNumeroPedido = new ParameterDiscreteValue();
            ParameterDiscreteValue paramDiscretValRazonSocia = new ParameterDiscreteValue();
            ParameterDiscreteValue paramDiscretValDireccion = new ParameterDiscreteValue();
            ParameterDiscreteValue paramDiscretValAtencion = new ParameterDiscreteValue();
            ParameterDiscreteValue paramDiscretValRemitido = new ParameterDiscreteValue();
            ParameterDiscreteValue paramDiscretValConsignado = new ParameterDiscreteValue();
            ParameterDiscreteValue paramDiscretValContenido = new ParameterDiscreteValue();

            paramDiscretValNumeroPedido.Value = NumeroPedido.ToString();
            paramNumeroPedido.Name = "NumeroPedido";
            paramNumeroPedido.CurrentValues.Add(paramDiscretValNumeroPedido);

            paramDiscretValRazonSocia.Value = RazonSocial;
            paramRazonSocial.Name = "RazonSocial";
            paramRazonSocial.CurrentValues.Add(paramDiscretValRazonSocia);

            paramDiscretValDireccion.Value = Direccion;
            paramDireccion.Name = "Direccion";
            paramDireccion.CurrentValues.Add(paramDiscretValDireccion);

            paramDiscretValAtencion.Value = Atencion;
            paramAtencion.Name = "Atencion";
            paramAtencion.CurrentValues.Add(paramDiscretValAtencion);

            paramDiscretValRemitido.Value = Remitido;
            paramRemitido.Name = "Remitido";
            paramRemitido.CurrentValues.Add(paramDiscretValRemitido);

            paramDiscretValConsignado.Value = Consignado;
            paramConsignado.Name = "Consignado";
            paramConsignado.CurrentValues.Add(paramDiscretValConsignado);

            paramDiscretValContenido.Value = Contenido;
            paramContenido.Name = "Contenido";
            paramContenido.CurrentValues.Add(paramDiscretValContenido);

            paramCampos.Add(paramNumeroPedido);
            paramCampos.Add(paramRazonSocial);
            paramCampos.Add(paramDireccion);
            paramCampos.Add(paramAtencion);
            paramCampos.Add(paramRemitido);
            paramCampos.Add(paramConsignado);
            paramCampos.Add(paramContenido);

            crystalReportViewer1.ParameterFieldInfo = new ParameterFields(paramCampos);

            crystalReportViewer1.ReportSource = rptEtiquetasEmpaque;
        }
        private void DespliegaReporteDatosFacturaBordado(int ClaveMaquilador, string Factura, string Etiqueta)
        {
            DataTable dataTableMpb = RepDatosFacturaBordado.RegresaDatosFacturaBordadoDetalle(ClaveMaquilador, Factura);
            rptDatosFacturaBordado reporte = new rptDatosFacturaBordado();




            reporte.SetDataSource(dataTableMpb);
            ParameterFields paramCampos = new ParameterFields();
            ParameterField paramFactura = new ParameterField();
            ParameterDiscreteValue paramDiscretVal = new ParameterDiscreteValue();




            paramDiscretVal.Value = Etiqueta;
            paramFactura.Name = "etiqueta";


            paramFactura.CurrentValues.Add(paramDiscretVal);

            paramCampos.Add(paramFactura);

            crystalReportViewer1.ParameterFieldInfo = new ParameterFields(paramCampos);
            //crystalReportViewer1.ParameterFieldInfo = paramCampos;
            crystalReportViewer1.ReportSource = reporte;
        }
        private void DespliegaTransferenciaXModelo(int agrupador)
        {
            DataTable datos = new DataTable();
            rptTransferenciaXModelo reporte = new rptTransferenciaXModelo();
            datos = TransferenciaPorModelo.DevuelveDatosRpt(Convert.ToString(agrupador));
            reporte.SetDataSource(datos);
            crystalReportViewer1.ReportSource = reporte;
        }
        private void DespliegaReporteOrdenTrabajo(int idPedido)
        {
            rptOrdenTrabajo reporte = new rptOrdenTrabajo();
            DataTable datos_ot = new DataTable();
            PED_MSTR ot_imprimir = new PED_MSTR();
            GuardarPed_Temp();
            ActualizaAcumuladosPed_Mstr();
            datos_ot = ot_imprimir.ConsultaImprimirOrdenTrabajo(idPedido);

            if (datos_ot.Rows.Count == 0)
            {
                if (this.Origen != "frmControlPedidos") // EVITAMOS QUE EL MENSAJE APAREZCA DENTRO DEL FLUJO DE CONTROL PEDIDOS
                {
                    MessageBox.Show("No existen datos para desplegar el reporte de OT", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                this.pedidoTambienImprimeOT = false;
                this.Close();

            }
            else
            {
                reporte.SetDataSource(datos_ot);
                crystalReportViewer1.ReportSource = reporte;
                this.pedidoTambienImprimeOT = true;
            }
            //datos_ot.TableName = "OrdenTrabajo";
            //datos_ot.WriteXmlSchema(Directory.GetCurrentDirectory().ToString() + @"\ot.xsd");
        }
        private void ActualizaAcumuladosPed_Mstr()
        {
            PED_MSTR ped_mstr = new PED_MSTR();
            ped_mstr.PEDIDO = Pedido;
            ped_mstr.ActualizaAcumulados(ped_mstr);
        }

        private void ActualizaCalculoComision()
        {
            PED_MSTR ped_mstr = new PED_MSTR();
            ped_mstr.PEDIDO = Pedido;
            ped_mstr.CalculaComision(ped_mstr);
        }
        private void GuardarPed_Temp()
        {
            PED_TEMP ped_temp = new PED_TEMP();
            ped_temp.PEDIDO = Pedido;
            ped_temp.CrearPorPedido(ped_temp, tPedido);
        }
        private ParameterFields DevuelveParametrosReportePedidos(string Estatus, string sDescuento, double DS, double CMP, Boolean esDAT = false, double importeDescuento = 0, Enumerados.TipoPedido tPedido = Enumerados.TipoPedido.Pedido, string direccionEntrega = "")
        {
            USUARIOS datos_firma1 = new USUARIOS();
            ParameterFields parameterFields = new ParameterFields();
            ParameterField parametroFirma1 = new ParameterField();
            ParameterDiscreteValue pdvFirma1 = new ParameterDiscreteValue();
            ParameterField parametroNomEquipo = new ParameterField();
            ParameterDiscreteValue pdvNomEquipo = new ParameterDiscreteValue();
            ParameterField parametroStatus = new ParameterField();
            ParameterDiscreteValue pdvStatus = new ParameterDiscreteValue();
            ParameterField parametroDescuento = new ParameterField();
            ParameterDiscreteValue pvdDescuento = new ParameterDiscreteValue();

            ParameterField parametroDS = new ParameterField();
            ParameterDiscreteValue pdvDS = new ParameterDiscreteValue();

            ParameterField parametroCMP = new ParameterField();
            ParameterDiscreteValue pdvCMP = new ParameterDiscreteValue();

            ParameterField parametroFormaPago = new ParameterField();
            ParameterDiscreteValue pdvFormaPago = new ParameterDiscreteValue();

            ParameterField parametroMetodoPago = new ParameterField();
            ParameterDiscreteValue pdvMetodoPago = new ParameterDiscreteValue();

            ParameterField parametroUsoCFDI = new ParameterField();
            ParameterDiscreteValue pdvUsoCFDI = new ParameterDiscreteValue();

            ParameterField parametroCorporativo = new ParameterField();
            ParameterDiscreteValue pdvCorporativo = new ParameterDiscreteValue();

            ParameterField parametroDAT = new ParameterField();
            ParameterDiscreteValue pdvDAT = new ParameterDiscreteValue();

            ParameterField parametroTipoPedido = new ParameterField();
            ParameterDiscreteValue pdvTipoPedido = new ParameterDiscreteValue();

            ParameterField parametroImporteDescuento = new ParameterField();
            ParameterDiscreteValue pdvImporteDescuento = new ParameterDiscreteValue();

            ParameterField parametroDireccionEntrega = new ParameterField();
            ParameterDiscreteValue pdvDireccionEntrega = new ParameterDiscreteValue();


            parametroFirma1.Name = "firma1";
            datos_firma1 = datos_firma1.ConsultarPorUsuario(Globales.UsuarioActual.UsuarioUsuario);
            string etiquetaFirma1 = "";
            switch (datos_firma1.GERENTE)
            {
                case "A":
                    etiquetaFirma1 = "Gte Vtas Metropolitano";
                    break;
                case "B":
                    etiquetaFirma1 = "Gte Vtas Interior";
                    break;
                case "X":
                    etiquetaFirma1 = "Gte Sistemas";
                    break;
                default:
                    break;
            }
            pdvFirma1.Value = etiquetaFirma1;
            parametroFirma1.CurrentValues.Add(pdvFirma1);
            parameterFields.Add(parametroFirma1);
            parametroNomEquipo.Name = "nombreEquipo";
            pdvNomEquipo.Value = GlobalesUI.nombreEquipo;
            parametroNomEquipo.CurrentValues.Add(pdvNomEquipo);
            parameterFields.Add(parametroNomEquipo);

            parametroStatus.Name = "Status";
            pdvStatus.Value = Estatus;
            parametroStatus.CurrentValues.Add(pdvStatus);
            parameterFields.Add(parametroStatus);

            parametroDescuento.Name = "Descuento";
            pvdDescuento.Value = sDescuento;
            parametroDescuento.CurrentValues.Add(pvdDescuento);
            parameterFields.Add(parametroDescuento);

            parametroDS.Name = "DS";
            pdvDS.Value = DS;
            parametroDS.CurrentValues.Add(pdvDS);
            parameterFields.Add(parametroDS);

            parametroCMP.Name = "CMP";
            pdvCMP.Value = CMP;
            parametroCMP.CurrentValues.Add(pdvCMP);
            parameterFields.Add(parametroCMP);

            parametroFormaPago.Name = "formaPago";
            pdvFormaPago.Value = formaPago;
            parametroFormaPago.CurrentValues.Add(pdvFormaPago);
            parameterFields.Add(parametroFormaPago);

            parametroMetodoPago.Name = "metodoPago";
            pdvMetodoPago.Value = metodoPago;
            parametroMetodoPago.CurrentValues.Add(pdvMetodoPago);
            parameterFields.Add(parametroMetodoPago);

            parametroUsoCFDI.Name = "usoCFDI";
            pdvUsoCFDI.Value = usoCFDI;
            parametroUsoCFDI.CurrentValues.Add(pdvUsoCFDI);
            parameterFields.Add(parametroUsoCFDI);

            parametroCorporativo.Name = "corporativo";
            pdvCorporativo.Value = grupo;
            parametroCorporativo.CurrentValues.Add(pdvCorporativo);
            parameterFields.Add(parametroCorporativo);

            parametroDAT.Name = "esDAT";
            pdvDAT.Value = esDAT ? "S" : "N";
            parametroDAT.CurrentValues.Add(pdvDAT);
            parameterFields.Add(parametroDAT);


            parametroImporteDescuento.Name = "importeDescuento";
            pdvImporteDescuento.Value = importeDescuento;
            parametroImporteDescuento.CurrentValues.Add(pdvImporteDescuento);
            parameterFields.Add(parametroImporteDescuento);

            parametroTipoPedido.Name = "tipoPedido";
            switch (tPedido)
            {
                case Enumerados.TipoPedido.Pedido:
                    pdvTipoPedido.Value = "P";
                    break;
                case Enumerados.TipoPedido.PedidoDAT:
                    pdvTipoPedido.Value = "D";
                    break;
                case Enumerados.TipoPedido.PedidoMOS:
                    pdvTipoPedido.Value = "M";
                    break;
                case Enumerados.TipoPedido.PedidoEC:
                    pdvTipoPedido.Value = "EC";
                    break;
                case Enumerados.TipoPedido.PedidoMOSCP:
                    pdvTipoPedido.Value = "MP";
                    break;
            }

            parametroTipoPedido.CurrentValues.Add(pdvTipoPedido);
            parameterFields.Add(parametroTipoPedido);

            parametroDireccionEntrega.Name = "direccionEntrega";
            pdvDireccionEntrega.Value = direccionEntrega;
            parametroDireccionEntrega.CurrentValues.Add(pdvDireccionEntrega);
            parameterFields.Add(parametroDireccionEntrega);

            return parameterFields;

        }
        private void DespliegaReportePedidos(int Pedido)
        {
            CMP = DS = 0;
            rptPedidos reporte = new rptPedidos();
            PED_MSTR pedido_imprimir = new PED_MSTR();
            datos_pedido = new DataTable();
            ActualizaCalculoComision();
            GuardarPed_Temp();
            ActualizaAcumuladosPed_Mstr();
            datos_pedido = pedido_imprimir.ConsultaImprimir(Pedido);


            foreach (DataRow dataRow in datos_pedido.Rows)
            {
                dataRow.SetField<string>("ESTATUS", "P");
            }

            totProcesos = datos_pedido.AsEnumerable().Count(c => c["CMT_PROCESO"].ToString() != string.Empty && c["CMT_PROCESO"].ToString() != "L");

            reporte.SetDataSource(datos_pedido);
            DS = double.Parse(datos_pedido.Rows[0]["DS"].ToString());
            CMP = double.Parse(datos_pedido.Rows[0]["CMP"].ToString());
            formaPago = datos_pedido.Rows[0]["formaPago"].ToString();
            metodoPago = datos_pedido.Rows[0]["metodoPago"].ToString();
            usoCFDI = datos_pedido.Rows[0]["usoCFDI"].ToString();
            grupo = datos_pedido.Rows[0]["CORPORATIVO"].ToString();
            descuentoPedido = double.Parse(datos_pedido.Rows[0]["DESCUENTO"].ToString());
            CMT_DET procesoDireccion = BuscaDireccionEmbarque(Pedido);
            if (procesoDireccion != null)
            {
                direccionEntrega = procesoDireccion.CMT_DONDE;
            }

            switch (this.tPedido)
            {
                case Enumerados.TipoPedido.PedidoDAT:
                    sPedidoDescuento = (descuentoPedido * 100).ToString() + "%";
                    break;
                case Enumerados.TipoPedido.PedidoEC:
                    sPedidoDescuento = (descuentoPedido * 100).ToString() + "%";
                    break;
                default:
                    sPedidoDescuento = PED_MSTR.ConsultaImprimirDevuelveDescuentoCifrado(Convert.ToDouble(datos_pedido.Rows[0]["DESCUENTO"]));
                    break;
            }

            /*
             * if (!esDAT)
            {
                sPedidoDescuento = PED_MSTR.ConsultaImprimirDevuelveDescuentoCifrado(Convert.ToDouble(datos_pedido.Rows[0]["DESCUENTO"]));
            }
            else
            {
                switch (this.tPedido)
                {
                    case Enumerados.TipoPedido.PedidoDAT:
                        sPedidoDescuento = (descuentoPedido * 100).ToString() + "%";
                        break;
                    case Enumerados.TipoPedido.PedidoEC:
                        sPedidoDescuento = (descuentoPedido * 100).ToString() + "%";
                        break;
                    case Enumerados.TipoPedido.PedidoMOS:
                        sPedidoDescuento = PED_MSTR.ConsultaImprimirDevuelveDescuentoCifrado(Convert.ToDouble(datos_pedido.Rows[0]["DESCUENTO"]));
                        break;
                }
                //sPedidoDescuento = "Descuento:" + (descuentoPedido * 100).ToString() + "%";

            }
             * */

            try
            {
                foreach (DataRow dr in datos_pedido.Rows)
                {
                    importeDescuentoPedido += Math.Round((((Convert.ToDouble(dr["PRECIO"].ToString()) / (1 - Convert.ToDouble(dr["DESCUENTO"].ToString()))) - Convert.ToDouble(dr["PRECIO"].ToString())) * Convert.ToDouble(dr["T_PRENDAS"].ToString())), 2);
                }
            }
            catch { }

            crystalReportViewer1.ParameterFieldInfo = DevuelveParametrosReportePedidos(datos_pedido.Rows[0]["Estatus"].ToString(), sPedidoDescuento, DS, CMP, esDAT, importeDescuentoPedido, this.tPedido, direccionEntrega);
            crystalReportViewer1.ReportSource = reporte;

            //datos_pedido.WriteXmlSchema(Directory.GetCurrentDirectory().ToString() + @"\pedido.xsd");
        }
        private void DespliegaReporteLogos(int idImagen)
        {
            rptLogotipos reporte = new rptLogotipos();
            DataSet dataSource = new DataSet();
            DataTable datosImagenes = new DataTable();
            DataTable datosCliente = new DataTable();
            IMAGENES imagenes = new IMAGENES();
            CLIE01 cliente = new CLIE01();
            datosImagenes = imagenes.ConsultarPorId(idImagen);
            datosCliente = cliente.ConsultarIDCadenaDataTable(clave_cliente);
            dataSource.Tables.Add(datosImagenes);
            dataSource.Tables.Add(datosCliente);

            //procesa información para la imagen
            dataSource.Tables.Add(dtImagen(Globales.rutaImagenes, "CL" + clave_cliente + "-ID" + idImagen + ".jpg"));

            reporte.SetDataSource(dataSource);


            crystalReportViewer1.ReportSource = reporte;

        }

        private DataTable dtImagen(string ruta, string archivo)
        {
            DataTable dtImg = new DataTable();
            dtImg.Columns.Add(new DataColumn("Img", System.Type.GetType("System.Byte[]")));
            DataRow drImage = dtImg.NewRow();
            dtImg.TableName = "ImagenLogo";

            if (File.Exists(ruta + archivo))
            {
                FileStream fs = new FileStream(ruta + archivo, FileMode.Open);
                BinaryReader br = new BinaryReader(fs);
                byte[] imgByte = new byte[fs.Length];

                imgByte = br.ReadBytes(Convert.ToInt32((fs.Length)));

                drImage["Img"] = imgByte;
                br.Close();
                fs.Close();
            }

            dtImg.Rows.Add(drImage);


            return dtImg;

        }


        private void frmReportes_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (tipoReporte)
            {
                case Enumerados.TipoReporteCrystal.Logotipos:
                    break;
                case Enumerados.TipoReporteCrystal.Pedidos:
                    if (this.Origen == "frmControlPedidos" && this.imprimirFirmas && this.imprimirPedido)
                    {
                        if (MessageBox.Show("¿Desea imprimir el PEDIDO con las firmas?", "Impresión DEFINITIVA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            //IMPRIMIMOS FISICAMENTE EL PEDIDO
                            /*CMP = DS = 0;
                            rptPedidosFirmas reporte = new rptPedidosFirmas();
                            PED_MSTR pedido_imprimir = new PED_MSTR();
                            DataTable datos_pedido = new DataTable();
                            GuardarPed_Temp();
                            ActualizaAcumuladosPed_Mstr();
                            datos_pedido = pedido_imprimir.ConsultaImprimir(Pedido);

                            totProcesos = datos_pedido.AsEnumerable().Count(c => c["CMT_PROCESO"].ToString() != string.Empty);

                            reporte.SetDataSource(datos_pedido);
                            DS = double.Parse(datos_pedido.Rows[0]["DS"].ToString());
                            CMP = double.Parse(datos_pedido.Rows[0]["CMP"].ToString());
                            formaPago = datos_pedido.Rows[0]["formaPago"].ToString();
                            metodoPago = datos_pedido.Rows[0]["metodoPago"].ToString();
                            usoCFDI = datos_pedido.Rows[0]["usoCFDI"].ToString();
                            grupo = datos_pedido.Rows[0]["CORPORATIVO"].ToString();
                            descuentoPedido = double.Parse(datos_pedido.Rows[0]["DESCUENTO"].ToString());
                            String tipoProveedor = datos_pedido.Rows[0]["CLASIFIC"].ToString();
                            if (!esDAT)
                            {
                                sPedidoDescuento = PED_MSTR.ConsultaImprimirDevuelveDescuentoCifrado(Convert.ToDouble(datos_pedido.Rows[0]["DESCUENTO"]));
                            }
                            else
                            {
                                //sPedidoDescuento = "Descuento:" + (descuentoPedido * 100).ToString() + "%";
                                sPedidoDescuento = (descuentoPedido * 100).ToString() + "%";
                            }

                            try
                            {
                                foreach (DataRow dr in datos_pedido.Rows)
                                {
                                    importeDescuentoPedido += Convert.ToDouble(dr["PRECIO"].ToString()) / (1 - Convert.ToDouble(dr["DESCUENTO"].ToString()));
                                }
                            }
                            catch { }

                            // OBTENEMOS LAS FIRMAS APLICABLES 
                            // DataTable dtFirmas = ControlPedidos.getAutorizacionPedido(Pedido); SE COMENTA A PETICION DEL CLIENTE

                            //crystalReportViewer1.ParameterFieldInfo = DevuelveParametrosReportePedidosFirmas(datos_pedido.Rows[0]["Estatus"].ToString(), sPedidoDescuento, DS, CMP, dtFirmas, esDAT, importeDescuentoPedido);
                            crystalReportViewer1.ParameterFieldInfo = DevuelveParametrosReportePedidosFirmas(datos_pedido.Rows[0]["Estatus"].ToString(), sPedidoDescuento, DS, CMP, esDAT, importeDescuentoPedido, tipoProveedor);
                            crystalReportViewer1.ReportSource = reporte;*/
                            DataTable dtFirmas = ControlPedidos.getAutorizacionPedido(Pedido); //SE COMENTA A PETICION DEL CLIENTE
                            CrystalDecisions.CrystalReports.Engine.ReportDocument rc;
                            rc = (CrystalDecisions.CrystalReports.Engine.ReportDocument)crystalReportViewer1.ReportSource;
                            rc.PrintToPrinter(1, true, 0, 0);
                            this.pedidoEstatusModificado = true;
                            // IMPRIMIMOS LA OT
                            if (totProcesos > 0)
                            {
                                if (
                                    MessageBox.Show("Imprimir también Ordenes de Trabajo?",
                                        "Impresión de Órdenes de Trabajo", MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    DespliegaReporteOrdenTrabajo(Pedido);
                                    CrystalDecisions.CrystalReports.Engine.ReportDocument rcOt;

                                    rcOt =
                                        (CrystalDecisions.CrystalReports.Engine.ReportDocument)
                                            crystalReportViewer1.ReportSource;
                                    rcOt.PrintToPrinter(1, true, 0, 0);
                                    //crystalReportViewer1.PrintReport();

                                    //pedidoTambienImprimeOT = true;
                                }
                            }

                        }

                    }
                    if (!enVentas)
                    {
                        // VALIDAMOS SI EL PEDIDO TIENE CODIGO ESPECIAL QUE PROVENGA DE SOLICITUD DE CODIGOS
                        /*
                        if (ControlPedidos.getSolicitudEspecialpedido(Pedido))
                        {
                            if (MessageBox.Show("Desea imprimir el PEDIDO para pasarlo a VENTAS?", "Impresión DEFINITIVA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {

                                CrystalDecisions.CrystalReports.Engine.ReportDocument rc;

                                PED_MSTR pasar_a_ventas = new PED_MSTR();
                                pasar_a_ventas.ESTATUS = "I";
                                pasar_a_ventas.PEDIDO = Pedido;
                                pasar_a_ventas.ModificarStatus(pasar_a_ventas);
                                UPPEDIDOS actualiza_fecha = new UPPEDIDOS();
                                actualiza_fecha.PEDIDO = Pedido;
                                actualiza_fecha.F_IMPRESION = DateTime.Now;
                                actualiza_fecha.Modificar(actualiza_fecha, "Se pasó a VENTAS");
                                rc = (CrystalDecisions.CrystalReports.Engine.ReportDocument)crystalReportViewer1.ReportSource;
                                // ParameterFields parametros = DevuelveParametrosReportePedidos("I", sPedidoDescuento, DS, CMP);
                                ParameterFields parametros = DevuelveParametrosReportePedidos("I", sPedidoDescuento, DS, CMP, this.esDAT, this.descuentoPedido, this.tPedido);

                                rc.SetParameterValue(parametros[0].Name, parametros[0].CurrentValues);
                                rc.SetParameterValue(parametros[1].Name, parametros[1].CurrentValues);
                                rc.SetParameterValue(parametros[2].Name, parametros[2].CurrentValues);
                                rc.PrintToPrinter(1, true, 0, 0);
                                GuardarPed_Temp();
                                ActualizaAcumuladosPed_Mstr();
                                pedidoEstatusModificado = true;
                                if (totProcesos > 0)
                                {
                                    if (
                                        MessageBox.Show("Imprimir también Ordenes de Trabajo?",
                                            "Impresión de Órdenes de Trabajo", MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        DespliegaReporteOrdenTrabajo(Pedido);
                                        CrystalDecisions.CrystalReports.Engine.ReportDocument rcOt;

                                        rcOt =
                                            (CrystalDecisions.CrystalReports.Engine.ReportDocument)
                                                crystalReportViewer1.ReportSource;
                                        rcOt.PrintToPrinter(1, true, 0, 0);
                                        //crystalReportViewer1.PrintReport();

                                        //pedidoTambienImprimeOT = true;
                                    }
                                }

                            }
                        }
                        else
                        {*/

                        if (MessageBox.Show("Desea pasar el PEDIDO a VENTAS?", "VENTAS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            Boolean soloEspecial = false;
                            this.pedidoEstatusModificado = true;
                            // VALIDAMOS SI EL PEDIDO ES 100% ESPECIAL
                            PED_MSTR pedmstr = new PED_MSTR();
                            this.datos_pedido = pedmstr.ConsultaImprimir(this.Pedido);
                            if (this.datos_pedido.Select("LIN_PROD='ESPE'").ToList().Count == this.datos_pedido.Rows.Count)
                            {
                                soloEspecial = true;
                            }

                            //SI EL PEDIDO ES 100% ESPECIAL SE INGRESA A FLUJO SIVO
                            if (soloEspecial)
                            {
                                this.EnviarPedidoCompletoAVentasSIVO();
                                this.InsertaFlujoSIVOSoloEspecial(this.Pedido);
                                break;
                            }
                            else
                            {
                                if (this.datos_pedido.Select("LIN_PROD='ESPE'").FirstOrDefault() != null)
                                {
                                    if (MessageBox.Show("¿Desea crear un pedido adicional con las tallas faltantes?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                                    {
                                        this.EnviarPedidoCompletoAVentasSIVO();
                                        this.InsertaFlujoSIVOSoloEspecial(this.Pedido);
                                        break;
                                    }
                                }
                            }

                            // VALIDAMOS SI EL PEDIDO TIENE EXISTENCIA TOTAL
                            vw_Inventario inventario = new vw_Inventario();
                            DataTable dtValidacionExistencias = inventario.ValidaExistenciaTotalPedido134(this.Pedido);

                            if (dtValidacionExistencias.Rows.Count > 0)
                            {
                                if (!dtValidacionExistencias.Select("(EXISTENCIAS - CANTIDAD) < 0").Any())
                                {
                                    //RN SE CREA UN SOLO PEDIDO CON EL 100% DE PRODUCTO
                                    this.EnviarPedidoCompletoAVentasSIVO();
                                    //RN INSERTAMOS EL FLUJO SIVO
                                    this.InsertaFlujoSIVO(this.Pedido, false);
                                    break;
                                }
                                else
                                {
                                    //RN SE SOLICITA CONFIRMACIÓN DE USUARIO PARA GENERACIÓN DE 1 SOLO PEDIDO O 2 PEDIDOS (TOTAL Y FALTANTE)
                                    if (MessageBox.Show("¿Desea crear un pedido adicional con las tallas faltantes?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                                    {
                                        DataTable dtDivisionPedidos = new DataTable();
                                        if (MessageBox.Show("¿Desea crear 1 Factura por el total del Pedido?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                                        {
                                            // SE CREAN 2 PEDIDOS VIRTUALES (EL PEDIDO ORIGINAL ES EL QUE SE FACTURA)
                                            // PEDIDO 1 POR LA EXISTENCIA TOTAL EN FLUJO NORMAL LINEA/ESPECIAL
                                            // PEDIDO 2 POR EL RESTANTE, (EL FLUJO DEPENDERA DEL TIPO DE FALTANTE)
                                            dtDivisionPedidos = this.DividePedidoSinExistencias(this.Pedido, true);
                                        }
                                        else
                                        {
                                            // SE CREAN 2 PEDIDOS INDEPENDIENTES (CADA PEDIDO TIENE SU PROPIO FLUJO)
                                            // PEDIDO 1 POR LA EXISTENCIA TOTAL EN FLUJO NORMAL LINEA/ESPECIAL
                                            // PEDIDO 2 POR EL RESTANTE, (EL FLUJO DEPENDERA DEL TIPO DE FALTANTE)
                                            dtDivisionPedidos = this.DividePedidoSinExistencias(this.Pedido, false);
                                        }
                                        // BARREMOS LOS PEDIDOS Y LOS DAMOS DE ALTA EN LA LINEA DE TIEMPO SIVO
                                        PED_MSTR pedmstr1 = new PED_MSTR();
                                        if (dtDivisionPedidos.Rows.Count > 0)
                                        {
                                            int pedido1, pedido2;
                                            pedido1 = int.Parse(dtDivisionPedidos.Rows[0]["Pedido1"].ToString());
                                            pedido2 = int.Parse(dtDivisionPedidos.Rows[0]["Pedido2"].ToString());
                                            UPPEDIDOS guardaUppedidos1 = new UPPEDIDOS();
                                            if (pedido1 != 0)
                                            {
                                                // PEDIDO 1: EXISTENCIA TOTAL
                                                // this.datos_pedido1 = pedmstr1.ConsultaImprimir(pedido1);
                                                //int _idProceso1 = int.Parse(ControlPedidos.getSiguienteIDProceso().Rows[0]["ID"].ToString());
                                                this.InsertaFlujoSIVO(pedido1, false);
                                                /*
                                                if (this.datos_pedido1.Select("LIN_PROD='ESPE'").FirstOrDefault() != null)
                                                {
                                                    setAltaLineaTiempoPedido(_idProceso1, "EV", "A", 1, "", Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoEspecial, pedido1, clave_cliente);
                                                    setLineaTiempoPedido(_idProceso1, "GV", "A", 2, "", Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoEspecial, pedido1, clave_cliente);
                                                }
                                                else
                                                {
                                                    setAltaLineaTiempoPedido(_idProceso1, "EV", "A", 1, "", Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoLinea, pedido1, clave_cliente);
                                                    setLineaTiempoPedido(_idProceso1, "GV", "A", 2, "", Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoLinea, pedido1, clave_cliente);
                                                }
                                                */
                                                // INSERTAMOS PEDIDO 1 A UPPEDIDOS
                                                guardaUppedidos1 = new UPPEDIDOS();
                                                guardaUppedidos1.PEDIDO = pedido1;
                                                guardaUppedidos1.COD_CLIENTE = clave_cliente;
                                                guardaUppedidos1.F_CAPT = DateTime.Now;
                                                guardaUppedidos1.Crear(guardaUppedidos1);
                                            }
                                            // PEDIDO 2: FALTANTE
                                            //this.datos_pedido2 = pedmstr1.ConsultaImprimir(pedido2);
                                            this.InsertaFlujoSIVO(pedido2, true);
                                            //int _idProceso2 = int.Parse(ControlPedidos.getSiguienteIDProceso().Rows[0]["ID"].ToString());
                                            /*
                                            if (this.datos_pedido1.Select("LIN_PROD='ESPE'").FirstOrDefault() != null)
                                            {
                                                setAltaLineaTiempoPedido(_idProceso2, "EV", "A", 1, "", Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoEspecial, pedido2, clave_cliente);
                                                setLineaTiempoPedido(_idProceso2, "GV", "A", 2, "", Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoEspecial, pedido2, clave_cliente);
                                            }
                                            else
                                            {
                                                setAltaLineaTiempoPedido(_idProceso2, "EV", "A", 1, "", Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoLinea, pedido2, clave_cliente);
                                                setLineaTiempoPedido(_idProceso2, "GV", "A", 2, "", Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoLinea, pedido2, clave_cliente);
                                            }
                                            */
                                            // INSERTAMOS PEDIDO 2 A UPPEDIDOS: FLUJO DE FALTANTE
                                            guardaUppedidos1 = new UPPEDIDOS();
                                            guardaUppedidos1.PEDIDO = pedido2;
                                            guardaUppedidos1.COD_CLIENTE = clave_cliente;
                                            guardaUppedidos1.F_CAPT = DateTime.Now;
                                            guardaUppedidos1.Crear(guardaUppedidos1);
                                            // INSERTAMOS PEDIDO COMO FALTANTE EN ESTATUS ACTIVO
                                            pedmstr.AltaPedidoFaltante(pedido2);
                                            MessageBox.Show(String.Format(
                                                "Se han generado los pedidos de forma correcta {0} {1}",
                                                pedido1 == 0 ? "" : pedido1.ToString(),
                                                pedido2.ToString()
                                                ), "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        //RN SE CREA UN SOLO PEDIDO CON EL 100% DE PRODUCTO
                                        this.EnviarPedidoCompletoAVentasSIVO();
                                        //RN INSERTAMOS EL FLUJO SIVO
                                        this.InsertaFlujoSIVO(this.Pedido, false);
                                        break;
                                    }
                                }

                                //***********************************************************************************************************
                                // SSI ES UN PEDIDO DIVIDIDO, CREAMOS EL PEDIDO VIRTUAL CON EL RESTO
                                //***********************************************************************************************************
                                pedmstr = new PED_MSTR();
                                // VALIDAMOS QUE EL PEDIDO NO SEA UN PEDIDO VIRTUAL
                                if (dividido)
                                {
                                    DataTable dtValidacionDivision = pedmstr.ValidaDivisionPedido(this.Pedido);
                                    if ((bool)dtValidacionDivision.Rows[0]["PuedeDividir"]) //UN PEDIDO ORIGEN SOLO PUEDE GENERAR 2 PEDIDOS VIRTUALES (LLENO Y FALTANTE)
                                    {
                                        int _pedidoRestante = 0;
                                        int _idProceso = int.Parse(ControlPedidos.getSiguienteIDProceso().Rows[0]["ID"].ToString());
                                        int tipoProceso;
                                        // INSERTAMOS ESTE NUEVO PEDIDO AL FLUJO DE CONTROL PEDIDOS




                                        _pedidoRestante = pedmstr.CreaPedidoVirtualRestante(Pedido);
                                        this.datos_pedidoVirtual = pedmstr.ConsultaImprimir(_pedidoRestante);
                                        if (this.datos_pedidoVirtual.Select("LIN_PROD='ESPE'").FirstOrDefault() != null)
                                        {
                                            tipoProceso = 2;
                                            setAltaLineaTiempoPedido(_idProceso, "EV", "A", 9, "", Globales.UsuarioActual.UsuarioUsuario, tipoProceso, _pedidoRestante, clave_cliente);
                                            setLineaTiempoPedido(_idProceso, "GV", "A", 10, "", Globales.UsuarioActual.UsuarioUsuario, tipoProceso, _pedidoRestante, clave_cliente);
                                        }
                                        else
                                        {
                                            tipoProceso = 3;
                                            setAltaLineaTiempoPedido(_idProceso, "EV", "A", 1, "", Globales.UsuarioActual.UsuarioUsuario, tipoProceso, _pedidoRestante, clave_cliente);
                                            //setLineaTiempoPedido(_idProceso, "CP", "A", 1, "", "sup", tipoProceso, Pedido, clave_cliente);
                                            setLineaTiempoPedido(_idProceso, "GV", "A", 2, "", Globales.UsuarioActual.UsuarioUsuario, tipoProceso, _pedidoRestante, clave_cliente);
                                            MessageBox.Show("Se ha creado el pedido complemento con número: " + _pedidoRestante.ToString() + ".", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                        // INSERTAMOS NUEVO PEDIDO A UPPEDIDOS
                                        UPPEDIDOS guardaUppedidos = new UPPEDIDOS();
                                        guardaUppedidos.PEDIDO = _pedidoRestante;
                                        guardaUppedidos.COD_CLIENTE = clave_cliente;
                                        guardaUppedidos.F_CAPT = DateTime.Now;
                                        guardaUppedidos.Crear(guardaUppedidos);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("No se puede determinar las existencias para el pedido seleccionado, favor de contactar al Administrador del Sistema", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }


                            // PARA PEDIDOS DAT, MOSTRADOR Y ECOMMER NO APLICA FLUJO DE CONTROL PEDIDOS
                            if (this.tPedido == Enumerados.TipoPedido.PedidoDAT || this.tPedido == Enumerados.TipoPedido.PedidoEC || this.tPedido == Enumerados.TipoPedido.PedidoMOS || this.tPedido == Enumerados.TipoPedido.PedidoMOSCP)
                            {
                                CrystalDecisions.CrystalReports.Engine.ReportDocument rc;

                                PED_MSTR pasar_a_ventas = new PED_MSTR();
                                pasar_a_ventas.ESTATUS = "I";
                                pasar_a_ventas.PEDIDO = Pedido;
                                pasar_a_ventas.ModificarStatus(pasar_a_ventas);

                                CMT_DET procesoDireccion = BuscaDireccionEmbarque(Pedido);
                                if (procesoDireccion != null)
                                {
                                    direccionEntrega = procesoDireccion.CMT_DONDE;
                                }

                                /*
                                UPPEDIDOS actualiza_fecha = new UPPEDIDOS();
                                actualiza_fecha.PEDIDO = Pedido;
                                actualiza_fecha.F_IMPRESION = DateTime.Now;
                                actualiza_fecha.Modificar(actualiza_fecha, "Se pasó a VENTAS");
                                 * */
                                rc = (CrystalDecisions.CrystalReports.Engine.ReportDocument)crystalReportViewer1.ReportSource;
                                // ParameterFields parametros = DevuelveParametrosReportePedidos("I", sPedidoDescuento, DS, CMP);
                                ParameterFields parametros = DevuelveParametrosReportePedidos("I", sPedidoDescuento, DS, CMP, this.esDAT, this.descuentoPedido, this.tPedido, direccionEntrega);

                                rc.SetParameterValue(parametros[0].Name, parametros[0].CurrentValues);
                                rc.SetParameterValue(parametros[1].Name, parametros[1].CurrentValues);
                                rc.SetParameterValue(parametros[2].Name, parametros[2].CurrentValues);
                                rc.PrintToPrinter(1, true, 0, 0);
                                GuardarPed_Temp();
                                ActualizaAcumuladosPed_Mstr();
                                pedidoEstatusModificado = true;
                                if (totProcesos > 0)
                                {
                                    if (
                                        MessageBox.Show("Imprimir también Ordenes de Trabajo?",
                                            "Impresión de Órdenes de Trabajo", MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        DespliegaReporteOrdenTrabajo(Pedido);
                                        CrystalDecisions.CrystalReports.Engine.ReportDocument rcOt;

                                        rcOt =
                                            (CrystalDecisions.CrystalReports.Engine.ReportDocument)
                                                crystalReportViewer1.ReportSource;
                                        rcOt.PrintToPrinter(1, true, 0, 0);
                                        //crystalReportViewer1.PrintReport();

                                        pedidoTambienImprimeOT = true;
                                    }
                                }
                            }
                            else
                            {
                                PED_MSTR pasar_a_ventas = new PED_MSTR();
                                pasar_a_ventas.ESTATUS = "I";
                                pasar_a_ventas.PEDIDO = Pedido;
                                pasar_a_ventas.ModificarStatus(pasar_a_ventas);
                                /*UPPEDIDOS actualiza_fecha = new UPPEDIDOS();
                                actualiza_fecha.PEDIDO = Pedido;
                                actualiza_fecha.F_IMPRESION = DateTime.Now;
                                actualiza_fecha.Modificar(actualiza_fecha, "Se pasó a VENTAS");*/
                                GuardarPed_Temp();
                                ActualizaAcumuladosPed_Mstr();
                                pedidoEstatusModificado = true;
                                enVentas = true;
                                // YA NO SE IMPRIMEN LA OT POR NUEVO FLUJO DE CONTROL PEDIDOS
                                /*
                                if (totProcesos > 0)
                                {
                                    if (
                                        MessageBox.Show("Imprimir también Ordenes de Trabajo?",
                                            "Impresión de Órdenes de Trabajo", MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        DespliegaReporteOrdenTrabajo(Pedido);
                                        CrystalDecisions.CrystalReports.Engine.ReportDocument rcOt;

                                        rcOt =
                                            (CrystalDecisions.CrystalReports.Engine.ReportDocument)
                                                crystalReportViewer1.ReportSource;
                                        rcOt.PrintToPrinter(1, true, 0, 0);
                                        //crystalReportViewer1.PrintReport();

                                        //pedidoTambienImprimeOT = true;
                                    }
                                }
                                 * */

                                // AGREGAMOS EL PEDIDO AL CONTROL DE PEDIDOS  (UNICAMENTE PEDIDOS P)
                                DataTable dtControlPedidos = ControlPedidos.getLineaTiempo(this.Pedido);
                                //if (!esDAT && Origen != "frmControlPedidos")
                                if (!esDAT)
                                {
                                    if (dtControlPedidos.Rows.Count == 0)
                                    {
                                        //***********************************************************************************************************
                                        // FLUJO PARA PEDIDO NORMAL
                                        //***********************************************************************************************************
                                        int tipoProceso;
                                        // VERIFICAMOS SI EL PEDIDO TIENE CODIGOS ESPECIALES PARA DEFINIR EL TIPO DE PROCESO PARA CONTROL PEDIDOS
                                        // 2 - PEDIDO ESPECIAL
                                        // 3 - PEDIDO DE LINEA
                                        int _idProceso = int.Parse(ControlPedidos.getSiguienteIDProceso().Rows[0]["ID"].ToString());
                                        if (this.datos_pedido.Select("LIN_PROD='ESPE'").FirstOrDefault() != null)
                                        {
                                            tipoProceso = 2;
                                            setAltaLineaTiempoPedido(_idProceso, "EV", "A", 9, "", Globales.UsuarioActual.UsuarioUsuario, tipoProceso, Pedido, clave_cliente);
                                            setLineaTiempoPedido(_idProceso, "GV", "A", 10, "", Globales.UsuarioActual.UsuarioUsuario, tipoProceso, Pedido, clave_cliente);
                                        }
                                        else
                                        {
                                            tipoProceso = 3;
                                            setAltaLineaTiempoPedido(_idProceso, "EV", "A", 9, "", Globales.UsuarioActual.UsuarioUsuario, tipoProceso, Pedido, clave_cliente);
                                            //setLineaTiempoPedido(_idProceso, "CP", "A", 1, "", "sup", tipoProceso, Pedido, clave_cliente);
                                            setLineaTiempoPedido(_idProceso, "GV", "A", 2, "", Globales.UsuarioActual.UsuarioUsuario, tipoProceso, Pedido, clave_cliente);
                                        }
                                    }
                                    //***********************************************************************************************************
                                    // SSI ES UN PEDIDO DIVIDIDO, CREAMOS EL PEDIDO VIRTUAL CON EL RESTO
                                    //***********************************************************************************************************
                                    pedmstr = new PED_MSTR();
                                    // VALIDAMOS QUE EL PEDIDO NO SEA UN PEDIDO VIRTUAL
                                    if (dividido)
                                    {
                                        DataTable dtValidacionDivision = pedmstr.ValidaDivisionPedido(this.Pedido);
                                        if ((bool)dtValidacionDivision.Rows[0]["PuedeDividir"]) //UN PEDIDO ORIGEN SOLO PUEDE GENERAR 2 PEDIDOS VIRTUALES (LLENO Y FALTANTE)
                                        {
                                            int _pedidoRestante = 0;
                                            int _idProceso = int.Parse(ControlPedidos.getSiguienteIDProceso().Rows[0]["ID"].ToString());
                                            int tipoProceso;
                                            // INSERTAMOS ESTE NUEVO PEDIDO AL FLUJO DE CONTROL PEDIDOS




                                            _pedidoRestante = pedmstr.CreaPedidoVirtualRestante(Pedido);
                                            this.datos_pedidoVirtual = pedmstr.ConsultaImprimir(_pedidoRestante);
                                            if (this.datos_pedidoVirtual.Select("LIN_PROD='ESPE'").FirstOrDefault() != null)
                                            {
                                                tipoProceso = 2;
                                                setAltaLineaTiempoPedido(_idProceso, "EV", "A", 9, "", Globales.UsuarioActual.UsuarioUsuario, tipoProceso, _pedidoRestante, clave_cliente);
                                                setLineaTiempoPedido(_idProceso, "GV", "A", 10, "", Globales.UsuarioActual.UsuarioUsuario, tipoProceso, _pedidoRestante, clave_cliente);
                                            }
                                            else
                                            {
                                                tipoProceso = 3;
                                                setAltaLineaTiempoPedido(_idProceso, "EV", "A", 9, "", Globales.UsuarioActual.UsuarioUsuario, tipoProceso, _pedidoRestante, clave_cliente);
                                                //setLineaTiempoPedido(_idProceso, "CP", "A", 1, "", "sup", tipoProceso, Pedido, clave_cliente);
                                                setLineaTiempoPedido(_idProceso, "GV", "A", 2, "", Globales.UsuarioActual.UsuarioUsuario, tipoProceso, _pedidoRestante, clave_cliente);
                                                MessageBox.Show("Se ha creado el pedido complemento con número: " + _pedidoRestante.ToString() + ".", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                            // INSERTAMOS NUEVO PEDIDO A UPPEDIDOS
                                            UPPEDIDOS guardaUppedidos = new UPPEDIDOS();
                                            guardaUppedidos.PEDIDO = _pedidoRestante;
                                            guardaUppedidos.COD_CLIENTE = clave_cliente;
                                            guardaUppedidos.F_CAPT = DateTime.Now;
                                            guardaUppedidos.Crear(guardaUppedidos);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //}

                    break;
                case Enumerados.TipoReporteCrystal.OrdenTrabajo:
                    if (Origen == "frmFindClie")
                    {
                        if (!enVentas)
                        {
                            if (MessageBox.Show("Desea imprimir la Orden de Trabajo?", "Impresión DEFINITIVA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                PED_MSTR pasar_a_ventas = new PED_MSTR();
                                pasar_a_ventas.ESTATUS = "I";
                                pasar_a_ventas.PEDIDO = Pedido;
                                pasar_a_ventas.ModificarStatus(pasar_a_ventas);
                                pedidoEstatusModificado = true;
                                CrystalDecisions.CrystalReports.Engine.ReportDocument rcOt;
                                rcOt = (CrystalDecisions.CrystalReports.Engine.ReportDocument)crystalReportViewer1.ReportSource;
                                rcOt.PrintToPrinter(1, true, 0, 0);
                            }
                        }
                    }
                    break;
                case Enumerados.TipoReporteCrystal.DatosFacturaBordado:
                    break;
                case Enumerados.TipoReporteCrystal.Requisicion:

                    if (datosRequisicion.Rows[0]["FechaImpresion"].ToString() == "")
                    {
                        if (MessageBox.Show("¿Desea imprimir la requisición y procesar las notificaciones correspondientes?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            CrystalDecisions.CrystalReports.Engine.ReportDocument rdReq;
                            rdReq = (CrystalDecisions.CrystalReports.Engine.ReportDocument)crystalReportViewer1.ReportSource;
                            rdReq.PrintToPrinter(1, true, 0, 0);
                            // Generamos notificación
                            FlujoControlNotificaciones oNotificacion;
                            switch (datosRequisicion.Rows[0]["TipoRequisicion"].ToString())
                            {
                                case "Materia Prima": // MP
                                    oNotificacion = new FlujoControlNotificaciones(Globales.UsuarioActual.UsuarioUsuario, "CPR", "Requisición", String.Format("Generar recepción de MP para el pedido {0}", this.Pedido.ToString()));
                                    oNotificacion.InsertaNotificacion();
                                    break;
                                case "Producto Comprado": // Comprado
                                    oNotificacion = new FlujoControlNotificaciones(Globales.UsuarioActual.UsuarioUsuario, "CA", "Requisición", String.Format("Generar recepción de ropa para el pedido {0}", this.Pedido.ToString()));
                                    oNotificacion.InsertaNotificacion();
                                    break;
                                case "Materia Prima y Producto Comprado": // MP y Comprado
                                    oNotificacion = new FlujoControlNotificaciones(Globales.UsuarioActual.UsuarioUsuario, "CPR", "Requisición", String.Format("Generar recepción de MP para el pedido {0}", this.Pedido.ToString()));
                                    oNotificacion.InsertaNotificacion();
                                    oNotificacion = new FlujoControlNotificaciones(Globales.UsuarioActual.UsuarioUsuario, "CA", "Requisición", String.Format("Generar recepción de ropa para el pedido {0}", this.Pedido.ToString()));
                                    oNotificacion.InsertaNotificacion();
                                    break;
                            }
                            this.procesado = true;
                            // Marcamos impresion
                            RequisicionPedido.ActualizaFechaImpresion(this.Pedido);
                            break;
                        }
                    }
                    break;
                case Enumerados.TipoReporteCrystal.RequisicionMostrador:
                    if (!enVentas)
                    {
                        if (MessageBox.Show("¿Desea imprimir y procesar la requisición?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            CrystalDecisions.CrystalReports.Engine.ReportDocument rc;
                            rc = (CrystalDecisions.CrystalReports.Engine.ReportDocument)crystalReportViewer1.ReportSource;
                            rc.PrintToPrinter(1, true, 0, 0);
                            this.procesado = true;
                        }
                        else { this.procesado = false; }
                    }
                    break;
                default:
                    break;
            }
            var rd = (ReportDocument)crystalReportViewer1.ReportSource;
            rd.Dispose();
        }

        private void frmReportes_Load(object sender, EventArgs e)
        {

        }

        private void DespliegaReportePedidosFirmas(int Pedido)
        {
            CMP = DS = 0;
            rptPedidosFirmas reporte = new rptPedidosFirmas();
            PED_MSTR pedido_imprimir = new PED_MSTR();
            DataTable datos_pedido = new DataTable();
            ActualizaCalculoComision();
            GuardarPed_Temp();
            ActualizaAcumuladosPed_Mstr();

            datos_pedido = pedido_imprimir.ConsultaImprimir(Pedido);

            // VERIFICAMOS SI SE REQUIERE IMPRIMIR YA EL PEDIDO
            if (!imprimirPedido)
            {
                foreach (DataRow dataRow in datos_pedido.Rows)
                {
                    dataRow.SetField<string>("ESTATUS", "P");
                }
            }

            if (Boolean.Parse(datos_pedido.Rows[0]["PedidoDividido"].ToString()))
            {
                this.tPedido = Enumerados.TipoPedido.PedidoDividido;
            }

            totProcesos = datos_pedido.AsEnumerable().Count(c => c["CMT_PROCESO"].ToString() != string.Empty && c["CMT_PROCESO"].ToString() != "L");

            reporte.SetDataSource(datos_pedido);
            DS = double.Parse(datos_pedido.Rows[0]["DS"].ToString());
            CMP = double.Parse(datos_pedido.Rows[0]["CMP"].ToString());
            formaPago = datos_pedido.Rows[0]["formaPago"].ToString();
            metodoPago = datos_pedido.Rows[0]["metodoPago"].ToString();
            usoCFDI = datos_pedido.Rows[0]["usoCFDI"].ToString();
            grupo = datos_pedido.Rows[0]["CORPORATIVO"].ToString();
            descuentoPedido = double.Parse(datos_pedido.Rows[0]["DESCUENTO"].ToString());

            CMT_DET procesoDireccion = BuscaDireccionEmbarque(Pedido);
            if (procesoDireccion != null)
            {
                direccionEntrega = procesoDireccion.CMT_DONDE;
            }

            String tipoProveedor = datos_pedido.Rows[0]["CLASIFIC"].ToString();
            String observacionesProgramacion = datos_pedido.Rows[0]["observacionesProgramacion"].ToString();
            if (!esDAT)
            {
                sPedidoDescuento = PED_MSTR.ConsultaImprimirDevuelveDescuentoCifrado(Convert.ToDouble(datos_pedido.Rows[0]["DESCUENTO"]));
            }
            else
            {
                //sPedidoDescuento = "Descuento:" + (descuentoPedido * 100).ToString() + "%";
                sPedidoDescuento = (descuentoPedido * 100).ToString() + "%";
            }

            try
            {
                foreach (DataRow dr in datos_pedido.Rows)
                {
                    importeDescuentoPedido += Convert.ToDouble(dr["PRECIO"].ToString()) / (1 - Convert.ToDouble(dr["DESCUENTO"].ToString()));
                }
            }
            catch { }

            // OBTENEMOS LAS FIRMAS APLICABLES 
            DataTable dtFirmas = ControlPedidos.getAutorizacionPedido(Pedido); //SE COMENTA A PETICION DEL CLIENTE

            //crystalReportViewer1.ParameterFieldInfo = DevuelveParametrosReportePedidosFirmas(datos_pedido.Rows[0]["Estatus"].ToString(), sPedidoDescuento, DS, CMP, dtFirmas, esDAT, importeDescuentoPedido);
            crystalReportViewer1.ParameterFieldInfo = DevuelveParametrosReportePedidosFirmas(datos_pedido.Rows[0]["Estatus"].ToString(), sPedidoDescuento, DS, CMP, esDAT, importeDescuentoPedido, tipoProveedor, dtFirmas, tPedido, observacionesProgramacion, direccionEntrega);
            crystalReportViewer1.ReportSource = reporte;

            //datos_pedido.WriteXmlSchema(Directory.GetCurrentDirectory().ToString() + @"\pedido.xsd");
        }
        private ParameterFields DevuelveParametrosReportePedidosFirmas(string Estatus, string sDescuento, double DS, double CMP, Boolean esDAT = false, double importeDescuento = 0, string tipoProveedor = "V", DataTable dtFirmas = null, Enumerados.TipoPedido tPedido = Enumerados.TipoPedido.Pedido, string observacionesProgramacion = "", string direccionEntrega = "")
        {
            USUARIOS datos_firma1 = new USUARIOS();
            ParameterFields parameterFields = new ParameterFields();
            ParameterField parametroFirma1 = new ParameterField();
            ParameterDiscreteValue pdvFirma1 = new ParameterDiscreteValue();
            ParameterField parametroNomEquipo = new ParameterField();
            ParameterDiscreteValue pdvNomEquipo = new ParameterDiscreteValue();
            ParameterField parametroStatus = new ParameterField();
            ParameterDiscreteValue pdvStatus = new ParameterDiscreteValue();
            ParameterField parametroDescuento = new ParameterField();
            ParameterDiscreteValue pvdDescuento = new ParameterDiscreteValue();

            ParameterField parametroDS = new ParameterField();
            ParameterDiscreteValue pdvDS = new ParameterDiscreteValue();

            ParameterField parametroCMP = new ParameterField();
            ParameterDiscreteValue pdvCMP = new ParameterDiscreteValue();

            ParameterField parametroFormaPago = new ParameterField();
            ParameterDiscreteValue pdvFormaPago = new ParameterDiscreteValue();

            ParameterField parametroMetodoPago = new ParameterField();
            ParameterDiscreteValue pdvMetodoPago = new ParameterDiscreteValue();

            ParameterField parametroUsoCFDI = new ParameterField();
            ParameterDiscreteValue pdvUsoCFDI = new ParameterDiscreteValue();

            ParameterField parametroCorporativo = new ParameterField();
            ParameterDiscreteValue pdvCorporativo = new ParameterDiscreteValue();

            ParameterField parametroDAT = new ParameterField();
            ParameterDiscreteValue pdvDAT = new ParameterDiscreteValue();

            ParameterField parametroTipoPedido = new ParameterField();
            ParameterDiscreteValue pdvTipoPedido = new ParameterDiscreteValue();

            ParameterField parametroImporteDescuento = new ParameterField();
            ParameterDiscreteValue pdvImporteDescuento = new ParameterDiscreteValue();

            //PARAMETROS DE FIRMAS
            ParameterField parametroFirmaGV = new ParameterField();
            ParameterDiscreteValue pdvFirmaGV = new ParameterDiscreteValue();

            ParameterField parametroFirmaDG = new ParameterField();
            ParameterDiscreteValue pdvFirmaDG = new ParameterDiscreteValue();

            ParameterField parametroFirmaCP = new ParameterField();
            ParameterDiscreteValue pdvFirmaCP = new ParameterDiscreteValue();

            ParameterField parametroFirmaFA = new ParameterField();
            ParameterDiscreteValue pdvFirmaFA = new ParameterDiscreteValue();

            ParameterField parametroFirmaCR = new ParameterField();
            ParameterDiscreteValue pdvFirmaCR = new ParameterDiscreteValue();


            ParameterField parametroTipoProveedor = new ParameterField();
            ParameterDiscreteValue pdvTipoProveedor = new ParameterDiscreteValue();

            ParameterField parametroUsuarioFirmaDG = new ParameterField();
            ParameterDiscreteValue pdvUsuarioFirmaDG = new ParameterDiscreteValue();

            ParameterField parametroObservacionesProgramacion = new ParameterField();
            ParameterDiscreteValue pdvObservacionesProgramacion = new ParameterDiscreteValue();

            ParameterField parametroDireccionEntrega = new ParameterField();
            ParameterDiscreteValue pdvDireccionEntrega = new ParameterDiscreteValue();

            parametroFirma1.Name = "firma1";
            datos_firma1 = datos_firma1.ConsultarPorUsuario(Globales.UsuarioActual.UsuarioUsuario);
            string etiquetaFirma1 = "";
            switch (datos_firma1.GERENTE)
            {
                case "A":
                    etiquetaFirma1 = "Gte Vtas Metropolitano";
                    break;
                case "B":
                    etiquetaFirma1 = "Gte Vtas Interior";
                    break;
                case "X":
                    etiquetaFirma1 = "Gte Sistemas";
                    break;
                default:
                    break;
            }
            pdvFirma1.Value = etiquetaFirma1;
            parametroFirma1.CurrentValues.Add(pdvFirma1);
            parameterFields.Add(parametroFirma1);
            parametroNomEquipo.Name = "nombreEquipo";
            pdvNomEquipo.Value = GlobalesUI.nombreEquipo;
            parametroNomEquipo.CurrentValues.Add(pdvNomEquipo);
            parameterFields.Add(parametroNomEquipo);

            parametroStatus.Name = "Status";
            pdvStatus.Value = Estatus;
            parametroStatus.CurrentValues.Add(pdvStatus);
            parameterFields.Add(parametroStatus);

            parametroDescuento.Name = "Descuento";
            pvdDescuento.Value = sDescuento;
            parametroDescuento.CurrentValues.Add(pvdDescuento);
            parameterFields.Add(parametroDescuento);

            parametroDS.Name = "DS";
            pdvDS.Value = DS;
            parametroDS.CurrentValues.Add(pdvDS);
            parameterFields.Add(parametroDS);

            parametroCMP.Name = "CMP";
            pdvCMP.Value = CMP;
            parametroCMP.CurrentValues.Add(pdvCMP);
            parameterFields.Add(parametroCMP);

            parametroFormaPago.Name = "formaPago";
            pdvFormaPago.Value = formaPago;
            parametroFormaPago.CurrentValues.Add(pdvFormaPago);
            parameterFields.Add(parametroFormaPago);

            parametroMetodoPago.Name = "metodoPago";
            pdvMetodoPago.Value = metodoPago;
            parametroMetodoPago.CurrentValues.Add(pdvMetodoPago);
            parameterFields.Add(parametroMetodoPago);

            parametroUsoCFDI.Name = "usoCFDI";
            pdvUsoCFDI.Value = usoCFDI;
            parametroUsoCFDI.CurrentValues.Add(pdvUsoCFDI);
            parameterFields.Add(parametroUsoCFDI);

            parametroCorporativo.Name = "corporativo";
            pdvCorporativo.Value = grupo;
            parametroCorporativo.CurrentValues.Add(pdvCorporativo);
            parameterFields.Add(parametroCorporativo);

            parametroDAT.Name = "esDAT";
            pdvDAT.Value = esDAT ? "S" : "N";
            parametroDAT.CurrentValues.Add(pdvDAT);
            parameterFields.Add(parametroDAT);

            parametroImporteDescuento.Name = "importeDescuento";
            pdvImporteDescuento.Value = importeDescuento;
            parametroImporteDescuento.CurrentValues.Add(pdvImporteDescuento);
            parameterFields.Add(parametroImporteDescuento);

            parametroTipoProveedor.Name = "tipoProveedor";
            pdvTipoProveedor.Value = tipoProveedor == "" ? "V" : tipoProveedor;
            parametroTipoProveedor.CurrentValues.Add(pdvTipoProveedor);
            parameterFields.Add(parametroTipoProveedor);

            // SE COMENTA ESTA SECCIÓN A PETICION DEL CLIENTE. EL REPORTE MUESTRA TODAS LAS FIRMAS SOLO CUANDO LO IMPRIMA PRODUCCION

            parametroFirmaGV.Name = "autorizacionGV";
            pdvFirmaGV.Value = false;
            parametroFirmaDG.Name = "autorizacionDG";
            pdvFirmaDG.Value = false;
            parametroFirmaCP.Name = "autorizacionCP";
            pdvFirmaCP.Value = false;
            parametroFirmaFA.Name = "autorizacionFA";
            pdvFirmaFA.Value = false;
            parametroFirmaCR.Name = "autorizacionCR";
            pdvFirmaCR.Value = false;
            parametroUsuarioFirmaDG.Name = "usuarioFirmaDG";
            pdvUsuarioFirmaDG.Value = "";


            foreach (DataRow _firma in dtFirmas.Rows)
            {
                switch (_firma["Area"].ToString())
                {
                    case "GV":
                        pdvFirmaGV.Value = true;
                        break;
                    case "DG":
                        pdvFirmaDG.Value = true;
                        pdvUsuarioFirmaDG.Value = _firma["usuario"].ToString();
                        // 
                        break;
                    case "CP":
                        pdvFirmaCP.Value = true;
                        break;
                    case "FA":
                        pdvFirmaFA.Value = true;
                        break;
                    case "CR":
                        pdvFirmaCR.Value = true;
                        break;
                }
            }


            parametroFirmaGV.CurrentValues.Add(pdvFirmaGV);
            parameterFields.Add(parametroFirmaGV);

            parametroFirmaDG.CurrentValues.Add(pdvFirmaDG);
            parameterFields.Add(parametroFirmaDG);

            parametroFirmaCP.CurrentValues.Add(pdvFirmaCP);
            parameterFields.Add(parametroFirmaCP);

            parametroFirmaFA.CurrentValues.Add(pdvFirmaFA);
            parameterFields.Add(parametroFirmaFA);

            parametroFirmaCR.CurrentValues.Add(pdvFirmaCR);
            parameterFields.Add(parametroFirmaCR);

            parametroUsuarioFirmaDG.CurrentValues.Add(pdvUsuarioFirmaDG);
            parameterFields.Add(parametroUsuarioFirmaDG);

            parametroTipoPedido.Name = "tipoPedido";
            switch (tPedido)
            {
                case Enumerados.TipoPedido.Pedido:
                    pdvTipoPedido.Value = "P";
                    break;
                case Enumerados.TipoPedido.PedidoDAT:
                    pdvTipoPedido.Value = "D";
                    break;
                case Enumerados.TipoPedido.PedidoMOS:
                    pdvTipoPedido.Value = "M";
                    break;
                case Enumerados.TipoPedido.PedidoEC:
                    pdvTipoPedido.Value = "EC";
                    break;
                case Enumerados.TipoPedido.PedidoMOSCP:
                    pdvTipoPedido.Value = "MP";
                    break;
                case Enumerados.TipoPedido.PedidoDividido:
                    pdvTipoPedido.Value = "PD";
                    break;
            }

            parametroTipoPedido.CurrentValues.Add(pdvTipoPedido);
            parameterFields.Add(parametroTipoPedido);

            parametroObservacionesProgramacion.Name = "observacionesProgramacion";
            pdvObservacionesProgramacion.Value = observacionesProgramacion;
            parametroObservacionesProgramacion.CurrentValues.Add(pdvObservacionesProgramacion);
            parameterFields.Add(parametroObservacionesProgramacion);

            parametroDireccionEntrega.Name = "direccionEntrega";
            pdvDireccionEntrega.Value = direccionEntrega;
            parametroDireccionEntrega.CurrentValues.Add(pdvDireccionEntrega);
            parameterFields.Add(parametroDireccionEntrega);

            return parameterFields;

        }
        private String setAltaLineaTiempoPedido(int _Pedido, String _ClaveArea, string _Estatus, int _OrdenAgrupador, string _Observaciones, string _usuario, int _cveTipoProceso, int _referenciaProceso, String _Cliente)
        {
            Exception ex = null;
            ControlPedidos.setAltaLineaTiempoPedido(_Pedido, _ClaveArea, _Estatus, _OrdenAgrupador, _Observaciones, _usuario, _cveTipoProceso, _referenciaProceso, _Cliente, ref ex);
            if (ex == null)
                return "";
            else
                return ex.Message;
        }
        private String setLineaTiempoPedido(int _Pedido, String _ClaveArea, string _Estatus, int _OrdenAgrupador, string _Observaciones, string _usuario, int _cveTipoProceso, int _referenciaProceso, String _Cliente)
        {
            Exception ex = null;
            ControlPedidos.setLineaTiempoPedido(_Pedido, _ClaveArea, _Estatus, _OrdenAgrupador, _Observaciones, _usuario, _cveTipoProceso, _referenciaProceso, _Cliente, ref ex);
            if (ex == null)
                return "";
            else
                return ex.Message;
        }
        private void EnviarPedidoCompletoAVentasSIVO()
        {
            PED_MSTR pasar_a_ventas = new PED_MSTR();
            pasar_a_ventas.ESTATUS = "I";
            pasar_a_ventas.PEDIDO = Pedido;
            pasar_a_ventas.ModificarStatus(pasar_a_ventas);
            GuardarPed_Temp();
            ActualizaAcumuladosPed_Mstr();
            pedidoEstatusModificado = true;
            enVentas = true;
        }
        private void InsertaFlujoSIVO(int pedido, Boolean esFaltante)
        {
            DataTable dtControlPedidos = ControlPedidos.getLineaTiempo(pedido);
            PED_MSTR pedmstr = new PED_MSTR();
            DataTable dtPedido = pedmstr.ConsultaImprimir(pedido);

            if (dtControlPedidos.Rows.Count == 0)
            {
                //***********************************************************************************************************
                // FLUJO PARA PEDIDO NORMAL
                //***********************************************************************************************************
                int tipoProceso = 0;
                // VERIFICAMOS SI EL PEDIDO TIENE CODIGOS ESPECIALES PARA DEFINIR EL TIPO DE PROCESO PARA CONTROL PEDIDOS
                // 2 - PEDIDO ESPECIAL
                // 3 - PEDIDO DE LINEA
                // 6 - PEDIDO DAT
                // 7 - PEDIDO MOSTRADOR
                int _idProceso = int.Parse(ControlPedidos.getSiguienteIDProceso().Rows[0]["ID"].ToString());


                if (dtPedido.Select("LIN_PROD='ESPE'").FirstOrDefault() != null)
                {
                    tipoProceso = (int)ControlFlujo.TiposPrceso.PedidoEspecial;
                    setAltaLineaTiempoPedido(_idProceso, "EV", "A", 1, "", Globales.UsuarioActual.UsuarioUsuario, tipoProceso, pedido, clave_cliente);
                    setLineaTiempoPedido(_idProceso, "GV", "A", 3, "", Globales.UsuarioActual.UsuarioUsuario, tipoProceso, pedido, clave_cliente);
                }
                else
                {
                    switch (this.tPedido)
                    {
                        case Enumerados.TipoPedido.PedidoDAT:
                            tipoProceso = (int)ControlFlujo.TiposPrceso.PedidoDAT;
                            if (!esFaltante) // Flujo de existencia
                            {
                                setAltaLineaTiempoPedido(_idProceso, "EV", "A", 1, "", Globales.UsuarioActual.UsuarioUsuario, tipoProceso, pedido, clave_cliente);
                                setLineaTiempoPedido(_idProceso, "GV", "A", 2, "", Globales.UsuarioActual.UsuarioUsuario, tipoProceso, pedido, clave_cliente);
                                this.GeneraNotificacionExistencias(pedido);
                            }
                            else // Flujo de faltante
                            {
                                // RN para el faltante entra como pedido de Linea con flujo de OP
                                setAltaLineaTiempoPedido(_idProceso, "EV", "A", 1, "", Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoLinea, pedido, clave_cliente);
                                setLineaTiempoPedido(_idProceso, "GV", "A", 2, "", Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoLinea, pedido, clave_cliente);
                            }
                            break;
                        case Enumerados.TipoPedido.PedidoMOS:
                        case Enumerados.TipoPedido.PedidoMOSCP:
                        case Enumerados.TipoPedido.PedidoEC:
                            tipoProceso = (int)ControlFlujo.TiposPrceso.PedidoMostrador;
                            string formaPago = dtPedido.Rows[0]["formaPago"].ToString();
                            switch (formaPago)
                            {
                                case "Efectivo":
                                case "Tarjeta de crédito":
                                case "Tarjeta de débito":
                                    if (!esFaltante) // Flujo de existencia
                                    {
                                        setAltaLineaTiempoPedido(_idProceso, "EV", "A", 1, "", Globales.UsuarioActual.UsuarioUsuario, tipoProceso, pedido, clave_cliente);
                                        // **** SE CAMBIA RN YA NO PASA POR CR SINO DIRECTO A CM
                                        setLineaTiempoPedido(_idProceso, "CM", "A", 4, "", Globales.UsuarioActual.UsuarioUsuario, tipoProceso, pedido, clave_cliente);
                                        this.GeneraNotificacionExistencias(pedido);
                                    }
                                    else // Flujo de faltante
                                    {
                                        // RN para el faltante entra como pedido de Linea con flujo de OP
                                        setAltaLineaTiempoPedido(_idProceso, "EV", "A", 1, "", Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoLinea, pedido, clave_cliente);
                                        setLineaTiempoPedido(_idProceso, "GV", "A", 2, "", Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoLinea, pedido, clave_cliente);
                                    }
                                    break;
                                default:
                                    if (!esFaltante) // Flujo de existencia
                                    {
                                        setAltaLineaTiempoPedido(_idProceso, "EV", "A", 1, "", Globales.UsuarioActual.UsuarioUsuario, tipoProceso, pedido, clave_cliente);
                                        // **** SE CAMBIA RN YA NO PASA POR GV SINO DIRECTO A CR
                                        setLineaTiempoPedido(_idProceso, "CR", "A", 3, "", Globales.UsuarioActual.UsuarioUsuario, tipoProceso, pedido, clave_cliente);
                                        this.GeneraNotificacionExistencias(pedido);
                                    }
                                    else // Flujo de faltante
                                    {
                                        // RN para el faltante entra como pedido de Linea con flujo de OP
                                        setAltaLineaTiempoPedido(_idProceso, "EV", "A", 1, "", Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoLinea, pedido, clave_cliente);
                                        setLineaTiempoPedido(_idProceso, "GV", "A", 2, "", Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoLinea, pedido, clave_cliente);
                                    }
                                    break;
                            }
                            break;
                        case Enumerados.TipoPedido.Pedido:
                            tipoProceso = (int)ControlFlujo.TiposPrceso.PedidoLinea;
                            if (!esFaltante) // Flujo de existencia
                            {
                                setAltaLineaTiempoPedido(_idProceso, "EV", "A", 1, "", Globales.UsuarioActual.UsuarioUsuario, tipoProceso, pedido, clave_cliente);
                                setLineaTiempoPedido(_idProceso, "GV", "A", 2, "", Globales.UsuarioActual.UsuarioUsuario, tipoProceso, pedido, clave_cliente);
                                this.GeneraNotificacionExistencias(pedido);
                            }
                            else // Flujo de faltante
                            {
                                setAltaLineaTiempoPedido(_idProceso, "EV", "A", 1, "", Globales.UsuarioActual.UsuarioUsuario, tipoProceso, pedido, clave_cliente);
                                setLineaTiempoPedido(_idProceso, "GV", "A", 2, "", Globales.UsuarioActual.UsuarioUsuario, tipoProceso, pedido, clave_cliente);
                            }
                            break;
                    }
                }
            }
        }
        private void InsertaFlujoSIVOSoloEspecial(int pedido)
        {
            DataTable dtControlPedidos = ControlPedidos.getLineaTiempo(pedido);
            int _idProceso = int.Parse(ControlPedidos.getSiguienteIDProceso().Rows[0]["ID"].ToString());
            if (dtControlPedidos.Rows.Count == 0)
            {
                setAltaLineaTiempoPedido(_idProceso, "EV", "A", 1, "", Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoEspecial, Pedido, clave_cliente);
                setLineaTiempoPedido(_idProceso, "GV", "A", 3, "", Globales.UsuarioActual.UsuarioUsuario, (int)ControlFlujo.TiposPrceso.PedidoEspecial, Pedido, clave_cliente);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>ID Pedido sin existencias</returns>
        private DataTable DividePedidoSinExistencias(int Pedido, Boolean FacturaTotal)
        {
            PED_MSTR division = new PED_MSTR();
            return division.DividePedidoSinExistencias(Pedido, FacturaTotal);
        }
        private void GeneraNotificacionExistencias(int Pedido)
        {
            ControlPedidos.GeneraNotificacionPedidoExistencias(Pedido);
        }

        private void DespliegaRequisicion(int pedido)
        {
            rptRequisicion reporte = new rptRequisicion();
            datosRequisicion = RequisicionPedido.GetRequisicion(pedido);
            reporte.SetDataSource(datosRequisicion);
            crystalReportViewer1.ReportSource = reporte;
        }
        private void DespliegaReporteNotificaciones(String area)
        {
            rptReporteNotificaciones reporte = new rptReporteNotificaciones();
            DataTable datosNotificacion = FlujoControlNotificaciones.GetNotificacionesByAreaReport(area, FlujoControlNotificaciones.Estatus.PENDIENTES);
            reporte.SetDataSource(datosNotificacion);
            crystalReportViewer1.ReportSource = reporte;
        }
        private void DespliegaRequisicionMostrador(int idRequisicion)
        {
            rptRequisicionMostrador reporte = new rptRequisicionMostrador();
            datosRequisicionMostrador = RequisicionMostrador.GetReporteRequisicion(idRequisicion);
            reporte.SetDataSource(datosRequisicionMostrador);
            crystalReportViewer1.ReportSource = reporte;
        }

        private CMT_DET BuscaDireccionEmbarque(int pedido)
        {
            CMT_DET det = new CMT_DET();
            det = CMT_DET.BuscaProcesoEmbarque(pedido);
            return det;
        }
    }
}
