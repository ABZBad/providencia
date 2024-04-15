using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using SIP.Utiles;
using ulp_bl;
using ulp_bl.Permisos;

namespace SIP
{
    public partial class frmPermisos : Form
    {
        private int idUsuarioParametro = 0;
        private int idUsuarioSeleccionado = 0;
        private bool cargandoPantalla = false;
        private bool seleccionarTodo = false;
        private int moduloIDPrevio = 0;
        private int moduloID;
        public frmPermisos()
        {
            cargandoPantalla = true;
            InitializeComponent();            
        }
        public frmPermisos(int IdUsuario)
        {
            InitializeComponent();
            idUsuarioParametro = IdUsuario;
        }
        private void frmPermisos_Load(object sender, EventArgs e)
        {
            typeof(DataGridView).InvokeMember("DoubleBuffered",BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,null,dataGridView1,new object[] { true });
            LlenaGridPermisos();
            LlenaComboNombres();            
            if (idUsuarioParametro > 0)
            {
                cmbNombres.SelectedValue = idUsuarioParametro;
            }
        }

        private void LlenaDatosGenerales()
        {
            throw new NotImplementedException();
        }

        private void LlenaComboNombres()
        {
            AutoCompleteStringCollection autoCompleteStringCollection = new AutoCompleteStringCollection();
            List<Usuario> usuarios = Usuario.ObtenerTodos();
            foreach (Usuario usuario in usuarios)
            {
                autoCompleteStringCollection.Add(usuario.UsuarioNombre);
            }

            cmbNombres.AutoCompleteCustomSource = autoCompleteStringCollection;
            cmbNombres.DataSource = usuarios;
            cmbNombres.ValueMember = "Id";
            cmbNombres.DisplayMember = "UsuarioNombre";
        }

        private void LlenaGridPermisos()
        {
            PermisosPantalla _pp = new PermisosPantalla();
            dataGridView1.DataSource = _pp.PermisosScr();            
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            e.CellStyle.BackColor = Color.White;
            if (e.ColumnIndex == 1)
            {
                e.CellStyle.BackColor = Color.White;
            }
            #region "DA FORMATO CHECKS"
            if (e.RowIndex > -1 && e.ColumnIndex > 1)
            {
                if (dataGridView1.DataSource != null)
                {
                    DataGridViewCheckBoxCell chkCell =
                        (DataGridViewCheckBoxCell)dataGridView1.Rows[e.RowIndex].Cells["TieneHijos"];
                    if (chkCell.Value != DBNull.Value)
                    {



                        DataGridViewCheckBoxCell chkCellInsertar =
                            (DataGridViewCheckBoxCell)dataGridView1.Rows[e.RowIndex].Cells["PuedeInsertar"];
                        DataGridViewCheckBoxCell chkCellEntrar =
                            (DataGridViewCheckBoxCell)dataGridView1.Rows[e.RowIndex].Cells["PuedeEntrar"];
                        DataGridViewCheckBoxCell chkCellModificar =
                            (DataGridViewCheckBoxCell)dataGridView1.Rows[e.RowIndex].Cells["PuedeModificar"];
                        DataGridViewCheckBoxCell chkCellBorrar =
                            (DataGridViewCheckBoxCell)dataGridView1.Rows[e.RowIndex].Cells["PuedeBorrar"];

                        if (Convert.ToBoolean(chkCell.Value))
                        {

                            //e.CellStyle.Font = new Font(FontFamily.GenericSansSerif,8f, FontStyle.Bold);


                            e.CellStyle.BackColor = Color.LightGray;
                            //e.CellStyle.ForeColor = Color.LightGray;

                            chkCellInsertar.Style.Padding = new Padding(10, 10, 0, 0);
                            //chkCellEntrar.Style.Padding = new Padding(10, 10, 0, 0);
                            chkCellBorrar.Style.Padding = new Padding(10, 10, 0, 0);
                            chkCellModificar.Style.Padding = new Padding(10, 10, 0, 0);

                            chkCellInsertar.ReadOnly = true;
                            //chkCellEntrar.ReadOnly = true;
                            chkCellBorrar.ReadOnly = true;
                            chkCellModificar.ReadOnly = true;


                        }
                        else
                        {
                            DataGridViewCheckBoxCell chkCellPB =
                        (DataGridViewCheckBoxCell)dataGridView1.Rows[e.RowIndex].Cells["PB"];
                            DataGridViewCheckBoxCell chkCellPM =
                        (DataGridViewCheckBoxCell)dataGridView1.Rows[e.RowIndex].Cells["PM"];
                            DataGridViewCheckBoxCell chkCellPE =
                        (DataGridViewCheckBoxCell)dataGridView1.Rows[e.RowIndex].Cells["PE"];
                            DataGridViewCheckBoxCell chkCellPI =
                        (DataGridViewCheckBoxCell)dataGridView1.Rows[e.RowIndex].Cells["PI"];

                            if ((bool)chkCellPE.Value == false)
                            {
                                chkCellEntrar.Style.Padding = new Padding(10, 10, 0, 0);
                            }
                            if ((bool)chkCellPI.Value == false)
                            {
                                chkCellInsertar.Style.Padding = new Padding(10, 10, 0, 0);
                            }
                            if ((bool)chkCellPM.Value == false)
                            {
                                chkCellModificar.Style.Padding = new Padding(10, 10, 0, 0);
                            }
                            if ((bool)chkCellPB.Value == false)
                            {
                                chkCellBorrar.Style.Padding = new Padding(10, 10, 0, 0);
                            }
                        }
                    }

                }

            }
            #endregion
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
                e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;                
            }
            
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex <= -1)
                return;

