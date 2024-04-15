namespace SIP
{
    partial class frmRepPartArt
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
            this.option1 = new System.Windows.Forms.RadioButton();
            this.option2 = new System.Windows.Forms.RadioButton();
            this.option3 = new System.Windows.Forms.RadioButton();
            this.dtpFechaIni = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.btnEmitir = new System.Windows.Forms.Button();
            this.scape1 = new SIP.UserControls.Scape();
            this.SuspendLayout();
            // 
            // option1
            // 
            this.option1.AutoSize = true;
            this.option1.Location = new System.Drawing.Point(13, 6);
            this.option1.Name = "option1";
            this.option1.Size = new System.Drawing.Size(53, 17);
            this.option1.TabIndex = 0;
            this.option1.TabStop = true;
            this.option1.Text = "Línea";
            this.option1.UseVisualStyleBackColor = true;
            // 
            // option2
            // 
            this.option2.AutoSize = true;
            this.option2.Location = new System.Drawing.Point(84, 6);
            this.option2.Name = "option2";
            this.option2.Size = new System.Drawing.Size(60, 17);
            this.option2.TabIndex = 1;
            this.option2.TabStop = true;
            this.option2.Text = "Modelo";
            this.option2.UseVisualStyleBackColor = true;
            // 
            // option3
            // 
            this.option3.AutoSize = true;
            this.option3.Location = new System.Drawing.Point(170, 6);
            this.option3.Name = "option3";
            this.option3.Size = new System.Drawing.Size(48, 17);
            this.option3.TabIndex = 2;
            this.option3.TabStop = true;
            this.option3.Text = "Talla";
            this.option3.UseVisualStyleBackColor = true;
            // 
            // dtpFechaIni
            // 
            this.dtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaIni.Location = new System.Drawing.Point(41, 35);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Size = new System.Drawing.Size(80, 20);
            this.dtpFechaIni.TabIndex = 6;
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(136, 34);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(80, 20);
            this.dtpFechaFin.TabIndex = 7;
            // 
            // btnEmitir
            // 
            this.btnEmitir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnEmitir.Location = new System.Drawing.Point(98, 65);
            this.btnEmitir.Name = "btnEmitir";
            this.btnEmitir.Size = new System.Drawing.Size(65, 23);
            this.btnEmitir.TabIndex = 8;
            this.btnEmitir.Text = "Emitir";
            this.btnEmitir.UseVisualStyleBackColor = true;
            this.btnEmitir.Click += new System.EventHandler(this.btnEmitir_Click);
            // 
            // scape1
            // 
            this.scape1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.scape1.Form = this;
            // 
            // frmRepPartArt
            // 
            this.AcceptButton = this.btnEmitir;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.scape1;
            this.ClientSize = new System.Drawing.Size(233, 101);
            this.Controls.Add(this.btnEmitir);
            this.Controls.Add(this.dtpFechaFin);
            this.Controls.Add(this.dtpFechaIni);
            this.Controls.Add(this.option3);
            this.Controls.Add(this.option2);
            this.Controls.Add(this.option1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRepPartArt";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Filtro";
            this.Load += new System.EventHandler(this.frmRepPartArt_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton option1;
        private System.Windows.Forms.RadioButton option2;
        private System.Windows.Forms.RadioButton option3;
        private System.Windows.Forms.DateTimePicker dtpFechaIni;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Button btnEmitir;
        private UserControls.Scape scape1;
    }
}