using System.Numerics;

namespace BLITZZ.Content.Font
{
    public interface IFontGlyph
    {
        Vector2 DrawOffset { get; }
        Rect Bounds { get; }
        int HorizontalAdvance { get; }
    }
}
