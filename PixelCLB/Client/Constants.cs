using MapleLib.WzLib;
using System;

namespace PixelCLB
{
    public static class Constants
    {
        public static byte defaultChannel;

        public static WzMapleVersion WzType;

        static Constants()
        {
            Constants.defaultChannel = 10;
            Constants.WzType = WzMapleVersion.BMS;
        }
    }
}
