namespace SIP
{
    partial class frmFacturasPorAnticipado
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
            this.lblDetalle = new System.Windows.Forms.Label();
            this.dgvDetallePedido = new System.Windows.Forms.DataGridView();
            this.btnProcesar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.lblReferencia = new System.Windows.Forms.Label();
            this.txtReferencia = new System.Windows.Forms.TextBox();
            this.txtFactAnt = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetallePedido)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitulo.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(16, 11);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(490, 28);
            this.lblTitulo.TabIndex = 4;
            this.lblTitulo.Text = "FACTURAS POR ANTICIPADO";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDetalle
            // 
            this.lblDetalle.AutoSize = true;
            this.lblDetalle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetalle.Location = new System.Drawing.Point(183, 39);
            this.lblDetalle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDetalle.Name = "lblDetalle";
            this.lblDetalle.Size = new System.Drawing.Size(142, 17);
            this.lblDetalle.TabIndex = 9;
            this.lblDetalle.Text = "Detalle de Pedido:";
            // 
            // dgvDetallePedido
            // 
            this.dgvDetallePedido.AllowUserToAddRows = false;
            this.dgvDetallePedido.AllowUserToDeleteRows = false;
            this.dgvDetallePedido.AllowUserToOrderColumns = true;
            this.dgvDetallePedido.AllowUserToResizeColumns = false;
            this.dgvDetallePedido.AllowUserToResizeRows = false;
            this.dgvDetallePedido.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvDetallePedido.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvDetallePedido.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvDetallePedido.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetallePedido.Location = new System.Drawing.Point(20, 69);
            this.dgvDetallePedido.Margin = new System.Windows.Forms.Padding(0);
            this.dgvDetallePedido.MultiSelect = false;
            this.dgvDetallePedido.Name = "dgvDetallePedido";
            this.dgvDetallePedido.ReadOnly = true;
            this.dgvDetallePedido.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.White;
            this.dgvDetallePedido.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvDetallePedido.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetallePedido.Size = new System.Drawing.Size(490, 389);
            this.dgvDetallePedido.TabIndex = 10;
            // 
            // btnProcesar
            // 
            this.btnProcesar.Location = new System.Drawing.Point(435, 461);
            this.btnProcesar.Name = "btnProcesar";
            this.btnProcesar.Size = new System.Drawing.Size(75, 23);
            this.btnProcesar.TabIndex = 11;
            this.btnProcesar.Text = "Procesar";
            this.btnProcesar.UseVisualStyleBackColor = true;
            this.btnProcesar.Click += new System.EventHandler(this.btnProcesar_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Location = new System.Drawing.Point(435, 490);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(75, 23);
            this.btnCerrar.TabIndex = 12;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // lblReferencia
            // 
            this.lblReferencia.AutoSize = true;
            this.lblReferencia.Location = new System.Drawing.Point(27, 464);
            this.lblReferencia.Name = "lblReferencia";
            this.lblReferencia.Size = new System.Drawing.Size(229, 34);
            this.lblReferencia.TabIndex = 13;
            this.lblReferencia.Text = "Introduce una referencia numérica \r\n(Máximo 10 Caracteres)";
            // 
            // txtReferencia
            // 
            this.txtReferencia.Location = new System.Drawing.Point(333, 461);
            this.txtReferencia.Name = "txtReferencia";
            this.txtReferencia.Size = new System.Drawing.Size(96, 22);
            this.txtReferencia.TabIndex = 14;
            // 
            // txtFactAnt
            // 
            this.txtFactAnt.Enabled = false;
            this.txtFactAnt.Location = new System.Drawing.Point(262, 461);
            this.txtFactAnt.Name = "txtFactAnt";
            this.txtFactAnt.Size = new System.Drawing.Size(65, 22);
            this.txtFactAnt.TabIndex = 15;
            this.txtFactAnt.Text = "FactAnt:";
            // 
            // frmFacturasPorAnticipado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 526);
            this.Controls.Add(this.txtFactAnt);
            this.Controls.Add(this.txtReferencia);
            this.Controls.Add(this.lblReferencia);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.btnProcesar);
            this.Controls.Add(this.dgvDetallePedido);
            this.Controls.Add(this.lblDetalle);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmFacturasPorAnticipado";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SIP - Facturas por Anticipado";
            this.Load += new System.EventHandler(this.frmFacturasPorAnticipado_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetallePedido)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblDetalle;
        private System.Windows.Forms.DataGridView dgvDetallePedido;
        private System.Windows.Forms.Button btnProcesar;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label lblReferencia;
        private System.Windows.Forms.TextBox txtReferencia;
        private System.Windows.Forms.TextBox txtFactAnt;
    }
}