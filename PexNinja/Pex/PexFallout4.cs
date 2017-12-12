using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text;
using PexNinja.IO;
using PexNinja.NumberHelpers;

namespace PexNinja.Pex
{
    public class PexFallout4 : PexBasic
    {
        // Fallout 4 pex files are Little-Endian.
        protected const Endian byteOrder = Endian.Little;
        private PexHeaderFallout4 header;
        public override IPexHeader Header { get => header; protected set => header = (PexHeaderFallout4)value; }

        public PexFallout4() : this(new PexHeaderFallout4()) { }
        protected PexFallout4(IPexHeader pexHeader) : base(pexHeader) { }

        public override bool IsPex(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            Contract.EndContractBlock();

            if (ReadHeader(stream))
            {
                return Header.IsValid();
            }
            else
            {
                return false;
            }
        }

        public override bool IsPex()
        {
            return Header.IsValid();
        }

        public override bool Read(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            if (!stream.CanRead)
                throw new EndOfStreamException();
            Contract.EndContractBlock();

            stream.Position = 0;
            if (ReadHeader(stream) && Header.IsValid())
            {
                var headerEndPos = stream.Position;
                var fileEndPos = stream.Length;
                var dataSize = fileEndPos - headerEndPos;
                using (var br = new BinaryReaderEndian(stream, encoding, true, byteOrder))
                {
                    Data = br.ReadBytes((int)dataSize);
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public override void Write(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            if (!stream.CanWrite)
                throw new EndOfStreamException();
            if (!Header.IsValid())
                throw new Exception("Tried to write Invalid header");
            Contract.EndContractBlock();

            stream.Position = 0;
            WriteHeader(stream);
            using (var br = new BinaryWriterEndian(stream, encoding, true, byteOrder))
            {
                br.Write(Data);
            }
        }

        public override bool ReadHeader(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            if (!stream.CanRead)
                throw new EndOfStreamException();
            Contract.EndContractBlock();

            stream.Position = 0;
            using (var binaryReader = new BinaryReaderEndian(stream, encoding, true, byteOrder))
            {
                Header.Magic = binaryReader.ReadUInt32();
                Header.MajorVersion = binaryReader.ReadByte();
                Header.MinorVersion = binaryReader.ReadByte();
                Header.GameID = binaryReader.ReadUInt16();
                if (!Header.IsValid())
                {
                    return false;
                }
                Header.CompilationTime = binaryReader.ReadUInt64();
                Header.SourceFileName = binaryReader.ReadWString();
                Header.UserName = binaryReader.ReadWString();
                Header.ComputerName = binaryReader.ReadWString();
            }

            return true;
        }

        public override void WriteHeader(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            if (!stream.CanWrite)
                throw new EndOfStreamException();
            Contract.EndContractBlock();

            stream.Position = 0;
            using (var binaryWriter = new BinaryWriterEndian(stream, encoding, true, byteOrder))
            {
                binaryWriter.Write(Header.Magic);
                binaryWriter.Write(Header.MajorVersion);
                binaryWriter.Write(Header.MinorVersion);
                binaryWriter.Write(Header.GameID);
                binaryWriter.Write(Header.CompilationTime);
                // Remove the full path before the file name before writing.
                // No idea why Bethesda in their infinte wisdom decided to add the path from the
                // temporary folder to the source file?
                binaryWriter.WriteWString(Path.GetFileName(Header.SourceFileName));
                binaryWriter.WriteWString(Header.UserName);
                binaryWriter.WriteWString(Header.ComputerName);
            }
        }

        public override string ToString()
        {
            return header.ToString();
        }
    }
}
