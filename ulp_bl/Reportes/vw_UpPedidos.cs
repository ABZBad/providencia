using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using ulp_dl;
using ulp_dl.SIPReportes;

namespace ulp_bl.Reportes
{
    public class vw_UpPedidos
    {
        public DataTable RegresaTablaPedidos(Enumerados.TipoReportePedido TipoReportePedido,DateTime FechaInicial,DateTime FechaFinal,string Vendedor = "")
        {
            DataTable dataTablePedidos = new DataTable();
            using (var DbContext = new SIPReportesContext())
            {
                
                
                if (TipoReportePedido == Enumerados.TipoReportePedido.ConFechaSurtido)
                {
                    if (Vendedor == string.Empty)
                    {
                        var pedidos = from p in DbContext.vw_UpPedidos
                            where
                                DbFunctions.TruncateTime(p.F_CAPT) >= FechaInicial &&
                                DbFunctions.TruncateTime(p.F_CAPT) <= FechaFinal
                            orderby
                                p.PEDIDO
                            select p;
                        dataTablePedidos = Linq2DataTable.CopyToDataTable(pedidos);
                    }
                    else
                    {
                        var pedidos = from p in DbContext.vw_UpPedidos
                                      where
                                          DbFunctions.TruncateTime(p.F_CAPT) >= FechaInicial &&
                                          DbFunctions.TruncateTime(p.F_CAPT) <= FechaFinal && p.VEND == Vendedor
                                      orderby
                                          p.PEDIDO
                                      select p;
                        dataTablePedidos = Linq2DataTable.CopyToDataTable(pedidos);
                    }
                }
                else
                {
                    if (Vendedor == string.Empty)
                    {
                        var pedidos = from p in DbContext.vw_UpPedidos
                            where
                                DbFunctions.TruncateTime(p.F_CAPT) >= FechaInicial &&
                                DbFunctions.TruncateTime(p.F_CAPT) <= FechaFinal
                                &&
                                p.F_SURTIDO == null
                            orderby
                                p.PEDIDO
                            select p;
                        dataTablePedidos = Linq2DataTable.CopyToDataTable(pedidos);
                    }
                    else
                    {
                        var pedidos = from p in DbContext.vw_UpPedidos
                                      where
                                          DbFunctions.TruncateTime(p.F_CAPT) >= FechaInicial &&
                                          DbFunctions.TruncateTime(p.F_CAPT) <= FechaFinal && p.VEND == Vendedor
                                          &&
                                          p.F_SURTIDO == null
                                      orderby
                                          p.PEDIDO
                                      select p;
                        dataTablePedidos = Linq2DataTable.CopyToDataTable(pedidos);
                    }
                }

            }
            return dataTablePedidos;
        }
        private static string FechaCorta(object Fecha)
        {
            DateTime resultadoFecha;
            bool resultado = DateTime.TryParse(Fecha.ToString(), out resultadoFecha);

            if (resultado)
            {
                return resultadoFecha.ToShortDateString();
            }
            else
            {
                return "";
            }
            
            
        }
        public static void GeneraArchivoExcel(string RutaYNombreArchivo, DataTable TablaPedidos)
        {
            HSSFWorkbook xlsWorkBook = new HSSFWorkbook();

            ISheet sheet = xlsWorkBook.CreateSheet("Hoja1");

            #region ENCABEZADOS

            IRow renglonCabezera = sheet.CreateRow(0);
            renglonCabezera.CreateCell(0).SetCellValue("PEDIDO");
            renglonCabezera.CreateCell(1).SetCellValue("Código de Cliente");
            renglonCabezera.CreateCell(2).SetCellValue("Nombre");
            renglonCabezera.CreateCell(3).SetCellValue("F. Entrega");
            renglonCabezera.CreateCell(4).SetCellValue("F. Estandar");
            renglonCabezera.CreateCell(5).SetCellValue("Procesos");
            renglonCabezera.CreateCell(6).SetCellValue("Comentarios");
            renglonCabezera.CreateCell(7).SetCellValue("Num. Prendas");
            renglonCabezera.CreateCell(8).SetCellValue("Agente");
            renglonCabezera.CreateCell(9).SetCellValue("F. Capt");
            renglonCabezera.CreateCell(10).SetCellValue("F. Impresion");
            renglonCabezera.CreateCell(11).SetCellValue("F. Gestión");
            renglonCabezera.CreateCell(12).SetCellValue("F. Capt. Aspel");
            renglonCabezera.CreateCell(13).SetCellValue("F. Credito");
            renglonCabezera.CreateCell(14).SetCellValue("F. Asig. Ruta");
            renglonCabezera.CreateCell(15).SetCellValue("F. Liberado");
            renglonCabezera.CreateCell(16).SetCellValue("F. Surtido");
            renglonCabezera.CreateCell(17).SetCellValue("F. Bordado");
            renglonCabezera.CreateCell(18).SetCellValue("F. Costura");
            renglonCabezera.CreateCell(19).SetCellValue("F. Corte");
            renglonCabezera.CreateCell(20).SetCellValue("F. Estampado");
            renglonCabezera.CreateCell(21).SetCellValue("F. Iniciales");
            renglonCabezera.CreateCell(22).SetCellValue("F. Empaque");
            renglonCabezera.CreateCell(23).SetCellValue("F. Embarque");
            renglonCabezera.CreateCell(24).SetCellValue("F. Pedido");
            renglonCabezera.CreateCell(25).SetCellValue("F. Ruta");
            renglonCabezera.CreateCell(26).SetCellValue("Guia");
            renglonCabezera.CreateCell(27).SetCellValue("Com Surtido");
            renglonCabezera.CreateCell(28).SetCellValue("Com Bordado");
            renglonCabezera.CreateCell(29).SetCellValue("Com Costura");
            renglonCabezera.CreateCell(30).SetCellValue("Com Corte");
            renglonCabezera.CreateCell(31).SetCellValue("Com Estampado");
            renglonCabezera.CreateCell(32).SetCellValue("Com Iniciales");
            renglonCabezera.CreateCell(33).SetCellValue("Com Empaque");


            #endregion

            int renglonIndex = 1; //basado en índice 0
            #region PEDIDOS
            
            foreach (DataRow renglonPedido in TablaPedidos.Rows)
            {
                IRow renglonNpoi = sheet.CreateRow(renglonIndex);

                renglonNpoi.CreateCell(0).SetCellValue(renglonPedido["PEDIDO"].ToString());
                renglonNpoi.CreateCell(1).SetCellValue(renglonPedido["COD_CLIENTE"].ToString().Trim());
                renglonNpoi.CreateCell(2).SetCellValue(renglonPedido["NOMBRE"].ToString());
                renglonNpoi.CreateCell(3).SetCellValue(FechaCorta(renglonPedido["F_VENCIMIENTO"]));
                renglonNpoi.CreateCell(4).SetCellValue(FechaCorta(renglonPedido["F_ESTANDAR"].ToString()));
                renglonNpoi.CreateCell(5).SetCellValue(renglonPedido["PROCESOS"].ToString());
                renglonNpoi.CreateCell(6).SetCellValue(renglonPedido["COMENTARIOS"].ToString());
                renglonNpoi.CreateCell(7).SetCellValue(renglonPedido["NUMERO_PRENDAS"].ToString());
                renglonNpoi.CreateCell(8).SetCellValue(renglonPedido["VEND"].ToString());
                renglonNpoi.CreateCell(9).SetCellValue(FechaCorta(renglonPedido["F_CAPT"]));
                renglonNpoi.CreateCell(10).SetCellValue(FechaCorta(renglonPedido["F_IMPRESION"]));
                renglonNpoi.CreateCell(11).SetCellValue(FechaCorta(renglonPedido["F_GESTION"]));
                renglonNpoi.CreateCell(12).SetCellValue(FechaCorta(renglonPedido["F_CAPT_ASPEL"]));
                renglonNpoi.CreateCell(13).SetCellValue(FechaCorta(renglonPedido["F_CREDITO"]));
                renglonNpoi.CreateCell(14).SetCellValue(FechaCorta(renglonPedido["F_ASIG_RUTA"]));
                renglonNpoi.CreateCell(15).SetCellValue(FechaCorta(renglonPedido["F_LIBERADO"]));
                renglonNpoi.CreateCell(16).SetCellValue(FechaCorta(renglonPedido["F_SURTIDO"]));
                renglonNpoi.CreateCell(17).SetCellValue(FechaCorta(renglonPedido["F_BORDADO"]));
                renglonNpoi.CreateCell(18).SetCellValue(FechaCorta(renglonPedido["F_COSTURA"]));
                renglonNpoi.CreateCell(19).SetCellValue(FechaCorta(renglonPedido["F_CORTE"]));
                renglonNpoi.CreateCell(20).SetCellValue(FechaCorta(renglonPedido["F_ESTAMPADO"]));
                renglonNpoi.CreateCell(21).SetCellValue(FechaCorta(renglonPedido["F_INICIALES"]));
                renglonNpoi.CreateCell(22).SetCellValue(FechaCorta(renglonPedido["F_EMPAQUE"]));
                renglonNpoi.CreateCell(23).SetCellValue(FechaCorta(renglonPedido["F_EMBARQUE"]));
                renglonNpoi.CreateCell(24).SetCellValue(FechaCorta(renglonPedido["FECHAPEDIDO"]));
                renglonNpoi.CreateCell(25).SetCellValue(FechaCorta(renglonPedido["FECHARUTA"]));
                renglonNpoi.CreateCell(26).SetCellValue(renglonPedido["GUIA"].ToString());
                renglonNpoi.CreateCell(27).SetCellValue(renglonPedido["COM_SURTIDO"].ToString());
                renglonNpoi.CreateCell(28).SetCellValue(renglonPedido["COM_BORDADO"].ToString());
                renglonNpoi.CreateCell(29).SetCellValue(renglonPedido["COM_COSTURA"].ToString());
                renglonNpoi.CreateCell(30).SetCellValue(renglonPedido["COM_CORTE"].ToString());
                renglonNpoi.CreateCell(31).SetCellValue(renglonPedido["COM_ESTAMPADO"].ToString());
                renglonNpoi.CreateCell(32).SetCellValue(renglonPedido["COM_INICIALES"].ToString());
                renglonNpoi.CreateCell(33).SetCellValue(renglonPedido["COM_EMPAQUE"].ToString());

                
                renglonIndex++;
            }
            #endregion

            for (int i = 0; i < 42; i++)
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
    }
}
