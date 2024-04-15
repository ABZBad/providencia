namespace SIP
{
    partial class frmEliminarPartidas
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
            this.gpBoxPrincipal = new System.Windows.Forms.GroupBox();
            this.gpBoxProceso = new System.Windows.Forms.GroupBox();
            this.btnParamProcesoCancelar = new System.Windows.Forms.Button();
            this.btnParamProcesoAceptar = new System.Windows.Forms.Button();
            this.txtParamPartida = new System.Windows.Forms.TextBox();
            this.btnAgregarProceso = new System.Windows.Forms.Button();
            this.btnGuardarProceso = new System.Windows.Forms.Button();
            this.dgViewProcesos = new System.Windows.Forms.DataGridView();
            this.btnEliminarPartidaYProcesos = new System.Windows.Forms.Button();
            this.dgViewPartidas = new System.Windows.Forms.DataGridView();
            this.Pedido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Modelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tallas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AGRUPADOR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRENDAS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DESCUENTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRECIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IMPORTE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRE_PROCESOS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrecioLista = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.scape1 = new SIP.UserControls.Scape();
            this.CMT_PROCESO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CMT_CMMT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CMT_COMO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CMT_DONDE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CMT_PRE_PROCESO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CMT_AGRUPADOR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CMT_INDX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CMT_MODELO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CMT_CANTIDAD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CMT_MAQUILERO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gpBoxPrincipal.SuspendLayout();
            this.gpBoxProceso.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewProcesos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewPartidas)).BeginInit();
            this.SuspendLayout();
            // 
            // gpBoxPrincipal
            // 
            this.gpBoxPrincipal.Controls.Add(this.gpBoxProceso);
            this.gpBoxPrincipal.Controls.Add(this.btnAgregarProceso);
            this.gpBoxPrincipal.Controls.Add(this.btnGuardarProceso);
            this.gpBoxPrincipal.Controls.Add(this.dgViewProcesos);
            this.gpBoxPrincipal.Controls.Add(this.btnEliminarPartidaYProcesos);
            this.gpBoxPrincipal.Controls.Add(this.dgViewPartidas);
            this.gpBoxPrincipal.Location = new System.Drawing.Point(16, 15);
            this.gpBoxPrincipal.Margin = new System.Windows.Forms.Padding(4);
            this.gpBoxPrincipal.Name = "gpBoxPrincipal";
            this.gpBoxPrincipal.Padding = new System.Windows.Forms.Padding(4);
            this.gpBoxPrincipal.Size = new System.Drawing.Size(1116, 586);
            this.gpBoxPrincipal.TabIndex = 5;
            this.gpBoxPrincipal.TabStop = false;
            // 
            // gpBoxProceso
            // 
            this.gpBoxProceso.Controls.Add(this.btnParamProcesoCancelar);
            this.gpBoxProceso.Controls.Add(this.btnParamProcesoAceptar);
            this.gpBoxProceso.Controls.Add(this.txtParamPartida);
            this.gpBoxProceso.Location = new System.Drawing.Point(464, 225);
            this.gpBoxProceso.Margin = new System.Windows.Forms.Padding(4);
            this.gpBoxProceso.Name = "gpBoxProceso";
            this.gpBoxProceso.Padding = new System.Windows.Forms.Padding(4);
            this.gpBoxProceso.Size = new System.Drawing.Size(267, 108);
            this.gpBoxProceso.TabIndex = 6;
            this.gpBoxProceso.TabStop = false;
            this.gpBoxProceso.Text = "Introduzca la letra del nuevo proceso";
            this.gpBoxProceso.Visible = false;
            // 
            // btnParamProcesoCancelar
            // 
            this.btnParamProcesoCancelar.Location = new System.Drawing.Point(144, 58);
            this.btnParamProcesoCancelar.Margin = new System.Windows.Forms.Padding(4);
            this.btnParamProcesoCancelar.Name = "btnParamProcesoCancelar";
            this.btnParamProcesoCancelar.Size = new System.Drawing.Size(96, 28);
            this.btnParamProcesoCancelar.TabIndex = 11;
            this.btnParamProcesoCancelar.Text = "Cancelar";
            this.btnParamProcesoCancelar.UseVisualStyleBackColor = true;
            this.btnParamProcesoCancelar.Click += new System.EventHandler(this.btnParamProcesoCancelar_Click);
            // 
            // btnParamProcesoAceptar
            // 
            this.btnParamProcesoAceptar.Location = new System.Drawing.Point(40, 58);
            this.btnParamProcesoAceptar.Margin = new System.Windows.Forms.Padding(4);
            this.btnParamProcesoAceptar.Name = "btnParamProcesoAceptar";
            this.btnParamProcesoAceptar.Size = new System.Drawing.Size(96, 28);
            this.btnParamProcesoAceptar.TabIndex = 10;
            this.btnParamProcesoAceptar.Text = "Aceptar";
            this.btnParamProcesoAceptar.UseVisualStyleBackColor = true;
            this.btnParamProcesoAceptar.Click += new System.EventHandler(this.btnParamProcesoAceptar_Click);
            // 
            // txtParamPartida
            // 
            this.txtParamPartida.Location = new System.Drawing.Point(24, 23);
            this.txtParamPartida.Margin = new System.Windows.Forms.Padding(4);
            this.txtParamPartida.MaxLength = 1;
            this.txtParamPartida.Name = "txtParamPartida";
            this.txtParamPartida.Size = new System.Drawing.Size(215, 22);
            this.txtParamPartida.TabIndex = 0;
            // 
            // btnAgregarProceso
            // 
            this.btnAgregarProceso.Location = new System.Drawing.Point(476, 544);
            this.btnAgregarProceso.Margin = new System.Windows.Forms.Padding(4);
            this.btnAgregarProceso.Name = "btnAgregarProceso";
            this.btnAgregarProceso.Size = new System.Drawing.Size(125, 28);
            this.btnAgregarProceso.TabIndex = 9;
            this.btnAgregarProceso.Text = "Agregar Proceso";
            this.btnAgregarProceso.UseVisualStyleBackColor = true;
            this.btnAgregarProceso.Click += new System.EventHandler(this.btnAgregarProceso_Click);
            // 
            // btnGuardarProceso
            // 
            this.btnGuardarProceso.Location = new System.Drawing.Point(604, 544);
            this.btnGuardarProceso.Margin = new System.Windows.Forms.Padding(4);
            this.btnGuardarProceso.Name = "btnGuardarProceso";
            this.btnGuardarProceso.Size = new System.Drawing.Size(127, 28);
            this.btnGuardarProceso.TabIndex = 8;
            this.btnGuardarProceso.Text = "Guardar Proceso";
            this.btnGuardarProceso.UseVisualStyleBackColor = true;
            this.btnGuardarProceso.Click += new System.EventHandler(this.btnGuardarProceso_Click);
            // 
            // dgViewProcesos
            // 
            this.dgViewProcesos.AllowUserToAddRows = false;
            this.dgViewProcesos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgViewProcesos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CMT_PROCESO,
            this.CMT_CMMT,
            this.CMT_COMO,
            this.CMT_DONDE,
            this.CMT_PRE_PROCESO,
            this.CMT_AGRUPADOR,
            this.CMT_INDX,
            this.CMT_MODELO,
            this.CMT_CANTIDAD,
            this.CMT_MAQUILERO});
            this.dgViewProcesos.Location = new System.Drawing.Point(20, 324);
            this.dgViewProcesos.Margin = new System.Windows.Forms.Padding(4);
            this.dgViewProcesos.MultiSelect = false;
            this.dgViewProcesos.Name = "dgViewProcesos";
            this.dgViewProcesos.Size = new System.Drawing.Size(1088, 213);
            this.dgViewProcesos.TabIndex = 7;
            this.dgViewProcesos.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgViewProcesos_CellBeginEdit);
            this.dgViewProcesos.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgViewProcesos_CellEndEdit);
            this.dgViewProcesos.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgViewProcesos_CellValidated);
            this.dgViewProcesos.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgViewProcesos_CellValidating);
            this.dgViewProcesos.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dgViewProcesos_RowsRemoved);
            this.dgViewProcesos.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgViewProcesos_UserDeletedRow);
            this.dgViewProcesos.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgViewProcesos_UserDeletingRow);
            this.dgViewProcesos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgViewProcesos_KeyDown);
            this.dgViewProcesos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgViewProcesos_KeyPress);
            // 
            // btnEliminarPartidaYProcesos
            // 
            this.btnEliminarPartidaYProcesos.Location = new System.Drawing.Point(473, 284);
            this.btnEliminarPartidaYProcesos.Margin = new System.Windows.Forms.Padding(4);
            this.btnEliminarPartidaYProcesos.Name = "btnEliminarPartidaYProcesos";
            this.btnEliminarPartidaYProcesos.Size = new System.Drawing.Size(243, 28);
            this.btnEliminarPartidaYProcesos.TabIndex = 6;
            this.btnEliminarPartidaYProcesos.Text = "Eliminar la partida y sus procesos";
            this.btnEliminarPartidaYProcesos.UseVisualStyleBackColor = true;
            this.btnEliminarPartidaYProcesos.Click += new System.EventHandler(this.btnEliminarPartidaYProcesos_Click);
            // 
            // dgViewPartidas
            // 
            this.dgViewPartidas.AllowUserToAddRows = false;
            this.dgViewPartidas.AllowUserToDeleteRows = false;
            this.dgViewPartidas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgViewPartidas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Pedido,
            this.Modelo,
            this.Descripcion,
            this.Tallas,
            this.AGRUPADOR,
            this.PRENDAS,
            this.DESCUENTO,
            this.PRECIO,
            this.IMPORTE,
            this.PRE_PROCESOS,
            this.colPrecioLista});
            this.dgViewPartidas.Location = new System.Drawing.Point(20, 31);
            this.dgViewPartidas.Margin = new System.Windows.Forms.Padding(4);
            this.dgViewPartidas.MultiSelect = false;
            this.dgViewPartidas.Name = "dgViewPartidas";
            this.dgViewPartidas.ReadOnly = true;
            this.dgViewPartidas.Size = new System.Drawing.Size(1088, 246);
            this.dgViewPartidas.TabIndex = 5;
            this.dgViewPartidas.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgViewPartidas_CellDoubleClick);
            this.dgViewPartidas.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgViewPartidas_CellEnter);
            // 
            // Pedido
            // 
            this.Pedido.DataPropertyName = "PEDIDO";
            this.Pedido.HeaderText = "Pedido";
            this.Pedido.Name = "Pedido";
            this.Pedido.ReadOnly = true;
            this.Pedido.Width = 70;
            // 
            // Modelo
            // 
            this.Modelo.DataPropertyName = "MODELO";
            this.Modelo.HeaderText = "Modelo";
            this.Modelo.Name = "Modelo";
            this.Modelo.ReadOnly = true;
            // 
            // Descripcion
            // 
            this.Descripcion.DataPropertyName = "DESCRIPCION";
            this.Descripcion.HeaderText = "Descripción";
            this.Descripcion.Name = "Descripcion";
            this.Descripcion.ReadOnly = true;
            this.Descripcion.Visible = false;
            // 
            // Tallas
            // 
            this.Tallas.DataPropertyName = "TALLAS";
            this.Tallas.HeaderText = "Talla";
            this.Tallas.Name = "Tallas";
            this.Tallas.ReadOnly = true;
            this.Tallas.Width = 300;
            // 
            // AGRUPADOR
            // 
            this.AGRUPADOR.DataPropertyName = "AGRUPADOR";
            this.AGRUPADOR.HeaderText = "Agrupador";
            this.AGRUPADOR.Name = "AGRUPADOR";
            this.AGRUPADOR.ReadOnly = true;
            this.AGRUPADOR.Visible = false;
            // 
            // PRENDAS
            // 
            this.PRENDAS.DataPropertyName = "PRENDAS";
            this.PRENDAS.HeaderText = "Prendas";
            this.PRENDAS.Name = "PRENDAS";
            this.PRENDAS.ReadOnly = true;
            this.PRENDAS.Width = 70;
            // 
            // DESCUENTO
            // 
            this.DESCUENTO.DataPropertyName = "DESCUENTO";
            dataGridViewCellStyle2.Format = "P2";
            dataGridViewCellStyle2.NullValue = null;
            this.DESCUENTO.DefaultCellStyle = dataGridViewCellStyle2;
            this.DESCUENTO.HeaderText = "Descuento";
            this.DESCUENTO.Name = "DESCUENTO";
            this.DESCUENTO.ReadOnly = true;
            this.DESCUENTO.Visible = false;
            // 
            // PRECIO
            // 
            this.PRECIO.DataPropertyName = "PRECIO";
            dataGridViewCellStyle3.Format = "C2";
            dataGridViewCellStyle3.NullValue = null;
            this.PRECIO.DefaultCellStyle = dataGridViewCellStyle3;
            this.PRECIO.HeaderText = "Precio";
            this.PRECIO.Name = "PRECIO";
            this.PRECIO.ReadOnly = true;
            // 
            // IMPORTE
            // 
            this.IMPORTE.DataPropertyName = "IMPORTE";
            this.IMPORTE.HeaderText = "Importe";
            this.IMPORTE.Name = "IMPORTE";
            this.IMPORTE.ReadOnly = true;
            this.IMPORTE.Visible = false;
            // 
            // PRE_PROCESOS
            // 
            this.PRE_PROCESOS.DataPropertyName = "PRE_PROCESOS";
            this.PRE_PROCESOS.HeaderText = "PRE_PROCESOS";
            this.PRE_PROCESOS.Name = "PRE_PROCESOS";
            this.PRE_PROCESOS.ReadOnly = true;
            this.PRE_PROCESOS.Visible = false;
            // 
            // colPrecioLista
            // 
            this.colPrecioLista.DataPropertyName = "PRECIO_LISTA";
            this.colPrecioLista.HeaderText = "PRECIO_LISTA";
            this.colPrecioLista.Name = "colPrecioLista";
            this.colPrecioLista.ReadOnly = true;
            this.colPrecioLista.Visible = false;
            // 
            // scape1
            // 
            this.scape1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.scape1.Form = this;
            // 
            // CMT_PROCESO
            // 
            this.CMT_PROCESO.DataPropertyName = "CMT_PROCESO";
            this.CMT_PROCESO.HeaderText = "Proceso";
            this.CMT_PROCESO.Name = "CMT_PROCESO";
            this.CMT_PROCESO.ReadOnly = true;
            this.CMT_PROCESO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CMT_CMMT
            // 
            this.CMT_CMMT.DataPropertyName = "CMT_CMMT";
            this.CMT_CMMT.HeaderText = "Qué";
            this.CMT_CMMT.MaxInputLength = 250;
            this.CMT_CMMT.Name = "CMT_CMMT";
            this.CMT_CMMT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CMT_COMO
            // 
            this.CMT_COMO.DataPropertyName = "CMT_COMO";
            this.CMT_COMO.HeaderText = "Cómo";
            this.CMT_COMO.MaxInputLength = 15;
            this.CMT_COMO.Name = "CMT_COMO";
            this.CMT_COMO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CMT_DONDE
            // 
            this.CMT_DONDE.DataPropertyName = "CMT_DONDE";
            this.CMT_DONDE.HeaderText = "Dónde";
            this.CMT_DONDE.MaxInputLength = 150;
            this.CMT_DONDE.Name = "CMT_DONDE";
            this.CMT_DONDE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CMT_DONDE.Width = 200;
            // 
            // CMT_PRE_PROCESO
            // 
            this.CMT_PRE_PROCESO.DataPropertyName = "CMT_PRE_PROCESO";
            dataGridViewCellStyle1.Format = "C2";
            dataGridViewCellStyle1.NullValue = null;
            this.CMT_PRE_PROCESO.DefaultCellStyle = dataGridViewCellStyle1;
            this.CMT_PRE_PROCESO.HeaderText = "P. Proceso";
            this.CMT_PRE_PROCESO.MaxInputLength = 10;
            this.CMT_PRE_PROCESO.Name = "CMT_PRE_PROCESO";
            this.CMT_PRE_PROCESO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CMT_AGRUPADOR
            // 
            this.CMT_AGRUPADOR.DataPropertyName = "CMT_AGRUPADOR";
            this.CMT_AGRUPADOR.HeaderText = "CMT_AGRUPADOR";
            this.CMT_AGRUPADOR.Name = "CMT_AGRUPADOR";
            this.CMT_AGRUPADOR.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CMT_AGRUPADOR.Visible = false;
            // 
            // CMT_INDX
            // 
            this.CMT_INDX.DataPropertyName = "CMT_INDX";
            this.CMT_INDX.HeaderText = "CMT_INDX";
            this.CMT_INDX.Name = "CMT_INDX";
            this.CMT_INDX.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CMT_INDX.Visible = false;
            // 
            // CMT_MODELO
            // 
            this.CMT_MODELO.DataPropertyName = "CMT_MODELO";
            this.CMT_MODELO.HeaderText = "CMT_MODELO";
            this.CMT_MODELO.Name = "CMT_MODELO";
            this.CMT_MODELO.Visible = false;
            // 
            // CMT_CANTIDAD
            // 
            this.CMT_CANTIDAD.DataPropertyName = "CMT_CANTIDAD";
            this.CMT_CANTIDAD.HeaderText = "CMT_CANTIDAD";
            this.CMT_CANTIDAD.Name = "CMT_CANTIDAD";
            this.CMT_CANTIDAD.Visible = false;
            // 
            // CMT_MAQUILERO
            // 
            this.CMT_MAQUILERO.DataPropertyName = "CMT_MAQUILERO";
            this.CMT_MAQUILERO.HeaderText = "CMT_MAQUILERO";
            this.CMT_MAQUILERO.Name = "CMT_MAQUILERO";
            this.CMT_MAQUILERO.Visible = false;
            // 
            // frmEliminarPartidas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1148, 608);
            this.Controls.Add(this.gpBoxPrincipal);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmEliminarPartidas";
            this.Text = "Eliminar Partidas";
            this.Load += new System.EventHandler(this.frmEliminarPartidas_Load);
            this.gpBoxPrincipal.ResumeLayout(false);
            this.gpBoxProceso.ResumeLayout(false);
            this.gpBoxProceso.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewProcesos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewPartidas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpBoxPrincipal;
        private System.Windows.Forms.Button btnAgregarProceso;
        private System.Windows.Forms.Button btnGuardarProceso;
        private System.Windows.Forms.DataGridView dgViewProcesos;
        private System.Windows.Forms.Button btnEliminarPartidaYProcesos;
        private System.Windows.Forms.DataGridView dgViewPartidas;
        private System.Windows.Forms.GroupBox gpBoxProceso;
        private System.Windows.Forms.Button btnParamProcesoCancelar;
        private System.Windows.Forms.Button btnParamProcesoAceptar;
        private System.Windows.Forms.TextBox txtParamPartida;
        private UserControls.Scape scape1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pedido;
        private System.Windows.Forms.DataGridViewTextBoxColumn Modelo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tallas;
        private System.Windows.Forms.DataGridViewTextBoxColumn AGRUPADOR;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRENDAS;
        private System.Windows.Forms.DataGridViewTextBoxColumn DESCUENTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRECIO;
        private System.Windows.Forms.DataGridViewTextBoxColumn IMPORTE;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRE_PROCESOS;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrecioLista;
        private System.Windows.Forms.DataGridViewTextBoxColumn CMT_PROCESO;
        private System.Windows.Forms.DataGridViewTextBoxColumn CMT_CMMT;
        private System.Windows.Forms.DataGridViewTextBoxColumn CMT_COMO;
        private System.Windows.Forms.DataGridViewTextBoxColumn CMT_DONDE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CMT_PRE_PROCESO;
        private System.Windows.Forms.DataGridViewTextBoxColumn CMT_AGRUPADOR;
        private System.Windows.Forms.DataGridViewTextBoxColumn CMT_INDX;
        private System.Windows.Forms.DataGridViewTextBoxColumn CMT_MODELO;
        private System.Windows.Forms.DataGridViewTextBoxColumn CMT_CANTIDAD;
        private System.Windows.Forms.DataGridViewTextBoxColumn CMT_MAQUILERO;

    }
}