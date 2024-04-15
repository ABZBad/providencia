using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDI33.Clases.Generales
{
    public class Receptor : Persona
    {
        #region<ATRIBUTOS Y CONSTRUCTORES>
        public Receptor()
        {           
            ResidenciaFiscal = string.Empty;
            NumRegIdTrib = string.Empty;
            UsoCFDI = string.Empty;
            ClaveReceptor = string.Empty;
            Email = string.Empty;
        }

        private string _residenciaFiscal;
        private string _numRegIdTrib;
        private string _usoCFDI;
        private string _claveReceptor;
        private string _email;

        public string ClaveReceptor
        {
            get { return _claveReceptor; }
            set { _claveReceptor = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string ResidenciaFiscal
        {
            get { return _residenciaFiscal; }
            set { _residenciaFiscal = value; }
        }
        public string NumRegIdTrib
        {
            get { return _numRegIdTrib; }
            set { _numRegIdTrib = value; }
        }
        public string UsoCFDI
        {
            get { return _usoCFDI; }
            set { _usoCFDI = value; }
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

            if (string.IsNullOrEmpty(RFC))
                result += "Sin RFC (Receptor) |";

            if (string.IsNullOrEmpty(UsoCFDI))
                result += "Sin Uso CFDI (Receptor) |";

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
