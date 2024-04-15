using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ulp_dl.aspel_sae80;
using System.Data;
using ulp_bl.Utiles;
using ulp_dl;

namespace ulp_bl
{
    public class CLIN01 :ICrud<CLIN01>
    {
        public string CVE_LIN { get; set; }
        public string DESC_LIN { get; set; }
        public string ESUNGPO { get; set; }
        public string CUENTA_COI { get; set; }
        public string STATUS { get; set; }

        public bool TieneError
        {
            get { throw new NotImplementedException(); }
        }

        public Exception Error
        {
            get { throw new NotImplementedException(); }
        }
        
        public CLIN01 Consultar(string ID)
        {
            CLIN01 CLIN01 = new CLIN01(); 
            using (var dbContext=new AspelSae80Context())
            {
                var resultado = dbContext.CLIN01.Find(ID);
                ulp_bl.Utiles.CopyClass.CopyObject(resultado, ref CLIN01);
            }
            return CLIN01;
        }
        public DataTable ConsultarCoincidencias(string DESC_LIN)
        {
            DataTable datos = new DataTable();
            using (var dbContext=new AspelSae80Context())
            {
                var resultado = from res in dbContext.CLIN01 where res.DESC_LIN.Contains(DESC_LIN) select new { CLAVE = res.CVE_LIN, NOMBRE = res.DESC_LIN };
                datos = Linq2DataTable.CopyToDataTable(resultado);
            }
            return datos;
        }
        public CLIN01 Consultar(int ID)
        {
            throw new NotImplementedException();
        }

        public void Crear(CLIN01 tEntidad)
        {
            throw new NotImplementedException();
        }

        public void Modificar(CLIN01 tEntidad)
        {
            throw new NotImplementedException();
        }

        public void Borrar(CLIN01 tEntidad, Enumerados.TipoBorrado TipoBorrado)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable ConsultarTodos()
        {
            throw new NotImplementedException();
        }
    }
}
