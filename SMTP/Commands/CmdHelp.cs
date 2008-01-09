using System;
using System.Collections.Generic;
using System.Text;

namespace SMTP.Commands
{
    class CmdHelp:Commandable
    {
        public CmdHelp(CommandManager commandManager)
            : base("Help")
        {
            this.CommandManager = commandManager;
            base.Command.Description = "Provide help information.";
        }

        private CommandManager _commandManager;
        private CommandManager CommandManager {
            set {
                this._commandManager = value;
            }
            get {
                return this._commandManager;
            }
        }
        
        public override bool Run()
        {
            base.RaiseUpdateMessage(this, "For more information about a specified command, type HELP COMMAND", SMTP.Commands.CommandEvent.CommandMessageType.Information);
            int i;
            for(i=0;i<this.CommandManager.Commands.Count;i++)
                base.RaiseUpdateMessage(this, this.CommandManager.Commands[i].Command.CommandName + "\t" + this.CommandManager.Commands[i].Command.Description, SMTP.Commands.CommandEvent.CommandMessageType.Information);


            return true;
        }

    }
}
