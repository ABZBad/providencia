namespace SIP
{
    partial class frmCorporativos
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
            this.label1 = new System.Windows.Forms.Label();
            this.dgvClientesCorporativo = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbCorporativos = new System.Windows.Forms.ComboBox();
            this.btnAgregarCorporativo = new System.Windows.Forms.Button();
            this.btnEliminaCorporativo = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.cmbClientes = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblClientesCorporativo = new System.Windows.Forms.Label();
            this.Clave = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Eliminar = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientesCorporativo)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(496, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "GESTIÓN DE CLIENTES - CORPORATIVOS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvClientesCorporativo
            // 
            this.dgvClientesCorporativo.AllowUserToAddRows = false;
            this.dgvClientesCorporativo.AllowUserToDeleteRows = false;
            this.dgvClientesCorporativo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvClientesCorporativo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClientesCorporativo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Clave,
            this.Nombre,
            this.Eliminar});
            this.dgvClientesCorporativo.Location = new System.Drawing.Point(15, 99);
            this.dgvClientesCorporativo.Name = "dgvClientesCorporativo";
            this.dgvClientesCorporativo.ReadOnly = true;
            this.dgvClientesCorporativo.Size = new System.Drawing.Size(487, 219);
            this.dgvClientesCorporativo.TabIndex = 3;
            this.dgvClientesCorporativo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvClientesCorporativo_CellContentClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Seleccionar el Corporativo";
            // 
            // cmbCorporativos
            // 
            this.cmbCorporativos.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCorporativos.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCorporativos.FormattingEnabled = true;
            this.cmbCorporativos.Location = new System.Drawing.Point(149, 54);
            this.cmbCorporativos.Name = "cmbCorporativos";
            this.cmbCorporativos.Size = new System.Drawing.Size(296, 21);
            this.cmbCorporativos.TabIndex = 8;
            this.cmbCorporativos.SelectedIndexChanged += new System.EventHandler(this.cmbCorporativos_SelectedIndexChanged);
            // 
            // btnAgregarCorporativo
            // 
            this.btnAgregarCorporativo.Location = new System.Drawing.Point(451, 52);
            this.btnAgregarCorporativo.Name = "btnAgregarCorporativo";
            this.btnAgregarCorporativo.Size = new System.Drawing.Size(19, 23);
            this.btnAgregarCorporativo.TabIndex = 9;
            this.btnAgregarCorporativo.Text = "+";
            this.btnAgregarCorporativo.UseVisualStyleBackColor = true;
            this.btnAgregarCorporativo.Click += new System.EventHandler(this.btnAgregarCorporativo_Click);
            // 
            // btnEliminaCorporativo
            // 
            this.btnEliminaCorporativo.Location = new System.Drawing.Point(476, 52);
            this.btnEliminaCorporativo.Name = "btnEliminaCorporativo";
            this.btnEliminaCorporativo.Size = new System.Drawing.Size(19, 23);
            this.btnEliminaCorporativo.TabIndex = 10;
            this.btnEliminaCorporativo.Text = "-";
            this.btnEliminaCorporativo.UseVisualStyleBackColor = true;
            this.btnEliminaCorporativo.Click += new System.EventHandler(this.btnEliminaCorporativo_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(483, 319);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(19, 23);
            this.btnAgregar.TabIndex = 13;
            this.btnAgregar.Text = "+";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // cmbClientes
            // 
            this.cmbClientes.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbClientes.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbClientes.FormattingEnabled = true;
            this.cmbClientes.Location = new System.Drawing.Point(181, 321);
            this.cmbClientes.Name = "cmbClientes";
            this.cmbClientes.Size = new System.Drawing.Size(296, 21);
            this.cmbClientes.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(79, 324);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Listado de Clientes";
            // 
            // lblClientesCorporativo
            // 
            this.lblClientesCorporativo.AutoSize = true;
            this.lblClientesCorporativo.Location = new System.Drawing.Point(146, 83);
            this.lblClientesCorporativo.Name = "lblClientesCorporativo";
            this.lblClientesCorporativo.Size = new System.Drawing.Size(258, 13);
            this.lblClientesCorporativo.TabIndex = 14;
            this.lblClientesCorporativo.Text = "CLIENTES QUE PERTENECEN AL CORPORATIVO";
            // 
            // Clave
            // 
            this.Clave.DataPropertyName = "ClaveCliente";
            this.Clave.HeaderText = "Clave";
            this.Clave.Name = "Clave";
            this.Clave.ReadOnly = true;
            this.Clave.Width = 50;
            // 
            // Nombre
            // 
            this.Nombre.DataPropertyName = "NombreCliente";
            this.Nombre.HeaderText = "Nombre";
            this.Nombre.Name = "Nombre";
            this.Nombre.ReadOnly = true;
            this.Nombre.Width = 270;
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
            // frmCorporativos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 357);
            this.Controls.Add(this.lblClientesCorporativo);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.cmbClientes);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnEliminaCorporativo);
            this.Controls.Add(this.btnAgregarCorporativo);
            this.Controls.Add(this.cmbCorporativos);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgvClientesCorporativo);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCorporativos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestión de Corporativos ";
            this.Load += new System.EventHandler(this.frmCorporativos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientesCorporativo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvClientesCorporativo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbCorporativos;
        private System.Windows.Forms.Button btnAgregarCorporativo;
        private System.Windows.Forms.Button btnEliminaCorporativo;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.ComboBox cmbClientes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblClientesCorporativo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Clave;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewButtonColumn Eliminar;

    }
}