using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ulp_dl.aspel_prod30
{
    [Table("PRO_TE0M01")]
    public class PRO_TE0M01
    {
        [Key()]
        public Int16 CLAVE { set; get; } //smallint        
        public int NUM_REGS { set; get; } //int
        public int? ULTCLAVE { set; get; } //int
        public Int16? VERBD { set; get; } //smallint
        public Int16? BLOQUEO { set; get; } //smallint

    }
}
