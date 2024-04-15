using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using ulp_dl;
using ulp_dl.SIPReportes;

namespace ulp_bl.Reportes
{
    public enum LadoAEscribir
	{
	    Izquierda,
        Derecha
	}
    public class vw_ExistenciasBase
    {
        public Enumerados.TipoReporteExistenciasBase ExistenciaBase { get; set; }

        private struct SMAXEncabezadoGrupo
        {
            public int CAMPOINTU;
            public string ART_DESC;
            public string MODELO_DESC;

        }

        public static DataTable DevuelvePrimerosArticulos(DataTable DataTableExistencias)
        {
            /*
             * Este método toma como parámetro la Tabla devuelta por el método: DevuelveExistencias(NUMERO_ALMACEN)
             * 
             * y lo que haces es que revisa el primer registro por cada Modelo y suma toda su existencia
             * y Stock máximo y mínimo para todos los registros con ese Modelo haciendo una especie de tabla agrupada
             * 
             * Se así así porque el reporte de existencias tiene este formato (tomar el primer registro de cada Modelo)
             * 
             * Las sumatorias Totales se ocupan para saber cuánta existencia y Stock existe para cada modelo
             * */
            string codigoControlActual = "";
            string codigoControlPrevio = "";



            //se crea estructura de la Tabla que se va a regresar
            DataTable dtPrimerosArticulos = new DataTable();

            dtPrimerosArticulos.Columns.Add("CAMPOINTU",typeof(int));
            dtPrimerosArticulos.Columns.Add("ART_DESC", typeof(string));            
            dtPrimerosArticulos.Columns.Add("DESCR", typeof(string));
            //columnas totalizadas
            dtPrimerosArticulos.Columns.Add("SUMOF_EXIST", typeof(double));
            dtPrimerosArticulos.Columns.Add("SUMOF_PP", typeof(double));
            dtPrimerosArticulos.Columns.Add("SUMOF_SMIN", typeof(double));
            dtPrimerosArticulos.Columns.Add("SUMOF_SMAX", typeof(double));


            //en este ciclo se obtiene siempre el primer registro para cada modelo comparando codigoControlActual y codigoControlPrevio
            foreach (DataRow dataRow in DataTableExistencias.Rows)
            {
                codigoControlActual = dataRow["CAMPOINTU"].ToString() + dataRow["ART_DESC"];

                if (codigoControlPrevio != codigoControlActual)
                {
                    DataRow dataRowPrimerArticulo = dtPrimerosArticulos.NewRow();
                    dataRowPrimerArticulo["CAMPOINTU"] = dataRow["CAMPOINTU"];
                    dataRowPrimerArticulo["ART_DESC"] = dataRow["ART_DESC"];
                    dataRowPrimerArticulo["DESCR"] = dataRow["DESCR"];

                    //este Query se usa para sacar la sumatoria para este modelo en particulas
                    var queryTotales = DataTableExistencias.AsEnumerable()
                        .Where(
                            c =>
                                (int) c["CAMPOINTU"] == (int) dataRow["CAMPOINTU"] && (string) c["ART_DESC"] == (string) dataRow["ART_DESC"])
                            .GroupBy(c => new { 
                                CAMPOINTU = c["CAMPOINTU"], 
                                ART_DESC = c["ART_DESC"] ,                                
                            }).Select(c=> new
                            {
                                c.Key.CAMPOINTU,
                                c.Key.ART_DESC,
                                SumOfExist = c.Sum(e=>(double)e["EXIST"]),
                                SumOfPP = c.Sum(e=>(double)e["COMP_X_REC"]),
                                SumOfSMin = c.Sum(e=>(double)e["STOCK_MIN"]),
                                SumOfSMax = c.Sum(e => (double)e["STOCK_MAX"])
                            }).First();




                    //se asignan los valores de los totalizados para este modelo
                    dataRowPrimerArticulo["SUMOF_EXIST"] = queryTotales.SumOfExist;
                    dataRowPrimerArticulo["SUMOF_PP"] = queryTotales.SumOfPP;
                    dataRowPrimerArticulo["SUMOF_SMIN"] = queryTotales.SumOfSMin;
                    dataRowPrimerArticulo["SUMOF_SMAX"] = queryTotales.SumOfSMax;
                    //se agrega el renglón
                    dtPrimerosArticulos.Rows.Add(dataRowPrimerArticulo);
                }
                codigoControlPrevio =  dataRow["CAMPOINTU"].ToString() + dataRow["ART_DESC"];

            }
            // se regresa la tabla
            return dtPrimerosArticulos;
        }

        public static DataTable DevuelveTotalesGenerales(DataTable DataTablePrimerosArticulos)
        {
            /*
             * Este método devuelve un solo renglón, con columnas.
             * Cada columna de la tabla resultado trae la sumatoria de cada columna de la tabla DataTablePrimerosArticulos
             * y se ocupa para regresar los totales ya procesados sin necesidad de acumular las sumas en un Loop
             * o sin necesidad de un .Compute() de la Tabla
             * 
             * Para usar este método se debe pasar como parámetro la Tabla devuelta por el método: RegresaPrimerosArticulos()
             */

            //Se crea la estructura de la tabla que se va a regresar
            DataTable dtTotalesGenerales = new DataTable();
            dtTotalesGenerales.Columns.Add("TOTAL_EXISTENCIA", typeof (double));
            dtTotalesGenerales.Columns.Add("TOTAL_PP", typeof(double));
            dtTotalesGenerales.Columns.Add("TOTAL_SMIN", typeof(double));
            dtTotalesGenerales.Columns.Add("TOTAL_SMAX", typeof(double));

            //Se suman las columnas 
            var queryTotalesGenerales = (from g in DataTablePrimerosArticulos.AsEnumerable()
                group g by 1
                into d
                select new
                {
                    TOTAL_EXISTENCIA = d.Sum(c => (double) c["SUMOF_EXIST"]),
                    TOTAL_PP = d.Sum(c => (double)c["SUMOF_PP"]),
                    TOTAL_SMIN = d.Sum(c => (double)c["SUMOF_SMIN"]),
                    TOTAL_SMAX = d.Sum(c => (double)c["SUMOF_SMAX"])

                }).First();


            //se crea el nuevo renglón donde se guardarán los totales
            DataRow dataRowTotalesGenerales = dtTotalesGenerales.NewRow();

            //Se guardan los totales en sus respectivos campos
            dataRowTotalesGenerales["TOTAL_EXISTENCIA"] = queryTotalesGenerales.TOTAL_EXISTENCIA;
            dataRowTotalesGenerales["TOTAL_PP"] = queryTotalesGenerales.TOTAL_PP;
            dataRowTotalesGenerales["TOTAL_SMIN"] = queryTotalesGenerales.TOTAL_SMIN;
            dataRowTotalesGenerales["TOTAL_SMAX"] = queryTotalesGenerales.TOTAL_SMAX;

            //se agrega el nuevo renglón a la tabla y se regresa
            dtTotalesGenerales.Rows.Add(dataRowTotalesGenerales);


            return dtTotalesGenerales;

        }

        private IRow Escribe(ISheet Sheet, int Renglon, int Columna, double Valor,bool CrearRenglon = true,short NumberFormat = 0)
        {
            IRow renglon = null;
            if (CrearRenglon)
            {                
                renglon = Sheet.CreateRow(Renglon);
                ICell cell = renglon.CreateCell(Columna);
                //if (NumberFormat > 0)
                //{
                //    cell.CellStyle.DataFormat = NumberFormat;
                //}
                cell.SetCellValue(Valor);
            }
            else
            {
                renglon = Sheet.GetRow(Renglon);
                ICell cell =  renglon.CreateCell(Columna);
                if (NumberFormat > 0)
                {
                    cell.CellStyle.DataFormat = NumberFormat;
                }
                cell.SetCellValue(Valor);
            }
            return renglon;
        }

