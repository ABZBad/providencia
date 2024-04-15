namespace SIP
{
    partial class frmReporteClientesDSvsDR
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
            this.txtAgente = new System.Windows.Forms.TextBox();
            this.lblAgente = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.btnContinuar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtAgente
            // 
            this.txtAgente.Location = new System.Drawing.Point(91, 145);
            this.txtAgente.Name = "txtAgente";
            this.txtAgente.Size = new System.Drawing.Size(125, 20);
            this.txtAgente.TabIndex = 11;
            // 
            // lblAgente
            // 
            this.lblAgente.AutoSize = true;
            this.lblAgente.Location = new System.Drawing.Point(25, 148);
            this.lblAgente.Name = "lblAgente";
            this.lblAgente.Size = new System.Drawing.Size(44, 13);
            this.lblAgente.TabIndex = 10;
            this.lblAgente.Text = "Agente:";
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(25, 125);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(40, 13);
            this.lblFecha.TabIndex = 9;
            this.lblFecha.Text = "Fecha:";
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(12, 9);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(201, 96);
            this.lblTitulo.TabIndex = 8;
            this.lblTitulo.Text = "Reporte de Clientes \r\n   Descuento Real\r\n             VS  \r\nDescuento Sugerido";
            // 
            // dtpFecha
            // 
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(91, 119);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(125, 20);
            this.dtpFecha.TabIndex = 7;
            // 
            // btnContinuar
            // 
            this.btnContinuar.Location = new System.Drawing.Point(138, 171);
            this.btnContinuar.Name = "btnContinuar";
            this.btnContinuar.Size = new System.Drawing.Size(75, 23);
            this.btnContinuar.TabIndex = 12;
            this.btnContinuar.Text = "Continuar";
            this.btnContinuar.UseVisualStyleBackColor = true;
            this.btnContinuar.Click += new System.EventHandler(this.btnContinuar_Click);
            // 
            // frmReporteClientesDSvsDR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(232, 203);
            this.Controls.Add(this.btnContinuar);
            this.Controls.Add(this.txtAgente);
            this.Controls.Add(this.lblAgente);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.dtpFecha);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmReporteClientesDSvsDR";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reporte de Clientes x Ejectuvo DS y DR";
            this.Load += new System.EventHandler(this.frmRepPedidosDSyCMP_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtAgente;
        private System.Windows.Forms.Label lblAgente;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Button btnContinuar;

    }
}