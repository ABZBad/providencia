using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDI33.Clases.Generales
{
    public class CuentaPredial
    {
        #region<ATRIBUTOS Y CONSTRUCTORES>
        public CuentaPredial()
        {
            Numero = string.Empty;
        }

        private string _numero;
        public string Numero
        {
            get { return _numero; }
            set { _numero = value; }
        }
        #endregion
        #region<METODOS>
        /// <summary>
        /// //Metodo encargado de validar los datos de la clase
        /// </summary>
        /// <returns></returns>
        public string valida()
        {
            string result = "";

            if (string.IsNullOrEmpty(Numero))
                result += "Sin Numero (Cuenta Predial) |";

            return result;
        }

        /// <summary>
        /// //Metodo encargado de cortar espacio de todas las propiedades de tipo string
        /// </summary>
        /// <returns></returns>
        public string mondar()
        {
            string result = "";

            result = FuncionesGlobales.mondar(this);

            return result;
        }
        #endregion
    }
}
