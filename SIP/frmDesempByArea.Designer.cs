namespace SIP
{
    partial class frmDesempByArea
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
            this.components = new System.ComponentModel.Container();
            this.scape1 = new SIP.UserControls.Scape();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCliente = new System.Windows.Forms.Label();
            this.lblObservaciones = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNumPedido = new SIP.UserControls.NumericTextBox();
            this.txtStdEspecial = new SIP.UserControls.NumericTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtVentasObs = new System.Windows.Forms.TextBox();
            this.optVentasNoCumplio = new System.Windows.Forms.RadioButton();
            this.optVentasCumplio = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtAlmacenObs = new System.Windows.Forms.TextBox();
            this.optAlmacenNoCumplio = new System.Windows.Forms.RadioButton();
            this.optAlmacenCumplio = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtSistemasObs = new System.Windows.Forms.TextBox();
            this.optSistemasNoCumplio = new System.Windows.Forms.RadioButton();
            this.optSistemasCumplio = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtComprasObs = new System.Windows.Forms.TextBox();
            this.optComprasNoCumplio = new System.Windows.Forms.RadioButton();
            this.optComprasCumplio = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtCreditoObs = new System.Windows.Forms.TextBox();
            this.optCreditoNoCumplio = new System.Windows.Forms.RadioButton();
            this.optCreditoCumplio = new System.Windows.Forms.RadioButton();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtOpObs = new System.Windows.Forms.TextBox();
            this.optOpNoCumplio = new System.Windows.Forms.RadioButton();
            this.optOpCumplio = new System.Windows.Forms.RadioButton();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.txtClienteObs = new System.Windows.Forms.TextBox();
            this.optClienteNoCumplio = new System.Windows.Forms.RadioButton();
            this.optClienteCumplio = new System.Windows.Forms.RadioButton();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.lblProcesos = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // scape1
            // 
            this.scape1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.scape1.Form = this;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pedido :";
            // 
            // lblCliente
            // 
            this.lblCliente.Location = new System.Drawing.Point(153, 8);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(294, 23);
            this.lblCliente.TabIndex = 2;
            this.lblCliente.Text = "Cliente...";
            // 
            // lblObservaciones
            // 
            this.lblObservaciones.Location = new System.Drawing.Point(7, 35);
            this.lblObservaciones.Name = "lblObservaciones";
            this.lblObservaciones.Size = new System.Drawing.Size(441, 38);
            this.lblObservaciones.TabIndex = 3;
            this.lblObservaciones.Text = "Observaciones";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(411, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Estandard Especial";
            // 
            // txtNumPedido
            // 
            this.txtNumPedido.Location = new System.Drawing.Point(59, 8);
            this.txtNumPedido.MaxValue = 0;
            this.txtNumPedido.MinValue = 0;
            this.txtNumPedido.Name = "txtNumPedido";
            this.txtNumPedido.Size = new System.Drawing.Size(88, 20);
            this.txtNumPedido.TabIndex = 0;
            this.txtNumPedido.Leave += new System.EventHandler(this.txtNumPedido_Leave);
            // 
            // txtStdEspecial
            // 
            this.txtStdEspecial.Location = new System.Drawing.Point(515, 70);
            this.txtStdEspecial.MaxValue = 0;
            this.txtStdEspecial.MinValue = 0;
            this.txtStdEspecial.Name = "txtStdEspecial";
            this.txtStdEspecial.Size = new System.Drawing.Size(71, 20);
            this.txtStdEspecial.TabIndex = 1;
            this.txtStdEspecial.Text = "0";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtVentasObs);
            this.groupBox1.Controls.Add(this.optVentasNoCumplio);
            this.groupBox1.Controls.Add(this.optVentasCumplio);
            this.groupBox1.Location = new System.Drawing.Point(3, 91);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 73);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Tag = "optVentasNoCumplio";
            this.groupBox1.Text = "Ventas";
            // 
            // txtVentasObs
            // 
            this.txtVentasObs.Location = new System.Drawing.Point(108, 14);
            this.txtVentasObs.MaxLength = 255;
            this.txtVentasObs.Multiline = true;
            this.txtVentasObs.Name = "txtVentasObs";
            this.txtVentasObs.Size = new System.Drawing.Size(171, 50);
            this.txtVentasObs.TabIndex = 2;
            this.txtVentasObs.Leave += new System.EventHandler(this.GenericTxt_Leave);
            // 
            // optVentasNoCumplio
            // 
            this.optVentasNoCumplio.AutoSize = true;
            this.optVentasNoCumplio.Location = new System.Drawing.Point(7, 43);
            this.optVentasNoCumplio.Name = "optVentasNoCumplio";
            this.optVentasNoCumplio.Size = new System.Drawing.Size(78, 17);
            this.optVentasNoCumplio.TabIndex = 1;
            this.optVentasNoCumplio.TabStop = true;
            this.optVentasNoCumplio.Tag = "txtVentasObs";
            this.optVentasNoCumplio.Text = "No cumplió";
            this.optVentasNoCumplio.UseVisualStyleBackColor = true;
            this.optVentasNoCumplio.CheckedChanged += new System.EventHandler(this.Generic_CheckedChanged);
            this.optVentasNoCumplio.Leave += new System.EventHandler(this.GenericRadioLeave);
            // 
            // optVentasCumplio
            // 
            this.optVentasCumplio.AutoSize = true;
            this.optVentasCumplio.Location = new System.Drawing.Point(7, 20);
            this.optVentasCumplio.Name = "optVentasCumplio";
            this.optVentasCumplio.Size = new System.Drawing.Size(62, 17);
            this.optVentasCumplio.TabIndex = 0;
            this.optVentasCumplio.TabStop = true;
            this.optVentasCumplio.Tag = "txtVentasObs";
            this.optVentasCumplio.Text = "Cumplió";
            this.optVentasCumplio.UseVisualStyleBackColor = true;
            this.optVentasCumplio.CheckedChanged += new System.EventHandler(this.Generic_Si_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtAlmacenObs);
            this.groupBox2.Controls.Add(this.optAlmacenNoCumplio);
            this.groupBox2.Controls.Add(this.optAlmacenCumplio);
            this.groupBox2.Location = new System.Drawing.Point(299, 91);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(289, 73);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Tag = "optAlmacenNoCumplio";
            this.groupBox2.Text = "Almacen";
            // 
            // txtAlmacenObs
            // 
            this.txtAlmacenObs.Location = new System.Drawing.Point(108, 14);
            this.txtAlmacenObs.MaxLength = 255;
            this.txtAlmacenObs.Multiline = true;
            this.txtAlmacenObs.Name = "txtAlmacenObs";
            this.txtAlmacenObs.Size = new System.Drawing.Size(171, 50);
            this.txtAlmacenObs.TabIndex = 2;
            this.txtAlmacenObs.Leave += new System.EventHandler(this.GenericTxt_Leave);
            // 
            // optAlmacenNoCumplio
            // 
            this.optAlmacenNoCumplio.AutoSize = true;
            this.optAlmacenNoCumplio.Location = new System.Drawing.Point(7, 43);
            this.optAlmacenNoCumplio.Name = "optAlmacenNoCumplio";
            this.optAlmacenNoCumplio.Size = new System.Drawing.Size(78, 17);
            this.optAlmacenNoCumplio.TabIndex = 1;
            this.optAlmacenNoCumplio.TabStop = true;
            this.optAlmacenNoCumplio.Tag = "txtAlmacenObs";
            this.optAlmacenNoCumplio.Text = "No cumplió";
            this.optAlmacenNoCumplio.UseVisualStyleBackColor = true;
            this.optAlmacenNoCumplio.CheckedChanged += new System.EventHandler(this.Generic_CheckedChanged);
            this.optAlmacenNoCumplio.Leave += new System.EventHandler(this.GenericRadioLeave);
            // 
            // optAlmacenCumplio
            // 
            this.optAlmacenCumplio.AutoSize = true;
            this.optAlmacenCumplio.Location = new System.Drawing.Point(7, 20);
            this.optAlmacenCumplio.Name = "optAlmacenCumplio";
            this.optAlmacenCumplio.Size = new System.Drawing.Size(62, 17);
            this.optAlmacenCumplio.TabIndex = 0;
            this.optAlmacenCumplio.TabStop = true;
            this.optAlmacenCumplio.Tag = "txtAlmacenObs";
            this.optAlmacenCumplio.Text = "Cumplió";
            this.optAlmacenCumplio.UseVisualStyleBackColor = true;
            this.optAlmacenCumplio.CheckedChanged += new System.EventHandler(this.Generic_Si_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtSistemasObs);
            this.groupBox3.Controls.Add(this.optSistemasNoCumplio);
            this.groupBox3.Controls.Add(this.optSistemasCumplio);
            this.groupBox3.Location = new System.Drawing.Point(299, 170);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(289, 73);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Tag = "optSistemasNoCumplio";
            this.groupBox3.Text = "Sistemas";
            // 
            // txtSistemasObs
            // 
            this.txtSistemasObs.Location = new System.Drawing.Point(108, 14);
            this.txtSistemasObs.MaxLength = 255;
            this.txtSistemasObs.Multiline = true;
            this.txtSistemasObs.Name = "txtSistemasObs";
            this.txtSistemasObs.Size = new System.Drawing.Size(171, 50);
            this.txtSistemasObs.TabIndex = 2;
            this.txtSistemasObs.Leave += new System.EventHandler(this.GenericTxt_Leave);
            // 
            // optSistemasNoCumplio
            // 
            this.optSistemasNoCumplio.AutoSize = true;
            this.optSistemasNoCumplio.Location = new System.Drawing.Point(7, 43);
            this.optSistemasNoCumplio.Name = "optSistemasNoCumplio";
            this.optSistemasNoCumplio.Size = new System.Drawing.Size(78, 17);
            this.optSistemasNoCumplio.TabIndex = 1;
            this.optSistemasNoCumplio.TabStop = true;
            this.optSistemasNoCumplio.Tag = "txtSistemasObs";
            this.optSistemasNoCumplio.Text = "No cumplió";
            this.optSistemasNoCumplio.UseVisualStyleBackColor = true;
            this.optSistemasNoCumplio.CheckedChanged += new System.EventHandler(this.Generic_CheckedChanged);
            this.optSistemasNoCumplio.Leave += new System.EventHandler(this.GenericRadioLeave);
            // 
            // optSistemasCumplio
            // 
            this.optSistemasCumplio.AutoSize = true;
            this.optSistemasCumplio.Location = new System.Drawing.Point(7, 20);
            this.optSistemasCumplio.Name = "optSistemasCumplio";
            this.optSistemasCumplio.Size = new System.Drawing.Size(62, 17);
            this.optSistemasCumplio.TabIndex = 0;
            this.optSistemasCumplio.TabStop = true;
            this.optSistemasCumplio.Tag = "txtSistemasObs";
            this.optSistemasCumplio.Text = "Cumplió";
            this.optSistemasCumplio.UseVisualStyleBackColor = true;
            this.optSistemasCumplio.CheckedChanged += new System.EventHandler(this.Generic_Si_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtComprasObs);
            this.groupBox4.Controls.Add(this.optComprasNoCumplio);
            this.groupBox4.Controls.Add(this.optComprasCumplio);
            this.groupBox4.Location = new System.Drawing.Point(3, 170);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(289, 73);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Tag = "optComprasNoCumplio";
            this.groupBox4.Text = "Compras";
            // 
            // txtComprasObs
            // 
            this.txtComprasObs.Location = new System.Drawing.Point(108, 14);
            this.txtComprasObs.MaxLength = 255;
            this.txtComprasObs.Multiline = true;
            this.txtComprasObs.Name = "txtComprasObs";
            this.txtComprasObs.Size = new System.Drawing.Size(171, 50);
            this.txtComprasObs.TabIndex = 2;
            this.txtComprasObs.Leave += new System.EventHandler(this.GenericTxt_Leave);
            // 
            // optComprasNoCumplio
            // 
            this.optComprasNoCumplio.AutoSize = true;
            this.optComprasNoCumplio.Location = new System.Drawing.Point(7, 43);
            this.optComprasNoCumplio.Name = "optComprasNoCumplio";
            this.optComprasNoCumplio.Size = new System.Drawing.Size(78, 17);
            this.optComprasNoCumplio.TabIndex = 1;
            this.optComprasNoCumplio.TabStop = true;
            this.optComprasNoCumplio.Tag = "txtComprasObs";
            this.optComprasNoCumplio.Text = "No cumplió";
            this.optComprasNoCumplio.UseVisualStyleBackColor = true;
            this.optComprasNoCumplio.CheckedChanged += new System.EventHandler(this.Generic_CheckedChanged);
            this.optComprasNoCumplio.Leave += new System.EventHandler(this.GenericRadioLeave);
            // 
            // optComprasCumplio
            // 
            this.optComprasCumplio.AutoSize = true;
            this.optComprasCumplio.Location = new System.Drawing.Point(7, 20);
            this.optComprasCumplio.Name = "optComprasCumplio";
            this.optComprasCumplio.Size = new System.Drawing.Size(62, 17);
            this.optComprasCumplio.TabIndex = 0;
            this.optComprasCumplio.TabStop = true;
            this.optComprasCumplio.Tag = "txtComprasObs";
            this.optComprasCumplio.Text = "Cumplió";
            this.optComprasCumplio.UseVisualStyleBackColor = true;
            this.optComprasCumplio.CheckedChanged += new System.EventHandler(this.Generic_Si_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtCreditoObs);
            this.groupBox5.Controls.Add(this.optCreditoNoCumplio);
            this.groupBox5.Controls.Add(this.optCreditoCumplio);
            this.groupBox5.Location = new System.Drawing.Point(299, 249);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(289, 73);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            this.groupBox5.Tag = "optCreditoNoCumplio";
            this.groupBox5.Text = "Crédito";
            // 
            // txtCreditoObs
            // 
            this.txtCreditoObs.Location = new System.Drawing.Point(108, 14);
            this.txtCreditoObs.MaxLength = 255;
            this.txtCreditoObs.Multiline = true;
            this.txtCreditoObs.Name = "txtCreditoObs";
            this.txtCreditoObs.Size = new System.Drawing.Size(171, 50);
            this.txtCreditoObs.TabIndex = 2;
            this.txtCreditoObs.Leave += new System.EventHandler(this.GenericTxt_Leave);
            // 
            // optCreditoNoCumplio
            // 
            this.optCreditoNoCumplio.AutoSize = true;
            this.optCreditoNoCumplio.Location = new System.Drawing.Point(7, 43);
            this.optCreditoNoCumplio.Name = "optCreditoNoCumplio";
            this.optCreditoNoCumplio.Size = new System.Drawing.Size(78, 17);
            this.optCreditoNoCumplio.TabIndex = 1;
            this.optCreditoNoCumplio.TabStop = true;
            this.optCreditoNoCumplio.Tag = "txtCreditoObs";
            this.optCreditoNoCumplio.Text = "No cumplió";
            this.optCreditoNoCumplio.UseVisualStyleBackColor = true;
            this.optCreditoNoCumplio.CheckedChanged += new System.EventHandler(this.Generic_CheckedChanged);
            this.optCreditoNoCumplio.Leave += new System.EventHandler(this.GenericRadioLeave);
            // 
            // optCreditoCumplio
            // 
            this.optCreditoCumplio.AutoSize = true;
            this.optCreditoCumplio.Location = new System.Drawing.Point(7, 20);
            this.optCreditoCumplio.Name = "optCreditoCumplio";
            this.optCreditoCumplio.Size = new System.Drawing.Size(62, 17);
            this.optCreditoCumplio.TabIndex = 0;
            this.optCreditoCumplio.TabStop = true;
            this.optCreditoCumplio.Tag = "txtCreditoObs";
            this.optCreditoCumplio.Text = "Cumplió";
            this.optCreditoCumplio.UseVisualStyleBackColor = true;
            this.optCreditoCumplio.CheckedChanged += new System.EventHandler(this.Generic_Si_CheckedChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txtOpObs);
            this.groupBox6.Controls.Add(this.optOpNoCumplio);
            this.groupBox6.Controls.Add(this.optOpCumplio);
            this.groupBox6.Location = new System.Drawing.Point(3, 249);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(289, 73);
            this.groupBox6.TabIndex = 6;
            this.groupBox6.TabStop = false;
            this.groupBox6.Tag = "optOpNoCumplio";
            this.groupBox6.Text = "Operaciones";
            // 
            // txtOpObs
            // 
            this.txtOpObs.Location = new System.Drawing.Point(108, 14);
            this.txtOpObs.MaxLength = 255;
            this.txtOpObs.Multiline = true;
            this.txtOpObs.Name = "txtOpObs";
            this.txtOpObs.Size = new System.Drawing.Size(171, 50);
            this.txtOpObs.TabIndex = 2;
            this.txtOpObs.Leave += new System.EventHandler(this.GenericTxt_Leave);
            // 
            // optOpNoCumplio
            // 
            this.optOpNoCumplio.AutoSize = true;
            this.optOpNoCumplio.Location = new System.Drawing.Point(7, 43);
            this.optOpNoCumplio.Name = "optOpNoCumplio";
            this.optOpNoCumplio.Size = new System.Drawing.Size(78, 17);
            this.optOpNoCumplio.TabIndex = 1;
            this.optOpNoCumplio.TabStop = true;
            this.optOpNoCumplio.Tag = "txtOpObs";
            this.optOpNoCumplio.Text = "No cumplió";
            this.optOpNoCumplio.UseVisualStyleBackColor = true;
            this.optOpNoCumplio.CheckedChanged += new System.EventHandler(this.Generic_CheckedChanged);
            this.optOpNoCumplio.Leave += new System.EventHandler(this.GenericRadioLeave);
            // 
            // optOpCumplio
            // 
            this.optOpCumplio.AutoSize = true;
            this.optOpCumplio.Location = new System.Drawing.Point(7, 20);
            this.optOpCumplio.Name = "optOpCumplio";
            this.optOpCumplio.Size = new System.Drawing.Size(62, 17);
            this.optOpCumplio.TabIndex = 0;
            this.optOpCumplio.TabStop = true;
            this.optOpCumplio.Tag = "txtOpObs";
            this.optOpCumplio.Text = "Cumplió";
            this.optOpCumplio.UseVisualStyleBackColor = true;
            this.optOpCumplio.CheckedChanged += new System.EventHandler(this.Generic_Si_CheckedChanged);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.txtClienteObs);
            this.groupBox7.Controls.Add(this.optClienteNoCumplio);
            this.groupBox7.Controls.Add(this.optClienteCumplio);
            this.groupBox7.Location = new System.Drawing.Point(159, 322);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(289, 73);
            this.groupBox7.TabIndex = 8;
            this.groupBox7.TabStop = false;
            this.groupBox7.Tag = "optClienteNoCumplio";
            this.groupBox7.Text = "Cliente";
            // 
            // txtClienteObs
            // 
            this.txtClienteObs.Location = new System.Drawing.Point(108, 14);
            this.txtClienteObs.MaxLength = 255;
            this.txtClienteObs.Multiline = true;
            this.txtClienteObs.Name = "txtClienteObs";
            this.txtClienteObs.Size = new System.Drawing.Size(171, 50);
            this.txtClienteObs.TabIndex = 2;
            this.txtClienteObs.Leave += new System.EventHandler(this.GenericTxt_Leave);
            // 
            // optClienteNoCumplio
            // 
            this.optClienteNoCumplio.AutoSize = true;
            this.optClienteNoCumplio.Location = new System.Drawing.Point(7, 43);
            this.optClienteNoCumplio.Name = "optClienteNoCumplio";
            this.optClienteNoCumplio.Size = new System.Drawing.Size(78, 17);
            this.optClienteNoCumplio.TabIndex = 1;
            this.optClienteNoCumplio.TabStop = true;
            this.optClienteNoCumplio.Tag = "txtClienteObs";
            this.optClienteNoCumplio.Text = "No cumplió";
            this.optClienteNoCumplio.UseVisualStyleBackColor = true;
            this.optClienteNoCumplio.CheckedChanged += new System.EventHandler(this.Generic_CheckedChanged);
            this.optClienteNoCumplio.Leave += new System.EventHandler(this.GenericRadioLeave);
            // 
            // optClienteCumplio
            // 
            this.optClienteCumplio.AutoSize = true;
            this.optClienteCumplio.Location = new System.Drawing.Point(7, 20);
            this.optClienteCumplio.Name = "optClienteCumplio";
            this.optClienteCumplio.Size = new System.Drawing.Size(62, 17);
            this.optClienteCumplio.TabIndex = 0;
            this.optClienteCumplio.TabStop = true;
            this.optClienteCumplio.Tag = "txtClienteObs";
            this.optClienteCumplio.Text = "Cumplió";
            this.optClienteCumplio.UseVisualStyleBackColor = true;
            this.optClienteCumplio.CheckedChanged += new System.EventHandler(this.Generic_Si_CheckedChanged);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(511, 365);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 9;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // lblProcesos
            // 
            this.lblProcesos.Location = new System.Drawing.Point(481, 11);
            this.lblProcesos.Name = "lblProcesos";
            this.lblProcesos.Size = new System.Drawing.Size(107, 51);
            this.lblProcesos.TabIndex = 10;
            this.lblProcesos.Text = "Procesos...";
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            // 
            // frmDesempByArea
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.scape1;
            this.ClientSize = new System.Drawing.Size(598, 399);
            this.Controls.Add(this.lblProcesos);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtStdEspecial);
            this.Controls.Add(this.txtNumPedido);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblObservaciones);
            this.Controls.Add(this.lblCliente);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDesempByArea";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Desempeño de Área por Pedido";
            this.Load += new System.EventHandler(this.frmDesempByArea_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UserControls.Scape scape1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Label lblObservaciones;
        private System.Windows.Forms.Label label4;
        private UserControls.NumericTextBox txtNumPedido;
        private UserControls.NumericTextBox txtStdEspecial;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtVentasObs;
        private System.Windows.Forms.RadioButton optVentasNoCumplio;
        private System.Windows.Forms.RadioButton optVentasCumplio;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtAlmacenObs;
        private System.Windows.Forms.RadioButton optAlmacenNoCumplio;
        private System.Windows.Forms.RadioButton optAlmacenCumplio;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtSistemasObs;
        private System.Windows.Forms.RadioButton optSistemasNoCumplio;
        private System.Windows.Forms.RadioButton optSistemasCumplio;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtComprasObs;
        private System.Windows.Forms.RadioButton optComprasNoCumplio;
        private System.Windows.Forms.RadioButton optComprasCumplio;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtCreditoObs;
        private System.Windows.Forms.RadioButton optCreditoNoCumplio;
        private System.Windows.Forms.RadioButton optCreditoCumplio;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox txtOpObs;
        private System.Windows.Forms.RadioButton optOpNoCumplio;
        private System.Windows.Forms.RadioButton optOpCumplio;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox txtClienteObs;
        private System.Windows.Forms.RadioButton optClienteNoCumplio;
        private System.Windows.Forms.RadioButton optClienteCumplio;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label lblProcesos;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}