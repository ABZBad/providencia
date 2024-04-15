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
using ulp_bl.Utiles;
using ulp_dl;
using ulp_dl.aspel_sae80;
using ulp_dl.SIPReportes;

namespace ulp_bl
{
    public class DesempByArea
    {
        private int numeroPedido = 0;
        private string cliente = "";
        private string proceso = "";
        private string observaciones = "";
        private int estandarEspecial = 0;
        private List<Desempeño> lstDesempeño = new List<Desempeño>();
        private bool existePedido;
        private bool existenEstandares;

        public bool ExistenEstandares
        {
            get { return existenEstandares; }
            set { existenEstandares = value; }
        }
        public bool ExistePedido
        {
            get { return existePedido; }
            set { existePedido = value; }
        }
        public List<Desempeño> Desempeños
        {
            set { lstDesempeño = value; }
            get { return lstDesempeño; }
        }
        public int NumeroPedido
        {
            get { return numeroPedido; }
            set { numeroPedido = value; }
        }
        public string Nombre
        {
            get { return cliente; }
            set { cliente = value; }
        }
        public string Proceso
        {
            get { return proceso; }
            set { proceso = value; }
        }
        public string Observaciones
        {
            get { return observaciones; }
            set { observaciones = value; }
        }
        public int EstandarEspecial
        {
            get { return estandarEspecial; }
            set { estandarEspecial = value; }
        }

