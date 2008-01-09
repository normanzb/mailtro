using System;
using System.Collections.Generic;
using System.Text;

namespace SMTP.Commands
{
    class CmdSMTPCommunicatable:Commandable
    {
        public delegate bool DeleRun();

        private SMTP.SMTPCommunicator _smtpCommunicator;
        public CmdSMTPCommunicatable(string commandName, SMTP.SMTPCommunicator smtpCommunicator):base(commandName) {

            this.SMTPCommunicator = smtpCommunicator;
            

        }

        public SMTP.SMTPCommunicator SMTPCommunicator {
            get {
                return this._smtpCommunicator;
            }
            set {
                this._smtpCommunicator = value;
            }
        }

        public void EnableProcessMessage() {
            this.SMTPCommunicator.OnLogWrite += new MessageEventHandler(OnLogWrite);
        }

        public void DisableProcessMessage() {
            this.SMTPCommunicator.OnLogWrite -= new MessageEventHandler(OnLogWrite);
        }

        public bool RunCommand(DeleRun delRun) {
            this.EnableProcessMessage();
            try
            {
                delRun();
            }
            catch (Exception ex)
            {
                base.RaiseUpdateMessage(this, ex.Message, SMTP.Commands.CommandEvent.CommandMessageType.Error);
            }
            finally
            {
                this.DisableProcessMessage();
            }
            return true;
        }

        private void OnLogWrite(object o, MessageEventArgs e)
        {
            CommandEvent.CommandMessageType cmt = (CommandEvent.CommandMessageType)(int)e.Type;
            base.RaiseUpdateMessage(this, e.Message, cmt);
        }

    }
}
