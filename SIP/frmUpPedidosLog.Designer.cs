namespace SIP
{
    partial class frmUpPedidosLog
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.dgvLogUpPedidos = new System.Windows.Forms.DataGridView();
            this.colTransaccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPantalla = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCampo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValorAnterior = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValorNuevo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFechaModificacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colModificadoPor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsuarioWin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaquina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtNumeroPedido = new SIP.UserControls.NumericTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogUpPedidos)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "No. de Pedido:";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(168, 6);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(55, 23);
            this.btnBuscar.TabIndex = 1;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // dgvLogUpPedidos
            // 
            this.dgvLogUpPedidos.AllowUserToAddRows = false;
            this.dgvLogUpPedidos.AllowUserToDeleteRows = false;
            this.dgvLogUpPedidos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLogUpPedidos.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvLogUpPedidos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvLogUpPedidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLogUpPedidos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTransaccion,
            this.colAccion,
            this.colPantalla,
            this.colCampo,
            this.colValorAnterior,
            this.colValorNuevo,
            this.colFechaModificacion,
            this.colModificadoPor,
            this.colUsuarioWin,
            this.colMaquina});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLogUpPedidos.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvLogUpPedidos.Location = new System.Drawing.Point(15, 35);
            this.dgvLogUpPedidos.Name = "dgvLogUpPedidos";
            this.dgvLogUpPedidos.ReadOnly = true;
            this.dgvLogUpPedidos.RowHeadersVisible = false;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(183)))), ((int)(((byte)(142)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.White;
            this.dgvLogUpPedidos.RowsDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvLogUpPedidos.RowTemplate.Height = 28;
            this.dgvLogUpPedidos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLogUpPedidos.Size = new System.Drawing.Size(1037, 418);
            this.dgvLogUpPedidos.TabIndex = 3;
            this.dgvLogUpPedidos.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvLogUpPedidos_CellFormatting);
            // 
            // colTransaccion
            // 
            this.colTransaccion.DataPropertyName = "Transaccion";
            this.colTransaccion.HeaderText = "# Tran.";
            this.colTransaccion.Name = "colTransaccion";
            this.colTransaccion.ReadOnly = true;
            this.colTransaccion.Width = 35;
            // 
            // colAccion
            // 
            this.colAccion.DataPropertyName = "Accion";
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colAccion.DefaultCellStyle = dataGridViewCellStyle1;
            this.colAccion.HeaderText = "Acción";
            this.colAccion.Name = "colAccion";
            this.colAccion.ReadOnly = true;
            // 
            // colPantalla
            // 
            this.colPantalla.DataPropertyName = "Pantalla";
            this.colPantalla.HeaderText = "Pantalla";
            this.colPantalla.Name = "colPantalla";
            this.colPantalla.ReadOnly = true;
            this.colPantalla.Width = 180;
            // 
            // colCampo
            // 
            this.colCampo.DataPropertyName = "Campo";
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colCampo.DefaultCellStyle = dataGridViewCellStyle2;
            this.colCampo.HeaderText = "Campo";
            this.colCampo.Name = "colCampo";
            this.colCampo.ReadOnly = true;
            // 
            // colValorAnterior
            // 
            this.colValorAnterior.DataPropertyName = "ValorAnterior";
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.colValorAnterior.DefaultCellStyle = dataGridViewCellStyle3;
            this.colValorAnterior.HeaderText = "Valor anterior";
            this.colValorAnterior.Name = "colValorAnterior";
            this.colValorAnterior.ReadOnly = true;
            this.colValorAnterior.Width = 140;
            // 
            // colValorNuevo
            // 
            this.colValorNuevo.DataPropertyName = "ValorNuevo";
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.colValorNuevo.DefaultCellStyle = dataGridViewCellStyle4;
            this.colValorNuevo.HeaderText = "Valor nuevo";
            this.colValorNuevo.Name = "colValorNuevo";
            this.colValorNuevo.ReadOnly = true;
            this.colValorNuevo.Width = 140;
            // 
            // colFechaModificacion
            // 
            this.colFechaModificacion.DataPropertyName = "FechaModificacion";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colFechaModificacion.DefaultCellStyle = dataGridViewCellStyle5;
            this.colFechaModificacion.HeaderText = "Modificado el";
            this.colFechaModificacion.Name = "colFechaModificacion";
            this.colFechaModificacion.ReadOnly = true;
            this.colFechaModificacion.Width = 120;
            // 
            // colModificadoPor
            // 
            this.colModificadoPor.DataPropertyName = "ModificadoPor";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colModificadoPor.DefaultCellStyle = dataGridViewCellStyle6;
            this.colModificadoPor.HeaderText = "Modificado por";
            this.colModificadoPor.Name = "colModificadoPor";
            this.colModificadoPor.ReadOnly = true;
            this.colModificadoPor.Width = 55;
            // 
            // colUsuarioWin
            // 
            this.colUsuarioWin.DataPropertyName = "UsuarioWin";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colUsuarioWin.DefaultCellStyle = dataGridViewCellStyle7;
            this.colUsuarioWin.HeaderText = "Usuario Windows";
            this.colUsuarioWin.Name = "colUsuarioWin";
            this.colUsuarioWin.ReadOnly = true;
            this.colUsuarioWin.Width = 55;
            // 
            // colMaquina
            // 
            this.colMaquina.DataPropertyName = "Maquina";
            this.colMaquina.HeaderText = "Máquina";
            this.colMaquina.Name = "colMaquina";
            this.colMaquina.ReadOnly = true;
            this.colMaquina.Width = 90;
            // 
            // txtNumeroPedido
            // 
            this.txtNumeroPedido.Location = new System.Drawing.Point(96, 6);
            this.txtNumeroPedido.MaxValue = 0;
            this.txtNumeroPedido.MinValue = 0;
            this.txtNumeroPedido.Name = "txtNumeroPedido";
            this.txtNumeroPedido.Size = new System.Drawing.Size(68, 20);
            this.txtNumeroPedido.TabIndex = 0;
            this.txtNumeroPedido.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNumeroPedido_KeyDown);
            // 
            // frmUpPedidosLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 499);
            this.Controls.Add(this.dgvLogUpPedidos);
            this.Controls.Add(this.txtNumeroPedido);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.label1);
            this.Name = "frmUpPedidosLog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Log de Cambios";
            this.Load += new System.EventHandler(this.frmUpPedidosLog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogUpPedidos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBuscar;
        private UserControls.NumericTextBox txtNumeroPedido;
        private System.Windows.Forms.DataGridView dgvLogUpPedidos;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTransaccion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAccion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPantalla;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCampo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValorAnterior;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValorNuevo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFechaModificacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colModificadoPor;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsuarioWin;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaquina;
    }
}