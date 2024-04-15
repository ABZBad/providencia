using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SIP.UserControls
{
    public class Scape : Component,IButtonControl
    {
        
        private DialogResult dlgResult = DialogResult.OK;
        private Form form;
        public Form Form {
            set { form = value; }
            get { return form; }
        }
        public DialogResult DialogResult
        {
            get { return dlgResult; }
            set { dlgResult = value; }
        }

        public void NotifyDefault(bool value)
        {
            throw new NotImplementedException();
        }

        public void PerformClick()
        {
            form.Close();
        }
    }
}
