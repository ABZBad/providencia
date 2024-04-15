namespace SIP
{
    partial class frmRepVentPesosPrendas
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
            this.btnContinuar = new System.Windows.Forms.Button();
            this.dtpFin = new System.Windows.Forms.DateTimePicker();
            this.dtpIni = new System.Windows.Forms.DateTimePicker();
            this.scape1 = new SIP.UserControls.Scape();
            this.chkVtasEnRango = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnContinuar
            // 
            this.btnContinuar.Location = new System.Drawing.Point(98, 83);
            this.btnContinuar.Name = "btnContinuar";
            this.btnContinuar.Size = new System.Drawing.Size(75, 23);
            this.btnContinuar.TabIndex = 7;
            this.btnContinuar.Text = "Continuar";
            this.btnContinuar.UseVisualStyleBackColor = true;
            this.btnContinuar.Click += new System.EventHandler(this.btnContinuar_Click);
            // 
            // dtpFin
            // 
            this.dtpFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFin.Location = new System.Drawing.Point(93, 11);
            this.dtpFin.Name = "dtpFin";
            this.dtpFin.Size = new System.Drawing.Size(80, 20);
            this.dtpFin.TabIndex = 5;
            // 
            // dtpIni
            // 
            this.dtpIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpIni.Location = new System.Drawing.Point(5, 11);
            this.dtpIni.Name = "dtpIni";
            this.dtpIni.Size = new System.Drawing.Size(80, 20);
            this.dtpIni.TabIndex = 4;
            // 
            // scape1
            // 
            this.scape1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.scape1.Form = this;
            // 
            // chkVtasEnRango
            // 
            this.chkVtasEnRango.AutoSize = true;
            this.chkVtasEnRango.Location = new System.Drawing.Point(5, 45);
            this.chkVtasEnRango.Name = "chkVtasEnRango";
            this.chkVtasEnRango.Size = new System.Drawing.Size(159, 17);
            this.chkVtasEnRango.TabIndex = 8;
            this.chkVtasEnRango.Text = "Solo con ventas en el rango";
            this.chkVtasEnRango.UseVisualStyleBackColor = true;
            // 
            // frmRepVentPesosPrendas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.scape1;
            this.ClientSize = new System.Drawing.Size(177, 118);
            this.Controls.Add(this.chkVtasEnRango);
            this.Controls.Add(this.btnContinuar);
            this.Controls.Add(this.dtpFin);
            this.Controls.Add(this.dtpIni);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRepVentPesosPrendas";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Filtro";
            this.Load += new System.EventHandler(this.frmRepVentPesosPrendas_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnContinuar;
        private System.Windows.Forms.DateTimePicker dtpFin;
        private System.Windows.Forms.DateTimePicker dtpIni;
        private UserControls.Scape scape1;
        private System.Windows.Forms.CheckBox chkVtasEnRango;
    }
}