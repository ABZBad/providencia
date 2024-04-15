using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ulp_bl.Utiles;
using ulp_dl.aspel_sae80;

namespace ulp_bl
{
    public class ESTDPEDI : ICrud<ESTDPEDI>
    {
        private Exception exception;
        private bool tieneError;

        public string PEDIDO { get; set; }
        public int ADVO { get; set; }
        public int LIB { get; set; }
        public int SUR { get; set; }
        public int COR { get; set; }
        public int EST { get; set; }
        public int BOR { get; set; }
        public int INI { get; set; }
        public int COS { get; set; }
        public int EMP { get; set; }
        public int EMB { get; set; }
        public int ESP { get; set; }
        public DateTime FCH { get; set; }

        public bool TieneError
        {
            get { return tieneError; }
        }

        public Exception Error
        {
            get { return exception; }
        }

        public ESTDPEDI Consultar(int ID)
        {
            ESTDPEDI estdpediResult = new ESTDPEDI();
            try
            {
                using (var dbContext = new AspelSae80Context())
                {
                    var query = from p in dbContext.ESTDPEDI where p.PEDIDO == Convert.ToString(ID) select p;

                    CopyClass.CopyObject(query, ref estdpediResult);

                }
            }
            catch (Exception Ex)
            {
                estdpediResult.tieneError = true;
                estdpediResult.exception = Ex;
            }
            return estdpediResult;
        }

        public void Crear(ESTDPEDI tEntidad)
        {
            throw new NotImplementedException();
        }
        public void ModificarES(int NumeroPedido,int EstandarEspecial)
        {
            ESTDPEDI estdpediResult = new ESTDPEDI();
            string NumPedido = Convert.ToString(NumeroPedido);
            try
            {
                using (var dbContext = new AspelSae80Context())
                {
                    var std = (from e in dbContext.ESTDPEDI where e.PEDIDO == NumPedido select e).FirstOrDefault();
                    std.ESP = EstandarEspecial;
                    dbContext.SaveChanges();
                }
            }
            catch (Exception Ex)
            {
                estdpediResult.tieneError = true;
                estdpediResult.exception = Ex;
            }   
        }
        public void Modificar(ESTDPEDI tEntidad)
        {
            throw new NotImplementedException();

        }

        public void Borrar(ESTDPEDI tEntidad, Enumerados.TipoBorrado TipoBorrado)
        {
            throw new NotImplementedException();
        }


        public System.Data.DataTable ConsultarTodos()
        {
            throw new NotImplementedException();
        }
    }
}
