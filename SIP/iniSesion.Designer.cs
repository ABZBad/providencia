namespace SIP
{
    partial class iniSesion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(iniSesion));
            this.lblFullVersion = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtUsuario = new SIP.UserControls.TextBoxEx();
            this.txtContraseña = new SIP.UserControls.TextBoxEx();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnContinuar = new System.Windows.Forms.Button();
            this.btnCambios = new System.Windows.Forms.Button();
            this.lblDetalleVersion = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblFullVersion
            // 
            this.lblFullVersion.Font = new System.Drawing.Font("Trebuchet MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFullVersion.Location = new System.Drawing.Point(3, 7);
            this.lblFullVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFullVersion.Name = "lblFullVersion";
            this.lblFullVersion.Size = new System.Drawing.Size(352, 59);
            this.lblFullVersion.TabIndex = 0;
            this.lblFullVersion.Text = "Accesar a [AppShortName] [AppFullVer]";
            this.lblFullVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(45, 70);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(275, 36);
            this.label2.TabIndex = 1;
            this.label2.Text = "Para poder ingresar al sistema, por favor introduzca sus credenciales de acceso.";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(64, 114);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 26);
            this.label3.TabIndex = 2;
            this.label3.Text = "Usuario:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(28, 153);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 26);
            this.label4.TabIndex = 3;
            this.label4.Text = "Contraseña:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtUsuario
            // 
            this.txtUsuario.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsuario.Location = new System.Drawing.Point(168, 114);
            this.txtUsuario.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.OnlyUpperCase = false;
            this.txtUsuario.Size = new System.Drawing.Size(164, 31);
            this.txtUsuario.TabIndex = 0;
            this.txtUsuario.Enter += new System.EventHandler(this.txtUsuario_Enter);
            this.txtUsuario.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUsuario_KeyDown);
            // 
            // txtContraseña
            // 
            this.txtContraseña.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContraseña.Location = new System.Drawing.Point(168, 153);
            this.txtContraseña.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtContraseña.Name = "txtContraseña";
            this.txtContraseña.OnlyUpperCase = false;
            this.txtContraseña.PasswordChar = '•';
            this.txtContraseña.Size = new System.Drawing.Size(164, 31);
            this.txtContraseña.TabIndex = 1;
            this.txtContraseña.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUsuario_KeyDown);
            // 
            // btnCerrar
            // 
            this.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCerrar.Location = new System.Drawing.Point(152, 197);
            this.btnCerrar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(73, 28);
            this.btnCerrar.TabIndex = 3;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnContinuar
            // 
            this.btnContinuar.Location = new System.Drawing.Point(233, 197);
            this.btnContinuar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnContinuar.Name = "btnContinuar";
            this.btnContinuar.Size = new System.Drawing.Size(100, 28);
            this.btnContinuar.TabIndex = 2;
            this.btnContinuar.Text = "Continuar";
            this.btnContinuar.UseVisualStyleBackColor = true;
            this.btnContinuar.Click += new System.EventHandler(this.btnContinuar_Click);
            // 
            // btnCambios
            // 
            this.btnCambios.Location = new System.Drawing.Point(192, 230);
            this.btnCambios.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCambios.Name = "btnCambios";
            this.btnCambios.Size = new System.Drawing.Size(141, 26);
            this.btnCambios.TabIndex = 5;
            this.btnCambios.Text = "Control de Cambios";
            this.btnCambios.UseVisualStyleBackColor = true;
            this.btnCambios.Click += new System.EventHandler(this.btnCambios_Click);
            // 
            // lblDetalleVersion
            // 
            this.lblDetalleVersion.Location = new System.Drawing.Point(3, 257);
            this.lblDetalleVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDetalleVersion.Name = "lblDetalleVersion";
            this.lblDetalleVersion.Size = new System.Drawing.Size(335, 28);
            this.lblDetalleVersion.TabIndex = 9;
            this.lblDetalleVersion.Text = "label1";
            this.lblDetalleVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblDetalleVersion.Click += new System.EventHandler(this.lblDetalleVersion_Click);
            // 
            // iniSesion
            // 
            this.AcceptButton = this.btnContinuar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCerrar;
            this.ClientSize = new System.Drawing.Size(368, 349);
            this.ControlBox = false;
            this.Controls.Add(this.lblDetalleVersion);
            this.Controls.Add(this.btnCambios);
            this.Controls.Add(this.btnContinuar);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.txtContraseña);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblFullVersion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "iniSesion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Autentificación de Usuario";
            this.Load += new System.EventHandler(this.iniSesion_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFullVersion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private SIP.UserControls.TextBoxEx txtUsuario;
        private SIP.UserControls.TextBoxEx txtContraseña;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnContinuar;
        private System.Windows.Forms.Button btnCambios;
        private System.Windows.Forms.Label lblDetalleVersion;
    }
}