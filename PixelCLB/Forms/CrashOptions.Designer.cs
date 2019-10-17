namespace PixelCLB
{
    partial class CrashOptions
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.crashButton = new System.Windows.Forms.Button();
            this.levelBox = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.TimerLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.levelBox)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.crashButton);
            this.groupBox1.Controls.Add(this.levelBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(163, 52);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // crashButton
            // 
            this.crashButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.crashButton.Location = new System.Drawing.Point(99, 18);
            this.crashButton.Name = "crashButton";
            this.crashButton.Size = new System.Drawing.Size(51, 23);
            this.crashButton.TabIndex = 1;
            this.crashButton.Text = "Crash";
            this.crashButton.UseVisualStyleBackColor = true;
            this.crashButton.Click += new System.EventHandler(this.crashButton_Click);
            // 
            // levelBox
            // 
            this.levelBox.Location = new System.Drawing.Point(47, 20);
            this.levelBox.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.levelBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.levelBox.Name = "levelBox";
            this.levelBox.Size = new System.Drawing.Size(46, 21);
            this.levelBox.TabIndex = 1;
            this.levelBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Level:";
            // 
            // TimerLabel
            // 
            this.TimerLabel.AutoSize = true;
            this.TimerLabel.Location = new System.Drawing.Point(9, 70);
            this.TimerLabel.Name = "TimerLabel";
            this.TimerLabel.Size = new System.Drawing.Size(37, 13);
            this.TimerLabel.TabIndex = 1;
            this.TimerLabel.Text = "label2";
            // 
            // CrashOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(189, 92);
            this.Controls.Add(this.TimerLabel);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Calibri", 8F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CrashOptions";
            this.Text = "AB Crash Options";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CrashOptions_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.levelBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown levelBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button crashButton;
        private System.Windows.Forms.Label TimerLabel;

    }
}