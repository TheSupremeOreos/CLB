using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace PixelCLB
{
    public partial class CrashOptions : Form
    {
        public Client client = null;
        public System.DateTime start = System.DateTime.Now;
        private bool crashable = true;

        public CrashOptions(Client c)
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
            this.Text = client.toProfile() + " - AB Crash Options";
            Thread timerThread = new Thread(delegate()
            {
                while (true)
                {
                    System.DateTime now = System.DateTime.Now;
                    double calc = Math.Floor((now - start).TotalSeconds);
                    double timeLeft = 65 - calc;
                    if (calc > 65 || crashable)
                    {
                        crashable = true;
                        Program.gui.GUIInvokeMethod(() => TimerLabel.ForeColor = System.Drawing.Color.Green);
                        Program.gui.GUIInvokeMethod(() => TimerLabel.Text = "Crashable!");
                    }
                    else
                    {
                        Program.gui.GUIInvokeMethod(() => TimerLabel.ForeColor = System.Drawing.Color.Red);
                        Program.gui.GUIInvokeMethod(() => TimerLabel.Text = "Time Left Before Next Crash: " + timeLeft.ToString());
                    }
                    Thread.Sleep(1000);
                }
            });
            timerThread.Start();
        }

        private void CrashOptions_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (Program.gui.crashWindows.Contains(this))
                    Program.gui.crashWindows.Remove(this);
            }
            catch { }
        }

        private void crashButton_Click(object sender, EventArgs e)
        {
            try
            {
                crashable = false;
                start = System.DateTime.Now;
                Thread ccThread = new Thread(delegate()
                {
                    byte level = (byte)int.Parse(levelBox.Text);
                    client.abCrash(65121003, level);
                });
                client.workerThreads.Add(ccThread);
                ccThread.Start();
            }
            catch { }
        }
    }
}
