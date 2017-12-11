using System;

namespace PexNinja.Pex
{
    public class PexHeaderSkyrimSE : PexHeaderSkyrim
    {
        private const byte validMinor = 1;
        private const ushort validGameID = 1;

        public PexHeaderSkyrimSE() : base()
        {
            MinorVersion = validMinor;
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
