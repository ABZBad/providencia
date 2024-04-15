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
    public partial class frmUpPedidos : Form
    {
        private enum tipoCondicion
        {
            Departamentos,
            Usuarios
        }
        int Pedido = 0;
        string Cliente = "";
        public bool procesado = false;

        public frmUpPedidos()
        {
            InitializeComponent();
        }
        public frmUpPedidos(string idCliente, int idPedido)
        {
            InitializeComponent();
            Pedido = idPedido;
            Cliente = idCliente;
            //USUARIOS datosUsr = new USUARIOS();
            //Globales.DatosUsuario = datosUsr.ConsultarPorUsuario("999");
            DatosPedido();
        }
        private void DatosPedido()
        {
            UPPEDIDOS pedido = new UPPEDIDOS();
            pedido = pedido.Consultar(Pedido);
            if (pedido != null)
            {
                if (pedido.ESTATUS_UPPEDIDOS != "C")
                {
                    //llena datos relacionados con UPPEDIDOS
                    txtFEntrega1.Text = ulp_bl.Utiles.Varios.ValidaNulos(pedido.F_VENCIMIENTO);
                    txtFEstandar.Text = ulp_bl.Utiles.Varios.ValidaNulos(pedido.F_ESTANDAR);
                    txtFProcesos.Text = ulp_bl.Utiles.Varios.ValidaNulos(pedido.PROCESOS);
                    txtComentarios.Text = ulp_bl.Utiles.Varios.ValidaNulos(pedido.COMENTARIOS);
                    txtFPedido.Text = ulp_bl.Utiles.Varios.ValidaNulos(pedido.F_CAPT);
                    txtFImpresion.Text = ulp_bl.Utiles.Varios.ValidaNulos(pedido.F_IMPRESION);
                    txtFGestion.Text = ulp_bl.Utiles.Varios.ValidaNulos(pedido.F_GESTION);
                    txtCapturaSAE.Text = ulp_bl.Utiles.Varios.ValidaNulos(pedido.F_CAPT_ASPEL);
                    txtFCredito.Text = ulp_bl.Utiles.Varios.ValidaNulos(pedido.F_CREDITO);
                    txtFAsigRuta.Text = ulp_bl.Utiles.Varios.ValidaNulos(pedido.F_ASIG_RUTA);
                    txtFLiberado.Text = ulp_bl.Utiles.Varios.ValidaNulos(pedido.F_LIBERADO);
                    txtFSurtido.Text = ulp_bl.Utiles.Varios.ValidaNulos(pedido.F_SURTIDO);
                    txtFBordado.Text = ulp_bl.Utiles.Varios.ValidaNulos(pedido.F_BORDADO);
                    txtFCostura.Text = ulp_bl.Utiles.Varios.ValidaNulos(pedido.F_COSTURA);
                    txtFCorte.Text = ulp_bl.Utiles.Varios.ValidaNulos(pedido.F_CORTE);
                    txtFEstampado.Text = ulp_bl.Utiles.Varios.ValidaNulos(pedido.F_ESTAMPADO);
                    txtFIniciales.Text = ulp_bl.Utiles.Varios.ValidaNulos(pedido.F_INICIALES);
                    txtFEmpaque.Text = ulp_bl.Utiles.Varios.ValidaNulos(pedido.F_EMPAQUE);
                    txtFembarque.Text = ulp_bl.Utiles.Varios.ValidaNulos(pedido.F_EMBARQUE);
                    txtFEntrega2.Text = ulp_bl.Utiles.Varios.ValidaNulos(pedido.F_VENCIMIENTO2);
                    // DATOS ADICIONALES DE CONTROL PEDIDOS
                    txtFCoordinador.Text = ulp_bl.Utiles.Varios.ValidaNulos(pedido.F_COORDINADOR);
                    txtFGV.Text = ulp_bl.Utiles.Varios.ValidaNulos(pedido.F_GV);
                    txtFDireccion.Text = ulp_bl.Utiles.Varios.ValidaNulos(pedido.F_DIRECCION);


                    if (pedido.F_ENTREGADO == null)
                    {
                        dtPickerEntregado.Value = DateTime.Now;
                    }
                    else
                    {
                        dtPickerEntregado.Value = pedido.F_ENTREGADO.Value;
                        dtPickerEntregado.Enabled = false;
                    }

                    lblDEnvioFechaRuta.Text = string.Format("F. de Ruta:    {0}", ulp_bl.Utiles.Varios.ValidaNulos(pedido.FECHARUTA));
                    lblDEnvioGuia.Text = string.Format("Guía:         {0}", ulp_bl.Utiles.Varios.ValidaNulos(pedido.GUIA));
                    lblDEnvioCajas.Text = string.Format("Cajas:        {0}", ulp_bl.Utiles.Varios.ValidaNulos(pedido.CAJAS));
                    lblDEnvioChofer.Text = string.Format("Chofer:       {0}", ulp_bl.Utiles.Varios.ValidaNulos(pedido.CHOFER));
                    lblDEnvioDepartamento.Text = string.Format("Departamento: {0}", ulp_bl.Utiles.Varios.ValidaNulos(pedido.DEPARTAMENTO));
                    lblDEnvioDestino.Text = string.Format("Destino: {0}", ulp_bl.Utiles.Varios.ValidaNulos(pedido.DESTINO));
                    lblDEnvioEstatus.Text = string.Format("Estatus: {0}", ulp_bl.Utiles.Varios.ValidaNulos(pedido.ESTATUS));
                    lblDEnvioTransporte.Text = string.Format("Transporte: {0}", ulp_bl.Utiles.Varios.ValidaNulos(pedido.TRANSPORTE));
                    txtObservaciones.Text = ulp_bl.Utiles.Varios.ValidaNulos(pedido.OBSERVACIONES);
                    //establece tooltips a controles
                    toolTip1.SetToolTip(txtFSurtido, ulp_bl.Utiles.Varios.ValidaNulos(pedido.F_SURTIDO));
                    toolTip1.SetToolTip(txtFBordado, ulp_bl.Utiles.Varios.ValidaNulos(pedido.F_BORDADO));
                    toolTip1.SetToolTip(txtFCostura, ulp_bl.Utiles.Varios.ValidaNulos(pedido.F_COSTURA));
                    toolTip1.SetToolTip(txtFCorte, ulp_bl.Utiles.Varios.ValidaNulos(pedido.F_CORTE));
                    toolTip1.SetToolTip(txtFEstampado, ulp_bl.Utiles.Varios.ValidaNulos(pedido.F_ESTAMPADO));
                    toolTip1.SetToolTip(txtFIniciales, ulp_bl.Utiles.Varios.ValidaNulos(pedido.F_INICIALES));
                    toolTip1.SetToolTip(txtFEmpaque, ulp_bl.Utiles.Varios.ValidaNulos(pedido.F_EMPAQUE));
                    toolTip1.SetToolTip(btnComentarioEntregado, ulp_bl.Utiles.Varios.ValidaNulos(pedido.COM_ENTREGA));


                    //llena datos relacionados con el cliente
                    vw_Clientes datosCliente = new vw_Clientes();
                    datosCliente = datosCliente.Consultar(Cliente);
                    CLIE01 datosCliente1 = new CLIE01();
                    datosCliente1 = datosCliente1.ConsultarIDCadena(Cliente);

                    //lblPedido1.Text = string.Format("PEDIDO: {0}", Pedido.ToString());
                    lblPedido2.Text = datosCliente.NOMBRE_CLIENTE;
                    lblDatosCliente.Text = string.Format("Dirección: {0} {1} {2}", ulp_bl.Utiles.Varios.ValidaNulos(datosCliente1.CALLE).Trim(), ulp_bl.Utiles.Varios.ValidaNulos(datosCliente1.NUMINT).Trim(), ulp_bl.Utiles.Varios.ValidaNulos(datosCliente1.NUMEXT).Trim());
                    lblColonia.Text = string.Format("Colonia: {0}", ulp_bl.Utiles.Varios.ValidaNulos(datosCliente1.COLONIA));
                    lblPoblacion.Text = string.Format("Poblacion: {0}", ulp_bl.Utiles.Varios.ValidaNulos(datosCliente1.MUNICIPIO));
                    lblCodigoPostal.Text = string.Format("CP: {0}", ulp_bl.Utiles.Varios.ValidaNulos(datosCliente1.CODIGO));
                    lblTelefono.Text = string.Format("Teléfono: {0}", ulp_bl.Utiles.Varios.ValidaNulos(datosCliente1.TELEFONO));
                    lblAtencion.Text = string.Format("Atención: {0}", ulp_bl.Utiles.Varios.ValidaNulos(datosCliente.ATENCION));
                    lblDatosCliente6.Text = "";

                    PED_MSTR ped_mstr = new PED_MSTR();
                    ped_mstr = ped_mstr.Consultar(Pedido);

                    lblDatosVta.Text = string.Format("Términos: {0}", ulp_bl.Utiles.Varios.ValidaNulos(ped_mstr.TERMINOS));
                    lblAgente.Text = string.Format("Agente: {0}", ulp_bl.Utiles.Varios.ValidaNulos(ped_mstr.AGENTE));
                    string tipoFormat = "";
                    switch (ped_mstr.TIPO)
                    {
                        case "OV":
                            tipoFormat = "P";
                            break;
                        case "MS":
                            tipoFormat = "M";
                            break;
                        case "EC":
                            tipoFormat = "E";
                            break;
                        case "MP":
                            tipoFormat = "MP";
                            break;
                        case "DT":
                            tipoFormat = "D";
                            break;
                    }
                    lblPedido1.Text = string.Format("PEDIDO: {0}{1}", tipoFormat, Pedido.ToString());

                    DOCTOSIGF01 factura = new DOCTOSIGF01();
                    factura = factura.Consultar(Pedido.ToString());
                    lblFactura.Text = factura.CVE_DOC;

                    //permisos de usuarios a botones
                    btnDesempeñoAreas.Enabled = ulp_bl.Reportes.PermisosUsuarioEspeciales.TienePermisos(Globales.UsuarioActual.Id, 54, 104);
                    dtPickerEntregado.Enabled = ulp_bl.Reportes.PermisosUsuarioEspeciales.TienePermisos(Globales.UsuarioActual.Id, 54, 105);
                    lnkLog.Visible = ulp_bl.Reportes.PermisosUsuarioEspeciales.TienePermisos(Globales.UsuarioActual.Id, 54, 106);
                }
                else
                {
                    lblCancelado.Visible = true;
                    MessageBox.Show("Pedido cancelado", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Éste es un documento mostrador, el cual no es aún contemplado.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }



        }

        private bool PuedeActualizar(TextBox txtAValidar, tipoCondicion tipoCond, string departACondicionar)
        {
            bool resp = PuedeActualizar(tipoCond, departACondicionar);
            if (resp)
            {
                if (txtAValidar.Text != "")
                {
                    resp = false;
                }
            }
            return resp;
        }
        private bool PuedeActualizar(tipoCondicion tipoCond, string departACondicionar)
        {
            bool Resp = false;
            if (tipoCond == tipoCondicion.Departamentos)
            {
                if (Globales.DatosUsuario.DEPARTAMENTO != null)
                {
                    if (Globales.DatosUsuario.DEPARTAMENTO.ToUpper().Trim() == departACondicionar || Globales.DatosUsuario.DEPARTAMENTO.ToUpper().Trim() == "TODOS" || Globales.DatosUsuario.DEPARTAMENTO.ToUpper().Trim() == "GO" || Globales.DatosUsuario.DEPARTAMENTO.ToUpper().Trim() == "SURTIDO")
                    {
                        Resp = true;

                    }
                }
            }
            else
            {
                if (Globales.DatosUsuario.CLAVE.ToUpper() == "LSANCHEZ" || Globales.DatosUsuario.CLAVE.ToUpper() == "SUP")
                {
                    Resp = true;
                }
            }
            return Resp;
        }
        private object devuelveValorAACtualizar(string titulo)
        {
            object valor = null;
            frmInputBox input = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto);
            input.Text = "SIP";
            input.lblTitulo.Text = titulo;
            if (input.ShowDialog() == DialogResult.OK)
            {
                valor = input.txtOrden.Text;
            }
            return valor;
        }

        private void frmUpPedidos_Load(object sender, EventArgs e)
        {

        }

        private void btnComentarioCred_Click(object sender, EventArgs e)
        {
            if (PuedeActualizar(tipoCondicion.Departamentos, "CREDITO"))
            {
                string valorAActualizar = (string)devuelveValorAACtualizar("COMENTARIOS");
                if (valorAActualizar != null)
                {
                    UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                    uppedidosModif.PEDIDO = Pedido;
                    uppedidosModif.COM_CREDITO = valorAActualizar;
                    uppedidosModif.Modificar(uppedidosModif, "UpPedidos");
                }
            }

        }

        private void btnComentarioSurtido_Click(object sender, EventArgs e)
        {
            if (PuedeActualizar(tipoCondicion.Departamentos, "EMPAQUE"))
            {
                string valorAActualizar = (string)devuelveValorAACtualizar("COMENTARIOS");
                if (valorAActualizar != null)
                {
                    UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                    uppedidosModif.PEDIDO = Pedido;
                    uppedidosModif.COM_SURTIDO = valorAActualizar;
                    uppedidosModif.Modificar(uppedidosModif, "UpPedidos");
                }
            }
        }

        private void btnComentarioBordado_Click(object sender, EventArgs e)
        {
            if (PuedeActualizar(tipoCondicion.Departamentos, "PROCESOS"))
            {
                string valorAActualizar = (string)devuelveValorAACtualizar("COMENTARIOS");
                if (valorAActualizar != null)
                {
                    UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                    uppedidosModif.PEDIDO = Pedido;
                    uppedidosModif.COM_BORDADO = valorAActualizar;
                    uppedidosModif.Modificar(uppedidosModif, "UpPedidos");
                }
            }
        }

        private void btnComentarioCostura_Click(object sender, EventArgs e)
        {
            if (PuedeActualizar(tipoCondicion.Departamentos, "PROCESOS"))
            {
                string valorAActualizar = (string)devuelveValorAACtualizar("COMENTARIOS");
                if (valorAActualizar != null)
                {
                    UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                    uppedidosModif.PEDIDO = Pedido;
                    uppedidosModif.COM_COSTURA = valorAActualizar;
                    uppedidosModif.Modificar(uppedidosModif, "UpPedidos");
                }
            }
        }

        private void btnComentarioCorte_Click(object sender, EventArgs e)
        {
            if (PuedeActualizar(tipoCondicion.Departamentos, "CORTE"))
            {
                string valorAActualizar = (string)devuelveValorAACtualizar("COMENTARIOS");
                if (valorAActualizar != null)
                {
                    UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                    uppedidosModif.PEDIDO = Pedido;
                    uppedidosModif.COM_CORTE = valorAActualizar;
                    uppedidosModif.Modificar(uppedidosModif, "UpPedidos");
                }
            }
        }

        private void btnComentarioEstampado_Click(object sender, EventArgs e)
        {
            if (PuedeActualizar(tipoCondicion.Departamentos, "ESTAMPADO"))
            {
                string valorAActualizar = (string)devuelveValorAACtualizar("COMENTARIOS");
                if (valorAActualizar != null)
                {
                    UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                    uppedidosModif.PEDIDO = Pedido;
                    uppedidosModif.COM_ESTAMPADO = valorAActualizar;
                    uppedidosModif.Modificar(uppedidosModif, "UpPedidos");
                }
            }
        }

        private void btnComentarioInicial_Click(object sender, EventArgs e)
        {
            if (PuedeActualizar(tipoCondicion.Departamentos, "INICIALES"))
            {
                string valorAActualizar = (string)devuelveValorAACtualizar("COMENTARIOS");
                if (valorAActualizar != null)
                {
                    UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                    uppedidosModif.PEDIDO = Pedido;
                    uppedidosModif.COM_INICIALES = valorAActualizar;
                    uppedidosModif.Modificar(uppedidosModif, "UpPedidos");
                }
            }
        }

        private void btnComentarioEmpaq_Click(object sender, EventArgs e)
        {
            if (PuedeActualizar(tipoCondicion.Departamentos, "EMPAQUE"))
            {
                string valorAActualizar = (string)devuelveValorAACtualizar("COMENTARIOS");
                if (valorAActualizar != null)
                {
                    UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                    uppedidosModif.PEDIDO = Pedido;
                    uppedidosModif.COM_EMPAQUE = valorAActualizar;
                    uppedidosModif.Modificar(uppedidosModif, "UpPedidos");
                }
            }
        }

        private void btnComentarioEntregado_Click(object sender, EventArgs e)
        {
            if (PuedeActualizar(tipoCondicion.Usuarios, ""))
            {
                string valorAActualizar = (string)devuelveValorAACtualizar("COMENTARIOS");
                if (valorAActualizar != null)
                {
                    UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                    uppedidosModif.PEDIDO = Pedido;
                    uppedidosModif.COM_ENTREGA = valorAActualizar;
                    uppedidosModif.Modificar(uppedidosModif, "UpPedidos");
                }
            }
        }

        private void btnProceEntrega_Click(object sender, EventArgs e)
        {
            if (PuedeActualizar(txtFEntrega1, tipoCondicion.Departamentos, "PROCESOS"))
            {
                //solicita primera fecha
                string valorAActualizar = (string)devuelveValorAACtualizar("FECHA de Entrega");
                DateTime dateResult;
                bool fechaValida = DateTime.TryParse(valorAActualizar, out dateResult);
                if (fechaValida)
                {
                    if (dateResult.Year < 2000)
                    {
                        fechaValida = false;
                    }

                }

                if (fechaValida)
                {
                    valorAActualizar = (string)devuelveValorAACtualizar("FECHA de Venc PROCESOS");
                    DateTime dateResult2;
                    bool fechaValida2 = DateTime.TryParse(valorAActualizar, out dateResult2);
                    if (fechaValida2)
                    {
                        if (dateResult2.Year < 2000)
                        {
                            fechaValida2 = false;
                        }

                    }
                    if (fechaValida2)
                    {
                        valorAActualizar = (string)devuelveValorAACtualizar("FECHA de Venc EMPAQUE");
                        DateTime dateResult3;
                        bool fechaValida3 = DateTime.TryParse(valorAActualizar, out dateResult3);
                        if (fechaValida3)
                        {
                            if (dateResult3.Year < 2000)
                            {
                                fechaValida3 = false;
                            }

                        }
                        if (fechaValida3)
                        {
                            //actualiza uppedidos
                            UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                            uppedidosModif.PEDIDO = Pedido;
                            uppedidosModif.F_VENCIMIENTO = dateResult;
                            uppedidosModif.Modificar(uppedidosModif, "UpPedidos");
                            //actualiza CMT_DET
                            CMT_DET cmt_det = new CMT_DET();
                            cmt_det.CMT_PEDIDO = Pedido;
                            cmt_det.CMT_FVENC = dateResult2;
                            cmt_det.CMT_FVENC_EMPAQUE = dateResult3;
                            cmt_det.ModificarPorPedido(cmt_det);
                            this.procesado = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Formato de fecha inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Formato de fecha inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (PuedeActualizar(txtFEntrega2, tipoCondicion.Departamentos, "PROCESOS"))
                {
                    //solicita primera fecha
                    string valorAActualizar = (string)devuelveValorAACtualizar("FECHA de Entrega");
                    DateTime dateResult;

                    bool fechaValida = DateTime.TryParse(valorAActualizar, out dateResult);
                    if (fechaValida)
                    {
                        if (dateResult.Year < 2000)
                        {
                            fechaValida = false;
                        }

                    }

                    if (fechaValida)
                    {
                        valorAActualizar = (string)devuelveValorAACtualizar("FECHA de Venc PROCESOS");
                        DateTime dateResult2;
                        bool fechaValida2 = DateTime.TryParse(valorAActualizar, out dateResult2);
                        if (fechaValida2)
                        {
                            if (dateResult2.Year < 2000)
                            {
                                fechaValida2 = false;
                            }

                        }
                        if (fechaValida2)
                        {
                            valorAActualizar = (string)devuelveValorAACtualizar("FECHA de Venc EMPAQUE");
                            DateTime dateResult3;
                            bool fechaValida3 = DateTime.TryParse(valorAActualizar, out dateResult3);
                            if (fechaValida3)
                            {
                                if (dateResult3.Year < 2000)
                                {
                                    fechaValida3 = false;
                                }

                            }
                            if (fechaValida3)
                            {
                                //actualiza uppedidos
                                UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                                uppedidosModif.PEDIDO = Pedido;
                                uppedidosModif.F_VENCIMIENTO2 = dateResult;
                                uppedidosModif.Modificar(uppedidosModif, "UpPedidos");
                                //actualiza CMT_DET
                                CMT_DET cmt_det = new CMT_DET();
                                cmt_det.CMT_PEDIDO = Pedido;
                                cmt_det.CMT_FVENC = dateResult2;
                                cmt_det.CMT_FVENC_EMPAQUE = dateResult3;
                                cmt_det.ModificarPorPedido(cmt_det);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Formato de fecha inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Formato de fecha inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Esta fecha ya ha sido asignada anteriormente o no puede ser asignada de manera manual.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnProceEstandar_Click(object sender, EventArgs e)
        {
            if (PuedeActualizar(txtFEstandar, tipoCondicion.Departamentos, "GESTION"))
            {
                string valorAActualizar = (string)devuelveValorAACtualizar("FECHA de Estandar");
                DateTime dateResult;
                if (DateTime.TryParse(valorAActualizar, out dateResult))
                {
                    UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                    uppedidosModif.PEDIDO = Pedido;
                    uppedidosModif.F_ESTANDAR = dateResult;
                    uppedidosModif.Modificar(uppedidosModif, "UpPedidos");
                }
                else
                {
                    MessageBox.Show("Formato de fecha inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Esta fecha ya ha sido asignada anteriormente o no puede ser asignada de manera manual.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnProcesos_Click(object sender, EventArgs e)
        {
            if (PuedeActualizar(txtFProcesos, tipoCondicion.Departamentos, "PROCESOS"))
            {
                string valorAActualizar = (string)devuelveValorAACtualizar("PROCESOS");
                if (valorAActualizar != null)
                {
                    UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                    uppedidosModif.PEDIDO = Pedido;
                    uppedidosModif.PROCESOS = valorAActualizar;
                    uppedidosModif.Modificar(uppedidosModif, "UpPedidos");
                }
            }
            else
            {
                MessageBox.Show("Esta fecha ya ha sido asignada anteriormente o no puede ser asignada de manera manual.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnComentarios_Click(object sender, EventArgs e)
        {
            if (PuedeActualizar(tipoCondicion.Departamentos, "SURTIDO"))
            {
                string valorAActualizar = (string)devuelveValorAACtualizar("COMENTARIOS");
                if (valorAActualizar != null)
                {
                    UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                    uppedidosModif.PEDIDO = Pedido;
                    uppedidosModif.COMENTARIOS = valorAActualizar;
                    uppedidosModif.Modificar(uppedidosModif, "UpPedidos");
                }
            }
        }

        private void btnProcePedido_Click(object sender, EventArgs e)
        {
            //no trae código
            MessageBox.Show("Esta fecha ya ha sido asignada anteriormente o no puede ser asignada de manera manual.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnProceImpre_Click(object sender, EventArgs e)
        {
            //no trae código
            MessageBox.Show("Esta fecha ya ha sido asignada anteriormente o no puede ser asignada de manera manual.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnProceGest_Click(object sender, EventArgs e)
        {
            //no trae código
            MessageBox.Show("Esta fecha ya ha sido asignada anteriormente o no puede ser asignada de manera manual.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnProceCaptura_Click(object sender, EventArgs e)
        {
            if (PuedeActualizar(txtCapturaSAE, tipoCondicion.Departamentos, "CAPTURA"))
            {
                UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                uppedidosModif.PEDIDO = Pedido;
                uppedidosModif.F_CAPT_ASPEL = DateTime.Now;
                uppedidosModif.Modificar(uppedidosModif, "UpPedidos");
            }
            else
            {
                MessageBox.Show("Esta fecha ya ha sido asignada anteriormente o no puede ser asignada de manera manual.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnProceCred_Click(object sender, EventArgs e)
        {
            if (PuedeActualizar(txtFCredito, tipoCondicion.Departamentos, "PROCESOS"))
            {
                UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                uppedidosModif.PEDIDO = Pedido;
                uppedidosModif.F_CREDITO = DateTime.Now;
                uppedidosModif.Modificar(uppedidosModif, "UpPedidos");
                this.procesado = true; // PL ASIGNA FECHA DE CREDITO Y SE ENVIA A DIRECCION
            }
            else
            {
                MessageBox.Show("Esta fecha ya ha sido asignada anteriormente o no puede ser asignada de manera manual.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnProceAsigRuta_Click(object sender, EventArgs e)
        {
            if (PuedeActualizar(txtFAsigRuta, tipoCondicion.Departamentos, "PROCESOS"))
            {
                UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                uppedidosModif.PEDIDO = Pedido;
                uppedidosModif.F_ASIG_RUTA = DateTime.Now;
                uppedidosModif.Modificar(uppedidosModif, "UpPedidos");
                this.procesado = true;
            }
            else
            {
                MessageBox.Show("Esta fecha ya ha sido asignada anteriormente o no puede ser asignada de manera manual.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnProceLiberado_Click(object sender, EventArgs e)
        {
            if (PuedeActualizar(txtFLiberado, tipoCondicion.Departamentos, "SURTIDO"))
            {
                UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                uppedidosModif.PEDIDO = Pedido;
                uppedidosModif.F_LIBERADO = DateTime.Now;
                uppedidosModif.Modificar(uppedidosModif, "UpPedidos");
                AsignarRuta.LiberarPedido(Pedido);
                this.procesado = true;
            }
            else
            {
                MessageBox.Show("Esta fecha ya ha sido asignada anteriormente o no puede ser asignada de manera manual.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnProceSurtido_Click(object sender, EventArgs e)
        {
            if (PuedeActualizar(txtFSurtido, tipoCondicion.Departamentos, "EMPAQUE"))
            {
                UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                uppedidosModif.PEDIDO = Pedido;
                uppedidosModif.F_SURTIDO = DateTime.Now;
                uppedidosModif.Modificar(uppedidosModif, "UpPedidos");
                this.procesado = true;
            }
            else
            {
                MessageBox.Show("Esta fecha ya ha sido asignada anteriormente o no puede ser asignada de manera manual.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnProceBordado_Click(object sender, EventArgs e)
        {
            if (PuedeActualizar(txtFBordado, tipoCondicion.Departamentos, "PROCESOS"))
            {
                UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                uppedidosModif.PEDIDO = Pedido;
                uppedidosModif.F_BORDADO = DateTime.Now;
                uppedidosModif.Modificar(uppedidosModif, "UpPedidos");
            }
            else
            {
                MessageBox.Show("Esta fecha ya ha sido asignada anteriormente o no puede ser asignada de manera manual.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnProceCostura_Click(object sender, EventArgs e)
        {
            if (PuedeActualizar(txtFCostura, tipoCondicion.Departamentos, "PROCESOS"))
            {
                UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                uppedidosModif.PEDIDO = Pedido;
                uppedidosModif.F_COSTURA = DateTime.Now;
                uppedidosModif.Modificar(uppedidosModif, "UpPedidos");
            }
            else
            {
                MessageBox.Show("Esta fecha ya ha sido asignada anteriormente o no puede ser asignada de manera manual.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnProceCorte_Click(object sender, EventArgs e)
        {
            if (PuedeActualizar(txtFCorte, tipoCondicion.Departamentos, "CORTE"))
            {
                UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                uppedidosModif.PEDIDO = Pedido;
                uppedidosModif.F_CORTE = DateTime.Now;
                uppedidosModif.Modificar(uppedidosModif, "UpPedidos");
            }
            else
            {
                MessageBox.Show("Esta fecha ya ha sido asignada anteriormente o no puede ser asignada de manera manual.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnProceEstampado_Click(object sender, EventArgs e)
        {
            if (PuedeActualizar(txtFEstampado, tipoCondicion.Departamentos, "ESTAMPADO"))
            {
                UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                uppedidosModif.PEDIDO = Pedido;
                uppedidosModif.F_ESTAMPADO = DateTime.Now;
                uppedidosModif.Modificar(uppedidosModif, "UpPedidos");
            }
            else
            {
                MessageBox.Show("Esta fecha ya ha sido asignada anteriormente o no puede ser asignada de manera manual.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnProceIniciales_Click(object sender, EventArgs e)
        {
            if (PuedeActualizar(txtFIniciales, tipoCondicion.Departamentos, "PROCESOS"))
            {
                UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                uppedidosModif.PEDIDO = Pedido;
                uppedidosModif.F_INICIALES = DateTime.Now;
                uppedidosModif.Modificar(uppedidosModif, "UpPedidos");
            }
            else
            {
                MessageBox.Show("Esta fecha ya ha sido asignada anteriormente o no puede ser asignada de manera manual.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnProceEmpaque_Click(object sender, EventArgs e)
        {
            if (PuedeActualizar(txtFEmpaque, tipoCondicion.Departamentos, "EMPAQUE"))
            {
                UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                uppedidosModif.PEDIDO = Pedido;
                uppedidosModif.F_EMPAQUE = DateTime.Now;
                uppedidosModif.Modificar(uppedidosModif, "UpPedidos");
            }
            else
            {
                MessageBox.Show("Esta fecha ya ha sido asignada anteriormente o no puede ser asignada de manera manual.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnProceEmbarque_Click(object sender, EventArgs e)
        {
            if (PuedeActualizar(txtFembarque, tipoCondicion.Departamentos, "EMBARQUES"))
            {
                UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                uppedidosModif.PEDIDO = Pedido;
                uppedidosModif.F_EMBARQUE = DateTime.Now;
                uppedidosModif.Modificar(uppedidosModif, "UpPedidos");
            }
            else
            {
                MessageBox.Show("Esta fecha ya ha sido asignada anteriormente o no puede ser asignada de manera manual.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnProceEntegrado_Click(object sender, EventArgs e)
        {
            if (dtPickerEntregado.Enabled)
            {
                UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                uppedidosModif.PEDIDO = Pedido;
                uppedidosModif.F_ENTREGADO = dtPickerEntregado.Value;
                uppedidosModif.Modificar(uppedidosModif, "UpPedidos");
            }
            else
            {
                MessageBox.Show("Fecha previamente asignada o sin permisos para edición de este campo.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDesempeñoAreas_Click(object sender, EventArgs e)
        {
            frmDesempByArea frmDesempByArea = new frmDesempByArea(Pedido);
            frmDesempByArea.Show();
        }

        private void btnPartidas_Click(object sender, EventArgs e)
        {
            frmPartidas partidas = new frmPartidas(Pedido);
            partidas.Show();
        }

        private void lnkLog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmUpPedidosLog frmUpPedidosLog = new frmUpPedidosLog(Pedido);
            frmUpPedidosLog.Show();
        }

        private void txtFLiberado_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnProceCoordinador_Click(object sender, EventArgs e)
        {
            if (PuedeActualizar(txtFCoordinador, tipoCondicion.Departamentos, "CAPTURA"))
            {
                UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                uppedidosModif.PEDIDO = Pedido;
                uppedidosModif.F_COORDINADOR = DateTime.Now;
                uppedidosModif.Modificar(uppedidosModif, "Autorización de CP - Control Pedidos");
            }
            else
            {
                MessageBox.Show("Esta fecha ya ha sido asignada anteriormente o no puede ser asignada de manera manual.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnProceGV_Click(object sender, EventArgs e)
        {
            if (PuedeActualizar(txtFGV, tipoCondicion.Departamentos, "GVI") || PuedeActualizar(txtFGV, tipoCondicion.Departamentos, "GVM"))
            {
                UPPEDIDOS uppedidosModif = new UPPEDIDOS();
                uppedidosModif.PEDIDO = Pedido;
                uppedidosModif.F_GV = DateTime.Now;
                uppedidosModif.Modificar(uppedidosModif, "Autorización de GV - Control Pedidos");
            }
            else
            {
                MessageBox.Show("Esta fecha ya ha sido asignada anteriormente o no puede ser asignada de manera manual.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnProceDireccion_Click(object sender, EventArgs e)
        {
            //no trae código
            MessageBox.Show("Esta fecha ya ha sido asignada anteriormente o no puede ser asignada de manera manual.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void frmUpPedidos_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (txtFEntrega1.Text != "") this.procesado = true;
        }

    }
}
