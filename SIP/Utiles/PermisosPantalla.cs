using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ulp_bl.Permisos;


namespace SIP.Utiles
{
    public class PermisosPantalla
    {
        private DataTable dtMenus = new DataTable();
        private DataTable dtPermisosMenu = new DataTable();
        public DataTable PermisosScr()
        {
            Utilerias utileriasMenu = new Utilerias();
            utileriasMenu.Permisos = new List<int>();
            dtMenus = utileriasMenu.Menus();
            dtMenus.TableName = "PermisosTabla";

            #region DEF. DE LA TABLA RESULTADO
            

            DataColumn dtColId = new DataColumn("ID", typeof (int));
            DataColumn dtColDesc = new DataColumn("Descripcion", typeof(string));
            DataColumn dtColPuedeEntrar = new DataColumn("PuedeEntrar", typeof(bool));
            DataColumn dtColPuedeInsertar = new DataColumn("PuedeInsertar", typeof(bool));
            DataColumn dtColPuedeModificar = new DataColumn("PuedeModificar", typeof(bool));
            DataColumn dtColPuedeBorrar = new DataColumn("PuedeBorrar", typeof(bool));
            DataColumn dtColTieneHijos = new DataColumn("TieneHijos", typeof(bool));
            DataColumn dtColPE = new DataColumn("PE", typeof(bool));
            DataColumn dtColPI = new DataColumn("PI", typeof(bool));
            DataColumn dtColPM = new DataColumn("PM", typeof(bool));
            DataColumn dtColPB = new DataColumn("PB", typeof(bool));


            dtPermisosMenu.Columns.Add(dtColId);
            dtPermisosMenu.Columns.Add(dtColDesc);
            dtPermisosMenu.Columns.Add(dtColPuedeEntrar);
            dtPermisosMenu.Columns.Add(dtColPuedeInsertar);
            dtPermisosMenu.Columns.Add(dtColPuedeModificar);
            dtPermisosMenu.Columns.Add(dtColPuedeBorrar);
            dtPermisosMenu.Columns.Add(dtColTieneHijos);
            dtPermisosMenu.Columns.Add(dtColPE);
            dtPermisosMenu.Columns.Add(dtColPI);
            dtPermisosMenu.Columns.Add(dtColPM);
            dtPermisosMenu.Columns.Add(dtColPB);



            #endregion

            List<DataRow> _renglones = new List<DataRow>();
             LlenaRenglones(0,"[>] ",ref _renglones);
            
            foreach (DataRow _renglon in _renglones)
            {
                dtPermisosMenu.Rows.Add(_renglon);
            }
            

            return dtPermisosMenu;
        }

        private int LlenaRenglones(int MenuOrigen,string profundidad,ref List<DataRow> _renglones)
        {

            DataRow[] drMenuHijos = dtMenus.Select(string.Format("MenuOrigen={0}", MenuOrigen),"OrdenMenu");
            
            foreach (DataRow drMenuHijo in drMenuHijos)
            {
                DataRow drNuevoRenglon = dtPermisosMenu.NewRow();
                drNuevoRenglon["ID"] = drMenuHijo["Id"];
                drNuevoRenglon["Descripcion"] = profundidad +  drMenuHijo["Descripcion"].ToString();

                drNuevoRenglon["PE"] = drMenuHijo["PuedeEntrar"];
                drNuevoRenglon["PI"] = drMenuHijo["PuedeInsertar"];
                drNuevoRenglon["PM"] = drMenuHijo["PuedeModificar"];
                drNuevoRenglon["PB"] = drMenuHijo["PuedeBorrar"];

                _renglones.Add(drNuevoRenglon);

                int numeroHijos = LlenaRenglones(Convert.ToInt32(drMenuHijo["id"]), "                  " + profundidad, ref _renglones);
                if (numeroHijos > 0)
                {
                    drNuevoRenglon["TieneHijos"] = true;
                }
                else
                {
                    drNuevoRenglon["TieneHijos"] = false;
                }

            }
            return drMenuHijos.GetLength(0);
        }
                
    }
}
