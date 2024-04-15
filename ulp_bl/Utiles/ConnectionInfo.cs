using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ulp_dl.SIPPermisos;
namespace ulp_bl.Utiles
{
    public class ConnectionInfo
    {
        public static string ConnectionString()
        {
            SIPPermisosContext sipContext = new SIPPermisosContext();
            string connectionString = sipContext.Database.Connection.ConnectionString;
            sipContext = null;
            return connectionString;
        }

        public static string DataSource()
        {
            SIPPermisosContext sipContext = new SIPPermisosContext();
            string dataSource = sipContext.Database.Connection.DataSource;
            sipContext = null;
            return dataSource;
        }
    }
}
