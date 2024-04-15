using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ulp_dl;
//using ulp_dl.aspel_prod30;
using ulp_dl.aspel_sae80;
using ulp_bl.Utiles;

namespace ulp_bl
{
    public class PROCESOS01:ICrud<PROCESOS01>
    {
        public int? NUM_REG { get; set; }
        public string CLAVE { get; set; }
        public string NOMBRE { get; set; }
        public string LOCALIZA { get; set; }
        public string STATUS { get; set; }
        public string CTA_COI { get; set; }
        public short? DEPTO { get; set; }
        public double? MONTO_CAP { get; set; }
        public string RESTO { get; set; }

        public bool TieneError
        {
            get { throw new NotImplementedException(); }
        }

        public Exception Error
        {
            get { throw new NotImplementedException(); }
        }

        public PROCESOS01 Consultar(int ID)
        {
            throw new NotImplementedException();
        }

        public DataTable ConsultarCoincidencias(string nombre)
        {
            DataTable datos = new DataTable();
            using (var dbContext=new AspelSae80Context())
            {
                var resultado = from res in dbContext.PROD_PROCESOS01 where res.DESCRIPCION.Contains(nombre) select new { CLAVE = res.CVE_PROC, NOMBRE = res.DESCRIPCION };
                datos = Linq2DataTable.CopyToDataTable(resultado);
            }            
            return datos;
        }


        public void Crear(PROCESOS01 tEntidad)
        {
            throw new NotImplementedException();
        }

        public void Modificar(PROCESOS01 tEntidad)
        {
            throw new NotImplementedException();
        }

        public void Borrar(PROCESOS01 tEntidad, Enumerados.TipoBorrado TipoBorrado)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable ConsultarTodos()
        {
            throw new NotImplementedException();
        }
    }
}
