namespace SIP
{
    partial class frmEmbarques
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtCodCliente = new SIP.UserControls.TextBoxEx();
            this.txtFechaPedido = new SIP.UserControls.TextBoxEx();
            this.txtNumeroPedido = new SIP.UserControls.TextBoxEx();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtObservaciones = new SIP.UserControls.TextBoxEx();
            this.txtFechaRuta = new SIP.UserControls.TextBoxEx();
            this.txtTransporte = new SIP.UserControls.TextBoxEx();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtEstatus = new SIP.UserControls.TextBoxEx();
            this.label11 = new System.Windows.Forms.Label();
            this.txtDepartamento = new SIP.UserControls.TextBoxEx();
            this.txtChofer = new SIP.UserControls.TextBoxEx();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDestino = new SIP.UserControls.TextBoxEx();
            this.txtCajas = new SIP.UserControls.NumericTextBox();
            this.txtGuia = new SIP.UserControls.TextBoxEx();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // scape1
            // 
            this.scape1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.scape1.Form = this;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCodCliente);
            this.groupBox1.Controls.Add(this.txtFechaPedido);
            this.groupBox1.Controls.Add(this.txtNumeroPedido);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(49, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(400, 94);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Generales";
            // 
            // txtCodCliente
            // 
            this.txtCodCliente.Location = new System.Drawing.Point(283, 22);
            this.txtCodCliente.Name = "txtCodCliente";
            this.txtCodCliente.OnlyUpperCase = false;
            this.txtCodCliente.ReadOnly = true;
            this.txtCodCliente.Size = new System.Drawing.Size(111, 20);
            this.txtCodCliente.TabIndex = 1;
            // 
            // txtFechaPedido
            // 
            this.txtFechaPedido.Location = new System.Drawing.Point(94, 49);
            this.txtFechaPedido.Name = "txtFechaPedido";
            this.txtFechaPedido.OnlyUpperCase = false;
            this.txtFechaPedido.ReadOnly = true;
            this.txtFechaPedido.Size = new System.Drawing.Size(133, 20);
            this.txtFechaPedido.TabIndex = 2;
            // 
            // txtNumeroPedido
            // 
            this.txtNumeroPedido.Location = new System.Drawing.Point(94, 22);
            this.txtNumeroPedido.Name = "txtNumeroPedido";
            this.txtNumeroPedido.OnlyUpperCase = false;
            this.txtNumeroPedido.ReadOnly = true;
            this.txtNumeroPedido.Size = new System.Drawing.Size(133, 20);
            this.txtNumeroPedido.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(231, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Cliente:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Fecha Pedido:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pedido:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtObservaciones);
            this.groupBox2.Controls.Add(this.txtFechaRuta);
            this.groupBox2.Controls.Add(this.txtTransporte);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtEstatus);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txtDepartamento);
            this.groupBox2.Controls.Add(this.txtChofer);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtDestino);
            this.groupBox2.Controls.Add(this.txtCajas);
            this.groupBox2.Controls.Add(this.txtGuia);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(12, 123);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(470, 221);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Generales";
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Location = new System.Drawing.Point(20, 115);
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.OnlyUpperCase = false;
            this.txtObservaciones.Size = new System.Drawing.Size(400, 98);
            this.txtObservaciones.TabIndex = 8;
            // 
            // txtFechaRuta
            // 
            this.txtFechaRuta.Location = new System.Drawing.Point(305, 89);
            this.txtFechaRuta.Name = "txtFechaRuta";
            this.txtFechaRuta.OnlyUpperCase = false;
            this.txtFechaRuta.Size = new System.Drawing.Size(115, 20);
            this.txtFechaRuta.TabIndex = 7;
            // 
            // txtTransporte
            // 
            this.txtTransporte.Location = new System.Drawing.Point(305, 66);
            this.txtTransporte.MaxLength = 20;
            this.txtTransporte.Name = "txtTransporte";
            this.txtTransporte.OnlyUpperCase = false;
            this.txtTransporte.Size = new System.Drawing.Size(115, 20);
            this.txtTransporte.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(233, 93);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "Fecha Ruta:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(238, 70);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "Transporte:";
            // 
            // txtEstatus
            // 
            this.txtEstatus.Location = new System.Drawing.Point(305, 43);
            this.txtEstatus.MaxLength = 10;
            this.txtEstatus.Name = "txtEstatus";
            this.txtEstatus.OnlyUpperCase = false;
            this.txtEstatus.Size = new System.Drawing.Size(115, 20);
            this.txtEstatus.TabIndex = 5;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(254, 47);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(45, 13);
            this.label11.TabIndex = 10;
            this.label11.Text = "Estatus:";
            // 
            // txtDepartamento
            // 
            this.txtDepartamento.Location = new System.Drawing.Point(99, 89);
            this.txtDepartamento.MaxLength = 50;
            this.txtDepartamento.Name = "txtDepartamento";
            this.txtDepartamento.OnlyUpperCase = false;
            this.txtDepartamento.Size = new System.Drawing.Size(115, 20);
            this.txtDepartamento.TabIndex = 3;
            // 
            // txtChofer
            // 
            this.txtChofer.Location = new System.Drawing.Point(99, 66);
            this.txtChofer.MaxLength = 20;
            this.txtChofer.Name = "txtChofer";
            this.txtChofer.OnlyUpperCase = false;
            this.txtChofer.Size = new System.Drawing.Size(115, 20);
            this.txtChofer.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 93);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Departamento:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(53, 70);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Chofer:";
            // 
            // txtDestino
            // 
            this.txtDestino.Location = new System.Drawing.Point(305, 21);
            this.txtDestino.MaxLength = 30;
            this.txtDestino.Name = "txtDestino";
            this.txtDestino.OnlyUpperCase = false;
            this.txtDestino.Size = new System.Drawing.Size(115, 20);
            this.txtDestino.TabIndex = 4;
            // 
            // txtCajas
            // 
            this.txtCajas.Location = new System.Drawing.Point(99, 43);
            this.txtCajas.MaxLength = 4;
            this.txtCajas.Name = "txtCajas";
            this.txtCajas.Size = new System.Drawing.Size(115, 20);
            this.txtCajas.TabIndex = 1;
            // 
            // txtGuia
            // 
            this.txtGuia.Location = new System.Drawing.Point(99, 21);
            this.txtGuia.MaxLength = 15;
            this.txtGuia.Name = "txtGuia";
            this.txtGuia.OnlyUpperCase = false;
            this.txtGuia.Size = new System.Drawing.Size(115, 20);
            this.txtGuia.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(253, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Destino:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(58, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Cajas:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(62, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Guia:";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(386, 357);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(96, 23);
            this.btnGuardar.TabIndex = 0;
            this.btnGuardar.Text = "Guardar y Cerrar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // frmEmbarques
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.scape1;
            this.ClientSize = new System.Drawing.Size(494, 392);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEmbarques";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Embarques";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmEmbarques_FormClosing);
            this.Load += new System.EventHandler(this.frmEmbarques_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.Scape scape1;
        private System.Windows.Forms.GroupBox groupBox2;
        private UserControls.TextBoxEx txtObservaciones;
        private UserControls.TextBoxEx txtFechaRuta;
        private UserControls.TextBoxEx txtTransporte;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private UserControls.TextBoxEx txtEstatus;
        private System.Windows.Forms.Label label11;
        private UserControls.TextBoxEx txtDepartamento;
        private UserControls.TextBoxEx txtChofer;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private UserControls.TextBoxEx txtDestino;
        private UserControls.NumericTextBox txtCajas;
        private UserControls.TextBoxEx txtGuia;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private UserControls.TextBoxEx txtCodCliente;
        private UserControls.TextBoxEx txtFechaPedido;
        private UserControls.TextBoxEx txtNumeroPedido;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGuardar;
    }
}