        public static DesempByArea RegresaDesempeños(int NumeroPedido)
        {
            DesempByArea resultadoDesempeños = new DesempByArea();

            DataTable dataTablePedidos = new DataTable();
            UPPEDIDOS upPedidos = new UPPEDIDOS();
            upPedidos = upPedidos.Consultar(NumeroPedido);

            if (upPedidos != null)
            {
                resultadoDesempeños.ExistePedido = true;
                resultadoDesempeños.NumeroPedido = NumeroPedido;
            }

            if (resultadoDesempeños.ExistePedido)
            {

                using (var dbContext = new ulp_dl.aspel_sae80.AspelSae80Context())
                {
                    // se consultan datos de cabecera
                    var pedidoDetalle = from pd in dbContext.UPPEDIDOS
                                        join cliente in dbContext.CLIE01
                                            on pd.COD_CLIENTE equals cliente.CLAVE into ps
                                        from cliente in ps.DefaultIfEmpty()
                                        where pd.PEDIDO == NumeroPedido
                                        select new
                                        {
                                            cliente.NOMBRE,
                                            pd.COD_CLIENTE,
                                            pd.OBSERVACIONES,
                                            pd.PROCESOS
                                        };

                    string pStdNumPedi = NumeroPedido.ToString();
                    var stdPedyQry = (from stdpedi in dbContext.ESTDPEDI where stdpedi.PEDIDO == pStdNumPedi select stdpedi).FirstOrDefault();
                    if (stdPedyQry != null)
                    {
                        resultadoDesempeños.ExistenEstandares = true;
                        resultadoDesempeños.EstandarEspecial = stdPedyQry.ESP;
                    }
                    else
                    {
                        resultadoDesempeños.ExistenEstandares = false;
                        resultadoDesempeños.EstandarEspecial = 0;
                    }
                    var lstPedido = pedidoDetalle.ToList();

                    if (lstPedido.Count == 1)
                    {

                        resultadoDesempeños.Nombre = string.Format("{0} - {1}", lstPedido[0].COD_CLIENTE.Trim(),
                            lstPedido[0].NOMBRE);
                        resultadoDesempeños.Proceso = lstPedido[0].PROCESOS;
                        resultadoDesempeños.observaciones = lstPedido[0].OBSERVACIONES;
                    }
                    //Se consulta información de detalle
                    var desempeñoPorArea = from desXArea in dbContext.DESEMBYAREA.AsEnumerable() where desXArea.PEDIDO == NumeroPedido.ToString() select desXArea;

                    foreach (var desempeño in desempeñoPorArea)
                    {
                        Desempeño desempeñoObj = new Desempeño();

                        switch (desempeño.DEPTO)
                        {
                            case "Almacen":
                                desempeñoObj.Area = Enumerados.AreasEmpresa.Almacen;
                                break;
                            case "Cliente":
                                desempeñoObj.Area = Enumerados.AreasEmpresa.Cliente;
                                break;
                            case "Compras":
                                desempeñoObj.Area = Enumerados.AreasEmpresa.Compras;
                                break;
                            case "Credito":
                                desempeñoObj.Area = Enumerados.AreasEmpresa.Credito;
                                break;
                            case "Operaciones":
                                desempeñoObj.Area = Enumerados.AreasEmpresa.Operaciones;
                                break;
                            case "Sistemas":
                                desempeñoObj.Area = Enumerados.AreasEmpresa.Sistemas;
                                break;
                            case "Ventas":
                                desempeñoObj.Area = Enumerados.AreasEmpresa.Ventas;
                                break;
                        }

                        desempeñoObj.AreaStr = desempeño.DEPTO;
                        desempeñoObj.Cumplio = (desempeño.CUMPLIO == "S" ? true : false);
                        desempeñoObj.CumplioStr = desempeño.CUMPLIO;
                        desempeñoObj.Observaciones = (string.IsNullOrEmpty(desempeño.OBSERVACIONES) ? "" : desempeño.OBSERVACIONES);
                        resultadoDesempeños.Desempeños.Add(desempeñoObj);
                    }

                }


            }

            return resultadoDesempeños;
        }
        private static int RegresaDesempeñoSegunEstandares(DataRow RenglonDetalleDesempeño)
        {
            int CalcularDesem = 0;
            Enumerados.EstandaresPedidos? stdTop = null;
            int DiasTop = 0;

            foreach (Enumerados.EstandaresPedidos estandarPedido in Enum.GetValues(typeof(Enumerados.EstandaresPedidos)))
            {
                int valorDelEstandar = Convert.ToInt32(RenglonDetalleDesempeño[estandarPedido.ToString()]);

                if (valorDelEstandar > DiasTop)
                {
                    stdTop = estandarPedido;
                    DiasTop = valorDelEstandar;
                }

            }
            //Si el mas alto fue LIB, SUR o ESP
            if (stdTop == Enumerados.EstandaresPedidos.LIB || stdTop == Enumerados.EstandaresPedidos.SUR ||
                stdTop == Enumerados.EstandaresPedidos.ESP)
            {
                int adv = Convert.ToInt32(RenglonDetalleDesempeño[Enumerados.EstandaresPedidos.ADVO.ToString()]);
                int cor = Convert.ToInt32(RenglonDetalleDesempeño[Enumerados.EstandaresPedidos.COR.ToString()]);
                int est = Convert.ToInt32(RenglonDetalleDesempeño[Enumerados.EstandaresPedidos.EST.ToString()]);
                int bor = Convert.ToInt32(RenglonDetalleDesempeño[Enumerados.EstandaresPedidos.BOR.ToString()]);
                int ini = Convert.ToInt32(RenglonDetalleDesempeño[Enumerados.EstandaresPedidos.INI.ToString()]);
                int cos = Convert.ToInt32(RenglonDetalleDesempeño[Enumerados.EstandaresPedidos.COS.ToString()]);
                int emb = Convert.ToInt32(RenglonDetalleDesempeño[Enumerados.EstandaresPedidos.EMB.ToString()]);
                int emp = Convert.ToInt32(RenglonDetalleDesempeño[Enumerados.EstandaresPedidos.EMP.ToString()]);

                CalcularDesem = Convert.ToInt32(RenglonDetalleDesempeño[Enumerados.EstandaresPedidos.LIB.ToString()]) +
                                Convert.ToInt32(RenglonDetalleDesempeño[Enumerados.EstandaresPedidos.SUR.ToString()]) +
                                Convert.ToInt32(RenglonDetalleDesempeño[Enumerados.EstandaresPedidos.ESP.ToString()]) +
                                (adv != 0 ? 1 : 0) +
                                (cor != 0 ? 1 : 0) +
                                (est != 0 ? 1 : 0) +
                                (bor != 0 ? 1 : 0) +
                                (ini != 0 ? 1 : 0) +
                                (cos != 0 ? 1 : 0) +
                                (emb != 0 ? 1 : 0) +
                                (emp != 0 ? 1 : 0);

            }
            else
            {
                foreach (Enumerados.EstandaresPedidos estandarPedido in Enum.GetValues(typeof(Enumerados.EstandaresPedidos)))
                {
                    if ((estandarPedido == Enumerados.EstandaresPedidos.LIB || estandarPedido == Enumerados.EstandaresPedidos.SUR || estandarPedido == Enumerados.EstandaresPedidos.ESP || estandarPedido == stdTop))
                    {
                        CalcularDesem += Convert.ToInt32(RenglonDetalleDesempeño[estandarPedido.ToString()]);
                    }
                    else
                    {
                        if (Convert.ToInt32(RenglonDetalleDesempeño[estandarPedido.ToString()]) != 0)
                        {
                            CalcularDesem++;
                        }
                    }
                }
            }
            return CalcularDesem;
        }
        public static DataTable RegresaDetalleDesempeño(DateTime FechaInicial, DateTime FechaFinal)
        {
            DataTable dataTableDetalleDesempeño = new DataTable();
            SqlServerCommand cmd = new SqlServerCommand();
            using (var dbContext = new SIPReportesContext())
            {
                cmd.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmd.ObjectName = "usp_RepFechasYDesempeñoPorArea";
                cmd.Parameters.Add(new SqlParameter("@fecha_inicial", FechaInicial.Date));
                cmd.Parameters.Add(new SqlParameter("@fecha_final", FechaFinal.Date));
                //OBTENEMOS REPORTE COMPLETO
                dataTableDetalleDesempeño = cmd.GetDataTable();
                cmd.Connection.Close();
            }

            foreach (DataRow renglonDetalle in dataTableDetalleDesempeño.Rows)
            {
                int dias = 0;
                //obtenemos el tiempo de entrega real basado en TODOS los estandares 
                dias = DesempByArea.RegresaDesempeñoSegunEstandares(renglonDetalle);

                /*                    
                SqlServerCommand cmdFechaStd = new SqlServerCommand();
                cmdFechaStd.Connection = cmd.Connection;
                cmdFechaStd.ObjectName = "usp_RepFechasYDesempeñoPorAreaFechaStd";
                cmdFechaStd.Parameters.Add(new SqlParameter("@pedido", Convert.ToInt32(renglonDetalle["Pedido"])));
                cmdFechaStd.Parameters.Add(new SqlParameter("@dias", dias));
                DataTable dtFStd = cmdFechaStd.GetDataTable();
                DateTime dtFStd2;



                DateTime.TryParseExact(dtFStd.Rows[0][0].ToString(), "dd MMM yy",
                    System.Globalization.CultureInfo.GetCultureInfo("es-MX"),
                    System.Globalization.DateTimeStyles.None, out dtFStd2);
                    

                //DateTime fechaStd = Convert.ToDateTime();



                cmdFechaStd.Connection.Close();
                */

                renglonDetalle.SetField("Fecha Estandar", DateTime.Parse(renglonDetalle["Fecha SAE"].ToString()).AddDays(dias));
                renglonDetalle.SetField("Tiempo Total Estandar", dias);

            }


            return dataTableDetalleDesempeño;
        }
        public static int RegresaTotalPedidosContado(DateTime FechaInicial, DateTime FechaFinal)
        {
            SqlServerCommand cmd = new SqlServerCommand();
            using (var dbContext = new SIPReportesContext())
            {
                cmd.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
            }
            cmd.ObjectName = "usp_RepDesempeñoTotalPedidosContado";
            cmd.Parameters.Add(new SqlParameter("@fecha_inicial", FechaInicial));
            cmd.Parameters.Add(new SqlParameter("@fecha_final", FechaFinal));

            int totalPedidosContado = (int)cmd.GetScalar();
            cmd.Connection.Close();
            return totalPedidosContado;
        }
        public static DataTable RegresaPedidosContadoPorAgente(DateTime FechaInicial, DateTime FechaFinal)
        {
            DataTable dataTablePedidosContadoPorAgente = new DataTable();

            SqlServerCommand cmd = new SqlServerCommand();
            using (var dbContext = new SIPReportesContext())
            {
                cmd.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
            }
            cmd.ObjectName = "usp_RepDesempeñoTotalPedidosContadoPorAgente";
            cmd.Parameters.Add(new SqlParameter("@fecha_inicial", FechaInicial.Date));
            cmd.Parameters.Add(new SqlParameter("@fecha_final", FechaFinal.Date));

            dataTablePedidosContadoPorAgente = cmd.GetDataTable();
            cmd.Connection.Close();
            return dataTablePedidosContadoPorAgente;
        }
        public static int RegresaFechasCumplidas(DateTime FechaInicial, DateTime FechaFinal, Enumerados.TipoFechaReporteDesempeños TipoFechaDesempeño)
        {
            SqlServerCommand cmd = new SqlServerCommand();
            using (var dbContext = new SIPReportesContext())
            {
                cmd.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
            }
            cmd.ObjectName = "usp_RepDesempeñoTotalPedidosSegunFecha";
            cmd.Parameters.Add(new SqlParameter("@fecha_inicial", FechaInicial.Date));
            cmd.Parameters.Add(new SqlParameter("@fecha_final", FechaFinal.Date));
            cmd.Parameters.Add(new SqlParameter("@tipo_fecha", TipoFechaDesempeño));

            int totalFechasDesempeño = (int)cmd.GetScalar();
            cmd.Connection.Close();
            return totalFechasDesempeño;
        }
        public static DataTable RegresaDesempeñoPorAreaResumen(DateTime FechaInicial, DateTime FechaFinal)
        {
            DataTable dataTableDesempeñoPorAreaResumen = new DataTable();

            SqlServerCommand cmd = new SqlServerCommand();
            using (var dbContext = new SIPReportesContext())
            {
                cmd.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
            }
            cmd.ObjectName = "usp_RepDesmpeñoPorAreasResumen";
            cmd.Parameters.Add(new SqlParameter("@fecha_inicial", FechaInicial.Date));
            cmd.Parameters.Add(new SqlParameter("@fecha_final", FechaFinal.Date));

            dataTableDesempeñoPorAreaResumen = cmd.GetDataTable();
            cmd.Connection.Close();
            return dataTableDesempeñoPorAreaResumen;
        }

