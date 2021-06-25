using System;
using System.Runtime.InteropServices;

namespace STB
{
    internal static unsafe class CRuntime
    {
        public const long DBL_EXP_MASK = 0x7ff0000000000000L;
        public const int DBL_MANT_BITS = 52;
        public const long DBL_SGN_MASK = -1 - 0x7fffffffffffffffL;
        public const long DBL_MANT_MASK = 0x000fffffffffffffL;
        public const long DBL_EXP_CLR_MASK = DBL_SGN_MASK | DBL_MANT_MASK;

        public static void* Malloc(ulong size)
        {
            return Malloc((long)size);
        }

        public static void* Malloc(long size)
        {
            var ptr = Marshal.AllocHGlobal((int)size);

            return ptr.ToPointer();
        }

        public static void Memcpy(void* a, void* b, long size)
        {
            var ap = (byte*)a;
            var bp = (byte*)b;
            for (long i = 0; i < size; ++i)
                *ap++ = *bp++;
        }

        public static void Memcpy(void* a, void* b, ulong size)
        {
            Memcpy(a, b, (long)size);
        }

        public static void Memmove(void* a, void* b, long size)
        {
            void* temp = null;

            try
            {
                temp = Malloc(size);
                Memcpy(temp, b, size);
                Memcpy(a, temp, size);
            }

            finally
            {
                if (temp != null)
                    Free(temp);
            }
        }

        public static void Memmove(void* a, void* b, ulong size)
        {
            Memmove(a, b, (long)size);
        }

        public static int Memcmp(void* a, void* b, long size)
        {
            var result = 0;
            var ap = (byte*)a;
            var bp = (byte*)b;
            for (long i = 0; i < size; ++i)
            {
                if (*ap != *bp)
                    result += 1;

                ap++;
                bp++;
            }

            return result;
        }

        public static int Memcmp(void* a, void* b, ulong size)
        {
            return Memcmp(a, b, (long)size);
        }

        public static int Memcmp(byte* a, byte[] b, ulong size)
        {
            fixed (void* bptr = b)
            {
                return Memcmp(a, bptr, (long)size);
            }
        }

        public static void Free(void* a)
        {
            if (a == null)
                return;

            var ptr = new IntPtr(a);
            Marshal.FreeHGlobal(ptr);
        }

        /// <summary>
		/// This code had been borrowed from here: https://github.com/MachineCognitis/C.math.NET
		/// </summary>
		/// <param name="number"></param>
		/// <param name="exponent"></param>
		/// <returns></returns>
		public static double Frexp(double number, int* exponent)
        {
            var bits = BitConverter.DoubleToInt64Bits(number);
            var exp = (int)((bits & DBL_EXP_MASK) >> DBL_MANT_BITS);
            *exponent = 0;

            if (exp == 0x7ff || number == 0D)
                number += number;
            else
            {
                // Not zero and finite.
                *exponent = exp - 1022;
                if (exp == 0)
                {
                    // Subnormal, scale number so that it is in [1, 2).
                    number *= BitConverter.Int64BitsToDouble(0x4350000000000000L); // 2^54
                    bits = BitConverter.DoubleToInt64Bits(number);
                    exp = (int)((bits & DBL_EXP_MASK) >> DBL_MANT_BITS);
                    *exponent = exp - 1022 - 54;
                }

                // Set exponent to -1 so that number is in [0.5, 1).
                number = BitConverter.Int64BitsToDouble((bits & DBL_EXP_CLR_MASK) | 0x3fe0000000000000L);
            }

            return number;
        }

        public static void Memset(void* ptr, int value, long size)
        {
            var bptr = (byte*)ptr;
            var bval = (byte)value;
            for (long i = 0; i < size; ++i)
                *bptr++ = bval;
        }

        public static void Memset(void* ptr, int value, ulong size)
        {
            Memset(ptr, value, (long)size);
        }

        public static uint _lrotl(uint x, int y)
        {
            return (x << y) | (x >> (32 - y));
        }

        public static void* Realloc(void* a, long newSize)
        {
            if (a == null)
                return Malloc(newSize);

            var ptr = new IntPtr(a);
            var result = Marshal.ReAllocHGlobal(ptr, new IntPtr(newSize));

            return result.ToPointer();
        }

        public static void* Realloc(void* a, ulong newSize)
        {
            return Realloc(a, (long)newSize);
        }

        public static int Abs(int v)
        {
            return Math.Abs(v);
        }

        public static float Fabs(double a)
        {
            return (float)Math.Abs(a);
        }

        public static double Sqrt(double val)
        {
            return Math.Sqrt(val);
        }

        public static double Ceil(double a)
        {
            return Math.Ceiling(a);
        }
        public static double Floor(double a)
        {
            return Math.Floor(a);
        }
        public static double Pow(double a, double b)
        {
            return Math.Pow(a, b);
        }

        public static double Fmod(double x, double y)
        {
            return x % y;
        }
        public static double Cos(double value)
        {
            return Math.Cos(value);
        }

        public static double Acos(double value)
        {
            return Math.Acos(value);
        }

        public static ulong Strlen(sbyte* str)
        {
            var ptr = str;

            while (*ptr != '\0')
                ptr++;

            return (ulong)ptr - (ulong)str - 1;
        }

        public static void SetArray<T>(T[] data, T value)
        {
            for (var i = 0; i < data.Length; ++i)
                data[i] = value;
        }
    }
}
