using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace PixelCLB
{
    public partial class AutoBuff : Form
    {
        private Client c = null;
        public AutoBuff(Client client)
        {
            c = client;
            InitializeComponent();
            LoadSettings();
            Text = "Auto Buffer - " + c.myCharacter.ign;
        }
        public override string ToString()
        {
            return c.toProfile();
        }
        private void LoadSettings()
        {
            Dictionary<string, string> sectionValues = Program.iniFile.GetSectionValues(c.accountProfile);
            GUIInvokeMethod(() =>
            {
                skillListBox.BeginUpdate();
                foreach (KeyValuePair<int, string> skill in Database.skills)
                {
                    string format = skill.Value + " , " + skill.Key.ToString();
                    skillListBox.Items.Add(format);
                }
                skillListBox.EndUpdate();
            });

            int buffCounter = 0;
            autoBuffDataGrid.Rows.Clear();
            while (true & c.clientMode != ClientMode.DISCONNECTED) //Add to DataGrid
            {
                if (sectionValues.ContainsKey("Buff" + buffCounter.ToString()))
                {
                    string entry = sectionValues["Buff" + buffCounter.ToString()];
                    char[] delimiters = new char[] { ',' };
                    string[] strArrays = entry.Split(delimiters);
                    autoBuffDataGrid.Rows.Add(strArrays[0], strArrays[1], strArrays[2]);
                }
                else
                {
                    break;
                }
                buffCounter++;
            }
            useComboBox.Items.Clear();
            List<int> mpPots = Database.getMpConsumePotList(c.myCharacter.Inventorys[InventoryType.USE].getItemList());
            foreach (int useItem in mpPots)
            {
                useComboBox.Items.Add(Database.getItemName(useItem));
            }
            if (sectionValues.ContainsKey("useMPPot"))
            {
                string entry = sectionValues["useMPPot"];
                if (useComboBox.Items.Contains(Database.getItemName(int.Parse(entry))))
                {
                    useComboBox.SelectedItem = Database.getItemName(int.Parse(entry));
                }
                else
                    useComboBox.SelectedIndex = -1;
            }
            else
            {
                useComboBox.SelectedIndex = -1;
            }
            if (sectionValues.ContainsKey("PotWhenMPUnder"))
            {
                string entry = sectionValues["PotWhenMPUnder"];
                mpBelowTextBox.Text = entry;
            }
            else
            {
                mpBelowTextBox.Text = "500";
            }
        }

        private delegate void GUIInvokeMethodDelegate(Action @delegate);
        public void GUIInvokeMethod(Action @delegate)
        {
            bool invokeRequired = !base.InvokeRequired;
            if (!invokeRequired)
            {
                try
                {
                    object[] objArray = new object[] { @delegate };
                    base.Invoke(new AutoBuff.GUIInvokeMethodDelegate(GUIInvokeMethod), objArray);
                    return;
                }
                catch
                {
                }
            }
            @delegate();
        }


        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                GUIInvokeMethod(() =>
                {
                    skillListBox.BeginUpdate();
                    skillListBox.Items.Clear();
                    foreach (KeyValuePair<int, string> skill in Database.skills)
                    {
                        if (skill.Value.ToLower().Contains(textBox3.Text.ToLower()))
                        {
                            string format = skill.Value + " , " + skill.Key.ToString();
                            skillListBox.Items.Add(format);
                        }
                    }
                    skillListBox.EndUpdate();
                });
                textBox3.Text = "";
            }
        }

        private void skillListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (skillListBox.SelectedIndex != -1)
            {
                string skillID = skillListBox.SelectedItem.ToString().Split(',')[1];
                skillIDTextBox.Text = skillID;
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            int num;
            if (skillIDTextBox.Text == string.Empty)
            {
                MessageBox.Show("Please enter a valid text.");
                return;
            }
            if (!int.TryParse(delayTextBox.Text, out num))
            {
                MessageBox.Show("Please enter a valid delay.");
                return;
            }
            if (!int.TryParse(skillIDTextBox.Text, out num))
            {
                MessageBox.Show("Please enter a skill id.");
                return;
            }
            if (levelComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a skill level.");
                return;
            }
            if (Database.skills.ContainsKey(int.Parse(skillIDTextBox.Text)))
            {
                string[] text = new string[] { skillIDTextBox.Text, levelComboBox.SelectedItem.ToString(), delayTextBox.Text };
                autoBuffDataGrid.Rows.Add(text);
                skillIDTextBox.Text = "";
                levelComboBox.SelectedIndex = -1;
                delayTextBox.Text = "";
                return;
            }
            else
            {
                MessageBox.Show("This is not a valid skill ID");
                return;
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            try
            {
                int num;
                int buffCounter = 0;
                foreach (DataGridViewRow item in autoBuffDataGrid.Rows) //Adds/updates texts
                {
                    string format = item.Cells[0].Value.ToString() + "," + item.Cells[1].Value.ToString() + "," + item.Cells[2].Value.ToString();
                    Program.iniFile.WriteValue(c.accountProfile, "Buff" + buffCounter.ToString(), format);
                    buffCounter++;
                }
                while (true & c.clientMode != ClientMode.DISCONNECTED) //Removes additional texts
                {
                    Dictionary<string, string> sectionValues = Program.iniFile.GetSectionValues(c.accountProfile);
                    if (sectionValues.ContainsKey("Buff" + buffCounter.ToString()))
                    {
                        Program.iniFile.DeleteKey(c.accountProfile, "Buff" + buffCounter.ToString());
                    }
                    else
                    {
                        break;
                    }
                    buffCounter++;
                }
                buffCounter = 0;
                c.autoBuffList.Clear();
                while (true & c.clientMode != ClientMode.DISCONNECTED) //Update / add to client profile
                {
                    Dictionary<string, string> sectionValues = Program.iniFile.GetSectionValues(c.accountProfile);
                    if (sectionValues.ContainsKey("Buff" + buffCounter.ToString()))
                    {
                        c.autoBuffList.Add(sectionValues["Buff" + buffCounter.ToString()]);
                    }
                    else
                    {
                        break;
                    }
                    buffCounter++;
                }
                if (!int.TryParse(mpBelowTextBox.Text, out num))
                {
                    MessageBox.Show("Please enter a valid MP value.");
                    return;
                }
                else
                {
                    c.MPUnderValue = int.Parse(mpBelowTextBox.Text);
                    Program.iniFile.WriteValue(c.accountProfile, "PotWhenMPUnder", Database.getItemID(useComboBox.SelectedItem.ToString()));
                }
                if (useComboBox.SelectedIndex != -1)
                {
                    c.MPPotID = Database.getItemID(useComboBox.SelectedItem.ToString());
                    Program.iniFile.WriteValue(c.accountProfile, "useMPPot", Database.getItemID(useComboBox.SelectedItem.ToString()));
                }
                else
                {
                    MessageBox.Show("Please select a valid use pot");
                    return;
                }
                return;
            }
            catch
            {
                return;
            }

        }

        private void startButton_Click(object sender, EventArgs e)
        {
            try
            {
                updateButton_Click(null, null);
                int num;
                if (!int.TryParse(mpBelowTextBox.Text, out num))
                {
                    return;
                }
                else
                {
                    c.MPUnderValue = int.Parse(mpBelowTextBox.Text);
                    Program.iniFile.WriteValue(c.accountProfile, "PotWhenMPUnder", mpBelowTextBox.Text);
                }
                if (useComboBox.SelectedIndex != -1)
                {
                    c.MPPotID = Database.getItemID(useComboBox.SelectedItem.ToString());
                    Program.iniFile.WriteValue(c.accountProfile, "useMPPot", Database.getItemID(useComboBox.SelectedItem.ToString()));
                }
                else
                {
                    return;
                }
                c.thread = new Thread(delegate()
                {
                    c.mode = Program.getModeID("Auto Buff");
                    c.modeBak = c.mode;
                    if (Program.gui.autoBuffWindows.Contains(this))
                    {
                        Program.gui.autoBuffWindows.Remove(this);
                    }
                    c.onServerConnected(false);
                });
                c.workerThreads.Add(c.thread);
                c.thread.Start();
                base.Close();
            }
            catch { }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (autoBuffDataGrid.SelectedCells.Count > 0)
                {
                    int rowIndex = autoBuffDataGrid.SelectedCells[0].RowIndex;
                    autoBuffDataGrid.Rows.RemoveAt(rowIndex);
                }
            }
            catch { }
        }

        private void AutoBuff_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (Program.gui.autoBuffWindows.Contains(this))
                {
                    Program.gui.autoBuffWindows.Remove(this);
                }
            }
            catch { }
        }
    }
}

