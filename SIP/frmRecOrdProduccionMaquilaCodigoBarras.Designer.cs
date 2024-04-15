namespace SIP
{
    partial class frmRecOrdProduccionMaquilaCodigoBarras
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
            this.lblCodigo = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.dgvRecepciones = new System.Windows.Forms.DataGridView();
            this.lblTotalLineaTexto = new System.Windows.Forms.Label();
            this.lblTotalLinea = new System.Windows.Forms.Label();
            this.lblTotalEspecialesTexto = new System.Windows.Forms.Label();
            this.lblTotalEspeciales = new System.Windows.Forms.Label();
            this.lblTotalTexto = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnProcesar = new System.Windows.Forms.Button();
            this.lblTotalDefectuosos = new System.Windows.Forms.Label();
            this.lblTotalDefectuososTexto = new System.Windows.Forms.Label();
            this.txtCodDefectuosos = new System.Windows.Forms.TextBox();
            this.txtPrefijo = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tvResumen = new System.Windows.Forms.TreeView();
            this.btnNuevaRecepcion = new System.Windows.Forms.Button();
            this.btnExportarExcel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTotalCodigosEscaneados = new System.Windows.Forms.Label();
            this.lblTituloTotalCodigos = new System.Windows.Forms.Label();
            this.txtEsquemaImp = new SIP.UserControls.NumericTextBox();
            this.Orden = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Maquila = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Modelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Talla = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Recibidos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Defectuosos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Eliminar = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecepciones)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(291, 25);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(334, 29);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Recepción de OP - Maquila";
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Location = new System.Drawing.Point(16, 102);
            this.lblCodigo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(142, 17);
            this.lblCodigo.TabIndex = 2;
            this.lblCodigo.Text = "Código de recepción:";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigo.Location = new System.Drawing.Point(168, 90);
            this.txtCodigo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(896, 30);
            this.txtCodigo.TabIndex = 3;
            this.txtCodigo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCodigo_KeyDown);
            this.txtCodigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigo_KeyPress);
            // 
            // dgvRecepciones
            // 
            this.dgvRecepciones.AllowUserToAddRows = false;
            this.dgvRecepciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecepciones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Orden,
            this.Maquila,
            this.OP,
            this.Modelo,
            this.Talla,
            this.Cantidad,
            this.Recibidos,
            this.Defectuosos,
            this.Eliminar});
            this.dgvRecepciones.Location = new System.Drawing.Point(16, 239);
            this.dgvRecepciones.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvRecepciones.Name = "dgvRecepciones";
            this.dgvRecepciones.Size = new System.Drawing.Size(1049, 377);
            this.dgvRecepciones.TabIndex = 4;
            this.dgvRecepciones.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRecepciones_CellClick);
            this.dgvRecepciones.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRecepciones_CellEndEdit);
            this.dgvRecepciones.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvRecepciones_CellValidating);
            this.dgvRecepciones.Click += new System.EventHandler(this.dgvRecepciones_Click);
            // 
            // lblTotalLineaTexto
            // 
            this.lblTotalLineaTexto.AutoSize = true;
            this.lblTotalLineaTexto.Location = new System.Drawing.Point(16, 638);
            this.lblTotalLineaTexto.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalLineaTexto.Name = "lblTotalLineaTexto";
            this.lblTotalLineaTexto.Size = new System.Drawing.Size(180, 17);
            this.lblTotalLineaTexto.TabIndex = 5;
            this.lblTotalLineaTexto.Text = "Total de Prendas de Línea:";
            // 
            // lblTotalLinea
            // 
            this.lblTotalLinea.AutoSize = true;
            this.lblTotalLinea.Location = new System.Drawing.Point(289, 638);
            this.lblTotalLinea.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalLinea.Name = "lblTotalLinea";
            this.lblTotalLinea.Size = new System.Drawing.Size(16, 17);
            this.lblTotalLinea.TabIndex = 6;
            this.lblTotalLinea.Text = "0";
            // 
            // lblTotalEspecialesTexto
            // 
            this.lblTotalEspecialesTexto.AutoSize = true;
            this.lblTotalEspecialesTexto.Location = new System.Drawing.Point(16, 661);
            this.lblTotalEspecialesTexto.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalEspecialesTexto.Name = "lblTotalEspecialesTexto";
            this.lblTotalEspecialesTexto.Size = new System.Drawing.Size(193, 17);
            this.lblTotalEspecialesTexto.TabIndex = 7;
            this.lblTotalEspecialesTexto.Text = "Total de Prendas Especiales:";
            // 
            // lblTotalEspeciales
            // 
            this.lblTotalEspeciales.AutoSize = true;
            this.lblTotalEspeciales.Location = new System.Drawing.Point(289, 661);
            this.lblTotalEspeciales.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalEspeciales.Name = "lblTotalEspeciales";
            this.lblTotalEspeciales.Size = new System.Drawing.Size(16, 17);
            this.lblTotalEspeciales.TabIndex = 8;
            this.lblTotalEspeciales.Text = "0";
            // 
            // lblTotalTexto
            // 
            this.lblTotalTexto.AutoSize = true;
            this.lblTotalTexto.Location = new System.Drawing.Point(16, 704);
            this.lblTotalTexto.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalTexto.Name = "lblTotalTexto";
            this.lblTotalTexto.Size = new System.Drawing.Size(187, 17);
            this.lblTotalTexto.TabIndex = 9;
            this.lblTotalTexto.Text = "Total de Prendas Recibidas:";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(289, 704);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(16, 17);
            this.lblTotal.TabIndex = 10;
            this.lblTotal.Text = "0";
            // 
            // btnProcesar
            // 
            this.btnProcesar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProcesar.Location = new System.Drawing.Point(895, 623);
            this.btnProcesar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnProcesar.Name = "btnProcesar";
            this.btnProcesar.Size = new System.Drawing.Size(172, 97);
            this.btnProcesar.TabIndex = 11;
            this.btnProcesar.Text = "F2\r\nPROCESAR";
            this.btnProcesar.UseVisualStyleBackColor = true;
            this.btnProcesar.Click += new System.EventHandler(this.btnProcesar_Click);
            // 
            // lblTotalDefectuosos
            // 
            this.lblTotalDefectuosos.AutoSize = true;
            this.lblTotalDefectuosos.Location = new System.Drawing.Point(289, 683);
            this.lblTotalDefectuosos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalDefectuosos.Name = "lblTotalDefectuosos";
            this.lblTotalDefectuosos.Size = new System.Drawing.Size(16, 17);
            this.lblTotalDefectuosos.TabIndex = 13;
            this.lblTotalDefectuosos.Text = "0";
            // 
            // lblTotalDefectuososTexto
            // 
            this.lblTotalDefectuososTexto.AutoSize = true;
            this.lblTotalDefectuososTexto.Location = new System.Drawing.Point(16, 683);
            this.lblTotalDefectuososTexto.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalDefectuososTexto.Name = "lblTotalDefectuososTexto";
            this.lblTotalDefectuososTexto.Size = new System.Drawing.Size(147, 17);
            this.lblTotalDefectuososTexto.TabIndex = 12;
            this.lblTotalDefectuososTexto.Text = "Total de Defectuosos:";
            // 
            // txtCodDefectuosos
            // 
            this.txtCodDefectuosos.Location = new System.Drawing.Point(856, 167);
            this.txtCodDefectuosos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCodDefectuosos.Name = "txtCodDefectuosos";
            this.txtCodDefectuosos.Size = new System.Drawing.Size(92, 22);
            this.txtCodDefectuosos.TabIndex = 18;
            this.txtCodDefectuosos.Text = "PRENDASA";
            // 
            // txtPrefijo
            // 
            this.txtPrefijo.Location = new System.Drawing.Point(572, 167);
            this.txtPrefijo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPrefijo.Name = "txtPrefijo";
            this.txtPrefijo.Size = new System.Drawing.Size(19, 22);
            this.txtPrefijo.TabIndex = 17;
            this.txtPrefijo.Text = "X";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(753, 171);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 17);
            this.label9.TabIndex = 21;
            this.label9.Text = "CodDef";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(469, 171);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 17);
            this.label8.TabIndex = 20;
            this.label8.Text = "Prefijo";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(120, 167);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 17);
            this.label5.TabIndex = 19;
            this.label5.Text = "Esquema Imp";
            // 
            // tvResumen
            // 
            this.tvResumen.Location = new System.Drawing.Point(1091, 90);
            this.tvResumen.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tvResumen.Name = "tvResumen";
            this.tvResumen.Size = new System.Drawing.Size(569, 623);
            this.tvResumen.TabIndex = 22;
            // 
            // btnNuevaRecepcion
            // 
            this.btnNuevaRecepcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevaRecepcion.Location = new System.Drawing.Point(715, 623);
            this.btnNuevaRecepcion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnNuevaRecepcion.Name = "btnNuevaRecepcion";
            this.btnNuevaRecepcion.Size = new System.Drawing.Size(172, 97);
            this.btnNuevaRecepcion.TabIndex = 23;
            this.btnNuevaRecepcion.Text = "F3\r\nNUEVA RECEPCIÓN";
            this.btnNuevaRecepcion.UseVisualStyleBackColor = true;
            this.btnNuevaRecepcion.Click += new System.EventHandler(this.btnNuevaRecepcion_Click);
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.Enabled = false;
            this.btnExportarExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportarExcel.Location = new System.Drawing.Point(535, 623);
            this.btnExportarExcel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(172, 97);
            this.btnExportarExcel.TabIndex = 24;
            this.btnExportarExcel.Text = "F4\r\nGENERAR EXCEL";
            this.btnExportarExcel.UseVisualStyleBackColor = true;
            this.btnExportarExcel.Visible = false;
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1099, 70);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 17);
            this.label1.TabIndex = 25;
            this.label1.Text = "Resumen:";
            // 
            // lblTotalCodigosEscaneados
            // 
            this.lblTotalCodigosEscaneados.AutoSize = true;
            this.lblTotalCodigosEscaneados.Location = new System.Drawing.Point(979, 219);
            this.lblTotalCodigosEscaneados.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalCodigosEscaneados.Name = "lblTotalCodigosEscaneados";
            this.lblTotalCodigosEscaneados.Size = new System.Drawing.Size(16, 17);
            this.lblTotalCodigosEscaneados.TabIndex = 27;
            this.lblTotalCodigosEscaneados.Text = "0";
            this.lblTotalCodigosEscaneados.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTituloTotalCodigos
            // 
            this.lblTituloTotalCodigos.AutoSize = true;
            this.lblTituloTotalCodigos.Location = new System.Drawing.Point(705, 219);
            this.lblTituloTotalCodigos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTituloTotalCodigos.Name = "lblTituloTotalCodigos";
            this.lblTituloTotalCodigos.Size = new System.Drawing.Size(201, 17);
            this.lblTituloTotalCodigos.TabIndex = 26;
            this.lblTituloTotalCodigos.Text = "Total de Códigos Escaneados:\r\n";
            // 
            // txtEsquemaImp
            // 
            this.txtEsquemaImp.Location = new System.Drawing.Point(223, 164);
            this.txtEsquemaImp.Margin = new System.Windows.Forms.Padding(4);
            this.txtEsquemaImp.MaxValue = 0;
            this.txtEsquemaImp.MinValue = 0;
            this.txtEsquemaImp.Name = "txtEsquemaImp";
            this.txtEsquemaImp.Size = new System.Drawing.Size(67, 22);
            this.txtEsquemaImp.TabIndex = 16;
            this.txtEsquemaImp.Text = "9";
            // 
            // Orden
            // 
            this.Orden.DataPropertyName = "Referencia";
            this.Orden.HeaderText = "Referencia";
            this.Orden.Name = "Orden";
            this.Orden.ReadOnly = true;
            // 
            // Maquila
            // 
            this.Maquila.DataPropertyName = "OrdenMaquila";
            this.Maquila.HeaderText = "Maquila";
            this.Maquila.Name = "Maquila";
            this.Maquila.ReadOnly = true;
            this.Maquila.Width = 70;
            // 
            // OP
            // 
            this.OP.DataPropertyName = "OrdenProduccion";
            this.OP.HeaderText = "OP";
            this.OP.Name = "OP";
            this.OP.Width = 60;
            // 
            // Modelo
            // 
            this.Modelo.DataPropertyName = "Modelo";
            this.Modelo.HeaderText = "Modelo";
            this.Modelo.Name = "Modelo";
            this.Modelo.ReadOnly = true;
            // 
            // Talla
            // 
            this.Talla.DataPropertyName = "Talla";
            this.Talla.HeaderText = "Talla";
            this.Talla.Name = "Talla";
            this.Talla.ReadOnly = true;
            this.Talla.Width = 70;
            // 
            // Cantidad
            // 
            this.Cantidad.DataPropertyName = "Cantidad";
            this.Cantidad.HeaderText = "Cantidad";
            this.Cantidad.Name = "Cantidad";
            this.Cantidad.ReadOnly = true;
            this.Cantidad.Width = 70;
            // 
            // Recibidos
            // 
            this.Recibidos.DataPropertyName = "Recibidos";
            this.Recibidos.HeaderText = "Recibidos";
            this.Recibidos.Name = "Recibidos";
            this.Recibidos.ReadOnly = true;
            this.Recibidos.Width = 70;
            // 
            // Defectuosos
            // 
            this.Defectuosos.DataPropertyName = "Defectuosos";
            this.Defectuosos.HeaderText = "Defectuosos";
            this.Defectuosos.Name = "Defectuosos";
            this.Defectuosos.Width = 80;
            // 
            // Eliminar
            // 
            this.Eliminar.HeaderText = "Eliminar";
            this.Eliminar.Name = "Eliminar";
            this.Eliminar.Text = "Eliminar";
            this.Eliminar.UseColumnTextForButtonValue = true;
            // 
            // frmRecOrdProduccionMaquilaCodigoBarras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1677, 729);
            this.Controls.Add(this.lblTotalCodigosEscaneados);
            this.Controls.Add(this.lblTituloTotalCodigos);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnExportarExcel);
            this.Controls.Add(this.btnNuevaRecepcion);
            this.Controls.Add(this.tvResumen);
            this.Controls.Add(this.txtCodDefectuosos);
            this.Controls.Add(this.txtPrefijo);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtEsquemaImp);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblTotalDefectuosos);
            this.Controls.Add(this.lblTotalDefectuososTexto);
            this.Controls.Add(this.btnProcesar);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblTotalTexto);
            this.Controls.Add(this.lblTotalEspeciales);
            this.Controls.Add(this.lblTotalEspecialesTexto);
            this.Controls.Add(this.lblTotalLinea);
            this.Controls.Add(this.lblTotalLineaTexto);
            this.Controls.Add(this.dgvRecepciones);
            this.Controls.Add(this.txtCodigo);
            this.Controls.Add(this.lblCodigo);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmRecOrdProduccionMaquilaCodigoBarras";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recepcion de OP y Mquila por Código de Barras";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmRecOrdProduccionMaquilaCodigoBarras_FormClosing);
            this.Load += new System.EventHandler(this.frmRecOrdProduccionMaquilaCodigoBarras_Load);
            this.Click += new System.EventHandler(this.frmRecOrdProduccionMaquilaCodigoBarras_Click);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecepciones)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.DataGridView dgvRecepciones;
        private System.Windows.Forms.Label lblTotalLineaTexto;
        private System.Windows.Forms.Label lblTotalLinea;
        private System.Windows.Forms.Label lblTotalEspecialesTexto;
        private System.Windows.Forms.Label lblTotalEspeciales;
        private System.Windows.Forms.Label lblTotalTexto;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnProcesar;
        private System.Windows.Forms.Label lblTotalDefectuosos;
        private System.Windows.Forms.Label lblTotalDefectuososTexto;
        private System.Windows.Forms.TextBox txtCodDefectuosos;
        private System.Windows.Forms.TextBox txtPrefijo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private UserControls.NumericTextBox txtEsquemaImp;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TreeView tvResumen;
        private System.Windows.Forms.Button btnNuevaRecepcion;
        private System.Windows.Forms.Button btnExportarExcel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTotalCodigosEscaneados;
        private System.Windows.Forms.Label lblTituloTotalCodigos;
        private System.Windows.Forms.DataGridViewTextBoxColumn Orden;
        private System.Windows.Forms.DataGridViewTextBoxColumn Maquila;
        private System.Windows.Forms.DataGridViewTextBoxColumn OP;
        private System.Windows.Forms.DataGridViewTextBoxColumn Modelo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Talla;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn Recibidos;
        private System.Windows.Forms.DataGridViewTextBoxColumn Defectuosos;
        private System.Windows.Forms.DataGridViewButtonColumn Eliminar;
    }
}