        private static DateTime? ValorComoFecha(string Fecha)
        {
            DateTime dtValue;
            bool resp = DateTime.TryParse(Fecha, out dtValue);

            return dtValue;


        }
        public static void GeneraArchivoExcel(DataTable DetalleDesempeño, DataTable PedidosContadoPorAgente, int TotFechasCumplidas, int TotFechasAdelantadas, int TotFechasNoCumplidas, int TotFechasNoEntregadas, DataTable DesempeñoPorAreaResumen, string RutaYNombreArchivo)
        {
            HSSFWorkbook xlsWorkBook = new HSSFWorkbook();

            //formato de para las fechas
            IDataFormat dataFormatFecha = xlsWorkBook.CreateDataFormat();
            ICellStyle cellStyleFecha = xlsWorkBook.CreateCellStyle();
            cellStyleFecha.DataFormat = dataFormatFecha.GetFormat("dd-mmm-yy");


            //reporte 1
            ISheet sheet = xlsWorkBook.CreateSheet("Detalle");
            #region ENCABEZADOS
            IRow renglonCabezera = sheet.CreateRow(0);

            renglonCabezera.CreateCell(0).SetCellValue("PEDIDO");
            renglonCabezera.CreateCell(1).SetCellValue("NOMBRE");
            renglonCabezera.CreateCell(2).SetCellValue("CVE_VEND");
            renglonCabezera.CreateCell(3).SetCellValue("PRENDAS");
            //FECHAS
            renglonCabezera.CreateCell(4).SetCellValue("FECHA SIP");
            renglonCabezera.CreateCell(5).SetCellValue("FECHA SAE");
            renglonCabezera.CreateCell(6).SetCellValue("FECHA VENCIMIENTO");
            renglonCabezera.CreateCell(7).SetCellValue("FECHA ESTANDAR");
            renglonCabezera.CreateCell(8).SetCellValue("FECHA ENTREGADO");

            //TOTAL DIAS
            renglonCabezera.CreateCell(9).SetCellValue("TOTAL");
            //ADMINISTRATIVO
            renglonCabezera.CreateCell(10).SetCellValue("ESTANDAR ADVO");
            renglonCabezera.CreateCell(11).SetCellValue("FECHA IMPRESION");
            renglonCabezera.CreateCell(12).SetCellValue("FECHA CREDITO");
            renglonCabezera.CreateCell(13).SetCellValue("REAL ADVO");

            //DETALLE DE PROCESOS
            renglonCabezera.CreateCell(14).SetCellValue("COR");
            renglonCabezera.CreateCell(15).SetCellValue("EST");
            renglonCabezera.CreateCell(16).SetCellValue("BOR");
            renglonCabezera.CreateCell(17).SetCellValue("INI");
            renglonCabezera.CreateCell(18).SetCellValue("COS");
            renglonCabezera.CreateCell(19).SetCellValue("ESP");
            renglonCabezera.CreateCell(20).SetCellValue("FECHA FIN CREDITO");
            renglonCabezera.CreateCell(21).SetCellValue("FECHA LIBERACION");
            renglonCabezera.CreateCell(22).SetCellValue("REAL PROCESOS");

            //EMPAQUE
            renglonCabezera.CreateCell(23).SetCellValue("EMPAQUE");
            renglonCabezera.CreateCell(24).SetCellValue("FECHA FIN LIBERACION");
            renglonCabezera.CreateCell(25).SetCellValue("FECHA EMPAQUE");
            renglonCabezera.CreateCell(26).SetCellValue("REAL EMP");

            //EMBARQUE
            renglonCabezera.CreateCell(27).SetCellValue("EMBARQUE");
            renglonCabezera.CreateCell(28).SetCellValue("FECHA FIN EMPAQUE");
            renglonCabezera.CreateCell(29).SetCellValue("FECHA EMBARQUE");
            renglonCabezera.CreateCell(30).SetCellValue("REAL EMBARQUE");

            //renglonCabezera.CreateCell(38).SetCellValue("DATOS ADICIONALES");
            renglonCabezera.CreateCell(31).SetCellValue("OBSERVACIONES");
            renglonCabezera.CreateCell(32).SetCellValue("COMENTARIOS");
            renglonCabezera.CreateCell(33).SetCellValue("OBSER. VENTAS");
            renglonCabezera.CreateCell(34).SetCellValue("OBSER. ALMACEN");
            renglonCabezera.CreateCell(35).SetCellValue("OBSER. COMPRAS");
            renglonCabezera.CreateCell(36).SetCellValue("OBSER. SISTEMAS");
            renglonCabezera.CreateCell(37).SetCellValue("OBSER. OPERACIONES");
            renglonCabezera.CreateCell(38).SetCellValue("OBSER. CREDITO");
            renglonCabezera.CreateCell(39).SetCellValue("OBSER. CLIENTE");

            //PROCESOS Y ESPECIAL
            renglonCabezera.CreateCell(40).SetCellValue("PROCESOS");
            renglonCabezera.CreateCell(41).SetCellValue("P. ESPECIAL");
            renglonCabezera.CreateCell(42).SetCellValue("TIPO PEDIDO");
            renglonCabezera.CreateCell(43).SetCellValue("FECHA SURTIDO");
            renglonCabezera.CreateCell(44).SetCellValue("FECHA BORDADO");
            renglonCabezera.CreateCell(45).SetCellValue("FECHA ESTAMPADO");

            #endregion
            #region VARIABLES DE CONTROL
            int renglonDetalle = 1;
            IDataFormat dFormat2 = xlsWorkBook.CreateDataFormat();
            short fmtFecha = dFormat2.GetFormat("dd-mmm-yy");

            #endregion
            #region DETALLE

            foreach (DataRow renglonDetalleDesempeño in DetalleDesempeño.Rows)
            {
                int i = 0;
                IRow renglonActual = sheet.CreateRow(renglonDetalle);

                // ICellStyle styleFecha = xlsWorkBook.CreateCellStyle();
                //styleFecha.DataFormat = fmtFecha;


                //celdaFchIng.SetCellValue((renglonDetalleDesempeño["Fch Ing"].ToString() == string.Empty ? "" : renglonDetalleDesempeño["Fch Ing"].ToString()));
                // celdaFchIng.CellStyle.DataFormat = fmtFecha;


                ICellStyle styleNumEntero = xlsWorkBook.CreateCellStyle();
                styleNumEntero.DataFormat = dFormat2.GetFormat("0");
                /**************************************************************************************************
                ***************************************ENCABEZADO DATOS PEDIDO*************************************
                ****************************************************************************************************/

                //renglonActual.CreateCell(0).SetCellValue(Convert.ToString(renglonDetalleDesempeño["DATOS DE PEDIDO"]));                

                ICell celdaPedido = renglonActual.CreateCell(i);
                celdaPedido.SetCellValue(Convert.ToInt32(renglonDetalleDesempeño["PEDIDO"]));
                celdaPedido.CellStyle = styleNumEntero;
                i++;

                renglonActual.CreateCell(i).SetCellValue(Convert.ToString(renglonDetalleDesempeño["NOMBRE"])); i++;
                renglonActual.CreateCell(i).SetCellValue(Convert.ToString(renglonDetalleDesempeño["CVE_VEND"])); i++;

                ICell celdaPren = renglonActual.CreateCell(i);
                celdaPren.SetCellValue(Convert.ToInt32(renglonDetalleDesempeño["PRENDAS"]));
                celdaPren.CellStyle = styleNumEntero;
                i++;

                /***************************************************************************************************
                *******************************************FECHAS***************************************************
                ****************************************************************************************************/

                renglonActual.CreateCell(i).SetCellValue(DateTime.Parse(renglonDetalleDesempeño["Fecha SIP"].ToString()) == new DateTime(1900, 1, 1) ? "NA" : DateTime.Parse(renglonDetalleDesempeño["Fecha SIP"].ToString()).ToString("dd/MM/yyyy")); i++;
                renglonActual.CreateCell(i).SetCellValue(DateTime.Parse(renglonDetalleDesempeño["Fecha SAE"].ToString()) == new DateTime(1900, 1, 1) ? "NA" : DateTime.Parse(renglonDetalleDesempeño["Fecha SAE"].ToString()).ToString("dd/MM/yyyy")); i++;
                renglonActual.CreateCell(i).SetCellValue(DateTime.Parse(renglonDetalleDesempeño["Fecha Vencimiento"].ToString()) == new DateTime(1900, 1, 1) ? "NA" : DateTime.Parse(renglonDetalleDesempeño["Fecha Vencimiento"].ToString()).ToString("dd/MM/yyyy")); i++;
                renglonActual.CreateCell(i).SetCellValue(DateTime.Parse(renglonDetalleDesempeño["Fecha Estandar"].ToString()) == new DateTime(1900, 1, 1) ? "NA" : DateTime.Parse(renglonDetalleDesempeño["Fecha Estandar"].ToString()).ToString("dd/MM/yyyy")); i++;
                renglonActual.CreateCell(i).SetCellValue(DateTime.Parse(renglonDetalleDesempeño["Fecha Entregado"].ToString()) == new DateTime(1900, 1, 1) ? "NA" : DateTime.Parse(renglonDetalleDesempeño["Fecha Entregado"].ToString()).ToString("dd/MM/yyyy")); i++;

                /***************************************************************************************************
                ***************************************ANALISIS DE ESTANDARES***************************************
                ****************************************************************************************************/
                ICell celdaTiempoTotalEstandar = renglonActual.CreateCell(i);
                celdaTiempoTotalEstandar.SetCellValue(Convert.ToInt32(renglonDetalleDesempeño["Tiempo Total Estandar"]));
                celdaTiempoTotalEstandar.CellStyle = styleNumEntero;
                i++;


                /*/***************************************ADMINSITRATIVO********************************************/

                ICell celdaADVO = renglonActual.CreateCell(i); i++;
                celdaADVO.SetCellValue(Convert.ToInt32(renglonDetalleDesempeño["ADVO"]));
                celdaADVO.CellStyle = styleNumEntero;

                renglonActual.CreateCell(i).SetCellValue(DateTime.Parse(renglonDetalleDesempeño["Fecha Impresion"].ToString()) == new DateTime(1900, 1, 1) ? "NA" : DateTime.Parse(renglonDetalleDesempeño["Fecha SIP"].ToString()).ToString("dd/MM/yyyy")); i++;
                renglonActual.CreateCell(i).SetCellValue(DateTime.Parse(renglonDetalleDesempeño["Fecha Credito"].ToString()) == new DateTime(1900, 1, 1) ? "NA" : DateTime.Parse(renglonDetalleDesempeño["Fecha SAE"].ToString()).ToString("dd/MM/yyyy")); i++;

                ICell celdaTiempoADVOReal = renglonActual.CreateCell(i);
                celdaTiempoADVOReal.SetCellValue(Convert.ToInt32(renglonDetalleDesempeño["Tiempo Administrativo Real"]));
                celdaTiempoADVOReal.CellStyle = styleNumEntero;
                i++;

                /*/***************************************PROCESOS********************************************/

                ICell celdaCOR = renglonActual.CreateCell(i);
                celdaCOR.SetCellValue(Convert.ToInt32(renglonDetalleDesempeño["COR"]));
                celdaCOR.CellStyle = styleNumEntero;
                i++;
                ICell celdaEST = renglonActual.CreateCell(i);
                celdaEST.SetCellValue(Convert.ToInt32(renglonDetalleDesempeño["EST"]));
                celdaEST.CellStyle = styleNumEntero;
                i++;
                ICell celdaBOR = renglonActual.CreateCell(i);
                celdaBOR.SetCellValue(Convert.ToInt32(renglonDetalleDesempeño["BOR"]));
                celdaBOR.CellStyle = styleNumEntero;
                i++;
                ICell celdaINI = renglonActual.CreateCell(i);
                celdaINI.SetCellValue(Convert.ToInt32(renglonDetalleDesempeño["INI"]));
                celdaINI.CellStyle = styleNumEntero;
                i++;
                ICell celdaCOS = renglonActual.CreateCell(i);
                celdaCOS.SetCellValue(Convert.ToInt32(renglonDetalleDesempeño["COS"]));
                celdaCOS.CellStyle = styleNumEntero;
                i++;
                ICell celdaESP = renglonActual.CreateCell(i);
                celdaESP.SetCellValue(Convert.ToInt32(renglonDetalleDesempeño["ESP"]));
                celdaESP.CellStyle = styleNumEntero;
                i++;
                renglonActual.CreateCell(i).SetCellValue(DateTime.Parse(renglonDetalleDesempeño["Fecha Fin Credito"].ToString()) == new DateTime(1900, 1, 1) ? "NA" : DateTime.Parse(renglonDetalleDesempeño["Fecha Fin Credito"].ToString()).ToString("dd/MM/yyyy")); i++;
                renglonActual.CreateCell(i).SetCellValue(DateTime.Parse(renglonDetalleDesempeño["Fecha Liberacion"].ToString()) == new DateTime(1900, 1, 1) ? "NA" : DateTime.Parse(renglonDetalleDesempeño["Fecha Liberacion"].ToString()).ToString("dd/MM/yyyy")); i++;

                ICell celdaTiempoProcesosOReal = renglonActual.CreateCell(i);
                celdaTiempoProcesosOReal.SetCellValue(Convert.ToInt32(renglonDetalleDesempeño["Tiempo Procesos Real"]));
                celdaTiempoProcesosOReal.CellStyle = styleNumEntero;
                i++;
                /*/***************************************EMPAQUE********************************************/


                ICell celdaEMP = renglonActual.CreateCell(i);
                celdaEMP.SetCellValue(Convert.ToInt32(renglonDetalleDesempeño["EMP"]));
                celdaEMP.CellStyle = styleNumEntero;
                i++;
                renglonActual.CreateCell(i).SetCellValue(DateTime.Parse(renglonDetalleDesempeño["Fecha Fin Liberacion"].ToString()) == new DateTime(1900, 1, 1) ? "NA" : DateTime.Parse(renglonDetalleDesempeño["Fecha Fin Liberacion"].ToString()).ToString("dd/MM/yyyy")); i++;
                renglonActual.CreateCell(i).SetCellValue(DateTime.Parse(renglonDetalleDesempeño["Fecha Empaque"].ToString()) == new DateTime(1900, 1, 1) ? "NA" : DateTime.Parse(renglonDetalleDesempeño["Fecha Empaque"].ToString()).ToString("dd/MM/yyyy")); i++;


                ICell celdaTiempoEMPReal = renglonActual.CreateCell(i);
                celdaTiempoEMPReal.SetCellValue(Convert.ToInt32(renglonDetalleDesempeño["Tiempo Empaque Real"]));
                celdaTiempoEMPReal.CellStyle = styleNumEntero;
                i++;

                /*/***************************************EMBARQUE********************************************/

                ICell celdaEMB = renglonActual.CreateCell(i);
                celdaEMB.SetCellValue(Convert.ToInt32(renglonDetalleDesempeño["EMP"]));
                celdaEMB.CellStyle = styleNumEntero;
                i++;

                renglonActual.CreateCell(i).SetCellValue(DateTime.Parse(renglonDetalleDesempeño["Fecha Fin Liberacion"].ToString()) == new DateTime(1900, 1, 1) ? "NA" : DateTime.Parse(renglonDetalleDesempeño["Fecha Fin Liberacion"].ToString()).ToString("dd/MM/yyyy")); i++;
                renglonActual.CreateCell(i).SetCellValue(DateTime.Parse(renglonDetalleDesempeño["Fecha Empaque"].ToString()) == new DateTime(1900, 1, 1) ? "NA" : DateTime.Parse(renglonDetalleDesempeño["Fecha Empaque"].ToString()).ToString("dd/MM/yyyy")); i++;

                ICell celdaTiempoEMBReal = renglonActual.CreateCell(i);
                celdaTiempoEMBReal.SetCellValue(Convert.ToInt32(renglonDetalleDesempeño["Tiempo Embarque Real"]));
                celdaTiempoEMBReal.CellStyle = styleNumEntero;
                i++;
                /***************************************************************************************************
                ********************************************DATOS ADICIONALES***************************************
                ****************************************************************************************************/
                renglonActual.CreateCell(i).SetCellValue(Convert.ToString(renglonDetalleDesempeño["Observaciones"])); i++;
                renglonActual.CreateCell(i).SetCellValue(Convert.ToString(renglonDetalleDesempeño["Comentarios"])); i++;
                renglonActual.CreateCell(i).SetCellValue(Convert.ToString(renglonDetalleDesempeño["Obser. Ventas"])); i++;
                renglonActual.CreateCell(i).SetCellValue(Convert.ToString(renglonDetalleDesempeño["Obser. Almacen"])); i++;
                renglonActual.CreateCell(i).SetCellValue(Convert.ToString(renglonDetalleDesempeño["Obser. Compras"])); i++;
                renglonActual.CreateCell(i).SetCellValue(Convert.ToString(renglonDetalleDesempeño["Obser. Sistemas"])); i++;
                renglonActual.CreateCell(i).SetCellValue(Convert.ToString(renglonDetalleDesempeño["Obser. Operaciones"])); i++;
                renglonActual.CreateCell(i).SetCellValue(Convert.ToString(renglonDetalleDesempeño["Obser. Credito"])); i++;
                renglonActual.CreateCell(i).SetCellValue(Convert.ToString(renglonDetalleDesempeño["Obser. Cliente"])); i++;

                /***************************************************************************************************
                ********************************************PEDIDO ESPECIAL Y PROCESOS******************************
                ****************************************************************************************************/
                var aux = Convert.ToString(renglonDetalleDesempeño["Procesos"]).Split(',');
                renglonActual.CreateCell(i).SetCellValue(Convert.ToString(renglonDetalleDesempeño["Procesos"])); i++;
                renglonActual.CreateCell(i).SetCellValue(Convert.ToString(renglonDetalleDesempeño["esEspecial"])); i++;
                renglonActual.CreateCell(i).SetCellValue(Convert.ToString(renglonDetalleDesempeño["TipoPedido"])); i++;
                renglonActual.CreateCell(i).SetCellValue(DateTime.Parse(renglonDetalleDesempeño["Fecha Surtido"].ToString()) == new DateTime(1900, 1, 1) ? "NA" : DateTime.Parse(renglonDetalleDesempeño["Fecha Surtido"].ToString()).ToString("dd/MM/yyyy")); i++;
                renglonActual.CreateCell(i).SetCellValue(DateTime.Parse(renglonDetalleDesempeño["Fecha Bordado"].ToString()) == new DateTime(1900, 1, 1) ? "NA" : DateTime.Parse(renglonDetalleDesempeño["Fecha Bordado"].ToString()).ToString("dd/MM/yyyy")); i++;
                renglonActual.CreateCell(i).SetCellValue(DateTime.Parse(renglonDetalleDesempeño["Fecha Estampado"].ToString()) == new DateTime(1900, 1, 1) ? "NA" : DateTime.Parse(renglonDetalleDesempeño["Fecha Estampado"].ToString()).ToString("dd/MM/yyyy")); i++;

                /*


                try
                {
                    renglonActual.CreateCell(5).SetCellValue(Convert.ToString(renglonDetalleDesempeño["Proc"]));
                    renglonActual.CreateCell(6)
                        .SetCellValue(Convert.ToDouble(renglonDetalleDesempeño["ADVO"].ToString()));
                    renglonActual.CreateCell(7)
                        .SetCellValue(Convert.ToDouble(renglonDetalleDesempeño["LIB"].ToString()));
                    renglonActual.CreateCell(8)
                        .SetCellValue(Convert.ToDouble(renglonDetalleDesempeño["SUR"].ToString()));
                    renglonActual.CreateCell(9)
                        .SetCellValue(Convert.ToDouble(renglonDetalleDesempeño["COR"].ToString()));
                    renglonActual.CreateCell(10)
                        .SetCellValue(Convert.ToDouble(renglonDetalleDesempeño["EST"].ToString()));
                    renglonActual.CreateCell(11)
                        .SetCellValue(Convert.ToDouble(renglonDetalleDesempeño["BOR"].ToString()));
                    renglonActual.CreateCell(12)
                        .SetCellValue(Convert.ToDouble(renglonDetalleDesempeño["INI"].ToString()));
                    renglonActual.CreateCell(13)
                        .SetCellValue(Convert.ToDouble(renglonDetalleDesempeño["COS"].ToString()));
                    renglonActual.CreateCell(14)
                        .SetCellValue(Convert.ToDouble(renglonDetalleDesempeño["EMP"].ToString()));
                    renglonActual.CreateCell(15)
                        .SetCellValue(Convert.ToDouble(renglonDetalleDesempeño["EMB"].ToString()));
                    renglonActual.CreateCell(16)
                        .SetCellValue(Convert.ToDouble(renglonDetalleDesempeño["ESP"].ToString()));
                    renglonActual.CreateCell(17)
                        .SetCellValue(Convert.ToDouble(renglonDetalleDesempeño["dias"].ToString()));
                }
                catch (FormatException FmtoEx)
                {
                    renglonActual.CreateCell(5).SetCellValue(Convert.ToString(renglonDetalleDesempeño["Proc"]));
                    renglonActual.CreateCell(6).SetCellValue(renglonDetalleDesempeño["ADVO"].ToString());
                    renglonActual.CreateCell(7).SetCellValue(renglonDetalleDesempeño["LIB"].ToString());
                    renglonActual.CreateCell(8).SetCellValue(renglonDetalleDesempeño["SUR"].ToString());
                    renglonActual.CreateCell(9).SetCellValue(renglonDetalleDesempeño["COR"].ToString());
                    renglonActual.CreateCell(10).SetCellValue(renglonDetalleDesempeño["EST"].ToString());
                    renglonActual.CreateCell(11).SetCellValue(renglonDetalleDesempeño["BOR"].ToString());
                    renglonActual.CreateCell(12).SetCellValue(renglonDetalleDesempeño["INI"].ToString());
                    renglonActual.CreateCell(13).SetCellValue(renglonDetalleDesempeño["COS"].ToString());
                    renglonActual.CreateCell(14).SetCellValue(renglonDetalleDesempeño["EMP"].ToString());
                    renglonActual.CreateCell(15).SetCellValue(renglonDetalleDesempeño["EMB"].ToString());
                    renglonActual.CreateCell(16).SetCellValue(renglonDetalleDesempeño["ESP"].ToString());
                    renglonActual.CreateCell(17).SetCellValue(renglonDetalleDesempeño["dias"].ToString());
                }
                
                

                var fch_Std = ValorComoFecha(renglonDetalleDesempeño["Fch Stnd"].ToString());
                var fch_Prog = ValorComoFecha(renglonDetalleDesempeño["Fch Prog."].ToString());
                var fch_Ing = ValorComoFecha(renglonDetalleDesempeño["Fch Ing"].ToString());
                var fch_Real = ValorComoFecha(renglonDetalleDesempeño["Fch Real"].ToString());
                var fch_Ruta = ValorComoFecha(renglonDetalleDesempeño["Fch Ruta"].ToString());
                var fch_Lib = ValorComoFecha(renglonDetalleDesempeño["Fch Lib."].ToString());
                var fch_Surt = ValorComoFecha(renglonDetalleDesempeño["Fch Surt."].ToString());
                
                if (fch_Ing.Value.Year != 1) { ExcelNpoiUtil.AsignaValorCelda(ref sheet, renglonDetalle, 0, cellStyleFecha, fch_Ing); }
                if (fch_Std.Value.Year != 1) { ExcelNpoiUtil.AsignaValorCelda(ref sheet, renglonDetalle, 18, cellStyleFecha, fch_Std); }
                if (fch_Prog.Value.Year != 1) { ExcelNpoiUtil.AsignaValorCelda(ref sheet, renglonDetalle, 19, cellStyleFecha, fch_Prog); }
                if (fch_Real.Value.Year != 1) { ExcelNpoiUtil.AsignaValorCelda(ref sheet, renglonDetalle, 20, cellStyleFecha,fch_Real); }
                if (fch_Ruta.Value.Year != 1) { ExcelNpoiUtil.AsignaValorCelda(ref sheet, renglonDetalle, 21, cellStyleFecha, fch_Ruta); }
                if (fch_Lib.Value.Year != 1) { ExcelNpoiUtil.AsignaValorCelda(ref sheet, renglonDetalle, 22, cellStyleFecha, fch_Lib); }
                if (fch_Surt.Value.Year != 1) { ExcelNpoiUtil.AsignaValorCelda(ref sheet, renglonDetalle, 23, cellStyleFecha, fch_Surt); }

                
                renglonActual.CreateCell(24).SetCellValue(Convert.ToString(renglonDetalleDesempeño["Observaciones"]));

                renglonActual.CreateCell(25).SetCellValue(Convert.ToString(renglonDetalleDesempeño["COMENTARIOS"]));

                renglonActual.CreateCell(26).SetCellValue(Convert.ToString(renglonDetalleDesempeño["Obser. Ventas"]));
                renglonActual.CreateCell(27).SetCellValue(Convert.ToString(renglonDetalleDesempeño["Obser. Almacén"]));
                renglonActual.CreateCell(28).SetCellValue(Convert.ToString(renglonDetalleDesempeño["Obser. Compras"]));
                renglonActual.CreateCell(29).SetCellValue(Convert.ToString(renglonDetalleDesempeño["Obser. Sistemas"]));
                renglonActual.CreateCell(30).SetCellValue(Convert.ToString(renglonDetalleDesempeño["Obser. Operaciones"]));
                renglonActual.CreateCell(31).SetCellValue(Convert.ToString(renglonDetalleDesempeño["Obser. Crédito"]));
                renglonActual.CreateCell(32).SetCellValue(Convert.ToString(renglonDetalleDesempeño["Obser. Cliente"]));
                */
                renglonDetalle++;
            }
            #endregion


            //reporte 2
            ISheet sheetResumen = xlsWorkBook.CreateSheet("Resumen");

            //Total de contado
            IFont fontNegrillas = xlsWorkBook.CreateFont();
            ICellStyle fmtNegrillas = xlsWorkBook.CreateCellStyle();

            fontNegrillas.Boldweight = (short)FontBoldWeight.Bold;
            fmtNegrillas.SetFont(fontNegrillas);
            fmtNegrillas.Alignment = HorizontalAlignment.Center;

            IRow renglonResumenTotalContado = sheetResumen.CreateRow(0);
            ICell cellTotalContado = renglonResumenTotalContado.CreateCell(0);
            cellTotalContado.SetCellValue("Total de Contado");
            cellTotalContado.CellStyle = fmtNegrillas;
            sheetResumen.AddMergedRegion(new CellRangeAddress(0, 0, 0, 2));


            #region ANCHOS DE COLUMNA (HOJA RESUMEN)
            sheetResumen.SetColumnWidth(0, ExcelNpoiUtil.AnchoColumna(51));
            sheetResumen.SetColumnWidth(1, ExcelNpoiUtil.AnchoColumna(21));
            sheetResumen.SetColumnWidth(2, ExcelNpoiUtil.AnchoColumna(40));
            sheetResumen.SetColumnWidth(3, ExcelNpoiUtil.AnchoColumna(80));
            sheetResumen.SetColumnWidth(4, ExcelNpoiUtil.AnchoColumna(137));
            sheetResumen.SetColumnWidth(5, ExcelNpoiUtil.AnchoColumna(62));
            sheetResumen.SetColumnWidth(6, ExcelNpoiUtil.AnchoColumna(84));
            sheetResumen.SetColumnWidth(7, ExcelNpoiUtil.AnchoColumna(63));
            sheetResumen.SetColumnWidth(8, ExcelNpoiUtil.AnchoColumna(91));
            sheetResumen.SetColumnWidth(9, ExcelNpoiUtil.AnchoColumna(84));
            #endregion

            //renglon 1 TOTAL DE PEDIDOS
            ICellStyle cellStyleNegrillasSinCentrado = xlsWorkBook.CreateCellStyle();
            IFont fontNegrillaSinCentrado = xlsWorkBook.CreateFont();
            fontNegrillaSinCentrado.Boldweight = (short)FontBoldWeight.Bold;
            cellStyleNegrillasSinCentrado.SetFont(fontNegrillaSinCentrado);
            ICell cellTextoTotalPedidos = renglonResumenTotalContado.CreateCell(4);
            cellTextoTotalPedidos.SetCellValue("TOTAL DE PEDIDOS");
            cellTextoTotalPedidos.CellStyle = cellStyleNegrillasSinCentrado;
            renglonResumenTotalContado.CreateCell(6).SetCellValue(DetalleDesempeño.Rows.Count);


            //renglón 2
            //A2
            IDataFormat dataFormat2Decimales = xlsWorkBook.CreateDataFormat();

            ICellStyle cellStyle2Decimales = xlsWorkBook.CreateCellStyle();
            cellStyle2Decimales.DataFormat = dataFormat2Decimales.GetFormat("##.##");

            IRow renglon2 = sheetResumen.CreateRow(1);
            renglon2.CreateCell(0).SetCellValue(DetalleDesempeño.Rows.Count);
            int sumaDePrendas = 0;
            try { sumaDePrendas = Convert.ToInt32(PedidosContadoPorAgente.Compute("SUM(PRENDAS)", null)); }
            catch { }
            renglon2.CreateCell(1).SetCellValue(sumaDePrendas);
            ICell cellPorcentajeTotalPedidosAgente = renglon2.CreateCell(2);
            cellPorcentajeTotalPedidosAgente.SetCellValue((sumaDePrendas / (double)DetalleDesempeño.Rows.Count) * 100);
            cellPorcentajeTotalPedidosAgente.CellStyle = cellStyle2Decimales;

            //se escriben los agentes
            int renglonPedidoPorAgenteIndex = 2;
            foreach (DataRow renglonPedidoPorAgente in PedidosContadoPorAgente.Rows)
            {
                IRow renglonXlsPedidoAgente = sheetResumen.CreateRow(renglonPedidoPorAgenteIndex);
                renglonXlsPedidoAgente.CreateCell(0).SetCellValue(renglonPedidoPorAgente["AGENTE"].ToString());
                renglonXlsPedidoAgente.CreateCell(1).SetCellValue(Convert.ToInt32(renglonPedidoPorAgente["PRENDAS"]));
                renglonPedidoPorAgenteIndex++;
            }

            //renglón FECHAS CUMPLIDAS
            EscribeTotalesFechas(ref sheetResumen, 2, 4, "FECHAS CUMPLIDAS", TotFechasCumplidas);
            //renglón FECHAS ADELANTADAS
            EscribeTotalesFechas(ref sheetResumen, 3, 4, "FECHAS ADELANTADAS", TotFechasAdelantadas);
            //renglón FECHAS NO CUMPLIDAS
            EscribeTotalesFechas(ref sheetResumen, 5, 4, "FECHAS NO CUMPLIDAS", TotFechasNoCumplidas);
            //renglón FECHAS NO ENTREGADAS
            EscribeTotalesFechas(ref sheetResumen, 6, 4, "FECHAS NO ENTREGADAS", TotFechasNoEntregadas);

            //Escribe resumen por área (TÍTULOS)
            IRow renglonResumenPorArea = sheetResumen.GetRow(8) ?? sheetResumen.CreateRow(8);

            ICell cellResumenPorAreaTitulo = renglonResumenPorArea.CreateCell(4);
            cellResumenPorAreaTitulo.SetCellValue("Desempeño por área");
            cellResumenPorAreaTitulo.CellStyle = fmtNegrillas;
            sheetResumen.AddMergedRegion(new CellRangeAddress(8, 8, 4, 9));

            IRow renglonResumenPorAreaTitulosColumnas = sheetResumen.GetRow(9) ?? sheetResumen.CreateRow(9);
            ICell cellFechasNoCumplidadTitulo = renglonResumenPorAreaTitulosColumnas.CreateCell(4);
            cellFechasNoCumplidadTitulo.SetCellValue("Fechas no Cumplidas");
            cellFechasNoCumplidadTitulo.CellStyle = cellStyleNegrillasSinCentrado;
            ICell cellCantidadTitulo = renglonResumenPorAreaTitulosColumnas.CreateCell(5);
            cellCantidadTitulo.SetCellValue("Cantidad");
            cellCantidadTitulo.CellStyle = cellStyleNegrillasSinCentrado;
            ICell cellPorcentajeTitulo = renglonResumenPorAreaTitulosColumnas.CreateCell(6);
            cellPorcentajeTitulo.SetCellValue("%");
            cellPorcentajeTitulo.CellStyle = fmtNegrillas;
            ICell cellDesgloseTitulo = renglonResumenPorAreaTitulosColumnas.CreateCell(7);
            cellDesgloseTitulo.SetCellValue("Desglose");
            cellDesgloseTitulo.CellStyle = cellStyleNegrillasSinCentrado;
            ICell cellPedidosTitulo = renglonResumenPorAreaTitulosColumnas.CreateCell(8);
            cellPedidosTitulo.SetCellValue(string.Format("Pedidos ({0})", DetalleDesempeño.Rows.Count));
            cellPedidosTitulo.CellStyle = cellStyleNegrillasSinCentrado;
            ICell cellCalifTitulo = renglonResumenPorAreaTitulosColumnas.CreateCell(9);
            cellCalifTitulo.SetCellValue("Calif.");
            cellCalifTitulo.CellStyle = fmtNegrillas;

            //Escribe resumen por área (DETALLE)
            int DesempeñoPorAreaResumenIndex = 11;
            foreach (DataRow renglonDesempeñoPorAreaResumen in DesempeñoPorAreaResumen.Rows)
            {
                IRow renglonXlsDesempeñoPorAreaResumen = sheetResumen.GetRow(DesempeñoPorAreaResumenIndex - 1) ?? sheetResumen.CreateRow(DesempeñoPorAreaResumenIndex - 1);
                renglonXlsDesempeñoPorAreaResumen.CreateCell(4).SetCellValue(renglonDesempeñoPorAreaResumen["Area"].ToString());
                renglonXlsDesempeñoPorAreaResumen.CreateCell(5).SetCellValue(Convert.ToInt32(renglonDesempeñoPorAreaResumen["PedidosNoCumplidos"].ToString()));
                renglonXlsDesempeñoPorAreaResumen.CreateCell(6).SetCellValue(Convert.ToDouble(renglonDesempeñoPorAreaResumen["%NoCumplidos"].ToString()) * 100);
                renglonXlsDesempeñoPorAreaResumen.CreateCell(8).SetCellValue(Convert.ToInt32(renglonDesempeñoPorAreaResumen["PedidosCumplidos"].ToString()));
                renglonXlsDesempeñoPorAreaResumen.CreateCell(9).SetCellValue(Convert.ToDouble(renglonDesempeñoPorAreaResumen["%Cumplidos"].ToString()) * 100);

                DesempeñoPorAreaResumenIndex++;

            }


            for (int i = 0; i < 32; i++)
            {
                sheet.AutoSizeColumn(i);
            }
            if (File.Exists(RutaYNombreArchivo))
            {
                File.Delete(RutaYNombreArchivo);
            }





            FileStream fs = new FileStream(RutaYNombreArchivo, FileMode.CreateNew);

            xlsWorkBook.Write(fs);


            fs.Close();
        }

