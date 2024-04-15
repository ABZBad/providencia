namespace SIP
{
    partial class frmApartadoLiberacion35
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblDetalle = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.dgvDetallePedido = new System.Windows.Forms.DataGridView();
            this.btnProcesar = new System.Windows.Forms.Button();
            this.lblNota = new System.Windows.Forms.Label();
            this.btnLiberar = new System.Windows.Forms.Button();
            this.txtFactApa = new System.Windows.Forms.TextBox();
            this.txtReferencia = new System.Windows.Forms.TextBox();
            this.lblReferencia = new System.Windows.Forms.Label();
            this._Pedido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._CVE_ART = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._CANTIDAD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._PRECIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._CANTIDAD_APARTADA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._POR_SURTIR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._ORIGEN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._EXISTENCIAS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._TRANSFERENCIA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetallePedido)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDetalle
            // 
            this.lblDetalle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDetalle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetalle.Location = new System.Drawing.Point(214, 37);
            this.lblDetalle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDetalle.Name = "lblDetalle";
            this.lblDetalle.Size = new System.Drawing.Size(561, 23);
            this.lblDetalle.TabIndex = 12;
            this.lblDetalle.Text = "Detalle de Pedido:";
            this.lblDetalle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitulo
            // 
            this.lblTitulo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitulo.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(60, 9);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(845, 28);
            this.lblTitulo.TabIndex = 11;
            this.lblTitulo.Text = "TRANSFERENCIAS POR APARTADO";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvDetallePedido
            // 
            this.dgvDetallePedido.AllowUserToAddRows = false;
            this.dgvDetallePedido.AllowUserToDeleteRows = false;
            this.dgvDetallePedido.AllowUserToResizeColumns = false;
            this.dgvDetallePedido.AllowUserToResizeRows = false;
            this.dgvDetallePedido.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDetallePedido.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvDetallePedido.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvDetallePedido.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvDetallePedido.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetallePedido.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._Pedido,
            this._CVE_ART,
            this._CANTIDAD,
            this._PRECIO,
            this._CANTIDAD_APARTADA,
            this._POR_SURTIR,
            this._ORIGEN,
            this._EXISTENCIAS,
            this._TRANSFERENCIA});
            this.dgvDetallePedido.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvDetallePedido.Location = new System.Drawing.Point(9, 70);
            this.dgvDetallePedido.Margin = new System.Windows.Forms.Padding(0);
            this.dgvDetallePedido.MultiSelect = false;
            this.dgvDetallePedido.Name = "dgvDetallePedido";
            this.dgvDetallePedido.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.White;
            this.dgvDetallePedido.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvDetallePedido.Size = new System.Drawing.Size(960, 379);
            this.dgvDetallePedido.TabIndex = 13;
            this.dgvDetallePedido.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetallePedido_CellEndEdit);
            this.dgvDetallePedido.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvDetallePedido_EditingControlShowing);
            this.dgvDetallePedido.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgvDetallePedido_KeyPress);
            // 
            // btnProcesar
            // 
            this.btnProcesar.Location = new System.Drawing.Point(795, 520);
            this.btnProcesar.Name = "btnProcesar";
            this.btnProcesar.Size = new System.Drawing.Size(174, 32);
            this.btnProcesar.TabIndex = 14;
            this.btnProcesar.Text = "Procesar apartado";
            this.btnProcesar.UseVisualStyleBackColor = true;
            this.btnProcesar.Click += new System.EventHandler(this.btnProcesar_Click);
            // 
            // lblNota
            // 
            this.lblNota.Location = new System.Drawing.Point(9, 460);
            this.lblNota.Name = "lblNota";
            this.lblNota.Size = new System.Drawing.Size(957, 57);
            this.lblNota.TabIndex = 15;
            this.lblNota.Text = "label1";
            // 
            // btnLiberar
            // 
            this.btnLiberar.Location = new System.Drawing.Point(795, 558);
            this.btnLiberar.Name = "btnLiberar";
            this.btnLiberar.Size = new System.Drawing.Size(174, 32);
            this.btnLiberar.TabIndex = 16;
            this.btnLiberar.Text = "Liberar Pedido";
            this.btnLiberar.UseVisualStyleBackColor = true;
            this.btnLiberar.Click += new System.EventHandler(this.btnLiberar_Click);
            // 
            // txtFactApa
            // 
            this.txtFactApa.Enabled = false;
            this.txtFactApa.Location = new System.Drawing.Point(622, 520);
            this.txtFactApa.Name = "txtFactApa";
            this.txtFactApa.Size = new System.Drawing.Size(65, 22);
            this.txtFactApa.TabIndex = 19;
            this.txtFactApa.Text = "FactApa:";
            // 
            // txtReferencia
            // 
            this.txtReferencia.Location = new System.Drawing.Point(693, 520);
            this.txtReferencia.Name = "txtReferencia";
            this.txtReferencia.Size = new System.Drawing.Size(96, 22);
            this.txtReferencia.TabIndex = 18;
            // 
            // lblReferencia
            // 
            this.lblReferencia.AutoSize = true;
            this.lblReferencia.Location = new System.Drawing.Point(387, 523);
            this.lblReferencia.Name = "lblReferencia";
            this.lblReferencia.Size = new System.Drawing.Size(229, 34);
            this.lblReferencia.TabIndex = 17;
            this.lblReferencia.Text = "Introduce una referencia numérica \r\n(Máximo 10 Caracteres)";
            // 
            // _Pedido
            // 
            this._Pedido.DataPropertyName = "Pedido";
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this._Pedido.DefaultCellStyle = dataGridViewCellStyle1;
            this._Pedido.HeaderText = "Pedido";
            this._Pedido.Name = "_Pedido";
            this._Pedido.ReadOnly = true;
            this._Pedido.Visible = false;
            this._Pedido.Width = 77;
            // 
            // _CVE_ART
            // 
            this._CVE_ART.DataPropertyName = "CVE_ART";
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this._CVE_ART.DefaultCellStyle = dataGridViewCellStyle2;
            this._CVE_ART.HeaderText = "ARTICULO";
            this._CVE_ART.Name = "_CVE_ART";
            this._CVE_ART.ReadOnly = true;
            this._CVE_ART.Width = 115;
            // 
            // _CANTIDAD
            // 
            this._CANTIDAD.DataPropertyName = "CANT";
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this._CANTIDAD.DefaultCellStyle = dataGridViewCellStyle3;
            this._CANTIDAD.HeaderText = "CANTIDAD";
            this._CANTIDAD.Name = "_CANTIDAD";
            this._CANTIDAD.ReadOnly = true;
            this._CANTIDAD.Width = 102;
            // 
            // _PRECIO
            // 
            this._PRECIO.DataPropertyName = "PREC";
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this._PRECIO.DefaultCellStyle = dataGridViewCellStyle4;
            this._PRECIO.HeaderText = "PRECIO";
            this._PRECIO.Name = "_PRECIO";
            this._PRECIO.ReadOnly = true;
            this._PRECIO.Width = 84;
            // 
            // _CANTIDAD_APARTADA
            // 
            this._CANTIDAD_APARTADA.DataPropertyName = "APARTADO";
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this._CANTIDAD_APARTADA.DefaultCellStyle = dataGridViewCellStyle5;
            this._CANTIDAD_APARTADA.HeaderText = "CANTIDAD APARTADA";
            this._CANTIDAD_APARTADA.Name = "_CANTIDAD_APARTADA";
            this._CANTIDAD_APARTADA.ReadOnly = true;
            this._CANTIDAD_APARTADA.Width = 164;
            // 
            // _POR_SURTIR
            // 
            this._POR_SURTIR.DataPropertyName = "PORSURTIR";
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this._POR_SURTIR.DefaultCellStyle = dataGridViewCellStyle6;
            this._POR_SURTIR.HeaderText = "POR SURTIR";
            this._POR_SURTIR.Name = "_POR_SURTIR";
            this._POR_SURTIR.ReadOnly = true;
            this._POR_SURTIR.Width = 108;
            // 
            // _ORIGEN
            // 
            this._ORIGEN.DataPropertyName = "ORIGEN";
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this._ORIGEN.DefaultCellStyle = dataGridViewCellStyle7;
            this._ORIGEN.HeaderText = "ORIGEN";
            this._ORIGEN.Name = "_ORIGEN";
            this._ORIGEN.ReadOnly = true;
            this._ORIGEN.Width = 70;
            // 
            // _EXISTENCIAS
            // 
            this._EXISTENCIAS.DataPropertyName = "EXIST";
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this._EXISTENCIAS.DefaultCellStyle = dataGridViewCellStyle8;
            this._EXISTENCIAS.HeaderText = "EXISTENCIAS";
            this._EXISTENCIAS.Name = "_EXISTENCIAS";
            this._EXISTENCIAS.ReadOnly = true;
            // 
            // _TRANSFERENCIA
            // 
            this._TRANSFERENCIA.HeaderText = "TRANSFERENCIA";
            this._TRANSFERENCIA.Name = "_TRANSFERENCIA";
            this._TRANSFERENCIA.Width = 147;
            // 
            // frmApartadoLiberacion35
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 623);
            this.Controls.Add(this.txtFactApa);
            this.Controls.Add(this.txtReferencia);
            this.Controls.Add(this.lblReferencia);
            this.Controls.Add(this.btnLiberar);
            this.Controls.Add(this.lblNota);
            this.Controls.Add(this.btnProcesar);
            this.Controls.Add(this.dgvDetallePedido);
            this.Controls.Add(this.lblDetalle);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmApartadoLiberacion35";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Liberación Almacen 35";
            this.Load += new System.EventHandler(this.frmApartadoLiberacion35_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetallePedido)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDetalle;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.DataGridView dgvDetallePedido;
        private System.Windows.Forms.Button btnProcesar;
        private System.Windows.Forms.Label lblNota;
        private System.Windows.Forms.Button btnLiberar;
        private System.Windows.Forms.TextBox txtFactApa;
        private System.Windows.Forms.TextBox txtReferencia;
        private System.Windows.Forms.Label lblReferencia;
        private System.Windows.Forms.DataGridViewTextBoxColumn _Pedido;
        private System.Windows.Forms.DataGridViewTextBoxColumn _CVE_ART;
        private System.Windows.Forms.DataGridViewTextBoxColumn _CANTIDAD;
        private System.Windows.Forms.DataGridViewTextBoxColumn _PRECIO;
        private System.Windows.Forms.DataGridViewTextBoxColumn _CANTIDAD_APARTADA;
        private System.Windows.Forms.DataGridViewTextBoxColumn _POR_SURTIR;
        private System.Windows.Forms.DataGridViewTextBoxColumn _ORIGEN;
        private System.Windows.Forms.DataGridViewTextBoxColumn _EXISTENCIAS;
        private System.Windows.Forms.DataGridViewTextBoxColumn _TRANSFERENCIA;
    }
}