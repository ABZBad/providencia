using System;
using System.Globalization;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ulp_bl;
using SIP.Utiles;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using CrystalDecisions.Shared;

namespace SIP
{
    public partial class frmFindClie : Form
    {
        Enumerados.TipoTareaProcesamiento tipoTarea = new Enumerados.TipoTareaProcesamiento();
        Enumerados.TipoPedido tipoPedido = new Enumerados.TipoPedido();
        //AddPartidasPedi frmAgregarPartidas;
        AddPartidasPedi2 frmAgregarPartidas;
        frmInputBox f;
        bool _otroTipoPrendaInidicado = false;
        bool _otroTipoComposicionInidicado = false;
        bool _otroTipoColorInidicado = false;

        Reportes.frmReportes frmReportes;
        Precarga precarga;
        private Timer tmrCargaDatos = new Timer();
        private string id_Cliente = "";
        string nombre_vendedor = "";
        string clave_cliente = "";
        string status = "A";
        bool ordenTrabajoNueva;
        bool pedidoNuevo;
        bool pedidoDATNuevo;
        bool pedidoMOSNuevo;
        bool pedidoMOSCPNuevo;
        bool solicitudNueva;
        String _observaciones;
        bool logotipoNuevo = false;
        bool huboCambiosEnPedido;
        private string CURR_COD_CLIENTE = "";
        private int CURR_SELECTED_NDX = 0;
        private int PREV_SELECTED_NDX = 0;
        string Termino;
        string Agente;
        DataTable pedidos = new DataTable("PED_MSTR");
        DataTable pedidosVirtuales = new DataTable("PED_MSTR");
        DataTable pedidosDAT = new DataTable("PED_MSTR");
        DataTable pedidosMOS = new DataTable("PED_MSTR");
        DataTable pedidosMOSCP = new DataTable("PED_MSTR");
        DataTable logotipos = new DataTable("IMAGENES");
        DataTable logotiposOri = new DataTable("IMAGENES_ORI");
        DataTable OrdenTrabajo = new DataTable("PED_MSTR");
        DataTable Solicitudes = new DataTable("SOLICITUDES");
        List<TextBox> txtPedSipAValidar = new List<TextBox>();
        List<ComboBox> cmbPedSipAValidar = new List<ComboBox>();
        List<TextBox> txtPedDATSipAValidar = new List<TextBox>();
        List<ComboBox> cmbPedDATSipAValidar = new List<ComboBox>();
        List<TextBox> txtPedMOSSipAValidar = new List<TextBox>();
        List<TextBox> txtPedMOSCPAValidar = new List<TextBox>();
        List<ComboBox> cmbPedMOSSipAValidar = new List<ComboBox>();
        List<ComboBox> cmbPedMOSCPAValidar = new List<ComboBox>();
        List<TextBox> txtLogosAValidar = new List<TextBox>();
        List<TextBox> txtSolicitudesAValidar = new List<TextBox>();
        List<TextBox> txtSolicitudesAValidarEnCero = new List<TextBox>();

        List<ComboBox> cmbSolicitudesAValidar = new List<ComboBox>();
        private Timer tmrTextBoxHilight;

        BackgroundWorker bgwInfoCliente = new BackgroundWorker();
        BackgroundWorker backgroundWorker = new BackgroundWorker();
        private bool DatosCrgados;

        private bool accesaTab1;
        private bool accesaTab2;
        private bool accesaTab3;
        private bool accesaTab4;
        private bool accesaTab5;
        private bool accesaTab6;
        private bool accesaTab7;
        private bool accesaTab8;
        private bool accesaTab9;

        DataTable dtCalogoFormaPagoComisiones;
        decimal formaPagoComision = 0;
        string frmOrigen = "";
        string tab = "";
        string clasificacionVendedor = "";

        private Dictionary<int, bool> accesosTabs = new Dictionary<int, bool>();

        #region PedidosSIP
        private void PedidosSIPHabilitaControlesNavegacionParaPrimerRegistro(bool Habilita)
        {
            toolStripBtnPedidosSIPGuardar.Enabled = Habilita;
            toolStripBtnPedidosSIPEliminar.Enabled = Habilita;
        }
        private void PedidosSIPHabilitaControlesNavegacion(bool Habilita)
        {
            bindingNavigatorMoveFirstItem.Enabled = Habilita;
            bindingNavigatorMovePreviousItem.Enabled = Habilita;
            bindingNavigatorMoveNextItem.Enabled = Habilita;
            bindingNavigatorMoveLastItem.Enabled = Habilita;
            toolStripBtnPedidosSIPNuevo.Enabled = Habilita;
            bindingNavigatorPositionItem.Enabled = Habilita;
        }
        private void PedidosSIPHabilitaControlesCambios(bool Habilita)
        {
            lblPedSIPYaImpreso.Visible = !Habilita;
            txtPedSIPOc.Enabled = Habilita;
            txtPedSIPRemitido.Enabled = Habilita;
            txtPedSIPConsignado.Enabled = Habilita;
            txtPedSIPObservaciones.Enabled = Habilita;
            if (Globales.UsuarioActual.UsuarioUsuario.Trim().ToUpper() == "MOSTRADOR")
            {
                btnPedSIPAgregarPartidas.Enabled = this.clasificacionVendedor == "F";
                btnPedSIPElimModPartidas.Enabled = this.clasificacionVendedor == "F";
            }
            else
            {
                btnPedSIPAgregarPartidas.Enabled = Habilita && !lblPedSIPDivision.Visible;
                btnPedSIPElimModPartidas.Enabled = Habilita;
            }

            btnPedSIPTerminosActualiza.Enabled = Habilita;
            toolStripBtnPedidosSIPGuardar.Enabled = Habilita;
            btnPedSIPImprimir.Enabled = Habilita;
            //chkPedSIPEcommerce.Enabled = Habilita;
            txtPedSIPCotizacion.Enabled = Habilita;
            btnPedSIPDocumentos.Enabled = true;
        }
        private void PedidosSIPEvaluaHabilitarControlesCambios()
        {
            if (bindingSourcePedidosSIP.Count > 0)
            {
                DataRowView row = (DataRowView)bindingSourcePedidosSIP.Current;
                string status = row["ESTATUS"].ToString();
                lblPedSIPDivision.Visible = this.pedidosVirtuales.Select("PedidoDivision = " + row["PEDIDO"].ToString()).Count() > 0;
                if (status == "I")
                {
                    PedidosSIPHabilitaControlesCambios(false);
                    btnPedSIPImprimir.Enabled = true;
                }
                else
                {
                    PedidosSIPHabilitaControlesCambios(true);
                }
            }
            else
            {
                PedidosSIPHabilitaControlesNavegacionParaPrimerRegistro(false);
                PedidosSIPHabilitaControlesCambios(false);
                lblPedSIPYaImpreso.Visible = false;
            }
        }
        private void PedidosSIPLlenaDatos(string id, bool reload = false)
        {
            PED_MSTR pedidosBl = new PED_MSTR();
            pedidos = pedidosBl.CosultarPedidosCliente(id);
            // QUITAMOS LOS PEDIDOS VIRTUALES
            List<int> _virtual = new List<int> { };
            foreach (DataRow dr in this.pedidosVirtuales.Rows)
            {
                _virtual.Add(int.Parse(dr["Pedido"].ToString()));
            }
            var query = from row in this.pedidos.AsEnumerable() where !_virtual.Contains(row.Field<int>("PEDIDO")) select row;
            pedidos = query.AsDataView().ToTable();



            bindingSourcePedidosSIP.DataSource = pedidos;
            try
            {
                if (!reload)
                {
                    txtPedSIPNo.DataBindings.Add("Text", bindingSourcePedidosSIP, "PEDIDO");
                    txtPedSIPFecha.DataBindings.Add("Text", bindingSourcePedidosSIP, "FECHA");
                    txtPedSIPAgente.DataBindings.Add("Text", bindingSourcePedidosSIP, "AGENTE");
                    txtPedSIPCliente.DataBindings.Add("Text", bindingSourcePedidosSIP, "CLIENTE");
                    txtPedSIPOc.DataBindings.Add("Text", bindingSourcePedidosSIP, "OC");
                    txtPedSIPTerminos.DataBindings.Add("Text", bindingSourcePedidosSIP, "TERMINOS");
                    txtPedSIPRemitido.DataBindings.Add(new Binding("Text", bindingSourcePedidosSIP, "REMITIDO", true));
                    txtPedSIPConsignado.DataBindings.Add(new Binding("Text", bindingSourcePedidosSIP, "CONSIGNADO", true));
                    txtPedSIPObservaciones.DataBindings.Add(new Binding("Text", bindingSourcePedidosSIP, "OBSERVACIONES", true));
                    cmbPedSIPFormaPago.DataBindings.Add(new Binding("SelectedValue", bindingSourcePedidosSIP, "FORMADEPAGOSAT", true));
                    cmbPedSIPUsoCFDI.DataBindings.Add(new Binding("SelectedValue", bindingSourcePedidosSIP, "USO_CFDI", true));
                    cmbPedSIPMetodoPago.DataBindings.Add(new Binding("SelectedValue", bindingSourcePedidosSIP, "METODODEPAGO", true));
                    //lblPedSIPEcommerce.DataBindings.Add("Text", bindingSourcePedidosSIP, "TIPO");
                    txtPedSIPCotizacion.DataBindings.Add("Text", bindingSourcePedidosSIP, "NUMERO_COTIZACION");
                }
            }
            catch { }


            bindingNavigatorPedidosSIP.BindingSource = bindingSourcePedidosSIP;
            PedidosSIPEvaluaHabilitarControlesCambios();
            txtPedSipAValidar.Add(txtPedSIPRemitido);
            cmbPedSipAValidar.Add(cmbPedSIPFormaPago);
            cmbPedSipAValidar.Add(cmbPedSIPUsoCFDI);
            cmbPedSipAValidar.Add(cmbPedSIPMetodoPago);
        }
        private void PedidosVirtualesSIPLlenaDatos(string id)
        {
            PED_MSTR pedidosBl = new PED_MSTR();
            pedidosVirtuales = pedidosBl.CosultarPedidosVirtualesCliente(id);
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

        private string PedidosSIPGuardar()
        {
            string resultado = "";
            int pedido = 0;
            PED_MSTR guardaPedido = new PED_MSTR();
            guardaPedido.AGENTE = Agente;
            guardaPedido.FECHA = Convert.ToDateTime(txtPedSIPFecha.Text);
            guardaPedido.CLIENTE = txtPedSIPCliente.Text;
            guardaPedido.OC = txtPedSIPOc.Text;
            guardaPedido.TERMINOS = Termino;
            guardaPedido.REMITIDO = txtPedSIPRemitido.Text;
            guardaPedido.CONSIGNADO = txtPedSIPConsignado.Text;
            guardaPedido.OBSERVACIONES = txtPedSIPObservaciones.Text;
            //guardaPedido.TIPO = chkPedSIPEcommerce.Checked ? "EC" : "OV";
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
                pedidos.Rows[pedidos.Rows.Count - 1]["PEDIDO"] = pedido;
            }
            else
            {
                resultado = guardaPedido.Error.InnerException.ToString();
            }
            return resultado;
        }

