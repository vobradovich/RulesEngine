using System;
using System.Security.Cryptography;

namespace RulesEngine.Benchmark
{
    public static class Nanoid2
    {
        private static RandomNumberGenerator _r = RandomNumberGenerator.Create();

        const string url = "ModuleSymbhasOwnPr-0123456789ABCDEFGHIJKLNQRTUVWXYZ_cfgijkpqtvxz";

        public static byte[] GetBytes(int size = 16)
        {
            var _b = new byte[size];
            _r.GetBytes(_b);
            return _b;
        }

        public static string Get(int size = 16)
        {
            Span<byte> b = new byte[size];
            Span<char> c = new char[size];
            RandomNumberGenerator.Fill(b);
            //_r.GetBytes(a);
            while (0 < size--)
            {
                c[size] = url[b[size] & 63];
            }
            var s = new string(c);
            //return new string(_c);
            return s;
            //return Get(GetBytes(size));
            //var _b = new byte[size];
            //var _c = new char[size];
            //_r.GetBytes(_b);

            //while (0 < size--)
            //{
            //    _c[size] = url[_b[size] & 63];
            //}
            //return new string(_c);            

            //fixed (char* c = new char[size])
            //{
            //    fixed (byte* inData = _b)
            //    {
            //        while (0 < size--)
            //        {
            //            c[size] = url[inData[size] & 63];
            //        }
            //        return new string(c);
            //    }
            //}
        }

        public static string Get(byte[] bytes)
        {
            var size = bytes.Length;
            var c = new char[size];

            while (0 < size--)
            {
                c[size] = url[bytes[size] & 63];
            }
            return new string(c);
        }
    }

    public class Nanoid
    {
        const string alphabet = "ModuleSymbhasOwnPr-0123456789ABCDEFGHIJKLNQRTUVWXYZ_cfgijkpqtvxz";

        public static string New(int size = 24, string abc = alphabet)
        {
            return New(GetBytes(size), abc);
        }

        public static byte[] GetBytes(int size = 24)
        {
            Span<byte> b = new byte[size];
            RandomNumberGenerator.Fill(b);
            return b.ToArray();
        }

        public static string New(byte[] bytes, string abc = alphabet)
        {
            var size = bytes.Length;
            var c = new char[size];
            while (0 < size--)
            {
                c[size] = abc[bytes[size] & 63];
            }
            return new string(c);
        }
    }

}
