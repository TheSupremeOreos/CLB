using System;
using PixelCLB.Tools;

namespace PixelCLB.Crypto
{
    static class MapleCustom
    {
        public static void Encrypt(byte[] data)
        {
            if (data == null) throw new ArgumentNullException("data");

            int length = data.Length;
            var truncatedLength = unchecked((byte)length);

            for (int j = 0; j < 6; j++)
            {
                if (j % 2 == 0)
                    EvenEncryptTransform(data, length, truncatedLength);
                else
                    OddEncryptTransform(data, length, truncatedLength);
            }
        }
        private static void EvenEncryptTransform(byte[] data, int length, byte lengthByte)
        {
            byte remember = 0;
            for (int i = 0; i < length; i++)
            {
                byte current = BitTools.RollLeft(data[i], 3);
                current += lengthByte;

                current ^= remember;
                remember = current;

                current = BitTools.RollRight(current, lengthByte);
                current = (byte)(~current & 0xFF);
                current += 0x48;
                data[i] = current;

                lengthByte--;
            }
        }
        private static void OddEncryptTransform(byte[] data, int length, byte lengthByte)
        {
            byte remember = 0;
            for (int i = length - 1; i >= 0; i--)
            {
                byte current = BitTools.RollLeft(data[i], 4);
                current += lengthByte;

                current ^= remember;
                remember = current;

                current ^= 0x13;
                current = BitTools.RollRight(current, 3);
                data[i] = current;

                lengthByte--;
            }
        }

        public static void Decrypt(byte[] data)
        {
            if (data == null) throw new ArgumentNullException("data");

            int length = data.Length;
            var truncatedLength = unchecked((byte)length);

            for (int j = 1; j <= 6; j++)
            {
                if (j % 2 == 0)
                    EvenDecryptTransform(data, length, truncatedLength);
                else
                    OddDecryptTransform(data, length, truncatedLength);
            }
        }
        private static void EvenDecryptTransform(byte[] data, int length, byte lengthByte)
        {
            byte remember = 0;
            for (int i = 0; i < length; i++)
            {
                byte current = data[i];
                current -= 0x48;
                current = unchecked((byte)(~current));
                current = BitTools.RollLeft(current, lengthByte);

                byte tmp = current;
                current ^= remember;
                remember = tmp;

                current -= lengthByte;
                data[i] = BitTools.RollRight(current, 3);

                lengthByte--;
            }
        }
        private static void OddDecryptTransform(byte[] data, int length, byte lengthByte)
        {
            byte remember = 0;
            for (int i = length - 1; i >= 0; i--)
            {
                byte current = BitTools.RollLeft(data[i], 3);
                current ^= 0x13;

                byte tmp = current;
                current ^= remember;
                remember = tmp;

                current -= lengthByte;
                data[i] = BitTools.RollRight(current, 4);

                lengthByte--;
            }
        }
    }
}
