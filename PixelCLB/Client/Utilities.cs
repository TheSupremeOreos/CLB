using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace PixelCLB
{
    internal class Utilities
    {
        public Utilities()
        {
        }

        public static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return dateTime.AddSeconds(timestamp);
        }

        public static void CopyBytes(byte[] Destination, byte[] Source)
        {
            Buffer.BlockCopy(Source, 0, Destination, 0, (int)Source.Length);
        }

        public static void CopyBytes(byte[] Destination, byte[] Source, int Count)
        {
            Buffer.BlockCopy(Source, 0, Destination, 0, Count);
        }

        public static void CopyBytes(byte[] Destination, int DestinationIndex, byte[] Source, int SourceIndex, int Count)
        {
            Buffer.BlockCopy(Source, SourceIndex, Destination, DestinationIndex, Count);
        }

        public static byte[] GetBytes(string bitString)
        {
            return Enumerable.Range(0, bitString.Length / 8).Select<int, byte>((int pos) => Convert.ToByte(bitString.Substring(pos * 8, 8), 2)).ToArray<byte>();
        }


        [DllImport("kernel32", CharSet = CharSet.None)]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public static char getRandomHexChar(int seed)
        {
            Random random = new Random(seed);
            char[] chrArray = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
            char[] chrArray1 = chrArray;
            return chrArray1[random.Next(16)];
        }


        public static string HTMLEncodeSpecialChars(string text)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string str = text;
            for (int i = 0; i < str.Length; i++)
            {
                char chr = str[i];
                if (chr <= '\uFFFF')
                {
                    stringBuilder.Append(chr);
                }
                else
                {
                    byte[] bytes = new byte[] { BitConverter.GetBytes(chr)[0] };
                    string str1 = BitConverter.ToString(bytes, 0);
                    stringBuilder.Append(string.Format("%{0}", str1));
                }
            }
            return stringBuilder.ToString();
        }

        public static string IniReadValue(string path, string Section, string Key)
        {
            StringBuilder stringBuilder = new StringBuilder(255);
            Utilities.GetPrivateProfileString(Section, Key, "", stringBuilder, 255, path);
            return stringBuilder.ToString();
        }

        public static bool IsFileLocked(FileInfo file)
        {
            bool flag;
            FileStream fileStream = null;
            try
            {
                try
                {
                    fileStream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                }
                catch
                {
                    flag = true;
                    return flag;
                }
                return false;
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }
        }

        public static void MoveBytes(byte[] Bytes, int Index)
        {
            for (int i = 0; i < (int)Bytes.Length - Index; i++)
            {
                Bytes[i] = Bytes[i + Index];
            }
        }

        public static byte[] ReplaceBytes(byte[] Bytes, int Index)
        {
            if ((int)Bytes.Length - Index != 0)
            {
                byte[] numArray = new byte[(int)Bytes.Length - Index];
                Buffer.BlockCopy(Bytes, Index, numArray, 0, (int)Bytes.Length - Index);
                return numArray;
            }
            else
            {
                byte[] numArray1 = new byte[1];
                return numArray1;
            }
        }

        public static byte[] StringToByteArray(string hex)
        {
            IEnumerable<int> nums = Enumerable.Range(0, hex.Length);
            return nums.Where<int>((int x) => x % 2 == 0).Select<int, byte>((int x) => Convert.ToByte(hex.Substring(x, 2), 16)).ToArray<byte>();
        }

        [DllImport("kernel32", CharSet = CharSet.None)]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
    }
}