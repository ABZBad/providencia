using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulp_bl
{
    public class CodigoBarra
    {
        public String UUID { get; set; }
        public int Contador { get; set; }
        public int Consecutivo { get; set; }
        public String Referencia { get; set; }
        public int OrdenProduccion { get; set; }
        public String OrdenMaquila { get; set; }
        public int Almacen { get; set; }
        public String Modelo { get; set; }
        public String Descripcion { get; set; }
        public String Talla { get; set; }
        public String Tipo { get; set; }
        public int Cantidad { get; set; }
        public int Recibidos { get; set; }
        public int Defectuosos { get; set; }
        public DateTime FechaGeneracion { get; set; }
        public DateTime FechaEscaneo { get; set; }
        public String Estatus { get; set; }
        public String UsuarioEscaneo { get; set; }
        public Boolean Procesado { get; set; }

        public CodigoBarra()
        {
            this.UUID = "";
            this.Consecutivo = 1;
            this.Contador = 1;
            this.Referencia = "";
            this.OrdenProduccion = 0;
            this.OrdenMaquila = "";
            this.Almacen = 0;
            this.Modelo = "";
            this.Descripcion = "";
            this.Talla = "";
            this.Tipo = "L";
            this.Cantidad = 0;
            this.Recibidos = 0;
            this.Defectuosos = 0;
            this.FechaGeneracion = DateTime.Now;
            this.FechaEscaneo = DateTime.Now;
            this.Estatus = "G"; //G - GENERADO, E - ESCANEADO, C - CANCELADO
            this.UsuarioEscaneo = "";
            this.Procesado = false;
        }

    }
}
