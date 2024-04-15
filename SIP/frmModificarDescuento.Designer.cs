namespace SIP
{
    partial class frmModificarDescuento
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
            this.btnActualizar = new System.Windows.Forms.Button();
            this.txtPrecioActual = new SIP.UserControls.NumericTextBox();
            this.txtPrecioAnterior = new SIP.UserControls.NumericTextBox();
            this.scape1 = new SIP.UserControls.Scape();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Descuento Anterior (%)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(17, 46);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(162, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "Descuento Nuevo (%)";
            // 
            // btnActualizar
            // 
            this.btnActualizar.Location = new System.Drawing.Point(57, 85);
            this.btnActualizar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(260, 28);
            this.btnActualizar.TabIndex = 4;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // txtPrecioActual
            // 
            this.txtPrecioActual.Location = new System.Drawing.Point(200, 45);
            this.txtPrecioActual.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPrecioActual.MaxValue = 0;
            this.txtPrecioActual.MinValue = 0;
            this.txtPrecioActual.Name = "txtPrecioActual";
            this.txtPrecioActual.NumberType = SIP.UserControls.TipoDeNumero.Decimal;
            this.txtPrecioActual.Size = new System.Drawing.Size(132, 22);
            this.txtPrecioActual.TabIndex = 6;
            // 
            // txtPrecioAnterior
            // 
            this.txtPrecioAnterior.Location = new System.Drawing.Point(200, 15);
            this.txtPrecioAnterior.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPrecioAnterior.MaxValue = 0;
            this.txtPrecioAnterior.MinValue = 0;
            this.txtPrecioAnterior.Name = "txtPrecioAnterior";
            this.txtPrecioAnterior.NumberType = SIP.UserControls.TipoDeNumero.Decimal;
            this.txtPrecioAnterior.Size = new System.Drawing.Size(132, 22);
            this.txtPrecioAnterior.TabIndex = 5;
            // 
            // scape1
            // 
            this.scape1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.scape1.Form = this;
            // 
            // frmModificarDescuento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.scape1;
            this.ClientSize = new System.Drawing.Size(348, 126);
            this.Controls.Add(this.txtPrecioActual);
            this.Controls.Add(this.txtPrecioAnterior);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmModificarDescuento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "d";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnActualizar;
        private UserControls.NumericTextBox txtPrecioAnterior;
        private UserControls.NumericTextBox txtPrecioActual;
        private UserControls.Scape scape1;
    }
}