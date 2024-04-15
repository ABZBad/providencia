
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ulp_dl.SIPReportes;
using sm_dl.SqlServer;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using System.Data.SqlClient;
using ulp_bl.Utiles;
using NPOI.SS.Util;
using sm_dl;


namespace ulp_bl.Reportes
{
    public class RepCostoUltimoPT
    {

        public static DataTable DevuelveCosteoPT()  
        {
            DataTable dataTableCosteoProductoTerminado = new DataTable();
            using (var dbContext = new SIPReportesContext())
            {
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmd.ObjectName = "usp_RepCostoUltimoPT";
                dataTableCosteoProductoTerminado = cmd.GetDataTable();
                cmd.Connection.Close();

            }
            return dataTableCosteoProductoTerminado;
        }
        
       
        public static void GeneraArchivoExcel(DataTable datosCosteoPT, string RutaYNombreArchivo)
        {

            HSSFWorkbook libro = new HSSFWorkbook();
            ISheet hoja = libro.CreateSheet("Hoja1");

            ICellStyle estilo = libro.CreateCellStyle();
            IFont font = libro.CreateFont();

            font.FontName = "Calibri";
            font.FontHeightInPoints = 11;

            estilo.SetFont(font);


            hoja.SetDefaultColumnStyle(0, estilo);
            hoja.SetDefaultColumnStyle(1, estilo);
            hoja.SetDefaultColumnStyle(2, estilo);
            hoja.SetDefaultColumnStyle(3, estilo);
            hoja.SetDefaultColumnStyle(4, estilo);
            hoja.SetDefaultColumnStyle(5, estilo);
            hoja.SetDefaultColumnStyle(6, estilo);

            //Encabezado
            IRow rengl = hoja.CreateRow(0);
            ICell celda = rengl.CreateCell(0);
            celda.SetCellValue("Reporte de Costeo de ProductoTerminado a Ultimo Costo");
            ICell celda2 = rengl.CreateCell(2);
            celda2.SetCellValue(string.Format("Emitido el {0}",DateTime.Now.ToString("dd/MM/yyyy")));

            //Etiquetas
            IRow rengl1 = hoja.CreateRow(2);
            ICell celda20 = rengl1.CreateCell(0);
            celda20.SetCellValue("CLAVE");
            ICell celda21 = rengl1.CreateCell(1);
            celda21.SetCellValue("DESCRIPCION");
            ICell celda22 = rengl1.CreateCell(2);
            celda22.SetCellValue("LINEA");
            ICell celda23 = rengl1.CreateCell(3);
            celda23.SetCellValue("F.ULT.COMP.");
            ICell celda24 = rengl1.CreateCell(4);
            celda24.SetCellValue("EXIST");
            ICell celda25 = rengl1.CreateCell(5);
            celda25.SetCellValue("COSTO");
            ICell celda26 = rengl1.CreateCell(6);
            celda26.SetCellValue("IMPORTE");
            

            int rng = 3;

          

            foreach (DataRow fila in datosCosteoPT.Rows)
            {
               
                //Crea renglon para la filas    
                IRow Fila = hoja.CreateRow(rng);
                
                //Asigna valor de fila
                Fila.CreateCell(0).SetCellValue(fila["CLV_ART"].ToString());               
                Fila.CreateCell(1).SetCellValue(fila["DESCR"].ToString());
                Fila.CreateCell(2).SetCellValue(fila["LIN_PROD"].ToString());

                if (fila["FCH_ULTCOM"].ToString() != "")
                {
                    Fila.CreateCell(3).SetCellValue(((DateTime)fila["FCH_ULTCOM"]).ToString("dd/MM/yyyy"));
                }
                
                Fila.CreateCell(4).SetCellValue(Convert.ToDouble((fila["EXIST"].ToString() == "" ? "0" : fila["EXIST"].ToString())));
                
                if (fila["ULT_COSTO"].ToString() == "S/E")
                {
                    Fila.CreateCell(5).SetCellValue(fila["ULT_COSTO"].ToString());
                }
                else 
                {   
                    Fila.CreateCell(5).SetCellValue(Convert.ToDouble(fila["EXIST"].ToString()));
                }
      
                //Fila.CreateCell(5).SetCellValue(Convert.ToDouble((fila["ULT_COSTO"].ToString() == "S/E" ? "" : fila["ULT_COSTO"].ToString())));
                Fila.CreateCell(6).SetCellValue(Convert.ToDouble(fila["IMPORTE"].ToString()));
                rng++;
            }

            //Totales
            IRow RowTotal = hoja.CreateRow(rng=rng + 2);
            
            ICell Totales = RowTotal.CreateCell(1);
            Totales.SetCellValue("Totales");


            ICell TotalExist = RowTotal.CreateCell(4);
            TotalExist.CellFormula = string.Format("SUM(E4:E" + (rng=rng-2).ToString() + ")");

            ICell TotalImporte = RowTotal.CreateCell(6);
            TotalImporte.CellFormula = string.Format("SUM(G4:G" + rng.ToString() + ")");
        
            hoja.SetColumnWidth(0, ExcelNpoiUtil.AnchoColumna(80));
            hoja.SetColumnWidth(1, ExcelNpoiUtil.AnchoColumna(321));
            hoja.SetColumnWidth(2, ExcelNpoiUtil.AnchoColumna(80));
            hoja.SetColumnWidth(3, ExcelNpoiUtil.AnchoColumna(80));
            hoja.SetColumnWidth(4, ExcelNpoiUtil.AnchoColumna(80));
            hoja.SetColumnWidth(5, ExcelNpoiUtil.AnchoColumna(80));
            hoja.SetColumnWidth(6, ExcelNpoiUtil.AnchoColumna(80));

            FileStream fs = new FileStream(RutaYNombreArchivo, FileMode.CreateNew);
            libro.Write(fs);
            fs.Close();

        }
    }
}
