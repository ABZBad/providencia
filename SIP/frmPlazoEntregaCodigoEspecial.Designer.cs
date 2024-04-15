namespace SIP
{
    partial class frmPlazoEntregaCodigoEspecial
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
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblPlazoEntrega = new System.Windows.Forms.Label();
            this.lblCodigoEspecial = new System.Windows.Forms.Label();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.txtCodigoEspecial = new SIP.UserControls.TextBoxEx();
            this.txtPlazoEntrega = new SIP.UserControls.TextBoxEx();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(36, 11);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(311, 62);
            this.lblTitulo.TabIndex = 3;
            this.lblTitulo.Text = "PLAZO DE ENTREGA \r\nY CÓDIGO ESPECIAL";
            // 
            // lblPlazoEntrega
            // 
            this.lblPlazoEntrega.AutoSize = true;
            this.lblPlazoEntrega.Location = new System.Drawing.Point(16, 106);
            this.lblPlazoEntrega.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPlazoEntrega.Name = "lblPlazoEntrega";
            this.lblPlazoEntrega.Size = new System.Drawing.Size(197, 17);
            this.lblPlazoEntrega.TabIndex = 4;
            this.lblPlazoEntrega.Text = "Introduce el plazo de entrega:";
            // 
            // lblCodigoEspecial
            // 
            this.lblCodigoEspecial.AutoSize = true;
            this.lblCodigoEspecial.Location = new System.Drawing.Point(16, 138);
            this.lblCodigoEspecial.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCodigoEspecial.Name = "lblCodigoEspecial";
            this.lblCodigoEspecial.Size = new System.Drawing.Size(184, 17);
            this.lblCodigoEspecial.TabIndex = 6;
            this.lblCodigoEspecial.Text = "Introduce el código especial";
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(221, 166);
            this.btnAceptar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(203, 28);
            this.btnAceptar.TabIndex = 8;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // txtCodigoEspecial
            // 
            this.txtCodigoEspecial.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCodigoEspecial.Location = new System.Drawing.Point(221, 134);
            this.txtCodigoEspecial.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCodigoEspecial.Name = "txtCodigoEspecial";
            this.txtCodigoEspecial.OnlyUpperCase = true;
            this.txtCodigoEspecial.Size = new System.Drawing.Size(201, 22);
            this.txtCodigoEspecial.TabIndex = 7;
            this.txtCodigoEspecial.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigoEspecial_KeyPress);
            // 
            // txtPlazoEntrega
            // 
            this.txtPlazoEntrega.Location = new System.Drawing.Point(221, 102);
            this.txtPlazoEntrega.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPlazoEntrega.Name = "txtPlazoEntrega";
            this.txtPlazoEntrega.OnlyUpperCase = false;
            this.txtPlazoEntrega.Size = new System.Drawing.Size(201, 22);
            this.txtPlazoEntrega.TabIndex = 5;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(221, 202);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(203, 28);
            this.btnCancelar.TabIndex = 9;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // frmPlazoEntregaCodigoEspecial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 241);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.txtCodigoEspecial);
            this.Controls.Add(this.lblCodigoEspecial);
            this.Controls.Add(this.txtPlazoEntrega);
            this.Controls.Add(this.lblPlazoEntrega);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPlazoEntregaCodigoEspecial";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Solicitud de Código Especial";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblPlazoEntrega;
        private UserControls.TextBoxEx txtPlazoEntrega;
        private UserControls.TextBoxEx txtCodigoEspecial;
        private System.Windows.Forms.Label lblCodigoEspecial;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;

    }
}