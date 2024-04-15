using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ulp_dl.aspel_sae80;
using ulp_dl.SIPReportes;
using ulp_dl.SIPNegocio;
using ulp_bl.Utiles;
using ulp_dl;
using sm_dl.SqlServer;
using sm_dl;

namespace ulp_bl
{
    public class SOLICITUDES_ESPECIALES : ICrud<SOLICITUDES_ESPECIALES>
    {
        private Exception error;
        private bool tieneError;

        public int SOLICITUD { get; set; }
        public String CLIENTE { get; set; }
        public DateTime FECHA { get; set; }
        public String AGENTE { get; set; }
        public int PRENDAS_LINEA { get; set; }
        public int PRENDAS_ESP { get; set; }
        public String GENERO { get; set; }
        public String TIPO_PRENDA { get; set; }
        public String COMPOSICION_TELA { get; set; }
        public String COLOR { get; set; }
        public String ESPECIFICACIONES { get; set; }
        public String TALLAS { get; set; }
        public String PLAZO_ENTREGA { get; set; }
        public decimal PRECIO_ASIGNADO { get; set; }
        public String CODIGO_COTIZACION { get; set; }
        public String CODIGOS_ASIGNADOS { get; set; }
        public String ESTATUS { get; set; }

        public bool TieneError
        {
            get { return tieneError; }
        }
        public Exception Error
        {
            get { return error; }
        }
        //CONSULTAS
        public SOLICITUDES_ESPECIALES Consultar(int ID)
        {
            ulp_bl.SOLICITUDES_ESPECIALES datosPedido = new SOLICITUDES_ESPECIALES();
            using (var dbContext = new AspelSae80Context())
            {
                var result = dbContext.SOLICITUDES_ESPECIALES.Find(ID);
                CopyClass.CopyObject(result, ref datosPedido);
            }
            return datosPedido;
        }
        public DataTable ConsultarSolicitud(int ID)
        {
            DataTable SolicitudEspecial = new DataTable();
            ulp_bl.SOLICITUDES_ESPECIALES datosPedido = new SOLICITUDES_ESPECIALES();
            using (var dbContext = new AspelSae80Context())
            {
                var result = from solicitudes in dbContext.SOLICITUDES_ESPECIALES
                             where solicitudes.SOLICITUD == ID
                             select solicitudes;
                SolicitudEspecial = Linq2DataTable.CopyToDataTable(result);
            }
            return SolicitudEspecial;
        }
        public DataTable ConsultarSolicitudes(List<int> IDSolicitudes)
        {
            DataTable SolicitudEspecial = new DataTable();
            ulp_bl.SOLICITUDES_ESPECIALES datosPedido = new SOLICITUDES_ESPECIALES();
            using (var dbContext = new AspelSae80Context())
            {
                var result = from solicitudes in dbContext.SOLICITUDES_ESPECIALES
                             join cliente in dbContext.CLIE01 on solicitudes.CLIENTE.Trim() equals cliente.CLAVE.Trim()
                             where IDSolicitudes.Contains(solicitudes.SOLICITUD)
                             select new
                             {
                                 solicitudes.SOLICITUD,
                                 solicitudes.CLIENTE,
                                 solicitudes.FECHA,
                                 solicitudes.AGENTE,
                                 solicitudes.PRENDAS_LINEA,
                                 solicitudes.PRENDAS_ESP,
                                 solicitudes.GENERO,
                                 solicitudes.TIPO_PRENDA,
                                 solicitudes.COMPOSICION_TELA,
                                 solicitudes.COLOR,
                                 solicitudes.ESPECIFICACIONES,
                                 solicitudes.TALLAS,
                                 solicitudes.PLAZO_ENTREGA,
                                 solicitudes.PRECIO_ASIGNADO,
                                 solicitudes.CODIGO_COTIZACION,
                                 solicitudes.CODIGOS_ASIGNADOS,
                                 CLIENTE_NOMBRE = cliente.NOMBRE
                             };
                SolicitudEspecial = Linq2DataTable.CopyToDataTable(result);
            }
            return SolicitudEspecial;
        }
        public DataTable ConsultasSolicitudesCliente(string idCliente)
        {
            DataTable Solicitudes = new DataTable();
            using (var dbContext = new AspelSae80Context())
            {
                var resultado = from solicitudes in dbContext.SOLICITUDES_ESPECIALES
                                where solicitudes.CLIENTE.Trim() == idCliente.Trim()
                                orderby solicitudes.SOLICITUD
                                select solicitudes;
                Solicitudes = Linq2DataTable.CopyToDataTable(resultado);

            }
            return Solicitudes;
        }


