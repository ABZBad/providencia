using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ulp_bl.Utiles
{
    public class Varios
    {
        /*
        public static string ValidaNulos(Nullable<DateTime> dato)
        {
            if (dato != null)
            {
                return Convert.ToString(dato.Value.ToShortDateString());
            }
            else
            {
                return "";
            }
        }
         */
        public static string ValidaNulos(object dato)
        {
            if (dato != null)
            {
                /*
                switch (dato.GetType().ToString())
                {
                    case "System.DateTime":
                        return Convert.ToDateTime(dato).ToShortDateString();
                        break;
                    default:
                        return Convert.ToString(dato);
                        break;
                }
                 */
                return Convert.ToString(dato);
            }
            else
            {
                return "";
            }
        }

        public static void ReportaErrorPorCorreo(Exception e,StackFrame sf,string VersionExe)
        {

            string screenShotFile = Path.GetTempFileName().Replace(".tmp", ".png");

            ArrayList al = new ArrayList();

            Bitmap errorImage = ScreenShot();

            errorImage.Save(screenShotFile);

            Thread thread = new Thread(new ParameterizedThreadStart(SendMailError));


            System.Reflection.MethodBase methodBase = sf.GetMethod();

            var parameter_info = methodBase.GetParameters();
            var localVariables = methodBase.GetMethodBody().LocalVariables;

            var pI = parameter_info;

            string usr = "";
            if (!string.IsNullOrEmpty(Globales.UsuarioActual.UsuarioUsuario))
            {
                usr = Globales.UsuarioActual.UsuarioUsuario;
            }
            string body = string.Format("Linea: {0}\nArchivo: {1}\nMétodo: {2}\nException: {3}\nSource: {4}\n\nUsuario: {7}\nFecha / Hr: {5}\nVersión EXE: {8}\n\n\nException:\n\n{6}", sf.GetFileLineNumber(), sf.GetFileName(), sf.GetMethod(), e.Message, e.Source,DateTime.Now,e.ToString(),usr,VersionExe);

            

            al.Add(body);
            al.Add(screenShotFile);


            thread.Start(al);            

        }
        
        private static void SendMailError(object obj)
        {

            ArrayList al = (ArrayList) obj;

            string errorImageFile = (string)al[1];
            //rozexa56
            //ulp.sip80@gmail.com

            

            var fromAddress = new MailAddress("ulp.sip80@gmail.com", "SIP 8.0");
            var toAddress = new MailAddress("victor.labastida@grupoivstec.mx", "Victor Labastida");
            const string fromPassword = "rozexa56";
            const string subject = "Runtime error...";
            string body = (string)al[0];

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            Attachment attachedImage = new Attachment(errorImageFile);                  
            
            using (var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body})
            {
                message.Attachments.Add(attachedImage);
                smtp.Send(message);
            }
            File.Delete(errorImageFile);
        }

        private static Bitmap ScreenShot()
        {
           
            Rectangle bounds = Screen.PrimaryScreen.Bounds;
            Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height);

            Graphics g = Graphics.FromImage(bitmap);
                
                    g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);                                    
                return bitmap;
            
        }
    }
}
