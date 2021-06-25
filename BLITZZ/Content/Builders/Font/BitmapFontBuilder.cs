using System;
using System.Collections.Generic;
using System.IO;

namespace BLITZZ.Content.Font
{
    public static class BitmapFontBuilder
    {
        private static Dictionary<string, Action> _parsers;
        private static BitmapFontLexer _lexer;

        public static BitmapFontData Build(string id, string relativePath)
        {
           
            var lines = File.ReadAllLines(AssetLoader.GetFullResourcePath(relativePath));

            BitmapFontInfo info;
            BitmapFontCommon common;
        }
    }
}
