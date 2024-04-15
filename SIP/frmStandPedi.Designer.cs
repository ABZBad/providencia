namespace SIP
{
    partial class frmStandPedi
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblUltimaModif = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.txtEmbarque = new SIP.UserControls.NumericTextBox();
            this.txtCostura = new SIP.UserControls.NumericTextBox();
            this.txtBordado = new SIP.UserControls.NumericTextBox();
            this.txtCorte = new SIP.UserControls.NumericTextBox();
            this.txtLiberacion = new SIP.UserControls.NumericTextBox();
            this.txtEmpaque = new SIP.UserControls.NumericTextBox();
            this.txtIni = new SIP.UserControls.NumericTextBox();
            this.txtEst = new SIP.UserControls.NumericTextBox();
            this.txtSurtido = new SIP.UserControls.NumericTextBox();
            this.txtAdministrativo = new SIP.UserControls.NumericTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(273, 45);
            this.label1.TabIndex = 0;
            this.label1.Text = "Utilice esta opción para actualizar los Standares de Pedidos. Estos son utilizado" +
    "s para imprimir el reporte de Análisis de Entrega y Desempeño por Área.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(202, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Introduzca los valores en número de días";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Administrativo:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Surtido:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 134);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Est.:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 160);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Ini.:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 188);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Empaque:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(159, 82);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Liberación:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(159, 109);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "Corte:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(159, 134);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(50, 13);
            this.label10.TabIndex = 16;
            this.label10.Text = "Bordado:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(159, 160);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(46, 13);
            this.label11.TabIndex = 18;
            this.label11.Text = "Costura:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(159, 188);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(58, 13);
            this.label12.TabIndex = 20;
            this.label12.Text = "Embarque:";
            // 
            // lblUltimaModif
            // 
            this.lblUltimaModif.Location = new System.Drawing.Point(15, 221);
            this.lblUltimaModif.Name = "lblUltimaModif";
            this.lblUltimaModif.Size = new System.Drawing.Size(179, 47);
            this.lblUltimaModif.TabIndex = 22;
            this.lblUltimaModif.Text = "Última modificación por:";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(210, 230);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 23;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // txtEmbarque
            // 
            this.txtEmbarque.Location = new System.Drawing.Point(248, 185);
            this.txtEmbarque.MaxValue = 0;
            this.txtEmbarque.MinValue = 0;
            this.txtEmbarque.Name = "txtEmbarque";
            this.txtEmbarque.Size = new System.Drawing.Size(37, 20);
            this.txtEmbarque.TabIndex = 21;
            this.txtEmbarque.Text = "0";
            // 
            // txtCostura
            // 
            this.txtCostura.Location = new System.Drawing.Point(248, 157);
            this.txtCostura.MaxValue = 0;
            this.txtCostura.MinValue = 0;
            this.txtCostura.Name = "txtCostura";
            this.txtCostura.Size = new System.Drawing.Size(37, 20);
            this.txtCostura.TabIndex = 19;
            this.txtCostura.Text = "0";
            // 
            // txtBordado
            // 
            this.txtBordado.Location = new System.Drawing.Point(248, 131);
            this.txtBordado.MaxValue = 0;
            this.txtBordado.MinValue = 0;
            this.txtBordado.Name = "txtBordado";
            this.txtBordado.Size = new System.Drawing.Size(37, 20);
            this.txtBordado.TabIndex = 17;
            this.txtBordado.Text = "0";
            // 
            // txtCorte
            // 
            this.txtCorte.Location = new System.Drawing.Point(248, 105);
            this.txtCorte.MaxValue = 0;
            this.txtCorte.MinValue = 0;
            this.txtCorte.Name = "txtCorte";
            this.txtCorte.Size = new System.Drawing.Size(37, 20);
            this.txtCorte.TabIndex = 15;
            this.txtCorte.Text = "0";
            // 
            // txtLiberacion
            // 
            this.txtLiberacion.Location = new System.Drawing.Point(248, 79);
            this.txtLiberacion.MaxValue = 0;
            this.txtLiberacion.MinValue = 0;
            this.txtLiberacion.Name = "txtLiberacion";
            this.txtLiberacion.Size = new System.Drawing.Size(37, 20);
            this.txtLiberacion.TabIndex = 13;
            this.txtLiberacion.Text = "0";
            // 
            // txtEmpaque
            // 
            this.txtEmpaque.Location = new System.Drawing.Point(93, 185);
            this.txtEmpaque.MaxValue = 0;
            this.txtEmpaque.MinValue = 0;
            this.txtEmpaque.Name = "txtEmpaque";
            this.txtEmpaque.Size = new System.Drawing.Size(37, 20);
            this.txtEmpaque.TabIndex = 11;
            this.txtEmpaque.Text = "0";
            // 
            // txtIni
            // 
            this.txtIni.Location = new System.Drawing.Point(93, 157);
            this.txtIni.MaxValue = 0;
            this.txtIni.MinValue = 0;
            this.txtIni.Name = "txtIni";
            this.txtIni.Size = new System.Drawing.Size(37, 20);
            this.txtIni.TabIndex = 9;
            this.txtIni.Text = "0";
            // 
            // txtEst
            // 
            this.txtEst.Location = new System.Drawing.Point(93, 131);
            this.txtEst.MaxValue = 0;
            this.txtEst.MinValue = 0;
            this.txtEst.Name = "txtEst";
            this.txtEst.Size = new System.Drawing.Size(37, 20);
            this.txtEst.TabIndex = 7;
            this.txtEst.Text = "0";
            // 
            // txtSurtido
            // 
            this.txtSurtido.Location = new System.Drawing.Point(93, 106);
            this.txtSurtido.MaxValue = 0;
            this.txtSurtido.MinValue = 0;
            this.txtSurtido.Name = "txtSurtido";
            this.txtSurtido.Size = new System.Drawing.Size(37, 20);
            this.txtSurtido.TabIndex = 5;
            this.txtSurtido.Text = "0";
            // 
            // txtAdministrativo
            // 
            this.txtAdministrativo.Location = new System.Drawing.Point(93, 79);
            this.txtAdministrativo.MaxValue = 0;
            this.txtAdministrativo.MinValue = 0;
            this.txtAdministrativo.Name = "txtAdministrativo";
            this.txtAdministrativo.Size = new System.Drawing.Size(37, 20);
            this.txtAdministrativo.TabIndex = 3;
            this.txtAdministrativo.Text = "0";
            // 
            // frmStandPedi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 277);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.lblUltimaModif);
            this.Controls.Add(this.txtEmbarque);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtCostura);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtBordado);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtCorte);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtLiberacion);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtEmpaque);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtIni);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtEst);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtSurtido);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtAdministrativo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmStandPedi";
            this.Text = "Modificación de Standares de Pedidos ";
            this.Load += new System.EventHandler(this.frmStandPedi_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private UserControls.NumericTextBox txtAdministrativo;
        private System.Windows.Forms.Label label4;
        private UserControls.NumericTextBox txtSurtido;
        private System.Windows.Forms.Label label5;
        private UserControls.NumericTextBox txtEst;
        private System.Windows.Forms.Label label6;
        private UserControls.NumericTextBox txtIni;
        private System.Windows.Forms.Label label7;
        private UserControls.NumericTextBox txtEmpaque;
        private System.Windows.Forms.Label label8;
        private UserControls.NumericTextBox txtLiberacion;
        private System.Windows.Forms.Label label9;
        private UserControls.NumericTextBox txtCorte;
        private System.Windows.Forms.Label label10;
        private UserControls.NumericTextBox txtBordado;
        private System.Windows.Forms.Label label11;
        private UserControls.NumericTextBox txtCostura;
        private System.Windows.Forms.Label label12;
        private UserControls.NumericTextBox txtEmbarque;
        private System.Windows.Forms.Label lblUltimaModif;
        private System.Windows.Forms.Button btnGuardar;
    }
}