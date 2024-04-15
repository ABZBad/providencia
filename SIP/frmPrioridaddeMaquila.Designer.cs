namespace SIP
{
    partial class frmPrioridaddeMaquila
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
            this.txtProveedor = new SIP.UserControls.TextBoxEx();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPrefijo = new SIP.UserControls.TextBoxEx();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.CVE_CLPV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FECHA_DOC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MODELO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SUMA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRIORIDAD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OBS_COMP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnImprimir = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Proveedor";
            // 
            // txtProveedor
            // 
            this.txtProveedor.Location = new System.Drawing.Point(74, 6);
            this.txtProveedor.Name = "txtProveedor";
            this.txtProveedor.OnlyUpperCase = true;
            this.txtProveedor.Size = new System.Drawing.Size(100, 20);
            this.txtProveedor.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(180, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Prefijo";
            // 
            // txtPrefijo
            // 
            this.txtPrefijo.Location = new System.Drawing.Point(222, 6);
            this.txtPrefijo.Name = "txtPrefijo";
            this.txtPrefijo.OnlyUpperCase = false;
            this.txtPrefijo.Size = new System.Drawing.Size(16, 20);
            this.txtPrefijo.TabIndex = 3;
            this.txtPrefijo.Text = "X";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(253, 4);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 4;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CVE_CLPV,
            this.OC,
            this.FECHA_DOC,
            this.MODELO,
            this.SUMA,
            this.PRIORIDAD,
            this.OBS_COMP});
            this.dataGridView1.Location = new System.Drawing.Point(12, 42);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(475, 457);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // CVE_CLPV
            // 
            this.CVE_CLPV.DataPropertyName = "CVE_CLPV";
            this.CVE_CLPV.HeaderText = "CVE_CLPV";
            this.CVE_CLPV.Name = "CVE_CLPV";
            this.CVE_CLPV.ReadOnly = true;
            this.CVE_CLPV.Visible = false;
            // 
            // OC
            // 
            this.OC.DataPropertyName = "CVE_DOC";
            this.OC.HeaderText = "OC";
            this.OC.Name = "OC";
            this.OC.ReadOnly = true;
            this.OC.Width = 60;
            // 
            // FECHA_DOC
            // 
            this.FECHA_DOC.DataPropertyName = "FECHA_DOC";
            this.FECHA_DOC.HeaderText = "FECHA";
            this.FECHA_DOC.Name = "FECHA_DOC";
            this.FECHA_DOC.ReadOnly = true;
            this.FECHA_DOC.Width = 90;
            // 
            // MODELO
            // 
            this.MODELO.DataPropertyName = "MODELO";
            this.MODELO.HeaderText = "MODELO";
            this.MODELO.Name = "MODELO";
            this.MODELO.ReadOnly = true;
            // 
            // SUMA
            // 
            this.SUMA.DataPropertyName = "SUMA";
            this.SUMA.HeaderText = "SUMA";
            this.SUMA.Name = "SUMA";
            this.SUMA.ReadOnly = true;
            this.SUMA.Width = 70;
            // 
            // PRIORIDAD
            // 
            this.PRIORIDAD.DataPropertyName = "PRIORIDAD";
            this.PRIORIDAD.HeaderText = "PRIORIDAD";
            this.PRIORIDAD.Name = "PRIORIDAD";
            this.PRIORIDAD.ReadOnly = true;
            this.PRIORIDAD.Width = 90;
            // 
            // OBS_COMP
            // 
            this.OBS_COMP.DataPropertyName = "OBS_COMP";
            this.OBS_COMP.HeaderText = "OBS_COMP";
            this.OBS_COMP.Name = "OBS_COMP";
            this.OBS_COMP.ReadOnly = true;
            this.OBS_COMP.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 518);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(210, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Ordenado por Prioridad, fecha doc, modelo";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 540);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(296, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Las ordenes con prioridad 1000 no se imrpimiran en el reporte";
            // 
            // btnImprimir
            // 
            this.btnImprimir.Location = new System.Drawing.Point(394, 518);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(75, 23);
            this.btnImprimir.TabIndex = 8;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // frmPrioridaddeMaquila
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 575);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtPrefijo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtProveedor);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPrioridaddeMaquila";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Prioridad de Maquila";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private SIP.UserControls.TextBoxEx txtProveedor;
        private System.Windows.Forms.Label label2;
        private SIP.UserControls.TextBoxEx txtPrefijo;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.DataGridViewTextBoxColumn CVE_CLPV;
        private System.Windows.Forms.DataGridViewTextBoxColumn OC;
        private System.Windows.Forms.DataGridViewTextBoxColumn FECHA_DOC;
        private System.Windows.Forms.DataGridViewTextBoxColumn MODELO;
        private System.Windows.Forms.DataGridViewTextBoxColumn SUMA;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRIORIDAD;
        private System.Windows.Forms.DataGridViewTextBoxColumn OBS_COMP;
    }
}