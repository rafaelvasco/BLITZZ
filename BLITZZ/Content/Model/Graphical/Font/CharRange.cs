using System;
using System.Collections.Generic;

namespace BLITZZ.Content.Font
{
    public readonly struct CharRange
    {
		public static readonly CharRange BasicLatin = new ("BasicLatin", 0x0020, 0x007F);
        public static readonly CharRange Latin1Supplement = new ("Latin1Supplement",0x00A0, 0x00FF);
        public static readonly CharRange LatinExtendedA = new ("LatinExtendedA", 0x0100, 0x017F);
        public static readonly CharRange LatinExtendedB = new ("LatinExtendedB", 0x0180, 0x024F);
        public static readonly CharRange Cyrillic = new ("Cyrillic", 0x0400, 0x04FF);
        public static readonly CharRange CyrillicSupplement = new ("CyrillicSupplement", 0x0500, 0x052F);
        public static readonly CharRange Hiragana = new ("Hiragana", 0x3040, 0x309F);
        public static readonly CharRange Katakana = new ("Katakana", 0x30A0, 0x30FF);
        public static readonly CharRange Greek = new ("Greek", 0x0370, 0x03FF);
        public static readonly CharRange CjkSymbolsAndPunctuation = new ("CjkSymbolsAndPunctuation", 0x3000, 0x303F);
        public static readonly CharRange CjkUnifiedIdeographs = new ("CjkUnifiedIdeographs", 0x4e00, 0x9fff);
        public static readonly CharRange HangulCompatibilityJamo = new ("HangulCompatibilityJamo", 0x3130, 0x318f);
        public static readonly CharRange HangulSyllables = new ("HangulSyllables", 0xac00, 0xd7af);

        private static readonly Dictionary<string, CharRange> Map = new()
        {
            { "BasicLatin", BasicLatin },
            { "Latin1Supplement", Latin1Supplement },
            { "LatinExtendedA", LatinExtendedA },
            { "LatinExtendedB", LatinExtendedB },
            { "Cyrillic", Cyrillic },
            { "CyrillicSupplement", CyrillicSupplement },
            { "Hiragana", Hiragana },
            { "Katakana", Katakana },
            { "Greek", Greek },
            { "CjkSymbolsAndPunctuation", CjkSymbolsAndPunctuation },
            { "CjkUnifiedIdeographs", CjkUnifiedIdeographs },
            { "HangulCompatibilityJamo", HangulCompatibilityJamo },
            { "HangulSyllables", HangulSyllables },
        };

        public static CharRange GetFromKey(string key)
        {
            if (Map.TryGetValue(key, out var charRange))
            {
                return charRange;
            }

            throw new Exception($"Invalid CharRange: {key}");
        }

        public string Name { get; }

        public int Start
        {
            get;
        }

        public int End
        {
            get;
        }

        public int Size => End - Start + 1;

        internal CharRange(string name, int start, int end)
        {
            Start = start;
            End = end;
            Name = name;
        }

        internal CharRange(string name, int single) : this(name, single, single)
        {
        }
	}
}