        private IRow Escribe(ISheet Sheet, int Renglon, int Columna, string Valor, bool CrearRenglon = true,bool EsFormula = false,short NumberFormat = 0)
        {
            IRow renglon = null;
            if (CrearRenglon)
            {
                renglon = Sheet.CreateRow(Renglon);
                if (!EsFormula)
                {
                    ICell cell = renglon.CreateCell(Columna);
                    //if (NumberFormat > 0)
                    //{
                    //    cell.CellStyle.DataFormat = NumberFormat;
                    //}
                    cell.SetCellValue(Valor);
                }
                else
                {
                    ICell cell = renglon.CreateCell(Columna);
                    //if (NumberFormat > 0)
                    //{
                    //    cell.CellStyle.DataFormat = NumberFormat;
                    //}
                    cell.SetCellFormula(Valor);
                    
                }
            }
            else
            {
                renglon = Sheet.GetRow(Renglon);
                if (!EsFormula)
                {
                    ICell cell = renglon.CreateCell(Columna);
                    //if (NumberFormat > 0)
                    //{
                    //    cell.CellStyle.DataFormat = NumberFormat;
                    //}
                    cell.SetCellValue(Valor);
                    
                }
                else
                {
                    ICell cell = renglon.CreateCell(Columna);
                    //if (NumberFormat > 0)
                    //{
                    //    cell.CellStyle.DataFormat = NumberFormat;
                    //}
                    cell.SetCellFormula(Valor);
                    
                }
            }
            return renglon;
        }

        private List<string> DevuelveNotas(Enumerados.TipoReporteExistenciasBase TipoReporteExistencia)
        {
            List<string> notas = new List<string>();

            

            
            
            switch (TipoReporteExistencia)
            {
                case Enumerados.TipoReporteExistenciasBase.SMAX:
                    notas.AddRange(new string[]
                    {
                        "NOTAS",
                        "Formula: MH = SMax - EXISTencia - ProdPrOC",
                        "Donde: MH = Mandar a hacer ",
                        "SMax = StOCk Maximo, este se calcula de la siguiente forma Smax = Smin * 1.5 (como estandar)",
                        "Smin = Stock RECOMENDADO",
                        "El SMax tambien se puede calcular como la VENTA maxima historica de un producto * 1.8",
                        "EXISTencia = EXISTencia de AG al momento de imprimir el reporte",
                        "ProdPrOC = Produccion en prOCeso",
                        "",
                        "ALMACEN de SURTIDO y mostrador trabajan en base a Smin debido a que resurten a diario",
                        "vgr. Se tienen que programar para mandar a hacer las CANTIDADes en negativo"
                    });
                    break;
                case Enumerados.TipoReporteExistenciasBase.SMIN:
                    notas.AddRange(new string[]
                    {
                        "NOTAS",
                        "Formula: MH = SMin - EXISTencia - ProdPrOC",
                        "Donde: MH = Mandar a hacer ",
                        "SMax = StOCk Maximo, este se calcula de la siguiente forma Smax = Smin * 1.5 (como estandar)",
                        "Smin = Stock RECOMENDADO",
                        "El SMax tambien se puede calcular como la VENTA maxima historica de un producto * 1.8",
                        "EXISTencia = EXISTencia de AG al momento de imprimir el reporte",
                        "ProdPrOC = Produccion en prOCeso",
                        "",
                        "ALMACEN de SURTIDO y mostrador trabajan en base a Smin debido a que resurten a diario",
                        "vgr. Se tienen que programar para mandar a hacer las CANTIDADes en negativo"
                    });
                    break;
                case Enumerados.TipoReporteExistenciasBase.SMINsinPP:
                    notas.AddRange(new string[]
                    {
                        "NOTAS:",
                        "Formula: MH = (EXIST + ProdPrOC) - SMIN * 1.53",
                        "Donde: MH = Mandar a hacer",
                        "SMax = Stock Maximo, este se calcula de la siguiente forma Smax = Smin * 1.5 (como estandar)",
                        "Smin = Stock RECOMENDADO",
                        "",
                        "EXISTencia = EXISTENCIA de AG al momento de imprimir el reporte",
                        "ProdPrOC = Produccion en prOCeso",
                        "",
                        "ALMACEN de SURTIDO y mostrador trabajan en base a Smin debido a que resurten a diario",
                        "vgr. Se tienen que programar para mandar a hacer las CANTIDADES en negativo"
                    });
                    break;
            }
            return notas;
        }

