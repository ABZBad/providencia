using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulp_dl.aspel_prod30
{
    [Table("INSUMOS01")]
    public class INSUMOS01 
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int? NUM_REG { get; set; } 
        public string CLAVE { get; set; } 
        public string NOMBRE { get; set; } 
        public string UNI_MED { get; set; } 
        public double? CSTO_UNIT { get; set; } 
        public string TIPO { get; set; } 
        public string CLASIF { get; set; } 
        public string TIPOG { get; set; } 
        public string RESTO { get; set; } 
    }
}
