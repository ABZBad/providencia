using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using SIP.Utiles;

namespace SIP
{
    public class frmRepCostoUltimoPT
    {
        private frmEspera frmEspera;
        private BackgroundWorker bgw = new BackgroundWorker();
        public void show()
        {
            bgw.DoWork += bgw_DoWork;
            bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
            frmEspera = new frmEspera();
            frmEspera.Show();
            bgw.RunWorkerAsync();
        }

        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            frmEspera.Close();
        }

        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            DataTable datosReporte = new DataTable();
            datosReporte = ulp_bl.Reportes.RepCostoUltimoPT.DevuelveCosteoPT();
            string ruta = Path.GetTempFileName().Replace(".tmp", ".xls");
            ulp_bl.Reportes.RepCostoUltimoPT.GeneraArchivoExcel(datosReporte, ruta);
            //System.Diagnostics.Process.Start(ruta);
            FuncionalidadesFormularios.MostrarExcel(ruta);
        }
    }
}
