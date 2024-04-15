using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulp_dl.aspel_prod30
{
    [Table("PROCESOS01")]
    public class PROCESOS01
    {
        [Key()]
        public int? NUM_REG { get; set; }
        public string CLAVE { get; set; }
        public string NOMBRE { get; set; }
        public string LOCALIZA { get; set; }
        public string STATUS { get; set; }
        public string CTA_COI { get; set; }
        public short? DEPTO { get; set; }      
        public double? MONTO_CAP { get; set; }      
        public string RESTO { get; set; }
    }
}
