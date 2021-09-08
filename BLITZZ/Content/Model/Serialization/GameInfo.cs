using System.Text.Json.Serialization;

namespace BLITZZ.Content
{
    public class GameInfo
    {
        private const int DefaultResWidth = 800;

        private const int DefaultResHeight = 600;

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("width")]
        public int ResolutionWidth { get; set; }

        [JsonPropertyName("height")]
        public int ResolutionHeight { get; set; }

        [JsonPropertyName("resizable_window")]
        public bool ResizableWindow { get; set; } = false;

        [JsonPropertyName("start_fullscreen")]
        public bool StartFullscreen { get; set; }

        [JsonPropertyName("assets_folder")]
        public string AssetsFolder { get; set; }

        [JsonPropertyName("preload_asset_paks")]
        public string[] PreloadPaks { get; set; }

        public static void AssumeDefaults(ref GameInfo info)
        {
            info.AssetsFolder ??= "Assets";
            info.Title ??= "BLITZZ Game";
            if (info.ResolutionWidth <= 0)
            {
                info.ResolutionWidth = DefaultResWidth;
            }

            if (info.ResolutionHeight <= 0)
            {
                info.ResolutionHeight = DefaultResHeight;
            }
        }

        
    }
}
