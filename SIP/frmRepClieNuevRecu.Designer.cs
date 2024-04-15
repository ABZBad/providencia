namespace SIP
{
    partial class frmRepClieNuevRecu
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
            this.optNuevos = new System.Windows.Forms.RadioButton();
            this.optRecuperados = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.optMetropolitano = new System.Windows.Forms.RadioButton();
            this.optForaneo = new System.Windows.Forms.RadioButton();
            this.dtFechaReporte = new System.Windows.Forms.DateTimePicker();
            this.btnGenerarReporte = new System.Windows.Forms.Button();
            this.scape1 = new SIP.UserControls.Scape();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // optNuevos
            // 
            this.optNuevos.AutoSize = true;
            this.optNuevos.Checked = true;
            this.optNuevos.Location = new System.Drawing.Point(7, 3);
            this.optNuevos.Name = "optNuevos";
            this.optNuevos.Size = new System.Drawing.Size(62, 17);
            this.optNuevos.TabIndex = 0;
            this.optNuevos.TabStop = true;
            this.optNuevos.Text = "Nuevos";
            this.optNuevos.UseVisualStyleBackColor = true;
            // 
            // optRecuperados
            // 
            this.optRecuperados.AutoSize = true;
            this.optRecuperados.Location = new System.Drawing.Point(91, 3);
            this.optRecuperados.Name = "optRecuperados";
            this.optRecuperados.Size = new System.Drawing.Size(89, 17);
            this.optRecuperados.TabIndex = 1;
            this.optRecuperados.Text = "Recuperados";
            this.optRecuperados.UseVisualStyleBackColor = true;
            this.optRecuperados.CheckedChanged += new System.EventHandler(this.optRecuperados_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.optMetropolitano);
            this.groupBox1.Controls.Add(this.optForaneo);
            this.groupBox1.Location = new System.Drawing.Point(3, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(177, 56);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tipo";
            // 
            // optMetropolitano
            // 
            this.optMetropolitano.AutoSize = true;
            this.optMetropolitano.Location = new System.Drawing.Point(82, 26);
            this.optMetropolitano.Name = "optMetropolitano";
            this.optMetropolitano.Size = new System.Drawing.Size(89, 17);
            this.optMetropolitano.TabIndex = 1;
            this.optMetropolitano.TabStop = true;
            this.optMetropolitano.Text = "Metropolitano";
            this.optMetropolitano.UseVisualStyleBackColor = true;
            // 
            // optForaneo
            // 
            this.optForaneo.AutoSize = true;
            this.optForaneo.Location = new System.Drawing.Point(6, 26);
            this.optForaneo.Name = "optForaneo";
            this.optForaneo.Size = new System.Drawing.Size(64, 17);
            this.optForaneo.TabIndex = 0;
            this.optForaneo.TabStop = true;
            this.optForaneo.Text = "Foraneo";
            this.optForaneo.UseVisualStyleBackColor = true;
            // 
            // dtFechaReporte
            // 
            this.dtFechaReporte.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFechaReporte.Location = new System.Drawing.Point(50, 95);
            this.dtFechaReporte.Name = "dtFechaReporte";
            this.dtFechaReporte.Size = new System.Drawing.Size(81, 20);
            this.dtFechaReporte.TabIndex = 3;
            // 
            // btnGenerarReporte
            // 
            this.btnGenerarReporte.Location = new System.Drawing.Point(43, 132);
            this.btnGenerarReporte.Name = "btnGenerarReporte";
            this.btnGenerarReporte.Size = new System.Drawing.Size(94, 23);
            this.btnGenerarReporte.TabIndex = 0;
            this.btnGenerarReporte.Text = "Generar Reporte";
            this.btnGenerarReporte.UseVisualStyleBackColor = true;
            this.btnGenerarReporte.Click += new System.EventHandler(this.btnGenerarReporte_Click);
            // 
            // scape1
            // 
            this.scape1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.scape1.Form = this;
            // 
            // frmRepClieNuevRecu
            // 
            this.AcceptButton = this.btnGenerarReporte;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.scape1;
            this.ClientSize = new System.Drawing.Size(183, 170);
            this.Controls.Add(this.btnGenerarReporte);
            this.Controls.Add(this.dtFechaReporte);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.optRecuperados);
            this.Controls.Add(this.optNuevos);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRepClieNuevRecu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Clientes Nuevos /Recuperados";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton optNuevos;
        private System.Windows.Forms.RadioButton optRecuperados;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton optMetropolitano;
        private System.Windows.Forms.RadioButton optForaneo;
        private System.Windows.Forms.DateTimePicker dtFechaReporte;
        private System.Windows.Forms.Button btnGenerarReporte;
        private UserControls.Scape scape1;
    }
}