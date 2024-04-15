using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ulp_bl;

namespace SIP
{
    public class frmActualizaPedido
    {
        public void Show()
        {
            int pedido = DevuelveIdPedido();
            if (pedido!=0)
            {
                UPPEDIDOS uppedidos = new UPPEDIDOS();
                uppedidos.PEDIDO = pedido;
                switch (Globales.DatosUsuario.DEPARTAMENTO.Trim().ToUpper().ToString())
                {
                    case "CREDITO":
                        uppedidos.F_CAPT_ASPEL = DateTime.Now;
                        break;
                    case "SURTIDO":
                        UPPEDIDOS busca_pedido = new UPPEDIDOS();
                        busca_pedido = busca_pedido.Consultar(pedido);
                        if (busca_pedido.F_CREDITO==null)
                        {
                            uppedidos.F_CREDITO = DateTime.Now;
                        }
                        else if (busca_pedido.F_ASIG_RUTA==null)
                        {
                            uppedidos.F_ASIG_RUTA = DateTime.Now;
                        }
                        else if (busca_pedido.F_LIBERADO==null)
                        {
                            uppedidos.F_LIBERADO = DateTime.Now;
                        }
                        break;
                    case "EMPAQUE":
                        uppedidos.F_SURTIDO = DateTime.Now;
                        break;
                    case "BORDADO":
                        uppedidos.F_BORDADO = DateTime.Now;
                        break;
                    case "COSTURA":
                        uppedidos.F_COSTURA = DateTime.Now;
                        break;
                    case "ESTAMPADO":
                        uppedidos.F_ESTAMPADO = DateTime.Now;
                        break;
                    case "INICIALES":
                        uppedidos.F_INICIALES = DateTime.Now;
                        break;
                    case "CORTE":
                        uppedidos.F_INICIALES = DateTime.Now;
                        break;
                    default:
                        break;
                }
                uppedidos.Modificar(uppedidos, "Actualizar pedido");
                MessageBox.Show("El proceso ha terminado exitosamente.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private int DevuelveIdPedido()
        {
            int idpedido = 0;
            frmInputBox input = new frmInputBox();
            input.Text = "SIP";
            input.lblTitulo.Text = "Por favor, introduzca un Pedido para actualizar sus \nfechas de acuerdo al Departamento";
            if (input.ShowDialog()==DialogResult.OK)
            {
                idpedido = Convert.ToInt32(input.NTxtOrden.Text);
            }
            return idpedido;
        }
    }
}
