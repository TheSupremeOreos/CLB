namespace PixelCLB.Net
{
    internal struct RecBuffer
    {
        public bool IsEncrypted;
        public bool IsHeader;
        public byte[] Data;
        public int Index;

        public RecBuffer(int pSize, bool pEncrypted)
        {
            IsEncrypted = pEncrypted;
            IsHeader = true;
            Data = new byte[pSize];
            Index = 0;
        }

        public short Position
        {
            get
            {
                return (short)(Length - Index);
            }
        }
        public short Length
        {
            get
            {
                return (short)Data.Length;
            }
        }
        public bool IsReady
        {
            get
            {
                return (Length == Index);
            }
        }

        public void Reset(bool pIsHeader, int pSize)
        {
            IsHeader = pIsHeader;
            Index = 0;
            Data = new byte[pSize];
        }
        public void Dispose()
        {
            Data = null;
            Index = 0;
        }
    }
}
