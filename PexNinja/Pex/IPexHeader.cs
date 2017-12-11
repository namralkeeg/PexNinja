namespace PexNinja.Pex
{
    public interface IPexHeader
    {
        ulong CompilationTime { get; set; }
        string ComputerName { get; set; }
        ushort GameID { get; set; }
        uint Magic { get; set; }
        byte MajorVersion { get; set; }
        byte MinorVersion { get; set; }
        string SourceFileName { get; set; }
        string UserName { get; set; }

        bool IsValid();
    }
}