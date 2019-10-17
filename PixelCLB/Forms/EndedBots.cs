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
using System.Text.RegularExpressions;
using System.Windows.Forms;
using PixelCLB;
using PixelCLB.CLBTools;
using PixelCLB.Tools;
using PixelCLB.Net;
using PixelCLB.Net.Packets;
using PixelCLB.Crypto;
using PixelCLB.Net.Events;
using PixelCLB.PacketCreation;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Threading;
using System.IO.Pipes;
using System.Management;
using System.Net.NetworkInformation;

namespace PixelCLB
{
    public partial class EndedBots : Form
    {
        private Client client;
        public EndedBots()
        {
            InitializeComponent();
            loadBotList();
            accountProfiles.SelectedIndex = -1;
        }

        private void accountProfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            client = (Client)accountProfiles.SelectedItem;
            updateLogs();
        }

        public void loadBotList()
        {
            try
            {
                accountProfiles.Items.Clear();
                foreach (Client c in Program.endedClients)
                {
                    accountProfiles.Items.Add(c);
                }
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Program.endedClients.Remove((Client)accountProfiles.SelectedItem);
                accountLog.Items.Clear();
                loadBotList();
                accountProfiles.SelectedIndex = -1;
            }
            catch { }
        }


        private void updateLogs()
        {
            try
            {
                accountLog.Invoke((MethodInvoker)delegate
                {
                    if (Text != null)
                    {
                        accountLog.Items.Clear();
                        foreach (string s in client.logBox)
                        {
                            if (!string.IsNullOrEmpty(s))
                                accountLog.Items.Add(s);
                        }
                    }
                    accountLog.SelectedIndex = -1;
                });
            }
            catch { }
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (accountLog.Items.Count > 0)
            {
                StringBuilder SB = new StringBuilder();
                foreach (string itemValue in accountLog.SelectedItems)
                {
                    SB.AppendLine(itemValue);
                }
                string result = SB.ToString().TrimEnd('\n');
                Clipboard.SetText(result);
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            accountLog.Items.Remove(accountLog.SelectedItem);
        }

        private void accountLog_MouseDown(object sender, MouseEventArgs e)
        {
            accountLog.SelectedIndex = accountLog.IndexFromPoint(e.X, e.Y);
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenuStrip2.Show(accountLog, e.X, e.Y);
            }
        }

        private void disposeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (accountProfiles.SelectedIndex > -1)
                {
                    Program.endedClients.Remove((Client)accountProfiles.SelectedItem);
                    accountLog.Items.Clear();
                    loadBotList();
                    accountProfiles.SelectedIndex = -1;
                }
            }
            catch { }
        }

        private void accountProfiles_MouseDown(object sender, MouseEventArgs e)
        {
            accountProfiles.SelectedIndex = accountProfiles.IndexFromPoint(e.X, e.Y);
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenuStrip1.Show(accountProfiles, e.X, e.Y);
            }
        }

        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Dispose();
        }

        private void cancelToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            contextMenuStrip2.Dispose();
        }

    }
}
