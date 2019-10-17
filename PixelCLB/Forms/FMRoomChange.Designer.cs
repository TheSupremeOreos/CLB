namespace PixelCLB
{
    partial class FMRoomChange
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
            this.Location = new System.Windows.Forms.GroupBox();
            this.moveButton = new System.Windows.Forms.Button();
            this.roomBox = new System.Windows.Forms.NumericUpDown();
            this.channelBox = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Location.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.roomBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.channelBox)).BeginInit();
            this.SuspendLayout();
            // 
            // Location
            // 
            this.Location.Controls.Add(this.moveButton);
            this.Location.Controls.Add(this.roomBox);
            this.Location.Controls.Add(this.channelBox);
            this.Location.Controls.Add(this.label2);
            this.Location.Controls.Add(this.label1);
            this.Location.Location = new System.Drawing.Point(12, 3);
            this.Location.Name = "Location";
            this.Location.Size = new System.Drawing.Size(269, 45);
            this.Location.TabIndex = 0;
            this.Location.TabStop = false;
            this.Location.Text = "Location";
            // 
            // moveButton
            // 
            this.moveButton.Location = new System.Drawing.Point(206, 13);
            this.moveButton.Name = "moveButton";
            this.moveButton.Size = new System.Drawing.Size(57, 21);
            this.moveButton.TabIndex = 3;
            this.moveButton.Text = "Move";
            this.moveButton.UseVisualStyleBackColor = true;
            this.moveButton.Click += new System.EventHandler(this.moveButton_Click);
            // 
            // roomBox
            // 
            this.roomBox.Location = new System.Drawing.Point(153, 13);
            this.roomBox.Maximum = new decimal(new int[] {
            22,
            0,
            0,
            0});
            this.roomBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.roomBox.Name = "roomBox";
            this.roomBox.Size = new System.Drawing.Size(47, 21);
            this.roomBox.TabIndex = 2;
            this.roomBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // channelBox
            // 
            this.channelBox.Location = new System.Drawing.Point(58, 13);
            this.channelBox.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.channelBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.channelBox.Name = "channelBox";
            this.channelBox.Size = new System.Drawing.Size(51, 21);
            this.channelBox.TabIndex = 1;
            this.channelBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(115, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Room: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Channel";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "label3";
            // 
            // FMRoomChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 76);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Location);
            this.Font = new System.Drawing.Font("Calibri", 8F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FMRoomChange";
            this.Text = "FM Mover";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FMRoomChange_FormClosed);
            this.Location.ResumeLayout(false);
            this.Location.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.roomBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.channelBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox Location;
        private System.Windows.Forms.Button moveButton;
        private System.Windows.Forms.NumericUpDown roomBox;
        private System.Windows.Forms.NumericUpDown channelBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
    }
}