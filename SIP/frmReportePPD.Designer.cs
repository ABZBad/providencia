namespace SIP
{
    partial class frmReportePPD
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
            this.btnContinuar = new System.Windows.Forms.Button();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.lblHasta = new System.Windows.Forms.Label();
            this.lblDesde = new System.Windows.Forms.Label();
            this.radCXP = new System.Windows.Forms.RadioButton();
            this.radCXC = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(12, 9);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(212, 24);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Reporte de CFDI PPD";
            // 
            // btnContinuar
            // 
            this.btnContinuar.Location = new System.Drawing.Point(80, 119);
            this.btnContinuar.Name = "btnContinuar";
            this.btnContinuar.Size = new System.Drawing.Size(75, 23);
            this.btnContinuar.TabIndex = 16;
            this.btnContinuar.Text = "Continuar";
            this.btnContinuar.UseVisualStyleBackColor = true;
            this.btnContinuar.Click += new System.EventHandler(this.btnContinuar_Click);
            // 
            // dtpHasta
            // 
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta.Location = new System.Drawing.Point(127, 60);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(80, 20);
            this.dtpHasta.TabIndex = 15;
            this.dtpHasta.Value = new System.DateTime(2004, 7, 19, 0, 0, 0, 0);
            // 
            // dtpDesde
            // 
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.Location = new System.Drawing.Point(33, 60);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(80, 20);
            this.dtpDesde.TabIndex = 14;
            this.dtpDesde.Value = new System.DateTime(2004, 7, 19, 0, 0, 0, 0);
            // 
            // lblHasta
            // 
            this.lblHasta.AutoSize = true;
            this.lblHasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHasta.Location = new System.Drawing.Point(145, 44);
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.Size = new System.Drawing.Size(40, 13);
            this.lblHasta.TabIndex = 13;
            this.lblHasta.Text = "Hasta";
            // 
            // lblDesde
            // 
            this.lblDesde.AutoSize = true;
            this.lblDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesde.Location = new System.Drawing.Point(53, 44);
            this.lblDesde.Name = "lblDesde";
            this.lblDesde.Size = new System.Drawing.Size(43, 13);
            this.lblDesde.TabIndex = 12;
            this.lblDesde.Text = "Desde";
            // 
            // radCXP
            // 
            this.radCXP.AutoSize = true;
            this.radCXP.Checked = true;
            this.radCXP.Location = new System.Drawing.Point(50, 86);
            this.radCXP.Name = "radCXP";
            this.radCXP.Size = new System.Drawing.Size(46, 17);
            this.radCXP.TabIndex = 17;
            this.radCXP.TabStop = true;
            this.radCXP.Text = "CXP";
            this.radCXP.UseVisualStyleBackColor = true;
            // 
            // radCXC
            // 
            this.radCXC.AutoSize = true;
            this.radCXC.Location = new System.Drawing.Point(148, 86);
            this.radCXC.Name = "radCXC";
            this.radCXC.Size = new System.Drawing.Size(46, 17);
            this.radCXC.TabIndex = 18;
            this.radCXC.Text = "CXC";
            this.radCXC.UseVisualStyleBackColor = true;
            // 
            // frmReportePPD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(238, 154);
            this.Controls.Add(this.radCXC);
            this.Controls.Add(this.radCXP);
            this.Controls.Add(this.btnContinuar);
            this.Controls.Add(this.dtpHasta);
            this.Controls.Add(this.dtpDesde);
            this.Controls.Add(this.lblHasta);
            this.Controls.Add(this.lblDesde);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReportePPD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmReportePPD";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnContinuar;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.Label lblDesde;
        private System.Windows.Forms.RadioButton radCXP;
        private System.Windows.Forms.RadioButton radCXC;
    }
}