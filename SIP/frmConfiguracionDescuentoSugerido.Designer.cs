namespace SIP
{
    partial class frmConfiguracionDescuentoSugerido
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
            this.dgvDescuentos = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RangoMinimo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.A = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RangoMaximo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Porcentaje = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblRangoMin = new System.Windows.Forms.Label();
            this.nudMin = new System.Windows.Forms.NumericUpDown();
            this.nudMax = new System.Windows.Forms.NumericUpDown();
            this.lblRangoMax = new System.Windows.Forms.Label();
            this.lblDescuento = new System.Windows.Forms.Label();
            this.txtDescuento = new System.Windows.Forms.TextBox();
            this.txtCMP = new System.Windows.Forms.TextBox();
            this.lblCMP = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDescuentos)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMax)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(651, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "POLITICA DE DESCUENTOS VENTAS METROPOLITANAS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvDescuentos
            // 
            this.dgvDescuentos.AllowUserToAddRows = false;
            this.dgvDescuentos.AllowUserToDeleteRows = false;
            this.dgvDescuentos.AllowUserToResizeColumns = false;
            this.dgvDescuentos.AllowUserToResizeRows = false;
            this.dgvDescuentos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDescuentos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.RangoMinimo,
            this.A,
            this.RangoMaximo,
            this.Descripcion,
            this.Porcentaje,
            this.Precio});
            this.dgvDescuentos.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dgvDescuentos.Location = new System.Drawing.Point(12, 35);
            this.dgvDescuentos.Name = "dgvDescuentos";
            this.dgvDescuentos.Size = new System.Drawing.Size(651, 286);
            this.dgvDescuentos.TabIndex = 2;
            this.dgvDescuentos.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDescuentos_CellEndEdit);
            this.dgvDescuentos.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvDescuentos_DataError);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Id.Visible = false;
            // 
            // RangoMinimo
            // 
            this.RangoMinimo.DataPropertyName = "RangoMinimo";
            this.RangoMinimo.HeaderText = "Min.";
            this.RangoMinimo.Name = "RangoMinimo";
            this.RangoMinimo.ReadOnly = true;
            this.RangoMinimo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // A
            // 
            this.A.DataPropertyName = "A";
            this.A.HeaderText = "A";
            this.A.Name = "A";
            this.A.ReadOnly = true;
            this.A.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // RangoMaximo
            // 
            this.RangoMaximo.DataPropertyName = "RangoMaximo";
            this.RangoMaximo.HeaderText = "Máx.";
            this.RangoMaximo.Name = "RangoMaximo";
            this.RangoMaximo.ReadOnly = true;
            this.RangoMaximo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Descripcion
            // 
            this.Descripcion.DataPropertyName = "Descripcion";
            this.Descripcion.HeaderText = "Descripción";
            this.Descripcion.Name = "Descripcion";
            this.Descripcion.ReadOnly = true;
            this.Descripcion.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Porcentaje
            // 
            this.Porcentaje.DataPropertyName = "PorcentajeDS";
            this.Porcentaje.HeaderText = "Porcentaje S.(%)";
            this.Porcentaje.Name = "Porcentaje";
            this.Porcentaje.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Precio
            // 
            this.Precio.DataPropertyName = "PrecioCMPS";
            this.Precio.HeaderText = "Precio S.";
            this.Precio.Name = "Precio";
            this.Precio.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // btnEliminar
            // 
            this.btnEliminar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnEliminar.Location = new System.Drawing.Point(507, 327);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(75, 23);
            this.btnEliminar.TabIndex = 3;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAgregar.Location = new System.Drawing.Point(588, 327);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(75, 23);
            this.btnAgregar.TabIndex = 4;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSalir.Location = new System.Drawing.Point(12, 327);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(75, 23);
            this.btnSalir.TabIndex = 5;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Visible = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnGuardar);
            this.groupBox1.Controls.Add(this.btnCancelar);
            this.groupBox1.Controls.Add(this.txtCMP);
            this.groupBox1.Controls.Add(this.lblCMP);
            this.groupBox1.Controls.Add(this.txtDescuento);
            this.groupBox1.Controls.Add(this.lblDescuento);
            this.groupBox1.Controls.Add(this.nudMax);
            this.groupBox1.Controls.Add(this.lblRangoMax);
            this.groupBox1.Controls.Add(this.nudMin);
            this.groupBox1.Controls.Add(this.lblRangoMin);
            this.groupBox1.Location = new System.Drawing.Point(15, 356);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(648, 86);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Nuevo Rango";
            this.groupBox1.Visible = false;
            // 
            // lblRangoMin
            // 
            this.lblRangoMin.AutoSize = true;
            this.lblRangoMin.Location = new System.Drawing.Point(33, 29);
            this.lblRangoMin.Name = "lblRangoMin";
            this.lblRangoMin.Size = new System.Drawing.Size(62, 13);
            this.lblRangoMin.TabIndex = 0;
            this.lblRangoMin.Text = "Rango Min.";
            // 
            // nudMin
            // 
            this.nudMin.Location = new System.Drawing.Point(101, 27);
            this.nudMin.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudMin.Name = "nudMin";
            this.nudMin.Size = new System.Drawing.Size(61, 20);
            this.nudMin.TabIndex = 1;
            // 
            // nudMax
            // 
            this.nudMax.Location = new System.Drawing.Point(276, 27);
            this.nudMax.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudMax.Name = "nudMax";
            this.nudMax.Size = new System.Drawing.Size(61, 20);
            this.nudMax.TabIndex = 3;
            // 
            // lblRangoMax
            // 
            this.lblRangoMax.AutoSize = true;
            this.lblRangoMax.Location = new System.Drawing.Point(205, 29);
            this.lblRangoMax.Name = "lblRangoMax";
            this.lblRangoMax.Size = new System.Drawing.Size(65, 13);
            this.lblRangoMax.TabIndex = 2;
            this.lblRangoMax.Text = "Rango Max.";
            // 
            // lblDescuento
            // 
            this.lblDescuento.AutoSize = true;
            this.lblDescuento.Location = new System.Drawing.Point(362, 29);
            this.lblDescuento.Name = "lblDescuento";
            this.lblDescuento.Size = new System.Drawing.Size(104, 13);
            this.lblDescuento.TabIndex = 4;
            this.lblDescuento.Text = "Descuento Sugerido";
            // 
            // txtDescuento
            // 
            this.txtDescuento.Location = new System.Drawing.Point(472, 26);
            this.txtDescuento.Name = "txtDescuento";
            this.txtDescuento.Size = new System.Drawing.Size(52, 20);
            this.txtDescuento.TabIndex = 5;
            // 
            // txtCMP
            // 
            this.txtCMP.Location = new System.Drawing.Point(590, 25);
            this.txtCMP.Name = "txtCMP";
            this.txtCMP.Size = new System.Drawing.Size(52, 20);
            this.txtCMP.TabIndex = 7;
            // 
            // lblCMP
            // 
            this.lblCMP.AutoSize = true;
            this.lblCMP.Location = new System.Drawing.Point(552, 29);
            this.lblCMP.Name = "lblCMP";
            this.lblCMP.Size = new System.Drawing.Size(30, 13);
            this.lblCMP.TabIndex = 6;
            this.lblCMP.Text = "CMP";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(567, 57);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 8;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(486, 57);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 9;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(525, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "%";
            // 
            // frmConfiguracionDescuentoSugerido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnSalir;
            this.ClientSize = new System.Drawing.Size(675, 454);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.dgvDescuentos);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConfiguracionDescuentoSugerido";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuración de Descuentos y Precios Sugeridos";
            this.Load += new System.EventHandler(this.frmConfiguracionDescuentoSugerido_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDescuentos)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMax)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvDescuentos;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn RangoMinimo;
        private System.Windows.Forms.DataGridViewTextBoxColumn A;
        private System.Windows.Forms.DataGridViewTextBoxColumn RangoMaximo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Porcentaje;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precio;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown nudMin;
        private System.Windows.Forms.Label lblRangoMin;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.TextBox txtCMP;
        private System.Windows.Forms.Label lblCMP;
        private System.Windows.Forms.TextBox txtDescuento;
        private System.Windows.Forms.Label lblDescuento;
        private System.Windows.Forms.NumericUpDown nudMax;
        private System.Windows.Forms.Label lblRangoMax;
        private System.Windows.Forms.Label label2;
    }
}