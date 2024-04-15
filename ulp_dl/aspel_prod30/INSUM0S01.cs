using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ulp_dl.aspel_prod30
{
    [Table("INSUM0S01")]
    public partial class INSUM0S01
    {
        [Key()]
        public Int16 CLAVE { get; set; }
        public int NUM_REGS { get; set; }
        public int ULT_CLV { get; set; }
        public Int16 BLOQUEO { get; set; }

    }
}
