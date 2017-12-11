using System;
using System.IO;
using System.Text;

namespace PexNinja.Pex
{
    public abstract class PexBasic : IPex
    {
        #region Properties
        // Windows 1252 default text encoding.
        protected Encoding encoding = Encoding.GetEncoding(1252);
        public byte[] Data { get; protected set; }
        public IPexHeader Header { get; protected set; }
        #endregion

        #region Constructors
        protected PexBasic(IPexHeader pexHeader)
        {
            Header = pexHeader ?? throw new ArgumentNullException("pexHeader");
        }
        #endregion

        #region Methods
        public abstract bool IsPex();
        public abstract bool IsPex(Stream stream);
        public abstract bool Read(Stream stream);
        public abstract void Write(Stream stream);
        public abstract bool ReadHeader(Stream stream);
        public abstract void WriteHeader(Stream stream);
        #endregion
    }
}
