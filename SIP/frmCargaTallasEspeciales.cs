using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ulp_bl;
using SIP.Utiles;

namespace SIP
{
    public partial class frmCargaTallasEspeciales : Form
    {
        public Boolean codigoExistente = false;
        public List<String> Tallas { get; set; }
        public List<String> Codigos { get; set; }
        public List<String> TallasAdicionales { get; set; }
        private List<int> NumeroSolicitudes;
        int referencia;
        DataTable Solicitudes = new DataTable("SOLICITUDES");
        private ModeloYTallas modeloYTallas;
        public String TallasString
        {
            get
            {
                String _tallasString = "";
                _tallasString = String.Join(",", this.Tallas);
                _tallasString += this.TallasAdicionales.Count == 0 ? "" : "," + String.Join(",", this.TallasAdicionales);
                return _tallasString;
            }
        }

        public frmCargaTallasEspeciales(int _referencia)
        {
            InitializeComponent();
            this.Tallas = new List<string> { };
            this.Codigos = new List<string> { };
            this.TallasAdicionales = new List<string> { };
            this.referencia = _referencia;
            this.NumeroSolicitudes = new List<int> { };
            this.NumeroSolicitudes.Add(this.referencia);
            this.dgvTallasExistentes.AutoGenerateColumns = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!this.codigoExistente)
            {
                foreach (DataGridViewRow drv in dgvTallas.Rows)
                {
                    if (drv.Cells["Talla"].Value != null && drv.Cells["Talla"].Value.ToString() != "")
                    {
                        Tallas.Add(drv.Cells["Talla"].Value.ToString());
                    }
                }
                this.Close();
            }
            else
            {
                // RECORREMOS LAS TALLAS SELECCIONADAS
                foreach (DataGridViewRow drv in dgvTallasExistentes.Rows)
                {
                    if (drv.Cells["Seleccionar"].Value != null)
                    {
                        if ((Boolean)drv.Cells["Seleccionar"].Value)
                        {
                            Tallas.Add(drv.Cells["TallaExistente"].Value.ToString());
                            Codigos.Add(Solicitudes.Rows[0]["CODIGO_COTIZACION"].ToString().Substring(0, 8) + drv.Cells["TallaExistente"].Value.ToString());
                        }
                    }
                }
                // RECORREMOS TALLAS NUEVAS
                foreach (DataGridViewRow drv in dgvTallas.Rows)
                {
                    if (drv.Cells["Talla"].Value != null && drv.Cells["Talla"].Value.ToString() != "")
                    {
                        // VERIFICAMOS QUE NO SE PUEDA DUPLICAR EL CODIGO
                        var existe = this.Codigos.Where(x => x.Contains(drv.Cells["Talla"].Value.ToString()));
                        if (!existe.Any())
                        {
                            TallasAdicionales.Add(drv.Cells["Talla"].Value.ToString());
                            Codigos.Add(Solicitudes.Rows[0]["CODIGO_COTIZACION"].ToString().Substring(0, 8) + drv.Cells["Talla"].Value.ToString());
                        }
                    }
                }
                this.Close();
            }
        }

        private void dgvTallas_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == 0) // 1 should be your column index
            {
                int i;

                if (!int.TryParse(Convert.ToString(e.FormattedValue), out i))
                {
                    if (e.FormattedValue.ToString().Trim() == "" || e.FormattedValue.ToString().Trim().Length == 4)
                    {
                        e.Cancel = false;
                    }
                    else
                    {
                        e.Cancel = true;
                        MessageBox.Show("Favor de introducir la talla en el siguiente formato XXYY.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (i.ToString().Length != 4)
                    {
                        e.Cancel = true;
                        MessageBox.Show("Favor de introducir la talla en el siguiente formato XXYY.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void frmCargaTallasEspeciales_Load(object sender, EventArgs e)
        {
            // OBTENEMOS LA INFORMACIÓN DE LA SOLICITUD
            SOLICITUDES_ESPECIALES solicitudesBl = new SOLICITUDES_ESPECIALES();
            Solicitudes = solicitudesBl.ConsultarSolicitudes(this.NumeroSolicitudes);
            if (Solicitudes.Rows.Count > 0)
            {
                if (!Solicitudes.Rows[0]["CODIGO_COTIZACION"].ToString().Contains("0000"))
                {
                    //OBTENEMOS TODAS LAS TALLAS EXISTENTES
                    this.codigoExistente = true;
                    modeloYTallas = DupCodProdEstr.RegresaDatosModelo(Solicitudes.Rows[0]["CODIGO_COTIZACION"].ToString().Substring(0, 8));
                    this.dgvTallasExistentes.DataSource = modeloYTallas.CodigosExistentes;
                }
            }
            if (this.codigoExistente)
            {
                this.dgvTallas.Visible = true;
                this.dgvTallasExistentes.Visible = true;
                this.Size = new Size(940, 380);
                this.StartPosition = FormStartPosition.CenterScreen;
                this.lblTitulo.Text = "Selección y/o captura de tallas";
            }
            else
            {
                this.dgvTallas.Visible = true;
                this.dgvTallasExistentes.Visible = false;
                this.dgvTallas.Location = new Point(12, 40);
                this.Size = new Size(510, 380);
                this.StartPosition = FormStartPosition.CenterScreen;
                this.lblTitulo.Text = "Captura de tallas";
            }


        }
    }
}
