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
    public partial class AddPartidasPediLogoPreview : Form
    {
        public AddPartidasPediLogoPreview()
        {
            InitializeComponent();
        }
        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }
        private const int WS_EX_TOPMOST = 0x00000008;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ExStyle |= WS_EX_TOPMOST;
                return createParams;
            }
        }
        private void AddPartidasPediLogoPreview_Load(object sender, EventArgs e)
        {
           // MessageBox.Show("=)");
        }
    }
}
