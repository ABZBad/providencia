namespace SIP
{
    partial class frmOrdProduccionMasiva
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
            this.lblPedido = new System.Windows.Forms.Label();
            this.dgvOrdenes = new System.Windows.Forms.DataGridView();
            this.noPedido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnProcesaOrdenes = new System.Windows.Forms.Button();
            this.dgvResumenOrdenes = new System.Windows.Forms.DataGridView();
            this.Modelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ordenes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Observaciones = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Procesado = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Configurar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.lblModelos = new System.Windows.Forms.Label();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblReferencia = new System.Windows.Forms.Label();
            this.dtFechaVencimiento = new System.Windows.Forms.DateTimePicker();
            this.gbConfiguraciónGlobal = new System.Windows.Forms.GroupBox();
            this.btnPathCodigoBarra = new System.Windows.Forms.Button();
            this.lblPathCodigoBarraValor = new System.Windows.Forms.Label();
            this.lblPathCodigoBarra = new System.Windows.Forms.Label();
            this.txtAlmacen = new SIP.UserControls.NumericTextBox();
            this.txtNumReferencia = new SIP.UserControls.NumericTextBox();
            this.gbConfiguracion = new System.Windows.Forms.GroupBox();
            this.lblTotalCantidad = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.txtEsqDeImp = new SIP.UserControls.NumericTextBox();
            this.txtCosto = new SIP.UserControls.NumericTextBox();
            this.dgvListaTallas = new System.Windows.Forms.DataGridView();
            this.Procesar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnGuardarConfiguracion = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.lblNombreProveedor = new System.Windows.Forms.Label();
            this.txtClaveProveedor = new SIP.UserControls.TextBoxEx();
            this.txtObservaciones = new SIP.UserControls.TextBoxEx();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResumenOrdenes)).BeginInit();
            this.gbConfiguraciónGlobal.SuspendLayout();
            this.gbConfiguracion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaTallas)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(413, 26);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(402, 29);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Generación de OP de Especiales";
            // 
            // lblPedido
            // 
            this.lblPedido.AutoSize = true;
            this.lblPedido.Location = new System.Drawing.Point(15, 81);
            this.lblPedido.Name = "lblPedido";
            this.lblPedido.Size = new System.Drawing.Size(148, 17);
            this.lblPedido.TabIndex = 1;
            this.lblPedido.Text = "Introduce los Pedidos:";
            // 
            // dgvOrdenes
            // 
            this.dgvOrdenes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrdenes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.noPedido});
            this.dgvOrdenes.Location = new System.Drawing.Point(15, 100);
            this.dgvOrdenes.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvOrdenes.Name = "dgvOrdenes";
            this.dgvOrdenes.RowTemplate.Height = 24;
            this.dgvOrdenes.Size = new System.Drawing.Size(232, 636);
            this.dgvOrdenes.TabIndex = 2;
            this.dgvOrdenes.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvOrdenes_EditingControlShowing);
            // 
            // noPedido
            // 
            this.noPedido.HeaderText = "Número de Pedido";
            this.noPedido.Name = "noPedido";
            this.noPedido.Width = 120;
            // 
            // btnProcesaOrdenes
            // 
            this.btnProcesaOrdenes.Location = new System.Drawing.Point(252, 196);
            this.btnProcesaOrdenes.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnProcesaOrdenes.Name = "btnProcesaOrdenes";
            this.btnProcesaOrdenes.Size = new System.Drawing.Size(87, 55);
            this.btnProcesaOrdenes.TabIndex = 3;
            this.btnProcesaOrdenes.Text = ">>";
            this.btnProcesaOrdenes.UseVisualStyleBackColor = true;
            this.btnProcesaOrdenes.Click += new System.EventHandler(this.btnProcesaOrdenes_Click);
            // 
            // dgvResumenOrdenes
            // 
            this.dgvResumenOrdenes.AllowUserToAddRows = false;
            this.dgvResumenOrdenes.AllowUserToDeleteRows = false;
            this.dgvResumenOrdenes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResumenOrdenes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Modelo,
            this.Ordenes,
            this.Observaciones,
            this.Procesado,
            this.Configurar});
            this.dgvResumenOrdenes.Location = new System.Drawing.Point(344, 100);
            this.dgvResumenOrdenes.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvResumenOrdenes.Name = "dgvResumenOrdenes";
            this.dgvResumenOrdenes.RowTemplate.Height = 24;
            this.dgvResumenOrdenes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResumenOrdenes.Size = new System.Drawing.Size(915, 240);
            this.dgvResumenOrdenes.TabIndex = 4;
            this.dgvResumenOrdenes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResumenOrdenes_CellClick);
            this.dgvResumenOrdenes.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResumenOrdenes_CellContentClick);
            this.dgvResumenOrdenes.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResumenOrdenes_CellEndEdit);
            this.dgvResumenOrdenes.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResumenOrdenes_CellEnter);
            this.dgvResumenOrdenes.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvResumenOrdenes_CellFormatting);
            this.dgvResumenOrdenes.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvResumenOrdenes_EditingControlShowing);
            // 
            // Modelo
            // 
            this.Modelo.DataPropertyName = "Modelo";
            this.Modelo.HeaderText = "Modelo";
            this.Modelo.Name = "Modelo";
            this.Modelo.ReadOnly = true;
            this.Modelo.Width = 80;
            // 
            // Ordenes
            // 
            this.Ordenes.DataPropertyName = "PedidosString";
            this.Ordenes.HeaderText = "Pedidos";
            this.Ordenes.Name = "Ordenes";
            this.Ordenes.ReadOnly = true;
            this.Ordenes.Width = 120;
            // 
            // Observaciones
            // 
            this.Observaciones.DataPropertyName = "objConfiguracion.observacionesOP";
            this.Observaciones.HeaderText = "Observaciones";
            this.Observaciones.Name = "Observaciones";
            this.Observaciones.Width = 250;
            // 
            // Procesado
            // 
            this.Procesado.DataPropertyName = "tieneConfiguracion";
            this.Procesado.HeaderText = "Configurado";
            this.Procesado.Name = "Procesado";
            this.Procesado.ReadOnly = true;
            this.Procesado.Width = 70;
            // 
            // Configurar
            // 
            this.Configurar.HeaderText = "Configurar";
            this.Configurar.Name = "Configurar";
            this.Configurar.ReadOnly = true;
            this.Configurar.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Configurar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Configurar.Text = "Configurar";
            this.Configurar.UseColumnTextForButtonValue = true;
            // 
            // lblModelos
            // 
            this.lblModelos.AutoSize = true;
            this.lblModelos.Location = new System.Drawing.Point(373, 81);
            this.lblModelos.Name = "lblModelos";
            this.lblModelos.Size = new System.Drawing.Size(145, 17);
            this.lblModelos.TabIndex = 5;
            this.lblModelos.Text = "Resumen de Modelos";
            // 
            // btnGenerar
            // 
            this.btnGenerar.Enabled = false;
            this.btnGenerar.Location = new System.Drawing.Point(652, 78);
            this.btnGenerar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(144, 43);
            this.btnGenerar.TabIndex = 25;
            this.btnGenerar.Text = "Generar OP";
            this.btnGenerar.UseVisualStyleBackColor = true;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(508, 38);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "Fch. Venc.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(343, 38);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 17);
            this.label2.TabIndex = 10;
            this.label2.Text = "Almacén";
            // 
            // lblReferencia
            // 
            this.lblReferencia.AutoSize = true;
            this.lblReferencia.Location = new System.Drawing.Point(20, 38);
            this.lblReferencia.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReferencia.Name = "lblReferencia";
            this.lblReferencia.Size = new System.Drawing.Size(179, 17);
            this.lblReferencia.TabIndex = 9;
            this.lblReferencia.Text = "Referencia (Orden) - Inicial";
            // 
            // dtFechaVencimiento
            // 
            this.dtFechaVencimiento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFechaVencimiento.Location = new System.Drawing.Point(667, 31);
            this.dtFechaVencimiento.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtFechaVencimiento.Name = "dtFechaVencimiento";
            this.dtFechaVencimiento.Size = new System.Drawing.Size(129, 22);
            this.dtFechaVencimiento.TabIndex = 22;
            // 
            // gbConfiguraciónGlobal
            // 
            this.gbConfiguraciónGlobal.Controls.Add(this.btnPathCodigoBarra);
            this.gbConfiguraciónGlobal.Controls.Add(this.lblPathCodigoBarraValor);
            this.gbConfiguraciónGlobal.Controls.Add(this.lblPathCodigoBarra);
            this.gbConfiguraciónGlobal.Controls.Add(this.txtAlmacen);
            this.gbConfiguraciónGlobal.Controls.Add(this.dtFechaVencimiento);
            this.gbConfiguraciónGlobal.Controls.Add(this.txtNumReferencia);
            this.gbConfiguraciónGlobal.Controls.Add(this.label3);
            this.gbConfiguraciónGlobal.Controls.Add(this.btnGenerar);
            this.gbConfiguraciónGlobal.Controls.Add(this.lblReferencia);
            this.gbConfiguraciónGlobal.Controls.Add(this.label2);
            this.gbConfiguraciónGlobal.Location = new System.Drawing.Point(344, 609);
            this.gbConfiguraciónGlobal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbConfiguraciónGlobal.Name = "gbConfiguraciónGlobal";
            this.gbConfiguraciónGlobal.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbConfiguraciónGlobal.Size = new System.Drawing.Size(913, 127);
            this.gbConfiguraciónGlobal.TabIndex = 15;
            this.gbConfiguraciónGlobal.TabStop = false;
            this.gbConfiguraciónGlobal.Text = "Configuración Global";
            // 
            // btnPathCodigoBarra
            // 
            this.btnPathCodigoBarra.Location = new System.Drawing.Point(211, 71);
            this.btnPathCodigoBarra.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPathCodigoBarra.Name = "btnPathCodigoBarra";
            this.btnPathCodigoBarra.Size = new System.Drawing.Size(113, 28);
            this.btnPathCodigoBarra.TabIndex = 24;
            this.btnPathCodigoBarra.Text = "Seleccionar...";
            this.btnPathCodigoBarra.UseVisualStyleBackColor = true;
            this.btnPathCodigoBarra.Click += new System.EventHandler(this.btnPathCodigoBarra_Click);
            // 
            // lblPathCodigoBarraValor
            // 
            this.lblPathCodigoBarraValor.AutoSize = true;
            this.lblPathCodigoBarraValor.Location = new System.Drawing.Point(211, 103);
            this.lblPathCodigoBarraValor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPathCodigoBarraValor.Name = "lblPathCodigoBarraValor";
            this.lblPathCodigoBarraValor.Size = new System.Drawing.Size(118, 17);
            this.lblPathCodigoBarraValor.TabIndex = 24;
            this.lblPathCodigoBarraValor.Text = "Seleccione ruta...";
            // 
            // lblPathCodigoBarra
            // 
            this.lblPathCodigoBarra.AutoSize = true;
            this.lblPathCodigoBarra.Location = new System.Drawing.Point(21, 78);
            this.lblPathCodigoBarra.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPathCodigoBarra.Name = "lblPathCodigoBarra";
            this.lblPathCodigoBarra.Size = new System.Drawing.Size(182, 17);
            this.lblPathCodigoBarra.TabIndex = 23;
            this.lblPathCodigoBarra.Text = "Ruta para código de barras";
            // 
            // txtAlmacen
            // 
            this.txtAlmacen.Enabled = false;
            this.txtAlmacen.Location = new System.Drawing.Point(415, 34);
            this.txtAlmacen.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtAlmacen.MaxValue = 0;
            this.txtAlmacen.MinValue = 0;
            this.txtAlmacen.Name = "txtAlmacen";
            this.txtAlmacen.Size = new System.Drawing.Size(31, 22);
            this.txtAlmacen.TabIndex = 22;
            this.txtAlmacen.Text = "32";
            // 
            // txtNumReferencia
            // 
            this.txtNumReferencia.Location = new System.Drawing.Point(224, 34);
            this.txtNumReferencia.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtNumReferencia.MaxValue = 0;
            this.txtNumReferencia.MinValue = 0;
            this.txtNumReferencia.Name = "txtNumReferencia";
            this.txtNumReferencia.Size = new System.Drawing.Size(99, 22);
            this.txtNumReferencia.TabIndex = 21;
            this.txtNumReferencia.Leave += new System.EventHandler(this.txtNumReferencia_Leave);
            // 
            // gbConfiguracion
            // 
            this.gbConfiguracion.Controls.Add(this.lblTotalCantidad);
            this.gbConfiguracion.Controls.Add(this.lblTotal);
            this.gbConfiguracion.Controls.Add(this.txtEsqDeImp);
            this.gbConfiguracion.Controls.Add(this.txtCosto);
            this.gbConfiguracion.Controls.Add(this.dgvListaTallas);
            this.gbConfiguracion.Controls.Add(this.btnGuardarConfiguracion);
            this.gbConfiguracion.Controls.Add(this.label1);
            this.gbConfiguracion.Controls.Add(this.btnBuscar);
            this.gbConfiguracion.Controls.Add(this.lblNombreProveedor);
            this.gbConfiguracion.Controls.Add(this.txtClaveProveedor);
            this.gbConfiguracion.Controls.Add(this.txtObservaciones);
            this.gbConfiguracion.Controls.Add(this.label9);
            this.gbConfiguracion.Controls.Add(this.label5);
            this.gbConfiguracion.Controls.Add(this.label4);
            this.gbConfiguracion.Controls.Add(this.label7);
            this.gbConfiguracion.Controls.Add(this.label8);
            this.gbConfiguracion.Enabled = false;
            this.gbConfiguracion.Location = new System.Drawing.Point(344, 346);
            this.gbConfiguracion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbConfiguracion.Name = "gbConfiguracion";
            this.gbConfiguracion.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbConfiguracion.Size = new System.Drawing.Size(913, 256);
            this.gbConfiguracion.TabIndex = 17;
            this.gbConfiguracion.TabStop = false;
            this.gbConfiguracion.Text = "Información";
            // 
            // lblTotalCantidad
            // 
            this.lblTotalCantidad.AutoSize = true;
            this.lblTotalCantidad.Location = new System.Drawing.Point(529, 218);
            this.lblTotalCantidad.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalCantidad.Name = "lblTotalCantidad";
            this.lblTotalCantidad.Size = new System.Drawing.Size(16, 17);
            this.lblTotalCantidad.TabIndex = 22;
            this.lblTotalCantidad.Text = "0";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(476, 218);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(44, 17);
            this.lblTotal.TabIndex = 21;
            this.lblTotal.Text = "Total:";
            // 
            // txtEsqDeImp
            // 
            this.txtEsqDeImp.Location = new System.Drawing.Point(392, 91);
            this.txtEsqDeImp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtEsqDeImp.MaxValue = 0;
            this.txtEsqDeImp.MinValue = 0;
            this.txtEsqDeImp.Name = "txtEsqDeImp";
            this.txtEsqDeImp.Size = new System.Drawing.Size(79, 22);
            this.txtEsqDeImp.TabIndex = 20;
            this.txtEsqDeImp.Text = "9";
            this.txtEsqDeImp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtCosto
            // 
            this.txtCosto.Location = new System.Drawing.Point(149, 86);
            this.txtCosto.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCosto.MaxValue = 0;
            this.txtCosto.MinValue = 0;
            this.txtCosto.Name = "txtCosto";
            this.txtCosto.NumberType = SIP.UserControls.TipoDeNumero.Decimal;
            this.txtCosto.Size = new System.Drawing.Size(116, 22);
            this.txtCosto.TabIndex = 18;
            this.txtCosto.Text = "0";
            this.txtCosto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dgvListaTallas
            // 
            this.dgvListaTallas.AllowUserToAddRows = false;
            this.dgvListaTallas.AllowUserToDeleteRows = false;
            this.dgvListaTallas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListaTallas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Procesar,
            this.Descripcion});
            this.dgvListaTallas.Location = new System.Drawing.Point(480, 39);
            this.dgvListaTallas.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvListaTallas.Name = "dgvListaTallas";
            this.dgvListaTallas.Size = new System.Drawing.Size(425, 165);
            this.dgvListaTallas.TabIndex = 19;
            this.dgvListaTallas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvListaTallas_CellClick);
            this.dgvListaTallas.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvListaTallas_CellEndEdit);
            // 
            // Procesar
            // 
            this.Procesar.DataPropertyName = "procesar";
            this.Procesar.HeaderText = "Procesar";
            this.Procesar.Name = "Procesar";
            this.Procesar.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Procesar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Procesar.Width = 60;
            // 
            // Descripcion
            // 
            this.Descripcion.DataPropertyName = "detalle";
            this.Descripcion.HeaderText = "Descripcion";
            this.Descripcion.Name = "Descripcion";
            this.Descripcion.ReadOnly = true;
            this.Descripcion.Width = 200;
            // 
            // btnGuardarConfiguracion
            // 
            this.btnGuardarConfiguracion.Location = new System.Drawing.Point(805, 212);
            this.btnGuardarConfiguracion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnGuardarConfiguracion.Name = "btnGuardarConfiguracion";
            this.btnGuardarConfiguracion.Size = new System.Drawing.Size(100, 28);
            this.btnGuardarConfiguracion.TabIndex = 18;
            this.btnGuardarConfiguracion.Text = "Guardar";
            this.btnGuardarConfiguracion.UseVisualStyleBackColor = true;
            this.btnGuardarConfiguracion.Click += new System.EventHandler(this.btnGuardarConfiguracion_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(536, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 17);
            this.label1.TabIndex = 17;
            this.label1.Text = "Resumen Tallas";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(235, 28);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(32, 26);
            this.btnBuscar.TabIndex = 1;
            this.btnBuscar.Text = "?";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // lblNombreProveedor
            // 
            this.lblNombreProveedor.Location = new System.Drawing.Point(149, 58);
            this.lblNombreProveedor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNombreProveedor.Name = "lblNombreProveedor";
            this.lblNombreProveedor.Size = new System.Drawing.Size(383, 30);
            this.lblNombreProveedor.TabIndex = 13;
            // 
            // txtClaveProveedor
            // 
            this.txtClaveProveedor.Location = new System.Drawing.Point(149, 30);
            this.txtClaveProveedor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtClaveProveedor.Name = "txtClaveProveedor";
            this.txtClaveProveedor.OnlyUpperCase = true;
            this.txtClaveProveedor.Size = new System.Drawing.Size(79, 22);
            this.txtClaveProveedor.TabIndex = 0;
            this.txtClaveProveedor.Text = "0";
            this.txtClaveProveedor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtClaveProveedor.Leave += new System.EventHandler(this.txtClaveProveedor_Leave);
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.AutoTABOnKeyDown = false;
            this.txtObservaciones.AutoTABOnKeyUp = false;
            this.txtObservaciones.Location = new System.Drawing.Point(149, 123);
            this.txtObservaciones.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtObservaciones.MaxLength = 254;
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.OnlyUpperCase = true;
            this.txtObservaciones.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtObservaciones.SelectAllOnFocus = false;
            this.txtObservaciones.Size = new System.Drawing.Size(321, 80);
            this.txtObservaciones.TabIndex = 4;
            this.txtObservaciones.Enter += new System.EventHandler(this.txtObservaciones_Enter);
            this.txtObservaciones.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtObservaciones_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(296, 95);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(86, 17);
            this.label9.TabIndex = 8;
            this.label9.Text = "Esq. de Imp.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 127);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "Observaciones";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 95);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "Costo";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 60);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 17);
            this.label7.TabIndex = 2;
            this.label7.Text = "Nombre";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(21, 33);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 17);
            this.label8.TabIndex = 1;
            this.label8.Text = "Proveedor";
            // 
            // frmOrdProduccionMasiva
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1273, 779);
            this.Controls.Add(this.gbConfiguracion);
            this.Controls.Add(this.gbConfiguraciónGlobal);
            this.Controls.Add(this.lblModelos);
            this.Controls.Add(this.dgvResumenOrdenes);
            this.Controls.Add(this.btnProcesaOrdenes);
            this.Controls.Add(this.dgvOrdenes);
            this.Controls.Add(this.lblPedido);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOrdProduccionMasiva";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generación de Ordenes de Producción Masiva";
            this.Load += new System.EventHandler(this.frmOrdProduccionMasiva_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResumenOrdenes)).EndInit();
            this.gbConfiguraciónGlobal.ResumeLayout(false);
            this.gbConfiguraciónGlobal.PerformLayout();
            this.gbConfiguracion.ResumeLayout(false);
            this.gbConfiguracion.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaTallas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblPedido;
        private System.Windows.Forms.DataGridView dgvOrdenes;
        private System.Windows.Forms.Button btnProcesaOrdenes;
        private System.Windows.Forms.DataGridView dgvResumenOrdenes;
        private System.Windows.Forms.Label lblModelos;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.DataGridViewTextBoxColumn noPedido;
        private System.Windows.Forms.Label label3;
        
        private System.Windows.Forms.Label label2;
        
        private System.Windows.Forms.Label lblReferencia;
        private System.Windows.Forms.DateTimePicker dtFechaVencimiento;
        private System.Windows.Forms.GroupBox gbConfiguraciónGlobal;
        private System.Windows.Forms.GroupBox gbConfiguracion;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label lblNombreProveedor;

        private UserControls.TextBoxEx txtClaveProveedor;

        private UserControls.TextBoxEx txtObservaciones;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGuardarConfiguracion;
        private System.Windows.Forms.DataGridView dgvListaTallas;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Procesar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descripcion;
        private UserControls.NumericTextBox txtCosto;
        private UserControls.NumericTextBox txtEsqDeImp;
        private UserControls.NumericTextBox txtAlmacen;
        private UserControls.NumericTextBox txtNumReferencia;
        private System.Windows.Forms.Label lblTotalCantidad;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Modelo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ordenes;
        private System.Windows.Forms.DataGridViewTextBoxColumn Observaciones;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Procesado;
        private System.Windows.Forms.DataGridViewButtonColumn Configurar;
        private System.Windows.Forms.Button btnPathCodigoBarra;
        private System.Windows.Forms.Label lblPathCodigoBarraValor;
        private System.Windows.Forms.Label lblPathCodigoBarra;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;


    }
}