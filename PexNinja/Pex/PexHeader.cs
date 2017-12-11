using System;
using System.IO;
using System.Text;

namespace PexNinja.Pex
{
    public abstract class PexHeader : IPexHeader
    {
        public const uint defaultMagic = 0xFA57C0DEu;

        #region Properties
        public uint Magic { get; set; } = defaultMagic;
        public byte MajorVersion { get; set; }
        public byte MinorVersion { get; set; }
        public ushort GameID { get; set; }
        public ulong CompilationTime { get; set; }

        public string SourceFileName { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }
        #endregion

        #region Methods
        public abstract bool IsValid();
        #endregion
    }
}
