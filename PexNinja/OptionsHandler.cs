using System;
using System.IO;
using System.Reflection;
using System.Text;
using Mono.Options;
using Newtonsoft.Json;

namespace PexNinja
{
    public class OptionsHandler
    {
        private string configFileName;
        private string programName;
        private string programPath;
        private string versionNumber;
        private OptionSet genericOptionSet;
        private OptionSet configOptionSet;
        public Options ProgramOptions { get; protected set; }

        public OptionsHandler()
        {
            Assembly assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
            versionNumber = assembly.GetName().Version.ToString();
            programPath = Path.GetDirectoryName(assembly.Location);
            programName = Path.GetFileName(assembly.Location);
            configFileName = Path.ChangeExtension(programName, ".json");
            ProgramOptions = new Options();

            configOptionSet = new OptionSet
            {
                {"c|config=",$"Use configuration file. Defaults to {configFileName}", c => configFileName = c },
            };

            genericOptionSet = new OptionSet
            {
                {"h|help", "Help Screen", h => ProgramOptions.ShowHelp = h != null },
                {"c|config=",$"Use configuration file. Defaults to {configFileName}", c => configFileName = c },
                {"version", "Show application version information.", v => ProgramOptions.ShowVersion = v != null },
                {"s|source=", "Source Folder(s), defaults to current folder.", s => ProgramOptions.SourceFolders.Add(s) },
                {"b|backup", "Enables the creation of backup Files.", b => ProgramOptions.DoBackup = b != null },
                {"m|mask=", "Character to mask computer and user name. Defaults to *", m => ProgramOptions.Mask = m.Substring(0,1) },
                {"r|recursive", "Recursively process all subfolders.", r => ProgramOptions.Recursive = r != null },
                {"verbose", "Enables verbose output mode.", v => ProgramOptions.Verbose = v != null }
            };
        }

        public bool ProcessOptions(string[] args)
        {
            try
            {
                var extras = configOptionSet.Parse(args);
                string fullConfigFile = Path.Combine(programPath, configFileName);

                if (File.Exists(fullConfigFile))
                {
                    JsonConvert.PopulateObject(File.ReadAllText(fullConfigFile), ProgramOptions);
                }

                extras = genericOptionSet.Parse(args);
                if (extras.Count > 0)
                {
                    ProgramOptions.SourceFolders.AddRange(extras);
                }

                if (ProgramOptions.SourceFolders.Count == 0)
                {
                    ProgramOptions.SourceFolders.Add(".");
                }

                return true;
            }
            catch (OptionException ex)
            {
                Console.Error.WriteLine(ex.Message);
                return false;
            }
        }

        public void ShowHelp()
        {
            Console.WriteLine(GetHelp());
        }

        public void ShowVersion()
        {
            Console.WriteLine(GetVersion());
        }

        public string GetVersion()
        {
            return $"{programName} .pex File Anonymizer V{versionNumber}";
        }

        public string GetHelp()
        {
            var sb = new StringBuilder();

            sb.Append($"Usage: {programName} [OPTIONS]+ [path]+{Environment.NewLine}");
            sb.Append($"Anonymizes Papyrus compiled script (.pex) files.{Environment.NewLine}");
            sb.Append($"Skyrim, SkyrimSE, and Fallout 4 files supported.{Environment.NewLine}");
            sb.Append($"If no path is specified, defaults to the current folder.{Environment.NewLine}");
            sb.Append(Environment.NewLine);
            sb.Append($"Options:{Environment.NewLine}");

            using (var stream = new MemoryStream())
            {
                var streamWriter = new StreamWriter(stream, Encoding.Default);
                genericOptionSet.WriteOptionDescriptions(streamWriter);
                streamWriter.Flush();

                stream.Position = 0;
                var streamReader = new StreamReader(stream, Encoding.Default);
                sb.Append(streamReader.ReadToEnd());
            }

            return sb.ToString();
        }
    }
}
