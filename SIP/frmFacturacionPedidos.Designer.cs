namespace SIP
{
    partial class frmFacturacionPedidos
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gbCSD = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblNuevoCSD = new System.Windows.Forms.Label();
            this.lblEstatusValor = new System.Windows.Forms.Label();
            this.lblFechaVencimientoValor = new System.Windows.Forms.Label();
            this.lblNoCerValor = new System.Windows.Forms.Label();
            this.lblRFCValor = new System.Windows.Forms.Label();
            this.lblEstatus = new System.Windows.Forms.Label();
            this.lblFechaVencimiento = new System.Windows.Forms.Label();
            this.lblNoCer = new System.Windows.Forms.Label();
            this.lblRFC = new System.Windows.Forms.Label();
            this.btnValidar = new System.Windows.Forms.Button();
            this.btnCargarKey = new System.Windows.Forms.Button();
            this.btnCargaCer = new System.Windows.Forms.Button();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCer = new System.Windows.Forms.TextBox();
            this.lblCer = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.gbPedidos = new System.Windows.Forms.GroupBox();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.chlAgrupar = new System.Windows.Forms.CheckBox();
            this.btnFacturar = new System.Windows.Forms.Button();
            this.dgvPedidos = new System.Windows.Forms.DataGridView();
            this.Facturar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Pedido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClaveCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Prendas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Subtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtPedido = new System.Windows.Forms.TextBox();
            this.lblBuscaPedido = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbAlmacen = new System.Windows.Forms.ComboBox();
            this.lblAlmacen = new System.Windows.Forms.Label();
            this.lblSerie = new System.Windows.Forms.Label();
            this.cmbSerie = new System.Windows.Forms.ComboBox();
            this.gbCSD.SuspendLayout();
            this.gbPedidos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidos)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbCSD
            // 
            this.gbCSD.Controls.Add(this.label3);
            this.gbCSD.Controls.Add(this.lblNuevoCSD);
            this.gbCSD.Controls.Add(this.lblEstatusValor);
            this.gbCSD.Controls.Add(this.lblFechaVencimientoValor);
            this.gbCSD.Controls.Add(this.lblNoCerValor);
            this.gbCSD.Controls.Add(this.lblRFCValor);
            this.gbCSD.Controls.Add(this.lblEstatus);
            this.gbCSD.Controls.Add(this.lblFechaVencimiento);
            this.gbCSD.Controls.Add(this.lblNoCer);
            this.gbCSD.Controls.Add(this.lblRFC);
            this.gbCSD.Controls.Add(this.btnValidar);
            this.gbCSD.Controls.Add(this.btnCargarKey);
            this.gbCSD.Controls.Add(this.btnCargaCer);
            this.gbCSD.Controls.Add(this.txtPass);
            this.gbCSD.Controls.Add(this.label2);
            this.gbCSD.Controls.Add(this.txtKey);
            this.gbCSD.Controls.Add(this.label1);
            this.gbCSD.Controls.Add(this.txtCer);
            this.gbCSD.Controls.Add(this.lblCer);
            this.gbCSD.Location = new System.Drawing.Point(12, 38);
            this.gbCSD.Name = "gbCSD";
            this.gbCSD.Size = new System.Drawing.Size(414, 318);
            this.gbCSD.TabIndex = 0;
            this.gbCSD.TabStop = false;
            this.gbCSD.Text = "CSD";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(97, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(200, 17);
            this.label3.TabIndex = 18;
            this.label3.Text = "Datos de CSD configurado";
            // 
            // lblNuevoCSD
            // 
            this.lblNuevoCSD.AutoSize = true;
            this.lblNuevoCSD.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNuevoCSD.Location = new System.Drawing.Point(150, 178);
            this.lblNuevoCSD.Name = "lblNuevoCSD";
            this.lblNuevoCSD.Size = new System.Drawing.Size(90, 17);
            this.lblNuevoCSD.TabIndex = 17;
            this.lblNuevoCSD.Text = "Nuevo CSD";
            // 
            // lblEstatusValor
            // 
            this.lblEstatusValor.AutoSize = true;
            this.lblEstatusValor.Location = new System.Drawing.Point(211, 148);
            this.lblEstatusValor.Name = "lblEstatusValor";
            this.lblEstatusValor.Size = new System.Drawing.Size(98, 17);
            this.lblEstatusValor.TabIndex = 16;
            this.lblEstatusValor.Text = "Sin Configurar";
            // 
            // lblFechaVencimientoValor
            // 
            this.lblFechaVencimientoValor.AutoSize = true;
            this.lblFechaVencimientoValor.Location = new System.Drawing.Point(211, 117);
            this.lblFechaVencimientoValor.Name = "lblFechaVencimientoValor";
            this.lblFechaVencimientoValor.Size = new System.Drawing.Size(98, 17);
            this.lblFechaVencimientoValor.TabIndex = 15;
            this.lblFechaVencimientoValor.Text = "Sin Configurar";
            // 
            // lblNoCerValor
            // 
            this.lblNoCerValor.AutoSize = true;
            this.lblNoCerValor.Location = new System.Drawing.Point(211, 86);
            this.lblNoCerValor.Name = "lblNoCerValor";
            this.lblNoCerValor.Size = new System.Drawing.Size(98, 17);
            this.lblNoCerValor.TabIndex = 14;
            this.lblNoCerValor.Text = "Sin Configurar";
            // 
            // lblRFCValor
            // 
            this.lblRFCValor.AutoSize = true;
            this.lblRFCValor.Location = new System.Drawing.Point(211, 55);
            this.lblRFCValor.Name = "lblRFCValor";
            this.lblRFCValor.Size = new System.Drawing.Size(98, 17);
            this.lblRFCValor.TabIndex = 13;
            this.lblRFCValor.Text = "Sin Configurar";
            // 
            // lblEstatus
            // 
            this.lblEstatus.AutoSize = true;
            this.lblEstatus.Location = new System.Drawing.Point(32, 150);
            this.lblEstatus.Name = "lblEstatus";
            this.lblEstatus.Size = new System.Drawing.Size(59, 17);
            this.lblEstatus.TabIndex = 12;
            this.lblEstatus.Text = "Estatus:";
            // 
            // lblFechaVencimiento
            // 
            this.lblFechaVencimiento.AutoSize = true;
            this.lblFechaVencimiento.Location = new System.Drawing.Point(32, 119);
            this.lblFechaVencimiento.Name = "lblFechaVencimiento";
            this.lblFechaVencimiento.Size = new System.Drawing.Size(132, 17);
            this.lblFechaVencimiento.TabIndex = 11;
            this.lblFechaVencimiento.Text = "Fecha Vencimiento:";
            // 
            // lblNoCer
            // 
            this.lblNoCer.AutoSize = true;
            this.lblNoCer.Location = new System.Drawing.Point(32, 88);
            this.lblNoCer.Name = "lblNoCer";
            this.lblNoCer.Size = new System.Drawing.Size(105, 17);
            this.lblNoCer.TabIndex = 10;
            this.lblNoCer.Text = "No. Certificado:";
            // 
            // lblRFC
            // 
            this.lblRFC.AutoSize = true;
            this.lblRFC.Location = new System.Drawing.Point(32, 57);
            this.lblRFC.Name = "lblRFC";
            this.lblRFC.Size = new System.Drawing.Size(39, 17);
            this.lblRFC.TabIndex = 9;
            this.lblRFC.Text = "RFC:";
            // 
            // btnValidar
            // 
            this.btnValidar.Location = new System.Drawing.Point(285, 280);
            this.btnValidar.Name = "btnValidar";
            this.btnValidar.Size = new System.Drawing.Size(108, 23);
            this.btnValidar.TabIndex = 103;
            this.btnValidar.Text = "Guardar CSD";
            this.btnValidar.UseVisualStyleBackColor = true;
            this.btnValidar.Click += new System.EventHandler(this.btnValidar_Click);
            // 
            // btnCargarKey
            // 
            this.btnCargarKey.Location = new System.Drawing.Point(362, 249);
            this.btnCargarKey.Name = "btnCargarKey";
            this.btnCargarKey.Size = new System.Drawing.Size(31, 22);
            this.btnCargarKey.TabIndex = 101;
            this.btnCargarKey.Text = "...";
            this.btnCargarKey.UseVisualStyleBackColor = true;
            this.btnCargarKey.Click += new System.EventHandler(this.btnCargarKey_Click);
            // 
            // btnCargaCer
            // 
            this.btnCargaCer.Location = new System.Drawing.Point(362, 221);
            this.btnCargaCer.Name = "btnCargaCer";
            this.btnCargaCer.Size = new System.Drawing.Size(31, 22);
            this.btnCargaCer.TabIndex = 100;
            this.btnCargaCer.Text = "...";
            this.btnCargaCer.UseVisualStyleBackColor = true;
            this.btnCargaCer.Click += new System.EventHandler(this.btnCargaCer_Click);
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(199, 280);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(80, 22);
            this.txtPass.TabIndex = 102;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 286);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Password";
            // 
            // txtKey
            // 
            this.txtKey.Enabled = false;
            this.txtKey.Location = new System.Drawing.Point(199, 252);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(157, 22);
            this.txtKey.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 252);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "CSD Llave privada (.key)";
            // 
            // txtCer
            // 
            this.txtCer.Enabled = false;
            this.txtCer.Location = new System.Drawing.Point(199, 221);
            this.txtCer.Name = "txtCer";
            this.txtCer.Size = new System.Drawing.Size(157, 22);
            this.txtCer.TabIndex = 100;
            // 
            // lblCer
            // 
            this.lblCer.AutoSize = true;
            this.lblCer.Location = new System.Drawing.Point(32, 221);
            this.lblCer.Name = "lblCer";
            this.lblCer.Size = new System.Drawing.Size(161, 17);
            this.lblCer.TabIndex = 0;
            this.lblCer.Text = "CSD Llave pública (.cer)";
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(572, 10);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(205, 25);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "Facturación CFDI33";
            // 
            // gbPedidos
            // 
            this.gbPedidos.Controls.Add(this.btnActualizar);
            this.gbPedidos.Controls.Add(this.chlAgrupar);
            this.gbPedidos.Controls.Add(this.btnFacturar);
            this.gbPedidos.Controls.Add(this.dgvPedidos);
            this.gbPedidos.Controls.Add(this.txtPedido);
            this.gbPedidos.Controls.Add(this.lblBuscaPedido);
            this.gbPedidos.Location = new System.Drawing.Point(432, 38);
            this.gbPedidos.Name = "gbPedidos";
            this.gbPedidos.Size = new System.Drawing.Size(989, 687);
            this.gbPedidos.TabIndex = 2;
            this.gbPedidos.TabStop = false;
            this.gbPedidos.Text = "Pedidos";
            // 
            // btnActualizar
            // 
            this.btnActualizar.BackgroundImage = global::SIP.Properties.Resources.refresh;
            this.btnActualizar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnActualizar.Location = new System.Drawing.Point(951, 27);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(32, 35);
            this.btnActualizar.TabIndex = 11;
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // chlAgrupar
            // 
            this.chlAgrupar.AutoSize = true;
            this.chlAgrupar.Location = new System.Drawing.Point(6, 660);
            this.chlAgrupar.Name = "chlAgrupar";
            this.chlAgrupar.Size = new System.Drawing.Size(249, 21);
            this.chlAgrupar.TabIndex = 2;
            this.chlAgrupar.Text = "Agrupar pedidos en 1 sola Factura";
            this.chlAgrupar.UseVisualStyleBackColor = true;
            this.chlAgrupar.Visible = false;
            // 
            // btnFacturar
            // 
            this.btnFacturar.Location = new System.Drawing.Point(908, 658);
            this.btnFacturar.Name = "btnFacturar";
            this.btnFacturar.Size = new System.Drawing.Size(75, 23);
            this.btnFacturar.TabIndex = 3;
            this.btnFacturar.Text = "Facturar";
            this.btnFacturar.UseVisualStyleBackColor = true;
            this.btnFacturar.Click += new System.EventHandler(this.btnFacturar_Click);
            // 
            // dgvPedidos
            // 
            this.dgvPedidos.AllowUserToAddRows = false;
            this.dgvPedidos.AllowUserToDeleteRows = false;
            this.dgvPedidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPedidos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Facturar,
            this.Pedido,
            this.Fecha,
            this.ClaveCliente,
            this.Cliente,
            this.Prendas,
            this.Subtotal});
            this.dgvPedidos.Location = new System.Drawing.Point(6, 58);
            this.dgvPedidos.Name = "dgvPedidos";
            this.dgvPedidos.RowTemplate.Height = 24;
            this.dgvPedidos.Size = new System.Drawing.Size(977, 594);
            this.dgvPedidos.TabIndex = 10;
            // 
            // Facturar
            // 
            this.Facturar.HeaderText = "Facturar";
            this.Facturar.Name = "Facturar";
            this.Facturar.Width = 70;
            // 
            // Pedido
            // 
            this.Pedido.DataPropertyName = "PEDIDO";
            this.Pedido.HeaderText = "Pedido";
            this.Pedido.Name = "Pedido";
            this.Pedido.ReadOnly = true;
            this.Pedido.Width = 60;
            // 
            // Fecha
            // 
            this.Fecha.DataPropertyName = "FECHA";
            dataGridViewCellStyle3.Format = "d";
            dataGridViewCellStyle3.NullValue = null;
            this.Fecha.DefaultCellStyle = dataGridViewCellStyle3;
            this.Fecha.HeaderText = "Fecha";
            this.Fecha.Name = "Fecha";
            this.Fecha.ReadOnly = true;
            // 
            // ClaveCliente
            // 
            this.ClaveCliente.DataPropertyName = "CLAVECLIENTE";
            this.ClaveCliente.HeaderText = "Cve. Cliente";
            this.ClaveCliente.Name = "ClaveCliente";
            this.ClaveCliente.ReadOnly = true;
            this.ClaveCliente.Width = 70;
            // 
            // Cliente
            // 
            this.Cliente.DataPropertyName = "CLIENTE";
            this.Cliente.HeaderText = "Cliente";
            this.Cliente.Name = "Cliente";
            this.Cliente.ReadOnly = true;
            this.Cliente.Width = 250;
            // 
            // Prendas
            // 
            this.Prendas.DataPropertyName = "PRENDAS";
            this.Prendas.HeaderText = "Prendas";
            this.Prendas.Name = "Prendas";
            this.Prendas.ReadOnly = true;
            this.Prendas.Width = 70;
            // 
            // Subtotal
            // 
            this.Subtotal.DataPropertyName = "IMPORTE";
            this.Subtotal.HeaderText = "Subtotal";
            this.Subtotal.Name = "Subtotal";
            this.Subtotal.ReadOnly = true;
            this.Subtotal.Width = 70;
            // 
            // txtPedido
            // 
            this.txtPedido.Location = new System.Drawing.Point(142, 30);
            this.txtPedido.Name = "txtPedido";
            this.txtPedido.Size = new System.Drawing.Size(803, 22);
            this.txtPedido.TabIndex = 1;
            this.txtPedido.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPedido_KeyUp);
            // 
            // lblBuscaPedido
            // 
            this.lblBuscaPedido.AutoSize = true;
            this.lblBuscaPedido.Location = new System.Drawing.Point(32, 33);
            this.lblBuscaPedido.Name = "lblBuscaPedido";
            this.lblBuscaPedido.Size = new System.Drawing.Size(104, 17);
            this.lblBuscaPedido.TabIndex = 0;
            this.lblBuscaPedido.Text = "Buscar Pedido:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbAlmacen);
            this.groupBox1.Controls.Add(this.lblAlmacen);
            this.groupBox1.Controls.Add(this.lblSerie);
            this.groupBox1.Controls.Add(this.cmbSerie);
            this.groupBox1.Location = new System.Drawing.Point(12, 362);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(414, 123);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos facturación";
            // 
            // cmbAlmacen
            // 
            this.cmbAlmacen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAlmacen.FormattingEnabled = true;
            this.cmbAlmacen.Location = new System.Drawing.Point(199, 64);
            this.cmbAlmacen.Name = "cmbAlmacen";
            this.cmbAlmacen.Size = new System.Drawing.Size(194, 24);
            this.cmbAlmacen.TabIndex = 106;
            // 
            // lblAlmacen
            // 
            this.lblAlmacen.AutoSize = true;
            this.lblAlmacen.Location = new System.Drawing.Point(32, 67);
            this.lblAlmacen.Name = "lblAlmacen";
            this.lblAlmacen.Size = new System.Drawing.Size(62, 17);
            this.lblAlmacen.TabIndex = 105;
            this.lblAlmacen.Text = "Almacen";
            // 
            // lblSerie
            // 
            this.lblSerie.AutoSize = true;
            this.lblSerie.Location = new System.Drawing.Point(32, 37);
            this.lblSerie.Name = "lblSerie";
            this.lblSerie.Size = new System.Drawing.Size(41, 17);
            this.lblSerie.TabIndex = 104;
            this.lblSerie.Text = "Serie";
            // 
            // cmbSerie
            // 
            this.cmbSerie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSerie.FormattingEnabled = true;
            this.cmbSerie.Location = new System.Drawing.Point(199, 34);
            this.cmbSerie.Name = "cmbSerie";
            this.cmbSerie.Size = new System.Drawing.Size(194, 24);
            this.cmbSerie.TabIndex = 0;
            this.cmbSerie.SelectedIndexChanged += new System.EventHandler(this.cmbSerie_SelectedIndexChanged);
            // 
            // frmFacturacionPedidos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1433, 737);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbPedidos);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.gbCSD);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmFacturacionPedidos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Facturación de Pedidos";
            this.Load += new System.EventHandler(this.frmFacturacionPedidos_Load);
            this.gbCSD.ResumeLayout(false);
            this.gbCSD.PerformLayout();
            this.gbPedidos.ResumeLayout(false);
            this.gbPedidos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidos)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbCSD;
        private System.Windows.Forms.Button btnCargarKey;
        private System.Windows.Forms.Button btnCargaCer;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCer;
        private System.Windows.Forms.Label lblCer;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnValidar;
        private System.Windows.Forms.GroupBox gbPedidos;
        private System.Windows.Forms.DataGridView dgvPedidos;
        private System.Windows.Forms.TextBox txtPedido;
        private System.Windows.Forms.Label lblBuscaPedido;
        private System.Windows.Forms.CheckBox chlAgrupar;
        private System.Windows.Forms.Button btnFacturar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblNuevoCSD;
        private System.Windows.Forms.Label lblEstatusValor;
        private System.Windows.Forms.Label lblFechaVencimientoValor;
        private System.Windows.Forms.Label lblNoCerValor;
        private System.Windows.Forms.Label lblRFCValor;
        private System.Windows.Forms.Label lblEstatus;
        private System.Windows.Forms.Label lblFechaVencimiento;
        private System.Windows.Forms.Label lblNoCer;
        private System.Windows.Forms.Label lblRFC;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Facturar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pedido;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClaveCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn Prendas;
        private System.Windows.Forms.DataGridViewTextBoxColumn Subtotal;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblSerie;
        private System.Windows.Forms.ComboBox cmbSerie;
        private System.Windows.Forms.ComboBox cmbAlmacen;
        private System.Windows.Forms.Label lblAlmacen;
        private System.Windows.Forms.Button btnActualizar;

    }
}