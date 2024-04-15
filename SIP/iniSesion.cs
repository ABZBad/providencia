using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using ulp_bl;
using ulp_bl.Permisos;
using SIP.Utiles;

namespace SIP
{
    public partial class iniSesion : Form
    {
        private Precarga precarga;
        private int contadorIntentos = 0;
        private bool puedeEntrar = false;
        public iniSesion()
        {
            InitializeComponent();
        }

        private void iniSesion_Load(object sender, EventArgs e)
        {
            AppInfo.RutaApp = System.Reflection.Assembly.GetExecutingAssembly().Location;

            lblFullVersion.Text = "Ingresar a " + AppInfo.NombreApp + " " + AppInfo.VersionCompleta;

            lblDetalleVersion.Text = "Version: " + AppInfo.VersionMayor + " Subversión: " + AppInfo.VersionMenor + " Compilación:" + AppInfo.VersionCompilacion;

            lblDetalleVersion.Text = string.Format("Versión: {0} Subversión: {1} Compilación: {2}", AppInfo.VersionMayor, AppInfo.VersionMenor, AppInfo.VersionCompilacion);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text.Trim() == "" || txtContraseña.Text.Trim() == "")
            {
                MessageBox.Show("Escriba un Usuario y Contraseña válido", "", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                txtUsuario.Focus();
                return;

            }
            //Se crea objeto para ejecutar tareas asíncronas
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            //Se mapean rutinas para ejecutar la tarea
            backgroundWorker.DoWork += backgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;

            //Se crea objeto que pone una pantalla en Espera
            precarga = new Precarga(this);
            precarga.MostrarEspera();
            precarga.AsignastatusProceso("Validando...");
            //Se ejecuta la tarea asíncrona
            backgroundWorker.RunWorkerAsync();
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Se quita el progressBar
            precarga.RemoverEspera();
            if (puedeEntrar)
            {

                Usuario usuario = new Usuario();

                usuario = usuario.Consultar(txtUsuario.Text);
                if (!usuario.TieneError)
                {
                    //se asigna como objeto Global a "usuario" para su uso futuro
                    Globales.UsuarioActual = usuario;

                    USUARIOS datosUsr = new USUARIOS();

                    Globales.DatosUsuario = datosUsr.ConsultarPorUsuario(usuario.UsuarioUsuario);
                    //Se obtiene el formulario principal de las variables globales
                    frmControlPanel frmCtrlPanel = (frmControlPanel)GlobalesUI.MainForm;

                    //Se pasan los permisos al método del formulario responable de cargar los menús
                    frmCtrlPanel.CargaMenus(usuario.PermisosPuedeEntrar);

                    //Se muestra el formulario principal:
                    frmCtrlPanel.Show();
                    this.Close();

                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error al recuperar la información del usuario:\n\r\n\r" +
                                    usuario.Error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                contadorIntentos = contadorIntentos + 1;
                MessageBox.Show("Los datos de Inicio de sesión son incorrectos, por favor intente de nuevo", "Inicio de Sesión", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtContraseña.Focus();

                if (contadorIntentos == 5)
                {
                    MessageBox.Show(
                        "Ha realizado demasiados intentos fallidos de inicio de sesión\n\rLa aplicación se cerrará. Esto es un retrazo temporal\n\rcuyo único objetivo es que su contraseña no pueda ser\n\raveriguada por un tercero.",
                        "Error de Inicio de Sesión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Exit();
                }
            }
        }

        void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Exception Ex = null;
            puedeEntrar = Acceso.ValidarUsuario(txtUsuario.Text, txtContraseña.Text, ref Ex);
            if (!puedeEntrar)
            {
                if (Ex != null)
                {
                    MessageBox.Show(Ex.Message, Ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnContinuar_Click(sender, e);
            }
        }

        private void txtUsuario_Enter(object sender, EventArgs e)
        {
            txtUsuario.SelectAll();
        }

        private void btnCambios_Click(object sender, EventArgs e)
        {

            string changeLog = "";
            Assembly assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream("SIP.ChangeLog.txt"))
            {
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    changeLog = streamReader.ReadToEnd();
                }
            }

            frmControlCambios frmCC = new frmControlCambios(changeLog);
            frmCC.ShowDialog();

        }

        private void lblDetalleVersion_Click(object sender, EventArgs e)
        {

        }


    }

}
