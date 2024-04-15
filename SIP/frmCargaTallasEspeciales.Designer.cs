namespace SIP
{
    partial class frmCargaTallasEspeciales
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
            this.dgvTallas = new System.Windows.Forms.DataGridView();
            this.Talla = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.dgvTallasExistentes = new System.Windows.Forms.DataGridView();
            this.Seleccionar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.TallaExistente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTitulo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTallas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTallasExistentes)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTallas
            // 
            this.dgvTallas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTallas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Talla});
            this.dgvTallas.Location = new System.Drawing.Point(618, 52);
            this.dgvTallas.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvTallas.Name = "dgvTallas";
            this.dgvTallas.RowTemplate.Height = 24;
            this.dgvTallas.Size = new System.Drawing.Size(587, 322);
            this.dgvTallas.TabIndex = 0;
            this.dgvTallas.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvTallas_CellValidating);
            // 
            // Talla
            // 
            this.Talla.DataPropertyName = "Talla";
            this.Talla.HeaderText = "Talla";
            this.Talla.Name = "Talla";
            this.Talla.Width = 200;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(12, 378);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 25);
            this.btnGuardar.TabIndex = 1;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // dgvTallasExistentes
            // 
            this.dgvTallasExistentes.AllowUserToAddRows = false;
            this.dgvTallasExistentes.AllowUserToDeleteRows = false;
            this.dgvTallasExistentes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTallasExistentes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Seleccionar,
            this.TallaExistente,
            this.Descripcion});
            this.dgvTallasExistentes.Location = new System.Drawing.Point(12, 52);
            this.dgvTallasExistentes.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvTallasExistentes.Name = "dgvTallasExistentes";
            this.dgvTallasExistentes.RowTemplate.Height = 24;
            this.dgvTallasExistentes.Size = new System.Drawing.Size(587, 322);
            this.dgvTallasExistentes.TabIndex = 2;
            // 
            // Seleccionar
            // 
            this.Seleccionar.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Seleccionar.HeaderText = "Seleccionar";
            this.Seleccionar.Name = "Seleccionar";
            this.Seleccionar.Width = 88;
            // 
            // TallaExistente
            // 
            this.TallaExistente.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.TallaExistente.DataPropertyName = "TALLA";
            this.TallaExistente.HeaderText = "Talla";
            this.TallaExistente.Name = "TallaExistente";
            this.TallaExistente.ReadOnly = true;
            this.TallaExistente.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TallaExistente.Width = 64;
            // 
            // Descripcion
            // 
            this.Descripcion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Descripcion.DataPropertyName = "DESCR";
            this.Descripcion.HeaderText = "Descripción";
            this.Descripcion.Name = "Descripcion";
            this.Descripcion.ReadOnly = true;
            this.Descripcion.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Descripcion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Descripcion.Width = 88;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(12, 13);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(60, 25);
            this.lblTitulo.TabIndex = 3;
            this.lblTitulo.Text = "Titulo";
            // 
            // frmCargaTallasEspeciales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 414);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.dgvTallasExistentes);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.dgvTallas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmCargaTallasEspeciales";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Solicitud de tallas";
            this.Load += new System.EventHandler(this.frmCargaTallasEspeciales_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTallas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTallasExistentes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTallas;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Talla;
        private System.Windows.Forms.DataGridView dgvTallasExistentes;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Seleccionar;
        private System.Windows.Forms.DataGridViewTextBoxColumn TallaExistente;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descripcion;
        private System.Windows.Forms.Label lblTitulo;
    }
}