using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ulp_dl.aspel_sae80;

namespace ulp_bl
{
    public class RetroAlimManObra
    {
        /// <summary>
        /// Cerrar índice
        /// </summary>
        /// <param name="NumeroDeIndice">Número de indice a cerrar</param>
        /// <param name="ResultadoCierre">Resultado de la transacción:\n\r1=NO SE ENCONTRÓ INDICE\n\r2=LA ORDEN CON EL INDICE DADO YA HA SIDO CERRADAD</param>
        /// <returns></returns>
        public static bool CerrarIndice(int NumeroDeIndice,ref int ResultadoCierre)
        {
            bool resultadoCierre = false;
            using (var dbContext = new AspelSae80Context())
            {
                var query =from i in dbContext.CMT_DET where i.CMT_INDX == NumeroDeIndice select i;

                if (query.Any())
                {
                    var objCmt = query.First();
                    if (objCmt.CMT_ESTATUS == "C")
                    {
                        ResultadoCierre = 2;
                    }
                    else
                    {
                        objCmt.CMT_ESTATUS = "C";
                        dbContext.SaveChanges();
                        ResultadoCierre = 0;
                    }
                }
                else
                {
                    ResultadoCierre = 1;
                }
            }
            
            return resultadoCierre;
        }
    }
}
