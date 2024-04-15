using System;
using System.Collections.Generic;
using System.Data;
using ulp_bl.Utiles;
using ulp_dl.SIPPermisos;

using System.Security.Cryptography;

namespace ulp_bl.Permisos
{
    public class Utilerias
    {
        public List<int> Permisos { get; set; }
        //public DataTable Menus(int iMenuOrigen)
        public DataTable Menus()
        {
            DataTable dtMenus = new DataTable();
            DataColumn[] colsMenus = {
                                         new DataColumn("Id",typeof(int)),
                                         new DataColumn("Descripcion",typeof(string)),
                                         new DataColumn("MenuOrigen",typeof(int)),
                                         new DataColumn("OrdenMenu",typeof(int)),
                                         new DataColumn("Habilitar",typeof(bool)),
                                         new DataColumn("PuedeEntrar",typeof(bool)),
                                         new DataColumn("PuedeInsertar",typeof(bool)),
                                         new DataColumn("PuedeModificar",typeof(bool)),
                                         new DataColumn("PuedeBorrar",typeof(bool))

                                    };
            dtMenus.Columns.AddRange(colsMenus);

            using (var dataBaseConext = new SIPPermisosContext())
            {
                //foreach (var menu in dataBaseConext.PermisosMenus.Where(m=>m.MenuOrigen==iMenuOrigen))
                
                foreach (var menu in dataBaseConext.PermisosModulos)
                {
                    DataRow drItem = dtMenus.NewRow();                    
                    drItem["Id"] = menu.Id;
                    drItem["Descripcion"] = menu.Descripcion;
                    drItem["MenuOrigen"] = menu.MenuOrigen;
                    drItem["OrdenMenu"] = menu.OrdenMenu;
                    drItem["Habilitar"] = HabilitarMenu(Convert.ToInt32(menu.Id));
                    drItem["PuedeEntrar"] = menu.PuedeEntrar;
                    drItem["PuedeInsertar"] = menu.PuedeInsertar;
                    drItem["PuedeModificar"] = menu.PuedeModificar;
                    drItem["PuedeBorrar"] = menu.PuedeBorrar;
                    dtMenus.Rows.Add(drItem);
                }                
            }


            return dtMenus;
        }
        public bool HabilitarMenu(int iIdMenu)
        {            
            bool bHabilitar=false;
            
            foreach (var iPermiso in Permisos)
            {
                if (iIdMenu==iPermiso)
                {
                    bHabilitar = true;
                    break;
                }                
            }
            return bHabilitar;
        }

        public static string GenerarMD5Hash(string TextToHash)
        {
            //para cifrar la contraseña y compararla con la base de datos

            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();

            byte[] data = System.Text.Encoding.ASCII.GetBytes(TextToHash);
            data = provider.ComputeHash(data);

            string md5 = string.Empty;

            for (int i = 0; i < data.Length; i++)
                md5 += data[i].ToString("x2").ToLower();

            return md5;
        }
       
        
    }
}
