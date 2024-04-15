using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using sm_dl.SqlServer;
using ulp_dl.SIPNegocio;
using sm_dl;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using ulp_bl.Utiles;
using System.IO;

namespace ulp_bl
{
    public class OrdenProduccionMasiva
    {
        public String Modelo { get; set; }
        public Boolean TieneConfiguracion { get; set; }
        public Configuracion objConfiguracion { get; set; }
        public List<Talla> ListaTallas { get; set; }
        public List<int> ListaPedidos { get; set; }
        public int OrdenProduccion { get; set; }
        public int OrdenMaquila { get; set; }


        public String PedidosString
        {
            get
            {
                String _res = "";
                foreach (int _Pedido in ListaPedidos)
                {
                    _res += _Pedido.ToString() + ",";
                }
                if (_res != "")
                {
                    _res = _res.Substring(0, _res.Length - 1);
                }
                return _res;
            }
        }
        public OrdenProduccionMasiva()
        {
            this.Modelo = "";
            this.TieneConfiguracion = false;
            this.objConfiguracion = new Configuracion();
            this.ListaTallas = new List<Talla> { };
            this.ListaPedidos = new List<int> { };
            this.OrdenProduccion = 0;
            this.OrdenMaquila = 0;
        }


        public class Talla
        {
            public String talla { get; set; }
            public int cantidad { get; set; }
            public Boolean procesar { get; set; }
            public String detalle
            {
                get
                {
                    return "Talla: " + talla + " - Cantidad:" + cantidad.ToString();
                }
            }
            public Talla()
            {
                this.talla = "";
                this.cantidad = 0;
                this.procesar = true;
            }
        }
        public class Configuracion
        {
            public String noProveedor { get; set; }
            public float costo { get; set; }
            public String observacionesOP { get; set; }
            public String observacionesOM { get; set; }
            public Configuracion()
            {
                this.noProveedor = "";
                this.costo = 0;
                this.observacionesOM = "";
                this.observacionesOP = "";
            }
        }



        public static DataTable GetTablaModelosByPedido(int Pedido)
        {
            try
            {
                String conStr = String.Empty;
                DataTable dataTableDescuentos = new DataTable();
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "[usp_ConsultaModelosEspecialesByPedido]";
                cmd.Parameters.Add(new SqlParameter("@pedido", Pedido));
                dataTableDescuentos = cmd.GetDataTable();
                cmd.Connection.Close();
                return dataTableDescuentos;
            }
            catch
            {
                return null;
            }
        }
        public static void GeneraArchivoExcel(string RutaYNombreArchivo, List<OrdenProduccionMasiva> ListaOrdenProduccionMasiva)
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
            fmtCentrado.Alignment = HorizontalAlignment.Center;

            //formato para texto en Negritas
            ICellStyle fmtNegritas = xlsWorkBook.CreateCellStyle();

            #endregion

            #region Encabezado

            IRow rngEncabezado = sheet.CreateRow(0);
            ICell celEncabezado = rngEncabezado.CreateCell(0);
            celEncabezado.SetCellValue("Reporte de Ordenes de Producción Especiales");
            celEncabezado.CellStyle = fmtCentrado;

            //se combinan las celdas
            CellRangeAddress range = new CellRangeAddress(0, 0, 0, 3);
            sheet.AddMergedRegion(range);

            #endregion

            #region Se escribe la fecha de emision

            IRow rngEmitidoDel = sheet.CreateRow(1); //renglón: "Emitido del.."
            rngEmitidoDel.CreateCell(1).SetCellValue(string.Format("Emitido el      {0}", DateTime.Now.ToShortDateString()));



            #endregion

            #region Insertamos encabezados de MODELO, OP, OM

            IRow rngEncabezadoModelos = sheet.CreateRow(3);
            rngEncabezadoModelos.CreateCell(0).SetCellValue("MODELO");
            rngEncabezadoModelos.CreateCell(1).SetCellValue("O.P.");
            rngEncabezadoModelos.CreateCell(2).SetCellValue("O.M.");



            #endregion

            #region Insertamos el detalle de MODELO, OP, OM

