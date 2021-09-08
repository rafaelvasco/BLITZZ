using System.IO;
using MessagePack;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BLITZZ.Content.Font;

namespace BLITZZ.Content
{
    internal static class AssetPakLoader
    {
        public static AssetPak Load(Stream stream)
        {
            using var ms = new MemoryStream();
            stream.CopyTo(ms);
            var bytes = ms.ToArray();

            var pakReader = new MessagePackReader(bytes);

            // Read Into Nested Object
            pakReader.ReadArrayHeader();

            var pakeName = pakReader.ReadString();

            var pak = new AssetPak(pakeName);

            // =============================================================
            // IMAGES
            // =============================================================

            int imageCount = pakReader.ReadMapHeader();

            if (imageCount > 0)
            {
                pak.Images = new Dictionary<string, ImageData>();

                for (int i = 0; i < imageCount; ++i)
                {
                    var key = pakReader.ReadString();

                    pakReader.ReadArrayHeader();

                    var imageId = pakReader.ReadString();

                    var imageData = pakReader.ReadBytes();
                    int imageWidth = pakReader.ReadInt32();
                    int imageHeight = pakReader.ReadInt32();

                    pak.Images.Add(key, new ImageData()
                    {
                        Id = imageId,
                        Data = imageData?.ToArray(),
                        Width = imageWidth,
                        Height = imageHeight
                    });
                }
            }

            // =============================================================
            // ATLASES
            // =============================================================

            int atlasCount = pakReader.ReadMapHeader();

            if (atlasCount > 0)
            {
                pak.Atlases = new Dictionary<string, TextureAtlasData>();

                for (int i = 0; i < atlasCount; i++)
                {
                    var key = pakReader.ReadString();
                    pakReader.ReadArrayHeader();
                    var atlasId = pakReader.ReadString();
                    var atlasImageData = pakReader.ReadBytes();
                    int imageWidth = pakReader.ReadInt32();
                    int imageHeight = pakReader.ReadInt32();

                    var atlasData = new TextureAtlasData()
                    {
                        Id = atlasId,
                        Data = atlasImageData?.ToArray(),
                        Atlas = new Dictionary<string, (int X, int Y, int Width, int Height)>(),
                        Width = imageWidth,
                        Height = imageHeight,
                    };

                    int rectsCount = pakReader.ReadMapHeader();

                    if (rectsCount > 0)
                    {
                        for (int j = 0; j < rectsCount; ++j)
                        {
                            var rectKey = pakReader.ReadString();

                            pakReader.ReadArrayHeader();
                            var rectX = pakReader.ReadInt32();
                            var rectY = pakReader.ReadInt32();
                            var rectW = pakReader.ReadInt32();
                            var rectH = pakReader.ReadInt32();

                            atlasData.Atlas.Add(rectKey, (rectX, rectY, rectW, rectH));
                        }
                    }

                    atlasData.RuntimeUpdatable = pakReader.ReadBoolean();


                    pak.Atlases.Add(key, atlasData);
                }
            }

            // =============================================================
            // SHADERS
            // =============================================================

            int shadersCount = pakReader.ReadMapHeader();

            if (shadersCount > 0)
            {
                pak.Shaders = new Dictionary<string, ShaderProgramData>();

                for (int i = 0; i < shadersCount; ++i)
                {
                    var shaderKey = pakReader.ReadString();

                    pakReader.ReadArrayHeader();

                    var shaderId = pakReader.ReadString();
                    var vertexShaderData = pakReader.ReadBytes();
                    var fragShaderData = pakReader.ReadBytes();
                    string[] samplers = null;
                    string[] @params = null;

                    int samplersCount = pakReader.ReadArrayHeader();

                    if (samplersCount > 0)
                    {
                        samplers = new string[samplersCount];

                        for (int j = 0; j < samplersCount; ++j)
                        {
                            samplers[j] = pakReader.ReadString();
                        }
                    }

                    int paramsCount = pakReader.ReadArrayHeader();

                    if (paramsCount > 0)
                    {
                        @params = new string[paramsCount];

                        for (int k = 0; k < paramsCount; ++k)
                        {
                            @params[k] = pakReader.ReadString();
                        }
                    }

                    pak.Shaders.Add(shaderKey, new ShaderProgramData()
                    {
                        Id = shaderId,
                        VertexShader = vertexShaderData?.ToArray(),
                        FragmentShader = fragShaderData?.ToArray(),
                        Samplers = samplers,
                        Params = @params
                    });
                }
            }

            // =============================================================
            // FONTS
            // =============================================================

            int trueTypeFontCount = pakReader.ReadMapHeader();

            if (trueTypeFontCount > 0)
            {
                pak.Fonts = new Dictionary<string, TrueTypeFontData>();

                for (int i = 0; i < trueTypeFontCount; ++i)
                {
                    var fontKey = pakReader.ReadString();

                    pakReader.ReadArrayHeader();

                    var fontId = pakReader.ReadString();

                    var trueTypeFontData = new TrueTypeFontData
                    {
                        Id = fontId,
                        Size = pakReader.ReadInt32(),
                        LineSpacing = pakReader.ReadInt32()
                    };

                    pakReader.ReadArrayHeader();

                    var fontImageDataId = pakReader.ReadString();
                    var fontImageData = pakReader.ReadBytes();
                    int fontImageWidth = pakReader.ReadInt32();
                    int fontImageHeight = pakReader.ReadInt32();

                    trueTypeFontData.FontSheet = new ImageData()
                    {
                        Id = fontImageDataId,
                        Width = fontImageWidth,
                        Height = fontImageHeight,
                        Data = fontImageData?.ToArray()
                    };

                    int charCount = pakReader.ReadArrayHeader();

                    if (charCount > 0)
                    {
                        trueTypeFontData.Chars = new char[charCount];

                        for (int j = 0; j < charCount; ++j)
                        {
                            trueTypeFontData.Chars[j] = pakReader.ReadChar();
                        }
                    }

                    int glyphRectCount = pakReader.ReadArrayHeader();

                    if (glyphRectCount > 0)
                    {
                        trueTypeFontData.GlyphRegions = new SRect[glyphRectCount];

                        for (int k = 0; k < glyphRectCount; ++k)
                        {
                            pakReader.ReadArrayHeader();

                            int x = pakReader.ReadInt32();
                            int y = pakReader.ReadInt32();
                            int w = pakReader.ReadInt32();
                            int h = pakReader.ReadInt32();

                            trueTypeFontData.GlyphRegions[k] = new SRect(x, y, w, h);
                        }
                    }

                    int glyphOffsetsCount = pakReader.ReadArrayHeader();

                    if (glyphOffsetsCount > 0)
                    {
                        trueTypeFontData.GlyphOffsets = new SVector2[glyphOffsetsCount];

                        for (int l = 0; l < glyphOffsetsCount; ++l)
                        {
                            pakReader.ReadArrayHeader();

                            float x = (float) pakReader.ReadDouble();
                            float y = (float) pakReader.ReadDouble();

                            trueTypeFontData.GlyphOffsets[l] = new SVector2(x, y);
                        }
                    }

                    int glyphXAdvancesCount = pakReader.ReadArrayHeader();

                    if (glyphXAdvancesCount > 0)
                    {
                        trueTypeFontData.GlyphXAdvances = new float[glyphXAdvancesCount];

                        for (int k = 0; k < glyphXAdvancesCount; ++k)
                        {
                            float xAdvance = (float) pakReader.ReadDouble();

                            trueTypeFontData.GlyphXAdvances[k] = xAdvance;
                        }
                    }

                    int kerningsCount = pakReader.ReadMapHeader();

                    if (kerningsCount > 0)
                    {
                        trueTypeFontData.GlyphKernings = new Dictionary<int, float>();

                        for (int k = 0; k < kerningsCount; ++k)
                        {
                            int charCode = pakReader.ReadInt32();

                            float kerning = (float) pakReader.ReadDouble();

                            trueTypeFontData.GlyphKernings[charCode] = kerning;
                        }
                    }

                    pak.Fonts.Add(fontKey, trueTypeFontData);
                }
            }

            int bitmapFontCount = pakReader.ReadMapHeader();

            if (bitmapFontCount > 0)
            {
                pak.BitmapFonts = new Dictionary<string, BitmapFontData>();

                for (int i = 0; i < bitmapFontCount; ++i)
                {
                    var fontKey = pakReader.ReadString();

                    pakReader.ReadArrayHeader();

                    var fontId = pakReader.ReadString();

                    var bitmapFontData = new BitmapFontData
                    {
                        Id = fontId,
                        Size = pakReader.ReadInt32(),
                        LineSpacing = pakReader.ReadInt32()
                    };

                    pakReader.ReadArrayHeader();

                    var fontImageDataId = pakReader.ReadString();
                    var fontImageData = pakReader.ReadBytes();
                    int fontImageWidth = pakReader.ReadInt32();
                    int fontImageHeight = pakReader.ReadInt32();

                    bitmapFontData.FontSheet = new ImageData()
                    {
                        Id = fontImageDataId,
                        Width = fontImageWidth,
                        Height = fontImageHeight,
                        Data = fontImageData?.ToArray()
                    };

                    int charCount = pakReader.ReadArrayHeader();

                    if (charCount > 0)
                    {
                        bitmapFontData.Chars = new char[charCount];

                        for (int j = 0; j < charCount; ++j)
                        {
                            bitmapFontData.Chars[j] = pakReader.ReadChar();
                        }
                    }

                    int glyphRectCount = pakReader.ReadArrayHeader();

                    if (glyphRectCount > 0)
                    {
                        bitmapFontData.GlyphRegions = new SRect[glyphRectCount];

                        for (int k = 0; k < glyphRectCount; ++k)
                        {
                            pakReader.ReadArrayHeader();

                            int x = pakReader.ReadInt32();
                            int y = pakReader.ReadInt32();
                            int w = pakReader.ReadInt32();
                            int h = pakReader.ReadInt32();

                            bitmapFontData.GlyphRegions[k] = new SRect(x, y, w, h);
                        }
                    }

                    int glyphOffsetsCount = pakReader.ReadArrayHeader();

                    if (glyphOffsetsCount > 0)
                    {
                        bitmapFontData.GlyphOffsets = new SVector2[glyphOffsetsCount];

                        for (int l = 0; l < glyphOffsetsCount; ++l)
                        {
                            pakReader.ReadArrayHeader();

                            float x = (float) pakReader.ReadDouble();
                            float y = (float) pakReader.ReadDouble();

                            bitmapFontData.GlyphOffsets[l] = new SVector2(x, y);
                        }
                    }

                    int glyphXAdvancesCount = pakReader.ReadArrayHeader();

                    if (glyphXAdvancesCount > 0)
                    {
                        bitmapFontData.GlyphXAdvances = new float[glyphXAdvancesCount];

                        for (int k = 0; k < glyphXAdvancesCount; ++k)
                        {
                            pakReader.ReadArrayHeader();

                            float xAdvance = (float) pakReader.ReadDouble();

                            bitmapFontData.GlyphXAdvances[k] = xAdvance;
                        }
                    }

                    int kerningsCount = pakReader.ReadArrayHeader();

                    if (kerningsCount > 0)
                    {
                        bitmapFontData.GlyphKernings = new (char First, char Second, float Amount)[kerningsCount];

                        for (int k = 0; k < kerningsCount; ++k)
                        {
                            char first = pakReader.ReadChar();
                            char second = pakReader.ReadChar();
                            float amount = (float) pakReader.ReadDouble();

                            bitmapFontData.GlyphKernings[k] = (first, second, amount);
                        }
                    }

                    pak.BitmapFonts.Add(fontKey, bitmapFontData);
                }
            }

            // =============================================================
            // TEXTS
            // =============================================================

            int textCount = pakReader.ReadMapHeader();

            if (textCount > 0)
            {
                pak.TextFiles = new Dictionary<string, TextFileData>();

                var textKey = pakReader.ReadString();

                pakReader.ReadArrayHeader();

                var textId = pakReader.ReadString();

                var textBytes = pakReader.ReadBytes();

                if (textBytes.HasValue)
                {
                    var textData = new TextFileData()
                    {
                        Id = textId,
                        TextData = textBytes.Value.ToArray()

                    };

                    pak.TextFiles.Add(textKey, textData);
                }

               
            }

            // =============================================================
            // AUDIO FILES COUNT
            // =============================================================

            int audioFilesCount = pakReader.ReadMapHeader();

            if (audioFilesCount > 0)
            {
                pak.AudioFiles = new Dictionary<string, AudioFileData>();

                var audioFileKey = pakReader.ReadString();

                pakReader.ReadArrayHeader();

                var audioFileId = pakReader.ReadString();

                var dataLength = pakReader.ReadArrayHeader();

                var audioFileData = new AudioFileData()
                {
                    Id = audioFileId,
                    Data = new byte[dataLength]
                };

                var data = pakReader.ReadBytes().Value.ToArray();

                Unsafe.CopyBlockUnaligned(ref audioFileData.Data[0], ref data[0], (uint) dataLength);

                audioFileData.Streamed = pakReader.ReadBoolean();

                pak.AudioFiles.Add(audioFileKey, audioFileData);
            }

            pak.TotalResourcesCount = pakReader.ReadInt32();

            return pak;
        }

        public static AssetPak Load(string path)
        {
            using var stream = File.OpenRead(path);

            return Load(stream);
        }
    }
}