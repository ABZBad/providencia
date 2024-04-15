using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SIP.UserControls
{
    public enum TipoDeNumero
    {
        Integer,
        Decimal
    }
    public class NumericTextBox : TextBox
    {
        private string decimalString = string.Empty;
        ToolTip toolTip = new ToolTip();

        [Description("Pone 0 cuando hagan TAB si la propiedad es verdadera")]
        [DefaultValue(false)]
        public bool PonerCeroCuandoSeaVacio { set; get; }

        [Description("Determina el tipo de número que se puede escribir")]
        [DefaultValue(0)]
        public TipoDeNumero NumberType { set; get; }

        [Description("Valor mínimo aceptado")]
        [DefaultValue(-9999)]
        public int MinValue { get; set; }
        [Description("Valor máximo aceptado")]
        [DefaultValue(9999)]        
        public int MaxValue { get; set; }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                decimalString = value;
            }
        }

        public NumericTextBox()
        {
            toolTip.Draw += toolTip_Draw;
            this.Validating += NumericTextBox_Validating;
            this.TextChanged += NumericTextBox_TextChanged;
        }

        void NumericTextBox_TextChanged(object sender, EventArgs e)
        {
            if (NumberType == UserControls.TipoDeNumero.Decimal)
            {
                if (this.Text.Length == 1 && this.Text == ".")
                {
                    this.Text = "0.";
                    this.SelectionStart = 2;
                }
            }
        }

        void NumericTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (NumberType == UserControls.TipoDeNumero.Decimal)
            {
                decimalString = Text;
                decimal decimalResult = 0;
                bool isDecimal = decimal.TryParse(decimalString, out decimalResult);
                if (!isDecimal)
                {
                    ShowError("Solo se aceptan valores decimales");
                }
                else
                {
                    Text = decimalResult.ToString();
                }
            }
        }

        void toolTip_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            e.DrawText();
        }
        protected override void OnMouseLeave(EventArgs e)
        {

            base.OnMouseLeave(e);
        }
        protected override void OnValidating(System.ComponentModel.CancelEventArgs e)
        {
            base.OnValidating(e);
            CheckMinMaxValue();
        }
        protected override void OnLeave(EventArgs e)
        {
            if (this.PonerCeroCuandoSeaVacio && string.IsNullOrEmpty(Text))
            {
                this.Text = "0";
            }
            base.OnLeave(e);
        }
        protected override void OnEnter(EventArgs e)
        {
            BeginInvoke((Action)SelectAll);
            base.OnEnter(e);
        }  
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            if (NumberType == UserControls.TipoDeNumero.Integer)
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;

                    ShowError("Solo se aceptan valores numéricos");

                    //toolTip.IsBalloon = true;                
                }
            }
            else
            {
                
                    if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
                    {
                        if (e.KeyChar != (char) 46)
                        {
                            e.Handled = true;

                            ShowError("Solo se aceptan valores decimales");
                        }
                        else
                        {
                            var count = Text.Count(x => x == '.');
                            if (count >= 1)
                            {
                                e.Handled = true;

                                ShowError("Solo se aceptan valores decimales");
                            }                           
                        }
                    }
                
                
            }


        }
        
        private void ShowError(string ErrorMessage)
        {
            toolTip.OwnerDraw = true;
            toolTip.BackColor = Color.FromArgb(255,186,186);
            toolTip.ForeColor = Color.Red;
            toolTip.Show(ErrorMessage, this,new Point(this.ClientRectangle.X, this.ClientSize.Height), 1000);
        }
        private void CheckMinMaxValue()
        {
            if (MinValue != 0 && MaxValue != 0)
            {

                int intValue = 0;
                int.TryParse(Text, out intValue);

                if (!(intValue >= MinValue && intValue <= MaxValue))
                {
                    ShowError(string.Format("Escriba un valor entre: {0} y {1}", MinValue, MaxValue));
                }
            }

        }
    }
}
