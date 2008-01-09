using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;

namespace SMTP
{
    public partial class MainForm : Form
    {
        SMTPCommunicator sc;

        public MainForm()
        {
            InitializeComponent();
        }


        private bool receiverPanExpand;
        private bool autoScrollLog;
        private Thread threadSendMail;
        private delegate void delWriteLogToItemlist(string _log);
        private delegate void delSetProperty(string _key, object _value);
        private delegate object delGetProperty(string _key);
        private bool m_issending;
        private bool m_stopsending;
        private bool m_customizemessagebody;
        private AuthenticationLogin m_authLogin;
        private EventWaitHandle signalSendMail;
        private delSetProperty iDelSetProperty;
        private delGetProperty iDelGetProperty;
        private Commands.CommandManager cmdMgr;
        private Utility.TextboxAcceptKeyHelper acceptKeyHelper;
        private SMTPCommunicator cmdSMTPCommunicator;
        private Be.Windows.Forms.DynamicByteProvider textByteProvider;
        private byte[] messageBodyBytes;

        private void SMTP_Load(object sender, EventArgs e)
        {
            //Initializing...
            Initialize();

            

            

        }

        private void Initialize() {

            fileMain.FileOk += new CancelEventHandler(fileMain_FileOk);



            ExpandRecPanel(false);
            ScrollLog(true);
            

            cmdSMTPCommunicator = new SMTPCommunicator();
            m_authLogin = new AuthenticationLogin();
            signalSendMail = new EventWaitHandle(false, EventResetMode.ManualReset);
            iDelSetProperty = new delSetProperty(this.setProperty);
            iDelGetProperty = new delGetProperty(this.getProperty);
            cmdMgr = new SMTP.Commands.CommandManager();
            cmdMgr.UpdateMessage += new SMTP.Commands.CommandEvent.CommandEventHandler(OnCommandManagerUpdateMessage);
            cmdMgr.Attach(Commands.PackageCommands.Pack(this.cmdSMTPCommunicator));
            cmdMgr.Attach(new Commands.CmdHelp(cmdMgr));

            acceptKeyHelper = new SMTP.Utility.TextboxAcceptKeyHelper(this.txtCommand);
            acceptKeyHelper.AcceptButton = this.btnCommand;

            this.messageBodyBytes = new byte[1];
            textByteProvider = new Be.Windows.Forms.DynamicByteProvider(this.messageBodyBytes);

            this.hexMessage.ByteProvider = textByteProvider;

            this.CustomizeMessageBody = false;
        }



        #region Properties
        public int Progress {
            set {
                tspMain.Value = value;
            }
            get {
                return tspMain.Value;
            }
        }

        public int Count {
            get {
                return Int32.Parse(txtCount.Text);
            }
            set {
                txtCount.Text = value.ToString();
            }
        }

        public bool IsSending {
            get {
                return m_issending;
            }
            set {
                if (value == true)
                {
                    this.btnSend.Text = "Cancel";
                    this.txtInterval.ReadOnly = true;
                    this.txtCount.ReadOnly = true;
                }
                else
                {
                    this.btnSend.Text = "Send";
                    this.txtInterval.ReadOnly = false;
                    this.txtCount.ReadOnly = false;
                }
                m_issending = value;
            }
        }

        public bool StopSending {
            get
            {
                return m_stopsending;
            }
            set
            {
                m_stopsending = value;
            }
        }

        //A milliseconds interval between every talks to SMTP server.
        public int Interval {

            get
            {
                return Int32.Parse(txtInterval.Text);
            }
            set
            {
                txtInterval.Text = value.ToString();
            }
        }

        /// <summary>
        /// Send message body as what user inputs,
        /// SMTPCommunicator won't help for formatting message.
        /// </summary>
        public bool CustomizeMessageBody
        {
            set
            {
                m_customizemessagebody = value;
                btnEditorView.Enabled = value;
                btnDatagramView.Enabled = !value;
                btnBrowse.Enabled = !value;
                txtSubject.ReadOnly = value;
                chkBase64.Enabled = !value;
                chkMultiPart.Enabled = !value;
                hexMessage.Visible = value;
                txtMessage.Visible = !value;
            }
            get
            {
                return m_customizemessagebody;
            }
        }

