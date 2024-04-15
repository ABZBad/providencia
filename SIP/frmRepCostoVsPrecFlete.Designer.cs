namespace SIP
{
    partial class frmRepCostoVsPrecFlete
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
            this.dtpIni = new System.Windows.Forms.DateTimePicker();
            this.dtpFin = new System.Windows.Forms.DateTimePicker();
            this.btnGenerarReporte = new System.Windows.Forms.Button();
            this.scape1 = new SIP.UserControls.Scape();
            this.SuspendLayout();
            // 
            // dtpIni
            // 
            this.dtpIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpIni.Location = new System.Drawing.Point(11, 15);
            this.dtpIni.Name = "dtpIni";
            this.dtpIni.Size = new System.Drawing.Size(80, 20);
            this.dtpIni.TabIndex = 0;
            this.dtpIni.ValueChanged += new System.EventHandler(this.dtpIni_ValueChanged);
            // 
            // dtpFin
            // 
            this.dtpFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFin.Location = new System.Drawing.Point(99, 15);
            this.dtpFin.Name = "dtpFin";
            this.dtpFin.Size = new System.Drawing.Size(80, 20);
            this.dtpFin.TabIndex = 1;
            this.dtpFin.ValueChanged += new System.EventHandler(this.dtpFin_ValueChanged);
            // 
            // btnGenerarReporte
            // 
            this.btnGenerarReporte.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnGenerarReporte.Location = new System.Drawing.Point(72, 53);
            this.btnGenerarReporte.Name = "btnGenerarReporte";
            this.btnGenerarReporte.Size = new System.Drawing.Size(97, 23);
            this.btnGenerarReporte.TabIndex = 3;
            this.btnGenerarReporte.Text = "Generar Reporte";
            this.btnGenerarReporte.UseVisualStyleBackColor = true;
            this.btnGenerarReporte.Click += new System.EventHandler(this.btnContinuar_Click);
            // 
            // scape1
            // 
            this.scape1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.scape1.Form = this;
            // 
            // frmRepCostoVsPrecFlete
            // 
            this.AcceptButton = this.btnGenerarReporte;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.scape1;
            this.ClientSize = new System.Drawing.Size(187, 94);
            this.Controls.Add(this.btnGenerarReporte);
            this.Controls.Add(this.dtpFin);
            this.Controls.Add(this.dtpIni);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRepCostoVsPrecFlete";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Costo Vs Precio Flete";
            this.Load += new System.EventHandler(this.frmRepExportaUpPedidos_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpIni;
        private System.Windows.Forms.DateTimePicker dtpFin;
        private System.Windows.Forms.Button btnGenerarReporte;
        private UserControls.Scape scape1;
    }
}