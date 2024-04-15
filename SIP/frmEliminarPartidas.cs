using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ulp_bl;
using System.Globalization;

namespace SIP
{
    public partial class frmEliminarPartidas : Form
    {

        Enumerados.TipoPedido tipoPedido;
        string cliente;
        int idPedido;
        DataTable procesos = new DataTable();
        bool procesoNuevo = false;
        bool procesoModificado = false;
        public frmEliminarPartidas(string id_cliente, int id, Enumerados.TipoPedido tipo)
        {
            InitializeComponent();
            Cursor.Current = Cursors.WaitCursor;
            cliente = id_cliente;
            tipoPedido = tipo;
            idPedido = id;
            GuardarPed_Temp();
            LlenaGridPartidas();
            if (tipoPedido == Enumerados.TipoPedido.OrdenTrabajo)
            {
                btnAgregarProceso.Visible = false;
                btnGuardarProceso.Visible = false;
                this.Text = "Eliminar partidas OT";
            }
            if (tipoPedido == Enumerados.TipoPedido.PedidoDAT || tipoPedido == Enumerados.TipoPedido.PedidoMOS || tipoPedido == Enumerados.TipoPedido.PedidoMOSCP)
            {
                this.dgViewPartidas.Columns["DESCUENTO"].Visible = true;
            }

            Cursor.Current = Cursors.Default;

        }
        private void GuardarPed_Temp()
        {
            PED_TEMP ped_temp = new PED_TEMP();
            ped_temp.PEDIDO = Convert.ToInt32(idPedido);
            ped_temp.CrearPorPedido(ped_temp, tipoPedido);
        }

        private void frmEliminarPartidas_Load(object sender, EventArgs e)
        {

        }

