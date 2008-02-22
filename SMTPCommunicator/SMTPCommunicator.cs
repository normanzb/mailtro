using System;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
namespace SMTP
{
 

    //SMTP communicator class 
    public class SMTPCommunicator
    {
        #region Classes and structures
        /// //////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Data structure which respond from SMTP server
        /// </summary>
        /// Define responded data, splited as two part, code and message
        /// The smtp response code already defined in SMTPDefinition
        /// This struct used by Read() function for return a struct which contained rich information.
        public struct ResponseData
        {
            public int Code;
            public string Message;
        }

        /// //////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// SMTP client attachment class
        /// </summary>
        public class Attachment
        {
            private string m_path;
            public Attachment()
            {

            }
            public Attachment(string _path)
            {
                this.Path = _path;
            }
            public string Path
            {
                get
                {
                    return m_path;
                }
                set
                {
                    // Check the file specified in the path parameters exists. 
                    FileInfo fi = new FileInfo(value);
                    if (!fi.Exists)
                    {
                        throw new Exception(this.ToString() + "File not exist");
                    }
                    m_path = value;
                }
            }
            public string FileName
            {
                get
                {
                    FileInfo fi = new FileInfo(this.Path);

                    return fi.Name;

                }
            }
            //Allow explicit converse from string
            public static explicit operator Attachment(string _path)
            {
                return new Attachment(_path);
            }
        }
        #endregion

        #region Private variables

        /// //////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Private variables and instance
        /// </summary>
        private string m_ipaddress;
        private int m_port;
        private string m_from;
        private string m_to;
        private string m_cc;
        private string m_bcc;
        private string m_subject;
        private string m_message;
        private bool m_customizemessagebody;
        private AuthenticationLogin m_authlogin;
        private bool m_tls;
        private MessageEncoding m_messageencoding;
        private bool m_enforcemultipart;
        private string m_encodingName;

        //Tcp client support a easy way to communicate with TCP.
        private TcpClient smtpServer;
        private NetworkStream writeStream;
        private StreamReader readStream;
        private SslStream tlsStream;

        //Support a write log event.
        //Event is actually a delegate with special entry.
        public event MessageEventHandler OnLogWrite;

        //A delegate for Serialization function to write data.
        public delegate void delWriteData(string _input);

        public Collection<Attachment> Attachments;

        #endregion

        #region  Contructors
        /// //////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Initialize a new instance of SMTPCommunicator.
        /// </summary>
        public SMTPCommunicator() {
            this.Initialize();
      
        }

        /// //////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Initialize a new instance of SMTPCommunicator.
        /// </summary>
        /// <param name="_ipaddress">Remote server ip address</param>
        /// <param name="_port"></param>
        public SMTPCommunicator(string ipaddress, int port)
        {
            this.IPAddress = ipaddress;
            this.Port = port;
            this.Initialize();
        }
        #endregion

        #region Properties

        //Public properties

        /// <summary>
        /// Set or get remote SMTP server IP address.
        /// </summary>
        public string IPAddress {
            get {
                return m_ipaddress;
            }
            set {
                //-------------
                // TODO: Input validation here
                //-------------
                m_ipaddress = value;
            }
        }

        /// <summary>
        /// Set or get remote SMTP server port.
        /// </summary>
        public int Port {
            get {
                return m_port;
            }
            set {
                //-------------
                // TODO: Input validation here
                //-------------
                m_port = value;
            }
        }

        public string From {
            get {
                return m_from;
            }
            set {
                //-------------
                // TODO: Input validation here
                //-------------
                m_from = value;
            }
        }

        public string To {
            get
            {
                return m_to;
            }
            set
            {
                //-------------
                // TODO: Input validation here
                //-------------
                m_to = value;
            }
        }

        public string Cc {
            get
            {
                return m_cc;
            }
            set
            {
                //-------------
                // TODO: Input validation here
                //-------------
                m_cc = value;
            }
        }

        public string Bcc {

            get
            {
                return m_bcc;
            }
            set
            {
                //-------------
                // TODO: Input validation here
                //-------------
                m_bcc = value;
            }
        }

