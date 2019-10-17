using System;
using PixelCLB.Net;

namespace PixelCLB.Tools
{
    public static class Extensions
    {
        public static string GetAscii(this byte[] pBytes)
        {
            string toRet = string.Empty;

            foreach (byte bit in pBytes)
            {
                char ascii = bit >= 0x20 && bit <= 0x7E ? (char)bit : '.'; // Space to ~
                toRet += ascii;
            }

            return toRet;
        }
        public static string GetString(this byte[] pBytes)
        {
            return BitConverter.ToString(pBytes).Replace('-', ' ');
        }    
        public static byte GetByte(this string pHex)
        {
            return byte.Parse(pHex, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture);
        }
        public static byte[] GetBytes(this string pHexString)
        {
            string trimmed = string.Empty;

            foreach (char c in pHexString.ToUpper())
            {
                if (c >= 'A' && c <= 'F' || c >= '0' && c <= '9')
                    trimmed += c;
                else if (c == '*')
                    trimmed += HexTools.hexChars[HexTools.Random.Next(0, 16)];
            }

            int max = (trimmed.Length % 2) == 0 ? trimmed.Length : trimmed.Length - 1;
            byte[] buffer = new byte[max / 2];

            for (int i = 0, j = 0; (i + 1) < max; i += 2, j++)
            {
                string bit = trimmed.Substring(i, 2);
                buffer[j] = bit.GetByte();
            }

            return buffer;
        }
        public static byte[] QuickClone(this byte[] pBytes)
        {
            int length = pBytes.Length;
            byte[] toRet = new byte[length];
            Array.Copy(pBytes, toRet, length);
            return toRet;
        }
        public static void Send(this AbstractPacket pPacket, Session pSession)
        {
            pSession.SendPacket(pPacket.ToArray());
        }
    }
}
