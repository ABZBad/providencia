using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ulp_dl;
//using ulp_dl.aspel_prod30;
using ulp_dl.aspel_sae80;

namespace ulp_bl
{
    public class U_DEPARTAMENTO : ICrud<U_DEPARTAMENTO>
    {        
        public int ID { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public string DEPARTAMENTO { get; set; }


        public bool TieneError
        {
            get { throw new NotImplementedException(); }
        }

        public Exception Error
        {
            get { throw new NotImplementedException(); }
        }

        public U_DEPARTAMENTO Consultar(int ID)
        {
            throw new NotImplementedException();
        }

        public void Crear(U_DEPARTAMENTO tEntidad)
        {
            using (var dbContext = new AspelSae80Context())
            {
                ulp_dl.aspel_sae80.U_DEPARTAMENTO u_depto = new ulp_dl.aspel_sae80.U_DEPARTAMENTO();

                u_depto = (ulp_dl.aspel_sae80.U_DEPARTAMENTO)tEntidad;

                dbContext.U_DEPARTAMENTO.Add(u_depto);
                dbContext.SaveChanges();
            }
        }
        public static explicit operator ulp_dl.aspel_sae80.U_DEPARTAMENTO(U_DEPARTAMENTO tEntidad)
        {
            ulp_dl.aspel_sae80.U_DEPARTAMENTO d = new ulp_dl.aspel_sae80.U_DEPARTAMENTO();

            d.DEPARTAMENTO = tEntidad.DEPARTAMENTO;
            d.DESCRIPCION = tEntidad.DESCRIPCION;
            d.ID = tEntidad.ID;
            d.NOMBRE = tEntidad.NOMBRE;
            return d;
        }
        public void Modificar(U_DEPARTAMENTO tEntidad)
        {
            throw new NotImplementedException();
        }

        public void Borrar(U_DEPARTAMENTO tEntidad, Enumerados.TipoBorrado TipoBorrado)
        {
            throw new NotImplementedException();
        }

        public DataTable ConsultarTodos()
        {
            throw new NotImplementedException();
        }

        public DataTable ConsultarDepartamentos()
        {
            DataTable dataTableDepartamentos = new DataTable();
            using (var dbContext = new AspelSae80Context())
            {
                var query = (from d in dbContext.U_DEPARTAMENTO orderby d.DEPARTAMENTO select new { d.DEPARTAMENTO }).Distinct();

                dataTableDepartamentos = Linq2DataTable.CopyToDataTable(query);
                DataRow dr = dataTableDepartamentos.NewRow();
                dr["DEPARTAMENTO"] = "BORDADO, COSTURA E INICIALES";
                dataTableDepartamentos.Rows.Add(dr);

                

            }
            return dataTableDepartamentos;
        }

        public static int SiguienteID()
        {
            int maxID = 0;
            using (var dbContext = new AspelSae80Context())
            {
                maxID = dbContext.U_DEPARTAMENTO.Max(m => m.ID);

            }
            return maxID + 1;
        }
    }
}
