using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIP.Utiles;
using ulp_bl;

namespace SIP
{
    public partial class frmFiltroConta : Form
    {
        private bool XLS_GENERADO;
        private int CVE_CPTO = 0;
        private BackgroundWorker bgw;
        private Precarga precarga;
        private Enumerados.TipoReporteFiltroConta tipoReporte;
        public frmFiltroConta(Enumerados.TipoReporteFiltroConta TipoReporte)
        {
            InitializeComponent();
            tipoReporte = TipoReporte;
            precarga = new Precarga(this);
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void lblDesde_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void lblHasta_Click(object sender, EventArgs e)
        {

        }

        private void frmFiltroConta_Load(object sender, EventArgs e)
        {
            dtpDesde.Value = DateTime.Now;
            dtpHasta.Value = DateTime.Now;
            switch (tipoReporte)
            {
                case Enumerados.TipoReporteFiltroConta.RecYCosteoDeInvMP_SMO:
                    
                    lblNomRepor.Text = "Reconstrucción y costeo de inventario MP a una fecha (S/MO)";
                    dtpDesde.Visible = false;
                    txtWichOne.Visible = false;
                    lblDesde.Visible = false;
                    break;
                case Enumerados.TipoReporteFiltroConta.RecYCosteoDeInvPP_SMO:
                    lblNomRepor.Text = "Reconstrucción y costeo de inventario PP a una fecha (S/MO)";
                    dtpDesde.Visible = false;
                    txtWichOne.Visible = false;
                    lblDesde.Visible = false;
                    break;
                case Enumerados.TipoReporteFiltroConta.RecYCosteoDeInvPT_SMO:
                    lblNomRepor.Text = "Reconstrucción y costeo de inventario PT a una fecha (S/MO)";
                    dtpDesde.Visible = false;
                    txtWichOne.Visible = false;
                    lblDesde.Visible = false;
                    break;
                case Enumerados.TipoReporteFiltroConta.CostoVendidoEntreFechas_SMO:
                    lblNomRepor.Text = "Costo de lo vendido entre fechas (S/MO)";                    
                    txtWichOne.Visible = false;
                    break;
                case Enumerados.TipoReporteFiltroConta.RecYCosteoDeMermas_SMO:
                    lblNomRepor.Text = "Reconstrucción y costeo de mermas entre fechas (S/MO)";
                    txtWichOne.Visible = false;
                    break;
                case Enumerados.TipoReporteFiltroConta.RecYCosteoDeInvPT_CMO:
                    lblNomRepor.Text = "Reconstrucción y costeo de inventario PT a una fecha (C/MO)";
                    dtpDesde.Visible = false;
                    txtWichOne.Visible = false;
                    lblDesde.Visible = false;
                    break;
                case Enumerados.TipoReporteFiltroConta.CostoVendidoEntreFechas_CMO:
                    lblNomRepor.Text = "Costo de lo vendido entre fechas (C/MO)";
                    txtWichOne.Visible = false;
                    break;
                case Enumerados.TipoReporteFiltroConta.RecYCosteoDeMermas_CMO:
                    lblNomRepor.Text = "Reconstrucción y costeo de mermas entre fechas (C/MO)";
                    txtWichOne.Visible = false;
                    break;
            }
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {

            if (tipoReporte == Enumerados.TipoReporteFiltroConta.RecYCosteoDeMermas_SMO || tipoReporte == Enumerados.TipoReporteFiltroConta.RecYCosteoDeMermas_CMO)
            {
                CVE_CPTO = 0;
                frmInputBox frmInputBoxCVE_CPTO = new frmInputBox(Enumerados.TipoCajaTextoInputBox.Numerica);
                frmInputBoxCVE_CPTO.lblTitulo.Text = "60 = Defectuosos PP, 61 = Promocion VTA MTP\n62 = Promocion VTA INT";
                frmInputBoxCVE_CPTO.Text = "Costeo de mermas";
                if (frmInputBoxCVE_CPTO.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (frmInputBoxCVE_CPTO.NTxtOrden.Text != string.Empty)
                    {
                        CVE_CPTO = int.Parse(frmInputBoxCVE_CPTO.NTxtOrden.Text);
                    }
                    else
                    {
                        MessageBox.Show("Es necesario capturar un número de Pedido.", "Verifique", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                else
                {
                    return;
                }
            }


                    bgw = new BackgroundWorker();
                    bgw.DoWork += bgw_DoWork;
                    bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
                    precarga.MostrarEspera();
                    bgw.RunWorkerAsync();                                
        }

        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            precarga.RemoverEspera();
            //if (XLS_GENERADO)
            //    MessageBox.Show("El Reporte se ha generado exitosamente","Información",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {            
            string archivoTemporal = Path.GetTempFileName().Replace(".tmp", ".xls");

            precarga.AsignastatusProceso("Generando información...");
            switch (tipoReporte)
            {
                case Enumerados.TipoReporteFiltroConta.RecYCosteoDeInvMP_SMO:

                    //Reconstrucción y costeo de inventario MP a una fecha
                    DataTable dataTableMP = FiltroConta.RegresaReconstruccionInvMP(dtpHasta.Value);
                    if (dataTableMP.Rows.Count == 0)
                    {
                        MessageBox.Show("No se encontraron registros", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    precarga.AsignastatusProceso("Creando archivo de excel...");                    
                    FiltroConta.GeneraArchivoExcelMP(dataTableMP, dtpHasta.Value, archivoTemporal);

                    break;
                case Enumerados.TipoReporteFiltroConta.RecYCosteoDeInvPP_SMO:

                    //Reconstrucción y costeo de inventario PP a una fecha                    
                    DataTable dataTablePP = FiltroConta.RegresaReconstruccionInvPP(dtpHasta.Value);
                    if (dataTablePP.Rows.Count == 0)
                    {
                        MessageBox.Show("No se encontraron registros", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    precarga.AsignastatusProceso("Creando archivo de excel...");
                    FiltroConta.GeneraArchivoExcelPP(dataTablePP, dtpHasta.Value, archivoTemporal);
                    break;
                case Enumerados.TipoReporteFiltroConta.RecYCosteoDeInvPT_SMO:

                    //Reconstrucción y costeo de inventario PT a una fecha                    
                    DataTable dataTablePT = FiltroConta.RegresaReconstruccionInvPT(dtpHasta.Value);
                    if (dataTablePT.Rows.Count == 0)
                    {
                        MessageBox.Show("No se encontraron registros", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    precarga.AsignastatusProceso("Creando archivo de excel...");
                    FiltroConta.GeneraArchivoExcelPT(dataTablePT, dtpHasta.Value, archivoTemporal);
                    break;
                case Enumerados.TipoReporteFiltroConta.CostoVendidoEntreFechas_SMO:

                    DataTable dataTableCVEF = FiltroConta.RegresaCostoVendidoEntreFechas(dtpDesde.Value, dtpHasta.Value);
                    if (dataTableCVEF.Rows.Count == 0)
                    {
                        MessageBox.Show("No se encontraron registros", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    precarga.AsignastatusProceso("Creando archivo de excel...");
                    FiltroConta.GeneraArchivoExcelCVEF(dataTableCVEF,dtpDesde.Value, dtpHasta.Value, archivoTemporal);
                    break;
                case Enumerados.TipoReporteFiltroConta.RecYCosteoDeMermas_SMO:

                    //Reconstrucción y costeo mermas entre fechas
                    DataSet dataTableRCM = FiltroConta.RegresaReconsYConteoMermas(CVE_CPTO,dtpDesde.Value, dtpHasta.Value);
                    if (dataTableRCM.Tables[1].Rows.Count == 0)
                    {
                        MessageBox.Show("No se encontraron registros", "", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                        return;
                    }
                    precarga.AsignastatusProceso("Creando archivo de excel...");
                    FiltroConta.GeneraArchivoExcelRCM(CVE_CPTO,dataTableRCM, dtpDesde.Value, dtpHasta.Value, archivoTemporal);
                    break;
                case Enumerados.TipoReporteFiltroConta.RecYCosteoDeInvPT_CMO:

                    //Reconstrucción y costeo de inventario PT a una fecha                    
                    DataTable dataTablePT_CMO = FiltroConta.RegresaReconstruccionInvPT_CMO(dtpHasta.Value);
                    if (dataTablePT_CMO.Rows.Count == 0)
                    {
                        MessageBox.Show("No se encontraron registros", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    precarga.AsignastatusProceso("Creando archivo de excel...");
                    FiltroConta.GeneraArchivoExcelPT_CMO(dataTablePT_CMO, dtpHasta.Value, archivoTemporal);
                    break;
                case Enumerados.TipoReporteFiltroConta.CostoVendidoEntreFechas_CMO:
                    //Costo de lo Vendido entre Fechas CMO
                    DataTable dataTableCVEF_CMO = FiltroConta.RegresaCostoVendidoEntreFechas_CMO(dtpDesde.Value, dtpHasta.Value);
                    if (dataTableCVEF_CMO.Rows.Count == 0)
                    {
                        MessageBox.Show("No se encontraron registros", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    precarga.AsignastatusProceso("Creando archivo de excel...");
                    FiltroConta.GeneraArchivoExcelCVEF_CMO(dataTableCVEF_CMO, dtpDesde.Value, dtpHasta.Value, archivoTemporal);
                    break;
                case Enumerados.TipoReporteFiltroConta.RecYCosteoDeMermas_CMO:

                    //Mermas CMO                   
                    DataSet dataTableRCM_CMO = FiltroConta.RegresaReconsYConteoMermas_CMO(CVE_CPTO, dtpDesde.Value, dtpHasta.Value);
                    if (dataTableRCM_CMO.Tables[1].Rows.Count == 0)
                    {
                        MessageBox.Show("No se encontraron registros", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    precarga.AsignastatusProceso("Creando archivo de excel...");
                    FiltroConta.GeneraArchivoExcelRCM_CMO(CVE_CPTO, dataTableRCM_CMO, dtpDesde.Value, dtpHasta.Value, archivoTemporal);
                    break;
            }
            XLS_GENERADO = true;
            //System.Diagnostics.Process.Start(archivoTemporal);
            FuncionalidadesFormularios.MostrarExcel(archivoTemporal);

        }
    }
}
