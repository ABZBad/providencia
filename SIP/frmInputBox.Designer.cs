using System.Windows.Forms;

namespace SIP
{
    public partial class frmInputBox : Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        public System.ComponentModel.IContainer components = null;

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
        public void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.txtOrden = new System.Windows.Forms.TextBox();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.NTxtOrden = new SIP.UserControls.NumericTextBox();
            this.scape1 = new SIP.UserControls.Scape();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Location = new System.Drawing.Point(13, 13);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(43, 13);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "lblTitulo";
            // 
            // btnAceptar
            // 
            this.btnAceptar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAceptar.Location = new System.Drawing.Point(282, 9);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(64, 23);
            this.btnAceptar.TabIndex = 1;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(281, 35);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(65, 23);
            this.btnCancelar.TabIndex = 2;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // txtOrden
            // 
            this.txtOrden.Location = new System.Drawing.Point(13, 90);
            this.txtOrden.Name = "txtOrden";
            this.txtOrden.Size = new System.Drawing.Size(330, 20);
            this.txtOrden.TabIndex = 3;
            // 
            // dtpFecha
            // 
            this.dtpFecha.Location = new System.Drawing.Point(12, 90);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(329, 20);
            this.dtpFecha.TabIndex = 4;
            this.dtpFecha.Visible = false;
            // 
            // NTxtOrden
            // 
            this.NTxtOrden.Location = new System.Drawing.Point(13, 90);
            this.NTxtOrden.MaxValue = 0;
            this.NTxtOrden.MinValue = 0;
            this.NTxtOrden.Name = "NTxtOrden";
            this.NTxtOrden.Size = new System.Drawing.Size(330, 20);
            this.NTxtOrden.TabIndex = 0;
            this.NTxtOrden.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NTxtOrden_KeyPress);
            // 
            // scape1
            // 
            this.scape1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.scape1.Form = this;
            // 
            // frmInputBox
            // 
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(353, 117);
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.NTxtOrden);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.txtOrden);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmInputBox";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Text";
            this.Activated += new System.EventHandler(this.frmInputBox_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmInputBox_FormClosing);
            this.Load += new System.EventHandler(this.frmRepOrdProd_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lblTitulo;
        protected UserControls.Scape scape1;
        public UserControls.NumericTextBox NTxtOrden;
        protected System.Windows.Forms.Button btnAceptar;
        protected System.Windows.Forms.Button btnCancelar;
        public TextBox txtOrden;
        public DateTimePicker dtpFecha;
    }
}