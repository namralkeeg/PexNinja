using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PexNinja.NumberHelpers
{
    public static partial class NumberHelpers
    {
        public static ushort Swap(this ushort value)
        {
            return (ushort)(((value >> 8) & 0x00FF) | ((value << 8) & 0xFF00));
        }

        public static short Swap(this short value)
        {
            return (short)Swap((ushort)value);
        }

        public static uint Swap(this uint value)
        {
            uint x = (value & 0x0000FFFF) >> 16 | (value & 0xFFFF0000) << 16;
            return ((x & 0xFF00FF00) >> 8 | (x & 0x00FF00FF) << 8);
        }

        public static int Swap(this int value)
        {
            return (int)Swap((uint)value);
        }

        public static ulong Swap(this ulong value)
        {
            // swap adjacent 32-bit blocks
            ulong x = (value >> 32) | (value << 32);
            // swap adjacent 16-bit blocks
            x = ((x & 0xFFFF0000FFFF0000) >> 16) | ((x & 0x0000FFFF0000FFFF) << 16);
            // swap adjacent 8-bit blocks
            return ((x & 0xFF00FF00FF00FF00) >> 8) | ((x & 0x00FF00FF00FF00FF) << 8);
        }

        public static long Swap(this long value)
        {
            return (long)Swap((ulong)value);
        }

        public static float Swap(this float value)
        {
            var su = new SingleUnion(value);
            su.AsUInt32 = su.AsUInt32.Swap();
            return su.AsSingle;
        }

        public static double Swap(this double value)
        {
            var du = new DoubleUnion(value);
            du.AsUInt64 = du.AsUInt64.Swap();
            return du.AsUInt64;
        }
    }
}