        public string Subject {
            get
            {
                return m_subject;
            }
            set
            {
                //-------------
                // TODO: Input validation here
                //-------------
                m_subject = value;
            }
        }

        public string Message {

            get
            {
                return m_message;
            }

            set
            {
                //-------------
                // TODO: Input validation here
                //-------------
                m_message = value;
            }

        }

        public AuthenticationLogin Authentication {

            get {
                return this.m_authlogin;
            }
            set {
                this.m_authlogin = value;
            }

        }

        public bool UseTls {
            get {
                return this.m_tls;
            }
            set {
                this.m_tls = value;
            }
        }

        /// <summary>
        /// Send Message property as entire message body, SMTPCommunicator won't generate message body from other property.
        /// </summary>
        public bool CustomizeMessageBody {
            set {
                m_customizemessagebody = value;
            }
            get {
                return m_customizemessagebody;
            }
        }

        //-------------
        // DEPRECATED: better use a new enum to described the encoding.
        //-------------
        public MessageEncoding MessageEncoding {
            set {
                this.m_messageencoding = value;
            }
            get {
                return this.m_messageencoding;
            }
        }

        public string EncodingName
        {
            set
            {
                this.m_encodingName = value;
            }
            get
            {
                return this.m_encodingName;
            }
        }

        /// <summary>
        /// Enforce to send a multipart email
        /// </summary>
        public bool EnforceMultiPart {
            set {
                this.m_enforcemultipart = value;
            }
            get {
                return this.m_enforcemultipart;
            }
        }

        #endregion

        /// //////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///  Initialize for private variables...
        /// </summary>
        private void Initialize()
        {
            Attachments = new Collection<Attachment>();
            this.MessageEncoding = MessageEncoding.Printable;
            this.EncodingName = Encoding.Default.EncodingName;
        }

