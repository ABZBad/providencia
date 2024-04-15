namespace SIP
{
    partial class frmControlPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmControlPanel));
            this.menuPrincipal = new System.Windows.Forms.MenuStrip();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnBuscarCliente = new System.Windows.Forms.Button();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.scape1 = new SIP.UserControls.Scape();
            this.lblVer = new System.Windows.Forms.Label();
            this.lblPrueba = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuPrincipal
            // 
            this.menuPrincipal.Location = new System.Drawing.Point(0, 0);
            this.menuPrincipal.Name = "menuPrincipal";
            this.menuPrincipal.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuPrincipal.Size = new System.Drawing.Size(636, 24);
            this.menuPrincipal.TabIndex = 0;
            this.menuPrincipal.Text = "menuStrip1";
            this.menuPrincipal.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuPrincipal_ItemClicked);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackgroundImage = global::SIP.Properties.Resources.controlPanelBackGround;
            this.pictureBox1.Location = new System.Drawing.Point(1, 31);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(633, 44);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // btnBuscarCliente
            // 
            this.btnBuscarCliente.Enabled = false;
            this.btnBuscarCliente.FlatAppearance.BorderSize = 0;
            this.btnBuscarCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarCliente.Image = global::SIP.Properties.Resources.ToolBar1;
            this.btnBuscarCliente.Location = new System.Drawing.Point(3, 30);
            this.btnBuscarCliente.Margin = new System.Windows.Forms.Padding(4);
            this.btnBuscarCliente.Name = "btnBuscarCliente";
            this.btnBuscarCliente.Size = new System.Drawing.Size(47, 42);
            this.btnBuscarCliente.TabIndex = 2;
            this.btnBuscarCliente.UseVisualStyleBackColor = true;
            this.btnBuscarCliente.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtCliente
            // 
            this.txtCliente.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtCliente.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtCliente.Location = new System.Drawing.Point(51, 39);
            this.txtCliente.Margin = new System.Windows.Forms.Padding(4);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(365, 22);
            this.txtCliente.TabIndex = 4;
            this.txtCliente.Visible = false;
            this.txtCliente.TextChanged += new System.EventHandler(this.txtCliente_TextChanged);
            // 
            // scape1
            // 
            this.scape1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.scape1.Form = this;
            // 
            // lblVer
            // 
            this.lblVer.AutoSize = true;
            this.lblVer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(25)))));
            this.lblVer.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblVer.Location = new System.Drawing.Point(436, 55);
            this.lblVer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVer.Name = "lblVer";
            this.lblVer.Size = new System.Drawing.Size(53, 15);
            this.lblVer.TabIndex = 5;
            this.lblVer.Text = "V.8.0.0.0";
            // 
            // lblPrueba
            // 
            this.lblPrueba.AutoSize = true;
            this.lblPrueba.ForeColor = System.Drawing.Color.Red;
            this.lblPrueba.Location = new System.Drawing.Point(11, 75);
            this.lblPrueba.Name = "lblPrueba";
            this.lblPrueba.Size = new System.Drawing.Size(195, 17);
            this.lblPrueba.TabIndex = 6;
            this.lblPrueba.Text = "Prueba de control de pedidos";
            this.lblPrueba.Visible = false;
            // 
            // frmControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(59)))), ((int)(((byte)(56)))));
            this.CancelButton = this.scape1;
            this.ClientSize = new System.Drawing.Size(636, 97);
            this.Controls.Add(this.lblPrueba);
            this.Controls.Add(this.lblVer);
            this.Controls.Add(this.txtCliente);
            this.Controls.Add(this.btnBuscarCliente);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuPrincipal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuPrincipal;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "frmControlPanel";
            this.Activated += new System.EventHandler(this.frmControlPanel_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmControlPanel_FormClosing);
            this.Load += new System.EventHandler(this.frmControlPanel_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmControlPanel_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnBuscarCliente;
        private System.Windows.Forms.TextBox txtCliente;
        private UserControls.Scape scape1;
        public System.Windows.Forms.MenuStrip menuPrincipal;
        private System.Windows.Forms.Label lblVer;
        private System.Windows.Forms.Label lblPrueba;
    }
}