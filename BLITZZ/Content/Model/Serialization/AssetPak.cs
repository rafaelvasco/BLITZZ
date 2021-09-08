using MessagePack;
using System.Collections.Generic;
using BLITZZ.Content.Font;

namespace BLITZZ.Content
{
    [MessagePackObject]
    public class AssetPak
    {

        [Key(0)]
        public string Name { get; set; }

        [Key(1)]
        public Dictionary<string, ImageData> Images { get; set; }

        [Key(2)]
        public Dictionary<string, TextureAtlasData> Atlases { get; set; }

        [Key(3)]
        public Dictionary<string, ShaderProgramData> Shaders { get; set; }

        [Key(4)]
        public Dictionary<string, TrueTypeFontData> Fonts { get; set; }

        [Key(5)]
        public Dictionary<string, BitmapFontData> BitmapFonts { get; set; }

        [Key(6)]
        public Dictionary<string, TextFileData> TextFiles { get; set; }

        [Key(7)]
        public Dictionary<string, AudioFileData> AudioFiles { get; set; }

        [Key(8)]
        public int TotalResourcesCount { get; set; }

        public AssetPak(string name)
        {
            Name = name;
            Images = new Dictionary<string, ImageData>();
            Atlases = new Dictionary<string, TextureAtlasData>();
            Shaders = new Dictionary<string, ShaderProgramData>();
            Fonts = new Dictionary<string, TrueTypeFontData>();
            BitmapFonts = new Dictionary<string, BitmapFontData>();
            TextFiles = new Dictionary<string, TextFileData>();
            AudioFiles = new Dictionary<string, AudioFileData>();
            TotalResourcesCount = 0;
        }

    }
}
