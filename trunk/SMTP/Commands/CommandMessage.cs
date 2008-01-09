using System;
using System.Collections.Generic;
using System.Text;

namespace SMTP.Commands.CommandEvent
{
    
        //Support a write log event.
        //Event is actually a delegate with special entry.
        public delegate void CommandEventHandler(object sender, CommandEventArgs e);

        public enum CommandMessageType
        {
            Information,
            Error,
            Warning
        }

        //A event argurments which defined by myself.
        //I use this event argurment to transfer my log message (string)
        public class CommandEventArgs : EventArgs
        {
            private string _message;
            private CommandMessageType _type;


            public CommandEventArgs(string message)
            {
                this._message = message;
            }

            //Message type: Debug, Error, Warning
            public CommandMessageType Type
            {
                get
                {
                    return _type;
                }
                set
                {
                    _type = value;
                }
            }
            public string Message
            {
                get
                {
                    return _message;
                }
                set
                {
                    _message = value;
                }
            }
        }
    
}
