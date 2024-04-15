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
    public partial class frmDiasFestivos : Form
    {
        public frmDiasFestivos()
        {
            InitializeComponent();
            LlenaDatos();
        }
        private void LlenaDatos()
        {
            DataTable datos = new DataTable();
            datos = DiasFestivos.ListaDias();
            dtViewMaestro.DataSource = datos;
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            DiasFestivos dia_agregar = new DiasFestivos();
            dia_agregar = dia_agregar.Consultar(DTPicker1.Value);
            if (dia_agregar.FECHA_FESTIVO.Year==0001)
            {
                dia_agregar.FECHA_FESTIVO = DTPicker1.Value.Date;
                dia_agregar.Crear(dia_agregar);
                LlenaDatos();
            }
            else
            {
                MessageBox.Show("El día ya ha sido agregado anteriormente.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
