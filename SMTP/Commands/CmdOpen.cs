using System;
using System.Collections.Generic;
using System.Text;

namespace SMTP.Commands
{
    class CmdOpen:CmdSMTPCommunicatable
    {
        public CmdOpen(SMTP.SMTPCommunicator smtpCommunicator)
            : base("open", smtpCommunicator)
        {
            base.Command.Description = "Open a connection to a STMP server.";
        }
        
        public override bool  Run()
        {

            base.RunCommand(new DeleRun(this.Process));

            return true;
   
        }
        public bool Process()
        {

            base.SMTPCommunicator.IPAddress = base.Command.Parameters[0];
            base.SMTPCommunicator.Port = Int32.Parse(base.Command.Parameters[1]);

            base.SMTPCommunicator.UseTls = false;
            base.SMTPCommunicator.Open();
            base.SMTPCommunicator.Read();

            return true;

        }

    }
}
