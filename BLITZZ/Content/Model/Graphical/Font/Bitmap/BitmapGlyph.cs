using System.Numerics;

namespace BLITZZ.Content.Font
{
    internal class BitmapGlyph : IFontGlyph
    {
        public char CodePoint { get; internal set; }

        public int BitmapX { get; internal set; }
        public int BitmapY { get; internal set; }
        public int Width { get; internal set; }
        public int Height { get; internal set; }

        public int OffsetX { get; internal set; }
        public int OffsetY { get; internal set; }

        public int HorizontalAdvance { get; internal set; }
        public int Page { get; internal set; }
        public int Channel { get; internal set; }

        public Vector2 DrawOffset => new Vector2(OffsetX, OffsetY);

        public Rect Bounds => Rect.FromBox(BitmapX, BitmapY, Width, Height);
    }
}