namespace SIP
{
    partial class frmRetroAlimManObraPedido
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
            this.txtPedido = new System.Windows.Forms.TextBox();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblPedido = new System.Windows.Forms.Label();
            this.dgvProcesos = new System.Windows.Forms.DataGridView();
            this.lblProcesos = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.CMT_CHECK = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.CMT_PROCESADO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CMT_INDX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CMT_CMMT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CMT_PROCESO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CMT_RUTA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CMT_CANTIDAD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CMT_DEPARTAMENTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProcesos)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPedido
            // 
            this.txtPedido.Location = new System.Drawing.Point(172, 62);
            this.txtPedido.Name = "txtPedido";
            this.txtPedido.Size = new System.Drawing.Size(131, 20);
            this.txtPedido.TabIndex = 0;
            this.txtPedido.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPedido_KeyPress);
            this.txtPedido.Leave += new System.EventHandler(this.txtPedido_Leave);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(266, 9);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(348, 24);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "Retroalimentación de Mano de Obra";
            // 
            // lblPedido
            // 
            this.lblPedido.AutoSize = true;
            this.lblPedido.Location = new System.Drawing.Point(12, 65);
            this.lblPedido.Name = "lblPedido";
            this.lblPedido.Size = new System.Drawing.Size(154, 13);
            this.lblPedido.TabIndex = 2;
            this.lblPedido.Text = "Introduce el número de pedido ";
            // 
            // dgvProcesos
            // 
            this.dgvProcesos.AllowUserToAddRows = false;
            this.dgvProcesos.AllowUserToDeleteRows = false;
            this.dgvProcesos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProcesos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CMT_CHECK,
            this.CMT_PROCESADO,
            this.CMT_INDX,
            this.CMT_CMMT,
            this.CMT_PROCESO,
            this.CMT_RUTA,
            this.CMT_CANTIDAD,
            this.CMT_DEPARTAMENTO});
            this.dgvProcesos.Location = new System.Drawing.Point(12, 126);
            this.dgvProcesos.Name = "dgvProcesos";
            this.dgvProcesos.Size = new System.Drawing.Size(800, 265);
            this.dgvProcesos.TabIndex = 3;
            // 
            // lblProcesos
            // 
            this.lblProcesos.AutoSize = true;
            this.lblProcesos.Location = new System.Drawing.Point(12, 110);
            this.lblProcesos.Name = "lblProcesos";
            this.lblProcesos.Size = new System.Drawing.Size(102, 13);
            this.lblProcesos.TabIndex = 4;
            this.lblProcesos.Text = "Detalle de Procesos";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(737, 396);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 5;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // CMT_CHECK
            // 
            this.CMT_CHECK.DataPropertyName = "CMT_CHECK";
            this.CMT_CHECK.HeaderText = "LIBERADO";
            this.CMT_CHECK.Name = "CMT_CHECK";
            // 
            // CMT_PROCESADO
            // 
            this.CMT_PROCESADO.DataPropertyName = "CMT_PROCESADO";
            this.CMT_PROCESADO.HeaderText = "PROCESADO";
            this.CMT_PROCESADO.Name = "CMT_PROCESADO";
            this.CMT_PROCESADO.ReadOnly = true;
            this.CMT_PROCESADO.Visible = false;
            // 
            // CMT_INDX
            // 
            this.CMT_INDX.DataPropertyName = "CMT_INDX";
            this.CMT_INDX.HeaderText = "CMT_INDX";
            this.CMT_INDX.Name = "CMT_INDX";
            this.CMT_INDX.ReadOnly = true;
            // 
            // CMT_CMMT
            // 
            this.CMT_CMMT.DataPropertyName = "CMT_CMMT";
            this.CMT_CMMT.HeaderText = "DESCRIPCIÓN";
            this.CMT_CMMT.Name = "CMT_CMMT";
            this.CMT_CMMT.ReadOnly = true;
            // 
            // CMT_PROCESO
            // 
            this.CMT_PROCESO.DataPropertyName = "CMT_PROCESO";
            this.CMT_PROCESO.HeaderText = "PROCESO";
            this.CMT_PROCESO.Name = "CMT_PROCESO";
            this.CMT_PROCESO.ReadOnly = true;
            // 
            // CMT_RUTA
            // 
            this.CMT_RUTA.DataPropertyName = "CMT_RUTA";
            this.CMT_RUTA.HeaderText = "RUTA";
            this.CMT_RUTA.Name = "CMT_RUTA";
            this.CMT_RUTA.ReadOnly = true;
            // 
            // CMT_CANTIDAD
            // 
            this.CMT_CANTIDAD.DataPropertyName = "CMT_CANTIDAD";
            this.CMT_CANTIDAD.HeaderText = "CANTIDAD";
            this.CMT_CANTIDAD.Name = "CMT_CANTIDAD";
            this.CMT_CANTIDAD.ReadOnly = true;
            // 
            // CMT_DEPARTAMENTO
            // 
            this.CMT_DEPARTAMENTO.DataPropertyName = "CMT_DEPARTAMENTO";
            this.CMT_DEPARTAMENTO.HeaderText = "DEPARTAMENTO";
            this.CMT_DEPARTAMENTO.Name = "CMT_DEPARTAMENTO";
            this.CMT_DEPARTAMENTO.ReadOnly = true;
            // 
            // frmRetroAlimManObraPedido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 431);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.lblProcesos);
            this.Controls.Add(this.dgvProcesos);
            this.Controls.Add(this.lblPedido);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.txtPedido);
            this.Name = "frmRetroAlimManObraPedido";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Retroalimetacion de Mano de Obra por Pedido";
            ((System.ComponentModel.ISupportInitialize)(this.dgvProcesos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPedido;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblPedido;
        private System.Windows.Forms.DataGridView dgvProcesos;
        private System.Windows.Forms.Label lblProcesos;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CMT_CHECK;
        private System.Windows.Forms.DataGridViewTextBoxColumn CMT_PROCESADO;
        private System.Windows.Forms.DataGridViewTextBoxColumn CMT_INDX;
        private System.Windows.Forms.DataGridViewTextBoxColumn CMT_CMMT;
        private System.Windows.Forms.DataGridViewTextBoxColumn CMT_PROCESO;
        private System.Windows.Forms.DataGridViewTextBoxColumn CMT_RUTA;
        private System.Windows.Forms.DataGridViewTextBoxColumn CMT_CANTIDAD;
        private System.Windows.Forms.DataGridViewTextBoxColumn CMT_DEPARTAMENTO;
    }
}