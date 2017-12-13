using System;
using System.Diagnostics.Contracts;
using System.IO;

namespace PexNinja.Pex
{
    public class PexHeaderFallout4 : PexHeader
    {
        protected const byte validMajor = 3;
        private const byte validMinor = 9;
        private const ushort validGameID = 2;

        public PexHeaderFallout4()
        {
            MajorVersion = validMajor;
            MinorVersion = validMinor;
            GameID = validGameID;
        }

        public override bool IsValid()
        {
            return ((Magic == defaultMagic)
                && (MajorVersion == validMajor)
                && (MinorVersion == validMinor)
                && (GameID == validGameID));
        }
    }
}
