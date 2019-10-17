namespace PixelCLB
{
    partial class CharacterInfo
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
            this.components = new System.ComponentModel.Container();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.spamButton = new System.Windows.Forms.Button();
            this.sendAllOnceButton = new System.Windows.Forms.Button();
            this.spamSelectedButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.delayTextBox = new System.Windows.Forms.TextBox();
            this.packetCenterGrid = new System.Windows.Forms.DataGridView();
            this.Packet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.packetTextBox = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.chatTextBox = new System.Windows.Forms.TextBox();
            this.ignTextBox = new System.Windows.Forms.TextBox();
            this.chatCollectionListBox = new System.Windows.Forms.ListBox();
            this.chatMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.packetCenterGrid)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.spamButton);
            this.groupBox2.Controls.Add(this.sendAllOnceButton);
            this.groupBox2.Controls.Add(this.spamSelectedButton);
            this.groupBox2.Controls.Add(this.removeButton);
            this.groupBox2.Controls.Add(this.delayTextBox);
            this.groupBox2.Controls.Add(this.packetCenterGrid);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.packetTextBox);
            this.groupBox2.Font = new System.Drawing.Font("Calibri", 8F, System.Drawing.FontStyle.Bold);
            this.groupBox2.Location = new System.Drawing.Point(12, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(420, 247);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Packet Center";
            // 
            // spamButton
            // 
            this.spamButton.Location = new System.Drawing.Point(212, 216);
            this.spamButton.Name = "spamButton";
            this.spamButton.Size = new System.Drawing.Size(91, 23);
            this.spamButton.TabIndex = 7;
            this.spamButton.Text = "Spam All";
            this.spamButton.UseVisualStyleBackColor = true;
            this.spamButton.Click += new System.EventHandler(this.spamButton_Click);
            // 
            // sendAllOnceButton
            // 
            this.sendAllOnceButton.Location = new System.Drawing.Point(108, 216);
            this.sendAllOnceButton.Name = "sendAllOnceButton";
            this.sendAllOnceButton.Size = new System.Drawing.Size(91, 23);
            this.sendAllOnceButton.TabIndex = 6;
            this.sendAllOnceButton.Text = "Send All Once";
            this.sendAllOnceButton.UseVisualStyleBackColor = true;
            this.sendAllOnceButton.Click += new System.EventHandler(this.sendAllOnceButton_Click);
            // 
            // spamSelectedButton
            // 
            this.spamSelectedButton.Location = new System.Drawing.Point(9, 216);
            this.spamSelectedButton.Name = "spamSelectedButton";
            this.spamSelectedButton.Size = new System.Drawing.Size(84, 23);
            this.spamSelectedButton.TabIndex = 5;
            this.spamSelectedButton.Text = "Spam Selected";
            this.spamSelectedButton.UseVisualStyleBackColor = true;
            this.spamSelectedButton.Click += new System.EventHandler(this.spamSelectedButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(318, 216);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(91, 23);
            this.removeButton.TabIndex = 8;
            this.removeButton.Text = "Remove";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // delayTextBox
            // 
            this.delayTextBox.Location = new System.Drawing.Point(348, 189);
            this.delayTextBox.Name = "delayTextBox";
            this.delayTextBox.Size = new System.Drawing.Size(35, 21);
            this.delayTextBox.TabIndex = 3;
            this.delayTextBox.Text = "1000";
            // 
            // packetCenterGrid
            // 
            this.packetCenterGrid.AllowUserToAddRows = false;
            this.packetCenterGrid.AllowUserToDeleteRows = false;
            this.packetCenterGrid.BackgroundColor = System.Drawing.SystemColors.Control;
            this.packetCenterGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.packetCenterGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Packet,
            this.Column2});
            this.packetCenterGrid.Location = new System.Drawing.Point(9, 16);
            this.packetCenterGrid.Name = "packetCenterGrid";
            this.packetCenterGrid.RowHeadersVisible = false;
            this.packetCenterGrid.Size = new System.Drawing.Size(400, 170);
            this.packetCenterGrid.TabIndex = 1;
            // 
            // Packet
            // 
            this.Packet.HeaderText = "Packet";
            this.Packet.Name = "Packet";
            this.Packet.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Packet.Width = 320;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Delay";
            this.Column2.Name = "Column2";
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column2.Width = 80;
            // 
            // button1
            // 
            this.button1.Image = global::PixelCLB.Properties.Resources.Plus16;
            this.button1.Location = new System.Drawing.Point(386, 187);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(23, 23);
            this.button1.TabIndex = 4;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // packetTextBox
            // 
            this.packetTextBox.Location = new System.Drawing.Point(9, 189);
            this.packetTextBox.Name = "packetTextBox";
            this.packetTextBox.Size = new System.Drawing.Size(335, 21);
            this.packetTextBox.TabIndex = 2;
            this.packetTextBox.Text = "00 0F";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.comboBox1);
            this.groupBox3.Controls.Add(this.chatTextBox);
            this.groupBox3.Controls.Add(this.ignTextBox);
            this.groupBox3.Controls.Add(this.chatCollectionListBox);
            this.groupBox3.Font = new System.Drawing.Font("Calibri", 8F, System.Drawing.FontStyle.Bold);
            this.groupBox3.Location = new System.Drawing.Point(12, 256);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(625, 182);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Chat Center";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "All",
            "Buddy",
            "Guild",
            "Whisper"});
            this.comboBox1.Location = new System.Drawing.Point(6, 155);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(66, 21);
            this.comboBox1.TabIndex = 10;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // chatTextBox
            // 
            this.chatTextBox.Location = new System.Drawing.Point(167, 155);
            this.chatTextBox.MaxLength = 70;
            this.chatTextBox.Name = "chatTextBox";
            this.chatTextBox.Size = new System.Drawing.Size(452, 21);
            this.chatTextBox.TabIndex = 12;
            this.chatTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.chatTextBox_KeyPress);
            // 
            // ignTextBox
            // 
            this.ignTextBox.Location = new System.Drawing.Point(76, 155);
            this.ignTextBox.MaxLength = 13;
            this.ignTextBox.Name = "ignTextBox";
            this.ignTextBox.ReadOnly = true;
            this.ignTextBox.Size = new System.Drawing.Size(85, 21);
            this.ignTextBox.TabIndex = 11;
            this.ignTextBox.Text = "Whisp IGN";
            // 
            // chatCollectionListBox
            // 
            this.chatCollectionListBox.FormattingEnabled = true;
            this.chatCollectionListBox.Location = new System.Drawing.Point(6, 19);
            this.chatCollectionListBox.Name = "chatCollectionListBox";
            this.chatCollectionListBox.Size = new System.Drawing.Size(613, 134);
            this.chatCollectionListBox.TabIndex = 9;
            this.chatCollectionListBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chatCollectionListBox_MouseDown);
            // 
            // chatMenuStrip
            // 
            this.chatMenuStrip.Name = "chatMenuStrip";
            this.chatMenuStrip.Size = new System.Drawing.Size(61, 4);
            // 
            // groupBox1
            // 
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 8F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(438, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(199, 247);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Future Addition? (NPC Chatter)";
            // 
            // CharacterInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 455);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Calibri", 8F, System.Drawing.FontStyle.Bold);
            this.Name = "CharacterInfo";
            this.RightToLeftLayout = true;
            this.Text = "CharacterInfo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CharacterInfo_FormClosing);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.packetCenterGrid)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox packetTextBox;
        private System.Windows.Forms.DataGridView packetCenterGrid;
        private System.Windows.Forms.Button sendAllOnceButton;
        private System.Windows.Forms.Button spamSelectedButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.TextBox delayTextBox;
        private System.Windows.Forms.Button spamButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn Packet;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox chatTextBox;
        private System.Windows.Forms.TextBox ignTextBox;
        private System.Windows.Forms.ListBox chatCollectionListBox;
        private System.Windows.Forms.ContextMenuStrip chatMenuStrip;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}