using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using ulp_dl.SIPPermisos;
using ulp_bl.Permisos;


namespace ulp_bl.Permisos
{
    public class Acceso
    {
        public static bool ValidarUsuario(string Usuario, string Contraseña,ref Exception Ex)
        {
            string contraseñaHash;
            contraseñaHash = Utilerias.GenerarMD5Hash(Contraseña);
            try
            {
               
                using (var dataBaseContext = new SIPPermisosContext())
                {
                    List<PermisosUsuarios> lstUsuarios =
                        dataBaseContext.PermisosUsuarios.Where(
                            u => u.UsuarioUsuario == Usuario && u.UsuarioContraseña == contraseñaHash).ToList();

                    if (lstUsuarios.Count >= 1)
                    {

                        if (lstUsuarios[0].UsuarioContraseña == contraseñaHash)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Ex = ex;
                return false;
            }
        }

    }
}
