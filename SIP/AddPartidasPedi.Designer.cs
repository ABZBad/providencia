namespace SIP
{
    partial class AddPartidasPedi
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
            this.dgViewTallas2 = new System.Windows.Forms.DataGridView();
            this.dgViewTallas1 = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dgViewTallas3 = new System.Windows.Forms.DataGridView();
            this.lblTotalPrendas = new System.Windows.Forms.Label();
            this.lblPrecioConsolidado = new System.Windows.Forms.Label();
            this.scape1 = new SIP.UserControls.Scape();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewProcesos)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas3)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
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
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(696, 83);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Generales";
            // 
            // txtPrecio
            // 
            this.txtPrecio.Location = new System.Drawing.Point(66, 53);
            this.txtPrecio.MaxValue = 0;
            this.txtPrecio.MinValue = 0;
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.NumberType = SIP.UserControls.TipoDeNumero.Decimal;
            this.txtPrecio.Size = new System.Drawing.Size(100, 20);
            this.txtPrecio.TabIndex = 2;
            this.txtPrecio.TextChanged += new System.EventHandler(this.txtPrecio_TextChanged);
            // 
            // txtModelo
            // 
            this.txtModelo.Location = new System.Drawing.Point(272, 25);
            this.txtModelo.Name = "txtModelo";
            this.txtModelo.OnlyUpperCase = true;
            this.txtModelo.Size = new System.Drawing.Size(100, 20);
            this.txtModelo.TabIndex = 1;
            this.txtModelo.TextChanged += new System.EventHandler(this.txtModelo_TextChanged);
            // 
            // lblProcesando
            // 
            this.lblProcesando.AutoSize = true;
            this.lblProcesando.Location = new System.Drawing.Point(276, 9);
            this.lblProcesando.Name = "lblProcesando";
            this.lblProcesando.Size = new System.Drawing.Size(73, 13);
            this.lblProcesando.TabIndex = 5;
            this.lblProcesando.Text = "Procesando...";
            this.lblProcesando.Visible = false;
            // 
            // lblModeloDescripcion
            // 
            this.lblModeloDescripcion.Location = new System.Drawing.Point(378, 26);
            this.lblModeloDescripcion.Name = "lblModeloDescripcion";
            this.lblModeloDescripcion.Size = new System.Drawing.Size(312, 30);
            this.lblModeloDescripcion.TabIndex = 4;
            // 
            // txtProcesos
            // 
            this.txtProcesos.Enabled = false;
            this.txtProcesos.Location = new System.Drawing.Point(272, 53);
            this.txtProcesos.MaxLength = 10;
            this.txtProcesos.Name = "txtProcesos";
            this.txtProcesos.OnlyUpperCase = true;
            this.txtProcesos.Size = new System.Drawing.Size(100, 20);
            this.txtProcesos.TabIndex = 3;
            this.txtProcesos.TextChanged += new System.EventHandler(this.txtProcesos_TextChanged);
            // 
            // txtPedido
            // 
            this.txtPedido.Enabled = false;
            this.txtPedido.Location = new System.Drawing.Point(66, 24);
            this.txtPedido.Name = "txtPedido";
            this.txtPedido.OnlyUpperCase = false;
            this.txtPedido.Size = new System.Drawing.Size(100, 20);
            this.txtPedido.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(221, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Procesos:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(221, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Modelo:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Precio:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pedido:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(553, 471);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Cancelar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnProcesar
            // 
            this.btnProcesar.Enabled = false;
            this.btnProcesar.Location = new System.Drawing.Point(634, 471);
            this.btnProcesar.Name = "btnProcesar";
            this.btnProcesar.Size = new System.Drawing.Size(75, 23);
            this.btnProcesar.TabIndex = 8;
            this.btnProcesar.Text = "Procesar";
            this.btnProcesar.UseVisualStyleBackColor = true;
            this.btnProcesar.Click += new System.EventHandler(this.btnProcesar_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgViewProcesos);
            this.groupBox2.Location = new System.Drawing.Point(13, 96);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(696, 192);
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
            this.dgViewProcesos.Location = new System.Drawing.Point(83, 20);
            this.dgViewProcesos.MultiSelect = false;
            this.dgViewProcesos.Name = "dgViewProcesos";
            this.dgViewProcesos.RowHeadersVisible = false;
            this.dgViewProcesos.Size = new System.Drawing.Size(526, 166);
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
            this.groupBox3.Controls.Add(this.dgViewTallas2);
            this.groupBox3.Controls.Add(this.dgViewTallas1);
            this.groupBox3.Location = new System.Drawing.Point(13, 287);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(696, 171);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tallas";
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
            this.dgViewTallas2.Location = new System.Drawing.Point(56, 68);
            this.dgViewTallas2.MultiSelect = false;
            this.dgViewTallas2.Name = "dgViewTallas2";
            this.dgViewTallas2.RowHeadersVisible = false;
            this.dgViewTallas2.Size = new System.Drawing.Size(592, 46);
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
            this.dgViewTallas1.Location = new System.Drawing.Point(56, 19);
            this.dgViewTallas1.MultiSelect = false;
            this.dgViewTallas1.Name = "dgViewTallas1";
            this.dgViewTallas1.RowHeadersVisible = false;
            this.dgViewTallas1.Size = new System.Drawing.Size(592, 46);
            this.dgViewTallas1.TabIndex = 5;
            this.dgViewTallas1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgViewTallas1_CellEndEdit);
            this.dgViewTallas1.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgViewTallas1_ColumnAdded);
            this.dgViewTallas1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgViewTallas1_DataError);
            this.dgViewTallas1.Leave += new System.EventHandler(this.dgViewTallas1_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 476);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Total de prendas:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(227, 476);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Precio consolidado:";
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
            this.dgViewTallas3.Location = new System.Drawing.Point(69, 405);
            this.dgViewTallas3.MultiSelect = false;
            this.dgViewTallas3.Name = "dgViewTallas3";
            this.dgViewTallas3.RowHeadersVisible = false;
            this.dgViewTallas3.Size = new System.Drawing.Size(592, 46);
            this.dgViewTallas3.TabIndex = 7;
            this.dgViewTallas3.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgViewTallas3_CellEndEdit);
            this.dgViewTallas3.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgViewTallas3_ColumnAdded);
            this.dgViewTallas3.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgViewTallas3_DataError);
            this.dgViewTallas3.Leave += new System.EventHandler(this.dgViewTallas3_Leave);
            // 
            // lblTotalPrendas
            // 
            this.lblTotalPrendas.AutoSize = true;
            this.lblTotalPrendas.Location = new System.Drawing.Point(107, 476);
            this.lblTotalPrendas.Name = "lblTotalPrendas";
            this.lblTotalPrendas.Size = new System.Drawing.Size(0, 13);
            this.lblTotalPrendas.TabIndex = 10;
            // 
            // lblPrecioConsolidado
            // 
            this.lblPrecioConsolidado.AutoSize = true;
            this.lblPrecioConsolidado.Location = new System.Drawing.Point(333, 476);
            this.lblPrecioConsolidado.Name = "lblPrecioConsolidado";
            this.lblPrecioConsolidado.Size = new System.Drawing.Size(0, 13);
            this.lblPrecioConsolidado.TabIndex = 11;
            // 
            // scape1
            // 
            this.scape1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.scape1.Form = this;
            // 
            // AddPartidasPedi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.scape1;
            this.ClientSize = new System.Drawing.Size(721, 504);
            this.Controls.Add(this.lblPrecioConsolidado);
            this.Controls.Add(this.lblTotalPrendas);
            this.Controls.Add(this.dgViewTallas3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnProcesar);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddPartidasPedi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nuevas Partidas";
            this.Activated += new System.EventHandler(this.AddPartidasPedi_Activated);
            this.Load += new System.EventHandler(this.AddPartidasPedi_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgViewProcesos)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas3)).EndInit();
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
        private System.Windows.Forms.DataGridView dgViewTallas2;
        private System.Windows.Forms.DataGridView dgViewTallas1;
        private System.Windows.Forms.DataGridView dgViewTallas3;
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
    }
}