using System;
using System.Collections.Generic;
using System.Linq;
using ulp_dl;
using ulp_bl.Utiles;
using ulp_dl.aspel_sae80;
using System.Text;

namespace ulp_bl
{
    public class USUARIOS : ICrud<USUARIOS>
    {
        public int ID_USUARIO { get; set; }
        public string NOMBRE { get; set; }
        public string CLAVE { get; set; }
        public string PASSWORD { get; set; }
        public string ACCESO { get; set; }
        public string TIPO_USUARIO { get; set; }
        public string GERENTE { get; set; }
        public int? INDICE { get; set; }
        public string CLAVE_TEL { get; set; }
        public decimal? PRESUPUESTO { get; set; }
        public bool? ACTIVO { get; set; }
        public string DEPARTAMENTO { get; set; }
        public string SKIN { get; set; }

        public bool TieneError
        {
            get { throw new NotImplementedException(); }
        }

        public Exception Error
        {
            get { throw new NotImplementedException(); }
        }

        public USUARIOS ConsultarPorUsuario(string CLAVE)
        {
            USUARIOS datos_usuario = new USUARIOS();
            using (var dbContext = new AspelSae80Context())
            {
                //var resultado = dbContext.USUARIOS.First(CLAVE).CLAVE;
                var resultado = (from usr in dbContext.USUARIOS where usr.CLAVE == CLAVE select usr).First();
                CopyClass.CopyObject(resultado, ref datos_usuario);
            }
            return datos_usuario;
        }

        public USUARIOS Consultar(int ID)
        {
            USUARIOS datos_usuario = new USUARIOS();
            using (var dbContext = new AspelSae80Context())
            {
                var resultado = dbContext.USUARIOS.Find(ID);
                CopyClass.CopyObject(datos_usuario, ref resultado);
            }
            return datos_usuario;
        }

        public void Crear(USUARIOS tEntidad)
        {
            throw new NotImplementedException();
        }

        public void Modificar(USUARIOS tEntidad)
        {
            throw new NotImplementedException();
        }

        public void Borrar(USUARIOS tEntidad, Enumerados.TipoBorrado TipoBorrado)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable ConsultarTodos()
        {
            throw new NotImplementedException();
        }

        public string GetClasificacionVendedor(string clave)
        {
            using (var dbContext = new AspelSae80Context())
            {
                //var resultado = dbContext.USUARIOS.First(CLAVE).CLAVE;
                var resultado = (from v in dbContext.VEND01 where v.CVE_VEND == clave select v).First();
                return resultado.CLASIFIC;
            }
        }
    }
}
