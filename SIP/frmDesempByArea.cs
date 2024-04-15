using System;
using System.Collections;
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
    public partial class frmDesempByArea : Form
    {
        private delegate void DelAsignaValoresControles();
        private Queue<int> colaDeErrores = new Queue<int>();
        private DesempByArea des = new DesempByArea();
        private bool consultandoInformacion;
        private BackgroundWorker bgw = new BackgroundWorker();
        public frmDesempByArea()
        {
            InitializeComponent();
            bgw.DoWork += bgw_DoWork;
            bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
        }

        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (des.ExistePedido)
            {
                lblCliente.Text = des.Nombre;
                lblObservaciones.Text = des.Observaciones;
                lblProcesos.Text = des.Proceso;
                txtStdEspecial.Text = des.EstandarEspecial.ToString();


                foreach (Desempeño desempeñoArea in des.Desempeños)
                {
                    asignaDesempeñoArea(desempeñoArea);
                }                
                this.Cursor = Cursors.Default;
                consultandoInformacion = false;
            }
            else
            {
                consultandoInformacion = false;
                this.Cursor = Cursors.Default;
                MessageBox.Show("El pedido no existe", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtNumPedido.Focus();
            }
        }

        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            ConusltaInformacion();
        }
        public frmDesempByArea(int NumeroPedido)
        {
            InitializeComponent();
            SendKeys.Send("{TAB}");
            bgw.DoWork += bgw_DoWork;
            bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
            Show();
            txtNumPedido.ReadOnly = true;
            txtNumPedido.Text = NumeroPedido.ToString();
            txtNumPedido_Leave(this, new EventArgs());

        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (des == null)
            {
                MessageBox.Show("Escriba un número de pedido válido", "", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                txtNumPedido.Focus();
                return;
            }
            else if (des.NumeroPedido == 0) 
            {
                MessageBox.Show("Escriba un número de pedido válido", "", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                txtNumPedido.Focus();
                return;
            }
            if (colaDeErrores.Count > 0)
            {
                MessageBox.Show("Hay errores en algún campo de \"Observación\", favor de corregir", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (!des.ExistenEstandares)
                {
                    MessageBox.Show("No se puede continuar \"Pedido sin Estándares\".\n\r\n\rEl Pedido " + des.NumeroPedido.ToString() + " no cuenta con Estándares especificados, esto se puede deber a que el pedido fué capturado antes de la implementación de este desarrollo.", "Pedido sin Estándares", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                DialogResult dlgResult = MessageBox.Show("¿Confirma guardar los cambios?","Confirme",MessageBoxButtons.YesNo,MessageBoxIcon.Question);

                if (dlgResult == DialogResult.Yes)
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        des.EstandarEspecial = Convert.ToInt32(txtStdEspecial.Text);
                        des.Desempeños.Clear();
                        foreach (Enumerados.AreasEmpresa area in Enum.GetValues(typeof (Enumerados.AreasEmpresa)))
                        {
                            Control[] controles = regresaControlesPorArea(area);

                            RadioButton radioSi = (RadioButton) controles[0];
                            RadioButton radioNo = (RadioButton) controles[1];
                            TextBox txtObs = (TextBox) controles[2];

                            des.Desempeños.Add(new Desempeño()
                            {
                                Area = area,
                                AreaStr = area.ToString(),
                                Cumplio = radioSi.Checked,
                                CumplioStr =
                                    (!radioNo.Checked && !radioSi.Checked ? "-" : (radioSi.Checked ? "S" : "N")),
                                Observaciones = txtObs.Text
                            });
                        }
                        this.Cursor = Cursors.Default;
                        DesempByArea.GrabaDesempeños(des);
                        MessageBox.Show(Properties.Resources.Cadena_DatosGuardados, "", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        if (txtNumPedido.ReadOnly == false) //si la llamada al formulario se hizo desde el Menú y no desde la pantalla de Pedidos...
                        {
                            //se limpia formulario
                            txtNumPedido.Text = "";
                            LimpiaControles();
                            des = null;
                            txtNumPedido.Focus();
                        }
                    }
                    catch (Exception Ex)
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show(Properties.Resources.Cadena_ErrorAlGuardar + "\n\r\n\r" + Ex.Message, Ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void LimpiaControles()
        {


            if (this.InvokeRequired)
            {
                DelAsignaValoresControles del = new DelAsignaValoresControles(LimpiaControles);
                this.Invoke(del);
            }
            else
            {
                lblCliente.Text = "Cliente...";
                lblObservaciones.Text = "Observaciones...";
                lblProcesos.Text = "Procesos...";
                txtStdEspecial.Text = "0";
                colaDeErrores.Clear();

                foreach (Control control in Controls)
                {
                    if (control is GroupBox)
                    {
                        foreach (Control control1 in control.Controls)
                        {
                            if (control1 is RadioButton)
                            {
                                RadioButton radio = (RadioButton) control1;
                                radio.Checked = false;
                            }
                            if (control1 is TextBox)
                            {
                                TextBox txtObs = (TextBox) control1;
                                txtObs.Text = "";
                                if (txtObs.Tag != null)
                                {
                                    ErrorProvider errProv = (ErrorProvider) txtObs.Tag;
                                    errProv.Clear();
                                }
                                txtObs.Tag = null;

                            }
                        }
                    }
                }
            }

        }

        private void ConusltaInformacion()
        {
            consultandoInformacion = true;
            LimpiaControles();

            des = DesempByArea.RegresaDesempeños(Convert.ToInt32(txtNumPedido.Text));

        }
        private void txtNumPedido_Leave(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtNumPedido.Text))
                return;
            this.Cursor = Cursors.WaitCursor;
            if (!consultandoInformacion)
            {
                if (!bgw.IsBusy)
                {
                    bgw.RunWorkerAsync();
                }                

            }


        }

        private void asignaDesempeñoArea(Desempeño desempeñoArea)
        {
            Control[] controles = regresaControlesPorArea(desempeñoArea.Area);

            RadioButton optCumplio = (RadioButton) controles[0];
            RadioButton optNoCumplio = (RadioButton)controles[1];
            TextBox txtObs = (TextBox)controles[2];


            txtObs.Text = desempeñoArea.Observaciones; 

            if (desempeñoArea.Cumplio)
            {
                optCumplio.Checked = true;
            }
            else if (desempeñoArea.CumplioStr == "N")
            {
                optNoCumplio.Checked = true;
            }
            else
            {
                optNoCumplio.Checked = false;
            }
                      
            
        }

        void Generic_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton opt = (RadioButton)sender;
            TextBox txtObs = (TextBox)Controls.Find(opt.Tag.ToString(), true).FirstOrDefault();
            if (opt.Checked == true)
            {

                
                if (string.IsNullOrEmpty(txtObs.Text))
                {
                    GroupBox gpoBoxParent = (GroupBox)txtObs.Parent;
                    ErrorProvider errProv = new ErrorProvider();

                    string errorMsg =
                        string.Format(
                            "El departamento de \"{0}\" ha indicado un INCUMPLIMIENTO. Debe de indicar la razón del mismo.",
                            gpoBoxParent.Text);


                    if (gpoBoxParent.Text == "Cliente")
                        errorMsg = errorMsg.Replace("departamento de", "");

                    errProv.BlinkStyle = ErrorBlinkStyle.NeverBlink;
                    errProv.RightToLeft = true;
                    errProv.SetError(txtObs,errorMsg );
                    txtObs.Tag = errProv;
                    colaDeErrores.Enqueue(1);
                    //txtObs.Focus();
                }
                
            }
            
        }

        void Generic_Si_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton opt = (RadioButton)sender;
            TextBox txtObs = (TextBox)Controls.Find(opt.Tag.ToString(), true).FirstOrDefault();

            if (opt.Checked)
            {
                //if (!string.IsNullOrEmpty(txtObs.Text))
                //{
                    //if (
                    //    MessageBox.Show("Se eliminarán las observaciones\n\r\n\r¿Desea continuar?", "Confirme",
                    //        MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) ==
                    //    System.Windows.Forms.DialogResult.Yes)
                    //{
                        txtObs.Text = "";
                    //}
                    //else
                    //{
                    //    opt.Checked = true;
                    //}
                //}
                if (txtObs.Tag != null)
                {
                    ErrorProvider errProv = (ErrorProvider) txtObs.Tag;
                    errProv.SetError(txtObs, null);
                    //errProv.Clear();
                    errProv = null;
                    txtObs.Tag = null;
                    if (colaDeErrores.Count > 0)
                    {
                        colaDeErrores.Dequeue();
                    }
                }
            }
        }
        void GenericRadioLeave(object sender,EventArgs e)
        {
            //Generic_CheckedChanged(sender, e);
        }
        void GenericTxt_Leave(object sender,EventArgs e)
        {
            TextBox txtObs = (TextBox) sender;
            RadioButton radioNo = (RadioButton)Controls.Find(txtObs.Parent.Tag.ToString(), true)[0];
            if (radioNo.Checked)
            {
                Generic_CheckedChanged(radioNo, new EventArgs());
            }
            if (((ErrorProvider) txtObs.Tag) != null)
            {
                if (!string.IsNullOrEmpty(txtObs.Text))
                {
                    if (colaDeErrores.Count > 0)
                    {
                        colaDeErrores.Dequeue();
                    }
                    ErrorProvider errProv = (ErrorProvider) txtObs.Tag;
                    errProv.SetError(txtObs, null);
                    errProv = null;
                    txtObs.Tag = null;

                }
            }
            
        }
        private Control[] regresaControlesPorArea(Enumerados.AreasEmpresa areasEmpresa)
        {
            Control[] resultadoControles = new Control[3];
            switch (areasEmpresa)
            {
                case Enumerados.AreasEmpresa.Almacen:
                    resultadoControles[0] = optAlmacenCumplio;
                    resultadoControles[1] = optAlmacenNoCumplio;
                    resultadoControles[2] = txtAlmacenObs;
                    break;
                case Enumerados.AreasEmpresa.Cliente:
                    resultadoControles[0] = optClienteCumplio;
                    resultadoControles[1] = optClienteNoCumplio;
                    resultadoControles[2] = txtClienteObs;
                    break;
                case Enumerados.AreasEmpresa.Compras:
                    resultadoControles[0] = optComprasCumplio;
                    resultadoControles[1] = optComprasNoCumplio;
                    resultadoControles[2] = txtComprasObs;
                    break;
                case Enumerados.AreasEmpresa.Credito:
                    resultadoControles[0] = optCreditoCumplio;
                    resultadoControles[1] = optCreditoNoCumplio;
                    resultadoControles[2] = txtCreditoObs;
                    break;
                case Enumerados.AreasEmpresa.Operaciones:
                    resultadoControles[0] = optOpCumplio;
                    resultadoControles[1] = optOpNoCumplio;
                    resultadoControles[2] = txtOpObs;
                    break;
                case Enumerados.AreasEmpresa.Sistemas:
                    resultadoControles[0] = optSistemasCumplio;
                    resultadoControles[1] = optSistemasNoCumplio;
                    resultadoControles[2] = txtSistemasObs;
                    break;
                case Enumerados.AreasEmpresa.Ventas:                   
                    resultadoControles[0] = optVentasCumplio;
                    resultadoControles[1] = optVentasNoCumplio;
                    resultadoControles[2] = txtVentasObs;
                    break;
            }
            return resultadoControles;
        }

        private void frmDesempByArea_Load(object sender, EventArgs e)
        {

        }
    }
}
