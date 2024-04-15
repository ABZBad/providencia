using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using sm_dl;
using sm_dl.SqlServer;
using ulp_dl;
//using ulp_dl.aspel_prod30;
using ulp_dl.aspel_sae80;
using ulp_dl.SIPNegocio;
using System.Data.SqlClient;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using ulp_bl.Utiles;
using System.IO;

namespace ulp_bl
{
    public class AltaBeneficiarioBanco
    {
        public static DataTable getBeneficiariosSAE()
        {
            String conStr = "";
            DataTable dt = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                conStr = dbContext.Database.Connection.ConnectionString;
            }
            SqlServerCommand cmd = new SqlServerCommand();
            cmd.Connection = DALUtil.GetConnection(conStr);
            cmd.ObjectName = "usp_getBeneficiariosBanco";
            dt = cmd.GetDataTable();
            cmd.Connection.Close();
            return dt;
        }
        public static String setAltaBeneficiario(BENEF objBenef)
        {
            try
            {
                String conStr = "";

                DataTable dt = new DataTable();
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "usp_setAltaBeneficiarioBanco";
                cmd.Parameters.Add(new SqlParameter("@NOMBRE", objBenef.NOMBRE));
                cmd.Parameters.Add(new SqlParameter("@RFC", objBenef.RFC));
                cmd.Parameters.Add(new SqlParameter("@CTA_CONTAB", objBenef.CTA_CONTAB));
                cmd.Parameters.Add(new SqlParameter("@INF_GENERAL", objBenef.INF_GENERAL));
                cmd.Parameters.Add(new SqlParameter("@REFERENCIA", objBenef.REFERENCIA));
                cmd.Parameters.Add(new SqlParameter("@BANCO", objBenef.BANCO));
                cmd.Parameters.Add(new SqlParameter("@SUCURSAL", objBenef.SUCURSAL));
                cmd.Parameters.Add(new SqlParameter("@CUENTA", objBenef.CUENTA));
                cmd.Parameters.Add(new SqlParameter("@CLABE", objBenef.CLABE));
                cmd.Parameters.Add(new SqlParameter("@ESBANCOEXT", objBenef.ESBANCOEXT));
                cmd.Parameters.Add(new SqlParameter("@BANCODESC", objBenef.BANCODESC));

                SqlParameter _out = new SqlParameter("@OUT", SqlDbType.VarChar, 200);
                _out.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(_out);
                cmd.Execute();
                cmd.Connection.Close();

                return _out.Value.ToString();
            }
            catch
            {
                return "";
            }
        }
        public static void setActualizaBeneficiario(BENEF objBenef)
        {
            try
            {
                String conStr = "";

                DataTable dt = new DataTable();
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "usp_setActualizaBeneficiarioBanco";
                cmd.Parameters.Add(new SqlParameter("@NUM_REG", objBenef.NUM_REG));
                cmd.Parameters.Add(new SqlParameter("@NOMBRE", objBenef.NOMBRE));
                cmd.Parameters.Add(new SqlParameter("@RFC", objBenef.RFC));
                cmd.Parameters.Add(new SqlParameter("@CTA_CONTAB", objBenef.CTA_CONTAB));
                cmd.Parameters.Add(new SqlParameter("@INF_GENERAL", objBenef.INF_GENERAL));
                cmd.Parameters.Add(new SqlParameter("@REFERENCIA", objBenef.REFERENCIA));
                cmd.Parameters.Add(new SqlParameter("@BANCO", objBenef.BANCO));
                cmd.Parameters.Add(new SqlParameter("@SUCURSAL", objBenef.SUCURSAL));
                cmd.Parameters.Add(new SqlParameter("@CUENTA", objBenef.CUENTA));
                cmd.Parameters.Add(new SqlParameter("@CLABE", objBenef.CLABE));
                cmd.Parameters.Add(new SqlParameter("@ESBANCOEXT", objBenef.ESBANCOEXT));
                cmd.Parameters.Add(new SqlParameter("@BANCODESC", objBenef.BANCODESC));


                cmd.Execute();
                cmd.Connection.Close();

            }
            catch { }
        }
    }
}
