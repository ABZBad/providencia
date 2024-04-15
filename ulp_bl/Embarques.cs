using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using ulp_dl.aspel_sae80;

namespace ulp_bl
{
    public class Embarques
    {
        public static UPPEDIDOS RegresaUpPedido(int NumeroPedido)
        {
            UPPEDIDOS upPedido = new UPPEDIDOS();


            upPedido = upPedido.Consultar(NumeroPedido);

            return upPedido;

        }

        public static string ModificarPedido(UPPEDIDOS Pedido)
        {
            using (var dbContext = new AspelSae80Context())
            {
                StringBuilder sbErrores = new StringBuilder();

                var query = (from up in dbContext.UPPEDIDOS where up.PEDIDO == Pedido.PEDIDO select up).FirstOrDefault();

                query.GUIA = Pedido.GUIA;
                query.CAJAS = Pedido.CAJAS;
                query.CHOFER = Pedido.CHOFER;                
                query.DEPARTAMENTO = Pedido.DEPARTAMENTO;
                query.DESTINO = Pedido.DESTINO;
                query.ESTATUS = Pedido.ESTATUS;
                query.TRANSPORTE = Pedido.TRANSPORTE;
                query.FECHARUTA = Pedido.FECHARUTA;
                query.OBSERVACIONES = Pedido.OBSERVACIONES;
                try
                {
                    dbContext.SaveChanges();
                    return string.Empty;
                }
                catch (DbUpdateException e)
                {
                    sbErrores.Append(e.InnerException.Message);
                    return sbErrores.ToString();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {

                        sbErrores.Append(string.Format("La entidad \"{0}\" en estado \"{1}\" tuvo los siguientes errores de validación:" + Environment.NewLine,
                            eve.Entry.Entity.GetType().Name, eve.Entry.State));
                        foreach (var ve in eve.ValidationErrors)
                        {
                            sbErrores.Append(string.Format("- Propiedad: \"{0}\", Error: \"{1}\"" + Environment.NewLine,
                                ve.PropertyName, ve.ErrorMessage));
                        }
                    }
                    return sbErrores.ToString();
                }
            }
            
        }

    }
}
