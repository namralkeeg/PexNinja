using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PexNinja.Pex
{
    public static class PexHeaderFactory
    {
        public static IPexHeader GetPexHeader(byte majorVersion, byte minorVersion, ushort gameID)
        {
            if ((majorVersion == 3) && (minorVersion == 2) && (gameID == 1))
            {
                return new PexHeaderSkyrim();
            }
            else if ((majorVersion == 3) && (minorVersion == 1) && (gameID == 1))
            {
                return new PexHeaderSkyrimSE();
            }
            else if ((majorVersion == 3) && (minorVersion == 9) && (gameID == 2))
            {
                return new PexHeaderFallout4();
            }
            else
            {
                return null;
            }
        }
    }
}