        public void GeneraArchivoExcelBase2(Enumerados.TipoReporteExistencias tipoReporte,Enumerados.TipoReporteExistenciasBase tipoReporteBase, string RutaYNombreArchivo)
        {

            //fragmento tomado de: GeneraArchivoExcelBase
            int CVE_ALMACEN = 0;
            switch (tipoReporte)
            {
                case Enumerados.TipoReporteExistencias.AlmGeneral:
                    CVE_ALMACEN = 1;
                    break;
                case Enumerados.TipoReporteExistencias.AlmSurtido:
                    CVE_ALMACEN = 3;
                    break;
                case Enumerados.TipoReporteExistencias.AlmMostrador:
                    CVE_ALMACEN = 4;
                    break;
            }
            ExistenciaBase = tipoReporteBase;

            /*
             * Obtención de información
             * */

            //Obtención del universo de información (Llama a base de Datos)
            DataTable DataTableExistencias = vw_ExistenciasBase.DevuelveExistencias(CVE_ALMACEN);
            //Obtención de solo de los primeros artículos por modelo (Se procesa del lado del cliente)
            DataTable dtPrimerosArticulos = DevuelvePrimerosArticulos(DataTableExistencias);
            //Obtención de Totales (Se procesa del lado del cliente)
            DataTable dtTotalesGenerales = DevuelveTotalesGenerales(dtPrimerosArticulos);


            #region Creación de la Hoja de Excel
            //Fragmento tomado de: GeneraArchivoExcelBase()

            //crea el objeto de Excel con compatibilidad para Excel 2003
            HSSFWorkbook xlsWorkBook = new HSSFWorkbook();

            //agrego la hoja
            ISheet sheet = xlsWorkBook.CreateSheet("Hoja1");

            //tipo de letra y tamaño de fuente
            ICellStyle defaultStyle = xlsWorkBook.CreateCellStyle();
            IFont defaultFont = xlsWorkBook.CreateFont();
            defaultFont.FontName = "Calibri";
            defaultFont.FontHeightInPoints = 11;
            defaultStyle.SetFont(defaultFont);

            ICellStyle estiloNegativos = xlsWorkBook.CreateCellStyle();
            estiloNegativos.FillBackgroundColor = IndexedColors.Yellow.Index;
            estiloNegativos.FillForegroundColor = IndexedColors.Yellow.Index;
            estiloNegativos.FillPattern = FillPattern.SolidForeground;

            for (int i = 0; i < 15; i++)
            {
                sheet.SetDefaultColumnStyle(i, defaultStyle);
            }
            


            #endregion
            //variable para acumular el total general de MH
            double TotalGeneralMH = 0;
            //renglón incial donde se empezará a escribir el reporte
            int iRenglon = 4;
            //renglones en blanco que se dejan entre grupo y grupo cuando se escriba la información
            int iRenglonesEnBlanco = 2;
            //se recorren los primeros artículos
            foreach (DataRow dataRowPrimerosArticulos in dtPrimerosArticulos.Rows)
            {
                // Se arma el título, ej: "10   CAGAMOSH   CAMISOLA GABARDINA MARINO TALLA 36 M/L" y se escribe
                string tituloDeGrupo = string.Format("{0} {1} {2}", dataRowPrimerosArticulos["CAMPOINTU"],dataRowPrimerosArticulos["ART_DESC"], dataRowPrimerosArticulos["DESCR"]);
                sheet.CreateRow(iRenglon).CreateCell(0).SetCellValue(tituloDeGrupo);
                iRenglon++;
                // Se arma el encabezado para el segmento que se va a escribir (se reusa método original RegresaYEscribeRenglonCabecerasColumnas())
                RegresaYEscribeRenglonCabecerasColumnas(sheet, iRenglon);

                //Se obtiene el detalle por Modelo para escribir el detalle del segmento actual a escribir en Excel
                var queryDatosModelo = DataTableExistencias.AsEnumerable().Where(c => (int)c["CAMPOINTU"] == (int)dataRowPrimerosArticulos["CAMPOINTU"] && (string)c["ART_DESC"] == (string)dataRowPrimerosArticulos["ART_DESC"]);

                //Se recorre el detalle de la consulta de este Modelo
                int i = 0;  //este contador es para sacar el Módulo y determinar de qué lado se escribe la información en el formato de Excel
                //con estas variables controlo de dónde a dónde se debera tomar el Rango que se pasa a la función "SUMAR.SI" para determinar la suma total de MH por c/Grupo
                int iniRangoIzq = 0;
                int finRangoIzq = 0;
                int iniRangoDer = 0;
                int finRangoDer = 0;                
                
                

                foreach (DataRow dataRowDetalleModelo in queryDatosModelo)
                {
                   
                    if (i % 2 == 0)
                    { 
                        //solo aumento el renglón aquí (se crea renglón) porque cuando se escribe del lado izquierdo el renglón ya existe (lo recupero y lo uso) pues se escribe en el miso renglón previamente creado
                        iRenglon++;
                        //reviso si el inicio del rango ya está asignado, si no entonces meto el valor de inicio de rango
                        if (iniRangoIzq == 0)
                            iniRangoIzq = iRenglon+1;

                        finRangoIzq = iRenglon+1;
                        //escribo del lado izquierdo del Excel
                        RegresaYEscribeRenglonDetalleColumnas2(sheet, iRenglon, dataRowDetalleModelo, tipoReporteBase, LadoAEscribir.Izquierda, estiloNegativos);
                    }
                    else
                    {
                        //escribo del lado Derecho del Excel

                        //reviso si el inicio del rango ya está asignado, si no entonces meto el valor de inicio de rango
                        if (iniRangoDer == 0)
                            iniRangoDer = iRenglon+1;

                        finRangoDer = iRenglon+1;
                        RegresaYEscribeRenglonDetalleColumnas2(sheet, iRenglon, dataRowDetalleModelo, tipoReporteBase, LadoAEscribir.Derecha, estiloNegativos);
                    }
                    i++;
                }
                iRenglon++;
                //escribo el total del grupo:
                EscribeRenglonTotalesGrupo2(xlsWorkBook, sheet, iRenglon, dataRowPrimerosArticulos, iniRangoIzq, finRangoIzq, iniRangoDer, finRangoDer, tipoReporteBase, ref TotalGeneralMH, estiloNegativos);
                iRenglon++;
                //se escriben los renglones en blanco
                iRenglon += iRenglonesEnBlanco;

            }
            //Se escriben los totales generales dependiendo de tipoReporteBase:
            if (tipoReporteBase == Enumerados.TipoReporteExistenciasBase.SMAX ||
                tipoReporteBase == Enumerados.TipoReporteExistenciasBase.SMIN)
            {
                Escribe(sheet, iRenglon, 0, "EXISTencia TOTAL");
                Escribe(sheet, iRenglon, 7, (double) dtTotalesGenerales.Rows[0]["TOTAL_EXISTENCIA"], false);
                iRenglon++;
                Escribe(sheet, iRenglon, 0, "PP TOTAL");
                Escribe(sheet, iRenglon, 7, (double) dtTotalesGenerales.Rows[0]["TOTAL_PP"], false);
                iRenglon++;
                Escribe(sheet, iRenglon, 0, "S Min TOTAL");
                Escribe(sheet, iRenglon, 7, (double) dtTotalesGenerales.Rows[0]["TOTAL_SMIN"], false);
                iRenglon++;
                Escribe(sheet, iRenglon, 0, "S Max TOTAL");
                Escribe(sheet, iRenglon, 7, (double) dtTotalesGenerales.Rows[0]["TOTAL_SMAX"], false);
                iRenglon++;
                Escribe(sheet, iRenglon, 0, "Pend x Surtir TOTAL");
                Escribe(sheet, iRenglon, 7, string.Empty, false);
                iRenglon += 2;
                Escribe(sheet, iRenglon, 0, "MH TOTAL 2");
                IRow renglonMHTotal = Escribe(sheet, iRenglon, 7, TotalGeneralMH, false);
                Utiles.ExcelNpoiUtil.AplicaEstiloCeldasParaNegativos(ref renglonMHTotal, estiloNegativos);
            }
            else
            {
                DataTable resumen1 = DevuelveExistenciasResumen(ExistenciaBase, CVE_ALMACEN, 1);

                Escribe(sheet, iRenglon, 1, "Recomendado");
                Escribe(sheet, iRenglon, 2, "Existencia",false);
                Escribe(sheet, iRenglon, 3, "Diferencia", false);
                iRenglon++;
                Escribe(sheet, iRenglon, 0, "Alm Gral");
                Escribe(sheet, iRenglon, 1, (double)resumen1.Rows[0]["RECOMENDADO_ALM_GEN"], false, Utiles.ExcelNpoiUtil.FormatoCelda(ref xlsWorkBook, Enumerados.FormatosNPOI.MILES));
                Escribe(sheet, iRenglon, 2, (double)resumen1.Rows[0]["TOTAL_ALM_GEN"],false,Utiles.ExcelNpoiUtil.FormatoCelda(ref xlsWorkBook,Enumerados.FormatosNPOI.MILES));                
                Escribe(sheet, iRenglon, 3, string.Format("C{0}-B{0}", iRenglon + 1), false, true, Utiles.ExcelNpoiUtil.FormatoCelda(ref xlsWorkBook, Enumerados.FormatosNPOI.MILES));

                iRenglon++;
                Escribe(sheet, iRenglon, 0, "Alm Surt");
                Escribe(sheet, iRenglon, 1, (double)resumen1.Rows[0]["RECOMENDADO_ALM_SURT"], false, Utiles.ExcelNpoiUtil.FormatoCelda(ref xlsWorkBook, Enumerados.FormatosNPOI.MILES));
                Escribe(sheet, iRenglon, 2, (double)resumen1.Rows[0]["TOTAL_ALM_SURT"], false, Utiles.ExcelNpoiUtil.FormatoCelda(ref xlsWorkBook, Enumerados.FormatosNPOI.MILES));                
                Escribe(sheet, iRenglon, 3, string.Format("C{0}-B{0}", iRenglon + 1), false, true, Utiles.ExcelNpoiUtil.FormatoCelda(ref xlsWorkBook, Enumerados.FormatosNPOI.MILES));

                iRenglon++;
                Escribe(sheet, iRenglon, 0, "Alm Most");
                Escribe(sheet, iRenglon, 1, (double)resumen1.Rows[0]["RECOMENDADO_ALM_MOST"], false, Utiles.ExcelNpoiUtil.FormatoCelda(ref xlsWorkBook, Enumerados.FormatosNPOI.MILES));
                Escribe(sheet, iRenglon, 2, (double)resumen1.Rows[0]["TOTAL_ALM_MOST"], false, Utiles.ExcelNpoiUtil.FormatoCelda(ref xlsWorkBook, Enumerados.FormatosNPOI.MILES));                
                Escribe(sheet, iRenglon, 3, string.Format("C{0}-B{0}", iRenglon + 1), false, true, Utiles.ExcelNpoiUtil.FormatoCelda(ref xlsWorkBook, Enumerados.FormatosNPOI.MILES));

                iRenglon++;
                Escribe(sheet, iRenglon, 1, string.Format("SUM(B{0}:B{1})", iRenglon - 2, iRenglon), true, true, Utiles.ExcelNpoiUtil.FormatoCelda(ref xlsWorkBook, Enumerados.FormatosNPOI.MILES));
                Escribe(sheet, iRenglon, 2, string.Format("SUM(C{0}:C{1})", iRenglon - 2, iRenglon), false, true, Utiles.ExcelNpoiUtil.FormatoCelda(ref xlsWorkBook, Enumerados.FormatosNPOI.MILES));
                Escribe(sheet, iRenglon, 3, string.Format("C{0}-B{1}", iRenglon + 1, iRenglon + 1), false, true, Utiles.ExcelNpoiUtil.FormatoCelda(ref xlsWorkBook, Enumerados.FormatosNPOI.MILES));
            }
            iRenglon+=2;

            // AGREGAMOS ENCABEZADOS PARA CADA LINEA
            Escribe(sheet, iRenglon, 6, "Recomendado");
            Escribe(sheet, iRenglon, 7, "Existencia",false);
            Escribe(sheet, iRenglon, 8, "Diferencia", false); 
            
            iRenglon+=2;

            DataTable resumen2 = DevuelveExistenciasResumen(ExistenciaBase, CVE_ALMACEN, 2);
            foreach (DataRow dataRow in resumen2.Rows)
            {
                Escribe(sheet, iRenglon, 0, dataRow["CAMPOSTRU1"].ToString());
                Escribe(sheet, iRenglon, 6, (double)dataRow["recomendado"], false);
                Escribe(sheet, iRenglon, 7, (double)dataRow["existencia"], false);
                Escribe(sheet, iRenglon, 8, (double)dataRow["existencia"] - (double)dataRow["recomendado"], false);
                iRenglon++;
            }
            //Notas finales            
            iRenglon+=2;            
            List<string> notas = DevuelveNotas(tipoReporteBase);
            foreach (string nota in notas)
            {
                IRow renComentario = sheet.CreateRow(iRenglon);
                renComentario.CreateCell(0).SetCellValue(nota);
                iRenglon++;
            }
            
            //Ajuste de anchos de columna
            //lado izquierdo
            for (int i = 1; i < 5; i++)
            {
                sheet.AutoSizeColumn(i);
                sheet.AutoSizeColumn(i+7);
            }
          
            sheet.SetColumnWidth(5, Utiles.ExcelNpoiUtil.AnchoColumna(40));//Col F
            sheet.SetColumnWidth(6,Utiles.ExcelNpoiUtil.AnchoColumna(60));//Col G
            sheet.SetColumnWidth(13, Utiles.ExcelNpoiUtil.AnchoColumna(40));//Col N
            sheet.SetColumnWidth(14,Utiles.ExcelNpoiUtil.AnchoColumna(60));//Col O


            //Si el reporte es "SMINsinPP" se ocultan las columnas: 4, 5, 12 y 13
            if (tipoReporteBase == Enumerados.TipoReporteExistenciasBase.SMINsinPP)
            {
                sheet.SetColumnHidden(4, true);
                sheet.SetColumnHidden(5, true);
                sheet.SetColumnHidden(12, true);
                sheet.SetColumnHidden(13, true);
            }
            sheet.FitToPage = false;
            sheet.PrintSetup.Scale = 90;
            //Escribe archivo
            if (File.Exists(RutaYNombreArchivo))
            {
                File.Delete(RutaYNombreArchivo);
            }
            FileStream fs = new FileStream(RutaYNombreArchivo, FileMode.CreateNew);

            xlsWorkBook.Write(fs);


            fs.Close();

        }
        public void GeneraArchivoExcelBase(Enumerados.TipoReporteExistencias tipoReporte, Enumerados.TipoReporteExistenciasBase tipoReporteBase, string RutaYNombreArchivo)
        {
            int CVE_ALMACEN = 0;
            switch (tipoReporte)
            {                   
                case Enumerados.TipoReporteExistencias.AlmGeneral:
                    CVE_ALMACEN = 1;
                    break;
                case Enumerados.TipoReporteExistencias.AlmSurtido:
                    CVE_ALMACEN = 3;
                    break;
                case Enumerados.TipoReporteExistencias.AlmMostrador:
                    CVE_ALMACEN = 4;
                    break;
            }
            ExistenciaBase = tipoReporteBase;
            
            DataTable DataTableExistencias = vw_ExistenciasBase.DevuelveExistencias(CVE_ALMACEN);




            //crea el objeto de Excel con compatibilidad para Excel 2003
                HSSFWorkbook xlsWorkBook = new HSSFWorkbook();

            //agrego la hoja
                ISheet sheet = xlsWorkBook.CreateSheet("Hoja1");

            if (tipoReporteBase == Enumerados.TipoReporteExistenciasBase.SMINsinPP)
            {
                Utiles.ExcelNpoiUtil.AsignaFormatoGeneral1(xlsWorkBook, sheet, 16, 60);
            }
            else
            {
                Utiles.ExcelNpoiUtil.AsignaFormatoGeneral1(xlsWorkBook, sheet, 16, 40);
            }

            ICellStyle estiloNegativos = xlsWorkBook.CreateCellStyle();
            estiloNegativos.FillBackgroundColor = IndexedColors.Yellow.Index;
            estiloNegativos.FillForegroundColor = IndexedColors.Yellow.Index;
            estiloNegativos.FillPattern = FillPattern.SolidForeground;

            //crea encabezado del reportes (aparece solo una vez en la parte superior
                IRow renglonCabezera = sheet.CreateRow(0);
                renglonCabezera.CreateCell(0).SetCellValue("Reporte de Existencias Matricial");
            //Declaración de variables que controlan el brinco entre renglón y renglón
                int grupoIni = 0;
                int grupoFin = 0;
            int renglonesEnBlanco = 2;
            int renglonActual = 0;
            int i = 0;
            string artDesc = "";
            string clvINTU = "";
            string clvINTUPrevio = "";
            bool nuevoGrupo = false;

            

            foreach (DataRow renglonExistencia in DataTableExistencias.Rows)
            {                
                //revisa variable de control para saber si escribe la cabecera de grupo o no
                nuevoGrupo = false;
                if (artDesc != (string)renglonExistencia["ART_DESC"])
                {
                    grupoFin = renglonActual + 1;
                    //asigna la variable de control
                    artDesc = (string)renglonExistencia["ART_DESC"];
                    //asigna totales de grupo
                    if (renglonActual!=0)
                    {
                        renglonActual++;
                        EscribeRenglonTotalesGrupo(sheet, renglonActual, grupoIni, grupoFin,tipoReporteBase, estiloNegativos);
                    }
                                            
                    //dejar 2 renglones vacíos (así viene el formato original)
                    renglonActual = renglonActual + renglonesEnBlanco;
                    //generar encabezado de grupo
                    SMAXEncabezadoGrupo cabeceraGrupo = RegresaEncabezadoGrupoSMAX(renglonExistencia);
                    //Escribe encabezado de grupo
                    renglonActual++;
                    IRow renglonCabezeraGrupo = sheet.CreateRow(renglonActual);
                    renglonCabezeraGrupo.CreateCell(0).SetCellValue(string.Format("{0} {1} {2}", cabeceraGrupo.CAMPOINTU, cabeceraGrupo.ART_DESC,cabeceraGrupo.MODELO_DESC));
                    //escribir columnas
                    renglonActual++;
                    IRow renglonCabecerasColumnas = RegresaYEscribeRenglonCabecerasColumnas(sheet, renglonActual);
                    nuevoGrupo = true;
                    grupoIni = renglonActual + 1;
                }
                
                //escribir detalle
                clvINTU = (string)renglonExistencia["CAMPOINTU"].ToString();
                //clvINTU = clvINTU.Substring(clvINTU.Length - 2, 2);
                //if ((int)renglonExistencia["CAMPOINTU"] == 1120)
                //    System.Diagnostics.Debugger.Break();
                    
                if ((clvINTUPrevio == clvINTU && artDesc == (string)renglonExistencia["ART_DESC"]) || clvINTUPrevio == string.Empty || nuevoGrupo)
                {
                    renglonActual++;
                    IRow renglonDetalleColumnas = RegresaYEscribeRenglonDetalleColumnas(sheet, renglonActual, renglonExistencia, estiloNegativos);
                }
                else
                {
                    //renglonActual++;
                    IRow renglonDetalleColumnas = RegresaYEscribeRenglonDetalleColumnas(sheet, renglonActual, renglonExistencia, estiloNegativos);
                    //renglonActual--;
                }
                
                
                clvINTUPrevio = clvINTU;
                //contador de control
                i++;
                 
            }
            renglonActual++;
            EscribeRenglonTotalesGrupo(sheet, renglonActual, grupoIni, renglonActual,tipoReporteBase, estiloNegativos);

            renglonActual = renglonActual + 3;
            //Escribe totales
            DataTable resumen1 = DevuelveExistenciasResumen(ExistenciaBase, CVE_ALMACEN, 1);
            if (ExistenciaBase!=Enumerados.TipoReporteExistenciasBase.SMINsinPP)
            {
                for (int j = 0; j < resumen1.Columns.Count; j++)
                {
                    IRow renglonTot1 = sheet.CreateRow(renglonActual);
                    renglonTot1.CreateCell(0).SetCellValue(resumen1.Columns[j].ColumnName);
                    renglonTot1.CreateCell(7).SetCellValue(resumen1.Rows[0][j].ToString());
                    if (resumen1.Rows[0][j].ToString() != "")
                    {
                        renglonTot1.CreateCell(7).SetCellValue(Convert.ToDouble(resumen1.Rows[0][j].ToString()));
                    }
                    else
                    {
                        renglonTot1.CreateCell(7).SetCellValue(resumen1.Rows[0][j].ToString());
                    }
                    Utiles.ExcelNpoiUtil.AplicaEstiloCeldasParaNegativos(ref renglonTot1, estiloNegativos);                    
                    renglonActual++;
                }                
            }
            else
            {
                IRow renglonTot1;
                renglonTot1 = sheet.CreateRow(renglonActual);

                renglonTot1.CreateCell(1).SetCellValue("Recomendado");
                renglonTot1.CreateCell(2).SetCellValue("Existencia");
                renglonTot1.CreateCell(3).SetCellValue("Diferencia");
                renglonActual++;
                renglonTot1 = sheet.CreateRow(renglonActual);
                renglonTot1.CreateCell(0).SetCellValue("Alm Gral");
                renglonTot1.CreateCell(1).SetCellValue(Convert.ToDouble(resumen1.Rows[0][1].ToString()));
                renglonTot1.CreateCell(2).SetCellValue(Convert.ToDouble(resumen1.Rows[0][0].ToString()));
                renglonTot1.CreateCell(3).SetCellFormula(string.Format("B{0}-C{0}", renglonActual+1));
                Utiles.ExcelNpoiUtil.AplicaEstiloCeldasParaNegativos(ref renglonTot1, estiloNegativos);
                renglonActual++;
                renglonTot1 = sheet.CreateRow(renglonActual);
                renglonTot1.CreateCell(0).SetCellValue("Alm Surt");
                renglonTot1.CreateCell(1).SetCellValue(Convert.ToDouble(resumen1.Rows[0][3].ToString()));
                renglonTot1.CreateCell(2).SetCellValue(Convert.ToDouble(resumen1.Rows[0][2].ToString()));
                renglonTot1.CreateCell(3).SetCellFormula(string.Format("B{0}-C{0}", renglonActual + 1));
                Utiles.ExcelNpoiUtil.AplicaEstiloCeldasParaNegativos(ref renglonTot1, estiloNegativos);
                renglonActual++;
                renglonTot1 = sheet.CreateRow(renglonActual);
                renglonTot1.CreateCell(0).SetCellValue("Alm Most");
                renglonTot1.CreateCell(1).SetCellValue(Convert.ToDouble(resumen1.Rows[0][5].ToString()));
                renglonTot1.CreateCell(2).SetCellValue(Convert.ToDouble(resumen1.Rows[0][4].ToString()));
                renglonTot1.CreateCell(3).SetCellFormula(string.Format("B{0}-C{0}", renglonActual + 1));
                Utiles.ExcelNpoiUtil.AplicaEstiloCeldasParaNegativos(ref renglonTot1, estiloNegativos);
                renglonActual++;

                renglonTot1 = sheet.CreateRow(renglonActual);
                renglonTot1.CreateCell(1).SetCellFormula(string.Format("SUM(B{0}:B{1})",(renglonActual-2).ToString(),(renglonActual).ToString()));
                Utiles.ExcelNpoiUtil.AplicaEstiloCeldasParaNegativos(ref renglonTot1, estiloNegativos);
                renglonTot1.CreateCell(2).SetCellFormula(string.Format("SUM(C{0}:C{1})", (renglonActual - 2).ToString(), (renglonActual).ToString()));
                Utiles.ExcelNpoiUtil.AplicaEstiloCeldasParaNegativos(ref renglonTot1, estiloNegativos);
                renglonTot1.CreateCell(3).SetCellFormula(string.Format("SUM(D{0}:D{1})", (renglonActual - 2).ToString(), (renglonActual).ToString()));
                Utiles.ExcelNpoiUtil.AplicaEstiloCeldasParaNegativos(ref renglonTot1, estiloNegativos);
                renglonActual++;
            }

            renglonActual++;

            DataTable resumen2 = DevuelveExistenciasResumen(ExistenciaBase, CVE_ALMACEN, 2);
            foreach (DataRow row in resumen2.Rows)
            {
                IRow renglonTot2 = sheet.CreateRow(renglonActual);
                renglonTot2.CreateCell(0).SetCellValue(row[0].ToString());
                if (row[1].ToString()!="")
                {
                    renglonTot2.CreateCell(7).SetCellValue(Convert.ToDouble(row[1].ToString()));    
                }
                else
                {
                    renglonTot2.CreateCell(7).SetCellValue(row[1].ToString());
                }

                Utiles.ExcelNpoiUtil.AplicaEstiloCeldasParaNegativos(ref renglonTot2, estiloNegativos);
                renglonActual++;
            }
            renglonActual = renglonActual + 2;

            string[] comentarios = { "NOTAS:", "Formula: MH = SMin - EXISTencia - ProdPrOC", "Donde: MH = Mandar a hacer ", 
                                       "SMax = StOCk Maximo, este se calcula de la siguiente forma Smax = Smin * 1.5 (como estandar)",
                                   "Smin = Maxima VENTA historica mensual de un producto por 1.2",
                                   "El SMax tambien se puede calcular como la VENTA maxima historica de un producto * 1.8",
                                   "EXISTencia = EXISTencia de AG al momento de imprimir el reporte","ProdPrOC = Produccion en prOCeso",
                                   "","ALMACEN de SURTIDO y mostrador trabajan en base a Smin debido a que resurten a diario",
                                   "vgr. Se tienen que programar para mandar a hacer las CANTIDADes en negativo"};

            for (int e = 0; e < comentarios.Length; e++)
            {
                IRow renComentario = sheet.CreateRow(renglonActual);
                renComentario.CreateCell(0).SetCellValue(comentarios.GetValue(e).ToString());
                renglonActual++;
            }
            if (tipoReporteBase == Enumerados.TipoReporteExistenciasBase.SMINsinPP)
            {
                sheet.SetColumnHidden(4, true);
                sheet.SetColumnHidden(5, true);
                sheet.SetColumnHidden(10, true);
                sheet.SetColumnHidden(11, true);
            }
            //Escribe archivo
            if (File.Exists(RutaYNombreArchivo))
            {
                File.Delete(RutaYNombreArchivo);
            }
            FileStream fs = new FileStream(RutaYNombreArchivo, FileMode.CreateNew);

            xlsWorkBook.Write(fs);


            fs.Close();
        }

