using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ulp_bl;

namespace SIP
{
    public class frmProcOrdenProcEntFrabrica
    {
        public void Show()
        {
            string id;
            DialogResult respuesta = DialogResult.No;
            do
            {
                id = SolcitarNumeroDeOrden();
                if (id!="")
                {
                    int idNum = 0;
                    int.TryParse(id, out idNum);
                    if (idNum!=0)
                    {
                        string resultado = RepOrdProd.EnviaAProduccion(id);
                        if (resultado=="")
                        {
                            MessageBox.Show("La liberación manual ha concluido exitosamente", "",MessageBoxButtons.OK,MessageBoxIcon.Information);
                            respuesta = MessageBox.Show("¿Deseas capturar otra Órden?", "Confirme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);                                                                        
                        }
                        else
                        {
                            MessageBox.Show(resultado, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Solo es posible capturar números. Por favor verifíque", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("Es necesario capturar un número de Pedido.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            } while (respuesta == DialogResult.Yes);
        }

        private string SolcitarNumeroDeOrden()
        {
            string id = "";
            frmInputBox frmInputBox = new frmInputBox();
            frmInputBox.Text = "Órdenes";
            frmInputBox.lblTitulo.Text = "Órden a liberar";
            if (frmInputBox.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                id = frmInputBox.NTxtOrden.Text;
            }
            return id;
        }
    }
}
