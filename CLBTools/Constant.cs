using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PixelCLB.Net.Packets;

namespace PixelCLB.CLBTools
{
    internal class Constant
    {
        public Constant()
        {
        }

        public static string getRandomHexString(int digets, char spacer)
        {
            Random random = new Random();
            string empty = string.Empty;
            empty = string.Concat(empty, HexEncoding.ToHex((byte)random.Next(255)));
            int num = 0;
            while (true)
            {
                bool flag = num < digets - 1;
                if (!flag)
                {
                    break;
                }
                empty = string.Concat(empty, spacer, HexEncoding.ToHex((byte)random.Next(255)));
                num++;
            }
            string str = empty;
            return str;
        }
        public static int timestamp = 0x10000000;
        public static int getNewTimestamp()
        {
            Random r = new Random();
            timestamp += r.Next(0xFFF);
            if (timestamp < 0)
            {
                timestamp &= 0x7FFFFFFF;
            }
            return timestamp;
        }
    }
}