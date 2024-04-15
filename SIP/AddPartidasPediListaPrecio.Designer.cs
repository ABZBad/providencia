namespace SIP
{
    partial class AddPartidasPediListaPrecio
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.colTipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrecio1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblModelo = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.scape1 = new SIP.UserControls.Scape();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTipo,
            this.colPrecio1});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Location = new System.Drawing.Point(12, 43);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(183)))), ((int)(((byte)(142)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(181, 148);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDoubleClick);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // colTipo
            // 
            this.colTipo.DataPropertyName = "TIPO";
            this.colTipo.HeaderText = "TIPO";
            this.colTipo.Name = "colTipo";
            this.colTipo.ReadOnly = true;
            this.colTipo.Width = 50;
            // 
            // colPrecio1
            // 
            this.colPrecio1.DataPropertyName = "PRECIO1";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "C2";
            dataGridViewCellStyle1.NullValue = "0";
            this.colPrecio1.DefaultCellStyle = dataGridViewCellStyle1;
            this.colPrecio1.HeaderText = "PRECIO";
            this.colPrecio1.Name = "colPrecio1";
            this.colPrecio1.ReadOnly = true;
            // 
            // lblModelo
            // 
            this.lblModelo.BackColor = System.Drawing.Color.White;
            this.lblModelo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblModelo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModelo.Location = new System.Drawing.Point(12, 13);
            this.lblModelo.Name = "lblModelo";
            this.lblModelo.Size = new System.Drawing.Size(182, 23);
            this.lblModelo.TabIndex = 1;
            this.lblModelo.Text = "label1";
            this.lblModelo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnOk
            // 
            this.btnOk.Image = global::SIP.Properties.Resources.downArrow;
            this.btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOk.Location = new System.Drawing.Point(87, 198);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(100, 27);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "Seleccionar";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // scape1
            // 
            this.scape1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.scape1.Form = this;
            // 
            // AddPartidasPediListaPrecio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.scape1;
            this.ClientSize = new System.Drawing.Size(206, 234);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblModelo);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AddPartidasPediListaPrecio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Precios para :";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddPartidasPediListaPrecio_FormClosing);
            this.Load += new System.EventHandler(this.AddPartidasPediListaPrecio_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrecio1;
        private System.Windows.Forms.Label lblModelo;
        private System.Windows.Forms.Button btnOk;
        private UserControls.Scape scape1;

    }
}