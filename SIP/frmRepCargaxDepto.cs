using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIP.Utiles;
using ulp_bl.Reportes;

namespace SIP
{
    public partial class frmRepCargaxDepto : Form
    {
        DataTable dataTableDeptos = new DataTable();
        private BackgroundWorker bgwDeptos = new BackgroundWorker();
        private BackgroundWorker bgwReporte;
        Precarga precargaDeptos;
        
        public frmRepCargaxDepto()
        {
            InitializeComponent();
            precargaDeptos = new Precarga(this);
        }

        private void frmRepCargaxDepto_Load(object sender, EventArgs e)
        {
            this.Show();
            precargaDeptos.MostrarEspera();
            precargaDeptos.AsignastatusProceso("     Cargando ...");            
            bgwDeptos.DoWork += bgwDeptos_DoWork;
            bgwDeptos.RunWorkerCompleted += bgwDeptos_RunWorkerCompleted;
            bgwDeptos.RunWorkerAsync();
        }

        void bgwDeptos_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            cmbDepartamentos.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDepartamentos.DisplayMember = "DEPARTAMENTO";
            cmbDepartamentos.ValueMember = "DEPARTAMENTO";
            cmbDepartamentos.DataSource = dataTableDeptos;
            precargaDeptos.RemoverEspera();
        }

        void bgwDeptos_DoWork(object sender, DoWorkEventArgs e)
        {
            
            dataTableDeptos = RepCargaxDepto.RegresaDepartamentos();            
        }

        private void btnEmitir_Click(object sender, EventArgs e)
        {
            bgwReporte = new BackgroundWorker();
            precargaDeptos.MostrarEspera();
            precargaDeptos.AsignastatusProceso("Generando...");
            bgwReporte.DoWork += bgwReporte_DoWork;
            bgwReporte.RunWorkerCompleted += bgwReporte_RunWorkerCompleted;
            bgwReporte.RunWorkerAsync(cmbDepartamentos.SelectedValue);
        }

        void bgwReporte_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            precargaDeptos.RemoverEspera();
            bgwReporte.Dispose();
        }

        void bgwReporte_DoWork(object sender, DoWorkEventArgs e)
        {
            string departamentoAConsultar = e.Argument.ToString();

            string archivoTemporal = System.IO.Path.GetTempFileName().Replace(".tmp", ".xls");

            DataTable dataTableEdoCtaXDepto = RepCargaxDepto.RegresaEstadoCuentaXDepto(departamentoAConsultar);
            precargaDeptos.AsignastatusProceso("Generando archivo de Excel...");

            if (departamentoAConsultar == "ESTAMPADO")
                RepCargaxDepto.GeneraArchivoExcelEstampado(archivoTemporal, dataTableEdoCtaXDepto);
            else if (departamentoAConsultar == "EMPAQUE")
                RepCargaxDepto.GeneraArchivoExcelEmpaque(archivoTemporal, dataTableEdoCtaXDepto, departamentoAConsultar);
            else if (departamentoAConsultar == "EMBARQUE")
                RepCargaxDepto.GeneraArchivoExcelEmbarque(archivoTemporal, dataTableEdoCtaXDepto, departamentoAConsultar);
            else
            RepCargaxDepto.GeneraArchivoExcel(archivoTemporal, dataTableEdoCtaXDepto, departamentoAConsultar);

            

            //System.Diagnostics.Process.Start(archivoTemporal);
            FuncionalidadesFormularios.MostrarExcel(archivoTemporal);
        }
    }
}
