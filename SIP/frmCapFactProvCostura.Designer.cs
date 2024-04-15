namespace SIP
{
    partial class frmCapFactProvCostura
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSubTotal = new SIP.UserControls.NumericTextBox();
            this.txtFactura = new SIP.UserControls.TextBoxEx();
            this.txtClave = new SIP.UserControls.TextBoxEx();
            this.lblNombreProveedor = new System.Windows.Forms.Label();
            this.btnMostrarBusqueda = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.scape1 = new SIP.UserControls.Scape();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblMsg = new System.Windows.Forms.Label();
            this.dgvFletes = new System.Windows.Forms.DataGridView();
            this.colPedido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Prendas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFacturas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCostoPrevio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnRemover = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.txtNumeroPedido = new SIP.UserControls.NumericTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblCostoPorPrenda = new System.Windows.Forms.Label();
            this.lblCostoTotalActual = new System.Windows.Forms.Label();
            this.lblCostoTotalPrevio = new System.Windows.Forms.Label();
            this.lblTotalPrendas = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFletes)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtSubTotal);
            this.groupBox1.Controls.Add(this.txtFactura);
            this.groupBox1.Controls.Add(this.txtClave);
            this.groupBox1.Controls.Add(this.lblNombreProveedor);
            this.groupBox1.Controls.Add(this.btnMostrarBusqueda);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(4, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(397, 133);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Proveedor";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(148, 106);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "* sin IVA";
            // 
            // txtSubTotal
            // 
            this.txtSubTotal.Location = new System.Drawing.Point(70, 105);
            this.txtSubTotal.MaxValue = 0;
            this.txtSubTotal.MinValue = 0;
            this.txtSubTotal.Name = "txtSubTotal";
            this.txtSubTotal.NumberType = SIP.UserControls.TipoDeNumero.Decimal;
            this.txtSubTotal.Size = new System.Drawing.Size(71, 20);
            this.txtSubTotal.TabIndex = 3;
            this.txtSubTotal.Leave += new System.EventHandler(this.txtSubTotal_Leave);
            // 
            // txtFactura
            // 
            this.txtFactura.Location = new System.Drawing.Point(70, 79);
            this.txtFactura.Name = "txtFactura";
            this.txtFactura.OnlyUpperCase = false;
            this.txtFactura.Size = new System.Drawing.Size(71, 20);
            this.txtFactura.TabIndex = 2;
            this.txtFactura.Leave += new System.EventHandler(this.txtFactura_Leave);
            // 
            // txtClave
            // 
            this.txtClave.Location = new System.Drawing.Point(70, 19);
            this.txtClave.Name = "txtClave";
            this.txtClave.OnlyUpperCase = false;
            this.txtClave.Size = new System.Drawing.Size(71, 20);
            this.txtClave.TabIndex = 0;
            this.txtClave.Text = "0";
            this.txtClave.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtClave.TextChanged += new System.EventHandler(this.txtClave_TextChanged);
            this.txtClave.Leave += new System.EventHandler(this.txtClave_Leave);
            // 
            // lblNombreProveedor
            // 
            this.lblNombreProveedor.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lblNombreProveedor.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblNombreProveedor.Location = new System.Drawing.Point(67, 44);
            this.lblNombreProveedor.Name = "lblNombreProveedor";
            this.lblNombreProveedor.Size = new System.Drawing.Size(298, 31);
            this.lblNombreProveedor.TabIndex = 6;
            this.lblNombreProveedor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnMostrarBusqueda
            // 
            this.btnMostrarBusqueda.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnMostrarBusqueda.Location = new System.Drawing.Point(147, 17);
            this.btnMostrarBusqueda.Name = "btnMostrarBusqueda";
            this.btnMostrarBusqueda.Size = new System.Drawing.Size(22, 21);
            this.btnMostrarBusqueda.TabIndex = 1;
            this.btnMostrarBusqueda.Text = "?";
            this.btnMostrarBusqueda.UseVisualStyleBackColor = true;
            this.btnMostrarBusqueda.Click += new System.EventHandler(this.btnMostrarBusqueda_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Subtotal";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Factura";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nombre";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Clave";
            // 
            // scape1
            // 
            this.scape1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.scape1.Form = this;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblMsg);
            this.groupBox2.Controls.Add(this.dgvFletes);
            this.groupBox2.Controls.Add(this.btnRemover);
            this.groupBox2.Controls.Add(this.btnAgregar);
            this.groupBox2.Controls.Add(this.txtNumeroPedido);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(4, 151);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(397, 262);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Documentos";
            // 
            // lblMsg
            // 
            this.lblMsg.Location = new System.Drawing.Point(235, 17);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(151, 36);
            this.lblMsg.TabIndex = 8;
            this.lblMsg.Text = "lblMsg";
            // 
            // dgvFletes
            // 
            this.dgvFletes.AllowUserToAddRows = false;
            this.dgvFletes.AllowUserToResizeColumns = false;
            this.dgvFletes.AllowUserToResizeRows = false;
            this.dgvFletes.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvFletes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFletes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPedido,
            this.Prendas,
            this.colFacturas,
            this.colCostoPrevio});
            this.dgvFletes.Location = new System.Drawing.Point(22, 57);
            this.dgvFletes.Name = "dgvFletes";
            this.dgvFletes.ReadOnly = true;
            this.dgvFletes.RowHeadersVisible = false;
            this.dgvFletes.RowTemplate.Height = 18;
            this.dgvFletes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFletes.Size = new System.Drawing.Size(364, 196);
            this.dgvFletes.TabIndex = 7;
            this.dgvFletes.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dgvFletes_RowsRemoved);
            // 
            // colPedido
            // 
            this.colPedido.DataPropertyName = "PEDIDO";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N0";
            dataGridViewCellStyle4.NullValue = null;
            this.colPedido.DefaultCellStyle = dataGridViewCellStyle4;
            this.colPedido.HeaderText = "Pedido";
            this.colPedido.Name = "colPedido";
            this.colPedido.ReadOnly = true;
            this.colPedido.Width = 65;
            // 
            // Prendas
            // 
            this.Prendas.DataPropertyName = "PRENDAS_COSTURA";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Prendas.DefaultCellStyle = dataGridViewCellStyle5;
            this.Prendas.HeaderText = "Prendas";
            this.Prendas.Name = "Prendas";
            this.Prendas.ReadOnly = true;
            this.Prendas.Width = 65;
            // 
            // colFacturas
            // 
            this.colFacturas.DataPropertyName = "FACTURAS";
            this.colFacturas.HeaderText = "Fact. Previa";
            this.colFacturas.Name = "colFacturas";
            this.colFacturas.ReadOnly = true;
            this.colFacturas.Width = 125;
            // 
            // colCostoPrevio
            // 
            this.colCostoPrevio.DataPropertyName = "COSTO_PREVIO";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colCostoPrevio.DefaultCellStyle = dataGridViewCellStyle6;
            this.colCostoPrevio.HeaderText = "Costo Previo";
            this.colCostoPrevio.Name = "colCostoPrevio";
            this.colCostoPrevio.ReadOnly = true;
            this.colCostoPrevio.Width = 90;
            // 
            // btnRemover
            // 
            this.btnRemover.Font = new System.Drawing.Font("Webdings", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemover.Image = global::SIP.Properties.Resources.upArrow;
            this.btnRemover.Location = new System.Drawing.Point(196, 26);
            this.btnRemover.Name = "btnRemover";
            this.btnRemover.Size = new System.Drawing.Size(33, 24);
            this.btnRemover.TabIndex = 6;
            this.btnRemover.UseVisualStyleBackColor = true;
            this.btnRemover.Click += new System.EventHandler(this.btnRemover_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Font = new System.Drawing.Font("Webdings", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.Image = global::SIP.Properties.Resources.downArrow;
            this.btnAgregar.Location = new System.Drawing.Point(162, 26);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(33, 24);
            this.btnAgregar.TabIndex = 5;
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // txtNumeroPedido
            // 
            this.txtNumeroPedido.Location = new System.Drawing.Point(85, 28);
            this.txtNumeroPedido.MaxValue = 0;
            this.txtNumeroPedido.MinValue = 0;
            this.txtNumeroPedido.Name = "txtNumeroPedido";
            this.txtNumeroPedido.Size = new System.Drawing.Size(71, 20);
            this.txtNumeroPedido.TabIndex = 4;
            this.txtNumeroPedido.TextChanged += new System.EventHandler(this.txtNumeroPedido_TextChanged);
            this.txtNumeroPedido.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNumeroPedido_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "No. Pedido";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 419);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Total de Prendas";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 441);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(94, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Costo Total Previo";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 464);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(94, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Costo Total Actual";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 486);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "Costo por Prenda";
            // 
            // lblCostoPorPrenda
            // 
            this.lblCostoPorPrenda.Location = new System.Drawing.Point(118, 486);
            this.lblCostoPorPrenda.Name = "lblCostoPorPrenda";
            this.lblCostoPorPrenda.Size = new System.Drawing.Size(66, 13);
            this.lblCostoPorPrenda.TabIndex = 9;
            this.lblCostoPorPrenda.Text = "0";
            this.lblCostoPorPrenda.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCostoTotalActual
            // 
            this.lblCostoTotalActual.Location = new System.Drawing.Point(118, 464);
            this.lblCostoTotalActual.Name = "lblCostoTotalActual";
            this.lblCostoTotalActual.Size = new System.Drawing.Size(66, 13);
            this.lblCostoTotalActual.TabIndex = 8;
            this.lblCostoTotalActual.Text = "0";
            this.lblCostoTotalActual.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCostoTotalPrevio
            // 
            this.lblCostoTotalPrevio.Location = new System.Drawing.Point(118, 441);
            this.lblCostoTotalPrevio.Name = "lblCostoTotalPrevio";
            this.lblCostoTotalPrevio.Size = new System.Drawing.Size(66, 13);
            this.lblCostoTotalPrevio.TabIndex = 7;
            this.lblCostoTotalPrevio.Text = "0";
            this.lblCostoTotalPrevio.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalPrendas
            // 
            this.lblTotalPrendas.Location = new System.Drawing.Point(118, 419);
            this.lblTotalPrendas.Name = "lblTotalPrendas";
            this.lblTotalPrendas.Size = new System.Drawing.Size(66, 13);
            this.lblTotalPrendas.TabIndex = 6;
            this.lblTotalPrendas.Text = "0";
            this.lblTotalPrendas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(237, 454);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 10;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(321, 454);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 11;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // frmCapFactProvCostura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.scape1;
            this.ClientSize = new System.Drawing.Size(408, 508);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.lblCostoPorPrenda);
            this.Controls.Add(this.lblCostoTotalActual);
            this.Controls.Add(this.lblCostoTotalPrevio);
            this.Controls.Add(this.lblTotalPrendas);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCapFactProvCostura";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Captura de facturas de proveedores de costura";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFletes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private UserControls.TextBoxEx txtClave;
        private System.Windows.Forms.Label lblNombreProveedor;
        private System.Windows.Forms.Button btnMostrarBusqueda;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private UserControls.NumericTextBox txtSubTotal;
        private UserControls.TextBoxEx txtFactura;
        private UserControls.Scape scape1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnAgregar;
        private UserControls.NumericTextBox txtNumeroPedido;
        private System.Windows.Forms.DataGridView dgvFletes;
        private System.Windows.Forms.Button btnRemover;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Label lblCostoPorPrenda;
        private System.Windows.Forms.Label lblCostoTotalActual;
        private System.Windows.Forms.Label lblCostoTotalPrevio;
        private System.Windows.Forms.Label lblTotalPrendas;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPedido;
        private System.Windows.Forms.DataGridViewTextBoxColumn Prendas;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFacturas;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCostoPrevio;
    }
}