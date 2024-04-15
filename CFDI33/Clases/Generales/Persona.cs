using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDI33.Clases.Generales
{
    public class Persona
    {
        #region<ATRIBUTOS Y CONSTRUCTORES>
        private string _rfc;
        private string _nombre;

        public string RFC
        {
            get { return _rfc; }
            set { _rfc = value; }
        }
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public Persona()
        {
            RFC = string.Empty;
            Nombre = string.Empty;
        }
        #endregion
    }
}
