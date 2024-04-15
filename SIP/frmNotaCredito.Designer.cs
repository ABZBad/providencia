namespace SIP
{
    partial class frmNotaCredito
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
            this.gbPedidos = new System.Windows.Forms.GroupBox();
            this.dgvPedidos = new System.Windows.Forms.DataGridView();
            this.FACTURA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FECHA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CLIENTE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtBusqueda = new System.Windows.Forms.TextBox();
            this.lblBuscaPedido = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.gbPedidos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidos)).BeginInit();
            this.SuspendLayout();
            // 
            // gbPedidos
            // 
            this.gbPedidos.Controls.Add(this.dgvPedidos);
            this.gbPedidos.Controls.Add(this.txtBusqueda);
            this.gbPedidos.Controls.Add(this.lblBuscaPedido);
            this.gbPedidos.Location = new System.Drawing.Point(12, 32);
            this.gbPedidos.Name = "gbPedidos";
            this.gbPedidos.Size = new System.Drawing.Size(989, 319);
            this.gbPedidos.TabIndex = 3;
            this.gbPedidos.TabStop = false;
            this.gbPedidos.Text = "Listado de Facturas";
            // 
            // dgvPedidos
            // 
            this.dgvPedidos.AllowUserToAddRows = false;
            this.dgvPedidos.AllowUserToDeleteRows = false;
            this.dgvPedidos.AllowUserToOrderColumns = true;
            this.dgvPedidos.AllowUserToResizeColumns = false;
            this.dgvPedidos.AllowUserToResizeRows = false;
            this.dgvPedidos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPedidos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvPedidos.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvPedidos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvPedidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPedidos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FACTURA,
            this.FECHA,
            this.CLIENTE});
            this.dgvPedidos.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvPedidos.Location = new System.Drawing.Point(6, 68);
            this.dgvPedidos.MultiSelect = false;
            this.dgvPedidos.Name = "dgvPedidos";
            this.dgvPedidos.RowTemplate.Height = 24;
            this.dgvPedidos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPedidos.Size = new System.Drawing.Size(977, 245);
            this.dgvPedidos.TabIndex = 10;
            this.dgvPedidos.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPedidos_CellEnter);
            this.dgvPedidos.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvPedidos_CellFormatting);
            this.dgvPedidos.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvPedidos_CellMouseDoubleClick);
            this.dgvPedidos.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvPedidos_CellPainting);
            this.dgvPedidos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvPedidos_KeyDown);
            // 
            // FACTURA
            // 
            this.FACTURA.DataPropertyName = "Factura";
            this.FACTURA.HeaderText = "Factura";
            this.FACTURA.Name = "FACTURA";
            this.FACTURA.ReadOnly = true;
            this.FACTURA.Width = 81;
            // 
            // FECHA
            // 
            this.FECHA.DataPropertyName = "Fecha";
            dataGridViewCellStyle1.Format = "d";
            dataGridViewCellStyle1.NullValue = null;
            this.FECHA.DefaultCellStyle = dataGridViewCellStyle1;
            this.FECHA.HeaderText = "Fecha";
            this.FECHA.Name = "FECHA";
            this.FECHA.ReadOnly = true;
            this.FECHA.Width = 72;
            // 
            // CLIENTE
            // 
            this.CLIENTE.DataPropertyName = "Cliente";
            this.CLIENTE.HeaderText = "Cliente";
            this.CLIENTE.Name = "CLIENTE";
            this.CLIENTE.ReadOnly = true;
            this.CLIENTE.Width = 76;
            // 
            // txtBusqueda
            // 
            this.txtBusqueda.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBusqueda.Location = new System.Drawing.Point(142, 30);
            this.txtBusqueda.Name = "txtBusqueda";
            this.txtBusqueda.Size = new System.Drawing.Size(841, 22);
            this.txtBusqueda.TabIndex = 1;
            this.txtBusqueda.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBusqueda_KeyDown);
            this.txtBusqueda.Leave += new System.EventHandler(this.txtBusqueda_Leave);
            // 
            // lblBuscaPedido
            // 
            this.lblBuscaPedido.AutoSize = true;
            this.lblBuscaPedido.Location = new System.Drawing.Point(32, 33);
            this.lblBuscaPedido.Name = "lblBuscaPedido";
            this.lblBuscaPedido.Size = new System.Drawing.Size(108, 17);
            this.lblBuscaPedido.TabIndex = 0;
            this.lblBuscaPedido.Text = "Buscar Factura:";
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(340, 4);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(223, 25);
            this.lblTitulo.TabIndex = 4;
            this.lblTitulo.Text = "Buscador de Facturas";
            // 
            // frmNotaCredito
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 371);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.gbPedidos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmNotaCredito";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Buscador";
            this.Load += new System.EventHandler(this.frmNotaCredito_Load);
            this.gbPedidos.ResumeLayout(false);
            this.gbPedidos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbPedidos;
        private System.Windows.Forms.DataGridView dgvPedidos;
        private System.Windows.Forms.TextBox txtBusqueda;
        private System.Windows.Forms.Label lblBuscaPedido;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.DataGridViewTextBoxColumn FACTURA;
        private System.Windows.Forms.DataGridViewTextBoxColumn FECHA;
        private System.Windows.Forms.DataGridViewTextBoxColumn CLIENTE;
    }
}