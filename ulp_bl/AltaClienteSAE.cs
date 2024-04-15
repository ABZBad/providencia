using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using sm_dl;
using sm_dl.SqlServer;
using ulp_dl;
//using ulp_dl.aspel_prod30;
using ulp_dl.aspel_sae80;
using ulp_dl.SIPNegocio;
using System.Data.SqlClient;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using ulp_bl.Utiles;
using System.IO;

namespace ulp_bl
{
    public class AltaClienteSAE
    {
        public static DataTable getClientesSAE()
        {

            String conStr = "";
            DataTable dtVend = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                conStr = dbContext.Database.Connection.ConnectionString;
            }
            SqlServerCommand cmd = new SqlServerCommand();
            cmd.Connection = DALUtil.GetConnection(conStr);
            cmd.ObjectName = "usp_getClientesSAE";
            dtVend = cmd.GetDataTable();
            cmd.Connection.Close();
            return dtVend;
        }
        public static DataTable getVendedoresSAE(String Perfil)
        {
            String conStr = "";
            DataTable dtVend = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                conStr = dbContext.Database.Connection.ConnectionString;
            }
            SqlServerCommand cmd = new SqlServerCommand();
            cmd.Connection = DALUtil.GetConnection(conStr);
            cmd.ObjectName = "usp_getVendedoresSAE";
            cmd.Parameters.Add(new SqlParameter("@perfil", Perfil));
            dtVend = cmd.GetDataTable();
            cmd.Connection.Close();
            return dtVend;
        }
        public static DataTable getCatalogoRegimenFiscal(char tipoPersona)
        {
            String conStr = "";
            DataTable dtVend = new DataTable();
            using (var dbContext = new SIPNegocioContext())
            {
                conStr = dbContext.Database.Connection.ConnectionString;
            }
            SqlServerCommand cmd = new SqlServerCommand();
            cmd.Connection = DALUtil.GetConnection(conStr);
            cmd.ObjectName = "usp_getCatalogoRegimenFiscal";
            cmd.Parameters.Add(new SqlParameter("@TipoPersona", tipoPersona));
            dtVend = cmd.GetDataTable();
            cmd.Connection.Close();
            return dtVend;
        }
        public static DataTable getContactosClientesSAE(String Clave)
        {
            DataTable dtContactos = new DataTable();
            using (var dbContext = new AspelSae80Context())
            {
                var query = (from contactos in dbContext.CONTAC01
                             where contactos.CVE_CLIE.Trim() == Clave.Trim()
                             select new
                             {
                                 NCONTACTO = contactos.NCONTACTO,
                                 NOMBRE = contactos.NOMBRE,
                                 DIRECCION = contactos.DIRECCION,
                                 TELEFONO = contactos.TELEFONO,
                                 EMAIL = contactos.EMAIL,
                                 TIPOCONTAC = contactos.TIPOCONTAC,
                                 TIPOCONTACDESC = contactos.TIPOCONTAC == "V" ? "VENTAS" : contactos.TIPOCONTAC == "P" ? "PAGOS" : contactos.TIPOCONTAC == "C" ? "COMPRAS" : ""
                             }).OrderBy(x => x.NOMBRE);
                dtContactos = Linq2DataTable.CopyToDataTable(query);
            }
            return dtContactos;
        }

        public static string setAltaCliente(ulp_bl.CLIE01 objClie, ulp_bl.CLIE_CLIB01 objClieClib)
        {
            try
            {
                using (var dbContext = new AspelSae80Context())
                {
                    dbContext.Configuration.ValidateOnSaveEnabled = false;
                    ulp_dl.aspel_sae80.TBLCONTROL01 control = new TBLCONTROL01();
                    TBLCONTROL01 tblControl = dbContext.TBLCONTROL01.Where(x => x.ID_TABLA == 0).FirstOrDefault();
                    tblControl.ULT_CVE = tblControl.ULT_CVE + 1;



                    ulp_dl.aspel_sae80.CLIE01 clie01 = new ulp_dl.aspel_sae80.CLIE01();
                    clie01.CLAVE = tblControl.ULT_CVE.Value.ToString().PadLeft(10, ' ');
                    clie01.STATUS = "A";
                    clie01.TIPO_EMPRESA = objClie.TIPO_EMPRESA;
                    clie01.RFC = objClie.RFC;
                    clie01.CURP = objClie.CURP;
                    clie01.NOMBRE = objClie.NOMBRE;
                    clie01.CALLE = objClie.CALLE;
                    clie01.NUMEXT = objClie.NUMEXT;
                    clie01.NUMINT = objClie.NUMINT;
                    clie01.CRUZAMIENTOS = objClie.CRUZAMIENTOS;
                    clie01.CRUZAMIENTOS2 = objClie.CRUZAMIENTOS2;
                    clie01.COLONIA = objClie.COLONIA;
                    clie01.REFERDIR = objClie.REFERDIR;
                    clie01.LOCALIDAD = objClie.LOCALIDAD;
                    clie01.CODIGO = objClie.CODIGO;
                    clie01.ESTADO = objClie.ESTADO;
                    clie01.PAIS = objClie.PAIS;
                    clie01.MUNICIPIO = objClie.MUNICIPIO;
                    clie01.NACIONALIDAD = objClie.NACIONALIDAD;
                    clie01.TELEFONO = objClie.TELEFONO;
                    clie01.FAX = objClie.FAX;

                    clie01.CON_CREDITO = objClie.CON_CREDITO;
                    clie01.DIASCRED = objClie.DIASCRED;
                    clie01.LIMCRED = objClie.LIMCRED;
                    clie01.SALDO = objClie.SALDO;
                    clie01.METODODEPAGO = objClie.METODODEPAGO;
                    clie01.NUMCTAPAGO = objClie.NUMCTAPAGO;
                    // clie01.FILIAL = objClie.FILIAL;

                    clie01.DIAPAGO = objClie.DIAPAGO;
                    clie01.DIAREV = objClie.DIAREV;
                    clie01.CVE_VEND = objClie.CVE_VEND;
                    clie01.CUENTA_CONTABLE = objClie.CUENTA_CONTABLE;
                    clie01.DESCUENTO = objClie.DESCUENTO;

                    clie01.IMPRIR = objClie.IMPRIR;
                    clie01.MAIL = objClie.MAIL;
                    clie01.EMAILPRED = objClie.EMAILPRED;
                    clie01.ENVIOSILEN = objClie.ENVIOSILEN;

                    dbContext.CLIE01.Add(clie01);
                    dbContext.TBLCONTROL01.Add(tblControl);
                    dbContext.Entry(tblControl).State = System.Data.Entity.EntityState.Modified;
                    dbContext.SaveChanges();

                    ulp_dl.aspel_sae80.CLIE_CLIB01 clib01 = new ulp_dl.aspel_sae80.CLIE_CLIB01();
                    clib01.CVE_CLIE = clie01.CLAVE;
                    clib01.CAMPLIB9 = objClieClib.CAMPLIB9;
                    dbContext.CLIE_CLIB01.Add(clib01);
                    dbContext.SaveChanges();
                    return clie01.CLAVE;
                }
            }
            catch { return ""; }
        }
        public static void setActualizaCliente(ulp_bl.CLIE01 objClie, ulp_bl.CLIE_CLIB01 objClib)
        {
            using (var dbContext = new AspelSae80Context())
            {
                dbContext.Configuration.ValidateOnSaveEnabled = false;
                ulp_dl.aspel_sae80.CLIE01 clie01 = dbContext.CLIE01.Where(x => x.CLAVE.Trim() == objClie.CLAVE.Trim()).FirstOrDefault();
                ulp_dl.aspel_sae80.CLIE_CLIB01 clib01 = dbContext.CLIE_CLIB01.Where(x => x.CVE_CLIE.Trim() == objClie.CLAVE.Trim()).FirstOrDefault();
                if (clie01 != null)
                {
                    // DATOS DE CLIE01
                    clie01.TIPO_EMPRESA = objClie.TIPO_EMPRESA;
                    clie01.RFC = objClie.RFC;
                    clie01.CURP = objClie.CURP;
                    clie01.REG_FISC = objClie.REG_FISC;
                    clie01.NOMBRE = objClie.NOMBRE;
                    clie01.CALLE = objClie.CALLE;
                    clie01.NUMEXT = objClie.NUMEXT;
                    clie01.NUMINT = objClie.NUMINT;
                    clie01.CRUZAMIENTOS = objClie.CRUZAMIENTOS;
                    clie01.CRUZAMIENTOS2 = objClie.CRUZAMIENTOS2;
                    clie01.COLONIA = objClie.COLONIA;
                    clie01.REFERDIR = objClie.REFERDIR;
                    clie01.LOCALIDAD = objClie.LOCALIDAD;
                    clie01.CODIGO = objClie.CODIGO;
                    clie01.ESTADO = objClie.ESTADO;
                    clie01.PAIS = objClie.PAIS;
                    clie01.MUNICIPIO = objClie.MUNICIPIO;
                    clie01.NACIONALIDAD = objClie.NACIONALIDAD;
                    clie01.TELEFONO = objClie.TELEFONO;
                    clie01.FAX = objClie.FAX;

                    clie01.CON_CREDITO = objClie.CON_CREDITO;
                    clie01.DIASCRED = objClie.DIASCRED;
                    clie01.LIMCRED = objClie.LIMCRED;
                    clie01.SALDO = objClie.SALDO;
                    clie01.METODODEPAGO = objClie.METODODEPAGO;
                    clie01.NUMCTAPAGO = objClie.NUMCTAPAGO;

                    clie01.DIAPAGO = objClie.DIAPAGO;
                    clie01.DIAREV = objClie.DIAREV;
                    clie01.CVE_VEND = objClie.CVE_VEND;
                    clie01.CUENTA_CONTABLE = objClie.CUENTA_CONTABLE;
                    clie01.DESCUENTO = objClie.DESCUENTO;

                    clie01.IMPRIR = objClie.IMPRIR;
                    clie01.MAIL = objClie.MAIL;
                    clie01.EMAILPRED = objClie.EMAILPRED;
                    clie01.ENVIOSILEN = objClie.ENVIOSILEN;

                    // DATOS DE CLIE_CLIB01                    
                    clib01.CAMPLIB9 = objClib.CAMPLIB9;

                    dbContext.CLIE01.Add(clie01);
                    dbContext.CLIE_CLIB01.Add(clib01);
                    dbContext.Entry(clie01).State = System.Data.Entity.EntityState.Modified;
                    dbContext.Entry(clib01).State = System.Data.Entity.EntityState.Modified;
                    dbContext.SaveChanges();
                }
            }
        }

        public static string setAltaContacto(ulp_bl.CONTAC01 objContacto)
        {
            try
            {
                using (var dbContext = new AspelSae80Context())
                {
                    dbContext.Configuration.ValidateOnSaveEnabled = false;
                    ulp_dl.aspel_sae80.TBLCONTROL01 control = new TBLCONTROL01();
                    TBLCONTROL01 tblControl = dbContext.TBLCONTROL01.Where(x => x.ID_TABLA == 59).FirstOrDefault();
                    tblControl.ULT_CVE = tblControl.ULT_CVE + 1;

                    ulp_dl.aspel_sae80.CONTAC01 contact01 = new ulp_dl.aspel_sae80.CONTAC01();
                    contact01.NCONTACTO = (int)tblControl.ULT_CVE.Value;
                    try
                    {
                        if (double.IsNaN(double.Parse(objContacto.CVE_CLIE)))
                            contact01.CVE_CLIE = objContacto.CVE_CLIE;
                        else
                            contact01.CVE_CLIE = objContacto.CVE_CLIE.PadLeft(10, ' ');
                    }
                    catch
                    {
                        contact01.CVE_CLIE = objContacto.CVE_CLIE;
                    }
                    contact01.NOMBRE = objContacto.NOMBRE;
                    contact01.DIRECCION = objContacto.DIRECCION;
                    contact01.TELEFONO = objContacto.TELEFONO;
                    contact01.EMAIL = objContacto.EMAIL;
                    contact01.TIPOCONTAC = objContacto.TIPOCONTAC;
                    contact01.STATUS = objContacto.STATUS;

                    dbContext.CONTAC01.Add(contact01);
                    dbContext.TBLCONTROL01.Add(tblControl);
                    dbContext.Entry(tblControl).State = System.Data.Entity.EntityState.Modified;

                    dbContext.SaveChanges();
                    return contact01.NCONTACTO.ToString();
                }
            }
            catch { return ""; }
        }
        public static void setActualizaContacto(ulp_bl.CONTAC01 objContacto)
        {
            try
            {
                using (var dbContext = new AspelSae80Context())
                {
                    dbContext.Configuration.ValidateOnSaveEnabled = false;
                    ulp_dl.aspel_sae80.CONTAC01 contact01 = new ulp_dl.aspel_sae80.CONTAC01();

                    contact01.NCONTACTO = objContacto.NCONTACTO;
                    try
                    {
                        if (double.IsNaN(double.Parse(objContacto.CVE_CLIE)))
                            contact01.CVE_CLIE = objContacto.CVE_CLIE;
                        else
                            contact01.CVE_CLIE = objContacto.CVE_CLIE.PadLeft(10, ' ');
                    }
                    catch
                    {
                        contact01.CVE_CLIE = objContacto.CVE_CLIE;
                    }

                    contact01.NOMBRE = objContacto.NOMBRE;
                    contact01.DIRECCION = objContacto.DIRECCION;
                    contact01.TELEFONO = objContacto.TELEFONO;
                    contact01.EMAIL = objContacto.EMAIL;
                    contact01.TIPOCONTAC = objContacto.TIPOCONTAC;
                    contact01.STATUS = objContacto.STATUS;

                    dbContext.CONTAC01.Add(contact01);
                    dbContext.Entry(contact01).State = System.Data.Entity.EntityState.Modified;

                    dbContext.SaveChanges();

                }
            }
            catch { }
        }

    }
}
