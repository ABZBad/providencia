using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDI33.Clases.Generales
{
    public class Impuestos
    {
        #region<ATRIBUTOS Y CONSTRUCTORES>
        public Impuestos()
        {
            TotalImpuestosRetenidos = 0;
            TotalImpuestosTrasladados = 0;
            Traslados = new List<Traslado>();
            Retenciones = new List<Retencion>();
        }

        private decimal _totalImpuestosRetenidos;
        private decimal _totalImpuestosTrasladados;
        private List<Traslado> _traslados;
        private List<Retencion> _retenciones;

        public decimal TotalImpuestosRetenidos
        {
            get { return _totalImpuestosRetenidos; }
            set { _totalImpuestosRetenidos = value; }
        }
        public decimal TotalImpuestosTrasladados
        {
            get { return _totalImpuestosTrasladados; }
            set { _totalImpuestosTrasladados = value; }
        }
        public List<Traslado> Traslados
        {
            get { return _traslados; }
            set { 
                _traslados = value;                
            }
        }
        public List<Retencion> Retenciones
        {
            get { return _retenciones; }
            set { _retenciones = value; }
        }                   
        #endregion
        #region<METODOS>
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
