using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ulp_dl.SIPReportes;
using ulp_bl;
using sm_dl.SqlServer;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using System.Data.SqlClient;
using ulp_bl.Utiles;
using NPOI.SS.Util;

namespace SIP
{
    public class RepOrdProd
    {
        public DataTable RegresaTabla(string referenciaInicial, string referenciaFinal, Enumerados.TipoOrdenProduccion tipo)
        {
            System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
            DataTable dataTablePedidos = new DataTable();
            using (var DbContext = new SIPReportesContext())
            {
                SqlServerCommand _cmd = new SqlServerCommand();
                _cmd.Connection = sm_dl.DALUtil.GetConnection(DbContext.Database.Connection.ConnectionString);
                _cmd.ObjectName = "usp_RepOrdProd";
                _cmd.Parameters.Add(new SqlParameter("@referenciaInicial", referenciaInicial));
                _cmd.Parameters.Add(new SqlParameter("@referenciaFinal", referenciaFinal));
                _cmd.Parameters.Add(new SqlParameter("@tipo", tipo));
                dataTablePedidos = _cmd.GetDataTable();
                _cmd.Connection.Close();

            }
            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.ElapsedMilliseconds);
            return dataTablePedidos;
        }

        public static void GeneraArchivoExcel(string RutaYNombreArchivo, DataTable Tabla, string referenciaFinal)
        {
            HSSFWorkbook xlsWorkBook = new HSSFWorkbook();

            ISheet sheet = xlsWorkBook.CreateSheet("Hoja1");

            #region ENCABEZADOS

            IRow renglonEncabezado = sheet.CreateRow(0);

            renglonEncabezado.CreateCell(1).SetCellValue("IMP OTs de Producción x MODELO");

            IRow renglonEncabezado2 = sheet.CreateRow(1);

            ICellStyle alineacionDerecha = xlsWorkBook.CreateCellStyle();

            alineacionDerecha.Alignment = HorizontalAlignment.Right;

            ICell celdaFecha = renglonEncabezado2.CreateCell(1);

            celdaFecha.CellStyle = alineacionDerecha;

            celdaFecha.SetCellValue(DateTime.Now.ToLongDateString());

            IRow renglonColumnName = sheet.CreateRow(2);
            renglonColumnName.CreateCell(0).SetCellValue("PTE.");
            renglonColumnName.CreateCell(1).SetCellValue("DESCRIPCION");
            renglonColumnName.CreateCell(2).SetCellValue("ORDEN");
            renglonColumnName.CreateCell(3).SetCellValue("ORDEN MAQUILA");
            renglonColumnName.CreateCell(4).SetCellValue("TALLER");
            renglonColumnName.CreateCell(5).SetCellValue("F. ALTA");


            #endregion


            #region Desc

            // AQUI COMIENZA EL DETALLE DEL REPORTE


            /*
            //Estilos de formato
            ICellStyle celdaEstiloSUM = xlsWorkBook.CreateCellStyle();
            celdaEstiloSUM = xlsWorkBook.CreateCellStyle();
            celdaEstiloSUM.DataFormat = HSSFDataFormat.GetBuiltinFormat("#,##0.00_);(#,##0.00)");


            IDataFormat dFormat2 = xlsWorkBook.CreateDataFormat();
            IDataFormat dFormat4 = xlsWorkBook.CreateDataFormat();

            short dosDig = dFormat2.GetFormat("0.00%");
            short cuatroDig = dFormat4.GetFormat("0.0000%");

            ICellStyle celdaEstiloPorcent2Dig = xlsWorkBook.CreateCellStyle();
            celdaEstiloPorcent2Dig = xlsWorkBook.CreateCellStyle();
            celdaEstiloPorcent2Dig.DataFormat = dosDig;

            ICellStyle celdaEstiloPorcent4Dig = xlsWorkBook.CreateCellStyle();
            celdaEstiloPorcent4Dig = xlsWorkBook.CreateCellStyle();
            celdaEstiloPorcent4Dig.DataFormat = cuatroDig;
            */

            Int32 SubTotal = 0;
            Int32 Total = 0;
            int renglonIndex = 2; //basado en índice 0
            string referencia = Tabla.Rows[0]["REFERENCIA"].ToString();
            string ordenMaquila = Tabla.Rows[0]["ORDENMAQUILA"].ToString();
            string taller = Tabla.Rows[0]["TALLER"].ToString();
            string Modelos = "";
            string descripcion = Tabla.Rows[0]["DESCRIPCION"].ToString();
            string observacion = Tabla.Rows[0]["OBS"].ToString();
            String fecha = Tabla.Rows[0]["FCAPTURA"] == null ? "" : DateTime.Parse(Tabla.Rows[0]["FCAPTURA"].ToString()).ToString("dd/MM/yyyy");
            foreach (DataRow renglon in Tabla.Rows)
            {
                if (referencia == renglon["REFERENCIA"].ToString())
                {
                    Modelos += ObtenerParteDerecha(renglon["PRODUCTO"].ToString(), 4) + "/" + renglon["CANTIDAD"].ToString() + " ";
                    SubTotal += Convert.ToInt32(renglon["CANTIDAD"].ToString());
                    fecha = renglon["FCAPTURA"] == null ? "" : DateTime.Parse(renglon["FCAPTURA"].ToString()).ToString("dd/MM/yyyy");
                }

                if (referencia != renglon["REFERENCIA"].ToString())
                {

                    renglonIndex++;
                    renglonIndex++;

                    IRow renglonDetalle = sheet.CreateRow(renglonIndex); ;

                    renglonDetalle.CreateCell(0).SetCellValue(SubTotal);
                    renglonDetalle.CreateCell(1).SetCellValue(descripcion);
                    renglonDetalle.CreateCell(2).SetCellValue(Convert.ToDouble(referencia));
                    renglonDetalle.CreateCell(3).SetCellValue(Convert.ToDouble(ordenMaquila));
                    renglonDetalle.CreateCell(4).SetCellValue(taller);
                    renglonDetalle.CreateCell(5).SetCellValue(fecha);

                    renglonIndex++;
                    IRow renglonModelos = sheet.CreateRow(renglonIndex); ;


                    string[] modelosSeparados0 = Modelos.Split();
                    if (modelosSeparados0.Length > 9)
                    {
                        string ModelosPt1 = "";
                        string ModelosPt2 = "";
                        for (int iPt = 0; iPt < modelosSeparados0.Length; iPt++)
                        {
                            if (iPt <= 8)
                            {
                                ModelosPt1 += modelosSeparados0[iPt] + " ";
                            }
                            else
                            {
                                ModelosPt2 += modelosSeparados0[iPt] + " ";
                            }
                        }

                        renglonModelos.CreateCell(1).SetCellValue(ModelosPt1.Trim());
                        renglonIndex++;
                        IRow renglonModelosFinal2 = sheet.CreateRow(renglonIndex);
                        renglonModelosFinal2.CreateCell(1).SetCellValue(ModelosPt2.Trim());

                    }
                    else
                    {
                        renglonModelos.CreateCell(1).SetCellValue(Modelos);
                    }

                    if (observacion.Trim() != string.Empty)
                    {
                        renglonIndex++;
                        IRow renglonObs = sheet.CreateRow(renglonIndex);
                        renglonObs.CreateCell(1).SetCellValue(observacion);
                    }
                    Total += SubTotal;

                    SubTotal = Convert.ToInt32(renglon["CANTIDAD"].ToString());
                    descripcion = renglon["DESCRIPCION"].ToString();
                    referencia = renglon["REFERENCIA"].ToString();
                    observacion = renglon["OBS"].ToString();
                    ordenMaquila = renglon["ORDENMAQUILA"].ToString();
                    taller = renglon["TALLER"].ToString();
                    Modelos = ObtenerParteDerecha(renglon["PRODUCTO"].ToString(), 4) + "/" + renglon["CANTIDAD"].ToString() + " ";
                }

            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            //IMPRIME EL ÚLTIMO REGISTRO
            renglonIndex++;
            renglonIndex++;

            IRow renglonDetalleFinal = sheet.CreateRow(renglonIndex); ;

            renglonDetalleFinal.CreateCell(0).SetCellValue(SubTotal);

            renglonDetalleFinal.CreateCell(1).SetCellValue(descripcion);
            renglonDetalleFinal.CreateCell(2).SetCellValue(Convert.ToDouble(referencia));
            renglonDetalleFinal.CreateCell(3).SetCellValue(Convert.ToDouble(ordenMaquila));
            renglonDetalleFinal.CreateCell(4).SetCellValue(taller);
            renglonDetalleFinal.CreateCell(5).SetCellValue(fecha);

            renglonIndex++;
            IRow renglonModelosFinal = sheet.CreateRow(renglonIndex); ;

            string[] modelosSeparados = Modelos.Split();
            if (modelosSeparados.Length > 9)
            {
                string ModelosPt1 = "";
                string ModelosPt2 = "";
                for (int iPt = 0; iPt < modelosSeparados.Length; iPt++)
                {
                    if (iPt <= 8)
                    {
                        ModelosPt1 += modelosSeparados[iPt] + " ";
                    }
                    else
                    {
                        ModelosPt2 += modelosSeparados[iPt] + " ";
                    }
                }

                renglonModelosFinal.CreateCell(1).SetCellValue(ModelosPt1.Trim());
                renglonIndex++;
                IRow renglonModelosFinal2 = sheet.CreateRow(renglonIndex);
                renglonModelosFinal2.CreateCell(1).SetCellValue(ModelosPt2.Trim());

            }
            else
            {
                renglonModelosFinal.CreateCell(1).SetCellValue(Modelos);
            }

            if (observacion.Trim() != string.Empty)
            {
                renglonIndex++;
                IRow renglonObsFinal = sheet.CreateRow(renglonIndex);
                renglonObsFinal.CreateCell(1).SetCellValue(observacion);
            }
            Total += SubTotal;

            ///////////////////////////////////////////////////////////////////////////////////////////////////////


            // Grand Total
            renglonIndex += 2;

            IRow GrandTotal = sheet.CreateRow(renglonIndex);
            GrandTotal.CreateCell(0).SetCellValue("TOTAL");
            GrandTotal.CreateCell(1).SetCellValue(Total);



            #endregion


            for (int i = 0; i < 5; i++)
            {
                /*
                if (i == 1)
                {
                    sheet.SetColumnWidth(i, ExcelNpoiUtil.AnchoColumna(650));
                }
                else
                {
                    sheet.SetColumnWidth(i, ExcelNpoiUtil.AnchoColumna(80));
                }
                 * */
                sheet.AutoSizeColumn(i);
            }

            sheet.FitToPage = false;
            sheet.PrintSetup.Scale = 90;

            if (File.Exists(RutaYNombreArchivo))
            {
                File.Delete(RutaYNombreArchivo);
            }

            FileStream fs = new FileStream(RutaYNombreArchivo, FileMode.CreateNew);

            xlsWorkBook.Write(fs);


            fs.Close();
        }

        public static void GeneraArchivoExcelOrdenesNoLiberadas(string RutaYNombreArchivo, DataTable Tabla, string referenciaFinal)
        {
            HSSFWorkbook xlsWorkBook = new HSSFWorkbook();

            ISheet sheet = xlsWorkBook.CreateSheet("Hoja1");

            #region ENCABEZADOS

            IRow renglonEncabezado = sheet.CreateRow(0);

            renglonEncabezado.CreateCell(1).SetCellValue("Reporte de Órdenes de Producción no liberadas");
            renglonEncabezado.CreateCell(2).SetCellValue(DateTime.Now.ToLongDateString());

            IRow renglonColumnName = sheet.CreateRow(1);
            renglonColumnName.CreateCell(0).SetCellValue("ORDEN");
            renglonColumnName.CreateCell(1).SetCellValue("DESCRIPCION");
            renglonColumnName.CreateCell(2).SetCellValue("TALLAS");
            renglonColumnName.CreateCell(3).SetCellValue("CANT. Pte.");

            #endregion


            #region Desc

            // AQUI COMIENZA EL DETALLE DEL REPORTE

            Int32 SubTotal = 0;
            Int32 Total = 0;
            int renglonIndex = 1; //basado en índice 0
            string referencia = Tabla.Rows[0]["REFERENCIA"].ToString();
            string Tallas = "";
            string descripcion = Tabla.Rows[0]["DESCRIPCION"].ToString();
            string observacion = Tabla.Rows[0]["OBS"].ToString();
            foreach (DataRow renglon in Tabla.Rows)
            {
                if (referencia == renglon["REFERENCIA"].ToString())
                {
                    Tallas += ObtenerParteDerecha(renglon["PRODUCTO"].ToString(), 4) + "/" + renglon["CANTIDAD"].ToString() + " ";
                    SubTotal += Convert.ToInt32(renglon["CANTIDAD"].ToString());
                }

                if (referencia != renglon["REFERENCIA"].ToString())
                {

                    renglonIndex++;
                    renglonIndex++;

                    IRow renglonDetalle = sheet.CreateRow(renglonIndex); ;
                    renglonDetalle.CreateCell(0).SetCellValue(Convert.ToDouble(referencia));//órden
                    renglonDetalle.CreateCell(1).SetCellValue(descripcion);
                    renglonDetalle.CreateCell(2).SetCellValue(Tallas);
                    renglonDetalle.CreateCell(3).SetCellValue(SubTotal);

                    renglonIndex++;
                    IRow renglonObs = sheet.CreateRow(renglonIndex);
                    renglonObs.CreateCell(1).SetCellValue(observacion);

                    Total += SubTotal;

                    SubTotal = Convert.ToInt32(renglon["CANTIDAD"].ToString());
                    descripcion = renglon["DESCRIPCION"].ToString();
                    referencia = renglon["REFERENCIA"].ToString();
                    observacion = renglon["OBS"].ToString();
                    Tallas = ObtenerParteDerecha(renglon["PRODUCTO"].ToString(), 4) + "/" + renglon["CANTIDAD"].ToString() + " ";
                }

            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            //IMPRIME EL ÚLTIMO REGISTRO
            renglonIndex++;
            renglonIndex++;

            IRow renglonDetalleFinal = sheet.CreateRow(renglonIndex); ;
            renglonDetalleFinal.CreateCell(0).SetCellValue(Convert.ToDouble(referencia));//órden
            renglonDetalleFinal.CreateCell(1).SetCellValue(descripcion);
            renglonDetalleFinal.CreateCell(2).SetCellValue(Tallas);
            renglonDetalleFinal.CreateCell(3).SetCellValue(SubTotal);

            renglonIndex++;
            IRow renglonObsFinal = sheet.CreateRow(renglonIndex);
            renglonObsFinal.CreateCell(1).SetCellValue(observacion);

            Total += SubTotal;

            ///////////////////////////////////////////////////////////////////////////////////////////////////////


            // Grand Total
            renglonIndex++;

            IRow GrandTotal = sheet.CreateRow(renglonIndex);
            GrandTotal.CreateCell(2).SetCellValue("TOTAL");
            GrandTotal.CreateCell(3).SetCellValue(Total);


            #endregion


            sheet.SetColumnWidth(0, ExcelNpoiUtil.AnchoColumna(60));
            sheet.SetColumnWidth(1, ExcelNpoiUtil.AnchoColumna(400));
            sheet.SetColumnWidth(2, ExcelNpoiUtil.AnchoColumna(500));
            sheet.SetColumnWidth(3, ExcelNpoiUtil.AnchoColumna(80));

            sheet.FitToPage = false;
            sheet.PrintSetup.Scale = 90;

            if (File.Exists(RutaYNombreArchivo))
            {
                File.Delete(RutaYNombreArchivo);
            }

            FileStream fs = new FileStream(RutaYNombreArchivo, FileMode.CreateNew);

            xlsWorkBook.Write(fs);


            fs.Close();
        }


        public static string ObtenerParteDerecha(string str, int Longitud)
        {
            int value = str.Length - Longitud;
            string Derecha = str.Substring(value, Longitud);
            return Derecha;
        }

        public static string EnviaAProduccion(string id)
        {
            string res = "";
            using (var dbContext = new SIPReportesContext())
            {
                sm_dl.SqlServer.SqlServerCommand Ejecuta = new SqlServerCommand();
                Ejecuta.Connection = sm_dl.DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                Ejecuta.ObjectName = "usp_EntradaAFabricacionOPManual";
                SqlParameter paramResult = new SqlParameter("@resultado", SqlDbType.VarChar, 500);
                paramResult.Direction = ParameterDirection.Output;
                Ejecuta.Parameters.Add(new SqlParameter("@idPedido", id));
                Ejecuta.Parameters.Add(paramResult);
                Ejecuta.Execute();
                res = paramResult.Value.ToString();
            }
            return res;
        }

        public DataTable RegresaTablaConProveedor(DateTime fechaInicial, DateTime fechaFinal, Enumerados.TipoOrdenProduccion tipo)
        {
            System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
            DataTable dataTablePedidos = new DataTable();
            using (var DbContext = new SIPReportesContext())
            {
                SqlServerCommand _cmd = new SqlServerCommand();
                _cmd.Connection = sm_dl.DALUtil.GetConnection(DbContext.Database.Connection.ConnectionString);
                _cmd.ObjectName = "[usp_RepOrdProdMaqu]";
                _cmd.Parameters.Add(new SqlParameter("@fechaInicial", fechaInicial));
                _cmd.Parameters.Add(new SqlParameter("@fechaFinal", fechaFinal));
                _cmd.Parameters.Add(new SqlParameter("@tipo", tipo));
                dataTablePedidos = _cmd.GetDataTable();
                _cmd.Connection.Close();

            }
            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.ElapsedMilliseconds);
            return dataTablePedidos;
        }

        public static void GeneraArchivoExcelConProveedor(string RutaYNombreArchivo, DataTable Tabla, DateTime fechaInicial, DateTime fechaFinal)
        {
            HSSFWorkbook xlsWorkBook = new HSSFWorkbook();

            ISheet sheet = xlsWorkBook.CreateSheet("Hoja1");

            #region ENCABEZADOS

            IRow renglonEncabezado = sheet.CreateRow(0);

            renglonEncabezado.CreateCell(1).SetCellValue("IMP OTs de Producción x MODELO");

            IRow renglonEncabezado2 = sheet.CreateRow(1);

            ICellStyle alineacionDerecha = xlsWorkBook.CreateCellStyle();

            alineacionDerecha.Alignment = HorizontalAlignment.Right;

            ICell celdaFecha = renglonEncabezado2.CreateCell(1);

            celdaFecha.CellStyle = alineacionDerecha;

            celdaFecha.SetCellValue(DateTime.Now.ToLongDateString());

            IRow renglonColumnName = sheet.CreateRow(2);
            renglonColumnName.CreateCell(0).SetCellValue("PTE.");
            renglonColumnName.CreateCell(1).SetCellValue("DESCRIPCION");
            renglonColumnName.CreateCell(2).SetCellValue("ORDEN");
            renglonColumnName.CreateCell(3).SetCellValue("F. ALTA");
            renglonColumnName.CreateCell(4).SetCellValue("F. FIN");
            renglonColumnName.CreateCell(5).SetCellValue("PROVEEDOR");
            renglonColumnName.CreateCell(6).SetCellValue("SEMANAS");


            #endregion


            #region Desc

            // AQUI COMIENZA EL DETALLE DEL REPORTE


            /*
            //Estilos de formato
            ICellStyle celdaEstiloSUM = xlsWorkBook.CreateCellStyle();
            celdaEstiloSUM = xlsWorkBook.CreateCellStyle();
            celdaEstiloSUM.DataFormat = HSSFDataFormat.GetBuiltinFormat("#,##0.00_);(#,##0.00)");


            IDataFormat dFormat2 = xlsWorkBook.CreateDataFormat();
            IDataFormat dFormat4 = xlsWorkBook.CreateDataFormat();

            short dosDig = dFormat2.GetFormat("0.00%");
            short cuatroDig = dFormat4.GetFormat("0.0000%");

            ICellStyle celdaEstiloPorcent2Dig = xlsWorkBook.CreateCellStyle();
            celdaEstiloPorcent2Dig = xlsWorkBook.CreateCellStyle();
            celdaEstiloPorcent2Dig.DataFormat = dosDig;

            ICellStyle celdaEstiloPorcent4Dig = xlsWorkBook.CreateCellStyle();
            celdaEstiloPorcent4Dig = xlsWorkBook.CreateCellStyle();
            celdaEstiloPorcent4Dig.DataFormat = cuatroDig;
            */

            Int32 SubTotal = 0;
            Int32 Total = 0;
            int renglonIndex = 2; //basado en índice 0
            string referencia = Tabla.Rows[0]["REFERENCIA"].ToString();
            string Modelos = "";
            string descripcion = Tabla.Rows[0]["DESCRIPCION"].ToString();
            string observacion = Tabla.Rows[0]["OBS"].ToString();
            string fecha = Tabla.Rows[0]["FCAPTURA"] == null ? "" : DateTime.Parse(Tabla.Rows[0]["FCAPTURA"].ToString()).ToString("dd/MM/yyyy");
            string fechaFin = Tabla.Rows[0]["FENTREGA"] == null ? "" : DateTime.Parse(Tabla.Rows[0]["FENTREGA"].ToString()).ToString("dd/MM/yyyy");
            string proveedor = Tabla.Rows[0]["PROVEEDOR"].ToString();
            // string estatus = Tabla.Rows[0]["ESTATUS"].ToString();
            int fechaEstimada = int.Parse(Tabla.Rows[0]["FECHAESTIMADA"].ToString());

            foreach (DataRow renglon in Tabla.Rows)
            {
                if (referencia == renglon["REFERENCIA"].ToString())
                {
                    Modelos += ObtenerParteDerecha(renglon["PRODUCTO"].ToString(), 4) + "/" + renglon["CANTIDAD"].ToString() + " ";
                    SubTotal += Convert.ToInt32(renglon["CANTIDAD"].ToString());
                    fecha = renglon["FCAPTURA"] == null ? "" : DateTime.Parse(renglon["FCAPTURA"].ToString()).ToString("dd/MM/yyyy");
                    fechaFin = renglon["FENTREGA"] == null ? "" : DateTime.Parse(renglon["FENTREGA"].ToString()).ToString("dd/MM/yyyy");
                    proveedor = renglon["PROVEEDOR"].ToString();
                    fechaEstimada = int.Parse(Tabla.Rows[0]["FECHAESTIMADA"].ToString());
                }

                if (referencia != renglon["REFERENCIA"].ToString())
                {

                    renglonIndex++;
                    renglonIndex++;

                    IRow renglonDetalle = sheet.CreateRow(renglonIndex); ;

                    renglonDetalle.CreateCell(0).SetCellValue(SubTotal);
                    renglonDetalle.CreateCell(1).SetCellValue(descripcion);
                    renglonDetalle.CreateCell(2).SetCellValue(Convert.ToDouble(referencia));
                    renglonDetalle.CreateCell(3).SetCellValue(fecha);
                    renglonDetalle.CreateCell(4).SetCellValue(fechaFin);
                    renglonDetalle.CreateCell(5).SetCellValue(proveedor);
                    renglonDetalle.CreateCell(6).SetCellValue(fechaEstimada);

                    renglonIndex++;
                    IRow renglonModelos = sheet.CreateRow(renglonIndex); ;


                    string[] modelosSeparados0 = Modelos.Split();
                    if (modelosSeparados0.Length > 9)
                    {
                        string ModelosPt1 = "";
                        string ModelosPt2 = "";
                        for (int iPt = 0; iPt < modelosSeparados0.Length; iPt++)
                        {
                            if (iPt <= 8)
                            {
                                ModelosPt1 += modelosSeparados0[iPt] + " ";
                            }
                            else
                            {
                                ModelosPt2 += modelosSeparados0[iPt] + " ";
                            }
                        }

                        renglonModelos.CreateCell(1).SetCellValue(ModelosPt1.Trim());
                        renglonIndex++;
                        IRow renglonModelosFinal2 = sheet.CreateRow(renglonIndex);
                        renglonModelosFinal2.CreateCell(1).SetCellValue(ModelosPt2.Trim());

                    }
                    else
                    {
                        renglonModelos.CreateCell(1).SetCellValue(Modelos);
                    }

                    if (observacion.Trim() != string.Empty)
                    {
                        renglonIndex++;
                        IRow renglonObs = sheet.CreateRow(renglonIndex);
                        renglonObs.CreateCell(1).SetCellValue(observacion);
                    }
                    Total += SubTotal;

                    SubTotal = Convert.ToInt32(renglon["CANTIDAD"].ToString());
                    descripcion = renglon["DESCRIPCION"].ToString();
                    referencia = renglon["REFERENCIA"].ToString();
                    observacion = renglon["OBS"].ToString();
                    Modelos = ObtenerParteDerecha(renglon["PRODUCTO"].ToString(), 4) + "/" + renglon["CANTIDAD"].ToString() + " ";
                    fecha = renglon["FCAPTURA"] == null ? "" : DateTime.Parse(renglon["FCAPTURA"].ToString()).ToString("dd/MM/yyyy");
                    fechaFin = renglon["FENTREGA"] == null ? "" : DateTime.Parse(renglon["FENTREGA"].ToString()).ToString("dd/MM/yyyy");
                    proveedor = renglon["PROVEEDOR"].ToString();
                    fechaEstimada = int.Parse(Tabla.Rows[0]["FECHAESTIMADA"].ToString());
                }

            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            //IMPRIME EL ÚLTIMO REGISTRO
            renglonIndex++;
            renglonIndex++;

            IRow renglonDetalleFinal = sheet.CreateRow(renglonIndex); ;

            renglonDetalleFinal.CreateCell(0).SetCellValue(SubTotal);

            renglonDetalleFinal.CreateCell(1).SetCellValue(descripcion);
            renglonDetalleFinal.CreateCell(2).SetCellValue(Convert.ToDouble(referencia));
            renglonDetalleFinal.CreateCell(3).SetCellValue(fecha);
            renglonDetalleFinal.CreateCell(4).SetCellValue(fechaFin);
            renglonDetalleFinal.CreateCell(5).SetCellValue(proveedor);
            renglonDetalleFinal.CreateCell(6).SetCellValue(fechaEstimada);

            renglonIndex++;
            IRow renglonModelosFinal = sheet.CreateRow(renglonIndex); ;

            string[] modelosSeparados = Modelos.Split();
            if (modelosSeparados.Length > 9)
            {
                string ModelosPt1 = "";
                string ModelosPt2 = "";
                for (int iPt = 0; iPt < modelosSeparados.Length; iPt++)
                {
                    if (iPt <= 8)
                    {
                        ModelosPt1 += modelosSeparados[iPt] + " ";
                    }
                    else
                    {
                        ModelosPt2 += modelosSeparados[iPt] + " ";
                    }
                }

                renglonModelosFinal.CreateCell(1).SetCellValue(ModelosPt1.Trim());
                renglonIndex++;
                IRow renglonModelosFinal2 = sheet.CreateRow(renglonIndex);
                renglonModelosFinal2.CreateCell(1).SetCellValue(ModelosPt2.Trim());

            }
            else
            {
                renglonModelosFinal.CreateCell(1).SetCellValue(Modelos);
            }

            if (observacion.Trim() != string.Empty)
            {
                renglonIndex++;
                IRow renglonObsFinal = sheet.CreateRow(renglonIndex);
                renglonObsFinal.CreateCell(1).SetCellValue(observacion);
            }
            Total += SubTotal;

            ///////////////////////////////////////////////////////////////////////////////////////////////////////


            // Grand Total
            renglonIndex += 2;

            IRow GrandTotal = sheet.CreateRow(renglonIndex);
            GrandTotal.CreateCell(0).SetCellValue("TOTAL");
            GrandTotal.CreateCell(1).SetCellValue(Total);



            #endregion


            for (int i = 0; i < 7; i++)
            {
                if (i == 1)
                {
                    sheet.SetColumnWidth(i, ExcelNpoiUtil.AnchoColumna(650));
                }
                else
                {
                    sheet.SetColumnWidth(i, ExcelNpoiUtil.AnchoColumna(80));
                }
            }

            sheet.FitToPage = false;
            sheet.PrintSetup.Scale = 90;

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
