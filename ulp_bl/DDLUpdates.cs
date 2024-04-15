using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using sm_dl;
using sm_dl.SqlEmbedded;
using sm_dl.SqlServer;
using ulp_dl.SIPNegocio;

namespace ulp_bl
{
    public class DDLControl {

    public int uid { set; get; } //int, identity!
    public int? Version { set; get; } //int
    //public DateTime UpdateDate { set; get; } //date
    public string ObjectName { set; get; } //varchar(255)
    public string UpdateDesc { set; get; } //varchar(500)

    }
    public class SIPDDLUpdates
    {
        private static void RunAlter(string connStr, int ObjectUpdateVersion, string ObjectName, string EmbeddedResourceFileName, string AlterDescriptionLog)
        {
            if (Utiles.DDL.Exists(connStr, ObjectName))
            {
                int nuevaVersion = ObjectUpdateVersion;   //<------------- aumentar este número por cada vez que se actualice este objeto

                if (GetObjectVersion(ObjectName) < nuevaVersion) //reviso la versión del SP para saber si se actualiza o no
                {

                    //extraigo el SQL del recurso incrustado con la nueva version del SP
                    string ddlCreateTable = Utiles.EmbeddedResoures.GetTextResource(string.Format("ulp_bl.DDL.{0}", EmbeddedResourceFileName));
                    //ejecuto el SQL
                    Utiles.DDL.Execute(connStr, ddlCreateTable);
                    //Grabo en la bitácora este evento
                    CreateDDLLogEntry(new DDLControl()
                    {
                        ObjectName = ObjectName,
                        UpdateDesc = AlterDescriptionLog,
                        Version = nuevaVersion
                    });
                }
            }
        }

        private static void RunSQL(string connStr, int ObjectUpdateVersion, string ObjectName,string EmbeddedResourceFileName, string SQLDescriptionLog)
        {
            int nuevaVersion = ObjectUpdateVersion;   //<------------- aumentar este número por cada vez que se actualice este objeto

            if (GetObjectVersion(ObjectName) < nuevaVersion) //reviso la versión del SP para saber si se actualiza o no
            {

                //extraigo el SQL del recurso incrustado con la nueva version del SP
                string scriptSQL = Utiles.EmbeddedResoures.GetTextResource(string.Format("ulp_bl.DDL.{0}", EmbeddedResourceFileName));
                //ejecuto el SQL
                Utiles.DDL.Execute(connStr, scriptSQL);
                //Grabo en la bitácora este evento
                CreateDDLLogEntry(new DDLControl()
                {
                    ObjectName = ObjectName,
                    UpdateDesc = SQLDescriptionLog,
                    Version = nuevaVersion
                });
            }    
        }
        private static void RunCreate(string connStr,string ObjectName,string EmbeddedResourceFileName,string CreateDescriptionLog,bool RunAfterCreate = false)
        {
            //si no existe la tabla...
            if (!Utiles.DDL.Exists(connStr, ObjectName))
            {
                //extraigo el SQL del recurso incrustado
                string ddlCreateTable = Utiles.EmbeddedResoures.GetTextResource(string.Format("ulp_bl.DDL.{0}", EmbeddedResourceFileName));
                //ejecuto el SQL
                Utiles.DDL.Execute(connStr, ddlCreateTable);
                //En caso de que el parámetro RunAfterCreate sea verdadero aparte de crear el Objeto también lo ejecuto "SOLO PARA STORED PROCEDURES!!"...
                if (RunAfterCreate)
                {
                    Utiles.DDL.Execute(connStr, ObjectName);
                }
                //Grabo en la bitácora este evento
                CreateDDLLogEntry(new DDLControl()
                {
                    ObjectName = ObjectName,
                    UpdateDesc = CreateDescriptionLog,
                    Version = 1
                });


            }
        }

