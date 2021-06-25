using System.Text.Json.Serialization;

namespace BLITZZ.Content
{
    public class GameInfo
    {
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

        
    }
}
