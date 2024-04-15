using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ulp_dl.aspel_sae80;
using ulp_bl.Utiles;

namespace ulp_bl
{
    public class FACTP01 : ICrud<FACTP01>
    {
        public string TIP_DOC { get; set; }
        public string CVE_DOC { get; set; }
        public string CVE_CLPV { get; set; }
        public string STATUS { get; set; }
        public int? DAT_MOSTR { get; set; }
        public string CVE_VEND { get; set; }
        public string CVE_PEDI { get; set; }
        public DateTime FECHA_DOC { get; set; }
        public DateTime? FECHA_ENT { get; set; }
        public DateTime? FECHA_VEN { get; set; }
        public DateTime? FECHA_CANCELA { get; set; }
        public double? CAN_TOT { get; set; }
        public double? IMP_TOT1 { get; set; }
        public double? IMP_TOT2 { get; set; }
        public double? IMP_TOT3 { get; set; }
        public double? IMP_TOT4 { get; set; }
        public double? DES_TOT { get; set; }
        public double? DES_FIN { get; set; }
        public double? COM_TOT { get; set; }
        public string CONDICION { get; set; }
        public int? CVE_OBS { get; set; }
        public int? NUM_ALMA { get; set; }
        public string ACT_CXC { get; set; }
        public string ACT_COI { get; set; }
        public string ENLAZADO { get; set; }
        public string TIP_DOC_E { get; set; }
        public int? NUM_MONED { get; set; }
        public double? TIPCAMB { get; set; }
        public int? NUM_PAGOS { get; set; }
        public DateTime? FECHAELAB { get; set; }
        public double? PRIMERPAGO { get; set; }
        public string RFC { get; set; }
        public int? CTLPOL { get; set; }
        public string ESCFD { get; set; }
        public int? AUTORIZA { get; set; }
        public string SERIE { get; set; }
        public int? FOLIO { get; set; }
        public string AUTOANIO { get; set; }
        public int? DAT_ENVIO { get; set; }
        public string CONTADO { get; set; }
        public int? CVE_BITA { get; set; }
        public string BLOQ { get; set; }
        public string FORMAENVIO { get; set; }
        public double? DES_FIN_PORC { get; set; }
        public double? DES_TOT_PORC { get; set; }
        public double? IMPORTE { get; set; }
        public double? COM_TOT_PORC { get; set; }
        public string METODODEPAGO { get; set; }
        public string NUMCTAPAGO { get; set; }
        public string TIP_DOC_ANT { get; set; }
        public string DOC_ANT { get; set; }
        public string TIP_DOC_SIG { get; set; }
        public string DOC_SIG { get; set; }
        public string FORMADEPAGOSAT { get; set; }
        public string USO_CFDI { get; set; }

        public bool TieneError
        {
            get { throw new NotImplementedException(); }
        }

        public Exception Error
        {
            get { throw new NotImplementedException(); }
        }

        public FACTP01 Consultar(int ID)
        {
            throw new NotImplementedException();
        }
        public FACTP01 ConsultarIdCadena(string ID)
        {
            FACTP01 resultado = new FACTP01();
            using (var dbContext = new AspelSae80Context())
            {
                var datos = (from res in dbContext.FACTP01 where res.CVE_DOC.Trim().Substring(1) == ID.Trim() select res).FirstOrDefault();
                CopyClass.CopyObject(datos, ref resultado);
            }
            return resultado;
        }

        public void Crear(FACTP01 tEntidad)
        {
            throw new NotImplementedException();
        }

        public void Modificar(FACTP01 tEntidad)
        {
            throw new NotImplementedException();
        }

        public void Borrar(FACTP01 tEntidad, Enumerados.TipoBorrado TipoBorrado)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable ConsultarTodos()
        {
            throw new NotImplementedException();
        }
    }
}
