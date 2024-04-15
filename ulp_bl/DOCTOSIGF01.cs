using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ulp_dl.aspel_sae80;
using ulp_dl;
using ulp_bl.Utiles;

namespace ulp_bl
{
    public class DOCTOSIGF01 : ICrud<DOCTOSIGF01>
    {
        public string TIP_DOC { get; set; }
        public string CVE_DOC { get; set; }
        public string ANT_SIG { get; set; }
        public string TIP_DOC_E { get; set; }
        public string CVE_DOC_E { get; set; }
        public int? PARTIDA { get; set; }
        public int? PART_E { get; set; }
        public double? CANT_E { get; set; }

        public bool TieneError
        {
            get { throw new NotImplementedException(); }
        }

        public Exception Error
        {
            get { throw new NotImplementedException(); }
        }

        public DOCTOSIGF01 Consultar(int ID)
        {
            throw new NotImplementedException();
        }
        public DOCTOSIGF01 Consultar(string pedido)
        {
            String pP, pD, pM, pE, pMP;
            DOCTOSIGF01 doctosig = new DOCTOSIGF01();
            pP = string.Format("P{0}", pedido);
            pD = string.Format("D{0}", pedido);
            pM = string.Format("M{0}", pedido);
            pE = string.Format("E{0}", pedido);
            pMP = string.Format("MP{0}", pedido);
            using (var dbContext = new AspelSae80Context())
            {
                var result = (from doc in dbContext.DOCTOSIGF01
                              where doc.TIP_DOC == "F" && doc.ANT_SIG == "A" && doc.TIP_DOC_E == "P" && (
                                doc.CVE_DOC_E == pP ||
                                doc.CVE_DOC_E == pD ||
                                doc.CVE_DOC_E == pM ||
                                doc.CVE_DOC_E == pE ||
                                doc.CVE_DOC_E == pMP
                              )
                              orderby doc.CVE_DOC descending
                              select doc).FirstOrDefault();
                CopyClass.CopyObject(result, ref doctosig);
            }
            return doctosig;
        }

        public void Crear(DOCTOSIGF01 tEntidad)
        {
            throw new NotImplementedException();
        }

        public void Modificar(DOCTOSIGF01 tEntidad)
        {
            throw new NotImplementedException();
        }

        public void Borrar(DOCTOSIGF01 tEntidad, Enumerados.TipoBorrado TipoBorrado)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable ConsultarTodos()
        {
            throw new NotImplementedException();
        }
    }
}
