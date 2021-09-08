using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using BLITZZ.Logging;

namespace BLITZZ.Content.Font
{
    public static class BitmapFontBuilder
    {
        private static Dictionary<string, Action> _parsers;
        private static BitmapFontLexer _lexer;

        private static readonly Log _log = LogManager.GetForCurrentAssembly();

        public static BitmapFontData Build(string id, string relativePath)
        {
            var fontFilePath = AssetLoader.GetFullResourcePath(relativePath);

            var lines = File.ReadAllLines(fontFilePath);

            int size = 0;
            Vector2 spacing = Vector2.Zero;
            int lineHeight = 0;
            var atlasData = new ImageData();
            var chars = new List<char>();
            var regions = new List<SRect>();
            var offsets = new List<SVector2>();
            var xAdvances = new List<float>();
            List<(char First, char Second, float Amount)> kernings = new();

            var result = new BitmapFontData();

            void ParseFontInformation()
            {
                while (true)
                {
                    switch (_lexer.CurrentKey)
                    {
                        case "size":
                            size = GetInteger(_lexer.CurrentValue);
                            break;
                        case "spacing":
                            spacing = GetSpacing(_lexer.CurrentValue);
                            break;
                    }

                    if (_lexer.IsEOL) break;

                    _lexer.Next();
                }
            }

            void ParseFontCommons()
            {
                while (true)
                {
                    lineHeight = _lexer.CurrentKey switch
                    {
                        "lineHeight" => GetInteger(_lexer.CurrentValue),
                        _ => lineHeight
                    };

                    if (_lexer.IsEOL) break;

                    _lexer.Next();
                }
            }

            void ParsePage()
            {
                var pageId = -1;
                var atlasFilePath = string.Empty;

                while (true)
                {
                    switch (_lexer.CurrentKey)
                    {
                        case "id":
                            pageId = GetInteger(_lexer.CurrentValue);
                            break;

                        case "file":
                            atlasFilePath = Path.Combine(
                                Path.GetDirectoryName(fontFilePath)!,
                                _lexer.CurrentValue
                            );
                            break;

                        default:
                            _log.Warning($"Unexpected page definition parameter '{_lexer.CurrentKey}'.");
                            break;
                    }

                    if (_lexer.IsEOL) break;
                    _lexer.Next();
                }

                switch (pageId)
                {
                    case < 0:
                        _log.Warning("Failed to parse page definition. Invalid page definition line?");
                        return;
                    case 0:
                        atlasData = ImageBuilder.BuildFromFullPath(id + "_atlas", atlasFilePath);
                        break;
                    default:
                        _log.Warning("Bitmap Fonts with more than one page are not supported.");
                        break;
                }
            }


            void ParseChar()
            {
                int x = 0, y = 0, w = 0, h = 0;
                float offsetX = 0, offsetY = 0;
                float xAdvance = 0;

                while (true)
                {
                    switch (_lexer.CurrentKey)
                    {
                        case "id":
                            chars.Add((char) GetInteger(_lexer.CurrentValue));
                            break;

                        case "x":
                            x = GetInteger(_lexer.CurrentValue);

                            break;

                        case "y":
                            y = GetInteger(_lexer.CurrentValue);
                            break;

                        case "width":
                            w = GetInteger(_lexer.CurrentValue);
                            break;

                        case "height":
                            h = GetInteger(_lexer.CurrentValue);
                            break;

                        case "xoffset":
                            offsetX = GetInteger(_lexer.CurrentValue);
                            break;

                        case "yoffset":
                            offsetY = GetInteger(_lexer.CurrentValue);
                            break;

                        case "xadvance":
                            xAdvance = GetInteger(_lexer.CurrentValue);
                            break;

                        default:
                            _log.Warning($"Unexpected glyph definition parameter '{_lexer.CurrentKey}'.");
                            break;
                    }

                    if (_lexer.IsEOL) break;
                    _lexer.Next();
                }

                regions.Add(new SRect(x, y, w, h));
                offsets.Add(new SVector2(offsetX, offsetY));
                xAdvances.Add(xAdvance);

            }


            void ParseKerningInfo()
            {

                char first = char.MinValue;
                char second = char.MinValue;
                float amount = 0;

                while (true)
                {
                    switch (_lexer.CurrentKey)
                    {
                        case "first":
                            first = (char) GetInteger(_lexer.CurrentValue);
                            break;

                        case "second":
                            second = (char) GetInteger(_lexer.CurrentValue);
                            break;

                        case "amount":
                            amount = GetInteger(_lexer.CurrentValue);
                            break;
                    }

                    if (_lexer.IsEOL) break;
                    _lexer.Next();
                }

                kernings.Add((first, second, amount));
            }


            static int GetInteger(string value)
                => int.Parse(value);

            static Vector2 GetSpacing(string value)
            {
                var values = value.Split(',').Select(int.Parse).ToArray();
                return new Vector2(values[0], values[1]);
            }

            _parsers ??= new Dictionary<string, Action>
            {
                {"info", ParseFontInformation},
                {"common", ParseFontCommons},
                {"page", ParsePage},
                {"char", ParseChar},
                {"kerning", ParseKerningInfo}
            };

            void ParseFontDefinition()
            {
                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    var words = line.Trim().Split(' ').ToList();
                    words.RemoveAll(string.IsNullOrWhiteSpace);

                    var verb = words[0];
                    var arguments = string.Join(' ', words.Skip(1));

                    if (_parsers.ContainsKey(verb))
                    {
                        _lexer = new BitmapFontLexer(arguments);
                        _parsers[verb]();
                    }
                    else
                    {
                        _log.Warning($"Unexpected BMFont block '{verb}'");
                    }
                }
            }

            ParseFontDefinition();

            result.Id = id;
            result.Size = size;
            result.LineSpacing = (int) (lineHeight + spacing.Y);
            result.FontSheet = atlasData;
            result.Chars = chars.ToArray();
            result.GlyphKernings = kernings.ToArray();
            result.GlyphRegions = regions.ToArray();
            result.GlyphOffsets = offsets.ToArray();
            result.GlyphXAdvances = xAdvances.ToArray();

            return result;
        }
    }
}