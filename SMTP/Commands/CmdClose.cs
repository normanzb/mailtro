using System;
using System.Collections.Generic;
using System.Text;

namespace SMTP.Commands
{
    class CmdClose:CmdSMTPCommunicatable
    {
        public CmdClose(SMTP.SMTPCommunicator smtpCommunicator)
            : base("Close", smtpCommunicator)
        {
            base.Command.Description = "Close the connection to a SMTP server.";
        }

        public override bool Run()
        {

            base.RunCommand(new DeleRun(this.Process));

            return true;
        }

        private bool Process() {

            base.SMTPCommunicator.Close();

            return true;
        }
    }
}
