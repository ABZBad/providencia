namespace SIP
{
    partial class frmAsignarRuta
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvMaestro = new System.Windows.Forms.DataGridView();
            this.dgvDetalle = new System.Windows.Forms.DataGridView();
            this.ColDetINDX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetPedido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetModelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetLogo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetRuta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetProceso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetDepto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetOrden = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnLiberar = new System.Windows.Forms.Button();
            this.colPedido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colModelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRuta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAgrupador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaestro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvMaestro
            // 
            this.dgvMaestro.AllowUserToAddRows = false;
            this.dgvMaestro.AllowUserToDeleteRows = false;
            this.dgvMaestro.AllowUserToResizeColumns = false;
            this.dgvMaestro.AllowUserToResizeRows = false;
            this.dgvMaestro.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvMaestro.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMaestro.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPedido,
            this.colModelo,
            this.colCantidad,
            this.colRuta,
            this.colAgrupador,
            this.colUID});
            this.dgvMaestro.Location = new System.Drawing.Point(12, 11);
            this.dgvMaestro.Name = "dgvMaestro";
            this.dgvMaestro.ReadOnly = true;
            this.dgvMaestro.RowHeadersVisible = false;
            this.dgvMaestro.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMaestro.Size = new System.Drawing.Size(418, 198);
            this.dgvMaestro.TabIndex = 0;
            this.dgvMaestro.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMaestro_RowEnter);
            // 
            // dgvDetalle
            // 
            this.dgvDetalle.AllowUserToAddRows = false;
            this.dgvDetalle.AllowUserToDeleteRows = false;
            this.dgvDetalle.AllowUserToResizeColumns = false;
            this.dgvDetalle.AllowUserToResizeRows = false;
            this.dgvDetalle.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalle.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColDetINDX,
            this.colDetPedido,
            this.colDetModelo,
            this.colDetLogo,
            this.colDetRuta,
            this.colDetProceso,
            this.colDetDepto,
            this.colDetOrden});
            this.dgvDetalle.Location = new System.Drawing.Point(12, 224);
            this.dgvDetalle.Name = "dgvDetalle";
            this.dgvDetalle.RowHeadersVisible = false;
            this.dgvDetalle.Size = new System.Drawing.Size(714, 252);
            this.dgvDetalle.TabIndex = 1;
            this.dgvDetalle.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvDetalle_CellValidating);
            // 
            // ColDetINDX
            // 
            this.ColDetINDX.DataPropertyName = "CMT_INDX";
            this.ColDetINDX.HeaderText = "INDX";
            this.ColDetINDX.Name = "ColDetINDX";
            this.ColDetINDX.ReadOnly = true;
            this.ColDetINDX.Visible = false;
            // 
            // colDetPedido
            // 
            this.colDetPedido.DataPropertyName = "CMT_PEDIDO";
            this.colDetPedido.HeaderText = "Pedido";
            this.colDetPedido.Name = "colDetPedido";
            this.colDetPedido.ReadOnly = true;
            // 
            // colDetModelo
            // 
            this.colDetModelo.DataPropertyName = "CMT_MODELO";
            this.colDetModelo.HeaderText = "Modelo";
            this.colDetModelo.Name = "colDetModelo";
            this.colDetModelo.ReadOnly = true;
            // 
            // colDetLogo
            // 
            this.colDetLogo.DataPropertyName = "CMT_COMO";
            this.colDetLogo.HeaderText = "Logo";
            this.colDetLogo.Name = "colDetLogo";
            this.colDetLogo.ReadOnly = true;
            // 
            // colDetRuta
            // 
            this.colDetRuta.DataPropertyName = "CMT_RUTA";
            this.colDetRuta.HeaderText = "Ruta";
            this.colDetRuta.Name = "colDetRuta";
            this.colDetRuta.ReadOnly = true;
            // 
            // colDetProceso
            // 
            this.colDetProceso.DataPropertyName = "CMT_PROCESO";
            this.colDetProceso.HeaderText = "Proceso";
            this.colDetProceso.Name = "colDetProceso";
            this.colDetProceso.ReadOnly = true;
            // 
            // colDetDepto
            // 
            this.colDetDepto.DataPropertyName = "CMT_DEPARTAMENTO";
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Blue;
            this.colDetDepto.DefaultCellStyle = dataGridViewCellStyle5;
            this.colDetDepto.HeaderText = "Departamento";
            this.colDetDepto.Name = "colDetDepto";
            // 
            // colDetOrden
            // 
            this.colDetOrden.DataPropertyName = "CMT_ORDENAMIENTO";
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Blue;
            this.colDetOrden.DefaultCellStyle = dataGridViewCellStyle6;
            this.colDetOrden.HeaderText = "Orden";
            this.colDetOrden.Name = "colDetOrden";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(443, 51);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(147, 32);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "Buscar órden de procesos";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnLiberar
            // 
            this.btnLiberar.Location = new System.Drawing.Point(443, 95);
            this.btnLiberar.Name = "btnLiberar";
            this.btnLiberar.Size = new System.Drawing.Size(147, 32);
            this.btnLiberar.TabIndex = 3;
            this.btnLiberar.Text = "Liberar órden manualmente";
            this.btnLiberar.UseVisualStyleBackColor = true;
            this.btnLiberar.Click += new System.EventHandler(this.btnLiberar_Click);
            // 
            // colPedido
            // 
            this.colPedido.DataPropertyName = "PEDIDO";
            this.colPedido.HeaderText = "Pedido";
            this.colPedido.Name = "colPedido";
            // 
            // colModelo
            // 
            this.colModelo.DataPropertyName = "MODELO";
            this.colModelo.HeaderText = "Modelo";
            this.colModelo.Name = "colModelo";
            // 
            // colCantidad
            // 
            this.colCantidad.DataPropertyName = "SUMA";
            this.colCantidad.HeaderText = "Cantidad";
            this.colCantidad.Name = "colCantidad";
            // 
            // colRuta
            // 
            this.colRuta.DataPropertyName = "RUTA";
            this.colRuta.HeaderText = "Ruta";
            this.colRuta.Name = "colRuta";
            // 
            // colAgrupador
            // 
            this.colAgrupador.DataPropertyName = "AGRUPADOR";
            this.colAgrupador.HeaderText = "AGRUPADOR";
            this.colAgrupador.Name = "colAgrupador";
            this.colAgrupador.ReadOnly = true;
            this.colAgrupador.Visible = false;
            // 
            // colUID
            // 
            this.colUID.DataPropertyName = "UID";
            this.colUID.HeaderText = "UID";
            this.colUID.Name = "colUID";
            this.colUID.ReadOnly = true;
            this.colUID.Visible = false;
            // 
            // frmAsignarRuta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 481);
            this.Controls.Add(this.btnLiberar);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.dgvDetalle);
            this.Controls.Add(this.dgvMaestro);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAsignarRuta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Asignar Ruta";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAsignarRuta_FormClosing);
            this.Load += new System.EventHandler(this.frmAsignarRuta_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaestro)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMaestro;
        private System.Windows.Forms.DataGridView dgvDetalle;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnLiberar;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDetINDX;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetPedido;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetModelo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetLogo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetRuta;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetProceso;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetDepto;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetOrden;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPedido;
        private System.Windows.Forms.DataGridViewTextBoxColumn colModelo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRuta;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAgrupador;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUID;
    }
}