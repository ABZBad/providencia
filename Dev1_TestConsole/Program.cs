using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using ulp_bl;

using sm_dl.SqlServer;
using sm_dl;

namespace Dev1_TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            SqlServerSelectCommand CMD = new SqlServerSelectCommand();
            CMD.Connection = DALUtil.GetConnection("sa", "0123456789", @"VICTOR-PC\SQLSRV2008R2", "aspel_sae50");
            CMD.ObjectName = "SELECT APP_NAME() AS APPNAME";
            DataTable DT = CMD.GetDataTable();
            CMD.Connection.Close();
        }
    }
}
