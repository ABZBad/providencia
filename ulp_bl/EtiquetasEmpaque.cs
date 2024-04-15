using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ulp_dl;
using ulp_dl.aspel_sae80;

namespace ulp_bl
{
    public class EtiquetasEmpaque
    {
        public static DataTable RegresaEtiquetasEmpaque(int NumeroPedido)
        {
            DataTable dataTableEtiquetasEmpaque = new DataTable();
            using (var dbContext = new AspelSae80Context())
            {
                var query = from ped_mstr in dbContext.PED_MSTR
                            join clie01 in dbContext.CLIE01
                                on ped_mstr.CLIENTE.Trim() equals clie01.CLAVE.Trim()
                            join contac in dbContext.CONTAC01 on clie01.CLAVE equals contac.CVE_CLIE into ps
                            from contact in ps.DefaultIfEmpty()
                            where ped_mstr.PEDIDO == NumeroPedido
                            select new
                            {
                                clie01.CLAVE,
                                clie01.CALLE ,
                                clie01.COLONIA,
                                clie01.MUNICIPIO ,
                                clie01.ESTADO,
                                clie01.CODIGO ,
                                clie01.TELEFONO,
                                ATENCION = contact.NOMBRE,
                                ped_mstr.PEDIDO,
                                RAZON_SOCIAL = clie01.NOMBRE,
                                ped_mstr.CLIENTE,
                                ped_mstr.REMITIDO,
                                ped_mstr.CONSIGNADO
                            };


                dataTableEtiquetasEmpaque = Linq2DataTable.CopyToDataTable(query);


            }
            return dataTableEtiquetasEmpaque;
        }
    }
}
