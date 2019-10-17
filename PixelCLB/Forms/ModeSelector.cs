 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using PixelCLB.Net;
using PixelCLB.PacketCreation;

namespace PixelCLB
{
    public partial class ModeSelector : Form
    {
        private Client c;
        public ModeSelector(Client client)
        {
            c = client;
            Program.gui.GUIInvokeMethod(() => Text = c.myCharacter.ign.Replace("\0", "") + " - Mode Selector");
            InitializeComponent();
            foreach (string str in Program.modes)
            {
                ModeComboBox.Items.Add(str);
            }
        }
        public override string ToString()
        {
            return c.toProfile();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (ModeComboBox.SelectedItem.ToString().Equals("DC Mode"))
            {
                if (!uidTextBox.Text.Equals(""))
                {
                    if (uidTextBox.Text.Split(' ').Length >= 4)
                    {
                        foreach (string str in uidTextBox.Text.Split(' '))
                        {
                            if (str.Count() != 2)
                            {
                                MessageBox.Show("Please enter a valid DC Target UID");
                                return;
                            }
                        }
                        Program.iniFile.WriteValue(c.accountProfile, "DCTarget", uidTextBox.Text);
                        c.dcTarget = uidTextBox.Text;
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid DC Target UID");
                        return;
                    }
                }
            }
            if (ModeComboBox.SelectedItem.ToString().Equals("FM Spot Bot (Permit)") || ModeComboBox.SelectedItem.ToString().Equals("FM Spot Bot (Non-Permit)"))
            {
                if (!coordTextBox.Text.Equals(""))
                {
                    if (coordTextBox.Text.Split(',').Length >= 2)
                    {
                        Program.iniFile.WriteValue(c.accountProfile, "Spawn", coordTextBox.Text);
                        c.coords = coordTextBox.Text;
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid coords");
                        return;
                    }
                }
            }
            c.mode = Program.getModeID(ModeComboBox.SelectedItem.ToString());
            c.modeBak = c.mode;
            c.regLogin = false;
            Program.iniFile.WriteValue(c.accountProfile, "Mode", ModeComboBox.SelectedItem.ToString());
            c.thread = new Thread(() => c.onServerConnected(false));
            c.workerThreads.Add(c.thread);
            c.thread.Start();
            try
            {
                base.Close();
            }
            catch { }
        }

        private void ModeSelector_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.gui.modeWindows.Remove(this);
        }

        private void ModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ModeComboBox.SelectedItem.ToString().Equals("DC Mode"))
                uidTextBox.ReadOnly = false;
            else
                uidTextBox.ReadOnly = true;
            if (ModeComboBox.SelectedItem.ToString().Equals("FM Spot Bot (Permit)") || ModeComboBox.SelectedItem.ToString().Equals("FM Spot Bot (Non-Permit)"))
                coordTextBox.ReadOnly = false;
            else
                coordTextBox.ReadOnly = true;
        }
    }
}
