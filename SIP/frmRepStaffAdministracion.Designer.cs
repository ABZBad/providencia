namespace SIP
{
    partial class frmRepStaffAdministracion
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
            this.scape1 = new SIP.UserControls.Scape();
            this.dtFechaDesde = new System.Windows.Forms.DateTimePicker();
            this.dtFechaHasta = new System.Windows.Forms.DateTimePicker();
            this.btnContinuar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // scape1
            // 
            this.scape1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.scape1.Form = this;
            // 
            // dtFechaDesde
            // 
            this.dtFechaDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFechaDesde.Location = new System.Drawing.Point(8, 9);
            this.dtFechaDesde.Name = "dtFechaDesde";
            this.dtFechaDesde.Size = new System.Drawing.Size(81, 20);
            this.dtFechaDesde.TabIndex = 0;
            // 
            // dtFechaHasta
            // 
            this.dtFechaHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFechaHasta.Location = new System.Drawing.Point(101, 9);
            this.dtFechaHasta.Name = "dtFechaHasta";
            this.dtFechaHasta.Size = new System.Drawing.Size(81, 20);
            this.dtFechaHasta.TabIndex = 1;
            // 
            // btnContinuar
            // 
            this.btnContinuar.Location = new System.Drawing.Point(106, 50);
            this.btnContinuar.Name = "btnContinuar";
            this.btnContinuar.Size = new System.Drawing.Size(75, 23);
            this.btnContinuar.TabIndex = 2;
            this.btnContinuar.Text = "Continuar";
            this.btnContinuar.UseVisualStyleBackColor = true;
            this.btnContinuar.Click += new System.EventHandler(this.btnContinuar_Click);
            // 
            // frmRepStaff
            // 
            this.AcceptButton = this.btnContinuar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.scape1;
            this.ClientSize = new System.Drawing.Size(197, 88);
            this.Controls.Add(this.btnContinuar);
            this.Controls.Add(this.dtFechaHasta);
            this.Controls.Add(this.dtFechaDesde);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRepStaff";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Filtro";
            this.Load += new System.EventHandler(this.frmRepStaff_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.Scape scape1;
        private System.Windows.Forms.Button btnContinuar;
        private System.Windows.Forms.DateTimePicker dtFechaHasta;
        private System.Windows.Forms.DateTimePicker dtFechaDesde;
    }
}