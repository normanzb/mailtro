using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SMTP.Commands
{
    class Commandable
    {

        public event CommandEvent.CommandEventHandler UpdateMessage;

        public Commandable(string commandName) {

            Initialize();
            this.Command.CommandName = commandName.Trim();

        }

        private void Initialize(){

            this.Command = new Command();

        }

        private Command _command;
        public Command Command {
            get {
                return this._command;
            }
            set {
                this._command = value;
            }
        }

        public virtual bool Run() {

            return true;
        }

        public void RaiseUpdateMessage(object sender, string message, CommandEvent.CommandMessageType type){
            CommandEvent.CommandEventArgs cea = new SMTP.Commands.CommandEvent.CommandEventArgs(message);
            cea.Type= type;
            this.UpdateMessage(sender, cea);
        }
    }
}
