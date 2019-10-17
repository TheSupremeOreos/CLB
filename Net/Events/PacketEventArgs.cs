using System;

namespace PixelCLB.Net.Events
{
    public class PacketEventArgs : EventArgs
    {
        public PacketReader Reader { get; set; }

        public PacketEventArgs(byte[] pPacket)
        {
            Reader = new PacketReader(pPacket);
        }
    }
}
