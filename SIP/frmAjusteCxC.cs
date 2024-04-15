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
    public partial class frmAjusteCxC : Form
    {
        private delegate void DelActivaBotones();
        private delegate void DelActualizaStatusAjuste(int ID, string Texto);
        private Queue<AjusteCxC> colaAjustes = new Queue<AjusteCxC>();
        private DataTable dtSaldos;
        private Precarga precarga;
        private int selectedRows = 0;
        public frmAjusteCxC()
        {
            InitializeComponent();
        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMontoDiferencia.Text))
            {
                BackgroundWorker bgw = new BackgroundWorker();
                bgw.DoWork += bgw_DoWork;
                bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
                precarga = new Precarga(this);
                precarga.AsignastatusProceso("Cargando saldos...");
                precarga.MostrarEspera();
                bgw.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show("Escriba un valor mayor a cero","Verifique", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtMontoDiferencia.Focus();
            }
        }

        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnSeleccionar.Text = "Seleccionar todo";
            precarga.RemoverEspera();
            dgSaldos.DataSource = dtSaldos;
            lblTotalRegistros.Text = string.Format("Total de registros: {0}", dtSaldos.Rows.Count);
            lblSeleccionados.Text = "Seleccionados : 0";

            if (dtSaldos.Rows.Count > 0)
            {
                btnAjustar.Enabled = true;
            }
            else
            {
                btnAjustar.Enabled = false;
            }
            int id = 0;
            foreach (DataRow row in dtSaldos.Rows)
            {
                id++;
                row["ID"] = id;
            }
        }

        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            selectedRows = 0;
            dtSaldos = AjusteCxC.RegresaSaldos(Convert.ToDecimal(txtMontoDiferencia.Text));
        }

        private void CargaSaldos()
        {
            
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAjustar_Click(object sender, EventArgs e)
        {
            if (selectedRows > 0)
            {
                if (
                    MessageBox.Show(string.Format("¿ Confirma ajustar el saldo de: {0} clientes ?", selectedRows),
                        "Confirme", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                    System.Windows.Forms.DialogResult.Yes)
                {
                    btnAjustar.Enabled = false;
                    btnCancelar.Enabled = false;
                    dgSaldos.Columns["STATUS"].Visible = true;
                    foreach (DataRow row in dtSaldos.Rows)
                    {
                        if (row["SEL"] != DBNull.Value)
                        {
                            if (Convert.ToBoolean(row["SEL"]) == true)
                            {
                                colaAjustes.Enqueue(new AjusteCxC()
                                {
                                    ID = (int)row["ID"],
                                    CVE_CLIE = (string) row["CVE_CLIE"],
                                    MONTO_AJUSTE = (double) row["SALDO"],
                                    NO_FACTURA = row["NO_FACTURA"].ToString(),
                                    REFER = (string) row["REFER"],
                                    ID_MOV = (int)row["ID_MOV"]
                                });

                                row["STATUS"] = "En espera...";
                            }
                        }
                    }
                    AplicaAjustes();
                }
            }
            else
            {
                if (dtSaldos != null)
                {
                    if (dtSaldos.Rows.Count > 0)
                    {
                        MessageBox.Show("Seleccione al menos 1 cliente de la lista", "Verifique", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        MessageBox.Show("La lista está vacía", "Verifique", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void ActualizaStatusAjuste(int ID,string Texto)
        {

            if (this.InvokeRequired)
            {
                DelActualizaStatusAjuste del = new DelActualizaStatusAjuste(ActualizaStatusAjuste);
                this.Invoke(del, new object[] {ID, Texto});
            }
            else
            {
                if (dtSaldos.Rows.Count > 0)
                {
                    try
                    {
                        dtSaldos.Rows[ID - 1]["STATUS"] = Texto;
                        if (ID + 10 > dgSaldos.DisplayedRowCount(true))
                        {
                            dgSaldos.FirstDisplayedScrollingRowIndex = ID - 1;
                        }
                    }
                    catch (Exception Exc)
                    {
                        System.Diagnostics.Debug.WriteLine("ID: {0},Texto: {1},Ex: {2}",ID,Texto,Exc.Message);
                    }
                }
            }            
        }
        private void AplicaAjustes()
        {
            if (colaAjustes.Count > 0)
            {
                AjusteCxC ajusteCxC = new AjusteCxC();
                ajusteCxC.OnSaldoAjustado += ajusteCxC_OnSaldoAjustado;
                ajusteCxC.OnError += ajusteCxC_OnError;
                AjusteCxC ajusteActual = colaAjustes.Dequeue();
                ajusteCxC.AjustarSaldo(ajusteActual);
                ActualizaStatusAjuste(ajusteActual.ID, "Procesando...");
            }
        }

        void ajusteCxC_OnError(AjusteCxC AjusteCxC, Exception Ex)
        {
            ActualizaStatusAjuste(AjusteCxC.ID, "Error : " + Ex.Message);
            AplicaAjustes();
        }

        void ajusteCxC_OnSaldoAjustado(AjusteCxC AjusteCxC)
        {
            ActualizaStatusAjuste(AjusteCxC.ID, "Terminado.");
            if (colaAjustes.Count > 0)
            {
                AjusteCxC ajusteCxC = new AjusteCxC();
                ajusteCxC.OnSaldoAjustado += ajusteCxC_OnSaldoAjustado;
                ajusteCxC.OnError += ajusteCxC_OnError;
                AjusteCxC ajusteActual = colaAjustes.Dequeue();

                ActualizaStatusAjuste(ajusteActual.ID, "Procesando...");
                ajusteCxC.AjustarSaldo(ajusteActual);
                
            }
            else
            {
                MessageBox.Show("Proceso terminado", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //dtSaldos.Clear();            
                ActivaBotones();
            }
            
        }

        private void ActivaBotones()
        {
            if (this.InvokeRequired)
            {
                DelActivaBotones del = new DelActivaBotones(ActivaBotones);
                this.Invoke(del);
            }
            else
            {
                btnCancelar.Enabled = true;
            }
        }
        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            int newValue;
            if (btnSeleccionar.Text == "Seleccionar todo")
            {
                newValue = 1;
                btnSeleccionar.Text = "Ninguno";
                selectedRows = dtSaldos.Rows.Count;
                UpdateSelectedRowsLabel();
            }
            else
            {
                newValue = 0;
                selectedRows = 0;
                UpdateSelectedRowsLabel();
                btnSeleccionar.Text = "Seleccionar todo";
            }
            foreach (DataRow row in dtSaldos.Rows)
            {
                row["SEL"] = newValue;
            }
        }

        private void dgSaldos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void IncSelectedChecks()
        {
            selectedRows++;
            UpdateSelectedRowsLabel();
        }

        private void DecSelectedChecks()
        {
            selectedRows--;
            UpdateSelectedRowsLabel();
        }

        private void UpdateSelectedRowsLabel()
        {
            lblSeleccionados.Text = string.Format("Seleccionados : {0}", selectedRows);
        }
        private void dgSaldos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                dgSaldos.EndEdit();

                bool isChecked;

                if (dgSaldos.CurrentCell.Value != DBNull.Value)
                {
                    isChecked = true;
                }
                else
                {
                    isChecked = false;
                }

                if (isChecked)
                {
                    IncSelectedChecks();
                }
                else
                {
                    DecSelectedChecks();
                }
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            string archivoTemporal = System.IO.Path.GetTempFileName().Replace(".tmp", ".xls");
            if (dtSaldos != null)
            {
                if (dtSaldos.Rows.Count > 0)
                {
                    Stream stream = ulp_bl.Utiles.ExcelNpoiUtil.Dt2Excel(dtSaldos);
                    FileStream fs = new FileStream(archivoTemporal, FileMode.CreateNew);
                    stream.Position = 0;
                    stream.CopyTo(fs);
                    fs.Close();

                    FuncionalidadesFormularios.MostrarExcel(archivoTemporal);
                }
            }
        }

        private void dgSaldos_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Sender: {0}, Error: {1},Col: {2}, Ren: {3},Context: {4}",sender,e.Exception.Message,e.ColumnIndex,e.RowIndex,e.Context.ToString());
            e.Cancel = true;
        }
    }
}
