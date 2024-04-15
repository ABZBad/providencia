namespace SIP
{
    partial class frmNotaCreditoDetalle
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
            this.lblAlmacen = new System.Windows.Forms.Label();
            this.lblSerie = new System.Windows.Forms.Label();
            this.gbGenerales = new System.Windows.Forms.GroupBox();
            this.lblTipoRelacion = new System.Windows.Forms.Label();
            this.cmbTipoRelacion = new System.Windows.Forms.ComboBox();
            this.lblAlmacenValor = new System.Windows.Forms.Label();
            this.lblSerieValor = new System.Windows.Forms.Label();
            this.lblUsoCFDI = new System.Windows.Forms.Label();
            this.cmbUsoCFDI = new System.Windows.Forms.ComboBox();
            this.lblMetodoDePago = new System.Windows.Forms.Label();
            this.cmbMetodoDePago = new System.Windows.Forms.ComboBox();
            this.lblFormaDePago = new System.Windows.Forms.Label();
            this.cmbFormaDePago = new System.Windows.Forms.ComboBox();
            this.lblClienteNombre = new System.Windows.Forms.Label();
            this.lblClienteRFC = new System.Windows.Forms.Label();
            this.gbDetalle = new System.Windows.Forms.GroupBox();
            this.dgvDetalle = new System.Windows.Forms.DataGridView();
            this.btnGenerar = new System.Windows.Forms.Button();
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
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.lblIVA = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblTotalImporte = new System.Windows.Forms.Label();
            this.lblIVAImporte = new System.Windows.Forms.Label();
            this.lblSubtotalImporte = new System.Windows.Forms.Label();
            this.lblObservaciones = new System.Windows.Forms.Label();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
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
            this.gbGenerales.SuspendLayout();
            this.gbDetalle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).BeginInit();
            this.gbCSD.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblAlmacen
            // 
            this.lblAlmacen.AutoSize = true;
            this.lblAlmacen.Location = new System.Drawing.Point(18, 247);
            this.lblAlmacen.Name = "lblAlmacen";
            this.lblAlmacen.Size = new System.Drawing.Size(62, 17);
            this.lblAlmacen.TabIndex = 105;
            this.lblAlmacen.Text = "Almacen";
            // 
            // lblSerie
            // 
            this.lblSerie.AutoSize = true;
            this.lblSerie.Location = new System.Drawing.Point(18, 217);
            this.lblSerie.Name = "lblSerie";
            this.lblSerie.Size = new System.Drawing.Size(41, 17);
            this.lblSerie.TabIndex = 104;
            this.lblSerie.Text = "Serie";
            // 
            // gbGenerales
            // 
            this.gbGenerales.Controls.Add(this.lblTipoRelacion);
            this.gbGenerales.Controls.Add(this.cmbTipoRelacion);
            this.gbGenerales.Controls.Add(this.lblAlmacenValor);
            this.gbGenerales.Controls.Add(this.lblSerieValor);
            this.gbGenerales.Controls.Add(this.lblUsoCFDI);
            this.gbGenerales.Controls.Add(this.cmbUsoCFDI);
            this.gbGenerales.Controls.Add(this.lblMetodoDePago);
            this.gbGenerales.Controls.Add(this.cmbMetodoDePago);
            this.gbGenerales.Controls.Add(this.lblFormaDePago);
            this.gbGenerales.Controls.Add(this.cmbFormaDePago);
            this.gbGenerales.Controls.Add(this.lblClienteNombre);
            this.gbGenerales.Controls.Add(this.lblAlmacen);
            this.gbGenerales.Controls.Add(this.lblClienteRFC);
            this.gbGenerales.Controls.Add(this.lblSerie);
            this.gbGenerales.Location = new System.Drawing.Point(433, 19);
            this.gbGenerales.Name = "gbGenerales";
            this.gbGenerales.Size = new System.Drawing.Size(843, 325);
            this.gbGenerales.TabIndex = 107;
            this.gbGenerales.TabStop = false;
            this.gbGenerales.Text = "Datos generales";
            // 
            // lblTipoRelacion
            // 
            this.lblTipoRelacion.AutoSize = true;
            this.lblTipoRelacion.Location = new System.Drawing.Point(18, 98);
            this.lblTipoRelacion.Name = "lblTipoRelacion";
            this.lblTipoRelacion.Size = new System.Drawing.Size(110, 17);
            this.lblTipoRelacion.TabIndex = 116;
            this.lblTipoRelacion.Text = "Tipo de relación";
            // 
            // cmbTipoRelacion
            // 
            this.cmbTipoRelacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoRelacion.FormattingEnabled = true;
            this.cmbTipoRelacion.Location = new System.Drawing.Point(185, 95);
            this.cmbTipoRelacion.Name = "cmbTipoRelacion";
            this.cmbTipoRelacion.Size = new System.Drawing.Size(307, 24);
            this.cmbTipoRelacion.TabIndex = 115;
            this.cmbTipoRelacion.SelectedValueChanged += new System.EventHandler(this.cmbTipoRelacion_SelectedValueChanged);
            // 
            // lblAlmacenValor
            // 
            this.lblAlmacenValor.AutoSize = true;
            this.lblAlmacenValor.Location = new System.Drawing.Point(182, 245);
            this.lblAlmacenValor.Name = "lblAlmacenValor";
            this.lblAlmacenValor.Size = new System.Drawing.Size(62, 17);
            this.lblAlmacenValor.TabIndex = 114;
            this.lblAlmacenValor.Text = "Almacen";
            // 
            // lblSerieValor
            // 
            this.lblSerieValor.AutoSize = true;
            this.lblSerieValor.Location = new System.Drawing.Point(182, 217);
            this.lblSerieValor.Name = "lblSerieValor";
            this.lblSerieValor.Size = new System.Drawing.Size(41, 17);
            this.lblSerieValor.TabIndex = 113;
            this.lblSerieValor.Text = "Serie";
            // 
            // lblUsoCFDI
            // 
            this.lblUsoCFDI.AutoSize = true;
            this.lblUsoCFDI.Location = new System.Drawing.Point(18, 188);
            this.lblUsoCFDI.Name = "lblUsoCFDI";
            this.lblUsoCFDI.Size = new System.Drawing.Size(87, 17);
            this.lblUsoCFDI.TabIndex = 112;
            this.lblUsoCFDI.Text = "Uso de CFDI";
            // 
            // cmbUsoCFDI
            // 
            this.cmbUsoCFDI.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUsoCFDI.FormattingEnabled = true;
            this.cmbUsoCFDI.Location = new System.Drawing.Point(185, 185);
            this.cmbUsoCFDI.Name = "cmbUsoCFDI";
            this.cmbUsoCFDI.Size = new System.Drawing.Size(307, 24);
            this.cmbUsoCFDI.TabIndex = 111;
            // 
            // lblMetodoDePago
            // 
            this.lblMetodoDePago.AutoSize = true;
            this.lblMetodoDePago.Location = new System.Drawing.Point(18, 128);
            this.lblMetodoDePago.Name = "lblMetodoDePago";
            this.lblMetodoDePago.Size = new System.Drawing.Size(111, 17);
            this.lblMetodoDePago.TabIndex = 110;
            this.lblMetodoDePago.Text = "Método de pago";
            // 
            // cmbMetodoDePago
            // 
            this.cmbMetodoDePago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMetodoDePago.FormattingEnabled = true;
            this.cmbMetodoDePago.Location = new System.Drawing.Point(185, 125);
            this.cmbMetodoDePago.Name = "cmbMetodoDePago";
            this.cmbMetodoDePago.Size = new System.Drawing.Size(307, 24);
            this.cmbMetodoDePago.TabIndex = 109;
            // 
            // lblFormaDePago
            // 
            this.lblFormaDePago.AutoSize = true;
            this.lblFormaDePago.Location = new System.Drawing.Point(18, 158);
            this.lblFormaDePago.Name = "lblFormaDePago";
            this.lblFormaDePago.Size = new System.Drawing.Size(104, 17);
            this.lblFormaDePago.TabIndex = 108;
            this.lblFormaDePago.Text = "Forma de pago";
            // 
            // cmbFormaDePago
            // 
            this.cmbFormaDePago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormaDePago.FormattingEnabled = true;
            this.cmbFormaDePago.Location = new System.Drawing.Point(185, 155);
            this.cmbFormaDePago.Name = "cmbFormaDePago";
            this.cmbFormaDePago.Size = new System.Drawing.Size(307, 24);
            this.cmbFormaDePago.TabIndex = 107;
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
            this.gbDetalle.Location = new System.Drawing.Point(12, 350);
            this.gbDetalle.Name = "gbDetalle";
            this.gbDetalle.Size = new System.Drawing.Size(1264, 268);
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetalle.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
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
            this.dgvDetalle.Size = new System.Drawing.Size(1242, 241);
            this.dgvDetalle.TabIndex = 11;
            this.dgvDetalle.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetalle_CellClick);
            this.dgvDetalle.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetalle_CellEndEdit);
            this.dgvDetalle.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvDetalle_CellFormatting);
            this.dgvDetalle.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetalle_CellLeave);
            this.dgvDetalle.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvDetalle_CellPainting);
            this.dgvDetalle.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvDetalle_CellValidating);
            // 
            // btnGenerar
            // 
            this.btnGenerar.Location = new System.Drawing.Point(1157, 698);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(119, 34);
            this.btnGenerar.TabIndex = 109;
            this.btnGenerar.Text = "Generar";
            this.btnGenerar.UseVisualStyleBackColor = true;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
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
            this.gbCSD.Location = new System.Drawing.Point(18, 12);
            this.gbCSD.Name = "gbCSD";
            this.gbCSD.Size = new System.Drawing.Size(409, 332);
            this.gbCSD.TabIndex = 110;
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
            // 
            // btnCargarKey
            // 
            this.btnCargarKey.Location = new System.Drawing.Point(362, 249);
            this.btnCargarKey.Name = "btnCargarKey";
            this.btnCargarKey.Size = new System.Drawing.Size(31, 22);
            this.btnCargarKey.TabIndex = 101;
            this.btnCargarKey.Text = "...";
            this.btnCargarKey.UseVisualStyleBackColor = true;
            // 
            // btnCargaCer
            // 
            this.btnCargaCer.Location = new System.Drawing.Point(362, 221);
            this.btnCargaCer.Name = "btnCargaCer";
            this.btnCargaCer.Size = new System.Drawing.Size(31, 22);
            this.btnCargaCer.TabIndex = 100;
            this.btnCargaCer.Text = "...";
            this.btnCargaCer.UseVisualStyleBackColor = true;
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
            // lblObservaciones
            // 
            this.lblObservaciones.AutoSize = true;
            this.lblObservaciones.Location = new System.Drawing.Point(15, 623);
            this.lblObservaciones.Name = "lblObservaciones";
            this.lblObservaciones.Size = new System.Drawing.Size(103, 17);
            this.lblObservaciones.TabIndex = 115;
            this.lblObservaciones.Text = "Observaciones";
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Location = new System.Drawing.Point(19, 643);
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Size = new System.Drawing.Size(1068, 89);
            this.txtObservaciones.TabIndex = 119;
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
            dataGridViewCellStyle2.Format = "C2";
            dataGridViewCellStyle2.NullValue = null;
            this.Precio.DefaultCellStyle = dataGridViewCellStyle2;
            this.Precio.HeaderText = "Precio U.";
            this.Precio.Name = "Precio";
            this.Precio.ReadOnly = true;
            this.Precio.Width = 80;
            // 
            // IVATasa
            // 
            this.IVATasa.DataPropertyName = "IVATasa";
            dataGridViewCellStyle3.Format = "P0";
            dataGridViewCellStyle3.NullValue = null;
            this.IVATasa.DefaultCellStyle = dataGridViewCellStyle3;
            this.IVATasa.HeaderText = "IVA";
            this.IVATasa.Name = "IVATasa";
            this.IVATasa.ReadOnly = true;
            this.IVATasa.Width = 80;
            // 
            // Subtotal
            // 
            this.Subtotal.DataPropertyName = "Subtotal";
            dataGridViewCellStyle4.Format = "C2";
            this.Subtotal.DefaultCellStyle = dataGridViewCellStyle4;
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
            // frmNotaCreditoDetalle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1295, 754);
            this.Controls.Add(this.txtObservaciones);
            this.Controls.Add(this.lblObservaciones);
            this.Controls.Add(this.lblTotalImporte);
            this.Controls.Add(this.lblIVAImporte);
            this.Controls.Add(this.lblSubtotalImporte);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblIVA);
            this.Controls.Add(this.lblSubtotal);
            this.Controls.Add(this.gbCSD);
            this.Controls.Add(this.btnGenerar);
            this.Controls.Add(this.gbDetalle);
            this.Controls.Add(this.gbGenerales);
            this.Name = "frmNotaCreditoDetalle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generación de Nota de Crédito";
            this.Load += new System.EventHandler(this.frmNotaCreditoDetalle_Load);
            this.gbGenerales.ResumeLayout(false);
            this.gbGenerales.PerformLayout();
            this.gbDetalle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).EndInit();
            this.gbCSD.ResumeLayout(false);
            this.gbCSD.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAlmacen;
        private System.Windows.Forms.Label lblSerie;
        private System.Windows.Forms.GroupBox gbGenerales;
        private System.Windows.Forms.GroupBox gbDetalle;
        private System.Windows.Forms.DataGridView dgvDetalle;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.GroupBox gbCSD;
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
        private System.Windows.Forms.Button btnValidar;
        private System.Windows.Forms.Button btnCargarKey;
        private System.Windows.Forms.Button btnCargaCer;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCer;
        private System.Windows.Forms.Label lblCer;
        private System.Windows.Forms.Label lblClienteNombre;
        private System.Windows.Forms.Label lblClienteRFC;
        private System.Windows.Forms.Label lblMetodoDePago;
        private System.Windows.Forms.ComboBox cmbMetodoDePago;
        private System.Windows.Forms.Label lblFormaDePago;
        private System.Windows.Forms.ComboBox cmbFormaDePago;
        private System.Windows.Forms.Label lblUsoCFDI;
        private System.Windows.Forms.ComboBox cmbUsoCFDI;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.Label lblIVA;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblTotalImporte;
        private System.Windows.Forms.Label lblIVAImporte;
        private System.Windows.Forms.Label lblSubtotalImporte;
        private System.Windows.Forms.Label lblAlmacenValor;
        private System.Windows.Forms.Label lblSerieValor;
        private System.Windows.Forms.Label lblTipoRelacion;
        private System.Windows.Forms.ComboBox cmbTipoRelacion;
        private System.Windows.Forms.Label lblObservaciones;
        private System.Windows.Forms.TextBox txtObservaciones;
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
    }
}