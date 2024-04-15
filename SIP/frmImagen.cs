using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SIP
{
    public partial class frmImagen : Form
    {
        public frmImagen(Image imagen)
        {
            InitializeComponent();
            pictureBox1.Image = imagen;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }


        private void frmImagen_Load(object sender, EventArgs e)
        {
            //pictBoxLogo.Size = new Size(splitContainer1.Panel1.Width - 1, splitContainer1.Panel1.Height - 1);
        }

        private void frmImagen_KeyDown(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.Control) + Convert.ToInt32(Keys.C))
            {
                if (pictureBox1.Bounds.Contains(this.PointToClient(Cursor.Position)))
                {
                    if (pictureBox1.Image != null)
                    {
                        Clipboard.SetImage(pictureBox1.Image);
                    }
                }
            }
        }

        private void frmImagen_Resize(object sender, EventArgs e)
        {
            pictureBox1.Size = new Size(this.Width - 1, this.Height - 1);
        }

        private void frmImagen_ResizeEnd(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }
    }
}
