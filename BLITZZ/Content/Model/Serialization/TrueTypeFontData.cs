using MessagePack;
using System.Collections.Generic;

namespace BLITZZ.Content
{
    [MessagePackObject]
    public class TrueTypeFontData
    {
        [Key(0)]
        public string Id { get; set; }

        [Key(1)]
        public int Size { get; set; }

        [Key(2)]
        public int LineSpacing;

        [Key(3)]
        public ImageData FontSheet;

        [Key(4)]
        public char[] Chars;

        [Key(5)]
        public SRect[] CharRegions;

        [Key(6)]
        public SVector2[] CharBearings;

        [Key(7)]
        public int[] CharXAdvances;

        [Key(8)]
        public Dictionary<int,int> CharKernings;

        
    }
}
