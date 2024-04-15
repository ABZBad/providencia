namespace SIP
{
    partial class frmRepFolNuev
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
            this.radioCredito = new System.Windows.Forms.RadioButton();
            this.radioMostrador = new System.Windows.Forms.RadioButton();
            this.radioAmbos = new System.Windows.Forms.RadioButton();
            this.dtpIni = new System.Windows.Forms.DateTimePicker();
            this.dtpFin = new System.Windows.Forms.DateTimePicker();
            this.btnContinuar = new System.Windows.Forms.Button();
            this.scape1 = new SIP.UserControls.Scape();
            this.SuspendLayout();
            // 
            // radioCredito
            // 
            this.radioCredito.AutoSize = true;
            this.radioCredito.Location = new System.Drawing.Point(13, 13);
            this.radioCredito.Name = "radioCredito";
            this.radioCredito.Size = new System.Drawing.Size(58, 17);
            this.radioCredito.TabIndex = 4;
            this.radioCredito.Text = "Crédito";
            this.radioCredito.UseVisualStyleBackColor = true;
            // 
            // radioMostrador
            // 
            this.radioMostrador.AutoSize = true;
            this.radioMostrador.Location = new System.Drawing.Point(81, 13);
            this.radioMostrador.Name = "radioMostrador";
            this.radioMostrador.Size = new System.Drawing.Size(72, 17);
            this.radioMostrador.TabIndex = 5;
            this.radioMostrador.Text = "Mostrador";
            this.radioMostrador.UseVisualStyleBackColor = true;
            // 
            // radioAmbos
            // 
            this.radioAmbos.AutoSize = true;
            this.radioAmbos.Checked = true;
            this.radioAmbos.Location = new System.Drawing.Point(164, 13);
            this.radioAmbos.Name = "radioAmbos";
            this.radioAmbos.Size = new System.Drawing.Size(57, 17);
            this.radioAmbos.TabIndex = 0;
            this.radioAmbos.TabStop = true;
            this.radioAmbos.Text = "Ambos";
            this.radioAmbos.UseVisualStyleBackColor = true;
            // 
            // dtpIni
            // 
            this.dtpIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpIni.Location = new System.Drawing.Point(16, 43);
            this.dtpIni.Name = "dtpIni";
            this.dtpIni.Size = new System.Drawing.Size(87, 20);
            this.dtpIni.TabIndex = 1;
            // 
            // dtpFin
            // 
            this.dtpFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFin.Location = new System.Drawing.Point(125, 43);
            this.dtpFin.Name = "dtpFin";
            this.dtpFin.Size = new System.Drawing.Size(87, 20);
            this.dtpFin.TabIndex = 2;
            // 
            // btnContinuar
            // 
            this.btnContinuar.Location = new System.Drawing.Point(80, 69);
            this.btnContinuar.Name = "btnContinuar";
            this.btnContinuar.Size = new System.Drawing.Size(75, 23);
            this.btnContinuar.TabIndex = 3;
            this.btnContinuar.Text = "Continuar";
            this.btnContinuar.UseVisualStyleBackColor = true;
            this.btnContinuar.Click += new System.EventHandler(this.btnContinuar_Click);
            // 
            // scape1
            // 
            this.scape1.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.scape1.Form = this;
            // 
            // frmRepFolNuev
            // 
            this.AcceptButton = this.btnContinuar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.scape1;
            this.ClientSize = new System.Drawing.Size(233, 101);
            this.Controls.Add(this.btnContinuar);
            this.Controls.Add(this.dtpFin);
            this.Controls.Add(this.dtpIni);
            this.Controls.Add(this.radioAmbos);
            this.Controls.Add(this.radioMostrador);
            this.Controls.Add(this.radioCredito);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRepFolNuev";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Filtro";
            this.Load += new System.EventHandler(this.frmRepFolAnt_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioCredito;
        private System.Windows.Forms.RadioButton radioMostrador;
        private System.Windows.Forms.RadioButton radioAmbos;
        private System.Windows.Forms.DateTimePicker dtpIni;
        private System.Windows.Forms.DateTimePicker dtpFin;
        private System.Windows.Forms.Button btnContinuar;
        private UserControls.Scape scape1;
    }
}