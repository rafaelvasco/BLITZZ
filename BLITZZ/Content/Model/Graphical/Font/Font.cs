namespace BLITZZ.Content.Font
{
    public abstract class Font : Asset
    {
        protected CharRegion[] Regions { get; init; }

        public Glyph[] Glyphs { get; protected set; }

        public Texture Texture { get; protected set; }
        public int Height { get; protected set; }

        public float LineSpacing { get; protected set; }

        public bool IsKerningEnabled { get; set; }
        
        public abstract Size Measure(string s);

        public abstract Glyph GetGlyphOrDefault(char c);

        public abstract float GetKerning(char left, char right);

    }
}