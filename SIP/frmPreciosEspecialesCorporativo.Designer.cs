namespace SIP
{
    partial class frmPreciosEspecialesCorporativo
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
            this.cmbCorporativos = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblClientesCorporativo = new System.Windows.Forms.Label();
            this.dgvArticulosCorporativo = new System.Windows.Forms.DataGridView();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.cmbArticulos = new System.Windows.Forms.ComboBox();
            this.lblListadoArticulos = new System.Windows.Forms.Label();
            this.Articulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Eliminar = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvArticulosCorporativo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitulo.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(12, 9);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(602, 23);
            this.lblTitulo.TabIndex = 3;
            this.lblTitulo.Text = "CONTROL DE PRECIOS ESPECIALES";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbCorporativos
            // 
            this.cmbCorporativos.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCorporativos.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCorporativos.FormattingEnabled = true;
            this.cmbCorporativos.Location = new System.Drawing.Point(146, 38);
            this.cmbCorporativos.Name = "cmbCorporativos";
            this.cmbCorporativos.Size = new System.Drawing.Size(304, 21);
            this.cmbCorporativos.TabIndex = 10;
            this.cmbCorporativos.SelectedIndexChanged += new System.EventHandler(this.cmbCorporativos_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Seleccionar el Corporativo";
            // 
            // lblClientesCorporativo
            // 
            this.lblClientesCorporativo.AutoSize = true;
            this.lblClientesCorporativo.Location = new System.Drawing.Point(134, 78);
            this.lblClientesCorporativo.Name = "lblClientesCorporativo";
            this.lblClientesCorporativo.Size = new System.Drawing.Size(327, 13);
            this.lblClientesCorporativo.TabIndex = 15;
            this.lblClientesCorporativo.Text = "PRODUCTOS CON PRECIO ESPECIAL PARA EL CORPORATIVO";
            // 
            // dgvArticulosCorporativo
            // 
            this.dgvArticulosCorporativo.AllowUserToAddRows = false;
            this.dgvArticulosCorporativo.AllowUserToDeleteRows = false;
            this.dgvArticulosCorporativo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvArticulosCorporativo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvArticulosCorporativo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Articulo,
            this.Descripcion,
            this.Precio,
            this.Eliminar});
            this.dgvArticulosCorporativo.Location = new System.Drawing.Point(12, 94);
            this.dgvArticulosCorporativo.Name = "dgvArticulosCorporativo";
            this.dgvArticulosCorporativo.ReadOnly = true;
            this.dgvArticulosCorporativo.Size = new System.Drawing.Size(610, 219);
            this.dgvArticulosCorporativo.TabIndex = 16;
            this.dgvArticulosCorporativo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvArticulosCorporativo_CellContentClick);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(603, 319);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(19, 23);
            this.btnAgregar.TabIndex = 19;
            this.btnAgregar.Text = "+";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // cmbArticulos
            // 
            this.cmbArticulos.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbArticulos.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbArticulos.FormattingEnabled = true;
            this.cmbArticulos.Location = new System.Drawing.Point(117, 321);
            this.cmbArticulos.Name = "cmbArticulos";
            this.cmbArticulos.Size = new System.Drawing.Size(480, 21);
            this.cmbArticulos.TabIndex = 18;
            // 
            // lblListadoArticulos
            // 
            this.lblListadoArticulos.AutoSize = true;
            this.lblListadoArticulos.Location = new System.Drawing.Point(12, 324);
            this.lblListadoArticulos.Name = "lblListadoArticulos";
            this.lblListadoArticulos.Size = new System.Drawing.Size(99, 13);
            this.lblListadoArticulos.TabIndex = 17;
            this.lblListadoArticulos.Text = "Listado de Articulos";
            // 
            // Articulo
            // 
            this.Articulo.DataPropertyName = "Articulo";
            this.Articulo.HeaderText = "Articulo";
            this.Articulo.Name = "Articulo";
            this.Articulo.ReadOnly = true;
            this.Articulo.Width = 80;
            // 
            // Descripcion
            // 
            this.Descripcion.DataPropertyName = "Descripcion";
            this.Descripcion.HeaderText = "Descripción";
            this.Descripcion.Name = "Descripcion";
            this.Descripcion.ReadOnly = true;
            this.Descripcion.Width = 270;
            // 
            // Precio
            // 
            this.Precio.DataPropertyName = "Precio";
            this.Precio.HeaderText = "Precio";
            this.Precio.Name = "Precio";
            this.Precio.ReadOnly = true;
            // 
            // Eliminar
            // 
            this.Eliminar.HeaderText = "Eliminar";
            this.Eliminar.Name = "Eliminar";
            this.Eliminar.ReadOnly = true;
            this.Eliminar.Text = "Eliminar";
            this.Eliminar.ToolTipText = "Eliminar";
            this.Eliminar.UseColumnTextForButtonValue = true;
            this.Eliminar.Width = 50;
            // 
            // frmPreciosEspecialesCorporativo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 358);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.cmbArticulos);
            this.Controls.Add(this.lblListadoArticulos);
            this.Controls.Add(this.dgvArticulosCorporativo);
            this.Controls.Add(this.lblClientesCorporativo);
            this.Controls.Add(this.cmbCorporativos);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblTitulo);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPreciosEspecialesCorporativo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Precios Especiales - Corporativos";
            this.Load += new System.EventHandler(this.frmPreciosEspecialesCorporativo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvArticulosCorporativo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.ComboBox cmbCorporativos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblClientesCorporativo;
        private System.Windows.Forms.DataGridView dgvArticulosCorporativo;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.ComboBox cmbArticulos;
        private System.Windows.Forms.Label lblListadoArticulos;
        private System.Windows.Forms.DataGridViewTextBoxColumn Articulo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precio;
        private System.Windows.Forms.DataGridViewButtonColumn Eliminar;
    }
}