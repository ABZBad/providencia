using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIP.Utiles;
using ulp_bl;
using System.Data;

namespace SIP
{
    public class frmRepExisXAlm
    {
        private frmEspera frmEspera;
        private BackgroundWorker bgw = new BackgroundWorker();
        private int iNumeroAlmacen = 0;
        public void show(){
            bgw.DoWork += bgw_DoWork;
            bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
            string numeroAlmacen = DevuelveNumeroAlmacen();

                        iNumeroAlmacen = 0;
                        if (int.TryParse(numeroAlmacen, out iNumeroAlmacen))
                        {

                            frmEspera = new frmEspera();
                            frmEspera.Show();
                            bgw.RunWorkerAsync();
                        }
                        else
                        {
                            MessageBox.Show("Sólo es posible capturar números. Por favor verifíque", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        
                            
                        }

        }

        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            frmEspera.Close();
        }

        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            DataTable datosReporte = new DataTable();
            datosReporte = ulp_bl.Reportes.RepExisXAlm.DevuelveExistenciaPorAlmacen(iNumeroAlmacen);
            string ruta = Path.GetTempFileName().Replace(".tmp", ".xls");
            ulp_bl.Reportes.RepExisXAlm.GeneraArchivoExcel(datosReporte, ruta);
            //System.Diagnostics.Process.Start(ruta);
            FuncionalidadesFormularios.MostrarExcel(ruta);
        }

        public string DevuelveNumeroAlmacen()
        {
            string referencia = "";
            frmInputBox InputBox = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Texto);
            InputBox.lblTitulo.Text = "Número de pedido a cancelar";
            InputBox.Text = "Reporte de existencias por almacén";
            if (InputBox.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                referencia = InputBox.txtOrden.Text;
            }
            return referencia;
        }
    }
}
