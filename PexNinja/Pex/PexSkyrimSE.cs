using System;

namespace PexNinja.Pex
{
    public class PexSkyrimSE : PexSkyrim
    {
        private PexHeaderSkyrimSE header;
        public override IPexHeader Header { get => header; protected set => header = (PexHeaderSkyrimSE)value; }

        public PexSkyrimSE() : this(new PexHeaderSkyrimSE()) { }
        protected PexSkyrimSE(IPexHeader pexHeader) : base(pexHeader) { }

        public override string ToString()
        {
            return header.ToString();
        }
    }
}
