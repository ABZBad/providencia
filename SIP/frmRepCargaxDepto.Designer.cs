namespace SIP
{
    partial class frmRepCargaxDepto
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
            this.cmbDepartamentos = new System.Windows.Forms.ComboBox();
            this.btnEmitir = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // scape1
            // 
            this.scape1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.scape1.Form = this;
            // 
            // cmbDepartamentos
            // 
            this.cmbDepartamentos.FormattingEnabled = true;
            this.cmbDepartamentos.Location = new System.Drawing.Point(12, 41);
            this.cmbDepartamentos.Name = "cmbDepartamentos";
            this.cmbDepartamentos.Size = new System.Drawing.Size(237, 21);
            this.cmbDepartamentos.TabIndex = 0;
            // 
            // btnEmitir
            // 
            this.btnEmitir.Location = new System.Drawing.Point(96, 68);
            this.btnEmitir.Name = "btnEmitir";
            this.btnEmitir.Size = new System.Drawing.Size(75, 23);
            this.btnEmitir.TabIndex = 1;
            this.btnEmitir.Text = "Emitir";
            this.btnEmitir.UseVisualStyleBackColor = true;
            this.btnEmitir.Click += new System.EventHandler(this.btnEmitir_Click);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Location = new System.Drawing.Point(12, 25);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(144, 13);
            this.lblTitulo.TabIndex = 2;
            this.lblTitulo.Text = "Selecciona el Departamento:";
            // 
            // frmRepCargaxDepto
            // 
            this.AcceptButton = this.btnEmitir;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.scape1;
            this.ClientSize = new System.Drawing.Size(261, 103);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.btnEmitir);
            this.Controls.Add(this.cmbDepartamentos);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRepCargaxDepto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cargas por departamento PROCESOS";
            this.Load += new System.EventHandler(this.frmRepCargaxDepto_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UserControls.Scape scape1;
        private System.Windows.Forms.Button btnEmitir;
        private System.Windows.Forms.ComboBox cmbDepartamentos;
        private System.Windows.Forms.Label lblTitulo;
    }
}