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
    public class BITA01
    {

        public enum ModuloBitacora
        {
            SAE,
            BANCO
        }

        public static void setAltaBitacora(ModuloBitacora _Modulo, DateTime _FechaHora, String _PC, String _orig, String _Usr, String _Mod, String _Opc, String _Op, String _Msg, String _Nivel, String _ov, String _nv)
        {
            //obtenemos la ruta
            String path;
            String fileName;
            if (_Modulo == ModuloBitacora.SAE)
            {
                path = System.Configuration.ConfigurationSettings.AppSettings["Path_Bitacora_SAE"].ToString();
                fileName = "Aspel-SAE 8.00_1_" + DateTime.Now.Day.ToString().PadLeft(2,'0') + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Year.ToString() + ".log"; //18-06-2018
            }
            else if (_Modulo == ModuloBitacora.BANCO)
            {
                path = System.Configuration.ConfigurationSettings.AppSettings["Path_Bitacora_BANCO"].ToString();
                fileName = "Aspel-Banco 4.0_1_" + DateTime.Now.Day.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Year.ToString() + ".log"; //18-06-2018
            }
            else
            {
                return;
            }

            if (!File.Exists(path + fileName))
            {
                var log = File.Create(path + fileName);
                log.Close();
                
            }
            using (StreamWriter sw = new StreamWriter(path + fileName, true, Encoding.Default))
            {
                sw.WriteLine(String.Format("<14>{0} {1} orig=\"{2}\" usr=\"{3}\" mod=\"{4}\" ov=\"{9}\" nv=\"{10}\" opc=\"{5}\" op=\"{6}\" msg=\"{7}\" nivel=\"{8}\"", _FechaHora.ToString("MMM").Replace(".","") + " " +  _FechaHora.ToString("dd") + _FechaHora.ToString(" HH:mm:ss"), _PC, _orig, _Usr.Trim(), _Mod.Trim(),_Opc.Trim(),_Op.Trim(),_Msg.Trim(),_Nivel.Trim(), _ov.Trim(), _nv.Trim()));
                sw.Flush();

            }	




        }

        /*public static void setAltaBitacora(String CVE_CLIE, String CVE_CAMPANIA, String CVE_ACTIVIDAD, DateTime FECHAHORA, String CVE_USUARIO, String OBSERVACIONES, String NOM_USUARIO)
        {
            try
            {
                String conStr = "";

                DataTable dt = new DataTable();
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                    SqlServerCommand cmd = new SqlServerCommand();
                    cmd.Connection = DALUtil.GetConnection(conStr);
                    cmd.ObjectName = "usp_setAltaBitacoraSAE";
                    cmd.Parameters.Add(new SqlParameter("@CVE_CLIE", CVE_CLIE));
                    cmd.Parameters.Add(new SqlParameter("@CVE_CAMPANIA", CVE_CAMPANIA));
                    cmd.Parameters.Add(new SqlParameter("@CVE_ACTIVIDAD", CVE_ACTIVIDAD));
                    cmd.Parameters.Add(new SqlParameter("@FECHAHORA", FECHAHORA));
                    cmd.Parameters.Add(new SqlParameter("@CVE_USUARIO", CVE_USUARIO));
                    cmd.Parameters.Add(new SqlParameter("@OBSERVACIONES", OBSERVACIONES));
                    cmd.Parameters.Add(new SqlParameter("@NOM_USUARIO", NOM_USUARIO));
                   
                    cmd.Execute();
                    cmd.Connection.Close();
                }
            }
            catch { }
        }*/
    }
}
