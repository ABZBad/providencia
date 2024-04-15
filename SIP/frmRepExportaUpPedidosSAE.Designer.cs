namespace SIP
{
    partial class frmRepExportaUpPedidosSAE
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
            this.dtpFin = new System.Windows.Forms.DateTimePicker();
            this.dtpIni = new System.Windows.Forms.DateTimePicker();
            this.chkSinFechaSurtido = new System.Windows.Forms.CheckBox();
            this.chkSinFechaEntregado = new System.Windows.Forms.CheckBox();
            this.btnContinuar = new System.Windows.Forms.Button();
            this.scape1 = new SIP.UserControls.Scape();
            this.SuspendLayout();
            // 
            // dtpFin
            // 
            this.dtpFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFin.Location = new System.Drawing.Point(92, 9);
            this.dtpFin.Name = "dtpFin";
            this.dtpFin.Size = new System.Drawing.Size(80, 20);
            this.dtpFin.TabIndex = 1;
            // 
            // dtpIni
            // 
            this.dtpIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpIni.Location = new System.Drawing.Point(4, 9);
            this.dtpIni.Name = "dtpIni";
            this.dtpIni.Size = new System.Drawing.Size(80, 20);
            this.dtpIni.TabIndex = 0;
            // 
            // chkSinFechaSurtido
            // 
            this.chkSinFechaSurtido.AutoSize = true;
            this.chkSinFechaSurtido.Location = new System.Drawing.Point(4, 40);
            this.chkSinFechaSurtido.Name = "chkSinFechaSurtido";
            this.chkSinFechaSurtido.Size = new System.Drawing.Size(120, 17);
            this.chkSinFechaSurtido.TabIndex = 2;
            this.chkSinFechaSurtido.Text = "Sin fecha de surtido";
            this.chkSinFechaSurtido.UseVisualStyleBackColor = true;
            // 
            // chkSinFechaEntregado
            // 
            this.chkSinFechaEntregado.AutoSize = true;
            this.chkSinFechaEntregado.Location = new System.Drawing.Point(4, 63);
            this.chkSinFechaEntregado.Name = "chkSinFechaEntregado";
            this.chkSinFechaEntregado.Size = new System.Drawing.Size(137, 17);
            this.chkSinFechaEntregado.TabIndex = 3;
            this.chkSinFechaEntregado.Text = "Sin fecha de entregado";
            this.chkSinFechaEntregado.UseVisualStyleBackColor = true;
            // 
            // btnContinuar
            // 
            this.btnContinuar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnContinuar.Location = new System.Drawing.Point(92, 86);
            this.btnContinuar.Name = "btnContinuar";
            this.btnContinuar.Size = new System.Drawing.Size(75, 23);
            this.btnContinuar.TabIndex = 4;
            this.btnContinuar.Text = "Continuar";
            this.btnContinuar.UseVisualStyleBackColor = true;
            this.btnContinuar.Click += new System.EventHandler(this.btnContinuar_Click);
            // 
            // scape1
            // 
            this.scape1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.scape1.Form = this;
            // 
            // frmRepExportaUpPedidosSAE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.scape1;
            this.ClientSize = new System.Drawing.Size(177, 115);
            this.Controls.Add(this.btnContinuar);
            this.Controls.Add(this.chkSinFechaEntregado);
            this.Controls.Add(this.chkSinFechaSurtido);
            this.Controls.Add(this.dtpFin);
            this.Controls.Add(this.dtpIni);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRepExportaUpPedidosSAE";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fecha SAE";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpFin;
        private System.Windows.Forms.DateTimePicker dtpIni;
        private System.Windows.Forms.CheckBox chkSinFechaSurtido;
        private System.Windows.Forms.CheckBox chkSinFechaEntregado;
        private System.Windows.Forms.Button btnContinuar;
        private UserControls.Scape scape1;
    }
}