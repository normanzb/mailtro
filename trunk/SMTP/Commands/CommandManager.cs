using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SMTP.Commands
{
    class CommandManager: Commandable
    {
        private Collection<Commandable> _commandCollection;

        public Collection<Commandable> Commands {
            get {
                return this._commandCollection;
            }
            set {
                this._commandCollection = value;
            }
        }

        public CommandManager():base("CommandManager") {
            this.Initialize();
        }

        private void Initialize() {
            this.Commands = new Collection<Commandable>();
        }

        /// <summary>
        /// Attach a command to this manager.
        /// </summary>
        /// <param name="command">Command which need to be managed</param>
        public void Attach(Commandable command){
            this.Commands.Add(command);
            command.UpdateMessage += new SMTP.Commands.CommandEvent.CommandEventHandler(OnUpdateMessage);
        }

        /// <summary>
        /// Attach commands to this manager.
        /// </summary>
        /// <param name="commands">An array of Command, contained commands which need to be managed</param>
        public void Attach(Commandable[] commands) {
            int i;
            for (i = 0; i < commands.Length; i++) {
                this.Attach(commands[i]);
            }
        }

        public void Detach(Commandable command)
        {
            this.Commands.Remove(command);
        }

        public void Run(string commandString) {
            Command temCmd = new Command();
            temCmd.AnalyzeCommandString(commandString);

            int i;
            bool commandFired = false ;
            for (i = 0; i < this.Commands.Count; i++) {
                if (this.Commands[i].Command.CommandName == temCmd.CommandName) {
                    commandFired = true;
                    this.Commands[i].Command.Parameters = temCmd.Parameters;
                    try
                    {
                        if (!this.Commands[i].Run())
                            base.RaiseUpdateMessage(this, "Run command " + Commands[i].Command.CommandName + " failed.", SMTP.Commands.CommandEvent.CommandMessageType.Error);
                    }
                    catch (Exception ex) {
                        base.RaiseUpdateMessage(this, "Exception: " + ex.Message, SMTP.Commands.CommandEvent.CommandMessageType.Error);
                    }
                }
            }
            if (commandFired == false)
                base.RaiseUpdateMessage(this, "No this command!", SMTP.Commands.CommandEvent.CommandMessageType.Warning);
        }

        private void OnUpdateMessage(object sender, SMTP.Commands.CommandEvent.CommandEventArgs e)
        {
            base.RaiseUpdateMessage(this, e.Message, e.Type);
        }

    }


}
