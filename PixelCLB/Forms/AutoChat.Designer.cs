namespace PixelCLB
{
    partial class AutoChat
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
            this.label5 = new System.Windows.Forms.Label();
            this.typeComboBox = new System.Windows.Forms.ComboBox();
            this.typeListBox = new System.Windows.Forms.ListBox();
            this.autoChatStartButton = new System.Windows.Forms.Button();
            this.typeRemoveButton = new System.Windows.Forms.Button();
            this.typeAddButton = new System.Windows.Forms.Button();
            this.textDataGrid = new System.Windows.Forms.DataGridView();
            this.textBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Delay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBox = new System.Windows.Forms.TextBox();
            this.delayTextBox = new System.Windows.Forms.TextBox();
            this.addText = new System.Windows.Forms.Button();
            this.deleteText = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.textDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(375, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Type:";
            // 
            // typeComboBox
            // 
            this.typeComboBox.FormattingEnabled = true;
            this.typeComboBox.Items.AddRange(new object[] {
            "All Chat",
            "FM Stores",
            "Change FM Rooms"});
            this.typeComboBox.Location = new System.Drawing.Point(416, 12);
            this.typeComboBox.Name = "typeComboBox";
            this.typeComboBox.Size = new System.Drawing.Size(124, 21);
            this.typeComboBox.TabIndex = 5;
            // 
            // typeListBox
            // 
            this.typeListBox.FormattingEnabled = true;
            this.typeListBox.Location = new System.Drawing.Point(378, 38);
            this.typeListBox.Name = "typeListBox";
            this.typeListBox.Size = new System.Drawing.Size(132, 69);
            this.typeListBox.TabIndex = 6;
            // 
            // autoChatStartButton
            // 
            this.autoChatStartButton.Location = new System.Drawing.Point(378, 112);
            this.autoChatStartButton.Name = "autoChatStartButton";
            this.autoChatStartButton.Size = new System.Drawing.Size(162, 23);
            this.autoChatStartButton.TabIndex = 9;
            this.autoChatStartButton.Text = "Start";
            this.autoChatStartButton.UseVisualStyleBackColor = true;
            this.autoChatStartButton.Click += new System.EventHandler(this.autoChatStartButton_Click);
            // 
            // typeRemoveButton
            // 
            this.typeRemoveButton.Image = global::PixelCLB.Properties.Resources.Delete16;
            this.typeRemoveButton.Location = new System.Drawing.Point(516, 70);
            this.typeRemoveButton.Name = "typeRemoveButton";
            this.typeRemoveButton.Size = new System.Drawing.Size(24, 24);
            this.typeRemoveButton.TabIndex = 8;
            this.typeRemoveButton.UseVisualStyleBackColor = true;
            this.typeRemoveButton.Click += new System.EventHandler(this.typeRemoveButton_Click);
            // 
            // typeAddButton
            // 
            this.typeAddButton.Image = global::PixelCLB.Properties.Resources.Plus16;
            this.typeAddButton.Location = new System.Drawing.Point(516, 38);
            this.typeAddButton.Name = "typeAddButton";
            this.typeAddButton.Size = new System.Drawing.Size(24, 24);
            this.typeAddButton.TabIndex = 7;
            this.typeAddButton.UseVisualStyleBackColor = true;
            this.typeAddButton.Click += new System.EventHandler(this.typeAddButton_Click);
            // 
            // textDataGrid
            // 
            this.textDataGrid.AllowUserToAddRows = false;
            this.textDataGrid.AllowUserToDeleteRows = false;
            this.textDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.textDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.textBoxColumn,
            this.Delay});
            this.textDataGrid.Location = new System.Drawing.Point(9, 12);
            this.textDataGrid.Name = "textDataGrid";
            this.textDataGrid.RowHeadersVisible = false;
            this.textDataGrid.Size = new System.Drawing.Size(363, 97);
            this.textDataGrid.TabIndex = 0;
            // 
            // textBoxColumn
            // 
            this.textBoxColumn.FillWeight = 300F;
            this.textBoxColumn.HeaderText = "Text";
            this.textBoxColumn.MinimumWidth = 100;
            this.textBoxColumn.Name = "textBoxColumn";
            this.textBoxColumn.Width = 300;
            // 
            // Delay
            // 
            this.Delay.FillWeight = 60F;
            this.Delay.HeaderText = "Delay";
            this.Delay.Name = "Delay";
            this.Delay.ReadOnly = true;
            this.Delay.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Delay.Width = 60;
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(9, 116);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(249, 21);
            this.textBox.TabIndex = 1;
            this.textBox.Text = "Text";
            // 
            // delayTextBox
            // 
            this.delayTextBox.Location = new System.Drawing.Point(264, 116);
            this.delayTextBox.Name = "delayTextBox";
            this.delayTextBox.Size = new System.Drawing.Size(50, 21);
            this.delayTextBox.TabIndex = 2;
            this.delayTextBox.Text = "1200";
            // 
            // addText
            // 
            this.addText.Image = global::PixelCLB.Properties.Resources.Plus16;
            this.addText.Location = new System.Drawing.Point(320, 112);
            this.addText.Name = "addText";
            this.addText.Size = new System.Drawing.Size(24, 24);
            this.addText.TabIndex = 3;
            this.addText.UseVisualStyleBackColor = true;
            this.addText.Click += new System.EventHandler(this.addText_Click);
            // 
            // deleteText
            // 
            this.deleteText.Image = global::PixelCLB.Properties.Resources.Delete16;
            this.deleteText.Location = new System.Drawing.Point(349, 112);
            this.deleteText.Name = "deleteText";
            this.deleteText.Size = new System.Drawing.Size(24, 24);
            this.deleteText.TabIndex = 4;
            this.deleteText.UseVisualStyleBackColor = true;
            this.deleteText.Click += new System.EventHandler(this.deleteText_Click);
            // 
            // AutoChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 140);
            this.Controls.Add(this.deleteText);
            this.Controls.Add(this.addText);
            this.Controls.Add(this.delayTextBox);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.textDataGrid);
            this.Controls.Add(this.typeRemoveButton);
            this.Controls.Add(this.typeAddButton);
            this.Controls.Add(this.autoChatStartButton);
            this.Controls.Add(this.typeListBox);
            this.Controls.Add(this.typeComboBox);
            this.Controls.Add(this.label5);
            this.Font = new System.Drawing.Font("Calibri", 8F, System.Drawing.FontStyle.Bold);
            this.Name = "AutoChat";
            this.Text = "Auto Chatter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AutoChat_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.textDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox typeComboBox;
        private System.Windows.Forms.ListBox typeListBox;
        private System.Windows.Forms.Button autoChatStartButton;
        private System.Windows.Forms.Button typeAddButton;
        private System.Windows.Forms.Button typeRemoveButton;
        private System.Windows.Forms.DataGridView textDataGrid;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.TextBox delayTextBox;
        private System.Windows.Forms.Button addText;
        private System.Windows.Forms.Button deleteText;
        private System.Windows.Forms.DataGridViewTextBoxColumn textBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Delay;
    }
}