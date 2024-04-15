using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ulp_dl.SIPNegocio;
using sm_dl;
using System.IO;

namespace ulp_bl.Reportes
{
    public class RepRecepcionMaquila
    {
        public static DataTable DevuelveDatosReporte(string referencias)
        {
            DataTable datos = new DataTable();
            using (var dbContext=new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand datosEjecuta = new sm_dl.SqlServer.SqlServerCommand();
                datosEjecuta.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                datosEjecuta.ObjectName = "usp_RepRecepcionMaquila";   
                datosEjecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@referencia", referencias));
                datos = datosEjecuta.GetDataTable();                
            }
            return datos;
        }
        public static void GeneraArchivoExcel(DataTable datosMaquila, string RutaYNombreArchivo)
        {
            NPOI.HSSF.UserModel.HSSFWorkbook libro = new NPOI.HSSF.UserModel.HSSFWorkbook();
            NPOI.SS.UserModel.ISheet hoja = libro.CreateSheet("Hoja1");
            int r = 0;
            NPOI.SS.UserModel.IRow rowE = hoja.CreateRow(0);
            foreach (DataRow fila in datosMaquila.Rows)
            {
                NPOI.SS.UserModel.IRow rowD = hoja.CreateRow(r + 2);
                for (int i = 0; i < datosMaquila.Columns.Count; i++)
                {
                    if (r == 0)
                    {
                        rowE.CreateCell(i).SetCellValue(datosMaquila.Columns[i].ColumnName.ToString());                        
                    }
                    if (datosMaquila.Columns[i].ColumnName == "FECHA_DOCU")
                    {
                        rowD.CreateCell(i).SetCellValue(((DateTime)fila[i]).ToString("dd/MM/yyyy"));
                    }
                    else if (datosMaquila.Columns[i].ColumnName == "CANT")
                    {
                        rowD.CreateCell(i).SetCellValue(((double)fila[i]));
                    }
                    else if ("REG,ALMACEN".Contains(datosMaquila.Columns[i].ColumnName))
                    {
                        rowD.CreateCell(i).SetCellValue(Convert.ToInt32(fila[i]));
                    }
                    else
                    {
                        rowD.CreateCell(i).SetCellValue(fila[i].ToString());
                    }
                    
                }
                r++;
            }
            for (int i = 0; i < 7; i++)
            {
                hoja.AutoSizeColumn(i);
            }
            if (File.Exists(RutaYNombreArchivo))
            {
                File.Delete(RutaYNombreArchivo);
            }
            FileStream fs = new FileStream(RutaYNombreArchivo, FileMode.CreateNew);
            libro.Write(fs);
            fs.Close();            
        }
    }
}
