namespace SIP
{
    partial class frmRecepcionFacturas
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
            this.lblRecepción = new System.Windows.Forms.Label();
            this.lblArea = new System.Windows.Forms.Label();
            this.txtArea = new System.Windows.Forms.TextBox();
            this.lblEntrega = new System.Windows.Forms.Label();
            this.txtEntrega = new System.Windows.Forms.TextBox();
            this.lblRecibidas = new System.Windows.Forms.Label();
            this.lstFacturas = new System.Windows.Forms.ListBox();
            this.btnProcesar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.lblFacturas = new System.Windows.Forms.Label();
            this.txtBusqueda = new System.Windows.Forms.TextBox();
            this.dgvFacturas = new System.Windows.Forms.DataGridView();
            this.FACTURA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblFecha = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFacturas)).BeginInit();
            this.SuspendLayout();
            // 
            // lblRecepción
            // 
            this.lblRecepción.AutoSize = true;
            this.lblRecepción.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecepción.Location = new System.Drawing.Point(100, 18);
            this.lblRecepción.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRecepción.Name = "lblRecepción";
            this.lblRecepción.Size = new System.Drawing.Size(289, 29);
            this.lblRecepción.TabIndex = 0;
            this.lblRecepción.Text = "Recepción de Facturas ";
            // 
            // lblArea
            // 
            this.lblArea.AutoSize = true;
            this.lblArea.Location = new System.Drawing.Point(23, 73);
            this.lblArea.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(116, 17);
            this.lblArea.TabIndex = 1;
            this.lblArea.Text = "Area de Entrega:";
            // 
            // txtArea
            // 
            this.txtArea.Location = new System.Drawing.Point(174, 64);
            this.txtArea.Margin = new System.Windows.Forms.Padding(4);
            this.txtArea.Name = "txtArea";
            this.txtArea.Size = new System.Drawing.Size(309, 22);
            this.txtArea.TabIndex = 1;
            // 
            // lblEntrega
            // 
            this.lblEntrega.AutoSize = true;
            this.lblEntrega.Location = new System.Drawing.Point(23, 100);
            this.lblEntrega.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEntrega.Name = "lblEntrega";
            this.lblEntrega.Size = new System.Drawing.Size(147, 17);
            this.lblEntrega.TabIndex = 3;
            this.lblEntrega.Text = "Persona que Entrega:";
            this.lblEntrega.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtEntrega
            // 
            this.txtEntrega.Location = new System.Drawing.Point(174, 96);
            this.txtEntrega.Margin = new System.Windows.Forms.Padding(4);
            this.txtEntrega.Name = "txtEntrega";
            this.txtEntrega.Size = new System.Drawing.Size(309, 22);
            this.txtEntrega.TabIndex = 2;
            // 
            // lblRecibidas
            // 
            this.lblRecibidas.AutoSize = true;
            this.lblRecibidas.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecibidas.Location = new System.Drawing.Point(245, 170);
            this.lblRecibidas.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRecibidas.Name = "lblRecibidas";
            this.lblRecibidas.Size = new System.Drawing.Size(130, 15);
            this.lblRecibidas.TabIndex = 7;
            this.lblRecibidas.Text = "Facturas Recibidas";
            // 
            // lstFacturas
            // 
            this.lstFacturas.FormattingEnabled = true;
            this.lstFacturas.ItemHeight = 16;
            this.lstFacturas.Location = new System.Drawing.Point(249, 190);
            this.lstFacturas.Margin = new System.Windows.Forms.Padding(4);
            this.lstFacturas.Name = "lstFacturas";
            this.lstFacturas.Size = new System.Drawing.Size(189, 260);
            this.lstFacturas.TabIndex = 10;
            // 
            // btnProcesar
            // 
            this.btnProcesar.Location = new System.Drawing.Point(340, 458);
            this.btnProcesar.Margin = new System.Windows.Forms.Padding(4);
            this.btnProcesar.Name = "btnProcesar";
            this.btnProcesar.Size = new System.Drawing.Size(100, 28);
            this.btnProcesar.TabIndex = 5;
            this.btnProcesar.Text = "Procesar";
            this.btnProcesar.UseVisualStyleBackColor = true;
            this.btnProcesar.Click += new System.EventHandler(this.btnProcesar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(448, 190);
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(4);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(36, 28);
            this.btnEliminar.TabIndex = 6;
            this.btnEliminar.Text = "X";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // lblFacturas
            // 
            this.lblFacturas.AutoSize = true;
            this.lblFacturas.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFacturas.Location = new System.Drawing.Point(21, 170);
            this.lblFacturas.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFacturas.Name = "lblFacturas";
            this.lblFacturas.Size = new System.Drawing.Size(133, 15);
            this.lblFacturas.TabIndex = 13;
            this.lblFacturas.Text = "Listado de Facturas";
            // 
            // txtBusqueda
            // 
            this.txtBusqueda.Location = new System.Drawing.Point(25, 190);
            this.txtBusqueda.Margin = new System.Windows.Forms.Padding(4);
            this.txtBusqueda.Name = "txtBusqueda";
            this.txtBusqueda.Size = new System.Drawing.Size(197, 22);
            this.txtBusqueda.TabIndex = 3;
            this.txtBusqueda.TextChanged += new System.EventHandler(this.txtBusqueda_TextChanged);
            this.txtBusqueda.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBusqueda_KeyPress);
            // 
            // dgvFacturas
            // 
            this.dgvFacturas.AllowUserToAddRows = false;
            this.dgvFacturas.AllowUserToDeleteRows = false;
            this.dgvFacturas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFacturas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FACTURA});
            this.dgvFacturas.Location = new System.Drawing.Point(25, 222);
            this.dgvFacturas.Margin = new System.Windows.Forms.Padding(4);
            this.dgvFacturas.Name = "dgvFacturas";
            this.dgvFacturas.ReadOnly = true;
            this.dgvFacturas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFacturas.Size = new System.Drawing.Size(199, 223);
            this.dgvFacturas.TabIndex = 4;
            this.dgvFacturas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvFacturas_KeyDown);
            this.dgvFacturas.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgvFacturas_KeyPress);
            this.dgvFacturas.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvFacturas_KeyUp);
            // 
            // FACTURA
            // 
            this.FACTURA.DataPropertyName = "FACTURA";
            this.FACTURA.HeaderText = "FACTURA";
            this.FACTURA.Name = "FACTURA";
            this.FACTURA.ReadOnly = true;
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(23, 130);
            this.lblFecha.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(117, 17);
            this.lblFecha.TabIndex = 14;
            this.lblFecha.Text = "Fecha recepción:";
            this.lblFecha.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // dtpFecha
            // 
            this.dtpFecha.Enabled = false;
            this.dtpFecha.Location = new System.Drawing.Point(174, 128);
            this.dtpFecha.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(309, 22);
            this.dtpFecha.TabIndex = 15;
            // 
            // frmRecepcionFacturas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 506);
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.dgvFacturas);
            this.Controls.Add(this.txtBusqueda);
            this.Controls.Add(this.lblFacturas);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnProcesar);
            this.Controls.Add(this.lstFacturas);
            this.Controls.Add(this.lblRecibidas);
            this.Controls.Add(this.txtEntrega);
            this.Controls.Add(this.lblEntrega);
            this.Controls.Add(this.txtArea);
            this.Controls.Add(this.lblArea);
            this.Controls.Add(this.lblRecepción);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRecepcionFacturas";
            this.Text = "Recepción";
            this.Load += new System.EventHandler(this.frmRecepción_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFacturas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblRecepción;
        private System.Windows.Forms.Label lblArea;
        private System.Windows.Forms.TextBox txtArea;
        private System.Windows.Forms.Label lblEntrega;
        private System.Windows.Forms.TextBox txtEntrega;
        private System.Windows.Forms.Label lblRecibidas;
        private System.Windows.Forms.ListBox lstFacturas;
        private System.Windows.Forms.Button btnProcesar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Label lblFacturas;
        private System.Windows.Forms.TextBox txtBusqueda;
        private System.Windows.Forms.DataGridView dgvFacturas;
        private System.Windows.Forms.DataGridViewTextBoxColumn FACTURA;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.DateTimePicker dtpFecha;
    }
}