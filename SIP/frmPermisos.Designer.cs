
namespace SIP
{
    partial class frmPermisos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Menu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PuedeEntrar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PuedeInsertar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PuedeModificar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PuedeBorrar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.TieneHijos = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PE = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PI = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PM = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PB = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSeleccionarTodo = new System.Windows.Forms.Button();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbNombres = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSalvarAcciones = new System.Windows.Forms.Button();
            this.chkLstAcciones = new SIP.UserControls.CheckedListBoxEx();
            this.scape1 = new SIP.UserControls.Scape();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAcceso = new SIP.UserControls.TextBoxEx();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Menu,
            this.PuedeEntrar,
            this.PuedeInsertar,
            this.PuedeModificar,
            this.PuedeBorrar,
            this.TieneHijos,
            this.PE,
            this.PI,
            this.PM,
            this.PB});
            this.dataGridView1.Location = new System.Drawing.Point(12, 109);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(717, 490);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            this.dataGridView1.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView1_CellPainting);
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
            this.dataGridView1.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dataGridView1_RowStateChanged);
            this.dataGridView1.Click += new System.EventHandler(this.dataGridView1_Click);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "ID";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // Menu
            // 
            this.Menu.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Menu.DataPropertyName = "Descripcion";
            this.Menu.HeaderText = "Menú";
            this.Menu.Name = "Menu";
            this.Menu.ReadOnly = true;
            this.Menu.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Menu.Width = 40;
            // 
            // PuedeEntrar
            // 
            this.PuedeEntrar.DataPropertyName = "PuedeEntrar";
            this.PuedeEntrar.HeaderText = "Puede Entrar";
            this.PuedeEntrar.Name = "PuedeEntrar";
            // 
            // PuedeInsertar
            // 
            this.PuedeInsertar.DataPropertyName = "PuedeInsertar";
            this.PuedeInsertar.HeaderText = "Puede Insertar";
            this.PuedeInsertar.Name = "PuedeInsertar";
            // 
            // PuedeModificar
            // 
            this.PuedeModificar.DataPropertyName = "PuedeModificar";
            this.PuedeModificar.HeaderText = "Puede Modificar";
            this.PuedeModificar.Name = "PuedeModificar";
            // 
            // PuedeBorrar
            // 
            this.PuedeBorrar.DataPropertyName = "PuedeBorrar";
            this.PuedeBorrar.HeaderText = "Puede Borrar";
            this.PuedeBorrar.Name = "PuedeBorrar";
            // 
            // TieneHijos
            // 
            this.TieneHijos.DataPropertyName = "TieneHijos";
            this.TieneHijos.HeaderText = "Tiene Hijos";
            this.TieneHijos.Name = "TieneHijos";
            this.TieneHijos.ReadOnly = true;
            this.TieneHijos.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TieneHijos.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.TieneHijos.Visible = false;
            // 
            // PE
            // 
            this.PE.DataPropertyName = "PE";
            this.PE.HeaderText = "PE";
            this.PE.Name = "PE";
            this.PE.ReadOnly = true;
            this.PE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.PE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.PE.Visible = false;
            // 
            // PI
            // 
            this.PI.DataPropertyName = "PI";
            this.PI.HeaderText = "PI";
            this.PI.Name = "PI";
            this.PI.ReadOnly = true;
            this.PI.Visible = false;
            // 
            // PM
            // 
            this.PM.DataPropertyName = "PM";
            this.PM.HeaderText = "PM";
            this.PM.Name = "PM";
            this.PM.ReadOnly = true;
            this.PM.Visible = false;
            // 
            // PB
            // 
            this.PB.DataPropertyName = "PB";
            this.PB.HeaderText = "PB";
            this.PB.Name = "PB";
            this.PB.ReadOnly = true;
            this.PB.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtAcceso);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnSeleccionarTodo);
            this.groupBox1.Controls.Add(this.txtUsuario);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmbNombres);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(918, 91);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // btnSeleccionarTodo
            // 
            this.btnSeleccionarTodo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSeleccionarTodo.Enabled = false;
            this.btnSeleccionarTodo.Location = new System.Drawing.Point(455, 62);
            this.btnSeleccionarTodo.Name = "btnSeleccionarTodo";
            this.btnSeleccionarTodo.Size = new System.Drawing.Size(128, 23);
            this.btnSeleccionarTodo.TabIndex = 5;
            this.btnSeleccionarTodo.Text = "Seleccionar todo";
            this.btnSeleccionarTodo.UseVisualStyleBackColor = true;
            this.btnSeleccionarTodo.Click += new System.EventHandler(this.btnSeleccionarTodo_Click);
            // 
            // txtUsuario
            // 
            this.txtUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsuario.Location = new System.Drawing.Point(360, 23);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.ReadOnly = true;
            this.txtUsuario.Size = new System.Drawing.Size(90, 20);
            this.txtUsuario.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(311, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Usuario";
            // 
            // cmbNombres
            // 
            this.cmbNombres.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbNombres.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cmbNombres.FormattingEnabled = true;
            this.cmbNombres.Location = new System.Drawing.Point(59, 19);
            this.cmbNombres.Name = "cmbNombres";
            this.cmbNombres.Size = new System.Drawing.Size(226, 21);
            this.cmbNombres.TabIndex = 2;
            this.cmbNombres.SelectedIndexChanged += new System.EventHandler(this.cmbNombres_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nombre:";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardar.Location = new System.Drawing.Point(774, 605);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 2;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(855, 605);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 3;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(732, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Permisos adicionales";
            // 
            // btnSalvarAcciones
            // 
            this.btnSalvarAcciones.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalvarAcciones.Image = global::SIP.Properties.Resources.Save;
            this.btnSalvarAcciones.Location = new System.Drawing.Point(904, 103);
            this.btnSalvarAcciones.Name = "btnSalvarAcciones";
            this.btnSalvarAcciones.Size = new System.Drawing.Size(26, 23);
            this.btnSalvarAcciones.TabIndex = 9;
            this.btnSalvarAcciones.UseVisualStyleBackColor = true;
            this.btnSalvarAcciones.Click += new System.EventHandler(this.btnSalvarAcciones_Click);
            // 
            // chkLstAcciones
            // 
            this.chkLstAcciones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkLstAcciones.FormattingEnabled = true;
            this.chkLstAcciones.Location = new System.Drawing.Point(735, 127);
            this.chkLstAcciones.Name = "chkLstAcciones";
            this.chkLstAcciones.Size = new System.Drawing.Size(195, 454);
            this.chkLstAcciones.TabIndex = 8;
            this.chkLstAcciones.TextForEmptyList = "(No se encontraron permisos)";
            // 
            // scape1
            // 
            this.scape1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.scape1.Form = this;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(466, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Acceso:";
            // 
            // txtAcceso
            // 
            this.txtAcceso.Location = new System.Drawing.Point(515, 23);
            this.txtAcceso.Name = "txtAcceso";
            this.txtAcceso.OnlyUpperCase = false;
            this.txtAcceso.Size = new System.Drawing.Size(29, 20);
            this.txtAcceso.TabIndex = 7;
            // 
            // frmPermisos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.scape1;
            this.ClientSize = new System.Drawing.Size(942, 633);
            this.Controls.Add(this.btnSalvarAcciones);
            this.Controls.Add(this.chkLstAcciones);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "frmPermisos";
            this.Text = "Permisos de usuario";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmPermisos_Load);
            this.Shown += new System.EventHandler(this.frmPermisos_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Menu;
        private System.Windows.Forms.DataGridViewCheckBoxColumn PuedeEntrar;
        private System.Windows.Forms.DataGridViewCheckBoxColumn PuedeInsertar;
        private System.Windows.Forms.DataGridViewCheckBoxColumn PuedeModificar;
        private System.Windows.Forms.DataGridViewCheckBoxColumn PuedeBorrar;
        private System.Windows.Forms.DataGridViewCheckBoxColumn TieneHijos;
        private System.Windows.Forms.DataGridViewCheckBoxColumn PE;
        private System.Windows.Forms.DataGridViewCheckBoxColumn PI;
        private System.Windows.Forms.DataGridViewCheckBoxColumn PM;
        private System.Windows.Forms.DataGridViewCheckBoxColumn PB;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbNombres;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label label3;
        private UserControls.CheckedListBoxEx chkLstAcciones;
        private System.Windows.Forms.Button btnSalvarAcciones;
        private UserControls.Scape scape1;
        private System.Windows.Forms.Button btnSeleccionarTodo;
        private System.Windows.Forms.Label label4;
        private UserControls.TextBoxEx txtAcceso;

    }
}