using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Data;
using ulp_dl.aspel_sae80;
using ulp_dl;
using ulp_bl.Utiles;
using ulp_dl.SIPNegocio;
using sm_dl.SqlServer;

namespace ulp_bl
{
    public class IMAGENES:ICrud<IMAGENES>
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public string COD_CATALOGO { get; set; }
        public string COD_CLIENTE { get; set; }
        public string COLOR_1 { get; set; }
        public string COLOR_2 { get; set; }
        public string COLOR_3 { get; set; }
        public string COLOR_4 { get; set; }
        public string COLOR_5 { get; set; }
        public string COLOR_6 { get; set; }
        public string COMENTARIOS { get; set; }
        public int? PUNTADAS { get; set; }
        public string COD_MFG { get; set; }

        bool tieneError;
        Exception error;
        public bool TieneError
        {
            get { return tieneError; }
        }

        public Exception Error
        {
            get { return error; }
        }

        public IMAGENES Consultar(int ID)
        {
            throw new NotImplementedException();
        }
        public DataTable ConsultarPorId(int ID)
        {
            DataTable imagenes = new DataTable();
            using (var dbContext = new AspelSae80Context())
            {
                var resultado = from img in dbContext.IMAGENES
                                where img.ID == ID
                                select img;
                imagenes = Linq2DataTable.CopyToDataTable(resultado);
            }
            return imagenes;
        }
        public DataTable Consultar(string COD_CATALOGO)
        {

            DataTable imagenes = new DataTable();
            using (var DbContext = new SIPNegocioContext())
            {
                SqlServerCommand _cmd = new SqlServerCommand();
                sm_dl.SqlEmbedded.Select ASS = new sm_dl.SqlEmbedded.Select();
                _cmd.Connection = sm_dl.DALUtil.GetConnection(DbContext.Database.Connection.ConnectionString);
                _cmd.ObjectName = "usp_Imagenes";                
                _cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("COD_CATALOGO",COD_CATALOGO));
                imagenes = _cmd.GetDataTable();
                _cmd.Connection.Close();
            }
            return imagenes;
        }
        public DataTable ConsultarPorCliente(string COD_CLIENTE)
        {
            DataTable imagenes = new DataTable();
            using (var dbContext = new AspelSae80Context())
            {
                var resultado = from img in dbContext.IMAGENES
                                where img.COD_CLIENTE.Trim() == COD_CLIENTE.Trim()
                                select new
                                {
                                    img.ID,
                                    img.NAME,
                                    COD_CATALOGO = img.COD_CATALOGO.Trim(),
                                    img.COD_CLIENTE,
                                    COLOR_1 = img.COLOR_1.Trim(),
                                    COLOR_2 = img.COLOR_2.Trim(),
                                    COLOR_3 = img.COLOR_3.Trim(),
                                    COLOR_4 = img.COLOR_4.Trim(),
                                    COLOR_5 = img.COLOR_5.Trim(),
                                    COLOR_6 = img.COLOR_6.Trim(),
                                    img.COMENTARIOS,
                                    img.PUNTADAS,
                                    img.COD_MFG

                                };
                imagenes = Linq2DataTable.CopyToDataTable(resultado);
            }

            

            return imagenes;
        }
        public void Crear(IMAGENES tEntidad)
        {
            throw new NotImplementedException();
        }
        public void Crear(IMAGENES tEntidad, ref int idImagen)
        {
            try
            {
                ulp_dl.aspel_sae80.IMAGENES img_a_guardar = new ulp_dl.aspel_sae80.IMAGENES();
                using (var dbContext = new AspelSae80Context())
                {
                    CopyClass.CopyObject(tEntidad, ref img_a_guardar);
                    dbContext.IMAGENES.Add(img_a_guardar);
                    dbContext.SaveChanges();
                    idImagen = img_a_guardar.ID;
                }
            }
            catch (Exception ex)
            {
                error = ex;
                tieneError = true;
            }

        }

        public void Modificar(IMAGENES tEntidad)
        {
            using (var dbContext=new AspelSae80Context())
            {
                var resultado = (from img in dbContext.IMAGENES where img.ID == tEntidad.ID select img).First();
                resultado.COD_CATALOGO = tEntidad.COD_CATALOGO;
                resultado.COD_CLIENTE = tEntidad.COD_CLIENTE;
                resultado.COLOR_1 = tEntidad.COLOR_1;
                resultado.COLOR_2 = tEntidad.COLOR_2;
                resultado.COLOR_3 = tEntidad.COLOR_3;
                resultado.COLOR_4 = tEntidad.COLOR_4;
                resultado.COLOR_5 = tEntidad.COLOR_5;
                resultado.COLOR_6 = tEntidad.COLOR_6;
                resultado.COMENTARIOS = tEntidad.COMENTARIOS;
                resultado.NAME = tEntidad.NAME;
                resultado.PUNTADAS = tEntidad.PUNTADAS;
                dbContext.SaveChanges();
            }
        }

        public void Borrar(IMAGENES tEntidad, Enumerados.TipoBorrado TipoBorrado)
        {
            using (var dbContext = new AspelSae80Context())
            {
                ulp_dl.aspel_sae80.IMAGENES img_a_borrar = dbContext.IMAGENES.Find(tEntidad.ID);                                
                dbContext.IMAGENES.Remove(img_a_borrar);
                dbContext.SaveChanges();
            }
        }


        public DataTable ConsultarTodos()
        {
            throw new NotImplementedException();
        }

        public static List<string> RegresaListaDeLogosCliente()
        {            

            List<string> lstResult = new List<string>();

            using (var dbContext = new AspelSae80Context())
            {
                var queryLogos = from l in dbContext.IMAGENES select new {l.COD_CATALOGO};
                foreach (var logo in queryLogos)
                {
                    lstResult.Add(logo.COD_CATALOGO.Trim());
                }
            }

            return lstResult;
        }
        private static Image LogotiposDevuelveImagen(string ruta, string archivo)
        {
            // string ImagenADesplegar = Directory.GetCurrentDirectory().ToString() + @" \Imagenes\sin_imagen.jpg";
            string ImagenADesplegar = "";
            Image imgADevolver = null;
            if (Directory.Exists(ruta))
            {
                if (File.Exists(ruta + archivo))
                {
                    ImagenADesplegar = ruta + archivo;
                    imgADevolver = null;
                    using (Stream img = File.OpenRead(ImagenADesplegar))
                    {
                        imgADevolver = Image.FromStream(img);
                    }
                }

            }
            System.Diagnostics.Debug.WriteLine("Ruta: " + ImagenADesplegar + " ;");
            return imgADevolver;
        }

        public static Image LogotiposDevueImagen(string CodigoCatalogo)
        {
            string nombreImagen = "";

            using (var dbContext = new AspelSae80Context())
            {
                var query =
                    (from i in dbContext.IMAGENES
                        where i.COD_CATALOGO.Trim() == CodigoCatalogo
                        select new {i.ID, i.COD_CLIENTE}).FirstOrDefault();
                if (query != null)
                {
                    nombreImagen = "CL" + query.COD_CLIENTE.Trim() + "-ID" + query.ID.ToString() + ".jpg";
                }
                else
                {
                    nombreImagen = "";
                }
            }
            if (nombreImagen != "")
            {
                return LogotiposDevuelveImagen(Globales.rutaImagenes, nombreImagen);
            }
            else
            {
                return null;
            }
        }
    }
}