        private IRow RegresaYEscribeRenglonDetalleColumnas2(ISheet sheet, int renglonActual, DataRow renglonExistencia, Enumerados.TipoReporteExistenciasBase ExistenciaBase, LadoAEscribir Lado, ICellStyle estiloNegativos)
        {
            /*
             * En este formato de de reporte de existencias, solicitado por ULP el 12-02-2014
             * El criterio que se ocupa para saber de qué lado va cada Talla es: "La primer talla a la izquierda y la siguiente a la derecha
             * y así sucesivamente hasta que se acaben las tallas" originalmente se hacía tal y como lo
             * explica el método: RegresaYEscribeRenglonDetalleColumnas
             * 
             * partes del código de esta Función se tomó originalmente de: RegresaYEscribeRenglonDetalleColumnas
             * algunas partes fueron modificadas o adicionadas
             * 
             * */

            int Col0 = 0, Col1 = 1, Col2 = 2, Col3 = 3, Col4 = 4, Col5 = 5, Col6 = 6, Col7 = 7;
            int brinco = 0;

            IRow renglonCabeceraColumnas = null;            

            if (Lado == LadoAEscribir.Derecha)
            {
                brinco = 8;
                renglonCabeceraColumnas = sheet.GetRow(renglonActual);
            }
            else
            {
                renglonCabeceraColumnas = sheet.CreateRow(renglonActual);
            }

            renglonCabeceraColumnas.CreateCell(Col0 + brinco).SetCellValue((string)renglonExistencia["ART_TALLA"]);         //talla
            renglonCabeceraColumnas.CreateCell(Col1 + brinco).SetCellValue((double)renglonExistencia["EXIST"]);                //existencia            
            renglonCabeceraColumnas.CreateCell(Col2 + brinco).SetCellValue((double)renglonExistencia["COMP_X_REC"]);           //PP
            renglonCabeceraColumnas.CreateCell(Col3 + brinco).SetCellValue((double)renglonExistencia["STOCK_MIN"]);            //S MIN
            renglonCabeceraColumnas.CreateCell(Col4 + brinco).SetCellValue((double)renglonExistencia["STOCK_MAX"]);            //S MAX
            renglonCabeceraColumnas.CreateCell(Col5 + brinco).SetCellValue("");

            ICell cell = renglonCabeceraColumnas.CreateCell(Col6 + brinco);
            double valor = 0;

            switch (ExistenciaBase)
            {
                case Enumerados.TipoReporteExistenciasBase.SMAX:
                    if (brinco == 0)
                    {
                        cell.SetCellFormula(string.Format("(E{0}-(C{0}+B{0})) * -1", renglonActual + 1));        
                        //MH                    
                    }
                    else
                    {
                        cell.SetCellFormula(string.Format("(M{0}-(J{0}+K{0})) * -1", renglonActual + 1));
                        //MH con brinco                    
                    }
                    valor = ((double)renglonExistencia["STOCK_MAX"] - ((double)renglonExistencia["COMP_X_REC"] + (double)renglonExistencia["EXIST"])) * -1;
                    
                    break;
                case Enumerados.TipoReporteExistenciasBase.SMIN:
                    
                    if (brinco == 0)
                    {
                        cell.SetCellFormula(string.Format("(D{0}-B{0}-C{0}) * -1", renglonActual + 1));
                        //MH                   
                    }
                    else
                    {
                        cell.SetCellFormula(string.Format("(L{0}-J{0}-K{0}) * -1", renglonActual + 1));
                        //MH + brinco                   
                    }
                    valor = ((double)renglonExistencia["STOCK_MIN"] - ((double)renglonExistencia["COMP_X_REC"] + (double)renglonExistencia["EXIST"])) * -1;
                    break;
                case Enumerados.TipoReporteExistenciasBase.SMINsinPP:
                    
                    if (brinco == 0)
                    {
                        cell.SetCellFormula(string.Format("(B{0}+C{0}-D{0}*1.53)", renglonActual + 1));
                        //MH                    
                    }
                    else
                    {
                        cell.SetCellFormula(string.Format("(J{0}+K{0}-L{0}*1.53)", renglonActual + 1));
                        //MH + brinco                    
                    }
                    valor = (double)renglonExistencia["EXIST"] + (double)renglonExistencia["COMP_X_REC"] - (double)renglonExistencia["STOCK_MIN"] * 1.53;
                    break;

            }
            renglonCabeceraColumnas.CreateCell(Col7 + brinco).SetCellValue("");           
            //(BLANCO)

            Utiles.ExcelNpoiUtil.AplicaEstiloCeldasParaNegativos(ref renglonCabeceraColumnas, estiloNegativos);

            return renglonCabeceraColumnas;
        }
        private IRow RegresaYEscribeRenglonDetalleColumnas(ISheet sheet, int renglonActual, DataRow renglonExistencia, ICellStyle estiloNegativos)
        {

            IRow renglonCabeceraColumnas = sheet.CreateRow(renglonActual);

            /*se revisa de qué lado va a escribir los valores
             * lo anterior depende de los 2 últimos caracteres del campo: CLV_ART
             * si los 2 últimos caracteres terminan en 02 entonces se escribe
             * del lado derecho, en caso contrario del lado izquierdo
             */
            int Col0 = 0, Col1 = 1, Col2 = 2, Col3 = 3, Col4 = 4, Col5 = 5, Col6 = 6, Col7 = 7;
            int brinco = 0;
            string cveArt = (string) renglonExistencia["CLV_ART"];
            if (cveArt.Substring(cveArt.Length - 2, 2) == "02")
            {
                brinco = 8;
            }


            renglonCabeceraColumnas.CreateCell(Col0 + brinco).SetCellValue((string)renglonExistencia["ART_TALLA"]);         //talla
            renglonCabeceraColumnas.CreateCell(Col1 + brinco).SetCellValue((double)renglonExistencia["EXIST"]);                //existencia            
            renglonCabeceraColumnas.CreateCell(Col2 + brinco).SetCellValue((double)renglonExistencia["COMP_X_REC"]);           //PP
            renglonCabeceraColumnas.CreateCell(Col3 + brinco).SetCellValue((double)renglonExistencia["STOCK_MIN"]);            //S MIN
            renglonCabeceraColumnas.CreateCell(Col4 + brinco).SetCellValue((double)renglonExistencia["STOCK_MAX"]);            //S MAX
            renglonCabeceraColumnas.CreateCell(Col5 + brinco).SetCellValue("");                                             //(BLANCO)

            
            ICell cell = renglonCabeceraColumnas.CreateCell(Col6 + brinco);
            double valor = 0;

            switch (ExistenciaBase)
            {
                case Enumerados.TipoReporteExistenciasBase.SMAX:
                    if (brinco == 0)
                    {
                        cell.SetCellFormula(string.Format("(E{0}-(C{0}+B{0})) * -1", renglonActual + 1));
                        //MH                    
                    }
                    else
                    {
                        cell.SetCellFormula(string.Format("(M{0}-(J{0}+K{0})) * -1", renglonActual + 1));
                        //MH con brinco                    
                    }
                    valor = ((double)renglonExistencia["STOCK_MAX"] - ((double)renglonExistencia["COMP_X_REC"] + (double)renglonExistencia["EXIST"])) * -1;
                    //renglonCabeceraColumnas.CreateCell(Col6 + brinco).SetCellFormula(string.Format("(E{0}-(C{0}+B{0})) * -1", renglonActual+1));//MH                    
                    break;
                case Enumerados.TipoReporteExistenciasBase.SMIN:
                    //renglonCabeceraColumnas.CreateCell(Col6 + brinco).SetCellFormula(string.Format("(D{0}-(C{0}+B{0})) * -1", renglonActual+1));//MH                   
                    if (brinco == 0)
                    {
                        cell.SetCellFormula(string.Format("(D{0}-B{0}-C{0}) * -1", renglonActual + 1));
                        //MH                   
                    }
                    else
                    {
                        cell.SetCellFormula(string.Format("(L{0}-J{0}-K{0}) * -1", renglonActual + 1));
                        //MH + brinco                   
                    }
                    valor = ((double)renglonExistencia["STOCK_MIN"] - ((double)renglonExistencia["COMP_X_REC"] + (double)renglonExistencia["EXIST"])) * -1;
                    break;
                case Enumerados.TipoReporteExistenciasBase.SMINsinPP:
                    //renglonCabeceraColumnas.CreateCell(Col6 + brinco).SetCellFormula(string.Format("(B{0}+C{0}-D{0}*1.53)", renglonActual + 1));//MH                    
                    if (brinco == 0)
                    {
                        cell.SetCellFormula(string.Format("(B{0}+C{0}-D{0}*1.53)", renglonActual + 1));
                        //MH                    
                    }
                    else
                    {
                        cell.SetCellFormula(string.Format("(J{0}+K{0}-L{0}*1.53)", renglonActual + 1));
                        //MH + brinco                    
                    }
                    valor = (double)renglonExistencia["EXIST"] + (double)renglonExistencia["COMP_X_REC"] - (double)renglonExistencia["STOCK_MIN"] * 1.53;
                    break;                           
            }
            
            renglonCabeceraColumnas.CreateCell(Col7 + brinco).SetCellValue("");                                             //(BLANCO)

            Utiles.ExcelNpoiUtil.AplicaEstiloCeldasParaNegativos(ref renglonCabeceraColumnas, estiloNegativos);

            return renglonCabeceraColumnas;
        }
        
