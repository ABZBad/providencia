using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data;

namespace SIP
{
    public class frmEliminarPedidosTemporales
    {
     
     

    
        private frmEspera frmEspera;
        private BackgroundWorker bgw = new BackgroundWorker();
        public void Show()
        {
            if (MessageBox.Show("Está seguro que desea eliminar los Pedidos temporales?", "Elimina Pedidos Temporales", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bgw.DoWork += bgw_DoWork;
                bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
                frmEspera = new frmEspera();
                frmEspera.Show();
                bgw.RunWorkerAsync();
            }
        }

        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Pedidos temporales eliminados","Pedidos Temporales Eliminados",MessageBoxButtons.OK, MessageBoxIcon.Information);
            frmEspera.Close();  
        } 

        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            DataTable datosReporte = new DataTable();
            ulp_bl.EliminarPedidosTemporales.EliminaPedidosTempo();
        }
          
    }
}
