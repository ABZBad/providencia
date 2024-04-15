namespace SIP
{
    partial class frmBitacoraComentariosClientes
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
            this.lblBitacora = new System.Windows.Forms.Label();
            this.lblComentario = new System.Windows.Forms.Label();
            this.txtComentarios = new System.Windows.Forms.TextBox();
            this.lblEvento = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.cmbClientesNombre = new System.Windows.Forms.ComboBox();
            this.cmbClientesRFC = new System.Windows.Forms.ComboBox();
            this.cmbClientesClave = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lblBitacora
            // 
            this.lblBitacora.AutoSize = true;
            this.lblBitacora.Location = new System.Drawing.Point(13, 53);
            this.lblBitacora.Name = "lblBitacora";
            this.lblBitacora.Size = new System.Drawing.Size(103, 13);
            this.lblBitacora.TabIndex = 0;
            this.lblBitacora.Text = "Selecione el Cliente:";
            // 
            // lblComentario
            // 
            this.lblComentario.AutoSize = true;
            this.lblComentario.Location = new System.Drawing.Point(13, 83);
            this.lblComentario.Name = "lblComentario";
            this.lblComentario.Size = new System.Drawing.Size(118, 13);
            this.lblComentario.TabIndex = 2;
            this.lblComentario.Text = "Introduce el comentario";
            // 
            // txtComentarios
            // 
            this.txtComentarios.Location = new System.Drawing.Point(15, 99);
            this.txtComentarios.Multiline = true;
            this.txtComentarios.Name = "txtComentarios";
            this.txtComentarios.Size = new System.Drawing.Size(572, 54);
            this.txtComentarios.TabIndex = 3;
            // 
            // lblEvento
            // 
            this.lblEvento.AutoSize = true;
            this.lblEvento.Location = new System.Drawing.Point(287, 163);
            this.lblEvento.Name = "lblEvento";
            this.lblEvento.Size = new System.Drawing.Size(94, 13);
            this.lblEvento.TabIndex = 6;
            this.lblEvento.Text = "Fecha del Evento:";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(512, 185);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 27);
            this.btnGuardar.TabIndex = 7;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(71, 9);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(488, 24);
            this.lblTitulo.TabIndex = 8;
            this.lblTitulo.Text = "Bitacora de Comentarios y Seguimiento de Clientes";
            // 
            // dtpFecha
            // 
            this.dtpFecha.Enabled = false;
            this.dtpFecha.Location = new System.Drawing.Point(387, 159);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(200, 20);
            this.dtpFecha.TabIndex = 9;
            // 
            // cmbClientesNombre
            // 
            this.cmbClientesNombre.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbClientesNombre.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbClientesNombre.FormattingEnabled = true;
            this.cmbClientesNombre.Location = new System.Drawing.Point(311, 50);
            this.cmbClientesNombre.Name = "cmbClientesNombre";
            this.cmbClientesNombre.Size = new System.Drawing.Size(276, 21);
            this.cmbClientesNombre.TabIndex = 12;
            // 
            // cmbClientesRFC
            // 
            this.cmbClientesRFC.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbClientesRFC.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbClientesRFC.FormattingEnabled = true;
            this.cmbClientesRFC.Location = new System.Drawing.Point(186, 50);
            this.cmbClientesRFC.Name = "cmbClientesRFC";
            this.cmbClientesRFC.Size = new System.Drawing.Size(119, 21);
            this.cmbClientesRFC.TabIndex = 11;
            // 
            // cmbClientesClave
            // 
            this.cmbClientesClave.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbClientesClave.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbClientesClave.FormattingEnabled = true;
            this.cmbClientesClave.Location = new System.Drawing.Point(115, 50);
            this.cmbClientesClave.Name = "cmbClientesClave";
            this.cmbClientesClave.Size = new System.Drawing.Size(65, 21);
            this.cmbClientesClave.TabIndex = 10;
            // 
            // frmBitacoraComentariosClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 228);
            this.Controls.Add(this.cmbClientesNombre);
            this.Controls.Add(this.cmbClientesRFC);
            this.Controls.Add(this.cmbClientesClave);
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.lblEvento);
            this.Controls.Add(this.txtComentarios);
            this.Controls.Add(this.lblComentario);
            this.Controls.Add(this.lblBitacora);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBitacoraComentariosClientes";
            this.Text = "Bitacora de Comentarios";
            this.Load += new System.EventHandler(this.frmBitacoraComentariosClientes_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblBitacora;
        private System.Windows.Forms.Label lblComentario;
        private System.Windows.Forms.TextBox txtComentarios;
        private System.Windows.Forms.Label lblEvento;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.ComboBox cmbClientesNombre;
        private System.Windows.Forms.ComboBox cmbClientesRFC;
        private System.Windows.Forms.ComboBox cmbClientesClave;
    }
}