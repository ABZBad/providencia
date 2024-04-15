namespace SIP
{
    partial class frmNotaCreditoIngreso
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gbGenerales = new System.Windows.Forms.GroupBox();
            this.lblClienteClave = new System.Windows.Forms.Label();
            this.lblAlmacen = new System.Windows.Forms.Label();
            this.lblFolio = new System.Windows.Forms.Label();
            this.lblSerie = new System.Windows.Forms.Label();
            this.lblClienteNombre = new System.Windows.Forms.Label();
            this.lblClienteRFC = new System.Windows.Forms.Label();
            this.gbDetalle = new System.Windows.Forms.GroupBox();
            this.dgvDetalle = new System.Windows.Forms.DataGridView();
            this.Seleccion = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Clave = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnidadVenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IVATasa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Subtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClaveUnidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClaveProductoServicio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.lblIVA = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblTotalImporte = new System.Windows.Forms.Label();
            this.lblIVAImporte = new System.Windows.Forms.Label();
            this.lblSubtotalImporte = new System.Windows.Forms.Label();
            this.chkCancelacion = new System.Windows.Forms.CheckBox();
            this.gbGenerales.SuspendLayout();
            this.gbDetalle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).BeginInit();
            this.SuspendLayout();
            // 
            // gbGenerales
            // 
            this.gbGenerales.Controls.Add(this.lblClienteClave);
            this.gbGenerales.Controls.Add(this.lblAlmacen);
            this.gbGenerales.Controls.Add(this.lblFolio);
            this.gbGenerales.Controls.Add(this.lblSerie);
            this.gbGenerales.Controls.Add(this.lblClienteNombre);
            this.gbGenerales.Controls.Add(this.lblClienteRFC);
            this.gbGenerales.Location = new System.Drawing.Point(12, 19);
            this.gbGenerales.Name = "gbGenerales";
            this.gbGenerales.Size = new System.Drawing.Size(1264, 125);
            this.gbGenerales.TabIndex = 107;
            this.gbGenerales.TabStop = false;
            this.gbGenerales.Text = "Datos de la Factura";
            // 
            // lblClienteClave
            // 
            this.lblClienteClave.AutoSize = true;
            this.lblClienteClave.Location = new System.Drawing.Point(18, 91);
            this.lblClienteClave.Name = "lblClienteClave";
            this.lblClienteClave.Size = new System.Drawing.Size(100, 17);
            this.lblClienteClave.TabIndex = 5;
            this.lblClienteClave.Text = "lblClienteClave";
            // 
            // lblAlmacen
            // 
            this.lblAlmacen.AutoSize = true;
            this.lblAlmacen.Location = new System.Drawing.Point(612, 91);
            this.lblAlmacen.Name = "lblAlmacen";
            this.lblAlmacen.Size = new System.Drawing.Size(76, 17);
            this.lblAlmacen.TabIndex = 4;
            this.lblAlmacen.Text = "lblAlmacen";
            // 
            // lblFolio
            // 
            this.lblFolio.AutoSize = true;
            this.lblFolio.Location = new System.Drawing.Point(612, 63);
            this.lblFolio.Name = "lblFolio";
            this.lblFolio.Size = new System.Drawing.Size(52, 17);
            this.lblFolio.TabIndex = 3;
            this.lblFolio.Text = "lblFolio";
            // 
            // lblSerie
            // 
            this.lblSerie.AutoSize = true;
            this.lblSerie.Location = new System.Drawing.Point(612, 34);
            this.lblSerie.Name = "lblSerie";
            this.lblSerie.Size = new System.Drawing.Size(55, 17);
            this.lblSerie.TabIndex = 2;
            this.lblSerie.Text = "lblSerie";
            // 
            // lblClienteNombre
            // 
            this.lblClienteNombre.AutoSize = true;
            this.lblClienteNombre.Location = new System.Drawing.Point(18, 63);
            this.lblClienteNombre.Name = "lblClienteNombre";
            this.lblClienteNombre.Size = new System.Drawing.Size(115, 17);
            this.lblClienteNombre.TabIndex = 1;
            this.lblClienteNombre.Text = "lblClienteNombre";
            // 
            // lblClienteRFC
            // 
            this.lblClienteRFC.AutoSize = true;
            this.lblClienteRFC.Location = new System.Drawing.Point(18, 34);
            this.lblClienteRFC.Name = "lblClienteRFC";
            this.lblClienteRFC.Size = new System.Drawing.Size(92, 17);
            this.lblClienteRFC.TabIndex = 0;
            this.lblClienteRFC.Text = "lblClienteRFC";
            // 
            // gbDetalle
            // 
            this.gbDetalle.Controls.Add(this.dgvDetalle);
            this.gbDetalle.Location = new System.Drawing.Point(12, 150);
            this.gbDetalle.Name = "gbDetalle";
            this.gbDetalle.Size = new System.Drawing.Size(1264, 468);
            this.gbDetalle.TabIndex = 108;
            this.gbDetalle.TabStop = false;
            this.gbDetalle.Text = "Detalle";
            // 
            // dgvDetalle
            // 
            this.dgvDetalle.AllowUserToAddRows = false;
            this.dgvDetalle.AllowUserToDeleteRows = false;
            this.dgvDetalle.AllowUserToResizeColumns = false;
            this.dgvDetalle.AllowUserToResizeRows = false;
            this.dgvDetalle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDetalle.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvDetalle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetalle.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalle.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Seleccion,
            this.Cantidad,
            this.Clave,
            this.Descripcion,
            this.TipoProducto,
            this.UnidadVenta,
            this.Precio,
            this.IVATasa,
            this.Subtotal,
            this.ClaveUnidad,
            this.ClaveProductoServicio});
            this.dgvDetalle.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvDetalle.Location = new System.Drawing.Point(6, 21);
            this.dgvDetalle.MultiSelect = false;
            this.dgvDetalle.Name = "dgvDetalle";
            this.dgvDetalle.RowTemplate.Height = 24;
            this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetalle.Size = new System.Drawing.Size(1242, 441);
            this.dgvDetalle.TabIndex = 11;
            this.dgvDetalle.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetalle_CellClick);
            this.dgvDetalle.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetalle_CellEndEdit);
            this.dgvDetalle.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvDetalle_CellFormatting);
            this.dgvDetalle.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetalle_CellLeave);
            this.dgvDetalle.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvDetalle_CellPainting);
            this.dgvDetalle.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvDetalle_CellValidating);
            // 
            // Seleccion
            // 
            this.Seleccion.DataPropertyName = "Seleccion";
            this.Seleccion.HeaderText = " ";
            this.Seleccion.Name = "Seleccion";
            this.Seleccion.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Seleccion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Seleccion.Width = 20;
            // 
            // Cantidad
            // 
            this.Cantidad.DataPropertyName = "Cantidad";
            this.Cantidad.HeaderText = "Cantidad";
            this.Cantidad.Name = "Cantidad";
            this.Cantidad.ReadOnly = true;
            this.Cantidad.Width = 80;
            // 
            // Clave
            // 
            this.Clave.DataPropertyName = "Clave";
            this.Clave.HeaderText = "Artículo";
            this.Clave.Name = "Clave";
            this.Clave.ReadOnly = true;
            this.Clave.Width = 120;
            // 
            // Descripcion
            // 
            this.Descripcion.DataPropertyName = "Descripcion";
            this.Descripcion.HeaderText = "Descripción";
            this.Descripcion.Name = "Descripcion";
            this.Descripcion.ReadOnly = true;
            this.Descripcion.Width = 250;
            // 
            // TipoProducto
            // 
            this.TipoProducto.DataPropertyName = "TipoProducto";
            this.TipoProducto.HeaderText = "T. Producto";
            this.TipoProducto.Name = "TipoProducto";
            this.TipoProducto.ReadOnly = true;
            // 
            // UnidadVenta
            // 
            this.UnidadVenta.DataPropertyName = "UnidadVenta";
            this.UnidadVenta.HeaderText = "Unidad";
            this.UnidadVenta.Name = "UnidadVenta";
            this.UnidadVenta.ReadOnly = true;
            this.UnidadVenta.Width = 80;
            // 
            // Precio
            // 
            this.Precio.DataPropertyName = "Precio";
            dataGridViewCellStyle6.Format = "C2";
            dataGridViewCellStyle6.NullValue = null;
            this.Precio.DefaultCellStyle = dataGridViewCellStyle6;
            this.Precio.HeaderText = "Precio U.";
            this.Precio.Name = "Precio";
            this.Precio.ReadOnly = true;
            this.Precio.Width = 80;
            // 
            // IVATasa
            // 
            this.IVATasa.DataPropertyName = "IVATasa";
            dataGridViewCellStyle7.Format = "P0";
            dataGridViewCellStyle7.NullValue = null;
            this.IVATasa.DefaultCellStyle = dataGridViewCellStyle7;
            this.IVATasa.HeaderText = "IVA";
            this.IVATasa.Name = "IVATasa";
            this.IVATasa.ReadOnly = true;
            this.IVATasa.Width = 80;
            // 
            // Subtotal
            // 
            this.Subtotal.DataPropertyName = "Subtotal";
            dataGridViewCellStyle8.Format = "C2";
            this.Subtotal.DefaultCellStyle = dataGridViewCellStyle8;
            this.Subtotal.HeaderText = "Subtotal";
            this.Subtotal.Name = "Subtotal";
            this.Subtotal.ReadOnly = true;
            this.Subtotal.Width = 80;
            // 
            // ClaveUnidad
            // 
            this.ClaveUnidad.DataPropertyName = "ClaveUnidad";
            this.ClaveUnidad.HeaderText = "ClaveUnidad";
            this.ClaveUnidad.Name = "ClaveUnidad";
            this.ClaveUnidad.ReadOnly = true;
            this.ClaveUnidad.Visible = false;
            // 
            // ClaveProductoServicio
            // 
            this.ClaveProductoServicio.DataPropertyName = "ClaveProductoServicio";
            this.ClaveProductoServicio.HeaderText = "ClaveProductoServicio";
            this.ClaveProductoServicio.Name = "ClaveProductoServicio";
            this.ClaveProductoServicio.ReadOnly = true;
            this.ClaveProductoServicio.Visible = false;
            // 
            // btnGenerar
            // 
            this.btnGenerar.Location = new System.Drawing.Point(1141, 698);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(119, 34);
            this.btnGenerar.TabIndex = 109;
            this.btnGenerar.Text = "Generar";
            this.btnGenerar.UseVisualStyleBackColor = true;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.AutoSize = true;
            this.lblSubtotal.Location = new System.Drawing.Point(1093, 623);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(64, 17);
            this.lblSubtotal.TabIndex = 113;
            this.lblSubtotal.Text = "Subtotal:";
            // 
            // lblIVA
            // 
            this.lblIVA.AutoSize = true;
            this.lblIVA.Location = new System.Drawing.Point(1093, 643);
            this.lblIVA.Name = "lblIVA";
            this.lblIVA.Size = new System.Drawing.Size(33, 17);
            this.lblIVA.TabIndex = 114;
            this.lblIVA.Text = "IVA:";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(1093, 663);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(44, 17);
            this.lblTotal.TabIndex = 115;
            this.lblTotal.Text = "Total:";
            // 
            // lblTotalImporte
            // 
            this.lblTotalImporte.AutoSize = true;
            this.lblTotalImporte.Location = new System.Drawing.Point(1165, 663);
            this.lblTotalImporte.Name = "lblTotalImporte";
            this.lblTotalImporte.Size = new System.Drawing.Size(36, 17);
            this.lblTotalImporte.TabIndex = 118;
            this.lblTotalImporte.Text = "0.00";
            this.lblTotalImporte.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblIVAImporte
            // 
            this.lblIVAImporte.AutoSize = true;
            this.lblIVAImporte.Location = new System.Drawing.Point(1165, 643);
            this.lblIVAImporte.Name = "lblIVAImporte";
            this.lblIVAImporte.Size = new System.Drawing.Size(36, 17);
            this.lblIVAImporte.TabIndex = 117;
            this.lblIVAImporte.Text = "0.00";
            this.lblIVAImporte.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblSubtotalImporte
            // 
            this.lblSubtotalImporte.AutoSize = true;
            this.lblSubtotalImporte.Location = new System.Drawing.Point(1165, 623);
            this.lblSubtotalImporte.Name = "lblSubtotalImporte";
            this.lblSubtotalImporte.Size = new System.Drawing.Size(36, 17);
            this.lblSubtotalImporte.TabIndex = 116;
            this.lblSubtotalImporte.Text = "0.00";
            this.lblSubtotalImporte.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // chkCancelacion
            // 
            this.chkCancelacion.AutoSize = true;
            this.chkCancelacion.Location = new System.Drawing.Point(12, 624);
            this.chkCancelacion.Name = "chkCancelacion";
            this.chkCancelacion.Size = new System.Drawing.Size(209, 21);
            this.chkCancelacion.TabIndex = 119;
            this.chkCancelacion.Text = "No aplica para refacturación";
            this.chkCancelacion.UseVisualStyleBackColor = true;
            this.chkCancelacion.CheckedChanged += new System.EventHandler(this.chkCancelacion_CheckedChanged);
            // 
            // frmNotaCreditoIngreso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1295, 754);
            this.Controls.Add(this.chkCancelacion);
            this.Controls.Add(this.lblTotalImporte);
            this.Controls.Add(this.lblIVAImporte);
            this.Controls.Add(this.lblSubtotalImporte);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblIVA);
            this.Controls.Add(this.lblSubtotal);
            this.Controls.Add(this.btnGenerar);
            this.Controls.Add(this.gbDetalle);
            this.Controls.Add(this.gbGenerales);
            this.Name = "frmNotaCreditoIngreso";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ingreso de Factura a Almacén";
            this.Load += new System.EventHandler(this.frmNotaCreditoDetalle_Load);
            this.gbGenerales.ResumeLayout(false);
            this.gbGenerales.PerformLayout();
            this.gbDetalle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbGenerales;
        private System.Windows.Forms.GroupBox gbDetalle;
        private System.Windows.Forms.DataGridView dgvDetalle;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.Label lblClienteNombre;
        private System.Windows.Forms.Label lblClienteRFC;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.Label lblIVA;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblTotalImporte;
        private System.Windows.Forms.Label lblIVAImporte;
        private System.Windows.Forms.Label lblSubtotalImporte;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Seleccion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn Clave;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnidadVenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precio;
        private System.Windows.Forms.DataGridViewTextBoxColumn IVATasa;
        private System.Windows.Forms.DataGridViewTextBoxColumn Subtotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClaveUnidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClaveProductoServicio;
        private System.Windows.Forms.Label lblFolio;
        private System.Windows.Forms.Label lblSerie;
        private System.Windows.Forms.Label lblAlmacen;
        private System.Windows.Forms.Label lblClienteClave;
        private System.Windows.Forms.CheckBox chkCancelacion;
    }
}