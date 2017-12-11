using System;
using System.Runtime.InteropServices;

namespace PexNinja.NumberHelpers
{
    [StructLayout(LayoutKind.Explicit)]
    internal struct SingleUnion
    {
        [FieldOffset(0)]
        private float F;

        [FieldOffset(0)]
        private uint U;

        public SingleUnion(float data)
        {
            U = default(uint);
            F = data;
        }

        public SingleUnion(uint data)
        {
            F = default(float);
            U = data;
        }

        public float AsSingle
        {
            get { return F; }
            set { F = value; }
        }

        public uint AsUInt32
        {
            get { return U; }
            set { U = value; }
        }
    }
}