        private IRow RegresaYEscribeRenglonCabecerasColumnas(ISheet sheet, int renglonActual)
        {

            IRow renglonCabeceraColumnas = sheet.CreateRow(renglonActual);

            renglonCabeceraColumnas.CreateCell(0).SetCellValue("Talla");
            renglonCabeceraColumnas.CreateCell(1).SetCellValue("EXIST");
            renglonCabeceraColumnas.CreateCell(2).SetCellValue("PP");
            renglonCabeceraColumnas.CreateCell(3).SetCellValue("S Min");
            renglonCabeceraColumnas.CreateCell(4).SetCellValue("S Max");
            renglonCabeceraColumnas.CreateCell(5).SetCellValue("");
            renglonCabeceraColumnas.CreateCell(6).SetCellValue("MH");
            renglonCabeceraColumnas.CreateCell(7).SetCellValue("");
            renglonCabeceraColumnas.CreateCell(8).SetCellValue("Talla");
            renglonCabeceraColumnas.CreateCell(9).SetCellValue("EXIST");
            renglonCabeceraColumnas.CreateCell(10).SetCellValue("PP");
            renglonCabeceraColumnas.CreateCell(11).SetCellValue("S Min");
            renglonCabeceraColumnas.CreateCell(12).SetCellValue("S Max");
            renglonCabeceraColumnas.CreateCell(13).SetCellValue("");
            renglonCabeceraColumnas.CreateCell(14).SetCellValue("MH");
            return renglonCabeceraColumnas;
        }

