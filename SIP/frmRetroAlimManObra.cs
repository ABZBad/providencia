using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ulp_bl;

namespace SIP
{
    public class frmRetroAlimManObra
    {
        public void Show()
        {
            int indice = 0;
            DialogResult resp;
            do
            {
                indice = SolcitarNumeroDeIndice();
                if (indice >= 0)
                {
                    int resultado = 0;
                    RetroAlimManObra.CerrarIndice(indice, ref resultado);
                    switch (resultado)
                    {
                        case 0:
                            MessageBox.Show("La órden se ha cerrado corectamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case 1:
                            MessageBox.Show("No se ha encontrado este número de órden", "Verifique", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            break;
                        case 2:
                            MessageBox.Show("Esta orden ya ha sido previamente cerrada","Verifique",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                            break;
                    }                    
                }
                resp = MessageBox.Show("¿Deseas capturar otro ÍNDICE?", "Confirme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);                    

            } while (resp == DialogResult.Yes);
        }

        private int SolcitarNumeroDeIndice()
        {
            int indice = -1;
            frmInputBox frmInputBox = new frmInputBox();
            frmInputBox.Text = "Retroalimentación de mano de obra";
            frmInputBox.lblTitulo.Text = "Numero de ÍNDICE a Cerrar";
            if (frmInputBox.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                indice = Convert.ToInt32(frmInputBox.NTxtOrden.Text);
            }
            return indice;
        }
    }
}
