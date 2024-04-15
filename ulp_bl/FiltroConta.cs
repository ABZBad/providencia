using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using sm_dl;
using sm_dl.SqlServer;
using ulp_dl.SIPNegocio;
using ulp_bl.Utiles;

namespace ulp_bl
{
    public class FiltroConta
    {
        public static DataSet RegresaReconsYConteoMermas(int CVE_CPTO, DateTime FechaDesde, DateTime FechaHasta)
        {
            System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();


            DataSet dataTableRCM = new DataSet();
            string conStr = "";
            using (var dbContext = new SIPNegocioContext())
            {
                conStr = dbContext.Database.Connection.ConnectionString;
            }
            SqlServerCommand cmd = new SqlServerCommand();
            cmd.Connection = DALUtil.GetConnection(conStr);
            cmd.ObjectName = "usp_RepReconsYConteoMermas";
            cmd.Parameters.Add(new SqlParameter("@CVE_CPTO", CVE_CPTO));
            cmd.Parameters.Add(new SqlParameter("@fecha_desde", FechaDesde));
            cmd.Parameters.Add(new SqlParameter("@fecha_hasta", FechaHasta));
            dataTableRCM = cmd.GetDataSet();
            cmd.Connection.Close();


            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.Elapsed);

            return dataTableRCM;
        }
        public static DataSet RegresaReconsYConteoMermas_CMO(int CVE_CPTO, DateTime FechaDesde, DateTime FechaHasta)
        {
            System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();


            DataSet dataTableRCM_CMO = new DataSet();
            string conStr = "";
            using (var dbContext = new SIPNegocioContext())
            {
                conStr = dbContext.Database.Connection.ConnectionString;
            }
            SqlServerCommand cmd = new SqlServerCommand();
            cmd.Connection = DALUtil.GetConnection(conStr);
            cmd.ObjectName = "usp_RepReconsYConteoMermas_CMO";
            cmd.Parameters.Add(new SqlParameter("@CVE_CPTO", CVE_CPTO));
            cmd.Parameters.Add(new SqlParameter("@fecha_desde", FechaDesde));
            cmd.Parameters.Add(new SqlParameter("@fecha_hasta", FechaHasta));
            dataTableRCM_CMO = cmd.GetDataSet();
            cmd.Connection.Close();


            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.Elapsed);