        /// //////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Send smtp message
        /// </summary>
        public void Send() 
        {
            //-------------
            // TODO: Validate the From, To and Message already assigned value.
            //-------------


            //Write log
            this.WriteLog(this.ToString() + "Begin to send message..." + this.IPAddress + "", MessageEventType.Warning);

            ResponseData resData;
            resData = this.ReadInCommonWay();

            if (resData.Code != SMTPDefinition.ServerReady) {
                //-------------
                // TODO: Throw a exception to report 'server refused'
                //-------------
                this.WriteLog(this.ToString() + "The server refused to connection or the SMTP server not opened.", MessageEventType.Error);
                return;
            }

            //If require connection via tls
            if (this.UseTls == true)
            {
                //Conversation start//////////////////////////////////////////////////////////////////////////
                    //Helo in common way
                    this.WriteInCommonWay(SMTPDefinition.ClientHello + (this.Authentication != null &&
                        this.Authentication.User.Trim() != string.Empty ? " " + System.Environment.MachineName : ""));
                    //Check whether server response correctly!
                    if (this.ReadInCommonWay().Code != SMTPDefinition.ServerOK)
                    {
                        //-------------
                        // TODO: Throw a exception to report 'not correct server'
                        //-------------
                        this.WriteLog(this.ToString() + "The server could not response correctly, might be not a SMTP server.", MessageEventType.Error);
                        return;
                    }
                //Conversation end////////////////////////////////////////////////////////////////////////////

                //Conversation start//////////////////////////////////////////////////////////////////////////
                    this.WriteInCommonWay(SMTPDefinition.ClientStartTls);
                    //Check whether server response correctly!
                    if (this.ReadInCommonWay().Code != SMTPDefinition.ServerReady)
                    {
                        //-------------
                        // TODO: Throw a exception to report 'not correct server'
                        //-------------
                        this.WriteLog(this.ToString() + "The server could not response correctly, or might not support authentication.", MessageEventType.Error);
                        return;
                    }
                //Conversation end////////////////////////////////////////////////////////////////////////////
            }

            // Send 'HELO' first, if authentication is required, add host name
            this.Write(SMTPDefinition.ClientHello + (this.Authentication != null &&
                this.Authentication.User.Trim() != string.Empty? " " + System.Environment.MachineName:""));

            //Check whether server response correctly!
            if (this.Read().Code != SMTPDefinition.ServerOK)
            {
                //-------------
                // TODO: Throw a exception to report 'not correct server'
                //-------------
                this.WriteLog(this.ToString() + "The server could not response correctly, might be not a SMTP server.", MessageEventType.Error);
                return;
            }
         
            //If authentication is required, write authentication stream first
            if (this.Authentication != null && this.Authentication.User.Trim() != string.Empty)
            {
                //Conversation start//////////////////////////////////////////////////////////////////////////
                this.Write(SMTPDefinition.ClientAuthLogin);
                //Check whether server response correctly!
                if (this.Read().Code != SMTPDefinition.ServerAuthOK)
                {
                    //-------------
                    // TODO: Throw a exception to report 'not correct server'
                    //-------------
                    this.WriteLog(this.ToString() + "The server could not response correctly, or might not support authentication.", MessageEventType.Error);
                    return;
                }
                //Conversation end////////////////////////////////////////////////////////////////////////////

                //Conversation start//////////////////////////////////////////////////////////////////////////

                //NONTLS USE THIS: this.Write(SMTPDefinition.ClientAuthUser + " " + SMTP.Utility.Base64Convertor.Convert(this.Authentication.User,Encoding.ASCII));
                this.Write(SMTP.Utility.Base64Convertor.Convert(this.Authentication.User, Encoding.ASCII));
                //Check whether server response correctly!
                if (this.Read().Code != SMTPDefinition.ServerAuthOK)
                {
                    //-------------
                    // TODO: Throw a exception to report 'not correct server'
                    //-------------
                    this.WriteLog(this.ToString() + "The server could not response correctly, or might not support authentication.", MessageEventType.Error);
                    return;
                }
                //Conversation end////////////////////////////////////////////////////////////////////////////

                //Conversation start//////////////////////////////////////////////////////////////////////////
                //NON-TLS USE THIS: this.Write(SMTPDefinition.ClientAuthPass + " " + SMTP.Utility.Base64Convertor.Convert(this.Authentication.Password, Encoding.ASCII));
                this.Write(SMTP.Utility.Base64Convertor.Convert(this.Authentication.Password, Encoding.ASCII));
                //Check whether server response correctly!
                int temCode = this.Read().Code;
                if (temCode != SMTPDefinition.ServerAuthOK && temCode!= SMTPDefinition.ServerAuthSuccess)
                {
                    if (temCode == SMTPDefinition.ServerAuthFailed)
                    {
                        //-------------
                        // TODO: Throw a exception to report 'not correct server'
                        //-------------
                        this.WriteLog(this.ToString() + "User name doesn't exist or password incorrect.", MessageEventType.Error);
                        return;
                    }
                    else if (temCode == SMTPDefinition.ServerAuthNotSupported)
                    {
                        //-------------
                        // TODO: Throw a exception to report 'not correct server'
                        //-------------
                        this.WriteLog(this.ToString() + "The server could not response correctly, or might not support authentication.", MessageEventType.Error);
                        return;
                    }
                    else {
                        //-------------
                        // TODO: Throw a exception to report 'not correct server'
                        //-------------
                        this.WriteLog(this.ToString() + "The server could not response correctly, or might not support authentication.", MessageEventType.Error);
                        return;
                    }
                }
                //Conversation end////////////////////////////////////////////////////////////////////////////

            }

            // Send 'From' E-mail Address
            this.Write(SMTPDefinition.ClientMailFrom + ":" + SMTP.Utility.MailAddressFormator.Format(this.From));

            resData = this.Read();
            if (resData.Code != SMTPDefinition.ServerOK)
            {
                if (resData.Code == SMTPDefinition.ServerInvalidAddress)
                {
                    this.WriteLog(this.ToString() + "Invalid sender address!", MessageEventType.Error);
                }
                else
                {
                    //-------------
                    // TODO: Throw a exception to report 'not correct server'
                    //-------------
                    this.WriteLog(this.ToString() + "The server could not response correctly, might be not a SMTP server.", MessageEventType.Error);
                }
                return;
            }


            //-------------
            // TODO: Support multi-receiver,(TO,cc,bcc) !!!
            // Support display name. !!!!
            //-------------
            // Sending 'To' E-mail Address
            this.Write(SMTPDefinition.ClientReceiver + ":" + SMTP.Utility.MailAddressFormator.Format(this.To));

            resData  =this.Read();
            if (resData.Code != SMTPDefinition.ServerOK)
            {
                if (resData.Code == SMTPDefinition.ServerInvalidAddress)
                {
                    this.WriteLog(this.ToString() + "Invalid receiver address!", MessageEventType.Error);
                }
                else
                {
                    //-------------
                    // TODO: Throw a exception to report 'not correct server'
                    //-------------
                    this.WriteLog(this.ToString() + "The server could not response correctly, might be not a SMTP server.", MessageEventType.Error);
                }
                return;
            }

            // If the CC and BCC do not assigned value, send message directly, or else do more communication.
            if (this.Cc != null && this.Cc.Trim() != "")
            {
                this.Write(SMTPDefinition.ClientReceiver + ":" + SMTP.Utility.MailAddressFormator.Format(this.Cc));
                resData = this.Read();
                if (resData.Code != SMTPDefinition.ServerOK)
                {
                    if (resData.Code == SMTPDefinition.ServerInvalidAddress)
                    {
                        this.WriteLog(this.ToString() + "Invalid receiver address!", MessageEventType.Error);
                    }
                    else
                    {
                        //-------------
                        // TODO: Throw a exception to report 'not correct server'
                        //-------------
                        this.WriteLog(this.ToString() + "The server could not response correctly, might be not a SMTP server.", MessageEventType.Error);
                    }
                    return;
                }

            }

            if (this.Bcc != null && this.Bcc.Trim() != "")
            {
                this.Write(SMTPDefinition.ClientReceiver + ":" + SMTP.Utility.MailAddressFormator.Format(this.Bcc));
                resData = this.Read();
                if (resData.Code != SMTPDefinition.ServerOK)
                {
                    if (resData.Code == SMTPDefinition.ServerInvalidAddress)
                    {
                        this.WriteLog(this.ToString() + "Invalid receiver address!", MessageEventType.Error);
                    }
                    else
                    {
                        //-------------
                        // TODO: Throw a exception to report 'not correct server'
                        //-------------
                        this.WriteLog(this.ToString() + "The server could not response correctly, might be not a SMTP server.", MessageEventType.Error);
                    }
                    return;
                }

            }

            // Send data
            this.Write(SMTPDefinition.ClientSendContent);

            if (this.Read().Code != SMTPDefinition.ServerAccepted)
            {
                //-------------
                // TODO: Throw a exception to report 'Do not have permission to send message via this smtp server'
                //-------------
                this.WriteLog(this.ToString() + "Do not have permission to send message via this smtp server.", MessageEventType.Error);
                return;
            }

            //Send Message Body
            //If CustomizeMessageBody is true send this.Message directly
            if (this.CustomizeMessageBody)
                this.Write(this.Message);
            else
                this.MessageBodySerialization(new delWriteData(this.Write));

            //End of message body
            this.Write(SMTPDefinition.ClientCQuit);

            if (this.Read().Code != SMTPDefinition.ServerOK)
            {
                
                this.WriteLog(this.ToString() + "Message sending is failed!", MessageEventType.Error);
                return;
            }

            // Sending Disconnected Message to Server
            this.Write(SMTPDefinition.ClientQuit);
            this.Read();

        }

