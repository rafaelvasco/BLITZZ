using MessagePack;
using System.Collections.Generic;

namespace BLITZZ.Content
{
    [MessagePackObject]
    public class TextureAtlasData
    {
        [Key(0)]
        public string Id { get; set; }

        [Key(1)]
        public byte[] Data { get; set; }

        [Key(2)]
        public int Width { get; set; }

        [Key(3)]
        public int Height { get; set; }

        [Key(4)]
        public Dictionary<string, (int X, int Y, int Width, int Height)> Atlas { get;set; }

        [Key(5)]
        public bool RuntimeUpdatable { get;set;}
    }
}
