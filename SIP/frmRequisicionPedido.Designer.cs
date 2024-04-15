namespace SIP
{
    partial class frmRequisicionPedido
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
            this.dgvDetalle = new System.Windows.Forms.DataGridView();
            this.Detalle_Modelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Detalle_Modelo_Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Detalle_Tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Detalle_Modelo_Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Detalle_Componente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Detalle_Componente_Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Detalle_Componente_Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.lblNumeroPedido = new System.Windows.Forms.Label();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.dgvMP = new System.Windows.Forms.DataGridView();
            this.lblMP = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvComprado = new System.Windows.Forms.DataGridView();
            this.Comprado_Seleccionar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Comprado_Modelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comprado_Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comprado_Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MP_Seleccionar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.MP_Componente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MP_Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MP_Unidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MP_Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MP_Existencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComprado)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(12, 9);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(297, 29);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Detalle de la requisición";
            // 
            // dgvDetalle
            // 
            this.dgvDetalle.AllowUserToAddRows = false;
            this.dgvDetalle.AllowUserToDeleteRows = false;
            this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalle.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Detalle_Modelo,
            this.Detalle_Modelo_Descripcion,
            this.Detalle_Tipo,
            this.Detalle_Modelo_Cantidad,
            this.Detalle_Componente,
            this.Detalle_Componente_Descripcion,
            this.Detalle_Componente_Total});
            this.dgvDetalle.Location = new System.Drawing.Point(17, 59);
            this.dgvDetalle.Name = "dgvDetalle";
            this.dgvDetalle.ReadOnly = true;
            this.dgvDetalle.RowTemplate.Height = 24;
            this.dgvDetalle.Size = new System.Drawing.Size(1519, 256);
            this.dgvDetalle.TabIndex = 1;
            // 
            // Detalle_Modelo
            // 
            this.Detalle_Modelo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Detalle_Modelo.DataPropertyName = "MODELO";
            this.Detalle_Modelo.HeaderText = "Modelo";
            this.Detalle_Modelo.Name = "Detalle_Modelo";
            this.Detalle_Modelo.ReadOnly = true;
            this.Detalle_Modelo.Width = 79;
            // 
            // Detalle_Modelo_Descripcion
            // 
            this.Detalle_Modelo_Descripcion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Detalle_Modelo_Descripcion.DataPropertyName = "MODELO_DESCRIPCION";
            this.Detalle_Modelo_Descripcion.HeaderText = "Descripción";
            this.Detalle_Modelo_Descripcion.Name = "Detalle_Modelo_Descripcion";
            this.Detalle_Modelo_Descripcion.ReadOnly = true;
            this.Detalle_Modelo_Descripcion.Width = 107;
            // 
            // Detalle_Tipo
            // 
            this.Detalle_Tipo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Detalle_Tipo.DataPropertyName = "MODELO_TIPO";
            this.Detalle_Tipo.HeaderText = "Tipo";
            this.Detalle_Tipo.Name = "Detalle_Tipo";
            this.Detalle_Tipo.ReadOnly = true;
            this.Detalle_Tipo.Width = 61;
            // 
            // Detalle_Modelo_Cantidad
            // 
            this.Detalle_Modelo_Cantidad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Detalle_Modelo_Cantidad.DataPropertyName = "MODELO_CANTIDAD";
            this.Detalle_Modelo_Cantidad.HeaderText = "Cantidad";
            this.Detalle_Modelo_Cantidad.Name = "Detalle_Modelo_Cantidad";
            this.Detalle_Modelo_Cantidad.ReadOnly = true;
            this.Detalle_Modelo_Cantidad.Width = 89;
            // 
            // Detalle_Componente
            // 
            this.Detalle_Componente.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Detalle_Componente.DataPropertyName = "COMPONENTE";
            this.Detalle_Componente.HeaderText = "Componente";
            this.Detalle_Componente.Name = "Detalle_Componente";
            this.Detalle_Componente.ReadOnly = true;
            this.Detalle_Componente.Width = 113;
            // 
            // Detalle_Componente_Descripcion
            // 
            this.Detalle_Componente_Descripcion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Detalle_Componente_Descripcion.DataPropertyName = "COMPONENTE_DESCRIPCION";
            this.Detalle_Componente_Descripcion.HeaderText = "Descripción";
            this.Detalle_Componente_Descripcion.Name = "Detalle_Componente_Descripcion";
            this.Detalle_Componente_Descripcion.ReadOnly = true;
            this.Detalle_Componente_Descripcion.Width = 107;
            // 
            // Detalle_Componente_Total
            // 
            this.Detalle_Componente_Total.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Detalle_Componente_Total.DataPropertyName = "COMPONENTE_TOTAL";
            this.Detalle_Componente_Total.HeaderText = "Total";
            this.Detalle_Componente_Total.Name = "Detalle_Componente_Total";
            this.Detalle_Componente_Total.ReadOnly = true;
            this.Detalle_Componente_Total.Width = 65;
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitulo.Location = new System.Drawing.Point(515, 9);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(111, 29);
            this.lblSubtitulo.TabIndex = 2;
            this.lblSubtitulo.Text = "#Pedido";
            // 
            // lblNumeroPedido
            // 
            this.lblNumeroPedido.AutoSize = true;
            this.lblNumeroPedido.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumeroPedido.Location = new System.Drawing.Point(639, 9);
            this.lblNumeroPedido.Name = "lblNumeroPedido";
            this.lblNumeroPedido.Size = new System.Drawing.Size(97, 29);
            this.lblNumeroPedido.TabIndex = 3;
            this.lblNumeroPedido.Text = "000000";
            // 
            // btnImprimir
            // 
            this.btnImprimir.Location = new System.Drawing.Point(1461, 588);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(75, 36);
            this.btnImprimir.TabIndex = 4;
            this.btnImprimir.Text = "Generar";
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // dgvMP
            // 
            this.dgvMP.AllowUserToAddRows = false;
            this.dgvMP.AllowUserToDeleteRows = false;
            this.dgvMP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMP.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MP_Seleccionar,
            this.MP_Componente,
            this.MP_Descripcion,
            this.MP_Unidad,
            this.MP_Total,
            this.MP_Existencia});
            this.dgvMP.Location = new System.Drawing.Point(12, 351);
            this.dgvMP.Name = "dgvMP";
            this.dgvMP.RowTemplate.Height = 24;
            this.dgvMP.Size = new System.Drawing.Size(861, 231);
            this.dgvMP.TabIndex = 5;
            // 
            // lblMP
            // 
            this.lblMP.AutoSize = true;
            this.lblMP.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMP.Location = new System.Drawing.Point(13, 328);
            this.lblMP.Name = "lblMP";
            this.lblMP.Size = new System.Drawing.Size(135, 20);
            this.lblMP.TabIndex = 7;
            this.lblMP.Text = "Detalle por MP";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(952, 328);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Detalle por modelo";
            // 
            // dgvComprado
            // 
            this.dgvComprado.AllowUserToAddRows = false;
            this.dgvComprado.AllowUserToDeleteRows = false;
            this.dgvComprado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvComprado.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Comprado_Seleccionar,
            this.Comprado_Modelo,
            this.Comprado_Descripcion,
            this.Comprado_Total});
            this.dgvComprado.Location = new System.Drawing.Point(879, 351);
            this.dgvComprado.Name = "dgvComprado";
            this.dgvComprado.RowTemplate.Height = 24;
            this.dgvComprado.Size = new System.Drawing.Size(657, 231);
            this.dgvComprado.TabIndex = 9;
            // 
            // Comprado_Seleccionar
            // 
            this.Comprado_Seleccionar.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Comprado_Seleccionar.HeaderText = "Seleccionar";
            this.Comprado_Seleccionar.Name = "Comprado_Seleccionar";
            this.Comprado_Seleccionar.Width = 88;
            // 
            // Comprado_Modelo
            // 
            this.Comprado_Modelo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Comprado_Modelo.DataPropertyName = "MODELO";
            this.Comprado_Modelo.HeaderText = "Modelo";
            this.Comprado_Modelo.Name = "Comprado_Modelo";
            this.Comprado_Modelo.ReadOnly = true;
            this.Comprado_Modelo.Width = 79;
            // 
            // Comprado_Descripcion
            // 
            this.Comprado_Descripcion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Comprado_Descripcion.DataPropertyName = "DESCRIPCION";
            this.Comprado_Descripcion.HeaderText = "Descripción";
            this.Comprado_Descripcion.Name = "Comprado_Descripcion";
            this.Comprado_Descripcion.ReadOnly = true;
            this.Comprado_Descripcion.Width = 107;
            // 
            // Comprado_Total
            // 
            this.Comprado_Total.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Comprado_Total.DataPropertyName = "TOTAL";
            this.Comprado_Total.HeaderText = "Total";
            this.Comprado_Total.Name = "Comprado_Total";
            this.Comprado_Total.ReadOnly = true;
            this.Comprado_Total.Width = 65;
            // 
            // MP_Seleccionar
            // 
            this.MP_Seleccionar.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.MP_Seleccionar.HeaderText = "Seleccionar";
            this.MP_Seleccionar.Name = "MP_Seleccionar";
            this.MP_Seleccionar.Width = 88;
            // 
            // MP_Componente
            // 
            this.MP_Componente.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.MP_Componente.DataPropertyName = "COMPONENTE";
            this.MP_Componente.HeaderText = "Componente";
            this.MP_Componente.Name = "MP_Componente";
            this.MP_Componente.ReadOnly = true;
            this.MP_Componente.Width = 113;
            // 
            // MP_Descripcion
            // 
            this.MP_Descripcion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.MP_Descripcion.DataPropertyName = "DESCRIPCION";
            this.MP_Descripcion.HeaderText = "Descripción";
            this.MP_Descripcion.Name = "MP_Descripcion";
            this.MP_Descripcion.ReadOnly = true;
            this.MP_Descripcion.Width = 107;
            // 
            // MP_Unidad
            // 
            this.MP_Unidad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.MP_Unidad.DataPropertyName = "UNIDAD";
            this.MP_Unidad.HeaderText = "Unidad";
            this.MP_Unidad.Name = "MP_Unidad";
            this.MP_Unidad.ReadOnly = true;
            this.MP_Unidad.Width = 78;
            // 
            // MP_Total
            // 
            this.MP_Total.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.MP_Total.DataPropertyName = "TOTAL";
            this.MP_Total.HeaderText = "Total";
            this.MP_Total.Name = "MP_Total";
            this.MP_Total.ReadOnly = true;
            this.MP_Total.Width = 65;
            // 
            // MP_Existencia
            // 
            this.MP_Existencia.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.MP_Existencia.DataPropertyName = "EXISTENCIA";
            this.MP_Existencia.HeaderText = "Existencia";
            this.MP_Existencia.Name = "MP_Existencia";
            this.MP_Existencia.Width = 96;
            // 
            // frmRequisicionPedido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1548, 636);
            this.Controls.Add(this.dgvComprado);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblMP);
            this.Controls.Add(this.dgvMP);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.lblNumeroPedido);
            this.Controls.Add(this.lblSubtitulo);
            this.Controls.Add(this.dgvDetalle);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmRequisicionPedido";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Requisición de compra";
            this.Load += new System.EventHandler(this.frmRequisicionPedido_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComprado)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.DataGridView dgvDetalle;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblNumeroPedido;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.DataGridView dgvMP;
        private System.Windows.Forms.Label lblMP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvComprado;
        private System.Windows.Forms.DataGridViewTextBoxColumn Detalle_Modelo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Detalle_Modelo_Descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Detalle_Tipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Detalle_Modelo_Cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn Detalle_Componente;
        private System.Windows.Forms.DataGridViewTextBoxColumn Detalle_Componente_Descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Detalle_Componente_Total;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Comprado_Seleccionar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comprado_Modelo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comprado_Descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comprado_Total;
        private System.Windows.Forms.DataGridViewCheckBoxColumn MP_Seleccionar;
        private System.Windows.Forms.DataGridViewTextBoxColumn MP_Componente;
        private System.Windows.Forms.DataGridViewTextBoxColumn MP_Descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn MP_Unidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn MP_Total;
        private System.Windows.Forms.DataGridViewTextBoxColumn MP_Existencia;
    }
}