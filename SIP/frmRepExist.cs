using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIP.Utiles;
using ulp_bl;
using ulp_bl.Reportes;
namespace SIP
{
    public partial class frmRepExist : Form
    {
        public frmRepExist()
        {
            InitializeComponent();
        }

        private void btnBaseSMAX_Click(object sender, EventArgs e)
        {
            Enumerados.TipoReporteExistencias tipoReporte = Enumerados.TipoReporteExistencias.AlmGeneral;

            if (optAlmGeneral.Checked)
            {
                tipoReporte = Enumerados.TipoReporteExistencias.AlmGeneral;
            }
            else if (optAlmSurtido.Checked)
            {
                tipoReporte = Enumerados.TipoReporteExistencias.AlmSurtido;
            }
            else if (OptAlmlMostrador.Checked)
            {
                tipoReporte = Enumerados.TipoReporteExistencias.AlmMostrador;
            }

            vw_ExistenciasBase existenciasBase = new vw_ExistenciasBase();

            string archivoTemporal = System.IO.Path.GetTempFileName().Replace(".tmp", ".xls");

            //reporte tal y como se pidió originalmente en el código de VB6 entregado el 4-abr-2014
            //existenciasBase.GeneraArchivoExcelBase(tipoReporte, Enumerados.TipoReporteExistenciasBase.SMAX, archivoTemporal);

            //reporte como lo pidieron (en su versión más antigua) el ejemplo se puede ver en código original de VB6 de may-2012
            this.Cursor = Cursors.WaitCursor;
            
                existenciasBase.GeneraArchivoExcelBase2(tipoReporte, Enumerados.TipoReporteExistenciasBase.SMAX,archivoTemporal);

                this.Cursor = Cursors.Default;
            //System.Diagnostics.Process.Start(archivoTemporal);
            FuncionalidadesFormularios.MostrarExcel(archivoTemporal);


        }

        private void btnBaseSMIN_Click(object sender, EventArgs e)
        {
            Enumerados.TipoReporteExistencias tipoReporte = Enumerados.TipoReporteExistencias.AlmGeneral;

            if (optAlmGeneral.Checked)
            {
                tipoReporte = Enumerados.TipoReporteExistencias.AlmGeneral;
            }
            else if (optAlmSurtido.Checked)
            {
                tipoReporte = Enumerados.TipoReporteExistencias.AlmSurtido;
            }
            else if (OptAlmlMostrador.Checked)
            {
                tipoReporte = Enumerados.TipoReporteExistencias.AlmMostrador;
            }

            vw_ExistenciasBase existenciasBase = new vw_ExistenciasBase();

            string archivoTemporal = System.IO.Path.GetTempFileName().Replace(".tmp", ".xls");           
            //existenciasBase.GeneraArchivoExcelBase(tipoReporte, Enumerados.TipoReporteExistenciasBase.SMIN, archivoTemporal);
            this.Cursor = Cursors.WaitCursor;
            existenciasBase.GeneraArchivoExcelBase2(tipoReporte, Enumerados.TipoReporteExistenciasBase.SMIN, archivoTemporal);
            this.Cursor = Cursors.Default;
            //System.Diagnostics.Process.Start(archivoTemporal);
            FuncionalidadesFormularios.MostrarExcel(archivoTemporal);
        }

        private void baseSMINSinPP_Click(object sender, EventArgs e)
        {
            Enumerados.TipoReporteExistencias tipoReporte = Enumerados.TipoReporteExistencias.AlmGeneral;

            if (optAlmGeneral.Checked)
            {
                tipoReporte = Enumerados.TipoReporteExistencias.AlmGeneral;
            }
            else if (optAlmSurtido.Checked)
            {
                tipoReporte = Enumerados.TipoReporteExistencias.AlmSurtido;
            }
            else if (OptAlmlMostrador.Checked)
            {
                tipoReporte = Enumerados.TipoReporteExistencias.AlmMostrador;
            }

            vw_ExistenciasBase existenciasBase = new vw_ExistenciasBase();

            string archivoTemporal = System.IO.Path.GetTempFileName().Replace(".tmp", ".xls");

            //existenciasBase.GeneraArchivoExcelBase(tipoReporte, Enumerados.TipoReporteExistenciasBase.SMINsinPP, archivoTemporal);
            this.Cursor = Cursors.WaitCursor;
            existenciasBase.GeneraArchivoExcelBase2(tipoReporte, Enumerados.TipoReporteExistenciasBase.SMINsinPP, archivoTemporal);
            this.Cursor = Cursors.Default;
            //System.Diagnostics.Process.Start(archivoTemporal);
            FuncionalidadesFormularios.MostrarExcel(archivoTemporal);
        }
    }
}
