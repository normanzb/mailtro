using System;
using System.Collections.Generic;
using System.Text;

namespace SMTP.Utility
{

    class NonEnglishCharDetector
    {
        public static bool Detect(string inputStr) {
            int i;
            for (i=0;i<inputStr.Length;i++){

                if (BitConverter.ToUInt16(Encoding.Unicode.GetBytes(new Char[] { inputStr[i] }), 0) > 0x2E80 && BitConverter.ToUInt16(Encoding.Unicode.GetBytes(new Char[] { inputStr[i] }), 0) < 0xFE2F)
                {

                    return true;
                }
            }
                return false;
        }
    }
}
