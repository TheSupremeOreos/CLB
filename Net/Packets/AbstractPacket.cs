using System;
using System.IO;
using PixelCLB.Tools;

namespace PixelCLB.Net
{
    public abstract class AbstractPacket : IDisposable
    {
        protected bool disposed;
        protected MemoryStream memStream;

        public int Length
        {
            get { return (int)memStream.Length; }
        }
        public int Position
        {
            get { return (int)memStream.Position; }
            set { memStream.Position = value; }
        }
        public byte[] ToArray()
        {
            return memStream.ToArray();
        }
        public override string ToString()
        {
            return ToArray().GetString();
        }

        protected virtual void Dispose(bool disposing)
        {
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~AbstractPacket()
        {
            Dispose(false);
        }
    }
}
