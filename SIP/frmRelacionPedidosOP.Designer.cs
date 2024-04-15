namespace SIP
{
    partial class frmRelacionPedidosOP
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblDetallePedidos = new System.Windows.Forms.Label();
            this.dgvPedidos = new System.Windows.Forms.DataGridView();
            this.btnFinalizar = new System.Windows.Forms.Button();
            this.Seleccion = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Pedido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClaveCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaPedido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Modelos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Prendas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblDetalleOP = new System.Windows.Forms.Label();
            this.dgvOPDetalle = new System.Windows.Forms.DataGridView();
            this.Modelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrdenProduccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrdenMaquila = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOPDetalle)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDetallePedidos
            // 
            this.lblDetallePedidos.AutoSize = true;
            this.lblDetallePedidos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetallePedidos.Location = new System.Drawing.Point(566, 22);
            this.lblDetallePedidos.Name = "lblDetallePedidos";
            this.lblDetallePedidos.Size = new System.Drawing.Size(768, 20);
            this.lblDetallePedidos.TabIndex = 0;
            this.lblDetallePedidos.Text = "Seleccione los pedidos que desea relacioanr con las Ordendes de Producción Genera" +
    "das:";
            // 
            // dgvPedidos
            // 
            this.dgvPedidos.AllowUserToAddRows = false;
            this.dgvPedidos.AllowUserToDeleteRows = false;
            this.dgvPedidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPedidos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Seleccion,
            this.Pedido,
            this.ClaveCliente,
            this.NombreCliente,
            this.FechaPedido,
            this.Modelos,
            this.Prendas});
            this.dgvPedidos.Location = new System.Drawing.Point(570, 58);
            this.dgvPedidos.Name = "dgvPedidos";
            this.dgvPedidos.RowHeadersWidth = 51;
            this.dgvPedidos.RowTemplate.Height = 24;
            this.dgvPedidos.Size = new System.Drawing.Size(861, 620);
            this.dgvPedidos.TabIndex = 1;
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.Location = new System.Drawing.Point(1356, 684);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(75, 23);
            this.btnFinalizar.TabIndex = 2;
            this.btnFinalizar.Text = "Finalizar";
            this.btnFinalizar.UseVisualStyleBackColor = true;
            this.btnFinalizar.Click += new System.EventHandler(this.btnFinalizar_Click);
            // 
            // Seleccion
            // 
            this.Seleccion.HeaderText = "";
            this.Seleccion.MinimumWidth = 6;
            this.Seleccion.Name = "Seleccion";
            this.Seleccion.Width = 40;
            // 
            // Pedido
            // 
            this.Pedido.DataPropertyName = "Pedido";
            this.Pedido.HeaderText = "Pedido";
            this.Pedido.MinimumWidth = 6;
            this.Pedido.Name = "Pedido";
            this.Pedido.ReadOnly = true;
            this.Pedido.Width = 125;
            // 
            // ClaveCliente
            // 
            this.ClaveCliente.DataPropertyName = "ClaveCliente";
            this.ClaveCliente.HeaderText = "Clave";
            this.ClaveCliente.MinimumWidth = 6;
            this.ClaveCliente.Name = "ClaveCliente";
            this.ClaveCliente.ReadOnly = true;
            this.ClaveCliente.Width = 125;
            // 
            // NombreCliente
            // 
            this.NombreCliente.DataPropertyName = "NombreCliente";
            this.NombreCliente.HeaderText = "Cliente";
            this.NombreCliente.MinimumWidth = 6;
            this.NombreCliente.Name = "NombreCliente";
            this.NombreCliente.ReadOnly = true;
            this.NombreCliente.Width = 125;
            // 
            // FechaPedido
            // 
            this.FechaPedido.DataPropertyName = "FechaPedido";
            dataGridViewCellStyle7.Format = "d";
            dataGridViewCellStyle7.NullValue = null;
            this.FechaPedido.DefaultCellStyle = dataGridViewCellStyle7;
            this.FechaPedido.HeaderText = "Fecha";
            this.FechaPedido.MinimumWidth = 6;
            this.FechaPedido.Name = "FechaPedido";
            this.FechaPedido.ReadOnly = true;
            this.FechaPedido.Width = 125;
            // 
            // Modelos
            // 
            this.Modelos.DataPropertyName = "Modelos";
            this.Modelos.HeaderText = "Modelos";
            this.Modelos.MinimumWidth = 6;
            this.Modelos.Name = "Modelos";
            this.Modelos.ReadOnly = true;
            this.Modelos.Width = 125;
            // 
            // Prendas
            // 
            this.Prendas.DataPropertyName = "Prendas";
            dataGridViewCellStyle8.Format = "N0";
            dataGridViewCellStyle8.NullValue = null;
            this.Prendas.DefaultCellStyle = dataGridViewCellStyle8;
            this.Prendas.HeaderText = "Prendas";
            this.Prendas.MinimumWidth = 6;
            this.Prendas.Name = "Prendas";
            this.Prendas.ReadOnly = true;
            this.Prendas.Width = 125;
            // 
            // lblDetalleOP
            // 
            this.lblDetalleOP.AutoSize = true;
            this.lblDetalleOP.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetalleOP.Location = new System.Drawing.Point(43, 22);
            this.lblDetalleOP.Name = "lblDetalleOP";
            this.lblDetalleOP.Size = new System.Drawing.Size(394, 25);
            this.lblDetalleOP.TabIndex = 3;
            this.lblDetalleOP.Text = "Detalle de las OP correspondientes:";
            // 
            // dgvOPDetalle
            // 
            this.dgvOPDetalle.AllowUserToAddRows = false;
            this.dgvOPDetalle.AllowUserToDeleteRows = false;
            this.dgvOPDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOPDetalle.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Modelo,
            this.OrdenProduccion,
            this.OrdenMaquila,
            this.Total});
            this.dgvOPDetalle.Location = new System.Drawing.Point(31, 58);
            this.dgvOPDetalle.Name = "dgvOPDetalle";
            this.dgvOPDetalle.ReadOnly = true;
            this.dgvOPDetalle.RowHeadersWidth = 51;
            this.dgvOPDetalle.RowTemplate.Height = 24;
            this.dgvOPDetalle.Size = new System.Drawing.Size(497, 620);
            this.dgvOPDetalle.TabIndex = 4;
            // 
            // Modelo
            // 
            this.Modelo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Modelo.DataPropertyName = "Modelo";
            this.Modelo.HeaderText = "Modelo";
            this.Modelo.MinimumWidth = 6;
            this.Modelo.Name = "Modelo";
            this.Modelo.ReadOnly = true;
            // 
            // OrdenProduccion
            // 
            this.OrdenProduccion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.OrdenProduccion.DataPropertyName = "OrdenProduccion";
            this.OrdenProduccion.HeaderText = "OP";
            this.OrdenProduccion.MinimumWidth = 6;
            this.OrdenProduccion.Name = "OrdenProduccion";
            this.OrdenProduccion.ReadOnly = true;
            // 
            // OrdenMaquila
            // 
            this.OrdenMaquila.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.OrdenMaquila.DataPropertyName = "OrdenMaquila";
            this.OrdenMaquila.HeaderText = "OM";
            this.OrdenMaquila.MinimumWidth = 6;
            this.OrdenMaquila.Name = "OrdenMaquila";
            this.OrdenMaquila.ReadOnly = true;
            // 
            // Total
            // 
            this.Total.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Total.DataPropertyName = "Prendas";
            this.Total.HeaderText = "Total";
            this.Total.MinimumWidth = 6;
            this.Total.Name = "Total";
            this.Total.ReadOnly = true;
            // 
            // frmRelacionPedidosOP
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1443, 718);
            this.Controls.Add(this.dgvOPDetalle);
            this.Controls.Add(this.lblDetalleOP);
            this.Controls.Add(this.btnFinalizar);
            this.Controls.Add(this.dgvPedidos);
            this.Controls.Add(this.lblDetallePedidos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmRelacionPedidosOP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relación de Pedidos con OPs";
            this.Load += new System.EventHandler(this.frmRelacionPedidosOP_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOPDetalle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDetallePedidos;
        private System.Windows.Forms.DataGridView dgvPedidos;
        private System.Windows.Forms.Button btnFinalizar;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Seleccion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pedido;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClaveCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaPedido;
        private System.Windows.Forms.DataGridViewTextBoxColumn Modelos;
        private System.Windows.Forms.DataGridViewTextBoxColumn Prendas;
        private System.Windows.Forms.Label lblDetalleOP;
        private System.Windows.Forms.DataGridView dgvOPDetalle;
        private System.Windows.Forms.DataGridViewTextBoxColumn Modelo;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrdenProduccion;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrdenMaquila;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
    }
}