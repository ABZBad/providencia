using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ulp_dl;
using ulp_dl.SIPNegocio;
using ulp_bl.Utiles;
using sm_dl;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;

namespace ulp_bl
{
    public class vw_EstadoCuenta:ICrud<vw_EstadoCuenta>
    {
        public string CVE_CLIE { get; set; }
        public string Concepto { get; set; }
        public string Documento { get; set; }
        public string Referencia { get; set; }
        public string Aplicado { get; set; }
        public string Vencido { get; set; }
        public string Elaborado { get; set; }
        public double Cargo { get; set; }
        public double Abono { get; set; }
        public int Dias { get; set; }

        public bool TieneError
        {
            get { throw new NotImplementedException(); }
        }

        public Exception Error
        {
            get { throw new NotImplementedException(); }
        }

        public vw_EstadoCuenta Consultar(int ID)
        {
            throw new NotImplementedException();
        }
        public DataTable ConsultarMovimientos(string clave_cliente)
        {            
            
            DataTable EstadoCuenta = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand ejecuta = new sm_dl.SqlServer.SqlServerCommand();
                ejecuta.Connection = sm_dl.DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                ejecuta.ObjectName = "usp_EstadoCuenta";
                ejecuta.Parameters.Add(new System.Data.SqlClient.SqlParameter("@clave_cliente", clave_cliente));
                EstadoCuenta = ejecuta.GetDataTable();
            }
            /*
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand sel = new sm_dl.SqlServer.SqlServerCommand();
                sel.Connection = sm_dl.DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                sel.ObjectName = "usp_EstadoCuenta";
                sel.Parameters.Add(new System.Data.SqlClient.SqlParameter("@clave_cliente", clave_cliente));
                EstadoCuenta = sel.GetDataTable();
                sel.Connection.Close();

                //var resultado = from edo in dbContext.vw_EstadoCuenta where edo.CVE_CLIE.Equals(ID) select edo;                               
                //EstadoCuenta =  Linq2DataTable.CopyToDataTable(resultado);
 
            }
             * */
            return EstadoCuenta;
        }

