namespace SIP
{
    partial class frmPermisosEspecialesPedidos
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
            this.lblTitulo = new System.Windows.Forms.Label();
            this.dgvUsuarios = new System.Windows.Forms.DataGridView();
            this.Activo = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.idUsuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Area = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreUsuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnGuardar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(162, 9);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(270, 26);
            this.lblTitulo.TabIndex = 3;
            this.lblTitulo.Text = "Autorización de Pedidos";
            // 
            // dgvUsuarios
            // 
            this.dgvUsuarios.AllowUserToAddRows = false;
            this.dgvUsuarios.AllowUserToDeleteRows = false;
            this.dgvUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsuarios.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Activo,
            this.idUsuario,
            this.usuario,
            this.Area,
            this.NombreUsuario});
            this.dgvUsuarios.Location = new System.Drawing.Point(17, 52);
            this.dgvUsuarios.Name = "dgvUsuarios";
            this.dgvUsuarios.Size = new System.Drawing.Size(531, 170);
            this.dgvUsuarios.TabIndex = 4;
            // 
            // Activo
            // 
            this.Activo.DataPropertyName = "activo";
            this.Activo.HeaderText = "Activo";
            this.Activo.Name = "Activo";
            this.Activo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Activo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Activo.Width = 50;
            // 
            // idUsuario
            // 
            this.idUsuario.DataPropertyName = "id";
            this.idUsuario.HeaderText = "Id";
            this.idUsuario.Name = "idUsuario";
            this.idUsuario.ReadOnly = true;
            this.idUsuario.Width = 50;
            // 
            // usuario
            // 
            this.usuario.DataPropertyName = "usuario";
            this.usuario.HeaderText = "Usuario";
            this.usuario.Name = "usuario";
            this.usuario.ReadOnly = true;
            // 
            // Area
            // 
            this.Area.DataPropertyName = "area";
            this.Area.HeaderText = "Area";
            this.Area.Name = "Area";
            this.Area.ReadOnly = true;
            this.Area.Visible = false;
            // 
            // NombreUsuario
            // 
            this.NombreUsuario.DataPropertyName = "nombre";
            this.NombreUsuario.HeaderText = "Nombre de usuario";
            this.NombreUsuario.Name = "NombreUsuario";
            this.NombreUsuario.ReadOnly = true;
            this.NombreUsuario.Width = 250;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(473, 228);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 5;
            this.btnGuardar.Text = "Guardar cambios";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // frmPermisosEspecialesPedidos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 261);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.dgvUsuarios);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmPermisosEspecialesPedidos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Permisos especiales Control de Pedidos";
            this.Load += new System.EventHandler(this.frmPermisosEspecialesPedidos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.DataGridView dgvUsuarios;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Activo;
        private System.Windows.Forms.DataGridViewTextBoxColumn idUsuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn usuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn Area;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreUsuario;
    }
}