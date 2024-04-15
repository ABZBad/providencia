using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ulp_dl.SIPNegocio;
using ulp_dl.aspel_sae80;
using ulp_bl.Utiles;

namespace ulp_bl
{
    public class DiasFestivos:ICrud<DiasFestivos>
    {
        public DateTime FECHA_FESTIVO { get; set; }

        public static DataTable ListaDias()
        {
            DataTable datos = new DataTable();
            using (var dbContext=new SIPNegocioContext())
            {
                sm_dl.SqlServer.SqlServerCommand resultado = new sm_dl.SqlServer.SqlServerCommand();
                resultado.Connection = sm_dl.DALUtil.GetConnection(dbContext.Database.Connection.ConnectionString);
                resultado.ObjectName = "usp_DiasFestivos";
                datos = resultado.GetDataTable();
                resultado.Connection.Close();
            }
            return datos;
        }




        public bool TieneError
        {
            get { throw new NotImplementedException(); }
        }

        public Exception Error
        {
            get { throw new NotImplementedException(); }
        }

        public DiasFestivos Consultar(int ID)
        {
            throw new NotImplementedException();
        }
        public DiasFestivos Consultar(DateTime fecha)
        {
            DiasFestivos dias = new DiasFestivos();
            using (var dbContext = new AspelSae80Context())
            {
                var resultado = dbContext.DIASFESTIVOS.Find(fecha.Date);
                CopyClass.CopyObject(resultado, ref dias);                    
            }
            return dias;
        }

        public void Crear(DiasFestivos tEntidad)
        {
            DIASFESTIVOS dia_a_crear = new DIASFESTIVOS();
            using (var dbContext=new AspelSae80Context())
            {
                CopyClass.CopyObject(tEntidad, ref dia_a_crear);
                dbContext.DIASFESTIVOS.Add(dia_a_crear);
                dbContext.SaveChanges();                
            }
        }

        public void Modificar(DiasFestivos tEntidad)
        {
            throw new NotImplementedException();
        }

        public void Borrar(DiasFestivos tEntidad, Enumerados.TipoBorrado TipoBorrado)
        {
            throw new NotImplementedException();
        }

        public DataTable ConsultarTodos()
        {
            throw new NotImplementedException();
        }
    }
}
