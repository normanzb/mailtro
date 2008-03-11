using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMTP.GUIs
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
        }

        private void FormAbout_Load(object sender, EventArgs e)
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            lblVersionNum.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
    }
}