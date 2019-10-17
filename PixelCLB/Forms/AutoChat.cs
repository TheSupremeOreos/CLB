using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Text.RegularExpressions;

namespace PixelCLB
{
    public partial class AutoChat : Form
    {
        private Client c = null;
        public AutoChat(Client client)
        {
            InitializeComponent();
            c = client;
            loadSettings();
            Text = "Auto Chatter - " + c.myCharacter.ign;
        }

        public override string ToString()
        {
            return c.toProfile();
        }

        private void loadSettings()
        {
            int textCounter = 0;
            textDataGrid.Rows.Clear();
            while (true & c.clientMode != ClientMode.DISCONNECTED) //Add to DataGrid
            {
                Dictionary<string, string> sectionValues = Program.iniFile.GetSectionValues(c.accountProfile);
                if (sectionValues.ContainsKey("Text" + textCounter.ToString()))
                {
                    string entry = sectionValues["Text" + textCounter.ToString()];
                    char[] delimiters = new char[] { '|', '/' };
                    string[] strArrays = entry.Split(delimiters);
                    textDataGrid.Rows.Add(strArrays[0], strArrays[2]);
                }
                else
                {
                    break;
                }
                textCounter++;
            }
            typeListBox.Items.Clear();
            while (true & c.clientMode != ClientMode.DISCONNECTED) //Add to list Box
            {
                Dictionary<string, string> sectionValues = Program.iniFile.GetSectionValues(c.accountProfile);
                if (sectionValues.ContainsKey("ChatMode"))
                {
                    string[] strArray = sectionValues["ChatMode"].Split(',');
                    foreach (var str in strArray)
                    {
                        if (str != "")
                            typeListBox.Items.Add(str);
                    }
                    break;
                }
                else
                {
                    break;
                }

            }

        }

        private void addText_Click(object sender, EventArgs e)
        {
            int num;
            if (textBox.Text == string.Empty)
            {
                MessageBox.Show("Please enter a valid text.");
                return;
            }
            if (!int.TryParse(delayTextBox.Text, out num))
            {
                MessageBox.Show("Please enter a valid delay.");
            }
            else
            {
                if (int.Parse(delayTextBox.Text) >= 1200)
                {
                    string[] text = new string[] { textBox.Text, delayTextBox.Text };
                    textDataGrid.Rows.Add(text);
                    textBox.Text = "";
                    return;
                }
                else
                {
                    MessageBox.Show("Please enter a delay 1200ms or higher");
                }
            }
        }

        private void deleteText_Click(object sender, EventArgs e)
        {
            try
            {
                if (textDataGrid.SelectedCells.Count > 0)
                {
                    int rowIndex = textDataGrid.SelectedCells[0].RowIndex;
                    textDataGrid.Rows.RemoveAt(rowIndex);
                }
            }
            catch { }
        }

        private void typeAddButton_Click(object sender, EventArgs e)
        {
            if (typeComboBox.SelectedIndex > -1)
            {
                foreach (var item in typeListBox.Items)
                {
                    if (item.ToString().Equals(typeComboBox.SelectedItem.ToString()))
                    {
                        typeComboBox.SelectedIndex = -1;
                        return;
                    }
                }
                typeListBox.Items.Add(typeComboBox.SelectedItem.ToString());
                typeComboBox.SelectedIndex = -1;
                return;
            }
            else
            {
                return;
            }
        }

        private void typeRemoveButton_Click(object sender, EventArgs e)
        {
            if (typeListBox.SelectedIndex != -1)
            {
                typeListBox.Items.Remove(typeListBox.SelectedItem);
                typeListBox.SelectedIndex = -1;
                return;
            }
            else
            {
                return;
            }
        }

        private void autoChatStartButton_Click(object sender, EventArgs e)
        {
            try
            {
               Thread t = new Thread(delegate()
                {
                    try
                    {
                        c.updateAccountStatus("Auto chat initiating");
                        c.updateLog("Auto chat initiating");
                        int textCounter = 0;
                        foreach (DataGridViewRow item in this.textDataGrid.Rows) //Adds/updates texts
                        {
                            string format = item.Cells[0].Value.ToString() + "|/" + item.Cells[1].Value.ToString();
                            Program.iniFile.WriteValue(c.accountProfile, "Text" + textCounter.ToString(), format);
                            textCounter++;
                        }
                        while (true & c.clientMode != ClientMode.DISCONNECTED) //Removes additional texts
                        {
                            Dictionary<string, string> sectionValues = Program.iniFile.GetSectionValues(c.accountProfile);
                            if (sectionValues.ContainsKey("Text" + textCounter.ToString()))
                            {
                                Program.iniFile.DeleteKey(c.accountProfile, "Text" + textCounter.ToString());
                            }
                            else
                            {
                                break;
                            }
                            textCounter++;
                        }
                        Thread.Sleep(100);
                        textCounter = 0;
                        c.autoChatText.Clear();
                        while (true & c.clientMode != ClientMode.DISCONNECTED) //Update / add to client profile
                        {
                            Dictionary<string, string> sectionValues = Program.iniFile.GetSectionValues(c.accountProfile);
                            if (sectionValues.ContainsKey("Text" + textCounter.ToString()))
                            {
                                c.autoChatText.Add(sectionValues["Text" + textCounter.ToString()]);
                            }
                            else
                            {
                                break;
                            }
                            textCounter++;
                        }
                        Thread.Sleep(100);
                        string type = "";
                        foreach (var item in typeListBox.Items)
                        {
                            if (!item.ToString().Equals(""))
                                type = string.Concat(type, item.ToString(), ",");
                        }

                        Thread.Sleep(100);
                        c.autoChatMode = type;
                        Program.iniFile.WriteValue(c.accountProfile, "ChatMode", type);
                        c.mode = Program.getModeID("AutoChat");
                        c.modeBak = c.mode;
                        c.cashShopManagement(false, false, 0, 0);
                        Thread.Sleep(500);
                        if (Program.gui.autoChatWindows.Contains(this))
                            Program.gui.autoChatWindows.Remove(this);
                        c.onServerConnected(false);
                        Program.gui.GUIInvokeMethod(() => base.Close());
                    }
                    catch { }
                });
                c.workerThreads.Add(t);
                t.Start();
            }
            catch { }
        }

        private void AutoChat_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (Program.gui.autoChatWindows.Contains(this))
                    Program.gui.autoChatWindows.Remove(this);
            }
            catch { }
        }
    }
}
