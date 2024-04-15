using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using sm_dl.SqlServer;
using ulp_dl.SIPNegocio;
using sm_dl;

namespace ulp_bl
{
    public class GestionCorporativos
    {
        public static DataTable GetCorporativos()
        {
            try
            {
                String conStr = String.Empty;
                DataTable dataTableCorporativos = new DataTable();
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "[usp_GetCorporativos]";
                dataTableCorporativos = cmd.GetDataTable();
                cmd.Connection.Close();
                return dataTableCorporativos;
            }
            catch
            {
                return null;
            }
        }
        public static DataTable GetClientesCorporativos(String _ClaveCorporativo)
        {
            try
            {
                String conStr = String.Empty;
                DataTable dataTableCorporativos = new DataTable();
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "[usp_GetClientesCorporativos]";
                cmd.Parameters.Add(new SqlParameter("@ClaveCorporativo", _ClaveCorporativo));
                dataTableCorporativos = cmd.GetDataTable();
                cmd.Connection.Close();
                return dataTableCorporativos;
            }
            catch
            {
                return null;
            }
        }
        public static DataTable GetArticulosCorporativos(String _ClaveCorporativo)
        {
            try
            {
                String conStr = String.Empty;
                DataTable dataTableCorporativos = new DataTable();
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "[usp_GetArticulosCorporativos]";
                cmd.Parameters.Add(new SqlParameter("@Clave", _ClaveCorporativo));
                dataTableCorporativos = cmd.GetDataTable();
                cmd.Connection.Close();
                return dataTableCorporativos;
            }
            catch
            {
                return null;
            }
        }

        public static bool setAltaCorporativo(String _Nombre)
        {
            try
            {
                String conStr = String.Empty;
                DataTable dataTableCorporativos = new DataTable();
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "[usp_setAltaCorporativo]";
                cmd.Parameters.Add(new SqlParameter("@Nombre", _Nombre));
                int i = cmd.Execute();
                cmd.Connection.Close();
                if (i>0)
                    return true;
                else
                    return false;
            }
            catch { return false; }
        }
        public static bool setBajaCorporativo(int Clave)
        {
            try
            {
                String conStr = String.Empty;
                DataTable dataTableCorporativos = new DataTable();
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "[usp_setBajaCorporativo]";
                cmd.Parameters.Add(new SqlParameter("@Clave", Clave));
                int i = cmd.Execute();
                cmd.Connection.Close();
                if (i > 0)
                    return true;
                else
                    return false;
            }
            catch { return false; }
        }

        public static bool setAltaClientesCorporativos(String _ClaveCliente, int _ClaveCorporativo)
        {
            try
            {
                String conStr = String.Empty;
                DataTable dataTableCorporativos = new DataTable();
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "[usp_setAltaClienteCorporativo]";
                cmd.Parameters.Add(new SqlParameter("@ClaveCliente", _ClaveCliente));
                cmd.Parameters.Add(new SqlParameter("@ClaveCorporativo", _ClaveCorporativo));
                int i = cmd.Execute();
                cmd.Connection.Close();
                if (i>0)
                    return true;
                else
                    return true;
                
            }
            catch { return true; }
        }
        public static bool setAltaArticuloCorporativos(int _ClaveCorporativo, String _ClaveArticulo, String _DescripcionArticulo, decimal _Precio )
        {
            try
            {
                String conStr = String.Empty;
                DataTable dataTableCorporativos = new DataTable();
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "[usp_setAltaArticuloCorporativo]";
                cmd.Parameters.Add(new SqlParameter("@ClaveCorporativo", _ClaveCorporativo));
                cmd.Parameters.Add(new SqlParameter("@ClaveArticulo", _ClaveArticulo));
                cmd.Parameters.Add(new SqlParameter("@Descripcion", _DescripcionArticulo));
                cmd.Parameters.Add(new SqlParameter("@Precio", _Precio));
                
                int i = cmd.Execute();
                cmd.Connection.Close();
                if (i > 0)
                    return true;
                else
                    return true;
                
            }
            catch { return true; }
        }

        public static bool setBajaClientesCorporativos(String _ClaveCliente, int _ClaveCorporativo)
        {
            try
            {
                String conStr = String.Empty;
                DataTable dataTableCorporativos = new DataTable();
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "[usp_setBajaClienteCorporativo]";
                cmd.Parameters.Add(new SqlParameter("@ClaveCliente", _ClaveCliente));
                cmd.Parameters.Add(new SqlParameter("@ClaveCorporativo", _ClaveCorporativo));
                int i = cmd.Execute();
                cmd.Connection.Close();
                if (i > 0)
                    return true;
                else
                    return true;
                
            }
            catch { return true; }
        }
        public static bool setBajaArticuloCorporativos(String _ClaveArticulo, int _ClaveCorporativo)
        {
            try
            {
                String conStr = String.Empty;
                DataTable dataTableCorporativos = new DataTable();
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "[usp_setBajaArticuloCorporativo]";
                cmd.Parameters.Add(new SqlParameter("@ClaveCorporativo", _ClaveCorporativo));
                cmd.Parameters.Add(new SqlParameter("@ClaveArticulo", _ClaveArticulo));
                int i = cmd.Execute();
                cmd.Connection.Close();
                if (i > 0)
                    return true;
                else
                    return true;
                
            }
            catch { return true; }
        }


        public static DataTable GetPreciosEspecialesCliente(String _ClaveCliente, String _ClaveArticulo)
        {
            try
            {
                String conStr = String.Empty;
                DataTable dataTableCorporativos = new DataTable();
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "[usp_getPreciosEspecialesCliente]";
                cmd.Parameters.Add(new SqlParameter("@ClaveCliente", _ClaveCliente));
                cmd.Parameters.Add(new SqlParameter("@Articulo", _ClaveArticulo));
                dataTableCorporativos = cmd.GetDataTable();
                cmd.Connection.Close();
                return dataTableCorporativos;
            }
            catch
            {
                return null;
            }
        }
        public static DataTable GetArticulos()
        {
            try
            {
                String conStr = String.Empty;
                DataTable dataTableCorporativos = new DataTable();
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "[usp_getArticulosSinTalla]";
                dataTableCorporativos = cmd.GetDataTable();
                cmd.Connection.Close();
                return dataTableCorporativos;
            }
            catch
            {
                return null;
            }
        }
        
    }
}
