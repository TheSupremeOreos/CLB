namespace PixelCLB.Tools
{
    static class BitTools
    {
        /// <summary>
        /// Performs a bit-wise left roll on a byte.
        /// </summary>
        /// <param name="b">The byte to roll left.</param>
        /// <param name="count">The number of bit positions to roll.</param>
        /// <returns>the resulting byte.</returns>
        public static byte RollLeft(byte b, int count)
        {
            int tmp = b << (count & 7);
            return unchecked((byte)(tmp | (tmp >> 8)));
        }

        /// <summary>
        /// Performs a bit-wise right roll on a byte.
        /// </summary>
        /// <param name="b">The byte to roll right.</param>
        /// <param name="count">The number of bit positions to roll.</param>
        /// <returns>the resulting byte.</returns>
        public static byte RollRight(byte b, int count)
        {
            int tmp = b << (8 - (count & 7));
            return unchecked((byte)(tmp | (tmp >> 8)));
        }
    }
}
