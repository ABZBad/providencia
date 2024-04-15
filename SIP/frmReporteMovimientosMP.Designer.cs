namespace SIP
{
    partial class frmReporteMovimientosMP
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
            this.btnContinuar = new System.Windows.Forms.Button();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.lblHasta = new System.Windows.Forms.Label();
            this.lblDesde = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgConceptos = new System.Windows.Forms.DataGridView();
            this.SELECCION = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.CVE_CPTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DESCR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgConceptos)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(100, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(343, 29);
            this.label1.TabIndex = 18;
            this.label1.Text = "Reporte de Movimientos MP";
            // 
            // btnContinuar
            // 
            this.btnContinuar.Location = new System.Drawing.Point(452, 517);
            this.btnContinuar.Margin = new System.Windows.Forms.Padding(4);
            this.btnContinuar.Name = "btnContinuar";
            this.btnContinuar.Size = new System.Drawing.Size(100, 28);
            this.btnContinuar.TabIndex = 17;
            this.btnContinuar.Text = "Continuar";
            this.btnContinuar.UseVisualStyleBackColor = true;
            this.btnContinuar.Click += new System.EventHandler(this.btnContinuar_Click);
            // 
            // dtpHasta
            // 
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta.Location = new System.Drawing.Point(339, 520);
            this.dtpHasta.Margin = new System.Windows.Forms.Padding(4);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(105, 22);
            this.dtpHasta.TabIndex = 16;
            this.dtpHasta.Value = new System.DateTime(2021, 12, 7, 0, 0, 0, 0);
            // 
            // dtpDesde
            // 
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.Location = new System.Drawing.Point(152, 520);
            this.dtpDesde.Margin = new System.Windows.Forms.Padding(4);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(105, 22);
            this.dtpDesde.TabIndex = 15;
            this.dtpDesde.Value = new System.DateTime(2021, 12, 7, 0, 0, 0, 0);
            // 
            // lblHasta
            // 
            this.lblHasta.AutoSize = true;
            this.lblHasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHasta.Location = new System.Drawing.Point(281, 523);
            this.lblHasta.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.Size = new System.Drawing.Size(50, 17);
            this.lblHasta.TabIndex = 14;
            this.lblHasta.Text = "Hasta";
            // 
            // lblDesde
            // 
            this.lblDesde.AutoSize = true;
            this.lblDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesde.Location = new System.Drawing.Point(90, 523);
            this.lblDesde.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDesde.Name = "lblDesde";
            this.lblDesde.Size = new System.Drawing.Size(54, 17);
            this.lblDesde.TabIndex = 13;
            this.lblDesde.Text = "Desde";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(19, 54);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 17);
            this.label2.TabIndex = 19;
            this.label2.Text = "Conceptos de Salida";
            // 
            // dgConceptos
            // 
            this.dgConceptos.AllowUserToAddRows = false;
            this.dgConceptos.AllowUserToDeleteRows = false;
            this.dgConceptos.AllowUserToResizeColumns = false;
            this.dgConceptos.AllowUserToResizeRows = false;
            this.dgConceptos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgConceptos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SELECCION,
            this.CVE_CPTO,
            this.DESCR});
            this.dgConceptos.Location = new System.Drawing.Point(22, 83);
            this.dgConceptos.Name = "dgConceptos";
            this.dgConceptos.RowTemplate.Height = 24;
            this.dgConceptos.Size = new System.Drawing.Size(523, 427);
            this.dgConceptos.TabIndex = 20;
            // 
            // SELECCION
            // 
            this.SELECCION.DataPropertyName = "SELECCION";
            this.SELECCION.FalseValue = "0";
            this.SELECCION.Frozen = true;
            this.SELECCION.HeaderText = "";
            this.SELECCION.IndeterminateValue = "0";
            this.SELECCION.Name = "SELECCION";
            this.SELECCION.TrueValue = "1";
            this.SELECCION.Width = 40;
            // 
            // CVE_CPTO
            // 
            this.CVE_CPTO.DataPropertyName = "CVE_CPTO";
            this.CVE_CPTO.HeaderText = "Clave";
            this.CVE_CPTO.Name = "CVE_CPTO";
            this.CVE_CPTO.ReadOnly = true;
            this.CVE_CPTO.Width = 70;
            // 
            // DESCR
            // 
            this.DESCR.DataPropertyName = "DESCR";
            this.DESCR.HeaderText = "Concepto";
            this.DESCR.Name = "DESCR";
            this.DESCR.ReadOnly = true;
            this.DESCR.Width = 250;
            // 
            // frmReporteMovimientosMP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 558);
            this.Controls.Add(this.dgConceptos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnContinuar);
            this.Controls.Add(this.dtpHasta);
            this.Controls.Add(this.dtpDesde);
            this.Controls.Add(this.lblHasta);
            this.Controls.Add(this.lblDesde);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmReporteMovimientosMP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reporte de Movimientos MP";
            this.Load += new System.EventHandler(this.frmReporteMovimientosMP_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgConceptos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnContinuar;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.Label lblDesde;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgConceptos;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SELECCION;
        private System.Windows.Forms.DataGridViewTextBoxColumn CVE_CPTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn DESCR;
    }
}