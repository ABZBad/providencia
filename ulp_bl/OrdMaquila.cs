using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ulp_dl;
using ulp_dl.aspel_sae80;

namespace ulp_bl
{
    public class OrdMaquila
    {
        public static DataTable RegresaTablaInventario(string Modelo)
        {
            DataTable dataTableInve = new DataTable();
            using (var dbContext = new AspelSae80Context())
            {

                var query = from i in dbContext.INVE01 where i.STATUS != "B" && i.CVE_ART.Substring(0, 9) == Modelo select i;

                dataTableInve = Linq2DataTable.CopyToDataTable(query);

            }
            return dataTableInve;
        }        
    }
}
