using System.IO;
using System.Drawing;
using System.Text;
using PixelCLB.Tools;


namespace PixelCLB.Net
{
    public sealed class PacketBuilder : AbstractPacket
    {
        private readonly BinaryWriter binWriter;

        public PacketBuilder(object pObj) : this()
        {
            WriteShort((short)pObj);
        }

        public PacketBuilder(short pOpCode) : this()
        {
            WriteShort(pOpCode);
        }

        public PacketBuilder()
        {
            memStream = new MemoryStream();
            binWriter = new BinaryWriter(memStream);
        }

        public void Write(byte pByte)
        {
            binWriter.Write(pByte);
        }
        public void WriteZeros(int pCount)
        {
            binWriter.Write(new byte[pCount]);
        }
        public void WriteBytes(byte[] pBytes)
        {
            binWriter.Write(pBytes);
        }
        public void WriteShort(short pShort)
        {
            binWriter.Write(pShort);
        }
        public void WriteShort2(int @short)
        {
            binWriter.Write((short)@short);
        }
        public void WriteInt(int pInt)
        {
            binWriter.Write(pInt);
        }
        public void WriteLong(long pLong)
        {
            binWriter.Write(pLong);
        }
        public void WriteMapleString(string pFormat, params object[] pArgs)
        {
            WriteMapleString(string.Format(pFormat, pArgs));
        }
        public void WriteMapleString(string pString)
        {
            WriteShort((short)pString.Length);
            WriteString(pString);
        }
        public void WriteString(string pString)
        {
            binWriter.Write(pString.ToCharArray());
        }

        public void WriteHex(string pString)
        {
            WriteBytes(pString.GetBytes());
        }
        public void WritePos(Point p)
        {
            binWriter.Write((ushort)p.X);
            binWriter.Write((ushort)p.Y);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            if (disposing)
            {
                if (memStream != null)
                    memStream.Dispose();

                if (binWriter != null)
                    binWriter.Dispose();
            }
            base.Dispose(disposing);
            disposed = true;   
        }
    }
}
