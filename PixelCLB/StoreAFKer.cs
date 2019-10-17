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
    public partial class StoreAFKer : Form
    {
        public Client client;
        public StoreAFKer(Client c)
        {
            client = c;
            InitializeComponent();
            loadSettings();
        }

        private void loadSettings()
        {
            Dictionary<string, string> sectionValues = Program.iniFile.GetSectionValues(client.accountProfile);
            if (sectionValues.ContainsKey("afkPermit"))
            {
                string entry = sectionValues["afkPermit"];
                if (entry.ToLower().Equals("false"))
                    storeRadioButton.Checked = true;
                else
                    permitRadioButton.Checked = true;
            }
            else
                storeRadioButton.Checked = true;
            if (sectionValues.ContainsKey("afkIGN"))
            {
                ignTextBox.Text = sectionValues["afkIGN"];
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (storeRadioButton.Checked)
                    client.afkPermit = false;
                else
                    client.afkPermit = true;

                if (ignTextBox.Text.Length > 3 & ignTextBox.Text.Length < 13)
                    client.storeAFKIGN = ignTextBox.Text;
                else
                {
                    Program.gui.GUIInvokeMethod(() => ignTextBox.Text = "");
                    MessageBox.Show("Please enter a valid ign");
                }
                saveSettings();
                Thread thread = new Thread(delegate()
                {
                    client.mode = Program.getModeID("Store AFK");
                    client.modeBak = client.mode;
                    if (Program.gui.storeAFKWindows.Contains(this))
                    {
                        Program.gui.storeAFKWindows.Remove(this);
                    }
                    client.onServerConnected(false);
                });
                client.workerThreads.Add(thread);
                thread.Start();
                base.Close();
            }
            catch { }
        }
        private void saveSettings()
        {
            if(storeRadioButton.Checked)
                Program.iniFile.WriteValue(client.accountProfile, "afkPermit", "false");
            else
                Program.iniFile.WriteValue(client.accountProfile, "afkPermit", "true");
            Program.iniFile.WriteValue(client.accountProfile, "afkIGN", ignTextBox.Text);
        }

        private void StoreAFKer_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Program.gui.storeAFKWindows.Contains(this))
                Program.gui.storeAFKWindows.Remove(this);
        }
    }
}
