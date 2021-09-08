using System.Collections.Generic;
using System.Text.Json.Serialization;
using BLITZZ.Content.Font;

namespace BLITZZ.Content
{
    public class CommonAssetInfo
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("path")]
        public string Path { get; set; }
    }

    public class ImageAssetInfo
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("path")]
        public string Path { get; set; }

    }


    public class FontAssetInfo
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("path")]
        public string Path { get; set; }

        [JsonPropertyName("size")]
        public int Size { get; set; }

        [JsonPropertyName("line_space")]
        public  int LineSpace { get; set; }

        [JsonPropertyName("ranges")]
        public string[] CharRanges { get; set; }

        [JsonPropertyName("use_hinting")]
        public bool UseHinting { get; set; }

        [JsonPropertyName("auto_hinting")]
        public bool UseAutoHinting { get; set; }

        [JsonPropertyName("hint_mode")]
        public HintingMode HintMode;

        [JsonPropertyName("kerning_mode")]
        public KerningMode KerningMode;
    }

    public class BitmapFontAssetInfo
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("path")]
        public string Path { get; set; }
    }

    public class AtlasAssetInfo
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("path")]
        public string Path { get; set; }

        [JsonPropertyName("regions")]
        public Dictionary<string, (int X, int Y, int Width, int Height)> Regions { get; set; }

    }

    public class ShaderAssetInfo
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("vs_path")]
        public string VsPath { get; set; }

        [JsonPropertyName("fs_path")]
        public string FsPath { get; set; }
    }

    public class AudioFileAssetInfo
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("path")]
        public string Path { get; set; }

        [JsonPropertyName("path")]
        public bool Streamed { get; set; }
    }
}
