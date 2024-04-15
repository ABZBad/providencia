using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Data.SqlClient;
using System.Data;
using sm_dl;
using ulp_bl.Utiles;
using ulp_dl;
using sm_dl.SqlServer;
using ulp_dl.SIPNegocio;
using ulp_dl.aspel_sae80;

namespace ulp_bl
{
    public class ProveedoresFletes
    {
        public static DataTable RegresaDocumentosPorPedido(int NumeroPedido)
        {
            string      conStr = "";
            DataTable dataTableDocumentos = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                conStr = dbContext.Database.Connection.ConnectionString;
            }

            SqlServerCommand cmd = new SqlServerCommand();
            cmd.Connection =  DALUtil.GetConnection(conStr);
            cmd.ObjectName = "usp_DocumentosPorPedidoFlete";
            cmd.Parameters.Add(new SqlParameter("@numero_pedido",NumeroPedido));
            dataTableDocumentos = cmd.GetDataTable();
            return dataTableDocumentos;
        }
        public static DataTable RegresaDocumentosPorPedidoPrevios(int NumeroPedido)
        {
            DataTable dataTablePedidosPrevios = new DataTable();
            #region definición de la tabla resultante
            DataColumn colPedido = new DataColumn("PEDIDO", typeof(int));
            DataColumn colPrendas = new DataColumn("PRENDAS_FLETE", typeof(int));
            DataColumn colFacturas = new DataColumn("FACTURAS", typeof(string));
            DataColumn colCostoPrevio = new DataColumn("COSTO_PREVIO", typeof(decimal));

            dataTablePedidosPrevios.Columns.Add(colPedido);
            dataTablePedidosPrevios.Columns.Add(colPrendas);
            dataTablePedidosPrevios.Columns.Add(colFacturas);
            dataTablePedidosPrevios.Columns.Add(colCostoPrevio);

            dataTablePedidosPrevios.PrimaryKey = new DataColumn[] { colPedido };

            #endregion           


            FLET_ENLA resultFLET_ENLA = new FLET_ENLA();
            using (var dbContext = new AspelSae80Context())
            {
                //FLET_ENLA query = (from f in dbContext.FLET_ENLA where f.PEDIDO == NumeroPedido orderby f.NUM_REG descending select new { f.CVE_CLPV, f.CVE_DOC }).Take(1);


                var query = dbContext.FLET_ENLA.Where(f => (f.PEDIDO == NumeroPedido))
                            .OrderByDescending(f => f.NUM_REG)
                            .Select(
                                f =>
                                    new
                                    {
                                        CVE_CLPV = f.CVE_CLPV,
                                        CVE_DOC = f.CVE_DOC
                                    }
                            )
                            .Take(1).ToList();

                if (query.Count == 1)
                {
                    CopyClass.CopyObject(query[0], ref resultFLET_ENLA);


                    var queryPedidos = from pp in dbContext.FLET_ENLA where pp.CVE_CLPV == resultFLET_ENLA.CVE_CLPV && pp.CVE_DOC == resultFLET_ENLA.CVE_DOC && pp.PEDIDO != NumeroPedido orderby pp.NUM_REG select new { pp.PEDIDO };
                    
                    foreach (var numeroPedidoPrevio in queryPedidos)
                    {
                        dataTablePedidosPrevios.Merge(RegresaDocumentosPorPedido(numeroPedidoPrevio.PEDIDO),true,MissingSchemaAction.Ignore);
                    }
                }

                //if (resultFLET_ENLA.CVE_DOC != null)


            }
            return dataTablePedidosPrevios;
        }
        public static bool GuardaInformacionFlete(string CVE_CLPV,string CVE_DOC,decimal SubTotalFactura,decimal CostoPrenda,DataTable DocumentosPorPedido)
        {
            try
            {
                var dbContextDetails = new AspelSae80Context();

                using (var dbContext = new AspelSae80Context())
                {
                    ulp_dl.aspel_sae80.FLET_MSTR fletMstr = new ulp_dl.aspel_sae80.FLET_MSTR();

                    fletMstr.CAN_TOT = Convert.ToDouble(SubTotalFactura);
                    fletMstr.CVE_DOC = CVE_DOC;
                    fletMstr.CVE_CLPV = CVE_CLPV;

                    dbContext.FLET_MSTR.Add(fletMstr);
                   // dbContext.SaveChanges();
                    foreach (DataRow renglonDocumentosPorPedido in DocumentosPorPedido.Rows)
                    {

                        int renglonPedido = (int) renglonDocumentosPorPedido["PEDIDO"];

                        FLET_ENLA fletEnla = new FLET_ENLA();

                        fletEnla.CVE_CLPV = CVE_CLPV;
                        fletEnla.CVE_DOC = CVE_DOC;
                        fletEnla.PEDIDO = renglonPedido;
                        dbContext.FLET_ENLA.Add(fletEnla);

                        //dbContext.SaveChanges();

                        /*
                        ulp_dl.aspel_sae50.CMT_DET cmdDet =
                            dbContext.CMT_DET.First(c => c.CMT_PEDIDO == renglonPedido && c.CMT_PROCESO == "F");
                        */
                        var cmdDetQuery = dbContextDetails.CMT_DET.Where(c => c.CMT_PEDIDO == renglonPedido && c.CMT_PROCESO == "F");



                        //cmdDet.CMT_COS_PROCESO = CostoPrenda;
                        foreach (ulp_dl.aspel_sae80.CMT_DET cmtDet in cmdDetQuery)
                        {
                            cmtDet.CMT_COS_PROCESO = CostoPrenda;
                            
                        }

                        

                    }
                    dbContextDetails.SaveChanges();
                    dbContext.SaveChanges();
                    // return true;


                }
                return true;
            }
            catch (Exception Ex)
            {
                return false;
            }
        }
    }
}
