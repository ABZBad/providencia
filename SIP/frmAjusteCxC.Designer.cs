namespace SIP
{
    partial class frmAjusteCxC
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
            this.scape1 = new SIP.UserControls.Scape();
            this.txtMontoDiferencia = new SIP.UserControls.NumericTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnMostrar = new System.Windows.Forms.Button();
            this.dgSaldos = new System.Windows.Forms.DataGridView();
            this.btnAjustar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblTotalRegistros = new System.Windows.Forms.Label();
            this.lblSeleccionados = new System.Windows.Forms.Label();
            this.btnSeleccionar = new System.Windows.Forms.Button();
            this.btnExportar = new System.Windows.Forms.Button();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SEL = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.CVE_CLIE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NO_FACTURA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REFER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CARGOS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SALDO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID_MOV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgSaldos)).BeginInit();
            this.SuspendLayout();
            // 
            // scape1
            // 
            this.scape1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.scape1.Form = this;
            // 
            // txtMontoDiferencia
            // 
            this.txtMontoDiferencia.Location = new System.Drawing.Point(172, 12);
            this.txtMontoDiferencia.MaxValue = 0;
            this.txtMontoDiferencia.MinValue = 0;
            this.txtMontoDiferencia.Name = "txtMontoDiferencia";
            this.txtMontoDiferencia.NumberType = SIP.UserControls.TipoDeNumero.Decimal;
            this.txtMontoDiferencia.Size = new System.Drawing.Size(41, 20);
            this.txtMontoDiferencia.TabIndex = 0;
            this.txtMontoDiferencia.Text = "15";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Saldos con diferencias ente +/-";
            // 
            // btnMostrar
            // 
            this.btnMostrar.Location = new System.Drawing.Point(219, 10);
            this.btnMostrar.Name = "btnMostrar";
            this.btnMostrar.Size = new System.Drawing.Size(75, 23);
            this.btnMostrar.TabIndex = 1;
            this.btnMostrar.Text = "Mostrar";
            this.btnMostrar.UseVisualStyleBackColor = true;
            this.btnMostrar.Click += new System.EventHandler(this.btnMostrar_Click);
            // 
            // dgSaldos
            // 
            this.dgSaldos.AllowUserToAddRows = false;
            this.dgSaldos.AllowUserToDeleteRows = false;
            this.dgSaldos.AllowUserToOrderColumns = true;
            this.dgSaldos.AllowUserToResizeColumns = false;
            this.dgSaldos.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgSaldos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgSaldos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgSaldos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSaldos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.SEL,
            this.CVE_CLIE,
            this.NO_FACTURA,
            this.REFER,
            this.CARGOS,
            this.SALDO,
            this.STATUS,
            this.ID_MOV});
            this.dgSaldos.Location = new System.Drawing.Point(15, 79);
            this.dgSaldos.Name = "dgSaldos";
            this.dgSaldos.Size = new System.Drawing.Size(829, 369);
            this.dgSaldos.TabIndex = 2;
            this.dgSaldos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSaldos_CellContentClick);
            this.dgSaldos.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgSaldos_DataError);
            // 
            // btnAjustar
            // 
            this.btnAjustar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAjustar.Enabled = false;
            this.btnAjustar.Location = new System.Drawing.Point(648, 478);
            this.btnAjustar.Name = "btnAjustar";
            this.btnAjustar.Size = new System.Drawing.Size(95, 26);
            this.btnAjustar.TabIndex = 3;
            this.btnAjustar.Text = "Ajustar";
            this.btnAjustar.UseVisualStyleBackColor = true;
            this.btnAjustar.Click += new System.EventHandler(this.btnAjustar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.Location = new System.Drawing.Point(749, 478);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(95, 26);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // lblTotalRegistros
            // 
            this.lblTotalRegistros.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalRegistros.AutoSize = true;
            this.lblTotalRegistros.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalRegistros.Location = new System.Drawing.Point(12, 478);
            this.lblTotalRegistros.Name = "lblTotalRegistros";
            this.lblTotalRegistros.Size = new System.Drawing.Size(121, 13);
            this.lblTotalRegistros.TabIndex = 5;
            this.lblTotalRegistros.Text = "Total de registros: 0";
            // 
            // lblSeleccionados
            // 
            this.lblSeleccionados.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSeleccionados.AutoSize = true;
            this.lblSeleccionados.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeleccionados.Location = new System.Drawing.Point(12, 494);
            this.lblSeleccionados.Name = "lblSeleccionados";
            this.lblSeleccionados.Size = new System.Drawing.Size(109, 13);
            this.lblSeleccionados.TabIndex = 6;
            this.lblSeleccionados.Text = "Seleccionados : 0";
            // 
            // btnSeleccionar
            // 
            this.btnSeleccionar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSeleccionar.Location = new System.Drawing.Point(15, 452);
            this.btnSeleccionar.Name = "btnSeleccionar";
            this.btnSeleccionar.Size = new System.Drawing.Size(106, 23);
            this.btnSeleccionar.TabIndex = 7;
            this.btnSeleccionar.Text = "Seleccionar todo";
            this.btnSeleccionar.UseVisualStyleBackColor = true;
            this.btnSeleccionar.Click += new System.EventHandler(this.btnSeleccionar_Click);
            // 
            // btnExportar
            // 
            this.btnExportar.Location = new System.Drawing.Point(579, 50);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(118, 23);
            this.btnExportar.TabIndex = 8;
            this.btnExportar.Text = "Exportar a Excel";
            this.btnExportar.UseVisualStyleBackColor = true;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // SEL
            // 
            this.SEL.DataPropertyName = "SEL";
            this.SEL.HeaderText = "Sel";
            this.SEL.Name = "SEL";
            this.SEL.Width = 50;
            // 
            // CVE_CLIE
            // 
            this.CVE_CLIE.DataPropertyName = "CVE_CLIE";
            this.CVE_CLIE.HeaderText = "CLAVE CLIENE";
            this.CVE_CLIE.Name = "CVE_CLIE";
            this.CVE_CLIE.ReadOnly = true;
            this.CVE_CLIE.Width = 105;
            // 
            // NO_FACTURA
            // 
            this.NO_FACTURA.DataPropertyName = "NO_FACTURA";
            this.NO_FACTURA.HeaderText = "FACTURA";
            this.NO_FACTURA.Name = "NO_FACTURA";
            this.NO_FACTURA.ReadOnly = true;
            // 
            // REFER
            // 
            this.REFER.DataPropertyName = "REFER";
            this.REFER.HeaderText = "DOCTO";
            this.REFER.Name = "REFER";
            this.REFER.ReadOnly = true;
            // 
            // CARGOS
            // 
            this.CARGOS.DataPropertyName = "CARGOS";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "C2";
            dataGridViewCellStyle2.NullValue = null;
            this.CARGOS.DefaultCellStyle = dataGridViewCellStyle2;
            this.CARGOS.HeaderText = "MONTO";
            this.CARGOS.Name = "CARGOS";
            this.CARGOS.ReadOnly = true;
            // 
            // SALDO
            // 
            this.SALDO.DataPropertyName = "SALDO";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "C2";
            dataGridViewCellStyle3.NullValue = null;
            this.SALDO.DefaultCellStyle = dataGridViewCellStyle3;
            this.SALDO.HeaderText = "SALDO";
            this.SALDO.Name = "SALDO";
            this.SALDO.ReadOnly = true;
            // 
            // STATUS
            // 
            this.STATUS.DataPropertyName = "STATUS";
            this.STATUS.HeaderText = "STATUS";
            this.STATUS.Name = "STATUS";
            this.STATUS.ReadOnly = true;
            this.STATUS.Visible = false;
            this.STATUS.Width = 150;
            // 
            // ID_MOV
            // 
            this.ID_MOV.DataPropertyName = "ID_MOV";
            this.ID_MOV.HeaderText = "ID_MOV";
            this.ID_MOV.Name = "ID_MOV";
            this.ID_MOV.ReadOnly = true;
            this.ID_MOV.Visible = false;
            // 
            // frmAjusteCxC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.scape1;
            this.ClientSize = new System.Drawing.Size(856, 513);
            this.Controls.Add(this.btnExportar);
            this.Controls.Add(this.btnSeleccionar);
            this.Controls.Add(this.lblSeleccionados);
            this.Controls.Add(this.lblTotalRegistros);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAjustar);
            this.Controls.Add(this.dgSaldos);
            this.Controls.Add(this.btnMostrar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMontoDiferencia);
            this.Name = "frmAjusteCxC";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ajuste de CxC en Aspel-Sae";
            ((System.ComponentModel.ISupportInitialize)(this.dgSaldos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UserControls.Scape scape1;
        private System.Windows.Forms.Button btnAjustar;
        private System.Windows.Forms.DataGridView dgSaldos;
        private System.Windows.Forms.Button btnMostrar;
        private System.Windows.Forms.Label label1;
        private UserControls.NumericTextBox txtMontoDiferencia;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblTotalRegistros;
        private System.Windows.Forms.Label lblSeleccionados;
        private System.Windows.Forms.Button btnSeleccionar;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SEL;
        private System.Windows.Forms.DataGridViewTextBoxColumn CVE_CLIE;
        private System.Windows.Forms.DataGridViewTextBoxColumn NO_FACTURA;
        private System.Windows.Forms.DataGridViewTextBoxColumn REFER;
        private System.Windows.Forms.DataGridViewTextBoxColumn CARGOS;
        private System.Windows.Forms.DataGridViewTextBoxColumn SALDO;
        private System.Windows.Forms.DataGridViewTextBoxColumn STATUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID_MOV;
    }
}