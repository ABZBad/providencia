using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ulp_dl.aspel_sae80;
using ulp_dl;
using ulp_bl.Utiles;

namespace ulp_bl
{
    public class BENEF
    {
        public int NUM_REG { get; set; }
        public String NOMBRE { get; set; }
        public String RFC { get; set; }
        public String CTA_CONTAB { get; set; }
        public String TIPO { get; set; }
        public String INF_GENERAL { get; set; }
        public String REFERENCIA { get; set; }
        public String BANCO { get; set; }
        public String SUCURSAL { get; set; }
        public String CUENTA { get; set; }
        public String CLABE { get; set; }
        public String CVE_BANCO { get; set; }
        public int ESBANCOEXT { get; set; }
        public String BANCODESC { get; set; }
    }
}
