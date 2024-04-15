using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ulp_bl;

namespace SIP
{
    public class frmEliminarHabilitarPedidoAspelSaeSip
    {
        public void Show()
        {
            Nullable<int> pedido = DevuelvePedido();
            if (pedido != null)
            {
                if (pedido > 0)
                {
                    string resp = "";
                    resp = EliminarHabilitarPedidoAspelSaeSip.Ejecutar(Convert.ToInt32(pedido), true);
                    if (resp == "")
                    {
                        MessageBox.Show("El pedido ha sido liberado exitosamente.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(resp, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Es necesario capturar un número de Pedido.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
        public static Nullable<int> DevuelvePedido()
        {
            Nullable<int> id = null;
            frmInputBox input = new frmInputBox();
            input.Text = "Cancelación de pedido";
            input.lblTitulo.Text = "Numero de pedido a Cancelar";
            if (input.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (input.NTxtOrden.Text != "")
                {
                    id = Convert.ToInt32(input.NTxtOrden.Text);
                }
                else
                {
                    id = 0;
                }

            }
            return id;
        }
    }

}
