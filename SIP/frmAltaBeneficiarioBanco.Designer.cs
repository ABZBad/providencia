namespace SIP
{
    partial class frmAltaBeneficiarioBanco
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
            this.btnActualizarClientes = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.cmbBeneficiarioNombre = new System.Windows.Forms.ComboBox();
            this.cmbBeneficiarioRFC = new System.Windows.Forms.ComboBox();
            this.cmbBeneficiarioClave = new System.Windows.Forms.ComboBox();
            this.lblBeneficario = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtCuentaContable = new System.Windows.Forms.TextBox();
            this.lblCuentaContable = new System.Windows.Forms.Label();
            this.lblAccion = new System.Windows.Forms.Label();
            this.lblRFCValidacion = new System.Windows.Forms.Label();
            this.lblReferencia = new System.Windows.Forms.Label();
            this.txtGenerales = new System.Windows.Forms.TextBox();
            this.txtRFC = new System.Windows.Forms.TextBox();
            this.lblRFC = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtClave = new System.Windows.Forms.TextBox();
            this.lblClave = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chkBancoExterno = new System.Windows.Forms.CheckBox();
            this.txtClabe = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCuenta = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSucursal = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtReferencia = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBanco = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblClaveTitulo = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnActualizarClientes
            // 
            this.btnActualizarClientes.BackgroundImage = global::SIP.Properties.Resources.refresh;
            this.btnActualizarClientes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnActualizarClientes.FlatAppearance.BorderSize = 0;
            this.btnActualizarClientes.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnActualizarClientes.Location = new System.Drawing.Point(775, 37);
            this.btnActualizarClientes.Name = "btnActualizarClientes";
            this.btnActualizarClientes.Size = new System.Drawing.Size(30, 30);
            this.btnActualizarClientes.TabIndex = 7;
            this.btnActualizarClientes.Text = "...";
            this.btnActualizarClientes.UseVisualStyleBackColor = true;
            this.btnActualizarClientes.Click += new System.EventHandler(this.btnActualizarClientes_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.BackgroundImage = global::SIP.Properties.Resources.file_search;
            this.btnEditar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEditar.Location = new System.Drawing.Point(739, 37);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(30, 30);
            this.btnEditar.TabIndex = 10;
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // cmbBeneficiarioNombre
            // 
            this.cmbBeneficiarioNombre.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbBeneficiarioNombre.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbBeneficiarioNombre.FormattingEnabled = true;
            this.cmbBeneficiarioNombre.Location = new System.Drawing.Point(420, 39);
            this.cmbBeneficiarioNombre.Name = "cmbBeneficiarioNombre";
            this.cmbBeneficiarioNombre.Size = new System.Drawing.Size(313, 21);
            this.cmbBeneficiarioNombre.TabIndex = 9;
            // 
            // cmbBeneficiarioRFC
            // 
            this.cmbBeneficiarioRFC.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbBeneficiarioRFC.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbBeneficiarioRFC.FormattingEnabled = true;
            this.cmbBeneficiarioRFC.Location = new System.Drawing.Point(295, 39);
            this.cmbBeneficiarioRFC.Name = "cmbBeneficiarioRFC";
            this.cmbBeneficiarioRFC.Size = new System.Drawing.Size(119, 21);
            this.cmbBeneficiarioRFC.TabIndex = 8;
            // 
            // cmbBeneficiarioClave
            // 
            this.cmbBeneficiarioClave.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbBeneficiarioClave.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbBeneficiarioClave.FormattingEnabled = true;
            this.cmbBeneficiarioClave.Location = new System.Drawing.Point(224, 39);
            this.cmbBeneficiarioClave.Name = "cmbBeneficiarioClave";
            this.cmbBeneficiarioClave.Size = new System.Drawing.Size(65, 21);
            this.cmbBeneficiarioClave.TabIndex = 5;
            // 
            // lblBeneficario
            // 
            this.lblBeneficario.AutoSize = true;
            this.lblBeneficario.Location = new System.Drawing.Point(30, 42);
            this.lblBeneficario.Name = "lblBeneficario";
            this.lblBeneficario.Size = new System.Drawing.Size(188, 13);
            this.lblBeneficario.TabIndex = 6;
            this.lblBeneficario.Text = "Seleccione un Cliente para su edición:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCuentaContable);
            this.groupBox1.Controls.Add(this.lblCuentaContable);
            this.groupBox1.Controls.Add(this.lblAccion);
            this.groupBox1.Controls.Add(this.lblRFCValidacion);
            this.groupBox1.Controls.Add(this.lblReferencia);
            this.groupBox1.Controls.Add(this.txtGenerales);
            this.groupBox1.Controls.Add(this.txtRFC);
            this.groupBox1.Controls.Add(this.lblRFC);
            this.groupBox1.Controls.Add(this.txtNombre);
            this.groupBox1.Controls.Add(this.lblNombre);
            this.groupBox1.Controls.Add(this.txtClave);
            this.groupBox1.Controls.Add(this.lblClave);
            this.groupBox1.Location = new System.Drawing.Point(12, 66);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(802, 164);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos Generales";
            // 
            // txtCuentaContable
            // 
            this.txtCuentaContable.Location = new System.Drawing.Point(120, 136);
            this.txtCuentaContable.Name = "txtCuentaContable";
            this.txtCuentaContable.Size = new System.Drawing.Size(131, 20);
            this.txtCuentaContable.TabIndex = 5;
            // 
            // lblCuentaContable
            // 
            this.lblCuentaContable.AutoSize = true;
            this.lblCuentaContable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCuentaContable.Location = new System.Drawing.Point(27, 136);
            this.lblCuentaContable.Name = "lblCuentaContable";
            this.lblCuentaContable.Size = new System.Drawing.Size(88, 13);
            this.lblCuentaContable.TabIndex = 52;
            this.lblCuentaContable.Text = "Cta. Contable:";
            // 
            // lblAccion
            // 
            this.lblAccion.AutoSize = true;
            this.lblAccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccion.ForeColor = System.Drawing.Color.Green;
            this.lblAccion.Location = new System.Drawing.Point(257, 49);
            this.lblAccion.Name = "lblAccion";
            this.lblAccion.Size = new System.Drawing.Size(46, 13);
            this.lblAccion.TabIndex = 50;
            this.lblAccion.Text = "Acción";
            this.lblAccion.Visible = false;
            // 
            // lblRFCValidacion
            // 
            this.lblRFCValidacion.AutoSize = true;
            this.lblRFCValidacion.Location = new System.Drawing.Point(626, 56);
            this.lblRFCValidacion.Name = "lblRFCValidacion";
            this.lblRFCValidacion.Size = new System.Drawing.Size(0, 13);
            this.lblRFCValidacion.TabIndex = 35;
            // 
            // lblReferencia
            // 
            this.lblReferencia.AutoSize = true;
            this.lblReferencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReferencia.Location = new System.Drawing.Point(27, 101);
            this.lblReferencia.Name = "lblReferencia";
            this.lblReferencia.Size = new System.Drawing.Size(68, 13);
            this.lblReferencia.TabIndex = 34;
            this.lblReferencia.Text = "Generales:";
            // 
            // txtGenerales
            // 
            this.txtGenerales.Location = new System.Drawing.Point(120, 98);
            this.txtGenerales.Multiline = true;
            this.txtGenerales.Name = "txtGenerales";
            this.txtGenerales.Size = new System.Drawing.Size(676, 32);
            this.txtGenerales.TabIndex = 4;
            // 
            // txtRFC
            // 
            this.txtRFC.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRFC.Location = new System.Drawing.Point(629, 72);
            this.txtRFC.Name = "txtRFC";
            this.txtRFC.Size = new System.Drawing.Size(167, 20);
            this.txtRFC.TabIndex = 3;
            this.txtRFC.Leave += new System.EventHandler(this.txtRFC_Leave);
            // 
            // lblRFC
            // 
            this.lblRFC.AutoSize = true;
            this.lblRFC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRFC.Location = new System.Drawing.Point(588, 75);
            this.lblRFC.Name = "lblRFC";
            this.lblRFC.Size = new System.Drawing.Size(35, 13);
            this.lblRFC.TabIndex = 23;
            this.lblRFC.Text = "RFC:";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(120, 72);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(462, 20);
            this.txtNombre.TabIndex = 2;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre.Location = new System.Drawing.Point(27, 75);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(54, 13);
            this.lblNombre.TabIndex = 21;
            this.lblNombre.Text = "Nombre:";
            // 
            // txtClave
            // 
            this.txtClave.Enabled = false;
            this.txtClave.Location = new System.Drawing.Point(120, 46);
            this.txtClave.Name = "txtClave";
            this.txtClave.Size = new System.Drawing.Size(131, 20);
            this.txtClave.TabIndex = 1;
            // 
            // lblClave
            // 
            this.lblClave.AutoSize = true;
            this.lblClave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClave.Location = new System.Drawing.Point(27, 49);
            this.lblClave.Name = "lblClave";
            this.lblClave.Size = new System.Drawing.Size(43, 13);
            this.lblClave.TabIndex = 18;
            this.lblClave.Text = "Clave:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtDescripcion);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.chkBancoExterno);
            this.groupBox2.Controls.Add(this.txtClabe);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtCuenta);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtSucursal);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtReferencia);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtBanco);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(12, 236);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(802, 208);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Datos Bancarios";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(84, 142);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(709, 51);
            this.txtDescripcion.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(1, 145);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 13);
            this.label6.TabIndex = 46;
            this.label6.Text = "Descripción:";
            // 
            // chkBancoExterno
            // 
            this.chkBancoExterno.AutoSize = true;
            this.chkBancoExterno.Location = new System.Drawing.Point(84, 119);
            this.chkBancoExterno.Name = "chkBancoExterno";
            this.chkBancoExterno.Size = new System.Drawing.Size(111, 17);
            this.chkBancoExterno.TabIndex = 11;
            this.chkBancoExterno.Text = "Es Banco Externo";
            this.chkBancoExterno.UseVisualStyleBackColor = true;
            // 
            // txtClabe
            // 
            this.txtClabe.Location = new System.Drawing.Point(601, 93);
            this.txtClabe.Name = "txtClabe";
            this.txtClabe.Size = new System.Drawing.Size(192, 20);
            this.txtClabe.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(552, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 43;
            this.label5.Text = "Clabe:";
            // 
            // txtCuenta
            // 
            this.txtCuenta.Location = new System.Drawing.Point(345, 93);
            this.txtCuenta.Name = "txtCuenta";
            this.txtCuenta.Size = new System.Drawing.Size(199, 20);
            this.txtCuenta.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(288, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 41;
            this.label4.Text = "Cuenta:";
            // 
            // txtSucursal
            // 
            this.txtSucursal.Location = new System.Drawing.Point(84, 67);
            this.txtSucursal.Name = "txtSucursal";
            this.txtSucursal.Size = new System.Drawing.Size(709, 20);
            this.txtSucursal.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(18, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 39;
            this.label3.Text = "Sucrusal:";
            // 
            // txtReferencia
            // 
            this.txtReferencia.Location = new System.Drawing.Point(84, 93);
            this.txtReferencia.Name = "txtReferencia";
            this.txtReferencia.Size = new System.Drawing.Size(167, 20);
            this.txtReferencia.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 37;
            this.label2.Text = "Referencia:";
            // 
            // txtBanco
            // 
            this.txtBanco.Location = new System.Drawing.Point(84, 41);
            this.txtBanco.Name = "txtBanco";
            this.txtBanco.Size = new System.Drawing.Size(709, 20);
            this.txtBanco.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 35;
            this.label1.Text = "Banco:";
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackgroundImage = global::SIP.Properties.Resources.plus;
            this.btnNuevo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNuevo.Location = new System.Drawing.Point(12, 450);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(30, 30);
            this.btnNuevo.TabIndex = 38;
            this.btnNuevo.UseVisualStyleBackColor = true;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackgroundImage = global::SIP.Properties.Resources.save2;
            this.btnGuardar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGuardar.Location = new System.Drawing.Point(707, 450);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(30, 30);
            this.btnGuardar.TabIndex = 39;
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Visible = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(743, 450);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(71, 30);
            this.btnCancelar.TabIndex = 40;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Visible = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // lblClaveTitulo
            // 
            this.lblClaveTitulo.AutoSize = true;
            this.lblClaveTitulo.Location = new System.Drawing.Point(242, 23);
            this.lblClaveTitulo.Name = "lblClaveTitulo";
            this.lblClaveTitulo.Size = new System.Drawing.Size(34, 13);
            this.lblClaveTitulo.TabIndex = 41;
            this.lblClaveTitulo.Text = "Clave";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(345, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(28, 13);
            this.label7.TabIndex = 42;
            this.label7.Text = "RFC";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(550, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 13);
            this.label8.TabIndex = 43;
            this.label8.Text = "Nombre";
            // 
            // frmAltaBeneficiarioBanco
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 488);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblClaveTitulo);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnActualizarClientes);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.cmbBeneficiarioNombre);
            this.Controls.Add(this.cmbBeneficiarioRFC);
            this.Controls.Add(this.cmbBeneficiarioClave);
            this.Controls.Add(this.lblBeneficario);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmAltaBeneficiarioBanco";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Alta / Edición de Beneficiario Banco 4.0";
            this.Load += new System.EventHandler(this.frmAltaBeneficiarioBanco_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnActualizarClientes;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.ComboBox cmbBeneficiarioNombre;
        private System.Windows.Forms.ComboBox cmbBeneficiarioRFC;
        private System.Windows.Forms.ComboBox cmbBeneficiarioClave;
        private System.Windows.Forms.Label lblBeneficario;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtClave;
        private System.Windows.Forms.Label lblClave;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtRFC;
        private System.Windows.Forms.Label lblRFC;
        private System.Windows.Forms.Label lblReferencia;
        private System.Windows.Forms.TextBox txtGenerales;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtClabe;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCuenta;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSucursal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtReferencia;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBanco;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkBancoExterno;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblClaveTitulo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblRFCValidacion;
        private System.Windows.Forms.Label lblAccion;
        private System.Windows.Forms.TextBox txtCuentaContable;
        private System.Windows.Forms.Label lblCuentaContable;
    }
}