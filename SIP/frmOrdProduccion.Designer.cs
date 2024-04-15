namespace SIP
{
    partial class frmOrdProduccion
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtFechaVencimiento = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dgViewTallas2 = new System.Windows.Forms.DataGridView();
            this.dgViewTallas1 = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgViewTallas3 = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnProcesar = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.txtObservaciones = new SIP.UserControls.TextBoxEx();
            this.txtPedido = new SIP.UserControls.TextBoxEx();
            this.txtModelo = new SIP.UserControls.TextBoxEx();
            this.txtAlmacen = new SIP.UserControls.NumericTextBox();
            this.txtNumReferencia = new SIP.UserControls.NumericTextBox();
            this.dgViewTallas4 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas1)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas4)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Referencia (Orden)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(207, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Almacén";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(304, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Fch. Venc.";
            // 
            // dtFechaVencimiento
            // 
            this.dtFechaVencimiento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFechaVencimiento.Location = new System.Drawing.Point(382, 6);
            this.dtFechaVencimiento.Name = "dtFechaVencimiento";
            this.dtFechaVencimiento.Size = new System.Drawing.Size(98, 20);
            this.dtFechaVencimiento.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Modelo";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(141, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Pedido";
            this.label5.Visible = false;
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgViewTallas2.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgViewTallas2.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgViewTallas2.Location = new System.Drawing.Point(6, 68);
            this.dgViewTallas2.MultiSelect = false;
            this.dgViewTallas2.Name = "dgViewTallas2";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgViewTallas2.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgViewTallas2.RowHeadersVisible = false;
            this.dgViewTallas2.Size = new System.Drawing.Size(592, 46);
            this.dgViewTallas2.TabIndex = 6;
            this.dgViewTallas2.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgViewTallas_CellValueChanged);
            this.dgViewTallas2.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgViewTallas2_ColumnAdded);
            this.dgViewTallas2.Enter += new System.EventHandler(this.dgViewTallas2_Enter);
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
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgViewTallas1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgViewTallas1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgViewTallas1.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgViewTallas1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgViewTallas1.Location = new System.Drawing.Point(6, 19);
            this.dgViewTallas1.MultiSelect = false;
            this.dgViewTallas1.Name = "dgViewTallas1";
            this.dgViewTallas1.RowHeadersVisible = false;
            this.dgViewTallas1.Size = new System.Drawing.Size(592, 46);
            this.dgViewTallas1.TabIndex = 5;
            this.dgViewTallas1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgViewTallas_CellValueChanged);
            this.dgViewTallas1.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgViewTallas1_ColumnAdded);
            this.dgViewTallas1.Enter += new System.EventHandler(this.dgViewTallas1_Enter);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox3.Controls.Add(this.dgViewTallas4);
            this.groupBox3.Controls.Add(this.dgViewTallas3);
            this.groupBox3.Controls.Add(this.dgViewTallas2);
            this.groupBox3.Controls.Add(this.dgViewTallas1);
            this.groupBox3.Location = new System.Drawing.Point(12, 74);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(660, 227);
            this.groupBox3.TabIndex = 10;
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
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgViewTallas3.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgViewTallas3.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgViewTallas3.Location = new System.Drawing.Point(6, 117);
            this.dgViewTallas3.MultiSelect = false;
            this.dgViewTallas3.Name = "dgViewTallas3";
            this.dgViewTallas3.RowHeadersVisible = false;
            this.dgViewTallas3.Size = new System.Drawing.Size(592, 46);
            this.dgViewTallas3.TabIndex = 12;
            this.dgViewTallas3.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgViewTallas_CellValueChanged);
            this.dgViewTallas3.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgViewTallas3_ColumnAdded);
            this.dgViewTallas3.Enter += new System.EventHandler(this.dgViewTallas3_Enter);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 304);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Observaciones";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(415, 326);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Total :";
            // 
            // btnProcesar
            // 
            this.btnProcesar.Location = new System.Drawing.Point(415, 387);
            this.btnProcesar.Name = "btnProcesar";
            this.btnProcesar.Size = new System.Drawing.Size(75, 23);
            this.btnProcesar.TabIndex = 14;
            this.btnProcesar.Text = "Procesar";
            this.btnProcesar.UseVisualStyleBackColor = true;
            this.btnProcesar.Click += new System.EventHandler(this.btnProcesar_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(450, 326);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(13, 13);
            this.lblTotal.TabIndex = 15;
            this.lblTotal.Text = "0";
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.AutoTABOnKeyDown = false;
            this.txtObservaciones.AutoTABOnKeyUp = false;
            this.txtObservaciones.Location = new System.Drawing.Point(18, 323);
            this.txtObservaciones.MaxLength = 244;
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.OnlyUpperCase = true;
            this.txtObservaciones.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtObservaciones.Size = new System.Drawing.Size(391, 87);
            this.txtObservaciones.TabIndex = 11;
            // 
            // txtPedido
            // 
            this.txtPedido.Location = new System.Drawing.Point(189, 37);
            this.txtPedido.Name = "txtPedido";
            this.txtPedido.OnlyUpperCase = false;
            this.txtPedido.Size = new System.Drawing.Size(78, 20);
            this.txtPedido.TabIndex = 9;
            this.txtPedido.Visible = false;
            // 
            // txtModelo
            // 
            this.txtModelo.Location = new System.Drawing.Point(57, 37);
            this.txtModelo.Name = "txtModelo";
            this.txtModelo.OnlyUpperCase = true;
            this.txtModelo.Size = new System.Drawing.Size(78, 20);
            this.txtModelo.TabIndex = 7;
            this.txtModelo.Leave += new System.EventHandler(this.txtModelo_Leave);
            // 
            // txtAlmacen
            // 
            this.txtAlmacen.Location = new System.Drawing.Point(261, 6);
            this.txtAlmacen.MaxValue = 0;
            this.txtAlmacen.MinValue = 0;
            this.txtAlmacen.Name = "txtAlmacen";
            this.txtAlmacen.Size = new System.Drawing.Size(24, 20);
            this.txtAlmacen.TabIndex = 4;
            this.txtAlmacen.Text = "1";
            this.txtAlmacen.Leave += new System.EventHandler(this.txtAlmacen_Leave);
            // 
            // txtNumReferencia
            // 
            this.txtNumReferencia.Location = new System.Drawing.Point(118, 6);
            this.txtNumReferencia.MaxValue = 0;
            this.txtNumReferencia.MinValue = 0;
            this.txtNumReferencia.Name = "txtNumReferencia";
            this.txtNumReferencia.Size = new System.Drawing.Size(75, 20);
            this.txtNumReferencia.TabIndex = 3;
            this.txtNumReferencia.Leave += new System.EventHandler(this.txtNumReferencia_Leave);
            // 
            // dgViewTallas4
            // 
            this.dgViewTallas4.AllowUserToAddRows = false;
            this.dgViewTallas4.AllowUserToDeleteRows = false;
            this.dgViewTallas4.AllowUserToResizeColumns = false;
            this.dgViewTallas4.AllowUserToResizeRows = false;
            this.dgViewTallas4.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgViewTallas4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgViewTallas4.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgViewTallas4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgViewTallas4.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgViewTallas4.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgViewTallas4.Location = new System.Drawing.Point(6, 169);
            this.dgViewTallas4.MultiSelect = false;
            this.dgViewTallas4.Name = "dgViewTallas4";
            this.dgViewTallas4.RowHeadersVisible = false;
            this.dgViewTallas4.Size = new System.Drawing.Size(592, 46);
            this.dgViewTallas4.TabIndex = 13;
            this.dgViewTallas4.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgViewTallas_CellValueChanged);
            this.dgViewTallas4.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgViewTallas4_ColumnAdded);
            this.dgViewTallas4.Enter += new System.EventHandler(this.dgViewTallas4_Enter);
            // 
            // frmOrdProduccion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 424);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.btnProcesar);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtObservaciones);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.txtPedido);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtModelo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtFechaVencimiento);
            this.Controls.Add(this.txtAlmacen);
            this.Controls.Add(this.txtNumReferencia);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOrdProduccion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generar Órdenes de Producción de Línea";
            this.Load += new System.EventHandler(this.frmOrdProduccion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private UserControls.NumericTextBox txtNumReferencia;
        private UserControls.NumericTextBox txtAlmacen;
        private System.Windows.Forms.DateTimePicker dtFechaVencimiento;
        private System.Windows.Forms.Label label4;
        private UserControls.TextBoxEx txtModelo;
        private UserControls.TextBoxEx txtPedido;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgViewTallas2;
        private System.Windows.Forms.DataGridView dgViewTallas1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgViewTallas3;
        private UserControls.TextBoxEx txtObservaciones;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnProcesar;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.DataGridView dgViewTallas4;
    }
}