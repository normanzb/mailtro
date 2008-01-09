using System;
using System.Collections.Generic;
using System.Text;

namespace SMTP
{
    public class AuthenticationLogin
    {
        public void AuthLogin(string user, string pass) {
            this.User = user;
            this.Password = pass;
        }

        private string _user;
        private string _pass;

        public string User {
            get {
                return this._user;
            }
            set {
                this._user = value;
            }
        }

        public string Password {
            get {
                return this._pass;
            }
            set {
                this._pass = value;
            }
        }



    }
}
