using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BLITZZ.Content
{
    public class AssetsManifest
    {
        [JsonPropertyName("assets")]
        public Dictionary<string, AssetFileGroup> Assets { get; set; }
    }
}
