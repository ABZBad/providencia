using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using ulp_dl.aspel_prod30;
using ulp_dl.aspel_sae80;
using ulp_bl.Utiles;

namespace ulp_bl
{
    public class INSUMOS01 : ICrud<INSUMOS01>
    {
        public int? NUM_REG { get; set; }
        public string CLAVE { get; set; }
        public string NOMBRE { get; set; }
        public string UNI_MED { get; set; }
        public double? CSTO_UNIT { get; set; }
        public string TIPO { get; set; }
        public string CLASIF { get; set; }
        public string TIPOG { get; set; }
        public string RESTO { get; set; }

        public bool TieneError
        {
            get { throw new NotImplementedException(); }
        }

        public Exception Error
        {
            get { throw new NotImplementedException(); }
        }

        public INSUMOS01 Consultar(int ID)
        {
            throw new NotImplementedException();
        }

        public INSUMOS01 Consultar(string CLAVE)
        {
            INSUMOS01 insumos01 = new INSUMOS01();
            using (var dbContext = new AspelSae80Context())
            {
                var resultado = (from res in dbContext.PROD_INSUMOS01
                                 where res.CVE_ART == CLAVE
                                 select new
                                 {
                                     CLAVE = res.CVE_ART,
                                     NOMBRE = res.DESCRIPCION,
                                     UNI_MED = res.UNI_MED,
                                     CSTO_UNIT = res.COSTO_UNIT,
                                     TIPO = res.TIPO,
                                     CLASIF = res.CLASIF,
                                     TIPOG = res.TIPOG
                                 }).FirstOrDefault();
                CopyClass.CopyObject(resultado, ref insumos01);
            }
            return insumos01;
        }

        public void Crear(INSUMOS01 tEntidad)
        {
            throw new NotImplementedException();
        }

        public void Modificar(INSUMOS01 tEntidad)
        {
            throw new NotImplementedException();
        }

        public void Borrar(INSUMOS01 tEntidad, Enumerados.TipoBorrado TipoBorrado)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable ConsultarTodos()
        {
            throw new NotImplementedException();
        }
    }
}
