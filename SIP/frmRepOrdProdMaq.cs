﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIP.Utiles;
using ulp_bl;

namespace SIP
{
    public partial class frmRepOrdProdMaq : Form
    {
        #region ATRIBUTOS Y CONSTRUCTORES
        private BackgroundWorker bgw;
        private Precarga precarga;
        private Exception ex;

        public frmRepOrdProdMaq()
        {
            InitializeComponent();
            precarga = new Precarga(this);
        }
        #endregion
        #region EVENTOS
        private void frmRepOrdProdMaq_Load(object sender, EventArgs e)
        {
            this.dtpDesde.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            this.dtpHasta.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
        }
        private void btnContinuar_Click(object sender, EventArgs e)
        {
            bgw = new BackgroundWorker();
            bgw.DoWork += bgw_DoWork;
            bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
            precarga.MostrarEspera();
            bgw.RunWorkerAsync();
        }
        #endregion
        #region WORKERS
        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            precarga.AsignastatusProceso("Generando información...");
            DataTable dtResultado = new DataTable();
            RepOrdProd OrdProd = new RepOrdProd();
            dtResultado = OrdProd.RegresaTablaConProveedor(dtpDesde.Value, dtpHasta.Value, Enumerados.TipoOrdenProduccion.Liberada);
            if (ex == null)
            {
                if (dtResultado != null)
                {
                    if (dtResultado.Rows.Count != 0)
                    {
                        precarga.AsignastatusProceso("Generando archivo de Excel...");
                        //GENERAMOS EL EXCEL
                        string archivoTemporal = System.IO.Path.GetTempFileName().Replace(".tmp", ".xls");
                        RepOrdProd.GeneraArchivoExcelConProveedor(archivoTemporal, dtResultado, dtpDesde.Value, dtpHasta.Value);
                        FuncionalidadesFormularios.MostrarExcel(archivoTemporal);
                    }
                    else
                    {
                        MessageBox.Show("No existen pedidos con este rango de fechas", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("No existen pedidos con este rango de fechas", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            precarga.RemoverEspera();
        }
        #endregion
    }
}
