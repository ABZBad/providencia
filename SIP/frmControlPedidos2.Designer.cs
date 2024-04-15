namespace SIP
{
    partial class frmControlPedidos2
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnReporte = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.treeViewProcesos = new System.Windows.Forms.TreeView();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusStripLblUsuario = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripSeparator1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripLblArea = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripSeparator2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripLblFecha = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSelectorFlujo = new System.Windows.Forms.Label();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.Seleccion = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Marcar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.MarcarHabilitado = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.lblHistorico = new System.Windows.Forms.Label();
            this.dgvHistorico = new System.Windows.Forms.DataGridView();
            this.pnlObservaciones = new System.Windows.Forms.Panel();
            this.btnCerrarObservaciones = new System.Windows.Forms.Button();
            this.lstObservaciones = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listNotificaciones = new System.Windows.Forms.ListView();
            this.Id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Modulo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Notificacion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnImprimirNotificaciones = new System.Windows.Forms.Button();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorico)).BeginInit();
            this.pnlObservaciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(13, 9);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(410, 31);
            this.lblTitulo.TabIndex = 3;
            this.lblTitulo.Text = "CONTROL ADMINISTRATIVO";
            // 
            // btnReporte
            // 
            this.btnReporte.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReporte.Location = new System.Drawing.Point(1557, 44);
            this.btnReporte.Margin = new System.Windows.Forms.Padding(4);
            this.btnReporte.Name = "btnReporte";
            this.btnReporte.Size = new System.Drawing.Size(91, 28);
            this.btnReporte.TabIndex = 16;
            this.btnReporte.Text = "Reporte";
            this.btnReporte.UseVisualStyleBackColor = true;
            this.btnReporte.Visible = false;
            this.btnReporte.Click += new System.EventHandler(this.btnReporte_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BackgroundImage = global::SIP.Properties.Resources.refresh;
            this.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRefresh.Location = new System.Drawing.Point(1656, 44);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(29, 28);
            this.btnRefresh.TabIndex = 15;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // treeViewProcesos
            // 
            this.treeViewProcesos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeViewProcesos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeViewProcesos.HideSelection = false;
            this.treeViewProcesos.Location = new System.Drawing.Point(13, 76);
            this.treeViewProcesos.Margin = new System.Windows.Forms.Padding(4);
            this.treeViewProcesos.Name = "treeViewProcesos";
            this.treeViewProcesos.Size = new System.Drawing.Size(391, 362);
            this.treeViewProcesos.TabIndex = 7;
            this.treeViewProcesos.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewProcesos_AfterSelect);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusStripLblUsuario,
            this.statusStripSeparator1,
            this.statusStripLblArea,
            this.statusStripSeparator2,
            this.statusStripLblFecha,
            this.toolStripStatusLabel2});
            this.statusStrip.Location = new System.Drawing.Point(0, 789);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip.Size = new System.Drawing.Size(1705, 25);
            this.statusStrip.TabIndex = 8;
            this.statusStrip.Text = "statusStrip1";
            // 
            // statusStripLblUsuario
            // 
            this.statusStripLblUsuario.Name = "statusStripLblUsuario";
            this.statusStripLblUsuario.Size = new System.Drawing.Size(60, 20);
            this.statusStripLblUsuario.Text = "usuario:";
            // 
            // statusStripSeparator1
            // 
            this.statusStripSeparator1.Name = "statusStripSeparator1";
            this.statusStripSeparator1.Size = new System.Drawing.Size(13, 20);
            this.statusStripSeparator1.Text = "|";
            // 
            // statusStripLblArea
            // 
            this.statusStripLblArea.Name = "statusStripLblArea";
            this.statusStripLblArea.Size = new System.Drawing.Size(43, 20);
            this.statusStripLblArea.Text = "Área:";
            // 
            // statusStripSeparator2
            // 
            this.statusStripSeparator2.Name = "statusStripSeparator2";
            this.statusStripSeparator2.Size = new System.Drawing.Size(13, 20);
            this.statusStripSeparator2.Text = "|";
            // 
            // statusStripLblFecha
            // 
            this.statusStripLblFecha.Name = "statusStripLblFecha";
            this.statusStripLblFecha.Size = new System.Drawing.Size(50, 20);
            this.statusStripLblFecha.Text = "Fecha:";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(13, 20);
            this.toolStripStatusLabel2.Text = "|";
            // 
            // lblSelectorFlujo
            // 
            this.lblSelectorFlujo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSelectorFlujo.AutoSize = true;
            this.lblSelectorFlujo.Location = new System.Drawing.Point(16, 55);
            this.lblSelectorFlujo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelectorFlujo.Name = "lblSelectorFlujo";
            this.lblSelectorFlujo.Size = new System.Drawing.Size(114, 17);
            this.lblSelectorFlujo.TabIndex = 9;
            this.lblSelectorFlujo.Text = "Selector de Flujo";
            // 
            // dgvResult
            // 
            this.dgvResult.AllowUserToAddRows = false;
            this.dgvResult.AllowUserToDeleteRows = false;
            this.dgvResult.AllowUserToResizeRows = false;
            this.dgvResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Seleccion,
            this.Marcar,
            this.MarcarHabilitado});
            this.dgvResult.Location = new System.Drawing.Point(419, 76);
            this.dgvResult.Margin = new System.Windows.Forms.Padding(4);
            this.dgvResult.MultiSelect = false;
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResult.Size = new System.Drawing.Size(1273, 565);
            this.dgvResult.TabIndex = 10;
            this.dgvResult.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResult_CellContentClick);
            this.dgvResult.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResult_CellDoubleClick);
            this.dgvResult.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvResult_MouseClick);
            // 
            // Seleccion
            // 
            this.Seleccion.DataPropertyName = "Seleccion";
            this.Seleccion.FalseValue = "0";
            this.Seleccion.HeaderText = "";
            this.Seleccion.IndeterminateValue = "0";
            this.Seleccion.Name = "Seleccion";
            this.Seleccion.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Seleccion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Seleccion.TrueValue = "1";
            this.Seleccion.Visible = false;
            this.Seleccion.Width = 25;
            // 
            // Marcar
            // 
            this.Marcar.DataPropertyName = "Marcar";
            this.Marcar.HeaderText = "Marcar";
            this.Marcar.Name = "Marcar";
            this.Marcar.Visible = false;
            // 
            // MarcarHabilitado
            // 
            this.MarcarHabilitado.DataPropertyName = "MarcarHabilitado";
            this.MarcarHabilitado.HeaderText = "MarcarHabilitado";
            this.MarcarHabilitado.Name = "MarcarHabilitado";
            this.MarcarHabilitado.Visible = false;
            // 
            // lblHistorico
            // 
            this.lblHistorico.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHistorico.AutoSize = true;
            this.lblHistorico.Location = new System.Drawing.Point(416, 645);
            this.lblHistorico.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHistorico.Name = "lblHistorico";
            this.lblHistorico.Size = new System.Drawing.Size(67, 17);
            this.lblHistorico.TabIndex = 11;
            this.lblHistorico.Text = "Historico:";
            // 
            // dgvHistorico
            // 
            this.dgvHistorico.AllowUserToAddRows = false;
            this.dgvHistorico.AllowUserToDeleteRows = false;
            this.dgvHistorico.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvHistorico.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvHistorico.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHistorico.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvHistorico.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvHistorico.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvHistorico.Location = new System.Drawing.Point(419, 666);
            this.dgvHistorico.Margin = new System.Windows.Forms.Padding(4);
            this.dgvHistorico.Name = "dgvHistorico";
            this.dgvHistorico.ReadOnly = true;
            this.dgvHistorico.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvHistorico.Size = new System.Drawing.Size(1266, 119);
            this.dgvHistorico.TabIndex = 12;
            // 
            // pnlObservaciones
            // 
            this.pnlObservaciones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlObservaciones.Controls.Add(this.btnCerrarObservaciones);
            this.pnlObservaciones.Controls.Add(this.lstObservaciones);
            this.pnlObservaciones.Location = new System.Drawing.Point(529, 97);
            this.pnlObservaciones.Margin = new System.Windows.Forms.Padding(4);
            this.pnlObservaciones.Name = "pnlObservaciones";
            this.pnlObservaciones.Size = new System.Drawing.Size(1045, 313);
            this.pnlObservaciones.TabIndex = 13;
            this.pnlObservaciones.Visible = false;
            // 
            // btnCerrarObservaciones
            // 
            this.btnCerrarObservaciones.Location = new System.Drawing.Point(891, 272);
            this.btnCerrarObservaciones.Margin = new System.Windows.Forms.Padding(4);
            this.btnCerrarObservaciones.Name = "btnCerrarObservaciones";
            this.btnCerrarObservaciones.Size = new System.Drawing.Size(67, 28);
            this.btnCerrarObservaciones.TabIndex = 7;
            this.btnCerrarObservaciones.Text = "Cerrar";
            this.btnCerrarObservaciones.UseVisualStyleBackColor = true;
            this.btnCerrarObservaciones.Click += new System.EventHandler(this.btnCerrarObservaciones_Click);
            // 
            // lstObservaciones
            // 
            this.lstObservaciones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstObservaciones.FormattingEnabled = true;
            this.lstObservaciones.HorizontalScrollbar = true;
            this.lstObservaciones.ItemHeight = 16;
            this.lstObservaciones.Location = new System.Drawing.Point(11, 4);
            this.lstObservaciones.Margin = new System.Windows.Forms.Padding(4);
            this.lstObservaciones.Name = "lstObservaciones";
            this.lstObservaciones.Size = new System.Drawing.Size(1029, 260);
            this.lstObservaciones.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 442);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 17);
            this.label1.TabIndex = 18;
            this.label1.Text = "Notificaciones";
            // 
            // listNotificaciones
            // 
            this.listNotificaciones.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.listNotificaciones.CheckBoxes = true;
            this.listNotificaciones.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Id,
            this.Modulo,
            this.Notificacion});
            this.listNotificaciones.Location = new System.Drawing.Point(12, 462);
            this.listNotificaciones.Name = "listNotificaciones";
            this.listNotificaciones.Size = new System.Drawing.Size(392, 279);
            this.listNotificaciones.TabIndex = 19;
            this.listNotificaciones.UseCompatibleStateImageBehavior = false;
            this.listNotificaciones.View = System.Windows.Forms.View.Details;
            this.listNotificaciones.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listNotificaciones_ItemChecked);
            // 
            // Id
            // 
            this.Id.Text = "Id";
            // 
            // Modulo
            // 
            this.Modulo.Text = "Módulo";
            // 
            // Notificacion
            // 
            this.Notificacion.Text = "Notificación";
            // 
            // btnImprimirNotificaciones
            // 
            this.btnImprimirNotificaciones.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImprimirNotificaciones.Location = new System.Drawing.Point(13, 747);
            this.btnImprimirNotificaciones.Name = "btnImprimirNotificaciones";
            this.btnImprimirNotificaciones.Size = new System.Drawing.Size(391, 38);
            this.btnImprimirNotificaciones.TabIndex = 21;
            this.btnImprimirNotificaciones.Text = "Imprimir Notificaciones";
            this.btnImprimirNotificaciones.UseVisualStyleBackColor = true;
            this.btnImprimirNotificaciones.Click += new System.EventHandler(this.btnImprimirNotificaciones_Click);
            // 
            // frmControlPedidos2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1705, 814);
            this.Controls.Add(this.btnImprimirNotificaciones);
            this.Controls.Add(this.listNotificaciones);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnReporte);
            this.Controls.Add(this.pnlObservaciones);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dgvHistorico);
            this.Controls.Add(this.lblHistorico);
            this.Controls.Add(this.dgvResult);
            this.Controls.Add(this.lblSelectorFlujo);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.treeViewProcesos);
            this.Controls.Add(this.lblTitulo);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmControlPedidos2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Control de Flujos";
            this.Load += new System.EventHandler(this.frmControlPedidos2_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorico)).EndInit();
            this.pnlObservaciones.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnReporte;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TreeView treeViewProcesos;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusStripLblUsuario;
        private System.Windows.Forms.ToolStripStatusLabel statusStripSeparator1;
        private System.Windows.Forms.ToolStripStatusLabel statusStripLblArea;
        private System.Windows.Forms.ToolStripStatusLabel statusStripSeparator2;
        private System.Windows.Forms.ToolStripStatusLabel statusStripLblFecha;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Label lblSelectorFlujo;
        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.Label lblHistorico;
        private System.Windows.Forms.DataGridView dgvHistorico;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Seleccion;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Marcar;
        private System.Windows.Forms.DataGridViewCheckBoxColumn MarcarHabilitado;
        private System.Windows.Forms.Panel pnlObservaciones;
        private System.Windows.Forms.Button btnCerrarObservaciones;
        private System.Windows.Forms.ListBox lstObservaciones;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listNotificaciones;
        private System.Windows.Forms.ColumnHeader Id;
        private System.Windows.Forms.ColumnHeader Modulo;
        private System.Windows.Forms.ColumnHeader Notificacion;
        private System.Windows.Forms.Button btnImprimirNotificaciones;
    }
}