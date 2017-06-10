using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace FashionAde.Utils
{
    public class Security
    {
        public static int DefineHashForHostName(string fileName)
        {
            return DefineHashForFileName(fileName, 2);
        }

        public static int DefineHashForFileName(string fileName, int bitCount) 
        {
            int val = 1;

            if (string.IsNullOrEmpty(fileName))
                return val;

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(fileName);
            byte[] retVal = md5.ComputeHash(bs);

            return GetBits2(retVal[0], 8 - bitCount, bitCount) + 1;
        }

        private static int GetBits2(byte b, int offset, int count)
        {
            int result = 0;
            int pow = 1;

            b >>= offset;
            for (int i = 0; i < count; ++i)
            {
                if (((byte)1 & b) == 1)
                {
                    result += pow;
                }

                b >>= 1;
                pow *= 2;
            }

            return result;
        }

    }


}
