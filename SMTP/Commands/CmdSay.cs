using System;
using System.Collections.Generic;
using System.Text;

namespace SMTP.Commands
{
    class CmdSay:CmdSMTPCommunicatable
    {
        public CmdSay(SMTP.SMTPCommunicator smtpCommunicator)
            : base("Say", smtpCommunicator)
        {
            base.Command.Description = "Send string to SMTP server and wait for its response.";
        }
        public override bool Run()
        {

            base.RunCommand(new DeleRun(this.Process));

            return true;
        }

        public bool Process() {
            StringBuilder temCommunicateStr = new StringBuilder();
            int i;
            for (i = 0; i < base.Command.Parameters.Count; i++)
            {
                temCommunicateStr.Append(base.Command.Parameters[i] + " ");
            }


            base.SMTPCommunicator.Write(temCommunicateStr.ToString());
            base.SMTPCommunicator.Read();


            return true;
        }
    }
}
