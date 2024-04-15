using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIP.Properties;
using SIP.Utiles;
using ulp_bl;
using ulp_bl.Reportes;
using ulp_bl.Utiles;
using ulp_bl.Permisos;
using System.IO;

namespace SIP
{
    public partial class frmControlPanel : Form
    {
        private delegate void DelAsignaAutoCompletarATextBox(AutoCompleteStringCollection AutoCompleteStringCollection);
        DataTable dtMenus = new DataTable();
        Utilerias utilerias = new Utilerias();
        private bool clientesCargados;
        private Timer tmrCheckForUpdates = new Timer();
        private BackgroundWorker bgwCheckForUpdates;
        NotifyIcon notifyIcon1 = new NotifyIcon();
        public frmControlPanel()
        {
            try
            {
                InitializeComponent();
                txtCliente.Enabled = false;
                txtCliente.Text = "Cargando clientes...";
                //BackgroundWorker backGroundWorkerClientes = new BackgroundWorker();
                //backGroundWorkerClientes.RunWorkerCompleted += backGroundWorkerClientes_RunWorkerCompleted;
                //backGroundWorkerClientes.DoWork += backGroundWorkerClientes_DoWork;
                //backGroundWorkerClientes.RunWorkerAsync();
                AsignaGlobales();


                BackgroundWorker backGroundWorkerDDLUpdates = new BackgroundWorker();
                backGroundWorkerDDLUpdates.DoWork += backGroundWorkerDDLUpdates_DoWork;
                backGroundWorkerDDLUpdates.RunWorkerAsync();

                iniSesion frmIniSesion = new iniSesion();
                frmIniSesion.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al inicar la aplicación: " + ex.Message, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void backGroundWorkerDDLUpdates_DoWork(object sender, DoWorkEventArgs e)
        {
            SIPDDLUpdates.Run();
            Precarga.PrecargaCrystalReports();
        }

        void backGroundWorkerClientes_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            txtCliente.Enabled = true;
            txtCliente.Text = "";
            //txtCliente.Focus();
            bool puedeBuscarClientes = PermisosUsuarioEspeciales.TienePermisos(Globales.UsuarioActual.Id, 6, 9);
            if (puedeBuscarClientes)
            {
                btnBuscarCliente.Enabled = true;
            }

        }

        void backGroundWorkerClientes_DoWork(object sender, DoWorkEventArgs e)
        {
            //   CargaClientes();
        }
        /// <summary>
        /// Asigna variables globales en las capas de presentación y negocios
        /// </summary>
        private void AsignaGlobales()
        {

            Globales.ConnectionStringSae80 = ConfigurationManager.ConnectionStrings["aspel_sae80"].ToString();


            GlobalesUI.MainForm = this;
            GlobalesUI.autoCompleteLogosCollection.AddRange(IMAGENES.RegresaListaDeLogosCliente().ToArray());
            Globales.ConnectionString = ConnectionInfo.ConnectionString();
            Globales.DataSource = ConnectionInfo.DataSource();

        }
        private void frmControlPanel_Load(object sender, EventArgs e)
        {
            //Se ejecutan actualizacines de DDL si son necesarias

            //Se asigna título de la pantalla
            Text = string.Format("Panel de Control - Conectado a - {0}", Globales.DataSource);
            AppInfo.RutaApp = System.Reflection.Assembly.GetExecutingAssembly().Location;
            lblVer.Text = AppInfo.VersionCompleta;
            this.Left = 60;
            this.Top = 60;
            tmrCheckForUpdates.Tick += tmrCheckForUpdates_Tick;
            tmrCheckForUpdates.Interval = AppInfo.IntervaloChequeoActualizaciones;
            tmrCheckForUpdates.Enabled = AppInfo.ChecarActualizaciones;

        }

        void tmrCheckForUpdates_Tick(object sender, EventArgs e)
        {
            VersionManager vm = new VersionManager();
            vm.OnCheckForUpdateComplete += vm_OnCheckForUpdateComplete;
            vm.CheckForUpdates(AppInfo.UrlParaActualizaciones);
        }
        void vm_OnCheckForUpdateComplete(PublishedVersion PublishedVersion)
        {
            if (PublishedVersion != null)
            {
                System.Diagnostics.FileVersionInfo appInfo =
                    System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);


                Version publishedVersion = new Version(PublishedVersion.NewVersionNumber);

                string localVersion = appInfo.FileVersion;

                if (publishedVersion.CompareTo(new Version(localVersion)) != 0)
                {
                    notifyIcon1.Icon = SystemIcons.Information;
                    notifyIcon1.BalloonTipTitle = "SIP.Net " + publishedVersion.ToString();
                    notifyIcon1.BalloonTipText = "Existe una actualizacíon del sistema SIP";
                    notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                    notifyIcon1.Visible = true;
                    notifyIcon1.ShowBalloonTip(3000);
                    System.Diagnostics.Debug.WriteLine("Existe una actualización!");
                }
            }

        }
        /// <summary>
        /// Se provoca que el panel de control sea invisible en su primera carga
        /// ya que el panel de control es el formulario principal de la aplicación
        /// </summary>
        /// <param name="value"></param>
        protected override void SetVisibleCore(bool value)
        {
            if (!this.IsHandleCreated)
            {
                this.CreateHandle();
                value = false;   // Prevent window from becoming visible
            }
            base.SetVisibleCore(value);
        }
        /// <summary>
        /// Esta subrutina se encarga de cargar los menús en el panel de control
        /// según los permisos del usuario
        /// </summary>
        public void CargaMenus(List<int> Permisos)
        {
            List<int> iPermisos = new List<int>();
            for (int i = 0; i < 89; i++)
            {
                iPermisos.Add(i);
            }
            /*
            /*
             * Estos permisos se cargan a mano con proósitos de prueba
             
            iPermisos.Add(1);
            iPermisos.Add(4);
            iPermisos.Add(61);
            iPermisos.Add(81);
            iPermisos.Add(90);
             
            utilerias.Permisos = iPermisos;             
             * */
            utilerias.Permisos = Permisos;
            dtMenus = utilerias.Menus();
            DataRow[] drMenuPrincipal = dtMenus.Select(string.Format("MenuOrigen={0}", "0"), "OrdenMenu");
            llenaMenus(drMenuPrincipal, null);
            AgregaOpcionActualizar();
        }