        private void EscribeRenglonTotalesGrupo2(IWorkbook xlsWorkBook, ISheet sheet, int renglonActual, DataRow DataRowTotales, int IniRangoIzquierdo,int FinRangoIzq,int IniRangoDerecho,int FinRangoDerecho,Enumerados.TipoReporteExistenciasBase tipoReporteBase,ref double TotalGeneralMH, ICellStyle estiloNegativos)
        {
            IRow renglonCabeceraColumnas = sheet.CreateRow(renglonActual);

            renglonCabeceraColumnas.CreateCell(0).SetCellValue("TOTAL");
            renglonCabeceraColumnas.CreateCell(1).SetCellValue((double)DataRowTotales["SUMOF_EXIST"]);
            renglonCabeceraColumnas.CreateCell(2).SetCellValue((double)DataRowTotales["SUMOF_PP"]);
            renglonCabeceraColumnas.CreateCell(3).SetCellValue((double)DataRowTotales["SUMOF_SMIN"]);
            renglonCabeceraColumnas.CreateCell(4).SetCellValue((double)DataRowTotales["SUMOF_SMAX"]);

            if (tipoReporteBase == Enumerados.TipoReporteExistenciasBase.SMAX)
            {
                renglonCabeceraColumnas.CreateCell(6).SetCellFormula(string.Format("(E{0}-B{0}-C{0}) * -1", renglonActual + 1));                
            }
            else if (tipoReporteBase == Enumerados.TipoReporteExistenciasBase.SMIN)
            {
                renglonCabeceraColumnas.CreateCell(6).SetCellFormula(string.Format("(D{0}-B{0}-C{0}) * -1", renglonActual + 1));
            }
            else if (tipoReporteBase == Enumerados.TipoReporteExistenciasBase.SMINsinPP)
            {
                renglonCabeceraColumnas.CreateCell(6).SetCellFormula(string.Format("(B{0}+C{0}-D{0} * 1.53)", renglonActual + 1));
            }

            ICell celdaTotalMH = renglonCabeceraColumnas.CreateCell(7);
            //CORRECCION. VERIFICAMOS SI TENEMOS LADO DERECHO
            if (IniRangoDerecho == 0)
                IniRangoDerecho = FinRangoDerecho = IniRangoIzquierdo;

            celdaTotalMH.SetCellFormula(string.Format("SUMIF(G{0}:G{1}, \"<0\") + SUMIF(O{2}:O{3}, \"<0\")", IniRangoIzquierdo, FinRangoIzq,IniRangoDerecho,FinRangoDerecho));
            CellValue totalMH = Utiles.ExcelNpoiUtil.EvaluaFormula(xlsWorkBook, sheet, celdaTotalMH);

            if (totalMH.NumberValue < 0)
            {
                //si el Total de MH es negativo lo agrego a lo sumo para obtener el total

                //TODO: sumar variable con el total general de MH
                TotalGeneralMH += totalMH.NumberValue;
            }
        

            Utiles.ExcelNpoiUtil.AplicaEstiloCeldasParaNegativos(ref renglonCabeceraColumnas, estiloNegativos);

        }
        private void EscribeRenglonTotalesGrupo(ISheet sheet, int renglonActual, int sumaIni, int sumaFin, Enumerados.TipoReporteExistenciasBase tipoReporteBase, ICellStyle estiloNegativos)
        {

            IRow renglonCabeceraColumnas = sheet.CreateRow(renglonActual);

            renglonCabeceraColumnas.CreateCell(0).SetCellValue("TOTAL");
            renglonCabeceraColumnas.CreateCell(1).SetCellFormula(string.Format("SUM(B{0}:B{1})", sumaIni.ToString(), sumaFin.ToString()));
            renglonCabeceraColumnas.CreateCell(2).SetCellFormula(string.Format("SUM(C{0}:C{1})", sumaIni.ToString(), sumaFin.ToString()));
            renglonCabeceraColumnas.CreateCell(3).SetCellFormula(string.Format("SUM(D{0}:D{1})", sumaIni.ToString(), sumaFin.ToString()));
            renglonCabeceraColumnas.CreateCell(4).SetCellFormula(string.Format("SUM(E{0}:E{1})", sumaIni.ToString(), sumaFin.ToString()));            
            
            if (tipoReporteBase == Enumerados.TipoReporteExistenciasBase.SMAX)
            {
                renglonCabeceraColumnas.CreateCell(6) .SetCellFormula(string.Format("(E{0}-B{0}-C{0}) * -1", renglonActual + 1));
            }
            else if (tipoReporteBase == Enumerados.TipoReporteExistenciasBase.SMIN)
            {
                renglonCabeceraColumnas.CreateCell(6).SetCellFormula(string.Format("(D{0}-B{0}-C{0}) * -1", renglonActual + 1));
            }
            else if (tipoReporteBase == Enumerados.TipoReporteExistenciasBase.SMINsinPP)
            {
                renglonCabeceraColumnas.CreateCell(6).SetCellFormula(string.Format("(B{0}+C{0}-D{0} * 1.53)", renglonActual + 1));
            }

            renglonCabeceraColumnas.CreateCell(7).SetCellFormula(string.Format("SUMIF(G{0}:G{1}, \"<0\")", sumaIni.ToString(), sumaFin.ToString()));

            renglonCabeceraColumnas.CreateCell(8).SetCellValue("TOTAL");
            renglonCabeceraColumnas.CreateCell(9).SetCellFormula(string.Format("SUM(J{0}:J{1})", sumaIni.ToString(), sumaFin.ToString()));
            renglonCabeceraColumnas.CreateCell(10).SetCellFormula(string.Format("SUM(K{0}:K{1})", sumaIni.ToString(), sumaFin.ToString()));
            renglonCabeceraColumnas.CreateCell(11).SetCellFormula(string.Format("SUM(L{0}:L{1})", sumaIni.ToString(), sumaFin.ToString()));
            renglonCabeceraColumnas.CreateCell(12).SetCellFormula(string.Format("SUM(M{0}:M{1})", sumaIni.ToString(), sumaFin.ToString()));


            if (tipoReporteBase == Enumerados.TipoReporteExistenciasBase.SMAX)
            {
                renglonCabeceraColumnas.CreateCell(14).SetCellFormula(string.Format("(M{0}-J{0}-K{0}) * -1", renglonActual + 1));
            }
            else if (tipoReporteBase == Enumerados.TipoReporteExistenciasBase.SMIN)
            {
                renglonCabeceraColumnas.CreateCell(14).SetCellFormula(string.Format("(L{0}-J{0}-K{0}) * -1", renglonActual + 1));
            }
            else if (tipoReporteBase == Enumerados.TipoReporteExistenciasBase.SMINsinPP)
            {
                renglonCabeceraColumnas.CreateCell(14).SetCellFormula(string.Format("(J{0}+k{0}-L{0} * 1.53)", renglonActual + 1));
            }

            //renglonCabeceraColumnas.CreateCell(15).SetCellFormula(string.Format("SUMIF(P{0}:P{1}, \"<0\")", sumaIni.ToString(), sumaFin.ToString()));
            renglonCabeceraColumnas.CreateCell(15).SetCellFormula(string.Format("SUMIF(O{0}:O{1}, \"<0\")", sumaIni.ToString(), sumaFin.ToString()));

            Utiles.ExcelNpoiUtil.AplicaEstiloCeldasParaNegativos(ref renglonCabeceraColumnas, estiloNegativos);            
            //return renglonCabeceraColumnas;
        }