            return dataTableRCM_CMO;
        }
        public static DataTable RegresaCostoVendidoEntreFechas(DateTime FechaDesde, DateTime FechaHasta)
        {
            System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();


            DataTable dataTableCVEF = new DataTable();
            string conStr = "";
            using (var dbContext = new SIPNegocioContext())
            {
                conStr = dbContext.Database.Connection.ConnectionString;
            }
            SqlServerCommand cmd = new SqlServerCommand();
            cmd.Connection = DALUtil.GetConnection(conStr);
            cmd.ObjectName = "usp_RepCostoVendidoEntreFechas";
            cmd.Parameters.Add(new SqlParameter("@fecha_desde", FechaDesde));
            cmd.Parameters.Add(new SqlParameter("@fecha_hasta", FechaHasta));
            dataTableCVEF = cmd.GetDataTable();
            cmd.Connection.Close();


            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.Elapsed);

            return dataTableCVEF;

        }
        public static DataTable RegresaCostoVendidoEntreFechas_CMO(DateTime FechaDesde, DateTime FechaHasta)
        {
            System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();


            DataTable dataTableCVEF_CMO = new DataTable();
            string conStr = "";
            using (var dbContext = new SIPNegocioContext())
            {
                conStr = dbContext.Database.Connection.ConnectionString;
            }
            SqlServerCommand cmd = new SqlServerCommand();
            cmd.Connection = DALUtil.GetConnection(conStr);
            cmd.ObjectName = "usp_RepCostoVendidoEntreFechas_CMO";
            cmd.Parameters.Add(new SqlParameter("@fecha_desde", FechaDesde));
            cmd.Parameters.Add(new SqlParameter("@fecha_hasta", FechaHasta));
            dataTableCVEF_CMO = cmd.GetDataTable();
            cmd.Connection.Close();


            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.Elapsed);

            return dataTableCVEF_CMO;

        }
        public static DataTable RegresaReconstruccionInvMP(DateTime Fecha)
        {
            DataTable dataTableReconstruccionInvMP = new DataTable();
            string connStr = "";
            using (var dbContext = new SIPNegocioContext())
            {
                connStr = dbContext.Database.Connection.ConnectionString;
            }

            SqlServerCommand cmd = new SqlServerCommand();
            cmd.Connection = DALUtil.GetConnection(connStr);
            cmd.ObjectName = "usp_RepRecInvMP";
            cmd.Parameters.Add(new SqlParameter("@fecha",Fecha));
            dataTableReconstruccionInvMP = cmd.GetDataTable();
            cmd.Connection.Close();

            foreach (DataRow dataRow in dataTableReconstruccionInvMP.Rows)
            {
                string Observaciones = "";
                if (dataRow["COSTO"].ToString() == "0.00") //LA FUNCION NO PUDO CALCULAR EL COSTO PROMEDIO
                {
                    DataTable dataTableCostoYFecha = RegresaCostoYFecha(dataRow["ARTICULO"].ToString(), Fecha, ref Observaciones);
                    if (dataTableCostoYFecha.Rows.Count > 0)
                    {
                        dataRow.SetField<decimal>("COSTO", Convert.ToDecimal(dataTableCostoYFecha.Rows[0]["COSTO"]));
                        dataRow.SetField<string>("OBSERVACION", Observaciones==""?"No se pudo calcular el costo promedio.":Observaciones);
                        System.Diagnostics.Debug.WriteLine(string.Format("Costo: {0}, Obs: {1}", dataTableCostoYFecha.Rows[0]["COSTO"], Observaciones));
                    }
                    else
                    {
                        dataRow.SetField<decimal>("COSTO", 0);
                        //dataRow.SetField<string>("OBSERVACION", string.Empty);
                        dataRow.SetField<string>("OBSERVACION",Observaciones);
                        System.Diagnostics.Debug.WriteLine(string.Format("Costo: {0}, Obs: {1}", 0, Observaciones));
                    }
                }
                else
                {
                    dataRow.SetField<string>("OBSERVACION", Observaciones);
                }
                
            }


            return dataTableReconstruccionInvMP;
        }

        public static DataTable RegresaReconstruccionInvPP(DateTime Fecha)
        {
            System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

            

            DataTable dataTableRIPP = new DataTable();
            DataTable dataTableComp49 = new DataTable();
            string conStr = "";
            using (var dbContext = new SIPNegocioContext())
            {
                conStr = dbContext.Database.Connection.ConnectionString;
            }

            SqlServerCommand cmd = new SqlServerCommand();
            cmd.Connection = DALUtil.GetConnection(conStr);
            cmd.ObjectName = "usp_RepReconstruccionYCosteo";
            cmd.Parameters.Add(new SqlParameter("@fecha",Fecha));            
            dataTableRIPP = cmd.GetDataTable();            
            cmd.Connection.Close();

            
            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.Elapsed);
            return dataTableRIPP;
        }
        /// <summary>
        /// Este método puede tardar hasta 3 minutos
        /// </summary>
        /// <param name="Fecha"></param>
        /// <returns></returns>
        public static DataTable RegresaReconstruccionInvPT(DateTime Fecha)
        {
            System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();            


            DataTable dataTableRIPP = new DataTable();            
            string conStr = "";
            using (var dbContext = new SIPNegocioContext())
            {
                conStr = dbContext.Database.Connection.ConnectionString;
            }

            SqlServerCommand cmd = new SqlServerCommand();
            cmd.Connection = DALUtil.GetConnection(conStr);                             
            cmd.ObjectName = "usp_RepReconstruccionYCosteoPT";
            cmd.Parameters.Add(new SqlParameter("@fecha", Fecha));
            dataTableRIPP = cmd.GetDataTable();
            cmd.Connection.Close();


            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.Elapsed);
            return dataTableRIPP;
        }
        public static DataTable RegresaReconstruccionInvPT_CMO(DateTime Fecha)
        {
            System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();


            DataTable dataTableRIPP_CMO = new DataTable();
            string conStr = "";
            using (var dbContext = new SIPNegocioContext())
            {
                conStr = dbContext.Database.Connection.ConnectionString;
            }

            SqlServerCommand cmd = new SqlServerCommand();
            cmd.Connection = DALUtil.GetConnection(conStr);
            cmd.ObjectName = "usp_RepReconstruccionYCosteoPT_CMO";
            cmd.Parameters.Add(new SqlParameter("@fecha", Fecha));
            dataTableRIPP_CMO = cmd.GetDataTable();
            cmd.Connection.Close();


            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.Elapsed);
            return dataTableRIPP_CMO;
        }

        private static DataTable RegresaCostoYFecha(string Articulo, DateTime Fecha, ref string Observacion)
        {
            DataTable dataTableCostoYFecha = new DataTable();
            string connStr = "";
            using (var dbContext = new SIPNegocioContext())
            {
                connStr = dbContext.Database.Connection.ConnectionString;
            }

            SqlServerCommand cmd = new SqlServerCommand();
            cmd.Connection = DALUtil.GetConnection(connStr);
            cmd.ObjectName = "usp_RegresaCostoInventarioMP2";
            cmd.Parameters.Add(new SqlParameter("@operador", "<="));
            cmd.Parameters.Add(new SqlParameter("@order", "DESC"));
            cmd.Parameters.Add(new SqlParameter("@articulo", Articulo));
            cmd.Parameters.Add(new SqlParameter("@fecha", Fecha));
            dataTableCostoYFecha = cmd.GetDataTable();
            cmd.Connection.Close();
            if (dataTableCostoYFecha.Rows.Count == 0)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@operador", ">"));
                cmd.Parameters.Add(new SqlParameter("@order", "ASC"));
                cmd.Parameters.Add(new SqlParameter("@articulo", Articulo));
                cmd.Parameters.Add(new SqlParameter("@fecha", Fecha));
                dataTableCostoYFecha = cmd.GetDataTable();
                if (dataTableCostoYFecha.Rows.Count == 0)
                {
                    cmd.ObjectName = "usp_RegresaCostoInventarioMP";
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("@operador", "<="));
                    cmd.Parameters.Add(new SqlParameter("@order", "DESC"));
                    cmd.Parameters.Add(new SqlParameter("@articulo", Articulo));
                    cmd.Parameters.Add(new SqlParameter("@fecha", Fecha));
                    dataTableCostoYFecha = cmd.GetDataTable();
                    if (dataTableCostoYFecha.Rows.Count == 0)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SqlParameter("@operador", ">"));
                        cmd.Parameters.Add(new SqlParameter("@order", "ASC"));
                        cmd.Parameters.Add(new SqlParameter("@articulo", Articulo));
                        cmd.Parameters.Add(new SqlParameter("@fecha", Fecha));
                        dataTableCostoYFecha = cmd.GetDataTable();
                        if (dataTableCostoYFecha.Rows.Count == 0)
                        {
                            Observacion = string.Format("No hay Compras Efectivas Anteriores ni Posteriores; ni Movimientos de Compra por Ajuste Anteriores ni Posteriores para el Producto '{0}'", Articulo);
                        }
                        else
                        {
                            Observacion = string.Format("No hay Compras Efectivas Anteriores ni Posteriores; ni Movimientos de Compra por Ajuste Anteriores para el Producto '{0}' . Se costea con el Movimiento de Compra por Ajuste del {1}", Articulo, Convert.ToDateTime(dataTableCostoYFecha.Rows[0]["FECHA_DOCU"]).ToShortDateString());
                        }
                    }
                    else
                    {
                        Observacion = string.Format("No hay Compras Efectivas Anteriores ni Posteriores para el Producto '{0}' . Se costea con el Movimiento de Compra por Ajuste del {1}", Articulo, Convert.ToDateTime(dataTableCostoYFecha.Rows[0]["FECHA_DOCU"]).ToShortDateString());
                    }
                }
                else
                {
                    Observacion = string.Format("No hay Compras Efectivas Anteriores para el Producto '{0}' . Se costea con la compra del {1}", Articulo, Convert.ToDateTime(dataTableCostoYFecha.Rows[0]["FECHA_DOC"]).ToShortDateString());
                }
            }
            
            return dataTableCostoYFecha;
        }

        public static void GeneraArchivoExcelMP(DataTable ReconstruccionInvMP, DateTime Fecha, string RutaYNombreArchivo)
        {
            HSSFWorkbook libro = new HSSFWorkbook();
            ISheet hoja = libro.CreateSheet("Hoja1");

            //fmto. negrillas:

            ICellStyle estiloNegritas = libro.CreateCellStyle();
            IFont fuenteNegritas = libro.CreateFont();
            fuenteNegritas.Boldweight = (short)FontBoldWeight.Bold;
            estiloNegritas.SetFont(fuenteNegritas);

            //fmto numérico con separador de miles
            IDataFormat dataFormat2Decimales = libro.CreateDataFormat();
            ICellStyle cellStyle2Decimales = libro.CreateCellStyle();
            cellStyle2Decimales.DataFormat = dataFormat2Decimales.GetFormat("###,##0.00");

            //titulo
            IRow renglonTitulo = hoja.CreateRow(0);
            ICell celdaTitulo = renglonTitulo.CreateCell(0);
            celdaTitulo.CellStyle = estiloNegritas;
            celdaTitulo.SetCellValue(string.Format("Reporte de Reconstruccion de inventario MP al {0}", Fecha.ToShortDateString()));
            celdaTitulo.CellStyle.Alignment = HorizontalAlignment.Center;
            hoja.AddMergedRegion(new CellRangeAddress(0, 0, 0, 8));

            //encabezados
            IRow renglonEncabezados = hoja.CreateRow(2);

            ICell celdaEncArticulo = renglonEncabezados.CreateCell(0);
            celdaEncArticulo.CellStyle = estiloNegritas;
            celdaEncArticulo.SetCellValue("Artículo");

            ICell celdaEncDesc = renglonEncabezados.CreateCell(1);
            celdaEncDesc.CellStyle = estiloNegritas;
            celdaEncDesc.SetCellValue("Descripción");

            ICell celdaEncExist = renglonEncabezados.CreateCell(2);
            celdaEncExist.CellStyle = estiloNegritas;
            celdaEncExist.SetCellValue("Existencia actual");

            ICell celdaEncEntradas = renglonEncabezados.CreateCell(3);
            celdaEncEntradas.CellStyle = estiloNegritas;
            celdaEncEntradas.SetCellValue("Entradas");

            ICell celdaEncSalidas = renglonEncabezados.CreateCell(4);
            celdaEncSalidas.CellStyle = estiloNegritas;
            celdaEncSalidas.SetCellValue("Salidas");

            ICell celdaEncInvR = renglonEncabezados.CreateCell(5);
            celdaEncInvR.CellStyle = estiloNegritas;
            celdaEncInvR.SetCellValue("Inv Reconst");

            ICell celdaEncLinea = renglonEncabezados.CreateCell(6);
            celdaEncLinea.CellStyle = estiloNegritas;
            celdaEncLinea.SetCellValue("Linea");

            ICell celdaEncCosto = renglonEncabezados.CreateCell(7);
            celdaEncCosto.CellStyle = estiloNegritas;
            celdaEncCosto.SetCellValue("Costo");

            ICell celdaEncImporte = renglonEncabezados.CreateCell(8);
            celdaEncImporte.CellStyle = estiloNegritas;
            celdaEncImporte.SetCellValue("Importe");

            ICell celdaEncObsrvaciones = renglonEncabezados.CreateCell(10);
            celdaEncObsrvaciones.CellStyle = estiloNegritas;
            celdaEncObsrvaciones.SetCellValue("Observaciones");


            int iRenglonDetalle = 4;
            foreach (DataRow dataRow in ReconstruccionInvMP.Rows)
            {
                IRow renglonDetalle = hoja.CreateRow(iRenglonDetalle);
                ICell celdaDetalle = renglonDetalle.CreateCell(0);
                celdaDetalle.SetCellValue(dataRow["ARTICULO"].ToString());

                ICell celdaDesc = renglonDetalle.CreateCell(1);
                celdaDesc.SetCellValue(dataRow["DESCRIPCION"].ToString());

                ICell celdaExist = renglonDetalle.CreateCell(2);
                celdaExist.SetCellValue(Convert.ToDouble(dataRow["EXISTENCIA"].ToString()));
                celdaExist.CellStyle = cellStyle2Decimales;

                ICell celdaEnt = renglonDetalle.CreateCell(3);
                celdaEnt.SetCellValue(Convert.ToDouble(dataRow["ENTRADAS"].ToString()));
                celdaEnt.CellStyle = cellStyle2Decimales;

                ICell celdaSal = renglonDetalle.CreateCell(4);
                celdaSal.SetCellValue(Convert.ToDouble(dataRow["SALIDAS"].ToString()));
                celdaSal.CellStyle = cellStyle2Decimales;

                ICell celdaIR = renglonDetalle.CreateCell(5);
                celdaIR.SetCellFormula(string.Format("C{0}-D{0}+E{0}",iRenglonDetalle + 1));
                celdaIR.CellStyle = cellStyle2Decimales;


                ICell celdaLinea = renglonDetalle.CreateCell(6);
                celdaLinea.SetCellValue(dataRow["LINEA"].ToString());

                ICell celdaCosto = renglonDetalle.CreateCell(7);
                celdaCosto.SetCellValue(Convert.ToDouble(dataRow["COSTO"].ToString()));
                celdaCosto.CellStyle = cellStyle2Decimales;

                ICell celdaImp = renglonDetalle.CreateCell(8);
                celdaImp.SetCellFormula(string.Format("H{0}*F{0}", iRenglonDetalle + 1));
                celdaImp.CellStyle = cellStyle2Decimales;

                ICell celdaCom = renglonDetalle.CreateCell(10);
                celdaCom.SetCellValue(dataRow["OBSERVACION"].ToString());


                iRenglonDetalle++;
            }
            IRow renglonSumatorias = hoja.CreateRow(iRenglonDetalle + 3);
            //sumatorias
            ICell celdaSumExist = renglonSumatorias.CreateCell(2);
            celdaSumExist.SetCellFormula(string.Format("SUM(C4:C{0})", iRenglonDetalle));
            celdaSumExist.CellStyle = cellStyle2Decimales;
            celdaSumExist.CellStyle = estiloNegritas;

            ICell celdaSumEnt = renglonSumatorias.CreateCell(3);
            celdaSumEnt.SetCellFormula(string.Format("SUM(D4:D{0})", iRenglonDetalle));
            celdaSumEnt.CellStyle = cellStyle2Decimales;
            celdaSumEnt.CellStyle = estiloNegritas;

            ICell celdaSumSal = renglonSumatorias.CreateCell(4);
            celdaSumSal.SetCellFormula(string.Format("SUM(E4:E{0})", iRenglonDetalle));
            celdaSumSal.CellStyle = cellStyle2Decimales;
            celdaSumSal.CellStyle = estiloNegritas;

            ICell celdaSumInvR = renglonSumatorias.CreateCell(5);
            celdaSumInvR.SetCellFormula(string.Format("SUM(F4:F{0})", iRenglonDetalle));
            celdaSumInvR.CellStyle = cellStyle2Decimales;
            celdaEncInvR.CellStyle = estiloNegritas;

            ICell celdaSumImp = renglonSumatorias.CreateCell(8);
            celdaSumImp.SetCellFormula(string.Format("SUM(I4:I{0})", iRenglonDetalle));
            celdaSumImp.CellStyle = cellStyle2Decimales;
            celdaSumImp.CellStyle = estiloNegritas;

            for (int i = 0; i < 9; i++)
            {
                hoja.AutoSizeColumn(i);
            }


            if (File.Exists(RutaYNombreArchivo))
            {
                File.Delete(RutaYNombreArchivo);
            }
            FileStream fs = new FileStream(RutaYNombreArchivo, FileMode.CreateNew);
            libro.Write(fs);
            fs.Close();

        }

        public static void GeneraArchivoExcelPP(DataTable dataTablePP, DateTime FechaHasta, string archivoTemporal)
        {

            HSSFWorkbook libro = new HSSFWorkbook();
            ISheet hoja = libro.CreateSheet("Hoja1");

            ICellStyle estilo = libro.CreateCellStyle();
            IFont font = libro.CreateFont();
            
            //Tipo de letra
            font.FontName = "Calibri";
            font.FontHeightInPoints = 11;

            estilo.SetFont(font);


            hoja.SetDefaultColumnStyle(0, estilo);
            hoja.SetDefaultColumnStyle(1, estilo);
            hoja.SetDefaultColumnStyle(2, estilo);
            hoja.SetDefaultColumnStyle(3, estilo);
            hoja.SetDefaultColumnStyle(4, estilo);
            hoja.SetDefaultColumnStyle(7, estilo);
            hoja.SetDefaultColumnStyle(8, estilo);
            hoja.SetDefaultColumnStyle(9, estilo);


            //fmto numérico miles y con 2 decimales
            IDataFormat dataFormat2Decimales = libro.CreateDataFormat();
            ICellStyle cellStyle2Decimales = libro.CreateCellStyle();
            cellStyle2Decimales.DataFormat = dataFormat2Decimales.GetFormat("###,##0.00");
            cellStyle2Decimales.SetFont(font);
            hoja.SetDefaultColumnStyle(5, cellStyle2Decimales);
            hoja.SetDefaultColumnStyle(6, cellStyle2Decimales);

            //titulo
            IRow renglonTitulo = hoja.CreateRow(0);
            ICell celdaTitulo = renglonTitulo.CreateCell(1);
            celdaTitulo.SetCellValue(string.Format("Reporte de Reconstruccion de inventario PP al {0}", FechaHasta.ToShortDateString()));
         
            //encabezados
            IRow renglonEncabezados = hoja.CreateRow(3);
            ICell celdaEncArticulo = renglonEncabezados.CreateCell(0);
            celdaEncArticulo.SetCellValue("Artículo");

            ICell celdaEncLinea = renglonEncabezados.CreateCell(1);
            celdaEncLinea.SetCellValue("Línea");

            ICell celdaEncPPActual = renglonEncabezados.CreateCell(2);
            celdaEncPPActual.SetCellValue("PP Actual");

            ICell celdaEncRecibidos = renglonEncabezados.CreateCell(3);
            celdaEncRecibidos.SetCellValue("Recibidos");

            ICell celdaEncEnviados = renglonEncabezados.CreateCell(4);
            celdaEncEnviados.SetCellValue("Enviados");

            ICell celdaEncPPRecons = renglonEncabezados.CreateCell(5);
            celdaEncPPRecons.SetCellValue("PP recons");

            ICell celdaEncCosto = renglonEncabezados.CreateCell(6);
            celdaEncCosto.SetCellValue("Costo");

            ICell celdaEncObservaciones = renglonEncabezados.CreateCell(8);
            celdaEncObservaciones.SetCellValue("Observaciones");

            

            int iRenglonDetalle = 5;
            foreach (DataRow dataRow in dataTablePP.Rows)
            {
                IRow renglonDetalle = hoja.CreateRow(iRenglonDetalle);
                ICell celdaDetalle = renglonDetalle.CreateCell(0);
                celdaDetalle.SetCellValue(dataRow["Articulo"].ToString());

                ICell celdaLinea = renglonDetalle.CreateCell(1);
                celdaLinea.SetCellValue(dataRow["Linea"].ToString());
      
                ICell celdaPPActual = renglonDetalle.CreateCell(2);
                celdaPPActual.SetCellValue(Convert.ToDouble(dataRow["PP Actual"].ToString()));
                       
                ICell celdaRecib = renglonDetalle.CreateCell(3);
                celdaRecib.SetCellValue(Convert.ToDouble(dataRow["Recibidos"].ToString()));
             
                ICell celdaEnviad = renglonDetalle.CreateCell(4);
                celdaEnviad.SetCellValue(Convert.ToDouble(dataRow["Enviados"].ToString()));
             

                //sumatoria =C6+D6-E6
                ICell celdaPPReco = renglonDetalle.CreateCell(5);
                celdaPPReco.SetCellFormula(string.Format("C{0}+D{0}-E{0}", iRenglonDetalle+1));
                celdaPPReco.CellStyle = cellStyle2Decimales;
   
 
                ICell celdaCosto = renglonDetalle.CreateCell(6);
                celdaCosto.SetCellValue(Convert.ToDouble(dataRow["COSTO"].ToString()));
                celdaCosto.CellStyle = cellStyle2Decimales;

                ICell celdaCom = renglonDetalle.CreateCell(8);
                celdaCom.SetCellValue(dataRow["Observacion"].ToString());

                iRenglonDetalle++;
            }
            IRow renglonSumatorias = hoja.CreateRow(iRenglonDetalle + 2);
            //sumatorias
            ICell celdaSumExist = renglonSumatorias.CreateCell(2);
            celdaSumExist.SetCellFormula(string.Format("SUM(C4:C{0})", iRenglonDetalle));
            celdaSumExist.CellStyle = cellStyle2Decimales;

            ICell celdaSumInvR = renglonSumatorias.CreateCell(5);
            celdaSumInvR.SetCellFormula(string.Format("SUM(F4:F{0})", iRenglonDetalle));
            celdaSumInvR.CellStyle = cellStyle2Decimales;

            hoja.SetColumnWidth(0, ExcelNpoiUtil.AnchoColumna(80));
            hoja.SetColumnWidth(1, ExcelNpoiUtil.AnchoColumna(80));
            hoja.SetColumnWidth(2, ExcelNpoiUtil.AnchoColumna(80));
            hoja.SetColumnWidth(3, ExcelNpoiUtil.AnchoColumna(80));
            hoja.SetColumnWidth(4, ExcelNpoiUtil.AnchoColumna(80));
            hoja.SetColumnWidth(5, ExcelNpoiUtil.AnchoColumna(80));
            hoja.SetColumnWidth(6, ExcelNpoiUtil.AnchoColumna(80));
            hoja.SetColumnWidth(7, ExcelNpoiUtil.AnchoColumna(80));
            hoja.SetColumnWidth(8, ExcelNpoiUtil.AnchoColumna(80));
            hoja.SetColumnWidth(9, ExcelNpoiUtil.AnchoColumna(80));

            FileStream fs = new FileStream(archivoTemporal, FileMode.CreateNew);
            libro.Write(fs);
            fs.Close();
        }

        public static void GeneraArchivoExcelPT(DataTable dataTablePT, DateTime FechaHasta, string archivoTemporal)
        {
            HSSFWorkbook libro = new HSSFWorkbook();
            ISheet hoja = libro.CreateSheet("Hoja1");

            ICellStyle estilo = libro.CreateCellStyle();
            IFont font = libro.CreateFont();

            //fmto. negrillas:

            ICellStyle estiloNegritas = libro.CreateCellStyle();
            IFont fuenteNegritas = libro.CreateFont();
            fuenteNegritas.Boldweight = (short)FontBoldWeight.Bold;
            estiloNegritas.SetFont(fuenteNegritas);

            //Tipo de letra
            font.FontName = "Calibri";
            font.FontHeightInPoints = 11;

            estilo.SetFont(font);


            hoja.SetDefaultColumnStyle(0, estilo);
            hoja.SetDefaultColumnStyle(1, estilo);
            hoja.SetDefaultColumnStyle(2, estilo);
            hoja.SetDefaultColumnStyle(3, estilo);
            hoja.SetDefaultColumnStyle(4, estilo);
            hoja.SetDefaultColumnStyle(5, estilo);
            hoja.SetDefaultColumnStyle(8, estilo);
            hoja.SetDefaultColumnStyle(9, estilo);

            hoja.CreateFreezePane(0,5);

            //fmto numérico miles y con 2 decimales
            IDataFormat dataFormat2Decimales = libro.CreateDataFormat();
            ICellStyle cellStyle2Decimales = libro.CreateCellStyle();
            cellStyle2Decimales.DataFormat = dataFormat2Decimales.GetFormat("###,##0.00");
            cellStyle2Decimales.SetFont(font);
            hoja.SetDefaultColumnStyle(6, cellStyle2Decimales);
            hoja.SetDefaultColumnStyle(7, cellStyle2Decimales);

            //titulo
            IRow renglonTitulo = hoja.CreateRow(0);
            ICell celdaTitulo = renglonTitulo.CreateCell(0);
            celdaTitulo.CellStyle = estiloNegritas;
            celdaTitulo.SetCellValue(string.Format("Reporte de Reconstruccion de inventario PT al {0}", FechaHasta.ToShortDateString()));
            celdaTitulo.CellStyle.Alignment = HorizontalAlignment.Center;
            hoja.AddMergedRegion(new CellRangeAddress(0, 0, 0, 7));
            

            //encabezados
            IRow renglonEncabezados = hoja.CreateRow(2);
            ICell celdaEncArticulo = renglonEncabezados.CreateCell(0);
            celdaEncArticulo.SetCellValue("Artículo");

            ICell celdaEncExis = renglonEncabezados.CreateCell(1);
            celdaEncExis.SetCellValue("Existencia actual");

            ICell celdaEncEntradas = renglonEncabezados.CreateCell(2);
            celdaEncEntradas.SetCellValue("Entradas");

            ICell celdaEncSalidas = renglonEncabezados.CreateCell(3);
            celdaEncSalidas.SetCellValue("Salidas");

            ICell celdaEncInv = renglonEncabezados.CreateCell(4);
            celdaEncInv.SetCellValue("Inv Reconst");

            ICell celdaEncLinea = renglonEncabezados.CreateCell(5);
            celdaEncLinea.SetCellValue("Línea");

            ICell celdaEncCosto = renglonEncabezados.CreateCell(6);
            celdaEncCosto.SetCellValue("Costo");

            ICell celdaEncImpor = renglonEncabezados.CreateCell(7);
            celdaEncImpor.SetCellValue("Importe");



            int iRenglonDetalle = 4;
            foreach (DataRow dataRow in dataTablePT.Rows)
            {
                IRow renglonDetalle = hoja.CreateRow(iRenglonDetalle);
                ICell celdaDetalle = renglonDetalle.CreateCell(0);
                celdaDetalle.SetCellValue(dataRow[0].ToString());

                ICell celdaExis = renglonDetalle.CreateCell(1);
                celdaExis.SetCellValue(Convert.ToDouble(dataRow[1].ToString()));

                ICell celdaEntrada = renglonDetalle.CreateCell(2);
                celdaEntrada.SetCellValue(Convert.ToDouble(dataRow["ENTRADAS"].ToString()));

                ICell celdaSalida = renglonDetalle.CreateCell(3);
                celdaSalida.SetCellValue(Convert.ToDouble(dataRow["SALIDAS"].ToString()));

                //Suma Inv Reconst=B1-C1+D1
                ICell celdaInvRecons = renglonDetalle.CreateCell(4);
                celdaInvRecons.SetCellFormula(string.Format("B{0}-C{0}+D{0}", iRenglonDetalle + 1));

                                
                ICell celdaLinea = renglonDetalle.CreateCell(5);
                celdaLinea.SetCellValue(dataRow["LIN_PROD"].ToString());

                ICell celdaCosto = renglonDetalle.CreateCell(6);
                celdaCosto.SetCellValue(Convert.ToDouble(dataRow["COSTO"].ToString()));
                celdaCosto.CellStyle = cellStyle2Decimales;

                //Importe =G1*E1
                ICell celdaImpor = renglonDetalle.CreateCell(7);
                celdaImpor.SetCellFormula(string.Format("G{0}*E{0}", iRenglonDetalle + 1));
                celdaImpor.CellStyle = cellStyle2Decimales;


                ICell celdaComen = renglonDetalle.CreateCell(9);
                celdaComen.SetCellValue(dataRow["COMENTARIO"].ToString());

                iRenglonDetalle++;
            }
            IRow renglonSumatorias = hoja.CreateRow(iRenglonDetalle + 2);
            //sumatorias

            ICell celdaSumExist = renglonSumatorias.CreateCell(1);
            celdaSumExist.SetCellFormula(string.Format("SUM(B5:B{0})", iRenglonDetalle));
           

            ICell celdaSumEntrada = renglonSumatorias.CreateCell(2);
            celdaSumEntrada.SetCellFormula(string.Format("SUM(C5:C{0})", iRenglonDetalle));

            ICell celdaSumSalida = renglonSumatorias.CreateCell(3);
            celdaSumSalida.SetCellFormula(string.Format("SUM(D5:D{0})", iRenglonDetalle));

            ICell celdaSumInvR = renglonSumatorias.CreateCell(4);
            celdaSumInvR.SetCellFormula(string.Format("SUM(E5:E{0})", iRenglonDetalle));
           
     
            ICell celdaSumImp = renglonSumatorias.CreateCell(7);
            celdaSumImp.SetCellFormula(string.Format("SUM(H5:H{0})", iRenglonDetalle));
            celdaSumImp.CellStyle = cellStyle2Decimales;

            hoja.SetColumnWidth(0, ExcelNpoiUtil.AnchoColumna(120));
            hoja.SetColumnWidth(1, ExcelNpoiUtil.AnchoColumna(257));
            hoja.SetColumnWidth(2, ExcelNpoiUtil.AnchoColumna(80));
            hoja.SetColumnWidth(3, ExcelNpoiUtil.AnchoColumna(80));
            hoja.SetColumnWidth(4, ExcelNpoiUtil.AnchoColumna(80));
            hoja.SetColumnWidth(5, ExcelNpoiUtil.AnchoColumna(80));
            hoja.SetColumnWidth(6, ExcelNpoiUtil.AnchoColumna(80));
            hoja.SetColumnWidth(7, ExcelNpoiUtil.AnchoColumna(94));
            
            FileStream fs = new FileStream(archivoTemporal, FileMode.CreateNew);
            libro.Write(fs);
            fs.Close();
        }
        public static void GeneraArchivoExcelPT_CMO(DataTable dataTablePT_CMO, DateTime FechaHasta, string archivoTemporal)
        {
            GeneraArchivoExcelPT(dataTablePT_CMO, FechaHasta, archivoTemporal);
        }

        public static void GeneraArchivoExcelCVEF(DataTable dataTableCVEF, DateTime FechaDesde, DateTime FechaHasta, string archivoTemporal)
        {
            HSSFWorkbook libro = new HSSFWorkbook();
            ISheet hoja = libro.CreateSheet("Hoja1");
    
            ICellStyle estilo = libro.CreateCellStyle();
            IFont font = libro.CreateFont();

            //Tipo de letra
            font.FontName = "Calibri";
            font.FontHeightInPoints = 11;

            estilo.SetFont(font);


            hoja.SetDefaultColumnStyle(0, estilo);
            hoja.SetDefaultColumnStyle(1, estilo);
            hoja.SetDefaultColumnStyle(2, estilo);
            hoja.SetDefaultColumnStyle(3, estilo);
            hoja.SetDefaultColumnStyle(4, estilo);
            hoja.SetDefaultColumnStyle(5, estilo);
            hoja.SetDefaultColumnStyle(8, estilo);
            hoja.SetDefaultColumnStyle(9, estilo);

            //fmto numérico miles y con 2 decimales
            IDataFormat dataFormat2Decimales = libro.CreateDataFormat();
            ICellStyle cellStyle2Decimales = libro.CreateCellStyle();
            cellStyle2Decimales.DataFormat = dataFormat2Decimales.GetFormat("###,##0.00");
            cellStyle2Decimales.SetFont(font);
            hoja.SetDefaultColumnStyle(6, cellStyle2Decimales);
            hoja.SetDefaultColumnStyle(7, cellStyle2Decimales);

            //fmto porcentaje y 4 decimales
            IDataFormat dataFormat4Decimales = libro.CreateDataFormat();
            short cuatroDig = dataFormat4Decimales.GetFormat("0.0000%");

            ICellStyle cellStyle4Decimales = libro.CreateCellStyle();
            cellStyle4Decimales = libro.CreateCellStyle();
            cellStyle4Decimales.DataFormat = cuatroDig;


            //fmto porcentaje y 4 decimales
            IDataFormat dataFormat2DecimalesPor = libro.CreateDataFormat();
            short dosDig = dataFormat2DecimalesPor.GetFormat("0.00%");

            ICellStyle cellStyle2DecimalesPor = libro.CreateCellStyle();
            cellStyle2DecimalesPor = libro.CreateCellStyle();
            cellStyle2DecimalesPor.DataFormat = dosDig;

            //titulo
            IRow renglonTitulo = hoja.CreateRow(0);
            ICell celdaTitulo = renglonTitulo.CreateCell(0);
            celdaTitulo.SetCellValue(string.Format("Reporte costo de lo vendido entre fechas"));
            
            //Fecha Emitido
            IRow renglonFechEmitido = hoja.CreateRow(2);
            ICell celdaFechEmitido = renglonFechEmitido.CreateCell(0);
            celdaFechEmitido.SetCellValue(string.Format("Emitido del      {0}", FechaDesde.ToShortDateString()));

            //Fecha Al
            IRow renglonFechAl = hoja.CreateRow(3);
            ICell celdaFechAl = renglonFechAl.CreateCell(0);
            celdaFechAl.SetCellValue(string.Format("Al                          {0}", FechaHasta.ToShortDateString()));

            

            //encabezados
            IRow renglonEncabezados = hoja.CreateRow(5);
            ICell celdaEncModelo = renglonEncabezados.CreateCell(0);
            celdaEncModelo.SetCellValue("MODELO");

            ICell celdaEncDesc = renglonEncabezados.CreateCell(1);
            celdaEncDesc.SetCellValue("DESCRIPCIÓN");

            ICell celdaEncFact = renglonEncabezados.CreateCell(2);
            celdaEncFact.SetCellValue("FACTURA");

            ICell celdaEncNC = renglonEncabezados.CreateCell(3);
            celdaEncNC.SetCellValue("NC");

            ICell celdaEncSuma = renglonEncabezados.CreateCell(4);
            celdaEncSuma.SetCellValue("Suma");

            ICell celdaEncPart = renglonEncabezados.CreateCell(5);
            celdaEncPart.SetCellValue("% PART");

            ICell celdaEncCosto = renglonEncabezados.CreateCell(6);
            celdaEncCosto.SetCellValue("Costo");

            ICell celdaEncImpor = renglonEncabezados.CreateCell(7);
            celdaEncImpor.SetCellValue("Importe");

            ICell celdaEncComentario = renglonEncabezados.CreateCell(9);
            celdaEncComentario.SetCellValue("Comentario");

            hoja.CreateFreezePane(0, 6);

            int iRenglonDetalle = 6;
            foreach (DataRow dataRow in dataTableCVEF.Rows)
            {
                IRow renglonDetalle = hoja.CreateRow(iRenglonDetalle);
                ICell celdaModelo = renglonDetalle.CreateCell(0);
                celdaModelo.SetCellValue(dataRow["MODELO"].ToString());

                ICell celdaDesc = renglonDetalle.CreateCell(1);
                celdaDesc.SetCellValue(dataRow["DESCRIPCION"].ToString());

                ICell celdaFac = renglonDetalle.CreateCell(2);
                celdaFac.SetCellValue(Convert.ToDouble(dataRow["FACTURA"].ToString()));

                ICell celdaNc = renglonDetalle.CreateCell(3);
                //celdaNc.SetCellValue(dataRow["NC"].ToString());
                celdaNc.SetCellFormula(string.Format("-{0}", dataRow["NC"].ToString()));
                
                //Suma =C7+D7
                ICell celdaSuma = renglonDetalle.CreateCell(4);
                celdaSuma.SetCellFormula(string.Format("C{0}+D{0}", iRenglonDetalle + 1));


                // % PART =E7/E1076
                ICell celdaPart = renglonDetalle.CreateCell(5);
                celdaPart.SetCellFormula(string.Format("E{0}/E{1}", iRenglonDetalle + 1, dataTableCVEF.Rows.Count + 8));
                celdaPart.CellStyle = cellStyle4Decimales;

                ICell celdaCosto = renglonDetalle.CreateCell(6);
                celdaCosto.SetCellValue(Convert.ToDouble(dataRow["COSTO"].ToString()));
                celdaCosto.CellStyle = cellStyle2Decimales;

                //Importe =G1*E1
                ICell celdaImpor = renglonDetalle.CreateCell(7);
                celdaImpor.SetCellFormula(string.Format("G{0}*E{0}", iRenglonDetalle + 1));
                celdaImpor.CellStyle = cellStyle2Decimales;


                ICell celdaComen = renglonDetalle.CreateCell(9);
                celdaComen.SetCellValue(dataRow["COMENTARIO"].ToString());

                iRenglonDetalle++;
            }
            IRow renglonSumatorias = hoja.CreateRow(iRenglonDetalle + 1);
            //sumatorias

            ICell celdaSumFact = renglonSumatorias.CreateCell(2);
            celdaSumFact.SetCellFormula(string.Format("SUM(C7:C{0})", iRenglonDetalle));
            celdaSumFact.CellStyle = cellStyle2Decimales;

            ICell celdaSumNC = renglonSumatorias.CreateCell(3);
            celdaSumNC.SetCellFormula(string.Format("SUM(D7:D{0})", iRenglonDetalle));

            ICell celdaSumaT= renglonSumatorias.CreateCell(4);
            celdaSumaT.SetCellFormula(string.Format("SUM(E7:E{0})", iRenglonDetalle));
            celdaSumaT.CellStyle = cellStyle2Decimales;
             
            ICell celdaSumPart = renglonSumatorias.CreateCell(5);
            celdaSumPart.SetCellFormula(string.Format("SUM(F7:F{0})", iRenglonDetalle));
            celdaSumPart.CellStyle = cellStyle2DecimalesPor;

            ICell celdaSumImp = renglonSumatorias.CreateCell(7);
            celdaSumImp.SetCellFormula(string.Format("SUM(H7:H{0})", iRenglonDetalle));
            celdaSumImp.CellStyle = cellStyle2Decimales;

            hoja.SetColumnWidth(0, ExcelNpoiUtil.AnchoColumna(103));
            hoja.SetColumnWidth(1, ExcelNpoiUtil.AnchoColumna(256));
            hoja.SetColumnWidth(2, ExcelNpoiUtil.AnchoColumna(80));
            hoja.SetColumnWidth(3, ExcelNpoiUtil.AnchoColumna(80));
            hoja.SetColumnWidth(4, ExcelNpoiUtil.AnchoColumna(80));
            hoja.SetColumnWidth(5, ExcelNpoiUtil.AnchoColumna(80));
            hoja.SetColumnWidth(6, ExcelNpoiUtil.AnchoColumna(80));
            hoja.SetColumnWidth(7, ExcelNpoiUtil.AnchoColumna(86));

            FileStream fs = new FileStream(archivoTemporal, FileMode.CreateNew);
            libro.Write(fs);
            fs.Close();
        }
        public static void GeneraArchivoExcelCVEF_CMO(DataTable dataTableCVEF_CMO, DateTime FechaDesde, DateTime FechaHasta, string archivoTemporal)
        {
            GeneraArchivoExcelCVEF(dataTableCVEF_CMO, FechaDesde, FechaHasta, archivoTemporal);
        }
        public static void GeneraArchivoExcelRCM(int CVE_CPTO, DataSet dataSetCVEF, DateTime FechaDesde, DateTime FechaHasta, string archivoTemporal)
        {
          
            string titulo = dataSetCVEF.Tables[0].Rows[0]["DESCR"].ToString();  //<---- esta variable traerá el título del reportes
            DataTable dataTableRCM = dataSetCVEF.Tables[1];     //<---- este DataTable trae el detalle del reporte
            
            HSSFWorkbook libro = new HSSFWorkbook();
            ISheet hoja = libro.CreateSheet("Hoja1");
    
            ICellStyle estilo = libro.CreateCellStyle();
            IFont font = libro.CreateFont();

            //Tipo de letra
            font.FontName = "Calibri";
            font.FontHeightInPoints = 11;

            estilo.SetFont(font);

            hoja.SetDefaultColumnStyle(0, estilo);
            hoja.SetDefaultColumnStyle(1, estilo);
            hoja.SetDefaultColumnStyle(2, estilo);
            hoja.SetDefaultColumnStyle(3, estilo);
            hoja.SetDefaultColumnStyle(4, estilo);
          
            //fmto numérico miles y con 2 decimales
            IDataFormat dataFormat2Decimales = libro.CreateDataFormat();
            ICellStyle cellStyle2Decimales = libro.CreateCellStyle();
            cellStyle2Decimales.DataFormat = dataFormat2Decimales.GetFormat("###,##0.00");
            cellStyle2Decimales.SetFont(font);
            hoja.SetDefaultColumnStyle(6, cellStyle2Decimales);
            hoja.SetDefaultColumnStyle(7, cellStyle2Decimales);

 
            //titulo
            IRow renglonTitulo = hoja.CreateRow(0);
            ICell celdaTitulo = renglonTitulo.CreateCell(1);
            celdaTitulo.SetCellValue(string.Format("Rep Costeo de mermas del " + FechaDesde.ToShortDateString() + " al " + FechaHasta.ToShortDateString()));
            
            //Título Defectuoso,etc
            IRow renglonTitulo2 = hoja.CreateRow(1);
            ICell celdaTitulo2 = renglonTitulo2.CreateCell(0);
      

            //forma 1
            celdaTitulo2.SetCellValue(CVE_CPTO.ToString() + " " + titulo);



            //encabezados
            IRow renglonEncabezados = hoja.CreateRow(3);
            ICell celdaEncArt = renglonEncabezados.CreateCell(0);
            celdaEncArt.SetCellValue("Artículo");

            ICell celdaEncDesc = renglonEncabezados.CreateCell(1);
            celdaEncDesc.SetCellValue("Descripción");

            ICell celdaEncCantidad = renglonEncabezados.CreateCell(2);
            celdaEncCantidad.SetCellValue("Cantidad");

       
            ICell celdaEncCosto = renglonEncabezados.CreateCell(3);
            celdaEncCosto.SetCellValue("Costo");

            ICell celdaEncImpor = renglonEncabezados.CreateCell(4);
            celdaEncImpor.SetCellValue("Importe");



            int iRenglonDetalle = 5;
            foreach (DataRow dataRow in dataTableRCM.Rows)
            {
                IRow renglonDetalle = hoja.CreateRow(iRenglonDetalle);
                ICell celdaArticulo = renglonDetalle.CreateCell(0);
                celdaArticulo.SetCellValue(dataRow["CLV_ART"].ToString());

                ICell celdaDesc = renglonDetalle.CreateCell(1);
                celdaDesc.SetCellValue(dataRow["DESCR"].ToString());

                ICell celdaCantidad = renglonDetalle.CreateCell(2);
                celdaCantidad.SetCellValue(Convert.ToDouble(dataRow["CANTIDAD"].ToString()));


                ICell celdaCosto = renglonDetalle.CreateCell(3);
                celdaCosto.SetCellValue(Convert.ToDouble(dataRow["COSTO"].ToString()));
                celdaCosto.CellStyle = cellStyle2Decimales;

                //Importe =C1*D1
                ICell celdaImpor = renglonDetalle.CreateCell(4);
                celdaImpor.SetCellFormula(string.Format("C{0}*D{0}", iRenglonDetalle + 1));
                celdaImpor.CellStyle = cellStyle2Decimales;


                

                iRenglonDetalle++;
            }
            IRow renglonSumatorias = hoja.CreateRow(iRenglonDetalle + 1);
            //sumatorias

            ICell celdaSumCant = renglonSumatorias.CreateCell(2);
            celdaSumCant.SetCellFormula(string.Format("SUM(C6:C{0})", iRenglonDetalle));
   
           
            ICell celdaSumaImporte= renglonSumatorias.CreateCell(4);
            celdaSumaImporte.SetCellFormula(string.Format("SUM(E6:E{0})", iRenglonDetalle));
            celdaSumaImporte.CellStyle = cellStyle2Decimales;

            

            hoja.SetColumnWidth(0, ExcelNpoiUtil.AnchoColumna(80));
            hoja.SetColumnWidth(1, ExcelNpoiUtil.AnchoColumna(80));
            hoja.SetColumnWidth(2, ExcelNpoiUtil.AnchoColumna(80));
            hoja.SetColumnWidth(3, ExcelNpoiUtil.AnchoColumna(80));
            hoja.SetColumnWidth(4, ExcelNpoiUtil.AnchoColumna(80));
          

            FileStream fs = new FileStream(archivoTemporal, FileMode.CreateNew);
            libro.Write(fs);
            fs.Close();


        }
        public static void GeneraArchivoExcelRCM_CMO(int CVE_CPTO, DataSet dataSetCVEF, DateTime FechaDesde,DateTime FechaHasta, string archivoTemporal)
        {
            string titulo = dataSetCVEF.Tables[0].Rows[0]["DESCR"].ToString();  //<---- esta variable traerá el título del reportes
            DataTable dataTableRCM = dataSetCVEF.Tables[1];     //<---- este DataTable trae el detalle del reporte

            HSSFWorkbook libro = new HSSFWorkbook();
            ISheet hoja = libro.CreateSheet("Hoja1");

            ICellStyle estilo = libro.CreateCellStyle();
            IFont font = libro.CreateFont();

            //Tipo de letra
            font.FontName = "Calibri";
            font.FontHeightInPoints = 11;

            estilo.SetFont(font);

            hoja.SetDefaultColumnStyle(0, estilo);
            hoja.SetDefaultColumnStyle(1, estilo);
            hoja.SetDefaultColumnStyle(2, estilo);
            hoja.SetDefaultColumnStyle(3, estilo);
            hoja.SetDefaultColumnStyle(4, estilo);
            hoja.SetDefaultColumnStyle(9, estilo);

            //fmto numérico miles y con 2 decimales
            IDataFormat dataFormat2Decimales = libro.CreateDataFormat();
            ICellStyle cellStyle2Decimales = libro.CreateCellStyle();
            cellStyle2Decimales.DataFormat = dataFormat2Decimales.GetFormat("###,##0.00");
            cellStyle2Decimales.SetFont(font);
            hoja.SetDefaultColumnStyle(3, cellStyle2Decimales);
            hoja.SetDefaultColumnStyle(4, cellStyle2Decimales);


            //titulo
            IRow renglonTitulo = hoja.CreateRow(0);
            ICell celdaTitulo = renglonTitulo.CreateCell(1);
            celdaTitulo.SetCellValue(string.Format("Rep Costeo de mermas del " + FechaDesde.ToShortDateString() + " al " + FechaHasta.ToShortDateString()));

            //Título Defectuoso,etc
            IRow renglonTitulo2 = hoja.CreateRow(1);
            ICell celdaTitulo2 = renglonTitulo2.CreateCell(0);
            celdaTitulo2.SetCellValue(CVE_CPTO.ToString() + " " + titulo);



            //encabezados
            IRow renglonEncabezados = hoja.CreateRow(3);
            ICell celdaEncArt = renglonEncabezados.CreateCell(0);
            celdaEncArt.SetCellValue("Artículo");

            ICell celdaEncDesc = renglonEncabezados.CreateCell(1);
            celdaEncDesc.SetCellValue("Descripción");

            ICell celdaEncCantidad = renglonEncabezados.CreateCell(2);
            celdaEncCantidad.SetCellValue("Cantidad");


            ICell celdaEncCosto = renglonEncabezados.CreateCell(3);
            celdaEncCosto.SetCellValue("Costo");

            ICell celdaEncImpor = renglonEncabezados.CreateCell(4);
            celdaEncImpor.SetCellValue("Importe");

            ICell celdaEncComentario = renglonEncabezados.CreateCell(9);
            celdaEncComentario.SetCellValue("Comentario");



            int iRenglonDetalle = 5;
            foreach (DataRow dataRow in dataTableRCM.Rows)
            {
                IRow renglonDetalle = hoja.CreateRow(iRenglonDetalle);
                ICell celdaArticulo = renglonDetalle.CreateCell(0);
                celdaArticulo.SetCellValue(dataRow["MODELO"].ToString());

                ICell celdaDesc = renglonDetalle.CreateCell(1);
                celdaDesc.SetCellValue(dataRow["DESCRIPCION"].ToString());

                ICell celdaCantidad = renglonDetalle.CreateCell(2);
                celdaCantidad.SetCellValue(Convert.ToDouble(dataRow["CANTIDAD"].ToString()));


                ICell celdaCosto = renglonDetalle.CreateCell(3);
                celdaCosto.SetCellValue(Convert.ToDouble(dataRow["COSTO"].ToString()));
                celdaCosto.CellStyle = cellStyle2Decimales;

                //Importe =C1*D1
                ICell celdaImpor = renglonDetalle.CreateCell(4);
                celdaImpor.SetCellFormula(string.Format("C{0}*D{0}", iRenglonDetalle + 1));
                celdaImpor.CellStyle = cellStyle2Decimales;

                ICell celdaComentario = renglonDetalle.CreateCell(9);
                celdaComentario.SetCellValue(dataRow["COMENTARIO"].ToString());

                iRenglonDetalle++;
            }
            IRow renglonSumatorias = hoja.CreateRow(iRenglonDetalle + 2);
            //sumatorias

            ICell celdaSumCant = renglonSumatorias.CreateCell(2);
            celdaSumCant.SetCellFormula(string.Format("SUM(C6:C{0})", iRenglonDetalle));


            ICell celdaSumaImporte = renglonSumatorias.CreateCell(4);
            celdaSumaImporte.SetCellFormula(string.Format("SUM(E6:E{0})", iRenglonDetalle));
            celdaSumaImporte.CellStyle = cellStyle2Decimales;



            hoja.SetColumnWidth(0, ExcelNpoiUtil.AnchoColumna(80));
            hoja.SetColumnWidth(1, ExcelNpoiUtil.AnchoColumna(80));
            hoja.SetColumnWidth(2, ExcelNpoiUtil.AnchoColumna(80));
            hoja.SetColumnWidth(3, ExcelNpoiUtil.AnchoColumna(80));
            hoja.SetColumnWidth(4, ExcelNpoiUtil.AnchoColumna(80));


            FileStream fs = new FileStream(archivoTemporal, FileMode.CreateNew);
            libro.Write(fs);
            fs.Close();
        }
    }
}
