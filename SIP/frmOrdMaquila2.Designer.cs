namespace SIP
{
    partial class frmOrdMaquila2
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.lblNombreProveedor = new System.Windows.Forms.Label();
            this.txtEsqDeImp = new SIP.UserControls.NumericTextBox();
            this.txtClaveProveedor = new SIP.UserControls.TextBoxEx();
            this.txtCosto = new SIP.UserControls.NumericTextBox();
            this.txtObservaciones = new SIP.UserControls.TextBoxEx();
            this.label9 = new System.Windows.Forms.Label();
            this.lblReferencia = new System.Windows.Forms.Label();
            this.lblNoOrdenMaq = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvDetalle = new System.Windows.Forms.DataGridView();
            this.ColModelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColCantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColOrdProd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColReferencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label7 = new System.Windows.Forms.Label();
            this.lblTotalPrendas = new System.Windows.Forms.Label();
            this.btnGenerarOrden = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.lblNombreProveedor);
            this.groupBox1.Controls.Add(this.txtEsqDeImp);
            this.groupBox1.Controls.Add(this.txtClaveProveedor);
            this.groupBox1.Controls.Add(this.txtCosto);
            this.groupBox1.Controls.Add(this.txtObservaciones);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.lblReferencia);
            this.groupBox1.Controls.Add(this.lblNoOrdenMaq);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(414, 184);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(176, 40);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(24, 21);
            this.btnBuscar.TabIndex = 1;
            this.btnBuscar.Text = "?";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // lblNombreProveedor
            // 
            this.lblNombreProveedor.Location = new System.Drawing.Point(112, 64);
            this.lblNombreProveedor.Name = "lblNombreProveedor";
            this.lblNombreProveedor.Size = new System.Drawing.Size(287, 24);
            this.lblNombreProveedor.TabIndex = 13;
            // 
            // txtEsqDeImp
            // 
            this.txtEsqDeImp.Location = new System.Drawing.Point(311, 91);
            this.txtEsqDeImp.MaxValue = 0;
            this.txtEsqDeImp.MinValue = 0;
            this.txtEsqDeImp.Name = "txtEsqDeImp";
            this.txtEsqDeImp.Size = new System.Drawing.Size(88, 20);
            this.txtEsqDeImp.TabIndex = 3;
            this.txtEsqDeImp.Text = "9";
            this.txtEsqDeImp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtEsqDeImp.Leave += new System.EventHandler(this.txtEsqDeImp_Leave);
            // 
            // txtClaveProveedor
            // 
            this.txtClaveProveedor.Location = new System.Drawing.Point(112, 41);
            this.txtClaveProveedor.Name = "txtClaveProveedor";
            this.txtClaveProveedor.OnlyUpperCase = true;
            this.txtClaveProveedor.Size = new System.Drawing.Size(60, 20);
            this.txtClaveProveedor.TabIndex = 0;
            this.txtClaveProveedor.Text = "0";
            this.txtClaveProveedor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtClaveProveedor.Leave += new System.EventHandler(this.txtClaveProveedor_Leave);
            // 
            // txtCosto
            // 
            this.txtCosto.Location = new System.Drawing.Point(112, 91);
            this.txtCosto.MaxValue = 0;
            this.txtCosto.MinValue = 0;
            this.txtCosto.Name = "txtCosto";
            this.txtCosto.NumberType = SIP.UserControls.TipoDeNumero.Decimal;
            this.txtCosto.Size = new System.Drawing.Size(88, 20);
            this.txtCosto.TabIndex = 2;
            this.txtCosto.Text = "0";
            this.txtCosto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCosto.Leave += new System.EventHandler(this.txtCosto_Leave);
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.AutoTABOnKeyDown = false;
            this.txtObservaciones.AutoTABOnKeyUp = false;
            this.txtObservaciones.Location = new System.Drawing.Point(112, 117);
            this.txtObservaciones.MaxLength = 254;
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.OnlyUpperCase = true;
            this.txtObservaciones.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtObservaciones.Size = new System.Drawing.Size(287, 51);
            this.txtObservaciones.TabIndex = 4;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(222, 94);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Esq. de Imp.";
            // 
            // lblReferencia
            // 
            this.lblReferencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReferencia.Location = new System.Drawing.Point(311, 20);
            this.lblReferencia.Name = "lblReferencia";
            this.lblReferencia.Size = new System.Drawing.Size(88, 14);
            this.lblReferencia.TabIndex = 7;
            this.lblReferencia.Text = "0";
            this.lblReferencia.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNoOrdenMaq
            // 
            this.lblNoOrdenMaq.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoOrdenMaq.Location = new System.Drawing.Point(107, 20);
            this.lblNoOrdenMaq.Name = "lblNoOrdenMaq";
            this.lblNoOrdenMaq.Size = new System.Drawing.Size(91, 14);
            this.lblNoOrdenMaq.TabIndex = 6;
            this.lblNoOrdenMaq.Text = "0";
            this.lblNoOrdenMaq.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(222, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Referencia (OP)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Observaciones";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Costo";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Nombre";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Proveedor";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "No. Orden Maq.";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvDetalle);
            this.groupBox2.Location = new System.Drawing.Point(12, 202);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(414, 225);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detalle";
            // 
            // dgvDetalle
            // 
            this.dgvDetalle.AllowUserToAddRows = false;
            this.dgvDetalle.AllowUserToDeleteRows = false;
            this.dgvDetalle.AllowUserToResizeColumns = false;
            this.dgvDetalle.AllowUserToResizeRows = false;
            this.dgvDetalle.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalle.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColModelo,
            this.ColCantidad,
            this.ColOrdProd,
            this.ColReferencia});
            this.dgvDetalle.Location = new System.Drawing.Point(19, 19);
            this.dgvDetalle.Name = "dgvDetalle";
            this.dgvDetalle.RowHeadersVisible = false;
            this.dgvDetalle.Size = new System.Drawing.Size(380, 185);
            this.dgvDetalle.TabIndex = 0;
            this.dgvDetalle.TabStop = false;
            // 
            // ColModelo
            // 
            this.ColModelo.DataPropertyName = "Modelo";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ColModelo.DefaultCellStyle = dataGridViewCellStyle1;
            this.ColModelo.HeaderText = "Modelo";
            this.ColModelo.Name = "ColModelo";
            this.ColModelo.ReadOnly = true;
            this.ColModelo.Width = 122;
            // 
            // ColCantidad
            // 
            this.ColCantidad.DataPropertyName = "Cantidad";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ColCantidad.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColCantidad.HeaderText = "Cantidad";
            this.ColCantidad.Name = "ColCantidad";
            this.ColCantidad.ReadOnly = true;
            this.ColCantidad.Width = 78;
            // 
            // ColOrdProd
            // 
            this.ColOrdProd.DataPropertyName = "OrdProd";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ColOrdProd.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColOrdProd.HeaderText = "Ord. Prod.";
            this.ColOrdProd.Name = "ColOrdProd";
            this.ColOrdProd.ReadOnly = true;
            this.ColOrdProd.Width = 78;
            // 
            // ColReferencia
            // 
            this.ColReferencia.DataPropertyName = "Referencia";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ColReferencia.DefaultCellStyle = dataGridViewCellStyle4;
            this.ColReferencia.HeaderText = "Referencia";
            this.ColReferencia.Name = "ColReferencia";
            this.ColReferencia.ReadOnly = true;
            this.ColReferencia.Width = 78;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 438);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Total prendas";
            // 
            // lblTotalPrendas
            // 
            this.lblTotalPrendas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPrendas.Location = new System.Drawing.Point(149, 438);
            this.lblTotalPrendas.Name = "lblTotalPrendas";
            this.lblTotalPrendas.Size = new System.Drawing.Size(76, 14);
            this.lblTotalPrendas.TabIndex = 7;
            this.lblTotalPrendas.Text = "0";
            this.lblTotalPrendas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnGenerarOrden
            // 
            this.btnGenerarOrden.Location = new System.Drawing.Point(323, 450);
            this.btnGenerarOrden.Name = "btnGenerarOrden";
            this.btnGenerarOrden.Size = new System.Drawing.Size(103, 23);
            this.btnGenerarOrden.TabIndex = 2;
            this.btnGenerarOrden.Text = "Generar Orden";
            this.btnGenerarOrden.UseVisualStyleBackColor = true;
            this.btnGenerarOrden.Click += new System.EventHandler(this.btnGenerarOrden_Click);
            // 
            // frmOrdMaquila2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 486);
            this.Controls.Add(this.btnGenerarOrden);
            this.Controls.Add(this.lblTotalPrendas);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOrdMaquila2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generación de Orden de Maquila";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmOrdMaquila2_FormClosing);
            this.Load += new System.EventHandler(this.frmOrdMaquila2_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblReferencia;
        private System.Windows.Forms.Label lblNoOrdenMaq;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblNombreProveedor;
        private UserControls.NumericTextBox txtEsqDeImp;
        private UserControls.TextBoxEx txtClaveProveedor;
        private UserControls.NumericTextBox txtCosto;
        private UserControls.TextBoxEx txtObservaciones;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvDetalle;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColModelo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColCantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColOrdProd;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColReferencia;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblTotalPrendas;
        private System.Windows.Forms.Button btnGenerarOrden;
    }
}