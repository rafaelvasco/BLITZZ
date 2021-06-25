using System.Collections.Generic;
using System.Text.Json.Serialization;

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

    public class FontFaceAssetInfo
    {
        [JsonPropertyName("path")]
        public string Path { get; set; }

        [JsonPropertyName("size")]
        public int Size { get; set; }

        [JsonPropertyName("char_ranges")]
        public string[] CharRanges { get; set; }
    }

    public class FontAssetInfo
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("faces")]
        public FontFaceAssetInfo[] Faces { get; set; }

        [JsonPropertyName("line_spacing")]
        public int LineSpacing { get; set; }

        [JsonPropertyName("spacing")]
        public int Spacing { get; set; }

        [JsonPropertyName("default_char")]
        public char DefaultChar { get; set; }
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
