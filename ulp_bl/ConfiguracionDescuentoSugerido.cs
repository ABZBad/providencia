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
    public class ConfiguracionDescuentoSugerido
    {
        public static DataTable GetTablaDescuentosYPreciosSugeridos()
        {
            try
            {
                String conStr = String.Empty;
                DataTable dataTableDescuentos = new DataTable();
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "[usp_DescuentosPreciosSugeridos]";
                dataTableDescuentos = cmd.GetDataTable();
                cmd.Connection.Close();
                return dataTableDescuentos;
            }
            catch
            {
                return null;
            }
        }
        public static void SetDescuentoSugerido(int ID, Decimal Descuento)
        {
            try
            {
                String conStr = String.Empty;
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "[usp_UpdateDescuentoSugerido]";
                cmd.Parameters.Add(new SqlParameter("@Id", ID));
                cmd.Parameters.Add(new SqlParameter("@PorcentajeDescuento", Descuento));
                cmd.Execute();
                cmd.Connection.Close();
            }
            catch
            {
            }
        }
        public static void SetPrecioSugerido(int ID, Decimal Precio)
        {
            try
            {
                String conStr = String.Empty;
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "[usp_UpdatePrecioSugerido]";
                cmd.Parameters.Add(new SqlParameter("@Id", ID));
                cmd.Parameters.Add(new SqlParameter("@PrecioDescuento", Precio));
                cmd.Execute();
                cmd.Connection.Close();
            }
            catch
            {
            }
        }

        public static void SetAlta(decimal RangoMin, decimal RangoMax, decimal Descuento, decimal Precio)
        {
            try
            {
                String conStr = String.Empty;
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "[usp_AltaDescuentoPrecioSugerido]";
                cmd.Parameters.Add(new SqlParameter("@RangoMin", RangoMin));
                cmd.Parameters.Add(new SqlParameter("@RangoMax", RangoMax));
                cmd.Parameters.Add(new SqlParameter("@Descuento", Descuento));
                cmd.Parameters.Add(new SqlParameter("@Precio", Precio));
                cmd.Execute();
                cmd.Connection.Close();
            }
            catch
            {
            }
        }
        public static void SetBaja(int ID)
        {
            try
            {
                String conStr = String.Empty;
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                SqlServerCommand cmd = new SqlServerCommand();
                cmd.Connection = DALUtil.GetConnection(conStr);
                cmd.ObjectName = "[usp_BajaDescuentoPrecioSugerido]";
                cmd.Parameters.Add(new SqlParameter("@Id", ID));
                cmd.Execute();
                cmd.Connection.Close();
            }
            catch
            {
            }
        }

        public static DataTable GetPrecioDescuentoCliente(String Cliente, DateTime Fecha, ref Exception EX)
        {
            try
            {
                String conStr = String.Empty;
                DataTable dataTableDescuentos = new DataTable();
                using (var dbContext = new SIPNegocioContext())
                {
                    conStr = dbContext.Database.Connection.ConnectionString;
                }
                using (SqlConnection con =new SqlConnection(conStr))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "select * from udf_CalculaDescuentoYPrecioSugerido('" + @Cliente + "','" + @Fecha.ToString("dd/MM/yyyy") + "')";
                        //cmd.Parameters.Add(new SqlParameter("@Cliente", Cliente));
                       //cmd.Parameters.Add(new SqlParameter("@Fecha",Fecha));
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds=new DataSet();
                        da.Fill(ds);
                        if (ds != null)
                        {
                            dataTableDescuentos = ds.Tables[0];
                            EX = null;
                            con.Close();
                            return dataTableDescuentos;
                        }
                        else
                            return null;
                    }
                }

            }
            catch(Exception ex)
            {
                EX = ex;
                return null;
            }
        }

    }
}
