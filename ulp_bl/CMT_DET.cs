using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data;
using sm_dl;
using sm_dl.SqlServer;
using ulp_dl.aspel_sae80;
using ulp_dl;
using ulp_bl.Utiles;
using ulp_dl.SIPNegocio;

namespace ulp_bl
{
    public class CMT_DET : ICrud<CMT_DET>
    {
        private Exception error;
        private bool tieneError;

        public int CMT_PEDIDO { get; set; }
        public int CMT_INDX { get; set; }
        public string CMT_CMMT { get; set; }
        public string CMT_COMO { get; set; }
        public string CMT_DONDE { get; set; }
        public string CMT_AGRUPADOR { get; set; }
        public string CMT_PROCESO { get; set; }
        public decimal? CMT_PRE_PROCESO { get; set; }
        public string CMT_MAQUILERO { get; set; }
        public DateTime? CMT_FECHA { get; set; }
        public string CMT_FACT_MAQUILA { get; set; }
        public string CMT_MODELO { get; set; }
        public string CMT_RUTA { get; set; }
        public string CMT_CLIENTE { get; set; }
        public int? CMT_PUNTADAS { get; set; }
        public string CMT_ESTATUS { get; set; }
        public int? CMT_CANTIDAD { get; set; }
        public string CMT_DEPARTAMENTO { get; set; }
        public int? CMT_ORDENAMIENTO { get; set; }
        public DateTime? CMT_FVENC { get; set; }
        public string CMT_TIPO { get; set; }
        public decimal? CMT_COS_PROCESO { get; set; }
        public DateTime? CMT_FVENC_EMPAQUE { get; set; }

        public bool TieneError
        {
            get { return tieneError; }
        }

        public Exception Error
        {
            get { return error; }
        }

        public CMT_DET Consultar(int ID)
        {
            throw new NotImplementedException();
        }
        public DataTable ConsultarPedidos(int NumeroPedido)
        {
            string connStr = "";
            DataTable dtPedDet = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                connStr = dbContext.Database.Connection.ConnectionString;
            }

            SqlServerCommand cmd = new SqlServerCommand();
            cmd.Connection = DALUtil.GetConnection(connStr);
            cmd.ObjectName = "usp_RegresaRutasPorPedido";
            cmd.Parameters.Add(new SqlParameter("@pedido", NumeroPedido));
            dtPedDet = cmd.GetDataTable();
            cmd.Connection.Close();
            return dtPedDet;
        }
        public CMT_DET ConsultarDistinct(int ID)
        {
            CMT_DET cmtDet = new CMT_DET();
            using (var dbContext = new AspelSae80Context())
            {
                var query =
                    (from x in dbContext.CMT_DET
                     where x.CMT_PEDIDO == ID
                     select new { x.CMT_PEDIDO, x.CMT_MODELO, x.CMT_CANTIDAD, x.CMT_RUTA }).Distinct();

                if (query.Count() > 0)
                {
                    CopyClass.CopyObject(query.First(), ref cmtDet);
                }


            }
            return cmtDet;
        }

        public DataTable ConsultarProcesosPartida(CMT_DET tEntidad)
        {
            DataTable procesos = new DataTable();
            using (var dbContext = new AspelSae80Context())
            {
                var resultado = from proc in dbContext.CMT_DET
                                where proc.CMT_PEDIDO == tEntidad.CMT_PEDIDO && proc.CMT_AGRUPADOR.Trim() == tEntidad.CMT_AGRUPADOR.Trim()
                                orderby proc.CMT_INDX
                                select new
                                {
                                    proc.CMT_PROCESO,
                                    proc.CMT_CMMT,
                                    proc.CMT_COMO,
                                    proc.CMT_DONDE,
                                    proc.CMT_PRE_PROCESO,
                                    proc.CMT_AGRUPADOR,
                                    proc.CMT_INDX,
                                    proc.CMT_MAQUILERO,
                                    proc.CMT_MODELO,
                                    proc.CMT_CANTIDAD
                                };

                procesos = Linq2DataTable.CopyToDataTable(resultado);
            }
            return procesos;
        }

