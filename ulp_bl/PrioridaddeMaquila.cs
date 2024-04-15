using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using sm_dl;
using ulp_dl.SIPNegocio;
using System.IO;

namespace ulp_bl
{
    public class PrioridaddeMaquila
    {
        public static void ActualizaPrioridad(string OC, int PRIORIDAD)
        {
            
            using (var dbContext=new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand actualiza = new sm_dl.SqlServer.SqlServerCommand();
                actualiza.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                actualiza.ObjectName = "usp_PrioridaddeMaquilaActualizaPrioridad";
                actualiza.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OC", OC));
                actualiza.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PRIORIDAD", PRIORIDAD));
                actualiza.Execute();
            }
        }
        public static DataTable DevuelveDatosBusqueda(string proveedor, string Prefijo)
        {
            DataTable datos = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "usp_PrioridaddeMaquila";
                cmdDatos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@proveedor", proveedor));
                cmdDatos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Prefijo", Prefijo));                
                datos = cmdDatos.GetDataTable();
            }
            return datos;
        }
        public static DataTable DevuelveDatosBusquedaXls(string proveedor, string Prefijo)
        {
            DataTable datos = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "usp_PrioridaddeMaquilaXls";
                cmdDatos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@proveedor", proveedor));
                cmdDatos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Prefijo", Prefijo));
                datos = cmdDatos.GetDataTable();
            }
            return datos;
        }
        public static void GeneraArchivoExcel(DataTable datosMaquila, string RutaYNombreArchivo)
        {
            NPOI.HSSF.UserModel.HSSFWorkbook libro = new NPOI.HSSF.UserModel.HSSFWorkbook();
            NPOI.SS.UserModel.ISheet hoja = libro.CreateSheet("Hoja1");
            int r = 3;
            NPOI.SS.UserModel.IRow rowTit = hoja.CreateRow(0);
            NPOI.SS.UserModel.IRow rowProv = hoja.CreateRow(1);
            rowProv.CreateCell(2).SetCellValue(datosMaquila.Rows[0][5].ToString());
            //datosMaquila.Columns.Remove("NOMBRE");
            rowTit.CreateCell(2).SetCellValue("Rep. Prioridad de maquila ");
            NPOI.SS.UserModel.IRow rowE = hoja.CreateRow(3);
            foreach (DataRow fila in datosMaquila.Rows)
            {
                NPOI.SS.UserModel.IRow rowD = hoja.CreateRow(r + 2);
                NPOI.SS.UserModel.IRow rowT = hoja.CreateRow(r + 3);
                NPOI.SS.UserModel.IRow rowObs = hoja.CreateRow(r + 4);
                for (int i = 0; i < datosMaquila.Columns.Count - 3; i++)
                {
                    if (r == 3)
                    {
                        rowE.CreateCell(i).SetCellValue(datosMaquila.Columns[i].ColumnName.ToString());
                        hoja.AutoSizeColumn(i);
                    }                    
                    if (i==0||i==4)
                    {
                        rowD.CreateCell(i).SetCellValue(Convert.ToDouble(fila[i].ToString()));                        
                    }
                    else
                    {
                        rowD.CreateCell(i).SetCellValue(fila[i].ToString());
                    } 
                     
                }
                rowT.CreateCell(2).SetCellValue(fila["tallas"].ToString());
                rowObs.CreateCell(2).SetCellValue(fila["X_OBSER"].ToString());
                
                r = r + 4;
            }

            NPOI.SS.UserModel.IRow rowTot = hoja.CreateRow(r + 3);
            rowTot.CreateCell(0).SetCellValue("TOTAL");
            NPOI.SS.UserModel.IRow rowTotVal = hoja.CreateRow(r + 4);
            rowTotVal.CreateCell(0).SetCellFormula(string.Format("SUM(A6:A{0})", r));

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
