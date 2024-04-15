using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using sm_dl;
using sm_dl.SqlServer;
using ulp_bl.Utiles;
using ulp_dl.SIPNegocio;

namespace ulp_bl
{
    public class FlujoControlNotificaciones
    {
        public enum Estatus
        {
            PENDIENTES = 0,
            LEIDAS = 1,
            TODAS = 2
        };

        public int Id { get; set; }
        public String Usuario { get; set; }
        public DateTime FechaCreacion { get; set; }
        public String Area { get; set; }
        public String Modulo { get; set; }
        public String Notificacion { get; set; }
        public DateTime? FechaLectura { get; set; }
        public FlujoControlNotificaciones()
        {
            this.Id = 0;
            this.Usuario = "";
            this.FechaCreacion = new DateTime();
            this.Modulo = "";
            this.Notificacion = "";
            this.FechaLectura = null;
        }
        public FlujoControlNotificaciones(String usuario, String area, String modulo, String notificacion)
        {
            this.Id = 0;
            this.Usuario = usuario;
            this.FechaCreacion = new DateTime();
            this.Area = area;
            this.Modulo = modulo;
            this.Notificacion = notificacion;
            this.FechaLectura = null;
        }

        public void InsertaNotificacion()
        {
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "[usp_setAltaFlujoControlNotificacion]";
                cmdDatos.Parameters.Add(new SqlParameter("@Usuario", this.Usuario));
                cmdDatos.Parameters.Add(new SqlParameter("@Area", this.Area));
                cmdDatos.Parameters.Add(new SqlParameter("@Modulo", this.Modulo));
                cmdDatos.Parameters.Add(new SqlParameter("@Notificacion", this.Notificacion));
                cmdDatos.Execute();
            }
        }
        public static List<FlujoControlNotificaciones> GetNotificacionesByArea(String area, Estatus estatus)
        {
            List<FlujoControlNotificaciones> result = new List<FlujoControlNotificaciones> { };
            //usp_ConsultaAreas
            DataTable dt = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "usp_getFlujoControlNotificacionesByArea";
                cmdDatos.Parameters.Add(new SqlParameter("@Area", area));
                cmdDatos.Parameters.Add(new SqlParameter("@Estatus", (int)estatus));
                dt = cmdDatos.GetDataTable();
                foreach (DataRow dr in dt.Rows)
                {
                    result.Add(new FlujoControlNotificaciones
                    {
                        Id = (int)dr["Id"],
                        Usuario = dr["Usuario"].ToString(),
                        FechaCreacion = (DateTime)dr["FechaCreacion"],
                        Area = dr["Area"].ToString(),
                        Modulo = dr["Modulo"].ToString(),
                        Notificacion = dr["Notificacion"].ToString()
                    });
                }
            }
            return result;
        }
        public static DataTable GetNotificacionesByAreaReport(String area, Estatus estatus)
        {
            List<FlujoControlNotificaciones> result = new List<FlujoControlNotificaciones> { };
            //usp_ConsultaAreas
            DataTable dt = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "usp_getFlujoControlNotificacionesByArea";
                cmdDatos.Parameters.Add(new SqlParameter("@Area", area));
                cmdDatos.Parameters.Add(new SqlParameter("@Estatus", (int)estatus));
                dt = cmdDatos.GetDataTable();
            }
            return dt;
        }
        public static void MarcaNotificacionLeida(int id)
        {
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "usp_setFlujoControlNotificacionLeida";
                cmdDatos.Parameters.Add(new SqlParameter("@Id", id));
                cmdDatos.Execute();
            }
        }
    }
}
