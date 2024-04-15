using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ulp_bl;

namespace SIP
{
    public partial class frmOrdMaquila2 : Form
    {
        int Almacen = 0;
        private bool OrdenGuardada;
        private string numReferOP = "";
        ErrorProvider errorPClave = new ErrorProvider();
        ErrorProvider errorPCosto = new ErrorProvider();
        ErrorProvider errorPEsq = new ErrorProvider();
        
        private bool DatosValidos()
        {
            int errores = 0;
            if (string.IsNullOrEmpty(txtClaveProveedor.Text))
            {
                errorPClave.BlinkStyle = ErrorBlinkStyle.NeverBlink;
                errorPClave.SetIconAlignment(txtClaveProveedor, ErrorIconAlignment.BottomLeft);
                errorPClave.SetError(txtClaveProveedor, "Campo requerido");
                errores++;
            }
            else
            {
                errorPClave.Clear();
            }
            if (string.IsNullOrEmpty(txtCosto.Text))
            {
                errorPCosto.BlinkStyle = ErrorBlinkStyle.NeverBlink;
                errorPCosto.SetIconAlignment(txtCosto, ErrorIconAlignment.BottomLeft);
                errorPCosto.SetError(txtCosto, "Campo requerido");
                errores++;
            }
            else
            {
                errorPCosto.Clear();
            }
            if (string.IsNullOrEmpty(txtEsqDeImp.Text) || txtEsqDeImp.Text == "0")
            {
                errorPEsq.BlinkStyle = ErrorBlinkStyle.NeverBlink;
                errorPEsq.SetIconAlignment(txtEsqDeImp, ErrorIconAlignment.BottomLeft);
                errorPEsq.SetError(txtEsqDeImp, "Campo requerido");
                errores++;
            }
            else
            {
                errorPEsq.Clear();
            }
            if (errores > 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public int CVE_DOC { get; set; }
        public int CVE_DOC_SUGGESTED { set; get; }

        public frmOrdMaquila2(string NumReferOP, int Almacen)
        {
            InitializeComponent();
            numReferOP = NumReferOP;
            this.Almacen = Almacen;
        }
        
        private void txtClaveProveedor_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtClaveProveedor.Text))
            {
                if (txtClaveProveedor.Text != "0")
                {
                    PROV01 prov01 = new PROV01();
                    prov01 = prov01.Consultar(txtClaveProveedor.Text, true);

                    if (!string.IsNullOrEmpty(prov01.NOMBRE))
                    {
                        lblNombreProveedor.Text = prov01.NOMBRE;
                        errorPClave.Clear();
                    }
                    else
                    {
                        MessageBox.Show("El Proveedor no existe.","Verifique",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                        txtClaveProveedor.Focus();
                    }
                }
                else
                {
                    lblNombreProveedor.Text = "";
                    DatosValidos();
                }
            }
            else
            {
                lblNombreProveedor.Text = "";
                DatosValidos();

            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            PROV01 prov01 = new PROV01();
            Cursor = Cursors.WaitCursor;
            DataTable dataTableProv01 = prov01.ConsultarTodoParaBusqueda();
            Cursor = Cursors.Default;
            frmBusquedaGenerica frmBusqueda = new frmBusquedaGenerica(dataTableProv01, "NOMBRE", "LOCALIZACION DE PROVEEDORES");
            frmBusqueda.ShowDialog();
            if (frmBusqueda.RenglonSeleccionado != null)
            {
                txtClaveProveedor.Text = frmBusqueda.RenglonSeleccionado["Clave"].ToString().Trim();
                txtClaveProveedor_Leave(sender, new EventArgs());
                txtCosto.Focus();
            }
        }

        private void frmOrdMaquila2_Load(object sender, EventArgs e)
        {
            lblReferencia.Text = numReferOP;
            DataTable dataTableOrden = OrdMaquila2.RegresaOrden(numReferOP);
            dgvDetalle.DataSource = dataTableOrden;
            lblTotalPrendas.Text = Convert.ToString(dataTableOrden.Compute("SUM([Cantidad])", null));
            CVE_DOC_SUGGESTED = OrdMaquila2.SiguienteOrdMaquila();
            lblNoOrdenMaq.Text = CVE_DOC_SUGGESTED.ToString();

        }

        private void btnGenerarOrden_Click(object sender, EventArgs e)
        {
            if (DatosValidos())
            {
                Exception ex = null;
                DataTable dataTableDetalle = (DataTable)dgvDetalle.DataSource;
                this.CVE_DOC = OrdMaquila2.GenerarOrden(
                                                    dataTableDetalle,
                                                    dataTableDetalle.Rows.Count,
                                                    Convert.ToInt32(lblTotalPrendas.Text),
                                                    txtClaveProveedor.Text,
                                                    Convert.ToInt32(txtCosto.Text),
                                                    Convert.ToInt32(txtEsqDeImp.Text),
                                                    txtObservaciones.Text,
                                                    this.Almacen,
                                                    ref ex
                                                    );
                if (ex == null)
                {
                    //MessageBox.Show("Orden de maquila guardada", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    OrdenGuardada = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Captura los datos del Panel de Información", "Verifique", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
        }

        private void frmOrdMaquila2_FormClosing(object sender, FormClosingEventArgs e)
        {
            
                if (!OrdenGuardada)
                {
                    MessageBox.Show("No puede cerrar esta ventana hasta completar el proceso", "", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    e.Cancel = true;
                }
            
        }

        private void txtCosto_Leave(object sender, EventArgs e)
        {
            DatosValidos();
        }

        private void txtEsqDeImp_Leave(object sender, EventArgs e)
        {
            DatosValidos();
        }

        private void dgView_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.Width = 58;
        }
    }
}
