namespace SIP
{
    partial class frmPartidas
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgViewPartidas = new System.Windows.Forms.DataGridView();
            this.CVE_ART = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CANT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PREC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gBoxTotales = new System.Windows.Forms.GroupBox();
            this.lblTotal5 = new System.Windows.Forms.Label();
            this.lblDescuento = new System.Windows.Forms.Label();
            this.lblTotal4 = new System.Windows.Forms.Label();
            this.lblTotal3 = new System.Windows.Forms.Label();
            this.lblTotal2 = new System.Windows.Forms.Label();
            this.lblTotal1 = new System.Windows.Forms.Label();
            this.lblPrendas = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblImpuesto = new System.Windows.Forms.Label();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.scape1 = new SIP.UserControls.Scape();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewPartidas)).BeginInit();
            this.gBoxTotales.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgViewPartidas
            // 
            this.dgViewPartidas.AllowUserToAddRows = false;
            this.dgViewPartidas.AllowUserToDeleteRows = false;
            this.dgViewPartidas.AllowUserToResizeColumns = false;
            this.dgViewPartidas.AllowUserToResizeRows = false;
            this.dgViewPartidas.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgViewPartidas.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dgViewPartidas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgViewPartidas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CVE_ART,
            this.CANT,
            this.PREC});
            this.dgViewPartidas.Location = new System.Drawing.Point(16, 15);
            this.dgViewPartidas.Margin = new System.Windows.Forms.Padding(4);
            this.dgViewPartidas.Name = "dgViewPartidas";
            this.dgViewPartidas.ReadOnly = true;
            this.dgViewPartidas.Size = new System.Drawing.Size(576, 375);
            this.dgViewPartidas.TabIndex = 0;
            // 
            // CVE_ART
            // 
            this.CVE_ART.DataPropertyName = "CVE_ART";
            this.CVE_ART.HeaderText = "Articulo";
            this.CVE_ART.Name = "CVE_ART";
            this.CVE_ART.ReadOnly = true;
            this.CVE_ART.Width = 150;
            // 
            // CANT
            // 
            this.CANT.DataPropertyName = "CANT";
            this.CANT.HeaderText = "Cantidad";
            this.CANT.Name = "CANT";
            this.CANT.ReadOnly = true;
            this.CANT.Width = 105;
            // 
            // PREC
            // 
            this.PREC.DataPropertyName = "PREC";
            dataGridViewCellStyle3.Format = "C2";
            dataGridViewCellStyle3.NullValue = null;
            this.PREC.DefaultCellStyle = dataGridViewCellStyle3;
            this.PREC.HeaderText = "Importe";
            this.PREC.Name = "PREC";
            this.PREC.ReadOnly = true;
            this.PREC.Width = 115;
            // 
            // gBoxTotales
            // 
            this.gBoxTotales.Controls.Add(this.lblTotal5);
            this.gBoxTotales.Controls.Add(this.lblDescuento);
            this.gBoxTotales.Controls.Add(this.lblTotal4);
            this.gBoxTotales.Controls.Add(this.lblTotal3);
            this.gBoxTotales.Controls.Add(this.lblTotal2);
            this.gBoxTotales.Controls.Add(this.lblTotal1);
            this.gBoxTotales.Controls.Add(this.lblPrendas);
            this.gBoxTotales.Controls.Add(this.lblTotal);
            this.gBoxTotales.Controls.Add(this.lblImpuesto);
            this.gBoxTotales.Controls.Add(this.lblSubtotal);
            this.gBoxTotales.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gBoxTotales.Location = new System.Drawing.Point(16, 398);
            this.gBoxTotales.Margin = new System.Windows.Forms.Padding(4);
            this.gBoxTotales.Name = "gBoxTotales";
            this.gBoxTotales.Padding = new System.Windows.Forms.Padding(4);
            this.gBoxTotales.Size = new System.Drawing.Size(576, 128);
            this.gBoxTotales.TabIndex = 1;
            this.gBoxTotales.TabStop = false;
            this.gBoxTotales.Text = "Totales";
            // 
            // lblTotal5
            // 
            this.lblTotal5.AutoSize = true;
            this.lblTotal5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal5.Location = new System.Drawing.Point(501, 31);
            this.lblTotal5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal5.Name = "lblTotal5";
            this.lblTotal5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblTotal5.Size = new System.Drawing.Size(45, 17);
            this.lblTotal5.TabIndex = 9;
            this.lblTotal5.Text = "Total";
            // 
            // lblDescuento
            // 
            this.lblDescuento.AutoSize = true;
            this.lblDescuento.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescuento.Location = new System.Drawing.Point(310, 30);
            this.lblDescuento.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDescuento.Name = "lblDescuento";
            this.lblDescuento.Size = new System.Drawing.Size(124, 17);
            this.lblDescuento.TabIndex = 8;
            this.lblDescuento.Text = "Descuento:     $";
            // 
            // lblTotal4
            // 
            this.lblTotal4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal4.Location = new System.Drawing.Point(102, 89);
            this.lblTotal4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal4.Name = "lblTotal4";
            this.lblTotal4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblTotal4.Size = new System.Drawing.Size(136, 17);
            this.lblTotal4.TabIndex = 7;
            this.lblTotal4.Text = "Total";
            // 
            // lblTotal3
            // 
            this.lblTotal3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal3.Location = new System.Drawing.Point(102, 69);
            this.lblTotal3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal3.Name = "lblTotal3";
            this.lblTotal3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblTotal3.Size = new System.Drawing.Size(136, 17);
            this.lblTotal3.TabIndex = 6;
            this.lblTotal3.Text = "Total";
            // 
            // lblTotal2
            // 
            this.lblTotal2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal2.Location = new System.Drawing.Point(102, 49);
            this.lblTotal2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal2.Name = "lblTotal2";
            this.lblTotal2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblTotal2.Size = new System.Drawing.Size(136, 17);
            this.lblTotal2.TabIndex = 5;
            this.lblTotal2.Text = "Total";
            // 
            // lblTotal1
            // 
            this.lblTotal1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal1.Location = new System.Drawing.Point(102, 30);
            this.lblTotal1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal1.Name = "lblTotal1";
            this.lblTotal1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblTotal1.Size = new System.Drawing.Size(136, 17);
            this.lblTotal1.TabIndex = 4;
            this.lblTotal1.Text = "Total";
            // 
            // lblPrendas
            // 
            this.lblPrendas.AutoSize = true;
            this.lblPrendas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrendas.Location = new System.Drawing.Point(9, 89);
            this.lblPrendas.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPrendas.Name = "lblPrendas";
            this.lblPrendas.Size = new System.Drawing.Size(73, 17);
            this.lblPrendas.TabIndex = 3;
            this.lblPrendas.Text = "Prendas:";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(9, 69);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(50, 17);
            this.lblTotal.TabIndex = 2;
            this.lblTotal.Text = "Total:";
            // 
            // lblImpuesto
            // 
            this.lblImpuesto.AutoSize = true;
            this.lblImpuesto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImpuesto.Location = new System.Drawing.Point(9, 49);
            this.lblImpuesto.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblImpuesto.Name = "lblImpuesto";
            this.lblImpuesto.Size = new System.Drawing.Size(78, 17);
            this.lblImpuesto.TabIndex = 1;
            this.lblImpuesto.Text = "Impuesto:";
            this.lblImpuesto.Click += new System.EventHandler(this.lblImpuesto_Click);
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.AutoSize = true;
            this.lblSubtotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtotal.Location = new System.Drawing.Point(8, 31);
            this.lblSubtotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(73, 17);
            this.lblSubtotal.TabIndex = 0;
            this.lblSubtotal.Text = "Subtotal:";
            // 
            // scape1
            // 
            this.scape1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.scape1.Form = this;
            // 
            // frmPartidas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.scape1;
            this.ClientSize = new System.Drawing.Size(621, 539);
            this.Controls.Add(this.gBoxTotales);
            this.Controls.Add(this.dgViewPartidas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPartidas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Partidas";
            this.Activated += new System.EventHandler(this.frmPartidas_Activated);
            this.Load += new System.EventHandler(this.frmPartidas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgViewPartidas)).EndInit();
            this.gBoxTotales.ResumeLayout(false);
            this.gBoxTotales.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgViewPartidas;
        private System.Windows.Forms.GroupBox gBoxTotales;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblImpuesto;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.Label lblPrendas;
        private System.Windows.Forms.Label lblTotal4;
        private System.Windows.Forms.Label lblTotal3;
        private System.Windows.Forms.Label lblTotal2;
        private System.Windows.Forms.Label lblTotal1;
        private UserControls.Scape scape1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CVE_ART;
        private System.Windows.Forms.DataGridViewTextBoxColumn CANT;
        private System.Windows.Forms.DataGridViewTextBoxColumn PREC;
        private System.Windows.Forms.Label lblTotal5;
        private System.Windows.Forms.Label lblDescuento;
    }
}