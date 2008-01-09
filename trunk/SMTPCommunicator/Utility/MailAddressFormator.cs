using System;
using System.Collections.Generic;
using System.Text;

namespace SMTP.Utility
{
    class MailAddressFormator
    {
        public static string Format(string inputStr) {
            return "<" + inputStr + ">";
        }
    }
}
