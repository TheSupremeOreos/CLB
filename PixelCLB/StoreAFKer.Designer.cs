namespace PixelCLB
{
    partial class StoreAFKer
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
            this.label1 = new System.Windows.Forms.Label();
            this.ignTextBox = new System.Windows.Forms.TextBox();
            this.startButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.storeRadioButton = new System.Windows.Forms.RadioButton();
            this.permitRadioButton = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Shop Owner (IGN):";
            // 
            // ignTextBox
            // 
            this.ignTextBox.Location = new System.Drawing.Point(109, 6);
            this.ignTextBox.MaxLength = 12;
            this.ignTextBox.Name = "ignTextBox";
            this.ignTextBox.Size = new System.Drawing.Size(113, 21);
            this.ignTextBox.TabIndex = 1;
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(228, 4);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(44, 23);
            this.startButton.TabIndex = 2;
            this.startButton.Text = "AFK!";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(266, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "If person has two stores in the room, afk in the following:";
            // 
            // storeRadioButton
            // 
            this.storeRadioButton.AutoSize = true;
            this.storeRadioButton.Location = new System.Drawing.Point(12, 46);
            this.storeRadioButton.Name = "storeRadioButton";
            this.storeRadioButton.Size = new System.Drawing.Size(50, 17);
            this.storeRadioButton.TabIndex = 4;
            this.storeRadioButton.TabStop = true;
            this.storeRadioButton.Text = "Store";
            this.storeRadioButton.UseVisualStyleBackColor = true;
            // 
            // permitRadioButton
            // 
            this.permitRadioButton.AutoSize = true;
            this.permitRadioButton.Location = new System.Drawing.Point(68, 46);
            this.permitRadioButton.Name = "permitRadioButton";
            this.permitRadioButton.Size = new System.Drawing.Size(57, 17);
            this.permitRadioButton.TabIndex = 5;
            this.permitRadioButton.TabStop = true;
            this.permitRadioButton.Text = "Permit";
            this.permitRadioButton.UseVisualStyleBackColor = true;
            // 
            // StoreAFKer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 68);
            this.Controls.Add(this.permitRadioButton);
            this.Controls.Add(this.storeRadioButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.ignTextBox);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Calibri", 8F, System.Drawing.FontStyle.Bold);
            this.MaximumSize = new System.Drawing.Size(300, 106);
            this.MinimumSize = new System.Drawing.Size(300, 106);
            this.Name = "StoreAFKer";
            this.Text = "Shop AFKer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.StoreAFKer_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ignTextBox;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton storeRadioButton;
        private System.Windows.Forms.RadioButton permitRadioButton;
    }
}