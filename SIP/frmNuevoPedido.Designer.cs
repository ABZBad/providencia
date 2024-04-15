namespace SIP
{
    partial class frmNuevoPedido
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
            this.components = new System.ComponentModel.Container();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.cmbPedSIPMetodoPago = new System.Windows.Forms.ComboBox();
            this.label44 = new System.Windows.Forms.Label();
            this.cmbPedSIPUsoCFDI = new System.Windows.Forms.ComboBox();
            this.label43 = new System.Windows.Forms.Label();
            this.cmbPedSIPFormaPago = new System.Windows.Forms.ComboBox();
            this.label42 = new System.Windows.Forms.Label();
            this.txtPedSIPOc = new SIP.UserControls.TextBoxEx();
            this.txtPedSIPFecha = new SIP.UserControls.TextBoxEx();
            this.txtPedSIPAgente = new SIP.UserControls.TextBoxEx();
            this.txtPedSIPCliente = new SIP.UserControls.TextBoxEx();
            this.txtPedSIPTerminos = new SIP.UserControls.TextBoxEx();
            this.txtPedSIPNo = new SIP.UserControls.TextBoxEx();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPedSIPAgregarPartidas = new System.Windows.Forms.Button();
            this.btnPedSIPElimModPartidas = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblPedSIPYaImpreso = new System.Windows.Forms.Label();
            this.txtPedSIPRemitido = new SIP.UserControls.TextBoxEx();
            this.txtPedSIPConsignado = new SIP.UserControls.TextBoxEx();
            this.txtPedSIPObservaciones = new SIP.UserControls.TextBoxEx();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnPedSIPImprimir = new System.Windows.Forms.Button();
            this.btnPedSIPDocumentos = new System.Windows.Forms.Button();
            this.label70 = new System.Windows.Forms.Label();
            this.txtPedSIPCotizacion = new SIP.UserControls.TextBoxEx();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnGuardar);
            this.groupBox3.Controls.Add(this.cmbPedSIPMetodoPago);
            this.groupBox3.Controls.Add(this.label44);
            this.groupBox3.Controls.Add(this.cmbPedSIPUsoCFDI);
            this.groupBox3.Controls.Add(this.label43);
            this.groupBox3.Controls.Add(this.cmbPedSIPFormaPago);
            this.groupBox3.Controls.Add(this.label42);
            this.groupBox3.Controls.Add(this.txtPedSIPOc);
            this.groupBox3.Controls.Add(this.txtPedSIPFecha);
            this.groupBox3.Controls.Add(this.txtPedSIPAgente);
            this.groupBox3.Controls.Add(this.txtPedSIPCliente);
            this.groupBox3.Controls.Add(this.txtPedSIPTerminos);
            this.groupBox3.Controls.Add(this.txtPedSIPNo);
            this.groupBox3.Controls.Add(this.label23);
            this.groupBox3.Controls.Add(this.label22);
            this.groupBox3.Controls.Add(this.label21);
            this.groupBox3.Controls.Add(this.label20);
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Size = new System.Drawing.Size(978, 195);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Datos Generales del Pedido";
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackgroundImage = global::SIP.Properties.Resources.Save;
            this.btnGuardar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGuardar.Location = new System.Drawing.Point(928, 144);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(38, 39);
            this.btnGuardar.TabIndex = 15;
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // cmbPedSIPMetodoPago
            // 
            this.cmbPedSIPMetodoPago.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbPedSIPMetodoPago.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbPedSIPMetodoPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPedSIPMetodoPago.FormattingEnabled = true;
            this.cmbPedSIPMetodoPago.Location = new System.Drawing.Point(744, 114);
            this.cmbPedSIPMetodoPago.Name = "cmbPedSIPMetodoPago";
            this.cmbPedSIPMetodoPago.Size = new System.Drawing.Size(223, 24);
            this.cmbPedSIPMetodoPago.TabIndex = 14;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(644, 119);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(65, 34);
            this.label44.TabIndex = 13;
            this.label44.Text = "Método\r\nde Pago:";
            // 
            // cmbPedSIPUsoCFDI
            // 
            this.cmbPedSIPUsoCFDI.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbPedSIPUsoCFDI.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbPedSIPUsoCFDI.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPedSIPUsoCFDI.FormattingEnabled = true;
            this.cmbPedSIPUsoCFDI.Location = new System.Drawing.Point(408, 121);
            this.cmbPedSIPUsoCFDI.Name = "cmbPedSIPUsoCFDI";
            this.cmbPedSIPUsoCFDI.Size = new System.Drawing.Size(206, 24);
            this.cmbPedSIPUsoCFDI.TabIndex = 12;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(333, 126);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(57, 34);
            this.label43.TabIndex = 11;
            this.label43.Text = "Uso de \r\nCFDI:";
            // 
            // cmbPedSIPFormaPago
            // 
            this.cmbPedSIPFormaPago.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbPedSIPFormaPago.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbPedSIPFormaPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPedSIPFormaPago.FormattingEnabled = true;
            this.cmbPedSIPFormaPago.Location = new System.Drawing.Point(138, 121);
            this.cmbPedSIPFormaPago.Name = "cmbPedSIPFormaPago";
            this.cmbPedSIPFormaPago.Size = new System.Drawing.Size(181, 24);
            this.cmbPedSIPFormaPago.TabIndex = 10;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(9, 126);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(109, 17);
            this.label42.TabIndex = 9;
            this.label42.Text = "Forma de Pago:";
            // 
            // txtPedSIPOc
            // 
            this.txtPedSIPOc.Location = new System.Drawing.Point(742, 75);
            this.txtPedSIPOc.MaxLength = 20;
            this.txtPedSIPOc.Name = "txtPedSIPOc";
            this.txtPedSIPOc.OnlyUpperCase = false;
            this.txtPedSIPOc.Size = new System.Drawing.Size(224, 22);
            this.txtPedSIPOc.TabIndex = 8;
            // 
            // txtPedSIPFecha
            // 
            this.txtPedSIPFecha.Location = new System.Drawing.Point(742, 29);
            this.txtPedSIPFecha.Name = "txtPedSIPFecha";
            this.txtPedSIPFecha.OnlyUpperCase = false;
            this.txtPedSIPFecha.ReadOnly = true;
            this.txtPedSIPFecha.Size = new System.Drawing.Size(224, 22);
            this.txtPedSIPFecha.TabIndex = 5;
            // 
            // txtPedSIPAgente
            // 
            this.txtPedSIPAgente.Location = new System.Drawing.Point(405, 75);
            this.txtPedSIPAgente.Name = "txtPedSIPAgente";
            this.txtPedSIPAgente.OnlyUpperCase = false;
            this.txtPedSIPAgente.ReadOnly = true;
            this.txtPedSIPAgente.Size = new System.Drawing.Size(210, 22);
            this.txtPedSIPAgente.TabIndex = 7;
            // 
            // txtPedSIPCliente
            // 
            this.txtPedSIPCliente.Location = new System.Drawing.Point(405, 29);
            this.txtPedSIPCliente.Name = "txtPedSIPCliente";
            this.txtPedSIPCliente.OnlyUpperCase = false;
            this.txtPedSIPCliente.ReadOnly = true;
            this.txtPedSIPCliente.Size = new System.Drawing.Size(210, 22);
            this.txtPedSIPCliente.TabIndex = 4;
            // 
            // txtPedSIPTerminos
            // 
            this.txtPedSIPTerminos.Location = new System.Drawing.Point(140, 75);
            this.txtPedSIPTerminos.Name = "txtPedSIPTerminos";
            this.txtPedSIPTerminos.OnlyUpperCase = false;
            this.txtPedSIPTerminos.ReadOnly = true;
            this.txtPedSIPTerminos.Size = new System.Drawing.Size(180, 22);
            this.txtPedSIPTerminos.TabIndex = 6;
            // 
            // txtPedSIPNo
            // 
            this.txtPedSIPNo.Location = new System.Drawing.Point(140, 29);
            this.txtPedSIPNo.Name = "txtPedSIPNo";
            this.txtPedSIPNo.OnlyUpperCase = false;
            this.txtPedSIPNo.ReadOnly = true;
            this.txtPedSIPNo.Size = new System.Drawing.Size(180, 22);
            this.txtPedSIPNo.TabIndex = 3;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(648, 80);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(32, 17);
            this.label23.TabIndex = 5;
            this.label23.Text = "OC:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(648, 34);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(51, 17);
            this.label22.TabIndex = 4;
            this.label22.Text = "Fecha:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(333, 80);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(57, 17);
            this.label21.TabIndex = 3;
            this.label21.Text = "Agente:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(333, 34);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(55, 17);
            this.label20.TabIndex = 2;
            this.label20.Text = "Cliente:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(9, 80);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(71, 17);
            this.label19.TabIndex = 1;
            this.label19.Text = "Términos:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "No:";
            // 
            // btnPedSIPAgregarPartidas
            // 
            this.btnPedSIPAgregarPartidas.Location = new System.Drawing.Point(850, 468);
            this.btnPedSIPAgregarPartidas.Name = "btnPedSIPAgregarPartidas";
            this.btnPedSIPAgregarPartidas.Size = new System.Drawing.Size(140, 35);
            this.btnPedSIPAgregarPartidas.TabIndex = 20;
            this.btnPedSIPAgregarPartidas.Text = "Agregar Partidas";
            this.btnPedSIPAgregarPartidas.UseVisualStyleBackColor = true;
            this.btnPedSIPAgregarPartidas.Click += new System.EventHandler(this.btnPedSIPAgregarPartidas_Click);
            // 
            // btnPedSIPElimModPartidas
            // 
            this.btnPedSIPElimModPartidas.Location = new System.Drawing.Point(624, 468);
            this.btnPedSIPElimModPartidas.Name = "btnPedSIPElimModPartidas";
            this.btnPedSIPElimModPartidas.Size = new System.Drawing.Size(220, 35);
            this.btnPedSIPElimModPartidas.TabIndex = 19;
            this.btnPedSIPElimModPartidas.Text = "Eliminar o Modificar Partidas";
            this.btnPedSIPElimModPartidas.UseVisualStyleBackColor = true;
            this.btnPedSIPElimModPartidas.Click += new System.EventHandler(this.btnPedSIPElimModPartidas_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label70);
            this.groupBox4.Controls.Add(this.lblPedSIPYaImpreso);
            this.groupBox4.Controls.Add(this.txtPedSIPCotizacion);
            this.groupBox4.Controls.Add(this.txtPedSIPRemitido);
            this.groupBox4.Controls.Add(this.txtPedSIPConsignado);
            this.groupBox4.Controls.Add(this.txtPedSIPObservaciones);
            this.groupBox4.Controls.Add(this.label26);
            this.groupBox4.Controls.Add(this.label25);
            this.groupBox4.Controls.Add(this.label24);
            this.groupBox4.Location = new System.Drawing.Point(12, 213);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Size = new System.Drawing.Size(978, 249);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Detalles del Pedido";
            // 
            // lblPedSIPYaImpreso
            // 
            this.lblPedSIPYaImpreso.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPedSIPYaImpreso.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPedSIPYaImpreso.Location = new System.Drawing.Point(9, 181);
            this.lblPedSIPYaImpreso.Name = "lblPedSIPYaImpreso";
            this.lblPedSIPYaImpreso.Size = new System.Drawing.Size(122, 58);
            this.lblPedSIPYaImpreso.TabIndex = 12;
            this.lblPedSIPYaImpreso.Text = "Previamente Impreso";
            // 
            // txtPedSIPRemitido
            // 
            this.txtPedSIPRemitido.Location = new System.Drawing.Point(140, 31);
            this.txtPedSIPRemitido.MaxLength = 255;
            this.txtPedSIPRemitido.Name = "txtPedSIPRemitido";
            this.txtPedSIPRemitido.OnlyUpperCase = false;
            this.txtPedSIPRemitido.Size = new System.Drawing.Size(638, 22);
            this.txtPedSIPRemitido.TabIndex = 9;
            // 
            // txtPedSIPConsignado
            // 
            this.txtPedSIPConsignado.Location = new System.Drawing.Point(140, 69);
            this.txtPedSIPConsignado.MaxLength = 255;
            this.txtPedSIPConsignado.Name = "txtPedSIPConsignado";
            this.txtPedSIPConsignado.OnlyUpperCase = false;
            this.txtPedSIPConsignado.Size = new System.Drawing.Size(638, 22);
            this.txtPedSIPConsignado.TabIndex = 10;
            // 
            // txtPedSIPObservaciones
            // 
            this.txtPedSIPObservaciones.AutoTABOnKeyDown = false;
            this.txtPedSIPObservaciones.AutoTABOnKeyUp = false;
            this.txtPedSIPObservaciones.Location = new System.Drawing.Point(140, 106);
            this.txtPedSIPObservaciones.MaxLength = 1000;
            this.txtPedSIPObservaciones.Multiline = true;
            this.txtPedSIPObservaciones.Name = "txtPedSIPObservaciones";
            this.txtPedSIPObservaciones.OnlyUpperCase = false;
            this.txtPedSIPObservaciones.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPedSIPObservaciones.Size = new System.Drawing.Size(638, 124);
            this.txtPedSIPObservaciones.TabIndex = 11;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(9, 118);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(107, 17);
            this.label26.TabIndex = 3;
            this.label26.Text = "Observaciones:";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(9, 80);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(87, 17);
            this.label25.TabIndex = 2;
            this.label25.Text = "Consignado:";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(9, 41);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(67, 17);
            this.label24.TabIndex = 1;
            this.label24.Text = "Remitido:";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // btnPedSIPImprimir
            // 
            this.btnPedSIPImprimir.Location = new System.Drawing.Point(13, 468);
            this.btnPedSIPImprimir.Name = "btnPedSIPImprimir";
            this.btnPedSIPImprimir.Size = new System.Drawing.Size(117, 35);
            this.btnPedSIPImprimir.TabIndex = 21;
            this.btnPedSIPImprimir.Text = "Ver Pedido";
            this.btnPedSIPImprimir.UseVisualStyleBackColor = true;
            this.btnPedSIPImprimir.Click += new System.EventHandler(this.btnPedSIPImprimir_Click);
            // 
            // btnPedSIPDocumentos
            // 
            this.btnPedSIPDocumentos.Location = new System.Drawing.Point(136, 468);
            this.btnPedSIPDocumentos.Name = "btnPedSIPDocumentos";
            this.btnPedSIPDocumentos.Size = new System.Drawing.Size(117, 35);
            this.btnPedSIPDocumentos.TabIndex = 22;
            this.btnPedSIPDocumentos.Text = "Documentos";
            this.btnPedSIPDocumentos.UseVisualStyleBackColor = true;
            this.btnPedSIPDocumentos.Click += new System.EventHandler(this.btnPedSIPDocumentos_Click);
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Location = new System.Drawing.Point(787, 34);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(85, 17);
            this.label70.TabIndex = 23;
            this.label70.Text = "# Cotización";
            // 
            // txtPedSIPCotizacion
            // 
            this.txtPedSIPCotizacion.Location = new System.Drawing.Point(874, 31);
            this.txtPedSIPCotizacion.MaxLength = 255;
            this.txtPedSIPCotizacion.Name = "txtPedSIPCotizacion";
            this.txtPedSIPCotizacion.OnlyUpperCase = false;
            this.txtPedSIPCotizacion.Size = new System.Drawing.Size(97, 22);
            this.txtPedSIPCotizacion.TabIndex = 24;
            // 
            // frmNuevoPedido
            // 
            this.ClientSize = new System.Drawing.Size(1017, 517);
            this.Controls.Add(this.btnPedSIPDocumentos);
            this.Controls.Add(this.btnPedSIPImprimir);
            this.Controls.Add(this.btnPedSIPAgregarPartidas);
            this.Controls.Add(this.btnPedSIPElimModPartidas);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmNuevoPedido";
            this.Text = "Nuevo pedido especial";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmNuevoPedido_FormClosing);
            this.Load += new System.EventHandler(this.frmNuevoPedido_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmbPedSIPMetodoPago;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.ComboBox cmbPedSIPUsoCFDI;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.ComboBox cmbPedSIPFormaPago;
        private System.Windows.Forms.Label label42;
        private UserControls.TextBoxEx txtPedSIPOc;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button btnPedSIPAgregarPartidas;
        private System.Windows.Forms.Button btnPedSIPElimModPartidas;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblPedSIPYaImpreso;
        private UserControls.TextBoxEx txtPedSIPRemitido;
        private UserControls.TextBoxEx txtPedSIPConsignado;
        private UserControls.TextBoxEx txtPedSIPObservaciones;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private UserControls.TextBoxEx txtPedSIPFecha;
        private UserControls.TextBoxEx txtPedSIPAgente;
        private UserControls.TextBoxEx txtPedSIPCliente;
        private UserControls.TextBoxEx txtPedSIPTerminos;
        private UserControls.TextBoxEx txtPedSIPNo;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button btnPedSIPImprimir;
        private System.Windows.Forms.Button btnPedSIPDocumentos;
        private System.Windows.Forms.Label label70;
        private UserControls.TextBoxEx txtPedSIPCotizacion;
    }
}