        // The following method is invoked by the RemoteCertificateValidationDelegate.
        public bool ValidateServerCertificate(
              object sender,
              X509Certificate certificate,
              X509Chain chain,
              SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            //Console.WriteLine("Certificate error: {0}", sslPolicyErrors);

            // Do not allow this client to communicate with unauthenticated servers.
            return true;
        }


        public ResponseData Read() {
            if (this.UseTls == true)
            {
                return this.ReadTls();
            }
            else {
                return this.ReadInCommonWay();
            }

            
        }

        public ResponseData ReadInCommonWay() {
            ResponseData resData = new ResponseData();

            byte[] buffer = new byte[1];

            StringBuilder msg = new StringBuilder();
            int i = 0;
            do
            {
                i = writeStream.Read(buffer, 0, 1);
                msg.Append(Convert.ToChar(buffer[0]));
            }
            while (writeStream.DataAvailable);


            resData.Message = msg.ToString();
            //-------------
            // TODO: Make sure message is a correctly responded with a code.
            //-------------
            resData.Code = Int32.Parse(resData.Message.Substring(0, 3));

            this.WriteLog(resData.Message);
            return resData;
        }

        public ResponseData ReadTls()
        {
            CreateSslInstance();

            ResponseData resData = new ResponseData();

            resData.Message = ReadSslStream(tlsStream);
            //-------------
            // TODO: Make sure message is a correctly responded with a code.
            //-------------
            resData.Code = Int32.Parse(resData.Message.Substring(0, 3));

            this.WriteLog(resData.Message);
            return resData;
        }

