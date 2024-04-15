using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ulp_bl.Utiles;
using ulp_dl.aspel_sae80;

namespace ulp_bl
{
    public class FLET_MSTR : ICrud<FLET_MSTR>
    {
        public int NUM_REG { get; set; }
        public string CVE_CLPV { get; set; }
        public string CVE_DOC { get; set; }
        public double CAN_TOT { get; set; }

        public bool TieneError
        {
            get { throw new NotImplementedException(); }
        }

        public Exception Error
        {
            get { throw new NotImplementedException(); }
        }

        public FLET_MSTR Consultar(string CVE_CLPV,string CVE_DOC)
        {
            FLET_MSTR fletMstr = new FLET_MSTR();
            using (var dbContext = new AspelSae80Context())
            {
                var query = dbContext.FLET_MSTR.Where(p=> p.CVE_CLPV.Trim() == CVE_CLPV && p.CVE_DOC == CVE_DOC).SingleOrDefault();
                CopyClass.CopyObject(query,ref fletMstr);
            }
            return fletMstr;
        }

        public FLET_MSTR Consultar(int ID)
        {
            throw new NotImplementedException();
        }

        public void Crear(FLET_MSTR tEntidad)
        {
            throw new NotImplementedException();
        }

        public void Modificar(FLET_MSTR tEntidad)
        {
            throw new NotImplementedException();
        }

        public void Borrar(FLET_MSTR tEntidad, Enumerados.TipoBorrado TipoBorrado)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable ConsultarTodos()
        {
            throw new NotImplementedException();
        }

        public static bool FacturaCapturada(string ClaveProveedor,string NumeroFactura)
        {
            FLET_MSTR fletMstr = new FLET_MSTR();
            fletMstr = fletMstr.Consultar(ClaveProveedor, NumeroFactura);
            if (fletMstr.CVE_DOC == null && fletMstr.CVE_CLPV == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
