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
        public virtual IPexHeader Header { get; protected set; }
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

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    if (Data != null)
                    {
                        Data = null;
                    }
                }

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
        #endregion
    }
}