        public static void Run()
        {
            string connStr = "";

            //Se crea en la base de datos de SIP 
            /*
             * 
             * */

            using (var dbContext = new SIPNegocioContext())
            {
                connStr = dbContext.Database.Connection.ConnectionString;
            }
            
            //solo en caso de no existir los objetos se crean con RunCreate()
            RunCreate(connStr, "DDLControl", "CreateTableDLLControl.sql", "Creación de la tabla DDLControl");
            RunCreate(connStr, "usp_inventario_con_pedido", "usp_inventario_con_pedido.sql", "Creación de usp_inventario_con_pedido");
            RunCreate(connStr, "usp_RegresaRutasPorPedido", "usp_RegresaRutasPorPedido.sql","Creación de usp_RegresaRutasPorPedido");
            RunCreate(connStr, "udf_Regresa_procesos_por_pedido_y_agrupador", "udf_Regresa_procesos_por_pedido_y_agrupador.sql", "Creación de udf_Regresa_procesos_por_pedido_y_agrupador");
            RunCreate(connStr, "usp_RepExistResumen", "usp_RepExistResumen.sql", "Creación de usp_RepExistResumen");
            RunCreate(connStr, "usp_RepExistBaseMin", "usp_RepExistBaseMin.sql", "Creación de usp_RepExistBaseMin");
            RunCreate(connStr, "usp_Permisos_CreaAccesosLogos", "usp_Permisos_CreaAccesosLogos.sql","Creación de registros necesarios para mermisos en Logotipos",true);
            RunCreate(connStr, "usp_Permisos_CreaAccesosUPPedidos", "usp_Permisos_CreaAccesosUPPedidos.sql", "Creación de registros necesarios para mermisos en UPPEDIDOS",true);
            RunCreate(connStr, "LogUPPedidos", "CreateTableLogUPPEDIDOS.sql", "Creación de tabla para Log de cambios en UPPEDIDOS");
            RunCreate(connStr, "COST_ENLA", "CreateTableCOST_ENLA.sql","Se crea tabla COST_ENLA para módulo de Captura de facturas de proveedores de Fletes");
            RunCreate(connStr, "COST_MSTR", "CreateTableCOST_MSTR.sql", "Se crea tabla COST_MSTR para módulo de Captura de facturas de proveedores de Fletes");
            RunCreate(connStr, "udf_RegresaFacturasPorPedidoCostura", "udf_RegresaFacturasPorPedidoCostura.sql","Se crea esta función para módulo de Captura de facturas de proveedores de Fletes");
            RunCreate(connStr, "usp_DocumentosPorPedidoCostura", "usp_DocumentosPorPedidoCostura.sql", "Se crea esta función para módulo de Captura de facturas de proveedores de Fletes");
            RunCreate(connStr, "usp_ProcesoAjusteSaldo", "usp_ProcesoAjusteSaldo.sql","Se crea SP responsable de ajustar los saldos de un cliente dado");
            RunCreate(connStr, "usp_RegresaSaldosCxC", "usp_RegresaSaldosCxC.sql","Se crea SP responsable de regresar los clientes con diferencias en sus saldos");

            //si ya existen y se va a modificar el objeto se utiliza RunAlter, no olvidar aumentar la versión si un objeto sufre + de 1 cambio
            RunAlter(connStr, 1, "usp_BuscaPedidosPorCriterio", "AlterUsp_BuscaPedidosPorCriterio.sql","Se actualiza el SP porque no filtraba por Agente");
            RunAlter(connStr, 1, "usp_RepReconstruccionYCosteo", "usp_RepReconstruccionYCosteo.sql", "Se actualiza el SP, no encontraba objeto #totales, es Case Sensitive se cambia por #TOTALES");
            RunAlter(connStr, 1, "usp_RepReconsYConteoMermas_CMO", "usp_RepReconsYConteoMermas_CMO.sql", "Se actualiza el SP, no encontraba variable #costo, es Case Sensitive se cambia por #Costo");
            RunAlter(connStr, 1, "usp_RepPedidosMas20Dias", "usp_RepPedidosMas20Dias.sql", "Se actualiza el SP, la cantidad de prendas no cuadraba con el sistema de VB6");
            RunAlter(connStr, 1, "usp_RepFacturaXMaquilador", "usp_RepFacturaXMaquilador.sql","Se actualiza el SP, aumentó el costo de bordado de 3 a 3.5");
            RunAlter(connStr, 1, "usp_Inserta_PED_TEMP_de_PED_DET", "usp_Inserta_PED_TEMP_de_PED_DET.sql", "Código para guardar los datos de importes, descuentos etc, antes no se imprimía en en reporte");
            RunAlter(connStr, 1, "usp_RepVentPesosPrendas", "usp_RepVentPesosPrendas.sql","Se modificó SP porque marcaba error de División entre 0, se dio de alta usuario UP que es el que marcaba error");
            RunAlter(connStr, 1, "usp_RegenerarCarteraViva", "usp_RegenerarCarteraViva.sql","Se corrige SP, estaba borrando toda la tabla y no solo el año a regenerar.");
            RunAlter(connStr, 1, "usp_EntradaAFabricacionOPManual", "usp_EntradaAFabricacionOPManual.sql", "Se aplica un Cast a Getdate()");
            RunAlter(connStr, 1, "usp_frmTransferenciaXModeloProcesar", "usp_frmTransferenciaXModeloProcesar.sql","Se aplica un Cast a Getdate()");
            RunAlter(connStr, 1, "vw_Inventario", "vw_Inventario.sql", "Se agrega filtro a la vista: I.STATUS= A, devolvía nulo el precio");
            RunAlter(connStr, 2, "usp_RepExistResumen", "usp_RepExistResumen2.sql", "Se metió ISNULL al campo existencia, ya que en el reporte se veía un dato vacío");
            RunAlter(connStr, 1, "usp_OrdenTrabajo", "usp_OrdenTrabajo.sql","Se metió un Left Join y se corrigió el orden en el que se mostraban los procesos");
            RunAlter(connStr, 1, "usp_Pedido", "usp_Pedido.sql","Se aumentó ORDER BY debido a que los procesos no salían igual a la captura");
            RunAlter(connStr, 1, "usp_frmTransferenciaXPedidoProcesar", "usp_frmTransferenciaXPedidoProcesar.sql","Se corrigió el orden en el que se insertan los registros a MINVE01");
            RunAlter(connStr, 1, "usp_RepStaff", "usp_RepStaff.sql","Se corrige error de división entre cero");
            RunAlter(connStr, 2, "usp_frmTransferenciaXModeloProcesar", "usp_frmTransferenciaXModeloProcesar2.sql", "Se agrega validación de Isnull al determinar el Costo, se cambia tipo de dato de Numeric a Int, marcaba desbordamiento");
            RunAlter(connStr, 1, "usp_RecOrdProduccionMaquiaProcesar", "usp_RecOrdProduccionMaquiaProcesar.sql","Correcciones al SP");
            RunAlter(connStr, 1, "usp_RecOrdProduccionMaquiaProcesar1", "usp_RecOrdProduccionMaquiaProcesar1.sql", "Correcciones al SP");
            RunAlter(connStr, 1, "usp_ActualizacionCostosConManoObra", "usp_ActualizacionCostosConManoObra.sql", "Se agrega Round a una SUMA");
            RunAlter(connStr, 1, "usp_RepUniversoPedidosEmpaque", "usp_RepUniversoPedidosEmpaque.sql", "Se corrige Bug de Case Sensitive para la BD de Aspel_Sae50 a aspel_sae50.");
            RunAlter(connStr, 1, "usp_RecOrdProduccionMaquia", "usp_RecOrdProduccionMaquia.sql", "Se agrega Order By");
            RunAlter(connStr, 2, "usp_Pedido", "usp_Pedido1.sql","Se pone ISNULL para el Descuento y la Comisión");
            RunAlter(connStr, 1, "usp_RepEstadoCuentaXDepto", "usp_RepEstadoCuentaXDepto.sql", "Se mejoró la velocidad en la generación del reporte");
            RunAlter(connStr, 2, "usp_RepPedidosMas20Dias", "usp_RepPedidosMas20Dias1.sql","Se resetea la variable @prendas a 0 (cero)");
            RunAlter(connStr, 1, "usp_RepCostoVsPrecioFlete", "usp_RepCostoVsPrecioFlete.sql", "Se modifican los parámetros de entrada de datetime a date");
            RunAlter(connStr, 2, "usp_RepCostoVsPrecioFlete", "usp_RepCostoVsPrecioFlete1.sql", "Se cambia el universo por solo los pedidos facturados");
            RunAlter(connStr, 1, "usp_ActualizaUPPedidosCapturaSae", "usp_ActualizaUPPedidosCapturaSae.sql","Se agrega DELETE antes de INSERT, marcaba error de llave duplicada");
            RunAlter(connStr, 2, "usp_EntradaAFabricacionOPManual", "usp_EntradaAFabricacionOPManual1.sql", "Se cambia tipo de datos INT a FLOAT para que mustre decimales cuando el Requerimiento no se cumple");
            RunAlter(connStr, 1, "usp_RepRecepcionMaquila", "usp_RepRecepcionMaquila.sql","Se cambian nombres de columnas y se aplica filtro");
            RunAlter(connStr, 2, "usp_Inserta_PED_TEMP_de_PED_DET", "usp_Inserta_PED_TEMP_de_PED_DET1.sql", "se agrega \"xx\" cuando la funcíon que regresa los procesos devuelve nulo");
            RunAlter(connStr, 2, "usp_RepVentPesosPrendas", "usp_RepVentPesosPrendas1.sql", "Se modifica totalmente el reporte de Ventas en pesos y prendas por agente según último código fuente entregado del SIP7");
            RunAlter(connStr, 1, "usp_RepDesmpeñoPorAreasResumen", "usp_RepDesmpeñoPorAreasResumen.sql", "Se modifica SP para que considere solo TIPO = \"OV\"");
            RunAlter(connStr, 1, "usp_RepFechasYDesempeñoPorArea", "usp_RepFechasYDesempeñoPorArea.sql","Se cambia F_ENTREGADO por F_EMBARQUE en Fch Real y pedidos tipo OV");
            RunAlter(connStr, 2, "usp_RepDesmpeñoPorAreasResumen", "usp_RepDesmpeñoPorAreasResumen1.sql","Se modifica SP para que considere solo TIPO = \"OV\" por cada consulta que inserta en #DesempeñoPorAreas");
            RunAlter(connStr, 3, "usp_RepVentPesosPrendas", "usp_RepVentPesosPrendas2.sql", "Se vuelve a modificar según código fuente de SIP 7.15.10 (hubo modificaciones)");
            RunAlter(connStr, 3, "usp_RepExistResumen", "usp_RepExistResumen3.sql", "se modifica consulta para que devuelva totalizados igual al SIP7 7.15.1");
            RunAlter(connStr, 3, "usp_EntradaAFabricacionOPManual", "usp_EntradaAFabricacionOPManual2.sql","Se aumentó el tamaño de una variable Varchar porque al hacer Convert de un float se desbordaba");

            //SQL directos a ejecutar
            RunSQL(connStr,1, "CMT_DET", "r_CMT_DET.sql", "Script que corrige las fechas no actualizadas por UPPEDIDOS en la fecha de entrega");
            RunSQL(connStr, 1, "r_Permisos_CreaAccesosPestanasCliente", "r_Permisos_CreaAccesosPestanasCliente.sql", "Ae agregan nuevos permisos a las pestañas la pantalla de frmFindClie");
            RunSQL(connStr, 1, "r_CreaAccesosYMenuRepCostura", "r_CreaAccesosYMenuRepCostura.sql","Creación del menú y permisos de Captura de facturas de proveedores de Fletes");
            RunSQL(connStr, 2, "CMT_DET", "r_CMT_DET2.sql","Script que corrige el campo CMT_ESTATUS=\"R\" cuando capturan fecha de liberación en UPPEDIDOS");
            RunSQL(connStr, 3, "CMT_DET", "r_CMT_DET3.sql", "Script que actualiza CMT_DET para pedidos con FLETE y que el Cliente es nulo");
            RunSQL(connStr, 1, "r_Permisos_CreaAccesosTransferenciaInv", "r_Permisos_CreaAccesosTransferenciaInv.sql","Nuevo permiso para limitar las transferencia del alm. 1 al 3");
            RunSQL(connStr, 1, "r_CreaAccesosYMenuCxC", "r_CreaAccesosYMenuCxC.sql", "Creación del menú y permisos de Ajuste de CxC en Aspel-SAE");
        }
        public static void CreateDDLLogEntry(DDLControl Control)
        {
            string connStr = "";
            using (var dbContext = new SIPNegocioContext())
            {
                connStr = dbContext.Database.Connection.ConnectionString;
            }            
            Insert insert = new Insert();
            insert.TableName = "DDLControl";
            insert.Connection = DALUtil.GetConnection(connStr);
            insert.FieldValue = new List<FieldValue>();
            insert.FieldValue.Add(new FieldValue("ObjectName", "'" + Control.ObjectName + "'"));
            insert.FieldValue.Add(new FieldValue("UpdateDate", "GETDATE()"));
            insert.FieldValue.Add(new FieldValue("UpdateTime", "GETDATE()"));            
            insert.FieldValue.Add(new FieldValue("UpdateDesc", "'" + Control.UpdateDesc + "'"));
            insert.FieldValue.Add(new FieldValue("Version", Control.Version));
            insert.FieldValue.Add(new FieldValue("UpdatedBy", "'" + Environment.MachineName + "'"));            
            
            insert.Execute();
            insert.Connection.Close();
        }

        public static int GetObjectVersion(string ObjectName)
        {
            string connStr = "";
            using (var dbContext = new SIPNegocioContext())
            {
                connStr = dbContext.Database.Connection.ConnectionString;
            }
            SqlServerSelectCommand select = new SqlServerSelectCommand();
            select.Connection = DALUtil.GetConnection(connStr);
            select.ObjectName = string.Format("select isnull(Max(Version),0) as [version] from DDLControl where [ObjectName]='{0}' group by [ObjectName]", ObjectName);            
            DataTable dt = select.GetDataTable();
            int ultVer = 0;
            if (dt.Rows.Count > 0)
            {
                ultVer = (int) dt.Rows[0]["version"];
            }
            else
            {
                ultVer = 0;
            }
            return ultVer;
        }
    }
}
