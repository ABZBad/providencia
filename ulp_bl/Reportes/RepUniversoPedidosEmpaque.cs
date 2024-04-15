using System;
using System.Data;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using sm_dl;
using sm_dl.SqlServer;
using ulp_dl.SIPReportes;

namespace ulp_bl.Reportes
{
    public class RepUniversoPedidosEmpaque
    {
        public static DataTable RegresaUniversoPedidosEmpaque()
        {
            DataTable dataTableUniversoPedidosEmpaque = new DataTable();
            string conStr = "";
            using (var dbContext = new SIPReportesContext())
            {
                conStr = dbContext.Database.Connection.ConnectionString;
            }
            SqlServerCommand cmd = new SqlServerCommand();
            cmd.Connection = DALUtil.GetConnection(conStr);
            cmd.ObjectName = "usp_RepUniversoPedidosEmpaque";
            dataTableUniversoPedidosEmpaque = cmd.GetDataTable();
            return dataTableUniversoPedidosEmpaque;
        }
        public static void GeneraArchivoExcel(DataTable UniversoPedidosEmpaque, string RutaYNombreArchivo)
        {
            HSSFWorkbook xlsWorkBook = new HSSFWorkbook();
            ISheet sheet = xlsWorkBook.CreateSheet("Hoja1");

            //variables de control
            int iColumnaInicialReporte = 1;
            int iRenglonInicialDetalle = 7;
            //se asigna título del reporte
            #region TÍTULO DEL REPORTE

            string tituloReporte = "Universo de Pedidos en Embarque";
           
            #endregion
            #region ENCABEZADOS

            //creación del título
            IRow renglonTitulo = sheet.CreateRow(5);
            ICell celdaTitulo = renglonTitulo.CreateCell(iColumnaInicialReporte);
            celdaTitulo.SetCellValue(tituloReporte);

            //se crea el estilo del título
            ICellStyle cellStyleTitulo = xlsWorkBook.CreateCellStyle();
            IFont fontTitulo = xlsWorkBook.CreateFont();
            fontTitulo.FontHeightInPoints = 14;
            fontTitulo.Boldweight = (short)FontBoldWeight.Bold;
            cellStyleTitulo.SetFont(fontTitulo);
            cellStyleTitulo.Alignment = HorizontalAlignment.Center;
            celdaTitulo.CellStyle = cellStyleTitulo;
            //Se hace un Merge de celdas para el título
            CellRangeAddress rangoMerge = new CellRangeAddress(5, 5, 1, 9);
            sheet.AddMergedRegion(rangoMerge);
            //se crean las columnas del reporte
            IRow renglonCabezera = sheet.CreateRow(6);
            int iCol = iColumnaInicialReporte; //contador de columnas
            foreach (DataColumn Columna in UniversoPedidosEmpaque.Columns)
            {
                renglonCabezera.CreateCell(iCol).SetCellValue(Columna.ColumnName);
                iCol++;
            }
            #endregion
            #region SE ESCRIBE EL DETALLE EN EL ARCHIVO
            int iRenglonActual = iRenglonInicialDetalle;
          
            foreach (DataRow renglonCliente in UniversoPedidosEmpaque.Rows)
            {
                IRow renglonDetalle = sheet.CreateRow(iRenglonActual);
                //Pedido:
                renglonDetalle.CreateCell(iColumnaInicialReporte).SetCellValue((string)renglonCliente[UniversoPedidosEmpaque.Columns[0].ColumnName]);
                //Fecha Pedido
                renglonDetalle.CreateCell(iColumnaInicialReporte + 1).SetCellValue(((DateTime)renglonCliente[UniversoPedidosEmpaque.Columns[1].ColumnName]).ToShortDateString());
                //clave del cliente
                renglonDetalle.CreateCell(iColumnaInicialReporte + 2).SetCellValue(((string)renglonCliente[UniversoPedidosEmpaque.Columns[2].ColumnName]).Trim());
                //nombre
                renglonDetalle.CreateCell(iColumnaInicialReporte + 3).SetCellValue((string)renglonCliente[UniversoPedidosEmpaque.Columns[3].ColumnName]);
                //Prendas
                renglonDetalle.CreateCell(iColumnaInicialReporte + 4).SetCellValue((int)renglonCliente[UniversoPedidosEmpaque.Columns[4].ColumnName]);
                //Fecha Venc.
                if (renglonCliente[UniversoPedidosEmpaque.Columns[5].ColumnName] != DBNull.Value)
                {
                    renglonDetalle.CreateCell(iColumnaInicialReporte + 5)
                        .SetCellValue(
                            ((DateTime) renglonCliente[UniversoPedidosEmpaque.Columns[5].ColumnName]).ToShortDateString());
                }
                //Fecha Empaque
                renglonDetalle.CreateCell(iColumnaInicialReporte + 6).SetCellValue(((DateTime)renglonCliente[UniversoPedidosEmpaque.Columns[6].ColumnName]).ToShortDateString());
                //Factura
                if (renglonCliente[UniversoPedidosEmpaque.Columns[7].ColumnName] != DBNull.Value)
                {
                    renglonDetalle.CreateCell(iColumnaInicialReporte + 7).SetCellValue(((string)renglonCliente[UniversoPedidosEmpaque.Columns[7].ColumnName]));
                }

               

                //se incrementa renglón
                iRenglonActual++;
            }
            #endregion
            
            //se ajustan las culumnas al ancho automático
            for (int i = 0; i < 8; i++)
            {
                sheet.AutoSizeColumn(i + 1);
            }
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
