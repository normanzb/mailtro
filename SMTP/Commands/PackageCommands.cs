using System;
using System.Collections.Generic;
using System.Text;

namespace SMTP.Commands
{
    class PackageCommands
    {
        public static Commandable[] Pack(SMTPCommunicator smtpCommunicator) {
            return new Commandable[] { new CmdOpen(smtpCommunicator),
                new CmdClose(smtpCommunicator),
                new CmdSay(smtpCommunicator),
                new CmdStartTls(smtpCommunicator)
            };
        }
    }
}
