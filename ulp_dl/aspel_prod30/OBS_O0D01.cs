using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ulp_dl.aspel_prod30
{
    [Table("OBS_O0D01")]
    public class OBS_O0D01
    {
        [Key()]
        public short? CLAVE { get; set; }
        public int? NUM_REGS { get; set; }
        public int? ULT_CLV { get; set; }
        public short? BLOQUEO { get; set; }

    }
}
