namespace SMTP
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSmtpServer = new System.Windows.Forms.TextBox();
            this.txtFrom = new System.Windows.Forms.TextBox();
            this.txtTo = new System.Windows.Forms.TextBox();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.lstLog = new System.Windows.Forms.ListBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.menMain = new System.Windows.Forms.MenuStrip();
            this.templateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.exitEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onlineSupportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.label8 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.btnSaveLogAs = new System.Windows.Forms.Button();
            this.fileMain = new System.Windows.Forms.OpenFileDialog();
            this.btnDatagramView = new System.Windows.Forms.Button();
            this.btnEditorView = new System.Windows.Forms.Button();
            this.btnScrollLog = new System.Windows.Forms.Button();
            this.stuMain = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tspMain = new System.Windows.Forms.ToolStripProgressBar();
            this.panReceiver = new System.Windows.Forms.Panel();
            this.lblBcc = new System.Windows.Forms.Label();
            this.txtBcc = new System.Windows.Forms.TextBox();
            this.txtCc = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnToPlus = new System.Windows.Forms.Button();
            this.panMessageBody = new System.Windows.Forms.Panel();
            this.cmbEncodingName = new System.Windows.Forms.ComboBox();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.chkMultiPart = new System.Windows.Forms.CheckBox();
            this.chkBase64 = new System.Windows.Forms.CheckBox();
            this.chkTls = new System.Windows.Forms.CheckBox();
            this.btnSetAuth = new System.Windows.Forms.Button();
            this.chkAuthentication = new System.Windows.Forms.CheckBox();
            this.lblEncodingName = new System.Windows.Forms.Label();
            this.txtAttachment = new ItemCollection.ItemCollection();
            this.hexMessage = new Be.Windows.Forms.HexBox();
            this.gruSending = new System.Windows.Forms.GroupBox();
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.txtCount = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.sfdExport = new System.Windows.Forms.SaveFileDialog();
            this.txtCommand = new System.Windows.Forms.TextBox();
            this.btnCommand = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.ofdImport = new System.Windows.Forms.OpenFileDialog();
            this.menMain.SuspendLayout();
            this.stuMain.SuspendLayout();
            this.panReceiver.SuspendLayout();
            this.panMessageBody.SuspendLayout();
            this.gruSending.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Smtp Server:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(22, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "From:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "To:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Subject:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(428, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Log:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Message to Send:";
            // 
            // txtSmtpServer
            // 
            this.txtSmtpServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSmtpServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSmtpServer.Location = new System.Drawing.Point(108, 38);
            this.txtSmtpServer.Name = "txtSmtpServer";
            this.txtSmtpServer.Size = new System.Drawing.Size(189, 20);
            this.txtSmtpServer.TabIndex = 0;
            // 
            // txtFrom
            // 
            this.txtFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFrom.Location = new System.Drawing.Point(108, 64);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.Size = new System.Drawing.Size(303, 20);
            this.txtFrom.TabIndex = 2;
            // 
            // txtTo
            // 
            this.txtTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTo.Location = new System.Drawing.Point(99, 3);
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(272, 20);
            this.txtTo.TabIndex = 4;
            // 
            // txtSubject
            // 
            this.txtSubject.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSubject.Location = new System.Drawing.Point(99, 1);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(303, 20);
            this.txtSubject.TabIndex = 6;
            // 
            // lstLog
            // 
            this.lstLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstLog.FormattingEnabled = true;
            this.lstLog.Location = new System.Drawing.Point(431, 77);
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(398, 275);
            this.lstLog.TabIndex = 99;
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.Location = new System.Drawing.Point(754, 433);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 9;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtPort
            // 
            this.txtPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPort.Location = new System.Drawing.Point(344, 38);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(67, 20);
            this.txtPort.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(303, 41);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Port:";
            // 
            // menMain
            // 
            this.menMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.templateToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menMain.Location = new System.Drawing.Point(3, 3);
            this.menMain.Name = "menMain";
            this.menMain.Size = new System.Drawing.Size(856, 24);
            this.menMain.TabIndex = 9;
            this.menMain.Text = "menuStrip1";
            // 
            // templateToolStripMenuItem
            // 
            this.templateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.exitToolStripMenuItem,
            this.exitEToolStripMenuItem});
            this.templateToolStripMenuItem.Name = "templateToolStripMenuItem";
            this.templateToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.templateToolStripMenuItem.Text = "&Template";
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Image = global::SMTP.Properties.Resources.Public_ico;
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.importToolStripMenuItem.Text = "Import(&I)";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exportToolStripMenuItem.Text = "Export(&E)";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(149, 6);
            // 
            // exitEToolStripMenuItem
            // 
            this.exitEToolStripMenuItem.Name = "exitEToolStripMenuItem";
            this.exitEToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitEToolStripMenuItem.Text = "Exit(&X)";
            this.exitEToolStripMenuItem.Click += new System.EventHandler(this.exitEToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onlineSupportToolStripMenuItem,
            this.aboutToolStripMenuItem1});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.aboutToolStripMenuItem.Text = "&Help";
            // 
            // onlineSupportToolStripMenuItem
            // 
            this.onlineSupportToolStripMenuItem.Name = "onlineSupportToolStripMenuItem";
            this.onlineSupportToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.onlineSupportToolStripMenuItem.Text = "Online &Support";
            this.onlineSupportToolStripMenuItem.Click += new System.EventHandler(this.onlineSupportToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(145, 22);
            this.aboutToolStripMenuItem1.Text = "&About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Attachment:";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(327, 25);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 7;
            this.btnBrowse.Text = "Add";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnClearLog
            // 
            this.btnClearLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearLog.Location = new System.Drawing.Point(787, 46);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(42, 23);
            this.btnClearLog.TabIndex = 100;
            this.btnClearLog.Text = "Clear";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // btnSaveLogAs
            // 
            this.btnSaveLogAs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveLogAs.Enabled = false;
            this.btnSaveLogAs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveLogAs.Location = new System.Drawing.Point(705, 46);
            this.btnSaveLogAs.Name = "btnSaveLogAs";
            this.btnSaveLogAs.Size = new System.Drawing.Size(76, 23);
            this.btnSaveLogAs.TabIndex = 101;
            this.btnSaveLogAs.Text = "Save as ...";
            this.btnSaveLogAs.UseVisualStyleBackColor = true;
            // 
            // fileMain
            // 
            this.fileMain.FileName = "*.*";
            // 
            // btnDatagramView
            // 
            this.btnDatagramView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDatagramView.Location = new System.Drawing.Point(315, 280);
            this.btnDatagramView.Name = "btnDatagramView";
            this.btnDatagramView.Size = new System.Drawing.Size(75, 23);
            this.btnDatagramView.TabIndex = 102;
            this.btnDatagramView.Text = "Datagram";
            this.btnDatagramView.UseVisualStyleBackColor = true;
            this.btnDatagramView.Click += new System.EventHandler(this.btnDatagramView_Click);
            // 
            // btnEditorView
            // 
            this.btnEditorView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEditorView.Location = new System.Drawing.Point(234, 280);
            this.btnEditorView.Name = "btnEditorView";
            this.btnEditorView.Size = new System.Drawing.Size(75, 23);
            this.btnEditorView.TabIndex = 103;
            this.btnEditorView.Text = "Editor";
            this.btnEditorView.UseVisualStyleBackColor = true;
            this.btnEditorView.Click += new System.EventHandler(this.btnEditorView_Click);
            // 
            // btnScrollLog
            // 
            this.btnScrollLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnScrollLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnScrollLog.Location = new System.Drawing.Point(624, 46);
            this.btnScrollLog.Name = "btnScrollLog";
            this.btnScrollLog.Size = new System.Drawing.Size(75, 23);
            this.btnScrollLog.TabIndex = 104;
            this.btnScrollLog.Text = "AutoScroll";
            this.btnScrollLog.UseVisualStyleBackColor = true;
            this.btnScrollLog.Click += new System.EventHandler(this.btnScrollLog_Click);
            // 
            // stuMain
            // 
            this.stuMain.GripMargin = new System.Windows.Forms.Padding(0);
            this.stuMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.stuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.tspMain});
            this.stuMain.Location = new System.Drawing.Point(3, 468);
            this.stuMain.Name = "stuMain";
            this.stuMain.Size = new System.Drawing.Size(856, 22);
            this.stuMain.SizingGrip = false;
            this.stuMain.Stretch = false;
            this.stuMain.TabIndex = 106;
            this.stuMain.Text = "stuMain";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(38, 17);
            this.toolStripStatusLabel1.Text = "Ready";
            // 
            // tspMain
            // 
            this.tspMain.Name = "tspMain";
            this.tspMain.Size = new System.Drawing.Size(100, 16);
            // 
            // panReceiver
            // 
            this.panReceiver.Controls.Add(this.lblBcc);
            this.panReceiver.Controls.Add(this.txtBcc);
            this.panReceiver.Controls.Add(this.txtCc);
            this.panReceiver.Controls.Add(this.label9);
            this.panReceiver.Controls.Add(this.btnToPlus);
            this.panReceiver.Controls.Add(this.txtTo);
            this.panReceiver.Controls.Add(this.label3);
            this.panReceiver.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panReceiver.Location = new System.Drawing.Point(9, 86);
            this.panReceiver.Name = "panReceiver";
            this.panReceiver.Size = new System.Drawing.Size(416, 23);
            this.panReceiver.TabIndex = 3;
            this.panReceiver.Resize += new System.EventHandler(this.panReceiver_Resize);
            // 
            // lblBcc
            // 
            this.lblBcc.AutoSize = true;
            this.lblBcc.Location = new System.Drawing.Point(12, 50);
            this.lblBcc.Name = "lblBcc";
            this.lblBcc.Size = new System.Drawing.Size(29, 13);
            this.lblBcc.TabIndex = 8;
            this.lblBcc.Text = "Bcc:";
            // 
            // txtBcc
            // 
            this.txtBcc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBcc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBcc.Location = new System.Drawing.Point(99, 47);
            this.txtBcc.Name = "txtBcc";
            this.txtBcc.Size = new System.Drawing.Size(303, 20);
            this.txtBcc.TabIndex = 7;
            // 
            // txtCc
            // 
            this.txtCc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCc.Location = new System.Drawing.Point(99, 25);
            this.txtCc.Name = "txtCc";
            this.txtCc.Size = new System.Drawing.Size(303, 20);
            this.txtCc.TabIndex = 6;
            this.txtCc.Enter += new System.EventHandler(this.txtCc_Enter);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 28);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(23, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "Cc:";
            // 
            // btnToPlus
            // 
            this.btnToPlus.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnToPlus.Location = new System.Drawing.Point(378, 4);
            this.btnToPlus.Name = "btnToPlus";
            this.btnToPlus.Size = new System.Drawing.Size(24, 18);
            this.btnToPlus.TabIndex = 4;
            this.btnToPlus.Text = "+";
            this.btnToPlus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnToPlus.UseVisualStyleBackColor = true;
            this.btnToPlus.Click += new System.EventHandler(this.btnToPlus_Click);
            // 
            // panMessageBody
            // 
            this.panMessageBody.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panMessageBody.Controls.Add(this.cmbEncodingName);
            this.panMessageBody.Controls.Add(this.txtMessage);
            this.panMessageBody.Controls.Add(this.chkMultiPart);
            this.panMessageBody.Controls.Add(this.chkBase64);
            this.panMessageBody.Controls.Add(this.chkTls);
            this.panMessageBody.Controls.Add(this.btnSetAuth);
            this.panMessageBody.Controls.Add(this.chkAuthentication);
            this.panMessageBody.Controls.Add(this.lblEncodingName);
            this.panMessageBody.Controls.Add(this.btnEditorView);
            this.panMessageBody.Controls.Add(this.label4);
            this.panMessageBody.Controls.Add(this.btnDatagramView);
            this.panMessageBody.Controls.Add(this.label6);
            this.panMessageBody.Controls.Add(this.txtSubject);
            this.panMessageBody.Controls.Add(this.txtAttachment);
            this.panMessageBody.Controls.Add(this.hexMessage);
            this.panMessageBody.Controls.Add(this.label8);
            this.panMessageBody.Controls.Add(this.btnBrowse);
            this.panMessageBody.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panMessageBody.Location = new System.Drawing.Point(9, 114);
            this.panMessageBody.Name = "panMessageBody";
            this.panMessageBody.Size = new System.Drawing.Size(416, 309);
            this.panMessageBody.TabIndex = 5;
            // 
            // cmbEncodingName
            // 
            this.cmbEncodingName.FormattingEnabled = true;
            this.cmbEncodingName.Items.AddRange(new object[] {
            "Default",
            "ASCII",
            "Gb2312",
            "Shift-JIS"});
            this.cmbEncodingName.Location = new System.Drawing.Point(351, 68);
            this.cmbEncodingName.Name = "cmbEncodingName";
            this.cmbEncodingName.Size = new System.Drawing.Size(51, 21);
            this.cmbEncodingName.TabIndex = 114;
            this.cmbEncodingName.Text = "Default";
            // 
            // txtMessage
            // 
            this.txtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMessage.Location = new System.Drawing.Point(16, 118);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessage.Size = new System.Drawing.Size(386, 156);
            this.txtMessage.TabIndex = 113;
            // 
            // chkMultiPart
            // 
            this.chkMultiPart.AutoSize = true;
            this.chkMultiPart.Location = new System.Drawing.Point(225, 93);
            this.chkMultiPart.Name = "chkMultiPart";
            this.chkMultiPart.Size = new System.Drawing.Size(107, 17);
            this.chkMultiPart.TabIndex = 112;
            this.chkMultiPart.Text = "Enforce MultiPart";
            this.chkMultiPart.UseVisualStyleBackColor = true;
            // 
            // chkBase64
            // 
            this.chkBase64.AutoSize = true;
            this.chkBase64.Location = new System.Drawing.Point(112, 93);
            this.chkBase64.Name = "chkBase64";
            this.chkBase64.Size = new System.Drawing.Size(107, 17);
            this.chkBase64.TabIndex = 111;
            this.chkBase64.Text = "Base64 encoded";
            this.chkBase64.UseVisualStyleBackColor = true;
            // 
            // chkTls
            // 
            this.chkTls.AutoSize = true;
            this.chkTls.Location = new System.Drawing.Point(242, 70);
            this.chkTls.Name = "chkTls";
            this.chkTls.Size = new System.Drawing.Size(46, 17);
            this.chkTls.TabIndex = 110;
            this.chkTls.Text = "TLS";
            this.chkTls.UseVisualStyleBackColor = true;
            // 
            // btnSetAuth
            // 
            this.btnSetAuth.Location = new System.Drawing.Point(156, 66);
            this.btnSetAuth.Name = "btnSetAuth";
            this.btnSetAuth.Size = new System.Drawing.Size(75, 23);
            this.btnSetAuth.TabIndex = 109;
            this.btnSetAuth.Text = "Set Auth...";
            this.btnSetAuth.UseVisualStyleBackColor = true;
            this.btnSetAuth.Click += new System.EventHandler(this.btnSetAuth_Click);
            // 
            // chkAuthentication
            // 
            this.chkAuthentication.AutoSize = true;
            this.chkAuthentication.Location = new System.Drawing.Point(16, 70);
            this.chkAuthentication.Name = "chkAuthentication";
            this.chkAuthentication.Size = new System.Drawing.Size(134, 17);
            this.chkAuthentication.TabIndex = 108;
            this.chkAuthentication.Text = "Require Authentication";
            this.chkAuthentication.UseVisualStyleBackColor = true;
            // 
            // lblEncodingName
            // 
            this.lblEncodingName.AutoSize = true;
            this.lblEncodingName.Location = new System.Drawing.Point(290, 71);
            this.lblEncodingName.Name = "lblEncodingName";
            this.lblEncodingName.Size = new System.Drawing.Size(55, 13);
            this.lblEncodingName.TabIndex = 107;
            this.lblEncodingName.Text = "Encoding:";
            // 
            // txtAttachment
            // 
            this.txtAttachment.Location = new System.Drawing.Point(96, 27);
            this.txtAttachment.Name = "txtAttachment";
            this.txtAttachment.Size = new System.Drawing.Size(225, 22);
            this.txtAttachment.TabIndex = 105;
            this.txtAttachment.TabStop = false;
            this.txtAttachment.TextBoxBorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // hexMessage
            // 
            this.hexMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.hexMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hexMessage.BytesPerLine = 12;
            this.hexMessage.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hexMessage.Location = new System.Drawing.Point(16, 118);
            this.hexMessage.Name = "hexMessage";
            this.hexMessage.SelectionLength = ((long)(0));
            this.hexMessage.SelectionStart = ((long)(-1));
            this.hexMessage.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.hexMessage.Size = new System.Drawing.Size(386, 156);
            this.hexMessage.StringViewVisible = true;
            this.hexMessage.TabIndex = 8;
            this.hexMessage.UseFixedBytesPerLine = true;
            this.hexMessage.Visible = false;
            this.hexMessage.VScrollBarVisible = true;
            // 
            // gruSending
            // 
            this.gruSending.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gruSending.Controls.Add(this.txtInterval);
            this.gruSending.Controls.Add(this.txtCount);
            this.gruSending.Controls.Add(this.label12);
            this.gruSending.Controls.Add(this.label11);
            this.gruSending.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gruSending.Location = new System.Drawing.Point(432, 387);
            this.gruSending.Name = "gruSending";
            this.gruSending.Size = new System.Drawing.Size(398, 40);
            this.gruSending.TabIndex = 109;
            this.gruSending.TabStop = false;
            this.gruSending.Text = "Sending Robot";
            // 
            // txtInterval
            // 
            this.txtInterval.Location = new System.Drawing.Point(174, 14);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(59, 20);
            this.txtInterval.TabIndex = 3;
            this.txtInterval.Text = "0";
            // 
            // txtCount
            // 
            this.txtCount.Location = new System.Drawing.Point(67, 14);
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(53, 20);
            this.txtCount.TabIndex = 2;
            this.txtCount.Text = "1";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(126, 17);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(42, 13);
            this.label12.TabIndex = 1;
            this.label12.Text = "Interval";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(26, 17);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Count";
            // 
            // txtCommand
            // 
            this.txtCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCommand.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCommand.Location = new System.Drawing.Point(489, 358);
            this.txtCommand.Name = "txtCommand";
            this.txtCommand.Size = new System.Drawing.Size(260, 20);
            this.txtCommand.TabIndex = 110;
            // 
            // btnCommand
            // 
            this.btnCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCommand.Location = new System.Drawing.Point(755, 356);
            this.btnCommand.Name = "btnCommand";
            this.btnCommand.Size = new System.Drawing.Size(75, 23);
            this.btnCommand.TabIndex = 111;
            this.btnCommand.Text = "Run";
            this.btnCommand.UseVisualStyleBackColor = true;
            this.btnCommand.Click += new System.EventHandler(this.btnCommand_Click);
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(431, 361);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(57, 13);
            this.label13.TabIndex = 112;
            this.label13.Text = "Command:";
            // 
            // ofdImport
            // 
            this.ofdImport.FileName = "openFileDialog1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 493);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.btnCommand);
            this.Controls.Add(this.txtCommand);
            this.Controls.Add(this.gruSending);
            this.Controls.Add(this.panMessageBody);
            this.Controls.Add(this.panReceiver);
            this.Controls.Add(this.stuMain);
            this.Controls.Add(this.btnScrollLog);
            this.Controls.Add(this.btnSaveLogAs);
            this.Controls.Add(this.btnClearLog);
            this.Controls.Add(this.menMain);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.lstLog);
            this.Controls.Add(this.txtFrom);
            this.Controls.Add(this.txtSmtpServer);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(4, 22);
            this.MainMenuStrip = this.menMain;
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "Mailtro";
            this.Load += new System.EventHandler(this.SMTP_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.menMain.ResumeLayout(false);
            this.menMain.PerformLayout();
            this.stuMain.ResumeLayout(false);
            this.stuMain.PerformLayout();
            this.panReceiver.ResumeLayout(false);
            this.panReceiver.PerformLayout();
            this.panMessageBody.ResumeLayout(false);
            this.panMessageBody.PerformLayout();
            this.gruSending.ResumeLayout(false);
            this.gruSending.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Button btnSend;
        private Be.Windows.Forms.HexBox hexMessage;
        private System.Windows.Forms.ListBox lstLog;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.TextBox txtTo;
        private System.Windows.Forms.TextBox txtFrom;
        private System.Windows.Forms.TextBox txtSmtpServer;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menMain;
        private System.Windows.Forms.ToolStripMenuItem templateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitEToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator exitToolStripMenuItem;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnClearLog;
        private System.Windows.Forms.Button btnSaveLogAs;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog fileMain;
        private System.Windows.Forms.Button btnDatagramView;
        private System.Windows.Forms.Button btnEditorView;
        private System.Windows.Forms.Button btnScrollLog;
        private ItemCollection.ItemCollection txtAttachment;
        private System.Windows.Forms.StatusStrip stuMain;
        private System.Windows.Forms.Panel panReceiver;
        private System.Windows.Forms.Button btnToPlus;
        private System.Windows.Forms.Panel panMessageBody;
        private System.Windows.Forms.Label lblBcc;
        private System.Windows.Forms.TextBox txtBcc;
        private System.Windows.Forms.TextBox txtCc;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox gruSending;
        private System.Windows.Forms.TextBox txtCount;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtInterval;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar tspMain;
        private System.Windows.Forms.SaveFileDialog sfdExport;
        private System.Windows.Forms.Label lblEncodingName;
        private System.Windows.Forms.CheckBox chkAuthentication;
        private System.Windows.Forms.Button btnSetAuth;
        private System.Windows.Forms.CheckBox chkTls;
        private System.Windows.Forms.CheckBox chkBase64;
        private System.Windows.Forms.CheckBox chkMultiPart;
        private System.Windows.Forms.ToolStripMenuItem onlineSupportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.TextBox txtCommand;
        private System.Windows.Forms.Button btnCommand;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.ComboBox cmbEncodingName;
        private System.Windows.Forms.OpenFileDialog ofdImport;


    }
}

