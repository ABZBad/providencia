namespace SIP
{
    partial class frmSeleccionSolicitudesSimulador
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
            this.dgvSolicitudes = new System.Windows.Forms.DataGridView();
            this.Solicitud = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Simulador = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSolicitudes)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(70, 9);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(248, 26);
            this.lblTitulo.TabIndex = 4;
            this.lblTitulo.Text = "Seleccion de Solicitud";
            // 
            // dgvSolicitudes
            // 
            this.dgvSolicitudes.AllowUserToAddRows = false;
            this.dgvSolicitudes.AllowUserToDeleteRows = false;
            this.dgvSolicitudes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSolicitudes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Solicitud,
            this.Codigo,
            this.Simulador});
            this.dgvSolicitudes.Location = new System.Drawing.Point(12, 38);
            this.dgvSolicitudes.Name = "dgvSolicitudes";
            this.dgvSolicitudes.ReadOnly = true;
            this.dgvSolicitudes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSolicitudes.Size = new System.Drawing.Size(356, 150);
            this.dgvSolicitudes.TabIndex = 5;
            this.dgvSolicitudes.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSolicitudes_CellContentClick);
            // 
            // Solicitud
            // 
            this.Solicitud.DataPropertyName = "Solicitud";
            this.Solicitud.HeaderText = "Solicitud";
            this.Solicitud.Name = "Solicitud";
            this.Solicitud.ReadOnly = true;
            this.Solicitud.Width = 50;
            // 
            // Codigo
            // 
            this.Codigo.DataPropertyName = "CODIGO_COTIZACION";
            this.Codigo.HeaderText = "Codigo";
            this.Codigo.Name = "Codigo";
            this.Codigo.ReadOnly = true;
            this.Codigo.Width = 150;
            // 
            // Simulador
            // 
            this.Simulador.HeaderText = "Simulador";
            this.Simulador.Name = "Simulador";
            this.Simulador.ReadOnly = true;
            this.Simulador.Text = "Abrir";
            this.Simulador.UseColumnTextForButtonValue = true;
            this.Simulador.Width = 80;
            // 
            // frmSeleccionSolicitudesSimulador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 199);
            this.Controls.Add(this.dgvSolicitudes);
            this.Controls.Add(this.lblTitulo);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSeleccionSolicitudesSimulador";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Solicitudes Pedido";
            this.Load += new System.EventHandler(this.frmSeleccionSolicitudesSimulador_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSolicitudes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.DataGridView dgvSolicitudes;
        private System.Windows.Forms.DataGridViewTextBoxColumn Solicitud;
        private System.Windows.Forms.DataGridViewTextBoxColumn Codigo;
        private System.Windows.Forms.DataGridViewButtonColumn Simulador;
    }
}