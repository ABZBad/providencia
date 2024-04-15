namespace SIP
{
    partial class frmCatalogoModelosProspect
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
            this.dgvModelos = new System.Windows.Forms.DataGridView();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblBuscador = new System.Windows.Forms.Label();
            this.txtBuscador = new System.Windows.Forms.TextBox();
            this.Clave = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tallas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Activo = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Guardar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Eliminar = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvModelos)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvModelos
            // 
            this.dgvModelos.AllowUserToDeleteRows = false;
            this.dgvModelos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvModelos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Clave,
            this.Descripcion,
            this.Tallas,
            this.Activo,
            this.Guardar,
            this.Eliminar});
            this.dgvModelos.Location = new System.Drawing.Point(12, 150);
            this.dgvModelos.Name = "dgvModelos";
            this.dgvModelos.RowTemplate.Height = 24;
            this.dgvModelos.Size = new System.Drawing.Size(1471, 572);
            this.dgvModelos.TabIndex = 0;
            this.dgvModelos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvModelos_CellClick);
            this.dgvModelos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvModelos_CellContentClick);
            this.dgvModelos.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvModelos_CellEndEdit);
            this.dgvModelos.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvModelos_DefaultValuesNeeded);
            this.dgvModelos.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvModelos_RowsAdded);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(22, 41);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(585, 29);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "Catálogo de modelos para cotizaciones Prospect";
            // 
            // lblBuscador
            // 
            this.lblBuscador.AutoSize = true;
            this.lblBuscador.Location = new System.Drawing.Point(12, 114);
            this.lblBuscador.Name = "lblBuscador";
            this.lblBuscador.Size = new System.Drawing.Size(80, 17);
            this.lblBuscador.TabIndex = 2;
            this.lblBuscador.Text = "Buscador...";
            // 
            // txtBuscador
            // 
            this.txtBuscador.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBuscador.Location = new System.Drawing.Point(98, 111);
            this.txtBuscador.Name = "txtBuscador";
            this.txtBuscador.Size = new System.Drawing.Size(509, 22);
            this.txtBuscador.TabIndex = 3;
            this.txtBuscador.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBuscador_KeyUp);
            // 
            // Clave
            // 
            this.Clave.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Clave.DataPropertyName = "Clave";
            this.Clave.HeaderText = "Clave";
            this.Clave.Name = "Clave";
            this.Clave.Width = 68;
            // 
            // Descripcion
            // 
            this.Descripcion.DataPropertyName = "Descripcion";
            this.Descripcion.HeaderText = "Descripción";
            this.Descripcion.Name = "Descripcion";
            this.Descripcion.Width = 400;
            // 
            // Tallas
            // 
            this.Tallas.DataPropertyName = "Tallas";
            this.Tallas.HeaderText = "Tallas";
            this.Tallas.Name = "Tallas";
            this.Tallas.Width = 250;
            // 
            // Activo
            // 
            this.Activo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Activo.DataPropertyName = "Activo";
            this.Activo.FalseValue = "0";
            this.Activo.HeaderText = "Activo";
            this.Activo.IndeterminateValue = "0";
            this.Activo.Name = "Activo";
            this.Activo.TrueValue = "1";
            this.Activo.Width = 52;
            // 
            // Guardar
            // 
            this.Guardar.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Guardar.HeaderText = "Guardar";
            this.Guardar.Name = "Guardar";
            this.Guardar.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Guardar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Guardar.Text = "Guardar";
            this.Guardar.ToolTipText = "Guardar";
            this.Guardar.UseColumnTextForButtonValue = true;
            this.Guardar.Width = 86;
            // 
            // Eliminar
            // 
            this.Eliminar.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Eliminar.HeaderText = "Eliminar";
            this.Eliminar.Name = "Eliminar";
            this.Eliminar.Text = "Eliminar";
            this.Eliminar.ToolTipText = "Eliminar";
            this.Eliminar.UseColumnTextForButtonValue = true;
            this.Eliminar.Width = 64;
            // 
            // frmCatalogoModelosProspect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1495, 734);
            this.Controls.Add(this.txtBuscador);
            this.Controls.Add(this.lblBuscador);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.dgvModelos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCatalogoModelosProspect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Catálogo de Modelos Prospect";
            this.Load += new System.EventHandler(this.frmCatalogoModelosProspect_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvModelos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvModelos;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblBuscador;
        private System.Windows.Forms.TextBox txtBuscador;
        private System.Windows.Forms.DataGridViewTextBoxColumn Clave;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tallas;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Activo;
        private System.Windows.Forms.DataGridViewButtonColumn Guardar;
        private System.Windows.Forms.DataGridViewButtonColumn Eliminar;
    }
}