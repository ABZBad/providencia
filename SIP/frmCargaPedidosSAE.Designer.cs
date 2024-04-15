namespace SIP
{
    partial class frmCargaPedidosSAE
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
            this.txtPedido = new SIP.UserControls.NumericTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtComision = new SIP.UserControls.NumericTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtIva = new SIP.UserControls.NumericTextBox();
            this.btnCapturaImprimir = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lblAdministrativo = new System.Windows.Forms.Label();
            this.lblSurtido = new System.Windows.Forms.Label();
            this.lblEstampado = new System.Windows.Forms.Label();
            this.lblIni = new System.Windows.Forms.Label();
            this.lblEmpaque = new System.Windows.Forms.Label();
            this.lblLiberacion = new System.Windows.Forms.Label();
            this.lblCorte = new System.Windows.Forms.Label();
            this.lblBordado = new System.Windows.Forms.Label();
            this.lblCostura = new System.Windows.Forms.Label();
            this.lblEmbarque = new System.Windows.Forms.Label();
            this.IblIntroValor = new System.Windows.Forms.Label();
            this.scape1 = new SIP.UserControls.Scape();
            this.chkMostrador = new System.Windows.Forms.RadioButton();
            this.chkDAT = new System.Windows.Forms.RadioButton();
            this.chkPedido = new System.Windows.Forms.RadioButton();
            this.chkPedidoEC = new System.Windows.Forms.RadioButton();
            this.chkMostradorCP = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pedido:";
            // 
            // txtPedido
            // 
            this.txtPedido.Location = new System.Drawing.Point(121, 14);
            this.txtPedido.Margin = new System.Windows.Forms.Padding(4);
            this.txtPedido.MaxValue = 0;
            this.txtPedido.MinValue = 0;
            this.txtPedido.Name = "txtPedido";
            this.txtPedido.Size = new System.Drawing.Size(160, 22);
            this.txtPedido.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 46);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Comisión:";
            // 
            // txtComision
            // 
            this.txtComision.Location = new System.Drawing.Point(121, 42);
            this.txtComision.Margin = new System.Windows.Forms.Padding(4);
            this.txtComision.MaxValue = 0;
            this.txtComision.MinValue = 0;
            this.txtComision.Name = "txtComision";
            this.txtComision.NumberType = SIP.UserControls.TipoDeNumero.Decimal;
            this.txtComision.Size = new System.Drawing.Size(160, 22);
            this.txtComision.TabIndex = 3;
            this.txtComision.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 82);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "IVA:";
            // 
            // txtIva
            // 
            this.txtIva.Location = new System.Drawing.Point(121, 73);
            this.txtIva.Margin = new System.Windows.Forms.Padding(4);
            this.txtIva.MaxValue = 0;
            this.txtIva.MinValue = 0;
            this.txtIva.Name = "txtIva";
            this.txtIva.Size = new System.Drawing.Size(160, 22);
            this.txtIva.TabIndex = 5;
            this.txtIva.Text = "16";
            // 
            // btnCapturaImprimir
            // 
            this.btnCapturaImprimir.Location = new System.Drawing.Point(40, 253);
            this.btnCapturaImprimir.Margin = new System.Windows.Forms.Padding(4);
            this.btnCapturaImprimir.Name = "btnCapturaImprimir";
            this.btnCapturaImprimir.Size = new System.Drawing.Size(224, 28);
            this.btnCapturaImprimir.TabIndex = 6;
            this.btnCapturaImprimir.Text = "Capturar pedido en SAE";
            this.btnCapturaImprimir.UseVisualStyleBackColor = true;
            this.btnCapturaImprimir.Click += new System.EventHandler(this.btnCapturaImprimir_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(37, 296);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(227, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Standares de Pedido Actuales";
            // 
            // lblAdministrativo
            // 
            this.lblAdministrativo.AutoSize = true;
            this.lblAdministrativo.Location = new System.Drawing.Point(20, 318);
            this.lblAdministrativo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAdministrativo.Name = "lblAdministrativo";
            this.lblAdministrativo.Size = new System.Drawing.Size(100, 17);
            this.lblAdministrativo.TabIndex = 8;
            this.lblAdministrativo.Text = "Administrativo:";
            // 
            // lblSurtido
            // 
            this.lblSurtido.AutoSize = true;
            this.lblSurtido.Location = new System.Drawing.Point(20, 336);
            this.lblSurtido.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSurtido.Name = "lblSurtido";
            this.lblSurtido.Size = new System.Drawing.Size(57, 17);
            this.lblSurtido.TabIndex = 9;
            this.lblSurtido.Text = "Surtido:";
            // 
            // lblEstampado
            // 
            this.lblEstampado.AutoSize = true;
            this.lblEstampado.Location = new System.Drawing.Point(20, 357);
            this.lblEstampado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEstampado.Name = "lblEstampado";
            this.lblEstampado.Size = new System.Drawing.Size(36, 17);
            this.lblEstampado.TabIndex = 10;
            this.lblEstampado.Text = "Est.:";
            // 
            // lblIni
            // 
            this.lblIni.AutoSize = true;
            this.lblIni.Location = new System.Drawing.Point(20, 378);
            this.lblIni.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIni.Name = "lblIni";
            this.lblIni.Size = new System.Drawing.Size(30, 17);
            this.lblIni.TabIndex = 11;
            this.lblIni.Text = "Ini.:";
            // 
            // lblEmpaque
            // 
            this.lblEmpaque.AutoSize = true;
            this.lblEmpaque.Location = new System.Drawing.Point(20, 399);
            this.lblEmpaque.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEmpaque.Name = "lblEmpaque";
            this.lblEmpaque.Size = new System.Drawing.Size(68, 17);
            this.lblEmpaque.TabIndex = 12;
            this.lblEmpaque.Text = "Empaque";
            // 
            // lblLiberacion
            // 
            this.lblLiberacion.AutoSize = true;
            this.lblLiberacion.Location = new System.Drawing.Point(172, 318);
            this.lblLiberacion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLiberacion.Name = "lblLiberacion";
            this.lblLiberacion.Size = new System.Drawing.Size(78, 17);
            this.lblLiberacion.TabIndex = 13;
            this.lblLiberacion.Text = "Liberación:";
            // 
            // lblCorte
            // 
            this.lblCorte.AutoSize = true;
            this.lblCorte.Location = new System.Drawing.Point(172, 336);
            this.lblCorte.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCorte.Name = "lblCorte";
            this.lblCorte.Size = new System.Drawing.Size(46, 17);
            this.lblCorte.TabIndex = 14;
            this.lblCorte.Text = "Corte:";
            // 
            // lblBordado
            // 
            this.lblBordado.AutoSize = true;
            this.lblBordado.Location = new System.Drawing.Point(172, 357);
            this.lblBordado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBordado.Name = "lblBordado";
            this.lblBordado.Size = new System.Drawing.Size(66, 17);
            this.lblBordado.TabIndex = 15;
            this.lblBordado.Text = "Bordado:";
            // 
            // lblCostura
            // 
            this.lblCostura.AutoSize = true;
            this.lblCostura.Location = new System.Drawing.Point(172, 378);
            this.lblCostura.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCostura.Name = "lblCostura";
            this.lblCostura.Size = new System.Drawing.Size(61, 17);
            this.lblCostura.TabIndex = 16;
            this.lblCostura.Text = "Costura:";
            // 
            // lblEmbarque
            // 
            this.lblEmbarque.AutoSize = true;
            this.lblEmbarque.Location = new System.Drawing.Point(172, 399);
            this.lblEmbarque.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEmbarque.Name = "lblEmbarque";
            this.lblEmbarque.Size = new System.Drawing.Size(77, 17);
            this.lblEmbarque.TabIndex = 17;
            this.lblEmbarque.Text = "Embarque:";
            // 
            // IblIntroValor
            // 
            this.IblIntroValor.AutoSize = true;
            this.IblIntroValor.Location = new System.Drawing.Point(12, 424);
            this.IblIntroValor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.IblIntroValor.Name = "IblIntroValor";
            this.IblIntroValor.Size = new System.Drawing.Size(268, 17);
            this.IblIntroValor.TabIndex = 18;
            this.IblIntroValor.Text = "Introduzca los valores en número de días";
            // 
            // scape1
            // 
            this.scape1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.scape1.Form = this;
            // 
            // chkMostrador
            // 
            this.chkMostrador.AutoSize = true;
            this.chkMostrador.Location = new System.Drawing.Point(97, 191);
            this.chkMostrador.Margin = new System.Windows.Forms.Padding(4);
            this.chkMostrador.Name = "chkMostrador";
            this.chkMostrador.Size = new System.Drawing.Size(160, 21);
            this.chkMostrador.TabIndex = 21;
            this.chkMostrador.Text = "Es pedido Mostrador";
            this.chkMostrador.UseVisualStyleBackColor = true;
            // 
            // chkDAT
            // 
            this.chkDAT.AutoSize = true;
            this.chkDAT.Location = new System.Drawing.Point(98, 163);
            this.chkDAT.Margin = new System.Windows.Forms.Padding(4);
            this.chkDAT.Name = "chkDAT";
            this.chkDAT.Size = new System.Drawing.Size(124, 21);
            this.chkDAT.TabIndex = 22;
            this.chkDAT.Text = "Es pedido DAT";
            this.chkDAT.UseVisualStyleBackColor = true;
            // 
            // chkPedido
            // 
            this.chkPedido.AutoSize = true;
            this.chkPedido.Checked = true;
            this.chkPedido.Location = new System.Drawing.Point(98, 105);
            this.chkPedido.Margin = new System.Windows.Forms.Padding(4);
            this.chkPedido.Name = "chkPedido";
            this.chkPedido.Size = new System.Drawing.Size(105, 21);
            this.chkPedido.TabIndex = 23;
            this.chkPedido.TabStop = true;
            this.chkPedido.Text = "Es pedido P";
            this.chkPedido.UseVisualStyleBackColor = true;
            // 
            // chkPedidoEC
            // 
            this.chkPedidoEC.AutoSize = true;
            this.chkPedidoEC.Location = new System.Drawing.Point(98, 134);
            this.chkPedidoEC.Margin = new System.Windows.Forms.Padding(4);
            this.chkPedidoEC.Name = "chkPedidoEC";
            this.chkPedidoEC.Size = new System.Drawing.Size(170, 21);
            this.chkPedidoEC.TabIndex = 24;
            this.chkPedidoEC.Text = "Es pedido Ecommerce";
            this.chkPedidoEC.UseVisualStyleBackColor = true;
            // 
            // chkMostradorCP
            // 
            this.chkMostradorCP.AutoSize = true;
            this.chkMostradorCP.Location = new System.Drawing.Point(97, 220);
            this.chkMostradorCP.Margin = new System.Windows.Forms.Padding(4);
            this.chkMostradorCP.Name = "chkMostradorCP";
            this.chkMostradorCP.Size = new System.Drawing.Size(184, 21);
            this.chkMostradorCP.TabIndex = 25;
            this.chkMostradorCP.Text = "Es pedido Mostrador MP";
            this.chkMostradorCP.UseVisualStyleBackColor = true;
            // 
            // frmCargaPedidosSAE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.scape1;
            this.ClientSize = new System.Drawing.Size(315, 461);
            this.Controls.Add(this.chkMostradorCP);
            this.Controls.Add(this.chkPedidoEC);
            this.Controls.Add(this.chkPedido);
            this.Controls.Add(this.chkDAT);
            this.Controls.Add(this.chkMostrador);
            this.Controls.Add(this.IblIntroValor);
            this.Controls.Add(this.lblEmbarque);
            this.Controls.Add(this.lblCostura);
            this.Controls.Add(this.lblBordado);
            this.Controls.Add(this.lblCorte);
            this.Controls.Add(this.lblLiberacion);
            this.Controls.Add(this.lblEmpaque);
            this.Controls.Add(this.lblIni);
            this.Controls.Add(this.lblEstampado);
            this.Controls.Add(this.lblSurtido);
            this.Controls.Add(this.lblAdministrativo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCapturaImprimir);
            this.Controls.Add(this.txtIva);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtComision);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPedido);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCargaPedidosSAE";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Aspel-SAE 7.0";
            this.Activated += new System.EventHandler(this.frmCargaPedidosSAE_Activated);
            this.Load += new System.EventHandler(this.frmCargaPedidosSAE_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private UserControls.NumericTextBox txtPedido;
        private System.Windows.Forms.Label label2;
        private UserControls.NumericTextBox txtComision;
        private System.Windows.Forms.Label label3;
        private UserControls.NumericTextBox txtIva;
        private System.Windows.Forms.Button btnCapturaImprimir;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblAdministrativo;
        private System.Windows.Forms.Label lblSurtido;
        private System.Windows.Forms.Label lblEstampado;
        private System.Windows.Forms.Label lblIni;
        private System.Windows.Forms.Label lblEmpaque;
        private System.Windows.Forms.Label lblLiberacion;
        private System.Windows.Forms.Label lblCorte;
        private System.Windows.Forms.Label lblBordado;
        private System.Windows.Forms.Label lblCostura;
        private System.Windows.Forms.Label lblEmbarque;
        private System.Windows.Forms.Label IblIntroValor;
        private UserControls.Scape scape1;
        private System.Windows.Forms.RadioButton chkPedido;
        private System.Windows.Forms.RadioButton chkDAT;
        private System.Windows.Forms.RadioButton chkMostrador;
        private System.Windows.Forms.RadioButton chkPedidoEC;
        private System.Windows.Forms.RadioButton chkMostradorCP;
    }
}