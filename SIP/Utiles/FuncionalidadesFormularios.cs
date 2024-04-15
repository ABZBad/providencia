using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace SIP.Utiles
{
    public class FuncionalidadesFormularios
    {
        public static List<T> DevuelveListaDeObjetosContenidosEnFormulario<T>(Form formulario,T tipo)
        {            
            List<T> controles = new List<T>();                        
            foreach (Control ctrls in formulario.Controls)
            {                
                foreach (var ctrlsChild in ctrls.Controls)
                {
                    if (tipo.GetType().Name == ctrlsChild.GetType().Name)
                    {
                        controles.Add((T)ctrlsChild);
                    }                    
                }
            }
            return controles;
        }

        public static void LimpiaobjetosPorTipo<T>(Form formulario, T tipo)
        {            
            foreach (Control ctrls in formulario.Controls)
            {
                foreach (var ctrlsChild in ctrls.Controls)
                {
                    switch (ctrlsChild.GetType().Name)
                    {
                        case "TextBox":
                            TextBox obj = (TextBox)ctrlsChild;
                            obj.Text = "";
                            break;
                        case "TextBoxEx":
                            TextBox obj0 = (TextBox)ctrlsChild;
                            obj0.Text = "";
                            break;
                        case "CheckBox":
                            CheckBox obj1 = (CheckBox)ctrlsChild;
                            obj1.Checked = false;
                            break;
                        case "NumericTextBox":
                            TextBox obj2 = (TextBox)ctrlsChild;
                            obj2.Text = "0";
                            break;
                        default:
                            break;
                    }
                }
            }
            
        }

        public static void MostrarExcel(string RutaArchivo)
        {


            FileInfo fileInfo = new FileInfo(RutaArchivo);

            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(RutaArchivo);
            psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized;
            
            System.Diagnostics.Process proc = System.Diagnostics.Process.Start(psi);
            //do
            //{
            //    if (proc != null)
            //    {
            //        proc.Refresh();
            //    }
            //} while (proc == null);

            //Apis.ShowWindow(proc.MainWindowHandle, 3);

            //proc.WaitForInputIdle();
            //System.Threading.Thread.Sleep(1500);
            IntPtr wndHandle = IntPtr.Zero;
            foreach (Process process in System.Diagnostics.Process.GetProcesses())
            {
                if (process.MainWindowTitle.ToUpper().Contains(fileInfo.Name.ToUpper()))
                {
                    wndHandle = process.MainWindowHandle;
                    break;
                }
            }
            if (!wndHandle.Equals(IntPtr.Zero))
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Ventana encontrada: {0}", wndHandle));
                //Apis.SetWindowPos(wndHandle, new IntPtr(-2), 0, 0, 0, 0, 3);
                Apis.ShowWindow(wndHandle, 3);
                Apis.FLASHWINFO fInfo = new Apis.FLASHWINFO();
                fInfo.cbSize = Convert.ToUInt32(Marshal.SizeOf(fInfo));
                fInfo.hwnd = wndHandle;
                fInfo.dwFlags = 3 | 12;
                fInfo.uCount = UInt32.MaxValue;
                fInfo.dwTimeout = 0;

                Apis.FlashWindowEx(ref fInfo);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Ventana no localizada");
            }
            MessageBox.Show("Archivo generado", "SIP 8", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        }
    }
}