        public void Crear(CMT_DET tEntidad, ref int idx)
        {
            try
            {
                ulp_dl.aspel_sae80.CMT_DET cmt_detEliminar = new ulp_dl.aspel_sae80.CMT_DET();
                using (var dbContext = new AspelSae80Context())
                {
                    CopyClass.CopyObject(tEntidad, ref cmt_detEliminar);
                    dbContext.CMT_DET.Add(cmt_detEliminar);
                    dbContext.SaveChanges();
                    idx = cmt_detEliminar.CMT_INDX;
                }
            }
            catch (Exception ex)
            {
                error = ex;
                tieneError = true;
            }
        }
        public void Crear(CMT_DET tEntidad)
        {
            int idx = 0;
            Crear(tEntidad, ref idx);
        }
        public void ModificarPorPedido(CMT_DET tEntidad)
        {
            using (var dbContext = new AspelSae80Context())
            {
                var resultado = (from cmt in dbContext.CMT_DET where cmt.CMT_PEDIDO == tEntidad.CMT_PEDIDO select cmt);
                //copio el valor para que no falle al asignar la llave primaria
                if (resultado.Any())
                {
                    foreach (ulp_dl.aspel_sae80.CMT_DET cmtDet in resultado)
                    {
                        tEntidad.CMT_INDX = cmtDet.CMT_INDX;
                        cmtDet.CMT_FVENC = tEntidad.CMT_FVENC;
                        cmtDet.CMT_FVENC_EMPAQUE = tEntidad.CMT_FVENC_EMPAQUE;
                        //CopyClass.CopiaValoresNoNulosClase(tEntidad, ref cmtDet);
                    }

                    dbContext.SaveChanges();
                }
            }
        }
        public void Modificar(CMT_DET tEntidad)
        {
            using (var dbContext = new AspelSae80Context())
            {
                var resultado = dbContext.CMT_DET.Find(tEntidad.CMT_INDX);

                resultado.CMT_PROCESO = tEntidad.CMT_PROCESO;
                resultado.CMT_AGRUPADOR = tEntidad.CMT_AGRUPADOR;
                resultado.CMT_PEDIDO = tEntidad.CMT_PEDIDO;
                resultado.CMT_CMMT = tEntidad.CMT_CMMT;
                resultado.CMT_COMO = tEntidad.CMT_COMO;
                resultado.CMT_DONDE = tEntidad.CMT_DONDE;
                resultado.CMT_PRE_PROCESO = tEntidad.CMT_PRE_PROCESO;
                dbContext.SaveChanges();

            }
        }
        public void BorrarPorINDX(CMT_DET tEntidad)
        {
            try
            {
                using (AspelSae80Context dbContext = new AspelSae80Context())
                {
                    var resultado = dbContext.CMT_DET.Find(tEntidad.CMT_INDX);
                    /*
                var resultado = from procesos in dbContext.CMT_DET
                                where procesos.CMT_PEDIDO == tEntidad.CMT_PEDIDO && procesos.CMT_AGRUPADOR.Trim() == tEntidad.CMT_AGRUPADOR.Trim()
                                select procesos;
                    */

                    dbContext.CMT_DET.Remove(resultado);
                    dbContext.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                tieneError = true;
                error = ex;
            }


        }
        public void Borrar(CMT_DET tEntidad, Enumerados.TipoBorrado TipoBorrado)
        {
            try
            {
                if (TipoBorrado == Enumerados.TipoBorrado.Fisico)
                {
                    using (AspelSae80Context dbContext = new AspelSae80Context())
                    {

                        var resultado = from procesos in dbContext.CMT_DET
                                        where procesos.CMT_PEDIDO == tEntidad.CMT_PEDIDO && procesos.CMT_AGRUPADOR.Trim() == tEntidad.CMT_AGRUPADOR.Trim()
                                        select procesos;

                        //se utiliza un ciclo por que no se sabe si la consulta linq devolverá más de un resultado, podría ser una colección de resultados
                        /*
                        foreach (ulp_dl.aspel_sae50.CMT_DET item in resultado)
                        {
                            dbContext.CMT_DET.Remove(item);                                
                        }                                                    
                        */

                        dbContext.CMT_DET.RemoveRange(resultado);
                        dbContext.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                tieneError = true;
                error = ex;
            }


        }

        public string GuardaCampoValor(int NumeroPedido, string Campo, string Valor, int? INDX)
        {

            string conStr = "";
            using (var dbContext = new AspelSae80Context())
            {
                conStr = dbContext.Database.Connection.ConnectionString;
            }

            sm_dl.SqlEmbedded.Update update = new sm_dl.SqlEmbedded.Update();
            update.Connection = DALUtil.GetConnection(conStr);
            update.TableName = "CMT_DET";
            update.FieldValue.Add(new sm_dl.SqlEmbedded.FieldValue(Campo, Valor));
            if (INDX == null)
            {
                update.Where = string.Format("CMT_PEDIDO = '{0}'", NumeroPedido);
            }
            else
            {
                update.Where = string.Format("CMT_PEDIDO = '{0}' AND CMT_INDX={1}", NumeroPedido, INDX);
            }
            update.Execute();
            return update.SQLSentence;
        }

        public DataTable ConsultarTodos()
        {
            throw new NotImplementedException();
        }

        public DataTable Consultar2(int NumeroPedido, string Modelo, string Agrupador)
        {

            DataTable dataTableCMT_DET = new DataTable();


            using (var dbContext = new AspelSae80Context())
            {
                var query = from cmt in dbContext.CMT_DET where cmt.CMT_PEDIDO == NumeroPedido && cmt.CMT_AGRUPADOR.Trim() == Agrupador && cmt.CMT_MODELO == Modelo orderby cmt.CMT_ORDENAMIENTO select cmt;

                dataTableCMT_DET = Linq2DataTable.CopyToDataTable(query);
            }
            return dataTableCMT_DET;
        }

        public static void ActualizaCMT_CantidadTotal(int CMT_PEDIDO, string CMT_AGRUPADOR, string CMT_MODELO, int CMT_CANTIDAD)
        {
            using (var dbContext = new AspelSae80Context())
            {
                var query = from cmt_det in dbContext.CMT_DET
                            where
                                cmt_det.CMT_PEDIDO == CMT_PEDIDO && cmt_det.CMT_AGRUPADOR == CMT_AGRUPADOR &&
                                cmt_det.CMT_MODELO == CMT_MODELO
                            select cmt_det;

                if (query.Any())
                {
                    foreach (ulp_dl.aspel_sae80.CMT_DET cmtDet in query)
                    {
                        cmtDet.CMT_CANTIDAD = CMT_CANTIDAD;
                    }
                }

                dbContext.SaveChanges();
            }
        }

        public static CMT_DET BuscaProcesoEmbarque(int CMT_PEDIDO)
        {
            using (var dbContext = new AspelSae80Context())
            {
                var query = dbContext.CMT_DET.Where(cmt_det => cmt_det.CMT_PEDIDO == CMT_PEDIDO && cmt_det.CMT_PROCESO == "Q").FirstOrDefault();
                if (query != null)
                {
                    CMT_DET res = new CMT_DET
                    {
                        CMT_CANTIDAD = query.CMT_CANTIDAD,
                        CMT_DONDE = query.CMT_DONDE,
                        CMT_PEDIDO = query.CMT_PEDIDO,
                        CMT_PRE_PROCESO = query.CMT_PRE_PROCESO,
                        CMT_COS_PROCESO = query.CMT_COS_PROCESO
                    };
                    return res;
                }

            }
            return null;
        }
    }
}


