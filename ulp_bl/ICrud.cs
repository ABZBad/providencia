using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ulp_bl
{
    /// <summary>
    /// Esta interfaz se utiliza para estandarizar operaciones básicas
    /// en los objetos de negocio.
    /// </summary>
    /// <typeparam name="T">Se debe especificar el tipo de objeto en el que será implementada esra interface</typeparam>
    interface ICrud<T>
    {   
        /// <summary>
        /// permitirá abanderar si los proceso Crear, Modificar, Consultar o Borrar tuvieron algún error
        /// </summary>
        bool TieneError { get; }
        /// <summary>
        /// Permite obtener la Excepción en caso de que esta ocurra, de esta forma se podrá conocer el detalle del error
        /// </summary>
        Exception Error { get; }
        /// <summary>
        /// Permite consultar la información de un objeto de negocio desde la Base de Datos
        /// </summary>
        /// <param name="ID">Llave primaria de la entidad a consultar</param>
        /// <returns>Mismo tipo de objeto en el que se implementó la interfaz</returns>
        T Consultar(int ID);
        /// <summary>
        /// Método estándar para codificar aquí todo lo que tenga que ver con
        /// el proceso de creación de registros nuevos en la Base de Datos
        /// </summary>
        void Crear(T tEntidad);
        /// <summary>
        /// Método estándar para codificar aquí todo lo que tenga que ver con
        /// el proceso de modificación de registros existentes en la Base de Datos
        /// </summary>
        void Modificar(T tEntidad);
        /// <summary>
        /// /// Método estándar para codificar aquí todo lo que tenga que ver con
        /// el proceso de borrado de registros existentes en la Base de Datos
        /// </summary>
        /// <param name="TipoBorrado">Estable si el borrado será lógico o fisico</param>
        void Borrar(T tEntidad, Enumerados.TipoBorrado TipoBorrado);

        DataTable ConsultarTodos();

    }
}
