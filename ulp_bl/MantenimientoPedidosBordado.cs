using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ulp_dl;
using ulp_dl.aspel_sae80;

namespace ulp_bl
{
    public class MantenimientoPedidosBordado
    {
        public static DataTable RegresaTablaPedidosBordado(int NumeroPedido, ref int resultadoBusqueda)
        {
            DataTable dataTablePedidosBordado = new DataTable();
            using (var dbContext = new AspelSae80Context())
            {
                /*
                var query = from pb in dbContext.CMT_DET
                    where pb.CMT_PEDIDO == NumeroPedido
                    select new {pb.CMT_PEDIDO, pb.CMT_MODELO, pb.CMT_CANTIDAD};
                */
                var query = from pb in dbContext.PED_DET 
                    where pb.PEDIDO == NumeroPedido orderby pb.AGRUPADOR
                    group pb by new {pb.PEDIDO, MODELO = pb.CODIGO.Substring(0,8), pb.AGRUPADOR}
                    into groupBy
                    select
                        new
                        {
                            PEDIDO = groupBy.Key.PEDIDO,
                            MODELO = groupBy.Key.MODELO,
                            AGRUPADOR = groupBy.Key.AGRUPADOR,
                            CANTIDAD = groupBy.Sum(p => p.CANTIDAD)
                        };
                             
                if (query.Any())
                {
                    resultadoBusqueda = 0;
                    dataTablePedidosBordado = Linq2DataTable.CopyToDataTable(query);
                }
                else
                {
                    resultadoBusqueda = 1;
                }

                

            }
            return dataTablePedidosBordado;
        }

        public static DataTable RegresaTablaPedidosBordadoDetalle(int NumeroPedido,string Agrupador)
        {
            DataTable dataTablePedidosBordadoDetalle = new DataTable();

            using (var dbContext = new AspelSae80Context())
            {
                var query = from pb in dbContext.CMT_DET
                            where pb.CMT_PEDIDO == NumeroPedido && pb.CMT_AGRUPADOR == Agrupador && pb.CMT_PROCESO == "B"
                            select new { pb.CMT_INDX, pb.CMT_COMO, pb.CMT_MAQUILERO,pb.CMT_FACT_MAQUILA };

                if (query.Any())
                {
                    dataTablePedidosBordadoDetalle = Linq2DataTable.CopyToDataTable(query);
                }
                



            }
            return dataTablePedidosBordadoDetalle;
        }

        public static bool GuardaCampoValorCMT_DET(int NumeroPedido,string Campo,object Valor,Type TipoDato,int INDX)
        {
            Type tipoDato = TipoDato;
            if (Valor == null)
            {
                Valor = "NULL";
                tipoDato = typeof(int);
            }

            string res = AsignarRuta.GuardaCampoValorCMT_DET(NumeroPedido, Campo, Valor, tipoDato, INDX);

            if (res == string.Empty)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
