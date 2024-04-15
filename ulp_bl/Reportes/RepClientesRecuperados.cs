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
using NPOI.XSSF.UserModel;
using sm_dl;
using sm_dl.SqlServer;
using ulp_dl.SIPReportes;

namespace ulp_bl.Reportes
{
    public class RepClientesRecuperados
    {
        public static DataTable RegresaClientesRecuperados(DateTime FechaDoc)
        {
            DataTable dataTableClientesRecuperados = new DataTable();
            using (var DbContext = new SIPReportesContext())
            {
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(DbContext.Database.Connection.ConnectionString);
                cmd.ObjectName = "usp_RepClientesRecuperados";
                cmd.Parameters.Add(new SqlParameter("@fecha_doc", FechaDoc.ToString("dd-MM-yyyy")));
                dataTableClientesRecuperados = cmd.GetDataTable();
                cmd.Connection.Close();

            }
            return dataTableClientesRecuperados;
        }
        public static DataTable RegresaClientesNuevos(DateTime FechaDoc,Enumerados.TipoReporteClientesRecuperados TipoReporte)
        {

            
            DataTable dataTableClientesRecuperados = new DataTable();
            using (var DbContext = new SIPReportesContext())
            {
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(DbContext.Database.Connection.ConnectionString);
                cmd.ObjectName = "usp_RepClientesNuevos";
                cmd.Parameters.Add(new SqlParameter("@fecha_doc", FechaDoc.ToString("dd-MM-yyyy")));
                cmd.Parameters.Add(new SqlParameter("@tipo", Convert.ToInt32(TipoReporte)));
                dataTableClientesRecuperados = cmd.GetDataTable();
                cmd.Connection.Close();
            }
            return dataTableClientesRecuperados;
        }

        public static void GeneraArchivoExcel(string RutaYNombreArchivo, DataTable TablaClientes,Enumerados.TipoReporteClientesRecuperados TipoReporte,DateTime FechaReporte)
    {
        HSSFWorkbook xlsWorkBook = new HSSFWorkbook();
        ISheet sheet = xlsWorkBook.CreateSheet("Hoja1");

        //variables de control
        int iColumnaInicialReporte = 1;
        int iRenglonInicialDetalle = 7;
        //se asigna título del reporte
        #region TÍTULO DEL REPORTE
        string tituloReporte = "";
            if (TipoReporte != Enumerados.TipoReporteClientesRecuperados.Ninguno)
            {
                tituloReporte = string.Format("Clientes Nuevos - {0} - {1}",
                    (TipoReporte == Enumerados.TipoReporteClientesRecuperados.Foraneo ? "Foraneos" : "Metropolitano"),
                    FechaReporte.ToString("dd/MM/yyyy"));
            }
            else
            {
                tituloReporte = string.Format("Clientes Recuperados - {0}", FechaReporte.ToString("dd/MM/yyyy"));
            }
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
        CellRangeAddress rangoMerge = new CellRangeAddress(5, 5, 1, 6);
        sheet.AddMergedRegion(rangoMerge);
        //se crean las columnas del reporte
        IRow renglonCabezera = sheet.CreateRow(6);
        int iCol = iColumnaInicialReporte; //contador de columnas
        foreach (DataColumn Columna in TablaClientes.Columns)
        {
            renglonCabezera.CreateCell(iCol).SetCellValue(Columna.ColumnName);
            iCol++;
        }            
        #endregion        
        #region SE ESCRIBE EL DETALLE EN EL ARCHIVO
            int iRenglonActual = iRenglonInicialDetalle;
            foreach (DataRow renglonCliente in TablaClientes.Rows)
            {
                IRow renglonDetalle = sheet.CreateRow(iRenglonActual);
                //vendedor:
                renglonDetalle.CreateCell(iColumnaInicialReporte).SetCellValue((string)renglonCliente[TablaClientes.Columns[0].ColumnName]);
                //clave del cliente
                renglonDetalle.CreateCell(iColumnaInicialReporte + 1).SetCellValue(((string)renglonCliente[TablaClientes.Columns[1].ColumnName]).Trim());
                //clave del vendedor
                renglonDetalle.CreateCell(iColumnaInicialReporte + 2).SetCellValue((string)renglonCliente[TablaClientes.Columns[2].ColumnName]);
                //nombre vendedor
                renglonDetalle.CreateCell(iColumnaInicialReporte + 3).SetCellValue((string)renglonCliente[TablaClientes.Columns[3].ColumnName]);
                //factura
                renglonDetalle.CreateCell(iColumnaInicialReporte + 4).SetCellValue((string)renglonCliente[TablaClientes.Columns[4].ColumnName]);
                //factura
                ICell cellPrendas = renglonDetalle.CreateCell(iColumnaInicialReporte + 5);
                 cellPrendas.SetCellValue((double)renglonCliente[TablaClientes.Columns[5].ColumnName]);

                ICellStyle cellStylePrendas = xlsWorkBook.CreateCellStyle();
                cellStylePrendas.DataFormat = HSSFDataFormat.GetBuiltinFormat("#,##0_);(#,##0)");
                cellPrendas.CellStyle = cellStylePrendas;
                //se incrementa renglón
                iRenglonActual++;
            }
        #endregion
        //se crea formula para la sumatoria de la prendas
            IRow renglonSumatoriaPrendas = sheet.CreateRow(iRenglonActual + 1);
            ICell celdaSumatoriaPrendas = renglonSumatoriaPrendas.CreateCell(6);
            ICellStyle cellStyleSumatoria = xlsWorkBook.CreateCellStyle();
            cellStyleSumatoria.DataFormat = HSSFDataFormat.GetBuiltinFormat("#,##0_);(#,##0)");
            List<string> formatos = HSSFDataFormat.GetBuiltinFormats();
            celdaSumatoriaPrendas.CellStyle = cellStyleSumatoria;
            celdaSumatoriaPrendas.SetCellFormula(string.Format("SUM(G{0}:G{1})", iRenglonInicialDetalle+1,iRenglonActual));
            
        //se ajustan las culumnas al ancho automático
            for (int i = 0; i < 5; i++)
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
