namespace SIP
{
    partial class frmCaptCostProcsInput
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
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Size = new System.Drawing.Size(95, 13);
            this.lblTitulo.Text = "Número de Pedido";
            // 
            // btnAceptar
            // 
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // frmCaptCostProcsInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(353, 117);
            this.Name = "frmCaptCostProcsInput";
            this.Text = "Captura de Costos de Procesos";
            this.Load += new System.EventHandler(this.frmCaptCostProcs_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}