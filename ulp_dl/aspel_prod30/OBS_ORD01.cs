using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ulp_dl.aspel_prod30
{
    [Table("OBS_ORD01")]
    public class OBS_ORD01
    {
        [Key()]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NUM_REG { get; set; }
        public string X_OBSER { get; set; }
    }
}
