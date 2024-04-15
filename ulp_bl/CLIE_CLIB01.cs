using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ulp_dl.aspel_sae80;
using ulp_dl;
using ulp_bl.Utiles;

namespace ulp_bl
{
    public class CLIE_CLIB01 : ICrud<CLIE_CLIB01>
    {
        public string CVE_CLIE { get; set; }
        public string CAMPLIB9 { get; set; }


        public bool TieneError
        {
            get { throw new NotImplementedException(); }
        }

        public Exception Error
        {
            get { throw new NotImplementedException(); }
        }

        public CLIE_CLIB01 Consultar(int ID)
        {
            throw new NotImplementedException();
        }
        public void Crear(CLIE_CLIB01 tEntidad)
        {
            throw new NotImplementedException();
        }
        public void Modificar(CLIE_CLIB01 tEntidad)
        {
            throw new NotImplementedException();
        }
        public void Borrar(CLIE_CLIB01 tEntidad, Enumerados.TipoBorrado TipoBorrado)
        {
            throw new NotImplementedException();
        }
        public DataTable ConsultarTodos()
        {
            return null;
        }
    }
}
