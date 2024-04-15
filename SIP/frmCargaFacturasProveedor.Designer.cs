namespace SIP
{
    partial class frmCargaFacturasProveedor
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
            this.lblCarga = new System.Windows.Forms.Label();
            this.lblSelección = new System.Windows.Forms.Label();
            this.btnSelección = new System.Windows.Forms.Button();
            this.lblCDFI = new System.Windows.Forms.Label();
            this.dgvCDFI = new System.Windows.Forms.DataGridView();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnProceso = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.UUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tipoCFDI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fechaEmision = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fechaTimbrado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblCantidad = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCDFI)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCarga
            // 
            this.lblCarga.AutoSize = true;
            this.lblCarga.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCarga.Location = new System.Drawing.Point(239, 9);
            this.lblCarga.Name = "lblCarga";
            this.lblCarga.Size = new System.Drawing.Size(318, 24);
            this.lblCarga.TabIndex = 0;
            this.lblCarga.Text = "Carga de CDFI Proveedores PPD";
            // 
            // lblSelección
            // 
            this.lblSelección.AutoSize = true;
            this.lblSelección.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelección.Location = new System.Drawing.Point(12, 62);
            this.lblSelección.Name = "lblSelección";
            this.lblSelección.Size = new System.Drawing.Size(167, 15);
            this.lblSelección.TabIndex = 1;
            this.lblSelección.Text = "Seleccione los achivos_XML:";
            this.lblSelección.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSelección
            // 
            this.btnSelección.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelección.Location = new System.Drawing.Point(185, 59);
            this.btnSelección.Name = "btnSelección";
            this.btnSelección.Size = new System.Drawing.Size(31, 20);
            this.btnSelección.TabIndex = 3;
            this.btnSelección.Text = "...";
            this.btnSelección.UseVisualStyleBackColor = true;
            this.btnSelección.Click += new System.EventHandler(this.btnSelección_Click);
            // 
            // lblCDFI
            // 
            this.lblCDFI.AutoSize = true;
            this.lblCDFI.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCDFI.Location = new System.Drawing.Point(26, 97);
            this.lblCDFI.Name = "lblCDFI";
            this.lblCDFI.Size = new System.Drawing.Size(97, 13);
            this.lblCDFI.TabIndex = 4;
            this.lblCDFI.Text = "Detalle de CDFI";
            // 
            // dgvCDFI
            // 
            this.dgvCDFI.AllowUserToAddRows = false;
            this.dgvCDFI.AllowUserToDeleteRows = false;
            this.dgvCDFI.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCDFI.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UUID,
            this.tipoCFDI,
            this.fechaEmision,
            this.fechaTimbrado,
            this.total});
            this.dgvCDFI.Location = new System.Drawing.Point(12, 113);
            this.dgvCDFI.Name = "dgvCDFI";
            this.dgvCDFI.ReadOnly = true;
            this.dgvCDFI.Size = new System.Drawing.Size(727, 349);
            this.dgvCDFI.TabIndex = 5;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(557, 468);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(90, 25);
            this.btnLimpiar.TabIndex = 6;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            // 
            // btnProceso
            // 
            this.btnProceso.Location = new System.Drawing.Point(653, 468);
            this.btnProceso.Name = "btnProceso";
            this.btnProceso.Size = new System.Drawing.Size(86, 25);
            this.btnProceso.TabIndex = 7;
            this.btnProceso.Text = "Procesar";
            this.btnProceso.UseVisualStyleBackColor = true;
            this.btnProceso.Click += new System.EventHandler(this.btnProceso_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Archivos XML (.xml)|*.xml";
            this.openFileDialog.Multiselect = true;
            // 
            // UUID
            // 
            this.UUID.DataPropertyName = "UUID";
            this.UUID.HeaderText = "UUID";
            this.UUID.Name = "UUID";
            this.UUID.ReadOnly = true;
            this.UUID.Width = 160;
            // 
            // tipoCFDI
            // 
            this.tipoCFDI.DataPropertyName = "TipoCFDI";
            this.tipoCFDI.HeaderText = "Tipo";
            this.tipoCFDI.Name = "tipoCFDI";
            this.tipoCFDI.ReadOnly = true;
            // 
            // fechaEmision
            // 
            this.fechaEmision.DataPropertyName = "FechaEmision";
            this.fechaEmision.HeaderText = "F. Emisión";
            this.fechaEmision.Name = "fechaEmision";
            this.fechaEmision.ReadOnly = true;
            this.fechaEmision.Width = 120;
            // 
            // fechaTimbrado
            // 
            this.fechaTimbrado.DataPropertyName = "FechaTimbrado";
            this.fechaTimbrado.HeaderText = "F. Timbrado";
            this.fechaTimbrado.Name = "fechaTimbrado";
            this.fechaTimbrado.ReadOnly = true;
            this.fechaTimbrado.Width = 120;
            // 
            // total
            // 
            this.total.DataPropertyName = "TotalPorTipoCFDI";
            this.total.HeaderText = "Total";
            this.total.Name = "total";
            this.total.ReadOnly = true;
            this.total.Width = 160;
            // 
            // lblCantidad
            // 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantidad.Location = new System.Drawing.Point(12, 474);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(140, 13);
            this.lblCantidad.TabIndex = 8;
            this.lblCantidad.Text = "Total de Documentos: 0";
            // 
            // frmCargaFacturasProveedor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 502);
            this.Controls.Add(this.lblCantidad);
            this.Controls.Add(this.btnProceso);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.dgvCDFI);
            this.Controls.Add(this.lblCDFI);
            this.Controls.Add(this.btnSelección);
            this.Controls.Add(this.lblSelección);
            this.Controls.Add(this.lblCarga);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCargaFacturasProveedor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Factura Proveedores PPD";
            this.Load += new System.EventHandler(this.frmFacProv_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCDFI)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCarga;
        private System.Windows.Forms.Label lblSelección;
        private System.Windows.Forms.Button btnSelección;
        private System.Windows.Forms.Label lblCDFI;
        private System.Windows.Forms.DataGridView dgvCDFI;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnProceso;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.DataGridViewTextBoxColumn UUID;
        private System.Windows.Forms.DataGridViewTextBoxColumn tipoCFDI;
        private System.Windows.Forms.DataGridViewTextBoxColumn fechaEmision;
        private System.Windows.Forms.DataGridViewTextBoxColumn fechaTimbrado;
        private System.Windows.Forms.DataGridViewTextBoxColumn total;
        private System.Windows.Forms.Label lblCantidad;
    }
}