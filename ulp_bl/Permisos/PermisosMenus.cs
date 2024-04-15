using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ulp_bl.Utiles;
using ulp_dl;
using ulp_dl.SIPPermisos;

namespace ulp_bl.Permisos
{
    public class PermisosMenus : ICrud<PermisosMenus>
    {

        private bool tieneError;
        private Exception error;

        public int Id { get; set; }
        public string Descripcion { get; set; }
        public decimal MenuOrigen { get; set; }
        public decimal OrdenMenu { get; set; }
        public bool PuedeEntrar { get; set; }
        public bool PuedeInsertar { get; set; }
        public bool PuedeModificar { get; set; }
        public bool PuedeBorrar { get; set; }

        public bool TieneError
        {
            get { return tieneError; }
        }

        public Exception Error
        {
            get { return error; }
        }

        public PermisosMenus Consultar(int ID)
        {
            //DataTable dataTableResultado = new DataTable();
            //IEnumerable<PermisosMenus> menu;
            PermisosMenus menuEx = new PermisosMenus();
            using (var dbContext = new SIPPermisosContext())
            {
                //menu = from p in dbContext.PermisosMenus.AsEnumerable() where p.Id == IdPermiso select p;                
                var menu = dbContext.PermisosModulos.Find(ID);

                CopyClass.CopyObject(menu, ref menuEx);
                // dataTableResultado = Linq2DataTable.CopyToDataTable<PermisosMenus>(menu, null, null);
            }



            //return dataTableResultado;
            return menuEx;
        }

        public void Crear(PermisosMenus tEntidad)
        {
            throw new NotImplementedException();
        }

        public void Modificar(PermisosMenus tEntidad)
        {
            using (var dbContext = new SIPPermisosContext())
            {
                var menu = dbContext.PermisosModulos.Find(tEntidad.Id);                
                //CopyClass.CopyObject(tEntidad, ref menu);
                menu.PuedeBorrar = tEntidad.PuedeBorrar;
                menu.PuedeEntrar = tEntidad.PuedeEntrar;
                menu.PuedeInsertar = tEntidad.PuedeInsertar;
                menu.PuedeModificar = tEntidad.PuedeModificar;
                dbContext.SaveChanges(); 
            }
        }

        public void Borrar(PermisosMenus tEntidad, Enumerados.TipoBorrado TipoBorrado)
        {
            throw new NotImplementedException();
        }

        public void Modificar(decimal idPermiso, bool PuedeEntrar, bool PuedeInsertar, bool PuedeModificar, bool PuedeBorrar)
        {
            try
            {
                using (var dbContext = new SIPPermisosContext())
                {
                    var menu = dbContext.PermisosModulos.Find(idPermiso);
                    menu.PuedeBorrar = PuedeBorrar;
                    menu.PuedeEntrar = PuedeEntrar;
                    menu.PuedeInsertar = PuedeInsertar;
                    menu.PuedeModificar = PuedeModificar;

                    dbContext.SaveChanges();
                }
            }
            catch (Exception Ex)
            {
                tieneError = true;
                error = Ex;
            }
        }
        public DataTable DevuelveAtributosPorModulo(int IDModulo)
        {
            DataTable dataTableAtributos = new DataTable();
            using (var DbContext = new SIPPermisosContext())
            {
                string[] atributosBasicos = new string[] {"Puede_Entrar"};
                var atributos = from a in DbContext.PermisosModuloAtributos where a.ModuloId == IDModulo && !atributosBasicos.Contains(a.AtributoAccion) select a;
                dataTableAtributos = Linq2DataTable.CopyToDataTable(atributos);
            }
            return dataTableAtributos;
        }


        public DataTable ConsultarTodos()
        {
            throw new NotImplementedException();
        }
    }
}
