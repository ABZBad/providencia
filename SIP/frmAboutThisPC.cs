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
    public partial class frmAboutThisPC : Form
    {
        public frmAboutThisPC()
        {
            InitializeComponent();
        }

        private void frmAboutThisPC_Load(object sender, EventArgs e)
        {

            lblOS.Text = Environment.GetEnvironmentVariable("OS");
            lblDrive.Text = Environment.GetEnvironmentVariable("homedrive");
            lblWinDir.Text = Environment.GetEnvironmentVariable("windir");
            lblIniSession.Text = Environment.GetEnvironmentVariable("SESSIONNAME");
            lblProcess.Text = Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER");
            lblArchitecture.Text = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE");
            lblWinUser.Text = Environment.GetEnvironmentVariable("username");
            lblComputerName.Text = Environment.GetEnvironmentVariable("COMPUTERNAME");
            lblPathProfile.Text = Environment.GetEnvironmentVariable("USERPROFILE") + @"\";
        }
    }
}
