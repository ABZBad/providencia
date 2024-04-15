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
    public partial class frmRecOrdProduccionMaquilaCodigoBarras : Form
    {
        #region ATRIBUTOS Y CONSTRUCTORES

        private Precarga precarga;
        private BackgroundWorker bgw;
        private List<CodigoBarra> Recepciones;
        private List<CodigoBarra> RecepcionesAgrupadas;
        List<string> Log = new List<string> { };
        private Boolean imprimirExcel;


        public frmRecOrdProduccionMaquilaCodigoBarras()
        {
            this.Recepciones = new List<CodigoBarra> { };
            this.RecepcionesAgrupadas = new List<CodigoBarra> { };
            InitializeComponent();
            dgvRecepciones.AutoGenerateColumns = false;
            txtCodigo.Focus();
            precarga = new Precarga(this);
            imprimirExcel = false;
        }
        #endregion
        #region EVENTOS
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
                        DataTable dtCodigoBarras = RecOrdProduccionMaquilaCodigoBarras.ConsultaCodigoBarras(codigoBarras[0], int.Parse(codigoBarras[1]));
                        if (dtCodigoBarras.Rows.Count == 0)
                        {
                            MessageBox.Show("El Código no existe en la Base de Datos.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtCodigo.Focus();
                            txtCodigo.Text = "";
                            return;
                        }
                        else
                        {
                            if (dtCodigoBarras.Rows[0]["Estatus"].ToString() == "E")
                            {
                                MessageBox.Show("El Código introducido ya fue escaneado y procesado.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                                objRecepcion.Cantidad = int.Parse(dtCodigoBarras.Rows[0]["Cantidad"].ToString());
                                objRecepcion.Recibidos = int.Parse(dtCodigoBarras.Rows[0]["Cantidad"].ToString());
                                this.Recepciones.Add(objRecepcion);
                            }
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

                    lblTotalCodigosEscaneados.Text = this.Recepciones.Count.ToString();
                    lblTotalLinea.Text = this.Recepciones.Where(x => x.Tipo == "L").Sum(x => x.Cantidad).ToString();
                    lblTotalEspeciales.Text = this.Recepciones.Where(x => x.Tipo == "E").Sum(x => x.Cantidad).ToString();
                    lblTotalDefectuosos.Text = this.Recepciones.Sum(x => x.Defectuosos).ToString();
                    lblTotal.Text = (this.Recepciones.Sum(x => x.Recibidos) - this.Recepciones.Sum(x => x.Defectuosos)).ToString();

                    txtCodigo.Focus();
                    txtCodigo.Text = "";
                    setResumenTreeView();
                    imprimirExcel = false;

                }
                catch
                {
                    MessageBox.Show("El Código introducido no tiene la estructura correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void dgvRecepciones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 8)
            {
                this.Recepciones.RemoveAt(e.RowIndex);
                lblTotalCodigosEscaneados.Text = this.Recepciones.Count.ToString();
                lblTotalLinea.Text = this.Recepciones.Where(x => x.Tipo == "L").Sum(x => x.Cantidad).ToString();
                lblTotalEspeciales.Text = this.Recepciones.Where(x => x.Tipo == "E").Sum(x => x.Cantidad).ToString();
                lblTotalDefectuosos.Text = this.Recepciones.Sum(x => x.Defectuosos).ToString();
                lblTotal.Text = (this.Recepciones.Sum(x => x.Recibidos) - this.Recepciones.Sum(x => x.Defectuosos)).ToString();
                setResumenTreeView();
                dgvRecepciones.DataSource = null;
                dgvRecepciones.DataSource = this.Recepciones;

                txtCodigo.Focus();
            }
        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            if (this.Recepciones.Count == 0)
            {
                MessageBox.Show("No existen recepciones para procesar.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            bgw = new BackgroundWorker();
            bgw.DoWork += bgw_DoWork;
            bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;

            precarga.MostrarEspera();

            bgw.RunWorkerAsync();

        }

        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnProcesar_Click(null, null);
            }
            if (e.KeyCode == Keys.F3)
            {
                btnNuevaRecepcion_Click(null, null);
            }
            if (e.KeyCode == Keys.F4)
            {
                btnExportarExcel_Click(null, null);
            }

        }
        private void dgvRecepciones_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7)
                this.Recepciones[e.RowIndex].Defectuosos = (int)dgvRecepciones[7, e.RowIndex].Value;
            if (e.ColumnIndex == 6)
                this.Recepciones[e.RowIndex].Recibidos = (int)dgvRecepciones[6, e.RowIndex].Value;
            lblTotalCodigosEscaneados.Text = this.Recepciones.Count.ToString();
            lblTotalLinea.Text = this.Recepciones.Where(x => x.Tipo == "L").Sum(x => x.Cantidad).ToString();
            lblTotalEspeciales.Text = this.Recepciones.Where(x => x.Tipo == "E").Sum(x => x.Cantidad).ToString();
            lblTotalDefectuosos.Text = this.Recepciones.Sum(x => x.Defectuosos).ToString();
            lblTotal.Text = (this.Recepciones.Sum(x => x.Recibidos) - this.Recepciones.Sum(x => x.Defectuosos)).ToString();
        }

        private void dgvRecepciones_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                dgvRecepciones.Rows[e.RowIndex].ErrorText = "";
                int newInteger;
                if (!int.TryParse(e.FormattedValue.ToString(), out newInteger) || newInteger < 0)
                {
                    e.Cancel = true;
                    dgvRecepciones.Rows[e.RowIndex].ErrorText = "Se debe de ingresar un valor numérico.";
                }
                else
                {
                    if (newInteger > (int)dgvRecepciones[6, e.RowIndex].Value)
                    {
                        e.Cancel = true;
                        dgvRecepciones.Rows[e.RowIndex].ErrorText = "La cantidad de artículos defectuosos no pude exceder la cantidad total de la recepción.";
                    }
                }
            }


        }

        private void frmRecOrdProduccionMaquilaCodigoBarras_Click(object sender, EventArgs e)
        {
            txtCodigo.Focus();
        }

        private void dgvRecepciones_Click(object sender, EventArgs e)
        {
            txtCodigo.Focus();
        }
        private void btnNuevaRecepcion_Click(object sender, EventArgs e)
        {
            this.Recepciones.Clear();
            this.RecepcionesAgrupadas.Clear();
            dgvRecepciones.DataSource = null;
            dgvRecepciones.Refresh();
            tvResumen.Nodes.Clear();
            txtCodigo.Text = "";
            txtCodigo.Focus();
            imprimirExcel = false;
            btnExportarExcel.Enabled = imprimirExcel;
        }
        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            string ruta = Path.GetTempFileName().Replace(".tmp", ".xls");
            RecOrdProduccionMaquilaCodigoBarras.GeneraArchivoExcel(this.Recepciones, ruta);
            Utiles.FuncionalidadesFormularios.MostrarExcel(ruta);
        }
        #endregion
        #region WORKERS
        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            //List<String> RecepcionesOP = new List<String>{};


            //GENERAMOS UN OBJETO PARA CADA OP
            var RecepcionesOP = this.Recepciones.Select(x => x.Referencia).Distinct();


            //AGRUPAMOS POR OP/MODELO/TALLA
            this.RecepcionesAgrupadas = new List<CodigoBarra> { };

            foreach (CodigoBarra _objRecepcion in this.Recepciones)
            {
                //GUARDAMOS LA RECEPCION Y AGRUPAMOS
                // RecOrdProduccionMaquilaCodigoBarras.GuardaRecepcionCodigoBarra(_objRecepcion.UUID, _objRecepcion.Consecutivo, _objRecepcion.Referencia, _objRecepcion.OrdenMaquila, _objRecepcion.Defectuosos, Globales.UsuarioActual.UsuarioUsuario);
                if (RecepcionesAgrupadas.Where(x => x.Modelo == _objRecepcion.Modelo && x.Talla == _objRecepcion.Talla && x.Referencia == _objRecepcion.Referencia).FirstOrDefault() != null)
                {

                    RecepcionesAgrupadas.Where(x => x.Modelo == _objRecepcion.Modelo && x.Talla == _objRecepcion.Talla && x.Referencia == _objRecepcion.Referencia).FirstOrDefault().Cantidad += _objRecepcion.Cantidad;
                    RecepcionesAgrupadas.Where(x => x.Modelo == _objRecepcion.Modelo && x.Talla == _objRecepcion.Talla && x.Referencia == _objRecepcion.Referencia).FirstOrDefault().Recibidos += _objRecepcion.Recibidos;
                    RecepcionesAgrupadas.Where(x => x.Modelo == _objRecepcion.Modelo && x.Talla == _objRecepcion.Talla && x.Referencia == _objRecepcion.Referencia).FirstOrDefault().Defectuosos += _objRecepcion.Defectuosos;
                }
                else
                {
                    CodigoBarra _codigo = new CodigoBarra { Referencia = _objRecepcion.Referencia, OrdenMaquila = _objRecepcion.OrdenMaquila, OrdenProduccion = _objRecepcion.OrdenProduccion, Cantidad = _objRecepcion.Cantidad, Recibidos = _objRecepcion.Recibidos, Defectuosos = _objRecepcion.Defectuosos, Modelo = _objRecepcion.Modelo, Talla = _objRecepcion.Talla, Descripcion = _objRecepcion.Descripcion, Consecutivo = _objRecepcion.Consecutivo, Almacen = _objRecepcion.Almacen, UUID = _objRecepcion.UUID };
                    RecepcionesAgrupadas.Add(_codigo);
                }

            }

            this.Log = new List<string> { };
            foreach (CodigoBarra _objRecepcion in RecepcionesAgrupadas)
            {
                String _proceso = "Procesando OP: " + _objRecepcion.Referencia + " OM: " + _objRecepcion.OrdenMaquila + " Modelo: " + _objRecepcion.Modelo + " Talla: " + _objRecepcion.Talla + " Cantidad: " + _objRecepcion.Cantidad;
                String _Resultado = RecOrdProduccionMaquilaCodigoBarras.GuardaRecepcionCodigoBarrasSAE(_objRecepcion.Referencia, _objRecepcion.OrdenMaquila.ToString(), _objRecepcion.OrdenProduccion, 0, _objRecepcion.Recibidos, _objRecepcion.Defectuosos, txtEsquemaImp.Text, _objRecepcion.Modelo, _objRecepcion.Talla, _objRecepcion.Consecutivo, txtCodDefectuosos.Text, txtPrefijo.Text, Globales.UsuarioActual.UsuarioUsuario);
                if (_Resultado == "")
                    _objRecepcion.Procesado = true;
                _proceso += " | ESTATUS: " + (_Resultado == "" ? "OK" : _Resultado);
                Log.Add(_proceso);
            }

            foreach (CodigoBarra _objRecepcion in this.Recepciones)
            {
                if (RecepcionesAgrupadas.Where(r => r.OrdenProduccion == _objRecepcion.OrdenProduccion && r.OrdenMaquila == _objRecepcion.OrdenMaquila && r.Referencia == _objRecepcion.Referencia && r.Procesado).FirstOrDefault() != null)
                {
                    //GUARDAMOS LA RECEPCION Y AGRUPAMOS
                    RecOrdProduccionMaquilaCodigoBarras.GuardaRecepcionCodigoBarra(_objRecepcion.UUID, _objRecepcion.Consecutivo, _objRecepcion.Referencia, _objRecepcion.OrdenMaquila, _objRecepcion.Defectuosos, Globales.UsuarioActual.UsuarioUsuario);
                }
            }

            imprimirExcel = true;
            //btnExportarExcel.Enabled = imprimirExcel;
        }
        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SaveFileDialog of = new SaveFileDialog();
            of.Filter = "Archivos de texto (txt) | *.txt";
            of.FileName = "Report de recepcion_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".txt";
            if (of.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = of.FileName;
                if (!File.Exists(path))
                {
                    // Create a file to write to.
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        foreach (String _log in this.Log)
                        {
                            sw.WriteLine(_log);
                        }

                    }
                }
            }
            precarga.RemoverEspera();
            if (
                    MessageBox.Show("Desea imprimir el recibo de la recepción ? ",
                        "Impresion de recibo de orden de maquila", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                    DialogResult.Yes)
            {
                string ruta = Path.GetTempFileName().Replace(".tmp", ".xls");
                RecOrdProduccionMaquilaCodigoBarras.GeneraArchivoExcel(this.RecepcionesAgrupadas, ruta);
                Utiles.FuncionalidadesFormularios.MostrarExcel(ruta);
            }
            this.Recepciones.Clear();
            this.RecepcionesAgrupadas.Clear();
            dgvRecepciones.DataSource = null;
            dgvRecepciones.Refresh();
            tvResumen.Nodes.Clear();
            txtCodigo.Text = "";
            txtCodigo.Focus();
        }


        #endregion
        #region METODOS
        void setResumenTreeView()
        {
            tvResumen.Nodes.Clear();

            foreach (CodigoBarra _codigo in this.Recepciones.OrderBy(x => x.Referencia).OrderBy(x => x.Modelo))
            {

                TreeNode[] _tnFindOP = tvResumen.Nodes.Cast<TreeNode>().Where(r => r.Text.Contains(_codigo.Referencia)).ToArray();

                if (_tnFindOP.Length == 0)
                {
                    TreeNode _tn = new TreeNode("OP: " + _codigo.Referencia + " - Total: " + getTotalPorOP(_codigo.Referencia));
                    tvResumen.Nodes.Add(_tn);
                    TreeNode _tn2 = new TreeNode("Modelo: " + _codigo.Modelo + " - Total: " + getTotalPorOPModelo(_codigo.Referencia, _codigo.Modelo));
                    _tn.Nodes.Add(_tn2);
                    TreeNode _tn3 = new TreeNode("Talla: " + _codigo.Talla + " - Total: " + getTotalPorOPModeloTalla(_codigo.Referencia, _codigo.Modelo, _codigo.Talla));
                    _tn2.Nodes.Add(_tn3);
                }
                else
                {

                    _tnFindOP[0].Text = "OP: " + _codigo.Referencia + " - Total: " + getTotalPorOP(_codigo.Referencia);
                    //BUSCAMOS EL MODELO
                    TreeNode[] _tnFindModelo = _tnFindOP[0].Nodes.Cast<TreeNode>().Where(r => r.Text.Contains(_codigo.Modelo)).ToArray();
                    if (_tnFindModelo.Length == 0)
                    {
                        TreeNode _tn2 = new TreeNode("Modelo: " + _codigo.Modelo + " - Total: " + getTotalPorOPModelo(_codigo.Referencia, _codigo.Modelo));
                        _tnFindModelo[0].Nodes.Add(_tn2);
                        TreeNode _tn3 = new TreeNode("Talla: " + _codigo.Talla + " - Total: " + getTotalPorOPModeloTalla(_codigo.Referencia, _codigo.Modelo, _codigo.Talla));
                        _tn2.Nodes.Add(_tn3);
                    }
                    else
                    {
                        _tnFindModelo[0].Text = "Modelo: " + _codigo.Modelo + " - Total: " + getTotalPorOPModelo(_codigo.Referencia, _codigo.Modelo);
                        //BUSCAMOS LA TALLA
                        TreeNode[] _tnFindTalla = _tnFindModelo[0].Nodes.Cast<TreeNode>().Where(r => r.Text.Contains(_codigo.Talla)).ToArray();
                        if (_tnFindTalla.Length == 0)
                        {
                            TreeNode _tn3 = new TreeNode("Talla: " + _codigo.Talla + " - Total: " + getTotalPorOPModeloTalla(_codigo.Referencia, _codigo.Modelo, _codigo.Talla));
                            _tnFindModelo[0].Nodes.Add(_tn3);
                        }
                        else
                        {
                            //SUMAMOS LA CANTIDAD
                            _tnFindTalla[0].Text = "Talla: " + _codigo.Talla + " - Total: " + getTotalPorOPModeloTalla(_codigo.Referencia, _codigo.Modelo, _codigo.Talla);
                        }
                    }

                }


            }

            /*

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

            */
            tvResumen.ExpandAll();
        }


        int getTotalPorOP(String _Referencia)
        {
            return this.Recepciones.Where(x => x.Referencia == _Referencia).Sum(x => x.Cantidad);
        }
        int getTotalPorOPModelo(String _Referencia, String _Modelo)
        {
            return this.Recepciones.Where(x => x.Modelo == _Modelo && x.Referencia == _Referencia).Sum(x => x.Cantidad);
        }
        int getTotalPorOPModeloTalla(String _Referencia, String _Modelo, String _Talla)
        {
            return this.Recepciones.Where(x => x.Modelo == _Modelo && x.Talla == _Talla && x.Referencia == _Referencia).Sum(x => x.Cantidad);
        }


        #endregion

        private void frmRecOrdProduccionMaquilaCodigoBarras_Load(object sender, EventArgs e)
        {
            btnExportarExcel.Enabled = imprimirExcel;
        }

        private void frmRecOrdProduccionMaquilaCodigoBarras_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!imprimirExcel)
            {
                if (MessageBox.Show("No se ha procesado la recepción, ¿Seguro que deseas salir de esta ventana?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.No)
                    e.Cancel = true;
            }
        }
    }
}