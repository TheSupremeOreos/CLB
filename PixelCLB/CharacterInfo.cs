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
    public partial class CharacterInfo : Form
    {
        private Client c;
        private string[] text;
        private bool spammingPacket = false;
        

        public CharacterInfo(Client client)
        {
            c = client;
            c.chatLogs = true;
            c.charInfoWindow = this;
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            Text = string.Concat(c.myCharacter.ign.Replace("\0", ""), " - Extra Bot Functions");
        }

        public override string ToString()
        {
            return c.toProfile();
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
                    base.Invoke(new CharacterInfo.GUIInvokeMethodDelegate(GUIInvokeMethod), objArray);
                    return;
                }
                catch
                {
                }
            }
            @delegate();
        }

        public void refreshChatBox()
        {
            try
            {
                GUIInvokeMethod(() =>
                    {
                        chatCollectionListBox.BeginUpdate();
                        chatCollectionListBox.Items.Clear();
                        foreach (string str in c.chatCollection)
                        {
                            chatCollectionListBox.Items.Add(str);
                            chatCollectionListBox.SelectedIndex = chatCollectionListBox.Items.Count - 1;
                        }
                        chatCollectionListBox.EndUpdate();
                    });
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void CharacterInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.gui.charWindows.Remove(this);
            c.charInfoWindow = null;
            c.chatLogs = false;
        }

        public void disconnectCheck()
        {
            try
            {
                if (spamSelectedButton.Text.Equals("Stop Spamming"))
                {
                    spamSelectedButton_Click(null, null);
                    spamSelectedButton_Click(null, null);
                }
                if (!spamButton.Text.Equals("Spam All"))
                {
                    spamButton_Click(null, null);
                    spamButton_Click(null, null);
                }
            }
            catch { }
        }


        private void removeButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (packetCenterGrid.SelectedCells.Count > 0)
                {
                    int rowIndex = packetCenterGrid.SelectedCells[0].RowIndex;
                    packetCenterGrid.Rows.RemoveAt(rowIndex);
                }

            }
            catch { }
        }

        private void spamSelectedButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (spamSelectedButton.Text.Equals("Spam Selected"))
                {
                    if (packetCenterGrid.SelectedCells.Count <= 0)
                    {
                        MessageBox.Show("Please add a valid packet, highlight it and try this feature again.");
                        return;
                    }
                    else
                    {
                        string str = packetCenterGrid.Rows[packetCenterGrid.SelectedCells[0].RowIndex].Cells[0].Value.ToString();
                        int num = int.Parse(packetCenterGrid.Rows[packetCenterGrid.SelectedCells[0].RowIndex].Cells[1].Value.ToString());
                        spammingPacket = true;
                        spamSelectedButton.Text = "Stop Spamming";
                        Thread packetSpammer = new Thread(delegate()
                        {
                            while (spammingPacket & c.clientMode != ClientMode.DISCONNECTED)
                            {
                                c.ses.SendPacket(PacketHandler.Custom(PixelCLB.replacePacketVars(str)).ToArray());
                                Thread.Sleep(num);
                                c.updateAccountStatus("Spamming packets");
                            }
                            c.updateAccountStatus("Idle");
                        });
                        c.workerThreads.Add(packetSpammer);
                        packetSpammer.Start();
                        return;
                    }
                }
                else
                {
                    if (spamSelectedButton.Text.Equals("Stop Spamming"))
                    {
                        spammingPacket = false;
                        spamSelectedButton.Text = "Spam Selected";
                        foreach (Thread t in c.workerThreads)
                        {
                            try
                            {
                                t.Abort();
                            }
                            catch { }
                        }
                        c.updateAccountStatus("Idle");
                    }
                    return;
                }
            }
            catch { }
        }

        private void sendAllOnceButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!sendAllOnceButton.Text.Equals("Sending..."))
                {
                    if (packetCenterGrid.SelectedCells.Count <= 0)
                    {
                        MessageBox.Show("Please add valid packet(s).");
                        return;
                    }
                    int rowCount = packetCenterGrid.Rows.Count;
                    int row = 0;
                    sendAllOnceButton.Text = "Sending...";
                    Thread packetSpammer = new Thread(delegate()
                    {
                        while (rowCount != 0 & c.clientMode != ClientMode.DISCONNECTED)
                        {
                            string str = packetCenterGrid.Rows[row].Cells[0].Value.ToString();
                            int num = int.Parse(packetCenterGrid.Rows[row].Cells[1].Value.ToString());
                            c.ses.SendPacket(PacketHandler.Custom(PixelCLB.replacePacketVars(str)).ToArray());
                            Thread.Sleep(num);
                            row++;
                            rowCount--;
                        }
                    });
                    c.workerThreads.Add(packetSpammer);
                    packetSpammer.Start();
                    packetSpammer.Join();
                    sendAllOnceButton.Text = "Send All Once";
                }
                else
                {
                    foreach (Thread t in c.workerThreads)
                    {
                        try
                        {
                            t.Abort();
                        }
                        catch { }
                    }
                    return;
                }
            }
            catch { }
        }

        private void spamButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (spamButton.Text.Equals("Spam All"))
                {
                    if (packetCenterGrid.SelectedCells.Count <= 0)
                    {
                        MessageBox.Show("Please add valid packet(s).");
                        return;
                    }
                    else
                    {
                        int rowCount = packetCenterGrid.Rows.Count;
                        int currentRow = 0;
                        spammingPacket = true;
                        spamButton.Text = "Stop Spamming";
                        Thread packetSpammer = new Thread(delegate()
                        {
                            while (spammingPacket & c.clientMode != ClientMode.DISCONNECTED)
                            {
                                c.updateAccountStatus("Spamming packets");
                                string str = packetCenterGrid.Rows[currentRow].Cells[0].Value.ToString();
                                int num = int.Parse(packetCenterGrid.Rows[currentRow].Cells[1].Value.ToString());
                                c.ses.SendPacket(PacketHandler.Custom(PixelCLB.replacePacketVars(str)).ToArray());
                                Thread.Sleep(num);
                                currentRow++;
                                if (rowCount == currentRow)
                                    currentRow = 0;
                            }
                            c.updateAccountStatus("Idle");
                        });
                        c.workerThreads.Add(packetSpammer);
                        packetSpammer.Start();
                        return;
                    }
                }
                else
                {
                    spammingPacket = false;
                    spamButton.Text = "Spam All";
                    c.updateAccountStatus("Idle");
                    foreach (Thread t in c.workerThreads)
                    {
                        try
                        {
                            t.Abort();
                        }
                        catch { }
                    }
                    return;
                }
            }
            catch { }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            int num;
            try
            {
                Utilities.StringToByteArray(packetTextBox.Text.Replace(" ", "").Replace("*", "0"));
            }
            catch
            {
                MessageBox.Show("Please enter a correct packet. Ex: 00 00 00 00 00 00");
                return;
            }
            if (!int.TryParse(delayTextBox.Text, out num))
            {
                MessageBox.Show("Please enter a valid delay.");
            }
            else
            {
                string[] text = new string[] { packetTextBox.Text, delayTextBox.Text };
                packetCenterGrid.Rows.Add(text);
                packetTextBox.Text = "";
                delayTextBox.Text = "";
                return;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "Whisper")
                ignTextBox.ReadOnly = false;
            else
                ignTextBox.ReadOnly = true;
        }

        private void chatTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) //Press Enter
            {
                string mode = comboBox1.SelectedItem.ToString();
                if (chatTextBox.Text != "")
                {
                    string[] str = chatTextBox.Text.Split(' ');
                    string header = str[0].ToLower();
                    if (str.Count() >= 2)
                    {
                        if (header.Equals("/find"))
                        {
                            if (str[1].Count() >= 4 & str[1].Count() <= 13)
                            {
                                c.chat(6, chatTextBox.Text, "");
                            }
                            else
                            {
                                c.refreshChatBox("*Wrong /guildinvite format!*");
                            }
                            chatTextBox.Text = "";
                            return;
                        }
                        else if (header.Equals("/guildinvite"))
                        {
                            if (str[1].Count() >= 4 & str[1].Count() <= 13)
                            {
                                c.chat(7, chatTextBox.Text, "");
                            }
                            else
                            {
                                c.refreshChatBox("*Wrong /guildinvite format!*");
                            }
                            chatTextBox.Text = "";
                            return;
                        }
                    }
                    if (mode.Equals("All"))
                        c.chat(1, chatTextBox.Text, "");
                    if (mode.Equals("Buddy"))
                        c.chat(3, chatTextBox.Text, "");
                    if (mode.Equals("Guild"))
                        c.chat(4, chatTextBox.Text, "");
                    if (mode.Equals("Whisper"))
                    {
                        if (ignTextBox.Text.Count() >= 4 & ignTextBox.Text.Count() <= 13)
                        {
                            c.chat(2, chatTextBox.Text, ignTextBox.Text);
                        }
                        else
                        {
                            c.refreshChatBox("Please enter a valid ign");
                        }
                    }
                    chatTextBox.Text = "";
                }
                else
                {
                    c.refreshChatBox("Please enter valid text");
                }
            }
        }

        private void chatCollectionListBox_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (chatCollectionListBox.SelectedIndex != -1)
                {
                    chatMenuStrip.Items.Clear();
                    text = chatCollectionListBox.SelectedItem.ToString().Split(' ');
                    if (text[0].Contains("Whisper"))
                        chatMenuStrip.Items.Add("Whisper " + text[1], null, whisperClicked);
                    if (e.Button == System.Windows.Forms.MouseButtons.Right)
                    {
                        chatMenuStrip.Show(chatCollectionListBox, e.X, e.Y);
                    }
                }
            }
            catch { }
        }

        private void whisperClicked(object sender, EventArgs e)
        {
            comboBox1.SelectedItem = "Whisper";
            ignTextBox.ReadOnly = false;
            ignTextBox.Text = text[1];
        }

        
    }
}