        public void GeneraArchivoExcel(string ruta, DataTable datosEstadoCuenta, vw_Clientes datosCliente)
        {
            NPOI.HSSF.UserModel.HSSFWorkbook libro = new HSSFWorkbook();
            NPOI.SS.UserModel.ISheet hoja = libro.CreateSheet("Estado de Cuenta");

            short formatoMoneda = libro.CreateDataFormat().GetFormat("$#,##0.00");
            short formatoEntero = libro.CreateDataFormat().GetFormat("#,##0");

            #region EncabezadoHoja
            ICellStyle estiloEnc = libro.CreateCellStyle();
            estiloEnc.Alignment = new HorizontalAlignment();
            estiloEnc.Alignment = HorizontalAlignment.Right;
            IRow renglonE1 = hoja.CreateRow(0);
            renglonE1.CreateCell(3).SetCellValue(datosCliente.CLAVE + " - " + datosCliente.NOMBRE_CLIENTE);
            renglonE1.Cells[0].CellStyle = estiloEnc;
            hoja.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 3, 9));
            
            IRow renglonE2 = hoja.CreateRow(1);
            renglonE2.CreateCell(3).SetCellValue(datosCliente.DIRECCION);
            hoja.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 3, 9));
            renglonE2.Cells[0].CellStyle = estiloEnc;
            IRow renglonE3 = hoja.CreateRow(2);
            renglonE3.CreateCell(6).SetCellValue("Última Compra:");
            if (datosCliente.FCH_ULTCOM!=null)
            {
                renglonE3.CreateCell(7).SetCellValue(datosCliente.FCH_ULTCOM.ToString());    
            }                        
            renglonE3.CreateCell(8).SetCellValue("Días de Crédito:");
            renglonE3.CreateCell(9).SetCellValue(datosCliente.DIAS_CRE.ToString());
            IRow renglonE4 = hoja.CreateRow(3);
            renglonE4.CreateCell(8).SetCellValue("Límite Crédito:");
            renglonE4.CreateCell(9).SetCellValue(Convert.ToDouble(datosCliente.LIM_CRED.ToString()));
            IRow renglonE5 = hoja.CreateRow(4);
            renglonE5.CreateCell(8).SetCellValue("Saldo Actual:");
            renglonE5.CreateCell(9).SetCellValue(Convert.ToDouble(datosCliente.SALDO.ToString()));
            IRow renglonE6 = hoja.CreateRow(5);

            //para encabezado ESTADO CUENTA
            ICell celdaE6 = renglonE6.CreateCell(1);
            celdaE6.SetCellValue("Estado de Cuenta");
            ICellStyle estilo = libro.CreateCellStyle();
                        
            estilo.Alignment = new HorizontalAlignment();            
            hoja.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(5, 5, 1, 9));

            IFont fontTitulo = libro.CreateFont();
            fontTitulo.FontHeightInPoints = 14;
            fontTitulo.Boldweight = (short)FontBoldWeight.Bold;
            fontTitulo.FontName = "Calibri";    
            estilo.SetFont(fontTitulo);
            estilo.Alignment = HorizontalAlignment.Center;
            
            celdaE6.CellStyle = estilo;

            #endregion EncabezadoHojaFin
 
            #region Detalles
            int idRenglon = 6;
            datosEstadoCuenta.Columns.Remove("CVE_CLIE");
            IRow renglonEnc = hoja.CreateRow(idRenglon);
            foreach (DataRow movimiento in datosEstadoCuenta.Rows)
            {
                IRow renglonD = hoja.CreateRow(idRenglon + 1);
                for (int i = 0; i < datosEstadoCuenta.Columns.Count; i++)
                {                    
                    if (idRenglon == 6)
                    {
                        if (i==0)
                        {
                            hoja.SetColumnWidth(i + 1, ExcelNpoiUtil.AnchoColumna(180));    
                        }
                        else
                        {
                            hoja.SetColumnWidth(i + 1, ExcelNpoiUtil.AnchoColumna(93));                                 
                        }
                        renglonEnc.CreateCell(i + 1).SetCellValue(datosEstadoCuenta.Columns[i].ColumnName.ToString());
                    }
                    if (i == 6||i==7)
                    {
                        renglonD.CreateCell(i + 1).SetCellValue(Convert.ToDouble(movimiento[i].ToString()));
                        //renglonD.Cells[i].SetCellType(CellType.Numeric);      
                        renglonD.Cells[i].CellStyle.DataFormat = formatoMoneda;
                    }
                    else
                    {
                        renglonD.CreateCell(i + 1).SetCellValue(movimiento[i].ToString());
                    }                                        
                }
                idRenglon++;
            }

            #endregion DetallesFin  

            #region Pie
            //formula para las sumatorias
            IRow renglonSumatoriaTotales = hoja.CreateRow(idRenglon + 2);
            ICell tituloTotales = renglonSumatoriaTotales.CreateCell(6);
            tituloTotales.SetCellValue("Totales:");
            ICellStyle cellStyleSumatoria = libro.CreateCellStyle();
            cellStyleSumatoria.DataFormat = formatoMoneda;

            //sumatoria del cargo            
            ICell celdaSumatoriaCargo = renglonSumatoriaTotales.CreateCell(7);
            celdaSumatoriaCargo.CellStyle = cellStyleSumatoria;

            celdaSumatoriaCargo.SetCellFormula(string.Format("SUM(H8:H{0})", idRenglon + 1));

            //sumatoria del abono
            ICell celdaSumatoriaAbono = renglonSumatoriaTotales.CreateCell(8);
            celdaSumatoriaAbono.CellStyle = cellStyleSumatoria;
            celdaSumatoriaAbono.SetCellFormula(string.Format("SUM(I8:I{0})", idRenglon + 1));

            //formula para el gran total
            IRow rGranTotal = hoja.CreateRow(idRenglon + 3);
            ICell cTitulo = rGranTotal.CreateCell(7);
            cTitulo.SetCellValue("Saldo:");
            ICell cSumatoria = rGranTotal.CreateCell(8);
            cSumatoria.CellStyle = cellStyleSumatoria;
            cSumatoria.SetCellFormula(string.Format("H{0}-I{0}", idRenglon + 3));

            #endregion PieFin

            if (File.Exists(ruta))
            {
                File.Delete(ruta);
            }
            FileStream fs = new FileStream(ruta, FileMode.CreateNew);
            libro.Write(fs);
            fs.Close();            
        }

        public void Crear(vw_EstadoCuenta tEntidad)
        {
            throw new NotImplementedException();
        }

        public void Modificar(vw_EstadoCuenta tEntidad)
        {
            throw new NotImplementedException();
        }

        public void Borrar(vw_EstadoCuenta tEntidad, Enumerados.TipoBorrado TipoBorrado)
        {
            throw new NotImplementedException();
        }


        public DataTable ConsultarTodos()
        {
            throw new NotImplementedException();
        }
    }
}
