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
        
        // Thread for sending mails.
        Utility.ThreadHelper.ThreadHelper threadSendMail;
        //private Thread threadSendMail;

        private delegate void delWriteLogToItemlist(string _log);
        private bool m_issending;
        private bool m_customizemessagebody;
        private AuthenticationLogin m_authLogin;
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
            // TODO: Move this line to design.cs
            fileMain.FileOk += new CancelEventHandler(fileMain_FileOk);

            ExpandRecPanel(false);

            // TODO: Change to property
            ScrollLog(true);
            
            // Initialize for command mode
            cmdSMTPCommunicator = new SMTPCommunicator();
            m_authLogin = new AuthenticationLogin();
            cmdMgr = new SMTP.Commands.CommandManager();
            cmdMgr.UpdateMessage += new SMTP.Commands.CommandEvent.CommandEventHandler(OnCommandManagerUpdateMessage);
            cmdMgr.Attach(Commands.PackageCommands.Pack(this.cmdSMTPCommunicator));
            cmdMgr.Attach(new Commands.CmdHelp(cmdMgr));

            // Set accept key word command textbox
            acceptKeyHelper = new SMTP.Utility.TextboxAcceptKeyHelper(this.txtCommand);
            acceptKeyHelper.AcceptButton = this.btnCommand;

            // Initialize hex editor
            this.messageBodyBytes = new byte[0];
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

        #region Process Events

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

            if (IsSending == false)
            {
                // Validate user input
                try
                {
                    InputValidation();
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                    return;
                }

                IsSending = !IsSending;

                Utility.ThreadHelper.ThreadHelper.DelProcess ProcessDelegate = 
                    new SMTP.Utility.ThreadHelper.ThreadHelper.DelProcess(this.SendMailProcess);
                Utility.ThreadHelper.ThreadHelper.StandardParameters sParams =
                    new SMTP.Utility.ThreadHelper.ThreadHelper.StandardParameters();
                threadSendMail = new SMTP.Utility.ThreadHelper.ThreadHelper(ProcessDelegate);
                threadSendMail.Run(sParams);

                //threadSendMail = new Thread(new ThreadStart(SendMail));
                //threadSendMail.Start();
            }
            else {
                this.CancelSending();
            }

        }

        private void threadSendMail_BeforeAbort(object sender, EventArgs e)
        {
            // Close connection before abort.
            if (sc.State != SMTPCommunicator.ConnectionState.Closed &&
                sc.State != SMTPCommunicator.ConnectionState.NotStart)
            {
                sc.Close();
            }

        }

        private void exitEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fileMain_FileOk(object sender, CancelEventArgs e)
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

            // Encoding
            string sMailEncodingName = this.cmbEncodingName.Text;
            if (sMailEncodingName.ToLower() == "default")
            {
                sMailEncodingName = Encoding.Default.HeaderName;
            }
            iSMTPC.EncodingName = sMailEncodingName;

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
                this.CancelSending();
            }
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GUIs.FormAbout fAbout = new SMTP.GUIs.FormAbout();
            fAbout.ShowDialog();
        }

        private void onlineSupportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process ieProcess = new System.Diagnostics.Process();
            ieProcess.StartInfo.FileName = "explorer.exe";                                           // the executable file to start
            ieProcess.StartInfo.Arguments = " http://mailtro.googlecode.com";
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

        private void SendMailProcess(Utility.ThreadHelper.ThreadHelper.StandardParameters sParams)
        {
            int i;
            int count;
            int sleep;
            
            count = (int)Utility.ThreadHelper.CrossThreadPropertyHelper.GetProperty(this, "Count");
            sleep = (int)Utility.ThreadHelper.CrossThreadPropertyHelper.GetProperty(this, "Interval");

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

            string sMailEncodingName = SMTP.Utility.ThreadHelper.CrossThreadPropertyHelper.GetProperty(this.cmbEncodingName, "Text").ToString();
            if (sMailEncodingName.ToLower() == "default")
            {
                sMailEncodingName = Encoding.Default.HeaderName;
            }
            sc.EncodingName = sMailEncodingName;

            //Check whether to send bcc and cc
            if (receiverPanExpand)
            {
                if (this.txtCc.Text.Trim() != "")
                    sc.Cc = this.txtCc.Text.Trim();

                if (this.lblBcc.Text.Trim() != "")
                    sc.Bcc = this.txtBcc.Text.Trim();

            }

            sc.Subject = this.txtSubject.Text;
            sc.CustomizeMessageBody = (bool)Utility.ThreadHelper.CrossThreadPropertyHelper.GetProperty(this, "CustomizeMessageBody");
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

            for (i = 0; i < count; i++)
            {
                //If StopSending property is set to true, stop send mail.
                if (threadSendMail.CancelSignal.Get())
                {
                    break;
                }

                try
                {
                    sc.Open();
                    sc.Send();
                }
                catch (Exception ex)
                {
                    // if the canceling signal is not set,
                    // then it must be a unexpected exception.
                    if (!threadSendMail.CancelSignal.Get())
                    {
                        // TODO: add ability to log list to view all text in it.
                        this.WriteLogToitemlist(ex.Message);
                    }
                }
                finally
                {
                    //Utility.ThreadHelper.CrossThreadPropertyHelper.SetProperty(this, "Progress", (int)(i * 100 / count));
                    sc.Close();
                }

                //If current circle is the last one, do not sleep.
                if (i + 1 < count)
                {
                    Thread.CurrentThread.Join(sleep);
                }
            }
            OnSendCompleted();
        }

        private void CancelSending()
        {

            threadSendMail.BeforeAbort += new EventHandler(threadSendMail_BeforeAbort);

            // Send cancel sigal.
            threadSendMail.Cancel();
            OnSendCompleted();

            this.WriteLogToitemlist("Job has been canceled.");
        }

        private void OnSendCompleted() {
            Utility.ThreadHelper.CrossThreadPropertyHelper.BeginSetProperty(this, "Progress", 100 );
            Utility.ThreadHelper.CrossThreadPropertyHelper.BeginSetProperty(this, "IsSending", false);
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
            if (cmbEncodingName.Text.Trim() == string.Empty)
            {
                throw new Exception("Please select a encoding.");
            }
            return true;
        }





    }
}