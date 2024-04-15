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
    
    public partial class PermisosModulos
    {
        public PermisosModulos()
        {
            this.PermisosModuloAtributos = new HashSet<PermisosModuloAtributos>();
            this.PermisosUsuarioEspeciales = new HashSet<PermisosUsuarioEspeciales>();
        }
    
        public int Id { get; set; }
        public int GrupoId { get; set; }
        public string Descripcion { get; set; }
        public decimal MenuOrigen { get; set; }
        public decimal OrdenMenu { get; set; }
        public bool PuedeEntrar { get; set; }
        public bool PuedeInsertar { get; set; }
        public bool PuedeModificar { get; set; }
        public bool PuedeBorrar { get; set; }
    
        public virtual PermisosGrupos PermisosGrupos { get; set; }
        public virtual ICollection<PermisosModuloAtributos> PermisosModuloAtributos { get; set; }
        public virtual ICollection<PermisosUsuarioEspeciales> PermisosUsuarioEspeciales { get; set; }
    }
}
