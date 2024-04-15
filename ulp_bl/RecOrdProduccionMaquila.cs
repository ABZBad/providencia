using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ulp_dl.SIPNegocio;
using sm_dl;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.SS.Util;
using ulp_bl.Utiles;
using System.IO;

namespace ulp_bl
{
    public class RecOrdProduccionMaquila
    {
        public static DataTable DevuelveDatosXls(string Referencias)
        {
            DataTable datos = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "usp_RecOrdProduccionMaquiaExportarXls";
                cmdDatos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@REFERENCIA", Referencias));
                datos = cmdDatos.GetDataTable();
            }
            return datos;
        }
        public static DataTable DevuelveDatosBusqueda(string referencia, string CVE_DOC, string CVE_ALM)
        {
            DataTable datos = new DataTable();
            using (var dbContext=new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "usp_RecOrdProduccionMaquia";
                cmdDatos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@referencia", referencia));
                cmdDatos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CVE_DOC", CVE_DOC));
                cmdDatos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CVE_ALM", CVE_ALM));
                datos = cmdDatos.GetDataTable();
            }
            return datos;
        }

        /// <summary>
        /// Este método se debe dependiendo la cantidad de registros que devuelva el método 
        /// DevuelveDatosBusqueda
        /// </summary>
        public void GuardaRegistroProc(int almacen, int NUM_REG, string REFERENCIA, string PRODUCTO, int tallaOK, int tallaDef, string defectuosos, 
            string orden_maquila, decimal CostoConfeccion, int TotalPrendas, string EsquemaImp, int ConsecutivoReg, string prefijo)
        {
            using (var dbContext=new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand guarda = new sm_dl.SqlServer.SqlServerCommand();
                guarda.Connection = sm_dl.DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                guarda.ObjectName = "usp_RecOrdProduccionMaquiaProcesar";
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@almacen", almacen));
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NUM_REG", NUM_REG));
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@REFERENCIA", REFERENCIA));
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PRODUCTO", PRODUCTO));
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@tallaOK", tallaOK));
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@tallaDef", tallaDef));
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@defectuosos", defectuosos));
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@orden_maquila", orden_maquila));
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CostoConfeccion", CostoConfeccion));
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TotalPrendas", TotalPrendas));
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EsquemaImp", EsquemaImp));
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ConsecutivoReg", ConsecutivoReg));
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@prefijo", prefijo));

                guarda.Execute();
            }
 
        }
        /// <summary>
        /// Este método se debe ejecutar una sola vez cada que se guarde la info de la orden de producción
            /// se guarda la información base
        /// </summary>
        public void GuardaRegistroProc1(int almacen, string clave_proveedor, string orden_maquila, decimal CostoConfeccion, 
            int TotalPrendasOK, int TotalPrendas, string EsquemaImp)
        {
            using (var dbContext = new SIPNegocioContext())
            {

                sm_dl.SqlServer.SqlServerCommand guarda = new sm_dl.SqlServer.SqlServerCommand();
                guarda.Connection = sm_dl.DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                guarda.ObjectName = "usp_RecOrdProduccionMaquiaProcesar1";
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@almacen", almacen));
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@clave_proveedor", clave_proveedor));
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@orden_maquila", orden_maquila));
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CostoConfeccion", CostoConfeccion));
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TotalPrendasOK", TotalPrendasOK));
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TotalPrendas", TotalPrendas));
                guarda.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EsquemaImp", EsquemaImp));
                guarda.Execute();
                guarda.Connection.Close();
            }

        }
        public static void GeneraArchivoExcel(DataTable datosMaquila, string RutaYNombreArchivo)
        {
            NPOI.HSSF.UserModel.HSSFWorkbook libro = new NPOI.HSSF.UserModel.HSSFWorkbook();
            NPOI.SS.UserModel.ISheet hoja = libro.CreateSheet("Hoja1");
            int r = 0;
            NPOI.SS.UserModel.IRow rowE = hoja.CreateRow(0);

            /*
             * Se renombra el nombre de las columnas para que sea igual al reporte de VB6
             * 
            */
            datosMaquila.Columns[0].ColumnName = "REG";
            datosMaquila.Columns[1].ColumnName = "ARTICULO";
            datosMaquila.Columns[2].ColumnName = "MOV";
            datosMaquila.Columns[3].ColumnName = "FECHA_DOCU";
            datosMaquila.Columns[4].ColumnName = "DOCTO";
            datosMaquila.Columns[5].ColumnName = "CANT";
            datosMaquila.Columns[6].ColumnName = "ALMACEN";

            foreach (DataRow fila in datosMaquila.Rows)
            {
                NPOI.SS.UserModel.IRow rowD = hoja.CreateRow(r + 2);
                for (int i = 0; i < datosMaquila.Columns.Count; i++)
                {
                    if (r == 0)
                    {
                        rowE.CreateCell(i).SetCellValue(datosMaquila.Columns[i].ColumnName.ToString());                        
                    }
                    
                    switch (datosMaquila.Columns[i].DataType.ToString())
	                {
                        case "System.String":
                            rowD.CreateCell(i).SetCellValue(fila[i].ToString());
                            break;
                        case "System.DateTime":
                            rowD.CreateCell(i).SetCellValue(Convert.ToDateTime(fila[i]).ToShortDateString());
                            break;
                        case "System.Boolean":
                            rowD.CreateCell(i).SetCellValue(Convert.ToBoolean(fila[i].ToString()));
                            break;
                        default:
                            rowD.CreateCell(i).SetCellValue(Convert.ToDouble(fila[i].ToString()));
                            break;
	                }
                     
                    
                }
                r++;
            }
            for (int i = 0; i < datosMaquila.Columns.Count; i++)
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
