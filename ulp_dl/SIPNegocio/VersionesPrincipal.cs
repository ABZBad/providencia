//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ulp_dl.SIPNegocio
{
    using System;
    using System.Collections.Generic;
    
    public partial class VersionesPrincipal
    {
        public VersionesPrincipal()
        {
            this.VersionesDetalles = new HashSet<VersionesDetalle>();
        }
    
        public int Id { get; set; }
        public string VersionesVersion { get; set; }
        public System.DateTime VersionFecha { get; set; }
        public string VersionUsuario { get; set; }
    
        public virtual ICollection<VersionesDetalle> VersionesDetalles { get; set; }
    }
}