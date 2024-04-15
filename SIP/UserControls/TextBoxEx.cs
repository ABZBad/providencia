using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SIP.UserControls
{
    public class TextBoxEx : TextBox
    {
        private bool _OnlyUpperCaseProperty;
        private bool _AutoTABOnKeyDown = true;
        private bool _AutoTABOnKeyUp = true;
        private bool _SelectAllOnFocus = true;
        [DefaultValue(true)]
        public bool SelectAllOnFocus
        {
            get { return _SelectAllOnFocus; }
            set { _SelectAllOnFocus = value; }
        }
        public bool OnlyUpperCase
        {
            get { return _OnlyUpperCaseProperty; }
            set { _OnlyUpperCaseProperty = value; }
        }
        [DefaultValue(true)]
        public bool AutoTABOnKeyDown
        {
            set { _AutoTABOnKeyDown = value; }
            get { return _AutoTABOnKeyDown; }
        }
        [DefaultValue(true)]
        public bool AutoTABOnKeyUp
        {
            set { _AutoTABOnKeyUp = value; }
            get { return _AutoTABOnKeyUp; }
        }

        protected override void OnEnter(EventArgs e)
        {
            if (_SelectAllOnFocus)
            {
                BeginInvoke((Action) SelectAll);
            }
            base.OnEnter(e);
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            if (_OnlyUpperCaseProperty)
            {
                if (!char.IsControl(e.KeyChar))
                {
                    e.KeyChar = char.ToUpper(e.KeyChar);
                }
            }

        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.Down)
            {
                if (_AutoTABOnKeyDown)
                {
                    System.Windows.Forms.SendKeys.Send("{TAB}");
                }
            }
            if (e.KeyCode == Keys.Up)
            {
                if (_AutoTABOnKeyUp)
                {
                    System.Windows.Forms.SendKeys.Send("+{TAB}");
                }
            }
        }
    }
}
