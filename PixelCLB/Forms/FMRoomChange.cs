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
    public partial class FMRoomChange : Form
    {
        private Client client = null;
        public FMRoomChange(Client c)
        {
            client = c;
            InitializeComponent();
            load();
        }

        public override string ToString()
        {
            return client.toProfile();
        }

        private void load()
        {
            this.Text = client.toProfile() + " - FM Location Changer";
            channelBox.Text = client.channel.ToString();
            roomBox.Text = (client.myCharacter.mapID - 910000000).ToString();
            label3.Text = "Status: Idle";
        }

        private void moveButton_Click(object sender, EventArgs e)
        {
            try
            {
                Thread ccThread = new Thread(delegate()
                {
                    try
                    {
                        byte channel = (byte)int.Parse(channelBox.Text);
                        Program.gui.GUIInvokeMethod(() => label3.Text = "Status: Moving to CH" + channelBox.Text + "FM" + roomBox.Text + ". This window will close when character has moved");
                        client.changeFreeMarketRoom(roomBox.Text, channel);
                        Thread.Sleep(1000);
                        Program.gui.GUIInvokeMethod(() => base.Close());
                    }
                    catch { }
                });
                client.workerThreads.Add(ccThread);
                ccThread.Start();
            }
            catch { }
        }

        private void FMRoomChange_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (Program.gui.FMRoomChangeWindows.Contains(this))
                    Program.gui.FMRoomChangeWindows.Remove(this);
            }
            catch { }
        }
    }
}
