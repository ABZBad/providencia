using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDI33.Clases.Generales
{
    public class CfdiRelaccionado
    {
        #region<ATRIBUTOS Y CONSTRUCTORES>
        private string _uuid;

        public string UUID
        {
            get { return _uuid; }
            set { _uuid = value; }
        }

        public CfdiRelaccionado()
        {
            UUID = string.Empty;
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

            if (string.IsNullOrEmpty(UUID))
                result += "Sin UUID (CFDI Relacionado) |";

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
