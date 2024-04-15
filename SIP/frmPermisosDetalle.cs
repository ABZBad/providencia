using System;
using System.Windows.Forms;
using ulp_bl;
using ulp_bl.Permisos;

namespace SIP
{
    public partial class frmPermisosDetalle : Form
    {
        
        private int idPermiso = 0;
        public frmPermisosDetalle(int IDPermiso)
        {
            idPermiso = IDPermiso;
            InitializeComponent();
        }

        private void frmPermisosDetalle_Load(object sender, EventArgs e)
        {
            PermisosMenus permisoMenu = new PermisosMenus();
            
            permisoMenu = permisoMenu.Consultar(idPermiso);

            lblMenu.Tag = permisoMenu.Id;
            lblMenu.Text = string.Format("\"{0}\"", permisoMenu.Descripcion);
            chkPuedeEnetrar.Checked = permisoMenu.PuedeEntrar;
            chkPuedeModificar.Checked = permisoMenu.PuedeModificar;
            chkPuedeInsertar.Checked = permisoMenu.PuedeInsertar;
            chkPuedeBorrar.Checked = permisoMenu.PuedeBorrar;

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirma guardar los cambios", "Confirme", MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Cursor = Cursors.WaitCursor;
                    PermisosMenus permisosMenu = new PermisosMenus();

                permisosMenu.Id = Convert.ToInt32(lblMenu.Tag);
                permisosMenu.PuedeEntrar = chkPuedeEnetrar.Checked;
                permisosMenu.PuedeInsertar = chkPuedeInsertar.Checked;
                permisosMenu.PuedeModificar = chkPuedeModificar.Checked;
                permisosMenu.PuedeBorrar = chkPuedeBorrar.Checked;

                permisosMenu.Modificar(permisosMenu);
                this.Cursor = Cursors.Default;
                if (!permisosMenu.TieneError)
                {
                    MessageBox.Show("Información almacenada", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(
                        string.Format("Ocurrió un error al intentar almacenar la información:\n\r\n\r{0}",
                            permisosMenu.Error.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }

            }
        }
    }
}
