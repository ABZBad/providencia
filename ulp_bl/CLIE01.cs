using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ulp_dl.aspel_sae80;
using ulp_dl;
using ulp_bl.Utiles;

namespace ulp_bl
{
    public class CLIE01 : ICrud<CLIE01>
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
        public string PAIS { get; set; }
        public string NACIONALIDAD { get; set; }
        public string REFERDIR { get; set; }
        public string TELEFONO { get; set; }
        public string CLASIFIC { get; set; }
        public string FAX { get; set; }
        public string PAG_WEB { get; set; }
        public string CURP { get; set; }
        public string CVE_ZONA { get; set; }
        public string IMPRIR { get; set; }
        public string MAIL { get; set; }
        public int? NIVELSEC { get; set; }
        public string ENVIOSILEN { get; set; }
        public string EMAILPRED { get; set; }
        public string DIAREV { get; set; }
        public string DIAPAGO { get; set; }
        public string CON_CREDITO { get; set; }
        public int? DIASCRED { get; set; }
        public double? LIMCRED { get; set; }
        public double? SALDO { get; set; }
        public int? LISTA_PREC { get; set; }
        public int? CVE_BITA { get; set; }
        public string ULT_PAGOD { get; set; }
        public double? ULT_PAGOM { get; set; }
        public DateTime? ULT_PAGOF { get; set; }
        public double? DESCUENTO { get; set; }
        public string ULT_VENTAD { get; set; }
        public double? ULT_COMPM { get; set; }
        public DateTime? FCH_ULTCOM { get; set; }
        public double? VENTAS { get; set; }
        public string CVE_VEND { get; set; }
        public int? CVE_OBS { get; set; }
        public string TIPO_EMPRESA { get; set; }
        public string MATRIZ { get; set; }
        public string PROSPECTO { get; set; }
        public string CALLE_ENVIO { get; set; }
        public string NUMINT_ENVIO { get; set; }
        public string NUMEXT_ENVIO { get; set; }
        public string CRUZAMIENTOS_ENVIO { get; set; }
        public string CRUZAMIENTOS_ENVIO2 { get; set; }
        public string COLONIA_ENVIO { get; set; }
        public string LOCALIDAD_ENVIO { get; set; }
        public string MUNICIPIO_ENVIO { get; set; }
        public string ESTADO_ENVIO { get; set; }
        public string PAIS_ENVIO { get; set; }
        public string CODIGO_ENVIO { get; set; }
        public string CVE_ZONA_ENVIO { get; set; }
        public string REFERENCIA_ENVIO { get; set; }
        public string CUENTA_CONTABLE { get; set; }
        public string ADDENDAF { get; set; }
        public string ADDENDAD { get; set; }
        public string NAMESPACE { get; set; }
        public string METODODEPAGO { get; set; }
        public string NUMCTAPAGO { get; set; }
        public string REG_FISC { get; set; }

        public bool TieneError
        {
            get { throw new NotImplementedException(); }
        }

        public Exception Error
        {
            get { throw new NotImplementedException(); }
        }
        public CLIE01 Consultar(int ID)
        {
            CLIE01 clie01 = new CLIE01();
            using (var dbContext = new AspelSae80Context())
            {
                dbContext.Database.ExecuteSqlCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
                var cliente = dbContext.CLIE01.Find(ID);
                CopyClass.CopyObject(cliente, ref clie01);
            }
            return clie01;
        }

        public CLIE01 ConsultarIDCadena(string ID)
        {
            CLIE01 clie01 = new CLIE01();
            using (var dbContext = new AspelSae80Context())
            {
                dbContext.Database.ExecuteSqlCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
                var cliente = (from cli in dbContext.CLIE01 where cli.CLAVE.Trim() == ID select cli).FirstOrDefault();
                CopyClass.CopyObject(cliente, ref clie01);
            }
            return clie01;
        }
        public DataTable ConsultarIDCadenaDataTable(string ID)
        {
            DataTable datosCliente = new DataTable();
            using (var dbContext = new AspelSae80Context())
            {
                dbContext.Database.ExecuteSqlCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
                var cliente = from cli in dbContext.CLIE01
                              where cli.CLAVE.Trim() == ID
                              select cli;
                datosCliente = Linq2DataTable.CopyToDataTable(cliente);
            }
            return datosCliente;
        }

        public DataTable Consultar(Usuario usuario, string Nombre)
        {
            string nombreUsuario = usuario.UsuarioUsuario.Trim();
            DataTable clientes = new DataTable();
            using (var dbContext = new AspelSae80Context())
            {
                dbContext.Database.ExecuteSqlCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
                var userDb =
                    (from user in dbContext.USUARIOS
                     where user.CLAVE.Trim() == usuario.UsuarioUsuario
                     select user).SingleOrDefault();
                if (userDb.ACCESO.Trim() == "L")
                {

                    var dbClientes = from cli in dbContext.CLIE01
                                     where cli.NOMBRE.Contains(Nombre) && cli.CVE_VEND.Trim() == nombreUsuario
                                     select new { cli.CLAVE, cli.NOMBRE };

                    clientes = Linq2DataTable.CopyToDataTable(dbClientes);
                }
                else
                {
                    clientes = Consultar("");
                }
            }
            return clientes;
        }
        public DataTable Consultar(string Nombre)
        {
            DataTable clientes = new DataTable();
            using (var dbContext = new AspelSae80Context())
            {
                dbContext.Database.ExecuteSqlCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
                if (Nombre != string.Empty)
                {
                    var dbClientes = from cli in dbContext.CLIE01
                                     where cli.NOMBRE.Contains(Nombre)
                                     select new { cli.CLAVE, cli.NOMBRE };
                    clientes = Linq2DataTable.CopyToDataTable(dbClientes);
                }
                else
                {
                    var dbClientes = from cli in dbContext.CLIE01 select new { cli.CLAVE, cli.NOMBRE };
                    clientes = Linq2DataTable.CopyToDataTable(dbClientes);
                }

            }
            return clientes;
        }


        public void Crear(CLIE01 tEntidad)
        {
            throw new NotImplementedException();
        }

        public void Modificar(CLIE01 tEntidad)
        {
            throw new NotImplementedException();
        }

        public void Borrar(CLIE01 tEntidad, Enumerados.TipoBorrado TipoBorrado)
        {
            throw new NotImplementedException();
        }


        public DataTable ConsultarTodos()
        {
            DataTable datosCliente = new DataTable();
            using (var dbContext = new AspelSae80Context())
            {
                dbContext.Database.ExecuteSqlCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
                var cliente = from cli in dbContext.CLIE01
                              where cli.NOMBRE.Trim() != "" && cli.NOMBRE != null && cli.STATUS == "A"
                              orderby cli.NOMBRE
                              select new
                              {
                                  CLAVE = cli.CLAVE.Trim(),
                                  CLAVENOMBRE = cli.CLAVE.Trim() + " - " + cli.NOMBRE.Trim()
                              };
                datosCliente = Linq2DataTable.CopyToDataTable(cliente);
            }
            return datosCliente;
        }
    }
}
