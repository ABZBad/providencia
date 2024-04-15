using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ulp_dl;
using ulp_dl.aspel_sae80;
using ulp_dl.SIPPermisos;
using ulp_bl.Utiles;

namespace ulp_bl
{
    /// <summary>
    /// Esta clase USUARIO es de negocio e implementa la Interface genérica ICrud
    /// la cuál es responsable de asegurarse que los métodos y propiedades para
    /// manejo de errores, guardar, modificar, borrar y consultar sean nombrados
    /// de forma estándar.
    /// 
    /// La clase Usuario de esta Capa es una copia en sus principales propiedades
    /// de la clase: PermisosUsuarios.
    /// 
    /// 
    /// una vez obtenida la información de la capa de datos, esta se copia por me-
    /// dio de: CopyClass.CopyObject para transferir la información a la capa de
    /// negocio.
    /// 
    /// Cualquier funcionalidad que tenga que ver con el Usuario a nivel "Negocio"
    /// se deberá codificar en esta clase
    /// </summary>
    public class Usuario: ICrud<Usuario>
    {

        private List<int> permisosPuedeEntrar = new List<int>();

        private Exception error;
        private bool tieneError;

        //Propiedades que hacen Match con el objeto: PermisosUsuario del modelo de datos
        public int Id { get; set; }
        public string UsuarioUsuario { get; set; }
        public string UsuarioNombre { get; set; }
        public string UsuarioContraseña { get; set; }
        public string UsuarioCorreo { get; set; }
        public bool UsuarioStatus { get; set; }
        public String UsuarioPerfil { get; set; }
        public DateTime UsuarioFechaIngreso { get; set; }
        public USUARIOS UsuarioSae { set; get; }

        //Otras propiedades necesarias
        public String UsuarioArea { get; set; }

        /// <summary>
        /// Devuelve una lista de Enteros con los permisos del usuario.
        /// </summary>
        public List<int> PermisosPuedeEntrar {
            get { return permisosPuedeEntrar; }
        }

        public static DataTable DevuelveAccionesPorModuloID(int UsuarioID,int ModuloID)
        {
            DataTable dataTableAcciones = new DataTable();
            string[] permisosBasicos = new string[] {"Puede_Entrar"};
            using (var dbContext = new SIPPermisosContext())
            {
                
                /*
                var atrributos = from atrib in dbContext.PermisosModuloAtributos 
                                 join esp   in dbContext.PermisosUsuarioEspeciales 
                                 on atrib.ModuloId equals esp.Modulo_Id
                                 where atrib.ModuloId == ModuloID && !permisosBasicos.Contains(atrib.AtributoAccion) && esp.UsuarioId == UsuarioID
                                 select atrib;
                */
                var atrributos = from atrib in dbContext.PermisosModuloAtributos
                                 from esp in dbContext.PermisosUsuarioEspeciales
                                 where
                                    atrib.ModuloId == esp.Modulo_Id
                                 &&
                                    atrib.Id == esp.ModuloAtributo_Id
                                 &&
                                    atrib.ModuloId == ModuloID
                                 &&
                                    !permisosBasicos.Contains(atrib.AtributoAccion)
                                 &&
                                    esp.UsuarioId == UsuarioID
                                 select atrib;
                
                dataTableAcciones = Linq2DataTable.CopyToDataTable(atrributos);
                
                return dataTableAcciones;
            }
        }
        //Métodos propios:
        public Usuario Consultar(string NombreDeUsuario)
        {
            Usuario blUsuario = new Usuario();
            try
            {
                using (var dbContext = new SIPPermisosContext())
                {
                    var usuario = dbContext.PermisosUsuarios.Where(u => u.UsuarioUsuario == NombreDeUsuario).ToList()[0];
                    //SE CONSULTA EL PERFIL
                    var perfil = (from p in dbContext.PermisosPerfiles
                                  join pu in dbContext.PermisosUsuarioPerfiles on p.Id equals pu.PerfilId
                                  where pu.UsuarioId == usuario.Id
                                  select new { PerfilNombre = p.PerfilNombre }).FirstOrDefault();

                    blUsuario.UsuarioPerfil = perfil==null?"":perfil.PerfilNombre;

                    //Se consultan los permisos:
                     var x = dbContext.vw_PermisosPorAtributo.Where(p => p.Id == usuario.Id && p.AtributoAccion == "Puede_Entrar").Select(u=> new { u.ModuloId }).ToList();
                    foreach (var idModulo in x)
                    {
                        blUsuario.PermisosPuedeEntrar.Add(idModulo.ModuloId);
                    }
                     

                    CopyClass.CopyObject(usuario, ref blUsuario);
                }
            }
            catch (Exception Ex)
            {
                tieneError = true;
                error = Ex;
            }
            return blUsuario;
        }
        //Métodos de la interface:
        public bool TieneError
        {
            get { return tieneError; }
        }

