using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ulp_dl.aspel_prod30
{
    [Table("U_DEPARTAMENTO")]
    public class U_DEPARTAMENTO
    {
        [Key()]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        [MaxLength(20)]
        public string DEPARTAMENTO { get; set; }

    }
}
