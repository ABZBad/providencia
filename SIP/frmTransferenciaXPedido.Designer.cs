namespace SIP
{
    partial class frmTransferenciaXPedido
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
            this.gBoxDocumento = new System.Windows.Forms.GroupBox();
            this.lblCliente = new System.Windows.Forms.Label();
            this.txtAlmDestino = new SIP.UserControls.NumericTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAlmOrigen = new SIP.UserControls.NumericTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPedido = new SIP.UserControls.NumericTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.gBoxDetalle = new System.Windows.Forms.GroupBox();
            this.dgViewDetalle = new System.Windows.Forms.DataGridView();
            this.CModelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LINEA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CANT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.origen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EXIST = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.destino = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnProcesar = new System.Windows.Forms.Button();
            this.gBoxDocumento.SuspendLayout();
            this.gBoxDetalle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewDetalle)).BeginInit();
            this.SuspendLayout();
            // 
            // gBoxDocumento
            // 
            this.gBoxDocumento.Controls.Add(this.lblCliente);
            this.gBoxDocumento.Controls.Add(this.txtAlmDestino);
            this.gBoxDocumento.Controls.Add(this.label3);
            this.gBoxDocumento.Controls.Add(this.txtAlmOrigen);
            this.gBoxDocumento.Controls.Add(this.label2);
            this.gBoxDocumento.Controls.Add(this.txtPedido);
            this.gBoxDocumento.Controls.Add(this.label1);
            this.gBoxDocumento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gBoxDocumento.Location = new System.Drawing.Point(12, 12);
            this.gBoxDocumento.Name = "gBoxDocumento";
            this.gBoxDocumento.Size = new System.Drawing.Size(501, 86);
            this.gBoxDocumento.TabIndex = 0;
            this.gBoxDocumento.TabStop = false;
            this.gBoxDocumento.Text = "Documento";
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.Location = new System.Drawing.Point(20, 50);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(45, 15);
            this.lblCliente.TabIndex = 6;
            this.lblCliente.Text = "Cliente";
            // 
            // txtAlmDestino
            // 
            this.txtAlmDestino.Location = new System.Drawing.Point(398, 18);
            this.txtAlmDestino.MaxValue = 0;
            this.txtAlmDestino.MinValue = 0;
            this.txtAlmDestino.Name = "txtAlmDestino";
            this.txtAlmDestino.Size = new System.Drawing.Size(32, 21);
            this.txtAlmDestino.TabIndex = 5;
            this.txtAlmDestino.Text = "5";
            this.txtAlmDestino.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(316, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Alm. Destino";
            this.label3.Visible = false;
            // 
            // txtAlmOrigen
            // 
            this.txtAlmOrigen.Location = new System.Drawing.Point(267, 18);
            this.txtAlmOrigen.MaxValue = 0;
            this.txtAlmOrigen.MinValue = 0;
            this.txtAlmOrigen.Name = "txtAlmOrigen";
            this.txtAlmOrigen.Size = new System.Drawing.Size(32, 21);
            this.txtAlmOrigen.TabIndex = 3;
            this.txtAlmOrigen.Text = "3";
            this.txtAlmOrigen.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(189, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Alm. Origen";
            this.label2.Visible = false;
            // 
            // txtPedido
            // 
            this.txtPedido.Location = new System.Drawing.Point(69, 18);
            this.txtPedido.MaxValue = 0;
            this.txtPedido.MinValue = 0;
            this.txtPedido.Name = "txtPedido";
            this.txtPedido.Size = new System.Drawing.Size(100, 21);
            this.txtPedido.TabIndex = 1;
            this.txtPedido.TextChanged += new System.EventHandler(this.txtPedido_TextChanged);
            this.txtPedido.Leave += new System.EventHandler(this.txtPedido_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pedido";
            // 
            // btnConsultar
            // 
            this.btnConsultar.Location = new System.Drawing.Point(438, 112);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(75, 23);
            this.btnConsultar.TabIndex = 1;
            this.btnConsultar.Text = "Consultar";
            this.btnConsultar.UseVisualStyleBackColor = true;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // gBoxDetalle
            // 
            this.gBoxDetalle.Controls.Add(this.dgViewDetalle);
            this.gBoxDetalle.Location = new System.Drawing.Point(12, 141);
            this.gBoxDetalle.Name = "gBoxDetalle";
            this.gBoxDetalle.Size = new System.Drawing.Size(501, 248);
            this.gBoxDetalle.TabIndex = 2;
            this.gBoxDetalle.TabStop = false;
            this.gBoxDetalle.Text = "Detalle";
            // 
            // dgViewDetalle
            // 
            this.dgViewDetalle.AllowUserToAddRows = false;
            this.dgViewDetalle.AllowUserToResizeColumns = false;
            this.dgViewDetalle.AllowUserToResizeRows = false;
            this.dgViewDetalle.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgViewDetalle.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dgViewDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgViewDetalle.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CModelo,
            this.LINEA,
            this.CANT,
            this.origen,
            this.EXIST,
            this.destino,
            this.Status});
            this.dgViewDetalle.Location = new System.Drawing.Point(13, 19);
            this.dgViewDetalle.Name = "dgViewDetalle";
            this.dgViewDetalle.ReadOnly = true;
            this.dgViewDetalle.RowHeadersVisible = false;
            this.dgViewDetalle.Size = new System.Drawing.Size(474, 215);
            this.dgViewDetalle.TabIndex = 0;
            // 
            // CModelo
            // 
            this.CModelo.DataPropertyName = "CVE_ART";
            this.CModelo.HeaderText = "Modelo";
            this.CModelo.Name = "CModelo";
            this.CModelo.ReadOnly = true;
            this.CModelo.Width = 110;
            // 
            // LINEA
            // 
            this.LINEA.DataPropertyName = "LINEA";
            this.LINEA.HeaderText = "Línea";
            this.LINEA.Name = "LINEA";
            this.LINEA.ReadOnly = true;
            this.LINEA.Width = 50;
            // 
            // CANT
            // 
            this.CANT.DataPropertyName = "CANT";
            this.CANT.HeaderText = "Cant.";
            this.CANT.Name = "CANT";
            this.CANT.ReadOnly = true;
            this.CANT.Width = 45;
            // 
            // origen
            // 
            this.origen.DataPropertyName = "origen";
            this.origen.HeaderText = "Orig.";
            this.origen.Name = "origen";
            this.origen.ReadOnly = true;
            this.origen.Width = 45;
            // 
            // EXIST
            // 
            this.EXIST.DataPropertyName = "EXIST";
            this.EXIST.HeaderText = "Exist.";
            this.EXIST.Name = "EXIST";
            this.EXIST.ReadOnly = true;
            this.EXIST.Width = 45;
            // 
            // destino
            // 
            this.destino.DataPropertyName = "destino";
            this.destino.HeaderText = "Dest.";
            this.destino.Name = "destino";
            this.destino.ReadOnly = true;
            this.destino.Width = 45;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            this.Status.HeaderText = "Estatus";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 130;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(15, 405);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(37, 13);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Status";
            // 
            // btnProcesar
            // 
            this.btnProcesar.Enabled = false;
            this.btnProcesar.Location = new System.Drawing.Point(438, 395);
            this.btnProcesar.Name = "btnProcesar";
            this.btnProcesar.Size = new System.Drawing.Size(75, 23);
            this.btnProcesar.TabIndex = 4;
            this.btnProcesar.Text = "Procesar";
            this.btnProcesar.UseVisualStyleBackColor = true;
            this.btnProcesar.Click += new System.EventHandler(this.btnProcesar_Click);
            // 
            // frmTransferenciaXPedido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 439);
            this.Controls.Add(this.btnProcesar);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.gBoxDetalle);
            this.Controls.Add(this.btnConsultar);
            this.Controls.Add(this.gBoxDocumento);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTransferenciaXPedido";
            this.Text = "Tranferencia por pedido";
            this.Load += new System.EventHandler(this.frmTransferenciaXPedido_Load);
            this.gBoxDocumento.ResumeLayout(false);
            this.gBoxDocumento.PerformLayout();
            this.gBoxDetalle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgViewDetalle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gBoxDocumento;
        private System.Windows.Forms.Label lblCliente;
        private UserControls.NumericTextBox txtAlmDestino;
        private System.Windows.Forms.Label label3;
        private UserControls.NumericTextBox txtAlmOrigen;
        private System.Windows.Forms.Label label2;
        private UserControls.NumericTextBox txtPedido;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.GroupBox gBoxDetalle;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnProcesar;
        private System.Windows.Forms.DataGridView dgViewDetalle;
        private System.Windows.Forms.DataGridViewTextBoxColumn CModelo;
        private System.Windows.Forms.DataGridViewTextBoxColumn LINEA;
        private System.Windows.Forms.DataGridViewTextBoxColumn CANT;
        private System.Windows.Forms.DataGridViewTextBoxColumn origen;
        private System.Windows.Forms.DataGridViewTextBoxColumn EXIST;
        private System.Windows.Forms.DataGridViewTextBoxColumn destino;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
    }
}