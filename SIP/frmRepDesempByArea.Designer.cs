namespace SIP
{
    partial class frmRepDesempByArea
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
            this.dtFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.dtFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.btnContinuar = new System.Windows.Forms.Button();
            this.scape1 = new SIP.UserControls.Scape();
            this.SuspendLayout();
            // 
            // dtFechaInicial
            // 
            this.dtFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFechaInicial.Location = new System.Drawing.Point(5, 22);
            this.dtFechaInicial.Name = "dtFechaInicial";
            this.dtFechaInicial.Size = new System.Drawing.Size(81, 20);
            this.dtFechaInicial.TabIndex = 0;
            // 
            // dtFechaFinal
            // 
            this.dtFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFechaFinal.Location = new System.Drawing.Point(95, 22);
            this.dtFechaFinal.Name = "dtFechaFinal";
            this.dtFechaFinal.Size = new System.Drawing.Size(81, 20);
            this.dtFechaFinal.TabIndex = 1;
            // 
            // btnContinuar
            // 
            this.btnContinuar.Location = new System.Drawing.Point(95, 62);
            this.btnContinuar.Name = "btnContinuar";
            this.btnContinuar.Size = new System.Drawing.Size(75, 23);
            this.btnContinuar.TabIndex = 2;
            this.btnContinuar.Text = "Continuar";
            this.btnContinuar.UseVisualStyleBackColor = true;
            this.btnContinuar.Click += new System.EventHandler(this.btnContinuar_Click);
            // 
            // scape1
            // 
            this.scape1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.scape1.Form = this;
            // 
            // frmRepDesempByArea
            // 
            this.AcceptButton = this.btnContinuar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.scape1;
            this.ClientSize = new System.Drawing.Size(184, 97);
            this.Controls.Add(this.btnContinuar);
            this.Controls.Add(this.dtFechaFinal);
            this.Controls.Add(this.dtFechaInicial);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRepDesempByArea";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Desempeño por área";
            this.Load += new System.EventHandler(this.frmRepDesempByArea_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtFechaInicial;
        private System.Windows.Forms.DateTimePicker dtFechaFinal;
        private System.Windows.Forms.Button btnContinuar;
        private UserControls.Scape scape1;
    }
}