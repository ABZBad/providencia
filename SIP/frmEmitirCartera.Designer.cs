namespace SIP
{
    partial class frmEmitirCartera
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnEmitir = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtAgente = new SIP.UserControls.TextBoxEx();
            this.txtMesesMC = new SIP.UserControls.NumericTextBox();
            this.txtAniosAtras = new SIP.UserControls.NumericTextBox();
            this.txtAniosAEmitir = new SIP.UserControls.NumericTextBox();
            this.scape1 = new SIP.UserControls.Scape();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Años a emitir:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Años a atrás:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(175, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "Meses para considerar CM:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 18);
            this.label4.TabIndex = 3;
            this.label4.Text = "Agente:";
            // 
            // btnEmitir
            // 
            this.btnEmitir.Location = new System.Drawing.Point(81, 105);
            this.btnEmitir.Name = "btnEmitir";
            this.btnEmitir.Size = new System.Drawing.Size(113, 23);
            this.btnEmitir.TabIndex = 4;
            this.btnEmitir.Text = "Emitir";
            this.btnEmitir.UseVisualStyleBackColor = true;
            this.btnEmitir.Click += new System.EventHandler(this.btnEmitir_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            // 
            // txtAgente
            // 
            this.txtAgente.Location = new System.Drawing.Point(176, 77);
            this.txtAgente.Name = "txtAgente";
            this.txtAgente.OnlyUpperCase = true;
            this.txtAgente.Size = new System.Drawing.Size(81, 20);
            this.txtAgente.TabIndex = 3;
            // 
            // txtMesesMC
            // 
            this.txtMesesMC.Location = new System.Drawing.Point(176, 54);
            this.txtMesesMC.MaxValue = 0;
            this.txtMesesMC.MinValue = 0;
            this.txtMesesMC.Name = "txtMesesMC";
            this.txtMesesMC.Size = new System.Drawing.Size(81, 20);
            this.txtMesesMC.TabIndex = 2;
            this.txtMesesMC.Text = "13";
            // 
            // txtAniosAtras
            // 
            this.txtAniosAtras.Location = new System.Drawing.Point(176, 31);
            this.txtAniosAtras.MaxValue = 99999;
            this.txtAniosAtras.MinValue = -99999;
            this.txtAniosAtras.Name = "txtAniosAtras";
            this.txtAniosAtras.Size = new System.Drawing.Size(81, 20);
            this.txtAniosAtras.TabIndex = 1;
            this.txtAniosAtras.Text = "2";
            // 
            // txtAniosAEmitir
            // 
            this.txtAniosAEmitir.Location = new System.Drawing.Point(176, 8);
            this.txtAniosAEmitir.MaxValue = 2050;
            this.txtAniosAEmitir.MinValue = 1985;
            this.txtAniosAEmitir.Name = "txtAniosAEmitir";
            this.txtAniosAEmitir.Size = new System.Drawing.Size(81, 20);
            this.txtAniosAEmitir.TabIndex = 0;
            this.txtAniosAEmitir.Text = "2012";
            // 
            // scape1
            // 
            this.scape1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.scape1.Form = this;
            // 
            // frmEmitirCartera
            // 
            this.AcceptButton = this.btnEmitir;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.scape1;
            this.ClientSize = new System.Drawing.Size(276, 140);
            this.Controls.Add(this.txtAgente);
            this.Controls.Add(this.txtMesesMC);
            this.Controls.Add(this.txtAniosAtras);
            this.Controls.Add(this.txtAniosAEmitir);
            this.Controls.Add(this.btnEmitir);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEmitirCartera";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Emisión de Cartera";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnEmitir;
        private UserControls.NumericTextBox txtAniosAEmitir;
        private UserControls.NumericTextBox txtAniosAtras;
        private UserControls.NumericTextBox txtMesesMC;
        private UserControls.TextBoxEx txtAgente;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private UserControls.Scape scape1;
    }
}