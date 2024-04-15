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
    public class OrdMaquila2
    {
        /// <summary>
        /// Regresa el detalle de la Orden
        /// </summary>
        /// <param name="Referencia">Número de Referencia u Orden de Producción</param>
        /// <returns></returns>
        public static DataTable RegresaOrden(string Referencia)
        {
            DataTable dataTableOrden = new DataTable();
            using (var dbContext = new AspelSae80Context())
            {
                var query = from fab in dbContext.PROD_ORDENES01 where fab.REFERENCIA == Referencia 
                            orderby fab.CVE_ORD
                            select new
                            {
                                Modelo = "X" + fab.CVE_ART,
                                Cantidad = fab.CANTIDAD,
                                OrdProd = fab.CVE_ORD,
                                Referencia = fab.REFERENCIA
                            };

                dataTableOrden = Linq2DataTable.CopyToDataTable(query);
            }
            return dataTableOrden;
        }

        public static int SiguienteOrdMaquila()
        {
            int ultDoc = 0;
            using (var dbContext = new AspelSae80Context())
            {
                FOLIOSC01 folio =
                               (from folios in dbContext.FOLIOSC01
                                where folios.TIP_DOC == "o" && folios.SERIE == "STAND."
                                select folios).SingleOrDefault();
                ultDoc = Convert.ToInt32(folio.ULT_DOC) + 1;                                
            }
            return ultDoc;
        }
        public static int GenerarOrden(DataTable dataTableOrden, int CantidadDeRenglonesDetalle, int SumaDeCantidadDeDetalle,string ClaveProveedor,int Costo, int ClaveEsquemaImpuestos,string Observaciones, int Almacen, ref Exception Error)
        {
            int ultDoc = 0,utlCve_tblControl01 = 0;

            using (var dbContext = new AspelSae80Context())
            {
                using (var tran = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        #region paso 1

                        FOLIOSC01 folio =
                            (from folios in dbContext.FOLIOSC01
                                where folios.TIP_DOC == "o" && folios.SERIE == "STAND."
                                select folios).SingleOrDefault();
                        ultDoc = Convert.ToInt32(folio.ULT_DOC) + 1;
                        folio.ULT_DOC = ultDoc;
                        folio.FECH_ULT_DOC = DateTime.Now;
                        dbContext.SaveChanges();

                        #endregion

                        #region paso 2 Insercion de las observaciones de la orden de compra y obtencion del num_reg

                        TBLCONTROL01 tblControl01 = (
                            from tblCtrl01 in dbContext.TBLCONTROL01
                            where tblCtrl01.ID_TABLA == 57
                            select tblCtrl01
                            ).SingleOrDefault();
                        utlCve_tblControl01 = Convert.ToInt32(tblControl01.ULT_CVE) + 1;
                        tblControl01.ULT_CVE = utlCve_tblControl01;
                        dbContext.SaveChanges();

                        OBS_DOCC01 obsDoc01 = new OBS_DOCC01();

                        obsDoc01.CVE_OBS = utlCve_tblControl01;
                        obsDoc01.STR_OBS = Observaciones;

                        dbContext.OBS_DOCC01.Add(obsDoc01);
                        dbContext.SaveChanges();

                        #endregion

                        #region Paso 3 Realizar la insercion del encabezado de compra

                        COMPO01 compo01 = new COMPO01();
                        compo01.TIP_DOC = "o";
                        compo01.CVE_DOC = ultDoc.ToString().PadLeft(20);
                        compo01.CVE_CLPV = ClaveProveedor;
                        compo01.STATUS = "E";
                        compo01.SU_REFER = "";
                        compo01.FECHA_DOC = DateTime.Now.Date;
                        compo01.FECHA_REC = DateTime.Now.Date;
                        compo01.FECHA_PAG = DateTime.Now.Date;
                        compo01.CAN_TOT = SumaDeCantidadDeDetalle * Costo; //verificar
                        compo01.IMP_TOT1 = 0;
                        compo01.IMP_TOT2 = 0;
                        compo01.IMP_TOT3 = 0;
                        compo01.DES_TOT = 0;

                        IMPU01 queryImpuestos = (from impuestos in dbContext.IMPU01
                            where impuestos.CVE_ESQIMPU == ClaveEsquemaImpuestos
                            select impuestos).SingleOrDefault();

                        compo01.IMP_TOT4 = SumaDeCantidadDeDetalle*Costo*queryImpuestos.IMPUESTO4/100;
                        compo01.DES_FIN = 0;
                        compo01.TOT_IND = 0;
                        compo01.OBS_COND = "";
                        compo01.CVE_OBS = utlCve_tblControl01; //verificar
                        //compo01.NUM_ALMA = 1;
                        compo01.NUM_ALMA = Almacen;
                        compo01.ACT_CXP = "";
                        compo01.ACT_COI = "";
                        compo01.NUM_MONED = 1;
                        compo01.TIPCAMB = 1;
                        compo01.ENLAZADO = "O";
                        compo01.TIP_DOC_E = "O";
                        compo01.NUM_PAGOS = 0;
                        compo01.FECHAELAB = DateTime.Now;
                        compo01.CTLPOL = 0;
                        compo01.ESCFD = "N";
                        compo01.BLOQ = "N";
                        compo01.DES_FIN_PORC = 0;
                        compo01.DES_TOT_PORC = 0;
                        compo01.IMPORTE =
                            Math.Round(Convert.ToDouble(compo01.CAN_TOT) + Convert.ToDouble(compo01.IMP_TOT4), 2);


                        dbContext.COMPO01.Add(compo01);
                        dbContext.SaveChanges();


                        #endregion

                        #region Paso 4  Realizar la inserción de las partidas de la orden de compra en COM0Y1

                        List<PAR_COMPO01> listaDeParCompo01 = new List<PAR_COMPO01>();
                        int numPar = 0;
                        foreach (DataRow rowTableOrden in dataTableOrden.Rows)
                        {
                            numPar++;
                            PAR_COMPO01 parCompo01 = new PAR_COMPO01();
                            parCompo01.CVE_DOC = ultDoc.ToString().PadLeft(20);
                            parCompo01.NUM_PAR = numPar;
                            parCompo01.CVE_ART = rowTableOrden["Modelo"].ToString();
                            parCompo01.CANT = Convert.ToDouble(rowTableOrden["Cantidad"].ToString());
                            parCompo01.PXR = Convert.ToDouble(rowTableOrden["Cantidad"].ToString());
                            parCompo01.PREC = 0;
                            parCompo01.COST = Costo;
                            parCompo01.IMPU1 = 0;
                            parCompo01.IMPU2 = 0;
                            parCompo01.IMPU3 = 0;
                            parCompo01.IMPU4 = queryImpuestos.IMPUESTO4;
                            parCompo01.IMP1APLA =
                                (short) (queryImpuestos.IMP1APLICA.HasValue ? queryImpuestos.IMP1APLICA : 0);
                            parCompo01.IMP2APLA =
                                (short) (queryImpuestos.IMP2APLICA.HasValue ? queryImpuestos.IMP2APLICA : 0);
                            parCompo01.IMP3APLA =
                                (short) (queryImpuestos.IMP3APLICA.HasValue ? queryImpuestos.IMP3APLICA : 0);
                            parCompo01.IMP4APLA =
                                (short) (queryImpuestos.IMP4APLICA.HasValue ? queryImpuestos.IMP4APLICA : 0);
                            parCompo01.TOTIMP1 = 0;
                            parCompo01.TOTIMP2 = 0;
                            parCompo01.TOTIMP3 = 0;
                            parCompo01.TOTIMP4 = Convert.ToDouble(rowTableOrden["Cantidad"].ToString())*Costo*
                                                 queryImpuestos.IMPUESTO4/100;
                            parCompo01.DESCU = 0;
                            parCompo01.ACT_INV = "";
                            parCompo01.TIP_CAM = 1;
                            parCompo01.UNI_VENTA = "pz";
                            parCompo01.TIPO_ELEM = "N";
                            parCompo01.TIPO_PROD = "P";
                            parCompo01.CVE_OBS = 0;
                            parCompo01.E_LTPD = 0;
                            parCompo01.REG_SERIE = 0;
                            parCompo01.FACTCONV = 1;
                            parCompo01.NUM_ALM = 1;
                            parCompo01.NUM_MOV = 0;
                            parCompo01.TOT_PARTIDA = Convert.ToDouble(rowTableOrden["Cantidad"].ToString())*Costo;

                            listaDeParCompo01.Add(parCompo01);
                        }
                        dbContext.PAR_COMPO01.AddRange(listaDeParCompo01);
                        dbContext.SaveChanges();

                        #endregion

                        tran.Commit();
                    }
                    catch (Exception Ex)
                    {
                        Error = Ex;
                        tran.Rollback();
                        return 0;
                    }
                }
            return ultDoc;
            }
        }
    }
    
}
