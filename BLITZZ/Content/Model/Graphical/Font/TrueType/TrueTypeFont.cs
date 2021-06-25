using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
namespace BLITZZ.Content.Font
{
    public class TrueTypeFont : Font
    {

        private readonly int _height;
        private readonly int _maxBearingY;
        private readonly char[] _chars;
        private readonly TrueTypeGlyph[] _glyphs;
        private readonly CharRegion[] _regions;
        private readonly Dictionary<int, int> _kernings = new();
        private readonly Texture _atlas;
        private int _lineSpacing;
        private readonly int _defaultGlyphIndex;


        public override bool IsKerningEnabled { get; set; } = true;

        public override int LineSpacing => _lineSpacing;

        public override int Height => _height;

        public override Texture Texture => _atlas;


        internal TrueTypeFont(Texture atlas, TrueTypeFontData fontData)
        {
            _height = fontData.Size;
            _chars = fontData.Chars;
            _kernings = fontData.CharKernings;
            _atlas = atlas;
            _lineSpacing = fontData.BaseLineSpacing;
            _glyphs = new TrueTypeGlyph[_chars.Length];

            var regions = new Stack<CharRegion>();

            for (int i = 0; i < _chars.Length; ++i)
            {
                _glyphs[i] = new TrueTypeGlyph()
                {
                    Region = fontData.CharRegions[i],
                    XAdvance = fontData.CharXAdvances[i],
                    Bearing = fontData.CharBearings[i]
                };

                if (regions.Count == 0 || _chars[i] > (regions.Peek().End + 1))
                {
                    // New Region

                    regions.Push(new CharRegion(_chars[i], i));
                }
                else if (_chars[i] == (regions.Peek().End + 1))
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

            _regions = regions.ToArray();

            Array.Reverse(_regions);

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
            var maxHeight = (text.Count(c => c == '\n') + 1) * LineSpacing;

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

                width += glyph.HorizontalAdvance;
            }

            if (maxWidth < width)
                maxWidth = width;

            return new Size(maxWidth, maxHeight);
        }

        public override IFontGlyph GetGlyphOrDefault(char c)
        {
            if (TryGetGlyphIndex(c, out var glyphIdx)) return _glyphs[glyphIdx];

            return _glyphs[_defaultGlyphIndex];
        }

        public override int GetKerning(char left, char right)
        {
            var key = (left << 16) | right;
            
            _kernings.TryGetValue(key, out var kerning);
            return kerning;
        }

        protected override void FreeManagedResources()
        {
            _atlas?.Dispose();
            _kernings.Clear();
        }

        private unsafe bool TryGetGlyphIndex(char c, out int index)
        {
            // Do a binary search on char regions

            fixed (CharRegion* pRegions = _regions)
            {
                int regionIdx = -1;
                var left = 0;
                var right = _regions.Length - 1;

                while (left <= right)
                {
                    var mid = (left + right) / 2;

                    Debug.Assert(mid >= 0 && mid < _regions.Length, "Index was outside of array bounds");

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