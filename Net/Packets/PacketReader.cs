using System.IO;
using System;
using System.Drawing;
using System.Text;
using PixelCLB.Tools;

namespace PixelCLB.Net
{
    public sealed class PacketReader : AbstractPacket
    {
        private readonly BinaryReader binReader;

        public PacketReader(byte[] pBytes)
        {
            memStream = new MemoryStream(pBytes);
            binReader = new BinaryReader(memStream);
        }

        public int Remaining
        {
            get { return Length - Position; }
        }
        public void Skip(int length)
        {
            memStream.Position += length;
        }
        public short Length
        {
            get { return (short)memStream.Length; }
        }

        public void Reset(int length)
        {
            memStream.Seek(length, SeekOrigin.Begin);
        }

        public byte Read()
        {
            return binReader.ReadByte();
        }
        public byte[] ReadBytes(short pCount)
        {
            return binReader.ReadBytes(pCount);
        }
        public short ReadShort()
        {
            return binReader.ReadInt16();
        }
        public ushort ReadUShort()
        {
            return binReader.ReadUInt16();
        }
        public int ReadInt()
        {
            return binReader.ReadInt32();
        }
        public uint ReadUInt()
        {
            return binReader.ReadUInt32();
        }
        public long ReadLong()
        {
            return binReader.ReadInt64();
        }
        public ulong ReadULong()
        {
            return binReader.ReadUInt64();
        }
        public string ReadString(short pLen = -1)
        {
            short count = (pLen == -1) ? binReader.ReadInt16() : pLen;
            return new string(binReader.ReadChars(count));
        }
        public string ReadMapleString()
        {
            return ReadString(ReadShort());
        }
        public Point ReadPos()
        {
            return new Point(ReadShort(), ReadShort());
        }
        public string ReadIP()
        {
            return Read() + "." + Read() + "." + Read() + "." + Read();
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

                if (binReader != null)
                    binReader.Dispose();
            }
            base.Dispose(disposing);
            disposed = true;
        }
    }
}
