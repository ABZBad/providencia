using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace SIP
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Assembly assembly = Assembly.LoadWithPartialName("CrystalDecisions.Windows.Forms");
            Assembly assembly2 = Assembly.LoadWithPartialName("CrystalDecisions.Shared");
            Assembly assembly4 = Assembly.LoadWithPartialName("CrystalDecisions.ReportAppServer");
            Assembly assembly5 = Assembly.LoadWithPartialName("CrystalDecisions.Web");
            Assembly assembly6 = Assembly.LoadWithPartialName("System.Web");

            /*
            Application.ThreadException += Application_ThreadException;
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            */
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Application.Run(new frmCapFactProvCostura());

            Application.Run(new frmControlPanel());



        }
        /*
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            StackTrace st = new StackTrace((Exception) e.ExceptionObject);

            StackFrame sf = st.GetFrame(0);
            
            try
            {
                AppInfo.RutaApp = System.Reflection.Assembly.GetExecutingAssembly().Location;
                ulp_bl.Utiles.Varios.ReportaErrorPorCorreo((Exception)e.ExceptionObject, sf, AppInfo.VersionCompleta);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            //MessageBox.Show(string.Format("Linea: {0}, Archivo: {1}, Método: {2}", sf.GetFileLineNumber(), sf.GetFileName(), sf.GetMethod()));

            //st.GetFrame(0).;

            MessageBox.Show(((Exception)e.ExceptionObject).Message,"Current Domain",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            
            StackTrace st = new StackTrace(e.Exception,true);

            StackFrame sf = st.GetFrame(0);
            try
            {
                AppInfo.RutaApp = System.Reflection.Assembly.GetExecutingAssembly().Location;
                ulp_bl.Utiles.Varios.ReportaErrorPorCorreo(e.Exception, sf, AppInfo.VersionCompleta);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            //MessageBox.Show(string.Format("Linea: {0}, Archivo: {1}, Método: {2}, Exception: {3},Source: {4}", sf.GetFileLineNumber(), sf.GetFileName(), sf.GetMethod(),e.Exception.Message,e.Exception.Source));
            MessageBox.Show(e.Exception.Message,"Main thread",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }
         * */
    }
}
