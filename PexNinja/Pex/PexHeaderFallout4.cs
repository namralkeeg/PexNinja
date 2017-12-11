using System;
using System.Diagnostics.Contracts;
using System.IO;
using PexNinja.IO;
using PexNinja.NumberHelpers;

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

        //public override bool Read(Stream stream)
        //{
        //    if (stream == null)
        //        throw new ArgumentNullException("stream");
        //    if (!stream.CanRead)
        //        throw new EndOfStreamException();
        //    Contract.EndContractBlock();

        //    stream.Position = 0;
        //    using (var binaryReader = new BinaryReaderEndian(stream, encoding, true, byteOrder))
        //    {
        //        Magic = binaryReader.ReadUInt32();
        //        MajorVersion = binaryReader.ReadByte();
        //        MinorVersion = binaryReader.ReadByte();
        //        GameID = binaryReader.ReadUInt16();
        //        CompilationTime = binaryReader.ReadUInt64();
        //        SourceFileName = binaryReader.ReadWString();
        //        UserName = binaryReader.ReadWString();
        //        ComputerName = binaryReader.ReadWString();
        //    }

        //    return true;
        //}

        //public override void Write(Stream stream)
        //{
        //    if (stream == null)
        //        throw new ArgumentNullException("stream");
        //    if (!stream.CanWrite)
        //        throw new EndOfStreamException();
        //    Contract.EndContractBlock();

        //    stream.Seek(0, SeekOrigin.Begin);
        //    using (var binaryWriter = new BinaryWriterEndian(stream, encoding, true, byteOrder))
        //    {
        //        binaryWriter.Write(Magic);
        //        binaryWriter.Write(MajorVersion);
        //        binaryWriter.Write(MinorVersion);
        //        binaryWriter.Write(GameID);
        //        binaryWriter.Write(CompilationTime);
        //        binaryWriter.WriteWString(SourceFileName);
        //        binaryWriter.WriteWString(UserName);
        //        binaryWriter.WriteWString(ComputerName);
        //    }
        //}
    }
}
