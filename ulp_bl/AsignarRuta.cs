using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ulp_dl;
//using ulp_dl.aspel_prod30;
using ulp_dl.aspel_sae80;

namespace ulp_bl
{
    public class AsignarRuta
    {
        public static DataTable RegresaRutaProcesosPedido(int NumeroPedido)
        {
            DataTable dataTablePedidoRuta = new DataTable();


            //dataTablePedidoRuta.Columns.Add("CMT_PEDIDO", typeof(int));
            //dataTablePedidoRuta.Columns.Add("CMT_MODELO", typeof(string));
            //dataTablePedidoRuta.Columns.Add("CMT_CANTIDAD", typeof(int));
            //dataTablePedidoRuta.Columns.Add("CMT_RUTA", typeof(string));

            CMT_DET c = new CMT_DET();
            //c = c.ConsultarDistinct(NumeroPedido);
            dataTablePedidoRuta = c.ConsultarPedidos(NumeroPedido);

            /*
             * Ajuste
             *              
             * 
             */
            bool reconsultar = false;
            using (var dbContext = new AspelSae80Context())
            {

                foreach (DataRow dataRow in dataTablePedidoRuta.Rows)
                {
                    if (dataRow["RUTA"].ToString() == "" || dataRow["RUTA"].ToString() == "--")
                    {
                        reconsultar = true;
                        string agrupador = dataRow["AGRUPADOR"].ToString();
                        var query = from cmt in dbContext.CMT_DET where cmt.CMT_PEDIDO == NumeroPedido && cmt.CMT_AGRUPADOR == agrupador  select cmt;

                        ulp_dl.aspel_sae80.PED_DET pedDet = (from pdet in dbContext.PED_DET where pdet.PEDIDO == NumeroPedido && pdet.AGRUPADOR == agrupador select pdet).First();

                        foreach (ulp_dl.aspel_sae80.CMT_DET cmtDet in query)
                        {
                            cmtDet.CMT_MAQUILERO = "1";
                            cmtDet.CMT_MODELO = dataRow["MODELO"].ToString().Trim();
                            cmtDet.CMT_RUTA = pedDet.PROCESOS.Trim();
                            using (var dbContext2 = new AspelSae80Context())
                            {
                                cmtDet.CMT_CLIENTE = dbContext2.PED_MSTR.Find(NumeroPedido).CLIENTE;
                            }
                            
                            cmtDet.CMT_ESTATUS = "P";
                            cmtDet.CMT_CANTIDAD = pedDet.CANTIDAD;
                            cmtDet.CMT_COS_PROCESO = 0;

                        }
                        if (query.Any())
                        {
                            dbContext.SaveChanges();
                        }
                    }
                }
            }            

            if (reconsultar)
            {
                dataTablePedidoRuta = null;
                dataTablePedidoRuta = c.ConsultarPedidos(NumeroPedido);
            }

            return dataTablePedidoRuta;
        }

        public static DataTable RegresaRutaProcesosPedidoDetalle(int NumeroPedido,string Modelo,string Agrupador)
        {
            DataTable dataTablePedidoRutaDetalle = new DataTable();
            CMT_DET c = new CMT_DET();

            List<CustomColumnNames> lstCampos = new List<CustomColumnNames>();


            lstCampos.Add(new CustomColumnNames("CMT_INDX", "CMT_INDX"));
            lstCampos.Add(new CustomColumnNames("CMT_PEDIDO","CMT_PEDIDO"));
            lstCampos.Add(new CustomColumnNames("CMT_MODELO","CMT_MODELO"));
            lstCampos.Add(new CustomColumnNames("CMT_COMO","CMT_COMO"));
            lstCampos.Add(new CustomColumnNames("CMT_RUTA","CMT_RUTA"));
            lstCampos.Add(new CustomColumnNames("CMT_PROCESO","CMT_PROCESO"));
            lstCampos.Add(new CustomColumnNames("CMT_DEPARTAMENTO","CMT_DEPARTAMENTO"));
            lstCampos.Add(new CustomColumnNames("CMT_ORDENAMIENTO","CMT_ORDENAMIENTO"));

            dataTablePedidoRutaDetalle  = CustomTable.GetCustomDataTable(c.Consultar2(NumeroPedido,Modelo,Agrupador), lstCampos);

            return dataTablePedidoRutaDetalle;
        }
        public static string GuardaCampoValorCMT_DET(int NumeroPedido,string Campo, object Valor,Type TipoDato,int? INDX)
        {
            CMT_DET c = new CMT_DET();
            string valor = "";
            switch (TipoDato.ToString())
            {
                case "System.String":
                    valor = string.Format("'{0}'", Valor);
                    break;
                default:
                    valor = Convert.ToString(Valor);
                    break;
            }

            string r =c.GuardaCampoValor(NumeroPedido, Campo, valor,INDX);
            
            return r;
        }
        public static bool LiberarPedido(int NumeroPedido)
        {
            try
            {
                using (var dbContext = new AspelSae80Context())
                {
                    var query = from cmt in dbContext.CMT_DET
                        where cmt.CMT_PEDIDO == NumeroPedido && cmt.CMT_ESTATUS == "P"
                        select cmt;

                    foreach (var item in query)
                    {
                        ulp_dl.aspel_sae80.CMT_DET c = new ulp_dl.aspel_sae80.CMT_DET();
                        c = item;

                        c.CMT_ESTATUS = "R";

                    }
                    dbContext.SaveChanges();
                }
                return true;
            }
            catch (Exception Ex)
            {
                return false;
            }
        }
        public static bool DepartamentoValido(string Departamento)
        {
            bool deptoValido = false;
            using (var dbContext = new AspelSae80Context())
            {
                var query = from d in dbContext.U_DEPARTAMENTO where d.NOMBRE == Departamento select d;

                deptoValido = query.Any();                
            }
            return deptoValido;
        }
        
    }
}
