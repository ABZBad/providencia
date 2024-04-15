namespace SIP
{
    partial class frmRequisiciones
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
            this.dgvRequisiciones = new System.Windows.Forms.DataGridView();
            this.btnNotificarRecepcion = new System.Windows.Forms.Button();
            this.GV_Seleccionar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.GV_Pedido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GV_Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GV_Tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GV_TipoDescripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GV_Ver = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRequisiciones)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(12, 9);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(179, 29);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Requisiciones";
            // 
            // dgvRequisiciones
            // 
            this.dgvRequisiciones.AllowUserToAddRows = false;
            this.dgvRequisiciones.AllowUserToDeleteRows = false;
            this.dgvRequisiciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRequisiciones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GV_Seleccionar,
            this.GV_Pedido,
            this.GV_Fecha,
            this.GV_Tipo,
            this.GV_TipoDescripcion,
            this.GV_Ver});
            this.dgvRequisiciones.Location = new System.Drawing.Point(17, 41);
            this.dgvRequisiciones.Name = "dgvRequisiciones";
            this.dgvRequisiciones.RowTemplate.Height = 24;
            this.dgvRequisiciones.Size = new System.Drawing.Size(810, 277);
            this.dgvRequisiciones.TabIndex = 1;
            this.dgvRequisiciones.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRequisiciones_CellClick);
            // 
            // btnNotificarRecepcion
            // 
            this.btnNotificarRecepcion.Location = new System.Drawing.Point(678, 324);
            this.btnNotificarRecepcion.Name = "btnNotificarRecepcion";
            this.btnNotificarRecepcion.Size = new System.Drawing.Size(149, 34);
            this.btnNotificarRecepcion.TabIndex = 2;
            this.btnNotificarRecepcion.Text = "Notificar Recepción";
            this.btnNotificarRecepcion.UseVisualStyleBackColor = true;
            this.btnNotificarRecepcion.Click += new System.EventHandler(this.btnNotificarRecepcion_Click);
            // 
            // GV_Seleccionar
            // 
            this.GV_Seleccionar.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.GV_Seleccionar.HeaderText = "Seleccionar";
            this.GV_Seleccionar.Name = "GV_Seleccionar";
            this.GV_Seleccionar.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.GV_Seleccionar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.GV_Seleccionar.Width = 107;
            // 
            // GV_Pedido
            // 
            this.GV_Pedido.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.GV_Pedido.DataPropertyName = "Pedido";
            this.GV_Pedido.HeaderText = "Pedido";
            this.GV_Pedido.Name = "GV_Pedido";
            this.GV_Pedido.ReadOnly = true;
            this.GV_Pedido.Width = 77;
            // 
            // GV_Fecha
            // 
            this.GV_Fecha.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.GV_Fecha.DataPropertyName = "FechaCreacion";
            this.GV_Fecha.HeaderText = "Fecha Requisición";
            this.GV_Fecha.Name = "GV_Fecha";
            this.GV_Fecha.ReadOnly = true;
            this.GV_Fecha.Width = 136;
            // 
            // GV_Tipo
            // 
            this.GV_Tipo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.GV_Tipo.DataPropertyName = "Tipo";
            this.GV_Tipo.HeaderText = "Tipo";
            this.GV_Tipo.Name = "GV_Tipo";
            this.GV_Tipo.ReadOnly = true;
            this.GV_Tipo.Visible = false;
            this.GV_Tipo.Width = 61;
            // 
            // GV_TipoDescripcion
            // 
            this.GV_TipoDescripcion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.GV_TipoDescripcion.DataPropertyName = "TipoDescripcion";
            this.GV_TipoDescripcion.HeaderText = "Tipo";
            this.GV_TipoDescripcion.Name = "GV_TipoDescripcion";
            this.GV_TipoDescripcion.ReadOnly = true;
            this.GV_TipoDescripcion.Width = 61;
            // 
            // GV_Ver
            // 
            this.GV_Ver.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.GV_Ver.HeaderText = "Ver";
            this.GV_Ver.Name = "GV_Ver";
            this.GV_Ver.ReadOnly = true;
            this.GV_Ver.Text = "Ver...";
            this.GV_Ver.UseColumnTextForButtonValue = true;
            this.GV_Ver.Width = 36;
            // 
            // frmRequisiciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 370);
            this.Controls.Add(this.btnNotificarRecepcion);
            this.Controls.Add(this.dgvRequisiciones);
            this.Controls.Add(this.lblTitulo);
            this.Name = "frmRequisiciones";
            this.Text = "Requisiciones Pendientes";
            this.Load += new System.EventHandler(this.frmRequisiciones_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRequisiciones)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.DataGridView dgvRequisiciones;
        private System.Windows.Forms.Button btnNotificarRecepcion;
        private System.Windows.Forms.DataGridViewCheckBoxColumn GV_Seleccionar;
        private System.Windows.Forms.DataGridViewTextBoxColumn GV_Pedido;
        private System.Windows.Forms.DataGridViewTextBoxColumn GV_Fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn GV_Tipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn GV_TipoDescripcion;
        private System.Windows.Forms.DataGridViewButtonColumn GV_Ver;
    }
}