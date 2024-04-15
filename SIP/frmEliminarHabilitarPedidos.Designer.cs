namespace SIP
{
    partial class frmEliminarHabilitarPedidos
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
            this.rbNormal = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.rbVirtual = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNumeroPedido = new System.Windows.Forms.TextBox();
            this.btnContinuar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rbNormal
            // 
            this.rbNormal.AutoSize = true;
            this.rbNormal.Checked = true;
            this.rbNormal.Location = new System.Drawing.Point(17, 48);
            this.rbNormal.Name = "rbNormal";
            this.rbNormal.Size = new System.Drawing.Size(163, 21);
            this.rbNormal.TabIndex = 0;
            this.rbNormal.TabStop = true;
            this.rbNormal.Text = "Habilitación completa";
            this.rbNormal.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(235, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Cancelación de Pedido";
            // 
            // rbVirtual
            // 
            this.rbVirtual.AutoSize = true;
            this.rbVirtual.Location = new System.Drawing.Point(220, 48);
            this.rbVirtual.Name = "rbVirtual";
            this.rbVirtual.Size = new System.Drawing.Size(253, 21);
            this.rbVirtual.TabIndex = 2;
            this.rbVirtual.Text = "Habilitación para división de pedido";
            this.rbVirtual.UseVisualStyleBackColor = true;
            this.rbVirtual.CheckedChanged += new System.EventHandler(this.rbVirtual_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(197, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Numero de pedido a Cancelar";
            // 
            // txtNumeroPedido
            // 
            this.txtNumeroPedido.Location = new System.Drawing.Point(217, 92);
            this.txtNumeroPedido.Name = "txtNumeroPedido";
            this.txtNumeroPedido.Size = new System.Drawing.Size(260, 22);
            this.txtNumeroPedido.TabIndex = 4;
            this.txtNumeroPedido.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumeroPedido_KeyPress);
            this.txtNumeroPedido.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtNumeroPedido_KeyUp);
            // 
            // btnContinuar
            // 
            this.btnContinuar.Location = new System.Drawing.Point(394, 120);
            this.btnContinuar.Name = "btnContinuar";
            this.btnContinuar.Size = new System.Drawing.Size(83, 28);
            this.btnContinuar.TabIndex = 5;
            this.btnContinuar.Text = "Continuar";
            this.btnContinuar.UseVisualStyleBackColor = true;
            this.btnContinuar.Click += new System.EventHandler(this.btnContinuar_Click);
            // 
            // frmEliminarHabilitarPedidos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 164);
            this.Controls.Add(this.btnContinuar);
            this.Controls.Add(this.txtNumeroPedido);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rbVirtual);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbNormal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmEliminarHabilitarPedidos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Eliminar/Habilitar Pedido";
            this.Load += new System.EventHandler(this.frmEliminarHabilitarPedidos_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbNormal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbVirtual;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNumeroPedido;
        private System.Windows.Forms.Button btnContinuar;
    }
}