        public Exception Error
        {
            get { return error; }
        }
        public Usuario Consultar(int ID)
        {
            Usuario blUsuario = new Usuario();
            try
            {
                using (var dbContext = new SIPPermisosContext())
                {
                    var usuario = dbContext.PermisosUsuarios.Where(u => u.Id == ID).ToList()[0];

                    //Se consultan los permisos:
                    var x = dbContext.vw_PermisosPorAtributo.Where(p => p.Id == usuario.Id && p.AtributoAccion == "Puede_Entrar").Select(u => new { u.ModuloId }).ToList();
                    foreach (var idModulo in x)
                    {
                        blUsuario.PermisosPuedeEntrar.Add(idModulo.ModuloId);
                    }


                    CopyClass.CopyObject(usuario, ref blUsuario);
                }
                using (var dbContext = new AspelSae80Context())
                {
                    var query = (from u in dbContext.USUARIOS where u.CLAVE.Trim() == blUsuario.UsuarioUsuario select u).SingleOrDefault();

                    USUARIOS usr = new USUARIOS();
                    CopyClass.CopyObject(query, ref usr);
                    blUsuario.UsuarioSae = usr;
                }
            }
            catch (Exception Ex)
            {
                tieneError = true;
                error = Ex;
            }
            return blUsuario;
        }

