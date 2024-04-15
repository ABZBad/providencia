using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using System.Data;
using sm_dl;
using sm_dl.SqlServer;
using ulp_dl.aspel_sae80;
using ulp_dl;

using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

using System.IO;
using ulp_dl.SIPNegocio;

namespace ulp_bl.Reportes
{
    public class RepExisXAlm
    {
    
        public static DataTable DevuelveExistenciaPorAlmacen(int NumeroAlmacen)
        {
            DataTable dataTableExistenciaPorAlmacen = new DataTable();
            string connStr = "";
            using (var dbContext = new SIPNegocioContext())
            {
                /*
                var query = from existencias in dbContext.MULT01 
                            where existencias.CVE_ALM == NumeroAlmacen
                            orderby existencias.CVE_ALM
                            select new { existencias.CVE_ART,existencias.EXIST };

                */

                connStr = dbContext.Database.Connection.ConnectionString;

                //dataTableExistenciaPorAlmacen = Linq2DataTable.CopyToDataTable(query);
            }

            SqlServerCommand cmd = new SqlServerCommand();
            cmd.Connection = DALUtil.GetConnection(connStr);
            cmd.ObjectName = "ups_ExistenciaPorAlmacen";
            cmd.Parameters.Add(new SqlParameter("@NumeroAlmacen", NumeroAlmacen));
            dataTableExistenciaPorAlmacen = cmd.GetDataTable();
            cmd.Connection.Close();

            return dataTableExistenciaPorAlmacen;
        }
        public static void GeneraArchivoExcel(DataTable datosExistenciaPorAlmacen, string RutaYNombreArchivo)
        {
        
            

            HSSFWorkbook libro = new HSSFWorkbook();
            ISheet hoja = libro.CreateSheet("Hoja1");

            hoja.FitToPage = false;


            ICellStyle estilo = libro.CreateCellStyle();
            IFont font = libro.CreateFont();

            font.FontName = "Calibri";
            font.FontHeightInPoints = 9;

            estilo.SetFont(font);


            hoja.SetDefaultColumnStyle(0, estilo);
            hoja.SetDefaultColumnStyle(1, estilo);
            hoja.SetDefaultColumnStyle(2, estilo);
            hoja.SetDefaultColumnStyle(3, estilo);
            hoja.SetDefaultColumnStyle(4, estilo);


            IRow rengl = hoja.CreateRow(3);
            ICell celda = rengl.CreateCell(0);
            celda.SetCellValue("Artículo");
            ICell celda1 = rengl.CreateCell(1);
            celda1.SetCellValue("Sistema");
            ICell celda2 = rengl.CreateCell(2);
            celda2.SetCellValue("Real");
            ICell celda3 = rengl.CreateCell(3);
            celda3.SetCellValue("2° Conteo");
            ICell celda4 = rengl.CreateCell(4);
            celda4.SetCellValue("Ajuste");

         
            int rng = 3;
            string modeloAnterior = "";
            string modeloActual = "";

            foreach (DataRow fila in datosExistenciaPorAlmacen.Rows)
            {
                if (fila["CVE_ART"].ToString().Length >= 8)
                {
                    modeloActual = fila["CVE_ART"].ToString().Substring(0, 8);
                }
                else
                {
                    modeloActual = fila["CVE_ART"].ToString();
                }

                if (modeloActual != modeloAnterior)
                {
                    hoja.SetRowBreak(rng);
                    rng++;
                    IRow renglon = hoja.CreateRow(rng);

                    //asigna valores de etiquetas
                    //Articulo
                    renglon.CreateCell(0).SetCellValue("Artículo");
                    //hoja.AutoSizeColumn(0);
                    //Sistema
                    renglon.CreateCell(1).SetCellValue("Sistema");
                    //hoja.AutoSizeColumn(1);
                    //Real
                    renglon.CreateCell(2).SetCellValue("Real");
                    //hoja.AutoSizeColumn(2);
                    //2° Conteo
                    renglon.CreateCell(3).SetCellValue("2° Conteo");
                    //hoja.AutoSizeColumn(3);
                    //Ajuste
                    renglon.CreateCell(4).SetCellValue("Ajuste");
                    //hoja.AutoSizeColumn(4);


                }

                rng = rng + 2;
                //Crea renglon para la filas    
                IRow Fila = hoja.CreateRow(rng);

                //Asigna valor de fila
                ICell cveArt = Fila.CreateCell(0);
                cveArt.SetCellValue(fila["CVE_ART"].ToString());                            
                Fila.CreateCell(1).SetCellValue(Convert.ToDouble((fila["EXIST"].ToString() == "" ? "0" : fila["EXIST"].ToString())));
                Fila.CreateCell(2).SetCellValue("_______________");
                Fila.CreateCell(3).SetCellValue("_______________");
                Fila.CreateCell(4).SetCellValue("_______________");
                

                if (fila["CVE_ART"].ToString().Length >= 8)
                {
                    modeloAnterior = fila["CVE_ART"].ToString().Substring(0, 8);
                }
                else
                {
                    modeloAnterior = fila["CVE_ART"].ToString();
                }
               
            }
            

            FileStream fs = new FileStream(RutaYNombreArchivo, FileMode.CreateNew);
            libro.Write(fs);
            fs.Close();
            
        }
    }

}