using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using PixelCLB;
using PixelCLB.Crypto;

namespace PixelCLB.Crypto
{
    public class MapleCrypt
    {
        private static ICryptoTransform mTransformer;

        private ushort mBuild;
        private byte[] mIV;
        private RijndaelManaged mAES = new RijndaelManaged();


        public MapleCrypt(ushort pBuild, byte[] pIV, bool show)
        {
            mBuild = pBuild;
            if ((short)pBuild < 0)
            { // Second one
                pBuild = (ushort)(0xFFFF - pBuild);
            }
            if (pBuild >= 118) // GMS uses random keys since 118!
                mAES.Key = GMSKeys.GetKeyForVersion(pBuild);
            else
                mAES.Key = CryptoKeys.AESKey;
            mAES.Mode = CipherMode.ECB;
            mAES.Padding = PaddingMode.PKCS7;
            mTransformer = mAES.CreateEncryptor();
            mBuild = pBuild;
            mIV = pIV;
            if (show == true)
                Logger.Write(Logger.LogTypes.DataLoad, "GMS Keys Loaded... Key: {0}", mAES.Key.ToString2s());

        }

        public void Transform(byte[] pBuffer)
        {
            int remaining = pBuffer.Length;
            int length = 0x5B0;
            int start = 0;
            byte[] realIV = new byte[4 * 4];
            while (remaining > 0)
            {
                for (int y = 0; y < 16; y++)
                {
                    realIV[y] = mIV[y % 4];
                }
                if (remaining < length)
                {
                    length = remaining;
                }
                for (int index = start; index < (start + length); ++index)
                {
                    if (((index - start) % realIV.Length) == 0)
                    {
                        byte[] tempIV = new byte[realIV.Length];
                        mTransformer.TransformBlock(realIV, 0, realIV.Length, tempIV, 0);
                        Buffer.BlockCopy(tempIV, 0, realIV, 0, realIV.Length);
                        //realIV = mTransformer.TransformFinalBlock(realIV, 0, realIV.Length);
                    }
                    pBuffer[index] ^= realIV[(index - start) % realIV.Length];
                }
                start += length;
                remaining -= length;
                length = 0x5B4;
            }
            ShiftIV();
        }
        private void ShiftIV()
        {
            byte[] newIV = CryptoKeys.ShuffleIV;

            for (int index = 0; index < 4; index++)
            {
                byte temp1 = newIV[1];
                byte temp2 = CryptoKeys.Shufflers[temp1];
                byte temp3 = mIV[index];
                temp2 -= temp3;
                newIV[0] += temp2;
                temp2 = newIV[2];
                temp2 ^= CryptoKeys.Shufflers[temp3];
                temp1 -= temp2;
                newIV[1] = temp1;
                temp1 = newIV[3];
                temp2 = temp1;
                temp1 -= newIV[0];
                temp2 = CryptoKeys.Shufflers[temp2];
                temp2 += temp3;
                temp2 ^= newIV[2];
                newIV[2] = temp2;
                temp1 += CryptoKeys.Shufflers[temp3];
                newIV[3] = temp1;

                uint result1 = (uint)newIV[0] | ((uint)newIV[1] << 8) | ((uint)newIV[2] << 16) | ((uint)newIV[3] << 24);
                uint result2 = result1 >> 0x1D;
                result1 <<= 3;
                result2 |= result1;

                newIV = new byte[]
                {
                    (byte)(result2 & 0xFF),
                    (byte)((result2 >> 8) & 0xFF),
                    (byte)((result2 >> 16) & 0xFF),
                    (byte)((result2 >> 24) & 0xFF)
                };
            }
            Buffer.BlockCopy(newIV, 0, mIV, 0, mIV.Length);
        }

        public byte[] GetHeaderToServer(int pSize)
        {
            int a = ((mIV[3] << 8) | mIV[2]) ^ mBuild;
            int b = a ^ pSize;

            return new byte[4]
            {
                unchecked((byte)(a % 0x100)),
                unchecked((byte)(a / 0x100)),
                unchecked((byte)(b % 0x100)),
                unchecked((byte)(b / 0x100))
            }; //TODO : find cast alternative
        }
        public static int GetPacketLength(byte[] pHeader)
        {
            if (pHeader.Length != 4) throw new ArgumentException("Array is not the size of 4", "pHeader");

            int size = (pHeader[0] + (pHeader[1] << 8)) ^ (pHeader[2] + (pHeader[3] << 8));

            return size;
        }
    }
}
