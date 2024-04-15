using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ulp_dl.aspel_prod30
{
    [Table("ORD_FAB01")]
    public class ORD_FAB01
    {
        public int? NUM_REG { get; set; }
        [Key()]
        public string CLAVE { get; set; }
        public string PRODUCTO { get; set; }
        public double? CANTIDAD { get; set; }
        public short? PRIORIDAD { get; set; }
        public DateTime? FCAPTURA { get; set; }
        public DateTime? FENTREGA { get; set; }
        public DateTime? FINICIAL { get; set; }
        public DateTime? FTERMINA { get; set; }
        public double? CANTTERM { get; set; }
        public double? TGASDIR { get; set; }
        public double? TGASIND { get; set; }
        public DateTime? FULTMOV { get; set; }
        public string REFERENCIA { get; set; }
        public double? COSTEST { get; set; }
        public short? TIPOCTO { get; set; }
        public short? TIPOORD { get; set; }
        public string STATUS { get; set; }
        public string STATUSAV { get; set; }
        public string PROCESO { get; set; }
        public short? NUMUSU { get; set; }
        public int? REGSERIE { get; set; }
        public short? OCUPADO { get; set; }
        public short? ACT_SAE { get; set; }
        public short? HRSXDIA { get; set; }
        public int? OBSORD { get; set; }
        public string CVELOTE { get; set; }
        public DateTime? FCHCADUC { get; set; }
        public DateTime? FCHPRODUC { get; set; }
        public int? REGLOTE { get; set; }
        public string RESTO { get; set; }
    }
}
