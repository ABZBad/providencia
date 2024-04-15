using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using sm_dl;
using sm_dl.SqlServer;
using ulp_dl;
//using ulp_dl.aspel_prod30;
using ulp_dl.aspel_sae80;
using ulp_dl.SIPNegocio;

namespace ulp_bl
{
    public enum DireccionBusqueda
    {
        Siguiente,
        Anterior
    }
    public class SimuladorCostos
    {
        public double CostoPrimo { get; set; }
        public double Integracion { set; get; }
        public double Margen { set; get; }
        public string Modelo { get; set; }
        public string ModeloDescripcion { get; set; }
        public DataTable ModeloEstructura { get; set; }
        public double PorcentajeGastosOperacion { set; get; }
        public double PorcentajeUtilidad { set; get; }
        public double PorcentajeAAplicar { set; get; }
        public double PorcentajeIncremento { set; get; }
        public double PrecioFinal { set; get; }        
        public double PrecioAnterior { set; get; }
        public double SubT1 { get; set; }
        public double SubT2 { get; set; }

        /// <summary>
        /// Regresa la siguiente talla hacia arriba/abajo
        /// </summary>
        /// <param name="modelo">Modelo con todo y Talla EJ: "PAGAMOSH3032"</param>
        /// <param name="direccion">Dependiendo de la dirección regresa la talla siguiente o anterior</param>
        /// <returns></returns>
        public static string RegresaSiguienteTalla(string modelo,List<string> listaModelos,DireccionBusqueda busqueda)
        {

            if (busqueda == DireccionBusqueda.Siguiente)
            {
                string talla = "";
                for (int i = 0; i < listaModelos.Count; i++)
                {

                    if (listaModelos[i] == modelo)
                    {
                        if (i + 1 <= listaModelos.Count - 1)
                        {
                            talla = listaModelos[i + 1];
                        }
                        else
                        {
                            talla = "";
                        }
                        break;
                    }
                }
                return talla;
            }
            else
            {
                string talla = "";
                for (int i = listaModelos.Count -1; i >= 0; i--)
                {

                    if (listaModelos[i] == modelo)
                    {
                        if (i - 1 >= 0)
                        {
                            talla = listaModelos[i - 1];
                        }
                        else
                        {
                            talla = "";
                        }
                        break;
                    }
                }
                return talla;
            }





        }
        /// <summary>
        /// Regresa un diccionario con todas las tallas
        /// según el modelo dado.
        /// </summary>
        /// <param name="modelo">Modelo (solo los primeros 8 caracteres) EJ: "PAGAMOSH"</param>
        /// <returns>Lista con con el total de las tallas según el modelo dado</returns>
        public static List<string> RegresaTallas(string modelo)
        {
            List<string> lstResult = new List<string>();

            using (var dbContext = new AspelSae80Context())
            {
                var query = from t in dbContext.INVE01 where t.CVE_ART.Substring(0, 8) == modelo select new { t.CVE_ART };
                foreach (var talla in query)
                {
                    lstResult.Add(talla.CVE_ART);
                }
            }
            return lstResult;
        }
        public static Exception ActualizaPrecio(string CveComponente, double Precio, int NUM_REG = 0)
        {
            Exception ex = null;

            try
            {
                using (var dbContext = new AspelSae80Context())
                {
                    //NO IMPORTANDO EL COMPONENTE, ACTUALIZAMOS LAS TABLAS DE PRODUCTOS Y CONFECCIONES
                    //1. PRECIO_X_PROD01

                    var query = from precio in dbContext.PRECIO_X_PROD01 where precio.CVE_ART == CveComponente && precio.CVE_PRECIO == 2 select precio;
                        foreach (PRECIO_X_PROD01 precioXProd01 in query)
                        {
                            precioXProd01.PRECIO = Precio;
                            precioXProd01.VERSION_SINC = DateTime.Now;
                        }
                        dbContext.SaveChanges();

                    //2. PRECIO_X_CONF01
                    var query2 = from precio in dbContext.PRECIO_X_CONF01 where precio.CVE_ART == CveComponente && precio.CVE_PRECIO == 2 select precio;
                    if (query2.Any())
                    {
                        foreach (PRECIO_X_CONF01 precioXProd01 in query2)
                        {
                            precioXProd01.PRECIO = Precio;
                            precioXProd01.VERSION_SINC = DateTime.Now;
                        }
                    }
                    else
                    {
                        dbContext.PRECIO_X_CONF01.Add(new PRECIO_X_CONF01() { CVE_ART = CveComponente, CVE_PRECIO = 2, PRECIO = Precio, UUID = Guid.NewGuid().ToString().ToUpper(), VERSION_SINC = DateTime.Now });
                    }
                    dbContext.SaveChanges();
                    
                }

            }
            catch (Exception Ex)
            {
                ex = Ex;
            }


            //try
            //{
            //    switch ((CveComponente.Contains("CONF") || CveComponente.Contains("SUAVIZADO")).ToString().ToUpper())
            //    {
            //        case "TRUE": //COMPONENTES DE TIPO 50
            //            using (var dbContext = new AspelSae80Context())
            //            {
            //                //YA NO SE UTILIZA LA TABLA DE PT_DET01, SE SUSTITUYE POR PRECIO_X_CONF01
            //                //var query = from precio in dbContext.PT_DET01 where precio.COMPONENTE == CveComponente && precio.NUM_REG == NUM_REG select precio;
            //                //foreach (ulp_dl.aspel_prod30.PT_DET01 PTDET01 in query)
            //                //{
            //                //    PTDET01.COSTOU = Precio;
            //                //}
            //                //dbContext.SaveChanges();
            //                var query = from precio in dbContext.PRECIO_X_CONF01 where precio.CVE_ART == CveComponente && precio.CVE_PRECIO == 2 select precio;
            //                if (query.ToArray().Length > 0)
            //                {
            //                    foreach (PRECIO_X_CONF01 precioXProd01 in query)
            //                    {
            //                        precioXProd01.PRECIO = Precio;
            //                    }
            //                    dbContext.SaveChanges();
            //                }
            //                else
            //                {
            //                    //INSERTAMOS LOS VALORES EN LA TABLA
                                
            //                }

            //            }
            //            break;
            //        case "FALSE": //COMPONENTES DE TIPO 49
            //            using (var dbContext = new AspelSae80Context())
            //            {
            //                var query = from precio in dbContext.PRECIO_X_PROD01 where precio.CVE_ART == CveComponente && precio.CVE_PRECIO == 2 select precio;
            //                foreach (PRECIO_X_PROD01 precioXProd01 in query)
            //                {
            //                    precioXProd01.PRECIO = Precio;
            //                }
            //                dbContext.SaveChanges();
            //            }
            //            break;
            //    }
            //}
            //catch (Exception Ex)
            //{
            //    ex = Ex;
            //}
            return ex;
        }
        public static Exception LogitudValida(string Modelo)
        {
            Exception Ex = null;
            if (Modelo.Length != 12)
            {
                Ex = new Exception("La longitud no corresponde al mínimo de un producto.");
            }
            return Ex;
        }
        public static DataTable ModeloExistente(string Modelo,ref Exception Ex)
        {
            DataTable dataTableModelo = new DataTable();
            try
            {
                using (var dbContext = new AspelSae80Context())
                {
                    var query = from modelo in dbContext.INVE01
                        join precio_x_prod in dbContext.PRECIO_X_PROD01
                                on modelo.CVE_ART equals precio_x_prod.CVE_ART
                        into modelo_precio
                        from submodelo in modelo_precio.DefaultIfEmpty()
                        where modelo.CVE_ART == Modelo
                        select new {modelo.CVE_ART,modelo.DESCR,submodelo.PRECIO };
                    if (query.Any())
                    {
                        dataTableModelo = Linq2DataTable.CopyToDataTable(query);
                    }
                    else
                    {
                        Ex = new Exception("El código es erróneo, o no ha sdo posible localizarlo en el catálogo. Inténtelo nuevamente.");                        
                    }
                }
            }
            catch (Exception ex)
            {
                Ex = ex;
            }
            return dataTableModelo;
        }
        public static DataTable RegresaEstructuraModelo(string Modelo,Enumerados.TipoSimulador TipoSimulador,ref Exception Ex)
        {
            DataTable dataTableEstructura = RegresaEstructuraTablaSimulador();
            try
            {
                string conStr = "";
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "usp_BuscaValoresSimuladorCostos";
                cmd.Parameters.Add(new SqlParameter("@modelo",Modelo));
                DataTable dataTableDatosComponentes = cmd.GetDataTable();
                int iCont = 0;
                if (dataTableDatosComponentes.Rows.Count > 0)
                {
                    
                    foreach (DataRow row in dataTableDatosComponentes.Rows)
                    {
                        iCont++;
                        DataRow nuevoRenglon = dataTableEstructura.NewRow();

                        nuevoRenglon["CNumero"] = iCont;
                        nuevoRenglon["CCod"] = row["COMPONENTE"];

                        if (row["DESCR"].ToString().Trim() != string.Empty)
                        {
                            nuevoRenglon["CDesc"] = row["DESCR"];
                        }
                        else
                        {
                            nuevoRenglon["CDesc"] = row["COMPONENTE"];
                        }
                        
                        nuevoRenglon["CCant"] = row["CANTIDAD"];

                        if (Convert.ToInt32(row["TIPOCOMP"]) == 1)
                        {
                            //SE MODIFICA A PETICION DEL AREA PARA QUE UNICAMENTE TOME ULTIMO COSTO
                            /*
                            if (TipoSimulador == Enumerados.TipoSimulador.SimuladorDeCostos)
                            {
                                nuevoRenglon["CPreci"] = row["PRECIO2"];
                            }
                            else
                            {
                                nuevoRenglon["CPreci"] = row["ULT_COSTO"];
                            }
                             * */
                            nuevoRenglon["CPreci"] = row["ULT_COSTO"];
                            nuevoRenglon["CPreci_Simulado"] = row["PRECIO2"];
                        }
                        else
                        {
                            nuevoRenglon["CPreci"] = row["COSTOU"];
                            nuevoRenglon["CPreci_Simulado"] = row["PRECIO2"];
                        }

                        
                        nuevoRenglon["CCod"] = row["COMPONENTE"];
                        if (TipoSimulador== Enumerados.TipoSimulador.SimuladorDeCostos)
                            nuevoRenglon["CSubt"] = Math.Round(Convert.ToDouble(nuevoRenglon["CCant"]) * Convert.ToDouble(nuevoRenglon["CPreci_Simulado"]),2);
                        else if (TipoSimulador == Enumerados.TipoSimulador.EstructuraDeProducto)
                            nuevoRenglon["CSubt"] = Math.Round(Convert.ToDouble(nuevoRenglon["CCant"]) * Convert.ToDouble(nuevoRenglon["CPreci"]), 2);

                        nuevoRenglon["CReg"] = row["NUM_REG"];
                        nuevoRenglon["CFecha"] = row["FECHA_SINCRONIZACION"];
                        dataTableEstructura.Rows.Add(nuevoRenglon);

                    }
                }
                else
                {
                    Ex = new Exception(string.Format("El producto no tiene una estructura: {0}",Modelo));
                }

            }
            catch (Exception ex)
            {
                Ex = ex;
            }
            return dataTableEstructura;
        }
        private static DataTable RegresaEstructuraTablaSimulador()
        {
            DataTable dataTableEstructura = new DataTable();

            dataTableEstructura.Columns.Add(new DataColumn("CNumero",typeof(int)));
            dataTableEstructura.Columns.Add(new DataColumn("CCod", typeof(string)));
            dataTableEstructura.Columns.Add(new DataColumn("CCambiaComp", typeof(object)));
            dataTableEstructura.Columns.Add(new DataColumn("CDesc", typeof(string)));
            dataTableEstructura.Columns.Add(new DataColumn("CCant", typeof(double)));
            dataTableEstructura.Columns.Add(new DataColumn("CPreci", typeof(double)));
            dataTableEstructura.Columns.Add(new DataColumn("CPreci_Simulado", typeof(double)));
            dataTableEstructura.Columns.Add(new DataColumn("CActuali", typeof(object)));
            dataTableEstructura.Columns.Add(new DataColumn("CSubt", typeof(double)));
            dataTableEstructura.Columns.Add(new DataColumn("CReg", typeof(int)));
            dataTableEstructura.Columns.Add(new DataColumn("CFecha", typeof(DateTime)));
            return dataTableEstructura;

        }
        public static void GeneraArchivoExcel(SimuladorCostos SimuladorCostos, string RutaYNombreArchivo)
        {
            int renglonDetalle = 6;
            HSSFWorkbook xlsWorkBook = new HSSFWorkbook();
            ISheet sheet = xlsWorkBook.CreateSheet("Hoja1");

            #region Se crea el título del de la hora de excel
            IRow renglonTitulo = sheet.CreateRow(0);            //creación del renglón donde va el Título
            renglonTitulo.CreateCell(2).SetCellValue(string.Format("{0},{1} de {2} de {3}", DateTime.Now.ToString("dddd"), DateTime.Now.ToString("dd"), DateTime.Now.ToString("MMMM"), DateTime.Now.ToString("yyyy")));
            #endregion
            #region Encabezados: (modelo, descripción dmoelo)
            IRow renglonEncabezados = sheet.CreateRow(2);

            renglonEncabezados.CreateCell(0).SetCellValue("MODELO"); //ENCABEZADOS (modelo, descripción dmoelo)
            renglonEncabezados.CreateCell(1).SetCellValue(SimuladorCostos.Modelo);
            renglonEncabezados.CreateCell(2).SetCellValue(SimuladorCostos.ModeloDescripcion);

            IRow renglonEncabezadosEstrcutrua = sheet.CreateRow(5);

            renglonEncabezadosEstrcutrua.CreateCell(1).SetCellValue("CODIGO");
            renglonEncabezadosEstrcutrua.CreateCell(2).SetCellValue("DESCRIPCIÓN");
            renglonEncabezadosEstrcutrua.CreateCell(3).SetCellValue("CANTIDAD");
            renglonEncabezadosEstrcutrua.CreateCell(4).SetCellValue("PRECIO");
            renglonEncabezadosEstrcutrua.CreateCell(5).SetCellValue("SUBTOTAL");

            #endregion
            #region Detalle de la estructura del modelo
            
            foreach (DataRow dataRow in SimuladorCostos.ModeloEstructura.Rows)
            {
                IRow renglonDetalleEstructura = sheet.CreateRow(renglonDetalle);
                renglonDetalleEstructura.CreateCell(0).SetCellValue(renglonDetalle - 5); //contador de renglones
                renglonDetalleEstructura.CreateCell(1).SetCellValue(dataRow["CCod"].ToString()); //código
                renglonDetalleEstructura.CreateCell(2).SetCellValue(dataRow["CDesc"].ToString()); //descripción
                renglonDetalleEstructura.CreateCell(3).SetCellValue(Convert.ToDouble(dataRow["CCant"].ToString())); //cantidad
                renglonDetalleEstructura.CreateCell(4).SetCellValue(Convert.ToDouble(dataRow["CPreci_Simulado"].ToString())); //precio
                renglonDetalleEstructura.CreateCell(5).SetCellValue(Convert.ToDouble(dataRow["CSubt"].ToString())); //subtotal

                renglonDetalle++;
            }
            #endregion
            #region Porcentajes y valores
            renglonDetalle++;
            //se escribe Costo primo
            IRow renglonCostoPrimo = sheet.CreateRow(renglonDetalle);
            renglonCostoPrimo.CreateCell(4).SetCellValue("COSTO Primo");
            renglonCostoPrimo.CreateCell(5).SetCellValue(SimuladorCostos.CostoPrimo);

            //gastos de operación y SubT1
            renglonDetalle++;
            IRow renglonGastosOperacion = sheet.CreateRow(renglonDetalle);
            renglonGastosOperacion.CreateCell(2).SetCellValue("Gto operación");
            renglonGastosOperacion.CreateCell(3).SetCellValue(SimuladorCostos.PorcentajeGastosOperacion);
            renglonGastosOperacion.CreateCell(5).SetCellValue(SimuladorCostos.SubT1);

            //utilidad y Subt2
            renglonDetalle++;
            IRow renglonUtilidad = sheet.CreateRow(renglonDetalle);
            renglonUtilidad.CreateCell(2).SetCellValue("Utilidad");
            renglonUtilidad.CreateCell(3).SetCellValue(SimuladorCostos.PorcentajeUtilidad);
            renglonUtilidad.CreateCell(5).SetCellValue(SimuladorCostos.SubT2);

            //% a aplicar e Integración
            renglonDetalle++;
            IRow renglonPorcentajeAAplicar = sheet.CreateRow(renglonDetalle);
            renglonPorcentajeAAplicar.CreateCell(2).SetCellValue("% por aplicar");
            renglonPorcentajeAAplicar.CreateCell(3).SetCellValue(SimuladorCostos.PorcentajeAAplicar);
            renglonPorcentajeAAplicar.CreateCell(4).SetCellValue("Integración");
            renglonPorcentajeAAplicar.CreateCell(5).SetCellValue(SimuladorCostos.Integracion);

            //Margen y Precio Final
            renglonDetalle++;
            IRow renglonMargen = sheet.CreateRow(renglonDetalle);
            renglonMargen.CreateCell(2).SetCellValue("Margen");
            renglonMargen.CreateCell(3).SetCellValue(SimuladorCostos.Margen);

            /*Se crea estilo en negrillas y se aumenta el tamaño de la fuente
             * para la etiqueta: "P. Final"
             */

            ICellStyle estiloPrecioFinal = xlsWorkBook.CreateCellStyle();
            IFont fuentePrecioFinal = xlsWorkBook.CreateFont();
            fuentePrecioFinal.FontHeightInPoints = 14;
            fuentePrecioFinal.FontName = "Calibri";
            fuentePrecioFinal.Boldweight = (short)FontBoldWeight.Bold;
            estiloPrecioFinal.SetFont(fuentePrecioFinal);

            ICell celdaEtiquetaPFinal = renglonMargen.CreateCell(4);
            celdaEtiquetaPFinal.SetCellValue("P. Final");
            celdaEtiquetaPFinal.CellStyle = estiloPrecioFinal;
            ICell celdaValorPFinal = renglonMargen.CreateCell(5);
            celdaValorPFinal.SetCellValue(SimuladorCostos.PrecioFinal);
            celdaValorPFinal.CellStyle = estiloPrecioFinal;

            //Precio anterior y % de incremento
            renglonDetalle++;
            IRow renglonPrecioAnterior = sheet.CreateRow(renglonDetalle);
            renglonPrecioAnterior.CreateCell(2).SetCellValue("PRECIO Anterior");
            renglonPrecioAnterior.CreateCell(3).SetCellValue(SimuladorCostos.PrecioAnterior);
            renglonPrecioAnterior.CreateCell(4).SetCellValue("% Incremento");
            renglonPrecioAnterior.CreateCell(5).SetCellValue(SimuladorCostos.PorcentajeIncremento);

            #endregion
            #region Ajuste de los anchos de columna
            //Ajuste de los anchos de columna
            for (int i = 0; i < 6; i++)
            {
                sheet.AutoSizeColumn(i);
            }
            #endregion

            if (File.Exists(RutaYNombreArchivo))
            {
                File.Delete(RutaYNombreArchivo);
            }

            FileStream fs = new FileStream(RutaYNombreArchivo, FileMode.CreateNew);

            xlsWorkBook.Write(fs);


            fs.Close();


        }
    }
}
