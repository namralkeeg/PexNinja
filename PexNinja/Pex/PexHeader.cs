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

        public override string ToString()
        {
            // Start with the unix epoch of Jan, 1, 1970.
            var compTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            // Add the total seconds to the epoch.
            compTime = compTime.AddSeconds(Convert.ToDouble(CompilationTime));

            var sb = new StringBuilder();
            sb.Append($"Magic: {Magic} ");
            sb.Append($"Major: {MajorVersion} ");
            sb.Append($"Minor: {MinorVersion} ");
            sb.Append($"Game ID: {GameID} ");
            sb.Append($"Compile: {compTime}{Environment.NewLine}");
            sb.Append($"Script Name: {SourceFileName}{Environment.NewLine}");
            sb.Append($"User Name: {UserName}{Environment.NewLine}");
            sb.Append($"Computer Name: {ComputerName}");
            return sb.ToString();
        }
        #endregion
    }
}
