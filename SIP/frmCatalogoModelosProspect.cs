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
    public partial class frmCatalogoModelosProspect : Form
    {
        private Precarga precarga;
        DataTable dtModelos = new DataTable();
        DataTable dtModelosOrigin = new DataTable();
        String error = "";

        public frmCatalogoModelosProspect()
        {
            InitializeComponent();
            precarga = new Precarga(this);
            dgvModelos.AutoGenerateColumns = false;
        }
        #region Eventos
        private void frmCatalogoModelosProspect_Load(object sender, EventArgs e)
        {
            precarga.MostrarEspera();
            BackgroundWorker backGroundWorker = new BackgroundWorker();
            backGroundWorker.DoWork += backGroundWorker_DoWork;
            backGroundWorker.RunWorkerCompleted += backGroundWorker_RunWorkerCompleted;
            backGroundWorker.RunWorkerAsync();
        }
        private void txtBuscador_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtBuscador.Text.Trim() != "")
            {

                var result = from data in this.dtModelosOrigin.AsEnumerable() where data.Field<string>("Clave").Contains(txtBuscador.Text.ToUpper().Trim()) select data;
                if (result.Any())
                {
                    this.dtModelos = result.CopyToDataTable();
                }
                else
                {
                    this.dtModelos = new DataTable();
                }
            }
            else
            {
                this.dtModelos = this.dtModelosOrigin;
            }
            dgvModelos.DataSource = this.dtModelos;
            dgvModelos.Refresh();
        }
        #endregion
        #region Metodos

        #endregion
        #region Workers
        private void backGroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Exception ex = null;
            this.dtModelosOrigin = ProspectModule.GetCatalogoModelosDescripcion(ref ex);
            if (ex != null)
            {
            }
        }
        void backGroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            precarga.RemoverEspera();
            this.dtModelos = this.dtModelosOrigin;
            dgvModelos.DataSource = this.dtModelos;
        }
        #endregion

        private void dgvModelos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void dgvModelos_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void dgvModelos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvModelos.Columns[e.ColumnIndex].Name == "Eliminar" || dgvModelos.Columns[e.ColumnIndex].Name == "Guardar")
            {
                ProspectModule.Accion _accion = ProspectModule.Accion.Alta;
                Cursor.Current = Cursors.WaitCursor;
                Exception ex = null;
                DataGridViewRow dgvr = dgvModelos.Rows[e.RowIndex];
                if (dgvr != null)
                {
                    var _existe = this.dtModelosOrigin.Select(String.Format("Clave='{0}'", (string)dgvr.Cells["Clave"].Value));
                    if (dgvModelos.Columns[e.ColumnIndex].Name == "Eliminar")
                    {
                        if (MessageBox.Show("¿Seguro que desea eliminar el Modelo de forma permanente?", "SIP", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.Yes)
                        {
                            _accion = ProspectModule.Accion.Baja;
                        }
                        else
                        {
                            return;
                        }

                    }
                    else if (dgvModelos.Columns[e.ColumnIndex].Name == "Guardar")
                    {
                        _accion = _existe.Any() ? ProspectModule.Accion.Edicion : ProspectModule.Accion.Alta;
                    }
                    // 1. Validamos si la clave existe en inve
                    if (ProspectModule.ValidaClaveExistente((string)dgvr.Cells["Clave"].Value))
                    {
                        ProspectModule.SetCatalogoModelosDescripcion(
                        (string)dgvr.Cells["Clave"].Value.ToString().ToUpper(),
                        (string)dgvr.Cells["Descripcion"].Value,
                        (string)dgvr.Cells["Tallas"].Value,
                        dgvr.Cells["Activo"].Value == null ? false : (bool)dgvr.Cells["Activo"].Value,
                        _accion,
                        Globales.UsuarioActual.UsuarioUsuario,
                        ref ex);
                        MessageBox.Show("Proceso finalizado correctamente.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BackgroundWorker backGroundWorker = new BackgroundWorker();
                        backGroundWorker.DoWork += backGroundWorker_DoWork;
                        backGroundWorker.RunWorkerCompleted += backGroundWorker_RunWorkerCompleted;
                        backGroundWorker.RunWorkerAsync();
                    }
                    else
                    {
                        MessageBox.Show("La clave no existe dentro del Inventario, el proceso no puede ejecutarse.", "SIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dgvModelos.CancelEdit();
                    }
                }
                dgvModelos.DataSource = this.dtModelosOrigin;
                dgvModelos.Refresh();
                txtBuscador.Text = "";
                Cursor.Current = Cursors.Default;
            }
        }

        private void dgvModelos_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {

        }

        private void dgvModelos_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["Activo"].Value = true;
        }
    }
}
