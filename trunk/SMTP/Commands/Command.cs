using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SMTP.Commands
{
    class Command
    {
        public Command() {
            this.Initialize();
        }

        private void Initialize(){

            this.Parameters = new Collection<string>();

        }

        private Collection<string> _parameters;

        public Collection<string> Parameters
        {
            get
            {
                return this._parameters;
            }
            set
            {
                this._parameters = value;
            }
        }

        private string _commandName;

        public string CommandName
        {
            get
            {
                //enforce to output upper cases character
                return this._commandName.ToUpper();
            }
            set
            {
                this._commandName = value;
            }
        }

        private string _description;

        public string Description {
            get {
                return this._description;
            }
            set {
                this._description = value;
            }
        }

        private string _usage;

        public string Usage {
            get {
                return this._usage;
            }
            set {
                this._usage = value;
            }
        }

        public void AnalyzeCommandString(string commandString) {

            string[] cells = commandString.Split(' ');
            this.CommandName = cells[0];
            int i;
            for(i=1;i<cells.Length;i++){

                this.Parameters.Add(cells[i]);

            }
        }
    }
}