            int i = 4;
            foreach (OrdenProduccionMasiva _orden in ListaOrdenProduccionMasiva.Where(x => x.TieneConfiguracion))
            {
                IRow detalleModelo = sheet.CreateRow(i);
                detalleModelo.CreateCell(0).SetCellValue(_orden.Modelo);
                detalleModelo.CreateCell(1).SetCellValue(_orden.OrdenProduccion.ToString());
                detalleModelo.CreateCell(2).SetCellValue(_orden.OrdenMaquila.ToString());

                i++;
            }

            #endregion
            i += 2;

            #region Insertamos Total de Prendas

            IRow totalPrendas = sheet.CreateRow(i);
            totalPrendas.CreateCell(0).SetCellValue("Total Prendas");
            totalPrendas.CreateCell(1).SetCellValue(ListaOrdenProduccionMasiva.Where(x => x.TieneConfiguracion).Sum(x => x.ListaTallas.Where(y => y.procesar).Sum(y => y.cantidad)));

            #endregion

            i += 2;

            #region Insertamos Titulo de Detalle por Modelo
            IRow rngEncabezadoDetalle = sheet.CreateRow(i);
            rngEncabezadoDetalle.CreateCell(1).SetCellValue("DETALLE POR MODELO");
            #endregion

            i += 2;

            #region Insertamos el detalle de tallas y totales para cada moedlo

            foreach (OrdenProduccionMasiva _orden in ListaOrdenProduccionMasiva.Where(x => x.TieneConfiguracion))
            {
                IRow rngDetalleOrdenModelo = sheet.CreateRow(i);
                rngDetalleOrdenModelo.CreateCell(0).SetCellValue("Modelo"); rngDetalleOrdenModelo.CreateCell(1).SetCellValue(_orden.Modelo);
                i++;
                IRow rngDetalleOrdenPedidos = sheet.CreateRow(i);
                rngDetalleOrdenPedidos.CreateCell(0).SetCellValue("Pedidos"); rngDetalleOrdenPedidos.CreateCell(1).SetCellValue(_orden.PedidosString);
                i++;
                IRow rngDetalleOrdenTallas = sheet.CreateRow(i);
                rngDetalleOrdenTallas.CreateCell(0).SetCellValue("Tallas");
                int j = 1;
                foreach (Talla _talla in _orden.ListaTallas.Where(x => x.procesar).OrderBy(x => x.talla))
                {
                    rngDetalleOrdenTallas.CreateCell(j).SetCellValue(_talla.talla);
                    j++;
                }
                //CREAMOS LA COLUMNA DE TOTAL GENERAL POR MODELO
                rngDetalleOrdenTallas.CreateCell(j).SetCellValue("TOTAL");
                j++;


                i++;
                IRow rngDetalleOrdenTotales = sheet.CreateRow(i);
                rngDetalleOrdenTotales.CreateCell(0).SetCellValue("Totales");
                j = 1;
                foreach (Talla _talla in _orden.ListaTallas.Where(x => x.procesar))
                {
                    rngDetalleOrdenTotales.CreateCell(j).SetCellValue(_talla.cantidad);
                    j++;
                }
                rngDetalleOrdenTotales.CreateCell(j).SetCellValue(_orden.ListaTallas.Where(x => x.procesar).Sum(x => x.cantidad));
                j++;
                i += 2;
            }

            #endregion

            sheet.SetColumnWidth(0, ExcelNpoiUtil.AnchoColumna(120));

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
        public static void InsertaPedidoOPMasiva(int Pedido)
        {
            try
            {
                String conStr = String.Empty;
                DataTable dataTableDescuentos = new DataTable();
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "[usp_InsertaPedidoOPMasiva]";
                cmd.Parameters.Add(new SqlParameter("@pedido", Pedido));
                cmd.Execute();
                cmd.Connection.Close();
            }
            catch
            {

            }
        }

        public static DataTable GetTablaModelosByRequisicion(int idRequisicion)
        {
            try
            {
                String conStr = String.Empty;
                DataTable dataTableDescuentos = new DataTable();
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "usp_ConsultaModelosByRequisicion";
                cmd.Parameters.Add(new SqlParameter("@IdRequisicion", idRequisicion));
                dataTableDescuentos = cmd.GetDataTable();
                cmd.Connection.Close();
                return dataTableDescuentos;
            }
            catch
            {
                return null;
            }
        }
    }

}
