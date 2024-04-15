namespace SIP
{
    partial class frmRepOrdProdMaq
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
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.lblHasta = new System.Windows.Forms.Label();
            this.lblDesde = new System.Windows.Forms.Label();
            this.btnContinuar = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitulo.Location = new System.Drawing.Point(37, 48);
            this.lblSubtitulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(206, 40);
            this.lblSubtitulo.TabIndex = 30;
            this.lblSubtitulo.Text = "Ordenes de Producción\r\ncon detalle Proveedor";
            // 
            // dtpHasta
            // 
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta.Location = new System.Drawing.Point(78, 141);
            this.dtpHasta.Margin = new System.Windows.Forms.Padding(4);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(180, 22);
            this.dtpHasta.TabIndex = 29;
            this.dtpHasta.Value = new System.DateTime(2017, 4, 23, 0, 0, 0, 0);
            // 
            // dtpDesde
            // 
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.Location = new System.Drawing.Point(78, 109);
            this.dtpDesde.Margin = new System.Windows.Forms.Padding(4);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(180, 22);
            this.dtpDesde.TabIndex = 28;
            this.dtpDesde.Value = new System.DateTime(2017, 4, 23, 17, 34, 13, 0);
            // 
            // lblHasta
            // 
            this.lblHasta.AutoSize = true;
            this.lblHasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHasta.Location = new System.Drawing.Point(19, 141);
            this.lblHasta.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.Size = new System.Drawing.Size(50, 17);
            this.lblHasta.TabIndex = 27;
            this.lblHasta.Text = "Hasta";
            // 
            // lblDesde
            // 
            this.lblDesde.AutoSize = true;
            this.lblDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesde.Location = new System.Drawing.Point(15, 109);
            this.lblDesde.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDesde.Name = "lblDesde";
            this.lblDesde.Size = new System.Drawing.Size(54, 17);
            this.lblDesde.TabIndex = 26;
            this.lblDesde.Text = "Desde";
            // 
            // btnContinuar
            // 
            this.btnContinuar.Location = new System.Drawing.Point(166, 187);
            this.btnContinuar.Margin = new System.Windows.Forms.Padding(4);
            this.btnContinuar.Name = "btnContinuar";
            this.btnContinuar.Size = new System.Drawing.Size(100, 28);
            this.btnContinuar.TabIndex = 25;
            this.btnContinuar.Text = "Continuar";
            this.btnContinuar.UseVisualStyleBackColor = true;
            this.btnContinuar.Click += new System.EventHandler(this.btnContinuar_Click);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(46, 9);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(188, 29);
            this.lblTitulo.TabIndex = 24;
            this.lblTitulo.Text = "Reporte de OP";
            // 
            // frmRepOrdProdMaq
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 225);
            this.Controls.Add(this.lblSubtitulo);
            this.Controls.Add(this.dtpHasta);
            this.Controls.Add(this.dtpDesde);
            this.Controls.Add(this.lblHasta);
            this.Controls.Add(this.lblDesde);
            this.Controls.Add(this.btnContinuar);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRepOrdProdMaq";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reporte OP";
            this.Load += new System.EventHandler(this.frmRepOrdProdMaq_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.Label lblDesde;
        private System.Windows.Forms.Button btnContinuar;
        private System.Windows.Forms.Label lblTitulo;
    }
}