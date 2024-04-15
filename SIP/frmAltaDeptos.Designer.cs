namespace SIP
{
    partial class frmAltaDeptos
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
            this.btnAgregar = new System.Windows.Forms.Button();
            this.txtDescripcion = new SIP.UserControls.TextBoxEx();
            this.txtDepartamento = new SIP.UserControls.TextBoxEx();
            this.txtNombre = new SIP.UserControls.TextBoxEx();
            this.scape1 = new SIP.UserControls.Scape();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Descripción";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Departamento";
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(233, 66);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(66, 23);
            this.btnAgregar.TabIndex = 3;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(93, 34);
            this.txtDescripcion.MaxLength = 40;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.OnlyUpperCase = false;
            this.txtDescripcion.Size = new System.Drawing.Size(209, 20);
            this.txtDescripcion.TabIndex = 1;
            // 
            // txtDepartamento
            // 
            this.txtDepartamento.Location = new System.Drawing.Point(93, 56);
            this.txtDepartamento.MaxLength = 20;
            this.txtDepartamento.Name = "txtDepartamento";
            this.txtDepartamento.OnlyUpperCase = false;
            this.txtDepartamento.Size = new System.Drawing.Size(134, 20);
            this.txtDepartamento.TabIndex = 2;
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(93, 10);
            this.txtNombre.MaxLength = 3;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.OnlyUpperCase = false;
            this.txtNombre.Size = new System.Drawing.Size(134, 20);
            this.txtNombre.TabIndex = 0;
            // 
            // scape1
            // 
            this.scape1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.scape1.Form = this;
            // 
            // frmAltaDeptos
            // 
            this.AcceptButton = this.btnAgregar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.scape1;
            this.ClientSize = new System.Drawing.Size(314, 107);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.txtDepartamento);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAltaDeptos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Alta de Departamentos";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private UserControls.TextBoxEx txtNombre;
        private UserControls.TextBoxEx txtDepartamento;
        private UserControls.TextBoxEx txtDescripcion;
        private System.Windows.Forms.Button btnAgregar;
        private UserControls.Scape scape1;
    }
}