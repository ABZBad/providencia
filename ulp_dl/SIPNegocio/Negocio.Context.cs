﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SIPNegocioContext : DbContext
    {
        public SIPNegocioContext()
            : base("name=SIPNegocioContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<vw_EstadoCuenta> vw_EstadoCuenta { get; set; }
        public DbSet<VersionesDetalle> VersionesDetalles { get; set; }
        public DbSet<VersionesPrincipal> VersionesPrincipals { get; set; }
        public DbSet<vw_Inventario> vw_Inventario { get; set; }
        public DbSet<LogUPPedidos> LogUPPedidos { get; set; }
        public DbSet<COST_ENLA> COST_ENLA { get; set; }
        public DbSet<COST_MSTR> COST_MSTR { get; set; }
        public DbSet<vw_Clientes> vw_Clientes { get; set; }
    }
}
