using System;
using System.Collections.Generic;
using System.Text;

namespace SMTP.Commands
{
    class CmdStartTls:CmdSMTPCommunicatable
    {
        public CmdStartTls(SMTP.SMTPCommunicator smtpCommunicator)
            : base("StartTls", smtpCommunicator)
        {
            base.Command.Description = "Open or close TLS connection session.";
        }

        public override bool Run()
        {
            base.RunCommand(new DeleRun(this.Process));
            return true;
            
        }
        public bool Process() {
            if (base.Command.Parameters.Count < 1) {
                RaiseUpdateMessage(this, "Parameter required!\r\n" +
                    "Usage:\r\n " +
                    "\tstarttls true/false", SMTP.Commands.CommandEvent.CommandMessageType.Error);
                return false;
            }
            string tlsState = base.Command.Parameters[0];
            if (tlsState != "true" && tlsState != "false")
            {
                RaiseUpdateMessage(this, "Incorrect parameter!", SMTP.Commands.CommandEvent.CommandMessageType.Error);
                return false;
            }

            base.SMTPCommunicator.UseTls = bool.Parse(tlsState);

            RaiseUpdateMessage(this, "Tls state: " + tlsState, SMTP.Commands.CommandEvent.CommandMessageType.Information);

            return true;
        }
    }
}
