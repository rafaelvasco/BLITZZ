namespace BLITZZ.Content.Font
{
    public abstract class Font : Asset
    {
        public abstract Texture Texture { get; }
        
        public abstract int Height { get; }

        public abstract int LineSpacing { get; }

        public abstract bool IsKerningEnabled { get; set; }
        
        public abstract Size Measure(string s);

        public abstract IFontGlyph GetGlyphOrDefault(char c);

        public abstract int GetKerning(char left, char right);

    }
}