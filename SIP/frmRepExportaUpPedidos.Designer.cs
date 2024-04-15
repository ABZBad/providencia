namespace SIP
{
    partial class frmRepExportaUpPedidos
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
            this.dtpIni = new System.Windows.Forms.DateTimePicker();
            this.dtpFin = new System.Windows.Forms.DateTimePicker();
            this.chkSinFechaSurtido = new System.Windows.Forms.CheckBox();
            this.btnContinuar = new System.Windows.Forms.Button();
            this.scape1 = new SIP.UserControls.Scape();
            this.SuspendLayout();
            // 
            // dtpIni
            // 
            this.dtpIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpIni.Location = new System.Drawing.Point(5, 8);
            this.dtpIni.Name = "dtpIni";
            this.dtpIni.Size = new System.Drawing.Size(80, 20);
            this.dtpIni.TabIndex = 0;
            this.dtpIni.ValueChanged += new System.EventHandler(this.dtpIni_ValueChanged);
            // 
            // dtpFin
            // 
            this.dtpFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFin.Location = new System.Drawing.Point(93, 8);
            this.dtpFin.Name = "dtpFin";
            this.dtpFin.Size = new System.Drawing.Size(80, 20);
            this.dtpFin.TabIndex = 1;
            this.dtpFin.ValueChanged += new System.EventHandler(this.dtpFin_ValueChanged);
            // 
            // chkSinFechaSurtido
            // 
            this.chkSinFechaSurtido.AutoSize = true;
            this.chkSinFechaSurtido.Location = new System.Drawing.Point(8, 36);
            this.chkSinFechaSurtido.Name = "chkSinFechaSurtido";
            this.chkSinFechaSurtido.Size = new System.Drawing.Size(120, 17);
            this.chkSinFechaSurtido.TabIndex = 2;
            this.chkSinFechaSurtido.Text = "Sin fecha de surtido";
            this.chkSinFechaSurtido.UseVisualStyleBackColor = true;
            this.chkSinFechaSurtido.CheckedChanged += new System.EventHandler(this.chkSinFechaSurtido_CheckedChanged);
            // 
            // btnContinuar
            // 
            this.btnContinuar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnContinuar.Location = new System.Drawing.Point(93, 53);
            this.btnContinuar.Name = "btnContinuar";
            this.btnContinuar.Size = new System.Drawing.Size(75, 23);
            this.btnContinuar.TabIndex = 3;
            this.btnContinuar.Text = "Continuar";
            this.btnContinuar.UseVisualStyleBackColor = true;
            this.btnContinuar.Click += new System.EventHandler(this.btnContinuar_Click);
            // 
            // scape1
            // 
            this.scape1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.scape1.Form = this;
            // 
            // frmRepExportaUpPedidos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.scape1;
            this.ClientSize = new System.Drawing.Size(178, 79);
            this.Controls.Add(this.btnContinuar);
            this.Controls.Add(this.chkSinFechaSurtido);
            this.Controls.Add(this.dtpFin);
            this.Controls.Add(this.dtpIni);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRepExportaUpPedidos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fecha SIP";
            this.Load += new System.EventHandler(this.frmRepExportaUpPedidos_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpIni;
        private System.Windows.Forms.DateTimePicker dtpFin;
        private System.Windows.Forms.CheckBox chkSinFechaSurtido;
        private System.Windows.Forms.Button btnContinuar;
        private UserControls.Scape scape1;
    }
}