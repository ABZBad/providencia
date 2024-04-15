using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ulp_bl;

namespace SIP
{
    public class frmRepRecepcionMaquila
    {
        public void Show()
        {
            string idRefAcumulado = "";
            string idAAgregar = "";
            bool repiteCliclo = true;
            bool puedeImprimir = true;
            do
            {
                idAAgregar = DevuelveReferencia();
                switch (idAAgregar)
                {
                    case "xxx":
                        repiteCliclo = false;                        
                        break;
                    case "XXX":
                        repiteCliclo = false;                        
                        break;
                    case "":
                        MessageBox.Show("Es necesario capturar un número de Pedido.","Error",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                        puedeImprimir = false;
                        repiteCliclo = false;
                        break;
                    default:
                        int num=0;
                        if (int.TryParse(idAAgregar,out num))
                        {
                            idRefAcumulado = idRefAcumulado + "(" + idAAgregar + ")";
                            repiteCliclo = true;
                        }
                        else
                        {
                            MessageBox.Show("Sólo es posible capturar números. Por favor verifíque", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            puedeImprimir = false;
                            repiteCliclo = false;
                        }
                        break;
                }
            } while (repiteCliclo);

            if (puedeImprimir)
            {
                string ruta = Path.GetTempFileName().Replace(".tmp", ".xls");
                ulp_bl.Reportes.RepRecepcionMaquila.GeneraArchivoExcel(ulp_bl.Reportes.RepRecepcionMaquila.DevuelveDatosReporte(idRefAcumulado), ruta);
                Utiles.FuncionalidadesFormularios.MostrarExcel(ruta);
            }

 
        }
        public string DevuelveReferencia()
        {
            string referencia = "";
            frmInputBox InputBox = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto);
            InputBox.lblTitulo.Text = "Órdenes a imprimir";
            InputBox.Text = "Para salir presione XXX";
            if (InputBox.ShowDialog()==System.Windows.Forms.DialogResult.OK)
            {
                referencia = InputBox.txtOrden.Text;
            }
            return referencia;
        }
    }
}
