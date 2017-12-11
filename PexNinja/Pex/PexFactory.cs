using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PexNinja.Pex
{
    public static class PexFactory
    {
        public static IPex GetPex(string game)
        {
            if (game == null)
                throw new ArgumentNullException("stream");
            Contract.EndContractBlock();

            var gameName = game.ToLower();
            switch (gameName)
            {
                case "skyrim":
                    return new PexSkyrim();
                case "skyrimse":
                    return new PexSkyrimSE();
                case "fallout4":
                    return new PexFallout4();
                default:
                    return null;
            }
        }

        public static IPex GetPex(byte majorVersion, byte minorVersion, ushort gameID)
        {
            IPexHeader header = PexHeaderFactory.GetPexHeader(majorVersion, minorVersion, gameID);
            if ((PexHeaderSkyrimSE)header != null)
            {
                return new PexSkyrimSE();
            }
            else if ((PexHeaderFallout4)header != null)
            {
                return new PexFallout4();
            }
            else if ((PexHeaderSkyrim)header != null)
            {
                return new PexSkyrim();
            }
            else
            {
                return null;
            }
        }

        public static IPex GetPex(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            Contract.EndContractBlock();

            IPex pex = new PexSkyrimSE();
            if (pex.ReadHeader(stream) && pex.Header.IsValid())
            {
                return pex;
            }

            pex = new PexFallout4();
            if (pex.ReadHeader(stream) && pex.Header.IsValid())
            {
                return pex;
            }

            pex = new PexSkyrim();
            if (pex.ReadHeader(stream) && pex.Header.IsValid())
            {
                return pex;
            }

            return null;
        }
    }
}