        private void btnPedSIPTerminosActualiza_Click(object sender, EventArgs e)
        {
            txtPedSIPTerminos.Text = Termino;
            string resultado = "";
            resultado = PedidosSIPModificar();
            if (resultado != "")
            {
                MessageBox.Show("Se presentó el siguiente error \\n" + resultado, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void toolStripBtnPedidosSIPNuevo_Click(object sender, EventArgs e)
        {
            pedidoNuevo = true;

            DataRow nuevo_pedido = pedidos.NewRow();
            pedidos.Rows.Add(nuevo_pedido);
            nuevo_pedido["FECHA"] = DateTime.Now;
            nuevo_pedido["AGENTE"] = Agente;
            nuevo_pedido["CLIENTE"] = clave_cliente;
            nuevo_pedido["OC"] = "";
            nuevo_pedido["TERMINOS"] = Termino;
            nuevo_pedido["ESTATUS"] = "P";
            nuevo_pedido["FORMADEPAGOSAT"] = "03";
            nuevo_pedido["METODODEPAGO"] = "PUE";
            nuevo_pedido["USO_CFDI"] = "G01";
            bindingSourcePedidosSIP.MoveLast();

            PedidosSIPHabilitaControlesNavegacion(false);
            PedidosSIPHabilitaControlesCambios(true);
            btnPedSIPAgregarPartidas.Enabled = false;
            btnPedSIPElimModPartidas.Enabled = false;
            btnPedSIPTerminosActualiza.Enabled = false;
            btnPedSIPImprimir.Enabled = false;
            btnPedSIPDocumentos.Enabled = false;
            PedidosSIPHabilitaControlesNavegacionParaPrimerRegistro(true);
            txtPedSIPOc.Focus();
            lblPedSIPDivision.Visible = false;
        }
        private void toolStripBtnPedidosSIPGuardar_Click(object sender, EventArgs e)
        {
            if (!GeneralesExistenDatosVacios(txtPedSipAValidar) && !GeneralesExistenDatosVacios(cmbPedSipAValidar))
            {
                string resultado = "";
                if (pedidoNuevo)
                {
                    resultado = PedidosSIPGuardar();
                }
                else
                {
                    resultado = PedidosSIPModificar();
                }

                if (resultado == "")
                {
                    PedidosSIPHabilitaControlesNavegacion(true);
                    PedidosSIPHabilitaControlesCambios(true);
                    pedidoNuevo = false;
                }
                else
                {
                    MessageBox.Show("Se presentó el siguiente error \\n" + resultado, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
        private void toolStripBtnPedidosSIPEliminar_Click(object sender, EventArgs e)
        {
            if (!pedidoNuevo)
            {
                PED_DET detalle_pedido = new PED_DET();
                int idPedido = Convert.ToInt32(txtPedSIPNo.Text);
                bool contiene_partidas = detalle_pedido.ContieneRegistrosPedido(idPedido);
                if (!contiene_partidas)
                {
                    if (MessageBox.Show("Está seguro que desea eliminar el pedido?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        PED_MSTR elimina_pedido = new PED_MSTR();
                        elimina_pedido.PEDIDO = idPedido;
                        elimina_pedido.Borrar(elimina_pedido, Enumerados.TipoBorrado.Fisico);
                        bindingSourcePedidosSIP.RemoveCurrent();
                        MessageBox.Show("El pedido ha sido eliminado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("No se puede eliminar el pedido debido a que ya tiene partidas cargadas.\n\n Debe eliminar las partidas desde el botón (Eliminar o Modificar Partidas) y luego podrá eliminar el pedido", "Información", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                bindingSourcePedidosSIP.RemoveCurrent();
                PedidosSIPHabilitaControlesNavegacion(true);
                PedidosSIPEvaluaHabilitarControlesCambios();
                pedidoNuevo = false;
            }
            if (bindingSourcePedidosSIP.Count == 0)
            {
                PedidosSIPHabilitaControlesNavegacionParaPrimerRegistro(false);
            }

        }

        private void btnPedSIPElimModPartidas_Click(object sender, EventArgs e)
        {
            frmEliminarPartidas frmEliminarPartidas = new frmEliminarPartidas(clave_cliente, Convert.ToInt32(txtPedSIPNo.Text), Enumerados.TipoPedido.Pedido);
            frmEliminarPartidas.Show();
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
                //frmAgregarPartidas = new AddPartidasPedi2(txtPedSIPCliente.Text, Convert.ToInt32(txtPedSIPNo.Text), chkPedSIPEcommerce.Checked ? Enumerados.TipoPedido.PedidoEC : Enumerados.TipoPedido.Pedido, nombre_vendedor, this.lblPedSIPDivision.Visible);
                frmAgregarPartidas = new AddPartidasPedi2(txtPedSIPCliente.Text, Convert.ToInt32(txtPedSIPNo.Text), Enumerados.TipoPedido.Pedido, nombre_vendedor, this.lblPedSIPDivision.Visible);
                frmAgregarPartidas.ShowDialog();
                if (frmAgregarPartidas.pedidoEstatusModificado) { PedidosVirtualesSIPLlenaDatos(clave_cliente); PedidosSIPLlenaDatos(clave_cliente, true); }
            }
        }

        private void bindingNavigatorMovePreviousItem_Click(object sender, EventArgs e)
        {
            PedidosSIPEvaluaHabilitarControlesCambios();
        }

        private void bindingNavigatorMoveFirstItem_Click(object sender, EventArgs e)
        {
            PedidosSIPEvaluaHabilitarControlesCambios();
        }

        private void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e)
        {
            PedidosSIPEvaluaHabilitarControlesCambios();
        }

        private void bindingNavigatorMoveLastItem_Click(object sender, EventArgs e)
        {
            PedidosSIPEvaluaHabilitarControlesCambios();
        }
        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
        }
        private void btnPedSIPImprimir_Click(object sender, EventArgs e)
        {
            if (pedidos.Rows[bindingSourcePedidosSIP.Position]["PEDIDO"] != DBNull.Value)
            {
                int idPedido = Convert.ToInt32(pedidos.Rows[bindingSourcePedidosSIP.Position]["PEDIDO"].ToString());
                string status = pedidos.Rows[bindingSourcePedidosSIP.Position]["ESTATUS"].ToString();
                //frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, clave_cliente, 0, idPedido, chkPedSIPEcommerce.Checked ? Enumerados.TipoPedido.PedidoEC : Enumerados.TipoPedido.Pedido, this.frmOrigen, false, false, lblPedSIPDivision.Visible);
                frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos, clave_cliente, 0, idPedido, Enumerados.TipoPedido.Pedido, this.frmOrigen, false, false, lblPedSIPDivision.Visible);
                if (status == "I")
                {
                    frmReportes.enVentas = true;
                }
                //frmReportes.Show();
                frmReportes.ShowDialog();
                if (frmReportes.pedidoEstatusModificado) { PedidosVirtualesSIPLlenaDatos(clave_cliente); PedidosSIPLlenaDatos(clave_cliente, true); }
            }
        }

        #endregion "Código para Pedidos SIP"

        #region Logotipos

        private void LogotiposLlenaDatos()
        {
            IMAGENES imagenes = new IMAGENES();
            logotipos = imagenes.ConsultarPorCliente(clave_cliente);
            logotiposOri = logotipos.Copy();
            bindingSourceLogotipos.DataSource = logotipos;
            txtLogoCliente.DataBindings.Add("Text", bindingSourceLogotipos, "COD_CLIENTE");
            txtLogoCodCatalogo.DataBindings.Add("Text", bindingSourceLogotipos, "COD_CATALOGO");
            txtLogoColor1.DataBindings.Add("Text", bindingSourceLogotipos, "COLOR_1");
            txtLogoColor2.DataBindings.Add("Text", bindingSourceLogotipos, "COLOR_2");
            txtLogoColor3.DataBindings.Add("Text", bindingSourceLogotipos, "COLOR_3");
            txtLogoColor4.DataBindings.Add("Text", bindingSourceLogotipos, "COLOR_4");
            txtLogoColor5.DataBindings.Add("Text", bindingSourceLogotipos, "COLOR_5");
            txtLogoColor6.DataBindings.Add("Text", bindingSourceLogotipos, "COLOR_6");
            txtLogoComentarios.DataBindings.Add("Text", bindingSourceLogotipos, "COMENTARIOS");
            txtLogoPuntadas.DataBindings.Add("Text", bindingSourceLogotipos, "PUNTADAS");
            txtLogoNomArch.DataBindings.Add("Text", bindingSourceLogotipos, "NAME");
            LogotiposCalculaPosicionRegistros();
            if (bindingSourceLogotipos.Count == 0)
            {
                LogotiposHabilitaControles(false);
                btnLogoNuevReg.Enabled = true;
                btnLogoSalvar.Enabled = false;
                btnLogoBorrar.Enabled = false;
            }
            txtLogosAValidar.Add(txtLogoComentarios);
            txtLogosAValidar.Add(txtLogoCodCatalogo);

        }
        private string LogotiposDevuelveNombreImagen()
        {
            string nombreImagen = "";
            if (bindingSourceLogotipos.Count > 0)
            {
                DataRowView row = (DataRowView)bindingSourceLogotipos.Current;
                string id = row["ID"].ToString();
                nombreImagen = "CL" + clave_cliente + "-ID" + id + ".jpg";
            }
            return nombreImagen;
        }
        private string LogotiposDevuelveNombreImagen(string idArchivo)
        {
            string nombreImagen = "";
            if (idArchivo != "")
            {
                nombreImagen = "CL" + clave_cliente + "-ID" + idArchivo + ".jpg";
            }
            return nombreImagen;
        }
        private void LogotiposHabilitaControles(bool Habilita)
        {
            lstViewLogotipos.Enabled = Habilita;
            btnLogoCambImg.Enabled = Habilita;
            btnLogoImprimir.Enabled = Habilita;
            btnLogoNuevReg.Enabled = Habilita;
        }

        private int LogotiposGuardaInfo()
        {
            int id = 0;
            IMAGENES guarda_inf_imagen = new IMAGENES();
            guarda_inf_imagen.COD_CATALOGO = txtLogoCodCatalogo.Text;
            guarda_inf_imagen.COD_CLIENTE = txtLogoCliente.Text;
            guarda_inf_imagen.COLOR_1 = txtLogoColor1.Text;
            guarda_inf_imagen.COLOR_2 = txtLogoColor2.Text;
            guarda_inf_imagen.COLOR_3 = txtLogoColor3.Text;
            guarda_inf_imagen.COLOR_4 = txtLogoColor4.Text;
            guarda_inf_imagen.COLOR_5 = txtLogoColor5.Text;
            guarda_inf_imagen.COLOR_6 = txtLogoColor6.Text;
            guarda_inf_imagen.COMENTARIOS = txtLogoComentarios.Text;
            guarda_inf_imagen.NAME = txtLogoNomArch.Text;
            int puntadas = 0;
            int.TryParse(txtLogoPuntadas.Text, out puntadas);
            guarda_inf_imagen.PUNTADAS = puntadas;
            guarda_inf_imagen.Crear(guarda_inf_imagen, ref id);
            return id;
        }
        private void LogotiposModificaInfo()
        {
            DataRow row = logotipos.Rows[bindingSourceLogotipos.Position];
            int id = Convert.ToInt32(row["id"].ToString());
            IMAGENES modifica_inf_imagen = new IMAGENES();
            modifica_inf_imagen.ID = id;
            modifica_inf_imagen.COD_CATALOGO = txtLogoCodCatalogo.Text;
            modifica_inf_imagen.COD_CLIENTE = txtLogoCliente.Text;
            modifica_inf_imagen.COLOR_1 = txtLogoColor1.Text;
            modifica_inf_imagen.COLOR_2 = txtLogoColor2.Text;
            modifica_inf_imagen.COLOR_3 = txtLogoColor3.Text;
            modifica_inf_imagen.COLOR_4 = txtLogoColor4.Text;
            modifica_inf_imagen.COLOR_5 = txtLogoColor5.Text;
            modifica_inf_imagen.COLOR_6 = txtLogoColor6.Text;
            modifica_inf_imagen.COMENTARIOS = txtLogoComentarios.Text;
            modifica_inf_imagen.NAME = txtLogoNomArch.Text;
            int puntadas = 0;
            int.TryParse(txtLogoPuntadas.Text, out puntadas);
            modifica_inf_imagen.PUNTADAS = puntadas;
            modifica_inf_imagen.Modificar(modifica_inf_imagen);
        }
        private string LogotiposDevuelveRutaImagenSeleccionada()
        {
            string rutaArchivo = "";
            OpenFileDialog archivo = new OpenFileDialog();
            archivo.InitialDirectory = @"c:\";
            archivo.Filter = "Archivos JPG (*.jpg)|*.jpg";
            if (archivo.ShowDialog() == DialogResult.OK)
            {
                rutaArchivo = archivo.FileName;
            }
            return rutaArchivo;
        }

        private Image LogotiposDevuelveImagen(string ruta, string archivo)
        {
            // string ImagenADesplegar = Directory.GetCurrentDirectory().ToString() + @" \Imagenes\sin_imagen.jpg";
            string ImagenADesplegar = "";
            Image imgADevolver = Properties.Resources.sin_imagen;
            try
            {
                if (Directory.Exists(ruta))
                {
                    if (File.Exists(ruta + archivo))
                    {
                        ImagenADesplegar = ruta + archivo;
                        imgADevolver = null;
                        using (Stream img = File.OpenRead(ImagenADesplegar))
                        {
                            imgADevolver = Image.FromStream(img);
                        }
                    }

                }
            }
            catch { }
            return imgADevolver;
        }

        private void LogotiposEliminaImagen(string ruta)
        {
            if (File.Exists(ruta))
            {
                /*
                FileSecurity fileSecurity = new System.Security.AccessControl.FileSecurity();
                FileInfo fInfo = new FileInfo(rutaDestino + archivoDestino);
                fileSecurity.RemoveAccessRuleAll(new System.Security.AccessControl.FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), 
                                         FileSystemRights.Delete,
                                         InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit,
                                         PropagationFlags.NoPropagateInherit, AccessControlType.Allow));                             
                fInfo.SetAccessControl(fileSecurity);
               */

                File.Delete(ruta);
            }
        }

        private void LogotiposCopiaImagenACarpeta(string rutaOrigen, string rutaDestino, string archivoDestino)
        {
            if (rutaOrigen != "")
            {
                if (!Directory.Exists(rutaDestino))
                {
                    Directory.CreateDirectory(rutaDestino);
                }
                else
                {
                    LogotiposEliminaImagen(rutaDestino + archivoDestino);
                }
                File.Copy(rutaOrigen, rutaDestino + archivoDestino);
            }
        }

        private void LogotiposCalculaPosicionRegistros()
        {

            lblLogoTotReg.Text = string.Format("{0} de {1}", (bindingSourceLogotipos.Position + 1).ToString(), bindingSourceLogotipos.Count.ToString());

            if (lstViewLogotipos.Items.Count > 0 && bindingSourceLogotipos.Position <= lstViewLogotipos.Items.Count - 1)
            {
                int pos = bindingSourceLogotipos.Position;
                lstViewLogotipos.Items[pos].Focused = true;
                lstViewLogotipos.Items[pos].Selected = true;
            }

        }
        private void LogotiposCargaListViewConImagenes(int idASeleccionar)
        {
            imgLstLogotipos.Images.Clear();
            lstViewLogotipos.Clear();
            lstViewLogotipos.Focus();

            foreach (DataRow row in logotipos.Rows)
            {
                string nombreArchivo = LogotiposDevuelveNombreImagen(row["id"].ToString());
                Image img = LogotiposDevuelveImagen(Globales.rutaImagenes, nombreArchivo);
                try
                {
                    try
                    {
                        Graphics g = Graphics.FromImage(img);
                        g.DrawRectangle(new Pen(SystemBrushes.ActiveBorder, 2), new Rectangle(0, 0, img.Width - 1, img.Height - 1));
                    }
                    catch { }

                    imgLstLogotipos.Images.Add(row["id"].ToString(), img);
                }
                catch (Exception Ex)
                {
                    //MessageBox.Show(
                    //    string.Format("El archivo: \"{0}\" parece estar dañado o es demasiado grande", nombreArchivo),
                    //    Ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //Image imgDefault = LogotiposDevuelveImagen(Directory.GetCurrentDirectory().ToString() + @" \Imagenes\", "archivo_danado.png");
                    Image imgDefault = SIP.Properties.Resources.archivo_danado;
                    imgLstLogotipos.Images.Add(imgDefault);
                }
            }
            lstViewLogotipos.View = View.LargeIcon;
            //imgLstLogotipos.ImageSize = new Size(120, 100);
            //imgLstLogotipos.ImageSize = new Size(120, 100);
            imgLstLogotipos.ImageSize = new Size(120, 120);

            lstViewLogotipos.LargeImageList = imgLstLogotipos;

            string codCatalogo = "";
            for (int j = 0; j < imgLstLogotipos.Images.Count; j++)
            {

                ; if (imgLstLogotipos.Images.Keys[j] != "")
                {
                    var cod_logo = (from c in logotipos.AsEnumerable()
                                    where (int)c["ID"] == int.Parse(imgLstLogotipos.Images.Keys[j])
                                    select new { COD_CATALOGO = c["COD_CATALOGO"].ToString() }).FirstOrDefault();

                    codCatalogo = cod_logo.COD_CATALOGO.Trim();

                }
                else
                {
                    codCatalogo = "Archivo dañado!";

                }
                ListViewItem item = new ListViewItem();
                item.Font = new System.Drawing.Font(FontFamily.GenericSansSerif, 14f, FontStyle.Bold);
                item.ImageKey = j.ToString();
                //item.Text = (j + 1).ToString();
                item.Text = codCatalogo;
                item.ImageIndex = j;
                lstViewLogotipos.Items.Add(item);
                if (j == idASeleccionar)
                {
                    lstViewLogotipos.Items[j].Focused = true;
                    lstViewLogotipos.Items[j].Selected = true;
                    lstViewLogotipos.Items[j].EnsureVisible();
                }
            }
        }

        private void btnLogoPrevio_Click(object sender, EventArgs e)
        {
        }
        private void lstViewLogotipos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!logotipoNuevo)
            {

                if (lstViewLogotipos.SelectedItems.Count > 0)
                {

                    ListViewItem lv = lstViewLogotipos.SelectedItems[0];

                    CURR_COD_CLIENTE = lv.Text;
                    PREV_SELECTED_NDX = CURR_SELECTED_NDX;

                    CURR_SELECTED_NDX = lv.Index;
                    bindingSourceLogotipos.Position = lv.ImageIndex;

                    LogotiposCalculaPosicionRegistros();


                }
                else
                {
                    CURR_COD_CLIENTE = "";
                }

            }

        }

        private void lstViewLogotipos_DoubleClick(object sender, EventArgs e)
        {

            DataRow row = logotipos.Rows[bindingSourceLogotipos.Position];
            string archivo = Globales.rutaImagenes + LogotiposDevuelveNombreImagen(row["id"].ToString());
            if (File.Exists(archivo))
            {
                System.Diagnostics.Process.Start(archivo);
            }
            else
            {
                MessageBox.Show("No hay imágen a desplegar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //frmImagen frmImagen = new frmImagen(LogotiposDevuelveImagen(Directory.GetCurrentDirectory().ToString() + @" \CliesLogos\", LogotiposDevuelveNombreImagen(row["id"].ToString())));
            // frmImagen.ShowDialog();                
        }

        private void btnLogoNuevReg_Click(object sender, EventArgs e)
        {
            logotipoNuevo = true;
            //Random rnd = new Random();
            DataRow rowLog = logotipos.NewRow();

            logotipos.Rows.Add(rowLog);
            rowLog["COD_CLIENTE"] = clave_cliente;
            //rowLog["COD_CATALOGO"] = "Rnd-" + Convert.ToString(Convert.ToInt32(255 * rnd.NextDouble() + 1));
            rowLog["COD_CATALOGO"] = clave_cliente + "-";

            bindingSourceLogotipos.MoveLast();
            LogotiposHabilitaControles(false);
            btnLogoSalvar.Enabled = true;
            btnLogoBorrar.Enabled = true;
            btnLogoBorrar.Text = "Cancelar";
            LogotiposCalculaPosicionRegistros();
            txtLogoCodCatalogo.SelectAllOnFocus = false;
            txtLogoCodCatalogo.SelectionStart = txtLogoCodCatalogo.Text.Length;
            txtLogoCodCatalogo.Focus();
            txtLogoCodCatalogo.SelectAllOnFocus = true;
        }

        private void btnLogoCambImg_Click(object sender, EventArgs e)
        {
            if (lstViewLogotipos.SelectedItems.Count == 0)
            {
                MessageBox.Show("Seleccione una imagen", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string rutaOrigen = LogotiposDevuelveRutaImagenSeleccionada();
            if (rutaOrigen != string.Empty)
            {
                DataRowView row = (DataRowView)bindingSourceLogotipos.Current;
                string id = row["ID"].ToString();
                string nombreArchivo = "CL" + clave_cliente + "-ID" + id + ".jpg";
                //string rutaDestino = Directory.GetCurrentDirectory().ToString() + @" \CliesLogos\";                
                LogotiposCopiaImagenACarpeta(rutaOrigen, Globales.rutaImagenes, nombreArchivo);
                LogotiposCargaListViewConImagenes(bindingSourceLogotipos.Position);
                MessageBox.Show("Imagen modificada", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLogoCodCatalogo.Focus();
            }
        }



        private void btnLogoSalvar_Click(object sender, EventArgs e)
        {

            string respuestaErrCodCat = "";
            if (!GeneralesExistenDatosVacios(txtLogosAValidar))
            {

                respuestaErrCodCat = LogosVAlidaCodigoCatalogo();
                if (respuestaErrCodCat == "")
                {
                    if (logotipoNuevo)
                    {
                        string rutaOrigen = LogotiposDevuelveRutaImagenSeleccionada();



                        if (rutaOrigen != string.Empty)
                        {

                            string id = LogotiposGuardaInfo().ToString();
                            string nombreArchivo = "CL" + clave_cliente + "-ID" + id + ".jpg";
                            //string rutaDestino = Directory.GetCurrentDirectory().ToString() + @" \CliesLogos\";
                            LogotiposCopiaImagenACarpeta(rutaOrigen, Globales.rutaImagenes, nombreArchivo);
                            DataRow row = logotipos.Rows[bindingSourceLogotipos.Position];
                            row["id"] = id;
                            LogotiposCargaListViewConImagenes(bindingSourceLogotipos.Position);
                            LogotiposHabilitaControles(true);
                            logotipoNuevo = false;

                            logotiposOri.Rows.Add(row.ItemArray);
                            LogotiposCalculaPosicionRegistros();
                            btnLogoBorrar.Text = "Borrar";
                            MessageBox.Show("Nuevo logotipo agregado", "", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }

                    }
                    else
                    {

                        if (bindingSourceLogotipos.Count > 0)
                        {
                            if (lstViewLogotipos.SelectedItems.Count == 0)
                            {
                                MessageBox.Show("Seleccione una imagen", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            string msg = string.Format("¿Confirma modificar el Logotipo: \"{0}\" ?", lstViewLogotipos.SelectedItems[0].Text);
                            DialogResult respMoif = MessageBox.Show(msg, "Confirme", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question);
                            if (respMoif == System.Windows.Forms.DialogResult.Yes)
                            {
                                LogotiposModificaInfo();
                                LogotiposCalculaPosicionRegistros();
                                MessageBox.Show("Logotipo modificado", "", MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show(respuestaErrCodCat, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtLogoCodCatalogo.Focus();
                }
            }

        }

        private void btnLogoBorrar_Click(object sender, EventArgs e)
        {



            if (!logotipoNuevo)
            {
                if (lstViewLogotipos.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Seleccione una imagen a borrar", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string msg = string.Format("¿Realmente desea eliminar el Logotipo: \"{0}\" ?", lstViewLogotipos.SelectedItems[0].Text);

                if (MessageBox.Show(msg, "Confirme", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {

                    IMAGENES img_a_borrar = new IMAGENES();
                    DataRowView registroActual = (DataRowView)bindingSourceLogotipos.Current;
                    int id = Convert.ToInt32(registroActual["ID"].ToString());
                    img_a_borrar.ID = id;
                    img_a_borrar.COD_CATALOGO = registroActual["COD_CATALOGO"].ToString();
                    img_a_borrar.Borrar(img_a_borrar, Enumerados.TipoBorrado.Fisico);
                    //bindingSourceLogotipos.RemoveCurrent();
                    logotipos.Rows.RemoveAt(bindingSourceLogotipos.Position);
                    try
                    {
                        var query = (from r in logotiposOri.AsEnumerable() where (int)r["ID"] == id select r).FirstOrDefault();
                        logotiposOri.Rows.Remove(query);
                    }
                    catch { }
                    string nombreArchivo = "CL" + clave_cliente + "-ID" + id + ".jpg";
                    string ruta = Globales.rutaImagenes;
                    LogotiposEliminaImagen(ruta + nombreArchivo);
                    LogotiposCargaListViewConImagenes(bindingSourceLogotipos.Position);
                    MessageBox.Show("El registro ha sido eliminado exitosamente.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    logotipoNuevo = false;
                }
            }
            else
            {

                logotipos.Rows.RemoveAt(bindingSourceLogotipos.Position);
                logotipoNuevo = false;
                //bindingSourceLogotipos.RemoveCurrent();                    
            }
            if (logotipos.Rows.Count > 0)
            {
                LogotiposHabilitaControles(true);
            }
            else
            {
                LogotiposCargaListViewConImagenes(0);
                LogotiposHabilitaControles(false);
                btnLogoNuevReg.Enabled = true;
                btnLogoSalvar.Enabled = false;
                btnLogoBorrar.Enabled = false;
            }
            btnLogoBorrar.Text = "Borrar";
            LogotiposCalculaPosicionRegistros();
        }
        private void btnLogoImprimir_Click(object sender, EventArgs e)
        {
            if (lstViewLogotipos.SelectedItems.Count == 0)
            {
                MessageBox.Show("Seleccione una imagen", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataRowView registroActual = (DataRowView)bindingSourceLogotipos.Current;
            int ID = Convert.ToInt32(registroActual["ID"].ToString());
            Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Logotipos, clave_cliente, ID, 0, Enumerados.TipoPedido.NoAplica);
            frmReportes.Show();
        }


        #endregion "Código correspondiente a logotipos"

        #region OrdenTrabajoSIP
        private void OrdenTrabSIPHabilitaControlesNavegacionParaPrimerRegistro(bool Habilita)
        {
            toolStripBtnOrdenTrabSIPEliminar.Enabled = Habilita;
            toolStripBtnOrdenTrabSIPGuardar.Enabled = Habilita;
        }
        private void OrdenTrabSIPHabilitaControlesNavegacion(bool Habilita)
        {
            bindingNavigatorMoveFirstItem1.Enabled = Habilita;
            bindingNavigatorMovePreviousItem1.Enabled = Habilita;
            bindingNavigatorMoveNextItem1.Enabled = Habilita;
            bindingNavigatorMoveLastItem1.Enabled = Habilita;
            toolStripBtnOrdenTrabSIPNuevo.Enabled = Habilita;
            bindingNavigatorPositionItem1.Enabled = Habilita;
            btnOrdenTrabSIPAgregarPartidas.Enabled = Habilita;
            btnOrdenTrabSIPEliminarPartidas.Enabled = Habilita;
        }
        private void OrdenTrabEvaluaHabilitarControlesCambios()
        {
            if (bindingSourceOrdenTrabSIP.Count > 0)
            {
                DataRowView row = (DataRowView)bindingSourceOrdenTrabSIP.Current;
                string status = row["ESTATUS"].ToString();
                if (status == "I")
                {
                    OrdenTrabHabilitaControlesCambios(false);
                }
                else
                {
                    OrdenTrabHabilitaControlesCambios(true);
                }
            }
            else
            {
                OrdenTrabSIPHabilitaControlesNavegacionParaPrimerRegistro(false);
            }
        }

        private void OrdenTrabHabilitaControlesCambios(bool Habilita)
        {
            lblOrdTrabYaImpreso.Visible = !Habilita;
            btnOrdenTrabSIPAgregarPartidas.Enabled = Habilita;
            btnOrdenTrabSIPEliminarPartidas.Enabled = Habilita;
        }
        private void OrdenTrabSIPLlenaDatos(string id)
        {
            PED_MSTR ordenTrabBl = new PED_MSTR();
            OrdenTrabajo = ordenTrabBl.ConsultarOrdenTrabajo(id);
            bindingSourceOrdenTrabSIP.DataSource = OrdenTrabajo;
            txtOrdenTrabSIPAgente.DataBindings.Add("Text", bindingSourceOrdenTrabSIP, "AGENTE");
            txtOrdenTrabSIPOT.DataBindings.Add("Text", bindingSourceOrdenTrabSIP, "PEDIDO");
            txtOrdenTrabSIPCliente.DataBindings.Add("Text", bindingSourceOrdenTrabSIP, "CLIENTE");
            txtOrdenTrabSIPFecha.DataBindings.Add("Text", bindingSourceOrdenTrabSIP, "FECHA");

            bindingNavigatorOrdenTrabSIP.BindingSource = bindingSourceOrdenTrabSIP;
            OrdenTrabEvaluaHabilitarControlesCambios();
        }

        private void OrdenTrabSIPGuardar()
        {
            int pedido = 0;
            PED_MSTR guardaPedido = new PED_MSTR();
            guardaPedido.AGENTE = Agente;
            guardaPedido.FECHA = DateTime.Now;
            guardaPedido.CLIENTE = clave_cliente;
            guardaPedido.ESTATUS = "P";
            guardaPedido.TIPO = "OT";
            guardaPedido.TERMINOS = Termino;
            guardaPedido.Crear(guardaPedido, ref pedido);

            UPPEDIDOS guardaUppedidos = new UPPEDIDOS();
            guardaUppedidos.PEDIDO = pedido;
            guardaUppedidos.COD_CLIENTE = clave_cliente;
            guardaUppedidos.F_CAPT = DateTime.Now;
            guardaUppedidos.Crear(guardaUppedidos);
            txtOrdenTrabSIPOT.Text = pedido.ToString();
            OrdenTrabajo.Rows[bindingSourceOrdenTrabSIP.Position]["PEDIDO"] = pedido;
        }

        private void toolStripBtnOrdenTrabSIPNuevo_Click(object sender, EventArgs e)
        {
            ordenTrabajoNueva = true;

            DataRow nueva_ot = OrdenTrabajo.NewRow();
            nueva_ot["FECHA"] = DateTime.Now;
            nueva_ot["AGENTE"] = Agente;
            nueva_ot["CLIENTE"] = clave_cliente;
            OrdenTrabajo.Rows.Add(nueva_ot);
            bindingSourceOrdenTrabSIP.MoveLast();

            OrdenTrabSIPHabilitaControlesNavegacion(false);
            OrdenTrabHabilitaControlesCambios(false);
            lblOrdTrabYaImpreso.Visible = false;
            OrdenTrabSIPHabilitaControlesNavegacionParaPrimerRegistro(true);
            toolStripBtnOrdenTrabSIPGuardar_Click(sender, e);
            //btnOrdenTrabSIPAgregarPartidas_Click(sender, e);

        }

        private void toolStripBtnOrdenTrabSIPGuardar_Click(object sender, EventArgs e)
        {
            if (ordenTrabajoNueva)
            {
                OrdenTrabSIPGuardar();
                OrdenTrabSIPHabilitaControlesNavegacion(true);
                OrdenTrabHabilitaControlesCambios(true);
                ordenTrabajoNueva = false;
            }
        }
        private void toolStripBtnOrdenTrabSIPEliminar_Click(object sender, EventArgs e)
        {
            if (!ordenTrabajoNueva)
            {
                PED_DET detalle_pedido = new PED_DET();
                int idPedido = Convert.ToInt32(txtOrdenTrabSIPOT.Text);
                bool contiene_partidas = detalle_pedido.ContieneRegistrosPedido(idPedido);
                if (!contiene_partidas)
                {
                    if (MessageBox.Show("Está seguro que desea eliminar la órden de trabajo?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        PED_MSTR elimina_pedido = new PED_MSTR();
                        elimina_pedido.PEDIDO = idPedido;
                        elimina_pedido.Borrar(elimina_pedido, Enumerados.TipoBorrado.Fisico);
                        //bindingSourceOrdenTrabSIP.RemoveCurrent();
                        OrdenTrabajo.Rows.RemoveAt(bindingSourceOrdenTrabSIP.Position);
                        MessageBox.Show("la órden de trabajo ha sido eliminada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("No se puede eliminar la órden de trabajo debido a que ya tiene partidas cargadas.\n\n Debe eliminar las partidas desde el botón (Eliminar Partidas) y luego podrá eliminar la órden de trabajo", "Información", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                bindingSourceOrdenTrabSIP.RemoveCurrent();
                OrdenTrabSIPHabilitaControlesNavegacion(true);
                OrdenTrabEvaluaHabilitarControlesCambios();
                ordenTrabajoNueva = false;
            }
            if (bindingSourceOrdenTrabSIP.Count == 0)
            {
                OrdenTrabSIPHabilitaControlesNavegacionParaPrimerRegistro(false);
            }
        }

        private void btnOrdenTrabSIPAgregarPartidas_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtOrdenTrabSIPOT.Text))
            {
                AddPartidasPedi frmAgregarPartidasOT = new AddPartidasPedi(clave_cliente,
                    Convert.ToInt32(txtOrdenTrabSIPOT.Text), Enumerados.TipoPedido.OrdenTrabajo, nombre_vendedor);
                frmAgregarPartidasOT.Show();
            }
        }

        private void btnOrdenTrabSIPEliminarPartidas_Click(object sender, EventArgs e)
        {

            frmEliminarPartidas frmEliminarPartidasOT = new frmEliminarPartidas(clave_cliente, Convert.ToInt32(txtOrdenTrabSIPOT.Text), Enumerados.TipoPedido.OrdenTrabajo);
            frmEliminarPartidasOT.Show();
        }
        private void btnOrdenTrabSIPImprimir_Click(object sender, EventArgs e)
        {
            if (OrdenTrabajo.Rows.Count > 0)
            {
                if (OrdenTrabajo.Rows[bindingSourceOrdenTrabSIP.Position]["PEDIDO"].ToString() != "")
                {

                    string status = OrdenTrabajo.Rows[bindingSourceOrdenTrabSIP.Position]["ESTATUS"].ToString();
                    int idPedido = Convert.ToInt32(OrdenTrabajo.Rows[bindingSourceOrdenTrabSIP.Position]["PEDIDO"].ToString());
                    frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.OrdenTrabajo, clave_cliente, 0, idPedido, Enumerados.TipoPedido.OrdenTrabajo, "frmFindClie", false, false);
                    if (status == "I")
                    {
                        frmReportes.enVentas = true;
                    }
                    frmReportes.Show();
                }
            }
        }
        #endregion "Código para órdenes de Trabajo"

        #region EstadoCuenta
        private void EstadoCuentaLlenaDatos(string id)
        {
            DataTable resultado = new DataTable();
            vw_EstadoCuenta estadoCuenta = new vw_EstadoCuenta();
            resultado = estadoCuenta.ConsultarMovimientos(id.Trim());
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            cellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            cellStyle.Format = "c";
            dGViewEstadoCuenta.DataSource = resultado;
            dGViewEstadoCuenta.Columns["Cargo"].DefaultCellStyle = cellStyle;
            dGViewEstadoCuenta.Columns["Abono"].DefaultCellStyle = cellStyle;
        }
        private void EstadoCuentaProcesaArchivoExcel()
        {
            precarga.AsignastatusProceso("Procesando Estado de Cuenta");
            vw_Clientes cliente = new vw_Clientes();
            cliente = cliente.Consultar(clave_cliente);
            DataTable estadoCuenta = new DataTable();
            vw_EstadoCuenta movimientos = new vw_EstadoCuenta();
            estadoCuenta = movimientos.ConsultarMovimientos(clave_cliente);
            string archivoTemporal = Path.GetTempFileName().Replace(".tmp", ".xls");
            precarga.AsignastatusProceso("Creando archivo de excel...");
            movimientos.GeneraArchivoExcel(archivoTemporal, estadoCuenta, cliente);
            //System.Diagnostics.Process.Start(archivoTemporal);
            FuncionalidadesFormularios.MostrarExcel(archivoTemporal);
        }
        private void btnEstadoCuenta_Click(object sender, EventArgs e)
        {
            tipoTarea = Enumerados.TipoTareaProcesamiento.ReporteEstadoCuenta;
            precarga.MostrarEspera();
            backgroundWorker.RunWorkerAsync();
        }
        void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (tipoTarea == Enumerados.TipoTareaProcesamiento.ReporteEstadoCuenta)
            {
                EstadoCuentaProcesaArchivoExcel();
            }

        }
        #endregion "Código para el estado de cuenta"


        #region General

        private int SiguienteConsecutivo(string Proceso)
        {
            int consResult = 0;
            int cons = 0;
            int subStrLen = 0;
            List<int> lstConsecutivos = new List<int>();

            if (clave_cliente.Length == 3)
            {
                subStrLen = 6;
            }
            else
            {
                subStrLen = 5;
            }

            var queryProceso = from consecutivo in logotiposOri.AsEnumerable() where consecutivo["COD_CATALOGO"].ToString().ToUpper().Contains("RND-") == false && consecutivo["COD_CATALOGO"].ToString().ToUpper().Substring(consecutivo["COD_CATALOGO"].ToString().IndexOf("-") + 1, consecutivo["COD_CATALOGO"].ToString().Length - consecutivo["COD_CATALOGO"].ToString().IndexOf("-", subStrLen) - 1) == Proceso select consecutivo;
            foreach (DataRow row in queryProceso)
            {
                cons = int.Parse(row["COD_CATALOGO"].ToString().Substring(row["COD_CATALOGO"].ToString().Length - 2, 2));
                lstConsecutivos.Add(cons);
            }
            if (lstConsecutivos.Count > 0)
            {
                lstConsecutivos.Sort();
                consResult = lstConsecutivos.Last() + 1;
            }
            else
            {
                consResult = 1;
            }
            return consResult;
        }
        private string LogosVAlidaCodigoCatalogo()
        {
            string respuestaError = "";
            string[] separador = { "-" };
            string[] elementos = txtLogoCodCatalogo.Text.Split(separador, StringSplitOptions.RemoveEmptyEntries);

            if (elementos.Count() == 3)
            {

                string proceso = elementos[1].ToUpper();
                int consecutivo = 0;

                int.TryParse(elementos[2].Trim(), out consecutivo);

                int siguienteConsecutivo = SiguienteConsecutivo(proceso);


                proceso = elementos[1].ToUpper();

                /*
                if (logotipoNuevo)
                {
                    //consecutivo escrito vs siguiente consecutivo según Proceso:
                        


                        
                    if (consecutivo != siguienteConsecutivo)
                    {
                        respuestaError =
                            "El número consecutivo que ha escrito es incorrecto, el siguiente consecutivo válido es: " +
                            siguienteConsecutivo.ToString("00");
                    }
                         
                    respuestaError = "";
                }
                else
                {*/
                if (txtLogoCodCatalogo.Text != CURR_COD_CLIENTE) // si modificó el código de catálogo...
                {
                    //reviso que el códifgo escrito no exista
                    var query = from cod in logotiposOri.AsEnumerable()
                                where
                                    cod["COD_CATALOGO"].ToString().Trim() == txtLogoCodCatalogo.Text &&
                                    cod["COD_CATALOGO"].ToString().Trim() != CURR_COD_CLIENTE
                                select cod;

                    if (query.Any())
                    {
                        foreach (DataRow dataRow in query)
                        {
                            System.Diagnostics.Debug.WriteLine(dataRow["COD_CATALOGO"]);
                        }
                        respuestaError = "El Código de Catálogo escrito ya pertenece a otro logotipo, verifique";
                    }
                    /*else
                    {
                        if (consecutivo != siguienteConsecutivo)
                        {
                            respuestaError =
                                "El número consecutivo que ha escrito es incorrecto, el siguiente consecutivo válido es: " +
                                siguienteConsecutivo.ToString("00");
                        }
                    }*/
                }
                //}




            }
            else
            {
                respuestaError = "El código del catálogo no contiene el formato correcto.\n Formato correcto: cliente-procesos-consecutivo imágen.";
            }

            return respuestaError;
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

        public frmFindClie(string idCliente)
        {
            InitializeComponent();
            precarga = new Precarga(this);
            tmrCargaDatos.Tick += tmrCargaDatos_Tick;
            tmrCargaDatos.Interval = 150;
            id_Cliente = idCliente;
            backgroundWorker.DoWork += backgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
            bgwInfoCliente.DoWork += bgwInfoCliente_DoWork;
            bgwInfoCliente.RunWorkerCompleted += bgwInfoCliente_RunWorkerCompleted;
        }

        public frmFindClie(string idCliente, string _frmOrigen, string _tab = "")
        {
            InitializeComponent();
            precarga = new Precarga(this);
            tmrCargaDatos.Tick += tmrCargaDatos_Tick;
            tmrCargaDatos.Interval = 150;
            id_Cliente = idCliente;
            frmOrigen = _frmOrigen;
            backgroundWorker.DoWork += backgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
            bgwInfoCliente.DoWork += bgwInfoCliente_DoWork;
            bgwInfoCliente.RunWorkerCompleted += bgwInfoCliente_RunWorkerCompleted;
            tab = _tab;
        }

        void tmrCargaDatos_Tick(object sender, EventArgs e)
        {
            tmrCargaDatos.Enabled = false;
            precarga.MostrarEspera();
            bgwInfoCliente.RunWorkerAsync(id_Cliente);
            tmrCargaDatos.Enabled = false;


        }

        void bgwInfoCliente_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            precarga.RemoverEspera();
            if (this.tab != "")
            {
                switch (this.tab)
                {
                    case "EstadoCuenta":
                        this.tabControl1.SelectTab("tabPage2");
                        break;
                    default:
                        this.tabControl1.SelectTab("tabPage1");
                        break;
                }
            }

        }

        void bgwInfoCliente_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Diagnostics.Stopwatch stopW = System.Diagnostics.Stopwatch.StartNew();
            string idCliente = (string)e.Argument;

            clave_cliente = idCliente.Trim();

            System.Diagnostics.Debug.WriteLine("Cargando ClienteLlenaDatos()", stopW.Elapsed.ToString());
            ClienteLlenaDatos(clave_cliente);
            System.Diagnostics.Debug.WriteLine("Cargando EstadoCuentaLlenaDatos()", stopW.Elapsed.ToString());
            EstadoCuentaLlenaDatos(clave_cliente);
            System.Diagnostics.Debug.WriteLine("Cargando PedidosVirtualesSIPLlenaDatos()", stopW.Elapsed.ToString());
            PedidosVirtualesSIPLlenaDatos(clave_cliente);
            System.Diagnostics.Debug.WriteLine("Cargando PedidosSIPLlenaDatos()", stopW.Elapsed.ToString());
            PedidosSIPLlenaDatos(clave_cliente);
            System.Diagnostics.Debug.WriteLine("Cargando LogotiposLlenaDatos()", stopW.Elapsed.ToString());
            LogotiposLlenaDatos();
            System.Diagnostics.Debug.WriteLine("Cargando LogotiposCargaListViewConImagenes()", stopW.Elapsed.ToString());
            LogotiposCargaListViewConImagenes(0);
            System.Diagnostics.Debug.WriteLine("Cargando OrdenTrabSIPLlenaDatos()", stopW.Elapsed.ToString());
            OrdenTrabSIPLlenaDatos(clave_cliente);
            System.Diagnostics.Debug.WriteLine("Cargando SolicitudesEspecialesLlenaDatos()", stopW.Elapsed.ToString());
            SolicitudesLlenaDatos(clave_cliente);
            System.Diagnostics.Debug.WriteLine("Cargando Permisos()", stopW.Elapsed.ToString());
            PermisosDePantalla();
            System.Diagnostics.Debug.WriteLine("Cargando PedidosDATSIPLlenaDatos()", stopW.Elapsed.ToString());
            PedidosDATSIPLlenaDatos(clave_cliente);
            System.Diagnostics.Debug.WriteLine("Cargando PedidosMOSSPLlenaDatos()", stopW.Elapsed.ToString());
            PedidosMOSSIPLlenaDatos(clave_cliente);
            System.Diagnostics.Debug.WriteLine("Cargando PedidosMOSCPLlenaDatos()", stopW.Elapsed.ToString());
            PedidosMOSCPLlenaDatos(clave_cliente);
        }

        private void PermisosDePantalla()
        {

            btnLogoNuevReg.Enabled = ulp_bl.Reportes.PermisosUsuarioEspeciales.TienePermisos(Globales.UsuarioActual.Id, 6, 99);
            btnLogoCambImg.Enabled = ulp_bl.Reportes.PermisosUsuarioEspeciales.TienePermisos(Globales.UsuarioActual.Id, 6, 100);
            btnLogoSalvar.Enabled = ulp_bl.Reportes.PermisosUsuarioEspeciales.TienePermisos(Globales.UsuarioActual.Id, 6, 101);
            btnLogoBorrar.Enabled = ulp_bl.Reportes.PermisosUsuarioEspeciales.TienePermisos(Globales.UsuarioActual.Id, 6, 102);
            btnLogoImprimir.Enabled = ulp_bl.Reportes.PermisosUsuarioEspeciales.TienePermisos(Globales.UsuarioActual.Id, 6, 103);


            //accesaTab1 = ulp_bl.Reportes.PermisosUsuarioEspeciales.TienePermisos(Globales.UsuarioActual.Id, 6, 107);
            accesaTab1 = true;
            accesaTab2 = ulp_bl.Reportes.PermisosUsuarioEspeciales.TienePermisos(Globales.UsuarioActual.Id, 6, 108);
            accesaTab3 = ulp_bl.Reportes.PermisosUsuarioEspeciales.TienePermisos(Globales.UsuarioActual.Id, 6, 109) && this.status == "A";
            accesaTab4 = ulp_bl.Reportes.PermisosUsuarioEspeciales.TienePermisos(Globales.UsuarioActual.Id, 6, 110);
            accesaTab5 = ulp_bl.Reportes.PermisosUsuarioEspeciales.TienePermisos(Globales.UsuarioActual.Id, 6, 111);
            accesaTab6 = ulp_bl.Reportes.PermisosUsuarioEspeciales.TienePermisos(Globales.UsuarioActual.Id, 6, 109) && this.status == "A";
            accesaTab7 = true;
            accesaTab8 = true;
            accesaTab9 = true;

            accesosTabs[0] = accesaTab1;
            accesosTabs[1] = accesaTab2;
            accesosTabs[2] = accesaTab3;
            accesosTabs[3] = accesaTab4;
            accesosTabs[4] = accesaTab5;
            accesosTabs[5] = accesaTab6;
            accesosTabs[6] = accesaTab7;
            accesosTabs[7] = accesaTab8;
            accesosTabs[8] = accesaTab9;

            //if (this.InvokeRequired)
            //{

            //    this.Invoke((MethodInvoker)delegate()
            //    {
            //        //if (!accesaTab1) { tabControl1.TabPages.Remove(tabPage1); }
            //        if (!accesaTab2) { tabControl1.TabPages.Remove(tabPage2); }
            //        if (!accesaTab3) { tabControl1.TabPages.Remove(tabPage3); }
            //        if (!accesaTab4) { tabControl1.TabPages.Remove(tabPage4); }
            //        if (!accesaTab5) { tabControl1.TabPages.Remove(tabPage5); }

            //        if (!accesaTab1 && !accesaTab2 && !accesaTab3 && !accesaTab4 && !accesaTab5)
            //        {
            //            Font font = new System.Drawing.Font(SystemFonts.DefaultFont, FontStyle.Bold);

            //            TabPage tp = new TabPage("SOLICITE PERMISOS");
            //            tp.Controls.Add(new Label()
            //            {
            //                Text =
            //                    "Solicite el acceso a:\n\n-Datos del cliente\n-Estado de cuenta\n-Pedidos en SIP\n-Logotipos\n-Ordenes de trabajo\n\nal área de SISTEMAS",
            //                Font = font,
            //                ForeColor = Color.FromArgb(0, 0, 255),
            //                Location = new Point(32, 32),
            //                AutoSize = true
            //            });

            //            tabControl1.Controls.Add(tp);
            //        }

            //    });
            //}

        }

        void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            precarga.RemoverEspera();
        }
        #endregion "Código General"


        #region DatosGeneralesCiente
        private void ClienteLlenaDatos(string id)
        {

            this.Invoke((MethodInvoker)delegate
            {
                vw_Clientes cliente = new vw_Clientes();
                cliente = cliente.Consultar(id);
                lblClave.Text = cliente.CLAVE.Trim();
                lblCP.Text = cliente.CODIGO;
                lblRazonSoc.Text = cliente.NOMBRE_CLIENTE;
                lblDireccion.Text = cliente.DIRECCION;
                lblTelefono.Text = cliente.TELEFONO;
                lblAtencion.Text = cliente.ATENCION;
                lblRFC.Text = cliente.RFC;
                lblVendedor.Text = cliente.NOMBRE_VENDEDOR;
                nombre_vendedor = cliente.NOMBRE_VENDEDOR;
                lblZona.Text = cliente.TEXTO;
                lblDescuento.Text = cliente.DESCUENTO.ToString();
                lblDiasCred.Text = cliente.DIAS_CRE.ToString();
                Termino = cliente.DIAS_CRE.ToString();
                if (cliente.CVE_VEND != null)
                {
                    Agente = cliente.CVE_VEND.ToString();
                    USUARIOS oUsuario = new USUARIOS();
                    this.clasificacionVendedor = oUsuario.GetClasificacionVendedor(Agente);
                }
                else
                {
                    MessageBox.Show("Este cliente no tiene un Vendedor asignado", "Verifique", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                lblUltComp.Text = cliente.FCH_ULTCOM.ToString();
                lblPzUltAnio.Text = cliente.PZAULTANIO.ToString();
                lblLimiteCred.Text = cliente.LIM_CRED.ToString("C", CultureInfo.CurrentCulture);
                lblCredDisponible.Text = cliente.CRED_DISPO.ToString("C", CultureInfo.CurrentCulture);
                lblSaldo.Text = cliente.SALDO.ToString("C", CultureInfo.CurrentCulture);
                lblObservaciones.Text = cliente.CL8;
                Text = string.Format("CLAVE: {0} - {1}", lblClave.Text, lblRazonSoc.Text);
                this.status = cliente.STATUS;
            });


        }
        #endregion "Código para llenar los datos generales de un cliente"

        #region Pedidos DAT SIP

        private void PedidosDATSIPHabilitaControlesNavegacionParaPrimerRegistro(bool Habilita)
        {
            toolStripBtnPedidosDATSIPGuardar.Enabled = Habilita;
            toolStripBtnPedidosDATSIPEliminar.Enabled = Habilita;
        }
        private void PedidosDATSIPHabilitaControlesNavegacion(bool Habilita)
        {
            bindingNavigatorMoveFirstItemDAT.Enabled = Habilita;
            bindingNavigatorMovePreviousItemDAT.Enabled = Habilita;
            bindingNavigatorMoveNextItemDAT.Enabled = Habilita;
            bindingNavigatorMoveLastItemDAT.Enabled = Habilita;
            toolStripBtnPedidosDATSIPNuevo.Enabled = Habilita;
            bindingNavigatorPositionItemDAT.Enabled = Habilita;
        }
        private void PedidosDATSIPHabilitaControlesCambios(bool Habilita)
        {
            lblPedDATSIPYaImpreso.Visible = !Habilita;
            txtPedDATSIPOc.Enabled = Habilita;
            txtPedDATSIPRemitido.Enabled = Habilita;
            txtPedDATSIPConsignado.Enabled = Habilita;
            txtPedDATSIPObservaciones.Enabled = Habilita;
            if (Globales.UsuarioActual.UsuarioUsuario.Trim().ToUpper() == "MOSTRADOR")
            {
                btnPedDATSIPAgregarPartidas.Enabled = this.clasificacionVendedor == "F";
                btnPedDATSIPElimModPartidas.Enabled = this.clasificacionVendedor == "F";
            }
            else
            {
                btnPedDATSIPAgregarPartidas.Enabled = Habilita && !lblPedDATSIPDivision.Visible;
                btnPedDATSIPElimModPartidas.Enabled = Habilita;
            }
            btnPedDATSIPTerminosActualiza.Enabled = Habilita;
            toolStripBtnPedidosDATSIPGuardar.Enabled = Habilita;
            btnPedDATSIPImprimir.Enabled = Habilita;
            txtPedDATSIPConsignado.Enabled = Habilita;
            btnPedDATSIPDocumentos.Enabled = true;
            HabilitaNuevoPedido();
        }
        private void PedidosDATSIPEvaluaHabilitarControlesCambios()
        {
            if (bindingSourcePedidosDATSIP.Count > 0)
            {
                DataRowView row = (DataRowView)bindingSourcePedidosDATSIP.Current;
                string status = row["ESTATUS"].ToString();
                lblPedDATSIPDivision.Visible = this.pedidosVirtuales.Select("PedidoDivision = " + row["PEDIDO"].ToString()).Count() > 0;
                if (status == "I")
                {
                    PedidosDATSIPHabilitaControlesCambios(false);
                    btnPedDATSIPImprimir.Enabled = true;
                }
                else
                {
                    PedidosDATSIPHabilitaControlesCambios(true);
                }
            }
            else
            {
                PedidosDATSIPHabilitaControlesNavegacionParaPrimerRegistro(false);
                PedidosDATSIPHabilitaControlesCambios(false);
                lblPedDATSIPYaImpreso.Visible = false;
            }

        }
        private void PedidosDATSIPLlenaDatos(string id, bool reload = false)
        {
            PED_MSTR pedidosBl = new PED_MSTR();
            pedidosDAT = pedidosBl.CosultarPedidosDATCliente(id);
            bindingSourcePedidosDATSIP.DataSource = pedidosDAT;
            try
            {
                if (!reload)
                {
                    txtPedDATSIPNo.DataBindings.Add("Text", bindingSourcePedidosDATSIP, "PEDIDO");
                    txtPedDATSIPFecha.DataBindings.Add("Text", bindingSourcePedidosDATSIP, "FECHA");
                    txtPedDATSIPAgente.DataBindings.Add("Text", bindingSourcePedidosDATSIP, "AGENTE");
                    txtPedDATSIPCliente.DataBindings.Add("Text", bindingSourcePedidosDATSIP, "CLIENTE");
                    txtPedDATSIPOc.DataBindings.Add("Text", bindingSourcePedidosDATSIP, "OC");
                    txtPedDATSIPTerminos.DataBindings.Add("Text", bindingSourcePedidosDATSIP, "TERMINOS");
                    txtPedDATSIPRemitido.DataBindings.Add(new Binding("Text", bindingSourcePedidosDATSIP, "REMITIDO", true));
                    txtPedDATSIPConsignado.DataBindings.Add(new Binding("Text", bindingSourcePedidosDATSIP, "CONSIGNADO", true));
                    txtPedDATSIPObservaciones.DataBindings.Add(new Binding("Text", bindingSourcePedidosDATSIP, "OBSERVACIONES", true));
                    cmbPedDATSIPFormaPago.DataBindings.Add(new Binding("SelectedValue", bindingSourcePedidosDATSIP, "FORMADEPAGOSAT", true));
                    cmbPedDATSIPUsoCFDI.DataBindings.Add(new Binding("SelectedValue", bindingSourcePedidosDATSIP, "USO_CFDI", true));
                    cmbPedDATSIPMetodoPago.DataBindings.Add(new Binding("SelectedValue", bindingSourcePedidosDATSIP, "METODODEPAGO", true));
                    lblPedDATSIPDescuento.DataBindings.Add("Text", bindingSourcePedidosDATSIP, "DESCUENTO");
                }
            }

            catch { }

            bindingNavigatorPedidosDATSIP.BindingSource = bindingSourcePedidosDATSIP;
            PedidosDATSIPEvaluaHabilitarControlesCambios();
            txtPedDATSipAValidar.Add(txtPedDATSIPRemitido);
            cmbPedDATSipAValidar.Add(cmbPedDATSIPFormaPago);
            cmbPedDATSipAValidar.Add(cmbPedDATSIPUsoCFDI);
            cmbPedDATSipAValidar.Add(cmbPedDATSIPMetodoPago);
        }
        private string PedidosDATSIPModificar()
        {
            string resultado = "";
            PED_MSTR pedidoModifica = new PED_MSTR();
            pedidoModifica.OC = txtPedDATSIPOc.Text;
            pedidoModifica.TERMINOS = txtPedDATSIPTerminos.Text;
            pedidoModifica.REMITIDO = txtPedDATSIPRemitido.Text;
            pedidoModifica.CONSIGNADO = txtPedDATSIPConsignado.Text;
            pedidoModifica.OBSERVACIONES = txtPedDATSIPObservaciones.Text;
            pedidoModifica.PEDIDO = Convert.ToInt32(txtPedDATSIPNo.Text);
            pedidoModifica.FORMADEPAGOSAT = cmbPedDATSIPFormaPago.SelectedValue.ToString();
            pedidoModifica.USO_CFDI = cmbPedDATSIPUsoCFDI.SelectedValue.ToString();
            pedidoModifica.METODODEPAGO = cmbPedDATSIPMetodoPago.SelectedValue.ToString();
            //pedidoModifica.DESCUENTO = txtPedDATSIPDescuento.Text == "" ? 0 : double.Parse(txtPedDATSIPDescuento.Text) / 100;
            pedidoModifica.Modificar(pedidoModifica);
            if (pedidoModifica.TieneError)
            {
                resultado = pedidoModifica.Error.InnerException.ToString();
            }
            return resultado;
        }
        private string PedidosDATSIPGuardar()
        {
            string resultado = "";
            int pedido = 0;
            PED_MSTR guardaPedido = new PED_MSTR();
            guardaPedido.AGENTE = Agente;
            guardaPedido.FECHA = Convert.ToDateTime(txtPedDATSIPFecha.Text);
            guardaPedido.CLIENTE = txtPedDATSIPCliente.Text;
            guardaPedido.OC = txtPedDATSIPOc.Text;
            guardaPedido.TERMINOS = Termino;
            guardaPedido.REMITIDO = txtPedDATSIPRemitido.Text;
            guardaPedido.CONSIGNADO = txtPedDATSIPConsignado.Text;
            guardaPedido.OBSERVACIONES = txtPedDATSIPObservaciones.Text;
            guardaPedido.TIPO = "DT";
            guardaPedido.LISTA = 1;
            guardaPedido.ESTATUS = "P";
            guardaPedido.FORMADEPAGOSAT = cmbPedDATSIPFormaPago.SelectedValue.ToString();
            guardaPedido.USO_CFDI = cmbPedDATSIPUsoCFDI.SelectedValue.ToString();
            guardaPedido.METODODEPAGO = cmbPedDATSIPMetodoPago.SelectedValue.ToString();
            //guardaPedido.DESCUENTO = txtPedDATSIPDescuento.Text == "" ? 0 : double.Parse(txtPedDATSIPDescuento.Text);
            guardaPedido.Crear(guardaPedido, ref pedido);
            if (!guardaPedido.TieneError)
            {
                UPPEDIDOS guardaUppedidos = new UPPEDIDOS();
                guardaUppedidos.PEDIDO = pedido;
                guardaUppedidos.COD_CLIENTE = clave_cliente;
                guardaUppedidos.F_CAPT = DateTime.Now;
                guardaUppedidos.Crear(guardaUppedidos);

                txtPedDATSIPNo.Text = pedido.ToString();
                pedidosDAT.Rows[pedidosDAT.Rows.Count - 1]["PEDIDO"] = pedido;
            }
            else
            {
                resultado = guardaPedido.Error.InnerException.ToString();
            }
            return resultado;
        }

        private void btnPedDATSIPTerminosActualiza_Click(object sender, EventArgs e)
        {
            txtPedDATSIPTerminos.Text = Termino;
            string resultado = "";
            resultado = PedidosDATSIPModificar();
            if (resultado != "")
            {
                MessageBox.Show("Se presentó el siguiente error \\n" + resultado, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void toolStripBtnPedidosDATSIPNuevo_Click(object sender, EventArgs e)
        {
            pedidoDATNuevo = true;

            DataRow nuevo_pedido = pedidosDAT.NewRow();
            pedidosDAT.Rows.Add(nuevo_pedido);
            nuevo_pedido["FECHA"] = DateTime.Now;
            nuevo_pedido["AGENTE"] = Agente;
            nuevo_pedido["CLIENTE"] = clave_cliente;
            nuevo_pedido["OC"] = "";
            nuevo_pedido["TERMINOS"] = Termino;
            nuevo_pedido["ESTATUS"] = "P";
            nuevo_pedido["FORMADEPAGOSAT"] = "03";
            nuevo_pedido["METODODEPAGO"] = "PUE";
            nuevo_pedido["USO_CFDI"] = "G01";
            bindingSourcePedidosDATSIP.MoveLast();

            PedidosDATSIPHabilitaControlesNavegacion(false);
            PedidosDATSIPHabilitaControlesCambios(true);
            btnPedDATSIPAgregarPartidas.Enabled = false;
            btnPedDATSIPElimModPartidas.Enabled = false;
            btnPedDATSIPTerminosActualiza.Enabled = false;
            btnPedDATSIPImprimir.Enabled = false;
            btnPedDATSIPDocumentos.Enabled = false;
            PedidosDATSIPHabilitaControlesNavegacionParaPrimerRegistro(true);
            txtPedDATSIPOc.Focus();
            lblPedDATSIPDivision.Visible = false;
        }
        private void toolStripBtnPedidosDATSIPGuardar_Click(object sender, EventArgs e)
        {
            if (!GeneralesExistenDatosVacios(txtPedDATSipAValidar) && !GeneralesExistenDatosVacios(cmbPedDATSipAValidar))
            {
                string resultado = "";
                if (pedidoDATNuevo)
                {
                    resultado = PedidosDATSIPGuardar();
                }
                else
                {
                    resultado = PedidosDATSIPModificar();
                }

                if (resultado == "")
                {
                    PedidosDATSIPHabilitaControlesNavegacion(true);
                    PedidosDATSIPHabilitaControlesCambios(true);
                    pedidoDATNuevo = false;
                }
                else
                {
                    MessageBox.Show("Se presentó el siguiente error \\n" + resultado, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void toolStripBtnPedidosDATSIPEliminar_Click(object sender, EventArgs e)
        {
            if (!pedidoDATNuevo)
            {
                PED_DET detalle_pedido = new PED_DET();
                int idPedido = Convert.ToInt32(txtPedDATSIPNo.Text);
                bool contiene_partidas = detalle_pedido.ContieneRegistrosPedido(idPedido);
                if (!contiene_partidas)
                {
                    if (MessageBox.Show("Está seguro que desea eliminar el pedido?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        PED_MSTR elimina_pedido = new PED_MSTR();
                        elimina_pedido.PEDIDO = idPedido;
                        elimina_pedido.Borrar(elimina_pedido, Enumerados.TipoBorrado.Fisico);
                        bindingSourcePedidosDATSIP.RemoveCurrent();
                        MessageBox.Show("El pedido ha sido eliminado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("No se puede eliminar el pedido debido a que ya tiene partidas cargadas.\n\n Debe eliminar las partidas desde el botón (Eliminar o Modificar Partidas) y luego podrá eliminar el pedido", "Información", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                bindingSourcePedidosDATSIP.RemoveCurrent();
                PedidosDATSIPHabilitaControlesNavegacion(true);
                PedidosDATSIPEvaluaHabilitarControlesCambios();
                pedidoDATNuevo = false;
            }
            if (bindingSourcePedidosDATSIP.Count == 0)
            {
                PedidosDATSIPHabilitaControlesNavegacionParaPrimerRegistro(false);
            }
        }
        private void btnPedDATSIPElimModPartidas_Click(object sender, EventArgs e)
        {
            frmEliminarPartidas frmEliminarPartidas = new frmEliminarPartidas(clave_cliente, Convert.ToInt32(txtPedDATSIPNo.Text), Enumerados.TipoPedido.PedidoDAT);
            frmEliminarPartidas.Show();
        }
        private void btnPedDATSIPAgregarPartidas_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPedDATSIPCliente.Text))
            {
                /*
                frmAgregarPartidas = new AddPartidasPedi(txtPedSIPCliente.Text,
                    Convert.ToInt32(txtPedSIPNo.Text), Enumerados.TipoPedido.Pedido, nombre_vendedor);
                frmAgregarPartidas.Show();
                 * */
                frmAgregarPartidas = new AddPartidasPedi2(txtPedDATSIPCliente.Text, Convert.ToInt32(txtPedDATSIPNo.Text), Enumerados.TipoPedido.PedidoDAT, nombre_vendedor, true);
                frmAgregarPartidas.ShowDialog();

                if (frmAgregarPartidas.pedidoEstatusModificado) { PedidosVirtualesSIPLlenaDatos(clave_cliente); PedidosDATSIPLlenaDatos(clave_cliente, true); PedidosSIPLlenaDatos(clave_cliente, true); }
            }
        }
        private void bindingNavigatorMovePreviousItemDAT_Click(object sender, EventArgs e)
        {
            PedidosDATSIPEvaluaHabilitarControlesCambios();
        }
        private void bindingNavigatorMoveNextItemDAT_Click(object sender, EventArgs e)
        {
            PedidosDATSIPEvaluaHabilitarControlesCambios();
        }
        private void bindingNavigatorMoveFirstItemDAT_Click(object sender, EventArgs e)
        {
            PedidosDATSIPEvaluaHabilitarControlesCambios();
        }
        private void bindingNavigatorMoveLastItemDAT_Click(object sender, EventArgs e)
        {
            PedidosDATSIPEvaluaHabilitarControlesCambios();
        }
        private void toolStripBtnBuscarDAT_Click(object sender, EventArgs e)
        {
            frmInputBox frmInputBox = new SIP.frmInputBox(Enumerados.TipoCajaTextoInputBox.Numerica);

            frmInputBox.lblTitulo.Text = "Escriba el número de pedido";
            frmInputBox.Text = "Búsqueda de pedido";

            if (frmInputBox.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                int pos = bindingSourcePedidosDATSIP.Find("Pedido", frmInputBox.NTxtOrden.Text);
                if (pos != -1)
                {
                    tmrTextBoxHilight = new Timer();
                    tmrTextBoxHilight.Tick += tmrTextBoxHilight_Tick;
                    tmrTextBoxHilight.Interval = 250;
                    tmrTextBoxHilight.Enabled = true;
                    bindingSourcePedidosDATSIP.Position = pos;
                }
                else
                {
                    MessageBox.Show("No se encontró número de pedido", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    toolStripBtnBuscarDAT_Click(sender, e);
                }
            }
        }
        private void btnPedDATSIPImprimir_Click(object sender, EventArgs e)
        {
            if (pedidosDAT.Rows[bindingSourcePedidosDATSIP.Position]["PEDIDO"] != DBNull.Value)
            {
                int idPedido = Convert.ToInt32(pedidosDAT.Rows[bindingSourcePedidosDATSIP.Position]["PEDIDO"].ToString());
                string status = pedidosDAT.Rows[bindingSourcePedidosDATSIP.Position]["ESTATUS"].ToString();
                frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos,
                    clave_cliente, 0, idPedido, Enumerados.TipoPedido.PedidoDAT, true, pedidosDAT.Rows[bindingSourcePedidosDATSIP.Position]["DESCUENTO"].ToString() == "" ? 0 : double.Parse(pedidosDAT.Rows[bindingSourcePedidosDATSIP.Position]["DESCUENTO"].ToString()), this.frmOrigen);
                if (status == "I")
                {
                    frmReportes.enVentas = true;
                }
                // frmReportes.Show();
                frmReportes.ShowDialog();
                if (frmReportes.pedidoEstatusModificado) { PedidosVirtualesSIPLlenaDatos(clave_cliente); PedidosDATSIPLlenaDatos(clave_cliente, true); PedidosSIPLlenaDatos(clave_cliente, true); }
            }
        }
        #endregion

        #region Pedidos MOSTRADOR SP

        private void PedidosMOSSIPHabilitaControlesNavegacionParaPrimerRegistro(bool Habilita)
        {
            toolStripBtnPedidosMOSSIPGuardar.Enabled = Habilita;
            toolStripBtnPedidosMOSSIPEliminar.Enabled = Habilita;
            btnPedMOSCPDocumentos.Enabled = Habilita;
        }
        private void PedidosMOSSIPHabilitaControlesNavegacion(bool Habilita)
        {
            bindingNavigatorMoveFirstItemMOS.Enabled = Habilita;
            bindingNavigatorMovePreviousItemMOS.Enabled = Habilita;
            bindingNavigatorMoveNextItemMOS.Enabled = Habilita;
            bindingNavigatorMoveLastItemMOS.Enabled = Habilita;
            toolStripBtnPedidosMOSSIPNuevo.Enabled = Habilita;
            bindingNavigatorPositionItemMOS.Enabled = Habilita;
        }
        private void PedidosMOSSIPHabilitaControlesCambios(bool Habilita)
        {
            lblPedMOSSIPYaImpreso.Visible = !Habilita;
            txtPedMOSSIPOc.Enabled = Habilita;
            txtPedMOSSIPRemitido.Enabled = Habilita;
            txtPedMOSSIPConsignado.Enabled = Habilita;
            txtPedMOSSIPObservaciones.Enabled = Habilita;
            if (Globales.UsuarioActual.UsuarioUsuario.Trim().ToUpper() == "MOSTRADOR")
            {
                btnPedMOSSIPAgregarPartidas.Enabled = this.clasificacionVendedor == "F"; ;
                btnPedMOSSIPElimModPartidas.Enabled = this.clasificacionVendedor == "F"; ;
            }
            else
            {
                btnPedMOSSIPAgregarPartidas.Enabled = Habilita && !lblPedMOSSIPDivision.Visible; ;
                btnPedMOSSIPElimModPartidas.Enabled = Habilita;
            }
            btnPedMOSSIPTerminosActualiza.Enabled = Habilita;
            toolStripBtnPedidosMOSSIPGuardar.Enabled = Habilita;
            btnPedMOSSIPImprimir.Enabled = Habilita;
            chkPedSIPEcommerce.Enabled = Habilita;
            txtPedMOSSIPConsignado.Enabled = Habilita;
            btnPedMOSSIPDocumentos.Enabled = true;
            HabilitaNuevoPedido();
        }
        private void PedidosMOSSIPEvaluaHabilitarControlesCambios()
        {
            if (bindingSourcePedidosMOSSIP.Count > 0)
            {
                DataRowView row = (DataRowView)bindingSourcePedidosMOSSIP.Current;
                string status = row["ESTATUS"].ToString();
                lblPedMOSSIPDivision.Visible = this.pedidosVirtuales.Select("PedidoDivision = " + row["PEDIDO"].ToString()).Count() > 0;
                if (status == "I")
                {
                    PedidosMOSSIPHabilitaControlesCambios(false);
                    btnPedMOSSIPImprimir.Enabled = true;
                }
                else
                {
                    PedidosMOSSIPHabilitaControlesCambios(true);
                }
            }
            else
            {
                PedidosMOSSIPHabilitaControlesNavegacionParaPrimerRegistro(false);
                PedidosMOSSIPHabilitaControlesCambios(false);
                lblPedMOSSIPYaImpreso.Visible = false;
                btnPedMOSSIPDocumentos.Enabled = false;
            }

        }
        private void PedidosMOSSIPLlenaDatos(string id, bool reload = false)
        {
            PED_MSTR pedidosBl = new PED_MSTR();
            pedidosMOS = pedidosBl.CosultarPedidosMOSCliente(id);
            bindingSourcePedidosMOSSIP.DataSource = pedidosMOS;

            try
            {
                if (!reload)
                {
                    txtPedMOSSIPNo.DataBindings.Add("Text", bindingSourcePedidosMOSSIP, "PEDIDO");
                    txtPedMOSSIPFecha.DataBindings.Add("Text", bindingSourcePedidosMOSSIP, "FECHA");
                    txtPedMOSSIPAgente.DataBindings.Add("Text", bindingSourcePedidosMOSSIP, "AGENTE");
                    txtPedMOSSIPCliente.DataBindings.Add("Text", bindingSourcePedidosMOSSIP, "CLIENTE");
                    txtPedMOSSIPOc.DataBindings.Add("Text", bindingSourcePedidosMOSSIP, "OC");
                    txtPedMOSSIPTerminos.DataBindings.Add("Text", bindingSourcePedidosMOSSIP, "TERMINOS");
                    txtPedMOSSIPRemitido.DataBindings.Add(new Binding("Text", bindingSourcePedidosMOSSIP, "REMITIDO", true));
                    txtPedMOSSIPConsignado.DataBindings.Add(new Binding("Text", bindingSourcePedidosMOSSIP, "CONSIGNADO", true));
                    txtPedMOSSIPObservaciones.DataBindings.Add(new Binding("Text", bindingSourcePedidosMOSSIP, "OBSERVACIONES", true));
                    cmbPedMOSSIPFormaPago.DataBindings.Add(new Binding("SelectedValue", bindingSourcePedidosMOSSIP, "FORMADEPAGOSAT", true));
                    cmbPedMOSSIPUsoCFDI.DataBindings.Add(new Binding("SelectedValue", bindingSourcePedidosMOSSIP, "USO_CFDI", true));
                    cmbPedMOSSIPMetodoPago.DataBindings.Add(new Binding("SelectedValue", bindingSourcePedidosMOSSIP, "METODODEPAGO", true));
                    lblPedMOSSIPDescuento.DataBindings.Add("Text", bindingSourcePedidosMOSSIP, "DESCUENTO");
                    lblPedSIPEcommerce.DataBindings.Add("Text", bindingSourcePedidosMOSSIP, "TIPO");
                }
            }

            catch { }

            bindingNavigatorPedidosMOSSIP.BindingSource = bindingSourcePedidosMOSSIP;
            PedidosMOSSIPEvaluaHabilitarControlesCambios();
            txtPedMOSSipAValidar.Add(txtPedMOSSIPRemitido);
            cmbPedMOSSipAValidar.Add(cmbPedMOSSIPFormaPago);
            cmbPedMOSSipAValidar.Add(cmbPedMOSSIPUsoCFDI);
            cmbPedMOSSipAValidar.Add(cmbPedMOSSIPMetodoPago);
        }
        private string PedidosMOSSIPModificar()
        {
            string resultado = "";
            PED_MSTR pedidoModifica = new PED_MSTR();
            pedidoModifica.OC = txtPedMOSSIPOc.Text;
            pedidoModifica.TERMINOS = txtPedMOSSIPTerminos.Text;
            pedidoModifica.REMITIDO = txtPedMOSSIPRemitido.Text;
            pedidoModifica.CONSIGNADO = txtPedMOSSIPConsignado.Text;
            pedidoModifica.OBSERVACIONES = txtPedMOSSIPObservaciones.Text;
            pedidoModifica.PEDIDO = Convert.ToInt32(txtPedMOSSIPNo.Text);
            pedidoModifica.FORMADEPAGOSAT = cmbPedMOSSIPFormaPago.SelectedValue.ToString();
            pedidoModifica.USO_CFDI = cmbPedMOSSIPUsoCFDI.SelectedValue.ToString();
            pedidoModifica.METODODEPAGO = cmbPedMOSSIPMetodoPago.SelectedValue.ToString();
            //pedidoModifica.DESCUENTO = txtPedMOSSIPDescuento.Text == "" ? 0 : double.Parse(txtPedMOSSIPDescuento.Text) / 100;
            pedidoModifica.Modificar(pedidoModifica);
            if (pedidoModifica.TieneError)
            {
                resultado = pedidoModifica.Error.InnerException.ToString();
            }
            return resultado;
        }
        private string PedidosMOSSIPGuardar()
        {
            string resultado = "";
            int pedido = 0;
            PED_MSTR guardaPedido = new PED_MSTR();
            guardaPedido.AGENTE = Agente;
            guardaPedido.FECHA = Convert.ToDateTime(txtPedMOSSIPFecha.Text);
            guardaPedido.CLIENTE = txtPedMOSSIPCliente.Text;
            guardaPedido.OC = txtPedMOSSIPOc.Text;
            guardaPedido.TERMINOS = Termino;
            guardaPedido.REMITIDO = txtPedMOSSIPRemitido.Text;
            guardaPedido.CONSIGNADO = txtPedMOSSIPConsignado.Text;
            guardaPedido.OBSERVACIONES = txtPedMOSSIPObservaciones.Text;
            guardaPedido.TIPO = chkPedSIPEcommerce.Checked ? "EC" : "MS";
            guardaPedido.LISTA = 1;
            guardaPedido.ESTATUS = "P";
            guardaPedido.FORMADEPAGOSAT = cmbPedMOSSIPFormaPago.SelectedValue.ToString();
            guardaPedido.USO_CFDI = cmbPedMOSSIPUsoCFDI.SelectedValue.ToString();
            guardaPedido.METODODEPAGO = cmbPedMOSSIPMetodoPago.SelectedValue.ToString();
            //guardaPedido.DESCUENTO = txtPedMOSSIPDescuento.Text == "" ? 0 : double.Parse(txtPedMOSSIPDescuento.Text);
            guardaPedido.Crear(guardaPedido, ref pedido);
            if (!guardaPedido.TieneError)
            {
                UPPEDIDOS guardaUppedidos = new UPPEDIDOS();
                guardaUppedidos.PEDIDO = pedido;
                guardaUppedidos.COD_CLIENTE = clave_cliente;
                guardaUppedidos.F_CAPT = DateTime.Now;
                guardaUppedidos.Crear(guardaUppedidos);

                txtPedMOSSIPNo.Text = pedido.ToString();
                pedidosMOS.Rows[pedidosMOS.Rows.Count - 1]["PEDIDO"] = pedido;
            }
            else
            {
                resultado = guardaPedido.Error.InnerException.ToString();
            }
            return resultado;
        }


        private void btnPedMOSSIPTerminosActualiza_Click(object sender, EventArgs e)
        {
            txtPedMOSSIPTerminos.Text = Termino;
            string resultado = "";
            resultado = PedidosMOSSIPModificar();
            if (resultado != "")
            {
                MessageBox.Show("Se presentó el siguiente error \\n" + resultado, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void toolStripBtnPedidosMOSSIPNuevo_Click(object sender, EventArgs e)
        {
            pedidoMOSNuevo = true;

            DataRow nuevo_pedido = pedidosMOS.NewRow();
            pedidosMOS.Rows.Add(nuevo_pedido);
            nuevo_pedido["FECHA"] = DateTime.Now;
            nuevo_pedido["AGENTE"] = Agente;
            nuevo_pedido["CLIENTE"] = clave_cliente;
            nuevo_pedido["OC"] = "";
            nuevo_pedido["TERMINOS"] = Termino;
            nuevo_pedido["ESTATUS"] = "P";
            nuevo_pedido["FORMADEPAGOSAT"] = "03";
            nuevo_pedido["METODODEPAGO"] = "PUE";
            nuevo_pedido["USO_CFDI"] = "G01";
            bindingSourcePedidosMOSSIP.MoveLast();

            PedidosMOSSIPHabilitaControlesNavegacion(false);
            PedidosMOSSIPHabilitaControlesCambios(true);
            btnPedMOSSIPAgregarPartidas.Enabled = false;
            btnPedMOSSIPElimModPartidas.Enabled = false;
            btnPedMOSSIPTerminosActualiza.Enabled = false;
            btnPedMOSSIPImprimir.Enabled = false;
            btnPedMOSSIPDocumentos.Enabled = false;
            PedidosMOSSIPHabilitaControlesNavegacionParaPrimerRegistro(true);
            txtPedMOSSIPOc.Focus();
            lblPedMOSSIPDivision.Visible = false;
        }
        private void toolStripBtnPedidosMOSSIPGuardar_Click(object sender, EventArgs e)
        {
            if (!GeneralesExistenDatosVacios(txtPedMOSSipAValidar) && !GeneralesExistenDatosVacios(cmbPedMOSSipAValidar))
            {
                string resultado = "";
                if (pedidoMOSNuevo)
                {
                    resultado = PedidosMOSSIPGuardar();
                }
                else
                {
                    resultado = PedidosMOSSIPModificar();
                }

                if (resultado == "")
                {
                    PedidosMOSSIPHabilitaControlesNavegacion(true);
                    PedidosMOSSIPHabilitaControlesCambios(true);
                    pedidoMOSNuevo = false;
                }
                else
                {
                    MessageBox.Show("Se presentó el siguiente error \\n" + resultado, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void toolStripBtnPedidosMOSSIPEliminar_Click(object sender, EventArgs e)
        {
            if (!pedidoMOSNuevo)
            {
                PED_DET detalle_pedido = new PED_DET();
                int idPedido = Convert.ToInt32(txtPedMOSSIPNo.Text);
                bool contiene_partidas = detalle_pedido.ContieneRegistrosPedido(idPedido);
                if (!contiene_partidas)
                {
                    if (MessageBox.Show("Está seguro que desea eliminar el pedido?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        PED_MSTR elimina_pedido = new PED_MSTR();
                        elimina_pedido.PEDIDO = idPedido;
                        elimina_pedido.Borrar(elimina_pedido, Enumerados.TipoBorrado.Fisico);
                        bindingSourcePedidosMOSSIP.RemoveCurrent();
                        MessageBox.Show("El pedido ha sido eliminado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("No se puede eliminar el pedido debido a que ya tiene partidas cargadas.\n\n Debe eliminar las partidas desde el botón (Eliminar o Modificar Partidas) y luego podrá eliminar el pedido", "Información", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                bindingSourcePedidosMOSSIP.RemoveCurrent();
                PedidosMOSSIPHabilitaControlesNavegacion(true);
                PedidosMOSSIPEvaluaHabilitarControlesCambios();
                pedidoMOSNuevo = false;
            }
            if (bindingSourcePedidosMOSSIP.Count == 0)
            {
                PedidosMOSSIPHabilitaControlesNavegacionParaPrimerRegistro(false);
            }
        }
        private void btnPedMOSSIPElimModPartidas_Click(object sender, EventArgs e)
        {
            frmEliminarPartidas frmEliminarPartidas = new frmEliminarPartidas(clave_cliente, Convert.ToInt32(txtPedMOSSIPNo.Text), Enumerados.TipoPedido.PedidoMOS);
            frmEliminarPartidas.Show();
        }
        private void btnPedMOSSIPAgregarPartidas_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPedMOSSIPCliente.Text))
            {
                /*
                frmAgregarPartidas = new AddPartidasPedi(txtPedSIPCliente.Text,
                    Convert.ToInt32(txtPedSIPNo.Text), Enumerados.TipoPedido.Pedido, nombre_vendedor);
                frmAgregarPartidas.Show();
                 * */
                frmAgregarPartidas = new AddPartidasPedi2(txtPedMOSSIPCliente.Text, Convert.ToInt32(txtPedMOSSIPNo.Text), chkPedSIPEcommerce.Checked ? Enumerados.TipoPedido.PedidoEC : Enumerados.TipoPedido.PedidoMOS, nombre_vendedor, chkPedSIPEcommerce.Checked ? false : true, this.formaPagoComision);
                frmAgregarPartidas.ShowDialog();
                if (frmAgregarPartidas.pedidoEstatusModificado) { PedidosVirtualesSIPLlenaDatos(clave_cliente); PedidosMOSSIPLlenaDatos(clave_cliente, true); PedidosSIPLlenaDatos(clave_cliente, true); }
            }
        }
        private void bindingNavigatorMovePreviousItemMOS_Click(object sender, EventArgs e)
        {
            PedidosMOSSIPEvaluaHabilitarControlesCambios();
        }
        private void bindingNavigatorMoveNextItemMOS_Click(object sender, EventArgs e)
        {
            PedidosMOSSIPEvaluaHabilitarControlesCambios();
        }
        private void bindingNavigatorMoveFirstItemMOS_Click(object sender, EventArgs e)
        {
            PedidosMOSSIPEvaluaHabilitarControlesCambios();
        }
        private void bindingNavigatorMoveLastItemMOS_Click(object sender, EventArgs e)
        {
            PedidosMOSSIPEvaluaHabilitarControlesCambios();
        }
        private void toolStripBtnBuscarMOS_Click(object sender, EventArgs e)
        {
            frmInputBox frmInputBox = new SIP.frmInputBox(Enumerados.TipoCajaTextoInputBox.Numerica);

            frmInputBox.lblTitulo.Text = "Escriba el número de pedido";
            frmInputBox.Text = "Búsqueda de pedido";

            if (frmInputBox.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                int pos = bindingSourcePedidosMOSSIP.Find("Pedido", frmInputBox.NTxtOrden.Text);
                if (pos != -1)
                {
                    tmrTextBoxHilight = new Timer();
                    tmrTextBoxHilight.Tick += tmrTextBoxHilight_Tick;
                    tmrTextBoxHilight.Interval = 250;
                    tmrTextBoxHilight.Enabled = true;
                    bindingSourcePedidosMOSSIP.Position = pos;
                }
                else
                {
                    MessageBox.Show("No se encontró número de pedido", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    toolStripBtnBuscarMOS_Click(sender, e);
                }
            }
        }
        private void btnPedMOSSIPImprimir_Click(object sender, EventArgs e)
        {
            if (pedidosMOS.Rows[bindingSourcePedidosMOSSIP.Position]["PEDIDO"] != DBNull.Value)
            {
                int idPedido = Convert.ToInt32(pedidosMOS.Rows[bindingSourcePedidosMOSSIP.Position]["PEDIDO"].ToString());
                string status = pedidosMOS.Rows[bindingSourcePedidosMOSSIP.Position]["ESTATUS"].ToString();
                frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos,
                    clave_cliente, 0, idPedido, chkPedSIPEcommerce.Checked ? Enumerados.TipoPedido.PedidoEC : Enumerados.TipoPedido.PedidoMOS, chkPedSIPEcommerce.Checked ? false : true, pedidosMOS.Rows[bindingSourcePedidosMOSSIP.Position]["DESCUENTO"].ToString() == "" ? 0 : double.Parse(pedidosMOS.Rows[bindingSourcePedidosMOSSIP.Position]["DESCUENTO"].ToString()), this.frmOrigen);
                if (status == "I")
                {
                    frmReportes.enVentas = true;
                }
                // frmReportes.Show();
                frmReportes.ShowDialog();
                if (frmReportes.pedidoEstatusModificado) { PedidosVirtualesSIPLlenaDatos(clave_cliente); PedidosMOSSIPLlenaDatos(clave_cliente, true); PedidosSIPLlenaDatos(clave_cliente, true); }
            }
        }
        #endregion

        #region PEDIDOS MOSTRADOR CP
        private void PedidosMOSCPHabilitaControlesNavegacionParaPrimerRegistro(bool Habilita)
        {
            toolStripBtnPedidosMOSCPGuardar.Enabled = Habilita;
            toolStripBtnPedidosMOSCPEliminar.Enabled = Habilita;
        }
        private void PedidosMOSCPHabilitaControlesNavegacion(bool Habilita)
        {
            bindingNavigatorMoveFirstItemMOSCP.Enabled = Habilita;
            bindingNavigatorMovePreviousItemMOSCP.Enabled = Habilita;
            bindingNavigatorMoveNextItemMOSCP.Enabled = Habilita;
            bindingNavigatorMoveLastItemMOSCP.Enabled = Habilita;
            toolStripBtnPedidosMOSCPNuevo.Enabled = Habilita;
            bindingNavigatorPositionItemMOSCP.Enabled = Habilita;
        }
        private void PedidosMOSCPHabilitaControlesCambios(bool Habilita)
        {
            lblPedMOSCPYaImpreso.Visible = !Habilita;
            txtPedMOSCPOc.Enabled = Habilita;
            txtPedMOSCPRemitido.Enabled = Habilita;
            txtPedMOSCPConsignado.Enabled = Habilita;
            txtPedMOSCPObservaciones.Enabled = Habilita;
            if (Globales.UsuarioActual.UsuarioUsuario.Trim().ToUpper() == "MOSTRADOR")
            {
                btnPedMOSCPAgregarPartidas.Enabled = btnPedDATSIPAgregarPartidas.Enabled = this.clasificacionVendedor == "F"; ;
                btnPedMOSCPElimModPartidas.Enabled = btnPedDATSIPAgregarPartidas.Enabled = this.clasificacionVendedor == "F"; ;
            }
            else
            {
                btnPedMOSCPAgregarPartidas.Enabled = Habilita && !lblPedMOSCPDivision.Visible;
                btnPedMOSCPElimModPartidas.Enabled = Habilita;
            }
            btnPedMOSCPTerminosActualiza.Enabled = Habilita;
            toolStripBtnPedidosMOSCPGuardar.Enabled = Habilita;
            btnPedMOSCPImprimir.Enabled = Habilita;
            txtPedMOSCPConsignado.Enabled = Habilita;
            btnPedMOSCPDocumentos.Enabled = true;
            HabilitaNuevoPedido();
        }
        private void PedidosMOSCPEvaluaHabilitarControlesCambios()
        {
            if (bindingSourcePedidosMOSCP.Count > 0)
            {
                DataRowView row = (DataRowView)bindingSourcePedidosMOSCP.Current;
                string status = row["ESTATUS"].ToString();
                lblPedMOSCPDivision.Visible = this.pedidosVirtuales.Select("PedidoDivision = " + row["PEDIDO"].ToString()).Count() > 0;
                if (status == "I")
                {
                    PedidosMOSCPHabilitaControlesCambios(false);
                    btnPedMOSCPImprimir.Enabled = true;
                }
                else
                {
                    PedidosMOSCPHabilitaControlesCambios(true);
                }
            }
            else
            {
                PedidosMOSCPHabilitaControlesNavegacionParaPrimerRegistro(false);
                PedidosMOSCPHabilitaControlesCambios(false);
                lblPedMOSCPYaImpreso.Visible = false;
                btnPedMOSCPDocumentos.Enabled = false;
            }

        }
        private void PedidosMOSCPLlenaDatos(string id, bool reload = false)
        {
            PED_MSTR pedidosBl = new PED_MSTR();
            pedidosMOSCP = pedidosBl.CosultarPedidosMOSCPCliente(id);
            bindingSourcePedidosMOSCP.DataSource = pedidosMOSCP;

            try
            {
                if (!reload)
                {
                    txtPedMOSCPNo.DataBindings.Add("Text", bindingSourcePedidosMOSCP, "PEDIDO");
                    txtPedMOSCPFecha.DataBindings.Add("Text", bindingSourcePedidosMOSCP, "FECHA");
                    txtPedMOSCPAgente.DataBindings.Add("Text", bindingSourcePedidosMOSCP, "AGENTE");
                    txtPedMOSCPCliente.DataBindings.Add("Text", bindingSourcePedidosMOSCP, "CLIENTE");
                    txtPedMOSCPOc.DataBindings.Add("Text", bindingSourcePedidosMOSCP, "OC");
                    txtPedMOSCPTerminos.DataBindings.Add("Text", bindingSourcePedidosMOSCP, "TERMINOS");
                    txtPedMOSCPRemitido.DataBindings.Add(new Binding("Text", bindingSourcePedidosMOSCP, "REMITIDO", true));
                    txtPedMOSCPConsignado.DataBindings.Add(new Binding("Text", bindingSourcePedidosMOSCP, "CONSIGNADO", true));
                    txtPedMOSCPObservaciones.DataBindings.Add(new Binding("Text", bindingSourcePedidosMOSCP, "OBSERVACIONES", true));
                    cmbPedMOSCPFormaPago.DataBindings.Add(new Binding("SelectedValue", bindingSourcePedidosMOSCP, "FORMADEPAGOSAT", true));
                    cmbPedMOSCPUsoCFDI.DataBindings.Add(new Binding("SelectedValue", bindingSourcePedidosMOSCP, "USO_CFDI", true));
                    cmbPedMOSCPMetodoPago.DataBindings.Add(new Binding("SelectedValue", bindingSourcePedidosMOSCP, "METODODEPAGO", true));
                    lblPedMOSCPDescuento.DataBindings.Add("Text", bindingSourcePedidosMOSCP, "DESCUENTO");
                }
            }

            catch { }

            bindingNavigatorPedidosMOSCP.BindingSource = bindingSourcePedidosMOSCP;
            PedidosMOSCPEvaluaHabilitarControlesCambios();
            txtPedMOSCPAValidar.Add(txtPedMOSCPRemitido);
            cmbPedMOSCPAValidar.Add(cmbPedMOSCPFormaPago);
            cmbPedMOSCPAValidar.Add(cmbPedMOSCPUsoCFDI);
            cmbPedMOSCPAValidar.Add(cmbPedMOSCPMetodoPago);
        }
        private string PedidosMOSCPModificar()
        {
            string resultado = "";
            PED_MSTR pedidoModifica = new PED_MSTR();
            pedidoModifica.OC = txtPedMOSCPOc.Text;
            pedidoModifica.TERMINOS = txtPedMOSCPTerminos.Text;
            pedidoModifica.REMITIDO = txtPedMOSCPRemitido.Text;
            pedidoModifica.CONSIGNADO = txtPedMOSCPConsignado.Text;
            pedidoModifica.OBSERVACIONES = txtPedMOSCPObservaciones.Text;
            pedidoModifica.PEDIDO = Convert.ToInt32(txtPedMOSCPNo.Text);
            pedidoModifica.FORMADEPAGOSAT = cmbPedMOSCPFormaPago.SelectedValue.ToString();
            pedidoModifica.USO_CFDI = cmbPedMOSCPUsoCFDI.SelectedValue.ToString();
            pedidoModifica.METODODEPAGO = cmbPedMOSCPMetodoPago.SelectedValue.ToString();
            //pedidoModifica.DESCUENTO = txtPedMOSCPDescuento.Text == "" ? 0 : double.Parse(txtPedMOSCPDescuento.Text) / 100;
            pedidoModifica.Modificar(pedidoModifica);
            if (pedidoModifica.TieneError)
            {
                resultado = pedidoModifica.Error.InnerException.ToString();
            }
            return resultado;
        }
        private string PedidosMOSCPGuardar()
        {
            string resultado = "";
            int pedido = 0;
            PED_MSTR guardaPedido = new PED_MSTR();
            guardaPedido.AGENTE = Agente;
            guardaPedido.FECHA = Convert.ToDateTime(txtPedMOSCPFecha.Text);
            guardaPedido.CLIENTE = txtPedMOSCPCliente.Text;
            guardaPedido.OC = txtPedMOSCPOc.Text;
            guardaPedido.TERMINOS = Termino;
            guardaPedido.REMITIDO = txtPedMOSCPRemitido.Text;
            guardaPedido.CONSIGNADO = txtPedMOSCPConsignado.Text;
            guardaPedido.OBSERVACIONES = txtPedMOSCPObservaciones.Text;
            guardaPedido.TIPO = "MP";
            guardaPedido.LISTA = 1;
            guardaPedido.ESTATUS = "P";
            guardaPedido.FORMADEPAGOSAT = cmbPedMOSCPFormaPago.SelectedValue.ToString();
            guardaPedido.USO_CFDI = cmbPedMOSCPUsoCFDI.SelectedValue.ToString();
            guardaPedido.METODODEPAGO = cmbPedMOSCPMetodoPago.SelectedValue.ToString();
            //guardaPedido.DESCUENTO = txtPedMOSCPDescuento.Text == "" ? 0 : double.Parse(txtPedMOSCPDescuento.Text);
            guardaPedido.Crear(guardaPedido, ref pedido);
            if (!guardaPedido.TieneError)
            {
                UPPEDIDOS guardaUppedidos = new UPPEDIDOS();
                guardaUppedidos.PEDIDO = pedido;
                guardaUppedidos.COD_CLIENTE = clave_cliente;
                guardaUppedidos.F_CAPT = DateTime.Now;
                guardaUppedidos.Crear(guardaUppedidos);

                txtPedMOSCPNo.Text = pedido.ToString();
                pedidosMOSCP.Rows[pedidosMOSCP.Rows.Count - 1]["PEDIDO"] = pedido;
            }
            else
            {
                resultado = guardaPedido.Error.InnerException.ToString();
            }
            return resultado;
        }

        private void btnPedMOSCPTerminosActualiza_Click(object sender, EventArgs e)
        {
            txtPedMOSCPTerminos.Text = Termino;
            string resultado = "";
            resultado = PedidosMOSCPModificar();
            if (resultado != "")
            {
                MessageBox.Show("Se presentó el siguiente error \\n" + resultado, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void toolStripBtnPedidosMOSCPNuevo_Click(object sender, EventArgs e)
        {
            pedidoMOSCPNuevo = true;

            DataRow nuevo_pedido = pedidosMOSCP.NewRow();
            pedidosMOSCP.Rows.Add(nuevo_pedido);
            nuevo_pedido["FECHA"] = DateTime.Now;
            nuevo_pedido["AGENTE"] = Agente;
            nuevo_pedido["CLIENTE"] = clave_cliente;
            nuevo_pedido["OC"] = "";
            nuevo_pedido["TERMINOS"] = Termino;
            nuevo_pedido["ESTATUS"] = "P";
            nuevo_pedido["FORMADEPAGOSAT"] = "03";
            nuevo_pedido["METODODEPAGO"] = "PUE";
            nuevo_pedido["USO_CFDI"] = "G01";
            bindingSourcePedidosMOSCP.MoveLast();

            PedidosMOSCPHabilitaControlesNavegacion(false);
            PedidosMOSCPHabilitaControlesCambios(true);
            btnPedMOSCPAgregarPartidas.Enabled = false;
            btnPedMOSCPElimModPartidas.Enabled = false;
            btnPedMOSCPTerminosActualiza.Enabled = false;
            btnPedMOSCPImprimir.Enabled = false;
            btnPedMOSCPDocumentos.Enabled = false;
            PedidosMOSCPHabilitaControlesNavegacionParaPrimerRegistro(true);
            txtPedMOSCPOc.Focus();
            lblPedMOSCPDivision.Visible = false;
        }
        private void toolStripBtnPedidosMOSCPGuardar_Click(object sender, EventArgs e)
        {
            if (!GeneralesExistenDatosVacios(txtPedMOSCPAValidar) && !GeneralesExistenDatosVacios(cmbPedMOSCPAValidar))
            {
                string resultado = "";
                if (pedidoMOSCPNuevo)
                {
                    resultado = PedidosMOSCPGuardar();
                }
                else
                {
                    resultado = PedidosMOSCPModificar();
                }

                if (resultado == "")
                {
                    PedidosMOSCPHabilitaControlesNavegacion(true);
                    PedidosMOSCPHabilitaControlesCambios(true);
                    pedidoMOSCPNuevo = false;
                }
                else
                {
                    MessageBox.Show("Se presentó el siguiente error \\n" + resultado, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void toolStripBtnPedidosMOSCPEliminar_Click(object sender, EventArgs e)
        {
            if (!pedidoMOSCPNuevo)
            {
                PED_DET detalle_pedido = new PED_DET();
                int idPedido = Convert.ToInt32(txtPedMOSCPNo.Text);
                bool contiene_partidas = detalle_pedido.ContieneRegistrosPedido(idPedido);
                if (!contiene_partidas)
                {
                    if (MessageBox.Show("Está seguro que desea eliminar el pedido?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        PED_MSTR elimina_pedido = new PED_MSTR();
                        elimina_pedido.PEDIDO = idPedido;
                        elimina_pedido.Borrar(elimina_pedido, Enumerados.TipoBorrado.Fisico);
                        bindingSourcePedidosMOSCP.RemoveCurrent();
                        MessageBox.Show("El pedido ha sido eliminado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("No se puede eliminar el pedido debido a que ya tiene partidas cargadas.\n\n Debe eliminar las partidas desde el botón (Eliminar o Modificar Partidas) y luego podrá eliminar el pedido", "Información", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                bindingSourcePedidosMOSCP.RemoveCurrent();
                PedidosMOSCPHabilitaControlesNavegacion(true);
                PedidosMOSCPEvaluaHabilitarControlesCambios();
                pedidoMOSCPNuevo = false;
            }
            if (bindingSourcePedidosMOSCP.Count == 0)
            {
                PedidosMOSCPHabilitaControlesNavegacionParaPrimerRegistro(false);
            }
        }
        private void btnPedMOSCPElimModPartidas_Click(object sender, EventArgs e)
        {
            frmEliminarPartidas frmEliminarPartidas = new frmEliminarPartidas(clave_cliente, Convert.ToInt32(txtPedMOSCPNo.Text), Enumerados.TipoPedido.PedidoMOSCP);
            frmEliminarPartidas.Show();
        }
        private void btnPedMOSCPAgregarPartidas_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPedMOSCPCliente.Text))
            {
                /*
                frmAgregarPartidas = new AddPartidasPedi(txtPedCPCliente.Text,
                    Convert.ToInt32(txtPedCPNo.Text), Enumerados.TipoPedido.Pedido, nombre_vendedor);
                frmAgregarPartidas.Show();
                 * */
                frmAgregarPartidas = new AddPartidasPedi2(txtPedMOSCPCliente.Text, Convert.ToInt32(txtPedMOSCPNo.Text), Enumerados.TipoPedido.PedidoMOSCP, nombre_vendedor, true, this.formaPagoComision);
                frmAgregarPartidas.ShowDialog();
                if (frmAgregarPartidas.pedidoEstatusModificado) { PedidosVirtualesSIPLlenaDatos(clave_cliente); PedidosMOSCPLlenaDatos(clave_cliente, true); PedidosSIPLlenaDatos(clave_cliente, true); }
            }
        }
        private void bindingNavigatorMovePreviousItemMOSCP_Click(object sender, EventArgs e)
        {
            PedidosMOSCPEvaluaHabilitarControlesCambios();
        }
        private void bindingNavigatorMoveNextItemMOSCP_Click(object sender, EventArgs e)
        {
            PedidosMOSCPEvaluaHabilitarControlesCambios();
        }
        private void bindingNavigatorMoveFirstItemMOSCP_Click(object sender, EventArgs e)
        {
            PedidosMOSCPEvaluaHabilitarControlesCambios();
        }
        private void bindingNavigatorMoveLastItemMOSCP_Click(object sender, EventArgs e)
        {
            PedidosMOSCPEvaluaHabilitarControlesCambios();
        }
        private void toolStripBtnBuscarMOSCP_Click(object sender, EventArgs e)
        {
            frmInputBox frmInputBox = new SIP.frmInputBox(Enumerados.TipoCajaTextoInputBox.Numerica);

            frmInputBox.lblTitulo.Text = "Escriba el número de pedido";
            frmInputBox.Text = "Búsqueda de pedido";

            if (frmInputBox.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                int pos = bindingSourcePedidosMOSCP.Find("Pedido", frmInputBox.NTxtOrden.Text);
                if (pos != -1)
                {
                    tmrTextBoxHilight = new Timer();
                    tmrTextBoxHilight.Tick += tmrTextBoxHilight_Tick;
                    tmrTextBoxHilight.Interval = 250;
                    tmrTextBoxHilight.Enabled = true;
                    bindingSourcePedidosMOSCP.Position = pos;
                }
                else
                {
                    MessageBox.Show("No se encontró número de pedido", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    toolStripBtnBuscarMOSCP_Click(sender, e);
                }
            }
        }
        private void btnPedMOSCPImprimir_Click(object sender, EventArgs e)
        {
            if (pedidosMOSCP.Rows[bindingSourcePedidosMOSCP.Position]["PEDIDO"] != DBNull.Value)
            {
                int idPedido = Convert.ToInt32(pedidosMOSCP.Rows[bindingSourcePedidosMOSCP.Position]["PEDIDO"].ToString());
                string status = pedidosMOSCP.Rows[bindingSourcePedidosMOSCP.Position]["ESTATUS"].ToString();
                frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.Pedidos,
                    clave_cliente, 0, idPedido, Enumerados.TipoPedido.PedidoMOSCP, true, pedidosMOSCP.Rows[bindingSourcePedidosMOSCP.Position]["DESCUENTO"].ToString() == "" ? 0 : double.Parse(pedidosMOSCP.Rows[bindingSourcePedidosMOSCP.Position]["DESCUENTO"].ToString()), this.frmOrigen);
                if (status == "I")
                {
                    frmReportes.enVentas = true;
                }
                // frmReportes.Show();
                frmReportes.ShowDialog();
                if (frmReportes.pedidoEstatusModificado) { PedidosMOSCPLlenaDatos(clave_cliente, true); PedidosSIPLlenaDatos(clave_cliente, true); PedidosVirtualesSIPLlenaDatos(clave_cliente); }
            }
        }
        #endregion

        #region SOLICITUDES DE CODIGO ESPECIALES

        private void SolicitudesEvaluaHabilitarControlesCambios()
        {
            if (bindingSourceSolicitudes.Count > 0)
            {
                DataRowView row = (DataRowView)bindingSourceSolicitudes.Current;
                string status = row["ESTATUS"].ToString();
                if (status == "A")
                {
                    SolicitudesHabilitaControlesCambios(false);
                }
                else
                {
                    SolicitudesHabilitaControlesCambios(true);
                }

            }
            else
            {
                SolicitudesHabilitaControlesNavegacionParaPrimerRegistro(false);
                SolicitudesHabilitaControlesCambios(false);
            }
        }
        private void SolicitudesHabilitaControlesNavegacionParaPrimerRegistro(bool Habilita)
        {
            toolStripBtnSolicitudesGuardar.Enabled = Habilita;
        }

        private void SolicitudesHabilitaControlesNavegacion(bool Habilita)
        {
            bindingNavigatorMoveFirstItem2.Enabled = Habilita;
            bindingNavigatorMovePreviousItem2.Enabled = Habilita;
            bindingNavigatorMoveNextItem2.Enabled = Habilita;
            bindingNavigatorMoveLastItem2.Enabled = Habilita;
            toolStripBtnSolicitudesNueva.Enabled = Habilita;
            bindingNavigatorPositionItem2.Enabled = Habilita;
        }
        private void SolicitudesHabilitaControlesCambios(bool Habilita)
        {
            txtSolicitudNumero.Enabled = Habilita;
            txtSolicitudAgente.Enabled = Habilita;
            txtSolicitudCliente.Enabled = Habilita;
            txtSolicitudFecha.Enabled = Habilita;

            txtSolicitudNoPrendasEsp.Enabled = Habilita;
            txtSolicitudNoPrendasLinea.Enabled = Habilita;
            cmbSolicitudGenero.Enabled = Habilita;

            cmbSolicitudTipoPrenda.Enabled = Habilita;
            cmbSolicitudComposicionTela.Enabled = Habilita;
            cmbSolicitudColor.Enabled = Habilita;
            txtSolicitudEspecificaciones.Enabled = Habilita;
            txtSolicitudObservaciones.Enabled = Habilita;

            toolStripBtnSolicitudesGuardar.Enabled = Habilita;

        }

        private void SolicitudesLlenaDatos(string id)
        {
            SOLICITUDES_ESPECIALES solicitudesBl = new SOLICITUDES_ESPECIALES();
            Solicitudes = solicitudesBl.ConsultasSolicitudesCliente(id);
            bindingSourceSolicitudes.DataSource = Solicitudes;

            txtSolicitudNumero.DataBindings.Add("Text", bindingSourceSolicitudes, "SOLICITUD");
            txtSolicitudCliente.DataBindings.Add("Text", bindingSourceSolicitudes, "CLIENTE");
            txtSolicitudFecha.DataBindings.Add("Text", bindingSourceSolicitudes, "FECHA");
            txtSolicitudAgente.DataBindings.Add("Text", bindingSourceSolicitudes, "AGENTE");

            txtSolicitudNoPrendasLinea.DataBindings.Add("Text", bindingSourceSolicitudes, "PRENDAS_LINEA");
            txtSolicitudNoPrendasEsp.DataBindings.Add("Text", bindingSourceSolicitudes, "PRENDAS_ESP");
            cmbSolicitudGenero.DataBindings.Add("Text", bindingSourceSolicitudes, "GENERO");

            cmbSolicitudTipoPrenda.DataBindings.Add("Text", bindingSourceSolicitudes, "TIPO_PRENDA");
            cmbSolicitudComposicionTela.DataBindings.Add("Text", bindingSourceSolicitudes, "COMPOSICION_TELA");
            cmbSolicitudColor.DataBindings.Add("Text", bindingSourceSolicitudes, "COLOR");
            txtSolicitudEspecificaciones.DataBindings.Add("Text", bindingSourceSolicitudes, "ESPECIFICACIONES");
            lblPrecio.DataBindings.Add("Text", bindingSourceSolicitudes, "PRECIO_ASIGNADO");
            lblSolicitudCodigo.DataBindings.Add("Text", bindingSourceSolicitudes, "CODIGO_COTIZACION");


            cmbSolicitudTipoPrenda.SelectedIndex = 0;
            cmbSolicitudComposicionTela.SelectedIndex = 0;
            cmbSolicitudColor.SelectedIndex = 0;

            //AGREGMOS LOS TXT QUE SE VALIDAN
            txtSolicitudesAValidar.Add(txtSolicitudNoPrendasLinea);
            txtSolicitudesAValidar.Add(txtSolicitudNoPrendasEsp);
            txtSolicitudesAValidar.Add(txtSolicitudEspecificaciones);

            cmbSolicitudesAValidar.Add(cmbSolicitudTipoPrenda);
            cmbSolicitudesAValidar.Add(cmbSolicitudComposicionTela);
            cmbSolicitudesAValidar.Add(cmbSolicitudColor);
            cmbSolicitudesAValidar.Add(cmbSolicitudGenero);


            bindingNavigatorSolicitudesEspeciales.BindingSource = bindingSourceSolicitudes;
            SolicitudesEvaluaHabilitarControlesCambios();
        }

        private string SolicitudesGuardar()
        {
            string resultado = "";
            int idSolicitud = 0;
            SOLICITUDES_ESPECIALES guardaSolicitud = new SOLICITUDES_ESPECIALES();
            guardaSolicitud.AGENTE = Agente;
            guardaSolicitud.FECHA = Convert.ToDateTime(txtSolicitudFecha.Text);
            guardaSolicitud.CLIENTE = txtSolicitudCliente.Text.Trim();

            guardaSolicitud.PRENDAS_LINEA = int.Parse(txtSolicitudNoPrendasLinea.Text);
            guardaSolicitud.PRENDAS_ESP = int.Parse(txtSolicitudNoPrendasEsp.Text);
            guardaSolicitud.TIPO_PRENDA = cmbSolicitudTipoPrenda.Text;
            guardaSolicitud.COMPOSICION_TELA = cmbSolicitudComposicionTela.Text;
            guardaSolicitud.COLOR = cmbSolicitudColor.Text;
            guardaSolicitud.ESPECIFICACIONES = lblSolicitudOtrasEspecificaciones.Text + " " + txtSolicitudEspecificaciones.Text;
            guardaSolicitud.GENERO = cmbSolicitudGenero.Text;
            guardaSolicitud.TALLAS = "";
            guardaSolicitud.PLAZO_ENTREGA = "";
            guardaSolicitud.PRECIO_ASIGNADO = 0;
            guardaSolicitud.CODIGO_COTIZACION = "";
            guardaSolicitud.CODIGOS_ASIGNADOS = "";
            guardaSolicitud.ESTATUS = "A";


            guardaSolicitud.Crear(guardaSolicitud, ref idSolicitud);
            if (!guardaSolicitud.TieneError)
            {

                txtSolicitudNumero.Text = idSolicitud.ToString();
                Solicitudes.Rows[Solicitudes.Rows.Count - 1]["SOLICITUD"] = idSolicitud;

                //INSERTAMOS LA LINEA DE TIEMPO
                //OBTENEMOS EL ULTIMO ID DE PROCES
                int _idProceso = int.Parse(ControlPedidos.getSiguienteIDProceso().Rows[0]["ID"].ToString());
                int tipoProceso = (int)ControlFlujo.TiposPrceso.SolicitudEspecial;
                //1. damos de alta el registro del ejecutivo de ventas
                setAltaLineaTiempoPedido(_idProceso, "EV", "A", 1, txtSolicitudObservaciones.Text.Trim(), Globales.UsuarioActual.UsuarioUsuario, tipoProceso, idSolicitud, guardaSolicitud.CLIENTE);
                //2. Damos de alta el siguiente registro en este caso para el gerente de ventas
                setLineaTiempoPedido(_idProceso, "GV", "G", 2, txtSolicitudObservaciones.Text, Globales.UsuarioActual.UsuarioUsuario, tipoProceso, idSolicitud, guardaSolicitud.CLIENTE);

            }
            else
            {
                resultado = guardaSolicitud.Error.InnerException.ToString();
            }
            return resultado;
        }
        private string SolicitudesModificar()
        {
            string resultado = "";

            SOLICITUDES_ESPECIALES modificaSolicitud = new SOLICITUDES_ESPECIALES();
            modificaSolicitud.SOLICITUD = int.Parse(txtSolicitudNumero.Text);
            modificaSolicitud.AGENTE = Agente;
            modificaSolicitud.FECHA = Convert.ToDateTime(txtSolicitudFecha.Text);
            modificaSolicitud.CLIENTE = txtSolicitudCliente.Text.Trim();

            modificaSolicitud.PRENDAS_LINEA = int.Parse(txtSolicitudNoPrendasLinea.Text);
            modificaSolicitud.PRENDAS_ESP = int.Parse(txtSolicitudNoPrendasEsp.Text);
            modificaSolicitud.TIPO_PRENDA = cmbSolicitudTipoPrenda.Text;
            modificaSolicitud.COMPOSICION_TELA = cmbSolicitudComposicionTela.Text;
            modificaSolicitud.COLOR = cmbSolicitudColor.Text;
            modificaSolicitud.ESPECIFICACIONES = lblSolicitudOtrasEspecificaciones.Text + " " + txtSolicitudEspecificaciones.Text;
            modificaSolicitud.GENERO = cmbSolicitudGenero.Text;
            modificaSolicitud.TALLAS = "";
            modificaSolicitud.PLAZO_ENTREGA = "";
            modificaSolicitud.PRECIO_ASIGNADO = 0;
            modificaSolicitud.CODIGO_COTIZACION = "";
            modificaSolicitud.CODIGOS_ASIGNADOS = "";
            modificaSolicitud.ESTATUS = "A";


            modificaSolicitud.Modificar(modificaSolicitud);
            if (modificaSolicitud.TieneError)
            {
                resultado = modificaSolicitud.Error.InnerException.ToString();
            }
            return resultado;
        }

        private void CargaHistorico(String solicitud)
        {
            dgvHistorico.Columns.Clear();
            dgvHistorico.Rows.Clear();
            //OBTENEMOS EL HISTORICO DEL PEDIDO
            String Pedido = solicitud;
            DataTable dtHistoricoPedido = new DataTable();
            dtHistoricoPedido = getHistoricoPedido(Pedido);
            if (dtHistoricoPedido.Rows.Count > 0)
            {
                DataGridViewRow rowFechas = new DataGridViewRow();
                DataGridViewRow rowDias = new DataGridViewRow();
                int totalColumnas = dtHistoricoPedido.Rows.Count * 2;
                int posicion = 1;

                //CREAMOS ENCABEZADOS
                dgvHistorico.Columns.Add("Col0", "Descripcion");
                dgvHistorico.Rows.Add();
                dgvHistorico.Rows.Add();

                dgvHistorico.Rows[0].Cells[0].Value = "Calendario";
                dgvHistorico.Rows[1].Cells[0].Value = "Días Transcurridos";
                //RECORREMOS CADA FILA Y GENERAMOS LAS COLUMANAS AL GRID
                foreach (DataRow _dr in dtHistoricoPedido.Rows)
                {
                    dgvHistorico.Columns.Add("Col" + posicion.ToString(), "INI - " + _dr["Area"].ToString());
                    dgvHistorico.Rows[0].Cells[posicion].Value = DateTime.Parse(_dr["FechaInicio"].ToString()).ToString("dd/MM/yyyy HH:mm");
                    dgvHistorico.Rows[1].Cells[posicion].Value = "";
                    posicion += 1;
                    dgvHistorico.Columns.Add("Col" + posicion.ToString(), "FIN - " + _dr["Area"].ToString());
                    dgvHistorico.Rows[0].Cells[posicion].Value = DateTime.Parse(_dr["FechaFin"].ToString()) == DateTime.Parse("01/01/1900") ? "En Curso" : DateTime.Parse(_dr["FechaFin"].ToString()).ToString("dd/MM/yyyy HH:mm");
                    TimeSpan time = TimeSpan.FromSeconds(double.Parse(_dr["DiasTranscurridosSegundos"].ToString()));
                    if (time.Ticks > 0)
                    {
                        string timeFormat = string.Format("{0:D2}days {1:D2}h:{2:D2}m",
                                        time.Days,
                                        time.Hours,
                                        time.Minutes);
                        dgvHistorico.Rows[1].Cells[posicion].Value = time.Ticks == 0 ? "En Curso" : timeFormat;
                    }

                    else
                        dgvHistorico.Rows[1].Cells[posicion].Value = "0";

                    //dgvHistorico.Rows[1].Cells[posicion].Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

                    posicion += 1;
                }
                TimeSpan totalTime = TimeSpan.FromSeconds(double.Parse(dtHistoricoPedido.Rows[0]["TiempoTotalTranscurridoSegundos"].ToString()));
                string totalTimeFormat = string.Format("{0:D2}days {1:D2}h:{2:D2}m",
                                       totalTime.Days,
                                       totalTime.Hours,
                                       totalTime.Minutes);
                dgvHistorico.Columns.Add("Col" + posicion.ToString(), "Total");
                dgvHistorico.Rows[0].Cells[posicion].Value = "";
                dgvHistorico.Rows[1].Cells[posicion].Value = totalTimeFormat;

            }
        }
        private DataTable getHistoricoPedido(String _Pedido)
        {
            DataTable dt = new DataTable();
            dt = ControlPedidos.getHistoricoPedido(_Pedido);
            return dt;
        }
        private void txtSolicitudNumero_TextChanged(object sender, EventArgs e)
        {
            if (bindingSourceSolicitudes.Position >= 0)
                CargaHistorico(this.Solicitudes.Rows[bindingSourceSolicitudes.Position]["SOLICITUD"].ToString());
        }
        private void SolicitudesSIPEvaluaHabilitarControlesCambios()
        {
            if (bindingSourceSolicitudes.Count > 0)
            {
                DataRowView row = (DataRowView)bindingSourceSolicitudes.Current;
                string status = row["ESTATUS"].ToString();
                if (status == "A")
                {
                    SolicitudesHabilitaControlesCambios(false);
                }
                else
                {
                    SolicitudesHabilitaControlesCambios(true);
                }
            }
            else
            {
                SolicitudesHabilitaControlesNavegacionParaPrimerRegistro(false);
                SolicitudesHabilitaControlesCambios(false);
                //PedidosSIPHabilitaControlesNavegacionParaPrimerRegistro(false);
                //PedidosSIPHabilitaControlesCambios(false);
                //lblPedSIPYaImpreso.Visible = false;
            }
        }

        #endregion


        private void frmFindClie_Load(object sender, EventArgs e)
        {
            try
            {

                DataSet dsCatalogosEspeciales = new DataSet();
                //CARGAMOS LOS CATALOGOS PARA SOLICITUDES ESPECIALES...
                dsCatalogosEspeciales = CatalogosSolicitudesEspeciales.getCatalogosEspeciales();

                DataTable dtCatalogoFormaPago = Catalogos.GetCatalogoFormaPago();
                DataTable dtCatalogoUsoCFDI = Catalogos.GetCatalogoUsoCFDI();
                DataTable dtCatalogoMetodoPago = Catalogos.GetCatalogoMetodoPago();
                dtCalogoFormaPagoComisiones = Catalogos.GetCatalogoFormaPagoComision();

                //SE CARGAN CATALOGOS 
                //***********************************FORMAS DE PAGO************************
                //PEDIDOS NORMALES
                cmbPedSIPFormaPago.DisplayMember = "Descripcion";
                cmbPedSIPFormaPago.ValueMember = "Clave";
                cmbPedSIPFormaPago.DataSource = dtCatalogoFormaPago;
                cmbPedSIPFormaPago.SelectedValue = "03";
                //PEDIDOS DAT
                cmbPedDATSIPFormaPago.DisplayMember = "Descripcion";
                cmbPedDATSIPFormaPago.ValueMember = "Clave";
                cmbPedDATSIPFormaPago.DataSource = dtCatalogoFormaPago;
                cmbPedDATSIPFormaPago.SelectedValue = "03";
                //PEDIDOS MOSTRADOR
                cmbPedMOSSIPFormaPago.DisplayMember = "Descripcion";
                cmbPedMOSSIPFormaPago.ValueMember = "Clave";
                cmbPedMOSSIPFormaPago.DataSource = dtCatalogoFormaPago;
                cmbPedMOSSIPFormaPago.SelectedValue = "03";
                cmbPedMOSCPFormaPago.DisplayMember = "Descripcion";
                cmbPedMOSCPFormaPago.ValueMember = "Clave";
                cmbPedMOSCPFormaPago.DataSource = dtCatalogoFormaPago;
                cmbPedMOSCPFormaPago.SelectedValue = "03";


                //***********************************USO DE CFDI************************
                //PEDIDOS NORMALES
                cmbPedSIPUsoCFDI.DisplayMember = "Descripcion";
                cmbPedSIPUsoCFDI.ValueMember = "Clave";
                cmbPedSIPUsoCFDI.DataSource = dtCatalogoUsoCFDI;
                cmbPedSIPUsoCFDI.SelectedValue = "G01";
                //PEDIDOS DAT
                cmbPedDATSIPUsoCFDI.DisplayMember = "Descripcion";
                cmbPedDATSIPUsoCFDI.ValueMember = "Clave";
                cmbPedDATSIPUsoCFDI.DataSource = dtCatalogoUsoCFDI;
                cmbPedDATSIPUsoCFDI.SelectedValue = "G01";
                //PEDIDOS MOSTRADOR
                cmbPedMOSSIPUsoCFDI.DisplayMember = "Descripcion";
                cmbPedMOSSIPUsoCFDI.ValueMember = "Clave";
                cmbPedMOSSIPUsoCFDI.DataSource = dtCatalogoUsoCFDI;
                cmbPedMOSSIPUsoCFDI.SelectedValue = "G01";
                cmbPedMOSCPUsoCFDI.DisplayMember = "Descripcion";
                cmbPedMOSCPUsoCFDI.ValueMember = "Clave";
                cmbPedMOSCPUsoCFDI.DataSource = dtCatalogoUsoCFDI;
                cmbPedMOSCPUsoCFDI.SelectedValue = "G01";
                //***********************************METODOS DE PAGO************************
                //PEDIDOS NORMALES
                cmbPedSIPMetodoPago.DisplayMember = "Descripcion";
                cmbPedSIPMetodoPago.ValueMember = "Clave";
                cmbPedSIPMetodoPago.DataSource = dtCatalogoMetodoPago;
                cmbPedSIPMetodoPago.SelectedValue = "PUE";
                //PEDIDOS DAT
                cmbPedDATSIPMetodoPago.DisplayMember = "Descripcion";
                cmbPedDATSIPMetodoPago.ValueMember = "Clave";
                cmbPedDATSIPMetodoPago.DataSource = dtCatalogoMetodoPago;
                cmbPedDATSIPMetodoPago.SelectedValue = "PUE";
                //PEDIDOS MOSTRADOR
                cmbPedMOSSIPMetodoPago.DisplayMember = "Descripcion";
                cmbPedMOSSIPMetodoPago.ValueMember = "Clave";
                cmbPedMOSSIPMetodoPago.DataSource = dtCatalogoMetodoPago;
                cmbPedMOSSIPMetodoPago.SelectedValue = "PUE";
                cmbPedMOSCPMetodoPago.DisplayMember = "Descripcion";
                cmbPedMOSCPMetodoPago.ValueMember = "Clave";
                cmbPedMOSCPMetodoPago.DataSource = dtCatalogoMetodoPago;
                cmbPedMOSCPMetodoPago.SelectedValue = "PUE";

                if (dsCatalogosEspeciales != null)
                {
                    if (dsCatalogosEspeciales.Tables.Count > 0)
                    {
                        //TIPO DE PRENDA
                        cmbSolicitudTipoPrenda.ValueMember = "Clave";
                        cmbSolicitudTipoPrenda.DisplayMember = "Descripcion";
                        cmbSolicitudTipoPrenda.DataSource = dsCatalogosEspeciales.Tables[0];
                        //COMPOSICION DE TELA
                        cmbSolicitudComposicionTela.ValueMember = "Clave";
                        cmbSolicitudComposicionTela.DisplayMember = "Descripcion";
                        cmbSolicitudComposicionTela.DataSource = dsCatalogosEspeciales.Tables[1];
                        //COLOR
                        cmbSolicitudColor.ValueMember = "Clave";
                        cmbSolicitudColor.DisplayMember = "Descripcion";
                        cmbSolicitudColor.DataSource = dsCatalogosEspeciales.Tables[2];
                        lblSolicitudOtrasEspecificaciones.Text = "";
                        //GENERO
                        cmbSolicitudGenero.ValueMember = "Clave";
                        cmbSolicitudGenero.DisplayMember = "Descripcion";
                        cmbSolicitudGenero.DataSource = dsCatalogosEspeciales.Tables[3];
                    }
                }
                HabilitaNuevoPedido();
            }
            catch { }
        }

        private void frmFindClie_Activated(object sender, EventArgs e)
        {
            if (!DatosCrgados)
            {
                tmrCargaDatos.Enabled = true;
                DatosCrgados = true;

            }
            if (frmReportes != null)
            {
                if (tipoPedido == Enumerados.TipoPedido.Pedido || tipoPedido == Enumerados.TipoPedido.PedidoEC || tipoPedido == Enumerados.TipoPedido.PedidoDAT || tipoPedido == Enumerados.TipoPedido.PedidoMOS || tipoPedido == Enumerados.TipoPedido.PedidoMOSCP)
                {
                    if (frmReportes.pedidoEstatusModificado)
                    {
                        if (bindingSourcePedidosSIP.Position >= 0)
                            pedidos.Rows[bindingSourcePedidosSIP.Position]["ESTATUS"] = "I";
                        if (bindingSourcePedidosDATSIP.Position >= 0)
                            pedidosDAT.Rows[bindingSourcePedidosDATSIP.Position]["ESTATUS"] = "I";
                        if (bindingSourcePedidosMOSSIP.Position >= 0)
                            pedidosMOS.Rows[bindingSourcePedidosMOSSIP.Position]["ESTATUS"] = "I";
                        if (bindingSourceSolicitudes.Position >= 0)
                            Solicitudes.Rows[bindingSourceSolicitudes.Position]["ESTATUS"] = "A";
                        PedidosDATSIPEvaluaHabilitarControlesCambios();
                        PedidosMOSSIPEvaluaHabilitarControlesCambios();
                        SolicitudesEvaluaHabilitarControlesCambios();
                    }
                }
                else if (tipoPedido == Enumerados.TipoPedido.OrdenTrabajo)
                {
                    if (frmReportes.pedidoEstatusModificado)
                    {

                        OrdenTrabajo.Rows[bindingSourceOrdenTrabSIP.Position]["ESTATUS"] = "I";
                        OrdenTrabHabilitaControlesCambios(false);
                    }
                }

            }
            if (frmAgregarPartidas != null)
            {
                if (frmAgregarPartidas.pedidoEstatusModificado)
                {
                    if (bindingSourcePedidosSIP.Position >= 0)
                        pedidos.Rows[bindingSourcePedidosSIP.Position]["ESTATUS"] = "I";
                    if (bindingSourcePedidosDATSIP.Position >= 0)
                        pedidosDAT.Rows[bindingSourcePedidosDATSIP.Position]["ESTATUS"] = "I";
                    if (bindingSourcePedidosMOSSIP.Position >= 0)
                        pedidosMOS.Rows[bindingSourcePedidosMOSSIP.Position]["ESTATUS"] = "I";
                    PedidosSIPEvaluaHabilitarControlesCambios();
                    PedidosDATSIPEvaluaHabilitarControlesCambios();
                    PedidosMOSSIPEvaluaHabilitarControlesCambios();
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            switch (e.TabPageIndex)
            {
                case 2:
                    tipoPedido = Enumerados.TipoPedido.Pedido;
                    this.WindowState = FormWindowState.Normal;
                    break;
                case 3:
                    this.WindowState = FormWindowState.Maximized;
                    break;
                case 4:
                    tipoPedido = Enumerados.TipoPedido.OrdenTrabajo;
                    this.WindowState = FormWindowState.Normal;
                    break;
                case 5:
                    tipoPedido = Enumerados.TipoPedido.PedidoDAT;
                    this.WindowState = FormWindowState.Normal;
                    break;
                case 7:
                    tipoPedido = Enumerados.TipoPedido.PedidoMOS;
                    this.WindowState = FormWindowState.Normal;
                    break;
                case 8:
                    tipoPedido = Enumerados.TipoPedido.PedidoMOSCP;
                    this.WindowState = FormWindowState.Normal;
                    break;
                default:
                    tipoPedido = Enumerados.TipoPedido.NoAplica;
                    this.WindowState = FormWindowState.Normal;
                    break;
            }

        }

        private void bindingNavigatorMoveFirstItem1_Click(object sender, EventArgs e)
        {
            OrdenTrabEvaluaHabilitarControlesCambios();
        }

        private void bindingNavigatorMovePreviousItem1_Click(object sender, EventArgs e)
        {
            OrdenTrabEvaluaHabilitarControlesCambios();
        }

        private void bindingNavigatorMoveNextItem1_Click(object sender, EventArgs e)
        {
            OrdenTrabEvaluaHabilitarControlesCambios();
        }

        private void bindingNavigatorMoveLastItem1_Click(object sender, EventArgs e)
        {
            OrdenTrabEvaluaHabilitarControlesCambios();
        }

        private void txtLogoCodCatalogo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string[] datos = txtLogoCodCatalogo.Text.Split('-');
                if (datos.Length == 3)
                {
                    if (!string.IsNullOrEmpty(datos[1]))
                    {
                        lblSigConsecutivo.Text = string.Format("<--[ {0} ] Siguiente consecutivo para: \"{1}\"", SiguienteConsecutivo(datos[1].ToUpper()).ToString("00"), datos[1].ToUpper());
                    }
                }
                else
                {
                    lblSigConsecutivo.Text = "<-- ??";
                }
            }
            catch (Exception Ex)
            {
                lblSigConsecutivo.Text = "<-- ??";
            }

        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {

            if (!accesosTabs[e.TabPageIndex])
            {
                if (this.status != "A")
                    MessageBox.Show("El Cliente no está Activo, por lo tanto no se permite el accesos a esta opción.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Solicite acceso al administrador del Sistema", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }

        }

        private void toolStripBtnBuscar_Click(object sender, EventArgs e)
        {
            frmInputBox frmInputBox = new SIP.frmInputBox(Enumerados.TipoCajaTextoInputBox.Numerica);

            frmInputBox.lblTitulo.Text = "Escriba el número de pedido";
            frmInputBox.Text = "Búsqueda de pedido";

            if (frmInputBox.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                int pos = bindingSourcePedidosSIP.Find("Pedido", frmInputBox.NTxtOrden.Text);
                if (pos != -1)
                {
                    tmrTextBoxHilight = new Timer();
                    tmrTextBoxHilight.Tick += tmrTextBoxHilight_Tick;
                    tmrTextBoxHilight.Interval = 250;
                    tmrTextBoxHilight.Enabled = true;
                    bindingSourcePedidosSIP.Position = pos;
                }
                else
                {
                    MessageBox.Show("No se encontró número de pedido", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    toolStripBtnBuscar_Click(sender, e);
                }
            }

        }

        private int tmrTxtHiLightCont = 0;
        private int tmrTxtHiLightTot = 8;
        void tmrTextBoxHilight_Tick(object sender, EventArgs e)
        {
            if (tmrTxtHiLightCont <= tmrTxtHiLightTot)
            {
                if (tmrTxtHiLightCont % 2 == 0)
                {
                    txtPedSIPNo.BackColor = Color.Yellow;
                    txtPedSIPNo.BorderStyle = BorderStyle.FixedSingle;
                    txtPedSIPNo.Font = new Font(txtPedSIPNo.Font.FontFamily, txtPedSIPNo.Font.Size, FontStyle.Bold);
                }
                else
                {
                    txtPedSIPNo.BackColor = SystemColors.Control;
                    txtPedSIPNo.BorderStyle = BorderStyle.Fixed3D;
                    txtPedSIPNo.Font = new Font(txtPedSIPNo.Font.FontFamily, txtPedSIPNo.Font.Size, FontStyle.Regular);
                }
                tmrTxtHiLightCont++;
            }
            if (tmrTxtHiLightCont == tmrTxtHiLightTot)
            {
                txtPedSIPNo.BackColor = SystemColors.Control;
                txtPedSIPNo.BorderStyle = BorderStyle.Fixed3D;
                txtPedSIPNo.Font = new Font(txtPedSIPNo.Font.FontFamily, txtPedSIPNo.Font.Size, FontStyle.Regular);
                tmrTxtHiLightCont = 0;
                tmrTextBoxHilight.Enabled = false;

            }
        }

        private void toolStripBtnSolicitudesGuardar_Click(object sender, EventArgs e)
        {
            if (!GeneralesExistenDatosVacios(txtSolicitudesAValidar))
                if (!GeneralesExistenDatosVacios(cmbSolicitudesAValidar))
                {

                    string resultado = "";
                    if (this.txtSolicitudNumero.Text != "")
                    {
                        resultado = SolicitudesModificar();
                    }
                    else
                    {
                        resultado = SolicitudesGuardar();
                    }

                    if (resultado == "")
                    {
                        SolicitudesHabilitaControlesNavegacion(true);
                        SolicitudesHabilitaControlesCambios(false);
                        solicitudNueva = true;
                        txtSolicitudObservaciones.Text = "";

                    }
                    else
                    {
                        MessageBox.Show("Se presentó el siguiente error \\n" + resultado, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
        }
        private void toolStripBtnSolicitudesNueva_Click(object sender, EventArgs e)
        {
            solicitudNueva = true;

            DataRow nueva_solicitud = Solicitudes.NewRow();
            Solicitudes.Rows.Add(nueva_solicitud);

            nueva_solicitud["FECHA"] = DateTime.Now;
            nueva_solicitud["AGENTE"] = Agente;
            nueva_solicitud["CLIENTE"] = clave_cliente;
            nueva_solicitud["PRENDAS_LINEA"] = 0;
            nueva_solicitud["PRENDAS_ESP"] = 0;
            nueva_solicitud["TIPO_PRENDA"] = "";
            nueva_solicitud["COMPOSICION_TELA"] = "";
            nueva_solicitud["COLOR"] = "";
            nueva_solicitud["ESPECIFICACIONES"] = "";
            nueva_solicitud["TALLAS"] = "";
            nueva_solicitud["GENERO"] = "";
            nueva_solicitud["PLAZO_ENTREGA"] = 0;
            nueva_solicitud["PRECIO_ASIGNADO"] = 0;
            nueva_solicitud["CODIGO_COTIZACION"] = "";
            nueva_solicitud["CODIGOS_ASIGNADOS"] = "";
            nueva_solicitud["ESTATUS"] = "A";
            txtSolicitudObservaciones.Text = "";
            lblSolicitudOtrasEspecificaciones.Text = "";
            this._otroTipoPrendaInidicado = false;
            this._otroTipoComposicionInidicado = false;
            this._otroTipoColorInidicado = false;



            bindingSourceSolicitudes.MoveLast();

            SolicitudesHabilitaControlesNavegacion(false);
            SolicitudesHabilitaControlesCambios(true);
            SolicitudesHabilitaControlesNavegacionParaPrimerRegistro(true);
            txtSolicitudNoPrendasLinea.Focus();
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

        private void cmbSolicitudTipoPrenda_Leave(object sender, EventArgs e)
        {
            //VALIDAMOS QUE EL VALOR INTRODUCIDO ESTE DENTRO DE LA LISTA
            if (cmbSolicitudTipoPrenda.FindString(cmbSolicitudTipoPrenda.Text.Trim()) == -1)
            {
                MessageBox.Show("Debes seleccionar un tipo válido dentro de la lista de selección.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _otroTipoPrendaInidicado = false;
                cmbSolicitudTipoPrenda.Focus();

            }
            else
            {
                if (cmbSolicitudTipoPrenda.Text.Trim().ToUpper() == "OTRO" && !_otroTipoPrendaInidicado)
                {
                    f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto, true);
                    f.Text = "OBSERVACIONES SIP";
                    f.lblTitulo.Text = "Introduce el tipo de prenda: ";
                    f.ShowDialog();
                    _observaciones = f.txtOrden.Text.Trim();
                    if (_observaciones == "")
                    {
                        MessageBox.Show("No se puede continuar con la solicitud, se debe de indicar el tipo de prenda", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbSolicitudTipoPrenda.SelectedIndex = -1;
                        _otroTipoPrendaInidicado = false;
                        cmbSolicitudTipoPrenda.Focus();
                    }
                    else
                    {
                        cmbSolicitudTipoPrenda.Text = cmbSolicitudTipoPrenda.Text.Trim().ToUpper();
                        lblSolicitudOtrasEspecificaciones.Text += " Otro tipo de prenda: " + _observaciones;
                        _otroTipoPrendaInidicado = true;
                    }

                }
            }
        }

        private void cmbSolicitudComposicionTela_Leave(object sender, EventArgs e)
        {
            //VALIDAMOS QUE EL VALOR INTRODUCIDO ESTE DENTRO DE LA LISTA

            if (cmbSolicitudComposicionTela.FindString(cmbSolicitudComposicionTela.Text.Trim()) == -1)
            {
                MessageBox.Show("Debes seleccionar un tipo válido dentro de la lista de selección.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _otroTipoComposicionInidicado = false;
                cmbSolicitudComposicionTela.Focus();
            }
            else
            {
                if (cmbSolicitudComposicionTela.Text.Trim().ToUpper() == "OTRO" && !_otroTipoComposicionInidicado)
                {
                    f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto, true);
                    f.Text = "OBSERVACIONES SIP";
                    f.lblTitulo.Text = "Introduce la composición de la tela: ";
                    f.ShowDialog();
                    _observaciones = f.txtOrden.Text.Trim();
                    if (_observaciones == "")
                    {
                        MessageBox.Show("No se puede continuar con la solicitud, se debe de indicar la composición de la tela.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbSolicitudComposicionTela.SelectedIndex = -1;
                        _otroTipoComposicionInidicado = false;
                        cmbSolicitudComposicionTela.Focus();
                    }
                    else
                    {
                        cmbSolicitudComposicionTela.Text = cmbSolicitudComposicionTela.Text.Trim().ToUpper();
                        lblSolicitudOtrasEspecificaciones.Text += " Otro tipo de Composicion: " + _observaciones;
                        _otroTipoComposicionInidicado = true;
                    }

                }
            }
        }

        private void cmbSolicitudColor_Leave(object sender, EventArgs e)
        {
            //VALIDAMOS QUE EL VALOR INTRODUCIDO ESTE DENTRO DE LA LISTA
            if (cmbSolicitudColor.FindString(cmbSolicitudColor.Text.Trim()) == -1)
            {
                MessageBox.Show("Debes seleccionar un tipo válido dentro de la lista de selección.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _otroTipoColorInidicado = false;
                cmbSolicitudColor.Focus();
            }
            else
            {
                if (cmbSolicitudColor.Text.Trim().ToUpper() == "OTRO" && !_otroTipoColorInidicado)
                {
                    f = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto, true);
                    f.Text = "OBSERVACIONES SIP";
                    f.lblTitulo.Text = "Introduce el color: ";
                    f.ShowDialog();
                    _observaciones = f.txtOrden.Text.Trim();
                    if (_observaciones == "")
                    {
                        MessageBox.Show("No se puede continuar con la solicitud, se debe de indicar la composición de la tela.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbSolicitudColor.SelectedIndex = -1;
                        _otroTipoColorInidicado = false;
                        cmbSolicitudColor.Focus();
                    }
                    else
                    {
                        cmbSolicitudColor.Text = cmbSolicitudColor.Text.Trim().ToUpper();
                        lblSolicitudOtrasEspecificaciones.Text += " Otro tipo de color: " + _observaciones;
                        _otroTipoColorInidicado = true;
                    }

                }
            }
        }

        private void bindingNavigatorMoveNextItem2_Click(object sender, EventArgs e)
        {
            SolicitudesSIPEvaluaHabilitarControlesCambios();
        }

        private void cmbPedMOSSIPFormaPago_SelectedValueChanged(object sender, EventArgs e)
        {
            DataRow[] busqueda;
            if (cmbPedMOSSIPFormaPago.SelectedValue != null)
            {
                try
                {
                    this.formaPagoComision = this.dtCalogoFormaPagoComisiones.Select("clave = " + cmbPedMOSSIPFormaPago.SelectedValue).Select(x => x.Field<decimal>("comision")).FirstOrDefault();
                }
                catch { }
            }
        }

        private void lblPedSIPEcommerce_TextChanged(object sender, EventArgs e)
        {
            if (lblPedSIPEcommerce.Text == "EC")
                chkPedSIPEcommerce.Checked = true;
            else
                chkPedSIPEcommerce.Checked = false;
        }

        private void btnPedSIPDocumentos_Click(object sender, EventArgs e)
        {
            frmDocumentosElectronicos btnPedSIPImprimir = new frmDocumentosElectronicos(Convert.ToInt32(txtPedSIPNo.Text));
            btnPedSIPImprimir.Show();
        }

        private void btnPedDATSIPDocumentos_Click(object sender, EventArgs e)
        {
            frmDocumentosElectronicos btnPedSIPImprimir = new frmDocumentosElectronicos(Convert.ToInt32(txtPedDATSIPNo.Text));
            btnPedSIPImprimir.Show();
        }

        private void btnPedMOSSIPDocumentos_Click(object sender, EventArgs e)
        {
            frmDocumentosElectronicos btnPedSIPImprimir = new frmDocumentosElectronicos(Convert.ToInt32(txtPedMOSSIPNo.Text));
            btnPedSIPImprimir.Show();
        }

        private void bindingNavigatorMovePreviousItem2_Click(object sender, EventArgs e)
        {
            SolicitudesSIPEvaluaHabilitarControlesCambios();
        }

        private void bindingNavigatorMoveFirstItem2_Click(object sender, EventArgs e)
        {
            SolicitudesSIPEvaluaHabilitarControlesCambios();
        }

        private void bindingNavigatorMoveLastItem2_Click(object sender, EventArgs e)
        {
            SolicitudesSIPEvaluaHabilitarControlesCambios();
        }

        private void lblPedSIPEcommerce_Click(object sender, EventArgs e)
        {

        }

        private void btnPedMOSCPDocumentos_Click(object sender, EventArgs e)
        {
            frmDocumentosElectronicos btnPedSIPImprimir = new frmDocumentosElectronicos(Convert.ToInt32(txtPedMOSCPNo.Text));
            btnPedSIPImprimir.Show();
        }

        private void HabilitaNuevoPedido()
        {
            if (Globales.UsuarioActual.UsuarioUsuario.Trim().ToUpper() == "MOSTRADOR")
            {
                toolStripBtnPedidosDATSIPNuevo.Enabled = this.clasificacionVendedor == "F";
                toolStripBtnPedidosMOSSIPNuevo.Enabled = this.clasificacionVendedor == "F";
                toolStripBtnPedidosMOSCPNuevo.Enabled = this.clasificacionVendedor == "F";
                toolStripBtnPedidosSIPNuevo.Enabled = this.clasificacionVendedor == "F";
            }
        }

        private void toolStripPedidosRefresh_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            PedidosVirtualesSIPLlenaDatos(clave_cliente);
            PedidosSIPLlenaDatos(clave_cliente, true);
            Cursor.Current = Cursors.Default;
        }

        private void toolStripPedidoDATRefresh_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            PedidosVirtualesSIPLlenaDatos(clave_cliente);
            PedidosDATSIPLlenaDatos(clave_cliente, true);
            PedidosSIPLlenaDatos(clave_cliente, true);
            Cursor.Current = Cursors.Default;
        }

        private void toolStripPedidosMOSRefresh_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            PedidosVirtualesSIPLlenaDatos(clave_cliente);
            PedidosMOSSIPLlenaDatos(clave_cliente, true);
            PedidosSIPLlenaDatos(clave_cliente, true);
            Cursor.Current = Cursors.Default;
        }

        private void toolStripPedidosMOSCPRefresh_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            PedidosMOSCPLlenaDatos(clave_cliente, true);
            PedidosSIPLlenaDatos(clave_cliente, true);
            PedidosVirtualesSIPLlenaDatos(clave_cliente);
            Cursor.Current = Cursors.Default;
        }
    }
}
