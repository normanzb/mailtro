using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SMTP.Utility
{
    class Base64Convertor
    {
        const int BufferLength = 255;
        public static string Convert(string inputStr) {

            return System.Convert.ToBase64String(Encoding.Default.GetBytes(inputStr));
        }
        public static string Convert(string inputStr, Encoding encoding)
        {

            return System.Convert.ToBase64String(encoding.GetBytes(inputStr));
        }
    }
}
