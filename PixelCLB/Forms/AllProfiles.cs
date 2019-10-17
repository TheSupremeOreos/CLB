using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PixelCLB
{
    public partial class AllProfiles : Form
    {
        public AllProfiles()
        {
            InitializeComponent();
            this.load();
        }

        private void load()
        {
            foreach (string str in Program.iniFile.GetSectionNames())
            {
                if (str != "CLB Settings")
                    accountsDoNotChangeListBox.Items.Add(str);
            } 
            foreach (string str in Program.modes)
            {
                modeComboBox.Items.Add(str);
            }

        }

        private void upButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (accountsDoNotChangeListBox.SelectedIndex != -1)
                {
                    foreach (string str in accountsDoNotChangeListBox.SelectedItems)
                    {
                        accountsDoNotChangeListBox.Items.Remove(str);
                        accountsChangeListBox.Items.Add(str);
                    }
                }
                else
                {
                    MessageBox.Show("No account selected. \nPlease select an account below to move above.");
                    return;
                }
            }
            catch
            { }
        }

        private void downButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (accountsChangeListBox.SelectedIndex != -1)
                {
                    foreach (string str in accountsChangeListBox.SelectedItems)
                    {
                        accountsChangeListBox.Items.Remove(str);
                        accountsDoNotChangeListBox.Items.Add(str);
                    }
                }
                else
                {
                    MessageBox.Show("No account selected. \nPlease select an account above to move below.");
                    return;
                }
            }
            catch
            { }
        }

        private void updateChannelButton_Click()
        {
            try
            {
                if (channelText.Text.Equals(""))
                {
                    MessageBox.Show("Please enter a valid channel.");
                    return;
                }
                else
                {
                    string accountNames = "";
                    foreach (string str in accountsChangeListBox.Items)
                    {
                        Program.iniFile.WriteValue(str, "Channel", channelText.Text);
                        accountNames = string.Concat(accountNames, "\n", str);
                    }
                    MessageBox.Show("The following profiles's Channel settings has been set to " + channelText.Text + ":" + accountNames);
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }

        private void channelText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                updateChannelButton_Click();
            }
        }

        private void storeTitleButton_Click()
        {
            try
            {
                if (storeTitleText.Text.Equals(""))
                {
                    MessageBox.Show("Please enter a valid store title.");
                    return;
                }
                else
                {
                    string accountNames = "";
                    foreach (string str in accountsChangeListBox.Items)
                    {
                        Program.iniFile.WriteValue(str, "Title", storeTitleText.Text);
                        accountNames = string.Concat(accountNames, "\n", str);
                    }
                    MessageBox.Show("The following profiles's store title settings has been set to '" + storeTitleText.Text + "':" + accountNames);
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }

        private void storeTitleText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                storeTitleButton_Click();
            }
        }

        private void coordFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                updateFMCoordFilter();
            }
        }


        private void updateFMCoordFilter()
        {
            try
            {
                int num;
                if (xLowTextBox.Text != "")
                {
                    if (!int.TryParse(xLowTextBox.Text, out num))
                    {
                        MessageBox.Show("Please enter valid number in the X Low coordinate box. A default has been set for you.");
                        xLowTextBox.Text = "0";
                        return;
                    }
                }
                if (xHighTextBox.Text != "")
                {
                    if (!int.TryParse(xHighTextBox.Text, out num))
                    {
                        MessageBox.Show("Please enter valid number in the X High coordinate box. A default has been set for you.");
                        xHighTextBox.Text = "0";
                        return;
                    }
                }
                if (yLowTextBox.Text != "")
                {
                    if (!int.TryParse(yLowTextBox.Text, out num))
                    {
                        MessageBox.Show("Please enter valid number in the Y Low coordinate box. A default has been set for you.");
                        yLowTextBox.Text = "0";
                        return;
                    }
                }
                if (yHighTextBox.Text != "")
                {
                    if (!int.TryParse(yHighTextBox.Text, out num))
                    {
                        MessageBox.Show("Please enter valid number in the Y High coordinate box. A default has been set for you.");
                        yHighTextBox.Text = "0";
                        return;
                    }
                }
                if (int.Parse(xLowTextBox.Text) > int.Parse(xHighTextBox.Text))
                {
                    MessageBox.Show("xLow coord cannot be higher than xHigh Coord. A default has been set for you.");
                    xLowTextBox.Text = "0";
                    xHighTextBox.Text = "0";
                    return;
                }
                if (int.Parse(yLowTextBox.Text) > int.Parse(yHighTextBox.Text))
                {
                    MessageBox.Show("yLow coord cannot be higher than yHigh Coord. A default has been set for you.");
                    yLowTextBox.Text = "0";
                    yHighTextBox.Text = "0";
                    return;
                }
                string accountNames = "";
                foreach (string str in accountsChangeListBox.Items)
                {
                    Program.iniFile.WriteValue(str, "FMxLow", xLowTextBox.Text);
                    Program.iniFile.WriteValue(str, "FMxHigh", xHighTextBox.Text);
                    Program.iniFile.WriteValue(str, "FMyLow", yLowTextBox.Text);
                    Program.iniFile.WriteValue(str, "FMyHigh", yHighTextBox.Text);
                    accountNames = string.Concat(accountNames, "\n", str);
                }
                MessageBox.Show("The following profiles's store title settings has been set to '" + storeTitleText.Text + "':" + accountNames);
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }

        private void fmCoordsButton_Click()
        {
            try
            {
                int num;
                if (fmCoordsText.Text.Equals(""))
                {
                    MessageBox.Show("Please enter valid coords. A default has been set for you.");
                    fmCoordsText.Text = "0,0";
                    return;
                }
                else
                {
                    if (fmCoordsText.Text.Split(',').Length < 2)
                    {
                        MessageBox.Show("Please enter valid coords seperated by a comma. A default has been set for you.");
                        fmCoordsText.Text = "0,0";
                        return;
                    }
                    else
                    {
                        foreach (string str in fmCoordsText.Text.Split(','))
                        {
                            if (int.TryParse(str, out num))
                            {
                                continue;
                            }
                            else
                            {
                                MessageBox.Show("Please enter valid coords seperated by a comma. A default has been set for you.");
                                fmCoordsText.Text = "0,0";
                                return;
                            }
                        }
                        string accountNames = "";
                        foreach (string str in accountsChangeListBox.Items)
                        {
                            Program.iniFile.WriteValue(str, "Spawn", fmCoordsText.Text);
                            accountNames = string.Concat(accountNames, "\n", str);
                        }
                        MessageBox.Show("The following profiles's fm coords settings has been set to '" + fmCoordsText.Text + "':" + accountNames);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }
        private void fmCoordsText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                fmCoordsButton_Click();
            }
        }

        private void updateFullMapCoordOverrideButton_Click(object sender, EventArgs e)
        {
            try
            {
                string accountNames = "";
                if (fullMapCoordOverrideCheckBox.Checked)
                {
                    foreach (string str in accountsChangeListBox.Items)
                    {
                        Program.iniFile.WriteValue(str, "CoordOverride", "True");
                        accountNames = string.Concat(accountNames, "\n", str);
                    }
                    MessageBox.Show("The following profiles's coord override settings has been enabled:" + accountNames);
                }
                else
                {
                    foreach (string str in accountsChangeListBox.Items)
                    {
                        Program.iniFile.WriteValue(str, "CoordOverride", "False");
                        accountNames = string.Concat(accountNames, "\n", str);
                    }
                    MessageBox.Show("The following profiles's coord override settings has been disabled:" + accountNames);
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }

        private void AllProfiles_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                this.Dispose();
            }
            catch { }
        }

        private void moveAllUpButton_Click(object sender, EventArgs e)
        {
            List<string> strings = new List<string>();
            foreach (string str in accountsDoNotChangeListBox.Items)
            {
                strings.Add(str);
            }
            foreach (string str in strings)
            {
                accountsChangeListBox.Items.Add(str);
                accountsDoNotChangeListBox.Items.Remove(str);
            }
            strings.Clear();
        }

        private void moveAllDownButton_Click(object sender, EventArgs e)
        {
            List<string> strings = new List<string>();
            foreach (string str in accountsChangeListBox.Items)
            {
                strings.Add(str);
            }
            foreach (string str in strings)
            {
                accountsDoNotChangeListBox.Items.Add(str);
                accountsChangeListBox.Items.Remove(str);
            }
            strings.Clear();
        }

        private void updateModesButton_Click(object sender, EventArgs e)
        {
            if (modeComboBox.SelectedIndex > -1)
            {
                string accountNames = "";
                foreach (string str in accountsChangeListBox.Items)
                {
                    Program.iniFile.WriteValue(str, "Mode", modeComboBox.Text);
                    accountNames = string.Concat(accountNames, "\n", str);
                }
                MessageBox.Show("The following profiles's modes have been set to " + modeComboBox.Text + ":" + accountNames);
            }
            else
            {
                MessageBox.Show("Please select a mode before continuing.");
            }
        }

        private void updateFMOverrideButton_Click(object sender, EventArgs e)
        {
            if (fmRoomBox.Text.Equals(""))
            {
                MessageBox.Show("Please enter a valid FM Room.");
                return;
            }
            string accountNames = "";
            foreach (string str in accountsChangeListBox.Items)
            {
                Program.iniFile.WriteValue(str, "FMRoomNum", fmRoomBox.Text);
                if (fmRoomOverrideCheckBox.Checked)
                    Program.iniFile.WriteValue(str, "FMRoomOverride", "True");
                else
                    Program.iniFile.WriteValue(str, "FMRoomOverride", "False");
                accountNames = string.Concat(accountNames, "\n", str);
            }
            if (fmRoomOverrideCheckBox.Checked)
                MessageBox.Show("The following profiles's are now going be preforming a check to ensure they are in FM" + fmRoomBox.Text + ":" + accountNames);
            else
                MessageBox.Show("The following profiles's are now no longer preforming a check to ensure they are in the correct FM room:" + accountNames);
        }

    }
}
