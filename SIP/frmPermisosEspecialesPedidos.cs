using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ulp_bl;
using SIP.Utiles;

namespace SIP
{
    public partial class frmPermisosEspecialesPedidos : Form
    {
        #region PROPEIDADES Y CONSTRUCTORES
        private BackgroundWorker bgw;
        private Precarga precarga;
        private Exception ex;
        DataTable dtUsuarios;
        List<String> ListaUsuarioActivos;
        string UsuariosActivos = "";

        public frmPermisosEspecialesPedidos()
        {
            InitializeComponent();
            this.dgvUsuarios.AutoGenerateColumns = false;
            precarga = new Precarga(this);
        }
        #endregion
        #region METODOS
        private void frmPermisosEspecialesPedidos_Load(object sender, EventArgs e)
        {
            bgw = new BackgroundWorker();
            bgw.DoWork += bgw_DoWorkLoad;
            bgw.RunWorkerCompleted += bgw_RunWorkerLoadCompleted;
            precarga.MostrarEspera();
            bgw.RunWorkerAsync();
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            this.ListaUsuarioActivos = new List<string> { };
            this.UsuariosActivos = "";
            foreach (DataGridViewRow dr in this.dgvUsuarios.Rows)
            {
                if ((Boolean)dr.Cells["Activo"].Value)
                {
                    this.ListaUsuarioActivos.Add("|" + dr.Cells["Usuario"].Value.ToString() + "|");
                }
            }
            this.UsuariosActivos = String.Join(",", this.ListaUsuarioActivos);
            bgw = new BackgroundWorker();
            bgw.DoWork += bgw_DoWorkSave;
            bgw.RunWorkerCompleted += bgw_RunWorkerSaveCompleted;
            precarga.MostrarEspera();
            bgw.RunWorkerAsync();
        }
        #endregion
        #region WORKERS
        void bgw_DoWorkLoad(object sender, DoWorkEventArgs e)
        {
            precarga.AsignastatusProceso("Cargando información...");
            this.dtUsuarios = new DataTable();
            this.dtUsuarios = ControlPedidos.getUsuariosEspeciales();
        }
        void bgw_DoWorkSave(object sender, DoWorkEventArgs e)
        {
            precarga.AsignastatusProceso("Cargando información...");
            ControlPedidos.setUsuariosEspeciales(this.UsuariosActivos, ref this.ex);

        }
        void bgw_RunWorkerLoadCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            precarga.RemoverEspera();
            this.dgvUsuarios.DataSource = this.dtUsuarios;
            this.dgvUsuarios.Refresh();
        }
        void bgw_RunWorkerSaveCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            precarga.RemoverEspera();
            MessageBox.Show("Permisos guardados de forma correcta.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        #endregion
    }


}
