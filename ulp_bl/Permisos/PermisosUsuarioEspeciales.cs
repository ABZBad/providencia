using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ulp_dl.SIPPermisos;

namespace ulp_bl.Reportes
{
    public class PermisosUsuarioEspeciales
    {
        public static bool TienePermisos(int UsuarioID, int ModuloID, int ModuloAtributoID)
        {
            bool hayRegistros = false;
            using (var DbContext = new SIPPermisosContext())
            {
                var perm = from p in DbContext.PermisosUsuarioEspeciales
                    where p.UsuarioId == UsuarioID && p.Modulo_Id == ModuloID && p.ModuloAtributo_Id == ModuloAtributoID select p;

                hayRegistros = perm.Any();

            }
            return hayRegistros;
        }

        public static bool TienePermisos(int UsuarioID, int ModuloID, String CriterioNombre, String CriterioAccion)
        {
            bool hayRegistros = false;
            using (var DbContext = new SIPPermisosContext())
            {
                

                var perm = from p in DbContext.PermisosUsuarioEspeciales
                           join a in DbContext.PermisosModuloAtributos
                           on p.ModuloAtributo_Id equals a.Id
                           where p.UsuarioId == UsuarioID && a.AtributoNombre.Contains(CriterioNombre) && p.Modulo_Id == ModuloID && a.AtributoAccion.Contains(CriterioAccion)
                           select p;

                hayRegistros = perm.Any();

            }
            return hayRegistros;
        }
    }
}
