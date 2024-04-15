namespace SIP
{
    partial class frmAltaAgente
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
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtIdApp = new System.Windows.Forms.TextBox();
            this.txtContrasena = new System.Windows.Forms.TextBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtContrasena2 = new System.Windows.Forms.TextBox();
            this.cmbTipoUsuario = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAcceso = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblArea = new System.Windows.Forms.Label();
            this.cmbArea = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 26);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 54);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Usuario:";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(173, 22);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtNombre.MaxLength = 300;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(247, 22);
            this.txtNombre.TabIndex = 0;
            this.txtNombre.Validating += new System.ComponentModel.CancelEventHandler(this.txtNombre_Validating);
            // 
            // txtIdApp
            // 
            this.txtIdApp.Location = new System.Drawing.Point(173, 50);
            this.txtIdApp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtIdApp.MaxLength = 250;
            this.txtIdApp.Name = "txtIdApp";
            this.txtIdApp.Size = new System.Drawing.Size(144, 22);
            this.txtIdApp.TabIndex = 1;
            this.txtIdApp.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdApp_Validating);
            // 
            // txtContrasena
            // 
            this.txtContrasena.Location = new System.Drawing.Point(173, 80);
            this.txtContrasena.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtContrasena.MaxLength = 50;
            this.txtContrasena.Name = "txtContrasena";
            this.txtContrasena.PasswordChar = '*';
            this.txtContrasena.Size = new System.Drawing.Size(144, 22);
            this.txtContrasena.TabIndex = 2;
            this.txtContrasena.Validating += new System.ComponentModel.CancelEventHandler(this.txtContrasena_Validating);
            // 
            // btnAgregar
            // 
            this.btnAgregar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAgregar.Location = new System.Drawing.Point(212, 263);
            this.btnAgregar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(100, 28);
            this.btnAgregar.TabIndex = 7;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.CausesValidation = false;
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(320, 263);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 28);
            this.btnCancelar.TabIndex = 7;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 84);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Contraseña:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 113);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(148, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Confirmar contraseña:";
            // 
            // txtContrasena2
            // 
            this.txtContrasena2.Location = new System.Drawing.Point(173, 110);
            this.txtContrasena2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtContrasena2.MaxLength = 50;
            this.txtContrasena2.Name = "txtContrasena2";
            this.txtContrasena2.PasswordChar = '*';
            this.txtContrasena2.Size = new System.Drawing.Size(144, 22);
            this.txtContrasena2.TabIndex = 3;
            this.txtContrasena2.Validating += new System.ComponentModel.CancelEventHandler(this.txtContrasena2_Validating);
            // 
            // cmbTipoUsuario
            // 
            this.cmbTipoUsuario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoUsuario.FormattingEnabled = true;
            this.cmbTipoUsuario.Items.AddRange(new object[] {
            "(Selecciona)",
            "AGT",
            "OPER",
            "SUP"});
            this.cmbTipoUsuario.Location = new System.Drawing.Point(173, 142);
            this.cmbTipoUsuario.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbTipoUsuario.Name = "cmbTipoUsuario";
            this.cmbTipoUsuario.Size = new System.Drawing.Size(144, 24);
            this.cmbTipoUsuario.TabIndex = 4;
            this.cmbTipoUsuario.Validating += new System.ComponentModel.CancelEventHandler(this.cmbTipoUsuario_Validating);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 145);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 17);
            this.label5.TabIndex = 12;
            this.label5.Text = "Tipo usuario:";
            // 
            // txtAcceso
            // 
            this.txtAcceso.Location = new System.Drawing.Point(173, 174);
            this.txtAcceso.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtAcceso.MaxLength = 250;
            this.txtAcceso.Name = "txtAcceso";
            this.txtAcceso.Size = new System.Drawing.Size(144, 22);
            this.txtAcceso.TabIndex = 5;
            this.txtAcceso.Validating += new System.ComponentModel.CancelEventHandler(this.txtAcceso_Validating);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 177);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 17);
            this.label6.TabIndex = 14;
            this.label6.Text = "Acceso:";
            // 
            // lblArea
            // 
            this.lblArea.AutoSize = true;
            this.lblArea.Location = new System.Drawing.Point(13, 207);
            this.lblArea.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(91, 17);
            this.lblArea.TabIndex = 15;
            this.lblArea.Text = "Tipo usuario:";
            // 
            // cmbArea
            // 
            this.cmbArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbArea.FormattingEnabled = true;
            this.cmbArea.Items.AddRange(new object[] {
            "(Selecciona)",
            "AGT",
            "OPER",
            "SUP"});
            this.cmbArea.Location = new System.Drawing.Point(173, 204);
            this.cmbArea.Margin = new System.Windows.Forms.Padding(4);
            this.cmbArea.Name = "cmbArea";
            this.cmbArea.Size = new System.Drawing.Size(144, 24);
            this.cmbArea.TabIndex = 6;
            // 
            // frmAltaAgente
            // 
            this.AcceptButton = this.btnAgregar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(440, 304);
            this.Controls.Add(this.cmbArea);
            this.Controls.Add(this.lblArea);
            this.Controls.Add(this.txtAcceso);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbTipoUsuario);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtContrasena2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.txtContrasena);
            this.Controls.Add(this.txtIdApp);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAltaAgente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Alta Nuevo Agente";
            this.Load += new System.EventHandler(this.frmAltaAgente_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtIdApp;
        private System.Windows.Forms.TextBox txtContrasena;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtContrasena2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAcceso;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbTipoUsuario;
        private System.Windows.Forms.ComboBox cmbArea;
        private System.Windows.Forms.Label lblArea;
    }
}