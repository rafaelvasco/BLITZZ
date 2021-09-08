using System;
using System.Collections.Generic;
using System.Linq;

namespace BLITZZ.Content.Font
{
    public class BitmapFont : Font
    {
        private readonly List<BitmapFontKerningPair> _kernings;

        public BitmapFont(Texture atlas, BitmapFontData fontData)
        {
            var chars = fontData.Chars;
            
            Height = fontData.Size;
            Texture = atlas;
            LineSpacing = fontData.LineSpacing;
            Glyphs = new Glyph[chars.Length];

            _kernings = new List<BitmapFontKerningPair>(fontData.GlyphKernings.Length);

            foreach (var (First, Second, Amount) in fontData.GlyphKernings)
            {
                _kernings.Add((new BitmapFontKerningPair(First, Second, Amount)));
            }

            var regions = new Stack<CharRegion>();

            for (int i = 0; i < chars.Length; ++i)
            {
                Glyphs[i] = new Glyph()
                {
                    Region = fontData.GlyphRegions[i],
                    XAdvance = fontData.GlyphXAdvances[i],
                    Offset = fontData.GlyphOffsets[i]
                };

                if (regions.Count == 0 || chars[i] > (regions.Peek().End + 1))
                {
                    // New Region

                    regions.Push(new CharRegion(chars[i], i));
                }
                else if (chars[i] == (regions.Peek().End + 1))
                {
                    // Add char in current region

                    var currentRegion = regions.Pop();

                    currentRegion.End++;
                    regions.Push(currentRegion);
                }
                else
                {
                    throw new InvalidOperationException(
                        "Invalid TrueTypeFontData. Character map must be in ascending order.");
                }

            }

        }


        public override Glyph GetGlyphOrDefault(char c)
        {
            throw new NotImplementedException();
        }

        public override float GetKerning(char first, char second)
            => _kernings.FirstOrDefault(x => x.First == first && x.Second == second).Amount;

        public override Size Measure(string s)
        {
            var maxWidth = 0;
            var width = 0;
            var height = (int)LineSpacing;

            for (var i = 0; i < s.Length; i++)
            {
                var c = s[i];

                if (c == '\n')
                {
                    height += (int)LineSpacing;

                    if (width > maxWidth)
                        maxWidth = width;

                    width = 0;
                    continue;
                }

                var glyph = GetGlyphOrDefault(c);

                width += (int)glyph.XAdvance;
            }

            if (width > maxWidth)
                maxWidth = width;

            return new Size(maxWidth, height);
        }


        protected override void FreeNativeResources()
        {
            Texture.Dispose();
        }

    }
}