namespace SIP
{
    partial class frmControlCambios
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
            this.txtDetalleVersiones = new System.Windows.Forms.TextBox();
            this.LblCurrentVersion = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.scape1 = new SIP.UserControls.Scape();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(859, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Control de Cambios";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDetalleVersiones
            // 
            this.txtDetalleVersiones.AcceptsReturn = true;
            this.txtDetalleVersiones.AcceptsTab = true;
            this.txtDetalleVersiones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDetalleVersiones.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDetalleVersiones.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDetalleVersiones.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtDetalleVersiones.Location = new System.Drawing.Point(11, 57);
            this.txtDetalleVersiones.Multiline = true;
            this.txtDetalleVersiones.Name = "txtDetalleVersiones";
            this.txtDetalleVersiones.ReadOnly = true;
            this.txtDetalleVersiones.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDetalleVersiones.Size = new System.Drawing.Size(859, 412);
            this.txtDetalleVersiones.TabIndex = 3;
            this.txtDetalleVersiones.WordWrap = false;
            // 
            // LblCurrentVersion
            // 
            this.LblCurrentVersion.AutoSize = true;
            this.LblCurrentVersion.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCurrentVersion.Location = new System.Drawing.Point(10, 38);
            this.LblCurrentVersion.Name = "LblCurrentVersion";
            this.LblCurrentVersion.Size = new System.Drawing.Size(44, 15);
            this.LblCurrentVersion.TabIndex = 2;
            this.LblCurrentVersion.Text = "8.0.1.3";
            this.LblCurrentVersion.Visible = false;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(814, 30);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 25);
            this.button1.TabIndex = 0;
            this.button1.Text = "Cerrar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // scape1
            // 
            this.scape1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.scape1.Form = this;
            // 
            // frmControlCambios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.scape1;
            this.ClientSize = new System.Drawing.Size(882, 474);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.LblCurrentVersion);
            this.Controls.Add(this.txtDetalleVersiones);
            this.Controls.Add(this.label1);
            this.Name = "frmControlCambios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmControlCambios_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDetalleVersiones;
        private System.Windows.Forms.Label LblCurrentVersion;
        private System.Windows.Forms.Button button1;
        private UserControls.Scape scape1;
    }
}