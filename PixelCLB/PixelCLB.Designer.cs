using System.Windows.Forms;


namespace PixelCLB
{
    partial class PixelCLB
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
            bool flag;
            flag = (!disposing ? true : this.components == null);
            bool flag1 = flag;
            if (!flag1)
            {
                this.components.Dispose();
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PixelCLB));
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.logBox = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.LogRecvCheckBox = new System.Windows.Forms.CheckBox();
            this.LogSendCheckBox = new System.Windows.Forms.CheckBox();
            this.userLabel = new System.Windows.Forms.Label();
            this.mapleVersionLabel = new System.Windows.Forms.Label();
            this.modeLabel = new System.Windows.Forms.Label();
            this.Map = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DCButton = new System.Windows.Forms.Button();
            this.SendPacket = new System.Windows.Forms.Button();
            this.ReceivePacket = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.Player_Box = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.mushy_Box = new System.Windows.Forms.ListBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.removeWhiteList = new System.Windows.Forms.Button();
            this.addWhiteList = new System.Windows.Forms.Button();
            this.whiteListTextBox = new System.Windows.Forms.TextBox();
            this.whiteListBox = new System.Windows.Forms.ListBox();
            this.MovementPacketTest = new System.Windows.Forms.Button();
            this.textBoxGetPlayer = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.PacketCenterLabel = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.commandTextBox = new System.Windows.Forms.TextBox();
            this.guildLabel = new System.Windows.Forms.Label();
            this.ignLabel = new System.Windows.Forms.Label();
            this.activeAccountsGroupBox = new System.Windows.Forms.GroupBox();
            this.activeAccounts = new System.Windows.Forms.ListBox();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.player_Box_Menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyPlayerUIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyPlayerCoordsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customizeableBotSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.proxySettingsToolStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.openSettingsFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allProfileSettingsEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.profToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.about1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.startTimeComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.logOffPasswordLabel = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.accessLevelLabel = new System.Windows.Forms.Label();
            this.lastUpdatedLabel = new System.Windows.Forms.Label();
            this.hwidCheckLabel = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.openProfiles = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.mesoLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.PacketCenterLabel.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.activeAccountsGroupBox.SuspendLayout();
            this.player_Box_Menu.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(7, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Profile:";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("Calibri", 8F, System.Drawing.FontStyle.Bold);
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(57, 27);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(160, 21);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.DropDown += new System.EventHandler(this.comboBox1_DropDown);
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            this.comboBox1.SelectionChangeCommitted += new System.EventHandler(this.comboBox1_SelectionChangeCommitted);
            this.comboBox1.Click += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // button2
            // 
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button2.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold);
            this.button2.Location = new System.Drawing.Point(130, 54);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(61, 21);
            this.button2.TabIndex = 4;
            this.button2.Text = "Start Bot";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold);
            this.button3.Location = new System.Drawing.Point(243, 54);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(29, 21);
            this.button3.TabIndex = 7;
            this.button3.Text = ">>";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(193, 54);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(48, 21);
            this.button1.TabIndex = 6;
            this.button1.Text = "Log-In";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.logBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.LogRecvCheckBox);
            this.groupBox1.Controls.Add(this.LogSendCheckBox);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 8F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(284, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBox1.Size = new System.Drawing.Size(230, 361);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Clientless Bot Logs";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::PixelCLB.Properties.Resources.Stop16;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(207, 340);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(17, 17);
            this.pictureBox1.TabIndex = 21;
            this.pictureBox1.TabStop = false;
            // 
            // logBox
            // 
            this.logBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.logBox.Font = new System.Drawing.Font("Calibri", 8F, System.Drawing.FontStyle.Bold);
            this.logBox.FormattingEnabled = true;
            this.logBox.HorizontalScrollbar = true;
            this.logBox.ImeMode = System.Windows.Forms.ImeMode.On;
            this.logBox.Location = new System.Drawing.Point(8, 12);
            this.logBox.Name = "logBox";
            this.logBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.logBox.Size = new System.Drawing.Size(216, 325);
            this.logBox.TabIndex = 3;
            this.logBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.logBox_MouseDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(157, 340);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 14);
            this.label3.TabIndex = 20;
            this.label3.Text = "Status: ";
            // 
            // LogRecvCheckBox
            // 
            this.LogRecvCheckBox.AutoSize = true;
            this.LogRecvCheckBox.Location = new System.Drawing.Point(79, 340);
            this.LogRecvCheckBox.Name = "LogRecvCheckBox";
            this.LogRecvCheckBox.Size = new System.Drawing.Size(66, 17);
            this.LogRecvCheckBox.TabIndex = 9;
            this.LogRecvCheckBox.Text = "Log Recv";
            this.LogRecvCheckBox.UseVisualStyleBackColor = true;
            this.LogRecvCheckBox.CheckedChanged += new System.EventHandler(this.LogRecvCheckBox_CheckedChanged);
            // 
            // LogSendCheckBox
            // 
            this.LogSendCheckBox.AutoSize = true;
            this.LogSendCheckBox.Location = new System.Drawing.Point(6, 340);
            this.LogSendCheckBox.Name = "LogSendCheckBox";
            this.LogSendCheckBox.Size = new System.Drawing.Size(67, 17);
            this.LogSendCheckBox.TabIndex = 8;
            this.LogSendCheckBox.Text = "Log Send";
            this.LogSendCheckBox.UseVisualStyleBackColor = true;
            this.LogSendCheckBox.CheckedChanged += new System.EventHandler(this.LogSendCheckBox_CheckedChanged);
            // 
            // userLabel
            // 
            this.userLabel.AutoSize = true;
            this.userLabel.Location = new System.Drawing.Point(517, 11);
            this.userLabel.Name = "userLabel";
            this.userLabel.Size = new System.Drawing.Size(85, 13);
            this.userLabel.TabIndex = 25;
            this.userLabel.Text = "Welcome: [user]";
            // 
            // mapleVersionLabel
            // 
            this.mapleVersionLabel.AutoSize = true;
            this.mapleVersionLabel.BackColor = System.Drawing.Color.Transparent;
            this.mapleVersionLabel.Location = new System.Drawing.Point(6, 12);
            this.mapleVersionLabel.Name = "mapleVersionLabel";
            this.mapleVersionLabel.Size = new System.Drawing.Size(61, 13);
            this.mapleVersionLabel.TabIndex = 22;
            this.mapleVersionLabel.Text = "MapleStory";
            // 
            // modeLabel
            // 
            this.modeLabel.AutoSize = true;
            this.modeLabel.Location = new System.Drawing.Point(6, 36);
            this.modeLabel.Name = "modeLabel";
            this.modeLabel.Size = new System.Drawing.Size(93, 13);
            this.modeLabel.TabIndex = 18;
            this.modeLabel.Text = "Mode: Not Started";
            // 
            // Map
            // 
            this.Map.AutoSize = true;
            this.Map.Location = new System.Drawing.Point(6, 24);
            this.Map.Name = "Map";
            this.Map.Size = new System.Drawing.Size(52, 13);
            this.Map.TabIndex = 17;
            this.Map.Text = "Map: N/A";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Font = new System.Drawing.Font("Calibri", 8F, System.Drawing.FontStyle.Bold);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.removeToolStripMenuItem,
            this.clearLogToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.ShowImageMargin = false;
            this.contextMenuStrip1.Size = new System.Drawing.Size(92, 70);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(91, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(91, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // clearLogToolStripMenuItem
            // 
            this.clearLogToolStripMenuItem.Name = "clearLogToolStripMenuItem";
            this.clearLogToolStripMenuItem.Size = new System.Drawing.Size(91, 22);
            this.clearLogToolStripMenuItem.Text = "Clear Log";
            this.clearLogToolStripMenuItem.Click += new System.EventHandler(this.clearLogToolStripMenuItem_Click);
            // 
            // DCButton
            // 
            this.DCButton.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold);
            this.DCButton.Location = new System.Drawing.Point(9, 258);
            this.DCButton.Name = "DCButton";
            this.DCButton.Size = new System.Drawing.Size(247, 21);
            this.DCButton.TabIndex = 5;
            this.DCButton.Text = "Terminate Selected Bot";
            this.DCButton.UseVisualStyleBackColor = true;
            this.DCButton.Click += new System.EventHandler(this.button5_Click);
            // 
            // SendPacket
            // 
            this.SendPacket.Location = new System.Drawing.Point(688, 12);
            this.SendPacket.Name = "SendPacket";
            this.SendPacket.Size = new System.Drawing.Size(27, 20);
            this.SendPacket.TabIndex = 15;
            this.SendPacket.Text = "SP";
            this.SendPacket.UseVisualStyleBackColor = true;
            this.SendPacket.Click += new System.EventHandler(this.SendPacket_Click);
            // 
            // ReceivePacket
            // 
            this.ReceivePacket.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.ReceivePacket.Location = new System.Drawing.Point(721, 12);
            this.ReceivePacket.Name = "ReceivePacket";
            this.ReceivePacket.Size = new System.Drawing.Size(27, 20);
            this.ReceivePacket.TabIndex = 16;
            this.ReceivePacket.Text = "RP";
            this.ReceivePacket.UseVisualStyleBackColor = true;
            this.ReceivePacket.Click += new System.EventHandler(this.ReceivePacket_Click);
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Location = new System.Drawing.Point(6, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(676, 21);
            this.textBox1.TabIndex = 14;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tabControl1);
            this.groupBox2.Font = new System.Drawing.Font("Calibri", 8F, System.Drawing.FontStyle.Bold);
            this.groupBox2.Location = new System.Drawing.Point(520, 134);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(244, 250);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Free Market Settings / Movement Packets";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(9, 19);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(229, 228);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.Player_Box);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(221, 199);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Player Coords";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // Player_Box
            // 
            this.Player_Box.FormattingEnabled = true;
            this.Player_Box.Location = new System.Drawing.Point(0, 0);
            this.Player_Box.Name = "Player_Box";
            this.Player_Box.Size = new System.Drawing.Size(221, 199);
            this.Player_Box.TabIndex = 14;
            this.Player_Box.Click += new System.EventHandler(this.Player_Box_Click);
            this.Player_Box.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Player_Box_MouseDown);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.mushy_Box);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(221, 199);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Store Coords";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // mushy_Box
            // 
            this.mushy_Box.FormattingEnabled = true;
            this.mushy_Box.Location = new System.Drawing.Point(0, 0);
            this.mushy_Box.Name = "mushy_Box";
            this.mushy_Box.Size = new System.Drawing.Size(221, 199);
            this.mushy_Box.TabIndex = 0;
            this.mushy_Box.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mushy_Box_MouseClick);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.removeWhiteList);
            this.tabPage3.Controls.Add(this.addWhiteList);
            this.tabPage3.Controls.Add(this.whiteListTextBox);
            this.tabPage3.Controls.Add(this.whiteListBox);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(221, 199);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "WhiteList";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // removeWhiteList
            // 
            this.removeWhiteList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.removeWhiteList.Image = global::PixelCLB.Properties.Resources.Delete16;
            this.removeWhiteList.Location = new System.Drawing.Point(194, 175);
            this.removeWhiteList.Name = "removeWhiteList";
            this.removeWhiteList.Size = new System.Drawing.Size(24, 24);
            this.removeWhiteList.TabIndex = 5;
            this.removeWhiteList.UseVisualStyleBackColor = true;
            this.removeWhiteList.Click += new System.EventHandler(this.removeWhiteList_Click);
            // 
            // addWhiteList
            // 
            this.addWhiteList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.addWhiteList.Image = global::PixelCLB.Properties.Resources.Plus16;
            this.addWhiteList.Location = new System.Drawing.Point(171, 175);
            this.addWhiteList.Name = "addWhiteList";
            this.addWhiteList.Size = new System.Drawing.Size(24, 24);
            this.addWhiteList.TabIndex = 4;
            this.addWhiteList.UseVisualStyleBackColor = true;
            this.addWhiteList.Click += new System.EventHandler(this.addWhiteList_Click);
            // 
            // whiteListTextBox
            // 
            this.whiteListTextBox.Location = new System.Drawing.Point(3, 176);
            this.whiteListTextBox.Name = "whiteListTextBox";
            this.whiteListTextBox.Size = new System.Drawing.Size(168, 21);
            this.whiteListTextBox.TabIndex = 3;
            this.whiteListTextBox.Text = "IGN Here";
            // 
            // whiteListBox
            // 
            this.whiteListBox.FormattingEnabled = true;
            this.whiteListBox.Location = new System.Drawing.Point(0, 0);
            this.whiteListBox.Name = "whiteListBox";
            this.whiteListBox.Size = new System.Drawing.Size(221, 173);
            this.whiteListBox.TabIndex = 2;
            // 
            // MovementPacketTest
            // 
            this.MovementPacketTest.Location = new System.Drawing.Point(174, 15);
            this.MovementPacketTest.Name = "MovementPacketTest";
            this.MovementPacketTest.Size = new System.Drawing.Size(60, 21);
            this.MovementPacketTest.TabIndex = 13;
            this.MovementPacketTest.Text = "Teleport";
            this.MovementPacketTest.UseVisualStyleBackColor = true;
            this.MovementPacketTest.Click += new System.EventHandler(this.MovementPacketTest_Click);
            // 
            // textBoxGetPlayer
            // 
            this.textBoxGetPlayer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxGetPlayer.Location = new System.Drawing.Point(9, 15);
            this.textBoxGetPlayer.Name = "textBoxGetPlayer";
            this.textBoxGetPlayer.Size = new System.Drawing.Size(163, 21);
            this.textBoxGetPlayer.TabIndex = 12;
            this.textBoxGetPlayer.Text = "X,Y,FH";
            this.textBoxGetPlayer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxGetPlayer_KeyPress);
            // 
            // button5
            // 
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button5.Location = new System.Drawing.Point(191, 15);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(31, 21);
            this.button5.TabIndex = 18;
            this.button5.Text = "CMD";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click_1);
            // 
            // PacketCenterLabel
            // 
            this.PacketCenterLabel.Controls.Add(this.textBox1);
            this.PacketCenterLabel.Controls.Add(this.ReceivePacket);
            this.PacketCenterLabel.Controls.Add(this.SendPacket);
            this.PacketCenterLabel.Font = new System.Drawing.Font("Calibri", 8F, System.Drawing.FontStyle.Bold);
            this.PacketCenterLabel.Location = new System.Drawing.Point(10, 435);
            this.PacketCenterLabel.Name = "PacketCenterLabel";
            this.PacketCenterLabel.Size = new System.Drawing.Size(754, 39);
            this.PacketCenterLabel.TabIndex = 14;
            this.PacketCenterLabel.TabStop = false;
            this.PacketCenterLabel.Text = "Packet Center";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.commandTextBox);
            this.groupBox3.Controls.Add(this.button5);
            this.groupBox3.Font = new System.Drawing.Font("Calibri", 8F, System.Drawing.FontStyle.Bold);
            this.groupBox3.Location = new System.Drawing.Point(284, 387);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(230, 42);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Command Center";
            // 
            // commandTextBox
            // 
            this.commandTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.commandTextBox.Location = new System.Drawing.Point(8, 15);
            this.commandTextBox.Name = "commandTextBox";
            this.commandTextBox.Size = new System.Drawing.Size(179, 21);
            this.commandTextBox.TabIndex = 17;
            this.commandTextBox.Text = "cmdlist";
            this.commandTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.commandTextBox_KeyPress);
            // 
            // guildLabel
            // 
            this.guildLabel.AutoSize = true;
            this.guildLabel.BackColor = System.Drawing.Color.Transparent;
            this.guildLabel.Location = new System.Drawing.Point(6, 62);
            this.guildLabel.Name = "guildLabel";
            this.guildLabel.Size = new System.Drawing.Size(26, 13);
            this.guildLabel.TabIndex = 31;
            this.guildLabel.Text = "N/A";
            // 
            // ignLabel
            // 
            this.ignLabel.AutoSize = true;
            this.ignLabel.BackColor = System.Drawing.Color.Transparent;
            this.ignLabel.Location = new System.Drawing.Point(6, 49);
            this.ignLabel.Name = "ignLabel";
            this.ignLabel.Size = new System.Drawing.Size(26, 13);
            this.ignLabel.TabIndex = 25;
            this.ignLabel.Text = "N/A";
            // 
            // activeAccountsGroupBox
            // 
            this.activeAccountsGroupBox.Controls.Add(this.activeAccounts);
            this.activeAccountsGroupBox.Controls.Add(this.DCButton);
            this.activeAccountsGroupBox.Font = new System.Drawing.Font("Calibri", 8F, System.Drawing.FontStyle.Bold);
            this.activeAccountsGroupBox.Location = new System.Drawing.Point(10, 81);
            this.activeAccountsGroupBox.Name = "activeAccountsGroupBox";
            this.activeAccountsGroupBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.activeAccountsGroupBox.Size = new System.Drawing.Size(262, 284);
            this.activeAccountsGroupBox.TabIndex = 22;
            this.activeAccountsGroupBox.TabStop = false;
            this.activeAccountsGroupBox.Text = "Active Accounts: X Bots";
            // 
            // activeAccounts
            // 
            this.activeAccounts.FormattingEnabled = true;
            this.activeAccounts.HorizontalScrollbar = true;
            this.activeAccounts.Location = new System.Drawing.Point(9, 14);
            this.activeAccounts.Name = "activeAccounts";
            this.activeAccounts.Size = new System.Drawing.Size(247, 238);
            this.activeAccounts.TabIndex = 0;
            this.activeAccounts.SelectedIndexChanged += new System.EventHandler(this.activeAccounts_SelectedIndexChanged);
            this.activeAccounts.MouseDown += new System.Windows.Forms.MouseEventHandler(this.activeAccounts_MouseDown);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Font = new System.Drawing.Font("Calibri", 8F, System.Drawing.FontStyle.Bold);
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.ShowImageMargin = false;
            this.contextMenuStrip2.Size = new System.Drawing.Size(36, 4);
            // 
            // player_Box_Menu
            // 
            this.player_Box_Menu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.player_Box_Menu.Font = new System.Drawing.Font("Calibri", 8F, System.Drawing.FontStyle.Bold);
            this.player_Box_Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyPlayerUIDToolStripMenuItem,
            this.copyPlayerCoordsToolStripMenuItem,
            this.cancelToolStripMenuItem2});
            this.player_Box_Menu.Name = "player_Box_Menu";
            this.player_Box_Menu.ShowImageMargin = false;
            this.player_Box_Menu.Size = new System.Drawing.Size(138, 70);
            // 
            // copyPlayerUIDToolStripMenuItem
            // 
            this.copyPlayerUIDToolStripMenuItem.Name = "copyPlayerUIDToolStripMenuItem";
            this.copyPlayerUIDToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.copyPlayerUIDToolStripMenuItem.Text = "Copy Player UID";
            this.copyPlayerUIDToolStripMenuItem.Click += new System.EventHandler(this.copyPlayerUIDToolStripMenuItem_Click);
            // 
            // copyPlayerCoordsToolStripMenuItem
            // 
            this.copyPlayerCoordsToolStripMenuItem.Name = "copyPlayerCoordsToolStripMenuItem";
            this.copyPlayerCoordsToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.copyPlayerCoordsToolStripMenuItem.Text = "Copy Player Coords";
            this.copyPlayerCoordsToolStripMenuItem.Click += new System.EventHandler(this.copyPlayerCoordsToolStripMenuItem_Click);
            // 
            // cancelToolStripMenuItem2
            // 
            this.cancelToolStripMenuItem2.Name = "cancelToolStripMenuItem2";
            this.cancelToolStripMenuItem2.Size = new System.Drawing.Size(137, 22);
            this.cancelToolStripMenuItem2.Text = "Cancel";
            this.cancelToolStripMenuItem2.Click += new System.EventHandler(this.cancelToolStripMenuItem2_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.AllowMerge = false;
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.menuStrip1.Font = new System.Drawing.Font("Calibri", 8F, System.Drawing.FontStyle.Bold);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.preferencesToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(772, 24);
            this.menuStrip1.TabIndex = 24;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.RightToLeftAutoMirrorImage = true;
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(36, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.closeToolStripMenuItem.Text = "Exit Bot";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.customizeableBotSettingsToolStripMenuItem,
            this.proxySettingsToolStripMenu,
            this.openSettingsFolderToolStripMenuItem,
            this.allProfileSettingsEditorToolStripMenuItem,
            this.profToolStripMenuItem});
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.preferencesToolStripMenuItem.Text = "Preferences";
            // 
            // customizeableBotSettingsToolStripMenuItem
            // 
            this.customizeableBotSettingsToolStripMenuItem.Name = "customizeableBotSettingsToolStripMenuItem";
            this.customizeableBotSettingsToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.customizeableBotSettingsToolStripMenuItem.Text = "Bot Settings";
            this.customizeableBotSettingsToolStripMenuItem.Click += new System.EventHandler(this.customizeableBotSettingsToolStripMenuItem_Click);
            // 
            // proxySettingsToolStripMenu
            // 
            this.proxySettingsToolStripMenu.Name = "proxySettingsToolStripMenu";
            this.proxySettingsToolStripMenu.Size = new System.Drawing.Size(223, 22);
            this.proxySettingsToolStripMenu.Text = "Proxy Settings";
            this.proxySettingsToolStripMenu.Click += new System.EventHandler(this.proxySettingsToolStripMenu_Click);
            // 
            // openSettingsFolderToolStripMenuItem
            // 
            this.openSettingsFolderToolStripMenuItem.Name = "openSettingsFolderToolStripMenuItem";
            this.openSettingsFolderToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.openSettingsFolderToolStripMenuItem.Text = "Open Settings Folder";
            this.openSettingsFolderToolStripMenuItem.Click += new System.EventHandler(this.openSettingsFolderToolStripMenuItem_Click);
            // 
            // allProfileSettingsEditorToolStripMenuItem
            // 
            this.allProfileSettingsEditorToolStripMenuItem.Name = "allProfileSettingsEditorToolStripMenuItem";
            this.allProfileSettingsEditorToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.allProfileSettingsEditorToolStripMenuItem.Text = "All Profile Settings Editor";
            this.allProfileSettingsEditorToolStripMenuItem.Click += new System.EventHandler(this.allProfileSettingsEditorToolStripMenuItem_Click);
            // 
            // profToolStripMenuItem
            // 
            this.profToolStripMenuItem.Name = "profToolStripMenuItem";
            this.profToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.profToolStripMenuItem.Text = "Profile Reorganizer / Reordering";
            this.profToolStripMenuItem.Click += new System.EventHandler(this.profToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reportToolStripMenuItem1,
            this.about1ToolStripMenuItem,
            this.aboutToolStripMenuItem1});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // reportToolStripMenuItem1
            // 
            this.reportToolStripMenuItem1.Name = "reportToolStripMenuItem1";
            this.reportToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.reportToolStripMenuItem1.Text = "Bot Info";
            // 
            // about1ToolStripMenuItem
            // 
            this.about1ToolStripMenuItem.Name = "about1ToolStripMenuItem";
            this.about1ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.about1ToolStripMenuItem.Text = "Report(Disabled)";
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem1.Text = "About";
            // 
            // groupBox6
            // 
            this.groupBox6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox6.Controls.Add(this.mesoLabel);
            this.groupBox6.Controls.Add(this.modeLabel);
            this.groupBox6.Controls.Add(this.mapleVersionLabel);
            this.groupBox6.Controls.Add(this.Map);
            this.groupBox6.Controls.Add(this.ignLabel);
            this.groupBox6.Controls.Add(this.guildLabel);
            this.groupBox6.Location = new System.Drawing.Point(520, 25);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(244, 106);
            this.groupBox6.TabIndex = 39;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Account Information";
            // 
            // startTimeComboBox
            // 
            this.startTimeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.startTimeComboBox.FormattingEnabled = true;
            this.startTimeComboBox.Items.AddRange(new object[] {
            "Start Now",
            "Post Server Check",
            "Regular Log-In (Post SC)"});
            this.startTimeComboBox.Location = new System.Drawing.Point(10, 54);
            this.startTimeComboBox.Name = "startTimeComboBox";
            this.startTimeComboBox.Size = new System.Drawing.Size(117, 21);
            this.startTimeComboBox.TabIndex = 40;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 371);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 13);
            this.label4.TabIndex = 41;
            this.label4.Text = "Custom Admin Password:";
            // 
            // logOffPasswordLabel
            // 
            this.logOffPasswordLabel.AutoSize = true;
            this.logOffPasswordLabel.Location = new System.Drawing.Point(136, 371);
            this.logOffPasswordLabel.Name = "logOffPasswordLabel";
            this.logOffPasswordLabel.Size = new System.Drawing.Size(50, 13);
            this.logOffPasswordLabel.TabIndex = 42;
            this.logOffPasswordLabel.Text = "Password";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBoxGetPlayer);
            this.groupBox4.Controls.Add(this.MovementPacketTest);
            this.groupBox4.Location = new System.Drawing.Point(520, 387);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(244, 42);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Movement Packet Format X,Y,FH(Optional)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 384);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 43;
            this.label2.Text = "Access Level:";
            // 
            // accessLevelLabel
            // 
            this.accessLevelLabel.AutoSize = true;
            this.accessLevelLabel.Location = new System.Drawing.Point(80, 384);
            this.accessLevelLabel.Name = "accessLevelLabel";
            this.accessLevelLabel.Size = new System.Drawing.Size(65, 13);
            this.accessLevelLabel.TabIndex = 44;
            this.accessLevelLabel.Text = "Access Level";
            // 
            // lastUpdatedLabel
            // 
            this.lastUpdatedLabel.AutoSize = true;
            this.lastUpdatedLabel.Location = new System.Drawing.Point(16, 397);
            this.lastUpdatedLabel.Name = "lastUpdatedLabel";
            this.lastUpdatedLabel.Size = new System.Drawing.Size(70, 13);
            this.lastUpdatedLabel.TabIndex = 45;
            this.lastUpdatedLabel.Text = "Last Updated:";
            // 
            // hwidCheckLabel
            // 
            this.hwidCheckLabel.AutoSize = true;
            this.hwidCheckLabel.Location = new System.Drawing.Point(16, 410);
            this.hwidCheckLabel.Name = "hwidCheckLabel";
            this.hwidCheckLabel.Size = new System.Drawing.Size(69, 13);
            this.hwidCheckLabel.TabIndex = 0;
            this.hwidCheckLabel.Text = "HWID Check: ";
            // 
            // button4
            // 
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button4.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.button4.Image = global::PixelCLB.Properties.Resources.Delete16;
            this.button4.Location = new System.Drawing.Point(248, 25);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(24, 24);
            this.button4.TabIndex = 3;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // openProfiles
            // 
            this.openProfiles.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.openProfiles.Image = global::PixelCLB.Properties.Resources.Plus16;
            this.openProfiles.Location = new System.Drawing.Point(223, 25);
            this.openProfiles.Name = "openProfiles";
            this.openProfiles.Size = new System.Drawing.Size(24, 24);
            this.openProfiles.TabIndex = 2;
            this.openProfiles.UseVisualStyleBackColor = true;
            this.openProfiles.Click += new System.EventHandler(this.openProfiles_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 600000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // timer3
            // 
            this.timer3.Enabled = true;
            this.timer3.Interval = 20000;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // mesoLabel
            // 
            this.mesoLabel.AutoSize = true;
            this.mesoLabel.Location = new System.Drawing.Point(6, 75);
            this.mesoLabel.Name = "mesoLabel";
            this.mesoLabel.Size = new System.Drawing.Size(42, 13);
            this.mesoLabel.TabIndex = 32;
            this.mesoLabel.Text = "Mesos: ";
            // 
            // PixelCLB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(772, 480);
            this.Controls.Add(this.hwidCheckLabel);
            this.Controls.Add(this.lastUpdatedLabel);
            this.Controls.Add(this.accessLevelLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.logOffPasswordLabel);
            this.Controls.Add(this.userLabel);
            this.Controls.Add(this.startTimeComboBox);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.activeAccountsGroupBox);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.PacketCenterLabel);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.openProfiles);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Calibri", 8F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(788, 518);
            this.MinimumSize = new System.Drawing.Size(292, 122);
            this.Name = "PixelCLB";
            this.Text = "PixelCLB";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PixelCLB_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.PacketCenterLabel.ResumeLayout(false);
            this.PacketCenterLabel.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.activeAccountsGroupBox.ResumeLayout(false);
            this.player_Box_Menu.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button openProfiles;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button DCButton;
        private System.Windows.Forms.Button SendPacket;
        private System.Windows.Forms.Button ReceivePacket;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxGetPlayer;
        private System.Windows.Forms.Button MovementPacketTest;
        private System.Windows.Forms.GroupBox PacketCenterLabel;
        private System.Windows.Forms.CheckBox LogRecvCheckBox;
        private System.Windows.Forms.CheckBox LogSendCheckBox;
        public System.Windows.Forms.ListBox logBox;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ListBox Player_Box;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListBox mushy_Box;
        private System.Windows.Forms.Button button5;
        public System.Windows.Forms.Label Map;
        public System.Windows.Forms.Label modeLabel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearLogToolStripMenuItem;
        public System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button removeWhiteList;
        private System.Windows.Forms.Button addWhiteList;
        private System.Windows.Forms.TextBox whiteListTextBox;
        private System.Windows.Forms.ListBox whiteListBox;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label mapleVersionLabel;
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.TextBox commandTextBox;
        private System.Windows.Forms.Label guildLabel;
        private System.Windows.Forms.Label ignLabel;
        private System.Windows.Forms.GroupBox activeAccountsGroupBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ContextMenuStrip player_Box_Menu;
        private System.Windows.Forms.ToolStripMenuItem copyPlayerUIDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyPlayerCoordsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cancelToolStripMenuItem2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customizeableBotSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem about1ToolStripMenuItem;
        private System.Windows.Forms.Label userLabel;
        public System.Windows.Forms.ListBox activeAccounts;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ComboBox startTimeComboBox;
        private System.Windows.Forms.ToolStripMenuItem openSettingsFolderToolStripMenuItem;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label logOffPasswordLabel;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label accessLevelLabel;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.Label lastUpdatedLabel;
        private System.Windows.Forms.Label hwidCheckLabel;
        private System.Windows.Forms.ToolStripMenuItem allProfileSettingsEditorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem profToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem proxySettingsToolStripMenu;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Timer timer3;
        private Label mesoLabel;
    }
}