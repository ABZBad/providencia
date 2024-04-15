using System;
using System.Collections.Generic;
//using System.Data.Entity.Infrastructure;
//using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using ulp_bl.Utiles;
using ulp_dl.aspel_sae80;

namespace ulp_bl
{
    public class StandPedi
    {
        public static EstandaresPedido RegresarEstandares()
        {

            EstandaresPedido estandaresPedido = new EstandaresPedido();

            using (var dbContext = new AspelSae80Context())
            {
                var query = (from std in dbContext.CSTDPEDI select std).SingleOrDefault();

                CopyClass.CopyObject(query, ref estandaresPedido);
            }
            return estandaresPedido;
        }
        public static string Crear(EstandaresPedido Estandares)
        {
            string resultado = string.Empty;
            ulp_dl.aspel_sae80.CSTDPEDI dato_a_guardar = new ulp_dl.aspel_sae80.CSTDPEDI();
            using (var dbContext = new AspelSae80Context())
            {
                CopyClass.CopyObject(Estandares, ref dato_a_guardar);
                //dato_a_guardar.ADVO = 13;
                dbContext.CSTDPEDI.Add(dato_a_guardar);
                dbContext.SaveChanges();
                /*
                try
                {
                    
                    dbContext.SaveChanges();
                }
                catch (Exception error)
                {
                    resultado = error.InnerException.ToString();                    
                } 
                 */
            }
            return resultado;
        }
        public static string ModificarEstandares(EstandaresPedido Estandares, int ADVOParaBusqueda)
        {
            using (var dbContext = new AspelSae80Context())
            {

                var query = dbContext.CSTDPEDI.FirstOrDefault();

                query.ADVO = Estandares.ADVO;
                query.SUR = Estandares.SUR;
                query.EST = Estandares.EST;
                query.INI = Estandares.INI;
                query.EMP = Estandares.EMP;
                query.LIB = Estandares.LIB;
                query.COR = Estandares.COR;
                query.BOR = Estandares.BOR;
                query.COS = Estandares.COS;
                query.EMB = Estandares.EMB;
                query.FCH_MODI = DateTime.Now;
                query.USUARIO = Estandares.USUARIO;
                try
                {
                    dbContext.SaveChanges();
                    return string.Empty;
                }
                catch (Exception Ex)
                {
                    return Ex.Message;
                }

            }
        }
    }
}