using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ulp_dl.aspel_prod30
{
    [Table("PT_DET01")]
    public class PT_DET01
    {
        [Key]
        public int NUM_REG { get; set; }
        public string CLAVE { get; set; }
        public string PROCESO { get; set; }
        public string COMPONENTE { get; set; }
        public short? ALMACEN { get; set; }
        public short? TIPOCOMP { get; set; }
        public double? CANTIDAD { get; set; }
        public double? COSTOU { get; set; }
        public string TIPOG { get; set; }
        public double? TIEMPOPROC { get; set; }
        public short? SECUENCIA { get; set; }
        public short? NUMCOMPO { get; set; }
        public int? OBSDPT { get; set; }
        public string RESTO { get; set; }
    }
}
