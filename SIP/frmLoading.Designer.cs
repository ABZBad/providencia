namespace SIP
{
    partial class frmLoading
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
            this.lblNombreProceso = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblNombreProceso
            // 
            this.lblNombreProceso.AutoSize = true;
            this.lblNombreProceso.Location = new System.Drawing.Point(13, 8);
            this.lblNombreProceso.Name = "lblNombreProceso";
            this.lblNombreProceso.Size = new System.Drawing.Size(93, 13);
            this.lblNombreProceso.TabIndex = 0;
            this.lblNombreProceso.Text = "lblNombreProceso";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(16, 31);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(261, 23);
            this.progressBar1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Solicitando información al servidor";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(225, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Este proceso puede durar un par de minutos...";
            // 
            // frmLoading
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 100);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblNombreProceso);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLoading";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Procesando Solicitud";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNombreProceso;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}