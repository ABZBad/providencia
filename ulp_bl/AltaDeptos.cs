using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulp_bl
{
    public class AltaDeptos
    {
        public static void CreaDepartamento(string Nombre,string Descripcion, string Departamento)
        {
            U_DEPARTAMENTO u_depto = new U_DEPARTAMENTO();

            u_depto.ID = U_DEPARTAMENTO.SiguienteID();
            u_depto.NOMBRE = Nombre;
            u_depto.DESCRIPCION = Descripcion;
            u_depto.DEPARTAMENTO = Departamento;
            u_depto.Crear(u_depto);


        }
    }
}
