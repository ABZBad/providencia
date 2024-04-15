﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDI33.Clases.Generales
{
    public class InformacionAduanera
    {
        #region<ATRIBUTOS Y CONSTRUCTORES>
        public InformacionAduanera()
        {
            NumeroPedimento = string.Empty;
        }

        private string _numeroPedimento;

        public string NumeroPedimento
        {
            get { return _numeroPedimento; }
            set { _numeroPedimento = value; }
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

            if (string.IsNullOrEmpty(NumeroPedimento))
                result += "Sin Numero Pedimento (Informacion Aduanera) |";

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
