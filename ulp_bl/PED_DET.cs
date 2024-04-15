using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ulp_dl.aspel_sae80;
using ulp_dl.SIPNegocio;
using ulp_dl;
using ulp_bl.Utiles;
using System.Data;
using sm_dl;

namespace ulp_bl
{

    public class PED_DET:ICrud<PED_DET>
    {
        public int CONTADOR { get; set; }
        public int PEDIDO { get; set; }
        public string CODIGO { get; set; }
        public string DESCRIPCION { get; set; }
        public decimal? PRECIO_PROD { get; set; }
        public double? DESCUENTO { get; set; }
        public int? CANTIDAD { get; set; }
        public decimal? PREC_PROCESO { get; set; }
        public string PROCESOS { get; set; }
        public decimal? SUBTOTAL { get; set; }
        public decimal? PRECIO_LISTA { get; set; }
        public string AGRUPADOR { get; set; }


        public bool TieneError
        {
            get { throw new NotImplementedException(); }
        }

        public Exception Error
        {
            get { throw new NotImplementedException(); }
        }

        public PED_DET Consultar(int ID)
        {
            throw new NotImplementedException();
        }        
        public DataTable ConsultarDetalle(int ID)
        {
            DataTable resultado = new DataTable();
            using (var dbContext=new  SIPNegocioContext())
            {
                //var datos = from reg in dbContext.PED_DET where reg.PEDIDO == ID select reg;
                //resultado = Linq2DataTable.CopyToDataTable(datos);                
                sm_dl.SqlServer.SqlServerCommand datos = new sm_dl.SqlServer.SqlServerCommand();
                datos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                datos.ObjectName = "usp_frmPartidas";
                datos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@idPedido", ID));
                resultado = datos.GetDataTable();
            }
            return resultado;
        }
        public DataTable ConsultarPorModeloYAgrupador(int pedido, string modelo, string agrupador)
        {
            DataTable resultado = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                //var datos = from reg in dbContext.PED_DET where reg.PEDIDO == ID select reg;
                //resultado = Linq2DataTable.CopyToDataTable(datos);                
                sm_dl.SqlServer.SqlServerCommand datos = new sm_dl.SqlServer.SqlServerCommand();
                datos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                datos.ObjectName = "usp_frmPartidas2";
                datos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@idPedido", pedido));
                datos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@modelo", modelo));
                datos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@agrupador", agrupador));
                resultado = datos.GetDataTable();
            }
            return resultado;
        }

        public bool ContieneRegistrosPedido(int ID)
        {
            bool resultado = false;
            using (var dbContext=new AspelSae80Context())
            {
                var num_registros= (from reg in dbContext.PED_DET where reg.PEDIDO==ID select reg).Count();
                if (num_registros>0)
                {
                    resultado = true;
                }
            }
            return resultado;
        }

        public void Crear(PED_DET tEntidad)
        {
            ulp_dl.aspel_sae80.PED_DET pedidos_detalle = new ulp_dl.aspel_sae80.PED_DET();
            using (var dbContext = new AspelSae80Context())
            {
                CopyClass.CopyObject(tEntidad, ref pedidos_detalle);
                dbContext.PED_DET.Add(pedidos_detalle);
                dbContext.SaveChanges();
            }
        }

        public void Modificar(PED_DET tEntidad)
        {
            using (var dbContext = new AspelSae80Context())
            {                
                var resultado = from pDet in dbContext.PED_DET where pDet.PEDIDO == tEntidad.PEDIDO && pDet.AGRUPADOR==tEntidad.AGRUPADOR select pDet;
                //copio el valor para que no falle al asignar la llave primaria
                foreach (var ped_det in resultado)
                {
                    ulp_dl.aspel_sae80.PED_DET datos_aGuardar = new ulp_dl.aspel_sae80.PED_DET();
                    datos_aGuardar = ped_det;
                    tEntidad.CONTADOR = datos_aGuardar.CONTADOR;
                    CopyClass.CopiaValoresNoNulosClase(tEntidad, ref datos_aGuardar);                    
                }
                dbContext.SaveChanges();

            }
        }

        public void Borrar(PED_DET tEntidad, Enumerados.TipoBorrado TipoBorrado)
        {
            if (TipoBorrado == Enumerados.TipoBorrado.Fisico)
            {
                using (var dbContext = new AspelSae80Context())
                {
                    var resultado = from pedDet in dbContext.PED_DET
                                    where pedDet.PEDIDO == tEntidad.PEDIDO && pedDet.AGRUPADOR.Trim() == tEntidad.AGRUPADOR.Trim() && pedDet.CODIGO.Substring(0, 8) == tEntidad.CODIGO
                                    select pedDet;
                    dbContext.PED_DET.RemoveRange(resultado);
                    dbContext.SaveChanges();
                }
            }

        }


        public System.Data.DataTable ConsultarTodos()
        {
            throw new NotImplementedException();
        }
    }
}
