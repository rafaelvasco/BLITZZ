using System.IO;
using MessagePack;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BLITZZ.Content
{
    internal static class AssetPakLoader
    {
        public static ResourcePak Load(string path)
        {
            var fileBytes = File.ReadAllBytes(path);

            var pakReader = new MessagePackReader(fileBytes);

            // Read Into Nested Object
            pakReader.ReadArrayHeader();

            var pakeName = pakReader.ReadString();

            var pak = new ResourcePak(pakeName);

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

            int fontsCount = pakReader.ReadMapHeader();

            if (fontsCount > 0)
            {
                pak.Fonts = new Dictionary<string, TrueTypeFontData>();

                for (int i = 0; i < fontsCount; ++i)
                {
                    var fontKey = pakReader.ReadString();

                    pakReader.ReadArrayHeader();

                    var fontId = pakReader.ReadString();

                    var fontData = new TrueTypeFontData()
                    {
                        Id = fontId
                    };

                    pakReader.ReadArrayHeader();

                    var fontImageDataId = pakReader.ReadString();
                    var fontImageData = pakReader.ReadBytes();
                    int fontImageWidth = pakReader.ReadInt32();
                    int fontImageHeight = pakReader.ReadInt32();

                    fontData.FontSheet = new ImageData()
                    {
                        Id = fontImageDataId,
                        Width = fontImageWidth,
                        Height = fontImageHeight,
                        Data = fontImageData?.ToArray()
                    };

                    int charCount = pakReader.ReadArrayHeader();

                    if (charCount > 0)
                    {
                        fontData.Chars = new char[charCount];

                        for (int j = 0; j < charCount; ++j)
                        {
                            fontData.Chars[j] = pakReader.ReadChar();
                        }
                    }

                    int glyphRectCount = pakReader.ReadArrayHeader();

                    if (glyphRectCount > 0)
                    {
                        fontData.GlyphRects = new (int X, int Y, int Width, int Height)[glyphRectCount];

                        for (int k = 0; k < glyphRectCount; ++k)
                        {
                            pakReader.ReadArrayHeader();

                            int x = pakReader.ReadInt32();
                            int y = pakReader.ReadInt32();
                            int w = pakReader.ReadInt32();
                            int h = pakReader.ReadInt32();

                            fontData.GlyphRects[k] = (x, y, w, h);
                        }
                    }

                    int glyphCroppingsCount = pakReader.ReadArrayHeader();

                    if (glyphCroppingsCount > 0)
                    {
                        fontData.GlyphCroppings = new (int X, int Y, int Width, int Height)[glyphCroppingsCount];

                        for (int l = 0; l < glyphCroppingsCount; ++l)
                        {
                            pakReader.ReadArrayHeader();

                            int x = pakReader.ReadInt32();
                            int y = pakReader.ReadInt32();
                            int w = pakReader.ReadInt32();
                            int h = pakReader.ReadInt32();

                            fontData.GlyphCroppings[l] = (x, y, w, h);
                        }
                    }

                    int glyphKerningsCount = pakReader.ReadArrayHeader();

                    if (glyphKerningsCount > 0)
                    {
                        fontData.GlyphKernings = new (float X, float Y, float Z)[glyphKerningsCount];

                        for (int k = 0; k < glyphKerningsCount; ++k)
                        {
                            pakReader.ReadArrayHeader();

                            float x = (float) pakReader.ReadDouble();
                            float y = (float)pakReader.ReadDouble();
                            float z = (float)pakReader.ReadDouble();

                            fontData.GlyphKernings[k] = new (x, y, z);
                        }
                    }

                    fontData.LineSpacing = pakReader.ReadInt32();
                    fontData.Spacing = pakReader.ReadInt32();
                    fontData.DefaultChar = pakReader.ReadChar();

                    pak.Fonts.Add(fontKey, fontData);
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

                int textRowCount = pakReader.ReadArrayHeader();

                var textData = new TextFileData()
                {
                    Id = textId,
                    TextData = new byte[textRowCount][]
                };

                for (int i = 0; i < textRowCount; ++i)
                {
                    textData.TextData[i] = pakReader.ReadBytes().Value.ToArray();
                }

                pak.TextFiles.Add(textKey, textData);
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

                Unsafe.CopyBlockUnaligned(ref audioFileData.Data[0], ref data[0], (uint)dataLength);

                audioFileData.Streamed = pakReader.ReadBoolean();

                pak.AudioFiles.Add(audioFileKey, audioFileData);
            }

            pak.TotalResourcesCount = pakReader.ReadInt32();

            return pak;
        }
    }
}
