using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Net.Sockets;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PixelCLB;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Threading;
using System.IO.Pipes;

namespace PixelCLB
{
    public partial class Profiles : Form
    {
        private bool refresh = false;
        public Profiles(string text, bool changePIC)
        {
            InitializeComponent();
            TitleTextBox.Text = Program.clbName + " v" + Program.version;
            Program.gui.GUIInvokeMethod(() =>
            {
                foreach (string str in Program.modes)
                {
                    ModeComboBox.Items.Add(str);
                }

                foreach (string str in Program.worlds)
                {
                    WorldcomboBox.Items.Add(str);
                }

                foreach (string str in Program.shopTypes)
                {
                    ShopTypeComboBox.Items.Add(str);
                }
                ShopTypeComboBox.SelectedIndex = 0;
                WorldcomboBox.SelectedIndex = 0;
                ModeComboBox.SelectedIndex = 0;
                ChannelBox.MouseWheel += new MouseEventHandler(comboBox_MouseWheel);
                fmRoomBox.MouseWheel += new MouseEventHandler(comboBox_MouseWheel);
                CharNumber.MouseWheel += new MouseEventHandler(comboBox_MouseWheel);
                ShopTypeComboBox.MouseWheel += new MouseEventHandler(comboBox_MouseWheel);
                WorldcomboBox.MouseWheel += new MouseEventHandler(comboBox_MouseWheel);
                ModeComboBox.MouseWheel += new MouseEventHandler(comboBox_MouseWheel);
                if (text != "")
                {
                    updateAccountInfo(text, changePIC);
                }
            });
        }

        private void Profiles_Load(object sender, EventArgs e)
        {
            refreshAccounts();
            Program.gui.profile = this;
        }

        private void refreshAccounts()
        {//Select Profile
            try
            {
                if (File.Exists(Program.settingFile))
                {
                    if (refresh == false)
                    {
                        Program.gui.GUIInvokeMethod(() =>
                        {
                            accountComboBox.BeginUpdate();
                            accountComboBox.Items.Clear();
                            accountComboBox.Items.Add("");
                            foreach (string section in Program.iniFile.GetSectionNames())
                            {
                                if (section != "CLB Settings")
                                    accountComboBox.Items.Add(section);
                            }
                            accountComboBox.EndUpdate();
                            refresh = true;
                        });
                    }
                }
                else
                {
                    refresh = false;
                    File.Create(Program.settingFile).Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message + " A settings file will now be created. Please add profiles by clicking the + button on the side.");
                File.Create(Program.settingFile).Dispose();
            }
        }


        private void comboBox_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            hideAllNecessary();
            if (ModeComboBox.Text == "Expedition DC" || ModeComboBox.Text == "Alliance DC")
            {
                dcModeGroupBox.Show();
            }
            else if (ModeComboBox.Text == "Mushy Redirect")
            {
                hiredMerchantRedirectGroupBox.Show();
            }
            else
            {
                hideAllNecessary();
            }
        }

        private void hideAllNecessary()
        {
            dcModeGroupBox.Hide();
            hiredMerchantRedirectGroupBox.Hide();
        }

