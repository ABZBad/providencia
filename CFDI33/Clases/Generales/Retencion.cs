using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDI33.Clases.Generales
{
    public class Retencion
    {
        #region<ATRIBUTOS Y CONSTRUCTORES>
        public Retencion()
        {
            Base = 0;
            Impuesto = string.Empty;
            TipoFactor = string.Empty;
            TasaOCuota = 0;
            Importe = 0;
        }

        private decimal _base;
        private string _impuesto;
        private string _tipoFactor;
        private decimal _tasaOCuota;
        private decimal _importe;

        public decimal Base
        {
            get { return _base; }
            set { _base = value; }
        }
        public string Impuesto
        {
            get { return _impuesto; }
            set { _impuesto = value; }
        }
        public string TipoFactor
        {
            get { return _tipoFactor; }
            set { _tipoFactor = value; }
        }
        public decimal TasaOCuota
        {
            get { return _tasaOCuota; }
            set { _tasaOCuota = value; }
        }
        public decimal Importe
        {
            get { return _importe; }
            set { _importe = value; }
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

            if (string.IsNullOrEmpty(Impuesto))
                result += "Sin Impuesto (Retencion) |";

            if (string.IsNullOrEmpty(TipoFactor))
                result += "Sin Tipo Factor (Retencion) |";

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