            DataGridViewCell dgVcTieneHijos = dataGridView1.Rows[e.RowIndex].Cells["TieneHijos"];
            //Se le se hace doble clic en en un renglón que tiene sub menús, se cancela la ejecución del código
            if ((bool) dgVcTieneHijos.Value)
                return;

            //Se extrae el ID del menú seleccionado
            DataGridViewCell dgVc = dataGridView1.Rows[e.RowIndex].Cells["Id"];

            //Se llama la forma pasando como parámetro el ID del menú
            frmPermisosDetalle frmPermisosDetalle = new SIP.frmPermisosDetalle(Convert.ToInt32(dgVc.Value));


            if (frmPermisosDetalle.ShowDialog() == DialogResult.OK)
            {
                //Si hubo cambios...
                //Refrescar pantalla, y mantiene la vista del Grid tal y como estaba antes del Refresh
                int primerRenglonALaVista = dataGridView1.FirstDisplayedScrollingRowIndex;
                int renglonSeleccionado = e.RowIndex;
                LlenaGridPermisos();
                dataGridView1.FirstDisplayedScrollingRowIndex = primerRenglonALaVista;
                dataGridView1.Rows[renglonSeleccionado].Selected = true;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbNombres_SelectedIndexChanged(object sender, EventArgs e)
        {
            //si aún no se ha asignado el value members se sale
            if (cmbNombres.ValueMember == string.Empty)
                return;

            Cursor = Cursors.WaitCursor;
            int primerRenglonALaVista = dataGridView1.FirstDisplayedScrollingRowIndex;
            int renglonSeleccionado = dataGridView1.Rows[0].Index;
            LlenaGridPermisos();
            dataGridView1.FirstDisplayedScrollingRowIndex = primerRenglonALaVista;
            dataGridView1.Rows[renglonSeleccionado].Selected = true;
            btnSeleccionarTodo.Enabled = true;

            //Se consulta al usuario seleccionado
            Usuario usuario = new Usuario();
            
            usuario = usuario.Consultar(Convert.ToInt32(cmbNombres.SelectedValue));
            txtUsuario.Text = usuario.UsuarioUsuario;
            if (!string.IsNullOrEmpty(usuario.UsuarioSae.ACCESO))
            {
                txtAcceso.Text = usuario.UsuarioSae.ACCESO.Trim();
            }
            else
            {
                txtAcceso.Text = "?";
            }
            idUsuarioSeleccionado = usuario.Id;
            foreach (int permiso in usuario.PermisosPuedeEntrar)
            {
                DataTable dataTable = (DataTable) dataGridView1.DataSource;
                DataRow[] dataRow = dataTable.Select(string.Format("Id={0}", permiso));
                dataRow[0]["PuedeEntrar"] = true;
            }
            Cursor = Cursors.Default; 
        }

        private bool UsuarioSeleccionado()
        {
            if (idUsuarioSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un usuario de la lista desplegable", "Verifique", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                return true;
            }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {


            if (UsuarioSeleccionado())
            {
                if (
                    MessageBox.Show(string.Format("Se van a guardar cambios para el usuario: \"{0}\" "+ Environment.NewLine + Environment.NewLine,txtUsuario.Text) + "¿ Confirma continuar ?", "Confirme", MessageBoxButtons.YesNo,MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        List<int> permisosPuedeEntrar = new List<int>();
                        DataTable dataTablePermisos = (DataTable) dataGridView1.DataSource;
                        foreach (DataRow row in dataTablePermisos.Rows)
                        {
                            if (row["PuedeEntrar"] != DBNull.Value)
                            {
                                if ((bool) row["PuedeEntrar"])
                                {
                                    permisosPuedeEntrar.Add(Convert.ToInt32(row["Id"]));
                                }
                            }
                        }

                        Usuario.SalvarPermisos(Enumerados.TipoPermiso.PuedeEntrar, permisosPuedeEntrar,
                            idUsuarioSeleccionado);
                        MessageBox.Show(Properties.Resources.Cadena_DatosGuardados, "", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        this.Cursor = Cursors.Default;
                    }
                    catch (Exception Ex)
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show(
                            Properties.Resources.Cadena_ErrorAlGuardar + Environment.NewLine + Environment.NewLine +
                            Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            //UsuarioSeleccionado();
        }
        
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {

            if (!cargandoPantalla)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DataGridViewCell celda = dataGridView1.SelectedRows[0].Cells["ID"];

                    moduloID = (int) celda.Value;
                    if (moduloIDPrevio == 0 || (moduloID != moduloIDPrevio))
                    {
                        this.Cursor = Cursors.WaitCursor;
                        PermisosMenus permisosMenus = new PermisosMenus();
                        DataTable dataTableAtributos = permisosMenus.DevuelveAtributosPorModulo(moduloID);
                        moduloIDPrevio = moduloID;
                        chkLstAcciones.DataSource = dataTableAtributos;
                        chkLstAcciones.DisplayMember = "AtributoNombre";
                        chkLstAcciones.ValueMember = "Id";


                        if (idUsuarioSeleccionado > 0)
                        {
                            DataTable dataTablePermisosAcciones = Usuario.DevuelveAccionesPorModuloID(
                                idUsuarioSeleccionado, moduloID);
                            for (int i = 0; i < chkLstAcciones.Items.Count; i++)
                            {
                                DataRowView dataRowViewRenglonAaccion = (DataRowView)chkLstAcciones.Items[i];

                                DataRow[] renglonesAcciones = dataTablePermisosAcciones.Select(string.Format("ID={0}", dataRowViewRenglonAaccion["Id"]));



                                if (renglonesAcciones.Length == 1)
                                {

                                    chkLstAcciones.SetItemChecked(i, true);
                                }
                            }
                        }

                        this.Cursor = Cursors.Default;

                    }                    

                }
            }
        }

        private void frmPermisos_Shown(object sender, EventArgs e)
        {
            //cargandoPantalla = false;
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            cargandoPantalla = false;
        }

        private void btnSalvarAcciones_Click(object sender, EventArgs e)
        {
            if (UsuarioSeleccionado())
            {
                if (
                    MessageBox.Show(string.Format("Se van a guardar cambios para el usuario: \"{0}\" " + Environment.NewLine + Environment.NewLine, txtUsuario.Text) + "¿ Confirma continuar ?", "Confirme", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        List<int> permisosPuedeEntrar = new List<int>();
                        //DataTable dataTablePermisos = (DataTable)chkLstAcciones.DataSource;
                        List<int> permisosAcciones = new List<int>();
                        foreach (object row in chkLstAcciones.CheckedItems)
                        {
                            DataRow row2 = ((DataRowView)row).Row;
                            permisosAcciones.Add(Convert.ToInt16(row2["Id"]));
                        }
                        Usuario.SalvarPermisos(moduloID, permisosAcciones, idUsuarioSeleccionado);
                        MessageBox.Show(Properties.Resources.Cadena_DatosGuardados, "", MessageBoxButtons.OK,MessageBoxIcon.Information);
                        
                        this.Cursor = Cursors.Default;
                    }
                    catch (Exception Ex)
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show(
                            Properties.Resources.Cadena_ErrorAlGuardar + Environment.NewLine + Environment.NewLine + Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;

        }

        private void btnSeleccionarTodo_Click(object sender, EventArgs e)
        {
            DataTable dtPermisos = (DataTable)dataGridView1.DataSource;

            foreach (DataRow dataRow in dtPermisos.Rows)
            {
                dataRow["PuedeEntrar"] = !seleccionarTodo;
            }

            seleccionarTodo = !seleccionarTodo;
            
            if (seleccionarTodo == true)
            {
                btnSeleccionarTodo.Text = "Des Seleccionar todo";
            }
            else
            {                
                    btnSeleccionarTodo.Text = "Seleccionar todo";                
            }

        }
    }
}
