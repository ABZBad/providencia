namespace SIP
{
    partial class frmOrdMaquila
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.txtOM = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPrefijoModelo = new System.Windows.Forms.TextBox();
            this.txtModelo = new SIP.UserControls.TextBoxEx();
            this.label4 = new System.Windows.Forms.Label();
            this.lblNombreProveedor = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgViewTallas4 = new System.Windows.Forms.DataGridView();
            this.dgViewTallas3 = new System.Windows.Forms.DataGridView();
            this.dgViewTallas2 = new System.Windows.Forms.DataGridView();
            this.dgViewTallas1 = new System.Windows.Forms.DataGridView();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnProcesar = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtEsquemaImpuesto = new SIP.UserControls.NumericTextBox();
            this.txtCosto = new SIP.UserControls.NumericTextBox();
            this.txtClaveProveedor = new SIP.UserControls.TextBoxEx();
            this.scape1 = new SIP.UserControls.Scape();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "OM";
            // 
            // txtOM
            // 
            this.txtOM.Enabled = false;
            this.txtOM.Location = new System.Drawing.Point(37, 8);
            this.txtOM.Name = "txtOM";
            this.txtOM.Size = new System.Drawing.Size(71, 20);
            this.txtOM.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(120, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Proveedor";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(277, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Modelo";
            // 
            // txtPrefijoModelo
            // 
            this.txtPrefijoModelo.BackColor = System.Drawing.SystemColors.Window;
            this.txtPrefijoModelo.Enabled = false;
            this.txtPrefijoModelo.Location = new System.Drawing.Point(325, 8);
            this.txtPrefijoModelo.Name = "txtPrefijoModelo";
            this.txtPrefijoModelo.ReadOnly = true;
            this.txtPrefijoModelo.Size = new System.Drawing.Size(17, 20);
            this.txtPrefijoModelo.TabIndex = 5;
            this.txtPrefijoModelo.Text = "X";
            // 
            // txtModelo
            // 
            this.txtModelo.BackColor = System.Drawing.SystemColors.Window;
            this.txtModelo.Location = new System.Drawing.Point(342, 8);
            this.txtModelo.Name = "txtModelo";
            this.txtModelo.OnlyUpperCase = true;
            this.txtModelo.Size = new System.Drawing.Size(83, 20);
            this.txtModelo.TabIndex = 1;
            this.txtModelo.TextChanged += new System.EventHandler(this.txtModelo_TextChanged);
            this.txtModelo.Leave += new System.EventHandler(this.txtModelo_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(599, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Costo";
            // 
            // lblNombreProveedor
            // 
            this.lblNombreProveedor.Location = new System.Drawing.Point(120, 48);
            this.lblNombreProveedor.Name = "lblNombreProveedor";
            this.lblNombreProveedor.Size = new System.Drawing.Size(166, 29);
            this.lblNombreProveedor.TabIndex = 9;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.Location = new System.Drawing.Point(312, 48);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(245, 30);
            this.lblDescripcion.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(563, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Esq impuesto";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgViewTallas4);
            this.groupBox1.Controls.Add(this.dgViewTallas3);
            this.groupBox1.Controls.Add(this.dgViewTallas2);
            this.groupBox1.Controls.Add(this.dgViewTallas1);
            this.groupBox1.Location = new System.Drawing.Point(12, 76);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(661, 226);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tallas";
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgViewTallas4.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgViewTallas4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgViewTallas4.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgViewTallas4.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgViewTallas4.Location = new System.Drawing.Point(11, 162);
            this.dgViewTallas4.MultiSelect = false;
            this.dgViewTallas4.Name = "dgViewTallas4";
            this.dgViewTallas4.RowHeadersVisible = false;
            this.dgViewTallas4.Size = new System.Drawing.Size(592, 46);
            this.dgViewTallas4.TabIndex = 3;
            this.dgViewTallas4.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgViewTallas_CellValueChanged);
            this.dgViewTallas4.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgView_ColumnAdded);
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
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgViewTallas3.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgViewTallas3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgViewTallas3.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgViewTallas3.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgViewTallas3.Location = new System.Drawing.Point(11, 113);
            this.dgViewTallas3.MultiSelect = false;
            this.dgViewTallas3.Name = "dgViewTallas3";
            this.dgViewTallas3.RowHeadersVisible = false;
            this.dgViewTallas3.Size = new System.Drawing.Size(592, 46);
            this.dgViewTallas3.TabIndex = 2;
            this.dgViewTallas3.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgViewTallas_CellValueChanged);
            this.dgViewTallas3.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgView_ColumnAdded);
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
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgViewTallas2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgViewTallas2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgViewTallas2.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgViewTallas2.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgViewTallas2.Location = new System.Drawing.Point(11, 64);
            this.dgViewTallas2.MultiSelect = false;
            this.dgViewTallas2.Name = "dgViewTallas2";
            this.dgViewTallas2.RowHeadersVisible = false;
            this.dgViewTallas2.Size = new System.Drawing.Size(592, 46);
            this.dgViewTallas2.TabIndex = 1;
            this.dgViewTallas2.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgViewTallas_CellValueChanged);
            this.dgViewTallas2.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgView_ColumnAdded);
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
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgViewTallas1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgViewTallas1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgViewTallas1.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgViewTallas1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgViewTallas1.Location = new System.Drawing.Point(11, 15);
            this.dgViewTallas1.MultiSelect = false;
            this.dgViewTallas1.Name = "dgViewTallas1";
            this.dgViewTallas1.RowHeadersVisible = false;
            this.dgViewTallas1.Size = new System.Drawing.Size(592, 46);
            this.dgViewTallas1.TabIndex = 0;
            this.dgViewTallas1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgViewTallas_CellValueChanged);
            this.dgViewTallas1.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgView_ColumnAdded);
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Location = new System.Drawing.Point(12, 309);
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtObservaciones.Size = new System.Drawing.Size(394, 86);
            this.txtObservaciones.TabIndex = 5;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(450, 322);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(13, 13);
            this.lblTotal.TabIndex = 18;
            this.lblTotal.Text = "0";
            // 
            // btnProcesar
            // 
            this.btnProcesar.Location = new System.Drawing.Point(418, 372);
            this.btnProcesar.Name = "btnProcesar";
            this.btnProcesar.Size = new System.Drawing.Size(75, 23);
            this.btnProcesar.TabIndex = 6;
            this.btnProcesar.Text = "Procesar";
            this.btnProcesar.UseVisualStyleBackColor = true;
            this.btnProcesar.Click += new System.EventHandler(this.btnProcesar_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(415, 322);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Total :";
            // 
            // txtEsquemaImpuesto
            // 
            this.txtEsquemaImpuesto.Location = new System.Drawing.Point(639, 34);
            this.txtEsquemaImpuesto.MaxValue = 0;
            this.txtEsquemaImpuesto.MinValue = 0;
            this.txtEsquemaImpuesto.Name = "txtEsquemaImpuesto";
            this.txtEsquemaImpuesto.Size = new System.Drawing.Size(28, 20);
            this.txtEsquemaImpuesto.TabIndex = 3;
            this.txtEsquemaImpuesto.Text = "9";
            // 
            // txtCosto
            // 
            this.txtCosto.Location = new System.Drawing.Point(639, 8);
            this.txtCosto.MaxValue = 0;
            this.txtCosto.MinValue = 0;
            this.txtCosto.Name = "txtCosto";
            this.txtCosto.Size = new System.Drawing.Size(43, 20);
            this.txtCosto.TabIndex = 2;
            this.txtCosto.Text = "0";
            // 
            // txtClaveProveedor
            // 
            this.txtClaveProveedor.Location = new System.Drawing.Point(181, 8);
            this.txtClaveProveedor.Name = "txtClaveProveedor";
            this.txtClaveProveedor.OnlyUpperCase = true;
            this.txtClaveProveedor.Size = new System.Drawing.Size(78, 20);
            this.txtClaveProveedor.TabIndex = 0;
            this.txtClaveProveedor.Leave += new System.EventHandler(this.txtClaveProveedor_Leave);
            // 
            // scape1
            // 
            this.scape1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.scape1.Form = this;
            // 
            // frmOrdMaquila
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.scape1;
            this.ClientSize = new System.Drawing.Size(694, 403);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.btnProcesar);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtObservaciones);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtEsquemaImpuesto);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblDescripcion);
            this.Controls.Add(this.lblNombreProveedor);
            this.Controls.Add(this.txtCosto);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtModelo);
            this.Controls.Add(this.txtPrefijoModelo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtClaveProveedor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtOM);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOrdMaquila";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Captura matricial de ordenes de maquila";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOM;
        private System.Windows.Forms.Label label2;
        private UserControls.TextBoxEx txtClaveProveedor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPrefijoModelo;
        private UserControls.TextBoxEx txtModelo;
        private UserControls.NumericTextBox txtCosto;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblNombreProveedor;
        private System.Windows.Forms.Label lblDescripcion;
        private UserControls.NumericTextBox txtEsquemaImpuesto;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnProcesar;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dgViewTallas3;
        private System.Windows.Forms.DataGridView dgViewTallas2;
        private System.Windows.Forms.DataGridView dgViewTallas1;
        private UserControls.Scape scape1;
        private System.Windows.Forms.DataGridView dgViewTallas4;
    }
}