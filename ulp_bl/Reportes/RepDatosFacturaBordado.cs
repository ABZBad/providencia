using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using sm_dl;
using sm_dl.SqlServer;
using ulp_dl.SIPReportes;
using ulp_dl;
using ulp_dl.aspel_sae80;

namespace ulp_bl.Reportes
{
    public class RepDatosFacturaBordado
    {
        public static DataTable RegresaDatosFacturaBordado(int ClaveMaquilador,string Factura)
        {
            DataTable dataTableDatosFacturaBordado = new DataTable();

            string claveMaquilador = ClaveMaquilador.ToString();
            using (var dbContext = new AspelSae80Context())
            {
                var query = from clie01 in dbContext.CLIE01
                    join ped_mstr in dbContext.PED_MSTR on clie01.CLAVE.Trim() equals ped_mstr.CLIENTE.Trim()
                    join cmt_det in dbContext.CMT_DET on ped_mstr.PEDIDO equals cmt_det.CMT_PEDIDO
                    join imagenes in dbContext.IMAGENES on cmt_det.CMT_COMO equals imagenes.COD_CATALOGO
                    where cmt_det.CMT_MAQUILERO.Trim() == claveMaquilador && cmt_det.CMT_FACT_MAQUILA.Trim() == Factura
                    select new
                    {
                        clie01.NOMBRE,
                        ped_mstr.AGENTE,
                        cmt_det.CMT_PEDIDO,
                        cmt_det.CMT_COMO,
                        cmt_det.CMT_AGRUPADOR,
                        cmt_det.CMT_INDX,
                        imagenes.PUNTADAS,
                        cmt_det.CMT_PRE_PROCESO
                    };

                dataTableDatosFacturaBordado = Linq2DataTable.CopyToDataTable(query);
                
            }
            return dataTableDatosFacturaBordado;
        }

        public static bool ExistenRegistrosMaquiladorVsFactura(int ClaveMaquilador, string Factura)
        {
            if (RegresaDatosFacturaBordado(ClaveMaquilador, Factura).Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static DataTable RegresaDatosFacturaBordadoDetalle(int ClaveMaquilador, string Factura)
        {
            string connStr = "";
            DataTable dataTableFacturaBordadoDetalle = new DataTable();
            using (var dbContext = new SIPReportesContext())
            {
                connStr = dbContext.Database.Connection.ConnectionString;
            }
            SqlServerCommand cmd = new SqlServerCommand();
            cmd.Connection = DALUtil.GetConnection(connStr);
            cmd.ObjectName = "usp_RepFacturaXMaquilador";
            cmd.Parameters.Add(new SqlParameter("@clave_maquilero",ClaveMaquilador ));
            cmd.Parameters.Add(new SqlParameter("@factura", Factura));
            dataTableFacturaBordadoDetalle = cmd.GetDataTable();
            cmd.Connection.Close();

            foreach (DataRow renglon in dataTableFacturaBordadoDetalle.Rows)
            {
                renglon.SetField<string>("TEMP1_PRECIOVENTA_R",Globales.CodificaCifra(Math.Round(Convert.ToDecimal(renglon["TEMP1_PRECIOVENTA"].ToString()),0)));
                renglon.SetField<string>("TEMP1_PRECIOTOTALVENTA_R", Globales.CodificaCifra(Math.Round(Convert.ToDecimal(renglon["TEMP1_PRECIOTOTALVENTA"].ToString()), 0)));
            }

            return dataTableFacturaBordadoDetalle;
        }
    }
}