        private void btnEliminarPartidaYProcesos_Click(object sender, EventArgs e)
        {
            if (dgViewPartidas.Rows.Count > 0)
            {

                if (MessageBox.Show("Está seguro de querer eliminar esta partida?", "Borrado de partidas", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    CMT_DET cmt_detABorrar = new CMT_DET();
                    PED_DET ped_detABorrar = new PED_DET();
                    PED_TEMP ped_tempABorrar = new PED_TEMP();
                    string agrupador = dgViewPartidas.CurrentRow.Cells[4].Value.ToString();
                    string modelo = dgViewPartidas.CurrentRow.Cells["MODELO"].Value.ToString();
                    cmt_detABorrar.CMT_PEDIDO = idPedido;
                    cmt_detABorrar.CMT_AGRUPADOR = agrupador;
                    cmt_detABorrar.Borrar(cmt_detABorrar, Enumerados.TipoBorrado.Fisico);

                    ped_detABorrar.PEDIDO = idPedido;
                    ped_detABorrar.AGRUPADOR = agrupador;
                    ped_detABorrar.CODIGO = modelo;
                    ped_detABorrar.Borrar(ped_detABorrar, Enumerados.TipoBorrado.Fisico);

                    ped_tempABorrar.PEDIDO = idPedido;
                    ped_tempABorrar.AGRUPADOR = agrupador;
                    ped_tempABorrar.MODELO = modelo;
                    ped_tempABorrar.Borrar(ped_tempABorrar, Enumerados.TipoBorrado.Fisico);
                    LlenaGridPartidas();
                    LlenaGridProcesos();
                }
            }
            else
            {
                MessageBox.Show("No hay registros para eliminar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void LlenaGridPartidas()
        {
            DataTable partidas = new DataTable();
            PED_TEMP partidasPedido = new PED_TEMP();
            partidas = partidasPedido.ConsultarPartidasPedido(idPedido);
            dgViewPartidas.DataSource = partidas;
        }
        private void LlenaGridProcesos()
        {
            if (dgViewPartidas.Rows.Count > 0)
            {
                string agrupador = dgViewPartidas.CurrentRow.Cells[4].Value.ToString();
                CMT_DET procesosBd = new CMT_DET();
                procesosBd.CMT_PEDIDO = idPedido;
                procesosBd.CMT_AGRUPADOR = agrupador;
                procesos = procesosBd.ConsultarProcesosPartida(procesosBd);
                dgViewProcesos.DataSource = procesos;
                DeshabilitaColumnaProcesosEmpaque();
                DeshabilitaColumnaProcesosEmbarque();
            }
            else
            {
                dgViewProcesos.DataSource = null;
            }
        }

        private void btnAgregarProceso_Click(object sender, EventArgs e)
        {
            if (dgViewPartidas.Rows.Count > 0)
            {
                gpBoxProceso.Visible = true;
                habilitaControles(false);
            }
        }

        private void btnParamProcesoAceptar_Click(object sender, EventArgs e)
        {
            if ("BCEIRFLMQ".Contains(txtParamPartida.Text) || "bceirflmq".Contains(txtParamPartida.Text))
            {
                gpBoxProceso.Visible = false;
                habilitaControles(true);
                btnGuardarProceso.Visible = true;
                btnAgregarProceso.Visible = false;
                DataRow rAnadido = procesos.Rows.Add();
                rAnadido[0] = txtParamPartida.Text.ToUpper();
                rAnadido["CMT_MODELO"] = dgViewPartidas.CurrentRow.Cells["Modelo"].Value;
                rAnadido["CMT_CANTIDAD"] = dgViewPartidas.CurrentRow.Cells["PRENDAS"].Value;
                rAnadido["CMT_MAQUILERO"] = "1";
                dgViewProcesos.Focus();
                dgViewProcesos.Rows[dgViewProcesos.Rows.Count - 1].Cells[1].Selected = true;
                procesoNuevo = true;
                if (txtParamPartida.Text.Trim().ToUpper() == "Q")
                {
                    // BUSCAMOS SI YA EXISTE UN PROCESO "DONDE" PARA ASIGNAR EL MISMO VALOR
                    CMT_DET procesoEmbarque = BuscaDireccionEmbarque(idPedido);
                    if (procesoEmbarque != null)
                    {
                        rAnadido["CMT_DONDE"] = procesoEmbarque.CMT_DONDE;
                        rAnadido["CMT_PRE_PROCESO"] = procesoEmbarque.CMT_PRE_PROCESO.ToString();
                        procesoNuevo = false;
                    }
                }
                txtParamPartida.Text = "";
            }
            else
            {
                MessageBox.Show("Proceso no válido");
            }
            DeshabilitaColumnaProcesosEmpaque();
            DeshabilitaColumnaProcesosEmbarque();
        }
        private void habilitaControles(bool habilita)
        {
            dgViewPartidas.Enabled = habilita;
            dgViewProcesos.Enabled = habilita;
            btnAgregarProceso.Enabled = habilita;
            btnGuardarProceso.Enabled = habilita;
            btnEliminarPartidaYProcesos.Enabled = habilita;
        }

        private void dgViewPartidas_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            LlenaGridProcesos();
        }

        private void btnParamProcesoCancelar_Click(object sender, EventArgs e)
        {
            gpBoxProceso.Visible = false;
            txtParamPartida.Text = "";
            habilitaControles(true);
        }
        private void ModificaProceso(int idRgistro)
        {
            procesoModificado = false;
            string agrupador = dgViewPartidas.CurrentRow.Cells[4].Value.ToString();

            CMT_DET cmt_detGuardaProc = new CMT_DET();
            cmt_detGuardaProc.CMT_INDX = Convert.ToInt32(dgViewProcesos.Rows[idRgistro].Cells[6].Value.ToString());
            cmt_detGuardaProc.CMT_PROCESO = dgViewProcesos.Rows[idRgistro].Cells[0].Value.ToString();
            cmt_detGuardaProc.CMT_AGRUPADOR = agrupador;
            cmt_detGuardaProc.CMT_PEDIDO = idPedido;
            cmt_detGuardaProc.CMT_CMMT = dgViewProcesos.Rows[idRgistro].Cells[1].Value.ToString();
            cmt_detGuardaProc.CMT_COMO = dgViewProcesos.Rows[idRgistro].Cells[2].Value.ToString();
            cmt_detGuardaProc.CMT_DONDE = dgViewProcesos.Rows[idRgistro].Cells[3].Value.ToString();

            decimal pre_proceso = 0;
            decimal.TryParse(dgViewProcesos.Rows[idRgistro].Cells[4].Value.ToString(), out pre_proceso);
            cmt_detGuardaProc.CMT_PRE_PROCESO = pre_proceso;
            cmt_detGuardaProc.Modificar(cmt_detGuardaProc);
            if (cmt_detGuardaProc.TieneError)
            {
                Cursor = Cursors.Default;
                MessageBox.Show(Properties.Resources.Cadena_ErrorAlGuardar + "\n\r\n\r" +
                                cmt_detGuardaProc.Error, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
        }
        private void CreaProceso(int idRgistro)
        {
            procesoNuevo = false;
            string agrupador = dgViewPartidas.CurrentRow.Cells[4].Value.ToString();

            CMT_DET cmt_detGuardaProc = new CMT_DET();
            cmt_detGuardaProc.CMT_PROCESO = dgViewProcesos.Rows[idRgistro].Cells[0].Value.ToString();
            cmt_detGuardaProc.CMT_AGRUPADOR = agrupador;
            cmt_detGuardaProc.CMT_PEDIDO = idPedido;
            cmt_detGuardaProc.CMT_CMMT = dgViewProcesos.Rows[idRgistro].Cells[1].Value.ToString();
            cmt_detGuardaProc.CMT_COMO = dgViewProcesos.Rows[idRgistro].Cells[2].Value.ToString();
            cmt_detGuardaProc.CMT_DONDE = dgViewProcesos.Rows[idRgistro].Cells[3].Value.ToString();
            cmt_detGuardaProc.CMT_MODELO = dgViewProcesos.Rows[idRgistro].Cells["CMT_MODELO"].Value.ToString();
            cmt_detGuardaProc.CMT_MAQUILERO = dgViewProcesos.Rows[idRgistro].Cells["CMT_MAQUILERO"].Value.ToString();
            cmt_detGuardaProc.CMT_CANTIDAD = int.Parse(dgViewProcesos.Rows[idRgistro].Cells["CMT_CANTIDAD"].Value.ToString());
            cmt_detGuardaProc.CMT_ORDENAMIENTO = idRgistro + 1;
            if (dgViewProcesos.Rows[idRgistro].Cells[4].Value.ToString() != "")
            {
                cmt_detGuardaProc.CMT_PRE_PROCESO = Convert.ToDecimal(dgViewProcesos.Rows[idRgistro].Cells[4].Value.ToString());
            }
            else { cmt_detGuardaProc.CMT_PRE_PROCESO = 0; }

            /*ADECUACION PARA ASIGNAR RUTA DE FORMA AUTOMATICA*/
            switch (cmt_detGuardaProc.CMT_PROCESO)
            {
                case "B":
                    cmt_detGuardaProc.CMT_DEPARTAMENTO = "B1";
                    break;
                case "C":
                    cmt_detGuardaProc.CMT_DEPARTAMENTO = "C1";
                    break;
                case "I":
                    cmt_detGuardaProc.CMT_DEPARTAMENTO = "B3";
                    break;
                case "E":
                    cmt_detGuardaProc.CMT_DEPARTAMENTO = "E1";
                    break;
            }

            int idx = 0;
            cmt_detGuardaProc.Crear(cmt_detGuardaProc, ref idx);
            if (!cmt_detGuardaProc.TieneError)
            {
                dgViewProcesos.Rows[idRgistro].Cells[6].Value = idx;
            }
            else
            {
                Cursor = Cursors.Default;
                MessageBox.Show(Properties.Resources.Cadena_ErrorAlGuardar + "\n\r\n\r" +
                                cmt_detGuardaProc.Error, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
        }
        private void EliminaProceso(int idRgistro)
        {
            procesoModificado = false;
            string agrupador = dgViewProcesos.Rows[idRgistro].Cells[5].Value.ToString();

            CMT_DET cmt_detGuardaProc = new CMT_DET();
            cmt_detGuardaProc.CMT_INDX = Convert.ToInt32(dgViewProcesos.Rows[idRgistro].Cells[6].Value.ToString());
            cmt_detGuardaProc.CMT_PROCESO = dgViewProcesos.Rows[idRgistro].Cells[0].Value.ToString();
            cmt_detGuardaProc.CMT_AGRUPADOR = agrupador;
            cmt_detGuardaProc.CMT_PEDIDO = idPedido;
            cmt_detGuardaProc.BorrarPorINDX(cmt_detGuardaProc);
            if (cmt_detGuardaProc.TieneError)
            {
                Cursor = Cursors.Default;
                MessageBox.Show(Properties.Resources.Cadena_ErrorAlGuardar + "\n\r\n\r" +
                                cmt_detGuardaProc.Error, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
        }
        private void btnGuardarProceso_Click(object sender, EventArgs e)
        {

            if (procesoNuevo)
            {
                int row = dgViewProcesos.Rows.Count - 1;
                if (dgViewProcesos.Rows[row].Cells[1].Value.ToString() != "" && dgViewProcesos.Rows[row].Cells[2].Value.ToString() != "" && dgViewProcesos.Rows[row].Cells[3].Value.ToString() != "" && dgViewProcesos.Rows[row].Cells[4].Value.ToString() != "")
                {
                    CreaProceso(dgViewProcesos.Rows.Count - 1);
                }
                else
                {
                    MessageBox.Show("Los datos del proceso agregado están incompletos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (procesoModificado)
                {
                    if (dgViewProcesos.CurrentRow != null)
                    {
                        ModificaProceso(dgViewProcesos.CurrentRow.Index);
                    }
                }
            }
            btnAgregarProceso.Visible = true;
            btnGuardarProceso.Visible = false;


        }


        private void dgViewProcesos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (procesoNuevo)
            {
                CreaProceso(dgViewProcesos.Rows.Count - 1);
            }
            else
            {
                if (procesoModificado && (e.RowIndex <= dgViewProcesos.Rows.Count - 1))
                {
                    ModificaProceso(e.RowIndex);
                }

            }
        }

        private void dgViewProcesos_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            procesoModificado = true;
        }

        private void dgViewProcesos_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dgViewProcesos_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void dgViewProcesos_KeyDown(object sender, KeyEventArgs e)
        {


        }

        private void dgViewProcesos_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 2 && this.dgViewProcesos.CurrentRow.Cells[0].Value.ToString() != "M")
                {
                    DataTable imagenes = new DataTable();
                    IMAGENES logo = new IMAGENES();
                    imagenes = logo.Consultar(e.FormattedValue.ToString());

                    if (imagenes.Rows.Count > 0)
                    {
                        string cod_cliente = imagenes.Rows[0]["COD_CLIENTE"].ToString();
                        if (logo.COD_CLIENTE != cliente)
                        {
                            MessageBox.Show("Atención:\n\nEste Logotipo existe pero no es exclusivo de este cliente.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        if (e.FormattedValue.ToString() != string.Empty)
                        {
                            MessageBox.Show("El código que escribió no es válido. Verifique.", "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            e.Cancel = true;
                        }
                    }
                }

                if (e.ColumnIndex == 3 && this.dgViewProcesos.CurrentRow.Cells[0].Value.ToString().Trim().ToUpper() == "Q")
                {
                    // VALIDAMOS QUE LA DIRECCION CONTENGA CP
                    // 1. VALIDAR LA PALABRA CP
                    // 2. VALIDAR 
                    if (e.FormattedValue.ToString().Trim() != "Dónde" && e.FormattedValue.ToString().Trim() != "")
                    {
                        if (!ContieneCP(e.FormattedValue.ToString()))
                        {
                            MessageBox.Show("Atención:\n\nLa dirección no contiene Código Postal.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            e.Cancel = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Atención:\n\nLa dirección no puede ir vacía.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        e.Cancel = true;
                    }
                }
            }
            catch { }
        }

        private void dgViewProcesos_CellValidated(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgViewProcesos_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {

        }

        private void dgViewProcesos_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            //EliminaProceso(e.Row.);
        }

        private void dgViewProcesos_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            EliminaProceso(e.Row.Index);
        }
        private bool MuestraFrmModificacion(int Renglon, int Col, string msg)
        {
            bool volverAMostrar = false;
            if (MessageBox.Show(dgViewPartidas.Rows[Renglon].Cells[Col].Value.ToString(), msg, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string modelo = dgViewPartidas.Rows[Renglon].Cells[1].Value.ToString();
                int pedido = Convert.ToInt32(dgViewPartidas.Rows[Renglon].Cells[0].Value.ToString());
                string AGRUPADOR = dgViewPartidas.Rows[Renglon].Cells["AGRUPADOR"].Value.ToString();
                decimal Precio = 0;
                decimal PRECIO_LISTA = 0;
                decimal descuento = 0;
                if (dgViewPartidas.Rows[Renglon].Cells["Precio"].Value != null)
                {
                    decimal.TryParse(dgViewPartidas.Rows[Renglon].Cells["Precio"].Value.ToString(), out Precio);
                }
                if (dgViewPartidas.Rows[Renglon].Cells["colPrecioLista"].Value != null)
                {
                    decimal.TryParse(dgViewPartidas.Rows[Renglon].Cells["colPrecioLista"].Value.ToString(), out PRECIO_LISTA);
                }
                if (dgViewPartidas.Rows[Renglon].Cells["DESCUENTO"].Value != null)
                {
                    decimal.TryParse(dgViewPartidas.Rows[Renglon].Cells["DESCUENTO"].Value.ToString(), out descuento);
                }
                if (dgViewPartidas.Columns[Col].Name == "Tallas")
                {
                    //frmModificarTallas frmModificarTallas = new frmModificarTallas(pedido, modelo, AGRUPADOR, Precio, PRECIO_LISTA);
                    frmModificarTallas2 frmModificarTallas = new frmModificarTallas2(pedido, modelo, AGRUPADOR, Precio, PRECIO_LISTA, descuento);
                    frmModificarTallas.ShowDialog();
                    volverAMostrar = true;
                }
                else if (dgViewPartidas.Columns[Col].Name == "PRECIO")
                {
                    frmModificarPrecio frmModificarPrecio = new frmModificarPrecio(pedido, AGRUPADOR, Precio);
                    frmModificarPrecio.ShowDialog();
                }
                else if (dgViewPartidas.Columns[Col].Name == "DESCUENTO")
                {
                    frmModificarDescuento frmModificarDescuento = new frmModificarDescuento(pedido, AGRUPADOR, descuento, Precio, PRECIO_LISTA);
                    frmModificarDescuento.ShowDialog();
                }
                GuardarPed_Temp();
                LlenaGridPartidas();
            }
            return volverAMostrar;
        }

        private void dgViewPartidas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgViewPartidas.Columns[e.ColumnIndex].Name == "Tallas")
            {
                if (MuestraFrmModificacion(e.RowIndex, e.ColumnIndex, "¿Modificar las tallas?"))
                {
                    if (tipoPedido == Enumerados.TipoPedido.Pedido || tipoPedido == Enumerados.TipoPedido.PedidoDAT || tipoPedido == Enumerados.TipoPedido.PedidoMOS || tipoPedido == Enumerados.TipoPedido.PedidoMOSCP)
                    {
                        MuestraFrmModificacion(e.RowIndex, 6, "¿Modificar precio?");
                    }

                }
            }
            if (dgViewPartidas.Columns[e.ColumnIndex].Name == "PRECIO")
            {
                if (tipoPedido == Enumerados.TipoPedido.Pedido || tipoPedido == Enumerados.TipoPedido.PedidoDAT || tipoPedido == Enumerados.TipoPedido.PedidoMOS || tipoPedido == Enumerados.TipoPedido.PedidoMOSCP)
                {
                    MuestraFrmModificacion(e.RowIndex, e.ColumnIndex, "¿Modificar precio?");
                }
                else
                {
                    MessageBox.Show("No hay ningún precio a modificar", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            if (dgViewPartidas.Columns[e.ColumnIndex].Name == "DESCUENTO")
            {
                if (tipoPedido == Enumerados.TipoPedido.PedidoDAT || tipoPedido == Enumerados.TipoPedido.PedidoMOS || tipoPedido == Enumerados.TipoPedido.PedidoMOSCP)
                {
                    MuestraFrmModificacion(e.RowIndex, e.ColumnIndex, "¿Modificar descuento?");
                }
                else
                {
                    MessageBox.Show("No hay ningún precio a modificar", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            GuardarPed_Temp();
            ActualizaAcumuladosPed_Mstr();
        }

        private void ActualizaAcumuladosPed_Mstr()
        {
            PED_MSTR ped_mstr = new PED_MSTR();
            ped_mstr.PEDIDO = idPedido;
            ped_mstr.ActualizaAcumulados(ped_mstr);
        }

        private void DeshabilitaColumnaProcesosEmpaque()
        {
            foreach (DataGridViewRow row in dgViewProcesos.Rows)
            {
                if (row.Cells[0].Value.ToString().Trim().ToUpper() == "M")
                {

                    row.Cells[1].ReadOnly = true;
                    row.Cells[1].Style.BackColor = Color.LightGray;
                    row.Cells[3].ReadOnly = true;
                    row.Cells[3].Style.BackColor = Color.LightGray;
                }
            }
        }
        private void DeshabilitaColumnaProcesosEmbarque()
        {
            foreach (DataGridViewRow row in dgViewProcesos.Rows)
            {
                if (row.Cells[0].Value.ToString().Trim().ToUpper() == "Q")
                {

                    row.Cells[1].ReadOnly = true;
                    row.Cells[1].Style.BackColor = Color.LightGray;
                    row.Cells[2].ReadOnly = true;
                    row.Cells[2].Style.BackColor = Color.LightGray;
                }
            }
        }

        private CMT_DET BuscaDireccionEmbarque(int pedido)
        {
            CMT_DET det = new CMT_DET();
            det = CMT_DET.BuscaProcesoEmbarque(pedido);
            return det;
        }
        private bool ContieneCP(string direccion)
        {
            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
            List<string> list = direccion.Split(delimiterChars).ToList();
            int parseCP = 0;
            if (list.Where(x => x.ToUpper().Trim().Replace(".", "").Replace(",", "") == "CP").Any()) { return true; }

            foreach (string value in list)
            {
                int.TryParse(value.Trim(), out parseCP);
                if (value.ToUpper().Trim().Replace(".", "").Replace(",", "") == "CP") { return true; }
                if (parseCP > 0)
                {
                    if (parseCP.ToString().Length == 5) { return true; }
                }
            }

            return false;
        }
    }
}