        private void AgregaOpcionActualizar()
        {
            ToolStripMenuItem mnuRefreshBtn = new ToolStripMenuItem();
            mnuRefreshBtn.DisplayStyle = ToolStripItemDisplayStyle.Image;
            mnuRefreshBtn.Image = Resources.refresh;
            mnuRefreshBtn.Name = "mnuActualizarPermisos";
            mnuRefreshBtn.ToolTipText = "Actualiza el menú de opciones según los permisos de usuario";
            mnuRefreshBtn.Tag = 99999;
            mnuRefreshBtn.Click += item_Click;
            menuPrincipal.Items.Add(mnuRefreshBtn);
        }
        /// <summary>
        /// Rutina recursiva que se encarga de llenar los menus a cualquier
        /// nivel de profundidad
        /// </summary>
        /// <param name="drMenus"></param>
        /// <param name="sMenuPadre"></param>
        void llenaMenus(DataRow[] drMenus, ToolStripMenuItem sMenuPadre)
        {
            for (int i = 0; i < drMenus.Length; i++)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(drMenus[i]["Descripcion"].ToString());
                item.Click += item_Click;
                int iId = Convert.ToInt32(drMenus[i]["ID"].ToString());
                item.Tag = iId;
                //item.Owner = sMenuPadre.Owner;               
                item.Enabled = Convert.ToBoolean(drMenus[i]["Habilitar"].ToString());
                if (drMenus[i]["MenuOrigen"].ToString() == "0")
                {
                    menuPrincipal.Items.Add(item);
                }
                else
                {
                    sMenuPadre.DropDownItems.Add(item);
                }

                DataRow[] drHijos = dtMenus.Select(string.Format("MenuOrigen={0}", iId));
                if (drHijos.Length > 0)
                {
                    llenaMenus(drHijos, item);
                }
            }
        }
        /// <summary>
        /// Manejador genérico del evento de Click de menú del Panel de Control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void item_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem itemClicked = (ToolStripMenuItem)sender;
            System.Diagnostics.Debug.Flush();
            //if (Convert.ToInt32(itemClicked.Tag) != 5)            
            //System.Diagnostics.Debugger.Break();
            string txt = string.Format("ID:{0}, texto: {1}", itemClicked.Tag, itemClicked.Text);
            System.Diagnostics.Debug.WriteLine(txt);




