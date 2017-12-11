using System;
using System.Runtime.InteropServices;

namespace PexNinja.NumberHelpers
{
    [StructLayout(LayoutKind.Explicit)]
    internal struct DoubleUnion
    {
        [FieldOffset(0)]
        private double D;

        [FieldOffset(0)]
        private ulong U;

        public DoubleUnion(double data)
        {
            U = default(ulong);
            D = data;
        }

        public DoubleUnion(ulong data)
        {
            D = default(double);
            U = data;
        }

        public double AsDouble
        {
            get { return D; }
            set { D = value; }
        }

        public ulong AsUInt64
        {
            get { return U; }
            set { U = value; }
        }
    }
}
