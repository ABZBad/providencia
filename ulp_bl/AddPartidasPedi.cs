using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ulp_bl
{
    public class AddPartidasPedi
    {
        public static int NumeroDeTablasSegunTotalTallas(DataTable tallas)
        {
            double totTallas = tallas.Rows.Count;

            if (totTallas <= 14)
            {
                return 1;
            }
            else if (totTallas > 14 && totTallas <= 28)
            {
                return 2;
            }
            else if (totTallas > 28 && totTallas <= 42)
            {
                return 3;
            }
            else if (totTallas > 42 && totTallas <= 56)
            {
                return 4;
            }
            /*
            else if (totTallas > 40 && totTallas <= 50)
            {
                return 5;
            }*/
            else
            {
                return -1;
            }
        }
        public static int NumeroDeTablasSegunTotalTallasModificar(DataTable tallas)
        {
            double totTallas = tallas.Rows.Count;

            if (totTallas <= 10)
            {
                return 1;
            }
            else if (totTallas > 10 && totTallas <= 20)
            {
                return 2;
            }
            else if (totTallas > 20 && totTallas <= 30)
            {
                return 3;
            }
            else if (totTallas > 30 && totTallas <= 40)
            {
                return 4;
            }
            else if (totTallas > 40 && totTallas <= 50)
            {
                return 5;
            }
            else
            {
                return -1;
            }
        }
    }
}
