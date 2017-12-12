using System;
using System.IO;

namespace PexNinja.Pex
{
    public interface IPex : IDisposable
    {
        byte[] Data { get; }
        IPexHeader Header { get; }

        bool IsPex();
        bool IsPex(Stream stream);
        bool Read(Stream stream);
        void Write(Stream stream);
        bool ReadHeader(Stream stream);
        void WriteHeader(Stream stream);
    }
}