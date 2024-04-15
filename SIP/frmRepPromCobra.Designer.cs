namespace SIP
{
    partial class frmRepPromCobra
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
            this.lblAgente = new System.Windows.Forms.Label();
            this.btnContinuar = new System.Windows.Forms.Button();
            this.txtAgente = new SIP.UserControls.TextBoxEx();
            this.scape1 = new SIP.UserControls.Scape();
            this.SuspendLayout();
            // 
            // lblAgente
            // 
            this.lblAgente.AutoSize = true;
            this.lblAgente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAgente.Location = new System.Drawing.Point(38, 8);
            this.lblAgente.Name = "lblAgente";
            this.lblAgente.Size = new System.Drawing.Size(47, 13);
            this.lblAgente.TabIndex = 1;
            this.lblAgente.Text = "Agente";
            // 
            // btnContinuar
            // 
            this.btnContinuar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnContinuar.Location = new System.Drawing.Point(16, 53);
            this.btnContinuar.Name = "btnContinuar";
            this.btnContinuar.Size = new System.Drawing.Size(84, 23);
            this.btnContinuar.TabIndex = 1;
            this.btnContinuar.Text = "Continuar";
            this.btnContinuar.UseVisualStyleBackColor = true;
            this.btnContinuar.Click += new System.EventHandler(this.btnContinuar_Click);
            // 
            // txtAgente
            // 
            this.txtAgente.Location = new System.Drawing.Point(20, 27);
            this.txtAgente.Name = "txtAgente";
            this.txtAgente.OnlyUpperCase = true;
            this.txtAgente.Size = new System.Drawing.Size(80, 20);
            this.txtAgente.TabIndex = 0;
            // 
            // scape1
            // 
            this.scape1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.scape1.Form = this;
            // 
            // frmRepPromCobra
            // 
            this.AcceptButton = this.btnContinuar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.scape1;
            this.ClientSize = new System.Drawing.Size(128, 84);
            this.Controls.Add(this.txtAgente);
            this.Controls.Add(this.btnContinuar);
            this.Controls.Add(this.lblAgente);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRepPromCobra";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pronóstico";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAgente;
        private System.Windows.Forms.Button btnContinuar;
        private UserControls.TextBoxEx txtAgente;
        private UserControls.Scape scape1;
    }
}