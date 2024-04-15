namespace SIP
{
    partial class frmControlPedidos
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
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusStripLblUsuario = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripSeparator1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripLblArea = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripSeparator2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripLblFecha = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.dgvPedidos = new System.Windows.Forms.DataGridView();
            this.Seleccion = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CLAVE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOMBRE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CVE_VEND = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Prendas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FECHA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreCorto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ESTATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OBSERVACIONES = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Referencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrdenAgrupador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Marcar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.TipoProceso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClaveTipoProceso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ESTATUSPEDIDO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PedidoOrigen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MarcarHabilitado = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.TipoPedido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.treeViewProcesos = new System.Windows.Forms.TreeView();
            this.dgvHistorico = new System.Windows.Forms.DataGridView();
            this.gbBuscador = new System.Windows.Forms.GroupBox();
            this.btnReporte = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.cmbTipo = new System.Windows.Forms.ComboBox();
            this.chkTipo = new System.Windows.Forms.CheckBox();
            this.cmbClientes = new System.Windows.Forms.ComboBox();
            this.chkCliente = new System.Windows.Forms.CheckBox();
            this.chkProceso = new System.Windows.Forms.CheckBox();
            this.chkFechaHasta = new System.Windows.Forms.CheckBox();
            this.dtpFechaHasta = new System.Windows.Forms.DateTimePicker();
            this.chkFechaDesde = new System.Windows.Forms.CheckBox();
            this.cmbProcesos = new System.Windows.Forms.ComboBox();
            this.dtpFechaDesde = new System.Windows.Forms.DateTimePicker();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.lblPedidos = new System.Windows.Forms.Label();
            this.pnlObservaciones = new System.Windows.Forms.Panel();
            this.btnCerrarObservaciones = new System.Windows.Forms.Button();
            this.lstObservaciones = new System.Windows.Forms.ListBox();
            this.lblHistorico = new System.Windows.Forms.Label();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorico)).BeginInit();
            this.gbBuscador.SuspendLayout();
            this.pnlObservaciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusStripLblUsuario,
            this.statusStripSeparator1,
            this.statusStripLblArea,
            this.statusStripSeparator2,
            this.statusStripLblFecha,
            this.toolStripStatusLabel2});
            this.statusStrip.Location = new System.Drawing.Point(0, 732);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip.Size = new System.Drawing.Size(1705, 25);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip1";
            // 
            // statusStripLblUsuario
            // 
            this.statusStripLblUsuario.Name = "statusStripLblUsuario";
            this.statusStripLblUsuario.Size = new System.Drawing.Size(60, 20);
            this.statusStripLblUsuario.Text = "usuario:";
            // 
            // statusStripSeparator1
            // 
            this.statusStripSeparator1.Name = "statusStripSeparator1";
            this.statusStripSeparator1.Size = new System.Drawing.Size(13, 20);
            this.statusStripSeparator1.Text = "|";
            // 
            // statusStripLblArea
            // 
            this.statusStripLblArea.Name = "statusStripLblArea";
            this.statusStripLblArea.Size = new System.Drawing.Size(43, 20);
            this.statusStripLblArea.Text = "Área:";
            // 
            // statusStripSeparator2
            // 
            this.statusStripSeparator2.Name = "statusStripSeparator2";
            this.statusStripSeparator2.Size = new System.Drawing.Size(13, 20);
            this.statusStripSeparator2.Text = "|";
            // 
            // statusStripLblFecha
            // 
            this.statusStripLblFecha.Name = "statusStripLblFecha";
            this.statusStripLblFecha.Size = new System.Drawing.Size(50, 20);
            this.statusStripLblFecha.Text = "Fecha:";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(13, 20);
            this.toolStripStatusLabel2.Text = "|";
            // 
            // dgvPedidos
            // 
            this.dgvPedidos.AllowUserToAddRows = false;
            this.dgvPedidos.AllowUserToDeleteRows = false;
            this.dgvPedidos.AllowUserToResizeRows = false;
            this.dgvPedidos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPedidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPedidos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Seleccion,
            this.ID,
            this.CLAVE,
            this.NOMBRE,
            this.CVE_VEND,
            this.Prendas,
            this.FECHA,
            this.NombreCorto,
            this.ESTATUS,
            this.OBSERVACIONES,
            this.Referencia,
            this.OrdenAgrupador,
            this.Marcar,
            this.TipoProceso,
            this.ClaveTipoProceso,
            this.ESTATUSPEDIDO,
            this.PedidoOrigen,
            this.MarcarHabilitado,
            this.TipoPedido});
            this.dgvPedidos.Location = new System.Drawing.Point(416, 127);
            this.dgvPedidos.Margin = new System.Windows.Forms.Padding(4);
            this.dgvPedidos.MultiSelect = false;
            this.dgvPedidos.Name = "dgvPedidos";
            this.dgvPedidos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPedidos.Size = new System.Drawing.Size(1273, 469);
            this.dgvPedidos.TabIndex = 1;
            this.dgvPedidos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPedidos_CellContentClick);
            this.dgvPedidos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPedidos_CellDoubleClick);
            this.dgvPedidos.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvPedidos_MouseClick);
            // 
            // Seleccion
            // 
            this.Seleccion.DataPropertyName = "Seleccion";
            this.Seleccion.FalseValue = "0";
            this.Seleccion.HeaderText = "";
            this.Seleccion.IndeterminateValue = "0";
            this.Seleccion.Name = "Seleccion";
            this.Seleccion.ReadOnly = true;
            this.Seleccion.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Seleccion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Seleccion.TrueValue = "1";
            this.Seleccion.Visible = false;
            this.Seleccion.Width = 25;
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            this.ID.Width = 50;
            // 
            // CLAVE
            // 
            this.CLAVE.DataPropertyName = "CLAVE";
            this.CLAVE.HeaderText = "Clave";
            this.CLAVE.Name = "CLAVE";
            this.CLAVE.ReadOnly = true;
            this.CLAVE.Width = 50;
            // 
            // NOMBRE
            // 
            this.NOMBRE.DataPropertyName = "NOMBRE";
            this.NOMBRE.HeaderText = "Nombre";
            this.NOMBRE.Name = "NOMBRE";
            this.NOMBRE.ReadOnly = true;
            this.NOMBRE.Width = 220;
            // 
            // CVE_VEND
            // 
            this.CVE_VEND.DataPropertyName = "CVE_VEND";
            this.CVE_VEND.HeaderText = "Ejecutivo";
            this.CVE_VEND.Name = "CVE_VEND";
            this.CVE_VEND.ReadOnly = true;
            // 
            // Prendas
            // 
            this.Prendas.DataPropertyName = "TotalPrendas";
            this.Prendas.HeaderText = "Prendas";
            this.Prendas.Name = "Prendas";
            this.Prendas.ReadOnly = true;
            // 
            // FECHA
            // 
            this.FECHA.DataPropertyName = "FECHA";
            this.FECHA.HeaderText = "Fecha";
            this.FECHA.Name = "FECHA";
            this.FECHA.ReadOnly = true;
            this.FECHA.Width = 70;
            // 
            // NombreCorto
            // 
            this.NombreCorto.DataPropertyName = "NombreCorto";
            this.NombreCorto.HeaderText = "Proceso";
            this.NombreCorto.Name = "NombreCorto";
            this.NombreCorto.ReadOnly = true;
            this.NombreCorto.Width = 130;
            // 
            // ESTATUS
            // 
            this.ESTATUS.DataPropertyName = "DescripcionEstatus";
            this.ESTATUS.HeaderText = "Estatus";
            this.ESTATUS.Name = "ESTATUS";
            this.ESTATUS.ReadOnly = true;
            this.ESTATUS.Width = 70;
            // 
            // OBSERVACIONES
            // 
            this.OBSERVACIONES.DataPropertyName = "UltimasObservaciones";
            this.OBSERVACIONES.HeaderText = "Observaciones";
            this.OBSERVACIONES.Name = "OBSERVACIONES";
            this.OBSERVACIONES.ReadOnly = true;
            this.OBSERVACIONES.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.OBSERVACIONES.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.OBSERVACIONES.VisitedLinkColor = System.Drawing.Color.Blue;
            this.OBSERVACIONES.Width = 220;
            // 
            // Referencia
            // 
            this.Referencia.DataPropertyName = "ReferenciaProceso";
            this.Referencia.HeaderText = "Referencia";
            this.Referencia.Name = "Referencia";
            this.Referencia.ReadOnly = true;
            this.Referencia.Width = 80;
            // 
            // OrdenAgrupador
            // 
            this.OrdenAgrupador.DataPropertyName = "OrdenAgrupador";
            this.OrdenAgrupador.HeaderText = "OrdenAgrupador";
            this.OrdenAgrupador.Name = "OrdenAgrupador";
            this.OrdenAgrupador.ReadOnly = true;
            this.OrdenAgrupador.Visible = false;
            // 
            // Marcar
            // 
            this.Marcar.DataPropertyName = "Marcar";
            this.Marcar.HeaderText = "Marcar";
            this.Marcar.Name = "Marcar";
            this.Marcar.ReadOnly = true;
            this.Marcar.Visible = false;
            // 
            // TipoProceso
            // 
            this.TipoProceso.DataPropertyName = "DescripcionProceso";
            this.TipoProceso.HeaderText = "Tipo";
            this.TipoProceso.Name = "TipoProceso";
            // 
            // ClaveTipoProceso
            // 
            this.ClaveTipoProceso.DataPropertyName = "ClaveTipoProceso";
            this.ClaveTipoProceso.HeaderText = "ClaveTipoProceso";
            this.ClaveTipoProceso.Name = "ClaveTipoProceso";
            this.ClaveTipoProceso.Visible = false;
            // 
            // ESTATUSPEDIDO
            // 
            this.ESTATUSPEDIDO.DataPropertyName = "ESTATUSPEDIDO";
            this.ESTATUSPEDIDO.HeaderText = "ESTATUS";
            this.ESTATUSPEDIDO.Name = "ESTATUSPEDIDO";
            this.ESTATUSPEDIDO.Visible = false;
            // 
            // PedidoOrigen
            // 
            this.PedidoOrigen.DataPropertyName = "PedidoOrigen";
            this.PedidoOrigen.HeaderText = "Pedido Origen";
            this.PedidoOrigen.Name = "PedidoOrigen";
            this.PedidoOrigen.ReadOnly = true;
            // 
            // MarcarHabilitado
            // 
            this.MarcarHabilitado.DataPropertyName = "MarcarHabilitado";
            this.MarcarHabilitado.HeaderText = "MarcarHabilitado";
            this.MarcarHabilitado.Name = "MarcarHabilitado";
            this.MarcarHabilitado.Visible = false;
            // 
            // TipoPedido
            // 
            this.TipoPedido.DataPropertyName = "TipoPedido";
            this.TipoPedido.HeaderText = "Identificador";
            this.TipoPedido.Name = "TipoPedido";
            this.TipoPedido.ReadOnly = true;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(16, 11);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(696, 31);
            this.lblTitulo.TabIndex = 2;
            this.lblTitulo.Text = "CONTROL ADMINISTRATIVO DEL PEDIDO EN SIP";
            // 
            // treeViewProcesos
            // 
            this.treeViewProcesos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeViewProcesos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeViewProcesos.HideSelection = false;
            this.treeViewProcesos.Location = new System.Drawing.Point(16, 107);
            this.treeViewProcesos.Margin = new System.Windows.Forms.Padding(4);
            this.treeViewProcesos.Name = "treeViewProcesos";
            this.treeViewProcesos.Size = new System.Drawing.Size(391, 618);
            this.treeViewProcesos.TabIndex = 3;
            this.treeViewProcesos.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewProcesos_AfterSelect);
            // 
            // dgvHistorico
            // 
            this.dgvHistorico.AllowUserToAddRows = false;
            this.dgvHistorico.AllowUserToDeleteRows = false;
            this.dgvHistorico.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvHistorico.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvHistorico.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHistorico.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvHistorico.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvHistorico.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvHistorico.Location = new System.Drawing.Point(416, 621);
            this.dgvHistorico.Margin = new System.Windows.Forms.Padding(4);
            this.dgvHistorico.Name = "dgvHistorico";
            this.dgvHistorico.ReadOnly = true;
            this.dgvHistorico.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvHistorico.Size = new System.Drawing.Size(1273, 105);
            this.dgvHistorico.TabIndex = 4;
            // 
            // gbBuscador
            // 
            this.gbBuscador.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbBuscador.Controls.Add(this.btnReporte);
            this.gbBuscador.Controls.Add(this.btnRefresh);
            this.gbBuscador.Controls.Add(this.cmbTipo);
            this.gbBuscador.Controls.Add(this.chkTipo);
            this.gbBuscador.Controls.Add(this.cmbClientes);
            this.gbBuscador.Controls.Add(this.chkCliente);
            this.gbBuscador.Controls.Add(this.chkProceso);
            this.gbBuscador.Controls.Add(this.chkFechaHasta);
            this.gbBuscador.Controls.Add(this.dtpFechaHasta);
            this.gbBuscador.Controls.Add(this.chkFechaDesde);
            this.gbBuscador.Controls.Add(this.cmbProcesos);
            this.gbBuscador.Controls.Add(this.dtpFechaDesde);
            this.gbBuscador.Controls.Add(this.btnBuscar);
            this.gbBuscador.Location = new System.Drawing.Point(23, 47);
            this.gbBuscador.Margin = new System.Windows.Forms.Padding(4);
            this.gbBuscador.Name = "gbBuscador";
            this.gbBuscador.Padding = new System.Windows.Forms.Padding(4);
            this.gbBuscador.Size = new System.Drawing.Size(1667, 53);
            this.gbBuscador.TabIndex = 5;
            this.gbBuscador.TabStop = false;
            this.gbBuscador.Text = "Buscador";
            // 
            // btnReporte
            // 
            this.btnReporte.Location = new System.Drawing.Point(1568, 16);
            this.btnReporte.Margin = new System.Windows.Forms.Padding(4);
            this.btnReporte.Name = "btnReporte";
            this.btnReporte.Size = new System.Drawing.Size(91, 28);
            this.btnReporte.TabIndex = 16;
            this.btnReporte.Text = "Reporte";
            this.btnReporte.UseVisualStyleBackColor = true;
            this.btnReporte.Visible = false;
            this.btnReporte.Click += new System.EventHandler(this.btnReporte_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackgroundImage = global::SIP.Properties.Resources.refresh;
            this.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRefresh.Location = new System.Drawing.Point(1531, 16);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(29, 28);
            this.btnRefresh.TabIndex = 15;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // cmbTipo
            // 
            this.cmbTipo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbTipo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbTipo.FormattingEnabled = true;
            this.cmbTipo.Location = new System.Drawing.Point(1320, 19);
            this.cmbTipo.Margin = new System.Windows.Forms.Padding(4);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.Size = new System.Drawing.Size(127, 24);
            this.cmbTipo.TabIndex = 14;
            // 
            // chkTipo
            // 
            this.chkTipo.AutoSize = true;
            this.chkTipo.Location = new System.Drawing.Point(1254, 20);
            this.chkTipo.Margin = new System.Windows.Forms.Padding(4);
            this.chkTipo.Name = "chkTipo";
            this.chkTipo.Size = new System.Drawing.Size(58, 21);
            this.chkTipo.TabIndex = 13;
            this.chkTipo.Text = "Tipo";
            this.chkTipo.UseVisualStyleBackColor = true;
            // 
            // cmbClientes
            // 
            this.cmbClientes.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbClientes.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbClientes.FormattingEnabled = true;
            this.cmbClientes.Location = new System.Drawing.Point(852, 19);
            this.cmbClientes.Margin = new System.Windows.Forms.Padding(4);
            this.cmbClientes.Name = "cmbClientes";
            this.cmbClientes.Size = new System.Drawing.Size(384, 24);
            this.cmbClientes.TabIndex = 12;
            // 
            // chkCliente
            // 
            this.chkCliente.AutoSize = true;
            this.chkCliente.Location = new System.Drawing.Point(771, 20);
            this.chkCliente.Margin = new System.Windows.Forms.Padding(4);
            this.chkCliente.Name = "chkCliente";
            this.chkCliente.Size = new System.Drawing.Size(73, 21);
            this.chkCliente.TabIndex = 11;
            this.chkCliente.Text = "Cliente";
            this.chkCliente.UseVisualStyleBackColor = true;
            // 
            // chkProceso
            // 
            this.chkProceso.AutoSize = true;
            this.chkProceso.Location = new System.Drawing.Point(449, 20);
            this.chkProceso.Margin = new System.Windows.Forms.Padding(4);
            this.chkProceso.Name = "chkProceso";
            this.chkProceso.Size = new System.Drawing.Size(82, 21);
            this.chkProceso.TabIndex = 10;
            this.chkProceso.Text = "Proceso";
            this.chkProceso.UseVisualStyleBackColor = true;
            // 
            // chkFechaHasta
            // 
            this.chkFechaHasta.AutoSize = true;
            this.chkFechaHasta.Location = new System.Drawing.Point(228, 20);
            this.chkFechaHasta.Margin = new System.Windows.Forms.Padding(4);
            this.chkFechaHasta.Name = "chkFechaHasta";
            this.chkFechaHasta.Size = new System.Drawing.Size(67, 21);
            this.chkFechaHasta.TabIndex = 9;
            this.chkFechaHasta.Text = "Hasta";
            this.chkFechaHasta.UseVisualStyleBackColor = true;
            // 
            // dtpFechaHasta
            // 
            this.dtpFechaHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaHasta.Location = new System.Drawing.Point(303, 20);
            this.dtpFechaHasta.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaHasta.Name = "dtpFechaHasta";
            this.dtpFechaHasta.Size = new System.Drawing.Size(125, 22);
            this.dtpFechaHasta.TabIndex = 8;
            // 
            // chkFechaDesde
            // 
            this.chkFechaDesde.AutoSize = true;
            this.chkFechaDesde.Location = new System.Drawing.Point(22, 20);
            this.chkFechaDesde.Margin = new System.Windows.Forms.Padding(4);
            this.chkFechaDesde.Name = "chkFechaDesde";
            this.chkFechaDesde.Size = new System.Drawing.Size(71, 21);
            this.chkFechaDesde.TabIndex = 7;
            this.chkFechaDesde.Text = "Desde";
            this.chkFechaDesde.UseVisualStyleBackColor = true;
            // 
            // cmbProcesos
            // 
            this.cmbProcesos.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbProcesos.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbProcesos.FormattingEnabled = true;
            this.cmbProcesos.Location = new System.Drawing.Point(538, 19);
            this.cmbProcesos.Margin = new System.Windows.Forms.Padding(4);
            this.cmbProcesos.Name = "cmbProcesos";
            this.cmbProcesos.Size = new System.Drawing.Size(225, 24);
            this.cmbProcesos.TabIndex = 6;
            // 
            // dtpFechaDesde
            // 
            this.dtpFechaDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaDesde.Location = new System.Drawing.Point(101, 20);
            this.dtpFechaDesde.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaDesde.Name = "dtpFechaDesde";
            this.dtpFechaDesde.Size = new System.Drawing.Size(119, 22);
            this.dtpFechaDesde.TabIndex = 4;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(1455, 17);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(4);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(68, 28);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // lblPedidos
            // 
            this.lblPedidos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPedidos.AutoSize = true;
            this.lblPedidos.Location = new System.Drawing.Point(412, 107);
            this.lblPedidos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPedidos.Name = "lblPedidos";
            this.lblPedidos.Size = new System.Drawing.Size(113, 17);
            this.lblPedidos.TabIndex = 7;
            this.lblPedidos.Text = "Lista de Pedidos";
            // 
            // pnlObservaciones
            // 
            this.pnlObservaciones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlObservaciones.Controls.Add(this.btnCerrarObservaciones);
            this.pnlObservaciones.Controls.Add(this.lstObservaciones);
            this.pnlObservaciones.Location = new System.Drawing.Point(563, 209);
            this.pnlObservaciones.Margin = new System.Windows.Forms.Padding(4);
            this.pnlObservaciones.Name = "pnlObservaciones";
            this.pnlObservaciones.Size = new System.Drawing.Size(1045, 313);
            this.pnlObservaciones.TabIndex = 9;
            this.pnlObservaciones.Visible = false;
            this.pnlObservaciones.Leave += new System.EventHandler(this.pnlObservaciones_Leave);
            // 
            // btnCerrarObservaciones
            // 
            this.btnCerrarObservaciones.Location = new System.Drawing.Point(891, 272);
            this.btnCerrarObservaciones.Margin = new System.Windows.Forms.Padding(4);
            this.btnCerrarObservaciones.Name = "btnCerrarObservaciones";
            this.btnCerrarObservaciones.Size = new System.Drawing.Size(67, 28);
            this.btnCerrarObservaciones.TabIndex = 7;
            this.btnCerrarObservaciones.Text = "Cerrar";
            this.btnCerrarObservaciones.UseVisualStyleBackColor = true;
            this.btnCerrarObservaciones.Click += new System.EventHandler(this.btnCerrarObservaciones_Click);
            // 
            // lstObservaciones
            // 
            this.lstObservaciones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstObservaciones.FormattingEnabled = true;
            this.lstObservaciones.HorizontalScrollbar = true;
            this.lstObservaciones.ItemHeight = 16;
            this.lstObservaciones.Location = new System.Drawing.Point(11, 4);
            this.lstObservaciones.Margin = new System.Windows.Forms.Padding(4);
            this.lstObservaciones.Name = "lstObservaciones";
            this.lstObservaciones.Size = new System.Drawing.Size(1029, 260);
            this.lstObservaciones.TabIndex = 10;
            // 
            // lblHistorico
            // 
            this.lblHistorico.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHistorico.AutoSize = true;
            this.lblHistorico.Location = new System.Drawing.Point(412, 600);
            this.lblHistorico.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHistorico.Name = "lblHistorico";
            this.lblHistorico.Size = new System.Drawing.Size(142, 17);
            this.lblHistorico.TabIndex = 8;
            this.lblHistorico.Text = "Historico del Pedido: ";
            // 
            // frmControlPedidos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1705, 757);
            this.Controls.Add(this.pnlObservaciones);
            this.Controls.Add(this.lblHistorico);
            this.Controls.Add(this.lblPedidos);
            this.Controls.Add(this.gbBuscador);
            this.Controls.Add(this.dgvHistorico);
            this.Controls.Add(this.treeViewProcesos);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.dgvPedidos);
            this.Controls.Add(this.statusStrip);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmControlPedidos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Control de UpPedidos";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmControlPedidos_FormClosing);
            this.Load += new System.EventHandler(this.frmControlPedidos_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorico)).EndInit();
            this.gbBuscador.ResumeLayout(false);
            this.gbBuscador.PerformLayout();
            this.pnlObservaciones.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusStripLblUsuario;
        private System.Windows.Forms.ToolStripStatusLabel statusStripSeparator1;
        private System.Windows.Forms.ToolStripStatusLabel statusStripLblArea;
        private System.Windows.Forms.ToolStripStatusLabel statusStripSeparator2;
        private System.Windows.Forms.ToolStripStatusLabel statusStripLblFecha;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.DataGridView dgvPedidos;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.TreeView treeViewProcesos;
        private System.Windows.Forms.DataGridView dgvHistorico;
        private System.Windows.Forms.GroupBox gbBuscador;
        private System.Windows.Forms.DateTimePicker dtpFechaDesde;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.ComboBox cmbProcesos;
        private System.Windows.Forms.Label lblPedidos;
        private System.Windows.Forms.Panel pnlObservaciones;
        private System.Windows.Forms.Button btnCerrarObservaciones;
        private System.Windows.Forms.ListBox lstObservaciones;
        private System.Windows.Forms.CheckBox chkFechaHasta;
        private System.Windows.Forms.DateTimePicker dtpFechaHasta;
        private System.Windows.Forms.CheckBox chkFechaDesde;
        private System.Windows.Forms.CheckBox chkProceso;
        private System.Windows.Forms.CheckBox chkCliente;
        private System.Windows.Forms.ComboBox cmbClientes;
        private System.Windows.Forms.ComboBox cmbTipo;
        private System.Windows.Forms.CheckBox chkTipo;
        private System.Windows.Forms.Label lblHistorico;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnReporte;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Seleccion;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CLAVE;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOMBRE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CVE_VEND;
        private System.Windows.Forms.DataGridViewTextBoxColumn Prendas;
        private System.Windows.Forms.DataGridViewTextBoxColumn FECHA;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreCorto;
        private System.Windows.Forms.DataGridViewTextBoxColumn ESTATUS;
        private System.Windows.Forms.DataGridViewLinkColumn OBSERVACIONES;
        private System.Windows.Forms.DataGridViewTextBoxColumn Referencia;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrdenAgrupador;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Marcar;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoProceso;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClaveTipoProceso;
        private System.Windows.Forms.DataGridViewTextBoxColumn ESTATUSPEDIDO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PedidoOrigen;
        private System.Windows.Forms.DataGridViewCheckBoxColumn MarcarHabilitado;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoPedido;
    }
}