namespace SIP
{
    partial class frmRepOrdProd : frmInputBox
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
            this.lblTitulo.Size = new System.Drawing.Size(66, 13);
            this.lblTitulo.Text = "Órden Inicial";
            // 
            // btnAceptar
            // 
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // frmRepOrdProd
            // 
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(353, 117);
            this.Name = "frmRepOrdProd";
            this.Text = "Reporte de Órdenes";
            this.Load += new System.EventHandler(this.frmRepOrdProd_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        
        /*private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
        private UserControls.Scape scape1;
        private UserControls.NumericTextBox NTxtOrden;
        */
    }

}