using System.Collections.Generic;
using MessagePack;

namespace BLITZZ.Content.Font
{

    [MessagePackObject]
    public class BitmapFontData
    {
        [Key(0)]
        public string Id { get; set; }

        [Key(1)]
        public int Size { get; set; }

        [Key(2)]
        public float LineSpacing;

        [Key(3)]
        public ImageData FontSheet;

        [Key(4)]
        public char[] Chars;

        [Key(5)]
        public SRect[] GlyphRegions;

        [Key(6)]
        public SVector2[] GlyphOffsets;

        [Key(7)]
        public float[] GlyphXAdvances;

        [Key(8)]
        public (char First, char Second, float Amount)[] GlyphKernings;
    }
}
