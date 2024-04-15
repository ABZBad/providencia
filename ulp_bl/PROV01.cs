using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ulp_bl.Utiles;
using ulp_dl;
using ulp_dl.aspel_sae80;

namespace ulp_bl
{
    public class PROV01 : ICrud<PROV01>
    {
        public string CLAVE { get; set; }
        public string STATUS { get; set; }
        public string NOMBRE { get; set; }
        public string RFC { get; set; }
        public string CALLE { get; set; }
        public string NUMINT { get; set; }
        public string NUMEXT { get; set; }
        public string CRUZAMIENTOS { get; set; }
        public string CRUZAMIENTOS2 { get; set; }
        public string COLONIA { get; set; }
        public string CODIGO { get; set; }
        public string LOCALIDAD { get; set; }
        public string MUNICIPIO { get; set; }
        public string ESTADO { get; set; }
        public string CVE_PAIS { get; set; }
        public string NACIONALIDAD { get; set; }
        public string TELEFONO { get; set; }
        public string CLASIFIC { get; set; }
        public string FAX { get; set; }
        public string PAG_WEB { get; set; }
        public string CURP { get; set; }
        public string CVE_ZONA { get; set; }
        public string CON_CREDITO { get; set; }
        public int? DIASCRED { get; set; }
        public double? LIMCRED { get; set; }
        public int? CVE_BITA { get; set; }
        public string ULT_PAGOD { get; set; }
        public double? ULT_PAGOM { get; set; }
        public DateTime? ULT_PAGOF { get; set; }
        public string ULT_COMPD { get; set; }
        public double? ULT_COMPM { get; set; }
        public DateTime? ULT_COMPF { get; set; }
        public double? SALDO { get; set; }
        public double? VENTAS { get; set; }
        public double? DESCUENTO { get; set; }
        public int? TIP_TERCERO { get; set; }
        public int? TIP_OPERA { get; set; }
        public int? CVE_OBS { get; set; }
        public string CUENTA_CONTABLE { get; set; }
        public int? FORMA_PAGO { get; set; }
        public string BENEFICIARIO { get; set; }
        public string TITULAR_CUENTA { get; set; }
        public string BANCO { get; set; }
        public string SUCURSAL_BANCO { get; set; }
        public string CUENTA_BANCO { get; set; }
        public string CLABE { get; set; }
        public string DESC_OTROS { get; set; }
        
        public bool TieneError
        {
            get { throw new NotImplementedException(); }
        }

        public Exception Error
        {
            get { throw new NotImplementedException(); }
        }

        public PROV01 Consultar(int ID)
        {
            PROV01 prov01 = new PROV01();
            
            using (var dbContext = new AspelSae80Context())
            {
                string clave = ID.ToString();
                ulp_dl.aspel_sae80.PROV01 p = dbContext.PROV01.Where(c => c.CLAVE.Trim() == clave).SingleOrDefault();
                CopyClass.CopyObject(p, ref prov01);
            }
            
            return prov01;
        }
        public PROV01 Consultar(string ID,bool FilterStatusB = false)
        {
            PROV01 prov01 = new PROV01();

            using (var dbContext = new AspelSae80Context())
            {
                string clave = ID;
                var query = dbContext.PROV01.Where(c => c.CLAVE.Trim() == clave);
                
                if (FilterStatusB)
                {
                    query = query.Where(s => s.STATUS != "B");
                }
                ulp_dl.aspel_sae80.PROV01 p = query.SingleOrDefault();
                CopyClass.CopyObject(p, ref prov01);
            }

            return prov01;
        }
        public void Crear(PROV01 tEntidad)
        {
            throw new NotImplementedException();
        }

        public void Modificar(PROV01 tEntidad)
        {
            throw new NotImplementedException();
        }

        public void Borrar(PROV01 tEntidad, Enumerados.TipoBorrado TipoBorrado)
        {
            throw new NotImplementedException();
        }


        public System.Data.DataTable ConsultarTodos()
        {
            DataTable dataTableProv01 = new DataTable();
            using (var dbContext = new AspelSae80Context())
            {
                var query = from p in dbContext.PROV01 orderby p.NOMBRE select p;
                dataTableProv01 = Linq2DataTable.CopyToDataTable(query);
            }
            return dataTableProv01;
        }
        public DataTable ConsultarTodoParaBusqueda()
        {


            List<CustomColumnNames> encabezados = new List<CustomColumnNames>();


            encabezados.Add(new CustomColumnNames("CLAVE", "CLAVE"));
            encabezados.Add(new CustomColumnNames("NOMBRE", "NOMBRE"));
            encabezados.Add(new CustomColumnNames("RFC", "RFC"));

            DataTable dataTableProv01 = CustomTable.GetCustomDataTable(ConsultarTodos(), encabezados);

            return dataTableProv01;

        }
    }
}
