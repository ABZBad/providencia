using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ulp_dl.SIPNegocio;
using ulp_dl.aspel_sae80;
using sm_dl.SqlServer;


namespace ulp_bl
{
    public class TransferenciaPorModelo
    {
        public static int incrementaAgrupador()
        {
            int agrupador = 0;
            using (var dbContext=new AspelSae80Context())
            {
                var resultado = (from Ctrl in dbContext.TBLCONTROL01 where Ctrl.ID_TABLA == 32 select Ctrl).FirstOrDefault();
                resultado.ULT_CVE = resultado.ULT_CVE + 1;
                agrupador = Convert.ToInt32(resultado.ULT_CVE);
                dbContext.SaveChanges();
            }
            return agrupador;
        }
        public static DataTable DevuelveDatosRpt(string CVE_FOLIO)
        {
            DataTable datos = new DataTable();
            using (var dbContext=new SIPNegocioContext())
            {
                SqlServerCommand resultado = new SqlServerCommand();
                resultado.Connection = sm_dl.DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                resultado.ObjectName = "usp_frmTransferenciaXModeloRpt";
                resultado.Parameters.Add(new SqlParameter("@CVE_FOLIO", CVE_FOLIO));
                datos = resultado.GetDataTable();
            }
            return datos;
        }
        public static DataTable DevuelveDatosConsulta(string modelo, string clave_origen,string clave_destino)
        {
            DataTable datos = new DataTable();
            using (var dbContext=new SIPNegocioContext())
            {
                SqlServerCommand resultado = new SqlServerCommand();
                resultado.Connection = sm_dl.DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                resultado.ObjectName = "usp_frmTransferenciaXModelo";
                resultado.Parameters.Add(new System.Data.SqlClient.SqlParameter("@modelo", modelo));
                resultado.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Origen", clave_origen));
                resultado.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Destino", clave_destino));
                datos = resultado.GetDataTable();
            }
            return datos;
        }
        public static string ProcesaTransferencia(string CVE_ART, string Origen, string Destino, int cantidad, string usuario, int agrupador)
        {
            string resultado = "";
            using (var dbContext=new SIPNegocioContext())
            {
                SqlServerCommand procesa = new SqlServerCommand();
                procesa.Connection = sm_dl.DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                procesa.ObjectName = "usp_frmTransferenciaXModeloProcesar";
                procesa.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CVE_ART", CVE_ART));
                procesa.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Origen", Origen));
                procesa.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Destino", Destino));
                procesa.Parameters.Add(new System.Data.SqlClient.SqlParameter("@cantidad", cantidad));
                procesa.Parameters.Add(new System.Data.SqlClient.SqlParameter("@usuario", usuario));
                procesa.Parameters.Add(new System.Data.SqlClient.SqlParameter("@agrupador", agrupador));
                SqlParameter paramResult = new SqlParameter("@resultado", SqlDbType.VarChar, 500);
                paramResult.Direction = ParameterDirection.Output;
                procesa.Parameters.Add(paramResult);
                procesa.Execute();
                resultado = paramResult.Value.ToString();                
            }
            return resultado;
        }
        
    }
}
