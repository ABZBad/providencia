using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using ulp_dl;
using ulp_dl.SIPNegocio;

namespace ulp_bl
{
    class InformacionCambios
    {
        public string Campo { get; set; }
        public string ValorAnterior { get; set; }
        public string ValorNuevo { get; set; }  
    }
    public class UpPedidosLog
    {

        public static void RegistraEntrada(LogUPPedidos RegistroOriginal, LogUPPedidos RegistroModificado, string Accion,string Pantalla)
        {
            double? numPedido = (RegistroOriginal != null ? RegistroOriginal.PEDIDO : RegistroModificado.PEDIDO);

            using (var dbContextAudit = new SIPNegocioContext())
            {
                //obtengo el máximo ID de la tabla Log para incrementarlo en 1 y entonces agrupar ambos registos con él.

                try
                {

                    int? currMaxId = dbContextAudit.LogUPPedidos.Where(p => p.PEDIDO == numPedido.Value).Max(i => (int?)i.TRAN_ID);

                    if (currMaxId != null)
                    {
                        currMaxId++;
                    }
                    else
                    {
                        currMaxId = 1;
                    }

                    if (RegistroOriginal != null)
                    {
                        RegistroOriginal.TRAN_ID = (int) currMaxId;
                        RegistroOriginal.SIP80_USER = Globales.UsuarioActual.UsuarioUsuario;
                        RegistroOriginal.WIN_USER = Environment.UserName;
                        RegistroOriginal.PC = Environment.MachineName;
                        RegistroOriginal.DATE = DateTime.Now;
                        RegistroOriginal.ACCION = Accion;
                        RegistroOriginal.PANTALLA = Pantalla;
                        dbContextAudit.LogUPPedidos.Add(RegistroOriginal);
                    }


                    RegistroModificado.TRAN_ID = (int) currMaxId;
                    RegistroModificado.SIP80_USER = Globales.UsuarioActual.UsuarioUsuario;
                    RegistroModificado.WIN_USER = Environment.UserName;
                    RegistroModificado.PC = Environment.MachineName;
                    RegistroModificado.DATE = DateTime.Now;
                    RegistroModificado.ACCION = Accion;
                    RegistroModificado.PANTALLA = Pantalla;

                    dbContextAudit.LogUPPedidos.Add(RegistroModificado);

                    dbContextAudit.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
            }
        }

        public static DataTable RegresaLog(double NumeroPedido)
        {
            DataTable dtLog = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                var logObject = ((from log in dbContext.LogUPPedidos where log.PEDIDO == NumeroPedido select log).OrderBy(o=> o.DATE)).ToArray();

                dtLog = Linq2DataTable.CopyToDataTable(logObject);

            }
            return dtLog;
        }

