using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIP.UserControls;
using ulp_bl;

namespace SIP
{
    public partial class frmStandPedi : Form
    {
        int ADVO_original = 0;

        public frmStandPedi()
        {
            InitializeComponent();
        }

        private void frmStandPedi_Load(object sender, EventArgs e)
        {
            EstandaresPedido es = StandPedi.RegresarEstandares();
            ADVO_original = es.ADVO;
             txtAdministrativo.Text = es.ADVO.ToString();
             txtSurtido.Text = es.SUR.ToString();
             txtEst.Text = es.EST.ToString();
             txtIni.Text = es.INI.ToString();
             txtEmpaque.Text = es.EMP.ToString();
             txtLiberacion.Text = es.LIB.ToString();
             txtCorte.Text = es.COR.ToString();
             txtBordado.Text = es.BOR.ToString();
             txtCostura.Text = es.COS.ToString();
             txtEmbarque.Text = es.EMB.ToString();
             lblUltimaModif.Text = "Última modificación por:" + es.USUARIO + " " + es.FCH_MODI.ToString();
    

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            EstandaresPedido estandaresPedido = new EstandaresPedido();
            
            estandaresPedido.ADVO = Convert.ToInt32(txtAdministrativo.Text);
            estandaresPedido.SUR = Convert.ToInt32(txtSurtido.Text);
            estandaresPedido.EST = Convert.ToInt32(txtEst.Text);
            estandaresPedido.INI = Convert.ToInt32(txtIni.Text);
            estandaresPedido.EMP = Convert.ToInt32(txtEmpaque.Text);
            estandaresPedido.LIB = Convert.ToInt32(txtLiberacion.Text);
            estandaresPedido.COR = Convert.ToInt32(txtCorte.Text);
            estandaresPedido.BOR = Convert.ToInt32(txtBordado.Text);
            estandaresPedido.COS = Convert.ToInt32(txtCostura.Text);
            estandaresPedido.EMB = Convert.ToInt32(txtEmbarque.Text);
            estandaresPedido.USUARIO = Globales.UsuarioActual.UsuarioUsuario;
            estandaresPedido.FCH_MODI = DateTime.Now;

            //estandaresPedido.FCH_MODI= Convert.ToDateTime(lblUltimaModif.Text);
            //string resultado = StandPedi.Crear(estandaresPedido);
            string resultado = StandPedi.ModificarEstandares(estandaresPedido, ADVO_original);
            if (resultado==string.Empty)
            {
                MessageBox.Show("Información actualizada","",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }
    }
}
