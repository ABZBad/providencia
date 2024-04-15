namespace SIP
{
    partial class frmFiltroConta
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
            this.lblNomRepor = new System.Windows.Forms.Label();
            this.lblDesde = new System.Windows.Forms.Label();
            this.lblHasta = new System.Windows.Forms.Label();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.txtWichOne = new System.Windows.Forms.TextBox();
            this.btnContinuar = new System.Windows.Forms.Button();
            this.scape1 = new SIP.UserControls.Scape();
            this.SuspendLayout();
            // 
            // lblNomRepor
            // 
            this.lblNomRepor.Location = new System.Drawing.Point(12, 9);
            this.lblNomRepor.Name = "lblNomRepor";
            this.lblNomRepor.Size = new System.Drawing.Size(175, 42);
            this.lblNomRepor.TabIndex = 0;
            this.lblNomRepor.Text = "Rpt";
            // 
            // lblDesde
            // 
            this.lblDesde.AutoSize = true;
            this.lblDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesde.Location = new System.Drawing.Point(33, 61);
            this.lblDesde.Name = "lblDesde";
            this.lblDesde.Size = new System.Drawing.Size(43, 13);
            this.lblDesde.TabIndex = 1;
            this.lblDesde.Text = "Desde";
            this.lblDesde.Click += new System.EventHandler(this.lblDesde_Click);
            // 
            // lblHasta
            // 
            this.lblHasta.AutoSize = true;
            this.lblHasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHasta.Location = new System.Drawing.Point(125, 61);
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.Size = new System.Drawing.Size(40, 13);
            this.lblHasta.TabIndex = 2;
            this.lblHasta.Text = "Hasta";
            this.lblHasta.Click += new System.EventHandler(this.lblHasta_Click);
            // 
            // dtpDesde
            // 
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.Location = new System.Drawing.Point(13, 89);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(80, 20);
            this.dtpDesde.TabIndex = 3;
            this.dtpDesde.Value = new System.DateTime(2004, 7, 19, 0, 0, 0, 0);
            this.dtpDesde.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // dtpHasta
            // 
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta.Location = new System.Drawing.Point(107, 89);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(80, 20);
            this.dtpHasta.TabIndex = 4;
            this.dtpHasta.Value = new System.DateTime(2004, 7, 19, 0, 0, 0, 0);
            this.dtpHasta.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            // 
            // txtWichOne
            // 
            this.txtWichOne.Location = new System.Drawing.Point(12, 116);
            this.txtWichOne.Name = "txtWichOne";
            this.txtWichOne.Size = new System.Drawing.Size(109, 20);
            this.txtWichOne.TabIndex = 5;
            // 
            // btnContinuar
            // 
            this.btnContinuar.Location = new System.Drawing.Point(62, 117);
            this.btnContinuar.Name = "btnContinuar";
            this.btnContinuar.Size = new System.Drawing.Size(75, 23);
            this.btnContinuar.TabIndex = 6;
            this.btnContinuar.Text = "Continuar";
            this.btnContinuar.UseVisualStyleBackColor = true;
            this.btnContinuar.Click += new System.EventHandler(this.btnContinuar_Click);
            // 
            // scape1
            // 
            this.scape1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.scape1.Form = this;
            // 
            // frmFiltroConta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.scape1;
            this.ClientSize = new System.Drawing.Size(201, 152);
            this.Controls.Add(this.btnContinuar);
            this.Controls.Add(this.txtWichOne);
            this.Controls.Add(this.dtpHasta);
            this.Controls.Add(this.dtpDesde);
            this.Controls.Add(this.lblHasta);
            this.Controls.Add(this.lblDesde);
            this.Controls.Add(this.lblNomRepor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFiltroConta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Filtro de Contabilidad";
            this.Load += new System.EventHandler(this.frmFiltroConta_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNomRepor;
        private System.Windows.Forms.Label lblDesde;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.TextBox txtWichOne;
        private System.Windows.Forms.Button btnContinuar;
        private UserControls.Scape scape1;
    }
}