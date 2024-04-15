using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sm_dl;
using sm_dl.SqlServer;
using ulp_bl.Utiles;
using ulp_dl.SIPNegocio;
using System.Data;
using System.Data.SqlClient;
using ulp_dl;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.IO;
using ulp_dl.aspel_sae80;

namespace ulp_bl
{
    public class ProspectModule
    {
        public enum Accion
        {
            Alta = 1,
            Edicion = 2,
            Baja = 3
        }

        public static DataTable GetCatalogoModelosDescripcion(ref Exception Ex)
        {
            String conStr = "";
            DataTable dtModelos = new DataTable();
            try
            {
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "Prospect.usp_getModelosDescripcion";
                dtModelos = cmd.GetDataTable();
                cmd.Connection.Close();
                Ex = null;
                return dtModelos;
            }
            catch (Exception ex)
            {
                Ex = ex;
                return null;
            }
        }
        public static DataTable SetCatalogoModelosDescripcion(String Clave, String Descripcion, String Tallas, Boolean Activo, Accion Accion, String Usuario, ref Exception Ex)
        {
            String conStr = "";
            DataTable dtModelos = new DataTable();
            try
            {
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "Prospect.usp_setModelosDescripcion";
                cmd.Parameters.Add(new SqlParameter("@Clave", Clave));
                cmd.Parameters.Add(new SqlParameter("@Descripcion", Descripcion));
                cmd.Parameters.Add(new SqlParameter("@Tallas", Tallas));
                cmd.Parameters.Add(new SqlParameter("@Activo", Activo));
                cmd.Parameters.Add(new SqlParameter("@Accion", (int)Accion));
                cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                cmd.Execute();
                cmd.Connection.Close();
                Ex = null;
                return dtModelos;
            }
            catch (Exception ex)
            {
                Ex = ex;
                return null;
            }
        }
        public static Boolean ValidaClaveExistente(String Clave)
        {
            using (var dbContext = new AspelSae80Context())
            {
                var resultado = from res in dbContext.INVE01 where res.CVE_ART.Substring(0, 8).Trim().ToUpper() == Clave.Trim().ToUpper() select res;
                return resultado.Any();
            }
        }
    }
}
