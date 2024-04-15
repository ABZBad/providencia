using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace ulp_dl.aspel_prod30
{
    public class AspelProd30Context : DbContext
    {
        public AspelProd30Context()
            : base("name=aspel_prod30")
        {
            var adapter = (IObjectContextAdapter)this;
            var objectContext = adapter.ObjectContext;
            objectContext.CommandTimeout = 0; // value in seconds
        }

        public DbSet<INSUM0S01> INSUM0S01 { set; get; }
        public DbSet<U_DEPARTAMENTO> U_DEPARTAMENTO { set; get; }
        public DbSet<ORD_FAB01> ORD_FAB01 { set; get; }
        public DbSet<ORD_F0B01> ORD_F0B01 { set; get; }
        public DbSet<PT_DET01> PT_DET01 { set; get; }
        public DbSet<OBS_O0D01> OBS_O0D01 { set; get; }
        public DbSet<OBS_ORD01> OBS_ORD01 { set; get; }
		public DbSet<PROCESOS01> PROCESOS01 { set; get; }
        public DbSet<PRO_TERM01> PRO_TERM01 { set; get; }
        public DbSet<PRO_TE0M01> PRO_TE0M01 { set; get; }
        public DbSet<PT_D0T01> PT_D0T01 { set; get; }
        public DbSet<INSUMOS01> INSUMOS01 { set; get; }
    }
}
