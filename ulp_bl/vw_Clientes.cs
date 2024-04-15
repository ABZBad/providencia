using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ulp_dl;
using ulp_dl.SIPPermisos;
using ulp_dl.SIPNegocio;
using ulp_bl.Utiles;

namespace ulp_bl
{
    public class vw_Clientes:ICrud<vw_Clientes>
    {
        public string CLAVE { get; set; }
        public string RFC { get; set; }
        public string NOMBRE_CLIENTE { get; set; }
        public string DIRECCION { get; set; }
        public string TELEFONO { get; set; }
        public string CODIGO { get; set; }
        public string ATENCION { get; set; }
        public string NOMBRE_VENDEDOR { get; set; }
        public double DESCUENTO { get; set; }
        public int DIAS_CRE { get; set; }
        public double LIM_CRED { get; set; }
        public double CRED_DISPO { get; set; }
        public double SALDO { get; set; }
        public string TEXTO { get; set; }
        public string FCH_ULTCOM { get; set; }
        public double? PZAULTANIO { get; set; }
        public string CL8 { get; set; }
        public string CVE_VEND { get; set; }
        public string STATUS { get; set; }

        public bool TieneError
        {
            get { throw new NotImplementedException(); }
        }

        public Exception Error
        {
            get { throw new NotImplementedException(); }
        }

        public vw_Clientes Consultar(int ID)
        {
            throw new NotImplementedException();
        }
        public vw_Clientes Consultar(string Clave)
        {
            vw_Clientes cliente = new vw_Clientes();
            using(var dbContext=new SIPNegocioContext())
            {
                var resultado = dbContext.vw_Clientes.Where(v => v.CLAVE.Trim() == Clave).FirstOrDefault();
                CopyClass.CopyObject(resultado, ref cliente);                
            }
            return cliente;
        }

        public void Crear(vw_Clientes tEntidad)
        {
            throw new NotImplementedException();
        }

        public void Modificar(vw_Clientes tEntidad)
        {
            throw new NotImplementedException();
        }

        public void Borrar(vw_Clientes tEntidad, Enumerados.TipoBorrado TipoBorrado)
        {
            throw new NotImplementedException();
        }


        public System.Data.DataTable ConsultarTodos()
        {
            throw new NotImplementedException();
        }
    }
}
