namespace SIP
{
    partial class frmTransferenciaXModelo
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblModelo = new System.Windows.Forms.Label();
            this.gbTallas = new System.Windows.Forms.GroupBox();
            this.dgViewTallas4 = new System.Windows.Forms.DataGridView();
            this.dgViewTallas1 = new System.Windows.Forms.DataGridView();
            this.dgViewTallas3 = new System.Windows.Forms.DataGridView();
            this.dgViewTallas2 = new System.Windows.Forms.DataGridView();
            this.btnProcesar = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.txtModelo = new SIP.UserControls.TextBoxEx();
            this.txtOrigen = new SIP.UserControls.NumericTextBox();
            this.txtDestino = new SIP.UserControls.NumericTextBox();
            this.scape1 = new SIP.UserControls.Scape();
            this.lblPermisoParaTransferir = new System.Windows.Forms.Label();
            this.gbTallas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Modelo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(180, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Origen";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(274, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Destino";
            // 
            // lblModelo
            // 
            this.lblModelo.AutoSize = true;
            this.lblModelo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModelo.Location = new System.Drawing.Point(19, 36);
            this.lblModelo.Name = "lblModelo";
            this.lblModelo.Size = new System.Drawing.Size(48, 13);
            this.lblModelo.TabIndex = 6;
            this.lblModelo.Text = "Modelo";
            // 
            // gbTallas
            // 
            this.gbTallas.Controls.Add(this.dgViewTallas4);
            this.gbTallas.Controls.Add(this.dgViewTallas1);
            this.gbTallas.Controls.Add(this.dgViewTallas3);
            this.gbTallas.Controls.Add(this.dgViewTallas2);
            this.gbTallas.Location = new System.Drawing.Point(15, 66);
            this.gbTallas.Name = "gbTallas";
            this.gbTallas.Size = new System.Drawing.Size(438, 302);
            this.gbTallas.TabIndex = 7;
            this.gbTallas.TabStop = false;
            this.gbTallas.Text = "Tallas";
            this.gbTallas.Visible = false;
            // 
            // dgViewTallas4
            // 
            this.dgViewTallas4.AllowUserToAddRows = false;
            this.dgViewTallas4.AllowUserToDeleteRows = false;
            this.dgViewTallas4.AllowUserToResizeColumns = false;
            this.dgViewTallas4.AllowUserToResizeRows = false;
            this.dgViewTallas4.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgViewTallas4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgViewTallas4.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dgViewTallas4.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgViewTallas4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgViewTallas4.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgViewTallas4.Location = new System.Drawing.Point(17, 233);
            this.dgViewTallas4.Name = "dgViewTallas4";
            this.dgViewTallas4.RowHeadersVisible = false;
            this.dgViewTallas4.Size = new System.Drawing.Size(405, 64);
            this.dgViewTallas4.TabIndex = 3;
            this.dgViewTallas4.TabStop = false;
            this.dgViewTallas4.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgViewTallas4_CellEndEdit);
            this.dgViewTallas4.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgViewTallas4_ColumnAdded);
            // 
            // dgViewTallas1
            // 
            this.dgViewTallas1.AllowUserToAddRows = false;
            this.dgViewTallas1.AllowUserToDeleteRows = false;
            this.dgViewTallas1.AllowUserToResizeColumns = false;
            this.dgViewTallas1.AllowUserToResizeRows = false;
            this.dgViewTallas1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgViewTallas1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgViewTallas1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dgViewTallas1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgViewTallas1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgViewTallas1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgViewTallas1.Location = new System.Drawing.Point(17, 19);
            this.dgViewTallas1.MultiSelect = false;
            this.dgViewTallas1.Name = "dgViewTallas1";
            this.dgViewTallas1.RowHeadersVisible = false;
            this.dgViewTallas1.Size = new System.Drawing.Size(405, 64);
            this.dgViewTallas1.TabIndex = 0;
            this.dgViewTallas1.TabStop = false;
            this.dgViewTallas1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgViewTallas1_CellEndEdit);
            this.dgViewTallas1.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgViewTallas1_ColumnAdded);
            // 
            // dgViewTallas3
            // 
            this.dgViewTallas3.AllowUserToAddRows = false;
            this.dgViewTallas3.AllowUserToDeleteRows = false;
            this.dgViewTallas3.AllowUserToResizeColumns = false;
            this.dgViewTallas3.AllowUserToResizeRows = false;
            this.dgViewTallas3.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgViewTallas3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgViewTallas3.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dgViewTallas3.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgViewTallas3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgViewTallas3.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgViewTallas3.Location = new System.Drawing.Point(17, 162);
            this.dgViewTallas3.Name = "dgViewTallas3";
            this.dgViewTallas3.RowHeadersVisible = false;
            this.dgViewTallas3.Size = new System.Drawing.Size(405, 64);
            this.dgViewTallas3.TabIndex = 2;
            this.dgViewTallas3.TabStop = false;
            this.dgViewTallas3.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgViewTallas3_CellEndEdit);
            this.dgViewTallas3.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgViewTallas3_ColumnAdded);
            // 
            // dgViewTallas2
            // 
            this.dgViewTallas2.AllowUserToAddRows = false;
            this.dgViewTallas2.AllowUserToDeleteRows = false;
            this.dgViewTallas2.AllowUserToResizeColumns = false;
            this.dgViewTallas2.AllowUserToResizeRows = false;
            this.dgViewTallas2.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgViewTallas2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgViewTallas2.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dgViewTallas2.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgViewTallas2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgViewTallas2.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgViewTallas2.Location = new System.Drawing.Point(17, 89);
            this.dgViewTallas2.Name = "dgViewTallas2";
            this.dgViewTallas2.RowHeadersVisible = false;
            this.dgViewTallas2.Size = new System.Drawing.Size(405, 64);
            this.dgViewTallas2.TabIndex = 1;
            this.dgViewTallas2.TabStop = false;
            this.dgViewTallas2.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgViewTallas2_CellEndEdit);
            this.dgViewTallas2.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgViewTallas2_ColumnAdded);
            // 
            // btnProcesar
            // 
            this.btnProcesar.Enabled = false;
            this.btnProcesar.Location = new System.Drawing.Point(15, 374);
            this.btnProcesar.Name = "btnProcesar";
            this.btnProcesar.Size = new System.Drawing.Size(75, 23);
            this.btnProcesar.TabIndex = 3;
            this.btnProcesar.Text = "Procesar";
            this.btnProcesar.UseVisualStyleBackColor = true;
            this.btnProcesar.Click += new System.EventHandler(this.btnProcesar_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(317, 379);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(133, 13);
            this.lblTotal.TabIndex = 9;
            this.lblTotal.Text = "Total: 0";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtModelo
            // 
            this.txtModelo.Location = new System.Drawing.Point(60, 6);
            this.txtModelo.Name = "txtModelo";
            this.txtModelo.OnlyUpperCase = true;
            this.txtModelo.Size = new System.Drawing.Size(100, 20);
            this.txtModelo.TabIndex = 0;
            this.txtModelo.TextChanged += new System.EventHandler(this.txtModelo_TextChanged);
            this.txtModelo.Leave += new System.EventHandler(this.txtModelo_Leave);
            // 
            // txtOrigen
            // 
            this.txtOrigen.Location = new System.Drawing.Point(224, 6);
            this.txtOrigen.MaxValue = 0;
            this.txtOrigen.MinValue = 0;
            this.txtOrigen.Name = "txtOrigen";
            this.txtOrigen.Size = new System.Drawing.Size(33, 20);
            this.txtOrigen.TabIndex = 1;
            this.txtOrigen.Text = "1";
            this.txtOrigen.Leave += new System.EventHandler(this.txtOrigen_Leave);
            // 
            // txtDestino
            // 
            this.txtDestino.Location = new System.Drawing.Point(323, 6);
            this.txtDestino.MaxValue = 0;
            this.txtDestino.MinValue = 0;
            this.txtDestino.Name = "txtDestino";
            this.txtDestino.Size = new System.Drawing.Size(33, 20);
            this.txtDestino.TabIndex = 2;
            this.txtDestino.Text = "3";
            this.txtDestino.Leave += new System.EventHandler(this.txtDestino_Leave);
            // 
            // scape1
            // 
            this.scape1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.scape1.Form = this;
            // 
            // lblPermisoParaTransferir
            // 
            this.lblPermisoParaTransferir.AutoSize = true;
            this.lblPermisoParaTransferir.ForeColor = System.Drawing.Color.Red;
            this.lblPermisoParaTransferir.Location = new System.Drawing.Point(97, 379);
            this.lblPermisoParaTransferir.Name = "lblPermisoParaTransferir";
            this.lblPermisoParaTransferir.Size = new System.Drawing.Size(0, 13);
            this.lblPermisoParaTransferir.TabIndex = 10;
            // 
            // frmTransferenciaXModelo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.scape1;
            this.ClientSize = new System.Drawing.Size(465, 403);
            this.Controls.Add(this.lblPermisoParaTransferir);
            this.Controls.Add(this.txtModelo);
            this.Controls.Add(this.txtOrigen);
            this.Controls.Add(this.txtDestino);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.btnProcesar);
            this.Controls.Add(this.gbTallas);
            this.Controls.Add(this.lblModelo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTransferenciaXModelo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transferencia por Modelo";
            this.Load += new System.EventHandler(this.frmTransferenciaXModelo_Load);
            this.gbTallas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewTallas2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblModelo;
        private System.Windows.Forms.GroupBox gbTallas;
        private System.Windows.Forms.Button btnProcesar;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.DataGridView dgViewTallas3;
        private System.Windows.Forms.DataGridView dgViewTallas2;
        private UserControls.NumericTextBox txtDestino;
        private UserControls.NumericTextBox txtOrigen;
        private UserControls.TextBoxEx txtModelo;
        private System.Windows.Forms.DataGridView dgViewTallas1;
        private UserControls.Scape scape1;
        private System.Windows.Forms.DataGridView dgViewTallas4;
        private System.Windows.Forms.Label lblPermisoParaTransferir;
    }
}