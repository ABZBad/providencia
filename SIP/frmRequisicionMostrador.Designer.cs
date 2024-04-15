namespace SIP
{
    partial class frmRequisicionMostrador
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
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgViewTallas3 = new System.Windows.Forms.DataGridView();
            this.dgViewTallas2 = new System.Windows.Forms.DataGridView();
            this.dgViewTallas1 = new System.Windows.Forms.DataGridView();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.lblTotalPrendas = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvTallas = new System.Windows.Forms.DataGridView();
            this.resumen_modelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.resumen_tallas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.resumen_total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.resumen_accion = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.lblProcesando = new System.Windows.Forms.Label();
            this.lblModeloDescripcion = new System.Windows.Forms.Label();
            this.cmbOrigenDestino = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtModelo = new SIP.UserControls.TextBoxEx();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTotalPrendasRequisicion = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTallas)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 46);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Modelo:";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox3.Controls.Add(this.dgViewTallas3);
            this.groupBox3.Controls.Add(this.dgViewTallas2);
            this.groupBox3.Controls.Add(this.dgViewTallas1);
            this.groupBox3.Location = new System.Drawing.Point(13, 121);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(1182, 405);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tallas";
            // 
            // dgViewTallas3
            // 
            this.dgViewTallas3.AllowUserToAddRows = false;
            this.dgViewTallas3.AllowUserToDeleteRows = false;
            this.dgViewTallas3.AllowUserToResizeColumns = false;
            this.dgViewTallas3.AllowUserToResizeRows = false;
            this.dgViewTallas3.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgViewTallas3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgViewTallas3.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgViewTallas3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgViewTallas3.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgViewTallas3.Location = new System.Drawing.Point(29, 263);
            this.dgViewTallas3.Margin = new System.Windows.Forms.Padding(4);
            this.dgViewTallas3.MultiSelect = false;
            this.dgViewTallas3.Name = "dgViewTallas3";
            this.dgViewTallas3.RowHeadersVisible = false;
            this.dgViewTallas3.Size = new System.Drawing.Size(1124, 97);
            this.dgViewTallas3.TabIndex = 4;
            this.dgViewTallas3.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgViewTallas3_CellEndEdit);
            this.dgViewTallas3.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgViewTallas3_ColumnAdded);
            this.dgViewTallas3.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgViewTallas3_DataError);
            this.dgViewTallas3.Leave += new System.EventHandler(this.dgViewTallas3_Leave);
            // 
            // dgViewTallas2
            // 
            this.dgViewTallas2.AllowUserToAddRows = false;
            this.dgViewTallas2.AllowUserToDeleteRows = false;
            this.dgViewTallas2.AllowUserToResizeColumns = false;
            this.dgViewTallas2.AllowUserToResizeRows = false;
            this.dgViewTallas2.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgViewTallas2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgViewTallas2.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgViewTallas2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgViewTallas2.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgViewTallas2.Location = new System.Drawing.Point(29, 158);
            this.dgViewTallas2.Margin = new System.Windows.Forms.Padding(4);
            this.dgViewTallas2.MultiSelect = false;
            this.dgViewTallas2.Name = "dgViewTallas2";
            this.dgViewTallas2.RowHeadersVisible = false;
            this.dgViewTallas2.Size = new System.Drawing.Size(1124, 97);
            this.dgViewTallas2.TabIndex = 3;
            this.dgViewTallas2.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgViewTallas2_CellEndEdit);
            this.dgViewTallas2.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgViewTallas2_ColumnAdded);
            this.dgViewTallas2.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgViewTallas2_DataError);
            this.dgViewTallas2.Leave += new System.EventHandler(this.dgViewTallas2_Leave);
            // 
            // dgViewTallas1
            // 
            this.dgViewTallas1.AllowUserToAddRows = false;
            this.dgViewTallas1.AllowUserToDeleteRows = false;
            this.dgViewTallas1.AllowUserToResizeColumns = false;
            this.dgViewTallas1.AllowUserToResizeRows = false;
            this.dgViewTallas1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgViewTallas1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgViewTallas1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dgViewTallas1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgViewTallas1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgViewTallas1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgViewTallas1.Location = new System.Drawing.Point(29, 53);
            this.dgViewTallas1.Margin = new System.Windows.Forms.Padding(4);
            this.dgViewTallas1.MultiSelect = false;
            this.dgViewTallas1.Name = "dgViewTallas1";
            this.dgViewTallas1.RowHeadersVisible = false;
            this.dgViewTallas1.Size = new System.Drawing.Size(1124, 97);
            this.dgViewTallas1.TabIndex = 2;
            this.dgViewTallas1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgViewTallas1_CellEndEdit);
            this.dgViewTallas1.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgViewTallas1_ColumnAdded);
            this.dgViewTallas1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgViewTallas1_DataError);
            this.dgViewTallas1.Leave += new System.EventHandler(this.dgViewTallas1_Leave);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Enabled = false;
            this.btnAgregar.Location = new System.Drawing.Point(1095, 534);
            this.btnAgregar.Margin = new System.Windows.Forms.Padding(4);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(100, 28);
            this.btnAgregar.TabIndex = 5;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(987, 534);
            this.btnLimpiar.Margin = new System.Windows.Forms.Padding(4);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(100, 28);
            this.btnLimpiar.TabIndex = 13;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // lblTotalPrendas
            // 
            this.lblTotalPrendas.AutoSize = true;
            this.lblTotalPrendas.Location = new System.Drawing.Point(167, 540);
            this.lblTotalPrendas.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalPrendas.Name = "lblTotalPrendas";
            this.lblTotalPrendas.Size = new System.Drawing.Size(0, 17);
            this.lblTotalPrendas.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(39, 540);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 17);
            this.label5.TabIndex = 12;
            this.label5.Text = "Total de prendas:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblTotalPrendasRequisicion);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dgvTallas);
            this.groupBox1.Location = new System.Drawing.Point(16, 569);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1179, 242);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Resumen de la requisición";
            // 
            // dgvTallas
            // 
            this.dgvTallas.AllowUserToAddRows = false;
            this.dgvTallas.AllowUserToDeleteRows = false;
            this.dgvTallas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTallas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.resumen_modelo,
            this.resumen_tallas,
            this.resumen_total,
            this.resumen_accion});
            this.dgvTallas.Location = new System.Drawing.Point(26, 35);
            this.dgvTallas.Name = "dgvTallas";
            this.dgvTallas.RowTemplate.Height = 24;
            this.dgvTallas.Size = new System.Drawing.Size(1124, 150);
            this.dgvTallas.TabIndex = 0;
            this.dgvTallas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTallas_CellClick);
            // 
            // resumen_modelo
            // 
            this.resumen_modelo.DataPropertyName = "modelo";
            this.resumen_modelo.HeaderText = "Modelo";
            this.resumen_modelo.Name = "resumen_modelo";
            this.resumen_modelo.ReadOnly = true;
            // 
            // resumen_tallas
            // 
            this.resumen_tallas.DataPropertyName = "tallas";
            this.resumen_tallas.HeaderText = "Tallas";
            this.resumen_tallas.Name = "resumen_tallas";
            this.resumen_tallas.ReadOnly = true;
            // 
            // resumen_total
            // 
            this.resumen_total.DataPropertyName = "total";
            this.resumen_total.HeaderText = "Total";
            this.resumen_total.Name = "resumen_total";
            this.resumen_total.ReadOnly = true;
            // 
            // resumen_accion
            // 
            this.resumen_accion.HeaderText = "Acción";
            this.resumen_accion.Name = "resumen_accion";
            this.resumen_accion.ReadOnly = true;
            this.resumen_accion.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.resumen_accion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.resumen_accion.Text = "Eliminar";
            this.resumen_accion.UseColumnTextForButtonValue = true;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Enabled = false;
            this.btnGuardar.Location = new System.Drawing.Point(987, 820);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(4);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(208, 28);
            this.btnGuardar.TabIndex = 12;
            this.btnGuardar.Text = "Generar requisición";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // lblProcesando
            // 
            this.lblProcesando.AutoSize = true;
            this.lblProcesando.Location = new System.Drawing.Point(13, 78);
            this.lblProcesando.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProcesando.Name = "lblProcesando";
            this.lblProcesando.Size = new System.Drawing.Size(96, 17);
            this.lblProcesando.TabIndex = 35;
            this.lblProcesando.Text = "Procesando...";
            this.lblProcesando.Visible = false;
            // 
            // lblModeloDescripcion
            // 
            this.lblModeloDescripcion.Location = new System.Drawing.Point(221, 69);
            this.lblModeloDescripcion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblModeloDescripcion.Name = "lblModeloDescripcion";
            this.lblModeloDescripcion.Size = new System.Drawing.Size(416, 37);
            this.lblModeloDescripcion.TabIndex = 36;
            // 
            // cmbOrigenDestino
            // 
            this.cmbOrigenDestino.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOrigenDestino.FormattingEnabled = true;
            this.cmbOrigenDestino.Items.AddRange(new object[] {
            "Almacén 4 al 3",
            "Almacén 3 al 1"});
            this.cmbOrigenDestino.Location = new System.Drawing.Point(224, 12);
            this.cmbOrigenDestino.Name = "cmbOrigenDestino";
            this.cmbOrigenDestino.Size = new System.Drawing.Size(210, 24);
            this.cmbOrigenDestino.TabIndex = 41;
            this.cmbOrigenDestino.SelectedIndexChanged += new System.EventHandler(this.cmbOrigenDestino_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 15);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(204, 17);
            this.label4.TabIndex = 42;
            this.label4.Text = "Selecione tipo de transferencia";
            // 
            // txtModelo
            // 
            this.txtModelo.Location = new System.Drawing.Point(224, 43);
            this.txtModelo.Margin = new System.Windows.Forms.Padding(4);
            this.txtModelo.Name = "txtModelo";
            this.txtModelo.OnlyUpperCase = true;
            this.txtModelo.Size = new System.Drawing.Size(210, 22);
            this.txtModelo.TabIndex = 1;
            this.txtModelo.TextChanged += new System.EventHandler(this.txtModelo_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(928, 188);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 17);
            this.label1.TabIndex = 43;
            this.label1.Text = "Total de prendas:";
            // 
            // lblTotalPrendasRequisicion
            // 
            this.lblTotalPrendasRequisicion.AutoSize = true;
            this.lblTotalPrendasRequisicion.Location = new System.Drawing.Point(1052, 188);
            this.lblTotalPrendasRequisicion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalPrendasRequisicion.Name = "lblTotalPrendasRequisicion";
            this.lblTotalPrendasRequisicion.Size = new System.Drawing.Size(0, 17);
            this.lblTotalPrendasRequisicion.TabIndex = 44;
            // 
            // frmRequisicionMostrador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1223, 869);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbOrigenDestino);
            this.Controls.Add(this.lblModeloDescripcion);
            this.Controls.Add(this.lblProcesando);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblTotalPrendas);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.txtModelo);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmRequisicionMostrador";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nueva requisición de mostrador";
            this.Load += new System.EventHandler(this.frmRequisicionMostrador_Load);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTallas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UserControls.TextBoxEx txtModelo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgViewTallas3;
        private System.Windows.Forms.DataGridView dgViewTallas2;
        private System.Windows.Forms.DataGridView dgViewTallas1;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Label lblTotalPrendas;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label lblProcesando;
        private System.Windows.Forms.Label lblModeloDescripcion;
        private System.Windows.Forms.DataGridView dgvTallas;
        private System.Windows.Forms.DataGridViewTextBoxColumn resumen_modelo;
        private System.Windows.Forms.DataGridViewTextBoxColumn resumen_tallas;
        private System.Windows.Forms.DataGridViewTextBoxColumn resumen_total;
        private System.Windows.Forms.DataGridViewButtonColumn resumen_accion;
        private System.Windows.Forms.ComboBox cmbOrigenDestino;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblTotalPrendasRequisicion;
        private System.Windows.Forms.Label label1;
    }
}