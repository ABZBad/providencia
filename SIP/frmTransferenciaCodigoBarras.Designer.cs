namespace SIP
{
    partial class frmTransferenciaCodigoBarras
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
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.tvResumen = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvRecepciones = new System.Windows.Forms.DataGridView();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblTotalTexto = new System.Windows.Forms.Label();
            this.btnProcesar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPermisoParaTransferir = new System.Windows.Forms.Label();
            this.txtOrigen = new SIP.UserControls.NumericTextBox();
            this.txtDestino = new SIP.UserControls.NumericTextBox();
            this.Modelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Talla = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Eliminar = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecepciones)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(128, 9);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(343, 24);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "Transferencia por Código de Barras";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigo.Location = new System.Drawing.Point(144, 50);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(451, 26);
            this.txtCodigo.TabIndex = 5;
            this.txtCodigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigo_KeyPress);
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Location = new System.Drawing.Point(30, 60);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(108, 13);
            this.lblCodigo.TabIndex = 4;
            this.lblCodigo.Text = "Código de recepción:";
            // 
            // tvResumen
            // 
            this.tvResumen.Location = new System.Drawing.Point(601, 50);
            this.tvResumen.Name = "tvResumen";
            this.tvResumen.Size = new System.Drawing.Size(225, 300);
            this.tvResumen.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(654, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Resumen:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(513, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "Destino";
            // 
            // dgvRecepciones
            // 
            this.dgvRecepciones.AllowUserToAddRows = false;
            this.dgvRecepciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecepciones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Modelo,
            this.Talla,
            this.Cantidad,
            this.Eliminar});
            this.dgvRecepciones.Location = new System.Drawing.Point(12, 108);
            this.dgvRecepciones.Name = "dgvRecepciones";
            this.dgvRecepciones.Size = new System.Drawing.Size(583, 242);
            this.dgvRecepciones.TabIndex = 31;
            this.dgvRecepciones.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRecepciones_CellClick);
            this.dgvRecepciones.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRecepciones_CellEndEdit);
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(217, 353);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(13, 13);
            this.lblTotal.TabIndex = 33;
            this.lblTotal.Text = "0";
            // 
            // lblTotalTexto
            // 
            this.lblTotalTexto.AutoSize = true;
            this.lblTotalTexto.Location = new System.Drawing.Point(12, 353);
            this.lblTotalTexto.Name = "lblTotalTexto";
            this.lblTotalTexto.Size = new System.Drawing.Size(91, 13);
            this.lblTotalTexto.TabIndex = 32;
            this.lblTotalTexto.Text = "Total de Prendas:";
            // 
            // btnProcesar
            // 
            this.btnProcesar.Location = new System.Drawing.Point(520, 356);
            this.btnProcesar.Name = "btnProcesar";
            this.btnProcesar.Size = new System.Drawing.Size(75, 23);
            this.btnProcesar.TabIndex = 34;
            this.btnProcesar.Text = "Procesar";
            this.btnProcesar.UseVisualStyleBackColor = true;
            this.btnProcesar.Click += new System.EventHandler(this.btnProcesar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(419, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Origen";
            // 
            // lblPermisoParaTransferir
            // 
            this.lblPermisoParaTransferir.AutoSize = true;
            this.lblPermisoParaTransferir.ForeColor = System.Drawing.Color.Red;
            this.lblPermisoParaTransferir.Location = new System.Drawing.Point(12, 392);
            this.lblPermisoParaTransferir.Name = "lblPermisoParaTransferir";
            this.lblPermisoParaTransferir.Size = new System.Drawing.Size(0, 13);
            this.lblPermisoParaTransferir.TabIndex = 35;
            // 
            // txtOrigen
            // 
            this.txtOrigen.Location = new System.Drawing.Point(463, 82);
            this.txtOrigen.MaxValue = 0;
            this.txtOrigen.MinValue = 0;
            this.txtOrigen.Name = "txtOrigen";
            this.txtOrigen.Size = new System.Drawing.Size(33, 20);
            this.txtOrigen.TabIndex = 27;
            this.txtOrigen.Text = "1";
            this.txtOrigen.Leave += new System.EventHandler(this.txtOrigen_Leave);
            // 
            // txtDestino
            // 
            this.txtDestino.Location = new System.Drawing.Point(562, 82);
            this.txtDestino.MaxValue = 0;
            this.txtDestino.MinValue = 0;
            this.txtDestino.Name = "txtDestino";
            this.txtDestino.Size = new System.Drawing.Size(33, 20);
            this.txtDestino.TabIndex = 28;
            this.txtDestino.Text = "3";
            this.txtDestino.Leave += new System.EventHandler(this.txtDestino_Leave);
            // 
            // Modelo
            // 
            this.Modelo.DataPropertyName = "Modelo";
            this.Modelo.HeaderText = "Modelo";
            this.Modelo.Name = "Modelo";
            this.Modelo.ReadOnly = true;
            // 
            // Talla
            // 
            this.Talla.DataPropertyName = "Talla";
            this.Talla.HeaderText = "Talla";
            this.Talla.Name = "Talla";
            this.Talla.ReadOnly = true;
            // 
            // Cantidad
            // 
            this.Cantidad.DataPropertyName = "Cantidad";
            this.Cantidad.HeaderText = "Cantidad";
            this.Cantidad.Name = "Cantidad";
            this.Cantidad.ReadOnly = true;
            this.Cantidad.Width = 50;
            // 
            // Eliminar
            // 
            this.Eliminar.HeaderText = "Eliminar";
            this.Eliminar.Name = "Eliminar";
            this.Eliminar.Text = "Eliminar";
            this.Eliminar.UseColumnTextForButtonValue = true;
            // 
            // frmTransferenciaCodigoBarras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 414);
            this.Controls.Add(this.lblPermisoParaTransferir);
            this.Controls.Add(this.btnProcesar);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblTotalTexto);
            this.Controls.Add(this.dgvRecepciones);
            this.Controls.Add(this.txtOrigen);
            this.Controls.Add(this.txtDestino);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tvResumen);
            this.Controls.Add(this.txtCodigo);
            this.Controls.Add(this.lblCodigo);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmTransferenciaCodigoBarras";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transferencia de Inventario por Código de Barras";
            this.Load += new System.EventHandler(this.frmTransferenciaCodigoBarras_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecepciones)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.TreeView tvResumen;
        private System.Windows.Forms.Label label1;
        private UserControls.NumericTextBox txtDestino;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvRecepciones;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblTotalTexto;
        private System.Windows.Forms.Button btnProcesar;
        private UserControls.NumericTextBox txtOrigen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblPermisoParaTransferir;
        private System.Windows.Forms.DataGridViewTextBoxColumn Modelo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Talla;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad;
        private System.Windows.Forms.DataGridViewButtonColumn Eliminar;
    }
}