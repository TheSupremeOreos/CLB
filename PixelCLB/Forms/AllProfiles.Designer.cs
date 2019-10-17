namespace PixelCLB
{
    partial class AllProfiles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AllProfiles));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.yHighTextBox = new System.Windows.Forms.TextBox();
            this.yLowTextBox = new System.Windows.Forms.TextBox();
            this.xHighTextBox = new System.Windows.Forms.TextBox();
            this.xLowTextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.fmRoomBox = new System.Windows.Forms.NumericUpDown();
            this.updateFMOverrideButton = new System.Windows.Forms.Button();
            this.fmRoomOverrideCheckBox = new System.Windows.Forms.CheckBox();
            this.updateModesButton = new System.Windows.Forms.Button();
            this.modeComboBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.fmCoordsText = new System.Windows.Forms.TextBox();
            this.updateFullMapCoordOverrideButton = new System.Windows.Forms.Button();
            this.fullMapCoordOverrideCheckBox = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.storeTitleText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.channelText = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.moveAllDownButton = new System.Windows.Forms.Button();
            this.moveAllUpButton = new System.Windows.Forms.Button();
            this.downButton = new System.Windows.Forms.Button();
            this.upButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.accountsChangeListBox = new System.Windows.Forms.ListBox();
            this.accountsDoNotChangeListBox = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fmRoomBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.channelText)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.yHighTextBox);
            this.groupBox1.Controls.Add(this.yLowTextBox);
            this.groupBox1.Controls.Add(this.xHighTextBox);
            this.groupBox1.Controls.Add(this.xLowTextBox);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.fmRoomBox);
            this.groupBox1.Controls.Add(this.updateFMOverrideButton);
            this.groupBox1.Controls.Add(this.fmRoomOverrideCheckBox);
            this.groupBox1.Controls.Add(this.updateModesButton);
            this.groupBox1.Controls.Add(this.modeComboBox);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.fmCoordsText);
            this.groupBox1.Controls.Add(this.updateFullMapCoordOverrideButton);
            this.groupBox1.Controls.Add(this.fullMapCoordOverrideCheckBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.storeTitleText);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.channelText);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Location = new System.Drawing.Point(203, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(188, 302);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Universal Settings";
            // 
            // yHighTextBox
            // 
            this.yHighTextBox.Location = new System.Drawing.Point(148, 271);
            this.yHighTextBox.Name = "yHighTextBox";
            this.yHighTextBox.Size = new System.Drawing.Size(30, 21);
            this.yHighTextBox.TabIndex = 19;
            this.yHighTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.coordFilter_KeyPress);
            // 
            // yLowTextBox
            // 
            this.yLowTextBox.Location = new System.Drawing.Point(112, 271);
            this.yLowTextBox.Name = "yLowTextBox";
            this.yLowTextBox.Size = new System.Drawing.Size(30, 21);
            this.yLowTextBox.TabIndex = 18;
            this.yLowTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.coordFilter_KeyPress);
            // 
            // xHighTextBox
            // 
            this.xHighTextBox.Location = new System.Drawing.Point(62, 271);
            this.xHighTextBox.Name = "xHighTextBox";
            this.xHighTextBox.Size = new System.Drawing.Size(30, 21);
            this.xHighTextBox.TabIndex = 17;
            this.xHighTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.coordFilter_KeyPress);
            // 
            // xLowTextBox
            // 
            this.xLowTextBox.Location = new System.Drawing.Point(22, 271);
            this.xLowTextBox.Name = "xLowTextBox";
            this.xLowTextBox.Size = new System.Drawing.Size(30, 21);
            this.xLowTextBox.TabIndex = 16;
            this.xLowTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.coordFilter_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(98, 274);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(14, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Y:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 274);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(16, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "X:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 258);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(109, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "[FM] Coordinate Filter";
            // 
            // fmRoomBox
            // 
            this.fmRoomBox.Location = new System.Drawing.Point(127, 209);
            this.fmRoomBox.Name = "fmRoomBox";
            this.fmRoomBox.Size = new System.Drawing.Size(51, 21);
            this.fmRoomBox.TabIndex = 14;
            // 
            // updateFMOverrideButton
            // 
            this.updateFMOverrideButton.Location = new System.Drawing.Point(9, 232);
            this.updateFMOverrideButton.Name = "updateFMOverrideButton";
            this.updateFMOverrideButton.Size = new System.Drawing.Size(169, 20);
            this.updateFMOverrideButton.TabIndex = 15;
            this.updateFMOverrideButton.Text = "Update FM Room Override";
            this.updateFMOverrideButton.UseVisualStyleBackColor = true;
            this.updateFMOverrideButton.Click += new System.EventHandler(this.updateFMOverrideButton_Click);
            // 
            // fmRoomOverrideCheckBox
            // 
            this.fmRoomOverrideCheckBox.AutoSize = true;
            this.fmRoomOverrideCheckBox.Location = new System.Drawing.Point(9, 213);
            this.fmRoomOverrideCheckBox.Name = "fmRoomOverrideCheckBox";
            this.fmRoomOverrideCheckBox.Size = new System.Drawing.Size(119, 17);
            this.fmRoomOverrideCheckBox.TabIndex = 13;
            this.fmRoomOverrideCheckBox.Text = "FM Room # Override";
            this.fmRoomOverrideCheckBox.UseVisualStyleBackColor = true;
            // 
            // updateModesButton
            // 
            this.updateModesButton.Location = new System.Drawing.Point(10, 143);
            this.updateModesButton.Name = "updateModesButton";
            this.updateModesButton.Size = new System.Drawing.Size(169, 20);
            this.updateModesButton.TabIndex = 9;
            this.updateModesButton.Text = "Update Modes";
            this.updateModesButton.UseVisualStyleBackColor = true;
            this.updateModesButton.Click += new System.EventHandler(this.updateModesButton_Click);
            // 
            // modeComboBox
            // 
            this.modeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.modeComboBox.FormattingEnabled = true;
            this.modeComboBox.Location = new System.Drawing.Point(49, 119);
            this.modeComboBox.Name = "modeComboBox";
            this.modeComboBox.Size = new System.Drawing.Size(129, 21);
            this.modeComboBox.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 127);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Mode:";
            // 
            // fmCoordsText
            // 
            this.fmCoordsText.Location = new System.Drawing.Point(9, 92);
            this.fmCoordsText.Name = "fmCoordsText";
            this.fmCoordsText.Size = new System.Drawing.Size(169, 21);
            this.fmCoordsText.TabIndex = 7;
            this.fmCoordsText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.fmCoordsText_KeyPress);
            // 
            // updateFullMapCoordOverrideButton
            // 
            this.updateFullMapCoordOverrideButton.Location = new System.Drawing.Point(9, 186);
            this.updateFullMapCoordOverrideButton.Name = "updateFullMapCoordOverrideButton";
            this.updateFullMapCoordOverrideButton.Size = new System.Drawing.Size(169, 20);
            this.updateFullMapCoordOverrideButton.TabIndex = 11;
            this.updateFullMapCoordOverrideButton.Text = "Update Full Map Coord Override";
            this.updateFullMapCoordOverrideButton.UseVisualStyleBackColor = true;
            this.updateFullMapCoordOverrideButton.Click += new System.EventHandler(this.updateFullMapCoordOverrideButton_Click);
            // 
            // fullMapCoordOverrideCheckBox
            // 
            this.fullMapCoordOverrideCheckBox.AutoSize = true;
            this.fullMapCoordOverrideCheckBox.Location = new System.Drawing.Point(9, 168);
            this.fullMapCoordOverrideCheckBox.Name = "fullMapCoordOverrideCheckBox";
            this.fullMapCoordOverrideCheckBox.Size = new System.Drawing.Size(162, 17);
            this.fullMapCoordOverrideCheckBox.TabIndex = 10;
            this.fullMapCoordOverrideCheckBox.Text = "[FM] Full-Map Coord Override";
            this.fullMapCoordOverrideCheckBox.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "[FM] Coordinates";
            // 
            // storeTitleText
            // 
            this.storeTitleText.Location = new System.Drawing.Point(9, 54);
            this.storeTitleText.Name = "storeTitleText";
            this.storeTitleText.Size = new System.Drawing.Size(169, 21);
            this.storeTitleText.TabIndex = 6;
            this.storeTitleText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.storeTitleText_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "[FM] Store Title";
            // 
            // channelText
            // 
            this.channelText.Location = new System.Drawing.Point(58, 14);
            this.channelText.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.channelText.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.channelText.Name = "channelText";
            this.channelText.Size = new System.Drawing.Size(120, 21);
            this.channelText.TabIndex = 5;
            this.channelText.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.channelText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.channelText_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Channel";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(52, 274);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(10, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "-";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(140, 274);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(10, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "-";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.moveAllDownButton);
            this.groupBox2.Controls.Add(this.moveAllUpButton);
            this.groupBox2.Controls.Add(this.downButton);
            this.groupBox2.Controls.Add(this.upButton);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.accountsChangeListBox);
            this.groupBox2.Controls.Add(this.accountsDoNotChangeListBox);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(185, 482);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Accounts";
            // 
            // moveAllDownButton
            // 
            this.moveAllDownButton.Location = new System.Drawing.Point(95, 241);
            this.moveAllDownButton.Name = "moveAllDownButton";
            this.moveAllDownButton.Size = new System.Drawing.Size(84, 20);
            this.moveAllDownButton.TabIndex = 4;
            this.moveAllDownButton.Text = "Move All Down";
            this.moveAllDownButton.UseVisualStyleBackColor = true;
            this.moveAllDownButton.Click += new System.EventHandler(this.moveAllDownButton_Click);
            // 
            // moveAllUpButton
            // 
            this.moveAllUpButton.Location = new System.Drawing.Point(6, 241);
            this.moveAllUpButton.Name = "moveAllUpButton";
            this.moveAllUpButton.Size = new System.Drawing.Size(84, 20);
            this.moveAllUpButton.TabIndex = 3;
            this.moveAllUpButton.Text = "Move All Up";
            this.moveAllUpButton.UseVisualStyleBackColor = true;
            this.moveAllUpButton.Click += new System.EventHandler(this.moveAllUpButton_Click);
            // 
            // downButton
            // 
            this.downButton.Location = new System.Drawing.Point(95, 218);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(84, 20);
            this.downButton.TabIndex = 2;
            this.downButton.Text = "Move Down";
            this.downButton.UseVisualStyleBackColor = true;
            this.downButton.Click += new System.EventHandler(this.downButton_Click);
            // 
            // upButton
            // 
            this.upButton.Location = new System.Drawing.Point(6, 218);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(84, 20);
            this.upButton.TabIndex = 1;
            this.upButton.Text = "Move Up";
            this.upButton.UseVisualStyleBackColor = true;
            this.upButton.Click += new System.EventHandler(this.upButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 266);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 26);
            this.label2.TabIndex = 0;
            this.label2.Text = "Do not change the settings \r\nof the following profiles:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Change the settings to the\r\nfollowing profiles:";
            // 
            // accountsChangeListBox
            // 
            this.accountsChangeListBox.FormattingEnabled = true;
            this.accountsChangeListBox.Location = new System.Drawing.Point(6, 45);
            this.accountsChangeListBox.Name = "accountsChangeListBox";
            this.accountsChangeListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.accountsChangeListBox.Size = new System.Drawing.Size(173, 160);
            this.accountsChangeListBox.TabIndex = 0;
            // 
            // accountsDoNotChangeListBox
            // 
            this.accountsDoNotChangeListBox.FormattingEnabled = true;
            this.accountsDoNotChangeListBox.Location = new System.Drawing.Point(6, 298);
            this.accountsDoNotChangeListBox.Name = "accountsDoNotChangeListBox";
            this.accountsDoNotChangeListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.accountsDoNotChangeListBox.Size = new System.Drawing.Size(173, 173);
            this.accountsDoNotChangeListBox.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(204, 317);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(182, 169);
            this.label6.TabIndex = 1;
            this.label6.Text = resources.GetString("label6.Text");
            // 
            // AllProfiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 499);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Calibri", 8F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AllProfiles";
            this.Text = "Change All Profile Settings";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AllProfiles_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fmRoomBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.channelText)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox accountsChangeListBox;
        private System.Windows.Forms.ListBox accountsDoNotChangeListBox;
        private System.Windows.Forms.TextBox fmCoordsText;
        private System.Windows.Forms.Button updateFullMapCoordOverrideButton;
        private System.Windows.Forms.CheckBox fullMapCoordOverrideCheckBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox storeTitleText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown channelText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button downButton;
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button moveAllDownButton;
        private System.Windows.Forms.Button moveAllUpButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button updateModesButton;
        private System.Windows.Forms.ComboBox modeComboBox;
        private System.Windows.Forms.NumericUpDown fmRoomBox;
        private System.Windows.Forms.Button updateFMOverrideButton;
        private System.Windows.Forms.CheckBox fmRoomOverrideCheckBox;
        private System.Windows.Forms.TextBox xLowTextBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox yHighTextBox;
        private System.Windows.Forms.TextBox yLowTextBox;
        private System.Windows.Forms.TextBox xHighTextBox;
    }
}