using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ulp_bl.Utiles;
//using ulp_dl.aspel_prod30;
using ulp_dl.aspel_sae80;

namespace ulp_bl
{
    public class PT_DET01
    {        
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

        public static List<PT_DET01> ConsultarPorClave(string Clave)
        {

            List<PT_DET01> lstPT_DET01 = new List<PT_DET01>();
            using (var dbContext = new AspelSae80Context())
            {
                var query = from ptDet01 in dbContext.PROD_PRODTERM_DET01 where ptDet01.CVE_ART == Clave select ptDet01;

                foreach (PROD_PRODTERM_DET01 det01 in query)
                {
                    PT_DET01 item = new PT_DET01();
                    //CopyClass.CopyObject(det01, ref item);

                    lstPT_DET01.Add(item);

                }

                
            }
            return lstPT_DET01;
        }
    }
}
