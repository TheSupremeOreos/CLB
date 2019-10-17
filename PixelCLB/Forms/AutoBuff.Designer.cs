namespace PixelCLB
{
    partial class AutoBuff
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
            this.autoBuffDataGrid = new System.Windows.Forms.DataGridView();
            this.skillIDTextBox = new System.Windows.Forms.TextBox();
            this.delayTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.skillListBox = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.levelComboBox = new System.Windows.Forms.ComboBox();
            this.addButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.updateButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.useComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.mpBelowTextBox = new System.Windows.Forms.TextBox();
            this.Skill_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Skill_Level = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Delay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.autoBuffDataGrid)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // autoBuffDataGrid
            // 
            this.autoBuffDataGrid.AllowUserToAddRows = false;
            this.autoBuffDataGrid.AllowUserToDeleteRows = false;
            this.autoBuffDataGrid.AllowUserToOrderColumns = true;
            this.autoBuffDataGrid.AllowUserToResizeColumns = false;
            this.autoBuffDataGrid.AllowUserToResizeRows = false;
            this.autoBuffDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.autoBuffDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Skill_ID,
            this.Skill_Level,
            this.Delay});
            this.autoBuffDataGrid.Location = new System.Drawing.Point(12, 12);
            this.autoBuffDataGrid.Name = "autoBuffDataGrid";
            this.autoBuffDataGrid.RowHeadersVisible = false;
            this.autoBuffDataGrid.Size = new System.Drawing.Size(264, 129);
            this.autoBuffDataGrid.TabIndex = 0;
            // 
            // skillIDTextBox
            // 
            this.skillIDTextBox.Location = new System.Drawing.Point(12, 158);
            this.skillIDTextBox.Name = "skillIDTextBox";
            this.skillIDTextBox.Size = new System.Drawing.Size(91, 21);
            this.skillIDTextBox.TabIndex = 1;
            // 
            // delayTextBox
            // 
            this.delayTextBox.Location = new System.Drawing.Point(109, 158);
            this.delayTextBox.Name = "delayTextBox";
            this.delayTextBox.Size = new System.Drawing.Size(48, 21);
            this.delayTextBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 144);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Skill ID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(164, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Level:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(106, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Delay (ms):";
            // 
            // skillListBox
            // 
            this.skillListBox.FormattingEnabled = true;
            this.skillListBox.HorizontalScrollbar = true;
            this.skillListBox.Location = new System.Drawing.Point(6, 20);
            this.skillListBox.Name = "skillListBox";
            this.skillListBox.Size = new System.Drawing.Size(245, 173);
            this.skillListBox.Sorted = true;
            this.skillListBox.TabIndex = 10;
            this.skillListBox.SelectedIndexChanged += new System.EventHandler(this.skillListBox_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.skillListBox);
            this.groupBox1.Location = new System.Drawing.Point(284, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(257, 225);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Skill Name, ID";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 202);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Skill Name Search:";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(111, 198);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(140, 21);
            this.textBox3.TabIndex = 11;
            this.textBox3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox3_KeyPress);
            // 
            // levelComboBox
            // 
            this.levelComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.levelComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.levelComboBox.FormattingEnabled = true;
            this.levelComboBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32"});
            this.levelComboBox.Location = new System.Drawing.Point(167, 158);
            this.levelComboBox.Name = "levelComboBox";
            this.levelComboBox.Size = new System.Drawing.Size(49, 21);
            this.levelComboBox.TabIndex = 3;
            // 
            // addButton
            // 
            this.addButton.Image = global::PixelCLB.Properties.Resources.Plus16;
            this.addButton.Location = new System.Drawing.Point(222, 155);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(24, 24);
            this.addButton.TabIndex = 4;
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Image = global::PixelCLB.Properties.Resources.Delete16;
            this.removeButton.Location = new System.Drawing.Point(252, 155);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(24, 24);
            this.removeButton.TabIndex = 5;
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(97, 214);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(179, 23);
            this.startButton.TabIndex = 9;
            this.startButton.Text = "START + Save";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(12, 214);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(79, 23);
            this.updateButton.TabIndex = 8;
            this.updateButton.Text = "Save + Update";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 192);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Using Pot:";
            // 
            // useComboBox
            // 
            this.useComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.useComboBox.FormattingEnabled = true;
            this.useComboBox.Location = new System.Drawing.Point(64, 187);
            this.useComboBox.Name = "useComboBox";
            this.useComboBox.Size = new System.Drawing.Size(94, 21);
            this.useComboBox.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(164, 190);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "MP Below:";
            // 
            // mpBelowTextBox
            // 
            this.mpBelowTextBox.Location = new System.Drawing.Point(222, 185);
            this.mpBelowTextBox.Name = "mpBelowTextBox";
            this.mpBelowTextBox.Size = new System.Drawing.Size(54, 21);
            this.mpBelowTextBox.TabIndex = 7;
            // 
            // Skill_ID
            // 
            this.Skill_ID.HeaderText = "Skill ID";
            this.Skill_ID.Name = "Skill_ID";
            // 
            // Skill_Level
            // 
            this.Skill_Level.HeaderText = "Skill Level";
            this.Skill_Level.MaxInputLength = 3;
            this.Skill_Level.MinimumWidth = 80;
            this.Skill_Level.Name = "Skill_Level";
            this.Skill_Level.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Skill_Level.Width = 80;
            // 
            // Delay
            // 
            this.Delay.HeaderText = "Delay (ms)";
            this.Delay.Name = "Delay";
            this.Delay.ReadOnly = true;
            this.Delay.Width = 80;
            // 
            // AutoBuff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 240);
            this.Controls.Add(this.mpBelowTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.useComboBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.levelComboBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.delayTextBox);
            this.Controls.Add(this.skillIDTextBox);
            this.Controls.Add(this.autoBuffDataGrid);
            this.Font = new System.Drawing.Font("Calibri", 8F, System.Drawing.FontStyle.Bold);
            this.MaximumSize = new System.Drawing.Size(563, 278);
            this.MinimumSize = new System.Drawing.Size(563, 278);
            this.Name = "AutoBuff";
            this.Text = "Auto Buff";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AutoBuff_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.autoBuffDataGrid)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView autoBuffDataGrid;
        private System.Windows.Forms.TextBox skillIDTextBox;
        private System.Windows.Forms.TextBox delayTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox skillListBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox levelComboBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox useComboBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox mpBelowTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn Skill_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Skill_Level;
        private System.Windows.Forms.DataGridViewTextBoxColumn Delay;
    }
}