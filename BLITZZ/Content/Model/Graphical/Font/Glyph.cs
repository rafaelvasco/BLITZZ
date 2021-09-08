using System.Numerics;

namespace BLITZZ.Content.Font
{
    public class Glyph
    {
        public Rect Region { get; init; }
        public Vector2 Offset { get; init; }
        public float XAdvance { get;  init; }
    }
}
