using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ulp_dl.aspel_sae80;
using ulp_bl.Utiles;
using ulp_dl.SIPNegocio;


namespace ulp_bl
{
    public class UPPEDIDOS : ICrud<UPPEDIDOS>
    {
        public int ID { get; set; }
        public double PEDIDO { get; set; }
        public string COD_CLIENTE { get; set; }
        public DateTime? F_VENCIMIENTO { get; set; }
        public DateTime? F_VENCIMIENTO2 { get; set; }
        public DateTime? F_ESTANDAR { get; set; }
        public string PROCESOS { get; set; }
        public string COMENTARIOS { get; set; }
        public DateTime? F_CAPT { get; set; }
        public DateTime? F_IMPRESION { get; set; }
        public DateTime? F_GESTION { get; set; }
        public DateTime? F_CAPT_ASPEL { get; set; }
        public DateTime? F_CREDITO { get; set; }
        public DateTime? F_ASIG_RUTA { get; set; }
        public DateTime? F_LIBERADO { get; set; }
        public DateTime? F_SURTIDO { get; set; }
        public DateTime? F_BORDADO { get; set; }
        public DateTime? F_COSTURA { get; set; }
        public DateTime? F_CORTE { get; set; }
        public DateTime? F_ESTAMPADO { get; set; }
        public DateTime? F_INICIALES { get; set; }
        public DateTime? F_EMPAQUE { get; set; }
        public DateTime? F_EMBARQUE { get; set; }
        public DateTime? FECHAPEDIDO { get; set; }
        public DateTime? FECHARUTA { get; set; }
        public string GUIA { get; set; }
        public int? CAJAS { get; set; }
        public string CHOFER { get; set; }
        public string DEPARTAMENTO { get; set; }
        public string DESTINO { get; set; }
        public string OBSERVACIONES { get; set; }
        public string ESTATUS { get; set; }
        public string TRANSPORTE { get; set; }
        public string COM_SURTIDO { get; set; }
        public string COM_BORDADO { get; set; }
        public string COM_COSTURA { get; set; }
        public string COM_CORTE { get; set; }
        public string COM_ESTAMPADO { get; set; }
        public string COM_INICIALES { get; set; }
        public string COM_EMPAQUE { get; set; }
        public string COM_CREDITO { get; set; }
        public string ESTATUS_UPPEDIDOS { get; set; }
        public DateTime? F_ENTREGADO { get; set; }
        public string COM_ENTREGA { get; set; }
        public DateTime? F_COORDINADOR { get; set; }
        public DateTime? F_DIRECCION { get; set; }
        public DateTime? F_GV { get; set; }
        public bool TieneError
        {
            get { throw new NotImplementedException(); }
        }

        public Exception Error
        {
            get { throw new NotImplementedException(); }
        }
        /// <summary>
        /// Devuelve un registro de UPPedidos
        /// </summary>
        /// <param name="ID">Clave del Pedido</param>
        /// <returns></returns>
        public UPPEDIDOS Consultar(int ID)
        {
            UPPEDIDOS uppedidosResultado = new UPPEDIDOS();
            using (var dbContext = new AspelSae80Context())
            {
                var up = from pedido in dbContext.UPPEDIDOS where pedido.PEDIDO == ID  select pedido;

                var upp = up.ToList();

                if (upp.Count == 1)
                {
                    CopyClass.CopyObject(upp[0], ref uppedidosResultado);
                }
                else
                {
                    uppedidosResultado = null;
                }
                

            }
            return uppedidosResultado;
        }

        public void Crear(UPPEDIDOS tEntidad)
        {

            
            LogUPPedidos upPedidoNvo = new LogUPPedidos();


            ulp_dl.aspel_sae80.UPPEDIDOS uppedidos = new ulp_dl.aspel_sae80.UPPEDIDOS();
            using (var dbContext = new AspelSae80Context())
            {
                CopyClass.CopyObject(tEntidad, ref uppedidos);
                CopyClass.CopyObject(uppedidos, ref upPedidoNvo);
                dbContext.UPPEDIDOS.Add(uppedidos);
                dbContext.SaveChanges();
            }
            UpPedidosLog.RegistraEntrada(null, upPedidoNvo, "ALTA","Pedidos");
        }

        public void Modificar(UPPEDIDOS tEntidad)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Actualiza Tabla UPPEDIDOS
        /// </summary>
        /// <param name="tEntidad">Entidad con los valores</param>
        /// <param name="Pantalla">Pantalla que está modificando la Tabla UPPEDIDOS, se ocupa para el Log de Cambios</param>
        public void Modificar(UPPEDIDOS tEntidad,string Pantalla)
        {

            //using (var dbContext = new AspelSae50Context())
            //{
            //    var res = (from ped in dbContext.UPPEDIDOS where ped.PEDIDO == tEntidad.PEDIDO select ped).FirstOrDefault();
            //    CopyClass.CopiaValoresNoNulosClase(tEntidad, ref res);

            //    dbContext.SaveChanges();
            //}
            //////////////////////////////////////////////////////////////
            
            LogUPPedidos logUpPedidosOriginal = new LogUPPedidos();
            LogUPPedidos logUpPedidosModificado = new LogUPPedidos();
            
            using (var dbContext = new AspelSae80Context())
            {
                
                    var registroUpPedidosOriginal = (from ped in dbContext.UPPEDIDOS where ped.PEDIDO == tEntidad.PEDIDO select ped).FirstOrDefault();

                    //obtengo un snapshot del registro antes de que se modifique
                    CopyClass.CopyObject(registroUpPedidosOriginal, ref logUpPedidosOriginal);
                    
                    CopyClass.CopiaValoresNoNulosClase(tEntidad, ref registroUpPedidosOriginal);

                    //obtengo el snapshot de como quedó ya después de las modificaciones
                    CopyClass.CopiaValoresNoNulosClase(registroUpPedidosOriginal, ref logUpPedidosModificado);

                    dbContext.SaveChanges();                    
            }
            //grabo en el Log de cambios de UpPedidos

            UpPedidosLog.RegistraEntrada(logUpPedidosOriginal, logUpPedidosModificado, "MODIFICACIÓN",Pantalla);

        }

        public void Borrar(UPPEDIDOS tEntidad, Enumerados.TipoBorrado TipoBorrado)
        {
            throw new NotImplementedException();
        }


        public DataTable ConsultarTodos()
        {
            throw new NotImplementedException();
        }
    }
}