        #endregion

        #region Events

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            lstLog.Items.Clear();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            fileMain.ShowDialog();

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            //-------------
            // TODO: Check user input
            //-------------
            //Use thread to send mail //SendMail();
            
            //setProperty("Progress", 100);
            if (IsSending == false)
            {
                try
                {
                    InputValidation();
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                    return;
                }
                IsSending = !IsSending;
                threadSendMail = new Thread(new ThreadStart(SendMail));
                threadSendMail.Start();
            }
            else {
                this.StopSending = true;
                signalSendMail.WaitOne(3000,false);
                threadSendMail.Join(1000);
                threadSendMail.Abort();
                OnSendCompleted();
            }

        }

        private void exitEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void fileMain_FileOk(object sender, CancelEventArgs e)
        {
            txtAttachment.Items.Add((ItemCollection.FileCollection)fileMain.FileName);
        }

        private void panReceiver_Resize(object sender, EventArgs e)
        {
            this.panMessageBody.Location = new Point(this.panMessageBody.Location.X, this.panReceiver.Location.Y + 2 + this.panReceiver.Size.Height);
        }

        private void btnToPlus_Click(object sender, EventArgs e)
        {
            ExpandRecPanel(!receiverPanExpand);
        }

        private void btnDatagramView_Click(object sender, EventArgs e)
        {
            CustomizeMessageBody = true;
        }

        private void txtCc_Enter(object sender, EventArgs e)
        {
            if (!this.receiverPanExpand)
                this.txtSubject.Focus();
        }

        private void btnScrollLog_Click(object sender, EventArgs e)
        {
            ScrollLog(!autoScrollLog);
        }

        private void btnEditorView_Click(object sender, EventArgs e)
        {
            CustomizeMessageBody = false;
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //-------------
            // TODO: Check out the core fields is entried.
            //-------------
            if (this.CustomizeMessageBody==true){
                MessageBox.Show("Could not support export data under datagram view mode");
                return;
            }

            sfdExport.FileOk += new CancelEventHandler(sfdExport_FileOk);
            sfdExport.ShowDialog();

        }

        void sfdExport_FileOk(object sender, CancelEventArgs e)
        {
            FileStream fs = new FileStream(sfdExport.FileName, FileMode.Create);
            fs.Close();

            //-------------
            // TODO: A new function to bind data from current form to SMTPCommunicator object
            //-------------
            SMTPCommunicator iSMTPC = new SMTPCommunicator();
            iSMTPC.To = this.txtTo.Text;
            iSMTPC.From = this.txtTo.Text;
            //Check whether to send bcc and cc
            if (receiverPanExpand)
            {
                if (this.txtCc.Text.Trim() != "")
                    iSMTPC.Cc = this.txtCc.Text.Trim();

                if (this.lblBcc.Text.Trim() != "")
                    iSMTPC.Bcc = this.txtBcc.Text.Trim();

            }
            iSMTPC.Subject = this.txtSubject.Text;

            if (this.CustomizeMessageBody)
                iSMTPC.Message = Encoding.Default.GetString(this.textByteProvider.Bytes.ToArray());
            else
                iSMTPC.Message = this.txtMessage.Text;

            int i;
            if (txtAttachment.Items.Count > 0)
            {

                //Clear attachment first
                iSMTPC.Attachments.Clear();
                for (i = 0; i < this.txtAttachment.Items.Count; i++)
                {
                    iSMTPC.Attachments.Add((SMTPCommunicator.Attachment)this.txtAttachment.Items[i].Path);
                }
            }
            iSMTPC.MessageBodySerialization(new SMTPCommunicator.delWriteData(this.ExportWriter));
        }

