namespace SIP
{
    partial class frmCaptCostProcs
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
            this.grdMaestro = new System.Windows.Forms.DataGridView();
            this.Pedido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Modelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tallas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Prendas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdDetalle = new System.Windows.Forms.DataGridView();
            this.Proceso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Que = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Como = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Donde = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pproceso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Costo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelCaption = new System.Windows.Forms.Panel();
            this.Caption = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdMaestro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDetalle)).BeginInit();
            this.panelCaption.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdMaestro
            // 
            this.grdMaestro.AllowUserToAddRows = false;
            this.grdMaestro.AllowUserToDeleteRows = false;
            this.grdMaestro.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdMaestro.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Pedido,
            this.Modelo,
            this.Tallas,
            this.Prendas,
            this.Precio});
            this.grdMaestro.Location = new System.Drawing.Point(8, 8);
            this.grdMaestro.Name = "grdMaestro";
            this.grdMaestro.ReadOnly = true;
            this.grdMaestro.RowHeadersWidth = 20;
            this.grdMaestro.Size = new System.Drawing.Size(704, 200);
            this.grdMaestro.TabIndex = 0;
            // 
            // Pedido
            // 
            this.Pedido.HeaderText = "Pedido";
            this.Pedido.Name = "Pedido";
            this.Pedido.ReadOnly = true;
            this.Pedido.Width = 65;
            // 
            // Modelo
            // 
            this.Modelo.HeaderText = "Modelo";
            this.Modelo.Name = "Modelo";
            this.Modelo.ReadOnly = true;
            this.Modelo.Width = 82;
            // 
            // Tallas
            // 
            this.Tallas.HeaderText = "Tallas";
            this.Tallas.Name = "Tallas";
            this.Tallas.ReadOnly = true;
            this.Tallas.Width = 400;
            // 
            // Prendas
            // 
            this.Prendas.HeaderText = "Prendas";
            this.Prendas.Name = "Prendas";
            this.Prendas.ReadOnly = true;
            this.Prendas.Width = 65;
            // 
            // Precio
            // 
            this.Precio.HeaderText = "Precio";
            this.Precio.Name = "Precio";
            this.Precio.ReadOnly = true;
            this.Precio.Width = 65;
            // 
            // grdDetalle
            // 
            this.grdDetalle.AccessibleDescription = "";
            this.grdDetalle.AccessibleName = "";
            this.grdDetalle.AllowUserToAddRows = false;
            this.grdDetalle.AllowUserToDeleteRows = false;
            this.grdDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdDetalle.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Proceso,
            this.Que,
            this.Como,
            this.Donde,
            this.Pproceso,
            this.Costo});
            this.grdDetalle.Location = new System.Drawing.Point(8, 232);
            this.grdDetalle.Name = "grdDetalle";
            this.grdDetalle.ReadOnly = true;
            this.grdDetalle.RowHeadersWidth = 20;
            this.grdDetalle.Size = new System.Drawing.Size(704, 120);
            this.grdDetalle.TabIndex = 1;
            // 
            // Proceso
            // 
            this.Proceso.HeaderText = "Proceso";
            this.Proceso.Name = "Proceso";
            this.Proceso.ReadOnly = true;
            this.Proceso.Width = 50;
            // 
            // Que
            // 
            this.Que.HeaderText = "Que";
            this.Que.Name = "Que";
            this.Que.ReadOnly = true;
            this.Que.Width = 150;
            // 
            // Como
            // 
            this.Como.HeaderText = "Como";
            this.Como.Name = "Como";
            this.Como.ReadOnly = true;
            this.Como.Width = 150;
            // 
            // Donde
            // 
            this.Donde.HeaderText = "Donde";
            this.Donde.Name = "Donde";
            this.Donde.ReadOnly = true;
            this.Donde.Width = 150;
            // 
            // Pproceso
            // 
            this.Pproceso.HeaderText = "P.Proceso";
            this.Pproceso.Name = "Pproceso";
            this.Pproceso.ReadOnly = true;
            this.Pproceso.Width = 70;
            // 
            // Costo
            // 
            this.Costo.HeaderText = "Costo";
            this.Costo.Name = "Costo";
            this.Costo.ReadOnly = true;
            this.Costo.Width = 70;
            // 
            // panelCaption
            // 
            this.panelCaption.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCaption.Controls.Add(this.Caption);
            this.panelCaption.Location = new System.Drawing.Point(8, 218);
            this.panelCaption.Name = "panelCaption";
            this.panelCaption.Size = new System.Drawing.Size(704, 14);
            this.panelCaption.TabIndex = 2;
            // 
            // Caption
            // 
            this.Caption.Location = new System.Drawing.Point(3, -1);
            this.Caption.Name = "Caption";
            this.Caption.Size = new System.Drawing.Size(700, 23);
            this.Caption.TabIndex = 0;
            this.Caption.Text = "Procesos de la partida";
            this.Caption.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // frmCaptCostProcs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 361);
            this.Controls.Add(this.panelCaption);
            this.Controls.Add(this.grdDetalle);
            this.Controls.Add(this.grdMaestro);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCaptCostProcs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Captura de Costos de Procesos";
            this.Load += new System.EventHandler(this.frmCaptCostProcs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdMaestro)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDetalle)).EndInit();
            this.panelCaption.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grdMaestro;
        private System.Windows.Forms.DataGridView grdDetalle;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pedido;
        private System.Windows.Forms.DataGridViewTextBoxColumn Modelo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tallas;
        private System.Windows.Forms.DataGridViewTextBoxColumn Prendas;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precio;
        private System.Windows.Forms.DataGridViewTextBoxColumn Proceso;
        private System.Windows.Forms.DataGridViewTextBoxColumn Que;
        private System.Windows.Forms.DataGridViewTextBoxColumn Como;
        private System.Windows.Forms.DataGridViewTextBoxColumn Donde;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pproceso;
        private System.Windows.Forms.DataGridViewTextBoxColumn Costo;
        private System.Windows.Forms.Panel panelCaption;
        private System.Windows.Forms.Label Caption;
    }
}