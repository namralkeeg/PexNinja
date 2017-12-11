using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;

namespace PexNinja
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Options
    {
        [DefaultValue(".bak")]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string BackupExtension { get; set; } = ".bak";

        [JsonProperty]
        public string ComputerName { get; set; }

        [DefaultValue(false)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public bool DoBackup { get; set; }

        [DefaultValue("*")]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string Mask { get; set; } = "*";

        [DefaultValue(false)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public bool Recursive { get; set; } 

        [DefaultValue(false)]
        public bool ShowHelp { get; set; }

        [DefaultValue(false)]
        public bool ShowVersion { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> SourceFolders { get; set; } = new List<string>();

        [JsonProperty]
        public string UserName { get; set; }

        [DefaultValue(false)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public bool Verbose { get; set; }

        [DefaultValue(new string[] { ".pex" })]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string[] ValidExtensions { get; set; } = new string[] { ".pex" };
    }
}
