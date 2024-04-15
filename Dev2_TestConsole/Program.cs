using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ulp_bl;


namespace Dev2_TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            DataTable tiposProductos = new DataTable();
            vw_Inventario inventario = new vw_Inventario();
            tiposProductos = inventario.Consultar("PAGAMOSH");
            //int result = ulp_bl.U_DEPARTAMENTO.GetLength("DEPARTAMENTO");

            //            Console.WriteLine(result.ToString());
            Console.ReadLine();
        }
    }
}