        //Stream reader for sslstream
        //TODO: place this functionto a utility class
        private string ReadSslStream(SslStream sslStream) {
            // Read the  message sent by the server.
            // The end of the message is signaled using the
            // "<EOF>" marker.
            byte[] buffer = new byte[2048];
            StringBuilder messageData = new StringBuilder();
            int bytes = -1;
            do
            {
                bytes = sslStream.Read(buffer, 0, buffer.Length);

                if (bytes == 0)
                    break;
                // Use Decoder class to convert from bytes to UTF8
                // in case a character spans two buffers.
                Decoder decoder = Encoding.ASCII.GetDecoder();
                char[] chars = new char[decoder.GetCharCount(buffer, 0, bytes)];
                decoder.GetChars(buffer, 0, bytes, chars, 0);
                messageData.Append(chars);
                // Check for EOF.
                if (messageData.ToString().IndexOf("\n") != -1)
                {
                    break;
                }
                bytes = bytes - messageData.Length;
            } while (bytes > 0);

            return messageData.ToString();

        }

        public void Write(string _input) {
            if (this.UseTls == true)
            {
                this.WriteTls(_input);
            }
            else {
                this.WriteInCommonWay(_input);
            }
            
        }

        public void WriteInCommonWay(string _input)
        {
            string sendString = _input + "\r\n";
            byte[] dataToSend = Encoding.ASCII.GetBytes(sendString);
            writeStream.Write(dataToSend, 0, dataToSend.Length);
            writeStream.Flush();

            this.WriteLog(_input);
        }

        public void WriteTls(string _input)
        {
            CreateSslInstance();

            string sendString = _input + "\r\n";
            byte[] dataToSend = Encoding.ASCII.GetBytes(sendString);
            tlsStream.Write(dataToSend, 0, dataToSend.Length);
            tlsStream.Flush();

            this.WriteLog(_input);
        }

        private void CreateSslInstance() {
            if (tlsStream == null || tlsStream.IsAuthenticated == false)
            {
                tlsStream = new SslStream(smtpServer.GetStream(), false, new RemoteCertificateValidationCallback(ValidateServerCertificate),
                        null);
                //if success start tls authentication
                tlsStream.AuthenticateAsClient(this.IPAddress);
            }
        }

        //Open/Close connection to SMTP server
        public void Open()
        {
            //-------------
            // TODO: Check ip and port 
            //-------------

            // Create an instance of the TcpClient class
            smtpServer = new TcpClient(this.IPAddress, this.Port);

            //If UseTls is true, use sslstream to handle stream;
            if (this.UseTls)
            {
                
                //tlsStream.AuthenticateAsClient(this.IPAddress);

            }

            // Create the stream classes for communication
            writeStream = smtpServer.GetStream();
            readStream = new StreamReader(smtpServer.GetStream());


            //-------------
            // TODO: Write log if a exception throw(server refuse to connect)
            //-------------
            //Write log
            this.WriteLog(this.ToString() + "Connection established with " + this.IPAddress + "", MessageEventType.Warning);

        }

