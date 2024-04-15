namespace SIP
{
    partial class frmEtiquetasEmpaque
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
            this.scape1 = new SIP.UserControls.Scape();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNumeroPedido = new SIP.UserControls.NumericTextBox();
            this.lblRazonSocial = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDireccion = new SIP.UserControls.TextBoxEx();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtContenido = new SIP.UserControls.TextBoxEx();
            this.txtConsignado = new SIP.UserControls.TextBoxEx();
            this.label6 = new System.Windows.Forms.Label();
            this.txtRemitido = new SIP.UserControls.TextBoxEx();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAntencion = new SIP.UserControls.TextBoxEx();
            this.label4 = new System.Windows.Forms.Label();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // scape1
            // 
            this.scape1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.scape1.Form = this;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pedido:";
            // 
            // txtNumeroPedido
            // 
            this.txtNumeroPedido.Location = new System.Drawing.Point(62, 9);
            this.txtNumeroPedido.MaxValue = 0;
            this.txtNumeroPedido.MinValue = 0;
            this.txtNumeroPedido.Name = "txtNumeroPedido";
            this.txtNumeroPedido.Size = new System.Drawing.Size(100, 20);
            this.txtNumeroPedido.TabIndex = 0;
            this.txtNumeroPedido.Leave += new System.EventHandler(this.txtNumeroPedido_Leave);
            // 
            // lblRazonSocial
            // 
            this.lblRazonSocial.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRazonSocial.Location = new System.Drawing.Point(13, 35);
            this.lblRazonSocial.Name = "lblRazonSocial";
            this.lblRazonSocial.Size = new System.Drawing.Size(264, 27);
            this.lblRazonSocial.TabIndex = 2;
            this.lblRazonSocial.Text = "Razón Social";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Dirección:";
            // 
            // txtDireccion
            // 
            this.txtDireccion.Location = new System.Drawing.Point(16, 79);
            this.txtDireccion.Multiline = true;
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.OnlyUpperCase = false;
            this.txtDireccion.Size = new System.Drawing.Size(261, 59);
            this.txtDireccion.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtContenido);
            this.groupBox1.Controls.Add(this.txtConsignado);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtRemitido);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtAntencion);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(12, 141);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(265, 246);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Generales";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 144);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Contenido:";
            // 
            // txtContenido
            // 
            this.txtContenido.Location = new System.Drawing.Point(8, 160);
            this.txtContenido.Multiline = true;
            this.txtContenido.Name = "txtContenido";
            this.txtContenido.OnlyUpperCase = false;
            this.txtContenido.Size = new System.Drawing.Size(249, 77);
            this.txtContenido.TabIndex = 3;
            // 
            // txtConsignado
            // 
            this.txtConsignado.Location = new System.Drawing.Point(8, 115);
            this.txtConsignado.Name = "txtConsignado";
            this.txtConsignado.OnlyUpperCase = false;
            this.txtConsignado.Size = new System.Drawing.Size(249, 20);
            this.txtConsignado.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 98);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Consignado:";
            // 
            // txtRemitido
            // 
            this.txtRemitido.Location = new System.Drawing.Point(8, 71);
            this.txtRemitido.Name = "txtRemitido";
            this.txtRemitido.OnlyUpperCase = false;
            this.txtRemitido.Size = new System.Drawing.Size(249, 20);
            this.txtRemitido.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Remitido:";
            // 
            // txtAntencion
            // 
            this.txtAntencion.Location = new System.Drawing.Point(8, 32);
            this.txtAntencion.Name = "txtAntencion";
            this.txtAntencion.OnlyUpperCase = false;
            this.txtAntencion.Size = new System.Drawing.Size(249, 20);
            this.txtAntencion.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Atención:";
            // 
            // btnImprimir
            // 
            this.btnImprimir.Location = new System.Drawing.Point(202, 399);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(75, 23);
            this.btnImprimir.TabIndex = 3;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // frmEtiquetasEmpaque
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.scape1;
            this.ClientSize = new System.Drawing.Size(289, 431);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtDireccion);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblRazonSocial);
            this.Controls.Add(this.txtNumeroPedido);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEtiquetasEmpaque";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Etiquetas de Empaque";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UserControls.Scape scape1;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private UserControls.TextBoxEx txtContenido;
        private UserControls.TextBoxEx txtConsignado;
        private System.Windows.Forms.Label label6;
        private UserControls.TextBoxEx txtRemitido;
        private System.Windows.Forms.Label label5;
        private UserControls.TextBoxEx txtAntencion;
        private System.Windows.Forms.Label label4;
        private UserControls.TextBoxEx txtDireccion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblRazonSocial;
        private UserControls.NumericTextBox txtNumeroPedido;
        private System.Windows.Forms.Label label1;
    }
}