using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIP;
using sm_dl;
using sm_dl.SqlServer;
//using ulp_dl.aspel_prod30;
using ulp_dl.aspel_sae80;
using ulp_dl.SIPNegocio;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using ulp_bl.Utiles;


namespace ulp_bl
{
    public class OrdProduccion
    {
        public static bool ReferenciaUtilizada(string NumeroReferencia)
        {
            using (var dbContext = new AspelSae80Context())
            {
                var query = from r in dbContext.PROD_ORDENES01 where r.REFERENCIA.Trim() == NumeroReferencia select r;

                if (query.Any())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public static DataTable RegresaTablaTallas(string Modelo, string Almacen)
        {
            DataTable dataTableTallas = new DataTable();
            string connStr = "";
            using (var dbContext = new SIPNegocioContext())
            {
                connStr = dbContext.Database.Connection.ConnectionString;
            }
            SqlServerCommand cmd = new SqlServerCommand();
            cmd.Connection = DALUtil.GetConnection(connStr);
            cmd.ObjectName = "usp_TallasPorModeloYAlmacen";
            cmd.Parameters.Add(new SqlParameter("@modelo", Modelo));
            cmd.Parameters.Add(new SqlParameter("@almacen", Almacen));
            dataTableTallas = cmd.GetDataTable();
            cmd.Connection.Close();
            return dataTableTallas;
        }
        public static bool GeneraOrdenProduccionDeLinea(DataTable TablaTallas, string Referencia, DateTime FechaEntrega, string Observaciones, string Pedido, int NumeroAlmacen)
        {

            using (var dbContext = new AspelSae80Context())
            {
                dbContext.Configuration.ValidateOnSaveEnabled = false;
                using (var tran = dbContext.Database.BeginTransaction())
                {
                    dbContext.Configuration.ValidateOnSaveEnabled = true;
                    DateTime dtFechaServer = DateTime.Now; //FechaHoraActualSQLServer(dbContext);
                    int NuevoNumReg_Ord_F0b01 = 1, NuevoUltClv_Ord_F0b01 = 1, NuevoNumReg_Obs_O0d01 = 1;

                    #region Se graban observaciones, se aumenta ID en una unidad
                    /* PROD40 - NO APLICA CAMPO DE OBSERVACIONES
                    OBS_O0D01 obs_o0d01 = (from obs in dbContext.OBS_O0D01 select obs).SingleOrDefault();
                    if (obs_o0d01.NUM_REGS.HasValue)
                    {
                        NuevoNumReg_Obs_O0d01 = Convert.ToInt32(obs_o0d01.NUM_REGS + 1);
                    }
                     * */
                    //obs_o0d01.NUM_REGS = NuevoNumReg_Obs_O0d01;
                    //dbContext.SaveChanges();
                    /* PROD40 - NO APLICA CAMPO DE OBSERVACIONES
                    PROD_OBSORD01 obs_ord01 = new PROD_OBSORD01();
                    //PROD_OBSORD01 obs_ord01 = new PROD_OBSORD01();
                    obs_ord01.CVE_OBS = NuevoNumReg_Obs_O0d01;
                    obs_ord01.STR_OBS = Pedido.PadLeft(10) + Observaciones;
                    dbContext.PROD_OBSORD01.Add(obs_ord01);
                     * */
                    //dbContext.SaveChanges();

                    #endregion

                    NuevoUltClv_Ord_F0b01 = Convert.ToInt32(dbContext.PROD_ORDENES01.OrderByDescending(x => x.CVE_ORD).FirstOrDefault().CVE_ORD) + 1;

                    foreach (DataRow dataRowTallas in TablaTallas.Rows)
                    {
                        #region 1.1.1 Seactualiza la tabla de control (se aumenta en una unadidad por cada talla)
                        /* PROD40 - SE OMITEN OBSERVACIONES
                        //1.1.1 se aumenta ORD_F0B01 en un una unidad
                        ORD_F0B01 ord_F0b01 = new ORD_F0B01();
                        ord_F0b01 = (from ord in dbContext.ORD_F0B01 select ord).SingleOrDefault();

                        if (ord_F0b01.NUM_REGS.HasValue)
                        {
                            NuevoNumReg_Ord_F0b01 = Convert.ToInt32(ord_F0b01.NUM_REGS + 1);
                        }
                        if (ord_F0b01.ULT_CLV.HasValue)
                        {
                            NuevoUltClv_Ord_F0b01 = Convert.ToInt32(ord_F0b01.ULT_CLV + 1);
                        }

                        ord_F0b01.NUM_REGS = NuevoNumReg_Ord_F0b01;
                        ord_F0b01.ULT_CLV = NuevoUltClv_Ord_F0b01;
                        //dbContext.SaveChanges();
                        */
                        NuevoNumReg_Ord_F0b01 = Convert.ToInt32(dbContext.PROD_OBSORD01.OrderByDescending(x => x.CVE_OBS).FirstOrDefault().CVE_OBS) + 1;
                        PROD_OBSORD01 obs_o0d01 = new PROD_OBSORD01();
                        obs_o0d01.CVE_OBS = NuevoNumReg_Ord_F0b01;
                        obs_o0d01.STR_OBS = Pedido.PadLeft(10) + Observaciones;
                        dbContext.PROD_OBSORD01.Add(obs_o0d01);
                        dbContext.SaveChanges();

                        // Insertamos las observaciones
                        #endregion

                        #region 1.1.2 Se carga la orden de producción en modo lineal

                        PROD_ORDENES01 ordenFab01 = new PROD_ORDENES01();
                        //ordenFab01.NUM_REG = NuevoNumReg_Ord_F0b01;
                        ordenFab01.CVE_ORD = NuevoUltClv_Ord_F0b01.ToString().PadLeft(16);
                        ordenFab01.CVE_ART = dataRowTallas["MODELO"].ToString() + dataRowTallas["TALLA"].ToString();
                        ordenFab01.CANTIDAD = Convert.ToDouble(dataRowTallas["CANTIDAD"]);////falla aquí
                        ordenFab01.PRIORIDAD = 2;
                        ordenFab01.FCAPTURA = dtFechaServer;
                        ordenFab01.FENTREGA = FechaEntrega;
                        ordenFab01.FINICIAL = dtFechaServer;
                        ordenFab01.FTERMINA = null;
                        ordenFab01.CANTTERM = 0;
                        ordenFab01.TGASDIR = 0;
                        ordenFab01.TGASIND = 0;
                        ordenFab01.FULTMOV = dtFechaServer;
                        ordenFab01.REFERENCIA = Referencia;
                        ordenFab01.COSTEST = 0;
                        ordenFab01.TIPOCOSTEO = 3;
                        ordenFab01.TIPOORD = 0;
                        ordenFab01.STATUS = "0";
                        ordenFab01.STATUSAVANCE = "0";
                        ordenFab01.PROCESO = "";
                        ordenFab01.NUMUSU = 0;
                        ordenFab01.REGSERIE = 0;
                        ordenFab01.OCUPADO = 0;
                        ordenFab01.ACT_SAE = 0;
                        ordenFab01.HRSXDIA = 8;
                        //ordenFab01.RESTO = "";
                        ordenFab01.CVE_OBS = NuevoNumReg_Ord_F0b01;
                        ordenFab01.STATUSBAJA = "A";
                        ordenFab01.CVE_PLANTA = 1;
                        dbContext.PROD_ORDENES01.Add(ordenFab01);
                        NuevoUltClv_Ord_F0b01++;

                        //dbContext.SaveChanges();

                        #endregion

                        string cveArt = string.Format("{0}{1}", dataRowTallas["MODELO"], dataRowTallas["TALLA"]);
                        double cantidad = Convert.ToDouble(dataRowTallas["CANTIDAD"]);
                        int cveAlm = NumeroAlmacen;

                        #region 1.2 Se incrementan pendientes por recibir en PT

                        using (var dbContextAspelSae50 = new AspelSae80Context())
                        {
                            #region 1.2.1 Se actualizan los pendiente por recibir en producto terminado eb MUTL01

                            var queryMulti = from multi in dbContextAspelSae50.MULT01
                                             where multi.CVE_ART == cveArt && multi.CVE_ALM == cveAlm
                                             select multi;
                            foreach (var mult01 in queryMulti)
                            {
                                mult01.COMP_X_REC = mult01.COMP_X_REC + Convert.ToDouble(dataRowTallas["CANTIDAD"]);
                            }
                            //dbContextAspelSae50.SaveChanges();

                            #endregion

                            #region 1.2.2 Se actualizan los pendientes por recibir en el producto terminado INVE01

                            var queryInve = from inve in dbContextAspelSae50.INVE01
                                            where inve.CVE_ART == cveArt
                                            select inve;
                            foreach (var inve01 in queryInve)
                            {
                                inve01.COMP_X_REC = inve01.COMP_X_REC + Convert.ToDouble(dataRowTallas["CANTIDAD"]);
                            }
                            //dbContextAspelSae50.SaveChanges();

                            #endregion

                            #region 1.3 Se incrementan pendientes por sustituir de los componentes de INVE01; en MULT01 no existe este campo

                            var queryPT = from pt in dbContext.PROD_PRODTERM_DET01
                                          where pt.CVE_ART == cveArt && pt.TIPOCOMP == 1
                                          select pt;
                            foreach (PROD_PRODTERM_DET01 ptDet01 in queryPT)
                            {
                                var queryInve2 = from inve2 in dbContextAspelSae50.INVE01
                                                 where inve2.CVE_ART == ptDet01.COMPONENTE
                                                 select inve2;
                                foreach (INVE01 inve01 in queryInve2)
                                {
                                    inve01.PEND_SURT =
                                        Math.Round(
                                            Convert.ToDouble(inve01.PEND_SURT) +
                                            Convert.ToDouble(ptDet01.CANTIDAD) * cantidad, 3);
                                }
                                //dbContextAspelSae50.SaveChanges();
                            }
                            #endregion

                            dbContextAspelSae50.SaveChanges();
                        }

                        #endregion

                    }
                    try
                    {
                        dbContext.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        var xxx = (IObjectContextAdapter)dbContext;
                        xxx.ObjectContext.Refresh(RefreshMode.ClientWins, dbContext);
                        dbContext.SaveChanges();
                    }
                    tran.Commit();
                }
                return true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Referencia">Referencia (Orden)</param>
        /// <returns></returns>
        public static string LiberarOrden(string Referencia)
        {
            string resultado = RepOrdProd.EnviaAProduccion(Referencia);
            return resultado;
        }
        private static DateTime FechaHoraActualSQLServer(AspelSae80Context dbContext)
        {
            var query = ((IObjectContextAdapter)dbContext).ObjectContext.CreateQuery<DateTime>("CurrentDateTime()");

            return query.AsEnumerable().First();
        }
        public static string GetLineaArticulo(string Modelo)
        {
            DataTable dataTableTallas = new DataTable();
            string connStr = "";
            using (var dbContext = new SIPNegocioContext())
            {
                connStr = dbContext.Database.Connection.ConnectionString;
            }
            SqlServerCommand cmd = new SqlServerCommand();
            cmd.Connection = DALUtil.GetConnection(connStr);
            cmd.ObjectName = "usp_getLineaArticulo";
            cmd.Parameters.Add(new SqlParameter("@CVE_ART", Modelo));
            dataTableTallas = cmd.GetDataTable();
            cmd.Connection.Close();
            if (dataTableTallas.Rows.Count > 0)
            {
                return dataTableTallas.Rows[0]["LIN_PROD"].ToString();
            }
            else
            {
                return "";
            }
        }
        public static DataTable RegresaReporteOPyOM(DateTime fechaInicio, DateTime fechaFin)
        {
            DataTable dataTableTallas = new DataTable();
            string connStr = "";
            using (var dbContext = new SIPNegocioContext())
            {
                connStr = dbContext.Database.Connection.ConnectionString;
            }
            SqlServerCommand cmd = new SqlServerCommand();
            cmd.Connection = DALUtil.GetConnection(connStr);
            cmd.ObjectName = "usp_RepOPOM";
            cmd.Parameters.Add(new SqlParameter("@FechaInicio", fechaInicio));
            cmd.Parameters.Add(new SqlParameter("@FechaFin", fechaFin));
            dataTableTallas = cmd.GetDataTable();
            cmd.Connection.Close();
            return dataTableTallas;
        }
        public static void GeneraArchivoExcelOPyOM(string RutaYNombreArchivo, DataTable dtReporte, DateTime FechaDesde, DateTime FechaHasta)
        {

            HSSFWorkbook xlsWorkBook = new HSSFWorkbook();
            ISheet sheet = xlsWorkBook.CreateSheet("Hoja1");

            //formatos
            #region Formatos


            //formato de miles SIN decimales
            ICellStyle fmtoMiles = xlsWorkBook.CreateCellStyle();
            fmtoMiles.DataFormat = ExcelNpoiUtil.FormatoCelda(ref xlsWorkBook, "#,##0");

            //formato para Texto Centrado
            ICellStyle fmtCentrado = xlsWorkBook.CreateCellStyle();
            fmtCentrado.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;

            //formato para texto en Negritas
            ICellStyle fmtNegritas = xlsWorkBook.CreateCellStyle();

            #endregion

            #region Encabezado

            IRow rngEncabezado = sheet.CreateRow(0);
            ICell celEncabezado = rngEncabezado.CreateCell(0);
            celEncabezado.SetCellValue("Reporte de OP y OM");
            celEncabezado.CellStyle = fmtCentrado;

            //se combinan las celdas

            CellRangeAddress range = new CellRangeAddress(0, 0, 0, 3);
            sheet.AddMergedRegion(range);


            #endregion

            #region Se escriben los rangos de fecha

            IRow rngEmitidoDel = sheet.CreateRow(1); //renglón: "Emitido del.."
            rngEmitidoDel.CreateCell(0).SetCellValue(string.Format("Emitido del      {0}", FechaDesde.ToShortDateString()));

            IRow rngEmitidoAl = sheet.CreateRow(2); //renglón: "Al.."
            rngEmitidoAl.CreateCell(0).SetCellValue(string.Format("Al                   {0}", FechaHasta.ToShortDateString()));

            #endregion

            #region Encabezados

            IRow rngEncabezados = sheet.CreateRow(4);
            int i = 0;
            rngEncabezados.CreateCell(i).SetCellValue("Fecha"); i++;
            rngEncabezados.CreateCell(i).SetCellValue("Taller"); i++;
            rngEncabezados.CreateCell(i).SetCellValue("Línea"); i++;
            rngEncabezados.CreateCell(i).SetCellValue("Cantidad"); i++;
            rngEncabezados.CreateCell(i).SetCellValue("Descripción"); i++;
            rngEncabezados.CreateCell(i).SetCellValue("Orden de Maquila"); i++;
            rngEncabezados.CreateCell(i).SetCellValue("Orden de Producción"); i++;
            rngEncabezados.CreateCell(i).SetCellValue("F. Entrega Estimada"); i++;
            rngEncabezados.CreateCell(i).SetCellValue("F. Entrega Taller"); i++;
            rngEncabezados.CreateCell(i).SetCellValue("Total Recepciones"); i++;
            rngEncabezados.CreateCell(i).SetCellValue("Fecha Escaneo"); i++;
            rngEncabezados.CreateCell(i).SetCellValue("Diferencia"); i++;

            #endregion

            #region Detalle

            int iRenglonDetalle = 5;

            foreach (DataRow _dr in dtReporte.Rows)
            {

                IRow renglonDetalle = sheet.CreateRow(iRenglonDetalle);
                i = 0;
                renglonDetalle.CreateCell(i).SetCellValue(DateTime.Parse(_dr["Fecha"].ToString()).ToString("dd/MM/yyyy")); i++;
                renglonDetalle.CreateCell(i).SetCellValue(_dr["Taller"].ToString()); i++;
                renglonDetalle.CreateCell(i).SetCellValue(_dr["Linea"].ToString()); i++;
                renglonDetalle.CreateCell(i).SetCellValue(int.Parse(_dr["Cantidad"].ToString())); i++;
                renglonDetalle.CreateCell(i).SetCellValue(_dr["Descripcion"].ToString()); i++;
                renglonDetalle.CreateCell(i).SetCellValue(int.Parse(_dr["OrdenMaquila"].ToString())); i++;
                renglonDetalle.CreateCell(i).SetCellValue(int.Parse(_dr["OrdenProduccion"].ToString())); i++;
                renglonDetalle.CreateCell(i).SetCellValue(DateTime.Parse(_dr["FechaEntregaEstimada"].ToString()).ToString("dd/MM/yyyy")); i++;
                renglonDetalle.CreateCell(i).SetCellValue(_dr["FechaEntregaTaller"].ToString() == "" ? "" : DateTime.Parse(_dr["FechaEntregaTaller"].ToString()).ToString("dd/MM/yyyy")); i++;
                renglonDetalle.CreateCell(i).SetCellValue(int.Parse(_dr["TotalRecepciones"].ToString())); i++;
                renglonDetalle.CreateCell(i).SetCellValue(_dr["FechaEscaneo"].ToString() == "" ? "" : DateTime.Parse(_dr["FechaEscaneo"].ToString()).ToString("dd/MM/yyyy")); i++;
                renglonDetalle.CreateCell(i).SetCellValue(int.Parse(_dr["Diferencia"].ToString())); i++;
                iRenglonDetalle++;
            }


            for (i = 0; i <= 11; i++)
            {
                sheet.AutoSizeColumn(i);
            }

            #endregion


            #region SE ESCRIBE EL ARCHIVO
            if (File.Exists(RutaYNombreArchivo))
            {
                File.Delete(RutaYNombreArchivo);
            }
            FileStream fs = new FileStream(RutaYNombreArchivo, FileMode.CreateNew);

            xlsWorkBook.Write(fs);


            fs.Close();
            #endregion
        }
    }
}
