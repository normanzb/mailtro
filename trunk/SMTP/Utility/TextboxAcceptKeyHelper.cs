using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SMTP.Utility
{
    class TextboxAcceptKeyHelper
    {
        public event EventHandler<KeyPressEventArgs> EnterPressed;

        private TextBox _txtHostage;
        private Button _acceptButton;

        public TextboxAcceptKeyHelper(TextBox textbox) {
            this.TextBox = textbox;
            this.TextBox.KeyPress += new KeyPressEventHandler(OnKeyPress);
        }

        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\x000d')
            {
                if (this.AcceptButton != null)
                    this.AcceptButton.PerformClick();
                if (EnterPressed != null)
                    EnterPressed(sender, e);
            }

        }


        public TextBox TextBox {
            get {
                return this._txtHostage;
            }
            set {
                this._txtHostage = value;
            }
        }

        public Button AcceptButton {
            get {
                return this._acceptButton;
            }
            set {
                this._acceptButton = value;
            }
        }
    }
}