        private static void EscribeTotalesFechas(ref ISheet Sheet, int NumeroDeRenglon, int NumeroColumna, string TextoEtiqueta, int ValorEtiqueta)
        {

            ICellStyle cellStyleNegrillas = Sheet.Workbook.CreateCellStyle();
            IFont fontNegrilla = Sheet.Workbook.CreateFont();
            fontNegrilla.Boldweight = (short)FontBoldWeight.Bold;
            cellStyleNegrillas.SetFont(fontNegrilla);

            IRow renglon = Sheet.GetRow(NumeroDeRenglon);
            if (renglon == null) { renglon = Sheet.CreateRow(NumeroDeRenglon); }
            ICell cell = renglon.CreateCell(NumeroColumna);
            cell.SetCellValue(TextoEtiqueta);
            cell.CellStyle = cellStyleNegrillas;
            renglon.CreateCell(NumeroColumna + 2).SetCellValue(ValorEtiqueta);
        }

        public static void GrabaDesempeños(DesempByArea des)
        {
            ulp_bl.ESTDPEDI std = new ulp_bl.ESTDPEDI();
            std.ModificarES(des.numeroPedido, des.estandarEspecial);

            List<ulp_bl.DESEMBYAREA> lstDesmbyarea = new List<ulp_bl.DESEMBYAREA>();
            DESEMBYAREA desmbyarea = new DESEMBYAREA();
            lstDesmbyarea = desmbyarea.Consultar(Convert.ToString(des.NumeroPedido));




            foreach (Desempeño desempeño in des.Desempeños)
            {
                DESEMBYAREA desempeñoFinal = new DESEMBYAREA();
                desempeñoFinal.DEPTO = desempeño.AreaStr;
                desempeñoFinal.CUMPLIO = desempeño.CumplioStr;
                desempeñoFinal.OBSERVACIONES = desempeño.Observaciones;
                desempeñoFinal.PEDIDO = Convert.ToString(des.NumeroPedido);
                if (lstDesmbyarea.Count > 0)
                {
                    desmbyarea.Modificar(desempeñoFinal);
                }
                else
                {
                    desmbyarea.Crear(desempeñoFinal);
                }
            }



        }
    }
    public class Desempeño
    {
        public Enumerados.AreasEmpresa Area { set; get; }
        public string AreaStr { set; get; }
        public bool Cumplio { set; get; }
        public string CumplioStr { set; get; }
        public string Observaciones { set; get; }

    }
}
