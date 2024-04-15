using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ulp_dl.aspel_sae80;
using ulp_bl.Utiles;
using ulp_dl.SIPNegocio;
using sm_dl;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;

namespace ulp_bl
{
    public class RequisicionMostrador
    {
        public int idRequisicion { get; set; }
        public DateTime fecha { get; set; }
        public int origen { get; set; }
        public int destino { get; set; }
        public bool requiereOP { get; set; }
        public List<DetalleRequisición> detalle { get; set; }

        public RequisicionMostrador()
        {
            this.idRequisicion = 0;
            this.fecha = new DateTime();
            this.origen = 0;
            this.destino = 0;
            this.requiereOP = false;
            this.detalle = new List<DetalleRequisición> { };
        }

        public void AltaRequisicion(string usuario)
        {
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "usp_AltaRequisicionMostrador";
                cmdDatos.Parameters.Add(new SqlParameter("@origen", this.origen));
                cmdDatos.Parameters.Add(new SqlParameter("@destino", this.destino));
                cmdDatos.Parameters.Add(new SqlParameter("@usuario", usuario));
                cmdDatos.Parameters.Add(new SqlParameter("@xmlDetalle", this.GeneraXMLDetalle()));
                SqlParameter _out = new SqlParameter("@out", SqlDbType.Int);
                _out.Direction = ParameterDirection.Output;
                cmdDatos.Parameters.Add(_out);
                cmdDatos.Execute();
                this.idRequisicion = int.Parse(_out.Value.ToString());
            }
        }
        public void AltaRequisicionFaltante(int idRequisicionOrigen, string usuario)
        {
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "usp_AltaRequisicionMostradorFaltante";
                cmdDatos.Parameters.Add(new SqlParameter("@idRequisicionOrigen", idRequisicionOrigen));
                cmdDatos.Parameters.Add(new SqlParameter("@origen", this.origen));
                cmdDatos.Parameters.Add(new SqlParameter("@destino", this.destino));
                cmdDatos.Parameters.Add(new SqlParameter("@usuario", usuario));
                cmdDatos.Parameters.Add(new SqlParameter("@xmlDetalle", this.GeneraXMLDetalle()));
                SqlParameter _out = new SqlParameter("@out", SqlDbType.Int);
                _out.Direction = ParameterDirection.Output;
                cmdDatos.Parameters.Add(_out);
                cmdDatos.Execute();
                this.idRequisicion = int.Parse(_out.Value.ToString());
            }
        }
        private string GeneraXMLDetalle()
        {
            string xmlResult = "<articulos>";
            foreach (RequisicionMostrador.DetalleRequisición detalle in this.detalle)
            {
                foreach (var x in detalle.tallas)
                {
                    xmlResult += "<articulo>";
                    xmlResult += String.Format("<modelo>{0}{1}</modelo>", detalle.modelo, x["talla"]);
                    xmlResult += String.Format("<cantidad>{0}</cantidad>", x["cantidad"]);
                    xmlResult += "</articulo>";
                }
            }
            xmlResult += "</articulos>";

            return xmlResult;
        }
        public DataTable ValidaExistenciasTotalesRequisicion(int tipo)
        {
            DataTable resultado = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand datos = new sm_dl.SqlServer.SqlServerCommand();
                datos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                datos.ObjectName = "usp_ValidaExistenciaTotalRequisicion";
                datos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IdRequisicion", this.idRequisicion));
                datos.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TIPO", tipo +2));
                resultado = datos.GetDataTable();
            }
            return resultado;
        }

        public static DataTable GetReporteRequisicion(int idRequisicion)
        {
            DataTable dt = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand cmdDatos = new sm_dl.SqlServer.SqlServerCommand();
                cmdDatos.Connection = DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                cmdDatos.ObjectName = "usp_ReporteRequisicionMostrador";
                cmdDatos.Parameters.Add(new SqlParameter("@IdRequisicion", idRequisicion));
                dt = cmdDatos.GetDataTable();
            }
            return dt;
        }

        public class DetalleRequisición
        {
            public string modelo { get; set; }
            public List<Dictionary<string, string>> tallas = new List<Dictionary<string, string>> { };

            public DetalleRequisición()
            {
                this.modelo = "";
                this.tallas = new List<Dictionary<string, string>> { };
            }
        }
    }
}
