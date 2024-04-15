using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ulp_dl.aspel_prod30
{
    [Table("PT_D0T01")]
    public class PT_D0T01
    {
        [Key()]
        public Int16 CLAVE { set; get; } //smallint        
        public int NUM_REGS { set; get; } //int
        public int? ULT_CLV { set; get; } //int
        public Int16? BLOQUEO { set; get; } //smallint

    }
}