        public DataTable ConsultarTodos()
        {
            throw new NotImplementedException();
        }
        //ALTAS
        public void Crear(SOLICITUDES_ESPECIALES tEntidad)
        {
            throw new NotImplementedException();
        }

        public void Crear(SOLICITUDES_ESPECIALES tEntidad, ref int idSolicitud)
        {
            try
            {
                ulp_dl.aspel_sae80.SOLICITUDES_ESPECIALES solicitudes = new ulp_dl.aspel_sae80.SOLICITUDES_ESPECIALES();
                //PED_MSTR pedidos = new PED_MSTR();
                using (var dbContext = new AspelSae80Context())
                {
                    CopyClass.CopyObject(tEntidad, ref solicitudes);
                    dbContext.SOLICITUDES_ESPECIALES.Add(solicitudes);
                    dbContext.SaveChanges();
                    idSolicitud = solicitudes.SOLICITUD;
                }
            }
            catch (Exception ex)
            {
                tieneError = true;
                error = ex;
            }
        }
        //BAJAS
        public void Borrar(SOLICITUDES_ESPECIALES tEntidad, Enumerados.TipoBorrado TipoBorrado)
        {
            using (var dbContext = new AspelSae80Context())
            {
                var solicitud_a_borrar = dbContext.SOLICITUDES_ESPECIALES.Find(tEntidad.SOLICITUD);
                dbContext.SOLICITUDES_ESPECIALES.Remove(solicitud_a_borrar);
                dbContext.SaveChanges();
            }
        }
        //MODIFICACIONES
        public void Modificar(SOLICITUDES_ESPECIALES tEntidad)
        {
            try
            {
                using (var dbContext = new AspelSae80Context())
                {
                    //var resultado = dbContext.PED_MSTR.Find(tEntidad.PEDIDO);
                    var resultado = (from res in dbContext.SOLICITUDES_ESPECIALES where res.SOLICITUD == tEntidad.SOLICITUD select res).FirstOrDefault();
                    //no se utiliza un ciclo por que la consulta linq siempre devolverá sólo un resultado find lo determina                    

                    resultado.PRENDAS_LINEA = tEntidad.PRENDAS_LINEA;
                    resultado.PRENDAS_ESP = tEntidad.PRENDAS_ESP;
                    resultado.TIPO_PRENDA = tEntidad.TIPO_PRENDA;
                    resultado.COMPOSICION_TELA = tEntidad.COMPOSICION_TELA;
                    resultado.COLOR = tEntidad.COLOR;
                    resultado.ESPECIFICACIONES = tEntidad.ESPECIFICACIONES;
                    resultado.GENERO = tEntidad.GENERO;
                    resultado.ESTATUS = "A";
                    int i = 0;
                    i = dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                tieneError = true;
                error = ex;
            }
        }
        public static void AsignaPlazoCodigo(int ClaveSolicitud, String _plazo, String _CodigoCotizacion)
        {
            try
            {
                using (var dbContext = new AspelSae80Context())
                {
                    var query = from solicitudes in dbContext.SOLICITUDES_ESPECIALES where solicitudes.SOLICITUD == ClaveSolicitud select solicitudes;
                    foreach (ulp_dl.aspel_sae80.SOLICITUDES_ESPECIALES solicitudes_especiales in query)
                    {
                        solicitudes_especiales.PLAZO_ENTREGA = _plazo;
                        solicitudes_especiales.CODIGO_COTIZACION = _CodigoCotizacion;
                    }
                    dbContext.SaveChanges();

                }
            }
            catch { }
        }
        public static void AsignaPrecio(int ClaveSolicitud, decimal _precio)
        {
            try
            {
                using (var dbContext = new AspelSae80Context())
                {
                    var query = from solicitudes in dbContext.SOLICITUDES_ESPECIALES where solicitudes.SOLICITUD == ClaveSolicitud select solicitudes;
                    foreach (ulp_dl.aspel_sae80.SOLICITUDES_ESPECIALES solicitudes_especiales in query)
                    {
                        solicitudes_especiales.PRECIO_ASIGNADO = _precio;
                    }
                    dbContext.SaveChanges();

                }
            }
            catch { }
        }
        public static void AsignaTallas(int ClaveSolicitud, String _tallas)
        {
            try
            {
                using (var dbContext = new AspelSae80Context())
                {
                    var query = from solicitudes in dbContext.SOLICITUDES_ESPECIALES where solicitudes.SOLICITUD == ClaveSolicitud select solicitudes;
                    foreach (ulp_dl.aspel_sae80.SOLICITUDES_ESPECIALES solicitudes_especiales in query)
                    {
                        solicitudes_especiales.TALLAS = _tallas;
                    }
                    dbContext.SaveChanges();

                }
            }
            catch { }
        }
        public static void AsignaCodigos(int ClaveSolicitud, String _codigos)
        {
            try
            {
                using (var dbContext = new AspelSae80Context())
                {
                    var query = from solicitudes in dbContext.SOLICITUDES_ESPECIALES where solicitudes.SOLICITUD == ClaveSolicitud select solicitudes;
                    foreach (ulp_dl.aspel_sae80.SOLICITUDES_ESPECIALES solicitudes_especiales in query)
                    {
                        solicitudes_especiales.CODIGOS_ASIGNADOS = _codigos;
                    }
                    dbContext.SaveChanges();

                }
            }
            catch { }
        }
        public static List<String> AsignaTallasCodigo(int ClaveSolicitud, List<String> _tallas)
        {
            List<String> Codigos = new List<string> { };
            try
            {
                SqlServerCommand cmd = new SqlServerCommand();
                using (var dbContext = new SIPReportesContext())
                {
                    cmd.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                }

                cmd.ObjectName = "usp_setAltaTallasCodigoEspecial";
                foreach (String _talla in _tallas)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@idSolicitudEspecial", ClaveSolicitud));
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@talla", _talla));
                    DataTable dt = cmd.GetDataTable();
                    Codigos.Add(dt.Rows[0]["CODIGO"].ToString());
                }
                cmd.Connection.Close();
                return Codigos;

            }
            catch { return null; }
        }
        public static void AsignaPrecioCodigo(String _codigo, Decimal _precio)
        {
            try
            {
                SqlServerCommand cmd = new SqlServerCommand();
                using (var dbContext = new SIPReportesContext())
                {
                    cmd.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                }
                cmd.ObjectName = "usp_setAltaPrecioCodigoEspecial";
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@codigo", _codigo));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@precio", _precio));
                cmd.Execute();
                cmd.Connection.Close();
            }
            catch { }
        }
        public static void EliminaCodigoBaseEspecial(int ClaveSolicitud)
        {
            try
            {
                SqlServerCommand cmd = new SqlServerCommand();
                using (var dbContext = new SIPReportesContext())
                {
                    cmd.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                }
                cmd.ObjectName = "usp_setEliminaCodigoBaseEspecial";
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@idSolicitudEspecial", ClaveSolicitud));
                cmd.Execute();
                cmd.Connection.Close();
            }
            catch { }
        }
        public static void ReasignaCliente(int ClaveSolicitud, string _Clave)
        {
            try
            {
                SqlServerCommand cmd = new SqlServerCommand();
                using (var dbContext = new SIPReportesContext())
                {
                    cmd.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                }
                cmd.ObjectName = "usp_setReasignaClienteSolicitud";
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SOLICITUD", ClaveSolicitud));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CLIENTE", _Clave));
                cmd.Execute();
                cmd.Connection.Close();
            }
            catch { }
        }
        public static void EliminaSolicitud(int ClaveSolicitud)
        {
            try
            {
                SqlServerCommand cmd = new SqlServerCommand();
                using (var dbContext = new SIPReportesContext())
                {
                    cmd.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                }
                cmd.ObjectName = "usp_setEliminaSolicitud";
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SOLICITUD", ClaveSolicitud));
                cmd.Execute();
                cmd.Connection.Close();
            }
            catch { }
        }
    }
}
