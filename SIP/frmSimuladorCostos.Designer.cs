namespace SIP
{
    partial class frmSimuladorCostos
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gbDatosProducto = new System.Windows.Forms.GroupBox();
            this.lblFecha = new System.Windows.Forms.Label();
            this.panelTallas = new System.Windows.Forms.Panel();
            this.lnkPrev = new System.Windows.Forms.LinkLabel();
            this.lnkSig = new System.Windows.Forms.LinkLabel();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.txtModelo = new SIP.UserControls.TextBoxEx();
            this.label1 = new System.Windows.Forms.Label();
            this.gbComponentesCostos = new System.Windows.Forms.GroupBox();
            this.dgViewComponentes = new System.Windows.Forms.DataGridView();
            this.CNumero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CCod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CCambiaComp = new System.Windows.Forms.DataGridViewButtonColumn();
            this.CDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CCant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CPreci = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CPreci_Simulado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CActuali = new System.Windows.Forms.DataGridViewButtonColumn();
            this.CSubt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumeroRegistro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtCostoPrimo = new SIP.UserControls.NumericTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnExportarExcel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPorcIncr = new SIP.UserControls.NumericTextBox();
            this.txtTotal = new SIP.UserControls.NumericTextBox();
            this.txtSubt2 = new SIP.UserControls.NumericTextBox();
            this.txtSubt = new SIP.UserControls.NumericTextBox();
            this.txtSubPrecio = new SIP.UserControls.NumericTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPrecioAnterior = new SIP.UserControls.NumericTextBox();
            this.txtPorcUtilidad = new SIP.UserControls.NumericTextBox();
            this.txtMargen = new SIP.UserControls.NumericTextBox();
            this.txtPorcAplicar = new SIP.UserControls.NumericTextBox();
            this.txtPorcGtosOpera = new SIP.UserControls.NumericTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.scape1 = new SIP.UserControls.Scape();
            this.btnGuardarPrecio = new System.Windows.Forms.Button();
            this.gbDatosProducto.SuspendLayout();
            this.panelTallas.SuspendLayout();
            this.gbComponentesCostos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewComponentes)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbDatosProducto
            // 
            this.gbDatosProducto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDatosProducto.Controls.Add(this.lblFecha);
            this.gbDatosProducto.Controls.Add(this.panelTallas);
            this.gbDatosProducto.Controls.Add(this.lblDescripcion);
            this.gbDatosProducto.Controls.Add(this.txtModelo);
            this.gbDatosProducto.Controls.Add(this.label1);
            this.gbDatosProducto.Location = new System.Drawing.Point(16, 6);
            this.gbDatosProducto.Margin = new System.Windows.Forms.Padding(4);
            this.gbDatosProducto.Name = "gbDatosProducto";
            this.gbDatosProducto.Padding = new System.Windows.Forms.Padding(4);
            this.gbDatosProducto.Size = new System.Drawing.Size(969, 88);
            this.gbDatosProducto.TabIndex = 0;
            this.gbDatosProducto.TabStop = false;
            this.gbDatosProducto.Text = "Datos del Producto";
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(244, 62);
            this.lblFecha.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(162, 17);
            this.lblFecha.TabIndex = 5;
            this.lblFecha.Text = "Fecha de Actualización: ";
            // 
            // panelTallas
            // 
            this.panelTallas.Controls.Add(this.lnkPrev);
            this.panelTallas.Controls.Add(this.lnkSig);
            this.panelTallas.Controls.Add(this.label12);
            this.panelTallas.Controls.Add(this.label11);
            this.panelTallas.Location = new System.Drawing.Point(72, 60);
            this.panelTallas.Margin = new System.Windows.Forms.Padding(4);
            this.panelTallas.Name = "panelTallas";
            this.panelTallas.Size = new System.Drawing.Size(164, 22);
            this.panelTallas.TabIndex = 4;
            this.panelTallas.Visible = false;
            // 
            // lnkPrev
            // 
            this.lnkPrev.AutoSize = true;
            this.lnkPrev.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkPrev.Location = new System.Drawing.Point(4, 4);
            this.lnkPrev.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lnkPrev.Name = "lnkPrev";
            this.lnkPrev.Size = new System.Drawing.Size(31, 13);
            this.lnkPrev.TabIndex = 3;
            this.lnkPrev.TabStop = true;
            this.lnkPrev.Text = "0000";
            // 
            // lnkSig
            // 
            this.lnkSig.AutoSize = true;
            this.lnkSig.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkSig.Location = new System.Drawing.Point(124, 2);
            this.lnkSig.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lnkSig.Name = "lnkSig";
            this.lnkSig.Size = new System.Drawing.Size(31, 13);
            this.lnkSig.TabIndex = 2;
            this.lnkSig.TabStop = true;
            this.lnkSig.Text = "0000";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(78, 2);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(45, 17);
            this.label12.TabIndex = 1;
            this.label12.Text = "- Sig>";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(38, 2);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(45, 17);
            this.label11.TabIndex = 0;
            this.label11.Text = "<Prev";
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescripcion.Location = new System.Drawing.Point(258, 41);
            this.lblDescripcion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(0, 17);
            this.lblDescripcion.TabIndex = 3;
            // 
            // txtModelo
            // 
            this.txtModelo.AutoTABOnKeyDown = false;
            this.txtModelo.AutoTABOnKeyUp = false;
            this.txtModelo.Location = new System.Drawing.Point(72, 34);
            this.txtModelo.Margin = new System.Windows.Forms.Padding(4);
            this.txtModelo.Name = "txtModelo";
            this.txtModelo.OnlyUpperCase = true;
            this.txtModelo.Size = new System.Drawing.Size(163, 22);
            this.txtModelo.TabIndex = 0;
            this.txtModelo.TextChanged += new System.EventHandler(this.txtModelo_TextChanged);
            this.txtModelo.Enter += new System.EventHandler(this.txtModelo_Enter);
            this.txtModelo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtModelo_KeyDown);
            this.txtModelo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtModelo_KeyUp);
            this.txtModelo.Leave += new System.EventHandler(this.txtModelo_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Modelo";
            // 
            // gbComponentesCostos
            // 
            this.gbComponentesCostos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbComponentesCostos.Controls.Add(this.dgViewComponentes);
            this.gbComponentesCostos.Controls.Add(this.txtCostoPrimo);
            this.gbComponentesCostos.Controls.Add(this.label10);
            this.gbComponentesCostos.Location = new System.Drawing.Point(16, 98);
            this.gbComponentesCostos.Margin = new System.Windows.Forms.Padding(4);
            this.gbComponentesCostos.Name = "gbComponentesCostos";
            this.gbComponentesCostos.Padding = new System.Windows.Forms.Padding(4);
            this.gbComponentesCostos.Size = new System.Drawing.Size(969, 458);
            this.gbComponentesCostos.TabIndex = 1;
            this.gbComponentesCostos.TabStop = false;
            this.gbComponentesCostos.Text = "Componentes y Costos";
            this.gbComponentesCostos.Enter += new System.EventHandler(this.gbComponentesCostos_Enter);
            // 
            // dgViewComponentes
            // 
            this.dgViewComponentes.AllowUserToAddRows = false;
            this.dgViewComponentes.AllowUserToDeleteRows = false;
            this.dgViewComponentes.AllowUserToResizeColumns = false;
            this.dgViewComponentes.AllowUserToResizeRows = false;
            this.dgViewComponentes.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgViewComponentes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgViewComponentes.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dgViewComponentes.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgViewComponentes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgViewComponentes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CNumero,
            this.CCod,
            this.CCambiaComp,
            this.CDesc,
            this.CCant,
            this.CPreci,
            this.CPreci_Simulado,
            this.CActuali,
            this.CSubt,
            this.NumeroRegistro});
            this.dgViewComponentes.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgViewComponentes.Location = new System.Drawing.Point(12, 42);
            this.dgViewComponentes.Margin = new System.Windows.Forms.Padding(4);
            this.dgViewComponentes.Name = "dgViewComponentes";
            this.dgViewComponentes.RowHeadersVisible = false;
            this.dgViewComponentes.Size = new System.Drawing.Size(949, 349);
            this.dgViewComponentes.TabIndex = 0;
            this.dgViewComponentes.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgViewComponentes_CellClick);
            this.dgViewComponentes.CellToolTipTextNeeded += new System.Windows.Forms.DataGridViewCellToolTipTextNeededEventHandler(this.dgViewComponentes_CellToolTipTextNeeded);
            this.dgViewComponentes.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgViewComponentes_CurrentCellDirtyStateChanged);
            this.dgViewComponentes.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgViewComponentes_DataError);
            // 
            // CNumero
            // 
            this.CNumero.DataPropertyName = "CNumero";
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            this.CNumero.DefaultCellStyle = dataGridViewCellStyle1;
            this.CNumero.HeaderText = "";
            this.CNumero.Name = "CNumero";
            this.CNumero.ReadOnly = true;
            this.CNumero.Width = 20;
            // 
            // CCod
            // 
            this.CCod.DataPropertyName = "CCod";
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Gray;
            this.CCod.DefaultCellStyle = dataGridViewCellStyle2;
            this.CCod.HeaderText = "Código";
            this.CCod.Name = "CCod";
            this.CCod.ReadOnly = true;
            this.CCod.Width = 150;
            // 
            // CCambiaComp
            // 
            this.CCambiaComp.DataPropertyName = "CCambiaComp";
            this.CCambiaComp.HeaderText = "";
            this.CCambiaComp.Name = "CCambiaComp";
            this.CCambiaComp.Width = 30;
            // 
            // CDesc
            // 
            this.CDesc.DataPropertyName = "CDesc";
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            this.CDesc.DefaultCellStyle = dataGridViewCellStyle3;
            this.CDesc.HeaderText = "Descripción";
            this.CDesc.Name = "CDesc";
            this.CDesc.ReadOnly = true;
            this.CDesc.Width = 247;
            // 
            // CCant
            // 
            this.CCant.DataPropertyName = "CCant";
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = "0";
            this.CCant.DefaultCellStyle = dataGridViewCellStyle4;
            this.CCant.HeaderText = "Cantidad";
            this.CCant.Name = "CCant";
            this.CCant.Width = 50;
            // 
            // CPreci
            // 
            this.CPreci.DataPropertyName = "CPreci";
            this.CPreci.DefaultCellStyle = dataGridViewCellStyle3;
            this.CPreci.HeaderText = "Ultimo Costo";
            this.CPreci.Name = "CPreci";
            this.CPreci.ReadOnly = true;
            this.CPreci.Width = 50;
            // 
            // CPreci_Simulado
            // 
            this.CPreci_Simulado.DataPropertyName = "CPreci_Simulado";
            this.CPreci_Simulado.HeaderText = "Simulador";
            this.CPreci_Simulado.Name = "CPreci_Simulado";
            this.CPreci_Simulado.Width = 50;
            // 
            // CActuali
            // 
            this.CActuali.DataPropertyName = "CActuali";
            this.CActuali.HeaderText = "";
            this.CActuali.Name = "CActuali";
            this.CActuali.Width = 30;
            // 
            // CSubt
            // 
            this.CSubt.DataPropertyName = "CSubt";
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Gray;
            this.CSubt.DefaultCellStyle = dataGridViewCellStyle5;
            this.CSubt.HeaderText = "Subtotal";
            this.CSubt.Name = "CSubt";
            this.CSubt.ReadOnly = true;
            this.CSubt.Width = 62;
            // 
            // NumeroRegistro
            // 
            this.NumeroRegistro.DataPropertyName = "CReg";
            this.NumeroRegistro.HeaderText = "NumeroRegistro";
            this.NumeroRegistro.Name = "NumeroRegistro";
            this.NumeroRegistro.Visible = false;
            // 
            // txtCostoPrimo
            // 
            this.txtCostoPrimo.Location = new System.Drawing.Point(885, 425);
            this.txtCostoPrimo.Margin = new System.Windows.Forms.Padding(4);
            this.txtCostoPrimo.MaxValue = 0;
            this.txtCostoPrimo.MinValue = 0;
            this.txtCostoPrimo.Name = "txtCostoPrimo";
            this.txtCostoPrimo.NumberType = SIP.UserControls.TipoDeNumero.Decimal;
            this.txtCostoPrimo.ReadOnly = true;
            this.txtCostoPrimo.Size = new System.Drawing.Size(75, 22);
            this.txtCostoPrimo.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(792, 428);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(84, 17);
            this.label10.TabIndex = 1;
            this.label10.Text = "Costo Primo";
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.Location = new System.Drawing.Point(16, 576);
            this.btnExportarExcel.Margin = new System.Windows.Forms.Padding(4);
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(128, 28);
            this.btnExportarExcel.TabIndex = 2;
            this.btnExportarExcel.Text = "Exportar a Excel";
            this.btnExportarExcel.UseVisualStyleBackColor = true;
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtPorcIncr);
            this.groupBox1.Controls.Add(this.txtTotal);
            this.groupBox1.Controls.Add(this.txtSubt2);
            this.groupBox1.Controls.Add(this.txtSubt);
            this.groupBox1.Controls.Add(this.txtSubPrecio);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtPrecioAnterior);
            this.groupBox1.Controls.Add(this.txtPorcUtilidad);
            this.groupBox1.Controls.Add(this.txtMargen);
            this.groupBox1.Controls.Add(this.txtPorcAplicar);
            this.groupBox1.Controls.Add(this.txtPorcGtosOpera);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(421, 562);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(556, 189);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // txtPorcIncr
            // 
            this.txtPorcIncr.Location = new System.Drawing.Point(442, 146);
            this.txtPorcIncr.Margin = new System.Windows.Forms.Padding(4);
            this.txtPorcIncr.MaxValue = 0;
            this.txtPorcIncr.MinValue = 0;
            this.txtPorcIncr.Name = "txtPorcIncr";
            this.txtPorcIncr.NumberType = SIP.UserControls.TipoDeNumero.Decimal;
            this.txtPorcIncr.PonerCeroCuandoSeaVacio = true;
            this.txtPorcIncr.Size = new System.Drawing.Size(73, 22);
            this.txtPorcIncr.TabIndex = 6;
            this.txtPorcIncr.Text = "0";
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point(442, 114);
            this.txtTotal.Margin = new System.Windows.Forms.Padding(4);
            this.txtTotal.MaxValue = 0;
            this.txtTotal.MinValue = 0;
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.NumberType = SIP.UserControls.TipoDeNumero.Decimal;
            this.txtTotal.PonerCeroCuandoSeaVacio = true;
            this.txtTotal.Size = new System.Drawing.Size(73, 22);
            this.txtTotal.TabIndex = 5;
            this.txtTotal.Text = "0";
            // 
            // txtSubt2
            // 
            this.txtSubt2.Location = new System.Drawing.Point(442, 48);
            this.txtSubt2.Margin = new System.Windows.Forms.Padding(4);
            this.txtSubt2.MaxValue = 0;
            this.txtSubt2.MinValue = 0;
            this.txtSubt2.Name = "txtSubt2";
            this.txtSubt2.NumberType = SIP.UserControls.TipoDeNumero.Decimal;
            this.txtSubt2.ReadOnly = true;
            this.txtSubt2.Size = new System.Drawing.Size(73, 22);
            this.txtSubt2.TabIndex = 15;
            // 
            // txtSubt
            // 
            this.txtSubt.Location = new System.Drawing.Point(442, 16);
            this.txtSubt.Margin = new System.Windows.Forms.Padding(4);
            this.txtSubt.MaxValue = 0;
            this.txtSubt.MinValue = 0;
            this.txtSubt.Name = "txtSubt";
            this.txtSubt.NumberType = SIP.UserControls.TipoDeNumero.Decimal;
            this.txtSubt.ReadOnly = true;
            this.txtSubt.Size = new System.Drawing.Size(73, 22);
            this.txtSubt.TabIndex = 14;
            // 
            // txtSubPrecio
            // 
            this.txtSubPrecio.Location = new System.Drawing.Point(442, 81);
            this.txtSubPrecio.Margin = new System.Windows.Forms.Padding(4);
            this.txtSubPrecio.MaxValue = 0;
            this.txtSubPrecio.MinValue = 0;
            this.txtSubPrecio.Name = "txtSubPrecio";
            this.txtSubPrecio.NumberType = SIP.UserControls.TipoDeNumero.Decimal;
            this.txtSubPrecio.PonerCeroCuandoSeaVacio = true;
            this.txtSubPrecio.Size = new System.Drawing.Size(73, 22);
            this.txtSubPrecio.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(336, 150);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(94, 17);
            this.label9.TabIndex = 12;
            this.label9.Text = "% Incremento";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(351, 116);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 20);
            this.label8.TabIndex = 11;
            this.label8.Text = "P. Final";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(351, 85);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 17);
            this.label7.TabIndex = 10;
            this.label7.Text = "Integración";
            // 
            // txtPrecioAnterior
            // 
            this.txtPrecioAnterior.Location = new System.Drawing.Point(169, 146);
            this.txtPrecioAnterior.Margin = new System.Windows.Forms.Padding(4);
            this.txtPrecioAnterior.MaxValue = 0;
            this.txtPrecioAnterior.MinValue = 0;
            this.txtPrecioAnterior.Name = "txtPrecioAnterior";
            this.txtPrecioAnterior.NumberType = SIP.UserControls.TipoDeNumero.Decimal;
            this.txtPrecioAnterior.PonerCeroCuandoSeaVacio = true;
            this.txtPrecioAnterior.Size = new System.Drawing.Size(59, 22);
            this.txtPrecioAnterior.TabIndex = 4;
            this.txtPrecioAnterior.Text = "0";
            // 
            // txtPorcUtilidad
            // 
            this.txtPorcUtilidad.Location = new System.Drawing.Point(169, 48);
            this.txtPorcUtilidad.Margin = new System.Windows.Forms.Padding(4);
            this.txtPorcUtilidad.MaxValue = 0;
            this.txtPorcUtilidad.MinValue = 0;
            this.txtPorcUtilidad.Name = "txtPorcUtilidad";
            this.txtPorcUtilidad.NumberType = SIP.UserControls.TipoDeNumero.Decimal;
            this.txtPorcUtilidad.PonerCeroCuandoSeaVacio = true;
            this.txtPorcUtilidad.Size = new System.Drawing.Size(59, 22);
            this.txtPorcUtilidad.TabIndex = 1;
            this.txtPorcUtilidad.Text = "5";
            // 
            // txtMargen
            // 
            this.txtMargen.Location = new System.Drawing.Point(169, 114);
            this.txtMargen.Margin = new System.Windows.Forms.Padding(4);
            this.txtMargen.MaxValue = 0;
            this.txtMargen.MinValue = 0;
            this.txtMargen.Name = "txtMargen";
            this.txtMargen.NumberType = SIP.UserControls.TipoDeNumero.Decimal;
            this.txtMargen.PonerCeroCuandoSeaVacio = true;
            this.txtMargen.Size = new System.Drawing.Size(59, 22);
            this.txtMargen.TabIndex = 3;
            this.txtMargen.Text = "85";
            // 
            // txtPorcAplicar
            // 
            this.txtPorcAplicar.Location = new System.Drawing.Point(169, 81);
            this.txtPorcAplicar.Margin = new System.Windows.Forms.Padding(4);
            this.txtPorcAplicar.MaxValue = 0;
            this.txtPorcAplicar.MinValue = 0;
            this.txtPorcAplicar.Name = "txtPorcAplicar";
            this.txtPorcAplicar.NumberType = SIP.UserControls.TipoDeNumero.Decimal;
            this.txtPorcAplicar.ReadOnly = true;
            this.txtPorcAplicar.Size = new System.Drawing.Size(59, 22);
            this.txtPorcAplicar.TabIndex = 6;
            this.txtPorcAplicar.Text = "70";
            // 
            // txtPorcGtosOpera
            // 
            this.txtPorcGtosOpera.Location = new System.Drawing.Point(169, 16);
            this.txtPorcGtosOpera.Margin = new System.Windows.Forms.Padding(4);
            this.txtPorcGtosOpera.MaxValue = 0;
            this.txtPorcGtosOpera.MinValue = 0;
            this.txtPorcGtosOpera.Name = "txtPorcGtosOpera";
            this.txtPorcGtosOpera.NumberType = SIP.UserControls.TipoDeNumero.Decimal;
            this.txtPorcGtosOpera.PonerCeroCuandoSeaVacio = true;
            this.txtPorcGtosOpera.Size = new System.Drawing.Size(59, 22);
            this.txtPorcGtosOpera.TabIndex = 0;
            this.txtPorcGtosOpera.Text = "24";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 150);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 17);
            this.label6.TabIndex = 4;
            this.label6.Text = "Precio anterior";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 118);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 17);
            this.label5.TabIndex = 3;
            this.label5.Text = "Margen";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 85);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "% a aplicar";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 52);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "Utilidad %";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 20);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Gastos de Operación %";
            // 
            // scape1
            // 
            this.scape1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.scape1.Form = this;
            // 
            // btnGuardarPrecio
            // 
            this.btnGuardarPrecio.Location = new System.Drawing.Point(151, 576);
            this.btnGuardarPrecio.Margin = new System.Windows.Forms.Padding(4);
            this.btnGuardarPrecio.Name = "btnGuardarPrecio";
            this.btnGuardarPrecio.Size = new System.Drawing.Size(128, 28);
            this.btnGuardarPrecio.TabIndex = 4;
            this.btnGuardarPrecio.Text = "Guardar precio";
            this.btnGuardarPrecio.UseVisualStyleBackColor = true;
            this.btnGuardarPrecio.Click += new System.EventHandler(this.btnGuardarPrecio_Click);
            // 
            // frmSimuladorCostos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.scape1;
            this.ClientSize = new System.Drawing.Size(1001, 766);
            this.Controls.Add(this.btnGuardarPrecio);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnExportarExcel);
            this.Controls.Add(this.gbComponentesCostos);
            this.Controls.Add(this.gbDatosProducto);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSimuladorCostos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Simualdor de Costos";
            this.Load += new System.EventHandler(this.frmSimuladorCostos_Load);
            this.gbDatosProducto.ResumeLayout(false);
            this.gbDatosProducto.PerformLayout();
            this.panelTallas.ResumeLayout(false);
            this.panelTallas.PerformLayout();
            this.gbComponentesCostos.ResumeLayout(false);
            this.gbComponentesCostos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewComponentes)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbDatosProducto;
        private System.Windows.Forms.Label lblDescripcion;
        private SIP.UserControls.TextBoxEx txtModelo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbComponentesCostos;
        private System.Windows.Forms.Button btnExportarExcel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private SIP.UserControls.NumericTextBox txtPrecioAnterior;
        private SIP.UserControls.NumericTextBox txtPorcUtilidad;
        private SIP.UserControls.NumericTextBox txtMargen;
        private SIP.UserControls.NumericTextBox txtPorcAplicar;
        private SIP.UserControls.NumericTextBox txtPorcGtosOpera;
        private SIP.UserControls.NumericTextBox txtPorcIncr;
        private SIP.UserControls.NumericTextBox txtTotal;
        private SIP.UserControls.NumericTextBox txtSubt2;
        private SIP.UserControls.NumericTextBox txtSubt;
        private SIP.UserControls.NumericTextBox txtSubPrecio;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private SIP.UserControls.NumericTextBox txtCostoPrimo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridView dgViewComponentes;
        private UserControls.Scape scape1;
        private System.Windows.Forms.Panel panelTallas;
        private System.Windows.Forms.LinkLabel lnkPrev;
        private System.Windows.Forms.LinkLabel lnkSig;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridViewTextBoxColumn CNumero;
        private System.Windows.Forms.DataGridViewTextBoxColumn CCod;
        private System.Windows.Forms.DataGridViewButtonColumn CCambiaComp;
        private System.Windows.Forms.DataGridViewTextBoxColumn CDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn CCant;
        private System.Windows.Forms.DataGridViewTextBoxColumn CPreci;
        private System.Windows.Forms.DataGridViewTextBoxColumn CPreci_Simulado;
        private System.Windows.Forms.DataGridViewButtonColumn CActuali;
        private System.Windows.Forms.DataGridViewTextBoxColumn CSubt;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumeroRegistro;
        private System.Windows.Forms.Button btnGuardarPrecio;
        private System.Windows.Forms.Label lblFecha;
    }
}