            switch (Convert.ToInt32(itemClicked.Tag))
            {
                //Permisos de usuario

                case 2:
                    //System.Diagnostics.Process.Start(Application.ExecutablePath);
                    //Application.ExitThread();
                    break;

                case 3:
                    //Información de este equipo
                    frmAboutThisPC frmAbout = new frmAboutThisPC();
                    frmAbout.Show();
                    break;
                case 4:
                    //Salir
                    Close();
                    break;
                case 6:
                    //Localizar Cliente 
                    CargaClientes();
                    frmBusquedaGenerica frmBusqueda = new frmBusquedaGenerica(ulp_bl.Globales.tablaclientes, "CLAVE", "LOCALIZACIÓN DE CLIENTES");
                    frmBusqueda.ShowDialog();
                    if (frmBusqueda.RenglonSeleccionado != null)
                    {

                        frmFindClie frmFindClie = new frmFindClie(frmBusqueda.RenglonSeleccionado["Clave"].ToString().Trim());
                        frmFindClie.Show();

                    }
                    break;
                case 9:
                    //Reporte de folios anteriores
                    frmRepFolAnt frmRepFolAnt = new frmRepFolAnt(Enumerados.AreasEmpresa.Ventas);
                    frmRepFolAnt.Show();
                    break;
                case 10:
                    //Reporte de folios nuevos
                    frmRepFolNuev frmRepFolNuev = new frmRepFolNuev(Enumerados.AreasEmpresa.Ventas);
                    frmRepFolNuev.Show();
                    break;
                case 12:
                    //Reporte de existencias
                    frmRepExist frmRepExist = new frmRepExist();
                    frmRepExist.Show();
                    break;
                case 13:
                    //Reporte de Ventas en pesos y prendas por agente
                    frmRepVentPesosPrendas RepVentPesosPrendas = new frmRepVentPesosPrendas();
                    RepVentPesosPrendas.Show();
                    break;
                case 14:
                    //Reporte de pedidos con mas de 20 días
                    frmRepPed20Dias RepPed20Dias = new frmRepPed20Dias();
                    RepPed20Dias.Show();
                    break;
                case 15:
                    //Emisión de cartera
                    frmEmitirCartera frmEmitirCartera = new frmEmitirCartera();
                    frmEmitirCartera.Show();
                    break;
                case 16:
                    //Reporte de Prónosticos de Cobranza
                    frmRepPromCobra RepPronCob = new frmRepPromCobra();
                    RepPronCob.Show();
                    break;
                case 17:
                    //Reporte Cliente Nuevos / Recuperados
                    frmRepClieNuevRecu frmRepClieNueRecu = new frmRepClieNuevRecu();
                    frmRepClieNueRecu.Show();
                    break;
                case 18:
                    //Reporte Staff
                    frmRepStaff frmRepStaff = new frmRepStaff();
                    frmRepStaff.Show();
                    break;
                case 19:
                    //Reporte de Participación de mercado
                    frmRepPartArt RepPartArt = new frmRepPartArt();
                    RepPartArt.Show();
                    break;
                case 20:
                    //Reporte de órdenes de Producción                    
                    frmRepOrdProd RepOrdProd = new frmRepOrdProd(Enumerados.TipoOrdenProduccion.Liberada);
                    RepOrdProd.Show();
                    break;
                case 21:
                    frmDesempByArea frmDesempByArea = new frmDesempByArea();
                    frmDesempByArea.Show();
                    break;
                case 22:
                    //Reporte Análisis de Fechas de entrega y desempeño por área
                    frmRepDesempByArea frmRepDesempByArea = new frmRepDesempByArea();
                    frmRepDesempByArea.Show();
                    break;
                case 24:
                    //Captura de costos de procesos
                    frmCaptCostProcsInput frmCCPI = new frmCaptCostProcsInput();
                    frmCCPI.Show();
                    break;
                case 25:
                    //Captura de facturas de proveedores de Fletes
                    frmCapFactProvFlete frmCapProvFlete = new frmCapFactProvFlete();
                    frmCapProvFlete.Show();
                    break;
                case 26:
                    //Reporte Fletes
                    frmRepCostoVsPrecFlete frmRepCostoVsPrecFlete = new frmRepCostoVsPrecFlete();
                    frmRepCostoVsPrecFlete.Show();
                    break;
                case 27:
                    //Reporte Costura
                    frmRepCostoVsPrecCostura frmRepCostoVsPrecCostura = new frmRepCostoVsPrecCostura();
                    frmRepCostoVsPrecCostura.Show();
                    break;
                case 28:
                    frmRepCostoVsPrecEstampado frmRepCostoVsPrecEstampado = new frmRepCostoVsPrecEstampado();
                    frmRepCostoVsPrecEstampado.Show();
                    break;
                case 32:
                    //Capturar ruta de procesos
                    frmAsignarRuta frmAsignarRuta = new frmAsignarRuta();
                    frmAsignarRuta.Show();
                    break;
                case 33:
                    //Reporte carga por departamento
                    frmRepCargaxDepto frmRepCargaxDepto = new frmRepCargaxDepto();
                    frmRepCargaxDepto.Show();
                    break;
                case 34:
                    //Retroalimentación de mano de obra
                    /*frmRetroAlimManObra frmRetroAlimManObra = new frmRetroAlimManObra();
                    frmRetroAlimManObra.Show();*/

                    //MODIFICACION DE RETROALIMENTACION POR PEDIDO
                    frmRetroAlimManObraPedido frmRetroAlimManObraPedido = new frmRetroAlimManObraPedido();
                    frmRetroAlimManObraPedido.Show();

                    break;
                case 36:
                    //Captura facturas de maquila de bordado
                    frmMantenimientoPedidosBordado frmMPB = new frmMantenimientoPedidosBordado();
                    frmMPB.Show();
                    break;
                case 37:
                    //Reporte de costos de bordado
                    frmDatosFacturaBordado frmDatosFacturaBordado = new frmDatosFacturaBordado();
                    frmDatosFacturaBordado.Show();
                    break;
                case 39:
                    //Actualizar datos de embarques
                    frmEmbarques frmEmbarques = new frmEmbarques();
                    frmEmbarques.Show();
                    break;
                case 40:
                    //Reporte de universo de pedidos de embarques
                    frmRptUniPediEmq frmRptUniPediEmq = new frmRptUniPediEmq();
                    frmRptUniPediEmq.Show();
                    break;
                case 41:
                    //Alta de departamentos
                    frmAltaDeptos frmAltaDeptos = new frmAltaDeptos();
                    frmAltaDeptos.Show();
                    break;
                case 42:
                    //Etiquetas de empaque
                    frmEtiquetasEmpaque frmEtiquetasEmpaque = new frmEtiquetasEmpaque();
                    frmEtiquetasEmpaque.Show();
                    break;
                case 44:
                    //Capturar órdenes de producción                    
                    frmOrdProduccion frmOrdProduccion = new frmOrdProduccion();
                    frmOrdProduccion.Show();
                    break;
                case 45:
                    //Capturar órdenes de maquila
                    frmOrdMaquila frmOrdMaquila = new frmOrdMaquila();
                    frmOrdMaquila.Show();
                    break;
                case 46:
                    //Reporte de órdenes de Producción 2
                    frmRepOrdProd RepOrdProd2 = new frmRepOrdProd(Enumerados.TipoOrdenProduccion.Liberada);
                    RepOrdProd2.Show();
                    break;
                case 47:
                    frmRepOrdProd RepOrdProdNoLib = new frmRepOrdProd(Enumerados.TipoOrdenProduccion.NoLiberada);
                    RepOrdProdNoLib.Show();
                    break;
                case 48:
                    frmProcOrdenProcEntFrabrica ProcOrdenProcEntFrabrica = new frmProcOrdenProcEntFrabrica();
                    ProcOrdenProcEntFrabrica.Show();
                    break;
                case 50:
                    frmRecOrdProduccionMaquila2 frmRecOrdProduccionMaquila2 = new frmRecOrdProduccionMaquila2();
                    frmRecOrdProduccionMaquila2.Show();
                    break;
                case 51:
                    frmRepRecepcionMaquila frmRepRecepcionMaquila = new frmRepRecepcionMaquila();
                    frmRepRecepcionMaquila.Show();
                    break;

                case 52:
                    frmPrioridaddeMaquila frmPrioridaddeMaquila = new frmPrioridaddeMaquila();
                    frmPrioridaddeMaquila.Show();
                    break;
                case 54:
                    //Localizar Pedidos
                    frmBusquedaGenerica frmBusqueda1 = new frmBusquedaGenerica("LOCALIZACIÓN DE PEDIDOS", Enumerados.TipoBusqueda.Pedidos);
                    frmBusqueda1.OnItemSelected += frmBusqueda1_OnItemSelected;
                    frmBusqueda1.ShowDialog();
                    /*
					if (frmBusqueda1.RenglonSeleccionado != null)
                    {
                        frmUpPedidos upPedidos = new frmUpPedidos(frmBusqueda1.RenglonSeleccionado[1].ToString().Trim(), Convert.ToInt32(frmBusqueda1.RenglonSeleccionado[0].ToString().Trim()));
                        upPedidos.Show();
                    } 
                     * */
                    break;
                case 56:
                    frmBusquedaGenerica frmBusqueda2 = new frmBusquedaGenerica("Localización de Órdenes de Trabajo", Enumerados.TipoBusqueda.OrdenesTrabajo);
                    frmBusqueda2.OnItemSelected += frmBusqueda2_OnItemSelected;
                    frmBusqueda2.ShowDialog();

                    //if (frmBusqueda2.RenglonSeleccionado != null)
                    //{
                    //    int idPedido = Convert.ToInt32(frmBusqueda2.RenglonSeleccionado[0].ToString().Trim());
                    //    string clave_cliente = frmBusqueda2.RenglonSeleccionado[1].ToString().Trim();
                    //    Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.OrdenTrabajo, clave_cliente, 0, idPedido, Enumerados.TipoPedido.OrdenTrabajo);
                    //    frmReportes.Show();                                                
                    //}
                    break;
                case 60:
                    //Estandares de Pedidos
                    frmStandPedi frmStanPedi = new frmStandPedi();
                    frmStanPedi.ShowDialog();
                    break;
                case 66:
                    //Reporte de costeo de Producto Terminado
                    frmRepCostoUltimoPT frmRepCostoUltimoPT = new frmRepCostoUltimoPT();
                    frmRepCostoUltimoPT.show();
                    break;
                case 67:
                    //Reporte de costeo de Producto en Proceso a Ultimo Costo 
                    frmRepCostoUltimoPP frmRepCostoUltimoPP = new frmRepCostoUltimoPP();
                    frmRepCostoUltimoPP.show();
                    break;
                case 55:
                    frmActualizaPedido frmActualizaPedido = new frmActualizaPedido();
                    frmActualizaPedido.Show();
                    break;
                case 58:
                    //Capturar pedido en Aspel-SAE 5.0
                    frmCargaPedidosSAE frmCargaPedidosSAE = new frmCargaPedidosSAE();
                    frmCargaPedidosSAE.Show();
                    break;
                case 62:
                    frmSimuladorCostos frmSimuladorCostos = new frmSimuladorCostos(Enumerados.TipoSimulador.SimuladorDeCostos);
                    frmSimuladorCostos.Show();
                    break;
                case 64:
                    //Estructura de producto
                    frmSimuladorCostos frmEstructuraProducto = new frmSimuladorCostos(Enumerados.TipoSimulador.EstructuraDeProducto);
                    frmEstructuraProducto.Show();
                    break;
                case 69:
                    //Reconstrucción y costeo de inventario MP a una fecha
                    frmFiltroConta frmFiltroContaMP =
                        new frmFiltroConta(Enumerados.TipoReporteFiltroConta.RecYCosteoDeInvMP_SMO);
                    frmFiltroContaMP.Show();
                    break;
                case 70:
                    //Reconstrucción y costeo de inventario PP a una fecha
                    frmFiltroConta frmFiltroContaPP =
                        new frmFiltroConta(Enumerados.TipoReporteFiltroConta.RecYCosteoDeInvPP_SMO);
                    frmFiltroContaPP.Show();
                    break;
                case 71:
                    //Reconstrucción y costeo de inventario PP a una fecha
                    frmFiltroConta frmFiltroContaPT =
                        new frmFiltroConta(Enumerados.TipoReporteFiltroConta.RecYCosteoDeInvPT_SMO);
                    frmFiltroContaPT.Show();
                    break;
                case 72:
                    //Costo de lo vendido entre fechas
                    frmFiltroConta frmFiltroContaCVEF =
                        new frmFiltroConta(Enumerados.TipoReporteFiltroConta.CostoVendidoEntreFechas_SMO);
                    frmFiltroContaCVEF.Show();
                    break;
                case 75:
                    //Reconstrucción y costeo de inventario PT a una fecha (CMO)
                    frmFiltroConta frmFiltroContaPT_CMO = new frmFiltroConta(Enumerados.TipoReporteFiltroConta.RecYCosteoDeInvPT_CMO);
                    frmFiltroContaPT_CMO.Show();
                    break;
                case 76:
                    //Costo de lo vendido entre fechas
                    frmFiltroConta frmFiltroContaCVEF_CMO = new frmFiltroConta(Enumerados.TipoReporteFiltroConta.CostoVendidoEntreFechas_CMO);
                    frmFiltroContaCVEF_CMO.Show();
                    break;
                case 77:
                    //Reconstrucción y costeo mermas entre fechas (CMO)
                    frmFiltroConta frmFiltroContaRCM_CMO = new frmFiltroConta(Enumerados.TipoReporteFiltroConta.RecYCosteoDeMermas_CMO);
                    frmFiltroContaRCM_CMO.Show();
                    break;
                case 73:
                    //Reconstrucción y costeo mermas entre fechas
                    frmFiltroConta frmFiltroContaRCM = new frmFiltroConta(Enumerados.TipoReporteFiltroConta.RecYCosteoDeMermas_SMO);
                    frmFiltroContaRCM.Show();
                    break;
                case 85:
                    //Regenerar cartera viva
                    frmRegenerarCarteraViva frmRegenerarCarteraViva = new frmRegenerarCarteraViva();
                    frmRegenerarCarteraViva.Show();
                    break;
                case 90:
                    //Asignación de permisos
                    frmPermisos frmPermisos = new frmPermisos();
                    frmPermisos.Show();
                    break;
                case 82:
                    //Elimina Pedidos Temporales
                    frmEliminarPedidosTemporales frmEliminarPedidosTemporales = new frmEliminarPedidosTemporales();
                    frmEliminarPedidosTemporales.Show();
                    break;
                case 84:
                    //Cancela Pedidos 
                    frmCancelaPedido frmCancelaPedido = new frmCancelaPedido();
                    frmCancelaPedido.Show();
                    break;
                case 88:
                    //Alta de usuarios
                    frmAltaAgente frmAltaAgente = new frmAltaAgente();
                    frmAltaAgente.Show();
                    break;
                case 91:
                    //Exporta UpPedidos a Excel fecha SIP
                    frmRepExportaUpPedidos frmExportaUpPedidos = new frmRepExportaUpPedidos();
                    frmExportaUpPedidos.Show();
                    break;
                case 92:
                    //Exporta UpPedidos a Excel fecha SAE
                    frmRepExportaUpPedidosSAE frmExportaUpPedidosSAE = new frmRepExportaUpPedidosSAE();
                    frmExportaUpPedidosSAE.Show();
                    break;
                case 93:
                    //Reporte de existencias por almacén
                    frmTransferenciaXModelo frmTransferenciaXModelo = new frmTransferenciaXModelo();
                    frmTransferenciaXModelo.Show();
                    break;
                case 94:
                    frmTransferenciaXPedido frmTransferenciaXPedido = new frmTransferenciaXPedido();
                    frmTransferenciaXPedido.Show();
                    break;
                case 95:
                    //Reporte de existencias por almacén
                    frmRepExisXAlm frmRepExisXAlm = new frmRepExisXAlm();
                    frmRepExisXAlm.show();
                    break;
                case 79://Actualización de costos SIN mano de obra
                    if (MessageBox.Show("ALERTA!!! Proceso de actualizacion de costos sin considerar Mano de Obra \n\n ¿Realmente desea continuar?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        ActualizacionCostosSinManoObra.Actualiza(1);
                        MessageBox.Show("Paso 1 completado. Actualizacion de costos del catalogo de MP a la estructura del producto", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ActualizacionCostosSinManoObra.Actualiza(2);
                        MessageBox.Show("Paso 2 completado. Actualizacion de los costos de PT en base a la estructura, Terminada la actualizacion de costos", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MessageBox.Show("El proceso ha terminado", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("La acción ha sido cancelada por el usuario", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;
                case 80://Actualización de costos CON mano de obra
                    if (MessageBox.Show("ALERTA!!! Proceso de actualizacion de costos considerando Mano de Obra \n\n ¿Realmente desea continuar?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        ActualizacionCostosConManoObra.Actualiza(1);
                        MessageBox.Show("Paso 1 completado. Actualizacion de costos del catalogo de MP a la estructura del producto", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ActualizacionCostosConManoObra.Actualiza(2);
                        MessageBox.Show("Paso 2 completado. Actualizacion de los costos de PT en base a la estructura, Terminada la actualizacion de costos", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ActualizacionCostosConManoObra.Actualiza(3);
                        MessageBox.Show("Paso 3 terminado. Se actualizo el costo en MINV01 considerando la mano de obra", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MessageBox.Show("El proceso ha terminado", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("La acción ha sido cancelada por el usuario", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;
                case 83: //Eliminar / Habilitar pedido (Aspel-SAE/SIP)
                    //frmEliminarHabilitarPedidoAspelSaeSip eliminaPedido = new frmEliminarHabilitarPedidoAspelSaeSip();
                    frmEliminarHabilitarPedidos eliminaPedido = new frmEliminarHabilitarPedidos();
                    eliminaPedido.Show();
                    break;
                case 89:
                    frmDiasFestivos frmDiasFestivos = new frmDiasFestivos();
                    frmDiasFestivos.Show();
                    break;
                case 96:
                    //Duplicador de códigos de producto y estructuras
                    frmDupCodProdEstr frmDupCodProdEstr = new frmDupCodProdEstr();
                    frmDupCodProdEstr.Show();
                    break;
                case 97:
                    frmCapFactProvCostura frmCapFactProvCostura = new frmCapFactProvCostura();
                    frmCapFactProvCostura.Show();
                    break;
                case 98:
                    //Ajuste de 
                    frmAjusteCxC frmAjusteCxC = new frmAjusteCxC();
                    frmAjusteCxC.Show();
                    break;
                case 99:
                    //REPORTE DE SALIDAS DE ALMACEN 1
                    frmRepTransAlmacen1 frmRepTransAlmacen1 = new frmRepTransAlmacen1();
                    frmRepTransAlmacen1.Show();
                    break;
                case 100:
                    //CONFIGURACION DE DESCUENTOS Y PRECIOS SUGERIDOS
                    frmConfiguracionDescuentoSugerido frmConfiguracionDescuentoSugerido = new frmConfiguracionDescuentoSugerido();
                    frmConfiguracionDescuentoSugerido.Show();
                    break;
                case 101:
                    //REPORTE DE ARTÍCULOS POR CLIENTE
                    frmReporteArticulosCliente frmReporteArticulosCliente = new frmReporteArticulosCliente();
                    frmReporteArticulosCliente.Show();
                    break;
                case 102:
                    //REPORTE DE PEDIDOS DS Y CMP
                    frmRepPedidosDSyCMP frmRepPedidosDSyCMP = new frmRepPedidosDSyCMP();
                    frmRepPedidosDSyCMP.ShowDialog();
                    break;
                case 103:
                    //REPORTE DR VS DS
                    frmReporteClientesDSvsDR frmReporteClientesDSvsDR = new frmReporteClientesDSvsDR();
                    frmReporteClientesDSvsDR.ShowDialog();
                    break;
                case 104:
                    //CONTROL DE UPPEDIDOS
                    Form frmExists = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "frmControlPedidos").SingleOrDefault<Form>();
                    if (frmExists != null)
                    {
                        frmExists.WindowState = FormWindowState.Normal;
                        frmExists.BringToFront();
                    }
                    else
                    {
                        frmControlPedidos frmControlPedidos = new frmControlPedidos();
                        frmControlPedidos.Show();
                    }
                    break;
                case 105:
                    //GESTION DE CORPORATIVOS
                    frmCorporativos frmCorporativos = new frmCorporativos();
                    frmCorporativos.ShowDialog();
                    break;
                case 106:
                    //GESTION DE CORPORATIVOS
                    frmPreciosEspecialesCorporativo frmPreciosEspecialesCorporativo = new frmPreciosEspecialesCorporativo();
                    frmPreciosEspecialesCorporativo.ShowDialog();
                    break;
                case 107:
                    frmReporteGrupoCarso frmReporteGrupoCarso = new frmReporteGrupoCarso();
                    frmReporteGrupoCarso.Show();
                    break;
                case 108:
                    //Localizar Pedidos
                    frmBusquedaGenerica frmBusquedaPedidosAnticipados = new frmBusquedaGenerica("LOCALIZACIÓN DE PEDIDOS", Enumerados.TipoBusqueda.Pedidos);
                    frmBusquedaPedidosAnticipados.OnItemSelected += frmBusquedaAnticipo_OnItemSelected;
                    frmBusquedaPedidosAnticipados.ShowDialog();

                    break;
                case 109:
                    //Localizar Pedidos
                    frmBusquedaGenerica frmBusquedaPedidosApartado = new frmBusquedaGenerica("LOCALIZACIÓN DE PEDIDOS", Enumerados.TipoBusqueda.Pedidos);
                    frmBusquedaPedidosApartado.OnItemSelected += frmBusquedaApartado_OnItemSelected;
                    frmBusquedaPedidosApartado.ShowDialog();
                    break;

                case 110:
                    frmReporteComparativoRealVSLista frmReporteComparativoRealVSLista = new frmReporteComparativoRealVSLista();
                    frmReporteComparativoRealVSLista.ShowDialog();
                    break;
                case 111:
                    frmReporteAnalisisRetrasoOP frmReporteAnalisisRetrasoOP = new frmReporteAnalisisRetrasoOP();
                    frmReporteAnalisisRetrasoOP.ShowDialog();
                    break;
                case 112:
                    frmSimuladorCompras frmSimuladorCompras = new frmSimuladorCompras();
                    frmSimuladorCompras.ShowDialog();
                    break;
                case 113:
                    frmRecOrdProduccionMaquilaCodigoBarras frmRecOrdProduccionMaquilaCodigoBarras = new frmRecOrdProduccionMaquilaCodigoBarras();
                    frmRecOrdProduccionMaquilaCodigoBarras.ShowDialog();
                    break;

                case 114:
                    frmAltaClienteSAE frmAltaClienteSAE = new frmAltaClienteSAE();
                    frmAltaClienteSAE.ShowDialog();
                    break;
                case 115:
                    frmAltaBeneficiarioBanco frmAltaBeneficiarioBanco = new frmAltaBeneficiarioBanco();
                    frmAltaBeneficiarioBanco.ShowDialog();
                    break;
                case 116:
                    frmTransferenciaCodigoBarras frmTransferenciaCodigoBarras = new frmTransferenciaCodigoBarras();
                    frmTransferenciaCodigoBarras.ShowDialog();
                    break;
                case 117:
                    DataTable dtCodigos = new DataTable();
                    frmInputBox frmInput = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto, true);
                    frmInput.lblTitulo.Text = "Introduce la Orden de Producción";
                    frmInput.Text = "Reimpresión de Código de Barras";
                    frmInput.ShowDialog();
                    if (frmInput.txtOrden.Text.Trim() == "")
                    {
                        MessageBox.Show("No se ha introducido ninguna orden.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    dtCodigos = ulp_bl.RecOrdProduccionMaquilaCodigoBarras.ConsultaCodigoBarrasByOrdenMaquila(frmInput.txtOrden.Text.Trim());
                    if (dtCodigos.Rows.Count == 0)
                    {

                        if (MessageBox.Show("No existen Códigos de Barra para la orden solicitada. ¿Deseas que el Sistema intente regenerarlos?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            frmInputBox frmInputOrden = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto, true);
                            frmInputOrden.lblTitulo.Text = "Introduce la Orden de Compra";
                            frmInputOrden.ShowDialog();
                            String numeroOrden = frmInputOrden.txtOrden.Text.Trim();

                            DataTable dtTallas = RecOrdProduccionMaquilaCodigoBarras.ConsultaCodigoBarrasByOrdenMaquilaSAE(frmInput.txtOrden.Text.Trim(), numeroOrden);
                            if (dtTallas.Rows.Count == 0)
                            {
                                MessageBox.Show("No se encontró la Orden de Maquila en SAE, imposible regenerar los códigos de barra", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            else
                            {
                                List<CodigoBarra> ListaCodigos2 = generaCodigosDeBarra(frmInput.txtOrden.Text.Trim(), numeroOrden, dtTallas);
                                RecOrdProduccionMaquilaCodigoBarras.GuardaCodigoBarras(ListaCodigos2);
                                MessageBox.Show("Códigos regenerados de forma correcta, favor de reimprimir manualmente.", "SIP");
                                return;
                            }
                        }
                        else
                            return;

                    }
                    List<CodigoBarra> ListaCodigos = new List<CodigoBarra> { };
                    int contador = 1;
                    foreach (DataRow dr in dtCodigos.Rows)
                    {
                        ListaCodigos.Add(new CodigoBarra { UUID = dr["UUID"].ToString(), Consecutivo = (int)dr["Consecutivo"], Descripcion = dr["Descripcion"].ToString(), Cantidad = (int)dr["Cantidad"], Referencia = dr["Referencia"].ToString(), Contador = contador, Talla = dr["Talla"].ToString() });
                        contador++;
                    }
                    frmCodigoDeBarras frmCodigoDeBarras = new frmCodigoDeBarras("", 0, ListaCodigos);
                    frmCodigoDeBarras.ShowDialog();
                    //GENERAMOS EL EXCEL DE SALIDA
                    generaExcelCodigoBarra(ListaCodigos);
                    break;

                case 118: //REPORTE DE PROGRAMA DE EMPAQUE
                    frmReporteProgramaEmpaque frmReporteProgramaEmpaque = new frmReporteProgramaEmpaque();
                    frmReporteProgramaEmpaque.ShowDialog();
                    break;

                case 119: //FACTURACION DE PEDIDOS
                    frmFacturacionPedidos frmFacturacionPedidos = new frmFacturacionPedidos();
                    frmFacturacionPedidos.ShowDialog();
                    break;

                case 120: //ORDEN DE PRODUCCION ESPECIAL
                    frmOrdProduccionMasiva frmOrdProduccionMasiva = new frmOrdProduccionMasiva();
                    frmOrdProduccionMasiva.Show();
                    break;

                case 123: //CAPTURA DE BITACORA DE SEGUIMIENTO
                    frmBitacoraComentariosClientes frmBitacoraComentariosClientes = new frmBitacoraComentariosClientes();
                    frmBitacoraComentariosClientes.Show();
                    break;

                case 124: //REPORTE DE BITACORA DE SEGUIMIENTO
                    frmReporteComentarioClientes frmReporteComentarioClientes = new frmReporteComentarioClientes();
                    frmReporteComentarioClientes.Show();
                    break;

                case 126: //RECEPCION DE FACTURAS
                    frmRecepcionFacturas frmRecepcionFacturas = new frmRecepcionFacturas(true);
                    frmRecepcionFacturas.ShowDialog();
                    break;

                case 127: //REPORTE DE RECEPCION
                    frmReporteRecepcionFacturas frmReporteRecepcionFacturas = new frmReporteRecepcionFacturas();
                    frmReporteRecepcionFacturas.Show();
                    break;
                case 128: //REPORTE DE CLIENTES EXCEDENTES DE CRÉDITO
                    generaReporteClientesExcedentesCredito();
                    break;
                case 129: //CARGA DE FACTURAS PROVEEDOR PPD
                    frmCargaFacturasProveedor frmCargaFacturasProveedor = new frmCargaFacturasProveedor();
                    frmCargaFacturasProveedor.Show();
                    break;
                case 130: //REPORTE PPD
                    frmReportePPD frmReportePPD = new frmReportePPD();
                    frmReportePPD.ShowDialog();
                    break;
                case 131: //REPORTE DE FACTURACION
                    frmReporteFacturacionCredito frmReporteFacturacionCredito = new frmReporteFacturacionCredito();
                    frmReporteFacturacionCredito.ShowDialog();
                    break;
                case 132: //CAPTURA DE FACTURAS
                    frmRecepcionFacturas frmCapturaFacturas = new frmRecepcionFacturas(false);
                    frmCapturaFacturas.ShowDialog();
                    break;
                case 133: //REPORTE OP CON PROVEEDOR
                    frmRepOrdProdMaq frmRepOrdProdMaq = new frmRepOrdProdMaq();
                    frmRepOrdProdMaq.ShowDialog();
                    break;
                case 135:
                    //Reporte de folios anteriores
                    frmRepFolAnt frmRepFolAntContabilidad = new frmRepFolAnt(Enumerados.AreasEmpresa.Contabilidad);
                    frmRepFolAntContabilidad.Show();
                    break;
                case 136:
                    //Reporte de folios nuevos
                    frmRepFolNuev frmRepFolNuevContabilidad = new frmRepFolNuev(Enumerados.AreasEmpresa.Contabilidad);
                    frmRepFolNuevContabilidad.Show();
                    break;
                case 137:
                    // Permisos especiales Control Pedidos
                    frmPermisosEspecialesPedidos frmPermisosEspecialesPedidos = new frmPermisosEspecialesPedidos();
                    frmPermisosEspecialesPedidos.Show();
                    break;
                case 138: // NOTAS DE CRÉDITO
                    frmNotaCredito frmNotaCredito = new frmNotaCredito(TipoNC.GENERAR);
                    frmNotaCredito.Show();
                    break;
                case 139: // NOTAS DE CRÉDITO
                    frmNotaCredito = new frmNotaCredito(TipoNC.INGRESAR);
                    frmNotaCredito.Show();
                    break;
                case 140: // REPORTE DE INGRESOS DE FACTURAS SAT
                    frmReporteIngresoFacturasSAT frmReporteIngresoFacturasSAT = new frmReporteIngresoFacturasSAT();
                    frmReporteIngresoFacturasSAT.Show();
                    break;
                case 141: // REPORTE DE PROVISION DE NÓMINA NOI
                    frmReporteProvision frmReporteProvision = new frmReporteProvision();
                    frmReporteProvision.Show();
                    break;
                case 142: // REPORTE DE MOVIMIENTOS MP
                    frmReporteMovimientosMP frmReporteMovimientosMP = new frmReporteMovimientosMP();
                    frmReporteMovimientosMP.Show();
                    break;
                case 144: // GESTION DE MODELOS PROSPECT
                    frmCatalogoModelosProspect frmCatalogoModelosProspect = new frmCatalogoModelosProspect();
                    frmCatalogoModelosProspect.Show();
                    break;
                case 145: // NUEVO PROGRAMA DE PRODUCCION
                    frmFlujoOP frmFlujoOP = new frmFlujoOP();
                    frmFlujoOP.ShowDialog();
                    break;
                case 146: // REPORTE OP Y OM
                    frmReporteOPyOM frmReporteOPyOM = new frmReporteOPyOM();
                    frmReporteOPyOM.ShowDialog();
                    break;
                case 147: //SIVO
                    //CONTROL DE UPPEDIDOS
                    Form frmExists2 = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "frmControlPedidos2").SingleOrDefault<Form>();
                    if (frmExists2 != null)
                    {
                        frmExists2.WindowState = FormWindowState.Normal;
                        frmExists2.BringToFront();
                    }
                    else
                    {
                        frmControlPedidos2 frmControlPedidos2 = new frmControlPedidos2();
                        frmControlPedidos2.Show();
                    }
                    break;
                case 149: // Reporte Staff Administración
                    frmRepStaffAdministracion frmRepStaffAdministracion = new frmRepStaffAdministracion();
                    frmRepStaffAdministracion.Show();
                    break;
                case 150: // Requisiciones
                    frmRequisiciones frmRequisiciones = new frmRequisiciones();
                    frmRequisiciones.Show();
                    break;
                case 152: // Requisicion Mostrador
                    frmRequisicionMostrador frmRequisicionMostrador = new frmRequisicionMostrador();
                    frmRequisicionMostrador.Show();
                    break;
                case 99999:
                    //refrescar permisos...
                    Usuario usuario = new Usuario();

                    usuario = usuario.Consultar(ulp_bl.Globales.UsuarioActual.UsuarioUsuario);
                    menuPrincipal.Items.Clear();
                    if (!usuario.TieneError)
                    {
                        CargaMenus(usuario.PermisosPuedeEntrar);
                    }
                    MessageBox.Show("Permisos cargados", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
        }

        private void frmBusquedaAnticipo_OnItemSelected(DataRow SelectedRow)
        {
            int idPedido = Convert.ToInt32(SelectedRow[0].ToString().Trim());
            frmFacturasPorAnticipado frmFacturasPorAnticipado = new SIP.frmFacturasPorAnticipado(idPedido);
            frmFacturasPorAnticipado.ShowDialog();
        }

        private void frmBusquedaApartado_OnItemSelected(DataRow SelectedRow)
        {
            int idPedido = Convert.ToInt32(SelectedRow[0].ToString().Trim());
            frmApartadoLiberacion35 frmApartadoLiberacion35 = new SIP.frmApartadoLiberacion35(idPedido);
            frmApartadoLiberacion35.ShowDialog();
        }

        private void frmBusqueda2_OnItemSelected(DataRow SelectedRow)
        {

            int idPedido = Convert.ToInt32(SelectedRow[0].ToString().Trim());
            string clave_cliente = SelectedRow[1].ToString().Trim();
            Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.OrdenTrabajo, clave_cliente, 0, idPedido, Enumerados.TipoPedido.OrdenTrabajo);
            frmReportes.Show();
        }
        void frmBusqueda1_OnItemSelected(DataRow SelectedRow)
        {
            frmUpPedidos upPedidos = new frmUpPedidos(SelectedRow[1].ToString().Trim(), Convert.ToInt32(SelectedRow[0].ToString().Trim()));
            upPedidos.Show();
        }

        private void menuPrincipal_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void frmControlPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Se confirma que realmente quiera salir del sistema
            //En caso contrario se cancela la salida
            DialogResult respuesta = MessageBox.Show("¿Esta seguro que desea terminar la sesión?", "Salir del Sistema", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (respuesta == DialogResult.No)
            {
                e.Cancel = true;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CargaClientes();


            frmBusquedaGenerica frmBusqueda = new frmBusquedaGenerica(ulp_bl.Globales.tablaclientes, "CLAVE", "LOCALIZACIÓN DE CLIENTES");
            frmBusqueda.ShowDialog();
            if (frmBusqueda.RenglonSeleccionado != null)
            {
                frmFindClie frmFindClie = new frmFindClie(frmBusqueda.RenglonSeleccionado["Clave"].ToString().Trim());
                frmFindClie.Show();
            }

            /*
            DataTable tablaclientes = new DataTable();
            CLIE01 clientes = new CLIE01();
            tablaclientes = clientes.Consultar(txtCliente.Text.ToUpper());            
            if (tablaclientes.Rows.Count>0)
            {
                frmFindClie frmFindClie = new frmFindClie(tablaclientes.Rows[0][0].ToString(), tablaclientes.Rows[0]["DIASCRED"].ToString(), tablaclientes.Rows[0]["CVE_VEND"].ToString());
                frmFindClie.Show();                
            }
             */
        }
        private void CargaClientes()
        {

            CLIE01 clientes = new CLIE01();
            Globales.tablaclientes = clientes.Consultar(Globales.UsuarioActual, "");
        }


        private void AsignaAutoCompletarATextBox(AutoCompleteStringCollection registrosTextBox)
        {
            if (txtCliente.InvokeRequired)
            {
                DelAsignaAutoCompletarATextBox delCargaClientes = new DelAsignaAutoCompletarATextBox(AsignaAutoCompletarATextBox);
                this.Invoke(delCargaClientes, new object[] { registrosTextBox });
            }
            else
            {
                txtCliente.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtCliente.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtCliente.AutoCompleteCustomSource = registrosTextBox;
            }
        }
        private void txtCliente_TextChanged(object sender, EventArgs e)
        {


        }

        private void frmControlPanel_KeyDown(object sender, KeyEventArgs e)
        {
            //Activa teclas CTRL+F1 para Localizar Pedidos


            if (Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.Control) + Convert.ToInt32(Keys.F1))
            {

                bool puedeBuscarClientes = PermisosUsuarioEspeciales.TienePermisos(Globales.UsuarioActual.Id, 6, 9);

                if (puedeBuscarClientes)
                {
                    CargaClientes();
                    frmBusquedaGenerica frmBusqueda = new frmBusquedaGenerica(ulp_bl.Globales.tablaclientes, "CLAVE",
                        "LOCALIZACIÓN DE CLIENTES");
                    frmBusqueda.ShowDialog();
                    if (frmBusqueda.RenglonSeleccionado != null)
                    {
                        frmFindClie frmFindClie =
                            new frmFindClie(frmBusqueda.RenglonSeleccionado["Clave"].ToString().Trim());
                        frmFindClie.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Por el momento usted no tiene acceso a la búsqueda de clientes", "",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }


        }

        private void frmControlPanel_Activated(object sender, EventArgs e)
        {
            if (!clientesCargados)
            {
                BackgroundWorker backGroundWorkerClientes = new BackgroundWorker();
                backGroundWorkerClientes.RunWorkerCompleted += backGroundWorkerClientes_RunWorkerCompleted;
                backGroundWorkerClientes.DoWork += backGroundWorkerClientes_DoWork;
                backGroundWorkerClientes.RunWorkerAsync();
                clientesCargados = true;
            }
        }

        void generaExcelCodigoBarra(List<CodigoBarra> ListaCodigo)
        {
            System.Windows.Forms.SaveFileDialog _file = new SaveFileDialog();
            _file.Filter = "Archivo de Excel (.xls) | *.xls";
            _file.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            _file.FileName = ListaCodigo[0].Referencia.ToString() + "_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".xls";

            if (_file.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //string ruta = (ListaCodigo[0].OrdenMaquila.ToString() + "_" + Path()).Replace(".tmp", ".xls");
                //string ruta = (Path.GetTempPath() + "//" + ListaCodigo[0].OrdenMaquila.ToString() + "_" + Path.GetFileName(Path.GetTempFileName())).Replace(".tmp", ".xls");
                String ruta = _file.FileName;
                RecOrdProduccionMaquilaCodigoBarras.GeneraArchivoExcelDetalle(ListaCodigo, ruta);
                Utiles.FuncionalidadesFormularios.MostrarExcel(ruta);
            }
        }
        List<CodigoBarra> generaCodigosDeBarra(string pedido, string ordenMaquila, DataTable tallasSeleccionadas)
        {
            DataTable dtCModelosEspeciales = RecOrdProduccionMaquilaCodigoBarras.ConsultaCodigoDeBarrasModelosEspeciales();
            List<CodigoBarra> codigos = new List<CodigoBarra> { };
            int cantidadTotal;
            int consecutivo = 0;
            int contador = 0;
            int cantidadAgrupada = 0;
            foreach (DataRow dr in tallasSeleccionadas.Rows)
            {
                cantidadTotal = int.Parse(dr["CANTIDAD"].ToString());
                var result = dtCModelosEspeciales.Select("Modelo = '" + dr["MODELO"].ToString() + "'").FirstOrDefault();
                if (result != null)
                    cantidadAgrupada = int.Parse(result["CantidadAgrupada"].ToString());
                else
                    cantidadAgrupada = 10;

                for (int i = 1; i <= cantidadTotal / cantidadAgrupada; i++)
                {
                    consecutivo = i;
                    contador++;
                    CodigoBarra objCodigo = new CodigoBarra();
                    objCodigo.UUID = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
                    objCodigo.Consecutivo = consecutivo;
                    objCodigo.Contador = contador;
                    objCodigo.Referencia = pedido;
                    objCodigo.OrdenMaquila = ordenMaquila;
                    objCodigo.Almacen = (int)dr["ALMACEN"];
                    objCodigo.Modelo = dr["MODELO"].ToString();
                    objCodigo.Descripcion = dr["DESCRIPCION"].ToString();
                    objCodigo.Talla = dr["TALLA"].ToString();
                    objCodigo.Tipo = (int)dr["ALMACEN"] == 32 ? "E" : "L";
                    objCodigo.Cantidad = cantidadAgrupada;
                    objCodigo.FechaGeneracion = DateTime.Now;
                    codigos.Add(objCodigo);

                }
                if ((cantidadTotal - ((cantidadTotal / cantidadAgrupada) * cantidadAgrupada)) > 0)
                {
                    consecutivo++;
                    contador++;
                    CodigoBarra objCodigo = new CodigoBarra();
                    objCodigo.UUID = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
                    objCodigo.Consecutivo = consecutivo;
                    objCodigo.Contador = contador;
                    objCodigo.Referencia = pedido;
                    objCodigo.OrdenMaquila = ordenMaquila;
                    objCodigo.Almacen = (int)dr["ALMACEN"]; ;
                    objCodigo.Modelo = dr["MODELO"].ToString();
                    objCodigo.Descripcion = dr["DESCRIPCION"].ToString();
                    objCodigo.Talla = dr["TALLA"].ToString();
                    objCodigo.Tipo = (int)dr["ALMACEN"] == 32 ? "E" : "L";
                    objCodigo.Cantidad = (cantidadTotal - ((cantidadTotal / cantidadAgrupada) * cantidadAgrupada));
                    objCodigo.FechaGeneracion = DateTime.Now;
                    codigos.Add(objCodigo);
                }
            }


            //CONSIDERAMOS QUE ALMACEN 32 ES ESPECIALES, CUALQUIER OTRO ALMACEN ES LINEA
            /*switch (almacen)
            {
                case 32:
                    //SE GENERA CODIGO DE BARRAS POR MODELO (EN MULTIPLOS DE 10)
                    cantidadTotal = int.Parse(tallasSeleccionadas.Compute("Sum(CANTIDAD)", "").ToString());
                    for (int i = 1; i <= cantidadTotal / 10; i++)
                    {
                        consecutivo = i;
                        CodigoBarra objCodigo = new CodigoBarra();
                        objCodigo.UUID = Guid.NewGuid().ToString().Substring(0, 5).ToUpper();
                        objCodigo.Consecutivo = consecutivo;
                        objCodigo.Referencia = pedido;
                        objCodigo.OrdenMaquila = ordenMaquila;
                        objCodigo.Almacen = almacen;
                        objCodigo.Modelo = tallasSeleccionadas.Rows[0]["MODELO"].ToString();
                        objCodigo.Descripcion = tallasSeleccionadas.Rows[0]["DESCR"].ToString();
                        objCodigo.Talla = tallasSeleccionadas.Rows[0]["TALLA"].ToString();
                        objCodigo.Tipo = "E";
                        objCodigo.Cantidad = 10;
                        objCodigo.FechaGeneracion = DateTime.Now;
                        codigos.Add(objCodigo);
                    }
                    consecutivo++;
                    if ((cantidadTotal - ((cantidadTotal / 10) * 10)) > 0)
                    {
                        CodigoBarra objCodigo = new CodigoBarra();
                        objCodigo.UUID = Guid.NewGuid().ToString().Substring(0, 5).ToUpper();
                        objCodigo.Consecutivo = consecutivo;
                        objCodigo.Referencia = pedido;
                        objCodigo.OrdenMaquila = ordenMaquila;
                        objCodigo.Almacen = almacen;
                        objCodigo.Modelo = tallasSeleccionadas.Rows[0]["MODELO"].ToString();
                        objCodigo.Talla = tallasSeleccionadas.Rows[0]["TALLA"].ToString();
                        objCodigo.Tipo = "E";
                        objCodigo.Cantidad = (cantidadTotal - ((cantidadTotal / 10) * 10));
                        objCodigo.FechaGeneracion = DateTime.Now;
                        codigos.Add(objCodigo);
                    }
                    break;
                default:
                    //SE GENERA CODIGO DE BARRAS POR MODELO POR TALLA EN MULTIPLOS DE 10
                    foreach (DataRow dr in tallasSeleccionadas.Rows)
                    {
                        cantidadTotal = (int)dr["CANTIDAD"];
                        var query = from row in this.dataTableTallas.AsEnumerable() where row.Field<String>("CLAVE") == dr["MODELO"].ToString() + dr["TALLA"].ToString() select row;
                        for (int i = 1; i <= cantidadTotal / 10; i++)
                        {
                            consecutivo = i;
                            CodigoBarra objCodigo = new CodigoBarra();
                            objCodigo.UUID = Guid.NewGuid().ToString().Substring(0,5).ToUpper();
                            objCodigo.Consecutivo = consecutivo;
                            objCodigo.Referencia = pedido;
                            objCodigo.OrdenMaquila = ordenMaquila;
                            objCodigo.Almacen = almacen;
                            objCodigo.Modelo = dr["MODELO"].ToString();
                            objCodigo.Descripcion = query.ToList()[0]["DESCR"].ToString();
                            objCodigo.Talla = dr["TALLA"].ToString();
                            objCodigo.Tipo = "L";
                            objCodigo.Cantidad = 10;
                            objCodigo.FechaGeneracion = DateTime.Now;
                            codigos.Add(objCodigo);
                        }
                        consecutivo++;
                        if ((cantidadTotal - ((cantidadTotal / 10) * 10)) > 0)
                        {
                            CodigoBarra objCodigo = new CodigoBarra();
                            objCodigo.UUID = Guid.NewGuid().ToString().Substring(0, 5).ToUpper();
                            objCodigo.Consecutivo = consecutivo;
                            objCodigo.Referencia = pedido;
                            objCodigo.OrdenMaquila = ordenMaquila;
                            objCodigo.Almacen = almacen;
                            objCodigo.Modelo = dr["MODELO"].ToString();
                            objCodigo.Descripcion = query.ToList()[0]["DESCR"].ToString();
                            objCodigo.Talla = dr["TALLA"].ToString();
                            objCodigo.Tipo = "L";
                            objCodigo.Cantidad = (cantidadTotal - ((cantidadTotal / 10) * 10));
                            objCodigo.FechaGeneracion = DateTime.Now;
                            codigos.Add(objCodigo);
                        }
                    }

                    break;
            }*/

            return codigos;
        }

        void generaReporteClientesExcedentesCredito()
        {
            try
            {
                string archivoTemporal = Path.GetTempFileName().Replace(".tmp", ".xls");
                DataTable dt = Credito.getClientesSAE();
                Credito.GeneraArchivoExcel(archivoTemporal, dt);
                FuncionalidadesFormularios.MostrarExcel(archivoTemporal);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el reporte: " + ex.Message, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
