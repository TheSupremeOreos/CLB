namespace PixelCLB
{
    partial class FMExportList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.listTime = new System.Windows.Forms.Label();
            this.totalStores = new System.Windows.Forms.Label();
            this.searchLabel = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.fmGrid = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.itemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemChannel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemRoom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemStoreName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.storeIGN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.fmGrid)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listTime
            // 
            this.listTime.AutoSize = true;
            this.listTime.Location = new System.Drawing.Point(9, 9);
            this.listTime.Name = "listTime";
            this.listTime.Size = new System.Drawing.Size(142, 13);
            this.listTime.TabIndex = 0;
            this.listTime.Text = "Free Market List Generated: ";
            // 
            // totalStores
            // 
            this.totalStores.AutoSize = true;
            this.totalStores.Location = new System.Drawing.Point(9, 22);
            this.totalStores.Name = "totalStores";
            this.totalStores.Size = new System.Drawing.Size(123, 13);
            this.totalStores.TabIndex = 0;
            this.totalStores.Text = "Total Stores Processed: ";
            // 
            // searchLabel
            // 
            this.searchLabel.AutoSize = true;
            this.searchLabel.Location = new System.Drawing.Point(477, 9);
            this.searchLabel.Name = "searchLabel";
            this.searchLabel.Size = new System.Drawing.Size(44, 13);
            this.searchLabel.TabIndex = 2;
            this.searchLabel.Text = "Search:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(527, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(148, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // fmGrid
            // 
            this.fmGrid.AllowUserToAddRows = false;
            this.fmGrid.AllowUserToDeleteRows = false;
            this.fmGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.fmGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.fmGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.fmGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.itemName,
            this.itemPrice,
            this.itemQuantity,
            this.itemChannel,
            this.itemRoom,
            this.itemStoreName,
            this.storeIGN,
            this.itemID});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.fmGrid.DefaultCellStyle = dataGridViewCellStyle7;
            this.fmGrid.Location = new System.Drawing.Point(12, 38);
            this.fmGrid.Name = "fmGrid";
            this.fmGrid.ReadOnly = true;
            this.fmGrid.RowHeadersVisible = false;
            this.fmGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.fmGrid.Size = new System.Drawing.Size(663, 226);
            this.fmGrid.TabIndex = 2;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 269);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(687, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(45, 17);
            this.toolStripStatusLabel1.Text = "Status: ";
            // 
            // itemName
            // 
            this.itemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.itemName.DefaultCellStyle = dataGridViewCellStyle2;
            this.itemName.FillWeight = 160F;
            this.itemName.HeaderText = "Item Info";
            this.itemName.MinimumWidth = 160;
            this.itemName.Name = "itemName";
            this.itemName.ReadOnly = true;
            this.itemName.Width = 160;
            // 
            // itemPrice
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.itemPrice.DefaultCellStyle = dataGridViewCellStyle3;
            this.itemPrice.FillWeight = 85F;
            this.itemPrice.HeaderText = "Price";
            this.itemPrice.MinimumWidth = 85;
            this.itemPrice.Name = "itemPrice";
            this.itemPrice.ReadOnly = true;
            this.itemPrice.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.itemPrice.Width = 85;
            // 
            // itemQuantity
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.itemQuantity.DefaultCellStyle = dataGridViewCellStyle4;
            this.itemQuantity.FillWeight = 70F;
            this.itemQuantity.HeaderText = "Quantity";
            this.itemQuantity.MinimumWidth = 70;
            this.itemQuantity.Name = "itemQuantity";
            this.itemQuantity.ReadOnly = true;
            this.itemQuantity.Width = 71;
            // 
            // itemChannel
            // 
            this.itemChannel.FillWeight = 30F;
            this.itemChannel.HeaderText = "CH";
            this.itemChannel.MinimumWidth = 30;
            this.itemChannel.Name = "itemChannel";
            this.itemChannel.ReadOnly = true;
            this.itemChannel.Width = 30;
            // 
            // itemRoom
            // 
            this.itemRoom.HeaderText = "FM";
            this.itemRoom.MinimumWidth = 30;
            this.itemRoom.Name = "itemRoom";
            this.itemRoom.ReadOnly = true;
            this.itemRoom.Width = 30;
            // 
            // itemStoreName
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.itemStoreName.DefaultCellStyle = dataGridViewCellStyle5;
            this.itemStoreName.HeaderText = "Store Name";
            this.itemStoreName.MinimumWidth = 100;
            this.itemStoreName.Name = "itemStoreName";
            this.itemStoreName.ReadOnly = true;
            // 
            // storeIGN
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.storeIGN.DefaultCellStyle = dataGridViewCellStyle6;
            this.storeIGN.HeaderText = "Store Owner";
            this.storeIGN.MinimumWidth = 100;
            this.storeIGN.Name = "storeIGN";
            this.storeIGN.ReadOnly = true;
            // 
            // itemID
            // 
            this.itemID.HeaderText = "Item ID";
            this.itemID.Name = "itemID";
            this.itemID.ReadOnly = true;
            this.itemID.Width = 66;
            // 
            // FMExportList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 291);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.fmGrid);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.searchLabel);
            this.Controls.Add(this.totalStores);
            this.Controls.Add(this.listTime);
            this.MinimumSize = new System.Drawing.Size(703, 329);
            this.Name = "FMExportList";
            this.Text = "Free Market Export List";
            this.Load += new System.EventHandler(this.FMExportList_Load);
            this.ResizeEnd += new System.EventHandler(this.FMExportList_ResizeEnd);
            this.Resize += new System.EventHandler(this.FMExportList_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.fmGrid)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label listTime;
        private System.Windows.Forms.Label totalStores;
        private System.Windows.Forms.Label searchLabel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView fmGrid;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemChannel;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemRoom;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemStoreName;
        private System.Windows.Forms.DataGridViewTextBoxColumn storeIGN;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemID;
    }
}