/* HS Packets
 * BD 00 [TIMESTAMP 4BYTES][SKILLID][LEVEL BYTE][X][Y][3F 00][00 00]
BD 00 EE 8A BF 01 6A 88 1E 00 04 00 00 00
BD 00 4C 87 BF 01 6A 88 1E 00 04 00 00 00


BD 00 C8 3D E4 01 5B 43 23 00 14 3C 03 6E FF 20 00 00 00
BD 00 6E 79 E4 01 5B 43 23 00 14 3C 03 6E FF 20 00 00 00
BD 00 7C 1C E8 01 5B 43 23 00 14 79 01 60 FE 20 00 00 00

BD 00 6E CC EB 01 5B 43 23 00 14 31 01 60 FE 30 00 00 00
BD 00 E2 68 EA 01 5B 43 23 00 14 4C 01 60 FE 30 00 00 00
BD 00 [TIMESTAMP] [skillid][X] [Y] FE 20 00 00 00
BD 00 94 1E EC 01 5B 43 23 00 14 31 01 32 FF 30 00 00 00


BD 00 32 80 EC 01 SKILLID (14?) X Y 20 00 00 00

BD 00 2C 32 E6 01 5B 43 23 00 14 79 01 60 FE 30 00 00 00
BD 00 5A 8B E6 01 5B 43 23 00 14 79 01 60 FE 30 00 00 00
BD 00 B4 55 EF 01 5B 43 23 00 14 94 01 60 FE 38 00 00 00
BD 00 1C BC F3 01 5B 43 23 00 14 80 02 60 FE 38 00 00 00
BD 00 0A 73 F4 01 5B 43 23 00 14 18 02 60 FE 3C 00 00 00 //4
8A F0 F6 01 5B 43 23 00 14 DE 01 60 FE 3E 00 00 00 //5
2A 33 F6 01 5B 43 23 00 14 82 01 60 FE 3F 00 00 00 //6
C8 85 F6 01 5B 43 23 00 14 CE 01 60 FE 3F 00 00 00 //6
*/