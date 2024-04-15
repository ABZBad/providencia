using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using sm_dl;
using sm_dl.SqlServer;

namespace ulp_bl.Utiles
{
    public class DDL
    {
        public static void Execute(string ConnectionString, string sql)
        {
            SqlServerCommand cmd = new SqlServerCommand();
            cmd.Connection = DALUtil.GetConnection(ConnectionString);
            cmd.ObjectName = "sp_executesql ";
            cmd.Parameters.Add(new SqlParameter("@query", sql));
            cmd.Execute();
            cmd.Connection.Close();
        }

        public static bool Exists(string ConnectionString, string DataBaseObject)
        {
            SqlServerSelectCommand cmd = new SqlServerSelectCommand();
            
            cmd.Connection = DALUtil.GetConnection(ConnectionString);
            cmd.ObjectName = string.Format("select object_id(N'{0}') as object_id",DataBaseObject);
            //cmd.Parameters.Add(new SqlParameter("@object_name", DataBaseObject));
            var result = cmd.GetScalar();
            cmd.Connection.Close();

            if (result == System.DBNull.Value)
            {
                return false;
            }
            else
            {
                return true;
            }


        }
    }
}
