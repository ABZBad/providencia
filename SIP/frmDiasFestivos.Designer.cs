namespace SIP
{
    partial class frmDiasFestivos
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
            this.dtViewMaestro = new System.Windows.Forms.DataGridView();
            this.DTPicker1 = new System.Windows.Forms.DateTimePicker();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dtViewMaestro)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(249, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Por favor capture un nuevo dia festivo o no laboral.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nota:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(57, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(313, 33);
            this.label3.TabIndex = 2;
            this.label3.Text = "No es necesario agregar Sábados o Domingos los mismos son descartados automáticam" +
    "ente.";
            // 
            // dtViewMaestro
            // 
            this.dtViewMaestro.AllowUserToAddRows = false;
            this.dtViewMaestro.AllowUserToDeleteRows = false;
            this.dtViewMaestro.AllowUserToResizeColumns = false;
            this.dtViewMaestro.AllowUserToResizeRows = false;
            this.dtViewMaestro.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dtViewMaestro.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtViewMaestro.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Fecha});
            this.dtViewMaestro.Location = new System.Drawing.Point(16, 70);
            this.dtViewMaestro.Name = "dtViewMaestro";
            this.dtViewMaestro.Size = new System.Drawing.Size(354, 197);
            this.dtViewMaestro.TabIndex = 3;
            // 
            // DTPicker1
            // 
            this.DTPicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTPicker1.Location = new System.Drawing.Point(183, 292);
            this.DTPicker1.Name = "DTPicker1";
            this.DTPicker1.Size = new System.Drawing.Size(79, 20);
            this.DTPicker1.TabIndex = 4;
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(288, 289);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(82, 23);
            this.btnAgregar.TabIndex = 5;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // Fecha
            // 
            this.Fecha.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Fecha.DataPropertyName = "Fecha";
            this.Fecha.HeaderText = "Fecha";
            this.Fecha.Name = "Fecha";
            this.Fecha.ReadOnly = true;
            this.Fecha.Width = 62;
            // 
            // frmDiasFestivos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 324);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.DTPicker1);
            this.Controls.Add(this.dtViewMaestro);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDiasFestivos";
            this.Text = "Administración de días festivos o no laborables";
            ((System.ComponentModel.ISupportInitialize)(this.dtViewMaestro)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dtViewMaestro;
        private System.Windows.Forms.DateTimePicker DTPicker1;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
    }
}