        private void SaveandAddProfile_Click(object sender, EventArgs e)
        {
            try
            {
                int num;
                if (UsernameTextBox.Text.Equals(""))
                {
                    MessageBox.Show("Please enter a valid username.");
                    return;
                }
                if (ignStealerTextBox.Text.Equals("") || ignStealerTextBox.Text.Count() < 4)
                {
                    MessageBox.Show("Please enter a valid ign in the ign stealer text box. A default has been set for you.");
                    ignStealerTextBox.Text = "IGNS";
                    return;
                }
                if (PasswordTextBox.Text.Equals(""))
                {
                    MessageBox.Show("Please enter a valid password.");
                    return;
                }
                if (PICTextBox.Text.Equals(""))
                {
                    MessageBox.Show("Please enter a valid PIC code.");
                    return;
                }
                if (WorldcomboBox.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a world");
                    return;
                }
                if (ChannelBox.Text.Equals(""))
                {
                    MessageBox.Show("Please enter a valid channel.");
                    return;
                }
                if (fmRoomBox.Text.Equals(""))
                {
                    MessageBox.Show("Please enter a valid FM Room.");
                    return;
                }
                if (ModeComboBox.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a valid mode");
                    return;
                }
                if (CharNumber.Text.Equals(""))
                {
                    MessageBox.Show("Please enter a character number or a valid character ign");
                    return;
                }
                else
                {
                    if (CharNumber.Text.Count() < 4 & !int.TryParse(CharNumber.Text, out num))
                    {
                        MessageBox.Show("Please enter a character number or a valid character ign.");
                        return;
                    }
                }
                if (SpawnTextBox.Text.Equals(""))
                {
                    MessageBox.Show("Please enter valid coords. A default has been set for you.");
                    SpawnTextBox.Text = "0,0";
                    return;
                }
                else
                {
                    if (SpawnTextBox.Text.Split(',').Length < 2)
                    {
                        MessageBox.Show("Please enter valid coords seperated by a comma. A default has been set for you.");
                        SpawnTextBox.Text = "0,0";
                        return;
                    }
                    else
                    {
                        foreach (string str in SpawnTextBox.Text.Split(','))
                        {
                            if (int.TryParse(str, out num))
                            {
                                continue;
                            }
                            else
                            {
                                MessageBox.Show("Please enter valid coords seperated by a comma. A default has been set for you.");
                                SpawnTextBox.Text = "0,0";
                                return;
                            }
                        }
                    }
                }
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
                if (dcTargetTextBox.Text.Equals(""))
                {
                    MessageBox.Show("Please enter a valid DC Target UID. A default value has been set.");
                    dcTargetTextBox.Text = "00 00 00 00";
                    return;
                }
                else
                {
                    if (dcTargetTextBox.Text.Split(' ').Length < 4)
                    {
                        MessageBox.Show("Please enter a valid DC Target UID. A default value has been set.");
                        dcTargetTextBox.Text = "00 00 00 00";
                        return;
                    }
                    else
                    {
                        foreach (string str in dcTargetTextBox.Text.Split(' '))
                        {
                            if (str.Count() != 2)
                            {
                                MessageBox.Show("Please enter a valid DC Target UID. A default value has been set.");
                                dcTargetTextBox.Text = "00 00 00 00";
                                return;
                            }
                        }
                    }
                }

                if (proxyTextBox.Text != "")
                {
                    if (!proxyTextBox.Text.Contains("@"))
                    {
                        string[] parts = proxyTextBox.Text.Split(':', '.');
                        if (parts.Length != 5)
                        {
                            MessageBox.Show("Proxy contains an invalid IP format! \nPlease fix the ip on the line and browse the file again.\nCorrect non-login format: x.x.x.x:xxxx(skip line)\nCorrect login format: user:pass@x.x.x.x:xxxx\n\nSetting value to blank.", "Invalid Format!", MessageBoxButtons.OK);
                            proxyTextBox.Text = "";
                            return;
                        }
                    }
                    else
                    {
                        string[] parts0 = proxyTextBox.Text.Split('@');
                        string[] parts = parts0[1].Split(':', '.');
                        string[] userPass = parts0[0].Split(':');
                        if (userPass.Length != 2 || parts.Length != 5)
                        {
                            MessageBox.Show("Proxy contains an invalid IP format! \nPlease fix the ip on the line and browse the file again.\nCorrect non-login format: x.x.x.x:xxxx(skip line)\nCorrect login format: user:pass@x.x.x.x:xxxx\n\nSetting value to blank.", "Invalid Format!", MessageBoxButtons.OK);
                            proxyTextBox.Text = "";
                            return;
                        }
                    }
                }

                if (mushyRedirectTextBox.Text != "")
                {
                    if (mushyRedirectTextBox.Text.Length < 4 || mushyRedirectTextBox.Text.Length > 13)
                    {
                        MessageBox.Show("Please input a valid IGN target with the length between 4 and 12 inclusive.", "Invalid IGN length!", MessageBoxButtons.OK);
                        mushyRedirectTextBox.Text = "";
                        mushyRedirectTextBox2.Text = "";
                        return;
                    }
                }
                if (mushyRedirectTextBox2.Text != "")
                {
                    if (mushyRedirectTextBox2.Text.Length < 4 || mushyRedirectTextBox2.Text.Length > 13)
                    {
                        MessageBox.Show("Please input a valid IGN target with the length between 4 and 12 inclusive.", "Invalid IGN length!", MessageBoxButtons.OK);
                        mushyRedirectTextBox.Text = "";
                        mushyRedirectTextBox2.Text = "";
                        return;
                    }
                }
                if (File.Exists(Program.settingFile))
                {
                    if (profileNickTextBox.Text.Equals(""))
                        profileNickTextBox.Text = UsernameTextBox.Text;
                    Program.iniFile.WriteValue(profileNickTextBox.Text, "Username", UsernameTextBox.Text);
                    Program.iniFile.WriteValue(profileNickTextBox.Text, "Password", PasswordTextBox.Text);
                    Program.iniFile.WriteValue(profileNickTextBox.Text, "PIC", PICTextBox.Text);
                    Program.iniFile.WriteValue(profileNickTextBox.Text, "World", WorldcomboBox.Text);
                    Program.iniFile.WriteValue(profileNickTextBox.Text, "Channel", ChannelBox.Text);
                    Program.iniFile.WriteValue(profileNickTextBox.Text, "Mode", ModeComboBox.Text);
                    Program.iniFile.WriteValue(profileNickTextBox.Text, "Title", TitleTextBox.Text);
                    Program.iniFile.WriteValue(profileNickTextBox.Text, "Spawn", SpawnTextBox.Text);
                    Program.iniFile.WriteValue(profileNickTextBox.Text, "Shop Type", ShopTypeComboBox.Text);
                    Program.iniFile.WriteValue(profileNickTextBox.Text, "CharNum", CharNumber.Text);
                    Program.iniFile.WriteValue(profileNickTextBox.Text, "DCTarget", dcTargetTextBox.Text);
                    Program.iniFile.WriteValue(profileNickTextBox.Text, "FMRoomNum", fmRoomBox.Text);
                    Program.iniFile.WriteValue(profileNickTextBox.Text, "ignSteal", ignStealerTextBox.Text);
                    Program.iniFile.WriteValue(profileNickTextBox.Text, "KeepRunning", "True");
                    if (coordOverrideCheckBox.Checked)
                        Program.iniFile.WriteValue(profileNickTextBox.Text, "CoordOverride", "True");
                    else
                        Program.iniFile.WriteValue(profileNickTextBox.Text, "CoordOverride", "False");
                    if (fmRoomOverrideCheckBox.Checked)
                        Program.iniFile.WriteValue(profileNickTextBox.Text, "FMRoomOverride", "True");
                    else
                        Program.iniFile.WriteValue(profileNickTextBox.Text, "FMRoomOverride", "False");
                    if (enableLootCheckBox.Checked)
                        Program.iniFile.WriteValue(profileNickTextBox.Text, "LootItems", "True");
                    else
                        Program.iniFile.WriteValue(profileNickTextBox.Text, "LootItems", "False");
                    Program.iniFile.WriteValue(profileNickTextBox.Text, "FMxLow", xLowTextBox.Text);
                    Program.iniFile.WriteValue(profileNickTextBox.Text, "FMxHigh", xHighTextBox.Text);
                    Program.iniFile.WriteValue(profileNickTextBox.Text, "FMyLow", yLowTextBox.Text);
                    Program.iniFile.WriteValue(profileNickTextBox.Text, "FMyHigh", yHighTextBox.Text);
                    Program.iniFile.WriteValue(profileNickTextBox.Text, "AccProxy", proxyTextBox.Text);
                    Program.iniFile.WriteValue(profileNickTextBox.Text, "MushyRedirect", mushyRedirectTextBox.Text);
                    Program.iniFile.WriteValue(profileNickTextBox.Text, "MushyRedirect2", mushyRedirectTextBox2.Text);
                    Program.gui.read = false;
                    refresh = false;
                    refreshAccounts();
                    updateStatusText("Profile for " + profileNickTextBox.Text + " has been successfully updated.");
                    //MessageBox.Show("Profile for " + profileNickTextBox.Text + " has been successfully updated.");
                }
                else
                {
                    File.Create(Program.settingFile).Dispose();
                    SaveandAddProfile_Click(null, null);
                }
            }
            catch
            {
                MessageBox.Show("Error with settings file!");
                File.Create(Program.settingFile).Dispose();
                SaveandAddProfile_Click(null, null);
            }
        }

