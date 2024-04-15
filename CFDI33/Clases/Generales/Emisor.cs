using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDI33.Clases.Generales
{
    public class Emisor : Persona
    {
        #region<ATRIBUTOS Y CONSTRUCTORES>
        public Emisor()
        {           
            RegimenFiscal = string.Empty;
            Idcontribuyente = string.Empty;
        }

        private string _regimenFiscal;
        private string _idcontribuyente;

        public string Idcontribuyente
        {
            get { return _idcontribuyente; }
            set { _idcontribuyente = value; }
        }
        public string RegimenFiscal
        {
            get { return _regimenFiscal; }
            set { _regimenFiscal = value; }
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
                result += "Sin RFC (Emisor) |";

            if (string.IsNullOrEmpty(RegimenFiscal))
                result += "Sin Regimen Fiscal (Emisor) |";

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
