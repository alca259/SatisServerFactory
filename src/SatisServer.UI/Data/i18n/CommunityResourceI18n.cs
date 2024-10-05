using Newtonsoft.Json;

namespace SatisServer.UI.Data.i18n;

/// <summary>Information about all resources in the game</summary>
public sealed class CommunityResourceI18N
{
    /// <summary>Array of all resources</summary>
    public ClassInfo[] AllResources { get; set; } = [];

    /// <summary>Load the resource information from a specific language</summary>
    public static CommunityResourceI18N FromLanguage(string languageID)
    {
        var path = AppDomain.CurrentDomain.BaseDirectory;
        var filePath = Path.Combine(path, "Resources", "i18n", $"{languageID}.json");
        if (!File.Exists(filePath))
        {
            return new CommunityResourceI18N();
        }

        var json = File.ReadAllText(filePath);
        var resources = JsonConvert.DeserializeObject<ClassInfo[]>(json)!;
        return new CommunityResourceI18N { AllResources = resources };
    }

    /// <summary>Get all schematics from the resource information</summary>
    public List<SchematicInfo> GetAllSchematics()
    {
        List<SchematicInfo> schematics = [];

        schematics.AddRange(AllResources
            .Where(x => ClassInfo.NATIVE_CLASS_SCHEMATIC.ContainsIgnoreCase(x.NativeClass))
            .SelectMany(x => x.Classes)
            .Where(x => SchematicInfo.MTYPE_MILESTONE.ContainsIgnoreCase(x.SchematicType))
            .ToList());

        return schematics;
    }

    /// <summary>Information about a specific resource</summary>
    public sealed class ClassInfo
    {
        /// <summary>Native class filter for schematics</summary>
        internal const string NATIVE_CLASS_SCHEMATIC = "/Script/CoreUObject.Class'/Script/FactoryGame.FGSchematic'";

        /// <summary>Native class of the resource</summary>
        public string? NativeClass { get; set; }
        /// <summary>Array of all schematics</summary>
        public SchematicInfo[] Classes { get; set; } = [];
    }

    /// <summary>Information about a specific schematic</summary>
    public sealed class SchematicInfo
    {
        /// <summary>Filter for milestone schematics</summary>
        internal const string MTYPE_MILESTONE = "EST_Milestone";

        /// <summary>Class name of the schematic</summary>
        public string? ClassName { get; set; }
        /// <summary>Full name of the schematic</summary>
        public string? FullName { get; set; }
        /// <summary>Type of the schematic</summary>
        [JsonProperty("mType")]
        public string? SchematicType { get; set; }
        /// <summary>Display name of the schematic</summary>
        [JsonProperty("mDisplayName")]
        public string? DisplayName { get; set; }
        /// <summary>Description of the schematic</summary>
        [JsonProperty("mDescription")]
        public string? Description { get; set; }
        /// <summary>Menu priority of the schematic</summary>
        [JsonProperty("mMenuPriority")]
        public string? PriorityOrder { get; set; }
        /// <summary>Tech tier of the schematic</summary>
        [JsonProperty("mTechTier")]
        public string? TechTier { get; set; }
    }
}