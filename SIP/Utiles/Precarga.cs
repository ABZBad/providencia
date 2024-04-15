using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using CrystalDecisions.CrystalReports.Engine;
using SIP.Properties;

namespace SIP.Utiles
{
    public class Precarga
    {
        private Timer tmrElapsed = new Timer();
        private int contadorSegundos = 0;
        private int contadorMinutos = 0;
        private Form forma;
        private Image imgLoader = Resources.ajax_loader;
        private PictureBox pictureBox2 = new PictureBox();
        private PictureBox pictureBox1 = new PictureBox();
        private string statusProceso = "Espere...";
        
        
        public Precarga(Form Forma)
        {
            
            forma = Forma;
            tmrElapsed.Tick += tmrElapsed_Tick;
            tmrElapsed.Interval = 1000;
            contadorSegundos = 0;
        }

        void tmrElapsed_Tick(object sender, EventArgs e)
        {
            contadorSegundos++;
            if (contadorSegundos > 59)
            {
                contadorMinutos++;
                contadorSegundos = 0;
            }
        }

        public void RemoverEspera()
        {
            pictureBox1.Visible = false;
            pictureBox1.Image = null;
            pictureBox2.Image = null;
            forma.Controls.Remove(pictureBox1);
            forma.Controls.Remove(pictureBox2);
            DesactivarControles(false);
            tmrElapsed.Stop();
            contadorSegundos = 0;
            contadorMinutos = 0;
        }
        /// <summary>
        /// Desactiva todos los controles del formulario contenedor
        /// </summary>
        public void DesactivarControles(bool Valor)
        {
            foreach (Control control in forma.Controls)
            {
                if (control is TextBox || control is Button || control is ComboBox)
                {
                    control.Enabled = !Valor;
                }
            }
        }
        public void MostrarEspera()
        {

            DesactivarControles(true);

            try
            {


                Rectangle bounds = forma.RectangleToScreen(forma.ClientRectangle);
                pictureBox1.BorderStyle = BorderStyle.None;
                pictureBox1.Name = "pic1";
                pictureBox1.Top = 0;
                pictureBox1.Left = 0;
                pictureBox1.Width = bounds.Width;
                pictureBox1.Height = bounds.Height;

                pictureBox2.Name = "pic2";
                pictureBox2.BorderStyle = BorderStyle.None;
                pictureBox2.Paint += pictureBox2_Paint;
                pictureBox2.Width = imgLoader.Width;
                pictureBox2.Height = imgLoader.Height;
                pictureBox2.Top = (pictureBox1.Height / 2) - (pictureBox2.Height / 2) - 18;
                //pictureBox2.Top = 0;
                pictureBox2.Left = (pictureBox1.Width / 2) - (pictureBox2.Width / 2);
                forma.Controls.Add(pictureBox1);
                pictureBox1.Controls.Add(pictureBox2);
                pictureBox2.Image = imgLoader;
                pictureBox1.BringToFront();
                pictureBox2.BringToFront();

                using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
                    }

                    Bitmap _bmp = new Bitmap(bitmap);
                    Bitmap _transp = new Bitmap(_bmp.Width, _bmp.Height);
                    Color _col_o = Color.Black;

                    for (int i = 0; i < _bmp.Width; i++)
                    {
                        for (int j = 0; j < _bmp.Height; j++)
                        {
                            _col_o = _bmp.GetPixel(i, j);
                            Color _col = Color.FromArgb(70, _col_o);
                            _transp.SetPixel(i, j, _col);
                        }
                    }


                    pictureBox1.Image = _transp;
                    pictureBox1.Visible = true;
                    //pictureBox2.Visible = true;
                    tmrElapsed.Start();
                }
            }
            catch (Exception ex) { }

        }
        void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            //g.DrawRectangle(new Pen(Brushes.Red), new Rectangle(0, 0, 24, 24));
            Font _font = new System.Drawing.Font(SystemFonts.StatusFont, FontStyle.Bold);

            g.DrawString(statusProceso, _font, Brushes.Black, new PointF(0, 0));
            int _x = pictureBox2.ClientRectangle.Width - 60;
            int _y = pictureBox2.ClientRectangle.Height - 16;            
            Font _fontC = new Font("Arial", 8, FontStyle.Regular);
            g.FillRectangle(Brushes.LightGray, new Rectangle(_x, _y, 60, 16));
            g.DrawRectangle(new Pen(Brushes.Gray, 1), new Rectangle(_x, _y, 60, 16));
            g.DrawRectangle(new Pen(Color.FromArgb(80, 140, 187), 1), new Rectangle(pictureBox2.ClientRectangle.X, pictureBox2.ClientRectangle.Y, imgLoader.Width - 1, imgLoader.Height - 1));
            g.DrawString(string.Format("[{0} m. {1} s.]", contadorMinutos, contadorSegundos),_fontC,Brushes.Gray,new PointF(_x,_y+1));
        }

        public void AsignastatusProceso(string StatusProceso)
        {
            statusProceso = StatusProceso;
        }

        public static void PrecargaCrystalReports()
        {
            Task.Factory.StartNew(() =>
            {
                try
                {

                    using (Reportes.frmReportes frmRpt = new Reportes.frmReportes(ulp_bl.Enumerados.TipoReporteCrystal.Precarga))
                    {
                        
                    }
                    //frmRpt.Show();

                }
                catch (Exception e)
                {
                    
                }

            }, TaskCreationOptions.LongRunning);
        }
    }
}
