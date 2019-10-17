namespace PixelCLB
{
    partial class ProxySettings
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.useProxyCheckBox = new System.Windows.Forms.CheckBox();
            this.browseTextBox = new System.Windows.Forms.TextBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.applyButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "txt files (*.txt)|*.txt";
            this.openFileDialog1.Title = "Browse your proxy list text file";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // useProxyCheckBox
            // 
            this.useProxyCheckBox.AutoSize = true;
            this.useProxyCheckBox.Location = new System.Drawing.Point(12, 42);
            this.useProxyCheckBox.Name = "useProxyCheckBox";
            this.useProxyCheckBox.Size = new System.Drawing.Size(71, 17);
            this.useProxyCheckBox.TabIndex = 3;
            this.useProxyCheckBox.Text = "Use Proxy";
            this.useProxyCheckBox.UseVisualStyleBackColor = true;
            // 
            // browseTextBox
            // 
            this.browseTextBox.Location = new System.Drawing.Point(12, 12);
            this.browseTextBox.Name = "browseTextBox";
            this.browseTextBox.ReadOnly = true;
            this.browseTextBox.Size = new System.Drawing.Size(200, 21);
            this.browseTextBox.TabIndex = 1;
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(218, 12);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(64, 20);
            this.browseButton.TabIndex = 2;
            this.browseButton.Text = "Browse";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // applyButton
            // 
            this.applyButton.Location = new System.Drawing.Point(89, 39);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(123, 21);
            this.applyButton.TabIndex = 4;
            this.applyButton.Text = "Apply Settings";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // ProxySettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 62);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.browseTextBox);
            this.Controls.Add(this.useProxyCheckBox);
            this.Font = new System.Drawing.Font("Calibri", 8F, System.Drawing.FontStyle.Bold);
            this.MinimumSize = new System.Drawing.Size(306, 100);
            this.Name = "ProxySettings";
            this.Text = "ProxySettings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.CheckBox useProxyCheckBox;
        private System.Windows.Forms.TextBox browseTextBox;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Button applyButton;
    }
}