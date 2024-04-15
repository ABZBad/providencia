namespace SIP
{
    partial class frmRepExist
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
            this.optAlmGeneral = new System.Windows.Forms.RadioButton();
            this.optAlmSurtido = new System.Windows.Forms.RadioButton();
            this.OptAlmlMostrador = new System.Windows.Forms.RadioButton();
            this.btnBaseSMAX = new System.Windows.Forms.Button();
            this.btnBaseSMIN = new System.Windows.Forms.Button();
            this.baseSMINSinPP = new System.Windows.Forms.Button();
            this.scape1 = new SIP.UserControls.Scape();
            this.SuspendLayout();
            // 
            // optAlmGeneral
            // 
            this.optAlmGeneral.AutoSize = true;
            this.optAlmGeneral.Checked = true;
            this.optAlmGeneral.Location = new System.Drawing.Point(40, 8);
            this.optAlmGeneral.Margin = new System.Windows.Forms.Padding(4);
            this.optAlmGeneral.Name = "optAlmGeneral";
            this.optAlmGeneral.Size = new System.Drawing.Size(107, 21);
            this.optAlmGeneral.TabIndex = 0;
            this.optAlmGeneral.TabStop = true;
            this.optAlmGeneral.Text = "Alm General";
            this.optAlmGeneral.UseVisualStyleBackColor = true;
            // 
            // optAlmSurtido
            // 
            this.optAlmSurtido.AutoSize = true;
            this.optAlmSurtido.Location = new System.Drawing.Point(177, 8);
            this.optAlmSurtido.Margin = new System.Windows.Forms.Padding(4);
            this.optAlmSurtido.Name = "optAlmSurtido";
            this.optAlmSurtido.Size = new System.Drawing.Size(104, 21);
            this.optAlmSurtido.TabIndex = 1;
            this.optAlmSurtido.Text = "Alml Surtido";
            this.optAlmSurtido.UseVisualStyleBackColor = true;
            // 
            // OptAlmlMostrador
            // 
            this.OptAlmlMostrador.AutoSize = true;
            this.OptAlmlMostrador.Location = new System.Drawing.Point(310, 8);
            this.OptAlmlMostrador.Margin = new System.Windows.Forms.Padding(4);
            this.OptAlmlMostrador.Name = "OptAlmlMostrador";
            this.OptAlmlMostrador.Size = new System.Drawing.Size(120, 21);
            this.OptAlmlMostrador.TabIndex = 2;
            this.OptAlmlMostrador.Text = "Alm Mostrador";
            this.OptAlmlMostrador.UseVisualStyleBackColor = true;
            // 
            // btnBaseSMAX
            // 
            this.btnBaseSMAX.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnBaseSMAX.Location = new System.Drawing.Point(22, 36);
            this.btnBaseSMAX.Margin = new System.Windows.Forms.Padding(4);
            this.btnBaseSMAX.Name = "btnBaseSMAX";
            this.btnBaseSMAX.Size = new System.Drawing.Size(127, 28);
            this.btnBaseSMAX.TabIndex = 3;
            this.btnBaseSMAX.Text = "Base SMAX";
            this.btnBaseSMAX.UseVisualStyleBackColor = true;
            this.btnBaseSMAX.Click += new System.EventHandler(this.btnBaseSMAX_Click);
            // 
            // btnBaseSMIN
            // 
            this.btnBaseSMIN.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnBaseSMIN.Location = new System.Drawing.Point(157, 36);
            this.btnBaseSMIN.Margin = new System.Windows.Forms.Padding(4);
            this.btnBaseSMIN.Name = "btnBaseSMIN";
            this.btnBaseSMIN.Size = new System.Drawing.Size(124, 28);
            this.btnBaseSMIN.TabIndex = 4;
            this.btnBaseSMIN.Text = "Base SMIN";
            this.btnBaseSMIN.UseVisualStyleBackColor = true;
            this.btnBaseSMIN.Click += new System.EventHandler(this.btnBaseSMIN_Click);
            // 
            // baseSMINSinPP
            // 
            this.baseSMINSinPP.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.baseSMINSinPP.Location = new System.Drawing.Point(289, 36);
            this.baseSMINSinPP.Margin = new System.Windows.Forms.Padding(4);
            this.baseSMINSinPP.Name = "baseSMINSinPP";
            this.baseSMINSinPP.Size = new System.Drawing.Size(152, 28);
            this.baseSMINSinPP.TabIndex = 5;
            this.baseSMINSinPP.Text = "Base SMIN con PP";
            this.baseSMINSinPP.UseVisualStyleBackColor = true;
            this.baseSMINSinPP.Click += new System.EventHandler(this.baseSMINSinPP_Click);
            // 
            // scape1
            // 
            this.scape1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.scape1.Form = this;
            // 
            // frmRepExist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.scape1;
            this.ClientSize = new System.Drawing.Size(454, 86);
            this.Controls.Add(this.baseSMINSinPP);
            this.Controls.Add(this.btnBaseSMIN);
            this.Controls.Add(this.btnBaseSMAX);
            this.Controls.Add(this.OptAlmlMostrador);
            this.Controls.Add(this.optAlmSurtido);
            this.Controls.Add(this.optAlmGeneral);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRepExist";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Filtro";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton optAlmGeneral;
        private System.Windows.Forms.RadioButton optAlmSurtido;
        private System.Windows.Forms.RadioButton OptAlmlMostrador;
        private System.Windows.Forms.Button btnBaseSMAX;
        private System.Windows.Forms.Button btnBaseSMIN;
        private System.Windows.Forms.Button baseSMINSinPP;
        private UserControls.Scape scape1;
    }
}