using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace BLITZZ.Content.Font
{
    public class TrueTypeFont : Font
    {

        private readonly Dictionary<int, float> _kernings;
        private readonly int _defaultGlyphIndex;


        internal TrueTypeFont(Texture atlas, TrueTypeFontData fontData)
        {
            var chars = fontData.Chars;
            
            Height = fontData.Size;
            Texture = atlas;
            LineSpacing = fontData.LineSpacing;
            Glyphs = new Glyph[chars.Length];
            _kernings = fontData.GlyphKernings;

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

            Regions = regions.ToArray();

            Array.Reverse(Regions);

            if(!TryGetGlyphIndex('?', out _defaultGlyphIndex)) {
                if (!TryGetGlyphIndex(' ', out _defaultGlyphIndex))
                {
                    throw new Exception("Missing '?' or ' ' characteers on font");
                }
            }
        }

        public override Size Measure(string text)
        {
            var width = 0;

            var maxWidth = width;
            var maxHeight = (int)((text.Count(c => c == '\n') + 1) * LineSpacing);

            foreach (var c in text)
            {
                if (c == '\n')
                {
                    if (maxWidth < width)
                        maxWidth = width;

                    width = 0;
                    continue;
                }


                var glyph = GetGlyphOrDefault(c);

                width += (int)glyph.XAdvance;
            }

            if (width > maxWidth)
                maxWidth = width;

            return new Size(maxWidth, maxHeight);
        }

        public override Glyph GetGlyphOrDefault(char c)
        {
            return TryGetGlyphIndex(c, out var glyphIdx) ? Glyphs[glyphIdx] : Glyphs[_defaultGlyphIndex];
        }

        public override float GetKerning(char left, char right)
        {
            var key = (left << 16) | right;
            
            _kernings.TryGetValue(key, out var kerning);
            return kerning;
        }

        protected override void FreeManagedResources()
        {
            _kernings.Clear();
        }

        protected override void FreeNativeResources()
        {
            Texture?.Dispose();
        }

        private unsafe bool TryGetGlyphIndex(char c, out int index)
        {
            // Do a binary search on char regions

            fixed (CharRegion* pRegions = Regions)
            {
                int regionIdx = -1;
                var left = 0;
                var right = Regions.Length - 1;

                while (left <= right)
                {
                    var mid = (left + right) / 2;

                    Debug.Assert(mid >= 0 && mid < Regions.Length, "Index was outside of array bounds");

                    if (pRegions[mid].End < c)
                    {
                        left = mid + 1;
                    }
                    else if (pRegions[mid].Start > c)
                    {
                        right = mid - 1;
                    }
                    else
                    {
                        regionIdx = mid;
                        break;
                    }
                }

                if (regionIdx == -1)
                {
                    index = -1;
                    return false;
                }

                index = pRegions[regionIdx].StartIndex + (c - pRegions[regionIdx].Start);
            }

            return true;
        }


    }
}