namespace SIP
{
    partial class frmDatosFacturaBordado
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnEmitir = new System.Windows.Forms.Button();
            this.txtFactura = new SIP.UserControls.TextBoxEx();
            this.txtMaquilero = new SIP.UserControls.NumericTextBox();
            this.scape1 = new SIP.UserControls.Scape();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Maquilero";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(178, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Factura";
            // 
            // btnEmitir
            // 
            this.btnEmitir.Location = new System.Drawing.Point(123, 47);
            this.btnEmitir.Name = "btnEmitir";
            this.btnEmitir.Size = new System.Drawing.Size(75, 23);
            this.btnEmitir.TabIndex = 4;
            this.btnEmitir.Text = "Emitir";
            this.btnEmitir.UseVisualStyleBackColor = true;
            this.btnEmitir.Click += new System.EventHandler(this.btnEmitir_Click);
            // 
            // txtFactura
            // 
            this.txtFactura.Location = new System.Drawing.Point(227, 11);
            this.txtFactura.Name = "txtFactura";
            this.txtFactura.OnlyUpperCase = false;
            this.txtFactura.Size = new System.Drawing.Size(85, 20);
            this.txtFactura.TabIndex = 3;
            // 
            // txtMaquilero
            // 
            this.txtMaquilero.Location = new System.Drawing.Point(72, 11);
            this.txtMaquilero.MaxValue = 0;
            this.txtMaquilero.MinValue = 0;
            this.txtMaquilero.Name = "txtMaquilero";
            this.txtMaquilero.Size = new System.Drawing.Size(85, 20);
            this.txtMaquilero.TabIndex = 2;
            // 
            // scape1
            // 
            this.scape1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.scape1.Form = this;
            // 
            // frmDatosFacturaBordado
            // 
            this.AcceptButton = this.btnEmitir;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.scape1;
            this.ClientSize = new System.Drawing.Size(332, 82);
            this.Controls.Add(this.btnEmitir);
            this.Controls.Add(this.txtFactura);
            this.Controls.Add(this.txtMaquilero);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDatosFacturaBordado";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Costos de Bordado - Facturas Maquila";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private SIP.UserControls.NumericTextBox txtMaquilero;
        private SIP.UserControls.TextBoxEx txtFactura;
        private System.Windows.Forms.Button btnEmitir;
        private UserControls.Scape scape1;
    }
}