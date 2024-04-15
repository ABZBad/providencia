using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using ulp_dl;
using ulp_dl.SIPReportes;
using sm_dl.SqlServer;

namespace ulp_bl.Reportes
{
    public class vw_UpPedidosSae
    {
        #region propiedades
        public int ID { get; set; }
        public double PEDIDO { get; set; }
        public string COD_CLIENTE { get; set; }
        public Nullable<System.DateTime> F_VENCIMIENTO { get; set; }
        public Nullable<System.DateTime> F_VENCIMIENTO2 { get; set; }
        public Nullable<System.DateTime> F_ESTANDAR { get; set; }
        public string PROCESOS { get; set; }
        public string COMENTARIOS { get; set; }
        public Nullable<System.DateTime> F_CAPT { get; set; }
        public Nullable<System.DateTime> F_IMPRESION { get; set; }
        public Nullable<System.DateTime> F_GESTION { get; set; }
        public Nullable<System.DateTime> F_CAPT_ASPEL { get; set; }
        public Nullable<System.DateTime> F_CREDITO { get; set; }
        public Nullable<System.DateTime> F_ASIG_RUTA { get; set; }
        public Nullable<System.DateTime> F_LIBERADO { get; set; }
        public Nullable<System.DateTime> F_SURTIDO { get; set; }
        public Nullable<System.DateTime> F_BORDADO { get; set; }
        public Nullable<System.DateTime> F_COSTURA { get; set; }
        public Nullable<System.DateTime> F_CORTE { get; set; }
        public Nullable<System.DateTime> F_ESTAMPADO { get; set; }
        public Nullable<System.DateTime> F_INICIALES { get; set; }
        public Nullable<System.DateTime> F_EMPAQUE { get; set; }
        public Nullable<System.DateTime> F_EMBARQUE { get; set; }
        public Nullable<System.DateTime> FECHAPEDIDO { get; set; }
        public Nullable<System.DateTime> FECHARUTA { get; set; }
        public string GUIA { get; set; }
        public Nullable<int> CAJAS { get; set; }
        public string CHOFER { get; set; }
        public string DEPARTAMENTO { get; set; }
        public string DESTINO { get; set; }
        public string OBSERVACIONES { get; set; }
        public string ESTATUS { get; set; }
        public string TRANSPORTE { get; set; }
        public string COM_SURTIDO { get; set; }
        public string COM_BORDADO { get; set; }
        public string COM_COSTURA { get; set; }
        public string COM_CORTE { get; set; }
        public string COM_ESTAMPADO { get; set; }
        public string COM_INICIALES { get; set; }
        public string COM_EMPAQUE { get; set; }
        public string COM_CREDITO { get; set; }
        public string ESTATUS_UPPEDIDOS { get; set; }
        public Nullable<System.DateTime> F_ENTREGADO { get; set; }
        public string COM_ENTREGA { get; set; }
        public int NUMERO_PRENDAS { get; set; }
        public string NOMBRE { get; set; }
        public string VEND { get; set; }
        #endregion
        public DataTable RegresaTablaPedidos(bool SinFechaSurtido, bool SinFechaEntregado, DateTime FechaInicial, DateTime FechaFinal, string Vendedor = "")
        {
            System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
            DataTable dataTablePedidos = new DataTable();
            using (var DbContext = new SIPReportesContext())
            {
                SqlServerCommand _cmd = new SqlServerCommand();
                _cmd.Connection = sm_dl.DALUtil.GetConnection(DbContext.Database.Connection.ConnectionString);
                _cmd.ObjectName = "usp_UpPedidosFechaSAE";
                _cmd.Parameters.Add(new SqlParameter("@fecha_inicial", FechaInicial));
                _cmd.Parameters.Add(new SqlParameter("@fecha_final", FechaFinal));
                _cmd.Parameters.Add(new SqlParameter("@sinFechaSurtido", SinFechaSurtido));
                _cmd.Parameters.Add(new SqlParameter("@sinFechaEntregado", SinFechaEntregado));
                _cmd.Parameters.Add(new SqlParameter("@vendedor", ""));
                dataTablePedidos = _cmd.GetDataTable();
                _cmd.Connection.Close();

            }
            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.ElapsedMilliseconds);
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
        private static string FechaHora(object Fecha)
        {
            DateTime resultadoFecha;
            bool resultado = DateTime.TryParse(Fecha.ToString(), out resultadoFecha);

            if (resultado)
            {
                return resultadoFecha.ToString("dd/MM/yyyy HH:mm:ss");
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
            int iEncabezado = 0;

            IRow renglonCabezera = sheet.CreateRow(0);
            renglonCabezera.CreateCell(iEncabezado).SetCellValue("PEDIDO"); iEncabezado++;
            renglonCabezera.CreateCell(iEncabezado).SetCellValue("Código de Cliente"); iEncabezado++;
            renglonCabezera.CreateCell(iEncabezado).SetCellValue("Nombre"); iEncabezado++;
            renglonCabezera.CreateCell(iEncabezado).SetCellValue("F. Entrega"); iEncabezado++;
            renglonCabezera.CreateCell(iEncabezado).SetCellValue("F. Estandar"); iEncabezado++;
            renglonCabezera.CreateCell(iEncabezado).SetCellValue("Procesos"); iEncabezado++;
            renglonCabezera.CreateCell(iEncabezado).SetCellValue("Comentarios"); iEncabezado++;
            renglonCabezera.CreateCell(iEncabezado).SetCellValue("Num. Prendas"); iEncabezado++;
            renglonCabezera.CreateCell(iEncabezado).SetCellValue("Agente"); iEncabezado++;
            renglonCabezera.CreateCell(iEncabezado).SetCellValue("F. Capt"); iEncabezado++;
            renglonCabezera.CreateCell(iEncabezado).SetCellValue("F. GV"); iEncabezado++;
            renglonCabezera.CreateCell(iEncabezado).SetCellValue("F. CP"); iEncabezado++;
            renglonCabezera.CreateCell(iEncabezado).SetCellValue("F. Gestión"); iEncabezado++;
            renglonCabezera.CreateCell(iEncabezado).SetCellValue("F. Capt. Aspel"); iEncabezado++;
            renglonCabezera.CreateCell(iEncabezado).SetCellValue("F. Credito"); iEncabezado++;
            renglonCabezera.CreateCell(iEncabezado).SetCellValue("F. Asig. Ruta"); iEncabezado++;
            renglonCabezera.CreateCell(iEncabezado).SetCellValue("F. Dirección"); iEncabezado++;
            renglonCabezera.CreateCell(iEncabezado).SetCellValue("F. Impresion"); iEncabezado++;
            renglonCabezera.CreateCell(iEncabezado).SetCellValue("F. Liberado"); iEncabezado++;
            renglonCabezera.CreateCell(iEncabezado).SetCellValue("F. Surtido"); iEncabezado++;
            renglonCabezera.CreateCell(iEncabezado).SetCellValue("F. Bordado"); iEncabezado++;
            renglonCabezera.CreateCell(iEncabezado).SetCellValue("F. Costura"); iEncabezado++;
            renglonCabezera.CreateCell(iEncabezado).SetCellValue("F. Corte"); iEncabezado++;
            renglonCabezera.CreateCell(iEncabezado).SetCellValue("F. Estampado"); iEncabezado++;
            renglonCabezera.CreateCell(iEncabezado).SetCellValue("F. Iniciales"); iEncabezado++;
            renglonCabezera.CreateCell(iEncabezado).SetCellValue("F. Empaque"); iEncabezado++;
            renglonCabezera.CreateCell(iEncabezado).SetCellValue("F. Embarque"); iEncabezado++;
            renglonCabezera.CreateCell(iEncabezado).SetCellValue("F. Pedido"); iEncabezado++;
            renglonCabezera.CreateCell(iEncabezado).SetCellValue("F. Ruta"); iEncabezado++;
            renglonCabezera.CreateCell(iEncabezado).SetCellValue("Guia"); iEncabezado++;
            renglonCabezera.CreateCell(iEncabezado).SetCellValue("Com Surtido"); iEncabezado++;
            renglonCabezera.CreateCell(iEncabezado).SetCellValue("Com Bordado"); iEncabezado++;
            renglonCabezera.CreateCell(iEncabezado).SetCellValue("Com Costura"); iEncabezado++;
            renglonCabezera.CreateCell(iEncabezado).SetCellValue("Com Corte"); iEncabezado++;
            renglonCabezera.CreateCell(iEncabezado).SetCellValue("Com Estampado"); iEncabezado++;
            renglonCabezera.CreateCell(iEncabezado).SetCellValue("Com Iniciales"); iEncabezado++;
            renglonCabezera.CreateCell(iEncabezado).SetCellValue("Com Empaque"); iEncabezado++;


            #endregion

            int renglonIndex = 1; //basado en índice 0
            int iDetalle = 0;
            #region PEDIDOS

            foreach (DataRow renglonPedido in TablaPedidos.Rows)
            {
                IRow renglonNpoi = sheet.CreateRow(renglonIndex);
                iDetalle = 0;
                renglonNpoi.CreateCell(iDetalle).SetCellValue(renglonPedido["PEDIDO"].ToString()); iDetalle++;
                renglonNpoi.CreateCell(iDetalle).SetCellValue(renglonPedido["COD_CLIENTE"].ToString().Trim()); iDetalle++;
                renglonNpoi.CreateCell(iDetalle).SetCellValue(renglonPedido["NOMBRE"].ToString()); iDetalle++;
                renglonNpoi.CreateCell(iDetalle).SetCellValue(FechaHora(renglonPedido["F_VENCIMIENTO"])); iDetalle++;
                renglonNpoi.CreateCell(iDetalle).SetCellValue(FechaHora(renglonPedido["F_ESTANDAR"].ToString())); iDetalle++;
                renglonNpoi.CreateCell(iDetalle).SetCellValue(renglonPedido["PROCESOS"].ToString()); iDetalle++;
                renglonNpoi.CreateCell(iDetalle).SetCellValue(renglonPedido["COMENTARIOS"].ToString()); iDetalle++;
                renglonNpoi.CreateCell(iDetalle).SetCellValue(Convert.ToDouble(renglonPedido["TOT_PEDIDOS"].ToString())); iDetalle++;
                renglonNpoi.CreateCell(iDetalle).SetCellValue(renglonPedido["VEND"].ToString()); iDetalle++;
                renglonNpoi.CreateCell(iDetalle).SetCellValue(FechaHora(renglonPedido["F_CAPT"])); iDetalle++;
                renglonNpoi.CreateCell(iDetalle).SetCellValue(FechaHora(renglonPedido["F_GV"])); iDetalle++;
                renglonNpoi.CreateCell(iDetalle).SetCellValue(FechaHora(renglonPedido["F_COORDINADOR"])); iDetalle++;
                renglonNpoi.CreateCell(iDetalle).SetCellValue(FechaHora(renglonPedido["F_GESTION"])); iDetalle++;
                renglonNpoi.CreateCell(iDetalle).SetCellValue(FechaHora(renglonPedido["F_CAPT_ASPEL"])); iDetalle++;
                renglonNpoi.CreateCell(iDetalle).SetCellValue(FechaHora(renglonPedido["F_CREDITO"])); iDetalle++;
                renglonNpoi.CreateCell(iDetalle).SetCellValue(FechaHora(renglonPedido["F_ASIG_RUTA"])); iDetalle++;
                renglonNpoi.CreateCell(iDetalle).SetCellValue(FechaHora(renglonPedido["F_DIRECCION"])); iDetalle++;
                renglonNpoi.CreateCell(iDetalle).SetCellValue(FechaHora(renglonPedido["F_IMPRESION"])); iDetalle++;
                renglonNpoi.CreateCell(iDetalle).SetCellValue(FechaHora(renglonPedido["F_LIBERADO"])); iDetalle++;
                renglonNpoi.CreateCell(iDetalle).SetCellValue(FechaHora(renglonPedido["F_SURTIDO"])); iDetalle++;
                renglonNpoi.CreateCell(iDetalle).SetCellValue(FechaHora(renglonPedido["F_BORDADO"])); iDetalle++;
                renglonNpoi.CreateCell(iDetalle).SetCellValue(FechaHora(renglonPedido["F_COSTURA"])); iDetalle++;
                renglonNpoi.CreateCell(iDetalle).SetCellValue(FechaHora(renglonPedido["F_CORTE"])); iDetalle++;
                renglonNpoi.CreateCell(iDetalle).SetCellValue(FechaHora(renglonPedido["F_ESTAMPADO"])); iDetalle++;
                renglonNpoi.CreateCell(iDetalle).SetCellValue(FechaHora(renglonPedido["F_INICIALES"])); iDetalle++;
                renglonNpoi.CreateCell(iDetalle).SetCellValue(FechaHora(renglonPedido["F_EMPAQUE"])); iDetalle++;
                renglonNpoi.CreateCell(iDetalle).SetCellValue(FechaHora(renglonPedido["F_EMBARQUE"])); iDetalle++;
                renglonNpoi.CreateCell(iDetalle).SetCellValue(FechaHora(renglonPedido["FECHAPEDIDO"])); iDetalle++;
                renglonNpoi.CreateCell(iDetalle).SetCellValue(FechaHora(renglonPedido["FECHARUTA"])); iDetalle++;
                renglonNpoi.CreateCell(iDetalle).SetCellValue(renglonPedido["GUIA"].ToString()); iDetalle++;
                renglonNpoi.CreateCell(iDetalle).SetCellValue(renglonPedido["COM_SURTIDO"].ToString()); iDetalle++;
                renglonNpoi.CreateCell(iDetalle).SetCellValue(renglonPedido["COM_BORDADO"].ToString()); iDetalle++;
                renglonNpoi.CreateCell(iDetalle).SetCellValue(renglonPedido["COM_COSTURA"].ToString()); iDetalle++;
                renglonNpoi.CreateCell(iDetalle).SetCellValue(renglonPedido["COM_CORTE"].ToString()); iDetalle++;
                renglonNpoi.CreateCell(iDetalle).SetCellValue(renglonPedido["COM_ESTAMPADO"].ToString()); iDetalle++;
                renglonNpoi.CreateCell(iDetalle).SetCellValue(renglonPedido["COM_INICIALES"].ToString()); iDetalle++;
                renglonNpoi.CreateCell(iDetalle).SetCellValue(renglonPedido["COM_EMPAQUE"].ToString()); iDetalle++;


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
