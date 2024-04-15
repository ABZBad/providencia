using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIP.Utiles;
using ulp_bl;
using System.IO;

namespace SIP
{
    public partial class frmTransferenciaCodigoBarras : Form
    {
        #region "ATRIBUTOS Y CONSTRUCTOR"
        private Precarga precarga;
        private BackgroundWorker bgw;
        private List<CodigoBarra> Recepciones;
        private int agrupador;
        public frmTransferenciaCodigoBarras()
        {
            this.Recepciones = new List<CodigoBarra> { };
            InitializeComponent();
            dgvRecepciones.AutoGenerateColumns = false;
            txtCodigo.Focus();
            precarga = new Precarga(this);
            agrupador = 0;
        }
        #endregion

        

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            List<String> codigoBarras = new List<string> { };
            if (e.KeyChar == (char)13)
            {
                //VERIFICAMOS QUE EL TEXTO INTRODUCIDO CONTENGA LA ESTRUCTURA DEL CODIGO DE BARRAS
                try
                {
                    codigoBarras = txtCodigo.Text.ToUpper().Replace("\r", "").Replace("\n", "").Split(' ').ToList();
                    if (codigoBarras.Count == 2)
                    {
                        //CON EL ID Y EL CONSECUTIVO VERIFICAMOS EL ESTATUS EN LA BD Y QUE NO HAYA SIDO ESCANEADO
                        if (this.Recepciones.Find(x => x.UUID == codigoBarras[0] && x.Consecutivo == int.Parse(codigoBarras[1])) != null)
                        {
                            MessageBox.Show("El Código ya fue escaneado.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtCodigo.Focus();
                            txtCodigo.Text = "";
                            return;
                        }
                        //BUSCAMOS EL CODIGO EN LA BD
                        DataTable dtCodigoBarras = RecOrdProduccionMaquilaCodigoBarras.ConsultaCodigoBarrasEscaneados(codigoBarras[0], int.Parse(codigoBarras[1]));
                        if (dtCodigoBarras.Rows.Count == 0)
                        {
                            MessageBox.Show("El Código no existe en la Base de Datos o no ha sido ingresado a ningún almacén.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtCodigo.Focus();
                            txtCodigo.Text = "";
                            return;
                        }
                        else
                        {
                            CodigoBarra objRecepcion = new CodigoBarra();
                            objRecepcion.UUID = dtCodigoBarras.Rows[0]["UUID"].ToString();
                            objRecepcion.Consecutivo = int.Parse(dtCodigoBarras.Rows[0]["Consecutivo"].ToString());
                            objRecepcion.Referencia = dtCodigoBarras.Rows[0]["Referencia"].ToString();
                            objRecepcion.OrdenProduccion = int.Parse(dtCodigoBarras.Rows[0]["OrdenProduccion"].ToString());
                            objRecepcion.OrdenMaquila = dtCodigoBarras.Rows[0]["OrdenMaquila"].ToString();
                            objRecepcion.Almacen = int.Parse(dtCodigoBarras.Rows[0]["Almacen"].ToString());

                            objRecepcion.Modelo = dtCodigoBarras.Rows[0]["Modelo"].ToString();
                            objRecepcion.Descripcion = dtCodigoBarras.Rows[0]["Descripcion"].ToString();

                            objRecepcion.Talla = dtCodigoBarras.Rows[0]["Talla"].ToString();
                            objRecepcion.Tipo = dtCodigoBarras.Rows[0]["TipoLinea"].ToString();

                            objRecepcion.Cantidad = int.Parse(dtCodigoBarras.Rows[0]["Recibidos"].ToString());
                            this.Recepciones.Add(objRecepcion);
                        }

                    }
                    else
                    {
                        MessageBox.Show("El Código introducido no tiene la estructura correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    dgvRecepciones.DataSource = null;
                    dgvRecepciones.DataSource = this.Recepciones;
                    dgvRecepciones.Refresh();

                    //lblTotalCodigosEscaneados.Text = this.Recepciones.Count.ToString();
                    //lblTotalLinea.Text = this.Recepciones.Where(x => x.Tipo == "L").Sum(x => x.Cantidad).ToString();
                    //lblTotalEspeciales.Text = this.Recepciones.Where(x => x.Tipo == "E").Sum(x => x.Cantidad).ToString();
                    //lblTotalDefectuosos.Text = this.Recepciones.Sum(x => x.Defectuosos).ToString();
                    lblTotal.Text = (this.Recepciones.Sum(x => x.Cantidad) - this.Recepciones.Sum(x => x.Defectuosos)).ToString();

                    txtCodigo.Focus();
                    txtCodigo.Text = "";
                    setResumenTreeView();

                }
                catch
                {
                    MessageBox.Show("El Código introducido no tiene la estructura correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            string resultado = String.Empty;
            string resultadoProceso = String.Empty;
            this.agrupador = TransferenciaPorModelo.incrementaAgrupador();
            ValidaPermisosTransferencia();


            //AGRUPAMOS LAS RECEPCIONES
            List<CodigoBarra> RecepcionesAgrupadas = new List<CodigoBarra> { };
            foreach (DataGridViewRow dr in dgvRecepciones.Rows)
            {
                if (RecepcionesAgrupadas.Where(x => x.Modelo == dr.Cells["Modelo"].Value.ToString() && x.Talla == dr.Cells["Talla"].Value.ToString()).FirstOrDefault() != null)
                {

                    RecepcionesAgrupadas.Where(x => x.Modelo == dr.Cells["Modelo"].Value.ToString() && x.Talla == dr.Cells["Talla"].Value.ToString()).FirstOrDefault().Cantidad += (int)dr.Cells["Cantidad"].Value;
                }
                else
                {
                    CodigoBarra _codigo = new CodigoBarra { Modelo = dr.Cells["Modelo"].Value.ToString(), Talla = dr.Cells["Talla"].Value.ToString(), Cantidad = (int)dr.Cells["Cantidad"].Value };
                    RecepcionesAgrupadas.Add(_codigo);
                }
            }

            try
            {
                foreach (CodigoBarra _codigo in RecepcionesAgrupadas)
                {
                    resultadoProceso = TransferenciaPorModelo.ProcesaTransferencia(_codigo.Modelo + _codigo.Talla, txtOrigen.Text, txtDestino.Text, _codigo.Cantidad, Globales.UsuarioActual.UsuarioUsuario, agrupador);
                    if (resultadoProceso != "") resultado += resultadoProceso + "|";
                    resultadoProceso = "";
                }

                if (resultado != "")
                {
                    MessageBox.Show("Existieron errores al realizar la transferencia: " + resultado, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                MessageBox.Show("Se ha completado la transferencia de forma correcta", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Recepciones.Clear();
                dgvRecepciones.DataSource = null;
                dgvRecepciones.DataSource = this.Recepciones;
                tvResumen.Nodes.Clear();
                txtCodigo.Focus();
                if (MessageBox.Show("Desea imprimir la transferencia?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DespliegaRpt();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar la transferencia: " + resultado + " Exception: " + ex.Message, "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void setResumenTreeView()
        {
            tvResumen.Nodes.Clear();
            foreach (CodigoBarra _codigo in this.Recepciones.OrderBy(x => x.Modelo).OrderBy(x => x.Talla))
            {

                TreeNode[] _tnFindModelo = tvResumen.Nodes.Cast<TreeNode>().Where(r => r.Text.Contains(_codigo.Modelo)).ToArray();

                if (_tnFindModelo.Length == 0)
                {
                    TreeNode _tn = new TreeNode("Modelo: " + _codigo.Modelo + " - Total: " + getTotalPorModelo(_codigo.Modelo));
                    tvResumen.Nodes.Add(_tn);
                    TreeNode _tn2 = new TreeNode("Talla: " + _codigo.Talla + " - Total: " + getTotalPorModeloTalla(_codigo.Modelo, _codigo.Talla));
                    _tn.Nodes.Add(_tn2);
                }
                else
                {
                    _tnFindModelo[0].Text = "Modelo: " + _codigo.Modelo + " - Total: " + getTotalPorModelo(_codigo.Modelo);
                    //BUSCAMOS LA TALLA
                    TreeNode[] _tnFindTalla = _tnFindModelo[0].Nodes.Cast<TreeNode>().Where(r => r.Text.Contains(_codigo.Talla)).ToArray();
                    if (_tnFindTalla.Length == 0)
                    {
                        TreeNode _tn2 = new TreeNode("Talla: " + _codigo.Talla + " - Total: " + getTotalPorModeloTalla(_codigo.Modelo, _codigo.Talla));
                        _tnFindModelo[0].Nodes.Add(_tn2);
                    }
                    else
                    {
                        //SUMAMOS LA CANTIDAD
                        _tnFindTalla[0].Text = "Talla: " + _codigo.Talla + " - Total: " + getTotalPorModeloTalla(_codigo.Modelo, _codigo.Talla);
                    }
                }


            }
            tvResumen.ExpandAll();
        }
        int getTotalPorModelo(String _Modelo)
        {
            return this.Recepciones.Where(x => x.Modelo == _Modelo).Sum(x => x.Cantidad);
        }
        int getTotalPorModeloTalla(String _Modelo, String _Talla)
        {
            return this.Recepciones.Where(x => x.Modelo == _Modelo && x.Talla == _Talla).Sum(x => x.Cantidad);
        }

        private void frmTransferenciaCodigoBarras_Load(object sender, EventArgs e)
        {
            ValidaPermisosTransferencia();
        }

        private void dgvRecepciones_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            this.Recepciones[e.RowIndex].Cantidad = (int)dgvRecepciones[2, e.RowIndex].Value;
            lblTotal.Text = (this.Recepciones.Sum(x => x.Cantidad)).ToString();
        }

        private void dgvRecepciones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                this.Recepciones.RemoveAt(e.RowIndex);

                lblTotal.Text = (this.Recepciones.Sum(x => x.Cantidad) - this.Recepciones.Sum(x => x.Defectuosos)).ToString();

                dgvRecepciones.DataSource = null;
                dgvRecepciones.DataSource = this.Recepciones;

                txtCodigo.Focus();
            }
        }

        private void ValidaPermisosTransferencia()
        {
            int noAlmacenOrigen = 0;
            int noAlmacenDestino = 0;

            int.TryParse(txtOrigen.Text, out noAlmacenOrigen);
            int.TryParse(txtDestino.Text, out noAlmacenDestino);

            String _criterio = "Transferir_" + noAlmacenOrigen.ToString() + "a" + noAlmacenDestino.ToString();

            //if (noAlmacenOrigen == 1 && noAlmacenDestino == 3)
            //{
            bool permisoTransferencia =
                //ulp_bl.Reportes.PermisosUsuarioEspeciales.TienePermisos(Globales.UsuarioActual.Id, 93, 113);
                ulp_bl.Reportes.PermisosUsuarioEspeciales.TienePermisos(Globales.UsuarioActual.Id, 93, "Transferencia", _criterio);
            btnProcesar.Enabled = permisoTransferencia;
            if (permisoTransferencia)
            {
                lblPermisoParaTransferir.Text = "";
            }
            else
            {
                lblPermisoParaTransferir.Text = "Solicite acceso para transferir del almacén " + noAlmacenOrigen.ToString() + " al " + noAlmacenDestino.ToString();
            }
            //}
            //else
            //{
            //    lblPermisoParaTransferir.Text = "";
            //    btnProcesar.Enabled = true;
            //}


        }

        private void txtOrigen_Leave(object sender, EventArgs e)
        {
            ValidaPermisosTransferencia();
        }

        private void txtDestino_Leave(object sender, EventArgs e)
        {
            ValidaPermisosTransferencia();
        }
        private void DespliegaRpt()
        {
            Reportes.frmReportes frmReportes = new Reportes.frmReportes(Enumerados.TipoReporteCrystal.TransferenciaXModelo, this.agrupador);
            //frmReportes.FormClosed += frmReportes_FormClosed;
            frmReportes.Show();
        }
        
    }
}
