namespace SIP
{
    partial class AddPartidasPedi2
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDescuento = new SIP.UserControls.TextBoxEx();
            this.lblDescuento = new System.Windows.Forms.Label();
            this.txtPrecio = new SIP.UserControls.NumericTextBox();
            this.txtModelo = new SIP.UserControls.TextBoxEx();
            this.lblProcesando = new System.Windows.Forms.Label();
            this.lblModeloDescripcion = new System.Windows.Forms.Label();
            this.txtProcesos = new SIP.UserControls.TextBoxEx();
            this.txtPedido = new SIP.UserControls.TextBoxEx();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnProcesar = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgViewProcesos = new System.Windows.Forms.DataGridView();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Que = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Como = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Donde = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgViewTallas3 = new System.Windows.Forms.DataGridView();
            this.dgViewTallas2 = new System.Windows.Forms.DataGridView();
            this.dgViewTallas1 = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblTotalPrendas = new System.Windows.Forms.Label();
            this.lblPrecioConsolidado = new System.Windows.Forms.Label();
            this.scape1 = new SIP.UserControls.Scape();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewProcesos)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtDescuento);
            this.groupBox1.Controls.Add(this.lblDescuento);
            this.groupBox1.Controls.Add(this.txtPrecio);
            this.groupBox1.Controls.Add(this.txtModelo);
            this.groupBox1.Controls.Add(this.lblProcesando);
            this.groupBox1.Controls.Add(this.lblModeloDescripcion);
            this.groupBox1.Controls.Add(this.txtProcesos);
            this.groupBox1.Controls.Add(this.txtPedido);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(17, 16);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1361, 102);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Generales";
            // 
            // txtDescuento
            // 
            this.txtDescuento.Location = new System.Drawing.Point(603, 66);
            this.txtDescuento.Margin = new System.Windows.Forms.Padding(4);
            this.txtDescuento.MaxLength = 10;
            this.txtDescuento.Name = "txtDescuento";
            this.txtDescuento.OnlyUpperCase = true;
            this.txtDescuento.Size = new System.Drawing.Size(132, 22);
            this.txtDescuento.TabIndex = 12;
            // 
            // lblDescuento
            // 
            this.lblDescuento.AutoSize = true;
            this.lblDescuento.Location = new System.Drawing.Point(515, 69);
            this.lblDescuento.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDescuento.Name = "lblDescuento";
            this.lblDescuento.Size = new System.Drawing.Size(80, 17);
            this.lblDescuento.TabIndex = 13;
            this.lblDescuento.Text = "Descuento:";
            // 
            // txtPrecio
            // 
            this.txtPrecio.Location = new System.Drawing.Point(88, 65);
            this.txtPrecio.Margin = new System.Windows.Forms.Padding(4);
            this.txtPrecio.MaxValue = 0;
            this.txtPrecio.MinValue = 0;
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.NumberType = SIP.UserControls.TipoDeNumero.Decimal;
            this.txtPrecio.Size = new System.Drawing.Size(132, 22);
            this.txtPrecio.TabIndex = 2;
            this.txtPrecio.TextChanged += new System.EventHandler(this.txtPrecio_TextChanged);
            // 
            // txtModelo
            // 
            this.txtModelo.Location = new System.Drawing.Point(363, 31);
            this.txtModelo.Margin = new System.Windows.Forms.Padding(4);
            this.txtModelo.Name = "txtModelo";
            this.txtModelo.OnlyUpperCase = true;
            this.txtModelo.Size = new System.Drawing.Size(132, 22);
            this.txtModelo.TabIndex = 1;
            this.txtModelo.TextChanged += new System.EventHandler(this.txtModelo_TextChanged);
            // 
            // lblProcesando
            // 
            this.lblProcesando.AutoSize = true;
            this.lblProcesando.Location = new System.Drawing.Point(368, 11);
            this.lblProcesando.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProcesando.Name = "lblProcesando";
            this.lblProcesando.Size = new System.Drawing.Size(96, 17);
            this.lblProcesando.TabIndex = 5;
            this.lblProcesando.Text = "Procesando...";
            this.lblProcesando.Visible = false;
            // 
            // lblModeloDescripcion
            // 
            this.lblModeloDescripcion.Location = new System.Drawing.Point(504, 32);
            this.lblModeloDescripcion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblModeloDescripcion.Name = "lblModeloDescripcion";
            this.lblModeloDescripcion.Size = new System.Drawing.Size(416, 37);
            this.lblModeloDescripcion.TabIndex = 4;
            // 
            // txtProcesos
            // 
            this.txtProcesos.Enabled = false;
            this.txtProcesos.Location = new System.Drawing.Point(363, 65);
            this.txtProcesos.Margin = new System.Windows.Forms.Padding(4);
            this.txtProcesos.MaxLength = 10;
            this.txtProcesos.Name = "txtProcesos";
            this.txtProcesos.OnlyUpperCase = true;
            this.txtProcesos.Size = new System.Drawing.Size(132, 22);
            this.txtProcesos.TabIndex = 3;
            this.txtProcesos.TextChanged += new System.EventHandler(this.txtProcesos_TextChanged);
            // 
            // txtPedido
            // 
            this.txtPedido.Enabled = false;
            this.txtPedido.Location = new System.Drawing.Point(88, 30);
            this.txtPedido.Margin = new System.Windows.Forms.Padding(4);
            this.txtPedido.Name = "txtPedido";
            this.txtPedido.OnlyUpperCase = false;
            this.txtPedido.Size = new System.Drawing.Size(132, 22);
            this.txtPedido.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(295, 69);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "Procesos:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(295, 33);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Modelo:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 69);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Precio:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 33);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pedido:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1169, 837);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 28);
            this.button1.TabIndex = 9;
            this.button1.Text = "Cancelar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnProcesar
            // 
            this.btnProcesar.Enabled = false;
            this.btnProcesar.Location = new System.Drawing.Point(1277, 837);
            this.btnProcesar.Margin = new System.Windows.Forms.Padding(4);
            this.btnProcesar.Name = "btnProcesar";
            this.btnProcesar.Size = new System.Drawing.Size(100, 28);
            this.btnProcesar.TabIndex = 8;
            this.btnProcesar.Text = "Procesar";
            this.btnProcesar.UseVisualStyleBackColor = true;
            this.btnProcesar.Click += new System.EventHandler(this.btnProcesar_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgViewProcesos);
            this.groupBox2.Location = new System.Drawing.Point(17, 118);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(1361, 236);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Descripción de procesos";
            // 
            // dgViewProcesos
            // 
            this.dgViewProcesos.AllowUserToAddRows = false;
            this.dgViewProcesos.AllowUserToDeleteRows = false;
            this.dgViewProcesos.AllowUserToResizeColumns = false;
            this.dgViewProcesos.AllowUserToResizeRows = false;
            this.dgViewProcesos.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgViewProcesos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgViewProcesos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dgViewProcesos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgViewProcesos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgViewProcesos.ColumnHeadersVisible = false;
            this.dgViewProcesos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Nombre,
            this.Que,
            this.Como,
            this.Donde,
            this.Precio});
            this.dgViewProcesos.Enabled = false;
            this.dgViewProcesos.Location = new System.Drawing.Point(111, 25);
            this.dgViewProcesos.Margin = new System.Windows.Forms.Padding(4);
            this.dgViewProcesos.MultiSelect = false;
            this.dgViewProcesos.Name = "dgViewProcesos";
            this.dgViewProcesos.RowHeadersVisible = false;
            this.dgViewProcesos.Size = new System.Drawing.Size(1225, 204);
            this.dgViewProcesos.TabIndex = 4;
            this.dgViewProcesos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgViewProcesos_CellContentClick);
            this.dgViewProcesos.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgViewProcesos_CellEndEdit);
            this.dgViewProcesos.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgViewProcesos_CellEnter);
            this.dgViewProcesos.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgViewProcesos_CellPainting);
            this.dgViewProcesos.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgViewProcesos_CellValidating);
            this.dgViewProcesos.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgViewProcesos_CurrentCellDirtyStateChanged);
            this.dgViewProcesos.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgViewProcesos_DataError);
            this.dgViewProcesos.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgViewProcesos_EditingControlShowing);
            this.dgViewProcesos.Leave += new System.EventHandler(this.dgViewProcesos_Leave);
            // 
            // Nombre
            // 
            this.Nombre.DataPropertyName = "NombreProceso";
            this.Nombre.HeaderText = "Nombre";
            this.Nombre.Name = "Nombre";
            // 
            // Que
            // 
            this.Que.DataPropertyName = "Que";
            this.Que.HeaderText = "Column1";
            this.Que.Name = "Que";
            // 
            // Como
            // 
            this.Como.DataPropertyName = "Como";
            this.Como.HeaderText = "Column2";
            this.Como.Name = "Como";
            // 
            // Donde
            // 
            this.Donde.DataPropertyName = "Donde";
            this.Donde.HeaderText = "Column3";
            this.Donde.Name = "Donde";
            // 
            // Precio
            // 
            this.Precio.DataPropertyName = "Precio";
            dataGridViewCellStyle1.Format = "C2";
            dataGridViewCellStyle1.NullValue = null;
            this.Precio.DefaultCellStyle = dataGridViewCellStyle1;
            this.Precio.HeaderText = "Column4";
            this.Precio.Name = "Precio";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox3.Controls.Add(this.dgViewTallas3);
            this.groupBox3.Controls.Add(this.dgViewTallas2);
            this.groupBox3.Controls.Add(this.dgViewTallas1);
            this.groupBox3.Location = new System.Drawing.Point(17, 353);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(1361, 476);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tallas";
            // 
            // dgViewTallas3
            // 
            this.dgViewTallas3.AllowUserToAddRows = false;
            this.dgViewTallas3.AllowUserToDeleteRows = false;
            this.dgViewTallas3.AllowUserToResizeColumns = false;
            this.dgViewTallas3.AllowUserToResizeRows = false;
            this.dgViewTallas3.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgViewTallas3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgViewTallas3.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgViewTallas3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgViewTallas3.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgViewTallas3.Location = new System.Drawing.Point(69, 233);
            this.dgViewTallas3.Margin = new System.Windows.Forms.Padding(4);
            this.dgViewTallas3.MultiSelect = false;
            this.dgViewTallas3.Name = "dgViewTallas3";
            this.dgViewTallas3.RowHeadersVisible = false;
            this.dgViewTallas3.Size = new System.Drawing.Size(1183, 97);
            this.dgViewTallas3.TabIndex = 7;
            this.dgViewTallas3.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgViewTallas3_CellEndEdit);
            this.dgViewTallas3.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgViewTallas3_ColumnAdded);
            this.dgViewTallas3.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgViewTallas3_DataError);
            this.dgViewTallas3.Leave += new System.EventHandler(this.dgViewTallas3_Leave);
            // 
            // dgViewTallas2
            // 
            this.dgViewTallas2.AllowUserToAddRows = false;
            this.dgViewTallas2.AllowUserToDeleteRows = false;
            this.dgViewTallas2.AllowUserToResizeColumns = false;
            this.dgViewTallas2.AllowUserToResizeRows = false;
            this.dgViewTallas2.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgViewTallas2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgViewTallas2.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgViewTallas2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgViewTallas2.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgViewTallas2.Location = new System.Drawing.Point(69, 128);
            this.dgViewTallas2.Margin = new System.Windows.Forms.Padding(4);
            this.dgViewTallas2.MultiSelect = false;
            this.dgViewTallas2.Name = "dgViewTallas2";
            this.dgViewTallas2.RowHeadersVisible = false;
            this.dgViewTallas2.Size = new System.Drawing.Size(1183, 97);
            this.dgViewTallas2.TabIndex = 6;
            this.dgViewTallas2.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgViewTallas2_CellEndEdit);
            this.dgViewTallas2.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgViewTallas2_ColumnAdded);
            this.dgViewTallas2.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgViewTallas2_DataError);
            this.dgViewTallas2.Leave += new System.EventHandler(this.dgViewTallas2_Leave);
            // 
            // dgViewTallas1
            // 
            this.dgViewTallas1.AllowUserToAddRows = false;
            this.dgViewTallas1.AllowUserToDeleteRows = false;
            this.dgViewTallas1.AllowUserToResizeColumns = false;
            this.dgViewTallas1.AllowUserToResizeRows = false;
            this.dgViewTallas1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgViewTallas1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgViewTallas1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dgViewTallas1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgViewTallas1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgViewTallas1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgViewTallas1.Location = new System.Drawing.Point(75, 23);
            this.dgViewTallas1.Margin = new System.Windows.Forms.Padding(4);
            this.dgViewTallas1.MultiSelect = false;
            this.dgViewTallas1.Name = "dgViewTallas1";
            this.dgViewTallas1.RowHeadersVisible = false;
            this.dgViewTallas1.Size = new System.Drawing.Size(1183, 97);
            this.dgViewTallas1.TabIndex = 5;
            this.dgViewTallas1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgViewTallas1_CellEndEdit);
            this.dgViewTallas1.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgViewTallas1_ColumnAdded);
            this.dgViewTallas1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgViewTallas1_DataError);
            this.dgViewTallas1.Leave += new System.EventHandler(this.dgViewTallas1_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 846);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 17);
            this.label5.TabIndex = 5;
            this.label5.Text = "Total de prendas:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(303, 846);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(132, 17);
            this.label6.TabIndex = 6;
            this.label6.Text = "Precio consolidado:";
            // 
            // lblTotalPrendas
            // 
            this.lblTotalPrendas.AutoSize = true;
            this.lblTotalPrendas.Location = new System.Drawing.Point(143, 846);
            this.lblTotalPrendas.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalPrendas.Name = "lblTotalPrendas";
            this.lblTotalPrendas.Size = new System.Drawing.Size(0, 17);
            this.lblTotalPrendas.TabIndex = 10;
            // 
            // lblPrecioConsolidado
            // 
            this.lblPrecioConsolidado.AutoSize = true;
            this.lblPrecioConsolidado.Location = new System.Drawing.Point(479, 846);
            this.lblPrecioConsolidado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPrecioConsolidado.Name = "lblPrecioConsolidado";
            this.lblPrecioConsolidado.Size = new System.Drawing.Size(0, 17);
            this.lblPrecioConsolidado.TabIndex = 11;
            // 
            // scape1
            // 
            this.scape1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.scape1.Form = this;
            // 
            // AddPartidasPedi2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.scape1;
            this.ClientSize = new System.Drawing.Size(1397, 880);
            this.Controls.Add(this.lblPrecioConsolidado);
            this.Controls.Add(this.lblTotalPrendas);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnProcesar);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddPartidasPedi2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nuevas Partidas";
            this.Activated += new System.EventHandler(this.AddPartidasPedi_Activated);
            this.Load += new System.EventHandler(this.AddPartidasPedi_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgViewProcesos)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private SIP.UserControls.TextBoxEx txtProcesos;
        private SIP.UserControls.TextBoxEx txtPedido;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnProcesar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgViewProcesos;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblModeloDescripcion;
        private System.Windows.Forms.Label lblTotalPrendas;
        private System.Windows.Forms.Label lblPrecioConsolidado;
        private System.Windows.Forms.Label lblProcesando;
        private UserControls.TextBoxEx txtModelo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn Que;
        private System.Windows.Forms.DataGridViewTextBoxColumn Como;
        private System.Windows.Forms.DataGridViewTextBoxColumn Donde;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precio;
        private UserControls.Scape scape1;
        private UserControls.NumericTextBox txtPrecio;
        private System.Windows.Forms.DataGridView dgViewTallas3;
        private System.Windows.Forms.DataGridView dgViewTallas2;
        private System.Windows.Forms.DataGridView dgViewTallas1;
        private UserControls.TextBoxEx txtDescuento;
        private System.Windows.Forms.Label lblDescuento;
    }
}