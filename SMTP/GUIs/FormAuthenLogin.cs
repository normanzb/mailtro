using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMTP
{
    public partial class FormAuthenLogin : Form
    {
        public FormAuthenLogin(AuthenticationLogin authLogin)
        {
            this.Authentication = authLogin;
            InitializeComponent();
        }

        AuthenticationLogin _authLogin;

        public AuthenticationLogin Authentication {
            get {
                return this._authLogin;
            }
            set {
                this._authLogin = value;
            }
        }

        public void GetRefUserPass(ref string user, ref string pass){
        
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.Authentication == null) {
                //-------------
                // TODO: Implement a customized exception
                //------------- 
                throw new Exception("Authentication must be specified!");
            }
            this._authLogin.User = this.txtUsername.Text;
            this._authLogin.Password = this.txtPassword.Text;
            this.DialogResult = DialogResult.OK;
            
            this.Close();
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (this.Authentication == null)
            {
                //-------------
                // TODO: Implement a customized exception
                //------------- 
                throw new Exception("Authentication must be specified!");
            }
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void AuthenLogin_Load(object sender, EventArgs e)
        {
            this.txtUsername.Text = this.Authentication.User;
            this.txtPassword.Text = this.Authentication.Password;
        }
    }
}