using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ulp_bl;

namespace SIP
{
    public class frmCancelaPedido
    {

        private frmEspera frmEspera;
        private BackgroundWorker bgw = new BackgroundWorker();
        private int iNumeroPedido = 0;
        private String _res = String.Empty;
        public void Show()
        {
            bgw.DoWork += bgw_DoWork;
            bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;

            string numeroPedido = "";
            do
            {
                numeroPedido = SolicitaNumeroPedido();
                if (numeroPedido != "Empty")
                {
                    if (int.TryParse(numeroPedido, out iNumeroPedido))
                    {

                        frmEspera = new frmEspera();
                        frmEspera.Show();
                        bgw.RunWorkerAsync();
                    }
                }
                else
                {
                    MessageBox.Show("Sólo es posible capturar números. Por favor verifíque", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
            } while (numeroPedido == "Empty");

            
        }

        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            frmEspera.Close();
            if (_res=="")
                MessageBox.Show("El pedido ha sido cancelado exitosamente", "Pedido Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show(_res, "Pedido Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //frmEspera = null;
        }

        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            _res = ulp_bl.CancelaPedido.AplicaCancelacionPedido(iNumeroPedido);
            //datosCancelacion = ulp_bl.CancelaPedido.DevuelveCancelacionPedido(iNumeroPedido);
            
        }
        
        public string SolicitaNumeroPedido()
        {
            string referencia = "";
            frmInputBox InputBox = new frmInputBox();
            InputBox.lblTitulo.Text = "Número de pedido a cancelar";
            InputBox.Text = "Cancelación de Pedido";
            if (InputBox.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (InputBox.NTxtOrden.Text != "")
                {
                    referencia = InputBox.NTxtOrden.Text;
                }
                else
                {
                    referencia = "Empty";
                }
                
            }

            return referencia;
        }
    }
}
