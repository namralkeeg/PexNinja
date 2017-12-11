using System;
using System.Runtime.CompilerServices;

namespace PexNinja.NumberHelpers
{
    public enum Endian : Byte
    {
        Big,
        Little,
#if BIGENDIAN
        Native = Big,
#else
        Native = Little,
#endif
    }

    public static partial class NumberHelpers
    {
        public static Boolean Doswap(this Endian endian)
        {
            if (endian == Endian.Native)
                return false;

            switch (endian)
            {
                case Endian.Little:
                    return Endian.Little != Endian.Native;
                case Endian.Big:
                    return Endian.Big != Endian.Native;
                default:
                    throw new ArgumentException("Endian type not supported.");
            }
        }
    }
}