        public static DataTable RegresaLog2(DataTable LogDeCambios)
        {

            /*
             
            esto es lo que regresa:
             
             * 
             * EJEMPLO:
             * 
             * 
             * 
              
             Campo      |   Valor anterior  |   Valor nuevo |   Fecha de modificación   |   Modificado por  |   Usiario win     |      Nombre Máquina
             -------------------------------------------------------------------------------------------------------------------------------------------
             F. Bordado |                   |   2015-02-04  |       2015-02-04          |        SUP        |       victor      |       VICTOR-PC
             -------------------------------------------------------------------------------------------------------------------------------------------
             F. Costura |                   |   2015-02-06  |       2015-02-06          |        SUP        |       victor      |       VICTOR-PC
              
              
              
             * */

           //Definición de la Tabla resultado:
            DataTable dtLog = new DataTable();

            dtLog.Columns.Add(new DataColumn("Transaccion", typeof(int)));
            dtLog.Columns.Add(new DataColumn("Accion", typeof(string)));
            dtLog.Columns.Add(new DataColumn("Pantalla", typeof(string)));
            dtLog.Columns.Add(new DataColumn("Campo", typeof(string)));
            dtLog.Columns.Add(new DataColumn("ValorAnterior", typeof(string)));
            dtLog.Columns.Add(new DataColumn("ValorNuevo", typeof(string)));
            dtLog.Columns.Add(new DataColumn("FechaModificacion", typeof(DateTime)));
            dtLog.Columns.Add(new DataColumn("ModificadoPor", typeof(string)));
            dtLog.Columns.Add(new DataColumn("UsuarioWin", typeof(string)));
            dtLog.Columns.Add(new DataColumn("Maquina", typeof(string)));

            //obtengo el total de modificaciones que ha tenido este pedido

            var groupedTrans =
                LogDeCambios.AsEnumerable().GroupBy(col => new
                                                        {
                                                            TRAN_ID = col["TRAN_ID"],
                                                            ACCION = col["ACCION"],
                                                            PANTALLA = col["PANTALLA"],
                                                            FECHA_MODIF =Convert.ToDateTime(col["DATE"]).Date,
                                                            WIN_USER = col["WIN_USER"],
                                                            SIP80_USER = col["SIP80_USER"],
                                                            PC = col["PC"]
                                                        });

            foreach (var TRAN in groupedTrans)
            {
                

                List<InformacionCambios> cambios = RegresaInformacionDeCambios((int)TRAN.Key.TRAN_ID, LogDeCambios);

                DateTime fechaModificacion = (from f in LogDeCambios.AsEnumerable() where (int) f["TRAN_ID"] == (int) TRAN.Key.TRAN_ID select new { fModif = (DateTime)f["DATE"] }).FirstOrDefault().fModif;

                foreach (InformacionCambios cambio in cambios)
                {
                    DataRow row = dtLog.NewRow();
                    row["Transaccion"] = TRAN.Key.TRAN_ID;
                    row["Accion"] = TRAN.Key.ACCION;
                    row["Pantalla"] = TRAN.Key.PANTALLA;
                    row["FechaModificacion"] = fechaModificacion;
                    row["ModificadoPor"] = TRAN.Key.SIP80_USER;
                    row["UsuarioWin"] = TRAN.Key.WIN_USER;
                    row["Maquina"] = TRAN.Key.PC;
                    row["Campo"] = cambio.Campo;
                    row["ValorAnterior"] = cambio.ValorAnterior;
                    row["ValorNuevo"] = cambio.ValorNuevo;
                    dtLog.Rows.Add(row);
                }

                

            }
            
            return dtLog;

        }

        private static List<InformacionCambios> RegresaInformacionDeCambios(int TRAN_ID, DataTable LogDeCambios)
        {
            List<InformacionCambios> lstCamposModificador = new List<InformacionCambios>();

            var query = LogDeCambios.AsEnumerable().OrderBy(d=>d["UID"]).Where(c => (int)c["TRAN_ID"] == TRAN_ID);

            DataRow[] rows = query.ToArray();
            DataRow rowOri = rows[0];

            if (rows.Length == 2)
            {
                DataRow rowNew = rows[1];

                foreach (DataColumn columna in LogDeCambios.Columns)
                {
                    if (!"UID, TRAN_ID, WIN_USER, SIP80_USER, PC, DATE, ID, ACCCION, PANTALLA".Contains(columna.ColumnName))
                    {
                        if (rowOri[columna.ColumnName].ToString() != rowNew[columna.ColumnName].ToString())
                        {
                            lstCamposModificador.Add(new InformacionCambios()
                            {
                                Campo = columna.ColumnName,
                                ValorAnterior = rowOri[columna.ColumnName].ToString(),
                                ValorNuevo = rowNew[columna.ColumnName].ToString()
                            });
                        }
                    }
                }
                return lstCamposModificador;
            }
            else
            {
                lstCamposModificador.Add(new InformacionCambios()
                {
                    Campo = "-",
                    ValorAnterior = "-",
                    ValorNuevo = "-"
                });
            }
            return lstCamposModificador;
        }
   }
}
