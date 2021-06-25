using FreeTypeSharp;
using FreeTypeSharp.Native;
using System;
using System.Collections.Generic;
using System.IO;

namespace BLITZZ.Content.Font
{
    public struct FontBuildProps
    {

        public int Size;
        public bool KerningEnabled;
        public Range[] CharRanges;
        public bool UseHinting;
        public bool UseAutoHinting;
        public HintingMode HintMode;
        public KerningMode KerningMode;

    }

    public static class TrueTypeFontBuilder
    {
        private static FreeTypeLibrary _library;

        public static unsafe TrueTypeFontData Build(string id, string relativePath, FontBuildProps buildProps) 
        {
            _library = new FreeTypeLibrary();

            TrueTypeFontData result = new ();

            FreeTypeFaceFacade freeTypeFontData;
            
            // =============================================================
            // =============================================================

            void LoadFreeType(byte[] fontData)
            {
                fixed (byte* fontPtr = &fontData[0])
                {
                    freeTypeFontData = new FreeTypeFaceFacade(_library, new IntPtr(fontPtr), fontData.Length);
                }
            }

            // =============================================================
            // =============================================================

            void UnloadFreeType()
            {
                FT.FT_Done_Face(freeTypeFontData.Face);
            }

            // =============================================================
            // =============================================================

            void PopulateChars()
            {
                char[] wantedChars;

                var validChars = new List<char>();

                if (buildProps.CharRanges != null)
                {
                    wantedChars = GenerateCharArrayFromRanges(buildProps.CharRanges);
                }
                else
                {
                    wantedChars = GenerateCharArrayFromRanges(1..512);
                }

                for (int i = 0; i < wantedChars.Length; ++i)
                {
                    if(FontFileContainsGlyphChar(wantedChars[i]))
                    {
                        validChars.Add(wantedChars[i]);
                    }
                }

                validChars.Sort();

                result.Chars = validChars.ToArray();
            }

            bool FontFileContainsGlyphChar(char c)
            {
                unsafe
                {
                    return FT.FT_Get_Char_Index(freeTypeFontData.Face, c) != 0;
                }
            }

            char[] GenerateCharArrayFromRanges(params Range[] ranges)
            {
                int length = 0;

                for (int i = 0; i < ranges.Length; ++i)
                {
                    length += ranges[i].End.Value - ranges[i].Start.Value + 1;
                }

                var result = new char[length];

                int index = 0;

                foreach (var range in ranges)
                {
                    for (var c = (char)range.Start.Value; c < (char)range.End.Value; c++)
                        result[index++] = c;
                }

                return result;
            }

            // =============================================================
            // =============================================================

            void SetFontPixelSize()
            {
                int matchingSize = freeTypeFontData.FindNearestMatchingPixelSize(buildProps.Size);
                FT.FT_Set_Pixel_Sizes(freeTypeFontData.Face, 0, (uint)matchingSize);
                result.Size = matchingSize;
                result.LineSpacing = freeTypeFontData.GlyphMetricHeight;
            }

            // =============================================================
            // =============================================================

            void PopulateKernelInfo()
            {
                result.CharKernings = new Dictionary<int, int>();

                for (var i = 0; i < result.Chars.Length; ++i)
                {
                    var left = result.Chars[i];
                    var right = result.Chars[result.Chars.Length - i - 1];

                    var key = (left << 16) | right;

                    var amount = GetTtfKerning(left, right);
                    result.CharKernings.Add(key, amount);
                }
            }

            unsafe int GetTtfKerning(char left, char right)
            {
                var leftIndex = FT.FT_Get_Char_Index(freeTypeFontData.Face, left);
                var rightIndex = FT.FT_Get_Char_Index(freeTypeFontData.Face, right);

                if (leftIndex == 0 || rightIndex == 0)
                    return 0;

                FT.FT_Get_Kerning(freeTypeFontData.Face, leftIndex, rightIndex, (uint)buildProps.KerningMode, out var kerning);
                return kerning.x.ToInt32() >> 6;
            }

            // =============================================================
            // =============================================================

            unsafe void PopulateAtlasAndGlyphs()
            {
                var chars = result.Chars;

                result.CharXAdvances = new int[chars.Length];
                result.CharBearings = new SVector2[chars.Length];
                result.CharRegions = new SRect[chars.Length];

                var maxDim = MathF.Ceiling(MathF.Sqrt(chars.Length));

                var imageWidth = 1;

                while (imageWidth < maxDim)
                    imageWidth <<= 1;

                var imageHeight = imageWidth;

                var imageBytesLength = imageWidth * imageHeight * 4;
                var pixelData = new byte[imageBytesLength];

                int maxBearingY = 0;

                fixed (byte* pixels = &pixelData[0])
                {
                    var penX = 0;
                    var penY = 0;

                    for (var i = 0; i < chars.Length; i++)
                    {
                        var character = chars[i];


                        var glyphFlags = FT.FT_LOAD_RENDER | FT.FT_LOAD_PEDANTIC;
                        var renderMode = FT_Render_Mode.FT_RENDER_MODE_NORMAL;

                        if (buildProps.UseHinting)
                        {
                            if (buildProps.UseAutoHinting)
                            {
                                glyphFlags |= FT.FT_LOAD_FORCE_AUTOHINT;
                            }

                            switch (buildProps.HintMode)
                            {
                                case HintingMode.Normal:
                                    glyphFlags |= FT.FT_LOAD_TARGET_NORMAL;
                                    break;

                                case HintingMode.Light:
                                    glyphFlags |= FT.FT_LOAD_TARGET_LIGHT;
                                    renderMode = FT_Render_Mode.FT_RENDER_MODE_LIGHT;
                                    break;

                                case HintingMode.Monochrome:
                                    glyphFlags |= FT.FT_LOAD_TARGET_MONO;
                                    glyphFlags |= FT.FT_LOAD_MONOCHROME;

                                    renderMode = FT_Render_Mode.FT_RENDER_MODE_MONO;
                                    break;

                                default: throw new InvalidOperationException("Unsupported hinting mode.");
                            }
                        }
                        else
                        {
                            glyphFlags |= FT.FT_LOAD_NO_HINTING;
                        }

                        var index = FT.FT_Get_Char_Index(freeTypeFontData.Face, character);
                        FT.FT_Load_Glyph(freeTypeFontData.Face, index, glyphFlags);
                        FT.FT_Render_Glyph((IntPtr)freeTypeFontData.FaceRec->glyph, renderMode);

                        FT_Bitmap bmp = freeTypeFontData.FaceRec->glyph->bitmap;

                        if (penX + bmp.width >= imageWidth)
                        {
                            penX = 0;
                            penY += ((freeTypeFontData.GlyphMetricHeight) + 1);
                        }

                        RenderGlyphToBitmap(bmp, penX, penY, imageWidth, pixels);

                        result.CharRegions[i] = new SRect(penX, penY, (int)bmp.width, (int)bmp.rows);
                        result.CharBearings[i] = new SVector2(freeTypeFontData.GlyphHoriBearingX, freeTypeFontData.GlyphHoriBearingY);
                        result.CharXAdvances[i] = freeTypeFontData.FaceRec->glyph->advance.x.ToInt32() >> 6;

                        if (result.CharBearings[i].Y > maxBearingY)
                        {
                            maxBearingY = (int)result.CharBearings[i].Y;
                        }

                        penX += (int)bmp.width + 1;
                    }

                    result.FontSheet = new ImageData()
                    {
                        Id = id + "_sheet",
                        Width = imageWidth, 
                        Height = imageHeight,
                        Data = ConvertFontPixelData(pixels, imageWidth, imageHeight)
                    };
                }

                for (int i = 0; i < result.CharBearings.Length; ++i)
                {
                    result.CharBearings[i] = new SVector2(result.CharBearings[i].X, -result.CharBearings[i].Y + maxBearingY);
                }
            }

            unsafe void RenderGlyphToBitmap(FT_Bitmap bmp, int penX, int penY, int texWidth, byte* pixels)
            {
                var buffer = (byte*)freeTypeFontData.GlyphBitmapPtr;

                for (var row = 0; row < bmp.rows; ++row)
                {
                    for (var col = 0; col < bmp.width; ++col)
                    {
                        var x = penX + col;
                        var y = penY + row;

                        if (buildProps.UseHinting && buildProps.HintMode == HintingMode.Monochrome)
                        {
                            pixels[y * texWidth + x] =
                                IsMonochromeBitSet(freeTypeFontData.FaceRec->glyph, col, row) ? (byte)0xFF : (byte)0x00;
                        }
                        else
                        {
                            pixels[y * texWidth + x] = buffer[row * bmp.pitch + col];
                        }
                    }
                }
            }

            byte[] ConvertFontPixelData(byte* pixels, int texWidth, int texHeight)
            {
                var surfaceSize = texWidth * texHeight * 4;

                var managedSurfaceData = new byte[surfaceSize];
                fixed (byte* surfaceData = &managedSurfaceData[0])
                {
                    for (var i = 0; i < texWidth * texHeight; ++i)
                    {
                        surfaceData[i * 4 + 0] = 0xFF;
                        surfaceData[i * 4 + 1] = 0xFF;
                        surfaceData[i * 4 + 2] = 0xFF;
                        surfaceData[i * 4 + 3] |= pixels[i];
                    }

                }

                return managedSurfaceData;
            }

            unsafe bool IsMonochromeBitSet(FT_GlyphSlotRec* glyph, int x, int y)
            {
                var pitch = glyph->bitmap.pitch;
                var buf = (byte*)glyph->bitmap.buffer.ToPointer();

                var row = &buf[pitch * y];
                var value = row[x >> 3];

                return (value & (0x80 >> (x & 7))) != 0;
            }

            // =============================================================
            // =============================================================

            using var file = File.OpenRead(AssetLoader.GetFullResourcePath(relativePath));

            byte[] fontBytes;

            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fontBytes = ms.ToArray();
            }

            LoadFreeType(fontBytes);

            PopulateChars();

            SetFontPixelSize();

            PopulateKernelInfo();

            PopulateAtlasAndGlyphs();

            UnloadFreeType();

            return result;

        }
    }
}
