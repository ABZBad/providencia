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



namespace ulp_bl.Reportes
{
    public class CaptCostProcs
    {
        public bool ActualizaDatos(string numPedido)
        {
            System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
            //actualiza datos necesarios que mostrar en la pantalla frmCaptCostProcs posteriormente


            
            //aqui ejecuta stored de proceso de actualización (Faltante por desarrollar en DB)
            
            using (var DbContext = new SIPReportesContext())
            {
                int resultado=0;
                SqlServerCommand _cmd = new SqlServerCommand();
                _cmd.Connection = sm_dl.DALUtil.GetConnection(DbContext.Database.Connection.ConnectionString);
                _cmd.ObjectName = "usp_CaptCostProcs";
                _cmd.Parameters.Add(new SqlParameter("@numPedido", numPedido));
                resultado=_cmd.Execute();
                _cmd.Connection.Close();
                if (resultado!=0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }


            //Si actualizo correctamente entonces manda aviso de mostrar pantalla en verdadero
            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.ElapsedMilliseconds);
        }
    }
}
