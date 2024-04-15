namespace SIP
{
    partial class frmRecOrdProduccionMaquila2
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtOProd = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOMaquila = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblClave = new System.Windows.Forms.Label();
            this.lblProveedor = new System.Windows.Forms.Label();
            this.btnBuscarOPyOC = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPrefijo = new System.Windows.Forms.TextBox();
            this.txtCodDefectuosos = new System.Windows.Forms.TextBox();
            this.lblModelo = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.dgViewTallas = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgViewTallas2 = new System.Windows.Forms.DataGridView();
            this.dgViewTallas1 = new System.Windows.Forms.DataGridView();
            this.lblTotalOK = new System.Windows.Forms.Label();
            this.lblTotalDefectuosos = new System.Windows.Forms.Label();
            this.btnProcesar = new System.Windows.Forms.Button();
            this.txtEsquemaImp = new SIP.UserControls.NumericTextBox();
            this.txtAlmacen = new SIP.UserControls.NumericTextBox();
            this.txtCostoConfeccion = new SIP.UserControls.NumericTextBox();
            this.dgViewTallas3 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas3)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "O. Prod:";
            // 
            // txtOProd
            // 
            this.txtOProd.Location = new System.Drawing.Point(64, 6);
            this.txtOProd.Name = "txtOProd";
            this.txtOProd.Size = new System.Drawing.Size(82, 20);
            this.txtOProd.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(153, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "O. Maquila:";
            // 
            // txtOMaquila
            // 
            this.txtOMaquila.Location = new System.Drawing.Point(221, 6);
            this.txtOMaquila.Name = "txtOMaquila";
            this.txtOMaquila.Size = new System.Drawing.Size(82, 20);
            this.txtOMaquila.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(309, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Almacen";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(394, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Costo CONF";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(517, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Esquema Imp";
            // 
            // lblClave
            // 
            this.lblClave.AutoSize = true;
            this.lblClave.Location = new System.Drawing.Point(12, 35);
            this.lblClave.Name = "lblClave";
            this.lblClave.Size = new System.Drawing.Size(56, 13);
            this.lblClave.TabIndex = 11;
            this.lblClave.Text = "Proveedor";
            // 
            // lblProveedor
            // 
            this.lblProveedor.AutoSize = true;
            this.lblProveedor.Location = new System.Drawing.Point(12, 65);
            this.lblProveedor.Name = "lblProveedor";
            this.lblProveedor.Size = new System.Drawing.Size(106, 13);
            this.lblProveedor.TabIndex = 12;
            this.lblProveedor.Text = "NOMBRE Proveedor";
            // 
            // btnBuscarOPyOC
            // 
            this.btnBuscarOPyOC.Location = new System.Drawing.Point(407, 56);
            this.btnBuscarOPyOC.Name = "btnBuscarOPyOC";
            this.btnBuscarOPyOC.Size = new System.Drawing.Size(104, 23);
            this.btnBuscarOPyOC.TabIndex = 7;
            this.btnBuscarOPyOC.Text = "Buscar OP y OC";
            this.btnBuscarOPyOC.UseVisualStyleBackColor = true;
            this.btnBuscarOPyOC.Click += new System.EventHandler(this.btnBuscarOPyOC_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(517, 36);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Prefijo";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(517, 61);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "CodDef";
            // 
            // txtPrefijo
            // 
            this.txtPrefijo.Location = new System.Drawing.Point(594, 33);
            this.txtPrefijo.Name = "txtPrefijo";
            this.txtPrefijo.Size = new System.Drawing.Size(15, 20);
            this.txtPrefijo.TabIndex = 6;
            this.txtPrefijo.Text = "X";
            // 
            // txtCodDefectuosos
            // 
            this.txtCodDefectuosos.Location = new System.Drawing.Point(594, 58);
            this.txtCodDefectuosos.Name = "txtCodDefectuosos";
            this.txtCodDefectuosos.Size = new System.Drawing.Size(70, 20);
            this.txtCodDefectuosos.TabIndex = 8;
            this.txtCodDefectuosos.Text = "PRENDASA";
            // 
            // lblModelo
            // 
            this.lblModelo.AutoSize = true;
            this.lblModelo.Location = new System.Drawing.Point(27, 100);
            this.lblModelo.Name = "lblModelo";
            this.lblModelo.Size = new System.Drawing.Size(42, 13);
            this.lblModelo.TabIndex = 18;
            this.lblModelo.Text = "Modelo";
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Location = new System.Drawing.Point(141, 100);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(63, 13);
            this.lblDescripcion.TabIndex = 19;
            this.lblDescripcion.Text = "Descripción";
            // 
            // dgViewTallas
            // 
            this.dgViewTallas.AllowUserToAddRows = false;
            this.dgViewTallas.AllowUserToDeleteRows = false;
            this.dgViewTallas.AllowUserToResizeColumns = false;
            this.dgViewTallas.AllowUserToResizeRows = false;
            this.dgViewTallas.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgViewTallas.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgViewTallas.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dgViewTallas.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgViewTallas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgViewTallas.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgViewTallas.Location = new System.Drawing.Point(19, 19);
            this.dgViewTallas.Name = "dgViewTallas";
            this.dgViewTallas.RowHeadersVisible = false;
            this.dgViewTallas.Size = new System.Drawing.Size(624, 95);
            this.dgViewTallas.TabIndex = 9;
            this.dgViewTallas.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgViewTallas_CellEndEdit);
            this.dgViewTallas.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgViewTallas_ColumnAdded);
            this.dgViewTallas.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgViewTallas_DataError);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgViewTallas3);
            this.groupBox1.Controls.Add(this.dgViewTallas2);
            this.groupBox1.Controls.Add(this.dgViewTallas1);
            this.groupBox1.Controls.Add(this.dgViewTallas);
            this.groupBox1.Location = new System.Drawing.Point(15, 116);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(649, 427);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            // 
            // dgViewTallas2
            // 
            this.dgViewTallas2.AllowUserToAddRows = false;
            this.dgViewTallas2.AllowUserToDeleteRows = false;
            this.dgViewTallas2.AllowUserToResizeColumns = false;
            this.dgViewTallas2.AllowUserToResizeRows = false;
            this.dgViewTallas2.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgViewTallas2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgViewTallas2.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dgViewTallas2.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgViewTallas2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgViewTallas2.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgViewTallas2.Location = new System.Drawing.Point(19, 221);
            this.dgViewTallas2.Name = "dgViewTallas2";
            this.dgViewTallas2.RowHeadersVisible = false;
            this.dgViewTallas2.Size = new System.Drawing.Size(624, 94);
            this.dgViewTallas2.TabIndex = 11;
            this.dgViewTallas2.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgViewTallas2_CellEndEdit);
            this.dgViewTallas2.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgViewTallas2_ColumnAdded);
            this.dgViewTallas2.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgViewTallas2_DataError);
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
            this.dgViewTallas1.Location = new System.Drawing.Point(19, 120);
            this.dgViewTallas1.Name = "dgViewTallas1";
            this.dgViewTallas1.RowHeadersVisible = false;
            this.dgViewTallas1.Size = new System.Drawing.Size(624, 95);
            this.dgViewTallas1.TabIndex = 10;
            this.dgViewTallas1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgViewTallas1_CellEndEdit);
            this.dgViewTallas1.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgViewTallas1_ColumnAdded);
            this.dgViewTallas1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgViewTallas1_DataError);
            // 
            // lblTotalOK
            // 
            this.lblTotalOK.AutoSize = true;
            this.lblTotalOK.Location = new System.Drawing.Point(31, 549);
            this.lblTotalOK.Name = "lblTotalOK";
            this.lblTotalOK.Size = new System.Drawing.Size(52, 13);
            this.lblTotalOK.TabIndex = 22;
            this.lblTotalOK.Text = "Total OK:";
            // 
            // lblTotalDefectuosos
            // 
            this.lblTotalDefectuosos.AutoSize = true;
            this.lblTotalDefectuosos.Location = new System.Drawing.Point(31, 566);
            this.lblTotalDefectuosos.Name = "lblTotalDefectuosos";
            this.lblTotalDefectuosos.Size = new System.Drawing.Size(97, 13);
            this.lblTotalDefectuosos.TabIndex = 23;
            this.lblTotalDefectuosos.Text = "Total Defectuosos:";
            // 
            // btnProcesar
            // 
            this.btnProcesar.Enabled = false;
            this.btnProcesar.Location = new System.Drawing.Point(303, 553);
            this.btnProcesar.Name = "btnProcesar";
            this.btnProcesar.Size = new System.Drawing.Size(75, 23);
            this.btnProcesar.TabIndex = 12;
            this.btnProcesar.Text = "Procesar";
            this.btnProcesar.UseVisualStyleBackColor = true;
            this.btnProcesar.Click += new System.EventHandler(this.btnProcesar_Click);
            // 
            // txtEsquemaImp
            // 
            this.txtEsquemaImp.Location = new System.Drawing.Point(594, 7);
            this.txtEsquemaImp.MaxValue = 0;
            this.txtEsquemaImp.MinValue = 0;
            this.txtEsquemaImp.Name = "txtEsquemaImp";
            this.txtEsquemaImp.Size = new System.Drawing.Size(51, 20);
            this.txtEsquemaImp.TabIndex = 5;
            this.txtEsquemaImp.Text = "9";
            // 
            // txtAlmacen
            // 
            this.txtAlmacen.Location = new System.Drawing.Point(358, 6);
            this.txtAlmacen.MaxValue = 0;
            this.txtAlmacen.MinValue = 0;
            this.txtAlmacen.Name = "txtAlmacen";
            this.txtAlmacen.Size = new System.Drawing.Size(30, 20);
            this.txtAlmacen.TabIndex = 3;
            this.txtAlmacen.Text = "1";
            // 
            // txtCostoConfeccion
            // 
            this.txtCostoConfeccion.Location = new System.Drawing.Point(466, 6);
            this.txtCostoConfeccion.MaxValue = 0;
            this.txtCostoConfeccion.MinValue = 0;
            this.txtCostoConfeccion.Name = "txtCostoConfeccion";
            this.txtCostoConfeccion.Size = new System.Drawing.Size(35, 20);
            this.txtCostoConfeccion.TabIndex = 4;
            this.txtCostoConfeccion.Text = "1";
            // 
            // dgViewTallas3
            // 
            this.dgViewTallas3.AllowUserToAddRows = false;
            this.dgViewTallas3.AllowUserToDeleteRows = false;
            this.dgViewTallas3.AllowUserToResizeColumns = false;
            this.dgViewTallas3.AllowUserToResizeRows = false;
            this.dgViewTallas3.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgViewTallas3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgViewTallas3.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dgViewTallas3.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgViewTallas3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgViewTallas3.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgViewTallas3.Location = new System.Drawing.Point(19, 321);
            this.dgViewTallas3.Name = "dgViewTallas3";
            this.dgViewTallas3.RowHeadersVisible = false;
            this.dgViewTallas3.Size = new System.Drawing.Size(624, 94);
            this.dgViewTallas3.TabIndex = 12;
            this.dgViewTallas3.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgViewTallas3_CellEndEdit);
            this.dgViewTallas3.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgViewTallas3_ColumnAdded);
            this.dgViewTallas3.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgViewTallas3_DataError);
            // 
            // frmRecOrdProduccionMaquila2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 583);
            this.Controls.Add(this.btnProcesar);
            this.Controls.Add(this.lblTotalDefectuosos);
            this.Controls.Add(this.lblTotalOK);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblDescripcion);
            this.Controls.Add(this.lblModelo);
            this.Controls.Add(this.txtCodDefectuosos);
            this.Controls.Add(this.txtPrefijo);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnBuscarOPyOC);
            this.Controls.Add(this.lblProveedor);
            this.Controls.Add(this.lblClave);
            this.Controls.Add(this.txtEsquemaImp);
            this.Controls.Add(this.txtAlmacen);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtCostoConfeccion);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtOMaquila);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtOProd);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRecOrdProduccionMaquila2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmRecOrdProduccionMaquila2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOProd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOMaquila;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private UserControls.NumericTextBox txtCostoConfeccion;
        private System.Windows.Forms.Label label5;
        private UserControls.NumericTextBox txtAlmacen;
        private UserControls.NumericTextBox txtEsquemaImp;
        private System.Windows.Forms.Label lblClave;
        private System.Windows.Forms.Label lblProveedor;
        private System.Windows.Forms.Button btnBuscarOPyOC;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtPrefijo;
        private System.Windows.Forms.TextBox txtCodDefectuosos;
        private System.Windows.Forms.Label lblModelo;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.DataGridView dgViewTallas;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgViewTallas1;
        private System.Windows.Forms.DataGridView dgViewTallas2;
        private System.Windows.Forms.Label lblTotalOK;
        private System.Windows.Forms.Label lblTotalDefectuosos;
        private System.Windows.Forms.Button btnProcesar;
        private System.Windows.Forms.DataGridView dgViewTallas3;
    }
}