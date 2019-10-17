using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using PixelCLB.Net;
using PixelCLB.Net.Events;
using PixelCLB.Net.Packets;
using PixelCLB;
using PixelCLB.PacketCreation;
using PixelCLB.Crypto;
using PixelCLB.CLBTools;


namespace PixelCLB
{
    public class HWIDVerify
    {
        private double min = 9.8;
        private double max = 10.3;
        public int failed = 0;
        private System.Timers.Timer verifyHWIDTimer = new System.Timers.Timer();

        public HWIDVerify()
        {
            timeOut(min, max);
        }

        public void timeOut(double minSec, double maxSec)
        {
            try
            {
                if(verifyHWIDTimer.Enabled)
                {
                    verifyHWIDTimer.Dispose();
                }
                verifyHWIDTimer = new System.Timers.Timer();
                verifyHWIDTimer.Interval = getInterval(min, max);
                verifyHWIDTimer.Elapsed += new System.Timers.ElapsedEventHandler(timerRaised);
                verifyHWIDTimer.Start();
            }
            catch(Exception e) {
            }
        }

        private void timerRaised(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (verifyHWIDTimer.Enabled)
                {
                    verifyHWIDTimer.Stop();
                    verifyHWIDTimer.Dispose();
                }
                verifyMyHWID();
            }
            catch { }
        }

        public double getInterval(double min, double max)
        {
            DateTime Now = DateTime.Now;
            double time = new Random().Next(Convert.ToInt32(min * 60000), Convert.ToInt32(max * 60000));
            while (min * 60000 > time || max * 60000 < time)
            {
                time = new Random().Next(Convert.ToInt32(min * 60000), Convert.ToInt32(max * 60000));
            }
            return time;
        }

        public void stopVerifyTimer()
        {
            if (verifyHWIDTimer.Enabled)
            {
                verifyHWIDTimer.Dispose();
            }
        }

        private  bool verifyHWID()
        {
            try
            {
                if (Program.usingHWID)
                {
                    System.Net.WebClient wc = new System.Net.WebClient();
                    byte[] data = wc.DownloadData("http://clb.pixelcha.com/clb/HWIDVerification/verifyHWID.php?HWID=" + Program.HWID + "&Version=" + Program.version + "&Launched=true");
                    string[] HWID = System.Text.Encoding.UTF8.GetString(data).Split(',');
                    wc.Dispose();
                    if (HWID.Length > 6)
                    {
                        if (HWID[4].Equals(Program.HWID))
                        {
                            Program.clbName = HWID[1];
                            Program.slogan = HWID[2];
                            Program.adLink = HWID[3];
                            Program.name = HWID[5];
                            HWID[7] = HWID[7].Replace(Environment.NewLine, "");  
                            if (HWID[7].ToLower().Contains("true"))
                            {
                                exitProgram();
                                return false;
                            }
                            Program.accessLevel = double.Parse(HWID[6]);
                            Program.getAccessLevel(Program.accessLevel);
                            if (Program.slogan.ToLower().Contains("debug mode"))
                                Program.debugMode = true;
                            else
                                Program.debugMode = false;
                            failed = 0;
                            return true;
                        }
                        else
                        {
                            if (failed >= 3)
                            {
                                MessageBox.Show(HWID[1]);
                                return false;
                            }
                            else
                                failed++;
                            return true;
                        }
                    }
                    else
                    {
                        if (failed >= 3)
                        {
                            MessageBox.Show(HWID[0]);
                            return false;
                        }
                        else
                            failed++;
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return true;
            }

        }

        private void exitProgram()
        {
            Program.loadPercent = 150;
            foreach (Client c in Program.clients)
            {
                c.updateLog("Disconnected due to inability licenses disabled");
                c.updateAccountStatus("Licenses disabled");
                c.forceDisconnect(false, 0, true, "Licenses Disabled", true);
            }
            MessageBox.Show("This version of the program is now disabled. Now terminating program.");
            Environment.Exit(0);
        }

        private void verifyMyHWID()
        {
            try
            {
                Thread checkHWID = new Thread(delegate()
                {
                    if (verifyHWID())
                    {
                        if (failed > 0)
                            Program.gui.updateProgramTexts(false);
                        else
                            Program.gui.updateProgramTexts(true);
                        timeOut(min, max);
                    }
                    else
                    {
                        Program.loadPercent = 150;
                        foreach (Client c in Program.clients)
                        {
                            c.updateLog("Disconnected due to inability to verify HWID");
                            c.updateAccountStatus("Unable to verify HWID");
                            c.forceDisconnect(false, 0, true, "HWID verification error");
                        }
                        MessageBox.Show("Unable to verify HWID! Please make sure you are a registered user. \nRestart the bot if you believe you are!", "Error!");
                        Environment.Exit(0);
                    }
                });
                checkHWID.Start();
            }
            catch
            { }
        }
    }
}
