using System;
using System.Collections.Generic;
using System.Text;

namespace SMTP
{
    //Definition for smtp
    class SMTPDefinition
    {
        public static int ServerReady = 220;
        public static int ServerOK = 250;
        public static int ServerAccepted = 354;
        public static int ServerBye = 221;
        public static int ServerInvalidAddress = 501;
        public static int ServerAuthOK = 334;
        public static int ServerAuthSuccess = 235;
        public static int ServerAuthFailed = 535;
        public static int ServerAuthNotSupported = 530;
        public static string ClientHello = "HELO";
        public static string ClientAuthLogin = "AUTH LOGIN";
        public static string ClientAuthUser = "USER";
        public static string ClientAuthPass = "PASS";
        public static string ClientMailFrom = "MAIL FROM";
        public static string ClientReceiver = "RCPT TO";
        public static string ClientSendContent = "DATA";
        public static string ClientCFrom = "From";
        public static string ClientCTo = "To";
        public static string ClientCCc = "Cc";
        public static string ClientCSubject = "Subject";
        public static string ClientCQuit = ".";
        public static string ClientQuit = "QUIT";
        public static string ClientStartTls = "starttls";
        public static string ClientCContentType = "Content-Type";
        public struct ClientCContentTypeOption
        {
            public static string TextPlain = "text/plain";
            public static string MultipartMixed = "multipart/mixed";
            public static string AlternativeMessage = "multipart/alternative";//This is available for those email client do not support HTML viewer.
            public static string AplicationStream = "application/octet-stream";
        }

        public static string ClientCBoundaryName = "boundary";
        public class ClientCBoundary
        {
            public string Key;
            public string SubKey;
            public string Boundary
            {
                get
                {
                    return "----_=_NextPart_" + this.SubKey + "_" + this.Key;
                }
            }
            public string NormalBoundary
            {
                get
                {
                    return "------_=_NextPart_" + this.SubKey + "_" + this.Key;
                }
            }
            public string EndBoundary
            {
                get
                {
                    return "------_=_NextPart_" + this.SubKey + "_" + this.Key + "--";
                }
            }
        }

        public static string ClientCMIMEVersion = "MIME-Version";

        public static string ClientCContentTransferEncoding = "Content-transfer-encoding";
        public struct ClientCContentTransferEncodingOption
        {
            public static string QuotedPrintable = "quoted-printable";
            public static string Base64 = "base64";
        }

        public static string ClientCContentDescription = "Content-Description";

        public static string ClientCContentDisposition = "Content-Disposition";
        public struct ClientCContentDispositionOption {
            public static string Attachment = "attachment";
        }
    }



        public enum MessageEncoding
        {
            Printable,
            Base64
        }

}
