using System.Numerics;

namespace BLITZZ.Content.Font
{
    internal class TrueTypeGlyph : IFontGlyph
    {
        public Rect Region;
        public Vector2 Bearing;
        public int XAdvance;

        public Vector2 DrawOffset => Bearing;

        public Rect Bounds => Region;

        public int HorizontalAdvance => XAdvance;
    }
}