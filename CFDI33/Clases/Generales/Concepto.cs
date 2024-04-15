using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDI33.Clases.Generales
{
    public class Concepto
    {
        #region<ATRIBUTOS Y CONSTRUCTORES>
        public Concepto()
        {
            ClaveProdServ = string.Empty;
            NoIdentificacion = string.Empty;
            Cantidad = 0;
            ClaveUnidad = string.Empty;
            Unidad = string.Empty;
            Descripcion = string.Empty;
            ValorUnitario = 0;
            Importe = 0;
            Descuento = 0;

            Impuestos = new Impuestos();
            InformacionAduanera = new List<InformacionAduanera>();
            Cuenta_Predial = new CuentaPredial();
            Parte = new List<Parte>();            
        }

        private string _claveProdServ;
        private string _noIdentificacion;
        private decimal _cantidad;
        private string _claveUnidad;
        private string _unidad;
        private string _descripcion;
        private decimal _valorUnitario;
        private decimal _importe;
        private decimal _descuento;
        private Impuestos _impuestos;
        private List<InformacionAduanera> _informacionAduanera;
        private CuentaPredial _cuenta_Predial;
        private List<Parte> _parte;

        public string ClaveProdServ
        {
            get { return _claveProdServ; }
            set { _claveProdServ = value; }
        }
        public string NoIdentificacion
        {
            get { return _noIdentificacion; }
            set { _noIdentificacion = value; }
        }
        public decimal Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }
        public string ClaveUnidad
        {
            get { return _claveUnidad; }
            set { _claveUnidad = value; }
        }
        public string Unidad
        {
            get { return _unidad; }
            set { _unidad = value; }
        }
        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
        public decimal ValorUnitario
        {
            get { return _valorUnitario; }
            set { _valorUnitario = value; }
        }
        public decimal Importe
        {
            get { return _importe; }
            set { _importe = value; }
        }
        public decimal Descuento
        {
            get { return _descuento; }
            set { _descuento = value; }
        }
        public Impuestos Impuestos
        {
            get { return _impuestos; }
            set { _impuestos = value; }
        }
        public List<InformacionAduanera> InformacionAduanera
        {
            get { return _informacionAduanera; }
            set { _informacionAduanera = value; }
        }
        public CuentaPredial Cuenta_Predial
        {
            get { return _cuenta_Predial; }
            set { _cuenta_Predial = value; }
        }
        public List<Parte> Parte
        {
            get { return _parte; }
            set { _parte = value; }
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

            if (string.IsNullOrEmpty(ClaveProdServ))
                result += "Sin Clave Prod Serv (Concepto) |";

            if (string.IsNullOrEmpty(ClaveUnidad))
                result += "Sin Clave Unidad (Concepto) |";

            if (string.IsNullOrEmpty(Descripcion))
                result += "Sin Descripción (Concepto) |";

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
