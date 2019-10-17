using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace System
{
	public static class Extension
	{
		public static byte RollLeft(this byte pThis, int pCount)
		{
			uint num = (uint)(pThis << (pCount % 8 & 31));
			return (byte)(num & 255 | num >> 8);
		}

		public static byte RollRight(this byte pThis, int pCount)
		{
			uint num = (uint)(pThis << 8 >> (pCount % 8 & 31));
			return (byte)(num & 255 | num >> 8);
		}

		public static string ToSHA512(this string pValue)
		{
			return BitConverter.ToString((new SHA512Managed()).ComputeHash(Encoding.Default.GetBytes(pValue))).Replace("-", string.Empty).ToUpper();
		}

		public static string ToString2s(this byte[] pData)
		{
			string str = "";
			byte[] numArray = pData;
			for (int i = 0; i < (int)numArray.Length; i++)
			{
				byte num = numArray[i];
				str = string.Concat(str, string.Format("{0:X2} ", num));
			}
			return str;
		}
	}
}