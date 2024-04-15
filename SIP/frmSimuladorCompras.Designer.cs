namespace SIP
{
    partial class frmSimuladorCompras
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.treeView = new System.Windows.Forms.TreeView();
            this.btnAgregarProveedor = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnNuevoSimulador = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.lblFecha = new System.Windows.Forms.Label();
            this.btnExportar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.txtDiferencia = new System.Windows.Forms.TextBox();
            this.lblDiferencia = new System.Windows.Forms.Label();
            this.txtSubtotal = new System.Windows.Forms.TextBox();
            this.lblSubotal = new System.Windows.Forms.Label();
            this.txtPresupuestoFinal = new System.Windows.Forms.TextBox();
            this.lblPresupeustoFinal = new System.Windows.Forms.Label();
            this.txtPorcentaje = new System.Windows.Forms.TextBox();
            this.lblPorcentaje = new System.Windows.Forms.Label();
            this.txtPresupuesto = new System.Windows.Forms.TextBox();
            this.lblPresupuestoCobranza = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lblProveedorTotal = new System.Windows.Forms.Label();
            this.btnProveedorAgregar = new System.Windows.Forms.Button();
            this.btnProveedorCerrar = new System.Windows.Forms.Button();
            this.lblProveedorTitiloTotal = new System.Windows.Forms.Label();
            this.cmbProveedores = new System.Windows.Forms.ComboBox();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.dgvArticulos = new System.Windows.Forms.DataGridView();
            this.lblCotizacionNombreProveedor = new System.Windows.Forms.Label();
            this.lblSimulaciones = new System.Windows.Forms.Label();
            this.cmbSimulaciones = new System.Windows.Forms.ComboBox();
            this.btnCargarSimulacion = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.txtCriterio = new SIP.UserControls.TextBoxEx();
            this.PEDIDO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ULTIMOCOSTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRECIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SUBTOTAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IVA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TOTAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvArticulos)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnEditar);
            this.groupBox1.Controls.Add(this.treeView);
            this.groupBox1.Controls.Add(this.btnAgregarProveedor);
            this.groupBox1.Location = new System.Drawing.Point(16, 111);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(689, 652);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Detalle";
            // 
            // treeView
            // 
            this.treeView.Location = new System.Drawing.Point(8, 76);
            this.treeView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(672, 528);
            this.treeView.TabIndex = 2;
            // 
            // btnAgregarProveedor
            // 
            this.btnAgregarProveedor.Location = new System.Drawing.Point(8, 36);
            this.btnAgregarProveedor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAgregarProveedor.Name = "btnAgregarProveedor";
            this.btnAgregarProveedor.Size = new System.Drawing.Size(673, 28);
            this.btnAgregarProveedor.TabIndex = 1;
            this.btnAgregarProveedor.Text = "+ Agregar Proveedor";
            this.btnAgregarProveedor.UseVisualStyleBackColor = true;
            this.btnAgregarProveedor.Click += new System.EventHandler(this.btnAgregarProveedor_Click);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(399, 11);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(281, 29);
            this.lblTitulo.TabIndex = 15;
            this.lblTitulo.Text = "Simulador de Compras";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnNuevoSimulador);
            this.groupBox2.Controls.Add(this.dateTimePicker1);
            this.groupBox2.Controls.Add(this.lblFecha);
            this.groupBox2.Controls.Add(this.btnExportar);
            this.groupBox2.Controls.Add(this.btnGuardar);
            this.groupBox2.Controls.Add(this.txtDiferencia);
            this.groupBox2.Controls.Add(this.lblDiferencia);
            this.groupBox2.Controls.Add(this.txtSubtotal);
            this.groupBox2.Controls.Add(this.lblSubotal);
            this.groupBox2.Controls.Add(this.txtPresupuestoFinal);
            this.groupBox2.Controls.Add(this.lblPresupeustoFinal);
            this.groupBox2.Controls.Add(this.txtPorcentaje);
            this.groupBox2.Controls.Add(this.lblPorcentaje);
            this.groupBox2.Controls.Add(this.txtPresupuesto);
            this.groupBox2.Controls.Add(this.lblPresupuestoCobranza);
            this.groupBox2.Location = new System.Drawing.Point(713, 111);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(405, 652);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Simulador";
            // 
            // btnNuevoSimulador
            // 
            this.btnNuevoSimulador.Location = new System.Drawing.Point(8, 23);
            this.btnNuevoSimulador.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnNuevoSimulador.Name = "btnNuevoSimulador";
            this.btnNuevoSimulador.Size = new System.Drawing.Size(389, 28);
            this.btnNuevoSimulador.TabIndex = 14;
            this.btnNuevoSimulador.Text = "Crear nueva simulación de compra";
            this.btnNuevoSimulador.UseVisualStyleBackColor = true;
            this.btnNuevoSimulador.Click += new System.EventHandler(this.btnNuevoSimulador_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(264, 59);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(132, 22);
            this.dateTimePicker1.TabIndex = 13;
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(29, 68);
            this.lblFecha.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(51, 17);
            this.lblFecha.TabIndex = 12;
            this.lblFecha.Text = "Fecha:";
            // 
            // btnExportar
            // 
            this.btnExportar.Location = new System.Drawing.Point(264, 597);
            this.btnExportar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(133, 28);
            this.btnExportar.TabIndex = 11;
            this.btnExportar.Text = "Exportar a excel";
            this.btnExportar.UseVisualStyleBackColor = true;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(264, 561);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(133, 28);
            this.btnGuardar.TabIndex = 10;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // txtDiferencia
            // 
            this.txtDiferencia.Enabled = false;
            this.txtDiferencia.Location = new System.Drawing.Point(264, 242);
            this.txtDiferencia.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDiferencia.Name = "txtDiferencia";
            this.txtDiferencia.Size = new System.Drawing.Size(132, 22);
            this.txtDiferencia.TabIndex = 9;
            this.txtDiferencia.Text = "0.0";
            this.txtDiferencia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblDiferencia
            // 
            this.lblDiferencia.AutoSize = true;
            this.lblDiferencia.Location = new System.Drawing.Point(29, 246);
            this.lblDiferencia.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDiferencia.Name = "lblDiferencia";
            this.lblDiferencia.Size = new System.Drawing.Size(72, 17);
            this.lblDiferencia.TabIndex = 8;
            this.lblDiferencia.Text = "Diferencia";
            // 
            // txtSubtotal
            // 
            this.txtSubtotal.Enabled = false;
            this.txtSubtotal.Location = new System.Drawing.Point(264, 210);
            this.txtSubtotal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSubtotal.Name = "txtSubtotal";
            this.txtSubtotal.Size = new System.Drawing.Size(132, 22);
            this.txtSubtotal.TabIndex = 7;
            this.txtSubtotal.Text = "0.0";
            this.txtSubtotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblSubotal
            // 
            this.lblSubotal.AutoSize = true;
            this.lblSubotal.Location = new System.Drawing.Point(29, 214);
            this.lblSubotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubotal.Name = "lblSubotal";
            this.lblSubotal.Size = new System.Drawing.Size(44, 17);
            this.lblSubotal.TabIndex = 6;
            this.lblSubotal.Text = "Total:";
            // 
            // txtPresupuestoFinal
            // 
            this.txtPresupuestoFinal.Enabled = false;
            this.txtPresupuestoFinal.Location = new System.Drawing.Point(264, 155);
            this.txtPresupuestoFinal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPresupuestoFinal.Name = "txtPresupuestoFinal";
            this.txtPresupuestoFinal.Size = new System.Drawing.Size(132, 22);
            this.txtPresupuestoFinal.TabIndex = 5;
            this.txtPresupuestoFinal.Text = "0.0";
            this.txtPresupuestoFinal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPresupeustoFinal
            // 
            this.lblPresupeustoFinal.AutoSize = true;
            this.lblPresupeustoFinal.Location = new System.Drawing.Point(29, 159);
            this.lblPresupeustoFinal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPresupeustoFinal.Name = "lblPresupeustoFinal";
            this.lblPresupeustoFinal.Size = new System.Drawing.Size(207, 17);
            this.lblPresupeustoFinal.TabIndex = 4;
            this.lblPresupeustoFinal.Text = "Presupuesto de Cobranza Final";
            // 
            // txtPorcentaje
            // 
            this.txtPorcentaje.Enabled = false;
            this.txtPorcentaje.Location = new System.Drawing.Point(264, 123);
            this.txtPorcentaje.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPorcentaje.Name = "txtPorcentaje";
            this.txtPorcentaje.Size = new System.Drawing.Size(132, 22);
            this.txtPorcentaje.TabIndex = 3;
            this.txtPorcentaje.Text = "52";
            this.txtPorcentaje.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPorcentaje
            // 
            this.lblPorcentaje.AutoSize = true;
            this.lblPorcentaje.Location = new System.Drawing.Point(29, 127);
            this.lblPorcentaje.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPorcentaje.Name = "lblPorcentaje";
            this.lblPorcentaje.Size = new System.Drawing.Size(20, 17);
            this.lblPorcentaje.TabIndex = 2;
            this.lblPorcentaje.Text = "%";
            // 
            // txtPresupuesto
            // 
            this.txtPresupuesto.Location = new System.Drawing.Point(264, 91);
            this.txtPresupuesto.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPresupuesto.Name = "txtPresupuesto";
            this.txtPresupuesto.Size = new System.Drawing.Size(132, 22);
            this.txtPresupuesto.TabIndex = 1;
            this.txtPresupuesto.Text = "0.0";
            this.txtPresupuesto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPresupuesto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPresupuesto_KeyPress);
            this.txtPresupuesto.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPresupuesto_KeyUp);
            this.txtPresupuesto.Leave += new System.EventHandler(this.txtPresupuesto_Leave);
            // 
            // lblPresupuestoCobranza
            // 
            this.lblPresupuestoCobranza.AutoSize = true;
            this.lblPresupuestoCobranza.Location = new System.Drawing.Point(29, 95);
            this.lblPresupuestoCobranza.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPresupuestoCobranza.Name = "lblPresupuestoCobranza";
            this.lblPresupuestoCobranza.Size = new System.Drawing.Size(173, 17);
            this.lblPresupuestoCobranza.TabIndex = 0;
            this.lblPresupuestoCobranza.Text = "Presupuesto de Cobranza";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtCriterio);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.lblProveedorTotal);
            this.panel1.Controls.Add(this.btnProveedorAgregar);
            this.panel1.Controls.Add(this.btnProveedorCerrar);
            this.panel1.Controls.Add(this.lblProveedorTitiloTotal);
            this.panel1.Controls.Add(this.cmbProveedores);
            this.panel1.Controls.Add(this.lblSubtitulo);
            this.panel1.Controls.Add(this.dgvArticulos);
            this.panel1.Controls.Add(this.lblCotizacionNombreProveedor);
            this.panel1.Location = new System.Drawing.Point(16, 96);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1119, 604);
            this.panel1.TabIndex = 3;
            this.panel1.Visible = false;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(469, 127);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 17);
            this.label2.TabIndex = 12;
            this.label2.Text = "Artículos";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(39, 98);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 17);
            this.label1.TabIndex = 11;
            this.label1.Text = "Criterio de búsqueda:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(43, 150);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.White;
            this.dataGridView1.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1051, 133);
            this.dataGridView1.TabIndex = 11;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEnter);
            this.dataGridView1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDoubleClick);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // lblProveedorTotal
            // 
            this.lblProveedorTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProveedorTotal.AutoSize = true;
            this.lblProveedorTotal.Location = new System.Drawing.Point(996, 528);
            this.lblProveedorTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProveedorTotal.Name = "lblProveedorTotal";
            this.lblProveedorTotal.Size = new System.Drawing.Size(28, 17);
            this.lblProveedorTotal.TabIndex = 8;
            this.lblProveedorTotal.Text = "0.0";
            // 
            // btnProveedorAgregar
            // 
            this.btnProveedorAgregar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProveedorAgregar.Location = new System.Drawing.Point(888, 561);
            this.btnProveedorAgregar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnProveedorAgregar.Name = "btnProveedorAgregar";
            this.btnProveedorAgregar.Size = new System.Drawing.Size(83, 30);
            this.btnProveedorAgregar.TabIndex = 7;
            this.btnProveedorAgregar.Text = "Guardar";
            this.btnProveedorAgregar.UseVisualStyleBackColor = true;
            this.btnProveedorAgregar.Click += new System.EventHandler(this.btnProveedorAgregar_Click);
            // 
            // btnProveedorCerrar
            // 
            this.btnProveedorCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProveedorCerrar.Location = new System.Drawing.Point(1021, 561);
            this.btnProveedorCerrar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnProveedorCerrar.Name = "btnProveedorCerrar";
            this.btnProveedorCerrar.Size = new System.Drawing.Size(73, 30);
            this.btnProveedorCerrar.TabIndex = 6;
            this.btnProveedorCerrar.Text = "Cerrar";
            this.btnProveedorCerrar.UseVisualStyleBackColor = true;
            this.btnProveedorCerrar.Click += new System.EventHandler(this.btnProveedorCerrar_Click);
            // 
            // lblProveedorTitiloTotal
            // 
            this.lblProveedorTitiloTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProveedorTitiloTotal.AutoSize = true;
            this.lblProveedorTitiloTotal.Location = new System.Drawing.Point(884, 528);
            this.lblProveedorTitiloTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProveedorTitiloTotal.Name = "lblProveedorTitiloTotal";
            this.lblProveedorTitiloTotal.Size = new System.Drawing.Size(44, 17);
            this.lblProveedorTitiloTotal.TabIndex = 5;
            this.lblProveedorTitiloTotal.Text = "Total:";
            // 
            // cmbProveedores
            // 
            this.cmbProveedores.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbProveedores.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProveedores.FormattingEnabled = true;
            this.cmbProveedores.Location = new System.Drawing.Point(107, 20);
            this.cmbProveedores.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbProveedores.Name = "cmbProveedores";
            this.cmbProveedores.Size = new System.Drawing.Size(982, 24);
            this.cmbProveedores.TabIndex = 4;
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Location = new System.Drawing.Point(417, 295);
            this.lblSubtitulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(156, 17);
            this.lblSubtitulo.TabIndex = 3;
            this.lblSubtitulo.Text = "Detalle de la Cotización";
            // 
            // dgvArticulos
            // 
            this.dgvArticulos.AllowUserToAddRows = false;
            this.dgvArticulos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvArticulos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvArticulos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PEDIDO,
            this.Cantidad,
            this.Descripcion,
            this.ULTIMOCOSTO,
            this.PRECIO,
            this.SUBTOTAL,
            this.IVA,
            this.TOTAL});
            this.dgvArticulos.Location = new System.Drawing.Point(43, 315);
            this.dgvArticulos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvArticulos.Name = "dgvArticulos";
            this.dgvArticulos.Size = new System.Drawing.Size(1049, 213);
            this.dgvArticulos.TabIndex = 2;
            this.dgvArticulos.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvArticulos_CellEndEdit);
            this.dgvArticulos.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvArticulos_CellFormatting);
            this.dgvArticulos.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvArticulos_CellValidating);
            this.dgvArticulos.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dgvArticulos_RowsRemoved);
            // 
            // lblCotizacionNombreProveedor
            // 
            this.lblCotizacionNombreProveedor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCotizacionNombreProveedor.AutoSize = true;
            this.lblCotizacionNombreProveedor.Location = new System.Drawing.Point(20, 25);
            this.lblCotizacionNombreProveedor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCotizacionNombreProveedor.Name = "lblCotizacionNombreProveedor";
            this.lblCotizacionNombreProveedor.Size = new System.Drawing.Size(78, 17);
            this.lblCotizacionNombreProveedor.TabIndex = 0;
            this.lblCotizacionNombreProveedor.Text = "Proveedor:";
            // 
            // lblSimulaciones
            // 
            this.lblSimulaciones.AutoSize = true;
            this.lblSimulaciones.Location = new System.Drawing.Point(20, 75);
            this.lblSimulaciones.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSimulaciones.Name = "lblSimulaciones";
            this.lblSimulaciones.Size = new System.Drawing.Size(163, 17);
            this.lblSimulaciones.TabIndex = 16;
            this.lblSimulaciones.Text = "Simulaciones anteriores:";
            // 
            // cmbSimulaciones
            // 
            this.cmbSimulaciones.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbSimulaciones.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbSimulaciones.FormattingEnabled = true;
            this.cmbSimulaciones.Location = new System.Drawing.Point(189, 71);
            this.cmbSimulaciones.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbSimulaciones.Name = "cmbSimulaciones";
            this.cmbSimulaciones.Size = new System.Drawing.Size(820, 24);
            this.cmbSimulaciones.TabIndex = 17;
            // 
            // btnCargarSimulacion
            // 
            this.btnCargarSimulacion.Location = new System.Drawing.Point(1019, 69);
            this.btnCargarSimulacion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCargarSimulacion.Name = "btnCargarSimulacion";
            this.btnCargarSimulacion.Size = new System.Drawing.Size(100, 28);
            this.btnCargarSimulacion.TabIndex = 18;
            this.btnCargarSimulacion.Text = "Cargar";
            this.btnCargarSimulacion.UseVisualStyleBackColor = true;
            this.btnCargarSimulacion.Click += new System.EventHandler(this.btnCargarSimulacion_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Location = new System.Drawing.Point(536, 611);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(144, 34);
            this.btnEditar.TabIndex = 19;
            this.btnEditar.Text = "Editar proveedor";
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // txtCriterio
            // 
            this.txtCriterio.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCriterio.Location = new System.Drawing.Point(384, 97);
            this.txtCriterio.Margin = new System.Windows.Forms.Padding(4);
            this.txtCriterio.Name = "txtCriterio";
            this.txtCriterio.OnlyUpperCase = false;
            this.txtCriterio.Size = new System.Drawing.Size(705, 22);
            this.txtCriterio.TabIndex = 10;
            this.txtCriterio.TextChanged += new System.EventHandler(this.txtCriterio_TextChanged);
            // 
            // PEDIDO
            // 
            this.PEDIDO.HeaderText = "Pedido";
            this.PEDIDO.Name = "PEDIDO";
            this.PEDIDO.Width = 80;
            // 
            // Cantidad
            // 
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = null;
            this.Cantidad.DefaultCellStyle = dataGridViewCellStyle1;
            this.Cantidad.HeaderText = "Cant.";
            this.Cantidad.Name = "Cantidad";
            this.Cantidad.Width = 40;
            // 
            // Descripcion
            // 
            this.Descripcion.HeaderText = "Desc.";
            this.Descripcion.Name = "Descripcion";
            this.Descripcion.ReadOnly = true;
            this.Descripcion.Width = 270;
            // 
            // ULTIMOCOSTO
            // 
            this.ULTIMOCOSTO.HeaderText = "Ult. Cos.";
            this.ULTIMOCOSTO.Name = "ULTIMOCOSTO";
            this.ULTIMOCOSTO.ReadOnly = true;
            this.ULTIMOCOSTO.Width = 70;
            // 
            // PRECIO
            // 
            dataGridViewCellStyle2.Format = "C2";
            dataGridViewCellStyle2.NullValue = null;
            this.PRECIO.DefaultCellStyle = dataGridViewCellStyle2;
            this.PRECIO.HeaderText = "Cos.";
            this.PRECIO.Name = "PRECIO";
            this.PRECIO.Width = 90;
            // 
            // SUBTOTAL
            // 
            dataGridViewCellStyle3.Format = "C2";
            dataGridViewCellStyle3.NullValue = null;
            this.SUBTOTAL.DefaultCellStyle = dataGridViewCellStyle3;
            this.SUBTOTAL.HeaderText = "Subtotal";
            this.SUBTOTAL.Name = "SUBTOTAL";
            this.SUBTOTAL.ReadOnly = true;
            this.SUBTOTAL.Width = 90;
            // 
            // IVA
            // 
            dataGridViewCellStyle4.Format = "C2";
            dataGridViewCellStyle4.NullValue = null;
            this.IVA.DefaultCellStyle = dataGridViewCellStyle4;
            this.IVA.HeaderText = "IVA";
            this.IVA.Name = "IVA";
            this.IVA.ReadOnly = true;
            this.IVA.Visible = false;
            this.IVA.Width = 70;
            // 
            // TOTAL
            // 
            dataGridViewCellStyle5.Format = "C2";
            dataGridViewCellStyle5.NullValue = null;
            this.TOTAL.DefaultCellStyle = dataGridViewCellStyle5;
            this.TOTAL.HeaderText = "Total";
            this.TOTAL.Name = "TOTAL";
            this.TOTAL.ReadOnly = true;
            this.TOTAL.Width = 90;
            // 
            // frmSimuladorCompras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1148, 788);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnCargarSimulacion);
            this.Controls.Add(this.cmbSimulaciones);
            this.Controls.Add(this.lblSimulaciones);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmSimuladorCompras";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Simulador de Compras";
            this.Load += new System.EventHandler(this.frmSimuladorCompras_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvArticulos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtPorcentaje;
        private System.Windows.Forms.Label lblPorcentaje;
        private System.Windows.Forms.TextBox txtPresupuesto;
        private System.Windows.Forms.Label lblPresupuestoCobranza;
        private System.Windows.Forms.Button btnAgregarProveedor;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.TextBox txtDiferencia;
        private System.Windows.Forms.Label lblDiferencia;
        private System.Windows.Forms.TextBox txtSubtotal;
        private System.Windows.Forms.Label lblSubotal;
        private System.Windows.Forms.TextBox txtPresupuestoFinal;
        private System.Windows.Forms.Label lblPresupeustoFinal;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblCotizacionNombreProveedor;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.DataGridView dgvArticulos;
        private System.Windows.Forms.Label lblProveedorTotal;
        private System.Windows.Forms.Button btnProveedorAgregar;
        private System.Windows.Forms.Button btnProveedorCerrar;
        private System.Windows.Forms.Label lblProveedorTitiloTotal;
        private System.Windows.Forms.ComboBox cmbProveedores;
        private System.Windows.Forms.Label lblSimulaciones;
        private System.Windows.Forms.ComboBox cmbSimulaciones;
        private System.Windows.Forms.Button btnCargarSimulacion;
        private System.Windows.Forms.DataGridView dataGridView1;
        private UserControls.TextBoxEx txtCriterio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnNuevoSimulador;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.DataGridViewTextBoxColumn PEDIDO;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn ULTIMOCOSTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRECIO;
        private System.Windows.Forms.DataGridViewTextBoxColumn SUBTOTAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn IVA;
        private System.Windows.Forms.DataGridViewTextBoxColumn TOTAL;
    }
}