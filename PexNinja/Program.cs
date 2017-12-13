using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PexNinja.Pex;

namespace PexNinja
{
    class Program
    {
        static void Main(string[] args)
        {
            var optionsHandler = new OptionsHandler();

            if (!optionsHandler.ProcessOptions(args))
            {
                optionsHandler.ShowHelp();
                return;
            }

            if (optionsHandler.ProgramOptions.ShowVersion)
            {
                optionsHandler.ShowVersion();
                Console.WriteLine();
                if (!optionsHandler.ProgramOptions.ShowHelp)
                    return;
            }

            if (optionsHandler.ProgramOptions.ShowHelp)
            {
                optionsHandler.ShowHelp();
                return;
            }

            var entries = GetValidFiles(optionsHandler.ProgramOptions);
            foreach (var entry in entries)
            {
                Console.WriteLine(entry);
                if (optionsHandler.ProgramOptions.Game == null)
                {
                    using (IPex pex = GetPex(entry))
                    {
                        if (pex != null)
                        {
                            Console.WriteLine(pex.ToString());
                        }
                    }
                }
                else
                {
                    using (IPex pex = GetPex(entry, optionsHandler.ProgramOptions.Game))
                    {
                        if (pex != null)
                        {
                            Console.WriteLine(pex.ToString());
                        }
                    }
                }
            }

        }

        private static bool CreateBackupFile(string fileName, string backupExtension)
        {
            if (fileName == null)
                throw new ArgumentNullException("fileName");
            if (backupExtension == null)
                throw new ArgumentNullException("backupExtension");
            Contract.EndContractBlock();

            if (File.Exists(fileName))
            {
                var backupFile = Path.ChangeExtension(fileName, backupExtension);
                try
                {
                    File.Copy(fileName, backupFile);
                    return true;
                }
                catch (IOException ex)
                {
                    Console.Error.WriteLine(ex.Message);
                    return false;
                }
            }

            return false;
        }

        private static bool RemoveBackupFile(string fileName, string backupExtension)
        {
            if (fileName == null)
                throw new ArgumentNullException("fileName");
            if (backupExtension == null)
                throw new ArgumentNullException("backupExtension");
            Contract.EndContractBlock();

            var backupFile = Path.ChangeExtension(fileName, backupExtension);
            if (File.Exists(backupFile))
            {
                try
                {
                    File.Delete(backupFile);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex.Message);
                    return false;
                }
            }

            return false;
        }

        private static IPex GetPex(string fileName)
        {
            if (fileName == null)
                throw new ArgumentNullException("fileName");
            Contract.EndContractBlock();

            IPex pex = null;

            if (File.Exists(fileName))
            {
                using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    pex = PexFactory.GetPex(fs);
                    if (pex != null)
                    {
                        pex.Read(fs);
                    }
                }
            }

            return pex;
        }

        private static IPex GetPex(string fileName, string game)
        {
            if (fileName == null)
                throw new ArgumentNullException("fileName");
            if (game == null)
                throw new ArgumentNullException("game");
            Contract.EndContractBlock();

            IPex pex = null;

            pex = PexFactory.GetPex(game);
            if ((pex != null) && File.Exists(fileName))
            {
                using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    pex.Read(fs);
                }
            }

            return pex;
        }

        private static List<string> GetValidFiles(Options options)
        {
            if (options == null)
                throw new ArgumentNullException("options");
            Contract.EndContractBlock();

            var validFiles = new List<string>();
            var searchOption = (options.Recursive) ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            foreach (var folder in options.SourceFolders)
            {
                if (Directory.Exists(folder))
                {
                    foreach (var extension in options.ValidExtensions)
                    {
                        validFiles.AddRange(Directory.EnumerateFiles(folder, $"*{extension}", searchOption));
                    }
                }
            }

            validFiles.Sort();
            return validFiles;
        }
    }
}
