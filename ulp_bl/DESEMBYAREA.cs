using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ulp_bl.Utiles;
using ulp_dl.aspel_sae80;

namespace ulp_bl
{
    public class DESEMBYAREA :ICrud<DESEMBYAREA>
    {

        private bool tieneError;
        private Exception exception;

        public string PEDIDO { get; set; }
        public string DEPTO { get; set; }
        public string CUMPLIO { get; set; }
        public string OBSERVACIONES { get; set; }


        public bool TieneError
        {
            get { return tieneError; }
        }

        public Exception Error
        {
            get { return exception; }
        }

        public List<DESEMBYAREA> Consultar(string NumeroPedido)
        {
            List<DESEMBYAREA> desemByAreaResult = new List<DESEMBYAREA>();
            
                using (var dbContext = new AspelSae80Context())
                {
                    var query = from p in dbContext.DESEMBYAREA where p.PEDIDO == NumeroPedido select p;

                    var queryList = query.ToList();
                    foreach (ulp_dl.aspel_sae80.DESEMBYAREA dby in queryList)
                    {
                        DESEMBYAREA dlDbaItem = new DESEMBYAREA();

                        CopyClass.CopyObject(dby, ref dlDbaItem);

                        desemByAreaResult.Add(dlDbaItem);
                    }
                    

                     
                }
            
            return desemByAreaResult;
        }

        public DESEMBYAREA Consultar(int ID)
        {
            throw new NotImplementedException();
        }

        public void Crear(DESEMBYAREA tEntidad)
        {

            ulp_dl.aspel_sae80.DESEMBYAREA desemByArea = new ulp_dl.aspel_sae80.DESEMBYAREA();
            desemByArea.CUMPLIO = tEntidad.CUMPLIO;
            desemByArea.DEPTO = tEntidad.DEPTO;
            desemByArea.OBSERVACIONES = tEntidad.OBSERVACIONES;
            desemByArea.PEDIDO = tEntidad.PEDIDO;
            
            
            using (var dbContext = new AspelSae80Context())
            {
                dbContext.DESEMBYAREA.Add(desemByArea);
                dbContext.SaveChanges();

            }
            
        }

        public void Modificar(DESEMBYAREA tEntidad)
        {
            //ulp_dl.aspel_sae50.DESEMBYAREA desemByArea = new ulp_dl.aspel_sae50.DESEMBYAREA();
            


            using (var dbContext = new AspelSae80Context())
            {
                var desemByArea =
                    (from desem in dbContext.DESEMBYAREA
                        where desem.PEDIDO == tEntidad.PEDIDO && desem.DEPTO == tEntidad.DEPTO
                        select desem).FirstOrDefault();

                desemByArea.CUMPLIO = tEntidad.CUMPLIO;
                desemByArea.DEPTO = tEntidad.DEPTO;
                desemByArea.OBSERVACIONES = tEntidad.OBSERVACIONES;
                desemByArea.PEDIDO = tEntidad.PEDIDO;
                dbContext.SaveChanges();

            }
        }

        public void Borrar(DESEMBYAREA tEntidad, Enumerados.TipoBorrado TipoBorrado)
        {
            throw new NotImplementedException();
        }


        public System.Data.DataTable ConsultarTodos()
        {
            throw new NotImplementedException();
        }
    }
}
