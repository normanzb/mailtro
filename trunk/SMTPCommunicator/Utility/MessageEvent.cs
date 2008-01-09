using System;
using System.Collections.Generic;
using System.Text;

namespace SMTP
{
    //Support a write log event.
    //Event is actually a delegate with special entry.
    public delegate void MessageEventHandler(object o, MessageEventArgs e);

    public enum MessageEventType { 
        Log,
        Error,
        Warning
    }

    //A event argurments which defined by myself.
    //I use this event argurment to transfer my log message (string)
    public class MessageEventArgs : EventArgs
    {
        private string m_message;
        private MessageEventType m_type;


        public MessageEventArgs(string _message)
        {
            m_message = _message;
        }
        
        //Message type: Debug, Error, Warning
        public MessageEventType Type {
            get{
                return m_type;
            }
            set {
                m_type = value;
            }
        }
        public string Message
        {
            get
            {
                return m_message;
            }
            set
            {
                //-------------
                // TODO: Input validate here
                //-------------
                m_message = value;
            }
        }
    }
}
