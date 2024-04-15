using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIP.Utiles;

namespace SIP
{
    public partial class frmEspera : Form
    {
        private bool IsAnimationInProgress;
        private bool IsFormLoading;
        private Timer tmrProgressAnimation = new Timer();
        private Precarga precarga;
        public frmEspera()
        {
            InitializeComponent();            
            tmrProgressAnimation.Interval = 500;
            tmrProgressAnimation.Tick += tmrProgressAnimation_Tick;
        }

        void tmrProgressAnimation_Tick(object sender, EventArgs e)
        {
            tmrProgressAnimation.Enabled = false;
            precarga = new Precarga(this);
            precarga.MostrarEspera();
        }

        private void frmEspera_Load(object sender, EventArgs e)
        {
            IsFormLoading = true;
            Show();
        }

        private void frmEspera_Activated(object sender, EventArgs e)
        {            
            if (IsFormLoading)
            {
                IsFormLoading = false;
                tmrProgressAnimation.Enabled = true;
            }


        }

        private void frmEspera_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (precarga != null)
                precarga.RemoverEspera();
        }
    }
}
