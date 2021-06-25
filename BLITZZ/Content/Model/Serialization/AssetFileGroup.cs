using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BLITZZ.Content
{
    public class AssetFileGroup
    {
        [JsonPropertyName("images")]
        public List<ImageAssetInfo> Images { get; set; }

        [JsonPropertyName("shaders")]
        public List<ShaderAssetInfo> Shaders { get; set; }

        [JsonPropertyName("text_files")]
        public List<CommonAssetInfo> TextFiles { get; set; }

        [JsonPropertyName("fonts")]
        public List<FontAssetInfo> Fonts { get; set; }

        [JsonPropertyName("atlases")]
        public List<AtlasAssetInfo> Atlases { get; set; }

        [JsonPropertyName("audio_files")]
        public List<AudioFileAssetInfo> AudioFiles { get; set; }

        public int AssetsCount()
        {
            int total = 0;

            total += Images?.Count ?? 0;
            total += Fonts?.Count ?? 0;
            total += Shaders?.Count ?? 0;
            total += TextFiles?.Count ?? 0;
            total += Atlases?.Count ?? 0;
            total += AudioFiles?.Count ?? 0;

            return total;
        }
    }
}
