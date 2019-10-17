using System;

namespace PixelCLB.Net.Events
{
    public sealed class HandshakeEventArgs : EventArgs
    {
        public ushort Version { get; private set; }
        public string Subversion { get; private set; }
        public byte[] SIV { get; private set; }
        public byte[] RIV { get; private set; }
        public byte Locale { get; private set; }

        public HandshakeEventArgs(PacketReader pPacket)
        {
            pPacket.Position = 0;

            Version = pPacket.ReadUShort();
            Subversion = pPacket.ReadString();

            SIV = pPacket.ReadBytes(4);
            RIV = pPacket.ReadBytes(4);

            Locale = pPacket.Read();

            pPacket.Dispose();
        }
        public HandshakeEventArgs(ushort pVersion, string pSubversion, byte[] pSIV, byte[] pRIV, byte pLocale)
        {
            Version = pVersion;
            Subversion = pSubversion;
            SIV = pSIV;
            RIV = pRIV;
            Locale = pLocale;
        }

        public override string ToString()
        {
            return string.Format("Maplestory v{0}.{1}", Version, Subversion);
        }
    }
}