        private void updateStatusText(string text)
        {
            if (statusLabel.InvokeRequired)
            {
                Program.gui.GUIInvokeMethod(() => statusLabel.Text = "Status: " + text);
            }
            else
            {
                statusLabel.Text = "Status: " + text;
            }
        }

        private void ModeComboBox_DropDown(object sender, EventArgs e)
        {
            ComboBox senderComboBox = (ComboBox)sender;
            int width = senderComboBox.DropDownWidth;
            Graphics g = senderComboBox.CreateGraphics();
            Font font = senderComboBox.Font;
            int vertScrollBarWidth =
                (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
                ? SystemInformation.VerticalScrollBarWidth : 0;

            int newWidth;
            foreach (string s in ((ComboBox)sender).Items)
            {
                newWidth = (int)g.MeasureString(s, font).Width
                    + vertScrollBarWidth;
                if (width < newWidth)
                {
                    width = newWidth;
                }
            }
            senderComboBox.DropDownWidth = width;
        }

        private void accountComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateAccountInfo(accountComboBox.Text, false);
            Program.gui.GUIInvokeMethod(() => Program.gui.comboBox1.Text = accountComboBox.Text);
        }

        public void updateAccountInfo(string user, bool changePIC)
        {
            try
            {
                if (user != "" && user != null)
                {
                    Dictionary<string, string> sectionValues = Program.iniFile.GetSectionValues(user);
                    profileNickTextBox.Text = user;
                    UsernameTextBox.Text = sectionValues["Username"];
                    PasswordTextBox.Text = sectionValues["Password"];
                    PICTextBox.Text = sectionValues["PIC"];
                    WorldcomboBox.Text = sectionValues["World"];
                    ChannelBox.Text = sectionValues["Channel"];
                    CharNumber.Text = sectionValues["CharNum"];
                    SpawnTextBox.Text = sectionValues["Spawn"];
                    TitleTextBox.Text = sectionValues["Title"];
                    ShopTypeComboBox.Text = sectionValues["Shop Type"];
                    ModeComboBox.Text = sectionValues["Mode"];
                    dcTargetTextBox.Text = sectionValues["DCTarget"];
                    fmRoomBox.Text = sectionValues["FMRoomNum"];
                    ignStealerTextBox.Text = sectionValues["ignSteal"];
                    xLowTextBox.Text = sectionValues["FMxLow"];
                    xHighTextBox.Text = sectionValues["FMxHigh"];
                    yLowTextBox.Text = sectionValues["FMyLow"];
                    yHighTextBox.Text = sectionValues["FMyHigh"];
                    proxyTextBox.Text = sectionValues["AccProxy"];
                    mushyRedirectTextBox.Text = sectionValues["MushyRedirect"];
                    mushyRedirectTextBox2.Text = sectionValues["MushyRedirect2"];
                    if (sectionValues["LootItems"].Equals("True"))
                        enableLootCheckBox.Checked = true;
                    else
                        enableLootCheckBox.Checked = false;
                    if (sectionValues["CoordOverride"].Equals("True"))
                        coordOverrideCheckBox.Checked = true;
                    else
                        coordOverrideCheckBox.Checked = false;
                    if (sectionValues["FMRoomOverride"].Equals("True"))
                        fmRoomOverrideCheckBox.Checked = true;
                    else
                        fmRoomOverrideCheckBox.Checked = false;
                    if (changePIC)
                        PICTextBox.Text = "";
                }
                else
                {
                    profileNickTextBox.Text = "";
                    UsernameTextBox.Text = "";
                    PasswordTextBox.Text = "";
                    PICTextBox.Text = "";
                    WorldcomboBox.Text = "";
                    ChannelBox.Text = "1";
                    CharNumber.Text = "1";
                    SpawnTextBox.Text = "0,0";
                    TitleTextBox.Text = "";
                    ShopTypeComboBox.Text = "";
                    ModeComboBox.Text = "";
                    dcTargetTextBox.Text = "00 00 00 00";
                    fmRoomBox.Text = "1";
                    ignStealerTextBox.Text = "IGNS";
                    xLowTextBox.Text = "0";
                    xHighTextBox.Text = "0";
                    yLowTextBox.Text = "0";
                    yHighTextBox.Text = "0";
                    proxyTextBox.Text = "";
                    mushyRedirectTextBox.Text = "";
                    mushyRedirectTextBox2.Text = "";
                    coordOverrideCheckBox.Checked = false;
                    fmRoomOverrideCheckBox.Checked = false;
                    enableLootCheckBox.Checked = false;
                }
            }
            catch
            {
                MessageBox.Show("Error retrieving account info from settings file");
            }
        }

        private void Profiles_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.gui.profile = null;
        }

    }
}
