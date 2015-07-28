using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElyDeckers.NetworkProcessAgent.UI
{
    class PlaceholderTextBox : TextBox
    {
        private const int EM_SETCUEBANNER = 0x1501;
        private string _placeholderText;
        private TextBox _textBox;

        public PlaceholderTextBox(TextBox textBox)
        {
            _textBox = textBox;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)]string lParam);

        public string PlaceholderText
        {
            get { return _placeholderText;  }
            set { _placeholderText = value; SendMessage(_textBox.Handle, EM_SETCUEBANNER, 0, _placeholderText); }
        }
    }
}