        //Open/Close connection to SMTP server
        public void Close()
        {
            if (tlsStream != null)
            {
                tlsStream.Close();
            }
            if (writeStream != null)
            {
                writeStream.Close();
            }
            if (readStream != null)
            {
                readStream.Close();
            }
            if (smtpServer != null)
            {
                smtpServer.Close();
            }

            //Write log
            this.WriteLog(this.ToString() + "Connection closed from " + this.IPAddress + "", MessageEventType.Warning);
        }

        // Dealing with Message Body
        public void MessageBodySerialization(delWriteData _idelwritedata) {
            //-------------
            // TODO: If the display name assigned value, set from, to to display name
            //       If the display name not assigned value, use the default value (From, To);
            //-------------
            _idelwritedata(SMTPDefinition.ClientCFrom + ": " + this.From);
            _idelwritedata(SMTPDefinition.ClientCTo + ": " + this.To);

            //If subject contained non-English character
            if (SMTP.Utility.StringDetectors.NonEnglishChar(this.Subject))
                _idelwritedata(SMTPDefinition.ClientCSubject + ": =?" + this.EncodingName + "?B?" + SMTP.Utility.Base64Convertor.Convert(this.Subject, this.EncodingName) + "?=");
            else
                _idelwritedata(SMTPDefinition.ClientCSubject + ": " + this.Subject);

            if (this.Cc != null && this.Cc.Trim() != "")
            {
                _idelwritedata(SMTPDefinition.ClientCCc + ": " + this.Cc);
            }
            //if file attached, use multipart/mixed to send message, or else sending message as the simplest way.
            if (this.Attachments.Count > 0 || this.EnforceMultiPart)
            {
                //-------------
                // TODO: A MIME version controller.
                //-------------
                _idelwritedata(SMTPDefinition.ClientCMIMEVersion + ": 1.0");

                _idelwritedata(SMTPDefinition.ClientCContentType + ": " + SMTPDefinition.ClientCContentTypeOption.MultipartMixed + ";");

                //Generate a boundary
                SMTPDefinition.ClientCBoundary currBoundary = new SMTPDefinition.ClientCBoundary();
                //-------------
                // TODO: boundary key generator, generate a key which never appeared in the other part of mail message.
                // Sample "01C73E90.8304A050"
                //-------------
                currBoundary.Key = "01C73E90.8304A050";
                currBoundary.SubKey = "001";
                _idelwritedata("\t" + SMTPDefinition.ClientCBoundaryName + "=\"" + currBoundary.Boundary + "\"");

                //Display a message if user client do not support Multi-part format.
                _idelwritedata("");
                _idelwritedata("This is a multi-part message in MIME format.");
                _idelwritedata("");

                //Begin to write content.
                _idelwritedata(currBoundary.NormalBoundary);
                //-------------
                // TODO: get content type from registry and add some logical for the multi part message generator.
                //..............And the charset
                //-------------
                _idelwritedata(SMTPDefinition.ClientCContentType + ": " + SMTPDefinition.ClientCContentTypeOption.TextPlain + "; charset=\"" + this.EncodingName + "\"");

                //if require base64
                if (this.MessageEncoding == MessageEncoding.Base64 || SMTP.Utility.StringDetectors.NonEnglishChar(this.Message))
                {
                    _idelwritedata(SMTPDefinition.ClientCContentTransferEncoding + ": " + SMTPDefinition.ClientCContentTransferEncodingOption.Base64);
                    _idelwritedata("");
                    _idelwritedata(SMTP.Utility.Base64Convertor.Convert(this.Message, this.EncodingName));
                    
                }
                else if (this.MessageEncoding == MessageEncoding.Printable)
                {
                    _idelwritedata(SMTPDefinition.ClientCContentTransferEncoding + ": " + SMTPDefinition.ClientCContentTransferEncodingOption.QuotedPrintable);
                    _idelwritedata("");
                    _idelwritedata(this.Message);
                }
                

                //Write file streams
                int i;
                for (i = 0; i < this.Attachments.Count; i++)
                {
                    _idelwritedata(currBoundary.NormalBoundary);

                    _idelwritedata(SMTPDefinition.ClientCContentType + ": " + SMTPDefinition.ClientCContentTypeOption.AplicationStream + ";\r\n\tname=\"" +
                        (this.MessageEncoding == MessageEncoding.Base64 || SMTP.Utility.StringDetectors.NonEnglishChar(this.Attachments[i].FileName) ?
                        "=?" + this.EncodingName + "?b?" + SMTP.Utility.Base64Convertor.Convert(this.Attachments[i].FileName) + "?=" : 
                        this.Attachments[i].FileName + "\""));

                    _idelwritedata(SMTPDefinition.ClientCContentTransferEncoding + ": " + SMTPDefinition.ClientCContentTransferEncodingOption.Base64);
                    //If non-english characters are found
                    if (this.MessageEncoding == MessageEncoding.Base64 || SMTP.Utility.StringDetectors.NonEnglishChar(this.Attachments[i].FileName))
                        _idelwritedata(SMTPDefinition.ClientCContentDescription + ": " + "=?" + this.EncodingName + "?b?" + SMTP.Utility.Base64Convertor.Convert(this.Attachments[i].FileName) + "?=");
                    else
                        _idelwritedata(SMTPDefinition.ClientCContentDescription + ": " + this.Attachments[i].FileName);

                    _idelwritedata(SMTPDefinition.ClientCContentDisposition + ": " + SMTPDefinition.ClientCContentDispositionOption.Attachment + "; filename=\"" +
                        (this.MessageEncoding == MessageEncoding.Base64 || SMTP.Utility.StringDetectors.NonEnglishChar(this.Attachments[i].FileName) ?
                        "=?" + this.EncodingName + "?b?" + SMTP.Utility.Base64Convertor.Convert(this.Attachments[i].FileName) + "?=" : 
                        this.Attachments[i].FileName + "\""));
                    _idelwritedata("");
                    _idelwritedata(this.EncodeFile(this.Attachments[i].Path));
                }

                _idelwritedata(currBoundary.EndBoundary);
            }
            //Text/plain mode
            else
            {
                //if require base64
                if (this.MessageEncoding == MessageEncoding.Base64 || SMTP.Utility.StringDetectors.NonEnglishChar(this.Message))
                {
                    _idelwritedata(SMTPDefinition.ClientCContentType + ": " + SMTPDefinition.ClientCContentTypeOption.TextPlain + "; charset=\"" + this.EncodingName + "\"");
                    _idelwritedata(SMTPDefinition.ClientCContentTransferEncoding + ": " + SMTPDefinition.ClientCContentTransferEncodingOption.Base64);
                    _idelwritedata("");
                    _idelwritedata(SMTP.Utility.Base64Convertor.Convert(this.Message, this.EncodingName));
                }
                else if (this.MessageEncoding== MessageEncoding.Printable)
                {
                    _idelwritedata("");
                    _idelwritedata(this.Message);
                }
            }

            

        }
        public void MessageBodyUnserialization(SMTPCommunicator _ismtpcommunicator) { 
        
        }

        //Convert a file to a base64 encoded string.
        private string EncodeFile(string _path){
            FileInfo fi = new FileInfo(_path);
            int fileSize = (int)fi.Length;
            FileStream fs = new FileStream(_path, FileMode.Open);
            byte[] dataFromFile = new byte[fileSize];
            fs.Read(dataFromFile,0, fileSize);
            fs.Close();
            return Convert.ToBase64String(dataFromFile);
        }

        private void WriteLog(string _log){
            WriteLog(_log, MessageEventType.Log);
        }
        private void WriteLog(string _log, MessageEventType _logType)
        {
            //If user has not defined the event. do not write log.
            if (null != this.OnLogWrite)
            {
                MessageEventArgs EventMessage = new MessageEventArgs(_log);
                EventMessage.Type = _logType;
                this.OnLogWrite("nscLibrary.Utility.SMTPCommunicator", EventMessage);
            }
        }

        public override string ToString() {
            return "SMTPCommunicator: ";
        }

        
    }



}