        private void btnSetAuth_Click(object sender, EventArgs e)
        {

            FormAuthenLogin al = new FormAuthenLogin(m_authLogin);
            if (al.ShowDialog() == DialogResult.OK)
            {
                chkAuthentication.Checked = true;
            }

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsSending)
            {
                this.StopSending = true;
                threadSendMail.Join(5000);
                threadSendMail.Abort();
            }
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Mailtro for SMTP tester\r\n by Norman Xu \r\n http://eroman.org \r\n http://blog.eroman.org");
        }

        private void onlineSupportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process ieProcess = new System.Diagnostics.Process();
            ieProcess.StartInfo.FileName = "explorer.exe";                                           // the executable file to start
            ieProcess.StartInfo.Arguments = " http://blog.eroman.org/";
            ieProcess.Start();
        }

        private void btnCommand_Click(object sender, EventArgs e)
        {
            cmdMgr.Run(txtCommand.Text);
            txtCommand.Text = "";
        }


        #endregion
        /////////////////////////////////GUI EVENT PROCESS END HERE/////////////////////////////////////

        //Write log to list.
        private void WriteLog(object sender, MessageEventArgs e) {
            string strLog = "";
            if (e.Type == MessageEventType.Log) {
                strLog += "  ";
            }
            else if (e.Type == MessageEventType.Error) {
                strLog += "!!!";
            }
            if (e.Message.Length > 255)
            {
                strLog += e.Message.Substring(0, 255);
            }
            else {
                strLog+=e.Message;
            }

            // Modified by Norman Xu 11/1/2007
            // Change Invoke to BeginInvoke.

            lstLog.BeginInvoke(new delWriteLogToItemlist(this.WriteLogToitemlist), strLog);
            
        }

        private void OnCommandManagerUpdateMessage(object sender, SMTP.Commands.CommandEvent.CommandEventArgs e)
        {
            MessageEventArgs mea = new MessageEventArgs(e.Message);
            mea.Type=(MessageEventType)(int)e.Type;
            this.WriteLog(sender, mea);
        }

        private void WriteLogToitemlist(string _log) {
            lstLog.Items.Add(_log);
            if (autoScrollLog)
            {
                lstLog.SelectedIndex = lstLog.Items.Count - 1;
                lstLog.SelectedIndex = -1;
            }
        }

        //UI control
        private void ExpandRecPanel(bool _sw) {
            if (_sw == true)
            {
                int i;
                for (i = 0; this.panReceiver.Size.Height < this.txtTo.Height * 3 + 7; i++)
                {
                    this.panReceiver.Size = new Size(this.panReceiver.Size.Width, panReceiver.Size.Height + 1);

                }
                this.btnToPlus.Text = "-";
                receiverPanExpand = true;
            }
            else {
                int i;
                if (this.panReceiver.Size.Height <= this.txtTo.Height + 5)
                    this.panReceiver.Size = new Size(this.panReceiver.Size.Width, this.txtTo.Height + 6);
                for (i = 0; this.panReceiver.Size.Height > this.txtTo.Height + 5; i--)
                {
                    this.panReceiver.Size = new Size(this.panReceiver.Size.Width, panReceiver.Size.Height - 1);
                }
                this.btnToPlus.Text = "+";
                receiverPanExpand = false;
            }
        }
        private void ScrollLog(bool _sw) {
            autoScrollLog = _sw;
            if (_sw == true)
                btnScrollLog.FlatStyle = FlatStyle.Flat;
            else
                btnScrollLog.FlatStyle = FlatStyle.Standard;

        }

        //Help for set and get property from another threads.
        private void setProperty(string _key, object _value)
        {
            PropertyInfo[] _tempProInfo = this.GetType().GetProperties();
            int i;
            for (i = 0; i < _tempProInfo.Length; i++)
            {
                if (_tempProInfo[i].Name == _key)
                    _tempProInfo[i].SetValue(this, _value, null);
            }
        }
        private object getProperty(string _key) {
            PropertyInfo[] _tempProInfo = this.GetType().GetProperties();
            int i;
            for (i = 0; i < _tempProInfo.Length; i++)
            {
                if (_tempProInfo[i].Name == _key)
                    break;
            }
            return _tempProInfo[i].GetValue(this, null);
        }

        private void SendMail() {
            int i;
            int count;
            int sleep;
            
            count = (int)stuMain.Invoke(iDelGetProperty, "Count");
            sleep = (int)stuMain.Invoke(iDelGetProperty, "Interval");

            //-------------
            // TODO: Change all read property method via delGetProperty
            //-------------

            sc = new SMTPCommunicator();
            sc.OnLogWrite += new MessageEventHandler(this.WriteLog); ;

            //If required authentication, add property.
            if (chkAuthentication.Checked == true)
            {
                sc.Authentication = this.m_authLogin;
            }
            if (chkTls.Checked == true) {
                sc.UseTls = true;
            }
            if (chkBase64.Checked == true) {
                sc.MessageEncoding = MessageEncoding.Base64;
            }
            if (chkMultiPart.Checked == true) {
                sc.EnforceMultiPart = true;
            }
            sc.IPAddress = this.txtSmtpServer.Text;
            sc.Port = Int32.Parse(this.txtPort.Text);
            sc.From = this.txtFrom.Text;
            sc.To = this.txtTo.Text;

            //Check whether to send bcc and cc
            if (receiverPanExpand)
            {
                if (this.txtCc.Text.Trim() != "")
                    sc.Cc = this.txtCc.Text.Trim();

                if (this.lblBcc.Text.Trim() != "")
                    sc.Bcc = this.txtBcc.Text.Trim();

            }

            sc.Subject = this.txtSubject.Text;
            sc.CustomizeMessageBody = (bool)this.Invoke(iDelGetProperty, "CustomizeMessageBody");
            if (sc.CustomizeMessageBody == true)
                sc.Message = Encoding.Default.GetString(this.textByteProvider.Bytes.ToArray());
            else
                sc.Message = this.txtMessage.Text;
            

            if (txtAttachment.Items.Count > 0)
            {
                
                //Clear attachment first
                sc.Attachments.Clear();
                for (i = 0; i < this.txtAttachment.Items.Count; i++)
                {
                    sc.Attachments.Add((SMTPCommunicator.Attachment)this.txtAttachment.Items[i].Path);
                }
            }

            object[] _obj = new object[] { "Progress", null };
            

            for (i = 0; i < count; i++)
            {
                //If StopSending property is set to true, stop send mail.
                if ((bool)stuMain.Invoke(iDelGetProperty, "StopSending"))
                    break;
                try
                {

                    sc.Open();
                    sc.Send();
                    _obj[1] = (int)(i * 100 / count);
                    stuMain.Invoke(iDelSetProperty, _obj);

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);

                }
                finally
                {
                    sc.Close();
                }

                //If current circle is the last one, do not sleep.
                if (i + 1 < count)
                {
                    Thread.CurrentThread.Join(sleep);
                }
            }
            signalSendMail.Set();
            OnSendCompleted();
        }

        private void OnSendCompleted() {
            stuMain.Invoke(iDelSetProperty, new object[] {"Progress",100 });
            stuMain.Invoke(iDelSetProperty, new object[] { "IsSending", false });
            stuMain.Invoke(iDelSetProperty, new object[] { "StopSending", false });
        }

        private void ExportWriter(string _input) {
            FileStream fs = new FileStream(sfdExport.FileName, FileMode.Append);
            byte[] inputBytes= Encoding.ASCII.GetBytes(_input + "\r\n");
            fs.Write(inputBytes, 0, inputBytes.Length);
            fs.Close();
        }

        private bool InputValidation() {
            if (txtTo.Text.Trim() == string.Empty)
            {
                throw new Exception("Recipient address not entered.");
                
            }
            if (txtPort.Text.Trim() == string.Empty)
            {
                throw new Exception("SMTP server port not entered.");
                
            }
            if (txtSmtpServer.Text.Trim() == string.Empty)
            {
                throw new Exception("SMTP server ip address not entered.");
                
            }
            return true;
        }





    }
}