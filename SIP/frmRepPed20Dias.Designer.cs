namespace SIP
{
    partial class frmRepPed20Dias
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
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.cmdContinuar = new System.Windows.Forms.Button();
            this.scape1 = new SIP.UserControls.Scape();
            this.SuspendLayout();
            // 
            // dtpFecha
            // 
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(18, 10);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(80, 20);
            this.dtpFecha.TabIndex = 5;
            // 
            // cmdContinuar
            // 
            this.cmdContinuar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdContinuar.Location = new System.Drawing.Point(20, 39);
            this.cmdContinuar.Name = "cmdContinuar";
            this.cmdContinuar.Size = new System.Drawing.Size(75, 23);
            this.cmdContinuar.TabIndex = 6;
            this.cmdContinuar.Text = "Continuar";
            this.cmdContinuar.UseVisualStyleBackColor = true;
            this.cmdContinuar.Click += new System.EventHandler(this.cmdContinuar_Click);
            // 
            // scape1
            // 
            this.scape1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.scape1.Form = this;
            // 
            // frmRepPed20Dias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.scape1;
            this.ClientSize = new System.Drawing.Size(116, 82);
            this.Controls.Add(this.cmdContinuar);
            this.Controls.Add(this.dtpFecha);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRepPed20Dias";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Filtro";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Button cmdContinuar;
        private UserControls.Scape scape1;
    }
}