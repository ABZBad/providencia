//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ulp_dl.SIPPermisos
{
    using System;
    using System.Collections.Generic;
    
    public partial class PermisosUsuarioEspeciales
    {
        public int Id { get; set; }
        public int ModuloId { get; set; }
        public int AtributoId { get; set; }
        public int UsuarioId { get; set; }
        public int Modulo_Id { get; set; }
        public int ModuloAtributo_Id { get; set; }
    
        public virtual PermisosModuloAtributos PermisosModuloAtributos { get; set; }
        public virtual PermisosModulos PermisosModulos { get; set; }
        public virtual PermisosUsuarios PermisosUsuarios { get; set; }
    }
}
