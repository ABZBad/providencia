using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ulp_dl.aspel_prod30
{
    [Table("PRO_TERM01")]
    public class PRO_TERM01
    {
        public int? NUM_REG { set; get; } //int
        [Key()]
        public string CLAVE { set; get; } //varchar(16)
        public Int16? PT_ALMACEN { set; get; } //smallint
        public Int16? ALMACENRC { set; get; } //smallint
        public Int16? PT_MOVINV { set; get; } //smallint
        public string MOVRC { set; get; } //varchar(1)
        public double? COSTOE { set; get; } //float
        public Int16? TIPOCTO { set; get; } //smallint
        public double? TIEMPOFAB { set; get; } //float
        public string IMAGEN { set; get; } //varchar(12)
        public double? LOTESUG { set; get; } //float
        public Int16? NUMPART { set; get; } //smallint
        public Int16? PER_PROD { set; get; } //smallint
        public Int16? NUMDIASPER { set; get; } //smallint
        public string OCUPADO { set; get; } //varchar(1)
        public Int16? COSTOMP { set; get; } //smallint
        public int? OBSPT { set; get; } //int
        public string RESTO { set; get; } //varchar(60)

    }
}
