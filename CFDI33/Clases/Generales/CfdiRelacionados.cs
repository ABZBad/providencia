using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDI33.Clases.Generales
{
    public class CfdiRelacionados
    {
        #region<ATRIBUTOS Y CONSTRUCTORES>
        public CfdiRelacionados()
        {
            TipoRelacion = string.Empty;
            Cfdi_Relacionados = new List<CfdiRelaccionado>();
        }

        private string _tipoRelacion;
        private List<CfdiRelaccionado> _cfdi_Relacionados;

        public string TipoRelacion
        {
            get { return _tipoRelacion; }
            set { _tipoRelacion = value; }
        }
        public List<CfdiRelaccionado> Cfdi_Relacionados
        {
            get { return _cfdi_Relacionados; }
            set { _cfdi_Relacionados = value; }
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

            if (string.IsNullOrEmpty(TipoRelacion))
                result += "Sin Tipo Relacion (CFDI Relacionados) |";

            if (!Cfdi_Relacionados.Any())
                result += "Sin Cfdi Relacionados (CFDI Relacionados) |";

            return result;
        }
        /// <summary>
        /// Metodo encargado de cortar espacio de todas las propiedades de tipo string
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
