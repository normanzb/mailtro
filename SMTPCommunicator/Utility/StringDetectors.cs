using System;
using System.Collections.Generic;
using System.Text;

namespace SMTP.Utility
{
    public class StringDetectors
    {
        public static bool IsNumeric(object value)
        {
            try
            {
                double d = System.Double.Parse(value.ToString(),
                System.Globalization.NumberStyles.Any);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static bool IsInteger(object value)
        {
            try
            {
                double d = System.Int64.Parse(value.ToString(),
                System.Globalization.NumberStyles.Any);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static bool NonEnglishChar(string inputStr)
        {
            int i;
            for (i = 0; i < inputStr.Length; i++)
            {

                if (BitConverter.ToUInt16(Encoding.Unicode.GetBytes(new Char[] { inputStr[i] }), 0) > 0x2E80 && BitConverter.ToUInt16(Encoding.Unicode.GetBytes(new Char[] { inputStr[i] }), 0) < 0xFE2F)
                {

                    return true;
                }
            }
            return false;
        }

    }
}
