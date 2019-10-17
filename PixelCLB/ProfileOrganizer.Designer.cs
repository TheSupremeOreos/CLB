namespace PixelCLB
{
    partial class ProfileOrganizer
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
            this.profiles = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.reorganizeButton = new System.Windows.Forms.Button();
            this.moveUpButton = new System.Windows.Forms.Button();
            this.moveDownButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // profiles
            // 
            this.profiles.FormattingEnabled = true;
            this.profiles.Location = new System.Drawing.Point(6, 19);
            this.profiles.Name = "profiles";
            this.profiles.Size = new System.Drawing.Size(184, 446);
            this.profiles.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.moveDownButton);
            this.groupBox1.Controls.Add(this.moveUpButton);
            this.groupBox1.Controls.Add(this.reorganizeButton);
            this.groupBox1.Controls.Add(this.profiles);
            this.groupBox1.Location = new System.Drawing.Point(12, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(235, 505);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Profiles";
            // 
            // reorganizeButton
            // 
            this.reorganizeButton.Location = new System.Drawing.Point(6, 471);
            this.reorganizeButton.Name = "reorganizeButton";
            this.reorganizeButton.Size = new System.Drawing.Size(219, 23);
            this.reorganizeButton.TabIndex = 3;
            this.reorganizeButton.Text = "Reoganize my profiles!";
            this.reorganizeButton.UseVisualStyleBackColor = true;
            this.reorganizeButton.Click += new System.EventHandler(this.reorganizeButton_Click);
            // 
            // moveUpButton
            // 
            this.moveUpButton.Location = new System.Drawing.Point(196, 19);
            this.moveUpButton.Name = "moveUpButton";
            this.moveUpButton.Size = new System.Drawing.Size(29, 210);
            this.moveUpButton.TabIndex = 1;
            this.moveUpButton.Text = "M\r\nO\r\nV\r\nE\r\n\r\nU\r\nP";
            this.moveUpButton.UseVisualStyleBackColor = true;
            this.moveUpButton.Click += new System.EventHandler(this.moveUpButton_Click);
            // 
            // moveDownButton
            // 
            this.moveDownButton.Location = new System.Drawing.Point(196, 235);
            this.moveDownButton.Name = "moveDownButton";
            this.moveDownButton.Size = new System.Drawing.Size(29, 230);
            this.moveDownButton.TabIndex = 2;
            this.moveDownButton.Text = "M\r\nO\r\nV\r\nE\r\n\r\nD\r\nO\r\nW\r\nN";
            this.moveDownButton.UseVisualStyleBackColor = true;
            this.moveDownButton.Click += new System.EventHandler(this.moveDownButton_Click);
            // 
            // ProfileOrganizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(261, 528);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximumSize = new System.Drawing.Size(277, 562);
            this.MinimumSize = new System.Drawing.Size(277, 562);
            this.Name = "ProfileOrganizer";
            this.Text = "Profile Organizer / Reordering";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox profiles;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button moveDownButton;
        private System.Windows.Forms.Button moveUpButton;
        private System.Windows.Forms.Button reorganizeButton;
    }
}