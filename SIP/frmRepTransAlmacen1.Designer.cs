namespace SIP
{
    partial class frmRepTransAlmacen1
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
            this.dtFechaHasta = new System.Windows.Forms.DateTimePicker();
            this.dtFechaDesde = new System.Windows.Forms.DateTimePicker();
            this.btnContinuar = new System.Windows.Forms.Button();
            this.cbAlmacenes = new System.Windows.Forms.ComboBox();
            this.lbAlmacenes = new System.Windows.Forms.Label();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.lstAlmacenes = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // dtFechaHasta
            // 
            this.dtFechaHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFechaHasta.Location = new System.Drawing.Point(120, 10);
            this.dtFechaHasta.Name = "dtFechaHasta";
            this.dtFechaHasta.Size = new System.Drawing.Size(96, 20);
            this.dtFechaHasta.TabIndex = 2;
            // 
            // dtFechaDesde
            // 
            this.dtFechaDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFechaDesde.Location = new System.Drawing.Point(12, 12);
            this.dtFechaDesde.Name = "dtFechaDesde";
            this.dtFechaDesde.Size = new System.Drawing.Size(102, 20);
            this.dtFechaDesde.TabIndex = 1;
            this.dtFechaDesde.ValueChanged += new System.EventHandler(this.dtFechaDesde_ValueChanged);
            // 
            // btnContinuar
            // 
            this.btnContinuar.Location = new System.Drawing.Point(15, 231);
            this.btnContinuar.Name = "btnContinuar";
            this.btnContinuar.Size = new System.Drawing.Size(75, 23);
            this.btnContinuar.TabIndex = 5;
            this.btnContinuar.Text = "Continuar";
            this.btnContinuar.UseVisualStyleBackColor = true;
            this.btnContinuar.Click += new System.EventHandler(this.btnContinuar_Click);
            // 
            // cbAlmacenes
            // 
            this.cbAlmacenes.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbAlmacenes.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbAlmacenes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAlmacenes.FormattingEnabled = true;
            this.cbAlmacenes.Location = new System.Drawing.Point(133, 38);
            this.cbAlmacenes.Name = "cbAlmacenes";
            this.cbAlmacenes.Size = new System.Drawing.Size(47, 21);
            this.cbAlmacenes.TabIndex = 3;
            // 
            // lbAlmacenes
            // 
            this.lbAlmacenes.AutoSize = true;
            this.lbAlmacenes.Location = new System.Drawing.Point(12, 41);
            this.lbAlmacenes.Name = "lbAlmacenes";
            this.lbAlmacenes.Size = new System.Drawing.Size(115, 13);
            this.lbAlmacenes.TabIndex = 6;
            this.lbAlmacenes.Text = "Selecciona el Almacén";
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(186, 36);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(30, 23);
            this.btnAgregar.TabIndex = 4;
            this.btnAgregar.Text = ">>";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // lstAlmacenes
            // 
            this.lstAlmacenes.FormattingEnabled = true;
            this.lstAlmacenes.Location = new System.Drawing.Point(15, 65);
            this.lstAlmacenes.Name = "lstAlmacenes";
            this.lstAlmacenes.Size = new System.Drawing.Size(201, 160);
            this.lstAlmacenes.TabIndex = 8;
            this.lstAlmacenes.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstAlmacenes_MouseDoubleClick);
            // 
            // frmRepTransAlmacen1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(222, 273);
            this.Controls.Add(this.lstAlmacenes);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.lbAlmacenes);
            this.Controls.Add(this.cbAlmacenes);
            this.Controls.Add(this.btnContinuar);
            this.Controls.Add(this.dtFechaHasta);
            this.Controls.Add(this.dtFechaDesde);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRepTransAlmacen1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reporte Salidas de Almacén 1";
            this.Load += new System.EventHandler(this.frmRepTransAlmacen1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtFechaHasta;
        private System.Windows.Forms.DateTimePicker dtFechaDesde;
        private System.Windows.Forms.Button btnContinuar;
        private System.Windows.Forms.ComboBox cbAlmacenes;
        private System.Windows.Forms.Label lbAlmacenes;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.ListBox lstAlmacenes;
    }
}