using System;
using System.Runtime.InteropServices;

namespace PixelCLB.Tools
{
    public static class HexTools
    {
        [DllImport("Winmm")]
        private static extern int timeGetTime();
        private static Random _rand;
        private static string _fakeMac;
        public static int timestamp = 0x00;

        public static readonly char[] hexChars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
        public static Random Random
        {
            get
            {
                if (_rand == null)
                    _rand = new Random();

                return _rand;
            }
        }
        public static int Timestamp
        {
            get
            {
                return timeGetTime();
            }
        }
        public static int getNewTimeStamp()
        {
            Random r = new Random();
            timestamp = r.Next(0xFF);
            if (timestamp < 0)
            {
                timestamp &= 0xFF;
            }
            return timestamp;
        }
        public static string FakeMAC
        {
            get
            {
                if (_fakeMac == null)
                {
                    byte randomByte = (byte)Random.Next(0xFF);
                    _fakeMac += randomByte.ToString("X2");

                    for (int i = 0; i < 6; i++)
                    {
                        randomByte = (byte)Random.Next(0xFF);
                        _fakeMac += '-' + randomByte.ToString("X2");
                    }
                }
                return _fakeMac;
            }
        }
    }
}
