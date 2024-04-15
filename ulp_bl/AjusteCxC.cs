using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using sm_dl;
using sm_dl.SqlServer;
using ulp_dl.SIPReportes;

namespace ulp_bl
{
    
    public class AjusteCxC
    {
        public int ID { set; get; }
        public string CVE_CLIE { get; set; }
        public string REFER { get; set; }
        public string NO_FACTURA { get; set; }
        public double MONTO_AJUSTE { get; set; }
        public int ID_MOV { set; get; }

        public delegate void OnErrorHandler(AjusteCxC AjusteCxC, Exception Ex);
        public delegate void OnSaldoAjustadoHAndler(AjusteCxC AjusteCxC);
        public event OnSaldoAjustadoHAndler OnSaldoAjustado;
        public event OnErrorHandler OnError;

        public static DataTable RegresaSaldos(decimal diferencia)
        {
            string connStr ="";
            using (var dbConntext = new SIPReportesContext())
            {
                connStr = dbConntext.Database.Connection.ConnectionString;
            }


            SqlServerCommand cmd = new SqlServerCommand();
            cmd.Connection = DALUtil.GetConnection(connStr);
            cmd.ObjectName = "usp_RegresaSaldosCxC";
            cmd.Parameters.Add(new SqlParameter("@monto_diferencia", diferencia));
            var dt = cmd.GetDataTable();
            cmd.Connection.Close();
            return dt;
        }

        public void AjustarSaldo(AjusteCxC AjusteCxC)
        {

            AjusteCxC ajusteCxC = new AjusteCxC();
            ajusteCxC.CVE_CLIE = AjusteCxC.CVE_CLIE;
            ajusteCxC.REFER = AjusteCxC.REFER;
            ajusteCxC.NO_FACTURA = AjusteCxC.NO_FACTURA;
            ajusteCxC.MONTO_AJUSTE = AjusteCxC.MONTO_AJUSTE;
            ajusteCxC.ID = AjusteCxC.ID;
            ajusteCxC.ID_MOV = AjusteCxC.ID_MOV;

            
            System.Threading.Thread t = new System.Threading.Thread(AplicaAjuste);
            t.Start(ajusteCxC);            
        }

        private void AplicaAjuste(object p)
        {

           AjusteCxC ajusteCxC = (AjusteCxC)p;

            try
            {
                string connStr = "";
                using (var dbConntext = new SIPReportesContext())
                {
                    connStr = dbConntext.Database.Connection.ConnectionString;
                }


                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(connStr);
                cmd.ObjectName = "usp_ProcesoAjusteSaldo";
                cmd.Parameters.Add(new SqlParameter("@CVE_CLIE", ajusteCxC.CVE_CLIE));
                cmd.Parameters.Add(new SqlParameter("@REFER", ajusteCxC.REFER));
                cmd.Parameters.Add(new SqlParameter("@ID_MOV",ajusteCxC.ID_MOV));
                cmd.Parameters.Add(new SqlParameter("@NO_FACTURA", ajusteCxC.NO_FACTURA));
                cmd.Parameters.Add(new SqlParameter("@MONTO_AJUSTE", ajusteCxC.MONTO_AJUSTE));
                cmd.Execute();
                cmd.Connection.Close();
                //System.Threading.Thread.Sleep(3000);
                if (this.OnSaldoAjustado != null)
                {
                    this.OnSaldoAjustado(ajusteCxC);
                }
            }
            catch(Exception Ex)
            {
                if (this.OnError != null)
                {
                    this.OnError(ajusteCxC, Ex);
                }
            }

        }
    }
}
