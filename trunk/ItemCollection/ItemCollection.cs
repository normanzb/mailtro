using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ItemCollection
{

    public partial class ItemCollection : UserControl
    {

        public Collection<FileCollection> Items;
        private Timer fcTimer;
        private int oldCountOfItems;
        private int maxControlsWidth;
        private int maxControlsHeight;
        private int controlsPadding;
        private Label lblPointer;
        private Size originalSize;

        public ItemCollection()
        {
            InitializeComponent();
        }

        private void ItemCollection_Load(object sender, EventArgs e)
        {
            originalSize = this.Size;
            this.txtMain.ReadOnly = true;
            this.txtMain.KeyDown += new KeyEventHandler(Container_KeyDown);
            this.Resize += new EventHandler(ItemCollection_Resize);
            this.ResizeTextbox();
            Items = new Collection<FileCollection>();

            //A timer to monitor the changes of Items
            fcTimer = new Timer();
            fcTimer.Interval = 300;
            fcTimer.Tick += new EventHandler(fcTimer_Tick);
            fcTimer.Enabled = true;

            oldCountOfItems = 0;
            InitDraw();
        }

        public BorderStyle TextBoxBorderStyle {
            set {
                this.txtMain.BorderStyle = value;
            }
            get {
                return this.txtMain.BorderStyle;
            }
        }

        void Container_KeyDown(object sender, KeyEventArgs e)
        {

            if (lblPointer != null & this.Items.Count > 0 & e.KeyCode == Keys.Delete)
            {
                int i;
                for(i=0;i<this.txtMain.Controls.Count;i++){
                    if (this.txtMain.Controls[i] == lblPointer)
                    {
                        break;
                    }
                }
                this.Items.Remove(this.Items[i]);
                lblPointer = null;
            }
        }

        void fcTimer_Tick(object sender, EventArgs e)
        {
            if (this.Items.Count != oldCountOfItems) {
                oldCountOfItems = this.Items.Count;

                //Add label to textbox
                ReDraw();
            }
        }

        void myLabel_Click(object sender, EventArgs e)
        {
            
            Label _self = ((Label)sender);
            if (lblPointer == null || _self != lblPointer)
            {
                if (lblPointer != null)
                {
                    lblPointer.ForeColor = Color.Empty;
                    lblPointer.BackColor = Color.Empty;
                }
                _self.BackColor = System.Drawing.Color.DarkBlue;
                _self.ForeColor = System.Drawing.Color.White;
                lblPointer = _self;
            }
            this.txtMain.Focus();

        }


        //When user resize my control resize textbox too.
        void ItemCollection_Resize(object sender, EventArgs e)
        {
            this.ResizeTextbox();
        }
        void ResizeTextbox() {
            Size _size = new Size(this.Size.Width - 4,this.Size.Height);
            this.txtMain.Size = _size;
            
        }

        //Initailize draw parameters
        private void InitDraw() {
            controlsPadding = 2;
            maxControlsWidth = controlsPadding;
            maxControlsHeight = controlsPadding;
            this.Size = originalSize;

        }
        //Redraw all item to textbox.
        private void ReDraw() {
            //Clear all settings
            this.txtMain.Controls.Clear();
            InitDraw();

            int i;
            for (i = 0; i < Items.Count; i++)
            {
                Label myLabel = new Label();
                Font myFont = new Font("tahoma", 7);

                myLabel.Click += new EventHandler(myLabel_Click);
                myLabel.Text = this.Items[i].FileName;
                myLabel.Cursor = System.Windows.Forms.Cursors.Hand;
                myLabel.Font = myFont;
                myLabel.BorderStyle = BorderStyle.FixedSingle;
                SizeF textRange = myLabel.CreateGraphics().MeasureString(myLabel.Text, myFont);
                myLabel.Width = controlsPadding + 10 + (int)textRange.Width;
                myLabel.Height = controlsPadding + (int)textRange.Height;
                if ((maxControlsWidth + myLabel.Width + controlsPadding) > this.txtMain.Width)
                {
                    maxControlsHeight += (myLabel.Height + 2);
                    maxControlsWidth = controlsPadding;
                    this.Size = new Size(this.Size.Width, maxControlsHeight + 22);
                }
                myLabel.Location = new Point(maxControlsWidth, maxControlsHeight);
                maxControlsWidth += myLabel.Width + controlsPadding;
                this.txtMain.Controls.Add(myLabel);
            }
        }

        private void txtMain_KeyDown(object sender, KeyEventArgs e)
        {
            this.Container_KeyDown(this, e);
        }
    }

    //FileCollection class
    public class FileCollection
    {
        private string m_path;
        public FileCollection()
        {

        }
        public FileCollection(string _path)
        {
            this.Path = _path;
        }
        public string Path
        {
            get
            {
                return m_path;
            }
            set
            {
                // Check the file specified in the path parameters exists. 
                FileInfo fi = new FileInfo(value);
                if (!fi.Exists)
                {
                    throw new Exception(this.ToString() + "File not exist");
                }
                m_path = value;
            }
        }
        public string FileName
        {
            get
            {
                FileInfo fi = new FileInfo(this.Path);

                return fi.Name;

            }
        }
        //Allow explicit converse from string
        public static explicit operator FileCollection(string _path)
        {
            return new FileCollection(_path);
        }
    }
}