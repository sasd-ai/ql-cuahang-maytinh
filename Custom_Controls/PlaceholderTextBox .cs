using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
namespace Custom_Controls
{
    public class PlaceholderTextBox : TextBox
    {
        private string _placeholderText = "Nhập văn bản...";

        public string PlaceholderText
        {
            get { return _placeholderText; }
            set { _placeholderText = value; Invalidate(); }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            SetPlaceholder();
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            RemovePlaceholder();
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            SetPlaceholder();
        }

        private void SetPlaceholder()
        {
            if (string.IsNullOrEmpty(Text))
            {
                Text = PlaceholderText;
                ForeColor = Color.Gray;
            }
        }

        private void RemovePlaceholder()
        {
            if (Text == PlaceholderText)
            {
                Text = "";
                ForeColor = Color.Black;
            }
        }
    }
}
