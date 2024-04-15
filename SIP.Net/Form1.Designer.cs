namespace SIP.Net
{
    partial class frmUpdater
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUpdater));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblCRStatus = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.picCrystal = new System.Windows.Forms.PictureBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.panelCR = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lnkDownloadCR = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.picCrystal)).BeginInit();
            this.panelCR.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(44, 31);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(192, 23);
            this.progressBar1.TabIndex = 3;
            this.progressBar1.Visible = false;
            // 
            // lblCRStatus
            // 
            this.lblCRStatus.AutoSize = true;
            this.lblCRStatus.Location = new System.Drawing.Point(94, 14);
            this.lblCRStatus.Name = "lblCRStatus";
            this.lblCRStatus.Size = new System.Drawing.Size(111, 13);
            this.lblCRStatus.TabIndex = 4;
            this.lblCRStatus.Text = "Crystal Reports 13.0.2";
            this.lblCRStatus.Visible = false;
            // 
            // picCrystal
            // 
            this.picCrystal.Location = new System.Drawing.Point(73, 12);
            this.picCrystal.Name = "picCrystal";
            this.picCrystal.Size = new System.Drawing.Size(19, 18);
            this.picCrystal.TabIndex = 5;
            this.picCrystal.TabStop = false;
            this.picCrystal.Visible = false;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(44, 11);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(143, 13);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "Revisando actualizaciones...";
            // 
            // panelCR
            // 
            this.panelCR.Controls.Add(this.lnkDownloadCR);
            this.panelCR.Controls.Add(this.label1);
            this.panelCR.Location = new System.Drawing.Point(21, 32);
            this.panelCR.Name = "panelCR";
            this.panelCR.Size = new System.Drawing.Size(218, 37);
            this.panelCR.TabIndex = 7;
            this.panelCR.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Instale SAP Crystal Reports";
            // 
            // lnkDownloadCR
            // 
            this.lnkDownloadCR.AutoSize = true;
            this.lnkDownloadCR.Location = new System.Drawing.Point(159, 6);
            this.lnkDownloadCR.Name = "lnkDownloadCR";
            this.lnkDownloadCR.Size = new System.Drawing.Size(56, 13);
            this.lnkDownloadCR.TabIndex = 1;
            this.lnkDownloadCR.TabStop = true;
            this.lnkDownloadCR.Text = "Descargar";
            this.lnkDownloadCR.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDownloadCR_LinkClicked);
            // 
            // frmUpdater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 71);
            this.Controls.Add(this.panelCR);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.picCrystal);
            this.Controls.Add(this.lblCRStatus);
            this.Controls.Add(this.progressBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUpdater";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SIP 8 : Revisor de actualizaciones";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picCrystal)).EndInit();
            this.panelCR.ResumeLayout(false);
            this.panelCR.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblCRStatus;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.PictureBox picCrystal;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel panelCR;
        private System.Windows.Forms.LinkLabel lnkDownloadCR;
        private System.Windows.Forms.Label label1;
    }
}

