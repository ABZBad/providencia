namespace SIP
{
    partial class frmPermisosDetalle
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
            this.lblMenu = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkPuedeBorrar = new System.Windows.Forms.CheckBox();
            this.chkPuedeModificar = new System.Windows.Forms.CheckBox();
            this.chkPuedeInsertar = new System.Windows.Forms.CheckBox();
            this.chkPuedeEnetrar = new System.Windows.Forms.CheckBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.scape1 = new SIP.UserControls.Scape();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Menú:";
            // 
            // lblMenu
            // 
            this.lblMenu.BackColor = System.Drawing.Color.White;
            this.lblMenu.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMenu.Location = new System.Drawing.Point(12, 36);
            this.lblMenu.Name = "lblMenu";
            this.lblMenu.Size = new System.Drawing.Size(308, 67);
            this.lblMenu.TabIndex = 1;
            this.lblMenu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkPuedeBorrar);
            this.groupBox1.Controls.Add(this.chkPuedeModificar);
            this.groupBox1.Controls.Add(this.chkPuedeInsertar);
            this.groupBox1.Controls.Add(this.chkPuedeEnetrar);
            this.groupBox1.Location = new System.Drawing.Point(17, 124);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(304, 115);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Propiedades";
            // 
            // chkPuedeBorrar
            // 
            this.chkPuedeBorrar.AutoSize = true;
            this.chkPuedeBorrar.Location = new System.Drawing.Point(101, 89);
            this.chkPuedeBorrar.Name = "chkPuedeBorrar";
            this.chkPuedeBorrar.Size = new System.Drawing.Size(139, 17);
            this.chkPuedeBorrar.TabIndex = 3;
            this.chkPuedeBorrar.Text = "Mostrar: \"Puede Borrar\"";
            this.chkPuedeBorrar.UseVisualStyleBackColor = true;
            // 
            // chkPuedeModificar
            // 
            this.chkPuedeModificar.AutoSize = true;
            this.chkPuedeModificar.Location = new System.Drawing.Point(101, 66);
            this.chkPuedeModificar.Name = "chkPuedeModificar";
            this.chkPuedeModificar.Size = new System.Drawing.Size(154, 17);
            this.chkPuedeModificar.TabIndex = 2;
            this.chkPuedeModificar.Text = "Mostrar: \"Puede Modificar\"";
            this.chkPuedeModificar.UseVisualStyleBackColor = true;
            // 
            // chkPuedeInsertar
            // 
            this.chkPuedeInsertar.AutoSize = true;
            this.chkPuedeInsertar.Location = new System.Drawing.Point(101, 43);
            this.chkPuedeInsertar.Name = "chkPuedeInsertar";
            this.chkPuedeInsertar.Size = new System.Drawing.Size(146, 17);
            this.chkPuedeInsertar.TabIndex = 1;
            this.chkPuedeInsertar.Text = "Mostrar: \"Puede Insertar\"";
            this.chkPuedeInsertar.UseVisualStyleBackColor = true;
            // 
            // chkPuedeEnetrar
            // 
            this.chkPuedeEnetrar.AutoSize = true;
            this.chkPuedeEnetrar.Location = new System.Drawing.Point(101, 20);
            this.chkPuedeEnetrar.Name = "chkPuedeEnetrar";
            this.chkPuedeEnetrar.Size = new System.Drawing.Size(139, 17);
            this.chkPuedeEnetrar.TabIndex = 0;
            this.chkPuedeEnetrar.Text = "Mostrar: \"Puede Entrar\"";
            this.chkPuedeEnetrar.UseVisualStyleBackColor = true;
            // 
            // btnGuardar
            // 
            this.btnGuardar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnGuardar.Location = new System.Drawing.Point(165, 258);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 3;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(246, 258);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // scape1
            // 
            this.scape1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.scape1.Form = this;
            // 
            // frmPermisosDetalle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.scape1;
            this.ClientSize = new System.Drawing.Size(333, 295);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblMenu);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPermisosDetalle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Detalle de permisos";
            this.Load += new System.EventHandler(this.frmPermisosDetalle_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblMenu;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkPuedeBorrar;
        private System.Windows.Forms.CheckBox chkPuedeModificar;
        private System.Windows.Forms.CheckBox chkPuedeInsertar;
        private System.Windows.Forms.CheckBox chkPuedeEnetrar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private UserControls.Scape scape1;
    }
}