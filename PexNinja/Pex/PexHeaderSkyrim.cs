using System;
using System.Diagnostics.Contracts;
using System.IO;

namespace PexNinja.Pex
{
    public class PexHeaderSkyrim : PexHeader
    {
        protected const byte validMajor = 3;
        private const byte validMinor = 2;
        private const ushort validGameID = 1;

        public PexHeaderSkyrim()
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
