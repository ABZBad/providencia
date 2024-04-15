using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ulp_dl.aspel_sae80;
using ulp_bl.Utiles;
using ulp_dl.SIPNegocio;
using sm_dl;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;


namespace ulp_bl
{
    public class ControlPedidos
    {
        public static DataTable getAreasSIP(Boolean soloFlujo = true)
        {
            //usp_ConsultaAreas
            DataTable dt = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.Parameters.Add(new SqlParameter("@flujo", soloFlujo));
                cmdDatos.ObjectName = "usp_ConsultaAreas";
                dt = cmdDatos.GetDataTable();
            }
            return dt;

        }
        public static DataTable getEstatusSIP()
        {
            //usp_ConsultaAreas
            DataTable dt = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "usp_ConsultaEstatus";
                dt = cmdDatos.GetDataTable();
            }
            return dt;

        }
        public static DataTable getPedidosPorArea(String _ClaveArea, String _Usuario)
        {
            //usp_ConsultaAreas
            DataTable dt = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "usp_getPedidos_Area";
                cmdDatos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ClaveArea", _ClaveArea));
                cmdDatos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Usuario", _Usuario));
                dt = cmdDatos.GetDataTable();
            }
            return dt;

        }
        public static DataTable getHistoricoPedido(String _Pedido)
        {
            //usp_ConsultaAreas
            DataTable dt = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "usp_getHistoricoPedido";
                cmdDatos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Pedido", _Pedido));
                dt = cmdDatos.GetDataTable();
            }
            return dt;

        }
        public static DataTable getObservacionesPedido(String _Pedido)
        {
            //usp_ConsultaAreas
            DataTable dt = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "usp_getObservacionesPedido";
                cmdDatos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Pedido", _Pedido));
                dt = cmdDatos.GetDataTable();
            }
            return dt;

        }
        public static DataTable getListaMenus(String _ClaveArea, int _OrdenAgrupador, int _tipoProceso)
        {
            //usp_GetListaMenusUPPedidos
            DataTable dt = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "usp_GetListaMenusUPPedidos";
                cmdDatos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Area", _ClaveArea));
                cmdDatos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OrdenAgrupador", _OrdenAgrupador));
                cmdDatos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@tipoProceso", _tipoProceso));
                dt = cmdDatos.GetDataTable();
            }
            return dt;

        }
        public static DataTable getSiguienteIDProceso()
        {
            //usp_ConsultaAreas
            DataTable dt = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "usp_GetUltimoIDProceso";
                dt = cmdDatos.GetDataTable();
            }
            return dt;

        }
        public static void setLineaTiempoPedido(int _Pedido, String _ClaveArea, string _Estatus, int _OrdenAgrupador, string _Observaciones, string _usuario, int _cveTipoProceso, int _referenciaProceso, String _Cliente, ref Exception _ex)
        {
            try
            {
                //usp_GetListaMenusUPPedidos
                using (var dbContext = new SIPNegocioContext())
                {
                    sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                    cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                    cmdDatos.ObjectName = "usp_setLineaTiempoUPPedidos";
                    cmdDatos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ID", _Pedido));
                    cmdDatos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Area", _ClaveArea));
                    cmdDatos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Estatus", _Estatus));
                    cmdDatos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OrdenAgrupador", _OrdenAgrupador));
                    cmdDatos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Observaciones", _Observaciones));
                    cmdDatos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Usuario", _usuario));
                    cmdDatos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CveTipoProceso", _cveTipoProceso));
                    cmdDatos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ReferenciaProceso", _referenciaProceso));
                    cmdDatos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Cliente", _Cliente));
                    cmdDatos.Execute();
                }
            }
            catch (Exception ex)
            {
                _ex = ex;
            }
        }
        public static void setAltaLineaTiempoPedido(int _Pedido, String _ClaveArea, string _Estatus, int _OrdenAgrupador, string _Observaciones, string _usuario, int _cveTipoProceso, int _referenciaProceso, String _Cliente, ref Exception _ex)
        {
            try
            {
                //usp_GetListaMenusUPPedidos
                using (var dbContext = new SIPNegocioContext())
                {
                    sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                    cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                    cmdDatos.ObjectName = "usp_setAltaLineaTiempoUPPedidos";
                    cmdDatos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ID", _Pedido));
                    cmdDatos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Area", _ClaveArea));
                    cmdDatos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Estatus", _Estatus));
                    cmdDatos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OrdenAgrupador", _OrdenAgrupador));
                    cmdDatos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Observaciones", _Observaciones));
                    cmdDatos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Usuario", _usuario));
                    cmdDatos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CveTipoProceso", _cveTipoProceso));
                    cmdDatos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ReferenciaProceso", _referenciaProceso));
                    cmdDatos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Cliente", _Cliente));
                    cmdDatos.Execute();
                }
            }
            catch (Exception ex)
            {
                _ex = ex;
            }
        }
        public static int getReferenciaProceso(int _ID, int _TipoProceso)
        {
            DataTable dt = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "usp_getReferenciaProceso";
                cmdDatos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Id", _ID));
                cmdDatos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ClaveTipoProceso", _TipoProceso));
                dt = cmdDatos.GetDataTable();

                if (dt.Rows.Count > 0)
                    return int.Parse(dt.Rows[0]["ReferenciaProceso"].ToString());
                else
                    return 0;
            }
        }
        public static DataTable getSolicitudesPedido(int _Pedido)
        {
            DataTable dt = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "usp_getSolicitudesPedido";
                cmdDatos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Pedido", _Pedido));
                dt = cmdDatos.GetDataTable();
                return dt;
            }
        }
        public static void setSolicitudesToPedido(int _idPedido, List<int> _Solicitudes)
        {
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "usp_setSolicitudToPedido";
                foreach (int _Solicitud in _Solicitudes)
                {
                    cmdDatos.Parameters.Clear();
                    cmdDatos.Parameters.Add(new SqlParameter("@Pedido", _idPedido));
                    cmdDatos.Parameters.Add(new SqlParameter("@Solicitud", _Solicitud));
                    cmdDatos.Execute();
                }
            }
        }
        public static void setFinSolicitudes(List<int> _Solicitudes)
        {
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "usp_setFinSolicitud";
                foreach (int _Solicitud in _Solicitudes)
                {
                    cmdDatos.Parameters.Clear();
                    cmdDatos.Parameters.Add(new SqlParameter("@Solicitud", _Solicitud));
                    cmdDatos.Execute();
                }
            }
        }
        public static void setFinPedido(int _Pedido)
        {
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "usp_setFinPedido";
                cmdDatos.Parameters.Add(new SqlParameter("@Pedido", _Pedido));
                cmdDatos.Execute();
            }
        }
        public static void setCancelaPedido(int _Pedido)
        {
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "usp_setCancelaPedido";
                cmdDatos.Parameters.Add(new SqlParameter("@Pedido", _Pedido));
                cmdDatos.Execute();
            }
        }
        public static void setAutorizacionPedido(int _Pedido, String _ClaveArea, string _Usuario, ref Exception _ex, Boolean _aplicaFirma = false)
        {
            try
            {
                using (var dbContext = new SIPNegocioContext())
                {
                    sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                    cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                    cmdDatos.ObjectName = "usp_setAutorizacionPedido";
                    cmdDatos.Parameters.Add(new SqlParameter("@Pedido", _Pedido));
                    cmdDatos.Parameters.Add(new SqlParameter("@ClaveArea", _ClaveArea));
                    cmdDatos.Parameters.Add(new SqlParameter("@Usuario", _Usuario));
                    cmdDatos.Parameters.Add(new SqlParameter("@AplicaFirma", _aplicaFirma));
                    cmdDatos.Execute();
                }
            }
            catch (Exception ex)
            {
                _ex = ex;
            }
        }
        public static void setCancelaAutorizacionPedido(int _Pedido, String _ClaveArea, string _Usuario, String _ClaveAreaInicial, ref Exception _ex)
        {
            try
            {
                using (var dbContext = new SIPNegocioContext())
                {
                    sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                    cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                    cmdDatos.ObjectName = "usp_setCancelaAutorizacionPedido";
                    cmdDatos.Parameters.Add(new SqlParameter("@Pedido", _Pedido));
                    cmdDatos.Parameters.Add(new SqlParameter("@ClaveArea", _ClaveArea));
                    cmdDatos.Parameters.Add(new SqlParameter("@Usuario", _Usuario));
                    cmdDatos.Parameters.Add(new SqlParameter("@ClaveAreaInicial", _ClaveAreaInicial));
                    cmdDatos.Execute();
                }
            }
            catch (Exception ex)
            {
                _ex = ex;
            }
        }
        public static DataTable getAutorizacionPedido(int _Pedido)
        {
            //usp_ConsultaAreas
            DataTable dt = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "usp_getAutorizacionPedido";
                cmdDatos.Parameters.Add(new SqlParameter("@Pedido", _Pedido));
                dt = cmdDatos.GetDataTable();
            }
            return dt;

        }
        public static Boolean getSolicitudEspecialpedido(int _Pedido)
        {
            try
            {
                DataTable dt = new DataTable();
                using (var dbContext = new SIPNegocioContext())
                {
                    sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                    cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                    cmdDatos.ObjectName = "usp_getSolicitudEspecialPedido";
                    cmdDatos.Parameters.Add(new SqlParameter("@Pedido", _Pedido));
                    dt = cmdDatos.GetDataTable();
                    return Boolean.Parse(dt.Rows[0]["IMPRIMIR"].ToString());
                }
            }
            catch
            {
                return false;
            }


        }
        public static DataTable getUsuariosEspeciales()
        {
            DataTable dt = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "usp_ConsultaUsuariosEspecialesControlPedidos";
                dt = cmdDatos.GetDataTable();
            }
            return dt;

        }
        public static void setUsuariosEspeciales(String _usuarios, ref Exception _ex)
        {
            try
            {
                using (var dbContext = new SIPNegocioContext())
                {
                    sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                    cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                    cmdDatos.ObjectName = "usp_AltaUsuariosEspecialesControlPedidos";
                    cmdDatos.Parameters.Add(new SqlParameter("@Usuarios", _usuarios));
                    cmdDatos.Execute();
                }
            }
            catch (Exception ex)
            {
                _ex = ex;
            }
        }
        public static void setAltaDocumentoElectronico(int _pedido, byte[] _file, string _tipo, string _nombreDocumento, string _observaciones, string _usuario)
        {
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "usp_setAltaDocumentoElectronico";

                cmdDatos.Parameters.Add(new SqlParameter("@pedido", _pedido));
                cmdDatos.Parameters.Add(new SqlParameter("@file", _file));
                cmdDatos.Parameters.Add(new SqlParameter("@tipo", _tipo));
                cmdDatos.Parameters.Add(new SqlParameter("@nombreDocumento", _nombreDocumento));
                cmdDatos.Parameters.Add(new SqlParameter("@observaciones", _observaciones));
                cmdDatos.Parameters.Add(new SqlParameter("@usuario", _usuario));
                cmdDatos.Execute();
            }
        }
        public static DataTable getDocumentosElectronicos(int _Pedido)
        {
            //usp_ConsultaAreas
            DataTable dt = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "usp_getDocumentosElectronicos";
                cmdDatos.Parameters.Add(new SqlParameter("@pedido", _Pedido));
                dt = cmdDatos.GetDataTable();
            }
            return dt;

        }
        public static DataTable setEliminaDocumento(int _Pedido, int idDocumento)
        {
            //usp_ConsultaAreas
            DataTable dt = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "usp_setEliminaDocumentoElectronico";
                cmdDatos.Parameters.Add(new SqlParameter("@pedido", _Pedido));
                cmdDatos.Parameters.Add(new SqlParameter("@idDocumento", idDocumento));
                cmdDatos.Execute();
            }
            return dt;

        }
        public static DataTable getLineaTiempo(int _Pedido)
        {
            DataTable dt = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "usp_ConsultaLineaTiempoUPPedidos";
                cmdDatos.Parameters.Add(new SqlParameter("@ReferenciaProceso", _Pedido));
                dt = cmdDatos.GetDataTable();
            }
            return dt;
        }
        public static DataTable getPedidosPorSolicitud(String _solicitudes)
        {
            DataTable dt = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "usp_GetPedidosPorSolicitud";
                cmdDatos.Parameters.Add(new SqlParameter("@Solicitudes", _solicitudes));
                dt = cmdDatos.GetDataTable();
            }
            return dt;
        }
        public static DataTable getReportePrendasAutorizadas()
        {
            DataTable dt = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "usp_GetReportePrendasAutorizadas";
                dt = cmdDatos.GetDataTable();
            }
            return dt;
        }
        public static void GeneraArchivoExcelPrendasAutorizadas(string RutaYNombreArchivo, DataTable TablaPedidos)
        {

            HSSFWorkbook xlsWorkBook = new HSSFWorkbook();

            ISheet sheet = xlsWorkBook.CreateSheet("Hoja1");

            #region ENCABEZADOS

            IRow renglonTitulo = sheet.CreateRow(0);
            renglonTitulo.CreateCell(2).SetCellValue(String.Format("REPORTE DE PRENDAS AUTORIZADAS POR DIRECCIÓN AL {0}", DateTime.Now.ToString("dd/MM/yyyy")));

            IRow renglonCabezera = sheet.CreateRow(2);
            renglonCabezera.CreateCell(0).SetCellValue("PEDIDOS");
            renglonCabezera.CreateCell(1).SetCellValue("CLAVE");
            renglonCabezera.CreateCell(2).SetCellValue("CLIENTE");
            renglonCabezera.CreateCell(3).SetCellValue("TOTAL PRENDAS");



            #endregion

            int renglonIndex = 3; //basado en índice 0
            #region Ventas

            ICellStyle celdaEstiloSUM = xlsWorkBook.CreateCellStyle();
            celdaEstiloSUM = xlsWorkBook.CreateCellStyle();
            celdaEstiloSUM.DataFormat = HSSFDataFormat.GetBuiltinFormat("(0);(0)");

            ICellStyle indicadorStyle = xlsWorkBook.CreateCellStyle();
            indicadorStyle = xlsWorkBook.CreateCellStyle();
            indicadorStyle.FillForegroundColor = IndexedColors.Yellow.Index;
            indicadorStyle.FillPattern = FillPattern.SolidForeground;

            ICellStyle indicador2Style = xlsWorkBook.CreateCellStyle();
            indicador2Style = xlsWorkBook.CreateCellStyle();
            indicador2Style.FillForegroundColor = IndexedColors.Orange.Index;
            indicador2Style.FillPattern = FillPattern.SolidForeground;

            foreach (DataRow renglon in TablaPedidos.Rows)
            {
                IRow renglonDetalle = sheet.CreateRow(renglonIndex);

                renglonDetalle.CreateCell(0).SetCellValue(renglon["PEDIDO"].ToString().Trim());
                renglonDetalle.CreateCell(1).SetCellValue(renglon["CLIENTE"].ToString().Trim());
                renglonDetalle.CreateCell(2).SetCellValue(renglon["NOMBRE"].ToString().Trim());
                renglonDetalle.CreateCell(3).SetCellValue((int)renglon["PRENDAS"]);

                renglonIndex++;
            }
            #endregion




            //Totales
            IRow RowTotal = sheet.CreateRow(renglonIndex + 1);

            // Total de pesos
            RowTotal.CreateCell(2).SetCellValue("TOTAL");
            ICell Total = RowTotal.CreateCell(3);
            Total.CellFormula = string.Format("SUM(D3:D" + renglonIndex.ToString() + ")");
            Total.CellStyle = celdaEstiloSUM;


            for (int i = 0; i < 4; i++)
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
        public static DataTable getReportePedidosFaltantes()
        {
            DataTable dt = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "usp_GeneraReportePedidosFaltantes";
                dt = cmdDatos.GetDataTable();
            }
            return dt;
        }
        public static void GeneraArchivoExcelPedidosFaltantes(string RutaYNombreArchivo, DataTable TablaPedidos)
        {

            HSSFWorkbook xlsWorkBook = new HSSFWorkbook();

            ISheet sheet = xlsWorkBook.CreateSheet("Hoja1");

            #region ENCABEZADOS

            IRow renglonTitulo = sheet.CreateRow(0);
            renglonTitulo.CreateCell(2).SetCellValue(String.Format("REPORTE DE PEDIDOS FALTANTES AL {0}", DateTime.Now.ToString("dd/MM/yyyy")));

            IRow renglonCabezera = sheet.CreateRow(2);
            renglonCabezera.CreateCell(0).SetCellValue("PEDIDO");
            renglonCabezera.CreateCell(1).SetCellValue("MODELO");
            renglonCabezera.CreateCell(2).SetCellValue("CANTIDAD");

            #endregion

            int renglonIndex = 3; //basado en índice 0
            #region Ventas

            ICellStyle celdaEstiloSUM = xlsWorkBook.CreateCellStyle();
            celdaEstiloSUM = xlsWorkBook.CreateCellStyle();
            celdaEstiloSUM.DataFormat = HSSFDataFormat.GetBuiltinFormat("(0);(0)");

            ICellStyle indicadorStyle = xlsWorkBook.CreateCellStyle();
            indicadorStyle = xlsWorkBook.CreateCellStyle();
            indicadorStyle.FillForegroundColor = IndexedColors.Yellow.Index;
            indicadorStyle.FillPattern = FillPattern.SolidForeground;

            ICellStyle indicador2Style = xlsWorkBook.CreateCellStyle();
            indicador2Style = xlsWorkBook.CreateCellStyle();
            indicador2Style.FillForegroundColor = IndexedColors.Orange.Index;
            indicador2Style.FillPattern = FillPattern.SolidForeground;

            foreach (DataRow renglon in TablaPedidos.Rows)
            {
                IRow renglonDetalle = sheet.CreateRow(renglonIndex);

                renglonDetalle.CreateCell(0).SetCellValue(renglon["PEDIDO"].ToString().Trim());
                renglonDetalle.CreateCell(1).SetCellValue(renglon["CODIGO"].ToString().Trim());
                renglonDetalle.CreateCell(2).SetCellValue((int)renglon["CANTIDAD"]);

                renglonIndex++;
            }
            #endregion


            for (int i = 0; i < 3; i++)
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
        public static void GeneraNotificacionPedidoExistencias(int _Pedido)
        {
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "usp_setNotificaPedidoExistencias";
                cmdDatos.Parameters.Add(new SqlParameter("@Pedido", _Pedido));
                cmdDatos.Execute();
            }
        }
    }
}
