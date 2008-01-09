using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace HexEditBox
{
    public partial class HexEditBox : UserControl
    {
        public HexEditBox()
        {
            InitializeComponent();
            Initialize();
        }

        private string _value;
        public string Value {
            get {
                return this._value;
            }
            set {
                this._value = value;
            }
        }

        private int _lineHeight;
        public string LineHeight {
            get {
                return this.LineHeight;
            }
            set {
                this.LineHeight = value;
            }
        }

        private void Initialize(){
            
        }

        private void Draw() {
            
        }

    }
}