        public void Crear(Usuario tEntidad)
        {
            //Código para crear un usuario nuevo
            try
            {
                using (var dbContext = new SIPPermisosContext())
                {
                    PermisosUsuarios permisosUsuarios = new PermisosUsuarios();
                    CopyClass.CopyObject(tEntidad, ref permisosUsuarios);
                    dbContext.PermisosUsuarios.Add(permisosUsuarios);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                tieneError = true;
                error = ex;
            }
            
        }

        public void CrearSae60(Usuario tEntidad,string Acceso,string TipoUsuario)
        {
            using (AspelSae80Context dbContext = new AspelSae80Context())
            {
                var usuario = new ulp_dl.aspel_sae80.USUARIOS();
                usuario.NOMBRE = tEntidad.UsuarioNombre;
                usuario.CLAVE = tEntidad.UsuarioUsuario;
                usuario.PASSWORD = tEntidad.UsuarioContraseña;
                usuario.ACCESO = Acceso;
                usuario.TIPO_USUARIO = TipoUsuario;
                usuario.INDICE = 1;
                usuario.CLAVE_TEL = "1";
                usuario.ACTIVO = true;

                dbContext.USUARIOS.Add(usuario);
                dbContext.SaveChanges();

            }
        }

        public void Modificar(Usuario tEntidad)
        {
            //Código para modificar información del usuario
            throw new NotImplementedException();
        }

        public void Borrar(Usuario tEntidad, Enumerados.TipoBorrado TipoBorrado)
        {
            
            /*Código para borrar información del usuario, ya sea
             * de forma lógica o física, para distinguir lo anterior
             * se ocupa el enumerado de los parámetros
             */
            throw new NotImplementedException();
        }

        public static List<Usuario> ObtenerTodos()
        {
            List<Usuario> blUsuarios = new List<Usuario>();
            
            using (var dbContext = new SIPPermisosContext())
            {
                List<PermisosUsuarios> usuarios = dbContext.PermisosUsuarios.OrderBy(n => n.UsuarioNombre).ToList();
                foreach (PermisosUsuarios usuario in usuarios)
                {
                    Usuario blusuario = new Usuario();
                    CopyClass.CopyObject(usuario, ref blusuario);
                    blUsuarios.Add(blusuario);
                }
                //CopyClass.CopyObject(usuarios, ref blUsuarios);
            }
            return blUsuarios;
        }

        public static void SalvarPermisos(Enumerados.TipoPermiso TipoPermiso,List<int> Permisos,int IdUsuario)
        {
            string atributo = "";
            switch (TipoPermiso)
            {
                case Enumerados.TipoPermiso.PuedeEntrar:
                    atributo = "Puede_Entrar";
                    break;
                default:
                    break;
            }
            using (var dbContext = new SIPPermisosContext())
            {
                using (var dbContextTransaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        
                        //se borran todos los permisos del su
                        //var registrosABorrar = dbContext.PermisosUsuarioEspeciales.Where(u => u.UsuarioId == IdUsuario).ToList().ForEach(dbContext.PermisosUsuarioEspeciales.de);
                        var cmd = from perm in dbContext.PermisosUsuarioEspeciales
                                  from atrib in dbContext.PermisosModuloAtributos
                                  where
                                    perm.Modulo_Id == atrib.ModuloId && atrib.AtributoAccion == atributo
                                 &&
                                    perm.ModuloAtributo_Id == atrib.Id
                                 &&
                                    perm.UsuarioId == IdUsuario select perm;
                                
                        dbContext.PermisosUsuarioEspeciales.RemoveRange(cmd);
                        var resultdados =
                            dbContext.PermisosModuloAtributos.Where(
                                p => Permisos.Contains(p.ModuloId) && p.AtributoAccion == atributo).ToList();

                        foreach (var permiso in resultdados)
                        {
                            PermisosUsuarioEspeciales permisosEspeciales = new PermisosUsuarioEspeciales();

                            permisosEspeciales.Modulo_Id = 0;
                            permisosEspeciales.AtributoId = 0;
                            permisosEspeciales.UsuarioId = IdUsuario;
                            permisosEspeciales.Modulo_Id = permiso.ModuloId;
                            permisosEspeciales.ModuloAtributo_Id = permiso.Id;
                            dbContext.PermisosUsuarioEspeciales.Add(permisosEspeciales);
                        }
                        dbContext.SaveChanges();
                        dbContextTransaction.Commit();
                    }
                    catch (Exception Ex)
                    {
                        dbContextTransaction.Rollback();
                    }
                }

                

            }
        }
        public static void SalvarPermisos(int ModuloID, List<int> Permisos, int IdUsuario)
        {
            string[] atributosBasicos = new string[] { "Puede_Entrar" };
            
                   // atributo = TipoPermiso;
            
            using (var dbContext = new SIPPermisosContext())
            {
                using (var dbContextTransaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        //se borran todos los permisos del su
                        //var registrosABorrar = dbContext.PermisosUsuarioEspeciales.Where(u => u.UsuarioId == IdUsuario).ToList().ForEach(dbContext.PermisosUsuarioEspeciales.de);
                        var cmd = from perm in dbContext.PermisosUsuarioEspeciales
                                  from atrib in dbContext.PermisosModuloAtributos
                                  where
                                  perm.ModuloAtributo_Id == atrib.Id
                                  &&
                                    perm.Modulo_Id == atrib.ModuloId
                                  &&
                                    !atributosBasicos.Contains(atrib.AtributoAccion)
                                  &&
                                    perm.UsuarioId == IdUsuario && perm.Modulo_Id == ModuloID select perm;

                        dbContext.PermisosUsuarioEspeciales.RemoveRange(cmd);
                        //var resultdados = dbContext.PermisosModuloAtributos.Where(p => Permisos.Contains(p.ModuloId) && !atributosBasicos.Contains(p.AtributoAccion)).ToList();

                        foreach (int AtributoID in Permisos)
                        {
                            PermisosUsuarioEspeciales permisosEspeciales = new PermisosUsuarioEspeciales();

                            permisosEspeciales.Modulo_Id = 0;
                            permisosEspeciales.AtributoId = 0;
                            permisosEspeciales.UsuarioId = IdUsuario;
                            permisosEspeciales.Modulo_Id = ModuloID;
                            permisosEspeciales.ModuloAtributo_Id = AtributoID;
                            dbContext.PermisosUsuarioEspeciales.Add(permisosEspeciales);
                        }
                        dbContext.SaveChanges();
                        dbContextTransaction.Commit();
                    }
                    catch (Exception Ex)
                    {
                        dbContextTransaction.Rollback();
                    }
                }



            }
        }


        public DataTable ConsultarTodos()
        {
            throw new NotImplementedException();
        }
       
    }
}
