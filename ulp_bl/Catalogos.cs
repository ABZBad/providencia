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
    public class Catalogos
    {
        public static DataTable GetCatalogoFormaPago()
        {
            try
            {
                String conStr = String.Empty;
                DataTable dataTableForma = new DataTable();
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "[usp_ConsultaFormaPago]";
                dataTableForma = cmd.GetDataTable();
                cmd.Connection.Close();
                return dataTableForma;
            }
            catch
            {
                return null;
            }
        }
        public static DataTable GetCatalogoUsoCFDI()
        {
            try
            {
                String conStr = String.Empty;
                DataTable dataTableUso = new DataTable();
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "[usp_ConsultaUsoCFDI]";
                dataTableUso = cmd.GetDataTable();
                cmd.Connection.Close();
                return dataTableUso;
            }
            catch
            {
                return null;
            }
        }
        public static DataTable GetCatalogoMetodoPago(){
            try
            {
                String conStr = String.Empty;
                DataTable dataTableForma = new DataTable();
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "[usp_ConsultaMetodoPago]";
                dataTableForma = cmd.GetDataTable();
                cmd.Connection.Close();
                return dataTableForma;
            }
            catch
            {
                return null;
            }
        }
        public static DataTable GetCatalogoFormaPagoComision()
        {
            try
            {
                String conStr = String.Empty;
                DataTable dataTableForma = new DataTable();
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "[usp_ConsultaFormaPagoComision]";
                dataTableForma = cmd.GetDataTable();
                cmd.Connection.Close();
                return dataTableForma;
            }
            catch
            {
                return null;
            }
        }
    }
}
