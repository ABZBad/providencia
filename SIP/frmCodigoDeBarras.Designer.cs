namespace SIP
{
    partial class frmCodigoDeBarras
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCodigoDeBarras));
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.crystalReportViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "0O25zr1.jpg");
            // 
            // crystalReportViewer
            // 
            this.crystalReportViewer.ActiveViewIndex = -1;
            this.crystalReportViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.crystalReportViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer.DisplayStatusBar = false;
            this.crystalReportViewer.Location = new System.Drawing.Point(12, 12);
            this.crystalReportViewer.Name = "crystalReportViewer";
            this.crystalReportViewer.Size = new System.Drawing.Size(559, 583);
            this.crystalReportViewer.TabIndex = 2;
            this.crystalReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // frmCodigoDeBarras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 607);
            this.Controls.Add(this.crystalReportViewer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmCodigoDeBarras";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Impresión de Código de Barras";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmCodigoDeBarras_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imgList;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer;

    }
}