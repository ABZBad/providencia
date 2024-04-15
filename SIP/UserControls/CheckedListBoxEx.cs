using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SIP.UserControls
{
    public class CheckedListBoxEx : CheckedListBox
    {
        private bool _b = true;
        public string TextForEmptyList { get; set; }

        public CheckedListBoxEx()
        {
            this.Resize += ListBoxEx_Resize;
        }

        void ListBoxEx_Resize(object sender, EventArgs e)
        {
            if (_b) Invalidate();
        }
        protected override void WndProc(ref Message m)
        {
           // System.Diagnostics.Debug.WriteLine(m.ToString());  
            base.WndProc(ref m);
            if (m.Msg == 15)
            {
                if (this.Items.Count == 0)
                {
                    
                    _b = true;
                    Graphics g = this.CreateGraphics();
                    int w = 0;
                    w = this.Width;
                    w -= g.MeasureString(TextForEmptyList, this.Font).ToSize().Width;
                    w = w / 2;
                    g.DrawString(TextForEmptyList, this.Font, SystemBrushes.ControlText, w, 30);
                }
                else
                {
                    if (_b)
                    {
                        this.Invalidate();
                        _b = false;
                    }
                }
            }
            if (m.Msg == 4127)
            {
                this.Invalidate();
            }
            
        }        
    }
}
