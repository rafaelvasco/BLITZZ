using System.Text.Json.Serialization;

namespace BLITZZ.Content
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AssetType
    {
        Image,
        Atlas,
        Font,
        Shader,
        Sfx,
        Song,
        TextFile,
        Unknown
    }
}
