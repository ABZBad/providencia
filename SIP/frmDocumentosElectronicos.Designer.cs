namespace SIP
{
    partial class frmDocumentosElectronicos
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblNombreArchivo = new System.Windows.Forms.Label();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.lblObservaciones = new System.Windows.Forms.Label();
            this.txtTipo = new System.Windows.Forms.TextBox();
            this.lblTipoDocumento = new System.Windows.Forms.Label();
            this.lblPath = new System.Windows.Forms.Label();
            this.btnSeleccionar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgDocumentos = new System.Windows.Forms.DataGridView();
            this.idDocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pedido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Observaciones = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Usuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ver = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Eliminar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDocumentos)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblNombreArchivo);
            this.groupBox1.Controls.Add(this.txtObservaciones);
            this.groupBox1.Controls.Add(this.lblObservaciones);
            this.groupBox1.Controls.Add(this.txtTipo);
            this.groupBox1.Controls.Add(this.lblTipoDocumento);
            this.groupBox1.Controls.Add(this.lblPath);
            this.groupBox1.Controls.Add(this.btnSeleccionar);
            this.groupBox1.Controls.Add(this.btnGuardar);
            this.groupBox1.Location = new System.Drawing.Point(12, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(198, 330);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Nuevo documento";
            // 
            // lblNombreArchivo
            // 
            this.lblNombreArchivo.AutoSize = true;
            this.lblNombreArchivo.Location = new System.Drawing.Point(6, 77);
            this.lblNombreArchivo.Name = "lblNombreArchivo";
            this.lblNombreArchivo.Size = new System.Drawing.Size(0, 17);
            this.lblNombreArchivo.TabIndex = 8;
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Location = new System.Drawing.Point(6, 184);
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Size = new System.Drawing.Size(168, 99);
            this.txtObservaciones.TabIndex = 7;
            // 
            // lblObservaciones
            // 
            this.lblObservaciones.AutoSize = true;
            this.lblObservaciones.Location = new System.Drawing.Point(6, 164);
            this.lblObservaciones.Name = "lblObservaciones";
            this.lblObservaciones.Size = new System.Drawing.Size(103, 17);
            this.lblObservaciones.TabIndex = 6;
            this.lblObservaciones.Text = "Observaciones";
            // 
            // txtTipo
            // 
            this.txtTipo.Location = new System.Drawing.Point(6, 130);
            this.txtTipo.Name = "txtTipo";
            this.txtTipo.Size = new System.Drawing.Size(168, 22);
            this.txtTipo.TabIndex = 5;
            // 
            // lblTipoDocumento
            // 
            this.lblTipoDocumento.AutoSize = true;
            this.lblTipoDocumento.Location = new System.Drawing.Point(6, 110);
            this.lblTipoDocumento.Name = "lblTipoDocumento";
            this.lblTipoDocumento.Size = new System.Drawing.Size(36, 17);
            this.lblTipoDocumento.TabIndex = 4;
            this.lblTipoDocumento.Text = "Tipo";
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Location = new System.Drawing.Point(6, 47);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(177, 17);
            this.lblPath.TabIndex = 3;
            this.lblPath.Text = "Sin archivo seleccionado...";
            // 
            // btnSeleccionar
            // 
            this.btnSeleccionar.Location = new System.Drawing.Point(6, 21);
            this.btnSeleccionar.Name = "btnSeleccionar";
            this.btnSeleccionar.Size = new System.Drawing.Size(168, 23);
            this.btnSeleccionar.TabIndex = 2;
            this.btnSeleccionar.Text = "Seleccionar archivo";
            this.btnSeleccionar.UseVisualStyleBackColor = true;
            this.btnSeleccionar.Click += new System.EventHandler(this.btnSeleccionar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(6, 289);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(168, 23);
            this.btnGuardar.TabIndex = 0;
            this.btnGuardar.Text = "Guardar documento";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgDocumentos);
            this.groupBox2.Location = new System.Drawing.Point(216, 49);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1164, 330);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Listado de documentos";
            // 
            // dgDocumentos
            // 
            this.dgDocumentos.AllowUserToAddRows = false;
            this.dgDocumentos.AllowUserToDeleteRows = false;
            this.dgDocumentos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDocumentos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDocumento,
            this.Pedido,
            this.Tipo,
            this.Nombre,
            this.Observaciones,
            this.Usuario,
            this.Fecha,
            this.Ver,
            this.Eliminar});
            this.dgDocumentos.Location = new System.Drawing.Point(6, 21);
            this.dgDocumentos.Name = "dgDocumentos";
            this.dgDocumentos.RowTemplate.Height = 24;
            this.dgDocumentos.Size = new System.Drawing.Size(1152, 303);
            this.dgDocumentos.TabIndex = 0;
            this.dgDocumentos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDocumentos_CellContentClick);
            // 
            // idDocumento
            // 
            this.idDocumento.DataPropertyName = "idDocumento";
            this.idDocumento.HeaderText = "idDocumento";
            this.idDocumento.Name = "idDocumento";
            this.idDocumento.Visible = false;
            // 
            // Pedido
            // 
            this.Pedido.DataPropertyName = "Pedido";
            this.Pedido.HeaderText = "Pedido";
            this.Pedido.Name = "Pedido";
            this.Pedido.Visible = false;
            // 
            // Tipo
            // 
            this.Tipo.DataPropertyName = "Tipo";
            this.Tipo.HeaderText = "Tipo de Documento";
            this.Tipo.Name = "Tipo";
            this.Tipo.ReadOnly = true;
            this.Tipo.Width = 80;
            // 
            // Nombre
            // 
            this.Nombre.DataPropertyName = "NombreDocumento";
            this.Nombre.HeaderText = "Nombre";
            this.Nombre.Name = "Nombre";
            this.Nombre.ReadOnly = true;
            this.Nombre.Width = 150;
            // 
            // Observaciones
            // 
            this.Observaciones.DataPropertyName = "Observaciones";
            this.Observaciones.HeaderText = "Observaciones";
            this.Observaciones.Name = "Observaciones";
            this.Observaciones.ReadOnly = true;
            this.Observaciones.Width = 200;
            // 
            // Usuario
            // 
            this.Usuario.DataPropertyName = "Usuario";
            this.Usuario.HeaderText = "Usuario";
            this.Usuario.Name = "Usuario";
            this.Usuario.ReadOnly = true;
            this.Usuario.Width = 60;
            // 
            // Fecha
            // 
            this.Fecha.DataPropertyName = "FechaAlta";
            this.Fecha.HeaderText = "Fecha";
            this.Fecha.Name = "Fecha";
            this.Fecha.ReadOnly = true;
            this.Fecha.Width = 120;
            // 
            // Ver
            // 
            this.Ver.HeaderText = "Ver";
            this.Ver.Name = "Ver";
            this.Ver.Text = "Ver documento";
            this.Ver.UseColumnTextForButtonValue = true;
            // 
            // Eliminar
            // 
            this.Eliminar.HeaderText = "Eliminar";
            this.Eliminar.Name = "Eliminar";
            this.Eliminar.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Eliminar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Eliminar.Text = "Eliminar";
            this.Eliminar.UseColumnTextForButtonValue = true;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(16, 9);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(362, 25);
            this.lblTitulo.TabIndex = 2;
            this.lblTitulo.Text = "Documentos electrónicos del Pedido";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "pdf";
            this.openFileDialog1.Filter = "Documentos PDF | *.pdf";
            // 
            // frmDocumentosElectronicos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1392, 391);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmDocumentosElectronicos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Documentación Electrónica del Pedido";
            this.Load += new System.EventHandler(this.frmDocumentosElectronicos_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDocumentos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.DataGridView dgDocumentos;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.Button btnSeleccionar;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox txtTipo;
        private System.Windows.Forms.Label lblTipoDocumento;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.Label lblObservaciones;
        private System.Windows.Forms.Label lblNombreArchivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDocumento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pedido;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn Observaciones;
        private System.Windows.Forms.DataGridViewTextBoxColumn Usuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewButtonColumn Ver;
        private System.Windows.Forms.DataGridViewButtonColumn Eliminar;
    }
}