using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SMTP.Utility
{
    class Base64Convertor
    {
        const int BufferLength = 255;

        public static string Convert(string inputStr) 
        {
            return Convert(inputStr, Encoding.Default);
        }

        public static string Convert(string inputStr, Encoding encoding)
        {
            return System.Convert.ToBase64String(encoding.GetBytes(inputStr));
        }

        public static string Convert(string inputStr, string encodingName)
        {
            Encoding oEncoding = Encoding.Default;

            switch(encodingName.ToLower())
            {
                case "ascii":
                    oEncoding = Encoding.ASCII;
                    break;
                case "unicode":
                    oEncoding = Encoding.Unicode;
                    break;
                case "bigendianunicode":
                    oEncoding = Encoding.BigEndianUnicode;
                    break;
                case "utf32":
                case "utf-32":
                    oEncoding = Encoding.UTF32;
                    break;
                case "utf7":
                case "utf-7":
                    oEncoding = Encoding.UTF7;
                    break;
                case "utf8":
                case "utf-8":
                    oEncoding = Encoding.UTF8;
                    break;
                default:
                    oEncoding = Encoding.GetEncoding(encodingName);
                    break;
            }

            return Convert(inputStr, oEncoding);
        }
    }
}
