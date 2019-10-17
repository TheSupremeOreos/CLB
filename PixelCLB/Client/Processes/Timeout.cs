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
using System.Xml;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;


namespace PixelCLB
{
    public partial class Client
    {
        //newTimeOut
        //Max Seconds / Min Seconds
        public void timeOut(int minSec, int maxSec)
        {
            try
            {
                foreach (System.Timers.Timer timerCount in timerList)
                {
                    try
                    {
                        timerCount.Dispose();
                    }
                    catch { }
                }
                timerList.Clear();
                timeOutCheck = false;
                timer = new System.Timers.Timer();
                timer.Interval = getInterval(minSec, maxSec);
                timer.Elapsed += new System.Timers.ElapsedEventHandler(timerRaised);
                timerList.Add(timer);
                timer.Start();
            }
            catch { }
        }
        public void timerRaised(object sender, System.Timers.ElapsedEventArgs e)
        {
            timeOutCheck = true;
            timer.Dispose();
        }

        public double getInterval(int min, int max)
        {
            DateTime Now = DateTime.Now;
            double time = new Random().Next(min * 1000, max * 1000);
            while (min * 1000 > time || max * 1000 < time)
            {
                time = new Random().Next(min * 1000, max * 1000);
            }
            return time;
        }
    }
}