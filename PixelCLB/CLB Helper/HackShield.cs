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
    public class HackShield
    {
        private double min = 4.5;
        private double max = 4.6;
        private System.Timers.Timer hackShieldtimer;
        private Client c;
        public List<System.Timers.Timer> hackShieldtimerList = new List<System.Timers.Timer>();
        public List<System.Timers.Timer> hackShieldtimerListBak = new List<System.Timers.Timer>();
        private DateTime execute;
         //4-4.5 minutes

        public HackShield()
        {
        }

        public HackShield(double minSecs, double maxSecs, Client client)
        {
            c = client;
            min = minSecs;
            max = maxSecs;
            timeOut(min, max);
        }


        public double timeLeft()
        {
            TimeSpan difference = execute - DateTime.Now;
            return Math.Floor(difference.TotalSeconds);
        }


        private void setTimers(double minSec, double maxSec)
        {
            try
            {
                double interval = getInterval(minSec, maxSec);

                hackShieldtimer = new System.Timers.Timer();
                execute = DateTime.Now.AddMilliseconds(interval);
                hackShieldtimer.Interval = interval;
                hackShieldtimer.Elapsed += new System.Timers.ElapsedEventHandler(timerRaised);
                hackShieldtimerList.Add(hackShieldtimer);
                hackShieldtimer.Start();


                hackShieldtimer = new System.Timers.Timer();
                hackShieldtimer.Interval = interval + 20000;
                hackShieldtimer.Elapsed += new System.Timers.ElapsedEventHandler(bakTimerRaised);
                hackShieldtimerListBak.Add(hackShieldtimer);
                hackShieldtimer.Start();
            }
            catch { }
        }

        public void timeOut(double minSec, double maxSec)
        {
            try
            {
                foreach (System.Timers.Timer timerCount in hackShieldtimerList)
                {
                    timerCount.Close();
                }
                setTimers(minSec, maxSec);
            }
            catch { }
        }


        public void bakTimerRaised(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                foreach (System.Timers.Timer timerCount in hackShieldtimerList)
                {
                    timerCount.Dispose();
                }
                hackShieldtimerList.Clear();
                foreach (System.Timers.Timer timerCount in hackShieldtimerListBak)
                {
                    timerCount.Dispose();
                }
                hackShieldtimerListBak.Clear();
                c.updateLog("[Hackshield Timer] Failed! Restarting Bot...");
                c.forceDisconnect(true, 2, false, "Hackshield timer failure");
            }
            catch { }
        }


        public void timerRaised(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                foreach (System.Timers.Timer timerCount in hackShieldtimerList)
                {
                    timerCount.Stop();
                    timerCount.Close();
                }
                //c.updateLog("[Hackshield Timer] Timer activated!");
                getAction(c);
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

        public void resetHackShieldTimer(double min, double max)
        {
            try
            {
                foreach (System.Timers.Timer timerCount in hackShieldtimerList)
                {
                    timerCount.Dispose();
                }
                hackShieldtimerList.Clear();
                foreach (System.Timers.Timer timerCount in hackShieldtimerListBak)
                {
                    timerCount.Dispose();
                }
                hackShieldtimerListBak.Clear();
                setTimers(min, max);
            }
            catch { }
        }


        public void stopHackShieldTimer()
        {
            try
            {
                foreach (System.Timers.Timer timerCount in hackShieldtimerList)
                {
                    timerCount.Dispose();
                }
                hackShieldtimerList.Clear();
                foreach (System.Timers.Timer timerCount in hackShieldtimerListBak)
                {
                    timerCount.Dispose();
                }
                hackShieldtimerListBak.Clear();
            }
            catch { }
        }

        private void getAction(Client c)
        {
            try
            {
                Thread threads = new Thread(delegate()
                {
                    if (c.clientStartAble)
                    {
                        if (c.clientMode == ClientMode.FMOWL)
                        {
                            try
                            {
                                string path = Path.Combine(Program.FMExport, c.owlWorldName, "Channel" + c.channel + ".txt");
                                if (File.Exists(path))
                                {
                                    using (FileStream stream = File.Open(path, FileMode.Open, FileAccess.ReadWrite))
                                    {
                                        stream.Close();
                                    }
                                }
                            }
                            catch (IOException)
                            {
                                Thread.Sleep(10);
                                getAction(c);
                            }
                        }
                        try
                        {
                            List<Thread> threadList = c.workerThreads.ToList<Thread>();
                            foreach (Thread t in threadList)
                            {
                                try
                                {
                                    t.Abort();
                                }
                                catch { }
                            }
                        }
                        catch
                        {
                            getAction(c);
                        }
                    }
                    try
                    {
                        if (c.clientStartAble & c.clientMode != ClientMode.DISCONNECTED)
                        {
                            Thread ts = new Thread(delegate()
                            {
                                if (c.clientMode == ClientMode.IDLE)
                                {
                                    c.cashShopManagement(false, false, 0, 0);
                                    c.onServerConnected(false);
                                }
                                else if (c.clientMode == ClientMode.MapleFarm)
                                {
                                    c.ExitFarm();
                                    c.onServerConnected(false);
                                }
                                /*if (c.clientMode == ClientMode.EXPLOIT)
                                {
                                    c.ses.SendPacket(PacketHandler.EXPLOIT_CLOSE(c).ToArray());
                                    c.cashShopManagement(false, false, 0, 0);
                                    c.onServerConnected();
                                }
                                 * */
                                else if (c.clientMode == ClientMode.FULLMAPPING_NP)
                                {
                                    c.cashShopManagement(false, false, 0, 0);
                                    c.updateAccountStatus("FullMapping @ CH" + c.channel.ToString() + " FM" + c.RoomNum + " via N.P.");
                                    c.onServerConnected(false);
                                }
                                else if (c.clientMode == ClientMode.SHOPAFKER)
                                {
                                    if (c.sent)
                                    {
                                        c.ses.SendPacket(PacketHandler.Close_Store().ToArray());
                                    }
                                    c.cashShopManagement(false, false, 0, 0);
                                    c.onServerConnected(true);
                                }
                                else if (c.clientMode == ClientMode.FMOWL)
                                {
                                    c.ses.SendPacket(PacketHandler.Close_Store().ToArray());
                                    c.cashShopManagement(false, false, 0, 0);
                                    c.onServerConnected(true);
                                }
                                else if (c.clientMode == ClientMode.PERMITUP || c.mode == 19)
                                {
                                    if (c.mode == 6)
                                    {
                                        c.mode = 5;
                                    }
                                    c.ses.SendPacket(PacketHandler.Close_Store().ToArray());
                                    c.cashShopManagement(false, false, 0, 0);
                                    c.onServerConnected(true);
                                }
                                else if (c.clientMode == ClientMode.LoginSpam && c.mode == 97)
                                {
                                    c.forceDisconnect(true, 1, false, "Login Spam Force DC");
                                    return;
                                }
                                else
                                {
                                    c.cashShopManagement(false, false, 0, 0);
                                    c.onServerConnected(false);
                                }
                            });
                            c.workerThreads.Add(ts);
                            ts.Start();
                        }
                    }
                    catch { }
                });
                threads.Start();
            }
            catch { }
        }
    }
}