        private static SMAXEncabezadoGrupo RegresaEncabezadoGrupoSMAX(DataRow Renglon)
        {
            return new SMAXEncabezadoGrupo() { CAMPOINTU = (int)Renglon["CAMPOINTU"], ART_DESC = (string)Renglon["ART_DESC"], MODELO_DESC = (string)Renglon["DESCR"] };
        }

        public static DataTable DevuelveExistencias(int CVE_ALMACEN)
        {
            DataTable dataTableExistencias = new DataTable();

            using (var DbContext = new SIPReportesContext())
            {
                var consulta = from e in DbContext.vw_ExistenciasBase where e.ALMACEN == CVE_ALMACEN orderby e.CAMPOINTU,e.ART_DESC, e.ART_TALLA select e;
                dataTableExistencias = Linq2DataTable.CopyToDataTable(consulta);
            }

            return dataTableExistencias;
        }        
        public static DataTable DevuelveExistenciasBaseSMin(int CVE_ALMACEN)
        {
            DataTable datos = new DataTable();
            using (var dbContext=new SIPReportesContext())
            {

                sm_dl.SqlServer.SqlServerCommand select = new sm_dl.SqlServer.SqlServerCommand();
                select.Connection = sm_dl.DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);                
                select.ObjectName = "usp_RepExistBaseMin";
                select.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CVE_ALM", CVE_ALMACEN));
                datos = select.GetDataTable();
            }
            return datos;
        }
        public DataTable DevuelveExistenciasResumen(Enumerados.TipoReporteExistenciasBase tipoExist, int CVE_ALMACEN,int Resumen)
        {
            DataTable datos = new DataTable();
            using (var dbContext = new SIPReportesContext())
            {
                int tipo = 0;
                sm_dl.SqlServer.SqlServerCommand select = new sm_dl.SqlServer.SqlServerCommand();
                select.Connection = sm_dl.DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                select.ObjectName = "usp_RepExistResumen";

                switch (tipoExist)
                {
                    case Enumerados.TipoReporteExistenciasBase.SMAX:
                        tipo = 1;
                        break;
                    case Enumerados.TipoReporteExistenciasBase.SMIN:
                        tipo = 2;
                        break;
                    case Enumerados.TipoReporteExistenciasBase.SMINsinPP:
                        tipo = 3;
                        break;
                }

                select.Parameters.Add(new System.Data.SqlClient.SqlParameter("@tipo", tipo));
                select.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CVE_ALM", CVE_ALMACEN));
                select.Parameters.Add(new System.Data.SqlClient.SqlParameter("@resumen", Resumen));
                datos = select.GetDataTable();
            }
            return datos;
        }
    }
}
