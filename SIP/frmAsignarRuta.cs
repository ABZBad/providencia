using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ulp_bl;
using ulp_bl.Reportes;

namespace SIP
{
    public partial class frmAsignarRuta : Form
    {
        private DataTable dataTableMaestro = new DataTable();
        private DataTable dataTableDetalle = new DataTable();
        private int numeroPedido = 0;
        public frmAsignarRuta()
        {
            InitializeComponent();
            InicializaEstructuraTablaMaestra();
        }

        private void InicializaEstructuraTablaMaestra()
        {
            
            dataTableMaestro.Columns.Add("CMT_PEDIDO", typeof(int));
            dataTableMaestro.Columns.Add("CMT_MODELO", typeof(string));
            dataTableMaestro.Columns.Add("CMT_CANTIDAD", typeof(int));
            dataTableMaestro.Columns.Add("CMT_RUTA", typeof(string));
            
            

        }

        private void dataTableMaestro_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            AsignarRuta.GuardaCampoValorCMT_DET(numeroPedido, e.Column.ColumnName, e.ProposedValue, e.Column.DataType,null);
            //LlenaDgvDetalle(numeroPedido);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            frmInputBox frmInputBoxPedido = new frmInputBox();
            frmInputBoxPedido.lblTitulo.Text = "Número de pedido";
            frmInputBoxPedido.Text = "Asignación de Ruta";
            frmInputBoxPedido.ShowDialog();
            if (frmInputBoxPedido.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                //MessageBox.Show(frmInputBoxPedido.NTxtOrden.Text);
                numeroPedido = Convert.ToInt32(frmInputBoxPedido.NTxtOrden.Text);
                dataTableMaestro = AsignarRuta.RegresaRutaProcesosPedido(numeroPedido).Copy();
                

                if (dataTableMaestro.Rows.Count > 0)
                {
                 //   dataTableMaestro.PrimaryKey = new DataColumn[] {dataTableMaestro.Columns[0]};
                    dataTableMaestro.ColumnChanged += dataTableMaestro_ColumnChanged;
                    dgvMaestro.DataSource = dataTableMaestro;

                    
                 

                }
                else
                {
                    MessageBox.Show("El pedido no existe", "Verifique", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }


        }

        private void LlenaDgvDetalle(int numeroPedido,string Modelo,string Agrupador)
        {
            dataTableDetalle = AsignarRuta.RegresaRutaProcesosPedidoDetalle(numeroPedido,Modelo,Agrupador);
            dataTableDetalle.ColumnChanged += dataTableDetalle_ColumnChanged;
            dgvDetalle.DataSource = dataTableDetalle;            
        }

        void dataTableDetalle_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            
            if (e.Column.ColumnName == "CMT_DEPARTAMENTO")
            {
                if (e.ProposedValue != null)
                {
                    if (AsignarRuta.DepartamentoValido(e.ProposedValue.ToString()))
                    {
                        AsignarRuta.GuardaCampoValorCMT_DET(numeroPedido, e.Column.ColumnName, e.ProposedValue,
                            e.Column.DataType, Convert.ToInt32(e.Row["CMT_INDX"]));
                    }
                    else
                    {
                        /*no debió llegar aquí
                         * la validación la debió hacer el Grid
                         * */
                    }
                }
                else
                {
                    AsignarRuta.GuardaCampoValorCMT_DET(numeroPedido, e.Column.ColumnName, "null",
                            typeof(int), Convert.ToInt32(e.Row["CMT_INDX"]));
                }
            }
            else
            {
                AsignarRuta.GuardaCampoValorCMT_DET(numeroPedido, e.Column.ColumnName, e.ProposedValue,
                            e.Column.DataType, Convert.ToInt32(e.Row["CMT_INDX"]));
            }
            //LlenaDgvDetalle(numeroPedido);
        }

        private void frmAsignarRuta_Load(object sender, EventArgs e)
        {
            /*
             * 
             * Permisos para este módulo:
             * 
             *      32=Capturar ruta de procesos
             *      97=Pude liberar pedido
             * 
             * 
             */
            btnLiberar.Enabled = PermisosUsuarioEspeciales.TienePermisos(Globales.UsuarioActual.Id, 32, 97);
        }

        private void btnLiberar_Click(object sender, EventArgs e)
        {
            if (numeroPedido > 0)
            {
                if (AsignarRuta.LiberarPedido(numeroPedido))
                {
                    MessageBox.Show("El pedido ha sido actualizado exitosamente.", "", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    LimpaGrids();
                }
            }
        }

        private void LimpaGrids()
        {
            if (dgvMaestro.DataSource != null)
            {
                ((DataTable) dgvMaestro.DataSource).Rows.Clear();
            }
            if (dgvDetalle.DataSource != null)
            {
                ((DataTable) dgvDetalle.DataSource).Rows.Clear();
            }
            numeroPedido = 0;
        }
        private void dgvDetalle_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                //se valida departamento
                if (e.FormattedValue.ToString() != string.Empty)
                {
                    if (!AsignarRuta.DepartamentoValido(e.FormattedValue.ToString()))
                    {
                        MessageBox.Show("Departamento inexistente", "Verifique", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                        e.Cancel = true;
                        
                    }
                }
            }
        }

        private void frmAsignarRuta_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult resp = MessageBox.Show("¿Deseas capturar otro Pedido?", "Confirme", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resp == System.Windows.Forms.DialogResult.Yes)
            {
                e.Cancel = true;
                LimpaGrids();
                btnBuscar_Click(sender, new EventArgs());
            }
        }

        private void dgvMaestro_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvMaestro.Rows.Count > 0)
            {

                int pedido = (int)dgvMaestro.Rows[e.RowIndex].Cells["colPedido"].Value;
                string modelo = dgvMaestro.Rows[e.RowIndex].Cells["colModelo"].Value.ToString();
                string agrupador = dgvMaestro.Rows[e.RowIndex].Cells["colAgrupador"].Value.ToString();

                Cursor = Cursors.WaitCursor;
                LlenaDgvDetalle(pedido, modelo, agrupador);
                Cursor = Cursors.Default;
            }
        }
    }
}
